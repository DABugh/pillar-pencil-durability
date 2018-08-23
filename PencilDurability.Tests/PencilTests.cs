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

        [TestInitialize()]
        public void Initialize()
        {
            sheet = new Paper();
            pencil = new Pencil(defaultDurability);
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

            Assert.AreEqual(-1, pencil.Durability);
        }

        [TestMethod]
        public void WhenPencilCreatedWithPointDurability_DurabilityIsSet()
        {
            pencil = new Pencil(100);

            Assert.AreEqual(100, pencil.Durability);
        }

        [TestMethod]
        public void WhenPencilWrites_ItLosesItsSharpness()
        {
            pencil.Write("test", sheet);
            Assert.AreEqual(defaultDurability - 4, pencil.Durability);
        }

        [TestMethod]
        public void WhenPencilWrites_ItDoesNotLoseItsSharpnessForSpaces()
        {
            pencil.Write("this is a test", sheet);
            Assert.AreEqual(defaultDurability - 11, pencil.Durability);
        }

        [TestMethod]
        public void WhenPencilWrites_ItLosesDoubleSharpnessForCapitalLetters()
        {
            pencil.Write("This is a Test", sheet);
            Assert.AreEqual(defaultDurability - 13, pencil.Durability);
        }

        [TestMethod]
        public void WhenPencilOverwrites_ItLosesSharpness()
        {
            pencil.Write("This is a new Test", sheet);
            Assert.AreEqual(defaultDurability - 16, pencil.Durability);

            pencil.Write("fun time", sheet, 10);
            Assert.AreEqual(defaultDurability - 23, pencil.Durability);
        }
    }
}
