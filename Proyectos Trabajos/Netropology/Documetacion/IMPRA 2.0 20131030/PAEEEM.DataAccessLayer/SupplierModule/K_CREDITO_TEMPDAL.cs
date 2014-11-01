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
    public class K_CREDITO_TEMPDAL
    {
        /// <summary>
        /// Readonly variable for class instance
        /// </summary>
        private static readonly K_CREDITO_TEMPDAL _classInstance = new K_CREDITO_TEMPDAL();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static K_CREDITO_TEMPDAL ClassInstance { get { return _classInstance; } }

        public int Insert_K_Credito_Temp(K_CREDITO_TEMPEntity CreditTempModel)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = @"INSERT INTO [dbo].[K_CREDITO_TEMP]
                           ([Cve_Tipo_Sociedad],[Dx_First_Name],[Dx_Last_Name],[Dx_Mother_Name],[Dt_Fecha_Nacimiento],[Cve_Tipo_Industria]
                           ,[Dx_Telephone],[Dx_Email] ,[Dx_CURP],[Dx_Nombre_Comercial],[Dx_RFC] ,[Dx_Nombre_Repre_Legal],[Cve_Acreditacion_Repre_legal]
                           ,[Fg_Sexo_Repre_legal] ,[No_RPU],[Fg_Edo_Civil_Repre_legal],[Cve_Reg_Conyugal_Repre_legal],[Cve_Identificacion_Repre_legal]
                           ,[Dx_No_Identificacion_Repre_Legal] ,[Mt_Ventas_Mes_Empresa],[Mt_Gastos_Mes_Empresa],[Dx_Email_Repre_legal],[No_consumo_promedio]
                           ,[Dx_Domicilio_Fisc_Calle],[Dx_Domicilio_Fisc_Num],[Dx_Domicilio_Fisc_CP] ,[Cve_Estado_Fisc],[Cve_Deleg_Municipio_Fisc],[Dx_Ciudad]
                           ,[Cve_Tipo_Propiedad_Fisc],[Dx_Tel_Fisc],[Dx_Domicilio_Fisc_Colonia],[Fg_Mismo_Domicilio],[Dx_Domicilio_Neg_Calle]
                           ,[Dx_Domicilio_Neg_Num],[Dx_Domicilio_Neg_CP],[Cve_Estado_Neg],[Cve_Deleg_Municipio_Neg],[Cve_Tipo_Propiedad_Neg]
                            ,[Dx_Tel_Neg],[Dx_Domicilio_Neg_Colonia],[Dx_Nombre_Aval],[Dx_First_Name_Aval],[Dx_Last_Name_Aval],[Dx_Mother_Name_Aval]
                            , [Dt_BirthDate_Aval],[Dx_RFC_CURP_Aval],[Dx_RFC_Aval],[Dx_CURP_Aval],[Dx_Tel_Aval],[Fg_Sexo_Aval]
                           ,[Dx_Domicilio_Aval_Calle],[Dx_Domicilio_Aval_Num],[Dx_Domicilio_Aval_CP],[Cve_Estado_Aval],[Cve_Deleg_Municipio_Aval]
                           ,[Dx_Domicilio_Aval_Colonia],[No_RPU_AVAL],[Mt_Ventas_Mes_Aval],[Mt_Gastos_Mes_Aval],[User_Name])
                     VALUES
                           (@Cve_Tipo_Sociedad,@Dx_First_Name,@Dx_Last_Name,@Dx_Mother_Name,@Dt_Fecha_Nacimiento,@Cve_Tipo_Industria
                           ,@Dx_Telephone,@Dx_Email ,@Dx_CURP,@Dx_Nombre_Comercial,@Dx_RFC ,@Dx_Nombre_Repre_Legal,@Cve_Acreditacion_Repre_legal
                           ,@Fg_Sexo_Repre_legal ,@No_RPU,@Fg_Edo_Civil_Repre_legal,@Cve_Reg_Conyugal_Repre_legal,@Cve_Identificacion_Repre_legal
                           ,@Dx_No_Identificacion_Repre_Legal ,@Mt_Ventas_Mes_Empresa,@Mt_Gastos_Mes_Empresa,@Dx_Email_Repre_legal,@No_consumo_promedio
                           ,@Dx_Domicilio_Fisc_Calle,@Dx_Domicilio_Fisc_Num,@Dx_Domicilio_Fisc_CP ,@Cve_Estado_Fisc,@Cve_Deleg_Municipio_Fisc,@Dx_Ciudad
                           ,@Cve_Tipo_Propiedad_Fisc,@Dx_Tel_Fisc,@Dx_Domicilio_Fisc_Colonia,@Fg_Mismo_Domicilio,@Dx_Domicilio_Neg_Calle
                           ,@Dx_Domicilio_Neg_Num,@Dx_Domicilio_Neg_CP,@Cve_Estado_Neg,@Cve_Deleg_Municipio_Neg,@Cve_Tipo_Propiedad_Neg
                            ,@Dx_Tel_Neg,@Dx_Domicilio_Neg_Colonia,@Dx_Nombre_Aval,@Dx_First_Name_Aval,@Dx_Last_Name_Aval,@Dx_Mother_Name_Aval
                            ,@Dt_BirthDate_Aval,@Dx_RFC_CURP_Aval,@Dx_RFC_Aval,@Dx_CURP_Aval,@Dx_Tel_Aval,@Fg_Sexo_Aval
                           ,@Dx_Domicilio_Aval_Calle,@Dx_Domicilio_Aval_Num,@Dx_Domicilio_Aval_CP,@Cve_Estado_Aval,@Cve_Deleg_Municipio_Aval
                           ,@Dx_Domicilio_Aval_Colonia,@No_RPU_AVAL,@Mt_Ventas_Mes_Aval,@Mt_Gastos_Mes_Aval,@User_Name)";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tipo_Sociedad", CreditTempModel.Cve_Tipo_Sociedad),
                    new SqlParameter("@Dx_First_Name", CreditTempModel.Dx_First_Name),
                    new SqlParameter("@Dx_Last_Name", CreditTempModel.Dx_Last_Name),
                    new SqlParameter("@Dx_Mother_Name", CreditTempModel.Dx_Mother_Name),
                    new SqlParameter("@Dt_Fecha_Nacimiento", CreditTempModel.Dt_Fecha_Nacimiento),
                    new SqlParameter("@Cve_Tipo_Industria", CreditTempModel.Cve_Tipo_Industria),

                    new SqlParameter("@Dx_Telephone", CreditTempModel.Dx_Telephone),
                    new SqlParameter("@Dx_Email", CreditTempModel.Dx_Email),
                    new SqlParameter("@Dx_CURP", CreditTempModel.Dx_CURP),
                    new SqlParameter("@Dx_Nombre_Comercial", CreditTempModel.Dx_Nombre_Comercial),
                    new SqlParameter("@Dx_RFC", CreditTempModel.Dx_RFC),
                    new SqlParameter("@Dx_Nombre_Repre_Legal", CreditTempModel.Dx_Nombre_Repre_Legal),
                    new SqlParameter("@Cve_Acreditacion_Repre_legal", CreditTempModel.Cve_Acreditacion_Repre_legal),

                    new SqlParameter("@Fg_Sexo_Repre_legal", CreditTempModel.Fg_Sexo_Repre_legal),
                    new SqlParameter("@No_RPU", CreditTempModel.No_RPU),
                    new SqlParameter("@Fg_Edo_Civil_Repre_legal", CreditTempModel.Fg_Edo_Civil_Repre_legal),                   
                    new SqlParameter("@Cve_Reg_Conyugal_Repre_legal", CreditTempModel.Cve_Reg_Conyugal_Repre_legal),
                    new SqlParameter("@Cve_Identificacion_Repre_legal", CreditTempModel.Cve_Identificacion_Repre_legal),

                    new SqlParameter("@Dx_No_Identificacion_Repre_Legal", CreditTempModel.Dx_No_Identificacion_Repre_Legal),
                    new SqlParameter("@Mt_Ventas_Mes_Empresa", CreditTempModel.Mt_Ventas_Mes_Empresa),
                    new SqlParameter("@Mt_Gastos_Mes_Empresa", CreditTempModel.Mt_Gastos_Mes_Empresa),
                    new SqlParameter("@Dx_Email_Repre_legal", CreditTempModel.Dx_Email_Repre_legal),
                    new SqlParameter("@No_consumo_promedio", CreditTempModel.No_consumo_promedio),

                    new SqlParameter("@Dx_Domicilio_Fisc_Calle", CreditTempModel.Dx_Domicilio_Fisc_Calle),
                    new SqlParameter("@Dx_Domicilio_Fisc_Num", CreditTempModel.Dx_Domicilio_Fisc_Num),
                    new SqlParameter("@Dx_Domicilio_Fisc_CP", CreditTempModel.Dx_Domicilio_Fisc_CP),
                    new SqlParameter("@Cve_Estado_Fisc", CreditTempModel.Cve_Estado_Fisc),
                    new SqlParameter("@Cve_Deleg_Municipio_Fisc", CreditTempModel.Cve_Deleg_Municipio_Fisc),
                    new SqlParameter("@Dx_Ciudad", CreditTempModel.Dx_Ciudad),
                   
                    //new SqlParameter("@Dx_Numero_Interior", CreditTempModel.Dx_Numero_Interior),
                    new SqlParameter("@Cve_Tipo_Propiedad_Fisc", CreditTempModel.Cve_Tipo_Propiedad_Fisc),
                    new SqlParameter("@Dx_Tel_Fisc", CreditTempModel.Dx_Tel_Fisc),
                    new SqlParameter("@Dx_Domicilio_Fisc_Colonia", CreditTempModel.Dx_Domicilio_Fisc_Colonia),
                    new SqlParameter("@Fg_Mismo_Domicilio", CreditTempModel.Fg_Mismo_Domicilio),
                    new SqlParameter("@Dx_Domicilio_Neg_Calle", CreditTempModel.Dx_Domicilio_Neg_Calle),
                   
                    new SqlParameter("@Dx_Domicilio_Neg_Num", CreditTempModel.Dx_Domicilio_Neg_Num),
                    new SqlParameter("@Dx_Domicilio_Neg_CP", CreditTempModel.Dx_Domicilio_Neg_CP),
                    new SqlParameter("@Cve_Estado_Neg", CreditTempModel.Cve_Estado_Neg),
                    new SqlParameter("@Cve_Deleg_Municipio_Neg", CreditTempModel.Cve_Deleg_Municipio_Neg),
                    new SqlParameter("@Cve_Tipo_Propiedad_Neg", CreditTempModel.Cve_Tipo_Propiedad_Neg),
                   
                    new SqlParameter("@Dx_Tel_Neg", CreditTempModel.Dx_Tel_Neg),
                    new SqlParameter("@Dx_Domicilio_Neg_Colonia", CreditTempModel.Dx_Domicilio_Neg_Colonia),

                    // RSA detailed Aval information for RFC validation
                    new SqlParameter("@Dx_Nombre_Aval", CreditTempModel.Dx_Nombre_Aval),
                    new SqlParameter("@Dx_First_Name_Aval", CreditTempModel.Dx_First_Name_Aval),
                    new SqlParameter("@Dx_Last_Name_Aval", CreditTempModel.Dx_Last_Name_Aval),
                    new SqlParameter("@Dx_Mother_Name_Aval", CreditTempModel.Dx_Mother_Name_Aval),
                    new SqlParameter("@Dt_BirthDate_Aval", CreditTempModel.Dt_BirthDate_Aval),
                    new SqlParameter("@Dx_RFC_CURP_Aval", CreditTempModel.Dx_RFC_CURP_Aval),
                    new SqlParameter("@Dx_RFC_Aval", CreditTempModel.Dx_RFC_Aval),
                    new SqlParameter("@Dx_CURP_Aval", CreditTempModel.Dx_CURP_Aval),

                    new SqlParameter("@Dx_Tel_Aval", CreditTempModel.Dx_Tel_Aval),
                    new SqlParameter("@Fg_Sexo_Aval", CreditTempModel.Fg_Sexo_Aval),
                 
                    new SqlParameter("@Dx_Domicilio_Aval_Calle", CreditTempModel.Dx_Domicilio_Aval_Calle),
                    new SqlParameter("@Dx_Domicilio_Aval_Num", CreditTempModel.Dx_Domicilio_Aval_Num),
                    new SqlParameter("@Dx_Domicilio_Aval_CP", CreditTempModel.Dx_Domicilio_Aval_CP),
                    new SqlParameter("@Cve_Estado_Aval", CreditTempModel.Cve_Estado_Aval),
                    new SqlParameter("@Cve_Deleg_Municipio_Aval", CreditTempModel.Cve_Deleg_Municipio_Aval),
                 
                    new SqlParameter("@Dx_Domicilio_Aval_Colonia", CreditTempModel.Dx_Domicilio_Aval_Colonia),
                    new SqlParameter("@No_RPU_AVAL", CreditTempModel.No_RPU_AVAL),
                    new SqlParameter("@Mt_Ventas_Mes_Aval", CreditTempModel.Mt_Ventas_Mes_Aval),
                    new SqlParameter("@Mt_Gastos_Mes_Aval", CreditTempModel.Mt_Gastos_Mes_Aval),
                    new SqlParameter("@User_Name", CreditTempModel.User_Name)  
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add creditTemp failed: Execute method Insert_K_Credito_Temp in K_CREDITO_TEMPDAL.", ex, true);
            }

            return iResult;
        }

        public int Delete_K_credito_TempByUserName(string UserName)
        {
            int iCount = 0;
            try
            {
                string Sql = "DELETE FROM K_CREDITO_TEMP WHERE User_Name =@User_Name";
                SqlParameter[] para = new SqlParameter[] { 
                 new SqlParameter("@User_Name",UserName)
                };
                iCount = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, para);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return iCount;
        }

        public DataTable Get_K_Credito_Temp(string UserName)
        {
            DataTable dtCreditTemp = null;
            try
            {
                string strSQL = "select top 1 * from K_CREDITO_TEMP where [User_Name] =@User_Name";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@User_Name", UserName)
                };
                dtCreditTemp = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get K_Credito_Temp by userName failed", ex, true);
            }

            return dtCreditTemp;
        }
    }
}
