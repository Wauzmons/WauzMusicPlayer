using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class AudioRecorderForm : Form, Themable
    {

        MusicPlayerForm musicPlayer;

        TreeNode treeNode;

        DirectoryInfo directoryInfo;

        WaveIn waveSource = null;

        WaveFileWriter waveFile = null;

        bool recording = false;

        public AudioRecorderForm(MusicPlayerForm musicPlayer, TreeNode treeNode, DirectoryInfo directoryInfo)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            this.musicPlayer = musicPlayer;
            this.treeNode = treeNode;
            this.directoryInfo = directoryInfo;

            NameLabel.Text = "Record Audio to: " + directoryInfo.Name;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.mainBackColorVariant1;
            TableLayoutPanel.BackColor = ThemeManager.mainBackColorVariant2;

            NameLabel.ForeColor = ThemeManager.highlightFontColor;
            label1.ForeColor = ThemeManager.highlightFontColor;
        }

        private void RecordButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!recording)
                {
                    string fileName = NameTextBox.Text;
                    foreach (char c in Path.GetInvalidFileNameChars())
                    {
                        fileName = fileName.Replace(c, '_');
                    }
                    if (String.IsNullOrWhiteSpace(fileName))
                    {
                        MessageBox.Show("Please enter a name for your file!");
                        return;
                    }
                    else
                    {
                        fileName = $"{directoryInfo.FullName + "\\" + fileName + ".wav"}";
                    }
                    if (new FileInfo(fileName).Exists)
                    {
                        MessageBox.Show("A file with this name already exists!");
                        return;
                    }
                    StartRecording(fileName);
                }
                else
                {
                    waveSource.StopRecording();
                }
            }
            catch
            {
                ShowMessage("Audio Recorder Error:", "Recording failed!");
            }
        }

        private void StartRecording(string fileName)
        {
            try
            {
                RecordButton.BackColor = System.Drawing.Color.Red;
                recording = true;

                waveSource = new WaveIn
                {
                    WaveFormat = new WaveFormat(44100, 1)
                };
                waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(IsDataAvailable);
                waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(StopRecording);

                waveFile = new WaveFileWriter(fileName, waveSource.WaveFormat);

                waveSource.StartRecording();
            }
            catch
            {
                ShowMessage("Audio Recorder Error:", "Recording failed!");
            }
        }

        void StopRecording(object sender, StoppedEventArgs e)
        {
            try
            {
                RecordButton.BackColor = System.Drawing.Color.Green;
                recording = false;

                if (waveSource != null)
                {
                    waveSource.Dispose();
                    waveSource = null;
                }

                if (waveFile != null)
                {
                    waveFile.Dispose();
                    waveFile = null;
                }

                ReloadTreeBranch();
                ShowMessage("Audio Recorder Info:", "Your recorded file was successfully saved!");
            }
            catch
            {
                ShowMessage("Audio Recorder Error:", "Recording failed!");
            }
        }

        void IsDataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        private void ReloadTreeBranch()
        {
            musicPlayer.BuildRecursiveFileExplorerTreeBranch(treeNode.Nodes, directoryInfo);
            treeNode.Expand();
        }

        private void ShowMessage(string title, string message)
        {
            if (musicPlayer.showSystemTrayInfo)
                musicPlayer.SystemTray.ShowBalloonTip(1000, title, message, new ToolTipIcon());
            else
                MessageBox.Show(message);
        }

        private void AudioRecorderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(recording)
                waveSource.StopRecording();
        }
    }
}
