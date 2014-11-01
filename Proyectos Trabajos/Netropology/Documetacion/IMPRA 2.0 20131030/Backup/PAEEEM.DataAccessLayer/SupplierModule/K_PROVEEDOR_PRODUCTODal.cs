/*	
	$Author:     coco,wang
	$Date:       2011-07-07	
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
    /// Data Access Layer for proveedor related products
    /// </summary>
    public class K_PROVEEDOR_PRODUCTODal
    {
        private static readonly K_PROVEEDOR_PRODUCTODal _classinstance = new K_PROVEEDOR_PRODUCTODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_PROVEEDOR_PRODUCTODal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get proveedor with primary key
        /// </summary>
        /// <param name="strProductId"></param>
        /// <param name="strProviderID"></param>
        /// <returns></returns>
        public DataTable Get_K_PROVEEDOR_PRODUCTO_ByPK(string strProductId, string strProviderID)
        {
            try
            {
                string SQL = "SELECT * from K_PROVEEDOR_PRODUCTO where Id_Proveedor=@Id_Proveedor and Cve_Producto=@Cve_Producto";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Proveedor", strProviderID),
                    new SqlParameter("@Cve_Producto",strProductId)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL,paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        //add by coco 2011-10-08
        /// <summary>
        /// Update provider products
        /// </summary>
        /// <param name="Provider_Product">proveedor products</param>
        /// <returns></returns>
        public int Update_K_PROVEEDOR_PRODUCTO(K_PROVEEDOR_PRODUCTOEntity Provider_Product)
        {
            try
            {
                string SQL = "update K_PROVEEDOR_PRODUCTO set Mt_Precio_Unitario=@Mt_Precio_Unitario,Dt_Fecha_Prov_Prod=@Dt_Fecha_Prov_Prod where Id_Proveedor=@Id_Proveedor and Cve_Producto=@Cve_Producto";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Proveedor", Provider_Product.Id_Proveedor),
                    new SqlParameter("@Cve_Producto",Provider_Product.Cve_Producto),
                    new SqlParameter("@Mt_Precio_Unitario",Provider_Product.Mt_Precio_Unitario),
                    new SqlParameter("@Dt_Fecha_Prov_Prod",Provider_Product.Dt_Fecha_Prov_Prod)
                };
               int Result= SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return Result;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get proveedor
        /// </summary>
        /// <param name="ProviderID">program</param>
        /// <returns></returns>
        public DataTable Get_K_PROVEEDOR_PRODUCTO_ByProviderID(int ProviderID)
        {
            try
            {
                //edit by tina 2012-07-18
                string SQL = "SELECT B.Cve_Tecnologia,B.Cve_Marca,B.Cve_Producto,B.Dx_Nombre_Producto,B.Dx_Modelo_Producto,A.Mt_Precio_Unitario,B.Mt_Precio_Max, Ft_Tipo_Producto from K_PROVEEDOR_PRODUCTO A INNER JOIN CAT_PRODUCTO B ON A.Cve_Producto=B.Cve_Producto where A.Id_Proveedor=@Id_Proveedor";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Proveedor", ProviderID)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }

        /// <summary>
        /// add record
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_K_PROVEEDOR_PRODUCTO(K_PROVEEDOR_PRODUCTOEntity instance)
        {
            try
            {
                string SQL = "insert into K_PROVEEDOR_PRODUCTO(Id_Proveedor,Cve_Producto,Cve_Estatus_Prov_Prod,Mt_Precio_Unitario,Dt_Fecha_Prov_Prod) values(@Id_Proveedor,@Cve_Producto,@Cve_Estatus_Prov_Prod,@Mt_Precio_Unitario,@Dt_Fecha_Prov_Prod)";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Proveedor", instance.Id_Proveedor),
                    new SqlParameter("@Cve_Producto",instance.Cve_Producto),
                    new SqlParameter("@Cve_Estatus_Prov_Prod",instance.Cve_Estatus_Prov_Prod),
                    new SqlParameter("@Mt_Precio_Unitario",instance.Mt_Precio_Unitario),
                    new SqlParameter("@Dt_Fecha_Prov_Prod",instance.Dt_Fecha_Prov_Prod)
                };
                int Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return Result;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }

        /// <summary>
        /// delete record
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public int Delete_K_PROVEEDOR_PRODUCTO(int supplierId,string productId)
        {
            try
            {
                string SQL = "delete from  K_PROVEEDOR_PRODUCTO where Id_Proveedor=" + supplierId + " and Cve_Producto in(" + productId + ")";
              
                int Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return Result;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
    }
}
