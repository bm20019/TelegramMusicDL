using System.Net;
using HtmlAgilityPack;

namespace MusixmathSearch;

public class Musixmath
{
    public Musixmath()
    {

    }
    public string SearchLyrics(string Query)
    {
        string url_Base = "https://www.musixmatch.com/es/search/";
        string pattern = "class=\"title\"";
        string extr = GetData(url_Base, Query, pattern, 80).Result;
        string hrefMusixmatch = extr.Split('\"')[1];
        string urlfinal = "https://www.musixmatch.com" + hrefMusixmatch + "/amp";
        Console.WriteLine("Lyrics Url: " + urlfinal);
        return GetLyrics(urlfinal);
    }

    private string GetLyrics(string url)
    {
        string uri = url;
        Console.WriteLine("URLAMP: " + url);
        string html = "";
        using WebClient client = new WebClient();
        try
        {
            html = client.DownloadString(url);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(url + "\n" + ex.Message);
            //throw;
            //html = client.DownloadString(url);
        }

        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        HtmlNodeCollection htmlBody = htmlDoc.DocumentNode.SelectNodes("//body");
        string lyrics = htmlDoc.GetElementbyId("lyrics").InnerText;
        return lyrics;
    }

    private async Task<string> GetLyricsAsync(Uri url)
    {
        //string uri = url;
        Console.WriteLine("URLAMP: " + url.AbsoluteUri);
        string html = "";
        using WebClient client = new WebClient();
        try
        {
            //client.DownloadStringAsync();
            html =  client.DownloadString(url);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(url + "\n" + ex.Message);
            //throw;
            //html = client.DownloadString(url);
        }

        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        HtmlNodeCollection htmlBody = htmlDoc.DocumentNode.SelectNodes("//body");
        string lyrics = htmlDoc.GetElementbyId("lyrics").InnerText;
        return lyrics;
    }

    /// <summary>
    /// Obtiene un ID del un html
    /// </summary>
    /// <param name="urlBase">Url base</param>
    /// <param name="Query">Consulta que se buscara</param>
    /// <param name="pattern">caracteres a buscar</param>
    /// <param name="lengthSubstring">longitud de la cadena que se obtendra</param>
    /// <returns></returns>
    protected async Task<string> GetData(string urlBase, string Query, string pattern, int lengthSubstring)
    {
        string url_Base = urlBase;
        string EncodeUrl = url_Base + Uri.EscapeDataString(Query);
        string body = await CallUrl(EncodeUrl);
        int index = body.IndexOf(pattern);
        string extr = body.Substring(index + pattern.Length, lengthSubstring);
        return extr;
    }

    /// <summary>
    /// Obtiene el codigo fuente de la url dada
    /// </summary>
    /// <param name="fullUrl">Url a cargar</param>
    /// <returns></returns>
    protected static async Task<string> CallUrl(string fullUrl)
    {
        string UserAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
        var response = await client.GetStringAsync(fullUrl);
        return response;
    }
}