using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


using SMS.Windows.Forms;

namespace eClockSync5
{
    public partial class Usc_Licencia : InteriorWizardPage
    {
        public Usc_Licencia()
        {
            InitializeComponent();
        }

        private void Usc_Licencia_Load(object sender, EventArgs e)
        {
            
        }
        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;

            Wizard.SetWizardButtons(WizardButton.Back |
                (checkBox1.Checked ? WizardButton.Next : WizardButton.Back));
            return true;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Enable both the Back and Finish (enabled/disabled) buttons on this page    
            OnSetActive();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void m_subtitleLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
