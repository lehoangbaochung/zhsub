using zhsub.Models;

namespace zhsub.Commands
{
    class Sort
    {
        public static void SrtList(int beginIndex, bool IsAscending)
        {
            for (int i = beginIndex; i < List.Srt.Count; i++)
            {
                if (IsAscending) List.Srt[i].Index = i + 1;

                else List.Srt[i].Index = i - 1;
            }
        } 
    }
}
