using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class MusicPlayerForm : Form, Themable
    {
        private string[] validAudioFormats = {".mp3", ".wav", ".flac", ".m4a", ".wma", ".wzst"};

        public string configPath = Application.StartupPath + "\\Preferences\\";

        public string playlistPath = Application.StartupPath + "\\Playlists\\";

        public string themePath = Application.StartupPath + "\\Themes\\";

        public string cachePath = Application.StartupPath + "\\Cache\\";

        public string streamPath = Application.StartupPath + "\\Streams\\";

        public string musicFolderPath;

        public string themeName;

        private string fileTreeFilter = "";

        private ConfigHotkeysForm hotkeysForm;

        private SoundCapture soundCapture;

        private WMPLib.WindowsMediaPlayer mainPlayerWMP = new WMPLib.WindowsMediaPlayer();

        private List<QueuedSong> mainPlaylist = new List<QueuedSong>();

        private Dictionary<TreeNode, FileSystemInfo> fileExplorerMap = new Dictionary<TreeNode, FileSystemInfo>();

        private Dictionary<string, DataGridView> playlistViewMap = new Dictionary<string, DataGridView>();

        public Dictionary<string, FileInfo> serializedFileMap = new Dictionary<string, FileInfo>();

        public List<string> playlistNames = new List<string>();

        public bool removeAfterTrackEnded;

        public bool rememberQueue;

        public bool showSystemTrayInfo;



        public MusicPlayerForm()
        {
            InitializeComponent();
            GenerateAndLoadConfigs();
            UpdateThemeList();

            ThemeManager.SetTheme(this, themeName);
            ThemeManager.AddThemableForm(this);

            FileExplorerTree.NodeMouseClick += (sender, args) => SelectNode(args.Node);
            FileExplorerTree.NodeMouseDoubleClick += (sender, args) => AddSelectedToQueue(true);
        }

        public void ApplyTheme()
        {
            PictureBox.Image = ThemeManager.image;
            AlbumBox.BackColor = ThemeManager.mainBackColorVariant1;
            BackColor = ThemeManager.accentBackColor;

            SearchPanel.BackColor = ThemeManager.mainBackColorVariant1;
            SearchTextBox.BackColor = ThemeManager.mainBackColorVariant2;
            SearchTextBox.ForeColor = ThemeManager.mainFontColor;
            SearchLabel.ForeColor = ThemeManager.highlightFontColor;

            FileExplorerTree.ForeColor = ThemeManager.mainFontColor;
            FileExplorerTree.BackColor = ThemeManager.mainBackColorVariant1;
            FormMenuStrip.BackColor = ThemeManager.mainBackColorVariant1;
            MarqueePanel.BackColor = ThemeManager.mainBackColorVariant1;
            DurationPanel.BackColor = ThemeManager.mainBackColorVariant1;
            SoundCaptureChart.BackColor = ThemeManager.accentBackColor;
            SoundCaptureChart.ChartAreas[0].BackColor = ThemeManager.accentBackColor;
            SoundCaptureChart.Series[0].Color = ThemeManager.highlightFontColor;
            MainPlayerTrackBar.BackColor = ThemeManager.mainBackColorVariant1;
            MainPlayerAudioControl.BackColor = ThemeManager.mainBackColorVariant1;

            SongQueueTab.BackColor = ThemeManager.mainBackColorVariant1;
            SongQueueMenu.BackColor = ThemeManager.mainBackColorVariant2;
            SaveAsPlaylistToolStripMenuItem.ForeColor = ThemeManager.highlightFontColor;

            ScrollPanel.BackColor = ThemeManager.mainBackColorVariant1;
            QueuePanel.BackColor = ThemeManager.mainBackColorVariant1;
            PlaylistsTab.BackColor = ThemeManager.mainBackColorVariant1;
            AboutTab.BackColor = ThemeManager.mainBackColorVariant1;
            AboutTextBox.BackColor = ThemeManager.mainBackColorVariant1;
            AboutTextBox.ForeColor = ThemeManager.mainFontColor;

            ExpandAllToolStripMenuItem.ForeColor = ThemeManager.highlightFontColor;
            CollapseAllToolStripMenuItem.ForeColor = ThemeManager.highlightFontColor;
            MusicLibraryToolStripMenuItem.ForeColor = ThemeManager.highlightFontColor;
            PlaylistsToolStripMenuItem.ForeColor = ThemeManager.highlightFontColor;
            ConfigurationToolStripMenuItem.ForeColor = ThemeManager.highlightFontColor;
            ThemesToolStripMenuItem.ForeColor = ThemeManager.highlightFontColor;

            MarqueeLabel.ForeColor = ThemeManager.mainFontColor;
            DurationLabel.ForeColor = ThemeManager.mainFontColor;

            List<Image> iconList = new List<Image>();
            foreach(Image image in FileTreeIconList.Images)
                iconList.Add(ThemeManager.PaintImage(image, ThemeManager.mainFontColor));
            FileTreeIconList.Images.Clear();
            FileTreeIconList.Images.AddRange(iconList.ToArray());

            foreach (QueuedSong queuedSong in mainPlaylist)
                queuedSong.ApplyTheme();
        }

        private void CreateCustomThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NewThemeForm(this).Show();
        }

        public void UpdateThemeList()
        {
            customThemesToolStripMenuItem.DropDownItems.Clear();

            foreach(FileInfo fileInfo in new DirectoryInfo(themePath).GetFiles())
            {
                string newThemeName = fileInfo.Name.Replace(".wzmp", "");
                bool isActive = String.Equals(newThemeName, themeName);

                ToolStripMenuItem themeItem = new ToolStripMenuItem();
                themeItem.Text = newThemeName;
                themeItem.Checked = isActive;
                themeItem.Click += (sender, e) => ChangeTheme(newThemeName);

                ToolStripMenuItem switchItem = new ToolStripMenuItem();
                switchItem.Text = "Switch to Theme";
                switchItem.Click += (sender, e) => ChangeTheme(newThemeName);

                ToolStripMenuItem deleteItem = new ToolStripMenuItem();
                deleteItem.Text = isActive ? "Can't Delete Active Theme" : "Delete Theme";
                deleteItem.Enabled = !isActive;
                if(!isActive)
                    deleteItem.Click += (sender, e) => DeleteTheme(themeItem);

                themeItem.DropDownItems.Add(switchItem);
                themeItem.DropDownItems.Add(deleteItem);
                customThemesToolStripMenuItem.DropDownItems.Add(themeItem);
            }
        }

        private void DeleteTheme(ToolStripMenuItem themeItem)
        {
            customThemesToolStripMenuItem.DropDownItems.Remove(themeItem);
            File.Delete(themePath + themeItem.Text + ".wzmp");
        }

        private void Print(string input)
        {
            Console.WriteLine(input);
        }

        private void ShowMessage(string title, string message)
        {
            if (showSystemTrayInfo)
                SystemTray.ShowBalloonTip(1000, title, message, new ToolTipIcon());
            else
                MessageBox.Show(message);
        }

        private void MusicPlayerFormInit(object sender, EventArgs e)
        {
            Enabled = false;
            UseWaitCursor = true;

            LoadPlaylistTabs();
            LoadAboutTab();

            mainPlayerWMP.settings.volume = 50;
            mainPlayerWMP.PlayStateChange += (playState) => MainPlayerPlayStateChange(playState);

            StartMainPlayerControlLoop();

            hotkeysForm = new ConfigHotkeysForm(this);

            ResetMarqueePosition();
            MarqueeTimer.Start();

            InitSoundCaptureChart();
            SoundCaptureTimer.Start();

            BuildRecursiveFileExplorerTreeTrunk();

            if (rememberQueue)
                LoadSongQueue();

            ApplyTheme();

            Enabled = true;
            UseWaitCursor = false;
        }

        private void LoadAboutTab()
        {
            int textBoxIndex = 0;
            AboutTextBox.AppendText("WauzMusicPlayer v" + ProductVersion + "\n");
            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.SelectionColor = ThemeManager.highlightFontColor;
            AboutTextBox.SelectionFont = new Font(AboutTextBox.Font.FontFamily, 13, AboutTextBox.Font.Style | FontStyle.Bold);

            AboutTextBox.AppendText("\u00a9 Copyright Seven Ducks Studios 2018 - 2019   https://www.seven-ducks.com\n\n");
            AboutTextBox.AppendText("Supported Audio-Formats (Win10):\n" + String.Join(" ", validAudioFormats) + "\n\n\n");

            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.AppendText("vAlpha 0.7.0\n");
            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.SelectionColor = ThemeManager.highlightFontColor;
            AboutTextBox.SelectionFont = new Font(AboutTextBox.Font.FontFamily, 11, AboutTextBox.Font.Style | FontStyle.Bold);
            AboutTextBox.AppendText("+ Usability Changes (Doubleclick in tree to play etc.)\n");
            AboutTextBox.AppendText("+ Audio Recorder\n");
            AboutTextBox.AppendText("+ Custom Album Art Uploads\n");
            AboutTextBox.AppendText("+ Custom Theme Mascot Uploads\n");
            AboutTextBox.AppendText("+ Themes: Pure White & Deep Black\n\n");

            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.AppendText("vAlpha 0.6.0\n");
            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.SelectionColor = ThemeManager.highlightFontColor;
            AboutTextBox.SelectionFont = new Font(AboutTextBox.Font.FontFamily, 11, AboutTextBox.Font.Style | FontStyle.Bold);
            AboutTextBox.AppendText("+ Filetree Search Bar\n");
            AboutTextBox.AppendText("+ Album Art Display\n");
            AboutTextBox.AppendText("+ Radio Streams\n");
            AboutTextBox.AppendText("+ Theme Editor\n");
            AboutTextBox.AppendText("+ Theme: Stardust Crusaders\n\n");

            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.AppendText("vAlpha 0.5.0\n");
            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.SelectionColor = ThemeManager.highlightFontColor;
            AboutTextBox.SelectionFont = new Font(AboutTextBox.Font.FontFamily, 11, AboutTextBox.Font.Style | FontStyle.Bold);
            AboutTextBox.AppendText("+ YouTube-Converter\n");
            AboutTextBox.AppendText("+ Filetree View/Create/Delete Directories \n");
            AboutTextBox.AppendText("+ Remember Queue on Restart Option\n");
            AboutTextBox.AppendText("+ Theme: For the Horde\n");
            AboutTextBox.AppendText("+ About Tab\n\n");

            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.AppendText("vAlpha 0.4.0\n");
            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.SelectionColor = ThemeManager.highlightFontColor;
            AboutTextBox.SelectionFont = new Font(AboutTextBox.Font.FontFamily, 11, AboutTextBox.Font.Style | FontStyle.Bold);
            AboutTextBox.AppendText("+ Theme Switcher\n");
            AboutTextBox.AppendText("+ Theme: Feels Good Man\n");
            AboutTextBox.AppendText("+ Sound Visualizer\n");
            AboutTextBox.AppendText("+ Reworked Playlists Tab\n\n");

            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.AppendText("vAlpha 0.3.0\n");
            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.SelectionColor = ThemeManager.highlightFontColor;
            AboutTextBox.SelectionFont = new Font(AboutTextBox.Font.FontFamily, 11, AboutTextBox.Font.Style | FontStyle.Bold);
            AboutTextBox.AppendText("+ Global Configurable Hotkeys\n");
            AboutTextBox.AppendText("+ Tag Editor\n");
            AboutTextBox.AppendText("+ Windows Notifications\n\n");

            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.AppendText("vAlpha 0.2.0\n");
            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.SelectionColor = ThemeManager.highlightFontColor;
            AboutTextBox.SelectionFont = new Font(AboutTextBox.Font.FontFamily, 11, AboutTextBox.Font.Style | FontStyle.Bold);
            AboutTextBox.AppendText("+ Song List View\n");
            AboutTextBox.AppendText("+ Playlists\n");
            AboutTextBox.AppendText("+ Remove Finished Tracks Option\n\n");

            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.AppendText("vAlpha 0.1.0\n");
            textBoxIndex = AboutSelectionUpdate(textBoxIndex);
            AboutTextBox.SelectionColor = ThemeManager.highlightFontColor;
            AboutTextBox.SelectionFont = new Font(AboutTextBox.Font.FontFamily, 11, AboutTextBox.Font.Style | FontStyle.Bold);
            AboutTextBox.AppendText("+ Basic Music Player\n");
            AboutTextBox.AppendText("+ Basic Commands (Play/Pause/Next/Prev/Shuffle/Clear)\n");
            AboutTextBox.AppendText("+ Filetree + Recursive Songfinder\n");
            AboutTextBox.AppendText("+ Song Queue\n");
        }

        private int AboutSelectionUpdate(int textBoxIndex)
        {
            AboutTextBox.Select(textBoxIndex, AboutTextBox.Text.Length - textBoxIndex);
            return AboutTextBox.Text.Length;
        }

        private void MusicPlayerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SystemTray.Visible = false;

            if (rememberQueue)
                SaveSongQueue();

            Environment.Exit(0);
        }

        private void ExitWauzMusicPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
