using GroceryApplication.DAO;
using GroceryApplication.Models;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GroceryApplication
{
    public partial class MainForm : Form
    {
        public static IAppDao Dao;
        public static DataTable ItemData;

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
            //TODO - at the end of all the code above
            Dao.DeleteRecord();
        }

        private void UpdateItem_Click(object sender, EventArgs e)
        {
            //TODO - at the end of all the code above
            Dao.UpdateRecord();
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
        private void Initialize()
        {
            ItemName.Text = "Enter Item";
            ItemPrice.Text = "0.00";
            ItemQuantity.Text = "0";
            Taxable.Checked = false;
        }

        private bool AddItems()
        {
            try
            {
                var model = new ListModel();
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
                Initialize();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem adding the record to the table: " + ex.Message);
                return false;
            }
        }






        #endregion

    }
}
