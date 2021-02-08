using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using zhsub.Models;

namespace zhsub.Commands
{
    class Open
    {
        static string PrevSubtitleFileFormat = ".*";

        public static void Subtitle(ListView listView, GridView gridView)
        {
            var openFileDialog = new OpenFileDialog() { Filter = "SubRip files (*.srt)|*.srt|Lyric files (*.lrc)|*.lrc" };

            if (openFileDialog.ShowDialog() == false) return;

            listView.ItemsSource = null;
            listView.Items.Clear();

            var fileName = openFileDialog.FileName;

            if (fileName.EndsWith(".srt"))
            {
                if (!fileName.EndsWith(PrevSubtitleFileFormat)) New.SrtFile(listView, gridView);

                Read.SrtFile(File.ReadAllText(fileName));
                listView.ItemsSource = List.Srt;
            }

            if (fileName.EndsWith(".lrc"))
            {
                if (!fileName.EndsWith(PrevSubtitleFileFormat)) New.LrcFile(listView, gridView);

                Read.LrcFile(File.ReadAllText(fileName));
                listView.ItemsSource = List.Lrc;
            }

            PrevSubtitleFileFormat = fileName[fileName.LastIndexOf('.')..];
            MainWindow.EditingFileName = fileName;
        }

        public static void Video(MediaElement mediaElement, DockPanel dockPanel)
        {
            var openFileDialog = new OpenFileDialog() { Filter = "Video files (*.mp4,*.flv,*.3gp,*.avi)|*.mp4;*.flv;*.3gp;*.avi" };

            if (openFileDialog.ShowDialog() == false) return;

            #region display video
            mediaElement.Source = new Uri(openFileDialog.FileName);
            mediaElement.Visibility = Visibility.Visible;
            dockPanel.Visibility = Visibility.Visible;

            mediaElement.Play();
            mediaElement.Pause();
            #endregion
        }
    }
}
