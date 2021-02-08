using System.Windows;
using System.Windows.Controls;
using zhsub.Models.Files;
using zhsub.Models;

namespace zhsub.Commands
{
    class Insert
    {
        public static void BeforeLine(ListView listView)
        {
            if (listView.SelectedItem == null) return;   

            if (listView.SelectedItem is Srt)
            {
                List.Srt.Insert(listView.SelectedIndex, new Srt()
                {
                    Index = listView.SelectedIndex + 1,
                    StartTime = (listView.SelectedItem as Srt).StartTime,
                    EndTime = (listView.SelectedItem as Srt).StartTime
                });

                Sort.SrtList(listView.SelectedIndex + 1, true);
                listView.Items.Refresh();
            }

            if (listView.SelectedItem is Lrc)
            {
                List.Lrc.Insert(listView.SelectedIndex, new Lrc()
                {
                    Time = "00:00:00", Text = null
                });

                listView.Items.Refresh();
            }
        }

        public static void AfterLine(ListView listView)
        {
            if (listView.SelectedItem == null) return;

            if (listView.SelectedItem is Srt)
            {
                List.Srt.Insert(listView.SelectedIndex + 1, new Srt()
                {
                     Index = listView.SelectedIndex + 2,
                     StartTime = (listView.SelectedItem as Srt).EndTime,
                     EndTime = (listView.SelectedItem as Srt).EndTime
                });

                Sort.SrtList(listView.SelectedIndex + 2, true);
                listView.Items.Refresh();
            }

            if (listView.SelectedItem is Lrc)
            {
                List.Lrc.Insert(listView.SelectedIndex + 1, new Lrc()
                {
                    Time = (listView.SelectedItem as Lrc).Time
                });

                listView.ItemsSource = null;
                listView.ItemsSource = List.Srt;
            }
        }

        public static void NewLine(ListView listView, GridView gridView)
        {
            if (gridView.ColumnHeaderToolTip.Equals("Srt"))
            {
                List.Srt.Clear();

                List.Srt.Add(new Srt()
                {
                    Index = 1,
                    StartTime = "0:00:00.000",
                    EndTime = "0:05:00.000"
                });

                listView.ItemsSource = List.Srt;
            }

            if (gridView.ColumnHeaderToolTip.Equals("Lrc"))
            {
                List.Lrc.Add(new Lrc()
                {
                    Time = "00:00.00"
                });

                listView.ItemsSource = List.Lrc;
            }

            MainWindow.EditingFileName = "Untitled";
        }

        public static void Duplicate(ListView listView)
        {
            if (listView.SelectedItem == null) return;

            if (listView.SelectedItem is Srt)
            {
                List.Srt.Insert(listView.SelectedIndex, listView.SelectedItems as Srt);
                Sort.SrtList(listView.SelectedIndex, true);

                listView.Items.Refresh();
            }
            
            if (listView.SelectedItem is Lrc)
            {
                List.Lrc.Insert(listView.SelectedIndex, listView.SelectedItems as Lrc);

                listView.ItemsSource = null;
                listView.ItemsSource = List.Lrc;
            }    
        }

        public static void Introduction(ListView listView, GridView gridView)
        {
            if (gridView.ColumnHeaderToolTip.Equals("Srt"))
            {
                List.Srt.Add(new Srt()
                {
                    Index = List.Srt.Count,
                    StartTime = List.Srt[^1].EndTime,
                    EndTime = List.Srt[^1].EndTime,
                    Text = "Vietsub: Zither Harp"
                });
            }

            if (gridView.ColumnHeaderToolTip.Equals("Lrc"))
            {
                
            }
        }
    }

    class Command
    {
        public DelegateCommand AddFolderCommand { get; set; }
        public DelegateCommand InsertToBeforeLine { get; set; }

        public Command()
        {
            AddFolderCommand = new DelegateCommand(ExecuteAddFolderCommand, (x) => true);
            InsertToBeforeLine = new DelegateCommand(InsertBefore_Executed, (x) => true);
        }

        public void ExecuteAddFolderCommand(object sender)
        {
            MessageBox.Show("this will be executed on button click later");
        }

        private void InsertBefore_Executed(object sender)
        {
            var item = sender as ListView;

            if (item.SelectedItem == null) return;
        }
    }
}
