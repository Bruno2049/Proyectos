using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace eClock5.Modelos
{
    class eClockSync
    {
        private bool m_Ejecutar = false;
        public bool Ejecutar
        {
            get { return m_Ejecutar; }
            set { m_Ejecutar = value; }
        }

        private string m_RutaApp = "eClockSync\\eClockSync.exe";
        public string RutaApp
        {
            get
            {
                return m_RutaApp;
            }
            set
            {
                m_RutaApp = value;
            }
        }

        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Proxy_URL { get; set; }
        public string Proxy_Usuario { get; set; }
        public string Proxy_Clave { get; set; }
        public string Sitios { get; set; }

        public bool Guarda()
        {
            return CeC_StreamFile.sNuevoTexto("eClockSync.cfg", JsonConvert.SerializeObject(this));
        }

        public static eClockSync Carga()
        {
            try
            {
                string Texto = CeC_StreamFile.sLeerString("eClockSync.cfg");
                eClockSync R = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockSync>(Texto);
                if (R != null)
                    return R;
            }
            catch { }
            return new eClockSync();
        }

        public static string CalculaHash(string Texto)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider Sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            string HashSR = BitConverter.ToString(Sha1.ComputeHash(new System.IO.MemoryStream(System.Text.ASCIIEncoding.Default.GetBytes(Texto))));
            return HashSR;
        }

    }
}
