using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eClockSync5
{
    public partial class Frm_Configuracion : Form
    {
        public Frm_Configuracion()
        {
            InitializeComponent();
        }

        private void Btn_UsuarioClave_Click(object sender, EventArgs e)
        {
            Frm_LogIn Frm = new Frm_LogIn();
            if (Frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                CeS_eCheck.GuardaParametros();
        }

        private void Btn_Sitios_Click(object sender, EventArgs e)
        {
            CeS_eCheck.EligeSitios();
        }
    }
}
