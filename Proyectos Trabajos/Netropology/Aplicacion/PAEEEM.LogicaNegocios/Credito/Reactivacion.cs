using System;
using System.Collections.Generic;
using System.Globalization;
using PAEEEM.AccesoDatos.ReactivarCredito;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.BussinessLayer;
using System.Data;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.AccesoDatos.Catalogos;


namespace PAEEEM.LogicaNegocios.Credito
{
    public class Reactivacion
    {
        private static readonly Reactivacion _classInstance = new Reactivacion();
        public static Reactivacion ClassInstance { get { return _classInstance; } }

        public List<DATOS_REACTIVACION> ObtenerDatos(string rpu, string noCre, string client, string nomComer, int reg, int zon)
        {
            var datos = DatosRequeridos.ClassInstance.DatosObtenidos(rpu, noCre, client, nomComer, reg, zon);

            return datos;
        }

        ///metodos para reglas de mayor o menor a 30 dias 
        ///
        public static bool Menor30Dias(string credit, string usu)
        {
            
            var cre = DatosRequeridos.ObtienePorId(credit);
            bool reactivacion;
            try
            {
                /////////
                GeneraNuevaTablaAmortizacion(cre, cre.No_Credito);

                cre.Cve_Estatus_Credito = (byte?)(cre.Fecha_Autorizado == null ? (cre.Fecha_Pre_Autorizado == null ? (cre.Fecha_En_revision == null ? (cre.Fecha_Por_entregar == null ? 1 : 2) : 3) : 11) : 4);

                cre.Fecha_Ultmod = DateTime.Now;
                cre.Usr_Ultmod = usu;
                cre.No_Reactivaciones = cre.No_Reactivaciones == null ? (byte)1 : Convert.ToByte( cre.No_Reactivaciones+1);

                reactivacion = DatosRequeridos.Actualizar(cre);
            }
            catch (Exception e)
            {
                reactivacion = false;
                var error = "Ocurrió un error al Reactivar. Motivo:" +
                            e.Message.ToString(CultureInfo.InvariantCulture);
            }
            return reactivacion;
        }

        public static bool Mayor30Dias(string credit, string user)
        {
            var cre = DatosRequeridos.ObtienePorId(credit);

            GeneraNuevaTablaAmortizacion(cre, cre.No_Credito);

            //se keda en estatus pendiente 
            cre.Fecha_Autorizado = null;
            cre.Fecha_En_revision = null;
            cre.Fecha_Por_entregar = null;
            cre.Cve_Estatus_Credito = 1;
            //se borra consulta crediticia 
            cre.Fecha_Consulta = null;
            cre.Folio_Consulta = null;
            cre.No_MOP = null;
            cre.Fecha_Ultmod = DateTime.Now;
            cre.Usr_Ultmod = user;
            cre.No_Reactivaciones = cre.No_Reactivaciones == null ? (byte)1 : Convert.ToByte(cre.No_Reactivaciones + 1);

            var reactivacion = DatosRequeridos.Actualizar(cre);

            return reactivacion;

        }

        public static bool SinConsulCrediticia(string credit, string usu)
        {
            var cre = DatosRequeridos.ObtienePorId(credit);
            var reactivacion = false;
            if (cre == null || cre.Fecha_Consulta != null)
            {
                return false;
            }
            try
            {
                GeneraNuevaTablaAmortizacion(cre, cre.No_Credito);
                cre.Fecha_Autorizado = null;
                cre.Fecha_En_revision = null;
                cre.Fecha_Por_entregar = null;
                cre.Cve_Estatus_Credito = 1;
                cre.Fecha_Ultmod = DateTime.Now;
                cre.Usr_Ultmod = usu;
                cre.No_Reactivaciones = cre.No_Reactivaciones == null ?(byte) 1 : Convert.ToByte(cre.No_Reactivaciones + 1);

                reactivacion = DatosRequeridos.Actualizar(cre);
            }
            catch (Exception e)
            {
               var error = "Ocurrió un error al Reactivar. Motivo:" + e.Message.ToString(CultureInfo.InvariantCulture);
            }

            return reactivacion;
        }

