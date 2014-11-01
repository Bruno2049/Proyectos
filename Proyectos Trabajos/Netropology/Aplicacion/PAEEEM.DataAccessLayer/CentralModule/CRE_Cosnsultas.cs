using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Entities;
using PAEEEM.Helpers;


namespace PAEEEM.DataAccessLayer.CentralModule
{
    public class CRE_Cosnsultas
    {

        public DataTable Get_Consultas()
        {
            try
            {
                string executesqlstr = "SELECT * FROM CRE_Cosnsultas";
                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Consultas cost failed: Execute method Get_Consultas in CRE_Cosnsultas.", ex, true);
            }
        }
        public DataTable Get_Data(string sQuety)
        {
            try
            {
                string executesqlstr = sQuety.ToString();
                return SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Consultas cost failed: Execute method Get_Data in CRE_Cosnsultas.", ex, true);
            }
        }
    }
    
}
