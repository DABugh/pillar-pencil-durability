using System;

namespace PencilDurability
{
    public class Pencil
    {
        public int Durability { get; private set; }

        public Pencil(int dur = -1)
        {
            //If Durability not specified when Pencil created, assume infinite durability (verify requirements)
            Durability = dur;
        }

        public Paper Write(string textToWrite, Paper sheet)
        {
            //Inelegant, but succinct. .NET Framework has a Count() method, which does not exist in .NET Core
            int numNonspaces = textToWrite.Replace(" ", String.Empty).Length;

            Durability -= numNonspaces;
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