using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.Entidades.Alta_Solicitud;

namespace PAEEEM.LogicaNegocios.SolicitudCredito
{
    public class CapturaAuxilialBL
    {
        public List<AUX_HISTORIAL_CONSUMO> ObtenHistorialConsumoAuxiliar(string rpu)
        {
            var lstHistoricoConsumo = new CapturaAuxiliarDal().ObtenHistorialConsumoAuxiliar(rpu);

            return lstHistoricoConsumo;
        }

        public AUX_HISTORIAL_CONSUMO ObtenRegistroHisConsumo(string rpu, int idHistorial)
        {
            var auxHistConsumo = new CapturaAuxiliarDal().ObtenRegistroHisConsumo(rpu, idHistorial);

            return auxHistConsumo;
        }

        public int ObtenIDhistorial(string rpu)
        {
            var lstHistoricoConsumo = new CapturaAuxiliarDal().ObtenHistorialConsumoAuxiliar(rpu);

            if (lstHistoricoConsumo.Count > 0)
            {
                return lstHistoricoConsumo.Max(me => me.IdHistorial) + 1;
            }
            else
            {
                return 1;
            }
        }

        public AUX_HISTORIAL_CONSUMO InsertaHistoricoConsumo(AUX_HISTORIAL_CONSUMO auxHistorial)
        {
            var auxHist = new CapturaAuxiliarDal().InsertaHistoricoConsumo(auxHistorial);

            return auxHist;
        }

        public bool ActalizaHistoricoConsumo(AUX_HISTORIAL_CONSUMO auxHistorial)
        {
            return new CapturaAuxiliarDal().ActalizaHistoricoConsumo(auxHistorial);
        }

        public bool EliminaHistoricoConsumo(AUX_HISTORIAL_CONSUMO auxHistorial)
        {
            return new CapturaAuxiliarDal().EliminaHistoricoConsumo(auxHistorial);
        }

        public AUX_DATOS_TRAMA InsertaDatosTrama(AUX_DATOS_TRAMA auxDatosTrama)
        {
            var newAuxDatosTrama = new CapturaAuxiliarDal().InsertaDatosTrama(auxDatosTrama);

            return newAuxDatosTrama;
        }

        public bool ActualizaDatosAuxTrama(AUX_DATOS_TRAMA auxDatosTrama)
        {
            return new CapturaAuxiliarDal().ActualizaDatosAuxTrama(auxDatosTrama);
        }

        public AUX_DATOS_TRAMA ObtenDatosAuxTrama(string rpu)
        {
            var datosAuxTrama = new CapturaAuxiliarDal().ObtenDatosAuxTrama(rpu);

            return datosAuxTrama;
        }

        public List<CapturaAuxiliar> ObtenCapturasDistribuidor(string usuario)
        {
            var lstCapturaAux = new CapturaAuxiliarDal().ObtenCapturasDistribuidor(usuario);

            if (lstCapturaAux.Count > 0)
            {
                foreach (var capturaAuxiliar in lstCapturaAux)
                {
                    if (capturaAuxiliar.CveEstatus == 1)
                    {
                        var fechaHoy = DateTime.Now.Date;
                        var fechaVence = capturaAuxiliar.FechaAdicion.AddDays(7);

                        var vigencia = fechaVence.Date - fechaHoy;

                        if (vigencia.Days <= 0)
                        {
                            var datosAuxTrama = new CapturaAuxiliarDal().ObtenDatosAuxTrama(capturaAuxiliar.Rpu);
                            datosAuxTrama.EstatusCapturaAux = 2;
                            datosAuxTrama.Vigencia = 0;
                            new CapturaAuxilialBL().ActualizaDatosAuxTrama(datosAuxTrama);

                            capturaAuxiliar.Estatus = "CANCELADO";
                        }
                        else
                        {
                            capturaAuxiliar.Vigencia = Convert.ToByte(vigencia.Days);
                            capturaAuxiliar.Estatus = "ACTIVO";
                        }
                    }
                    else
                    {
                        capturaAuxiliar.Estatus = "CANCELADO";
                        capturaAuxiliar.Vigencia = 0;
                    }
                }
            }

            return lstCapturaAux;
        }

        public List<CapturaAuxiliar> ObtenCapturasDistribuidorFiltro(string usuario, int estatusCaptura, int tarifa, string rpu)
        {
            var lstCapturaAux = new CapturaAuxiliarDal().ObtenCapturasDistribuidor(usuario);

            if (lstCapturaAux.Count > 0)
            {
                if (estatusCaptura != 0)
                    lstCapturaAux = lstCapturaAux.FindAll(me => me.CveEstatus == estatusCaptura);
                if (tarifa != 0)
                    lstCapturaAux = lstCapturaAux.FindAll(me => me.CveTarifa == tarifa);
                if (!string.IsNullOrEmpty(rpu))
                    lstCapturaAux = lstCapturaAux.FindAll(me => me.Rpu == rpu);

                foreach (var capturaAuxiliar in lstCapturaAux)
                {
                    if (capturaAuxiliar.CveEstatus == 1)
                    {
                        var fechaHoy = DateTime.Now.Date;
                        var fechaVence = capturaAuxiliar.FechaAdicion.AddDays(7);

                        var vigencia = fechaVence.Date - fechaHoy;

                        if (vigencia.Days <= 0)
                        {
                            var datosAuxTrama = new CapturaAuxiliarDal().ObtenDatosAuxTrama(capturaAuxiliar.Rpu);
                            datosAuxTrama.EstatusCapturaAux = 2;
                            datosAuxTrama.Vigencia = 0;
                            new CapturaAuxilialBL().ActualizaDatosAuxTrama(datosAuxTrama);

                            capturaAuxiliar.Estatus = "CANCELADO";
                        }
                        else
                        {
                            capturaAuxiliar.Vigencia = Convert.ToByte(vigencia.Days);
                            capturaAuxiliar.Estatus = "ACTIVO";
                        }
                    }
                    else
                    {
                        capturaAuxiliar.Estatus = "CANCELADO";
                        capturaAuxiliar.Vigencia = 0;
                    }
                }
            }

            return lstCapturaAux;
        }
    }
}
