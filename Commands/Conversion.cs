using System.Collections.Generic;
using System.IO;
using zhsub.Models;

namespace zhsub.Features
{
    //class Conversion
    //{
    //    public static object TimeLrcToSrt(object time) // xử lý time từ file lrc
    //    {
    //        var timeSplit = time.ToString().Split(':'); // 83:67.3205

    //        var min = int.Parse(timeSplit[0]); // 83

    //        var sec = int.Parse(timeSplit[1].Split('.')[0]); // 67

    //        var tick = int.Parse(timeSplit[1].Split('.')[1]); // 320

    //        int hour = 0;

    //        if (min > 60) // xét số phút
    //        {
    //            hour = min / 60; // 1
    //            min %= 60; // 23
    //        }

    //        if (sec > 60) // xét số giây
    //        {
    //            min += sec / 60; // 1
    //            sec += sec % 60; // 7
    //        }

    //        // xét số tích
    //        if (tick > 1000)
    //            tick /= 100; // 3205 -> 32

    //        if (tick > 100)
    //            tick /= 10; // 320 -> 32

    //        var oldTime = new int[] { min, sec, tick };

    //        var newTime = new string[] { hour.ToString() };

    //        for (int i = 0; i < oldTime.Length; i++)
    //        {
    //            if (oldTime[i] < 10)
    //            {
    //                string str = "0" + oldTime[i];
    //                newTime[i] = str;
    //            }               
    //        }   
            
    //        if (min < 10)
    //            return $"{hour}:0{min}:{sec},{tick}";
            
    //        if (sec < 10)
    //            return $"{hour}:{min}:0{sec},{tick}";
            
    //        if (tick < 10)
    //            return $"{hour}:{min}:{sec},0{tick}";
            
    //        if (min < 10 && sec < 10)
    //            return $"{hour}:0{min}:0{sec},{tick}";
            
    //        if (min < 10 && tick < 10)
    //            return $"{hour}:0{min}:{sec},0{tick}";
            
    //        if (min < 10 && sec < 10 && tick < 10)
    //            return $"{hour}:0{min}:0{sec},0{tick}";
    //        else
    //            return $"{hour}:{min}:{sec},{tick}";
    //    }

    //    public static void LrcToSrt(string text)
    //    {
    //        var sr = new StringReader(text);
            
    //        string line;

    //        var list = new List<string>();

    //        while ((line = sr.ReadLine()) != null)
    //        {
    //            list.Add(line);
    //        }

    //        for (int i = 0; i < list.Count - 1; i++)
    //        {
    //            var srt = new Srt()
    //            {
    //                Index = i + 1,
    //                StartTime = list[i].Substring(list[i].IndexOf('[') + 1, list[i].IndexOf(']') - 1),
    //                EndTime = list[i + 1].Substring(list[i + 1].IndexOf('[') + 1, list[i + 1].IndexOf(']') - 1),
    //                Text = list[i].Substring(list[i].IndexOf(']') + 1)
    //            };

    //            List.Srt.Add(srt);
    //        }
    //    }
        
    //    public static void LrcToSrt()
    //    {
    //        List.Srt.Clear();

    //        for (int i = 0; i < List.Lrc.Count - 1; i++)
    //        {
    //            var srt = new Srt()
    //            {
    //                Index = i + 1,
    //                StartTime = List.Lrc[i].Time.ToString()
    //                    .Substring(List.Lrc[i].Time.ToString().IndexOf('[') + 1, List.Lrc[i].Time.ToString().IndexOf(']') - 1),
    //                EndTime = List.Lrc[i + 1].Time.ToString()
    //                    .Substring(List.Lrc[i + 1].Time.ToString().IndexOf('[') + 1, List.Lrc[i + 1].Time.ToString().IndexOf(']') - 1),
    //                Text = List.Lrc[i].Text.ToString().Substring(List.Lrc[i].Text.ToString().IndexOf(']') + 1)
    //            };

    //            List.Srt.Add(srt);
    //        }
    //    }

    //    public static void SrtToLrc()
    //    {
    //        List.Lrc.Clear();

    //        foreach (var item in List.Srt)
    //        {
    //            var lrc = new Lrc()
    //            {
    //                Time = item.StartTime,
    //                Text = item.Text
    //            };

    //            List.Lrc.Add(lrc);
    //        }    
    //    }
    //}
}
