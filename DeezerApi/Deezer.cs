namespace DeezerApi
{
    public class Deezer
    {
        public int id {get;set;}
        public bool readable {get;set;}
        public string title{get;set;}
        public string isrc {get;set;}
        public Uri link {get;set;}
        public Uri share {get;set;}
        public int duration {get;set;}
        public int track_position {get;set;}
        public int disk_number {get;set;}
        public DateTime release_date {get;set;}
        public bool explicit_lyrics {get;set;}
        public Uri preview {get;set; }
        public Contributor[] contributors {get;set;}
        public string md5_image {get;set;}
        public Contributor artist {get;set;}
        public Album album {get;set;}
        public string type {get;set;}
    }
}