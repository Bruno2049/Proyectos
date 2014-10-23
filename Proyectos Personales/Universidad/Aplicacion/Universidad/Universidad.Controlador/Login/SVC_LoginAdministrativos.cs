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

        public SVC_LoginAdministrativos()
        {
        }

        #endregion

        public US_USUARIOS LoginAdministrativos(string Usuario, string Contrasena)
        {
            S_LoginClient Servicio = new S_LoginClient();

            

            string JLogin = Servicio.LoginAdministrador(Usuario, Contrasena);

            US_USUARIOS Login = JsonConvert.DeserializeObject<US_USUARIOS>(JLogin);

            return Login;
        }




        //public event System.EventHandler Logeo_Finalizado = null;
        //public event EventArgs 
    }
}
