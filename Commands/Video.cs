using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using zhsub.Models;

namespace zhsub.Commands
{
    class Video
    {
        static DispatcherTimer timer;

        public static void Timer(MediaElement mediaElement, Slider slider, ListView listView, TextBlock textBlock)
        {
            slider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            slider.TickFrequency = slider.Maximum / 60;

            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(0.001) };

            timer.Tick += (s, e) =>
            {
                slider.Value = mediaElement.Position.TotalSeconds;

                if (mediaElement.Position.ToString().Length < 12) return;

                ////var start = List.Srt.Find(s => s.StartTime.ToString() == mediaElement.Position.ToString().Remove(12));

                //if (start == null) return;

                //textBlock.Text = start.Text.ToString();
            };

            slider.IsEnabledChanged += (s, e) =>
            {
                mediaElement.Position = new TimeSpan(0, 0, (int)slider.Value / 60, (int)slider.Value % 60);
            };
        }

        public static void Play(MediaElement mediaElement)
        {
            mediaElement.Play();
            timer.Start();
        }

        public static void Pause(MediaElement mediaElement)
        {
            mediaElement.Pause();
            timer.Stop();
        }
    }
}
