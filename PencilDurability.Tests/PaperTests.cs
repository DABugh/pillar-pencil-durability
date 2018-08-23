using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilDurability;

namespace PencilDurability.Tests
{
    [TestClass]
    public class PaperTests
    {
        private Paper sheet;

#region Write

        // WRITE
        // As a writer
        //  I want to be able use a pencil to write text on a sheet of paper
        //  so that I can better remember my thoughts

        [TestMethod]
        public void WhenPaperInitializedWithText_PaperContainsText()
        {
            string textToWrite = "It was the best of times";
            sheet = new Paper(textToWrite);

            Assert.AreEqual(textToWrite, sheet.Text);
        }

        [TestMethod]
        public void WhenPaperGivenTextToAppend_PaperContainsText()
        {
            string textToWrite = "It was the best of times";
            sheet = new Paper();

            sheet.Append(textToWrite);
            Assert.AreEqual(textToWrite, sheet.Text);
        }

        [TestMethod]
        public void WhenNonblankPaperGivenTextToWrite_PaperContainsOriginalAndAppendedText()
        {
            string originalText = "It was the best of times";
            string textToWrite = ", it was the blurst of times.";
            sheet = new Paper(originalText);

            sheet.Append(textToWrite);
            Assert.AreEqual(originalText + textToWrite, sheet.Text);
        }

#endregion //Write

#region Erase

        // ERASE
        // As a writer
        //  I want to be able to erase previously written text
        //  so that I can remove my mistakes

        [TestMethod]
        public void WhenNonblankPaperGivenTextToErase_PaperContainsBlankSpacesAtLastTextPosition()
        {
            string originalText = "It was the best of times, it was the blurst of times.";
            string eraseText = "times";
            sheet = new Paper(originalText);

            sheet.Erase(eraseText);
            Assert.AreEqual("It was the best of times, it was the blurst of      .", sheet.Text);

            sheet.Erase(eraseText);
            Assert.AreEqual("It was the best of      , it was the blurst of      .", sheet.Text);
        }
        
        [TestMethod]
        public void WhenNonblankPaperGivenTextToEraseWithNoMatch_PaperIsUnchanged()
        {
            string originalText = "It was the best of times, it was the blurst of times.";
            string eraseText = "days";
            sheet = new Paper(originalText);

            sheet.Erase(eraseText);
            Assert.AreEqual(originalText, sheet.Text);
        }       

        [TestMethod]
        public void WhenNonblankPaperGivenTextToEraseAndLengthValueLessThanNumCharacters_SomeCharactersAreErasedAndOthersRemain()
        {
            string originalText = "It was the best of times, it was the blurst of times.";
            string eraseText = "the best of";
            sheet = new Paper(originalText);

            // This should also test that the correct substring is erased, and the search is not based on a
            //  substring of the search term ("the best of" is 9 characters, but we can only erase 4: "st of".
            //  Pencil should still erase part of "the best of" and not "the blurst of")
            sheet.Erase(eraseText, 4);
            Assert.AreEqual("It was the be      times, it was the blurst of times.", sheet.Text);
        }

        [TestMethod]
        public void WhenPaperWithNewlineGivenTextToEraseAndACount_NewlineDoesNotGetCountedOrReplacedWithASpace()
        {
            string originalText = "It was the best of times." + System.Environment.NewLine + "It was the blurst of times.";
            string eraseText = "the best of times." + System.Environment.NewLine + "It was ";
            sheet = new Paper(originalText);

            sheet.Erase(eraseText, 8);
            Assert.AreEqual("It was the best of tim   " + System.Environment.NewLine + "       the blurst of times.", sheet.Text);
        }

#endregion //Erase

#region Edit

        // EDIT
        // As a writer
        //  I want to be able to edit previously written text
        //  so that I can change my writing without starting over

        [TestMethod]
        public void WhenNonblankPaperGivenTextToOverwriteAtPosition_PaperContainsOverwriteTextInsteadOfSpacesAtPosition()
        {
            string originalText = "It was the best of times,     it was the blurst of times.";
            sheet = new Paper(originalText);
            
            //Leave one space after comma, overwrite after that
            sheet.Overwrite("but", 26);

            Assert.AreEqual("It was the best of times, but it was the blurst of times.", sheet.Text);
        }

        [TestMethod]
        public void WhenNonblankPaperGivenTextToOverwriteAtPosition_PaperContainsCollisionsInsteadOfCharactersAtPosition()
        {
            string originalText = "It was the best of times, it was the blurst of times.";
            sheet = new Paper(originalText);

            sheet.Overwrite("worst", 37);

            Assert.AreEqual("It was the best of times, it was the @@@@@t of times.", sheet.Text);
        }

        [TestMethod]
        public void WhenNonblankPaperGivenTextToOverwriteAtPosition_PaperContainsTextAndCollisions()
        {
            string originalText = "It was the best of times, it was the blurst of times.";
            sheet = new Paper(originalText);

            sheet.Overwrite("super amazing", 33);

            //Requirements state that writing a character over a space does not cause a collision,
            //  but do not specify expectations when writing a space over an existing character.
            //  This test assumes no collision (see 'l' in expected result), as that reflects
            //  real-world outcome. In a professional environment, requirement should be verified.
            Assert.AreEqual("It was the best of times, it was @@@e@l@@@@i@@ times.", sheet.Text);
        }

#endregion //Edit
    }
}
