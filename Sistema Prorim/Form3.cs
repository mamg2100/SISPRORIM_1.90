using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Sistema_prorim
{
    public partial class frmAcesso : Form
    
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        //private DataSet mDataSet;


        public frmAcesso()
        {
            InitializeComponent();
            
        }

                private void frmAcesso_Load(object sender, EventArgs e)
        {
            txtLogin.Focus();

            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            mAdapter = new MySqlDataAdapter("Select Login_usuario,Senha_usuario FROM usuario ", mConn);
            DataTable usuario = new DataTable();
            mAdapter.Fill(usuario);

            //populando cmbUsuario
            try
            {
                for (int i = 0; i < usuario.Rows.Count; i++)
                {
                    cmbUsuario.Items.Add(usuario.Rows[i]["Login_usuario"]);
                    
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

            
             //populando cmbSenha
            try
            {
                for (int i = 0; i < usuario.Rows.Count; i++)
                {
                    cmbSenha.Items.Add(usuario.Rows[i]["Senha_usuario"]);

                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

            mConn.Close();
                                   
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {

        }
         
        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                for (int i = 0 ; i < cmbUsuario.Items.Count; i++)
                {
                    if (txtLogin.Text == cmbUsuario.Items[i].ToString())
                    {
                        textBox1.Text = txtLogin.Text;
                        txtSenha.Focus();
                    }
                    else
                    {                        
                    }
                }

                if (textBox1.Text == "")
                {
                    MessageBox.Show("Usuário não cadastrado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // txtLogin.Text = "";
                }
                else { 
                
                }           
                
            }
            else
            {
                           
            }
                 
       }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        { 
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        /*{
            
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtSenha.Text == "Admin")  
                {
                    Global.Logon.usuario = txtLogin.Text;
                    Global.Logon.senha = txtSenha.Text;
                    this.Close();                                
                                     
                }
                else
                {

                    MessageBox.Show("Senha inválida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSenha.Text = "";
                }

            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }
        }
        */
        {
            
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                for (int i = 0; i < cmbSenha.Items.Count; i++)
                {
                    if (txtSenha.Text == cmbSenha.Items[i].ToString())
                    {
                        textBox2.Text = txtSenha.Text;
                        Global.Logon.usuario = txtLogin.Text;
                        txtLogin.Text = "";
                        txtSenha.Text = "";
                        this.Close();
                    }
                    else
                    {

                    }
                }

                if (textBox2.Text == "")
                {
                    MessageBox.Show("Senha inválida", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // txtLogin.Text = "";
                }
                else
                {

                }

            }
            else
            {
            }
            
        }

        private void txtLogin_Enter(object sender, EventArgs e)
        {
            txtLogin.BackColor = Color.Yellow;
        }

        private void txtLogin_Leave(object sender, EventArgs e)
        {
            txtLogin.BackColor = Color.White;
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            txtSenha.BackColor = Color.Yellow;
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            txtSenha.BackColor = Color.White;
        }
                
    }
        
}
