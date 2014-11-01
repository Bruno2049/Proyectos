using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.MRV
{
    public class ConsumosDal
    {
        #region Metodos de Consulta

        public List<MRV_CONSULTA_CONSUMOS> ObtenListaConsumos(string idCredito)
        {
            List<MRV_CONSULTA_CONSUMOS> lstConsumos = null;

            using (var r = new Repositorio<MRV_CONSULTA_CONSUMOS>())
            {
                lstConsumos = r.Filtro(me => me.No_Credito == idCredito);
            }

            return lstConsumos;
        }

        public List<MRV_HIST_CONSULTA_CONSUMOS> ObtenHistoricoConsumos(int idConsultaConsumo)
        {
            List<MRV_HIST_CONSULTA_CONSUMOS> lstConsumos = null;

            using (var r = new Repositorio<MRV_HIST_CONSULTA_CONSUMOS>())
            {
                lstConsumos = r.Filtro(me => me.IdConsultaConsumo == idConsultaConsumo);
            }

            return lstConsumos;
        }

        #endregion

        public MRV_CONSULTA_CONSUMOS GuardaConsumo(MRV_CONSULTA_CONSUMOS consultaConsumo)
        {
            MRV_CONSULTA_CONSUMOS newConsultaConsumo = null;

            using (var r = new Repositorio<MRV_CONSULTA_CONSUMOS>())
            {
                newConsultaConsumo = r.Agregar(consultaConsumo);
            }

            return newConsultaConsumo;
        }

        public MRV_HIST_CONSULTA_CONSUMOS GuardaHistoricoConsumo(MRV_HIST_CONSULTA_CONSUMOS consultaHistConsumo)
        {
            MRV_HIST_CONSULTA_CONSUMOS newConsultaHistConsumo = null;

            using (var r = new Repositorio<MRV_HIST_CONSULTA_CONSUMOS>())
            {
                newConsultaHistConsumo = r.Agregar(consultaHistConsumo);
            }

            return newConsultaHistConsumo;
        }
    }
}
