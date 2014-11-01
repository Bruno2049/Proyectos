using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace eEnroler
{
    public partial class FLogIn : Form
    {
        public WS_eCheck.WS_eCheck ws_eCheck = null;
        public string Usuario = "";
        public string Clave = "";
        public FLogIn()
        {
            InitializeComponent();
        }

        private void Lnk_NuevoUsuario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(ws_eCheck.ObtenLinkNuevoUsuario());
        }
        /// <summary>
        /// Calcula el valor del Hash de un texto
        /// </summary>
        /// <param name="Texto">Texto de donde se va a obtener el hash</param>
        /// <returns></returns>
        public static string CalculaHash(string Texto)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider Sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            string HashSR = BitConverter.ToString(Sha1.ComputeHash(new System.IO.MemoryStream(System.Text.ASCIIEncoding.Default.GetBytes(Texto))));
            return HashSR;
        }

        private void Lnk_OlvidoClave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(ws_eCheck.ObtenLinkOlvidoClave());

        }

        private void Btn_Iniciar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ws_eCheck.ValidarUsuario(Tbx_Usuario.Text, Tbx_Clave.Text) > 0)
                {
                    Usuario = Tbx_Usuario.Text;
                    Clave = Tbx_Clave.Text;
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                    return;
                }
                Pbx_Icono.Image = global::eEnroler.Properties.Resources.warning_triangle;
                Linea1.BackColor = Color.Red;
                Linea2.BackColor = Color.Red;
                LblMensaje.ForeColor = Color.Red;
                LblMensaje.Text = "Usuario o contraseña incorrectos, si no recuerda su clave o no tiene una use los links en la parte inferor de la ventana";
                return;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaErrorMsg(ex);
//                MessageBox.Show(ex.Message);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
