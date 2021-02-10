using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace zhsub.Models.SubtitleFiles
{
    class Srt : Subtitle, INotifyPropertyChanged
    {
        private object _index, _startTime, _endTime, _text;

        private void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public object Index
        {
            get { return _index; }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    NotifyPropertyChanged("Index");
                }
            }
        }

        public object StartTime
        {
            get { return _startTime; }
            set
            {
                if (_startTime != value)
                {
                    _startTime = value;
                    NotifyPropertyChanged("StartTime");
                }
            }
        }

        public object EndTime
        {
            get { return _endTime; }
            set
            {
                if (_endTime != value)
                {
                    _endTime = value;
                    NotifyPropertyChanged("EndTime");
                }
            }
        }

        public object Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Srt() { }

        public Srt(ListView listView, GridView gridView) : base(listView, gridView)
        {
            New();
        }

        public Srt(object index, object startTime, object endTime, object text)
        {
            _index = index;
            _startTime = startTime;
            _endTime = endTime;
            _text = text;
        }

        protected override void Create()
        {
            _listView.ItemsSource = new List<object>() { new Srt(1, "0:00:00.000", "0:00:05.000", null) };

            _gridView.ColumnHeaderToolTip = "srt";

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
        }

        public void Open(string fileData, List<object> list)
        {
            list = new List<object>();

            var lineArray = fileData.Split("\r\n\r\n");

            foreach (var line in lineArray)
            {
                var itemArray = line.Split("\r\n");

                if (itemArray.Length < 3) return;

                _text = itemArray[2];

                if (itemArray.Length > 3)
                {
                    for (int i = 3; i < itemArray.Length; i++)
                    {
                        _text += "\n" + itemArray[i];
                    }
                }

                var srt = new Srt()
                {
                    Index = itemArray[0],
                    StartTime = itemArray[1].Substring(0, itemArray[1].IndexOf('-') - 1).Replace(',', '.'),
                    EndTime = itemArray[1][(itemArray[1].IndexOf('>') + 2)..].Replace(',', '.'),
                    Text = _text
                };

                list.Add(srt); 
            }
        }

        public static void Save(string fileName, List<object> list)
        {
            string srt = null;

            foreach (Srt item in list)
            {
                if (item.Text == null) item.Text = " ";

                srt += $"{ item.Index }\r\n" +
                    $"{ item.StartTime.ToString().Replace('.', ',') } --> { item.EndTime.ToString().Replace('.', ',') }\r\n" +
                    $"{ item.Text }\r\n\r\n";
            }

            System.IO.File.WriteAllText(fileName, srt);
        }
    }
}
