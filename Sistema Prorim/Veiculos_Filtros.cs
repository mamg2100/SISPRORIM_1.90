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

namespace Sistema_prorim
{
    public partial class Veiculos_Filtros : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        public string stConection;
        public MySqlConnection Cmn = new MySqlConnection();
       
        public Veiculos_Filtros()
        {
            InitializeComponent();
        }

        private void Veiculos_Filtros_Load(object sender, EventArgs e)
        {
            //-----------------------------------
            //Recupera que está gravado no arquivo Path: "d:\\IPSERVIDOR.txt"
            
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

            Global.Logon.ipservidor = textBox4.Text;
                     
            txtAnoValido.Text = DateTime.Now.ToString("yyyy");

            if (txtAnoValido.Text != "")
            {
                txtDataInicial.Text = "01/01/" + txtAnoValido.Text;
                txtDataFinal.Text = "31/12/" + txtAnoValido.Text;

            }
            else
            {

            }

            populaCmbPlaca();
                    
           
            mostrarResultados();

            }

        private void populaCmbPlaca()
        {
            // populando cmbPlaca 
            //------------------------------------------------------
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();
            mAdapter = new MySqlDataAdapter("SELECT placa FROM veiculos", mConn);

            DataTable veiculos = new DataTable();
            mAdapter.Fill(veiculos);
            try
            {
                for (int i = 0; i < veiculos.Rows.Count; i++)
                {
                    cmbPlacaConsulta.Items.Add(veiculos.Rows[i]["placa"]);

                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

        }

        private void mostrarResultados()
        {
            //instância do DataSet criada
            mDataSet = new DataSet();
            //definindo a strinf de conexão
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            //abrindo a conexão
            mConn.Open();

            if (rbPorDescricao.Checked == true)
            {
                //instanciando adapter que recebe a instrução SQL
                mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro order by Descricao", mConn);

            }
            else
            {
                if (rbPorValor.Checked == true)
                {
                    mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro order by Valor", mConn);
                }
                else
                {
                    if (rbPorPlaca.Checked == true)
                    {
                        mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro order by Placa", mConn);
                    }
                    else
                    {
                        if (rbPorCetil.Checked == true)
                        {
                            mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro order by Cetil", mConn);
                        }
                        else {
                         
                                mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro order by DataCetilSQL", mConn);
                            
                        }
                    }
                }
            }
                        //preenche o dataset através do adapter
                        mAdapter.Fill(mDataSet, "placa_filtro");

                        //atribui o resultado à propriedade DataSource da dataGridView
                        dataGridView1.DataSource = mDataSet;
                        dataGridView1.DataMember = "placa_filtro";

                        //Renomeia as colunas
                        dataGridView1.Columns[0].HeaderText = "Unidade";
                        dataGridView1.Columns[1].HeaderText = "Descrição";
                        dataGridView1.Columns[2].HeaderText = "Valor R$";
                        dataGridView1.Columns[3].HeaderText = "Placa";
                        dataGridView1.Columns[4].HeaderText = "Lotado em";
                        dataGridView1.Columns[5].HeaderText = "Cetil";
                        dataGridView1.Columns[6].HeaderText = "Data";
                        dataGridView1.Columns[7].HeaderText = "Contabilidade";
                        dataGridView1.Columns[8].HeaderText = "Ordenador/RI";
                        dataGridView1.Columns[9].HeaderText = "Compras";
                        dataGridView1.Columns[10].HeaderText = "Ordenador/Empenho";
                        dataGridView1.Columns[11].HeaderText = "Compras/AF";
                        dataGridView1.Columns[12].HeaderText = "Dipe";
            
                        calculaQuantidadeRegistros();
                        dataGridView1.Enabled = true;
                        txtTotalReal.Text = "";
        }

        
                private void calculaQuantidadeRegistros()
                {
 	                 if (dataGridView1.RowCount == 1 || dataGridView1.RowCount == 0)
                         label28.Text = (dataGridView1.RowCount).ToString() + " registro";
                    else 
                         label28.Text = (dataGridView1.RowCount).ToString() + " registros";

                }

                private void dataGridVeículos_CellContentClick(object sender, DataGridViewCellEventArgs e)
                {

                }

                private void groupBox3_Enter(object sender, EventArgs e)
                {

                }

                private void textBox4_TextChanged(object sender, EventArgs e)
                {

                }

                private void button1_Click(object sender, EventArgs e)
                {
                    //lblMsg.Text = "Somando o valor da despesas das requisição. filtradas";
                    //toolStripStatusMensagem.Text = "captura o valor da despesas das requisição filtradas";

                    Double ValorTotal1 = 0;
                    // Double valorTotalEstimado = 0;

                    // lblTotalEstimado.Visible = true;
                    //lblTotalReal.Visible = true;
                    txtTotalReal.Visible = true;
                    // txtTotalEstimado.Visible = true;

                    try
                    {
                        foreach (DataGridViewRow col in dataGridView1.Rows)
                        {
                            ValorTotal1 = ValorTotal1 + Convert.ToDouble(col.Cells[2].Value); // se a  Planilha de despesas estiver selecionada a coluna para soma se altera

                            //valorTotalEstimado = valorTotalEstimado + Convert.ToDouble(col.Cells[9].Value);

                        }

                        txtTotalReal.Text = ValorTotal1.ToString("C");
                        //txtTotalEstimado.Text = valorTotalEstimado.ToString("C");
                    }
                    catch
                    {

                        MessageBox.Show("Erro na soma. Há valores inconsistentes na Planilha de Despesas [coluna valor real].", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lblTotalReal.Visible = false;
                        txtTotalReal.Visible = false;
                    }                    

                }

                private void rbPorCodigo_CheckedChanged(object sender, EventArgs e)
                {

                }

                private void rbPorDescricao_CheckedChanged(object sender, EventArgs e)
                {
                    mostrarResultados();
                }

                private void rbPorValor_CheckedChanged(object sender, EventArgs e)
                {
                    mostrarResultados();

                }

                private void rbPorPlaca_CheckedChanged(object sender, EventArgs e)
                {
                    mostrarResultados();
                }

                private void rbPorCetil_CheckedChanged(object sender, EventArgs e)
                {
                    mostrarResultados();
                }

                private void rbPorDataCetil_CheckedChanged(object sender, EventArgs e)
                {
                    mostrarResultados();
                }

                private void txtCheckDescricao_KeyPress(object sender, KeyPressEventArgs e)
                {

                    if (e.KeyChar == 13) //Se for Enter executa a validação
                    {
                        PesquisaPorDescricao(txtCheckDescricao.Text);
                        LimpaFiltros();
                    }
                    else
                    {
                        // MessageBox.Show("Tecle 'ENTER'");
                    }
                }

                private void LimpaFiltros()
                {
                    txtCheckDescricao.Text = "";
                    txtCheckIdentificação.Text = "";
                    txtCheckCetil.Text = "";                        
                    txtCheckLotacao.Text = "";
                    cmbPlacaConsulta.Text = "";
                    txtAcumulado.Text = "";
                    txtCheckLotacao.Text = "";
                    textBox1.Text = "";
                }

                private void PesquisaPorDescricao(string p)
                {
                    try
                    {
                        mDataSet = new DataSet();
                        mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                        mConn.Open();

                        //cria um adapter utilizando a instrução SQL para acessar a tabela 
                        // ordena a tabela de acordo com o critério estabelecido
                        mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro WHERE Descricao " + "LIKE " + "'%" + p + "%'", mConn);

                        //preenche o dataset através do adapter
                        mAdapter.Fill(mDataSet, "placa_filtro");

                        //atribui o resultado à propriedade DataSource da dataGridView
                        dataGridView1.DataSource = mDataSet;
                        dataGridView1.DataMember = "placa_filtro";
                    }
                    catch {
                        MessageBox.Show("Não foi possível fazer a pesquisa solicitada", "Informação",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                    calculaQuantidadeRegistros();

                }

                private void txtCheckIdentificação_KeyPress(object sender, KeyPressEventArgs e)
                {

                    if (e.KeyChar == 13) //Se for Enter executa a validação
                    {
                        PesquisaPoridentificacao(txtCheckIdentificação.Text);
                        LimpaFiltros();
                    }
                    else
                    {
                        // MessageBox.Show("Tecle 'ENTER'");
                    }
                }

                private void PesquisaPoridentificacao(string p)
                {
                    try
                    {
                        mDataSet = new DataSet();
                        mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                        mConn.Open();

                        //cria um adapter utilizando a instrução SQL para acessar a tabela 
                        // ordena a tabela de acordo com o critério estabelecido
                        mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro WHERE Nome_Unidade " + "LIKE " + "'%" + p + "%'", mConn);

                        //preenche o dataset através do adapter
                        mAdapter.Fill(mDataSet, "placa_filtro");

                        //atribui o resultado à propriedade DataSource da dataGridView
                        dataGridView1.DataSource = mDataSet;
                        dataGridView1.DataMember = "placa_filtro";
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possível fazer a pesquisa solicitada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    calculaQuantidadeRegistros();

                }

                private void PesquisaPorValor(string p)
                {
                    try
                    {
                        mDataSet = new DataSet();
                        mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                        mConn.Open();

                        //cria um adapter utilizando a instrução SQL para acessar a tabela 
                        // ordena a tabela de acordo com o critério estabelecido
                        mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro WHERE Valor=" + txtAcumulado.Text, mConn);

                        //preenche o dataset através do adapter
                        mAdapter.Fill(mDataSet, "placa_filtro");

                        //atribui o resultado à propriedade DataSource da dataGridView
                        dataGridView1.DataSource = mDataSet;
                        dataGridView1.DataMember = "placa_filtro";
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possível fazer a pesquisa solicitada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    calculaQuantidadeRegistros();
                }

                private void label2_Click(object sender, EventArgs e)
                {

                }

                private void txtAcumulado_TextChanged(object sender, EventArgs e)
                {

                }

                private void txtCheckCetil_KeyPress(object sender, KeyPressEventArgs e)
                {
                    if (e.KeyChar == 13) //Se for Enter executa a validação
                    {
                        PesquisaPorCetil(txtCheckCetil.Text);
                        LimpaFiltros();
                    }
                    else
                    {
                        // MessageBox.Show("Tecle 'ENTER'");
                    }


                }

                private void PesquisaPorCetil(string p)
                {
                    try
                    {
                        mDataSet = new DataSet();
                        mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                        mConn.Open();

                        //cria um adapter utilizando a instrução SQL para acessar a tabela 
                        // ordena a tabela de acordo com o critério estabelecido
                        mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro WHERE Cetil=" + txtCheckCetil.Text, mConn);

                        //preenche o dataset através do adapter
                        mAdapter.Fill(mDataSet, "placa_filtro");

                        //atribui o resultado à propriedade DataSource da dataGridView
                        dataGridView1.DataSource = mDataSet;
                        dataGridView1.DataMember = "placa_filtro";
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possível fazer a pesquisa solicitada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    calculaQuantidadeRegistros();
                }

                private void txtCheckDataCetil_KeyPress(object sender, KeyPressEventArgs e)
                {
                    if (e.KeyChar == 13) //Se for Enter executa a validação
                    {
                        PesquisaPorDataCetil(txtCheckDataCetil.Text);
                        LimpaFiltros();
                    }
                    else
                    {
                        // MessageBox.Show("Tecle 'ENTER'");
                    }
                }

                private void PesquisaPorDataCetil(string p)
                {
                    try
                    {
                        mDataSet = new DataSet();
                        mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                        mConn.Open();

                        //cria um adapter utilizando a instrução SQL para acessar a tabela 
                        // ordena a tabela de acordo com o critério estabelecido
                        mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro WHERE Data " + "LIKE " + "'%" + p + "%'", mConn);

                        //preenche o dataset através do adapter
                        mAdapter.Fill(mDataSet, "placa_filtro");

                        //atribui o resultado à propriedade DataSource da dataGridView
                        dataGridView1.DataSource = mDataSet;
                        dataGridView1.DataMember = "placa_filtro";
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possível fazer a pesquisa solicitada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    calculaQuantidadeRegistros();

                }

                private void txtCheckLotacao_KeyPress(object sender, KeyPressEventArgs e)
                {
                    if (e.KeyChar == 13) //Se for Enter executa a validação
                    {
                        PesquisaPorLotacao(txtCheckLotacao.Text);
                        LimpaFiltros();
                    }
                    else
                    {
                        // MessageBox.Show("Tecle 'ENTER'");
                    }
                }

                private void PesquisaPorLotacao(string p)
                {
                    try
                    {
                        mDataSet = new DataSet();
                        mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                        mConn.Open();

                        //cria um adapter utilizando a instrução SQL para acessar a tabela 
                        // ordena a tabela de acordo com o critério estabelecido
                        mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro WHERE Lotado" + " LIKE " + "'%" + p + "%'", mConn);

                        //preenche o dataset através do adapter
                        mAdapter.Fill(mDataSet, "placa_filtro");

                        //atribui o resultado à propriedade DataSource da dataGridView
                        dataGridView1.DataSource = mDataSet;
                        dataGridView1.DataMember = "placa_filtro";
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possível fazer a pesquisa solicitada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    calculaQuantidadeRegistros();
                
                }

                private void cmbPlacaConsulta_SelectedIndexChanged(object sender, EventArgs e)
                {
                    PesquisaPorPlaca(cmbPlacaConsulta.Text);
                    LimpaFiltros();
                }

                private void PesquisaPorPlaca(string p)
                {
                    try
                    {
                        mDataSet = new DataSet();
                        mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                        mConn.Open();

                        //cria um adapter utilizando a instrução SQL para acessar a tabela 
                        // ordena a tabela de acordo com o critério estabelecido
                        mAdapter = new MySqlDataAdapter("SELECT * FROM placa_filtro WHERE Placa " + "LIKE " + "'%" + p + "%'", mConn);

                        //preenche o dataset através do adapter
                        mAdapter.Fill(mDataSet, "placa_filtro");

                        //atribui o resultado à propriedade DataSource da dataGridView
                        dataGridView1.DataSource = mDataSet;
                        dataGridView1.DataMember = "placa_filtro";
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possível fazer a pesquisa solicitada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    calculaQuantidadeRegistros();
                
                }

                private void txtCheckLotacao_TextChanged(object sender, EventArgs e)
                {

                }

                private void textBox3_TextChanged(object sender, EventArgs e)
                {

                }

                private void txtTotalReal_TextChanged(object sender, EventArgs e)
                {

                }

                private void label32_MouseHover(object sender, EventArgs e)
                {
                    label32.ForeColor = Color.Red;
                }

                private void label33_MouseHover(object sender, EventArgs e)
                {
                    label33.ForeColor = Color.Red;
                }

                private void label33_MouseLeave(object sender, EventArgs e)
                {
                    label33.ForeColor = Color.Black;
                }

                private void label32_Click(object sender, EventArgs e)
                {
                }

                private void txtDataInicial_TextChanged(object sender, EventArgs e)
                {

                }

                private void txtDataFinal_TextChanged(object sender, EventArgs e)
                {

                }

                private void label32_MouseClick(object sender, MouseEventArgs e)
                {
                    Calendar.Visible = true;

                }

                private void label33_MouseClick(object sender, MouseEventArgs e)
                {
                    Calendar2.Visible = true;
                }

                private void btnCalendario_Click(object sender, EventArgs e)
                {
                    if (txtDataInicial.Text == "")
                        Calendar.Visible = true;
                    else
                        Calendar2.Visible = true;

                }

                private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
                {
                    txtDataInicial.Text = Calendar.SelectionRange.Start.ToString("dd/MM/yyyy");
                    Calendar.Visible = false;
                    verificaValidadeData();
                }

                private void verificaValidadeData()
                {
                    DateTime data1, data2;
                    if (DateTime.TryParse(txtDataInicial.Text.ToString(), out data1).Equals(true) &&
                    DateTime.TryParse(txtDataFinal.Text.ToString(), out data2).Equals(true))
                    {
                        if (data1 > data2)
                        {
                            MessageBox.Show("A primeira data é maior que a segunda. Escolha uma data válida.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                            txtDataFinal.Text = "";

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        //MessageBox.Show("Data(s) inválida(s)");
                    }
                }

                private void Calendar2_DateSelected(object sender, DateRangeEventArgs e)
                {
                    txtDataFinal.Text = Calendar2.SelectionRange.Start.ToString("dd/MM/yyyy");
                    Calendar2.Visible = false;
                    verificaValidadeData();
                }


                private void label32_MouseLeave(object sender, EventArgs e)
                {
                    label32.ForeColor = Color.Black;
                }

                private void Veiculos_Filtros_KeyPress(object sender, KeyPressEventArgs e)
                {
                    if (e.KeyChar == 27)
                    {   

                        Calendar.Visible = false;
                        Calendar2.Visible = false;
                        //Calendar3.Visible = false;
                        txtDataFinal.Text = "";

                    }
                    else
                    {

                    }
                }

                private void txtAcumulado_KeyPress(object sender, KeyPressEventArgs e)
                {
                    if (e.KeyChar == 13) //Se for Enter executa a validação
                    {
                        PesquisaPorValor(txtAcumulado.Text);
                        LimpaFiltros();
                    }
                    else
                    {
                        // MessageBox.Show("Tecle 'ENTER'");
                    }
                }

                private void button2_Click(object sender, EventArgs e)
                {
                    RIM tela = new RIM();
                    tela.Show();

                }

                private void btnSair_Click(object sender, EventArgs e)
                {
                    this.Close();
                }

                private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
                {
                    label3.Text = "Módulo apenas de consulta. Para alterar , vá em 'Filtros'  opção 'Alterar' e  selecione a requsição desejada.";
                }

                private void dataGridView1_MouseEnter(object sender, EventArgs e)
                {
                    label3.Text = "Módulo apenas de consulta. Para alterar , vá em 'Filtros'  opção 'Alterar' e  selecione a requsição desejada.";
                }

                private void dataGridView1_MouseLeave(object sender, EventArgs e)
                {
                    label3.Text = "";             
                }
                       
            
        }
            
         }        

        

    

