using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilDurability;

namespace PencilDurability.Tests
{
    [TestClass]
    public class PencilTests
    {
        private Pencil pencil;
        private Paper sheet;

        [TestInitialize()]
        public void Initialize()
        {
            pencil = new Pencil();
        }

        // WRITE
        // As a writer
        //  I want to be able use a pencil to write text on a sheet of paper
        //  so that I can better remember my thoughts

        [TestMethod]
        public void WhenPencilGivenBlankPaperAndTextToWrite_PaperContainsText()
        {
            string textToWrite = "It was the best of times";
            sheet = new Paper();

            pencil.Write(textToWrite, sheet);
            Assert.AreEqual(textToWrite, sheet.Text);
        }

        [TestMethod]
        public void WhenPencilGivenNonblankPaperAndTextToWrite_PaperContainsOriginalAndAppendedText()
        {
            string originalText = "It was the best of times";
            string textToWrite = ", it was the blurst of times.";
            sheet = new Paper(originalText);

            pencil.Write(textToWrite, sheet);
            Assert.AreEqual(originalText + textToWrite, sheet.Text);
        }


        // ERASE
        // As a writer
        //  I want to be able to erase previously written text
        //  so that I can remove my mistakes

        [TestMethod]
        public void WhenPencilGivenNonblankPaperAndTextToErase_PaperContainsBlankSpacesAtLastTextPosition()
        {
            string originalText = "It was the best of times, it was the blurst of times.";
            string eraseText = "times";
            sheet = new Paper(originalText);

            pencil.Erase(eraseText, sheet);
            Assert.AreEqual("It was the best of times, it was the blurst of      .", sheet.Text);

            pencil.Erase(eraseText, sheet);
            Assert.AreEqual("It was the best of      , it was the blurst of      .", sheet.Text);
        }
        
        [TestMethod]
        public void WhenPencilGivenNonblankPaperAndTextToEraseWithNoMatch_PaperIsUnchanged()
        {
            string originalText = "It was the best of times, it was the blurst of times.";
            string eraseText = "days";
            sheet = new Paper(originalText);

            pencil.Erase(eraseText, sheet);
            Assert.AreEqual(originalText, sheet.Text);
        }


        // EDIT
        // As a writer
        //  I want to be able to edit previously written text
        //  so that I can change my writing without starting over

        [TestMethod]
        public void WhenPencilGivenNonblankPaperAndTextToOverwriteAtPosition_PaperContainsOverwriteTextInsteadOfSpacesAtPosition()
        {
            string originalText = "It was the best of times,     it was the blurst of times.";
            sheet = new Paper(originalText);
            
            //Leave one space after comma, overwrite after that
            pencil.Overwrite("but", sheet, 26);

            Assert.AreEqual("It was the best of times, but it was the blurst of times.", sheet.Text);
        }

        [TestMethod]
        public void WhenPencilGivenNonblankPaperAndTextToOverwriteAtPosition_PaperContainsCollisionsInsteadOfCharactersAtPosition()
        {
            string originalText = "It was the best of times, it was the blurst of times.";
            sheet = new Paper(originalText);

            pencil.Overwrite("worst", sheet, 37);

            Assert.AreEqual("It was the best of times, it was the @@@@@t of times.", sheet.Text);
        }

        [TestMethod]
        public void WhenPencilGivenNonblankPaperAndTextToOverwriteAtPosition_PaperContainsTextAndCollisions()
        {
            string originalText = "It was the best of times, it was the blurst of times.";
            sheet = new Paper(originalText);

            pencil.Overwrite("super amazing", sheet, 33);

            //Requirements state that writing a character over a space does not cause a collision,
            //  but do not specify expectations when writing a space over an existing character.
            //  This test assumes no collision (see 'l' in expected result), as that reflects
            //  real-world outcome. In a professional environment, requirement should be verified.
            Assert.AreEqual("It was the best of times, it was @@@e@l@@@@i@@ times.", sheet.Text);
        }
    }
}
