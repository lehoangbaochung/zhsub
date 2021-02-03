using System.Collections.ObjectModel;
using zhsub.Models.Files;

namespace zhsub.Models.Lists
{
    class ViewModel
    {
        public static ObservableCollection<Lrc> LrcList { get; set; }
        public static ObservableCollection<Srt> SrtList { get; set; }
    }
}
