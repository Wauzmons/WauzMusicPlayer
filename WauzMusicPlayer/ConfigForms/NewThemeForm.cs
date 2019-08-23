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
        public Regex AllowedCharcters { get; } = new Regex("^[a-zA-Z0-9]*$"); 

        public Dictionary<string, Bitmap> MascotMap { get; } = ThemeManager.GetMascotMap();

        public MusicPlayerForm MusicPlayer { get; }

        private bool PreventMascotChangeWindow = false;

        public NewThemeForm(MusicPlayerForm musicPlayer)
        {
            InitializeComponent();

            ThemeManager.AddThemableForm(this);
            ApplyTheme();

            MusicPlayer = musicPlayer;
            string themeName = musicPlayer.themeName;

            foreach(string mascotName in MascotMap.Keys)
            {
                MascotSelection.Items.Add(mascotName);
                if (mascotName.Equals(ThemeManager.imageString))
                    MascotSelection.SelectedIndex = MascotSelection.Items.Count - 1;
            }
            MascotSelection.Items.Add("Custom");
            if (ThemeManager.imageString.StartsWith("Custom:"))
            {
                PreviewMascotBox.Image = ThemeManager.image;
                PreventMascotChangeWindow = true;
                MascotSelection.SelectedIndex = MascotSelection.Items.Count - 1;
            }

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
            PreviewLabel.ForeColor = ThemeManager.highlightFontColor;
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

            UpdatePreview();
        }

        private void ChooseColor(object sender, EventArgs e)
        {
            Button button = (Button) sender;

            ColorDialog.Color = button.BackColor;
            ColorDialog.ShowDialog();

            button.BackColor = ColorDialog.Color;

            UpdatePreview();
        }

        private void MascotSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PreventMascotChangeWindow)
            {
                PreventMascotChangeWindow = false;
                return;
            }

            string selection = MascotSelection.SelectedItem.ToString();
            if (selection.Equals("Custom"))
            {
                UploadCustomMascotDialog.Filter = "Animated Image Files(*.GIF)| *.GIF";
                if (UploadCustomMascotDialog.ShowDialog() == DialogResult.OK)
                {
                    PreviewMascotBox.Image = Image.FromFile(UploadCustomMascotDialog.FileName);
                }
            }
            else
            {
                PreviewMascotBox.Image = ThemeManager.GetMascotMap()[selection];
            }
        }

        private void UpdatePreview()
        {
            PreviewPanel.BackColor = button3.BackColor;
            PreviewSubPanel.BackColor = button1.BackColor;

            PreviewLabel1.BackColor = button4.BackColor;
            PreviewLabel1.ForeColor = button6.BackColor;

            PreviewLabel2.BackColor = button2.BackColor;
            PreviewLabel3.BackColor = button2.BackColor;
            PreviewLabel2.ForeColor = button5.BackColor;
            PreviewLabel3.ForeColor = button5.BackColor;
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
                if (!AllowedCharcters.IsMatch(themeName.Replace(" ", "")))
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
                string selection = MascotSelection.SelectedItem.ToString();

                if (selection.Equals("Custom"))
                    themeString += "Custom:" + ThemeManager.ImageToBase64String(PreviewMascotBox.Image);
                else
                    themeString += MascotSelection.SelectedItem;

                MusicPlayer.UpdateConfig(MusicPlayer.themePath + themeName + ".wzmp", themeString);
                MusicPlayer.UpdateThemeList();
                ShowMessage("Theme Info:", "Your new theme was successfully created!");

                if (applyTheme)
                {
                    MusicPlayer.BringToFront();
                    MusicPlayer.ChangeTheme(themeName);
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
            if (MusicPlayer.showSystemTrayInfo)
                MusicPlayer.SystemTray.ShowBalloonTip(1000, title, message, new ToolTipIcon());
            else
                MessageBox.Show(message);
        }
    }
}
