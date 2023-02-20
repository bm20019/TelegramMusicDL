using System.Text.Json;
using SpotifyAPI.Web;
using Swan;

namespace MusicDownloaderLibrary;

class Config
{

    static string FolderBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".SpotdlSharp/");
    static string folderTemp = Path.Combine(FolderBase, "temp");
    static string TokenFile = Path.Combine(FolderBase, ".spotdlJson");

    string tokenAccess = "-1";
    SpotifyClient spotifyClient;
    public Config()
    {
    }
    public void Inicializar()
    {
        if (Directory.Exists(FolderBase) == false)
            Directory.CreateDirectory(FolderBase);
        if (Directory.Exists(folderTemp) == false)
            Directory.CreateDirectory(folderTemp);
    }

    public void InizializarToken()
    {
        if (File.Exists(TokenFile))
        {
            try
            {
                Token? tokenSpotify = JsonSerializer.Deserialize<Token>(File.ReadAllText(TokenFile));
                if (tokenSpotify == null)
                {
                    Console.WriteLine("Token is null... Recargando Token");
                    CreateToken();
                }
                else
                {
                    DateTime creado = Convert.ToDateTime(tokenSpotify.CreatedAt);
                    DateTime UTC = GetDateTimeWordl();
                    TimeSpan diferencia =UTC- creado;
                    int tiempo = Convert.ToInt32(diferencia.TotalSeconds);
                    if (tiempo<3600){
                        Console.WriteLine("Token Todavia valido");
                        spotifyClient = new SpotifyClient(tokenSpotify.AccessToken,tokenSpotify.TokenType);
                    }else{
                        Console.WriteLine("Token Expirado... Recargando Token");
                        CreateToken();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Token Not Found... Creando Token");
                CreateToken();
            }
        }
        else
        {
            CreateToken();
        }
    }
    private void CreateToken()
    {
        string pathENV = Path.Combine(new FileInfo(Environment.GetCommandLineArgs()[0]).Directory.FullName,".env");
        Load(pathENV);
        string clienId = Environment.GetEnvironmentVariable("ClientID");
        string clientSecret = Environment.GetEnvironmentVariable("ClientSecret");
        if(string.IsNullOrEmpty(clienId) || string.IsNullOrEmpty(clientSecret))
            throw new ArgumentNullException("ClientId or ClientSecret is null");

        var config = SpotifyClientConfig.CreateDefault();
        ClientCredentialsRequest request = new ClientCredentialsRequest(clienId, clientSecret);
        ClientCredentialsTokenResponse response = new OAuthClient(config).RequestToken(request).Result;
        spotifyClient = new SpotifyClient(response.AccessToken, response.TokenType);
        File.WriteAllText(TokenFile, response.ToJson());
    }

    public DateTime GetDateTimeWordl()
    {
        string request = "https://www.timeapi.io/api/Time/current/zone?timeZone=UTC";
        var myClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
        var response = myClient.GetAsync(request);
        using var strem = new StreamReader(response.Result.Content.ReadAsStream());
        string rs = strem.ReadToEnd();

        return JsonSerializer.Deserialize<Tiempo>(rs).dateTime;
    }


    private string GetIpPublic()
    {
        return new System.Net.WebClient().DownloadString("https://api.ipify.org");
    }

    public SpotifyClient GetSpotifyClient()
    {
        return spotifyClient;
    }


    /// <summary>
    /// Obtiene el folder donde se guardan los datos de configuracion, y temporales
    /// </summary>
    /// <returns></returns>
    public string GetFolderBase()
    {
        return FolderBase;
    }

    /// <summary>
    /// Obtiene el folder donde se  guardan los archivos temporales de descarga
    /// </summary>
    /// <returns></returns>
    public string GetFolderTemp()
    {
        return folderTemp;
    }

    private static void Load(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("El archivo de variable no existe");

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split(
                '=',
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                continue;
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}

public partial class Tiempo
{
    public int year {get;set;}
    public int mont{get;set;}
    public int day{get;set;}
    public int hour{get;set;}
    public int minute{get;set;}
    public int seconds{get;set;}
    public int milliseconds{get;set;}
    public DateTime dateTime {get;set;}
    public string date {get;set;}

    public string time {get;set;}
    public string  timeZone {get;set;}
    public string dayOfWeek {get;set;}
    public bool dstActive {get;set;}

}