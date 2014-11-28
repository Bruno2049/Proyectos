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

        private S_LoginClient _servicio;

        public SVC_LoginAdministrativos()
        {
            _servicio = new S_LoginClient();
        }

        #endregion

        #region LoginAdministrativo
        public delegate void LoginAdministrativoArgs(US_USUARIOS usuario);

        public event LoginAdministrativoArgs LoginAdministrativosFinalizado;

        public void LoginAdministrativo(string usuario, string contrasena)
        {
            _servicio.LoginAdministradorCompleted += _servicio_LoginAdministradorCompleted;
            _servicio.LoginAdministradorAsync(usuario,contrasena);
        }

        void _servicio_LoginAdministradorCompleted(object sender, LoginAdministradorCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var resultado = e.Result;
            var usuario = JsonConvert.DeserializeObject<US_USUARIOS>(resultado);

            LoginAdministrativosFinalizado(usuario ?? null);
        }
        #endregion

    }
}
