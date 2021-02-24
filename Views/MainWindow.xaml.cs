using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using zhsub.Models;

namespace zhsub.Views
{
    public partial class MainWindow : Window
    {
        #region private objects
        private readonly Subtitle subtitle;
        private readonly SubtitleLine line;
        private readonly SubtitleTime time;
        private readonly Video video;

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            new ViewModels.Hotkey(this);
            subtitle = new Subtitle(this);
            time = new SubtitleTime();
            line = new SubtitleLine(lvEditor);
            video = new Video(this);
            

            subtitle.New();
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
                    subtitle.Save();
                    break;
                case "newWindow":
                    new MainWindow().Show();
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

        private void MenuItem_Timing_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as MenuItem).Name)
            {
                case "shiftTime":
                    new TimingWindow(lvEditor).ShowDialog();
                    break;
            }
        }

        private void ToolBar_Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btnNewFile":
                    subtitle.New();
                    break;
                case "btnOpenFile":
                    subtitle.Open();
                    break;
                case "btnSaveFile":
                    subtitle.Save();
                    break;
                case "btnOpenVideo":
                    video.Open();
                    break;
                case "btnSearchSubtitle":
                    new SearchWindow(this).ShowDialog();
                    break;
            }
        }

        private void ActionBar_Button_Click(object sender, RoutedEventArgs e)
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
                case "btnReplay":
                    video.Replay();
                    break;
                case "btnAutoSeek":
                    break;
            }
        }

        private void TextBox_TimeInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbxStartHour.Text != "" && tbxStartMinute.Text != "" && tbxStartSecond.Text != "" && tbxStartMilliSecond.Text != "")
            {
                tbxStartTime.Text = $"{ tbxStartHour.Text }:{ tbxStartMinute.Text }:{ tbxStartSecond.Text }.{ tbxStartMilliSecond.Text }";
            }

            if (tbxEndHour.Text != "" && tbxEndMinute.Text != "" && tbxEndSecond.Text != "" && tbxEndMilliSecond.Text != "")
            {
                tbxEndTime.Text = $"{ tbxEndHour.Text }:{ tbxEndMinute.Text }:{ tbxEndSecond.Text }.{ tbxEndMilliSecond.Text }";
            } 
            

        }

        private void TextBox_StartTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbxStartTime.Text == "") return;

            var startTime = time.Split(tbxStartTime.Text);
            
            tbxStartHour.Text = startTime[0];
            tbxStartMinute.Text = startTime[1];
            tbxStartSecond.Text = startTime[2];
            tbxStartMilliSecond.Text = startTime[3];
        }

        private void TextBox_EndTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbxEndTime.Text == "") return;

            var endTime = time.Split(tbxEndTime.Text);

            tbxEndHour.Text = endTime[0];
            tbxEndMinute.Text = endTime[1];
            tbxEndSecond.Text = endTime[2];
            tbxEndMilliSecond.Text = endTime[3];
        }

        private void TextBox_TextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var item = lvEditor.SelectedItem as SubtitleLine;
            var duration = time.GetDuration(tbxStartTime.Text, tbxEndTime.Text);

            if (item == null || duration == 0) return;

            item.CPS = tbxInput.Text.Length / duration;
            tbxCPS.Text = item.CPS.ToString();
            lvEditor.Items.Refresh();
        }

        private void TextBox_FindText_TextChanged(object sender, TextChangedEventArgs e)
        {
            line.Find(tbxFind.Text);
        }    

        public void SetTitle(string fileName)
        {
            Title = $"{ fileName } - Zither Harp Subtitles 1.0.0";

            var isLrcFile = fileName.EndsWith("lrc");

            tbxCPS.IsEnabled = !isLrcFile;
            tbxStartHour.IsEnabled = !isLrcFile;
            tbxEndHour.IsEnabled = !isLrcFile;
            tbxEndMinute.IsEnabled = !isLrcFile;
            tbxEndSecond.IsEnabled = !isLrcFile;
            tbxEndMilliSecond.IsEnabled = !isLrcFile;
            tbxDurationHour.IsEnabled = !isLrcFile;
            tbxDurationMinute.IsEnabled = !isLrcFile;
            tbxDurationSecond.IsEnabled = !isLrcFile;
            tbxDurationMilliSecond.IsEnabled = !isLrcFile;

            tbxCPS.Text = null;
            tbxStartHour.Text = null;
            tbxEndHour.Text = null;
            tbxEndMinute.Text = null;
            tbxEndSecond.Text = null;
            tbxEndMilliSecond.Text = null;
            tbxDurationHour.Text = null;
            tbxDurationMinute.Text = null;
            tbxDurationSecond.Text = null;
            tbxDurationMilliSecond.Text = null;
        }

        private void TextBox_Time_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key > 33 && (int)e.Key < 44) || ((int)e.Key > 73 && (int)e.Key < 84) 
                || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Tab)
            {
                e.Handled = false; 
            }
            else
                e.Handled = true;
        }

        private void lvEditor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnAutoSeek.IsChecked == true)
            {
                mdeVideo.Position = new System.TimeSpan(0,
                    int.Parse(tbxStartHour.Text),
                    int.Parse(tbxStartMinute.Text),
                    int.Parse(tbxStartSecond.Text),
                    int.Parse(tbxStartMilliSecond.Text));
                video.Pause();
            }

            if (lvEditor.SelectedItem == null || tbxStartTime.Text == "" || tbxEndTime.Text == "") return;

            var durationTime = time.Subtract(tbxStartTime.Text, tbxEndTime.Text);

            tbxDurationHour.Text = durationTime[0];
            tbxDurationMinute.Text = durationTime[1];
            tbxDurationSecond.Text = durationTime[2];
            tbxDurationMilliSecond.Text = durationTime[3];
        }
    }
}