using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;
using PAEEEM.Helpers;
using PAEEEM.Entidades.ModuloCentral;


namespace PAEEEM.AccesoDatos.SolicitudCredito
{

    public class CREDITO_DAL
    {
        private static readonly CREDITO_DAL _classInstance = new CREDITO_DAL();

        public static CREDITO_DAL ClassInstance
        {
            get { return _classInstance; }
        }

        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public CREDITO_DAL()
        {
        }

        public K_CREDITO BuscaCredito(string noCredito)
        {
            K_CREDITO credito;

            using (var r = new Repositorio<K_CREDITO>())
            {
                credito = r.Extraer(me => me.No_Credito == noCredito);
            }
             
            return credito;
        }

        public DatosCredito GetDatosCredito(string noCredito)
        {
            var datos = (from credito in _contexto.CRE_Credito
                         join estatus in _contexto.CAT_ESTATUS_CREDITO on credito.Cve_Estatus_Credito equals
                             estatus.Cve_Estatus_Credito
                         join periodoPago in _contexto.CAT_PERIODO_PAGO on credito.Cve_Periodo_Pago equals
                             periodoPago.Cve_Periodo_Pago
                         join cliente in _contexto.CLI_Cliente on credito.IdCliente equals cliente.IdCliente
                         join dir in _contexto.DIR_Direcciones on cliente.IdCliente equals dir.IdCliente
                         join estado in _contexto.CAT_ESTADO on dir.Cve_Estado equals estado.Cve_Estado
                         join munc in _contexto.CAT_DELEG_MUNICIPIO on dir.Cve_Deleg_Municipio equals munc.Cve_Deleg_Municipio
                         join colonia in _contexto.CAT_CODIGO_POSTAL_SEPOMEX on dir.CVE_CP equals colonia.Cve_CP
                         where credito.No_Credito == noCredito && dir.IdTipoDomicilio == 2 && dir.IdNegocio == credito.IdNegocio
                         select new DatosCredito
                         {
                             IdProgProy = credito.ID_Prog_Proy,
                             RFC = cliente.RFC,
                             PeriodoPago = periodoPago.Dx_Periodo_Pago,
                             CveEstatusCredito = estatus.Cve_Estatus_Credito,
                             MontoSolicitado = credito.Monto_Solicitado,
                             NoPlazoPago = credito.No_Plazo_Pago,
                             RazonSocial = cliente.Razon_Social,
                             CveTipoSociedad = cliente.Cve_Tipo_Sociedad,
                             CveCc = estado.Dx_Cve_CC,
                             CvePm = estado.Dx_Cve_PM,
                             Colonia = colonia.Dx_Colonia,
                             Estado = estado.Dx_Nombre_Estado,
                             DelegMunicipio = munc.Dx_Deleg_Municipio,
                             Calle = dir.Calle,
                             NumExt = dir.Num_Ext,
                             TipoDomicilio = dir.IdTipoDomicilio,
                             Nombre = cliente.Nombre,
                             ApPaterno = cliente.Ap_Paterno,
                             ApMaterno = cliente.Ap_Materno,
                             FecNacimiento = cliente.Fec_Nacimiento,
                             CodigoPostal = dir.CP,
                             FechaCalificacionMopNoValida = credito.Fecha_Calificación_MOP_no_válida,
                             NoMop = credito.No_MOP
                         }).ToList().FirstOrDefault();

            return datos;
        }

        public static CAT_PROGRAMA getDatosCat_Programa(int idProgProy)
        {
            CAT_PROGRAMA datosCatPrograma;

            using (var r = new Repositorio<CAT_PROGRAMA>())
            {
                datosCatPrograma = r.Extraer(me => me.ID_Prog_Proy == idProgProy);
            }
            return datosCatPrograma;
        }

