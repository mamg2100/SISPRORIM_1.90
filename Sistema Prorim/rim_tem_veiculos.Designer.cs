namespace Sistema_prorim
{
    partial class rim_tem_veiculos
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblCodVeiculo = new System.Windows.Forms.Label();
            this.txtCodRI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCetil = new System.Windows.Forms.TextBox();
            this.lblCetil = new System.Windows.Forms.Label();
            this.rbExclui = new System.Windows.Forms.RadioButton();
            this.rbNovo = new System.Windows.Forms.RadioButton();
            this.rbAlterar = new System.Windows.Forms.RadioButton();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.bt_Gravar = new System.Windows.Forms.Button();
            this.txtAnoVeiculo = new System.Windows.Forms.TextBox();
            this.lblAnoVeiculo = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.cmbEscolha = new System.Windows.Forms.ComboBox();
            this.lblUnidade = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.cmbPlaca = new System.Windows.Forms.ComboBox();
            this.txtCodPlaca = new System.Windows.Forms.TextBox();
            this.bt_Excluir = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.txtSetorVeiculo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(447, 178);
            this.dataGridView1.TabIndex = 216;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // lblCodVeiculo
            // 
            this.lblCodVeiculo.AutoSize = true;
            this.lblCodVeiculo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodVeiculo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodVeiculo.Location = new System.Drawing.Point(171, 299);
            this.lblCodVeiculo.Name = "lblCodVeiculo";
            this.lblCodVeiculo.Size = new System.Drawing.Size(75, 16);
            this.lblCodVeiculo.TabIndex = 215;
            this.lblCodVeiculo.Text = "Cod Veiculo";
            // 
            // txtCodRI
            // 
            this.txtCodRI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtCodRI.Enabled = false;
            this.txtCodRI.ForeColor = System.Drawing.Color.Black;
            this.txtCodRI.Location = new System.Drawing.Point(90, 316);
            this.txtCodRI.Name = "txtCodRI";
            this.txtCodRI.Size = new System.Drawing.Size(74, 20);
            this.txtCodRI.TabIndex = 214;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(92, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 213;
            this.label1.Text = "Cod R.I";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(13, 186);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 12);
            this.label9.TabIndex = 212;
            // 
            // txtCetil
            // 
            this.txtCetil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtCetil.Enabled = false;
            this.txtCetil.ForeColor = System.Drawing.Color.Black;
            this.txtCetil.Location = new System.Drawing.Point(12, 316);
            this.txtCetil.Name = "txtCetil";
            this.txtCetil.Size = new System.Drawing.Size(74, 20);
            this.txtCetil.TabIndex = 210;
            this.txtCetil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCetil_KeyPress);
            // 
            // lblCetil
            // 
            this.lblCetil.AutoSize = true;
            this.lblCetil.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCetil.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCetil.Location = new System.Drawing.Point(15, 299);
            this.lblCetil.Name = "lblCetil";
            this.lblCetil.Size = new System.Drawing.Size(33, 16);
            this.lblCetil.TabIndex = 209;
            this.lblCetil.Text = "Cetil";
            // 
            // rbExclui
            // 
            this.rbExclui.AutoSize = true;
            this.rbExclui.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExclui.Location = new System.Drawing.Point(369, 55);
            this.rbExclui.Name = "rbExclui";
            this.rbExclui.Size = new System.Drawing.Size(63, 17);
            this.rbExclui.TabIndex = 208;
            this.rbExclui.Text = "&Excluir";
            this.rbExclui.UseVisualStyleBackColor = true;
            this.rbExclui.Visible = false;
            // 
            // rbNovo
            // 
            this.rbNovo.AutoSize = true;
            this.rbNovo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNovo.Location = new System.Drawing.Point(369, 25);
            this.rbNovo.Name = "rbNovo";
            this.rbNovo.Size = new System.Drawing.Size(61, 17);
            this.rbNovo.TabIndex = 206;
            this.rbNovo.Text = "&Incluir";
            this.rbNovo.UseVisualStyleBackColor = true;
            this.rbNovo.Visible = false;
            this.rbNovo.CheckedChanged += new System.EventHandler(this.rbNovo_CheckedChanged);
            // 
            // rbAlterar
            // 
            this.rbAlterar.AutoSize = true;
            this.rbAlterar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAlterar.Location = new System.Drawing.Point(369, 83);
            this.rbAlterar.Name = "rbAlterar";
            this.rbAlterar.Size = new System.Drawing.Size(75, 17);
            this.rbAlterar.TabIndex = 207;
            this.rbAlterar.Text = "&Atualizar";
            this.rbAlterar.UseVisualStyleBackColor = true;
            this.rbAlterar.Visible = false;
            // 
            // btnSair
            // 
            this.btnSair.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Location = new System.Drawing.Point(360, 314);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(84, 23);
            this.btnSair.TabIndex = 203;
            this.btnSair.Text = "&Voltar";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(268, 314);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 205;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // bt_Gravar
            // 
            this.bt_Gravar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Gravar.Location = new System.Drawing.Point(360, 237);
            this.bt_Gravar.Name = "bt_Gravar";
            this.bt_Gravar.Size = new System.Drawing.Size(84, 23);
            this.bt_Gravar.TabIndex = 204;
            this.bt_Gravar.Text = "&Vincular";
            this.bt_Gravar.UseVisualStyleBackColor = true;
            this.bt_Gravar.Click += new System.EventHandler(this.bt_Gravar_Click);
            // 
            // txtAnoVeiculo
            // 
            this.txtAnoVeiculo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtAnoVeiculo.Enabled = false;
            this.txtAnoVeiculo.ForeColor = System.Drawing.Color.Black;
            this.txtAnoVeiculo.Location = new System.Drawing.Point(308, 277);
            this.txtAnoVeiculo.Name = "txtAnoVeiculo";
            this.txtAnoVeiculo.Size = new System.Drawing.Size(35, 20);
            this.txtAnoVeiculo.TabIndex = 202;
            // 
            // lblAnoVeiculo
            // 
            this.lblAnoVeiculo.AutoSize = true;
            this.lblAnoVeiculo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnoVeiculo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblAnoVeiculo.Location = new System.Drawing.Point(310, 260);
            this.lblAnoVeiculo.Name = "lblAnoVeiculo";
            this.lblAnoVeiculo.Size = new System.Drawing.Size(30, 16);
            this.lblAnoVeiculo.TabIndex = 201;
            this.lblAnoVeiculo.Text = "Ano";
            // 
            // txtModelo
            // 
            this.txtModelo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtModelo.Enabled = false;
            this.txtModelo.ForeColor = System.Drawing.Color.Black;
            this.txtModelo.Location = new System.Drawing.Point(89, 277);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(215, 20);
            this.txtModelo.TabIndex = 200;
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblModelo.Location = new System.Drawing.Point(92, 260);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(49, 16);
            this.lblModelo.TabIndex = 199;
            this.lblModelo.Text = "Modelo";
            // 
            // txtMarca
            // 
            this.txtMarca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtMarca.Enabled = false;
            this.txtMarca.ForeColor = System.Drawing.Color.Black;
            this.txtMarca.Location = new System.Drawing.Point(12, 277);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(74, 20);
            this.txtMarca.TabIndex = 198;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMarca.Location = new System.Drawing.Point(15, 260);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(43, 16);
            this.lblMarca.TabIndex = 197;
            this.lblMarca.Text = "Marca";
            // 
            // cmbEscolha
            // 
            this.cmbEscolha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbEscolha.Enabled = false;
            this.cmbEscolha.FormattingEnabled = true;
            this.cmbEscolha.Location = new System.Drawing.Point(313, 186);
            this.cmbEscolha.Name = "cmbEscolha";
            this.cmbEscolha.Size = new System.Drawing.Size(143, 21);
            this.cmbEscolha.TabIndex = 195;
            this.cmbEscolha.Visible = false;
            // 
            // lblUnidade
            // 
            this.lblUnidade.AutoSize = true;
            this.lblUnidade.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidade.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblUnidade.Location = new System.Drawing.Point(15, 220);
            this.lblUnidade.Name = "lblUnidade";
            this.lblUnidade.Size = new System.Drawing.Size(201, 16);
            this.lblUnidade.TabIndex = 196;
            this.lblUnidade.Text = "Unidade/Setor (gestor do veículo)";
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Enabled = false;
            this.lblPlaca.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaca.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPlaca.Location = new System.Drawing.Point(259, 220);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(38, 16);
            this.lblPlaca.TabIndex = 194;
            this.lblPlaca.Text = "Placa";
            // 
            // cmbPlaca
            // 
            this.cmbPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cmbPlaca.Enabled = false;
            this.cmbPlaca.ForeColor = System.Drawing.Color.Black;
            this.cmbPlaca.FormattingEnabled = true;
            this.cmbPlaca.Location = new System.Drawing.Point(257, 236);
            this.cmbPlaca.Name = "cmbPlaca";
            this.cmbPlaca.Size = new System.Drawing.Size(83, 21);
            this.cmbPlaca.TabIndex = 193;
            // 
            // txtCodPlaca
            // 
            this.txtCodPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtCodPlaca.Enabled = false;
            this.txtCodPlaca.ForeColor = System.Drawing.Color.Black;
            this.txtCodPlaca.Location = new System.Drawing.Point(168, 316);
            this.txtCodPlaca.Name = "txtCodPlaca";
            this.txtCodPlaca.Size = new System.Drawing.Size(74, 20);
            this.txtCodPlaca.TabIndex = 217;
            // 
            // bt_Excluir
            // 
            this.bt_Excluir.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Excluir.Location = new System.Drawing.Point(360, 275);
            this.bt_Excluir.Name = "bt_Excluir";
            this.bt_Excluir.Size = new System.Drawing.Size(84, 23);
            this.bt_Excluir.TabIndex = 218;
            this.bt_Excluir.Text = "&Desvincular";
            this.bt_Excluir.UseVisualStyleBackColor = true;
            this.bt_Excluir.Click += new System.EventHandler(this.bt_Excluir_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 365);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(464, 25);
            this.toolStrip1.TabIndex = 219;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripTextBox1.ForeColor = System.Drawing.Color.Red;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(400, 25);
            // 
            // txtSetorVeiculo
            // 
            this.txtSetorVeiculo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtSetorVeiculo.Enabled = false;
            this.txtSetorVeiculo.ForeColor = System.Drawing.Color.Black;
            this.txtSetorVeiculo.Location = new System.Drawing.Point(12, 236);
            this.txtSetorVeiculo.Name = "txtSetorVeiculo";
            this.txtSetorVeiculo.Size = new System.Drawing.Size(242, 20);
            this.txtSetorVeiculo.TabIndex = 220;
            // 
            // rim_tem_veiculos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(464, 390);
            this.Controls.Add(this.txtSetorVeiculo);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.bt_Excluir);
            this.Controls.Add(this.txtCodPlaca);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblCodVeiculo);
            this.Controls.Add(this.txtCodRI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCetil);
            this.Controls.Add(this.lblCetil);
            this.Controls.Add(this.rbExclui);
            this.Controls.Add(this.rbNovo);
            this.Controls.Add(this.rbAlterar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.bt_Gravar);
            this.Controls.Add(this.txtAnoVeiculo);
            this.Controls.Add(this.lblAnoVeiculo);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.lblMarca);
            this.Controls.Add(this.cmbEscolha);
            this.Controls.Add(this.lblUnidade);
            this.Controls.Add(this.lblPlaca);
            this.Controls.Add(this.cmbPlaca);
            this.Name = "rim_tem_veiculos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vinculação de Veículos & Requisição";
            this.Load += new System.EventHandler(this.rim_tem_veiculos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        internal System.Windows.Forms.Label lblCodVeiculo;
        internal System.Windows.Forms.TextBox txtCodRI;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox txtCetil;
        internal System.Windows.Forms.Label lblCetil;
        private System.Windows.Forms.RadioButton rbExclui;
        private System.Windows.Forms.RadioButton rbNovo;
        private System.Windows.Forms.RadioButton rbAlterar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button bt_Gravar;
        internal System.Windows.Forms.TextBox txtAnoVeiculo;
        internal System.Windows.Forms.Label lblAnoVeiculo;
        internal System.Windows.Forms.TextBox txtModelo;
        internal System.Windows.Forms.Label lblModelo;
        internal System.Windows.Forms.TextBox txtMarca;
        internal System.Windows.Forms.Label lblMarca;
        internal System.Windows.Forms.ComboBox cmbEscolha;
        internal System.Windows.Forms.Label lblUnidade;
        internal System.Windows.Forms.Label lblPlaca;
        internal System.Windows.Forms.ComboBox cmbPlaca;
        internal System.Windows.Forms.TextBox txtCodPlaca;
        private System.Windows.Forms.Button bt_Excluir;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        internal System.Windows.Forms.TextBox txtSetorVeiculo;
    }
}