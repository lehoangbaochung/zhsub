using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using zhsub.Models;
using zhsub.Models.Files;
using zhsub.Models.Lists;
using zhsub.Boundaries;
using System.Collections.ObjectModel;

namespace zhsub
{
    public partial class MainWindow : Window
    {
        #region properties
        readonly List<SearchResult> searchResults;
        readonly HttpClient httpClient;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.kugeci.com/")
            };

            searchResults = new List<SearchResult>();
            ViewModel.SrtList = new ObservableCollection<Srt>();
            lvEditor.ItemsSource = ViewModel.SrtList;
            DataContext = this;
        }

        void Crawl(string keyword)
        {
            string innerHtml = httpClient.GetStringAsync("search?q=" + keyword).Result;

            var regex = Regex.Match(innerHtml, @"<tbody>(.*?)</tbody>", RegexOptions.Singleline).Value;

            var results = Regex.Matches(regex.ToString(), @"(?=<tr>).*?(?=</tr>)", RegexOptions.Singleline);

            txbResultCount.Text = "Found " + results.Count + " result(s)";

            if (results == null) return; // ko tìm thấy kết quả phù hợp

            foreach (var result in results)
            {
                var items = Regex.Matches(result.ToString(), @"(?=<td.*?>).*?(?=</td>)", RegexOptions.Singleline);

                var time = items[0].ToString().Substring(items[0].ToString().LastIndexOf('>') + 1);
                var song = Regex.Match(items[1].ToString(), @"(?=<a.*?>).*?(?=</a>)", RegexOptions.Singleline).Value;
                var artist = Regex.Match(items[2].ToString(), @"(?=<a.*?>).*?(?=</a>)", RegexOptions.Singleline).Value;
                var view = items[3].ToString().Substring(items[3].ToString().LastIndexOf('>') + 1);
                var link = Regex.Match(items[4].ToString(), @"(?=<a.*?).*?(?="">)", RegexOptions.Singleline).Value;

                var val = new SearchResult()
                {
                    ID = link.Substring(link.LastIndexOf('/') + 1),
                    Song = song.Substring(song.LastIndexOf('>') + 1),
                    Artist = artist.Substring(artist.LastIndexOf('>') + 1),
                    Time = time.Trim(),
                    View = view
                };

                val.Lyric = new WebClient().DownloadString("https://www.kugeci.com/download/txt/" + val.ID).Trim();

                if (val.Lyric.ToString().Length > 200)
                    val.Lyric = val.Lyric.ToString().Substring(0, 200) + "...";

                searchResults.Add(val);
            }

            lvSearch.ItemsSource = searchResults.OrderByDescending(s => s.Time).ToList();
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as ListViewItem;

            if (item != null && item.IsSelected)
            {
                var selectedResult = lvSearch.SelectedItems[0] as SearchResult;
                MessageBox.Show("Download " + selectedResult.ID + " successfully!", "Download", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnBookmark_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            tiSearch.IsSelected = true;
            searchResults.Clear();
            lvSearch.ItemsSource = null;

            Crawl(tbxSearch.Text);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;

            if (item != null && item.IsSelected)
            {
                var selectedResult = lvSearch.SelectedItems[0] as SearchResult;

                tiEditor.IsSelected = true;
                tbxInput.Text = null;
                ViewModel.SrtList.Clear();

                Display.SrtFile(new WebClient().DownloadString("https://www.kugeci.com/download/lrc/" + selectedResult.ID));
            }
        }

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (lvEditor.SelectedItem != null)
            //{
            //    tbxInput.Text = (lvEditor.SelectedItem as Srt).Text.ToString();
            //    tbxStartTime.Text = (lvEditor.SelectedItem as Srt).StartTime.ToString();
            //    tbxEndTime.Text = (lvEditor.SelectedItem as Srt).EndTime.ToString();
            //}
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (lvEditor != null && lvEditor.SelectedItem != null)
            { 
                if (textBox == tbxInput) (lvEditor.SelectedItem as Srt).Text = tbxInput.Text;

                if (textBox == tbxStartTime) (lvEditor.SelectedItem as Srt).StartTime = tbxStartTime.Text;

                if (textBox == tbxEndTime) (lvEditor.SelectedItem as Srt).EndTime = tbxEndTime.Text;
            }
        }

        private void tbxStartTime_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Back || e.Key == Key.Delete) && tbxStartTime.Text.Length == 7)
            {

            }
        }
    }
}