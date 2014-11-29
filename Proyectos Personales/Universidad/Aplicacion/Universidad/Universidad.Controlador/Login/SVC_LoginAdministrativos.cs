using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
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
            _servicio.LoginAdministradorAsync(usuario, contrasena);
        }

        void _servicio_LoginAdministradorCompleted(object sender, LoginAdministradorCompletedEventArgs e)
        {
            try
            {

                if (e.Result == null) return;

                var resultado = e.Result;
                var usuario = JsonConvert.DeserializeObject<US_USUARIOS>(resultado);

                LoginAdministrativosFinalizado(usuario);
            }
            catch (Exception er)
            {
                throw;
            }
        }

        #endregion

        public delegate void ObtenNombreCompletoArgs(PER_PERSONAS persona);

        public event ObtenNombreCompletoArgs ObtenNombreCompletoFinalizado;

        public void ObtenNombreCompleto(US_USUARIOS usuario)
        {
            _servicio.ObtenPersonaCompleted += _servicio_ObtenPersonaCompleted;
            _servicio.ObtenPersonaAsync(usuario);
        }

        void _servicio_ObtenPersonaCompleted(object sender, ObtenPersonaCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var resultado = e.Result;
            var persona = JsonConvert.DeserializeObject<PER_PERSONAS>(resultado);
            ObtenNombreCompletoFinalizado(persona);
        }
    }
}
