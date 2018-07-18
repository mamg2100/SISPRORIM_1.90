using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistema_Prorim
{
    public partial class Informação : Form
    {
        public Informação()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            FSM sugestao = new FSM();
            sugestao.Show();
            this.Close();
        }
    }
}
