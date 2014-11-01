using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Controladores
{
    public class Nominas
    {
        ES_Nominas.S_NominasClient m_S_Nominas = null;
        CeC_SesionBase m_SesionBase = null;

        public Nominas(CeC_SesionBase SesionBase)
        {
            m_S_Nominas = new ES_Nominas.S_NominasClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Nominas.svc"));
            m_SesionBase = SesionBase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecNominas"></param>
        public delegate void ObtenDatosReciboArgs(eClockBase.Modelos.Nomina.Reporte_RecNomina RecNominas);
        public event ObtenDatosReciboArgs ObtenDatosReciboEvent;
        public void ObtenDatosRecibo(int RecNominaID)
        {
            m_SesionBase.MuestraMensaje("Obteniendo recibo de nomina");
            m_S_Nominas.ObtenDatosReciboCompleted += m_S_Nominas_ObtenDatosReciboCompleted;
            m_S_Nominas.ObtenDatosReciboAsync(m_SesionBase.SESION_SEGURIDAD, RecNominaID, m_SesionBase.IDIOMA);
        }

        void m_S_Nominas_ObtenDatosReciboCompleted(object sender, ES_Nominas.ObtenDatosReciboCompletedEventArgs e)
        {
            try
            {
                try
                {
                    if (ObtenDatosReciboEvent != null)
                        ObtenDatosReciboEvent(eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Nomina.Reporte_RecNomina>(e.Result));
                    return;
                }
                catch { }
                if (ObtenDatosReciboEvent != null)
                    ObtenDatosReciboEvent(null);
                m_SesionBase.MuestraMensaje("Error desconocido", 5);
                return;
            }
            catch { }
            if (ObtenDatosReciboEvent != null)
                ObtenDatosReciboEvent(null);
            m_SesionBase.MuestraMensaje("Error de red", 5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Resultado"></param>
        public delegate void ConfirmaReciboNominaImpresoArgs(bool Resultado);
        public event ConfirmaReciboNominaImpresoArgs ConfirmaReciboNominaImpresoEvent;

        public void ConfirmaReciboNominaImpreso(int RecNominaID, int RecNominaImp)
        {
            m_SesionBase.MuestraMensaje("Recibo de Nómina Impreso");
            m_S_Nominas.ConfirmaReciboNominaImpresoCompleted += m_S_Nominas_ConfirmaReciboNominaImpresoCompleted;
            m_S_Nominas.ConfirmaReciboNominaImpresoAsync(m_SesionBase.SESION_SEGURIDAD, RecNominaID, RecNominaImp);
        }

        void m_S_Nominas_ConfirmaReciboNominaImpresoCompleted(object sender, ES_Nominas.ConfirmaReciboNominaImpresoCompletedEventArgs e)
        {
            try
            {
                if (e.Result != false)
                {
                    m_SesionBase.MuestraMensaje("Recibo Impreso", 3);
                }
                else
                {
                    m_SesionBase.MuestraMensaje("Foto NO Guardada", 5);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Parametros de notificacion de que se termino de importar el registro
        /// </summary>
        /// <param name="PersonasLinksIds">PersonasLinkIds separadon por coma correctamente importados</param>
        /// <param name="sPersonasLinksIDs"></param>
        /// <param name="NoRegistros">Si es -1 significa que trono</param>
        public delegate void ImportaRecibosArgs(string PersonasLinksIDs, string[] sPersonasLinksIDs, int NoRegistros);
        /// <summary>
        /// Evento que notifica que se terminaron de importar los registros, los parametros estan definidos en
        /// ImportaRecibosArgs
        /// </summary>
        public event ImportaRecibosArgs ImportaRecibosEvent;

        /// <summary>
        /// Importa un recibo de nomina
        /// </summary>
        /// <param name="ReciboNomina">Recibo de nomina a importar</param>
        public void ImportaRecibos(eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML ReciboNomina)
        {
            List<eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML> RecibosNomina = new List<Modelos.Nomina.Model_RecNominasPDFyXML>();
            RecibosNomina.Add(ReciboNomina);
            ImportaRecibos(RecibosNomina);
        }
        /// <summary>
        /// Importa recibos de nomina
        /// </summary>
        /// <param name="RecibosNomina">Lista con los recibos de nomina a importar</param>
        public void ImportaRecibos(List<eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML> RecibosNomina)
        {
            //Muestra un mensaje de la accion
            m_SesionBase.MuestraMensaje("Importando recibo de nómina...");

            m_S_Nominas.ImportaRecibosCompleted += m_S_Nominas_ImportaRecibosCompleted;

            ///Serializa los recibos de nómina en formato JSon,
            ///Si el json es muy grande regresa uno comprimido
            string Json = eClockBase.Controladores.CeC_ZLib.Object2ZJson(RecibosNomina);
            //Inicia la importacion de los recibos
            m_S_Nominas.ImportaRecibosAsync(m_SesionBase.SESION_SEGURIDAD,
                Json);

        }

        void m_S_Nominas_ImportaRecibosCompleted(object sender, ES_Nominas.ImportaRecibosCompletedEventArgs e)
        {
            //Codigo de accion a realizar cuando se termine la tarea en segundo plano
            try
            {
                //Muestra un mensaje para el usuario dependiendo el resultado
                if (e.Result == null || e.Result == "")
                {
                    m_SesionBase.MuestraMensaje("No se importaron registros", 5);
                    if (ImportaRecibosEvent != null)
                        ImportaRecibosEvent("", new string[] { }, 0);
                }
                else
                {
                    string []s = CeC.ObtenArregoSeparador(e.Result, ",");
                    m_SesionBase.MuestraMensaje(
                        "Se importaron " + s.Length + "registro(s)", 3);
                    if (ImportaRecibosEvent != null)
                        ImportaRecibosEvent(e.Result, s, s.Length);

                }
                //e.Result
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                m_SesionBase.MuestraMensaje("Error de Red", 5);
                if (ImportaRecibosEvent != null)
                    ImportaRecibosEvent("", new string[] { }, 0);

            }

        }


    }
}
