using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Data Access Layer for credit related product
    /// </summary>
    public class K_CREDITO_PRODUCTODal
    {
        /// <summary>
        /// Readonly variable for class instance
        /// </summary>
        private static readonly K_CREDITO_PRODUCTODal _classInstance = new K_CREDITO_PRODUCTODal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static K_CREDITO_PRODUCTODal ClassInstance { get { return _classInstance; } }
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public int Insert_K_CREDITO_PRODUCTO(K_CREDITO_PRODUCTOEntity Instance)
        {
            int iCount = 0;
            try
            {
                string SQL = "INSERT INTO K_CREDITO_PRODUCTO ([No_Credito] ,[Cve_Producto],[No_Cantidad],Mt_Precio_Unitario,Mt_Precio_Unitario_Sin_IVA,Mt_Total,Dt_Fecha_Credito_Producto,Cve_Producto_Capacidad, Mt_Gastos_Instalacion_Mano_Obra)  ";
                SQL = SQL + "   VALUES(@No_Credito,@Cve_Producto,@No_Cantidad,@Mt_Precio_Unitario,@Mt_Precio_Unitario_Sin_IVA,@Mt_Total,@Dt_Fecha_Credito_Producto,@Cve_Producto_Capacidad, @Mt_Gastos_Instalacion_Mano_Obra)";
                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@No_Credito", Instance.No_Credito),
                    new SqlParameter("@Cve_Producto", Instance.Cve_Producto),
                    new SqlParameter("@No_Cantidad", Instance.No_Cantidad),
                    new SqlParameter("@Mt_Precio_Unitario", Instance.Mt_Precio_Unitario),
                    new SqlParameter("@Mt_Precio_Unitario_Sin_IVA", Instance.Mt_Precio_Unitario_Sin_IVA),
                    new SqlParameter("@Mt_Gastos_Instalacion_Mano_Obra", Instance.Mt_Gastos_Instalacion_Mano_Obra),
                    new SqlParameter("@Mt_Total", Instance.Mt_Total),
                    new SqlParameter("@Dt_Fecha_Credito_Producto", Instance.Dt_Fecha_Credito_Producto),
                    new SqlParameter("@Cve_Producto_Capacidad",Instance.Cve_Producto_Capacidad)
                  
                };
                iCount = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return iCount;
        }
        /// <summary>
        /// get by Credit_No
        /// </summary>
        /// <param name="creditNo"></param>
        /// <returns></returns>
        public DataTable get_K_Credit_ProductByCreditNo(string creditNo)
        {
            try
            {
                //edit by coco 20110823
                //string SQL = "select B.Cve_Tecnologia as Technology,A.Cve_Producto as Modelo,C.Ft_Tipo_Producto as TypeProduct,B.Cve_Marca as Marca,B.Dx_Modelo_Producto as Modelo,A.No_Cantidad as Cantidad,A.Mt_Precio_Unitario as PrecioUnitario,A.Mt_Total  as Subtotal ,";
                //SQL = SQL + "A.Cve_Producto_Capacidad as Capacidad, Mt_Gastos_Instalacion_Mano_Obra Gastos from K_CREDITO_PRODUCTO A left join CAT_PRODUCTO B on A.Cve_Producto=B.Cve_Producto  inner join CAT_TIPO_PRODUCTO C on B.Ft_Tipo_Producto =C.Ft_Tipo_Producto  where No_Credito=@No_Credito";
                //end edit
                StringBuilder sql = new StringBuilder();
                sql.Append("select B.Cve_Tecnologia as Technology,");
                sql.Append("       A.Cve_Producto as Modelo,");
                sql.Append("       C.Ft_Tipo_Producto as TypeProduct,");
                sql.Append("       B.Cve_Marca as Marca,");
                sql.Append("       B.Dx_Modelo_Producto as Modelo,");
                sql.Append("       A.No_Cantidad as Cantidad,");
                sql.Append("       A.Mt_Precio_Unitario_Sin_IVA as PrecioUnitario,");
                sql.Append("       (A.Mt_Precio_Unitario_Sin_IVA * A.No_Cantidad )   as Subtotal ,");
                sql.Append("       b.Cve_Capacidad_Sust as Capacidad, ");
                sql.Append("       round((Mt_Gastos_Instalacion_Mano_Obra / (1 + (CRE.Tasa_IVA / 100))),2) Gastos, c.ADICIONADO_POR");
                sql.Append("  from K_CREDITO_PRODUCTO A left join CAT_PRODUCTO B on A.Cve_Producto=B.Cve_Producto  ");
                sql.Append("       inner join CAT_TIPO_PRODUCTO C on B.Ft_Tipo_Producto =C.Ft_Tipo_Producto ");
                sql.Append("       inner join CRE_CREDITO CRE on A.No_Credito = CRE.No_Credito ");
                sql.Append("    where A.No_Credito=@No_Credito");

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditNo)                   
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql.ToString(), paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditNo"></param>
        /// <returns></returns>
        public int Dalete_K_CREDITO_PRODUCTO(string creditNo)
        {
            int iCount = 0;
            try
            {
                string Sql = "DELETE FROM K_CREDITO_PRODUCTO WHERE No_Credito =@No_Credito";
                SqlParameter[] para = new SqlParameter[] { 
                 new SqlParameter("@No_Credito",creditNo)
                };
                iCount = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return iCount;
        }
        // Added by Tina 2011/08/23
        /// <summary>
        /// get all the technology of the requested product
        /// </summary>
        /// <param name="creditNo"></param>
        /// <param name="program"></param>
        /// <returns></returns>
        public DataTable get_All_RequestTechnologyByCredit(string creditNo, int program)
        {
            try
            {
                string SQL = "select distinct A.Cve_Tecnologia,A.Dx_Nombre_Particular from CAT_TECNOLOGIA A inner join ";
                SQL = SQL + "(select Cve_Tecnologia from CAT_PRODUCTO inner join K_CREDITO_PRODUCTO on CAT_PRODUCTO.Cve_Producto=K_CREDITO_PRODUCTO.Cve_Producto where K_CREDITO_PRODUCTO.No_Credito=@No_Credito) B ";
                SQL = SQL + " on A.Cve_Tecnologia=B.Cve_Tecnologia ";
                SQL = SQL + " inner join K_PROG_TECNOLOGIA C on B.Cve_Tecnologia=C.Cve_Tecnologia where C.ID_Prog_Proy=@ID_Prog_Proy";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditNo),
                    new SqlParameter("@ID_Prog_Proy", program)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        // End
        // Added by Tina 2011/08/31
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditNo"></param>
        /// <param name="program"></param>
        /// <returns></returns>
        public DataTable get_RequestTechnologyAmountByCredit(string creditNo, int program)
        {
            try
            {
                string SQL = "select A.Cve_Tecnologia Technology,B.No_Cantidad Amount from CAT_PRODUCTO A inner join K_CREDITO_PRODUCTO B on A.Cve_Producto=B.Cve_Producto and B.No_Credito=@No_Credito ";
                SQL = SQL + " inner join K_PROG_TECNOLOGIA C on A.Cve_Tecnologia=C.Cve_Tecnologia where C.ID_Prog_Proy=@ID_Prog_Proy";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@No_Credito", creditNo),
                    new SqlParameter("@ID_Prog_Proy", program)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        // End
    }
}
