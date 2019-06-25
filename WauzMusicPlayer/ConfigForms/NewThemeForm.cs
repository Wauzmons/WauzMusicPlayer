using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class NewThemeForm : Form, Themable
    {

        Regex allowedCharcters = new Regex("^[a-zA-Z0-9]*$");

        Dictionary<string, Bitmap> mascotMap = ThemeManager.GetMascotMap();

        MusicPlayerForm musicPlayer;

        public NewThemeForm(MusicPlayerForm musicPlayer)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            this.musicPlayer = musicPlayer;
            string themeName = musicPlayer.themeName;

            foreach(string mascotName in mascotMap.Keys)
                MascotSelection.Items.Add(mascotName);

            MascotSelection.SelectedIndex = 0;

            Stack<char> stack = new Stack<char>();
            for (var i = themeName.Length - 1; i >= 0; i--)
            {
                if (!char.IsNumber(themeName[i]))
                    break;

                stack.Push(themeName[i]);
            }

            string version = new string(stack.ToArray());

            try
            {
                long versionNumber = long.Parse(version);
                themeName = themeName.Substring(0, themeName.LastIndexOf(version));

                while (new FileInfo(musicPlayer.themePath + themeName + versionNumber + ".wzmp").Exists)
                    versionNumber++;

                NameTextBox.Text = themeName + versionNumber;
            }
            catch
            {
                NameTextBox.Text = themeName + "1";
            }
        }

        public void ApplyTheme()
        {
            BackColor = ThemeManager.mainBackColorVariant1;
            TableLayoutPanel.BackColor = ThemeManager.mainBackColorVariant2;

            NameLabel.ForeColor = ThemeManager.highlightFontColor;
            label1.ForeColor = ThemeManager.highlightFontColor;
            label2.ForeColor = ThemeManager.highlightFontColor;
            label3.ForeColor = ThemeManager.highlightFontColor;
            label4.ForeColor = ThemeManager.highlightFontColor;
            label5.ForeColor = ThemeManager.highlightFontColor;
            label6.ForeColor = ThemeManager.highlightFontColor;
            label7.ForeColor = ThemeManager.highlightFontColor;
            label8.ForeColor = ThemeManager.highlightFontColor;

            button1.BackColor = ThemeManager.mainBackColorVariant1;
            button2.BackColor = ThemeManager.mainBackColorVariant2;
            button3.BackColor = ThemeManager.accentBackColor;
            button4.BackColor = ThemeManager.highlightBackColor;
            button5.BackColor = ThemeManager.mainFontColor;
            button6.BackColor = ThemeManager.highlightFontColor;
        }

        private void ChooseColor(object sender, EventArgs e)
        {
            Button button = (Button) sender;

            colorDialog.Color = button.BackColor;
            colorDialog.ShowDialog();

            button.BackColor = colorDialog.Color;
        }

        private void SaveAndUseButton_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Save(bool applyTheme)
        {
            try
            {
                string themeName = NameTextBox.Text;

                if (String.IsNullOrWhiteSpace(themeName))
                {
                    MessageBox.Show("Please enter a name for your theme!");
                    return;
                }
                if (!allowedCharcters.IsMatch(themeName.Replace(" ", "")))
                {
                    MessageBox.Show("The name of the theme must be alphanumeric!");
                    return;
                }
                if(ThemeManager.IsPredefinedTheme(themeName))
                {
                    MessageBox.Show("This name is already used by a predefined theme!");
                    return;
                }

                string themeString = "";
                themeString += ColorTranslator.ToHtml(button1.BackColor) + ";";
                themeString += ColorTranslator.ToHtml(button2.BackColor) + ";";
                themeString += ColorTranslator.ToHtml(button3.BackColor) + ";";
                themeString += ColorTranslator.ToHtml(button4.BackColor) + ";";
                themeString += ColorTranslator.ToHtml(button5.BackColor) + ";";
                themeString += ColorTranslator.ToHtml(button6.BackColor) + ";";
                themeString += MascotSelection.SelectedItem;

                musicPlayer.UpdateConfig(musicPlayer.themePath + themeName + ".wzmp", themeString);
                musicPlayer.UpdateThemeList();
                ShowMessage("Theme Info:", "Your new theme was successfully created!");

                if (applyTheme)
                {
                    musicPlayer.BringToFront();
                    musicPlayer.ChangeTheme(themeName);
                }

                Close();
            }
            catch
            {
                ShowMessage("Theme Error:", "Creation of theme failed!");
            }
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
