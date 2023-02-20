namespace TelegramDownloadMusic
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            TelegramBot telegramBot = new TelegramBot();
            await telegramBot.ServiceTelegramDowload("5341641558:AAEZJoLGHzaJ5V8AVbpNOvKnmO8ozNLQsGA");
        }
    }
}