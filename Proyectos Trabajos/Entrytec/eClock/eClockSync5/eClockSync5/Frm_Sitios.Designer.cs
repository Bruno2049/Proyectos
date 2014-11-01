namespace eClockSync5
{
    partial class Frm_Sitios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Sitios));
            this.CbxLst_Sitios = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Lnk_Sitios = new System.Windows.Forms.LinkLabel();
            this.Btn_Aceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CbxLst_Sitios
            // 
            resources.ApplyResources(this.CbxLst_Sitios, "CbxLst_Sitios");
            this.CbxLst_Sitios.FormattingEnabled = true;
            this.CbxLst_Sitios.Name = "CbxLst_Sitios";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Lnk_Sitios
            // 
            resources.ApplyResources(this.Lnk_Sitios, "Lnk_Sitios");
            this.Lnk_Sitios.Name = "Lnk_Sitios";
            this.Lnk_Sitios.TabStop = true;
            this.Lnk_Sitios.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lnk_Sitios_LinkClicked);
            // 
            // Btn_Aceptar
            // 
            resources.ApplyResources(this.Btn_Aceptar, "Btn_Aceptar");
            this.Btn_Aceptar.Name = "Btn_Aceptar";
            this.Btn_Aceptar.UseVisualStyleBackColor = true;
            this.Btn_Aceptar.Click += new System.EventHandler(this.Btn_Aceptar_Click);
            // 
            // Frm_Sitios
            // 
            this.AcceptButton = this.Btn_Aceptar;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn_Aceptar);
            this.Controls.Add(this.Lnk_Sitios);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CbxLst_Sitios);
            this.Name = "Frm_Sitios";
            this.Load += new System.EventHandler(this.Frm_Sitios_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CbxLst_Sitios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel Lnk_Sitios;
        private System.Windows.Forms.Button Btn_Aceptar;
    }
}