using System;
using System.Windows.Forms;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Entidades;

namespace Universidad.AplicacionAdministrativa.Vistas
{
    public partial class Inicio : Form
    {
        private readonly Form _padre;
        private readonly US_USUARIOS _usuario;

        public Inicio(Form padre,US_USUARIOS usuario)
        {
            _padre = padre;
            _usuario = usuario;
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            LBL_Nombre_Usuario.Text = @"Bienvenido " + _usuario.NOMBRE_COMPLETO;

            if (_usuario.ID_TIPO_USUARIO == null) return;
            var tipoUsuario = SVC_GestionCatalogos.ClassInstance.ObtenTipoUsuario((int)_usuario.ID_TIPO_USUARIO);

            LBL_Tipo_Usuario.Text = tipoUsuario.TIPO_USUARIO;
        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            _padre.Close();
        }
    }
}
