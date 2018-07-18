namespace Sistema_prorim
{
    partial class NotaFiscal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rbExclui = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCodRim = new System.Windows.Forms.TextBox();
            this.rbPorCodFornecedor = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.rbNovo = new System.Windows.Forms.RadioButton();
            this.txtConsultaCodigoSeq = new System.Windows.Forms.TextBox();
            this.txtConsultaCodFornecedor = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.rbAlterar = new System.Windows.Forms.RadioButton();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.txtNumeroNota = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSituacaoNF = new System.Windows.Forms.CheckBox();
            this.monthCalendar2 = new System.Windows.Forms.MonthCalendar();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSetorEnviado = new System.Windows.Forms.TextBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.txtDataEnvioSetor = new System.Windows.Forms.TextBox();
            this.cmbSetor = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.lblCodFornecedor = new System.Windows.Forms.Label();
            this.txtCodFornecedor = new System.Windows.Forms.TextBox();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodNF = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtValorNF = new System.Windows.Forms.TextBox();
            this.txtDataNotaFiscal = new System.Windows.Forms.TextBox();
            this.lblRIvinculada = new System.Windows.Forms.Label();
            this.bt_Gravar = new System.Windows.Forms.Button();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.rbPorCodigoSeq = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbPorCodRim = new System.Windows.Forms.RadioButton();
            this.rbPorNumeroNota = new System.Windows.Forms.RadioButton();
            this.txtConsultaNrNota = new System.Windows.Forms.Label();
            this.txtConsultaPorNrNotaFiscal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtConsultaPorRI = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssMensagem = new System.Windows.Forms.ToolStripStatusLabel();
            this.bt_visualizar = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtAcumulado = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbExclui
            // 
            this.rbExclui.AutoSize = true;
            this.rbExclui.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExclui.Location = new System.Drawing.Point(546, 109);
            this.rbExclui.Name = "rbExclui";
            this.rbExclui.Size = new System.Drawing.Size(63, 17);
            this.rbExclui.TabIndex = 68;
            this.rbExclui.Text = "&Excluir";
            this.rbExclui.UseVisualStyleBackColor = true;
            this.rbExclui.CheckedChanged += new System.EventHandler(this.rbExclui_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(99, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 13);
            this.label13.TabIndex = 63;
            this.label13.Text = "Cod.Fornecedor";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // txtCodRim
            // 
            this.txtCodRim.BackColor = System.Drawing.Color.Cornsilk;
            this.txtCodRim.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCodRim.Enabled = false;
            this.txtCodRim.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodRim.Location = new System.Drawing.Point(144, 40);
            this.txtCodRim.Name = "txtCodRim";
            this.txtCodRim.Size = new System.Drawing.Size(105, 14);
            this.txtCodRim.TabIndex = 6;
            this.txtCodRim.TextChanged += new System.EventHandler(this.txtCodRim_TextChanged);
            // 
            // rbPorCodFornecedor
            // 
            this.rbPorCodFornecedor.AutoSize = true;
            this.rbPorCodFornecedor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPorCodFornecedor.Location = new System.Drawing.Point(592, 31);
            this.rbPorCodFornecedor.Name = "rbPorCodFornecedor";
            this.rbPorCodFornecedor.Size = new System.Drawing.Size(133, 17);
            this.rbPorCodFornecedor.TabIndex = 7;
            this.rbPorCodFornecedor.TabStop = true;
            this.rbPorCodFornecedor.Text = "&Codigo Fornecedor";
            this.rbPorCodFornecedor.UseVisualStyleBackColor = true;
            this.rbPorCodFornecedor.CheckedChanged += new System.EventHandler(this.rbPorNome_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(5, 332);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 71;
            this.label9.Text = "label9";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // rbNovo
            // 
            this.rbNovo.AutoSize = true;
            this.rbNovo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNovo.Location = new System.Drawing.Point(546, 35);
            this.rbNovo.Name = "rbNovo";
            this.rbNovo.Size = new System.Drawing.Size(54, 17);
            this.rbNovo.TabIndex = 66;
            this.rbNovo.Text = "No&vo";
            this.rbNovo.UseVisualStyleBackColor = true;
            this.rbNovo.CheckedChanged += new System.EventHandler(this.rbNovo_CheckedChanged);
            // 
            // txtConsultaCodigoSeq
            // 
            this.txtConsultaCodigoSeq.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsultaCodigoSeq.Location = new System.Drawing.Point(20, 42);
            this.txtConsultaCodigoSeq.Name = "txtConsultaCodigoSeq";
            this.txtConsultaCodigoSeq.Size = new System.Drawing.Size(78, 21);
            this.txtConsultaCodigoSeq.TabIndex = 1;
            this.txtConsultaCodigoSeq.Enter += new System.EventHandler(this.txtConsultaCodigoSeq_Enter);
            this.txtConsultaCodigoSeq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConsultaCodigoSeq_KeyPress);
            this.txtConsultaCodigoSeq.Leave += new System.EventHandler(this.txtConsultaCodigoSeq_Leave);
            // 
            // txtConsultaCodFornecedor
            // 
            this.txtConsultaCodFornecedor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsultaCodFornecedor.Location = new System.Drawing.Point(102, 42);
            this.txtConsultaCodFornecedor.Name = "txtConsultaCodFornecedor";
            this.txtConsultaCodFornecedor.Size = new System.Drawing.Size(112, 21);
            this.txtConsultaCodFornecedor.TabIndex = 2;
            this.txtConsultaCodFornecedor.Enter += new System.EventHandler(this.txtConsultaCodFornecedor_Enter);
            this.txtConsultaCodFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConsultaCodFornecedor_KeyPress);
            this.txtConsultaCodFornecedor.Leave += new System.EventHandler(this.txtConsultaCodFornecedor_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(20, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 13);
            this.label12.TabIndex = 62;
            this.label12.Text = "Cod.Seq.N.F";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // rbAlterar
            // 
            this.rbAlterar.AutoSize = true;
            this.rbAlterar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAlterar.Location = new System.Drawing.Point(546, 72);
            this.rbAlterar.Name = "rbAlterar";
            this.rbAlterar.Size = new System.Drawing.Size(64, 17);
            this.rbAlterar.TabIndex = 67;
            this.rbAlterar.Text = "&Alterar";
            this.rbAlterar.UseVisualStyleBackColor = true;
            this.rbAlterar.CheckedChanged += new System.EventHandler(this.rbAlterar_CheckedChanged);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.Location = new System.Drawing.Point(431, 52);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(75, 23);
            this.btnExcluir.TabIndex = 16;
            this.btnExcluir.Text = "&Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Visible = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // txtNumeroNota
            // 
            this.txtNumeroNota.Enabled = false;
            this.txtNumeroNota.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroNota.Location = new System.Drawing.Point(19, 118);
            this.txtNumeroNota.Name = "txtNumeroNota";
            this.txtNumeroNota.Size = new System.Drawing.Size(162, 21);
            this.txtNumeroNota.TabIndex = 9;
            this.txtNumeroNota.TextChanged += new System.EventHandler(this.txtNumeroNota_TextChanged);
            this.txtNumeroNota.Enter += new System.EventHandler(this.txtNumeroNota_Enter);
            this.txtNumeroNota.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroNota_KeyPress);
            this.txtNumeroNota.Leave += new System.EventHandler(this.txtNumeroNota_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSituacaoNF);
            this.groupBox1.Controls.Add(this.monthCalendar2);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtSetorEnviado);
            this.groupBox1.Controls.Add(this.monthCalendar1);
            this.groupBox1.Controls.Add(this.txtDataEnvioSetor);
            this.groupBox1.Controls.Add(this.cmbSetor);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.rbExclui);
            this.groupBox1.Controls.Add(this.lblCodFornecedor);
            this.groupBox1.Controls.Add(this.btnExcluir);
            this.groupBox1.Controls.Add(this.rbNovo);
            this.groupBox1.Controls.Add(this.txtNumeroNota);
            this.groupBox1.Controls.Add(this.rbAlterar);
            this.groupBox1.Controls.Add(this.txtCodRim);
            this.groupBox1.Controls.Add(this.txtCodFornecedor);
            this.groupBox1.Controls.Add(this.btnPesquisa);
            this.groupBox1.Controls.Add(this.txtFornecedor);
            this.groupBox1.Controls.Add(this.btnSair);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCodNF);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtValorNF);
            this.groupBox1.Controls.Add(this.txtDataNotaFiscal);
            this.groupBox1.Controls.Add(this.lblRIvinculada);
            this.groupBox1.Controls.Add(this.bt_Gravar);
            this.groupBox1.Controls.Add(this.btnAtualizar);
            this.groupBox1.Location = new System.Drawing.Point(7, 360);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(733, 197);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // chkSituacaoNF
            // 
            this.chkSituacaoNF.AutoSize = true;
            this.chkSituacaoNF.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSituacaoNF.ForeColor = System.Drawing.Color.Red;
            this.chkSituacaoNF.Location = new System.Drawing.Point(630, 156);
            this.chkSituacaoNF.Name = "chkSituacaoNF";
            this.chkSituacaoNF.Size = new System.Drawing.Size(81, 27);
            this.chkSituacaoNF.TabIndex = 151;
            this.chkSituacaoNF.Text = "PAGA";
            this.chkSituacaoNF.UseVisualStyleBackColor = true;
            this.chkSituacaoNF.CheckedChanged += new System.EventHandler(this.chkSituacaoNF_CheckedChanged);
            this.chkSituacaoNF.Click += new System.EventHandler(this.chkSituacaoNF_Click);
            // 
            // monthCalendar2
            // 
            this.monthCalendar2.BackColor = System.Drawing.Color.Silver;
            this.monthCalendar2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthCalendar2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.monthCalendar2.Location = new System.Drawing.Point(307, 21);
            this.monthCalendar2.Name = "monthCalendar2";
            this.monthCalendar2.TabIndex = 145;
            this.monthCalendar2.TitleBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.monthCalendar2.Visible = false;
            this.monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar2_DateSelected);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(297, 148);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 13);
            this.label11.TabIndex = 150;
            this.label11.Text = "Setor Enviado";
            // 
            // txtSetorEnviado
            // 
            this.txtSetorEnviado.Enabled = false;
            this.txtSetorEnviado.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSetorEnviado.Location = new System.Drawing.Point(297, 162);
            this.txtSetorEnviado.Name = "txtSetorEnviado";
            this.txtSetorEnviado.Size = new System.Drawing.Size(128, 21);
            this.txtSetorEnviado.TabIndex = 149;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.BackColor = System.Drawing.Color.Silver;
            this.monthCalendar1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthCalendar1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.monthCalendar1.Location = new System.Drawing.Point(72, 21);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 69;
            this.monthCalendar1.TitleBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // txtDataEnvioSetor
            // 
            this.txtDataEnvioSetor.Enabled = false;
            this.txtDataEnvioSetor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataEnvioSetor.Location = new System.Drawing.Point(187, 162);
            this.txtDataEnvioSetor.Name = "txtDataEnvioSetor";
            this.txtDataEnvioSetor.Size = new System.Drawing.Size(100, 21);
            this.txtDataEnvioSetor.TabIndex = 13;
            this.txtDataEnvioSetor.Enter += new System.EventHandler(this.txtDataEnvioSetor_Enter);
            this.txtDataEnvioSetor.Leave += new System.EventHandler(this.txtDataEnvioSetor_Leave);
            // 
            // cmbSetor
            // 
            this.cmbSetor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbSetor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetor.FormattingEnabled = true;
            this.cmbSetor.Items.AddRange(new object[] {
            "ALMOX.CENTRAL",
            "ALMOX. SAÚDE",
            "DIPE",
            "SECRETÁRIO /p ASS"});
            this.cmbSetor.Location = new System.Drawing.Point(19, 162);
            this.cmbSetor.Name = "cmbSetor";
            this.cmbSetor.Size = new System.Drawing.Size(162, 21);
            this.cmbSetor.TabIndex = 12;
            this.cmbSetor.SelectedIndexChanged += new System.EventHandler(this.cmbSetor_SelectedIndexChanged);
            this.cmbSetor.Enter += new System.EventHandler(this.cmbSetor_Enter);
            this.cmbSetor.Leave += new System.EventHandler(this.cmbSetor_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(189, 148);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 148;
            this.label14.Text = "Data envio:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(20, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 13);
            this.label10.TabIndex = 146;
            this.label10.Text = "Enviado para:";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBox6.Enabled = false;
            this.textBox6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(297, 118);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(28, 21);
            this.textBox6.TabIndex = 144;
            this.textBox6.Text = "R$";
            // 
            // lblCodFornecedor
            // 
            this.lblCodFornecedor.AutoSize = true;
            this.lblCodFornecedor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodFornecedor.Location = new System.Drawing.Point(18, 65);
            this.lblCodFornecedor.Name = "lblCodFornecedor";
            this.lblCodFornecedor.Size = new System.Drawing.Size(98, 13);
            this.lblCodFornecedor.TabIndex = 50;
            this.lblCodFornecedor.Text = "Cod.Fornecedor";
            this.lblCodFornecedor.Click += new System.EventHandler(this.lblCodFornecedor_Click);
            // 
            // txtCodFornecedor
            // 
            this.txtCodFornecedor.BackColor = System.Drawing.Color.Cornsilk;
            this.txtCodFornecedor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCodFornecedor.Enabled = false;
            this.txtCodFornecedor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodFornecedor.Location = new System.Drawing.Point(20, 79);
            this.txtCodFornecedor.Name = "txtCodFornecedor";
            this.txtCodFornecedor.Size = new System.Drawing.Size(106, 14);
            this.txtCodFornecedor.TabIndex = 7;
            this.txtCodFornecedor.TextChanged += new System.EventHandler(this.txtCodFornecedor_TextChanged);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisa.Location = new System.Drawing.Point(431, 26);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisa.TabIndex = 15;
            this.btnPesquisa.Text = "&Filtrar";
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Visible = false;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // txtFornecedor
            // 
            this.txtFornecedor.BackColor = System.Drawing.Color.Cornsilk;
            this.txtFornecedor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFornecedor.Enabled = false;
            this.txtFornecedor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFornecedor.Location = new System.Drawing.Point(144, 80);
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.Size = new System.Drawing.Size(281, 14);
            this.txtFornecedor.TabIndex = 8;
            this.txtFornecedor.TextChanged += new System.EventHandler(this.txtFornecedor_TextChanged);
            this.txtFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFornecedor_KeyPress);
            // 
            // btnSair
            // 
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Location = new System.Drawing.Point(636, 102);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 16;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(141, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Fornecedor";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(636, 69);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Nº Nota Fiscal";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtCodNF
            // 
            this.txtCodNF.BackColor = System.Drawing.Color.Cornsilk;
            this.txtCodNF.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCodNF.Enabled = false;
            this.txtCodNF.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodNF.Location = new System.Drawing.Point(20, 40);
            this.txtCodNF.Name = "txtCodNF";
            this.txtCodNF.Size = new System.Drawing.Size(105, 14);
            this.txtCodNF.TabIndex = 5;
            this.txtCodNF.TextChanged += new System.EventHandler(this.txtCodNF_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(186, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Data N.F.";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(18, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Cod N.F.";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(321, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Valor N.F.";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtValorNF
            // 
            this.txtValorNF.Enabled = false;
            this.txtValorNF.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorNF.Location = new System.Drawing.Point(325, 118);
            this.txtValorNF.Name = "txtValorNF";
            this.txtValorNF.Size = new System.Drawing.Size(100, 21);
            this.txtValorNF.TabIndex = 11;
            this.txtValorNF.TextChanged += new System.EventHandler(this.txtValorNF_TextChanged);
            this.txtValorNF.Enter += new System.EventHandler(this.txtValorNF_Enter);
            this.txtValorNF.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorNF_KeyPress);
            this.txtValorNF.Leave += new System.EventHandler(this.txtValorNF_Leave);
            // 
            // txtDataNotaFiscal
            // 
            this.txtDataNotaFiscal.Enabled = false;
            this.txtDataNotaFiscal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataNotaFiscal.Location = new System.Drawing.Point(189, 118);
            this.txtDataNotaFiscal.Name = "txtDataNotaFiscal";
            this.txtDataNotaFiscal.Size = new System.Drawing.Size(100, 21);
            this.txtDataNotaFiscal.TabIndex = 10;
            this.txtDataNotaFiscal.TextChanged += new System.EventHandler(this.txtDataNotaFiscal_TextChanged);
            this.txtDataNotaFiscal.Enter += new System.EventHandler(this.txtDataNotaFiscal_Enter);
            this.txtDataNotaFiscal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataNotaFiscal_KeyPress);
            this.txtDataNotaFiscal.Leave += new System.EventHandler(this.txtDataNotaFiscal_Leave);
            // 
            // lblRIvinculada
            // 
            this.lblRIvinculada.AutoSize = true;
            this.lblRIvinculada.Enabled = false;
            this.lblRIvinculada.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRIvinculada.Location = new System.Drawing.Point(141, 25);
            this.lblRIvinculada.Name = "lblRIvinculada";
            this.lblRIvinculada.Size = new System.Drawing.Size(114, 13);
            this.lblRIvinculada.TabIndex = 34;
            this.lblRIvinculada.Text = "Cod RIM vinculada";
            this.lblRIvinculada.Click += new System.EventHandler(this.lblRIvinculada_Click);
            // 
            // bt_Gravar
            // 
            this.bt_Gravar.Enabled = false;
            this.bt_Gravar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Gravar.Location = new System.Drawing.Point(636, 36);
            this.bt_Gravar.Name = "bt_Gravar";
            this.bt_Gravar.Size = new System.Drawing.Size(75, 23);
            this.bt_Gravar.TabIndex = 14;
            this.bt_Gravar.Text = "&OK";
            this.bt_Gravar.UseVisualStyleBackColor = true;
            this.bt_Gravar.Click += new System.EventHandler(this.bt_Gravar_Click);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Enabled = false;
            this.btnAtualizar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.Location = new System.Drawing.Point(431, 79);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(75, 23);
            this.btnAtualizar.TabIndex = 17;
            this.btnAtualizar.Text = "A&tualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Visible = false;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // rbPorCodigoSeq
            // 
            this.rbPorCodigoSeq.AutoSize = true;
            this.rbPorCodigoSeq.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPorCodigoSeq.Location = new System.Drawing.Point(495, 31);
            this.rbPorCodigoSeq.Name = "rbPorCodigoSeq";
            this.rbPorCodigoSeq.Size = new System.Drawing.Size(91, 17);
            this.rbPorCodigoSeq.TabIndex = 5;
            this.rbPorCodigoSeq.Text = "Codi&go Seq";
            this.rbPorCodigoSeq.UseVisualStyleBackColor = true;
            this.rbPorCodigoSeq.CheckedChanged += new System.EventHandler(this.rbPorCodigoFornecedor_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
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
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.Location = new System.Drawing.Point(6, 103);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(733, 226);
            this.dataGridView1.TabIndex = 69;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            this.dataGridView1.MouseEnter += new System.EventHandler(this.dataGridView1_MouseEnter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPorCodRim);
            this.groupBox2.Controls.Add(this.rbPorNumeroNota);
            this.groupBox2.Controls.Add(this.txtConsultaNrNota);
            this.groupBox2.Controls.Add(this.txtConsultaPorNrNotaFiscal);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtConsultaPorRI);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.rbPorCodigoSeq);
            this.groupBox2.Controls.Add(this.rbPorCodFornecedor);
            this.groupBox2.Controls.Add(this.txtConsultaCodigoSeq);
            this.groupBox2.Controls.Add(this.txtConsultaCodFornecedor);
            this.groupBox2.Location = new System.Drawing.Point(7, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(733, 82);
            this.groupBox2.TabIndex = 72;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // rbPorCodRim
            // 
            this.rbPorCodRim.AutoSize = true;
            this.rbPorCodRim.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPorCodRim.Location = new System.Drawing.Point(592, 55);
            this.rbPorCodRim.Name = "rbPorCodRim";
            this.rbPorCodRim.Size = new System.Drawing.Size(82, 17);
            this.rbPorCodRim.TabIndex = 8;
            this.rbPorCodRim.Text = "Codigo &RI";
            this.rbPorCodRim.UseVisualStyleBackColor = true;
            this.rbPorCodRim.CheckedChanged += new System.EventHandler(this.rbPorCodRim_CheckedChanged);
            // 
            // rbPorNumeroNota
            // 
            this.rbPorNumeroNota.AutoSize = true;
            this.rbPorNumeroNota.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPorNumeroNota.Location = new System.Drawing.Point(495, 55);
            this.rbPorNumeroNota.Name = "rbPorNumeroNota";
            this.rbPorNumeroNota.Size = new System.Drawing.Size(69, 17);
            this.rbPorNumeroNota.TabIndex = 6;
            this.rbPorNumeroNota.TabStop = true;
            this.rbPorNumeroNota.Text = "&Nº Nota";
            this.rbPorNumeroNota.UseVisualStyleBackColor = true;
            this.rbPorNumeroNota.CheckedChanged += new System.EventHandler(this.rbPorValorNota_CheckedChanged);
            // 
            // txtConsultaNrNota
            // 
            this.txtConsultaNrNota.AutoSize = true;
            this.txtConsultaNrNota.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsultaNrNota.Location = new System.Drawing.Point(218, 28);
            this.txtConsultaNrNota.Name = "txtConsultaNrNota";
            this.txtConsultaNrNota.Size = new System.Drawing.Size(51, 13);
            this.txtConsultaNrNota.TabIndex = 67;
            this.txtConsultaNrNota.Text = "Nº Nota";
            this.txtConsultaNrNota.Click += new System.EventHandler(this.txtConsultaNrNota_Click);
            // 
            // txtConsultaPorNrNotaFiscal
            // 
            this.txtConsultaPorNrNotaFiscal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsultaPorNrNotaFiscal.Location = new System.Drawing.Point(219, 42);
            this.txtConsultaPorNrNotaFiscal.Name = "txtConsultaPorNrNotaFiscal";
            this.txtConsultaPorNrNotaFiscal.Size = new System.Drawing.Size(112, 21);
            this.txtConsultaPorNrNotaFiscal.TabIndex = 3;
            this.txtConsultaPorNrNotaFiscal.Enter += new System.EventHandler(this.txtConsultaPorNrNotaFiscal_Enter);
            this.txtConsultaPorNrNotaFiscal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConsultaPorNrNotaFiscal_KeyPress);
            this.txtConsultaPorNrNotaFiscal.Leave += new System.EventHandler(this.txtConsultaPorNrNotaFiscal_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(334, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 65;
            this.label5.Text = "R.I.";
            this.label5.Visible = false;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txtConsultaPorRI
            // 
            this.txtConsultaPorRI.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsultaPorRI.Location = new System.Drawing.Point(336, 42);
            this.txtConsultaPorRI.Name = "txtConsultaPorRI";
            this.txtConsultaPorRI.Size = new System.Drawing.Size(112, 21);
            this.txtConsultaPorRI.TabIndex = 4;
            this.txtConsultaPorRI.Visible = false;
            this.txtConsultaPorRI.TextChanged += new System.EventHandler(this.txtConsultaValorNF_TextChanged);
            this.txtConsultaPorRI.Enter += new System.EventHandler(this.txtConsultaPorRI_Enter);
            this.txtConsultaPorRI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConsultaPorRI_KeyPress);
            this.txtConsultaPorRI.Leave += new System.EventHandler(this.txtConsultaPorRI_Leave);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label38.Location = new System.Drawing.Point(89, 7);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(72, 25);
            this.label38.TabIndex = 167;
            this.label38.Text = "filtros";
            this.label38.Click += new System.EventHandler(this.label38_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(491, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(237, 25);
            this.label6.TabIndex = 168;
            this.label6.Text = "opções de ordenação";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMensagem});
            this.statusStrip1.Location = new System.Drawing.Point(0, 560);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(745, 22);
            this.statusStrip1.TabIndex = 169;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssMensagem
            // 
            this.tssMensagem.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tssMensagem.ForeColor = System.Drawing.Color.Red;
            this.tssMensagem.Name = "tssMensagem";
            this.tssMensagem.Size = new System.Drawing.Size(0, 17);
            // 
            // bt_visualizar
            // 
            this.bt_visualizar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_visualizar.Location = new System.Drawing.Point(659, 332);
            this.bt_visualizar.Name = "bt_visualizar";
            this.bt_visualizar.Size = new System.Drawing.Size(75, 23);
            this.bt_visualizar.TabIndex = 170;
            this.bt_visualizar.Text = "&Visualizar";
            this.bt_visualizar.UseVisualStyleBackColor = true;
            this.bt_visualizar.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(480, 329);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(21, 20);
            this.textBox2.TabIndex = 173;
            this.textBox2.Text = "R$";
            // 
            // txtAcumulado
            // 
            this.txtAcumulado.Enabled = false;
            this.txtAcumulado.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcumulado.Location = new System.Drawing.Point(502, 329);
            this.txtAcumulado.Name = "txtAcumulado";
            this.txtAcumulado.Size = new System.Drawing.Size(142, 21);
            this.txtAcumulado.TabIndex = 171;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(348, 332);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 13);
            this.label7.TabIndex = 172;
            this.label7.Text = "Valor Acumulado N.F";
            this.label7.Click += new System.EventHandler(this.label7_Click_1);
            // 
            // NotaFiscal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(745, 582);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txtAcumulado);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bt_visualizar);
            this.KeyPreview = true;
            this.Name = "NotaFiscal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nota Fiscal";
            this.Load += new System.EventHandler(this.NotaFiscal_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NotaFiscal_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbExclui;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCodRim;
        private System.Windows.Forms.RadioButton rbPorCodFornecedor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbNovo;
        private System.Windows.Forms.TextBox txtConsultaCodigoSeq;
        private System.Windows.Forms.TextBox txtConsultaCodFornecedor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rbAlterar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.TextBox txtNumeroNota;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCodFornecedor;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.TextBox txtFornecedor;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodNF;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtValorNF;
        private System.Windows.Forms.TextBox txtDataNotaFiscal;
        private System.Windows.Forms.Label lblRIvinculada;
        private System.Windows.Forms.Button bt_Gravar;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.RadioButton rbPorCodigoSeq;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblCodFornecedor;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        internal System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label txtConsultaNrNota;
        private System.Windows.Forms.TextBox txtConsultaPorNrNotaFiscal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtConsultaPorRI;
        private System.Windows.Forms.RadioButton rbPorNumeroNota;
        private System.Windows.Forms.RadioButton rbPorCodRim;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssMensagem;
        private System.Windows.Forms.Button bt_visualizar;
        internal System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txtAcumulado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MonthCalendar monthCalendar2;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.ComboBox cmbSetor;
        private System.Windows.Forms.TextBox txtDataEnvioSetor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSetorEnviado;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkSituacaoNF;
    }
}