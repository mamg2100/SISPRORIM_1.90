namespace Sistema_prorim
{
    partial class rim_tem_fornecedores
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
            this.lblRI = new System.Windows.Forms.Label();
            this.rbExclui = new System.Windows.Forms.RadioButton();
            this.rbNovo = new System.Windows.Forms.RadioButton();
            this.rbAlterar = new System.Windows.Forms.RadioButton();
            this.txtCodRim = new System.Windows.Forms.TextBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.txtCodFornecedor = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.bt_Gravar = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssMensagem = new System.Windows.Forms.ToolStripStatusLabel();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(524, 161);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // lblRI
            // 
            this.lblRI.AutoSize = true;
            this.lblRI.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRI.Location = new System.Drawing.Point(24, 40);
            this.lblRI.Name = "lblRI";
            this.lblRI.Size = new System.Drawing.Size(134, 13);
            this.lblRI.TabIndex = 69;
            this.lblRI.Text = "Código Sequencial R.I";
            // 
            // rbExclui
            // 
            this.rbExclui.AutoSize = true;
            this.rbExclui.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExclui.Location = new System.Drawing.Point(298, 82);
            this.rbExclui.Name = "rbExclui";
            this.rbExclui.Size = new System.Drawing.Size(63, 17);
            this.rbExclui.TabIndex = 68;
            this.rbExclui.Text = "&Excluir";
            this.rbExclui.UseVisualStyleBackColor = true;
            this.rbExclui.CheckedChanged += new System.EventHandler(this.rbExclui_CheckedChanged);
            // 
            // rbNovo
            // 
            this.rbNovo.AutoSize = true;
            this.rbNovo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNovo.Location = new System.Drawing.Point(298, 30);
            this.rbNovo.Name = "rbNovo";
            this.rbNovo.Size = new System.Drawing.Size(54, 17);
            this.rbNovo.TabIndex = 66;
            this.rbNovo.Text = "No&vo";
            this.rbNovo.UseVisualStyleBackColor = true;
            this.rbNovo.CheckedChanged += new System.EventHandler(this.rbNovo_CheckedChanged);
            // 
            // rbAlterar
            // 
            this.rbAlterar.AutoSize = true;
            this.rbAlterar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAlterar.Location = new System.Drawing.Point(298, 56);
            this.rbAlterar.Name = "rbAlterar";
            this.rbAlterar.Size = new System.Drawing.Size(75, 17);
            this.rbAlterar.TabIndex = 67;
            this.rbAlterar.Text = "&Atualizar";
            this.rbAlterar.UseVisualStyleBackColor = true;
            this.rbAlterar.Visible = false;
            this.rbAlterar.CheckedChanged += new System.EventHandler(this.rbAlterar_CheckedChanged);
            // 
            // txtCodRim
            // 
            this.txtCodRim.BackColor = System.Drawing.Color.White;
            this.txtCodRim.Enabled = false;
            this.txtCodRim.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodRim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtCodRim.Location = new System.Drawing.Point(173, 36);
            this.txtCodRim.Name = "txtCodRim";
            this.txtCodRim.Size = new System.Drawing.Size(72, 21);
            this.txtCodRim.TabIndex = 3;
            this.txtCodRim.TextChanged += new System.EventHandler(this.txtCodRim_TextChanged);
            this.txtCodRim.Enter += new System.EventHandler(this.txtCodRim_Enter);
            this.txtCodRim.Leave += new System.EventHandler(this.txtCodRim_Leave);
            // 
            // btnSair
            // 
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Location = new System.Drawing.Point(428, 96);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(79, 23);
            this.btnSair.TabIndex = 10;
            this.btnSair.Text = "&Voltar";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // txtCodFornecedor
            // 
            this.txtCodFornecedor.BackColor = System.Drawing.Color.White;
            this.txtCodFornecedor.Enabled = false;
            this.txtCodFornecedor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodFornecedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCodFornecedor.Location = new System.Drawing.Point(173, 68);
            this.txtCodFornecedor.Name = "txtCodFornecedor";
            this.txtCodFornecedor.Size = new System.Drawing.Size(72, 21);
            this.txtCodFornecedor.TabIndex = 1;
            this.txtCodFornecedor.Enter += new System.EventHandler(this.txtCodFornecedor_Enter);
            this.txtCodFornecedor.Leave += new System.EventHandler(this.txtCodFornecedor_Leave);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.Location = new System.Drawing.Point(24, 71);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(141, 13);
            this.lblCodigo.TabIndex = 47;
            this.lblCodigo.Text = "Código Seq.Fornecedor";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Controls.Add(this.lblRI);
            this.groupBox1.Controls.Add(this.rbExclui);
            this.groupBox1.Controls.Add(this.rbNovo);
            this.groupBox1.Controls.Add(this.rbAlterar);
            this.groupBox1.Controls.Add(this.txtCodRim);
            this.groupBox1.Controls.Add(this.btnSair);
            this.groupBox1.Controls.Add(this.txtCodFornecedor);
            this.groupBox1.Controls.Add(this.lblCodigo);
            this.groupBox1.Controls.Add(this.bt_Gravar);
            this.groupBox1.Location = new System.Drawing.Point(11, 190);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 128);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(428, 47);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(79, 23);
            this.btnCancelar.TabIndex = 70;
            this.btnCancelar.Text = "Cance&lar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // bt_Gravar
            // 
            this.bt_Gravar.Enabled = false;
            this.bt_Gravar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Gravar.Location = new System.Drawing.Point(428, 19);
            this.bt_Gravar.Name = "bt_Gravar";
            this.bt_Gravar.Size = new System.Drawing.Size(79, 23);
            this.bt_Gravar.TabIndex = 10;
            this.bt_Gravar.Text = "&Confirmar";
            this.bt_Gravar.UseVisualStyleBackColor = true;
            this.bt_Gravar.Click += new System.EventHandler(this.bt_Gravar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMensagem});
            this.statusStrip1.Location = new System.Drawing.Point(0, 321);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(547, 22);
            this.statusStrip1.TabIndex = 76;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssMensagem
            // 
            this.tssMensagem.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tssMensagem.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tssMensagem.ForeColor = System.Drawing.Color.Red;
            this.tssMensagem.Name = "tssMensagem";
            this.tssMensagem.Size = new System.Drawing.Size(0, 17);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(12, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 77;
            // 
            // rim_tem_fornecedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(547, 343);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "rim_tem_fornecedores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vinculando Fornecedor & Requisição";
            this.Load += new System.EventHandler(this.rim_tem_fornecedores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblRI;
        private System.Windows.Forms.RadioButton rbExclui;
        private System.Windows.Forms.RadioButton rbNovo;
        private System.Windows.Forms.RadioButton rbAlterar;
        private System.Windows.Forms.TextBox txtCodRim;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.TextBox txtCodFornecedor;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_Gravar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssMensagem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancelar;
    }
}