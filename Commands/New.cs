using System.Windows.Controls;
using System.Windows.Data;

namespace zhsub.Commands
{
    class New
    {
        public static void SrtFile(ListView listView, GridView gridView)
        {
            listView.ItemsSource = null;
            listView.Items.Clear();

            if (gridView.Columns.Count > 0) gridView.Columns.Clear();
            
            var columns = new GridViewColumn[]
            {
                new GridViewColumn() { Header = "#", Width = 40 },
                new GridViewColumn() { Header = "Start", Width = 90 },
                new GridViewColumn() { Header = "End", Width = 90 },
                new GridViewColumn() { Header = "CPS", Width = 40 },
                new GridViewColumn() { Header = "Text", Width = 550 }
            };

            var displayMembers = new Binding[]
            {
                new Binding("Index"), new Binding("StartTime"), new Binding("EndTime"), new Binding("CPS"), new Binding("Text")
            };

            for (int i = 0; i < columns.Length; i++)
            {
                gridView.Columns.Add(columns[i]);
                gridView.Columns[i].DisplayMemberBinding = displayMembers[i];
            }

            gridView.ColumnHeaderToolTip = "Srt";
        }

        public static void LrcFile(ListView listView, GridView gridView)
        {
            listView.ItemsSource = null;
            listView.Items.Clear();

            if (gridView.Columns.Count > 0) gridView.Columns.Clear();

            var columns = new GridViewColumn[]
            {
                new GridViewColumn() { Header = "Time", Width = 100 },
                new GridViewColumn() { Header = "Text", Width = 700 }
            };

            var displayMembers = new Binding[]
            {
                new Binding("Time"), new Binding("Text")
            };

            for (int i = 0; i < columns.Length; i++)
            {
                gridView.Columns.Add(columns[i]);
                gridView.Columns[i].DisplayMemberBinding = displayMembers[i];
            }

            gridView.ColumnHeaderToolTip = "Lrc";
        }
    }
}
