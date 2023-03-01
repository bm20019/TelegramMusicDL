namespace TestCode;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
public class Program
{
    public static async Task Main()
    {
        string query = "hosanna hillsong united español";
        string json = await POST(query);
        Modelo md = JsonSerializer.Deserialize<Modelo>(json);
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/TODO.jso", json);
        //Console.WriteLine(md.contents.tabbedSearchResultsRenderer.tabs[0].tabRenderer.content.sectionListRenderer.contents[0].musicShelfRenderer.contents[0].musicResponsiveListItemRenderer.thumbnail.musicThumbnailRenderer.thumbnail.thumbnails[0].url);
        Contents3[] fc = md.contents.tabbedSearchResultsRenderer.tabs[0].tabRenderer.content.sectionListRenderer.contents[1].musicShelfRenderer.contents;

        foreach (Contents3 item in fc)
        {
            foreach (FlexColumns itemC in item.musicResponsiveListItemRenderer.flexColumns)
            {
                foreach (Runs itemR in itemC.musicResponsiveListItemFlexColumnRenderer.text.runs)
                {
                    if (itemR.text != null)
                        if (itemR.text == " • ")
                            Console.Write(" || ");
                        else
                            Console.Write(itemR.text);
                }
                Console.WriteLine();
            }
        }
    }

    public static async Task<string> POST(string query)
    {
        if (string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query))
            throw new NullReferenceException("La consulta esta vacia");

        var handler = new HttpClientHandler();
        handler.AutomaticDecompression = DecompressionMethods.None;
        using (var httpClient = new HttpClient(handler))
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://music.youtube.com/youtubei/v1/search?key=AIzaSyC9XL3ZjWddXya6X74dJoCTL-WEYFDNX30&prettyPrint=false"))
            {
                request.Headers.TryAddWithoutValidation("authority", "music.youtube.com");
                request.Headers.TryAddWithoutValidation("accept", "*/*");
                request.Headers.TryAddWithoutValidation("accept-language", "es-419,es;q=0.9");
                request.Headers.TryAddWithoutValidation("cookie", "YSC=CR-HL3OhMvw; VISITOR_INFO1_LIVE=TuZtPJNtwZs; DEVICE_INFO=ChxOekl3TlRNeU1UY3pPVEk1TlRnMk5qZzVPUT09EPLj+Z8GGPLj+Z8G; _gcl_au=1.1.26706039.1677619699");
                request.Headers.TryAddWithoutValidation("origin", "https://music.youtube.com");
                request.Headers.TryAddWithoutValidation("referer", $"https://music.youtube.com/search?q={Uri.EscapeDataString(query)}");
                request.Headers.TryAddWithoutValidation("sec-ch-ua", "\"Chromium\";v=\"110\", \"Not A(Brand\";v=\"24\", \"Google Chrome\";v=\"110\"");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-arch", "\"x86\"");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-bitness", "\"64\"");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-full-version", "\"110.0.5481.177\"");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-full-version-list", "\"Chromium\";v=\"110.0.5481.177\", \"Not A(Brand\";v=\"24.0.0.0\", \"Google Chrome\";v=\"110.0.5481.177\"");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-platform", "\"Linux\"");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-platform-version", "\"6.1.0\"");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-wow64", "?0");
                request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
                request.Headers.TryAddWithoutValidation("sec-fetch-mode", "same-origin");
                request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
                request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36");
                request.Headers.TryAddWithoutValidation("x-goog-visitor-id", "CgtUdVp0UEpOdHdacyiH5fmfBg%3D%3D");
                request.Headers.TryAddWithoutValidation("x-youtube-bootstrap-logged-in", "false");
                request.Headers.TryAddWithoutValidation("x-youtube-client-name", "67");
                request.Headers.TryAddWithoutValidation("x-youtube-client-version", "1.20230222.01.00");

                request.Content = new StringContent("{\"context\":{\"client\":{\"hl\":\"es-419\",\"gl\":\"SV\",\"remoteHost\":\"190.87.160.208\",\"deviceMake\":\"\",\"deviceModel\":\"\",\"visitorData\":\"CgtUdVp0UEpOdHdacyiH5fmfBg%3D%3D\",\"userAgent\":\"Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36,gzip(gfe)\",\"clientName\":\"WEB_REMIX\",\"clientVersion\":\"1.20230222.01.00\",\"osName\":\"X11\",\"osVersion\":\"\",\"originalUrl\":\"https://music.youtube.com/search?q=" + Uri.EscapeDataString(query) + "\",\"platform\":\"DESKTOP\",\"clientFormFactor\":\"UNKNOWN_FORM_FACTOR\",\"configInfo\":{\"appInstallData\":\"CIfl-Z8GEMzfrgUQ2umuBRC4rP4SEILdrgUQ5_euBRCH3a4FEP24_RIQ5aD-EhDTrP4SEO2GrwUQuIuuBQ%3D%3D\"},\"browserName\":\"Chrome\",\"browserVersion\":\"110.0.0.0\",\"acceptHeader\":\"text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7\",\"deviceExperimentId\":\"ChxOekl3TlRNeU1UY3pPVEk1TlRnMk5qZzVPUT09EIfl-Z8GGPLj-Z8G\",\"screenWidthPoints\":1600,\"screenHeightPoints\":795,\"screenPixelDensity\":1,\"screenDensityFloat\":1,\"utcOffsetMinutes\":-360,\"userInterfaceTheme\":\"USER_INTERFACE_THEME_LIGHT\",\"timeZone\":\"America/El_Salvador\",\"musicAppInfo\":{\"pwaInstallabilityStatus\":\"PWA_INSTALLABILITY_STATUS_UNKNOWN\",\"webDisplayMode\":\"WEB_DISPLAY_MODE_BROWSER\",\"storeDigitalGoodsApiSupportStatus\":{\"playStoreDigitalGoodsApiSupportStatus\":\"DIGITAL_GOODS_API_SUPPORT_STATUS_UNSUPPORTED\"}}},\"user\":{\"lockedSafetyMode\":false},\"request\":{\"useSsl\":true,\"internalExperimentFlags\":[],\"consistencyTokenJars\":[]},\"clickTracking\":{\"clickTrackingParams\":\"IhMIq57a-JS5_QIVR8ycCh0mFA2fMghleHRlcm5hbA==\"},\"adSignalsInfo\":{\"params\":[{\"key\":\"dt\",\"value\":\"1677619847922\"},{\"key\":\"flash\",\"value\":\"0\"},{\"key\":\"frm\",\"value\":\"0\"},{\"key\":\"u_tz\",\"value\":\"-360\"},{\"key\":\"u_his\",\"value\":\"4\"},{\"key\":\"u_h\",\"value\":\"900\"},{\"key\":\"u_w\",\"value\":\"1600\"},{\"key\":\"u_ah\",\"value\":\"868\"},{\"key\":\"u_aw\",\"value\":\"1600\"},{\"key\":\"u_cd\",\"value\":\"24\"},{\"key\":\"bc\",\"value\":\"31\"},{\"key\":\"bih\",\"value\":\"795\"},{\"key\":\"biw\",\"value\":\"1600\"},{\"key\":\"brdim\",\"value\":\"0,0,0,0,1600,0,1600,868,1600,795\"},{\"key\":\"vis\",\"value\":\"1\"},{\"key\":\"wgl\",\"value\":\"true\"},{\"key\":\"ca_type\",\"value\":\"image\"}]}},\"query\":\"" + query + "\"}");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var response = await httpClient.SendAsync(request);

                return await response.Content.ReadAsStringAsync();
                //File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"/"+query+".json", res);
            }
        }
    }
}
