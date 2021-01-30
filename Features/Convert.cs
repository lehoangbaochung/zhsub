using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using zhsub.Models.Files;
using zhsub.Models.Lists;

namespace zhsub.Features
{
    class Conversion
    {
        public static object TimeLrcToSrt(object time) // xử lý time từ file lrc
        {
            var timeSplit = time.ToString().Split(':'); // 83:67.3205

            var min = int.Parse(timeSplit[0]); // 83

            var sec = int.Parse(timeSplit[1].Split('.')[0]); // 67

            var tick = int.Parse(timeSplit[1].Split('.')[1]); // 320

            int hour = 0;

            if (min > 60) // xét số phút
            {
                hour = min / 60; // 1
                min %= 60; // 23
            }

            if (sec > 60) // xét số giây
            {
                min += sec / 60; // 1
                sec += sec % 60; // 7
            }

            // xét số tích
            if (tick > 1000)
                tick /= 100; // 3205 -> 32

            if (tick > 100)
                tick /= 10; // 320 -> 32

            var oldTime = new int[] { min, sec, tick };

            var newTime = new string[] { hour.ToString() };

            for (int i = 0; i < oldTime.Length; i++)
            {
                if (oldTime[i] < 10)
                {
                    string str = "0" + oldTime[i];
                    newTime[i] = str;
                }               
            }   
            
            if (min < 10)
                return $"{hour}:0{min}:{sec},{tick}";
            
            if (sec < 10)
                return $"{hour}:{min}:0{sec},{tick}";
            
            if (tick < 10)
                return $"{hour}:{min}:{sec},0{tick}";
            
            if (min < 10 && sec < 10)
                return $"{hour}:0{min}:0{sec},{tick}";
            
            if (min < 10 && tick < 10)
                return $"{hour}:0{min}:{sec},0{tick}";
            
            if (min < 10 && sec < 10 && tick < 10)
                return $"{hour}:0{min}:0{sec},0{tick}";
            else
                return $"{hour}:{min}:{sec},{tick}";
        }

        public static object LrcToSrt(List<Lrc> lrcList)
        {
            for (int i = 0; i < lrcList.Count; i++)
            {
                var srt = new Srt()
                {
                    Index = i + 1,
                    StartTime = TimeLrcToSrt(lrcList[i].Time),
                    Text = lrcList[i].Text
                };

                if ((i + 1) < lrcList.Count)
                    srt.EndTime = TimeLrcToSrt(lrcList[i + 1].Time);
                else
                    srt.EndTime = srt.StartTime; // plus 3 secs

                ViewModel.SrtList.Add(srt);
            }

            return ViewModel.SrtList;
        }

        public static List<Lrc> SrtToLrc(List<Srt> srtList)
        {
            var lrcList = new List<Lrc>();

            foreach (var item in srtList)
            {
                //var time = item.StartTime.ToString().Split(':');

                var startTime = item.StartTime.ToString();

                var lrc = new Lrc()
                {
                    Time = startTime.Substring(startTime.IndexOf(':') + 1, startTime.IndexOf(',') + 2).Replace(',', '.'),
                    Text = item.Text
                };

                lrcList.Add(lrc);
            }

            return lrcList;
        }

        public static void Lrc(string text)
        {
            var sr = new StringReader(text);

            string line;

            List<string> lineList = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                lineList.Add(line);
            }

            for (int i = 0; i < lineList.Count; i++)
            {
                var srt = new Srt()
                {
                    Index = 1,
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
