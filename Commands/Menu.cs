using System.Windows;
using System.Windows.Controls;

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
                    New.SrtFile(listView, gridView);
                    Insert.NewLine(listView, gridView);
                    Display.FileName(window, MainWindow.EditingFileName);
                    break;
                case "newLrcFile":
                    New.LrcFile(listView, gridView);
                    Insert.NewLine(listView, gridView);
                    Display.FileName(window, MainWindow.EditingFileName);
                    break;
                case "openSubtitle":
                    Open.Subtitle(listView, gridView);
                    Display.FileName(window, MainWindow.EditingFileName);
                    break;
                case "saveSubtitle":
                    Save.Subtitle();
                    Display.FileName(window, MainWindow.EditingFileName);
                    break;
                case "saveAsSubtitle":
                    Save.Subtitle();
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
                case "insertBeforeLines":
                    Insert.BeforeLine(listView);
                    break;
                case "insertAfterLines":
                    Insert.AfterLine(listView);
                    break;
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
