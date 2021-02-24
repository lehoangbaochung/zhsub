using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using zhsub.Models;

namespace zhsub.Views
{
    /// <summary>
    /// The window provides search online subtitles feature
    /// </summary>
    public partial class SearchWindow : Window
    {
        private readonly MainWindow _mainWindow;
        private readonly OnlineSubtitle _onlineSubtitle;

        public SearchWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            _onlineSubtitle = new OnlineSubtitle(lvSearch);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item && item.IsSelected)
            {
                _mainWindow.lvEditor.Items.Clear();
                _mainWindow.SetTitle((lvSearch.SelectedItem as OnlineSubtitle).Song);
                _onlineSubtitle.Download(_mainWindow.lvEditor);
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btnSearch":
                    try
                    {
                        _onlineSubtitle.Search(tbxSearch.Text);
                        txbResultCount.Text = _onlineSubtitle.GetCount();
                    }
                    catch (System.AggregateException)
                    {
                        MessageBox.Show("No connection! Please check your network!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
            }    
        }
    }
}