        public int AddCredit(EntidadCredito credito)
        {
            int iResult = 0;

            try
            {
                //cliente

                var cli = new CLI_Cliente();

                cli.Cve_Tipo_Sociedad = byte.Parse(credito.Cve_Tipo_Sociedad.ToString());
                cli.Razon_Social = credito.Dx_Razon_Social;
                cli.RFC = credito.Dx_RFC;
                cli.CURP = credito.Dx_CURP;
                cli.Nombre = credito.Dx_Nombre_Repre_Legal;
                cli.Genero = byte.Parse(credito.Fg_Sexo_Repre_legal.ToString());
                cli.Cve_Estado_Civil = byte.Parse(credito.Fg_Edo_Civil_Repre_legal.ToString());
                cli.IdRegimenConyugal = byte.Parse(credito.Cve_Reg_Conyugal_Repre_legal.ToString());
                cli.email = credito.Dx_Email_Repre_legal;

                //datos de la tabla _auxiliar
                cli.Nombre = credito.Dx_First_Name_Aval;
                cli.Ap_Paterno = credito.Dx_Last_Name_Aval;
                cli.Ap_Materno = credito.Dx_Mother_Name_Aval;
                cli.Fec_Nacimiento = DateTime.Parse(credito.Dt_BirthDate_Aval.ToString());

                beCliente.Insertar(cli);

                //Negocio

                var neg = new CLI_Negocio();

                neg.Cve_Tipo_Industria = credito.Cve_Tipo_Industria;
                neg.Nombre_Comercial = credito.Dx_Nombre_Comercial;
                neg.IdTipoAcreditacion = byte.Parse(credito.Cve_Acreditacion_Repre_legal.ToString());
                neg.IdTipoIdentificacion = byte.Parse(credito.Cve_Identificacion_Repre_legal.ToString());
                neg.Numero_Identificacion = credito.Dx_No_Identificacion_Repre_Legal;
                neg.Ventas_Mes = decimal.Parse(credito.Mt_Ventas_Mes_Empresa.ToString());
                neg.Gastos_Mes = decimal.Parse(credito.Mt_Gastos_Mes_Empresa.ToString());
                neg.Ingreso_Neto = decimal.Parse(credito.Mt_Ingreso_Neto_Mes_Empresa.ToString());
                neg.Cve_Sector = byte.Parse(credito.Cve_Sector_economico.ToString());
                neg.Numero_Empleados = credito.No_Empleados;

                //Referencias cliente

                var rcli = new CLI_Ref_Cliente();

                rcli.Nombre = credito.Dx_Nombre_Aval;
                rcli.RFC = (credito.Dx_RFC_CURP_Aval.Length <= 13 ? credito.Dx_RFC_CURP_Aval : "");
                rcli.CURP = (credito.Dx_RFC_CURP_Aval.Length > 13 ? credito.Dx_RFC_CURP_Aval : "");
                rcli.Telefono_Local = credito.Dx_Tel_Aval;
                rcli.Genero = byte.Parse(credito.Fg_Sexo_Aval.ToString());
                rcli.Ventas_Mes = decimal.Parse(credito.Mt_Ventas_Mes_Aval.ToString());
                rcli.Gastos_Mes = decimal.Parse(credito.Mt_Gastos_Mes_Aval.ToString());
                rcli.Ingreso_Neto = decimal.Parse(credito.Mt_Ingreso_Neto_Mes_Aval.ToString());
                //rcli.RPU = credito.No_RPU_AVAL;

                beRefCliente.Insertar(rcli);

                //referencias notariales

                var rn = new CLI_Referencias_Notariales();

                rn.Numero_Escritura = credito.Dx_No_Escritura_Aval;
                rn.Fecha_Escritura = DateTime.Parse(credito.Dt_Fecha_Escritura_Aval.ToString());
                rn.Nombre_Notario = credito.Dx_Nombre_Notario_Escritura_Aval;
                rn.Numero_Notaria = credito.Dx_No_Notario_Escritura_Aval;
                rn.Estado = byte.Parse(credito.Cve_Estado_Escritura_Aval.ToString());
                rn.Municipio = byte.Parse(credito.Cve_Deleg_Municipio_Escritura_Aval.ToString());
                rn.Numero_Escritura = credito.Dx_No_Escritura_Poder;
                rn.Fecha_Escritura = DateTime.Parse(credito.Dt_Fecha_Poder.ToString());
                rn.Nombre_Notario = credito.Dx_Nombre_Notario_Poder;
                rn.Numero_Notaria = credito.Dx_No_Notario_Poder;
                rn.Estado = byte.Parse(credito.Cve_Estado_Poder.ToString());
                rn.Municipio = byte.Parse(credito.Cve_Deleg_Municipio_Poder.ToString());
                rn.Numero_Escritura = credito.Dx_No_Escritura_Acta;
                rn.Fecha_Escritura = DateTime.Parse(credito.Dt_Fecha_Acta.ToString());
                rn.Nombre_Notario = credito.Dx_Nombre_Notario_Acta;
                rn.Numero_Notaria = credito.Dx_No_Notario_Acta;
                rn.Estado = byte.Parse(credito.Cve_Estado_Escritura_Aval.ToString()); //Cve_Estado_Acta
                rn.Municipio = byte.Parse(credito.Cve_Deleg_Municipio_Acta.ToString());

                beRefNotariales.Insertar(rn);

                //credito

                var cre = new CRE_Credito();

                cre.No_Credito = credito.No_Credito;
                cre.RPU = credito.No_RPU;
                cre.ID_Prog_Proy = byte.Parse(credito.ID_Prog_Proy.ToString());
                cre.Id_Proveedor = credito.Id_Proveedor;
                cre.Cve_Estatus_Credito = byte.Parse(credito.Cve_Estatus_Credito.ToString());
                cre.No_Ahorro_Energetico = credito.No_Ahorro_Energetico;
                cre.No_Ahorro_Economico = decimal.Parse(credito.No_Ahorro_Economico.ToString());
                cre.Monto_Solicitado = decimal.Parse(credito.Mt_Monto_Solicitado.ToString());
                cre.Monto_Total_Pagar = decimal.Parse(credito.Mt_Monto_Total_Pagar.ToString());
                cre.Capacidad_Pago = decimal.Parse(credito.Mt_Capacidad_Pago.ToString());
                cre.No_Plazo_Pago = byte.Parse(credito.No_Plazo_Pago.ToString());
                cre.Cve_Periodo_Pago = byte.Parse(credito.Cve_Periodo_Pago.ToString());
                cre.Tasa_Interes = credito.Pct_Tasa_Interes;
                cre.Tasa_Fija = credito.Pct_Tasa_Fija;
                cre.CAT = credito.Pct_CAT;
                cre.Tasa_IVA = credito.Pct_Tasa_IVA;
                cre.Adquisicion_Sust = byte.Parse(credito.Fg_Adquisicion_Sust.ToString());
                cre.Fecha_Cancelado = DateTime.Parse(credito.Dt_Fecha_Cancelado.ToString());
                cre.Fecha_Pendiente = DateTime.Parse(credito.Dt_Fecha_Pendiente.ToString());
                cre.Fecha_Por_entregar = DateTime.Parse(credito.Dt_Fecha_Por_entregar.ToString());
                cre.Fecha_En_revision = DateTime.Parse(credito.Dt_Fecha_En_revision.ToString());
                cre.Fecha_Autorizado = DateTime.Parse(credito.Dt_Fecha_Autorizado.ToString());
                cre.Fecha_Rechazado = DateTime.Parse(credito.Dt_Fecha_Rechazado.ToString());
                cre.Fecha_Finanzas = DateTime.Parse(credito.Dt_Fecha_Finanzas.ToString());
                cre.Fecha_Ultmod = DateTime.Parse(credito.Dt_Fecha_Ultmod.ToString());
                cre.Usr_Ultmod = credito.Dx_Usr_Ultmod;
                cre.Cancel_Sistema = "";
                cre.Tasa_IVA_Intereses = credito.Pct_Tasa_IVA_Intereses;
                cre.Fecha_Beneficiario_con_adeudos = DateTime.Parse(credito.Dt_Fecha_Beneficiario_con_adeudos.ToString());
                cre.Fecha_Tarifa_fuera_de_programa = DateTime.Parse(credito.Dt_Fecha_Tarifa_fuera_de_programa.ToString());
                cre.No_consumo_promedio = credito.No_consumo_promedio;
                cre.Tipo_Usuario = credito.Tipo_Usuario;
                cre.Fecha_Calificación_MOP_no_válida =
                    DateTime.Parse(credito.Dt_Fecha_Calificación_MOP_no_válida.ToString());
                cre.ID_Intelisis = "";
                cre.ATB01 = "";
                cre.ATB02 = "";
                cre.ATB03 = "";
                cre.ATB04 = "";
                cre.ATB05 = "";
                cre.Fecha_Pago_Intelisis = null;
                cre.Fecha_Liberacion_Intelisis = null;
                cre.Afectacion_SICOM_fecha = null;
                cre.Afectacion_SICOM_DigitoVer = null;
                cre.Afectacion_SICOM_DigitoVerOk = null;
                cre.Afectacion_SICOM_Sufijo = "";
                cre.Afectacion_SIRCA_Fecha = null;
                cre.Afectacion_SIRCA_Digito = "";
                cre.Gastos_Instalacion =
                    decimal.Parse(credito.Mt_Gastos_Instalacion_Mano_Obra.ToString());

                beCredito.Insertar(cre);

                //direcciones

                var dir = new DIR_Direcciones();

                dir.Calle = credito.Dx_Domicilio_Fisc_Calle;
                dir.Num_Ext = credito.Dx_Domicilio_Fisc_Num;
                dir.Colonia = credito.Dx_Domicilio_Fisc_Colonia;
                dir.CP = credito.Dx_Domicilio_Fisc_CP;
                dir.Cve_Estado = credito.Cve_Estado_Fisc;
                dir.Cve_Deleg_Municipio = credito.Cve_Deleg_Municipio_Fisc;
                dir.Cve_Tipo_Propiedad = credito.Cve_Tipo_Propiedad_Fisc;
                dir.Telefono_Local = credito.Dx_Tel_Fisc;
                dir.Calle = credito.Dx_Domicilio_Neg_Calle;
                dir.Num_Ext = credito.Dx_Domicilio_Neg_Num;
                dir.Colonia = credito.Dx_Domicilio_Neg_Colonia;
                dir.CP = credito.Dx_Domicilio_Neg_CP;
                dir.Cve_Estado = credito.Cve_Estado_Neg;
                dir.Cve_Deleg_Municipio = credito.Cve_Deleg_Municipio_Neg;
                dir.Cve_Tipo_Propiedad = credito.Cve_Tipo_Propiedad_Neg;
                dir.Telefono_Local = credito.Dx_Tel_Neg;
                dir.Calle = credito.Dx_Domicilio_Aval_Calle;
                dir.Num_Ext = credito.Dx_Domicilio_Aval_Num;
                dir.Colonia = credito.Dx_Domicilio_Aval_Colonia;
                dir.CP = credito.Dx_Domicilio_Aval_CP;
                dir.Cve_Estado = credito.Cve_Estado_Aval;
                dir.Cve_Deleg_Municipio = credito.Cve_Deleg_Municipio_Aval;

                beDireccion.Insertar(dir);

            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error al insertar los datos del crédito.", ex, true);
            }

            return iResult;
        }

