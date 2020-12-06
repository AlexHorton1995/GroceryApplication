using GroceryApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryApplication
{
    public partial class UpdateItems : Form
    {
        public UpdateItems(ListModel model)
        {
            InitializeComponent();
            ItemName.Text = model.ItemName;
            ItemPrice.Text = model.ItemPrice.ToString();
            ItemQuantity.Text = model.ItemQuantity.ToString();
            Taxable.Checked = model.Taxable;
        }

        public ListModel UpdateRowData()
        {
            var iName = ItemName.Text;
            decimal.TryParse(ItemPrice.Text, out decimal price);
            Int16.TryParse(ItemQuantity.Text, out short qty);
            var iQty = qty;
            var iPrice = price * qty;

            ListModel retModel = new ListModel()
            {
                ItemName = iName,
                ItemPrice = iPrice,
                ItemQuantity = iQty,
                Taxable = Taxable.Checked
            };

            return retModel;
        }

        private void UpdateItem_Click(object sender, EventArgs e)
        {
            UpdateRowData();
            Close();
        }
    }
}
