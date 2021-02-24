// Copyright (c) ZitherHarp 2021. All rights reserved.
// Licensed under the ZitherHarp license.
// Email: harp.zither@gmail.com.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using zhsub.Models;
using zhsub.Views;

namespace zhsub.ViewModels
{
    public class ViewModel
    {
        public ObservableCollection<SubtitleLine> SubtitleLines { get; private set; }
        public Dictionary<SubtitleExtension, SubtitleFormat> SubtitleDictionary { get; }

        public ViewModel()
        {
            SubtitleLines = new ObservableCollection<SubtitleLine>();

            SubtitleDictionary = new Dictionary<SubtitleExtension, SubtitleFormat>
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
        }
    }

    
}
