using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class MusicPlayerForm : Form
    {

        private void AddSelectedToQueue(bool startPlaying)
        {
            if (GetFileSystemInfo(FileExplorerTree.SelectedNode) is FileInfo)
            {
                FileInfo fileInfo = (FileInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);
                AddToQueue(fileInfo, startPlaying);
            }
        }

        private void AddAllToQueue(DirectoryInfo parentDirectoryInfo, bool recursive)
        {
            foreach (FileInfo fileInfo in parentDirectoryInfo.GetFiles())
                if (validAudioFormats.Contains(fileInfo.Extension))
                    AddToQueue(fileInfo, false);

            if (recursive)
                foreach (DirectoryInfo directoryInfo in parentDirectoryInfo.GetDirectories())
                    AddAllToQueue(directoryInfo, recursive);
        }

        public void AddToQueue(FileInfo fileInfo, bool startPlaying)
        {
            FlowLayoutPanel queuedSongPanel = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            QueuedSong queuedSong = new QueuedSong(fileInfo, queuedSongPanel);

            Button queuedSongSkipToButton = new Button
            {
                ImageIndex = 0,
                ImageList = ButtonIconList,
                UseVisualStyleBackColor = true,
                Size = new System.Drawing.Size(26, 26)
            };
            queuedSongSkipToButton.Click += (rmsender, rmargs) => SkipToSong(queuedSong);
            queuedSongPanel.Controls.Add(queuedSongSkipToButton);

            Button queuedSongRemoveButton = new Button
            {
                ImageIndex = 2,
                ImageList = ButtonIconList,
                UseVisualStyleBackColor = true,
                Size = new System.Drawing.Size(26, 26)
            };
            queuedSongRemoveButton.Click += (rmsender, rmargs) => RemoveFromQueue(queuedSong);
            queuedSongPanel.Controls.Add(queuedSongRemoveButton);

            Label queuedSongLabel = new Label
            {
                AutoSize = true,
                Text = fileInfo.Name,
                Margin = new Padding(0, 8, 2, 8),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)))
            };
            queuedSongPanel.Controls.Add(queuedSongLabel);

            if (startPlaying)
            {
                MainPlayerSkip(false);
                mainPlaylist.Insert(0, queuedSong);
                QueuePanel.Controls.Add(queuedSongPanel);
                QueuePanel.Controls.SetChildIndex(queuedSongPanel, 0);
                queuedSong.SetPlaying(true);
                PlayOnMainPlayer(fileInfo);
            }
            else
            {
                mainPlaylist.Add(queuedSong);
                QueuePanel.Controls.Add(queuedSongPanel);
                queuedSong.SetPlaying(false);
            }
        }

        private void RemoveFromQueue(QueuedSong queuedSong)
        {
            QueuedSong currentSong = mainPlaylist.First();

            mainPlaylist.Remove(queuedSong);
            QueuePanel.Controls.Remove(queuedSong.panel);

            if (queuedSong.Equals(currentSong))
            {
                mainPlayerWMP.URL = null;
                MainPlayerNext();
            }
        }

        private void SkipToSong(QueuedSong queuedSong)
        {
            while (mainPlaylist.First() != queuedSong)
            {
                MainPlayerSkip(false);
            }
            queuedSong.SetPlaying(true);
            PlayOnMainPlayer(queuedSong.fileInfo);
            PausePlayButton.Focus();
        }

        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            List<QueuedSong> songList = new List<QueuedSong>(mainPlaylist);

            while (mainPlaylist.Count != 0)
                RemoveFromQueue(mainPlaylist.Last());

            Random random = new Random();
            while (songList.Count != 0)
            {
                QueuedSong song = songList[random.Next(songList.Count)];
                AddToQueue(song.fileInfo, false);
                songList.Remove(song);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            MainPlayerStop();
        }

        private void MainPlayerStop()
        {
            while (mainPlaylist.Count != 0)
                RemoveFromQueue(mainPlaylist.Last());
        }

        private void SaveSongQueue()
        {
            string rememberedQueuePath = configPath + "configRememberedQueue.wzmp";
            string rememberedQueueContent = "volume*" + MainPlayerAudioControl.Value + "\n";

            foreach (QueuedSong song in mainPlaylist)
                if (song.IsPlaying())
                    rememberedQueueContent += "play*" + song.GetSongPath() + "*" + MainPlayerTrackBar.Value + "\n";
                else
                    rememberedQueueContent += "queue*" + song.GetSongPath() + "\n";

            UpdateConfig(rememberedQueuePath, rememberedQueueContent);
        }

        private void LoadSongQueue()
        {
            string rememberedQueuePath = configPath + "configRememberedQueue.wzmp";
            FileInfo fileInfo = new FileInfo(rememberedQueuePath);
            if (fileInfo.Exists)
            {
                string rememberedQueueContent = "";
                using (StreamReader streamReader = new StreamReader(fileInfo.FullName))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();

                        if (!String.IsNullOrEmpty(line.Replace(" ", "")))
                            rememberedQueueContent += (String.IsNullOrEmpty(rememberedQueueContent.Replace(" ", "")) ? "" : "|") + line;
                    }
                    string[] queueLines = rememberedQueueContent.Split('|');
                    if (queueLines.Count() > 0)
                    {
                        foreach (string queueLine in queueLines)
                        {
                            string[] songInfo = queueLine.Split('*');
                            if (songInfo.Count() < 2)
                                continue;

                            if (songInfo[0].Equals("volume"))
                            {
                                MainPlayerVolumeChange(int.Parse(songInfo[1]) - 50);
                            }
                            else if (songInfo[0].Equals("play") && new FileInfo(songInfo[1]).Exists)
                            {
                                AddToQueue(new FileInfo(songInfo[1]), true);
                                mainPlayerWMP.controls.currentPosition = int.Parse(songInfo[2]);
                            }
                            else if (songInfo[0].Equals("queue") && new FileInfo(songInfo[1]).Exists)
                            {
                                AddToQueue(new FileInfo(songInfo[1]), false);
                            }
                        }
                    }
                }
            }
        }

    }
}
