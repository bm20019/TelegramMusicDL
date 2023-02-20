using System.Globalization;
using System.Text.RegularExpressions;

namespace YTMUSICAPI
{
    public class SearchYtMusic
    {
        public string GetUrlMusic(string Query, int OriginalSS = 0, Provider proveedorAGenerar = Provider.YtMusic)
        {
            if (proveedorAGenerar == Provider.YtVideo)
                return "https://www.youtube.com/watch?v=" + GetId(Query, OriginalSS);
            else
                return "https://music.youtube.com/watch?v=" + GetId(Query, OriginalSS);
        }
        private string GetId(string Query, int OriginalSS = 0)
        {
            string url_Base = "https://music.youtube.com/search?q=";
            string EncodeUrl = url_Base + System.Web.HttpUtility.UrlEncode(Query);
            Ytmusic yt = new Ytmusic();
            string id = yt.GetIdYtMusic(EncodeUrl, OriginalSS);
            Json = yt.GetJson();
            return id;
        }

        public MusicData[] GeListMusic(string Query)
        {
            string url_Base = "https://music.youtube.com/search?q=";
            string EncodeUrl = url_Base + System.Web.HttpUtility.UrlEncode(Query);
            Ytmusic yt = new Ytmusic();
            return yt.GetData(EncodeUrl);
        }
        
        public MusicData GeMusicData(string Query,int duracion=0)
        {
            string url_Base = "https://music.youtube.com/search?q=";
            string EncodeUrl = url_Base + System.Web.HttpUtility.UrlEncode(Query);
            Ytmusic yt = new Ytmusic();
            MusicData[] datos = yt.GetData(EncodeUrl);
            List<int> timeSS = new List<int>();
            
            foreach (MusicData VARIABLE in datos)
            {
                timeSS.Add(getimeSS(VARIABLE.Time));
            }
            
            for (int i = 0; i <datos.Length ; i++)
            {
                int tiempo = getimeSS(datos[i].Time);
                if (tiempo == duracion)
                {
                    return datos[i];
                }
                else
                {
                    int cuantos = 0, hasta = 1;
                    do
                    {
                        //Console.WriteLine($"Comparando {tiempo}/{timeSS[cuantos]}");
                        if (tiempo >= timeSS[cuantos] - hasta && tiempo <= timeSS[cuantos] + hasta)
                        {
                            //Console.WriteLine($"Se encontro: {ids[i]} | {timeList[i]}");
                            return datos[cuantos];
                        }
                        hasta++;
                        cuantos++;
                    } while (cuantos < datos.Length);
                }
                return datos[i];
            }

            return datos[0];
        }
        public async Task<MusicData> GeMusicDataforUrl(string URLMusic)
        {
            if (URLMusic.StartsWith("https://music.youtube.com/watch") == false) throw new Exception("No es url"); 
            string videoId = URLMusic.Split("=")[1].Split("&")[0];
            Ytmusic yt = new Ytmusic();
            return await yt.GetMetadata(videoId);
        }

        int getimeSS(string tiempo)
        {
            string longTime = tiempo;
            TimeSpan duration = TimeSpan.Parse(longTime, CultureInfo.InvariantCulture);
            int seconds = (int)duration.TotalSeconds;
            return seconds;
        }
        private string Json {get;set;}
        public string GetJson(){
            return Json;
        }
    }
}