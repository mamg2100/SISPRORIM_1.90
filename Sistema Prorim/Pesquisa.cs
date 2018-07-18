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
    public partial class Pesquisa : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        //String temp;
        //int codigo;
        //int estadocodigo = 1;
        //int estadoident = 1;
        //int estado = 0;
        
        public Pesquisa()
        {
            InitializeComponent();
        }

        private void Pesquisa_Load(object sender, EventArgs e)
        {
            mostrarResultados();
         
        }
        private void mostrarResultados()
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False; server=localhost; database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            if (rbPorCodigo.Checked == true)
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM rim ORDER BY Cod_rim", mConn);
            else
                if (rbPorNomeUnidade.Checked == true)
                    mAdapter = new MySqlDataAdapter("SELECT * FROM rim ORDER BY Nome_unidade", mConn);
                else
                    if (rbFornecedor.Checked == true)
                        mAdapter = new MySqlDataAdapter("SELECT * FROM rim ORDER by Nome_fornecedor", mConn);
                    else
                        mAdapter = new MySqlDataAdapter("SELECT * FROM rim ORDER by descricao", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "rim");


            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "rim";

            
            dataGridView1.Columns[0].HeaderText = "Codigo";

            //omitndo a exibição de colunas do grid

            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[18].Visible = false;
            dataGridView1.Columns[19].Visible = false;
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[23].Visible = false;
            dataGridView1.Columns[24].Visible = false;
            dataGridView1.Columns[25].Visible = false;
            dataGridView1.Columns[26].Visible = false;
                        
            label9.Text = dataGridView1.RowCount.ToString() + " registros";

            // populando cmbUnidade

            mAdapter = new MySqlDataAdapter("SELECT * FROM unidade ORDER BY Nome_unidade", mConn);
            DataTable dt = new DataTable();
            mAdapter.Fill(dt);
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbEscolha.Items.Add(dt.Rows[i]["Nome_Unidade"]);
                }
            }
            catch (MySqlException erro)
            {
                throw erro;
            }

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

                 }

        
        

        private void rbPorNome_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
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
                      

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        
        
        private void bt_visualizar_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbEscolha.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdescricao.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDO.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCetil.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdataCetil.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtvalorEstimado.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtvalorReal.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtProcesso.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtAutorizacao.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDataAutorizacao.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbSetor.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdataEnvio.Text = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataContabilidade.Text = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataOrdenador1.Text = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataCompras1.Text = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataOrdenador2.Text = dataGridView1[16, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataCompras2.Text = dataGridView1[17, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataDipe.Text = dataGridView1[18, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbcadastradoPor.Text = dataGridView1[19, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdtCadastro.Text = dataGridView1[20, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbFornecedor.Text = dataGridView1[21, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtnotaFiscal.Text = dataGridView1[22, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdataNotaFiscal.Text = dataGridView1[23, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtObs.Text = dataGridView1[24, dataGridView1.CurrentCellAddress.Y].Value.ToString();

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            txtCodigo.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbEscolha.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdescricao.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDO.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCetil.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdataCetil.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtvalorEstimado.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtvalorReal.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtProcesso.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtAutorizacao.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtDataAutorizacao.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbSetor.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdataEnvio.Text = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataContabilidade.Text = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataOrdenador1.Text = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataCompras1.Text = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataOrdenador2.Text = dataGridView1[16, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataCompras2.Text = dataGridView1[17, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            lblDataDipe.Text = dataGridView1[18, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbcadastradoPor.Text = dataGridView1[19, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdtCadastro.Text = dataGridView1[20, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbFornecedor.Text = dataGridView1[21, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtnotaFiscal.Text = dataGridView1[22, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtdataNotaFiscal.Text = dataGridView1[23, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtObs.Text = dataGridView1[24, dataGridView1.CurrentCellAddress.Y].Value.ToString();
        }

        private void rbPorCodigo_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void rbPorNomeUnidade_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void rbFornecedor_CheckedChanged(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
                
    }
}

