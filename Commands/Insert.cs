using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using zhsub.Models.Files;
using zhsub.Models;

namespace zhsub.Commands
{
    class Insert
    {
        public static void BeforeLine(ListView listView)
        {
            if (listView.SelectedItem == null) return;

            List.Srt.Insert(listView.SelectedIndex, new Srt()
            {
                Index = listView.SelectedIndex + 1,
                StartTime = (listView.Items[listView.SelectedIndex] as Srt).EndTime,
                EndTime = (listView.SelectedItem as Srt).StartTime
            });
        }

        public static void AfterLine(ListView listView)
        {
            if (listView.SelectedItem == null) return;

            List.Srt.Insert(listView.SelectedIndex + 1, new Srt()
            {
                Index = listView.SelectedIndex + 2,
                StartTime = (listView.SelectedItem as Srt).EndTime,
                EndTime = (listView.SelectedItem as Srt).EndTime
            });
        }

        public static void Introduction(ListView listView)
        {
            listView.Items.Add(new Srt() { Text = "Vietsub: Zither Harp" });
        }
    }

    class Command
    {
        public DelegateCommand AddFolderCommand { get; set; }
        public DelegateCommand InsertToBeforeLine { get; set; }

        public Command()
        {
            AddFolderCommand = new DelegateCommand(ExecuteAddFolderCommand, (x) => true);
            InsertToBeforeLine = new DelegateCommand(InsertBefore_Executed, (x) => true);
        }

        public void ExecuteAddFolderCommand(object sender)
        {
            MessageBox.Show("this will be executed on button click later");
        }

        private void InsertBefore_Executed(object sender)
        {
            var item = sender as ListView;

            if (item.SelectedItem == null) return;
        }
    }
}
