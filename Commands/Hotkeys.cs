using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace zhsub.Commands
{
    class Hotkeys : MainWindow
    {
        public Hotkeys()
        {
            var copyListViewItem = new RoutedCommand();

            copyListViewItem.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));

            CommandBindings.Add(new CommandBinding(copyListViewItem, Copy_ListViewItem));
        }

        public static void Copy_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public static void Pasted_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        {
            var item = sender as ListViewItem;

            if (item == null) return;

            Clipboard.GetDataObject();
        }
    }
}
