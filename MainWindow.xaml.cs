using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using zhsub.Models;
using zhsub.Commands;
using zhsub.Features;
using System.Net;

namespace zhsub
{
    public partial class MainWindow : Window
    {
        public static string EditingFileName;

        public MainWindow()
        {
            InitializeComponent();
            //Event.ListViewItem_SelectionChanged(lvEditor, mdeVideo, sliDuration);
            Hotkeys();
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as ListViewItem;

            if (item != null && item.IsSelected)
            {
                //var selectedResult = lvSearch.SelectedItems[0] as SearchResult;
                //MessageBox.Show("Download " + selectedResult.ID + " successfully!", "Download", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnBookmark_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
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
            MenuItemCommand.File(sender as MenuItem, this, lvEditor, gvEditor);
        }

        private void MenuItem_Video_Click(object sender, RoutedEventArgs e)
        {
            MenuItemCommand.Video(sender as MenuItem, mdeVideo, dpaVideo);
        }

        private void MenuItem_Subtitle_Click(object sender, RoutedEventArgs e)
        {
            MenuItemCommand.Subtitle(sender as MenuItem, lvEditor);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            switch(button.Name)
            {
                case "btnInsertBefore":

                    break;
                case "btnInsertAfter":
                    break;
                case "btnTrim":
                    break;
            }
        }

        private void ButtonControl_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button == btnPlay)
            {
                Video.Timer(mdeVideo, sliDuration, lvEditor, txbSubtitle);
                Video.Play(mdeVideo);
                sliDuration.IsEnabled = false;
            }
            
            if (button == btnPause)
            {
                Video.Pause(mdeVideo);
                sliDuration.IsEnabled = true;
            } 
            
            if (button == btnPlayLine)
            {
                //mdeVideo.Position = TimeSpan.Parse(tbxStartTime.Text);
                
                
            }    
        }
    }
}