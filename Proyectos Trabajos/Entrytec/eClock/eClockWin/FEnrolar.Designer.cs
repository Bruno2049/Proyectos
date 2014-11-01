namespace eClockWin
{
    partial class FEnrolar
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FEnrolar));
            this.CmbTerminales = new System.Windows.Forms.ComboBox();
            this.iTWTERMINALESBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grb_Huella1 = new System.Windows.Forms.GroupBox();
            this.BtnLeerH1 = new System.Windows.Forms.Button();
            this.BtnBorrarH1 = new System.Windows.Forms.Button();
            this.LblHuella1 = new System.Windows.Forms.Label();
            this.grb_Huella2 = new System.Windows.Forms.GroupBox();
            this.BtnLeerH2 = new System.Windows.Forms.Button();
            this.BtnBorrarH2 = new System.Windows.Forms.Button();
            this.LblHuella2 = new System.Windows.Forms.Label();
            this.BtnCerrar = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BtnLeerT = new System.Windows.Forms.Button();
            this.BtnBorrarT = new System.Windows.Forms.Button();
            this.LblTarjeta = new System.Windows.Forms.Label();
            this.LblPersona = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iTWTERMINALESBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grb_Huella1.SuspendLayout();
            this.grb_Huella2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmbTerminales
            // 
            this.CmbTerminales.DataSource = this.iTWTERMINALESBindingSource;
            this.CmbTerminales.DisplayMember = "TERMINAL_NOMBRE";
            this.CmbTerminales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTerminales.DropDownWidth = 300;
            this.CmbTerminales.FormattingEnabled = true;
            this.CmbTerminales.Location = new System.Drawing.Point(228, 19);
            this.CmbTerminales.Name = "CmbTerminales";
            this.CmbTerminales.Size = new System.Drawing.Size(121, 21);
            this.CmbTerminales.TabIndex = 0;
            this.CmbTerminales.ValueMember = "TERMINAL_ID";
            this.CmbTerminales.SelectedIndexChanged += new System.EventHandler(this.CmbTerminales_SelectedIndexChanged);
            // 
            // iTWTERMINALESBindingSource
            // 
            this.iTWTERMINALESBindingSource.AllowNew = false;
            this.iTWTERMINALESBindingSource.DataMember = "ITW_TERMINALES";
            this.iTWTERMINALESBindingSource.DataSource = typeof(eClockWin.WSChecador.DS_WSPersonasTerminales);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Elija la terminal desde donde desea enrolar";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmbTerminales);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 58);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Terminal de enrolamiento";
            // 
            // grb_Huella1
            // 
            this.grb_Huella1.Controls.Add(this.BtnLeerH1);
            this.grb_Huella1.Controls.Add(this.BtnBorrarH1);
            this.grb_Huella1.Controls.Add(this.LblHuella1);
            this.grb_Huella1.Location = new System.Drawing.Point(33, 103);
            this.grb_Huella1.Name = "grb_Huella1";
            this.grb_Huella1.Size = new System.Drawing.Size(159, 88);
            this.grb_Huella1.TabIndex = 3;
            this.grb_Huella1.TabStop = false;
            this.grb_Huella1.Text = "Huella 1";
            // 
            // BtnLeerH1
            // 
            this.BtnLeerH1.Image = global::eClockWin.Properties.Resources.stock_autoformat_16;
            this.BtnLeerH1.Location = new System.Drawing.Point(44, 54);
            this.BtnLeerH1.Name = "BtnLeerH1";
            this.BtnLeerH1.Size = new System.Drawing.Size(109, 28);
            this.BtnLeerH1.TabIndex = 5;
            this.BtnLeerH1.Text = "Leer Huella";
            this.BtnLeerH1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLeerH1.UseVisualStyleBackColor = true;
            this.BtnLeerH1.Click += new System.EventHandler(this.BtnLeerH1_Click);
            // 
            // BtnBorrarH1
            // 
            this.BtnBorrarH1.Image = global::eClockWin.Properties.Resources.stock_delete_16;
            this.BtnBorrarH1.Location = new System.Drawing.Point(7, 54);
            this.BtnBorrarH1.Name = "BtnBorrarH1";
            this.BtnBorrarH1.Size = new System.Drawing.Size(31, 28);
            this.BtnBorrarH1.TabIndex = 4;
            this.BtnBorrarH1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnBorrarH1.UseVisualStyleBackColor = true;
            this.BtnBorrarH1.Click += new System.EventHandler(this.BtnBorrarH1_Click);
            // 
            // LblHuella1
            // 
            this.LblHuella1.AutoSize = true;
            this.LblHuella1.Location = new System.Drawing.Point(36, 27);
            this.LblHuella1.Name = "LblHuella1";
            this.LblHuella1.Size = new System.Drawing.Size(88, 13);
            this.LblHuella1.TabIndex = 0;
            this.LblHuella1.Text = "Huella capturada";
            // 
            // grb_Huella2
            // 
            this.grb_Huella2.Controls.Add(this.BtnLeerH2);
            this.grb_Huella2.Controls.Add(this.BtnBorrarH2);
            this.grb_Huella2.Controls.Add(this.LblHuella2);
            this.grb_Huella2.Location = new System.Drawing.Point(198, 103);
            this.grb_Huella2.Name = "grb_Huella2";
            this.grb_Huella2.Size = new System.Drawing.Size(159, 88);
            this.grb_Huella2.TabIndex = 6;
            this.grb_Huella2.TabStop = false;
            this.grb_Huella2.Text = "Huella 2";
            // 
            // BtnLeerH2
            // 
            this.BtnLeerH2.Image = global::eClockWin.Properties.Resources.stock_autoformat_16;
            this.BtnLeerH2.Location = new System.Drawing.Point(44, 54);
            this.BtnLeerH2.Name = "BtnLeerH2";
            this.BtnLeerH2.Size = new System.Drawing.Size(109, 28);
            this.BtnLeerH2.TabIndex = 5;
            this.BtnLeerH2.Text = "Leer Huella";
            this.BtnLeerH2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLeerH2.UseVisualStyleBackColor = true;
            this.BtnLeerH2.Click += new System.EventHandler(this.BtnLeerH2_Click);
            // 
            // BtnBorrarH2
            // 
            this.BtnBorrarH2.Image = global::eClockWin.Properties.Resources.stock_delete_16;
            this.BtnBorrarH2.Location = new System.Drawing.Point(7, 54);
            this.BtnBorrarH2.Name = "BtnBorrarH2";
            this.BtnBorrarH2.Size = new System.Drawing.Size(31, 28);
            this.BtnBorrarH2.TabIndex = 4;
            this.BtnBorrarH2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnBorrarH2.UseVisualStyleBackColor = true;
            this.BtnBorrarH2.Click += new System.EventHandler(this.BtnBorrarH2_Click);
            // 
            // LblHuella2
            // 
            this.LblHuella2.AutoSize = true;
            this.LblHuella2.Location = new System.Drawing.Point(36, 27);
            this.LblHuella2.Name = "LblHuella2";
            this.LblHuella2.Size = new System.Drawing.Size(88, 13);
            this.LblHuella2.TabIndex = 0;
            this.LblHuella2.Text = "Huella capturada";
            // 
            // BtnCerrar
            // 
            this.BtnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("BtnCerrar.Image")));
            this.BtnCerrar.Location = new System.Drawing.Point(152, 272);
            this.BtnCerrar.Name = "BtnCerrar";
            this.BtnCerrar.Size = new System.Drawing.Size(87, 28);
            this.BtnCerrar.TabIndex = 7;
            this.BtnCerrar.Text = "&Cerrar";
            this.BtnCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCerrar.UseVisualStyleBackColor = true;
            this.BtnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BtnLeerT);
            this.groupBox4.Controls.Add(this.BtnBorrarT);
            this.groupBox4.Controls.Add(this.LblTarjeta);
            this.groupBox4.Location = new System.Drawing.Point(33, 197);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(324, 69);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tarjeta";
            // 
            // BtnLeerT
            // 
            this.BtnLeerT.Image = global::eClockWin.Properties.Resources.stock_autoformat_16;
            this.BtnLeerT.Location = new System.Drawing.Point(209, 27);
            this.BtnLeerT.Name = "BtnLeerT";
            this.BtnLeerT.Size = new System.Drawing.Size(109, 28);
            this.BtnLeerT.TabIndex = 5;
            this.BtnLeerT.Text = "Leer Tarjeta";
            this.BtnLeerT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLeerT.UseVisualStyleBackColor = true;
            this.BtnLeerT.Click += new System.EventHandler(this.BtnLeerT_Click);
            // 
            // BtnBorrarT
            // 
            this.BtnBorrarT.Image = global::eClockWin.Properties.Resources.stock_delete_16;
            this.BtnBorrarT.Location = new System.Drawing.Point(172, 27);
            this.BtnBorrarT.Name = "BtnBorrarT";
            this.BtnBorrarT.Size = new System.Drawing.Size(31, 28);
            this.BtnBorrarT.TabIndex = 4;
            this.BtnBorrarT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnBorrarT.UseVisualStyleBackColor = true;
            this.BtnBorrarT.Click += new System.EventHandler(this.BtnBorrarT_Click);
            // 
            // LblTarjeta
            // 
            this.LblTarjeta.Location = new System.Drawing.Point(13, 27);
            this.LblTarjeta.Name = "LblTarjeta";
            this.LblTarjeta.Size = new System.Drawing.Size(146, 21);
            this.LblTarjeta.TabIndex = 0;
            this.LblTarjeta.Text = "..";
            // 
            // LblPersona
            // 
            this.LblPersona.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPersona.Location = new System.Drawing.Point(12, 13);
            this.LblPersona.Name = "LblPersona";
            this.LblPersona.Size = new System.Drawing.Size(366, 23);
            this.LblPersona.TabIndex = 10;
            this.LblPersona.Text = "Nombre de persona";
            // 
            // FEnrolar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 310);
            this.Controls.Add(this.LblPersona);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.BtnCerrar);
            this.Controls.Add(this.grb_Huella2);
            this.Controls.Add(this.grb_Huella1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FEnrolar";
            this.Text = "Enrolamiento";
            this.Load += new System.EventHandler(this.FEnrolar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iTWTERMINALESBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grb_Huella1.ResumeLayout(false);
            this.grb_Huella1.PerformLayout();
            this.grb_Huella2.ResumeLayout(false);
            this.grb_Huella2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbTerminales;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grb_Huella1;
        private System.Windows.Forms.Button BtnBorrarH1;
        private System.Windows.Forms.Label LblHuella1;
        private System.Windows.Forms.Button BtnLeerH1;
        private System.Windows.Forms.GroupBox grb_Huella2;
        private System.Windows.Forms.Button BtnLeerH2;
        private System.Windows.Forms.Button BtnBorrarH2;
        private System.Windows.Forms.Label LblHuella2;
        private System.Windows.Forms.Button BtnCerrar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button BtnLeerT;
        private System.Windows.Forms.Button BtnBorrarT;
        private System.Windows.Forms.Label LblTarjeta;
        private System.Windows.Forms.Label LblPersona;
        private System.Windows.Forms.BindingSource iTWTERMINALESBindingSource;
    }
}