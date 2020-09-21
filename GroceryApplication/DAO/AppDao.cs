using GroceryApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryApplication.DAO
{
    public interface IAppDao
    {
        DataTable CreateDataTable();
        bool AddRecord(DataTable dt, ListModel model);
        void UpdateRecord();
        void DeleteRecord();
        void DeleteAllRecords();
    }

    public class AppDao : IAppDao
    {
        public DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable("Items");
            dataTable.Columns.Add("ItemName", typeof(string));
            dataTable.Columns.Add("ItemPrice", typeof(decimal));
            dataTable.Columns.Add("ItemQuantity", typeof(short));
            dataTable.Columns.Add("Taxable", typeof(bool));

            return dataTable;
        }

        public bool AddRecord(DataTable dt, ListModel model)
        {
            try
            {
                DataRow row = dt.NewRow();
                row["ItemName"] = model.ItemName;
                row["ItemPrice"] = model.ItemPrice;
                row["ItemQuantity"] = model.ItemQuantity;
                row["Taxable"] = model.Taxable;
                dt.Rows.Add(row);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateRecord()
        {

        }

        public void DeleteRecord()
        {

        }

        public void DeleteAllRecords()
        {

        }

    }
}
