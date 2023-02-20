namespace DeezerApi
{
    public class Album
    {
        public int id {get;set;}
        public string title {get;set;}
        public Uri link {get;set;}
        public Uri cover{get;set;}
        public Uri cover_small {get;set;}
        public Uri cover_medium {get;set;}
        public Uri cover_big {get;set;}
        public Uri cover_xl {get;set;}
        public string md5_image {get;set;}
        public DateTime release_date {get;set;}
        public Uri tracklist {get;set;}
        public string type { get;set;}
    }
}