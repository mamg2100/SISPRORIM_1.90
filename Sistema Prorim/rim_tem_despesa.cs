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


        public rim_tem_despesa()
        {
            InitializeComponent();
        }

        private void rim_tem_despesa_Load(object sender, EventArgs e)
        {            
            rbNovo.Checked = true;

            bt_Gravar.Focus();
            bt_Cancelar.Enabled = true;
            txtCodDespesa.Text = Global.despesa.coddespesas;
            txtCodRim.Text = Global.NotaFiscal.codigoRI;
            txtDespesa.Text = Global.despesa.despesas;
            txtCodigoSeqRI.Text = Global.RI.codcetil;
            
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
            txtCodigo.Text = txtCodRim.Text;
            txtCodRim.Enabled = false;
            mostrarResultados();
            HabilitaRadionButtons();          
        }

        private void mostrarResultados()
        {
        
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela
            mAdapter = new MySqlDataAdapter("SELECT * FROM rim_has_dotacao WHERE Cetil= '" + txtCodRim.Text + "'ORDER BY Cod_rim", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "rim_has_dotacao");

            //atribui o resultado à propriedade DataSource do dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "rim_has_dotacao";

            //Renomeia as colunas
            dataGridView1.Columns[0].HeaderText = "Código";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "ID RIM";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "ID Despesa";
            dataGridView1.Columns[2].Visible = false;
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
            dataGridView1.Columns[13].Visible = false;

                        
            calculaRegistros();

          //--------- apresentando o valor acumulado sempre que atualizar -----------
           
            somaEmpenhos();
            somaAF();

            /*
            Double acumulado = 0;
            Double acumuladoAF = 0;

            try
            {
                foreach (DataGridViewRow col in dataGridView1.Rows)
                { 
                    acumulado = acumulado + Convert.ToDouble(col.Cells[6].Value);
                    acumuladoAF = acumuladoAF + Convert.ToDouble(col.Cells[9].Value);
                }

                txtAcumulado.Text = acumulado.ToString("C");
                txtAcumulado.Text = txtAcumulado.Text.Replace("R$","");

                txtAcumuladoAF.Text = acumuladoAF.ToString("C");
                txtAcumuladoAF.Text = txtAcumuladoAF.Text.Replace("R$", "");
                
            }
            catch
            {
                txtAcumulado.Text = "0.00";
                txtAcumuladoAF.Text = "0.00";
            }

            //------------------------------------------------------------------------------
            */

            calculaRegistros();

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

        private void bt_Gravar_Click(object sender, EventArgs e)
        {
            groupBox1.Height = 71;
            Global.despesa.flag_valor_real = "1";
            Global.despesa.empenhoTotal = txtAcumulado.Text;
            
            if (rbNovo.Checked == true)
            
            {
                if (txtCodRim.Text == "")
                {
                   // txtCodRim.Enabled = true;
                   // txtCodRim.Focus();
                   toolStripStatusLabel4.Text = "confirme os dados capturados.";

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

                    if (rbAlterar.Checked == true)
                    {
                        //int codigo0 = Convert.ToInt32(txtCodigohasDotacao);
                        int codigo1 = Convert.ToInt32(txtCodigo.Text);
                        //int codigo2 = Convert.ToInt32(txtCodDespesa.Text);
                        Alterar(codigo1);
                    }
                    else
                    {
                        //int codigo0 = Convert.ToInt32(txtCodigohasDotacao);
                        int codigo1 = Convert.ToInt32(txtCodigo.Text);
                        //int codigo2 = Convert.ToInt32(txtCodDespesa.Text);
                        Excluir(codigo1);
                    }
                }
                catch 
                {
                    MessageBox.Show("Erro:Código da despesa (parâmetro) não definido. Verifique.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void Excluir(int codigo1)
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
                    //cmd.CommandText = "delete from rim_has_dotacao where Codigo=" + codigo0 + "AND" + "Cod_rim=" + codigo1 + " AND " 
                    //+ "Cod_despesa=" + codigo2;
                    //mConn.Open();
                    cmd.CommandText = "delete from rim_has_dotacao where Codigo=" + codigo1;


                    int resultado = cmd.ExecuteNonQuery();
                    if (resultado != 1)
                    {
                        throw new Exception("Não foi possível excluir a Despesa" + codigo1);
                    }

                    //MessageBox.Show("delete from rim_has_dotacao where Codigo=" + codigo0 + "AND" + "Cod_rim=" + codigo1 + " AND " 
                    //+ "Cod_despesa=" + codigo2, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("delete from rim_has_dotacao where Codigo=" + codigo1, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch 
                {
                    //MessageBox.Show("Falha na conexão com o Banco de Dados [delete]. Erro:" + ex.Number, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // MessageBox.Show("delete from rim_has_dotacao where Codigo=" + codigo0 + "AND" + "Cod_rim=" + codigo1 + " AND "
                    // + "Cod_despesa=" + codigo2, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Não foi possível excluir a Despesa [" + codigo1 + "]", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                }

                //mostrarResultados();
                mConn.Close();
                
                UncheckedRadioButtons();
                mostrarResultados();
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
                    //mConn.Close();

                }
                catch
                {
                    MessageBox.Show("Não foi possível fazer a conexão", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else {
                MessageBox.Show("Você deve escolher o fornecedor informado na A.F.","Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupBox1.Height = 212;
                cmbFornecedor.Focus();            
            }
        }

        private void Gravar()
        {
            if (txtCodDespesa.Text == "")           
                        
            {
                MessageBox.Show("Despesa não encontrada.", "Erro na gravação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    + Convert.ToInt32(txtCodRim.Text) + "," + Convert.ToInt32(txtCodDespesa.Text) + ",'" + txtCodRim.Text + "','" + txtEmpenho.Text + "','" + txtEmpenho.Text 
                    + "','" + txtValorEmpenho.Text + "','" + cmbFornecedor.Text + "','" + tiporim + "')", mConn);

                    //MySqlCommand command = new MySqlCommand("INSERT INTO rim_has_dotacao (Cod_rim,Cod_despesa,Cetil) VALUES('" + txtCodRim.Text
                    //+ "'," + txtCodDespesa.Text + ",'" + txtCodRim.Text + "')", mConn);
                 
                    //Executa a Query SQL  
                    command.ExecuteNonQuery();
                
                    mostrarResultados();
                    calculaRegistros();
                    
                    //Mensagem de Sucesso
                    MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        
                    // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"

                    LimpaCampos();
                    DesabilitaTextBox();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("INSERT INTO rim_has_dotacao (Cod_rim,Cod_despesa,Cetil,empenho,dataempenho,valorempenho,nome_fornecedor) VALUES(" + Convert.ToInt32(txtCodRim.Text)
                    + "," + Convert.ToInt32(txtCodDespesa.Text) + ",'" + txtCodRim.Text + "','" + txtEmpenho.Text + "','" + txtEmpenho.Text + "','" + txtValorEmpenho.Text + "','" + cmbFornecedor.Text + "','" + tiporim   + "')", "Erro na gravação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                Global.despesa.coddespesas = "";
                Global.despesa.despesas = "";
                Global.NotaFiscal.codigoRI = "";
                Global.RI.cetil = "";
                // Variável que recebe o valor acumulado em txtAcumulado ref. total empenho gravado.
                       
            }

            Global.despesa.empenhoTotal = txtAcumulado.Text;
        }

        private void UncheckedRadioButtons()
        {
            rbAlterar.Checked = false;
            rbExclui.Checked = false;
            rbNovo.Checked = false;

        }

        private void HabilitaRadionButtons()
        {
            rbAlterar.Enabled = true;
            rbExclui.Enabled = true;
            rbNovo.Enabled = true;
        }

        private void DesabilitaTextBox()
        {

        }

        private void LimpaCampos()
        {
            txtCodDespesa.Text = "";
            txtDespesa.Text = "";
            txtCodRim.Text = "";
            txtCodigo.Text = "";
            txtEmpenho.Text = "";
            txtDataEmpenho.Text = "";
            txtAutorizacao.Text = "";
            txtDataAutorizacao.Text = "";
            txtValorAutorizacao.Text = "";

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bt_Gravar.Enabled = false;
            UncheckedRadioButtons();
            LimpaCampos();
            DesabilitaTextBox();
            HabilitaRadionButtons();
        }

        private void rbNovo_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "módulo inclusão ativado";
            txtCodDespesa.Enabled = false;
            //txtCodRim.Enabled = true;
            txtDespesa.Enabled = false;
            txtCodigo.Enabled = false;
            //HabilitaTextBox();
            DesabilitaRadioButtons();
            bt_Gravar.Enabled = true;
            // btnExcluir.Enabled = false;
            tssMensagem.Text = "módulo inclusão ativado";
            
        }

        private void DesabilitaRadioButtons()
        {
            rbAlterar.Enabled = false;
            rbExclui.Enabled = false;
            rbNovo.Enabled = false;
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

        private void rbAlterar_CheckedChanged(object sender, EventArgs e)
        {

            if (rbAlterar.Checked == true)
            {                
                tssMensagem.Text = "módulo atualização ativado";
                rbExclui.Enabled = false;
                rbNovo.Enabled = false;

                MessageBox.Show("Duplo clique na planilha para escolher os dados para alteração", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //groupBox1.Height = 148;
                dataGridView1.Visible = true;
                dataGridView1.Enabled = true;
                dataGridView1.Focus();
                
                /* Codigo transferido para duplo clique no data grid, pois é obrigatorio a escolha da despesa para atualização dos dados
                txtEmpenho.Visible = true;
                txtDataEmpenho.Visible = true;
                txtValorEmpenho.Visible = true;
                
                lblEmpenho.Visible = true;
                lblDataEmpenho.Visible = true;
                lblValorEmpenho.Visible = true;
                
                lblAF.Visible = true;
                lblDataAF.Visible = true;
                lblValorAF.Visible = true;                
                
                textBox1.Visible = true;
                textBox6.Visible = true;
                txtValorEmpenho.Enabled = true;
                txtDataAutorizacao.Visible=true;
                txtValorAutorizacao.Visible = true;
                txtAutorizacao.Visible = true;
                txtValorAutorizacao.Enabled = true;
                */
            }
            else
            {

            }

        }
         
        private void rbExclui_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExclui.Checked == true)
            {
                tssMensagem.Text = "módulo exclusão ativado ativado";

                MessageBox.Show("Confira os dados ou duplo clique na planilha para escolhê-los e depois botão [Confirmar]", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //rbAlterar.Checked = false;
                //LimpaCampos();
                //HabilitaTextBox();
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

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bt_Gravar.Enabled = true;
            //btnAtualizar.Enabled = false;

            //-----------------codigo transferido do rbAtualizar

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
            txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
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

                stConsulta = "SELECT Despesa FROM dotacao WHERE Cod_Despesa='" + txtCodDespesa.Text + "'";

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
            catch
            {
                MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            //
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
            

        private void rim_tem_despesa_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void txtDataEmpenho_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtEmpenho_TextChanged(object sender, EventArgs e)
        {
            if (txtCodDespesa.Text == "")
            {
                MessageBox.Show("Você deve escolher algum item na planilha", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //txtEmpenho.Text = "";
                txtEmpenho.Focus();
            }
            else
            {
            }
        }

        private void txtValorEmpenho_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodDespesa_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDespesa_TextChanged(object sender, EventArgs e)
        {

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

        }

        private void txtDataEmpenho_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lblCodRI_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtCodigoSeqRI_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void bt_Cancelar_Click(object sender, EventArgs e)
        {
            rbAlterar.Enabled = true;
            rbExclui.Enabled = true;
            rbNovo.Enabled = true;
            rbAlterar.Checked = true;
            rbExclui.Checked= true;
            rbNovo.Checked = true;

        }
    }
}