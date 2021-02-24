using System.Windows;

namespace zhsub.Models
{
    /// <summary>
    /// Display a custom message box
    /// </summary>
    internal class Message
    {
   

        public void SaveChanges(bool isTrue, string fileName)
        {
            if (!isTrue) return;

            var resultDialog = MessageBox.Show("Do you want to save changes to " + fileName, "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (resultDialog == MessageBoxResult.Cancel) return;

            if (resultDialog == MessageBoxResult.No) return;

            //if (resultDialog == MessageBoxResult.Yes) _subtitle.Save();
        }
    }
}
