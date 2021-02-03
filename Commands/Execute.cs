using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using zhsub.Models.Files;
using zhsub.Models.Lists;

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

			if (item.SelectedIndex > 0)
			{
				ViewModel.SrtList.Insert(item.SelectedIndex, new Srt()
				{
					Index = item.SelectedIndex - 1,
					StartTime = (item.SelectedItem as Srt).EndTime,
					EndTime = (item.SelectedItem as Srt).EndTime
				});
			}
			else
				ViewModel.SrtList.Insert(0, new Srt()
				{
					Index = 1,
					StartTime = (item.SelectedItem as Srt).EndTime,
					EndTime = (item.SelectedItem as Srt).EndTime
				});
		}

		private void InsertAfter_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			var item = sender as ListView;

			if (item.SelectedItem == null) return;

			ViewModel.SrtList.Insert(item.SelectedIndex + 1, new Srt()
			{
				Index = item.SelectedIndex + 2,
				StartTime = (item.SelectedItem as Srt).EndTime,
				EndTime = (item.SelectedItem as Srt).EndTime
			});
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
