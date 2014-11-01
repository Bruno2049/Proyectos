namespace TesterProyecto
{
    partial class FrmParseo
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
            this.btnTarifa = new System.Windows.Forms.Button();
            this.btnParseo = new System.Windows.Forms.Button();
            this.cboTarifa = new System.Windows.Forms.ComboBox();
            this.txtTrama = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnFactorPotencia = new System.Windows.Forms.Button();
            this.btnPeriodoMesAnio = new System.Windows.Forms.Button();
            this.btnDetConsDem = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnVerConsumos = new System.Windows.Forms.Button();
            this.dgtInfoGeneral = new System.Windows.Forms.DataGridView();
            this.HId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PuntoInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PuntoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PuntoLongitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorEntero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorDecimal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorCadena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtDemanMax = new System.Windows.Forms.TextBox();
            this.txtFecMaxDemanda = new System.Windows.Forms.TextBox();
            this.txtFecMinDeman = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtPromConsumo = new System.Windows.Forms.TextBox();
            this.txtFecMaxConsumo = new System.Windows.Forms.TextBox();
            this.txtFecMinConsumo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtgHistDetalleConsumo = new System.Windows.Forms.DataGridView();
            this.IdHistorial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Consumo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Demanda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactorPotencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgtInfoGeneral)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistDetalleConsumo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnTarifa);
            this.groupBox1.Controls.Add(this.btnParseo);
            this.groupBox1.Controls.Add(this.cboTarifa);
            this.groupBox1.Controls.Add(this.txtTrama);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1067, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Captura";
            // 
            // btnTarifa
            // 
            this.btnTarifa.Location = new System.Drawing.Point(743, 44);
            this.btnTarifa.Name = "btnTarifa";
            this.btnTarifa.Size = new System.Drawing.Size(168, 23);
            this.btnTarifa.TabIndex = 4;
            this.btnTarifa.Text = "Ver Resultado de la Tarifa";
            this.btnTarifa.UseVisualStyleBackColor = true;
            this.btnTarifa.Click += new System.EventHandler(this.btnTarifa_Click);
            // 
            // btnParseo
            // 
            this.btnParseo.Location = new System.Drawing.Point(743, 9);
            this.btnParseo.Name = "btnParseo";
            this.btnParseo.Size = new System.Drawing.Size(168, 23);
            this.btnParseo.TabIndex = 3;
            this.btnParseo.Text = "Parseo";
            this.btnParseo.UseVisualStyleBackColor = true;
            this.btnParseo.Click += new System.EventHandler(this.btnParseo_Click);
            // 
            // cboTarifa
            // 
            this.cboTarifa.FormattingEnabled = true;
            this.cboTarifa.Items.AddRange(new object[] {
            "T02",
            "T03",
            "THM",
            "TOM"});
            this.cboTarifa.Location = new System.Drawing.Point(611, 11);
            this.cboTarifa.Name = "cboTarifa";
            this.cboTarifa.Size = new System.Drawing.Size(115, 21);
            this.cboTarifa.TabIndex = 2;
            this.cboTarifa.Text = "Seleccione Tarifa";
            // 
            // txtTrama
            // 
            this.txtTrama.Location = new System.Drawing.Point(63, 10);
            this.txtTrama.Multiline = true;
            this.txtTrama.Name = "txtTrama";
            this.txtTrama.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTrama.Size = new System.Drawing.Size(525, 52);
            this.txtTrama.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trama:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(13, 102);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(1067, 414);
            this.splitContainer1.SplitterDistance = 633;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.dgtInfoGeneral);
            this.groupBox2.Location = new System.Drawing.Point(6, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(607, 387);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informacion General";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.btnFactorPotencia);
            this.groupBox7.Controls.Add(this.btnPeriodoMesAnio);
            this.groupBox7.Controls.Add(this.btnDetConsDem);
            this.groupBox7.Location = new System.Drawing.Point(15, 291);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(577, 78);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Detalle de Consumos";
            // 
            // btnFactorPotencia
            // 
            this.btnFactorPotencia.Location = new System.Drawing.Point(323, 19);
            this.btnFactorPotencia.Name = "btnFactorPotencia";
            this.btnFactorPotencia.Size = new System.Drawing.Size(136, 23);
            this.btnFactorPotencia.TabIndex = 5;
            this.btnFactorPotencia.Text = "Factor de Potencia";
            this.btnFactorPotencia.UseVisualStyleBackColor = true;
            this.btnFactorPotencia.Click += new System.EventHandler(this.btnFactorPotencia_Click);
            // 
            // btnPeriodoMesAnio
            // 
            this.btnPeriodoMesAnio.Location = new System.Drawing.Point(15, 19);
            this.btnPeriodoMesAnio.Name = "btnPeriodoMesAnio";
            this.btnPeriodoMesAnio.Size = new System.Drawing.Size(118, 23);
            this.btnPeriodoMesAnio.TabIndex = 4;
            this.btnPeriodoMesAnio.Text = "Periodo Mes y Año";
            this.btnPeriodoMesAnio.UseVisualStyleBackColor = true;
            this.btnPeriodoMesAnio.Click += new System.EventHandler(this.btnPeriodoMesAnio_Click);
            // 
            // btnDetConsDem
            // 
            this.btnDetConsDem.Location = new System.Drawing.Point(157, 19);
            this.btnDetConsDem.Name = "btnDetConsDem";
            this.btnDetConsDem.Size = new System.Drawing.Size(139, 23);
            this.btnDetConsDem.TabIndex = 3;
            this.btnDetConsDem.Text = "Consumos y Demanda";
            this.btnDetConsDem.UseVisualStyleBackColor = true;
            this.btnDetConsDem.Click += new System.EventHandler(this.btnDetConsDem_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnVerConsumos);
            this.groupBox4.Location = new System.Drawing.Point(18, 199);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(574, 69);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Consumos";
            // 
            // btnVerConsumos
            // 
            this.btnVerConsumos.Location = new System.Drawing.Point(15, 26);
            this.btnVerConsumos.Name = "btnVerConsumos";
            this.btnVerConsumos.Size = new System.Drawing.Size(118, 23);
            this.btnVerConsumos.TabIndex = 1;
            this.btnVerConsumos.Text = "Consumos";
            this.btnVerConsumos.UseVisualStyleBackColor = true;
            this.btnVerConsumos.Click += new System.EventHandler(this.btnVerConsumos_Click);
            // 
            // dgtInfoGeneral
            // 
            this.dgtInfoGeneral.AllowUserToAddRows = false;
            this.dgtInfoGeneral.AllowUserToDeleteRows = false;
            this.dgtInfoGeneral.AllowUserToOrderColumns = true;
            this.dgtInfoGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgtInfoGeneral.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgtInfoGeneral.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HId,
            this.PuntoInicial,
            this.PuntoFinal,
            this.PuntoLongitud,
            this.Concepto,
            this.Dato,
            this.ValorFecha,
            this.ValorEntero,
            this.ValorDecimal,
            this.ValorCadena});
            this.dgtInfoGeneral.Location = new System.Drawing.Point(18, -11);
            this.dgtInfoGeneral.Name = "dgtInfoGeneral";
            this.dgtInfoGeneral.ReadOnly = true;
            this.dgtInfoGeneral.Size = new System.Drawing.Size(574, 169);
            this.dgtInfoGeneral.TabIndex = 0;
            // 
            // HId
            // 
            this.HId.DataPropertyName = "Id";
            this.HId.HeaderText = "ID";
            this.HId.Name = "HId";
            this.HId.ReadOnly = true;
            this.HId.Visible = false;
            // 
            // PuntoInicial
            // 
            this.PuntoInicial.DataPropertyName = "PuntoInicial";
            this.PuntoInicial.HeaderText = "INICIO";
            this.PuntoInicial.Name = "PuntoInicial";
            this.PuntoInicial.ReadOnly = true;
            // 
            // PuntoFinal
            // 
            this.PuntoFinal.DataPropertyName = "PuntoFinal";
            this.PuntoFinal.HeaderText = "FIN";
            this.PuntoFinal.Name = "PuntoFinal";
            this.PuntoFinal.ReadOnly = true;
            // 
            // PuntoLongitud
            // 
            this.PuntoLongitud.DataPropertyName = "PuntoLongitud";
            this.PuntoLongitud.HeaderText = "Longitud";
            this.PuntoLongitud.Name = "PuntoLongitud";
            this.PuntoLongitud.ReadOnly = true;
            // 
            // Concepto
            // 
            this.Concepto.DataPropertyName = "Concepto";
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.Name = "Concepto";
            this.Concepto.ReadOnly = true;
            // 
            // Dato
            // 
            this.Dato.DataPropertyName = "Dato";
            this.Dato.HeaderText = "DATO";
            this.Dato.Name = "Dato";
            this.Dato.ReadOnly = true;
            // 
            // ValorFecha
            // 
            this.ValorFecha.DataPropertyName = "ValorFecha";
            this.ValorFecha.HeaderText = "VALOR (FECHA)";
            this.ValorFecha.Name = "ValorFecha";
            this.ValorFecha.ReadOnly = true;
            // 
            // ValorEntero
            // 
            this.ValorEntero.DataPropertyName = "ValorEntero";
            this.ValorEntero.HeaderText = "VALOR(NUMERICO)";
            this.ValorEntero.Name = "ValorEntero";
            this.ValorEntero.ReadOnly = true;
            this.ValorEntero.Visible = false;
            // 
            // ValorDecimal
            // 
            this.ValorDecimal.DataPropertyName = "ValorDecimal";
            this.ValorDecimal.HeaderText = "VALOR(DECIMAL)";
            this.ValorDecimal.Name = "ValorDecimal";
            this.ValorDecimal.ReadOnly = true;
            // 
            // ValorCadena
            // 
            this.ValorCadena.DataPropertyName = "ValorCadena";
            this.ValorCadena.HeaderText = "VALOR(CADENA)";
            this.ValorCadena.Name = "ValorCadena";
            this.ValorCadena.ReadOnly = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.txtDemanMax);
            this.groupBox6.Controls.Add(this.txtFecMaxDemanda);
            this.groupBox6.Controls.Add(this.txtFecMinDeman);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Location = new System.Drawing.Point(13, 290);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(398, 91);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Informacion de Demada";
            // 
            // txtDemanMax
            // 
            this.txtDemanMax.Location = new System.Drawing.Point(274, 42);
            this.txtDemanMax.Name = "txtDemanMax";
            this.txtDemanMax.ReadOnly = true;
            this.txtDemanMax.Size = new System.Drawing.Size(100, 20);
            this.txtDemanMax.TabIndex = 11;
            // 
            // txtFecMaxDemanda
            // 
            this.txtFecMaxDemanda.Location = new System.Drawing.Point(138, 43);
            this.txtFecMaxDemanda.Name = "txtFecMaxDemanda";
            this.txtFecMaxDemanda.ReadOnly = true;
            this.txtFecMaxDemanda.Size = new System.Drawing.Size(115, 20);
            this.txtFecMaxDemanda.TabIndex = 10;
            // 
            // txtFecMinDeman
            // 
            this.txtFecMinDeman.Location = new System.Drawing.Point(12, 44);
            this.txtFecMinDeman.Name = "txtFecMinDeman";
            this.txtFecMinDeman.ReadOnly = true;
            this.txtFecMinDeman.Size = new System.Drawing.Size(100, 20);
            this.txtFecMinDeman.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(293, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Demanda Maxima";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Fecha Maxima";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Fecha Minima";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.txtPromConsumo);
            this.groupBox5.Controls.Add(this.txtFecMaxConsumo);
            this.groupBox5.Controls.Add(this.txtFecMinConsumo);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(13, 171);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(398, 106);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Informacion de Consumo";
            // 
            // txtPromConsumo
            // 
            this.txtPromConsumo.Location = new System.Drawing.Point(270, 45);
            this.txtPromConsumo.Name = "txtPromConsumo";
            this.txtPromConsumo.ReadOnly = true;
            this.txtPromConsumo.Size = new System.Drawing.Size(100, 20);
            this.txtPromConsumo.TabIndex = 5;
            // 
            // txtFecMaxConsumo
            // 
            this.txtFecMaxConsumo.Location = new System.Drawing.Point(138, 44);
            this.txtFecMaxConsumo.Name = "txtFecMaxConsumo";
            this.txtFecMaxConsumo.ReadOnly = true;
            this.txtFecMaxConsumo.Size = new System.Drawing.Size(115, 20);
            this.txtFecMaxConsumo.TabIndex = 4;
            // 
            // txtFecMinConsumo
            // 
            this.txtFecMinConsumo.Location = new System.Drawing.Point(9, 45);
            this.txtFecMinConsumo.Name = "txtFecMinConsumo";
            this.txtFecMinConsumo.ReadOnly = true;
            this.txtFecMinConsumo.Size = new System.Drawing.Size(100, 20);
            this.txtFecMinConsumo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Promedio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Fecha Maxima";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Fecha Minima";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dtgHistDetalleConsumo);
            this.groupBox3.Location = new System.Drawing.Point(13, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(398, 147);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Historial  de Detalle de Consumos";
            // 
            // dtgHistDetalleConsumo
            // 
            this.dtgHistDetalleConsumo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgHistDetalleConsumo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgHistDetalleConsumo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdHistorial,
            this.Fecha,
            this.Consumo,
            this.Demanda,
            this.FactorPotencia,
            this.Id});
            this.dtgHistDetalleConsumo.Location = new System.Drawing.Point(8, 31);
            this.dtgHistDetalleConsumo.Name = "dtgHistDetalleConsumo";
            this.dtgHistDetalleConsumo.Size = new System.Drawing.Size(378, 103);
            this.dtgHistDetalleConsumo.TabIndex = 0;
            // 
            // IdHistorial
            // 
            this.IdHistorial.DataPropertyName = "IdHistorial";
            this.IdHistorial.HeaderText = "ID Historial";
            this.IdHistorial.Name = "IdHistorial";
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "FECHA";
            this.Fecha.HeaderText = "FECHA";
            this.Fecha.Name = "Fecha";
            // 
            // Consumo
            // 
            this.Consumo.DataPropertyName = "Consumo";
            this.Consumo.HeaderText = "CONSUMO";
            this.Consumo.Name = "Consumo";
            this.Consumo.ReadOnly = true;
            // 
            // Demanda
            // 
            this.Demanda.DataPropertyName = "Demanda";
            this.Demanda.HeaderText = "DEMANDA";
            this.Demanda.Name = "Demanda";
            this.Demanda.ReadOnly = true;
            // 
            // FactorPotencia
            // 
            this.FactorPotencia.DataPropertyName = "FactorPotencia";
            this.FactorPotencia.HeaderText = "FactorPotencia";
            this.FactorPotencia.Name = "FactorPotencia";
            this.FactorPotencia.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // FrmParseo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 528);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmParseo";
            this.Text = "FrmParseo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgtInfoGeneral)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistDetalleConsumo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboTarifa;
        private System.Windows.Forms.TextBox txtTrama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgtInfoGeneral;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dtgHistDetalleConsumo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdHistorial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Consumo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Demanda;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactorPotencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnVerConsumos;
        private System.Windows.Forms.Button btnPeriodoMesAnio;
        private System.Windows.Forms.Button btnDetConsDem;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnParseo;
        private System.Windows.Forms.DataGridViewTextBoxColumn HId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuntoInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuntoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuntoLongitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dato;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorEntero;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorDecimal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorCadena;
        private System.Windows.Forms.Button btnFactorPotencia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDemanMax;
        private System.Windows.Forms.TextBox txtFecMaxDemanda;
        private System.Windows.Forms.TextBox txtFecMinDeman;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPromConsumo;
        private System.Windows.Forms.TextBox txtFecMaxConsumo;
        private System.Windows.Forms.TextBox txtFecMinConsumo;
        private System.Windows.Forms.Button btnTarifa;
    }
}