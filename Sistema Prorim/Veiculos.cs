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
    public partial class Veiculos : Form
    {
        
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        String temp="";
        int codigo = 0;
        public string stConection;
        private string stConsulta;
        public MySqlConnection Cmn = new MySqlConnection();
    

        public Veiculos()
        {
            InitializeComponent();
        }

        private void Veiculos_Load(object sender, EventArgs e)
        {

            DesabilitaTextBox();
            tssMensagem.Text = "tela para consultas, inclusão, alteração e exclusão de dados";
            // populando cmbUnidade
            //------------------------------------------------------

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();
            mAdapter = new MySqlDataAdapter("SELECT Nome_Unidade FROM unidade ORDER BY Nome_unidade", mConn);
            DataTable unidade = new DataTable();
            mAdapter.Fill(unidade);
            try
            {
                for (int i = 0; i < unidade.Rows.Count; i++)
                {
                    cmbUnidade.Items.Add(unidade.Rows[i]["Nome_Unidade"]);
                    comboBox1.Items.Add(unidade.Rows[i]["Nome_Unidade"]);
                    comboBox2.Items.Add(unidade.Rows[i]["NOme_Unidade"]);
                    cmbLotacao.Items.Add(unidade.Rows[i]["Nome_Unidade"]);
                }
            }
            catch 
            {
                MessageBox.Show("ERRO DE CONEXÂO [form load]", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
            }

            //------------------------------------------------------
            
            mostrarResultados();
            
        }

        private void mostrarResultados()
        {
            try
            {
                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                if (rbPorCodigo.Checked == true)
                    // ordena a tabela de acordo com o critério estabelecido
                    mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos ORDER BY Cod_seq_veiculo", mConn);
                else
                    if (rbPorNome.Checked == true)
                        mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos ORDER BY Setor_gestor", mConn);
                    else
                        mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos ORDER by placa", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "veiculos");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "veiculos";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Codigo Sequencial";
                dataGridView1.Columns[1].HeaderText = "Unidade Gestora";
                dataGridView1.Columns[2].HeaderText = "Placa";
                dataGridView1.Columns[3].HeaderText = "Lotação";
                dataGridView1.Columns[4].HeaderText = "Marca";
                dataGridView1.Columns[5].HeaderText = "Modelo";
                dataGridView1.Columns[6].HeaderText = "Ano";
                dataGridView1.Columns[7].HeaderText = "Unidade";
                dataGridView1.Columns[7].Visible=false;

                contandoRegistros();

                /*
                int registro;
                registro = dataGridView1.RowCount - 1;
                if (registro == 1)
                    label9.Text = registro + " registro";
                else
                    label9.Text = registro + " registros";
                 */
            }
            catch {

                MessageBox.Show("Falha na conexão com Banco de Dados[mostra resultado].", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void contandoRegistros()
        {
            int registro;
            registro = dataGridView1.RowCount - 1;
            if (registro == 1)
                label9.Text = registro + " registro";
            else
                label9.Text = registro + " registros";
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
                        cmd.CommandText = "delete FROM veiculos where Cod_seq_veiculo = " + codigo;
                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível excluir a veiculo " + codigo);
                        }
                    }
                    /*catch (MySqlException ex)
                    {
                        throw new Exception("Servidor SQL Erro:" + ex.Number);
                    }*/
                    catch
                    {
                        MessageBox.Show("Falha na conexão com Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    finally
                    {
                        mConn.Close();
                        mostrarResultados();
                    }

                    UncheckedRadioButtons();
                    HabilitaRadionButtons();
                    LimpaCampos();
                    MessageBox.Show("Excluído veículo nr. " + codigo + " com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbUnidade.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            mskPlaca.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbLotacao.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtMarca.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtModelo.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtAno.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodUnidade.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();

          
            HabilitaTextBox();
        }

        private void HabilitaTextBox()
        {
            txtCodigo.Enabled = true;
            cmbUnidade.Enabled = true;
            mskPlaca.Enabled = true;
            cmbLotacao.Enabled = true;
            txtMarca.Enabled = true;
            txtModelo.Enabled = true;
            txtAno.Enabled = true;
           
        }

        private void PesquisaPorCodigo(int codigo)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 

            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos Where Cod_seq_veiculo=" + codigo, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "veiculos");
            
            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "veiculos";
            
        }

        private void bt_visualizar_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }        

        private void UncheckedRadioButtons()
        {
            rbNovo.Checked = false;
            rbAlterar.Checked = false;
            rbExclui.Checked = false;
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
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = mConn;

                        // Vamos deixar essa codificação abaixo - que não funcionou - para comparação com a que funciona logo abaixo.
                        //cmd.CommandText = "UPDATE fornecedor SET Nome_fornecedor=" + "'" + txtFornecedor.Text + "'," + " End_fornecedor =" + "'" + txtEndereço + "'," +
                        //" Fone1_fornecedor =" + "'" + txtFone1 + "'," + " Fone2_fornecedor =" + "'" + txtFone2 + "'," + " Email_fornecedor =" + "'" + txtEmail + "'" + "Where Cod_fornecedor = " + codigo;

                        cmd.CommandText = "UPDATE veiculos SET setor_gestor ='" + cmbUnidade.Text + "'," + "placa='" + mskPlaca.Text + "'," + "Lotacao='" 
                            + cmbLotacao.Text +  "'," + "marca='" + txtMarca.Text + "'," + "modelo='" + txtModelo.Text + "'," + "ano='" + txtAno.Text + "'," 
                            + "Cod_unidade='" + txtCodUnidade.Text + "'" + "WHERE Cod_seq_veiculo=" + codigo;

                        dataGridView1.Enabled = false; 
                        MessageBox.Show("Registro '" + codigo + "'" + " Alterado com sucesso.");

                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível alterar os dados da 'Unidade' " + codigo);
                        }

                    }
                    /*catch (MySqlException ex)
                    {
                        throw new Exception("Servidor SQL Erro:" + ex.Number);
                    }*/
                    catch
                    {
                        MessageBox.Show("UPDATE veiculos SET setor_gestor ='" + cmbUnidade.Text + "'," + "placa='" + mskPlaca.Text + "'," + "Lotacao='" 
                        + cmbLotacao.Text + "marca='" + txtMarca.Text + "'," + "modelo='" + txtModelo.Text + "'," + "ano='" + txtAno.Text + "'," 
                        + "Cod_unidade='" + txtCodUnidade.Text + "'" + "WHERE Cod_seq_veiculo=" + codigo, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    finally
                    {
                        mostrarResultados();
                        LimpaCampos();
                        DesabilitaTextBox();
                        UncheckedRadioButtons();
                        HabilitaRadionButtons();
                        mConn.Close();
                    }
                }
            }
        }

        private void HabilitaRadionButtons()
        {
            rbNovo.Enabled = true;
            rbAlterar.Enabled = true;
            rbExclui.Enabled = true;
        }

        private void DesabilitaTextBox()
        {
            txtCodigo.Enabled = false;
            mskPlaca.Enabled = false;
            cmbUnidade.Enabled = false;
            txtMarca.Enabled = false;
            txtModelo.Enabled = false;
            txtAno.Enabled = false;
            cmbLotacao.Enabled = false;
        }

        private void LimpaCampos()
        {
            txtCodigo.Text= "";
            mskPlaca.Text = "";
            cmbUnidade.Text = "";
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtAno.Text = "";
            txtCodUnidade.Text = "";
            cmbLotacao.Text = "";
        }

        private void Gravar()
        {

            // Início da Conexão com indicação de qual o servidor, nome de base de dados a utilizar

            /* É aconselhável criar um utilizador com password. Para acrescentar a password é somente
               necessário acrescentar o seguinte código a seguir ao uid=root;password=xxxxx
             */

            tssMensagem.Text = "";

            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

            // Abre a conexão
            mConn.Open();

            //Query SQL
            MySqlCommand command = new MySqlCommand("INSERT INTO veiculos (setor_gestor,placa,lotacao,marca,modelo,ano,cod_unidade)" +
            "VALUES('" + cmbUnidade.Text + "','" + mskPlaca.Text + "','" + cmbLotacao.Text + "','" + txtMarca.Text + "','" + txtModelo.Text + "','" + txtAno.Text + "','" + txtCodUnidade.Text + "')", mConn);
            // Esta representando a sequencia "...VALUES(txtSetor,txtEndereço,...)"

            //Executa a Query SQL
            command.ExecuteNonQuery();

            // Fecha a conexão
            mConn.Close();

           
            // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"
            //Mensagem de Sucesso
            MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpaCampos();
                       
            mostrarResultados();
            UncheckedRadioButtons();
            HabilitaRadionButtons();
            DesabilitaTextBox(); 
        }


        private void DesabilitaRadioButtons()
        {
            rbNovo.Enabled = false;
            rbAlterar.Enabled = false;
            rbExclui.Enabled = false;
        }



        private void txtSetor_KeyPress(object sender, KeyPressEventArgs e)
        {

            
        }

        private void txtEndereço_KeyPress(object sender, KeyPressEventArgs e)
        {

            
        }

        private void txtFone1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtFone2_KeyPress(object sender, KeyPressEventArgs e)
        {

            
        }

        private void txtResp_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void txtSetor_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtCheckCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtFone2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

        }



        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtMarca.Text = txtMarca.Text.ToUpper();
                txtModelo.Focus();
            }
            else
            {
                txtMarca.Focus();
            }

        }

        private void txtModelo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtModelo.Text = txtModelo.Text.ToUpper();
                txtAno.Focus();
            }
            else
            {
                txtModelo.Focus();
            }

        }

        
        private void rbNovo_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "modulo de inclusão ativado. Entre com as informações e clique botão 'OK' ou 'Cancelar'";
            //dataGridView1.Enabled = false;
            HabilitaTextBox();
            cmbUnidade.Focus();
            DesabilitaRadioButtons();
            bt_Gravar.Enabled = true;
        }

        private void rbAlterar_CheckedChanged(object sender, EventArgs e)
        {
                        
            if (rbAlterar.Checked == true)
            {
                MessageBox.Show("Clique no Grid 'Veículos' na linha a ser alterada.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tssMensagem.Text = "módulo ALTERAÇÃO ativado...";
                
                dataGridView1.Enabled = true;
                //rbAlterar.Checked = false;

                textBox1.Visible = false;
                textBox2.Visible = false;
                label6.Visible = false;

                //HabilitaTextBox();
                DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                btnAtualizar.Enabled = false;
                DesabilitaTextBox();
            }
            else
            {

            }
            //HabilitaTextBox();
            cmbUnidade.Focus();
            DesabilitaRadioButtons();
            bt_Gravar.Enabled = false;
        }

        private void rbExclui_CheckedChanged(object sender, EventArgs e)
        {
            
            tssMensagem.Text = "modulo de exclusão ativado. Escolha o item e clique botão 'OK' ou 'Cancelar'";

            if (rbExclui.Checked == true)
            {
                MessageBox.Show("Clique no Grid 'Veículos' na linha correspondente a ser excluída.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //rbAlterar.Checked = false;
                dataGridView1.Enabled = true;
                textBox1.Visible = false;
                textBox2.Visible = false;
                label6.Visible = false;

                //HabilitaTextBox();
                DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                btnAtualizar.Enabled = false;
                DesabilitaTextBox();
            }
            else
            {
            }

        }

        private void bt_Gravar_Click(object sender, EventArgs e)
        {
            tssMensagem.Text = "";

            if (rbNovo.Checked == true)
            {
                if (cmbUnidade.Text == "")
                {
                    MessageBox.Show("Deve ser escolhida a unidade responsável pelo veículo", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbUnidade.Focus();
                }
                else
                {
                    if (mskPlaca.Text == "")
                    {
                        MessageBox.Show("Campo Placa de preenchimento obrigatório.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskPlaca.Focus();
                    }
                    else
                    {
                        if (txtMarca.Text == "")
                        {
                            MessageBox.Show("Campo Marca de preenchimento obrigatório.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMarca.Focus();
                        }
                        else
                        {
                            if (txtModelo.Text == "")
                            {
                                MessageBox.Show("Campo Modelo de preenchimento obrigatório.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtModelo.Focus();
                            }
                            else
                            {

                                if (txtAno.Text == "")
                                {
                                    MessageBox.Show("Campo Ano de preenchimento obrigatório.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtAno.Focus();
                                }
                                else
                                {

                                    Gravar();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (txtCodigo.Text == "")
                {
                    MessageBox.Show("Não foram definidos dados para exclusão", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
                }
                else
                {
                    codigo = Convert.ToInt32(txtCodigo.Text);
                    if (rbAlterar.Checked == true)
                        Alterar(codigo);
                    else
                        Excluir(codigo);
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            //tssMensagem.Text = "operação cancelada";
            UncheckedRadioButtons();
            bt_Gravar.Enabled = false;
            btnAtualizar.Enabled = false;
            LimpaCampos();
            DesabilitaTextBox();
            HabilitaRadionButtons();
            tssMensagem.Text = "operação cancelada";

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            UncheckedRadioButtons();
            bt_Gravar.Enabled = false;
            btnAtualizar.Enabled = false;
            LimpaCampos();
            DesabilitaTextBox();
            HabilitaRadionButtons();
            this.Close();
        }

        private void cmbUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter_1(object sender, EventArgs e)
        {

        }


        private void txtCheckCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtCheckCodigo.Text != "")
                {
                    temp = txtCheckCodigo.Text;
                    codigo = Convert.ToInt32(temp);
                    PesquisaPorCodigo(codigo);
                    txtCheckCodigo.Text = "";
                }
                else
                {

                }

            }

            contandoRegistros();
        }

        private void PesquisaPorUnidade(TextBox txtCheckUnidade)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos WHERE Cod_unidade= " , mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "veiculos");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "veiculos";
        
        }

        
        private void txtCheckUnidade_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                try
                {

                    stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT * FROM unidade WHERE Nome_unidade='" + comboBox1.Text + "'";

                    MySqlCommand myCmd = new MySqlCommand();
                    myCmd.Connection = Cmn;
                    myCmd.CommandText = stConsulta;
                    MySqlDataReader myReader = myCmd.ExecuteReader();

                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            myReader.Read();
                            textBox3.Text = myReader["Cod_unidade"] + Environment.NewLine;
                        }
                    }

                    //------------


                    if (textBox3.Text != "")
                    {
                        PesquisaPorUnidade(textBox3.Text);
                        textBox3.Text = "";
                    }
                    else
                    {

                    }

                    //-------------
                }
                catch
                {
                    MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                Cmn.Close();

                
            }
            else
            {
                textBox3.Focus();
            }

            contandoRegistros();
        }

        private void PesquisaPorUnidade(string p)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos WHERE Cod_unidade= " + p, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "veiculos");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "veiculos";
        
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
               try
                {

                    stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT * FROM unidade WHERE Nome_unidade='" + comboBox1.Text + "'";

                    MySqlCommand myCmd = new MySqlCommand();
                    myCmd.Connection = Cmn;
                    myCmd.CommandText = stConsulta;
                    MySqlDataReader myReader = myCmd.ExecuteReader();

                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            myReader.Read();
                            textBox3.Text = myReader["Cod_unidade"] + Environment.NewLine;
                        }
                    }

                    //------------


                    if (textBox3.Text != "")
                    {
                        PesquisaPorUnidade(textBox3.Text);
                        textBox3.Text = "";
                    }
                    else
                    {

                    }

                    //-------------
                }
                catch
                {
                    MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                
                
                Cmn.Close();

                contandoRegistros();

        }

        private void txtCheckPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                temp = txtCheckPlaca.Text;
                PesquisaPorPlaca(temp);
                
            }

            else
            {

            }

            contandoRegistros();
        }

        private void PesquisaPorPlaca(string temp)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos WHERE placa " + "LIKE " + "'%" + temp + "%'", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "veiculos");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "veiculos";
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lblUnidade_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void lblPlaca_Click(object sender, EventArgs e)
        {

        }

        private void txtCheckPlaca_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCodigo_Click(object sender, EventArgs e)
        {

        }

        private void bt_visualizar_Click_1(object sender, EventArgs e)
        {

        }

        private void txtCheckCodigo_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void mskPlaca_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void cmbUnidade_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void txtCodUnidade_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {


        }

        private void lblMarca_Click(object sender, EventArgs e)
        {

        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAtualizar_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void txtAno_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            bt_Gravar.Enabled = true;

            txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbUnidade.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            mskPlaca.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbLotacao.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtMarca.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtModelo.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtAno.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodUnidade.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            
            if (rbAlterar.Checked == true)
            {
                dataGridView1.Enabled = true;
                HabilitaTextBox();
            }
            else
            {               
                if (rbExclui.Checked == true)
                {
                    dataGridView1.Enabled = true;
                    //HabilitaTextBox();
                }
                else
                {
                    dataGridView1.Enabled = false;
                }
            }
                        
        }

        private void cmbUnidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {

                try
                {

                    stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT * FROM unidade WHERE Nome_unidade='" + cmbUnidade.Text + "'";

                    MySqlCommand myCmd = new MySqlCommand();
                    myCmd.Connection = Cmn;
                    myCmd.CommandText = stConsulta;
                    MySqlDataReader myReader = myCmd.ExecuteReader();

                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            myReader.Read();
                            txtCodUnidade.Text = myReader["Cod_unidade"] + Environment.NewLine;
                        }
                    }

                }
                catch
                {
                    MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                Cmn.Close();

                mskPlaca.Focus();
            }
            else
            {
                cmbUnidade.Focus();
            }
            
        }

        private void cmbUnidade_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                stConsulta = "SELECT * FROM unidade WHERE Nome_unidade='" + cmbUnidade.Text + "'";

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        txtCodUnidade.Text = myReader["Cod_unidade"] + Environment.NewLine;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            Cmn.Close();

            mskPlaca.Focus();
            

        }

        private void txtCheckCodigo_Enter(object sender, EventArgs e)
        {
            txtCheckCodigo.BackColor = Color.Yellow;
            LimpaCamposFiltros();
          
        }

        private void LimpaCamposFiltros()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            txtCheckCodigo.Text = "";
            txtCheckPlaca.Text = "";
        }

        private void txtCheckPlaca_Enter(object sender, EventArgs e)
        {
            txtCheckPlaca.BackColor = Color.Yellow;
            LimpaCamposFiltros();
        }

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            txtCodigo.BackColor = Color.Yellow;
        }

       
        private void txtMarca_Enter(object sender, EventArgs e)
        {
            txtMarca.BackColor = Color.Yellow;
        }

        private void txtModelo_Enter(object sender, EventArgs e)
        {
            txtModelo.BackColor = Color.Yellow;
        }

        private void txtCheckCodigo_Leave(object sender, EventArgs e)
        {
            txtCheckCodigo.BackColor = Color.White;
        }

        private void txtCheckPlaca_Leave(object sender, EventArgs e)
        {
            txtCheckPlaca.BackColor = Color.White;
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            txtCodigo.BackColor = Color.White;
        }

    

        private void txtMarca_Leave(object sender, EventArgs e)
        {
            txtMarca.BackColor = Color.White;
            txtMarca.Text = txtMarca.Text.ToUpper();
        }

        private void txtModelo_Leave(object sender, EventArgs e)
        {
            txtModelo.BackColor = Color.White;
            txtModelo.Text = txtModelo.Text.ToUpper();
        }

        private void cmbUnidade_Enter(object sender, EventArgs e)
        {
            cmbUnidade.BackColor = Color.Yellow;
        }

        private void cmbUnidade_Leave(object sender, EventArgs e)
        {
            cmbUnidade.BackColor = Color.White;
            cmbUnidade.Text = cmbUnidade.Text.ToUpper();
        }

        private void txtAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            bt_Gravar.Focus();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cmbUnidade_SelectedIndexChanged_2(object sender, EventArgs e)
        {
                
        }

        private void cmbLotacao_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbLotacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtMarca.Focus();
            }
            else
            {
                cmbLotacao.Focus();
            }
        }

        private void cmbLotacao_SelectedValueChanged(object sender, EventArgs e)
        {
            txtMarca.Focus();
        }

        private void cmbLotacao_Enter(object sender, EventArgs e)
        {
            cmbLotacao.BackColor = Color.Yellow;
        }

        private void cmbLotacao_Leave(object sender, EventArgs e)
        {
            cmbLotacao.BackColor = Color.White;
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            PesquisaLotacao(comboBox2.Text);
        }

        private void PesquisaLotacao(string p)
        {
            try
            {
                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos WHERE lotacao " + "LIKE " + "'%" + p + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "veiculos");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "veiculos";
            }
            catch 
            {
                MessageBox.Show("Não foi possível fazer a consulta [Pesquisa por Lotacao].", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

                   
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesquisaLotacao(comboBox2.Text);
            }
            else 
            {
                comboBox2.Focus();    
            
            }
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            LimpaCamposFiltros();
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            LimpaCamposFiltros();
        }

        private void rbPorCodigo_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "dados ordenados por código";
            mostrarResultados();
        }

        private void rbPorNome_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "dados ordenados por unidade";
            mostrarResultados();
        }

        private void rbPorContato_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "dados ordenados por placa";
            mostrarResultados();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mskPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                mskPlaca.Text = mskPlaca.Text.ToUpper();
                cmbLotacao.Focus();
            }
            else
            {
                mskPlaca.Focus();
            }

            contandoRegistros();
        }

        private void mskPlaca_Enter(object sender, EventArgs e)
        {
            mskPlaca.BackColor = Color.Yellow;
        }

        private void mskPlaca_Leave(object sender, EventArgs e)
        {
            mskPlaca.BackColor = Color.White;
            mskPlaca.Text = mskPlaca.Text.ToUpper();
        }

        private void mskPlaca_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mskCheckPlaca_Enter(object sender, EventArgs e)
        {
            txtCheckPlaca.BackColor = Color.Yellow;
            LimpaCamposFiltros();

        }

        private void mskCheckPlaca_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mskCheckPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                temp = mskCheckPlaca.Text;
                PesquisaPorPlaca(temp);

            }

            else
            {

            }

            contandoRegistros();
        }

        private void mskCheckPlaca_Leave(object sender, EventArgs e)
        {
            txtCheckPlaca.BackColor = Color.White;
        }
                     
    }
}

