using System.Net;
using ConvertAudioLibrary;
using DeezerApi;
using SpotifyAPI.Web;
using MusixmathSearch;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YTMUSICAPI;
using Bitrate = ConvertAudioLibrary.Bitrate;

namespace MusicDownloaderLibrary
{
    public class Download
    {
        string path { get; set; }
        Metadata metadata { get; set; }
        Config config;
        const string extension = ".spotdlSharp";
        private string PictureArtUrl;
        public Download()
        {
            config = new Config();
            config.Inicializar();
        }

        Action<Uri, string> downloadPictureUri = delegate (Uri url, string outputFile)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36");
            wc.DownloadFile(url, outputFile);
        };
        private string GetLyrics(string Query)
        {
            Musixmath musixmath = new Musixmath();
            return musixmath.SearchLyrics(Query);
        }
        string GetTempFolder()
        {
            return config.GetFolderTemp();
        }
        private async Task DownloaderMusicAsync(string urlMusic, DirectoryInfo directoryOutput, Bitrate bitrate = Bitrate.btr_256, ConvertAudioLibrary.Codecs codec = ConvertAudioLibrary.Codecs.libmp3lame, string pattern = "{trackNumber} {title}")
        {
            if (string.IsNullOrEmpty(urlMusic) ||
               string.IsNullOrWhiteSpace(urlMusic))
                throw new Exception("Is Null, Empy or WhiteSpace");

            string Query = "", QueryLyrics = "", ruta = "", urlDownloader = "";
            int timeOriginal = -1;
            Metadata datos = new Metadata();
            Servidor server = Utils.UrlServerIs(urlMusic);
            SearchYtMusic sytMusic = new SearchYtMusic();
            string pathPicute = "";
            if (server == Servidor.Spotify)
            {
                config.InizializarToken();
                SpotifyClient spotifyClient = config.GetSpotifyClient();
                SpotifyClass.SpotifyClass spotifyClass = new SpotifyClass.SpotifyClass(spotifyClient);
                SpotifyClass.ModelSpotify modelSpotify = spotifyClass.GetFullData(urlMusic);
                Query = $"{modelSpotify.GetTitle()} - {modelSpotify.GetTrackArtist()[0]}";
                QueryLyrics = $"{modelSpotify.GetTitle()} - {modelSpotify.GetTrackArtist()[0]}";
                timeOriginal = Convert.ToInt32(modelSpotify.GetDuration());
                pathPicute = Path.Combine(GetTempFolder(), modelSpotify.GetIdSong() + ".jpeg");
                PictureArtUrl = modelSpotify.GetCovertArtMax().AbsoluteUri;
                downloadPictureUri(new Uri(modelSpotify.GetCovertArtMax().AbsoluteUri), pathPicute);
                datos = new Metadata(modelSpotify, pathPicute);
                urlDownloader = sytMusic.GetUrlMusic(Query, timeOriginal, YTMUSICAPI.Provider.YtVideo);
            }
            else if (server == Servidor.Deezer)
            {
                DeezerClass DC = new DeezerClass();
                Deezer? dz = DC.GetDeezer(urlMusic);
                if (dz == null)
                    throw new Exception("Deezer return null");
                Query = $"{dz.title} - {dz.artist.name}";
                QueryLyrics = $"{dz.title} - {dz.artist.name}";
                timeOriginal = Convert.ToInt32(dz.duration);
                pathPicute = Path.Combine(GetTempFolder(), dz.id + ".jpeg");
                PictureArtUrl = dz.album.cover_big.AbsoluteUri;
                downloadPictureUri(dz.album.cover_big, pathPicute);
                datos = new Metadata(dz, pathPicute);
                urlDownloader = sytMusic.GetUrlMusic(Query, timeOriginal, YTMUSICAPI.Provider.YtVideo);
            }
            else if (server == Servidor.YouTubeVideo)
            {
                YoutubeClient youtube = new YoutubeClient();
                var videos = await youtube.Videos.GetAsync(urlMusic);
                pathPicute = Path.Combine(GetTempFolder(), Guid.NewGuid().ToString() + ".jpeg");
                PictureArtUrl = "https://www.gstatic.com/youtube/img/web/monochrome/logo_512x512.png";
                downloadPictureUri(new Uri(PictureArtUrl), pathPicute);
                datos = new Metadata(config.GetDateTimeWordl(), Guid.NewGuid().ToString(), videos.Title, "", "Unknown", new string[] { videos.Author.ChannelTitle }, "", new string[] { videos.Author.ChannelTitle }, 0, 0, 1, 0, 0, "", new string[] { "Unknown" }, "", "", "", "", "", pathPicute);
                urlDownloader = urlMusic;
            }
            else if (server == Servidor.youtubeMusic)
            {
                urlMusic = urlMusic.Replace("music.youtube.com", "www.youtube.com");
                SearchYtMusic SytMusic = new SearchYtMusic();
                MusicData md = SytMusic.GeMusicDataforUrl(urlMusic.Replace("www.youtube.com", "music.youtube.com")).Result;
                pathPicute = Path.Combine(GetTempFolder(), Guid.NewGuid().ToString() + ".jpeg");
                PictureArtUrl = md.PictureUrl;
                downloadPictureUri(new Uri(PictureArtUrl), pathPicute);
                datos = new Metadata(config.GetDateTimeWordl(), md.Id, md.Title, "", md.Album, new string[] { md.Artist }, "", new string[] { md.Artist }, 0, 0, 1, 0, Convert.ToUInt32(md.Year), "", new string[] { "Unknown" }, "", "", "", "", "", pathPicute);
                urlDownloader = urlMusic;
                QueryLyrics = $"{datos.Title} - {datos.Performers[0]}";
            }
            else
            {
                Console.WriteLine("La url no pertence a Spotify, Deezer, Youtube o YouTubeMusic");
                return;
            }
            //Get Lyrics
            try
            {
                datos.Lyrics = GetLyrics(QueryLyrics);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Musixmatch not Lyrics found\nSearch with Genius");
                GeniusLyricsLibrary.GeniusApi Ga = new GeniusLyricsLibrary.GeniusApi();
                try
                {
                    string lyrics = await Ga.GetLyricsforSearch("Hermoso nombre Hillsong");
                    datos.Lyrics = lyrics;
                }
                catch (Exception exp)
                {
                    Console.WriteLine("Lyrics not Found");
                }
            }

            //Descargar File
            datos.Comment = urlDownloader;
            datos.Title = datos.Title.Replace('/', '_').Replace('\\', '_');
            ruta = Path.Combine(GetTempFolder(), datos.id + extension);
            await DownloadAudio(urlDownloader, ruta);
            //Convertir File
            ConvertAudio ca = new ConvertAudio();
            string output = Path.Combine(directoryOutput.FullName, pattern);
            string outputFinal = await ca.ConversionAsync(ruta, output, datos, codec, bitrate);
            metadata = datos;
            path = outputFinal;
            //Incrustar metadatos
            Console.WriteLine("Exito");
        }

