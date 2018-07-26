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
    public partial class FSM : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        String temp;
        int codigo = 0;
        string tipousuario;
        int flagInclusao = 1;
        int tentativa = 1;
        String valor = "";

        public FSM()
        {
            InitializeComponent();
        }

        private void FSM_Load(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void mostrarResultados()
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            {
                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //if (rbPorCodigo.Checked == true)
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM sugestao ORDER BY idsugestao DESC", mConn);

                //else
                //if (rbPorNome.Checked == true)
                // mAdapter = new MySqlDataAdapter("SELECT * FROM usuario ORDER BY Nome_Usuario", mConn);
                //else
                //  mAdapter = new MySqlDataAdapter("SELECT * FROM usuario ORDER by Setor_Usuario", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "sugestao");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "sugestao";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Sugestão de Melhoria";
                dataGridView1.Columns[2].HeaderText = "FeedBack";
                dataGridView1.Columns[3].HeaderText = "Data";
                dataGridView1.Columns[4].HeaderText = "Situação Atual";
                dataGridView1.Columns[5].HeaderText = "Solicitante";
                dataGridView1.Columns[6].HeaderText = "Data Solução";
                dataGridView1.Columns[6].Visible = false;
            }
            
            int registro;
            registro = dataGridView1.RowCount - 1;
            if (registro < 2)
            {
                label3.Text = registro + " registro";
            }
            else
            {
                label3.Text = registro + " registros";
            }
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
            dataGridView1.Enabled = false;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnOK.Visible = true;
            HabilitaTextBox();
            txtMelhoria.Focus();
        }

        private void HabilitaTextBox()
        {
            txtMelhoria.Enabled = true;
            if (flagInclusao == 1)
            {
                txtFeedback.Enabled = false;
            }
            else {
                txtFeedback.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Campos obrigatórios na inclusão.
            // Nenhum campo é obrigatorio no BD, mas para evitar de gravar dados em branco 
            // colocamos uma restrição controlada pela aplicação logo abaixo.


            // O botão OK serve tanto para inclusão quanto para alteração de dados dos usuários
            // Foi criada um variável flag para informar o que estamos fazendo. Inclusão ou alteração.
            if (flagInclusao == 1)
            {
                if (txtMelhoria.Text != "")
                {                    
                    // Força a primeira vez que a sugestão está sendo inclusa ficar com status de registrada.
                    rbRegistrado.Checked = true;
                    Gravar();
                    btnOK.Visible = true;
                    btnIncluir.Enabled = true;
                    btnAlterar.Enabled = true;
                    btnExcluir.Enabled = true;
                    
                }
                else 
                {
                    btnOK.Visible = true;
                    MessageBox.Show("Não é permitida a gravação de informação com esse campo vazio.","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    txtMelhoria.Focus();                    
                }
            }
            else
            {  
                // essa linha só serve para casos de alteração de dados e exclusão
                if (flagInclusao == 0)
                {
                    if (txtMelhoria.Text == "")
                    {
                        MessageBox.Show("Escolha na planilha a linha correspondente à informação que deve ser alterada com DUPLO CLICK.","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        txtMelhoria.Focus();
                    }
                    else {
                        codigo = Convert.ToInt32(lblID.Text);                
                        Alterar(codigo);
                        btnIncluir.Enabled = true;
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnOK.Visible = false;
                        txtFeedback.Enabled = false;
                        txtMelhoria.Enabled = false;
                    }              

                }
                else
                {
                    if (txtMelhoria.Text == "")
                    {
                        MessageBox.Show("Escolha na planilha a linha correspondente à informação que deve ser excluída com DUPLO CLICK.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        codigo = Convert.ToInt32(lblID.Text);
                        Excluir(codigo);
                        txtFeedback.Enabled = false;
                        txtMelhoria.Enabled = false;
                        btnIncluir.Enabled = true;
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnOK.Visible = false;
                    }
                }
            }           
            
        }

        private void Excluir(int codigo)
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
                cmd.CommandText = "Delete from sugestao where idsugestao = " + codigo;
                //mConn.Open();
                int resultado = cmd.ExecuteNonQuery();
                if (resultado != 1)
                {
                    throw new Exception("Não foi possível excluir dado." + codigo);
                }
                MessageBox.Show("Excluído o dado nº. " + codigo + " com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na conexão com Banco de Dados. Erro: Delete from sugestao where idsugestao = " + codigo + ex.Message, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            finally
            {
                mConn.Close();
                //mostrarResultados();
            }

            dataGridView1.Enabled = false;
            LimparCampos();
            mostrarResultados();
        }

        private void Alterar(int codigo)
        {
            if (txtMelhoria.Text == "")
            {
                MessageBox.Show("Escolha algum dado que deva ser alterado \n'clicando'na planilha na linha correspondente.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
              
                if (rbAnalise.Checked == true)
                {
                    valor = "analise";
                }
                else
                {
                    if (rbResolvido.Checked == true)
                    {
                        valor = "resolvido";
                    }
                    else
                    {
                        if (rbSemPossibilidade.Checked == true)
                        {

                            valor = "não possível";
                        }
                        else
                        {
                            valor = "registrado";
                        }
                    }
                }                 
               

                //conexao
                mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                mConn.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = mConn;
                                   
                    cmd.CommandText = "UPDATE sugestao SET melhoria ='" + txtMelhoria.Text + "', feedback=" + "'" + txtFeedback.Text + "', data='"+
                        dateTimePicker1.Text + "',situacao=" + "'" + valor + "',cadastrante=" + "'" + Sistema_prorim.Global.Logon.nome_usuario 
                        + "' WHERE idsugestao=" + codigo;

                    MessageBox.Show("Registro  [ " + "'" + codigo + "'" + " ] Alterado com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    int resultado = cmd.ExecuteNonQuery();
                    if (resultado != 1)
                    {
                        throw new Exception("Não foi possível alterar os dados.' " + codigo);
                    }

                }
                 catch (Exception ex)
                {
                    MessageBox.Show("Falha na conexão com Banco de Dados."+ex.Message, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("UPDATE sugestao SET melhoria ='" + txtMelhoria.Text + "', feedback=" + "'" + txtFeedback.Text + "', data='"+
                        dateTimePicker1.Text + "',situacao=" + "'" + valor + "',cadastrante=" + "'" + Sistema_prorim.Global.Logon.nome_usuario 
                        + "' WHERE idsugestao=" + codigo,"ERRO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                mConn.Close();
                LimparCampos();
                mostrarResultados();
                dataGridView1.Enabled = false;
            }
        }

        private void Gravar()
        {
            // Início da Conexão com indicação de qual o servidor, nome de base de dados a utilizar

            /* É aconselhável criar um utilizador com password. Para acrescentar a password é somente
               necessário acrescentar o seguinte código a seguir ao uid=root;password=xxxxx
             */

            //capturaValorChk();
            
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Sistema_prorim.Global.Logon.ipservidor + ";database=prorim;uid=root;password=");

            // Abre a conexão
            mConn.Open();
            
            try
            {
                if (rbAnalise.Checked == true)
                {
                    valor = "analise";
                }
                else
                {
                    if (rbResolvido.Checked == true)
                    {
                        valor = "resolvido";
                    }
                    else
                    {
                        if (rbSemPossibilidade.Checked == true)
                        {

                            valor = "não possível";
                        }
                        else
                        {
                            valor = "registrado";
                        }
                    }
                }

                //Query SQL
                MySqlCommand command = new MySqlCommand("INSERT INTO sugestao (melhoria,feedback, data, situacao,cadastrante)" +
                "VALUES('" + txtMelhoria.Text + "','" + txtFeedback.Text + "','" + dateTimePicker1.Text + "','" + valor + "','" + Sistema_prorim.Global.Logon.nome_usuario+"')", mConn);
                // Esta representando a sequencia "...VALUES(txtSetor,txtEndereço,...)"

                //Executa a Query SQL
                command.ExecuteNonQuery();

                //Fecha a conexão
                //mConn.Close();

                //Mensagem de Sucesso
                MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMelhoria.Enabled = false;
                txtFeedback.Enabled= false;

                // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"
            }
            catch 
            {
                MessageBox.Show("Erro de gravação!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            
            mConn.Close();
            LimparCampos();
            mostrarResultados();
            dataGridView1.Enabled = false;           
        }

        private void LimparCampos()
        {
            txtMelhoria.Text = "";
            txtFeedback.Text = "";
            lblID.Text = "";
        }

        

        private void txtMelhoria_Enter(object sender, EventArgs e)
        {
            txtMelhoria.BackColor = Color.Yellow;

        }
        private void txtMelhoria_Leave(object sender, EventArgs e)
        {
            txtMelhoria.BackColor = Color.LightGray;
        }

        private void txtFeedback_Enter(object sender, EventArgs e)
        {
            txtFeedback.BackColor = Color.Yellow;
        }

        private void txtFeedback_Leave(object sender, EventArgs e)
        {
            txtFeedback.BackColor = Color.LightGray;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (flagInclusao == 2)
            {
                txtMelhoria.Enabled = false;
                txtFeedback.Enabled = false;
            }
            else
            {
                txtMelhoria.Enabled = true;
                txtFeedback.Enabled = true;
            }

            lblID.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtMelhoria.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtFeedback.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            dateTimePicker1.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            
            dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                       
            if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString()== "registrado")
            {
                rbRegistrado.Checked = true;
            }
            else
            {
                if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "não possível")
                {
                    rbSemPossibilidade.Checked = true;
                }
                else {
                    if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "resolvido")
                    {
                        rbResolvido.Checked = true;
                    }
                    else {
                        rbAnalise.Checked = true;
                    }
                }                
            }

            btnOK.Visible = true;
            //HabilitaTextBox();
        }
    }            
 }
    

