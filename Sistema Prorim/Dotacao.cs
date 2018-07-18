using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Sistema_Prorim;

namespace Sistema_prorim
{
    public partial class Dotacao : Form
    {
                
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        String temp;
        int codigo = 0;
        int flagInclusao = 1;

        public Dotacao()
        {
            InitializeComponent();
        }

        private void Dotacao_Load(object sender, EventArgs e)
        {
            rbPorDespesa.Checked = true;
            mostrarResultados();
        }   

        private void mostrarResultados()
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                if (rbPorCodigo.Checked == true)
                    // ordena a tabela de acordo com o critério estabelecido
                    mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao ORDER BY Cod_Despesa", mConn);
                else
                    if (rbPorReduzida.Checked == true)
                        mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao ORDER BY Reduzida", mConn);
                    else
                        if (rbPorDespesa.Checked ==true)
                        mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao ORDER by Despesa", mConn);
                        else
                            if(rbPorPrograma.Checked==true)
                            mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao ORDER by Programa", mConn);
                            else
                                mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao ORDER by Acao", mConn);
                               
                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "dotacao");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "dotacao";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Despesa";
                dataGridView1.Columns[2].HeaderText = "Reduzida";
                //dataGridView1.Columns[3].HeaderText = "Aplicação";
                //dataGridView1.Columns[4].HeaderText = "Categ.Econ";
                //dataGridView1.Columns[5].HeaderText = "Descrição";
                //dataGridView1.Columns[6].HeaderText = "Fonte Recurso";
                //dataGridView1.Columns[7].HeaderText = "Descrição F.R.";
                dataGridView1.Columns[3].HeaderText = "Programa";
                dataGridView1.Columns[4].HeaderText = "Ação";
                        
                //MySqlCommand cmd = new MySqlCommand();
                //cmd.Connection = mConn;
                //cmd.CommandText = "SELECT Nome_fornecedor from fornecedor where Cod_fornecedor = " + 1;

