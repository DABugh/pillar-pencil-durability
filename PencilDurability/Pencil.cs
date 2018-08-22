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

        public string Overwrite(string requestedText, string sheet, int pos)
        {
            //TODO: Verify that [pos + requestedText.Length] does not exceed sheet.Length
            //  If so, must overwrite overlapping portions and append the rest
            //  If pos > sheet.Length, possibly pad with whitespace -- requirements should be clarified
            //  As currently written, this scenario will throw an exception

            char[] requestedTextChars = requestedText.ToCharArray();
            char[] oldTextChars = sheet.Substring(pos, requestedText.Length).ToCharArray();
            char[] newTextChars = new char[requestedText.Length];

            for (int i=0; i<newTextChars.Length; i++)
            {
                if (requestedTextChars[i] == ' ' && oldTextChars[i] == ' ')
                {
                    newTextChars[i] = ' ';
                }
                else if (requestedTextChars[i] == ' ' && oldTextChars[i] != ' ')
                {
                    newTextChars[i] = oldTextChars[i];
                }
                else if (requestedTextChars[i] != ' ' && oldTextChars[i] == ' ')
                {
                    newTextChars[i] = requestedTextChars[i];
                }
                else if (requestedTextChars[i] != ' ' && oldTextChars[i] != ' ')
                {
                    newTextChars[i] = '@';
                }
            }

            string newText = new String(newTextChars);
            sheet = sheet.Substring(0, pos) + newText + sheet.Substring(pos + requestedText.Length);

            return sheet;
        }
    }
}