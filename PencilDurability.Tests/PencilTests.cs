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
            sheet = new Paper();
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
            pencil = new Pencil(100);
            
            pencil.Write("test", sheet);
            Assert.AreEqual(96, pencil.Durability);
        }

        [TestMethod]
        public void WhenPencilWrites_ItDoesNotLoseItsSharpnessForSpaces()
        {
            pencil = new Pencil(100);
            
            pencil.Write("this is a test", sheet);
            Assert.AreEqual(89, pencil.Durability);
        }

        [TestMethod]
        public void WhenPencilWrites_ItLosesDoubleSharpnessForCapitalLetters()
        {
            pencil = new Pencil(100);
            
            pencil.Write("This is a Test", sheet);
            Assert.AreEqual(87, pencil.Durability);
        }
    }
}
