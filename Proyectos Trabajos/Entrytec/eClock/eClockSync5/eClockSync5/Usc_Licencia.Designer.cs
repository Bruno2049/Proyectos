namespace eClockSync5
{
    partial class Usc_Licencia
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usc_Licencia));
            this.Tbx_Licencia = new System.Windows.Forms.RichTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_headerPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // m_titleLabel
            // 
            resources.ApplyResources(this.m_titleLabel, "m_titleLabel");
            // 
            // m_subtitleLabel
            // 
            resources.ApplyResources(this.m_subtitleLabel, "m_subtitleLabel");
            this.m_subtitleLabel.Click += new System.EventHandler(this.m_subtitleLabel_Click);
            // 
            // Tbx_Licencia
            // 
            resources.ApplyResources(this.Tbx_Licencia, "Tbx_Licencia");
            this.Tbx_Licencia.Name = "Tbx_Licencia";
            this.Tbx_Licencia.ReadOnly = true;
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Usc_Licencia
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Tbx_Licencia);
            this.Name = "Usc_Licencia";
            this.Load += new System.EventHandler(this.Usc_Licencia_Load);
            this.Controls.SetChildIndex(this.m_headerPanel, 0);
            this.Controls.SetChildIndex(this.m_headerSeparator, 0);
            this.Controls.SetChildIndex(this.m_titleLabel, 0);
            this.Controls.SetChildIndex(this.m_subtitleLabel, 0);
            this.Controls.SetChildIndex(this.m_headerPicture, 0);
            this.Controls.SetChildIndex(this.Tbx_Licencia, 0);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_headerPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Tbx_Licencia;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;

    }
}
