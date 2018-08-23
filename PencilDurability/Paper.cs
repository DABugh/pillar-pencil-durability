using System;
using System.Text;

namespace PencilDurability
{
    public class Paper
    {
        public string Text {get; private set;}

        public Paper()
        {
            Text = string.Empty;
        }

        public Paper(string initialText)
        {
            Text = initialText;
        }

        public Paper Append(string textToWrite)
        {
            Text += textToWrite;

            return this;
        }

        //Replace the last instance of textToErase with spaces of equal length
        public Paper Erase(string textToErase, int charsToErase = -1)
        {
            int pos = Text.LastIndexOf(textToErase);
            if (pos >= 0)
            {
                string replacementText = String.Empty;
                // Erase entire string
                if (charsToErase < 0 || charsToErase >= textToErase.Length)
                {
                    replacementText = new String(' ', textToErase.Length);
                }
                // Else, erase one non-whitespace character at a time
                else
                {
                    StringBuilder sbTextToErase = new StringBuilder(textToErase);
                    for(int i=sbTextToErase.Length-1; i>=0; i--)
                    {
                        if (!char.IsWhiteSpace(sbTextToErase[i]) && charsToErase > 0)
                        {
                            sbTextToErase[i] = ' ';
                            charsToErase--;
                        }
                    }
                    replacementText = sbTextToErase.ToString();
                }
                Text = Text.Substring(0, pos) + replacementText + Text.Substring(pos + textToErase.Length);
            }

            return this;
        }

        public Paper Overwrite(string requestedText, int pos)
        {
            //TODO: Verify that [pos + requestedText.Length] does not exceed sheet.Length
            //  If so, must overwrite overlapping portions and append the rest
            //  If pos > sheet.Length, possibly pad with whitespace -- requirements should be clarified
            //  As currently written, this scenario will throw an exception
              
            char[] requestedTextChars = requestedText.ToCharArray();
            char[] oldTextChars = Text.Substring(pos, requestedText.Length).ToCharArray();
            char[] newTextChars = new char[requestedText.Length];

            //TODO: This will treat newline characters as written characters, which will overwrite spaces and collide with
            //  other characters; to properly simulate paper, a newline must be retained in the overwriting text, but this would
            //  potentially break up words or otherwise alter the intent. Requirements must be clarified and a new test added.
            for (int i=0; i<newTextChars.Length; i++)
            {
                //Overwrite space with space
                if (requestedTextChars[i] == ' ' && oldTextChars[i] == ' ')
                {
                    newTextChars[i] = ' ';
                }
                //Space does not overwrite character
                else if (requestedTextChars[i] == ' ' && oldTextChars[i] != ' ')
                {
                    newTextChars[i] = oldTextChars[i];
                }
                //Overwrite space with character
                else if (requestedTextChars[i] != ' ' && oldTextChars[i] == ' ')
                {
                    newTextChars[i] = requestedTextChars[i];
                }
                //Overwrite character with character: output is gibberish
                else if (requestedTextChars[i] != ' ' && oldTextChars[i] != ' ')
                {
                    newTextChars[i] = '@';
                }
            }

            string newText = new String(newTextChars);
            Text = Text.Substring(0, pos) + newText + Text.Substring(pos + requestedText.Length);

            return this;
        }
    }
}