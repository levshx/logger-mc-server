using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using LoggerCore;
using System.Net;

namespace Logger
{
    public partial class FormGUI : MetroFramework.Forms.MetroForm
    {
        public Random random = new Random();
        public LoggerCore.Logger core = new LoggerCore.Logger();
        public Audio audio = new Audio();

        public FormGUI()
        {
            InitializeComponent();
            this.StyleManager = this.metroStyleManager;
            versionLabel.Text = "version " + Application.ProductVersion;
            core.Start();
        }

        private void FormGUI_Load(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api.github.com/repos/levshx/logger-mc-server/releases");
                request.UserAgent = ".NET Framework Test Client";
                request.Method = "GET";
                string jsonString = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    jsonString = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }

                if (jsonString != string.Empty)
                {
                    var githubObj = JArray.Parse(jsonString);

                    var tagText = Convert.ToString(githubObj[0]["tag_name"]);
                    var newVersionURL = Convert.ToString(githubObj[0]["html_url"]);
                    var newVersionBody = Convert.ToString(githubObj[0]["body"]);

                    if (Application.ProductVersion != tagText)
                    {
                        DialogResult dr = MetroFramework.MetroMessageBox.Show(this,
                        "\nThe new version ("+ tagText + ") is available. Current version: ("+ 
                        Application.ProductVersion+ ")\nNew features:\n" + newVersionBody,
                        "Update",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,300);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(newVersionURL);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                MetroFramework.MetroMessageBox.Show(this,
                    "Connect to github error: "+ex, "Check update error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            MetroFramework.MetroMessageBox.Show(this, core.settings.currentLog.name);
        }

        private void FormGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            core.Terminate();
        }

        private void LogsListBox_DoubleClick(object sender, EventArgs e)
        {            
            if (this.LogsListBox.SelectedIndex > -1)
            {
                DialogResult dr = MetroFramework.MetroMessageBox.Show(this,
                    "Yes -> Open log " + LogsListBox.SelectedItem.ToString() + " in browser.\n" +
                    "No  -> Open log " + LogsListBox.SelectedItem.ToString() + " notepad.",
                    "Open Log",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(core.settings.LogURL+ LogsListBox.SelectedItem.ToString()+".txt");
                }
                else if (dr == DialogResult.No)
                {
                    // Скачать лог и открыть его
                }
            }
        }

        private void LogUpdateTimer_Tick(object sender, EventArgs e)
        {
            LoggerCore.LogLines tmp_loglines = new LogLines();
            tmp_loglines = core.GetLines();
            tmp_loglines.ForEach(DrawLine); // вызов функции обработки строки
            
            
            if (LogsListBox.Items.Count < core.GetLogList().Count)
            {
                LogsListBox.Items.Clear();
                core.GetLogList().ForEach(AddLogToLogList);
                void AddLogToLogList(Log tmp_log)
                {
                    LogsListBox.Items.Add(tmp_log.name);
                }
            }
            
        }

        private void WorkToogle_CheckedChanged(object sender, EventArgs e)
        {           
            if (WorkToogle.Checked == true)
            {
                core.Start();
            }
            else
            {
                core.Stop();
            }
        }

        private void ScrollOffButton_Click(object sender, EventArgs e)
        {
            LogRichTextBox.SelectionStart = 0;
            LogRichTextBox.SelectionLength = 1;
            WorkToogle.Focus();
        }

        private void ScrollOnButton_Click(object sender, EventArgs e)
        {
            LogRichTextBox.SelectionStart = LogRichTextBox.TextLength;
            LogRichTextBox.SelectionLength = 0;
            LogRichTextBox.Focus();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            LogUpdateTimer.Enabled = true;
            LogRichTextBox.AppendText(core.settings.currentLog.name+"\n", Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)));
        }

