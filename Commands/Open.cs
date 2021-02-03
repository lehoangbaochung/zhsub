using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace zhsub.Commands
{
    class Open
    {
        public static Tuple<string, string> Subtitle()
        {
            var openFileDialog = new OpenFileDialog() { Filter = "SubRip files (*.srt)|*.srt|Lyric files (*.lrc)|*.lrc" };

            if (openFileDialog.ShowDialog() == false) return new Tuple<string, string>(null, null);

            return new Tuple<string, string>(openFileDialog.FileName, File.ReadAllText(openFileDialog.FileName));
        }

        public static void Video(MediaElement mediaElement)
        {
            var openFileDialog = new OpenFileDialog() { Filter = "Video files (*.mp4,*.flv,*.3gp,*.avi)|*.mp4;*.flv;*.3gp;*.avi" };

            if (openFileDialog.ShowDialog() == false) return;

            mediaElement.Source = new Uri(openFileDialog.FileName);
        }
    }
}