        public int updateCreditAccountFilter(EntidadCredito credito)
        {
            int iUpdate = 0;

            try
            {
                var cre = beCredito.Obtener(credito.No_Credito);

                cre.Monto_Solicitado = decimal.Parse(credito.Mt_Monto_Solicitado.ToString());
                cre.No_Ahorro_Economico = decimal.Parse(credito.No_Ahorro_Economico.ToString());
                cre.Capacidad_Pago = decimal.Parse(credito.Mt_Capacidad_Pago.ToString());

                beCredito.Actualizar(cre);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Error al actualizar los montos del crédito.", ex, true);
            }
            return iUpdate;
        }

        public int UpdateCredit(EntidadCredito credito)
        {
            int iResult = 0;
            
            try
            {
                var cre = beCredito.Obtener(credito.No_Credito);
                var cli = beCliente.Obtener(int.Parse(cre.IdCliente.ToString()));
                var rnPoder = new CLI_Referencias_Notariales();
                var rnActa = new CLI_Referencias_Notariales();
                var rnAval = new CLI_Referencias_Notariales();
                var neg = new CLI_Negocio();

                cre.Monto_Solicitado = decimal.Parse(credito.Mt_Monto_Solicitado.ToString());
                cre.No_Ahorro_Economico = decimal.Parse(credito.No_Ahorro_Economico.ToString());
                cre.Capacidad_Pago = decimal.Parse(credito.Mt_Capacidad_Pago.ToString());
                cre.Cve_Estatus_Credito = byte.Parse(credito.Cve_Estatus_Credito.ToString());
                cre.Adquisicion_Sust = byte.Parse(credito.Fg_Adquisicion_Sust.ToString());
                cre.Fecha_Por_entregar = DateTime.Parse(credito.Dt_Fecha_Por_entregar.ToString());
                cre.Usr_Ultmod = credito.Dx_Usr_Ultmod;
                cre.Fecha_Ultmod = DateTime.Parse(credito.Dt_Fecha_Ultmod.ToString());

                beCredito.Actualizar(cre);

                neg.Cve_Sector = byte.Parse(credito.Cve_Sector_economico.ToString());
                neg.Numero_Empleados = credito.No_Empleados;

                beNegocio.Actualizar(neg);

                rnPoder.IdTipoReferencia = 6; //poder notarial representante legal
                rnPoder.Numero_Escritura = credito.Dx_No_Escritura_Poder;
                rnPoder.Fecha_Escritura = DateTime.Parse(credito.Dt_Fecha_Poder.ToString());
                rnPoder.Nombre_Notario = credito.Dx_Nombre_Notario_Poder;
                rnPoder.Numero_Notaria = credito.Dx_No_Notario_Poder;
                rnPoder.Estado = byte.Parse(credito.Cve_Estado_Poder.ToString());
                rnPoder.Municipio = byte.Parse(credito.Cve_Deleg_Municipio_Poder.ToString());

                beRefNotariales.Actualizar(rnPoder);

                rnActa.Numero_Escritura = credito.Dx_No_Escritura_Acta;
                rnActa.Fecha_Escritura = DateTime.Parse(credito.Dt_Fecha_Acta.ToString());
                rnActa.Nombre_Notario = credito.Dx_Nombre_Notario_Acta;
                rnActa.Numero_Notaria = credito.Dx_No_Notario_Acta;
                rnActa.Estado = byte.Parse(credito.Cve_Estado_Acta.ToString());
                rnActa.Municipio = byte.Parse(credito.Cve_Deleg_Municipio_Acta.ToString());

                beRefNotariales.Actualizar(rnActa);

                rnAval.Numero_Escritura = credito.Dx_No_Escritura_Aval;
                rnAval.Fecha_Escritura = DateTime.Parse(credito.Dt_Fecha_Escritura_Aval.ToString());
                rnAval.Nombre_Notario = credito.Dx_Nombre_Notario_Escritura_Aval;
                rnAval.Numero_Notaria = credito.Dx_No_Notario_Escritura_Aval;
                rnAval.Estado = byte.Parse(credito.Cve_Estado_Escritura_Aval.ToString());
                rnAval.Municipio = byte.Parse(credito.Cve_Deleg_Municipio_Escritura_Aval.ToString());

                beRefNotariales.Actualizar(rnAval);

            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error al actualizar los datos del crédito.", ex, true);
            }
            return iResult;
        }

