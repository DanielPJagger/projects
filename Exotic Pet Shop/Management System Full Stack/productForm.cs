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

namespace Management_System_Full_Stack
{
    public partial class productForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "Exotic Pet Shop Management System";
        public productForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            LoadProduct();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            productModule module = new productModule(this);
            module.ShowDialog();
        }

        private void dgvProductSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProducts.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                productModule module = new productModule(this);
                module.lblPcode.Text = dgvProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvProducts.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtType.Text = dgvProducts.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.cbCategory.Text = dgvProducts.Rows[e.RowIndex].Cells[4].Value.ToString();
                module.txtQty.Text = dgvProducts.Rows[e.RowIndex].Cells[5].Value.ToString();
                module.txtPrice.Text = dgvProducts.Rows[e.RowIndex].Cells[6].Value.ToString();

                module.btnSave.Enabled = false;
                module.btnUpdate.Enabled = true;
                module.ShowDialog();
            }
            else if(colName == "Delete")
            {
                if(MessageBox.Show("Are you sure you want to delete this item?","Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbcon.executeQuery("DELETE FROM tblProducts WHERE pcode LIKE '" + dgvProducts.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");
                    
                    MessageBox.Show("Item record has been successfully removed!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            LoadProduct();
        }

        #region Method
        public void LoadProduct()
        {
            int i = 0;
            dgvProducts.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tblProducts WHERE CONCAT(pname,ptype,pcategory) LIKE '%" + dgvProductSearch.Text + "%'", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProducts.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            cn.Close();
        }
        #endregion Method
    }
}
