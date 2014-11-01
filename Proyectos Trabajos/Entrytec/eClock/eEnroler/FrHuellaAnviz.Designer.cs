namespace eEnroler
{
    partial class FrHuellaAnviz
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Pbx_Huella = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LblNoHuella = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.TActualiza = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Huella)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = global::eEnroler.Properties.Resources.Correcta;
            this.pictureBox2.Location = new System.Drawing.Point(6, 36);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(260, 281);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Location = new System.Drawing.Point(305, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 324);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ejemplo de como colocar la huella";
            // 
            // Pbx_Huella
            // 
            this.Pbx_Huella.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pbx_Huella.Location = new System.Drawing.Point(6, 48);
            this.Pbx_Huella.Name = "Pbx_Huella";
            this.Pbx_Huella.Size = new System.Drawing.Size(260, 281);
            this.Pbx_Huella.TabIndex = 4;
            this.Pbx_Huella.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(2, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Capture la huella 2 veces";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // LblNoHuella
            // 
            this.LblNoHuella.AutoSize = true;
            this.LblNoHuella.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblNoHuella.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNoHuella.ForeColor = System.Drawing.Color.Red;
            this.LblNoHuella.Location = new System.Drawing.Point(237, 14);
            this.LblNoHuella.Name = "LblNoHuella";
            this.LblNoHuella.Size = new System.Drawing.Size(29, 31);
            this.LblNoHuella.TabIndex = 6;
            this.LblNoHuella.Text = "1";
            this.LblNoHuella.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Pbx_Huella);
            this.groupBox2.Controls.Add(this.LblNoHuella);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 335);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enrolamiento";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(409, 353);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(52, 353);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(192, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Capturar Huella";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TActualiza
            // 
            this.TActualiza.Interval = 500;
            this.TActualiza.Tick += new System.EventHandler(this.TActualiza_Tick);
            // 
            // FrHuellaAnviz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 384);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrHuellaAnviz";
            this.Text = "Enrolamiento de huella eClock";
            this.Load += new System.EventHandler(this.FrHuellaAnviz_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrHuellaAnviz_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Huella)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox Pbx_Huella;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LblNoHuella;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer TActualiza;
    }
}