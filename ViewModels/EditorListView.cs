// Copyright (c) ZitherHarp 2021. All rights reserved.
// Licensed under the ZitherHarp license.
// Email: harp.zither@gmail.com.

using System.Windows.Controls;
using zhsub.Models;
using zhsub.Views;

namespace zhsub.ViewModels
{
    class EditorListView
    {
        private MainWindow _mainWindow;

        public EditorListView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }    

        public void Event()
        {
            _mainWindow.lvEditor.SelectionChanged += (s, e) =>
            {
                if (_mainWindow.lvEditor.SelectedItem == null) return;

                var item = _mainWindow.lvEditor.SelectedItem as SubtitleLine;

                var startTime = item.StartTime.ToString().Replace(',', '.').Split(':');

                _mainWindow.tbxStartMinute.Text = startTime[^2];
                _mainWindow.tbxStartSecond.Text = startTime[^1].Split('.')[0];
                _mainWindow.tbxStartMilliSecond.Text = startTime[^1].Split('.')[1];

                if (startTime.Length < 3)
                    _mainWindow.tbxStartHour.Text = null;
                else
                    _mainWindow.tbxStartHour.Text = startTime[^3];

                if (item.EndTime == null) return;

                var endTime = item.EndTime.ToString().Replace(',', '.').Split(':');

                _mainWindow.tbxEndHour.Text = endTime[0];
                _mainWindow.tbxEndMinute.Text = endTime[^2];
                _mainWindow.tbxEndSecond.Text = endTime[^1].Split('.')[0];
                _mainWindow.tbxEndMilliSecond.Text = endTime[^1].Split('.')[1];

                if (endTime.Length < 3)
                {
                    _mainWindow.tbxEndHour.Text = null;
                    _mainWindow.tbxEndMinute.Text = null;
                    _mainWindow.tbxEndSecond.Text = null;
                    _mainWindow.tbxEndMilliSecond.Text = null;
                }    

                if (_mainWindow.btnAutoSeek.IsPressed)
                {
                    _mainWindow.mdeVideo.Position = new System.TimeSpan(
                        int.Parse(_mainWindow.tbxStartHour.Text),
                        int.Parse(_mainWindow.tbxStartHour.Text),
                        int.Parse(_mainWindow.tbxStartHour.Text),
                        int.Parse(_mainWindow.tbxStartHour.Text));
                }    
            };
        }    

        public void TextBox_TextChanged(TextBox textBox)
        {
            textBox.TextChanged += (s, e) =>
            {

            };
        }
    }
}
