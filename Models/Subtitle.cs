using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace zhsub.Models
{
    /// <summary>
    /// Lớp diễn ra các thao tác với file phụ đề
    /// </summary>

    public class Subtitle
    {
        #region protected properties
        protected Window window;
        protected ListView listView;
        #endregion

        #region private properties
        private string _filePath;
        private string _fileExtension;
        private string _fileData;

        private HttpClient httpClient;
        #endregion

        #region attributes
        public event PropertyChangedEventHandler PropertyChanged;
        private object _startTime, _endTime;
        private string _text;

        private void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public object StartTime
        {
            get { return _startTime; }
            set
            {
                if (_startTime != value)
                {
                    _startTime = value;
                    NotifyPropertyChanged("StartTime");
                }
            }
        }

        public object EndTime
        {
            get { return _endTime; }
            set
            {
                if (_endTime != value)
                {
                    _endTime = value;
                    NotifyPropertyChanged("EndTime");
                }
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }
        #endregion

        protected Dictionary<SubtitleExtension, SubtitleFormat> SubtitleDict;

        protected Subtitle() { }

        public Subtitle(Window window, ListView listView)
        {
            this.window = window;
            this.listView = listView;

            SubtitleDict = new Dictionary<SubtitleExtension, SubtitleFormat>
            {
                {
                    SubtitleExtension.SBV, new SubtitleFormat()
                    {
                        LineSplitChar = "\r\n\r\n",
                        ItemSplitChar = "\r\n",
                        TimeSeparatorChar = ",",
                        LineStartIndex = 0,
                        ItemStartIndex = 0,
                        ItemCount = 2
                    }
                },
                {
                    SubtitleExtension.SRT, new SubtitleFormat()
                    {
                        LineSplitChar = "\r\n\r\n",
                        ItemSplitChar = "\r\n",
                        TimeSeparatorChar = " --> ",
                        LineStartIndex = 0,
                        ItemStartIndex = 1,
                        ItemCount = 3
                    }
                },
                {
                    SubtitleExtension.VTT, new SubtitleFormat()
                    {
                        LineSplitChar = "\r\n\r\n",
                        ItemSplitChar = "\r\n",
                        TimeSeparatorChar = " --> ",
                        LineStartIndex = 1,
                        ItemStartIndex = 0,
                        ItemCount = 2
                    }
                },
                {
                    SubtitleExtension.LRC, new SubtitleFormat()
                    {
                        LineSplitChar = "\n",
                        TimeSeparatorChar = "]",
                        LineStartIndex = 0,
                        ItemStartIndex = 0,
                        ItemCount = 1
                    }
                }
            };
        }    

        public Subtitle(object startTime, object endTime, string text)
        {
            _startTime = startTime;
            _endTime = endTime;
            _text = text;
        }   
        
        public void New()
        {
            Refresh();

            listView.Items.Add(new Subtitle("0:00:00.000", "0:00:05.000", null));
            listView.SelectedItem = listView.Items[0];

            window.Title = "Untitled - Zither Harp Subtitles 1.0.0";
        }

        public void Open()
        {
            var openFileDialog = new OpenFileDialog() 
            { 
                Filter = "Subtitle formats (*.ass,*.lrc,*.krc,*.qrc,*.sbv,*.srt,*.vtt)|*.ass;*.lrc;*.krc;*.qrc;*.sbv;*.srt;*.vtt"
            };

            if (openFileDialog.ShowDialog() == false) return;

            _filePath = openFileDialog.FileName;
            _fileExtension = _filePath[(_filePath.LastIndexOf('.') + 1)..];
            _fileData = File.ReadAllText(_filePath);
            Refresh();

            switch (_fileExtension)
            {
                case "lrc":
                    Export(SubtitleExtension.LRC);
                    break;
                case "sbv":
                    Export(SubtitleExtension.SBV);
                    break;
                case "srt":
                    Export(SubtitleExtension.SRT);
                    break;
                case "vtt":
                    Export(SubtitleExtension.VTT);
                    break;
            }

            window.Title = $"{ _filePath[(_filePath.LastIndexOf('\\') + 1)..] } - Zither Harp Subtitles 1.0.0";
        }

        private void Refresh()
        {
            listView.ItemsSource = null;
            listView.Items.Clear();
        }

        private void Export(SubtitleExtension subtitleExtension)
        {
            var subFormat = SubtitleDict[subtitleExtension];

            var lineArray = _fileData.Split(subFormat.LineSplitChar);

            if (subtitleExtension == SubtitleExtension.LRC)
            {
                foreach (var line in lineArray)
                {
                    var subtitle = new Subtitle()
                    {
                        StartTime = line[1..line.IndexOf(subFormat.TimeSeparatorChar)],
                        Text = line[line.IndexOf(subFormat.TimeSeparatorChar)..].Replace(subFormat.LineSplitChar, "")
                    };

                    listView.Items.Add(subtitle);
                }
            }
            else
            {
                for (int i = subFormat.LineStartIndex; i < lineArray.Length; i++)
                {
                    var itemArray = lineArray[i].Split(subFormat.ItemSplitChar);

                    if (itemArray.Length < subFormat.ItemCount) return;

                    _text = itemArray[subFormat.ItemCount - 1];

                    if (itemArray.Length > subFormat.ItemCount)
                    {
                        for (int j = subFormat.ItemCount; j < itemArray.Length; j++)
                        {
                            _text += "\n" + itemArray[j];
                        }
                    }

                    var subtitle = new Subtitle()
                    {
                        StartTime = itemArray[subFormat.ItemStartIndex]
                            [0..itemArray[subFormat.ItemStartIndex].IndexOf(subFormat.TimeSeparatorChar)],
                        EndTime = itemArray[subFormat.ItemStartIndex]
                            [(itemArray[subFormat.ItemStartIndex].IndexOf(subFormat.TimeSeparatorChar) + subFormat.TimeSeparatorChar.Length - 1)..],
                        Text = _text
                    };

                    listView.Items.Add(subtitle);
                }
            }
        }

        private void Import(SubtitleExtension subtitleExtension)
        {
            var subFormat = SubtitleDict[subtitleExtension];

            _fileData = null;
            int? index = null;

            if (subtitleExtension == SubtitleExtension.VTT)
            {
                _fileData = "WEBVTT\nKind: captions\nLanguage: vi";
            }

            foreach (Subtitle item in listView.Items)
            {
                if (item.Text == null) return;

                if (subtitleExtension == SubtitleExtension.SRT)
                {
                    index++;
                }

                _fileData += index + $"{ item.StartTime }" + subFormat.TimeSeparatorChar 
                    + $"{ item.EndTime }" + subFormat.ItemSplitChar + $"{ item.Text }" + subFormat.LineSplitChar;
            }
        }

        public void Save()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "SubRip files (*.srt)|*.srt|Lyric files (*.lrc)|*.lrc|Sbv files (*.sbv)|*.sbv|Vtt files (*.vtt)|*.vtt"
            };

            if (saveFileDialog.ShowDialog() == false) return;

            _filePath = saveFileDialog.FileName;
            _fileExtension = _filePath[(_filePath.LastIndexOf('.') + 1)..];
            _fileData = null;

            switch (_fileExtension)
            {
                case "lrc":
                    Import(SubtitleExtension.LRC);
                    break;
                case "sbv":
                    Import(SubtitleExtension.SBV);
                    break;
                case "srt":
                    Import(SubtitleExtension.SRT);
                    break;
                case "vtt":
                    Import(SubtitleExtension.VTT);
                    break;
            }

            File.WriteAllText(_filePath, _fileData);
            window.Title = $"{ _filePath[(_filePath.LastIndexOf('\\') + 1)..] } - Zither Harp Subtitles 1.0.0";
        }

        public void TrimLines()
        {
            foreach (Subtitle item in listView.Items)
            {
                item.Text = item.Text.Trim();
            }
        }

        public void Translate(string sourceLanguage, string translateLanguage)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://translate.google.com/")
            };

            string text = null;

            foreach (Subtitle item in listView.Items)
            {
                text += item.Text + "\n";
            }    

            string result = httpClient.GetStringAsync("?sl=" + sourceLanguage + "&tl=" + translateLanguage + "&text=" + text + "&op=translate").Result;
            var value0 = Regex.Match(result, @"<div class=""OPPzxe"">(.*?)<div class=""kGmWO"">", RegexOptions.Singleline).Value;
            var valuet = Regex.Match(value0, @"<c-wiz jsrenderer=""WFss9b""(.*?)>(.*?)<div class=""ZyvIDe"" jsname=""kDm4dd"">", RegexOptions.Singleline).Value;
            var value1 = Regex.Match(valuet, @"<div class=""dePhmb""(.*?)>(.*?)<div class=""BdDRKe""(.*?)>", RegexOptions.Singleline).Value;
        }
    }
}
