using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Universidad.AplicacionAdministrativa.Vistas.Sistema;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Entidades;

namespace Universidad.AplicacionAdministrativa.Vistas
{
    public partial class Inicio : Form
    {
        private Form Padre;
        private US_USUARIOS Usuario;

        public Inicio(Form Padre,US_USUARIOS Usuario)
        {
            this.Padre = Padre;
            this.Usuario = Usuario;
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            LBL_Nombre_Usuario.Text = "Bienvenido " + Usuario.NOMBRE_COMPLETO;

            var TipoUsuario = SVC_GestionCatalogos.ClassInstance.ObtenTipoUsuario((int)Usuario.ID_TIPO_USUARIO);

            LBL_Tipo_Usuario.Text = TipoUsuario.TIPO_USUARIO;

        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Padre.Close();
        }
    }
}
