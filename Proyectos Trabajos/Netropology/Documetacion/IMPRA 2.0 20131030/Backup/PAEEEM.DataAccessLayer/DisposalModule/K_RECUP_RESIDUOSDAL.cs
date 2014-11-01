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
    /// K_RECUP_RESIDUOSDAL data access lay
    /// </summary>
    public class K_RECUP_RESIDUOSDAL
    {
        private static readonly K_RECUP_RESIDUOSDAL _classinstance = new K_RECUP_RESIDUOSDAL();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_RECUP_RESIDUOSDAL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_K_RECUP_RESIDUOS(K_RECUP_RESIDUOSModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO K_RECUP_RESIDUOS (Codigo,No_Credito,ID_Centro_Disp,Codigo_Producto,Cve_Material,Valor_Material,Fg_Incluido,Fg_Tipo_Centro_Disp,Dt_Fecha_Creacion,ID_Estatus) " +
                                              " VALUES(@Codigo,@No_Credito,@ID_Centro_Disp,@Codigo_Producto,@Cve_Material,@Valor_Material,@Fg_Incluido,@Fg_Tipo_Centro_Disp,@Dt_Fecha_Creacion,@ID_Estatus)";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Codigo",instance.Codigo),
                    new SqlParameter("@No_Credito",instance.No_Credito),
                    new SqlParameter("@ID_Centro_Disp",instance.ID_Centro_Disp),
                    new SqlParameter("@Codigo_Producto",instance.Codigo_Producto),
                    new SqlParameter("@Cve_Material",instance.Cve_Material),
                    new SqlParameter("@Valor_Material",instance.Valor_Material),
                    new SqlParameter("@Fg_Incluido",instance.Fg_Incluido),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",instance.Fg_Tipo_Centro_Disp),
                    new SqlParameter("@Dt_Fecha_Creacion",instance.Dt_Fecha_Creacion),
                    new SqlParameter("@ID_Estatus",instance.ID_Estatus)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_RECUP_RESIDUOS failed: Execute method Insert_K_RECUP_RESIDUOS in K_RECUP_RESIDUOSDAL.", ex, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Fg_Incluido"></param>
        /// <param name="ID_Estatus"></param>
        /// <param name="Credit"></param>
        /// <param name="Codigo_Producto"></param>
        /// <returns></returns>
        public int UpdateK_RECUP_RESIDUOSStatus(string Fg_Incluido, int ID_Estatus, string Credit, string Codigo_Producto)
        {
            try
            {
                string executesqlstr = "update K_RECUP_RESIDUOS set Fg_Incluido =@Fg_Incluido,ID_Estatus=@ID_Estatus where No_Credito=@No_Credito and Codigo_Producto=@Codigo_Producto ";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Fg_Incluido",Fg_Incluido),     
                    new SqlParameter("@ID_Estatus",ID_Estatus),
                    new SqlParameter("@No_Credito",Credit),
                    new SqlParameter("@Codigo_Producto",Codigo_Producto)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update K_CREDITO_SUSTITUCION failed: Execute method UpdateK_CREDITO_SUSTITUCIONEstatusToRecuperacion in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
        }
        /// <summary>
        /// get material data by credit and product code
        /// </summary>
        /// <param name="No_Credito"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public DataTable Get_MaterialByCreditAndProduct(string No_Credito, string Product)
        {
            try
            {
                string SQL = "SELECT * FROM K_RECUP_RESIDUOS where No_Credito=@No_Credito and Codigo_Producto=@Codigo_Producto";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@No_Credito",No_Credito),
                    new SqlParameter("@Codigo_Producto",Product)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Material Data failed: Execute method Get_MaterialByCreditAndProduct in K_CREDITO_SUSTITUCIONDAL.", ex, true);
            }
        }
    }
}
