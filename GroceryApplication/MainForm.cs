using GroceryApplication.DAO;
using GroceryApplication.Models;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

[assembly: InternalsVisibleTo("UnitTestProject1")]
namespace GroceryApplication
{
    public partial class MainForm : Form
    {
        public static IAppDao Dao;
        public static DataTable ItemData;
        public ListModel model;

        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        static MainForm()
        {
            Dao = new AppDao();
            ItemData = Dao.CreateDataTable();
        }

        #endregion

        #region Event Handlers
        private void MainForm_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void AddItem_Click(object sender, EventArgs e)
        {
            AddItems();
        }

        private void RemoveItem_Click(object sender, EventArgs e)
        {
            RemoveAnItem();
        }

        private void UpdateItem_Click(object sender, EventArgs e)
        {
            //TODO - at the end of all the code above

            UpdateTableItem();
            //Dao.UpdateRecord();
        }

        private void LoadList_Click(object sender, EventArgs e)
        {

        }

        private void SaveList_Click(object sender, EventArgs e)
        {

        }

        private void ShareList_Click(object sender, EventArgs e)
        {

        }

        private void CloseForm_Click(object sender, EventArgs e)
        {
            var selection = MessageBox.Show("Do you want to exit?", "Exit Grocery App", MessageBoxButtons.YesNo);

            if (selection.ToString() == "Yes")
            {
                this.Close();
            }
        }




        #endregion

        #region Worker Methods
        internal bool Initialize()
        {
            try
            {
                ItemName.Text = "Enter Item";
                ItemPrice.Text = "0.00";
                ItemQuantity.Text = "0";
                Taxable.Checked = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool AddItems()
        {
            try
            {
                model = new ListModel();
                Regex rx = new Regex("[^A-Za-z0-9\\ ]");

                #region Validation

                //item name
                if (!rx.IsMatch(ItemName.Text))
                {
                    model.ItemName = ItemName.Text;
                }
                else
                {
                    MessageBox.Show("You cannot have special Characters.  Please try again.");
                    return false;
                }

                //item quantity
                if (short.TryParse(ItemQuantity.Text, out short quantity))
                {
                    model.ItemQuantity = quantity;
                }
                else
                {
                    MessageBox.Show("You must enter a valid quantity.  Please try again.");
                    return false;
                }

                //item price
                if (decimal.TryParse(ItemPrice.Text, out decimal price))
                {
                    model.ItemPrice = price * quantity;
                }
                else
                {
                    MessageBox.Show("You must enter a decimal value (e.g., 1.75).  Please try again.");
                    return false;
                }

                //Taxable?
                model.Taxable = Taxable.Checked;

                #endregion

                Dao.AddRecord(ItemData, model);
                dataGridView1.DataSource = ItemData;
                Initialize();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem adding the record to the table: " + ex.Message);
                return false;
            }
        }

        internal bool RemoveAnItem()
        {
            bool retVal = true;

            try
            {
                var sel = dataGridView1.CurrentCell.RowIndex;

                var itemName = ItemData.Rows[sel]["ItemName"].ToString();
                var deleteDecision = MessageBox.Show($"Would you like to remove item {itemName}?",
                     "Delete Item?",
                     MessageBoxButtons.YesNo);

                if (deleteDecision.ToString() == "Yes")
                {
                    Dao.DeleteRecord(ItemData, sel);
                    return retVal;
                }
            }
            catch (Exception)
            {
                retVal = false;
            }
            return retVal;
        }

        internal bool UpdateTableItem()
        {
            bool retVal = true;

            try
            {
                //1. identify column that needs changing
                var sel = dataGridView1.CurrentCell.RowIndex;

                //2. change the column by updating the cell
                ListModel model = new ListModel()
                {
                    ItemName = ItemData.Rows[sel]["ItemName"].ToString(),
                    ItemPrice = Convert.ToDecimal(ItemData.Rows[sel]["ItemPrice"].ToString()),
                    ItemQuantity = Convert.ToInt16(ItemData.Rows[sel]["ItemQuantity"].ToString()),
                    Taxable = ItemData.Rows[sel]["Taxable"].ToString().Equals("T")
                };

                using (var formPopup = new UpdateItems(model))
                {
                    formPopup.ShowDialog();
                    model = formPopup.UpdateRowData();
                    Dao.UpdateRecord(ItemData, model, sel);
                }


                //3. commit changes to the datatable.
                //4. update the view with the new data (totals, quantity, etc)

                //foreach (DataRow row in ItemData.Rows)
                //{
                //    var itemName = row["ItemName"].ToString();
                //    var deleteDecision = MessageBox.Show($"Would you like to remove item {itemName}?",
                //         "Delete Item?",
                //         MessageBoxButtons.YesNo);

                //    if (deleteDecision.ToString() == "Yes")
                //    {
                //        Dao.DeleteRecord(ItemData, itemName);
                //        return retVal;
                //    }
                //}
            }
            catch (Exception)
            {
                retVal = false;
            }
            return retVal;
        }


        #endregion

    }
}
