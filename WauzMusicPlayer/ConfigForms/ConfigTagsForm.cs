using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class ConfigTagsForm : Form, Themable
    {
        private MusicPlayerForm MusicPlayer { get; }

        private FileInfo FileInfo { get; }

        private SongTag SongTag { get; }

        public ConfigTagsForm(MusicPlayerForm musicPlayer, FileInfo fileInfo)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            this.MusicPlayer = musicPlayer;
            this.FileInfo = fileInfo;

            NameLabel.Text = fileInfo.Name;

            SongTag = new SongTag(fileInfo.FullName);
            TitleTextBox.Text = SongTag.Title;
            TrackTextBox.Text = SongTag.Track;
            AlbumTextBox.Text = SongTag.Album;
            ArtistTextBox.Text = SongTag.Artist;
            YearTextBox.Text = SongTag.Year;
            AlbumBox.Image = SongTag.Art ?? Properties.Resources.album_art;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.mainBackColorVariant1;
            TableLayoutPanel.BackColor = ThemeManager.mainBackColorVariant2;
            AlbumBox.BackColor = ThemeManager.mainBackColorVariant2;

            NameLabel.ForeColor = ThemeManager.highlightFontColor;
            label1.ForeColor = ThemeManager.highlightFontColor;
            label2.ForeColor = ThemeManager.highlightFontColor;
            label3.ForeColor = ThemeManager.highlightFontColor;
            label4.ForeColor = ThemeManager.highlightFontColor;
            label5.ForeColor = ThemeManager.highlightFontColor;
            label6.ForeColor = ThemeManager.highlightFontColor;
        }

        private void TrackTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void YearTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void AlbumBox_Click(object sender, EventArgs e)
        {
            UploadAlbumArtDialog.Filter = "Image Files(*.BMP; *.JPG; *.PNG; *.GIF)| *.BMP; *.JPG; *.PNG; *.GIF";
            if (UploadAlbumArtDialog.ShowDialog() == DialogResult.OK)
            {
                AlbumBox.Image = Image.FromFile(UploadAlbumArtDialog.FileName);
            }
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

                SongTag.Update(Title, Track, Album, Artist, Year, AlbumBox.Image);

                Invoke(new Action<string, string>(ShowMessage), "Tag Info:", 
                    "The tags of \"" + FileInfo.Name + "\" have successfully been updated! " +
                    "You may need to restart the player, to see the changes in all of your playlists.");

                Invoke(new Action(Close));
            }
            catch
            {
                Invoke(new Action<bool>(SetEnabled), true);
                Invoke(new Action<string, string>(ShowMessage), "Tag Error:",
                    "Updating of tags for \"" + FileInfo.Name + "\" has failed!");
            }
        }

        private void SetEnabled(bool enabled)
        {
            Enabled = enabled;
            UseWaitCursor = !enabled;
        }

        private void ShowMessage(string title, string message)
        {
            if (MusicPlayer.showSystemTrayInfo)
                MusicPlayer.SystemTray.ShowBalloonTip(1000, title, message, new ToolTipIcon());
            else
                MessageBox.Show(message);
        }
    }
}
