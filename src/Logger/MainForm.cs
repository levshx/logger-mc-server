using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;



namespace Logger
{
    

    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        private const int WM_VSCROLL = 0x115;
        private const int SB_BOTTOM = 7;

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam,
        IntPtr lParam);



        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern
            Boolean PlaySound(string lpszName, int hModule, int dwFlags);

        public class WarpAdMessage
        {
            public DateTime DateTimeOfLastMessage;
            public string Nick;
            public string LastMessage;
        }

        public class ComerAdMessage
        {
            public DateTime DateTimeOfLastMessage;
            public string Nick;
            public string LastMessage;
        }

        public class SaveSettings
        {
            public bool https;
            public string chatUrl;
            public string dropUrl;
            public string deathUrl;
            public bool adCommerce;
            public bool obscenity;
            public bool adWarp;
            public bool restartedTrigger;
            public List<string> adCommerceWords = new List<string>();
            public List<string> adWarpWords = new List<string>();
            public List<string> commandsTrigger = new List<string>();
            public List<string> obscenityWords = new List<string>();
            public List<string> playerJoinTrigger = new List<string>();
            public List<string> publicWordsTrigger = new List<string>();
            public List<string> localWordsTrigger = new List<string>();
            public List<string> messageWordsTrigger = new List<string>();
        }

        public Random random = new Random();

        public WebClient webClient = new WebClient(); //main thread
        public WebClient webClient2 = new WebClient(); //all logs
        public WebClient webClient3 = new WebClient(); // drop
        public WebClient webClient4 = new WebClient(); // death

        public const String chatPath = "logs/chat.txt";
        public const String oldchatPath = "logs/oldchat.txt";
        public const String chathtmlPath = "logs/chat.html.txt";
        public const String deathPath = "logs/death.txt";
        public const String deathhtmlPath = "logs/death.html.txt";
        public const String dropPath = "logs/drop.txt";
        public const String settingsPath = "settings.json";
        public const String soundPath = "sound/info.mp3";
        public SaveSettings settings = new SaveSettings();
        public int chatLine = 0; // последняя обработанная линия в логе
        public string protocol = "http";
        public DateTime LogDate;        

        public Thread thread;

        public MainForm()
        {
            InitializeComponent();
            settings  = JsonConvert.DeserializeObject<SaveSettings>(File.ReadAllText(@"" + Environment.CurrentDirectory + "/" + settingsPath));
            
            metroTextBox3.Text = settings.chatUrl;
            metroTextBox4.Text = settings.dropUrl;
            metroTextBox5.Text = settings.deathUrl;
            metroCheckBox1.Checked = settings.https;
            metroCheckBox5.Checked = settings.restartedTrigger;
            metroCheckBox3.Checked = settings.adCommerce;
            metroCheckBox4.Checked = settings.adWarp;
            metroCheckBox6.Checked = settings.obscenity;

            if (settings.https)
            {
                protocol = "https";        
            }
            else
            {
                protocol = "http";
            }
            LoggerInit();
            thread = new Thread(LoggerUpdate);
            thread.Start();
        }

        private void LoggerInit() // Предзагрузка
        {

            var wmp = new WMPLib.WindowsMediaPlayer();
            wmp.URL = @"" + Environment.CurrentDirectory + "/" + soundPath;
            if (File.Exists(chathtmlPath))
            {
                File.Delete(chathtmlPath);
            }
            webClient.DownloadFile(@"" + protocol + "://" + settings.chatUrl, @"" + Environment.CurrentDirectory + "/" + chathtmlPath);
            var chathtmlFileData = File.ReadLines(@"" + Environment.CurrentDirectory + "/" + chathtmlPath).ToList();

            var logsDates = new List<DateTime>();

            for (int i = 4; i <= chathtmlFileData.Count - 3; i++)
            {
                logsDates.Add(DateTime.ParseExact(chathtmlFileData[i].Substring(9, 10), "dd-MM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture)
                    );
            }
            logsDates.Sort();

            if (LogDate != logsDates[logsDates.Count - 1])
            {
                LogDate = logsDates[logsDates.Count - 1];

                listBox2.Items.Clear();
                for (int i = 0; i < logsDates.Count; i++)
                {
                    listBox2.Items.Add(logsDates[i].ToString("dd-MM-yyyy"));
                }
                SendMessage(listBox2.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
                // НОВАЯ ДАТА

                richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                richTextBox1.AppendText("\n" + "[NETWORK] ");
                richTextBox1.SelectionColor = Color.FromArgb(230, 230, 230);
                richTextBox1.AppendText("SUCCESSFULLY CONNECTED TO LOG: ");
                richTextBox1.SelectionColor = Color.FromArgb(180, 80, 0);
                richTextBox1.AppendText(LogDate.ToString("dd-MM-yyyy"));
                

                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipText = "LOG: " + LogDate.ToString("dd-MM-yyyy");
                notifyIcon1.BalloonTipTitle = "DATE";
                notifyIcon1.ShowBalloonTip(2);


                wmp.controls.play();

                // /НОВАЯ ДАТА
            }

            // ПОЛУЧЕНИЕ КОЛ-ВА СТРОК

            if (File.Exists(chatPath))
            {
                File.Delete(chatPath);
            }
            webClient.DownloadFile(@"" + protocol + "://" + settings.chatUrl+LogDate.ToString("dd-MM-yyyy") + ".txt", @"" + Environment.CurrentDirectory + "/" + chatPath);
            var chatData = File.ReadLines(@"" + Environment.CurrentDirectory + "/" + chatPath).ToList();
            chatLine = chatData.Count-1;
            chatData.Clear();
        }



        private void LoggerUpdate() // ОСНОВНОЙ ЦИКЛ В ПОТОКЕ
        {
            var wmp = new WMPLib.WindowsMediaPlayer();
            wmp.URL = @"" + Environment.CurrentDirectory + "/" + soundPath;
            DateTime messageDateTime = new DateTime();
            string nick;
            string command;
            string killedNick;
            string messageText;


            List<WarpAdMessage> WarpAdData  = new List<WarpAdMessage>();
            WarpAdMessage WarpAdBuffer = new WarpAdMessage();

            List<ComerAdMessage> ComerAdData = new List<ComerAdMessage>();
            ComerAdMessage ComerAdBuffer = new ComerAdMessage();

            while (true) if (metroToggle1.Checked)
            {
                    //Обработка даты               
                    
                if (File.Exists(chathtmlPath))
                {
                    File.Delete(chathtmlPath);
                }

                webClient2.DownloadFile(@"" + protocol + "://" + settings.chatUrl, @"" + Environment.CurrentDirectory + "/" + chathtmlPath);

                var chathtmlFileData = File.ReadLines(@"" + Environment.CurrentDirectory + "/" + chathtmlPath).ToList();

                var logsDates = new List<DateTime>();

                for (int i = 4; i <= chathtmlFileData.Count - 3; i++)
                {
                    logsDates.Add(DateTime.ParseExact(chathtmlFileData[i].Substring(9, 10), "dd-MM-yyyy",
                        System.Globalization.CultureInfo.InvariantCulture)
                        );
                }
                logsDates.Sort();

                if (LogDate != logsDates[logsDates.Count - 1])
                {
                    LogDate = logsDates[logsDates.Count - 1];

                    listBox2.Items.Clear();
                    for (int i = 0; i < logsDates.Count; i++)
                    {
                        listBox2.Items.Add(logsDates[i].ToString("dd-MM-yyyy"));
                    }

                    // НОВАЯ ДАТА

                    richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                    richTextBox1.AppendText("\n" + "[NETWORK] ");
                    richTextBox1.SelectionColor = Color.FromArgb(230, 230, 230);
                    richTextBox1.AppendText("SUCCESSFULLY CONNECTED TO LOG: ");
                    richTextBox1.SelectionColor = Color.FromArgb(180, 80, 0);
                    richTextBox1.AppendText(LogDate.ToString("dd-MM-yyyy"));
                                        


                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon1.BalloonTipText = "LOG: " + LogDate.ToString("dd-MM-yyyy");
                    notifyIcon1.BalloonTipTitle = "DATE";
                    notifyIcon1.ShowBalloonTip(2);

                                        
                    wmp.URL = @"" + Environment.CurrentDirectory + "/" + soundPath;
                    wmp.controls.play();

                    // /НОВАЯ ДАТА
                }

                //Обработка сообзений 
                // ГЛАВНАЯ ЛОГГЕРА

                if (File.Exists(chatPath))
                {
                    File.Delete(chatPath);
                }

                webClient2.DownloadFile(@"" + protocol + "://" + settings.chatUrl + LogDate.ToString("dd-MM-yyyy") + ".txt", @"" + Environment.CurrentDirectory + "/" + chatPath);
                var chatData = File.ReadLines(@"" + Environment.CurrentDirectory + "/" + chatPath).ToList();

                if (chatData.Count > 1) { 
                    for (int i = chatLine; i < chatData.Count; i++)
                    {
                        //chatData[i]
                        messageDateTime = DateTime.ParseExact(chatData[i].Substring(1, 8) + "/" + LogDate.ToString("dd-MM-yyyy"), "HH:mm:ss/dd-MM-yyyy", 
                            System.Globalization.CultureInfo.InvariantCulture);
                        if (chatData[i].Substring(11,1)=="[")
                        {
                            if (chatData[i].Substring(12, 1) == "G")
                            {
                                    nick = chatData[i].Substring(15, chatData[i].Length-15);
                                    nick = nick.Substring(0, nick.IndexOf(":"));
                                    messageText = chatData[i].Substring(18 + nick.Length, (chatData[i].Length - 18)-nick.Length);

                                    messageText = messageText.Replace("&1", "");
                                    messageText = messageText.Replace("&2", "");
                                    messageText = messageText.Replace("&3", "");
                                    messageText = messageText.Replace("&4", "");
                                    messageText = messageText.Replace("&5", "");
                                    messageText = messageText.Replace("&6", "");
                                    messageText = messageText.Replace("&7", "");
                                    messageText = messageText.Replace("&8", "");
                                    messageText = messageText.Replace("&9", "");
                                    messageText = messageText.Replace("&a", "");
                                    messageText = messageText.Replace("&b", "");
                                    messageText = messageText.Replace("&c", "");
                                    messageText = messageText.Replace("&d", "");
                                    messageText = messageText.Replace("&e", "");
                                    messageText = messageText.Replace("&f", "");
                                    messageText = messageText.Replace("&g", "");
                                    messageText = messageText.Replace("&k", "");

                                    richTextBox1.AppendText("\n");
                                    richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                    richTextBox1.AppendText("[" + messageDateTime.ToString("HH:mm:ss") + "] ");
                                    richTextBox1.SelectionColor = Color.FromArgb(0, 180, 255);
                                    richTextBox1.AppendText("[G] ");
                                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Bold);
                                    richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                    richTextBox1.AppendText(nick+ ": ");
                                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Regular);
                                    richTextBox1.SelectionColor = Color.FromArgb(0, 180, 255);
                                    richTextBox1.AppendText(messageText);

                                    if (settings.obscenity)
                                    {
                                        for (int ii = 0; ii < settings.obscenityWords.Count; ii++)
                                        {
                                            if (messageText.ToLower().IndexOf(settings.obscenityWords[ii].ToLower()) > -1)
                                            {
                                                richTextBox1.SelectionColor = Color.FromArgb(255, 40, 0);
                                                richTextBox1.AppendText("\n[LOGGER] 3.2 " + nick + " цензура: "+ settings.obscenityWords[ii]);
                                                richTextBox1.AppendText("\n  >>>>> " + messageText);


                                                notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                                                notifyIcon1.BalloonTipText = " 3.2 " + nick + " цензура: " + settings.obscenityWords[ii];
                                                notifyIcon1.BalloonTipTitle = " 3.2 " + nick + " цензура: " + settings.obscenityWords[ii];
                                                notifyIcon1.ShowBalloonTip(2);


                                                wmp.URL = @"" + Environment.CurrentDirectory + "/" + soundPath;
                                                wmp.controls.play();
                                            }
                                        }
                                    }

                                    if (settings.adWarp) // РЕКЛАМА ВАРПОВ
                                    {
                                        for (int ii = 0; ii < settings.adWarpWords.Count; ii++)
                                        {
                                            if (messageText.ToLower().IndexOf(settings.adWarpWords[ii].ToLower()) > -1)
                                            {
                                                foreach (WarpAdMessage WarpAdMsg in WarpAdData)
                                                {
                                                    if (WarpAdMsg.Nick == nick)
                                                    {
                                                        if (messageDateTime - WarpAdMsg.DateTimeOfLastMessage < TimeSpan.FromSeconds(600))
                                                        {
                                                            //ТРИГГЕР НА ВАРП РЕКЛАМУ

                                                            richTextBox1.SelectionColor = Color.FromArgb(255, 40, 0);
                                                            richTextBox1.AppendText("\n[LOGGER] 3.8 " + nick + " не дождал " + (TimeSpan.FromSeconds(600) - (messageDateTime - WarpAdMsg.DateTimeOfLastMessage)).TotalSeconds.ToString() + " сек. на рекламе варпа");
                                                            richTextBox1.AppendText("\n 1 >>>>> " + messageText);
                                                            richTextBox1.AppendText("\n 2 >>>>> " + WarpAdMsg.LastMessage);


                                                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                                                            notifyIcon1.BalloonTipText = nick + " 3.8 не дождал " + (TimeSpan.FromSeconds(600) - (messageDateTime - WarpAdMsg.DateTimeOfLastMessage)).TotalSeconds.ToString() + " сек.";
                                                            notifyIcon1.BalloonTipTitle = nick + " 3.8 не дождал " + (TimeSpan.FromSeconds(600) - (messageDateTime - WarpAdMsg.DateTimeOfLastMessage)).TotalSeconds.ToString() + " сек.";
                                                            notifyIcon1.ShowBalloonTip(2);


                                                            wmp.URL = @"" + Environment.CurrentDirectory + "/" + soundPath;
                                                            wmp.controls.play();

                                                        }


                                                    }

                                                }

                                                for (int iii = 0; iii < WarpAdData.Count; iii++)
                                                {
                                                    if (WarpAdData[iii].Nick == nick)
                                                    {
                                                        WarpAdData.Remove(WarpAdData[iii]);
                                                    }
                                                }


                                                WarpAdBuffer.Nick = nick;
                                                WarpAdBuffer.DateTimeOfLastMessage = messageDateTime;
                                                WarpAdBuffer.LastMessage = messageText;
                                                WarpAdData.Add(WarpAdBuffer);
                                                break;
                                            }
                                        }
                                    }

                                    if (settings.adCommerce) // КОМЕРЦИЯ
                                    {
                                        for (int ii = 0; ii < settings.adCommerceWords.Count; ii++)
                                        {
                                            if (messageText.ToLower().IndexOf(settings.adCommerceWords[ii].ToLower()) > -1)
                                            {
                                                foreach (ComerAdMessage ComerAdMsg in ComerAdData)
                                                {
                                                    if (ComerAdMsg.Nick == nick)
                                                    {
                                                        if (messageDateTime - ComerAdMsg.DateTimeOfLastMessage < TimeSpan.FromSeconds(300))
                                                        {
                                                            //ТРИГГЕР НА Комерцию

                                                            richTextBox1.SelectionColor = Color.FromArgb(255, 40, 0);


                                                            richTextBox1.AppendText("\n[LOGGER] " + nick + " не дождал " + (TimeSpan.FromSeconds(300) - (messageDateTime - ComerAdMsg.DateTimeOfLastMessage)).TotalSeconds.ToString() + " сек. на коммерции");
                                                            richTextBox1.AppendText("\n 1 >>>>> " + messageText);
                                                            richTextBox1.AppendText("\n 2 >>>>> " + ComerAdMsg.LastMessage);


                                                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                                                            notifyIcon1.BalloonTipText = nick + " не дождал " + (TimeSpan.FromSeconds(300) - (messageDateTime - ComerAdMsg.DateTimeOfLastMessage)).TotalSeconds.ToString() + " сек.";
                                                            notifyIcon1.BalloonTipTitle = nick + " не дождал " + (TimeSpan.FromSeconds(300) - (messageDateTime - ComerAdMsg.DateTimeOfLastMessage)).TotalSeconds.ToString() + " сек.";
                                                            notifyIcon1.ShowBalloonTip(2);


                                                            wmp.URL = @"" + Environment.CurrentDirectory + "/" + soundPath;
                                                            wmp.controls.play();

                                                        }


                                                    }

                                                }

                                                for (int iii = 0; iii < ComerAdData.Count; iii++)
                                                {
                                                    if (ComerAdData[iii].Nick == nick)
                                                    {
                                                        ComerAdData.Remove(ComerAdData[iii]);
                                                    }
                                                }

                                                ComerAdBuffer.Nick = nick;
                                                ComerAdBuffer.DateTimeOfLastMessage = messageDateTime;
                                                ComerAdBuffer.LastMessage = messageText;
                                                ComerAdData.Add(ComerAdBuffer);
                                                break;
                                            }
                                        }
                                    }

                                }
                            else
                            {
                                    nick = chatData[i].Substring(15, chatData[i].Length - 15);
                                    nick = nick.Substring(0, nick.IndexOf(":"));
                                    messageText = chatData[i].Substring(17 + nick.Length, (chatData[i].Length - 17) - nick.Length);

                                    messageText = messageText.Replace("&1", "");
                                    messageText = messageText.Replace("&2", "");
                                    messageText = messageText.Replace("&3", "");
                                    messageText = messageText.Replace("&4", "");
                                    messageText = messageText.Replace("&5", "");
                                    messageText = messageText.Replace("&6", "");
                                    messageText = messageText.Replace("&7", "");
                                    messageText = messageText.Replace("&8", "");
                                    messageText = messageText.Replace("&9", "");
                                    messageText = messageText.Replace("&a", "");
                                    messageText = messageText.Replace("&b", "");
                                    messageText = messageText.Replace("&c", "");
                                    messageText = messageText.Replace("&d", "");
                                    messageText = messageText.Replace("&e", "");
                                    messageText = messageText.Replace("&f", "");
                                    messageText = messageText.Replace("&g", "");
                                    messageText = messageText.Replace("&k", "");

                                    richTextBox1.AppendText("\n");
                                    richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                    richTextBox1.AppendText("[" + messageDateTime.ToString("HH:mm:ss") + "] ");
                                    richTextBox1.SelectionColor = Color.FromArgb(0, 255, 180);
                                    richTextBox1.AppendText("[L] ");
                                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Bold);
                                    richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                    richTextBox1.AppendText(nick + ": ");
                                    richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Regular);
                                    richTextBox1.SelectionColor = Color.FromArgb(0, 255, 180);
                                    richTextBox1.AppendText(messageText);                                    
                                }
                        }
                        else if (chatData[i].Substring(11, 1) == "С")
                        {
                            if (chatData[i].Substring(19, 1) == "ы")
                            {
                                richTextBox1.AppendText("\n");
                                richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                richTextBox1.AppendText("[" + messageDateTime.ToString("HH:mm:ss") + "] ");
                                richTextBox1.SelectionColor = Color.FromArgb(180, 10, 10);
                                richTextBox1.AppendText("СЕРВЕР ВЫКЛЮЧИЛСЯ");   
                            }
                            else
                            {
                                richTextBox1.AppendText("\n");
                                richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                richTextBox1.AppendText("[" + messageDateTime.ToString("HH:mm:ss") + "] ");
                                richTextBox1.SelectionColor = Color.FromArgb(10, 180, 10);
                                richTextBox1.AppendText("СЕРВЕР ВКЛЮЧИЛСЯ");
                                if (settings.restartedTrigger == true)
                                {

                                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                                    notifyIcon1.BalloonTipText = "RESTART: " + messageDateTime.ToString("HH:mm:ss");
                                    notifyIcon1.BalloonTipTitle = "SERVER was RESTARTED";
                                    notifyIcon1.ShowBalloonTip(2);

                                    wmp.controls.play();
                                }
                            }

                         
                        }
                        else
                        {
                                nick = chatData[i].Substring(11, chatData[i].Length - 11);
                                nick = nick.Substring(0, nick.IndexOf(" "));

                                switch (chatData[i].Substring(15+nick.Length, 2))
                                {
                                    case " к":
                                        
                                        
                                        //БЫЛ КИКНУТ
                                        richTextBox1.AppendText("\n");
                                        richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                        richTextBox1.AppendText("[" + messageDateTime.ToString("HH:mm:ss") + "] ");
                                        richTextBox1.SelectionColor = Color.FromArgb(139, 0, 255);
                                        richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Bold);
                                        richTextBox1.AppendText(nick);
                                        richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Regular);
                                        richTextBox1.AppendText(" был кикнут");



                                        break;
                                    case "ёл":
                                        //ЗАШЁЛ
                                        for (int q=0; q<settings.playerJoinTrigger.Count; q++)
                                        {
                                            if (settings.playerJoinTrigger[q].ToLower()==nick.ToLower())
                                            {
                                                richTextBox1.SelectionColor = Color.FromArgb(255, 80, 0);
                                                richTextBox1.AppendText("\n[LOGGER] ТРИГГЕР, ЗАШЁЛ: ");
                                                richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Bold);
                                                richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                                richTextBox1.AppendText(nick);
                                                richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Regular);


                                                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                                                notifyIcon1.BalloonTipText = nick+" JOINED";
                                                notifyIcon1.BalloonTipTitle = nick + " ON SERVER";
                                                notifyIcon1.ShowBalloonTip(2);

                                                wmp.controls.play();

                                            }
                                        }

                                        richTextBox1.AppendText("\n");
                                        richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                        richTextBox1.AppendText("[" + messageDateTime.ToString("HH:mm:ss") + "] ");                                        
                                        richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Bold);
                                        richTextBox1.SelectionColor = Color.FromArgb(139, 0, 255);
                                        richTextBox1.AppendText(nick);
                                        richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Regular);
                                        richTextBox1.AppendText(" зашёл");

                                        break;
                                    case "ел":
                                        //ВЫШЕЛ
                                        richTextBox1.AppendText("\n");
                                        richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                        richTextBox1.AppendText("[" + messageDateTime.ToString("HH:mm:ss") + "] ");
                                        richTextBox1.SelectionColor = Color.FromArgb(139, 0, 255);
                                        richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Bold);
                                        richTextBox1.AppendText(nick);
                                        richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Regular);
                                        richTextBox1.AppendText(" вышел");
                                        break;
                                    case "р ":
                                        //УМЕР
                                        break;
                                    case "л ":
                                        //УБИЛ КОГО-ТО
                                        break;
                                    case "ue":
                                        //ВВЁЛ КОМАНДУ

                                        command = chatData[i].Substring(35+nick.Length, chatData[i].Length-(35 + nick.Length));

                                        richTextBox1.AppendText("\n");
                                        richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                        richTextBox1.AppendText("[" + messageDateTime.ToString("HH:mm:ss") + "] ");
                                        richTextBox1.SelectionColor = Color.FromArgb(180, 80, 0);
                                        richTextBox1.AppendText("[C] ");
                                        richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Bold);
                                        richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                        richTextBox1.AppendText(nick + ": ");
                                        richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Regular);
                                        richTextBox1.SelectionColor = Color.FromArgb(180, 80, 0);
                                        richTextBox1.AppendText(command);


                                        
                                        for (int ii = 0; ii < settings.commandsTrigger.Count; ii++) 
                                        {
                                            if (command.ToLower() == settings.commandsTrigger[ii].ToLower())
                                            {
                                                richTextBox1.SelectionColor = Color.FromArgb(255, 40, 0);
                                                richTextBox1.AppendText("\n[LOGGER] ТРИГГЕР КОММАНДЫ: ");
                                                richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Bold);
                                                richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                                richTextBox1.AppendText(nick);
                                                richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size, FontStyle.Regular);
                                                richTextBox1.SelectionColor = Color.FromArgb(255, 40, 0);
                                                richTextBox1.AppendText("\n >>>> "+command);

                                                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                                                notifyIcon1.BalloonTipText = nick +" "+ command;
                                                notifyIcon1.BalloonTipTitle = nick + " was issued command";
                                                notifyIcon1.ShowBalloonTip(2);

                                                wmp.controls.play();
                                            }
                                        }


                                        break;
                                    default:
                                        richTextBox1.AppendText("\n");
                                        richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                                        richTextBox1.AppendText("[" + messageDateTime.ToString("HH:mm:ss") + "] ");
                                        richTextBox1.SelectionColor = Color.FromArgb(180, 80, 0);
                                        richTextBox1.AppendText("ХУЙ ЗНАЕТ ЧТО ЭТО, НО ЭТО ОШИБКА ЛОГГЕРА");
                                        break;

                                }
                        }

                        // richTextBox1.AppendText("\n");
                        //   richTextBox1.SelectionColor = Color.FromArgb(180, 180, 180);
                        //  richTextBox1.AppendText("[" + messageDateTime.ToString("HH:mm:ss") + "] ");
                        //  richTextBox1.SelectionColor = Color.FromArgb(180, 80, 0);
                        //   richTextBox1.AppendText(chatData[i].Substring(10, chatData[i].Length-10));

                        SendMessage(richTextBox1.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);

                        chatLine++;
                    }
                }



                chatData.Clear();
                Thread.Sleep(500);


            // КОНЕЦ WHILE
            }    
            
        }
      

        //тестовая кнопка
        private void metroButton4_Click(object sender, EventArgs e)
        {
            //test
            richTextBox1.SelectionColor = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)); ;
            richTextBox1.AppendText("\n" + "Сосать");
            SendMessage(richTextBox1.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);

        }

        //Хуйня загрузки формы
        private void MainForm_Load(object sender, EventArgs e)
        {
        
        }



        //РЕКОНФИГУРАЦИЯ
        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            SaveSettings Reconfig = new SaveSettings();            
            
            Reconfig.chatUrl = metroTextBox3.Text;
            Reconfig.dropUrl = metroTextBox4.Text;
            Reconfig.deathUrl = metroTextBox5.Text;
            Reconfig.https = metroCheckBox1.Checked;
            Reconfig.restartedTrigger = metroCheckBox5.Checked;
            Reconfig.adCommerce = metroCheckBox3.Checked;
            Reconfig.adWarp = metroCheckBox4.Checked;
            Reconfig.obscenity = metroCheckBox6.Checked;

            Reconfig.adCommerceWords = settings.adCommerceWords;
            Reconfig.adWarpWords = settings.adWarpWords;
            Reconfig.commandsTrigger = settings.commandsTrigger;
            Reconfig.obscenityWords = settings.obscenityWords;
            Reconfig.playerJoinTrigger = settings.playerJoinTrigger;
            Reconfig.publicWordsTrigger = settings.publicWordsTrigger;
            Reconfig.localWordsTrigger = settings.localWordsTrigger;
            Reconfig.messageWordsTrigger = settings.messageWordsTrigger;

            string json = JsonConvert.SerializeObject(Reconfig,Formatting.Indented);
            if (File.Exists(settingsPath))
            {
                File.Delete(settingsPath);
            }

            File.WriteAllText(@"" + Environment.CurrentDirectory + "/" +settingsPath, json);
                        
            System.Windows.Forms.Application.Restart();
            Close();
            System.Environment.Exit(1);

        }

        private void metroButton5_Click(object sender, EventArgs e)
        {

           

        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {

            if (File.Exists(oldchatPath))
            {
                File.Delete(oldchatPath);
            }
            webClient.DownloadFile(@"" + protocol + "://" + settings.chatUrl+listBox2.SelectedItem.ToString()+".txt", @"" + Environment.CurrentDirectory + "/" + oldchatPath);
            richTextBox2.Text = File.ReadAllText(@"" + Environment.CurrentDirectory + "/" + oldchatPath);
            metroTabControl1.SelectTab(1);
            richTextBox2.SelectionStart = richTextBox2.Text.Length;
            richTextBox2.ScrollToCaret();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
        }
        
    }
}
