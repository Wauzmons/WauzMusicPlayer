using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class ConfigTagsForm : Form, Themable
    {

        MusicPlayerForm musicPlayer;

        FileInfo fileInfo;

        SongTag songTag;

        public ConfigTagsForm(MusicPlayerForm musicPlayer, FileInfo fileInfo)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            this.musicPlayer = musicPlayer;
            this.fileInfo = fileInfo;

            NameLabel.Text = fileInfo.Name;

            songTag = new SongTag(fileInfo.FullName);
            TitleTextBox.Text = songTag.Title;
            TrackTextBox.Text = songTag.Track;
            AlbumTextBox.Text = songTag.Album;
            ArtistTextBox.Text = songTag.Artist;
            YearTextBox.Text = songTag.Year;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.mainBackColorVariant1;
            TableLayoutPanel.BackColor = ThemeManager.mainBackColorVariant2;

            NameLabel.ForeColor = ThemeManager.highlightFontColor;
            label1.ForeColor = ThemeManager.highlightFontColor;
            label2.ForeColor = ThemeManager.highlightFontColor;
            label3.ForeColor = ThemeManager.highlightFontColor;
            label4.ForeColor = ThemeManager.highlightFontColor;
            label5.ForeColor = ThemeManager.highlightFontColor;
        }

        private void TrackTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void YearTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Task task = Task.Factory.StartNew(UpdateAndSaveTags);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateAndSaveTags()
        {
            try
            {
                Invoke(new Action<bool>(SetEnabled), false);

                string Title = string.IsNullOrWhiteSpace(TitleTextBox.Text)
                    ? null : TitleTextBox.Text;
                string Album = string.IsNullOrWhiteSpace(AlbumTextBox.Text)
                    ? null : AlbumTextBox.Text;
                string Artist = string.IsNullOrWhiteSpace(ArtistTextBox.Text)
                    ? null : ArtistTextBox.Text;

                uint? Track = null;
                if (!string.IsNullOrWhiteSpace(TrackTextBox.Text) && int.Parse(TrackTextBox.Text) >= 0)
                    Track = uint.Parse(TrackTextBox.Text);
                uint? Year = null;
                if (!string.IsNullOrWhiteSpace(YearTextBox.Text) && int.Parse(YearTextBox.Text) >= 0)
                    Year = uint.Parse(YearTextBox.Text);

                songTag.Update(Title, Track, Album, Artist, Year);

                Invoke(new Action<string, string>(ShowMessage), "Tag Info:", 
                    "The tags of \"" + fileInfo.Name + "\" have successfully been updated! " +
                    "You may need to restart the player, to see the changes in all of your playlists.");

                Invoke(new Action(Close));
            }
            catch
            {
                Invoke(new Action<bool>(SetEnabled), true);
                Invoke(new Action<string, string>(ShowMessage), "Tag Error:",
                    "Updating of tags for \"" + fileInfo.Name + "\" has failed!");
            }
        }

        private void SetEnabled(bool enabled)
        {
            Enabled = enabled;
            UseWaitCursor = !enabled;
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
