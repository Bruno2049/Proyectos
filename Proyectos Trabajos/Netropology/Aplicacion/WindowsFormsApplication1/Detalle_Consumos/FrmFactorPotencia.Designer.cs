namespace WindowsFormsApplication1.Detalle_Consumos
{
    partial class FrmFactorPotencia
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
            this.dtgFactorPotencia = new System.Windows.Forms.DataGridView();
            this.FPuntoInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FPuntoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FPuntoLongitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FValorDecimal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FValorCadena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FValorEntero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FValorFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFactorPotencia)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dtgFactorPotencia);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(975, 385);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registros de Factor Potencia";
            // 
            // dtgFactorPotencia
            // 
            this.dtgFactorPotencia.AllowUserToAddRows = false;
            this.dtgFactorPotencia.AllowUserToDeleteRows = false;
            this.dtgFactorPotencia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgFactorPotencia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgFactorPotencia.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FPuntoInicial,
            this.FPuntoFinal,
            this.FPuntoLongitud,
            this.FConcepto,
            this.FDato,
            this.FValorDecimal,
            this.FValorCadena,
            this.FValorEntero,
            this.FValorFecha,
            this.FId});
            this.dtgFactorPotencia.Location = new System.Drawing.Point(15, 43);
            this.dtgFactorPotencia.Name = "dtgFactorPotencia";
            this.dtgFactorPotencia.ReadOnly = true;
            this.dtgFactorPotencia.Size = new System.Drawing.Size(940, 318);
            this.dtgFactorPotencia.TabIndex = 2;
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
            this.FPuntoFinal.HeaderText = "Fin";
            this.FPuntoFinal.Name = "FPuntoFinal";
            this.FPuntoFinal.ReadOnly = true;
            // 
            // FPuntoLongitud
            // 
            this.FPuntoLongitud.DataPropertyName = "PuntoLongitud";
            this.FPuntoLongitud.HeaderText = "Longitud";
            this.FPuntoLongitud.Name = "FPuntoLongitud";
            this.FPuntoLongitud.ReadOnly = true;
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
            // FValorDecimal
            // 
            this.FValorDecimal.DataPropertyName = "ValorDecimal";
            this.FValorDecimal.HeaderText = "ValorDecimal";
            this.FValorDecimal.Name = "FValorDecimal";
            this.FValorDecimal.ReadOnly = true;
            // 
            // FValorCadena
            // 
            this.FValorCadena.DataPropertyName = "ValorCadena";
            this.FValorCadena.HeaderText = "ValorCadena";
            this.FValorCadena.Name = "FValorCadena";
            this.FValorCadena.ReadOnly = true;
            this.FValorCadena.Visible = false;
            // 
            // FValorEntero
            // 
            this.FValorEntero.DataPropertyName = "ValorEntero";
            this.FValorEntero.HeaderText = "ValorEntero";
            this.FValorEntero.Name = "FValorEntero";
            this.FValorEntero.ReadOnly = true;
            this.FValorEntero.Visible = false;
            // 
            // FValorFecha
            // 
            this.FValorFecha.DataPropertyName = "ValorFecha";
            this.FValorFecha.HeaderText = "ValorFecha";
            this.FValorFecha.Name = "FValorFecha";
            this.FValorFecha.ReadOnly = true;
            this.FValorFecha.Visible = false;
            // 
            // FId
            // 
            this.FId.DataPropertyName = "Id";
            this.FId.HeaderText = "ID";
            this.FId.Name = "FId";
            this.FId.ReadOnly = true;
            this.FId.Visible = false;
            // 
            // FrmFactorPotencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 409);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmFactorPotencia";
            this.Text = "Detalle de Consumos";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgFactorPotencia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dtgFactorPotencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn FPuntoInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn FPuntoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn FPuntoLongitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn FConcepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn FDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn FValorDecimal;
        private System.Windows.Forms.DataGridViewTextBoxColumn FValorCadena;
        private System.Windows.Forms.DataGridViewTextBoxColumn FValorEntero;
        private System.Windows.Forms.DataGridViewTextBoxColumn FValorFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn FId;
    }
}