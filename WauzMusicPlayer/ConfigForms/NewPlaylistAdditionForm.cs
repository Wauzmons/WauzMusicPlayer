using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class NewPlaylistAdditionForm : Form, Themable
    {

        MusicPlayerForm musicPlayer;

        List<string> songPaths;

        public NewPlaylistAdditionForm(MusicPlayerForm musicPlayer, List<string> songPaths)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            this.musicPlayer = musicPlayer;
            this.songPaths = songPaths;

            foreach (string playlistName in musicPlayer.playlistNames.OrderBy(s => s).ToList())
                PlaylistInput.Items.Add(playlistName);

            foreach(string songPath in songPaths)
                SelectedFilePathsBox.AppendText(songPath + "\n");
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.mainBackColorVariant1;
            SelectedFilePathsBox.BackColor = ThemeManager.mainBackColorVariant2;
            SelectedFilePathsBox.ForeColor = ThemeManager.mainFontColor;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string playlistName = PlaylistInput.Text;

            if (String.IsNullOrWhiteSpace(playlistName) || !musicPlayer.playlistNames.Contains(playlistName))
            {
                MessageBox.Show("Please select a valid playlist first!");
                return;
            }

            musicPlayer.AddPlaylistSongs(playlistName, songPaths);
            this.Close();

            MessageBox.Show("The songs where successfully added to the playlist \"" + playlistName + "\"!");
        }

    }
}
