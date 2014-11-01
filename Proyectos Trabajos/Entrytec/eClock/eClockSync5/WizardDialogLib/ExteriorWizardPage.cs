//
// ExteriorWizardPage.cs
//
// Copyright (C) 2002-2002 Steven M. Soloff (mailto:s_soloff@bellsouth.net)
// All rights reserved.
//

using System;
using System.Windows.Forms;

namespace SMS.Windows.Forms
{
    /// <summary>
    /// Base class that is used to represent an exterior page (welcome or
    /// completion page) within a wizard dialog.
    /// </summary>
    public class ExteriorWizardPage : WizardPage
	{
        // ==================================================================
        // Protected Fields
        // ==================================================================

        /// <summary>
        /// The title label.
        /// </summary>
        protected Label m_titleLabel;
        protected Label m_DescriptionLabel;
        
        /// <summary>
        /// The watermark graphic.
        /// </summary>
        protected PictureBox m_watermarkPicture;


        // ==================================================================
        // Public Constructors
        // ==================================================================
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SMS.Windows.Forms.ExteriorWizardPage">ExteriorWizardPage</see>
        /// class.
        /// </summary>
        public ExteriorWizardPage()
		{
			// This call is required by the Windows Form Designer
			InitializeComponent();
		}


        // ==================================================================
        // Private Methods
        // ==================================================================

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_titleLabel = new System.Windows.Forms.Label();
            this.m_watermarkPicture = new System.Windows.Forms.PictureBox();
            this.m_DescriptionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_watermarkPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // m_titleLabel
            // 
            this.m_titleLabel.BackColor = System.Drawing.Color.White;
            this.m_titleLabel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_titleLabel.Location = new System.Drawing.Point(170, 13);
            this.m_titleLabel.Name = "m_titleLabel";
            this.m_titleLabel.Size = new System.Drawing.Size(292, 39);
            this.m_titleLabel.TabIndex = 0;
            this.m_titleLabel.Text = "Sample Setup Wizard";
            // 
            // m_watermarkPicture
            // 
            this.m_watermarkPicture.BackColor = System.Drawing.Color.White;
            this.m_watermarkPicture.Image = global::SMS.Windows.Forms.Properties.Resources.watermark;
            this.m_watermarkPicture.Location = new System.Drawing.Point(0, 0);
            this.m_watermarkPicture.Name = "m_watermarkPicture";
            this.m_watermarkPicture.Size = new System.Drawing.Size(164, 312);
            this.m_watermarkPicture.TabIndex = 1;
            this.m_watermarkPicture.TabStop = false;
            // 
            // m_DescriptionLabel
            // 
            this.m_DescriptionLabel.BackColor = System.Drawing.Color.White;
            this.m_DescriptionLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_DescriptionLabel.Location = new System.Drawing.Point(170, 73);
            this.m_DescriptionLabel.Name = "m_DescriptionLabel";
            this.m_DescriptionLabel.Size = new System.Drawing.Size(292, 64);
            this.m_DescriptionLabel.TabIndex = 2;
            this.m_DescriptionLabel.Text = "Welcome to the Sample Setup Wizard";
            // 
            // ExteriorWizardPage
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_DescriptionLabel);
            this.Controls.Add(this.m_watermarkPicture);
            this.Controls.Add(this.m_titleLabel);
            this.Name = "ExteriorWizardPage";
            this.Load += new System.EventHandler(this.ExteriorWizardPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_watermarkPicture)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        private void ExteriorWizardPage_Load(object sender, EventArgs e)
        {

        }

    }
}
