namespace Sistema_Prorim
{
    partial class FSM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSM));
            this.txtMelhoria = new System.Windows.Forms.TextBox();
            this.lblMelhoria = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnIncluir = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.txtFeedback = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEmAnalise = new System.Windows.Forms.CheckBox();
            this.chkResolvido = new System.Windows.Forms.CheckBox();
            this.chkSemPossibilidade = new System.Windows.Forms.CheckBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbRegistrado = new System.Windows.Forms.RadioButton();
            this.rbSemPossibilidade = new System.Windows.Forms.RadioButton();
            this.rbResolvido = new System.Windows.Forms.RadioButton();
            this.rbAnalise = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMelhoria
            // 
            this.txtMelhoria.BackColor = System.Drawing.Color.LightGray;
            this.txtMelhoria.Enabled = false;
            this.txtMelhoria.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMelhoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtMelhoria.Location = new System.Drawing.Point(55, 257);
            this.txtMelhoria.Multiline = true;
            this.txtMelhoria.Name = "txtMelhoria";
            this.txtMelhoria.Size = new System.Drawing.Size(569, 99);
            this.txtMelhoria.TabIndex = 0;
            this.txtMelhoria.Enter += new System.EventHandler(this.txtMelhoria_Enter);
            this.txtMelhoria.Leave += new System.EventHandler(this.txtMelhoria_Leave);
            // 
            // lblMelhoria
            // 
            this.lblMelhoria.AutoSize = true;
            this.lblMelhoria.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMelhoria.Location = new System.Drawing.Point(55, 236);
            this.lblMelhoria.Name = "lblMelhoria";
            this.lblMelhoria.Size = new System.Drawing.Size(150, 16);
            this.lblMelhoria.TabIndex = 1;
            this.lblMelhoria.Text = "Sugestão de Melhoria";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(207, 236);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 16);
            this.lblID.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnOK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnOK.Location = new System.Drawing.Point(351, 501);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 40);
            this.btnOK.TabIndex = 262;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAlterar.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnAlterar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAlterar.Location = new System.Drawing.Point(212, 501);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(95, 40);
            this.btnAlterar.TabIndex = 261;
            this.btnAlterar.Text = "&Alterar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnIncluir
            // 
            this.btnIncluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnIncluir.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnIncluir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnIncluir.Location = new System.Drawing.Point(73, 501);
            this.btnIncluir.Name = "btnIncluir";
            this.btnIncluir.Size = new System.Drawing.Size(95, 40);
            this.btnIncluir.TabIndex = 260;
            this.btnIncluir.Text = "&Incluir";
            this.btnIncluir.UseVisualStyleBackColor = true;
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExcluir.BackgroundImage")));
            this.btnExcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExcluir.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnExcluir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExcluir.Location = new System.Drawing.Point(490, 501);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(95, 40);
            this.btnExcluir.TabIndex = 259;
            this.btnExcluir.Text = "      &Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // txtFeedback
            // 
            this.txtFeedback.BackColor = System.Drawing.Color.LightGray;
            this.txtFeedback.Enabled = false;
            this.txtFeedback.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFeedback.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtFeedback.Location = new System.Drawing.Point(55, 379);
            this.txtFeedback.Multiline = true;
            this.txtFeedback.Name = "txtFeedback";
            this.txtFeedback.Size = new System.Drawing.Size(569, 99);
            this.txtFeedback.TabIndex = 1;
            this.txtFeedback.Enter += new System.EventHandler(this.txtFeedback_Enter);
            this.txtFeedback.Leave += new System.EventHandler(this.txtFeedback_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 264;
            this.label1.Text = "FeedBack";
            // 
            // btnSair
            // 
            this.btnSair.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnSair.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSair.Location = new System.Drawing.Point(688, 501);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(95, 40);
            this.btnSair.TabIndex = 265;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(679, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 266;
            this.label2.Text = "Status Atual";
            // 
            // chkEmAnalise
            // 
            this.chkEmAnalise.AutoSize = true;
            this.chkEmAnalise.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmAnalise.Location = new System.Drawing.Point(672, 308);
            this.chkEmAnalise.Name = "chkEmAnalise";
            this.chkEmAnalise.Size = new System.Drawing.Size(97, 20);
            this.chkEmAnalise.TabIndex = 267;
            this.chkEmAnalise.Text = "Em Análise";
            this.chkEmAnalise.UseVisualStyleBackColor = true;
            // 
            // chkResolvido
            // 
            this.chkResolvido.AutoSize = true;
            this.chkResolvido.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkResolvido.Location = new System.Drawing.Point(672, 342);
            this.chkResolvido.Name = "chkResolvido";
            this.chkResolvido.Size = new System.Drawing.Size(88, 20);
            this.chkResolvido.TabIndex = 268;
            this.chkResolvido.Text = "Resolvido";
            this.chkResolvido.UseVisualStyleBackColor = true;
            // 
            // chkSemPossibilidade
            // 
            this.chkSemPossibilidade.AutoSize = true;
            this.chkSemPossibilidade.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSemPossibilidade.Location = new System.Drawing.Point(672, 376);
            this.chkSemPossibilidade.Name = "chkSemPossibilidade";
            this.chkSemPossibilidade.Size = new System.Drawing.Size(142, 20);
            this.chkSemPossibilidade.TabIndex = 270;
            this.chkSemPossibilidade.Text = "Sem Possibilidade";
            this.chkSemPossibilidade.UseVisualStyleBackColor = true;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(835, 562);
            this.shapeContainer1.TabIndex = 271;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.Color.Silver;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 29;
            this.lineShape2.X2 = 29;
            this.lineShape2.Y1 = 225;
            this.lineShape2.Y2 = 549;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.Color.Silver;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 643;
            this.lineShape1.X2 = 643;
            this.lineShape1.Y1 = 225;
            this.lineShape1.Y2 = 549;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightGray;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(30, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(782, 187);
            this.dataGridView1.TabIndex = 272;
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(489, 234);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(134, 21);
            this.dateTimePicker1.TabIndex = 273;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 274;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbRegistrado);
            this.panel1.Controls.Add(this.rbSemPossibilidade);
            this.panel1.Controls.Add(this.rbResolvido);
            this.panel1.Controls.Add(this.rbAnalise);
            this.panel1.Location = new System.Drawing.Point(657, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(155, 166);
            this.panel1.TabIndex = 275;
            // 
            // rbRegistrado
            // 
            this.rbRegistrado.AutoSize = true;
            this.rbRegistrado.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRegistrado.Location = new System.Drawing.Point(8, 8);
            this.rbRegistrado.Name = "rbRegistrado";
            this.rbRegistrado.Size = new System.Drawing.Size(93, 18);
            this.rbRegistrado.TabIndex = 3;
            this.rbRegistrado.TabStop = true;
            this.rbRegistrado.Text = "Registrada";
            this.rbRegistrado.UseVisualStyleBackColor = true;
            // 
            // rbSemPossibilidade
            // 
            this.rbSemPossibilidade.AutoSize = true;
            this.rbSemPossibilidade.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSemPossibilidade.Location = new System.Drawing.Point(8, 137);
            this.rbSemPossibilidade.Name = "rbSemPossibilidade";
            this.rbSemPossibilidade.Size = new System.Drawing.Size(138, 18);
            this.rbSemPossibilidade.TabIndex = 2;
            this.rbSemPossibilidade.TabStop = true;
            this.rbSemPossibilidade.Text = "Sem Possibilidade";
            this.rbSemPossibilidade.UseVisualStyleBackColor = true;
            // 
            // rbResolvido
            // 
            this.rbResolvido.AutoSize = true;
            this.rbResolvido.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbResolvido.Location = new System.Drawing.Point(8, 94);
            this.rbResolvido.Name = "rbResolvido";
            this.rbResolvido.Size = new System.Drawing.Size(85, 18);
            this.rbResolvido.TabIndex = 1;
            this.rbResolvido.TabStop = true;
            this.rbResolvido.Text = "Resolvido";
            this.rbResolvido.UseVisualStyleBackColor = true;
            // 
            // rbAnalise
            // 
            this.rbAnalise.AutoSize = true;
            this.rbAnalise.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAnalise.Location = new System.Drawing.Point(8, 51);
            this.rbAnalise.Name = "rbAnalise";
            this.rbAnalise.Size = new System.Drawing.Size(93, 18);
            this.rbAnalise.TabIndex = 0;
            this.rbAnalise.TabStop = true;
            this.rbAnalise.Text = "Em Análise";
            this.rbAnalise.UseVisualStyleBackColor = true;
            // 
            // FSM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(835, 562);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chkSemPossibilidade);
            this.Controls.Add(this.chkResolvido);
            this.Controls.Add(this.chkEmAnalise);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFeedback);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.btnIncluir);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblMelhoria);
            this.Controls.Add(this.txtMelhoria);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "FSM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário de Sugestão de Melhoria";
            this.Load += new System.EventHandler(this.FSM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMelhoria;
        private System.Windows.Forms.Label lblMelhoria;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnIncluir;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.TextBox txtFeedback;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEmAnalise;
        private System.Windows.Forms.CheckBox chkResolvido;
        private System.Windows.Forms.CheckBox chkSemPossibilidade;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbSemPossibilidade;
        private System.Windows.Forms.RadioButton rbResolvido;
        private System.Windows.Forms.RadioButton rbAnalise;
        private System.Windows.Forms.RadioButton rbRegistrado;
    }
}