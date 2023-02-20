namespace MusicDownloaderLibrary;

public class Token{
    public string AccessToken { get; set; }
    public string TokenType {get; set; }
    public  int ExpiresIn { get; set; }
    public string CreatedAt { get; set; }
    public bool IsExpired { get; set; }
}