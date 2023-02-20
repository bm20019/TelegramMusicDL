namespace YTMUSICAPI;

public class MusicData
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string PictureUrl { get; set; }
    public  string Time { get; set; }
    
    public string Year { get; set; }

    public MusicData(string Id, string Title, string Artist,string Album,string time,string PictureUrl,string year="")
    {
        this.Id = Id;
        this.Title = Title;
        this.Artist = Artist;
        this.Album = Album;
        this.Time = time;
        this.Year = year;
        this.PictureUrl = PictureUrl;
    }
    
    public string GenerateUrlMusic()
    {
        return "https://music.youtube.com/watch?v=" + Id;
    }
    public string GenerateUrlYouTube()
    {
        return "https://www.youtube.com/watch?v=" + Id;
    }
}