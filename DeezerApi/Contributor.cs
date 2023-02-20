namespace DeezerApi
{
    public class Contributor
    {
        public int id {get;set;}
        public string name {get;set;}
        public Uri link {get;set;}
        public Uri share {get;set;}
        public Uri picture{get;set;}
        public Uri picture_small {get;set;}
        public Uri picture_medium {get;set;}
        public Uri picture_big {get;set;}
        public Uri picture_xl {get;set;}
        public bool radio {get;set;}
        public Uri tracklist {get;set;}
        public string type { get;set;}
        public string role {get;set;}
    }
}