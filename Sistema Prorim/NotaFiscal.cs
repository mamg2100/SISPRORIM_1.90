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
    public partial class NotaFiscal : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        String temp;
        int codigo = 0;
        public string stConection;
        public String situacao;
        private string stConsulta;
        public MySqlConnection Cmn = new MySqlConnection();

        
        public NotaFiscal()
        {
            InitializeComponent();
        }

        private void NotaFiscal_Load(object sender, EventArgs e)
        {            
            txtCodFornecedor.Text = Global.NotaFiscal.fornecedor;
            txtCodRim.Text = Global.NotaFiscal.codigoRI;
            txtFornecedor.Text = Global.NotaFiscal.nomefornecedor;
            rbPorCodFornecedor.Checked = true;
            mostrarResultados();
        }

        private void mostrarResultados()
         
        {
                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                    if (rbPorCodigoSeq.Checked == true){   // ordena a tabela de acordo com o critério estabelecido
                        mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais where Cod_rim='" + txtCodRim.Text + "' ORDER BY Cod_notas_fiscais", mConn);
                    }else
                        if (rbPorNumeroNota.Checked == true){
                            mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais where Cod_rim='" + txtCodRim.Text + "' ORDER BY Num_NotaFiscal", mConn);
                        }else
                            if (rbPorCodFornecedor.Checked == true){
                                mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais where Cod_rim='" + txtCodRim.Text + "' ORDER BY Cod_fornecedor", mConn);
                            }else{
                                   // mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais where Cod_rim='" + txtCodRim.Text + "'", mConn);// + Global.NotaFiscal.codigoRI + "ORDER by Cod_rim", mConn);
                                mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais where Cod_rim='" + txtCodRim.Text + "'Order by Cod_rim", mConn);// + Global.NotaFiscal.codigoRI + "ORDER by Cod_rim", mConn);
                                                    
                            }                       

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "notas_fiscais");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "notas_fiscais";
                
                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Codigo Nota Fiscal";
                dataGridView1.Columns[1].HeaderText = "R.I vinculada";
                dataGridView1.Columns[2].HeaderText = "Cod.Fornecedor";
                dataGridView1.Columns[3].HeaderText = "Nº N.Fiscal";
                dataGridView1.Columns[4].HeaderText = "Data Emissão";
                dataGridView1.Columns[5].HeaderText = "Valor";
                dataGridView1.Columns[6].HeaderText = "Setor Enviado";
                dataGridView1.Columns[7].HeaderText = "Data Envio";
                dataGridView1.Columns[8].HeaderText = "Situação";                

                calculaQuantidadeRegistros();
            //------------------------------------
            //lblMsg.Text = "Somando o valor da despesas das requisição. filtradas";
            
            Double ValorTotal1 = 0;
            
            try                {
                    foreach (DataGridViewRow col in dataGridView1.Rows)
                    {
                        ValorTotal1 = ValorTotal1 + Convert.ToDouble(col.Cells[5].Value);

                        //valorTotalEstimado = valorTotalEstimado + Convert.ToDouble(col.Cells[9].Value);

                    }

                     txtAcumulado.Text = ValorTotal1.ToString("C");
                    
                }
                catch
                {

                    MessageBox.Show("Erro na soma. Há valores inconsistentes nas requisições [coluna valor real].", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }

            //------------------------------------

                
        }

        private void calculaQuantidadeRegistros()
        {
            int registro;
            registro = dataGridView1.RowCount - 1;
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
                        cmd.CommandText = "delete from notas_fiscais where Cod_notas_fiscais= " + codigo;
                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível excluir a Nota Fiscal" + codigo);
                        }
                    }
                    /*catch (MySqlException ex)
                    {
                        throw new Exception("Servidor SQL Erro:" + ex.Number);
                    }*/
                    catch 
                    {
                        MessageBox.Show("Falha na conexão com o Banco de Dados [delete].", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    finally
                    {
                        mConn.Close();
                        mostrarResultados();
                    }

                    MessageBox.Show("Excluido o item de código " + "'" + codigo + "'" + " com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tssMensagem.Text = "";
                    UncheckedRadioButtons();
               }
            }
        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            temp = txtCodFornecedor.Text;
            codigo = Convert.ToInt32(temp);
            Excluir(codigo);
            txtCodFornecedor.Visible = false;
        }
        
        private void Gravar()
        {
                // Início da Conexão com indicação de qual o servidor, nome de base de dados a utilizar

                /* É aconselhável criar um utilizador com password. Para acrescentar a password é somente
                   necessário acrescentar o seguinte código a seguir ao uid=root;password=xxxxx
                 */

            if (txtNumeroNota.Text == "")
            {
                MessageBox.Show("Entre com número da N.F", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (txtDataNotaFiscal.Text == "")
                {
                    MessageBox.Show("Entre com a data da N.F", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (txtValorNF.Text == "")
                    {
                        MessageBox.Show("Entre com o valor da N.F", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        try
                        {
                            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

                            // Abre a conexão
                            mConn.Open();

                            //Query SQL
                            /*
                            MySqlCommand command = new MySqlCommand("INSERT INTO notas_fiscais (Cod_rim,Cod_fornecedor,Num_NotaFiscal,Data_NotaFiscal,Valor_NotaFiscal)" +
                            "VALUES('" + txtCodRim.Text + "','" + txtCodFornecedor.Text + "','" + txtNumeroNota.Text + "','" + txtDataNotaFiscal.Text + "'," + Convert.ToDecimal(txtValorNF.Text) + ")", mConn);
                            // Está representando a sequencia "...VALUES(txtCodDespesa,txtReduzido,...)" 
                            */
                            
                            if (chkSituacaoNF.Checked == true)
                            {
                                situacao = "Paga";
                            }
                            else {
                                situacao = "";
                            }


                            MySqlCommand command = new MySqlCommand("INSERT INTO notas_fiscais (Cod_rim,Cod_fornecedor,Num_NotaFiscal,Data_NotaFiscal,Valor_NotaFiscal,Setor_Enviado,Data_Envio,Situacao)" +
                            "VALUES(" + Convert.ToInt32(txtCodRim.Text) + "," + Convert.ToInt32(txtCodFornecedor.Text) + ",'" + txtNumeroNota.Text + "','" + txtDataNotaFiscal.Text + "','" + Convert.ToDecimal(txtValorNF.Text) + "','" + cmbSetor.Text + "','" + txtDataEnvioSetor.Text + "','" + situacao + "')", mConn);
                            // Está representando a sequencia "...VALUES(txtCodDespesa,txtReduzido,...)" 
                            //Executa a Query SQL
                            command.ExecuteNonQuery();
                            }
                            catch {

                                MessageBox.Show("INSERT INTO notas_fiscais (Cod_rim,Cod_fornecedor,Num_NotaFiscal,Data_NotaFiscal,Valor_NotaFiscal,Setor_Enviado,Data_Envio,Situacao)" +
                                "VALUES(" + Convert.ToInt32(txtCodRim.Text) + "," + Convert.ToInt32(txtCodFornecedor.Text) + ",'" + txtNumeroNota.Text + "','" + txtDataNotaFiscal.Text + "','" + Convert.ToDecimal(txtValorNF.Text) + "','" + cmbSetor.Text + "','" + txtDataEnvioSetor.Text + "','" + situacao + "')");
                            }

                        // Fecha a conexão
                        // mConn.Close();

                        //Mensagem de Sucesso
                       
                        mostrarResultados();

                        tssMensagem.Text = "";
                        // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"
                        LimpaCampos();
                        DesabilitaTextBox();
                        HabilitaRadionButtons();
                        MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UncheckedRadioButtons();
                        this.Close();
                    }
                }
            }
        }
        
        private void UncheckedRadioButtons()
        {

            rbNovo.Checked = false;
            rbAlterar.Checked = false;
            rbExclui.Checked = false;

        }
        
        private void HabilitaRadionButtons()
        {
            rbNovo.Enabled = true;
            rbAlterar.Enabled = true;
            rbExclui.Enabled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtCodFornecedor.Text == "")
                {
                    MessageBox.Show("Entre com algum código ou cancele a operação", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodFornecedor.Focus();
                }
                else
                {

                    temp = txtCodFornecedor.Text;
                    int codigo = Convert.ToInt32(temp);
                    Excluir(codigo);
                    lblRIvinculada.Visible = false;
                    txtCodFornecedor.Text = "";
                    txtCodFornecedor.Visible = false;
                }
            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }
        }
                
        private void LimpaCampos()
        {
            //txtCodNF.Text = "";
            //txtFornecedor.Text = "";
            txtNumeroNota.Text = "";
            txtDataNotaFiscal.Text = "";
            txtValorNF.Text = "";
            cmbSetor.Text = "";
            txtDataEnvioSetor.Text = "";
            //txtCodRim.Text = "";
            //txtCodFornecedor.Text = "";


        }

        private void rbPorCodigo_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void rbPorCodigo_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void rbPorNome_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void rbPorEmail_Click(object sender, EventArgs e)
        {
            mostrarResultados();
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

                        //-----------------------------
                                              

                        if (chkSituacaoNF.Checked == true)
                        {
                            situacao = "Paga";
                        }
                        else
                        {
                            situacao = "";
                        }


                        //-----------------------------


                        cmd.CommandText = "UPDATE notas_fiscais SET Cod_rim=" + txtCodRim.Text + "," + "Cod_fornecedor="
                            + txtCodFornecedor.Text + "," + "Num_NotaFiscal='" + txtNumeroNota.Text + "'," + "Data_NotaFiscal='"
                            + txtDataNotaFiscal.Text + "'," + "Valor_NotaFiscal='" + Convert.ToDecimal(txtValorNF.Text) + "',"
                            + "Setor_Enviado='" + cmbSetor.Text + "'," + "Data_Envio='" + txtDataEnvioSetor.Text + "'," + "Situacao='" + situacao +
                            "' WHERE Cod_notas_fiscais=" + codigo;

                        
                        MessageBox.Show("Registro " + " '" + codigo + "' " + "alterado com sucesso.");
                        tssMensagem.Text = "";

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
                            MessageBox.Show("UPDATE notas_fiscais SET Cod_rim=" + txtCodRim.Text + "," + "Cod_fornecedor="
                            + txtCodFornecedor.Text + "," + "Num_NotaFiscal='" + txtNumeroNota.Text + "'," + "Data_NotaFiscal='"
                            + txtDataNotaFiscal.Text + "'," + "Valor_NotaFiscal='" + Convert.ToDecimal(txtValorNF.Text) + "',"
                            + "Setor_Enviado='" + cmbSetor.Text + "'," + "Data_Envio='" + txtDataEnvioSetor.Text + "'," + "Situacao='" + situacao +
                            "' WHERE Cod_notas_fiscais=" + codigo, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    finally
                    {
                        mConn.Close();
                        LimpaCampos();
                        DesabilitaTextBox();
                        HabilitaRadionButtons();
                        mostrarResultados();
                        UncheckedRadioButtons();
                        bt_Gravar.Enabled = false;

                    }
                }
            } 
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtCodRim.Text == "")
                {
                    MessageBox.Show("Entre com algum código ou cancele a operação", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodRim.Focus();
                }
                else
                {
                    codigo = Convert.ToInt32(txtCodRim.Text);
                    alimentaTextBox(codigo);
                    lblRIvinculada.Visible = false;
                    txtCodRim.Text = "";
                    txtCodRim.Visible = false;
                    HabilitaTextBox();
                    txtFornecedor.Focus();
                    txtCodRim.Visible = false;
                }
            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }
        }

        private void alimentaTextBox(int codigo)
        {
            txtCodNF.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFornecedor.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtNumeroNota.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDataNotaFiscal.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtValorNF.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbSetor.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDataEnvioSetor.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
        }



        private void PesquisaPorCodigo(int codigo)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais Where Cod_notas_fiscais=" + codigo, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "notas_fiscais");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "notas_fiscais";

        }

        private void Pesquisa(int codigo)
        {

            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtCheckIdentificação_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                temp = txtConsultaCodFornecedor.Text;
                PesquisaPorFornecedor(temp);
                LimpaCheckBoxes();
                txtConsultaCodFornecedor.Text = "";

            }

        }

        private void LimpaCheckBoxes()
        {
            txtConsultaCodigoSeq.Text = "";
            txtConsultaCodFornecedor.Text = "";

        }


        private void PesquisaPorFornecedor(string temp)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 

            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais WHERE cod_fornecedor " + "LIKE " + "'%" + temp + "%'", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "notas_fiscais");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "notas_fiscais";
        }

        private void txtCheckIdentificação_TextChanged(object sender, EventArgs e)
        {

        }

        private void HabilitaTextBox()
        {
            txtFornecedor.Enabled = false;
            txtFornecedor.Focus();
            txtNumeroNota.Enabled = true;
            txtDataNotaFiscal.Enabled = false;
            txtValorNF.Enabled = true;
        }

        private void DesabilitaTextBox()
        {
            txtFornecedor.Enabled = false;
            txtNumeroNota.Enabled = false;
            txtDataNotaFiscal.Enabled = false;
            txtValorNF.Enabled = false;
        }


        private void uncheckedRadiodButtons()
        {
            rbNovo.Checked = false;
            rbAlterar.Checked = false;
            rbExclui.Checked = false;
        }

        private void habilitaBotoes()
        {
            bt_Gravar.Enabled = true;
            btnExcluir.Enabled = true;
            btnAtualizar.Enabled = true;
        }

        private void desabilitaBotoes()
        {
            bt_Gravar.Enabled = false;
            btnExcluir.Enabled = false;
            btnAtualizar.Enabled = false;
        }


        private void DesabilitaRadioButtons()
        {
            rbNovo.Enabled = false;
            rbAlterar.Enabled = false;
            rbExclui.Enabled = false;
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtValorNF.Focus();
            }
            else
            {
                txtDataNotaFiscal.Focus();
            }
        }

        private void txtFone1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtValorNF.Focus();
            }
            else
            {
                txtDataNotaFiscal.Focus();
            }

        }

        private void txtEndereço_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtDataNotaFiscal.Focus();
            }
            else
            {
                txtNumeroNota.Focus();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtConsultaCodigoSeq.Text != "")
                {
                    temp = txtConsultaCodigoSeq.Text;
                    codigo = Convert.ToInt32(temp);
                    PesquisaPorCodigo(codigo);
                    txtConsultaCodigoSeq.Text = "";
                }
                else
                {

                }

            }
        }


        private void bt_visualizar_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

        }

        private void txtDataNotaFiscal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumeroNota_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtValorNF_TextChanged(object sender, EventArgs e)
        {

        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void rbNovo_CheckedChanged(object sender, EventArgs e)
        {
            txtCodFornecedor.Enabled = false;
            txtCodRim.Enabled = false;
            //lblRIvinculada.Visible = false;
            HabilitaTextBox();
            DesabilitaRadioButtons();
            bt_Gravar.Enabled = true;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
            txtNumeroNota.Focus();
            tssMensagem.Text = "módulo inclusão ativado";
        }

        private void bt_Gravar_Click(object sender, EventArgs e)
        {
            if (rbNovo.Checked == true)
            {

                if (txtCodRim.Text == "" || txtCodFornecedor.Text == "" || txtFornecedor.Text == "")
                {
                    MessageBox.Show("Para incluir N.F é necessário vincular um fornecedor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    Gravar();
                }
            }
            else
            {
                try
                {
                    codigo = Convert.ToInt32(txtCodNF.Text);
                    if (rbAlterar.Checked == true)
                        Alterar(codigo);
                    else
                        Excluir(codigo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro:código com parâmetro não definido" + ex, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

         }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click_2(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //tssMensagem.Text = "";
            bt_Gravar.Enabled = false;
            btnAtualizar.Enabled = false;
            uncheckedRadiodButtons();
            LimpaCampos();
            DesabilitaTextBox();
            HabilitaRadionButtons();
            tssMensagem.Text = "";
            dataGridView1.Enabled = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        
        private void rbExclui_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExclui.Checked == true)
            {
                MessageBox.Show("Clique no Grid no nome do fornecedor a ser excluído.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Enabled = true;
                tssMensagem.Text = "módulo exclusão ativado";
                //rbAlterar.Checked = false;

                //txtCodFornecedor.Visible = false;
                //txtCodRim.Visible = false;
                //lblRIvinculada.Visible = false;

                HabilitaTextBox();
                DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                btnAtualizar.Enabled = false;
                btnExcluir.Enabled = false;
            }
            else
            {

            }

        }

        private void rbAlterar_CheckedChanged(object sender, EventArgs e)
        {
            //desabilitaBotoes();
            //btnAtualizar.Enabled = true;
            if (rbAlterar.Checked == true)
            {
                MessageBox.Show("Clique no Grid na linha da Nota Fiscal a ser alterada.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Enabled = true;

                tssMensagem.Text = "módulo de atualização ativado";
                //rbAlterar.Checked = false;

                txtCodFornecedor.Enabled = false;
                txtCodRim.Enabled = false;
                //lblRIvinculada.Visible = false;

                HabilitaTextBox();
                DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                btnAtualizar.Enabled = false;
                btnExcluir.Enabled = false;
            }
            else
            {

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void txtFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtNumeroNota.Focus();
            }
            else
            {
                txtFornecedor.Focus();
            }
        }

        private void txtNumeroNota_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                monthCalendar1.Visible = true;
            }
            else
            {
                txtNumeroNota.Focus();
                
            }                      

        }

        private void txtValorNF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtValorNF.Text != "")
                {
                    try
                    {
                        txtValorNF.Text = Convert.ToDecimal(txtValorNF.Text).ToString("C");
                        txtValorNF.Text = txtValorNF.Text.Replace("R$", "");

                    }
                    catch
                    {
                        MessageBox.Show("Insira um valor válido.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtValorNF.Text = "";
                        txtValorNF.Focus();
                    }

                }
                else
                {
                }
            }
            else
            {
                txtValorNF.Focus();

            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
                            
                bt_Gravar.Enabled = true;
                btnAtualizar.Enabled = false;
                            
                txtCodNF.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtCodRim.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtCodFornecedor.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtNumeroNota.Text= dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtDataNotaFiscal.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtValorNF.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                //cmbSetor.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtDataEnvioSetor.Text =  dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtSetorEnviado.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                string temporaria; // para receber o valor da coluna Situação do grid

                temporaria = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                if (temporaria == "Paga")
                {
                    chkSituacaoNF.Checked = true;
                }
                else
                {
                    chkSituacaoNF.Checked = false;
                }

            // Capturando o nome do fornecedor com o codigo = txtCodFornecedor. Vai capturar da tabela fornecedor

                stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                stConsulta = "SELECT nome_fornecedor FROM fornecedor WHERE cod_fornecedor='" + txtCodFornecedor.Text + "'";

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        txtFornecedor.Text = myReader["nome_fornecedor"] + Environment.NewLine;
                    }
                }

                Cmn.Close();
            
        }

        private void rbPorCodigoFornecedor_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
            tssMensagem.Text = "ordenando por código sequencial de Nota Fiscal ";
        }

        private void rbPorNome_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
            tssMensagem.Text = "ordenando por nome do fornecedor";
        }

        private void txtConsultaCodigoSeq_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
                {
                    if (txtConsultaCodigoSeq.Text != "")
                    {
                        temp = txtConsultaCodigoSeq.Text;
                        codigo = Convert.ToInt32(temp);
                        PesquisaPorCodigo(codigo);
                        txtConsultaCodigoSeq.Text = "";
                    }
                    else
                    {
                        txtConsultaCodigoSeq.Focus();
                    }

                }

        }

        private void txtConsultaCodFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtConsultaCodFornecedor.Text != "")
                {
                    temp = txtConsultaCodFornecedor.Text;
                    codigo = Convert.ToInt32(temp);
                    PesquisaPorCodigoFornecedor(codigo);
                    txtConsultaCodFornecedor.Text = "";
                }
                else
                {
                    txtConsultaCodFornecedor.Focus();
                }

            }

        }

        private void PesquisaPorCodigoFornecedor(int codigo)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais Where Cod_fornecedor=" + codigo, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "notas_fiscais");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "notas_fiscais";

        }

        private void txtConsultaPorNrNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtConsultaPorNrNotaFiscal.Text != "")
                {
                    temp = txtConsultaPorNrNotaFiscal.Text;
                    codigo = Convert.ToInt32(temp);
                    PesquisaPorNrNotaFiscal(codigo);
                    txtConsultaPorNrNotaFiscal.Text = "";
                }
                else
                {
                    txtConsultaPorNrNotaFiscal.Focus();
                }

            }

        }

        private void PesquisaPorNrNotaFiscal(int codigo)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais Where Num_NotaFiscal=" + codigo, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "notas_fiscais");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "notas_fiscais";

        }

        
        private void PesquisaValorNF(string codigo)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais Where Valor_NotaFiscal=" + codigo, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "notas_fiscais");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "notas_fiscais";

        }

        private void rbPorCodRim_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
            tssMensagem.Text = "ordenando por código sequencial de requisição";
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtConsultaNrNota_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lblRIvinculada_Click(object sender, EventArgs e)
        {

        }

        private void lblCodFornecedor_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rbPorValorNota_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
            tssMensagem.Text = "ordenando por número de N.F";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtCodNF_TextChanged(object sender, EventArgs e)
        {
                
        }

        private void txtCodRim_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodFornecedor_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFornecedor_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtConsultaValorNF_TextChanged(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDataNotaFiscal.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;
            txtValorNF.Focus();
        }

        private void NotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27) {

                monthCalendar1.Visible = false;
                monthCalendar2.Visible = false;

            }else{
            }

            }

        private void txtConsultaPorRI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtConsultaPorRI.Text != "")
                {
                    temp = txtConsultaPorRI.Text;
                    codigo = Convert.ToInt32(temp);
                    PesquisaPorRI(codigo);
                    txtConsultaCodFornecedor.Text = "";
                }
                else
                {
                    
                }

            }

        }

        private void PesquisaPorRI(int codigo)
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais Where Cod_rim=" + codigo, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "notas_fiscais");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "notas_fiscais";
        }

        private void txtConsultaCodigoSeq_Enter(object sender, EventArgs e)
        {
            txtConsultaCodigoSeq.BackColor = Color.Yellow;
        }

        private void txtConsultaCodFornecedor_Enter(object sender, EventArgs e)
        {
            txtConsultaCodFornecedor.BackColor = Color.Yellow;
        }

        private void txtConsultaPorNrNotaFiscal_Enter(object sender, EventArgs e)
        {
            txtConsultaPorNrNotaFiscal.BackColor = Color.Yellow;
        }

        private void txtConsultaPorRI_Enter(object sender, EventArgs e)
        {
            txtConsultaPorRI.BackColor = Color.Yellow;
        }

        private void txtNumeroNota_Enter(object sender, EventArgs e)
        {
            txtNumeroNota.BackColor = Color.Yellow;
        }

        private void txtDataNotaFiscal_Enter(object sender, EventArgs e)
        {
            txtDataNotaFiscal.BackColor = Color.Yellow;
        }

        private void txtValorNF_Enter(object sender, EventArgs e)
        {
            txtValorNF.BackColor = Color.Yellow;
        }

        private void txtConsultaCodigoSeq_Leave(object sender, EventArgs e)
        {
            txtConsultaCodigoSeq.BackColor = Color.White;
        }

        private void txtConsultaCodFornecedor_Leave(object sender, EventArgs e)
        {
            txtConsultaCodFornecedor.BackColor = Color.White;
        }

        private void txtConsultaPorNrNotaFiscal_Leave(object sender, EventArgs e)
        {
            txtConsultaPorNrNotaFiscal.BackColor = Color.White;
        }

        private void txtConsultaPorRI_Leave(object sender, EventArgs e)
        {
            txtConsultaPorRI.BackColor = Color.White;
        }

        private void txtNumeroNota_Leave(object sender, EventArgs e)
        {
            txtNumeroNota.BackColor = Color.White;
        }

        private void txtDataNotaFiscal_Leave(object sender, EventArgs e)
        {
            txtDataNotaFiscal.BackColor = Color.White;
        }

        private void txtValorNF_Leave(object sender, EventArgs e)
        {
            txtValorNF.BackColor = Color.White;
        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void cmbSetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            monthCalendar2.Visible = true;
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDataEnvioSetor.Text = monthCalendar2.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar2.Visible = false;
            bt_Gravar.Focus();
        }

        private void cmbSetor_Enter(object sender, EventArgs e)
        {
            cmbSetor.BackColor = Color.Yellow;
        }

        private void txtDataEnvioSetor_Enter(object sender, EventArgs e)
        {

            txtDataEnvioSetor.BackColor = Color.Yellow;
        }

        private void cmbSetor_Leave(object sender, EventArgs e)
        {
            cmbSetor.BackColor = Color.White;
        }

        private void txtDataEnvioSetor_Leave(object sender, EventArgs e)
        {
            txtDataEnvioSetor.BackColor = Color.White;
        }

        private void txtDataNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtValorNF.Visible = true;
            }
            else
            {
                txtDataNotaFiscal.Focus();

            }           
        }

        private void chkSituacaoNF_CheckedChanged(object sender, EventArgs e)
        {
            /*
            string mensagem = "Você tem certeza que esta Nota Fiscal foi liquidada?";
            string titulo = "Atenção!";
            //MessageBox.Show(mensagem, titulo);
            MessageBoxButtons botao = MessageBoxButtons.YesNo;
            DialogResult resultado = MessageBox.Show(mensagem, titulo, botao, MessageBoxIcon.Warning);

            if (resultado == DialogResult.No){
               chkSituacaoNF.Checked = false;
            }
            else {
               chkSituacaoNF.Checked = true;
            }
            */            
        }

        private void chkSituacaoNF_Click(object sender, EventArgs e)
        {
            string mensagem = "Você tem certeza que esta Nota Fiscal foi liquidada?";
            string titulo = "Atenção!";

            //MessageBox.Show(mensagem, titulo);
            MessageBoxButtons botao = MessageBoxButtons.YesNo;
            DialogResult resultado = MessageBox.Show(mensagem, titulo, botao, MessageBoxIcon.Warning);

            if (resultado == DialogResult.No)
            {
                chkSituacaoNF.Checked = false;

            }
            else
            {
                chkSituacaoNF.Checked = true;
                
            }
        }

        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
            this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Yellow;
        }

        
        }                        
    
}           