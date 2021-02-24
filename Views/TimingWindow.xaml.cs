// Copyright (c) ZitherHarp 2021. All rights reserved.
// Licensed under the ZitherHarp license.
// Email: harp.zither@gmail.com.

using System.Windows;
using System.Windows.Controls;
using zhsub.Models;
using zhsub.ViewModels;

namespace zhsub.Views
{
    /// <summary>
    /// Interaction logic for TimingWindow.xaml
    /// </summary>
    public partial class TimingWindow : Window
    {
        private ListView listView;
        private SubtitleTime subtitleTime;

        public TimingWindow(ListView listView)
        {
            InitializeComponent();

            this.listView = listView;
        }

        /// <summary>
        /// Contains button click events on this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btnOK":
                    break;
                case "btnCancel":
                    Close();
                    break;
            }
        }

        private void RadioButton_IsChecked(object sender, RoutedEventArgs e)
        {
            if (rbtForward.IsChecked == true && rbtAllRows.IsChecked == true)
            {
                subtitleTime.Forward(subtitleTime.Split(Title), subtitleTime.Split(""));
            }    
        }

        /// <summary>
        /// Provides the ability to backward and forward the times
        /// </summary>
        private void ShiftTime(bool isForward, bool isAllLines)
        {
            switch (isForward)
            {
                case true:
                    switch (isAllLines)
                    {
                        case true:
                            break;
                    }    
                    break;
            }    
        }    

        private void ShiftTimeBy(RadioButton radioButton)
        {
            if (radioButton.IsChecked != true) return;

            switch (radioButton.Name)
            {
                case "rbtAllLines":
                    foreach (SubtitleLine line in listView.Items)
                    {
                        line.StartTime = subtitleTime.Forward(subtitleTime.Split(Title), subtitleTime.Split(""));
                    }    
                    break;
            }    
        }

        private void Shift()
        {            
            switch (rbtStartTime.IsChecked)
            {
                case true:
                    switch (rbtAllRows.IsChecked)
                    {
                        case true:
                            switch (rbtForward.IsChecked)
                            {
                                case true:
                                    foreach (SubtitleLine line in listView.Items)
                                    {
                                        line.StartTime = subtitleTime.Forward(subtitleTime.Split(Title), subtitleTime.Split(""));
                                    }
                                    break;
                            }    
                            break;
                    }    
                    break;
            }    
        }
    }
}
