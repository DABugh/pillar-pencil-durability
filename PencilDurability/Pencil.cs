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
    }
}