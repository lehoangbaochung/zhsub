using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace zhsub.Models
{
    public class OnlineSubtitle
    {
        private readonly ListView _listView;

        private HttpClient httpClient;
        private MatchCollection matchCollection;
        private SubtitleTime subtitleTime;

        public string ID { get; set; }
        public string Song { get; set; }
        public string Artist { get; set; }
        public string Lyric { get; set; }

        public OnlineSubtitle() { }

        public OnlineSubtitle(ListView listView)
        {
            _listView = listView;
        }

        public void Search(string keyword)
        {
            _listView.Items.Clear();

            if (keyword == "") return;

            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.kugeci.com/")
            };

            string html = httpClient.GetStringAsync("search?q=" + keyword).Result;

            var content = Regex.Match(html, @"<tbody>(.*?)</tbody>", RegexOptions.Singleline).Value;

            matchCollection = Regex.Matches(content, @"(?=<tr>).*?(?=</tr>)", RegexOptions.Singleline);

            foreach (var item in matchCollection)
            {
                var items = Regex.Matches(item.ToString(), @"(?=<td.*?>).*?(?=</td>)", RegexOptions.Singleline);

                var item1 = Regex.Match(items[1].ToString(), @"(?=<a.*?>).*?(?=</a>)", RegexOptions.Singleline).Value;
                var item2 = Regex.Match(items[2].ToString(), @"(?=<a.*?>).*?(?=</a>)", RegexOptions.Singleline).Value;
                var item3 = Regex.Match(items[4].ToString(), @"(?=<a.*?).*?(?="">)", RegexOptions.Singleline).Value;

                var result = new OnlineSubtitle()
                {
                    ID = item3[(item3.LastIndexOf('/') + 1)..],
                    Song = item1[(item1.LastIndexOf('>') + 1)..],
                    Artist = item2[(item2.LastIndexOf('>') + 1)..]
                };

                _listView.Items.Add(result);
            }
        }

        public string GetCount()
        {
            return "Found " + _listView.Items.Count + " result(s)";
        }

        public void GetReview(Button button)
        {
            if (_listView.SelectedItem == null) return;

            var text = "https://www.kugeci.com/download/lrc/" + (_listView.SelectedItem as OnlineSubtitle).ID;

            button.ToolTip = new WebClient().DownloadString(text);
        }

        public void Download(ListView listView)
        {
            var selectedResult = _listView.SelectedItem as OnlineSubtitle;

            var text = new WebClient().DownloadString("https://www.kugeci.com/download/lrc/" + selectedResult.ID).Trim();

            var lineArray = text.Split("\n");

            subtitleTime = new SubtitleTime();

            int index = 1;

            for (int i = 0; i < lineArray.Length - 1; i++)
            {
                var subtitle = new SubtitleLine()
                {
                    Index = index++,
                    StartTime = subtitleTime.Format(lineArray[i][1..lineArray[i].IndexOf(']')]),
                    EndTime = subtitleTime.Format(lineArray[i + 1][1..lineArray[i + 1].IndexOf(']')]),
                    Text = lineArray[i][(lineArray[i].IndexOf(']') + 1)..].Trim()
                };
                
                listView.Items.Add(subtitle);
            }

            var sub = new SubtitleLine()
            {
                Index = lineArray.Length,
                StartTime = subtitleTime.Format(lineArray[^1][1..lineArray[^1].IndexOf(']')]),
                EndTime = subtitleTime.Format(lineArray[^1][1..lineArray[^1].IndexOf(']')]),
                Text = lineArray[^1][(lineArray[^1].IndexOf(']') + 1)..].Trim()
            };

            listView.Items.Add(sub);
        }
    }
}