        public int CancelCredit(string creditNumber, int statusID, DateTime cancelDate, string updateUser, DateTime userDate, string connString)
        {
            int iResult = 0;

            try
            {
                var cre = beCredito.Obtener(creditNumber);

                cre.Cve_Estatus_Credito = byte.Parse(statusID.ToString());
                cre.Fecha_Cancelado = cancelDate;
                cre.Usr_Ultmod = updateUser;
                cre.Fecha_Ultmod = userDate;

                beCredito.Actualizar(cre);

            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error al cancelar el crédito.", ex, false);
            }

            return iResult;
        }

        public int CancelCredit(string creditNumber, string updateUser)
        {
            int iResult = 0;
            
            try
            {
                var cre = beCredito.Obtener(creditNumber);

                cre.Cve_Estatus_Credito = (int) CreditStatus.CANCELADO;
                cre.Fecha_Cancelado = DateTime.Now.Date;
                cre.Usr_Ultmod = updateUser;
                cre.Fecha_Ultmod = DateTime.Now.Date;

                beCredito.Actualizar(cre);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error al cancelar el crédito.", ex, false);
            }

            return iResult;
        }

        public int EnRevisionCredit(string creditNumber, int statusID, DateTime enRevisionDate, string updateUser, DateTime userDate)
        {
            int iResult = 0;
            
            try
            {
                var cre = beCredito.Obtener(creditNumber);

                cre.Cve_Estatus_Credito = byte.Parse(statusID.ToString());
                cre.Fecha_En_revision = enRevisionDate;
                cre.Usr_Ultmod = updateUser;
                cre.Fecha_Ultmod = userDate;

                beCredito.Actualizar(cre);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error de crédito en revisión.", ex, true);
            }

            return iResult;
        }

