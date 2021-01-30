using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace zhsub.Commands
{
    static class Open
    {
        public static readonly RoutedUICommand File = new RoutedUICommand("Subs", "Subtitles", typeof(Open), new InputGestureCollection()
        {
            new KeyGesture(Key.O, ModifierKeys.Control)
        });

        public static readonly RoutedUICommand Video = new RoutedUICommand("Video", "Video", typeof(Open));
    }
}
