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
    public partial class Frm_LogIn : Form
    {
        public Frm_LogIn()
        {
            InitializeComponent();
        }
        public void Incorrecto()
        {
            Pbx_Icono.Image = eClockSync5.Properties.Resources.warning_triangle;
            Linea1.BackColor = Color.Red;
            Linea2.BackColor = Color.Red;
            LblMensaje.ForeColor = Color.Red;
            LblMensaje.Text = "Usuario o contraseña incorrectos, si no recuerda su clave o no tiene una use los links en la parte inferor de la ventana";
        }
        private void Btn_Iniciar_Click(object sender, EventArgs e)
        {
            CeS_eCheck.m_Usuario = Tbx_Usuario.Text;
            CeS_eCheck.m_Clave = Tbx_Clave.Text;
            CeS_eCheck.m_GuardarClave = Chk_Recordar.Checked;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Frm_LogIn_Load(object sender, EventArgs e)
        {
            Tbx_Usuario.Text = CeS_eCheck.m_Usuario;
            Tbx_Clave.Text = CeS_eCheck.m_Clave;
            Chk_Recordar.Checked = CeS_eCheck.m_GuardarClave;
        }

        private void Lnk_OlvidoClave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void Tbx_Clave_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
