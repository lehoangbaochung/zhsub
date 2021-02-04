using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using zhsub.Models;

namespace zhsub.Commands
{
    class Bind
    {       
        public static List<SearchResult> Kugeci(MatchCollection matchCollection)
        {
            foreach (var item in matchCollection)
            {
                var items = Regex.Matches(item.ToString(), @"(?=<td.*?>).*?(?=</td>)", RegexOptions.Singleline);

                //var time = items[0].ToString().Substring(items[0].ToString().LastIndexOf('>') + 1).Trim();
                //var view = items[3].ToString().Substring(items[3].ToString().LastIndexOf('>') + 1);

                var item1 = Regex.Match(items[1].ToString(), @"(?=<a.*?>).*?(?=</a>)", RegexOptions.Singleline).Value;
                var item2 = Regex.Match(items[2].ToString(), @"(?=<a.*?>).*?(?=</a>)", RegexOptions.Singleline).Value;
                var item3 = Regex.Match(items[4].ToString(), @"(?=<a.*?).*?(?="">)", RegexOptions.Singleline).Value;

                var result = new SearchResult()
                {
                    ID = item3[(item3.LastIndexOf('/') + 1)..],
                    Song = item1[(item1.LastIndexOf('>') + 1)..],
                    Artist = item2[(item2.LastIndexOf('>') + 1)..]
                };

                result.Lyric = new WebClient().DownloadString("https://www.kugeci.com/download/txt/" + result.ID).Trim();

                if (result.Lyric.ToString().Length > 200)
                    result.Lyric = result.Lyric.ToString().Substring(0, 200) + "...";

                List.SearchResult.Add(result);
            }

            return List.SearchResult.OrderBy(s => s.Song).ToList();
        }    
    }
}
