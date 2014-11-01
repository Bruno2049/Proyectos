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
    /// disposal center of assigned to supplier
    /// </summary>
    public class K_CAT_PROVEEDOR_CAT_CENTRO_DISPDal
    {
        /// <summary>
        /// readonly class instance
        /// </summary>
        private static readonly K_CAT_PROVEEDOR_CAT_CENTRO_DISPDal _classinstance = new K_CAT_PROVEEDOR_CAT_CENTRO_DISPDal();
        /// <summary>
        /// Property
        /// </summary>
        public static K_CAT_PROVEEDOR_CAT_CENTRO_DISPDal ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// insert K_CAT_PROVEEDOR_CAT_CENTRO_DISP
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_K_CAT_PROVEEDOR_CAT_CENTRO_DISP(K_CAT_PROVEEDOR_CAT_CENTRO_DISPModel instance)
        {
            try
            {
                string sql = " INSERT INTO K_CAT_PROVEEDOR_CAT_CENTRO_DISP(Id_Proveedor,Id_Centro_Disp,Fg_Tipo_Proveedor,Fg_Tipo_Centro_Disp)" +
                                " VALUES(@Id_Proveedor,@Id_Centro_Disp,@Fg_Tipo_Proveedor,@Fg_Tipo_Centro_Disp)";
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@Id_Proveedor",instance.Id_Proveedor),
                    new SqlParameter("@Id_Centro_Disp",instance.Id_Centro_Disp),
                    new SqlParameter("@Fg_Tipo_Proveedor",instance.Fg_Tipo_Proveedor),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",instance.Fg_Tipo_Centro_Disp)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_CAT_PROVEEDOR_CAT_CENTRO_DISP failed: Execute method Insert_K_CAT_PROVEEDOR_CAT_CENTRO_DISP in K_CAT_PROVEEDOR_CAT_CENTRO_DISPDal.", ex, true);
            }
        }

        /// <summary>
        /// get disposal of assigned to supplier
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <param name="SupplierType"></param>
        /// <returns></returns>
        public DataTable GetDisposalRelatedSupplier(int SupplierID,string SupplierType,int RegionalID)
        {
            try
            {
                string SQL = "select A.Id_Centro_Disp,A.Fg_Tipo_Centro_Disp,C.Id_Centro_Disp as MainCenterID from K_CAT_PROVEEDOR_CAT_CENTRO_DISP A " +
                                " inner join (select Id_Centro_Disp,'M' as DisposalType,Cve_Region from CAT_CENTRO_DISP union all " +
                                " select Id_Centro_Disp_Sucursal as Id_Centro_Disp,'B' as DisposalType,Cve_Region from CAT_CENTRO_DISP_SUCURSAL) B" +
                                " on A.Id_Centro_Disp=B.Id_Centro_Disp and A.Fg_Tipo_Centro_Disp=B.DisposalType" +
                                " left outer join CAT_CENTRO_DISP_SUCURSAL C" +
                                " on A.Id_Centro_Disp=C.Id_Centro_Disp_Sucursal and A.Fg_Tipo_Centro_Disp='B'" +
                                " where A.Id_Proveedor=@Id_Proveedor and A.Fg_Tipo_Proveedor=@Fg_Tipo_Proveedor"; // and B.Cve_Region=@Cve_Region";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Proveedor", SupplierID),
                    new SqlParameter("@Fg_Tipo_Proveedor", SupplierType),
                    new SqlParameter("@Cve_Region", RegionalID)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }


        public int Delete_K_CAT_PROVEEDOR_CAT_CENTRO_DISP(int supplierId,string supplierType,string disposalId,string disposalType)
        {
            try
            {
                string sql = " DELETE FROM K_CAT_PROVEEDOR_CAT_CENTRO_DISP WHERE Id_Proveedor=@Id_Proveedor AND Id_Centro_Disp=@Id_Centro_Disp AND Fg_Tipo_Proveedor=@Fg_Tipo_Proveedor AND Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@Id_Proveedor",supplierId),
                    new SqlParameter("@Id_Centro_Disp",disposalId),
                    new SqlParameter("@Fg_Tipo_Proveedor",supplierType),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",disposalType)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Delete record from  K_CAT_PROVEEDOR_CAT_CENTRO_DISP failed: Execute method Delete_K_CAT_PROVEEDOR_CAT_CENTRO_DISP in K_CAT_PROVEEDOR_CAT_CENTRO_DISPDal.", ex, true);
            }
        }
    }
}
