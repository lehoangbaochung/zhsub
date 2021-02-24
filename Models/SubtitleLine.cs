using System.ComponentModel;
using System.Windows.Controls;

namespace zhsub.Models
{
    public class SubtitleLine
    {
        #region objects
        public event PropertyChangedEventHandler PropertyChanged;

        private int index;
        private int? cps;
        private string startTime, endTime, text;

        private readonly SubtitleTime time = new SubtitleTime();
        private readonly ListView listView;
        #endregion

        #region properties
        private void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public int Index
        {
            get { return index; }
            set
            {
                if (index != value)
                {
                    index = value;
                    NotifyPropertyChanged("Index");
                }
            }
        }

        public string StartTime
        {
            get { return startTime; }
            set
            {
                if (startTime != value)
                {
                    startTime = value;
                    NotifyPropertyChanged("StartTime");
                }
            }
        }

        public string EndTime
        {
            get { return endTime; }
            set
            {
                if (endTime != value)
                {
                    endTime = value;
                    NotifyPropertyChanged("EndTime");
                }
            }
        }

        public int? CPS
        {
            get { return cps; }
            set
            {
                if (cps != value)
                {
                    cps = value;
                    NotifyPropertyChanged("CPS");
                }
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }
        #endregion

        public SubtitleLine() { }

        public SubtitleLine(ListView listView)
        {
            this.listView = listView;
        }    

        public SubtitleLine(int index, string startTime, string endTime, string text)
        {
            this.index = index;
            this.startTime = startTime;
            this.endTime = endTime;
            this.text = text;

            var duration = time.GetDuration(startTime, endTime);

            if (text == null || duration == 0) return;

            cps = text.Length / duration;
        }

        public void InsertBefore()
        {
            if (listView.SelectedItem == null) return;

            listView.Items.Insert(listView.SelectedIndex, new SubtitleLine()
            {
                Index = listView.SelectedIndex,
                StartTime = (listView.SelectedItem as SubtitleLine).StartTime,
                EndTime = (listView.SelectedItem as SubtitleLine).StartTime
            });

            Sort(listView.SelectedIndex, true);
            listView.Items.Refresh();
        }

        public void InsertAfter()
        {
            if (listView.SelectedItem == null) return;

            listView.Items.Insert(listView.SelectedIndex + 1, new SubtitleLine()
            {
                Index = listView.SelectedIndex + 1,
                StartTime = (listView.SelectedItem as SubtitleLine).EndTime,
                EndTime = (listView.SelectedItem as SubtitleLine).EndTime
            });

            Sort(listView.SelectedIndex + 1, true);
            listView.Items.Refresh();
        }

        public void Delete()
        {
            if (listView.SelectedItem == null) return;

            Sort(listView.SelectedIndex, false);
            listView.Items.Refresh();

            listView.Items.Remove(listView.SelectedItem);
        }

        public void Duplicate()
        {
            if (listView.SelectedItem == null) return;

            listView.Items.Insert(listView.SelectedIndex + 1, listView.SelectedItem);

            Sort(listView.SelectedIndex + 1, true);
            listView.Items.Refresh();
        }

        public void SelectAll()
        {
            listView.SelectAll();
        }

        public void Trim()
        {
            foreach (SubtitleLine line in listView.Items)
            {
                line.Text = line.Text.Trim();
            }
        }

        public void Sort(int startIndex, bool IsDown)
        {
            if (IsDown)
            {
                for (int i = startIndex; i < listView.Items.Count; i++)
                {
                    (listView.Items[i] as SubtitleLine).Index += 1;
                }
            }
            else
            {
                for (int i = startIndex; i < listView.Items.Count; i++)
                {
                    (listView.Items[i] as SubtitleLine).Index -= 1;
                }
            }    
        }    

        public void Find(string keyword)
        {
            foreach (SubtitleLine line in listView.Items)
            {
                if (line.Text.Contains(keyword))
                {
                    listView.SelectedItem = listView.Items[line.Index - 1];
                }    
            }    
        }    
    }
}
