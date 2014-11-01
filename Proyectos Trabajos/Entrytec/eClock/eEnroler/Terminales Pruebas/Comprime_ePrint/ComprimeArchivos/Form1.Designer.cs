namespace ComprimeArchivos
{
    partial class Form1
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
            this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.SuspendLayout();
            // 
            // ultraButton1
            // 
            this.ultraButton1.Location = new System.Drawing.Point(197, 53);
            this.ultraButton1.Name = "ultraButton1";
            this.ultraButton1.Size = new System.Drawing.Size(75, 23);
            this.ultraButton1.TabIndex = 0;
            this.ultraButton1.Text = "Seleccionar";
            this.ultraButton1.Click += new System.EventHandler(this.ultraButton1_Click);
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Location = new System.Drawing.Point(21, 24);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(251, 23);
            this.ultraLabel1.TabIndex = 1;
            this.ultraLabel1.Text = "Selecciona el Archivo a Compirmir";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 105);
            this.Controls.Add(this.ultraLabel1);
            this.Controls.Add(this.ultraButton1);
            this.Name = "Form1";
            this.Text = "ePrintZip";
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraButton ultraButton1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
    }
}

