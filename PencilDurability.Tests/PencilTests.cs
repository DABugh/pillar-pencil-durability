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
        private int defaultDurability = 100;
        private int defaultLength = 2;

        [TestInitialize()]
        public void Initialize()
        {
            sheet = new Paper();
            pencil = new Pencil(defaultDurability, defaultLength);
        }


        // POINT DEGRADATION
        //  As a pencil manufacturer
        //  I want writing to cause a pencil point to go dull
        //  so that I can sell more pencils

        // Instructions state: "When a pencil is created, it can be provided with a value for point durability."
        //  This implies that it can also be created without a durability. Assuming infinite durability
        //  until requirements are clarified.
        [TestMethod]
        public void WhenPencilCreatedWithoutPointDurability_UseInfiniteDurability()
        {
            pencil = new Pencil();

            Assert.AreEqual(-1, pencil.Sharpness);
        }

        [TestMethod]
        public void WhenPencilCreatedWithPointDurability_SharpnessIsSet()
        {
            pencil = new Pencil(100);

            Assert.AreEqual(100, pencil.Sharpness);
        }

        [TestMethod]
        public void WhenPencilWrites_ItLosesItsSharpness()
        {
            pencil.Write("test", sheet);
            Assert.AreEqual(defaultDurability - 4, pencil.Sharpness);
        }

        [TestMethod]
        public void WhenPencilWrites_ItDoesNotLoseItsSharpnessForSpaces()
        {
            pencil.Write("this is a test", sheet);
            Assert.AreEqual(defaultDurability - 11, pencil.Sharpness);
        }

        [TestMethod]
        public void WhenPencilWrites_ItDoesNotLoseItsSharpnessForNewlineCharacters()
        {
            pencil.Write("this is a test" + System.Environment.NewLine + "of newline characters", sheet);
            Assert.AreEqual(defaultDurability - 30, pencil.Sharpness);
        }

        [TestMethod]
        public void WhenPencilWrites_ItLosesDoubleSharpnessForCapitalLetters()
        {
            pencil.Write("This is a Test", sheet);
            Assert.AreEqual(defaultDurability - 13, pencil.Sharpness);
        }

        [TestMethod]
        public void WhenPencilOverwrites_ItLosesSharpness()
        {
            pencil.Write("This is a new Test", sheet);
            Assert.AreEqual(defaultDurability - 16, pencil.Sharpness);

            pencil.Write("fun time", sheet, 10);
            Assert.AreEqual(defaultDurability - 16 - 7, pencil.Sharpness);
        }

        [TestMethod]
        public void WhenPencilSharpnessReachesZero_SharpnessNoLongerDegrades()
        {
            pencil = new Pencil(20);

            pencil.Write("A funny thing happened on the way to integration testing.", sheet);
            Assert.AreEqual(0, pencil.Sharpness);
        }

        [TestMethod]
        public void WhenPencilSharpnessReachesZero_PencilNoLongerWrites()
        {
            pencil = new Pencil(20);

            pencil.Write("A funny thing happened on the way to integration testing.", sheet);
            Assert.AreEqual("A funny thing happened                                   ", sheet.Text);
        }

        [TestMethod]
        public void WhenPencilHasDurabilityOfNegativeOne_ItWritesEverythingAndDoesNotDegrade()
        {
            pencil = new Pencil(-1);

            pencil.Write("A funny thing happened on the way to integration testing.", sheet);
            Assert.AreEqual(-1, pencil.Sharpness);
            Assert.AreEqual("A funny thing happened on the way to integration testing.", sheet.Text);
        }


        // SHARPEN
        // As a writer
        // I want to be able to sharpen my pencil
        // so that I can continue to write with it after it goes dull

        [TestMethod]
        public void WhenPencilIsSharpened_SharpnessReturnsToInitialDurability()
        {
            pencil.Write("Sharpness is getting reduced.", sheet);
            Assert.AreEqual(defaultDurability - 27, pencil.Sharpness);

            pencil.Sharpen();
            Assert.AreEqual(defaultDurability, pencil.Sharpness);
        }

        [TestMethod]
        public void WhenPencilIsSharpened_LengthIsReduced()
        {
            pencil.Write("Sharpness is getting reduced.", sheet);
            pencil.Sharpen();
            Assert.AreEqual(defaultLength - 1, pencil.Length);

            pencil.Write("Sharpness is getting reduced again.", sheet);
            pencil.Sharpen();
            Assert.AreEqual(defaultLength - 2, pencil.Length);
        }

        [TestMethod]
        public void WhenPencilLengthIsZeroAndIsSharpened_PencilSharpnessAndLengthRemainUnchanged()
        {
            pencil = new Pencil(100, 0);
            
            pencil.Write("Sharpness is in the eye of the beholder.", sheet);
            pencil.Sharpen();
            Assert.AreEqual(defaultDurability - 34, pencil.Sharpness);
            Assert.AreEqual(0, pencil.Length);
        }

        [TestMethod]
        public void WhenPencilHasLengthOfNegativeOneAndIsSharpened_PencilIsSharpenedAndLengthRemainsNegativeOne()
        {
            pencil = new Pencil(defaultDurability, -1);
            
            pencil.Write("Sharpness is in the eye of the beholder.", sheet);
            Assert.AreEqual(defaultDurability - 34, pencil.Sharpness);
            pencil.Sharpen();
            Assert.AreEqual(defaultDurability, pencil.Sharpness);
            Assert.AreEqual(-1, pencil.Length);
        }

        // ERASER DEGRADATION
        //  As a pencil manufacturer
        //  I want a pencil eraser to eventually wear out
        //  so that I can sell more pencils

        // Instructions state: "When a pencil is created, it can be provided with a value for eraser durability."
        //  This implies that it can also be created without a durability. Assuming infinite durability
        //  until requirements are clarified.
        [TestMethod]
        public void WhenPencilCreatedWithoutEraserDurability_UseInfiniteDurability()
        {
            pencil = new Pencil();

            Assert.AreEqual(-1, pencil.Eraser);
        }
    }
}
