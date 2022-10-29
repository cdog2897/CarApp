/*
 * Caleb Wolin
 * CST-250
 * 9/16/22
 */
using CarClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarAppGUI
{
    public partial class Main : Form
    {
        // create a new store instance
        Store myStore = new Store();
        // binding source for linking arraylist to list 
        BindingSource carInventoryBindingSource = new BindingSource();
        BindingSource cartBindingSource = new BindingSource();
        public Main()
        {
            InitializeComponent();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            // check for exceptions before adding to list
            try
            {
                int price = int.Parse(txtPrice.Text);
                int condition = int.Parse(txtCondition.Text);

                // create new car and add to list
                Car c = new Car(txtMake.Text, txtModel.Text, price, condition, txtColor.Text);

                myStore.CarList.Add(c);
                // reset values
                carInventoryBindingSource.ResetBindings(false);

                txtMake.Text = null;
                txtModel.Text = null;
                txtPrice.Text = null;
                txtCondition.Text = null;
                txtColor.Text = null;

                lblException.Visible = false;
            }
            catch (Exception except)
            {
                lblException.Visible = true;
                txtMake.Text = null;
                txtModel.Text = null;
                txtPrice.Text = null;
                txtCondition.Text = null;
                txtColor.Text = null;
            }

        }
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            // add to cart list
            Car selected = (Car) lstInventory.SelectedItem;
            myStore.ShoppingList.Add(selected);
            cartBindingSource.ResetBindings(false);
        }
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            // calculate total
            decimal total = myStore.Checkout();
            lblTotal.Text = "$" + total.ToString();
            cartBindingSource.ResetBindings(false);
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            carInventoryBindingSource.DataSource = myStore.CarList;
            cartBindingSource.DataSource = myStore.ShoppingList;
            lstInventory.DataSource = carInventoryBindingSource;
            lstInventory.DisplayMember = ToString();
            lstCart.DataSource = cartBindingSource;
            lstCart.DisplayMember = ToString();
        }
    }
}
