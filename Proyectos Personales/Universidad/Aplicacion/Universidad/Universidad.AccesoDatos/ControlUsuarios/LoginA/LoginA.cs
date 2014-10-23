using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.ControlUsuarios.LoginA
{
    public class LoginA
    {
        #region Propiedades de la clase
        
        private static readonly LoginA _classInstance = new LoginA();

        public static LoginA ClassInstance
        {
            get { return _classInstance; }
        }

        /// <summary>
        ///  Instancia hacia el contexto de la DB
        /// </summary>
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public LoginA()
        {
        }

        #endregion

        #region Metodos de Insercion

        #endregion

        #region Metodos de Extraccion

        public US_USUARIOS LoginAdministrador(string Nombre, string Contrasena)
        {
            US_USUARIOS Usuario = null;

            using (var Aux = new Repositorio<US_USUARIOS> ())
            {
                Usuario = Aux.Extraer(r => r.USUARIO == Nombre && r.CONTRASENA == Contrasena);
            }

            return Usuario;
        }

        #endregion
    }
}
