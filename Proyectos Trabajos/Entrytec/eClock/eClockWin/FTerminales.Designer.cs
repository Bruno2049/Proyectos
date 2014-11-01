namespace eClockWin
{
    partial class FTerminales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTerminales));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LblBuscando = new System.Windows.Forms.Label();
            this.LboTerminales = new System.Windows.Forms.ListBox();
            this.networkConfigButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.TBioEntry = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LblBuscando);
            this.groupBox1.Controls.Add(this.LboTerminales);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 257);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Terminales Encontradas";
            // 
            // LblBuscando
            // 
            this.LblBuscando.AutoSize = true;
            this.LblBuscando.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBuscando.ForeColor = System.Drawing.Color.DarkRed;
            this.LblBuscando.Location = new System.Drawing.Point(99, 115);
            this.LblBuscando.Name = "LblBuscando";
            this.LblBuscando.Size = new System.Drawing.Size(83, 17);
            this.LblBuscando.TabIndex = 1;
            this.LblBuscando.Text = "Buscando...";
            // 
            // LboTerminales
            // 
            this.LboTerminales.FormattingEnabled = true;
            this.LboTerminales.Location = new System.Drawing.Point(9, 21);
            this.LboTerminales.Name = "LboTerminales";
            this.LboTerminales.Size = new System.Drawing.Size(281, 225);
            this.LboTerminales.TabIndex = 0;
            // 
            // networkConfigButton
            // 
            this.networkConfigButton.Image = ((System.Drawing.Image)(resources.GetObject("networkConfigButton.Image")));
            this.networkConfigButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.networkConfigButton.Location = new System.Drawing.Point(314, 235);
            this.networkConfigButton.Name = "networkConfigButton";
            this.networkConfigButton.Size = new System.Drawing.Size(148, 25);
            this.networkConfigButton.TabIndex = 2;
            this.networkConfigButton.Text = "&Configurar Terminal";
            this.networkConfigButton.UseVisualStyleBackColor = true;
            this.networkConfigButton.Click += new System.EventHandler(this.networkConfigButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::eClockWin.Properties.Resources.imgencabezado;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(454, 50);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(314, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "&Cerrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(314, 297);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 25);
            this.button2.TabIndex = 5;
            this.button2.Text = "Guardar en IsTime";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // TBioEntry
            // 
            this.TBioEntry.Enabled = true;
            this.TBioEntry.Interval = 1000;
            this.TBioEntry.Tick += new System.EventHandler(this.TBioEntry_Tick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(314, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 94);
            this.label1.TabIndex = 6;
            this.label1.Text = "    Este modulo se encarga de buscar a los dispositivos BioEntry Plus conectados " +
                "en la misma subred y si usamos el boton \"Guardar en IsTime\" se actualizarán en e" +
                "l IsTime.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(308, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 62);
            this.label2.TabIndex = 7;
            this.label2.Text = "    *NOTA: Se recomienda asignarle una dirección IP Fija a las terminales y a la " +
                "PC-Servidor ";
            // 
            // FTerminales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(474, 337);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.networkConfigButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FTerminales";
            this.Text = "Busqueda de Terminales";
            this.Load += new System.EventHandler(this.FTerminales_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button networkConfigButton;
        private System.Windows.Forms.ListBox LboTerminales;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer TBioEntry;
        private System.Windows.Forms.Label LblBuscando;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

