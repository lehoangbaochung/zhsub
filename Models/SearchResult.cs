namespace zhsub.Models
{
    public class SearchResult
    {
        public object ID { get; set; }
        public object Song { get; set; }
        public object Artist { get; set; }
        public object Lyric { get; set; }
    }
    
    public class SubtitleFormat
    {
        public string LineSplitChar, ItemSplitChar, TimeSeparatorChar;
        public int LineStartIndex, ItemStartIndex, ItemCount;
    }

    public enum SubtitleExtension { ASS, LRC, KRC, QRC, SBV, SRT, VTT }
}
