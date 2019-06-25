using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class NewStreamForm : Form, Themable
    {

        Regex allowedCharcters = new Regex("^[a-zA-Z0-9]*$");

        MusicPlayerForm musicPlayer;

        public NewStreamForm(MusicPlayerForm musicPlayer)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            this.musicPlayer = musicPlayer;

            InfoLabel.Text
                = "Tip: When a webradio offers different stream formats for different players,\n"
                + "you should always choose the .asx (Windows Media Player) format."
                ;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.mainBackColorVariant1;
            TableLayoutPanel.BackColor = ThemeManager.mainBackColorVariant2;

            NameLabel.ForeColor = ThemeManager.highlightFontColor;
            label1.ForeColor = ThemeManager.highlightFontColor;
            label2.ForeColor = ThemeManager.highlightFontColor;

            InfoLabel.ForeColor = ThemeManager.mainFontColor;
            InfoLabel.BackColor = ThemeManager.mainBackColorVariant1;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string streamName = NameTextBox.Text;
                string url = UrlTextBox.Text;

                if (String.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show("Please enter a url for your stream file!");
                    return;
                }
                if(!IsUrlValid(url))
                {
                    MessageBox.Show("The url of the stream file is not valid!");
                    return;
                }
                if (String.IsNullOrWhiteSpace(streamName))
                {
                    MessageBox.Show("Please enter a name for your stream file!");
                    return;
                }
                if (!allowedCharcters.IsMatch(streamName.Replace(" ", "")))
                {
                    MessageBox.Show("The name of the stream file must be alphanumeric!");
                    return;
                }

                musicPlayer.UpdateConfig(musicPlayer.streamPath + streamName + ".wzst", url);

                musicPlayer.BuildStreamTreeBranch();
                ShowMessage("Stream Info:", "Your new stream file was successfully created!");

                Close();
            }
            catch
            {
                ShowMessage("Stream Error:", "Creation of stream file failed!");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsUrlValid(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
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
