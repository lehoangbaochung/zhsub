using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace zhsub.Commands
{
    class Crawl
    {
        public static MatchCollection Kugeci(string keyword)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.kugeci.com/")
            };

            string result = httpClient.GetStringAsync("search?q=" + keyword).Result;

            var value = Regex.Match(result, @"<tbody>(.*?)</tbody>", RegexOptions.Singleline).Value;

            var results = Regex.Matches(value.ToString(), @"(?=<tr>).*?(?=</tr>)", RegexOptions.Singleline);

            return results;
        }
    }
}
