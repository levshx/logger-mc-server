
namespace Logger
{
    partial class FormGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGUI));
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.LogPage = new System.Windows.Forms.TabPage();
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.ScrollOnButton = new MetroFramework.Controls.MetroButton();
            this.ScrollOffButton = new MetroFramework.Controls.MetroButton();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.PlayerDropButton = new MetroFramework.Controls.MetroButton();
            this.PlayerDropTextBox = new MetroFramework.Controls.MetroTextBox();
            this.PlayerDropLabel = new MetroFramework.Controls.MetroLabel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.PlayerDeathButton = new MetroFramework.Controls.MetroButton();
            this.PlayerDeathTextBox = new MetroFramework.Controls.MetroTextBox();
            this.CheckDeathLabel = new MetroFramework.Controls.MetroLabel();
            this.AllLogsPage = new System.Windows.Forms.TabPage();
            this.LogsListBox = new System.Windows.Forms.ListBox();
            this.SettingsPage = new System.Windows.Forms.TabPage();
            this.versionLabel = new MetroFramework.Controls.MetroLabel();
            this.LogUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.WorkToogle = new MetroFramework.Controls.MetroToggle();
            this.GitHubBugsReport = new MetroFramework.Controls.MetroButton();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.metroTabControl1.SuspendLayout();
            this.LogPage.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.AllLogsPage.SuspendLayout();
            this.TrayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            this.metroStyleManager.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.LogPage);
            this.metroTabControl1.Controls.Add(this.AllLogsPage);
            this.metroTabControl1.Controls.Add(this.SettingsPage);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(20, 74);
            this.metroTabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 2;
            this.metroTabControl1.Size = new System.Drawing.Size(969, 593);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // LogPage
            // 
            this.LogPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.LogPage.Controls.Add(this.LogRichTextBox);
            this.LogPage.Controls.Add(this.metroPanel1);
            this.LogPage.Location = new System.Drawing.Point(4, 38);
            this.LogPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LogPage.Name = "LogPage";
            this.LogPage.Size = new System.Drawing.Size(961, 551);
            this.LogPage.TabIndex = 0;
            this.LogPage.Text = "Log";
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.LogRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LogRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.ReadOnly = true;
            this.LogRichTextBox.Size = new System.Drawing.Size(755, 551);
            this.LogRichTextBox.TabIndex = 2;
            this.LogRichTextBox.Text = "";
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroButton1);
            this.metroPanel1.Controls.Add(this.ScrollOnButton);
            this.metroPanel1.Controls.Add(this.ScrollOffButton);
            this.metroPanel1.Controls.Add(this.metroPanel3);
            this.metroPanel1.Controls.Add(this.metroPanel2);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(755, 0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(206, 551);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(51, 347);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(90, 53);
            this.metroButton1.TabIndex = 6;
            this.metroButton1.Text = "metroButton1";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // ScrollOnButton
            // 
            this.ScrollOnButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ScrollOnButton.BackgroundImage")));
            this.ScrollOnButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ScrollOnButton.Cursor = System.Windows.Forms.Cursors.PanSouth;
            this.ScrollOnButton.Location = new System.Drawing.Point(3, 228);
            this.ScrollOnButton.Name = "ScrollOnButton";
            this.ScrollOnButton.Size = new System.Drawing.Size(200, 49);
            this.ScrollOnButton.TabIndex = 5;
            this.ScrollOnButton.Text = "Auto scrolling allow";
            this.ScrollOnButton.UseSelectable = true;
            this.ScrollOnButton.Click += new System.EventHandler(this.ScrollOnButton_Click);
            // 
            // ScrollOffButton
            // 
            this.ScrollOffButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ScrollOffButton.BackgroundImage")));
            this.ScrollOffButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ScrollOffButton.Cursor = System.Windows.Forms.Cursors.NoMoveVert;
            this.ScrollOffButton.Location = new System.Drawing.Point(3, 173);
            this.ScrollOffButton.Name = "ScrollOffButton";
            this.ScrollOffButton.Size = new System.Drawing.Size(200, 49);
            this.ScrollOffButton.TabIndex = 4;
            this.ScrollOffButton.Text = "Auto scrolling disable";
            this.ScrollOffButton.UseSelectable = true;
            this.ScrollOffButton.Click += new System.EventHandler(this.ScrollOffButton_Click);
            // 
            // metroPanel3
            // 
            this.metroPanel3.Controls.Add(this.PlayerDropButton);
            this.metroPanel3.Controls.Add(this.PlayerDropTextBox);
            this.metroPanel3.Controls.Add(this.PlayerDropLabel);
            this.metroPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(0, 82);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(206, 85);
            this.metroPanel3.TabIndex = 3;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // PlayerDropButton
            // 
            this.PlayerDropButton.Location = new System.Drawing.Point(3, 56);
            this.PlayerDropButton.Name = "PlayerDropButton";
            this.PlayerDropButton.Size = new System.Drawing.Size(200, 23);
            this.PlayerDropButton.TabIndex = 5;
            this.PlayerDropButton.Text = "Find";
            this.PlayerDropButton.UseSelectable = true;
            // 
            // PlayerDropTextBox
            // 
            // 
            // 
            // 
            this.PlayerDropTextBox.CustomButton.Image = null;
            this.PlayerDropTextBox.CustomButton.Location = new System.Drawing.Point(178, 2);
            this.PlayerDropTextBox.CustomButton.Name = "";
            this.PlayerDropTextBox.CustomButton.Size = new System.Drawing.Size(19, 19);
            this.PlayerDropTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.PlayerDropTextBox.CustomButton.TabIndex = 1;
            this.PlayerDropTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.PlayerDropTextBox.CustomButton.UseSelectable = true;
            this.PlayerDropTextBox.CustomButton.Visible = false;
            this.PlayerDropTextBox.Lines = new string[0];
            this.PlayerDropTextBox.Location = new System.Drawing.Point(3, 26);
            this.PlayerDropTextBox.MaxLength = 32767;
            this.PlayerDropTextBox.Name = "PlayerDropTextBox";
            this.PlayerDropTextBox.PasswordChar = '\0';
            this.PlayerDropTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.PlayerDropTextBox.SelectedText = "";
            this.PlayerDropTextBox.SelectionLength = 0;
            this.PlayerDropTextBox.SelectionStart = 0;
            this.PlayerDropTextBox.ShortcutsEnabled = true;
            this.PlayerDropTextBox.Size = new System.Drawing.Size(200, 24);
            this.PlayerDropTextBox.TabIndex = 4;
            this.PlayerDropTextBox.UseSelectable = true;
            this.PlayerDropTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.PlayerDropTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // PlayerDropLabel
            // 
            this.PlayerDropLabel.AutoSize = true;
            this.PlayerDropLabel.Location = new System.Drawing.Point(0, 3);
            this.PlayerDropLabel.Name = "PlayerDropLabel";
            this.PlayerDropLabel.Size = new System.Drawing.Size(91, 20);
            this.PlayerDropLabel.TabIndex = 3;
            this.PlayerDropLabel.Text = "Player Drops:";
            // 
            // metroPanel2
            // 
            this.metroPanel2.Controls.Add(this.PlayerDeathButton);
            this.metroPanel2.Controls.Add(this.PlayerDeathTextBox);
            this.metroPanel2.Controls.Add(this.CheckDeathLabel);
            this.metroPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(0, 0);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(206, 82);
            this.metroPanel2.TabIndex = 2;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // PlayerDeathButton
            // 
            this.PlayerDeathButton.Location = new System.Drawing.Point(3, 53);
            this.PlayerDeathButton.Name = "PlayerDeathButton";
            this.PlayerDeathButton.Size = new System.Drawing.Size(200, 23);
            this.PlayerDeathButton.TabIndex = 4;
            this.PlayerDeathButton.Text = "Find";
            this.PlayerDeathButton.UseSelectable = true;
            // 
            // PlayerDeathTextBox
            // 
            // 
            // 
            // 
            this.PlayerDeathTextBox.CustomButton.Image = null;
            this.PlayerDeathTextBox.CustomButton.Location = new System.Drawing.Point(178, 2);
            this.PlayerDeathTextBox.CustomButton.Name = "";
            this.PlayerDeathTextBox.CustomButton.Size = new System.Drawing.Size(19, 19);
            this.PlayerDeathTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.PlayerDeathTextBox.CustomButton.TabIndex = 1;
            this.PlayerDeathTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.PlayerDeathTextBox.CustomButton.UseSelectable = true;
            this.PlayerDeathTextBox.CustomButton.Visible = false;
            this.PlayerDeathTextBox.Lines = new string[0];
            this.PlayerDeathTextBox.Location = new System.Drawing.Point(3, 23);
            this.PlayerDeathTextBox.MaxLength = 32767;
            this.PlayerDeathTextBox.Name = "PlayerDeathTextBox";
            this.PlayerDeathTextBox.PasswordChar = '\0';
            this.PlayerDeathTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.PlayerDeathTextBox.SelectedText = "";
            this.PlayerDeathTextBox.SelectionLength = 0;
            this.PlayerDeathTextBox.SelectionStart = 0;
            this.PlayerDeathTextBox.ShortcutsEnabled = true;
            this.PlayerDeathTextBox.Size = new System.Drawing.Size(200, 24);
            this.PlayerDeathTextBox.TabIndex = 3;
            this.PlayerDeathTextBox.UseSelectable = true;
            this.PlayerDeathTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.PlayerDeathTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // CheckDeathLabel
            // 
            this.CheckDeathLabel.AutoSize = true;
            this.CheckDeathLabel.Location = new System.Drawing.Point(0, 0);
            this.CheckDeathLabel.Name = "CheckDeathLabel";
            this.CheckDeathLabel.Size = new System.Drawing.Size(97, 20);
            this.CheckDeathLabel.TabIndex = 2;
            this.CheckDeathLabel.Text = "Player Deaths:";
            // 
            // AllLogsPage
            // 
            this.AllLogsPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.AllLogsPage.Controls.Add(this.LogsListBox);
            this.AllLogsPage.Location = new System.Drawing.Point(4, 38);
            this.AllLogsPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AllLogsPage.Name = "AllLogsPage";
            this.AllLogsPage.Size = new System.Drawing.Size(961, 551);
            this.AllLogsPage.TabIndex = 1;
            this.AllLogsPage.Text = "All Logs";
            // 
            // LogsListBox
            // 
            this.LogsListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.LogsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LogsListBox.ForeColor = System.Drawing.Color.OldLace;
            this.LogsListBox.FormattingEnabled = true;
            this.LogsListBox.ItemHeight = 25;
            this.LogsListBox.Location = new System.Drawing.Point(0, 0);
            this.LogsListBox.Name = "LogsListBox";
            this.LogsListBox.Size = new System.Drawing.Size(961, 551);
            this.LogsListBox.TabIndex = 0;
            this.LogsListBox.DoubleClick += new System.EventHandler(this.LogsListBox_DoubleClick);
            // 
            // SettingsPage
            // 
            this.SettingsPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.SettingsPage.Location = new System.Drawing.Point(4, 38);
            this.SettingsPage.Name = "SettingsPage";
            this.SettingsPage.Size = new System.Drawing.Size(961, 551);
            this.SettingsPage.TabIndex = 2;
            this.SettingsPage.Text = "Settings";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.versionLabel.Location = new System.Drawing.Point(898, 74);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(91, 20);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "version x.x.x.x";
            // 
            // LogUpdateTimer
            // 
            this.LogUpdateTimer.Enabled = true;
            this.LogUpdateTimer.Interval = 500;
            this.LogUpdateTimer.Tick += new System.EventHandler(this.LogUpdateTimer_Tick);
            // 
            // WorkToogle
            // 
            this.WorkToogle.AutoSize = true;
            this.WorkToogle.Checked = true;
            this.WorkToogle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WorkToogle.Location = new System.Drawing.Point(143, 39);
            this.WorkToogle.Name = "WorkToogle";
            this.WorkToogle.Size = new System.Drawing.Size(80, 21);
            this.WorkToogle.TabIndex = 2;
            this.WorkToogle.Text = "On";
            this.WorkToogle.UseSelectable = true;
            this.WorkToogle.CheckedChanged += new System.EventHandler(this.WorkToogle_CheckedChanged);
            // 
            // GitHubBugsReport
            // 
            this.GitHubBugsReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GitHubBugsReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GitHubBugsReport.BackgroundImage")));
            this.GitHubBugsReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.GitHubBugsReport.ForeColor = System.Drawing.SystemColors.Desktop;
            this.GitHubBugsReport.Location = new System.Drawing.Point(763, 40);
            this.GitHubBugsReport.Name = "GitHubBugsReport";
            this.GitHubBugsReport.Size = new System.Drawing.Size(226, 31);
            this.GitHubBugsReport.TabIndex = 3;
            this.GitHubBugsReport.UseSelectable = true;
            this.GitHubBugsReport.Click += new System.EventHandler(this.GitHubBugsReport_Click);
            // 
            // TrayIcon
            // 
            this.TrayIcon.ContextMenuStrip = this.TrayContextMenu;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Logger";
            this.TrayIcon.Visible = true;
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.TrayContextMenu.Name = "TrayContextMenu";
            this.TrayContextMenu.Size = new System.Drawing.Size(103, 28);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(102, 24);
            this.toolStripMenuItem1.Text = "Kek";
            // 
            // FormGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 687);
            this.Controls.Add(this.GitHubBugsReport);
            this.Controls.Add(this.WorkToogle);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.metroTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormGUI";
            this.Padding = new System.Windows.Forms.Padding(20, 74, 20, 20);
            this.Text = "Logger";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGUI_FormClosing);
            this.Load += new System.EventHandler(this.FormGUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.metroTabControl1.ResumeLayout(false);
            this.LogPage.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel3.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.AllLogsPage.ResumeLayout(false);
            this.TrayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager metroStyleManager;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.TabPage LogPage;
        private System.Windows.Forms.TabPage AllLogsPage;
        private MetroFramework.Controls.MetroLabel versionLabel;
        private System.Windows.Forms.ListBox LogsListBox;
        private System.Windows.Forms.Timer LogUpdateTimer;
        private MetroFramework.Controls.MetroToggle WorkToogle;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private MetroFramework.Controls.MetroButton PlayerDropButton;
        private MetroFramework.Controls.MetroTextBox PlayerDropTextBox;
        private MetroFramework.Controls.MetroLabel PlayerDropLabel;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroButton PlayerDeathButton;
        private MetroFramework.Controls.MetroTextBox PlayerDeathTextBox;
        private MetroFramework.Controls.MetroLabel CheckDeathLabel;
        private System.Windows.Forms.RichTextBox LogRichTextBox;
        private MetroFramework.Controls.MetroButton ScrollOffButton;
        private MetroFramework.Controls.MetroButton ScrollOnButton;
        private System.Windows.Forms.TabPage SettingsPage;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroButton GitHubBugsReport;
        private MetroFramework.Controls.MetroContextMenu TrayContextMenu;
        public System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}