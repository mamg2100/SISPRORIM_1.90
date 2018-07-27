using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace Sistema_Prorim
{
    public partial class Requisicao : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        public string stConection;
        private string stConsulta;
        public MySqlConnection Cmn = new MySqlConnection();
        private int flagTramite = 0;

        private int codigoultimari = 0; // recebe o numero da ultima ri cadastrada 
        String ano = DateTime.Now.Year.ToString();


        public Requisicao()
        {
            InitializeComponent();
        }

        private void Requisicao_Load(object sender, EventArgs e)
        {
            RefreshComboBoxes();
            // desabilita o cmbCadastrante pois a partir da versão 1.90 o cadastrante da RIM é quem está logado.
            cmbcadastradoPor.Enabled = false;

            label18.Text = Sistema_prorim.Global.InclusaoRI.flagIncluirRim.ToString();

            // Se for alteração flagIncluirRim=0
            if (Sistema_prorim.Global.InclusaoRI.flagIncluirRim == 0)
            {
                btnEmpenho.Enabled = true;
                txtCetil.Enabled = false;
                txtdataCetil.Enabled = false;
            }
            else
            {
                btnEmpenho.Enabled = false;
            }

            //Campos que não podem ser nulos no BD: cetil, data cetil, unidade, ano processo e processo contábil, codigo usuário, 
            //código da unidade. Logo tem que ter obrigatoriedade de preenchimento destes campos antes de tentar gravar/incluir 
            //uma RI.

            if (Sistema_prorim.Global.Logon.tipoRequisicao == "RIM")
            {
                label37.Visible = false;
                txtProcessoContabil.Visible = false;
                textBox7.Visible = false;
                textBoxAnoProcContabil.Visible = false;
            }
            else
            {
                label37.Visible = true;
                txtProcessoContabil.Visible = true;
                textBox7.Visible = true;
                textBoxAnoProcContabil.Visible = true;
            }

            textBoxAnoProcesso.Text = ano;
            textBoxAnoProcContabil.Text = ano;

            //Código para marcar o radioButton correspondente à RIM ou RRP.
            if (Sistema_prorim.Global.Logon.tipoRequisicao == "RIM")
            {
                radioButtonRIM.Checked = true;
                radioButtonRRP.Checked = false;
            }
            else
            {

                radioButtonRRP.Checked = true;
                radioButtonRIM.Checked = false;
            }

            //indica que foi clicado botão RIM do form consulta que é para inclusão de RIM e não alteração, diferente se 
            //clicarmos no grid - em uma rim já existente - e teremos sim uma alteração e flagIncluirRim != 1

            if (Sistema_prorim.Global.InclusaoRI.flagIncluirRim != 1)
            {
                // Se é inclusão o botão de excluir não deve aparecer, mas na alteração sim...
                btnExcluir.Visible = true;
                btnAlterar.Visible = true;
                btnIncluir.Visible = false;

                if (radioButtonVeiculo.Checked == false)
                {
                    txtdescricao.Top = 104;
                    txtdescricao.Height = 85;
                    lblDescricao.Top = 87;
                    lblDescricao.Left = 147;

                    lblCodigo.Text = Sistema_prorim.Global.DadosRim.codigo;
                    txtdataCetil2.Text = Sistema_prorim.Global.DadosRim.dataCetil;
                    txtdataCetil.Text = Sistema_prorim.Global.DadosRim.dataCetil;
                    txtCetil.Text = Sistema_prorim.Global.DadosRim.cetil;
                    cmbEscolha.Text = Sistema_prorim.Global.DadosRim.escolhaUnid;
                    txtdescricao.Text = Sistema_prorim.Global.DadosRim.descricao;
                    txtProcesso.Text = Sistema_prorim.Global.DadosRim.Processo;
                    txtAnoProcesso.Text = Sistema_prorim.Global.DadosRim.AnoProcesso;
                    txtDO.Text = Sistema_prorim.Global.DadosRim.DO;
                    txtvalorEstimado2.Text = Sistema_prorim.Global.DadosRim.valorEstimado;
                    txtvalorReal2.Text = Sistema_prorim.Global.DadosRim.valorReal;
                    txtProcessoContabil.Text = Sistema_prorim.Global.DadosRim.ProcessoContabil;
                    txtAnoProcessoContabil.Text = Sistema_prorim.Global.DadosRim.AnoProcessoContabil;

                    if (Sistema_prorim.Global.DadosRim.DataContabilidade != "")
                    {
                        checkBoxContab.Checked = true;
                        lblDataContabilidade.Text = Sistema_prorim.Global.DadosRim.DataContabilidade;
                    }
                    else
                    {
                        checkBoxContab.Checked = false;
                        lblDataContabilidade.Text = Sistema_prorim.Global.DadosRim.DataContabilidade;
                    }

                    if (Sistema_prorim.Global.DadosRim.DataOrdenador1 != "")
                    {
                        checkBoxOrdenador1.Checked = true;
                        lblDataOrdenador1.Text = Sistema_prorim.Global.DadosRim.DataOrdenador1;
                    }
                    else
                    {
                        checkBoxOrdenador1.Checked = false;
                        lblDataOrdenador1.Text = Sistema_prorim.Global.DadosRim.DataOrdenador1;
                    }

                    if (Sistema_prorim.Global.DadosRim.DataPrefeito != "")
                    {
                        checkBoxPrefeito.Checked = true;
                        lblDataPrefeito.Text = Sistema_prorim.Global.DadosRim.DataOrdenador1;
                    }
                    else
                    {
                        checkBoxOrdenador1.Checked = false;
                        lblDataOrdenador1.Text = Sistema_prorim.Global.DadosRim.DataOrdenador1;
                    }
                    if (Sistema_prorim.Global.DadosRim.DataCompras1 != "")
                    {
                        checkBoxCompras1.Checked = true;
                        lblDataCompras1.Text = Sistema_prorim.Global.DadosRim.DataCompras1;
                    }
                    else
                    {
                        checkBoxCompras1.Checked = false;
                        lblDataCompras1.Text = Sistema_prorim.Global.DadosRim.DataCompras1;
                    }

                    if (Sistema_prorim.Global.DadosRim.DataOrdenador2 != "")
                    {
                        checkBoxOrdenador2.Checked = true;
                        lblDataOrdenador2.Text = Sistema_prorim.Global.DadosRim.DataOrdenador2;
                    }
                    else
                    {
                        checkBoxOrdenador1.Checked = false;
                        lblDataOrdenador2.Text = Sistema_prorim.Global.DadosRim.DataOrdenador2;
                    }

                    if (Sistema_prorim.Global.DadosRim.DataCompras2 != "")
                    {
                        checkBoxCompras2.Checked = true;
                        lblDataCompras2.Text = Sistema_prorim.Global.DadosRim.DataCompras2;
                    }
                    else
                    {
                        checkBoxCompras2.Checked = false;
                        lblDataCompras2.Text = Sistema_prorim.Global.DadosRim.DataCompras2;
                    }

                    if (Sistema_prorim.Global.DadosRim.DataDipe != "")
                    {
                        checkBoxDIPE.Checked = true;
                        lblDataDipe.Text = Sistema_prorim.Global.DadosRim.DataDipe;
                    }
                    else
                    {
                        checkBoxDIPE.Checked = false;
                        lblDataDipe.Text = Sistema_prorim.Global.DadosRim.DataDipe;
                    }


                    //cmbcadastradoPor.Text = Sistema_prorim.Global.DadosRim.cadastradoPor;

                    cmbcadastradoPor.Text = Sistema_prorim.Global.Logon.nome_usuario;
                    lblCodUsuario.Text = Sistema_prorim.Global.Logon.codigo_usuario;
                    txtdtCadastro2.Text = Sistema_prorim.Global.DadosRim.dtCadastro;


                    if (Sistema_prorim.Global.DadosRim.dtCadastro != "")
                    {
                        txtdtCadastro.Text = Sistema_prorim.Global.DadosRim.dtCadastro;
                    }
                    else
                    {
                        txtdtCadastro.Text = "";
                    }

                    txtObs.Text = Sistema_prorim.Global.DadosRim.Obs;

                    carregarDadosDespesa();
                }
            }
            else
            {
                // Se não entrar no if acima é porque trata-se de inclusão.
                btnExcluir.Visible = false;
                btnAlterar.Visible = false;
                btnIncluir.Visible = true;
                // Como é inclusão de nova RIM as variáveis devem estar "limpas".
                LimpaVariaveis();

                if (radioButtonVeiculo.Checked == false)
                {
                    txtdescricao.Top = 104;
                    lblDescricao.Top = 87;
                    lblDescricao.Left = 147;
                    txtdescricao.Height = 85;

                    lblCodigo.Text = Sistema_prorim.Global.DadosRim.codigo;
                    txtdataCetil2.Text = Sistema_prorim.Global.DadosRim.dataCetil;
                    txtdataCetil.Text = Sistema_prorim.Global.DadosRim.dataCetil;
                    txtCetil.Text = Sistema_prorim.Global.DadosRim.cetil;
                    cmbEscolha.Text = Sistema_prorim.Global.DadosRim.escolhaUnid;
                    txtdescricao.Text = Sistema_prorim.Global.DadosRim.descricao;
                    txtProcesso.Text = Sistema_prorim.Global.DadosRim.Processo;
                    txtAnoProcesso.Text = Sistema_prorim.Global.DadosRim.AnoProcesso;
                    txtDO.Text = Sistema_prorim.Global.DadosRim.DO;
                    txtvalorEstimado2.Text = Sistema_prorim.Global.DadosRim.valorEstimado;
                    txtvalorReal2.Text = Sistema_prorim.Global.DadosRim.valorReal;
                    txtProcessoContabil.Text = Sistema_prorim.Global.DadosRim.ProcessoContabil;
                    txtAnoProcessoContabil.Text = Sistema_prorim.Global.DadosRim.AnoProcessoContabil;

                    if (Sistema_prorim.Global.DadosRim.DataContabilidade != "")
                    {
                        checkBoxContab.Checked = true;
                        lblDataContabilidade.Text = Sistema_prorim.Global.DadosRim.DataContabilidade;
                    }
                    else
                    {
                        checkBoxContab.Checked = false;
                        lblDataContabilidade.Text = Sistema_prorim.Global.DadosRim.DataContabilidade;
                    }

                    if (Sistema_prorim.Global.DadosRim.DataOrdenador1 != "")
                    {
                        checkBoxOrdenador1.Checked = true;
                        lblDataOrdenador1.Text = Sistema_prorim.Global.DadosRim.DataOrdenador1;
                    }
                    else
                    {
                        checkBoxOrdenador1.Checked = false;
                        lblDataOrdenador1.Text = Sistema_prorim.Global.DadosRim.DataOrdenador1;
                    }


                    if (Sistema_prorim.Global.DadosRim.DataPrefeito != "")
                    {
                        checkBoxPrefeito.Checked = true;
                        lblDataOrdenador1.Text = Sistema_prorim.Global.DadosRim.DataPrefeito;
                    }
                    else
                    {
                        checkBoxPrefeito.Checked = false;
                        lblDataPrefeito.Text = Sistema_prorim.Global.DadosRim.DataPrefeito;
                    }

                    if (Sistema_prorim.Global.DadosRim.DataCompras1 != "")
                    {
                        checkBoxCompras1.Checked = true;
                        lblDataCompras1.Text = Sistema_prorim.Global.DadosRim.DataCompras1;
                    }
                    else
                    {
                        checkBoxCompras1.Checked = false;
                        lblDataCompras1.Text = Sistema_prorim.Global.DadosRim.DataCompras1;
                    }

                    if (Sistema_prorim.Global.DadosRim.DataOrdenador2 != "")
                    {
                        checkBoxOrdenador2.Checked = true;
                        lblDataOrdenador2.Text = Sistema_prorim.Global.DadosRim.DataOrdenador2;
                    }
                    else
                    {
                        checkBoxOrdenador1.Checked = false;
                        lblDataOrdenador2.Text = Sistema_prorim.Global.DadosRim.DataOrdenador2;
                    }

                    if (Sistema_prorim.Global.DadosRim.DataCompras2 != "")
                    {
                        checkBoxCompras2.Checked = true;
                        lblDataCompras2.Text = Sistema_prorim.Global.DadosRim.DataCompras2;
                    }
                    else
                    {
                        checkBoxCompras2.Checked = false;
                        lblDataCompras2.Text = Sistema_prorim.Global.DadosRim.DataCompras2;
                    }

                    if (Sistema_prorim.Global.DadosRim.DataDipe != "")
                    {
                        checkBoxDIPE.Checked = true;
                        lblDataDipe.Text = Sistema_prorim.Global.DadosRim.DataDipe;
                    }
                    else
                    {
                        checkBoxDIPE.Checked = false;
                        lblDataDipe.Text = Sistema_prorim.Global.DadosRim.DataDipe;
                    }

                    cmbcadastradoPor.Text = Sistema_prorim.Global.Logon.nome_usuario;
                    lblCodUsuario.Text = Sistema_prorim.Global.Logon.codigo_usuario;
                    txtdtCadastro2.Text = Sistema_prorim.Global.DadosRim.dtCadastro;

                    if (Sistema_prorim.Global.DadosRim.dtCadastro != "")
                    {
                        txtdtCadastro.Text = Sistema_prorim.Global.DadosRim.dtCadastro;
                    }
                    else
                    {
                        txtdtCadastro.Text = "";
                    }

                    txtObs.Text = Sistema_prorim.Global.DadosRim.Obs;

                    txtCetil.Focus();
                }
                lblCodUsuario.Text = Sistema_prorim.Global.Logon.codigo_usuario;
                cmbcadastradoPor.Text = Sistema_prorim.Global.Logon.nome_usuario;
            }

            popularCmbFornecedorComFornecedorVinculado();

            if (cmbFornecedor.Items.Count == 0)
            {
                populaCmbFornecedorComTodosFornecedores();
            }
            else
            {
                //popularCmbFornecedorComFornecedorVinculado(); 
                cmbFornecedor.SelectedIndex = 0;
            }

            monthCalendar.Visible = false;
        }

        private void populaCmbFornecedorComTodosFornecedores()
        {
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
        }

        private void popularCmbFornecedorComFornecedorVinculado()
        {
            // Se for alteração e existir um fornecedor vinculado à RI, ele já aparece no cmbFornecedor
            // Se não tiver fornecedor vinculadoaparecem todos, para que futuramente seja escolhido um ou alguns

            //Puxando nome dos fornecedores vinculados caso existam ou todos caso não ainda não haja vinculação.

            //----------------------------------------------------------------------------------------------------------
            // populando cmbFornecedor com um fornecedor cadastrado se aqui estivermos em alteração de RI e se já
            // existir um fornecedor vinculado à RI na tabela rim_has_fornecedor

            try
            {
                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                stConsulta = "SELECT Cod_fornecedor FROM rim_has_fornecedor where cod_rim='" + lblCodigo.Text + "'";

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        lblCodigoFornecedor.Text = myReader["Cod_fornecedor"] + Environment.NewLine;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível fazer conexão. Erro: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            Cmn.Close();

            //-----------------------------------------------------------------------------------------------------------
            // Agora que temos o código do fornecedor da tabela rim_has_fornecedor, vamos popular o combo somente com nome do
            // fornecedor vinculado

            try
            {
                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();

                stConsulta = "SELECT nome_fornecedor FROM fornecedor where cod_fornecedor='" + lblCodigoFornecedor.Text + "'";

                MySqlCommand myCmd = new MySqlCommand();
                myCmd.Connection = Cmn;
                myCmd.CommandText = stConsulta;
                MySqlDataReader myReader = myCmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        myReader.Read();
                        cmbFornecedor.Items.Add(myReader["nome_fornecedor"] + Environment.NewLine);
                        //txtObs.Text = myReader["nome_fornecedor"] + Environment.NewLine;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível fazer conexão. Erro: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            Cmn.Close();

            //-----------------------------------------------------------------------------------------------------------

        }

        private void carregarDadosDespesa()
        {
            try
            {
                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
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

                        lblCodigoDespesa.Text = myReader["Cod_Despesa"] + Environment.NewLine;
                        txtCodigoDespesa.Text = myReader["Despesa"] + Environment.NewLine;
                        txtReduzida.Text = myReader["Reduzida"] + Environment.NewLine;
                        txtPrograma.Text = myReader["Programa"] + Environment.NewLine;
                        txtAcao.Text = myReader["Acao"] + Environment.NewLine;
                    }
                }

            }
            catch
            {
                MessageBox.Show("ERRO. Não foi possível fazer a conexão com Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Sistema_prorim.Global.despesa.coddespesas = lblCodigoDespesa.Text;
            Sistema_prorim.Global.despesa.despesas = txtDO.Text;
            Sistema_prorim.Global.NotaFiscal.codigoRI = txtCetil.Text;

            //--------------------- Método que verifica se a despesa já está vinculada à RI que se está cadastrando----------
            //verificaSeDespesaEstaVinculada();

            Cmn.Close();
        }

        private void RefreshComboBoxes()
        {
            cmbcadastradoPor.Items.Clear();
            cmbEscolha.Items.Clear();
            cmbFornecedor.Items.Clear();
            cmbPlaca.Items.Clear();

            retirarEspaços();

            // POPULANDO TODOS ComboBox

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
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

            if (Sistema_prorim.Global.InclusaoRI.flagIncluirRim == 0)
            {
                // popula somente cmbFornecedor com dados do vinculado
                try
                {
                    stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT Cod_fornecedor FROM rim_has_fornecedor where cod_rim='" + lblCodigo.Text + "'";

                    MySqlCommand myCmd = new MySqlCommand();
                    myCmd.Connection = Cmn;
                    myCmd.CommandText = stConsulta;
                    MySqlDataReader myReader = myCmd.ExecuteReader();

                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            myReader.Read();
                            lblCodigoFornecedor.Text = myReader["Cod_fornecedor"] + Environment.NewLine;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível fazer conexão. Erro: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                Cmn.Close();

                //-----------------------------------------------------------------------------------------------------------
                // Agora que temos o código do fornecedor da tabela rim_has_fornecedor, vamos popular o combo somente com nome do
                // fornecedor vinculado

                try
                {
                    stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                    Cmn.ConnectionString = stConection;
                    Cmn.Open();

                    stConsulta = "SELECT nome_fornecedor FROM fornecedor where cod_fornecedor='" + lblCodigoFornecedor.Text + "'";

                    MySqlCommand myCmd = new MySqlCommand();
                    myCmd.Connection = Cmn;
                    myCmd.CommandText = stConsulta;
                    MySqlDataReader myReader = myCmd.ExecuteReader();

                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            myReader.Read();
                            cmbFornecedor.Text = myReader["nome_fornecedor"] + Environment.NewLine;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível fazer conexão. Erro: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                Cmn.Close();

                //-----------------------------------------------------------------------------------------------------------

            }
            else
            {
                //popula com todos os nomes existentes pois aqui é uma inclusão.
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
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível fazer conexão. Erro: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                Cmn.Close();
            }
        }

        private void LimpaVariaveis()
        {
            Sistema_prorim.Global.DadosRim.codigo = "";
            Sistema_prorim.Global.DadosRim.dataCetil = "";
            Sistema_prorim.Global.DadosRim.cetil = "";
            Sistema_prorim.Global.DadosRim.escolhaUnid = "";
            Sistema_prorim.Global.DadosRim.descricao = "";
            Sistema_prorim.Global.DadosRim.Processo = "";
            Sistema_prorim.Global.DadosRim.AnoProcesso = "";
            Sistema_prorim.Global.DadosRim.DO = "";
            Sistema_prorim.Global.DadosRim.valorEstimado = "";
            Sistema_prorim.Global.DadosRim.valorReal = "";
            Sistema_prorim.Global.DadosRim.ProcessoContabil = "";
            Sistema_prorim.Global.DadosRim.AnoProcessoContabil = "";
            Sistema_prorim.Global.DadosRim.DataContabilidade = "";
            Sistema_prorim.Global.DadosRim.DataOrdenador1 = "";
            Sistema_prorim.Global.DadosRim.DataPrefeito = "";
            Sistema_prorim.Global.DadosRim.DataCompras1 = "";
            Sistema_prorim.Global.DadosRim.DataOrdenador2 = "";
            Sistema_prorim.Global.DadosRim.DataCompras2 = "";
            Sistema_prorim.Global.DadosRim.DataDipe = "";
            Sistema_prorim.Global.DadosRim.cadastradoPor = "";
            Sistema_prorim.Global.DadosRim.dtCadastro = "";
            Sistema_prorim.Global.DadosRim.Obs = "";
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            Consulta cons = new Consulta();
            cons.Show();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            // Verifica se os campos de preenchimento obrigatório foram preenchidos antes de gravar, caso 
            // contrário haverá erro na inclusão de dados.

            if (txtCetil.Text == "")
            {
                txtCetil.Focus();
                toolStripStatusLabel4.Text = "Preencha o código da RI a ser incluída.";
            }
            else
            {
                if (txtdataCetil2.Text == "")
                {
                    txtdataCetil.Focus();
                    toolStripStatusLabel4.Text = "Escolha uma data válida para a RI no calendário.";
                }
                else
                {
                    if (cmbEscolha.Text == "")
                    {
                        cmbEscolha.Focus();
                        toolStripStatusLabel4.Text = "Escolha uma Unidade/Setor válida(o).";
                    }
                    else
                    {
                        if (txtDO.Text == "")
                        {
                            txtDO.Focus();
                            toolStripStatusLabel4.Text = "Escolha uma despesa válida.";
                        }
                        else
                        {
                            if (txtvalorEstimado2.Text == "")
                            {
                                txtvalorEstimado2.Focus();
                                toolStripStatusLabel4.Text = "Entre com algum valor válido da aquisição ou contratação.";
                            }
                            else
                            {
                                if (cmbcadastradoPor.Text == "")
                                {
                                    cmbcadastradoPor.Focus();
                                    toolStripStatusLabel4.Text = "Informe uma pessoa responsável pela confecção da requisição.";
                                }
                                else
                                {
                                    if (txtdtCadastro2.Text == "")
                                    {
                                        txtdtCadastro.Focus();
                                        toolStripStatusLabel4.Text = "Escolha uma data válida para inclusão da RI no calendário.";
                                    }
                                    else
                                    {
                                        gravar();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void gravar()
        {

            /* É aconselhável criar um utilizador com password. Para acrescentar a password é somente
               necessário acrescentar o seguinte código a seguir ao uid=root;password= */

            retirarEspaços();

            if (radioButtonRIM.Checked == true)
            {
                Sistema_prorim.Global.Logon.tipoRequisicao = "RIM";
            }
            else
            {
                Sistema_prorim.Global.Logon.tipoRequisicao = "RRP";
            }


            // Início da Conexão com indicação de qual o servidor, nome de base de dados a utilizar
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

            // Abre a conexão
            mConn.Open();

            try
            {
                if (txtvalorReal2.Text == "")
                {
                    txtvalorReal2.Text = "0.00";
                }

                if (radioButtonRRP.Checked == true)
                {

                    MySqlCommand command = new MySqlCommand("INSERT INTO rim (Nome_Unidade,Descricao,Dotacao,Tipo_RIM,Cetil,DataCetil,DataCetilSQL,ValorEstimado,ValorReal,Processo,ano_processo,ProcessoContabil,ano_processo_contabil,Contabilidade,OrdenadorAss,Prefeito,ComprasPrim,OrdenadorEmpenho,ComprasSeg,Dipe,Cadastrante,DataCadastro,Observacao,Cd_Usuario,CD_unidade)"
                    + "VALUES('" + cmbEscolha.Text + "','"
                    + txtdescricao.Text + "','" + txtDO.Text + "','" + Sistema_prorim.Global.Logon.tipoRequisicao.Trim() + "','"
                    + txtCetil.Text + "','" + txtdataCetil.Text + "','" + txtdataCetil2.Text + "','"
                    + Convert.ToDecimal(txtvalorEstimado2.Text) + "','" + Convert.ToDecimal(txtvalorReal2.Text) + "','" + txtProcesso.Text + "','"
                    + txtAnoProcesso.Text + "','" + txtProcessoContabil.Text + "','" + txtAnoProcessoContabil.Text + "','"
                    + lblDataContabilidade.Text + "','" + lblDataOrdenador1.Text + "','" + lblDataPrefeito.Text + "','" + lblDataCompras1.Text + "','"
                    + lblDataOrdenador2.Text + "','" + lblDataCompras2.Text + "','" + lblDataDipe.Text + "','" + cmbcadastradoPor.Text + "','"
                    + txtdtCadastro2.Text + "','" + txtObs.Text + "'," + Convert.ToInt32(lblCodUsuario.Text) + "," + Convert.ToInt32(lblCodUnidade.Text) + ")", mConn);
                    //Executa a Query SQL
                    command.ExecuteNonQuery();
                    // Antes de fechar a conexão. captura o codigo sequencial da RI (atraves da variável GLOBAL e mais abaixo abre a conexão
                    // com a tabela rim_tem_dotacao e atualiza pra essa RI (Cetil) específica o campo (atributo) 'Cod_rim'

                }
                else
                {

                    txtAnoProcessoContabil.Text = "";

                    MySqlCommand command = new MySqlCommand("INSERT INTO rim (Nome_Unidade,Descricao,Dotacao,Tipo_RIM,Cetil,DataCetil,DataCetilSQL,ValorEstimado,ValorReal,Processo,ano_processo,ProcessoContabil,ano_processo_contabil,Contabilidade,OrdenadorAss,Prefeito,ComprasPrim,OrdenadorEmpenho,ComprasSeg,Dipe,Cadastrante,DataCadastro,Observacao,Cd_Usuario,CD_unidade)"
                    + "VALUES('" + cmbEscolha.Text + "','"
                    + txtdescricao.Text + "','" + txtDO.Text + "','" + Sistema_prorim.Global.Logon.tipoRequisicao.Trim() + "','"
                    + txtCetil.Text + "','" + txtdataCetil.Text + "','" + txtdataCetil2.Text + "','"
                    + Convert.ToDecimal(txtvalorEstimado2.Text) + "','" + Convert.ToDecimal(txtvalorReal2.Text) + "','" + txtProcesso.Text + "','"
                    + txtAnoProcesso.Text + "','" + txtProcessoContabil.Text + "','" + txtAnoProcessoContabil.Text + "','"
                    + lblDataContabilidade.Text + "','" + lblDataOrdenador1.Text + "','" + lblDataPrefeito.Text + "','" + lblDataCompras1.Text + "','"
                    + lblDataOrdenador2.Text + "','" + lblDataCompras2.Text + "','" + lblDataDipe.Text + "','" + cmbcadastradoPor.Text + "','"
                    + txtdtCadastro2.Text + "','" + txtObs.Text + "'," + Convert.ToInt32(lblCodUsuario.Text) + "," + Convert.ToInt32(lblCodUnidade.Text) + ")", mConn);

                    //Executa a Query SQL
                    command.ExecuteNonQuery();
                    // Antes de fechar a conexão. captura o codigo sequencial da RI (atraves da variável GLOBAL e mais abaixo abre a conexão
                    // com a tabela rim_tem_dotacao e atualiza pra essa RI (Cetil) específica o campo (atributo) 'Cod_rim'


                }

                calcularCodigo(); // descobre qual o codigo ID da proxima RI a ser incluída.
                Sistema_prorim.Global.RI.codcetil = lblCodigo.Text;

                // Fecha a conexão
                mConn.Close();

                //Mensagem de Sucesso 
                MessageBox.Show("Requisição [ " + txtCetil.Text + " ] gravada com Sucesso! " + " [ ID: " + Sistema_prorim.Global.RI.codcetil + "]", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //------- Método para atualizar a tabela ref. despesas vinculadas a determinada RI / Update da tabela 'rim_has_dotacao' ----------

                atualizaRim_has_dotacao();

                if (radioButtonVeiculo.Checked == true)
                {
                    atualizarim_has_veiculo();
                }

                //--------------------------------------------------------------------------------------------------------------------------------

                limparCampos();
                //DesabilitaTextBox();
                //HabilitaRadionButtons();

                //btnCancelar.Visible = false;

                //label28.Visible = true;
                toolStripStatusLabel4.Text = "trâmite Desabilitado ";
                //GroupBox1.Enabled = false;
                //DesabilitaCheckBox();

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                MessageBox.Show("Requisição não foi gravada." + "\n" + "[ " + "INSERT INTO rim (Nome_Unidade,Descricao,Dotacao,Tipo_RIM,Cetil,DataCetil,DataCetilSQL,ValorEstimado,ValorReal,Processo,ano_processo,ProcessoContabil,ano_processo_contabil,Contabilidade,OrdenadorAss,ComprasPrim,OrdenadorEmpenho,ComprasSeg,Dipe,Cadastrante,DataCadastro,Observacao,Cd_Usuario,CD_unidade)"
                    + "VALUES('" + cmbEscolha.Text + "','"
                    + txtdescricao.Text + "','" + txtDO.Text + "','" + Sistema_prorim.Global.Logon.tipoRequisicao.Trim() + "','"
                    + txtCetil.Text + "','" + txtdataCetil.Text + "','" + txtdataCetil2.Text + "','"
                    + Convert.ToDecimal(txtvalorEstimado2.Text) + "','" + Convert.ToDecimal(txtvalorReal2.Text) + "','" + txtProcesso.Text + "','"
                    + txtAnoProcesso.Text + "','" + txtProcessoContabil.Text + "','" + txtAnoProcessoContabil.Text + "','"
                    + lblDataContabilidade.Text + "','" + lblDataOrdenador1.Text + "','" + lblDataPrefeito.Text + "','" + lblDataCompras1.Text + "','"
                    + lblDataOrdenador2.Text + "','" + lblDataCompras2.Text + "','" + lblDataDipe.Text + "','" + cmbcadastradoPor.Text + "','"
                    + txtdtCadastro2.Text + "','" + txtObs.Text + "'," + Convert.ToInt32(lblCodUsuario.Text) + "," + Convert.ToInt32(lblCodUnidade.Text) + ")" + " ] ", "ATENÇÂO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void atualizarim_has_veiculo()
        {
            try
            {
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

                // Abre a conexão
                mConn.Open();

                //Query SQl
                MySqlCommand command = new MySqlCommand("UPDATE rim_has_veiculo SET Cod_rim=" + Sistema_prorim.Global.RI.cetil + " WHERE Cod_rim='" +
                 txtCetil.Text + "'", mConn);

                //Executa a Query SQL
                command.ExecuteNonQuery();
                mConn.Close();
                MessageBox.Show("Tabela de Veículos atualizada com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Erro! UPDATE rim_has_veiculo SET Cod_rim=" + Sistema_prorim.Global.RI.cetil + " WHERE Cod_rim='" +
                txtCetil.Text + "'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void limparCampos()
        {
            lblCodigo.Text = "";
            txtdescricao.Text = "";
            txtDO.Text = "";
            txtCetil.Text = "";
            txtdataCetil2.Text = "";
            txtvalorEstimado2.Text = "";
            txtvalorReal2.Text = "";
            txtProcesso.Text = "";
            txtProcessoContabil.Text = "";
            lblDataContabilidade.Text = "";
            lblDataOrdenador1.Text = "";
            lblDataCompras1.Text = "";
            lblDataOrdenador2.Text = "";
            lblDataCompras2.Text = "";
            lblDataDipe.Text = "";
            txtdtCadastro2.Text = "";
            txtObs.Text = "";
            lblCodigoDespesa.Text = "";
            txtReduzida.Text = "";
            txtPrograma.Text = "";
            txtAcao.Text = "";
            lblCodigoFornecedor.Text = "";
            lblCodUnidade.Text = "";
            lblCodUsuario.Text = "";
        }

        private void atualizaRim_has_dotacao()
        {
            Sistema_prorim.Global.RI.cetil = lblCodigo.Text;

            try
            {
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

                // Abre a conexão
                mConn.Open();

                //Query SQl
                MySqlCommand command = new MySqlCommand("UPDATE rim_has_dotacao SET Cod_rim=" + Sistema_prorim.Global.RI.cetil + " WHERE Cetil='" +
                 txtCetil.Text + "'", mConn);

                //Executa a Query SQL
                command.ExecuteNonQuery();

                //Mensagem de Sucesso
                //MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Fecha a conexão
                mConn.Close();
            }
            catch
            {
                MessageBox.Show("ERRO! UPDATE rim_has_dotacao SET Cod_rim=" + Sistema_prorim.Global.RI.cetil + " WHERE Cetil='" +
                 txtCetil.Text + "'", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void calcularCodigo()
        {
            try
            {

                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
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

                        lblCodigo.Text = myReader["Cod_rim"] + Environment.NewLine;
                        codigoultimari = Convert.ToInt32(lblCodigo.Text);
                        lblCodigo.Text = codigoultimari.ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }

        }

        private void retirarEspaços()
        {
            txtdescricao.Text = txtdescricao.Text.Trim();
            lblCodigo.Text = lblCodigo.Text.Trim();
            cmbEscolha.Text = cmbEscolha.Text.Trim();
            txtdescricao.Text = txtdescricao.Text.Trim();
            txtdescricao.Text = txtdescricao.Text.ToUpper();
            txtDO.Text = txtDO.Text.Trim();
            txtCetil.Text = txtCetil.Text.Trim();
            txtdataCetil2.Text = txtdataCetil2.Text.Trim();
            txtvalorEstimado2.Text = txtvalorEstimado2.Text.Trim();
            txtvalorReal2.Text = txtvalorReal2.Text.Trim();
            txtProcesso.Text = txtProcesso.Text.Trim();
            cmbcadastradoPor.Text = cmbcadastradoPor.Text.Trim();
            txtdtCadastro2.Text = txtdtCadastro2.Text.Trim();
            cmbFornecedor.Text = cmbFornecedor.Text.Trim();
            txtObs.Text = txtObs.Text.Trim();

        }

        private void cmbEscolha_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
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
                        lblCodUnidade.Text = myReader["Cod_unidade"] + Environment.NewLine;
                    }
                }
            }
            catch
            {

                MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //lblMsg.Text = "Falha na conexão.";
                toolStripStatusLabel4.Text = "houve falha na conexão";

            }

            Cmn.Close();

        }

        private void cmbcadastradoPor_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
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
                        lblCodUsuario.Text = myReader["Cod_usuario"] + Environment.NewLine;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            Cmn.Close();

        }

        private void cmbFornecedor_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
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
                        lblCodigoFornecedor.Text = myReader["Cod_fornecedor"] + Environment.NewLine;
                    }
                }

                if (lblCodigoFornecedor.Text != "")
                {
                    Sistema_prorim.Global.fornecedor.codfornecedor = lblCodigoFornecedor.Text;
                    Sistema_prorim.Global.RI.cetil = txtCetil.Text;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível fazer conexão. Erro: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            Cmn.Close();

        }

        private void txtdataCetil_ValueChanged(object sender, EventArgs e)
        {
            if (txtdataCetil2.Text == "")
            {
                toolStripStatusLabel4.Text = "É obrigatório informar a Data da RI.";
                txtdataCetil.Focus();
                //txtdataCetil.BackColor = Color.Yellow;
            }
            else
            {
                cmbEscolha.Focus();
                //cmbEscolha.BackColor = Color.Yellow;
                //txtdataCetil.BackColor = Color.White;
                Sistema_prorim.Global.NotaFiscal.codigoRI = txtCetil.Text;
            }

            txtdataCetil2.Text = Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy-MM-dd");

        }

        private void txtdtCadastro_ValueChanged(object sender, EventArgs e)
        {
            txtObs.Focus();
            txtdtCadastro2.Text = txtdtCadastro.Text;
        }

        private void txtCetil_Enter(object sender, EventArgs e)
        {
            txtCetil.BackColor = Color.Yellow;
        }

        private void txtdataCetil_Enter(object sender, EventArgs e)
        {
            txtdataCetil.BackColor = Color.Yellow;
        }

        private void cmbEscolha_Enter(object sender, EventArgs e)
        {
            cmbEscolha.BackColor = Color.Yellow;
        }

        private void txtdescricao_Enter(object sender, EventArgs e)
        {
            txtdescricao.BackColor = Color.Yellow;
        }

        private void txtProcesso_Enter(object sender, EventArgs e)
        {
            txtProcesso.BackColor = Color.Yellow;
        }

        private void txtDO_Enter(object sender, EventArgs e)
        {
            txtDO.BackColor = Color.Yellow;
        }

        private void txtProcessoContabil_Enter(object sender, EventArgs e)
        {
            txtProcessoContabil.BackColor = Color.Yellow;
        }

        private void cmbcadastradoPor_Enter(object sender, EventArgs e)
        {
            cmbcadastradoPor.BackColor = Color.Yellow;
        }

        private void txtdtCadastro_Enter(object sender, EventArgs e)
        {
            txtdtCadastro.BackColor = Color.Yellow;
        }

        private void txtObs_Enter(object sender, EventArgs e)
        {
            txtObs.BackColor = Color.Yellow;
        }

        private void cmbFornecedor_Enter(object sender, EventArgs e)
        {
            cmbFornecedor.BackColor = Color.Yellow;
        }

        private void txtCetil_Leave(object sender, EventArgs e)
        {
            txtCetil.BackColor = Color.White;
        }

        private void txtdataCetil_Leave(object sender, EventArgs e)
        {
            txtdataCetil.BackColor = Color.White;
        }

        private void cmbEscolha_Leave(object sender, EventArgs e)
        {
            cmbEscolha.BackColor = Color.White;
        }

        private void txtdescricao_Leave(object sender, EventArgs e)
        {
            txtdescricao.BackColor = Color.White;
        }

        private void txtProcesso_Leave(object sender, EventArgs e)
        {
            txtProcesso.BackColor = Color.White;
        }

        private void txtDO_Leave(object sender, EventArgs e)
        {
            txtDO.BackColor = Color.White;
        }

        private void txtProcessoContabil_Leave(object sender, EventArgs e)
        {
            txtProcessoContabil.BackColor = Color.White;
        }

        private void cmbcadastradoPor_Leave(object sender, EventArgs e)
        {
            cmbcadastradoPor.BackColor = Color.White;
        }

        private void txtdtCadastro_Leave(object sender, EventArgs e)
        {
            txtdtCadastro.BackColor = Color.White;
        }

        private void txtObs_Leave(object sender, EventArgs e)
        {
            txtObs.BackColor = Color.White;
            txtObs.Text = txtObs.Text.ToUpper();
        }

        private void cmbFornecedor_Leave(object sender, EventArgs e)
        {
            cmbFornecedor.BackColor = Color.White;
        }

        private void txtCetil_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Devemos analisar se para determinada requisição foi vinculada uma placa de veículo caso contrário não podemos 
            // prosseguir com o cadastramento da requisição.

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtCetil.Text == "")
                {
                    toolStripStatusLabel4.Text = "Código da RI é obrigatório";
                    txtCetil.Focus();
                    //txtCetil.BackColor = Color.Yellow;
                }
                else
                {
                    txtdataCetil.Focus();
                    //txtdataCetil.BackColor = Color.Yellow;
                    //txtCetil.BackColor = Color.White;
                    Sistema_prorim.Global.NotaFiscal.codigoRI = txtCetil.Text;
                }
            }
            else
            {
                txtCetil.Focus();
            }
        }

        private void txtdataCetil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtdataCetil2.Text == "")
                {
                    toolStripStatusLabel4.Text = "É obrigatório informar a Data da RI.";
                    txtdataCetil.Focus();
                    //txtdataCetil.BackColor = Color.Yellow;
                }
                else
                {
                    cmbEscolha.Focus();
                    //cmbEscolha.BackColor = Color.Yellow;
                    //txtdataCetil.BackColor = Color.White;
                    Sistema_prorim.Global.NotaFiscal.codigoRI = txtCetil.Text;
                }
            }
            else
            {
                txtdataCetil.Focus();

            }
        }

        private void cmbEscolha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (cmbEscolha.Text == "")
                {
                    toolStripStatusLabel4.Text = "É obrigatório a escolha de uma Unidade requisitante.";
                    cmbEscolha.Focus();
                    //cmbEscolha.BackColor = Color.Yellow;
                }
                else
                {
                    txtdescricao.Focus();
                    //cmbEscolha.BackColor = Color.White;
                    Sistema_prorim.Global.NotaFiscal.codigoRI = txtCetil.Text;
                }
            }
            else
            {
                cmbEscolha.Focus();

            }
        }

        private void txtdescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter, executa a validação
            {
                if (txtdescricao.Text == "")
                {
                    txtdescricao.Focus();
                }
                else
                {
                    txtProcesso.Focus();
                    txtdescricao.Text = txtdescricao.Text.ToUpper();
                }
            }
            else
            {
                txtdescricao.Focus();

            }
        }

        private void txtProcesso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter, executa a validação
            {
                if (txtProcesso.Text == "")
                {
                    txtProcesso.Focus();
                }
                else
                {
                    txtvalorEstimado2.Focus();
                }
            }
            else
            {
                txtProcesso.Focus();

            }
        }
        private void txtProcessoContabil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter, executa a validação
            {
                if (txtProcessoContabil.Text == "")
                {
                    txtProcessoContabil.Focus();
                }
                else
                {
                    cmbcadastradoPor.Focus();
                }
            }
            else
            {
                txtProcessoContabil.Focus();

            }
        }

        private void cmbcadastradoPor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter, executa a validação
            {
                if (cmbcadastradoPor.Text == "")
                {
                    cmbcadastradoPor.Focus();
                }
                else
                {
                    txtdtCadastro.Focus();
                }
            }
            else
            {
                cmbcadastradoPor.Focus();

            }
        }

        private void txtdtCadastro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter, executa a validação
            {
                if (txtdtCadastro.Text == "")
                {
                    txtdtCadastro.Focus();
                }
                else
                {
                    txtObs.Focus();
                }
            }
            else
            {
                txtdtCadastro.Focus();

            }
        }

        private void txtDO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ///-----------------------------------------------
                //try
                //{
                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
                Cmn.ConnectionString = stConection;
                Cmn.Open();
                // Queremos achar o código da despesas digitada.
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

                        lblCodigoDespesa.Text = myReader["Cod_Despesa"] + Environment.NewLine;
                        txtCodigoDespesa.Text = myReader["Despesa"] + Environment.NewLine; //txtDO é textBox de cima e txtCodigoDespesa textBox de dados da despesa.
                        txtReduzida.Text = myReader["Reduzida"] + Environment.NewLine;
                        txtPrograma.Text = myReader["Programa"] + Environment.NewLine;
                        txtAcao.Text = myReader["Acao"] + Environment.NewLine;


                        if (txtCetil.Text == "")
                        {
                            MessageBox.Show("Informe o número da requisição", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCetil.Focus();
                        }
                        else
                        {
                            Sistema_prorim.Global.despesa.coddespesas = lblCodigoDespesa.Text;
                            Sistema_prorim.Global.despesa.despesas = txtDO.Text;
                            Sistema_prorim.Global.DadosRim.cetil = txtCetil.Text;
                            Sistema_prorim.Global.DadosRim.codigo = lblCodigo.Text;                            
                            txtvalorEstimado2.Focus();
                            Sistema_prorim.rim_tem_despesa despesa = new Sistema_prorim.rim_tem_despesa();
                            despesa.ShowDialog();
                        }
                    }

                }
                else
                {
                    lblCodigoDespesa.Text = "";
                    txtDO.Text = "";
                    toolStripStatusLabel4.Text = "Despesa não cadastrada. Opções: clique no botão 'DESPESA' para cadastrá-la ou escolha outra válida";
                    MessageBox.Show("A Despesa informada não existe ou está incorreta!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDO.Focus();
                }

                Cmn.Close();
            }
            else
            {
                txtDO.Focus();
            }
        }


        private void txtvalorReal2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter, executa a validação
            {
                if (txtvalorReal2.Text == "")
                {
                    txtvalorReal2.Focus();
                }
                else
                {
                    // desse jeito funciona até o impedimento de entrar com valor não numérico
                    txtvalorReal2.Text = Convert.ToDecimal(txtvalorReal2.Text).ToString("C");
                    txtvalorReal2.Text = txtvalorReal2.Text.Replace("R$", "");
                    if (txtProcessoContabil.Visible == true)
                    {
                        txtProcessoContabil.Focus();
                    }
                    else
                    {
                        cmbcadastradoPor.Focus();
                    }
                }
            }
            else
            {
                txtvalorReal2.Focus();

            }
        }

        private void txtvalorEstimado2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter, executa a validação
            {
                if (txtvalorEstimado2.Text == "")
                {
                    txtvalorEstimado2.Focus();
                }
                else
                {
                    // desse jeito funciona até o impedimento de entrar com valor não numérico
                    txtvalorEstimado2.Text = Convert.ToDecimal(txtvalorEstimado2.Text).ToString("C");
                    txtvalorEstimado2.Text = txtvalorEstimado2.Text.Replace("R$", "");
                    txtvalorEstimado2.Focus();
                }
            }
            else
            {
                txtvalorEstimado2.Focus();
            }
        }

        private void txtvalorEstimado2_Enter(object sender, EventArgs e)
        {
            txtvalorEstimado2.BackColor = Color.Yellow;
        }

        private void txtvalorEstimado2_Leave(object sender, EventArgs e)
        {
            txtvalorEstimado2.BackColor = Color.White;
        }

        private void txtvalorReal2_Enter(object sender, EventArgs e)
        {
            txtvalorReal2.BackColor = Color.Yellow;
        }

        private void txtvalorReal2_Leave(object sender, EventArgs e)
        {
            txtvalorReal2.BackColor = Color.White;
        }


        private void label5_Click(object sender, EventArgs e)
        {
            flagTramite = 1;
            ocultarCalendario();

        }

        private void ocultarCalendario()
        {

            if (monthCalendar.Visible == false)
            {
                monthCalendar.Visible = true;
            }
            else
            {
                monthCalendar.Visible = false;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            flagTramite = 2;
            ocultarCalendario();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            flagTramite = 3;
            ocultarCalendario();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            flagTramite = 4;
            ocultarCalendario();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            flagTramite = 5;
            ocultarCalendario();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            flagTramite = 6;
            ocultarCalendario();
        }


        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            switch (flagTramite)
            {

                case 1:
                    lblDataContabilidade.Text = monthCalendar.SelectionRange.Start.ToString("dd/MM/yyyy");
                    monthCalendar.Visible = false; ;
                    break;

                case 2:
                    lblDataOrdenador1.Text = monthCalendar.SelectionRange.Start.ToString("dd/MM/yyyy");
                    monthCalendar.Visible = false; ;
                    break;

                case 3:
                    lblDataPrefeito.Text = monthCalendar.SelectionRange.Start.ToString("dd/MM/yyyy");
                    monthCalendar.Visible = false; ;
                    break;

                case 4:
                    lblDataCompras1.Text = monthCalendar.SelectionRange.Start.ToString("dd/MM/yyyy");
                    monthCalendar.Visible = false; ;
                    break;

                case 5:
                    lblDataOrdenador2.Text = monthCalendar.SelectionRange.Start.ToString("dd/MM/yyyy");
                    monthCalendar.Visible = false; ;
                    break;

                case 6:
                    lblDataCompras2.Text = monthCalendar.SelectionRange.Start.ToString("dd/MM/yyyy");
                    monthCalendar.Visible = false; ;
                    break;

                case 7:
                    lblDataDipe.Text = monthCalendar.SelectionRange.Start.ToString("dd/MM/yyyy");
                    monthCalendar.Visible = false; ;
                    break;
            }
        }

        private void checkBoxContab_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxContab.Checked == true)
            {
                monthCalendar.Visible = true;
                lblDataContabilidade.Visible = true;
                flagTramite = 1;
            }
            else
            {
                lblDataContabilidade.Text = "";
                monthCalendar.Visible = false;
                flagTramite = 0;

            }
        }

        private void Requisicao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                monthCalendar.Visible = false;
            }
        }

        private void checkBoxOrdenador1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxOrdenador1.Checked == true)
            {
                monthCalendar.Visible = true;
                lblDataOrdenador1.Visible = true;
                //checkBoxContab.Enabled = true;
                flagTramite = 2;
            }
            else
            {
                lblDataOrdenador1.Text = "";
                monthCalendar.Visible = false;
                flagTramite = 0;

            }
        }

        private void checkBoxOrdenador2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxOrdenador2.Checked == true)
            {
                monthCalendar.Visible = true;
                lblDataOrdenador2.Visible = true;
                flagTramite = 5;
            }
            else
            {
                lblDataOrdenador2.Text = "";
                monthCalendar.Visible = false;
                flagTramite = 0;
            }
        }

        private void checkBoxCompras1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxCompras1.Checked == true)
            {
                monthCalendar.Visible = true;
                checkBoxCompras1.Visible = true;
                //checkBoxContab.Enabled = true;
                flagTramite = 4;
            }
            else
            {
                lblDataCompras1.Text = "";
                monthCalendar.Visible = false;
                flagTramite = 0;
            }
        }

        private void checkBoxCompras2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxCompras2.Checked == true)
            {
                monthCalendar.Visible = true;
                checkBoxCompras2.Visible = true;
                //checkBoxContab.Enabled = true;
                flagTramite = 6;
            }
            else
            {
                lblDataCompras2.Text = "";
                monthCalendar.Visible = false;
                flagTramite = 0;
            }
        }

        private void checkBoxDIPE_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxDIPE.Checked == true)
            {
                monthCalendar.Visible = true;
                checkBoxDIPE.Visible = true;
                //checkBoxContab.Enabled = true;
                flagTramite = 7;
            }
            else
            {
                lblDataDipe.Text = "";
                monthCalendar.Visible = false;
                flagTramite = 0;

            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            alterar();
        }

        private void alterar()
        {
            if (radioButtonRIM.Checked == true)
            {
                Sistema_prorim.Global.Logon.tipoRequisicao = "RIM";
            }
            else
            {
                Sistema_prorim.Global.Logon.tipoRequisicao = "RRP";
            }


            if (txtvalorReal2.Text == "")
            {
                txtvalorReal2.Text = "0.00";
            }
            else
            {
            }

            retirarEspaços();

            //conexao
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mConn;

                if (radioButtonRRP.Checked == true)
                {
                    cmd.CommandText = "UPDATE rim SET Nome_Unidade =" +
                    "'" + cmbEscolha.Text + "',"
                    + "Descricao=" + "'" + txtdescricao.Text + "',"
                    + "Dotacao=" + "'" + txtDO.Text + "',"
                    + "Tipo_RIM=" + "'" + Sistema_prorim.Global.Logon.tipoRequisicao + "',"
                    + "Cetil=" + "'" + txtCetil.Text + "',"
                    + "DataCetil=" + "'" + txtdataCetil.Text + "',"
                    + "DataCetilSQL=" + "'" + Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd") + "',"
                    + "ValorEstimado=" + "'" + txtvalorEstimado2.Text + "',"
                    + "ValorReal=" + "'" + txtvalorReal2.Text + "',"
                    + "Processo=" + "'" + txtProcesso.Text + "',"
                    + "ano_processo=" + "'" + txtAnoProcesso.Text + "',"
                    + "ProcessoContabil=" + "'" + txtProcessoContabil.Text + "',"
                    + "ano_processo_contabil=" + "'" + txtAnoProcessoContabil.Text + "',"
                    + "Contabilidade=" + "'" + lblDataContabilidade.Text + "',"
                    + "OrdenadorAss=" + "'" + lblDataOrdenador1.Text + "',"
                    + "Prefeito=" + "'" + lblDataPrefeito.Text + "',"
                    + "ComprasPrim=" + "'" + lblDataCompras1.Text + "',"
                    + "OrdenadorEmpenho=" + "'" + lblDataOrdenador2.Text + "',"
                    + "ComprasSeg=" + "'" + lblDataCompras2.Text + "',"
                    + "Dipe=" + "'" + lblDataDipe.Text + "',"
                    + "Cadastrante=" + "'" + cmbcadastradoPor.Text + "',"
                    + "DataCadastro=" + "'" + txtdtCadastro.Text + "',"
                    + "Observacao=" + "'" + txtObs.Text
                    + "'" + "WHERE Cod_rim=" + lblCodigo.Text;
                }
                else
                {
                    txtAnoProcessoContabil.Text = "";
                    cmd.CommandText = "UPDATE rim SET Nome_Unidade =" +
                    "'" + cmbEscolha.Text + "',"
                    + "Descricao=" + "'" + txtdescricao.Text + "',"
                    + "Dotacao=" + "'" + txtDO.Text + "',"
                    + "Tipo_RIM=" + "'" + Sistema_prorim.Global.Logon.tipoRequisicao + "',"
                    + "Cetil=" + "'" + txtCetil.Text + "',"
                    + "DataCetil=" + "'" + txtdataCetil.Text + "',"
                    + "DataCetilSQL=" + "'" + Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd") + "',"
                    + "ValorEstimado=" + "'" + txtvalorEstimado2.Text + "',"
                    + "ValorReal=" + "'" + txtvalorReal2.Text + "',"
                    + "Processo=" + "'" + txtProcesso.Text + "',"
                    + "ano_processo=" + "'" + txtAnoProcesso.Text + "',"
                    + "ProcessoContabil=" + "'" + txtProcessoContabil.Text + "',"
                    + "ano_processo_contabil=" + "'" + txtAnoProcessoContabil.Text + "',"
                    + "Contabilidade=" + "'" + lblDataContabilidade.Text + "',"
                    + "OrdenadorAss=" + "'" + lblDataOrdenador1.Text + "',"
                    + "Prefeito=" + "'" + lblDataPrefeito.Text + "',"
                    + "ComprasPrim=" + "'" + lblDataCompras1.Text + "',"
                    + "OrdenadorEmpenho=" + "'" + lblDataOrdenador2.Text + "',"
                    + "ComprasSeg=" + "'" + lblDataCompras2.Text + "',"
                    + "Dipe=" + "'" + lblDataDipe.Text + "',"
                    + "Cadastrante=" + "'" + cmbcadastradoPor.Text + "',"
                    + "DataCadastro=" + "'" + txtdtCadastro.Text + "',"
                    + "Observacao=" + "'" + txtObs.Text
                    + "'" + " WHERE Cod_rim=" + lblCodigo.Text;
                }

                //mConn.Open();
                int resultado = cmd.ExecuteNonQuery();
                if (resultado != 1)
                {
                    throw new Exception("Não foi possível alterar os dados da Unidade " + lblCodigo.Text);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("UPDATE rim SET Nome_Unidade =" +
                        "'" + cmbEscolha.Text + "',"
                        + "Descricao=" + "'" + txtdescricao.Text + "',"
                        + "Dotacao=" + "'" + txtDO.Text + "',"
                        + "Tipo_RIM=" + "'" + Sistema_prorim.Global.Logon.tipoRequisicao + "',"
                        + "Cetil=" + "'" + txtCetil.Text + "',"
                        + "DataCetil=" + "'" + txtdataCetil.Text + "',"
                        + "DataCetilSQL=" + "'" + Convert.ToDateTime(txtdataCetil.Text).ToString("yyyy/MM/dd") + "',"
                        + "ValorEstimado=" + "'" + txtvalorEstimado2.Text + "',"
                        + "ValorReal=" + "'" + txtvalorReal2.Text + "',"
                        + "Processo=" + "'" + txtProcesso.Text + "',"
                        + "ano_processo=" + "'" + txtAnoProcesso.Text + "',"
                        + "ProcessoContabil=" + "'" + txtProcessoContabil.Text + "',"
                        + "ano_processo_contabil=" + "'" + txtAnoProcessoContabil.Text + "',"
                        + "Contabilidade=" + "'" + lblDataContabilidade.Text + "',"
                        + "OrdenadorAss=" + "'" + lblDataOrdenador1.Text + "',"
                        + "Prefeito=" + "'" + lblDataPrefeito.Text + "',"
                        + "ComprasPrim=" + "'" + lblDataCompras1.Text + "',"
                        + "OrdenadorEmpenho=" + "'" + lblDataOrdenador2.Text + "',"
                        + "ComprasSeg=" + "'" + lblDataCompras2.Text + "',"
                        + "Dipe=" + "'" + lblDataDipe.Text + "',"
                        + "Cadastrante=" + "'" + cmbcadastradoPor.Text + "',"
                        + "DataCadastro=" + "'" + txtdtCadastro.Text + "',"
                        + "Observacao=" + "'" + txtObs.Text
                        + "'" + "WHERE Cod_rim=" + lblCodigo.Text, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                MessageBox.Show("Erro: " + ex.Message);

            }
            finally
            {
                mConn.Close();

                MessageBox.Show("Requisição [ " + txtCetil.Text + " ]" + " alterada com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Sistema_prorim.Global.despesa.flag_valor_real = "0";

                Sistema_prorim.Global.InclusaoRI.flagIncluirRim = 1;

                Consulta consulta = new Consulta();
                consulta.Show();

                atualizaRim_has_dotacao();

                this.Close();
            }

            cmbFornecedor.Items.Clear();
        }

        private void Requisicao_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sistema_prorim.Global.InclusaoRI.flagIncluirRim = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(lblCodigo.Text);
            excluir(temp);
        }

        private void excluir(int codigo)
        {
            {
                {
                    //conexao
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
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
                        //mostrarResultados();

                    }

                    MessageBox.Show("Requisição nr. '" + codigo + " 'excluída com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(lblCodigo.Text);

            string message = "Confirma a exclusão da Requisição " + txtCetil.Text + "?";
            string caption = "Confirmação da Ação";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                excluir(temp);
            }

        }

        private void btn_Despesa_Click(object sender, EventArgs e)
        {
            Sistema_prorim.Dotacao despesa = new Sistema_prorim.Dotacao();
            despesa.Show();
        }

        private void btnFornecedor_Click(object sender, EventArgs e)
        {
            Sistema_prorim.Fornecedor fornecedor = new Sistema_prorim.Fornecedor();
            fornecedor.ShowDialog();
        }

        private void btnFornecedorVinculado_Click(object sender, EventArgs e)
        {
            if (lblCodigoFornecedor.Text == "")
            {

                MessageBox.Show("Você deve escolher um fornecedor para vincular à RI", "Atenção");
                cmbFornecedor.Focus();
            }
            else
            {
                Sistema_prorim.Global.RI.codcetil = lblCodigo.Text;
                Sistema_prorim.Global.fornecedor.codfornecedor = lblCodigoFornecedor.Text;
                Sistema_prorim.rim_tem_fornecedores fornecedor = new Sistema_prorim.rim_tem_fornecedores();
                fornecedor.Show();
            }
        }

        private void btnNotaFiscal_Click(object sender, EventArgs e)
        {
            codigoFornecedor();

            if (cmbFornecedor.Text != "" && lblCodigo.Text != "")
            {
                Sistema_prorim.Global.NotaFiscal.codigoRI = lblCodigo.Text;
                Sistema_prorim.Global.NotaFiscal.fornecedor = lblCodigoFornecedor.Text;
                Sistema_prorim.Global.NotaFiscal.nomefornecedor = cmbFornecedor.Text;
                Sistema_prorim.NotaFiscal notafiscal = new Sistema_prorim.NotaFiscal();
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

                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
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
                        lblCodigoFornecedor.Text = myReader["Cod_fornecedor"] + Environment.NewLine;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Não foi possível fazer conexão.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //lblMsg.Text = "Código do fornecedor não localizado. Conexão falhou.";
                toolStripStatusLabel4.Text = "código do fornecedor não localizado";
            }
            Cmn.Close();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void checkBoxPrefeito_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxPrefeito.Checked == true)
            {
                monthCalendar.Visible = true;
                lblDataPrefeito.Visible = true;
                flagTramite = 3;//flag de exclusão
            }
            else
            {
                lblDataPrefeito.Text = "";
                monthCalendar.Visible = false;
                flagTramite = 0;//flag de alteração

            }
        }

        private void btnEmpenho_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
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
                        lblCodDespesa.Text = myReader["Cod_Despesa"] + Environment.NewLine;
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

            Sistema_prorim.Global.despesa.coddespesas = lblCodDespesa.Text;
            Sistema_prorim.Global.despesa.despesas = txtDO.Text;
            Sistema_prorim.Global.NotaFiscal.codigoRI = txtCetil.Text;
            //Sistema_prorim.Global.fornecedor.codfornecedor = lblCodFornecedor.Text;

            Sistema_prorim.rim_tem_despesa despesa = new Sistema_prorim.rim_tem_despesa();
            despesa.ShowDialog();
             * */
            ///-----------------------------------------------
            //try
            //{
            stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
            Cmn.ConnectionString = stConection;
            Cmn.Open();
            // Queremos achar o código da despesas digitada.
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

                    lblCodigoDespesa.Text = myReader["Cod_Despesa"] + Environment.NewLine;
                    txtCodigoDespesa.Text = myReader["Despesa"] + Environment.NewLine; //txtDO é textBox de cima e txtCodigoDespesa textBox de dados da despesa.
                    txtReduzida.Text = myReader["Reduzida"] + Environment.NewLine;
                    txtPrograma.Text = myReader["Programa"] + Environment.NewLine;
                    txtAcao.Text = myReader["Acao"] + Environment.NewLine;


                    if (txtCetil.Text == "")
                    {
                        MessageBox.Show("Informe o número da requisição", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCetil.Focus();
                    }
                    else
                    {
                        Sistema_prorim.Global.despesa.coddespesas = lblCodigoDespesa.Text;
                        Sistema_prorim.Global.despesa.despesas = txtDO.Text;
                        Sistema_prorim.Global.DadosRim.cetil = txtCetil.Text;
                        Sistema_prorim.Global.DadosRim.codigo = lblCodigo.Text;
                        txtvalorEstimado2.Focus();
                        Sistema_prorim.rim_tem_despesa despesa = new Sistema_prorim.rim_tem_despesa();
                        despesa.ShowDialog();
                    }
                }

            }
            else
            {
                lblCodigoDespesa.Text = "";
                txtDO.Text = "";
                toolStripStatusLabel4.Text = "Despesa não cadastrada. Opções: clique no botão 'DESPESA' para cadastrá-la ou escolha outra válida";
                MessageBox.Show("A Despesa informada não existe ou está incorreta!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDO.Focus();
            }

            Cmn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                stConection = "Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=";
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

                        lblCodigoDespesa.Text = myReader["Cod_Despesa"] + Environment.NewLine;
                        txtCodigoDespesa.Text = myReader["Despesa"] + Environment.NewLine;
                        txtReduzida.Text = myReader["Reduzida"] + Environment.NewLine;
                        txtPrograma.Text = myReader["Programa"] + Environment.NewLine;
                        txtAcao.Text = myReader["Acao"] + Environment.NewLine;

                    }
                }

            }
            catch
            {
                MessageBox.Show("ERRO. Não foi possível fazer a conexão com Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Sistema_prorim.Global.despesa.coddespesas = lblCodigoDespesa.Text;
            Sistema_prorim.Global.despesa.despesas = txtDO.Text;
            Sistema_prorim.Global.NotaFiscal.codigoRI = txtCetil.Text;

            //--------------------- Método que verifica se a despesa já está vinculada à RI que se está cadastrando----------
            //verificaSeDespesaEstaVinculada();

            Cmn.Close();
        }

        private void txtDO_TextChanged(object sender, EventArgs e)
        {
            /*
            txtCodigoDespesa.Text = "";
            txtAcao.Text="";
            txtPrograma.Text="";
            txtReduzida.Text="";
             */
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            populaCmbFornecedorComTodosFornecedores();
        }

        private void txtdescricao_MouseLeave(object sender, EventArgs e)
        {
            txtdescricao.Text = txtdescricao.Text.ToUpper();
        }

    }

}

