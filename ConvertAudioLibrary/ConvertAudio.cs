using TagLib.Id3v2;
using Xabe.FFmpeg;

namespace ConvertAudioLibrary
{
    public class ConvertAudio
    {
        public void TagSet(string pathFile, Metadata metadata)
        {
            Tag.DefaultEncoding = TagLib.StringType.UTF8;
            Tag.ForceDefaultEncoding = true;
            Tag.DefaultVersion = 3;
            TagLib.File file = TagLib.File.Create(pathFile);
            file.Tag.Title = metadata.Title;
            file.Tag.Album = metadata.Album;
            file.Tag.Performers = metadata.Performers;
            file.Tag.AlbumArtists = metadata.Performers;
            file.Tag.Comment = metadata.Comment;
            file.Tag.Composers = metadata.Composers;
            file.Tag.Disc = metadata.Disc;
            file.Tag.DiscCount = metadata.DiscCount;
            file.Tag.TrackCount = metadata.TrackCount;
            file.Tag.Track = metadata.Track;
            file.Tag.Year = metadata.Year;
            file.Tag.Lyrics = metadata.Lyrics;
            file.Tag.Genres = metadata.Genres;
            file.Tag.ISRC = metadata.ISRC;
            file.Tag.Conductor = metadata.Conductor;
            file.Tag.Copyright = metadata.Copyright;
            file.Tag.DateTagged = metadata.DateTagged;
            file.Tag.Grouping = metadata.Grouping;
            file.Tag.Publisher = metadata.Publisher;
            
            if (metadata.PicturePath != null)
            {
                TagLib.IPicture[] pictureArt = { new TagLib.Picture(metadata.PicturePath) };
                file.Tag.Pictures = pictureArt;
            }
            file.Save();
        }

        private static string ChangeExtension(string nombreBase, Codecs codecs)
        {
            switch (codecs)
            {
                case Codecs.aac:
                    return nombreBase + ".m4a";
                case Codecs.flac:
                    return nombreBase + ".flac";
                case Codecs.libmp3lame:
                    return nombreBase + ".mp3";
                case Codecs.libopus:
                    return nombreBase + ".opus";
                case Codecs.libvorbis:
                    return nombreBase + ".ogg";
                default:
                    return nombreBase;
            }
        }

        public async Task<string> ConversionAsync(string fileAudioPath, string fileAudioOutputPath, Metadata datos, Codecs codec, Bitrate bitrate)
        {
            string codecs = Enum.GetName(codec).ToString();
            int btr = ((int)bitrate);
            string outputPath = ChangeExtension(name(fileAudioOutputPath,datos), codec);
            string arguments = $"-i \"{fileAudioPath}\" -acodec {codecs} -b:a {btr}k -y \"{outputPath}\"";
            Console.WriteLine($"ffmpeg {arguments}");
            await FFmpeg.Conversions.New().Start(arguments);
            Console.WriteLine("Conversion Lista...");
            metadata = datos;
            TagSet(outputPath,datos);
            return outputPath;
        }
        private Metadata metadata {get;set;}
        public Metadata GetMetadata(){
            return metadata;
        }
        string[] modelos = { "{title}", "{album}", "{artist}","{trackNumber}"};

        private string name(string input, Metadata datos){
            //Title
            if(input.Contains(modelos[0]))
               input = input.Replace(modelos[0],datos.Title);
            //Album
            if(input.Contains(modelos[1]))
                input = input.Replace(modelos[1],datos.Album);
            //Artist
            if(input.Contains(modelos[2]))
                if(datos.Composers.Length>0)
                    input = input.Replace(modelos[2],datos.Composers[0]);
                else 
                    input = input.Replace(modelos[2],"Null");
            //TrackNumber
            if(input.Contains(modelos[3]))
                input = input.Replace(modelos[3],datos.Track.ToString());

            return input;
        }
    }
}