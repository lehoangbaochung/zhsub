using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace zhsub.Models
{
    class Video : Subtitle
    {
        private readonly MediaElement mediaElement;
        private readonly DockPanel dockPanel;
        private readonly Slider slider;
        private readonly TextBox txbPosition;
        private readonly TextBox txbTick;
        private readonly ComboBox comboBox;
        private readonly TextBlock txbSubtitle;
        private DispatcherTimer timer;


        public Video(DockPanel dockPanel, MediaElement mediaElement, Slider slider, TextBox txbPosition, TextBox txbTick, ComboBox comboBox, ListView listView, TextBlock textBlock)
        {
            this.mediaElement = mediaElement;
            this.dockPanel = dockPanel;
            this.slider = slider;
            this.txbPosition = txbPosition;
            this.txbTick = txbTick;
            this.comboBox = comboBox;
            this.txbSubtitle = textBlock;
        }

        public Video(ListView listView, TextBlock textBlock)
        {
            this.txbSubtitle = textBlock;
        }

        public void Close()
        {
            mediaElement.Pause();
            dockPanel.Visibility = Visibility.Collapsed;
        }

        public new void Open()
        {
            var openFileDialog = new OpenFileDialog() 
            { 
                Filter = "Video formats (*.3gp,*.asf,*.avi,*.avs,*.d2v,*.m2ts,*.m4v,*.mkv,*.mov,*.mp4,*.mpeg,*.mpg,*.ogm,*.webm,*.wmv,*.ts,*.y4m,*.yuv)" +
                    "|*.3gp;*.asf;*.avi;*.avs;*.d2v;*.m2ts;*.m4v;*.mkv;*.mov;*.mp4;*.mpeg;*.mpg;*.ogm;*.webm;*.wmv;*.ts;*.y4m;*.yuv"
            };

            if (openFileDialog.ShowDialog() == false) return;

            mediaElement.Source = new Uri(openFileDialog.FileName);
            dockPanel.Visibility = Visibility.Visible;

            mediaElement.Play();
            mediaElement.Pause();

            Timer();
            Slider();
        }

        public void Pause()
        {
            slider.IsEnabled = true;

            mediaElement.Pause();
            timer.Stop();
        }

        public void Play()
        {
            slider.IsEnabled = false;

            mediaElement.Play();
            timer.Start();  
        }

        public void PlayCurrentLine()
        {
            Play();
        }

        public void Replay()
        {
            slider.IsEnabled = false;

            mediaElement.Position = new TimeSpan(0, 0, 0, 0);
            mediaElement.Play();

            Timer();
            timer.Start();
        }   
        
        private void Slider()
        {
            mediaElement.MediaOpened += (s, e) =>
            {
                slider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                slider.TickFrequency = slider.Maximum / 60;
            };

            slider.IsEnabledChanged += (s, e) =>
            {
                mediaElement.Position = new TimeSpan(0, 0, (int)slider.Value / 60, (int)slider.Value % 60);
            };
        }

        private void Timer()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(0.0000001) };

            timer.Tick += (s, e) =>
            {
                slider.Value = mediaElement.Position.TotalSeconds;
                txbPosition.Text = $"{ mediaElement.Position } - { mediaElement.Position.Ticks }";   
            };
        }
    }
}
