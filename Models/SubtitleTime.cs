// Copyright (c) ZitherHarp 2021. All rights reserved.
// Licensed under the ZitherHarp license.
// Email: harp.zither@gmail.com.

using zhsub.Views;

namespace zhsub.Models
{
    internal class SubtitleTime
    {
        private readonly MainWindow mainWindow;
        private readonly TimingWindow timingWindow;

        public SubtitleTime() { }

        public SubtitleTime(MainWindow mainWindow, TimingWindow timingWindow)
        {
            this.mainWindow = mainWindow;
            this.timingWindow = timingWindow;
        }

        public string[] Subtract(string startTime, string endTime)
        {
            return Subtract(Split(startTime), Split(endTime));
        }

        public string[] Subtract(string[] startTime, string[] endTime)
        {
            var timeArray = new string[4];

            for (int i = 3; i >= 0; i--)
            {
                timeArray[i] = $"{ int.Parse(endTime[i]) - int.Parse(startTime[i]) }";
            }

            if (int.Parse(timeArray[3]) < 0)
            {
                timeArray[3] = $"{ int.Parse(timeArray[3]) + 1000 }";
                timeArray[2] = $"{ int.Parse(timeArray[2]) - 1 }";
            }

            if (int.Parse(timeArray[2]) < 0)
            {
                timeArray[2] = $"{ int.Parse(timeArray[2]) + 60 }";
                timeArray[1] = $"{ int.Parse(timeArray[1]) - 1 }";
            }

            if (int.Parse(timeArray[1]) < 0)
            {
                timeArray[1] = $"{ int.Parse(timeArray[1]) + 60 }";
                timeArray[0] = $"{ int.Parse(timeArray[0]) - 1 }";
            }

            return Format(timeArray);
        }    

        public string[] Split(string time)
        {
            var timeSplit = time.Split(':');

            string min = timeSplit[^2];
            string sec = timeSplit[^1].Split('.')[0];
            string millisec = timeSplit[^1].Split('.')[1];
            string hour;

            if (timeSplit.Length < 3)
                hour = null;
            else
                hour = timeSplit[^3];

            return new string[] { hour, min, sec, millisec };
        }

        public void ShiftAllLines(bool isForward)
        {
            if (isForward)
            {
                foreach (SubtitleLine line in mainWindow.lvEditor.Items)
                {
                    //line.StartTime = Forward(line.StartTime);
                }
            }
            else
            {
                foreach (SubtitleLine line in mainWindow.lvEditor.Items)
                {
                    var array = Split(line.StartTime);

                    array[3] = int.Parse(array[3]) - int.Parse(timingWindow.tbxMilliSecond.Text) + "";
                    array[2] = int.Parse(array[2]) - int.Parse(timingWindow.tbxSecond.Text) + "";
                    array[1] = int.Parse(array[1]) - int.Parse(timingWindow.tbxMinute.Text) + "";
                    array[0] = int.Parse(array[0]) - int.Parse(timingWindow.tbxHour.Text) + "";

                    line.StartTime = $"{ array[0] }:{ array[1] }:{ array[2] }.{ array[3] }";
                }
            }    
        }

        public string Forward(string[] sourceTime, string[] forwardTime)
        {
            string hour = $"{ int.Parse(sourceTime[0]) + int.Parse(forwardTime[0]) }";
            string minute = $"{ int.Parse(sourceTime[1]) + int.Parse(forwardTime[1]) }";
            string second = $"{ int.Parse(sourceTime[2]) + int.Parse(forwardTime[2]) }";
            string millisecond = $"{ int.Parse(sourceTime[3]) + int.Parse(forwardTime[3]) }";

            return $"{ hour }:{ minute }:{ second }.{ millisecond }";
        }

        /// <summary>
        /// Format times of lines displayed on listview
        /// </summary>
        /// <param name="time"></param>
        /// <returns>The times has been formatted in the .ZHS file</returns>
        public string Format(string time)
        {
            time = time.Replace(',', '.');

            var timeArray = time.Split(':');

            if (timeArray.Length < 3) // 00:00.000
            {
                time = "00:" + time; // -> 00:00:00.000

                if (!timeArray[^1].Contains('.')) // 00:00:00 -> 00:00:00.000
                    time += ".000";
            }

            for (int i = 0; i < 1; i++)
            {
                // 0:00:00.000 -> 00:00:00.000
                if (timeArray[i].Length == 1) timeArray[i] = "0" + timeArray[i];
                // 00:00:00.0000[..] -> 00:00:00.000
                if (timeArray[i].Length > 3) timeArray[i].Remove(3);
            }

            if (timeArray[^1].Length == 5) // 00:00:00.00 -> 00:00:00.000
                timeArray[^1] += "0";

            return time;
        }

        private string[] Format(string[] timeArray)
        {
            var hour = int.Parse(timeArray[0]);
            var minute = int.Parse(timeArray[1]);
            var second = int.Parse(timeArray[2]);
            var millisecond = int.Parse(timeArray[3]);

            if (millisecond < 10)
                timeArray[3] = "00" + timeArray[3];   
            else if (millisecond > 10 && millisecond < 100)
                timeArray[3] = "0" + timeArray[3];

            if (second < 10)
                timeArray[2] = "0" + timeArray[2];

            if (minute < 10)
                timeArray[1] = "0" + timeArray[1];

            if (hour < 10)
                timeArray[0] = "0" + timeArray[0];

            return timeArray;
        }

        public int GetDuration(string startTime, string endTime)
        {
            if (startTime == "" || endTime == "") return 0;

            var timeArray = Subtract(startTime, endTime);

            return int.Parse(timeArray[0]) * 3600 + int.Parse(timeArray[1]) * 60 + int.Parse(timeArray[2]) + int.Parse(timeArray[3]) / 1000;
        }
    }
}
