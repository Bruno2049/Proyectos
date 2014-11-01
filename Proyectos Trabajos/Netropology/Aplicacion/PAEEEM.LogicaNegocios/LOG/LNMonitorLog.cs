using System;
using System.Collections.Generic;
using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Helpers;

namespace PAEEEM.LogicaNegocios.LOG
{
    public class LnMonitorLog
    {
        public static List<CAT_TIPO_PROCESOS> TipoProcesos()
        {
            var listTipoProcesos = new AccesoDatos.Log.DbMonitorLog().ObtieneTiposProcesos();
            return listTipoProcesos;
        }

        public static List<CAT_TAREAS_PROCESOS> TareasProcesos(int idTipoProceso)
        {
          var listTareasProcesos = new AccesoDatos.Log.DbMonitorLog().ObtieneTareasProcesos(idTipoProceso);
            return listTareasProcesos;
        }

        public static List<US_ROL> Roles()
        {
            var listRoles = new AccesoDatos.Log.DbMonitorLog().ObtieneRoles();
            return listRoles;
        }

        public static DataTable Consulta_Log(string fechaInicio, string fechaFin,string nomUser, int? idRol,  int? idProceso,string descTarea)
        {
            return new AccesoDatos.Log.DbMonitorLog().DBConsulta_Log(fechaInicio, fechaFin, nomUser, idRol, idProceso,descTarea);
         
        }
    }
}
