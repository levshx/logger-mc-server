using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Logger
{
    public class Audio
    {
        static WMPLib.WindowsMediaPlayer Player;

        public static void PlaySound(string uuid)
        {
            PlayFile("sounds/"+uuid+".mp3");
        }

        private static void PlayFile(String url)
        {
            Player = new WMPLib.WindowsMediaPlayer();
            Player.PlayStateChange +=
                new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
            Player.MediaError +=
                new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
            Player.URL = url;
            Player.controls.play();
        }

        private static void Player_PlayStateChange(int NewState)
        {
            if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped)
            {
                Player.close();
            }
        }

        private static void Player_MediaError(object pMediaObject)
        {
            Player.close();
        }
    }
}
