using System;
using System.ComponentModel;

namespace zhsub.Models.Files
{
    class Srt : INotifyPropertyChanged
    {
        private object startTime, endTime, text, cps;

        public object Index { get; set; }

        public object StartTime
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

        public object EndTime
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

        public object Text
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

        public object CPS
        {
            get { return cps; }

            private set 
            {
                value = DateTime.Parse(EndTime.ToString()).Subtract(DateTime.Parse(StartTime.ToString())).TotalSeconds;

                if ((int)value > 0)
                    cps = Text.ToString().Length / (int)value;
                else
                    cps = 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