        public int AprobarCredit(string creditNumber, int statusID, DateTime aprovarDate, string updateUser, DateTime userDate)
        {
            int iResult = 0;
            
            try
            {
                var cre = beCredito.Obtener(creditNumber);

                cre.Cve_Estatus_Credito = byte.Parse(statusID.ToString());
                cre.Fecha_Autorizado = aprovarDate;
                cre.Usr_Ultmod = updateUser;
                cre.Fecha_Ultmod = userDate;

                beCredito.Actualizar(cre);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error al aprobar el crédito.", ex, true);
            }

            return iResult;
        }

        public int RechazarCredit(string creditNumber, int statusID, DateTime rechazarDate, string updateUser, DateTime userDate)
        {
            int iResult = 0;
            
            try
            {
                var cre = beCredito.Obtener(creditNumber);

                cre.Cve_Estatus_Credito = byte.Parse(statusID.ToString());
                cre.Fecha_Rechazado = rechazarDate;
                cre.Usr_Ultmod = updateUser;
                cre.Fecha_Ultmod = userDate;

                beCredito.Actualizar(cre);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error al rechazar el crédito.", ex, true);
            }

            return iResult;
        }

        public int PendienteCredit(string creditNumber, int statusID, DateTime pendienteDate, string updateUser, DateTime userDate)
        {
            int iResult = 0;
            
            try
            {
                var cre = beCredito.Obtener(creditNumber);

                cre.Cve_Estatus_Credito = byte.Parse(statusID.ToString());
                cre.Fecha_Pendiente = pendienteDate;
                cre.Usr_Ultmod = updateUser;
                cre.Fecha_Ultmod = userDate;

                beCredito.Actualizar(cre);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error al pasar a pendiente el crédito.", ex, true);
            }

            return iResult;
        }

        public int CalificacionMopNoValidCredit(string creditNumber, int statusID, DateTime NoValidDate, string updateUser, DateTime userDate)
        {
            int iResult = 0;
            
            try
            {
                var cre = beCredito.Obtener(creditNumber);

                cre.Cve_Estatus_Credito = byte.Parse(statusID.ToString());
                cre.Fecha_Calificación_MOP_no_válida = NoValidDate;
                cre.Usr_Ultmod = updateUser;
                cre.Fecha_Ultmod = userDate;

                beCredito.Actualizar(cre);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error al calificar como no válido el crédito.", ex, true);
            }

            return iResult;
        }

