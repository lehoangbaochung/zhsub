using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using zhsub.Models;
using zhsub.Models.Files;

namespace zhsub.Commands
{
    class Save
    {
        public static void Subtitle()
        {
            var saveFileDialog = new SaveFileDialog { Filter = "SubRip file (*.srt)|*.srt|Lyric file (*.lrc)|*.lrc" };

            if (saveFileDialog.ShowDialog() == false) return;

            var fileName = saveFileDialog.FileName;

            if (fileName.EndsWith(".srt"))
            {
                SrtFile(fileName);
            }

            if (fileName.EndsWith(".lrc"))
            {
                LrcFile(fileName);
            }

            MainWindow.EditingFileName = fileName;
        }   

        private static void SrtFile(string fileName)
        {
            string srt = null;

            foreach (Srt item in List.Srt)
            {
                if (item.Text == null) item.Text = " ";

                srt += $"{ item.Index }\r\n" +
                    $"0{ item.StartTime.ToString().Replace('.', ',') } --> 0{ item.EndTime.ToString().Replace('.', ',') }\r\n" +
                    $"{ item.Text }\r\n\r\n";
            }

            File.WriteAllText(fileName, srt);
        }

        private static void LrcFile(string fileName)
        {
            string lrc = null;

            foreach (Lrc item in List.Lrc)
            {
                lrc += $"[{ item.Time }]{ item.Text }";
            }

            File.WriteAllText(fileName, lrc);
        }
    }

    class SaveAs
    {

    }
}
