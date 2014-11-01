using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos.MRV;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.Entidades.Trama;

namespace PAEEEM.LogicaNegocios.MRV
{
    public class ConsumosLN
    {
        public List<MRV_CONSULTA_CONSUMOS> ObtenListaConsumos(string idCredito)
        {
            var resultado = new ConsumosDal().ObtenListaConsumos(idCredito);

            return resultado;
        }

        public MRV_CONSULTA_CONSUMOS GuardaConsumo(MRV_CONSULTA_CONSUMOS consultaConsumo)
        {
            var resultado = new ConsumosDal().GuardaConsumo(consultaConsumo);

            return resultado;
        }

        public List<MRV_HIST_CONSULTA_CONSUMOS> ObtenHistoricoConsumos(int idConsultaConsumo)
        {
            var resultado = new ConsumosDal().ObtenHistoricoConsumos(idConsultaConsumo);

            return resultado;
        }

        public void GuardaHistoricoConsumos(List<CompHistorialDetconsumo> lstHistorico, int idConsultaConsumo,
                                            string usuario)
        {
            var lsthistDetConsumo = lstHistorico.FindAll(me => me.Fecha > Convert.ToDateTime("01/01/0001"));

            foreach (var compHistorialDetconsumo in lsthistDetConsumo)
            {
                var historico = new MRV_HIST_CONSULTA_CONSUMOS();
                historico.IdConsultaConsumo = idConsultaConsumo;
                historico.idHistorico = Convert.ToByte(compHistorialDetconsumo.IdHistorial);
                historico.FechaPeriodo = compHistorialDetconsumo.Fecha;
                historico.Consumo = compHistorialDetconsumo.Consumo;
                historico.Demanda = compHistorialDetconsumo.Demanda;
                historico.FactorPotencia = compHistorialDetconsumo.FactorPotencia;
                historico.IdValido = Convert.ToByte(compHistorialDetconsumo.Id);
                historico.Estatus = true;
                historico.FechaAdicion = DateTime.Now.Date;
                historico.AdicionadoPor = usuario;

                new ConsumosDal().GuardaHistoricoConsumo(historico);
            }
        }

        public void CopiaHistoricosCredito(string idCredito, int idConsultaConsumo, string usuario)
        {
            var lstHistoricoConsumoCredito = new CreCredito().ObtenHistoricoConsumo(idCredito);

            if (lstHistoricoConsumoCredito.Count > 0)
            {
                foreach (var creHistoricoConsumo in lstHistoricoConsumoCredito)
                {
                    var historico = new MRV_HIST_CONSULTA_CONSUMOS();
                    historico.IdConsultaConsumo = idConsultaConsumo;
                    historico.idHistorico = creHistoricoConsumo.IdHistorial;
                    historico.FechaPeriodo = creHistoricoConsumo.Fecha_Periodo;
                    historico.Consumo = creHistoricoConsumo.Consumo;
                    historico.Demanda = creHistoricoConsumo.Demanda;
                    historico.FactorPotencia = creHistoricoConsumo.FactorPotencia;
                    historico.IdValido = creHistoricoConsumo.IdValido;
                    historico.Estatus = true;
                    historico.FechaAdicion = DateTime.Now.Date;
                    historico.AdicionadoPor = usuario;

                    new ConsumosDal().GuardaHistoricoConsumo(historico);
                }
            }
        }
    }
}
