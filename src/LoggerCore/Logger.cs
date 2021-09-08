using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.ComponentModel;


namespace LoggerCore
{
    public partial class Logger
    {
        public Settings settings = new Settings();
        
        // Поток
        static ParameterizedThreadStart parsing = new ParameterizedThreadStart(Parsing);
        static Thread thread = new Thread(parsing);

        public Logger()
        {
            ref Settings settingsPoint = ref settings;           
            thread.Start(settingsPoint);
        }
        
        public void Start()
        {
            settings.work = true;
        }

        public void Stop()
        {
            settings.work = false;
        }

        public LogLines GetLines()
        {
            //создание копии LogLines
            LogLines tmp_loglines_output = new LogLines();
            LogLines tmp_currentLogLines = settings.currentLogLines;
            try
            {
                foreach (LogLine item in tmp_currentLogLines.ToArray())
                {
                    try
                    {
                        tmp_loglines_output.Add(new LogLine(item.datetime, item.type, item.nick, item.nick2, item.text, item.name, item.triggerType));
                        //удаление старых записей
                        settings.currentLogLines.Remove(item);
                    }
                    catch
                    {
                        return tmp_loglines_output;
                    }
                }
            }
            catch
            {
                return tmp_loglines_output;
            }

            return tmp_loglines_output;
        }

        public LogList GetLogList()
        {
            return settings.currentLogList;
        }

        public void Terminate()
        {
            thread.Abort();
            thread.Join();
        }


        static void Parsing(object threadPoint)
        {
            Settings settingsPoint = threadPoint as Settings;

            settingsPoint.currentLog = LogList.getCurrentLog(settingsPoint);
            settingsPoint.currentLogList = LogList.getCurrentLogList(settingsPoint);

            var currentLogStringList = settingsPoint.currentLog.getLog();
            int log_line_number = currentLogStringList.Count()-1;
            while (true)
            {
                if (settingsPoint.work == true)
                {
                    settingsPoint.currentLog = LogList.getCurrentLog(settingsPoint);
                    settingsPoint.currentLogList = LogList.getCurrentLogList(settingsPoint);

                    currentLogStringList = settingsPoint.currentLog.getLog();

                    if (currentLogStringList.Count > 0)
                    {

                        for (int i = log_line_number; i < currentLogStringList.Count; i++)
                        {
                            var tmp_new_line = new LogLine(DateTime.Now, 0, null, null, null, null, 0);
                            tmp_new_line.text = currentLogStringList[i];
                            settingsPoint.currentLogLines.Add(tmp_new_line);


                            // Устанавливаем новую текущую строку
                            log_line_number++;
                        }
                        
                    }               

                }                
                Thread.Sleep(settingsPoint.sleep);                
            }            
        }

        public static (bool status, WebException exception) Download(string url, string file_name)
        {
            using (var client = new WebClient())
            {
                try { client.DownloadFile(url, file_name); }
                catch (WebException webEx)
                {
                    return (false, webEx); 
                }
                return (true, new WebException());
            }
        }
        

    }
}
