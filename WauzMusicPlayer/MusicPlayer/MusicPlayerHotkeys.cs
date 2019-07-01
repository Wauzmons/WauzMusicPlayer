using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class MusicPlayerForm : Form
    {

        private Keys GetDefaultKeyCode(int id)
        {
            switch (id)
            {
                case 1: return Keys.Alt + Keys.P.GetHashCode();
                case 2: return Keys.Alt + Keys.B.GetHashCode();
                case 3: return Keys.Alt + Keys.N.GetHashCode();
                case 4: return Keys.Alt + Keys.C.GetHashCode();
                case 5: return Keys.Alt + Keys.V.GetHashCode();
                case 6: return Keys.Alt + Keys.Oemplus.GetHashCode();
                case 7: return Keys.Alt + Keys.OemMinus.GetHashCode();
                default: return Keys.None;
            }
        }

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        public int hotkeysAmount = 7;

        public Dictionary<int, Keys> hotkeysMap = new Dictionary<int, Keys>();

        private KeysConverter keysConverter = new KeysConverter();

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);              // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);   // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                    // The id of the hotkey that was pressed.

                if (id == 1 || id == 101)
                    PlayPauseMainPlayer();
                else if (id == 2 || id == 102)
                    MainPlayerPrev();
                else if (id == 3 || id == 103)
                    MainPlayerSkip(true);
                else if (id == 104)
                    MainPlayerStop();
                else if (id == 4)
                    MainPlayerTrackPositionChange(-30);
                else if (id == 5)
                    MainPlayerTrackPositionChange(30);
                else if (id == 6)
                    MainPlayerVolumeChange(10);
                else if (id == 7)
                    MainPlayerVolumeChange(-10);
            }
        }

        public void ChangeHotkey(Keys keys, int id)
        {
            String keysString = keysConverter.ConvertToString(keys);
            UpdateConfig(configHotkeys + id + ".wzmp", keysString);

            hotkeysMap[id] = keys;

            KeyModifier keyModifier = KeyModifier.None;

            if (keys.ToString().Contains("Alt"))
                keyModifier = KeyModifier.Alt;
            else if (keys.ToString().Contains("Control"))
                keyModifier = KeyModifier.Control;
            else if (keys.ToString().Contains("Shift"))
                keyModifier = KeyModifier.Shift;
            else if (keys.ToString().Contains("WinKey"))
                keyModifier = KeyModifier.WinKey;

            keys = keys & ~Keys.Modifiers;

            UnregisterHotKey(Handle, id);
            RegisterHotKey(Handle, id, (int)keyModifier, keys.GetHashCode());
        }

    }
}
