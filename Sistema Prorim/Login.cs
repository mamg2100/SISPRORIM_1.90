using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;
using Sistema_prorim;

namespace Sistema_Prorim
{
    public partial class Login : Form
    {
        private bool Logado = false; 
        public string stConection;
        private string stConsulta;
        private string ipServidor;
        private MySqlConnection Cmn = new MySqlConnection();
        public string StringDeConexao;

        public Login()
        {
            InitializeComponent();
            capturaIPServidor();
            stConection = "";
            StringDeConexao = @"Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=''";                   
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
                ipServidor += sLine + Environment.NewLine;
            }
            objReader.Close();            
            Global.Logon.ipservidor = ipServidor;

        }       

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        bool VerificaLogin()
        {
            bool result = false;
            
            {   
                Cmn.ConnectionString = StringDeConexao;

                if (txtSenha.Text == "")
                {
                    try
                    {
                        // Somente analisa se o usuário existe...
                        MySqlCommand cmd = new MySqlCommand("Select * from usuario WHERE Login_usuario ='" + txtUsuario.Text + "';", Cmn);
                        Cmn.Open();
                        MySqlDataReader dados = cmd.ExecuteReader();
                        result = dados.HasRows;
                        if (result == false)
                        {
                            MessageBox.Show("Usuário não cadastrado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            txtUsuario.Text = "";
                            txtUsuario.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível fazer conexão com Banco de Dados: "+ex.Message, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    finally
                    {
                        Cmn.Close();
                    }
                }
                else {

                    try
                    {
                        // Analisa se o usuário e a senha existem...
                        MySqlCommand cmd = new MySqlCommand("Select * from usuario WHERE Login_usuario ='" + txtUsuario.Text + "'AND Senha_usuario = '" + txtSenha.Text + "';", Cmn);
                        Cmn.Open();
                        MySqlDataReader dados = cmd.ExecuteReader();
                        result = dados.HasRows;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível fazer conexão com Banco de Dados: "+ex.Message, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    finally
                    {
                        Cmn.Close();
                    }
                }
            }
            return result;
        }
              
                    
             
        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            txtUsuario.BackColor = Color.Yellow;
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            txtSenha.BackColor = Color.Yellow;
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            txtUsuario.BackColor = Color.White;
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            txtSenha.BackColor = Color.White;
        }        

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtUsuario.Text == "")
                {
                    txtUsuario.Focus();                   
                }
                else
                {  //-------------------
                    
                    bool result = VerificaLogin();
                    Logado = result;

                    if (result)
                    {
                        Global.Logon.usuario = txtUsuario.Text;
                        try
                        {
                            Cmn.ConnectionString = StringDeConexao;
                            Cmn.Open();

                            stConsulta = "SELECT Nome_usuario,Cod_usuario,Tipo_usuario FROM usuario WHERE Login_usuario='" + Global.Logon.usuario + "'";

                            MySqlCommand myCmd = new MySqlCommand();
                            myCmd.Connection = Cmn;
                            myCmd.CommandText = stConsulta;
                            MySqlDataReader myReader = myCmd.ExecuteReader();

                            if (myReader.HasRows)
                            {
                                while (myReader.Read())
                                {
                                    myReader.Read();
                                    Global.Logon.nome_usuario = myReader["Nome_usuario"] + Environment.NewLine;
                                    Global.Logon.codigo_usuario = myReader["Cod_usuario"] + Environment.NewLine;
                                    Global.Logon.tipousuario = myReader["Tipo_usuario"] + Environment.NewLine;
                                }
                            }
                            txtSenha.Focus();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Problema(s) de conexão. "+ex.Message, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtUsuario.Text = "";
                        }

                        Cmn.Close();
                    }
                  }
                }
            else{
                    txtUsuario.Focus();
                }                    
        }          
            
        
        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13){
                if (txtUsuario.Text != "")
                {
                    if (txtSenha.Text == "")
                    {
                        txtSenha.Focus();
                    }
                    else
                    {
                        bool result = VerificaLogin();
                        Logado = result;

                        if (result)
                        {
                            {
                                Global.Logon.usuario = txtUsuario.Text;
                                try
                                {
                                    Cmn.ConnectionString = StringDeConexao;
                                    Cmn.Open();

                                    stConsulta = "SELECT Nome_usuario,Cod_usuario,Tipo_usuario FROM usuario WHERE Login_usuario='" + Global.Logon.usuario + "'";

                                    MySqlCommand myCmd = new MySqlCommand();
                                    myCmd.Connection = Cmn;
                                    myCmd.CommandText = stConsulta;
                                    MySqlDataReader myReader = myCmd.ExecuteReader();

                                    if (myReader.HasRows)
                                    {
                                        while (myReader.Read())
                                        {
                                            myReader.Read();
                                            Global.Logon.nome_usuario = myReader["Nome_usuario"] + Environment.NewLine;
                                            Global.Logon.codigo_usuario = myReader["Cod_usuario"] + Environment.NewLine;
                                            Global.Logon.tipousuario = myReader["Tipo_usuario"] + Environment.NewLine;
                                        }
                                    }
                                    txtSenha.Focus();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Problema(s) de conexão. " + ex.Message, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtUsuario.Text = "";
                                }

                                Cmn.Close();
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Senha não cadastrada ou incorreta.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            txtSenha.Text = "";
                            txtSenha.Focus();
                        }
                    }
                }
                else {

                    MessageBox.Show("Entre com um login válido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtUsuario.Focus();
                
                }                               
            }            
        }

        private void linkLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }      
       
    }
}
