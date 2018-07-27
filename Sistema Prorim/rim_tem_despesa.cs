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
    public partial class rim_tem_despesa : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        public string stConection;
        private string stConsulta;
        public MySqlConnection Cmn = new MySqlConnection();
        public string tiporim;
        private int flagInclusao;
        private int tempEmpenho=0;


        public rim_tem_despesa()
        {
            InitializeComponent();
        }

        private void rim_tem_despesa_Load(object sender, EventArgs e)
        {            
                                
            //Código para recuoerar o fornecedor já vinculado à RI. Escolhido na RI.
                       
            txtCodDespesa.Text =  Sistema_prorim.Global.despesa.coddespesas;
            txtCodRim.Text = Sistema_prorim.Global.DadosRim.cetil;
            txtDespesa.Text = Sistema_prorim.Global.despesa.despesas;
            txtCodigoSeqRI.Text = Sistema_prorim.Global.DadosRim.codigo;
                 
            // Essa variável recebe valor do total do empenho ao gravar cada um, somando-os.
            // Valor que será transferido para o valor real na requisição. Deve ser criado flag para
            // forçar o usuário a gravar novamente a RI quando houver alteração no empenho para persistir
            // esse o valor total do empenho atualizado.
            Global.despesa.empenhoTotal = "";

            if (Global.Logon.tipoRequisicao == "1")
            {
                tiporim = "RIM";
            }
            else
            {
                tiporim = "RRP";

            }
            
            statusStrip1.Text = "você pode 'incluir', 'atualizar' ou 'excluir' despesas, empenhos e autorizações relacionados à requisição especificada.";

            popularCmbFornecedorComFornecedorVinculado();
            //populaCmbFornecedorComTodosFornecedores();

            mostrarResultados();
                     
        }

        private void popularCmbFornecedorComFornecedorVinculado()
        {

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            mAdapter = new MySqlDataAdapter("SELECT nome_fornecedor FROM pesquisa_fornecedor WHERE cod_rim=" + txtCodigoSeqRI.Text, mConn);
            DataTable pesquisa_fornecedor = new DataTable();
            mAdapter.Fill(pesquisa_fornecedor);
            try
            {
                for (int i = 0; i < pesquisa_fornecedor.Rows.Count; i++)
                {
                    cmbFornecedor.Items.Add(pesquisa_fornecedor.Rows[i]["nome_fornecedor"]);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: "+ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            mConn.Close();

        }
       
        private void mostrarResultados()
        {
            //txtCodRim.Text = Global.DadosRim.cetil;                

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela
                mAdapter = new MySqlDataAdapter("SELECT * FROM rim_has_dotacao WHERE Cetil='" + Global.DadosRim.cetil + "' ORDER BY Cod_rim", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim_has_dotacao");

                //atribui o resultado à propriedade DataSource do dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim_has_dotacao";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                //dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Cod RI";
                //dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].HeaderText = "Cod Despesa";
                //dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].HeaderText = "Cetil";
                dataGridView1.Columns[4].HeaderText = "Empenho";
                dataGridView1.Columns[5].HeaderText = "Data Empenho";
                dataGridView1.Columns[6].HeaderText = "Valor Empenho";
                dataGridView1.Columns[7].HeaderText = "Autorização";
                dataGridView1.Columns[8].HeaderText = "Data AF";
                dataGridView1.Columns[9].HeaderText = "Valor AF";
                dataGridView1.Columns[10].HeaderText = "Fornecedor";
                dataGridView1.Columns[11].HeaderText = "Tipo";
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].HeaderText = "DT EMP SQL";
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].HeaderText = "Ano RI";
                dataGridView1.Columns[13].Visible = false;
                
                calcularRegistros();
                somaEmpenhos();
                somaAF();
                mConn.Close();//acrescentada ----------------------------------------------------------------------------------
        }

        private void somaAF()
        {
            Double somaAF = 0;

            try
            {
                foreach (DataGridViewRow col in dataGridView1.Rows)
                {
                    somaAF = somaAF + Convert.ToDouble(col.Cells[9].Value);
                    //acumuladoAF = acumuladoAF + Convert.ToDouble(col.Cells[9].Value);
                }

                txtAcumuladoAF.Text = somaAF.ToString("C");
                txtAcumuladoAF.Text = txtAcumuladoAF.Text.Replace("R$", "");

            }
            catch
            {
                txtAcumuladoAF.Text = "0.00";
            }
        }

        private void somaEmpenhos()
        {
            Double somaEmpenho = 0;
            
            try
            {
                foreach (DataGridViewRow col in dataGridView1.Rows)
                {
                    somaEmpenho = somaEmpenho + Convert.ToDouble(col.Cells[6].Value);
                    //acumuladoAF = acumuladoAF + Convert.ToDouble(col.Cells[9].Value);
                }

                txtAcumulado.Text = somaEmpenho.ToString("C");
                txtAcumulado.Text = txtAcumulado.Text.Replace("R$", "");

                
            }
            catch
            {
                txtAcumulado.Text = "0.00";
               
            }

        }

        private void calcularRegistros()
        {
            int registro;
            registro = dataGridView1.RowCount;
            if (registro == 1 || registro == 0)
                label9.Text = registro + " registro";
            else
                label9.Text = registro + " registros";

        }
                
        private void Excluir(int codigo1)
        {
            {
                //conexao
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();
                try
                {
                    //command
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = mConn;
                    cmd.CommandText = "Delete from rim_has_dotacao where Codigo=" + codigo1;

                    int resultado = cmd.ExecuteNonQuery();
                    if (resultado != 1)
                    {
                        throw new Exception("Não foi possível excluir a linha da tabela de vinculo da despesa com a RI de Codigo Sequencial nº " + codigo1);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível excluir a linha da tabela de vinculo da despesa com a RI de Codigo Sequencial nº [" + codigo1 + "] " + "| Delete from rim_has_dotacao where Codigo=" + codigo1 + ex.Message);
                }

                finally
                {
                    MessageBox.Show("Vínculo da despesa com a RI excluída com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    limparCampos();
                    mConn.Close();
                    mostrarResultados();
                    //DesabilitaTextBox();
                }
            }
           
        }

        private void Alterar(int codigo1)
        {
            // Não podemos deixar o cmbFornecedor em branco daí o controle a condição abaixo

            if (cmbFornecedor.Text != "")
            {
                try
                {
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    // Abre a conexão
                    mConn.Open();

                    if (txtAutorizacao.Text != "" || txtDataAutorizacao.Text != "" || txtValorAutorizacao.Text != "")
                    {
                        try
                        {
                            MySqlCommand command = new MySqlCommand("UPDATE rim_has_dotacao SET empenho='" + txtEmpenho.Text + "', dataempenho='"
                                + txtDataEmpenho.Text + "', valorempenho='" + Convert.ToDecimal(txtValorEmpenho.Text) +
                                "', autorizacao='" + txtAutorizacao.Text + "', dataAF='" + txtDataAutorizacao.Text +
                                "', valorAF='" + Convert.ToDecimal(txtValorAutorizacao.Text) + "', nome_fornecedor='" + cmbFornecedor.Text
                                + "' WHERE Codigo=" + codigo1, mConn);

                            //Executa a Query SQL
                            command.ExecuteNonQuery();

                            MessageBox.Show("Atualização realizada com sucesso","Mensagem",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            this.Close();
                        }
                        catch
                        {
                            MessageBox.Show("UPDATE rim_has_dotacao SET empenho='" + txtEmpenho.Text + "', dataempenho='"
                                + txtDataEmpenho.Text + "', valorempenho='" + Convert.ToDecimal(txtValorEmpenho.Text) +
                                "', autorizacao='" + txtAutorizacao.Text + "', dataAF='" + txtDataAutorizacao.Text +
                                "', valorAF='" + Convert.ToDecimal(txtValorAutorizacao.Text) + "', nome_fornecedor='" + cmbFornecedor.Text
                                + "' WHERE Codigo=" + codigo1, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        try
                        {
                            MySqlCommand command = new MySqlCommand("UPDATE rim_has_dotacao SET empenho='" + txtEmpenho.Text + "', dataempenho='"
                               + txtDataEmpenho.Text + "', valorempenho='" + Convert.ToDecimal(txtValorEmpenho.Text) + "', nome_fornecedor='" + cmbFornecedor.Text
                               + "' WHERE Codigo=" + txtCodigo.Text, mConn);

                            //Executa a Query SQL
                            command.ExecuteNonQuery();

                            MessageBox.Show("Atualização realizada com sucesso", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();      
                        }
                        catch
                        {
                            MessageBox.Show("UPDATE rim_has_dotacao SET empenho='" + txtEmpenho.Text + "', dataempenho='"
                               + txtDataEmpenho.Text + "', valorempenho='" + Convert.ToDecimal(txtValorEmpenho.Text) + "', nome_fornecedor='" + cmbFornecedor.Text
                               + "' WHERE Codigo=" + txtCodigo.Text, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                    mostrarResultados();
                                    }
                catch
                {
                    MessageBox.Show("Não foi possível fazer a conexão", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else {
                MessageBox.Show("Você deve escolher o fornecedor informado na A.F.","Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //groupBox1.Height = 212;
                cmbFornecedor.Focus();
                btnOK.Visible = true;
            }
            btnIncluir.Visible = false;
            btnOK.Visible = true;
        }

        private void Gravar()
        {
            if (txtCodDespesa.Text == "")           
                        
            {
                MessageBox.Show("Despesa não encontrada.", "Erro na Vinculação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MySqlCommand command = new MySqlCommand("INSERT INTO rim_has_dotacao (Cod_rim,Cod_despesa,Cetil,empenho,dataempenho,valorempenho,nome_fornecedor,tipo_rim) VALUES("
                    + Convert.ToInt32(txtCodigoSeqRI.Text) + "," + Convert.ToInt32(txtCodDespesa.Text) + ",'" + txtCodRim.Text + "','" + txtEmpenho.Text + "','" + txtEmpenho.Text 
                    + "','" + txtValorEmpenho.Text + "','" + cmbFornecedor.Text + "','" + tiporim + "')", mConn);

                    //MySqlCommand command = new MySqlCommand("INSERT INTO rim_has_dotacao (Cod_rim,Cod_despesa,Cetil) VALUES('" + txtCodRim.Text
                    //+ "'," + txtCodDespesa.Text + ",'" + txtCodRim.Text + "')", mConn);
                 
                    //Executa a Query SQL  
                    command.ExecuteNonQuery();
                
                    mostrarResultados();
                    calcularRegistros();
                    
                    //Mensagem de Sucesso
                    MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        
                    // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"

                    limparCampos();
                    //DesabilitaTextBox();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("INSERT INTO rim_has_dotacao (Cod_rim,Cod_despesa,Cetil,empenho,dataempenho,valorempenho,nome_fornecedor) VALUES(" + Convert.ToInt32(txtCodRim.Text)
                    + "," + Convert.ToInt32(txtCodDespesa.Text) + ",'" + txtCodRim.Text + "','" + txtEmpenho.Text + "','" + txtEmpenho.Text + "','" + txtValorEmpenho.Text + "','" + cmbFornecedor.Text + "','" + tiporim   + "')", "Erro na gravação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                // LIMPANDO VARIÁVEIS.
                LimparVariáveis();

                // Variável que recebe o valor acumulado em txtAcumulado ref. total empenho gravado.
                       
            }

            Global.despesa.empenhoTotal = txtAcumulado.Text;
        }

        private void LimparVariáveis()
        {
            Global.despesa.coddespesas = "";
            Global.despesa.despesas = "";
            Global.NotaFiscal.codigoRI = "";
            Global.RI.cetil = "";
            Global.despesa.empenhoTotal = "";                
        }
        
 
        private void limparCampos()
        {
            txtCodDespesa.Text = "";
            txtDespesa.Text = "";
            txtCodRim.Text = "";
            txtCodigo.Text = "";
            txtEmpenho.Text = "";
            txtValorEmpenho.Text = "";
            txtDataEmpenho.Text = "";
            txtAutorizacao.Text = "";
            txtDataAutorizacao.Text = "";
            txtValorAutorizacao.Text = "";
        }

       
        private void HabilitaTextBox()
        {
            txtDespesa.Enabled = true;

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            groupBox1.Height = 71;

            //-------- verifica a quantidade de registros. Se for '0' ao fechar, força a inclusão de pelo menos uma despesa.

                       
            if (txtCodDespesa.Text == "")
            {

                MessageBox.Show("Deve ser incluída pelo menos uma despesa para cada R.I", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                Global.despesa.coddespesas = "";
                Global.despesa.despesas = "";
                Global.NotaFiscal.codigoRI = "";
                Global.RI.cetil = "";
                this.Close();
            }

            statusStrip1.Text = "";

        }

        private void analisandoDatagrid()
        {
            // se a tabela estiver vazia de despesa para aquela RI, é gerada uma mensagem exigindo o registro de pelo menos 
            // uma despesa.

        }
                 
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //bt_Gravar.Enabled = true;
           
            //-----------------codigo transferido do rbAtualizar
            /*
            txtEmpenho.Visible = true;
            txtDataEmpenho.Visible = true;
            txtValorEmpenho.Visible = true;

            lblEmpenho.Visible = true;
            lblDataEmpenho.Visible = true;
            lblValorEmpenho.Visible = true;

            lblAF.Visible = true;
            lblDataAF.Visible = true;
            lblValorAF.Visible = true;

            lblFornecedor.Visible = true;
            cmbFornecedor.Visible = true;

            textBox1.Visible = true;
            textBox6.Visible = true;
            label10.Visible = true;
            label8.Visible = true;
            txtValorEmpenho.Enabled = true;
            txtDataAutorizacao.Visible = true;
            txtValorAutorizacao.Visible = true;
            txtAutorizacao.Visible = true;
            txtValorAutorizacao.Enabled = true;

            //---------------------------------
                        
            groupBox1.Height = 222;
            txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString().Trim();
            txtCodRim.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodDespesa.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodigohasDotacao.Text = dataGridView1[3,dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEmpenho.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDataEmpenho.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtValorEmpenho.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtAutorizacao.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDataAutorizacao.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtValorAutorizacao.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            
            if (txtEmpenho.Text == "")
            {
                txtEmpenho.Focus();
            }
            else
            {
                txtAutorizacao.Focus();
            }
            
            try
            {
                stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                stConsulta = "SELECT Despesa FROM dotacao WHERE Cod_Despesa=" + Convert.ToInt32(txtCodDespesa.Text);

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        txtDespesa.Text = myReader["Despesa"] + Environment.NewLine;
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("SELECT Despesa FROM dotacao WHERE Cod_Despesa='" + txtCodDespesa.Text + "'"+ex.Message,"Atenção",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            Cmn.Close();
            

            //Populando cmbFornecedor somente com os fornecedores já vinculados à Requisição
            //tabela pesquisa_fornecedor (pelo nome) é uma VIEW
            mAdapter = new MySqlDataAdapter("Select * from pesquisa_fornecedor where cod_rim=" + Convert.ToInt32(txtCodigoSeqRI.Text), mConn);
            DataTable pesquisa_fornecedor = new DataTable();
            mAdapter.Fill(pesquisa_fornecedor);
            try
            {
                for (int i = 0; i < pesquisa_fornecedor.Rows.Count; i++)
                {
                    cmbFornecedor.Items.Add(pesquisa_fornecedor.Rows[i]["nome_fornecedor"]);

                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }
            
            */

            panel1.Enabled = true;

            txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodigoSeqRI.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodigoSeqDespesa.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodDespesa.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodRim.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            // Vamos abrir conexão com a tabela de Dotacao para ver qual despesa tem codigo txtCodigo.
            stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
            Cmn.ConnectionString = stConection;
            Cmn.Open();
            try
            {               
                stConsulta = "SELECT Despesa FROM dotacao WHERE Cod_Despesa=" + Convert.ToInt32(txtCodDespesa.Text);

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        txtDespesa.Text = myReader["Despesa"] + Environment.NewLine;
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("SELECT Despesa FROM dotacao WHERE Cod_Despesa='" + txtCodDespesa.Text + "'"+ex.Message,"Atenção",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            Cmn.Close();            

            txtEmpenho.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDataEmpenho.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtValorEmpenho.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtAutorizacao.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDataAutorizacao.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtValorAutorizacao.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            //Há duas situações possíveis: se for alteração (controlada flag tempEmpenho=0) o cmb deve receber a informação
            //no cmbFornecedor que está na coluna 10 de rim_has_dotacao. Se for inclusão (controlada flag tempEmpenho=1) segue 
            //o codigo original

            if (flagInclusao == 0) {
                cmbFornecedor.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                cmbFornecedor.SelectedIndex = 0;
            }
            

        }
        
        private void rim_tem_despesa_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void txtEmpenho_TextChanged(object sender, EventArgs e)
        {
            /*
            if (txtCodDespesa.Text == "")
            {
                MessageBox.Show("Você deve escolher algum item na planilha", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //txtEmpenho.Text = "";
                txtEmpenho.Focus();
            }
            else
            {
            }
             * */
        }

        private void txtValorEmpenho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Convert.ToDouble(txtValorEmpenho.Text) <= 0)
                {                      
                    MessageBox.Show("Valor não pode ser nulo ou negativo", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValorEmpenho.Focus();
                }
                else 
                {
                    txtValorEmpenho.Text = Convert.ToDouble(txtValorEmpenho.Text).ToString("C");
                    txtValorEmpenho.Text = txtValorEmpenho.Text.Replace("R$", "");

                    txtAutorizacao.Focus();
                    //bt_Gravar.Focus();
                }

            }
            else
            {

                txtValorEmpenho.Focus();

            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void tssMensagem_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

       

        private void txtDataAutorizacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtValorEmpenho.Text == "")
                {
                    txtDataAutorizacao.Focus();
                }
                else
                {
                    txtValorAutorizacao.Focus();
                }
            }
        }

        private void txtDataEmpenho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtValorEmpenho.Text == "")
                {
                    txtDataEmpenho.Focus();
                }
                else
                {
                    txtValorEmpenho.Focus();
                }
            }
        }

  
        private void txtValorAutorizacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Convert.ToDouble(txtValorAutorizacao.Text) <= 0)
                {
                    MessageBox.Show("Valor não pode ser nulo ou negativo", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValorEmpenho.Focus();
                }
                else
                {
                    txtValorAutorizacao.Text = Convert.ToDouble(txtValorAutorizacao.Text).ToString("C");
                    txtValorAutorizacao.Text = txtValorAutorizacao.Text.Replace("R$", "");

                    txtAutorizacao.Focus();
                    //bt_Gravar.Focus();
                }

            }
            else
            {

                txtValorAutorizacao.Focus();

            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        
        private void txtEmpenho_Leave(object sender, EventArgs e)
        {
            txtEmpenho.BackColor = Color.White;
        }

        private void txtDataEmpenho_Leave(object sender, EventArgs e)
        {
            txtDataEmpenho.BackColor = Color.White;
        }

        private void txtValorEmpenho_Leave(object sender, EventArgs e)
        {
            txtValorEmpenho.BackColor = Color.White;
        }

        private void txtAutorizacao_Leave(object sender, EventArgs e)
        {
            txtAutorizacao.BackColor = Color.White;
        }

        private void txtDataAutorizacao_Leave(object sender, EventArgs e)
        {
            txtDataAutorizacao.BackColor = Color.White;
        }

        private void txtValorAutorizacao_Leave(object sender, EventArgs e)
        {
            txtValorAutorizacao.BackColor = Color.White;
        }

        private void txtEmpenho_Enter(object sender, EventArgs e)
        {
            txtEmpenho.BackColor = Color.Yellow;
        }

        private void txtDataEmpenho_Enter(object sender, EventArgs e)
        {
            txtDataEmpenho.BackColor = Color.Yellow;
        }

        private void txtValorEmpenho_Enter(object sender, EventArgs e)
        {
            txtValorEmpenho.BackColor = Color.Yellow;
        }

        private void txtAutorizacao_Enter(object sender, EventArgs e)
        {
            txtAutorizacao.BackColor = Color.Yellow;
        }

        private void txtDataAutorizacao_Enter(object sender, EventArgs e)
        {
            txtDataAutorizacao.BackColor = Color.Yellow;
        }

        private void txtValorAutorizacao_Enter(object sender, EventArgs e)
        {
            txtValorAutorizacao.BackColor = Color.Yellow;
        }

                
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = true;
            //flagInclusao=1 (inserir) / flagInclusao=0 (alterar) / flagInclusao=2 (Excluir)
            flagInclusao = 2;
            limparCampos();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            flagInclusao = 1;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = false;

            if (txtCodDespesa.Text == "")
            {
                MessageBox.Show("Campos não podem estar vazios. Volte no formulário da requisição e escolha uma despesa válida para vinculá-la.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }else{
                Gravar();                
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Campos obrigatórios na inclusão.
            // Nome, Login, Senha e Tipo de Usuário. Esse último se - não marcado - ficará como usuário tipo comum.
            // Portanto a verificação será dos campos três iniciais

            if (txtCodDespesa.Text == "")
            {
                //if (flagInclusao == 1)
                //{
                //    MessageBox.Show("Campos não podem estar vazios. Volte no formulário da requisição e escolha uma despesa válida para vinculá-la.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //}
                //else
                //{
                    if (flagInclusao == 0)
                    {
                        MessageBox.Show("Campos não podem estar vazios. Escolha na planilha a linha correspondente cujos dados devam ser alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        MessageBox.Show("Campos não podem estar vazios. Escolha na planilha a linha correspondente cujos dados devam ser excluídos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                //}
            }
            else
            {
                // O botão OK serve tanto para inclusão quanto para alteração e exclusão de dados da tabela em questão
                // Foi criada um variável flag para informar o que estamos fazendo. Inclusão, alteração ou exclusão de dados.
                /*
                //btnOK.Visible = true;
                //if (flagInclusao == 1)
                //{
                    Gravar();
                    btnIncluir.Enabled = true;
                    btnAlterar.Enabled = true;
                    btnExcluir.Enabled = true;
                    btnOK.Visible = false;
                //}
                //else
                //{  // essa linha só serve para casos de alteração de dados e exclusão                    
                  
                 */
                if (flagInclusao == 0)
                {
                    if (dataGridView1.RowCount == 0)
                    {
                        MessageBox.Show("Não há dados a serem alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnIncluir.Enabled = true;
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                    }
                    else
                    {
                        if (txtCodigo.Text == "")
                        {
                            MessageBox.Show("Não foi escolhido item a ser alterado. Escolha na planilha clicando duas vezes com o mouse na linha correspondente","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                        else
                        {
                            Alterar(Convert.ToInt32(txtCodigo.Text));
                            btnIncluir.Enabled = true;
                            btnAlterar.Enabled = true;
                            btnExcluir.Enabled = true;
                            btnOK.Visible = false;
                        }
                    }
                }
                else
                {
                    if (dataGridView1.RowCount == 0)
                    {
                        MessageBox.Show("Não há dados a serem excluídos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnIncluir.Enabled = true;
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                    }
                    else
                    {
                        Excluir(Convert.ToInt32(txtCodigo.Text));
                        btnIncluir.Enabled = true;
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnOK.Visible = false;
                    }
                }
                //}
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Não há dados a serem alterados.","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Clique na planilha sobre a linha cujos dados devam ser alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                panel1.Visible = true;
                groupBox1.Visible = true;
                dataGridView1.Enabled = true;
                btnIncluir.Enabled = false;
                btnAlterar.Enabled = false;
                btnExcluir.Enabled = false;
                btnOK.Visible = true;
                flagInclusao = 0;
            } 
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEmpenho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {
                monthCalendar1.Visible = true;
                tempEmpenho = 1;// para saber se o que for selecionado no monthCalendar1 vai para empenho 
                //ou para autorização.

                if (txtEmpenho.Text == "")
                {
                    txtEmpenho.Focus();
                }
                else {
                    txtDataEmpenho.Focus();                 
                }            
            }
        }

        private void txtAutorizacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                monthCalendar1.Visible = true;
                tempEmpenho = 0;// para saber se o que for selecionado no monthCalendar1 vai para empenho 
                //ou para autorização.

                if (txtValorEmpenho.Text == "")
                {
                    txtAutorizacao.Focus();
                }
                else
                {
                    txtDataAutorizacao.Focus();

                }
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (tempEmpenho==0)
            {
                txtDataAutorizacao.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
                monthCalendar1.Visible = false;
                txtValorAutorizacao.Focus();
            }
            else 
            {
                txtDataEmpenho.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
                monthCalendar1.Visible = false;
                txtValorEmpenho.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            btnIncluir.Visible = true;
            btnIncluir.Enabled = true;
            btnAlterar.Visible = true;
            btnAlterar.Enabled = true;
            btnExcluir.Visible = true;
            btnExcluir.Enabled = true;
        }

        private void cmbFornecedor_SelectedValueChanged(object sender, EventArgs e)
        {
            btnOK.Visible = true;
        }

        private void rim_tem_despesa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27) {

                monthCalendar1.Visible = false;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            tempEmpenho = 1;
            monthCalendar1.Visible = true;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            tempEmpenho = 0;
            monthCalendar1.Visible = true;
        }    
        
     
    }
}