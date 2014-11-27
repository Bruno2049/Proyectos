using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Universidad.Controlador.SVR_Login;
using Universidad.Entidades;

namespace Universidad.Controlador.Login
{
    public class SVC_LoginAdministrativos
    {
        #region Propiedades de la clase

        /// <summary>
        /// Instancia de la clace SVC_GestionCatalogos
        /// </summary>
        private static readonly SVC_LoginAdministrativos _classInstance = new SVC_LoginAdministrativos();

        private S_LoginClient _servicio;
        

        public static SVC_LoginAdministrativos ClassInstance
        {
            get { return _classInstance; }
        }

        private SVC_LoginAdministrativos()
        {
            _servicio = new S_LoginClient();
        }

        #endregion

        public delegate void LoginAdministrativosArgs(US_USUARIOS usuario);

        public event LoginAdministrativosArgs LoginAdministrativosFinalizado;

        public void LoginAdministrativos(string usuario, string contrasena)
        {

            //var jLogin = _servicio.LoginAdministrador(usuario, contrasena);

            //var login = JsonConvert.DeserializeObject<US_USUARIOS>(jLogin);

            //return login;
            //_servicio.LoginAdministradorCompleted += LoginAdministrativosFinalizado;
            //_servicio.LoginAdministradorAsync(usuario, contrasena);
        }
    }
}
