using System;
using System.IO;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    class QueuedSong : Themable
    {

        public FileInfo fileInfo;

        public FlowLayoutPanel panel;

        private Boolean isPlaying = false;

        public QueuedSong(FileInfo fileInfo, FlowLayoutPanel panel)
        {
            this.fileInfo = fileInfo;
            this.panel = panel;
        }

        public String GetSongPath()
        {
            return fileInfo.FullName;
        }

        public Boolean IsPlaying()
        {
            return panel.BackColor.Equals(ThemeManager.highlightBackColor);
        }

        public void SetPlaying(Boolean isPlaying)
        {
            this.isPlaying = isPlaying;
            ApplyTheme();
        }

        public void ApplyTheme()
        {
            panel.BackColor = isPlaying ? ThemeManager.highlightBackColor : ThemeManager.mainBackColorVariant2;
            panel.ForeColor = isPlaying ? ThemeManager.highlightFontColor : ThemeManager.mainFontColor;
        }

    }
}
