using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace zhsub.Commands
{
    class Video
    {
        static DispatcherTimer timer;

        public static void Timer(MediaElement mediaElement, Slider slider)
        {
            slider.TickFrequency = mediaElement.NaturalDuration.TimeSpan.TotalSeconds / 60;
            slider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;

            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1) };

            timer.Tick += (s, e) =>
            {
                slider.Value = mediaElement.Position.TotalSeconds;
            };

            slider.ValueChanged += (s, e) =>
            {
                mediaElement.Position = new TimeSpan(0, 0, 0);
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
