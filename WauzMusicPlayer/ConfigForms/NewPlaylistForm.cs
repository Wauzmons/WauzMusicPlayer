using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class NewPlaylistForm : Form, Themable
    {

        Regex allowedCharcters = new Regex("^[a-zA-Z0-9]*$");

        MusicPlayerForm musicPlayer;

        List<string> songPaths;

        public NewPlaylistForm(MusicPlayerForm musicPlayer, List<string> songPaths, string playlistName)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            this.musicPlayer = musicPlayer;
            this.songPaths = songPaths;
            PlaylistNameInput.Text = Regex.Replace(playlistName, "^[a-zA-Z0-9]*$", "");

            foreach(string songPath in songPaths)
                SelectedFilePathsBox.AppendText(songPath + (songPaths.Last().Equals(songPath) ? "" : "\n"));
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.mainBackColorVariant1;
            SelectedFilePathsBox.BackColor = ThemeManager.mainBackColorVariant2;
            SelectedFilePathsBox.ForeColor = ThemeManager.mainFontColor;
        }

        private void PlaylistNameInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                CreateButton_Click(sender, e);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            string playlistName = PlaylistNameInput.Text;

            if (String.IsNullOrWhiteSpace(playlistName))
            {
                MessageBox.Show("Please enter a name for your playlist!");
                return;
            }
            if(!allowedCharcters.IsMatch(playlistName.Replace(" ", "")))
            {
                MessageBox.Show("The name of the playlist must be alphanumeric!");
                return;
            }
            if(musicPlayer.playlistNames.Contains(playlistName))
            {
                MessageBox.Show("A playlist with this name already exists!");
                return;
            }

            string playlistFilePath = musicPlayer.playlistPath + playlistName + ".wzmp";
            using (StreamWriter streamWriter = new StreamWriter(playlistFilePath, false))
            {
                streamWriter.WriteLine(SelectedFilePathsBox.Text);
            }

            musicPlayer.AddPlaylistTab(playlistName, songPaths);
            musicPlayer.SortPlaylistTabs();
            musicPlayer.SizePlaylistTabs();
            this.Close();

            ShowMessage("Playlist Info:", "The playlist \"" + playlistName + "\" was successfully created!");
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
