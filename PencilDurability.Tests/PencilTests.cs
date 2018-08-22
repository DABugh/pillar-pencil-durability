using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilDurability;

namespace PencilDurability.Tests
{
    [TestClass]
    public class PencilTests
    {
        // WRITE
        // As a writer
        // I want to be able use a pencil to write text on a sheet of paper
        // so that I can better remember my thoughts

        [TestMethod]
        public void WhenPencilGivenBlankPaperAndTextToWrite_PaperContainsText()
        {
            Pencil pencil = new Pencil();
            string sheet = String.Empty;
            string textToWrite = "It was the best of times";

            sheet = pencil.Write(textToWrite, sheet);
            Assert.AreEqual(textToWrite, sheet);
        }

        [TestMethod]
        public void WhenPencilGivenNonblankPaperAndTextToWrite_PaperContainsOriginalAndAppendedText()
        {
            Pencil pencil = new Pencil();
            string originalText = "It was the best of times";
            string textToWrite = ", it was the blurst of times.";
            string sheet = originalText;

            sheet = pencil.Write(textToWrite, sheet);
            Assert.AreEqual(originalText + textToWrite, sheet);
        }
    }
}
