using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Globalization;
using System.Diagnostics;
using Sistema_Prorim;

namespace Sistema_prorim
{
    public partial class Principal : Form
    {
        public string tipoRequisicao;
        
        public Principal()
        {

            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Global.Logon.tipousuario = "";
            Global.Logon.nome_usuario = "";

            Login acesso = new Login();
            acesso.ShowDialog();
                   
        }   

        private void rIMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.Logon.tipoRequisicao = "1";
            Global.Veiculos.veiculo = "1";
            RIM rim = new RIM();
            rim.ShowDialog();

        }

        private void fornecedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.ShowDialog();
        }      

        private void unidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unidades unidade = new Unidades();
            unidade.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }     

        private void rRPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.Logon.tipoRequisicao = "2";
            Global.Veiculos.veiculo = "1";
            RIM rim = new RIM();
            rim.ShowDialog();
        }
                     
     
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void backUPToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            Backup backup = new Backup();
            backup.Show();
                        
        }
        
        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios usuario = new Usuarios();
            usuario.Show();
        
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Global.Logon.tipoRequisicao = "1";
            Global.Veiculos.veiculo = "1";
          
            Requisicao rim = new Requisicao();
            rim.ShowDialog();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Global.Logon.tipoRequisicao = "2";
            Global.Veiculos.veiculo = "2";
            RIM rim = new RIM();
            rim.ShowDialog();

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Unidades unidade = new Unidades();
            unidade.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.ShowDialog();
        }

        private void rIMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Pesquisa navegarRIM = new Pesquisa();
            navegarRIM.ShowDialog();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }


        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            frmConfiguração config = new frmConfiguração();
            config.ShowDialog();
        }
       
        private void Principal_Shown(object sender, EventArgs e)
        {            
            button1.Visible = true;
            button2.Visible = true; 
            button3.Visible = true; 
            button4.Visible = true; 
            button5.Visible = true; 
            button6.Visible = true; 
            button7.Visible = true; 
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            button11.Visible = true;
            button12.Visible = true;


            if (Global.Logon.usuario != "")
            {
                toolStripStatusLabel4.Text = " " + (Global.Logon.usuario).ToUpper() + " | " + DateTime.Now.ToString("dd/MM/yy") +
                " | " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString() + " | vrs "
                + "1.90.0.0" + " | Servidor: " + Global.Logon.ipservidor;

                //System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                
                if (Global.Logon.tipousuario.Trim() == "Master")
                {
                    habilitaUsuarioMaster();
                }
                else
                {
                    habilitaUsuarioComum();

                }
            }           
                                   
        }

       private void habilitaUsuarioComum()
       {
            requisiçõesToolStripMenuItem.Enabled = true;
            cadastroToolStripMenuItem.Enabled = true;
            exportarToolStripMenuItem.Enabled = true;
            calculadoraToolStripMenuItem.Enabled = true;
            acessoToolStripMenuItem.Enabled = false;
            auditoriaToolStripMenuItem.Enabled = false;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = false;
            toolStripButton4.Enabled = true;
            toolStripButton5.Enabled = true;
            toolStripButton6.Enabled = true;
            toolStripButton7.Enabled = true;
            toolStripButton8.Enabled = false;
            toolStripButton9.Enabled = true;
            
            button5.Enabled = false;
            button7.Enabled = false;

            usuáriosToolStripMenuItem.Enabled = false;
                
        }
        private void habilitaUsuarioMaster()
        {
            requisiçõesToolStripMenuItem.Enabled = true;
            cadastroToolStripMenuItem.Enabled = true;
            exportarToolStripMenuItem.Enabled = true;
            calculadoraToolStripMenuItem.Enabled = true;
            acessoToolStripMenuItem.Enabled = true;
            auditoriaToolStripMenuItem.Enabled = true;

            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            toolStripButton4.Enabled = true;
            toolStripButton5.Enabled = true;
            toolStripButton6.Enabled = true;
            toolStripButton7.Enabled = true;
            toolStripButton8.Enabled = true;
            toolStripButton9.Enabled = true;

            button5.Enabled = true;
            button7.Enabled = true;
        }
           

        private void despesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dotacao despesas = new Dotacao();
            despesas.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Dotacao despesas = new Dotacao();
            despesas.Show();        
        }

        private void iPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  Aqui arquivo .ini para definir/alterar configuração da string de conexão para
            //  as máquinas clientes.   
            frmConfiguração config = new frmConfiguração();
            config.ShowDialog();

        }

        private void Principal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                Principal frm = new Principal();
                frm.Show();
            }
           
        }
   
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Global.Logon.tipoRequisicao = "1";
            Global.Veiculos.veiculo = "2";
            RIM rim = new RIM();
            rim.ShowDialog();
            
        }

        private void rRPVeículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.Logon.tipoRequisicao = "2";
            Global.Veiculos.veiculo = "2";
            RIM rim = new RIM();
            rim.ShowDialog();

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            Global.Logon.tipoRequisicao = "1";
            Global.Veiculos.veiculo = "2";
            RIM rim = new RIM();
            rim.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            Global.Logon.tipoRequisicao = "2";
            Global.Veiculos.veiculo = "1";
            RIM rim = new RIM();
            rim.ShowDialog();

        }

        private void veículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
             Veiculos veiculos = new Veiculos();
             veiculos.Show();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            frmConfiguração config = new frmConfiguração();
            config.ShowDialog();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

     
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            Veiculos_Filtros v = new Veiculos_Filtros();
            v.Show();
        }    

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            Global.Logon.tipoRequisicao = "2";
            Global.Veiculos.veiculo = "2";
            RIM rim = new RIM();
            rim.ShowDialog();

        }      

        private void veiculosVinculadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Veiculos_Filtros v = new Veiculos_Filtros();
            v.Show();
        }

        private void planilhaDeDespesasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sistema_Prorim.PlanilhaDespesa pd = new Sistema_Prorim.PlanilhaDespesa();
            pd.Show();

        }

        private void exportarPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consulta cons = new Consulta();
            cons.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Consulta cons = new Consulta();
            cons.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sistema_prorim.Global.Logon.tipoRequisicao = "RIM";
            Requisicao rim = new Requisicao();
            rim.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sistema_prorim.Global.Logon.tipoRequisicao = "RRP";
            Requisicao rim = new Requisicao();
            rim.Show();
        }        

        private void notasFicaisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Backup copia = new Backup();
            copia.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Text = "Pesquisas";
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Text = "";
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.Text = "RIM";
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Text = "";
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.Text = "RRP";
        }

       
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Text = "";
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.Text = "Veículos";
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.Text = "";
        }

        private void button12_MouseEnter(object sender, EventArgs e)
        {
            button12.Text = "Fornecedores";
        }

        private void button12_MouseLeave(object sender, EventArgs e)
        {
            button12.Text = "";
        }

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            button11.Text = "Despesas";
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.Text = "";
        }

        private void button10_MouseEnter(object sender, EventArgs e)
        {
            button10.Text = "Documentos Fiscais";
        }

        private void button10_MouseLeave(object sender, EventArgs e)
        {
            button10.Text = "";
        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {
            button9.Text = "Usuários";
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            button9.Text = "";
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.Text = "BackUP";
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.Text = "";
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            button7.Text = "Configurações";
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button7.Text = "";
        }

        private void button8_MouseEnter(object sender, EventArgs e)
        {
            button8.Text = "";
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Dotacao despesa = new Dotacao();
            despesa.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PlanilhaDespesa pd = new PlanilhaDespesa();
            pd.Show();            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Usuarios user = new Usuarios();
            user.Show();
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.Text = "Notas";
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Informação info = new Informação();
            info.Show();
        }

        private void sugestõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FSM sugestao = new FSM();
            sugestao.Show();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.Show();
        }          
                 
    }
  }

