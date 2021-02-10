using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using zhsub.Models;

namespace zhsub.Models.SubtitleFiles
{
    class Lrc : Subtitle
    {
        private object _time, _text;

        public static List<Lrc> List = new List<Lrc>();

        public object Time { get; set; }
        public object Text { get; set; }

        public Lrc() { }

        public Lrc(ListView listView, GridView gridView) : base(listView, gridView)
        {
            listView.ItemsSource = new List<object>() { new Lrc("00:00:00", null) };

            gridView.ColumnHeaderToolTip = "lrc";

            _gridViewColumns = new GridViewColumn[]
            {
                new GridViewColumn() { Header = "#", Width = 100 },
                new GridViewColumn() { Header = "Text", Width = 710 }
            };

            _displayMembers = new Binding[]
            {
                new Binding("Time"), new Binding("Text")
            };
        }

        public Lrc(object time, object text)
        {
            _time = time;
            _text = text;
        }
    }
}