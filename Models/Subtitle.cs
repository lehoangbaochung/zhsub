using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using zhsub.Views;

namespace zhsub.Models
{
    /// <summary>
    /// Lớp diễn ra các thao tác với file phụ đề
    /// </summary>

    public class Subtitle
    {
        #region properties
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public string FileData { get; set; }
        public Dictionary<SubtitleExtension, SubtitleFormat> SubtitleDictionary = new Dictionary<SubtitleExtension, SubtitleFormat>
            {
                {
                    SubtitleExtension.SBV, new SubtitleFormat()
                    {
                        LineSplitChar = "\r\n\r\n",
                        ItemSplitChar = "\r\n",
                        TimeSeparatorChar = ",",
                        TimeFormat = "0:00:00.000",
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
                        TickSeparatorChar = ',',
                        TimeFormat = "00:00:00,000",
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
                        TickSeparatorChar = '.',
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
                        TimeFormat = "00:00.00",
                        TickSeparatorChar = '.',
                        LineStartIndex = 0,
                        ItemStartIndex = 0,
                        ItemCount = 1
                    }
                }
            };

        protected MainWindow mainWindow;

        private HttpClient _httpClient;
        private SubtitleTime time;
        #endregion

        public Subtitle(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            time = new SubtitleTime();
        }      
        
        public void Convert(SubtitleExtension sourceSubtitle, SubtitleExtension convertSubtitle)
        {
            Import(sourceSubtitle);
            Import(convertSubtitle);
            Import(convertSubtitle);
        }

        public void New()
        {
            mainWindow.lvEditor.Items.Clear();
            mainWindow.lvEditor.Items.Add(new SubtitleLine(1, "00:00:00.000", "00:00:05.000", ""));

            mainWindow.lvEditor.Focus();
            mainWindow.lvEditor.SelectedItem = mainWindow.lvEditor.Items[0];

            mainWindow.SetTitle("Untitled");
        }  

        public void Open()
        {
            var openFileDialog = new OpenFileDialog() 
            { 
                Filter = "Subtitle formats (*.ass,*.lrc,*.krc,*.qrc,*.sbv,*.srt,*.vtt)|*.ass;*.lrc;*.krc;*.qrc;*.sbv;*.srt;*.vtt"
            };

            if (openFileDialog.ShowDialog() == false) return;

            FilePath = openFileDialog.FileName;
            FileExtension = FilePath[(FilePath.LastIndexOf('.') + 1)..];
            FileData = File.ReadAllText(FilePath);
            mainWindow.lvEditor.Items.Clear();

            switch (FileExtension)
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

            mainWindow.lvEditor.Focus();
            mainWindow.lvEditor.SelectedItem = mainWindow.lvEditor.Items[0];
            mainWindow.SetTitle(FilePath[(FilePath.LastIndexOf('\\') + 1)..]);
        }

        public void Export(SubtitleExtension subtitleExtension)
        {
            var subFormat = SubtitleDictionary[subtitleExtension];

            var lineArray = FileData.Trim().Split(subFormat.LineSplitChar);

            int index = 1;

            if (subtitleExtension == SubtitleExtension.LRC)
            {
                foreach (var line in lineArray)
                {
                    var subtitle = new SubtitleLine()
                    {
                        Index = index++,
                        StartTime = line[1..line.IndexOf(subFormat.TimeSeparatorChar)],
                        Text = line[(line.IndexOf(subFormat.TimeSeparatorChar) + 1)..].Trim()
                    };

                    mainWindow.lvEditor.Items.Add(subtitle);
                }
            }
            else
            {
                for (int i = subFormat.LineStartIndex; i < lineArray.Length; i++)
                {
                    var itemArray = lineArray[i].Split(subFormat.ItemSplitChar);

                    if (itemArray.Length < subFormat.ItemCount) return;

                    string text = itemArray[subFormat.ItemCount - 1];

                    if (itemArray.Length > subFormat.ItemCount)
                    {
                        for (int j = subFormat.ItemCount; j < itemArray.Length; j++)
                        {
                            text += "\n" + itemArray[j].Trim();
                        }
                    }

                    var item = itemArray[subFormat.ItemStartIndex];
                    var separator = subFormat.TimeSeparatorChar;

                    var startTime = time.Format(item[0..item.IndexOf(separator)]);
                    var endTime = time.Format(item[(item.IndexOf(separator) + separator.Length)..]);

                    var subtitle = new SubtitleLine(index++, startTime, endTime, text);

                    mainWindow.lvEditor.Items.Add(subtitle);
                }
            }
        }

        private void Import(SubtitleExtension subtitleExtension)
        {
            var subFormat = SubtitleDictionary[subtitleExtension];

            int? index = null;

            if (subtitleExtension == SubtitleExtension.VTT)
            {
                FileData = "WEBVTT\nKind: captions\nLanguage: vi\r\n\r\n";
            }

            foreach (SubtitleLine line in mainWindow.lvEditor.Items)
            {
                if (line.Text == null) return;

                if (subtitleExtension == SubtitleExtension.SRT)
                {
                    index++;
                }

                FileData += index + $"{ line.StartTime }" + subFormat.TimeSeparatorChar 
                    + $"{ line.EndTime }" + subFormat.ItemSplitChar + $"{ line.Text }" + subFormat.LineSplitChar;    
            }
        }

        public void Save()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "SubRip files (*.srt)|*.srt|Lyric files (*.lrc)|*.lrc|Sbv files (*.sbv)|*.sbv|Vtt files (*.vtt)|*.vtt"
            };

            if (saveFileDialog.ShowDialog() == false) return;

            FilePath = saveFileDialog.FileName;
            FileExtension = FilePath[(FilePath.LastIndexOf('.') + 1)..];
            FileData = null;

            switch (FileExtension)
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

            File.WriteAllText(FilePath, FileData.Trim());

            mainWindow.lvEditor.Focus();
            mainWindow.SetTitle(FilePath[(FilePath.LastIndexOf('\\') + 1)..]);
        }

        public void Translate(string sourceLanguage, string translateLanguage)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://translate.google.com/")
            };

            string text = null;

            foreach (SubtitleLine item in mainWindow.lvEditor.Items)
            {
                text += item.Text + "\n";
            }    

            string result = _httpClient.GetStringAsync("?sl=" + sourceLanguage + "&tl=" + translateLanguage + "&text=" + text + "&op=translate").Result;
            var value0 = Regex.Match(result, @"<div class=""OPPzxe"">(.*?)<div class=""kGmWO"">", RegexOptions.Singleline).Value;
            var valuet = Regex.Match(value0, @"<c-wiz jsrenderer=""WFss9b""(.*?)>(.*?)<div class=""ZyvIDe"" jsname=""kDm4dd"">", RegexOptions.Singleline).Value;
            var value1 = Regex.Match(valuet, @"<div class=""dePhmb""(.*?)>(.*?)<div class=""BdDRKe""(.*?)>", RegexOptions.Singleline).Value;
        }
    }
}
