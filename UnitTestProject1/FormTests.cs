using System;
using System.Data;
using GroceryApplication;
using GroceryApplication.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestProject1.MockClasses;

namespace UnitTestProject1
{
    [TestClass]
    public class FormTests
    {
        public MockMainForm mockMainForm;

        [TestInitialize]
        public void TestInitialize()
        {
            mockMainForm = new MockMainForm();
        }

        [TestMethod]
        public void TestFormInitialize()
        {
            var actual = mockMainForm.Initialize();
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void TestAddItems()
        {

            var actual = mockMainForm.AddItems();
            Assert.AreEqual(false, actual);            
        }

    }
}