                calculaQuantidadeRegistros();
                LimparCamposFiltros();
            mConn.Close();

        }

        private void LimparCamposFiltros()
        {
            txtCheckAcao.Text = "";
            txtCheckReduzida.Text = "";                            
        }

        private void calculaQuantidadeRegistros()
        {
            int registro;
            //registro = dataGridView1.RowCount - 1;
            registro = dataGridView1.RowCount;
            if (registro == 1 || registro == 0)
                label9.Text = registro + " registro";
            else
                label9.Text = registro + " registros";

        }

        private void PesquisaPorCodigo(int codigo)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                
                mConn.Open();

                    //cria um adapter utilizando a instrução SQL para acessar a tabela 

                    // ordena a tabela de acordo com o critério estabelecido
                    mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao Where Cod_Despesa=" + codigo, mConn);

                    //preenche o dataset através do adapter
                    mAdapter.Fill(mDataSet, "dotacao");

                    //atribui o resultado à propriedade DataSource da dataGridView
                    dataGridView1.DataSource = mDataSet;
                    dataGridView1.DataMember = "dotacao";

                    calculaQuantidadeRegistros();
             
                mConn.Close();

        }
                    

        private void PesquisaPorSetor(string temp)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
           
                mConn.Open();
                    //cria um adapter utilizando a instrução SQL para acessar a tabela 
                    // ordena a tabela de acordo com o critério estabelecido
                    mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao WHERE Nome_dotacao=" + "LIKE " + "'%" + temp + "%'", mConn);

                    //preenche o dataset através do adapter
                    mAdapter.Fill(mDataSet, "dotacao");

                    //atribui o resultado à propriedade DataSource da dataGridView
                    dataGridView1.DataSource = mDataSet;
                    dataGridView1.DataMember = "dotacao";

                    calculaQuantidadeRegistros();
                mConn.Close();

        }
                        
       
        private void DesabilitarTextBox()
        {
            txtCodigoSequencial.Enabled = false;
            txtCodDespesa.Enabled = false;
            //txtReduzida.Enabled = false;
            txtPrograma.Enabled = false;
            txtAcao.Enabled = false;
            cmbPrograma.Enabled = false;
            cmbReduzida.Enabled = false;

        }

        private void LimparCampos()
        {
            txtCodigoSequencial.Text = "";
            txtCodDespesa.Text = "";
            txtReduzida.Text = "";
            txtPrograma.Text = "";
            txtAcao.Text = "";
            cmbPrograma.Text = "";

        }
        
       private void rbPorCodigo_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void rbPorNome_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void rbPorContato_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }


        private void HabilitaTextBox()
        {
            //txtCodigoSequencial.Enabled = true;
            txtCodDespesa.Enabled = true;
            //txtCodigoAplicação.Enabled = true;
            //txtCodCategoriaEconomica.Enabled = true;
            //txtReduzida.Enabled = true;
            //txtDescCategoriaEconomica.Enabled = true;
            //txtCodFonteRecursos.Enabled = true;
            txtPrograma.Enabled = true;
            txtAcao.Enabled = true;
            cmbPrograma.Enabled = true;
            cmbReduzida.Enabled = true;

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtCodigoSequencial_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void Excluir(int codigo)
        {
            {
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
                            cmd.CommandText = "delete from dotacao where Cod_Despesa = " + codigo;
                            //mConn.Open();
                            int resultado = cmd.ExecuteNonQuery();
                            if (resultado != 1)
                            {
                                throw new Exception("Não foi possível excluir a despesa " + codigo);
                            }
                        }
                        catch 
                        {
                            MessageBox.Show("Não foi possível estabelecer a conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                        }
                        finally
                        {
                            mConn.Close();
                            mostrarResultados();
                        
                        }

                    mostrarResultados();
                    
                    dataGridView1.Enabled = false;
                    tssMensagem.Text = "";
                    LimparCampos();
                    DesabilitarTextBox();
                    MessageBox.Show("Excluída a despesa '" + codigo + "' com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
            }
        }

        private void Alterar(int codigo)
        {
            {
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

                        // Vamos deixar essa codificação abaixo - que não funcionou - para comparação com a que funciona logo abaixo.
                        //cmd.CommandText = "UPDATE fornecedor SET Nome_fornecedor=" + "'" + txtFornecedor.Text + "'," + " End_fornecedor =" + "'" + txtEndereço + "'," +
                        //" Fone1_fornecedor =" + "'" + txtFone1 + "'," + " Fone2_fornecedor =" + "'" + txtFone2 + "'," + " Email_fornecedor =" + "'" + txtEmail + "'" + "Where Cod_fornecedor = " + codigo;

                        cmd.CommandText = "UPDATE dotacao SET Despesa =" + "'" + txtCodDespesa.Text + "',"  + "Reduzida=" + "'" + txtReduzida.Text + "'," +
                            "Programa=" + "'" + txtPrograma.Text + "'," + "Acao=" + "'" + txtAcao.Text + "'" + " WHERE Cod_Despesa = " + codigo;

                        MessageBox.Show("Registro " + "'" + codigo + "'" + " alterado com sucesso.","Informação",MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível alterar os dados da 'Despesa' " + codigo);
                        }

                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Não foi possível alterar os dados da 'Despesa' " + codigo + "/ Erro: " + ex.Number);
                    }
                    finally
                    {

                        mostrarResultados();                        
                        mConn.Close();

                        dataGridView1.Enabled = false;
                        tssMensagem.Text = "";
                        LimparCampos();
                                         
                        DesabilitarTextBox();

                    }

                }
            }
        }

        private void Gravar()
        {
            // Início da Conexão com indicação de qual o servidor, nome de base de dados a utilizar

            /* É aconselhável criar um utilizador com password. Para acrescentar a password é somente
               necessário acrescentar o seguinte código a seguir ao uid=root;password=xxxxx
             */

            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

            // Abre a conexão
            mConn.Open();
            try
            {
                //Query SQL
            
                MySqlCommand command = new MySqlCommand("INSERT INTO dotacao (Despesa,Reduzida,Programa,Acao)" +
                "VALUES('" + txtCodDespesa.Text + "','" + txtReduzida.Text + "','" + txtPrograma.Text + "','" + txtAcao.Text + "')", mConn);
                // Está representando a sequencia "...VALUES(txtCodDespesa,txtReduzido,...)"

                //Executa a Query SQL
                command.ExecuteNonQuery();
                mostrarResultados();
                // Fecha a conexão
                mConn.Close();

                //Mensagem de Sucesso
                
                // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"
                //LimpaCampos();          
                dataGridView1.Enabled = false;
                tssMensagem.Text = "";

             
                MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DesabilitarTextBox();
                LimparCampos();
                
            
            }
            catch 
            {

              if (txtCodDespesa.Text == "" ||  txtReduzida.Text == "" || txtPrograma.Text == "" || txtAcao.Text == "")
              {

                  MessageBox.Show("Falha na gravação. Verifique se há campos 'em branco'.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  txtCodDespesa.Focus();

              }
              else {

                  MessageBox.Show("Falha na Conexão com Banco de Dados.'", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
              }
                
            }

            LimparCampos();
            mConn.Close();
        }

        
        private void txtCodDespesa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) 
            { 
                txtReduzida.Focus();
           
            }else{
            
                txtCodDespesa.Focus();

            }
            
        }

        private void txtReduzida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPrograma.Focus();
            }
            else
            {
                txtReduzida.Focus();
            }
        }
                
       
        
        private void bt_visualizar_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigoSequencial.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodDespesa.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtReduzida.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbReduzida.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtPrograma.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbPrograma.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtAcao.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            HabilitaTextBox();

        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtCodigoSequencial.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodDespesa.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtReduzida.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbReduzida.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtPrograma.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbPrograma.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtAcao.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            HabilitaTextBox();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigoSequencial.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodDespesa.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtReduzida.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbReduzida.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtPrograma.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbPrograma.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtAcao.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            HabilitaTextBox();

        }

        private void groupBox2_Enter_1(object sender, EventArgs e)
        {

        }

        private void txtCheckCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void txtCheckReduzida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorReduzida(txtCheckReduzida.Text);
                txtCheckReduzida.Text = "";
            }
           
        }

        private void PesquisaPorReduzida(string temp)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
           
            mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao WHERE Reduzida " + "LIKE " + "'%" + temp + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "dotacao");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "dotacao";

                calculaQuantidadeRegistros();

            mConn.Close();
        }

        
        private void PesquisaPorPrograma(string temp)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
           
            mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao WHERE Programa " + "LIKE " + "'%" + temp + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "dotacao");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "dotacao";

             mConn.Close();

        }

        private void txtAcao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtAcao.Text = txtAcao.Text.ToUpper();
                btnOK.Focus();

            }
            else
            {

                txtAcao.Focus();
            }
        }

        private void txtPrograma_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                txtPrograma.Text = txtPrograma.Text.ToUpper();
                txtAcao.Focus();

            }
            else
            {

                txtPrograma.Focus();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void txtCheckReduzida_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtCheckPrograma_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void rbPorCodigo_CheckedChanged_1(object sender, EventArgs e)
        {
            tssMensagem.Text = "ordenando por código";
            mostrarResultados();
        }

        private void rbPorReduzida_CheckedChanged(object sender, EventArgs e)
        {

            tssMensagem.Text = "ordenando por reduzida";
            mostrarResultados();

        }

        private void rbPorPrograma_CheckedChanged(object sender, EventArgs e)
        {

            tssMensagem.Text = "ordenando por programa";
            mostrarResultados();

        }

        private void txtCheckPrograma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                temp = txtCheckPrograma.Text;
                PesquisaPorPrograma(temp);
                txtCheckPrograma.Text = "";
            }

            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }

            calculaQuantidadeRegistros();
        }

        private void rbPorDespesa_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "ordenando por despesa";
            mostrarResultados();
        }

        private void rbPorAcao_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "ordenando por ação";
            mostrarResultados();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtCheckDespesa_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                temp = txtCheckDespesa.Text;
                PesquisaPorDespesa(temp);
                txtCheckDespesa.Text = "";
            }

            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }

            calculaQuantidadeRegistros();
        }

        private void PesquisaPorDespesa(string temp)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            
            mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao WHERE despesa " + "LIKE " + "'%" + temp + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "dotacao");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "dotacao";

            mConn.Close();
        
        }

        private void txtCheckAcao_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                temp = txtCheckAcao.Text;
                PesquisaPorAcao(temp);
                txtCheckAcao.Text = "";
            }

            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }

            calculaQuantidadeRegistros();
        }

        private void PesquisaPorAcao(string temp)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
           
            mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM dotacao WHERE acao " + "LIKE " + "'%" + temp + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "dotacao");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "dotacao";

            mConn.Close();
        
        }

        private void txtCodDespesa_Enter(object sender, EventArgs e)
        {
            txtCodDespesa.BackColor = Color.Yellow;
        }

        private void txtCodDespesa_Leave(object sender, EventArgs e)
        {
            txtCodDespesa.BackColor = Color.White;
        }
        
        private void txtPrograma_Enter(object sender, EventArgs e)
        {
            txtPrograma.BackColor = Color.Yellow;
        }

        private void txtPrograma_Leave(object sender, EventArgs e)
        {
            txtPrograma.BackColor = Color.White;
        }
              
        private void txtCheckReduzida_Leave(object sender, EventArgs e)
        {
            txtCheckReduzida.BackColor = Color.White;
        }

        private void txtCheckReduzida_Enter(object sender, EventArgs e)
        {
            txtCheckReduzida.BackColor = Color.Yellow;
        }

        private void txtCheckPrograma_Leave(object sender, EventArgs e)
        {
            txtCheckPrograma.BackColor = Color.White;
        }

        private void txtCheckPrograma_Enter(object sender, EventArgs e)
        {
            txtCheckPrograma.BackColor = Color.Yellow;
        }

        private void txtCheckDespesa_Leave(object sender, EventArgs e)
        {
            txtCheckDespesa.BackColor = Color.White;
        }

        private void txtCheckDespesa_Enter(object sender, EventArgs e)
        {
            txtCheckDespesa.BackColor = Color.Yellow;
        }

        private void txtCheckAcao_Leave(object sender, EventArgs e)
        {
            txtCheckAcao.BackColor = Color.White;
        }

        private void txtCheckAcao_Enter(object sender, EventArgs e)
        {
            txtCheckAcao.BackColor = Color.Yellow;
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            dataGridView1.BackgroundColor = Color.Yellow;
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            dataGridView1.BackgroundColor = Color.White;
        }

        private void btn_ativa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Enabled == true)
                dataGridView1.Enabled = false;
            else
                dataGridView1.Enabled = true;
        }

        private void cmbPrograma_SelectedValueChanged(object sender, EventArgs e)
        {
            txtPrograma.Text = cmbPrograma.Text;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void cmbReduzida_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            enquadramento();

           
        }

        private void enquadramento()
        {
            txtReduzida.Text = cmbReduzida.Text;
            cmbPrograma.Text = "";

            try
            {

                if ((Convert.ToInt32(cmbReduzida.Text) >= 692) && (Convert.ToInt32(cmbReduzida.Text) <= 699))
                {
                    txtPrograma.Text = "Adm";
                    cmbPrograma.Text = "ADM";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 700 && Convert.ToInt32(cmbReduzida.Text) <= 703)
                {
                    txtPrograma.Text = "Obras Ampl";
                    cmbPrograma.Text = "OBRAS AMPL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 709 && Convert.ToInt32(cmbReduzida.Text) <= 713)
                {
                    txtPrograma.Text = "Atenção Básica";
                    cmbPrograma.Text = "AB";


                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 718 && Convert.ToInt32(cmbReduzida.Text) <= 724)
                {
                    txtPrograma.Text = "ODONTO.SAUDE BUCAL";
                    cmbPrograma.Text = "ODONTO.SAUDE BUCAL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 725 && Convert.ToInt32(cmbReduzida.Text) <= 727)
                {
                    txtPrograma.Text = "PSF";
                    cmbPrograma.Text = "PSF";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 731 && Convert.ToInt32(cmbReduzida.Text) <= 732)
                {
                    txtPrograma.Text = "ODONTO.SAUDE BUCAL";
                    cmbPrograma.Text = "ODONTO.SAUDE BUCAL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) == 733)
                {
                    txtPrograma.Text = "Obras Ampliação";
                    cmbPrograma.Text = "OBRAS AMPL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 740 && Convert.ToInt32(cmbReduzida.Text) <= 745)
                {
                    txtPrograma.Text = "Emergencia";
                    cmbPrograma.Text = "EMERG";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 734 && Convert.ToInt32(cmbReduzida.Text) <= 735)
                {
                    txtPrograma.Text = "Especialidades";
                    cmbPrograma.Text = "Especialidades";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 750 && Convert.ToInt32(cmbReduzida.Text) <= 758)
                {
                    txtPrograma.Text = "Especialidades";
                    cmbPrograma.Text = "Especialidades";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 759 && Convert.ToInt32(cmbReduzida.Text) <= 761)
                {
                    txtPrograma.Text = "Med Alimen";
                    cmbPrograma.Text = "MED ALIMEN";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 767 && Convert.ToInt32(cmbReduzida.Text) <= 774)
                {
                    txtPrograma.Text = "VISA";
                    cmbPrograma.Text = "VISA";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 779 && Convert.ToInt32(cmbReduzida.Text) <= 793)
                {
                    txtPrograma.Text = "VISE";
                    cmbPrograma.Text = "VISE";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 17047)
                {
                    txtPrograma.Text = "Emergência";
                    cmbPrograma.Text = "EMERG";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 17048)
                {
                    txtPrograma.Text = "Obras Ampliação";
                    cmbPrograma.Text = "OBRAS AMPL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 17165 && Convert.ToInt32(cmbReduzida.Text) <= 17167)
                {
                    txtPrograma.Text = "Atenção Básica";
                    cmbPrograma.Text = "AB";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 17169 && Convert.ToInt32(cmbReduzida.Text) <= 17170)
                {
                    txtPrograma.Text = "PSF";
                    cmbPrograma.Text = "PSF";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) >= 17171 && Convert.ToInt32(cmbReduzida.Text) <= 17172)
                {
                    txtPrograma.Text = "Obras Ampliação";
                    cmbPrograma.Text = "OBRAS AMPL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 17184 && Convert.ToInt32(cmbReduzida.Text) <= 17185)
                {
                    txtPrograma.Text = "Obras Ampliação";
                    cmbPrograma.Text = "OBRAS AMPL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) == 17657)
                {
                    txtPrograma.Text = "Emergência";
                    cmbPrograma.Text = "EMERG";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 18288)
                {
                    txtPrograma.Text = "Obras Ampliação";
                    cmbPrograma.Text = "OBRAS AMPL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 18890 && Convert.ToInt32(cmbReduzida.Text) <= 18891)
                {
                    txtPrograma.Text = "ODONTO.SAUDE BUCAL";
                    cmbPrograma.Text = "ODONTO.SAUDE BUCAL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 18892 && Convert.ToInt32(cmbReduzida.Text) <= 18894)
                {
                    txtPrograma.Text = "Obras Ampliação";
                    cmbPrograma.Text = "OBRAS AMPL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 18295)
                {
                    txtPrograma.Text = "Especialidades";
                    cmbPrograma.Text = "Especialidades";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 18297 && Convert.ToInt32(cmbReduzida.Text) <= 18298)
                {
                    txtPrograma.Text = "ODONTO.SAUDE BUCAL";
                    cmbPrograma.Text = "ODONTO.SAUDE BUCAL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 18299 && Convert.ToInt32(cmbReduzida.Text) <= 18301)
                {
                    txtPrograma.Text = "Med Alimen";
                    cmbPrograma.Text = "MED ALIMEN";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 18303 && Convert.ToInt32(cmbReduzida.Text) <= 18306)
                {
                    txtPrograma.Text = "VISA";
                    cmbPrograma.Text = "VISA";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 18307 && Convert.ToInt32(cmbReduzida.Text) <= 18315)
                {
                    txtPrograma.Text = "VISE";
                    cmbPrograma.Text = "VISE";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 29216)
                {
                    txtPrograma.Text = "ODONTO.SAUDE BUCAL";
                    cmbPrograma.Text = "ODONTO.SAUDE BUCAL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 29261)
                {
                    txtPrograma.Text = "ODONTO.SAUDE BUCAL";
                    cmbPrograma.Text = "ODONTO.SAUDE BUCAL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 29322)
                {
                    txtPrograma.Text = "ODONTO.SAUDE BUCAL";
                    cmbPrograma.Text = "ODONTO.SAUDE BUCAL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 29910)
                {
                    txtPrograma.Text = "ODONTO.SAUDE BUCAL";
                    cmbPrograma.Text = "ODONTO.SAUDE BUCAL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 29219)
                {
                    txtPrograma.Text = "ADM";
                    cmbPrograma.Text = "ADM";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) == 29318)
                {
                    txtPrograma.Text = "ADM";
                    cmbPrograma.Text = "ADM"; ;
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 29343)
                {
                    txtPrograma.Text = "ADM";
                    cmbPrograma.Text = "ADM";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 29262)
                {
                    txtPrograma.Text = "Especialidades";
                    cmbPrograma.Text = "Especialidades";
                }
                else
                {
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 29263)
                {
                    txtPrograma.Text = "Especialidades";
                    cmbPrograma.Text = "Especialidades";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) == 29322)
                {
                    txtPrograma.Text = "Especialidades";
                    cmbPrograma.Text = "Especialidades";
                }
                else
                {
                    cmbPrograma.Text = "";
                }


                if (Convert.ToInt32(cmbReduzida.Text) == 29425)
                {
                    txtPrograma.Text = "Especialidades";
                    cmbPrograma.Text = "Especialidades";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) >= 29264 && Convert.ToInt32(cmbReduzida.Text) <= 29271)
                {
                    txtPrograma.Text = "VISE";
                    cmbPrograma.Text = "VISE";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

                if (Convert.ToInt32(cmbReduzida.Text) == 30614 && Convert.ToInt32(cmbReduzida.Text) == 17048)
                {
                    txtPrograma.Text = "Obras Ampliação";
                    cmbPrograma.Text = "OBRAS AMPL";
                }
                else
                {
                    cmbPrograma.Text = "";
                }

            }
            catch
            {
                MessageBox.Show("Escolha fora da faixa!", "Informação");

            }

        }

        private void txtReduzida_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbReduzida_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"C:/D.O.pdf");
                                
            }
            catch
            {

                MessageBox.Show("Verifique se há o arquivo 'D.O.pdf' em 'C:/'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"C:/Reduzidas.pdf");
            }
            catch
            {
                MessageBox.Show("Verifique se há o arquivo 'Reduzidas.pdf' em 'C:/'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void cmbReduzida_KeyPress(object sender, KeyPressEventArgs e)
        {           
                if (e.KeyChar == 13)
                {
                    enquadramento();
                    /*
                    if (MessageBox.Show("Confirma a inclusão da DESPESA REDUZIDA na listagem?", "Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        cmbReduzida.Items.Add(cmbReduzida.Text);
                        MessageBox.Show("DESPESA REDUZIDA adicionada na listagem", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                    else
                    {
                        MessageBox.Show("DESPESA REDUZIDA não adicionada na listagem", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }  txtReduzida.Text = cmbReduzida.Text;
                    */
                }              
        }

        private void verificaListaComboBox()
        {
            int quant = cmbReduzida.Items.Count;
            int i = 0;
            
            //string [] relacao = new string[quant];
            
            // populando o vetor
            /*
            for (int j=0; j < quant; j++) {

                relacao[j] = cmbReduzida.GetItemText(j);
            
            }
             */

            while (i != quant)
            {
                if (cmbReduzida.Text != cmbReduzida.GetItemText(i))
                {
                    textBox3.Text = quant.ToString();
                    textBox4.Text = cmbReduzida.GetItemText(i);
                    i++;
                }
                else 
                {
                    if (MessageBox.Show("Confirma a inclusão da DESPESA REDUZIDA na listagem?", "Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        cmbReduzida.Items.Add(cmbReduzida.Text);
                        MessageBox.Show("DESPESA REDUZIDA adicionada na listagem", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("DESPESA REDUZIDA não adicionada na listagem", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    i = quant; // se acha a despesa informada uma única vez iguala os valores pra sair do while

                }      
            }           

        }
        
        private void btRelatorio_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dataGridView1);
        }

        private void btnReduzida_Click(object sender, EventArgs e)
        {
            // para inserir na última posição
            int cnt = cmbReduzida.Items.Count;

            if (cmbReduzida.Text != String.Empty)
            {
                cmbReduzida.Items.Insert(cnt, cmbReduzida.Text);
            }
            else
            {
                cmbReduzida.Items.Insert(cnt, "Item " + cnt);
            }
            //txtItens.Text = ""; //limpa a caixa de texto após a inclusão
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmbReduzida.Items.Remove(cmbReduzida.SelectedItem);
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = true;
            flagInclusao = 2;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = true;
            flagInclusao = 0;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            flagInclusao = 1;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = true;
            HabilitaTextBox();
            txtReduzida.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Campo obrigatório na inclusão: Identificação do Fornecedor

            // O botão OK serve tanto para inclusão quanto para alteração  e exclusão de dados de despesas
            // Foi criada um variável flag para informar o que estamos fazendo: Inclusão,alteração ou exclusão.
            
            if (flagInclusao == 1)
            {
                if (txtReduzida.Text == "")
                {
                    MessageBox.Show("Você tem que informar qual a despesa reduzida para ser incluída.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtReduzida.Focus();
                    btnOK.Visible = true;
                }else{
                    if (txtCodDespesa.Text==""){
                        MessageBox.Show("Você tem que informar qual a despesa principal para ser incluída.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        txtCodDespesa.Focus();
                        btnOK.Visible = true;
                    }else{
                         if (cmbPrograma.Text==""){
                            MessageBox.Show("Você tem que informar a qual programa a despesa pertence ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            cmbPrograma.Focus();
                           btnOK.Visible = true;                    
                         }else{
                             if (txtAcao.Text==""){
                                 MessageBox.Show("Você tem que informar a qual programa a despesa pertence ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                 txtAcao.Focus();
                                 btnOK.Visible=true;
                             }else{
                                    btnOK.Visible = false;
                                    Gravar();
                                    btnIncluir.Enabled = true;
                                    btnAlterar.Enabled = true;
                                    btnExcluir.Enabled = true;
                             }
                         }
                    }
                }
            }     
            else{
            // essa linha só serve para casos de alteração de dados e exclusão

                if (flagInclusao == 0)
                {
                    if (txtCodigoSequencial.Text == ""){
                    
                        MessageBox.Show("Escolha na planilha a despesa cujos dados devam ser alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        btnOK.Visible = true;
                        btnOK.Enabled = true;
                    }
                    else
                    {
                        btnOK.Visible = false;
                        codigo = Convert.ToInt32(txtCodigoSequencial.Text);
                        Alterar(codigo);
                        btnIncluir.Enabled = true;
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                    }
                
                }
                else
                {
                    if (txtCodigoSequencial.Text == "")
                    {
                        MessageBox.Show("Escolha na planilha a despesa cujos dados devam ser excluídos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        btnOK.Visible = true;
                        btnOK.Enabled = true;
                    }
                    else
                    {
                        btnOK.Visible = false;
                        codigo = Convert.ToInt32(txtCodigoSequencial.Text);
                        Excluir(codigo);
                        btnIncluir.Enabled = true;
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                    }
                }
                
            }
        
                        
        }

        private void txtReduzida_Enter(object sender, EventArgs e)
        {
            txtReduzida.BackColor = Color.Yellow;
        }

        private void txtReduzida_Leave(object sender, EventArgs e)
        {
            txtReduzida.BackColor = Color.White;
        }

        private void txtCodDespesa_Enter_1(object sender, EventArgs e)
        {
            txtCodDespesa.BackColor = Color.Yellow;
        }

        private void txtCodDespesa_Leave_1(object sender, EventArgs e)
        {
            txtCodDespesa.BackColor = Color.White;
        }

        private void cmbPrograma_Enter(object sender, EventArgs e)
        {
            cmbPrograma.BackColor = Color.Yellow;
        }

        private void cmbPrograma_Leave(object sender, EventArgs e)
        {
            cmbPrograma.BackColor = Color.White;
        }

        private void txtAcao_Enter(object sender, EventArgs e)
        {
            txtAcao.BackColor = Color.Yellow;

        }

        private void txtAcao_Leave(object sender, EventArgs e)
        {
            txtAcao.BackColor = Color.White;

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtReduzida_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {

                if (txtReduzida.Text == "")                {

                    txtReduzida.Focus();
                }
                else {

                    txtCodDespesa.Focus();
                }
            }
        }

        private void txtCodDespesa_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                if (txtCodDespesa.Text == "")
                {

                    txtCodDespesa.Focus();
                }
                else
                {

                    cmbPrograma.Focus();
                }
            }
        }

        private void cmbPrograma_SelectedValueChanged_1(object sender, EventArgs e)
        {
            if (cmbPrograma.Text == "")
                {

                    cmbPrograma.Focus();
                }
                else
                {

                    txtAcao.Focus();
                }
            }

        private void cmbPrograma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (cmbPrograma.Text == "")
                {
                    cmbPrograma.Focus();
                }
                else
                {
                    txtAcao.Focus();
                }
            }
        }

        private void txtAcao_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtAcao.Text == "")
                {
                    txtAcao.Focus();
                }
                else
                {
                    btnOK.Focus();
                }
            }
        }
                                      
    }       
}
