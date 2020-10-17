using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestProject1.MockClasses;

namespace UnitTestProject1
{
    [TestClass]
    public class DaoTests
    {
        public IMockAppDao Dao;
        public static DataTable ItemData;
        public MockListModel MockModel;
        public DataTable MockTable;

        [TestInitialize]
        public void TestInitialize()
        {
            Dao = new MockDao();
            MockTable = Dao.CreateDataTable();
        }

        [TestMethod]
        public void TestCreateDatatable()
        {
            Assert.IsInstanceOfType(MockTable, typeof(DataTable));
        }

        [TestMethod]
        public void TestAddItems()
        {
            MockModel = new MockListModel()
            {
                ItemName = "test",
                ItemPrice = 0.00M,
                Taxable = true,
                ItemQuantity = 2
            };

            Dao.AddRecord(MockTable, MockModel);

            foreach (DataRow row in MockTable.Rows)
            {
                Assert.AreEqual(MockModel.ItemName, row["ItemName"]);
                Assert.AreEqual(MockModel.ItemPrice, row["ItemPrice"]);
                Assert.AreEqual(MockModel.ItemQuantity, row["ItemQuantity"]);
                Assert.AreEqual(MockModel.Taxable ? "T" : String.Empty, row["Taxable"]);
            }

        }
    }
}
