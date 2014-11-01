using System;
using System.Data;
using System.Web;
using System.Web.Services.Protocols;
using PAEEEM.Helpers.PersonaMoral1;
using System.Text;
using System.Collections.Generic;

namespace PAEEEM.Helpers
{
    public class PMHelper
    {
        #region Properties IN
        public string Producto { get; set; }
        public string Rfc { get; set; }
        public string Nombres { get; set; }
        public string Direccion1 { get; set; }
        public string CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string NumSolicitud { get; set; }
        #endregion

        #region Properties out
        public string Mop { get; set; }
        public string Folio { get; set; }
        public string Resultado { get; set; }
        #endregion

        public string ConsultarPersonaMoral()
        {

            PersonaMoral pm = new PersonaMoral();

            try
            {
                NumSolicitud = DateTime.Now.ToString("yyMMddHHmmss");
                Resultado = string.Empty;
                Folio = "0000001";
                Mop = "0";

                pm.Timeout = 30000; // RSA timeout in milliseconds
                Resultado = pm.enviarPaqueteConsulta(Producto, Rfc, Nombres, Direccion1, CodigoPostal, Colonia, Ciudad, Estado, Pais, NumSolicitud);

                ConsultaPersonaMoral();

                if (Resultado.Contains("Error"))
                    throw new Exception(Resultado);
            }
            catch (SoapException ex)
            {
                // Manejo de errores custom???
                //if (ex.Message == "Respuesta incorrecta de circulo de crédito, intente más tarde. Respuesta incorrecta de circulo de crédito, intente más tarde. Elemento cuentas vacío.")
                //{
                //    folio = "0000001";
                //    mop = "0";
                //}
                //else
                Resultado += ": Error en la consulta Web a Persona Moral: SOAP: " + ex.Message;
                throw new Exception(Resultado, ex);
            }
            catch (Exception ex)
            {
                Resultado += ": Error en la consulta Web a Persona Moral: Exception: " + ex.Message;
                throw new Exception(Resultado, ex);
            }
            finally
            {
                Insert_PMData();
            }

            return Mop;
        }

        private bool ConsultaPersonaMoral()
        {
            bool result = false;

            try
            {
                LsApplicationState ApplicationState = new LsApplicationState(HttpContext.Current.Application);
                LsDatabase Database = new LsDatabase(ApplicationState.SQLConnString);

                DataTable dt;
                // RSA Assume NumSolicitud is a number
                Database.ExecuteSQLDataTable(@"Select Mop, FolioConsulta, LogError, NumSolicitud from
                    OPENQUERY([10.55.210.48],'SELECT Top 1 Mop, FolioConsulta, LogError, NumSolicitud
                    FROM Circulo.dbo.PersonaMoral Where NumSolicitud=" + NumSolicitud + @"
                    Order by IdFolio Desc') a ", out dt);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["MOP"] != DBNull.Value && dt.Rows[0]["FolioConsulta"] != DBNull.Value)
                    {
                        Folio = dt.Rows[0]["FolioConsulta"].ToString();
                        Mop = dt.Rows[0]["MOP"].ToString();

                        result = true;
                    }
                    else if (dt.Rows[0]["LogError"] != DBNull.Value && !string.IsNullOrEmpty(dt.Rows[0]["LogError"].ToString().Trim()))
                        throw new Exception("LogError: " + dt.Rows[0]["LogError"].ToString());
                    else
                        throw new Exception("PersonaMoral: Error Registro vacio");
                }
                else
                {
                    throw new Exception("PersonaMoral: Error Registro no encontrado");
                }
            }
            catch (LsDAException ex)
            {
                throw new LsDAException(null, "DB Error:" + ex.Message, ex, true);
            }

            return result;
        }
        /// <summary>
        /// Add new record
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private void Insert_PMData()
        {
            try
            {
                LsApplicationState ApplicationState = new LsApplicationState(HttpContext.Current.Application);
                LsDatabase Database = new LsDatabase(ApplicationState.SQLConnString);

                StringBuilder executesqlstr = new StringBuilder("INSERT INTO PersonaMoralData (");
                StringBuilder valuessqlstr = new StringBuilder("Values (");

                Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    {"Producto"    , Producto.Length     > 5   ? Resultado.Substring(0, 4) + ">" : Producto},
                    {"Rfc"         , Rfc.Length          > 20  ? Resultado.Substring(0, 19) + ">" : Rfc},
                    {"Nombres"     , Nombres.Length      > 255 ? Resultado.Substring(0, 254) + ">" : Nombres},
                    {"Direccion1"  , Direccion1.Length   > 100 ? Resultado.Substring(0, 99) + ">" : Direccion1},
                    {"CodigoPostal", CodigoPostal.Length > 50  ? Resultado.Substring(0, 49) + ">" : CodigoPostal},
                    {"Colonia"     , Colonia.Length      > 50  ? Resultado.Substring(0, 49) + ">" : Colonia},
                    {"Ciudad"      , Ciudad.Length       > 50  ? Resultado.Substring(0, 49) + ">" : Ciudad},
                    {"Estado"      , Estado.Length       > 5   ? Resultado.Substring(0, 4) + ">" : Estado},
                    {"Pais"        , Pais.Length         > 5   ? Resultado.Substring(0, 4) + ">" : Pais},
                    {"NumSolicitud", NumSolicitud.Length > 15  ? Resultado.Substring(0, 14) + ">" : NumSolicitud},
                    {"Mop"         , Mop.Length          > 5   ? Resultado.Substring(0, 4) + ">" : Mop},
                    {"Folio"       , Folio.Length        > 100 ? Resultado.Substring(0, 99) + ">" : Folio},
                    {"Resultado"   , Resultado.Length    > 2048? Resultado.Substring(0, 2047) + ">" : Resultado}
                };

                foreach (string key in attributes.Keys)
                {
                    executesqlstr.Append(key);
                    executesqlstr.Append(',');

                    valuessqlstr.Append('@');
                    valuessqlstr.Append(key);
                    valuessqlstr.Append(',');
                    //System.Diagnostics.Debug.Print(key + " : " + attributes[key].Length);
                }
                executesqlstr.Length--;
                executesqlstr.Append(')');
                valuessqlstr.Length--;
                valuessqlstr.Append(')');
                executesqlstr.Append(valuessqlstr);

                Database.ExecuteSQLNonQuery(executesqlstr.ToString(), attributes);
            }
            catch (LsDAException ex)
            {
                throw new LsDAException(null, "PersonaMoralData INSERT: " + ex.Message, ex, true);
            }
        }
    }
}