        public int UpdateCreditApproveInfo(EntidadCredito credito)
        {
            int iResult = 0;

            try
            {
                var cre = beCredito.Obtener(credito.No_Credito);
                var cli = beCliente.Obtener(int.Parse(cre.IdCliente.ToString()));

                //credito
                cre.Fecha_Ultmod = DateTime.Parse(credito.Dt_Fecha_Ultmod.ToString());
                cre.Usr_Ultmod = credito.Dx_Usr_Ultmod;

                //cliente
                cli.Razon_Social = credito.Dx_Razon_Social;
                cli.Cve_Tipo_Sociedad = byte.Parse(credito.Cve_Tipo_Sociedad.ToString());
                cli.CURP = credito.Dx_CURP;
                cli.Nombre = credito.Dx_Nombre_Repre_Legal;
                cli.Genero = byte.Parse(credito.Fg_Sexo_Repre_legal.ToString());
                cli.Cve_Estado_Civil = byte.Parse(credito.Fg_Edo_Civil_Repre_legal.ToString());
                cli.IdRegimenConyugal =
                    byte.Parse(credito.Cve_Reg_Conyugal_Repre_legal.ToString());
                cli.email = credito.Dx_Email_Repre_legal;

                //negocio
                var neg = new CLI_Negocio();

                neg.IdTipoIdentificacion = byte.Parse(credito.Cve_Identificacion_Repre_legal.ToString());
                neg.Numero_Identificacion = credito.Dx_No_Identificacion_Repre_Legal;
                neg.Ventas_Mes = decimal.Parse(credito.Mt_Ventas_Mes_Empresa.ToString());
                neg.Gastos_Mes = decimal.Parse(credito.Mt_Gastos_Mes_Empresa.ToString());
                neg.Cve_Tipo_Industria = credito.Cve_Tipo_Industria;
                neg.IdTipoAcreditacion = byte.Parse(credito.Cve_Acreditacion_Repre_legal.ToString());
                neg.Ventas_Mes = decimal.Parse(credito.Mt_Ventas_Mes_Aval.ToString());
                neg.Gastos_Mes = decimal.Parse(credito.Mt_Gastos_Mes_Aval.ToString());
                neg.Cve_Sector = byte.Parse(credito.Cve_Sector_economico.ToString());
                neg.Numero_Empleados = credito.No_Empleados;

                //fiscal
                var dir = beDireccion.Obtener(int.Parse(cre.IdCliente.ToString()), 2);

                dir.Calle = credito.Dx_Domicilio_Fisc_Calle;
                dir.Num_Ext = credito.Dx_Domicilio_Fisc_Num;
                dir.CP = credito.Dx_Domicilio_Fisc_CP;
                dir.Cve_Estado = byte.Parse(credito.Cve_Estado_Fisc.ToString());
                dir.Cve_Deleg_Municipio = credito.Cve_Deleg_Municipio_Fisc;
                dir.Cve_Tipo_Propiedad = credito.Cve_Tipo_Propiedad_Fisc;
                dir.Telefono_Local = credito.Dx_Tel_Fisc;

                //negocio
                var dir2 = beDireccion.Obtener(int.Parse(cre.IdCliente.ToString()), 1);

                dir.RPU = credito.No_RPU;
                dir2.Calle = credito.Dx_Domicilio_Neg_Calle;
                dir2.Num_Ext = credito.Dx_Domicilio_Neg_Num;
                dir2.CP = credito.Dx_Domicilio_Neg_CP;
                dir2.Cve_Estado = credito.Cve_Estado_Neg;
                dir2.Cve_Deleg_Municipio = credito.Cve_Deleg_Municipio_Neg;
                dir2.IdTipoDomicilio = byte.Parse(credito.Cve_Tipo_Propiedad_Neg.ToString());
                dir2.Telefono_Local = credito.Dx_Tel_Neg;

                //obligado solidario
                var dir3 = beDireccion.Obtener(int.Parse(cre.IdCliente.ToString()), 3);

                dir3.Calle = credito.Dx_Domicilio_Aval_Calle;
                dir3.Num_Ext = credito.Dx_Domicilio_Aval_Num;
                dir3.CP = credito.Dx_Domicilio_Aval_CP;
                dir3.Cve_Estado = credito.Cve_Estado_Aval;
                dir3.Cve_Deleg_Municipio = credito.Cve_Deleg_Municipio_Aval;

                //obligado solidario
                var rcli = beRefCliente.Obtener(int.Parse(cre.IdCliente.ToString()), 2);

                rcli.Nombre = credito.Dx_Nombre_Aval;
                rcli.RFC = (credito.Dx_RFC_CURP_Aval.Length <= 13 ? credito.Dx_RFC_CURP_Aval : null);
                rcli.CURP = (credito.Dx_RFC_CURP_Aval.Length > 13 ? credito.Dx_RFC_CURP_Aval : null);
                rcli.Telefono_Local = credito.Dx_Tel_Aval;
                rcli.Genero = byte.Parse(credito.Fg_Sexo_Aval.ToString());

                beCredito.Actualizar(cre);
                beCliente.Actualizar(cli);
                beNegocio.Actualizar(neg);
                beDireccion.Actualizar(dir);
                beDireccion.Actualizar(dir2);
                beDireccion.Actualizar(dir3);
                beRefCliente.Actualizar(rcli);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error actualizando los datos del crédito aprobado.", ex,
                    true);
            }
            return iResult;
        }

