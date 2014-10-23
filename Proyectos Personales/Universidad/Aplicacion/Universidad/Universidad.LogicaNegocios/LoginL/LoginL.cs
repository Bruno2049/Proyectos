using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.AccesoDatos.ControlUsuarios.LoginA;
using Universidad.Entidades;

namespace Universidad.LogicaNegocios.LoginL
{
    public class LoginL
    {
        #region Propiedades de la clase

        private static readonly LoginL _classInstance = new LoginL();

        public static LoginL ClassInstance
        {
            get { return _classInstance; }
        }

        public LoginL()
        {
        }

        #endregion

        #region Login Administrativos

        public US_USUARIOS LoginAdminitradorUsuarios(string Usuario, string Contrasena)
        {
            var Login = new US_USUARIOS();
            
            Login = LoginA.ClassInstance.LoginAdministrador(Usuario, Contrasena);

            return Login ?? null;
        }

        #endregion
    }
}
