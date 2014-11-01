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
    public class K_PRODUCTO_CHARACTERSDal
    {        
            private static readonly K_PRODUCTO_CHARACTERSDal _classinstance = new K_PRODUCTO_CHARACTERSDal();
            /// <summary>
            /// Class Instance
            /// </summary>
            public static K_PRODUCTO_CHARACTERSDal ClassInstance { get { return _classinstance; } }
            public int Insert_K_PRODUCTO_CHARACTERS(K_PRODUCTO_CHARACTERSEntity instance)
            {
                try
                {
                    string executesqlstr = "INSERT INTO K_PRODUCTO_CHARACTERS (Dx_Marca,No_Serial, Dx_Color, No_Peso, Cve_Capacidad_Sust, Dx_Antiguedad,Id_Pre_Folio,Id_Credito_Sustitucion,Dx_Modelo_Producto)" +
                                                    "VALUES (@Dx_Marca,@No_Serial,@Dx_Color,@No_Peso, @Cve_Capacidad_Sust, @Dx_Antiguedad,@Id_Pre_Folio,@Id_Credito_Sustitucion,@Dx_Modelo_Producto)";
                    SqlParameter[] para = new SqlParameter[] {                    
                    new SqlParameter("@Dx_Marca",instance.Dx_Marca),
                    new SqlParameter("@No_Serial",instance.No_Serial),
                    new SqlParameter("@Dx_Color", instance.Dx_Color),
                    new SqlParameter("@No_Peso", instance.No_Peso),
                    new SqlParameter("@Cve_Capacidad_Sust", instance.Cve_Capacidad_Sust),
                    new SqlParameter("@Dx_Antiguedad",instance.Dx_Antiguedad),
                    new SqlParameter("@Id_Credito_Sustitucion",instance.Id_Credito_Sustitucion),
                    new SqlParameter("@Id_Pre_Folio",instance.Id_Pre_Folio),
                    new SqlParameter("@Dx_Modelo_Producto",instance.Dx_Modelo_Producto)
                };                   
                    return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
                }
                catch (SqlException ex)
                {
                    throw new LsDAException(this, "Add K_PRODUCTO_CHARACTERS failed: Execute method Insert_K_PRODUCTO_CHARACTERS in K_PRODUCTO_CHARACTERSDal.", ex, true);
                }
            }      
    }
}
