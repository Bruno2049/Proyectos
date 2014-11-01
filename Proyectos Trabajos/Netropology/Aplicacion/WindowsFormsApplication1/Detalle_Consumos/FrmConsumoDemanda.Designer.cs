namespace WindowsFormsApplication1.Detalle_Consumos
{
    partial class FrmConsumoDemanda
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtgConsumos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgDemandas = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.CPuntoInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPuntoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPuntoLongitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CValorCadena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CValorEntero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CValorDecimal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CValorFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DPuntoInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DPuntoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DPuntoLongitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DValorCadena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DValorDecimal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DValorEntero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DValorFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgConsumos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDemandas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1059, 489);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Muestra Consumos y Demandas";
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
            this.splitContainer1.Panel1.Controls.Add(this.dtgConsumos);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgDemandas);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1035, 438);
            this.splitContainer1.SplitterDistance = 503;
            this.splitContainer1.TabIndex = 0;
            // 
            // dtgConsumos
            // 
            this.dtgConsumos.AllowUserToAddRows = false;
            this.dtgConsumos.AllowUserToDeleteRows = false;
            this.dtgConsumos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgConsumos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgConsumos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CPuntoInicial,
            this.CPuntoFinal,
            this.CPuntoLongitud,
            this.CConcepto,
            this.CDato,
            this.CValorCadena,
            this.CValorEntero,
            this.CValorDecimal,
            this.CValorFecha,
            this.CId});
            this.dtgConsumos.Location = new System.Drawing.Point(15, 38);
            this.dtgConsumos.Name = "dtgConsumos";
            this.dtgConsumos.ReadOnly = true;
            this.dtgConsumos.Size = new System.Drawing.Size(472, 386);
            this.dtgConsumos.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Consumos";
            // 
            // dtgDemandas
            // 
            this.dtgDemandas.AllowUserToAddRows = false;
            this.dtgDemandas.AllowUserToDeleteRows = false;
            this.dtgDemandas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDemandas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDemandas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DPuntoInicial,
            this.DPuntoFinal,
            this.DPuntoLongitud,
            this.DConcepto,
            this.DDato,
            this.DValorCadena,
            this.DValorDecimal,
            this.DValorEntero,
            this.DValorFecha,
            this.DId});
            this.dtgDemandas.Location = new System.Drawing.Point(17, 38);
            this.dtgDemandas.Name = "dtgDemandas";
            this.dtgDemandas.ReadOnly = true;
            this.dtgDemandas.Size = new System.Drawing.Size(494, 386);
            this.dtgDemandas.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Demandas";
            // 
            // CPuntoInicial
            // 
            this.CPuntoInicial.DataPropertyName = "PuntoInicial";
            this.CPuntoInicial.HeaderText = "Inicio";
            this.CPuntoInicial.Name = "CPuntoInicial";
            this.CPuntoInicial.ReadOnly = true;
            // 
            // CPuntoFinal
            // 
            this.CPuntoFinal.DataPropertyName = "PuntoFinal";
            this.CPuntoFinal.HeaderText = "Fin";
            this.CPuntoFinal.Name = "CPuntoFinal";
            this.CPuntoFinal.ReadOnly = true;
            // 
            // CPuntoLongitud
            // 
            this.CPuntoLongitud.DataPropertyName = "PuntoLongitud";
            this.CPuntoLongitud.HeaderText = "Longitud";
            this.CPuntoLongitud.Name = "CPuntoLongitud";
            this.CPuntoLongitud.ReadOnly = true;
            // 
            // CConcepto
            // 
            this.CConcepto.DataPropertyName = "Concepto";
            this.CConcepto.HeaderText = "Concepto";
            this.CConcepto.Name = "CConcepto";
            this.CConcepto.ReadOnly = true;
            // 
            // CDato
            // 
            this.CDato.DataPropertyName = "Dato";
            this.CDato.HeaderText = "Dato";
            this.CDato.Name = "CDato";
            this.CDato.ReadOnly = true;
            // 
            // CValorCadena
            // 
            this.CValorCadena.DataPropertyName = "ValorCadena";
            this.CValorCadena.HeaderText = "Valor";
            this.CValorCadena.Name = "CValorCadena";
            this.CValorCadena.ReadOnly = true;
            // 
            // CValorEntero
            // 
            this.CValorEntero.DataPropertyName = "ValorEntero";
            this.CValorEntero.HeaderText = "ValorEntero";
            this.CValorEntero.Name = "CValorEntero";
            this.CValorEntero.ReadOnly = true;
            this.CValorEntero.Visible = false;
            // 
            // CValorDecimal
            // 
            this.CValorDecimal.DataPropertyName = "ValorDecimal";
            this.CValorDecimal.HeaderText = "ValorDecimal";
            this.CValorDecimal.Name = "CValorDecimal";
            this.CValorDecimal.ReadOnly = true;
            this.CValorDecimal.Visible = false;
            // 
            // CValorFecha
            // 
            this.CValorFecha.DataPropertyName = "ValorFecha";
            this.CValorFecha.HeaderText = "ValorFecha";
            this.CValorFecha.Name = "CValorFecha";
            this.CValorFecha.ReadOnly = true;
            this.CValorFecha.Visible = false;
            // 
            // CId
            // 
            this.CId.DataPropertyName = "Id";
            this.CId.HeaderText = "ID";
            this.CId.Name = "CId";
            this.CId.ReadOnly = true;
            this.CId.Visible = false;
            // 
            // DPuntoInicial
            // 
            this.DPuntoInicial.DataPropertyName = "PuntoInicial";
            this.DPuntoInicial.HeaderText = "Inicio";
            this.DPuntoInicial.Name = "DPuntoInicial";
            this.DPuntoInicial.ReadOnly = true;
            // 
            // DPuntoFinal
            // 
            this.DPuntoFinal.DataPropertyName = "PuntoFinal";
            this.DPuntoFinal.HeaderText = "Fin";
            this.DPuntoFinal.Name = "DPuntoFinal";
            this.DPuntoFinal.ReadOnly = true;
            // 
            // DPuntoLongitud
            // 
            this.DPuntoLongitud.DataPropertyName = "PuntoLongitud";
            this.DPuntoLongitud.HeaderText = "Longitud";
            this.DPuntoLongitud.Name = "DPuntoLongitud";
            this.DPuntoLongitud.ReadOnly = true;
            // 
            // DConcepto
            // 
            this.DConcepto.DataPropertyName = "Concepto";
            this.DConcepto.HeaderText = "Concepto";
            this.DConcepto.Name = "DConcepto";
            this.DConcepto.ReadOnly = true;
            // 
            // DDato
            // 
            this.DDato.DataPropertyName = "Dato";
            this.DDato.HeaderText = "Dato";
            this.DDato.Name = "DDato";
            this.DDato.ReadOnly = true;
            // 
            // DValorCadena
            // 
            this.DValorCadena.DataPropertyName = "ValorCadena";
            this.DValorCadena.HeaderText = "ValorCadena";
            this.DValorCadena.Name = "DValorCadena";
            this.DValorCadena.ReadOnly = true;
            // 
            // DValorDecimal
            // 
            this.DValorDecimal.DataPropertyName = "ValorDecimal";
            this.DValorDecimal.HeaderText = "ValorDecimal";
            this.DValorDecimal.Name = "DValorDecimal";
            this.DValorDecimal.ReadOnly = true;
            this.DValorDecimal.Visible = false;
            // 
            // DValorEntero
            // 
            this.DValorEntero.DataPropertyName = "ValorEntero";
            this.DValorEntero.HeaderText = "ValorEntero";
            this.DValorEntero.Name = "DValorEntero";
            this.DValorEntero.ReadOnly = true;
            this.DValorEntero.Visible = false;
            // 
            // DValorFecha
            // 
            this.DValorFecha.DataPropertyName = "ValorFecha";
            this.DValorFecha.HeaderText = "ValorFecha";
            this.DValorFecha.Name = "DValorFecha";
            this.DValorFecha.ReadOnly = true;
            this.DValorFecha.Visible = false;
            // 
            // DId
            // 
            this.DId.DataPropertyName = "Id";
            this.DId.HeaderText = "Id";
            this.DId.Name = "DId";
            this.DId.ReadOnly = true;
            this.DId.Visible = false;
            // 
            // FrmConsumoDemanda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 513);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmConsumoDemanda";
            this.Text = "FrmConsumoDemanda";
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgConsumos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDemandas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dtgConsumos;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPuntoInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPuntoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPuntoLongitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn CConcepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn CDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn CValorCadena;
        private System.Windows.Forms.DataGridViewTextBoxColumn CValorEntero;
        private System.Windows.Forms.DataGridViewTextBoxColumn CValorDecimal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CValorFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn CId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgDemandas;
        private System.Windows.Forms.DataGridViewTextBoxColumn DPuntoInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn DPuntoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn DPuntoLongitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn DConcepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn DDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn DValorCadena;
        private System.Windows.Forms.DataGridViewTextBoxColumn DValorDecimal;
        private System.Windows.Forms.DataGridViewTextBoxColumn DValorEntero;
        private System.Windows.Forms.DataGridViewTextBoxColumn DValorFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn DId;
        private System.Windows.Forms.Label label2;

    }
}