        private void GitHubBugsReport_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/levshx/logger-mc-server/issues/new");
        }
              
        public void DrawLine(LogLine logline)
        {
            switch (logline.type)
            {
                case (int)LogLine.LineType.GlobalChat:
                    LogRichTextBox.AppendText("[" + logline.datetime.ToString("HH:mm:ss") + "] ", Color.FromArgb(180, 180, 180));
                    LogRichTextBox.AppendText("[G] ", Color.FromArgb(0, 180, 255));
                    LogRichTextBox.AppendText(logline.nick + ": ", Color.FromArgb(180, 180, 180), true);                    
                    LogRichTextBox.AppendText(logline.text+ "\n", Color.FromArgb(0, 180, 255));
                    break;
                case (int)LogLine.LineType.LocalChat:
                    LogRichTextBox.AppendText("[" + logline.datetime.ToString("HH:mm:ss") + "] ", Color.FromArgb(180, 180, 180));
                    LogRichTextBox.AppendText("[L] ", Color.FromArgb(0, 255, 180));                    
                    LogRichTextBox.AppendText(logline.nick + ": ", Color.FromArgb(180, 180, 180), true);                    
                    LogRichTextBox.AppendText(logline.text + "\n", Color.FromArgb(0, 255, 180));
                    break;
                case (int)LogLine.LineType.Command:
                    LogRichTextBox.AppendText("[" + logline.datetime.ToString("HH:mm:ss") + "] ", Color.FromArgb(180, 180, 180));
                    LogRichTextBox.AppendText("[C] ", Color.FromArgb(180, 80, 0));                    
                    LogRichTextBox.AppendText(logline.nick + ": ", Color.FromArgb(180, 180, 180), true);                    
                    LogRichTextBox.AppendText(logline.text + "\n", Color.FromArgb(180, 80, 0));
                    break;
                case (int)LogLine.LineType.PlayerConnect:
                    LogRichTextBox.AppendText("[" + logline.datetime.ToString("HH:mm:ss") + "] ", Color.FromArgb(180, 180, 180));
                    LogRichTextBox.AppendText("[P] ", Color.FromArgb(139, 0, 255));
                    LogRichTextBox.AppendText(logline.nick + " зашёл\n", Color.FromArgb(139, 0, 255), true);                    
                    break;
                case (int)LogLine.LineType.PlayerDisconnect:
                    LogRichTextBox.AppendText("[" + logline.datetime.ToString("HH:mm:ss") + "] ", Color.FromArgb(180, 180, 180));
                    LogRichTextBox.AppendText("[P] ", Color.FromArgb(139, 0, 255));
                    LogRichTextBox.AppendText(logline.nick + " вышел\n", Color.FromArgb(139, 0, 255), true);
                    break;
                case (int)LogLine.LineType.Kick:
                    LogRichTextBox.AppendText("[" + logline.datetime.ToString("HH:mm:ss") + "] ", Color.FromArgb(180, 180, 180));
                    LogRichTextBox.AppendText("[P] ", Color.FromArgb(139, 0, 255));
                    LogRichTextBox.AppendText(logline.nick + " был кикнут\n", Color.Red, true);
                    break;
                case (int)LogLine.LineType.Death:
                    LogRichTextBox.AppendText("[" + logline.datetime.ToString("HH:mm:ss") + "] ", Color.FromArgb(180, 180, 180));
                    LogRichTextBox.AppendText("[P] ", Color.FromArgb(139, 0, 255));
                    LogRichTextBox.AppendText(logline.nick + " умер\n", Color.FromArgb(180, 180, 180), true);
                    break;
                case (int)LogLine.LineType.Kill:
                    LogRichTextBox.AppendText("[" + logline.datetime.ToString("HH:mm:ss") + "] ", Color.FromArgb(180, 180, 180));
                    LogRichTextBox.AppendText("[PvP] ", Color.Olive);
                    LogRichTextBox.AppendText(logline.nick + " убил " + logline.nick2+ "\n", Color.Olive, true);
                    break;
                case (int)LogLine.LineType.Message:
                    LogRichTextBox.AppendText("[" + logline.datetime.ToString("HH:mm:ss") + "] ", Color.FromArgb(180, 180, 180));
                    LogRichTextBox.AppendText("[M] ", Color.Orange);
                    LogRichTextBox.AppendText(logline.nick + " ✉→ " + logline.nick2 + ": ", Color.Orange, true);
                    LogRichTextBox.AppendText(logline.text + "\n", Color.OrangeRed);

                    break;
            }
        }

    }
}
