using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LD3_10_MKuliesius;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void Validation_OnlySymbolsAndSpaces_ReturnsTrue()
        {
            //Arrange act assert
            var sub = "Naujoji Akmenė";
            bool result = TaskUtils.Validation(sub);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Validation_WithSpecialCharacters_ReturnsFalse()
        {
            var sub = "Kaunas!";
            bool result = TaskUtils.Validation(sub);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Validation_Empty_ReturnsFalse()
        {
            var sub = "";
            bool result = TaskUtils.Validation(sub);
            Assert.IsFalse(result);
        }
    }
}
