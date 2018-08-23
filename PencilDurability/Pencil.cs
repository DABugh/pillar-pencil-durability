using System;
using System.Text;

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

        public Paper Write(string textToWrite, Paper sheet, int pos = -1)
        {
            StringBuilder sbTextToWrite = new StringBuilder(textToWrite);

            if (Durability >= 0)
            {
                for (int i = 0; i < textToWrite.Length; i++)
                {
                    if (Durability == 0)
                    {
                        sbTextToWrite[i] = ' ';
                    }

                    if (char.IsUpper(sbTextToWrite[i]))
                    {
                        // Note: Requirements do not state what happens if Pencil attempts to write a capital letter with a sharpness of 1
                        //  In real-world circumstances, user would get half of a letter. One way to simulate this is converting the
                        //  capital to lower case. For now, capital letter will be skipped and sharpness will be left at 1, allowing a
                        //  lower-case to be written later. In professional situation, requirements should be clarified.
                        if (Durability == 1)
                        {
                            sbTextToWrite[i] = ' ';
                        }
                        else
                        {
                            Durability -= 2;
                        }
                    }
                    else if (char.IsWhiteSpace(sbTextToWrite[i]))
                    {
                        //Do nothing
                    }
                    else if (Durability >= 1)
                    {
                        Durability--;
                    }
                }
            }

            textToWrite = sbTextToWrite.ToString();

            if (pos >= 0)
            {
                sheet.Overwrite(textToWrite, pos);
            }
            else
            {
                sheet.Append(textToWrite);
            }

            return sheet;
        }

        //Replace the last instance of textToErase with spaces of equal length
        public Paper Erase(string textToErase, Paper sheet)
        {
            return sheet.Erase(textToErase);
        }
    }
}