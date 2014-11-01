using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eClockSync5
{
    public class CeC_ABC
    {
        public static bool ObtenDatos(string Tabla, string CamposLlave, object Objeto)
        {
            try
            {
                eClockSync5.ES_Sesion.S_SesionClient Cliente = new ES_Sesion.S_SesionClient();
                System.Data.DataTable Dt = CeC.ConvertToDataTable(Objeto);
                System.Data.DataTable Datos = Cliente.ObtenDatos(eClockSync5.CeC_SesionMVC.SESION_SEGURIDAD, Tabla, CamposLlave, Dt);
                return CeC.ConvierteDataRow2Object(Datos.Rows[0], Objeto, false);
            }
            catch(Exception ex)
            {
                //CIsLog2.AgregaError(ex);
            }
            return false;
        }

        public static int GuardaDatos(string Tabla, string CamposLlave, object Objeto, bool EsNuevo)
        {
            try
            {
                eClockSync5.ES_Sesion.S_SesionClient Cliente = new ES_Sesion.S_SesionClient();
                //System.Data.DataTable Dt = CeC.ConvertToDataTable(Objeto);
                //return Cliente.GuardaDatos(eClockSync5.CeC_SesionMVC.SESION_SEGURIDAD, Tabla, CamposLlave, Dt, eClockSync5.CeC_SesionMVC.SUSCRIPCION_ID_SELECCIONADA, EsNuevo);
            }
            catch (Exception ex)
            {
                //CIsLog2.AgregaError(ex);
            }
            return -100;
        }
    }
}