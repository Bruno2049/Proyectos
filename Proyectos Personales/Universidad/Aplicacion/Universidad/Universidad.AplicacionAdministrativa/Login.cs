using System;
using System.Windows.Forms;
using Universidad.AplicacionAdministrativa.Vistas;
using Universidad.Controlador.Login;
using Universidad.Entidades;


namespace Universidad.AplicacionAdministrativa
{
// ReSharper disable once InconsistentNaming
    public partial class FORM_Login : Form
    {
        private US_USUARIOS _login;

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
                var usuario = TXB_Usuario.Text;
                var contrasena = TXB_Contrasena.Text;

                _login = SVC_LoginAdministrativos.ClassInstance.LoginAdministrativos(usuario, contrasena);

                //var Login = SVC_LoginAdministrativos.ClassInstance.Logeo_Finalizado += LogeoFinalizado;

                if (_login != null)
                {
                    switch (_login.ID_ESTATUS_USUARIOS)
                    {
                        case 1:
                        {
                            var  fromInicio = new Inicio(this, _login);
                            Hide();
                            fromInicio.Show();
                        }
                            break;
                    }
                }

                else
                {
                    MessageBox.Show(text: @"El usuario o contraseña no son valido favor de verificar", caption: @"Error al verificar Login", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);

                }

            }
            catch (Exception exception)
            {
                MessageBox.Show((exception.InnerException).ToString());
            }
        }

        //private void LogeoFinalizado(object sender, EventArgs eventArgs)
        //{
        //}

        private void BTN_Salir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
