using Microsoft.VisualStudio.TestTools.UnitTesting;
using LD3_10_MKuliesius;
using System;
using System.Configuration;
using System.Diagnostics;

namespace LD3_10_MKuliesius.UnitTest
{
    [TestClass]
    public class TaskUtilsTest
    {
        #region variables
        LinkedList<Worker> workers = InOut.ReadWorkerFile("U10a1.txt");
        LinkedList<Detail> details = InOut.ReadDetailFile("U10B.txt");

        #endregion

        [TestMethod]
        public void Validation_CalculatesCorrectEarningForPrice()
        {
            var detail = "DetaleA";
            decimal expection = 1942.5m;
            decimal result = TaskUtils.CalculateEarningsForPart(workers,details,detail);
            Assert.AreEqual(expection, result);
        }
        [TestMethod]
        public void Validation_CalculatesCorrectEarningForPriceWorker()
        {
            var name = "Smith John";
            var detail = "A123";

            decimal result = TaskUtils.CalculateEarningsForPart(workers, details, name, detail);
            Assert.AreEqual(1470.00m, result);
        }
        [TestMethod]
        public void Validation_IsInList_True()
        {
            var name = "Smith John";
            var detail = "A123";
            Worker worker = new Worker(name, "2024 - 04 - 01", "A123", 50);

            bool result = TaskUtils.IsInList(workers, worker);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validation_IsInList_False()
        {
            var name = "SAAA";
            var detail = "A123";
            Worker worker = new Worker(name, "2024 - 04 - 01", "A122", 20);

            bool result = TaskUtils.IsInList(workers, worker);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validation_TotalPartsForWorker()
        {
            var name = "Smith John";
            var detail = "A123";
            Worker worker = new Worker(name, "2024 - 04 - 01", "A123", 50);

            var result = TaskUtils.TotalDetails(workers, worker);
            Assert.AreEqual(140,result);
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

        [TestMethod]
        public void CalculateEarningsForPart_WithValidData_ReturnsCorrectEarnings()
        {
            // Expected earnings calculation: 50 * 10.50 = 525.00
            decimal expected = 1942.50m;
            decimal result = TaskUtils.CalculateEarningsForPart(workers, details, "DetaleA");
            Assert.AreEqual(expected, result, "The calculated earnings do not match expected.");
        }
        [TestMethod]
        public void CalculateEarningsForPart_WithValidWorkerAndPart_ReturnsCorrectEarnings()
        {
            // Expected earnings calculation: 50 * 10.50 = 525.00
            decimal expected = 1470.00m;
            decimal result = TaskUtils.CalculateEarningsForPart(workers, details, "Smith John", "A123");
            Assert.AreEqual(expected, result, "The calculated earnings for specific worker and part do not match expected.");
        }

        [TestMethod]
        public void IsInList_ExistingWorker_ReturnsTrue()
        {
            Worker testWorker = new Worker("Smith John", "2024-04-01", "A123", 50);
            bool isInList = TaskUtils.IsInList(workers, testWorker);
            Assert.IsTrue(isInList, "Existing worker should be found in list.");
        }

        [TestMethod]
        public void IsInList_NonExistingWorker_ReturnsFalse()
        {
            Worker testWorker = new Worker("Non Existent", "2024-04-01", "XYZ987", 10);
            bool isInList = TaskUtils.IsInList(workers, testWorker);
            Assert.IsFalse(isInList, "Non-existent worker should not be found in list.");
        }

        [TestMethod]
        public void TotalDetails_ForExistingWorker_ReturnsCorrectTotal()
        {
            int expected = 140; // Smith John produced 50 details
            Worker testWorker = new Worker("Smith John", "2024-04-01", "A123", 50);
            int totalDetails = TaskUtils.TotalDetails(workers, testWorker);
            Assert.AreEqual(expected, totalDetails, "Total details produced by the worker does not match expected.");
        }

        [TestClass]
        public class LinkedListTests
        {
            LinkedList<Worker> workers = InOut.ReadWorkerFile("U10a1.txt");
            LinkedList<Detail> details = InOut.ReadDetailFile("U10B.txt");

            [TestMethod]
            public void Sort_BasicCorrectData_SortsCorrectly()
            {
                LinkedList<Detail> TestList = new LinkedList<Detail>();
                bool correct = false;

                var det1 = new Detail("AAA", "ADATA", 2.5m);
                var det2 = new Detail("BBB", "BamBaliys", 2.6m);
                var det3 = new Detail("CCC", "CAdiLavaas", 3.7m);

                TestList.Add(det3);
                TestList.Add(det2);
                TestList.Add(det1);
                string message = "AAA " + TestList.Get(0).Code + " BBB " + TestList.Get(1).Code + " CCC " + TestList.Get(2).Code;
                TestList.Sort();

                if (TestList.Get(0).Name == "AAA" && TestList.Get(1).Name == "BBB" && TestList.Get(2).Name == "CCC")
                {
                    correct = true;
                }

                 message += " AAA " + TestList.Get(0).Code + " BBB " + TestList.Get(1).Code + " CCC " + TestList.Get(2).Code;
                Debug.WriteLine(message);

                Assert.IsTrue(TestList.Get(2).Code.Equals("CCC"), message);
            }

            [TestMethod]
            public void Add_BasicCorrectData_AddsToTheEndCorrectly()
            {
                LinkedList<Detail> TestList = new LinkedList<Detail>();
                bool correct = false;

                var det1 = new Detail("AAA", "ADATA", 2.5m);
                var det2 = new Detail("BBB", "BamBaliys", 2.6m);
                var det3 = new Detail("CCC", "CAdiLavaas", 3.7m);

                TestList.Add(det2);
                TestList.Add(det1);
                TestList.Add(det3);

                if (TestList.Get(2).Code == "CCC") 
                {
                    correct = true;
                }

                
                Assert.IsTrue(TestList.Get(2).Code.Equals("CCC"));
            }

            [TestMethod]
            public void Sort_BasicCorrectData_SortsInt()
            {
                LinkedList<int> AllSubs = new LinkedList<int>();
                bool sorted = false;
                var sub1 = 10; //2nd
                var sub2 = 1; //1st
                var sub3 = 25; //3rd

                AllSubs.Add(sub1);
                AllSubs.Add(sub2);
                AllSubs.Add(sub3);

                AllSubs.Sort();

                if (AllSubs.Get(0) <= AllSubs.Get(1) && AllSubs.Get(1) <= AllSubs.Get(2))
                {
                    sorted = true;
                }

                Assert.IsTrue(sorted);
            }

        }

    }
}
