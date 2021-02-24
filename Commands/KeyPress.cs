using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using zhsub.Models;

namespace zhsub.Commands
{
    class KeyPress
    {
        protected virtual void Cut_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender as ListView == null) return;

            var item = (sender as ListView).SelectedItem as SubtitleLine;

            var text = $"{ item.StartTime };{ item.EndTime };{ item.Text }";

            Clipboard.SetText(text);

            (sender as ListView).Items.Remove((sender as ListView).SelectedItem);
        }


        protected void Copy_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Paste_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
