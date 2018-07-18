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
    public partial class frmConfiguração : Form
    {
        public frmConfiguração()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    //System.IO.File.WriteAllText(@"D:\\IPSERVIDOR.txt", textBox1.Text);
                    System.IO.File.WriteAllText(textBox2.Text, textBox1.Text);
                    MessageBox.Show("Arquivo salvo com sucesso");
                    textBox1.Text = "";
                    this.Close();
                }
                catch 
                {
                    MessageBox.Show("Problemas na gravação do arquivo! \n \n 1. Verifique se há permissão para gravação na unidade especificada;  \n 2. Se o caminho é válido;  \n 3. Se o arquivo existe.", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
                }
            }
            else 
            {
                
            }
            // há o componente saveFileDialog que pode ser usado fazendo
            // System.IO.File.WriteAllText(saveFileDialog.FileName, textBox1.Text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConfiguração_Load(object sender, EventArgs e)
        {

        }
    }
}
