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
                if (charsToErase < 0 || charsToErase >= textToErase.Length)
                {
                    replacementText = new String(' ', textToErase.Length);
                }
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
            Text = Text.Substring(0, pos) + newText + Text.Substring(pos + requestedText.Length);

            return this;
        }
    }
}