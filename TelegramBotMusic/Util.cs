using System.Text.RegularExpressions;

namespace TelegramDownloadMusic;
public class Utils
{
    public Utils()
    {

    }
    const string spotify1 = "https://open.spotify.com";
    const string deezer1 = "https://deezer.page.link";
    const string deezer2 = "https://www.deezer.com";
    const string youtube1 = "https://www.youtube.com/watch?v=";
    const string youtube2 = "https://youtu.be/";
    const string ytMusic1 = "https://music.youtube.com/watch?v=";
    public static bool UrlServerIs(string urlMusic)
    {
        if (urlMusic.Contains(spotify1))
        {
            if (Isvalid(urlMusic, Servidor.Spotify))
                return true;
        }
        else if (urlMusic.Contains(deezer1))
        {
            if (Isvalid(urlMusic, Servidor.Deezer))
                return true;
        }
        else if (urlMusic.Contains(deezer2))
        {
            if (Isvalid(urlMusic, Servidor.Deezer))
                return true;
        }
        else if (urlMusic.Contains(youtube1))
        {
            if (Isvalid(urlMusic, Servidor.YouTubeVideo))
                return true;
        }
        else if (urlMusic.Contains(youtube2))
        {
            if (Isvalid(urlMusic, Servidor.YouTubeVideo))
                return true;
        }
        else if (urlMusic.Contains(ytMusic1))
        {
            if (Isvalid(urlMusic, Servidor.youtubeMusic))
                return true;
        }
        return false;
    }

    private static bool Isvalid(String input, Servidor server)
    {
        if (server == Servidor.Spotify)
        {
            Regex regex = new Regex("https://open\\.spotify\\.com/[a-zA-Z]+/[A-Za-z0-9]+", RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }
        else if (server == Servidor.Deezer)
        {
            if (input.Contains("deezer.page.link"))
            {
                Regex regex = new Regex("https://deezer\\.page\\.link/[A-Za-z0-9]+", RegexOptions.IgnoreCase);
                return regex.IsMatch(input);
            }
            else if (input.Contains("deezer.com/"))
            {
                Regex regex1 = new Regex("https://www\\.deezer\\.com/[a-zA-Z]+/[a-zA-Z]+/[0-9]+", RegexOptions.IgnoreCase);
                Regex regex2 = new Regex("https://www\\.deezer\\.com/[A-Za-z]+/track/[A-Za-z0-9]+", RegexOptions.IgnoreCase);
                if (regex1.IsMatch(input))
                    return true;
                else if (regex2.IsMatch(input))
                    return true;
            }
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