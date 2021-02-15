using System.Windows;
using System.Windows.Controls;
using zhsub.Models;

namespace zhsub.Commands
{
    /// <summary>
    /// Nơi thực thi các sự kiện click của MenuItem
    /// </summary>
    class MenuItemCommand
    {
        //public static void File(MenuItem menuItem, Window window, ListView listView, GridView gridView)
        //{
        //    //var subtitle = new Subtitle(listView, gridView);

        //    switch (menuItem.Name)
        //    {
        //        case "newSrtFile":
        //            subtitle.New("srt");
        //            Display.FileName(window, MainWindow.EditingFileName);
        //            break;
        //        case "newLrcFile":
        //            New.LrcFile(listView, gridView);
        //            Display.FileName(window, MainWindow.EditingFileName);
        //            break;
        //        case "openSubtitle":
        //            subtitle.Open();
        //            Display.FileName(window, MainWindow.EditingFileName);
        //            break;
        //        case "saveSubtitle":
        //            subtitle.Save();
        //            Display.FileName(window, MainWindow.EditingFileName);
        //            break;
        //        case "saveAsSubtitle":
        //            //Save.Subtitle();
        //            Display.FileName(window, MainWindow.EditingFileName);
        //            break;
        //        case "newWindow":
        //            new MainWindow().Show();
        //            break;
        //        case "closeWindow":
        //            window.Close();
        //            break;
        //    }    
        //}


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
