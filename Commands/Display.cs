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

        public static void Video(MediaElement mediaElement, DockPanel dockPanel, string fileName)
        {
            mediaElement.Source = new Uri(fileName);
            mediaElement.Visibility = Visibility.Visible;
            dockPanel.Visibility = Visibility.Visible;

            mediaElement.Play();
            mediaElement.Pause();
        }

        public static void VideoSlider(MediaElement mediaElement, Slider slider)
        {
            slider.TickFrequency = mediaElement.Position.TotalSeconds / 10;

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

            timer.Tick += (s, e) =>
            {
                slider.Value += 1;
            };
        }
    }
}
