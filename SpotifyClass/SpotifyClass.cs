using System.Net;
using SpotifyAPI.Web;


namespace SpotifyClass;

public class SpotifyClass
{
    private SpotifyClient spotifyClient;
    private FullTrack fullTrack;
    public SpotifyClass(string AccessToken, string TokenType) => InicializarSpotify(AccessToken, TokenType);
    public SpotifyClass(SpotifyClient spotifyClient) => InicializarSpotify(spotifyClient);

    private void InicializarSpotify(string AccessToken, string TokenType)
    {
        spotifyClient = new SpotifyClient(AccessToken, TokenType);
    }

    private void InicializarSpotify(SpotifyClient spotifyClient)
    {
        this.spotifyClient = spotifyClient;
    }

    public FullTrack GetFullTrack(string Url)
    {
        string url = Url;
        string id = GetIDUrlSpotify(url);
        fullTrack = GetSong(id);
        FullTrack ftrack = fullTrack;
        string _urImages = ftrack.Album.Images[0].Url;
        WebClient wc = new WebClient();
        return fullTrack;
    }

    public ModelSpotify GetFullData(string Url)
    {
        string id = GetIDUrlSpotify(Url);
        return new ModelSpotify(spotifyClient, id);
    }

    private string GetIDUrlSpotify(string url)
    {
        return url.Split('/')[4].Split('?')[0];
    }

    private FullTrack GetSong(string IdSong)
    {
        return spotifyClient.Tracks.Get(IdSong).Result;
    }
}