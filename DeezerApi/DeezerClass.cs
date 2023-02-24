using System.Text.Json;
using System.Text.RegularExpressions;

namespace DeezerApi
{
    public class DeezerClass
    {
        private const string deezer1 = "https://deezer.page.link";
        private const string deezer2 = "https://www.deezer.com";
        public Deezer? GetDeezer(string url){
            string urlApi =  GetUrlApi(url).Result;
            string jsonStr = RepsonseStr(urlApi);
            return Serializer(jsonStr);
        }
        private async Task<string> GetUrlApi(string url)
        {
            string url_Base = "https://api.deezer.com/track/";
            string body = await CallUrl(url);
            string id = System.Text.RegularExpressions.Regex.Matches(body,"track/[0-9]+")[0].Value.Remove(0,6);
            return url_Base+id;//+extr;
        }
        private async Task<string> CallUrl(string fullUrl)
        {
            string UserAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            var response = await client.GetStringAsync(fullUrl);
            return response;
        }

        private static string RepsonseStr(string urlApi)
        {
            var myClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            var response = myClient.GetAsync(urlApi);
            using var strem = new StreamReader(response.Result.Content.ReadAsStream());
            var rs = strem.ReadToEnd();
            return rs;
        }

        private static Deezer? Serializer(string dataJson)
        {
            JsonSerializerOptions jsSo = new JsonSerializerOptions();
            jsSo.PropertyNameCaseInsensitive = true;
            jsSo.IncludeFields = false;
            Deezer? t = JsonSerializer.Deserialize<Deezer>(dataJson, jsSo);
            return t;
        }
    }
}