using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using System.Globalization;
namespace eClockSync
{
    class CePanelPlumas : CeTerminalSync
    {


        public override Errores PoleoChecadas()
        {
            DateTime UltimaFecha= new DateTime(2002, 09, 24);
            CultureInfo culture = new CultureInfo("es-MX");
            try
            {
                UltimaFecha = Convert.ToDateTime(IUChecadas, culture);
            }
            catch
            {
            }

            if (UltimaFecha > DateTime.Now)
            {
                UltimaFecha = DateTime.Today.AddDays(-5);
            }

            try
            {
                DS_PlumasMTableAdapters.EventosTableAdapter TAEventos = new eClockSync.DS_PlumasMTableAdapters.EventosTableAdapter();
                DS_PlumasM.EventosDataTable TablaBuf = TAEventos.GetDataByFecha(UltimaFecha);
                foreach (DS_PlumasM.EventosRow Fila in TablaBuf)
                {
                    try
                    {
                        DateTime FechaHora = Fila.Fecha_Hora;

                        if (!Fila.IsFCNull() && !Fila.IsID_EventoNull() && !Fila.IsIDNull())
                        {
                            string NS = Fila.FC.ToString() + "/" + Fila.ID.ToString();
                            if (Fila.ID_Evento == 0 )
                            {
                                AgregaChecada(NS, FechaHora, TipoAccesos.Entrada);
                            }
                            if (Fila.ID_Evento == 1)
                            {
                                AgregaChecada(NS, FechaHora, TipoAccesos.Salida);
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        CeLog2.AgregaError("PoleoChecadas", Fila.ToString(), ex);
                    }
                }

                IUChecadasNuevo = UltimaFecha.ToString(culture);
                return Errores.Correcto;
            }
            catch
            {
            }
            
            return Errores.Error_Conexion;

        }

    }
}
