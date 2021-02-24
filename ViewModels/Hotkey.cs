using System.Windows;
using System.Windows.Input;
using zhsub.Models;
using zhsub.Views;

namespace zhsub.ViewModels
{
    public class Hotkey
    {
        private RoutedCommand cut, copy, paste, pasteOver;
        private MainWindow mainWindow;
        private SubtitleLine line;

        public Hotkey(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            line = new SubtitleLine(mainWindow.lvEditor);

            Initialize();
            AddInputGesture();
            AddCommandBinding();   
        }

        private void Initialize()
        {
            cut = new RoutedCommand();
            copy = new RoutedCommand();
            paste = new RoutedCommand();
            pasteOver = new RoutedCommand();
        }

        private void AddInputGesture()
        {
            cut.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Control));
            copy.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));
            paste.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));
            pasteOver.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control | ModifierKeys.Shift));
        }

        private void AddCommandBinding()
        {
            mainWindow.CommandBindings.Add(new CommandBinding(cut, Cut_ListViewItem));
            mainWindow.CommandBindings.Add(new CommandBinding(copy, Copy_ListViewItem));
            mainWindow.CommandBindings.Add(new CommandBinding(paste, Paste_ListViewItem));
        }

        private void Cut_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        {
            Copy_ListViewItem(sender, e);

            line.Sort(mainWindow.lvEditor.SelectedIndex, false);
            mainWindow.lvEditor.Items.Remove(mainWindow.lvEditor.SelectedItem);

            if (mainWindow.lvEditor.Items.Count == 0)
                mainWindow.lvEditor.Items.Add(new SubtitleLine(1, "00:00:00.000", "00:00:05.000", ""));
        }

        private void Copy_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        {
            if (mainWindow.lvEditor.SelectedItem == null) return;

            var line = mainWindow.lvEditor.SelectedItem as SubtitleLine;

            var item = $"{ line.StartTime };{ line.EndTime };{ line.Text }";

            Clipboard.SetText(item);
        }

        private void Paste_ListViewItem(object sender, ExecutedRoutedEventArgs e)
        {
            if (mainWindow.lvEditor.SelectedItem == null) return;

            var lineArray = Clipboard.GetText().Split(';');

            if (lineArray.Length < 3) return;

            string text = null;

            for (int i = 2; i < lineArray.Length; i++) text += lineArray[i];

            mainWindow.lvEditor.Items.Add(new SubtitleLine(mainWindow.lvEditor.SelectedIndex, lineArray[0], lineArray[1], text));
        }
    }
}
