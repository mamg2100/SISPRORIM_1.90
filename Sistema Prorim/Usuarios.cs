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
    public partial class Usuarios : Form
    {
        private MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        String temp;
        int codigo=0;
        string tipousuario;
        int flagInclusao = 1;
        int tentativa = 1;

        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            analisarTipoUsuario();

            mostrarResultados(); 
        }

        private void analisarTipoUsuario()
        {
            if (Sistema_prorim.Global.Logon.tipousuario.Trim() == "admin")
            {
                btnExcluir.Visible = true;
                btnExcluir.Enabled = true;
                btnIncluir.Visible = true;
                btnIncluir.Enabled = true;
            }
            else
            {
                btnExcluir.Visible = false;
                btnIncluir.Visible = false;
            }
        }

        private void mostrarResultados()
        {
            // Se for usuário administrador deverá ter pemissões para incluir, excluir e alterar dados do usuário se não
            // terá autorização apenas para ateração de sua própria credencial, não de outros.

            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            if (Sistema_prorim.Global.Logon.tipousuario.Trim()== "admin")
            {
                //cria um adapter utilizando a instrução SQL para acessar a tabela 
                //if (rbPorCodigo.Checked == true)
                // ordena a tabela de acordo com o critério estabelecido
                mAdapter = new MySqlDataAdapter("SELECT * FROM usuario ORDER BY Cod_usuario DESC", mConn);

                //else
                //if (rbPorNome.Checked == true)
                // mAdapter = new MySqlDataAdapter("SELECT * FROM usuario ORDER BY Nome_Usuario", mConn);
                //else
                //  mAdapter = new MySqlDataAdapter("SELECT * FROM usuario ORDER by Setor_Usuario", mConn);

                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet, "usuario");

                //atribui o resultado à propriedade DataSource da dataGridView
                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "usuario";

                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Nome";
                dataGridView1.Columns[2].HeaderText = "Login";
                //omitindo a exibição da coluna de senha do usuário
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[3].HeaderText = "Senha";
                dataGridView1.Columns[4].HeaderText = "Setor";
                dataGridView1.Columns[5].HeaderText = "e-mail";
                dataGridView1.Columns[6].HeaderText = "Tipo";
                //dataGridView1.Columns[6].Visible = false;
            }
            else 
            {
                mAdapter = new MySqlDataAdapter("SELECT * FROM usuario Where Cod_usuario=" + Global.Logon.codigo_usuario, mConn);
                mAdapter.Fill(mDataSet, "usuario");

                dataGridView1.DataSource = mDataSet;
                dataGridView1.DataMember = "usuario";
                
                //Renomeia as colunas
                dataGridView1.Columns[0].HeaderText = "Código";
                dataGridView1.Columns[1].HeaderText = "Nome";
                dataGridView1.Columns[2].HeaderText = "Login";
                dataGridView1.Columns[3].HeaderText = "Senha";
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].HeaderText = "Setor";
                dataGridView1.Columns[5].HeaderText = "e-mail";
                dataGridView1.Columns[6].HeaderText = "Tipo";
                //dataGridView1.Columns[6].Visible = false;
                //MySqlCommand cmd = new MySqlCommand();
                //cmd.Connection = mConn;
                //cmd.CommandText = "SELECT Nome_fornecedor from fornecedor where Cod_fornecedor = " + 1;
           }

            int registro;
            registro = dataGridView1.RowCount - 1;
            if (dataGridView1.RowCount < 2)
            {
                label9.Text = registro + " registro";
            }
            else {
                label9.Text = registro + " registros";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void HabilitaTextBox()
        {
            txtUsuario.Enabled = true;
            txtLogin.Enabled = true;
            txtSenha.Enabled = true;
            txtSetorUsuario.Enabled = true;
            txtEmail.Enabled = true;                          
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
                else
                {
                    txtCheckCodigo.Focus();
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
            mAdapter = new MySqlDataAdapter("SELECT * FROM usuario Where Cod_usuario=" + codigo, mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "usuario");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "usuario";

        }

        private void txtCheckIdentificação_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                temp = txtCheckIdentificação.Text;
                PesquisaPorSetor(temp);
                PesquisaPorUsuario(temp);
            }           
        }

        private void PesquisaPorUsuario(string temp)
        {
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
            mConn.Open();

            //cria um adapter utilizando a instrução SQL para acessar a tabela 
            // ordena a tabela de acordo com o critério estabelecido
            mAdapter = new MySqlDataAdapter("SELECT * FROM usuario WHERE Nome_usuario " + "LIKE " + "'%" + temp + "%'", mConn);

            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "usuario");

            //atribui o resultado à propriedade DataSource da dataGridView
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = "usuario";
        
        }

        private void PesquisaPorSetor(string temp)
        {
            
        }

        private void bt_visualizar_Click(object sender, EventArgs e)
        {
            mostrarResultados();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtLogin.Focus();

            }
            else
            {
                txtUsuario.Focus();

            }
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtSenha.Focus();

            }
            else
            {
                txtLogin.Focus();

            }
        }


        private void txtConfirmacaoSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                if (txtConfirmacaoSenha.Text == txtSenha.Text)
                {
                    txtSetorUsuario.Focus();
                }
                else
                {
                    //txtConfirmacaoSenha.Text = "";
                    //txtConfirmacaoSenha.Visible = false;
                    //lblConfirmacaoSenha.Visible = false;
                    MessageBox.Show("Senha não confere.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtConfirmacaoSenha.Focus();
                }
                
            }
             */

            if (e.KeyChar == 13) //Se for Enter executa a validação 
            {  
                if (tentativa < 4)
                {
                    if (txtConfirmacaoSenha.Text == txtSenha.Text)
                    {
                        txtSetorUsuario.Focus();

                    }
                    else
                    {
                        //txtConfirmacaoSenha.Text = "";
                        //txtConfirmacaoSenha.Visible = false;
                        //lblConfirmacaoSenha.Visible = false;
                        MessageBox.Show("Senha não confere.Tentativa: " + tentativa, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtConfirmacaoSenha.Focus();
                        tentativa = tentativa + 1;
                    }
                }
            }

            if (tentativa == 4)
            {
                lblConfirmacaoSenha.Visible = false;
                txtConfirmacaoSenha.Visible = false;
                txtSenha.Focus();
                tentativa = 1;
            }
                        
        }

        private void txtSetorUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                txtEmail.Focus();
            }
            else
            {
                txtSetorUsuario.Focus();

            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                btnOK.Focus();

            }
            else
            {
                txtEmail.Focus();

            }
        }

        
        private void Excluir(int codigo)
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
                        cmd.CommandText = "delete from usuario where Cod_usuario = " + codigo;
                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível excluir o usuário " + codigo);
                        }
                        MessageBox.Show("Excluído o usuário nº. " + codigo + " com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    }
                    catch 
                    {
                        MessageBox.Show("Falha na conexão com Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                    }
                    finally
                    {
                        mConn.Close();
                        mostrarResultados();
                    }

                    //UncheckedRadioButtons();
                    //HabilitaRadionButtons();
                    LimpaCampos();
                    DesabilitaTextBox();
                    mostrarResultados();
        }

       

        private void Alterar(int codigo)
        {

            if (txtUsuario.Text == "") {
                MessageBox.Show("Escolha algum usuário cujos dados devam ser alterados \n'clicando'na planilha na linha correspondente.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else{
                  //conexao
                    mConn = new MySqlConnection("Persist Security Info=False;server=" + Global.Logon.ipservidor + ";database=prorim;uid=root;password=");
                    mConn.Open();
                    try
                    {
                        //mConn.ConnectionString = Dados.StringDeConexao;

                        //command
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = mConn;

                        if (chkComum.Checked == true)
                            tipousuario = "comum";
                        else
                            tipousuario = "admin";

                        // Vamos deixar essa codificação abaixo - que não funcionou - para comparação com a que funciona logo abaixo.
                        //cmd.CommandText = "UPDATE fornecedor SET Nome_fornecedor=" + "'" + txtFornecedor.Text + "'," + " End_fornecedor =" + "'" + txtEndereço + "'," +
                        //" Fone1_fornecedor =" + "'" + txtFone1 + "'," + " Fone2_fornecedor =" + "'" + txtFone2 + "'," + " Email_fornecedor =" + "'" + txtEmail + "'" + "Where Cod_fornecedor = " + codigo;

                        cmd.CommandText = "UPDATE usuario SET Nome_usuario =" + "'" + txtUsuario.Text + "'," + "Login_usuario=" + "'" + txtLogin.Text + "'," + "Senha_usuario=" + "'" +
                            txtSenha.Text + "'," + "Setor_usuario=" + "'" + txtSetorUsuario.Text + "'," + "email_usuario=" + "'" + txtEmail.Text + "'," + "Tipo_usuario=" + "'" + tipousuario + "'" + "WHERE Cod_usuario=" + codigo;

                        MessageBox.Show("Registro  [ " + "'" + codigo + "'" + " ] Alterado com sucesso.","Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //mConn.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado != 1)
                        {
                            throw new Exception("Não foi possível alterar os dados do 'USUARIO' " + codigo);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Falha na conexão com Banco de Dados.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show("Erro: "+ex.Message);
                    }
                        mConn.Close();                        
                        LimpaCampos();
                        DesabilitaTextBox();
                        //HabilitaRadionButtons();
                        mostrarResultados();
                        //UncheckedRadioButtons();
            }
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
            
            if (chkComum.Checked == true)
                tipousuario = "comun";
            else
                tipousuario = "admin";

            try
            {
                //Query SQL
                MySqlCommand command = new MySqlCommand("INSERT INTO usuario (Nome_usuario,Login_usuario,Senha_usuario,Setor_usuario,Email_usuario,Tipo_usuario)" +
                "VALUES('" + txtUsuario.Text + "','" + txtLogin.Text + "','" + txtSenha.Text + "','" + txtSetorUsuario.Text + "','" + txtEmail.Text + "','" + tipousuario + "')", mConn);
                // Esta representando a sequencia "...VALUES(txtSetor,txtEndereço,...)"

                //Executa a Query SQL
                command.ExecuteNonQuery();

                // Fecha a conexão
                //mConn.Close();

                //Mensagem de Sucesso
                MessageBox.Show("Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Método criado para que quando o registo é gravado, automaticamente a dataGridView efetue um "Refresh"
            }
            catch 
            {
                MessageBox.Show("Erro de gravação!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            
            mConn.Close();
            LimpaCampos();

           
            //HabilitaRadionButtons();
            mostrarResultados();

            //UncheckedRadioButtons();
            DesabilitaTextBox();
            analisarTipoUsuario();

        }
               
       

        private void DesabilitaTextBox()
        {
            txtUsuario.Enabled=false;
            txtLogin.Enabled = false;
            txtSenha.Enabled = false;
            txtConfirmacaoSenha.Enabled = false;
            txtSetorUsuario.Enabled = false; ;
            txtEmail.Enabled = false;
        }

        private void LimpaCampos()
        {
            textBox3.Text = "";
            txtUsuario.Text = "";
            txtLogin.Text = "";
            txtSenha.Text = "";
            txtSetorUsuario.Text = "";
            txtConfirmacaoSenha.Text = "";
            txtConfirmacaoSenha.Visible = false;
            txtEmail.Text = "";
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Se for Enter executa a validação
            {
                lblConfirmacaoSenha.Visible = true;
                txtConfirmacaoSenha.Visible = true;
                txtConfirmacaoSenha.Enabled = true;
                txtConfirmacaoSenha.Focus();
                
            }
            else
            {
                txtSenha.Focus();

            }
        }

        
        
        private void txtConfirmacaoSenha_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void groupBox2_Enter(object sender, EventArgs e)
        {
             
        }

        private void rbPorCodigo_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "ordenando por código...";
            mostrarResultados();
        }

        private void txtCheckIdentificação_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbPorNome_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "ordenando por nome...";
            mostrarResultados();
        }

        private void chkMaster_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMaster.Checked == true)
            {
                chkComum.Checked = false;
            }else
            {
            }                
        }

        private void chkComum_CheckedChanged(object sender, EventArgs e)
        {
            if (chkComum.Checked == true)
            {
                chkMaster.Checked = false;
            }
            else
            {
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rbPorContato_CheckedChanged(object sender, EventArgs e)
        {
            tssMensagem.Text = "ordenando por unidade...";
            mostrarResultados();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

        }

        private void txtCheckCodigo_Enter(object sender, EventArgs e)
        {
            txtCheckCodigo.BackColor = Color.Yellow;
        }

        private void txtCheckIdentificação_Enter(object sender, EventArgs e)
        {
            txtCheckIdentificação.BackColor = Color.Yellow;
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            txtUsuario.BackColor = Color.Yellow;
        }

        private void txtLogin_Enter(object sender, EventArgs e)
        {
            txtLogin.Text = txtLogin.Text.ToLower();
            txtLogin.BackColor = Color.Yellow;
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            txtSenha.BackColor = Color.Yellow;
        }

        private void txtSetorUsuario_Enter(object sender, EventArgs e)
        {
            txtSetorUsuario.BackColor = Color.Yellow;
        }

        private void txtCheckCodigo_Leave(object sender, EventArgs e)
        {
            txtCheckCodigo.BackColor = Color.White;
        }

        private void txtConfirmacaoSenha_Enter(object sender, EventArgs e)
        {

        }

        private void txtSetorUsuario_Leave(object sender, EventArgs e)
        {
            txtSetorUsuario.Text = txtSetorUsuario.Text.ToUpper();
            txtSetorUsuario.BackColor = Color.White;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            txtEmail.Text = txtEmail.Text.ToLower();
            txtEmail.BackColor = Color.White;
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            txtUsuario.Text = txtUsuario.Text.ToUpper();
            txtUsuario.BackColor = Color.White;
        }

        private void txtLogin_Leave(object sender, EventArgs e)
        {
            txtLogin.Text = txtLogin.Text.ToLower();
            txtLogin.BackColor = Color.White;
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            txtSenha.BackColor = Color.White;
        }

        private void txtConfirmacaoSenha_Leave(object sender, EventArgs e)
        {
          if (tentativa < 4)
            {
                if (txtConfirmacaoSenha.Text == txtSenha.Text)
                {
                    txtSetorUsuario.Focus();
                    
                }
                else
                {
                    //txtConfirmacaoSenha.Text = "";
                    //txtConfirmacaoSenha.Visible = false;
                    //lblConfirmacaoSenha.Visible = false;
                    MessageBox.Show("Senha não confere.Tentativa: "+tentativa, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtConfirmacaoSenha.Focus();
                    tentativa = tentativa + 1;
                }               
            }

          if (tentativa == 4) {

              txtSenha.Focus();
          }
               
        }

        private void txtCheckIdentificação_Leave(object sender, EventArgs e)
        {
            txtCheckIdentificação.BackColor = Color.White;
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            txtEmail.Text = txtEmail.Text.ToLower();
            txtEmail.BackColor = Color.Yellow;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox3.Text = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtUsuario.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtLogin.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtSenha.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtSetorUsuario.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            txtEmail.Text = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            if (dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "comum")
            {
                toolStripStatusLabel1.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                chkComum.Checked = true;
                chkMaster.Checked = false;
            }
            else
            {
                toolStripStatusLabel1.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                chkComum.Checked = false;
                chkMaster.Checked = true;
            }

            btnOK.Visible = true;
            HabilitaTextBox();
        }

        
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void lineShape2_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            flagInclusao = 2;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            flagInclusao = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Campos obrigatórios na inclusão.
            // Nome, Login, Senha e Tipo de Usuário. Esse último se - não marcado - ficará como usuário tipo comum.
            // Portanto a verificação será dos campos três iniciais

            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Campo não pode ficar vazio.","Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtUsuario.Focus();
            }
            else {
                if (txtLogin.Text == "") {
                    MessageBox.Show("Campo não pode ficar vazio.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtLogin.Focus();
                }else{
                     if(txtSenha.Text==""){
                         MessageBox.Show("Campo não pode ficar vazio.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                         txtSenha.Focus();
                     }else{
                         
                         // O botão OK serve tanto para inclusão quanto para alteração de dados dos usuários
                         // Foi criada um variável flag para informar o que estamos fazendo. Inclusão ou alteração.
                         if (flagInclusao == 1)
                         {
                             Gravar(); 
                         }
                         else
                         {  // essa linha só serve para casos de alteração de dados e exclusão
                             codigo = Convert.ToInt32(textBox3.Text);
                             if (flagInclusao == 0)
                             {
                                 Alterar(codigo);
                             }
                             else
                             {
                                 Excluir(codigo);
                             }
                         }                    
                     
                     }    
                }
                btnIncluir.Enabled = true;
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;
                btnOK.Visible = false;
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
            txtUsuario.Focus();
        }        
    }
}
