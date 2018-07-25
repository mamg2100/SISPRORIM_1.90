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
    public partial class Unidades : Form
    {
       
        int codigo = 0;
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        public String temp;
        int flagInclusao = 1; // 0 - para alteração de dados  1- para inclusão de dados  2 - para exclusão de dados  
       
        public Unidades()
        {
            InitializeComponent();
        }

        private void Unidades_Load(object sender, EventArgs e)
        {
            mostrarResultados();
            
        }

        private void mostrarResultados()
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            if (rbPorCodigo.Checked == true)                
                mAdapter = new MySqlDataAdapter("SELECT * FROM unidade ORDER BY Cod_unidade DESC", mConn);
            else
                if (rbPorNome.Checked == true)
                    mAdapter = new MySqlDataAdapter("SELECT * FROM unidade ORDER BY Nome_unidade", mConn);
                else
                      if (rbPorContato.Checked==true)
                        mAdapter = new MySqlDataAdapter("SELECT * FROM unidade ORDER by Resp_unidade", mConn);
                      else                      
                        mAdapter = new MySqlDataAdapter("SELECT * FROM unidade ORDER by Cod_unidade DESC", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "unidade");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "unidade";

            //Renomeia as colunas
            dataGridView1.Columns[0].HeaderText = "Codigo";
            dataGridView1.Columns[1].HeaderText = "Unidade";
            dataGridView1.Columns[2].HeaderText = "Tipo";
            dataGridView1.Columns[3].HeaderText = "Lograd.";
            dataGridView1.Columns[4].HeaderText = "Nº";
            dataGridView1.Columns[5].HeaderText = "Bairro";
            dataGridView1.Columns[6].HeaderText = "Cidade";
            dataGridView1.Columns[7].HeaderText = "UF";
            dataGridView1.Columns[8].HeaderText = "Fone 1";
            dataGridView1.Columns[9].HeaderText = "Fone 2";
            dataGridView1.Columns[10].HeaderText = "Contato";
            dataGridView1.Columns[11].HeaderText = "Fone/Contato";

            calculaQuantidadeRegistros();
            //dataGridView1.Enabled = false;
           
        }

        private void calculaQuantidadeRegistros()
        {
            int registro;
            registro = dataGridView1.RowCount - 1;
            if (registro == 1 || registro == 0)
                label9.Text = registro + " registro";
            else
                label9.Text = registro + " registros";

        }

        private void Excluir(int codigo)
        {
            btnOK.Visible = true;
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
                        cmd.CommandText = "Delete FROM prorim.unidade Where Cod_unidade=" + codigo;
                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível excluir a unidade " + codigo);
                        }
                    }                    
                    catch
                    {
                        MessageBox.Show("[Erro] Delete FROM prorim.unidade Where Cod_unidade=" + codigo, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    finally
                    {
                        MessageBox.Show("Unidade de código [ " + codigo +" ] excluída com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DesabilitaTextBox();
                        limparCampos();
                        mConn.Close();
                        mostrarResultados();
                    }
                    
                }
            
        }
        

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            
        }

        private void HabilitaTextBox()
        {
            textBox3.Enabled = false;
            txtSetor.Enabled = true;
            cmbTipo.Enabled = true;
            txtEndereço.Enabled = true;
            txtNumero.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            cmbUF.Enabled = true;
            txtFone1.Enabled = true;
            txtFone2.Enabled = true;
            txtResp.Enabled = true;
            txtFoneContato.Enabled = true;                       

        }

        private void txtCheckCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtCheckCodigo.Text != "")
                {
                    temp = txtCheckCodigo.Text;
                    codigo = Convert.ToInt32(temp);
                    PesquisaPorCodigo(codigo);
                    txtCheckCodigo.Text = "";
                }               
            }

        }
        

        private void PesquisaPorCodigo(int codigo)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 

            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM unidade Where Cod_unidade=" + codigo, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "unidade");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "unidade";

        }

        private void txtCheckIdentificação_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                temp = txtCheckIdentificação.Text;
                PesquisaPorSetor(temp);
                txtCheckIdentificação.Text = "";
            }

            else
            {
                // MessageBox.Show("Tecle 'ENTER'");
            }

        }

        private void PesquisaPorSetor(string temp)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM unidade WHERE Nome_unidade " + "LIKE " + "'%" + temp + "%'", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "unidade");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "unidade";

        }

             
        private void Alterar(int codigo)
        {
                        //conexao
                        mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                        mConn.Open();
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = mConn;

                            // Vamos deixar essa codificação abaixo - que não funcionou - para comparação com a que funciona logo abaixo.
                            // cmd.CommandText = "UPDATE fornecedor SET Nome_fornecedor=" + "'" + txtFornecedor.Text + "'," + " End_fornecedor =" + "'" + txtEndereço + "'," +
                            //" Fone1_fornecedor =" + "'" + txtFone1 + "'," + " Fone2_fornecedor =" + "'" + txtFone2 + "'," + " Email_fornecedor =" + "'" + txtEmail + "'" + "Where Cod_fornecedor = " + codigo;

                            cmd.CommandText = "UPDATE unidade SET Nome_unidade ='" + txtSetor.Text + "'," + "Tipo_unidade='"  
                                + cmbTipo.Text + "'," + "End_unidade='"  + txtEndereço.Text + "'," + "Nr_endereco='" 
                                + txtNumero.Text + "'," + "Bairro_unidade='" + txtBairro.Text + "'," + "Cidade_unidade='" 
                                + txtCidade.Text + "'," + "UF_unidade='" + cmbUF.Text + "'," + "Fone1_unidade='" 
                                + txtFone1.Text + "'," + "Fone2_unidade='" + txtFone2.Text + "'," + "Resp_unidade='"+ txtResp.Text + "'," 
                                + "Fone_contato='" + txtFoneContato.Text + "'"+ "WHERE Cod_unidade=" + codigo;

                            MessageBox.Show("Registro " + "'" + codigo + "'" + " Alterado com sucesso.","informação");

                            //mConn.Open();
                            int resultado = cmd.ExecuteNonQuery();
                            if (resultado != 1)
                            {
                                throw new Exception("Não foi possível alterar os dados da 'Unidade' " + codigo);
                            }


                        }
                        catch
                        {
                            MessageBox.Show("UPDATE unidade SET Nome_unidade ='" + txtSetor.Text + "'," + "Tipo_unidade='"  
                                + cmbTipo.Text + "'," + "End_unidade='"  + txtEndereço.Text + "'," + "Nr_endereco='" 
                                + txtNumero.Text + "'," + "Bairro_unidade='" + txtBairro.Text + "'," + "Cidade_unidade='" 
                                + txtCidade.Text + "'," + "UF_unidade='" + cmbUF.Text + "'," + "Fone1_unidade='" 
                                + txtFone1.Text + "'," + "Fone2_unidade='" + txtFone2.Text + "'," + "Resp_unidade='"+ txtResp.Text + "'," 
                                + "Fone_contato='" + txtFoneContato.Text + "'"+ " WHERE Cod_unidade=" + codigo, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        finally
                        {
                            mConn.Close();
                            limparCampos();
                            DesabilitaTextBox();                            
                            mostrarResultados();                           
                    }            
        }

        private void DesabilitaTextBox()
        {          
            textBox3.Enabled = false;
            txtSetor.Enabled = false;
            txtEndereço.Enabled = false;
            txtFone1.Enabled = false;
            txtFone2.Enabled = false;
            txtResp.Enabled = false;
            cmbTipo.Enabled = false;
            txtNumero.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            cmbUF.Enabled = false;
            txtFoneContato.Enabled = false;
            
        }

        private void limparCampos()
        {
            textBox3.Text = "";
            txtSetor.Text = "";
            txtEndereço.Text = "";
            txtFone1.Text = "";
            txtFone2.Text = "";
            txtResp.Text = "";
            cmbTipo.Text = "";
            txtNumero.Text = "";
            txtBairro.Text = "";
            cmbUF.Text = "";
            txtFoneContato.Text = "";
        }

        private void Gravar()
        {

            // Início da Conexão com indicação de qual o servidor, nome de base de dados a utilizar

            /* É aconselhável criar um utilizador com password. Para acrescentar a password é somente
               necessário acrescentar o seguinte código a seguir ao uid=root;password=xxxxx
             */

            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

            // Abre a conexão
            mConn.Open();

            try
            {
                //Query SQL
                MySqlCommand command = new MySqlCommand("INSERT INTO unidade (Nome_unidade,Tipo_unidade,End_unidade,Nr_endereco,Bairro_unidade,Cidade_unidade,UF_unidade,Fone1_unidade,Fone2_unidade,Resp_unidade,Fone_contato)" +
                "VALUES('" + txtSetor.Text + "','" + cmbTipo.Text + "','" + txtEndereço.Text + "','" + txtNumero.Text + "','" + txtBairro.Text + "','" + txtCidade.Text
                + "','" + cmbUF.Text + "','" + txtFone1.Text + "','" + txtFone2.Text + "','" + txtResp.Text + "','" + txtFoneContato.Text + "')", mConn);
                // Esta representando a sequencia "...VALUES(txtSetor,txtEndereço,...)"

                //Executa a Query SQL
                command.ExecuteNonQuery();

                // Fecha a conexão
                mConn.Close();

                //Mensagem de Sucesso
                MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"

                limparCampos();
                DesabilitaTextBox();
                uncheckedrb();
                caixaAlta();
            }

            catch
            {
                //Mensagem de Erro
                MessageBox.Show("Erro de gravação!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }

            mostrarResultados();
            DesabilitaTextBox();
            
        }

        private void caixaAlta()
        {
            txtSetor.Text = txtSetor.Text.ToUpper();
            txtEndereço.Text = txtEndereço.Text.ToUpper();
            txtBairro.Text = txtBairro.Text.ToUpper();
            txtCidade.Text = txtCidade.Text.ToUpper();
            txtResp.Text = txtResp.Text.ToUpper();
        }

       
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbPorCodigo_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "ordenando por código...";
            mostrarResultados();
        }

        private void rbPorNome_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "ordenando por nome...";
            mostrarResultados();
        }

        private void rbPorContato_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "ordenando por contato...";
            mostrarResultados();
        }

        
        private void txtSetor_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtSetor.Text = txtSetor.Text.ToUpper();
                cmbTipo.Focus();
            }
            else
            {
                txtSetor.Focus();
            }
        
        }

        private void txtEndereço_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtEndereço.Text = txtEndereço.Text.ToUpper();
                txtNumero.Focus();
            }
            else
            {
                txtEndereço.Focus();
            }
        
        }

        private void txtFone1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtFone2.Focus();
            }
            else
            {
                txtFone1.Focus();
            }
        
        }

        private void txtFone2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtResp.Focus();
            }
            else
            {
                txtFone2.Focus();
            }
        
        }

        private void txtResp_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtResp.Text = txtResp.Text.ToUpper();
                txtFoneContato.Focus();
            }
            else
            {
                txtResp.Focus();
            }
        
        }       

        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtEndereço.Focus();
            }
            else
            {
                cmbTipo.Focus();
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEndereço.Focus();
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtBairro.Focus();
            }
            else
            {
                txtNumero.Focus();
            }
        }

        private void txtBairro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtBairro.Text = txtBairro.Text.ToUpper();
                txtCidade.Focus();
            }
            else
            {
                txtBairro.Focus();
            }

        }

        private void txtCidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtCidade.Text = txtCidade.Text.ToUpper();
                cmbUF.Focus();
            }
            else
            {
                txtCidade.Focus();
            }
        }

        private void cmbUF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtFone1.Focus();
            }
            else
            {
                cmbUF.Focus();
            }
        }

        private void txtFoneContato_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                btnOK.Focus();
            }
            else
            {
                txtFoneContato.Focus();
            }
        }

        private void cmbUF_SelectedIndexChanged(object sender, EventArgs e)
        {
                txtFone1.Focus();
        
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void txtEndereço_Leave(object sender, EventArgs e)
        {
            txtEndereço.Text = txtEndereço.Text.ToUpper();
            txtEndereço.BackColor = Color.White;
        }

        private void txtSetor_Leave(object sender, EventArgs e)
        {
            txtSetor.Text = txtSetor.Text.ToUpper();
            txtEndereço.BackColor = Color.White;
        }

        private void txtBairro_Leave(object sender, EventArgs e)
        {
            txtBairro.Text = txtBairro.Text.ToUpper();
            txtBairro.BackColor = Color.White;
        }

        private void txtResp_Leave(object sender, EventArgs e)
        {
            txtResp.Text = txtResp.Text.ToUpper();
            txtResp.BackColor = Color.White;
        }

        private void txtCidade_Leave(object sender, EventArgs e)
        {
            txtCidade.Text = txtCidade.Text.ToUpper();
            txtCidade.BackColor = Color.White;
        }

       private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Enabled == true)
                dataGridView1.Enabled = false;
            else
                dataGridView1.Enabled = true;
        }

       private void txtNumero_Leave(object sender, EventArgs e)
        {
            txtNumero.BackColor = Color.White;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            txtCep.BackColor = Color.White;
        }

        private void txtFone1_Leave(object sender, EventArgs e)
        {
            txtFone1.BackColor = Color.White;
        }

        private void txtFone2_Leave(object sender, EventArgs e)
        {
            txtFone2.BackColor = Color.White;
        }

        private void txtFoneContato_Leave(object sender, EventArgs e)
        {
            txtFoneContato.BackColor = Color.White;
        }

        private void txtCheckCodigo_Leave(object sender, EventArgs e)
        {
            txtCheckCodigo.BackColor = Color.White;
        }

        private void txtCheckIdentificação_Leave(object sender, EventArgs e)
        {
            txtCheckIdentificação.BackColor = Color.White;
        }

        private void txtCheckCodigo_Enter(object sender, EventArgs e)
        {
            txtCheckCodigo.BackColor = Color.Yellow;
        }

        private void txtCheckIdentificação_Enter(object sender, EventArgs e)
        {
            txtCheckIdentificação.BackColor = Color.Yellow;
        }

        private void txtSetor_Enter(object sender, EventArgs e)
        {
            txtSetor.BackColor = Color.Yellow;
        }

        private void txtEndereço_Enter(object sender, EventArgs e)
        {
            txtEndereço.BackColor = Color.Yellow;
        }

        private void txtNumero_Enter(object sender, EventArgs e)
        {
            txtNumero.BackColor = Color.Yellow;
        }

        private void txtBairro_Enter(object sender, EventArgs e)
        {
            txtBairro.BackColor = Color.Yellow;
        }

        private void txtCidade_Enter(object sender, EventArgs e)
        {
            txtCidade.BackColor = Color.Yellow;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            txtCep.BackColor = Color.Yellow;
        }

        private void txtFone1_Enter(object sender, EventArgs e)
        {
            txtFone1.BackColor = Color.Yellow;
        }

        private void txtFone2_Enter(object sender, EventArgs e)
        {
            txtFone2.BackColor = Color.Yellow;
        }

        private void txtResp_Enter(object sender, EventArgs e)
        {
            txtResp.BackColor = Color.Yellow;
        }

        private void txtFoneContato_Enter(object sender, EventArgs e)
        {
            txtFoneContato.BackColor = Color.Yellow;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.buscacep.correios.com.br/");
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            uncheckedrb();
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;            
            btnOK.Visible = true;
            rbPorCodigo.Checked = false;
            rbPorContato.Checked = false;
            rbPorNome.Checked = false;
            flagInclusao = 2;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            uncheckedrb();
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = true;
            rbPorCodigo.Checked = false;
            rbPorContato.Checked = false;
            rbPorNome.Checked = false;
            flagInclusao = 0;
        }

        private void uncheckedrb()
        {
            if (rbPorCodigo.Checked == true || rbPorContato.Checked == true || rbPorNome.Checked == true)
            {
                rbPorCodigo.Checked = false;
                rbPorContato.Checked = false;
                rbPorNome.Checked = false;
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            flagInclusao = 1;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = true;
            HabilitaTextBox();
            txtSetor.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Campos obrigatórios na inclusão.
            // Nome, Login, Senha e Tipo de Usuário. Esse último se - não marcado - ficará como usuário tipo comum.
            // Portanto a verificação será dos campos três iniciais

            if (txtSetor.Text == "")
            {
                if (flagInclusao == 1)
                {
                    MessageBox.Show("Campo não pode ficar vazio. Entre pelo menos com a identificação da Unidade para incluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtSetor.Focus();
                }
                else 
                {
                    if (flagInclusao == 0)
                    {
                        MessageBox.Show("Campo não pode ficar vazio. Clique na planilha na linha correspondente à Unidade cujos dados devam ser alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        
                    }
                    else
                    {
                        MessageBox.Show("Campo não pode ficar vazio. Clique na planilha na linha correspondente à Unidade cujos dados devam ser excluídos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }       
                                          
                }
            }
            else
            {
              // O botão OK serve tanto para inclusão quanto para alteração de dados dos usuários
              // Foi criada um variável flag para informar o que estamos fazendo. Inclusão ou alteração.
                btnOK.Visible = true;
                if (flagInclusao == 1)
              {
                 Gravar();
                 btnIncluir.Enabled = true;
                 btnAlterar.Enabled = true;
                 btnExcluir.Enabled = true;
                 btnOK.Visible = false;
              }
                else
              {  // essa linha só serve para casos de alteração de dados e exclusão
                 codigo = Convert.ToInt32(textBox3.Text);
                   if (flagInclusao == 0){
                       Alterar(codigo);
                       btnIncluir.Enabled = true;
                       btnAlterar.Enabled = true;
                       btnExcluir.Enabled = true;
                       btnOK.Visible = false;
                   }else{
                       Excluir(codigo);
                       btnIncluir.Enabled = true;
                       btnAlterar.Enabled = true;
                       btnExcluir.Enabled = true;
                       btnOK.Visible = false;
                   }
              }
            }             
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox3.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtSetor.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbTipo.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEndereço.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtNumero.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtBairro.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCidade.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbUF.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFone1.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFone2.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtResp.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFoneContato.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            // para alterar os dados, deve-se habilitar os textBox

            btnOK.Visible = true;

            if (flagInclusao == 0 || flagInclusao==1)
            {
                HabilitaTextBox();
            }
            else {
                DesabilitaTextBox();
            }
        }
    }
}

