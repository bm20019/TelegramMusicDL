using SpotifyAPI.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SpotifyClass;

public class ModelSpotify
{
    private FullAlbum fullAlbum;
    private FullArtist fullArtist;
    private FullTrack fullTrack;
    
    public ModelSpotify(SpotifyClient spotifyClient, string idSong)
    {
        this.fullTrack = spotifyClient.Tracks.Get(idSong).Result;
        this.fullAlbum = spotifyClient.Albums.Get(fullTrack.Album.Id).Result;
        this.fullArtist = spotifyClient.Artists.Get(fullTrack.Artists[0].Id).Result;
    }
    public string GetTitle()
    {
        return fullTrack.Name;
    }
    public string GetAlbumName()
    {
        return fullAlbum.Name;
    }
    public string[] GetAlbumArtists()
    {
        List<string> arts = new List<string>();
        foreach (SimpleArtist item in fullAlbum.Artists)
        {
            arts.Add(item.Name);
        }
        return arts.ToArray();
    }
    public string[] GetTrackArtist()
    {
        List<string> arts = new List<string>();
        foreach (SimpleArtist item in fullTrack.Artists)
        {
            arts.Add(item.Name);
        }

        return arts.ToArray();
    }

    public string GetISRC_Track()
    {
        return fullTrack.ExternalIds["isrc"];
    }

    public string[] GetGenresTrack()
    {
        return fullAlbum.Genres.ToArray();
    }

    public string GetDate()
    {
        return fullTrack.Album.ReleaseDate;
    }

    public string GetIdSong()
    {
        return fullTrack.Id;
    }

    public uint GetDiscNumber()
    {
        return ((uint)fullTrack.DiscNumber);
    }
    public uint GetDuration()
    {
        return (uint)fullTrack.DurationMs / 1000;
    }

    public uint GetYear()
    {
        string input = fullTrack.Album.ReleaseDate;
        if (input.Contains('-'))
        {
            string[] datas = input.Split('-');
            List<int> datInt = new List<int>();

            foreach (string item in datas)
            {
                datInt.Add(Convert.ToInt32(item));
            }
            int max = datInt.Max();
            return Convert.ToUInt32(max);
        }
        else if (input.Contains('/'))
        {
            string[] datas = input.Split('/');
            List<int> datInt = new List<int>();

            foreach (string item in datas)
            {
                datInt.Add(Convert.ToInt32(item));
            }
            int max = datInt.Max();
            return Convert.ToUInt32(max);
        }
        else if (new Regex("[0-9]+", RegexOptions.IgnoreCase).IsMatch(input))
        {
            return Convert.ToUInt32(input);
        }
        else
        {
            return 0;
        }
    }

    public uint GetTrackNumber()
    {
        return (uint)fullTrack.TrackNumber;
    }

    public uint GetTrackCount()
    {
        return (uint)fullAlbum.TotalTracks;
    }

    public bool IsExplicit()
    {
        return fullTrack.Explicit;
    }

    public string[] GetCopyrights()
    {

        List<string> copy = new List<string>();
        foreach (Copyright item in fullAlbum.Copyrights)
        {
            copy.Add(item.Text);
        }
        return copy.ToArray();
    }

    public string GetPublisher()
    {
        return $"{GetAlbumArtists()[0]}, {GetYear()}";
    }
    public Uri GetPreviewUrl()
    {
        return new Uri(fullTrack.PreviewUrl);
    }

    public Uri GetCovertArtMax()
    {
        int maxWidth = fullAlbum.Images.Max(e => e.Width);
        return new Uri(fullAlbum.Images.First(e => e.Width == maxWidth).Url);
    }

    public Uri GetCovertArtMin()
    {
        int minWidth = fullAlbum.Images.Min(e => e.Width);
        return new Uri(fullAlbum.Images.First(e => e.Width == minWidth).Url);
    }

    public Uri GetCovertArtMedium()
    {
        int maxWidth = fullAlbum.Images.Max(e => e.Width);
        int minWidth = fullAlbum.Images.Min(e => e.Width);
        return new Uri(fullAlbum.Images.First(e => e.Width > minWidth && e.Width < maxWidth).Url);
    }

    public DateTime GetDateTimeUTC()
    {
        string request = "https://www.timeapi.io/api/Time/current/zone?timeZone=UTC";
        var myClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
        var response = myClient.GetAsync(request);
        using var strem = new StreamReader(response.Result.Content.ReadAsStream());
        string rs = strem.ReadToEnd();
        return JsonSerializer.Deserialize<Tiempo>(rs).dateTime;
    }
}