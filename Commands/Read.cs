using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using zhsub.Models.Files;

namespace zhsub.Commands
{
    class Read
    {
        public static void SrtFile(string text, ref ListView listView)
        {
            var sr = new StringReader(text);

            string line;

            var list = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }

            for (int i = 0; i < list.Count; i += 4)
            {
                var srt = new Srt()
                {
                    Index = list[i],
                    StartTime = list[i + 1].Substring(0, list[i + 1].IndexOf('-') - 1).Replace(',', '.'),
                    EndTime = list[i + 1][(list[i + 1].IndexOf('>') + 2)..].Replace(',', '.'),
                    Text = list[i + 2]
                };

                listView.Items.Add(srt);
            }
        }
    }
}
