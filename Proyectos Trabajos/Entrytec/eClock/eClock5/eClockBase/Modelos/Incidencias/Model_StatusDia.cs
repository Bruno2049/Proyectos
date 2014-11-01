using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Incidencias
{
    public class Model_StatusDia
    {
        public DateTime Dia { get; set; }
        public string ExtraID { get; set; }
        /// <summary>
        /// 0: Incidencia
        /// 1: Porcentaje de uso guarda en extra id 0 si hay menos permisos que OcupacionAceptable, 
        /// 1 si hay mas permisos que OcupacionAceptable, 2 si hay mas que OcupacionAlerta y 3 si hay mas que OcupacionMinima 
        /// </summary>
        public int Tipo { get; set; }
        public int Color { get; set; }
        public Model_StatusDia()
        {
        }
        public Model_StatusDia(DateTime dtDia, string sExtraID, int iTipo, int iColor)
        {
            Dia = dtDia;
            ExtraID = sExtraID;
            Tipo = iTipo;
            Color = iColor;
        }
    }
}
