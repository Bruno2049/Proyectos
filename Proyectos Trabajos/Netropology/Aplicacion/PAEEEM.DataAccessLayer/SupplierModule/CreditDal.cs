/*
	Copyright IMPRA, Inc. 2010
	All rights are reserved. Reproduction or transmission in whole or in part,
      in any form or by any means, electronic, mechanical or otherwise, is 
prohibited without the prior written consent of the copyright owner.

	$Archive:    $
	$Revision:   $
	$Author:     $
	$Date:       $
	Log at end of file
*/

using System;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Credit Data Access Layer used to manipulate K_CREDITO table
    /// </summary>
    public class K_CREDITODal
    {
        /// <summary>
        /// Readonly variable for class instance
        /// </summary>
        private static readonly K_CREDITODal _classInstance = new K_CREDITODal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static K_CREDITODal ClassInstance { get { return _classInstance; } }
        /// <summary>
        /// Add credit
        /// </summary>
        /// <param name="CreditModel">Credit Model Object</param>
        /// <returns></returns>
       
        public int AddCredit(EntidadCredito credito)
        {
            int iResult = 0;

            try
            {
                var usr = ((US_USUARIO)(HttpContext.Current.Session["User"]));

                //cliente

                var cli = new CLI_Cliente();

                cli.Id_Proveedor = credito.Id_Proveedor;
                cli.Id_Branch = 0;
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

                neg.Id_Proveedor = credito.Id_Proveedor;
                neg.Id_Branch = 0;
                neg.IdCliente = cli.IdCliente;
                neg.IdTipoAcreditacion = byte.Parse(credito.Cve_Acreditacion_Repre_legal.ToString());
                neg.IdTipoIdentificacion = byte.Parse(credito.Cve_Identificacion_Repre_legal.ToString());
                neg.Cve_Tipo_Industria = credito.Cve_Tipo_Industria;
                neg.Nombre_Comercial = credito.Dx_Nombre_Comercial;
                neg.Numero_Identificacion = credito.Dx_No_Identificacion_Repre_Legal;
                neg.Ventas_Mes = decimal.Parse(credito.Mt_Ventas_Mes_Empresa.ToString());
                neg.Gastos_Mes = decimal.Parse(credito.Mt_Gastos_Mes_Empresa.ToString());
                neg.Ingreso_Neto = decimal.Parse(credito.Mt_Ingreso_Neto_Mes_Empresa.ToString());
                neg.Cve_Sector = byte.Parse(credito.Cve_Sector_economico.ToString());
                neg.Numero_Empleados = credito.No_Empleados;

                beNegocio.Insertar(neg);

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
                cre.Id_Proveedor = credito.Id_Proveedor;
                cre.Id_Branch = 0;
                cre.IdCliente = cli.IdCliente;
                cre.IdNegocio = neg.IdNegocio;
                cre.RPU = credito.No_RPU;
                cre.ID_Prog_Proy = byte.Parse(credito.ID_Prog_Proy.ToString());
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

                dir.Id_Proveedor = credito.Id_Proveedor;
                dir.Id_Branch = 0;
                dir.IdCliente = cli.IdCliente;
                dir.IdNegocio = neg.IdNegocio;
                dir.IdTipoDomicilio = 1;  //negocio
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
                dir.RPU = credito.No_RPU;

                beDireccion.Insertar(dir);

                iResult = 1;

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

                iUpdate = beCredito.Actualizar(cre) ? 1 : 0;
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
                var rnPoder = beRefNotariales.Obtener(cli.IdCliente, 6);
                var rnActa = beRefNotariales.Obtener(cli.IdCliente, 7);
                var rnAval = beRefNotariales.Obtener(cli.IdCliente, 2);
                var neg = beNegocio.Obtener(credito.No_Credito);

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

                iResult = beRefNotariales.Actualizar(rnAval) ? 1 : 0;

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

                iResult = beCredito.Actualizar(cre) ? 1 : 0;

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

                    cre.Cve_Estatus_Credito = (int)CreditStatus.CANCELADO;
                    cre.Fecha_Cancelado = DateTime.Now.Date;
                    cre.Usr_Ultmod = updateUser;
                    cre.Fecha_Ultmod = DateTime.Now.Date;

                iResult = beCredito.Actualizar(cre) ? 1 : 0;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error al cancelar el crédito.", ex, false);
            }

            return iResult;
        }

        ////add by @l3x////
        public int RechazarCredit(string creditNumber, string updateUser)
        {
            int iResult = 0;

            try
            {
                var cre = beCredito.Obtener(creditNumber);

                cre.Cve_Estatus_Credito = (int)CreditStatus.PORENTREGAR;
                cre.Fecha_Rechazado = DateTime.Now.Date;
                cre.Usr_Ultmod = updateUser;
                cre.Fecha_Ultmod = DateTime.Now.Date;
                cre.Fecha_En_revision = null;
                cre.Fecha_Autorizado = null;

                iResult = beCredito.Actualizar(cre) ? 1 : 0;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error al cancelar el crédito.", ex, false);
            }

            return iResult;
        }
        ////<---------////

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

                iResult = beCredito.Actualizar(cre) ? 1 : 0;
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

                iResult = beCredito.Actualizar(cre) ? 1 : 0;
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

                iResult = beCredito.Actualizar(cre) ? 1 : 0;
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

                iResult = beCredito.Actualizar(cre) ? 1 : 0;
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

                iResult = beCredito.Actualizar(cre) ? 1 : 0;
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
                var neg = beNegocio.Obtener(credito.No_Credito);

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

                iResult = 1;
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
                var neg = beNegocio.Obtener(credito.No_Credito);

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

                iResult = 1;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Error actualizando review del crédito.", ex,
                    true);
            }
            return iResult;
        }

        /// <summary>
        /// Get credits with where clause
        /// </summary>
        /// <param name="proveedor">Proveedor ID</param>
        /// <param name="userType">Credit Created User Type</param>
        /// <param name="pendingDate">pending status date</param>
        /// <param name="creditStatus">Credit Status</param>
        /// <param name="razonSocial">Razón Social Name</param>
        /// <param name="technology">Technology</param>
        /// <param name="sortName">Sort name</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageCount">page Count</param>
        /// <returns></returns>
        public DataTable GetCredits(int proveedor, string userType, string pendingDate, int creditStatus, string razonSocial, int technology, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            StringBuilder WhereBuilder = new StringBuilder();

            try
            {
                WhereBuilder.Append(" WHERE 1 = 1");

                if (proveedor > 0 && !string.IsNullOrEmpty(userType))
                {
                    if (userType == GlobalVar.SUPPLIER)
                    {
                        WhereBuilder.Append(" and ([Id_Proveedor] = " + proveedor +
                            " and [Tipo_Usuario] = '" + userType +
                            "' OR (Id_Branch in (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Id_Proveedor = " + proveedor +
                            ") AND Tipo_Usuario = 'S_B'))");
                    }
                    else
                    {
                        WhereBuilder.Append(" and [Id_Branch] = " +
                            proveedor + " and [Tipo_Usuario] = '" + userType + "'");
                    }
                }

                if (!string.IsNullOrEmpty(pendingDate))
                {
                    WhereBuilder.Append(" and [Dt_Fecha_Pendiente] = '" + pendingDate + "'");
                }

                if (creditStatus > 0)
                {
                    WhereBuilder.Append(" and [Cve_Estatus_Credito] = " + creditStatus);
                }

                if (!string.IsNullOrEmpty(razonSocial))
                {
                    WhereBuilder.Append(" and [Dx_Razon_Social] = '" + razonSocial + "'");
                }

                if (technology > 0)
                {
                    // WhereBuilder.Append(" and [No_Credito] IN (SELECT K_CREDITO_SUSTITUCION.No_Credito FROM K_CREDITO_SUSTITUCION WHERE K_CREDITO_SUSTITUCION.Cve_Tecnologia =  " + technology + ")");
                    WhereBuilder.Append(" and [No_Credito] IN (SELECT [No_Credito] FROM K_CREDITO_PRODUCTO k INNER JOIN CAT_PRODUCTO p (nolock) ON k.Cve_Producto = p.Cve_Producto WHERE p.Cve_Tecnologia = " + technology + ")");
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@spWhere", WhereBuilder.ToString()),
                    new SqlParameter("@SortName", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize)
                };

                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_credits", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credits failed: Execute method GetCredits in CreditDal.", ex, true);
            }

            return dtResult;
        }

        /// <summary>
        /// Get credits without where clause
        /// </summary>
        /// <param name="proveedor">Proveedor ID</param>
        /// <param name="userType">Credit Created User Type</param>
        /// <param name="sortName">Sort Column</param>
        /// <param name="pageIndex">page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageCount">page Count</param>
        /// <returns></returns>
        public DataTable GetCredits(int proveedor, string userType, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            try
            {
                string strWhere = "";
                if (proveedor > 0 && !string.IsNullOrEmpty(userType))
                {
                    if (userType == GlobalVar.SUPPLIER)
                    {
                        strWhere = "Where (Id_Proveedor = " + proveedor + " and Tipo_Usuario = '" + userType + "' OR (Id_Branch in (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Id_Proveedor = " + proveedor + ") AND Tipo_Usuario = 'S_B'))";
                    }
                    else
                    {
                        strWhere = "Where ID_BRANCH = " + proveedor + " and Tipo_Usuario = '" + userType + "'";
                    }
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@spWhere", strWhere),
                    new SqlParameter("@SortName", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize)
                };
                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_credits", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credits failed: Execute method GetCredits in CreditDal.", ex, true);
            }

            return dtResult;
        }

        /// <summary>
        /// Get credits for authorization
        /// </summary>
        /// <param name="role">Role Type</param>
        /// <param name="filter">Filter Condition</param>
        /// <param name="sortName">Sort Name</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetCredits(int role, int filter, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            try
            {
                string strWhere = "";
                if (filter > 0)
                {
                    if (role == (int)UserRole.ZONE)
                    {
                        strWhere = "Where (Id_Proveedor IN (SELECT Id_Proveedor FROM CAT_PROVEEDOR WHERE Cve_Zona = " + filter + ") and Tipo_Usuario = 'S')" +
                                            "OR (Id_Branch IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona = " + filter + ") and Tipo_Usuario = 'S_B')";
                    }
                    else
                    {
                        //strWhere = "Where (Id_Proveedor IN (SELECT Id_Proveedor FROM CAT_PROVEEDOR WHERE Cve_Zona IN " +
                        //           "( SELECT Cve_Zona FROM CAT_ZONA WHERE Cve_Region = " + filter + ")) and Tipo_Usuario = 'S')" +
                        //           "OR (ID_BRANCH IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona IN " +
                        //           "( SELECT Cve_Zona FROM CAT_ZONA WHERE Cve_Region = " + filter + ")) and Tipo_Usuario = 'S_B')";

                        strWhere = "Where ((Id_Proveedor IN (SELECT Id_Proveedor FROM CAT_PROVEEDOR WHERE Cve_Zona IN " +
                                   "( SELECT Cve_Zona FROM CAT_ZONA WHERE Cve_Region = " + filter + ")) and Tipo_Usuario = 'S')" +
                                   "OR (ID_BRANCH IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona IN " +
                                   "( SELECT Cve_Zona FROM CAT_ZONA WHERE Cve_Region = " + filter + ")) and Tipo_Usuario = 'S_B'))";
                    }
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Count", SqlDbType.Int),
                    new SqlParameter("@spWhere", strWhere),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@CurrentPageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize)
                };
                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_credits_authorization", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credits failed: Execute method GetCredits in CreditDal.", ex, true);
            }

            return dtResult;
        }

        /// <summary>
        /// Get credits for authorization with filter conditions
        /// </summary>
        /// <param name="role">Role Type</param>
        /// <param name="filter">Filter Condition</param>
        /// <param name="proveedor">Proveedor</param>
        /// <param name="proveedorBranch">Proveedor Branches</param>
        /// <param name="creditStatus">Credit Status</param>
        /// <param name="technology">Technology</param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetCredits(int role, int filter, int proveedor, int proveedorBranch, int creditStatus, int technology, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            try
            {
                string strWhere = "";
                if (filter > 0)
                {
                    if (role == (int)UserRole.REGIONAL) //for region credits  //changed by Jerry 2011/08/04
                    {
                        if (proveedor <= 0 && proveedorBranch <= 0)
                        {
                            strWhere += "Where ((Id_Proveedor IN (SELECT Id_Proveedor FROM CAT_PROVEEDOR WHERE Cve_Zona IN ( SELECT Cve_Zona FROM CAT_ZONA WHERE Cve_Region = " + filter + ")) and Tipo_Usuario = 'S') " +
                                                "OR (Id_Branch IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona IN ( SELECT Cve_Zona FROM CAT_ZONA WHERE Cve_Region = " + filter + ")) and Tipo_Usuario = 'S_B'))";
                        }
                        else if (proveedor > 0 && proveedorBranch <= 0)
                        {
                            strWhere += "Where Id_Proveedor =" + proveedor + " and Tipo_Usuario = 'S'";
                        }
                        else if (proveedor <= 0 && proveedorBranch > 0)
                        {
                            strWhere += "Where Id_Branch = " + proveedorBranch + " and Tipo_Usuario = 'S_B'";
                        }
                        else
                        {
                            strWhere += "Where ((Id_Proveedor  = " + proveedor + " and Tipo_Usuario = 'S') " +
                                                "OR (Id_Branch  = " + proveedorBranch + " and Tipo_Usuario = 'S_B'))";
                        }
                    }
                    else
                    {
                        if (proveedor <= 0 && proveedorBranch <= 0)
                        {
                            strWhere += "Where ((Id_Proveedor IN (SELECT Id_Proveedor FROM CAT_PROVEEDOR WHERE Cve_Zona = " + filter + ") and Tipo_Usuario = 'S') " +
                                                "OR (Id_Branch IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona = " + filter + ") and Tipo_Usuario = 'S_B'))";
                        }
                        else if (proveedor > 0 && proveedorBranch <= 0)
                        {
                            strWhere += "Where Id_Proveedor =" + proveedor + " and Tipo_Usuario = 'S'";
                        }
                        else if (proveedor <= 0 && proveedorBranch > 0)
                        {
                            strWhere += "Where Id_Branch = " + proveedorBranch + " and Tipo_Usuario = 'S_B'";
                        }
                        else
                        {
                            strWhere += "Where ((Id_Proveedor  = " + proveedor + " and Tipo_Usuario = 'S') " +
                                                "OR (Id_Branch  = " + proveedorBranch + " and Tipo_Usuario = 'S_B'))";
                        }
                    }

                    if (creditStatus > 0)
                    {
                        strWhere += " and Cve_Estatus_Credito = " + creditStatus;
                    }

                    if (technology > 0)
                    {
                        // strWhere += " and ([No_Credito] IN (SELECT K_CREDITO_SUSTITUCION.No_Credito FROM K_CREDITO_SUSTITUCION WHERE K_CREDITO_SUSTITUCION.Cve_Tecnologia =  " + technology + "))";
                        strWhere += " and ([No_Credito] IN (SELECT [No_Credito] FROM K_CREDITO_PRODUCTO k (nolock) INNER JOIN CAT_PRODUCTO p (nolock) ON k.Cve_Producto = p.Cve_Producto WHERE p.Cve_Tecnologia = " + technology + "))";
                    }
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Count", SqlDbType.Int),
                    new SqlParameter("@spWhere", strWhere),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@CurrentPageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize)
                };
                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_credits_authorization", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credits failed: Execute method GetCredits in CreditDal.", ex, true);
            }

            return dtResult;
        }

        /// <summary>
        /// Get credits several fields by pk
        /// </summary>
        /// <param name="CreditNumber">Credit Number</param>
        /// <returns></returns>
        public DataTable GetCredits(string CreditNumber)
        {
            DataTable dtResult = null;
            //string sql = "select K_CREDITO.Dx_Razon_Social,K_CREDITO.Mt_Monto_Solicitado,K_CREDITO.No_Plazo_Pago,K_CREDITO.Fg_Adquisicion_Sust,CAT_PERIODO_PAGO.Dx_Periodo_Pago from K_CREDITO " +
            //            " left outer join CAT_PERIODO_PAGO on K_CREDITO.Cve_Periodo_Pago=CAT_PERIODO_PAGO.Cve_Periodo_Pago where K_CREDITO.No_Credito=@CreditNumber";

            string sql =
                @"SELECT k.*, p.Dx_Periodo_Pago, e.Dx_Cve_CC, m.Dx_Deleg_Municipio, e.Dx_Cve_PM,cli.Cve_Tipo_Sociedad
                FROM CRE_Credito k 
                join CLI_Cliente cli on k.IdCliente = cli.IdCliente
                LEFT OUTER JOIN CAT_PERIODO_PAGO p ON k.Cve_Periodo_Pago = p.Cve_Periodo_Pago 
                LEFT OUTER JOIN DIR_Direcciones d ON d.IdCliente = K.IdCliente 
                LEFT OUTER JOIN CAT_ESTADO e ON d.Cve_Estado = e.Cve_Estado 
                LEFT OUTER JOIN CAT_DELEG_MUNICIPIO m ON d.Cve_Deleg_Municipio = m.Cve_Deleg_Municipio 
                WHERE k.No_Credito = @CreditNumber";
            try
            {
                SqlParameter[] paras = new SqlParameter[] 
                { 
                    new SqlParameter("@CreditNumber", CreditNumber)
                };

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credits failed: Execute method GetCredits in CreditDal.", ex, true);
            }

            return dtResult;
        }

        /// <summary>
        /// Get razón social without parameters
        /// </summary>
        /// <param name="proveedorId">Proveedor ID</param>
        /// <param name="userType">user Type</param>
        /// <returns></returns>
        public DataTable GetRazonSocial(int proveedorId, string userType)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                //SQL = "SELECT DISTINCT Dx_Razon_Social FROM [dbo].[K_CREDITO] WHERE [Id_Proveedor] = @Id_Proveedor AND [Tipo_Usuario] = @Tipo_Usuario";
                SQL = @"
                    SELECT DISTINCT 
	                UPPER(LTRIM(RTRIM(ISNULL(cl.Razon_Social,'') + ISNULL(cl.Nombre,'') + ' ' + ISNULL(cl.Ap_Paterno,'') + ' ' + ISNULL(cl.Ap_Materno,'')))) Dx_Razon_Social  
                    FROM [CRE_CREDITO] c
                    INNER JOIN CLI_NEGOCIO n ON c.IdCliente = n.IdCliente  AND c.Id_Branch = n.Id_Branch  AND c.Id_Proveedor = n.Id_Proveedor
                    INNER JOIN CLI_Cliente cl ON cl.IdCliente = c.IdCliente  AND cl.Id_Branch = c.Id_Branch  AND cl.Id_Proveedor = c.Id_Proveedor  AND n.IdNegocio = n.IdNegocio
                    WHERE c.Id_Proveedor = @Id_Proveedor AND c.Tipo_Usuario = @Tipo_Usuario 
                        AND UPPER(LTRIM(RTRIM(ISNULL(cl.Razon_Social,'') + ISNULL(cl.Nombre,'') + ' ' + ISNULL(cl.Ap_Paterno,'') + ' ' + ISNULL(cl.Ap_Materno,'')))) <> ''";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Proveedor", proveedorId),
                    new SqlParameter("@Tipo_Usuario", userType)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credit razon social failed:Execute method  GetRazonSocial in CreditDal.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// Get pendiente date
        /// </summary>
        /// <param name="proveedorId">Proveedor ID</param>
        /// <param name="userType">user Type</param>
        /// <returns></returns>
        public DataTable GetPendienteDate(int proveedorId, string userType)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = @"
                    SELECT DISTINCT CONVERT(VARCHAR(10),Fecha_Pendiente, 120) Dt_Fecha_Pendiente 
                    FROM [dbo].[CRE_CREDITO]  
                    WHERE [Id_Proveedor] = @Id_Proveedor AND [Tipo_Usuario] = @Tipo_Usuario";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Proveedor", proveedorId),
                    new SqlParameter("@Tipo_Usuario", userType)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credit pendiente date failed:Execute method  GetPendienteDate in CreditDal.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// Get credit with credit number
        /// </summary>
        /// <param name="creditNo"></param>
        /// <returns></returns>
        public DataTable GetCreditsReview(string creditNo)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                //SQL = "SELECT TOP 1  *  FROM  [dbo].[K_CREDITO] where No_Credito = @CreditNo";

                /**/

                #region query duplicados

