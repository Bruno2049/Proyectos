namespace eClockSync5
{
    partial class Frm_Asistente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Asistente));
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_backButton
            // 
            resources.ApplyResources(this.m_backButton, "m_backButton");
            // 
            // m_nextButton
            // 
            resources.ApplyResources(this.m_nextButton, "m_nextButton");
            this.m_nextButton.Click += new System.EventHandler(this.m_nextButton_Click);
            // 
            // m_cancelButton
            // 
            resources.ApplyResources(this.m_cancelButton, "m_cancelButton");
            // 
            // m_finishButton
            // 
            resources.ApplyResources(this.m_finishButton, "m_finishButton");
            // 
            // m_separator
            // 
            resources.ApplyResources(this.m_separator, "m_separator");
            // 
            // button4
            // 
            this.button4.Image = global::eClockSync5.Properties.Resources.Usa18;
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Image = global::eClockSync5.Properties.Resources.Mexico18;
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Image = global::eClockSync5.Properties.Resources.Francia18;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = global::eClockSync5.Properties.Resources.Brasil18;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Frm_Asistente
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Frm_Asistente";
            this.ShowInTaskbar = true;
            this.Load += new System.EventHandler(this.Frm_Asistente_Load);
            this.Controls.SetChildIndex(this.m_separator, 0);
            this.Controls.SetChildIndex(this.m_finishButton, 0);
            this.Controls.SetChildIndex(this.m_cancelButton, 0);
            this.Controls.SetChildIndex(this.m_nextButton, 0);
            this.Controls.SetChildIndex(this.m_backButton, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.button3, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}