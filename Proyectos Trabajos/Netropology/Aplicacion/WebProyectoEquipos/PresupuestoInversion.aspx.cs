using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.AccesoDatos.AdminUsuarios;
using PAEEEM.LogicaNegocios.AdminUsuarios;

namespace WebProyectoEquipos
{
    public partial class PresupuestoInversion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var lstUsuarios = new PermisoUsuarioDal().ObtenUsuarios();

            try
            {
                foreach (var lstUsuario in lstUsuarios)
                {
                    var contrasenaEncriptada = ValidacionesUsuario.Encriptar(lstUsuario.Contrasena);
                    lstUsuario.Contrasena = contrasenaEncriptada;
                    new PermisoUsuarioDal().ActualizaUsuario(lstUsuario);
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox1.Text))
            {
                var contrasena = ValidacionesUsuario.Desencriptar(TextBox1.Text);
                Label1.Text = "Contraseña: " + contrasena;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox1.Text))
            {
                var contrasena = ValidacionesUsuario.Encriptar(TextBox1.Text);
                Label1.Text = "Contraseña Ecriptada: " + contrasena;
            }
        }
    }
}