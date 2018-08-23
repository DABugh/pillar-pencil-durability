using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilDurability;

namespace PencilDurability.Tests
{
    [TestClass]
    public class PencilTests
    {
        private Pencil pencil;

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
    }
}
