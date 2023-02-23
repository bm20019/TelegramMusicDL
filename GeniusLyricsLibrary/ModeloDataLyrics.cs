namespace GeniusLyricsLibrary.ModelLyrics;
    public class Album
    {
        public Artist artist { get; set; }
        public string url { get; set; }
        public string nameWithArtist { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string fullTitle { get; set; }
        public string coverArtUrl { get; set; }
        public string coverArtThumbnailUrl { get; set; }
        public string apiPath { get; set; }
        public string type { get; set; }
    }
    public class Annotatable
    {
        public string url { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string linkTitle { get; set; }
        public string imageUrl { get; set; }
        public int id { get; set; }
        public string context { get; set; }
        public ClientTimestamps clientTimestamps { get; set; }
        public string apiPath { get; set; }
    }

    public class Artist
    {
        public string url { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public bool isVerified { get; set; }
        public bool isMemeVerified { get; set; }
        public string indexCharacter { get; set; }
        public string imageUrl { get; set; }
        public int id { get; set; }
        public string headerImageUrl { get; set; }
        public string apiPath { get; set; }
        public string type { get; set; }
    }

    public class Author
    {
        public int user { get; set; }
        public object pinnedRole { get; set; }
        public int attribution { get; set; }
        public string type { get; set; }
    }

    public class Avatar
    {
        public Medium medium { get; set; }
    }

    public class Body
    {
        public string html { get; set; }
        public List<object> children { get; set; }
        public string tag { get; set; }
        public string markdown { get; set; }
    }

    public class BoundingBox
    {
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Chartbeat
    {
        public string title { get; set; }
        public string sections { get; set; }
        public string authors { get; set; }
    }

    public class ClientTimestamps
    {
        public int updatedByHumanAt { get; set; }
        public int lyricsUpdatedAt { get; set; }
    }

    public class CurrentUserMetadata
    {
        public Interactions interactions { get; set; }
        public List<string> excludedPermissions { get; set; }
        public List<object> permissions { get; set; }
        public IqByAction iqByAction { get; set; }
    }

    public class Description
    {
        public string markdown { get; set; }
        public string html { get; set; }
    }

    public class DfpKv
    {
        public List<string> values { get; set; }
        public string name { get; set; }
    }

    public class DmpDataLayer
    {
        public Page page { get; set; }
    }
    public class HotSongsPreview
    {
        public string url { get; set; }
        public string title { get; set; }
        public int id { get; set; }
    }

    public class Interactions
    {
        public bool following { get; set; }
        public bool pyong { get; set; }
        public object vote { get; set; }
        public bool cosign { get; set; }
    }

    public class IqByAction
    {
    }

    public class LyricsData
    {
        public List<object> referents { get; set; }
        public Body body { get; set; }
        public object lyricsPlaceholderReason { get; set; }
        public ClientTimestamps clientTimestamps { get; set; }
    }

    public class Medium
    {
        public BoundingBox boundingBox { get; set; }
        public string url { get; set; }
    }

    public class Page
    {
        public string type { get; set; }
    }

    public class PrimaryTag
    {
        public string url { get; set; }
        public bool primary { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string type { get; set; }
    }

    public class Root
    {
        public string currentPage { get; set; }
        public string deviceType { get; set; }
        public SongPage songPage { get; set; }

        public string GetLyrics(){
           return songPage.lyricsData.body.html.Replace("<p>","").Replace("</p>","").Replace("<br>","").Replace("\\"+"n","\n");
        }
    }

    public class Song
    {
        public string url { get; set; }
        public string title { get; set; }
        public string path { get; set; }
        public string lyricsState { get; set; }
        public string language { get; set; }
        public int id { get; set; }
        public string apiPath { get; set; }
        public string type { get; set; }
    }

    public class SongPage
    {
        public bool isPrimisAtf { get; set; }
        public object s3CacheExperiment { get; set; }
        public int song { get; set; }
        public List<object> pinnedQuestions { get; set; }
        public LyricsData lyricsData { get; set; }
        public List<HotSongsPreview> hotSongsPreview { get; set; }
        public object featuredQuestion { get; set; }
        public bool showFeaturedQuestion { get; set; }
        public int pendingQuestionCount { get; set; }
        public List<DfpKv> dfpKv { get; set; }
        public string title { get; set; }
        public string path { get; set; }
        public string pageType { get; set; }
        public List<string> initialAdUnits { get; set; }
        public string hotSongsLink { get; set; }
        public List<object> headerBidPlacements { get; set; }
        public DmpDataLayer dmpDataLayer { get; set; }
        public string controllerAndAction { get; set; }
        public Chartbeat chartbeat { get; set; }
    }
