using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace zhsub.Commands
{
    class Display
    {
        public static void FileName(Window window, string fileName)
        {
            if (fileName == null) return;

            window.Title = fileName[(fileName.LastIndexOf('\\') + 1)..] + " - Zither Harp Subtitles 1.0.0";
        }

        public static void Video(MediaElement mdeVideo, DockPanel dockPanel, string fileName)
        {
            mdeVideo.Source = new Uri(fileName);
            mdeVideo.Visibility = Visibility.Visible;
            dockPanel.Visibility = Visibility.Visible;

            mdeVideo.Play();
            mdeVideo.Pause();
        }

        public static void VideoSlider(MediaElement mdeVideo, Slider slider)
        {
            slider.TickFrequency = mdeVideo.Position.TotalSeconds / 10;

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

            timer.Tick += (s, e) =>
            {
                slider.Value += 1;
            };
        }
    }
}
