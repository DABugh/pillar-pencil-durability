using System;
using System.Text;

namespace PencilDurability
{
    public class Paper
    {
        public string Text { get; private set; }

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
            int position = Text.LastIndexOf(textToErase);

            if (position < 0) return this;

            string replacementText = buildEraseText(textToErase, charsToErase);

            replaceText(position, replacementText);

            return this;
        }

        private string buildEraseText(string textToErase, int charsToErase)
        {
            // Eraser is either long enough or infinite
            if (charsToErase >= textToErase.Length || charsToErase < 0)
            {
                return new String(' ', textToErase.Length);
            }

            StringBuilder sbTextToErase = new StringBuilder(textToErase);
            for (int i = sbTextToErase.Length - 1; i >= 0; i--)
            {
                if (!char.IsWhiteSpace(sbTextToErase[i]) && charsToErase > 0)
                {
                    sbTextToErase[i] = ' ';
                    charsToErase--;
                }
            }
            return sbTextToErase.ToString();
        }

        public Paper Overwrite(string requestedText, int position)
        {
            //TODO: Verify that [pos + requestedText.Length] does not exceed sheet.Length
            //  If so, must overwrite overlapping portions and append the rest
            //  If pos > sheet.Length, possibly pad with whitespace -- requirements should be clarified
            //  As currently written, this scenario will throw an exception

            string replacementText = buildOverwriteText(requestedText, position);

            replaceText(position, replacementText);

            return this;
        }

        private string buildOverwriteText(string requestedText, int position)
        {
            StringBuilder sbRequestedText = new StringBuilder(requestedText);
            StringBuilder sbOldText = new StringBuilder(Text.Substring(position, requestedText.Length));
            StringBuilder sbNewText = new StringBuilder(requestedText);

            //TODO: This will treat newline characters as written characters, which will overwrite spaces and collide with
            //  other characters; to properly simulate paper, a newline must be retained in the overwriting text, but this would
            //  potentially break up words or otherwise alter the intent. Requirements must be clarified and a new test added.
            for (int i = 0; i < sbRequestedText.Length; i++)
            {
                sbNewText[i] = determineOverwriteCharacter(sbOldText[i], sbRequestedText[i]);
            }

            return sbNewText.ToString();
        }

        private static char determineOverwriteCharacter(char oldChar, char requestedChar)
        {
            if (requestedChar == ' ')
            {
                return oldChar;
            }
            else if (oldChar == ' ')
            {
                return requestedChar;
            }
            else
            {
                return '@';
            }
        }

        private void replaceText(int position, string newText)
        {
            Text = Text.Substring(0, position) + newText + Text.Substring(position + newText.Length);
        }
    }
}