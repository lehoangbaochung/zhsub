using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace zhsub.Commands
{
    class Execute : Window
    {
		private void ExitApplication_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void ExitApplication_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void InsertLine_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			var item = sender as ListView;

			if (item.SelectedItem == null) return;

			e.CanExecute = true;
		}

		private void InsertBefore_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			var item = sender as ListView;

			if (item.SelectedItem == null) return;

		
		}

		private void InsertAfter_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			var item = sender as ListView;

			if (item.SelectedItem == null) return;
		}
	}

	public class DelegateCommand : ICommand
	{
		private readonly Predicate<object> _canExecute;
		private readonly Action<object> _execute;

		public event EventHandler CanExecuteChanged;

		public DelegateCommand(Action<object> execute) : this(execute, null) { }

		public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			if (_canExecute == null) return true;

			return _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}

		public void RaiseCanExecuteChanged()
		{
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
	}
}
