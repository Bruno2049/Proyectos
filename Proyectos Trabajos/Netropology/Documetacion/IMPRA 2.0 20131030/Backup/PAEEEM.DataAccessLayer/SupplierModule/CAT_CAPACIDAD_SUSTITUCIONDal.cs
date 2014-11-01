/* ----------------------------------------------------------------------
 * File Name: CAT_CAPACIDAD_SUSTITUCIONDal.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/11/10
 *
 * Description:   CAT_CAPACIDAD_SUSTITUCION data access lay
 *----------------------------------------------------------------------*/

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
    /// CAT_CAPACIDAD_SUSTITUCION data access lay
    /// </summary>
    public class CAT_CAPACIDAD_SUSTITUCIONDal
    {
        private static readonly CAT_CAPACIDAD_SUSTITUCIONDal _classinstance = new CAT_CAPACIDAD_SUSTITUCIONDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_CAPACIDAD_SUSTITUCIONDal ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Get Record by Credit
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CAT_CAPACIDAD_SUSTITUCIONByTechnology(int technologyID)
        {
            try
            {
                string SQL = "select *,(Convert(varchar,No_Capacidad)+' '+ Dx_Unidades) as CapacidadUnidades from CAT_CAPACIDAD_SUSTITUCION where Cve_Tecnologia=@Cve_Tecnologia ORDER BY No_Capacidad";//edit by coco 2012-04-12
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia",technologyID)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get CAT_CAPACIDAD_SUSTITUCION failed: Execute method Get_CAT_CAPACIDAD_SUSTITUCIONByTechnology in CAT_CAPACIDAD_SUSTITUCIONDal.", ex, true);
            }
        }

        // added by tina 2012-02-24
        /// <summary>
        /// Get All Record
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_CAPACIDAD_SUSTITUCION()
        {
            try
            {
                string SQL = "select * from CAT_CAPACIDAD_SUSTITUCION ORDER BY No_Capacidad";
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get CAT_CAPACIDAD_SUSTITUCION failed: Execute method Get_ALL_CAT_CAPACIDAD_SUSTITUCION in CAT_CAPACIDAD_SUSTITUCIONDal.", ex, true);
            }
        }

        // added by tina 2012/04/12
        /// <summary>
        /// Get Record by Credit
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CapacidaByTechnology(int technologyID)
        {
            try
            {
                string SQL = "select Cve_Capacidad_Sust,convert(nvarchar,No_Capacidad)+' '+ISNULL(Dx_Unidades,'') as No_Capacidad, No_Capacidad as orden from CAT_CAPACIDAD_SUSTITUCION where Cve_Tecnologia=@Cve_Tecnologia Order by orden";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia",technologyID)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get CAT_CAPACIDAD_SUSTITUCION failed: Execute method Get_CAT_CAPACIDAD_SUSTITUCIONByTechnology in CAT_CAPACIDAD_SUSTITUCIONDal.", ex, true);
            }
        }
        // end
    }
}
