using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Globalization;


namespace Sistema_prorim
{
    public partial class PermissãoAcesso : Form
    {
        private bool Logado = false; 
        public string stConection;
        private string stConsulta;
                   
        public MySqlConnection Cmn = new MySqlConnection();
        public string StringDeConexao;

        public PermissãoAcesso()
        {
            InitializeComponent();
            capturaIPServidor();
            stConection = "";
        }

        private void capturaIPServidor()
        {
            StreamReader objReader = new StreamReader("c:\\Servidor\\IPSERVIDOR.txt");
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
                textBox4.Text += sLine + Environment.NewLine;
            }
            objReader.Close();

            StringDeConexao = @"Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=''";
        
            Global.Logon.ipservidor = textBox4.Text;

        }

        private void PermissãoAcesso_Load(object sender, EventArgs e)
        {
               
        }

        bool VerificaLogin()
        {
            bool result = false;

            //StringDeConexao = @"Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=''";
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = StringDeConexao;

                try
                {
                    MySqlCommand cmd = new MySqlCommand("Select * from usuario WHERE Login_usuario ='" + txtUsuario.Text + "'AND Senha_usuario = '" + txtSenha.Text + "';", cn);
                    cn.Open();
                    MySqlDataReader dados = cmd.ExecuteReader();
                    result = dados.HasRows;
                 
                                       
                }
            
                
                catch 
               {
                   MessageBox.Show("Não foi possível fazer conexão com Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }

                finally
                
                {
                    cn.Close();
                    
                }
            }
            return result;
        }

        private void btEntrar_Click(object sender, EventArgs e)
        {
            bool result = VerificaLogin();

            Logado = result;

            if (result)
            {

                MessageBox.Show("Seja bem vindo!");

                this.Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorreto!");
            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PermissãoAcesso_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Logado)
            {
                this.Close();
            }
            else
            {
                Application.Exit();
            }

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtSenha.Focus();
            }
            else
            {

            }
                
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                bool result = VerificaLogin();

                Logado = result;

                if (result)
                {
                    Global.Logon.usuario = txtUsuario.Text;
                    
                    //-----------------------------------------------------------------------------------------
                    // populando cmbCadastradoPor

                    //capturando o nome do usuario correspondente ao usuario logado
                    
                    try
                    {

                        //stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=''";
                        Cmn.ConnectionString = StringDeConexao;
                        Cmn.Open();

                        //stConsulta = "SELECT Co d_unidade FROM unidade WHERE Cod_unidade='" + cmbSetor.Text + "'";
                        stConsulta = "SELECT nome_usuario,cod_usuario FROM usuario WHERE Login_usuario='" + Global.Logon.usuario + "'";

                        MySqlCommand myCmd = new MySqlCommand();
                        myCmd.Connection = Cmn;
                        myCmd.CommandText = stConsulta;
                        MySqlDataReader myReader = myCmd.ExecuteReader();

                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                myReader.Read();
                                //txtTipoUsuário.Text = myReader["Tipo_usuario"] + Environment.NewLine;
                                Global.Logon.nome_usuario = myReader["nome_usuario"] + Environment.NewLine;
                                Global.Logon.codigo_usuario = myReader["Cod_usuario"] + Environment.NewLine;

                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Não foi possível fazer conexão com Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    Cmn.Close();
            
            


                    //-----------------------------------------------------------------------------------------
                    
                    //MessageBox.Show(Global.Logon.usuario + ", seja bem vindo ao Sistema prorim." , "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tipoUsuario();
                   // MessageBox.Show("Tipo Usuário: " + Global.Logon.tipousuario, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Usuário ou senha incorreto!","Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else 
            {       
            
            }
        }

        private void tipoUsuario()
        {
            //----------------

            try
            {

                //stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=''";
                Cmn.ConnectionString = StringDeConexao;
                Cmn.Open();

                //stConsulta = "SELECT Co d_unidade FROM unidade WHERE Cod_unidade='" + cmbSetor.Text + "'";
                stConsulta = "SELECT Tipo_usuario FROM usuario WHERE Login_usuario='" + txtUsuario.Text + "'";

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        //txtTipoUsuário.Text = myReader["Tipo_usuario"] + Environment.NewLine;
                        Global.Logon.tipousuario= myReader["Tipo_usuario"] + Environment.NewLine;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Não foi possível fazer conexão com Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            Cmn.Close();
            
            
            //----------------
                    
        }

        private void PermissãoAcesso_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void PermissãoAcesso_Load_1(object sender, EventArgs e)
        {

        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            txtUsuario.BackColor = Color.White;
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            txtSenha.BackColor = Color.White;
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            txtUsuario.BackColor = Color.White;
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            txtSenha.BackColor = Color.White;
        }

        private void PermissãoAcesso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
            else 
            {
            }

        }

        
    }
}
