namespace zhsub.Models
{
    public enum SubtitleExtension { ASS, LRC, KRC, QRC, SBV, SRT, VTT, ZHS }

    public class SubtitleFormat
    {
        public string LineSplitChar { get; set; }
        public string ItemSplitChar { get; set; }
        public string TimeSeparatorChar { get; set; }
        public char TickSeparatorChar { get; set; }
        public string TimeFormat { get; set; }
        public int LineStartIndex { get; set; }
        public int ItemStartIndex { get; set; }
        public int ItemCount { get; set; }
        public int TimeHourLength { get; set; }
    }
}
