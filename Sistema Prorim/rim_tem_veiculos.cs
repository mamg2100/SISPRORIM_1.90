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
    public partial class rim_tem_veiculos : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        public string stConection;
        public MySqlConnection Cmn = new MySqlConnection();

        public rim_tem_veiculos()
        {
            InitializeComponent();
        }

        private void rim_tem_veiculos_Load(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "confira os dados, tecle em 'vincular' ou cancele a ação...";

            txtSetorVeiculo.Text = Global.Veiculos.unidade;
            Global.Veiculos.unidade = "";
            cmbPlaca.Text = Global.Veiculos.placa;
            Global.Veiculos.placa = "";
            txtCodPlaca.Text = Global.Veiculos.codPlaca;
            Global.Veiculos.codPlaca = "";
            txtMarca.Text = Global.Veiculos.marca;
            Global.Veiculos.marca = "";
            txtModelo.Text = Global.Veiculos.modelo;
            Global.Veiculos.modelo = "";
            txtAnoVeiculo.Text = Global.Veiculos.ano;
            Global.Veiculos.ano = "";
            txtCetil.Text = Global.RI.cetil;
            txtCodRI.Text = txtCetil.Text;



            bt_Gravar.Enabled = true;

            mostrarResultados();
            txtCetil.Focus();
        }

        private void rbExclui_CheckedChanged(object sender, EventArgs e)
        {

            if (rbExclui.Checked == true)
            {
                MessageBox.Show("Clique no Grid na linha que será excluída.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //rbAlterar.Checked = false;

               // HabilitaTextBox();
               // DesabilitaRadioButtons();
                bt_Gravar.Enabled = true;
                //btnAtualizar.Enabled = false;

            }
            else
            {
            }

        }

        private void Excluir(int codigo1, int codigo2)
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
                        cmd.CommandText = "delete from rim_has_veiculo where Cod_rim= " + codigo1 + " AND cod_seq_veiculo=" + codigo2;
                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível fazer a exclusão");
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Falha na conexão com Banco de Dados.[excluir()- RIM_tem_veiculos", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    
                    mostrarResultados();
                    //mConn.Close(); no 'mostrarResultados' já há essa linha

                    MessageBox.Show("Excluída com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //UncheckedRadioButtons();
                }
            }
        }

        private void mostrarResultados()
        {
            try
                {
                    mDataSet = new DataSet();
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    mConn.Open();
                    //mAdapter = new MySqlDataAdapter("SELECT * FROM rim_has_veiculo WHERE cod_seq_veiculo=" + txtCodPlaca.Text , mConn);
                    
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim_has_veiculo WHERE Cod_rim=" + Convert.ToInt32(txtCetil.Text), mConn);
                    //mAdapter = new MySqlDataAdapter("SELECT * FROM rim_has_veiculo WHERE cod_seq_veiculo=" + txtCodPlaca.Text + " AND Cod_rim=" + txtCetil.Text , mConn);
                    //preenche o dataset através do adapterert.
                    mAdapter.Fill(mDataSet, "rim_has_veiculo");

                    //atribui o resultado à propriedade DataSource da dataGridView
                    dataGridView1.DataSource = mDataSet;
                    dataGridView1.DataMember = "rim_has_veiculo";

                    //Renomeia as colunas
                    dataGridView1.Columns[0].HeaderText = "Codigo Sequencial da RI";
                    dataGridView1.Columns[1].HeaderText = "Codigo Veiculo";                              
                
                    txtCetil.Focus();
                    //mAdapter = new MySqlDataAdapter("SELECT * FROM rim_has_veiculo WHERE cod_seq_veiculo=" + txtCodPlaca.Text, mConn);
                        
                }
            catch
                {
                    MessageBox.Show("Falha na conexão com o Banco de Dados[met:mostrarResultados/tab:rim_tem_veiculos]", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                mConn.Close();
                txtCetil.Focus();

                calculaQuantidadeRegistro();

                Global.Veiculos.quantPlaca = label9.Text;               
                
                
        }

        private void calculaQuantidadeRegistro()
        {
            int registro;
            registro = dataGridView1.RowCount - 1;
            if (registro == 1 || registro == 0)
                label9.Text = registro + " registro";
            else
                label9.Text = registro + " registros";
        }


        private void DesabilitaTextBox()
        {
            txtCetil.Enabled = false;
        }

        private void LimpaCampos()
        {

        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
         
        private void bt_Gravar_Click(object sender, EventArgs e)
        
        {
            if (txtCetil.Text == "")
            {
                toolStripTextBox1.Text = "Informe o nº da Requisição";
                txtCetil.Focus();
            }
            else
            {
                if (txtCodRI.Text == "")
                {
                    txtCodRI.Text = txtCetil.Text;
                }
                else
                {
                    Gravar();
                    bt_Gravar.Enabled = false;
                }

            }
        }

        private void Gravar()
        {

            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

            // Abre a conexão
            mConn.Open();

            try
                    {
                        //Query SQL
                        MySqlCommand command = new MySqlCommand("INSERT INTO rim_has_veiculo (Cod_rim,Cod_seq_veiculo)" +
                            "VALUES(" + Convert.ToInt32(txtCetil.Text) + "," + Convert.ToInt32(txtCodPlaca.Text) + ")", mConn);
                
                        //Executa a Query SQL
                        command.ExecuteNonQuery();

                        mostrarResultados();
                        MessageBox.Show("Operação realizada com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                        this.Close();                
                    }
            catch 
                    {

                        MessageBox.Show("Placa já vinculada", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toolStripTextBox1.Text = "Verifique se todos os campos estão corretos.";
                    }

            // Fecha a conexão
            mConn.Close();

            //Mensagem de Sucesso
           
            // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"
            
            LimpaCampos();
            DesabilitaTextBox();
            //HabilitaRadionButtons();
            //mostrarResultados();
            //UncheckedRadioButtons();                      
                        
        }

        private void txtCetil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCodRI.Text = txtCetil.Text;
                Global.RI.cetil = txtCetil.Text;
                mostrarResultados();
              
            }
            else
            {
                txtCetil.Focus();
            }
        }

        private void bt_Excluir_Click(object sender, EventArgs e)
        {
            if (label9.Text == "0 registro")
            {
                MessageBox.Show("Não há veículo cadastrado para exclusão", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                MessageBox.Show("Duplo clique no item desejado!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }           
                            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void rbNovo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtCodRI.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCodPlaca.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            int codigo1 = Convert.ToInt32(txtCodRI.Text);
            int codigo2 = Convert.ToInt32(txtCodPlaca.Text);
            Excluir(codigo1, codigo2);
            
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            mostrarResultados();
            this.Close();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
