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

        public static SVC_LoginAdministrativos ClassInstance
        {
            get { return _classInstance; }
        }

        private SVC_LoginAdministrativos()
        {
        }

        #endregion

        public US_USUARIOS LoginAdministrativos(string usuario, string contrasena)
        {
            var servicio = new S_LoginClient();

            

            var jLogin = servicio.LoginAdministrador(usuario, contrasena);

            var login = JsonConvert.DeserializeObject<US_USUARIOS>(jLogin);

            return login;
        }
    }
}