        public static bool Masd30DiasSnActividad(string credit, string usu)
        {
            var reactivar = false;
            var cre = DatosRequeridos.ObtienePorId(credit);
            //
           
            //Más de 30 días sin actividad (solo en estatus pendiente), se volverá a reiniciar la fecha de pendiente
            try
            {
                GeneraNuevaTablaAmortizacion(cre, cre.No_Credito);

                cre.Fecha_Pendiente = DateTime.Now;
                cre.Cve_Estatus_Credito = 1;
                cre.Fecha_Ultmod = DateTime.Now;
                cre.Usr_Ultmod = usu;
                cre.No_Reactivaciones = cre.No_Reactivaciones == null ? (byte)1 : Convert.ToByte(cre.No_Reactivaciones + 1);

                reactivar = DatosRequeridos.Actualizar(cre);
            }
            catch (Exception e)
            {
                var validacion = "Ocurrió un error al Reactivar. Motivo: " +
                             e.Message.ToString(CultureInfo.InvariantCulture);
            }

            return reactivar;
        }

        public static void NumMop(string credit, string usu)
        {
            var cre = DatosRequeridos.ObtienePorId(credit);
            if (!(cre.No_MOP > 4) || cre.No_MOP == null) return;
            try
            {
                cre.Fecha_Calificación_MOP_no_válida = DateTime.Now;
                cre.Fecha_Ultmod = DateTime.Now;
                cre.Usr_Ultmod = usu;

                var actualiza = DatosRequeridos.Actualizar(cre);

            }
            catch (Exception exception)
            {
            }
        }
        public static bool HayMotivosCancelacion(string noCredito)
        {
            return DatosRequeridos.ClassInstance.ExisteMotivosCancelacion(noCredito);
        }


        public static void GeneraNuevaTablaAmortizacion(CRE_Credito credito, string idCreditoActual)
        {
            var diasFinanzas = VariablesGlobales.ObtienePorIdValor(19, 17);
            var lstAmortizaciones = new CreCredito().ObtenAmortizacionesCredito(idCreditoActual);
            var fechaPrimerPago = lstAmortizaciones.Find(me => me.No_Pago == 1 && me.No_Credito == idCreditoActual).Dt_Fecha;
            var fechaSegundoPago = lstAmortizaciones.Find(me => me.No_Pago == 2 && me.No_Credito == idCreditoActual).Dt_Fecha;

            //var prueba = Convert.ToDateTime("2014-10-02");
            var quinceDias = fechaPrimerPago.Value.AddDays(-diasFinanzas);


            var fechaActual = DateTime.Now.Date;

            if (quinceDias <  fechaActual)
            {
                //string creditNumber, double montoSolicitado, double pctTasaFija, int plazoPago, int periodo, double pctTasaInteres, double pctTasaIVA, double pctCAT, DateTime startDate
                DateTime? fechaPago = null;

                foreach (var kCreditoAmortizacion in lstAmortizaciones.FindAll(me => me.No_Pago != 1))
                {
                    if (kCreditoAmortizacion.No_Pago != 1)
                    {
                        var diasVencimiento = kCreditoAmortizacion.Dt_Fecha.Value.AddDays(-diasFinanzas);

                        if (diasVencimiento >= fechaActual)
                        {
                            fechaPago = kCreditoAmortizacion.Dt_Fecha.Value;
                            break;
                        }
                    }
                }

                DataTable CreditAmortizacionDt =
                K_CREDITOBll.ClassInstance.CalculateCreditAmortizacionReactiva(credito.No_Credito,
                                                                       Convert.ToDouble(credito.Monto_Solicitado),
                                                                       (double)credito.Tasa_Fija / 100,
                                                                       (int)credito.No_Plazo_Pago,
                                                                       (int)credito.Cve_Periodo_Pago,
                                                                       (double)credito.Tasa_Interes / 100,
                                                                       (double)credito.Tasa_IVA_Intereses / 100,
                                                                       (double)credito.CAT, (DateTime)fechaPago);

                if (CreditAmortizacionDt != null)
                {
                    new CreCredito().EliminaAmortizacioncredito(idCreditoActual);

                    var creditoAmortizacion = new OpEquiposAbEficiencia().InsertaAmortizacionesCredito(
                        CreditAmortizacionDt, credito.No_Credito);
                }


            }

        }


      

    }
}
