using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Sistema_Prorim
{
    public partial class PlanilhaDespesa : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;

        public string stConection;
        public MySqlConnection Cmn = new MySqlConnection();
        
        public PlanilhaDespesa()
        {
            InitializeComponent();
        }

        private void PlanilhaDespesa_Load(object sender, EventArgs e)
        {
            txtAno.Text = DateTime.Today.ToString("yyyy");
            label2.Text = "TOTAL EMPENHOS | NFs ";
            
            if (txtAno.Text != "")
            {
                txtDataInicial.Text = "01/01/" + txtAno.Text;
                txtDataFinal.Text = "31/12/" + txtAno.Text;
            }
            
            mostrarResultados();
        }

        private void mostrarResultados()
        {          
            /*
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa", mConn);

            // é bom colocar o período pois só vão aparecer as requisições que tiverem empenhos cadastrados. Isso porque ao cadstrar empenho entramos com sua data.
            mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE dataempenhoSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") 
                + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);

            //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE (data_Empenho BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd")
            //                        + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "')" , mConn);
            
            //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE (data_Empenho BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "')", mConn);


            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "planilhadespesa");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "planilhadespesa";

            //Renomeia as colunas
            dataGridView1.Columns[0].HeaderText = "Código";
            dataGridView1.Columns[1].HeaderText = "Cetil";
            dataGridView1.Columns[2].HeaderText = "Empenho";
            dataGridView1.Columns[3].HeaderText = "Valor Empenho";
            dataGridView1.Columns[4].HeaderText = "Data Empenho";
            dataGridView1.Columns[5].HeaderText = "Data Empenho SQL";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].HeaderText = "Autorização";
            dataGridView1.Columns[7].HeaderText = "Data AF";
            dataGridView1.Columns[8].HeaderText = "Valor AF";
            dataGridView1.Columns[9].HeaderText = "Fornecedor";
            dataGridView1.Columns[10].HeaderText = "Despesa";
            dataGridView1.Columns[11].HeaderText = "Reduzida";

            //------------------------------------------------------
            // populando cmbFornecedor

            mAdapter = new MySqlDataAdapter("SELECT * FROM fornecedor ORDER BY Nome_fornecedor", mConn);
            DataTable fornecedor = new DataTable();
            mAdapter.Fill(fornecedor);
            try
            {
                for (int i = 0; i < fornecedor.Rows.Count; i++)
                {
                    cmbFornecedor.Items.Add(fornecedor.Rows[i]["Nome_fornecedor"]);

                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }


            //--  Somando automaticamente a coluna valor empenho  ---_-

            somatorio();
            
            //---------------------------------------------------------

            calculaQuantidadeRegistros();
            //LimpaCamposFiltros();

            mConn.Close();
            */

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            
            mConn.Open();
            
            try
            {
                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    mAdapter = new MySqlDataAdapter("Select * FROM planilhadespesa", mConn);

                }
                else
                {
                    //igual a consulta anterior só que para um período definido

                    //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE (dataempenhoSQL BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ", mConn);
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd")
                        + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "ORDER BY dataempenhoSQL'", mConn);

                    //preenche o dataset através do adapter
                    mAdapter.Fill(mDataSet, "planilhadespesa");

                    //atribui o resultado à propriedade DataSource da dataGridView
                    dataGridView1.DataSource = mDataSet;
                    dataGridView1.DataMember = "planilhadespesa";

                    //Renomeia as colunas
                    dataGridView1.Columns[0].HeaderText = "Código";
                    dataGridView1.Columns[1].HeaderText = "Cetil";
                    dataGridView1.Columns[2].HeaderText = "Empenho";
                    dataGridView1.Columns[3].HeaderText = "Valor Empenho";
                    dataGridView1.Columns[4].HeaderText = "Data Empenho";// essa data é varchar
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Columns[5].HeaderText = "Data Empenho";// essa é a data empenho Date (que seria o correto no Banco de Dados
                    dataGridView1.Columns[5].Visible = true;
                    dataGridView1.Columns[6].HeaderText = "Autorização";
                    dataGridView1.Columns[7].HeaderText = "Data AF";
                    dataGridView1.Columns[8].HeaderText = "Valor AF";
                    dataGridView1.Columns[9].HeaderText = "Fornecedor";
                    dataGridView1.Columns[10].HeaderText = "Despesa";
                    dataGridView1.Columns[11].HeaderText = "Reduzida";
                    dataGridView1.Columns[12].HeaderText = "Cod.Aplic.";
                    dataGridView1.Columns[13].HeaderText = "Programa";
                }

                mConn.Close();
                
                //------------------------------------------------------
                // populando cmbFornecedor

                mAdapter = new MySqlDataAdapter("SELECT * FROM fornecedor ORDER BY Nome_fornecedor", mConn);
                DataTable fornecedor = new DataTable();
                mAdapter.Fill(fornecedor);
                try
                {
                    for (int i = 0; i < fornecedor.Rows.Count; i++)
                    {
                        cmbFornecedor.Items.Add(fornecedor.Rows[i]["Nome_fornecedor"]);

                    }
                }
                catch (MySqlException erro)
                {
                    throw erro;
                }


                //--  Somando automaticamente a coluna valor empenho  ---_-

                somatorio();

                //---------------------------------------------------------

                calculaQuantidadeRegistros();

            } // fim try
            catch
            {
                MessageBox.Show("Insira um valor válido.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }         
            
        }

        private void somatorio()
        {
            Double ValorTotal1 = 0;

            try
            {
                foreach (DataGridViewRow col in dataGridView1.Rows)
                {
                    ValorTotal1 = ValorTotal1 + Convert.ToDouble(col.Cells[3].Value);

                }

                txtTotal.Text = ValorTotal1.ToString("C");
                lblTotal.Text = ValorTotal1.ToString("C");
                txtTotal.Text = txtTotal.Text.Replace("R$","");
                lblTotal.Text = lblTotal.Text.Replace("R$","");

            }
            catch
            {
                MessageBox.Show("Erro na soma. Há valores inconsistentes [coluna valor empenho] nas requisições.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void LimpaCamposFiltros()
        {
            
        }

        private void calculaQuantidadeRegistros()
        {
            if (dataGridView1.RowCount == 1 || dataGridView1.RowCount == 0)
                lblRegistros.Text = (dataGridView1.RowCount).ToString() + " registro";
            else
                lblRegistros.Text = (dataGridView1.RowCount).ToString() + " registros";

        }

       
        private void PesquisaPorReduzida(string codigo)
        {
            
            try
            {

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa where reduzida=" + txtReduzida.Text, mConn);
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE Reduzida=" + codigo + " And dataempenhoSQL BETWEEN '"
                    + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                    + "' ORDER BY dataempenhoSQL", mConn);


                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilhadespesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilhadespesa";

                //Renomeia as colunas
                
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Cetil";
                dataGridView1.Columns[2].HeaderText = "Empenho";
                dataGridView1.Columns[3].HeaderText = "Valor Empenho";
                dataGridView1.Columns[4].HeaderText = "Data Empenho";// essa data é varchar
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Data Empenho";// essa é a data empenho Date (que seria o correto no Banco de Dados
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].HeaderText = "Autorização";
                dataGridView1.Columns[7].HeaderText = "Data AF";
                dataGridView1.Columns[8].HeaderText = "Valor AF";
                dataGridView1.Columns[9].HeaderText = "Fornecedor";
                dataGridView1.Columns[10].HeaderText = "Despesa";
                dataGridView1.Columns[11].HeaderText = "Reduzida";
                dataGridView1.Columns[12].HeaderText = "Cod.Aplic.";
                dataGridView1.Columns[13].HeaderText = "Programa";

                somatorio();

                calculaQuantidadeRegistros();
                //LimpaCamposFiltros();
            }

            catch
            {
                MessageBox.Show("SELECT * FROM planilhadespesa WHERE Reduzida=" + codigo + " And dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                    + "'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            mConn.Close();
            

             
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e){

            if (txtDataInicial.Text == "")
            {
                if (txtDataFinal.Text == "")
                {
                    txtDataInicial.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
                    monthCalendar1.Visible = false;
                }
                else
                {
                    txtDataInicial.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
                    monthCalendar1.Visible = false;
                    verificavalidadedata();

                }
            }
            else
            {
                txtDataFinal.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
                monthCalendar1.Visible = false;
                verificavalidadedata();

            }

            label10.Visible = false;
            mostrarResultados();

        }

        private void verificavalidadedata()
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
               
            }
            else
            {
                MessageBox.Show("Data(s) inválida(s)");
            }

        }

        private void txtDataInicial_MouseEnter(object sender, EventArgs e)
        {
           // monthCalendar1.Visible = true;
        }

        private void txtDataFinal_MouseEnter(object sender, EventArgs e)
        {
            // monthCalendar1.Visible = true;
        }

             

        private void PlanilhaDespesa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                monthCalendar1.Visible = false;
                label10.Visible = false;
            }
            else
            {

            }
          
        }

        
        private void PesquisaPorEmpenho(string p)
        {
            /*
            try
            {

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE reduzida='" + codigo + "' AND (dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", mConn);
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE Empenho=" + p + " And dataempenhoSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);


                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilhadespesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilhadespesa";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Cetil";
                dataGridView1.Columns[2].HeaderText = "Empenho";
                dataGridView1.Columns[3].HeaderText = "Valor Empenho";
                dataGridView1.Columns[4].HeaderText = "Data Empenho";
                dataGridView1.Columns[5].HeaderText = "Data Empenho SQL";
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].HeaderText = "Autorização";
                dataGridView1.Columns[7].HeaderText = "Data AF";
                dataGridView1.Columns[8].HeaderText = "Valor AF";
                dataGridView1.Columns[9].HeaderText = "Fornecedor";
                dataGridView1.Columns[10].HeaderText = "Despesa";
                dataGridView1.Columns[11].HeaderText = "Reduzida";

                somatorio();

                calculaQuantidadeRegistros();
                //LimpaCamposFiltros();
            }

            catch
            {
                MessageBox.Show("SELECT * FROM planilhadespesa WHERE Empenho=" + txtAF.Text + " AND (dataempenhoSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //}
            mConn.Close();
            */

            try
            {

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE reduzida='" + codigo + "' AND (dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", mConn);
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE Empenho=" + p + " And dataempenhoSQL BETWEEN '"
                    + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                    + "ORDER BY dataempenhoSQL'", mConn);


                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilhadespesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilhadespesa";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Cetil";
                dataGridView1.Columns[2].HeaderText = "Empenho";
                dataGridView1.Columns[3].HeaderText = "Valor Empenho";
                dataGridView1.Columns[4].HeaderText = "Data Empenho";// essa data é varchar
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Data Empenho";// essa é a data empenho Date (que seria o correto no Banco de Dados
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].HeaderText = "Autorização";
                dataGridView1.Columns[7].HeaderText = "Data AF";
                dataGridView1.Columns[8].HeaderText = "Valor AF";
                dataGridView1.Columns[9].HeaderText = "Fornecedor";
                dataGridView1.Columns[10].HeaderText = "Despesa";
                dataGridView1.Columns[11].HeaderText = "Reduzida";
                dataGridView1.Columns[12].HeaderText = "Cod.Aplic.";
                dataGridView1.Columns[13].HeaderText = "Programa";
                
                somatorio();

                calculaQuantidadeRegistros();
                //LimpaCamposFiltros();
            }

            catch
            {
                MessageBox.Show("SELECT * FROM planilhadespesa WHERE Empenho=" + txtAF.Text + " AND (dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //}
            mConn.Close();
           

        }

        private void PesquisaPorAF(string p)
        {
            /*
            try
            {

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE reduzida='" + codigo + "' AND (dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", mConn);
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE Autorização=" + p + " And dataempenhoSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);


                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilhadespesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilhadespesa";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Cetil";
                dataGridView1.Columns[2].HeaderText = "Empenho";
                dataGridView1.Columns[3].HeaderText = "Valor Empenho";
                dataGridView1.Columns[4].HeaderText = "Data Empenho";
                dataGridView1.Columns[5].HeaderText = "Data Empenho SQL";
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].HeaderText = "Autorização";
                dataGridView1.Columns[7].HeaderText = "Data AF";
                dataGridView1.Columns[8].HeaderText = "Valor AF";
                dataGridView1.Columns[9].HeaderText = "Fornecedor";
                dataGridView1.Columns[10].HeaderText = "Despesa";
                dataGridView1.Columns[11].HeaderText = "Reduzida";

                somatorio();

                calculaQuantidadeRegistros();
                //LimpaCamposFiltros();
            }

            catch
            {
                MessageBox.Show("SELECT * FROM planilhadespesa WHERE autorização=" + txtAF.Text + " AND (dataempenhoSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //}
            mConn.Close();
            */

            try
            {

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE reduzida='" + codigo + "' AND (dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", mConn);
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE Autorização=" + p + " And dataempenhoSQL BETWEEN '"
                    + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                    + "ORDER BY dataempenhoSQL '", mConn);


                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilhadespesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilhadespesa";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Cetil";
                dataGridView1.Columns[2].HeaderText = "Empenho";
                dataGridView1.Columns[3].HeaderText = "Valor Empenho";
                dataGridView1.Columns[4].HeaderText = "Data Empenho";// essa data é varchar
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Data Empenho";// essa é a data empenho Date (que seria o correto no Banco de Dados
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].HeaderText = "Autorização";
                dataGridView1.Columns[7].HeaderText = "Data AF";
                dataGridView1.Columns[8].HeaderText = "Valor AF";
                dataGridView1.Columns[9].HeaderText = "Fornecedor";
                dataGridView1.Columns[10].HeaderText = "Despesa";
                dataGridView1.Columns[11].HeaderText = "Reduzida";
                dataGridView1.Columns[12].HeaderText = "Cod.Aplic.";
                dataGridView1.Columns[13].HeaderText = "Programa";



                somatorio();

                calculaQuantidadeRegistros();
                //LimpaCamposFiltros();
            }

            catch
            {
                MessageBox.Show("SELECT * FROM planilhadespesa WHERE autorização=" + txtAF.Text + " AND (dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //}
            mConn.Close();    
        
        }

        private void PesquisaPorFornecedor(string p)
        {
            try
            {

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //mAdapter = new MySqlDataAdapter("SELECT * FROM teste where reduzida=" + txtReduzida.Text, mConn);
                //mAdapter = new MySqlDataAdapter("SELECT * FROM teste WHERE reduzida=" + txtReduzida.Text + " AND (data_Empenho BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "')", mConn);
                //mAdapter = new MySqlDataAdapter("SELECT * FROM teste WHERE reduzida='" + codigo + "' AND (data_empenho_Sql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", mConn);
                
                
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE Fornecedor LIKE '%" + p + "%' And dataempenhoSQL BETWEEN '"
                    + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                    + "' ORDER BY dataempenhoSQL", mConn);
                

                //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE Fornecedor LIKE '%" + p + "%' ORDER BY dataempenhoSQL", mConn);

                //////////////////////////// SELECT * FROM prorim.planilhadespesa WHERE FORNECEDOR LIKE 'BAROMED%';

                //SELECT * FROM planilhadespesa WHERE Fornecedor='MCC GONÇALVES' And (dataempenhoSQL BETWEEN '2015/06/25' AND '2015/12/31') order by dataempenhosql;
               
                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilhadespesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilhadespesa";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Cetil";
                dataGridView1.Columns[2].HeaderText = "Empenho";
                dataGridView1.Columns[3].HeaderText = "Valor Empenho";
                dataGridView1.Columns[4].HeaderText = "Data Empenho";// essa data é varchar
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Data Empenho";// essa é a data empenho Date (que seria o correto no Banco de Dados
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].HeaderText = "Autorização";
                dataGridView1.Columns[7].HeaderText = "Data AF";
                dataGridView1.Columns[8].HeaderText = "Valor AF";
                dataGridView1.Columns[9].HeaderText = "Fornecedor";
                dataGridView1.Columns[10].HeaderText = "Despesa";
                dataGridView1.Columns[11].HeaderText = "Reduzida";
                dataGridView1.Columns[12].HeaderText = "Cod.Aplic.";
                dataGridView1.Columns[13].HeaderText = "Programa";


                somatorio();

                calculaQuantidadeRegistros();
                //LimpaCamposFiltros();
            }

            catch
            {
                MessageBox.Show("SELECT * FROM planilhadespesa WHERE Fornecedor LIKE '%" + p + "%' And dataempenhoSQL BETWEEN '"
                    + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                    + "' ORDER BY dataempenhoSQL", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //}
            mConn.Close();

        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void label7_MouseClick(object sender, MouseEventArgs e)
        {
            monthCalendar1.Visible = true;
            label10.Visible = true;
        }

        private void label6_MouseClick(object sender, MouseEventArgs e)
        {
            monthCalendar1.Visible = true;
            label10.Visible = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDataInicial.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtDataFinal.Text = "";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnSoma_Click(object sender, EventArgs e)
        {                    
            Double ValorTotal1 = 0;
                      
                try
                {
                    foreach (DataGridViewRow col in dataGridView1.Rows)
                    {
                        ValorTotal1 = ValorTotal1 + Convert.ToDouble(col.Cells[3].Value);

                    }

                    txtTotal.Text = ValorTotal1.ToString("C");
                    lblTotal.Text = ValorTotal1.ToString("C");
           
                }
                catch
                {
                    MessageBox.Show("Erro na soma. Há valores inconsistentes [coluna valor empenho] nas requisições.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }            
                
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void PesquisaPorDesdobrada(string p)
        {

            try
            {

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa where reduzida=" + txtReduzida.Text, mConn);
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE Despesa=" + p + " And dataempenhoSQL BETWEEN '"
                    + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                    + "' ORDER BY dataempenhoSQL", mConn);


                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilhadespesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilhadespesa";

                //Renomeia as colunas


                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Cetil";
                dataGridView1.Columns[2].HeaderText = "Empenho";
                dataGridView1.Columns[3].HeaderText = "Valor Empenho";
                dataGridView1.Columns[4].HeaderText = "Data Empenho";// essa data é varchar
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Data Empenho";// essa é a data empenho Date (que seria o correto no Banco de Dados
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].HeaderText = "Autorização";
                dataGridView1.Columns[7].HeaderText = "Data AF";
                dataGridView1.Columns[8].HeaderText = "Valor AF";
                dataGridView1.Columns[9].HeaderText = "Fornecedor";
                dataGridView1.Columns[10].HeaderText = "Despesa";
                dataGridView1.Columns[11].HeaderText = "Reduzida";
                dataGridView1.Columns[12].HeaderText = "Cod.Aplic.";
                dataGridView1.Columns[13].HeaderText = "Programa";


                somatorio();

                calculaQuantidadeRegistros();
                //LimpaCamposFiltros();
            }

            catch
            {
                MessageBox.Show("SELECT * FROM planilhadespesa WHERE Despesa=" + p + " And dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                    + "'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            mConn.Close();
            

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btn_relatorio_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dataGridView1);
        }

        
        private void PesquisaPorPrograma(string p)
        {
            try
            {

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //mAdapter = new MySqlDataAdapter("SELECT * FROM teste where reduzida=" + txtReduzida.Text, mConn);
                //mAdapter = new MySqlDataAdapter("SELECT * FROM teste WHERE reduzida=" + txtReduzida.Text + " AND (data_Empenho BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "')", mConn);
                //mAdapter = new MySqlDataAdapter("SELECT * FROM teste WHERE reduzida='" + codigo + "' AND (data_empenho_Sql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", mConn);
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE Programa='" + p + "' And dataempenhoSQL BETWEEN '"
                    + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                    + "' ORDER BY dataempenhoSQL", mConn);

                //SELECT * FROM planilhadespesa WHERE Fornecedor='MCC GONÇALVES' And (dataempenhoSQL BETWEEN '2015/06/25' AND '2015/12/31') order by dataempenhosql;

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilhadespesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilhadespesa";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Cetil";
                dataGridView1.Columns[2].HeaderText = "Empenho";
                dataGridView1.Columns[3].HeaderText = "Valor Empenho";
                dataGridView1.Columns[4].HeaderText = "Data Empenho";// essa data é varchar
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Data Empenho";// essa é a data empenho Date (que seria o correto no Banco de Dados
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].HeaderText = "Autorização";
                dataGridView1.Columns[7].HeaderText = "Data AF";
                dataGridView1.Columns[8].HeaderText = "Valor AF";
                dataGridView1.Columns[9].HeaderText = "Fornecedor";
                dataGridView1.Columns[10].HeaderText = "Despesa";
                dataGridView1.Columns[11].HeaderText = "Reduzida";
                dataGridView1.Columns[12].HeaderText = "Cod.Aplic.";
                dataGridView1.Columns[13].HeaderText = "Programa";


                somatorio();

                calculaQuantidadeRegistros();
                //LimpaCamposFiltros();
            }

            catch
            {
                MessageBox.Show("SELECT * FROM planilhadespesa WHERE Programa='" + p + "' And (dataempenhoSQL BETWEEN '"
                    + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                    + "') ORDER BY dataempenhoSQL;", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //}
            mConn.Close();

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                e.CellStyle.BackColor = Color.LightGray;
            }
        }

        private void txtAno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                txtDataInicial.Text = ""; // DateTime.Now.ToString("01/01/yyyy");
                txtDataFinal.Text = "";


                if (txtAno.Text == "")
                {
                    txtDataInicial.Text = ""; // DateTime.Now.ToString("01/01/yyyy");
                    txtDataFinal.Text = ""; // DateTime.Now.ToString("31/12/yyyy");
                }
                else
                {
                    //txtDataInicial.Text = DateTime.Now.ToString("01/01/yyyy");
                    //txtDataFinal.Text = DateTime.Now.ToString("31/12/yyyy");
                    txtDataInicial.Text = "01/01/" + txtAno.Text;
                    txtDataFinal.Text = "31/12/" + txtAno.Text;

                }

                mostrarResultados();

            }
            else
            {

            }
        }

        private void txtReduzida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                label2.Text = "TOTAL EMPENHOS";
                PesquisaPorReduzida(txtReduzida.Text);
                txtReduzida.Text = "";
            }
        }

        private void txtDesdobrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                label2.Text = "TOTAL EMPENHOS";
                PesquisaPorDesdobrada(txtDesdobrada.Text);
                txtDesdobrada.Text = "";
            }
        }

        private void txtEmpenho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                label2.Text = "TOTAL EMPENHOS";
                PesquisaPorEmpenho(txtEmpenho.Text);
                txtEmpenho.Text = "";
            }
        }

        private void txtNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorNotaFiscal(txtNotaFiscal.Text);
                txtNotaFiscal.Text = "";
            }
        }

        private void PesquisaPorNotaFiscal(string p)
        {
            label2.Text = "TOTAL NOTAS FISCAIS";

            if (txtNotaFiscal.Text != "")
            {
                try
                {
                    mDataSet = new DataSet();
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    mConn.Open();

                    //cria um adapter utilizando a instrução SQL para acessar a tabela 
                    //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE reduzida='" + codigo + "' AND (dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", mConn);
                    mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais WHERE Num_NotaFiscal=" + p + " And DataNotaSQL BETWEEN '"
                        + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                        + "'", mConn);


                    //preenche o dataset através do adapter
                    mAdapter.Fill(mDataSet, "notas_fiscais");

                    //atribui o resultado à propriedade DataSource da dataGridView
                    dataGridView1.DataSource = mDataSet;
                    dataGridView1.DataMember = "notas_fiscais";

                    //Renomeia as colunas
                    dataGridView1.Columns[0].HeaderText = "Codigo da NF";
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Cetil";
                    dataGridView1.Columns[2].HeaderText = "Fornecedor";
                    dataGridView1.Columns[3].HeaderText = "Nota Fiscal";
                    dataGridView1.Columns[4].HeaderText = "Data N.F.";
                    dataGridView1.Columns[5].HeaderText = "Valor N.F.";
                    dataGridView1.Columns[6].HeaderText = "Setor";
                    dataGridView1.Columns[7].HeaderText = "Data Envio";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].Visible = false;

                    somatorioNF();

                    calculaQuantidadeRegistros();
                    //LimpaCamposFiltros();
                }

                catch
                {
                    MessageBox.Show("SELECT * FROM notas_fiscais WHERE Num_NotaFiscal=" + p
                        + " And DataNotaSQL BETWEEN '"
                        + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd")
                        + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                        + "'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //}
                mConn.Close();
            }
            else {
                try
                {
                    mDataSet = new DataSet();
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    mConn.Open();

                    //cria um adapter utilizando a instrução SQL para acessar a tabela 
                    //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE reduzida='" + codigo + "' AND (dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", mConn);
                    mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais WHERE DataNotaSQL BETWEEN '"
                        + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                        + "'", mConn);


                    //preenche o dataset através do adapter
                    mAdapter.Fill(mDataSet, "notas_fiscais");

                    //atribui o resultado à propriedade DataSource da dataGridView
                    dataGridView1.DataSource = mDataSet;
                    dataGridView1.DataMember = "notas_fiscais";

                    //Renomeia as colunas
                    dataGridView1.Columns[0].HeaderText = "Codigo da NF";
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Cetil";
                    dataGridView1.Columns[2].HeaderText = "Fornecedor";
                    dataGridView1.Columns[3].HeaderText = "Nota Fiscal";
                    dataGridView1.Columns[4].HeaderText = "Data N.F.";
                    dataGridView1.Columns[5].HeaderText = "Valor N.F.";
                    dataGridView1.Columns[6].HeaderText = "Setor";
                    dataGridView1.Columns[7].HeaderText = "Data Envio";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].Visible = false;

                    somatorioNF();

                    calculaQuantidadeRegistros();
                    //LimpaCamposFiltros();
                }

                catch
                {
                    MessageBox.Show("SELECT * FROM notas_fiscais WHERE DataNotaSQL BETWEEN '"
                        + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" 
                        + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                        + "'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //}
                mConn.Close();
                        
            }

        }

        private void somatorioNF()
        {

            Double ValorTotal1 = 0;

            try
            {
                foreach (DataGridViewRow col in dataGridView1.Rows)
                {
                    ValorTotal1 = ValorTotal1 + Convert.ToDouble(col.Cells[5].Value);

                }

                txtTotal.Text = ValorTotal1.ToString("C");
                lblTotal.Text = ValorTotal1.ToString("C");
                txtTotal.Text = txtTotal.Text.Replace("R$", "");
                lblTotal.Text = lblTotal.Text.Replace("R$", "");

            }
            catch
            {
                MessageBox.Show("Erro na soma. Há valores inconsistentes [coluna valor N.Fiscal] na planilha.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void txtAF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                label2.Text = "TOTAL EMPENHOS";
                PesquisaPorAF(txtAF.Text);
                txtAF.Text = "";
            }

        }

        private void cmbFornecedor_SelectedValueChanged(object sender, EventArgs e)
        {
            capturarCodigoFornecedor(cmbFornecedor.Text);

            int temp = Convert.ToInt32(label16.Text);
            pesquisarNotaFiscalPeloCodFornecedor(temp);
        }

        private void pesquisarNotaFiscalPeloCodFornecedor(int temp)
        {
                try
                {
                    mDataSet = new DataSet();
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    mConn.Open();

                    //cria um adapter utilizando a instrução SQL para acessar a tabela 
                    //mAdapter = new MySqlDataAdapter("SELECT * FROM planilhadespesa WHERE reduzida='" + codigo + "' AND (dataempenhoSQL BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "');", mConn);
                    mAdapter = new MySqlDataAdapter("SELECT * FROM notas_fiscais WHERE Cod_fornecedor=" + temp + " And DataNotaSQL BETWEEN '"
                        + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                        + "'", mConn);


                    //preenche o dataset através do adapter
                    mAdapter.Fill(mDataSet, "notas_fiscais");

                    //atribui o resultado à propriedade DataSource da dataGridView
                    dataGridView1.DataSource = mDataSet;
                    dataGridView1.DataMember = "notas_fiscais";

                    //Renomeia as colunas
                    dataGridView1.Columns[0].HeaderText = "Codigo da NF";
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Cetil";
                    dataGridView1.Columns[2].HeaderText = "Fornecedor";
                    dataGridView1.Columns[3].HeaderText = "Nota Fiscal";
                    dataGridView1.Columns[4].HeaderText = "Data N.F.";
                    dataGridView1.Columns[5].HeaderText = "Valor N.F.";
                    dataGridView1.Columns[6].HeaderText = "Setor";
                    dataGridView1.Columns[7].HeaderText = "Data Envio";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].Visible = false;

                    somatorioNF();

                    calculaQuantidadeRegistros();
                    //LimpaCamposFiltros();
                }

                catch
                {
                    MessageBox.Show("SELECT * FROM notas_fiscais WHERE Cod_fornecedor=" + temp + " And DataNotaSQL BETWEEN '"
                        + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd")
                        + "'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //}
                mConn.Close();
            }
        

        private void capturarCodigoFornecedor(string p)
        {
            // -------- recuperando o codigo ID do fornecedor escolhido no comboBox -------------------------
            try
            {

                stConection = "Persist Security Info=False;server=" +  Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                string stConsulta = "SELECT Cod_fornecedor FROM fornecedor WHERE Nome_fornecedor='" + cmbFornecedor.Text + "'";

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        label16.Text = myReader["Cod_fornecedor"] + Environment.NewLine;
                    }
                }


            }
            catch
            {

                MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //lblMsg.Text = "Falha na conexão.";
                
            }

            Cmn.Close();
            
        }


        private void cmbFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorFornecedor(cmbFornecedor.Text);
            }       
        }

        private void txtPrograma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                label2.Text = "TOTAL EMPENHOS";
                //PesquisaPorAF(Convert.ToInt32(txtAF.Text));
                PesquisaPorPrograma(txtPrograma.Text);
                txtPrograma.Text = "";
            }            
        }

        private void txtReduzida_Enter(object sender, EventArgs e)
        {
            txtReduzida.BackColor = Color.Yellow;
        }

        private void txtDesdobrada_Enter(object sender, EventArgs e)
        {
            txtDesdobrada.BackColor = Color.Yellow;
        }

        private void txtEmpenho_Enter(object sender, EventArgs e)
        {
            txtEmpenho.BackColor = Color.Yellow;
        }

        private void txtNotaFiscal_Enter(object sender, EventArgs e)
        {
            txtNotaFiscal.BackColor = Color.Yellow;
        }

        private void txtAF_Enter(object sender, EventArgs e)
        {
            txtAF.BackColor = Color.Yellow;
        }

        private void txtPrograma_Enter(object sender, EventArgs e)
        {
            txtPrograma.BackColor = Color.Yellow;
        }

        private void txtAno_Enter(object sender, EventArgs e)
        {
            txtAno.BackColor = Color.Yellow;
        }

        private void txtReduzida_Leave(object sender, EventArgs e)
        {
            txtReduzida.BackColor = Color.White;
        }

        private void txtDesdobrada_Leave(object sender, EventArgs e)
        {
            txtDesdobrada.BackColor = Color.White;
        }

        private void txtEmpenho_Leave(object sender, EventArgs e)
        {
            txtEmpenho.BackColor = Color.White;
        }

        private void txtNotaFiscal_Leave(object sender, EventArgs e)
        {
            txtNotaFiscal.BackColor = Color.White;
        }

        private void txtAF_Leave(object sender, EventArgs e)
        {
            txtAF.BackColor = Color.White;
        }

        private void txtPrograma_Leave(object sender, EventArgs e)
        {
            txtPrograma.BackColor = Color.White;
        }

        private void txtAno_Leave(object sender, EventArgs e)
        {
            txtAno.BackColor = Color.White;
        }     
                
        
    }
}
