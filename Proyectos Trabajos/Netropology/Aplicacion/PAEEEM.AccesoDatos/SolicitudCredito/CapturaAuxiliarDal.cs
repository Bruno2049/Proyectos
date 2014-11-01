using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;

namespace PAEEEM.AccesoDatos.SolicitudCredito
{
    public class CapturaAuxiliarDal
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public List<AUX_HISTORIAL_CONSUMO> ObtenHistorialConsumoAuxiliar(string rpu)
        {
            List<AUX_HISTORIAL_CONSUMO> lstHistorialConsumo = null;

            using (var r = new Repositorio<AUX_HISTORIAL_CONSUMO>())
            {
                lstHistorialConsumo = r.Filtro(me => me.Rpu == rpu);
            }

            return lstHistorialConsumo;

        }

        public AUX_HISTORIAL_CONSUMO ObtenRegistroHisConsumo(string rpu, int idHistorial)
        {
            using (var r = new Repositorio<AUX_HISTORIAL_CONSUMO>())
            {
                return r.Extraer(me => me.Rpu == rpu && me.IdHistorial == idHistorial);
            }
        }

        public AUX_HISTORIAL_CONSUMO InsertaHistoricoConsumo(AUX_HISTORIAL_CONSUMO auxHistorial)
        {
            AUX_HISTORIAL_CONSUMO newAuxHistorial = null;

            using (var r = new Repositorio<AUX_HISTORIAL_CONSUMO>())
            {
                newAuxHistorial = r.Agregar(auxHistorial);
            }

            return newAuxHistorial;
        }

        public bool ActalizaHistoricoConsumo(AUX_HISTORIAL_CONSUMO auxHistorial)
        {
            bool actualiza = false;

            using (var r = new Repositorio<AUX_HISTORIAL_CONSUMO>())
            {
                actualiza = r.Actualizar(auxHistorial);
            }

            return actualiza;
        }

        public bool EliminaHistoricoConsumo(AUX_HISTORIAL_CONSUMO auxHistorial)
        {
            bool elimina = false;

            using (var r = new Repositorio<AUX_HISTORIAL_CONSUMO>())
            {
                elimina = r.Eliminar(auxHistorial);
            }

            return elimina;
        }

        public AUX_DATOS_TRAMA ObtenDatosAuxTrama(string rpu)
        {
            AUX_DATOS_TRAMA auxTrama = null;

            using (var r = new Repositorio<AUX_DATOS_TRAMA>())
            {
                auxTrama = r.Extraer(me => me.Rpu == rpu);
            }

            return auxTrama;
        }

        public AUX_DATOS_TRAMA InsertaDatosTrama(AUX_DATOS_TRAMA auxDatosTrama)
        {
            using (var r = new Repositorio<AUX_DATOS_TRAMA>())
            {
                return r.Agregar(auxDatosTrama);
            }
        }

        public bool ActualizaDatosAuxTrama(AUX_DATOS_TRAMA auxDatosTrama)
        {
            using (var r = new Repositorio<AUX_DATOS_TRAMA>())
            {
                return r.Actualizar(auxDatosTrama);
            }
        } 

        public bool EliminaDatosAuxTrama(AUX_DATOS_TRAMA auxDatosTrama)
        {
            using (var r = new Repositorio<AUX_DATOS_TRAMA>())
            {
                return r.Eliminar(auxDatosTrama);
            }
        }

        public List<CapturaAuxiliar> ObtenCapturasDistribuidor(string usuario)
        {
            var resultado = (from captura in _contexto.AUX_DATOS_TRAMA
                             join tarifa in _contexto.CAT_TARIFA
                                 on captura.Cve_Tarifa equals tarifa.Cve_Tarifa
                             join periodo in _contexto.CAT_PERIODO_PAGO
                                 on captura.Cve_Periodo_Pago equals periodo.Cve_Periodo_Pago
                             where captura.AdicionadoPor == usuario
                             select new CapturaAuxiliar
                                 {
                                     Rpu = captura.Rpu,
                                     Nombre =
                                         captura.Nombres + " " + captura.ApellidoPaterno + " " + captura.ApellidoMaterno,
                                     CveTarifa = (int) captura.Cve_Tarifa,
                                     Tarifa = tarifa.Dx_Tarifa,
                                     Cuenta = captura.Cuenta,
                                     CvePeriodoPago = (int) captura.Cve_Periodo_Pago,
                                     PeriodoPago = periodo.Dx_Periodo_Pago,
                                     TotalPeriodos = (byte) captura.TotalPeriodos,
                                     CveEstatus = (byte)captura.EstatusCapturaAux,
                                     FechaAdicion = (DateTime)captura.FechaAdicion,
                                     AdicionadoPor = captura.AdicionadoPor
                                 }
                            ).ToList();

            return resultado;
        }
    }
}
