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
    public partial class Fornecedor : Form
    {
        
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        String temp;
        int codigo;
        bool ordem;
        int flagInclusao = 1;
        
        public Fornecedor()
        {
            InitializeComponent();
        }

        private void Fornecedor_Load(object sender, EventArgs e)
        {
            //Ao abrir o form dataGrid já é populado
            ordem = true;
            mostrarResultados();
            //Global.Logon.ipservidor = "192.168.5.88";
        }
        
    
        private void mostrarResultados()

            // popula o dataGridView
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False; server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            if (rbPorCodigo.Checked == true)
                // ordena a tabela de acordo com o critério estabelecido
                if (ordem)
                {
                    mAdapter = new MySqlDataAdapter("SELECT * FROM fornecedor ORDER BY Cod_fornecedor ASC", mConn);
                }
                else
                {
                    mAdapter = new MySqlDataAdapter("SELECT * FROM fornecedor ORDER BY Cod_fornecedor DESC ", mConn);
                }
            else
                if (rbPorNome.Checked == true)
                    mAdapter = new MySqlDataAdapter("SELECT * FROM fornecedor ORDER BY Nome_fornecedor", mConn);
                else
                    mAdapter = new MySqlDataAdapter("SELECT * FROM fornecedor ORDER by Numero_tipo", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "fornecedor");
            
            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "fornecedor";
            //Renomeia as colunas
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Fornecedor";
            dataGridView1.Columns[2].HeaderText = "Tipo";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "CPF/CNPJ";
            dataGridView1.Columns[4].HeaderText = "Lograd.";
            dataGridView1.Columns[5].HeaderText = "Endereco";
            dataGridView1.Columns[6].HeaderText = "Nr";
            dataGridView1.Columns[7].HeaderText = "Bairro";
            dataGridView1.Columns[8].HeaderText = "Cidade";
            dataGridView1.Columns[9].HeaderText = "UF";
            dataGridView1.Columns[10].HeaderText = "Fone 1";
            dataGridView1.Columns[11].HeaderText = "Fone 2";
            dataGridView1.Columns[12].HeaderText = "E-mail";
            dataGridView1.Columns[13].HeaderText = "Site";
            dataGridView1.Columns[14].HeaderText = "Contato";
            dataGridView1.Columns[15].HeaderText = "E-mail Contato";
            

            consultaTotalRegistros();

            }

        private void consultaTotalRegistros()
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
            {
                {
                    //conexao
                    mConn = new MySqlConnection("Persist Security Info=False; server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    mConn.Open();
                    try
                    {
                        //mConn.ConnectionString = Dados.StringDeConexao;
                        //command
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = mConn;
                        cmd.CommandText = "delete from fornecedor where Cod_fornecedor = " + codigo;
                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível excluir o fornecedor " + codigo);
                        }
                    }
                    /*catch (MySqlException ex)
                    {
                        throw new Exception("Servidor SQL Erro:" + ex.Number);
                    }*/
                    catch 
                    {
                        MessageBox.Show("Falha na conexão com o Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                    }
                    finally
                    {
                        mConn.Close();
                        mostrarResultados();
                    }

                    MessageBox.Show("Excluido o item de código " + "'" + codigo + "'" + " com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mostrarResultados();
                    LimpaCampos();
                    DesabilitaTextBox();
                    
                }
            }

        }       
        
        private void Gravar()
        {
            {
                // Início da Conexão com indicação de qual o servidor, nome de base de dados a utilizar

                mConn = new MySqlConnection("Persist Security Info=False; server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

                // Abre a conexão
                mConn.Open();

                try
                {
                    //Query SQL
                    MySqlCommand command = new MySqlCommand("INSERT INTO fornecedor (Nome_fornecedor,Tipo_fornecedor,Numero_tipo,Tipo_logradouro,End_fornecedor,Nr_endereco,Bairro_fornecedor,Cidade_fornecedor,UF_fornecedor,Fone1_fornecedor,Fone2_fornecedor,Email_fornecedor,Site_fornecedor,Contato,Email_contato)" +
                    "VALUES('" + txtFornecedor.Text + "','" + cmbTipoFornecedor.Text + "','" + txtTipoNumero.Text + "','" + cmbTipo.Text + "','"
                    + txtEndereço.Text + "','" + txtNumero.Text + "','" + txtBairro.Text + "','" + txtCidade.Text + "','" + cmbUF.Text + "','" + txtFone1.Text + "','"
                    + txtFone2.Text + "','" + txtEmail.Text + "','" + txtSite.Text + "','" + txtResp.Text + "','" + txtEmailContato.Text + "')", mConn);
                    // Esta representando a sequencia "...VALUES(txtFornecedor,txtEndereço,...)"

                    //Executa a Query SQL
                    command.ExecuteNonQuery();
                }

                catch 
                {

                    MessageBox.Show("INSERT INTO fornecedor (Nome_fornecedor,Tipo_fornecedor,Numero_tipo,Tipo_logradouro,End_fornecedor,Nr_endereco,Bairro_fornecedor,Cidade_fornecedor,UF_fornecedor,Fone1_fornecedor,Fone2_fornecedor,Email_fornecedor,Site_fornecedor,Contato,Email_contato)" +
                    "VALUES('" + txtFornecedor.Text + "','" + cmbTipoFornecedor.Text + "','" + txtTipoNumero.Text + "','" + cmbTipo.Text + "','"
                    + txtEndereço.Text + "','" + txtNumero.Text + "','" + txtBairro.Text + "','" + txtCidade.Text + "','" + cmbUF.Text + "','" + txtFone1.Text + "','"
                    + txtFone2.Text + "','" + txtEmail.Text + "','" + txtSite.Text + "','" + txtResp.Text + "','" + txtEmailContato.Text + "')", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                }
                // Fecha a conexão
                mConn.Close();

                //Mensagem de Sucesso
                MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"
                mostrarResultados();
                LimpaCampos();
                DesabilitaTextBox();                
            }
        }
    
        private void LimpaCampos()
        {
            textBox3.Text = "";
            txtFornecedor.Text = "";
            cmbTipoFornecedor.Text = "";
            txtTipoNumero.Text = "";
            cmbTipo.Text = "";
            txtEndereço.Text = "";
            txtNumero.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            cmbUF.Text = "";
            txtFone1.Text = "";
            txtFone2.Text = "";
            txtEmail.Text = "";
            txtSite.Text = "";
            txtResp.Text = "";
            txtEmailContato.Text = ""; 

        }

        private void rbPorCodigo_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "Ordenando por código sequencial...";
            mostrarResultados();
        }

        private void rbPorNome_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "Ordenando por nome fornecedor...";
            mostrarResultados();
        }

        private void rbPorCodigo_Click(object sender, EventArgs e)
        {
            tssMensagem.Text = "Ordenando por código sequencial...";
            mostrarResultados();
        }

        private void rbPorNome_Click(object sender, EventArgs e)
        {
            tssMensagem.Text = "Ordenando por nome fornecedor...";
            mostrarResultados();
        }

        private void rbPorEmail_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }
        
        private void Alterar(int codigo)
        {
            {
                {
                    //conexao
                    mConn = new MySqlConnection("Persist Security Info=False; server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    mConn.Open();
                    try
                    {
                        //mConn.ConnectionString = Dados.StringDeConexao;
                        
                        //command
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = mConn;
                        
                        // Vamos deixar essa codificação abaixo - que não funcionou - para comparação com a que funciona logo abaixo.
                        //cmd.CommandText = "UPDATE fornecedor SET Nome_fornecedor=" + "'" + txtFornecedor.Text + "'," + " End_fornecedor =" + "'" + txtEndereço + "'," +
                        //" Fone1_fornecedor =" + "'" + txtFone1 + "'," + " Fone2_fornecedor =" + "'" + txtFone2 + "'," + " Email_fornecedor =" + "'" + txtEmail + "'" + "Where Cod_fornecedor = " + codigo;

                        /*
                        cmd.CommandText = "UPDATE fornecedor SET Nome_fornecedor =" + "'" + txtFornecedor.Text + "'," + "Tipo_fornecedor=" + "'" + txtEndereço.Text + "'," 
                            + "Fone1_fornecedor=" + "'" + txtFone1.Text + "'," + "Fone2_fornecedor=" + "'" + txtFone2.Text + "'," + "Email_fornecedor=" + "'" + txtEmail.Text 
                            + "'" + "WHERE Cod_Fornecedor=" + codigo;
                        */
                            cmd.CommandText = "UPDATE fornecedor SET " + 
                            "Nome_fornecedor='" +  txtFornecedor.Text + "'," +
                            "Tipo_fornecedor='" + cmbTipoFornecedor.Text + "'," + 
                            "Numero_tipo='" + txtTipoNumero.Text + "'," +
                            "Tipo_logradouro='" + cmbTipo.Text + "'," +
                            "End_fornecedor='" + txtEndereço.Text + "'," +
                            "Nr_endereco='" + txtNumero.Text + "'," +
                            "Bairro_fornecedor='" + txtBairro.Text + "'," +
                            "Cidade_fornecedor='" + txtCidade.Text + "'," +
                            "UF_fornecedor='" + cmbUF.Text + "'," +
                            "Fone1_fornecedor='" + txtFone1.Text + "'," +
                            "Fone2_fornecedor='" + txtFone2.Text + "'," +
                            "Email_fornecedor='" + txtEmail.Text + "'," +                            
                            "Site_fornecedor='" + txtSite.Text + "'," +                            
                            "Contato='" + txtResp.Text + "'," +
                            "Email_contato='" + txtEmailContato.Text + "'" +
                             "Where Cod_fornecedor=" + codigo;
                        /*
                        UPDATE fornecedor SET 
                        Nome_fornecedor='txtFornecedor.Text',
                        Tipo_fornecedor='cc',
                        Numero_tipo='234',
                        Tipo_logradouro='cmbTipo.Text',
                        End_fornecedor='txtEndereço.Text',
                        Nr_endereco='234',
                        Bairro_fornecedor='txtBairro.Text',
                        Cidade_fornecedor='txtCidade.Text',
                        UF_fornecedor='UF',
                        Fone1_fornecedor='txtFone1.Text',
                        Fone2_fornecedor='txtFone2.Text',
                        Email_fornecedor='txtEmail.Text',
                        Site_fornecedor='txtSite.Text',
                        Contato='txtResp.Text',
                        Email_contato='txtEmailContato.Text' 
                        Where Cod_fornecedor=200;
                        */
                        
                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível alterar os dados do fornecedor " + codigo);
                        }
                        MessageBox.Show("Dados do fornecedor alterados com sucesso", "Informações", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    catch
                    {
                        MessageBox.Show("UPDATE fornecedor SET " + 
                            "Nome_fornecedor='" +  txtFornecedor.Text + "'," +
                            "Tipo_fornecedor='" + cmbTipoFornecedor.Text + "'," + 
                            "Numero_tipo='" + txtTipoNumero.Text + "'," +
                            "Tipo_logradouro='" + cmbTipo.Text + "'," +
                            "End_fornecedor='" + txtEndereço.Text + "'," +
                            "Nr_endereco='" + txtNumero.Text + "'," +
                            "Bairro_fornecedor='" + txtBairro.Text + "'," +
                            "Cidade_fornecedor='" + txtCidade.Text + "'," +
                            "UF_fornecedor='" + cmbUF.Text + "'," +
                            "Fone1_fornecedor='" + txtFone1.Text + "'," +
                            "Fone2_fornecedor='" + txtFone2.Text + "'," +
                            "Email_fornecedor='" + txtEmail.Text + "'," +                            
                            "Site_fornecedor='" + txtSite.Text + "'," +                            
                            "Contato='" + txtResp.Text + "'," +
                            "Email_contato='" + txtEmailContato.Text + "'" +
                             "Where Cod_fornecedor=" + codigo, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                    }
                    finally
                    {
                        mConn.Close();
                        mostrarResultados();
                        //uncheckedRadiodButtons();
                        
                        LimpaCampos();
                        DesabilitaTextBox();                        
                    }

                }
            }
                
        }
        
       
        private void alimentaTextBox(int codigo)
        {
            textBox3.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFornecedor.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbTipoFornecedor.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtTipoNumero.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbTipo.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEndereço.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtNumero.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtBairro.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCidade.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbUF.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFone1.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFone2.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEmail.Text = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtSite.Text = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtResp.Text = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEmailContato.Text = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();

        }

              
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();           
        }
                              
        private void PesquisaPorCodigo(int codigo)
        {
           
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False; server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM fornecedor Where Cod_fornecedor=" + codigo, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "fornecedor");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "fornecedor";

        }

        private void Pesquisa(int codigo)
        {
            
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False; server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM fornecedor Where Cod_fornecedor=" + codigo, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "fornecedor");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "fornecedor";

        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void txtCheckIdentificação_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                temp = txtIdentificação.Text;
                PesquisaPorFornecedor(temp);
                LimpaCheckBoxes();
                txtIdentificação.Text = "";

            }
            else { }

            consultaTotalRegistros(); 

        }

        private void LimpaCheckBoxes()
        {
            txtCodigo.Text = "";
            txtIdentificação.Text = "";

        }


        private void PesquisaPorFornecedor(string temp)
        {
            
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False; server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM fornecedor WHERE Nome_fornecedor " + "LIKE " + "'%" + temp + "%'", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "fornecedor");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "fornecedor";
        }

        private void txtCheckIdentificação_TextChanged(object sender, EventArgs e)
        {

        }

        private void HabilitaTextBox()
        {
            txtFornecedor.Enabled = true;
            txtFornecedor.Focus();
            txtTipoNumero.Enabled = true;
            txtNumero.Enabled = true;
            cmbTipo.Enabled = true;
            txtEndereço.Enabled = true;
            txtNumero.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            cmbUF.Enabled = true;
            txtFone1.Enabled = true;
            txtFone2.Enabled = true;
            txtEmail.Enabled = true;
            txtSite.Enabled = true;
            txtResp.Enabled = true;
            txtEmailContato.Enabled = true;
            cmbTipoFornecedor.Enabled = true;
            
        }

        private void DesabilitaTextBox()
        {
            txtFornecedor.Enabled = false;
            txtFornecedor.Focus();
            txtTipoNumero.Enabled = false;
            txtNumero.Enabled = false;
            cmbTipo.Enabled = false;
            txtEndereço.Enabled = false;
            txtNumero.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            cmbUF.Enabled = false;
            txtFone1.Enabled = false;
            txtFone2.Enabled = false;
            txtEmail.Enabled = false;
            txtSite.Enabled = false;
            txtResp.Enabled = false;
            txtEmailContato.Enabled = false;
            cmbTipoFornecedor.Enabled = false;
            
        }

        
        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox3.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFornecedor.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbTipoFornecedor.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtTipoNumero.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbTipo.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEndereço.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtNumero.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtBairro.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCidade.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbUF.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFone1.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFone2.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEmail.Text = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtSite.Text = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtResp.Text = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEmailContato.Text = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();

        }

        private void txtFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtFornecedor.Text = txtFornecedor.Text.ToUpper();
                cmbTipoFornecedor.Focus(); 
                
            }
            else
            {
                txtFornecedor.Focus();
            }
        
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
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
                txtEmail.Focus();
            }
            else
            {
                txtFone2.Focus();
            }
        }

               
        private void txtEndereço_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtFone1.Focus();
            }
            else
            {
                txtEndereço.Focus();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFornecedor.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbTipoFornecedor.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtTipoNumero.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbTipo.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEndereço.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtNumero.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtBairro.Text = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtCidade.Text = dataGridView1[8, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            cmbUF.Text = dataGridView1[9, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFone1.Text = dataGridView1[10, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFone2.Text = dataGridView1[11, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEmail.Text = dataGridView1[12, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtSite.Text = dataGridView1[13, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtResp.Text = dataGridView1[14, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEmailContato.Text = dataGridView1[15, dataGridView1.CurrentCellAddress.Y].Value.ToString();


            HabilitaTextBox();
        }
        
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtCodigo.Text != "")
                {
                    temp = txtCodigo.Text;
                    codigo = Convert.ToInt32(temp);
                    PesquisaPorCodigo(codigo);
                    txtCodigo.Text = "";
                }
                else
                {

                }
                                
            }

            consultaTotalRegistros();

        }


        private void bt_visualizar_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
                    }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtFornecedor_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbTipoFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtTipoNumero.Focus();
            }
            else
            {
                cmbTipoFornecedor.Focus();
            }
        
        }

        private void cmbTipoFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoFornecedor.Text == "PF")
            {
                lblTipoNumero.Text = "CPF";
            }
            else
            {
                lblTipoNumero.Text = "CNPJ";
            }

            txtTipoNumero.Enabled = true;
            txtTipoNumero.Focus();
            
        }

        private void txtTipoNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                cmbTipo.Focus();
            }
            else
            {
                txtTipoNumero.Focus();
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

        private void txtEndereço_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void cmbUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFone1.Focus();
        }

        private void txtFone1_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void txtFone2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtEmail.Focus();
            }
            else
            {
                txtFone2.Focus();
            }
        
        }

        private void txtEmail_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtEmail.Text = txtEmail.Text.ToLower();
                
                txtSite.Focus();
            }
            else
            {
                txtEmail.Focus();
            }
        
        }

        private void txtSite_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtSite.Text = txtSite.Text.ToLower();
                txtResp.Focus();
            }
            else
            {
                txtSite.Focus();
            }
        
        }

        private void txtResp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtEmailContato.Focus();
                txtResp.Text = txtResp.Text.ToUpper();
            }
            else
            {
                txtResp.Focus();
            }
        
        }

        private void txtBairro_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmailContato_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtEmailContato.Text = txtEmailContato.Text.ToLower();
               
                btnOK.Focus();
                
            }
            else
            {
                txtEmailContato.Focus();
            }
        
        }

        private void chkCrescente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCrescente.Checked == true)
            {
                chkDecrescente.Checked = false;
                ordem = true;
            }
            else
            {
                chkDecrescente.Checked = true;
                ordem = false;
            }

            rbPorCodigo.Checked = true; ;
            mostrarResultados();

        }

        private void chkDecrescente_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chkDecrescente.Checked == true)
            {
                chkCrescente.Checked = false;
                ordem = false;
            }
            else
            {
                chkCrescente.Checked = true;
                ordem = true;
            }

            rbPorCodigo.Checked = true; ;
            mostrarResultados();


        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            txtCodigo.BackColor = Color.White;
        }

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            txtCodigo.BackColor = Color.Yellow;
        }

        private void txtIdentificação_Leave(object sender, EventArgs e)
        {
            txtIdentificação.BackColor = Color.White;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            textBox4.BackColor = Color.White;
        }

        private void txtIdentificação_Enter(object sender, EventArgs e)
        {
            txtIdentificação.BackColor = Color.Yellow;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.BackColor = Color.Yellow;
        }

        private void txtFornecedor_Leave(object sender, EventArgs e)
        {
            txtFornecedor.BackColor = Color.White;
        }

        private void txtTipoNumero_Leave(object sender, EventArgs e)
        {
            txtTipoNumero.BackColor = Color.White;
        }

        private void txtEndereço_Leave(object sender, EventArgs e)
        {
            txtEndereço.BackColor = Color.White;
        }

        private void txtNumero_Leave(object sender, EventArgs e)
        {
            txtNumero.BackColor = Color.White;
        }

        private void txtBairro_Leave(object sender, EventArgs e)
        {
            txtBairro.BackColor = Color.White;
        }

        private void txtCidade_Leave(object sender, EventArgs e)
        {
            txtCidade.BackColor = Color.White;
        }

        private void txtFone1_Leave(object sender, EventArgs e)
        {
            txtFone1.BackColor = Color.White;
        }

        private void txtFone2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtFone2_Leave(object sender, EventArgs e)
        {
            txtFone2.BackColor = Color.White;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            txtEmail.BackColor = Color.White;
        }

        private void txtResp_Leave(object sender, EventArgs e)
        {
            txtResp.BackColor = Color.White;
        }

        private void txtSite_Leave(object sender, EventArgs e)
        {
            txtSite.BackColor = Color.White;
        }

        private void txtEmailContato_Leave(object sender, EventArgs e)
        {
            txtEmailContato.BackColor = Color.White;
        }

        private void txtFornecedor_Enter(object sender, EventArgs e)
        {
            txtFornecedor.BackColor = Color.Yellow;
        }

        private void txtTipoNumero_Enter(object sender, EventArgs e)
        {
            txtTipoNumero.BackColor = Color.Yellow;
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

        private void txtFone1_Enter(object sender, EventArgs e)
        {
            txtFone1.BackColor = Color.Yellow;
        }

        private void txtFone2_Enter(object sender, EventArgs e)
        {
            txtFone2.BackColor = Color.Yellow;
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            txtEmail.BackColor = Color.Yellow;
        }

        private void txtResp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtResp_Enter(object sender, EventArgs e)
        {
            txtResp.BackColor = Color.Yellow;
        }

        private void txtSite_Enter(object sender, EventArgs e)
        {
            txtSite.BackColor = Color.Yellow;
        }

        private void txtEmailContato_Enter(object sender, EventArgs e)
        {
            txtEmailContato.BackColor = Color.Yellow;
        }

        private void txtTipoNumero_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = true;
            flagInclusao = 2;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = true;
            flagInclusao = 0;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            flagInclusao = 1;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = true;
            HabilitaTextBox();
            txtFornecedor.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Campo obrigatório na inclusão:Identificação do Fornecedor

            // O botão OK serve tanto para inclusão quanto para alteração de dados dos usuários
                        // Foi criada um variável flag para informar o que estamos fazendo: Inclusão ou alteração.
                if (flagInclusao == 1)
                {
                    if (txtFornecedor.Text == "")
                    {                        
                        MessageBox.Show("Entre com pelo menos o nome do fornecedor para inclusão.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        txtFornecedor.Focus();
                        btnOK.Visible = true;
                    }
                    else
                    {
                        btnOK.Visible = false;
                        Gravar();
                    }
                }
                else
                {  // essa linha só serve para casos de alteração de dados e exclusão
                    
                    if (flagInclusao == 0)
                    {
                        if (textBox3.Text == "")
                        {
                            MessageBox.Show("Escolha na planilha o fornecedor cujos dados devam ser alterados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            btnOK.Visible = true;
                            btnOK.Enabled = true;
                        }
                        else
                        {
                            btnOK.Visible = false;
                            codigo = Convert.ToInt32(textBox3.Text);
                            Alterar(codigo);
                        }
                    }
                    else
                    {
                        if (textBox3.Text == "")
                        {
                            MessageBox.Show("Escolha na planilha o fornecedor cujos dados devam ser excluídos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            btnOK.Visible = true;
                            btnOK.Enabled = true;
                        }
                        else
                        {
                            btnOK.Visible = false;
                            codigo = Convert.ToInt32(textBox3.Text);
                            Excluir(codigo);
                        }
                    }
                }                    
                
                btnIncluir.Enabled = true;
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;
                //btnOK.Visible = false;
            }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOK.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.buscacep.correios.com.br/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.receita.fazenda.gov.br/PessoaJuridica/CNPJ/cnpjreva/Cnpjreva_Solicitacao.asp");
            
        }
    }        
 }
    


