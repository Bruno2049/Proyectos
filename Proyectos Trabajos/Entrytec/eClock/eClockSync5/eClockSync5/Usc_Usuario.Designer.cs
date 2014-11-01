namespace eClockSync5
{
    partial class Usc_Usuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usc_Usuario));
            this.Chk_Recordar = new System.Windows.Forms.CheckBox();
            this.Tbx_Clave = new System.Windows.Forms.TextBox();
            this.Tbx_Usuario = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Btn_Iniciar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_headerPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // m_titleLabel
            // 
            resources.ApplyResources(this.m_titleLabel, "m_titleLabel");
            this.m_titleLabel.Click += new System.EventHandler(this.m_titleLabel_Click);
            // 
            // m_subtitleLabel
            // 
            resources.ApplyResources(this.m_subtitleLabel, "m_subtitleLabel");
            // 
            // Chk_Recordar
            // 
            resources.ApplyResources(this.Chk_Recordar, "Chk_Recordar");
            this.Chk_Recordar.Checked = true;
            this.Chk_Recordar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_Recordar.Name = "Chk_Recordar";
            this.Chk_Recordar.UseVisualStyleBackColor = true;
            // 
            // Tbx_Clave
            // 
            resources.ApplyResources(this.Tbx_Clave, "Tbx_Clave");
            this.Tbx_Clave.Name = "Tbx_Clave";
            this.Tbx_Clave.UseSystemPasswordChar = true;
            // 
            // Tbx_Usuario
            // 
            resources.ApplyResources(this.Tbx_Usuario, "Tbx_Usuario");
            this.Tbx_Usuario.Name = "Tbx_Usuario";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // Btn_Iniciar
            // 
            resources.ApplyResources(this.Btn_Iniciar, "Btn_Iniciar");
            this.Btn_Iniciar.Name = "Btn_Iniciar";
            this.Btn_Iniciar.UseVisualStyleBackColor = true;
            // 
            // Usc_Usuario
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Chk_Recordar);
            this.Controls.Add(this.Tbx_Clave);
            this.Controls.Add(this.Tbx_Usuario);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Btn_Iniciar);
            this.Name = "Usc_Usuario";
            this.Load += new System.EventHandler(this.Usc_Usuario_Load);
            this.Controls.SetChildIndex(this.m_headerPanel, 0);
            this.Controls.SetChildIndex(this.m_headerSeparator, 0);
            this.Controls.SetChildIndex(this.m_titleLabel, 0);
            this.Controls.SetChildIndex(this.m_subtitleLabel, 0);
            this.Controls.SetChildIndex(this.m_headerPicture, 0);
            this.Controls.SetChildIndex(this.Btn_Iniciar, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.Tbx_Usuario, 0);
            this.Controls.SetChildIndex(this.Tbx_Clave, 0);
            this.Controls.SetChildIndex(this.Chk_Recordar, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_headerPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox Chk_Recordar;
        private System.Windows.Forms.TextBox Tbx_Clave;
        private System.Windows.Forms.TextBox Tbx_Usuario;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Btn_Iniciar;
    }
}
