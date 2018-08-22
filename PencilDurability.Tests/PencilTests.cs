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
        public void WhenPencilGivenPaperAndTextToWrite_PaperContainsText()
        {
            Pencil pencil = new Pencil();
            string sheet = String.Empty;
            string textToWrite = "It was the best of times";

            pencil.Write(textToWrite, out sheet);
            Assert.AreEqual(textToWrite, sheet);
        }
    }
}
