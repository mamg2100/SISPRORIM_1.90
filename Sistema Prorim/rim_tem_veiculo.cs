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
    public partial class rim_tem_veiculo : Form
    {

        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        public string stConection;
        public MySqlConnection Cmn = new MySqlConnection();

        public rim_tem_veiculo()
        {
            InitializeComponent();
        }

        private void rim_tem_veiculo_Load(object sender, EventArgs e)
        
        {
            cmbEscolha.Text = Global.VEICULO.unidade;
            Global.VEICULO.unidade = "";
            cmbPlaca.Text = Global.VEICULO.placa;
            Global.VEICULO.placa = "";
            txtCodPlaca.Text = Global.VEICULO.codPlaca;
            Global.VEICULO.codPlaca="";
            txtMarca.Text = Global.VEICULO.marca;
            Global.VEICULO.marca = "";
            txtModelo.Text = Global.VEICULO.modelo;
            Global.VEICULO.modelo = "";
            txtAnoVeiculo.Text = Global.VEICULO.ano;
            Global.VEICULO.ano = "";
            


            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=admin");
            mConn.Open();
          
            // populando cmbEscolha (mostra as unidades cadastradas)
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
            mConn.Close();
            mostraResultados();
                      
        }

        private void rbNovo_CheckedChanged(object sender, EventArgs e)
        {
            //HabilitaTextBox();
            txtCetil.Enabled = true;
            txtCetil.Focus();
            DesabilitaRadioButtons();
            bt_Gravar.Enabled = true;
       
        }

        private void HabilitaTextBox()
        {
            rbNovo.Enabled = true;
            rbAlterar.Enabled = true;
            rbExclui.Enabled = true;
        }

        private void DesabilitaRadioButtons()
        {
            rbNovo.Enabled = false;
            rbAlterar.Enabled = false;
            rbExclui.Enabled = false;
        }

        private void rbExclui_CheckedChanged(object sender, EventArgs e)
        {

            if (rbExclui.Checked == true)
            {
                MessageBox.Show("Clique no Grid na Coluna 'Unidade' selecionando a que será excluída.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //rbAlterar.Checked = false;
                
                HabilitaTextBox();
                DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                //btnAtualizar.Enabled = false;

            }
            else
            {
            }

        }

        private void rbAlterar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAlterar.Checked == true)
            {
                MessageBox.Show("Clique no Grid na Coluna 'Unidade' a ser alterado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //rbAlterar.Checked = false;
                
                HabilitaTextBox();
                DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                //btnAtualizar.Enabled = false;

            }
            else
            {

            }
            HabilitaTextBox();
            txtCetil.Focus();
            DesabilitaRadioButtons();
            bt_Gravar.Enabled = true;
       
        }

        private void bt_Gravar_Click(object sender, EventArgs e)
        {
            if (rbNovo.Checked == true)
            {
                Gravar(); 
            }
            else
            {
                int codigo = Convert.ToInt32(cmbPlaca.Text);
                if (rbAlterar.Checked == true)
                    Alterar(codigo);
                else
                    Excluir(codigo);
            }

        }

        private void Excluir(int codigo)
        {
            {
                {
                    //conexao
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=admin");
                    mConn.Open();
                    try
                    {
                        //mConn.ConnectionString = Dados.StringDeConexao;
                        //command
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = mConn;
                        cmd.CommandText = "delete from unidade where Cod_unidade = " + codigo;
                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível excluir a unidade " + codigo);
                        }
                    
                    }
                    catch
                    {
                        MessageBox.Show("Falha na conexão com Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                                      
                        mConn.Close();
                        mostraResultados();
                    

                    MessageBox.Show("Excluída a unidade nr. " + codigo + " com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UncheckedRadioButtons();
                }
            }
        }

        private void mostraResultados()
        {
            try
            {
                mDataSet = new DataSet();
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=admin");
                mConn.Open();

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "rim_has_veiculo");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "rim_has_veiculo";

                //Renomeia as colunas
                //dataGridView1.Columns[0].HeaderText = "Codigo Sequencial";
                //dataGridView1.Columns[1].HeaderText = "Placa";

                int registro;
                registro = dataGridView1.RowCount - 1;
                if (registro == 1)
                    label9.Text = registro + " registro";
                else
                    label9.Text = registro + " registros";
            }
            catch
            {

                MessageBox.Show("Falha na conexão com Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        
        private void UncheckedRadioButtons()
        {
            rbNovo.Checked = false;
            rbAlterar.Checked = false;
            rbExclui.Checked = false;
        }

        private void Alterar(int codigo)
        {

            if (rbAlterar.Checked == true)
            {
                MessageBox.Show("Clique no Grid na Coluna 'Unidade' a ser alterado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //rbAlterar.Checked = false;

                HabilitaTextBox();
                DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                //btnAtualizar.Enabled = false;

            }
            else
            {

            }
            HabilitaTextBox();
            txtCetil.Focus();
            DesabilitaRadioButtons();
            bt_Gravar.Enabled = true;
        }

        private void Gravar()
        {
            // Início da Conexão com indicação de qual o servidor, nome de base de dados a utilizar

            /* É aconselhável criar um utilizador com password. Para acrescentar a password é somente
               necessário acrescentar o seguinte código a seguir ao uid=root;password=adminxxxxx
             */

            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=admin");

            // Abre a conexão
            mConn.Open();

            //Query SQL
            MySqlCommand command = new MySqlCommand("INSERT INTO rim_has_veiculo (Cod_rim,Cod_seq_veiculo)" +
            "VALUES('" + txtCetil.Text + "','" + cmbPlaca.Text + "')", mConn);
            // Esta representando a sequencia "...VALUES(txtSetor,txtEndereço,...)"

            //Executa a Query SQL
            command.ExecuteNonQuery();

            // Fecha a conexão
            mConn.Close();

            //Mensagem de Sucesso
            MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"

            LimpaCampos();
            DesabilitaTextBox();
            //HabilitaRadionButtons();
            mostraResultados();
            UncheckedRadioButtons();
        }

        private void DesabilitaTextBox()
        {
            throw new NotImplementedException();
        }

        private void LimpaCampos()
        {
             
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            //txtSetor.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            
            HabilitaTextBox();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
