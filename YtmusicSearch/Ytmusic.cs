using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace YTMUSICAPI
{
    public class Ytmusic : YtmusicParse
    {
        private string JsonApi { get; set; }
        public string GetIdYtMusic(string urlQuery, int OriginalTimeSS = 0)
        {
            string inicio = "({path: '\\/search', params: JSON.parse('";
            string final = "'});ytcfg.set";
            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36");
            string htmlString = wc.DownloadString(urlQuery);//"https://music.youtube.com/search?q=rey+christine+d%27clario");

            int indexInicio = htmlString.IndexOf(inicio) + inicio.Length;
            int indexFinal = htmlString.LastIndexOf(final) + final.Length;

            string data = htmlString.Remove(indexFinal).Substring(indexInicio);
            string unescape = Regex.Unescape(data);
            JsonApi = JsonParse(unescape);
            List<string> musicaId = new List<string>();
            List<string> musicaTime = new List<string>();

            MatchCollection MC = Regex.Matches(JsonApi, "\"videoId\":\"(?:[^\"]|\"\")*\",");
            foreach (Match item in MC)
            {
                string id = item.Value.Replace("videoId", "").Replace("\"", "").Replace(",", "").Replace(":", "");
                if (!musicaId.Contains(id))
                    musicaId.Add(id);
            }

            MatchCollection MCTiempo = Regex.Matches(JsonApi, "\"text\":\"[0-9]+:\\d\\d\"");
            foreach (Match item in MCTiempo)
            {
                string time = item.Value.Remove(0, 7).Replace("\"", "");
                musicaTime.Add(time);
            }
            SetIdsAndTimes(musicaId, musicaTime);
            return new YtmusicParse(musicaId, musicaTime).GetIdforTime(OriginalTimeSS);
        }
        
        public MusicData[] GetData(string urlQuery)
        {
            string inicio = "({path: '\\/search', params: JSON.parse('";
            string final = "'});ytcfg.set";
            WebClient wc = new WebClient();
            wc.Headers.Add("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36");
            string htmlString = wc.DownloadString(urlQuery);//"https://music.youtube.com/search?q=rey+christine+d%27clario");

            int indexInicio = htmlString.IndexOf(inicio) + inicio.Length;
            int indexFinal = htmlString.LastIndexOf(final) + final.Length;

            string data = htmlString.Remove(indexFinal).Substring(indexInicio);
            string unescape = System.Text.RegularExpressions.Regex.Unescape(data);
            JsonApi = JsonParse(unescape);
            List<string> musicaId = new List<string>();
            List<string> musicaTime = new List<string>();
            List<string> musicaType = new List<string>();
            List<string> musicaAlbum = new List<string>();
            List<string> musicaArtist = new List<string>();
            List<string> musicaTitle = new List<string>();
            List<string> musicaPictureUrl = new List<string>();
            
            MatchCollection MC = Regex.Matches(JsonApi, "\"videoId\":\"(?:[^\"]|\"\")*\",");
            foreach (Match item in MC)
            {
                string id = item.Value.Replace("videoId", "").Replace("\"", "").Replace(",", "").Replace(":", "");
                if (!musicaId.Contains(id))
                    musicaId.Add(id);
            }
            
            MatchCollection MCTiempo = Regex.Matches(JsonApi, "\"text\":\"[0-9]+:\\d\\d\"");
            foreach (Match item in MCTiempo)
            {
                string time = item.Value.Remove(0, 7).Replace("\"", "");
                musicaTime.Add(time);
            }

            MatchCollection MCType = Regex.Matches(JsonApi, "\"runs\":\\[\\{\"text\":\"[^\"]*\"\\},");
            for (int i = 0; i < MCType.Count; i++)
            {
                string type = MCType[i].Value.Remove(0, 16).Replace("\"", "").Replace("},", "");
                musicaType.Add(type);
            }
            MatchCollection MCAlbumAndArtist = Regex.Matches(JsonApi, ",\\{\"text\":\" • \"\\},\\{\"text\":\"[^\"]*\",");
            int fin = Convert.ToInt32(MCAlbumAndArtist.Count / 2);
            for (int i = 0; i < fin; i=i+2)
            {
                string arts = $"{MCAlbumAndArtist[i].Value.Remove(0,24).Replace("\"","").TrimEnd(',')}";
                string album = $"{MCAlbumAndArtist[i+1].Value.Remove(0,24).Replace("\"","").TrimEnd(',')}";
                musicaAlbum.Add(album);
                musicaArtist.Add(arts);
            }
            MatchCollection MCTitles = Regex.Matches(JsonApi, "\"runs\":\\[\\{\"text\":\"[^\"]*\",\"navigationEndpoint\":");
            for (int i = 0; i < 6; i++)
            {
                string title = MCTitles[i].Value.Remove(0, 17).Replace("\",\"navigationEndpoint\":", "");
                musicaTitle.Add(title);
            }
            MatchCollection MCPictures = Regex.Matches(JsonApi, "\"thumbnails\":\\[\\{\"url\":\"[^\"]*\"");
            int c = 0;
            for (int i = 0; i < MCPictures.Count; i++)
            {
                string url = MCPictures[i].Value.Remove(0, 22).Replace("=w60-h60-l90-rj\"", ""); 
                musicaPictureUrl.Add(url); 
                c++; 
                if(c==7) 
                    break;
            }

            List<MusicData> MDList = new List<MusicData>();
            for (int i = 0; i < musicaAlbum.Count; i++)
            {
                MusicData MD = new MusicData(musicaId[i],musicaTitle[i],musicaArtist[i],musicaAlbum[i],musicaTime[i],musicaPictureUrl[i],"0000");
                MDList.Add(MD);
            }
            return MDList.ToArray();
        }

        public async Task<MusicData> GetMetadata(string id)
        {
            var handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.None;
            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                           "https://music.youtube.com/youtubei/v1/player?key=AIzaSyC9XL3ZjWddXya6X74dJoCTL-WEYFDNX30&prettyPrint=false"))
                {
                    request.Headers.TryAddWithoutValidation("authority", "music.youtube.com");
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    request.Headers.TryAddWithoutValidation("accept-language", "es");
                    request.Headers.TryAddWithoutValidation("cookie",
                        "YSC=0Fh8ngZgm9g; DEVICE_INFO=ChxOekl3TVRVek5USTJNRGMwTmpJMU16ZzJPUT09EKr8w58GGKr8w58G; VISITOR_INFO1_LIVE=XL5YP0Ec86A; _gcl_au=1.1.532838402.1676738092; PREF=autoplay=true");
                    request.Headers.TryAddWithoutValidation("origin", "https://music.youtube.com");
                    request.Headers.TryAddWithoutValidation("referer", $"https://music.youtube.com/watch?v={id}");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua",
                        "\"Not?A_Brand\";v=\"8\", \"Chromium\";v=\"108\"");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua-arch", "\"x86\"");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua-bitness", "\"64\"");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua-full-version", "\"108.0.5359.94\"");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua-full-version-list",
                        "\"Not?A_Brand\";v=\"8.0.0.0\", \"Chromium\";v=\"108.0.5359.94\"");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua-platform", "\"Linux\"");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua-platform-version", "\"5.10.0\"");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua-wow64", "?0");
                    request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
                    request.Headers.TryAddWithoutValidation("sec-fetch-mode", "cors");
                    request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
                    request.Headers.TryAddWithoutValidation("user-agent",
                        "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36");
                    request.Headers.TryAddWithoutValidation("x-goog-visitor-id", "CgtYTDVZUDBFYzg2QSi4_MOfBg%3D%3D");
                    request.Headers.TryAddWithoutValidation("x-youtube-bootstrap-logged-in", "false");
                    request.Headers.TryAddWithoutValidation("x-youtube-client-name", "67");
                    request.Headers.TryAddWithoutValidation("x-youtube-client-version", "1.20230213.01.00");

                    request.Content = new StringContent("{\"videoId\":\"" + id +
                                                        "\",\"context\":{\"client\":{\"hl\":\"es-419\",\"gl\":\"SV\",\"remoteHost\":\"190.62.3.155\",\"deviceMake\":\"\",\"deviceModel\":\"\",\"visitorData\":\"CgtYTDVZUDBFYzg2QSi4_MOfBg%3D%3D\",\"userAgent\":\"Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36,gzip(gfe)\",\"clientName\":\"WEB_REMIX\",\"clientVersion\":\"1.20230213.01.00\",\"osName\":\"X11\",\"osVersion\":\"\",\"originalUrl\":\"https://music.youtube.com/watch?v=" +
                                                        id +
                                                        "\",\"platform\":\"DESKTOP\",\"clientFormFactor\":\"UNKNOWN_FORM_FACTOR\",\"configInfo\":{\"appInstallData\":\"CLj8w58GEIfdrgUQoqf-EhDthq8FELCk_hIQzN-uBRCC3a4FEP24_RIQuIuuBRDn964FEOWg_hIQ2umuBQ%3D%3D\"},\"browserName\":\"Chrome\",\"browserVersion\":\"108.0.0.0\",\"acceptHeader\":\"text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9\",\"deviceExperimentId\":\"ChxOekl3TVRVek5USTJNRGMwTmpJMU16ZzJPUT09ELj8w58GGKr8w58G\",\"screenWidthPoints\":1600,\"screenHeightPoints\":763,\"screenPixelDensity\":1,\"screenDensityFloat\":1,\"utcOffsetMinutes\":-360,\"userInterfaceTheme\":\"USER_INTERFACE_THEME_LIGHT\",\"connectionType\":\"CONN_CELLULAR_3G\",\"timeZone\":\"America/El_Salvador\",\"playerType\":\"UNIPLAYER\",\"tvAppInfo\":{\"livingRoomAppMode\":\"LIVING_ROOM_APP_MODE_UNSPECIFIED\"},\"clientScreen\":\"WATCH_FULL_SCREEN\"},\"user\":{\"lockedSafetyMode\":false},\"request\":{\"useSsl\":true,\"internalExperimentFlags\":[],\"consistencyTokenJars\":[]},\"clientScreenNonce\":\"MC4xNjQ4NDcwODU3MjIyMTA0\",\"adSignalsInfo\":{\"params\":[{\"key\":\"dt\",\"value\":\"1676738106273\"},{\"key\":\"flash\",\"value\":\"0\"},{\"key\":\"frm\",\"value\":\"0\"},{\"key\":\"u_tz\",\"value\":\"-360\"},{\"key\":\"u_his\",\"value\":\"2\"},{\"key\":\"u_h\",\"value\":\"900\"},{\"key\":\"u_w\",\"value\":\"1600\"},{\"key\":\"u_ah\",\"value\":\"868\"},{\"key\":\"u_aw\",\"value\":\"1600\"},{\"key\":\"u_cd\",\"value\":\"24\"},{\"key\":\"bc\",\"value\":\"31\"},{\"key\":\"bih\",\"value\":\"763\"},{\"key\":\"biw\",\"value\":\"1600\"},{\"key\":\"brdim\",\"value\":\"0,0,0,0,1600,0,1600,868,1600,763\"},{\"key\":\"vis\",\"value\":\"1\"},{\"key\":\"wgl\",\"value\":\"true\"},{\"key\":\"ca_type\",\"value\":\"image\"}]},\"clickTracking\":{\"clickTrackingParams\":\"IhMIlNjYmMCf_QIVn6rRBB1KrQfwMghleHRlcm5hbA==\"}},\"playbackContext\":{\"contentPlaybackContext\":{\"html5Preference\":\"HTML5_PREF_WANTS\",\"lactMilliseconds\":\"1415\",\"referer\":\"https://music.youtube.com/watch?v=LSRRFbpqcyo\",\"signatureTimestamp\":19404,\"autoCaptionsDefaultOn\":false,\"mdxContext\":{}}},\"cpn\":\"W3SdULrEoSuDbj5u\",\"params\":\"igMDCNgEoAME\",\"captionParams\":{}}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    string sd = await response.Content.ReadAsStringAsync();
                    File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),id+".json"),sd);
                    Root d = JsonSerializer.Deserialize<Root>(sd);
                    return new MusicData(d.videoDetails.videoId, 
                        d.videoDetails.title,
                        d.videoDetails.author,
                        d.microformat.microformatDataRenderer.tags[1],
                        d.microformat.microformatDataRenderer.videoDetails.durationSeconds,
                        d.videoDetails.thumbnail.thumbnails.Last().url,
                        d.microformat.microformatDataRenderer.year);
                }
            }
        }

        private string JsonParse(string json)
        {
            json = json.Replace("'});ytcfg.set", "");
            int cont = json.IndexOf("data: '{\"response") + 7;
            string nuevo = json.Remove(0, cont);
            return nuevo;
        }
        public string GetJson()
        {
            return JsonApi;
        }
    }
}