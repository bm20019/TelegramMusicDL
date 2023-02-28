namespace TestCode;
using System.Text.RegularExpressions;
public class Program
{
    public static async Task Main(){
        //GeniusLyricsLibrary.GeniusApi Ga = new GeniusLyricsLibrary.GeniusApi();
       //string lyrics = await Ga.GetLyricsforSearch("Hermoso nombre Hillsong");
       //Console.WriteLine(lyrics);

      MusicDownloaderLibrary.Download dl = new MusicDownloaderLibrary.Download();
      string url = "https://open.spotify.com/track/1oYafejykcv8ZYRHlF1oHF?si=4d6391a5e27245ce";
      ConvertAudioLibrary.Metadata mt = dl.GetDataInfo(url);
      Console.WriteLine($"Title: {mt.Title}\nAlbum: {mt.Album}\nYear:{mt.Year}");
      await dl.MusicDownload(url,new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)),pattern:"{artist} {title}");
    }
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("El archivo no existe de variables no existe");

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
