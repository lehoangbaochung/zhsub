using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using zhsub.Models.Files;

namespace zhsub.Features
{
    class Export
    {
        public static void SrtFile(ListView listView)
        {
            foreach (var item in listView.Items)
            {
                var t = (item as Srt).Index;
            }    
        }
    }
}
