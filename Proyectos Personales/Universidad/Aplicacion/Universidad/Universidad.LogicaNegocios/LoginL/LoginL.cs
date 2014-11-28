
using Universidad.AccesoDatos.ControlUsuarios.LoginA;
using Universidad.Entidades;

namespace Universidad.LogicaNegocios.LoginL
{
    public class LoginL
    {
        #region Propiedades de la clase

// ReSharper disable once InconsistentNaming
        protected static readonly LoginL _classInstance = new LoginL();

        public static LoginL ClassInstance
        {
            get { return _classInstance; }
        }

        #endregion

        #region Login Administrativos

        public US_USUARIOS LoginAdminitradorUsuarios(string usuario, string contrasena)
        {
            var login = LoginA.ClassInstance.LoginAdministrador(usuario, contrasena);
            //var login = LoginA.ClassInstance.LoginAdministradoresTSQL(usuario, contrasena);
            return login;
        }

        #endregion
    }
}
