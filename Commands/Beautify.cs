using System.Windows.Controls;
using zhsub.Models.Files;

namespace zhsub.Features
{
    class Beautify
    {
        public static void TrimLines(ListView listView)
        {
            if (listView.Items.SourceCollection is Srt)
            {
                foreach (Srt item in listView.Items)
                {
                    item.Text = item.Text.ToString().Trim();
                }
            }

            if (listView.Items.SourceCollection is Lrc)
            {
                foreach (Lrc item in listView.Items)
                {
                    item.Text = item.Text.ToString().Trim();
                }
            }
        }

        public static void TimeFormat(ListView listView)
        {
            foreach (Srt item in listView.Items)
            {
                if (item.StartTime.ToString().Split('.')[1].Length > 2) // 0:00:01.023 -> 0:00:01.02
                {
                    item.StartTime = item.StartTime.ToString().Split('.')[0] + item.StartTime.ToString().Split('.')[1].Substring(0, 2);
                }

                if (item.EndTime.ToString().Split('.')[1].Length > 2) // 0:00:01.023 -> 0:00:01.02
                {
                    item.EndTime = item.EndTime.ToString().Split('.')[0] + item.EndTime.ToString().Split('.')[1].Substring(0, 2);
                }
            }
        }
    }
}
