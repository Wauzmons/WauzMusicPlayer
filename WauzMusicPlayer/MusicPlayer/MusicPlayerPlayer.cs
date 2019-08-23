using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WauzMusicPlayer
{
    public partial class MusicPlayerForm : Form
    {

        private void PausePlayButton_Click(object sender, EventArgs e)
        {
            PlayPauseMainPlayer();
        }

        private void PlayPauseMainPlayer()
        {
            if (IsMainPlayerActive())
                MainPlayerPause();
            else if (IsMainPlayerPaused())
                MainPlayerPlay();
            else
                MainPlayerNext();
        }

        private Boolean IsMainPlayerActive()
        {
            return mainPlayerWMP.playState.Equals(WMPLib.WMPPlayState.wmppsPlaying);
        }

        private Boolean IsMainPlayerPaused()
        {
            return mainPlayerWMP.playState.Equals(WMPLib.WMPPlayState.wmppsPaused);
        }

        private void PlayOnMainPlayer(FileInfo fileInfo)
        {
            if (!new FileInfo(fileInfo.FullName).Exists)
            {
                RemoveFromQueue(mainPlaylist.First());
                return;
            }

            if (fileInfo.Extension.Equals(".wzst"))
                mainPlayerWMP.URL = ReadConfig(fileInfo.FullName);
            else
                mainPlayerWMP.URL = fileInfo.FullName;

            MainPlayerPlay();
        }

        private void MainPlayerPlay()
        {
            if (mainPlayerWMP.currentMedia == null)
                return;

            mainPlayerWMP.controls.play();
            PausePlayButton.ImageIndex = 1;
        }

        private void MainPlayerPause()
        {
            mainPlayerWMP.controls.pause();
            PausePlayButton.ImageIndex = 0;
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            MainPlayerPrev();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            MainPlayerSkip(true);
        }

        private void MainPlayerSkip(bool playNext)
        {
            if (mainPlaylist.Count == 0)
            {
                PausePlayButton.ImageIndex = 0;
                return;
            }

            QueuedSong currentSong = mainPlaylist.First();
            MainPlayerPause();
            currentSong.SetPlaying(false);
            mainPlaylist.Remove(currentSong);
            if (removeAfterTrackEnded)
                QueuePanel.Controls.Remove(currentSong.panel);
            else
            {
                mainPlaylist.Add(currentSong);
                QueuePanel.Controls.SetChildIndex(currentSong.panel, QueuePanel.Controls.Count);
            }

            if (playNext)
                MainPlayerNext();
        }

        private void MainPlayerPrev()
        {
            if (mainPlaylist.Count == 0)
            {
                PausePlayButton.ImageIndex = 0;
                return;
            }

            QueuedSong currentSong = mainPlaylist.First();
            QueuedSong nextSong = mainPlaylist.Last();
            MainPlayerPause();
            currentSong.SetPlaying(false);
            nextSong.SetPlaying(true);
            mainPlaylist.Remove(nextSong);
            mainPlaylist.Insert(0, nextSong);
            QueuePanel.Controls.SetChildIndex(nextSong.panel, 0);
            PlayOnMainPlayer(nextSong.fileInfo);
        }

        private void MainPlayerNext()
        {
            if (mainPlaylist.Count == 0)
            {
                PausePlayButton.ImageIndex = 0;
                return;
            }

            QueuedSong currentSong = mainPlaylist.First();
            currentSong.SetPlaying(true);
            PlayOnMainPlayer(currentSong.fileInfo);
        }

        private void MainPlayerAudioControl_ValueChanged(object sender, EventArgs e)
        {
            mainPlayerWMP.settings.volume = MainPlayerAudioControl.Value;
        }

        private void MainPlayerVolumeChange(int amount)
        {
            if (MainPlayerAudioControl.Value + amount > 100)
                MainPlayerAudioControl.Value = 100;
            else if (MainPlayerAudioControl.Value + amount < 0)
                MainPlayerAudioControl.Value = 0;
            else
                MainPlayerAudioControl.Value += amount;

            mainPlayerWMP.settings.volume = MainPlayerAudioControl.Value;
        }

        private void MainPlayerTrackBar_Scroll(object sender, EventArgs e)
        {
            if (mainPlayerWMP.controls.currentPositionString != "")
            {
                mainPlayerWMP.controls.currentPosition = MainPlayerTrackBar.Value;
                MainPlayerUpdateDuration();
            }
        }

        private void MainPlayerTrackPositionChange(int amount)
        {
            if (mainPlayerWMP.controls.currentPositionString == "")
                return;

            if (MainPlayerTrackBar.Value + amount > MainPlayerTrackBar.Maximum)
                MainPlayerTrackBar.Value = MainPlayerTrackBar.Maximum;
            else if (MainPlayerTrackBar.Value + amount < MainPlayerTrackBar.Minimum)
                MainPlayerTrackBar.Value = MainPlayerTrackBar.Minimum;
            else
                MainPlayerTrackBar.Value += amount;

            mainPlayerWMP.controls.currentPosition = MainPlayerTrackBar.Value;
            MainPlayerUpdateDuration();
        }

        private async void MainPlayerPlayStateChange(int playState)
        {
            if (mainPlayerWMP.playState.Equals(WMPLib.WMPPlayState.wmppsMediaEnded))
            {
                await Task.Delay(100);
                MainPlayerSkip(true);
            }
        }

        private async void StartMainPlayerControlLoop()
        {
            while (true)
            {
                if (IsMainPlayerActive())
                {
                    PausePlayButton.ImageIndex = 1;

                    MainPlayerUpdateDuration();
                }
                else
                {
                    PausePlayButton.ImageIndex = 0;

                    if (!IsMainPlayerPaused())
                        MainPlayerResetDuration();
                }
                await Task.Delay(1000);
            }
        }

        private void MainPlayerUpdateDuration()
        {
            String currentPositionString = mainPlayerWMP.controls.currentPositionString;
            DurationLabel.Text = currentPositionString == "" ? "00:00" : currentPositionString + " / " + mainPlayerWMP.currentMedia.durationString;

            int index = mainPlayerWMP.URL.LastIndexOf("\\") + 1;
            string marqueeString = mainPlayerWMP.URL.Substring(index, mainPlayerWMP.URL.Length - index);

            if (MarqueeLabel.Text != marqueeString)
            {
                MarqueeLabel.Text = marqueeString;
                ResetMarqueePosition();

                try
                {
                    TagLib.File file = TagLib.File.Create(mainPlayerWMP.URL);
                    if (file.Tag.Pictures.Length >= 1)
                        AlbumBox.Image = Image.FromStream(new MemoryStream(file.Tag.Pictures[0].Data.Data))
                            .GetThumbnailImage(130, 130, null, IntPtr.Zero);
                    else
                        AlbumBox.Image = Properties.Resources.album_art;
                }
                catch
                {
                    AlbumBox.Image = Properties.Resources.album_art;
                }


                if (showSystemTrayInfo)
                    SystemTray.ShowBalloonTip(1000, "Now Playing:", marqueeString, new ToolTipIcon());
            }

            MainPlayerTrackBar.Maximum = (int)mainPlayerWMP.currentMedia.duration;
            MainPlayerTrackBar.Value = MainPlayerTrackBar.Maximum == 0 ? 0 : (int)mainPlayerWMP.controls.currentPosition;
        }

        private void MainPlayerResetDuration()
        {
            DurationLabel.Text = "00:00";
            if (MarqueeLabel.Text != "No Song playing...")
            {
                MarqueeLabel.Text = "No Song playing...";
                ResetMarqueePosition();
            }

            AlbumBox.Image = Properties.Resources.album_art;

            MainPlayerTrackBar.Maximum = 0;
            MainPlayerTrackBar.Value = 0;
        }

        private void MarqueeTimer_Tick(object sender, EventArgs e)
        {
            MarqueeLabel.Location = new Point(MarqueeLabel.Location.X - 1, MarqueeLabel.Location.Y);
            if (MarqueeLabel.Location.X < 0 - MarqueeLabel.Width)
                ResetMarqueePosition();
        }

        private void ResetMarqueePosition()
        {
            MarqueeLabel.Location = new Point(MarqueePanel.Width, MarqueeLabel.Location.Y);
        }

        private void InitSoundCaptureChart()
        {
            SoundCaptureChart.ChartAreas[0].AxisY.Minimum = 0;
            SoundCaptureChart.ChartAreas[0].AxisY.Maximum = 75;
            SoundCaptureChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            SoundCaptureChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            SoundCaptureChart.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
            SoundCaptureChart.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
            soundCapture = new SoundCapture();
        }

        private void SoundCaptureTimer_Tick(object sender, EventArgs e)
        {
            SoundCaptureChart.Series[0].Points.Clear();

            int volume = (mainPlayerWMP.settings.volume == 0 ? 1 : mainPlayerWMP.settings.volume);
            float multiplier = 1 + (float)(100 - mainPlayerWMP.settings.volume) / volume;
            //multiplier = multiplier > 2 ? multiplier : 2;

            float[] points = soundCapture.GetFFtData(100 * multiplier);

            if (points != null && points.Count() > 0)
                foreach (float point in points)
                    SoundCaptureChart.Series[0].Points.AddY(point * multiplier);
            else
                for (int i = 1; i <= 15; i++)
                    SoundCaptureChart.Series[0].Points.AddY(0);
        }

    }
}
