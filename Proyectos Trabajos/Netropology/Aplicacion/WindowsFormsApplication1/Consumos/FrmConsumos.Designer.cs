namespace WindowsFormsApplication1.Consumos
{
    partial class FrmConsumos
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgConNum = new System.Windows.Forms.DataGridView();
            this.NPuntoInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NPuntoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NLongitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NValorEntero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NValorCadena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NValorDecimal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NValorFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgConFechas = new System.Windows.Forms.DataGridView();
            this.FPuntoInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FPuntoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FLongitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FValorFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FValorEntero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FValorCadena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FValorDecimal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dtgConNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgConFechas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(989, 418);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Consumos";
            // 
            // dtgConNum
            // 
            this.dtgConNum.AllowUserToAddRows = false;
            this.dtgConNum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgConNum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgConNum.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NPuntoInicial,
            this.NPuntoFinal,
            this.NLongitud,
            this.NConcepto,
            this.NDato,
            this.NValorEntero,
            this.NId,
            this.NValorCadena,
            this.NValorDecimal,
            this.NValorFecha});
            this.dtgConNum.Location = new System.Drawing.Point(15, 36);
            this.dtgConNum.Name = "dtgConNum";
            this.dtgConNum.Size = new System.Drawing.Size(436, 343);
            this.dtgConNum.TabIndex = 3;
            // 
            // NPuntoInicial
            // 
            this.NPuntoInicial.DataPropertyName = "PuntoInicial";
            this.NPuntoInicial.HeaderText = "Inicio";
            this.NPuntoInicial.Name = "NPuntoInicial";
            // 
            // NPuntoFinal
            // 
            this.NPuntoFinal.DataPropertyName = "PuntoFinal";
            this.NPuntoFinal.HeaderText = "Fin";
            this.NPuntoFinal.Name = "NPuntoFinal";
            // 
            // NLongitud
            // 
            this.NLongitud.DataPropertyName = "PuntoLongitud";
            this.NLongitud.HeaderText = "Longitud";
            this.NLongitud.Name = "NLongitud";
            // 
            // NConcepto
            // 
            this.NConcepto.DataPropertyName = "Concepto";
            this.NConcepto.HeaderText = "Concepto";
            this.NConcepto.Name = "NConcepto";
            // 
            // NDato
            // 
            this.NDato.DataPropertyName = "Dato";
            this.NDato.HeaderText = "Dato";
            this.NDato.Name = "NDato";
            // 
            // NValorEntero
            // 
            this.NValorEntero.DataPropertyName = "ValorEntero";
            this.NValorEntero.HeaderText = "Valor";
            this.NValorEntero.Name = "NValorEntero";
            // 
            // NId
            // 
            this.NId.DataPropertyName = "Id";
            this.NId.HeaderText = "ID";
            this.NId.Name = "NId";
            this.NId.ReadOnly = true;
            this.NId.Visible = false;
            // 
            // NValorCadena
            // 
            this.NValorCadena.DataPropertyName = "ValorCadena";
            this.NValorCadena.HeaderText = "ValorCadena";
            this.NValorCadena.Name = "NValorCadena";
            this.NValorCadena.ReadOnly = true;
            this.NValorCadena.Visible = false;
            // 
            // NValorDecimal
            // 
            this.NValorDecimal.DataPropertyName = "ValorDecimal";
            this.NValorDecimal.HeaderText = "ValorDecimal";
            this.NValorDecimal.Name = "NValorDecimal";
            this.NValorDecimal.ReadOnly = true;
            this.NValorDecimal.Visible = false;
            // 
            // NValorFecha
            // 
            this.NValorFecha.DataPropertyName = "ValorFecha";
            this.NValorFecha.HeaderText = "ValorFecha";
            this.NValorFecha.Name = "NValorFecha";
            this.NValorFecha.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(288, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Esta Informacion unicamente muestra los valores numericos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Esta Informacion unicamente muestra los valores de fechas";
            // 
            // dtgConFechas
            // 
            this.dtgConFechas.AllowUserToAddRows = false;
            this.dtgConFechas.AllowUserToDeleteRows = false;
            this.dtgConFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgConFechas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgConFechas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FPuntoInicial,
            this.FPuntoFinal,
            this.FLongitud,
            this.FConcepto,
            this.FDato,
            this.FValorFecha,
            this.FValorEntero,
            this.FValorCadena,
            this.FValorDecimal,
            this.FId});
            this.dtgConFechas.Location = new System.Drawing.Point(6, 34);
            this.dtgConFechas.Name = "dtgConFechas";
            this.dtgConFechas.ReadOnly = true;
            this.dtgConFechas.Size = new System.Drawing.Size(480, 343);
            this.dtgConFechas.TabIndex = 0;
            // 
            // FPuntoInicial
            // 
            this.FPuntoInicial.DataPropertyName = "PuntoInicial";
            this.FPuntoInicial.HeaderText = "Inicio";
            this.FPuntoInicial.Name = "FPuntoInicial";
            this.FPuntoInicial.ReadOnly = true;
            // 
            // FPuntoFinal
            // 
            this.FPuntoFinal.DataPropertyName = "PuntoFinal";
            this.FPuntoFinal.HeaderText = "Final";
            this.FPuntoFinal.Name = "FPuntoFinal";
            this.FPuntoFinal.ReadOnly = true;
            // 
            // FLongitud
            // 
            this.FLongitud.DataPropertyName = "PuntoLongitud";
            this.FLongitud.HeaderText = "Longitud";
            this.FLongitud.Name = "FLongitud";
            this.FLongitud.ReadOnly = true;
            // 
            // FConcepto
            // 
            this.FConcepto.DataPropertyName = "Concepto";
            this.FConcepto.HeaderText = "Concepto";
            this.FConcepto.Name = "FConcepto";
            this.FConcepto.ReadOnly = true;
            // 
            // FDato
            // 
            this.FDato.DataPropertyName = "Dato";
            this.FDato.HeaderText = "Dato";
            this.FDato.Name = "FDato";
            this.FDato.ReadOnly = true;
            // 
            // FValorFecha
            // 
            this.FValorFecha.DataPropertyName = "ValorFecha";
            this.FValorFecha.HeaderText = "Valor";
            this.FValorFecha.Name = "FValorFecha";
            this.FValorFecha.ReadOnly = true;
            // 
            // FValorEntero
            // 
            this.FValorEntero.DataPropertyName = "ValorEntero";
            this.FValorEntero.HeaderText = "ValorEntero";
            this.FValorEntero.Name = "FValorEntero";
            this.FValorEntero.ReadOnly = true;
            this.FValorEntero.Visible = false;
            // 
            // FValorCadena
            // 
            this.FValorCadena.DataPropertyName = "ValorCadena";
            this.FValorCadena.HeaderText = "ValorCadena";
            this.FValorCadena.Name = "FValorCadena";
            this.FValorCadena.ReadOnly = true;
            this.FValorCadena.Visible = false;
            // 
            // FValorDecimal
            // 
            this.FValorDecimal.DataPropertyName = "ValorDecimal";
            this.FValorDecimal.HeaderText = "ValorDecimal";
            this.FValorDecimal.Name = "FValorDecimal";
            this.FValorDecimal.ReadOnly = true;
            this.FValorDecimal.Visible = false;
            // 
            // FId
            // 
            this.FId.DataPropertyName = "Id";
            this.FId.HeaderText = "ID";
            this.FId.Name = "FId";
            this.FId.ReadOnly = true;
            this.FId.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(26, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtgConFechas);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgConNum);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(969, 393);
            this.splitContainer1.SplitterDistance = 498;
            this.splitContainer1.TabIndex = 1;
            // 
            // FrmConsumos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 442);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmConsumos";
            this.Text = "FrmConsumos";
            ((System.ComponentModel.ISupportInitialize)(this.dtgConNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgConFechas)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dtgConNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgConFechas;
        private System.Windows.Forms.DataGridViewTextBoxColumn FPuntoInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn FPuntoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn FLongitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn FConcepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn FDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn FValorFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn FValorEntero;
        private System.Windows.Forms.DataGridViewTextBoxColumn FValorCadena;
        private System.Windows.Forms.DataGridViewTextBoxColumn FValorDecimal;
        private System.Windows.Forms.DataGridViewTextBoxColumn FId;
        private System.Windows.Forms.DataGridViewTextBoxColumn NPuntoInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn NPuntoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NLongitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn NConcepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn NValorEntero;
        private System.Windows.Forms.DataGridViewTextBoxColumn NId;
        private System.Windows.Forms.DataGridViewTextBoxColumn NValorCadena;
        private System.Windows.Forms.DataGridViewTextBoxColumn NValorDecimal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NValorFecha;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}