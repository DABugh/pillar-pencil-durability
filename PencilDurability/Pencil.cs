using System;

namespace PencilDurability
{
    public class Pencil
    {
        public string Write(string textToWrite, string sheet)
        {
            return sheet + textToWrite;
        }

        //Replace the last instance of textToErase with spaces of equal length
        public string Erase(string textToErase, string sheet)
        {
            int pos = sheet.LastIndexOf(textToErase);
            if (pos >= 0)
            {
                string replacementSpace = new String(' ', textToErase.Length);
                sheet = sheet.Substring(0, pos) + replacementSpace + sheet.Substring(pos + textToErase.Length);
            }

            return sheet;
        }

        public string Overwrite(string textToWrite, string sheet, int pos)
        {
            string oldString = sheet.Substring(pos, textToWrite.Length);

            //White space - overwrite
            if(String.IsNullOrWhiteSpace(oldString))
            {
                sheet = sheet.Substring(0, pos) + textToWrite + sheet.Substring(pos + textToWrite.Length);
            }
            //Written characters - convert to gibberish ('@')
            else
            {
                sheet = sheet.Substring(0, pos) + new String('@', textToWrite.Length) + sheet.Substring(pos + textToWrite.Length);
            }

            return sheet;
        }
    }
}