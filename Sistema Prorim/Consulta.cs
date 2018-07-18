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
using Sistema_Prorim;
using Sistema_prorim;


namespace Sistema_Prorim
{
    public partial class Consulta : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        public String TipoRIM;
        String temp;
        String temp_1;
        //int codunidade = 0;
        //int codigo;
        int estadocodigo = 1;
        int estadoident = 1;
        int estado = 0;
        public string stConection;
        public string stConsulta;
        public MySqlConnection Cmn = new MySqlConnection();

        int codigoultimari = 0; // recebe o numero da ultima ri cadastrada 
        
        public Consulta()
        {
            InitializeComponent();
            stConsulta = "";
            stConection = "";     
        }
                                   
        private void calculaQuantidadeRegistros()
        {
            if ((dataGridView1.RowCount) == 1 || (dataGridView1.RowCount) == 0)
                label3.Text = (dataGridView1.RowCount.ToString()) + " registro";
            else
                label3.Text = (dataGridView1.RowCount.ToString()) + " registros";
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Consulta_Load(object sender, EventArgs e)
        {
            String ano = DateTime.Now.Year.ToString();
            txtAnoValido.Text = ano;

            if (txtAnoValido.Text != "")
            {
                txtDataInicial.Text = "01/01/" + txtAnoValido.Text;
                txtDataFinal.Text = "31/12/" + txtAnoValido.Text;
                dtpDataInicial.Text = txtDataInicial.Text;
                dtpDataFinal.Text = txtDataFinal.Text;
            }
            else
            {

            }

            txtCheckCetil.Focus();

            dataGridView1.Visible = true;
            
            //label3.Visible = false;

            groupBox3.Visible = true;


            // POPULANDO ComboBox
            try
            {
                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                // populando cmbUnidade
                //------------------------------------------------------

                mAdapter = new MySqlDataAdapter("SELECT * FROM unidade ORDER BY Nome_unidade", mConn);
                DataTable unidade = new DataTable();
                mAdapter.Fill(unidade);
                try
                {
                    for (int i = 0; i < unidade.Rows.Count; i++)
                    {
                        cmbUnidade.Items.Add(unidade.Rows[i]["Nome_Unidade"]);
                    }
                }
                catch (MySqlException erro)
                {
                    throw erro;
                }
            }
            catch (Exception ex) {

                MessageBox.Show("Erro ao preencher 'Unidades' :"+ex.Message);
            }

            mostrarResultados();

            toolStripStatusMensagem.Text = "módulo de consulta ativado";
                     
        }

        
        private void mostrarResultados()
        {
            textBox4.Text = Global.Logon.ipservidor;

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            
            if (txtAnoValido.Text != "")
            {
                txtDataInicial.Text = "01/01/" + txtAnoValido.Text;
                txtDataFinal.Text = "31/12/" + txtAnoValido.Text;
            } else {
                txtDataInicial.Text = "";
                txtDataFinal.Text = "";

            }            
                toolStripStatusMensagem.Text = "Exibindo todas as Requisições do ano selecionado.";
                
                try
                {
                    groupBox7.Enabled = true;

                    mDataSet = new DataSet();
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    mConn.Open();

                    if (checkBoxRRP.Checked == true && checkBoxRIM.Checked == false)
                    {
                        mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE (dataCetilSql BETWEEN '" +
                        Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy-MM-dd")
                        + "' AND '"
                        + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy-MM-dd") + "')"
                        + "AND Tipo_RIM='RRP' ORDER BY Cetil", mConn);
                    }
                    else
                    {
                        if (checkBoxRRP.Checked == false && checkBoxRIM.Checked == true)
                        {
                            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE (dataCetilSql BETWEEN '" +
                                Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy-MM-dd")
                                + "' AND '"
                                + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy-MM-dd") + "')"
                                + "AND Tipo_RIM='RIM' ORDER BY Cetil", mConn);
                        }
                        else
                        {
                            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE (dataCetilSql BETWEEN '" +
                            Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy-MM-dd")
                            + "' AND '"
                            + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy-MM-dd") + "')"
                            + "ORDER BY Cetil", mConn);
                        }
                    }

                    //preenche o dataset através do adapter
                    mAdapter.Fill(mDataSet, "rim");

                    //atribui o resultado à propriedade DataSource da dataGridView
                    dataGridView1.DataSource = mDataSet;
                    dataGridView1.DataMember = "rim";

                    //Exibindo as colunas de acordo com os checkBox marcados ou não.               
                    dataGridView1.Columns[0].HeaderText = "Cód.Seq.RI";

                    if (chkCodigoSeq.Checked == true)
                    {
                        dataGridView1.Columns[0].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Columns[0].Visible = false;
                    }
                    
                    dataGridView1.Columns[1].HeaderText = "Unidade";
                    if (chkUnidade.Checked == true)
                    {
                        dataGridView1.Columns[1].Visible = true;
                    }
                    else {
                        dataGridView1.Columns[1].Visible = false;
                    }

                    dataGridView1.Columns[2].HeaderText = "Descrição";
                    if (chkDescricao.Checked == true)
                    {
                        dataGridView1.Columns[2].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Columns[2].Visible = false;
                    }

                    
                    dataGridView1.Columns[3].HeaderText = "D.O";
                    if (chkDotacao.Checked == true)
                    {
                        dataGridView1.Columns[3].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Columns[3].Visible = false;
                    }


                    dataGridView1.Columns[4].HeaderText = "Tipo RI";
                    
                    dataGridView1.Columns[5].HeaderText = "Cod.Cetil";
                    dataGridView1.Columns[6].HeaderText = "Data RI";

                    if (chkCetil.Checked == true)
                    {
                        dataGridView1.Columns[5].Visible = true;
                        dataGridView1.Columns[6].Visible = true;

                    }
                    else
                    {
                        dataGridView1.Columns[5].Visible = false;
                        dataGridView1.Columns[6].Visible = false;
                    }                  
                                       
                    dataGridView1.Columns[7].HeaderText = "Data RI SQL";                    
                    dataGridView1.Columns[7].Visible = false;
                    
                    dataGridView1.Columns[8].HeaderText = "Estimado";
                    if (chkVlEstimado.Checked == true)
                    {
                        dataGridView1.Columns[8].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Columns[8].Visible = false;
                    }
                    
                    dataGridView1.Columns[9].HeaderText = "Valor Real";
                    if (chkValorReal.Checked == true)
                    {
                        dataGridView1.Columns[9].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Columns[9].Visible = false;
                    }
                    
                    
                    dataGridView1.Columns[10].HeaderText = "Processo";
                    dataGridView1.Columns[11].HeaderText = "Ano Proc.";
                    if (chkProc.Checked == true)
                    {
                        dataGridView1.Columns[10].Visible = true;
                        dataGridView1.Columns[11].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Columns[10].Visible = false;
                        dataGridView1.Columns[11].Visible = false;
                    }                        
                    
                    dataGridView1.Columns[12].HeaderText = "Proc.Contábil";
                    dataGridView1.Columns[13].HeaderText = "Ano Cont.";
                    if (chkProcessoContabil.Checked == true)
                    {
                        dataGridView1.Columns[12].Visible = true;
                        dataGridView1.Columns[13].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Columns[12].Visible = false;
                        dataGridView1.Columns[13].Visible = false;
                    }           
                   
                    dataGridView1.Columns[14].HeaderText = "Contabilidade";
                    dataGridView1.Columns[15].HeaderText = "Ordenador";
                    dataGridView1.Columns[16].HeaderText = "Compras 1ª";
                    dataGridView1.Columns[17].HeaderText = "Ord.Empenho";
                    dataGridView1.Columns[18].HeaderText = "Compras 2ª";
                    dataGridView1.Columns[19].HeaderText = "Dipe";

                    if (chkTramite.Checked == true)
                    {
                        dataGridView1.Columns[14].Visible = true;
                        dataGridView1.Columns[15].Visible = true;
                        dataGridView1.Columns[16].Visible = true;
                        dataGridView1.Columns[17].Visible = true;
                        dataGridView1.Columns[18].Visible = true;
                        dataGridView1.Columns[19].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Columns[14].Visible = false;
                        dataGridView1.Columns[15].Visible = false;
                        dataGridView1.Columns[16].Visible = false;
                        dataGridView1.Columns[17].Visible = false;
                        dataGridView1.Columns[18].Visible = false;
                        dataGridView1.Columns[19].Visible = false;
                    }
                    
                    dataGridView1.Columns[20].HeaderText = "Cadastrante";
                    dataGridView1.Columns[21].HeaderText = "Data Cadastro";
                    
                    if (chkCadastrante.Checked == true) {
                        dataGridView1.Columns[20].Visible = true;
                        dataGridView1.Columns[21].Visible = true;
                    }else{
                        dataGridView1.Columns[20].Visible = false;
                        dataGridView1.Columns[21].Visible = false;
                    }

                    dataGridView1.Columns[22].HeaderText = "Observações";
                    if (chkObs.Checked == true)
                    {

                        dataGridView1.Columns[22].Visible = true;
                    }
                    else {
                        dataGridView1.Columns[22].Visible = false;
                    }
                    
                    dataGridView1.Columns[23].HeaderText = "Atendida";
                    dataGridView1.Columns[23].Visible = false;
                    dataGridView1.Columns[24].HeaderText = "Concluída";
                    dataGridView1.Columns[24].Visible = false;
                    dataGridView1.Columns[25].HeaderText = "Usuário";
                    dataGridView1.Columns[25].Visible = false;
                    dataGridView1.Columns[26].HeaderText = "Unidade";
                    dataGridView1.Columns[26].Visible = false;
                    
                    mConn.Close();
                    calculaQuantidadeRegistros();
                }
                     
                catch (Exception ex)
                {
                    MessageBox.Show(mAdapter + "Erro: " + ex.Message);       
                }                 
                
                                
        }    
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount < 3)
            {
                if (dataGridView1.DefaultCellStyle.WrapMode == DataGridViewTriState.True)
                {
                    dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                }
                else {
                    dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }
            }
            else {
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
        }
        
