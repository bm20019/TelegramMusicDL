using System.Text.RegularExpressions;

namespace YTMUSICAPI;
  
    public class Microformat
    {
        public MicroformatDataRenderer microformatDataRenderer { get; set; }
    }

    public class MicroformatDataRenderer
    {
        public string urlCanonical { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string siteName { get; set; }
        public string appName { get; set; }
        public string androidPackage { get; set; }
        public string iosAppStoreId { get; set; }
        public string iosAppArguments { get; set; }
        public string ogType { get; set; }
        public string urlApplinksIos { get; set; }
        public string urlApplinksAndroid { get; set; }
        public string urlTwitterIos { get; set; }
        public string urlTwitterAndroid { get; set; }
        public string twitterCardType { get; set; }
        public string twitterSiteHandle { get; set; }
        public string schemaDotOrgType { get; set; }
        public bool? noindex { get; set; }
        public bool? unlisted { get; set; }
        public bool? paid { get; set; } 
        public bool? familySafe { get; set; }
        public List<string> tags { get; set; }
        public List<string> availableCountries { get; set; }
        public VideoDetails videoDetails { get; set; }
        public string viewCount { get; set; }
        public string publishDate { get; set; }
        public string category { get; set; }
        public string uploadDate { get; set; }
        public string year {
            get{return GetYear();}
        }
        private string GetYear()
        {
            string re = new Regex("\\d\\d\\d\\d").Match(description).Value;
            return string.IsNullOrEmpty(re) == true ? "0" : re;
        }
    }

    public class Root
    {
        public VideoDetails videoDetails { get; set; }
        public Microformat microformat { get; set; }
    }

  
    public class Thumbnail
    {
        public List<Thumbnail2> thumbnails { get; set; }
    }

    public class Thumbnail2
    {
        public string url{ get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
    }

    public class VideoDetails
    {
        public string videoId { get; set; }
        public string title { get; set; }
        public string lengthSeconds { get; set; }
        public string channelId { get; set; }
        public bool? isOwnerViewing { get; set; }
        public bool? isCrawlable { get; set; }
        public Thumbnail thumbnail { get; set; }
        public bool? allowRatings { get; set; }
        public string viewCount { get; set; }
        public string author { get; set; }
        public bool? isPrivate { get; set; }
        public bool? isUnpluggedCorpus { get; set; }
        public string musicVideoType { get; set; }
        public bool? isLiveContent { get; set; }
        public string externalVideoId { get; set; }
        public string durationSeconds { get; set; }
        public string durationIso8601 { get; set; }
    }