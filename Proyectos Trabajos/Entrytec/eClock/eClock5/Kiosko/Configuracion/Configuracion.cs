using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using eClock5;

namespace Kiosko.Configuracion
{
    public class Configuracion
    {
        private string m_Color = "#FF003250";
        public string Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

        private string m_TicketAppPDF = "C:\\Archivos de programa\\Adobe\\Reader 8.0\\Reader\\AcroRd32.exe";
        public string TicketAppPDF
        {
            get
            {
                return m_TicketAppPDF;
            }
            set
            {
                m_TicketAppPDF = value;
            }
        }

        private string m_TicketParametros = "/t \"@ARCHIVO@\" \"@IMPRESORA@\"";
        public string TicketParametros
        {
            get
            {
                return m_TicketParametros;
            }
            set
            {
                m_TicketParametros = value;
            }
        }

        public static Configuracion Carga()
        {
            try
            {
                string Texto = CeC_StreamFile.sLeerString("KioskoConfig.cfg");
                Configuracion R = JsonConvert.DeserializeObject<Configuracion>(Texto);
               // System.Windows.MessageBox.Show(R.TicketParametros);
                if (R != null)
                    return R;
            }
            catch (Exception ex) { 
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return new Configuracion();
        }

        public bool Guarda()
        {
            return CeC_StreamFile.sNuevoTexto("KioskoConfig.cfg", JsonConvert.SerializeObject(this));
        }

    }
}
