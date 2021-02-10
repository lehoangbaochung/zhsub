using System.Windows;
using System.Windows.Controls;
using zhsub.Models.SubtitleFiles;
using zhsub.Models;

namespace zhsub.Commands
{
    /// <summary>
    /// Nơi thực thi các sự kiện click của MenuItem
    /// </summary>
    class MenuItemCommand
    {
        public static void File(MenuItem menuItem, Window window, ListView listView, GridView gridView)
        {
            switch (menuItem.Name)
            {
                case "newSrtFile":
                    new Srt(listView, gridView, "", ""); 
                    Display.FileName(window, MainWindow.EditingFileName);
                    break;
                case "newLrcFile":
                    New.LrcFile(listView, gridView);
                    //Insert.NewLine(listView, gridView);
                    Display.FileName(window, MainWindow.EditingFileName);
                    break;
                case "openSubtitle":
                    new Subtitle(listView, gridView).Open();
                    Display.FileName(window, MainWindow.EditingFileName);
                    break;
                case "saveSubtitle":
                    new Subtitle(listView, gridView).Save();
                    Display.FileName(window, MainWindow.EditingFileName);
                    break;
                case "saveAsSubtitle":
                    //Save.Subtitle();
                    Display.FileName(window, MainWindow.EditingFileName);
                    break;
                case "newWindow":
                    new MainWindow().Show();
                    break;
                case "closeWindow":
                    window.Close();
                    break;
            }    
        }

        public static void Subtitle(MenuItem menuItem, ListView listView)
        {
            switch (menuItem.Name)
            {
                //case "insertBeforeLines":
                //    Insert.BeforeLine(listView);
                //    break;
                //case "insertAfterLines":
                //    Insert.AfterLine(listView);
                //    break;
            }
        }

        public static void Video(MenuItem menuItem, MediaElement mediaElement, DockPanel dockPanel)
        {
            switch (menuItem.Name)
            {
                case "openVideo":
                    Open.Video(mediaElement, dockPanel);
                    break;
            }    
        }
    }
}
