using System;

namespace PencilDurability
{
    public class Pencil
    {
        public Paper Write(string textToWrite, Paper sheet)
        {
            return sheet.Append(textToWrite);
        }

        //Replace the last instance of textToErase with spaces of equal length
        public Paper Erase(string textToErase, Paper sheet)
        {
            return sheet.Erase(textToErase);
        }

        public Paper Overwrite(string requestedText, Paper sheet, int pos)
        {
            return sheet.Overwrite(requestedText, pos);
        }
    }
}