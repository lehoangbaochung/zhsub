using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Threading;
using zhsub.Views;

namespace zhsub.Models
{
    internal class Video
    {
        private MainWindow mainWindow;
        private DispatcherTimer timer;

        public Video(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void Close()
        {
            mainWindow.mdeVideo.Pause(); 
            timer.Stop();

            mainWindow.dpaVideo.Visibility = Visibility.Collapsed;
        }

        public void Open()
        {
            var openFileDialog = new OpenFileDialog() 
            { 
                Filter = "Video formats (*.3gp,*.asf,*.avi,*.avs,*.d2v,*.m2ts,*.m4v,*.mkv,*.mov,*.mp4,*.mpeg,*.mpg,*.ogm,*.webm,*.wmv,*.ts,*.y4m,*.yuv)" +
                    "|*.3gp;*.asf;*.avi;*.avs;*.d2v;*.m2ts;*.m4v;*.mkv;*.mov;*.mp4;*.mpeg;*.mpg;*.ogm;*.webm;*.wmv;*.ts;*.y4m;*.yuv"
            };

            if (openFileDialog.ShowDialog() == false) return;

            mainWindow.mdeVideo.Source = new Uri(openFileDialog.FileName);
            mainWindow.dpaVideo.Visibility = Visibility.Visible;

            mainWindow.mdeVideo.Play();
            mainWindow.mdeVideo.Pause();

            Timer();
            Slider();
        }

        public void Pause()
        {
            mainWindow.sliDuration.IsEnabled = true;

            mainWindow.mdeVideo.Pause();
            timer.Stop();
        }

        public void Play()
        {
            mainWindow.sliDuration.IsEnabled = false;

            mainWindow.mdeVideo.Play();
            timer.Start();  
        }

        public void PlayCurrentLine()
        {
            Play();
        }

        public void Replay()
        {
            mainWindow.sliDuration.IsEnabled = false;

            mainWindow.mdeVideo.Position = new TimeSpan(0, 0, 0, 0);
            mainWindow.mdeVideo.Play();

            Timer();
            timer.Start();
        }   
        
        private void Slider()
        {
            mainWindow.mdeVideo.MediaOpened += (s, e) =>
            {
                mainWindow.sliDuration.Maximum = mainWindow.mdeVideo.NaturalDuration.TimeSpan.TotalSeconds;
                mainWindow.sliDuration.TickFrequency = mainWindow.sliDuration.Maximum / 60;
            };

            mainWindow.sliDuration.IsEnabledChanged += (s, e) =>
            {
                mainWindow.mdeVideo.Position = new TimeSpan(0, 0, (int)mainWindow.sliDuration.Value / 60, (int)mainWindow.sliDuration.Value % 60);
            };
        }

        private void Timer()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(0.0000001) };

            timer.Tick += (s, e) =>
            {
                mainWindow.sliDuration.Value = mainWindow.mdeVideo.Position.TotalSeconds;
                mainWindow.txbDuration.Text = $"{ mainWindow.mdeVideo.Position } - { mainWindow.mdeVideo.Position.Ticks }";   
            };
        }
    }
}
