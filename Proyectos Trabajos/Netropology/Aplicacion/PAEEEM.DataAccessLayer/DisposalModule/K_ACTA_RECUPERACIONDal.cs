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
    /// K_ACTA_RECUPERACION data access lay
    /// </summary>
    public class K_ACTA_RECUPERACIONDal
    {
        private static readonly K_ACTA_RECUPERACIONDal _classinstance = new K_ACTA_RECUPERACIONDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_ACTA_RECUPERACIONDal ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get last date from final disposal act 
        /// </summary>
        /// <returns></returns>
        public string GetFromDateForFinalActOrSupervision(string TechnologyID)
        {
            string date = "";
            try
            {
                string SQL = @"if(ISNULL((select top 1 Id_Acta_Recuperacion from [K_RECUPERACION] where [Cve_Tecnologia] = @Cve_Tecnologia order by Id_Acta_Recuperacion desc), '0') = '0')  
	                                SELECT  convert(char(10),min(Dt_Fecha_Recepcion) ,120)        
		                            FROM dbo.K_CREDITO_SUSTITUCION A  where [Cve_Tecnologia] = @Cve_Tecnologia
                                    else 
	                                SELECT CONVERT(char(10), DATEADD(D,1, max(A.Dt_Fecha_Recepcion)),120)      
		                            FROM dbo.K_CREDITO_SUSTITUCION A INNER JOIN
                                    dbo.K_RECUPERACION_PRODUCTO B ON 
                                    A.Id_Credito_Sustitucion = B.Id_Credito_Sustitucion INNER JOIN
                                    dbo.K_RECUPERACION C ON B.Id_Recuperacion = C.Id_Recuperacion inner JOIN
                                    dbo.K_ACTA_RECUPERACION  D ON C.Id_Acta_Recuperacion = D .Id_Acta_Recuperacion
                                    where A.Cve_Tecnologia=@Cve_Tecnologia     ";
                SqlParameter[] para = new SqlParameter[]
                {                 
                    new SqlParameter("@Cve_Tecnologia",TechnologyID)
                };
                object result = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL,para);
                if (result != null)
                {
                    date = result.ToString();
                }
                return date;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get last create date failed: Execute method GetFromDateForFinalActOrSupervision in K_ACTA_RECUPERACIONDal.", ex, true);
            }
        }

        /// <summary>
        /// insert
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_K_ACTA_RECUPERACION(K_ACTA_RECUPERACIONModel instance)
        {
            int result = 0;
            try
            {
                string SQL = "insert into K_ACTA_RECUPERACION(Id_Acta_Recuperacion,Dt_Fe_Inicio_Recup,Dt_Fe_Fin_Recup,Dt_Fe_Creacion)"+
                                                                      " values (@Id_Acta_Recuperacion,@Dt_Fe_Inicio_Recup,@Dt_Fe_Fin_Recup,@Dt_Fe_Creacion) ";
                SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@Id_Acta_Recuperacion",instance.Id_Acta_Recuperacion),
                    new SqlParameter("@Dt_Fe_Inicio_Recup",instance.Dt_Fe_Inicio_Recup),
                    new SqlParameter("@Dt_Fe_Fin_Recup",instance.Dt_Fe_Fin_Recup),
                    new SqlParameter("@Dt_Fe_Creacion",instance.Dt_Fe_Creacion)
                };
                result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return result;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Insert K_ACTA_RECUPERACION failed: Execute method Insert_K_ACTA_RECUPERACION in K_ACTA_RECUPERACIONDal.", ex, true);
            }
        }
    }
}
