using GroceryApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

[assembly: InternalsVisibleTo("UnitTestProject1")]
namespace GroceryApplication.DAO
{
    public interface IAppDao
    {
        DataTable CreateDataTable();
        bool AddRecord(DataTable dt, ListModel model);
        void UpdateRecord(DataTable dt, ListModel model, int row);
        void DeleteRecord(DataTable dt, int row);
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
            dataTable.Columns.Add("Taxable", typeof(string));

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
                row["Taxable"] = model.Taxable ? "T" : string.Empty;
                dt.Rows.Add(row);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateRecord(DataTable dt, ListModel model, int row)
        {
            //1. identify column that needs changing
            //2. change the column by updating the cell
            //3. commit changes to the datatable.
            //4. update the view with the new data (totals, quantity, etc)

            dt.Rows[row]["ItemName"] = model.ItemName;
            dt.Rows[row]["ItemPrice"] = model.ItemPrice;
            dt.Rows[row]["ItemQuantity"] = model.ItemQuantity;
            dt.Rows[row]["Taxable"] = model.Taxable ? "T" : string.Empty;
            dt.AcceptChanges();

        }

        public void DeleteRecord(DataTable dt, int row)
        {
            dt.Rows[row].Delete();
            dt.AcceptChanges();
        }

        public void DeleteAllRecords()
        {

        }

    }
}
