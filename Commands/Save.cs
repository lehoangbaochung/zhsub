using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;
using zhsub.Models.Files;

namespace zhsub.Commands
{
    class Save
    {
        public static void Subtitle(ListView listView)
        {
            if (FilePath().EndsWith(".srt"))
            {
                SrtFile(listView);
            }

            if (FilePath().EndsWith(".lrc"))
            {
                LrcFile(listView);
            }
        }   
        
        private static string FilePath()
        {
            var saveFileDialog = new SaveFileDialog { Filter = "SubRip file (*.srt)|*.srt|Lyric file (*.lrc)|*.lrc" };

            if (saveFileDialog.ShowDialog() == false) return null;

            return saveFileDialog.FileName;
        }

        private static void SrtFile(ListView listView)
        {
            string srt = null;

            foreach (Srt item in listView.Items)
            {
                srt += $"{ item.Index }\n" +
                    $"00:{ item.StartTime.ToString().Replace('.', ',') } --> 00:{ item.EndTime.ToString().Replace('.', ',') }\n" +
                    $"{ item.Text }\n\n";
            }

            File.WriteAllText(FilePath(), srt);
        }

        private static void LrcFile(ListView listView)
        {
            string lrc = null;

            foreach (Lrc item in listView.Items)
            {
                lrc += $"[{ item.Time }]{ item.Text }";
            }

            File.WriteAllText(FilePath(), lrc);
        }
    }

    class SaveAs
    {

    }
}
