using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using zhsub.Models;
using zhsub.Windows;

namespace zhsub
{
    public partial class MainWindow : Window
    {
        public static string EditingFileName;

        public readonly Subtitle subtitle;
        private readonly Line line;
        private readonly Video video;

        public MainWindow()
        {
            InitializeComponent();
            //Event.ListViewItem_SelectionChanged(lvEditor, mdeVideo, sliDuration);
            Event();

            subtitle = new Subtitle(this, lvEditor);
            line = new Line(lvEditor);
            video = new Video(dpaVideo, mdeVideo, sliDuration, txbCurrentTime, txbRelativeTime, cbxZoom, lvEditor, txbSubtitle);

            subtitle.New();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            griEditor.Visibility = Visibility.Collapsed;
            griSearch.Visibility = Visibility.Visible;

            //lvSearch.ItemsSource = Bind.Kugeci(Crawl.Kugeci(tbxSearch.Text));
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;

            if (item != null && item.IsSelected)
            {
                var selectedResult = lvSearch.SelectedItems[0] as SearchResult;
                
                griEditor.Visibility = Visibility.Visible;
                griSearch.Visibility = Visibility.Collapsed;
                tbxInput.Text = null;
                //Conversion.LrcToSrt(new WebClient().DownloadString("https://www.kugeci.com/download/lrc/" + selectedResult.ID));

                lvEditor.ItemsSource = null;
                //lvEditor.ItemsSource = List.Srt;
                
            }
        }

        private void Hotkeys()
        {
            var cutListViewItem = new RoutedCommand();
            var copyListViewItem = new RoutedCommand();
            var pasteListViewItem = new RoutedCommand();

            cutListViewItem.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Control));
            copyListViewItem.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));
            pasteListViewItem.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));

            //CommandBindings.Add(new CommandBinding(cutListViewItem, Cut_ListViewItem));
            //CommandBindings.Add(new CommandBinding(copyListViewItem, Copy_ListViewItem));
            //CommandBindings.Add(new CommandBinding(pasteListViewItem, Paste_ListViewItem));
        }

        //private void Cut_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        //{
        //    if (lvEditor.SelectedItem == null) return;

        //    if (lvEditor.SelectedItem is Srt)
        //    {
        //        var srt = (lvEditor.SelectedItem as Srt).StartTime + "," + (lvEditor.SelectedItem as Srt).EndTime + "," + (lvEditor.SelectedItem as Srt).Text;
        //        List.Srt.Remove(lvEditor.SelectedItem as Srt);
        //        Sort.SrtList(lvEditor.SelectedIndex, false);
        //        Sort.SrtList(lvEditor.SelectedIndex, true);
        //        lvEditor.Items.Refresh();
        //        Clipboard.SetText(srt);
        //    }
        //}

        //private void Copy_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        //{
        //    if (lvEditor.SelectedItem == null) return;

        //    if (lvEditor.SelectedItem is Srt)
        //    {
        //        var srt = (lvEditor.SelectedItem as Srt).StartTime + "," + (lvEditor.SelectedItem as Srt).EndTime + "," + (lvEditor.SelectedItem as Srt).Text;
        //        Clipboard.SetText(srt);
        //    }       
        //}

        //private void Paste_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        //{
        //    if (lvEditor.SelectedItem == null) return;

        //    if (lvEditor.SelectedItem is Srt)
        //    {
        //        var srtItems = Clipboard.GetText().Split(',');

        //        string text = null;

        //        for (int i = 2; i <srtItems.Length; i++)
        //        {
        //            text += srtItems[i];
        //        }   
                
        //        List.Srt.Insert(lvEditor.SelectedIndex, new Srt()
        //        {
        //            Index = lvEditor.SelectedIndex + 1,
        //            StartTime = srtItems[0],
        //            EndTime = srtItems[1],
        //            Text = text
        //        });

        //        Sort.SrtList(lvEditor.SelectedIndex + 1, true);
        //        lvEditor.Items.Refresh();
        //    }
        //}

        private void tbxStartTime_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Back || e.Key == Key.Delete) && tbxStartTime.Text.Length == 7)
            {

            }
        }

        private void MenuItem_File_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as MenuItem).Name)
            {
                case "newSubtitle":
                    subtitle.New();
                    break;
                case "openSubtitle":
                    subtitle.Open();
                    break;
                case "saveSubtitle":
                    subtitle.Save();
                    break;
                case "saveAsSubtitle":
                    //Save.Subtitle();
                    break;
                case "newWindow":
                    Show();
                    break;
                case "closeWindow":
                    Close();
                    break;
            }
        }

        private void MenuItem_Video_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as MenuItem).Name)
            {
                case "openVideo":
                    video.Open();
                    break;
                case "closeVideo":
                    video.Close();
                    break;
            }
        }

        private void MenuItem_Line_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as MenuItem).Name)
            {
                case "insertBeforeLine":
                    line.InsertBefore();
                    break;
                case "insertAfterLine":
                    line.InsertAfter();
                    break;
                case "deleteLines":
                    line.Delete();
                    break;
                case "duplicateLines":
                    line.Duplicate();
                    break;
                case "selectAllLines":
                    line.SelectAll();
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btnCut":
                    subtitle.Translate("zh-CN", "zh-TW");
                    break;
            }
        }

        private void Button_Video_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btnPlay":
                    video.Play();
                    break;
                case "btnPause":
                    video.Pause();
                    break;
                case "btnPlayCurrentLine":
                    video.PlayCurrentLine();
                    break;
            }
        }

        private void Event()
        {
            cbxZoom.DropDownClosed += (s, e) =>
            {
                var ratio = double.Parse(cbxZoom.Text.Replace("%", "")) / 100;

                dpaEditor.Width = 575 * ratio;
                mdeVideo.Height = 325 * ratio;
            };

            //lvEditor.SelectionChanged += (s, e) =>
            //{
            //    if (lvEditor.SelectedItem is Models.SubtitleFiles.Srt)
            //    {
            //        var item = lvEditor.SelectedItem as Models.SubtitleFiles.Srt;

            //        var hour = int.Parse(item.StartTime.ToString().Split(':')[0]);
            //        var minute = int.Parse(item.StartTime.ToString().Split(':')[1]);
            //        var second = int.Parse(item.StartTime.ToString().Split(':')[2].Split('.')[0]);
            //        var millisecond = int.Parse(item.StartTime.ToString().Split(':')[2].Split('.')[1]);

            //        mdeVideo.Position = new System.TimeSpan(0, hour, minute, second, millisecond);
            //        sliDuration.Value = mdeVideo.Position.TotalSeconds;
            //    }
            //};

            //sliDuration.ValueChanged += (s, e) =>
            //{
            //    foreach (Models.SubtitleFiles.Srt item in lvEditor.Items)
            //    {
            //        var hour = int.Parse(item.StartTime.ToString().Split(':')[0]);
            //        var minute = int.Parse(item.StartTime.ToString().Split(':')[1]);
            //        var second = int.Parse(item.StartTime.ToString().Split(':')[2].Split('.')[0]);
            //        var millisecond = int.Parse(item.StartTime.ToString().Split(':')[2].Split('.')[1]);

            //        var time = new System.TimeSpan(0, hour, minute, second, millisecond);

            //        if (mdeVideo.Position == time)
            //        {
            //            txbSubtitle.Text = item.Text;
            //            break;
            //        }
            //    }
            //};
        }
    }
}