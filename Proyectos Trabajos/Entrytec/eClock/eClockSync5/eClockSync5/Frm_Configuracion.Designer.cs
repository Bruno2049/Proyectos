namespace eClockSync5
{
    partial class Frm_Configuracion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Configuracion));
            this.Btn_UsuarioClave = new System.Windows.Forms.Button();
            this.Btn_Sitios = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_UsuarioClave
            // 
            resources.ApplyResources(this.Btn_UsuarioClave, "Btn_UsuarioClave");
            this.Btn_UsuarioClave.Name = "Btn_UsuarioClave";
            this.Btn_UsuarioClave.UseVisualStyleBackColor = true;
            this.Btn_UsuarioClave.Click += new System.EventHandler(this.Btn_UsuarioClave_Click);
            // 
            // Btn_Sitios
            // 
            resources.ApplyResources(this.Btn_Sitios, "Btn_Sitios");
            this.Btn_Sitios.Name = "Btn_Sitios";
            this.Btn_Sitios.UseVisualStyleBackColor = true;
            this.Btn_Sitios.Click += new System.EventHandler(this.Btn_Sitios_Click);
            // 
            // Frm_Configuracion
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn_Sitios);
            this.Controls.Add(this.Btn_UsuarioClave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Configuracion";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_UsuarioClave;
        private System.Windows.Forms.Button Btn_Sitios;
    }
}