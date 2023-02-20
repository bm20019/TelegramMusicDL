using System.Net;
using SpotifyAPI.Web;

namespace SpotifyClass;

public class SpotifyClass
{
    private SpotifyClient spotifyClient;
    private FullTrack fullTrack;
    public SpotifyClass() => InicializarSpotify();
    public SpotifyClass(string AccessToken,string TokenType) => InicializarSpotify(AccessToken,TokenType);
    public SpotifyClass(SpotifyClient spotifyClient) => InicializarSpotify(spotifyClient);

    private void InicializarSpotify()
    {
        var config = SpotifyClientConfig.CreateDefault();
        ClientCredentialsRequest request = new ClientCredentialsRequest("5f573c9620494bae87890c0f08a60293", "212476d9b0f3472eaa762d90b19b0ba8");
        ClientCredentialsTokenResponse response = new OAuthClient(config).RequestToken(request).Result;
        spotifyClient = new SpotifyClient(response.AccessToken, response.TokenType);
    }

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

    public ModelSpotify GetFullData(string Url){
        string id  = GetIDUrlSpotify(Url);
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