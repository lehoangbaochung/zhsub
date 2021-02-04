using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using zhsub.Models;
using zhsub.Models.Files;
using zhsub.Boundaries;
using zhsub.Commands;
using System.Linq;
using zhsub.Features;
using System.Net;

namespace zhsub
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List.Srt.Add(new Srt()
            {
                Index = 1,
                StartTime = new TimeSpan(0, 0, 0, 0, 0),
                EndTime = new TimeSpan(0, 0, 0, 5, 0),
                Text = null
            });

            lvSrtFile.ItemsSource = List.Srt;
            lvSrtFile.SelectedIndex = 0;
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as ListViewItem;

            if (item != null && item.IsSelected)
            {
                //var selectedResult = lvSearch.SelectedItems[0] as SearchResult;
                //MessageBox.Show("Download " + selectedResult.ID + " successfully!", "Download", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnBookmark_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            griEditor.Visibility = Visibility.Collapsed;
            griSearch.Visibility = Visibility.Visible;

            lvSearch.ItemsSource = Bind.Kugeci(Crawl.Kugeci(tbxSearch.Text));
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;

            if (item != null && item.IsSelected)
            {
                var selectedResult = lvSearch.SelectedItems[0] as SearchResult;
                
                griEditor.Visibility = Visibility.Visible;
                griSearch.Visibility = Visibility.Collapsed;
                tbxInput.Text = null;
                List.Srt.Clear();
                Conversion.LrcToSrt(new WebClient().DownloadString("https://www.kugeci.com/download/lrc/" + selectedResult.ID));

                lvSrtFile.ItemsSource = null;
                lvSrtFile.ItemsSource = List.Srt;
            }
        }

        private void tbxStartTime_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Back || e.Key == Key.Delete) && tbxStartTime.Text.Length == 7)
            {

            }
        }

        private void MenuItem_File_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;

            switch (menuItem.Header)
            {
                case "_New":
                    lvSrtFile.ItemsSource = null;
                    List.Srt.Clear();
                    break;
                case "_Open":
                    Display.SubtitleFile(Open.Subtitle(), lvSrtFile);
                    break;
                case "_Save":
                    Save.Subtitle(lvSrtFile);
                    break;
                case "Save _As":
                    Save.Subtitle(lvSrtFile);
                    break;
                case "New Window":
                    Show();
                    break;
                case "Exit":
                    Close();
                    break;
            }
        }

        private void MenuItem_Video_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;

            switch (menuItem.Header)
            {
                case "Open":
                    Open.Video(mdeVideo);
                    mdeVideo.Visibility = Visibility.Visible;
                    tbtVideo.Visibility = Visibility.Visible;
                    mdeVideo.Play();
                    mdeVideo.Pause();
                    break;
                case "Close":
                    mdeVideo.Source = null;
                    mdeVideo.Visibility = Visibility.Collapsed;
                    tbtVideo.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            
            switch(button.Name)
            {
                case "btnInsertBefore":

                    break;
                case "btnInsertAfter":
                    break;
                case "btnTrim":
                    break;
            }
        }

        private void ButtonControl_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button == btnPlay)
            {
                mdeVideo.Play();
            }
            
            if (button == btnPause)
            {
                mdeVideo.Pause();
            } 
            
            if (button == btnPlayLine)
            {
                //mdeVideo.Position = TimeSpan.Parse(tbxStartTime.Text);
                
                
            }    
        }
    }
}