using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;

namespace eClockWin
{
    static class Program
    {

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CIS_Global Global = new CIS_Global();
            Global.Corre();
        }
    }
    public class CIS_Global
    {
        static System.Net.CookieContainer CC = new System.Net.CookieContainer();
        static public WSChecador.WSChecador wsChecador = new eClockWin.WSChecador.WSChecador();
        private Dictionary<string, string> GetQueryStringParameters()
        {
            Dictionary<string, string> nameValueTable = new Dictionary<string, string>();

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                string url = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
                string queryString = (new Uri(url)).Query;
                string[] nameValuePairs = queryString.Split('&');
                foreach (string pair in nameValuePairs)
                {
                    string[] vars = pair.Split('=');
                    if (!nameValueTable.ContainsKey(vars[0]))
                    {
                        nameValueTable.Add(vars[0].Replace("?", ""), vars[1]);
                    }
                }
            }

            return (nameValueTable);
        }
        void IniciaWS()
        {
            try
            {
                wsChecador.Proxy = null;
                wsChecador.ObtenFechaHora();
#if DEBUG
                MessageBox.Show("Conectado ObtenFechaHora");
#endif
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                System.Net.ICredentials Cred = new System.Net.NetworkCredential("invitado.mexico@codere.com", "inme0909");
                System.Net.WebProxy wp = new System.Net.WebProxy("172.31.255.2:8080", true, null, Cred);
                wsChecador.Proxy = wp;
                wsChecador.Proxy = null;
            }
        }
        public void Corre()
        {
            wsChecador.CookieContainer = CC;
            IniciaWS();

            string Sesion_ID = "";
            string Enrolar = "";
            string Persona_ID = "";
            try
            {
                Uri myUri = new Uri(ApplicationDeployment.CurrentDeployment.ActivationUri, "WSChecador.asmx");
                wsChecador.Url = myUri.ToString();

                Dictionary<string, string> param = GetQueryStringParameters();
#if DEBUG
                foreach (KeyValuePair<string, string> kvp in param)
                {
                    MessageBox.Show(string.Format("Llave {0} = {1}", kvp.Key, kvp.Value));

                }
                
                   MessageBox.Show(param.Count.ToString());
#endif
                param.TryGetValue("SESION_ID", out Sesion_ID);
                param.TryGetValue("PERSONA_ID", out Persona_ID);
                if (param.TryGetValue("HUELLAS", out Enrolar))
                {
                    FEnrolar EnrolarDlg = new FEnrolar();
                    if (Sesion_ID.Length > 0)
                        EnrolarDlg.m_Sesion_Id = Convert.ToInt32(Sesion_ID);
                    if (Persona_ID.Length > 0)
                        EnrolarDlg.m_Persona_Id = Convert.ToInt32(Persona_ID);
                    Application.Run(EnrolarDlg);
                    return;
                    return;
                }
            }
            catch
            {
            }
#if ENROLA
            FEnrolar EnrolarDlg2 = new FEnrolar();
            Application.Run(EnrolarDlg2);
            return;
#endif

            FTerminales Terminales = new FTerminales();
            if (Sesion_ID.Length > 0)
                Terminales.m_Sesion_ID = Convert.ToInt32(Sesion_ID);
            Application.Run(Terminales);
        }
    }

}