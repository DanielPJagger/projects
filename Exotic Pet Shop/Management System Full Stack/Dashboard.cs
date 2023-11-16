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
    public partial class Dashboard : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "Exotic Pet Shop Management System";
        public Dashboard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
        }

        #region method
        public int extractData(string str)
        {
            int data = 0;
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT ISNULL(SUM(pqty),0) AS qty FROM tblProducts WHERE pcategory='"+ str + "'", cn);
                data = int.Parse(cm.ExecuteScalar().ToString());
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
            return data;
        }
        #endregion method

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblSnakes.Text = extractData("Dog").ToString();
            lblFrogs.Text = extractData("Frog").ToString();
            lblFish.Text = extractData("Fish").ToString();
            lblBirds.Text = extractData("Bird").ToString();
        }
    }
    

}

