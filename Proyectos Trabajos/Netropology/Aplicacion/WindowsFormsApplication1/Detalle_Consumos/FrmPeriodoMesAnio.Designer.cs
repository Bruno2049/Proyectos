namespace WindowsFormsApplication1.Detalle_Consumos
{
    partial class FrmPeriodoMesAnio
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgPeriodoMes = new System.Windows.Forms.DataGridView();
            this.dtgPeriodoAnio = new System.Windows.Forms.DataGridView();
            this.MPuntoInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPuntoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPuntoLongitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MValorCadena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MValorEntero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MValorDecimal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MValorFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APuntoInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APuntoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APuntoLongitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AValorCadena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AValorDecimal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AValorEntero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AValorFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPeriodoMes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPeriodoAnio)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(14, 34);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtgPeriodoMes);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgPeriodoAnio);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(867, 356);
            this.splitContainer1.SplitterDistance = 423;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(891, 407);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Periodo Mes y Año";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Periodo Mes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Periodo Año";
            // 
            // dtgPeriodoMes
            // 
            this.dtgPeriodoMes.AllowUserToAddRows = false;
            this.dtgPeriodoMes.AllowUserToDeleteRows = false;
            this.dtgPeriodoMes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgPeriodoMes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgPeriodoMes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MPuntoInicial,
            this.MPuntoFinal,
            this.MPuntoLongitud,
            this.MConcepto,
            this.MDato,
            this.MValorCadena,
            this.MValorEntero,
            this.MValorDecimal,
            this.MValorFecha,
            this.MId});
            this.dtgPeriodoMes.Location = new System.Drawing.Point(15, 38);
            this.dtgPeriodoMes.Name = "dtgPeriodoMes";
            this.dtgPeriodoMes.ReadOnly = true;
            this.dtgPeriodoMes.Size = new System.Drawing.Size(392, 304);
            this.dtgPeriodoMes.TabIndex = 1;
            // 
            // dtgPeriodoAnio
            // 
            this.dtgPeriodoAnio.AllowUserToAddRows = false;
            this.dtgPeriodoAnio.AllowUserToDeleteRows = false;
            this.dtgPeriodoAnio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgPeriodoAnio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgPeriodoAnio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.APuntoInicial,
            this.APuntoFinal,
            this.APuntoLongitud,
            this.AConcepto,
            this.ADato,
            this.AValorCadena,
            this.AValorDecimal,
            this.AValorEntero,
            this.AValorFecha,
            this.AId});
            this.dtgPeriodoAnio.Location = new System.Drawing.Point(17, 38);
            this.dtgPeriodoAnio.Name = "dtgPeriodoAnio";
            this.dtgPeriodoAnio.ReadOnly = true;
            this.dtgPeriodoAnio.Size = new System.Drawing.Size(406, 304);
            this.dtgPeriodoAnio.TabIndex = 1;
            // 
            // MPuntoInicial
            // 
            this.MPuntoInicial.DataPropertyName = "PuntoInicial";
            this.MPuntoInicial.HeaderText = "Inicio";
            this.MPuntoInicial.Name = "MPuntoInicial";
            this.MPuntoInicial.ReadOnly = true;
            // 
            // MPuntoFinal
            // 
            this.MPuntoFinal.DataPropertyName = "PuntoFinal";
            this.MPuntoFinal.HeaderText = "Fin";
            this.MPuntoFinal.Name = "MPuntoFinal";
            this.MPuntoFinal.ReadOnly = true;
            // 
            // MPuntoLongitud
            // 
            this.MPuntoLongitud.DataPropertyName = "PuntoLongitud";
            this.MPuntoLongitud.HeaderText = "Longitud";
            this.MPuntoLongitud.Name = "MPuntoLongitud";
            this.MPuntoLongitud.ReadOnly = true;
            // 
            // MConcepto
            // 
            this.MConcepto.DataPropertyName = "Concepto";
            this.MConcepto.HeaderText = "Concepto";
            this.MConcepto.Name = "MConcepto";
            this.MConcepto.ReadOnly = true;
            // 
            // MDato
            // 
            this.MDato.DataPropertyName = "Dato";
            this.MDato.HeaderText = "Dato";
            this.MDato.Name = "MDato";
            this.MDato.ReadOnly = true;
            // 
            // MValorCadena
            // 
            this.MValorCadena.DataPropertyName = "ValorCadena";
            this.MValorCadena.HeaderText = "Valor";
            this.MValorCadena.Name = "MValorCadena";
            this.MValorCadena.ReadOnly = true;
            // 
            // MValorEntero
            // 
            this.MValorEntero.DataPropertyName = "ValorEntero";
            this.MValorEntero.HeaderText = "ValorEntero";
            this.MValorEntero.Name = "MValorEntero";
            this.MValorEntero.ReadOnly = true;
            this.MValorEntero.Visible = false;
            // 
            // MValorDecimal
            // 
            this.MValorDecimal.DataPropertyName = "ValorDecimal";
            this.MValorDecimal.HeaderText = "ValorDecimal";
            this.MValorDecimal.Name = "MValorDecimal";
            this.MValorDecimal.ReadOnly = true;
            this.MValorDecimal.Visible = false;
            // 
            // MValorFecha
            // 
            this.MValorFecha.DataPropertyName = "ValorFecha";
            this.MValorFecha.HeaderText = "ValorFecha";
            this.MValorFecha.Name = "MValorFecha";
            this.MValorFecha.ReadOnly = true;
            this.MValorFecha.Visible = false;
            // 
            // MId
            // 
            this.MId.DataPropertyName = "Id";
            this.MId.HeaderText = "ID";
            this.MId.Name = "MId";
            this.MId.ReadOnly = true;
            this.MId.Visible = false;
            // 
            // APuntoInicial
            // 
            this.APuntoInicial.DataPropertyName = "PuntoInicial";
            this.APuntoInicial.HeaderText = "Inicio";
            this.APuntoInicial.Name = "APuntoInicial";
            this.APuntoInicial.ReadOnly = true;
            // 
            // APuntoFinal
            // 
            this.APuntoFinal.DataPropertyName = "PuntoFinal";
            this.APuntoFinal.HeaderText = "Fin";
            this.APuntoFinal.Name = "APuntoFinal";
            this.APuntoFinal.ReadOnly = true;
            // 
            // APuntoLongitud
            // 
            this.APuntoLongitud.DataPropertyName = "PuntoLongitud";
            this.APuntoLongitud.HeaderText = "Longitud";
            this.APuntoLongitud.Name = "APuntoLongitud";
            this.APuntoLongitud.ReadOnly = true;
            // 
            // AConcepto
            // 
            this.AConcepto.DataPropertyName = "Concepto";
            this.AConcepto.HeaderText = "Concepto";
            this.AConcepto.Name = "AConcepto";
            this.AConcepto.ReadOnly = true;
            // 
            // ADato
            // 
            this.ADato.DataPropertyName = "Dato";
            this.ADato.HeaderText = "Dato";
            this.ADato.Name = "ADato";
            this.ADato.ReadOnly = true;
            // 
            // AValorCadena
            // 
            this.AValorCadena.DataPropertyName = "ValorCadena";
            this.AValorCadena.HeaderText = "ValorCadena";
            this.AValorCadena.Name = "AValorCadena";
            this.AValorCadena.ReadOnly = true;
            // 
            // AValorDecimal
            // 
            this.AValorDecimal.DataPropertyName = "ValorDecimal";
            this.AValorDecimal.HeaderText = "ValorDecimal";
            this.AValorDecimal.Name = "AValorDecimal";
            this.AValorDecimal.ReadOnly = true;
            this.AValorDecimal.Visible = false;
            // 
            // AValorEntero
            // 
            this.AValorEntero.DataPropertyName = "ValorEntero";
            this.AValorEntero.HeaderText = "ValorEntero";
            this.AValorEntero.Name = "AValorEntero";
            this.AValorEntero.ReadOnly = true;
            this.AValorEntero.Visible = false;
            // 
            // AValorFecha
            // 
            this.AValorFecha.DataPropertyName = "ValorFecha";
            this.AValorFecha.HeaderText = "ValorFecha";
            this.AValorFecha.Name = "AValorFecha";
            this.AValorFecha.ReadOnly = true;
            this.AValorFecha.Visible = false;
            // 
            // AId
            // 
            this.AId.DataPropertyName = "Id";
            this.AId.HeaderText = "Id";
            this.AId.Name = "AId";
            this.AId.ReadOnly = true;
            this.AId.Visible = false;
            // 
            // FrmPeriodoMesAnio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 431);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPeriodoMesAnio";
            this.Text = "FrmPeriodoMesAnio";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgPeriodoMes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPeriodoAnio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dtgPeriodoMes;
        private System.Windows.Forms.DataGridViewTextBoxColumn MPuntoInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn MPuntoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn MPuntoLongitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn MConcepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn MDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn MValorCadena;
        private System.Windows.Forms.DataGridViewTextBoxColumn MValorEntero;
        private System.Windows.Forms.DataGridViewTextBoxColumn MValorDecimal;
        private System.Windows.Forms.DataGridViewTextBoxColumn MValorFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn MId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgPeriodoAnio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn APuntoInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn APuntoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn APuntoLongitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn AConcepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADato;
        private System.Windows.Forms.DataGridViewTextBoxColumn AValorCadena;
        private System.Windows.Forms.DataGridViewTextBoxColumn AValorDecimal;
        private System.Windows.Forms.DataGridViewTextBoxColumn AValorEntero;
        private System.Windows.Forms.DataGridViewTextBoxColumn AValorFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn AId;

    }
}