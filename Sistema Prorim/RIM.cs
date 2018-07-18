using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System. Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Globalization;
using Sistema_Prorim;

namespace Sistema_prorim
{
    public partial class RIM : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        String TipoRIM;
        String temp;
        String temp_1;
        //int codunidade = 0;
        //int codigo;
        int estadocodigo = 1;
        int estadoident = 1;
        int estado = 0;
        public string stConection;
        private string stConsulta;
        public MySqlConnection Cmn = new MySqlConnection();
       
        int codigoultimari=0; // recebe o numero da ultima ri cadastrada 
        
        public RIM()
        {
            InitializeComponent();
            stConsulta = "";
            stConection = "";            
        }

        private void RIM_Load(object sender, EventArgs e)
        {
            lblInformacao.Text = "";
            //-----------------------------------
            //Recupera que está gravado no arquivo Path: "d:\\IPSERVIDOR.txt"
            //StreamReader objReader = new StreamReader("d:\\IPSERVIDOR.txt");
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

            //-----------------------------------

            txtCetil.Focus();
            
            //--------------------------------------
            lblModuloVisualização.Visible = false;
            dataGridView1.Visible = false;
            btnCancelar.Visible = true;
            
            label28.Visible = false;
            groupBox3.Visible = false;
            GroupBox1.Visible = true; 
            groupBox2.Visible = true;
            bt_Cancelar.Visible = false;
            GroupBox1.Enabled = true;
            groupBox4.Visible = false;
                        
            //WebBrowser1.Visible = true;
            rbNovo.Enabled = true;
            rbNovo.Checked = true;

            LimpaCampos();
            
            analisaRadioButton();

            //DesabilitaRadioButtons();
            bt_Gravar.Enabled = true;
            

            //--------------------------------------
            
            cmbcadastradoPor.Text = Global.Logon.nome_usuario;
            txtCodUsuario.Text = Global.Logon.codigo_usuario;

            //-------------------------------------

            toolStripStatusMensagem.Text = "módulo de consulta ativado";
                        
            txtAno.Text = DateTime.Today.ToString("yyyy");
            txtAnoProcesso.Text = DateTime.Today.ToString("yyyy");
            txtAnoProcessoContabil.Text = DateTime.Today.ToString("yyyy");
            
                        
            //WebBrowser1.Navigate("C:\\SISprorim_BETA\\Protocolo Requisicoes C#\\SIS prorim\\2554.gif");
                                 
            txtAnoValido.Text = DateTime.Now.ToString("yyyy");

            if (txtAnoValido.Text != "")
            {
                txtDataInicial.Text = "01/01/" + txtAnoValido.Text;
                txtDataFinal.Text = "31/12/" + txtAnoValido.Text;

            }
            else
            {

            }

            if (Global.Logon.tipoRequisicao == "1")
            {
                radioButtonRIM.Checked = true;
            }
            else
            {
                radioButtonRRP.Checked = true;

            }

            if (Global.Veiculos.veiculo == "1")
            {
                radioButtonVeiculo.Checked = false;
                ocultadadosveiculos();
                btnVeiculos.Visible = false;
                txtdescricao.Height = 94;
                txtdescricao.Top = 92;
                lblDescricao.Top = 92;

            }
            else 
            {
                btnVeiculos.Visible = true;
                radioButtonVeiculo.Checked = true;
                mostradadosveiculos();
                lblVeiculosVinculados.Visible = true;
                txtVerificaVeiculo.Visible = true;
            }

                       
            if (radioButtonRRP.Checked == true)
            {
                txtProcessoContabil.Visible = true;
                label37.Visible = true;
                txtAnoProcessoContabil.Visible = true;
                textBox7.Visible = true;
            }
            else
            {
                txtProcessoContabil.Visible = false;
                label37.Visible = false;
                txtAnoProcessoContabil.Visible = false;
                textBox7.Visible = false;
            }

            // POPULANDO TODOS ComboBox
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
                    cmbEscolha.Items.Add(unidade.Rows[i]["Nome_Unidade"]);
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

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

            //---------------------------------------------------------
            // populando cmbCadastradoPor

            mAdapter = new MySqlDataAdapter("SELECT * FROM usuario ORDER BY Nome_usuario", mConn);
            DataTable usuario = new DataTable();
            mAdapter.Fill(usuario);
            try
            {
                for (int i = 0; i < usuario.Rows.Count; i++)
                {
                    cmbcadastradoPor.Items.Add(usuario.Rows[i]["Nome_usuario"]);
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

            //---------------------------------------------------------
            // populando cmbPlacaConsulta

            mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos ORDER BY Placa", mConn);
            DataTable veiculos = new DataTable();
            mAdapter.Fill(veiculos);
            try
            {
                for (int i = 0; i < veiculos.Rows.Count; i++)
                {
                    cmbPlacaConsulta.Items.Add(veiculos.Rows[i]["Placa"]);
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

            //---------------------------------------------------------
           
           
            mConn.Close();

            mostrarResultados();
            }
            catch
            {
                MessageBox.Show("Erro! Informe um IP válido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }        

        private void mostradadosveiculos()
        {
            lblPlaca.Visible=true;
            lblUnidadeGestora.Visible = true;
            cmbPlaca.Visible = true;
            lblMarca.Visible = true;
            txtMarca.Visible = true;
            lblModelo.Visible = true;
            txtModelo.Visible = true;
            lblAnoVeiculo.Visible = true;
            txtAnoVeiculo.Visible = true;
            txtSetorVeiculo.Visible = true;
        }
        
        private void ocultadadosveiculos()
        {
            lblPlaca.Visible = false;
            cmbPlaca.Visible = false;
            lblMarca.Visible = false;
            txtMarca.Visible = false;
            lblModelo.Visible = false;
            txtModelo.Visible = false;
            lblAnoVeiculo.Visible = false;
            txtAnoVeiculo.Visible = false;
            txtSetorVeiculo.Visible = false;

        }

        private void calculaCodigo()
        {
            try
            { 

                stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                //stConsulta = "SELECT Co d_unidade FROM unidade WHERE Cod_unidade='" + cmbSetor.Text + "'";
                stConsulta = "SELECT Cod_rim FROM rim ORDER BY Cod_rim DESC LIMIT 1";
                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            myReader.Read();
                            
                            txtCodigo.Text = myReader["Cod_rim"] + Environment.NewLine;
                            codigoultimari = Convert.ToInt32(txtCodigo.Text);
                            txtCodigo.Text = codigoultimari.ToString();
                        }

                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: "+ex.Message );
            }
                 
        }

        private void capturaIPServidor()
        {

        }

        private void mostrarResultados()
        {
            txtTotalReal.Text = "";
            textBox4.Text = Global.Logon.ipservidor;

                        
            if (chkPlanilhaDespesas.Checked == false)
            {
                checkBox4.Visible = false;
                chkPrograma.Visible = false;
                chkAcao.Visible = false;
                chkReduzida.Visible = false;
                chkEmpenho.Visible = false;
                chkAF.Visible = false;

                
                txtCheckFornecedor.Enabled = false;
                txtCheckPrograma.Enabled = false;
                txtCheckReduzida.Enabled = false;
                txtCheckAF.Enabled = false;
                txtCheckEmpenho.Enabled = false;
                txtCheckCodigoAplicacao.Enabled = false;

                lblDesdobrada.Visible = false;
                lblProgram.Visible = false;
                lblEmpenho.Visible = false;
                lblCodAplicacao.Visible = false;
                lblRed.Visible = false;
                lblAF.Visible = false;

                txtCheckDesdobrada.Visible = false;
                txtCheckPrograma.Visible = false;
                txtCheckEmpenho.Visible = false;
                txtCheckCodigoAplicacao.Visible = false;
                txtCheckReduzida.Visible = false;
                txtCheckAF.Visible = false;

                               
                
                toolStripStatusMensagem.Text = "módulo de exibição de requisições por filtros";
                lblTotalReal.Visible = false;
                txtTotalReal.Visible = false;
                txtTotalReal.Text = "";
                //textBox4.Text = "";

                txtAno.Text = DateTime.Today.ToString("yyyy");
                txtAnoProcesso.Text = DateTime.Today.ToString("yyyy");


                if (radioButtonRIM.Checked == true)
                {
                    TipoRIM = "RIM";
                }
                else
                {
                    TipoRIM = "RRP";
                }

                
                if (TipoRIM == "RIM"){

                    checkBoxRIM.Checked = true;
                    checkBoxRRP.Checked = false;
                }
                else{
                    checkBoxRIM.Checked = false;
                    checkBoxRRP.Checked = true;
                }

                /*
                //Recupera que está gravado no arquivo Path: "d:\\IPSERVIDOR.txt"
                //StreamReader objReader = new StreamReader("d:\\IPSERVIDOR.txt");
                StreamReader objReader = new StreamReader("c:\\IPSERVIDOR.txt");
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
                */

                try
                {
                    groupBox7.Enabled = true;

                    mDataSet = new DataSet();
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

                    mConn.Open();

                    //cria um adapter utilizando a instrução SQL para acessar a tabela 
                    if (rbPorCodigo.Checked == true)

                        if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                        {
                            //mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Tipo_RIM ='" + TipoRIM + "' ORDER BY Cod_rim", mConn);
                            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Tipo_RIM ='" + TipoRIM + "' ORDER BY Nome_unidade", mConn);


                            if (groupBox2.Visible == true)
                                toolStripStatusMensagem.Text = "";
                            else
                                toolStripStatusMensagem.Text = "requisições ordenadas por 'Código sequencial'";
                            
                        }
                        else
                        {
                            //após cadastro das requisições com retirada dos espaços - confirmar com botão gravar (que também retira espaços)
                            //voltar com as linhas marcadas com @
                            //mAdapter = new MySqlDataAdapter("SELECT * FROM rim ", mConn);
                            
                            
                            //igual a consulta anterior só que para um período definido
                            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE (dataCetilSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd")
                               + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "')" + "AND Tipo_RIM='" + TipoRIM + "'ORDER BY Cod_rim", mConn);
                            toolStripStatusMensagem.Text = "requisições ordenadas por 'Código sequêncial' no período selecionado";                             
                          
                            /*
                            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE (dataCetil BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("dd/MM/YYYY")
                            + "' AND '" + Convert.ToDateTime(txtDataFinal.Text.ToString("dd/MM/yyyy") + "')" + " AND Tipo_RIM='" + TipoRIM + "' ORDER BY Cod_rim", mConn);
                            toolStripStatusMensagem.Text = "requisições ordenadas por 'Código sequêncial' no período selecionado";
                            */
 
                            if (groupBox2.Visible == true)
                                toolStripStatusMensagem.Text = "";
                            else
                                toolStripStatusMensagem.Text = "requisições ordenadas por 'Código sequencial'";
                            

                        }

                    else

                        if (rbPorNomeUnidade.Checked == true)
                            if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                            {
                                mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Tipo_RIM ='" + TipoRIM + "' ORDER BY Nome_unidade", mConn);

                                if (groupBox2.Visible == true)
                                    toolStripStatusMensagem.Text = "";
                                else
                                    toolStripStatusMensagem.Text = "requisições ordenadas por 'Nome da Unidade'";
                            }
                            else
                            {
                                mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE (dataCetilSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd")
                               + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "')" + "AND Tipo_RIM='" + TipoRIM + "' ORDER BY Nome_unidade", mConn);

                                if (groupBox2.Visible == true)
                                    toolStripStatusMensagem.Text = "";
                                else
                                    toolStripStatusMensagem.Text = "requisições ordenadas por 'Nome da Unidade' no período selecionado";
                            }

                        else

                            if (rbProcesso.Checked == true)
                                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                                {
                                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Tipo_RIM ='" + TipoRIM + "' ORDER BY Processo", mConn);

                                    if (groupBox2.Visible == true)
                                        toolStripStatusMensagem.Text = "";
                                    else

                                        toolStripStatusMensagem.Text = "requisições ordenadas por 'Processo'";
                                }
                                else
                                {
                                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE (dataCetilSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd")
                                    + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "')" + "AND Tipo_RIM='" + TipoRIM + "' ORDER BY Processo", mConn);

                                    if (groupBox2.Visible == true)
                                        toolStripStatusMensagem.Text = "";
                                    else

                                        toolStripStatusMensagem.Text = "requisições ordenadas por 'Processo' no período selecionado";
                                }
                            else

                                if (rbDescricao.Checked == true)
                                    if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                                    {
                                        mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Tipo_RIM ='" + TipoRIM + "' ORDER BY Descricao", mConn);


                                        if (groupBox2.Visible == true)
                                            toolStripStatusMensagem.Text = "";
                                        else

                                            toolStripStatusMensagem.Text = "requisições ordenadas por 'Descrição do Objeto'";
                                    }
                                    else
                                    {
                                        mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE (dataCetilSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd")
                                        + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "')" + "AND Tipo_RIM='" + TipoRIM + "' ORDER BY Descricao", mConn);


                                        if (groupBox2.Visible == true)
                                            toolStripStatusMensagem.Text = "";
                                        else

                                            toolStripStatusMensagem.Text = "requisições ordenadas por 'Descrição do Objeto' no período selecionado";
                                    }
                                else

                                    if (rbCetil.Checked == true)

                                        if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                                        {
                                            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Tipo_RIM ='" + TipoRIM + "' ORDER BY Cetil", mConn);


                                            if (groupBox2.Visible == true)
                                                toolStripStatusMensagem.Text = "";
                                            else

                                                toolStripStatusMensagem.Text = "requisições ordenadas por 'Cetil'";
                                        }
                                        else
                                        {
                                            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE (dataCetilSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd")
                                            + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "')" + "AND Tipo_RIM='" + TipoRIM + "' ORDER BY Cetil", mConn);


                                            if (groupBox2.Visible == true)
                                                toolStripStatusMensagem.Text = "";
                                            else

                                                toolStripStatusMensagem.Text = "requisições ordenadas por 'Cetil' no período selecionado";
                                        }

                                    else
                                        if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                                        {
                                            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Tipo_RIM ='" + TipoRIM + "' ORDER BY DataCetil", mConn);


                                            if (groupBox2.Visible == true)
                                                toolStripStatusMensagem.Text = "";
                                            else

                                                toolStripStatusMensagem.Text = "requisições ordenadas por 'Data do Cetil'";
                                        }
                                        else
                                        {
                                            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE (dataCetilSql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd")
                                            + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "')" + "AND Tipo_RIM='" + TipoRIM + "' ORDER BY DataCetil", mConn);


                                            if (groupBox2.Visible == true)
                                                toolStripStatusMensagem.Text = "";
                                            else

                                                toolStripStatusMensagem.Text = "requisições ordenadas por 'Data do Cetil' no período selecionado";
                                        }


                    //preenche o dataset através do adapter
                    mAdapter.Fill(mDataSet, "rim");

                    //atribui o resultado à propriedade DataSource da dataGridView
                    dataGridView1.DataSource = mDataSet;
                    dataGridView1.DataMember = "rim";

                    dataGridView1.Columns[0].HeaderText = "Código";
                    dataGridView1.Columns[1].HeaderText = "Unidade";
                    dataGridView1.Columns[2].HeaderText = "Objeto";
                    dataGridView1.Columns[2].Width = 40;
                    dataGridView1.Columns[3].HeaderText = "Dotação";
                    dataGridView1.Columns[4].HeaderText = "Tipo";
                    dataGridView1.Columns[5].HeaderText = "Cetil";
                    dataGridView1.Columns[6].HeaderText = "Data RI";
                    dataGridView1.Columns[7].HeaderText = "Dt RI SQL";
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[8].HeaderText = "R$ Estimado";
                    dataGridView1.Columns[9].HeaderText = "R$ Real";
                    dataGridView1.Columns[10].HeaderText = "Processo";
                    dataGridView1.Columns[11].HeaderText = "Ano/Proc";
                    dataGridView1.Columns[12].HeaderText = "Processo Contábil";
                    dataGridView1.Columns[13].HeaderText = "Ano/Proc.Cont";
                    dataGridView1.Columns[14].HeaderText = "Contabilidade";
                    dataGridView1.Columns[15].HeaderText = "Ordenador/Ass";
                    dataGridView1.Columns[16].HeaderText = "Compras";
                    dataGridView1.Columns[17].HeaderText = "Ordenador Empenho";
                    dataGridView1.Columns[18].HeaderText = "Compras";
                    dataGridView1.Columns[19].HeaderText = "Dipe";
                    dataGridView1.Columns[20].HeaderText = "Cadastrante";
                    dataGridView1.Columns[21].HeaderText = "Data";
                    dataGridView1.Columns[22].HeaderText = "Observação";

                    if (chkCodigo.Checked == false)
                    {
                        dataGridView1.Columns[0].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[0].Visible = true;
                    }

                    if (chkDescricao.Checked == false)
                    {
                        dataGridView1.Columns[2].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[2].Visible = true;
                    }

                    if (chkDotacao.Checked == false)
                    {
                        dataGridView1.Columns[3].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[3].Visible = true;
                    }

                    if (chkCetil.Checked == false)
                    {
                        dataGridView1.Columns[5].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[5].Visible = true;
                    }

                    if (chkVlEstimado.Checked == false)
                    {
                        dataGridView1.Columns[8].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[8].Visible = true;
                    }

                    if (chkValorReal.Checked == false)
                    {
                        dataGridView1.Columns[9].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[9].Visible = true;
                    }

                    if (chkProc.Checked == false)
                    {
                        dataGridView1.Columns[10].Visible = false;
                        dataGridView1.Columns[11].Visible = false;
                        
                    }
                    else
                    {
                        dataGridView1.Columns[10].Visible = true;
                        dataGridView1.Columns[11].Visible = true;
                    }

                    if (chkProcessoContabil.Checked == false)
                    {
                        dataGridView1.Columns[12].Visible = false;
                        dataGridView1.Columns[13].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[12].Visible = true;
                        dataGridView1.Columns[13].Visible = true;
                    }
                    
                    if (chkTramite.Checked == false)
                    {
                        dataGridView1.Columns[14].Visible = false;
                        dataGridView1.Columns[15].Visible = false;
                        dataGridView1.Columns[16].Visible = false;
                        dataGridView1.Columns[17].Visible = false;
                        dataGridView1.Columns[18].Visible = false;
                        dataGridView1.Columns[19].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[14].Visible = true;
                        dataGridView1.Columns[15].Visible = true;
                        dataGridView1.Columns[16].Visible = true;
                        dataGridView1.Columns[17].Visible = true;
                        dataGridView1.Columns[18].Visible = true;
                        dataGridView1.Columns[19].Visible = true;
                    }

                    if (chkCadastrante.Checked == false)
                    {
                        dataGridView1.Columns[20].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[20].Visible = true;
                    }


                    if (chkDataCadastro.Checked == false)
                    {
                        dataGridView1.Columns[21].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[21].Visible = true;
                    }


                    if (chkObs.Checked == false)
                    {
                        dataGridView1.Columns[22].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[22].Visible = true;

                    }

                    dataGridView1.Columns[23].Visible = false;
                    dataGridView1.Columns[24].Visible = false;
                    dataGridView1.Columns[25].Visible = false;
                    dataGridView1.Columns[26].Visible = false;



                    calculaQuantidadeRegistros();

                }
                catch
                {
                    // MessageBox.Show("Não foi possível fazer conexão. Erro IP.");
                }


            }
            else  // Se checkbox 'planilha de despesas tiver marcado'
            {
                checkBox4.Visible = true;
                chkPrograma.Visible = true;
                chkAcao.Visible = true;
                chkReduzida.Visible = true;
                chkEmpenho.Visible = true;
                chkAF.Visible = true;
                

                toolStripStatusMensagem.Text = "módulo de exibição da Planilha de Despesas por filtros";
                lblTotalReal.Visible = false;
                txtTotalReal.Visible = false;
                txtTotalReal.Text = "";
                //textBox4.Text = "";

                txtAno.Text = DateTime.Today.ToString("yyyy");
                txtAnoProcesso.Text = DateTime.Today.ToString("yyyy");


                if (radioButtonRIM.Checked == true)
                {
                    TipoRIM = "RIM";
                }
                else
                {
                    TipoRIM = "RRP";
                }

                
                txtCheckFornecedor.Enabled = true;
                txtCheckPrograma.Enabled = true;
                txtCheckReduzida.Enabled = true;
                txtCheckAF.Enabled = true;
                txtCheckEmpenho.Enabled = true;
                txtCheckCodigoAplicacao.Enabled = true;

                lblDesdobrada.Visible = true;
                lblProgram.Visible = true;
                lblEmpenho.Visible = true;
                lblCodAplicacao.Visible = true;
                lblRed.Visible = true;
                lblAF.Visible = true;

                txtCheckDesdobrada.Visible = true;
                txtCheckPrograma.Visible = true;
                txtCheckEmpenho.Visible = true;
                txtCheckCodigoAplicacao.Visible = true;
                txtCheckReduzida.Visible = true;
                txtCheckAF.Visible = true;

                try
                {
                    groupBox7.Enabled = true;

                    mDataSet = new DataSet();
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    //mConn = new MySqlConnection("Persist Security Info=False; server=127.0.0.1; database=prorim;uid=root");
                    mConn.Open();

                    //cria um adapter utilizando a instrução SQL para acessar a tabela 
                    if (rbPorCodigo.Checked == true)

                        if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                        {
                           // mAdapter = new MySqlDataAdapter("Select * FROM planilha_despesa WHERE Tipo_RIM='" + TipoRIM + "' ORDER BY Cod_rim", mConn);
                            mAdapter = new MySqlDataAdapter("Select * FROM planilha_despesa ORDER BY Cod_rim", mConn);

                            if (groupBox2.Visible == true)
                                toolStripStatusMensagem.Text = "";
                            else
                                toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Código sequencial'";

                        }
                        else
                        {
                            //igual a consulta anterior só que para um período definido
                            //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "')" + " AND Tipo_RIM='" + TipoRIM + "' ORDER BY Cod_rim", mConn);
                            mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ORDER BY Cod_rim", mConn);


                            if (groupBox2.Visible == true)
                                toolStripStatusMensagem.Text = "";
                            else
                                toolStripStatusMensagem.Text = "requisições ordenadas por 'Código sequêncial' no período selecionado";
                        }

                    else
                        if (rbPorNomeUnidade.Checked == true)
                            if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                            {
                                //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Tipo_RIm='" + TipoRIM + "' ORDER BY Nome_unidade", mConn);
                                mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa ORDER BY Nome_unidade", mConn);


                                if (groupBox2.Visible == true)
                                    toolStripStatusMensagem.Text = "";
                                else
                                    toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Unidade'";

                            }
                            else
                            {
                                //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "')" + " AND Tipo_RIM='" + TipoRIM + "' ORDER BY Nome_unidade", mConn);
                                mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ORDER BY Nome_unidade", mConn);

                                if (groupBox2.Visible == true)
                                    toolStripStatusMensagem.Text = "";
                                else
                                    toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Unidade' no período selecionado";
                            }

                        else

                            if (rbProcesso.Checked == true)
                                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                                {
                                   // mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Tipo_RIm='" + TipoRIM + "' ORDER BY Processo", mConn);
                                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa ORDER BY Processo", mConn);


                                    if (groupBox2.Visible == true)
                                        toolStripStatusMensagem.Text = "";
                                    else

                                        toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Processo'";
                                }
                                else
                                {
                                   //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "')" + " AND Tipo_RIM='" + TipoRIM + "' ORDER BY Processo", mConn);
                                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ORDER BY Processo", mConn);

                                    if (groupBox2.Visible == true)
                                        toolStripStatusMensagem.Text = "";
                                    else

                                        toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Processo' no período selecionado";
                                }
                            else

                                if (rbDescricao.Checked == true)
                                    if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                                    {
                                        //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Tipo_RIM ='" + TipoRIM + "' ORDER BY Descricao", mConn);
                                        mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa ORDER BY Descricao", mConn);

                                        if (groupBox2.Visible == true)
                                            toolStripStatusMensagem.Text = "";
                                        else

                                            toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Descrição do Objeto'";
                                    }
                                    else
                                    {
                                        //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "')" + " AND Tipo_RIM='" + TipoRIM + "' ORDER BY Descricao", mConn);
                                        mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ORDER BY Descricao", mConn);


                                        if (groupBox2.Visible == true)
                                            toolStripStatusMensagem.Text = "";
                                        else

                                            toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Descrição do Objeto' no período selecionado";
                                    }
                                else

                                    if (rbCetil.Checked == true)

                                        if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                                        {
                                            //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Tipo_RIM ='" + TipoRIM + "' ORDER BY Cetil", mConn);
                                            mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa ORDER BY Cetil", mConn);


                                            if (groupBox2.Visible == true)
                                                toolStripStatusMensagem.Text = "";
                                            else

                                                toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Cetil'";
                                        }
                                        else
                                        {
                                            //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "')" + " AND Tipo_RIM='" + TipoRIM + "' ORDER BY Cetil", mConn);
                                            mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ORDER BY Cetil", mConn);


                                            if (groupBox2.Visible == true)
                                                toolStripStatusMensagem.Text = "";
                                            else

                                                toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Cetil' no período selecionado";
                                        }

                                    else
                                        if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                                        {
                                            //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Tipo_RIM ='" + TipoRIM + "' ORDER BY DataCetil", mConn);
                                            mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa ORDER BY DataCetil", mConn);


                                            if (groupBox2.Visible == true)
                                                toolStripStatusMensagem.Text = "";
                                            else

                                                toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Data do Cetil'";
                                        }
                                        else
                                        {
                                           //mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "')" + " AND Tipo_RIM='" + TipoRIM + "' ORDER BY DataCetil", mConn);
                                            mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ORDER BY DataCetil", mConn);


                                            if (groupBox2.Visible == true)
                                                toolStripStatusMensagem.Text = "";
                                            else

                                                toolStripStatusMensagem.Text = "Planilha de Despesas ordenada por 'Data do Cetil' no período selecionado";
                                        }


                    //preenche o dataset através do adapter
                    mAdapter.Fill(mDataSet, "planilha_despesa");

                    //atribui o resultado à propriedade DataSource da dataGridView
                    dataGridView1.DataSource = mDataSet;
                    dataGridView1.DataMember = "planilha_despesa";

                    /*
                    dataGridView1.Columns[0].HeaderText = "Cód.Seq.Despesa";
                    dataGridView1.Columns[1].HeaderText = "Despesa";
                    dataGridView1.Columns[2].HeaderText = "Reduzida";
                    dataGridView1.Columns[3].HeaderText = "Programa";
                    dataGridView1.Columns[4].HeaderText = "Cod.Aplic";
                    dataGridView1.Columns[5].HeaderText = "Cod.Seq.RI";
                    dataGridView1.Columns[6].HeaderText = "Unidade";
                    dataGridView1.Columns[7].HeaderText = "Objeto";
                    dataGridView1.Columns[9].HeaderText = "Tipo";
                    dataGridView1.Columns[10].HeaderText = "Cetil";
                    dataGridView1.Columns[11].HeaderText = "Data Cetil";
                    dataGridView1.Columns[12].HeaderText = "R$ Estimado";
                    dataGridView1.Columns[13].HeaderText = "R$ Real";
                    dataGridView1.Columns[14].HeaderText = "Processo";
                    dataGridView1.Columns[15].HeaderText = "Ano";                   
                    dataGridView1.Columns[16].HeaderText = "Contábil";
                    dataGridView1.Columns[17].HeaderText = "Ano";
                    dataGridView1.Columns[18].HeaderText = "Empenho";
                    dataGridView1.Columns[19].HeaderText = "Data Empenho";
                    dataGridView1.Columns[20].HeaderText = "Valor";
                    dataGridView1.Columns[21].HeaderText = "AF";
                    dataGridView1.Columns[22].HeaderText = "Data";
                    dataGridView1.Columns[23].HeaderText = "Valor";
                    */

                    dataGridView1.Columns[0].HeaderText = "Cód.Seq.Despesa";
                    dataGridView1.Columns[1].HeaderText = "Despesa";
                    dataGridView1.Columns[2].HeaderText = "Reduzida";
                    dataGridView1.Columns[3].HeaderText = "Programa";
                    dataGridView1.Columns[4].HeaderText = "Cod.Aplic";
                    dataGridView1.Columns[5].HeaderText = "Cod.Seq.RI";
                    dataGridView1.Columns[6].HeaderText = "Unidade";
                    dataGridView1.Columns[7].HeaderText = "Objeto";
                    dataGridView1.Columns[8].HeaderText = "Dotação";
                    dataGridView1.Columns[9].HeaderText = "Tipo";
                    //Linha repetida 'dotação', já temos a mesma informação em 'despesa'
                    //dataGridView1.Columns[9].HeaderText = "Tipo"; 
                    dataGridView1.Columns[10].HeaderText = "Cetil";
                    dataGridView1.Columns[11].HeaderText = "Data Cetil";
                    dataGridView1.Columns[12].HeaderText = "Data Cetil SQL";
                    dataGridView1.Columns[13].HeaderText = "Processo";
                    dataGridView1.Columns[14].HeaderText = "R$ Estimado";
                    dataGridView1.Columns[15].HeaderText = "R$ Real";
                    dataGridView1.Columns[16].HeaderText = "Contábil";
                    dataGridView1.Columns[17].HeaderText = "Empenho";
                    dataGridView1.Columns[18].HeaderText = "Data Empenho";
                    dataGridView1.Columns[19].HeaderText = "Valor";
                    dataGridView1.Columns[20].HeaderText = "AF";
                    dataGridView1.Columns[21].HeaderText = "Data";
                    dataGridView1.Columns[22].HeaderText = "Valor";

                    
                    if (chkCodigo.Checked == false)
                    {
                        dataGridView1.Columns[0].Visible = false;   }
                    else
                    {
                        dataGridView1.Columns[0].Visible = true;
                        
                    }

                    
                    if (chkDotacao.Checked == false)
                    {
                        dataGridView1.Columns[1].Visible = false;
                        dataGridView1.Columns[8].Visible = false;

                    }
                    else
                    {
                        dataGridView1.Columns[1].Visible = true;
                        dataGridView1.Columns[8].Visible = false;
                 
                    }
                    
                    
                    if (chkReduzida.Checked == false)
                    {
                        dataGridView1.Columns[2].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[2].Visible = true;
                    }

                    
                    if (chkPrograma.Checked == false)
                    {
                        dataGridView1.Columns[3].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[3].Visible = true;
                        
                    }

                    if (chkAcao.Checked == false)
                    {
                        dataGridView1.Columns[4].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[4].Visible = true;
                       
                    }

                    /*
                    if (chk.Checked == false)
                    {
                        dataGridView1.Columns[5].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[5].Visible = true;
                        dataGridView1.Columns[5].HeaderText = "Cod.Seq.Cetil";

                    }
                    
                    if (chkAcao.Checked == false)
                    {
                        dataGridView1.Columns[6].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[6].Visible = true;
                        dataGridView1.Columns[6].HeaderText = "Unidade";

                    }
                     */

                    if (chkDescricao.Checked == false)
                    {
                        dataGridView1.Columns[7].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[7].Visible = true;
                        
                    }
                                      

                    if (chkCetil.Checked == false)
                    {
                        dataGridView1.Columns[10].Visible = false;
                        dataGridView1.Columns[11].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[10].Visible = true;
                        dataGridView1.Columns[10].Visible = true;
                                               
                    }

                                        
                    if (chkVlEstimado.Checked == true){

                        dataGridView1.Columns[12].Visible = true;
                        
                    }
                    else
                    {
                        dataGridView1.Columns[12].Visible = false;
                    }

                    
                    if (chkValorReal.Checked == true)
                    {
                        dataGridView1.Columns[13].Visible = true;
                        
                    }
                    else
                    {
                        dataGridView1.Columns[13].Visible = false;                    
                    }  


                    if (chkProc.Checked == true)
                    {
                        dataGridView1.Columns[14].Visible = true;
                        
                    }
                    else
                    {
                        dataGridView1.Columns[14].Visible = false;
                        
                    }


                    if (chkProcessoContabil.Checked == true)
                    {
                        dataGridView1.Columns[15].Visible = true;
                        
                    }
                    else
                    {
                        dataGridView1.Columns[15].Visible = false;
                        
                    }

                     
                    if (chkEmpenho.Checked == true)
                    {
                        dataGridView1.Columns[16].Visible = true;
                        dataGridView1.Columns[17].Visible = true;
                        dataGridView1.Columns[18].Visible = true;      
                        
                    }
                    else
                    {
                        dataGridView1.Columns[16].Visible = false;
                        dataGridView1.Columns[17].Visible = false;
                        dataGridView1.Columns[18].Visible = false;                                    
                     
                    }

                    if (chkAF.Checked == true)
                    {
                        dataGridView1.Columns[19].Visible = true;
                        dataGridView1.Columns[20].Visible = true;
                        dataGridView1.Columns[21].Visible = true;
                                                
                    }
                    else
                    {
                        dataGridView1.Columns[19].Visible = false;
                        dataGridView1.Columns[20].Visible = false;
                        dataGridView1.Columns[21].Visible = false;

                    }
                                        
                    mConn.Close();
                    
                    calculaQuantidadeRegistros();

                }
                     
                catch
                {
                    MessageBox.Show("SELECT * FROM planilha_despesa WHERE (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "')"  + " AND Tipo_RIM='" + TipoRIM + "' ORDER BY Cod_rim");
                }
                    
                }

                                
        }

        private void calculaQuantidadeRegistros()
        {
            if ((dataGridView1.RowCount) == 1 || (dataGridView1.RowCount) == 0)
                label28.Text = (dataGridView1.RowCount.ToString())+ " registro";
            else
                label28.Text = (dataGridView1.RowCount.ToString()) + " registros";

        }
       
        
        private void HabilitaRadionButtons()
        {
            rbNovo.Enabled = true;
            rbAlterar.Enabled = true;
            rbExclui.Enabled = true;
        }

        private void DesabilitaTextBox()
        {
            cmbEscolha.Enabled = false;
            txtdescricao.Enabled = false;
            txtDO.Enabled = false;

            chkRIM.Enabled = false;
            chkRRP.Enabled = false;

            txtCetil.Enabled = false;
            txtdataCetil.Enabled = false;
            txtvalorEstimado.Enabled = false;
            txtvalorReal.Enabled = false;
            txtProcesso.Enabled = false;
            //txtAutorizacao.Enabled = false;
            //txtDataAutorizacao.Enabled = false;
            //cmbSetor.Enabled = false;
            //txtdataEnvio.Enabled = false;
            lblDataContabilidade.Enabled = false;
            lblDataOrdenador1.Enabled = false;
            lblDataCompras1.Enabled = false;
            lblDataOrdenador2.Enabled = false;
            lblDataCompras2.Enabled = false;
            lblDataDipe.Enabled = false;
            cmbcadastradoPor.Enabled = false;
            txtdtCadastro.Enabled = false;
            cmbFornecedor.Enabled = false;
            //txtnotaFiscal.Enabled = false;
            //txtdataNotaFiscal.Enabled = false;
            txtObs.Enabled = false;
            //txtEmpenho.Enabled = false;
        }

        private void LimpaCampos()
        {

            txtCodigo.Text = "";
            cmbEscolha.Text = "";
            txtdescricao.Text = "";
            txtDO.Text = "";
            
            chkRIM.Checked = false;
            chkRRP.Checked = false;

            //radioButtonRIM.Checked = false;
            //radioButtonRRP.Checked = false;
            
            txtCetil.Text = "";
            txtdataCetil.Text = "";
            txtvalorEstimado.Text = "";
            txtvalorReal.Text = "";
            txtProcesso.Text = "";
            txtProcessoContabil.Text = "";
            //txtAutorizacao.Text = "";
            //txtDataAutorizacao.Text = "";
            //cmbSetor.Text = "";
            //txtdataEnvio.Text = "";

            lblDataContabilidade.Text = "";
            lblDataOrdenador1.Text = "";
            lblDataCompras1.Text = "";
            lblDataOrdenador2.Text = "";
            lblDataCompras2.Text = "";
            lblDataDipe.Text = "";
            
            cmbcadastradoPor.Text = "";
            txtdtCadastro.Text = "";
            cmbFornecedor.Text = "";
            //txtnotaFiscal.Text = "";
            //txtdataNotaFiscal.Text = "";
            txtObs.Text = "";
            txtCodigoDespesa.Text = "";
            txtReduzida.Text = "";
            txtPrograma.Text = "";
            txtAcao.Text = "";

            txtCodFornecedor.Text = "";
            txtCodUnidade.Text = "";
            txtCodUsuario.Text = "";  

        }

        private void rbNovo_CheckedChanged(object sender, EventArgs e)
        {
            lblInformacao.Visible = false;

            groupBox10.Visible = false;

            if (bt_Cancelar.Visible == true)
                bt_Cancelar.Visible = false;
            else


            //lblMsg.Text = "Módulo de inclusão de dados atividado.";
            toolStripStatusMensagem.Text = "módulo de inclusão de dados ativado. Os campos com '*' são de preenchimento obrigatório";

            lblModuloVisualização.Visible = false;
            dataGridView1.Visible = false;
            btnCancelar.Visible = true;
            
            label28.Visible = false;
            groupBox3.Visible = false;
            GroupBox1.Visible = true; 
            groupBox2.Visible = true;
            bt_Cancelar.Visible = false;
            GroupBox1.Enabled = true;
            groupBox4.Visible = false;
            
            LimpaCampos();
            txtCetil.Focus();
            //cmbEscolha.Focus();
            analisaRadioButton();

            //DesabilitaRadioButtons();
            bt_Gravar.Enabled = true;
            
            cmbcadastradoPor.Text = Global.Logon.usuario;
               
        }

        private void analisaRadioButton()
        {
            if (rbNovo.Checked == true)
            {
                rbAlterar.Enabled = false;
                rbExclui.Enabled = false;
            }
            else
            {
                if (rbAlterar.Enabled == true)
                {
                    rbNovo.Enabled = false;
                    rbExclui.Enabled = false;
                }
                else
                {
                    rbNovo.Enabled = false;
                    rbAlterar.Enabled = false;
                }

            }
        }

        private void DesabilitaRadioButtons()
        {
            rbNovo.Enabled = false;
            rbAlterar.Enabled = false;
            rbExclui.Enabled = false;
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
   
            if (Global.despesa.flag_valor_real == "1")
            {
                MessageBox.Show("A requisição foi alterada. Grave a alteração antes de sair.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
            }
            else
            {
                Global.despesa.flag_valor_real = "0";
                this.Close();
            }


        }

        private void rbAlterar_CheckedChanged(object sender, EventArgs e)
        {
            groupBox10.Visible = true;
                        
            //lblMsg.Text = "Módulo de alteração ativado.";
            toolStripStatusMensagem.Text = "módulo de atualização ativado... aguardando seleção de item.";
            bt_Gravar.Enabled = false;
            bt_Cancelar.Visible = true;

            bt_Cancelar.Enabled = true;

            lblModuloVisualização.Visible = false;
            
            analisaRadioButton();
            GroupBox1.Enabled = true;
            //WebBrowser1.Visible = true;
            //bt_Gravar.Enabled = true;
            btnEmpenho.Enabled = true;

            //btnAtualizar.Enabled = false;
            if (rbAlterar.Checked == true)
            {
                lblInformacao.Visible = true;
                lblInformacao.Text = "Localize na planilha abaixo a requisição para alteração e dê duplo clique na linha correspondente para abri-la";
           
                dataGridView1.Visible = true;
                btnCancelar.Visible = false;
                dataGridView1.Enabled = true;
                label28.Visible = true;
                txtdataCetil.Enabled = false;
                txtCetil.Enabled = false;
                //txtDO.Enabled = false;
                dataGridView1.Focus();
                    
            }
            else
            {
                txtdataCetil.Enabled = true;
                txtCetil.Enabled = true;
                txtDO.Enabled = true;


            }

        }

        private void FiltrandoComOR(int codigo, string temp_1, string temp_2)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            //if (checkBox1.Checked == true)
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM rim Where Cod_rim=" + codigo + " OR Nome_Unidade" + " LIKE '%" + temp + "%'" + "OR Cetil=" + "'" + temp_2 + "'", mConn);
            // = new MySqlDataAdapter("SELECT * FROM rim Where Cod_rim=" + codigo + " AND Nome_Unidade=" + "'" + temp_1 + "'" + "AND Cetil=" + "'" + temp_2 + "'", mConn);
            //+ "'" + temp_1 + "'" +

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "rim");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "rim";

        }

        private void bt_visualizar_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

            if (estadocodigo == 0)
            {
                txtCheckCodigo.Visible = false;
                estadocodigo = 1;
            }
            else
            {
                txtCheckCodigo.Visible = true;
                txtCheckCodigo.Focus();
                estadocodigo = 0;
            }

        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {

            if (estadoident == 0)
            {
                txtCheckIdentificação.Visible = false;
                estadoident = 1;
            }
            else
            {
                txtCheckIdentificação.Visible = true;
                txtCheckIdentificação.Focus();
                estadoident = 0;
            }
        }

        private void bt_Gravar_Click(object sender, EventArgs e)
        {                              
            //Verificando se os campos obrigatórios (destacados em Azul) estão todos preeenchidos, senão informa os campos
            // que faltam e ao final estando todos preenchidos acessamos o método Gravar()  
         
            txtVerificaVeiculo.Text = Global.Veiculos.quantPlaca;

            if (txtAno.Text == "")
                txtAno.Text = DateTime.Today.ToString("yyyy"); else { }
            if (txtAnoProcesso.Text == "")
                txtAnoProcesso.Text = DateTime.Today.ToString("yyyy"); else { }
            
            if (rbNovo.Checked == true)
            {
                toolStripStatusMensagem.Text = "módulo INCLUSÃO ativado. Os campos marcados com '*' são de preencimento obrigatório";

                if (cmbEscolha.Text == "")
                {
                    toolStripStatusMensagem.Text = "informe a UNIDADE/SETOR relacionada à contratação de serviço ou aquisição do produto";
                    cmbEscolha.Focus();
                    
                }
                else
                {
                    if (txtCetil.Text == "")
                    {
                        toolStripStatusMensagem.Text = "informe o NÚMERO da requisição";
                        txtCetil.Focus();

                    }
                    else
                    {
                        if (txtdataCetil.Text == "")
                        {
                            toolStripStatusMensagem.Text = "informe a DATA da requisição";
                            txtdataCetil.Focus();
                        }
                        else
                        {
                            if (txtDO.Text == "")
                            {
                                DialogResult result;
                                result = MessageBox.Show("A Requisição está sem D.O. Você quer gravá-la como uma Requisição de Necessidades?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (result == System.Windows.Forms.DialogResult.Yes)
                                {
                                    txtDO.Text = "R.N."; 
                                }
                                else
                                {
                                    toolStripStatusMensagem.Text = "informe pelo menos uma DESPESA vinculada à requisição";
                                    txtDO.Focus();
                                }
                            }
                            else
                            {

                                if (cmbcadastradoPor.Text == "")
                                {
                                    toolStripStatusMensagem.Text = "informe o USUÁRIO responsável pelo cadastro da requisição";
                                    cmbcadastradoPor.Focus();
                                }
                                else
                                {
                                    if (txtCodUsuario.Text == "")
                                    {
                                        toolStripStatusMensagem.Text = "confirme o USUÁRIO responsável pelo cadastro da requisição";
                                        cmbcadastradoPor.Focus();
                                    }
                                    else
                                    {
                                        if (txtvalorEstimado.Text == "")
                                        {
                                            toolStripStatusMensagem.Text = "informe um valor estimado para requisição";
                                            cmbcadastradoPor.Focus();
                                        }
                                        else
                                        {

                                            if (txtVerificaVeiculo.Text == "0 registro")
                                            {
                                                MessageBox.Show("É obrigatório vincular algum veículo à requisição ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //toolStripStatusMensagem.Text = "vincular algum veículo à requisição";
                                                cmbPlaca.Focus();
                                            }
                                            else
                                            {                                                
                                                Gravar();
                                                cmbFornecedor.Items.Clear();
                                                
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            else // else do if que analisa se o rbNovo.Checked==false
            {
                if (txtCodigo.Text == "")
                {
                    toolStripStatusMensagem.Text = "Escolha na planilha a requisição a ser alterada ou excluída";
                }
                else
                {
                    codigoultimari = Convert.ToInt32(txtCodigo.Text);

                    if (rbAlterar.Checked == true)
                    {
                        //
                        if (cmbEscolha.Text == "") {

                            toolStripStatusMensagem.Text = "Informe a UNIDADE/SETOR solicitante";
                            MessageBox.Show("informe a UNIDADE/SETOR relacionada à contratação de serviço ou aquisição do produto ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                      
                            cmbEscolha.Focus();

                        }else{
                        
                            Alterar(codigoultimari);
                            toolStripStatusMensagem.Text = "módulo ALTERAÇÃO ativado";
                        
                        }

                    }
                    else
                    {
                        Excluir(codigoultimari);
                        toolStripStatusMensagem.Text = "módulo EXCLUSÃO ativado";
                    }
                }
            }
        }

        private void analisaSeHaVeiculoVinculadoRI()
        {
            MessageBox.Show("Analisando se há veiculo vinculado à RI.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void analisaTrâmite()
        {
            string message = "Você quer atualizar trâmite da RI?";
            string caption = "Pergunta";
            MessageBoxButtons botoes = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, botoes);

            if (groupBox2.Enabled == false)
            {
                if (result == System.Windows.Forms.DialogResult.No)
                Gravar();
            }
            else
            {
                groupBox2.Enabled = true;
            }

        }

        private void Gravar()
        {
            // Início da Conexão com indicação de qual o servidor, nome de base de dados a utilizar

            /* É aconselhável criar um utilizador com password. Para acrescentar a password é somente
               necessário acrescentar o seguinte código a seguir ao uid=root;password= */

            retiraEspaços();

            if (radioButtonRIM.Checked == true)
            {
                TipoRIM = "RIM";
            }
            else
            {
                TipoRIM = "RRP";
            }


            //if (cmbFornecedor.Text != "" || cmbcadastradoPor.Text != "" || cmbSetor.Text != "")  
            
            //{

            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

                // Abre a conexão
                mConn.Open();

                try
                {

                    //Query SQL
                    /*
                    MySqlCommand command = new MySqlCommand("INSERT INTO rim (Nome_Unidade,Descricao,Dotacao,Tipo_RIM,Cetil,DataCetil,DataCetilSQL,ValorEstimado,ValorReal,Processo,ProcessoContabil,Autorizacao,DataAF,SetorEnviado,DataEnvio,Contabilidade,OrdenadorAss,ComprasPrim,OrdenadorEmpenho,ComprasSeg,Dipe,Cadastrante,DataCadastro,Nome_fornecedor,Num_NotaFiscal,Data_NotaFiscal,Observacao,Empenho,Data_Empenho,CodReduzida,Programa,Acao,Cd_Usuario,CD_unidade,Cd_fornecedor)"
                    + "VALUES('" + cmbEscolha.Text + "','" + txtdescricao.Text + "','" + txtDO.Text + "','" + TipoRIM + "','" + txtCetil.Text + "','" + txtdataCetil.Text +"','" 
                    + txtDataCetilSQL.Text + "','" + txtvalorEstimado.Text + "','" + txtvalorReal.Text + "','" + txtProcesso.Text + "','" + txtProcessoContabil.Text + "','"
                    + txtAutorizacao.Text + "','" + txtDataAutorizacao.Text + "','" + cmbSetor.Text + "','"+ txtdataEnvio.Text + "','" + lblDataContabilidade.Text + "','" 
                    + lblDataOrdenador1.Text + "','" + lblDataCompras1.Text + "','" + lblDataOrdenador2.Text + "','" + lblDataCompras2.Text + "','" + lblDataDipe.Text + "','" 
                    + cmbcadastradoPor.Text + "','" + txtdtCadastro.Text + "','" + cmbFornecedor.Text + "','" + txtnotaFiscal.Text + "','" + txtdataNotaFiscal.Text + "','" 
                    + txtObs.Text + "','" + txtEmpenho.Text + "','" + txtDataEmpenho.Text + "','" + txtReduzida.Text + "','" + txtPrograma.Text + "','" 
                    + txtAcao.Text + "','" + txtCodUsuario.Text + "','" + txtCodUnidade.Text + "','" + txtCodFornecedor.Text + "')", mConn);
                    */

                    if (txtvalorReal.Text == "")
                    {
                        txtvalorReal.Text = "0.00";
                    }
                    else
                    {
                    }

                    if (radioButtonRRP.Checked == true)
                    {

                        MySqlCommand command = new MySqlCommand("INSERT INTO rim (Nome_Unidade,Descricao,Dotacao,Tipo_RIM,Cetil,DataCetil,DataCetilSQL,ValorEstimado,ValorReal,Processo,ano_processo,ProcessoContabil,ano_processo_contabil,Contabilidade,OrdenadorAss,ComprasPrim,OrdenadorEmpenho,ComprasSeg,Dipe,Cadastrante,DataCadastro,Observacao,Cd_Usuario,CD_unidade)"
                        + "VALUES('" + cmbEscolha.Text + "','"
                        + txtdescricao.Text + "','" + txtDO.Text + "','" + TipoRIM.Trim() + "','"
                        + txtCetil.Text + "','" + txtdataCetil.Text + "','" + Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd") + "','"
                        + Convert.ToDecimal(txtvalorEstimado.Text) + "','" + Convert.ToDecimal(txtvalorReal.Text) + "','" + txtProcesso.Text + "','"
                        + txtAnoProcesso.Text + "','" + txtProcessoContabil.Text + "','" + txtAnoProcessoContabil.Text + "','"
                        + lblDataContabilidade.Text + "','" + lblDataOrdenador1.Text + "','" + lblDataCompras1.Text + "','"
                        + lblDataOrdenador2.Text + "','" + lblDataCompras2.Text + "','" + lblDataDipe.Text + "','" + cmbcadastradoPor.Text + "','"
                        + txtdtCadastro.Text + "','" + txtObs.Text + "'," + txtCodUsuario.Text + "," + txtCodUnidade.Text + ")", mConn);

                        //Executa a Query SQL
                        command.ExecuteNonQuery();
                        // Antes de fechar a conexão. captura o codigo sequencial da RI (atraves da variável GLOBAL e mais abaixo abre a conexão
                        // com a tabela rim_tem_dotacao e atualiza pra essa RI (Cetil) específica o campo (atributo) 'Cod_rim'

                    }
                    else {

                        txtAnoProcessoContabil.Text = "";
                        MySqlCommand command = new MySqlCommand("INSERT INTO rim (Nome_Unidade,Descricao,Dotacao,Tipo_RIM,Cetil,DataCetil,DataCetilSQL,ValorEstimado,ValorReal,Processo,ano_processo,ProcessoContabil,ano_processo_contabil,Contabilidade,OrdenadorAss,ComprasPrim,OrdenadorEmpenho,ComprasSeg,Dipe,Cadastrante,DataCadastro,Observacao,Cd_Usuario,CD_unidade)"
                        + "VALUES('" + cmbEscolha.Text + "','"
                        + txtdescricao.Text + "','" + txtDO.Text + "','" + TipoRIM.Trim() + "','"
                        + txtCetil.Text + "','" + txtdataCetil.Text + "','" + Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd") + "','"
                        + Convert.ToDecimal(txtvalorEstimado.Text) + "','" + Convert.ToDecimal(txtvalorReal.Text) + "','" + txtProcesso.Text + "','"
                        + txtAnoProcesso.Text + "','" + txtProcessoContabil.Text + "','" + txtAnoProcessoContabil.Text + "','"
                        + lblDataContabilidade.Text + "','" + lblDataOrdenador1.Text + "','" + lblDataCompras1.Text + "','"
                        + lblDataOrdenador2.Text + "','" + lblDataCompras2.Text + "','" + lblDataDipe.Text + "','" + cmbcadastradoPor.Text + "','"
                        + txtdtCadastro.Text + "','" + txtObs.Text + "'," + txtCodUsuario.Text + "," + txtCodUnidade.Text + ")", mConn);

                        //Executa a Query SQL
                        command.ExecuteNonQuery();
                        // Antes de fechar a conexão. captura o codigo sequencial da RI (atraves da variável GLOBAL e mais abaixo abre a conexão
                        // com a tabela rim_tem_dotacao e atualiza pra essa RI (Cetil) específica o campo (atributo) 'Cod_rim'

                                            
                    }

                    calculaCodigo();
                    Global.RI.cetil = txtCodigo.Text;

                    // Fecha a conexão
                    mConn.Close();

                    //Mensagem de Sucesso 
                    MessageBox.Show( "requisição " + txtCetil.Text + " Gravada com Sucesso! " + " [índice: " + Global.RI.cetil + "]", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"

                    //mostrarResultados();

                    //------- Método para atualizar a tabela ref. despesas vinculadas a determinada RI / Update da tabela 'rim_has_dotacao' ----------

                    atualizaRim_has_dotacao();

                    if (radioButtonVeiculo.Checked == true)
                        //gravaRim_has_veiculo();
                        atualizarim_has_veiculo();
                    else


                        //--------------------------------------------------------------------------------------------------------------------------------

                    LimpaCampos();
                    DesabilitaTextBox();
                    HabilitaRadionButtons();
                   
                    btnCancelar.Visible = false;
                    
                    label28.Visible = true;
                    toolStripStatusMensagem.Text = "trâmite Desabilitado ";
                    GroupBox1.Enabled = false;
                    DesabilitaCheckBox();
                      
                    this.Close();

                }
                catch 
                {
                    MessageBox.Show("Requisição não foi gravada." + "\n" + "[ " + "INSERT INTO rim (Nome_Unidade,Descricao,Dotacao,Tipo_RIM,Cetil,DataCetil,DataCetilSQL,ValorEstimado,ValorReal,Processo,ano_processo,ProcessoContabil,ano_processo_contabil,Contabilidade,OrdenadorAss,ComprasPrim,OrdenadorEmpenho,ComprasSeg,Dipe,Cadastrante,DataCadastro,Observacao,Cd_Usuario,CD_unidade)"
                    + "VALUES('" + cmbEscolha.Text + "','" 
                    + txtdescricao.Text + "','" + txtDO.Text + "','" + TipoRIM + "','"
                    + txtCetil.Text + "','" + txtdataCetil.Text + "','" + Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd") + "','" 
                    + Convert.ToDecimal(txtvalorEstimado.Text) + "','" + Convert.ToDecimal(txtvalorReal.Text) + "','" + txtProcesso.Text + "','"
                    + txtAnoProcesso.Text + "','" + txtProcessoContabil.Text + "','" + "','" + txtAnoProcessoContabil.Text +"','"
                    + lblDataContabilidade.Text + "','" + lblDataOrdenador1.Text + "','" + lblDataCompras1.Text + "','" 
                    + lblDataOrdenador2.Text + "','" + lblDataCompras2.Text + "','"+ lblDataDipe.Text + "','" + cmbcadastradoPor.Text + "','" 
                    + txtdtCadastro.Text + "','" + txtObs.Text + "'," + txtCodUsuario.Text + "," + txtCodUnidade.Text + ")"  + " ] " , "ATENÇÂO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    
                }
            
        }

        private void atualizarim_has_veiculo()
        {
            try
            {
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

                // Abre a conexão
                mConn.Open();

                //Query SQl
                
                MySqlCommand command = new MySqlCommand("UPDATE rim_has_veiculo SET Cod_rim=" + Global.RI.cetil + " WHERE Cod_rim='" +
                 txtCetil.Text + "'", mConn);

                
                //Executa a Query SQL
                command.ExecuteNonQuery();
                mConn.Close();

                            }
            catch
            {
                MessageBox.Show("Erro! UPDATE rim_has_veiculo SET Cod_rim=" + Global.RI.cetil + " WHERE Cod_rim='" +
                 txtCetil.Text + "'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } 

        }

        private void gravaRim_has_veiculo()
        {
            try
            {
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

                // Abre a conexão
                mConn.Open();

                //Query SQl
                
                //MySqlCommand command = new MySqlCommand("INSERT INTO rim_has_dotacao (Cod_rim,Cod_despesa,Cetil) VALUES(999999," + txtCodDespesa.Text + ",'" +
                // txtCodigo.Text + "')", mConn);

                MySqlCommand command = new MySqlCommand("INSERT INTO rim_has_veiculo (Cod_rim,Cod_seq_veiculo) VALUES(" + Global.RI.cetil 
                    + ",'" + txtCodVeiculo.Text + "')", mConn);


                //Executa a Query SQL
                command.ExecuteNonQuery();

                // Fecha a conexão
                mConn.Close();

                            }
            catch
            {
                MessageBox.Show("ERRO! INSERT INTO rim_has_veiculo (Cod_rim, Cod_seq_veiculo) VALUES(" + Global.RI.cetil
                    + ",'" + txtCodVeiculo.Text + "')", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        
        // A STRING DE CONEXÃO ABAIXO DEVE SER REVISTA PARA QUE HAJA ATUALIZAÇÃO DO CODIGO RIM GERADO APOS GRAVAÇÃO DA RI. A TABELA RIM_HAS_DOTAÇÃO
        // RECEBE PARA OS ATRIBUTOS Cod_rim e Cetil o mesmo valor QUE É O cETIL INFORMADO NO FORM RIM. NA GRAVACAO DE FORM RIM APOS A GERACAO DO CODIGO SEQUENCIAL
        // DEVEREMOS ATUALIZAR O Cod_rim ATRAVES DO METOOO ABAIXO.

        private void atualizaRim_has_dotacao()
        {
            try
            {
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

                // Abre a conexão
                mConn.Open();

                //Query SQl
                //MySqlCommand command = new MySqlCommand("INSERT INTO rim_has_dotacao (Cod_rim,Cod_despesa,Cetil) VALUES(999999," + txtCodDespesa.Text + ",'" +
                // txtCodigo.Text + "')", mConn);

                Global.RI.cetil = txtCodigo.Text;

                MySqlCommand command = new MySqlCommand("UPDATE rim_has_dotacao SET Cod_rim=" + Global.RI.cetil + " WHERE Cetil='" +
                 txtCetil.Text + "'", mConn);

                
                //Executa a Query SQL
                command.ExecuteNonQuery();


                //Mensagem de Sucesso
                //MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //mostrarResultados();
                // Fecha a conexão
                mConn.Close();
            
            }
            catch
            {
                MessageBox.Show("ERRO! UPDATE rim_has_dotacao SET Cod_rim=" + Global.RI.cetil + " WHERE Cetil='" +
                 txtCetil.Text + "'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } 

        }

        private void retiraEspaços()
        {
            txtdescricao.Text = txtdescricao.Text.Trim();
            txtCodigo.Text = txtCodigo.Text.Trim();
            cmbEscolha.Text = cmbEscolha.Text.Trim();
            txtdescricao.Text = txtdescricao.Text.Trim();
            txtdescricao.Text = txtdescricao.Text.ToUpper();
            txtDO.Text = txtDO.Text.Trim();
            txtCetil.Text = txtCetil.Text.Trim();
            txtdataCetil.Text = txtdataCetil.Text.Trim();
            txtvalorEstimado.Text = txtvalorEstimado.Text.Trim();
            txtvalorReal.Text = txtvalorReal.Text.Trim();
            txtProcesso.Text = txtProcesso.Text.Trim();
            //txtAutorizacao.Text = txtAutorizacao.Text.Trim();
            //txtDataAutorizacao.Text = txtDataAutorizacao.Text.Trim();
            //cmbSetor.Text = cmbSetor.Text.Trim();
            //txtdataEnvio.Text = txtdataEnvio.Text.Trim();
            cmbcadastradoPor.Text = cmbcadastradoPor.Text.Trim();
            txtdtCadastro.Text = txtdtCadastro.Text.Trim();
            cmbFornecedor.Text = cmbFornecedor.Text.Trim();
            //txtnotaFiscal.Text = txtnotaFiscal.Text.Trim();
            //txtdataNotaFiscal.Text = txtdataNotaFiscal.Text.Trim();
            txtObs.Text = txtObs.Text.Trim();
            
        
        }

        private void DesabilitaCheckBox()
        {
            checkBoxCompras1.Checked = false;
            checkBoxCompras2.Checked = false;
            checkBoxContab.Checked = false;
            checkBoxDIPE.Checked = false;
            checkBoxOrdenador1.Checked = false;
            checkBoxOrdenador2.Checked = false;

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

            //lblMsg.Text = "Módulo de atualização de dados.";
            toolStripStatusMensagem.Text = "módulo de atualização ativado.";

            bt_Gravar.Enabled = false;

            codigoultimari = Convert.ToInt32(txtCodigo.Text);
            Alterar(codigoultimari);
            mostrarResultados();

            MessageBox.Show("Item " + "'" + codigoultimari + "'" + " alterado com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            bt_Gravar.Enabled = false;
            dataGridView1.Visible = true;
            btnCancelar.Visible = false;
            label28.Visible = true;
            groupBox3.Visible = true;
            groupBox4.Visible = true;
            LimpaCampos();
            rbNovo.Enabled = true;

        }

        private void Alterar(int codigo)
        {            
            if (radioButtonRIM.Checked == true)
            {
                TipoRIM = "RIM";
            }
            else
            {
                TipoRIM = "RRP";
            }


            if (txtvalorReal.Text == "")
            {
                txtvalorReal.Text = "0.00";
            }
            else
            {
            }

            retiraEspaços();

                    //conexao
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    mConn.Open();

                    try
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = mConn;
                        
                        if (radioButtonRRP.Checked == true)
                        {
                            /*
                            ("INSERT INTO rim (Nome_Unidade,Descricao,Dotacao,Tipo_RIM,Cetil,DataCetil,DataCetilSQL,ValorEstimado,ValorReal,Processo,ano_processo,
                             * ProcessoContabil,ano_processo_contabil,Contabilidade,OrdenadorAss,ComprasPrim,OrdenadorEmpenho,ComprasSeg,Dipe,Cadastrante,DataCadastro,
                             * Observacao,Cd_Usuario,CD_unidade)" + "VALUES('" + cmbEscolha.Text + "','" + txtdescricao.Text + "','" + txtDO.Text + "','" 
                             * + TipoRIM.Trim() + "','"
                        + txtCetil.Text + "','" + txtdataCetil.Text + "','" + Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd") + "','"
                        + Convert.ToDecimal(txtvalorEstimado.Text) + "','" + Convert.ToDecimal(txtvalorReal.Text) + "','" + txtProcesso.Text + "','"
                        + txtAnoProcesso.Text + "','" + txtProcessoContabil.Text + "','" + txtAnoProcessoContabil.Text + "','"
                        + lblDataContabilidade.Text + "','" + lblDataOrdenador1.Text + "','" + lblDataCompras1.Text + "','"
                        + lblDataOrdenador2.Text + "','" + lblDataCompras2.Text + "','" + lblDataDipe.Text + "','" + cmbcadastradoPor.Text + "','"
                        + txtdtCadastro.Text + "','" + txtObs.Text + "'," + txtCodUsuario.Text + "," + txtCodUnidade.Text + ")", mConn);
                            */

                            cmd.CommandText = "UPDATE rim SET Nome_Unidade =" +
                                "'" + cmbEscolha.Text + "',"
                                + "Descricao=" + "'" + txtdescricao.Text + "',"
                                + "Dotacao=" + "'" + txtDO.Text + "',"
                                + "Tipo_RIM=" + "'" + TipoRIM + "',"
                                + "Cetil=" + "'" + txtCetil.Text + "',"
                                + "DataCetil=" + "'" + txtdataCetil.Text + "',"
                                + "DataCetilSQL=" + "'" + Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd") + "',"
                                + "ValorEstimado=" + "'" + txtvalorEstimado.Text + "'," 
                                + "ValorReal=" + "'" + txtvalorReal.Text + "',"
                                + "Processo=" + "'" + txtProcesso.Text + "',"
                                + "ano_processo=" + "'" + txtAnoProcesso.Text + "',"
                                + "ProcessoContabil=" + "'" + txtProcessoContabil.Text + "',"
                                + "ano_processo_contabil=" + "'" + txtAnoProcessoContabil.Text + "',"
                                + "Contabilidade=" + "'" + lblDataContabilidade.Text + "',"
                                + "OrdenadorAss=" + "'" + lblDataOrdenador1.Text + "',"
                                + "ComprasPrim=" + "'" + lblDataCompras1.Text + "',"
                                + "OrdenadorEmpenho=" + "'" + lblDataOrdenador2.Text + "',"
                                + "ComprasSeg=" + "'" + lblDataCompras2.Text + "',"
                                + "Dipe=" + "'" + lblDataDipe.Text + "',"
                                + "Cadastrante=" + "'" + cmbcadastradoPor.Text + "',"
                                + "DataCadastro=" + "'" + txtdtCadastro.Text + "',"
                                + "Observacao=" + "'" + txtObs.Text
                                + "'" + "WHERE Cod_rim=" + codigo;
                        }
                        else {
                            txtAnoProcessoContabil.Text = "";
                                cmd.CommandText = "UPDATE rim SET Nome_Unidade =" +
                                "'" + cmbEscolha.Text + "',"
                                + "Descricao=" + "'" + txtdescricao.Text + "',"
                                + "Dotacao=" + "'" + txtDO.Text + "',"
                                + "Tipo_RIM=" + "'" + TipoRIM + "',"
                                + "Cetil=" + "'" + txtCetil.Text + "',"
                                + "DataCetil=" + "'" + txtdataCetil.Text + "',"
                                + "DataCetilSQL=" + "'" + Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd") + "',"
                                + "ValorEstimado=" + "'" + txtvalorEstimado.Text + "',"
                                + "ValorReal=" + "'" + txtvalorReal.Text + "',"
                                + "Processo=" + "'" + txtProcesso.Text + "',"
                                + "ano_processo=" + "'" + txtAnoProcesso.Text + "',"
                                + "ProcessoContabil=" + "'" + txtProcessoContabil.Text + "',"
                                + "ano_processo_contabil=" + "'" + txtAnoProcessoContabil.Text + "',"
                                + "Contabilidade=" + "'" + lblDataContabilidade.Text + "',"
                                + "OrdenadorAss=" + "'" + lblDataOrdenador1.Text + "',"
                                + "ComprasPrim=" + "'" + lblDataCompras1.Text + "',"
                                + "OrdenadorEmpenho=" + "'" + lblDataOrdenador2.Text + "',"
                                + "ComprasSeg=" + "'" + lblDataCompras2.Text + "',"
                                + "Dipe=" + "'" + lblDataDipe.Text + "',"
                                + "Cadastrante=" + "'" + cmbcadastradoPor.Text + "',"
                                + "DataCadastro=" + "'" + txtdtCadastro.Text + "',"
                                + "Observacao=" + "'" + txtObs.Text
                                + "'" + " WHERE Cod_rim=" + codigo;
                        }

                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível alterar os dados da Unidade " + codigo);
                        }
                    }

                    catch 
                    {
                        MessageBox.Show("UPDATE rim SET Nome_Unidade =" +
                                "'" + cmbEscolha.Text + "',"
                                + "Descricao=" + "'" + txtdescricao.Text + "',"
                                + "Dotacao=" + "'" + txtDO.Text + "',"
                                + "Tipo_RIM=" + "'" + TipoRIM + "',"
                                + "Cetil=" + "'" + txtCetil.Text + "',"
                                + "DataCetil=" + "'" + txtdataCetil.Text + "',"
                                + "DataCetilSQL=" + "'" + Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd") + "',"
                                + "ValorEstimado=" + "'" + txtvalorEstimado.Text + "',"
                                + "ValorReal=" + "'" + txtvalorReal.Text + "',"
                                + "Processo=" + "'" + txtProcesso.Text + "',"
                                + "ano_processo=" + "'" + txtAnoProcesso.Text + "',"
                                + "ProcessoContabil=" + "'" + txtProcessoContabil.Text + "',"
                                + "ano_processo_contabil=" + "'" + txtAnoProcessoContabil.Text + "',"
                                + "Contabilidade=" + "'" + lblDataContabilidade.Text + "',"
                                + "OrdenadorAss=" + "'" + lblDataOrdenador1.Text + "',"
                                + "ComprasPrim=" + "'" + lblDataCompras1.Text + "',"
                                + "OrdenadorEmpenho=" + "'" + lblDataOrdenador2.Text + "',"
                                + "ComprasSeg=" + "'" + lblDataCompras2.Text + "',"
                                + "Dipe=" + "'" + lblDataDipe.Text + "',"
                                + "Cadastrante=" + "'" + cmbcadastradoPor.Text + "',"
                                + "DataCadastro=" + "'" + txtdtCadastro.Text + "',"
                                + "Observacao=" + "'" + txtObs.Text
                                + "'" + "WHERE Cod_rim=" + codigo,"Informação",MessageBoxButtons.OK,MessageBoxIcon.Warning);                       
                    }
                    finally
                    {
                        mConn.Close();

                        MessageBox.Show("Item " + "'" + codigo + "'" + " alterado com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Global.despesa.flag_valor_real = "0";

                        bt_Gravar.Enabled = false;

                        GroupBox1.Visible = false;
                        dataGridView1.Visible = true;
                        btnCancelar.Visible = false;
                        groupBox3.Visible = true;
                        mostrarResultados();

                        if (rbNovo.Checked == true)
                            rbNovo.Checked = false;
                        else
                            if (rbExclui.Checked == true)
                                rbExclui.Checked = false;
                            else
                                rbAlterar.Checked = false;
                                rbExclui.Enabled = true;
                        
                        rbNovo.Enabled = true;
                        rbAlterar.Enabled = true;
                        
                    }

                    cmbFornecedor.Items.Clear();
                    atualizaRim_has_dotacao();

        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if ((rbNovo.Checked == false) && (rbAlterar.Checked == false) && (rbAlterar.Checked == false))
                {
                    dataGridView1.Visible = false;
                    btnCancelar.Visible = true;
                    
                    label28.Visible = false;
                    GroupBox1.Visible = true; 
                    groupBox2.Visible = true;
                    bt_Cancelar.Visible = false;
                    GroupBox1.Enabled = false;
                    groupBox3.Visible = false;
                    groupBox4.Visible = false;
                   
                    lblModuloVisualização.Visible = true;
                    lblModuloVisualização.Text = "Módulo Consulta";

                    //DesabilitaRadioButtons();
                    bt_Gravar.Enabled = false;
                   
                    cmbcadastradoPor.Text = Global.Logon.usuario;

                    txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    cmbEscolha.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtdescricao.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtDO.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                    if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "RIM")
                    {
                        radioButtonRIM.Checked = true;
                    }
                    else
                    {
                        radioButtonRRP.Checked = true;
                    }

                    txtCetil.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtdataCetil.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtvalorEstimado.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtvalorReal.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtProcesso.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtProcessoContabil.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                    //txtAutorizacao.Text = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    //txtDataAutorizacao.Text = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    //cmbSetor.Text = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    //txtdataEnvio.Text = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    lblDataContabilidade.Text = dataGridView1[16, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                    lblDataOrdenador1.Text = dataGridView1[17, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    lblDataCompras1.Text = dataGridView1[18, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    lblDataOrdenador2.Text = dataGridView1[19, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    lblDataCompras2.Text = dataGridView1[20, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    lblDataDipe.Text = dataGridView1[21, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                    cmbcadastradoPor.Text = dataGridView1[22, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtdtCadastro.Text = dataGridView1[23, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    cmbFornecedor.Text = dataGridView1[24, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    //txtnotaFiscal.Text = dataGridView1[25, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    //txtdataNotaFiscal.Text = dataGridView1[26, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                    txtObs.Text = dataGridView1[27, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtReduzida.Text = dataGridView1[32, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtPrograma.Text = dataGridView1[33, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtAcao.Text = dataGridView1[34, dataGridView1.CurrentCellAddress.Y].Value.ToString();


                    ///////////////////////////////////////////////////////////////////////////////////////////////////

                }
                else
                {

                    lblModuloVisualização.Visible = false;

                    dataGridView1.Visible = false;
                    btnCancelar.Visible = true;
                   
                    label28.Visible = false;
                    GroupBox1.Visible = true; 
                    groupBox2.Visible = true;
                    bt_Cancelar.Visible = false;
                    GroupBox1.Enabled = false;
                    groupBox3.Visible = false;
                    groupBox4.Visible = false;
                    //WebBrowser1.Visible = false;


                    //DesabilitaRadioButtons();
                    bt_Gravar.Enabled = false;
                    
                    cmbcadastradoPor.Text = Global.Logon.usuario;

                    txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    cmbEscolha.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtdescricao.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtDO.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                    if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "RIM")
                    {
                        radioButtonRIM.Checked = true;

                    }
                    else
                    {
                        radioButtonRRP.Checked = true;
                    }


                    txtCetil.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtdataCetil.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtvalorEstimado.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtvalorReal.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtProcesso.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtProcessoContabil.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                    // txtAutorizacao.Text = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    // txtDataAutorizacao.Text = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    //cmbSetor.Text = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    //txtdataEnvio.Text = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    lblDataContabilidade.Text = dataGridView1[16, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                    lblDataOrdenador1.Text = dataGridView1[17, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    lblDataCompras1.Text = dataGridView1[18, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    lblDataOrdenador2.Text = dataGridView1[19, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    lblDataCompras2.Text = dataGridView1[20, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    lblDataDipe.Text = dataGridView1[21, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                    cmbcadastradoPor.Text = dataGridView1[22, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtdtCadastro.Text = dataGridView1[23, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    cmbFornecedor.Text = dataGridView1[24, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    //txtnotaFiscal.Text = dataGridView1[25, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    //txtdataNotaFiscal.Text = dataGridView1[26, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                    txtObs.Text = dataGridView1[27, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtReduzida.Text = dataGridView1[32, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtPrograma.Text = dataGridView1[33, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                    txtAcao.Text = dataGridView1[34, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                } 
            }
              
        }

        private void rbExclui_CheckedChanged(object sender, EventArgs e)
        {
            //lblMsg.Text = "Módulo de exclusão de dados secionado.";
            

            toolStripStatusMensagem.Text = "módulo de EXCLUSÃO ativado... aguardando seleção de item.";
            bt_Gravar.Enabled = false;
            bt_Cancelar.Visible = true;
            bt_Cancelar.Enabled = true;
            

            if (rbExclui.Checked == true)
            {

                lblInformacao.Visible = true;
                lblInformacao.Text = "";
                lblInformacao.Text = "Localize na planilha a requisição para exclusão e dê duplo clique na linha correspondente para abri-la";
                //MessageBox.Show("Dê duplo clique, na planilha, na linha da requisição a ser excluída.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dataGridView1.Enabled = true;
                lblModuloVisualização.Visible = false;
                textBox1.Visible = false;
       
                DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                
              
            }
            
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
                        cmd.CommandText = "delete from rim where Cod_rim = " + codigo;
                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível excluir a Unidade " + codigo);
                        }
                    }
                    /*catch (MySqlException ex)
                    {
                        throw new Exception("Servidor SQL Erro:" + ex.Number);
                    }*/

                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        mConn.Close();
                        mostrarResultados();

                    }

                    MessageBox.Show("Requisição nr. '" + codigo + " 'excluída com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //lblMsg.Text = "";
                    toolStripStatusMensagem.Text = "";

                    GroupBox1.Visible = false;
                    //WebBrowser1.Visible = false;
                    dataGridView1.Visible = true;
                    btnCancelar.Visible = false;
                    groupBox3.Visible = true;

                    textBox1.Text = "";
                    textBox1.Visible = false;

                    if (rbNovo.Checked == true)
                        rbNovo.Checked = false;
                    else
                        if (rbExclui.Checked == true)
                            rbExclui.Checked = false;
                        else
                            rbAlterar.Checked = false;


                    rbExclui.Enabled = true;
                    rbNovo.Enabled = true;
                    rbAlterar.Enabled = true;

                }
            }
        }

        private void checkBoxContab_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxContab.Checked == true)
            {
                /*
                dtpDivisaoContabil.Visible = true;
                dtpCompras1.Visible = false;
                dtpCompras2.Visible = false;
                dtpOrdenador1.Visible = false;
                dtpOrdenadorEmpenho.Visible = false;
                dtpDipe.Visible = false;
                */
                monthCalendar1.Visible = true;
                lblDataContabilidade.Visible = true;
                checkBoxContab.Enabled = true;
                 
            }
            else {

                lblDataContabilidade.Text = "";
                monthCalendar1.Visible = false;
                                          
            }

        }

        
        private void rbPorNomeUnidade_CheckedChanged(object sender, EventArgs e)
        {
           // lblMsg.Text = "Requisições ordenadas por unidade.";
            toolStripStatusMensagem.Text = "requisições ordenadas por unidade";

            mostrarResultados();
        }

        private void rbFornecedor_CheckedChanged(object sender, EventArgs e)
        {
            //lblMsg.Text = "Requisições ordenadas por código.";
            toolStripStatusMensagem.Text = "requisições ordenadas por código";
            mostrarResultados();
        }

        private void txtCheckCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //lblMsg.Text = "Pesquisa por código da RI";
                       
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtCheckCodigo.Text != "")
                {
                    temp = txtCheckCodigo.Text;
                    codigoultimari = Convert.ToInt32(temp);
                    PesquisaPorCodigo(codigoultimari);
                    LimpaFiltros();

                }
                else {
                }

            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }
        }

        private void LimpaFiltros()
        {
            txtCheckCodigo.Text = "";
            txtCheckIdentificação.Text = "";
            txtCheckFornecedor.Text = "";
            txtCheckDescricao.Text = "";
            txtCheckAF.Text = "";
            txtCheckCetil.Text = "";
            txtCheckDataCetil.Text = "";
            txtCheckProcesso.Text = "";
            txtCheckProcessoContabil.Text = "";
            txtCheckReduzida.Text = "";
            txtCheckPrograma.Text = "";
            txtCheckDesdobrada.Text = "";
            txtCheckEmpenho.Text = "";
            txtCheckCodigoAplicacao.Text = "";
            txtCheckAF.Text = "";

        }

        private void PesquisaPorCodigo(int codigo)
        {
            /*
            if (chkPlanilhaDespesas.Checked == false)
            {
                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //if (checkBox1.Checked == true)
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM rim Where Tipo_RIM ='" + TipoRIM + "' AND Cod_rim=" + codigo, mConn);
                
                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else
            {
                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //if (checkBox1.Checked == true)
                // ordena a tabela de acordo com o critério estabelecido
                
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa Where Tipo_RIM ='" + TipoRIM + "' AND Cod_rim=" + codigo, mConn);
                
                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";
            }

            calculaQuantidadeRegistros();
            */

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

        private void txtCheckCetil_KeyPress(object sender, KeyPressEventArgs e)
        {
            toolStripStatusMensagem.Text = "pesquisa por número da requisição";

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtCheckCetil.Text != "")
                {
                    PesquisaPorCetil(txtCheckCetil.Text);
                    LimpaCampos();
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

                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Cetil like '%" + temp + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                }
             
                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else {

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

        private void txtCheckIdentificação_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                //temp_1 = txtCheckIdentificação.Text;
                PesquisaPorIdentificação(txtCheckIdentificação.Text);
                LimpaFiltros();
            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }

        }

        private void PesquisaPorIdentificação(string temp)
        {
            /*
            if (chkPlanilhaDespesas.Checked == false)
            {
                //mostraChecks();
                //lblMsg.Text = "Pesquisa por nome da Unidade Gestora.";
                toolStripStatusMensagem.Text = "pesquisa por nome da Unidade Gestora";

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //if (checkBox2.Checked == true)
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Tipo_RIM ='" + TipoRIM + "' AND Nome_Unidade " + "LIKE " + "'%" + temp + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else 
            {
                //mostraChecks();
                //lblMsg.Text = "Pesquisa por nome da Unidade Gestora.";
                toolStripStatusMensagem.Text = "pesquisa por nome da Unidade Gestora";

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //if (checkBox2.Checked == true)
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Tipo_RIM ='" + TipoRIM + "' AND Nome_Unidade " + "LIKE " + "'%" + temp + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";
            }

            calculaQuantidadeRegistros();
            */

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
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Nome_Unidade like '%" + temp + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
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
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
            if (bt_Cancelar.Enabled == true)
            {
                bt_Cancelar.Enabled = false;
           
            }
            else {}
            
           // desmarca radio buttons
            if (rbNovo.Checked == true)
                rbNovo.Checked = false;
            else
                if (rbExclui.Checked == true)
                    rbExclui.Checked = false;
                else
                    rbAlterar.Checked = false;

            //lblMsg.Text = "Ação cancelada.";
            toolStripStatusMensagem.Text = "ação de inclusão cancelada";

            rbExclui.Enabled = true;
            rbNovo.Enabled = true;
            rbAlterar.Enabled = true;
            btnEmpenho.Enabled = false;

            //rbExclui.Checked = false;
            //rbNovo.Checked = false;
            //rbAlterar.Checked = false;

            GroupBox1.Enabled = false;

            bt_Gravar.Enabled = false;
            // btnAtualizar.Enabled = false;

            dataGridView1.Visible = true;
            btnCancelar.Visible = false;

            label28.Visible = true;
            groupBox3.Visible = true;
            groupBox4.Visible = true;
           
            GroupBox1.Visible = false;
            //WebBrowser1.Visible = false;
            textBox1.Visible = false;
            label30.Text = "";
           
        }

        private void cmbEscolha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação 
            {
                    for (int i = 0; i < cmbPlaca.Items.Count; i++)
                    {
                      cmbPlaca.Items.RemoveAt(i);
                    }
                

                // isso será feito no metodo recupera dados veiculos


                // -------- Obtendo codigo da unidade escolhida

                try
                {

                    stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT Cod_unidade FROM unidade WHERE Nome_unidade='" + cmbEscolha.Text + "'";

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

                    MessageBox.Show("Unidade não cadastrada.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //lblMsg.Text = "Falha na conexão.";
                    toolStripStatusMensagem.Text = "houve falha na conexão";

                }

                Cmn.Close();

                txtdescricao.Focus();

                if (radioButtonVeiculo.Enabled == true)
                {
                    //populaCmbPlaca(codunidade);
                    populaCmbPlaca();
                    cmbPlaca.Focus();
                      
                }
                else
                {
                    txtdescricao.Focus();
                }

                
                
            }
            else
            {
                cmbEscolha.Focus();
            }

            
            
        }

        
                
        private void txtvalorReal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtvalorReal.Text != "")
                {
                    try
                    {
                        txtvalorReal.Text = Convert.ToDecimal(txtvalorReal.Text).ToString("C");
                        txtvalorReal.Text = txtvalorReal.Text.Replace("R$", "");
                       
                        groupBox2.Enabled = true;
                        toolStripStatusMensagem.Text = "trâmite Habilitado ";
                        //lblMsg.Text = "Clique no setor correspondente de destino do documento.";
                        toolStripStatusMensagem.Text = "clique no setor correspondente de destino do documento";

                        cmbcadastradoPor.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("Insira um valor válido.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lblMsg.Text = "O valor para o campo deve estar num padrão de moeda correto.";
                        toolStripStatusMensagem.Text = "o valor para o campo deve estar num padrão correto de moeda";
                        
                        txtvalorReal.Text = "";
                        txtvalorReal.Focus();
                    }

                }
                else
                {
                }
            }
            else
            {
                txtvalorReal.Focus();

            }
        }

        private void txtvalorEstimado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                
                if (txtvalorEstimado.Text != "")
                {
                    try
                    { 
                        // desse jeito funicona até o impedimento de entrar com valor não numérico
                        txtvalorEstimado.Text = Convert.ToDecimal(txtvalorEstimado.Text).ToString("C");
                        txtvalorEstimado.Text = txtvalorEstimado.Text.Replace("R$", "");
                        if (radioButtonRRP.Checked == true)
                        {
                            txtvalorReal.Text = txtvalorEstimado.Text;
                        }
                        else {
                        }

                            cmbcadastradoPor.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("Insira um valor válido.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toolStripStatusMensagem.Text = "o valor para o campo deve estar num padrão de moeda correto";
                        txtvalorEstimado.Text = "";
                        txtvalorEstimado.Focus();

                    }

                }
                else
                {
                }

            }
            else
            {

                txtvalorEstimado.Focus();

            }

        }

        private void txtProcesso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                //txtDO.Focus();
                txtAnoProcesso.Focus();
            }
            else
            {
                txtProcesso.Focus();

            }
        }

        

        private void txtdataEnvio_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtdataEnvio.Text != "")
                {
                    txtObs.Focus();
                }
                else
                {
                    txtdataEnvio.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    toolStripStatusMensagem.Text = "verifique se a data da ação correspondente está correta";
                    txtObs.Focus();

                }
            }
            else
            {
                txtdataEnvio.Focus();

            }
             */
        }


        private void txtdtCadastro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtdtCadastro.Text == "")
                {
                    txtdtCadastro.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    cmbFornecedor.Focus();
                }
                else
                {

                }
            }
            else
            {
                txtdtCadastro.Focus();

            }
        }


        private void txtnotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                monthCalendar1.Visible = true;
                //txtdataNotaFiscal.Focus();
            }
            else
            {
                //txtnotaFiscal.Focus();

            }

        }

        private void txtdataNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void rbPorCodigo_CheckedChanged(object sender, EventArgs e)
        {                    
            mostrarResultados();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int indice = dataGridView1.SelectedRows[0].Index;
                if (indice >= 0)
                {

                    txtCodigo.Text = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["CustomerCode"].Value.ToString();

                }

            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)

                if (textBox1.Text == "")
                {
                    MessageBox.Show("Escolha um item ou cancele a operação de exclusão.");
                }
                else
                {

                    temp_1 = textBox1.Text;
                    codigoultimari = Convert.ToInt32(temp_1);
                    Excluir(codigoultimari);
                    textBox1.Visible = false;
                }
            else
                textBox1.Focus();
        }

        private void checkBoxOrdenador1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOrdenador1.Checked == true)
            {
                /*
                dtpDivisaoContabil.Visible = true;
                dtpCompras1.Visible = false;
                dtpCompras2.Visible = false;
                dtpOrdenador1.Visible = false;
                dtpOrdenadorEmpenho.Visible = false;
                dtpDipe.Visible = false;
                */
                monthCalendar1.Visible = true;
                lblDataOrdenador1.Visible = true;
                checkBoxOrdenador1.Visible = true;
            }
            else
            {
                lblDataOrdenador1.Text = "";
                monthCalendar1.Visible = false;

            }
            
        }

        private void checkBoxCompras1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCompras1.Checked == true)
            {
                /*
                dtpDivisaoContabil.Visible = true;
                dtpCompras1.Visible = false;
                dtpCompras2.Visible = false;
                dtpOrdenador1.Visible = false;
                dtpOrdenadorEmpenho.Visible = false;
                dtpDipe.Visible = false;
                */
                monthCalendar1.Visible = true;
                lblDataCompras1.Visible = true;
                checkBoxOrdenador1.Visible = true;

            }
            else
            {

                lblDataCompras1.Text = "";
                monthCalendar1.Visible = false;

            }
        }

        private void checkBoxOrdenador2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOrdenador2.Checked == true)
            {
                /*
                dtpDivisaoContabil.Visible = true;
                dtpCompras1.Visible = false;
                dtpCompras2.Visible = false;
                dtpOrdenador1.Visible = false;
                dtpOrdenadorEmpenho.Visible = false;
                dtpDipe.Visible = false;
                */
                monthCalendar1.Visible = true;
                lblDataOrdenador2.Visible = true;
                checkBoxOrdenador2.Visible = true;

            }
            else
            {
                lblDataOrdenador2.Text = "";
                monthCalendar1.Visible = false;
            }
        }

        private void checkBoxCompras2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCompras2.Checked == true)
            {
                /*
                dtpDivisaoContabil.Visible = true;
                dtpCompras1.Visible = false;
                dtpCompras2.Visible = false;
                dtpOrdenador1.Visible = false;
                dtpOrdenadorEmpenho.Visible = false;
                dtpDipe.Visible = false;
                */
                monthCalendar1.Visible = true;
                lblDataCompras2.Visible = true;
                checkBoxCompras2.Visible = true;

            }
            else
            {
                lblDataCompras2.Text = "";
                monthCalendar1.Visible = false;
            }
                       
        }

        private void checkBoxDIPE_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDIPE.Checked == true)
            {
                /*
                dtpDivisaoContabil.Visible = true;
                dtpCompras1.Visible = false;
                dtpCompras2.Visible = false;
                dtpOrdenador1.Visible = false;
                dtpOrdenadorEmpenho.Visible = false;
                dtpDipe.Visible = false;
                */
                monthCalendar1.Visible = true;
                lblDataDipe.Visible = true;
                checkBoxDIPE.Visible = true;

            }
            else
            {
                lblDataDipe.Text = "";
                monthCalendar1.Visible = false;
            }
        
        }


        private void txtObs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtObs.Text.ToUpper();
            }
            else
            {
                txtObs.Focus();
            }
        }

        private void btnFornecedor_Click(object sender, EventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.ShowDialog();

        }

        private void btnCadastrante_Click(object sender, EventArgs e)
        {
            Usuarios cadastrante = new Usuarios();
            cadastrante.Show();
        }

        private void btnHabilitaSetores_Click(object sender, EventArgs e)
        {            
            if (estado == 0)
            {
                MessageBox.Show("INFORMAÇÔES IMPORTANTES. Atualizar sempre que necessário. ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupBox2.Enabled = true;
                estado = 1;
                toolStripStatusMensagem.Text="trâmite Habilitado";
            }
            else
            {
                groupBox2.Enabled = false;
                estado = 0;
                toolStripStatusMensagem.Text = "trâmite desabilitado";
            }
        }


        private void txtCheckFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorFornecedor(txtCheckFornecedor.Text);
                LimpaFiltros();
            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }
             */

        }

        private void PesquisaPorFornecedor(string p)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Nome_fornecedor " + "LIKE " + "'%" + p + "%'", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "rim");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "rim";

            calculaQuantidadeRegistros();
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

        private void PesquisaPorDescricao(string p)
        {
            /*
            if (chkPlanilhaDespesas.Checked == false)
            {
                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Tipo_RIM ='" + TipoRIM + "' AND Descricao " + "LIKE " + "'%" + p + "%'", mConn);
               // mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Descricao " + "LIKE " + "'%" + p + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else
            {

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Tipo_RIM ='" + TipoRIM + "' AND Descricao " + "LIKE " + "'%" + p + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";           
            
            }

            calculaQuantidadeRegistros();
             */

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

        private void txtCheckAF_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorAF(txtCheckAF.Text);
                LimpaFiltros();
            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }


        }

        private void PesquisaPorAF(string p)
        {
            /*
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE autorizacao " + "LIKE " + "'%" + p + "%'", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "planilha_despesa");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "planilha_despesa";

            calculaQuantidadeRegistros();
            */

            mDataSet = new DataSet();
               mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
               mConn.Open();
               // não há empenho na planilha rim
               if (chkPlanilhaDespesas.Checked == false)
               {
                   //mostraChecks();

                   //cria um adapter utilizando a instrução SQL para acessar a tabela 

                   /*
                   if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº do empenho vinculado à requisição";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE empemho like '%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº do empenho vinculado à requisição no período selecionado";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE empenho '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                   }

                   //preenche o dataset através do adapter
                   mAdapter.Fill(mDataSet, "rim");

                   //atribui o resultado à propriedade DataSource da dataGridView
                   dataGridView1.DataSource = mDataSet;
                   dataGridView1.DataMember = "rim";
                    */
               }
               else
               {
                   if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº da Autorização vinculado à requisição";

                       mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE autorizacao like" + "'%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº da Autorização vinculado à requisição no período selecionado";

                       mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (autorizacao like '%" + p + "%' and (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text+ "') ) ORDER BY Processo", mConn);
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

        private void txtCheckDataCetil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorDataCetil(txtCheckDataCetil.Text);
                LimpaCampos();
            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }

        }

        private void PesquisaPorDataCetil(string p)
        {
            /*
            if (chkPlanilhaDespesas.Checked == false)
            {
                mDataSet = new DataSet();

                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();
                // 10.1.112.7

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE DataCetil='" + p + "'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else {


                mDataSet = new DataSet();

                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();
                // 10.1.112.7

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE DataCetil='" + p + "'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";
                   
            }

            calculaQuantidadeRegistros();
            */
            
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
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE DataCetil like '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
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
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Data_Cetil like" + "'%" + p + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por data da requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Data_Cetil like '%" + p + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
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
                LimpaCampos();
            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }
        }

        private void PesquisaPorProcesso(string p)
        {
           /*
            if (chkPlanilhaDespesas.Checked == false)
            {
                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Processo " + "LIKE " + "'%" + p + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim";
            }
            else {

                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Processo " + "LIKE " + "'%" + p + "%'", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa;       
            }
            */

            /*
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            if (chkPlanilhaDespesas.Checked == false)
            {
                //mostraChecks();

                //cria um adapter utilizando a instrução SQL para acessar a tabela 

                if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo vinculado à requisição";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE processo like '%" + p + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo vinculado à requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE processo like '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
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
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo vinculado à requisição";

                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE processo like" + "'%" + p + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo vinculado à requisição no período selecionado";

                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (processo like '%" + p + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
                }

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "planilha_despesa");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "planilha_despesa";

            }

            mConn.Close();
            calculaQuantidadeRegistros();
            */

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
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Processo like '%" + p + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição no período selecionado";
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Processo like '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
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

                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Processo like" + "'%" + p + "%'", mConn);
                }
                else
                {
                    toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição no período selecionado";

                    mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Processo like '%" + p + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbEscolha.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdescricao.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDO.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCetil.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdataCetil.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtvalorEstimado.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtvalorReal.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtProcesso.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtProcessoContabil.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            //txtAutorizacao.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            //txtDataAutorizacao.Text = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            //cmbSetor.Text = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            //txtdataEnvio.Text = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataContabilidade.Text = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataOrdenador1.Text = dataGridView1[16, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataCompras1.Text = dataGridView1[17, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataOrdenador2.Text = dataGridView1[18, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataCompras2.Text = dataGridView1[19, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataDipe.Text = dataGridView1[20, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbcadastradoPor.Text = dataGridView1[21, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdtCadastro.Text = dataGridView1[22, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbFornecedor.Text = dataGridView1[23, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            //txtnotaFiscal.Text = dataGridView1[24, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            //txtdataNotaFiscal.Text = dataGridView1[25, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtObs.Text = dataGridView1[26, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtReduzida.Text = dataGridView1[31, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtAcao.Text = dataGridView1[32, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtPrograma.Text = dataGridView1[33, dataGridView1.CurrentCellAddress.Y].Value.ToString();
          
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {
            cmbcadastradoPor.Items.Clear();
            cmbEscolha.Items.Clear();
            cmbFornecedor.Items.Clear();

            retiraEspaços();

            // POPULANDO TODOS ComboBox

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
                    cmbEscolha.Items.Add(unidade.Rows[i]["Nome_Unidade"]);
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

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

            //---------------------------------------------------------
            // populando cmbCadastradoPor

            mAdapter = new MySqlDataAdapter("SELECT * FROM usuario ORDER BY Nome_usuario", mConn);
            DataTable usuario = new DataTable();
            mAdapter.Fill(usuario);
            try
            {
                for (int i = 0; i < usuario.Rows.Count; i++)
                {
                    cmbcadastradoPor.Items.Add(usuario.Rows[i]["Nome_usuario"]);
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

            //---------------------------------------------------------

            mConn.Close();

        }

        private void txtdataEnvio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtdescricao_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtvalorReal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDataAutorizacao_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {

            if (estado == 0)
            {
                GroupBox1.Visible = true; 
                //groupBox2.Visible = true;
                bt_Cancelar.Visible = false;
                
                estado = 1;
            }
            else
            {
                GroupBox1.Visible = false;
                
                estado = 0;
            }
        }

        private void cmbcadastradoPor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtdtCadastro.Focus();

                try
                {

                    stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT Cod_usuario FROM usuario WHERE Nome_usuario='" + cmbcadastradoPor.Text + "'";

                    MySqlCommand myCmd = new MySqlCommand();
                    myCmd.Connection = Cmn;
                    myCmd.CommandText = stConsulta;
                    MySqlDataReader myReader = myCmd.ExecuteReader();

                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            myReader.Read();
                            txtCodUsuario.Text = myReader["Cod_usuario"] + Environment.NewLine;
                        }
                    }

                }
                catch
                {
                    MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                Cmn.Close();
            }
            else
            {
                cmbcadastradoPor.Focus();

            }
        }

        private void codigoUsuario()
        {

        }

        private void cmbFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                try
                {

                    stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT Cod_fornecedor FROM fornecedor WHERE Nome_fornecedor='" + cmbFornecedor.Text + "'";

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
                    Cmn.Close();
 

                }
                catch
                {
                    MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                               //btnNotaFiscal.Enabled = true;
                //btnNotaFiscal.Focus();

                if (txtCodigo.Text != "")
                {
                    Global.fornecedor.codfornecedor = txtCodFornecedor.Text;
                    Global.RI.cetil = txtCetil.Text;

                    rim_tem_fornecedores fornecedor = new rim_tem_fornecedores();
                    fornecedor.Show();
                }
                else
                {
                    MessageBox.Show("Clique em 'FILTROS' e depois opção 'ALTERAR' para escolher a RI para vincular fornecedor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                                
                //cmbSetor.Focus();
            }
            else
            {
                cmbFornecedor.Focus();

            }

        }

        private void cmbSetor_KeyPress(object sender, KeyPressEventArgs e)
        {/*
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                try
                {

                    stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT Cod_unidade FROM unidade WHERE Cod_unidade='" + cmbSetor.Text + "'";

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
                monthCalendar4.Visible = true;
                //txtdataEnvio.Focus();

            }
            else
            {
                //cmbSetor.Focus();

            }
          */
        }

        private void btnRetEspaços_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnRetEspaços_Click(object sender, EventArgs e)
        {
            cmbcadastradoPor.Items.Clear();
            cmbEscolha.Items.Clear();
            cmbFornecedor.Items.Clear();

            retiraEspaços();

            // POPULANDO TODOS ComboBox

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
                    cmbEscolha.Items.Add(unidade.Rows[i]["Nome_Unidade"]);
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

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

            //---------------------------------------------------------
            // populando cmbCadastradoPor

            mAdapter = new MySqlDataAdapter("SELECT * FROM usuario ORDER BY Nome_usuario", mConn);
            DataTable usuario = new DataTable();
            mAdapter.Fill(usuario);
            try
            {
                for (int i = 0; i < usuario.Rows.Count; i++)
                {
                    cmbcadastradoPor.Items.Add(usuario.Rows[i]["Nome_usuario"]);
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

            //---------------------------------------------------------

            mConn.Close();

        }

        private void rbdataCetil_CheckedChanged(object sender, EventArgs e)
        {
            //lblMsg.Text = "Requisições ordenadas por data do 'Cetil'";
            toolStripStatusMensagem.Text = "requisições ordenadas por data do 'Cetil'";

            mostrarResultados();
        }

        private void rbDescricao_CheckedChanged(object sender, EventArgs e)
        {
            //lblMsg.Text = "Requisições ordenadas por objeto.";
            toolStripStatusMensagem.Text = "requisições ordenadas por objeto";

            mostrarResultados();
        }

        private void chkCodigo_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        
        private void chkCetil_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkAF_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkObs_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkValorReal_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkProcesso_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkVlEstimado_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkDescricao_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkDotacao_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkFornecedor_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkNF_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void txtCheckIdentificação_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtdescricao_TextChanged_1(object sender, EventArgs e)
        {
            txtdescricao.Text = txtdescricao.Text.ToUpper();
        }

        private void txtdescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                
                               
                //if (radioButtonVeiculo.Checked == true)
                //{
                    //txtdescricao.Text = (txtdescricao.Text.ToUpper().Trim() + " para VEÍCULO " + txtMarca.Text + " " + txtModelo.Text
                    //+ "PLACA " + cmbPlaca.Text + " ANO " + txtAnoVeiculo.Text);
                    
                    //txtCetil.Text = Global.RI.cetil;
                    
                    txtProcesso.Focus();
                }
                else
                {
                    txtdescricao.Text.ToUpper();
                }

               // txtdescricao.Focus();
                
         }
            //else
            //{
             //   txtProcesso.Focus();
           // }

        
        private void btnImprimir_Click(object sender, EventArgs e)
        {
                
             SaveFileDialog salvar = new SaveFileDialog();// novo SaveFileDialog

             try
             {

                 Excel.Application App; // Aplicação Excel
                 Excel.Workbook WorkBook; // Pasta
                 Excel.Worksheet WorkSheet; // Planilha
                 object misValue = System.Reflection.Missing.Value;

                 App = new Excel.Application();
                 WorkBook = App.Workbooks.Add(misValue);
                 WorkSheet = (Excel.Worksheet)WorkBook.Worksheets.get_Item(1);
                 int i = 0;
                 int j = 0;

                 // passa as celulas do DataGridView para a Pasta do Excel
                 for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                 {
                     for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                     {
                         DataGridViewCell cell = dataGridView1[j, i];
                         WorkSheet.Cells[i + 1, j + 1] = cell.Value;
                     }
                 }

                 // define algumas propriedades da caixa salvar
                 salvar.Title = "Exportar para Excel";
                 salvar.Filter = "Arquivo do Excel *.xls | *.xls";
                 salvar.ShowDialog(); // mostra

                 // salva o arquivo
                 WorkBook.SaveAs(salvar.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue);
                 WorkBook.Close(true, misValue, misValue);
                 App.Quit(); // encerra o excel            

                 MessageBox.Show("Exportado com sucesso!","Informação");

             }
             catch 
             {

                 MessageBox.Show("Não foi possível a exportação", "Informação");
             
             }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void txtdescricao_KeyDown(object sender, KeyEventArgs e)
        {

        }

        
        private void txtAutorizacao_TextChanged(object sender, EventArgs e)
        {

        }

        private void monthCalendar4_DateChanged(object sender, DateRangeEventArgs e)
        {
            
        }

        private void monthCalendar3_DateChanged(object sender, DateRangeEventArgs e)
        {
            //txtdataNotaFiscal.Text = monthCalendar3.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;
        }

        private void monthCalendar1_DateSelected_1(object sender, DateRangeEventArgs e)
        {
            txtdataCetil.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            //txtDataAutorizacao.Text = monthCalendar2.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;
        }

        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            //txtdataNotaFiscal.Text = monthCalendar3.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;
        }

        private void monthCalendar4_DateSelected(object sender, DateRangeEventArgs e)
        {
            //txtdataEnvio.Text = monthCalendar4.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;
        }

        private void RIM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {

                Calendar.Visible = false;
                Calendar2.Visible = false;
                Calendar3.Visible = false;
                monthCalendar1.Visible = false;
                txtDataFinal.Text = "";

            }
            else
            {

            }
            
        }

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            if (txtDataInicial.Text == "")
                Calendar.Visible = true;
            else
                Calendar2.Visible = true;

        }

        private void Calendar_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {

                txtDataInicial.Text = Calendar.SelectionRange.Start.ToString("dd/MM/yyyy");
                Calendar.Visible = false;
                //PesquisaPorPeriodo(txtDataInicial.Text,txtDataFinal.Text);        

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        // Para ESSA CONSULTA FUNCIONAR DEVERMOS MUDAR AS DATAS NO BD PARA 'DATE' e não 'VARCHAR' COMO ESTA

        private void PesquisaPorPeriodo(string p, string p_2)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            //if (checkBox3.Checked == true)
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE DataCetil " + "BETWEEN " + p + "AND" + p_2, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "rim");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "rim";

        }


        private void txtvalorEstimado_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtvalorEstimado_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtvalorEstimado.Text != "")
                {
                    try
                    { // acrescentar o impedimento de entrar com valor não numérico

                        //string.format({0:n2}, valor)
                        txtvalorEstimado.Text = Convert.ToDecimal(txtvalorEstimado.Text).ToString();


                        //txtvalorEstimado.Text = Convert.ToDecimal(txtvalorEstimado.Text).ToString();

                        txtProcesso.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("Insira um valor válido.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtvalorEstimado.Text = "";
                        txtvalorEstimado.Focus();

                    }

                }
                else
                {
                }

            }
            else
            {

                txtvalorEstimado.Focus();

            }


        }

        private void txtdataCetil_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtdataCetil.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtdescricao.Focus();
                //dateString = txtdataCetil.Text;
                //txtDataCetilSQL.Text =  Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd");

            }
            else
            {
                txtdataCetil.Focus();

            }
        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDataOrdenador1_Click(object sender, EventArgs e)
        {

        }

        private void btnHabilitaSetores_Click_1(object sender, EventArgs e)
        {
            
            if (estado == 0)
            { 
               //MessageBox.Show("As alterações devem ser confirmadas antes de qualquer gravação. ", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupBox2.Enabled = true;
                estado = 1;
                toolStripStatusMensagem.Text = "trâmite Habilitado"; 
                checkBoxContab.Enabled = true;
                checkBoxCompras1.Enabled = true;
                checkBoxCompras2.Enabled = true;
                checkBoxDIPE.Enabled = true;
                checkBoxOrdenador1.Enabled = true;
                checkBoxOrdenador2.Enabled = true;

            }
            else
            {
                groupBox2.Enabled = false;
                estado = 0;
                toolStripStatusMensagem.Text = "trâmite desabilitado"; 
            }
        }

        private void btn_Despesa_Click(object sender, EventArgs e)
        {
            Dotacao despesa = new Dotacao();
            despesa.ShowDialog();
        }

        private void txtDO_TextChanged(object sender, EventArgs e)
        {
            txtCodDespesa.Text = "";
            toolStripStatusMensagem.Text = "";
        }

        private void txtCheckFornecedor_TextChanged(object sender, EventArgs e)
        {

        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
            chkCodigo.Checked = true;
            chkDescricao.Checked = true;
            chkDotacao.Checked = true;
            chkCetil.Checked = true;
            
            }
            else
            {
            chkCodigo.Checked = false;
            chkDescricao.Checked = false;
            chkDotacao.Checked = false;
            chkCetil.Checked = false;
                       
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                chkVlEstimado.Checked = true;
                chkValorReal.Checked = true;
                //chkDataCadastro.Checked = true;
                chkProc.Checked = true;
                chkProcessoContabil.Checked = true;

            }
            else
            {
                chkVlEstimado.Checked = false;
                chkValorReal.Checked = false;
                //chkDataCadastro.Checked = false;
                chkProc.Checked = false;
                chkProcessoContabil.Checked = false;
            }
           
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox3.Checked == true)
            {
                chkObs.Checked = true;
                chkTramite.Checked = true;
                chkCadastrante.Checked = true;
                chkDataCadastro.Checked = true;

                /*
                chkProcessoContabil.Checked = true;
                chkAcao.Checked = true;
                chkObs.Checked = true;
                chkEmpenho.Checked = true;
                chkTramite.Checked = true;
                chkDataCadastro.Checked = true;
                */
            }
            else
            {
                chkObs.Checked = false;
                chkTramite.Checked = false;
                chkCadastrante.Checked = false;
                chkDataCadastro.Checked = false;

                /*
                chkCadastrante.Checked = false;
                chkProcessoContabil.Checked = false;
                chkAcao.Checked = false;
                chkObs.Checked = false;
                chkEmpenho.Checked = false;
                chkTramite.Checked = false;
                chkDataCadastro.Checked = false;   
                 */
            }
        }

        private void chkDespesas_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                chkPrograma.Checked = true;
                chkReduzida.Checked = true;
                chkEmpenho.Checked = true;
                chkAcao.Checked = true;
                chkAF.Checked = true;
            }
            else
            {
                chkPrograma.Checked = false;
                chkReduzida.Checked = false;
                chkEmpenho.Checked = false;
                chkAcao.Checked = false;
                chkAF.Checked = false;             
            }
        }

        private void chkSetor_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkProcessoContabil_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();          
        }

        private void chkDataEmpenho_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void chkEmpenho_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkReduzida_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkPrograma_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void chkAcao_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkDespesa_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }      
        

        private void txtdataCetil_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotalReal_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void chkPlanilhaDespesas_CheckedChanged(object sender, EventArgs e)
        {
           
            /*
            mostrarResultados();
            
            if (chkPlanilhaDespesas.Checked == false)
            {
                checkBox4.Visible = false;
                chkPrograma.Visible = false;
                chkAcao.Visible = false;
                chkReduzida.Visible = false;
                chkEmpenho.Visible = false;
                chkAF.Visible = false;

                txtCheckFornecedor.Enabled = false;
                txtCheckPrograma.Enabled = false;
                txtCheckReduzida.Enabled = false;
                txtCheckAF.Enabled = false;
                txtCheckEmpenho.Enabled = false;
                txtCheckCodigoAplicacao.Enabled = false;

                lblDesdobrada.Visible = false;
                lblProgram.Visible = false;
                lblEmpenho.Visible = false;
                lblCodAplicacao.Visible = false;
                lblRed.Visible = false;
                lblAF.Visible = false;

                txtCheckDesdobrada.Visible = false;
                txtCheckPrograma.Visible = false;
                txtCheckEmpenho.Visible = false;
                txtCheckCodigoAplicacao.Visible = false;
                txtCheckReduzida.Visible = false;
                txtCheckAF.Visible = false;

                // código que permite manipular os dados do dataGridView novamente pois trata-se de uma planilha (RIM) física no banco de dados
                rbNovo.Enabled = true;
                rbAlterar.Enabled = true;
                rbExclui.Enabled = true;

                if (radioButtonRIM.Checked == true)
                    checkBoxRIM.Checked = true;
                else
                    checkBoxRRP.Checked = true;

                dataGridView1.Enabled = false;


            }
            else
            {
                checkBox4.Visible = true;
                chkPrograma.Visible = true;
                chkAcao.Visible = true;
                chkReduzida.Visible = true;
                chkEmpenho.Visible = true;
                chkAF.Visible = true;

                checkBox4.Enabled = true;
                chkPrograma.Enabled = true;
                chkAcao.Enabled = true;
                chkReduzida.Enabled = true;
                chkEmpenho.Enabled = true;
                chkAF.Enabled = true;

                lblDesdobrada.Visible = true;
                lblProgram.Visible = true;
                lblEmpenho.Visible = true;
                lblCodAplicacao.Visible = true;
                lblRed.Visible = true;
                lblAF.Visible = true;

                txtCheckDesdobrada.Visible = true;
                txtCheckPrograma.Visible = true;
                txtCheckEmpenho.Visible = true;
                txtCheckCodigoAplicacao.Visible = true;
                txtCheckReduzida.Visible = true;
                txtCheckAF.Visible = true;

                txtCheckDesdobrada.Enabled = true;
                txtCheckPrograma.Enabled = true;
                txtCheckEmpenho.Enabled = true;
                txtCheckCodigoAplicacao.Enabled = true;
                txtCheckReduzida.Enabled = true;
                txtCheckAF.Enabled = true;

                // código que impede de manipular os dados do dataGridView que mostra a planilha de depesas (é uma view (usada somete pra consulta) e não uma abela física)
                rbNovo.Enabled = false;
                rbAlterar.Enabled = false;
                rbExclui.Enabled = false; 
                
                checkBoxRIM.Checked = true;
                checkBoxRRP.Checked = true;

                dataGridView1.Enabled = false;
                                              
            }
             */
             
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void txtCheckPrograma_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCheckPrograma_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaPorPrograma(txtCheckPrograma.Text);
                LimpaFiltros();
            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }
        }

        private void PesquisaPorPrograma(string p)
        {
            /*
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            //if (checkBox3.Checked == true)
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Programa " + "LIKE " + "'%" + p + "%'", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "planilha_despesa");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "planilha_despesa";

            calculaQuantidadeRegistros();
             * */
                           
               mDataSet = new DataSet();
               mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
               mConn.Open();

               if (chkPlanilhaDespesas.Checked == false)
               {
                   //mostraChecks();

                   //cria um adapter utilizando a instrução SQL para acessar a tabela 

                   /*
                   if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº da despesa desdobrada vinculado à requisição";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Dotacao like '%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº da despesa desdobrada vinculado à requisição no período selecionado";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Dotacao '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                   }

                   //preenche o dataset através do adapter
                   mAdapter.Fill(mDataSet, "rim");

                   //atribui o resultado à propriedade DataSource da dataGridView
                   dataGridView1.DataSource = mDataSet;
                   dataGridView1.DataMember = "rim";
                    */
               }
               else
               {
                   if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº da despesa desdobrada vinculada à requisição";

                       mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Programa like" + "'%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº da despesa desdobrada vinculada à requisição no período selecionado";
                      // mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Programa like '%" + p + "%' and (dataCetil BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
                       mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Programa like '%" + p + "%' and (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ) ORDER BY Processo", mConn);
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

        private void txtCheckReduzida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                PesquisaReduzida(txtCheckReduzida.Text);
                LimpaFiltros();
            }
            else
            {
                // MessageBox.Show("Tecle 'ENTER'");

            }
            calculaQuantidadeRegistros();
        }

        private void PesquisaReduzida(string p)
        {
            /*
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            //if (checkBox3.Checked == true)
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE reduzida " + "LIKE " + "'%" + p + "%'", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "planilha_despesa");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "planilha_despesa";        
             */
                           
               mDataSet = new DataSet();
               mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
               mConn.Open();

               if (chkPlanilhaDespesas.Checked == false)
               {
                   /*

                   if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº da despesa reduzida vinculada à requisição";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Dotacao like '%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº da despesa reduzida vinculada à requisição no período selecionado";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Dotacao '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                   }

                   //preenche o dataset através do adapter
                   mAdapter.Fill(mDataSet, "rim");

                   //atribui o resultado à propriedade DataSource da dataGridView
                   dataGridView1.DataSource = mDataSet;
                   dataGridView1.DataMember = "rim";
                    * */
               } 
               else
               {
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

               }

               mConn.Close();
               calculaQuantidadeRegistros();

        }

        private void cmbEscolha_SelectedValueChanged(object sender, EventArgs e)
        {
            
            //-------- toda vez que escolhermos uma unidade o cmbPlaca deve ser esvaziado e depois populado
            // com somentes as placas cadastradas para aquela unidade.

            //Global.VEICULO.unidade = cmbEscolha.Text;
            

            for (int i = 0; i < cmbPlaca.Items.Count; i++)
            {
                cmbPlaca.Items.RemoveAt(i);

            }

                        
            // -------- recuperando o codigo da unidade escolhida no cmbUnidade -------------------------
                try
                {

                    stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT Cod_unidade FROM unidade WHERE Nome_unidade='" + cmbEscolha.Text + "'";

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
                    //lblMsg.Text = "Falha na conexão.";
                    toolStripStatusMensagem.Text = "houve falha na conexão";
                        
                }

                        Cmn.Close();


                        //codunidade = 0; // recebe provisoriamente o codigo da unidade
                        //codunidade = Convert.ToInt32(txtCodUnidade.Text);


                if (radioButtonVeiculo.Enabled == true)
                {
                    //populaCmbPlaca(codunidade);
                    populaCmbPlaca();
                    cmbPlaca.Focus();
                    //Global.VEICULO.unidade = txtSetorVeiculo.Text;

                    //recuperadadosveiculos(cmbPlaca.Text);
                }
                else
                {
                    txtdescricao.Focus();
                }
                         
            }

        // void populaCmbPlaca(int codunidade)

        public void populaCmbPlaca()
          {
            // populando cmbPlaca 
            //------------------------------------------------------
              for (int i = 0; i < cmbPlaca.Items.Count; i++)
              {
                  cmbPlaca.Items.RemoveAt(i);

              }


              if (cmbEscolha.Text == "DEFROTA")
              {
                  mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos where Setor_gestor='DEFROTA'", mConn);
              }
              else
              {
                  mAdapter = new MySqlDataAdapter("SELECT * FROM veiculos where Lotacao='" + cmbEscolha.Text + "'", mConn);
              }

            DataTable veiculos = new DataTable();
            mAdapter.Fill(veiculos);
            try
            {
                for (int i = 0; i < veiculos.Rows.Count; i++)
                {
                    cmbPlaca.Items.Add(veiculos.Rows[i]["placa"]);
                   
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

        }

        private void recuperadadosveiculos(string p)
        {
            // Método que recupera dados de veiculo cadastrado na unidade escolhida no cmbUnidade
            
            // DESPOPULANDO ComboBox placa: cmbPlaca
                for (int i = 0; i < cmbPlaca.Items.Count; i++){
                   cmbPlaca.Items.RemoveAt(i);
                }         
                
            //-----------------------------------------------------
            // POPULANDO ComboBox placa: cmbPlaca
                       
            mAdapter = new MySqlDataAdapter("SELECT placa FROM veiculos", mConn);
            DataTable veiculo = new DataTable();
            mAdapter.Fill(veiculo);
            try
            {
                for (int i = 0; i < veiculo.Rows.Count; i++)
                {
                    cmbPlaca.Items.Add(veiculo.Rows[i]["placa"]);

                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

            
           
        }

        private void cmbcadastradoPor_SelectedValueChanged(object sender, EventArgs e)
        {
            dTPDataCadastro.Visible = true;

            try
            {
                stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                stConsulta = "SELECT Cod_usuario FROM usuario WHERE Nome_usuario='" + cmbcadastradoPor.Text + "'";

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        txtCodUsuario.Text = myReader["Cod_usuario"] + Environment.NewLine;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            Cmn.Close();
            txtdtCadastro.Focus();
        }

        private void cmbFornecedor_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbSetor_SelectedValueChanged(object sender, EventArgs e)
        
        {
            
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

                }

                mostrarResultados();

            }
            else
            { 
            
            }

        }

        private void checkBoxRIM_CheckedChanged(object sender, EventArgs e)
        {
           /*
            temp = txtCheckCodigo.Text;
            codigoultimari = Convert.ToInt32(temp);
            PesquisaPorCodigo(codigoultimari);
             */                    
        }

        private void checkBoxRRP_CheckedChanged(object sender, EventArgs e)
        {
            /*
            temp = txtCheckCodigo.Text;
            codigoultimari = Convert.ToInt32(temp);
            PesquisaPorCodigo(codigoultimari);
              */      
        }

        
        private void radioButtonRRP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRRP.Checked == true)
            {
                txtProcessoContabil.Visible = true;
                label37.Visible = true;
                txtAnoProcessoContabil.Visible = true;
                textBox7.Visible = true;
            }
            else
            {
                txtProcessoContabil.Visible = false;
                label37.Visible = false;
                txtAnoProcessoContabil.Visible = false;
                textBox7.Visible = false;

            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void txtAnoValido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDataInicial_Click(object sender, EventArgs e)
        {
            Calendar.Visible = true;

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //data_valida = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy" );
            //txtdataCetil.Text = data_valida;
            
        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            
        }

        private void txtDO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)                               
            {
                    ///-----------------------------------------------
                    try
                    {
                        stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                        Cmn.ConnectionString = stConection;
                        Cmn.Open();

                        stConsulta = "SELECT Cod_Despesa,Despesa,Reduzida,Programa,Acao FROM dotacao WHERE Despesa='" + txtDO.Text + "'";

                        MySqlCommand myCmd = new MySqlCommand();
                        myCmd.Connection = Cmn;
                        myCmd.CommandText = stConsulta;
                        MySqlDataReader myReader = myCmd.ExecuteReader();

                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                myReader.Read();

                                txtCodDespesa.Text = myReader["Cod_Despesa"] + Environment.NewLine;
                                txtCodigoDespesa.Text = myReader["Despesa"] + Environment.NewLine;
                                txtReduzida.Text = myReader["Reduzida"] + Environment.NewLine;
                                txtPrograma.Text = myReader["Programa"] + Environment.NewLine;
                                txtAcao.Text = myReader["Acao"] + Environment.NewLine;
                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("ERRO.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //Cmn.Close();

                            Global.despesa.coddespesas = txtCodDespesa.Text;
                            Global.despesa.despesas = txtDO.Text;
                            //if (radioButtonRRP.Checked == true)
                              //  Global.NotaFiscal.codigoRI = txtCetil.Text + "00";
                            //else
                                Global.NotaFiscal.codigoRI = txtCetil.Text;
                                                        
                            //--------------------- Método que verifica se a despesa já está vinculada à RI que se está cadastrando----------

                            //verificaSeDespesaEstaVinculada();

                            //----------------------------------------------------------------------------------------------------------------
                            if (txtCodDespesa.Text == "")
                            {
                                toolStripStatusMensagem.Text = "despesa não cadastrada. Opção: clique no botão 'DESPESA' para cadastrá-la ou escolha outra válida";
                                MessageBox.Show("Escolha uma despesa válida.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDO.Focus();
                            }
                            else
                            {
                                if (txtCetil.Text == "")
                                {
                                    MessageBox.Show("Informe o número da requisição", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtCetil.Focus();
                                }
                                else
                                {
                                    rim_tem_despesa despesa = new rim_tem_despesa();
                                    despesa.ShowDialog();
                                }

                            }

                            txtvalorEstimado.Focus();
                            Cmn.Close();

                
                }
                else
                {
                  txtDO.Focus();
                }
                                    
        }

        private void Continua_cadastro()
        {
            
           DialogResult =  MessageBox.Show("Cadastrar mais despesas para essa RI ?", "Informação", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblInformacao.Text = "";
            dataGridView1.Enabled = false;
            bt_Gravar.Enabled = true;

            if (bt_Cancelar.Visible == true)
            {
                bt_Cancelar.Visible = false;
            }
            else { }


                if (rbAlterar.Checked == true)
                    toolStripStatusMensagem.Text = "módulo de ALTERAÇÂO ativado. Altere e clique botão 'OK' para efetivar ou 'Filtros' para voltar";
                else
                    if (rbExclui.Checked == true)
                        toolStripStatusMensagem.Text = "módulo de EXCLUSÃO ativado. Confira os dados, clique botão 'OK' para efetivar ou 'Filtros' para voltar";
                
    

            // nesse caso trata-se de apenas uma consulta e tudo fica desabilitado senão habilita os objetos que interessam
            // à inclusão, alteração e exclusão...Na verdade essa situação não mais existe no caso de entrar na tela de inclusão,
            // alteração e exclusão primeiro. Essa situação era para quando a tela de filtros abria em primeiro plano.
            
            if ((rbNovo.Checked == false) && (rbAlterar.Checked == false) && (rbExclui.Checked == false))
            
            {
                /*
                bt_Cancelar.Visible = false;
                groupBox5.Visible = false;
                btnVoltar.Visible = true;
                dataGridView1.Visible = false;
                btnCancelar.Visible = true;
                label28.Visible = false;
                GroupBox1.Visible = true; 
                groupBox2.Visible = true;
                bt_Cancelar.Visible = false;
                GroupBox1.Enabled = false;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                
                lblModuloVisualização.Visible = true;
                lblModuloVisualização.Text = "Módulo Consulta";

                //DesabilitaRadioButtons();
                bt_Gravar.Enabled = false;
                
                cmbcadastradoPor.Text = Global.Logon.usuario;

                txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                cmbEscolha.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtdescricao.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtDO.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "RIM")
                {
                    radioButtonRIM.Checked = true;
                }
                else
                {
                    radioButtonRRP.Checked = true;
                }

                txtCetil.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtdataCetil.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtvalorEstimado.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtvalorReal.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtProcesso.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                               
                txtProcessoContabil.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                //txtAutorizacao.Text = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                //txtDataAutorizacao.Text = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                //cmbSetor.Text = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                //txtdataEnvio.Text = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                
                lblDataContabilidade.Text = dataGridView1[16, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                lblDataOrdenador1.Text = dataGridView1[17, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                lblDataCompras1.Text = dataGridView1[18, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                lblDataOrdenador2.Text = dataGridView1[19, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                lblDataCompras2.Text = dataGridView1[20, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                lblDataDipe.Text = dataGridView1[21, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                cmbcadastradoPor.Text = dataGridView1[22, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtdtCadastro.Text = dataGridView1[23, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                cmbFornecedor.Text = dataGridView1[24, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                //txtnotaFiscal.Text = dataGridView1[25, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                //txtdataNotaFiscal.Text = dataGridView1[26, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                txtObs.Text = dataGridView1[27, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtReduzida.Text = dataGridView1[32, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtPrograma.Text = dataGridView1[33, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtAcao.Text = dataGridView1[34, dataGridView1.CurrentCellAddress.Y].Value.ToString();


                ///////////////////////////////////////////////////////////////////////////////////////////////////
                */
            }
            else
            {

                lblModuloVisualização.Visible = false; 
                
                dataGridView1.Visible = false;
                btnCancelar.Visible = true;
                label28.Visible = false;
                GroupBox1.Visible = true; 
                groupBox2.Visible = true;
                bt_Cancelar.Visible = false;
                GroupBox1.Enabled = true;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
              
                
                //DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                //bt_Gravar.Enabled = false;
                
                cmbcadastradoPor.Text = Global.Logon.usuario;                
                
                                
                txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                Global.RI.codcetil = txtCodigo.Text;
                cmbEscolha.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtdescricao.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtDO.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "RIM")
                { radioButtonRIM.Checked = true;
                                    }
                else {
                    radioButtonRRP.Checked = true;
                }
                
                txtCetil.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtdataCetil.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();

                txtvalorEstimado.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtvalorReal.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtProcesso.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtAnoProcesso.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtProcessoContabil.Text = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtAnoProcessoContabil.Text = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                               
                lblDataContabilidade.Text = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                lblDataOrdenador1.Text = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                lblDataCompras1.Text = dataGridView1[16, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                lblDataOrdenador2.Text = dataGridView1[17, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                lblDataCompras2.Text = dataGridView1[18, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                lblDataDipe.Text = dataGridView1[19, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                
                cmbcadastradoPor.Text = dataGridView1[20, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                txtdtCadastro.Text = dataGridView1[21, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                
                txtObs.Text = dataGridView1[22, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                
                codigoFornecedor();

                cmbEscolha.Focus();
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            // desmarca radio buttons
            //lblMsg.Text = "Ação anterior cancelada";
            toolStripStatusMensagem.Text = "ação anterior cancelada";
                        

            if (rbNovo.Checked == true)
                rbNovo.Checked = false;
            else
                if (rbExclui.Checked == true)
                    rbExclui.Checked = false;
                else
                    rbAlterar.Checked = false;
            
            rbExclui.Enabled = true;
            rbNovo.Enabled = true;
            rbAlterar.Enabled = true;


            //rbExclui.Checked = false;
            //rbNovo.Checked = false;
            //rbAlterar.Checked = false;

            GroupBox1.Enabled = false;

            bt_Gravar.Enabled = false;
            // btnAtualizar.Enabled = false;

            dataGridView1.Visible = true;
            btnCancelar.Visible = false;
            label28.Visible = true;
            groupBox3.Visible = true;
            groupBox4.Visible = true;
           
            GroupBox1.Visible = false;
           
            textBox1.Visible = false;
            label30.Text = "";
           ;
            btnVoltar.Visible = false;
            groupBox5.Visible = true;
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnNotaFiscal_Click(object sender, EventArgs e)
        {
            codigoFornecedor();

            if (cmbFornecedor.Text != "" && txtCodigo.Text !="")
            {
                Global.NotaFiscal.codigoRI = txtCodigo.Text;
                Global.NotaFiscal.fornecedor = txtCodFornecedor.Text;
                Global.NotaFiscal.nomefornecedor = cmbFornecedor.Text;
                NotaFiscal notafiscal = new NotaFiscal();
                notafiscal.Show();
            }
            else 
            {
                MessageBox.Show("Escolha o Fornecedor vinculado à Requisição", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            
            }
            
        }

        private void codigoFornecedor()
        {
                try
                {

                    stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT Cod_fornecedor FROM fornecedor WHERE Nome_fornecedor='" + cmbFornecedor.Text + "'";

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
                    //lblMsg.Text = "Código do fornecedor não localizado. Conexão falhou.";
                    toolStripStatusMensagem.Text = "código do fornecedor não localizado";    
                }
                Cmn.Close();
                
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label36_Click_1(object sender, EventArgs e)
        {

        }

        private void txtTotalReal_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void lblTotalReal_Click(object sender, EventArgs e)
        {

        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Enabled == false) {
                dataGridView1.Enabled = true;
            }else{
                dataGridView1.Enabled=false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //lblMsg.Text = "Somando o valor da despesas das requisição. filtradas";
            toolStripStatusMensagem.Text = "captura o valor real das despesas na planilha abaixo.";
                        
            Double ValorTotal1 = 0;
            // Double valorTotalEstimado = 0;

            // lblTotalEstimado.Visible = true;
            lblTotalReal.Visible = true;
            txtTotalReal.Visible = true;
            // txtTotalEstimado.Visible = true;

            if (chkPlanilhaDespesas.Checked == false)
            {

                try
                {
                    foreach (DataGridViewRow col in dataGridView1.Rows)
                    {
                        ValorTotal1 = ValorTotal1 + Convert.ToDouble(col.Cells[9].Value);

                        //valorTotalEstimado = valorTotalEstimado + Convert.ToDouble(col.Cells[9].Value);

                    }

                    txtTotalReal.Text = ValorTotal1.ToString("C");
                    //txtTotalEstimado.Text = valorTotalEstimado.ToString("C");
                }
                catch
                {

                    MessageBox.Show("Erro na soma. Há valores inconsistentes nas requisições [coluna valor real].", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblTotalReal.Visible = false;
                    txtTotalReal.Visible = false;
                }
            }
            else
            { 
                  try
                {
                    foreach (DataGridViewRow col in dataGridView1.Rows)
                    {
                        ValorTotal1 = ValorTotal1 + Convert.ToDouble(col.Cells[13].Value); // se a  Planilha de despesas estiver selecionada a coluna para soma se altera

                        //valorTotalEstimado = valorTotalEstimado + Convert.ToDouble(col.Cells[9].Value);

                    }

                    txtTotalReal.Text = ValorTotal1.ToString("C");
                    //txtTotalEstimado.Text = valorTotalEstimado.ToString("C");
                }
                catch
                {

                    MessageBox.Show("Erro na soma. Há valores inconsistentes na Planilha de Despesas [coluna valor real].", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblTotalReal.Visible = false;
                    txtTotalReal.Visible = false;
                }
            
            
            }

        }

        private void btnEmpenho_Click(object sender, EventArgs e)
        {
                   
            try
            {
                stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                stConsulta = "SELECT Cod_Despesa FROM dotacao WHERE Despesa=" + txtDO.Text;

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        txtCodDespesa.Text = myReader["Cod_Despesa"] + Environment.NewLine;
                    }
                }

                Cmn.Close();
            }
            catch
            {
                MessageBox.Show("Verifique se a despesa está cadastrada.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cmn.Close();
            }

            //}  

            Global.despesa.coddespesas = txtCodDespesa.Text;
            Global.despesa.despesas = txtDO.Text;
            Global.NotaFiscal.codigoRI = txtCetil.Text;
            //Global.RI.cetil = txtCodigo.Text;   // 
            rim_tem_despesa despesa = new rim_tem_despesa();
            despesa.ShowDialog();
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void rbCetil_CheckedChanged(object sender, EventArgs e)
        {
            //lblMsg.Text = "Requisições ordenadas por código 'Cetil'.";
            toolStripStatusMensagem.Text = "requisições ordenadas por código 'Cetil'";

            mostrarResultados();
        }

        private void cmbEscolha_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < cmbPlaca.Items.Count; i++)
            {
                cmbPlaca.Items.RemoveAt(i);
            }


            // isso será feito no metodo recupera dados veiculos


            // -------- Obtendo codigo da unidade escolhida

            try
            {

                stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                stConsulta = "SELECT Cod_unidade FROM unidade WHERE Nome_unidade='" + cmbEscolha.Text + "'";

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

                MessageBox.Show("Unidade não cadastrada.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //lblMsg.Text = "Falha na conexão.";
                toolStripStatusMensagem.Text = "houve falha na conexão";

            }

            Cmn.Close();

            txtdescricao.Focus();

            if (radioButtonVeiculo.Enabled == true)
            {
                //populaCmbPlaca(codunidade);
                populaCmbPlaca();
                cmbPlaca.Focus();

            }
            else
            {
                txtdescricao.Focus();
            }
            

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label36_Click_2(object sender, EventArgs e)
        {

        }


        private void cmbPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                try
                {
                    stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();
                    // ----------------------- carrega dados do veiculo nos respectivos text ------------------------------

                    stConsulta = "SELECT * FROM veiculos WHERE placa='" + cmbPlaca.Text + "'";

                    MySqlCommand myCmd = new MySqlCommand();
                    myCmd.Connection = Cmn;
                    myCmd.CommandText = stConsulta;
                    MySqlDataReader myReader = myCmd.ExecuteReader();

                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            myReader.Read();
                            txtSetorVeiculo.Text = myReader["setor_gestor"] + Environment.NewLine;
                            txtMarca.Text = myReader["marca"] + Environment.NewLine;
                            txtModelo.Text = myReader["modelo"] + Environment.NewLine;
                            txtAnoVeiculo.Text = myReader["ano"] + Environment.NewLine;
                            txtCodVeiculo.Text = myReader["Cod_seq_veiculo"] + Environment.NewLine;
                        }
                    }

                    Global.Veiculos.placa = cmbPlaca.Text;
                    Global.Veiculos.codPlaca = txtCodVeiculo.Text;
                    Global.Veiculos.marca = txtMarca.Text;
                    Global.Veiculos.modelo = txtModelo.Text;
                    Global.Veiculos.ano = txtAnoVeiculo.Text;
                    Global.Veiculos.unidade = txtSetorVeiculo.Text;

                    if (txtCetil.Text == "")
                    {
                        MessageBox.Show("Campo 'CETIL' vazio. Informar o nº da Requisição.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCetil.Focus();

                    }
                    else
                    {
                        Global.RI.cetil = txtCetil.Text;

                        rim_tem_veiculos rimveiculo = new rim_tem_veiculos();
                        rimveiculo.Show();
                        //txtdescricao.Focus();

                    }


                    Cmn.Close();

                }
                catch
                {
                    MessageBox.Show("SELECT * FROM veiculos WHERE placa='" + cmbPlaca.Text, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void cmbPlaca_SelectedValueChanged(object sender, EventArgs e)
        {                        
            try
            {
                stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();
                // ----------------------- carrega dados do veiculo nos respectivos text ------------------------------

                stConsulta = "SELECT * FROM veiculos WHERE placa='" + cmbPlaca.Text + "'";

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        txtSetorVeiculo.Text = myReader["lotacao"] + Environment.NewLine;
                        txtMarca.Text = myReader["marca"] + Environment.NewLine;
                        txtModelo.Text = myReader["modelo"] + Environment.NewLine;
                        txtAnoVeiculo.Text = myReader["ano"] + Environment.NewLine;
                        txtCodVeiculo.Text = myReader["Cod_seq_veiculo"] + Environment.NewLine;
                    }
                }

                Global.Veiculos.placa = cmbPlaca.Text;
                Global.Veiculos.codPlaca = txtCodVeiculo.Text;
                Global.Veiculos.marca = txtMarca.Text;
                Global.Veiculos.modelo = txtModelo.Text;
                Global.Veiculos.ano = txtAnoVeiculo.Text;
                Global.Veiculos.unidade = txtSetorVeiculo.Text;

                if (txtCetil.Text == "")
                {
                    MessageBox.Show("Campo 'CETIL' vazio. Informar o nº da Requisição.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCetil.Focus();

                }
                else
                {
                    Global.RI.cetil = txtCetil.Text;

                    rim_tem_veiculos rimveiculo = new rim_tem_veiculos();
                    rimveiculo.Show();
                    //txtdescricao.Focus();
                    
                }

                txtVerificaVeiculo.Text = Global.Veiculos.quantPlaca;

                Cmn.Close();

            }
            catch
            {
                MessageBox.Show("SELECT * FROM veiculos WHERE placa='" + cmbPlaca.Text, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtdescricao.Text = txtdescricao.Text + cmbPlaca.Text + " - ";  

        }

        private void lblMarca_Click(object sender, EventArgs e)
        {

        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblModelo_Click(object sender, EventArgs e)
        {

        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblAnoVeiculo_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter_1(object sender, EventArgs e)
        {

        }

        private void txtCodUnidade_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtDO_CursorChanged(object sender, EventArgs e)
        {
            
        }

        private void txtCheckPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
             {
                PesquisaPlaca(txtCheckPlaca.Text);
                LimpaFiltros();
             }
            else
             {
                // MessageBox.Show("Tecle 'ENTER'");
             }
        }

        private void PesquisaPlaca(string p)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            //if (checkBox3.Checked == true)
            // ordena a tabela de acordo com o critério estabelecido
            //mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE CodReduzida " + "LIKE " + "'%" + p + "%'", mConn);
            mAdapter = new MySqlDataAdapter("SELECT cod_rim as Rim,cod_seq_veiculo as Codigo_Veiculo FROM rim_has_veiculo WHERE Cod_seq_Veiculo=(SELECT Cod_seq_Veiculo FROM veiculo Where placa='"  + txtCheckPlaca.Text + "')", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "rim_has_veiculo");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "rim_has_veiculo";
        }

        private void toolStripStatusMensagem_Click(object sender, EventArgs e)
        {

        }

        private void chkTramite_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void bt_Cancelar_Click(object sender, EventArgs e)
        {
           

            if (rbExclui.Checked == true || rbAlterar.Checked == true || rbNovo.Checked==true)
            {
                rbExclui.Checked = false;
                rbAlterar.Checked = false;
                rbNovo.Checked = false;
                dataGridView1.Enabled = false;
                rbNovo.Enabled = true;
                rbAlterar.Enabled = true;
                rbExclui.Enabled = true;
                toolStripStatusMensagem.Text = "você pode selecionar 'Incluir', 'Excluir' ou 'Atualizar' requisições";
                
            }
            else
            {
                
            }

            if (rbExclui.Checked == false && rbAlterar.Checked == false && rbNovo.Checked == false)
            {
                bt_Cancelar.Enabled = false;
                toolStripStatusMensagem.Text = "você pode selecionar 'Incluir', 'Excluir' ou 'Atualizar' requisições";
                lblInformacao.Text = "";

            }
            else
            {

            } 
          
               
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void txtdtCadastro_TextChanged(object sender, EventArgs e)
        {

        }

        private void label32_MouseHover(object sender, EventArgs e)
        {
            label32.ForeColor = Color.Red;

        }

        private void label32_MouseLeave(object sender, EventArgs e)
        {
            label32.ForeColor = Color.Black;

        }

        private void label32_MouseClick(object sender, MouseEventArgs e)
        {
            Calendar.Visible = true;
        }

        private void label33_MouseHover(object sender, EventArgs e)
        {
            label33.ForeColor = Color.Red;
        }

        private void label33_MouseLeave(object sender, EventArgs e)
        {
            label33.ForeColor = Color.Black;
        }

        private void label33_MouseClick(object sender, MouseEventArgs e)
        {
            Calendar2.Visible = true;
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

        private void monthCalendar1_DateSelected_2(object sender, DateRangeEventArgs e)
        {
            txtdataCetil.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;
            cmbEscolha.Focus();
        }

        private void monthCalendar5_DateSelected(object sender, DateRangeEventArgs e)
        {
            //txtDataEmpenho.Text = monthCalendar5.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;
        }

        private void monthCalendar2_DateSelected_1(object sender, DateRangeEventArgs e)
        {
            //txtDataAutorizacao.Text = monthCalendar2.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;
        }

        private void monthCalendar6_DateSelected(object sender, DateRangeEventArgs e)
        {
            //txtDataEmpenho.Text = monthCalendar6.SelectionRange.Start.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;        
        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void Calendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDataFinal.Text = Calendar2.SelectionRange.Start.ToString("dd/MM/yyyy");
            Calendar2.Visible = false;
            verificaValidadeData();
        }

        private void txtCodDespesa_TextChanged(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged_1(object sender, DateRangeEventArgs e)
        {

        }

        private void monthCalendar5_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void monthCalendar6_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void lblCodDespesa_Click(object sender, EventArgs e)
        {

        }

        private void txtCodigoDespesa_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblReduzida_Click(object sender, EventArgs e)
        {

        }

        private void txtReduzida_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnVeiculos_Click(object sender, EventArgs e)
        {
            Veiculos veiculos = new Veiculos();
            veiculos.Show();

        }

        private void monthCalendar3_DateChanged_1(object sender, DateRangeEventArgs e)
        {

        }

        private void cmbSetor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                stConsulta = "SELECT Cod_fornecedor FROM fornecedor WHERE Nome_fornecedor='" + cmbFornecedor.Text + "'";

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

                if (txtCodFornecedor.Text != "")
                {
                    Global.fornecedor.codfornecedor = txtCodFornecedor.Text;
                    Global.RI.cetil = txtCetil.Text;

                    /*
                    rim_tem_fornecedores fornecedor = new rim_tem_fornecedores();
                    fornecedor.Show();
                     */
                }
                else 
                {
                    MessageBox.Show("Clique em 'FILTROS' e depois opção 'ALTERAR' para escolher a RI para vincular fornecedor", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
                }
                
            }
            catch
            {
                MessageBox.Show("Não foi possível fazer conexão.", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            Cmn.Close();
            //btnNotaFiscal.Enabled = true;
            //btnNotaFiscal.Focus();

            //cmbSetor.Focus();

            
                        
            
        }

        private void Label13_Click(object sender, EventArgs e)
        {

        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void cmbcadastradoPor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void cmbEscolha_Leave(object sender, EventArgs e)
        {
            cmbEscolha.BackColor = Color.White;
        }

        private void cmbEscolha_Enter(object sender, EventArgs e)
        {
            cmbEscolha.BackColor = Color.Yellow;
            txtCodUnidade.Text = "";

            for (int i = 0; i < cmbPlaca.Items.Count; i++)
            {
                cmbPlaca.Items.RemoveAt(i);

            }

            cmbPlaca.Items.Clear();

        }

        private void lblCodFornecedor_Click(object sender, EventArgs e)
        {

        }

        private void RIM_Enter(object sender, EventArgs e)
        {


        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            cmbcadastradoPor.Items.Clear();
            cmbEscolha.Items.Clear();
            cmbFornecedor.Items.Clear();
            cmbPlaca.Items.Clear();

            retiraEspaços();

            // POPULANDO TODOS ComboBox

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
                    cmbEscolha.Items.Add(unidade.Rows[i]["Nome_Unidade"]);
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

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

            //---------------------------------------------------------
            // populando cmbCadastradoPor

            mAdapter = new MySqlDataAdapter("SELECT * FROM usuario ORDER BY Nome_usuario", mConn);
            DataTable usuario = new DataTable();
            mAdapter.Fill(usuario);
            try
            {
                for (int i = 0; i < usuario.Rows.Count; i++)
                {
                    cmbcadastradoPor.Items.Add(usuario.Rows[i]["Nome_usuario"]);
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

            //---------------------------------------------------------

            mConn.Close();

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.Yellow;
        }

        private void txtCheckDescricao_Enter(object sender, EventArgs e)
        {
            txtCheckDescricao.BackColor = Color.Yellow;
            toolStripStatusMensagem.Text = "pesquisa por descrição";
        }

        private void txtCheckIdentificação_Enter(object sender, EventArgs e)
        {
            txtCheckIdentificação.BackColor = Color.Yellow;
            toolStripStatusMensagem.Text = "pesquisa por Unidade/Setor solicitante";

        }

        private void txtCheckAF_Enter(object sender, EventArgs e)
        {
            txtCheckAF.BackColor = Color.Yellow;

            toolStripStatusMensagem.Text = "pesquisa por número da AF gerada";
        }

        private void txtCheckDataCetil_Enter(object sender, EventArgs e)
        {
            txtCheckDataCetil.BackColor = Color.Yellow;
            toolStripStatusMensagem.Text = "pesquisa pela data da solicitação";
        }

        private void txtCheckFornecedor_Enter(object sender, EventArgs e)
        {
            txtCheckFornecedor.BackColor = Color.Yellow;
        }

        private void txtCheckCetil_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCheckCetil_Enter(object sender, EventArgs e)
        {
            txtCheckCetil.BackColor = Color.Yellow;
            toolStripStatusMensagem.Text = "pesquisa por código CETIL";

        }

        private void txtCheckPrograma_Enter(object sender, EventArgs e)
        {
            txtCheckPrograma.BackColor = Color.Yellow;

            toolStripStatusMensagem.Text = "pesquisa por programa indicado";

        }

        private void txtCheckReduzida_Enter(object sender, EventArgs e)
        {
            txtCheckReduzida.BackColor = Color.Yellow;

            toolStripStatusMensagem.Text = "pesquisa por código reduzido da despesa";

        }

           private void txtCheckPlaca_Enter(object sender, EventArgs e)
        {
            txtCheckPlaca.BackColor = Color.Yellow;

        }

           private void textBox1_Leave(object sender, EventArgs e)
           {
               textBox1.BackColor = Color.White;

           }

           private void txtCheckDescricao_Leave(object sender, EventArgs e)
           {
               txtCheckDescricao.BackColor = Color.White;
           }

           private void txtCheckIdentificação_Leave(object sender, EventArgs e)
           {
               txtCheckIdentificação.BackColor = Color.White;
           }

           private void txtCheckAF_TextChanged(object sender, EventArgs e)
           {
               
           }

           private void txtCheckAF_Leave(object sender, EventArgs e)
           {
               txtCheckAF.BackColor = Color.White;
           }

           private void txtCheckDataCetil_Leave(object sender, EventArgs e)
           {
               txtCheckDataCetil.BackColor = Color.White;
           }

           private void txtCheckFornecedor_Leave(object sender, EventArgs e)
           {
               txtCheckFornecedor.BackColor = Color.White;
           }

           private void txtCheckCetil_Leave(object sender, EventArgs e)
           {
               txtCheckCetil.BackColor = Color.White;
           }

           private void txtCheckProcesso_Leave(object sender, EventArgs e)
           {
               txtCheckProcesso.BackColor = Color.White;
           }

           private void txtCheckPrograma_Leave(object sender, EventArgs e)
           {
               txtCheckPrograma.BackColor = Color.White;
           }

           private void txtCheckReduzida_Leave(object sender, EventArgs e)
           {
               txtCheckReduzida.BackColor = Color.White;
           }

           private void txtCheckPlaca_Leave(object sender, EventArgs e)
           {
               txtCheckPlaca.BackColor = Color.White;
           }

           private void txtCheckCodigo_TextChanged(object sender, EventArgs e)
           {

           }

           private void txtCheckCodigo_Enter(object sender, EventArgs e)
           {
               txtCheckCodigo.BackColor = Color.Yellow;
               toolStripStatusMensagem.Text = "pesquisa por código da Requisição";
           }

           private void txtCheckCodigo_Leave(object sender, EventArgs e)
           {
               txtCheckCodigo.BackColor = Color.White;
           }

           private void txtCheckProcesso_Enter(object sender, EventArgs e)
           {
               txtCheckProcesso.BackColor = Color.Yellow;
               toolStripStatusMensagem.Text = "pesquisa por código gerado do processo";
           }

           private void lblModuloVisualização_Click(object sender, EventArgs e)
           {

           }

           private void GroupBox1_Enter_1(object sender, EventArgs e)
           {
               txtVerificaVeiculo.Text = Global.Veiculos.quantPlaca;
           }

           private void txtCetil_KeyPress(object sender, KeyPressEventArgs e)
           {
               // Devemos analisar se para determinada requisição foi vinculada uma placa de veículo caso contrário não podemos 
               // prosseguir com o cadastramento da requisição.

               if (e.KeyChar == 13) //Se for Enter executa a validação
               {
                   //txtCetil.Text = Global.RI.cetil;

                   if (txtCetil.Text == "")
                   {

                       toolStripStatusMensagem.Text = "informar o número da requisição";
                       txtCetil.Focus();

                   }
                   else
                   {
                       monthCalendar1.Visible = true;
                       Global.NotaFiscal.codigoRI = txtCetil.Text;
                   }
               }
               else
               {
                   //cmbEscolha.Focus();

               }
           }

           private void txtdescricao_Leave(object sender, EventArgs e)
           {
               txtdescricao.BackColor = Color.White;
               txtdescricao.Text = txtdescricao.Text.ToUpper();
               txtdescricao.Text = txtdescricao.Text.Trim();
           }

           private void txtCetil_Enter(object sender, EventArgs e)
           {
               txtCetil.BackColor = Color.Yellow;
           }

           private void txtCetil_Leave(object sender, EventArgs e)
           {
               txtCetil.BackColor = Color.White;
           }

           private void txtdataCetil_Enter(object sender, EventArgs e)
           {

           }

           private void cmbPlaca_Enter(object sender, EventArgs e)
           {
               cmbPlaca.BackColor = Color.Yellow;
           }

           private void cmbPlaca_Leave(object sender, EventArgs e)
           {
               cmbPlaca.BackColor = Color.White;
           }

           private void txtdescricao_Enter(object sender, EventArgs e)
           {
               txtdescricao.BackColor = Color.Yellow;
           }

           private void txtProcesso_Enter(object sender, EventArgs e)
           {
               txtProcesso.BackColor = Color.Yellow;
           }

           private void txtProcesso_Leave(object sender, EventArgs e)
           {
               txtProcesso.BackColor = Color.White;
           }

           private void txtDO_Enter(object sender, EventArgs e)
           {
               txtDO.BackColor = Color.Yellow;
           }

           private void txtDO_Leave(object sender, EventArgs e)
           {
               txtDO.BackColor = Color.White;
                
               /*
                try
                        {
                            stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                            Cmn.ConnectionString = stConection;
                            Cmn.Open();

                            stConsulta = "SELECT Despesa,Reduzida,Programa,Acao FROM dotacao WHERE Despesa='" + txtDO.Text + "'";

                            MySqlCommand myCmd = new MySqlCommand();
                            myCmd.Connection = Cmn;
                            myCmd.CommandText = stConsulta;
                            MySqlDataReader myReader = myCmd.ExecuteReader();

                            if (myReader.HasRows)
                            {
                                while (myReader.Read())
                                {
                                    myReader.Read();
                                    txtCodigoDespesa.Text = myReader["Despesa"] + Environment.NewLine;
                                    txtReduzida.Text = myReader["Reduzida"] + Environment.NewLine;
                                    txtPrograma.Text = myReader["Programa"] + Environment.NewLine;
                                    txtAcao.Text = myReader["Acao"] + Environment.NewLine;
                                }
                            }

                        }
                        catch
                        {
                            MessageBox.Show("Despesa não cadastrada ou inválida.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    
                        Cmn.Close();

                    
                        {
                            try
                            {
                                stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                                Cmn.ConnectionString = stConection;
                                Cmn.Open();

                                stConsulta = "SELECT Cod_Despesa FROM dotacao WHERE Despesa=" + txtDO.Text;

                                MySqlCommand myCmd = new MySqlCommand();
                                myCmd.Connection = Cmn;
                                myCmd.CommandText = stConsulta;
                                MySqlDataReader myReader = myCmd.ExecuteReader();

                                if (myReader.HasRows)
                                {
                                    while (myReader.Read())
                                    {
                                        myReader.Read();

                                        txtCodDespesa.Text = myReader["Cod_Despesa"] + Environment.NewLine;
                                                                                
                                    }
                                }
                                //---------------------

                                Global.despesa.coddespesas = txtCodDespesa.Text;
                                Global.despesa.despesas = txtDO.Text;
                                Global.NotaFiscal.codigoRI = txtCetil.Text;
                            

                                if (txtCodDespesa.Text == "")
                                {
                                    toolStripStatusMensagem.Text = "despesa não cadastrada. Opção: clique no botão 'DESPESA' para cadastrá-la ou escolha outra válida";
                                    MessageBox.Show("Verifique se a despesa está cadastrada.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtDO.Focus();
                                }
                                else
                                {
                                    if (txtCetil.Text == "")
                                    {
                                        MessageBox.Show("Informe o número da requisição", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtCetil.Focus();
                                    }
                                    else
                                    {
                                        rim_tem_despesa despesa = new rim_tem_despesa();
                                        despesa.ShowDialog();
                                    }

                                }

                                Cmn.Close();
                            }
                            catch
                            {
                                //---------------------------
                                txtCodDespesa.Text = "";
                                txtDO.Text = "";
                                txtDO.Focus();
                                //---------------------------
                                
                                Cmn.Close();
                            }
                        }
                */
                        
           }
            
            
           private void verificaSeDespesaEstaVinculada()
           {
               //verifica se a despesa digitada está vinculada à RI corrente e dá um alerta se não estiver.
               string teste="";

               // a conexão está aberta não é necessário criar outra conexão
               // stConection = "Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
               // Cmn.ConnectionString = stConection;
               // Cmn.Open();
                
               try
                {

                    //stConsulta = "SELECT Co d_unidade FROM unidade WHERE Cod_unidade='" + cmbSetor.Text + "'";
                    stConsulta = "SELECT Cod_rim,Cod_despesa FROM rim_has_dotacao Where Cod_rim=" + Convert.ToInt32(txtCetil.Text) + " and Cod_despesa=" + Convert.ToInt32(txtCodDespesa.Text);

                    MySqlCommand myCmd = new MySqlCommand();
                    myCmd.Connection = Cmn;
                    myCmd.CommandText = stConsulta;
                    MySqlDataReader myReader = myCmd.ExecuteReader();

                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            myReader.Read();
                            teste = myReader["Cod_despesa"] + Environment.NewLine;
                            
                        }
                    }

                    
                }
                catch 
                {
                    MessageBox.Show("ERRO", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                }

                if (teste != "")
                {
                    //txtvalorEstimado.Focus();
                    MessageBox.Show("Despesa " + txtDO.Text + " já vinculada a essa R.I" + txtCetil.Text, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                   
                }
                else 
                {
                    //MessageBox.Show("Despesa " + txtDO.Text + " ainda não vinculada requisição " + txtCetil.Text, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //txtDO.Focus();
                    txtvalorEstimado.Focus();

                }

               //Cmn.Close();
                                          

           }

           private void txtvalorEstimado_Enter(object sender, EventArgs e)
           {
               txtvalorEstimado.BackColor = Color.Yellow;
           }

           private void txtvalorEstimado_Leave(object sender, EventArgs e)
           {
               txtvalorEstimado.BackColor = Color.White;
               if (txtvalorEstimado.Text != "")
               {
                   try
                   {
                       // desse jeito funicona até o impedimento de entrar com valor não numérico
                       txtvalorEstimado.Text = Convert.ToDecimal(txtvalorEstimado.Text).ToString("C");
                       txtvalorEstimado.Text = txtvalorEstimado.Text.Replace("R$", "");


                       cmbcadastradoPor.Focus();
                   }
                   catch
                   {
                       MessageBox.Show("Insira um valor válido.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       toolStripStatusMensagem.Text = "o valor para o campo deve estar num padrão de moeda correto";
                       txtvalorEstimado.Text = "";
                       txtvalorEstimado.Focus();

                   }

               }
           }

           private void txtvalorReal_Enter(object sender, EventArgs e)
           {
               txtvalorReal.BackColor = Color.Yellow;
           }

           private void txtvalorReal_Leave(object sender, EventArgs e)
           {
               txtvalorReal.BackColor = Color.White;
           }

           private void txtProcessoContabil_Enter(object sender, EventArgs e)
           {
               txtProcessoContabil.BackColor = Color.Yellow;
           }

           private void txtProcessoContabil_Leave(object sender, EventArgs e)
           {
               txtProcessoContabil.BackColor = Color.White;
           }

           private void cmbcadastradoPor_Enter(object sender, EventArgs e)
           {
               cmbcadastradoPor.BackColor = Color.Yellow;
           }

           private void cmbcadastradoPor_Leave(object sender, EventArgs e)
           {
               cmbcadastradoPor.BackColor = Color.White;
           }

           private void cmbFornecedor_Enter(object sender, EventArgs e)
           {
               cmbFornecedor.BackColor = Color.Yellow;
           }

           private void cmbFornecedor_Leave(object sender, EventArgs e)
           {
               cmbFornecedor.BackColor = Color.White;
           }

           private void cmbSetor_Enter(object sender, EventArgs e)
           {
              // cmbSetor.BackColor = Color.Yellow;
           }

           private void cmbSetor_Leave(object sender, EventArgs e)
           {
               // cmbSetor.BackColor = Color.White;
           }

           private void txtObs_Enter(object sender, EventArgs e)
           {
               txtObs.BackColor = Color.Yellow;
           }

           private void txtObs_Leave(object sender, EventArgs e)
           {
               txtObs.BackColor = Color.White;
           }

           private void label34_Click(object sender, EventArgs e)
           {

           }

           private void chkCadastrante_CheckedChanged(object sender, EventArgs e)
           {
               mostrarResultados();
           }

           private void button2_Click_2(object sender, EventArgs e)
           {
               
               
           }

           private void chkDataCadastro_CheckedChanged(object sender, EventArgs e)
           {
               mostrarResultados();
           }

           private void chkProcessoContabil_CheckedChanged_1(object sender, EventArgs e)
           {
               mostrarResultados();
           }

           private void chkProc_CheckedChanged(object sender, EventArgs e)
           {
               mostrarResultados();
           }

           private void chkAF_CheckedChanged_1(object sender, EventArgs e)
           {
               mostrarResultados();
           }

           private void txtAno_TextChanged(object sender, EventArgs e)
           {

           }

           private void Label8_Click(object sender, EventArgs e)
           {

           }

           private void txtSetorVeiculo_TextChanged(object sender, EventArgs e)
           {

           }

           private void txtCheckDesdobrada_KeyPress(object sender, KeyPressEventArgs e)
           {

           }

           private void PesquisaDesdobrada(string p)
           {
               /*
               if (chkPlanilhaDespesas.Checked == false)
               {

                   mDataSet = new DataSet();
                   mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                   mConn.Open();

                   //cria um adapter utilizando a instrução SQL para acessar a tabela 
                   //if (checkBox3.Checked == true)
                   // ordena a tabela de acordo com o critério estabelecido
                   mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE dotacao " + "LIKE " + "'%" + p + "%'", mConn);

                   //preenche o dataset através do adapter
                   mAdapter.Fill(mDataSet, "rim");

                   //atribui o resultado à propriedade DataSource da dataGridView
                   dataGridView1.DataSource = mDataSet;
                   dataGridView1.DataMember = "rim";
               }
               else {


                   mDataSet = new DataSet();
                   mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                   mConn.Open();

                   //cria um adapter utilizando a instrução SQL para acessar a tabela 
                   //if (checkBox3.Checked == true)
                   // ordena a tabela de acordo com o critério estabelecido
                   mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE dotacao " + "LIKE " + "'%" + p + "%'", mConn);

                   //preenche o dataset através do adapter
                   mAdapter.Fill(mDataSet, "rim");

                   //atribui o resultado à propriedade DataSource da dataGridView
                   dataGridView1.DataSource = mDataSet;
                   dataGridView1.DataMember = "rim";
                         
               }
                */
                              
               mDataSet = new DataSet();
               mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
               mConn.Open();

               if (chkPlanilhaDespesas.Checked == false)
               {
                   /*

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
                    * */
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

           private void txtCheckProcessoContabil_KeyPress(object sender, KeyPressEventArgs e)
           {
               if (e.KeyChar == 13) //Se for Enter executa a validação
               {
                   PesquisaPorProcessoContabil(txtCheckProcessoContabil.Text);
                   LimpaFiltros();
               }
               else
               {
                   // MessageBox.Show("Tecle 'ENTER'");
               }
           }

           private void PesquisaPorProcessoContabil(string p)
           {
               /*
               if (chkPlanilhaDespesas.Checked == false)
               {
                   mDataSet = new DataSet();
                   mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                   mConn.Open();

                   //cria um adapter utilizando a instrução SQL para acessar a tabela 
                   //if (checkBox3.Checked == true)
                   // ordena a tabela de acordo com o critério estabelecido
                   mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE ProcessoContabil " + "LIKE " + "'%" + p + "%'", mConn);

                   //preenche o dataset através do adapter
                   mAdapter.Fill(mDataSet, "rim");

                   //atribui o resultado à propriedade DataSource da dataGridView
                   dataGridView1.DataSource = mDataSet;
                   dataGridView1.DataMember = "rim";
               }
               else
               {
                   mDataSet = new DataSet();
                   mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                   mConn.Open();

                   //cria um adapter utilizando a instrução SQL para acessar a tabela 
                   //if (checkBox3.Checked == true)
                   // ordena a tabela de acordo com o critério estabelecido
                   mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE ProcessoContabil " + "LIKE " + "'%" + p + "%'", mConn);

                   //preenche o dataset através do adapter
                   mAdapter.Fill(mDataSet, "planilha_despesa");

                   //atribui o resultado à propriedade DataSource da dataGridView
                   dataGridView1.DataSource = mDataSet;
                   dataGridView1.DataMember = "planilha_despesa";               
               
               }

               calculaQuantidadeRegistros();
               */

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
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE ProcessoContabil like '%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição no período selecionado";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE ProcessoContabil like '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
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

                       mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE ProcessoContabil like" + "'%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº do processo contábil vinculado à requisição no período selecionado";

                       mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Processocontabil like '%" + p + "%' and (dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "') ) ORDER BY Processo", mConn);
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

           private void txtCheckEmpenho_Enter(object sender, EventArgs e)
           {
               txtCheckEmpenho.BackColor = Color.Yellow;

               toolStripStatusMensagem.Text = "pesquisa por código gerado do empenho";
           }

           private void txtCheckEmpenho_Leave(object sender, EventArgs e)
           {
               txtCheckEmpenho.BackColor = Color.White;
           }

           private void txtCheckProcessoContabil_Leave(object sender, EventArgs e)
           {
               txtCheckProcessoContabil.BackColor = Color.White;
           }

           private void txtCheckProcessoContabil_Enter(object sender, EventArgs e)
           {
               txtCheckProcessoContabil.BackColor = Color.Yellow;
               toolStripStatusMensagem.Text = "pesquisa por código do processo contábil gerado";
           }

           private void txtCheckCodigoAplicacao_Leave(object sender, EventArgs e)
           {
               txtCheckCodigoAplicacao.BackColor = Color.White;

               toolStripStatusMensagem.Text = "pesquisa por código aplicação";

           }

           private void txtCheckCodigoAplicacao_Enter(object sender, EventArgs e)
           {
               txtCheckCodigoAplicacao.BackColor = Color.Yellow;

               toolStripStatusMensagem.Text = "pesquisa por fonte de recursos";
           }

           private void txtCheckDescricao_TextChanged(object sender, EventArgs e)
           {

           }

           private void txtCheckDesdobrada_Enter(object sender, EventArgs e)
           {
               txtCheckDesdobrada.BackColor = Color.Yellow;

               toolStripStatusMensagem.Text = "pesquisa por código da despesa desdobrada";
           }

           private void txtCheckDesdobrada_Leave(object sender, EventArgs e)
           {
               txtCheckDesdobrada.BackColor = Color.White;
           }

           private void txtCheckDesdobrada_KeyPress_1(object sender, KeyPressEventArgs e)
           {
               if (e.KeyChar == 13) //Se for Enter executa a validação
               {
                   PesquisaDesdobrada(txtCheckDesdobrada.Text);
                   LimpaFiltros();
               }
               else
               {
                   // MessageBox.Show("Tecle 'ENTER'");
               }
 
           }

           private void label46_Click(object sender, EventArgs e)
           {

           }

           private void chkPlanilhaDespesas_MouseHover(object sender, EventArgs e)
           {
               chkPlanilhaDespesas.ForeColor = Color.Red;
           }

           private void chkPlanilhaDespesas_MouseLeave(object sender, EventArgs e)
           {
               chkPlanilhaDespesas.ForeColor = Color.Blue;
           }

           private void txtCheckEmpenho_KeyPress(object sender, KeyPressEventArgs e)
           {
               if (e.KeyChar == 13) //Se for Enter executa a validação
               {
                   PesquisaPorEmpenho(txtCheckEmpenho.Text);
                   LimpaFiltros();
               }
               else
               {
                   // MessageBox.Show("Tecle 'ENTER'");
               }
           }

           private void PesquisaPorEmpenho(string p)
           {
               /*
               mDataSet = new DataSet();
               mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
               mConn.Open();

               //cria um adapter utilizando a instrução SQL para acessar a tabela 
               //if (checkBox3.Checked == true)
               // ordena a tabela de acordo com o critério estabelecido
               mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE empenho " + "LIKE " + "'%" + p + "%'", mConn);

               //preenche o dataset através do adapter
               mAdapter.Fill(mDataSet, "planilha_despesa");

               //atribui o resultado à propriedade DataSource da dataGridView
               dataGridView1.DataSource = mDataSet;
               dataGridView1.DataMember = "planilha_despesa";

               calculaQuantidadeRegistros();
                */
                                             
               mDataSet = new DataSet();
               mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
               mConn.Open();
               // não há empenho na planilha rim
               if (chkPlanilhaDespesas.Checked == false)
               {
                   //mostraChecks();

                   //cria um adapter utilizando a instrução SQL para acessar a tabela 

                   /*
                   if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº do empenho vinculado à requisição";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE empemho like '%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº do empenho vinculado à requisição no período selecionado";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE empenho '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                   }

                   //preenche o dataset através do adapter
                   mAdapter.Fill(mDataSet, "rim");

                   //atribui o resultado à propriedade DataSource da dataGridView
                   dataGridView1.DataSource = mDataSet;
                   dataGridView1.DataMember = "rim";
                    */
               }
               else
               {
                   if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº do empenho vinculado à requisição";

                       mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE empenho like" + "'%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº do empenho vinculado à requisição no período selecionado";

                       mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Empenho like '%" + p + "%' and (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ) ORDER BY Processo", mConn);
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

           private void txtCheckCodigoAplicacao_KeyPress(object sender, KeyPressEventArgs e)
           {
               if (e.KeyChar == 13) //Se for Enter executa a validação
               {
                   PesquisaPorCodigoAplicacao(txtCheckCodigoAplicacao.Text);
                   LimpaFiltros();
               }
               else
               {
                   // MessageBox.Show("Tecle 'ENTER'");
               }
           }

           private void PesquisaPorCodigoAplicacao(string p)
           {
               /*
               mDataSet = new DataSet();
               mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
               mConn.Open();

               //cria um adapter utilizando a instrução SQL para acessar a tabela 
               //if (checkBox3.Checked == true)
               // ordena a tabela de acordo com o critério estabelecido
               mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE acao LIKE " + "'%" + p + "%'", mConn);

               //preenche o dataset através do adapter
               mAdapter.Fill(mDataSet, "planilha_despesa");

               //atribui o resultado à propriedade DataSource da dataGridView
               dataGridView1.DataSource = mDataSet;
               dataGridView1.DataMember = "planilha_despesa";

               calculaQuantidadeRegistros();
                * */
                                             
               mDataSet = new DataSet();
               mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
               mConn.Open();

               if (chkPlanilhaDespesas.Checked == false)
               {
                  /*

                   //cria um adapter utilizando a instrução SQL para acessar a tabela 

                   if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº da despesa desdobrada vinculado à requisição";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Dotacao like '%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por nº da despesa desdobrada vinculado à requisição no período selecionado";
                       mAdapter = new MySqlDataAdapter("SELECT * FROM rim WHERE Dotacao '%" + p + "%' and dataCetilsql BETWEEN '" + Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy/MM/dd") + "' AND '" + Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy/MM/dd") + "'", mConn);
                   }

                   //preenche o dataset através do adapter
                   mAdapter.Fill(mDataSet, "rim");

                   //atribui o resultado à propriedade DataSource da dataGridView
                   dataGridView1.DataSource = mDataSet;
                   dataGridView1.DataMember = "rim";
                   */

               }
                   // É codigo de Aplicação, mas no banco de dados o campo é Açao. Alterá-lo para Cod_Aplicacao
               else
               {
                   if (txtDataInicial.Text == "" || txtDataFinal.Text == "")
                   {
                       toolStripStatusMensagem.Text = "pesquisa por codigo de aplicação vinculada à requisição";

                       mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE Acao like" + "'%" + p + "%'", mConn);
                   }
                   else
                   {
                       toolStripStatusMensagem.Text = "pesquisa por código de aplicação vinculada à requisição no período selecionado";

                       mAdapter = new MySqlDataAdapter("SELECT * FROM planilha_despesa WHERE (Acao like '%" + p + "%' and (dataCetil BETWEEN '" + txtDataInicial.Text + "' AND '" + txtDataFinal.Text + "') ) ORDER BY Processo", mConn);
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

           private void txtCheckDataCetil_TextChanged(object sender, EventArgs e)
           {
               //Calendar3.Visible = true;
           }

           private void Calendar3_DateSelected(object sender, DateRangeEventArgs e)
           {
               txtCheckDataCetil.Text = Calendar3.SelectionRange.Start.ToString("dd/MM/yyyy");
               Calendar3.Visible = false; 
           }

           private void dTPDataCadastro_ValueChanged(object sender, EventArgs e)
           {
               txtdtCadastro.Text = dTPDataCadastro.Value.ToString("dd/MM/yyyy");
               dTPDataCadastro.Visible = false;
           }

           private void txtdtCadastro_Enter(object sender, EventArgs e)
           {
               dTPDataCadastro.Visible = true;
           }

           private void btnFornecedorVinculado_Click(object sender, EventArgs e)
           {
               if (txtCodFornecedor.Text == "") {

                   MessageBox.Show("Escolha um fornecedor","Atenção");
                   cmbFornecedor.Focus();
               }
               else 
               { 
                   rim_tem_fornecedores fornecedor = new rim_tem_fornecedores();
                   fornecedor.Show();              
               }
           }

           private void dtpDataInicial_ValueChanged(object sender, EventArgs e)
           {
               txtDataInicial.Text = dtpDataInicial.Value.ToString("dd/MM/yyyy");
               //dtpDataInicial.Visible = false;
           }

           private void dtpDataFinal_ValueChanged(object sender, EventArgs e)
           {
               txtDataFinal.Text = dtpDataFinal.Value.ToString("dd/MM/yyyy");
               //dtpDataFinal.Visible = false;
           }

           private void txtAnoProcesso_TextChanged(object sender, EventArgs e)
           {

           }

           private void txtAnoProcesso_KeyPress(object sender, KeyPressEventArgs e)
           {
               if (e.KeyChar == 13) //Se for Enter executa a validação
               {
                   txtDO.Focus();
                   //txtAnoProcesso.Focus();
               }
               else
               {
                   txtAnoProcesso.Focus();

               }
           }

           private void dtpDivisaoContabil_ValueChanged(object sender, EventArgs e)
           {
               //dtpDivisaoContabil.Visible = false;
               lblDataContabilidade.Visible = true;
               //lblDataContabilidade.Text = dtpDivisaoContabil.Value.ToString("dd/MM/yyyy");
               checkBoxContab.Enabled = false;
           }

           private void dtpOrdenador1_ValueChanged(object sender, EventArgs e)
           {
               //dtpOrdenador1.Visible = false;
               lblDataOrdenador1.Visible = true;
               //lblDataOrdenador1.Text = dtpOrdenador1.Value.ToString("dd/MM/yyyy");
               checkBoxOrdenador1.Enabled = false;
           }

           private void dtpCompras1_ValueChanged(object sender, EventArgs e)
           {
               //dtpCompras1.Visible = false;
               lblDataCompras1.Visible = true;
               //lblDataCompras1.Text = dtpCompras1.Value.ToString("dd/MM/yyyy");
               checkBoxCompras1.Enabled = false;
           }
        
           private void dtpOrdenadorEmpenho_ValueChanged(object sender, EventArgs e)
           {
               //dtpOrdenadorEmpenho.Visible = false;
               lblDataOrdenador2.Visible = true;
              // lblDataOrdenador2.Text = dtpOrdenadorEmpenho.Value.ToString("dd/MM/yyyy");
               checkBoxOrdenador2.Enabled = false;
           }

           private void dtpCompras2_ValueChanged(object sender, EventArgs e)
           {
               //dtpCompras2.Visible = false;
               lblDataCompras2.Visible = true;
               //lblDataCompras2.Text = dtpCompras2.Value.ToString("dd/MM/yyyy");
               checkBoxCompras2.Enabled = false;
           }

           private void dtpDipe_ValueChanged(object sender, EventArgs e)
           {
               //dtpDipe.Visible = false;
               lblDataDipe.Visible = true;
               //lblDataDipe.Text = dtpDipe.Value.ToString("dd/MM/yyyy");
               checkBoxDIPE.Enabled = false;
           }


           private void dtpOrdenador1_Leave(object sender, EventArgs e)
           {
               
           }

           private void dtpCompras1_Leave(object sender, EventArgs e)
           {
               
           }

           private void dtpOrdenadorEmpenho_Leave(object sender, EventArgs e)
           {
           }

           private void dtpCompras2_Leave(object sender, EventArgs e)
           {
               
           }

           private void dtpDipe_Leave(object sender, EventArgs e)
           {
              
           }

           private void dtpDivisaoContabil_Leave(object sender, EventArgs e)
           {
              
           }

           private void dtpDivisaoContabil_Enter(object sender, EventArgs e)
           {
               verificaDTP();
           }

           private void verificaDTP()
           {
                                  
           }

           private void txtCodigo_TextChanged(object sender, EventArgs e)
           {

           }

           private void cmbPlacaConsulta_SelectedValueChanged(object sender, EventArgs e)
           {
               PesquisaRimPorPlaca(cmbPlacaConsulta.Text);
                
           }

           private void PesquisaRimPorPlaca(string p)
           {
               mDataSet = new DataSet();
               mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
               mConn.Open();

               //cria um adapter utilizando a instrução SQL para acessar a tabela 
               //if (checkBox3.Checked == true)
               // ordena a tabela de acordo com o critério estabelecido
               mAdapter = new MySqlDataAdapter("select * from consultaPlaca WHERE placa='" + cmbPlacaConsulta.Text + "'", mConn);

               //preenche o dataset através do adapter
               mAdapter.Fill(mDataSet, "consultaPlaca");

               //atribui o resultado à propriedade DataSource da dataGridView
               dataGridView1.DataSource = mDataSet;
               dataGridView1.DataMember = "consultaPlaca";

               calculaQuantidadeRegistros();

           }

           private void Calendar2_DateChanged(object sender, DateRangeEventArgs e)
           {

           }

           private void monthCalendar7_DateSelected(object sender, DateRangeEventArgs e)
           {
               lblDataContabilidade.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
               monthCalendar1.Visible = false;
               checkBoxContab.Enabled = false;
               
           }

           private void monthCalendar8_DateSelected(object sender, DateRangeEventArgs e)
           {
               lblDataOrdenador1.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
               monthCalendar1.Visible = false;
               checkBoxOrdenador1.Enabled = false;
           }

           private void monthCalendar9_DateSelected(object sender, DateRangeEventArgs e)
           {               
               lblDataCompras1.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
               monthCalendar1.Visible = false;
               checkBoxCompras1.Enabled = false;
           }

           private void monthCalendar10_DateSelected(object sender, DateRangeEventArgs e)
           {               
               lblDataOrdenador2.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
               monthCalendar1.Visible = false;
               checkBoxOrdenador2.Enabled = false;
           }

           private void monthCalendar11_DateSelected(object sender, DateRangeEventArgs e)
           {               
               lblDataCompras2.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
               monthCalendar1.Visible = false;
               checkBoxCompras2.Enabled = false;
           }

           private void monthCalendar12_DateSelected(object sender, DateRangeEventArgs e)
           {               
               lblDataDipe.Text = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
               monthCalendar1.Visible = false;
               checkBoxDIPE.Enabled = false;
           }

           private void txtCheckProcesso_TextChanged(object sender, EventArgs e)
           {

           }

           private void Label4_Click(object sender, EventArgs e)
           {

           }

           private void txtnotaFiscal_TextChanged(object sender, EventArgs e)
           {

           }

           private void monthCalendar7_DateChanged(object sender, DateRangeEventArgs e)
           {

           }

           private void button2_Click_3(object sender, EventArgs e)
           {
       
            PrintDGV.Print_DataGridView(dataGridView1);
            
        
           }

           private void button2_Click_4(object sender, EventArgs e)
           {
               PlanilhaDespesa pd = new PlanilhaDespesa();
               pd.Show();
           }

           private void label5_Click(object sender, EventArgs e)
           {
               monthCalendar1.Visible = true;
           }

           private void label6_Click(object sender, EventArgs e)
           {
               monthCalendar1.Visible = true;
           }

           private void label8_Click_1(object sender, EventArgs e)
           {
               monthCalendar1.Visible = true;
           }

           private void label9_Click_1(object sender, EventArgs e)
           {
               monthCalendar1.Visible = true;
           }

           private void label10_Click(object sender, EventArgs e)
           {
               monthCalendar1.Visible = true;
           }

           private void label11_Click_1(object sender, EventArgs e)
           {
               monthCalendar1.Visible = true;
           }
                               
      }

     }


                
     


