using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace LoggerCore
{
    public class SettingsData
    {
        public string LogURL;
        public string DeathURL;
        public string AllDropURL;
        public string PlayerDropURL;
        public int sleep;
        public Triggers triggers;
        public bool firstRun;
    }
    public class Settings : SettingsData
    {
        // условный ссылочный репозиторий переменных для потока
        public bool work = false;
        public string status;
        public int lastLogLine;
        public LogLines currentLogLines = new LogLines();
        public LogList currentLogList = new LogList();
        public Log currentLog = new Log();
        public MessagesRelations msgRelations = new MessagesRelations();
        // его конец

        public Settings()
        {    
            if (File.Exists("settings.json"))
            {
                string jsonString = File.ReadAllText("settings.json");
                SettingsData values_tmp = JsonConvert.DeserializeObject<SettingsData>(jsonString);
                this.LogURL = values_tmp.LogURL;
                this.DeathURL = values_tmp.DeathURL;
                this.AllDropURL = values_tmp.AllDropURL;
                this.PlayerDropURL = values_tmp.PlayerDropURL;
                this.sleep = values_tmp.sleep;
                this.triggers = values_tmp.triggers;
            }
            else
            {
                SettingsData tmp_data = new SettingsData();

                // Standart settings
                this.LogURL = tmp_data.LogURL = "http://logs.s9.mcskill.net/Technomagic2_public_logs/";
                this.DeathURL = tmp_data.DeathURL = "http://logs.s9.mcskill.net/Technomagic2_logger_public_logs/Death/";
                this.AllDropURL = tmp_data.AllDropURL = "http://logs.s9.mcskill.net/Technomagic2_logger_public_logs/Drop/All/";
                this.PlayerDropURL = tmp_data.PlayerDropURL = "http://logs.s9.mcskill.net/Technomagic2_logger_public_logs/Drop/Players/";
                this.sleep = tmp_data.sleep = 1000;
                this.triggers = tmp_data.triggers = new Triggers();

              
                string output = JsonConvert.SerializeObject(tmp_data, Formatting.Indented);
                File.WriteAllText(@"settings.json", output);
            }
        }

        public void SaveSettings(SettingsData newSettings)
        {
            this.LogURL = newSettings.LogURL;
            this.DeathURL = newSettings.DeathURL;
            this.AllDropURL = newSettings.AllDropURL;
            this.PlayerDropURL = newSettings.PlayerDropURL;
            this.sleep = newSettings.sleep;
            this.triggers = newSettings.triggers;

            string output = JsonConvert.SerializeObject(newSettings, Formatting.Indented);
            File.WriteAllText(@"settings.json", output);
        }
    }

}
