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
        //static string PrevSubtitleFileFormat = ".*";

        //public static void subtitle(listview listview, gridview gridview)
        //{
        //    var openfiledialog = new openfiledialog() { filter = "subrip files (*.srt)|*.srt|lyric files (*.lrc)|*.lrc" };

        //    if (openfiledialog.showdialog() == false) return;

        //    listview.itemssource = null;
        //    listview.items.clear();

        //    var filename = openfiledialog.filename;

        //    if (filename.endswith(".srt"))
        //    {
        //        if (!filename.endswith(prevsubtitlefileformat)) //new.srtfile(listview, gridview);

        //            read.srtfile(file.readalltext(filename));
        //        listview.itemssource = srt.list;
        //    }

        //    if (filename.endswith(".lrc"))
        //    {
        //        if (!filename.endswith(prevsubtitlefileformat)) new.lrcfile(listview, gridview);

        //        read.lrcfile(system.io.file.readalltext(filename));
        //        listview.itemssource = list.lrc;
        //    }

        //    prevsubtitlefileformat = filename[filename.lastindexof('.')..];
        //    mainwindow.editingfilename = filename;
        //}

        public static void Video(MediaElement mediaElement, DockPanel dockPanel)
        {
            var openFileDialog = new OpenFileDialog() { Filter = "Video files (*.mp4,*.3gp,*.avi)|*.mp4;*.3gp;*.avi" };

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
