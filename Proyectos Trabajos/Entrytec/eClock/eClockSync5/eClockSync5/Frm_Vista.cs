using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace eClockSync5
{
    public partial class Frm_Vista : Form
    {
        public string m_Url = "";
        public Frm_Vista()
        {
            InitializeComponent();
        }

        public static bool MuestraUrl(string Url)
        {
            return MuestraUrl(Url, true);
        }


        public static bool MuestraUrl(string Url, bool EsRutaRelativa)
        {
            Frm_Vista Frm = new Frm_Vista();
            if (EsRutaRelativa)
                Frm.m_Url = CeS_eCheck.ObtenUrl(Url);
            else
                Frm.m_Url = Url;
            Frm.ShowDialog();
            return true;
        }


        private void Frm_Vista_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(m_Url);
        }
    }
}
