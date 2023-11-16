using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Management_System_Full_Stack
{
    public partial class UserModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Exotic Pet Shop Management System";

        bool check = false;
        UserForm userForm;
        public UserModule(UserForm user)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            userForm = user;
            cbRole.SelectedIndex = 1;
        }

        public UserModule()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this user?", "User Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tblUser(name,address,phone,role,dob,password)VALUES(@name,@address,@phone,@role,@dob,@password)", cn);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@password", txtPass.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("User has been successfully registered!", title);
                        Clear();
                        userForm.LoadUser();
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
                    if (MessageBox.Show("Are you sure you want to update this record?", "Edit Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tblUser SET name=@name, address=@address, phone=@phone, role=@role, dob=@dob, password=@password WHERE id=@id", cn);
                        cm.Parameters.AddWithValue("@id", lblUid.Text);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@password", txtPass.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("User's has been successfully update!", title);
                        Clear();
                        userForm.LoadUser();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.Text == "Employee")
            {
                this.Height = 490 - 31;
                lblPass.Visible = false;
                txtPass.Visible = false;
            }
            else
            {
                lblPass.Visible = true;
                txtPass.Visible = true;
                this.Height = 490;
            }
        }

        #region Method

        public void Clear()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtPass.Clear();
            cbRole.SelectedIndex = 0;
            dtDob.Value = DateTime.Now;

            btnUpdate.Enabled = false;
        }

        //check field and DOB
        public void CheckField()
        {
            if(txtName.Text == ""| txtAddress.Text == "")
            {
                MessageBox.Show("Required data field!", "Warning");
                return;
            }

            if(checkAge(dtDob.Value) < 18) 
            {
                MessageBox.Show("User is under the age of 18!", "Warning");
                return;
            }
            check = true;
        }

        // calculate age for under 18
        private static int checkAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;
            return age;
        }
        #endregion Method
    }
}
