using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using zhsub.Models.Lists;
using zhsub.Models.Files;

namespace zhsub.Boundaries
{
    class Display
    {
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

                ViewModel.LrcList.Add(lrc);
            }
        }

        public static void SrtFile(string text)
        {
            var sr = new StringReader(text);

            string line;

            var lineList = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                lineList.Add(line);
            }

            for (int i = 0; i < lineList.Count; i++)
            {
                var srt = new Srt()
                {
                    Index = i + 1,
                    StartTime = lineList[i].Substring(lineList[i].IndexOf('[') + 1, lineList[i].IndexOf(']') - 1),
                    Text = lineList[i].Substring(lineList[i].IndexOf(']') + 1)
                };

                if (i + 1 < lineList.Count)
                    srt.EndTime = lineList[i + 1].Substring(lineList[i + 1].IndexOf('[') + 1, lineList[i + 1].IndexOf(']') - 1);
                else
                    srt.EndTime = srt.StartTime;

                ViewModel.SrtList.Add(srt);
            }
        }
    }
}
