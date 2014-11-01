using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Inhabilitación process and product relationships
    /// </summary>
    public class K_INHABILITACION_PRODUCTODal
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly K_INHABILITACION_PRODUCTODal _classinstance = new K_INHABILITACION_PRODUCTODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_INHABILITACION_PRODUCTODal ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id_Inhabilitacion"></param>
        /// <param name="Id_Credito_Sustitucion"></param>
        /// <returns></returns>
        public int Insert_K_INHABILITACION_PRODUCTO(int Id_Inhabilitacion, int Id_Credito_Sustitucion)
        {
            try
            {
                string executesqlstr = "INSERT INTO K_INHABILITACION_PRODUCTO (Id_Inhabilitacion,Id_Credito_Sustitucion) VALUES(@Id_Inhabilitacion,@Id_Credito_Sustitucion) ";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Inhabilitacion",Id_Inhabilitacion),
                    new SqlParameter("@Id_Credito_Sustitucion",Id_Credito_Sustitucion)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_INHABILITACION_PRODUCTO failed: Execute method Insert_K_INHABILITACION_PRODUCTO in K_INHABILITACION_PRODUCTODal.", ex, true);
            }
        }
        /// <summary>
        /// Get all the disabled products
        /// </summary>
        /// <returns></returns>
        public List<string> GetWholeDisabledProducts()
        {
            List<string> products = new List<string>();
            try
            {
                string SQL = "SELECT DISTINCT [Id_Credito_Sustitucion] FROM [dbo].[K_INHABILITACION_PRODUCTO]";
                SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                while (sqlDataReader.Read())
                {
                    products.Add(sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Id_Credito_Sustitucion")).ToString());
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return products;
        }

        // updated by tina 2012-02-27
        // added by Tina 2012-02-20
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
        public DataTable GetUnableProductsForAct(string program, string technology, string fromDate, string toDate, int UserID, string UserType, string disposalId, string disposalType)
        {
            try
            {
                string SQL = "select distinct C.Id_Inhabilitacion,A.Id_Centro_Disp,A.Fg_Tipo_Centro_Disp from dbo.View_Supervision_ProductsII A" +
                            " inner join K_INHABILITACION_PRODUCTO C on A.Id_Credito_Sustitucion = C.Id_Credito_Sustitucion";

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
    }
}
