namespace YTMUSICAPI
{
    public class YtmusicParse
    {
        private List<string> ids = new List<string>();
        private List<string> timeList = new List<string>();
        private List<int> timeSS = new List<int>();
        public YtmusicParse(List<string> ids, List<string> time)
        {
            this.ids = ids;
            this.timeList = time;

            for (int i = 0; i < time.Count; i++)
            {
                int t = GetDurationS(i);
                timeSS.Add(t);
            }
        }

        public YtmusicParse(){
        }

        public void SetIdsAndTimes(List<string> ids, List<string> time)
        {
            this.ids = ids;
            this.timeList = time;

            for (int i = 0; i < time.Count; i++)
            {
                int t = GetDurationS(i);
                timeSS.Add(t);
            }
        }

        public string GetIdforTime(int tiempo)
        {
            if (tiempo == 0)
            {
                return ids[0];
            }
            if (timeSS.Contains(tiempo))
            {
                int index = timeSS.IndexOf(tiempo);
                return ids[index];
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
                        return ids[cuantos];
                    }
                    hasta++;
                    cuantos++;
                } while (cuantos < ids.Count);
            }
            return ids[0];
        }
        private string GetId(int position)
        {
            if (position >= ids.Count)
                throw new Exception("Posicion fuera de alcanze");
            return ids[position];
        }

        private int GetDurationS(int position)
        {
            if (position >= timeList.Count)
                throw new Exception("Posicion fuera de alcanze");

            string[] tiempos = timeList[position].Split(':');//tiempo.Split(':');

            if (tiempos.Length == 1)
            {
                return Convert.ToInt32(tiempos[0]);
            }
            else if (tiempos.Length == 2)
            {
                return Convert.ToInt32(tiempos[0]) * 60 + Convert.ToInt32(tiempos[1]);
            }
            else if (tiempos.Length == 3)
            {
                return Convert.ToInt32(tiempos[0]) * 3600 + Convert.ToInt32(tiempos[1]) * 60 + Convert.ToInt32(tiempos[2]);
            }
            else
            {
                return -1;
            }
        }

        private (string, int) GetIdAndTime(int position)
        {
            if (position >= timeList.Count)
                throw new Exception("Posicion fuera de alcanze");
            if (position >= ids.Count)
                throw new Exception("Posicion fuera de alcanze");

            return (ids[position], GetDurationS(position));
        }
    }
}