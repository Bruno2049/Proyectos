using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.Log;
using PAEEEM.Entidades.Alta_Solicitud;

namespace PAEEEM.LogicaNegocios.LOG
{
    public class ConsultaHistoricoBL
    {
        public List<HistoricoCredito> ObtenHistoricoCredito(string idCredito)
        {
            var lstHistorico = new ConsultaHistoricos().ObtenHistoricoCredito(idCredito);

            return lstHistorico;
        }
    }
}
