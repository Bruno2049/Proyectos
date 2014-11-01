using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace eEnroler
{
    public partial class FrLeerTarjeta : Form
    {
        public string NoSerie = "";
        public FrLeerTarjeta()
        {
            InitializeComponent();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (Tbx_NoSerie.Text.Length < 4)
            {
                MessageBox.Show("No se ha introducido el no. de serie de la tarjeta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //NoSerie = Tbx_NoSerie.Text;
            try
            {
                int iNoSerie = Convert.ToInt32(Tbx_NoSerie.Text);
                
                if (eEnroler.Properties.Settings.Default.TransformarNS2LongNS)
                {
                    int SiteCode = iNoSerie / 100000;
                    int NoCard = iNoSerie % 100000;
                    iNoSerie = SiteCode * 65536 + NoCard;
                }
                NoSerie = iNoSerie.ToString();
            }
            catch {
                MessageBox.Show("No. de serie de la tarjeta Invalido", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
