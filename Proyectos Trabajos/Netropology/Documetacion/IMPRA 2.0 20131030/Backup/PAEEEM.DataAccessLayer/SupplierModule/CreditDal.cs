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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Entities;
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
        public int AddCredit(CreditEntity CreditModel)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "INSERT INTO[dbo].[K_CREDITO]([No_Credito],[ID_Prog_Proy],[Id_Proveedor],[Cve_Estatus_Credito],[Cve_Tipo_Sociedad],[Cve_Tipo_Industria] " +
                            ",[Dx_Nombre_Comercial],[Dx_Razon_Social],[Dx_RFC],[Dx_CURP],[Dx_Nombre_Repre_Legal],[Cve_Acreditacion_Repre_legal],[Cve_Identificacion_Repre_legal] " +
                            ",[Dx_No_Identificacion_Repre_Legal],[Fg_Sexo_Repre_legal],[No_RPU],[Fg_Edo_Civil_Repre_legal],[Cve_Reg_Conyugal_Repre_legal]" +
                            ",[Dx_Email_Repre_legal],[Dx_Domicilio_Fisc_Calle],[Dx_Domicilio_Fisc_Num],[Dx_Domicilio_Fisc_Colonia],[Dx_Domicilio_Fisc_CP]" +
                            ",[Cve_Estado_Fisc],[Cve_Deleg_Municipio_Fisc],[Cve_Tipo_Propiedad_Fisc],[Dx_Tel_Fisc],[Fg_Mismo_Domicilio],[Dx_Domicilio_Neg_Calle]" +
                            ",[Dx_Domicilio_Neg_Num],[Dx_Domicilio_Neg_Colonia],[Dx_Domicilio_Neg_CP],[Cve_Estado_Neg],[Cve_Deleg_Municipio_Neg],[Cve_Tipo_Propiedad_Neg]" +
                            ",[Dx_Tel_Neg],[Mt_Ventas_Mes_Empresa],[Mt_Gastos_Mes_Empresa],[Mt_Ingreso_Neto_Mes_Empresa]" +
                            ",[Dx_Nombre_Aval],[Dx_First_Name_Aval],[Dx_Last_Name_Aval],[Dx_Mother_Name_Aval],[Dt_BirthDate_Aval],[Dx_RFC_CURP_Aval],[Dx_RFC_Aval],[Dx_CURP_Aval]" +
                            ",[Dx_Tel_Aval],[Fg_Sexo_Aval],[Dx_Domicilio_Aval_Calle],[Dx_Domicilio_Aval_Num],[Dx_Domicilio_Aval_Colonia],[Dx_Domicilio_Aval_CP]" +
                            ",[Cve_Estado_Aval],[Cve_Deleg_Municipio_Aval],[Mt_Ventas_Mes_Aval],[Mt_Gastos_Mes_Aval],[Mt_Ingreso_Neto_Mes_Aval],[No_RPU_AVAL]" +
                            ",[Dx_No_Escritura_Aval],[Dt_Fecha_Escritura_Aval],[Dx_Nombre_Notario_Escritura_Aval],[Dx_No_Notario_Escritura_Aval],[Cve_Estado_Escritura_Aval]" +
                            ",[Cve_Deleg_Municipio_Escritura_Aval],[Dt_Fecha_Gravamen],[Dx_Emite_Gravamen],[Dx_Num_Acta_Matrimonio_Aval],[Dx_Registro_Civil_Mat_Aval]" +
                            ",[Dx_Nombre_Coacreditado],[Dx_RFC_CURP_Coacreditado],[Dx_Telefono_Coacreditado],[Fg_Sexo_Coacreditado],[Dx_Domicilio_Coacreditado_Calle],[Dx_Domicilio_Coacreditado_Num]" +
                            ",[Dx_Domicilio_Coacreditado_Colonia],[Dx_Domicilio_Coacreditado_CP],[Cve_Estado_Coacreditado],[Cve_Deleg_Municipio_Coacreditado],[Dx_No_Escritura_Poder]" +
                            ",[Dt_Fecha_Poder],[Dx_Nombre_Notario_Poder],[Dx_No_Notario_Poder],[Cve_Estado_Poder],[Cve_Deleg_Municipio_Poder],[Dx_No_Escritura_Acta]" +
                            ",[Dt_Fecha_Acta],[Dx_Nombre_Notario_Acta],[Dx_No_Notario_Acta],[Cve_Estado_Acta],[Cve_Deleg_Municipio_Acta],[No_Ahorro_Energetico]" +
                            ",[No_Ahorro_Economico],[Mt_Monto_Solicitado],[Mt_Monto_Total_Pagar],[Mt_Capacidad_Pago],[No_Plazo_Pago],[Cve_Periodo_Pago]" +
                            ",[Pct_Tasa_Interes],[Pct_Tasa_Fija],[Pct_CAT],[Pct_Tasa_IVA],[Fg_Adquisicion_Sust],[Dt_Fecha_Cancelado],[Dt_Fecha_Pendiente]" +
                            ",[Dt_Fecha_Por_entregar],[Dt_Fecha_En_revision],[Dt_Fecha_Autorizado],[Dt_Fecha_Rechazado],[Dt_Fecha_Finanzas],[Dt_Fecha_Ultmod]" +
                            ",[Dx_Usr_Ultmod],[Pct_Tasa_IVA_Intereses], [Dt_Fecha_Beneficiario_con_adeudos], [Dt_Fecha_Tarifa_fuera_de_programa], [No_consumo_promedio],Tipo_Usuario,ATB04,ATB05, Mt_Gastos_Instalacion_Mano_Obra)" +//edit by coco 2012-07-16
                            " VALUES(@No_Credito, @ID_Prog_Proy, @Id_Proveedor, @Cve_Estatus_Credito, @Cve_Tipo_Sociedad, @Cve_Tipo_Industria, @Dx_Nombre_Comercial," +
                            "@Dx_Razon_Social, @Dx_RFC,@Dx_CURP, @Dx_Nombre_Repre_Legal, @Cve_Acreditacion_Repre_legal, @Cve_Identificacion_Repre_legal, " +
                            " @Dx_No_Identificacion_Repre_Legal, @Fg_Sexo_Repre_legal, @No_RPU, @Fg_Edo_Civil_Repre_legal, @Cve_Reg_Conyugal_Repre_legal, " +
                            "@Dx_Email_Repre_legal, @Dx_Domicilio_Fisc_Calle, @Dx_Domicilio_Fisc_Num, @Dx_Domicilio_Fisc_Colonia, " +
                            "@Dx_Domicilio_Fisc_CP, @Cve_Estado_Fisc,@Cve_Deleg_Municipio_Fisc, @Cve_Tipo_Propiedad_Fisc, " +
                            "@Dx_Tel_Fisc, @Fg_Mismo_Domicilio, @Dx_Domicilio_Neg_Calle, @Dx_Domicilio_Neg_Num, " +
                            "@Dx_Domicilio_Neg_Colonia,@Dx_Domicilio_Neg_CP, @Cve_Estado_Neg, @Cve_Deleg_Municipio_Neg,@Cve_Tipo_Propiedad_Neg, " +
                            "@Dx_Tel_Neg, @Mt_Ventas_Mes_Empresa, @Mt_Gastos_Mes_Empresa, @Mt_Ingreso_Neto_Mes_Empresa, " +
                            "@Dx_Nombre_Aval,@Dx_First_Name_Aval,@Dx_Last_Name_Aval,@Dx_Mother_Name_Aval, @Dt_BirthDate_Aval, @Dx_RFC_CURP_Aval, @Dx_RFC_Aval, @Dx_CURP_Aval, " +
                            "@Dx_Tel_Aval, @Fg_Sexo_Aval, @Dx_Domicilio_Aval_Calle, @Dx_Domicilio_Aval_Num, @Dx_Domicilio_Aval_Colonia, " +
                            "@Dx_Domicilio_Aval_CP,@Cve_Estado_Aval, @Cve_Deleg_Municipio_Aval, @Mt_Ventas_Mes_Aval, " +
                            "@Mt_Gastos_Mes_Aval, @Mt_Ingreso_Neto_Mes_Aval, @No_RPU_AVAL, @Dx_No_Escritura_Aval, @Dt_Fecha_Escritura_Aval, @Dx_Nombre_Notario_Escritura_Aval, @Dx_No_Notario_Escritura_Aval, " +
                            "@Cve_Estado_Escritura_Aval, @Cve_Deleg_Municipio_Escritura_Aval, @Dt_Fecha_Gravamen,@Dx_Emite_Gravamen, " +
                            "@Dx_Num_Acta_Matrimonio_Aval, @Dx_Registro_Civil_Mat_Aval, @Dx_Nombre_Coacreditado, @Dx_RFC_CURP_Coacreditado, " +
                            "@Dx_Telefono_Coacreditado, @Fg_Sexo_Coacreditado, @Dx_Domicilio_Coacreditado_Calle, @Dx_Domicilio_Coacreditado_Num, " +
                            "@Dx_Domicilio_Coacreditado_Colonia, @Dx_Domicilio_Coacreditado_CP, @Cve_Estado_Coacreditado,@Cve_Deleg_Municipio_Coacreditado, @Dx_No_Escritura_Poder, @Dt_Fecha_Poder,@Dx_Nombre_Notario_Poder, " +
                            "@Dx_No_Notario_Poder, @Cve_Estado_Poder,@Cve_Deleg_Municipio_Poder, @Dx_No_Escritura_Acta, @Dt_Fecha_Acta, @Dx_Nombre_Notario_Acta, @Dx_No_Notario_Acta, @Cve_Estado_Acta, " +
                            "@Cve_Deleg_Municipio_Acta,@No_Ahorro_Energetico,@No_Ahorro_Economico, @Mt_Monto_Solicitado, @Mt_Monto_Total_Pagar, " +
                            "@Mt_Capacidad_Pago, @No_Plazo_Pago, @Cve_Periodo_Pago, @Pct_Tasa_Interes, @Pct_Tasa_Fija, @Pct_CAT,@Pct_Tasa_IVA, @Fg_Adquisicion_Sust, @Dt_Fecha_Cancelado, " +
                            "@Dt_Fecha_Pendiente, @Dt_Fecha_Por_entregar, @Dt_Fecha_En_revision, @Dt_Fecha_Autorizado, @Dt_Fecha_Rechazado, @Dt_Fecha_Finanzas, @Dt_Fecha_Ultmod, @Dx_Usr_Ultmod, @Pct_Tasa_IVA_Intereses, "+
                            "@Dt_Fecha_Beneficiario_con_adeudos, @Dt_Fecha_Tarifa_fuera_de_programa, @No_consumo_promedio,@Tipo_Usuario,@ATB04,@ATB05, @Mt_Gastos_Instalacion_Mano_Obra)";//edit by coco 2012-07-16

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", CreditModel.No_Credito),
                    new SqlParameter("@ID_Prog_Proy", CreditModel.ID_Prog_Proy),
                    new SqlParameter("@Id_Proveedor", CreditModel.Id_Proveedor),
                    new SqlParameter("@Cve_Estatus_Credito", CreditModel.Cve_Estatus_Credito),
                    new SqlParameter("@Cve_Tipo_Sociedad", CreditModel.Cve_Tipo_Sociedad),
                    new SqlParameter("@Cve_Tipo_Industria", CreditModel.Cve_Tipo_Industria),
                    new SqlParameter("@Dx_Nombre_Comercial", CreditModel.Dx_Nombre_Comercial),
                    new SqlParameter("@Dx_Razon_Social", CreditModel.Dx_Razon_Social),
                    new SqlParameter("@Dx_RFC", CreditModel.Dx_RFC),
                    new SqlParameter("@Dx_CURP", CreditModel.Dx_CURP),
                    new SqlParameter("@Dx_Nombre_Repre_Legal", CreditModel.Dx_Nombre_Repre_Legal),
                    new SqlParameter("@Cve_Acreditacion_Repre_legal", CreditModel.Cve_Acreditacion_Repre_legal),
                    new SqlParameter("@Cve_Identificacion_Repre_legal", CreditModel.Cve_Identificacion_Repre_legal),
                    new SqlParameter("@Dx_No_Identificacion_Repre_Legal", CreditModel.Dx_No_Identificacion_Repre_Legal),
                    new SqlParameter("@Fg_Sexo_Repre_legal", CreditModel.Fg_Sexo_Repre_legal),
                    new SqlParameter("@No_RPU", CreditModel.No_RPU),
                    new SqlParameter("@Fg_Edo_Civil_Repre_legal", CreditModel.Fg_Edo_Civil_Repre_legal),
                    new SqlParameter("@Cve_Reg_Conyugal_Repre_legal", CreditModel.Cve_Reg_Conyugal_Repre_legal),
                    new SqlParameter("@Dx_Email_Repre_legal", CreditModel.Dx_Email_Repre_legal),
                    new SqlParameter("@Dx_Domicilio_Fisc_Calle", CreditModel.Dx_Domicilio_Fisc_Calle),
                    new SqlParameter("@Dx_Domicilio_Fisc_Num", CreditModel.Dx_Domicilio_Fisc_Num),
                    new SqlParameter("@Dx_Domicilio_Fisc_Colonia", CreditModel.Dx_Domicilio_Fisc_Colonia),
                    new SqlParameter("@Dx_Domicilio_Fisc_CP", CreditModel.Dx_Domicilio_Fisc_CP),
                    new SqlParameter("@Cve_Estado_Fisc", CreditModel.Cve_Estado_Fisc),
                    new SqlParameter("@Cve_Deleg_Municipio_Fisc", CreditModel.Cve_Deleg_Municipio_Fisc),
                    new SqlParameter("@Cve_Tipo_Propiedad_Fisc", CreditModel.Cve_Tipo_Propiedad_Fisc),
                    new SqlParameter("@Dx_Tel_Fisc", CreditModel.Dx_Tel_Fisc),
                    new SqlParameter("@Fg_Mismo_Domicilio", CreditModel.Fg_Mismo_Domicilio),
                    new SqlParameter("@Dx_Domicilio_Neg_Calle", CreditModel.Dx_Domicilio_Neg_Calle),
                    new SqlParameter("@Dx_Domicilio_Neg_Num", CreditModel.Dx_Domicilio_Neg_Num),
                    new SqlParameter("@Dx_Domicilio_Neg_Colonia", CreditModel.Dx_Domicilio_Neg_Colonia),
                    new SqlParameter("@Dx_Domicilio_Neg_CP", CreditModel.Dx_Domicilio_Neg_CP),
                    new SqlParameter("@Cve_Estado_Neg", CreditModel.Cve_Estado_Neg),
                    new SqlParameter("@Cve_Deleg_Municipio_Neg", CreditModel.Cve_Deleg_Municipio_Neg),
                    new SqlParameter("@Cve_Tipo_Propiedad_Neg", CreditModel.Cve_Tipo_Propiedad_Neg),
                    new SqlParameter("@Dx_Tel_Neg", CreditModel.Dx_Tel_Neg),
                    new SqlParameter("@Mt_Ventas_Mes_Empresa", CreditModel.Mt_Ventas_Mes_Empresa),
                    new SqlParameter("@Mt_Gastos_Mes_Empresa", CreditModel.Mt_Gastos_Mes_Empresa),
                    new SqlParameter("@Mt_Ingreso_Neto_Mes_Empresa", CreditModel.Mt_Ingreso_Neto_Mes_Empresa),
                    
                    // RSA detailed Aval information for RFC validation
                    new SqlParameter("@Dx_Nombre_Aval", CreditModel.Dx_Nombre_Aval),
                    new SqlParameter("@Dx_First_Name_Aval", CreditModel.Dx_First_Name_Aval),
                    new SqlParameter("@Dx_Last_Name_Aval", CreditModel.Dx_Last_Name_Aval),
                    new SqlParameter("@Dx_Mother_Name_Aval", CreditModel.Dx_Mother_Name_Aval),
                    new SqlParameter("@Dt_BirthDate_Aval", CreditModel.Dt_BirthDate_Aval),
                    new SqlParameter("@Dx_RFC_CURP_Aval", CreditModel.Dx_RFC_CURP_Aval),
                    new SqlParameter("@Dx_RFC_Aval", CreditModel.Dx_RFC_Aval),
                    new SqlParameter("@Dx_CURP_Aval", CreditModel.Dx_CURP_Aval),

                    new SqlParameter("@Dx_Tel_Aval", CreditModel.Dx_Tel_Aval),
                    new SqlParameter("@Fg_Sexo_Aval", CreditModel.Fg_Sexo_Aval),
                    new SqlParameter("@Dx_Domicilio_Aval_Calle", CreditModel.Dx_Domicilio_Aval_Calle),
                    new SqlParameter("@Dx_Domicilio_Aval_Num", CreditModel.Dx_Domicilio_Aval_Num),
                    new SqlParameter("@Dx_Domicilio_Aval_Colonia", CreditModel.Dx_Domicilio_Aval_Colonia),
                    new SqlParameter("@Dx_Domicilio_Aval_CP", CreditModel.Dx_Domicilio_Aval_CP),
                    new SqlParameter("@Cve_Estado_Aval", CreditModel.Cve_Estado_Aval),
                    new SqlParameter("@Cve_Deleg_Municipio_Aval", CreditModel.Cve_Deleg_Municipio_Aval),
                    new SqlParameter("@Mt_Ventas_Mes_Aval", CreditModel.Mt_Ventas_Mes_Aval),
                    new SqlParameter("@Mt_Gastos_Mes_Aval", CreditModel.Mt_Gastos_Mes_Aval),
                    new SqlParameter("@Mt_Ingreso_Neto_Mes_Aval", CreditModel.Mt_Ingreso_Neto_Mes_Aval),
                    new SqlParameter("@No_RPU_AVAL", CreditModel.No_RPU_AVAL),
                    new SqlParameter("@Dx_No_Escritura_Aval", CreditModel.Dx_No_Escritura_Aval),
                    new SqlParameter("@Dt_Fecha_Escritura_Aval", CreditModel.Dt_Fecha_Escritura_Aval),
                    new SqlParameter("@Dx_Nombre_Notario_Escritura_Aval", CreditModel.Dx_Nombre_Notario_Escritura_Aval),
                    new SqlParameter("@Dx_No_Notario_Escritura_Aval", CreditModel.Dx_No_Notario_Escritura_Aval),
                    new SqlParameter("@Cve_Estado_Escritura_Aval", CreditModel.Cve_Estado_Escritura_Aval),
                    new SqlParameter("@Cve_Deleg_Municipio_Escritura_Aval", CreditModel.Cve_Deleg_Municipio_Escritura_Aval),
                    new SqlParameter("@Dt_Fecha_Gravamen", CreditModel.Dt_Fecha_Gravamen),
                    new SqlParameter("@Dx_Emite_Gravamen", CreditModel.Dx_Emite_Gravamen),
                    new SqlParameter("@Dx_Num_Acta_Matrimonio_Aval", CreditModel.Dx_Num_Acta_Matrimonio_Aval),
                    new SqlParameter("@Dx_Registro_Civil_Mat_Aval", CreditModel.Dx_Registro_Civil_Mat_Aval),
                    new SqlParameter("@Dx_Nombre_Coacreditado", CreditModel.Dx_Nombre_Coacreditado),
                    new SqlParameter("@Dx_RFC_CURP_Coacreditado", CreditModel.Dx_RFC_CURP_Coacreditado),
                    new SqlParameter("@Dx_Telefono_Coacreditado", CreditModel.Dx_Telefono_Coacreditado),
                    new SqlParameter("@Fg_Sexo_Coacreditado", CreditModel.Fg_Sexo_Coacreditado),
                    new SqlParameter("@Dx_Domicilio_Coacreditado_Calle", CreditModel.Dx_Domicilio_Coacreditado_Calle),
                    new SqlParameter("@Dx_Domicilio_Coacreditado_Num", CreditModel.Dx_Domicilio_Coacreditado_Num),
                    new SqlParameter("@Dx_Domicilio_Coacreditado_Colonia", CreditModel.Dx_Domicilio_Coacreditado_Colonia),
                    new SqlParameter("@Dx_Domicilio_Coacreditado_CP", CreditModel.Dx_Domicilio_Coacreditado_CP),
                    new SqlParameter("@Cve_Estado_Coacreditado", CreditModel.Cve_Estado_Coacreditado),
                    new SqlParameter("@Cve_Deleg_Municipio_Coacreditado", CreditModel.Cve_Deleg_Municipio_Coacreditado),
                    new SqlParameter("@Dx_No_Escritura_Poder", CreditModel.Dx_No_Escritura_Poder),
                    new SqlParameter("@Dt_Fecha_Poder", CreditModel.Dt_Fecha_Poder),
                    new SqlParameter("@Dx_Nombre_Notario_Poder", CreditModel.Dx_Nombre_Notario_Poder),
                    new SqlParameter("@Dx_No_Notario_Poder", CreditModel.Dx_No_Notario_Poder),
                    new SqlParameter("@Cve_Estado_Poder", CreditModel.Cve_Estado_Poder),
                    new SqlParameter("@Cve_Deleg_Municipio_Poder", CreditModel.Cve_Deleg_Municipio_Poder),
                    new SqlParameter("@Dx_No_Escritura_Acta", CreditModel.Dx_No_Escritura_Acta),
                    new SqlParameter("@Dt_Fecha_Acta", CreditModel.Dt_Fecha_Acta),
                    new SqlParameter("@Dx_Nombre_Notario_Acta", CreditModel.Dx_Nombre_Notario_Acta),
                    new SqlParameter("@Dx_No_Notario_Acta", CreditModel.Dx_No_Notario_Acta),
                    new SqlParameter("@Cve_Estado_Acta", CreditModel.Cve_Estado_Acta),
                    new SqlParameter("@Cve_Deleg_Municipio_Acta", CreditModel.Cve_Deleg_Municipio_Acta),
                    new SqlParameter("@No_Ahorro_Energetico", CreditModel.No_Ahorro_Energetico),
                    new SqlParameter("@No_Ahorro_Economico", CreditModel.No_Ahorro_Economico),
                    new SqlParameter("@Mt_Monto_Solicitado", CreditModel.Mt_Monto_Solicitado),
                    new SqlParameter("@Mt_Monto_Total_Pagar", CreditModel.Mt_Monto_Total_Pagar),
                    new SqlParameter("@Mt_Capacidad_Pago", CreditModel.Mt_Capacidad_Pago),
                    new SqlParameter("@No_Plazo_Pago", CreditModel.No_Plazo_Pago),
                    new SqlParameter("@Cve_Periodo_Pago", CreditModel.Cve_Periodo_Pago),
                    new SqlParameter("@Pct_Tasa_Interes", CreditModel.Pct_Tasa_Interes),
                    new SqlParameter("@Pct_Tasa_Fija", CreditModel.Pct_Tasa_Fija),
                    new SqlParameter("@Pct_CAT", CreditModel.Pct_CAT),
                    new SqlParameter("@Pct_Tasa_IVA", CreditModel.Pct_Tasa_IVA),
                    new SqlParameter("@Fg_Adquisicion_Sust", CreditModel.Fg_Adquisicion_Sust),
                    new SqlParameter("@Dt_Fecha_Cancelado", CreditModel.Dt_Fecha_Cancelado),
                    new SqlParameter("@Dt_Fecha_Pendiente", CreditModel.Dt_Fecha_Pendiente),
                    new SqlParameter("@Dt_Fecha_Por_entregar", CreditModel.Dt_Fecha_Por_entregar),
                    new SqlParameter("@Dt_Fecha_En_revision", CreditModel.Dt_Fecha_En_revision),
                    new SqlParameter("@Dt_Fecha_Autorizado", CreditModel.Dt_Fecha_Autorizado),
                    new SqlParameter("@Dt_Fecha_Rechazado", CreditModel.Dt_Fecha_Rechazado),
                    new SqlParameter("@Dt_Fecha_Finanzas", CreditModel.Dt_Fecha_Finanzas),
                    new SqlParameter("@Dt_Fecha_Ultmod", CreditModel.Dt_Fecha_Ultmod),
                    new SqlParameter("@Dx_Usr_Ultmod", CreditModel.Dx_Usr_Ultmod),
                    new SqlParameter("@Pct_Tasa_IVA_Intereses", CreditModel.Pct_Tasa_IVA_Intereses),
                    new SqlParameter("@Dt_Fecha_Beneficiario_con_adeudos", CreditModel.Dt_Fecha_Beneficiario_con_adeudos),
                    new SqlParameter("@Dt_Fecha_Tarifa_fuera_de_programa", CreditModel.Dt_Fecha_Tarifa_fuera_de_programa),
                    new SqlParameter("@No_consumo_promedio", CreditModel.No_consumo_promedio),                   
                    new SqlParameter("@Tipo_Usuario",CreditModel.Tipo_Usuario),
                     //edit by coco 2012-07-16
                    new SqlParameter("@ATB04",CreditModel.Telephone),
                    new SqlParameter("@ATB05",CreditModel.Email),
                    //end add

                    new SqlParameter("@Mt_Gastos_Instalacion_Mano_Obra", CreditModel.Mt_Gastos_Instalacion_Mano_Obra)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add credit failed: Execute method AddCredit in CreditDal.", ex, true);
            }

            return iResult;
        }
        /// <summary>
        /// update No_Ahorro_Energetico No_Ahorro_Economico Mt_Capacidad_Pago
        /// </summary>
        /// <param name="creditModel"></param>
        /// <returns></returns>
        public int updateCreditAccountFilter(CreditEntity creditModel)
        {
            int iUpdate = 0;
            try
            {
                string SQL = "update K_CREDITO set Mt_Monto_Solicitado =@Mt_Monto_Solicitado ,No_Ahorro_Economico=@No_Ahorro_Economico,Mt_Capacidad_Pago=@Mt_Capacidad_Pago where No_Credito =@No_Credito";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditModel.No_Credito),
                    new SqlParameter("@Mt_Monto_Solicitado", creditModel.Mt_Monto_Solicitado),
                    new SqlParameter("@No_Ahorro_Economico", creditModel.No_Ahorro_Economico),
                    new SqlParameter("@Mt_Capacidad_Pago", creditModel.Mt_Capacidad_Pago)};
                iUpdate = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return iUpdate;
        }
        /// <summary>
        /// Update credit with credit entity
        /// </summary>
        /// <param name="CreditModel">Credit Entity</param>
        /// <returns></returns>
        public int UpdateCredit(CreditEntity CreditModel)
        {
            int iResult = 0;
            string SQL = "";          
            try
            {
                SQL = "UPDATE [dbo].[K_CREDITO] SET " +
                                "[Cve_Estatus_Credito] = @Cve_Estatus_Credito, [Dx_No_Escritura_Poder] = @Dx_No_Escritura_Poder, [Dt_Fecha_Poder] = @Dt_Fecha_Poder, [Dx_Nombre_Notario_Poder] = @Dx_Nombre_Notario_Poder, [Dx_No_Notario_Poder] = @Dx_No_Notario_Poder, " +
                                "[Cve_Estado_Poder] = @Cve_Estado_Poder, [Cve_Deleg_Municipio_Poder] = @Cve_Deleg_Municipio_Poder, [Dx_No_Escritura_Acta] = @Dx_No_Escritura_Acta, [Dt_Fecha_Acta] = @Dt_Fecha_Acta, " +
                                "[Dx_Nombre_Notario_Acta] = @Dx_Nombre_Notario_Acta, [Dx_No_Notario_Acta] = @Dx_No_Notario_Acta, [Cve_Estado_Acta] = @Cve_Estado_Acta, [Cve_Deleg_Municipio_Acta] = @Cve_Deleg_Municipio_Acta, " +
                    /*"[No_Ahorro_Energetico] = @No_Ahorro_Energetico, [No_Ahorro_Economico] = @No_Ahorro_Economico, [Mt_Monto_Solicitado] = @Mt_Monto_Solicitado, [Mt_Monto_Total_Pagar] = @Mt_Monto_Total_Pagar, "+
                    "[Mt_Capacidad_Pago] = @Mt_Capacidad_Pago, [No_Plazo_Pago] = @No_Plazo_Pago, [Cve_Periodo_Pago] = @Cve_Periodo_Pago, "+;*/
                                "[Dx_No_Escritura_Aval] = @Dx_No_Escritura_Aval, [Dt_Fecha_Escritura_Aval] = @Dt_Fecha_Escritura_Aval, [Dx_Nombre_Notario_Escritura_Aval]=@Dx_Nombre_Notario_Escritura_Aval, [Dx_No_Notario_Escritura_Aval]=@Dx_No_Notario_Escritura_Aval, " +
                                "[Cve_Estado_Escritura_Aval] = @Cve_Estado_Escritura_Aval, [Cve_Deleg_Municipio_Escritura_Aval]=@Cve_Deleg_Municipio_Escritura_Aval, [Dt_Fecha_Gravamen] = @Dt_Fecha_Gravamen, " +
                                "[Dx_Emite_Gravamen] = @Dx_Emite_Gravamen, [Dx_Num_Acta_Matrimonio_Aval] = @Dx_Num_Acta_Matrimonio_Aval, [Dx_Registro_Civil_Mat_Aval]=@Dx_Registro_Civil_Mat_Aval, " +
                                "[Fg_Adquisicion_Sust] = @Fg_Adquisicion_Sust, [Dt_Fecha_Por_entregar] = @Dt_Fecha_Por_entregar, [Dx_Usr_Ultmod] = @Dx_Usr_Ultmod, [Dt_Fecha_Ultmod] = @Dt_Fecha_Ultmod WHERE [No_Credito] = @No_Credito";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", CreditModel.No_Credito),
                    new SqlParameter("@Cve_Estatus_Credito", CreditModel.Cve_Estatus_Credito),
                    new SqlParameter("@Dx_No_Escritura_Poder", CreditModel.Dx_No_Escritura_Poder),
                    new SqlParameter("@Dt_Fecha_Poder", CreditModel.Dt_Fecha_Poder),
                    new SqlParameter("@Dx_Nombre_Notario_Poder", CreditModel.Dx_Nombre_Notario_Poder),
                    new SqlParameter("@Dx_No_Notario_Poder", CreditModel.Dx_No_Notario_Poder),
                    new SqlParameter("@Cve_Estado_Poder", CreditModel.Cve_Estado_Poder),
                    new SqlParameter("@Cve_Deleg_Municipio_Poder", CreditModel.Cve_Deleg_Municipio_Poder),
                    new SqlParameter("@Dx_No_Escritura_Acta", CreditModel.Dx_No_Escritura_Acta),
                    new SqlParameter("@Dt_Fecha_Acta", CreditModel.Dt_Fecha_Acta),
                    new SqlParameter("@Dx_Nombre_Notario_Acta", CreditModel.Dx_Nombre_Notario_Acta),
                    new SqlParameter("@Dx_No_Notario_Acta", CreditModel.Dx_No_Notario_Acta),
                    new SqlParameter("@Cve_Estado_Acta", CreditModel.Cve_Estado_Acta),
                    new SqlParameter("@Cve_Deleg_Municipio_Acta", CreditModel.Cve_Deleg_Municipio_Acta),
                    new SqlParameter("@Dx_No_Escritura_Aval", CreditModel.Dx_No_Escritura_Aval),
                    new SqlParameter("@Dt_Fecha_Escritura_Aval",CreditModel.Dt_Fecha_Escritura_Aval),
                    new SqlParameter("@Dx_Nombre_Notario_Escritura_Aval", CreditModel.Dx_Nombre_Notario_Escritura_Aval),
                    new SqlParameter("@Dx_No_Notario_Escritura_Aval", CreditModel.Dx_No_Notario_Escritura_Aval),
                    new SqlParameter("@Cve_Estado_Escritura_Aval", CreditModel.Cve_Estado_Escritura_Aval),
                    new SqlParameter("@Cve_Deleg_Municipio_Escritura_Aval", CreditModel.Cve_Deleg_Municipio_Escritura_Aval),
                    new SqlParameter("@Dt_Fecha_Gravamen", CreditModel.Dt_Fecha_Gravamen),
                    new SqlParameter("@Dx_Emite_Gravamen", CreditModel.Dx_Emite_Gravamen),
                    new SqlParameter("@Dx_Num_Acta_Matrimonio_Aval", CreditModel.Dx_Num_Acta_Matrimonio_Aval),
                    new SqlParameter("@Dx_Registro_Civil_Mat_Aval", CreditModel.Dx_Registro_Civil_Mat_Aval),
                    new SqlParameter("@Fg_Adquisicion_Sust", CreditModel.Fg_Adquisicion_Sust),
                    new SqlParameter("@Dt_Fecha_Por_entregar", CreditModel.Dt_Fecha_Por_entregar),
                    new SqlParameter("@Dx_Usr_Ultmod", CreditModel.Dx_Usr_Ultmod),
                    new SqlParameter("@Dt_Fecha_Ultmod", DateTime.Now)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update credit failed: Execute method UpdateCredit in CreditDal.", ex, true);
            }
            return iResult;
        }
        /// <summary>
        /// Cancel the credits with pending status for some days
        /// </summary>
        /// <param name="creditNumber">Credit Number</param>
        /// <param name="statusID">Status ID</param>
        /// <param name="cancelDate">Cancel Date</param>
        /// <param name="updateUser">Update User Name</param>
        /// <param name="userDate">User Date</param>
        /// <param name="connString"></param>
        /// <returns></returns>
        public int CancelCredit(string creditNumber, int statusID, DateTime cancelDate, string updateUser, DateTime userDate, string connString)
        {
            int iResult = 0;
            string SQL = "";
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                SQL = "UPDATE [dbo].[K_CREDITO] SET  "
                            + "[Cve_Estatus_Credito] = @Cve_Estatus_Credito, [Dt_Fecha_Cancelado] = @Dt_Fecha_Cancelado, [Dx_Usr_Ultmod] = @Dx_Usr_Ultmod, [Dt_Fecha_Ultmod] = @Dt_Fecha_Ultmod   WHERE [No_Credito] = @No_Credito";
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@No_Credito", creditNumber);
                cmd.Parameters.AddWithValue("@Cve_Estatus_Credito", statusID);
                cmd.Parameters.AddWithValue("@Dt_Fecha_Cancelado", cancelDate);
                cmd.Parameters.AddWithValue("@Dx_Usr_Ultmod", updateUser);
                cmd.Parameters.AddWithValue("@Dt_Fecha_Ultmod", userDate);
                conn.Open();

                iResult = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Cancel credit failed: Execute method CancelCredit in CreditDal.", ex, false);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
            }

            return iResult;
        }
        /// <summary>
        /// Cancel the credits with pending status for some days
        /// </summary>
        /// <param name="creditNumber">Credit Number</param>
        /// <param name="statusID">Status ID</param>
        /// <param name="cancelDate">Cancel Date</param>
        /// <param name="updateUser">Update User Name</param>
        /// <param name="userDate">User Date</param>
        /// <returns></returns>
        public int CancelCredit(string creditNumber, string updateUser)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "UPDATE [dbo].[K_CREDITO] SET  "
                            + "[Cve_Estatus_Credito] = @Cve_Estatus_Credito, [Dt_Fecha_Cancelado] = @Dt_Fecha_Cancelado, [Dx_Usr_Ultmod] = @Dx_Usr_Ultmod, [Dt_Fecha_Ultmod] = @Dt_Fecha_Ultmod   WHERE [No_Credito] = @No_Credito";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditNumber),
                    new SqlParameter("@Cve_Estatus_Credito", (int)CreditStatus.CANCELADO),
                    new SqlParameter("@Dt_Fecha_Cancelado", DateTime.Now.Date),
                    new SqlParameter("@Dx_Usr_Ultmod", updateUser),
                    new SqlParameter("@Dt_Fecha_Ultmod", DateTime.Now.Date)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Cancel credit failed: Execute method CancelCredit in CreditDal.", ex, false);
            }

            return iResult;
        }
        /// <summary>
        /// En revision credit
        /// </summary>
        /// <param name="creditNumber">Credit Number</param>
        /// <param name="statusID">Status ID</param>
        /// <param name="enRevisionDate">En Revision Date</param>
        /// <param name="updateUser">Update User Name</param>
        /// <param name="userDate">User Date</param>
        /// <returns></returns>
        public int EnRevisionCredit(string creditNumber, int statusID, DateTime enRevisionDate, string updateUser, DateTime userDate)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "UPDATE [dbo].[K_CREDITO] SET  "
                            + "[Cve_Estatus_Credito] = @Cve_Estatus_Credito, [Dt_Fecha_En_revision] = @Dt_Fecha_En_revision, [Dx_Usr_Ultmod] = @Dx_Usr_Ultmod, [Dt_Fecha_Ultmod] = @Dt_Fecha_Ultmod   WHERE [No_Credito] = @No_Credito";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditNumber),
                    new SqlParameter("@Cve_Estatus_Credito", statusID),
                    new SqlParameter("@Dt_Fecha_En_revision", enRevisionDate),
                    new SqlParameter("@Dx_Usr_Ultmod", updateUser),
                    new SqlParameter("@Dt_Fecha_Ultmod", userDate)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "En revision credit failed: Execute method EnRevisionCredit in CreditDal.", ex, true);
            }

            return iResult;
        }
        /// <summary>
        /// Aprobar credit
        /// </summary>
        /// <param name="creditNumber">Credit Number</param>
        /// <param name="statusID">Status ID</param>
        /// <param name="aprovarDate">Aprovar Date</param>
        /// <param name="updateUser">Update User Name</param>
        /// <param name="userDate">User Date</param>
        /// <returns></returns>
        public int AprobarCredit(string creditNumber, int statusID, DateTime aprovarDate, string updateUser, DateTime userDate)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "UPDATE [dbo].[K_CREDITO] SET  "
                            + "[Cve_Estatus_Credito] = @Cve_Estatus_Credito, [Dt_Fecha_Autorizado] = @Dt_Fecha_Autorizado, [Dx_Usr_Ultmod] = @Dx_Usr_Ultmod, [Dt_Fecha_Ultmod] = @Dt_Fecha_Ultmod  WHERE [No_Credito] = @No_Credito";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditNumber),
                    new SqlParameter("@Cve_Estatus_Credito", statusID),
                    new SqlParameter("@Dt_Fecha_Autorizado", aprovarDate),
                    new SqlParameter("@Dx_Usr_Ultmod", updateUser),
                    new SqlParameter("@Dt_Fecha_Ultmod", userDate)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Aprovar credit failed: Execute method AprobarCredit in CreditDal.", ex, true);
            }

            return iResult;
        }
        /// <summary>
        /// Rechazar credit
        /// </summary>
        /// <param name="creditNumber">Credit Number</param>
        /// <param name="statusID">Status ID</param>
        /// <param name="rechazarDate">Rechazar Date</param>
        /// <param name="updateUser">Update User Name</param>
        /// <param name="userDate">User Date</param>
        /// <returns></returns>
        public int RechazarCredit(string creditNumber, int statusID, DateTime rechazarDate, string updateUser, DateTime userDate)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "UPDATE [dbo].[K_CREDITO] SET  "
                            + "[Cve_Estatus_Credito] = @Cve_Estatus_Credito, [Dt_Fecha_Rechazado] = @Dt_Fecha_Rechazado, [Dx_Usr_Ultmod] = @Dx_Usr_Ultmod, [Dt_Fecha_Ultmod] = @Dt_Fecha_Ultmod    WHERE [No_Credito] = @No_Credito";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditNumber),
                    new SqlParameter("@Cve_Estatus_Credito", statusID),
                    new SqlParameter("@Dt_Fecha_Rechazado", rechazarDate),
                    new SqlParameter("@Dx_Usr_Ultmod", updateUser),
                    new SqlParameter("@Dt_Fecha_Ultmod", userDate)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Rechazar credit failed: Execute method RechazarCredit in CreditDal.", ex, true);
            }

            return iResult;
        }
        /// <summary>
        /// Pendiente credit
        /// </summary>
        /// <param name="creditNumber">Credit Number</param>
        /// <param name="statusID">Status ID</param>
        /// <param name="pendienteDate">Pendiente Date</param>
        /// <param name="updateUser">Update User Name</param>
        /// <param name="userDate">User Date</param>
        /// <returns></returns>
        public int PendienteCredit(string creditNumber, int statusID, DateTime pendienteDate, string updateUser, DateTime userDate)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "UPDATE [dbo].[K_CREDITO] SET  "
                            + "[Cve_Estatus_Credito] = @Cve_Estatus_Credito, [Dt_Fecha_Pendiente] = @Dt_Fecha_Pendiente, [Dx_Usr_Ultmod] = @Dx_Usr_Ultmod, [Dt_Fecha_Ultmod] = @Dt_Fecha_Ultmod   WHERE [No_Credito] = @No_Credito";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditNumber),
                    new SqlParameter("@Cve_Estatus_Credito", statusID),
                    new SqlParameter("@Dt_Fecha_Pendiente", pendienteDate),
                    new SqlParameter("@Dx_Usr_Ultmod", updateUser),
                    new SqlParameter("@Dt_Fecha_Ultmod", userDate)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Pendiente credit failed: Execute method PendienteCredit in CreditDal.", ex, true);
            }

            return iResult;
        }

        /// <summary>
        /// Calificación Mop No Valid Credit
        /// </summary>
        /// <param name="creditNumber">Credit Number</param>
        /// <param name="statusID">Status ID</param>
        /// <param name="NoValidDate">Invalid Date</param>
        /// <param name="updateUser">Update User Name</param>
        /// <param name="userDate">User Date</param>
        /// <returns></returns>
        public int CalificacionMopNoValidCredit(string creditNumber, int statusID, DateTime NoValidDate, string updateUser, DateTime userDate)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "UPDATE [dbo].[K_CREDITO] SET  "
                            + "[Cve_Estatus_Credito] = @Cve_Estatus_Credito, [Dt_Fecha_Calificación_MOP_no_válida] = @Dt_Fecha_Calificación_MOP_no_válida, [Dx_Usr_Ultmod] = @Dx_Usr_Ultmod, [Dt_Fecha_Ultmod] = @Dt_Fecha_Ultmod   WHERE [No_Credito] = @No_Credito";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditNumber),
                    new SqlParameter("@Cve_Estatus_Credito", statusID),
                    new SqlParameter("@Dt_Fecha_Calificación_MOP_no_válida", NoValidDate),
                    new SqlParameter("@Dx_Usr_Ultmod", updateUser),
                    new SqlParameter("@Dt_Fecha_Ultmod", userDate)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Pendiente credit failed: Execute method PendienteCredit in CreditDal.", ex, true);
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
                        WhereBuilder.Append(" and ([Id_Proveedor] = " + proveedor + " and [Tipo_Usuario] = '" + userType + "' OR (Id_Proveedor in (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Id_Proveedor = " + proveedor + ") AND Tipo_Usuario = 'S_B'))");
                    }
                    else
                    {
                        WhereBuilder.Append(" and [Id_Proveedor] = " + proveedor + " and [Tipo_Usuario] = '" + userType + "'");
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
                        strWhere = "Where (Id_Proveedor = " + proveedor + " and Tipo_Usuario = '" + userType + "' OR (Id_Proveedor in (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Id_Proveedor = " + proveedor + ") AND Tipo_Usuario = 'S_B'))";
                    }
                    else
                    {
                        strWhere = "Where Id_Proveedor = " + proveedor + " and Tipo_Usuario = '" + userType + "'";
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
                                            "OR (Id_Proveedor IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona = " + filter + ") and Tipo_Usuario = 'S_B')";
                    }
                    else
                    {
                        strWhere = "Where (Id_Proveedor IN (SELECT Id_Proveedor FROM CAT_PROVEEDOR WHERE Cve_Zona IN ( SELECT Cve_Zona FROM CAT_ZONA WHERE Cve_Region = " + filter + ")) and Tipo_Usuario = 'S')" +
                                            "OR (Id_Proveedor IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona IN ( SELECT Cve_Zona FROM CAT_ZONA WHERE Cve_Region = " + filter + ")) and Tipo_Usuario = 'S_B')";
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
                                                "OR (Id_Proveedor IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona IN ( SELECT Cve_Zona FROM CAT_ZONA WHERE Cve_Region = " + filter + ")) and Tipo_Usuario = 'S_B'))";
                        }
                        else if (proveedor > 0 && proveedorBranch <= 0)
                        {
                            strWhere += "Where Id_Proveedor =" + proveedor + " and Tipo_Usuario = 'S'";
                        }
                        else if (proveedor <= 0 && proveedorBranch > 0)
                        {
                            strWhere += "Where Id_Proveedor = " + proveedorBranch + " and Tipo_Usuario = 'S_B'";
                        }
                        else
                        {
                            strWhere += "Where ((Id_Proveedor  = " + proveedor + " and Tipo_Usuario = 'S') " +
                                                "OR (Id_Proveedor  = " + proveedorBranch + " and Tipo_Usuario = 'S_B'))";
                        }
                    }
                    else
                    {
                        if (proveedor <= 0 && proveedorBranch <= 0)
                        {
                            strWhere += "Where ((Id_Proveedor IN (SELECT Id_Proveedor FROM CAT_PROVEEDOR WHERE Cve_Zona = " + filter + ") and Tipo_Usuario = 'S') " +
                                                "OR (Id_Proveedor IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona = " + filter + ") and Tipo_Usuario = 'S_B'))";
                        }
                        else if (proveedor > 0 && proveedorBranch <= 0)
                        {
                            strWhere += "Where Id_Proveedor =" + proveedor + " and Tipo_Usuario = 'S'";
                        }
                        else if (proveedor <= 0 && proveedorBranch > 0)
                        {
                            strWhere += "Where Id_Proveedor = " + proveedorBranch + " and Tipo_Usuario = 'S_B'";
                        }
                        else
                        {
                            strWhere += "Where ((Id_Proveedor  = " + proveedor + " and Tipo_Usuario = 'S') " +
                                                "OR (Id_Proveedor  = " + proveedorBranch + " and Tipo_Usuario = 'S_B'))";
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

            string sql = "select k.*,p.Dx_Periodo_Pago,e.Dx_Cve_CC,m.Dx_Deleg_Municipio,e.Dx_Cve_PM from K_CREDITO k " +
                         " left outer join CAT_PERIODO_PAGO p on k.Cve_Periodo_Pago=p.Cve_Periodo_Pago " +
                         " left outer join CAT_ESTADO e on k.Cve_Estado_Fisc=e.Cve_Estado " +
                         " left outer join CAT_DELEG_MUNICIPIO m on k.Cve_Deleg_Municipio_Fisc=m.Cve_Deleg_Municipio " +
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
                SQL = "SELECT DISTINCT Dx_Razon_Social FROM [dbo].[K_CREDITO] WHERE [Id_Proveedor] = @Id_Proveedor AND [Tipo_Usuario] = @Tipo_Usuario";
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
                SQL = "SELECT DISTINCT CONVERT(VARCHAR(10),Dt_Fecha_Pendiente, 120) AS Dt_Fecha_Pendiente FROM [dbo].[K_CREDITO]  WHERE [Id_Proveedor] = @Id_Proveedor AND [Tipo_Usuario] = @Tipo_Usuario";
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
                SQL = "SELECT TOP 1  *  FROM  [dbo].[K_CREDITO] where No_Credito=@CreditNo";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@CreditNo", creditNo)
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
        /// Update credit by regional or supplier
        /// </summary>
        /// <param name="CreditModel"></param>
        /// <returns></returns>
        public int UpdateCreditApproveInfo(CreditEntity CreditModel)
        {
            int iResult = 0;
            string SQL = "";

            try
            {
                SQL = @" UPDATE dbo.K_CREDITO SET 
                         Dx_Razon_Social =   @Dx_Razon_Social   ,
                         Cve_Tipo_Industria          =   @Cve_Tipo_Industria   ,
                         Cve_Tipo_Sociedad       =   @Cve_Tipo_Sociedad   ,
                         Dx_CURP       =   @Dx_CURP   ,
                         Dx_Nombre_Repre_Legal       =   @Dx_Nombre_Repre_Legal   ,
                         Cve_Acreditacion_Repre_legal     =   @Cve_Acreditacion_Repre_legal   ,
                         Fg_Sexo_Repre_legal     =   @Fg_Sexo_Repre_legal  ,
                         No_RPU     =   @No_RPU   ,
                         Fg_Edo_Civil_Repre_legal    =   @Fg_Edo_Civil_Repre_legal  , 
                         Cve_Reg_Conyugal_Repre_legal     =   @Cve_Reg_Conyugal_Repre_legal   ,
                         Cve_Identificacion_Repre_legal     =   @Cve_Identificacion_Repre_legal   ,
                         Dx_No_Identificacion_Repre_Legal      =   @Dx_No_Identificacion_Repre_Legal   ,
                         Mt_Ventas_Mes_Empresa         =   @Mt_Ventas_Mes_Empresa   ,
                         Mt_Gastos_Mes_Empresa       =   @Mt_Gastos_Mes_Empresa   ,
                         Dx_Email_Repre_legal    =   @Dx_Email_Repre_legal   ,
                         Dx_Domicilio_Fisc_Calle    =   @Dx_Domicilio_Fisc_Calle   ,
                         Dx_Domicilio_Fisc_Num    =   @Dx_Domicilio_Fisc_Num    ,
                         Dx_Domicilio_Fisc_CP    =   @Dx_Domicilio_Fisc_CP   ,
                         Cve_Estado_Fisc    =   @Cve_Estado_Fisc   ,
                         Cve_Deleg_Municipio_Fisc   =   @Cve_Deleg_Municipio_Fisc   ,
                         Cve_Tipo_Propiedad_Fisc    =   @Cve_Tipo_Propiedad_Fisc   ,
                         Dx_Tel_Fisc    =   @Dx_Tel_Fisc   ,
                         Fg_Mismo_Domicilio     =   @Fg_Mismo_Domicilio  ,
                         Dx_Domicilio_Neg_Calle   =   @Dx_Domicilio_Neg_Calle   ,
                         Dx_Domicilio_Neg_Num  =   @Dx_Domicilio_Neg_Num   ,
                         Dx_Domicilio_Neg_CP   =   @Dx_Domicilio_Neg_CP   ,
                         Cve_Estado_Neg    =   @Cve_Estado_Neg  ,
                         Cve_Deleg_Municipio_Neg    =   @Cve_Deleg_Municipio_Neg  ,
                         Cve_Tipo_Propiedad_Neg    =   @Cve_Tipo_Propiedad_Neg   ,
                         Dx_Tel_Neg     =   @Dx_Tel_Neg   ,
                         Dx_Nombre_Aval     =   @Dx_Nombre_Aval   ,
                         Dx_First_Name_Aval     =   @Dx_First_Name_Aval   ,
                         Dx_Last_Name_Aval     =   @Dx_Last_Name_Aval   ,
                         Dx_Mother_Name_Aval     =   @Dx_Mother_Name_Aval   ,
                         Dt_BirthDate_Aval     =   @Dt_BirthDate_Aval   ,
                         Dx_RFC_CURP_Aval      =   @Dx_RFC_CURP_Aval   ,
                         Dx_RFC_Aval      =   @Dx_RFC_Aval   ,
                         Dx_CURP_Aval      =   @Dx_CURP_Aval   ,
                         Dx_Tel_Aval     =   @Dx_Tel_Aval   ,
                         Fg_Sexo_Aval    =   @Fg_Sexo_Aval   ,
                         Dx_Domicilio_Aval_Calle    =   @Dx_Domicilio_Aval_Calle  ,
                         Dx_Domicilio_Aval_Num    =   @Dx_Domicilio_Aval_Num   ,
                         Dx_Domicilio_Aval_CP    =   @Dx_Domicilio_Aval_CP   ,
                         Cve_Estado_Aval   =   @Cve_Estado_Aval  ,
                         Cve_Deleg_Municipio_Aval   =   @Cve_Deleg_Municipio_Aval ,
                         Mt_Ventas_Mes_Aval    =   @Mt_Ventas_Mes_Aval ,
                         Mt_Gastos_Mes_Aval   =   @Mt_Gastos_Mes_Aval  ,
                         Dx_Nombre_Coacreditado    =   @Dx_Nombre_Coacreditado   ,
                         Dx_RFC_CURP_Coacreditado   =   @Dx_RFC_CURP_Coacreditado   ,
                         Fg_Sexo_Coacreditado   =   @Fg_Sexo_Coacreditado   ,
                         Dx_Telefono_Coacreditado   =   @Dx_Telefono_Coacreditado  ,
                         Dx_Domicilio_Coacreditado_Calle   =   @Dx_Domicilio_Coacreditado_Calle   ,
                         Dx_Domicilio_Coacreditado_Num   =   @Dx_Domicilio_Coacreditado_Num   ,
                         Dx_Domicilio_Coacreditado_CP    =   @Dx_Domicilio_Coacreditado_CP   ,
                         Cve_Estado_Coacreditado   =   @Cve_Estado_Coacreditado   ,
                         Cve_Deleg_Municipio_Coacreditado   =   @Cve_Deleg_Municipio_Coacreditado  ,
                         Dt_Fecha_Ultmod   =   @Dt_Fecha_Ultmod   ,
                         Dx_Usr_Ultmod =   @Dx_Usr_Ultmod   
                         where No_Credito  =   @No_Credito ";

                SqlParameter[] paras = new SqlParameter[] { 
                        new SqlParameter("@No_Credito", CreditModel.No_Credito),
                        new SqlParameter("@Dx_Razon_Social",CreditModel.Dx_Razon_Social  ),
                        new SqlParameter("@Cve_Tipo_Industria",         CreditModel.Cve_Tipo_Industria  ),
                        new SqlParameter("@Cve_Tipo_Sociedad",      CreditModel.Cve_Tipo_Sociedad  ),
                        new SqlParameter("@Dx_CURP",      CreditModel.Dx_CURP  ),
                        new SqlParameter("@Dx_Nombre_Repre_Legal",      CreditModel.Dx_Nombre_Repre_Legal  ),
                        new SqlParameter("@Cve_Acreditacion_Repre_legal",    CreditModel.Cve_Acreditacion_Repre_legal  ),
                        new SqlParameter("@Fg_Sexo_Repre_legal",    CreditModel.Fg_Sexo_Repre_legal ),
                        new SqlParameter("@No_RPU",    CreditModel.No_RPU  ),
                        new SqlParameter("@Fg_Edo_Civil_Repre_legal",   CreditModel.Fg_Edo_Civil_Repre_legal ), 
                        new SqlParameter("@Cve_Reg_Conyugal_Repre_legal",    CreditModel.Cve_Reg_Conyugal_Repre_legal  ),
                        new SqlParameter("@Cve_Identificacion_Repre_legal",    CreditModel.Cve_Identificacion_Repre_legal  ),
                        new SqlParameter("@Dx_No_Identificacion_Repre_Legal",     CreditModel.Dx_No_Identificacion_Repre_Legal  ),
                        new SqlParameter("@Mt_Ventas_Mes_Empresa",        CreditModel.Mt_Ventas_Mes_Empresa  ),
                        new SqlParameter("@Mt_Gastos_Mes_Empresa",      CreditModel.Mt_Gastos_Mes_Empresa  ),
                        new SqlParameter("@Dx_Email_Repre_legal",   CreditModel.Dx_Email_Repre_legal  ),
                        new SqlParameter("@Dx_Domicilio_Fisc_Calle",   CreditModel.Dx_Domicilio_Fisc_Calle  ),
                        new SqlParameter("@Dx_Domicilio_Fisc_Num",   CreditModel.Dx_Domicilio_Fisc_Num   ),
                        new SqlParameter("@Dx_Domicilio_Fisc_CP",   CreditModel.Dx_Domicilio_Fisc_CP  ),
                        new SqlParameter("@Cve_Estado_Fisc",   CreditModel.Cve_Estado_Fisc  ),
                        new SqlParameter("@Cve_Deleg_Municipio_Fisc",  CreditModel.Cve_Deleg_Municipio_Fisc  ),
                        new SqlParameter("@Cve_Tipo_Propiedad_Fisc",   CreditModel.Cve_Tipo_Propiedad_Fisc  ),
                        new SqlParameter("@Dx_Tel_Fisc",   CreditModel.Dx_Tel_Fisc  ),
                        new SqlParameter("@Fg_Mismo_Domicilio",    CreditModel.Fg_Mismo_Domicilio ),
                        new SqlParameter("@Dx_Domicilio_Neg_Calle",  CreditModel.Dx_Domicilio_Neg_Calle  ),
                        new SqlParameter("@Dx_Domicilio_Neg_Num", CreditModel.Dx_Domicilio_Neg_Num  ),
                        new SqlParameter("@Dx_Domicilio_Neg_CP",  CreditModel.Dx_Domicilio_Neg_CP  ),
                        new SqlParameter("@Cve_Estado_Neg",   CreditModel.Cve_Estado_Neg ),
                        new SqlParameter("@Cve_Deleg_Municipio_Neg",   CreditModel.Cve_Deleg_Municipio_Neg ),
                        new SqlParameter("@Cve_Tipo_Propiedad_Neg",   CreditModel.Cve_Tipo_Propiedad_Neg  ),
                        new SqlParameter("@Dx_Tel_Neg",    CreditModel.Dx_Tel_Neg  ),
                        
                        // RSA detailed Aval information for RFC validation
                        new SqlParameter("@Dx_Nombre_Aval",    CreditModel.Dx_Nombre_Aval  ),
                        new SqlParameter("@Dx_First_Name_Aval",    CreditModel.Dx_First_Name_Aval  ),
                        new SqlParameter("@Dx_Last_Name_Aval",    CreditModel.Dx_Last_Name_Aval  ),
                        new SqlParameter("@Dx_Mother_Name_Aval",    CreditModel.Dx_Mother_Name_Aval  ),
                        new SqlParameter("@Dt_BirthDate_Aval",    CreditModel.Dt_BirthDate_Aval  ),
                        new SqlParameter("@Dx_RFC_CURP_Aval",     CreditModel.Dx_RFC_CURP_Aval  ),
                        new SqlParameter("@Dx_RFC_Aval",     CreditModel.Dx_RFC_Aval  ),
                        new SqlParameter("@Dx_CURP_Aval",     CreditModel.Dx_CURP_Aval  ),

                        new SqlParameter("@Dx_Tel_Aval",    CreditModel.Dx_Tel_Aval  ),
                        new SqlParameter("@Fg_Sexo_Aval",   CreditModel.Fg_Sexo_Aval  ),
                        new SqlParameter("@Dx_Domicilio_Aval_Calle",   CreditModel.Dx_Domicilio_Aval_Calle ),
                        new SqlParameter("@Dx_Domicilio_Aval_Num",   CreditModel.Dx_Domicilio_Aval_Num  ),
                        new SqlParameter("@Dx_Domicilio_Aval_CP",   CreditModel.Dx_Domicilio_Aval_CP  ),
                        new SqlParameter("@Cve_Estado_Aval",  CreditModel.Cve_Estado_Aval ),
                        new SqlParameter("@Cve_Deleg_Municipio_Aval",  CreditModel.Cve_Deleg_Municipio_Aval),
                        new SqlParameter("@Mt_Ventas_Mes_Aval",   CreditModel.Mt_Ventas_Mes_Aval),
                        new SqlParameter("@Mt_Gastos_Mes_Aval",  CreditModel.Mt_Gastos_Mes_Aval ),
                        new SqlParameter("@Dx_Nombre_Coacreditado",   CreditModel.Dx_Nombre_Coacreditado  ),
                        new SqlParameter("@Dx_RFC_CURP_Coacreditado",  CreditModel.Dx_RFC_CURP_Coacreditado  ),
                        new SqlParameter("@Fg_Sexo_Coacreditado",  CreditModel.Fg_Sexo_Coacreditado  ),
                        new SqlParameter("@Dx_Telefono_Coacreditado",  CreditModel.Dx_Telefono_Coacreditado ),
                        new SqlParameter("@Dx_Domicilio_Coacreditado_Calle",  CreditModel.Dx_Domicilio_Coacreditado_Calle  ),
                        new SqlParameter("@Dx_Domicilio_Coacreditado_Num",  CreditModel.Dx_Domicilio_Coacreditado_Num  ),
                        new SqlParameter("@Dx_Domicilio_Coacreditado_CP",   CreditModel.Dx_Domicilio_Coacreditado_CP  ),
                        new SqlParameter("@Cve_Estado_Coacreditado",  CreditModel.Cve_Estado_Coacreditado  ),
                        new SqlParameter("@Cve_Deleg_Municipio_Coacreditado",  CreditModel.Cve_Deleg_Municipio_Coacreditado ),
                        new SqlParameter("@Dt_Fecha_Ultmod",  CreditModel.Dt_Fecha_Ultmod  ),
                        new SqlParameter("@Dx_Usr_Ultmod",CreditModel.Dx_Usr_Ultmod  )         
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update credit failed: Execute method UpdateCredit in CreditDal.", ex, true);
            }
            return iResult;
        }
        /// <summary>
        /// Get max credit number
        /// </summary>
        /// <returns></returns>
        public int Select_Max_No_Credit()
        {
            int ImaxNo = 0; 
            try
            {
                string Sql = "select MAX(No_Credito)+1 as MaxNo from K_CREDITO";
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
                SQL = "SELECT CAST(SUM(ISNULL(P.[No_Eficiencia_Energia],0)*ISNULL(B.[No_Capacidad],0)) AS float) AS Total_Energy_Saving FROM CAT_PRODUCTO AS P"+
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
        /// <summary>
        /// Update Credit and Credit_Product
        /// </summary>
        /// <param name="CreditModel"></param>
        /// <returns></returns>
        public int UpdateCreditReview(CreditEntity CreditModel)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = @" UPDATE dbo.K_CREDITO SET 
                         Dx_Razon_Social =   @Dx_Razon_Social   ,
                         Cve_Tipo_Industria =   @Cve_Tipo_Industria   ,
                         Cve_Tipo_Sociedad =   @Cve_Tipo_Sociedad   ,
                         Dx_CURP =   @Dx_CURP   ,
                         Dx_RFC=@Dx_RFC,
                         Dx_Nombre_Repre_Legal  =   @Dx_Nombre_Repre_Legal   ,
                         Cve_Acreditacion_Repre_legal  =   @Cve_Acreditacion_Repre_legal   ,
                         Fg_Sexo_Repre_legal  =   @Fg_Sexo_Repre_legal  ,
                         No_RPU =   @No_RPU   ,
                         Fg_Edo_Civil_Repre_legal    =   @Fg_Edo_Civil_Repre_legal  , 
                         No_consumo_promedio = @No_consumo_promedio,
                         Cve_Reg_Conyugal_Repre_legal     =   @Cve_Reg_Conyugal_Repre_legal   ,
                         Cve_Identificacion_Repre_legal     =   @Cve_Identificacion_Repre_legal   ,
                         Dx_No_Identificacion_Repre_Legal      =   @Dx_No_Identificacion_Repre_Legal   ,
                         Mt_Ventas_Mes_Empresa         =   @Mt_Ventas_Mes_Empresa   ,
                         Mt_Gastos_Mes_Empresa       =   @Mt_Gastos_Mes_Empresa   ,
                         Dx_Email_Repre_legal    =   @Dx_Email_Repre_legal   ,
                         Dx_Domicilio_Fisc_Calle    =   @Dx_Domicilio_Fisc_Calle   ,
                         Dx_Domicilio_Fisc_Num    =   @Dx_Domicilio_Fisc_Num    ,
                         Dx_Domicilio_Fisc_CP    =   @Dx_Domicilio_Fisc_CP   ,
                         Dx_Domicilio_Fisc_Colonia =@Dx_Domicilio_Fisc_Colonia,
                         Cve_Estado_Fisc    =   @Cve_Estado_Fisc   ,
                         Cve_Deleg_Municipio_Fisc   =   @Cve_Deleg_Municipio_Fisc   ,
                         Cve_Tipo_Propiedad_Fisc    =   @Cve_Tipo_Propiedad_Fisc   ,
                         Dx_Tel_Fisc    =   @Dx_Tel_Fisc   ,
                         Fg_Mismo_Domicilio     =   @Fg_Mismo_Domicilio  ,
                         Dx_Domicilio_Neg_Calle   =   @Dx_Domicilio_Neg_Calle   ,
                         Dx_Domicilio_Neg_Num  =   @Dx_Domicilio_Neg_Num   ,
                         Dx_Domicilio_Neg_CP   =   @Dx_Domicilio_Neg_CP   ,
                         Dx_Domicilio_Neg_Colonia =@Dx_Domicilio_Neg_Colonia,
                         Cve_Estado_Neg    =   @Cve_Estado_Neg  ,
                         Cve_Deleg_Municipio_Neg    =   @Cve_Deleg_Municipio_Neg  ,
                         Cve_Tipo_Propiedad_Neg    =   @Cve_Tipo_Propiedad_Neg   ,
                         Dx_Tel_Neg     =   @Dx_Tel_Neg   ,
                         Dx_Nombre_Aval     =   @Dx_Nombre_Aval   ,
                         Dx_First_Name_Aval     =   @Dx_First_Name_Aval   ,
                         Dx_Last_Name_Aval     =   @Dx_Last_Name_Aval   ,
                         Dx_Mother_Name_Aval     =   @Dx_Mother_Name_Aval   ,
                         Dt_BirthDate_Aval     =   @Dt_BirthDate_Aval   ,
                         Dx_RFC_CURP_Aval      =   @Dx_RFC_CURP_Aval   ,
                         Dx_RFC_Aval      =   @Dx_RFC_Aval   ,
                         Dx_CURP_Aval      =   @Dx_CURP_Aval   ,
                         Dx_Tel_Aval     =   @Dx_Tel_Aval   ,
                         Fg_Sexo_Aval    =   @Fg_Sexo_Aval   ,
                         Dx_Domicilio_Aval_Calle    =   @Dx_Domicilio_Aval_Calle  ,
                         Dx_Domicilio_Aval_Num    =   @Dx_Domicilio_Aval_Num   ,
                         Dx_Domicilio_Aval_CP    =   @Dx_Domicilio_Aval_CP   ,
                         Dx_Domicilio_Aval_Colonia =@Dx_Domicilio_Aval_Colonia,
                         Cve_Estado_Aval   =   @Cve_Estado_Aval  ,
                         Cve_Deleg_Municipio_Aval   =   @Cve_Deleg_Municipio_Aval ,
                         Mt_Ventas_Mes_Aval    =   @Mt_Ventas_Mes_Aval ,
                         Mt_Gastos_Mes_Aval   =   @Mt_Gastos_Mes_Aval  ,
                         Mt_Ingreso_Neto_Mes_Aval =@Mt_Ingreso_Neto_Mes_Aval,
                         No_RPU_AVAL = @No_RPU_AVAL,
                         Dx_Nombre_Coacreditado    =   @Dx_Nombre_Coacreditado   ,
                         Dx_RFC_CURP_Coacreditado   =   @Dx_RFC_CURP_Coacreditado   ,
                         Fg_Sexo_Coacreditado   =   @Fg_Sexo_Coacreditado   ,
                         Dx_Telefono_Coacreditado   =   @Dx_Telefono_Coacreditado  ,
                         Dx_Domicilio_Coacreditado_Calle   =   @Dx_Domicilio_Coacreditado_Calle   ,
                         Dx_Domicilio_Coacreditado_Num   =   @Dx_Domicilio_Coacreditado_Num   ,
                         Dx_Domicilio_Coacreditado_CP    =   @Dx_Domicilio_Coacreditado_CP   ,
                         Dx_Domicilio_Coacreditado_Colonia=@Dx_Domicilio_Coacreditado_Colonia,
                         Cve_Estado_Coacreditado   =   @Cve_Estado_Coacreditado   ,
                         Cve_Deleg_Municipio_Coacreditado   =   @Cve_Deleg_Municipio_Coacreditado  ,                         
                         Cve_Periodo_Pago =@Cve_Periodo_Pago,
                         Mt_Monto_Solicitado =@Mt_Monto_Solicitado,
                         Mt_Ingreso_Neto_Mes_Empresa =@Mt_Ingreso_Neto_Mes_Empresa,
                        No_Ahorro_Energetico =@No_Ahorro_Energetico,
                         No_Ahorro_Economico =@No_Ahorro_Economico,
                         Mt_Capacidad_Pago=@Mt_Capacidad_Pago,
                        Mt_Monto_Total_Pagar =@Mt_Monto_Total_Pagar,
                        Cve_Estatus_Credito=@Cve_Estatus_Credito,
                        Dt_Fecha_Beneficiario_con_adeudos = @Dt_Fecha_Beneficiario_con_adeudos,
                        Dt_Fecha_Tarifa_fuera_de_programa=@Dt_Fecha_Tarifa_fuera_de_programa,
                         No_Plazo_Pago = @No_Plazo_Pago,

                        Dt_Fecha_Ultmod          = @Dt_Fecha_Ultmod,
                        Dx_Nombre_Comercial      = @Dx_Nombre_Comercial,
                        Dx_Usr_Ultmod            = @Dx_Usr_Ultmod,
                        ID_Prog_Proy             = @ID_Prog_Proy,
                        Id_Proveedor             = @Id_Proveedor,
                        Pct_Tasa_Interes         = @Pct_Tasa_Interes,
                        Pct_Tasa_Fija            = @Pct_Tasa_Fija,
                        Pct_CAT                  = @Pct_CAT,
                        Pct_Tasa_IVA             = @Pct_Tasa_IVA,
                        Pct_Tasa_IVA_Intereses   = @Pct_Tasa_IVA_Intereses,
                        Tipo_Usuario             = @Tipo_Usuario,
                        ATB04                    = @ATB04,
                        ATB05                    = @ATB05,

                        Mt_Gastos_Instalacion_Mano_Obra = @Mt_Gastos_Instalacion_Mano_Obra

                         where No_Credito  =   @No_Credito ";

                SqlParameter[] paras = new SqlParameter[] { 
                        new SqlParameter("@No_Credito", CreditModel.No_Credito),
                        new SqlParameter("@Dx_Razon_Social",CreditModel.Dx_Razon_Social  ),
                        new SqlParameter("@Cve_Tipo_Industria",         CreditModel.Cve_Tipo_Industria  ),
                        new SqlParameter("@Cve_Tipo_Sociedad",      CreditModel.Cve_Tipo_Sociedad  ),
                        new SqlParameter("@Dx_CURP",      CreditModel.Dx_CURP  ),
                        new SqlParameter("@Dx_RFC",CreditModel.Dx_RFC),
                        new SqlParameter("@Dx_Nombre_Repre_Legal",      CreditModel.Dx_Nombre_Repre_Legal  ),
                        new SqlParameter("@Cve_Acreditacion_Repre_legal",    CreditModel.Cve_Acreditacion_Repre_legal  ),
                        new SqlParameter("@Fg_Sexo_Repre_legal",    CreditModel.Fg_Sexo_Repre_legal ),
                        new SqlParameter("@No_RPU",    CreditModel.No_RPU  ),
                        new SqlParameter("@Fg_Edo_Civil_Repre_legal",   CreditModel.Fg_Edo_Civil_Repre_legal ), 
                        new SqlParameter("@No_consumo_promedio",CreditModel.No_consumo_promedio),
                        new SqlParameter("@Cve_Reg_Conyugal_Repre_legal",    CreditModel.Cve_Reg_Conyugal_Repre_legal  ),
                        new SqlParameter("@Cve_Identificacion_Repre_legal",    CreditModel.Cve_Identificacion_Repre_legal  ),
                        new SqlParameter("@Dx_No_Identificacion_Repre_Legal",     CreditModel.Dx_No_Identificacion_Repre_Legal  ),
                        new SqlParameter("@Mt_Ventas_Mes_Empresa",        CreditModel.Mt_Ventas_Mes_Empresa  ),
                        new SqlParameter("@Mt_Gastos_Mes_Empresa",      CreditModel.Mt_Gastos_Mes_Empresa  ),
                        new SqlParameter("@Dx_Email_Repre_legal",   CreditModel.Dx_Email_Repre_legal  ),
                        new SqlParameter("@Dx_Domicilio_Fisc_Calle",   CreditModel.Dx_Domicilio_Fisc_Calle  ),
                        new SqlParameter("@Dx_Domicilio_Fisc_Num",   CreditModel.Dx_Domicilio_Fisc_Num   ),
                        new SqlParameter("@Dx_Domicilio_Fisc_CP",   CreditModel.Dx_Domicilio_Fisc_CP  ),
                        new SqlParameter ("@Dx_Domicilio_Fisc_Colonia",CreditModel.Dx_Domicilio_Fisc_Colonia),
                        new SqlParameter("@Cve_Estado_Fisc",   CreditModel.Cve_Estado_Fisc  ),
                        new SqlParameter("@Cve_Deleg_Municipio_Fisc",  CreditModel.Cve_Deleg_Municipio_Fisc  ),
                        new SqlParameter("@Cve_Tipo_Propiedad_Fisc",   CreditModel.Cve_Tipo_Propiedad_Fisc  ),
                        new SqlParameter("@Dx_Tel_Fisc",   CreditModel.Dx_Tel_Fisc  ),
                        new SqlParameter("@Fg_Mismo_Domicilio",    CreditModel.Fg_Mismo_Domicilio ),
                        new SqlParameter("@Dx_Domicilio_Neg_Calle",  CreditModel.Dx_Domicilio_Neg_Calle  ),
                        new SqlParameter("@Dx_Domicilio_Neg_Num", CreditModel.Dx_Domicilio_Neg_Num  ),
                        new SqlParameter("@Dx_Domicilio_Neg_CP",  CreditModel.Dx_Domicilio_Neg_CP  ),
                        new SqlParameter("@Dx_Domicilio_Neg_Colonia",CreditModel.Dx_Domicilio_Neg_Colonia),
                        new SqlParameter("@Cve_Estado_Neg",   CreditModel.Cve_Estado_Neg ),
                        new SqlParameter("@Cve_Deleg_Municipio_Neg",   CreditModel.Cve_Deleg_Municipio_Neg ),
                        new SqlParameter("@Cve_Tipo_Propiedad_Neg",   CreditModel.Cve_Tipo_Propiedad_Neg  ),
                        new SqlParameter("@Dx_Tel_Neg",    CreditModel.Dx_Tel_Neg  ),
                                            
                        // RSA detailed Aval information for RFC validation
                        new SqlParameter("@Dx_Nombre_Aval",    CreditModel.Dx_Nombre_Aval  ),
                        new SqlParameter("@Dx_First_Name_Aval",    CreditModel.Dx_First_Name_Aval  ),
                        new SqlParameter("@Dx_Last_Name_Aval",    CreditModel.Dx_Last_Name_Aval  ),
                        new SqlParameter("@Dx_Mother_Name_Aval",    CreditModel.Dx_Mother_Name_Aval  ),
                        new SqlParameter("@Dt_BirthDate_Aval",    CreditModel.Dt_BirthDate_Aval  ),
                        new SqlParameter("@Dx_RFC_CURP_Aval",     CreditModel.Dx_RFC_CURP_Aval  ),
                        new SqlParameter("@Dx_RFC_Aval",     CreditModel.Dx_RFC_Aval  ),
                        new SqlParameter("@Dx_CURP_Aval",     CreditModel.Dx_CURP_Aval  ),

                        new SqlParameter("@Dx_Tel_Aval",    CreditModel.Dx_Tel_Aval  ),
                        new SqlParameter("@Fg_Sexo_Aval",   CreditModel.Fg_Sexo_Aval  ),
                        new SqlParameter("@Dx_Domicilio_Aval_Calle",   CreditModel.Dx_Domicilio_Aval_Calle ),
                        new SqlParameter("@Dx_Domicilio_Aval_Num",   CreditModel.Dx_Domicilio_Aval_Num  ),
                        new SqlParameter("@Dx_Domicilio_Aval_CP",   CreditModel.Dx_Domicilio_Aval_CP  ),
                        new SqlParameter("@Dx_Domicilio_Aval_Colonia",CreditModel.Dx_Domicilio_Aval_Colonia),
                        new SqlParameter("@Cve_Estado_Aval",  CreditModel.Cve_Estado_Aval ),
                        new SqlParameter("@Cve_Deleg_Municipio_Aval",  CreditModel.Cve_Deleg_Municipio_Aval),
                        new SqlParameter("@Mt_Ventas_Mes_Aval",   CreditModel.Mt_Ventas_Mes_Aval),
                        new SqlParameter("@Mt_Gastos_Mes_Aval",  CreditModel.Mt_Gastos_Mes_Aval ),
                        new SqlParameter("@Mt_Ingreso_Neto_Mes_Aval",CreditModel.Mt_Ingreso_Neto_Mes_Aval),
                        new SqlParameter("@No_RPU_AVAL", CreditModel.No_RPU_AVAL),
                        new SqlParameter("@Dx_Nombre_Coacreditado",   CreditModel.Dx_Nombre_Coacreditado  ),
                        new SqlParameter("@Dx_RFC_CURP_Coacreditado",  CreditModel.Dx_RFC_CURP_Coacreditado  ),
                        new SqlParameter("@Fg_Sexo_Coacreditado",  CreditModel.Fg_Sexo_Coacreditado  ),
                        new SqlParameter("@Dx_Telefono_Coacreditado",  CreditModel.Dx_Telefono_Coacreditado ),
                        new SqlParameter("@Dx_Domicilio_Coacreditado_Calle",  CreditModel.Dx_Domicilio_Coacreditado_Calle  ),
                        new SqlParameter("@Dx_Domicilio_Coacreditado_Num",  CreditModel.Dx_Domicilio_Coacreditado_Num  ),
                        new SqlParameter("@Dx_Domicilio_Coacreditado_CP",   CreditModel.Dx_Domicilio_Coacreditado_CP  ),
                        new SqlParameter("@Dx_Domicilio_Coacreditado_Colonia",CreditModel.Dx_Domicilio_Coacreditado_Colonia),
                        new SqlParameter("@Cve_Estado_Coacreditado",  CreditModel.Cve_Estado_Coacreditado  ),
                        new SqlParameter("@Cve_Deleg_Municipio_Coacreditado",  CreditModel.Cve_Deleg_Municipio_Coacreditado ),                       
                        new SqlParameter("@Mt_Monto_Solicitado",CreditModel.Mt_Monto_Solicitado),
                        new SqlParameter("@Mt_Ingreso_Neto_Mes_Empresa",CreditModel.Mt_Ingreso_Neto_Mes_Empresa),
                        new SqlParameter("@Cve_Periodo_Pago",CreditModel.Cve_Periodo_Pago),
                        new SqlParameter("@No_Ahorro_Energetico",CreditModel.No_Ahorro_Energetico),
                        new SqlParameter("@No_Ahorro_Economico",CreditModel.No_Ahorro_Economico),
                        new SqlParameter("@Mt_Capacidad_Pago",CreditModel.Mt_Capacidad_Pago),
                        new SqlParameter ("@Mt_Monto_Total_Pagar",CreditModel.Mt_Monto_Total_Pagar),
                        new SqlParameter("@Dt_Fecha_Beneficiario_con_adeudos",CreditModel.Dt_Fecha_Beneficiario_con_adeudos),
                        new SqlParameter("@Cve_Estatus_Credito",CreditModel.Cve_Estatus_Credito),
                        new SqlParameter("@Dt_Fecha_Tarifa_fuera_de_programa",CreditModel.Dt_Fecha_Tarifa_fuera_de_programa),
                        new SqlParameter("@No_Plazo_Pago",CreditModel.No_Plazo_Pago),

                        // RSA 20130814 Enable edition
                        new SqlParameter("@Dt_Fecha_Ultmod", CreditModel.Dt_Fecha_Ultmod),
                        new SqlParameter("@Dx_Nombre_Comercial", CreditModel.Dx_Nombre_Comercial),
                        new SqlParameter("@Dx_Usr_Ultmod", CreditModel.Dx_Usr_Ultmod),
                        new SqlParameter("@ID_Prog_Proy", CreditModel.ID_Prog_Proy),
                        new SqlParameter("@Id_Proveedor", CreditModel.Id_Proveedor),
                        new SqlParameter("@Pct_Tasa_Interes", CreditModel.Pct_Tasa_Interes),
                        new SqlParameter("@Pct_Tasa_Fija", CreditModel.Pct_Tasa_Fija),
                        new SqlParameter("@Pct_CAT", CreditModel.Pct_CAT),
                        new SqlParameter("@Pct_Tasa_IVA", CreditModel.Pct_Tasa_IVA),
                        new SqlParameter("@Pct_Tasa_IVA_Intereses", CreditModel.Pct_Tasa_IVA_Intereses),
                        new SqlParameter("@Tipo_Usuario", CreditModel.Tipo_Usuario),
                        new SqlParameter("@ATB04", CreditModel.Telephone),
                        new SqlParameter("@ATB05", CreditModel.Email),

                        new SqlParameter("@Mt_Gastos_Instalacion_Mano_Obra", CreditModel.Mt_Gastos_Instalacion_Mano_Obra)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update credit failed: Execute method UpdateCredit in CreditDal.", ex, true);
            }
            return iResult;
        }
        /// <summary>
        /// Check if service code exist
        /// </summary>
        /// <param name="servicecode"></param>
        /// <returns></returns>
        public bool IsServiceCodeExist(string servicecode)
        {
            bool bResult = false;
            string SQL = "";
            try
            {
                SQL = "SELECT No_Credito FROM K_CREDITO (NOLOCK) WHERE No_RPU = @No_RPU AND CVE_ESTATUS_CREDITO IN (1,2,3,4)" ;
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_RPU", servicecode)
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL,paras);
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
        public DataTable Get_CreditRequestForDisposal(int DisposalID, string userType, int ProviderID,string ProviderType, int TechnologyID, string sortName, int pageIndex, int pageSize, out int pageCount)
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
                if (ProviderID>0)
                {
                    if(ProviderType == GlobalVar.SUPPLIER)
                    {
                    WhereBuilder.Append(" and [Id_Proveedor] = " + ProviderID + " and [Tipo_Usuario] = '" + GlobalVar.SUPPLIER+ "'" );//+ "' OR (Id_Proveedor in (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Id_Proveedor = " + ProviderID + ") AND Tipo_Usuario = '" + GlobalVar.SUPPLIER_BRANCH + "'))");
                    }
                    else
                    {
                        WhereBuilder.Append("  And Id_Proveedor in (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE  Id_Branch= " + ProviderID + ")  AND Tipo_Usuario = '" + GlobalVar.SUPPLIER_BRANCH+"'" );
                    }
                }
                if (TechnologyID>0)
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

            string sql = "select k.Barcode_Solicitud from K_CREDITO k " +                         
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

            string sql = "select COUNT(*) from K_CREDITO where No_Credito=@No_Credito";
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

            string sql = "select COUNT(*) from K_CREDITO where No_Credito=@No_Credito and Cve_Estatus_Credito in (@Cve_Estatus_Credito, @Cve_Estatus_Credito2)";
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
                string sql = "select No_Credito from K_CREDITO where No_Credito in(select No_Credito from K_CREDITO_SUSTITUCION where Id_Centro_Disp=@Id_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp)";
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
                string SQL = "SELECT DISTINCT A.No_Credito FROM K_CREDITO A INNER JOIN K_CREDITO_SUSTITUCION B" +
                                 " ON A.No_Credito=B.No_Credito AND B.Id_Folio IS NOT NULL AND B.Dt_Fecha_Recepcion IS NOT NULL AND ISNULL(B.Fg_Si_Funciona,0)=1 " +
                                 " WHERE 1=1";
                if (program != "")
                {
                    SQL += " AND A.ID_Prog_Proy=@program";
                }
                if (disposalId != 0)
                {
                    SQL += " AND B.Id_Centro_Disp=@disposalId";
                }
                if (disposalType != "")
                {
                    SQL += " AND B.Fg_Tipo_Centro_Disp=@disposalType";
                }
                if (technology != "")
                {
                    SQL += " AND B.Cve_Tecnologia=@technology";
                }
                if (supplierId != "")
                {
                    SQL += " AND A.Id_Proveedor=@supplierId";
                }
                if (supplierType != "")
                {
                    SQL += " AND A.Tipo_Usuario=@supplierType";
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
        public DataTable GetCreditByProgramAndDisposalAndSupplierAndTechnology(string program, string DisposalID, string DisposalType,string Supplier,string SupplierType,string Technology)
        {
            DataTable dtResult = null;
            try
            {
                string sql = "select No_Credito from K_CREDITO where No_Credito in(select No_Credito from K_CREDITO_SUSTITUCION ";
                sql = sql + " where Id_Centro_Disp=@Id_Centro_Disp and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp ";
                if (Technology != "")
                {
                    sql = sql + "  and Cve_Tecnologia=@Cve_Tecnologia";
                }
                sql = sql + ")";
                if (program != "")
                {
                    sql += " and ID_Prog_Proy=" + program;
                }
                if (Supplier != "")
                {
                    sql = sql + " and Id_Proveedor=@Id_Proveedor And Tipo_Usuario=@Tipo_Usuario";
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
            string sql = @"Select Sum(Mt_Monto_Solicitado) From K_CREDITO Where RTrim(LTrim(Dx_RFC))=@Dx_RFC And
                Cve_Estatus_Credito In (@CreditStatus1, @CreditStatus2, @CreditStatus3, @CreditStatus4, @CreditStatus6)";
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
                , dsResult, new string[] {"Inputs", "Calculated"}, paras);
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
