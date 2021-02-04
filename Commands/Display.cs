using System;
using System.IO;
using zhsub.Models;
using zhsub.Models.Files;
using System.Windows.Controls;

namespace zhsub.Commands
{
    class Display
    {
        public static void SubtitleFile(Tuple<string, string> tuple, ListView listView)
        {
            List.Srt.Clear();
            listView.ItemsSource = null; // reset listview

            if (tuple.Item1.EndsWith(".srt"))
            {

                listView.ItemsSource = List.Srt;
            }

            if (tuple.Item1.EndsWith(".lrc"))
                LrcFile(tuple.Item2);
        }

        public static void LrcFile(string text)
        {
            var sr = new StringReader(text);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                var lrc = new Lrc()
                {
                    Time = line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - 1),
                    Text = line.Substring(line.IndexOf(']') + 1)
                };

                List.Lrc.Add(lrc);
            }
        }

        public static void SrtFile(GridView gridView)
        {
            gridView.Columns.Clear();

            var columns = new GridViewColumn[]
            {
                new GridViewColumn() { Header = "Index", Width = 50 },
                new GridViewColumn() { Header = "Start", Width = 100 },
                new GridViewColumn() { Header = "End", Width = 100 },
                new GridViewColumn() { Header = "CPS", Width = 50 },
                new GridViewColumn() { Header = "Text", Width = 470 }
            };

            foreach (var column in columns)
            {
                gridView.Columns.Add(column);
            }
            

        }

        public static void Video(string filePath, MediaElement mediaElement)
        {
            if (filePath == null) return;

            mediaElement.Visibility = System.Windows.Visibility.Visible;
            mediaElement.Source = new Uri(filePath);
        }
    }
}
