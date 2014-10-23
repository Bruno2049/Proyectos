using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityPrueba.Entidad;
using EntityPrueba.LogicaNegocios.Login;

namespace EntityPrueba.Prueba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            US_USUARIOS Usuario = new US_USUARIOS();
            Usuario.USUARIO = "EstebanCL";
            Usuario.NOMBRE_COMPLETO = "Esteban Cruz Lagunes";
            Usuario.CORREO_ELECTRONICO = "ecruzlagunes@hotmail.com";
            Usuario.CONTRASENA = "A@141516";
            var UsuarioR = ControlUsuariosLN.ClassInstance.InsertaUsuario(Usuario);

            radLabel1.Text = Usuario.NOMBRE_COMPLETO;
        }
    }
}
