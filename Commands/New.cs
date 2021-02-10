using System.Windows.Controls;
using System.Windows.Data;

namespace zhsub.Commands
{
    class New
    {
        private readonly ListView _listView;
        private readonly GridView _gridView;
        private GridViewColumn[] _gridViewColumns;
        private Binding[] _displayMembers;

        public New(ListView listView, GridView gridView)
        {
            _listView = listView;
            _gridView = gridView;

            _listView.ItemsSource = null;

            _listView.Items.Clear();
            _gridView.Columns.Clear();  
        }

        private void Binding()
        {
            for (int i = 0; i < _gridViewColumns.Length; i++)
            {
                _gridView.Columns.Add(_gridViewColumns[i]);
                _gridView.Columns[i].DisplayMemberBinding = _displayMembers[i];
            }
        }

        public void Srt()
        {
            _gridView.ColumnHeaderToolTip = "Srt";

            _gridViewColumns = new GridViewColumn[]
            {
                new GridViewColumn() { Header = "#", Width = 40 },
                new GridViewColumn() { Header = "Start", Width = 90 },
                new GridViewColumn() { Header = "End", Width = 90 },
                new GridViewColumn() { Header = "Text", Width = 590 }
            };

            _displayMembers = new Binding[]
            {
                new Binding("Index"), new Binding("StartTime"), new Binding("EndTime"), new Binding("Text")
            };

            Binding();
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
