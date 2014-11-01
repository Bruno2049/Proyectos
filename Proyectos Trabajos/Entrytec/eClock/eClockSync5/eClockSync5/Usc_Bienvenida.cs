using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SMS.Windows.Forms;

namespace eClockSync5
{
    public partial class Usc_Bienvenida : ExteriorWizardPage
    {
        public Usc_Bienvenida()
        {
            InitializeComponent();
        }
        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;

            // Enable both the Next and Back buttons on this page    
            Wizard.SetWizardButtons(WizardButton.Next);
            return true;
        }

        private void Usc_Bienvenida_Load(object sender, EventArgs e)
        {

        }
    }
}
