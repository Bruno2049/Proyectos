namespace eClockSync5
{
    partial class Usc_Serial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usc_Serial));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Tbx_Llave = new System.Windows.Forms.MaskedTextBox();
            this.Lbl_IDUnico = new System.Windows.Forms.Label();
            this.Lbl_LlaveNValida = new System.Windows.Forms.Label();
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
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Tbx_Llave
            // 
            resources.ApplyResources(this.Tbx_Llave, "Tbx_Llave");
            this.Tbx_Llave.Name = "Tbx_Llave";
            this.Tbx_Llave.TextChanged += new System.EventHandler(this.Tbx_Llave_TextChanged);
            // 
            // Lbl_IDUnico
            // 
            resources.ApplyResources(this.Lbl_IDUnico, "Lbl_IDUnico");
            this.Lbl_IDUnico.Name = "Lbl_IDUnico";
            // 
            // Lbl_LlaveNValida
            // 
            resources.ApplyResources(this.Lbl_LlaveNValida, "Lbl_LlaveNValida");
            this.Lbl_LlaveNValida.ForeColor = System.Drawing.Color.Red;
            this.Lbl_LlaveNValida.Name = "Lbl_LlaveNValida";
            // 
            // Usc_Serial
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Lbl_LlaveNValida);
            this.Controls.Add(this.Lbl_IDUnico);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Tbx_Llave);
            this.Controls.Add(this.label1);
            this.Name = "Usc_Serial";
            this.Load += new System.EventHandler(this.Usc_Serial_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.Tbx_Llave, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m_headerPanel, 0);
            this.Controls.SetChildIndex(this.m_headerSeparator, 0);
            this.Controls.SetChildIndex(this.m_titleLabel, 0);
            this.Controls.SetChildIndex(this.m_subtitleLabel, 0);
            this.Controls.SetChildIndex(this.m_headerPicture, 0);
            this.Controls.SetChildIndex(this.Lbl_IDUnico, 0);
            this.Controls.SetChildIndex(this.Lbl_LlaveNValida, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_headerPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox Tbx_Llave;
        private System.Windows.Forms.Label Lbl_IDUnico;
        private System.Windows.Forms.Label Lbl_LlaveNValida;
    }
}
