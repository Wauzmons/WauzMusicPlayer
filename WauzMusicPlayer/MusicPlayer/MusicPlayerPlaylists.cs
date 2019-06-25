using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class MusicPlayerForm : Form
    {

        private void AddQueueToNewPlaylist()
        {
            List<string> songPaths = new List<string>();
            foreach(QueuedSong queuedSong in mainPlaylist)
            {
                if((!queuedSong.fileInfo.Extension.Equals(".wzst")))
                    songPaths.Add(queuedSong.GetSongPath());
            }
            if (songPaths.Count == 0)
            {
                MessageBox.Show("No songs were found!");
                return;
            }
            NewPlaylistForm newPlaylistForm = new NewPlaylistForm(this, songPaths, "");
            newPlaylistForm.Show();
        }

        private void AddSongsToNewPlaylist(DirectoryInfo parentDirectoryInfo, bool recursive)
        {
            List<string> songPaths = GetSongPaths(parentDirectoryInfo, recursive);
            if (songPaths.Count == 0)
            {
                MessageBox.Show("No songs were found!");
                return;
            }
            NewPlaylistForm newPlaylistForm = new NewPlaylistForm(this, songPaths, parentDirectoryInfo.Name);
            newPlaylistForm.Show();
        }

        private void AddSongsToNewSonglistView(DirectoryInfo parentDirectoryInfo, bool recursive)
        {
            List<string> songPaths = GetSongPaths(parentDirectoryInfo, recursive);
            if (songPaths.Count == 0)
            {
                MessageBox.Show("No songs were found!");
                return;
            }
            SongListViewForm songListViewForm = new SongListViewForm(this, null, songPaths);
            songListViewForm.Show();
        }

        private List<string> GetSongPaths(DirectoryInfo parentDirectoryInfo, bool recursive)
        {
            List<string> songPaths = new List<string>();
            foreach (FileInfo fileInfo in parentDirectoryInfo.GetFiles())
                if (validAudioFormats.Contains(fileInfo.Extension))
                    songPaths.Add(fileInfo.FullName);

            if (recursive)
                foreach (DirectoryInfo directoryInfo in parentDirectoryInfo.GetDirectories())
                    songPaths.AddRange(GetSongPaths(directoryInfo, true));

            return songPaths;
        }

        private void LoadPlaylistTabs()
        {
            PlaylistTabControl.Controls.Clear();

            DirectoryInfo playlistDirectory = new DirectoryInfo(playlistPath);
            foreach (FileInfo fileInfo in playlistDirectory.GetFiles())
            {
                string fileContent = "";
                using (StreamReader streamReader = new StreamReader(fileInfo.FullName))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();

                        if (!String.IsNullOrEmpty(line.Replace(" ", "")) && File.Exists(line))
                            fileContent += (String.IsNullOrEmpty(fileContent.Replace(" ", "")) ? "" : "|") + line;
                    }
                }
                string[] songPaths = fileContent.Split('|');
                if (songPaths.Count() > 0)
                    AddPlaylistTab(fileInfo.Name.Replace(".wzmp", ""), new List<string>(songPaths));
            }

            //Belastungs - und Scrolltest
            //Random random = new Random();
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //for (int i = 0; i != 60; i++)
            //{
            //    string tabName = new string(Enumerable.Repeat(chars, random.Next(9) + 3)
            //        .Select(s => s[random.Next(s.Length)]).ToArray());
            //    AddPlaylistTab("Tab " + tabName, new List<string>());
            //}

            PlaylistTabControl.Visible = PlaylistTabControl.TabCount > 0;
            SizePlaylistTabs();
        }

        private void PlaylistTabControl_Selected(object sender, TabControlEventArgs e)
        {
            SizePlaylistTabs();
        }

        private void PlaylistTabControl_Resize(object sender, EventArgs e)
        {
            SizePlaylistTabs();
        }

        public void AddPlaylistTab(string playlistName, List<string> songPaths)
        {
            TabPage tabPage = new TabPage
            {
                BackColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Text = playlistName
            };
            DataGridView dataGridView = new SongListViewForm(this, playlistName, songPaths).GetDataGridView();
            tabPage.Controls.Add(dataGridView);

            playlistNames.Add(playlistName);
            playlistViewMap.Add(playlistName, dataGridView);

            PlaylistTabControl.Controls.Add(tabPage);

            PlaylistTabControl.Visible = true;
            SizePlaylistTabs();
        }

        public void SortPlaylistTabs()
        {
            List<TabPage> tabs = new List<TabPage>();
            foreach (TabPage tabPage in PlaylistTabControl.TabPages)
                tabs.Add(tabPage);
            PlaylistTabControl.Controls.Clear();
            PlaylistTabControl.Controls.AddRange(tabs.OrderBy(tab => tab.Text).ToArray());
        }

        public void SizePlaylistTabs()
        {
            if (PlaylistTabControl.TabCount < 1)
                return;

            DataGridView dataGridView = playlistViewMap[PlaylistTabControl.SelectedTab.Text];
            int currentTabSize = (dataGridView.RowCount + 2) * dataGridView.RowTemplate.Height;
            int headerSize = PlaylistTabControl.RowCount * (PlaylistTabControl.ItemSize.Height + PlaylistTabControl.Padding.Y);

            PlaylistTabControl.Height = currentTabSize + headerSize + PlaylistTabControl.Padding.Y;
        }

        public void RemovePlaylistTab(string playlistName)
        {
            DialogResult confirmResult = MessageBox.Show(
                "Do you really want to delete the playlist \"" + playlistName + "\"?",
                "Confirm", MessageBoxButtons.YesNo);

            if (confirmResult != DialogResult.Yes)
                return;

            foreach (TabPage tabPage in PlaylistTabControl.Controls)
            {
                if (tabPage.Text.Equals(playlistName))
                    PlaylistTabControl.Controls.Remove(tabPage);
            }

            playlistNames.Remove(playlistName);
            playlistViewMap.Remove(playlistName);

            File.Delete(playlistPath + playlistName + ".wzmp");
            PlaylistTabControl.Visible = PlaylistTabControl.TabCount > 0;

            ShowMessage("Playlist Info:", "The playlist \"" + playlistName + "\" was successfully deleted!");
        }

        public void RemovePlaylistSong(string playlistName, string songPath)
        {
            string[] lines = File.ReadAllLines(playlistPath + playlistName + ".wzmp").Where(line => line.Trim() != songPath).ToArray();
            File.WriteAllLines(playlistPath + playlistName + ".wzmp", lines);
            SizePlaylistTabs();
        }

        public void AddPlaylistSongs(string playlistName, List<string> songPaths)
        {
            string totalPath = playlistPath + playlistName + ".wzmp";
            string selectedSongs = File.ReadAllText(totalPath);
            string[] addedSongs = songPaths.Where(path => !selectedSongs.Contains(path)).Select(path => path + "\n").ToArray();

            bool append = !String.IsNullOrWhiteSpace(ReadConfig(totalPath).Replace("\n", ""));
            using (StreamWriter streamWriter = new StreamWriter(totalPath, append))
            {
                streamWriter.WriteLine(String.Join("", addedSongs));
            }

            DataTable dataTable = (DataTable)playlistViewMap[playlistName].DataSource;
            foreach (string songPath in addedSongs)
            {
                FileInfo fileInfo = new FileInfo(songPath.Replace("\n", ""));
                SongTag songTag = new SongTag(songPath.Replace("\n", ""));

                dataTable.Rows.Add(
                    fileInfo.FullName,
                    fileInfo.Name,
                    songTag.Title,
                    songTag.Track,
                    songTag.Album,
                    songTag.Artist,
                    songTag.Year,
                    songTag.Length);
            }

            playlistViewMap[playlistName].Sort(playlistViewMap[playlistName].Columns[1], ListSortDirection.Ascending);
            playlistViewMap[playlistName].Columns[0].Visible = false;
            SizePlaylistTabs();
        }

        private void AddPlaylistSongs(List<string> songPaths)
        {
            if (playlistNames.Count == 0)
            {
                MessageBox.Show("You have no playlists yet!");
                return;
            }
            if (songPaths.Count == 0)
            {
                MessageBox.Show("No songs were found!");
                return;
            }
            NewPlaylistAdditionForm newPlaylistAdditionForm = new NewPlaylistAdditionForm(this, songPaths);
            newPlaylistAdditionForm.Show();
        }

    }
}
