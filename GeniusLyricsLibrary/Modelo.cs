namespace GeniusLyricsLibrary.ModelSearch;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Artist
    {
        public string _type { get; set; }
        public string api_path { get; set; }
        public string header_image_url { get; set; }
        public int id { get; set; }
        public string image_url { get; set; }
        public string index_character { get; set; }
        public bool is_meme_verified { get; set; }
        public bool is_verified { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
    }

    public class FeaturedArtist
    {
        public string _type { get; set; }
        public string api_path { get; set; }
        public string header_image_url { get; set; }
        public int id { get; set; }
        public string image_url { get; set; }
        public string index_character { get; set; }
        public bool is_meme_verified { get; set; }
        public bool is_verified { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
    }

    public class Highlight
    {
        public string property { get; set; }
        public string value { get; set; }
        public bool snippet { get; set; }
        public List<Range> ranges { get; set; }
    }

    public class Hit
    {
        public List<Highlight> highlights { get; set; }
        public string index { get; set; }
        public string type { get; set; }
        public Result result { get; set; }
    }

    public class Meta
    {
        public int status { get; set; }
    }

    public class PrimaryArtist
    {
        public string _type { get; set; }
        public string api_path { get; set; }
        public string header_image_url { get; set; }
        public int id { get; set; }
        public string image_url { get; set; }
        public string index_character { get; set; }
        public bool is_meme_verified { get; set; }
        public bool is_verified { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public int iq { get; set; }
    }

    public class Range
    {
        public int start { get; set; }
        public int end { get; set; }
    }

    public class ReleaseDateComponents
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

    public class Response
    {
        public List<Section> sections { get; set; }
    }

    public class Result
    {
        public string _type { get; set; }
        public int annotation_count { get; set; }
        public string api_path { get; set; }
        public string artist_names { get; set; }
        public string full_title { get; set; }
        public string header_image_thumbnail_url { get; set; }
        public string header_image_url { get; set; }
        public int id { get; set; }
        public bool instrumental { get; set; }
        public string language { get; set; }
        public int lyrics_owner_id { get; set; }
        public string lyrics_state { get; set; }
        public int lyrics_updated_at { get; set; }
        public string path { get; set; }
        public object pyongs_count { get; set; }
        public string relationships_index_url { get; set; }
        public ReleaseDateComponents release_date_components { get; set; }
        public string release_date_for_display { get; set; }
        public string release_date_with_abbreviated_month_for_display { get; set; }
        public string song_art_image_thumbnail_url { get; set; }
        public string song_art_image_url { get; set; }
        public Stats stats { get; set; }
        public string title { get; set; }
        public string title_with_featured { get; set; }
        public int updated_by_human_at { get; set; }
        public string url { get; set; }
        public List<FeaturedArtist> featured_artists { get; set; }
        public PrimaryArtist primary_artist { get; set; }
        public string cover_art_thumbnail_url { get; set; }
        public string cover_art_url { get; set; }
        public string name { get; set; }
        public string name_with_artist { get; set; }
        public Artist artist { get; set; }
    }

    public class GeniusModel
    {
        public Meta meta { get; set; }
        public Response response { get; set; }
    }

    public class Section
    {
        public string type { get; set; }
        public List<Hit> hits { get; set; }
    }

    public class Stats
    {
        public int unreviewed_annotations { get; set; }
        public bool hot { get; set; }
    }
