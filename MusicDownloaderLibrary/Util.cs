using System.Text.RegularExpressions;

namespace MusicDownloaderLibrary;
public class Utils
{
    public Utils(){
        
    }
    protected const string spotify1 = "https://open.spotify.com";
    protected const string deezer1 = "https://deezer.page.link";
    protected const string deezer2 = "https://www.deezer.com";
    protected const string youtube1 = "https://www.youtube.com/watch?v=";
    protected const string youtube2 = "https://youtu.be/";
    protected const string ytMusic1 = "https://music.youtube.com/watch?v=";

    public static Servidor UrlServerIs(string urlMusic)
    {
        if (urlMusic.Contains(spotify1))
        {
            if (Isvalid(urlMusic, Servidor.Spotify))
                return Servidor.Spotify;
        }
        else if (urlMusic.Contains(deezer1))
        {
            if (Isvalid(urlMusic, Servidor.Deezer))
                return Servidor.Deezer;
        }
        else if (urlMusic.Contains(deezer2))
        {
            if (Isvalid(urlMusic, Servidor.Deezer))
                return Servidor.Deezer;
        }
        else if (urlMusic.Contains(youtube1))
        {
            if (Isvalid(urlMusic, Servidor.YouTubeVideo))
                return Servidor.YouTubeVideo;
        }
        else if (urlMusic.Contains(youtube2))
        {
            if (Isvalid(urlMusic, Servidor.YouTubeVideo))
                return Servidor.YouTubeVideo;
        }
        else if (urlMusic.Contains(ytMusic1))
        {
            if (Isvalid(urlMusic, Servidor.youtubeMusic))
                return Servidor.youtubeMusic;
        }

        throw new Exception("Error url no pertenece a un servicio de streaming");

    }

    private static bool Isvalid(String input, Servidor server)
    {
        if (server == Servidor.Spotify)
        {
            Regex regex = new Regex("https://open\\.spotify\\.com/[a-zA-Z]+/[A-Za-z0-9]+", RegexOptions.IgnoreCase);
            //UrlSpotify = input;
            return regex.IsMatch(input);
        }
        else if (server == Servidor.Deezer)
        {
            if (input.Contains("deezer.page.link"))
            {
                Regex regex = new Regex("https://deezer\\.page\\.link/[A-Za-z0-9]+", RegexOptions.IgnoreCase);
                // UrlSpotify = input;
                return regex.IsMatch(input);
            }
            else if (input.Contains("deezer.com/track") || 
                     new Regex("https://www\\.deezer\\.com/[A-Za-z]+/track/113145662", RegexOptions.IgnoreCase).IsMatch(input))
            {
                Regex regex = new Regex("https://www\\.deezer\\.com/[a-zA-Z]+/[a-zA-Z]+/[0-9]+", RegexOptions.IgnoreCase);
                // UrlSpotify = input;
                return regex.IsMatch(input);
            }
            //return false;
        }
        else if (server == Servidor.YouTubeVideo)
        {
            if (input.Contains("www.youtube.com"))
            {
                Regex regex = new Regex("https://www\\.youtube\\.com/watch\\?v=[A-Za-z0-9]+", RegexOptions.IgnoreCase);
                return regex.IsMatch(input);
            }
            else if (input.Contains("youtu.be"))
            {
                Regex regex = new Regex("https://youtu\\.be/[A-Za-z0-9]+", RegexOptions.IgnoreCase);
                return regex.IsMatch(input);
            }
        }
        else if (server == Servidor.youtubeMusic)
        {
            if (input.Contains("music.youtube.com"))
            {
                Regex regex = new Regex("https://music\\.youtube\\.com/watch\\?v=", RegexOptions.IgnoreCase);
                return regex.IsMatch(input);
            }
        }
        return false;
    }
}