        private void txtAnoValido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtAnoValido.Text == "")
                {
                    txtDataInicial.Text = ""; // DateTime.Now.ToString("01/01/yyyy");
                    txtDataFinal.Text = ""; // DateTime.Now.ToString("31/12/yyyy");
                }
                else
                {
                    //txtDataInicial.Text = DateTime.Now.ToString("01/01/yyyy");
                    //txtDataFinal.Text = DateTime.Now.ToString("31/12/yyyy");
                    txtDataInicial.Text = "01/01/" + txtAnoValido.Text;
                    txtDataFinal.Text = "31/12/" + txtAnoValido.Text;
                    dtpDataInicial.Text = txtDataInicial.Text;
                    dtpDataFinal.Text = txtDataFinal.Text;

                }

                mostrarResultados();

            }
            else
            {

            }

        }

        private void btnCalendario_Click(object sender, EventArgs e)
        {

        }

        private void dtpDataFinal_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void dtpDataInicial_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkValorReal_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                chkCodigoSeq.Checked = true;
                chkUnidade.Checked = true;
                chkDescricao.Checked = true;
                chkDotacao.Checked = true;
                chkCetil.Checked = true;
            }
            else
            {
                chkCodigoSeq.Checked = false;
                chkUnidade.Checked = false;
                chkDescricao.Checked = false;
                chkDotacao.Checked = false;
                chkCetil.Checked = false;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                chkCodigoSeq.Checked = true;
                chkUnidade.Checked = true;
                chkDescricao.Checked = true;
                chkDotacao.Checked = true;
                chkCetil.Checked = true;
                chkValorReal.Checked = true;
                chkVlEstimado.Checked = true;
                chkProc.Checked = true;
                chkProcessoContabil.Checked = true;

            }
            else
            {
                chkCodigoSeq.Checked = false;
                chkUnidade.Checked = false;
                chkDescricao.Checked = false;
                chkDotacao.Checked = false;
                chkCetil.Checked = false;
                chkValorReal.Checked = false;
                chkVlEstimado.Checked = false;
                chkProc.Checked = false;
                chkProcessoContabil.Checked = false;

            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                chkCodigoSeq.Checked = true;
                chkUnidade.Checked = true;
                chkDescricao.Checked = true;
                chkDotacao.Checked = true;
                chkCetil.Checked = true;
                chkValorReal.Checked = false;
                chkVlEstimado.Checked = false;
                chkProc.Checked = true;
                chkProcessoContabil.Checked = true;
                chkObs.Checked = true;
                chkTramite.Checked = true;
                chkCadastrante.Checked = true;
                chkDataCadastro.Checked = true;
                

            }
            else
            {
                chkCodigoSeq.Checked = false;
                chkUnidade.Checked = false;
                chkDescricao.Checked = false;
                chkDotacao.Checked = false;
                chkCetil.Checked = false;
                chkValorReal.Checked = false;
                chkVlEstimado.Checked = false;
                chkProc.Checked = false;
                chkProcessoContabil.Checked = false;
                chkObs.Checked = false;
                chkTramite.Checked = false;
                chkCadastrante.Checked = false;
                chkDataCadastro.Checked = false;
            }
        }

        private void chkTramite_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkCodigoSeq_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkUnidade_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkDescricao_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkDotacao_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkCetil_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkVlEstimado_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkProc_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkProcessoContabil_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkObs_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkCadastrante_CheckedChanged(object sender, EventArgs e)
        {
            //mostrarResultados();
        }

        private void chkDataCadastro_CheckedChanged(object sender, EventArgs e)
        {
           //mostrarResultados();
        }
        
        public string TipoRim { get; set; }

        
        private void txtCheckCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Se for apertada a tecla 'Enter' no campo após digitar uma consulta
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtCheckCodigo.Text != "")
                {
                    temp = txtCheckCodigo.Text;
                    codigoultimari = Convert.ToInt32(temp);
                    PesquisaPorCodigo(codigoultimari);
                    LimparCampos();

                }
                else
                {
                }

            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }
        }

        private void LimparCampos()
        {
            txtCheckCodigo.Text = "";
            cmbUnidade.Text = "";
            txtCheckDescricao.Text = "";
            txtCheckAF.Text = "";
            txtCheckCetil.Text = "";
            //txtCheckDataCetil.Text = "";
            txtCheckProcesso.Text = "";
            txtCheckProcessoContabil.Text = "";
            txtCheckDespPrincipal.Text = "";
            txtCheckDespPrincipal.Text = "";
            txtCheckDesdobrada.Text = "";
            txtCheckEmpenho.Text = "";
            //txtCheckCodigoAplicacao.Text = "";
            txtCheckAF.Text = "";
        }        

        private void PesquisaPorCodigo(int codigoultimari)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            if (chkPlanilhaDespesas.Checked == false)
            {
                //mostraChecks();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por código sequencial da requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Cod_rim like '%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por código sequencial da requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Cod_rim like '%" + temp + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else
            {

                toolStripStatusMensagem.Text = "pesquisa por número da requisição no período selecionado";

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "Planilha de Despesas. Pesquisa por código sequencial da requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Cod_rim like" + "'%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "Planilha de Despesas. Pesquisa por código sequencial da requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_de_despesas WHERE (Cod_rim like '%" + temp + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_de_despesas");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_de_despesas";

            }

            mConn.Close();
            calculaQuantidadeRegistros();

        }

        private void checkBoxRIM_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void checkBoxRRP_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void txtCheckDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorDescricao(txtCheckDescricao.Text);
                LimparCampos();
            }           
        }

        private void PesquisaPorDescricao(string p)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            if (chkPlanilhaDespesas.Checked == false)
            {
                //mostraChecks();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por descrição da requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Descricao like '%" + p + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por descrição da requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Descricao like '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else
            {

                toolStripStatusMensagem.Text = "pesquisa por descrição da requisição";

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "Planilha de despesas.Pesquisa por descrição da requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Cetil like" + "'%" + p + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "Planilha de despesas. Pesquisa por descrição da requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Cetil like '%" + p + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";

            }

            mConn.Close();
            calculaQuantidadeRegistros();
            
        }

        private void txtCheckIdentificação_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorIdentificação(cmbUnidade.SelectedItem.ToString());
                LimparCampos();
            }
           
        }

        private void PesquisaPorIdentificação(string temp)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            if (chkPlanilhaDespesas.Checked == false)
            {
                //mostraChecks();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por unidade solicitante da requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Nome_Unidade like '%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por unidade solicitante da requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Nome_Unidade like '%" + temp + "%' and dataCetilsql BETWEEN '" 
                        + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" 
                        + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else
            {
                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por unidade solicitante da requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Nome_Unidade like" + "'%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por unidade solicitante da requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Nome_Unidade like '%" + temp + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";

            }

            mConn.Close();
            calculaQuantidadeRegistros();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void txtCheckCetil_KeyPress(object sender, KeyPressEventArgs e)
        {
            toolStripStatusMensagem.Text = "pesquisa por número da requisição";

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtCheckCetil.Text != "")
                {
                    PesquisaPorCetil(txtCheckCetil.Text);
                    LimparCampos();
                }
                else
                {
                    txtCheckCetil.Focus();
                }
            }
           
        }

        private void PesquisaPorCetil(string temp)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            
            if (chkPlanilhaDespesas.Checked == false)
            {
                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {

                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Cetil like '%" + temp + "%'", mConn);
                }
                else
                {                                       
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Cetil like '%" + temp + "%' and dataCetilsql BETWEEN '" 
                        + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
           
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else
            {

                toolStripStatusMensagem.Text = "pesquisa por número da requisição no período selecionado";

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Cetil like" + "'%" + temp + "%'", mConn);
                }
                else
                {
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Cetil like '%" + temp + "%' and (dataCetilSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";

            }

            mConn.Close();
            calculaQuantidadeRegistros();
        }

        private void dtpDataCetil_ValueChanged(object sender, EventArgs e)
        {
            PesquisaPorDataCetil(dtpDataCetil.Value.ToString("dd/MM/yyyy"));
            LimparCampos();
           
        }

        private void PesquisaPorDataCetil(string temp)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            if (chkPlanilhaDespesas.Checked == false)
            {
                //mostraChecks();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por data da requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE DataCetil like '%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por data da requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE DataCetil like '%" + temp + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else
            {
                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por data da requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Data_Cetil like" + "'%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por data da requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Data_Cetil like '%" + temp + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";

            }

            mConn.Close();
            calculaQuantidadeRegistros();

        }

        private void txtCheckProcesso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorProcesso(txtCheckProcesso.Text);
                LimparCampos();
            }
            else
            {
                txtCheckProcesso.Focus();
            }
        }

        private void PesquisaPorProcesso(string temp)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            if (chkPlanilhaDespesas.Checked == false)
            {
                //mostraChecks();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Processo like '%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Processo like '%" + temp + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else
            {
                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição";

                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Processo like" + "'%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição no período selecionado";

                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Processo like '%" + temp + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";

            }

            mConn.Close();
            calculaQuantidadeRegistros();

        }

        private void txtCheckProcessoContabil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorProcessoContabil(txtCheckProcessoContabil.Text);
                LimparCampos();
            }
            else
            {
                txtCheckProcessoContabil.Focus();
            }
        }

        private void PesquisaPorProcessoContabil(string temp)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            if (chkPlanilhaDespesas.Checked == false)
            {
                //mostraChecks();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE ProcessoContabil like '%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE ProcessoContabil like '%" + temp + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else
            {
                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição";

                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE ProcessoContabil like" + "'%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição no período selecionado";

                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Processocontabil like '%" + temp + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";

            }

            mConn.Close();
            calculaQuantidadeRegistros();

            
        }

        private void txtCheckDesdobrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaDesdobrada(txtCheckDesdobrada.Text);
                LimparCampos();
            }
            else
            {
                txtCheckDesdobrada.Focus();
            }
        }

        private void PesquisaDesdobrada(string p)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            if (chkPlanilhaDespesas.Checked == false)
            {
                

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº da despesa desdobrada vinculado à requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Dotacao like '%" + p + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº da despesa desdobrada vinculado à requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Dotacao like '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
               
            }
            else
            {
                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº da despesa desdobrada vinculada à requisição";

                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Dotacao like" + "'%" + temp + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº da despesa desdobrada vinculada à requisição no período selecionado";

                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Dotacao like '%" + temp + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
                    //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Dotacao like '%" + temp + "%' and (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ) ORDER BY Processo", mConn);

                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";

            }

            mConn.Close();
            calculaQuantidadeRegistros();


        }

        private void txtCheckDespPrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisarReduzida(txtCheckDespPrincipal.Text);
                LimparCampos();
            }
            else
            {
                txtCheckDespPrincipal.Focus();

            }
        }

        private void PesquisarReduzida(string p)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            
                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº da despesa reduzida vinculada à requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Reduzida like" + "'%" + p + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº da despesa reduzida vinculada à requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Reduzida like '%" + p + "%' and (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ) ORDER BY Processo", mConn);

                }
                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";

            

            mConn.Close();
            calculaQuantidadeRegistros();
        }

        private void txtDataInicial_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbUnidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorIdentificação(cmbUnidade.SelectedItem.ToString());
                LimparCampos();
            }

        }

        private void cmbUnidade_SelectedValueChanged(object sender, EventArgs e)
        {
            PesquisaPorIdentificação(cmbUnidade.SelectedItem.ToString());
            LimparCampos();
        }

        private void btnPlanilhaDespesas_Click(object sender, EventArgs e)
        {
            if (chkPlanilhaDespesas.Checked == true)
            {
                chkPlanilhaDespesas.Checked =false;
            }
            else {

                chkPlanilhaDespesas.Checked = true;
            }

            if (chkPlanilhaDespesas.Checked == true)
            {
                groupBox7.Enabled = false;
            }
            else {
                groupBox7.Enabled = true;
            }

            Sistema_Prorim.PlanilhaDespesa pd = new Sistema_Prorim.PlanilhaDespesa();
            pd.Show();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Sistema_prorim.Global.InclusaoRI.flagIncluirRim = 0;

            Sistema_prorim.Global.DadosRim.codigo = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            //Global.RI.codcetil = txtCodigo.Text;
            Sistema_prorim.Global.DadosRim.escolhaUnid = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.descricao = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.DO = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.Logon.tipoRequisicao = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "RIM")
            {
                //radioButtonRIM.Checked = true;
            }
            else
            {
                //radioButtonRRP.Checked = true;
            }

            Sistema_prorim.Global.DadosRim.cetil = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.dataCetil = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            Sistema_prorim.Global.DadosRim.valorEstimado = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.valorReal = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.Processo = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.AnoProcesso = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.ProcessoContabil = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.AnoProcessoContabil = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            Sistema_prorim.Global.DadosRim.DataContabilidade = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.DataOrdenador1 = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.DataCompras1 = dataGridView1[16, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.DataOrdenador2 = dataGridView1[17, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.DataCompras2 = dataGridView1[18, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.DataDipe = dataGridView1[19, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            Sistema_prorim.Global.DadosRim.cadastradoPor = dataGridView1[20, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            Sistema_prorim.Global.DadosRim.dtCadastro = dataGridView1[21, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            Sistema_prorim.Global.DadosRim.Obs = dataGridView1[22, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            Requisicao rim = new Requisicao();
            rim.Show();

            this.Close();
            
        }

        private void Consulta_Shown(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void txtAnoValido_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRIM_Click(object sender, EventArgs e)
        {
            
            // Criada variável como Flag para saber se foi instanciado um objeto a partir do botão 'RIM' da tela de consulta
            // pois se isso acontecer será para incluir uma nova RIM e as variáveis devem estar com valores zerados. Se o form
            // for aberto a partir do grid significa que essa variável flagInclusaoRim=0 (será para alteração de dados da RI).
            Sistema_prorim.Global.InclusaoRI.flagIncluirRim = 1;
            Sistema_prorim.Global.Logon.tipoRequisicao = "RIM";            
            Requisicao rim = new Requisicao();
            rim.Show();
            this.Close();
            
        }

        private void btnRRP_Click(object sender, EventArgs e)
        {
            // Criada variável como Flag para saber se foi instanciado um objeto a partir do botão 'RIM' da tela de consulta
            // pois se isso acontecer será para incluir uma nova RIM e as variáveis devem estar com valores zerados. Se o form
            // for aberto a partir do grid significa que essa variável flagInclusaoRim=0 (será para alteração de dados da RI).
            Sistema_prorim.Global.InclusaoRI.flagIncluirRim = 1;
            Sistema_prorim.Global.Logon.tipoRequisicao = "RRP";
            this.Close();
            Requisicao rim = new Requisicao();
            rim.ShowDialog();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex % 2 ==0)
            {
                e.CellStyle.BackColor = Color.LightBlue;
            }
        }                                     
       
    }
}
