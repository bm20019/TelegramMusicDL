namespace TelegramDownloadMusic
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string TokenTelegram;
            string pathEnv = Path.Combine( new FileInfo(Environment.GetCommandLineArgs()[0]).Directory.FullName,".env");
            Load(pathEnv);
            TokenTelegram= Environment.GetEnvironmentVariable("TokenTelegram");
            if(string.IsNullOrEmpty(TokenTelegram)
            )
            throw new ArgumentNullException("Token invalidos");

            TelegramBot telegramBot = new TelegramBot();
            await telegramBot.ServiceTelegramDowload(TokenTelegram);
        }

        private static void Load(string filePath)
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

}