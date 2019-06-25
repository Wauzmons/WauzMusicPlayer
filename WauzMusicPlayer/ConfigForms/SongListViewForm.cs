using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class SongListViewForm : Form, Themable
    {

        MusicPlayerForm musicPlayer;

        string playlistName;

        List<string> songPaths;

        public SongListViewForm(MusicPlayerForm musicPlayer, string playlistName, List<string> songPaths)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            this.musicPlayer = musicPlayer;
            this.playlistName = playlistName;
            this.songPaths = songPaths;

            if(String.IsNullOrWhiteSpace(playlistName))
            {
                toolStripSeparator1.Visible = false;
                showSonglistToolStripMenuItem.Visible = false;
                removeFromPlaylistToolStripMenuItem.Visible = false;
                deletePlaylistToolStripMenuItem.Visible = false;
            }

            CreateTable();

            DataGridView.CellMouseDown += (sender, args) => SelectSong(sender, args);
        }

        public void ApplyTheme()
        {
            DataGridView.BackgroundColor = ThemeManager.mainBackColorVariant1;

            DataGridView.RowsDefaultCellStyle.BackColor = ThemeManager.mainBackColorVariant1;
            DataGridView.RowsDefaultCellStyle.ForeColor = ThemeManager.mainFontColor;
            DataGridView.RowsDefaultCellStyle.SelectionBackColor = ThemeManager.highlightBackColor;
            DataGridView.RowsDefaultCellStyle.SelectionForeColor = ThemeManager.highlightFontColor;

            DataGridView.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.mainBackColorVariant2;
            DataGridView.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.mainFontColor;
            DataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = ThemeManager.highlightBackColor;
            DataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemeManager.highlightFontColor;
        }

        public void CreateTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Path");
            dataTable.Columns.Add("File");
            dataTable.Columns.Add("Title");
            dataTable.Columns.Add("#");
            dataTable.Columns.Add("Album");
            dataTable.Columns.Add("Artist");
            dataTable.Columns.Add("Year");
            dataTable.Columns.Add("Length");

            foreach(string songPath in songPaths)
            {
                if (String.IsNullOrWhiteSpace(songPath))
                    continue;

                FileInfo fileInfo = new FileInfo(songPath);
                SongTag songTag = new SongTag(songPath);

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


            DataGridView.DataSource = dataTable;
            DataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            DataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            DataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridView.Sort(DataGridView.Columns[1], ListSortDirection.Ascending);
            DataGridView.Columns[0].Visible = false;
        }

        public DataGridView GetDataGridView()
        {
            return DataGridView;
        }

        private void SelectSong(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if(e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridView.CurrentCell = DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    SongViewMenu.Show(DataGridView, DataGridView.PointToClient(MousePosition));
                }
                else if(!String.IsNullOrWhiteSpace(playlistName))
                {
                    PlaylistMenu.Show(DataGridView, DataGridView.PointToClient(MousePosition));
                }
            }
        }

        private void PlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = DataGridView.SelectedRows[0].Cells[0].Value.ToString();
            FileInfo fileInfo = new FileInfo(path);

            musicPlayer.AddToQueue(fileInfo, true);
        }

        private void AddToQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = DataGridView.SelectedRows[0].Cells[0].Value.ToString();
            FileInfo fileInfo = new FileInfo(path);

            musicPlayer.AddToQueue(fileInfo, false);
        }

        private void AddAllToQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in DataGridView.Rows)
            {
                FileInfo fileInfo = new FileInfo(row.Cells[0].Value.ToString());
                musicPlayer.AddToQueue(fileInfo, false);
            }
        }

        private void RemoveFromPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = DataGridView.SelectedRows[0].Cells[0].Value.ToString();
            DataGridView.Rows.Remove(DataGridView.SelectedRows[0]);
            musicPlayer.RemovePlaylistSong(playlistName, path);
        }

        private void DeletePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musicPlayer.RemovePlaylistTab(playlistName);
        }

        private void ShowTagEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = DataGridView.SelectedRows[0].Cells[0].Value.ToString();
            FileInfo fileInfo = new FileInfo(path);
            if(fileInfo.Exists)
            {
                ConfigTagsForm configTagsForm = new ConfigTagsForm(musicPlayer, fileInfo);
                configTagsForm.Show();
            }
        }

        private void ShowSonglistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            songPaths = new List<string>();
            foreach (DataGridViewRow row in DataGridView.Rows)
                songPaths.Add(row.Cells[0].Value.ToString());

            SongListViewForm songListViewForm = new SongListViewForm(musicPlayer, null, songPaths);
            songListViewForm.Show();
        }
    }
}
