using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YTMUSICAPI
{
    public class DownloadMusic
    {
        public async Task DescargarMusica(string Query, string output, int OriginalSS)
        {
            YTMUSICAPI.SearchYtMusic SytM = new YTMUSICAPI.SearchYtMusic();
            string url = SytM.GetUrlMusic(Query, OriginalSS, Provider.YtVideo);
            await Descargar(url, output);
        }

        private async Task Descargar(string urlYoutube, string output)
        {
            YoutubeClient youtube = new YoutubeClient();
            StreamManifest streamManifest = await youtube.Videos.Streams.GetManifestAsync(urlYoutube);
            IStreamInfo streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            IProgress<double> progress = new Progress<double>(s => Console.Write(String.Format("{0}/{1}MB...\r",Convert.ToInt32(s*100),streamInfo.Size.MegaBytes)));
            await youtube.Videos.Streams.DownloadAsync(streamInfo, output,progress);
        }
    }
}