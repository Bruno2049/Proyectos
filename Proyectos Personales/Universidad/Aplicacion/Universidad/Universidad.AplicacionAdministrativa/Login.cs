using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Universidad.AplicacionAdministrativa.Vistas;
using Universidad.Controlador;
using Universidad.Controlador.Login;
using Universidad.Entidades;


namespace Universidad.AplicacionAdministrativa
{
    public partial class FORM_Login : Form
    {
        public FORM_Login()
        {
            InitializeComponent();
        }

        private void FORM_Login_Load(object sender, EventArgs e)
        {

        }

        private void BTN_Login_Click(object sender, EventArgs e)
        {
            try
            {
                var Usuario = TXB_Usuario.Text;
                var Contrasena = TXB_Contrasena.Text;

                var Login = SVC_LoginAdministrativos.ClassInstance.LoginAdministrativos(Usuario, Contrasena);

                //var Login = SVC_LoginAdministrativos.ClassInstance.Logeo_Finalizado += LogeoFinalizado;

                if (Login != null)
                {
                    switch (Login.ID_ESTATUS_USUARIOS)
                    {
                        case 1:
                        {
                            var  fromInicio = new Inicio(this, Login);
                            this.Hide();
                            fromInicio.Show();
                        }
                            break;
                    }
                }

                else
                {
                    MessageBox.Show("El usuario o contraseña no son valido favor de verificar", "Error al verificar Login",MessageBoxButtons.OK,MessageBoxIcon.Information);

                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void LogeoFinalizado(object sender, EventArgs eventArgs)
        {
        }

        private void BTN_Salir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
