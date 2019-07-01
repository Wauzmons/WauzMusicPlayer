using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace WauzMusicPlayer
{
    public partial class YouTubeDownloadForm : Form, Themable
    {

        MusicPlayerForm musicPlayer;

        TreeNode treeNode;

        DirectoryInfo directoryInfo;

        YouTube youTube = YouTube.Default;

        public YouTubeDownloadForm(MusicPlayerForm musicPlayer, TreeNode treeNode, DirectoryInfo directoryInfo)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            this.musicPlayer = musicPlayer;
            this.treeNode = treeNode;
            this.directoryInfo = directoryInfo;

            NameLabel.Text = "Download Audio to: " + directoryInfo.Name;

            InfoLabel.Text
                = "Tip: You shall not exploit any Content for any other than personal purposes\n"
                + "without the prior written consent of YouTube or the licensors of the Content."
                ;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.mainBackColorVariant1;
            TableLayoutPanel.BackColor = ThemeManager.mainBackColorVariant2;

            NameLabel.ForeColor = ThemeManager.highlightFontColor;
            label1.ForeColor = ThemeManager.highlightFontColor;
            label2.ForeColor = ThemeManager.highlightFontColor;
            label3.ForeColor = ThemeManager.highlightFontColor;
            radioButton1.ForeColor = ThemeManager.mainFontColor;
            radioButton2.ForeColor = ThemeManager.mainFontColor;
            radioButton3.ForeColor = ThemeManager.mainFontColor;

            InfoLabel.ForeColor = ThemeManager.mainFontColor;
            InfoLabel.BackColor = ThemeManager.mainBackColorVariant1;
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            string format = ".mp3";

            if (radioButton1.Checked)
                format = radioButton1.Text;
            else if (radioButton2.Checked)
                format = radioButton2.Text;
            else if (radioButton3.Checked)
                format = radioButton3.Text;

            Task task = Task.Factory.StartNew(() => DownloadAndConvert(format));
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UrlTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                YouTubeVideo youTubeVideo = youTube.GetVideo(UrlTextBox.Text);
                string youTubeVideoName = youTubeVideo.Title;
                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    youTubeVideoName = youTubeVideoName.Replace(c, '_');
                }
                NameTextBox.Text = youTubeVideoName;
            }
            catch
            {
                NameTextBox.Text = "";
            }
        }

        private void DownloadAndConvert(string format)
        {
            try
            {
                Invoke(new Action<bool>(SetEnabled), false);

                YouTubeVideo youTubeVideo = youTube.GetVideo(UrlTextBox.Text);

                string youTubeVideoName = String.IsNullOrWhiteSpace(NameTextBox.Text)
                    ? youTubeVideo.Title : NameTextBox.Text;
                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    youTubeVideoName = youTubeVideoName.Replace(c, '_');
                }

                File.WriteAllBytes(musicPlayer.cachePath + youTubeVideoName + youTubeVideo.FileExtension, youTubeVideo.GetBytes());

                MediaFile inputFile = new MediaFile { Filename = musicPlayer.cachePath + youTubeVideoName + youTubeVideo.FileExtension };
                MediaFile outputFile = new MediaFile { Filename = $"{directoryInfo.FullName + "\\" + youTubeVideoName + format}" };

                using (Engine engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    engine.Convert(inputFile, outputFile);
                }

                if (File.Exists(inputFile.Filename))
                    File.Delete(inputFile.Filename);

                Invoke(new Action(ReloadTreeBranch));
                Invoke(new Action<string, string>(ShowMessage), "YouTube Converter Info:", "Your video was successfully converted!");

                Invoke(new Action(Close));
            }
            catch(Exception ex)
            {
                ex.ToString();
                Invoke(new Action<bool>(SetEnabled), true);
                Invoke(new Action<string, string>(ShowMessage), "YouTube-Converter Error:", "Conversion of video failed!");
            }
        }

        private void SetEnabled(bool enabled)
        {
            Enabled = enabled;
            UseWaitCursor = !enabled;
        }

        private void ReloadTreeBranch()
        {
            musicPlayer.BuildRecursiveFileExplorerTreeBranch(treeNode.Nodes, directoryInfo);
            treeNode.Expand();
        }

        private void ShowMessage(string title, string message)
        {
            if (musicPlayer.showSystemTrayInfo)
                musicPlayer.SystemTray.ShowBalloonTip(1000, title, message, new ToolTipIcon());
            else
                MessageBox.Show(message);
        }

    }
}
