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

        public Pencil(int initialDurability = -1, int initialLength = -1, int initialEraser = -1)
        {
            // If Durability not specified when Pencil created, assume infinite durability (verify requirements)
            Durability = initialDurability;
            // Assume pencil comes sharpened
            Sharpness = Durability;
            // Requirements state that length "should" be specified, so infinite length is probably outside the scope of requirements;
            //  Including it anyway for consistency... if durability is not specified, length probably won't be either.
            Length = initialLength;
            // Same as defaults above - if not specified, default to infinite eraser (verify requirements)
            Eraser = initialEraser;
        }

        public Paper Write(string textToWrite, Paper sheet, int position = -1)
        {
            textToWrite = degradePencilTip(textToWrite);

            if (position >= 0)
            {
                sheet.Overwrite(textToWrite, position);
            }
            else
            {
                sheet.Append(textToWrite);
            }

            return sheet;
        }

        private string degradePencilTip(string textToWrite)
        {
            if (Sharpness == -1) return textToWrite;

            StringBuilder sbTextToWrite = new StringBuilder(textToWrite);

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
                else if (!char.IsWhiteSpace(sbTextToWrite[i]))
                {
                    Sharpness--;
                }
            }

            return sbTextToWrite.ToString();
        }

        public Paper Erase(string textToErase, Paper sheet)
        {
            int charsToErase = degradeEraser(textToErase);

            if (charsToErase != 0)
            {
                sheet.Erase(textToErase, charsToErase);
            }

            return sheet;
        }

        private int degradeEraser(string textToErase)
        {
            int writtenCharacterCount = textToErase.Replace(" ", String.Empty).Length;
            int charsToErase = Math.Min(writtenCharacterCount, Eraser);

            if (Eraser != -1)
            {
                Eraser -= charsToErase;
            }

            return charsToErase;
        }

        public void Sharpen()
        {
            if (Length != 0)
            {
                Sharpness = Durability;
            }

            if (Length > 0)
            {
                //Assumes requirement that sharpening an already-sharp pencil still reduces its length
                Length--;
            }
        }
    }
}