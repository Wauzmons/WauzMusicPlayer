using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class MusicPlayerForm : Form
    {

        private void GenerateAndLoadConfigs()
        {
            DirectoryInfo configDirectory = new DirectoryInfo(configPath);
            if (!configDirectory.Exists) Directory.CreateDirectory(configDirectory.FullName);

            DirectoryInfo playlistDirectory = new DirectoryInfo(playlistPath);
            if (!playlistDirectory.Exists) Directory.CreateDirectory(playlistDirectory.FullName);

            DirectoryInfo themeDirectory = new DirectoryInfo(themePath);
            if (!themeDirectory.Exists) Directory.CreateDirectory(themeDirectory.FullName);

            DirectoryInfo cacheDirectory = new DirectoryInfo(cachePath);
            if (!cacheDirectory.Exists) Directory.CreateDirectory(cacheDirectory.FullName);

            DirectoryInfo streamDirectory = new DirectoryInfo(streamPath);
            if (!streamDirectory.Exists)
            {
                Directory.CreateDirectory(streamDirectory.FullName);
                UpdateConfig(streamPath + "ChroniX AGGRESSION.wzst", "https://player.fastcast4u.com/gebacher/?pl=wmp&c=0");
                UpdateConfig(streamPath + "MetalBlast FM.wzst", "http://stream.laut.fm/metalblastfm");
            }

            foreach (FileSystemInfo fileSystemInfo in cacheDirectory.GetFileSystemInfos())
                DeleteFileSystemInfo(fileSystemInfo, true);

            configMusicFolderPath = configPath + "configMusicFolderPath.wzmp";
            if (!File.Exists(configMusicFolderPath))
                UpdateConfig(configMusicFolderPath, Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            musicFolderPath = ReadConfig(configMusicFolderPath);

            configColorTheme = configPath + "configColorTheme.wzmp";
            if (!File.Exists(configColorTheme))
                UpdateConfig(configColorTheme, "penguinPurple");
            themeName = ReadConfig(configColorTheme);

            configRemoveAfterTrackEnded = configPath + "configRemoveAfterTrackEnded.wzmp";
            if (!File.Exists(configRemoveAfterTrackEnded))
                UpdateConfig(configRemoveAfterTrackEnded, false.ToString());
            SetRemoveAfterTrackEnded(Boolean.Parse(ReadConfig(configRemoveAfterTrackEnded)));

            configRememberQueue = configPath + "configRememberQueue.wzmp";
            if (!File.Exists(configRememberQueue))
                UpdateConfig(configRememberQueue, false.ToString());
            SetRememberQueue(Boolean.Parse(ReadConfig(configRememberQueue)));

            configShowSystemTrayInfo = configPath + "configShowSystemTrayInfo.wzmp";
            if (!File.Exists(configShowSystemTrayInfo))
                UpdateConfig(configShowSystemTrayInfo, true.ToString());
            SetShowSystemTrayInfo(Boolean.Parse(ReadConfig(configShowSystemTrayInfo)));

            configHotkeys = configPath + "configHotkey";
            for (int id = 1; id <= hotkeysAmount; id++)
            {
                if (!File.Exists(configHotkeys + id + ".wzmp"))
                    ChangeHotkey(GetDefaultKeyCode(id), id);
                else
                {
                    string keyString = ReadConfig(configHotkeys + id + ".wzmp");
                    ChangeHotkey((Keys)(keysConverter.ConvertFromString(keyString)), id);
                }
            }

            ChangeHotkey(Keys.MediaPlayPause, 101);
            ChangeHotkey(Keys.MediaPreviousTrack, 102);
            ChangeHotkey(Keys.MediaNextTrack, 103);
        }

        private string configMusicFolderPath;

        private string configColorTheme;

        private string configRemoveAfterTrackEnded;

        private string configRememberQueue;

        private string configShowSystemTrayInfo;

        private string configHotkeys;

        public string ReadConfig(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                return streamReader.ReadLine();
            }
        }

        public void UpdateConfig(string path, string value)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.WriteLine(value);
            }
        }

    }
}
