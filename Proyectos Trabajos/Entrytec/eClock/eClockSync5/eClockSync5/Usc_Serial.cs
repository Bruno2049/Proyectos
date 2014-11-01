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
    public partial class Usc_Serial : InteriorWizardPage
    {
       

        public Usc_Serial()
        {
            InitializeComponent();
        }

        private void Usc_Serial_Load(object sender, EventArgs e)
        {
            Lbl_IDUnico.Text = CeC_Registro.ObtenIDWindows();
            Tbx_Llave.Text = eClockSync5.Properties.Settings.Default.Licencia;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private bool ClaveValida()
        {
            Lbl_LlaveNValida.Visible = true;
            if (Tbx_Llave.Text.Length <= 20)
                return false;
            string Llave = Tbx_Llave.Text.Replace("-", "");
            string Licencia = Llave.Substring(0, 16);
            byte[] Arreglo = CeC.ObtenArregloBytes(Licencia);
            int Chk = 0;
            foreach (byte Caracter in Arreglo)
            {
                Chk = Chk + Caracter * 7 + 52;
            }
            Chk = Chk % 100;
            string sChk = Llave.Substring(Llave.Length - 2, 2);
            if (Chk.ToString() != sChk)
                return false;
            
            Lbl_LlaveNValida.Visible = false;
            return true;
        }
        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;

            Wizard.SetWizardButtons(WizardButton.Back |
                (ClaveValida() ? WizardButton.Next : WizardButton.Back));
            return true;
        }
        protected override string OnWizardNext()
        {

            eClockSync5.Properties.Settings.Default.Licencia = Tbx_Llave.Text;
            eClockSync5.Properties.Settings.Default.Save();

            // Move to the default next page in the wizard
            return base.OnWizardNext();
        }

        private void Tbx_Llave_TextChanged(object sender, EventArgs e)
        {
            OnSetActive();
        }
    }
}
