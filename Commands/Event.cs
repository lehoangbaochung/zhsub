﻿using System;
using System.Windows.Controls;
using zhsub.Models;

namespace zhsub.Commands
{
    //class Event
    //{
    //    public static void ListViewItem_SelectionChanged(ListView listView, MediaElement mdeVideo, Slider slider)
    //    {
    //        listView.SelectionChanged += (s, e) =>
    //        {
    //            if (listView.SelectedItem is Srt)
    //            {
    //                var item = listView.SelectedItem as Srt;

    //                var hour = int.Parse(item.StartTime.ToString().Split(':')[0]);
    //                var minute = int.Parse(item.StartTime.ToString().Split(':')[1]);
    //                var second = int.Parse(item.StartTime.ToString().Split(':')[2].Split('.')[0]);
    //                var millisecond = int.Parse(item.StartTime.ToString().Split(':')[2].Split('.')[1]);

    //                mdeVideo.Position = new TimeSpan(0, hour, minute, second, millisecond);
    //                slider.Value = mdeVideo.Position.TotalSeconds;
    //            }    
    //        };
    //    }

    //    public static void MediaElement_ValueChanged(MediaElement mdeVideo, ListView listView, TextBlock textBlock)
    //    {
    //        if (listView.Items[0] is Srt)
    //        {
    //            foreach (Srt item in listView.Items)
    //            {
    //                if (item.StartTime.ToString() == mdeVideo.Position.ToString().Remove(12))
    //                {
    //                    textBlock.Text = item.Text.ToString();
                        
    //                }

    //                //if (item.EndTime.ToString() == mdeVideo.Position.ToString().Remove(12))
    //                //{
    //                //    textBlock.Text = null;
    //                //    break;
    //                //}
    //            }    
    //        }
    //    }
    //}
}
