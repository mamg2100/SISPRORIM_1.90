using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistema_prorim
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
               progressBar1.Value = progressBar1.Value + 2;
            }
            else
            {
                timer1.Enabled = false;
                this.Visible = false;
                Principal frm = new Principal();
                frm.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar1.Visible = true;
            timer2.Enabled = false;
        }
    }
}
