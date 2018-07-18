namespace Sistema_prorim
{
    partial class Fornecedor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fornecedor));
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_visualizar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.rbPorCodigo = new System.Windows.Forms.RadioButton();
            this.rbPorNome = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtIdentificação = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTipoNumero = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSite = new System.Windows.Forms.TextBox();
            this.txtTipoNumero = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbUF = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbTipoFornecedor = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtEmailContato = new System.Windows.Forms.TextBox();
            this.txtEndereço = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtFone2 = new System.Windows.Forms.TextBox();
            this.txtFone1 = new System.Windows.Forms.TextBox();
            this.txtResp = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkDecrescente = new System.Windows.Forms.CheckBox();
            this.chkCrescente = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.rbCPF = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssMensagem = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape6 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape5 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnIncluir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFornecedor
            // 
            this.txtFornecedor.Enabled = false;
            this.txtFornecedor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFornecedor.Location = new System.Drawing.Point(38, 68);
            this.txtFornecedor.MaxLength = 100;
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.Size = new System.Drawing.Size(354, 21);
            this.txtFornecedor.TabIndex = 9;
            this.txtFornecedor.TextChanged += new System.EventHandler(this.txtFornecedor_TextChanged);
            this.txtFornecedor.Enter += new System.EventHandler(this.txtFornecedor_Enter);
            this.txtFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFornecedor_KeyPress);
            this.txtFornecedor.Leave += new System.EventHandler(this.txtFornecedor_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(696, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 14);
            this.label1.TabIndex = 21;
            // 
            // bt_visualizar
            // 
            this.bt_visualizar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_visualizar.Location = new System.Drawing.Point(1026, 392);
            this.bt_visualizar.Name = "bt_visualizar";
            this.bt_visualizar.Size = new System.Drawing.Size(75, 23);
            this.bt_visualizar.TabIndex = 18;
            this.bt_visualizar.Text = "&Visualizar";
            this.bt_visualizar.UseVisualStyleBackColor = true;
            this.bt_visualizar.Visible = false;
            this.bt_visualizar.Click += new System.EventHandler(this.bt_visualizar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightGray;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(61, 145);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(1040, 214);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(41, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "email fornecedor";
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.Blue;
            this.txtEmail.Location = new System.Drawing.Point(38, 211);
            this.txtEmail.MaxLength = 40;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(295, 21);
            this.txtEmail.TabIndex = 22;
            this.txtEmail.Enter += new System.EventHandler(this.txtEmail_Enter);
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress_1);
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            // 
            // rbPorCodigo
            // 
            this.rbPorCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbPorCodigo.AutoSize = true;
            this.rbPorCodigo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPorCodigo.Location = new System.Drawing.Point(753, 56);
            this.rbPorCodigo.Name = "rbPorCodigo";
            this.rbPorCodigo.Size = new System.Drawing.Size(65, 17);
            this.rbPorCodigo.TabIndex = 6;
            this.rbPorCodigo.Text = "Codi&go";
            this.rbPorCodigo.UseVisualStyleBackColor = true;
            this.rbPorCodigo.CheckedChanged += new System.EventHandler(this.rbPorCodigo_CheckedChanged);
            this.rbPorCodigo.Click += new System.EventHandler(this.rbPorCodigo_Click);
            // 
            // rbPorNome
            // 
            this.rbPorNome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbPorNome.AutoSize = true;
            this.rbPorNome.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPorNome.Location = new System.Drawing.Point(831, 56);
            this.rbPorNome.Name = "rbPorNome";
            this.rbPorNome.Size = new System.Drawing.Size(58, 17);
            this.rbPorNome.TabIndex = 7;
            this.rbPorNome.TabStop = true;
            this.rbPorNome.Text = "&Nome";
            this.rbPorNome.UseVisualStyleBackColor = true;
            this.rbPorNome.CheckedChanged += new System.EventHandler(this.rbPorNome_CheckedChanged);
            this.rbPorNome.Click += new System.EventHandler(this.rbPorNome_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(41, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 14);
            this.label8.TabIndex = 47;
            this.label8.Text = "Código";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Enabled = false;
            this.textBox3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(97, 22);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(64, 15);
            this.textBox3.TabIndex = 1;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(24, 56);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(64, 21);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            this.txtCodigo.Enter += new System.EventHandler(this.txtCodigo_Enter);
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            this.txtCodigo.Leave += new System.EventHandler(this.txtCodigo_Leave);
            // 
            // txtIdentificação
            // 
            this.txtIdentificação.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdentificação.Location = new System.Drawing.Point(254, 56);
            this.txtIdentificação.Name = "txtIdentificação";
            this.txtIdentificação.Size = new System.Drawing.Size(224, 21);
            this.txtIdentificação.TabIndex = 2;
            this.txtIdentificação.TextChanged += new System.EventHandler(this.txtCheckIdentificação_TextChanged);
            this.txtIdentificação.Enter += new System.EventHandler(this.txtIdentificação_Enter);
            this.txtIdentificação.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheckIdentificação_KeyPress);
            this.txtIdentificação.Leave += new System.EventHandler(this.txtIdentificação_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.linkLabel2);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.cmbTipo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblTipoNumero);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSite);
            this.groupBox1.Controls.Add(this.txtTipoNumero);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.cmbUF);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtCidade);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtBairro);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtNumero);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cmbTipoFornecedor);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtEmailContato);
            this.groupBox1.Controls.Add(this.txtEndereço);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.txtFone2);
            this.groupBox1.Controls.Add(this.txtFone1);
            this.groupBox1.Controls.Add(this.txtResp);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtFornecedor);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Location = new System.Drawing.Point(61, 394);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(773, 293);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(655, 109);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(112, 13);
            this.linkLabel2.TabIndex = 135;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "RECEITA FEDERAL";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Image = ((System.Drawing.Image)(resources.GetObject("linkLabel1.Image")));
            this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.Location = new System.Drawing.Point(655, 70);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(98, 13);
            this.linkLabel1.TabIndex = 134;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "       CORREIOS";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // cmbTipo
            // 
            this.cmbTipo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cmbTipo.Enabled = false;
            this.cmbTipo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "Alameda",
            "Área",
            "Avenida",
            "Campo",
            "Chácara",
            "Estrada",
            "Ladeira",
            "Loteamento",
            "Morro",
            "Parque",
            "Praça",
            "Quadra",
            "Rodovia",
            "Rua",
            "Travessa",
            "Via",
            "Outros"});
            this.cmbTipo.Location = new System.Drawing.Point(39, 118);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(64, 21);
            this.cmbTipo.TabIndex = 14;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            this.cmbTipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTipo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 133;
            this.label2.Text = "Tipo";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblTipoNumero
            // 
            this.lblTipoNumero.AutoSize = true;
            this.lblTipoNumero.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoNumero.Location = new System.Drawing.Point(463, 53);
            this.lblTipoNumero.Name = "lblTipoNumero";
            this.lblTipoNumero.Size = new System.Drawing.Size(0, 13);
            this.lblTipoNumero.TabIndex = 132;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 131;
            this.label3.Text = "Web Site fornecedor";
            // 
            // txtSite
            // 
            this.txtSite.Enabled = false;
            this.txtSite.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSite.Location = new System.Drawing.Point(38, 250);
            this.txtSite.MaxLength = 40;
            this.txtSite.Name = "txtSite";
            this.txtSite.Size = new System.Drawing.Size(295, 21);
            this.txtSite.TabIndex = 23;
            this.txtSite.Enter += new System.EventHandler(this.txtSite_Enter);
            this.txtSite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSite_KeyPress);
            this.txtSite.Leave += new System.EventHandler(this.txtSite_Leave);
            // 
            // txtTipoNumero
            // 
            this.txtTipoNumero.Enabled = false;
            this.txtTipoNumero.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoNumero.Location = new System.Drawing.Point(462, 68);
            this.txtTipoNumero.MaxLength = 40;
            this.txtTipoNumero.Name = "txtTipoNumero";
            this.txtTipoNumero.Size = new System.Drawing.Size(173, 21);
            this.txtTipoNumero.TabIndex = 11;
            this.txtTipoNumero.TextChanged += new System.EventHandler(this.txtTipoNumero_TextChanged);
            this.txtTipoNumero.Enter += new System.EventHandler(this.txtTipoNumero_Enter);
            this.txtTipoNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTipoNumero_KeyPress);
            this.txtTipoNumero.Leave += new System.EventHandler(this.txtTipoNumero_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(230, 152);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(21, 13);
            this.label18.TabIndex = 127;
            this.label18.Text = "UF";
            // 
            // cmbUF
            // 
            this.cmbUF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cmbUF.Enabled = false;
            this.cmbUF.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUF.FormattingEnabled = true;
            this.cmbUF.Items.AddRange(new object[] {
            "AC",
            "AL",
            "AM",
            "AP",
            "BA",
            "CE",
            "DF",
            "ES",
            "GO",
            "MA",
            "MG",
            "MS",
            "MT",
            "PA",
            "PB",
            "PE",
            "PI",
            "PR",
            "RJ",
            "RN",
            "RO",
            "RR",
            "RS",
            "SC",
            "SE",
            "SP",
            "TO"});
            this.cmbUF.Location = new System.Drawing.Point(228, 167);
            this.cmbUF.Name = "cmbUF";
            this.cmbUF.Size = new System.Drawing.Size(45, 21);
            this.cmbUF.TabIndex = 19;
            this.cmbUF.SelectedIndexChanged += new System.EventHandler(this.cmbUF_SelectedIndexChanged);
            this.cmbUF.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUF_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(41, 152);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 13);
            this.label17.TabIndex = 125;
            this.label17.Text = "Cidade";
            // 
            // txtCidade
            // 
            this.txtCidade.Enabled = false;
            this.txtCidade.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCidade.Location = new System.Drawing.Point(40, 167);
            this.txtCidade.MaxLength = 30;
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(187, 21);
            this.txtCidade.TabIndex = 18;
            this.txtCidade.Enter += new System.EventHandler(this.txtCidade_Enter);
            this.txtCidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCidade_KeyPress);
            this.txtCidade.Leave += new System.EventHandler(this.txtCidade_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(461, 103);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 123;
            this.label15.Text = "Bairro";
            // 
            // txtBairro
            // 
            this.txtBairro.Enabled = false;
            this.txtBairro.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBairro.Location = new System.Drawing.Point(462, 118);
            this.txtBairro.MaxLength = 40;
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(173, 21);
            this.txtBairro.TabIndex = 17;
            this.txtBairro.TextChanged += new System.EventHandler(this.txtBairro_TextChanged);
            this.txtBairro.Enter += new System.EventHandler(this.txtBairro_Enter);
            this.txtBairro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBairro_KeyPress);
            this.txtBairro.Leave += new System.EventHandler(this.txtBairro_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(397, 104);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(21, 13);
            this.label14.TabIndex = 121;
            this.label14.Text = "Nº";
            // 
            // txtNumero
            // 
            this.txtNumero.Enabled = false;
            this.txtNumero.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumero.Location = new System.Drawing.Point(395, 118);
            this.txtNumero.MaxLength = 9;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(64, 21);
            this.txtNumero.TabIndex = 16;
            this.txtNumero.Enter += new System.EventHandler(this.txtNumero_Enter);
            this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumero_KeyPress);
            this.txtNumero.Leave += new System.EventHandler(this.txtNumero_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(107, 103);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 13);
            this.label16.TabIndex = 119;
            this.label16.Text = "Logradouro";
            // 
            // cmbTipoFornecedor
            // 
            this.cmbTipoFornecedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cmbTipoFornecedor.Enabled = false;
            this.cmbTipoFornecedor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoFornecedor.FormattingEnabled = true;
            this.cmbTipoFornecedor.Items.AddRange(new object[] {
            "PF",
            "PJ"});
            this.cmbTipoFornecedor.Location = new System.Drawing.Point(395, 68);
            this.cmbTipoFornecedor.Name = "cmbTipoFornecedor";
            this.cmbTipoFornecedor.Size = new System.Drawing.Size(64, 21);
            this.cmbTipoFornecedor.TabIndex = 10;
            this.cmbTipoFornecedor.SelectedIndexChanged += new System.EventHandler(this.cmbTipoFornecedor_SelectedIndexChanged);
            this.cmbTipoFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTipoFornecedor_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(343, 236);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 13);
            this.label19.TabIndex = 117;
            this.label19.Text = "email contato";
            // 
            // txtEmailContato
            // 
            this.txtEmailContato.Enabled = false;
            this.txtEmailContato.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailContato.ForeColor = System.Drawing.Color.Blue;
            this.txtEmailContato.Location = new System.Drawing.Point(340, 250);
            this.txtEmailContato.MaxLength = 40;
            this.txtEmailContato.Name = "txtEmailContato";
            this.txtEmailContato.Size = new System.Drawing.Size(295, 21);
            this.txtEmailContato.TabIndex = 25;
            this.txtEmailContato.Enter += new System.EventHandler(this.txtEmailContato_Enter);
            this.txtEmailContato.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmailContato_KeyPress);
            this.txtEmailContato.Leave += new System.EventHandler(this.txtEmailContato_Leave);
            // 
            // txtEndereço
            // 
            this.txtEndereço.Enabled = false;
            this.txtEndereço.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndereço.Location = new System.Drawing.Point(105, 118);
            this.txtEndereço.MaxLength = 100;
            this.txtEndereço.Name = "txtEndereço";
            this.txtEndereço.Size = new System.Drawing.Size(287, 21);
            this.txtEndereço.TabIndex = 15;
            this.txtEndereço.Enter += new System.EventHandler(this.txtEndereço_Enter);
            this.txtEndereço.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEndereço_KeyPress_1);
            this.txtEndereço.Leave += new System.EventHandler(this.txtEndereço_Leave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(397, 52);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(31, 13);
            this.label20.TabIndex = 112;
            this.label20.Text = "Tipo";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(433, 153);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(66, 13);
            this.label21.TabIndex = 113;
            this.label21.Text = "Telefone 1";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(536, 153);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(66, 13);
            this.label22.TabIndex = 114;
            this.label22.Text = "Telefone 2";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(343, 196);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(129, 13);
            this.label23.TabIndex = 115;
            this.label23.Text = "Responsável/Contato";
            // 
            // txtFone2
            // 
            this.txtFone2.Enabled = false;
            this.txtFone2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFone2.Location = new System.Drawing.Point(535, 167);
            this.txtFone2.MaxLength = 12;
            this.txtFone2.Name = "txtFone2";
            this.txtFone2.Size = new System.Drawing.Size(100, 21);
            this.txtFone2.TabIndex = 21;
            this.txtFone2.TextChanged += new System.EventHandler(this.txtFone2_TextChanged);
            this.txtFone2.Enter += new System.EventHandler(this.txtFone2_Enter);
            this.txtFone2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFone2_KeyPress_1);
            this.txtFone2.Leave += new System.EventHandler(this.txtFone2_Leave);
            // 
            // txtFone1
            // 
            this.txtFone1.Enabled = false;
            this.txtFone1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFone1.Location = new System.Drawing.Point(430, 167);
            this.txtFone1.MaxLength = 12;
            this.txtFone1.Name = "txtFone1";
            this.txtFone1.Size = new System.Drawing.Size(100, 21);
            this.txtFone1.TabIndex = 20;
            this.txtFone1.Enter += new System.EventHandler(this.txtFone1_Enter);
            this.txtFone1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFone1_KeyPress_1);
            this.txtFone1.Leave += new System.EventHandler(this.txtFone1_Leave);
            // 
            // txtResp
            // 
            this.txtResp.Enabled = false;
            this.txtResp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResp.Location = new System.Drawing.Point(340, 211);
            this.txtResp.MaxLength = 40;
            this.txtResp.Name = "txtResp";
            this.txtResp.Size = new System.Drawing.Size(295, 21);
            this.txtResp.TabIndex = 24;
            this.txtResp.TextChanged += new System.EventHandler(this.txtResp_TextChanged);
            this.txtResp.Enter += new System.EventHandler(this.txtResp_Enter);
            this.txtResp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResp_KeyPress);
            this.txtResp.Leave += new System.EventHandler(this.txtResp_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(41, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 50;
            this.label10.Text = "Fornecedor";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(68, 364);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 59;
            this.label9.Text = "label9";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(27, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 62;
            this.label12.Text = "Código";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(257, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 63;
            this.label13.Text = "Fornecedor";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.chkDecrescente);
            this.groupBox2.Controls.Add(this.chkCrescente);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.rbCPF);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.rbPorCodigo);
            this.groupBox2.Controls.Add(this.rbPorNome);
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.txtIdentificação);
            this.groupBox2.Controls.Add(this.shapeContainer2);
            this.groupBox2.Location = new System.Drawing.Point(42, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1118, 96);
            this.groupBox2.TabIndex = 65;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // chkDecrescente
            // 
            this.chkDecrescente.AutoSize = true;
            this.chkDecrescente.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDecrescente.Location = new System.Drawing.Point(592, 56);
            this.chkDecrescente.Name = "chkDecrescente";
            this.chkDecrescente.Size = new System.Drawing.Size(97, 17);
            this.chkDecrescente.TabIndex = 5;
            this.chkDecrescente.Text = "Decrescente";
            this.chkDecrescente.UseVisualStyleBackColor = true;
            this.chkDecrescente.CheckedChanged += new System.EventHandler(this.chkDecrescente_CheckedChanged);
            // 
            // chkCrescente
            // 
            this.chkCrescente.AutoSize = true;
            this.chkCrescente.Checked = true;
            this.chkCrescente.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCrescente.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCrescente.Location = new System.Drawing.Point(499, 56);
            this.chkCrescente.Name = "chkCrescente";
            this.chkCrescente.Size = new System.Drawing.Size(84, 17);
            this.chkCrescente.TabIndex = 4;
            this.chkCrescente.Text = "Crescente";
            this.chkCrescente.UseVisualStyleBackColor = true;
            this.chkCrescente.CheckedChanged += new System.EventHandler(this.chkCrescente_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(92, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 173;
            this.label4.Text = "CPF/CNPJ";
            this.label4.Click += new System.EventHandler(this.label4_Click_1);
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(94, 56);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(154, 21);
            this.textBox4.TabIndex = 3;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            this.textBox4.Enter += new System.EventHandler(this.textBox4_Enter);
            this.textBox4.Leave += new System.EventHandler(this.textBox4_Leave);
            // 
            // rbCPF
            // 
            this.rbCPF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbCPF.AutoSize = true;
            this.rbCPF.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCPF.Location = new System.Drawing.Point(902, 56);
            this.rbCPF.Name = "rbCPF";
            this.rbCPF.Size = new System.Drawing.Size(81, 17);
            this.rbCPF.TabIndex = 8;
            this.rbCPF.TabStop = true;
            this.rbCPF.Text = "&CPF/CNPJ";
            this.rbCPF.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(748, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(245, 25);
            this.label11.TabIndex = 170;
            this.label11.Text = "Opções de Ordenação";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(21, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(234, 25);
            this.label7.TabIndex = 171;
            this.label7.Text = "Critérios de Pesquisa";
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape3});
            this.shapeContainer2.Size = new System.Drawing.Size(1112, 77);
            this.shapeContainer2.TabIndex = 174;
            this.shapeContainer2.TabStop = false;
            // 
            // lineShape3
            // 
            this.lineShape3.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.X1 = 711;
            this.lineShape3.X2 = 711;
            this.lineShape3.Y1 = -18;
            this.lineShape3.Y2 = 67;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMensagem});
            this.statusStrip1.Location = new System.Drawing.Point(0, 709);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1143, 22);
            this.statusStrip1.TabIndex = 66;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // tssMensagem
            // 
            this.tssMensagem.BackColor = System.Drawing.Color.White;
            this.tssMensagem.ForeColor = System.Drawing.Color.Red;
            this.tssMensagem.Name = "tssMensagem";
            this.tssMensagem.Size = new System.Drawing.Size(0, 17);
            // 
            // btnAlterar
            // 
            this.btnAlterar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAlterar.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnAlterar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAlterar.Location = new System.Drawing.Point(921, 459);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(95, 40);
            this.btnAlterar.TabIndex = 262;
            this.btnAlterar.Text = "&Alterar";
            this.toolTip1.SetToolTip(this.btnAlterar, "Clique duas vezes na planilha sobre a linha a ser alterada");
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExcluir.BackgroundImage")));
            this.btnExcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExcluir.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnExcluir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExcluir.Location = new System.Drawing.Point(921, 411);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(95, 40);
            this.btnExcluir.TabIndex = 260;
            this.btnExcluir.Text = "      &Excluir";
            this.toolTip1.SetToolTip(this.btnExcluir, "Clique duas vezes na planilha sobre a linha a ser alterada");
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 61;
            this.lineShape1.X2 = 1100;
            this.lineShape1.Y1 = 123;
            this.lineShape1.Y2 = 123;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape6,
            this.lineShape5,
            this.lineShape4,
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1143, 731);
            this.shapeContainer1.TabIndex = 67;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape6
            // 
            this.lineShape6.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.lineShape6.Name = "lineShape6";
            this.lineShape6.X1 = 844;
            this.lineShape6.X2 = 844;
            this.lineShape6.Y1 = 407;
            this.lineShape6.Y2 = 667;
            // 
            // lineShape5
            // 
            this.lineShape5.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.lineShape5.Name = "lineShape5";
            this.lineShape5.X1 = 61;
            this.lineShape5.X2 = 1100;
            this.lineShape5.Y1 = 687;
            this.lineShape5.Y2 = 687;
            // 
            // lineShape4
            // 
            this.lineShape4.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.lineShape4.Name = "lineShape4";
            this.lineShape4.X1 = 61;
            this.lineShape4.X2 = 1100;
            this.lineShape4.Y1 = 12;
            this.lineShape4.Y2 = 12;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 61;
            this.lineShape2.X2 = 1100;
            this.lineShape2.Y1 = 390;
            this.lineShape2.Y2 = 390;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnOK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnOK.Location = new System.Drawing.Point(921, 555);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 40);
            this.btnOK.TabIndex = 263;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnIncluir
            // 
            this.btnIncluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnIncluir.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnIncluir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnIncluir.Location = new System.Drawing.Point(921, 507);
            this.btnIncluir.Name = "btnIncluir";
            this.btnIncluir.Size = new System.Drawing.Size(95, 40);
            this.btnIncluir.TabIndex = 261;
            this.btnIncluir.Text = "&Incluir";
            this.btnIncluir.UseVisualStyleBackColor = true;
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // btnSair
            // 
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Location = new System.Drawing.Point(921, 625);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(95, 40);
            this.btnSair.TabIndex = 259;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.button2_Click);
            // 
            // Fornecedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1143, 731);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.btnIncluir);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bt_visualizar);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fornecedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerenciamento de Fornecedores";
            this.Load += new System.EventHandler(this.Fornecedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFornecedor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_visualizar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.RadioButton rbPorCodigo;
        private System.Windows.Forms.RadioButton rbPorNome;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtIdentificação;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssMensagem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSite;
        private System.Windows.Forms.TextBox txtTipoNumero;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbUF;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbTipoFornecedor;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtEmailContato;
        private System.Windows.Forms.TextBox txtEndereço;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtFone2;
        private System.Windows.Forms.TextBox txtFone1;
        private System.Windows.Forms.TextBox txtResp;
        private System.Windows.Forms.Label lblTipoNumero;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.RadioButton rbCPF;
        private System.Windows.Forms.CheckBox chkCrescente;
        private System.Windows.Forms.CheckBox chkDecrescente;
        private System.Windows.Forms.ToolTip toolTip1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape6;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape5;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnIncluir;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
}
}