        public Metadata GetDataInfo(string urlMusic)
        {
            if (string.IsNullOrEmpty(urlMusic) ||
               string.IsNullOrWhiteSpace(urlMusic))
                throw new Exception("Is Null, Empy or WhiteSpace");
            Servidor server = Utils.UrlServerIs(urlMusic);
            if (server == Servidor.Spotify)
            {
                config.InizializarToken();
                SpotifyClient spotifyClient = config.GetSpotifyClient();
                SpotifyClass.SpotifyClass spotifyClass = new SpotifyClass.SpotifyClass(spotifyClient);
                SpotifyClass.ModelSpotify modelSpotify = spotifyClass.GetFullData(urlMusic);
                PictureArtUrl = modelSpotify.GetCovertArtMax().AbsoluteUri;
                return new Metadata(modelSpotify, PictureArtUrl);
            }
            else if (server == Servidor.Deezer)
            {
                DeezerClass dc = new DeezerClass();
                Deezer? dz = dc.GetDeezer(urlMusic);
                if (dz == null)
                    throw new Exception("Deezer return null");
                PictureArtUrl = dz.album.cover_big.AbsoluteUri;
                return new Metadata(dz, PictureArtUrl);
            }
            else if (server == Servidor.YouTubeVideo)
            {
                var youtube = new YoutubeClient();
                var video = youtube.Videos.GetAsync(urlMusic).Result;
                PictureArtUrl = "https://www.gstatic.com/youtube/img/web/monochrome/logo_512x512.png";
                return new Metadata(config.GetDateTimeWordl(), Guid.NewGuid().ToString(), video.Title, "", "Unknown", new string[] { video.Author.ChannelTitle }, "", new string[] { video.Author.ChannelTitle }, 0, 0, 0, 0, 0, "", new string[] { "Unknown" }, "", "", "", "", "", PictureArtUrl);
            }
            else if (server == Servidor.youtubeMusic)
            {
                SearchYtMusic sytMusic = new SearchYtMusic();
                MusicData md = sytMusic.GeMusicDataforUrl(urlMusic.Replace("www.youtube.com", "music.youtube.com")).Result;
                PictureArtUrl = md.PictureUrl;
                Console.WriteLine(md.Year);
                return new Metadata(config.GetDateTimeWordl(), md.Id, md.Title, "", md.Album, new string[] { md.Artist }, "", new string[] { md.Artist }, 0, 0, 1, 0, Convert.ToUInt32(md.Year), "", new string[] { "Unknown" }, "", "", "", "", "", md.PictureUrl);
            }
            else
            {
                string urlNull = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/36/NULL.jpg/640px-NULL.jpg";
                return new Metadata(config.GetDateTimeWordl(), Guid.NewGuid().ToString(), "Null", "Null", "Null", new string[] { "null" }, "null", new string[] { "null" }, 0, 0, 0, 0, 0, "null", new string[] { "null" }, "null", "null", "null", "null", "null", urlNull);
            }
        }
        public async Task MusicDownload(string urlMusic,
                                    DirectoryInfo directoryOutput,
                                    Bitrate bitrate = Bitrate.btr_256,
                                    Codecs codec = Codecs.libmp3lame,
                                    string pattern = "{trackNumber} {title}")
        {
            try
            {
                await DownloaderMusicAsync(urlMusic, directoryOutput, bitrate, codec, pattern);
                RemoveFiles();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Delay(5000);
                await DownloaderMusicAsync(urlMusic, directoryOutput, bitrate, codec, pattern);
                RemoveFiles();
            }
        }

        private void RemoveFiles()
        {
            string[] files = Directory.GetFiles(GetTempFolder());
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }
        public string PathFileOutput()
        {
            return path;
        }
        public string GetCovertArtMax()
        {
            return PictureArtUrl;
        }
        private async Task DownloadAudio(string url, string pathOutput)
        {
            YoutubeClient youtube = new YoutubeClient();
            StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync(url);
            IStreamInfo streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            IProgress<double> progress = new Progress<double>(s => Console.Write(String.Format("{0}/{1}MB...\r", Convert.ToInt32(s * 100), streamInfo.Size.MegaBytes)));
            await youtube.Videos.Streams.DownloadAsync(streamInfo, pathOutput, progress);
        }
    }
}