using Xabe.FFmpeg;

namespace ConvertAudioLibrary
{
    public class ConvertAudio
    {
        private void TagSet(string pathFile, Metadata metadata)
        {
            TagLib.File file = TagLib.File.Create(pathFile);
            TagLib.Id3v2.Tag.DefaultVersion=3;
            TagLib.Id3v2.Tag.ForceDefaultVersion=true;
            TagLib.Id3v2.Tag.DefaultEncoding = TagLib.StringType.UTF16;
            TagLib.Id3v2.Tag.ForceDefaultEncoding = true;
            TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag)file.GetTag(TagLib.TagTypes.Id3v2);

            tag.Title = metadata.Title;
            tag.Album = metadata.Album;
            tag.Performers = metadata.Performers;
            tag.AlbumArtists = metadata.Performers;
            tag.Comment = metadata.Comment;
            tag.Composers = metadata.Composers;
            tag.Disc = metadata.Disc;
            tag.DiscCount = metadata.DiscCount;
            tag.TrackCount = metadata.TrackCount;
            tag.Track = metadata.Track;
            tag.Year = metadata.Year;
            tag.Lyrics = metadata.Lyrics;
            tag.Genres = metadata.Genres;
            tag.ISRC = metadata.ISRC;
            tag.Conductor = metadata.Conductor;
            tag.Copyright = metadata.Copyright;
            tag.DateTagged = metadata.DateTagged;
            tag.Grouping = metadata.Grouping;
            tag.Publisher = metadata.Publisher;
            if (metadata.PicturePath != null)
            {
                TagLib.IPicture[] pictureArt = { new TagLib.Picture(metadata.PicturePath) };
                tag.Pictures = pictureArt;
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
        string[] modelos = { "{title}", "{album}", "{artist}", "{trackNumber}"};

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