//                 SELECT
//                    c.No_Credito,
//                    c.ID_Prog_Proy,
//                    c.Id_Proveedor,
//                    c.Cve_Estatus_Credito,
//                    cl.Cve_Tipo_Sociedad,
//                    cl2.Cve_Tipo_Industria Cve_Tipo_Industria,
//                    cl2.Nombre_Comercial Dx_Nombre_Comercial,
//                    cl.Nombre Dx_Nombres,
//                    cl.Ap_Paterno Dx_Apellido_Paterno,
//                    cl.Ap_Materno Dx_Apellido_Materno,
//                    cl.Razon_Social Dx_Razon_Social,
//                    cl.Fec_Nacimiento Dt_Nacimiento_Fecha,
//                    cl.email,
//                    dn.telefono_local, 
//                    cl.RFC Dx_RFC,
//                    cl.CURP Dx_CURP,
//                    cl.IdRegimenConyugal Cl_Regimen_Conyugal,
//                    CASE
//                        WHEN ISNULL(rc.Nombre,'') <> '' 
//                            THEN UPPER(rc.Nombre) + ' ' + ISNULL(cl.Ap_Paterno,'') + ' ' + ISNULL(cl.Ap_Materno,'') 
//                        WHEN rc.Nombre IS NULL AND cl.Razon_Social IS NOT NULL 
//                            THEN UPPER(LTRIM(RTRIM(ISNULL(cl.Razon_Social,'')))) 
//                        WHEN rc.Nombre IS NULL AND cl.Razon_Social IS NULL 
//                            THEN UPPER(LTRIM(RTRIM(ISNULL(cl.Nombre,'') + ' ' + ISNULL(cl.Ap_Paterno,'') + ' ' + ISNULL(cl.Ap_Materno,''))))
//                    END [Dx_Nombre_Repre_Legal],
//                    cl2.IdTipoAcreditacion Cve_Acreditacion_Repre_legal,
//                    cl2.IdTipoIdentificacion Cve_Identificacion_Repre_legal,
//                    cl2.Numero_Identificacion Dx_No_Identificacion_Repre_Legal,
//                    rc.Genero Fg_Sexo_Repre_legal,
//                    c.RPU No_RPU,
//                    cl.Cve_Estado_Civil Fg_Edo_Civil_Repre_legal,
//                    rc.Reg_Conyugal Cve_Reg_Conyugal_Repre_legal,
//                    rc.email Dx_Email_Repre_legal,
//                    df.Calle Dx_Domicilio_Fisc_Calle,
//                    df.Num_Ext Dx_Domicilio_Fisc_Num,
//                    UPPER((SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  df.CVE_CP)) Dx_Domicilio_Fisc_Colonia,
//                    df.CP Dx_Domicilio_Fisc_CP,
//                    df.Cve_Estado Cve_Estado_Fisc,
//                    df.Cve_Deleg_Municipio Cve_Deleg_Municipio_Fisc,
//                    df.Cve_Tipo_Propiedad Cve_Tipo_Propiedad_Fisc,
//                    df.Telefono_Oficina Dx_Tel_Fisc,
//                    dn.Calle Dx_Domicilio_Neg_Calle,
//                    dn.Num_Ext Dx_Domicilio_Neg_Num,
//                    UPPER((SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  dn.CVE_CP)) Dx_Domicilio_Neg_Colonia,
//                    dn.CP Dx_Domicilio_Neg_CP,
//                    dn.Cve_Estado Cve_Estado_Neg,
//                    dn.Cve_Deleg_Municipio [Cve_Deleg_Municipio_Neg],
//                    dn.Cve_Tipo_Propiedad Cve_Tipo_Propiedad_Neg,
//                    dn.Telefono_Local Dx_Tel_Neg,
//                    cl2.Ventas_Mes Mt_Ventas_Mes_Empresa,
//                    cl2.Gastos_Mes Mt_Gastos_Mes_Empresa,
//                    cl2.Ingreso_Neto Mt_Ingreso_Neto_Mes_Empresa,
//                    ra.Nombre +' '+ ra.Ap_Paterno +' '+ ra.Ap_Materno  Dx_Nombre_Aval,
//                    CASE WHEN ra.RFC IS NOT NULL THEN ra.RFC ELSE ra.CURP END Dx_RFC_CURP_Aval,
//                    ra.Telefono_Local Dx_Tel_Aval,
//                    ra.Genero Fg_Sexo_Aval,
//                    da.Calle Dx_Domicilio_Aval_Calle,
//                    da.Num_Ext Dx_Domicilio_Aval_Num,
//                    UPPER(CPda.Dx_Colonia) Dx_Domicilio_Aval_Colonia,
//                    da.CP Dx_Domicilio_Aval_CP,
//                    da.Cve_Estado Cve_Estado_Aval,
//                    da.Cve_Deleg_Municipio Cve_Deleg_Municipio_Aval,
//                    ra.Ventas_Mes Mt_Ventas_Mes_Aval,
//                    ra.Gastos_Mes Mt_Gastos_Mes_Aval,
//                    ra.Ingreso_Neto Mt_Ingreso_Neto_Mes_Aval,
//                    --ra.RPU No_RPU_AVAL,
//                    rn.Numero_Escritura Dx_No_Escritura_Aval,
//                    rn.Fecha_Escritura Dt_Fecha_Escritura_Aval,
//                    rn.Nombre_Notario Dx_Nombre_Notario_Escritura_Aval,
//                    rn.Numero_Notaria Dx_No_Notario_Escritura_Aval,
//                    rn.Estado Cve_Estado_Escritura_Aval,
//                    rn.Municipio Cve_Deleg_Municipio_Escritura_Aval,
//                    pn.Numero_Escritura Dx_No_Escritura_Poder,
//                    pn.Fecha_Escritura Dt_Fecha_Poder,
//                    pn.Nombre_Notario Dx_Nombre_Notario_Poder,
//                    pn.Numero_Notaria Dx_No_Notario_Poder,
//                    pn.Estado Cve_Estado_Poder,
//                    pn.Municipio Cve_Deleg_Municipio_Poder,
//                    ac.Numero_Escritura Dx_No_Escritura_Acta,
//                    ac.Fecha_Escritura Dt_Fecha_Acta,
//                    ac.Nombre_Notario Dx_Nombre_Notario_Acta,
//                    ac.Numero_Notaria Dx_No_Notario_Acta,
//                    ac.Estado Cve_Estado_Acta,
//                    ac.Municipio Cve_Deleg_Municipio_Acta,
//                    c.No_Ahorro_Energetico No_Ahorro_Energetico,
//                    c.No_Ahorro_Economico No_Ahorro_Economico,
//                    c.Monto_Solicitado Mt_Monto_Solicitado,
//                    c.Monto_Total_Pagar Mt_Monto_Total_Pagar,
//                    c.Capacidad_Pago Mt_Capacidad_Pago,
//                    c.No_Plazo_Pago No_Plazo_Pago,
//                    c.Cve_Periodo_Pago Cve_Periodo_Pago,
//                    c.Tasa_IVA_Intereses Pct_Tasa_Interes,
//                    c.Tasa_Fija Pct_Tasa_Fija,
//                    c.CAT Pct_CAT,
//                    c.Tasa_IVA Pct_Tasa_IVA,
//                    c.Adquisicion_Sust Fg_Adquisicion_Sust,
//                    c.Fecha_Cancelado Dt_Fecha_Cancelado,
//                    c.Fecha_Pendiente Dt_Fecha_Pendiente,
//                    c.Fecha_Por_entregar Dt_Fecha_Por_entregar,
//                    c.Fecha_En_revision Dt_Fecha_En_revision,
//                    c.Fecha_Autorizado Dt_Fecha_Autorizado,
//                    c.Fecha_Rechazado Dt_Fecha_Rechazado,
//                    c.Fecha_Finanzas Dt_Fecha_Finanzas,
//                    c.Fecha_Ultmod Dt_Fecha_Ultmod,
//                    c.Usr_Ultmod Dx_Usr_Ultmod,
//                    c.Cancel_Sistema Dx_Cancel_Sistema,
//                    c.Tasa_IVA_Intereses Pct_Tasa_IVA_Intereses,
//                    c.Fecha_Beneficiario_con_adeudos Dt_Fecha_Beneficiario_con_adeudos,
//                    c.Fecha_Tarifa_fuera_de_programa Dt_Fecha_Tarifa_fuera_de_programa,
//                    c.No_consumo_promedio No_consumo_promedio,
//                    c.Tipo_Usuario Tipo_Usuario,
//                    c.Fecha_Calificación_MOP_no_válida Dt_Fecha_Calificación_MOP_no_válida,
//                    c.ID_Intelisis ID_Intelisis,
//                    c.ATB01,
//                    c.ATB02,
//                    c.ATB03,
//                    c.ATB04,
//                    c.ATB05,
//                    c.Fecha_Pago_Intelisis Dt_Fecha_Pago_Intelisis,
//                    c.Fecha_Liberacion_Intelisis Dt_Fecha_Liberacion_Intelisis,
//                    c.Afectacion_SICOM_fecha Afectacion_SICOM_fecha,
//                    c.Afectacion_SICOM_DigitoVer Afectacion_SICOM_DigitoVer,
//                    c.Afectacion_SICOM_DigitoVerOk Afectacion_SICOM_DigitoVerOk,
//                    c.Afectacion_SICOM_Sufijo,
//                    c.Afectacion_SIRCA_Fecha,
//                    c.Afectacion_SIRCA_Digito,
//                    c.Gastos_Instalacion Mt_Gastos_Instalacion_Mano_Obra
//                    ,cl2.Cve_Sector
//                    ,cl2.Numero_Empleados No_Empleados
//                    ,am.Nombre_Notario DX_NOMBRE_COACREDITADO
//                FROM [CRE_Credito] c 
//                    LEFT JOIN	CLI_Cliente cl
//                                    ON	cl.IdCliente = c.IdCliente
//                     LEFT JOIN	CLI_Negocio cl2
//                                    ON	cl2.IdCliente = c.IdCliente
//                    LEFT JOIN CLI_Ref_Cliente rc 
//                                    ON rc.IdCliente = c.IdCliente AND rc.IdTipoReferencia = 1
//                    LEFT JOIN CLI_Ref_Cliente ra 
//                                    ON ra.IdCliente = c.IdCliente  AND ra.IdTipoReferencia = 2
//                    LEFT JOIN DIR_Direcciones dn
//                                    ON dn.IdCliente = c.IdCliente AND dn.IdTipoDomicilio = 1
//                    LEFT JOIN DIR_Direcciones df
//                                    ON df.IdCliente = c.IdCliente AND df.IdTipoDomicilio = 2
//                    LEFT JOIN DIR_Direcciones da
//                                    ON da.IdCliente = c.IdCliente AND da.IdTipoDomicilio = 3
//                    LEFT JOIN CAT_CODIGO_POSTAL_SEPOMEX CpDa 
//                                    ON da.CVE_CP = CpDa.CVE_CP
//                    LEFT JOIN CLI_Referencias_Notariales rn
//                                    ON rn.IdCliente = c.IdCliente AND rn.IdTipoReferencia = 2
//                    LEFT JOIN CLI_Referencias_Notariales pn
//                                    ON pn.IdCliente = c.IdCliente AND pn.IdTipoReferencia = 6
//                    LEFT JOIN CLI_Referencias_Notariales ac
//                                    ON ac.IdCliente = c.IdCliente AND ac.IdTipoReferencia = 7
//                    LEFT JOIN CLI_Referencias_Notariales am
//                                    ON am.IdCliente = c.IdCliente AND am.IdTipoReferencia = 8
//                WHERE No_Credito  = @CreditNo
//                AND cl.Cve_Tipo_Sociedad = 1
//                UNION ALL
//               SELECT
//                    c.No_Credito,
//                    c.ID_Prog_Proy,
//                    c.Id_Proveedor,
//                    c.Cve_Estatus_Credito,
//                    cl.Cve_Tipo_Sociedad,
//                    cl2.Cve_Tipo_Industria Cve_Tipo_Industria,
//                    cl2.Nombre_Comercial Dx_Nombre_Comercial,
//                    cl.Nombre Dx_Nombres,
//                    cl.Ap_Paterno Dx_Apellido_Paterno,
//                    cl.Ap_Materno Dx_Apellido_Materno,
//                    cl.Razon_Social Dx_Razon_Social,
//                    cl.Fec_Nacimiento Dt_Nacimiento_Fecha,
//                    cl.email,
//                    dn.telefono_local, 
//                    cl.RFC Dx_RFC,
//                    cl.CURP Dx_CURP,
//                    cl.IdRegimenConyugal Cl_Regimen_Conyugal,
//                    CASE
//                        WHEN ISNULL(rc.Nombre,'') <> '' 
//                            THEN UPPER(rc.Nombre) +' '+ rc.Ap_Paterno +' '+ rc.Ap_Materno
//                        WHEN rc.Nombre IS NULL AND cl.Razon_Social IS NOT NULL 
//                            THEN UPPER(LTRIM(RTRIM(ISNULL(cl.Razon_Social,'')))) 
//                        WHEN rc.Nombre IS NULL AND cl.Razon_Social IS NULL 
//                            THEN UPPER(LTRIM(RTRIM(ISNULL(cl.Nombre,'') + ' ' + ISNULL(cl.Ap_Paterno,'') + ' ' + ISNULL(cl.Ap_Materno,''))))
//                    END [Dx_Nombre_Repre_Legal],
//                    cl2.IdTipoAcreditacion Cve_Acreditacion_Repre_legal,
//                    cl2.IdTipoIdentificacion Cve_Identificacion_Repre_legal,
//                    cl2.Numero_Identificacion Dx_No_Identificacion_Repre_Legal,
//                    rc.Genero Fg_Sexo_Repre_legal,
//                    c.RPU No_RPU,
//                    cl.Cve_Estado_Civil Fg_Edo_Civil_Repre_legal,
//                    rc.Reg_Conyugal Cve_Reg_Conyugal_Repre_legal,
//                    rc.email Dx_Email_Repre_legal,
//                    df.Calle Dx_Domicilio_Fisc_Calle,
//                    df.Num_Ext Dx_Domicilio_Fisc_Num,
//                    UPPER((SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  df.CVE_CP)) Dx_Domicilio_Fisc_Colonia,
//                    df.CP Dx_Domicilio_Fisc_CP,
//                    df.Cve_Estado Cve_Estado_Fisc,
//                    df.Cve_Deleg_Municipio Cve_Deleg_Municipio_Fisc,
//                    df.Cve_Tipo_Propiedad Cve_Tipo_Propiedad_Fisc,
//                    df.Telefono_Oficina Dx_Tel_Fisc,
//                    dn.Calle Dx_Domicilio_Neg_Calle,
//                    dn.Num_Ext Dx_Domicilio_Neg_Num,
//                    UPPER((SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  dn.CVE_CP)) Dx_Domicilio_Neg_Colonia,
//                    dn.CP Dx_Domicilio_Neg_CP,
//                    dn.Cve_Estado Cve_Estado_Neg,
//                    dn.Cve_Deleg_Municipio [Cve_Deleg_Municipio_Neg],
//                    dn.Cve_Tipo_Propiedad Cve_Tipo_Propiedad_Neg,
//                    dn.Telefono_Local Dx_Tel_Neg,
//                    cl2.Ventas_Mes Mt_Ventas_Mes_Empresa,
//                    cl2.Gastos_Mes Mt_Gastos_Mes_Empresa,
//                    cl2.Ingreso_Neto Mt_Ingreso_Neto_Mes_Empresa,
//                    CASE WHEN ra.Nombre IS NOT NULL THEN ra.Nombre + ' ' + isnull(ra.Ap_Paterno,'') + ' ' + isnull(ra.Ap_Materno,'') ELSE ra.Razon_Social END Dx_Nombre_Aval,
//                    CASE WHEN ra.RFC IS NOT NULL THEN ra.RFC ELSE ra.CURP END Dx_RFC_CURP_Aval,
//                    ra.Telefono_Local Dx_Tel_Aval,
//                    ra.Genero Fg_Sexo_Aval,
//                    da.Calle Dx_Domicilio_Aval_Calle,
//                    da.Num_Ext Dx_Domicilio_Aval_Num,
//--	                CPda.Dx_Colonia Dx_Domicilio_Aval_Colonia,
//                    UPPER((SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  da.CVE_CP)) as Dx_Domicilio_Aval_Colonia,
//                    da.CP Dx_Domicilio_Aval_CP,                    
//                    da.Cve_Estado Cve_Estado_Aval,
//                    da.Cve_Deleg_Municipio Cve_Deleg_Municipio_Aval,
//                    ra.Ventas_Mes Mt_Ventas_Mes_Aval,
//                    ra.Gastos_Mes Mt_Gastos_Mes_Aval,
//                    ra.Ingreso_Neto Mt_Ingreso_Neto_Mes_Aval,
//                    --ra.RPU No_RPU_AVAL,
//                    rn.Numero_Escritura Dx_No_Escritura_Aval,
//                    rn.Fecha_Escritura Dt_Fecha_Escritura_Aval,
//                    rn.Nombre_Notario Dx_Nombre_Notario_Escritura_Aval,
//                    rn.Numero_Notaria Dx_No_Notario_Escritura_Aval,
//                    rn.Estado Cve_Estado_Escritura_Aval,
//                    rn.Municipio Cve_Deleg_Municipio_Escritura_Aval,
//                    pn.Numero_Escritura Dx_No_Escritura_Poder,
//                    pn.Fecha_Escritura Dt_Fecha_Poder,
//                    pn.Nombre_Notario Dx_Nombre_Notario_Poder,
//                    pn.Numero_Notaria Dx_No_Notario_Poder,
//                    pn.Estado Cve_Estado_Poder,
//                    pn.Municipio Cve_Deleg_Municipio_Poder,
//                    ac.Numero_Escritura Dx_No_Escritura_Acta,
//                    ac.Fecha_Escritura Dt_Fecha_Acta,
//                    ac.Nombre_Notario Dx_Nombre_Notario_Acta,
//                    ac.Numero_Notaria Dx_No_Notario_Acta,
//                    ac.Estado Cve_Estado_Acta,
//                    ac.Municipio Cve_Deleg_Municipio_Acta,
//                    c.No_Ahorro_Energetico No_Ahorro_Energetico,
//                    c.No_Ahorro_Economico No_Ahorro_Economico,
//                    c.Monto_Solicitado Mt_Monto_Solicitado,
//                    c.Monto_Total_Pagar Mt_Monto_Total_Pagar,
//                    c.Capacidad_Pago Mt_Capacidad_Pago,
//                    c.No_Plazo_Pago No_Plazo_Pago,
//                    c.Cve_Periodo_Pago Cve_Periodo_Pago,
//                    c.Tasa_IVA_Intereses Pct_Tasa_Interes,
//                    c.Tasa_Fija Pct_Tasa_Fija,
//                    c.CAT Pct_CAT,
//                    c.Tasa_IVA Pct_Tasa_IVA,
//                    c.Adquisicion_Sust Fg_Adquisicion_Sust,
//                    c.Fecha_Cancelado Dt_Fecha_Cancelado,
//                    c.Fecha_Pendiente Dt_Fecha_Pendiente,
//                    c.Fecha_Por_entregar Dt_Fecha_Por_entregar,
//                    c.Fecha_En_revision Dt_Fecha_En_revision,
//                    c.Fecha_Autorizado Dt_Fecha_Autorizado,
//                    c.Fecha_Rechazado Dt_Fecha_Rechazado,
//                    c.Fecha_Finanzas Dt_Fecha_Finanzas,
//                    c.Fecha_Ultmod Dt_Fecha_Ultmod,
//                    c.Usr_Ultmod Dx_Usr_Ultmod,
//                    c.Cancel_Sistema Dx_Cancel_Sistema,
//                    c.Tasa_IVA_Intereses Pct_Tasa_IVA_Intereses,
//                    c.Fecha_Beneficiario_con_adeudos Dt_Fecha_Beneficiario_con_adeudos,
//                    c.Fecha_Tarifa_fuera_de_programa Dt_Fecha_Tarifa_fuera_de_programa,
//                    c.No_consumo_promedio No_consumo_promedio,
//                    c.Tipo_Usuario Tipo_Usuario,
//                    c.Fecha_Calificación_MOP_no_válida Dt_Fecha_Calificación_MOP_no_válida,
//                    c.ID_Intelisis ID_Intelisis,
//                    c.ATB01,
//                    c.ATB02,
//                    c.ATB03,
//                    c.ATB04,
//                    c.ATB05,
//                    c.Fecha_Pago_Intelisis Dt_Fecha_Pago_Intelisis,
//                    c.Fecha_Liberacion_Intelisis Dt_Fecha_Liberacion_Intelisis,
//                    c.Afectacion_SICOM_fecha Afectacion_SICOM_fecha,
//                    c.Afectacion_SICOM_DigitoVer Afectacion_SICOM_DigitoVer,
//                    c.Afectacion_SICOM_DigitoVerOk Afectacion_SICOM_DigitoVerOk,
//                    c.Afectacion_SICOM_Sufijo,
//                    c.Afectacion_SIRCA_Fecha,
//                    c.Afectacion_SIRCA_Digito,
//                    c.Gastos_Instalacion Mt_Gastos_Instalacion_Mano_Obra
//                    ,cl2.Cve_Sector
//                    ,cl2.Numero_Empleados No_Empleados,
//                    am.Nombre_Notario DX_NOMBRE_COACREDITADO
//               FROM [CRE_Credito] c INNER JOIN	CLI_Cliente cl ON cl.IdCliente = c.IdCliente  
//                     LEFT JOIN CLI_Negocio cl2 ON	cl2.IdCliente = c.IdCliente
//                     LEFT JOIN CLI_Ref_Cliente rc ON rc.IdCliente = c.IdCliente AND rc.IdTipoReferencia = 1
//                     LEFT JOIN CLI_Ref_Cliente ra ON ra.IdCliente = CL.IdCliente  AND ra.IdTipoReferencia = 2
//                     LEFT JOIN DIR_Direcciones dn ON dn.IdCliente = c.IdCliente AND dn.IdTipoDomicilio = 1
//                     LEFT JOIN DIR_Direcciones df ON df.IdCliente = c.IdCliente AND df.IdTipoDomicilio = 2
//                     LEFT JOIN DIR_Direcciones da ON da.IdCliente = c.IdCliente AND da.IdTipoDomicilio = 3
//--					 LEFT JOIN CAT_CODIGO_POSTAL_SEPOMEX CpDa ON da.CVE_CP = CpDa.CVE_CP
//                     LEFT JOIN CLI_Referencias_Notariales rn ON rn.IdCliente = c.IdCliente AND rn.IdTipoReferencia = 4
//                     LEFT JOIN CLI_Referencias_Notariales pn ON pn.IdCliente = c.IdCliente AND pn.IdTipoReferencia = 6
//                     LEFT JOIN CLI_Referencias_Notariales ac ON ac.IdCliente = c.IdCliente AND ac.IdTipoReferencia = 7
//                     LEFT JOIN CLI_Referencias_Notariales am ON am.IdCliente = c.IdCliente AND am.IdTipoReferencia = 8
//                WHERE No_Credito  = @CreditNo
//                AND cl.Cve_Tipo_Sociedad = 2";  

                #endregion

                SQL = @"SELECT
                        CASE WHEN 
						df.Num_Ext=dn.Num_Ext and 
						df.Calle=dn.Calle AND 
						df.CP=dn.CP 		
						THEN 'TRUE'
						ELSE
						'FALSE'
						END FG_MISMO_DOMICILIO,
                    c.No_Credito,
                    c.ID_Prog_Proy,
                    c.Id_Proveedor,
                    c.Cve_Estatus_Credito,
                    cl.Cve_Tipo_Sociedad,
                    cl2.Cve_Tipo_Industria Cve_Tipo_Industria,
                    cl2.Nombre_Comercial Dx_Nombre_Comercial,
                    cl.Nombre Dx_Nombres,
                    cl.Ap_Paterno Dx_Apellido_Paterno,
                    cl.Ap_Materno Dx_Apellido_Materno,
                    cl.Razon_Social Dx_Razon_Social,
                    cl.Fec_Nacimiento Dt_Nacimiento_Fecha,
                    cl.email,
                    dn.telefono_local, 
                    cl.RFC Dx_RFC,
                    cl.CURP Dx_CURP,
                    cl.IdRegimenConyugal Cl_Regimen_Conyugal,
                    CASE
                        WHEN ISNULL(rc.Nombre,'') <> '' 
                            THEN UPPER(rc.Nombre) + ' ' + ISNULL(rc.Ap_Paterno,'') + ' ' + ISNULL(ltrim(rc.Ap_Materno),'') 
                        WHEN rc.Nombre IS NULL AND cl.Razon_Social IS NOT NULL 
                            THEN UPPER(LTRIM(RTRIM(ISNULL(cl.Razon_Social,'')))) 
                        WHEN rc.Nombre IS NULL AND cl.Razon_Social IS NULL 
                            THEN UPPER(LTRIM(RTRIM(ISNULL(cl.Nombre,'') + ' ' + ISNULL(cl.Ap_Paterno,'') + ' ' + ISNULL(cl.Ap_Materno,''))))
                    END [Dx_Nombre_Repre_Legal],
                    cl2.IdTipoAcreditacion Cve_Acreditacion_Repre_legal,
                    cl2.IdTipoIdentificacion Cve_Identificacion_Repre_legal,
                    cl2.Numero_Identificacion Dx_No_Identificacion_Repre_Legal,
                    rc.Genero Fg_Sexo_Repre_legal,
                    c.RPU No_RPU,
                    cl.Cve_Estado_Civil Fg_Edo_Civil_Repre_legal,
                    rc.Reg_Conyugal Cve_Reg_Conyugal_Repre_legal,
                    rc.email Dx_Email_Repre_legal,
                    df.Calle Dx_Domicilio_Fisc_Calle,
                    df.Num_Ext Dx_Domicilio_Fisc_Num,
                    --AGREGADO´POR ALEX
                    CASE WHEN df.CVE_CP IS NULL OR df.CVE_CP = 0
                    THEN df.Colonia
                    ELSE 
                    UPPER((SELECT	TOP 1 [Dx_Colonia] 
									FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] 
									where [Cve_CP] =  df.CVE_CP)) 
					END Dx_Domicilio_Fisc_Colonia,
                    ----
                    df.CP Dx_Domicilio_Fisc_CP,
                    df.Cve_Estado Cve_Estado_Fisc,
                    df.Cve_Deleg_Municipio Cve_Deleg_Municipio_Fisc,
                    df.Cve_Tipo_Propiedad Cve_Tipo_Propiedad_Fisc,
                    df.Telefono_Oficina Dx_Tel_Fisc,
                    dn.Calle Dx_Domicilio_Neg_Calle,
                    dn.Num_Ext Dx_Domicilio_Neg_Num,
                    --AGREGADO POR ALEX
                    CASE WHEN dn.CVE_CP IS NULL OR dn.CVE_CP=0
                    THEN dn.Colonia
                    ELSE
                    UPPER((SELECT TOP 1 [Dx_Colonia] FROM [dbo].[CAT_CODIGO_POSTAL_SEPOMEX] where [Cve_CP] =  dn.CVE_CP)) end as Dx_Domicilio_Neg_Colonia,
                    dn.CP Dx_Domicilio_Neg_CP,
                    dn.Cve_Estado Cve_Estado_Neg,
                    dn.Cve_Deleg_Municipio [Cve_Deleg_Municipio_Neg],
                    dn.Cve_Tipo_Propiedad Cve_Tipo_Propiedad_Neg,
                    dn.Telefono_Local Dx_Tel_Neg,
                    cl2.Ventas_Mes Mt_Ventas_Mes_Empresa,
                    cl2.Gastos_Mes Mt_Gastos_Mes_Empresa,
                    cl2.Ingreso_Neto Mt_Ingreso_Neto_Mes_Empresa,
                    CASE WHEN ra.Nombre IS NOT NULL THEN ra.Nombre + ' ' + isnull(ra.Ap_Paterno,'') + ' ' + isnull(ra.Ap_Materno,'') ELSE ra.Razon_Social END Dx_Nombre_Aval,
                    CASE WHEN ra.RFC IS NOT NULL THEN ra.RFC ELSE ra.CURP END Dx_RFC_CURP_Aval,
                    ra.Telefono_Local Dx_Tel_Aval,
                    ra.Genero Fg_Sexo_Aval,
                    da.Calle Dx_Domicilio_Aval_Calle,
                    da.Num_Ext Dx_Domicilio_Aval_Num,
                    ---AGREGADO POR ALEX
                    CASE WHEN da.CVE_CP IS NULL OR da.CVE_CP=0
                    THEN UPPER(da.Colonia)
                    ELSE
                    UPPER(CPda.Dx_Colonia) end as Dx_Domicilio_Aval_Colonia,
                    da.CP Dx_Domicilio_Aval_CP,
                    da.Cve_Estado Cve_Estado_Aval,
                    da.Cve_Deleg_Municipio Cve_Deleg_Municipio_Aval,
                    ra.Ventas_Mes Mt_Ventas_Mes_Aval,
                    ra.Gastos_Mes Mt_Gastos_Mes_Aval,
                    ra.Ingreso_Neto Mt_Ingreso_Neto_Mes_Aval,
                    --ra.RPU No_RPU_AVAL,
                    rn.Numero_Escritura Dx_No_Escritura_Aval,
                    rn.Fecha_Escritura Dt_Fecha_Escritura_Aval,
                    rn.Nombre_Notario Dx_Nombre_Notario_Escritura_Aval,
                    rn.Numero_Notaria Dx_No_Notario_Escritura_Aval,
                    rn.Estado Cve_Estado_Escritura_Aval,
                    rn.Municipio Cve_Deleg_Municipio_Escritura_Aval,
                    pn.Numero_Escritura Dx_No_Escritura_Poder,
                    pn.Fecha_Escritura Dt_Fecha_Poder,
                    pn.Nombre_Notario Dx_Nombre_Notario_Poder,
                    pn.Numero_Notaria Dx_No_Notario_Poder,
                    pn.Estado Cve_Estado_Poder,
                    pn.Municipio Cve_Deleg_Municipio_Poder,
                    ac.Numero_Escritura Dx_No_Escritura_Acta,
                    ac.Fecha_Escritura Dt_Fecha_Acta,
                    ac.Nombre_Notario Dx_Nombre_Notario_Acta,
                    ac.Numero_Notaria Dx_No_Notario_Acta,
                    ac.Estado Cve_Estado_Acta,
                    ac.Municipio Cve_Deleg_Municipio_Acta,
                    c.No_Ahorro_Energetico No_Ahorro_Energetico,
                    c.No_Ahorro_Economico No_Ahorro_Economico,
                    c.Monto_Solicitado Mt_Monto_Solicitado,
                    c.Monto_Total_Pagar Mt_Monto_Total_Pagar,
                    c.Capacidad_Pago Mt_Capacidad_Pago,
                    c.No_Plazo_Pago No_Plazo_Pago,
                    c.Cve_Periodo_Pago Cve_Periodo_Pago,
                    c.Tasa_IVA_Intereses Pct_Tasa_Interes,
                    c.Tasa_Fija Pct_Tasa_Fija,
                    c.CAT Pct_CAT,
                    c.Tasa_IVA Pct_Tasa_IVA,
                    c.Adquisicion_Sust Fg_Adquisicion_Sust,
                    c.Fecha_Cancelado Dt_Fecha_Cancelado,
                    c.Fecha_Pendiente Dt_Fecha_Pendiente,
                    c.Fecha_Por_entregar Dt_Fecha_Por_entregar,
                    c.Fecha_En_revision Dt_Fecha_En_revision,
                    c.Fecha_Autorizado Dt_Fecha_Autorizado,
                    c.Fecha_Rechazado Dt_Fecha_Rechazado,
                    c.Fecha_Finanzas Dt_Fecha_Finanzas,
                    c.Fecha_Ultmod Dt_Fecha_Ultmod,
                    c.Usr_Ultmod Dx_Usr_Ultmod,
                    c.Cancel_Sistema Dx_Cancel_Sistema,
                    c.Tasa_IVA_Intereses Pct_Tasa_IVA_Intereses,
                    c.Fecha_Beneficiario_con_adeudos Dt_Fecha_Beneficiario_con_adeudos,
                    c.Fecha_Tarifa_fuera_de_programa Dt_Fecha_Tarifa_fuera_de_programa,
                    c.No_consumo_promedio No_consumo_promedio,
                    c.Tipo_Usuario Tipo_Usuario,
                    c.Fecha_Calificación_MOP_no_válida Dt_Fecha_Calificación_MOP_no_válida,
                    c.ID_Intelisis ID_Intelisis,
                    c.ATB01,
                    c.ATB02,
                    c.ATB03,
                    c.ATB04,
                    c.ATB05,
                    c.Fecha_Pago_Intelisis Dt_Fecha_Pago_Intelisis,
                    c.Fecha_Liberacion_Intelisis Dt_Fecha_Liberacion_Intelisis,
                    c.Afectacion_SICOM_fecha Afectacion_SICOM_fecha,
                    c.Afectacion_SICOM_DigitoVer Afectacion_SICOM_DigitoVer,
                    c.Afectacion_SICOM_DigitoVerOk Afectacion_SICOM_DigitoVerOk,
                    c.Afectacion_SICOM_Sufijo,
                    c.Afectacion_SIRCA_Fecha,
                    c.Afectacion_SIRCA_Digito,
                    round((c.Gastos_Instalacion / (1 + (c.Tasa_IVA / 100))),2) Mt_Gastos_Instalacion_Mano_Obra
                    ,cl2.Cve_Sector
                    ,cl2.Numero_Empleados No_Empleados
                    ,am.Nombre_Notario DX_NOMBRE_COACREDITADO
                    FROM [CRE_Credito] c 
                    LEFT JOIN	CLI_Cliente cl ON	cl.IdCliente = c.IdCliente
                    LEFT JOIN	CLI_Negocio cl2 ON	cl2.IdCliente = c.IdCliente	
                           AND cl2.IdNegocio = c.IdNegocio			                    
                    LEFT JOIN CLI_Ref_Cliente rc ON rc.IdCliente = c.IdCliente AND rc.IdTipoReferencia = 1
                                    AND rc.IdNegocio = c.IdNegocio
                    LEFT JOIN CLI_Ref_Cliente ra ON ra.IdCliente = c.IdCliente  AND ra.IdTipoReferencia = 2
                                     AND ra.IdNegocio = c.IdNegocio
                    LEFT JOIN DIR_Direcciones dn ON dn.IdCliente = c.IdCliente AND dn.IdTipoDomicilio = 1
                                    AND dn.IdNegocio = c.IdNegocio
                    LEFT JOIN DIR_Direcciones df ON df.IdCliente = c.IdCliente AND df.IdTipoDomicilio = 2
                                     AND df.IdNegocio = c.IdNegocio
                    LEFT JOIN DIR_Direcciones da ON da.IdCliente = c.IdCliente AND da.IdTipoDomicilio = 3
                                     AND da.IdNegocio = c.IdNegocio
                    LEFT JOIN CAT_CODIGO_POSTAL_SEPOMEX CpDa ON da.CVE_CP = CpDa.CVE_CP
                    LEFT JOIN CLI_Referencias_Notariales rn ON rn.IdCliente = c.IdCliente AND rn.IdTipoReferencia = 2
                                     AND rn.IdNegocio = c.IdNegocio
                    LEFT JOIN CLI_Referencias_Notariales pn ON pn.IdCliente = c.IdCliente AND pn.IdTipoReferencia = 6
                                     AND pn.IdNegocio = c.IdNegocio
                    LEFT JOIN CLI_Referencias_Notariales ac ON ac.IdCliente = c.IdCliente AND ac.IdTipoReferencia = 7
                                     AND ac.IdNegocio = c.IdNegocio				                     
                    LEFT JOIN CLI_Referencias_Notariales am ON am.IdCliente = c.IdCliente AND am.IdTipoReferencia = 8
                                    AND am.IdNegocio = c.IdNegocio
                    WHERE C.No_Credito  = @CreditNo";
                  
                var paras = new []
                {
                    new SqlParameter("@CreditNo", creditNo)
                };

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this,
                    "Get credit pendiente date failed:Execute method  GetPendienteDate in CreditDal.", ex, true);
            }
            return dtResult;
        }

        ///// <summary>
        ///// Update credit by regional or supplier
        ///// </summary>
        ///// <param name="CreditModel"></param>
        ///// <returns></returns>
        //public int UpdateCreditApproveInfo(CreditEntity CreditModel)
        //{
        //    int iResult = 0;
        //    string SQL = "";

        //    try
        //    {
        //        using (var m = new CreditEntities())
        //        {
        //            var cre = m.CreditoSet.First(p => p.No_Credito == CreditModel.No_Credito);
        //            var cli = m.ClienteSet.First(p => p.IdCliente == cre.IdCliente);

        //            //credito

        //            cli.Razon_Social = CreditModel.Dx_Razon_Social;
        //            cli.Tipo_Industria = CreditModel.Cve_Tipo_Industria;
        //            cli.Cve_Tipo_Sociedad = byte.Parse(CreditModel.Cve_Tipo_Sociedad.ToString(CultureInfo.InvariantCulture));
        //            cli.CURP = CreditModel.Dx_CURP;
        //            cli.Nombre = CreditModel.Dx_Nombre_Repre_Legal;
        //            cli.IdTipoAcreditacion = byte.Parse(CreditModel.Cve_Acreditacion_Repre_legal.ToString(CultureInfo.InvariantCulture));
        //            cli.Genero = byte.Parse(CreditModel.Fg_Sexo_Repre_legal.ToString(CultureInfo.InvariantCulture));
        //            cli.RPU = CreditModel.No_RPU;
        //            cli.Cve_Estado_Civil = CreditModel.Fg_Edo_Civil_Repre_legal;
        //            cli.IdRegimenConyugal = byte.Parse(CreditModel.Cve_Reg_Conyugal_Repre_legal.ToString(CultureInfo.InvariantCulture));
        //            cli.IdTipoIdentificacion = byte.Parse(CreditModel.Cve_Identificacion_Repre_legal.ToString(CultureInfo.InvariantCulture));
        //            cli.Numero_Identificacion = CreditModel.Dx_No_Identificacion_Repre_Legal;
        //            cli.Ventas_Mes = decimal.Parse(CreditModel.Mt_Ventas_Mes_Empresa.ToString(CultureInfo.InvariantCulture));
        //            cli.Gastos_Mes = decimal.Parse(CreditModel.Mt_Gastos_Mes_Empresa.ToString(CultureInfo.InvariantCulture));
        //            cli.email = CreditModel.Dx_Email_Repre_legal;

        //            var dir = m.DireccionesSet.First(p => p.IdCliente == cre.IdCliente && p.IdTipoDomicilio == 2);

        //            //fiscal
        //            dir.Calle = CreditModel.Dx_Domicilio_Fisc_Calle;
        //            dir.Num_Ext = CreditModel.Dx_Domicilio_Fisc_Num;
        //            dir.CP = CreditModel.Dx_Domicilio_Fisc_CP;
        //            dir.Cve_Estado = byte.Parse(CreditModel.Cve_Estado_Fisc.ToString(CultureInfo.InvariantCulture));
        //            dir.Cve_Deleg_Municipio = CreditModel.Cve_Deleg_Municipio_Fisc;
        //            dir.Cve_Tipo_Propiedad = CreditModel.Cve_Tipo_Propiedad_Fisc;
        //            dir.Telefono_Local = CreditModel.Dx_Tel_Fisc;

        //            var dir2 = m.DireccionesSet.First(p => p.IdCliente == cre.IdCliente && p.IdTipoDomicilio == 1);

        //            //negocio
        //            dir2.Calle = CreditModel.Dx_Domicilio_Neg_Calle;
        //            dir2.Num_Ext = CreditModel.Dx_Domicilio_Neg_Num;
        //            dir2.CP = CreditModel.Dx_Domicilio_Neg_CP;
        //            dir2.Cve_Estado = CreditModel.Cve_Estado_Neg;
        //            dir2.Cve_Deleg_Municipio = CreditModel.Cve_Deleg_Municipio_Neg;
        //            dir2.IdTipoDomicilio = byte.Parse(CreditModel.Cve_Tipo_Propiedad_Neg.ToString(CultureInfo.InvariantCulture));
        //            dir2.Telefono_Local = CreditModel.Dx_Tel_Neg;

        //            //obligado solidario
        //            var rcli = m.RefClienteSet.First(p => p.IdCliente == cre.IdCliente && p.IdTipoReferencia == 2);

        //            rcli.Nombre = CreditModel.Dx_Nombre_Aval;
        //            rcli.RFC = (CreditModel.Dx_RFC_CURP_Aval.Length <= 13 ? CreditModel.Dx_RFC_CURP_Aval : null);
        //            rcli.CURP = (CreditModel.Dx_RFC_CURP_Aval.Length > 13 ? CreditModel.Dx_RFC_CURP_Aval : null);
        //            rcli.Telefono_Local = CreditModel.Dx_Tel_Aval;
        //            rcli.Genero = byte.Parse(CreditModel.Fg_Sexo_Aval.ToString(CultureInfo.InvariantCulture));

        //            //obligado solidario
        //            var dir3 = m.DireccionesSet.First(p => p.IdCliente == cre.IdCliente && p.IdTipoDomicilio == 3);

        //            dir3.Calle = CreditModel.Dx_Domicilio_Aval_Calle;
        //            dir3.Num_Ext = CreditModel.Dx_Domicilio_Aval_Num;
        //            dir3.CP = CreditModel.Dx_Domicilio_Aval_CP;
        //            dir3.Cve_Estado = CreditModel.Cve_Estado_Aval;
        //            dir3.Cve_Deleg_Municipio = CreditModel.Cve_Deleg_Municipio_Aval;

        //            cli.Ventas_Mes = decimal.Parse(CreditModel.Mt_Ventas_Mes_Aval.ToString(CultureInfo.InvariantCulture));
        //            cli.Gastos_Mes = decimal.Parse(CreditModel.Mt_Gastos_Mes_Aval.ToString(CultureInfo.InvariantCulture));

        //            cre.Fecha_Ultmod = DateTime.Parse(CreditModel.Dt_Fecha_Ultmod.ToString());
        //            cre.Usr_Ultmod = CreditModel.Dx_Usr_Ultmod;

        //            cli.Cve_Sector = CreditModel.Cve_Sector_economico;
        //            cli.Numero_Empleados = CreditModel.No_Empleados;

        //            m.SaveChanges();
        //        }

        //        //                    SQL =  @"UPDATE dbo.K_CREDITO SET 
        //        //                             Dx_Razon_Social =   @Dx_Razon_Social   ,
        //        //                             Cve_Tipo_Industria          =   @Cve_Tipo_Industria   ,
        //        //                             Cve_Tipo_Sociedad       =   @Cve_Tipo_Sociedad   ,
        //        //                             Dx_CURP       =   @Dx_CURP   ,
        //        //                             Dx_Nombre_Repre_Legal       =   @Dx_Nombre_Repre_Legal   ,
        //        //                             Cve_Acreditacion_Repre_legal     =   @Cve_Acreditacion_Repre_legal   ,
        //        //                             Fg_Sexo_Repre_legal     =   @Fg_Sexo_Repre_legal  ,
        //        //                             No_RPU     =   @No_RPU   ,
        //        //                             Fg_Edo_Civil_Repre_legal    =   @Fg_Edo_Civil_Repre_legal  , 
        //        //                             Cve_Reg_Conyugal_Repre_legal     =   @Cve_Reg_Conyugal_Repre_legal   ,
        //        //                             Cve_Identificacion_Repre_legal     =   @Cve_Identificacion_Repre_legal   ,
        //        //                             Dx_No_Identificacion_Repre_Legal      =   @Dx_No_Identificacion_Repre_Legal   ,
        //        //                             Mt_Ventas_Mes_Empresa         =   @Mt_Ventas_Mes_Empresa   ,
        //        //                             Mt_Gastos_Mes_Empresa       =   @Mt_Gastos_Mes_Empresa   ,
        //        //                             Dx_Email_Repre_legal    =   @Dx_Email_Repre_legal   ,
        //        //                             Dx_Domicilio_Fisc_Calle    =   @Dx_Domicilio_Fisc_Calle   ,
        //        //                             Dx_Domicilio_Fisc_Num    =   @Dx_Domicilio_Fisc_Num    ,
        //        //                             Dx_Domicilio_Fisc_CP    =   @Dx_Domicilio_Fisc_CP   ,
        //        //                             Cve_Estado_Fisc    =   @Cve_Estado_Fisc   ,
        //        //                             Cve_Deleg_Municipio_Fisc   =   @Cve_Deleg_Municipio_Fisc   ,
        //        //                             Cve_Tipo_Propiedad_Fisc    =   @Cve_Tipo_Propiedad_Fisc   ,
        //        //                             Dx_Tel_Fisc    =   @Dx_Tel_Fisc   ,
        //        //                             Fg_Mismo_Domicilio     =   @Fg_Mismo_Domicilio  ,
        //        //                             Dx_Domicilio_Neg_Calle   =   @Dx_Domicilio_Neg_Calle   ,
        //        //                             Dx_Domicilio_Neg_Num  =   @Dx_Domicilio_Neg_Num   ,
        //        //                             Dx_Domicilio_Neg_CP   =   @Dx_Domicilio_Neg_CP   ,
        //        //                             Cve_Estado_Neg    =   @Cve_Estado_Neg  ,
        //        //                             Cve_Deleg_Municipio_Neg    =   @Cve_Deleg_Municipio_Neg  ,
        //        //                             Cve_Tipo_Propiedad_Neg    =   @Cve_Tipo_Propiedad_Neg   ,
        //        //                             Dx_Tel_Neg     =   @Dx_Tel_Neg   ,
        //        //                             Dx_Nombre_Aval     =   @Dx_Nombre_Aval   ,
        //        //                             Dx_First_Name_Aval     =   @Dx_First_Name_Aval   ,
        //        //                             Dx_Last_Name_Aval     =   @Dx_Last_Name_Aval   ,
        //        //                             Dx_Mother_Name_Aval     =   @Dx_Mother_Name_Aval   ,
        //        //                             Dt_BirthDate_Aval     =   @Dt_BirthDate_Aval   ,
        //        //                             Dx_RFC_CURP_Aval      =   @Dx_RFC_CURP_Aval   ,
        //        //                             Dx_RFC_Aval      =   @Dx_RFC_Aval   ,
        //        //                             Dx_CURP_Aval      =   @Dx_CURP_Aval   ,
        //        //                             Dx_Tel_Aval     =   @Dx_Tel_Aval   ,
        //        //                             Fg_Sexo_Aval    =   @Fg_Sexo_Aval   ,
        //        //                             Dx_Domicilio_Aval_Calle    =   @Dx_Domicilio_Aval_Calle  ,
        //        //                             Dx_Domicilio_Aval_Num    =   @Dx_Domicilio_Aval_Num   ,
        //        //                             Dx_Domicilio_Aval_CP    =   @Dx_Domicilio_Aval_CP   ,
        //        //                             Cve_Estado_Aval   =   @Cve_Estado_Aval  ,
        //        //                             Cve_Deleg_Municipio_Aval   =   @Cve_Deleg_Municipio_Aval ,
        //        //                             Mt_Ventas_Mes_Aval    =   @Mt_Ventas_Mes_Aval ,
        //        //                             Mt_Gastos_Mes_Aval   =   @Mt_Gastos_Mes_Aval  ,
        //        //                             Dx_Nombre_Coacreditado    =   @Dx_Nombre_Coacreditado   ,
        //        //                             Dx_RFC_CURP_Coacreditado   =   @Dx_RFC_CURP_Coacreditado   ,
        //        //                             Fg_Sexo_Coacreditado   =   @Fg_Sexo_Coacreditado   ,
        //        //                             Dx_Telefono_Coacreditado   =   @Dx_Telefono_Coacreditado  ,
        //        //                             Dx_Domicilio_Coacreditado_Calle   =   @Dx_Domicilio_Coacreditado_Calle   ,
        //        //                             Dx_Domicilio_Coacreditado_Num   =   @Dx_Domicilio_Coacreditado_Num   ,
        //        //                             Dx_Domicilio_Coacreditado_CP    =   @Dx_Domicilio_Coacreditado_CP   ,
        //        //                             Cve_Estado_Coacreditado   =   @Cve_Estado_Coacreditado   ,
        //        //                             Cve_Deleg_Municipio_Coacreditado   =   @Cve_Deleg_Municipio_Coacreditado  ,
        //        //                             Dt_Fecha_Ultmod   =   @Dt_Fecha_Ultmod   ,
        //        //                             Dx_Usr_Ultmod =   @Dx_Usr_Ultmod   ,
        //        //                             Cve_Sector = @Cve_Sector_economico ,
        //        //                             No_Empleados = @No_Empleados   
        //        //                             where No_Credito  =   @No_Credito ";

        //        //                SqlParameter[] paras = new SqlParameter[] { 
        //        //                        new SqlParameter("@No_Credito", CreditModel.No_Credito),
        //        //                        new SqlParameter("@Dx_Razon_Social",CreditModel.Dx_Razon_Social  ),
        //        //                        new SqlParameter("@Cve_Tipo_Industria",         CreditModel.Cve_Tipo_Industria  ),
        //        //                        new SqlParameter("@Cve_Tipo_Sociedad",      CreditModel.Cve_Tipo_Sociedad  ),
        //        //                        new SqlParameter("@Dx_CURP",      CreditModel.Dx_CURP  ),
        //        //                        new SqlParameter("@Dx_Nombre_Repre_Legal",      CreditModel.Dx_Nombre_Repre_Legal  ),
        //        //                        new SqlParameter("@Cve_Acreditacion_Repre_legal",    CreditModel.Cve_Acreditacion_Repre_legal  ),
        //        //                        new SqlParameter("@Fg_Sexo_Repre_legal",    CreditModel.Fg_Sexo_Repre_legal ),
        //        //                        new SqlParameter("@No_RPU",    CreditModel.No_RPU  ),
        //        //                        new SqlParameter("@Fg_Edo_Civil_Repre_legal",   CreditModel.Fg_Edo_Civil_Repre_legal ), 
        //        //                        new SqlParameter("@Cve_Reg_Conyugal_Repre_legal",    CreditModel.Cve_Reg_Conyugal_Repre_legal  ),
        //        //                        new SqlParameter("@Cve_Identificacion_Repre_legal",    CreditModel.Cve_Identificacion_Repre_legal  ),
        //        //                        new SqlParameter("@Dx_No_Identificacion_Repre_Legal",     CreditModel.Dx_No_Identificacion_Repre_Legal  ),
        //        //                        new SqlParameter("@Mt_Ventas_Mes_Empresa",        CreditModel.Mt_Ventas_Mes_Empresa  ),
        //        //                        new SqlParameter("@Mt_Gastos_Mes_Empresa",      CreditModel.Mt_Gastos_Mes_Empresa  ),
        //        //                        new SqlParameter("@Dx_Email_Repre_legal",   CreditModel.Dx_Email_Repre_legal  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Fisc_Calle",   CreditModel.Dx_Domicilio_Fisc_Calle  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Fisc_Num",   CreditModel.Dx_Domicilio_Fisc_Num   ),
        //        //                        new SqlParameter("@Dx_Domicilio_Fisc_CP",   CreditModel.Dx_Domicilio_Fisc_CP  ),
        //        //                        new SqlParameter("@Cve_Estado_Fisc",   CreditModel.Cve_Estado_Fisc  ),
        //        //                        new SqlParameter("@Cve_Deleg_Municipio_Fisc",  CreditModel.Cve_Deleg_Municipio_Fisc  ),
        //        //                        new SqlParameter("@Cve_Tipo_Propiedad_Fisc",   CreditModel.Cve_Tipo_Propiedad_Fisc  ),
        //        //                        new SqlParameter("@Dx_Tel_Fisc",   CreditModel.Dx_Tel_Fisc  ),
        //        //                        new SqlParameter("@Fg_Mismo_Domicilio",    CreditModel.Fg_Mismo_Domicilio ),
        //        //                        new SqlParameter("@Dx_Domicilio_Neg_Calle",  CreditModel.Dx_Domicilio_Neg_Calle  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Neg_Num", CreditModel.Dx_Domicilio_Neg_Num  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Neg_CP",  CreditModel.Dx_Domicilio_Neg_CP  ),
        //        //                        new SqlParameter("@Cve_Estado_Neg",   CreditModel.Cve_Estado_Neg ),
        //        //                        new SqlParameter("@Cve_Deleg_Municipio_Neg",   CreditModel.Cve_Deleg_Municipio_Neg ),
        //        //                        new SqlParameter("@Cve_Tipo_Propiedad_Neg",   CreditModel.Cve_Tipo_Propiedad_Neg  ),
        //        //                        new SqlParameter("@Dx_Tel_Neg",    CreditModel.Dx_Tel_Neg  ),

        //        //                        // RSA detailed Aval information for RFC validation
        //        //                        new SqlParameter("@Dx_Nombre_Aval",    CreditModel.Dx_Nombre_Aval  ),
        //        //                        new SqlParameter("@Dx_First_Name_Aval",    CreditModel.Dx_First_Name_Aval  ),
        //        //                        new SqlParameter("@Dx_Last_Name_Aval",    CreditModel.Dx_Last_Name_Aval  ),
        //        //                        new SqlParameter("@Dx_Mother_Name_Aval",    CreditModel.Dx_Mother_Name_Aval  ),
        //        //                        new SqlParameter("@Dt_BirthDate_Aval",    CreditModel.Dt_BirthDate_Aval  ),
        //        //                        new SqlParameter("@Dx_RFC_CURP_Aval",     CreditModel.Dx_RFC_CURP_Aval  ),
        //        //                        new SqlParameter("@Dx_RFC_Aval",     CreditModel.Dx_RFC_Aval  ),
        //        //                        new SqlParameter("@Dx_CURP_Aval",     CreditModel.Dx_CURP_Aval  ),

        //        //                        new SqlParameter("@Dx_Tel_Aval",    CreditModel.Dx_Tel_Aval  ),
        //        //                        new SqlParameter("@Fg_Sexo_Aval",   CreditModel.Fg_Sexo_Aval  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Aval_Calle",   CreditModel.Dx_Domicilio_Aval_Calle ),
        //        //                        new SqlParameter("@Dx_Domicilio_Aval_Num",   CreditModel.Dx_Domicilio_Aval_Num  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Aval_CP",   CreditModel.Dx_Domicilio_Aval_CP  ),
        //        //                        new SqlParameter("@Cve_Estado_Aval",  CreditModel.Cve_Estado_Aval ),
        //        //                        new SqlParameter("@Cve_Deleg_Municipio_Aval",  CreditModel.Cve_Deleg_Municipio_Aval),
        //        //                        new SqlParameter("@Mt_Ventas_Mes_Aval",   CreditModel.Mt_Ventas_Mes_Aval),
        //        //                        new SqlParameter("@Mt_Gastos_Mes_Aval",  CreditModel.Mt_Gastos_Mes_Aval ),
        //        //                        new SqlParameter("@Dx_Nombre_Coacreditado",   CreditModel.Dx_Nombre_Coacreditado  ),
        //        //                        new SqlParameter("@Dx_RFC_CURP_Coacreditado",  CreditModel.Dx_RFC_CURP_Coacreditado  ),
        //        //                        new SqlParameter("@Fg_Sexo_Coacreditado",  CreditModel.Fg_Sexo_Coacreditado  ),
        //        //                        new SqlParameter("@Dx_Telefono_Coacreditado",  CreditModel.Dx_Telefono_Coacreditado ),
        //        //                        new SqlParameter("@Dx_Domicilio_Coacreditado_Calle",  CreditModel.Dx_Domicilio_Coacreditado_Calle  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Coacreditado_Num",  CreditModel.Dx_Domicilio_Coacreditado_Num  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Coacreditado_CP",   CreditModel.Dx_Domicilio_Coacreditado_CP  ),
        //        //                        new SqlParameter("@Cve_Estado_Coacreditado",  CreditModel.Cve_Estado_Coacreditado  ),
        //        //                        new SqlParameter("@Cve_Deleg_Municipio_Coacreditado",  CreditModel.Cve_Deleg_Municipio_Coacreditado ),
        //        //                        new SqlParameter("@Dt_Fecha_Ultmod",  CreditModel.Dt_Fecha_Ultmod  ),
        //        //                        new SqlParameter("@Dx_Usr_Ultmod",CreditModel.Dx_Usr_Ultmod  ),

        //        //                        new SqlParameter("@Cve_Sector_economico", CreditModel.Cve_Sector_economico),
        //        //                        new SqlParameter("@No_Empleados", CreditModel.No_Empleados)
        //        //                };

        //        //                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new LsDAException(this, "Update credit failed: Execute method UpdateCredit in CreditDal.", ex, true);
        //    }
        //    return iResult;
        //}

        /// <summary>
        /// Get max credit number
        /// </summary>
        /// <returns></returns>
        public int Select_Max_No_Credit()
        {
            int ImaxNo = 0;
            try
            {
                string Sql = "SELECT MAX(No_Credito)+1 as MaxNo FROM CRE_Credito";
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql);
                if (o != null)
                {
                    ImaxNo = int.Parse(o.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return ImaxNo;
        }
        /// <summary>
        /// Calculate the total energy consumption savings for all selected products
        /// </summary>
        /// <param name="ProductID">Product ID</param>
        /// <returns></returns>
        public double CalculateTotalEnergyConsumptionSavings(string ProductID)
        {
            double totalEnergyConsumptionSavings = default(double);
            string SQL = "";
            try
            {
                // updated by tina 2012-02-27
                SQL = "SELECT CAST(SUM(ISNULL(P.[No_Eficiencia_Energia],0) * ISNULL(B.[No_Capacidad],0)) AS float) AS Total_Energy_Saving FROM CAT_PRODUCTO AS P" +
                        " inner join CAT_CAPACIDAD_SUSTITUCION B on P.Cve_Capacidad_Sust=B.Cve_Capacidad_Sust where P.Cve_Producto in ( " + ProductID + ") ";

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
                if (o != null)
                {
                    totalEnergyConsumptionSavings = Convert.ToDouble(o);
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get total energy consumption savings failed:Execute method CalculateTotalEnergyConsumptionSavings in CreditDal.", ex, true);
            }
            return totalEnergyConsumptionSavings;
        }

        ///// <summary>
        ///// Update Credit and Credit_Product
        ///// </summary>
        ///// <param name="CreditModel"></param>
        ///// <returns></returns>
        //public int UpdateCreditReview(CreditEntity CreditModel)
        //{
        //    int iResult = 0;
        //    string SQL = "";
        //    try
        //    {
        //        using (var m = new CreditEntities())
        //        {
        //            var cre = m.CreditoSet.First(p => p.No_Credito == CreditModel.No_Credito);
        //            var cli = m.ClienteSet.First(p => p.IdCliente == int.Parse(cre.No_Credito));

        //            cli.Razon_Social = CreditModel.Dx_Razon_Social;
        //            cli.Tipo_Industria = CreditModel.Cve_Tipo_Industria;
        //            cli.Cve_Tipo_Sociedad = byte.Parse(CreditModel.Cve_Tipo_Sociedad.ToString());
        //            cli.CURP = CreditModel.Dx_CURP;
        //            cli.RFC = CreditModel.Dx_RFC;
        //            cli.Nombre = CreditModel.Dx_Nombre_Repre_Legal;
        //            cli.IdTipoAcreditacion = byte.Parse(CreditModel.Cve_Acreditacion_Repre_legal.ToString());
        //            cli.Genero = byte.Parse(CreditModel.Fg_Sexo_Repre_legal.ToString());
        //            cli.RPU = CreditModel.No_RPU;
        //            cli.Cve_Estado_Civil = CreditModel.Fg_Edo_Civil_Repre_legal;

        //            cre.No_consumo_promedio = CreditModel.No_consumo_promedio;

        //            cli.IdRegimenConyugal = byte.Parse(CreditModel.Cve_Reg_Conyugal_Repre_legal.ToString());
        //            cli.IdTipoIdentificacion = byte.Parse(CreditModel.Cve_Identificacion_Repre_legal.ToString());
        //            cli.Numero_Identificacion = CreditModel.Dx_No_Identificacion_Repre_Legal;

        //            cli.Ventas_Mes = decimal.Parse(CreditModel.Mt_Ventas_Mes_Empresa.ToString());
        //            cli.Gastos_Mes = decimal.Parse(CreditModel.Mt_Gastos_Mes_Empresa.ToString());
        //            cli.email = CreditModel.Dx_Email_Repre_legal;

        //            //fiscal
        //            var dir = m.DireccionesSet.First(p => p.IdCliente == cli.IdCliente && p.IdTipoDomicilio == 2);

        //            dir.Calle = CreditModel.Dx_Domicilio_Fisc_Calle;
        //            dir.Num_Ext = CreditModel.Dx_Domicilio_Fisc_Num;
        //            dir.CP = CreditModel.Dx_Domicilio_Fisc_CP;
        //            dir.Colonia = CreditModel.Dx_Domicilio_Fisc_Colonia;
        //            dir.Cve_Estado = CreditModel.Cve_Estado_Fisc;
        //            dir.Cve_Deleg_Municipio = CreditModel.Cve_Deleg_Municipio_Fisc;
        //            dir.Cve_Tipo_Propiedad = CreditModel.Cve_Tipo_Propiedad_Fisc;
        //            dir.Telefono_Local = CreditModel.Dx_Tel_Fisc;

        //            //negocio
        //            var dirn = m.DireccionesSet.First(p => p.IdCliente == cli.IdCliente && p.IdTipoDomicilio == 1);

        //            dirn.Calle = CreditModel.Dx_Domicilio_Fisc_Calle;
        //            dirn.Num_Ext = CreditModel.Dx_Domicilio_Fisc_Num;
        //            dirn.CP = CreditModel.Dx_Domicilio_Fisc_CP;
        //            dirn.Colonia = CreditModel.Dx_Domicilio_Fisc_Colonia;
        //            dirn.Cve_Estado = CreditModel.Cve_Estado_Fisc;
        //            dirn.Cve_Deleg_Municipio = CreditModel.Cve_Deleg_Municipio_Fisc;
        //            dirn.Cve_Tipo_Propiedad = CreditModel.Cve_Tipo_Propiedad_Fisc;
        //            dirn.Telefono_Local = CreditModel.Dx_Tel_Fisc;

        //            //obligado solidario
        //            var rcli = m.RefClienteSet.First(p => p.IdCliente == cli.IdCliente && p.IdTipoReferencia == 2);

        //            rcli.Nombre = CreditModel.Dx_Nombre_Aval;
        //            rcli.Telefono_Local = CreditModel.Dx_Tel_Aval;
        //            rcli.Genero = byte.Parse(CreditModel.Fg_Sexo_Aval.ToString());

        //            //obligado solidario
        //            var diro = m.DireccionesSet.First(p => p.IdCliente == cli.IdCliente && p.IdTipoDomicilio == 3);

        //            diro.Calle = CreditModel.Dx_Domicilio_Fisc_Calle;
        //            diro.Num_Ext = CreditModel.Dx_Domicilio_Fisc_Num;
        //            diro.CP = CreditModel.Dx_Domicilio_Fisc_CP;
        //            diro.Colonia = CreditModel.Dx_Domicilio_Fisc_Colonia;
        //            diro.Cve_Estado = CreditModel.Cve_Estado_Fisc;
        //            diro.Cve_Deleg_Municipio = CreditModel.Cve_Deleg_Municipio_Fisc;
        //            diro.Cve_Tipo_Propiedad = CreditModel.Cve_Tipo_Propiedad_Fisc;
        //            diro.Telefono_Local = CreditModel.Dx_Tel_Fisc;

        //            rcli.Ventas_Mes = decimal.Parse(CreditModel.Mt_Ventas_Mes_Aval.ToString());
        //            rcli.Gastos_Mes = decimal.Parse(CreditModel.Mt_Gastos_Mes_Aval.ToString());
        //            rcli.Ingreso_Neto = decimal.Parse(CreditModel.Mt_Ingreso_Neto_Mes_Aval.ToString());
        //            rcli.RPU = CreditModel.No_RPU_AVAL;

        //            cre.Monto_Solicitado = decimal.Parse(CreditModel.Mt_Monto_Solicitado.ToString());

        //            cli.Ingreso_Neto = decimal.Parse(CreditModel.Mt_Ingreso_Neto_Mes_Empresa.ToString());

        //            cre.Cve_Periodo_Pago = CreditModel.Cve_Periodo_Pago;
        //            cre.No_Ahorro_Energetico = CreditModel.No_Ahorro_Energetico;
        //            cre.No_Ahorro_Economico = decimal.Parse(CreditModel.No_Ahorro_Economico.ToString());
        //            cre.Capacidad_Pago = decimal.Parse(CreditModel.Mt_Capacidad_Pago.ToString());
        //            cre.Monto_Total_Pagar = decimal.Parse(CreditModel.Mt_Monto_Total_Pagar.ToString());
        //            cre.Fecha_Beneficiario_con_adeudos = DateTime.Parse(CreditModel.Dt_Fecha_Beneficiario_con_adeudos.ToString());
        //            cre.Cve_Estatus_Credito = CreditModel.Cve_Estatus_Credito;
        //            cre.Fecha_Tarifa_fuera_de_programa = DateTime.Parse(CreditModel.Dt_Fecha_Tarifa_fuera_de_programa.ToString());
        //            cre.No_Plazo_Pago = CreditModel.No_Plazo_Pago;
        //            cre.Fecha_Ultmod = DateTime.Parse(CreditModel.Dt_Fecha_Ultmod.ToString());

        //            cli.Nombre_Comercial = CreditModel.Dx_Nombre_Comercial;

        //            cre.Usr_Ultmod = CreditModel.Dx_Usr_Ultmod;
        //            cre.ID_Prog_Proy = CreditModel.ID_Prog_Proy;
        //            cre.Id_Proveedor = CreditModel.Id_Proveedor;
        //            cre.Tasa_Interes = CreditModel.Pct_Tasa_Interes;
        //            cre.Tasa_Fija = CreditModel.Pct_Tasa_Fija;
        //            cre.CAT = CreditModel.Pct_CAT;
        //            cre.Tasa_IVA = CreditModel.Pct_Tasa_IVA;
        //            cre.Tasa_IVA_Intereses = CreditModel.Pct_Tasa_IVA_Intereses;
        //            cre.Tipo_Usuario = CreditModel.Tipo_Usuario;

        //            cli.Telefono_Local = CreditModel.Telephone;
        //            cli.email = CreditModel.Email;

        //            cre.Gastos_Instalacion = decimal.Parse(CreditModel.Mt_Gastos_Instalacion_Mano_Obra.ToString());

        //            m.SaveChanges();
        //        }

        //        //                SQL = @" UPDATE dbo.K_CREDITO SET 
        //        //                         Dx_Razon_Social =   @Dx_Razon_Social   ,
        //        //                         Cve_Tipo_Industria =   @Cve_Tipo_Industria   ,
        //        //                         Cve_Tipo_Sociedad =   @Cve_Tipo_Sociedad   ,
        //        //                         Dx_CURP =   @Dx_CURP   ,
        //        //                         Dx_RFC=@Dx_RFC,
        //        //                         Dx_Nombre_Repre_Legal  =   @Dx_Nombre_Repre_Legal   ,
        //        //                         Cve_Acreditacion_Repre_legal  =   @Cve_Acreditacion_Repre_legal   ,
        //        //                         Fg_Sexo_Repre_legal  =   @Fg_Sexo_Repre_legal  ,
        //        //                         No_RPU =   @No_RPU   ,
        //        //                         Fg_Edo_Civil_Repre_legal    =   @Fg_Edo_Civil_Repre_legal  , 
        //        //                         No_consumo_promedio = @No_consumo_promedio,
        //        //                         Cve_Reg_Conyugal_Repre_legal     =   @Cve_Reg_Conyugal_Repre_legal   ,
        //        //                         Cve_Identificacion_Repre_legal     =   @Cve_Identificacion_Repre_legal   ,
        //        //                         Dx_No_Identificacion_Repre_Legal      =   @Dx_No_Identificacion_Repre_Legal   ,
        //        //                         Mt_Ventas_Mes_Empresa         =   @Mt_Ventas_Mes_Empresa   ,
        //        //                         Mt_Gastos_Mes_Empresa       =   @Mt_Gastos_Mes_Empresa   ,
        //        //                         Dx_Email_Repre_legal    =   @Dx_Email_Repre_legal   ,
        //        //                         Dx_Domicilio_Fisc_Calle    =   @Dx_Domicilio_Fisc_Calle   ,
        //        //                         Dx_Domicilio_Fisc_Num    =   @Dx_Domicilio_Fisc_Num    ,
        //        //                         Dx_Domicilio_Fisc_CP    =   @Dx_Domicilio_Fisc_CP   ,
        //        //                         Dx_Domicilio_Fisc_Colonia =@Dx_Domicilio_Fisc_Colonia,
        //        //                         Cve_Estado_Fisc    =   @Cve_Estado_Fisc   ,
        //        //                         Cve_Deleg_Municipio_Fisc   =   @Cve_Deleg_Municipio_Fisc   ,
        //        //                         Cve_Tipo_Propiedad_Fisc    =   @Cve_Tipo_Propiedad_Fisc   ,
        //        //                         Dx_Tel_Fisc    =   @Dx_Tel_Fisc   ,
        //        //                         Fg_Mismo_Domicilio     =   @Fg_Mismo_Domicilio  ,
        //        //                         Dx_Domicilio_Neg_Calle   =   @Dx_Domicilio_Neg_Calle   ,
        //        //                         Dx_Domicilio_Neg_Num  =   @Dx_Domicilio_Neg_Num   ,
        //        //                         Dx_Domicilio_Neg_CP   =   @Dx_Domicilio_Neg_CP   ,
        //        //                         Dx_Domicilio_Neg_Colonia =@Dx_Domicilio_Neg_Colonia,
        //        //                         Cve_Estado_Neg    =   @Cve_Estado_Neg  ,
        //        //                         Cve_Deleg_Municipio_Neg    =   @Cve_Deleg_Municipio_Neg  ,
        //        //                         Cve_Tipo_Propiedad_Neg    =   @Cve_Tipo_Propiedad_Neg   ,
        //        //                         Dx_Tel_Neg     =   @Dx_Tel_Neg   ,
        //        //                         Dx_Nombre_Aval     =   @Dx_Nombre_Aval   ,
        //        //                         Dx_First_Name_Aval     =   @Dx_First_Name_Aval   ,
        //        //                         Dx_Last_Name_Aval     =   @Dx_Last_Name_Aval   ,
        //        //                         Dx_Mother_Name_Aval     =   @Dx_Mother_Name_Aval   ,
        //        //                         Dt_BirthDate_Aval     =   @Dt_BirthDate_Aval   ,
        //        //                         Dx_RFC_CURP_Aval      =   @Dx_RFC_CURP_Aval   ,
        //        //                         Dx_RFC_Aval      =   @Dx_RFC_Aval   ,
        //        //                         Dx_CURP_Aval      =   @Dx_CURP_Aval   ,
        //        //                         Dx_Tel_Aval     =   @Dx_Tel_Aval   ,
        //        //                         Fg_Sexo_Aval    =   @Fg_Sexo_Aval   ,
        //        //                         Dx_Domicilio_Aval_Calle    =   @Dx_Domicilio_Aval_Calle  ,
        //        //                         Dx_Domicilio_Aval_Num    =   @Dx_Domicilio_Aval_Num   ,
        //        //                         Dx_Domicilio_Aval_CP    =   @Dx_Domicilio_Aval_CP   ,
        //        //                         Dx_Domicilio_Aval_Colonia =@Dx_Domicilio_Aval_Colonia,
        //        //                         Cve_Estado_Aval   =   @Cve_Estado_Aval  ,
        //        //                         Cve_Deleg_Municipio_Aval   =   @Cve_Deleg_Municipio_Aval ,
        //        //                         Mt_Ventas_Mes_Aval    =   @Mt_Ventas_Mes_Aval ,
        //        //                         Mt_Gastos_Mes_Aval   =   @Mt_Gastos_Mes_Aval  ,
        //        //                         Mt_Ingreso_Neto_Mes_Aval =@Mt_Ingreso_Neto_Mes_Aval,
        //        //                         No_RPU_AVAL = @No_RPU_AVAL,
        //        //                         Dx_Nombre_Coacreditado    =   @Dx_Nombre_Coacreditado   ,
        //        //                         Dx_RFC_CURP_Coacreditado   =   @Dx_RFC_CURP_Coacreditado   ,
        //        //                         Fg_Sexo_Coacreditado   =   @Fg_Sexo_Coacreditado   ,
        //        //                         Dx_Telefono_Coacreditado   =   @Dx_Telefono_Coacreditado  ,
        //        //                         Dx_Domicilio_Coacreditado_Calle   =   @Dx_Domicilio_Coacreditado_Calle   ,
        //        //                         Dx_Domicilio_Coacreditado_Num   =   @Dx_Domicilio_Coacreditado_Num   ,
        //        //                         Dx_Domicilio_Coacreditado_CP    =   @Dx_Domicilio_Coacreditado_CP   ,
        //        //                         Dx_Domicilio_Coacreditado_Colonia=@Dx_Domicilio_Coacreditado_Colonia,
        //        //                         Cve_Estado_Coacreditado   =   @Cve_Estado_Coacreditado   ,
        //        //                         Cve_Deleg_Municipio_Coacreditado   =   @Cve_Deleg_Municipio_Coacreditado  ,                         
        //        //                         Cve_Periodo_Pago =@Cve_Periodo_Pago,
        //        //                         Mt_Monto_Solicitado =@Mt_Monto_Solicitado,
        //        //                         Mt_Ingreso_Neto_Mes_Empresa =@Mt_Ingreso_Neto_Mes_Empresa,
        //        //                        No_Ahorro_Energetico =@No_Ahorro_Energetico,
        //        //                         No_Ahorro_Economico =@No_Ahorro_Economico,
        //        //                         Mt_Capacidad_Pago=@Mt_Capacidad_Pago,
        //        //                        Mt_Monto_Total_Pagar =@Mt_Monto_Total_Pagar,
        //        //                        Cve_Estatus_Credito=@Cve_Estatus_Credito,
        //        //                        Dt_Fecha_Beneficiario_con_adeudos = @Dt_Fecha_Beneficiario_con_adeudos,
        //        //                        Dt_Fecha_Tarifa_fuera_de_programa=@Dt_Fecha_Tarifa_fuera_de_programa,
        //        //                         No_Plazo_Pago = @No_Plazo_Pago,
        //        //
        //        //                        Dt_Fecha_Ultmod          = @Dt_Fecha_Ultmod,
        //        //                        Dx_Nombre_Comercial      = @Dx_Nombre_Comercial,
        //        //                        Dx_Usr_Ultmod            = @Dx_Usr_Ultmod,
        //        //                        ID_Prog_Proy             = @ID_Prog_Proy,
        //        //                        Id_Proveedor             = @Id_Proveedor,
        //        //                        Pct_Tasa_Interes         = @Pct_Tasa_Interes,
        //        //                        Pct_Tasa_Fija            = @Pct_Tasa_Fija,
        //        //                        Pct_CAT                  = @Pct_CAT,
        //        //                        Pct_Tasa_IVA             = @Pct_Tasa_IVA,
        //        //                        Pct_Tasa_IVA_Intereses   = @Pct_Tasa_IVA_Intereses,
        //        //                        Tipo_Usuario             = @Tipo_Usuario,
        //        //                        ATB04                    = @ATB04,
        //        //                        ATB05                    = @ATB05,
        //        //
        //        //                        Mt_Gastos_Instalacion_Mano_Obra = @Mt_Gastos_Instalacion_Mano_Obra
        //        //
        //        //                         where No_Credito  =   @No_Credito ";

        //        //                SqlParameter[] paras = new SqlParameter[] { 
        //        //                        new SqlParameter("@No_Credito", CreditModel.No_Credito),
        //        //                        new SqlParameter("@Dx_Razon_Social",CreditModel.Dx_Razon_Social  ),
        //        //                        new SqlParameter("@Cve_Tipo_Industria",         CreditModel.Cve_Tipo_Industria  ),
        //        //                        new SqlParameter("@Cve_Tipo_Sociedad",      CreditModel.Cve_Tipo_Sociedad  ),
        //        //                        new SqlParameter("@Dx_CURP",      CreditModel.Dx_CURP  ),
        //        //                        new SqlParameter("@Dx_RFC",CreditModel.Dx_RFC),
        //        //                        new SqlParameter("@Dx_Nombre_Repre_Legal",      CreditModel.Dx_Nombre_Repre_Legal  ),
        //        //                        new SqlParameter("@Cve_Acreditacion_Repre_legal",    CreditModel.Cve_Acreditacion_Repre_legal  ),
        //        //                        new SqlParameter("@Fg_Sexo_Repre_legal",    CreditModel.Fg_Sexo_Repre_legal ),
        //        //                        new SqlParameter("@No_RPU",    CreditModel.No_RPU  ),
        //        //                        new SqlParameter("@Fg_Edo_Civil_Repre_legal",   CreditModel.Fg_Edo_Civil_Repre_legal ), 
        //        //                        new SqlParameter("@No_consumo_promedio",CreditModel.No_consumo_promedio),
        //        //                        new SqlParameter("@Cve_Reg_Conyugal_Repre_legal",    CreditModel.Cve_Reg_Conyugal_Repre_legal  ),
        //        //                        new SqlParameter("@Cve_Identificacion_Repre_legal",    CreditModel.Cve_Identificacion_Repre_legal  ),
        //        //                        new SqlParameter("@Dx_No_Identificacion_Repre_Legal",     CreditModel.Dx_No_Identificacion_Repre_Legal  ),
        //        //                        new SqlParameter("@Mt_Ventas_Mes_Empresa",        CreditModel.Mt_Ventas_Mes_Empresa  ),
        //        //                        new SqlParameter("@Mt_Gastos_Mes_Empresa",      CreditModel.Mt_Gastos_Mes_Empresa  ),
        //        //                        new SqlParameter("@Dx_Email_Repre_legal",   CreditModel.Dx_Email_Repre_legal  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Fisc_Calle",   CreditModel.Dx_Domicilio_Fisc_Calle  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Fisc_Num",   CreditModel.Dx_Domicilio_Fisc_Num   ),
        //        //                        new SqlParameter("@Dx_Domicilio_Fisc_CP",   CreditModel.Dx_Domicilio_Fisc_CP  ),
        //        //                        new SqlParameter ("@Dx_Domicilio_Fisc_Colonia",CreditModel.Dx_Domicilio_Fisc_Colonia),
        //        //                        new SqlParameter("@Cve_Estado_Fisc",   CreditModel.Cve_Estado_Fisc  ),
        //        //                        new SqlParameter("@Cve_Deleg_Municipio_Fisc",  CreditModel.Cve_Deleg_Municipio_Fisc  ),
        //        //                        new SqlParameter("@Cve_Tipo_Propiedad_Fisc",   CreditModel.Cve_Tipo_Propiedad_Fisc  ),
        //        //                        new SqlParameter("@Dx_Tel_Fisc",   CreditModel.Dx_Tel_Fisc  ),
        //        //                        new SqlParameter("@Fg_Mismo_Domicilio",    CreditModel.Fg_Mismo_Domicilio ),
        //        //                        new SqlParameter("@Dx_Domicilio_Neg_Calle",  CreditModel.Dx_Domicilio_Neg_Calle  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Neg_Num", CreditModel.Dx_Domicilio_Neg_Num  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Neg_CP",  CreditModel.Dx_Domicilio_Neg_CP  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Neg_Colonia",CreditModel.Dx_Domicilio_Neg_Colonia),
        //        //                        new SqlParameter("@Cve_Estado_Neg",   CreditModel.Cve_Estado_Neg ),
        //        //                        new SqlParameter("@Cve_Deleg_Municipio_Neg",   CreditModel.Cve_Deleg_Municipio_Neg ),
        //        //                        new SqlParameter("@Cve_Tipo_Propiedad_Neg",   CreditModel.Cve_Tipo_Propiedad_Neg  ),
        //        //                        new SqlParameter("@Dx_Tel_Neg",    CreditModel.Dx_Tel_Neg  ),

        //        //                        // RSA detailed Aval information for RFC validation
        //        //                        new SqlParameter("@Dx_Nombre_Aval",    CreditModel.Dx_Nombre_Aval  ),
        //        //                        new SqlParameter("@Dx_First_Name_Aval",    CreditModel.Dx_First_Name_Aval  ),
        //        //                        new SqlParameter("@Dx_Last_Name_Aval",    CreditModel.Dx_Last_Name_Aval  ),
        //        //                        new SqlParameter("@Dx_Mother_Name_Aval",    CreditModel.Dx_Mother_Name_Aval  ),
        //        //                        new SqlParameter("@Dt_BirthDate_Aval",    CreditModel.Dt_BirthDate_Aval  ),
        //        //                        new SqlParameter("@Dx_RFC_CURP_Aval",     CreditModel.Dx_RFC_CURP_Aval  ),
        //        //                        new SqlParameter("@Dx_RFC_Aval",     CreditModel.Dx_RFC_Aval  ),
        //        //                        new SqlParameter("@Dx_CURP_Aval",     CreditModel.Dx_CURP_Aval  ),

        //        //                        new SqlParameter("@Dx_Tel_Aval",    CreditModel.Dx_Tel_Aval  ),
        //        //                        new SqlParameter("@Fg_Sexo_Aval",   CreditModel.Fg_Sexo_Aval  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Aval_Calle",   CreditModel.Dx_Domicilio_Aval_Calle ),
        //        //                        new SqlParameter("@Dx_Domicilio_Aval_Num",   CreditModel.Dx_Domicilio_Aval_Num  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Aval_CP",   CreditModel.Dx_Domicilio_Aval_CP  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Aval_Colonia",CreditModel.Dx_Domicilio_Aval_Colonia),
        //        //                        new SqlParameter("@Cve_Estado_Aval",  CreditModel.Cve_Estado_Aval ),
        //        //                        new SqlParameter("@Cve_Deleg_Municipio_Aval",  CreditModel.Cve_Deleg_Municipio_Aval),
        //        //                        new SqlParameter("@Mt_Ventas_Mes_Aval",   CreditModel.Mt_Ventas_Mes_Aval),
        //        //                        new SqlParameter("@Mt_Gastos_Mes_Aval",  CreditModel.Mt_Gastos_Mes_Aval ),
        //        //                        new SqlParameter("@Mt_Ingreso_Neto_Mes_Aval",CreditModel.Mt_Ingreso_Neto_Mes_Aval),
        //        //                        new SqlParameter("@No_RPU_AVAL", CreditModel.No_RPU_AVAL),
        //        //                        new SqlParameter("@Dx_Nombre_Coacreditado",   CreditModel.Dx_Nombre_Coacreditado  ),
        //        //                        new SqlParameter("@Dx_RFC_CURP_Coacreditado",  CreditModel.Dx_RFC_CURP_Coacreditado  ),
        //        //                        new SqlParameter("@Fg_Sexo_Coacreditado",  CreditModel.Fg_Sexo_Coacreditado  ),
        //        //                        new SqlParameter("@Dx_Telefono_Coacreditado",  CreditModel.Dx_Telefono_Coacreditado ),
        //        //                        new SqlParameter("@Dx_Domicilio_Coacreditado_Calle",  CreditModel.Dx_Domicilio_Coacreditado_Calle  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Coacreditado_Num",  CreditModel.Dx_Domicilio_Coacreditado_Num  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Coacreditado_CP",   CreditModel.Dx_Domicilio_Coacreditado_CP  ),
        //        //                        new SqlParameter("@Dx_Domicilio_Coacreditado_Colonia",CreditModel.Dx_Domicilio_Coacreditado_Colonia),
        //        //                        new SqlParameter("@Cve_Estado_Coacreditado",  CreditModel.Cve_Estado_Coacreditado  ),
        //        //                        new SqlParameter("@Cve_Deleg_Municipio_Coacreditado",  CreditModel.Cve_Deleg_Municipio_Coacreditado ),                       
        //        //                        new SqlParameter("@Mt_Monto_Solicitado",CreditModel.Mt_Monto_Solicitado),
        //        //                        new SqlParameter("@Mt_Ingreso_Neto_Mes_Empresa",CreditModel.Mt_Ingreso_Neto_Mes_Empresa),
        //        //                        new SqlParameter("@Cve_Periodo_Pago",CreditModel.Cve_Periodo_Pago),
        //        //                        new SqlParameter("@No_Ahorro_Energetico",CreditModel.No_Ahorro_Energetico),
        //        //                        new SqlParameter("@No_Ahorro_Economico",CreditModel.No_Ahorro_Economico),
        //        //                        new SqlParameter("@Mt_Capacidad_Pago",CreditModel.Mt_Capacidad_Pago),
        //        //                        new SqlParameter ("@Mt_Monto_Total_Pagar",CreditModel.Mt_Monto_Total_Pagar),
        //        //                        new SqlParameter("@Dt_Fecha_Beneficiario_con_adeudos",CreditModel.Dt_Fecha_Beneficiario_con_adeudos),
        //        //                        new SqlParameter("@Cve_Estatus_Credito",CreditModel.Cve_Estatus_Credito),
        //        //                        new SqlParameter("@Dt_Fecha_Tarifa_fuera_de_programa",CreditModel.Dt_Fecha_Tarifa_fuera_de_programa),
        //        //                        new SqlParameter("@No_Plazo_Pago",CreditModel.No_Plazo_Pago),

        //        //                        // RSA 20130814 Enable edition
        //        //                        new SqlParameter("@Dt_Fecha_Ultmod", CreditModel.Dt_Fecha_Ultmod),
        //        //                        new SqlParameter("@Dx_Nombre_Comercial", CreditModel.Dx_Nombre_Comercial),
        //        //                        new SqlParameter("@Dx_Usr_Ultmod", CreditModel.Dx_Usr_Ultmod),
        //        //                        new SqlParameter("@ID_Prog_Proy", CreditModel.ID_Prog_Proy),
        //        //                        new SqlParameter("@Id_Proveedor", CreditModel.Id_Proveedor),
        //        //                        new SqlParameter("@Pct_Tasa_Interes", CreditModel.Pct_Tasa_Interes),
        //        //                        new SqlParameter("@Pct_Tasa_Fija", CreditModel.Pct_Tasa_Fija),
        //        //                        new SqlParameter("@Pct_CAT", CreditModel.Pct_CAT),
        //        //                        new SqlParameter("@Pct_Tasa_IVA", CreditModel.Pct_Tasa_IVA),
        //        //                        new SqlParameter("@Pct_Tasa_IVA_Intereses", CreditModel.Pct_Tasa_IVA_Intereses),
        //        //                        new SqlParameter("@Tipo_Usuario", CreditModel.Tipo_Usuario),
        //        //                        new SqlParameter("@ATB04", CreditModel.Telephone),
        //        //                        new SqlParameter("@ATB05", CreditModel.Email),

        //        //                        new SqlParameter("@Mt_Gastos_Instalacion_Mano_Obra", CreditModel.Mt_Gastos_Instalacion_Mano_Obra)
        //        //                };

        //        //                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new LsDAException(this, "Update credit failed: Execute method UpdateCredit in CreditDal.", ex, true);
        //    }
        //    return iResult;
        //}

        /// <summary>
        /// Check if service code exist
        /// </summary>
        /// <param name="servicecode"></param>
        /// <returns></returns>
        public bool IsServiceCodeExist(string servicecode)
        {
            var bResult = false;
            try
            {
                var bdPaeeemProd = ConfigurationManager.AppSettings["BdPaeeemProd"].ToString();

                var SQL = "SELECT RPU from CRE_Credito CRE (NOLOCK) WHERE RPU = @No_RPU " +
                             " AND Cve_Estatus_Credito IN (1,2,3,4,6) " +
                             " union all " +
                             " SELECT NO_RPU FROM " + bdPaeeemProd + "..K_CREDITO (NOLOCK) WHERE No_RPU = @No_RPU " + 
                             " AND CVE_ESTATUS_CREDITO IN (1,2,3,4,6)";

               // SQL = "select * from CRE_Credito CRE LEFT  JOIN CLI_Cliente CLIENTE ON CRE.IdCliente  = CLIENTE.IdCliente INNER JOIN K_CREDITO_PRODUCTO PRODUCTO ON CRE.No_Credito = PRODUCTO.No_Credito where RPU=@No_RPU AND Cve_Estatus_Credito IN (1,2,3,4)";
                SqlParameter[] paras = { 
                    new SqlParameter("@No_RPU", servicecode)
                };

                var o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                if (o != null)
                {
                    bResult = true;
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Check service code exist failed: Execute method IsServiceCodeExist in CreditDal.", ex, true);
            }

            return bResult;
        }

        public bool IsServiceCode_K_Datos_Pyme(string servicecode)
        {
            bool bResult = false;
            string SQL = "";
            try
            {
                SQL = "select MAX(Cve_Dato_Pyme),No_RPU from K_DATOS_PYME PYME where No_RPU= @No_RPU AND Cve_Es_Pyme = 0 GROUP BY No_RPU";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_RPU", servicecode)
                };

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                if (o != null)
                {
                    bResult = true;
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Check service code exist failed: Execute method IsServiceCode_K_Datos_Pyme in CreditDal.", ex, true);
            }

            return bResult;
        }


        //add by coco 2011-10-09
        /// <summary>
        /// Get credit requests
        /// </summary>
        /// <param name="DisposalID">disposal center</param>
        /// <param name="userType">disposal center type:main or branch</param>
        /// <param name="ProviderID">provider</param>
        /// <param name="ProviderType">provider type: main or branch</param>
        /// <param name="TechnologyID">technology</param>
        /// <param name="sortName">sort name</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <param name="pageCount">page count</param>
        /// <returns></returns>
        public DataTable Get_CreditRequestForDisposal(int DisposalID, string userType, int ProviderID, string ProviderType, int TechnologyID, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            StringBuilder WhereBuilder = new StringBuilder();
            try
            {
                WhereBuilder.Append(" WHERE 1 = 1");

                if (DisposalID > 0 && !string.IsNullOrEmpty(userType))
                {
                    if (userType == GlobalVar.DISPOSAL_CENTER)
                    {
                        userType = "M";
                    }
                    else
                    {
                        userType = "B";
                    }
                    WhereBuilder.Append(" and [Id_Proveedor] in (select A.Id_Proveedor from CAT_PROVEEDOR A  inner join K_CAT_PROVEEDOR_CAT_CENTRO_DISP B on A.Id_Proveedor=B.Id_Proveedor and B.Id_Centro_Disp='" + DisposalID + "' and B.Fg_Tipo_Centro_Disp='" + userType + "') ");
                    WhereBuilder.Append(" and [No_Credito] IN (SELECT K_CREDITO_SUSTITUCION.No_Credito FROM K_CREDITO_SUSTITUCION WHERE Dt_Fecha_Recepcion is not  null and K_CREDITO_SUSTITUCION.Cve_Tecnologia in(select A.Cve_Tecnologia from CAT_TECNOLOGIA  A  inner join K_PROG_TECNOLOGIA B on A.Cve_Tecnologia=B.Cve_Tecnologia and B.ID_Prog_Proy='1' inner join K_CENTRO_DISP_TECNOLOGIA C on A.Cve_Tecnologia=C.Cve_Tecnologia and C.Id_Centro_Disp= '" + DisposalID + "' and C.Fg_Tipo_Centro_Disp='" + userType + "'))");
                }
                if (ProviderID > 0)
                {
                    if (ProviderType == GlobalVar.SUPPLIER)
                    {
                        WhereBuilder.Append(" and [Id_Proveedor] = " + ProviderID + " and [Tipo_Usuario] = '" + GlobalVar.SUPPLIER + "'");//+ "' OR (Id_Proveedor in (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Id_Proveedor = " + ProviderID + ") AND Tipo_Usuario = '" + GlobalVar.SUPPLIER_BRANCH + "'))");
                    }
                    else
                    {
                        WhereBuilder.Append("  And Id_Branch in (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE  Id_Branch= " + ProviderID + ")  AND Tipo_Usuario = '" + GlobalVar.SUPPLIER_BRANCH + "'");
                    }
                }
                if (TechnologyID > 0)
                {
                    WhereBuilder.Append(" and [No_Credito] IN (SELECT K_CREDITO_SUSTITUCION.No_Credito FROM K_CREDITO_SUSTITUCION WHERE Dt_Fecha_Recepcion is not  null and  K_CREDITO_SUSTITUCION.Cve_Tecnologia =  '" + TechnologyID + "')");
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@spWhere", WhereBuilder.ToString()),
                    new SqlParameter("@SortName", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize)
                };
                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_creditRequest", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credits failed: Execute method Get_CreditRequestForDisposal in CreditDal.", ex, true);
            }

            return dtResult;
        }

        /// <summary>
        /// Get barcode with the credit number
        /// </summary>
        /// <param name="CreditNumber">Credit Number</param>
        /// <returns>Barcode String</returns>
        public DataTable GetCreditsBarCodeByNoCredit(string CreditNumber)
        {
            DataTable dtResult = null;

            string sql = "select k.Barcode_Solicitud from CRE_Creditok " +   //RY: no existe Barcode_solicitud
                         " where k.No_Credito=@CreditNumber";
            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@CreditNumber", CreditNumber)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get GetCreditsBarCodeByNoCredit failed: Execute method GetCreditsBarCodeByNoCredit in CreditDal.", ex, true);
            }

            return dtResult;
        }

        /// <summary>
        /// Get credit by credit barcode
        /// </summary>
        /// <param name="CreditNumber"></param>
        /// <returns></returns>
        public string GetCreditsNoByBarCode(string CreditNumber)
        {
            string Result = "";

            //string sql = "select COUNT(*) from K_CREDITO where No_Credito=@No_Credito";

            string sql = "SELECT COUNT(1) FROM CRE_Credito WHERE No_Credito = @No_Credito";

            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", CreditNumber)
                };
                object obj = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
                if (obj != null)
                {
                    Result = obj.ToString();
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get CreditNo failed: Execute method GetCreditsBarCodeByBarCode in CreditDal.", ex, true);
            }
            return Result;
        }

        //end add
        //add by coco 2011-11-09
        /// <summary>
        /// Check if the entered credit request is in valid status
        /// </summary>
        /// <param name="creditNumber">credit number</param>
        /// <returns>record count</returns>
        public int IsCreditInValidStatus(string creditNumber)
        {
            int Result = 0;

            //string sql = "select COUNT(*) from K_CREDITO where No_Credito=@No_Credito and Cve_Estatus_Credito in (@Cve_Estatus_Credito, @Cve_Estatus_Credito2)";

            string sql = "SELECT COUNT(*) FROM CRE_Credito WHERE No_Credito = @No_Credito AND Cve_Estatus_Credito IN (@Cve_Estatus_Credito, @Cve_Estatus_Credito2)";

            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditNumber),
                    new SqlParameter("@Cve_Estatus_Credito",(int)CreditStatus.PORENTREGAR),
                    new SqlParameter("@Cve_Estatus_Credito2",(int)CreditStatus.ENREVISION)
                };

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
                if (o != null)
                {
                    Result = int.Parse(o.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get CreditNo failed: Execute method GetCreditByEstatusAndBarCode in CreditDal.", ex, true);
            }

            return Result;
        }

        //end add
        ///// <summary>
        ///// get related credit requests with the specific program
        ///// </summary>
        ///// <param name="program">Program applied for the credit request</param>
        ///// <returns></returns>
        //public DataTable GetCreditsByProgram(string program)
        //{
        //    DataTable TableResult = null;                       

        //    try
        //    {
        //        //Build the query SQL string
        //        string SQL = "select A.No_Credito from K_CREDITO A ";
        //        if (program != "")
        //        {
        //            SQL += " where A.ID_Prog_Proy=" + program;
        //        }

        //        TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new LsDAException(this, "Get credit with program failed: Execute method GetCreditsByProgram in CreditDal.", ex, true);
        //    }

        //    return TableResult;
        //}

        /// <summary>
        /// get credit
        /// </summary>
        /// <param name="program"></param>
        /// <param name="DisposalID"></param>
        /// <param name="DisposalType"></param>
        /// <returns></returns>
        public DataTable GetCreditByProgramAndDisposal(string program, string DisposalID, string DisposalType)
        {
            DataTable dtResult = null;
            try
            {
                //string sql = "select No_Credito from K_CREDITO where No_Credito in(select No_Credito from K_CREDITO_SUSTITUCION where Id_Centro_Disp=@Id_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp)";

                string sql = @"SELECT No_Credito FROM CRE_Credito WHERE No_Credito IN 
                    (SELECT No_Credito FROM K_CREDITO_SUSTITUCION WHERE Id_Centro_Disp = @Id_Centro_Disp AND Fg_Tipo_Centro_Disp = @Fg_Tipo_Centro_Disp)";

                if (program != "")
                {
                    sql += " and ID_Prog_Proy=" + program;
                }
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Centro_Disp", DisposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",DisposalType)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Get credit with program and disposal failed: Execute method GetCreditByProgramAndDisposal in CreditDal.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// Get credits with program and disposal and technology and supplier
        /// </summary>
        /// <param name="program"></param>
        /// <param name="technology"></param>
        /// <param name="supplierId"></param>
        /// <param name="supplierType"></param>
        /// <param name="disposalId"></param>
        /// <param name="disposalType"></param>
        /// <returns></returns>
        public DataTable GetCreditsByProgramAndDisposalAndTechnologyAndSupplierForDisableOldProduct(string program, string technology, string supplierId, string supplierType, int disposalId, string disposalType, string registry)
        {
            DataTable TableResult = null;

            try
            {
                //Build the query SQL string

                //string SQL = "SELECT DISTINCT A.No_Credito FROM K_CREDITO A INNER JOIN K_CREDITO_SUSTITUCION B" +
                //                 " ON A.No_Credito=B.No_Credito AND B.Id_Folio IS NOT NULL AND B.Dt_Fecha_Recepcion IS NOT NULL AND ISNULL(B.Fg_Si_Funciona,0)=1 " +
                //                 " WHERE 1=1";

                string SQL = "SELECT DISTINCT A.No_Credito FROM CRE_Credito A INNER JOIN K_CREDITO_SUSTITUCION B" +
                                 " ON A.No_Credito = B.No_Credito AND B.Id_Folio IS NOT NULL AND B.Dt_Fecha_Recepcion IS NOT NULL AND ISNULL(B.Fg_Si_Funciona,0) = 1 " +
                                 " WHERE 1=1";

                if (program != "")
                {
                    SQL += " AND A.ID_Prog_Proy = @program";
                }
                if (disposalId != 0)
                {
                    SQL += " AND B.Id_Centro_Disp = @disposalId";
                }
                if (disposalType != "")
                {
                    SQL += " AND B.Fg_Tipo_Centro_Disp = @disposalType";
                }
                if (technology != "")
                {
                    SQL += " AND B.Cve_Tecnologia = @technology";
                }
                if (supplierId != "")
                {
                    SQL += " AND A.Id_Proveedor = @supplierId";
                }
                if (supplierType != "")
                {
                    SQL += " AND A.Tipo_Usuario = @supplierType";
                }
                if (registry != "")
                {
                    if (registry == "Inhabilitados")
                    {
                        SQL += " AND B.Id_Credito_Sustitucion IN (SELECT Id_Credito_Sustitucion FROM K_INHABILITACION_PRODUCTO)";
                    }
                    else
                    {
                        SQL += " AND B.Id_Credito_Sustitucion NOT IN (SELECT Id_Credito_Sustitucion FROM K_INHABILITACION_PRODUCTO)";
                    }
                }

                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@program",program),
                    new SqlParameter("@disposalId",disposalId),
                    new SqlParameter("@disposalType",disposalType),
                    new SqlParameter("@technology",technology),
                    new SqlParameter("@supplierId",supplierId),
                    new SqlParameter("@supplierType",supplierType)
                };

                TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credits with program and disposal and technology and supplier failed: Execute method GetCreditsByProgramAndDisposalAndTechnologyAndSupplierForDisableOldProduct in CreditDal.", ex, true);
            }

            return TableResult;
        }

        /// <summary>
        /// Get Credits with technology and material and program and supplier
        /// </summary>
        /// <param name="program"></param>
        /// <param name="technology"></param>
        /// <param name="material"></param>
        /// <param name="order"></param>
        /// <param name="supplierId"></param>
        /// <param name="supplierType"></param>
        /// <param name="disposalId"></param>
        /// <param name="disposalType"></param>
        /// <returns></returns>
        public DataTable GetCreditsWithTechnologyAndMaterialAndProgramAndSupplier(string program, string technology, string material, int order, string supplierId,
                                                                string supplierType, int disposalId, string disposalType, string equipmentNotRegistryAllMeterial)//updated by tina 2012-08-09
        {
            DataTable dtResult = null;
            try
            {
                //updated by tina 2012-08-09
                string SQL = "SELECT DISTINCT A.No_Credito FROM dbo.View_Old_Equipment_List A  INNER JOIN K_INHABILITACION_PRODUCTO B ON A.Id_Credito_Sustitucion=B.Id_Credito_Sustitucion INNER JOIN K_INHABILITACION C ON B.Id_Inhabilitacion=C.Id_Inhabilitacion";

                if (order == 1)
                {
                    SQL += " WHERE 1=1 AND A.Id_Credito_Sustitucion NOT IN" +
                             " (SELECT DISTINCT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology AND Id_Orden=1)";
                }
                if (order == 2)
                {
                    SQL += " WHERE 1=1 AND A.Id_Credito_Sustitucion not IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology AND Cve_Residuo_Material=@material)" +
                             " AND A.Id_Credito_Sustitucion IN(SELECT DISTINCT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology AND Id_Orden=1)";
                }
                else if (order > 2)
                {
                    SQL += " WHERE 1=1 AND A.Id_Credito_Sustitucion not IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology AND Cve_Residuo_Material=@material)" +
                             "  AND A.Id_Credito_Sustitucion IN(SELECT Id_Credito_Sustitucion FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology" +
                             "  AND Cve_Residuo_Material=(SELECT DISTINCT Cve_Residuo_Material FROM dbo.View_RecoveryProducts WHERE Cve_Tecnologia=@technology AND Id_Orden= (@order - 1)))";
                }
                //end

                if (program != "")
                {
                    SQL += " AND A.ID_Prog_Proy=@program";
                }
                if (supplierId != "")
                {
                    SQL += " AND A.Id_Proveedor=@supplierId";
                }
                if (supplierType != "")
                {
                    SQL += " AND A.Tipo_Usuario=@supplierType";
                }
                if (technology != "")
                {
                    SQL += " AND A.Cve_Tecnologia=@technology";
                }
                if (disposalId != 0)
                {
                    SQL += " AND A.Id_Centro_Disp=@disposalID";
                }
                if (disposalType != "")
                {
                    SQL += " AND A.Fg_Tipo_Centro_Disp=@disposalType";
                }
                //added by tina 2012-08-09
                if (equipmentNotRegistryAllMeterial != "")
                {
                    SQL += " AND A.Id_Credito_Sustitucion IN(" + equipmentNotRegistryAllMeterial + ")";
                }
                //end

                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@technology",technology),
                    new SqlParameter("@material",material),
                    new SqlParameter("@order",order),
                    new SqlParameter("@program",program),
                    new SqlParameter("@supplierId",supplierId),
                    new SqlParameter("@supplierType",supplierType),
                    new SqlParameter("@disposalId",disposalId),
                    new SqlParameter("@disposalType",disposalType)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Credits with technology and material and program and supplier failed: Execute method GetCreditsWithTechnologyAndMaterialAndProgramAndSupplier in CreditDal.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        /// <param name="DisposalID"></param>
        /// <param name="DisposalType"></param>
        /// <param name="Supplier"></param>
        /// <param name="SupplierType"></param>
        /// <param name="Technology"></param>
        /// <returns></returns>
        public DataTable GetCreditByProgramAndDisposalAndSupplierAndTechnology(string program, string DisposalID, string DisposalType, string Supplier, string SupplierType, string Technology)
        {
            DataTable dtResult = null;
            try
            {
                //string sql = "select No_Credito from K_CREDITO where No_Credito in(select No_Credito from K_CREDITO_SUSTITUCION ";

                string sql = "SELECT No_Credito FROM CRE_Credito WHERE No_Credito IN (SELECT No_Credito FROM K_CREDITO_SUSTITUCION ";

                sql = sql + " WHERE Id_Centro_Disp = @Id_Centro_Disp AND Fg_Tipo_Centro_Disp = @Fg_Tipo_Centro_Disp ";

                if (Technology != "")
                {
                    sql = sql + "  and Cve_Tecnologia = @Cve_Tecnologia";
                }
                sql = sql + ")";
                if (program != "")
                {
                    sql += " and ID_Prog_Proy =" + program;
                }
                if (Supplier != "")
                {
                    sql = sql + " and Id_Proveedor = @Id_Proveedor And Tipo_Usuario = @Tipo_Usuario";
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Centro_Disp", DisposalID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",DisposalType),
                    new SqlParameter("@Cve_Tecnologia",Technology),
                    new SqlParameter("@Id_Proveedor",Supplier),
                    new SqlParameter("@Tipo_Usuario",SupplierType)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Get credit with program and disposal failed: Execute method GetCreditByProgramAndDisposal in CreditDal.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// Check if the entered credit request is in valid status
        /// </summary>
        /// <param name="creditNumber">credit number</param>
        /// <returns>record count</returns>
        public double TotalAmountByRFC(string rfc)
        {
            double Result = 0;

            // RSA 20131003 changed to use monto without interes (IVA included), ie Mt_Monto_Solicitado

            //            string sql = @"Select Sum(Mt_Monto_Solicitado) From K_CREDITO Where RTrim(LTrim(Dx_RFC))=@Dx_RFC And
            //                Cve_Estatus_Credito In (@CreditStatus1, @CreditStatus2, @CreditStatus3, @CreditStatus4, @CreditStatus6)";
            var bdPaeeemProd = ConfigurationManager.AppSettings["BdPaeeemProd"];

            string sql = @"SELECT SUM(Mt_Monto_Solicitado) FROM " + bdPaeeemProd +
                        "..K_Credito WHERE RTRIM(LTRIM(Dx_RFC)) = @Dx_RFC " +
                        "And Cve_Estatus_Credito IN (@CreditStatus1, @CreditStatus2, @CreditStatus3, @CreditStatus4, @CreditStatus6) " +
                        "and No_Credito not in (select No_Credito from CRE_Credito cr " +
                        "join CLI_Cliente cl on cr.IdCliente = cl.IdCliente " +
                        "where cl.RFC = @Dx_RFC " +
                        "and Cve_Estatus_Credito IN (@CreditStatus1, @CreditStatus2, @CreditStatus3, @CreditStatus4, @CreditStatus6))";

      try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Dx_RFC", rfc.Trim()),
                    new SqlParameter("@CreditStatus1",(int)CreditStatus.PENDIENTE),
                    new SqlParameter("@CreditStatus2",(int)CreditStatus.PORENTREGAR),
                    new SqlParameter("@CreditStatus3",(int)CreditStatus.ENREVISION),
                    new SqlParameter("@CreditStatus4",(int)CreditStatus.AUTORIZADO),
                    new SqlParameter("@CreditStatus6",(int)CreditStatus.PARAFINANZAS)
                };

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
                if (o != null && o != DBNull.Value)
                {
                    Result = double.Parse(o.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Total ammount failed: Execute method TotalAmmountByRFC in CreditDal.", ex, true);
            }

            return Result;
        }

        /// <summary>
        /// Generates de "RFC" from the moral person's company name and registry date 
        /// the "homoclave" cannot be generated, it will use the first 2 chars from 
        /// the one provided and add a recalculated "digito verificador"
        /// if a full RFC is provided, the sp will strip uneeded chars
        /// </summary>
        /// <param name="companyName">Nombre de la Empresa</param>
        /// <param name="registryDate"></param>
        /// <param name="homoclave">Homoclave (Full RFC, or homoclave with or without digito verificador)</param>
        /// <returns></returns>
        public string GenerateRFCMoral(string companyName, string registryDate, string homoclave)
        {
            string genRFC = string.Empty;
            DataSet dsResult = new DataSet();

            SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@NombreEmpresa", companyName),
                    new SqlParameter("@FechaRegistro", registryDate),
                    new SqlParameter("@RFCValidar", homoclave)
                };
            SqlHelper.FillDataset(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "spMORRegistroSugerir"
                , dsResult, new string[] { "Inputs", "Calculated" }, paras);
            if (dsResult != null && dsResult.Tables.Count > 1 && dsResult.Tables[1].Rows.Count > 0)
                genRFC = dsResult.Tables[1].Rows[0][0].ToString().Trim();

            return genRFC;
        }
        /// <summary>
        /// Generates de RFC from the first, last and mother names and date of birth of the person
        /// the homoclave is also created including the "digito verificador"
        /// </summary>
        /// <param name="firstName">Nombres</param>
        /// <param name="lastName">Apellido Paterno</param>
        /// <param name="motherName">Apellido Materno</param>
        /// <param name="birthDate">Fecha de nacimiento</param>
        /// <returns></returns>
        public string GenerateRFCInmoral(string firstName, string lastName, string motherName, string birthDate)
        {
            string genRFC = string.Empty;
            DataTable dtResult;

            SqlParameter[] paras = new SqlParameter[] { 
                // parameters required by the stored procedure with fixed values
                new SqlParameter("@Cual", "RFC"),
                new SqlParameter("@Registro", string.Empty),    // ignored if it ain't CURP
                new SqlParameter("@Sexo", string.Empty),        // ignored if it ain't CURP
                new SqlParameter("@Estado", string.Empty),      // ignored if it ain't CURP

                // this are the parameters need to generate RFC-Homoclave(including digito verificador)
                new SqlParameter("@Nombre", firstName),
                new SqlParameter("@Paterno", lastName),
                new SqlParameter("@Materno", motherName),    
                new SqlParameter("@Nacimiento", birthDate)    
            };
            dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "spRegistroSugerir", paras);
            if (dtResult != null && dtResult.Rows.Count > 0)
                genRFC = dtResult.Rows[0][0].ToString().Trim();

            return genRFC;
        }
    }
}
