namespace TestCode;

public class cs
{
    static void Main(){
        Load(".enxv");
        Console.WriteLine(Environment.GetEnvironmentVariable("var2"));

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
