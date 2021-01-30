using System.Collections.ObjectModel;
using zhsub.Models.Files;

namespace zhsub.Models.Lists
{
    class ViewModel
    {
        public static ObservableCollection<Lrc> LrcList;
        public static ObservableCollection<Srt> SrtList { get; set; }
    }
}
