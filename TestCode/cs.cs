namespace TestCode;

public class Program
{
    public static async Task Main(){
        GeniusLyricsLibrary.GeniusApi Ga = new GeniusLyricsLibrary.GeniusApi();
       string lyrics = await Ga.GetLyricsforSearch("Hermoso nombre Hillsong");
       Console.WriteLine(lyrics);
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
