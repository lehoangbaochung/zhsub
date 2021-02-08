using zhsub.Models;
using zhsub.Models.Files;

namespace zhsub.Commands
{
    class Read
    {
        public static void SrtFile(string fileData)
        {
            List.Srt.Clear();

            var lineArray = fileData.Split("\r\n\r\n");

            foreach (var line in lineArray)
            {
                var itemArray = line.Split("\r\n");

                if (itemArray.Length < 3) return;

                string text = itemArray[2];

                if (itemArray.Length > 3)
                {
                    for (int i = 3; i < itemArray.Length; i++)
                    {
                        text += "\n" + itemArray[i];
                    }
                }    

                var srt = new Srt()
                {
                    Index = itemArray[0],
                    StartTime = itemArray[1].Substring(0, itemArray[1].IndexOf('-') - 1).Replace(',', '.'),
                    EndTime = itemArray[1][(itemArray[1].IndexOf('>') + 2)..].Replace(',', '.'),
                    Text = text.Trim()
                };

                List.Srt.Add(srt);
            }   
        }

        public static void LrcFile(string fileData)
        {
            List.Lrc.Clear();

            var lineArray = fileData.Trim().Split("\n");

            string text;

            foreach (var line in lineArray)
            {
                if (line.Length == line.IndexOf(']'))
                    text = line + " ";
                else
                    text = line[(line.IndexOf(']') + 1)..];

                var lrc = new Lrc()
                {
                    Time = line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - 1),
                    Text = text.Trim()
                };

                List.Lrc.Add(lrc);                
            }
        }
    }
}
