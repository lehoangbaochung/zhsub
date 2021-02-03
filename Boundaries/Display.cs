using System;
using System.Collections.Generic;
using System.IO;
using zhsub.Models.Lists;
using zhsub.Models.Files;
using System.Windows.Controls;

namespace zhsub.Boundaries
{
    class Display
    {
        public static void SubtitleFile(Tuple<string, string> tuple, ListView listView)
        {
            ListModel.SrtList.Clear();
            listView.ItemsSource = null; // reset listview

            if (tuple.Item1.EndsWith(".srt"))
            {
                SrtFile(tuple.Item2);
                listView.ItemsSource = ListModel.SrtList;
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

                ListModel.LrcList.Add(lrc);
            }
        }

        public static void SrtFile(string text)
        {
            var sr = new StringReader(text);

            string line;

            var list = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }

            for (int i = 0; i < list.Count; i+=4)
            {
                var srt = new Srt()
                {
                    Index = list[i],
                    StartTime = list[i + 1].Substring(0, list[i + 1].IndexOf('-') - 1).Replace(',', '.'),
                    EndTime = list[i + 1].Substring(list[i + 1].IndexOf('>') + 2).Replace(',', '.'),
                    Text = list[i + 2]
                };

                ListModel.SrtList.Add(srt);
            }

            //var lastTime = list[list.Count - 1].Substring(list[list.Count - 1].IndexOf('[') + 1, list[list.Count - 1].IndexOf(']') - 1);

            //ListModel.SrtList.Add(new Srt() // thêm dòng cuối
            //{
            //    Index = list.Count,
            //    StartTime = lastTime,
            //    EndTime = lastTime,
            //    Text = list[list.Count - 1].Substring(list[list.Count - 1].IndexOf(']') + 1)
            //});
        }

        public static void Video(string filePath, MediaElement mediaElement)
        {
            if (filePath == null) return;

            mediaElement.Visibility = System.Windows.Visibility.Visible;
            mediaElement.Source = new Uri(filePath);
        }
    }
}
