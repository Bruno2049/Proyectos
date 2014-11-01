namespace eClockSync5
{
    partial class Usc_Bienvenida
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usc_Bienvenida));
            ((System.ComponentModel.ISupportInitialize)(this.m_watermarkPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // m_titleLabel
            // 
            resources.ApplyResources(this.m_titleLabel, "m_titleLabel");
            // 
            // m_DescriptionLabel
            // 
            resources.ApplyResources(this.m_DescriptionLabel, "m_DescriptionLabel");
            // 
            // Usc_Bienvenida
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Usc_Bienvenida";
            this.Load += new System.EventHandler(this.Usc_Bienvenida_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_watermarkPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
