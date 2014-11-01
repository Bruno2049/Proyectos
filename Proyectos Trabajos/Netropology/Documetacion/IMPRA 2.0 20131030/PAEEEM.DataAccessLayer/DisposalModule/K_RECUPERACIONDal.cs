using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;
using PAEEEM.Entities;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// 
    /// </summary>
    public class K_RECUPERACIONDal
    {
        private static readonly K_RECUPERACIONDal _classinstance = new K_RECUPERACIONDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_RECUPERACIONDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// check if the special material have registered
        /// </summary>
        /// <param name="technology"></param>
        /// <param name="material"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataTable GetTodayRecordByTechnologyAndMaterial(string technology, string material, string date, int ID_departamento, string Fg_Tipo_Centro_Disp)
        {
            try
            {
                string SQL = "select * from K_RECUPERACION where Cve_Tecnologia=@Cve_Tecnologia and Cve_Residuo_Material=@Cve_Residuo_Material and CONVERT(VARCHAR(10),Dt_Fecha_Recuperacion, 120)=@date and Id_Centro_Disp=@Departamento and Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia",technology),
                    new SqlParameter("@Cve_Residuo_Material",material),
                    new SqlParameter("@date",date),
                    new SqlParameter("@Departamento",ID_departamento),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",Fg_Tipo_Centro_Disp) 
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL,para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get today record with technology and material failed: Execute method GetTodayRecordByTechnologyAndMaterial in K_RECUPERACIONDal.", ex, true);
            }
        }
        /// <summary>
        /// insert K_RECUPERACION
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="Id_Recuperacion"></param>
        /// <returns></returns>
        public int Insert_K_RECUPERACION(K_RECUPERACIONModel instance, out int Id_Recuperacion)
        {
            int result = 0;
            try
            {
                string executesqlstr = "INSERT INTO K_RECUPERACION(No_Material,Dt_Fecha_Recuperacion,Cve_Tecnologia,Cve_Residuo_Material,Id_Centro_Disp,Fg_Tipo_Centro_Disp) VALUES" +
                                              "(@No_Material,@Dt_Fecha_Recuperacion,@Cve_Tecnologia,@Cve_Residuo_Material,@Id_Centro_Disp,@Fg_Tipo_Centro_Disp)" +
                                              " SELECT @Id_Recuperacion=@@IDENTITY";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Material",instance.No_Material),
                    new SqlParameter("@Dt_Fecha_Recuperacion",instance.Dt_Fecha_Recuperacion),
                    new SqlParameter("@Cve_Tecnologia",instance.Cve_Tecnologia),
                    new SqlParameter("@Cve_Residuo_Material",instance.Cve_Residuo_Material),
                    new SqlParameter("@Id_Centro_Disp",instance.Id_Centro_Disp),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",instance.Fg_Tipo_Centro_Disp),
                    new SqlParameter("@Id_Recuperacion",SqlDbType.Int)
                };
                para[6].Direction = ParameterDirection.Output;

                result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);

                int.TryParse(para[6].Value.ToString(), out Id_Recuperacion);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_RECUPERACION failed: Execute method Insert_K_RECUPERACION in K_RECUPERACIONDal.", ex, true);
            }
            return result;
        }
        // updated by tina 2012/04/12
        /// <summary>
        /// get all recovery materials
        /// </summary>
        /// <param name="program"></param>
        /// <param name="technology"></param>
        /// <param name="registryDate"></param>
        /// <param name="material"></param>
        /// <param name="disposalID"></param>
        /// <param name="disposalType"></param>
        /// <param name="sortName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetRecoveryMaterials(string program, string technology, string registryDate, string registryToDate, string material, int disposalID, string disposalType, string sortName, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            string strWhere = " INNER JOIN K_INHABILITACION_PRODUCTO B ON A.Id_Credito_Sustitucion=B.Id_Credito_Sustitucion INNER JOIN K_INHABILITACION C ON B.Id_Inhabilitacion=C.Id_Inhabilitacion";
            strWhere += " WHERE 1=1";
            try
            {
                if (program != "")
                {
                    strWhere += " AND A.ID_Prog_Proy=" + program;
                }
                if (technology != "")
                {
                    strWhere += " AND A.Cve_Tecnologia=" + technology;
                }
                if (registryDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),A.Dt_Fecha_Recuperacion, 120)>='" + registryDate + "'";
                }
                if (registryToDate != "")
                {
                    strWhere += " AND CONVERT(VARCHAR(10),A.Dt_Fecha_Recuperacion, 120)<='" + registryToDate + "'";
                }
                if (disposalID != 0)
                {
                    strWhere += " AND A.Id_Centro_Disp=" + disposalID;
                }
                if (disposalType != "")
                {
                    strWhere += " AND A.Fg_Tipo_Centro_Disp='" + disposalType + "'";
                }
                if (material != "")
                {
                    if (material == "0")
                    {
                        strWhere += " AND A.Cve_Residuo_Material IN(SELECT Cve_Residuo_Material FROM CAT_RESIDUO_MATERIAL WHERE ISNULL(Fg_Residuo_Material,0)=1)";
                    }
                    else
                    {
                        strWhere += " AND A.Cve_Residuo_Material=" + material;
                    }
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@OrderBy", sortName),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@spWhere", strWhere)
                };
                paras[0].Direction = ParameterDirection.Output;

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "UP_Pager_GetView_RecoveryMaterials", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Recovery Materials failed:Execute method  GetRecoveryMaterials in K_RECUPERACIONDal.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// get recovery material by pk
        /// </summary>
        /// <param name="Id_Recuperacion"></param>
        /// <returns></returns>
        public DataTable GetRecoveryMaterialByRecoveryID(string Id_Recuperacion)
        {
            try
            {
                string SQL = "select distinct Dx_Nombre_Programa,Dx_Nombre_General,Dt_Fecha_Recuperacion,No_Material,Dx_Residuo_Material_Gral,Dx_Unidades from dbo.View_All_Material_Recovery where Id_Recuperacion=@Id_Recuperacion";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Recuperacion",Id_Recuperacion)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Material Data and material failed: Execute method GetRecoveryMaterialByRecoveryID in K_RECUPERACIONDal.", ex, true);
            }
        }
        /// <summary>
        /// update material weight
        /// </summary>
        /// <param name="Id_Recuperacion"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public int UpdateMaterialWeight(string Id_Recuperacion,double weight)
        {
            int result = 0;
            try
            {
                string executesqlstr = "UPDATE K_RECUPERACION SET No_Material=@No_Material WHERE Id_Recuperacion=@Id_Recuperacion";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Material",weight),
                    new SqlParameter("@Id_Recuperacion",Id_Recuperacion)
                };

                result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update weight failed: Execute method UpdateMaterialWeight in K_RECUPERACIONDal.", ex, true);
            }
            return result;
        }
        /// <summary>
        /// delete K_RECUPERACION
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public int Delete_K_RECUPERACION(string Id_Recuperacion) //updated by tina 2012-03-09
        {
            int result = 0;
            try
            {
                string executesqlstr = "DELETE FROM K_RECUPERACION  WHERE Id_Recuperacion=@Id_Recuperacion";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Recuperacion",Id_Recuperacion)
                };
                result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr,para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "delete K_RECUPERACION failed: Execute method Delete_K_RECUPERACION in K_RECUPERACIONDal.", ex, true);
            }
            return result;
        }

        /// <summary>
        /// Update K_RECUPERACION Number of Final Act
        /// </summary>
        /// <param name="Id_Recuperacion"></param>
        /// <param name="finalActID"></param>
        /// <returns></returns>
        public int UpdateFinalActID(string Id_Recuperacion,string finalActID)
        {
            int result = 0;
            try
            {
                string executesqlstr = "UPDATE K_RECUPERACION SET Id_Acta_Recuperacion=@Id_Acta_Recuperacion WHERE Id_Recuperacion=@Id_Recuperacion";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Acta_Recuperacion",finalActID),
                    new SqlParameter("@Id_Recuperacion",Id_Recuperacion)
                };

                result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update K_RECUPERACION Number of Final Act failed: Execute method UpdateFinalActID in K_RECUPERACIONDal.", ex, true);
            }
            return result;
        }

        // added by tina 2012-03-13 //updated by tina 2012-08-10
        public int GetRecuperacionProductMaxRecoveryMaterialAmount(string Id_Recuperacion)
        {
            int maxMaterialAmount = 0;
            try
            {
                string SQL = "SELECT top 1 A.MaxRegistryMaterialOrder FROM(" +
                                  " SELECT Id_Credito_Sustitucion,MAX(Id_Orden) AS MaxRegistryMaterialOrder FROM dbo.View_All_Material_Recovery" +
                                  "  WHERE Id_Credito_Sustitucion IN " +
                                  " (SELECT DISTINCT Id_Credito_Sustitucion FROM K_RECUPERACION_PRODUCTO WHERE Id_Recuperacion=@Id_Recuperacion)" +
                                  " GROUP BY Id_Credito_Sustitucion) A" +
                                  " ORDER BY A.MaxRegistryMaterialOrder DESC";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Recuperacion",Id_Recuperacion)
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                if (o != null)
                {
                    maxMaterialAmount = Convert.ToInt32(o.ToString());
                }
                return maxMaterialAmount;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get max amount of recovery material failed: Execute method GetRecuperacionProductMaxRecoveryMaterialAmount in K_RECUPERACIONDal.", ex, true);
            }
        }

        // added by tina 2012/04/12
        /// <summary>
        /// Get date of last material recovery registry without Act Number
        /// </summary>
        /// <param name="program"></param>
        /// <param name="technology"></param>
        /// <param name="material"></param>
        /// <param name="disposalID"></param>
        /// <param name="disposalType"></param>
        /// <returns></returns>
        public DateTime GetLastMaterialRecoveryDateByTechnologyAndMaterial(string program, string technology, string material, int disposalID, string disposalType)
        {
            DateTime lastMaterialRecovery = DateTime.Now;

            string sql = "SELECT MAX(A.Dt_Fecha_Recuperacion) FROM View_All_Material_Recovery A";
            sql += " WHERE 1=1 AND ISNULL(A.Id_Acta_Recuperacion,'')=''";
            try
            {
                if (program != "")
                {
                    sql += " AND A.ID_Prog_Proy=" + program;
                }
                if (technology != "")
                {
                    sql += " AND A.Cve_Tecnologia=" + technology;
                }
                if (disposalID != 0)
                {
                    sql += " AND A.Id_Centro_Disp=" + disposalID;
                }
                if (disposalType != "")
                {
                    sql += " AND A.Fg_Tipo_Centro_Disp='" + disposalType + "'";
                }
                if (material != "")
                {
                    if (material == "0")
                    {
                        sql += " AND A.Cve_Residuo_Material IN(SELECT Cve_Residuo_Material FROM CAT_RESIDUO_MATERIAL WHERE ISNULL(Fg_Residuo_Material,0)=1)";
                    }
                    else
                    {
                        sql += " AND A.Cve_Residuo_Material=" + material;
                    }
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@spWhere", sql)
                };

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
                if (o != null && o != DBNull.Value)
                {
                    lastMaterialRecovery = Convert.ToDateTime(o.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get date of last material recovery registry without Act Number failed:Execute method  GetRecoveryMaterials in K_RECUPERACIONDal.", ex, true);
            }
            return lastMaterialRecovery;
        }
        // end
    }
}
