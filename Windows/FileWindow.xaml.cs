using System.Windows;
using System.Windows.Controls;
using zhsub.Models;

namespace zhsub.Windows
{
    /// <summary>
    /// Interaction logic for FileWindow.xaml
    /// </summary>
    public partial class FileWindow : Window
    {
        private readonly MainWindow _mainWindow;

        public FileWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (cbxFormat.SelectionBoxItem == null) return;


            Close();
        }
    }
}
