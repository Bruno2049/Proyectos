﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Relation between register process and product
    /// </summary>
    public class K_RECUPERACION_PRODUCTODal
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly K_RECUPERACION_PRODUCTODal _classinstance = new K_RECUPERACION_PRODUCTODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_RECUPERACION_PRODUCTODal ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_Recuperacion"></param>
        /// <param name="Id_Credito_Sustitucion"></param>
        /// <returns></returns>
        public int Insert_K_RECUPERACION_PRODUCTO(int Id_Recuperacion, int Id_Credito_Sustitucion)
        {
            try
            {
                string executesqlstr = "INSERT INTO K_RECUPERACION_PRODUCTO (Id_Recuperacion,Id_Credito_Sustitucion) VALUES(@Id_Recuperacion,@Id_Credito_Sustitucion) ";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Recuperacion",Id_Recuperacion),
                    new SqlParameter("@Id_Credito_Sustitucion",Id_Credito_Sustitucion)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_RECUPERACION_PRODUCTO failed: Execute method Insert_K_RECUPERACION_PRODUCTO in K_RECUPERACION_PRODUCTODal.", ex, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_Recuperacion"></param>
        /// <returns></returns>
        public int Delete_K_RECUPERACION_PRODUCTOById_Recuperacion(string Id_Recuperacion)
        {
            int result = 0;
            try
            {
                // updated by tina 2012-03-09
                string executesqlstr = "DELETE FROM K_RECUPERACION_PRODUCTO WHERE Id_Recuperacion=@Id_Recuperacion";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Recuperacion",Id_Recuperacion)
                };

                result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "delete K_RECUPERACION_PRODUCTO failed: Execute method Delete_K_RECUPERACION_PRODUCTOById_Recuperacion in K_RECUPERACION_PRODUCTODal.", ex, true);
            }
            return result;
        }

        // updated by tina 2012-02-27
        /// <summary>
        /// get supervision products for generate final act
        /// </summary>
        /// <param name="program"></param>
        /// <param name="technology"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="UserID"></param>
        /// <param name="UserType"></param>
        /// <param name="disposalId"></param>
        /// <param name="disposalType"></param>
        /// <returns></returns>
        public DataTable GetSupervisionProducts(string program, string technology, string fromDate, string toDate, int UserID, string UserType, string disposalId, string disposalType)
        {
            try
            {
                string SQL = "select distinct A.Id_Recuperacion,A.Id_Centro_Disp,A.Fg_Tipo_Centro_Disp from dbo.View_Supervision_Products A";
                if (UserType == "R" || UserType == "Z")
                {
                    SQL += " inner join (select Id_Centro_Disp,'M' as Fg_Tipo_Centro_Disp,Cve_Region,Cve_Zona from CAT_CENTRO_DISP " +
                                " union all select Id_Centro_Disp_Sucursal as Id_Centro_Disp,'B' as Fg_Tipo_Centro_Disp,Cve_Region,Cve_Zona  from CAT_CENTRO_DISP_SUCURSAL) B";
                    SQL += " on A.Id_Centro_Disp=B.Id_Centro_Disp and A.Fg_Tipo_Centro_Disp=B.Fg_Tipo_Centro_Disp";
                    if (disposalId != "")
                    {
                        SQL += " and B.Id_Centro_Disp=" + disposalId;
                    }
                    if (disposalType != "")
                    {
                        SQL += " and B.Fg_Tipo_Centro_Disp='" + disposalType + "'";
                    }
                    if (UserType == "R")
                    {
                        SQL += " and B.Cve_Region=" + UserID;
                    }
                    else if (UserType == "Z")
                    {
                        SQL += " and B.Cve_Zona=" + UserID;
                    }
                }
                SQL += " where 1=1";
                if (program != "")
                {
                    SQL += " and A.ID_Prog_Proy=" + program;
                }
                if (technology != "")
                {
                    SQL += " and A.Cve_Tecnologia=" + technology;
                }
                if (fromDate != "")
                {
                    SQL += " and convert(varchar(10),A.Dt_Fecha_Recepcion,120)>='" + fromDate + "'";
                }
                if (toDate != "")
                {
                    SQL += " and convert(varchar(10),A.Dt_Fecha_Recepcion,120)<='" + toDate + "'";
                }

                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get supervision products failed: Execute method GetSupervisionDate in K_RECUPERACION_PRODUCTODal.", ex, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CreditSusID"></param>
        /// <returns></returns>
        public string GetFgInspectByIDCredit(string CreditSusID)
        {
            string Result = "";
            string sql = " select top 1 Fg_Inspeccionado from K_RECUPERACION_PRODUCTO where Id_Credito_Sustitucion=@Id_Credito_Sustitucion order by Fg_Inspeccionado ";
            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Credito_Sustitucion", CreditSusID)                    
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
                if (o != null)
                {
                    Result = o.ToString();
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get CreditNo failed: Execute method GetFgInspectByIDCredit in K_RECUPERACION_PRODUCTODal.", ex, true);
            }
            return Result;
        }
        /// <summary>
        /// Update reception product
        /// </summary>
        /// <param name="CreditIDList"></param>
        /// <param name="RecuperacionDate"></param>
        /// <returns></returns>
        public int UpdateK_Recuperacion_Product(string CreditIDList, string RecuperacionDate)
        {
            try
            {
                string executesqlstr = " Update K_RECUPERACION_PRODUCTO set Fg_Inspeccionado= 1, Dt_Fecha_Inspeccion=@Dt_Fecha_Inspeccion where Id_Credito_Sustitucion in("+ CreditIDList +")";
                SqlParameter[] para = new SqlParameter[] {
                    new SqlParameter("@Dt_Fecha_Inspeccion",RecuperacionDate)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update K_RECUPERACION_PRODUCTO failed: Execute method UpdateK_Recuperacion_Product in K_RECUPERACION_PRODUCTODal.", ex, true);
            }
        }
    }
}