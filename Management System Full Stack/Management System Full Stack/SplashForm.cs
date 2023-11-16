using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_System_Full_Stack
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        int startPoint = 0;

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            startPoint += 2;
            guna2ProgressBar1.Value = startPoint;
            if (guna2ProgressBar1.Value == 100)
            {
                guna2ProgressBar1.Value = 0;
                timer1.Stop();
                LoginForm login = new LoginForm();
                login.ShowDialog();
                this.Hide();
            }
        }


        private void SplashForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
