using System;
using System.Collections.Generic;
using System.Text;

namespace zhsub.Boundaries
{
    class Setting
    {
        public static string FileFormatToEdit(string formatFile)
        {
            if (formatFile == ".lrc")
            {
                return formatFile;
            }

            return ".srt";
        }
    }
}