        public int UpdateCreditReview(EntidadCredito credito)
        {
            int iResult = 0;
            
            try
            {
                //credito
                var cre = beCredito.Obtener(credito.No_Credito);

                cre.No_consumo_promedio = credito.No_consumo_promedio;
                cre.Monto_Solicitado = decimal.Parse(credito.Mt_Monto_Solicitado.ToString());
                cre.Cve_Periodo_Pago = byte.Parse(credito.Cve_Periodo_Pago.ToString());
                cre.No_Ahorro_Energetico = credito.No_Ahorro_Energetico;
                cre.No_Ahorro_Economico = decimal.Parse(credito.No_Ahorro_Economico.ToString());
                cre.Capacidad_Pago = decimal.Parse(credito.Mt_Capacidad_Pago.ToString());
                cre.Monto_Total_Pagar = decimal.Parse(credito.Mt_Monto_Total_Pagar.ToString());
                cre.Fecha_Beneficiario_con_adeudos = DateTime.Parse(credito.Dt_Fecha_Beneficiario_con_adeudos.ToString());
                cre.Cve_Estatus_Credito = byte.Parse(credito.Cve_Estatus_Credito.ToString());
                cre.Fecha_Tarifa_fuera_de_programa = DateTime.Parse(credito.Dt_Fecha_Tarifa_fuera_de_programa.ToString());
                cre.No_Plazo_Pago = byte.Parse(credito.No_Plazo_Pago.ToString());
                cre.Fecha_Ultmod = DateTime.Parse(credito.Dt_Fecha_Ultmod.ToString());
                cre.Usr_Ultmod = credito.Dx_Usr_Ultmod;
                cre.ID_Prog_Proy = byte.Parse(credito.ID_Prog_Proy.ToString());
                cre.Id_Proveedor = credito.Id_Proveedor;
                cre.Tasa_Interes = credito.Pct_Tasa_Interes;
                cre.Tasa_Fija = credito.Pct_Tasa_Fija;
                cre.CAT = credito.Pct_CAT;
                cre.Tasa_IVA = credito.Pct_Tasa_IVA;
                cre.Tasa_IVA_Intereses = credito.Pct_Tasa_IVA_Intereses;
                cre.Tipo_Usuario = credito.Tipo_Usuario;
                cre.Gastos_Instalacion = decimal.Parse(credito.Mt_Gastos_Instalacion_Mano_Obra.ToString());

                beCredito.Actualizar(cre);

                //cliente
                var cli = beCliente.Obtener(int.Parse(cre.IdCliente.ToString()));

                cli.Razon_Social = credito.Dx_Razon_Social;
                cli.Cve_Tipo_Sociedad = byte.Parse(credito.Cve_Tipo_Sociedad.ToString());
                cli.CURP = credito.Dx_CURP;
                cli.RFC = credito.Dx_RFC;
                cli.Nombre = credito.Dx_Nombre_Repre_Legal;
                cli.Genero = byte.Parse(credito.Fg_Sexo_Repre_legal.ToString());
                cli.Cve_Estado_Civil = byte.Parse(credito.Fg_Edo_Civil_Repre_legal.ToString());
                cli.email = credito.Dx_Email_Repre_legal;
                cli.IdRegimenConyugal = byte.Parse(credito.Cve_Reg_Conyugal_Repre_legal.ToString());
                cli.Telefono_Local = credito.Telephone;
                cli.email = credito.Email;

                beCliente.Actualizar(cli);
                
                //negocio
                var neg = new CLI_Negocio();

                neg.Cve_Tipo_Industria = credito.Cve_Tipo_Industria;
                neg.IdTipoAcreditacion = byte.Parse(credito.Cve_Acreditacion_Repre_legal.ToString());
                neg.IdTipoIdentificacion = byte.Parse(credito.Cve_Identificacion_Repre_legal.ToString());
                neg.Numero_Identificacion = credito.Dx_No_Identificacion_Repre_Legal;
                neg.Ventas_Mes = decimal.Parse(credito.Mt_Ventas_Mes_Empresa.ToString());
                neg.Gastos_Mes = decimal.Parse(credito.Mt_Gastos_Mes_Empresa.ToString());
                neg.Nombre_Comercial = credito.Dx_Nombre_Comercial;
                neg.Ingreso_Neto = decimal.Parse(credito.Mt_Ingreso_Neto_Mes_Empresa.ToString());

                beNegocio.Actualizar(neg);

                //obligado solidario
                var rcli = beRefCliente.Obtener(cli.IdCliente, 2);

                rcli.Nombre = credito.Dx_Nombre_Aval;
                rcli.Telefono_Local = credito.Dx_Tel_Aval;
                rcli.Genero = byte.Parse(credito.Fg_Sexo_Aval.ToString());
                rcli.Ventas_Mes = decimal.Parse(credito.Mt_Ventas_Mes_Aval.ToString());
                rcli.Gastos_Mes = decimal.Parse(credito.Mt_Gastos_Mes_Aval.ToString());
                rcli.Ingreso_Neto = decimal.Parse(credito.Mt_Ingreso_Neto_Mes_Aval.ToString());
                //rcli.RPU = credito.No_RPU_AVAL;

                beRefCliente.Actualizar(rcli);

                //fiscal
                var dir = beDireccion.Obtener(cli.IdCliente, 2);

                dir.Calle = credito.Dx_Domicilio_Fisc_Calle;
                dir.Num_Ext = credito.Dx_Domicilio_Fisc_Num;
                dir.CP = credito.Dx_Domicilio_Fisc_CP;
                dir.Colonia = credito.Dx_Domicilio_Fisc_Colonia;
                dir.Cve_Estado = credito.Cve_Estado_Fisc;
                dir.Cve_Deleg_Municipio = credito.Cve_Deleg_Municipio_Fisc;
                dir.Cve_Tipo_Propiedad = credito.Cve_Tipo_Propiedad_Fisc;
                dir.Telefono_Local = credito.Dx_Tel_Fisc;

                beDireccion.Actualizar(dir);

                //negocio
                var dirn = beDireccion.Obtener(cli.IdCliente, 1);

                dirn.Calle = credito.Dx_Domicilio_Fisc_Calle;
                dir.RPU = credito.No_RPU;
                dirn.Num_Ext = credito.Dx_Domicilio_Fisc_Num;
                dirn.CP = credito.Dx_Domicilio_Fisc_CP;
                dirn.Colonia = credito.Dx_Domicilio_Fisc_Colonia;
                dirn.Cve_Estado = credito.Cve_Estado_Fisc;
                dirn.Cve_Deleg_Municipio = credito.Cve_Deleg_Municipio_Fisc;
                dirn.Cve_Tipo_Propiedad = credito.Cve_Tipo_Propiedad_Fisc;
                dirn.Telefono_Local = credito.Dx_Tel_Fisc;

                beDireccion.Actualizar(dirn);

                //obligado solidario
                var diro = beDireccion.Obtener(cli.IdCliente, 3);

                diro.Calle = credito.Dx_Domicilio_Fisc_Calle;
                diro.Num_Ext = credito.Dx_Domicilio_Fisc_Num;
                diro.CP = credito.Dx_Domicilio_Fisc_CP;
                diro.Colonia = credito.Dx_Domicilio_Fisc_Colonia;
                diro.Cve_Estado = credito.Cve_Estado_Fisc;
                diro.Cve_Deleg_Municipio = credito.Cve_Deleg_Municipio_Fisc;
                diro.Cve_Tipo_Propiedad = credito.Cve_Tipo_Propiedad_Fisc;
                diro.Telefono_Local = credito.Dx_Tel_Fisc;

                beDireccion.Actualizar(diro);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error actualizando review del crédito.", ex,
                    true);
            }
            return iResult;
        }



        ////add by @l3x /////
        public List<DatosConsulta> consultasCrediticias(string noCredit)
        {

            var getrfc = (from cre in _contexto.CRE_Credito
                            join cli in _contexto.CLI_Cliente on new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals new { CP = (int)cli.Id_Proveedor, CB = (int)cli.Id_Branch, CC = (int)cli.IdCliente }
                            where cre.No_Credito==noCredit

                            select new DatosConsulta
                            {
                                RFC = cli.RFC,
                                RPU = cre.RPU,
                                No_Credito = cre.No_Credito,
                            }).ToList();
            
            string rpu=getrfc[0].RPU, rfc=getrfc[0].RFC;

         var consutasCredi = (from cre in _contexto.CRE_Credito
                             join cli in _contexto.CLI_Cliente on new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals new { CP = (int)cli.Id_Proveedor, CB = (int)cli.Id_Branch, CC = (int)cli.IdCliente }
                              where cre.RPU == rpu && cli.RFC == rfc 

                             select new DatosConsulta
                             {
                                 RFC = cli.RFC,
                                 RPU = cre.RPU,
                                 No_Credito = cre.No_Credito,
                                 no_consultasCrediticias = (int)cre.No_Consultas_Crediticias == null ? 0 : (int)cre.No_Consultas_Crediticias
                             }).ToList();

         return consutasCredi;
        }
        ////<-------------/////
    }
}
