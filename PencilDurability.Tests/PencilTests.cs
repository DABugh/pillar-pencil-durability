using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilDurability;

namespace PencilDurability.Tests
{
    [TestClass]
    public class PencilTests
    {
        private Pencil pencil;
        private string sheet;

        [TestInitialize()]
        public void Initialize()
        {
            pencil = new Pencil();
            sheet = String.Empty;
        }

        // WRITE
        // As a writer
        //  I want to be able use a pencil to write text on a sheet of paper
        //  so that I can better remember my thoughts

        [TestMethod]
        public void WhenPencilGivenBlankPaperAndTextToWrite_PaperContainsText()
        {
            string textToWrite = "It was the best of times";

            sheet = pencil.Write(textToWrite, sheet);
            Assert.AreEqual(textToWrite, sheet);
        }

        [TestMethod]
        public void WhenPencilGivenNonblankPaperAndTextToWrite_PaperContainsOriginalAndAppendedText()
        {
            string originalText = "It was the best of times";
            string textToWrite = ", it was the blurst of times.";
            sheet = originalText;

            sheet = pencil.Write(textToWrite, sheet);
            Assert.AreEqual(originalText + textToWrite, sheet);
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
            sheet = originalText;

            sheet = pencil.Erase(eraseText, sheet);
            Assert.AreEqual("It was the best of times, it was the blurst of      .", sheet);

            sheet = pencil.Erase(eraseText, sheet);
            Assert.AreEqual("It was the best of      , it was the blurst of      .", sheet);
        }
        
        [TestMethod]
        public void WhenPencilGivenNonblankPaperAndTextToEraseWithNoMatch_PaperIsUnchanged()
        {
            string originalText = "It was the best of times, it was the blurst of times.";
            string eraseText = "days";
            sheet = originalText;

            sheet = pencil.Erase(eraseText, sheet);
            Assert.AreEqual(originalText, sheet);
        }


        // EDIT
        // As a writer
        //  I want to be able to edit previously written text
        //  so that I can change my writing without starting over

        [TestMethod]
        public void WhenPencilGivenNonblankPaperAndTextToOverwriteAtPosition_PaperContainsOverwriteTextInsteadOfSpacesAtPosition()
        {
            string originalText = "It was the best of times,     it was the blurst of times.";
            sheet = originalText;
            
            //Leave one space after comma, overwrite after that
            sheet = pencil.Overwrite("but", sheet, 26);

            Assert.AreEqual("It was the best of times, but it was the blurst of times.", sheet);
        }
    }
}
