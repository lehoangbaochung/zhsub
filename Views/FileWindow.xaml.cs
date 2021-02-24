using System.Windows;
using System.Windows.Controls;
using zhsub.Models;

namespace zhsub.Views
{
    /// <summary>
    /// Interaction logic for FileWindow.xaml
    /// </summary>
    public partial class FileWindow : Window
    {
        private readonly MainWindow _mainWindow;
        private Subtitle _subtitle;

        public FileWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
            _subtitle = new Subtitle(_mainWindow);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbxFormat.SelectionBoxItem == null) return;

            switch (cbxFormat.SelectionBoxItem)
            {
                case "SBV":
                    _subtitle.Convert(SubtitleExtension.LRC, SubtitleExtension.SBV);
                    break;
            }    

            Close();
        }
    }
}
