using System;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class ConfigHotkeysForm : Form, Themable
    {

        MusicPlayerForm musicPlayer;

        public ConfigHotkeysForm(MusicPlayerForm musicPlayer)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            this.musicPlayer = musicPlayer;
            LoadHotkeys();

            InfoLabel.Text
                = "Tip: You can also use the media buttons on\n"
                + "your keyboard to control the player."
                ;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.mainBackColorVariant1;
            TableLayoutPanel.BackColor = ThemeManager.mainBackColorVariant2;

            label1.ForeColor = ThemeManager.highlightFontColor;
            label2.ForeColor = ThemeManager.highlightFontColor;
            label3.ForeColor = ThemeManager.highlightFontColor;
            label4.ForeColor = ThemeManager.highlightFontColor;
            label5.ForeColor = ThemeManager.highlightFontColor;
            label6.ForeColor = ThemeManager.highlightFontColor;
            label7.ForeColor = ThemeManager.highlightFontColor;

            InfoLabel.ForeColor = ThemeManager.mainFontColor;
            InfoLabel.BackColor = ThemeManager.mainBackColorVariant1;
        }

        private void ConfigHotkeysForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            LoadHotkeys();
            Hide();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            LoadHotkeys();
            Hide();
        }

        private void LoadHotkeys()
        {
            hotKeyControl1.HotKey = musicPlayer.hotkeysMap[1];
            hotKeyControl2.HotKey = musicPlayer.hotkeysMap[2];
            hotKeyControl3.HotKey = musicPlayer.hotkeysMap[3];
            hotKeyControl4.HotKey = musicPlayer.hotkeysMap[4];
            hotKeyControl5.HotKey = musicPlayer.hotkeysMap[5];
            hotKeyControl6.HotKey = musicPlayer.hotkeysMap[6];
            hotKeyControl7.HotKey = musicPlayer.hotkeysMap[7];
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveHotkeys();
            Hide();
        }

        private void SaveHotkeys()
        {
            musicPlayer.ChangeHotkey(hotKeyControl1.HotKey, 1);
            musicPlayer.ChangeHotkey(hotKeyControl2.HotKey, 2);
            musicPlayer.ChangeHotkey(hotKeyControl3.HotKey, 3);
            musicPlayer.ChangeHotkey(hotKeyControl4.HotKey, 4);
            musicPlayer.ChangeHotkey(hotKeyControl5.HotKey, 5);
            musicPlayer.ChangeHotkey(hotKeyControl6.HotKey, 6);
            musicPlayer.ChangeHotkey(hotKeyControl7.HotKey, 7);
        }

    }
}
