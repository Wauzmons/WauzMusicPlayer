using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class MusicPlayerForm : Form
    {

        private void FileNodeMenuItemPlay_Click(object sender, EventArgs e)
        {
            AddSelectedToQueue(true);
        }

        private void FileNodeMenuItemAddToQueue_Click(object sender, EventArgs e)
        {
            AddSelectedToQueue(false);
        }

        private void AddAllToQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            AddAllToQueue(directoryInfo, false);
        }

        private void AddAllToQueueRecursivelyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            AddAllToQueue(directoryInfo, true);
        }

        private void SaveAsPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddQueueToNewPlaylist();
        }

        private void CreateEmptyPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPlaylistForm newPlaylistForm = new NewPlaylistForm(this, new List<string>(), "");
            newPlaylistForm.Show();
        }

        private void CreatePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            AddSongsToNewPlaylist(directoryInfo, false);
        }

        private void CreatePlaylistRecursivelyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            AddSongsToNewPlaylist(directoryInfo, true);
        }

        private void AddToPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fileInfo = (FileInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            AddPlaylistSongs(new List<string> { fileInfo.FullName });
        }

        private void AddAllToPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            AddPlaylistSongs(GetSongPaths(directoryInfo, false));
        }

        private void AddAllToPlaylistRecursivelyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            AddPlaylistSongs(GetSongPaths(directoryInfo, true));
        }

        private void ShowSonglistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            AddSongsToNewSonglistView(directoryInfo, false);
        }

        private void ShowSonglistRecursivelyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            AddSongsToNewSonglistView(directoryInfo, true);
        }

        private void ShowTagEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fileInfo = (FileInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            if (fileInfo.Exists)
            {
                ConfigTagsForm configTagsForm = new ConfigTagsForm(this, fileInfo);
                configTagsForm.Show();
            }
        }

        private void CreateMp3FromYouTubeURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            new YouTubeDownloadForm(this, FileExplorerTree.SelectedNode, directoryInfo).Show();
        }

        private void RecordAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            new AudioRecorderForm(this, FileExplorerTree.SelectedNode, directoryInfo).Show();
        }

        private void CreateRadioStreamFromURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NewStreamForm(this).Show();
        }

        private void DirectoryNodeMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            CreateFolderTextBox.Text = "New Folder";
        }

        private void CreateFolderTextBox_Click(object sender, EventArgs e)
        {
            if (CreateFolderTextBox.Text == "New Folder")
            {
                CreateFolderTextBox.SelectionStart = 0;
                CreateFolderTextBox.SelectionLength = CreateFolderTextBox.Text.Length;
            }
        }

        private void CreateFolderTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            string folderName = "" + CreateFolderTextBox.Text;
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                folderName = folderName.Replace(c, '_');
            }

            if (!string.IsNullOrWhiteSpace(folderName))
            {
                try
                {
                    DirectoryInfo newDirectoryInfo = new DirectoryInfo(directoryInfo.FullName + "\\" + folderName);
                    if (!newDirectoryInfo.Exists)
                        Directory.CreateDirectory(newDirectoryInfo.FullName);

                    BuildRecursiveFileExplorerTreeBranch(FileExplorerTree.SelectedNode.Nodes, directoryInfo);
                    FileExplorerTree.SelectedNode.Expand();

                    ShowMessage("Directory Info:", "The folder \"" + folderName + "\" was successfully created!");
                }
                catch
                {
                    ShowMessage("Directory Error:", "Creation of folder failed!");
                }
            }

            CreateFolderTextBox.Text = "New Folder";
            DirectoryNodeMenu.Close();
        }

        private void DeleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            DialogResult confirmResult = MessageBox.Show(
                "Do you really want to delete \"" + directoryInfo.Name + "\" and all its contents?",
                "Confirm", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    if (!DeleteFileSystemInfo(directoryInfo, true))
                        throw new Exception("Deletion of folder failed!");

                    FileExplorerTree.SelectedNode.Remove();

                    ShowMessage("Directory Info:", "The folder \"" + directoryInfo.Name + "\" was successfully deleted!");
                }
                catch
                {
                    ShowMessage("Directory Error:", "Deletion of folder failed!");
                }
            }
        }

        private void DeleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fileInfo = (FileInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            DialogResult confirmResult = MessageBox.Show(
                "Do you really want to delete \"" + fileInfo.Name + "\"?",
                "Confirm", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    if (!DeleteFileSystemInfo(fileInfo, true))
                        throw new Exception("Deletion of file failed!");

                    FileExplorerTree.SelectedNode.Remove();

                    ShowMessage("File Info:", "The file \"" + fileInfo.Name + "\" was successfully deleted!");
                }
                catch
                {
                    ShowMessage("File Error:", "Deletion of file failed!");
                }
            }
        }

        private static bool DeleteFileSystemInfo(FileSystemInfo fileSystemInfo, bool root)
        {
            if (fileSystemInfo is DirectoryInfo)
            {
                DirectoryInfo directoryInfo = (DirectoryInfo)fileSystemInfo;
                foreach (FileSystemInfo childFileSystemInfo in directoryInfo.GetFileSystemInfos())
                    if (!DeleteFileSystemInfo(childFileSystemInfo, false))
                        return false;
            }

            try
            {
                if (fileSystemInfo is FileInfo)
                {
                    FileInfo fileInfo = (FileInfo)fileSystemInfo;
                    FileStream fs = new FileStream(fileSystemInfo.FullName, FileMode.Open, FileAccess.ReadWrite);
                    fs.Close();

                    if (root)
                        fileInfo.Delete();
                }
                else
                {
                    if (root)
                        ((DirectoryInfo)fileSystemInfo).Delete(true);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ShowNodeInWindowsExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = (DirectoryInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
            if (directoryInfo.Exists)
                Process.Start(directoryInfo.FullName);
        }

        private void ShowInWindowsExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new DirectoryInfo(musicFolderPath).Exists)
                Process.Start(musicFolderPath);
        }

        private void ShowInWindowsExplorerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (new DirectoryInfo(playlistPath).Exists)
                Process.Start(playlistPath);
        }

        private void ShowInWindowsExplorerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (new DirectoryInfo(configPath).Exists)
                Process.Start(configPath);
        }
        private void ShowInWindowsExplorerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (new DirectoryInfo(themePath).Exists)
                Process.Start(themePath);
        }

        private void TemporarilyChangeDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeDirectory(false);
        }

        private void ChangeDefaultDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeDirectory(true);
        }

        private void SwitchToDefaultDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musicFolderPath = ReadConfig(configMusicFolderPath);
            DirectoryInfo directoryInfo = new DirectoryInfo(musicFolderPath);
            BuildRecursiveFileExplorerTreeTrunk();
        }

        private void SetStandardFolderAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musicFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            UpdateConfig(configMusicFolderPath, musicFolderPath);
            BuildRecursiveFileExplorerTreeTrunk();
        }

        private void ChangeDirectory(bool asDefault)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    musicFolderPath = fbd.SelectedPath;
                    BuildRecursiveFileExplorerTreeTrunk();
                    if (asDefault)
                        UpdateConfig(configMusicFolderPath, musicFolderPath);
                }
            }
        }

        private void FileTreeExpandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileExplorerTree.ExpandAll();
        }

        private void FileTreeCollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileExplorerTree.CollapseAll();
        }

        private void ConfigureHotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hotkeysForm.Show();
        }

        private void ResetHotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int id = 1; id <= hotkeysAmount; id++)
                ChangeHotkey(GetDefaultKeyCode(id), id);
            hotkeysForm.Hide();
            hotkeysForm = new ConfigHotkeysForm(this);
        }

        private void RemoveAfterTrackEndedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetRemoveAfterTrackEnded(!removeAfterTrackEnded);
            UpdateConfig(configRemoveAfterTrackEnded, removeAfterTrackEnded.ToString());
        }

        private void SetRemoveAfterTrackEnded(bool value)
        {
            removeAfterTrackEnded = value;
            removeFinishedTracksToolStripMenuItem.Checked = value;
        }

        private void RememberQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetRememberQueue(!rememberQueue);
            UpdateConfig(configRememberQueue, rememberQueue.ToString());
        }

        private void SetRememberQueue(bool value)
        {
            rememberQueue = value;
            rememberQueueToolStripMenuItem.Checked = value;
        }

        private void ShowSystemTrayInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetShowSystemTrayInfo(!showSystemTrayInfo);
            UpdateConfig(configShowSystemTrayInfo, showSystemTrayInfo.ToString());
        }

        private void SetShowSystemTrayInfo(bool value)
        {
            showSystemTrayInfo = value;
            showSystemTrayInfoToolStripMenuItem.Checked = value;
        }

        private void ThemePenguinPurpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("penguinPurple");
        }

        private void ThemeFeelsGoodManToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("feelsGoodMan");
        }

        private void ThemeForTheHordeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("forTheHorde");
        }

        private void StardustCrusadersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("stardustCrusaders");
        }

        public void ChangeTheme(string themeName)
        {
            this.themeName = themeName;
            ThemeManager.SetTheme(this, themeName);
            UpdateConfig(configColorTheme, themeName);
            UpdateThemeList();
        }

        private void AboutTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

    }
}
