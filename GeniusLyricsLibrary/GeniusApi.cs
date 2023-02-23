namespace GeniusLyricsLibrary;
using System.Net;
using GeniusLyricsLibrary.ModelSearch;
using System.Text.Json;
using HtmlAgilityPack;
using GeniusLyricsLibrary.ModelLyrics;
public class GeniusApi
{

    public async Task<string> GetLyricsforSearch(string search){
        GeniusModel GM = await GetDataSearch(search);
        string json = GetJsonLyrics(GM.response.sections[0].hits[0].result.url);
        return JsonSerializer.Deserialize<Root>(json).GetLyrics();
    }
    public async Task<GeniusModel> GetDataSearch(string search){
        string JsonSTR = await SearchInGenius(search);
        return JsonSerializer.Deserialize<GeniusModel>(JsonSTR);
    }
    public Root GetLyricsforUrl(string url){
        string json = GetJsonLyrics(url);
        return JsonSerializer.Deserialize<Root>(json);
    }

    private async Task<string> SearchInGenius(string search)
    {
        string dataString = Uri.EscapeDataString(search);
        var handler = new HttpClientHandler();
        handler.AutomaticDecompression = DecompressionMethods.All;
        using (var httpClient = new HttpClient(handler))
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://genius.com/api/search/multi?per_page=5&q={dataString}"))
            {
                request.Headers.TryAddWithoutValidation("authority", "genius.com");
                request.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("accept-language", "es-419,es;q=0.9");
                request.Headers.TryAddWithoutValidation("cookie", "_genius_ab_test_cohort=12; _genius_ab_test_desktop_song_primis=inread; genius_first_impression=1677174307839; _csrf_token=s6J4rcGqHztxRf4zWOaJncb7E5dyEwf0drxMzGvCWY0%3D; _rapgenius_session=BAh7BzoPc2Vzc2lvbl9pZEkiJTk5ZmMzNGE0ZGNlYzVjN2Q3MmQ0YTExMzgxMDJkN2IzBjoGRUY6EF9jc3JmX3Rva2VuSSIxczZKNHJjR3FIenR4UmY0eldPYUpuY2I3RTVkeUV3ZjBkcnhNekd2Q1dZMD0GOwZG--25cff2d4850a38b18b535614adf05039d8a48b5f; _ab_tests_identifier=09d2b410-af4f-46dd-9d2c-7f3a9ea24c3a; AMP_TOKEN=%24NOT_FOUND; _ga=GA1.2.1873699525.1677174321; _gid=GA1.2.109244833.1677174321; _fbp=fb.1.1677174322108.2043266261; __qca=P0-1421198418-1677174314707; GLAM-AID=5edbdc78989a484f907c769940b7e8b0; GLAM-SID=c391a180bb8043f79fadb0c7d3ca507f; __gads=ID=93cf5fe4eebd6709:T=1677174333:S=ALNI_MZ-9eQHf9cLqPof8HnyWMYIQjd0cg; __gpi=UID=000009ed1f2719bd:T=1677174333:RT=1677174333:S=ALNI_MY6OHdZRaIiIGDJB9Yr-naALVcR-w; mp_mixpanel__c=0; _cb=B-wjbgCeT2WHLBQTM; _cb_svref=https%3A%2F%2Fgenius.com%2F; bounceClientVisit5453v=N4IgNgDiBcIBYBcEQM4FIDMBBNAmAYnvgOYCmAdgJYCuKAdAMYD2AtkSqQIYBODcm+AI6YAIgEkw1FpXKc8AVlwAGALI9mC5QCEe3ShQRMUIADQhuMEGSq1GrUyEooA+sSbOOKFJSbkYAM04wDjMnVwgPUi8fP2hA4NIAXyA; _pbjs_userid_consent_data=3524755945110770; _li_dcdm_c=.genius.com; _lc2_fpi=78587c35eff6--01gszpbk2tj2tvzj0b5whb46qd; _pubcid=005c9278-aed4-42e3-84a7-124800dcd066; _lr_retry_request=true; _lr_env_src_ats=false; pbjs-unifiedid=%7B%22TDID%22%3A%222954266b-f3e0-425b-a17e-bb3fb6230ab4%22%2C%22TDID_LOOKUP%22%3A%22TRUE%22%2C%22TDID_CREATED_AT%22%3A%222023-01-23T17%3A50%3A25%22%7D; __li_idex_cache=%7B%7D; pbjs_li_nonid=%7B%7D; panoramaId_expiry=1677261025999; _cc_id=c8ba095d6bbc16bf3c652a7dff55c0e9; panoramaId=0210005afe800a9d0d6184168b55a9fb927ab85f35d18a7eb41e1eecbdc1e1d5; __j_state=%7B%22landing_url%22%3A%22https%3A%2F%2Fgenius.com%2F%22%2C%22pageViews%22%3A9%2C%22prevPvid%22%3A%22be04a490c3d04eac81c0a7cdee90af70%22%2C%22extreferer%22%3A%22https%3A%2F%2Fwww.google.com%2F%22%2C%22user_worth%22%3A0%7D; mp_77967c52dc38186cc1aadebdd19e2a82_mixpanel=%7B%22%24search_engine%22%3A%20%22google%22%2C%22%24initial_referrer%22%3A%20%22https%3A%2F%2Fwww.google.com%2F%22%2C%22%24initial_referring_domain%22%3A%20%22www.google.com%22%2C%22AMP%22%3A%20false%2C%22genius_platform%22%3A%20%22web%22%2C%22%24device_id%22%3A%20%221867f613d2523e-03e27863f5cad9-17462c6c-15f900-1867f613d262db%22%2C%22Logged%20In%22%3A%20false%2C%22Is%20Editor%22%3A%20null%2C%22Is%20Moderator%22%3A%20null%2C%22Mobile%20Site%22%3A%20false%2C%22%24user_id%22%3A%20%221873699525.1677174321%22%2C%22Tag%22%3A%20%22pop%22%7D; _chartbeat2=.1677174619018.1677174897084.1.DusV5GzVdlkDICvcxD96XwgCRms0p.4; _chartbeat4=t=BLItDpBfmMpSCiMHMMBF0eLED7XzEU&E=10&x=0&c=12.56&y=1434&w=795");
                request.Headers.TryAddWithoutValidation("if-none-match", "W/\"b3aaa852ffa4a03a4169b34292a56a11\"");
                request.Headers.TryAddWithoutValidation("referer", $"https://genius.com/search?q={dataString}");
                request.Headers.TryAddWithoutValidation("sec-ch-ua", "\"Chromium\";v=\"110\", \"Not A(Brand\";v=\"24\", \"Google Chrome\";v=\"110\"");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-platform", "\"Linux\"");
                request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
                request.Headers.TryAddWithoutValidation("sec-fetch-mode", "cors");
                request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
                request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36");
                request.Headers.TryAddWithoutValidation("x-requested-with", "XMLHttpRequest");

                var response = await httpClient.SendAsync(request);
                string str = await response.Content.ReadAsStringAsync();
                return str;
            }
        }
    }

    private string GetJsonLyrics(string url){
        string html ="";
        using WebClient wc = new WebClient();
        try
        {
            html = wc.DownloadString(url);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(url + "\n" + ex.Message);
            //throw;
            //html = client.DownloadString(url);
        }

        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        HtmlNode htmlBody = htmlDoc.DocumentNode.SelectNodes("//body")[0];
        string patIn = "JSON.parse('{", patOut = "}}}}');";
        int indexIn = htmlBody.InnerHtml.IndexOf(patIn)+patIn.Length-1;
        int indexOut = htmlBody.InnerHtml.LastIndexOf(patOut);
        Console.WriteLine(indexOut);
        //string replace = "\"";
        string data = htmlBody.InnerHtml.Substring(indexIn,indexOut-indexIn+patOut.Length-3).Replace("\\"+"\"","\"").Replace("\\"+"\'","\'");
        //Console.WriteLine(data);
        //File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"/ds.json",data);
        return data;
    }
}

