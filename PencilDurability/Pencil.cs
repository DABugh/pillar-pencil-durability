using System;
using System.Text;

namespace PencilDurability
{
    public class Pencil
    {
        // Durability cannot be changed after pencil is created
        public int Durability { get; }
        //Sharpness degrades from Durability to 0
        public int Sharpness { get; private set; }
        public int Length { get; private set; }
        public int Eraser { get; private set; }

        public Pencil(int dur = -1, int len = -1, int er = -1)
        {
            // If Durability not specified when Pencil created, assume infinite durability (verify requirements)
            Durability = dur;
            // Assume pencil comes sharpened
            Sharpness = Durability;
            // Requirements state that length "should" be specified, so infinite length is probably outside the scope of requirements;
            //  Including it anyway for consistency... if durability is not specified, length probably won't be either.
            Length = len;
            // Same as defaults above - if not specified, default to infinite eraser (verify requirements)
            Eraser = er;
        }

        public Paper Write(string textToWrite, Paper sheet, int pos = -1)
        {
            StringBuilder sbTextToWrite = new StringBuilder(textToWrite);

            if (Sharpness >= 0)
            {
                for (int i = 0; i < textToWrite.Length; i++)
                {
                    if (Sharpness == 0)
                    {
                        sbTextToWrite[i] = ' ';
                    }

                    if (char.IsUpper(sbTextToWrite[i]))
                    {
                        // Note: Requirements do not state what happens if Pencil attempts to write a capital letter with a sharpness of 1
                        //  In real-world circumstances, user would get half of a letter. One way to simulate this is converting the
                        //  capital to lower case. For now, capital letter will be skipped and sharpness will be left at 1, allowing a
                        //  lower-case to be written later. In professional situation, requirements should be clarified.
                        if (Sharpness == 1)
                        {
                            sbTextToWrite[i] = ' ';
                        }
                        else
                        {
                            Sharpness -= 2;
                        }
                    }
                    else if (char.IsWhiteSpace(sbTextToWrite[i]))
                    {
                        //Do nothing
                    }
                    else if (Sharpness >= 1)
                    {
                        Sharpness--;
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
            if (Eraser > 0)
            {
                Eraser -= textToErase.Replace(" ", String.Empty).Length;
                sheet.Erase(textToErase);
            }

            return sheet;
        }

        public void Sharpen()
        {
            if (Length > 0 || Length == -1)
            {
                Sharpness = Durability;
                if (Length > 0)
                //Assumes that sharpening an already-sharp pencil still reduces its length
                Length--;
            }
        }
    }
}