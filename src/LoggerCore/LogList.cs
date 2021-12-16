using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LoggerCore
{
    public class LogList: List<Log> 
    {
        public static Log getCurrentLog(Settings settings)
        {
            var tmp_list = LogList.getCurrentLogList(settings);
            if (tmp_list.Any())
            {
                tmp_list.Sort((x, y) => x.date.CompareTo(y.date));
                return tmp_list.Last();
            }
            else
            {
                return new Log();
            }            
        }

        public static LogList getCurrentLogList(Settings settings)
        {
            settings.status = "getLogList";
            var download_callback = Logger.Download(settings.LogURL, "logs/currentList.log");
            if (download_callback.status)
            {
                settings.status = ("Logger.Download Complete");
            }
            else
            {
                settings.status = ("Logger.Download Error: " + download_callback.exception);
                return new LogList();
            }

            
            var chathtmlFileData = File.ReadLines(@"" + Environment.CurrentDirectory + "/logs/currentList.log").ToList();

            // возможно тут > 6 или 7 или 4 хз
            // я не предусматривал случай если в папке не будет логов 
            if (chathtmlFileData.Count>5)
            {
                LogList tmp_loglist = new LogList();

                for (int i = 4; i <= chathtmlFileData.Count - 3; i++)
                {
                    Log tmp_log = new Log();
                    tmp_log.name = chathtmlFileData[i].Substring(9, 10);
                    tmp_log.url = settings.LogURL + chathtmlFileData[i].Substring(9, 10) + ".txt";
                    tmp_log.date = DateTime.ParseExact(chathtmlFileData[i].Substring(9, 10), "dd-MM-yyyy",
                        System.Globalization.CultureInfo.InvariantCulture);

                    tmp_loglist.Add(tmp_log);
                }
                tmp_loglist.Sort((x, y) => y.date.CompareTo(x.date));
                return tmp_loglist;
            }
            else
            {
                return new LogList();
            }            
        }        
    }

    public class Log
    {
        public string name = "";
        public DateTime date;
        public string url = "";

        public List<string> getLog()
        {
            if (this.url.Length>0)
            {
                var download_callback = Logger.Download(this.url, "logs/" + this.name + ".log");
                if (download_callback.status)
                {
                    return new List<string>(File.ReadAllLines("logs/" + this.name + ".log"));
                }
                else
                {
                    return new List<string>();
                }
            }
            else
                return new List<string>();

        }
    }

}
