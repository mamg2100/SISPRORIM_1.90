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
    public partial class rim_tem_fornecedores : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        public string stConection;
        //private string stConsulta;
        public MySqlConnection Cmn = new MySqlConnection();
     
        
        public rim_tem_fornecedores()
        {
            InitializeComponent();
        }

        private void rim_tem_fornecedores_Load(object sender, EventArgs e)
        {
            txtCodFornecedor.Text = Sistema_prorim.Global.fornecedor.codfornecedor;
            txtCodRim.Text = Sistema_prorim.Global.RI.codcetil;                

            if (txtCodRim.Text == "")
            {
                MessageBox.Show("Você só pode vincular um fornecedor a uma requisição já incluída.","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                this.Close();
            }
            else {
                txtCodRim.Text = Global.RI.codcetil;
                mostrarResultados();
            }
            
            //rbNovo.Checked = true;
            //mostrarResultados();

        }

        
        private void rbNovo_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "módulo inclusão ativado";
            txtCodFornecedor.Enabled = false;
            //txtCodRim.Enabled = true;
            txtCodRim.Enabled = false;
            //HabilitaTextBox();
            DesabilitaRadioButtons();
            bt_Gravar.Enabled = true;
            // btnExcluir.Enabled = false;
            tssMensagem.Text = "módulo inclusão ativado";

        }

        private void DesabilitaRadioButtons()
        {
            rbNovo.Enabled = false;
            rbAlterar.Enabled = false;
            rbExclui.Enabled = false;
        }

        private void rbExclui_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExclui.Checked == true)
            {
                tssMensagem.Text = "módulo exclusão ativado";

                MessageBox.Show("Confirme os dados ou clique na planilha nos dados a serem excluídos", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //rbAlterar.Checked = false;
                LimpaCampos();
                //HabilitaTextBox();'
                DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                dataGridView1.Enabled = true;
                //btnAtualizar.Enabled = false;
                //btnExcluir.Enabled = false;
            }
            else
            {

            }

        }

        private void rbAlterar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAlterar.Checked == true)
            {

                tssMensagem.Text = "módulo atualização ativado";
                rbExclui.Enabled = false;
                rbNovo.Enabled = false;

                MessageBox.Show("Confirme a despesa ou clique na planilha nos dados a serem alterados", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                groupBox1.Height = 148;
                dataGridView1.Visible = true;
                dataGridView1.Enabled = true;
                dataGridView1.Focus();
                
            }
            else
            {

            }

        }

        private void bt_Gravar_Click(object sender, EventArgs e)
        {
            if (rbNovo.Checked == true)
            {
                if (txtCodRim.Text == "")
                {
                    // txtCodRim.Enabled = true;
                    // txtCodRim.Focus();
                    tssMensagem.Text = "confirme os dados capturados";

                }
                else
                {
                    Gravar();
                    bt_Gravar.Enabled = false;
                }
            }
            else
            {
                try
                {
                    // int codigo1 = Convert.ToInt32(txtCodRim.Text);
                    // int codigo2 = Convert.ToInt32(txtCodDespesa.Text);

                    //if (rbAlterar.Checked == true)
                    //{

                      //  int codigo1 = Convert.ToInt32(txtCodRim.Text);
                      //  int codigo2 = Convert.ToInt32(txtCodFornecedor.Text);
                      //  Alterar(codigo1, codigo2);
                    //}
                    //else
                    //{
                        int codigo1 = Convert.ToInt32(txtCodRim.Text);
                        int codigo2 = Convert.ToInt32(txtCodFornecedor.Text);
                        Excluir(codigo1, codigo2);
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro:código como parâmetro não definido" + ex, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void Excluir(int codigo1, int codigo2)
        {
            //conexao
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();
            try
            {
                //mConn.ConnectionString = Dados.StringDeConexao;
                //command
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "delete from rim_has_fornecedor where Cod_rim=" + codigo1 + " AND " + "Cod_fornecedor=" + codigo2;
                //mConn.Open();
                int resultado = cmd.ExecuteNonQuery();
                if (resultado != 1)
                {
                    throw new Exception("Não foi possível excluir o fornecedor " + codigo2);
                }
                MessageBox.Show("Excluido o fornecedor de código " + "'" + codigo2 + "'" + " com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("delete from rim_has_fornecedor where Cod_rim=" + codigo1 + " AND " + "Cod_fornecedor=" + codigo2, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            mostrarResultados();
            mConn.Close();

            UncheckedRadioButtons();

        }

        /*
        private void Alterar(int codigo1, int codigo2)
        {

            try
            {
                mConn = new MySqlConnection("Persist Security Info=False; server=" + textBox4.Text;database=prorim;uid=root;password=");

                mConn.Open();

                MySqlCommand command = new MySqlCommand("UPDATE rim_has_fornecedor SET Cod_fornecedor='" + Convert.ToInt32(txtCodFornecedor.Text) + 
                    "' WHERE Cod_rim=" + txtCodRim.Text,mConn);

                //Executa a Query SQL
                command.ExecuteNonQuery();


                //Mensagem de Sucesso
                //MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //mostrarResultados();
                // Fecha a conexão
                mostrarResultados();
                mConn.Close();

                // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"
                // -------------------
                //LimpaCampos();
                //DesabilitaTextBox();
                //  -------------------
                //HabilitaRadionButtons();

                //UncheckedRadioButtons(); 

            }
            catch
            {
                MessageBox.Show("Despesa já cadastrada para essa requisição", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        */
        private void UncheckedRadioButtons()
        {
             rbAlterar.Checked = false;
             rbExclui.Checked = false;
             rbNovo.Checked = false;

        }

        private void Gravar()
        {
            if (txtCodFornecedor.Text == "")
            {
                MessageBox.Show("Dados do Fornecedor não capturados [gravação abortada].", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                // temp = Convert.ToInt32(txtCodDespesa.Text);

                try
                {
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

                    // Abre a conexão
                    mConn.Open();

                    //Query SQl // Codigo da RIM vai ser substituido quando for gerado o codigo na gravação da ri e será atualizado no update
                    MySqlCommand command = new MySqlCommand("INSERT INTO rim_has_fornecedor (Cod_rim,Cod_fornecedor) VALUES(" + 
                        Convert.ToInt32(txtCodRim.Text) + "," + Convert.ToInt32(txtCodFornecedor.Text) + ");", mConn);

                    //MySqlCommand command = new MySqlCommand("INSERT INTO rim_has_dotacao (Cod_rim,Cod_despesa,Cetil) VALUES('" + txtCodRim.Text
                    //+ "'," + txtCodDespesa.Text + ",'" + txtCodRim.Text + "')", mConn);

                    //Executa a Query SQL  
                    command.ExecuteNonQuery();

                    //Mensagem de Sucesso
                    //MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"

                    mostrarResultados();
                    LimpaCampos();
                    DesabilitaTextBox();
                    //HabilitaRadionButtons();
                    //mostrarResultados();
                    //UncheckedRadioButtons(); 

                    // Fecha a conexão
                    mConn.Close();

                }
                catch
                {
                    //MessageBox.Show("Despesa já cadastrada para a R.I." + txtCodDespesa.Text + ",'" +
                    // txtCodRim.Text + "')", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //MessageBox.Show("Despesa '" + txtDespesa.Text + "' já cadastrada para a R.I. '" + txtCodRim.Text + "'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // MessageBox.Show("INSERT INTO rim_has_dotacao (Cod_rim,Cod_despesa,Cetil) VALUES(" + Convert.ToInt32(txtCodRim.Text)
                    //+ "," + Convert.ToInt32(txtCodDespesa.Text) + ",'" + txtCodRim.Text + "')", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MessageBox.Show("INSERT INTO rim_has_fornecedor (Cod_rim,Cod_fornecedor) VALUES(" + 
                        Convert.ToInt32(txtCodRim.Text) + "," + Convert.ToInt32(txtCodFornecedor.Text) + ")", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //this.Close();

                }

                Global.fornecedor.codfornecedor = "";
                Global.RI.cetil= "";
            }
        }

        private void DesabilitaTextBox()
        {
            txtCodRim.Text = "";
            txtCodFornecedor.Text = "";
        }

        private void LimpaCampos()
        {
            txtCodFornecedor.Text = "";
            txtCodRim.Text = "";
        }

        private void mostrarResultados()
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela
            //mAdapter = new MySqlDataAdapter("SELECT * FROM rim_has_fornecedor WHERE Cod_rim= '" + txtCodRim.Text + "'ORDER BY Cod_rim", mConn);
                 
            //Criamos uma View no Banco de Dados que faz Join entre tabelas rim_has_fornecedor e tabela fornecedores e fazemos a consulta
            //nessa view filtrando pelo Cod_rim que interessa...observe as alterações abaixo...

            mAdapter = new MySqlDataAdapter("SELECT * FROM nome_fornecedor WHERE Cod_rim='" + txtCodRim.Text + "'ORDER BY Cod_rim", mConn);
            
                        //preenche o dataset através do adapter
            //mAdapter.Fill(mDataSet, "rim_has_fornecedor");
            mAdapter.Fill(mDataSet, "nome_fornecedor");
            //atribui o resultado à propriedade DataSource do dataGridView
            dataGridView1.DataSource = mDataSet;
            //dataGridView1.DataMember = "rim_has_fornecedor";
            dataGridView1.DataMember = "nome_fornecedor";


            //Renomeia as colunas
            dataGridView1.Columns[0].HeaderText = "Cod. Sequencial Requisicao";
            dataGridView1.Columns[1].HeaderText = "Cod. Sequencial Fornecedor";

            calculaRegistros();

        }

        private void calculaRegistros()
        {
            int registro;
            registro = dataGridView1.RowCount;
            if (registro == 1 || registro == 0)
                label9.Text = registro + " registro";
            else
                label9.Text = registro + " registros";

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (label9.Text == "0 registro")
            {
                MessageBox.Show("Cancelando a vinculação de um fornecedor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                Global.fornecedor.codfornecedor = "";
                Global.RI.cetil= "";                         
                this.Close();
            }

            tssMensagem.Text = "";

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void txtCodRim_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bt_Gravar.Enabled = true;
            //btnAtualizar.Enabled = false;
            txtCodRim.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodFornecedor.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            /*
            try
            {
                stConection = "Persist Security Info=False; server=" + textBox4.Text;database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                stConsulta = "SELECT Cod_fornecedor FROM rim_has_fornecedor WHERE Cod_rim='" + txtCodRim.Text + "'";

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        txtCodFornecedor.Text = myReader["Cod_fornecedor"] + Environment.NewLine;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            Cmn.Close();
             */
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            uncheckedChecks();
            tssMensagem.Text = "ação cancelada";
            habilitaChecks();
            
        }

        private void uncheckedChecks()
        {
            rbNovo.Checked = false;
            rbExclui.Checked = false;
            rbAlterar.Checked = false;

        }

        private void habilitaChecks()
        {
            rbAlterar.Enabled = true;
            rbExclui.Enabled = true;
            rbNovo.Enabled = true;
        }

        private void txtCodRim_Leave(object sender, EventArgs e)
        {
            txtCodRim.BackColor = Color.White;
        }

        private void txtCodFornecedor_Leave(object sender, EventArgs e)
        {
            txtCodFornecedor.BackColor = Color.White;
        }

        private void txtCodFornecedor_Enter(object sender, EventArgs e)
        {
            txtCodFornecedor.BackColor = Color.Yellow;
        }

        private void txtCodRim_Enter(object sender, EventArgs e)
        {
            txtCodRim.BackColor = Color.Yellow;
        }

        
    }
}
