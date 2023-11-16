using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Management_System_Full_Stack
{
    public partial class productModule : Form
    {
            SqlConnection cn = new SqlConnection();
            SqlCommand cm = new SqlCommand();
            DbConnect dbcon = new DbConnect();
            string title = "Exotic Pet Shop Management System";
            bool check = false;
            productForm product;
            public productModule(productForm form)
            {
                InitializeComponent();
                cn = new SqlConnection(dbcon.connection());
                product = form;
                cbCategory.SelectedIndex = 0;
            }
        //not sure why this is here, investigate to remove without corrupting the design.
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this product?", "Product Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tblProducts(pname, ptype, pcategory, pqty, pprice)VALUES(@pname,@ptype,@pcategory,@pqty,@pprice)", cn);
                        cm.Parameters.AddWithValue("@pname", txtName.Text);
                        cm.Parameters.AddWithValue("@ptype", txtType.Text);
                        cm.Parameters.AddWithValue("@pcategory", cbCategory.Text);
                        cm.Parameters.AddWithValue("@pqty", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@pprice", double.Parse(txtPrice.Text));

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Product has been successfully registered!", title);
                        Clear();
                        product.LoadProduct();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to update this product?", "Product Edited", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tblProducts SET pname=@pname, ptype=@ptype, pcategory=@pcategory, pqty=@pqty, pprice=@pprice WHERE pcode=@pcode", cn);
                        cm.Parameters.AddWithValue("@pcode", lblPcode.Text);
                        cm.Parameters.AddWithValue("@pname", txtName.Text);
                        cm.Parameters.AddWithValue("@ptype", txtType.Text);
                        cm.Parameters.AddWithValue("@pcategory", cbCategory.Text);
                        cm.Parameters.AddWithValue("@pqty", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@pprice", double.Parse(txtPrice.Text));

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Product has been successfully Updated!", title);
                        product.LoadProduct();
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #region Method
        public void Clear()
        {
            txtName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            txtType.Clear();
            cbCategory.SelectedIndex = 0;

            btnUpdate.Enabled = false;
        }

        public void CheckField()
        {
            if (txtName.Text == "" | txtPrice.Text == "" | txtQty.Text == "" | txtType.Text == "")
            {
                MessageBox.Show("Required data field!", "Warning");
                return;
            }
            check = true;
        }
        #endregion Method

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow digit number
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) 
            {
                e.Handled = true;           
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow digit number
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        #region Method
        public void PClear()
        {
            txtName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            txtType.Clear();
            cbCategory.SelectedIndex = 0;

            btnUpdate.Enabled = false;
        }

        public void PCheckField()
        {
            if (txtName.Text == "" | txtPrice.Text == "" | txtQty.Text == "" | txtType.Text == "")
            {
                MessageBox.Show("Required data field!", "Warning");
                return;
            }
            check = true;
        }
        #endregion Method

    }
}
