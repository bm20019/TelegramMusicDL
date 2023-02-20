using System.Text.RegularExpressions;

namespace ConvertAudioLibrary
{
    public class Metadata
    {
        public Metadata() { }
        public Metadata(DateTime DateTagged,string id, string? Title, string? Subititle, string? Album, string[]? Performers, string? Comment, string[]? Composers,
        uint Disc, uint DiscCount, uint Track, uint TrackCount, uint Year, string? Lyrics, string[]? Genres,
        string? ISRC = "", string? Conductor = "", string? Copyright = "", string? Grouping = "", string? Publisher = "",
        string? PicturePath = "")
        {
            this.Album = Album;
            this.Comment = Comment;
            this.Composers = Composers;
            this.Conductor = Conductor;
            this.Copyright = Copyright;
            this.DateTagged = DateTagged;
            this.Disc = Disc;
            this.DiscCount = DiscCount;
            this.Genres = Genres;
            this.Grouping = Grouping;
            this.ISRC = ISRC;
            this.Lyrics = Lyrics;
            this.Performers = Performers;
            this.PicturePath = PicturePath;
            this.Publisher = Publisher;
            this.Subtitle = Subititle;
            this.Title = Title;
            this.Track = Track;
            this.TrackCount = TrackCount;
            this.Year = Year;
            this.id = id;
        }

        public Metadata(DeezerApi.Deezer deezer, string CovertArtPath)
        {
            this.Album = deezer.album.title;
            this.Composers = deezer.contributors.Select(x => x.name).ToArray();
            this.Conductor = deezer.artist.name;
            this.DateTagged = DateTime.Now;
            this.Disc = (uint)deezer.disk_number;
            this.ISRC = deezer.isrc;
            this.Performers = deezer.contributors.Select(x => x.name).ToArray();
            this.PicturePath = CovertArtPath;
            this.Title = deezer.title;
            this.Track = (uint)deezer.track_position;
            this.Year = (uint)deezer.release_date.Year;
            this.Duracion = deezer.duration;
            this.id = deezer.id.ToString();
        }

        public Metadata(SpotifyClass.ModelSpotify modelSpotify, string CovertArtPath){

            this.Album = modelSpotify.GetAlbumName();
            this.Composers = modelSpotify.GetTrackArtist();
            this.Conductor = modelSpotify.GetTrackArtist()[0];
            this.DateTagged = modelSpotify.GetDateTimeUTC();
            this.Disc = modelSpotify.GetDiscNumber();
            this.Duracion = (int)modelSpotify.GetDuration();
            this.Genres = modelSpotify.GetGenresTrack();
            this.Copyright = string.Join(',',modelSpotify.GetCopyrights());
            this.id = modelSpotify.GetIdSong();
            this.ISRC = modelSpotify.GetISRC_Track();
            this.Performers = modelSpotify.GetTrackArtist();
            this.PicturePath = CovertArtPath;
            this.Publisher =modelSpotify.GetPublisher();
            this.Title = modelSpotify.GetTitle();
            this.Track = modelSpotify.GetTrackNumber();
            this.TrackCount = modelSpotify.GetTrackCount();
            this.Year = modelSpotify.GetYear();
        }
        public string id { get; set; }
        public int Duracion { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Album { get; set; }
        public string[]? Performers { get; set; }
        public string? Comment { get; set; }
        public string[]? Composers { get; set; }
        public uint Disc { get; set; }
        public uint DiscCount { get; set; }
        public uint TrackCount { get; set; }
        public uint Track { get; set; }
        public uint Year { get; set; }
        public string? Lyrics { get; set; }
        public string[]? Genres { get; set; }
        public string? ISRC { get; set; }
        public string? Conductor { get; set; }
        public string? Copyright { get; set; }
        public DateTime DateTagged { get; set; }
        public string? Grouping { get; set; }
        public string? Publisher { get; set; }
        public string? PicturePath { get; set; }
    }
}