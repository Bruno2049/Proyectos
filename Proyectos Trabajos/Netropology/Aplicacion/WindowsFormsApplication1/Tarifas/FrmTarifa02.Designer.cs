namespace WindowsFormsApplication1.Tarifas
{
    partial class FrmTarifa02
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMontFactMnSnIva = new System.Windows.Forms.TextBox();
            this.txtMontoMaxFact = new System.Windows.Forms.TextBox();
            this.txtPagFacturacion = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgT02 = new System.Windows.Forms.DataGridView();
            this.IdConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPromedioODemax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CargoAdicional = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Facturacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactorPotencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgT02)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.dtgT02);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(919, 439);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resultado";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtMontFactMnSnIva);
            this.groupBox2.Controls.Add(this.txtMontoMaxFact);
            this.groupBox2.Controls.Add(this.txtPagFacturacion);
            this.groupBox2.Controls.Add(this.txtTotal);
            this.groupBox2.Controls.Add(this.txtIva);
            this.groupBox2.Controls.Add(this.txtSubTotal);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(534, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 193);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resumen";
            // 
            // txtMontFactMnSnIva
            // 
            this.txtMontFactMnSnIva.Location = new System.Drawing.Point(183, 156);
            this.txtMontFactMnSnIva.Name = "txtMontFactMnSnIva";
            this.txtMontFactMnSnIva.Size = new System.Drawing.Size(175, 20);
            this.txtMontFactMnSnIva.TabIndex = 11;
            // 
            // txtMontoMaxFact
            // 
            this.txtMontoMaxFact.Location = new System.Drawing.Point(143, 129);
            this.txtMontoMaxFact.Name = "txtMontoMaxFact";
            this.txtMontoMaxFact.Size = new System.Drawing.Size(183, 20);
            this.txtMontoMaxFact.TabIndex = 10;
            // 
            // txtPagFacturacion
            // 
            this.txtPagFacturacion.Location = new System.Drawing.Point(201, 102);
            this.txtPagFacturacion.Name = "txtPagFacturacion";
            this.txtPagFacturacion.Size = new System.Drawing.Size(158, 20);
            this.txtPagFacturacion.TabIndex = 9;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(69, 75);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(186, 20);
            this.txtTotal.TabIndex = 8;
            // 
            // txtIva
            // 
            this.txtIva.Location = new System.Drawing.Point(69, 48);
            this.txtIva.Name = "txtIva";
            this.txtIva.Size = new System.Drawing.Size(186, 20);
            this.txtIva.TabIndex = 7;
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.Location = new System.Drawing.Point(69, 21);
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.Size = new System.Drawing.Size(186, 20);
            this.txtSubTotal.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(170, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Monto a Facturar Mensual Sin Iva:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Monto Maximo a Facturar:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Pago Facturacion(Bimestral o Mensual):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Iva:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SubTotal:";
            // 
            // dtgT02
            // 
            this.dtgT02.AllowUserToAddRows = false;
            this.dtgT02.AllowUserToDeleteRows = false;
            this.dtgT02.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgT02.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgT02.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdConcepto,
            this.Concepto,
            this.CPromedioODemax,
            this.CargoAdicional,
            this.Facturacion,
            this.FactorPotencia});
            this.dtgT02.Location = new System.Drawing.Point(18, 36);
            this.dtgT02.Name = "dtgT02";
            this.dtgT02.ReadOnly = true;
            this.dtgT02.Size = new System.Drawing.Size(880, 170);
            this.dtgT02.TabIndex = 0;
            // 
            // IdConcepto
            // 
            this.IdConcepto.DataPropertyName = "IdConcepto";
            this.IdConcepto.HeaderText = "IdConcepto";
            this.IdConcepto.Name = "IdConcepto";
            this.IdConcepto.ReadOnly = true;
            this.IdConcepto.Visible = false;
            // 
            // Concepto
            // 
            this.Concepto.DataPropertyName = "Concepto";
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.Name = "Concepto";
            this.Concepto.ReadOnly = true;
            // 
            // CPromedioODemax
            // 
            this.CPromedioODemax.DataPropertyName = "CPromedioODemMax";
            this.CPromedioODemax.HeaderText = "Consumo";
            this.CPromedioODemax.Name = "CPromedioODemax";
            this.CPromedioODemax.ReadOnly = true;
            // 
            // CargoAdicional
            // 
            this.CargoAdicional.DataPropertyName = "CargoAdicional";
            this.CargoAdicional.HeaderText = "Costo Mesual";
            this.CargoAdicional.Name = "CargoAdicional";
            this.CargoAdicional.ReadOnly = true;
            // 
            // Facturacion
            // 
            this.Facturacion.DataPropertyName = "Facturacion";
            this.Facturacion.HeaderText = "Facturacion";
            this.Facturacion.Name = "Facturacion";
            this.Facturacion.ReadOnly = true;
            // 
            // FactorPotencia
            // 
            this.FactorPotencia.DataPropertyName = "FactorPotencia";
            this.FactorPotencia.HeaderText = "FactorPotencia";
            this.FactorPotencia.Name = "FactorPotencia";
            this.FactorPotencia.ReadOnly = true;
            this.FactorPotencia.Visible = false;
            // 
            // FrmTarifa02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 481);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmTarifa02";
            this.Text = "FrmTarifa02";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgT02)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMontFactMnSnIva;
        private System.Windows.Forms.TextBox txtMontoMaxFact;
        private System.Windows.Forms.TextBox txtPagFacturacion;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtIva;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgT02;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdConcepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPromedioODemax;
        private System.Windows.Forms.DataGridViewTextBoxColumn CargoAdicional;
        private System.Windows.Forms.DataGridViewTextBoxColumn Facturacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactorPotencia;
    }
}