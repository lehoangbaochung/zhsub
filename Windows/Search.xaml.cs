using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using zhsub.Models;

namespace zhsub.Windows
{
    public partial class Search : Window
    {
        #region properties
        readonly List<SearchResult> searchResults;
        readonly HttpClient httpClient;
        #endregion

        public Search()
        {
            InitializeComponent();

            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.kugeci.com/")
            };

            searchResults = new List<SearchResult>();
        }

        void Crawl(string keyword)
        {
            string innerHtml = httpClient.GetStringAsync("search?q=" + keyword).Result;

            var regex = Regex.Match(innerHtml, @"<tbody>(.*?)</tbody>", RegexOptions.Singleline).Value;

            var results = Regex.Matches(regex.ToString(), @"(?=<tr>).*?(?=</tr>)", RegexOptions.Singleline);

            foreach (var result in results)
            {
                var items = Regex.Matches(result.ToString(), @"(?=<td.*?>).*?(?=</td>)", RegexOptions.Singleline);

                var time = items[0].ToString().Substring(items[0].ToString().LastIndexOf('>') + 1);
                var song = Regex.Match(items[1].ToString(), @"(?=<a.*?>).*?(?=</a>)", RegexOptions.Singleline).Value;
                var artist = Regex.Match(items[2].ToString(), @"(?=<a.*?>).*?(?=</a>)", RegexOptions.Singleline).Value;
                var view = items[3].ToString().Substring(items[3].ToString().LastIndexOf('>') + 1);
                var detail = Regex.Match(items[4].ToString(), @"(?=<a.*?).*?(?="">)", RegexOptions.Singleline).Value;
                var id = detail.Substring(detail.LastIndexOf('/') + 1);

                var val = new SearchResult()
                {
                    ID = id,
                    Song = song.Substring(song.LastIndexOf('>') + 1),
                    Artist = artist.Substring(artist.LastIndexOf('>') + 1),
                    Time = time.Trim(),
                    View = view
                };

                searchResults.Add(val);
            }

            searchResults.Sort();
            lvResult.ItemsSource = searchResults;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            tiSearch.IsSelected = true;
            searchResults.Clear();
            lvResult.ItemsSource = null;

            Crawl(tbxSearch.Text);

            tbxReview.Width = 0;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;

            if (item != null && item.IsSelected)
            {
                var selectedResult = lvResult.SelectedItems[0] as SearchResult;

                if (selectedResult == null) return;

                tbxReview.Text = new WebClient().DownloadString("https://www.kugeci.com/download/txt/" + selectedResult.ID);
                svReview.Visibility = Visibility.Visible;
            }
        }
    }
}
