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
        // I want to be able use a pencil to write text on a sheet of paper
        // so that I can better remember my thoughts

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
    }
}
