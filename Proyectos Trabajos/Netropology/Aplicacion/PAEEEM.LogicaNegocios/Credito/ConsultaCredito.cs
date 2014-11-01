using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AdminUsuarios;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.SolicitudCredito;

namespace PAEEEM.LogicaNegocios.Credito
{
    public class ConsultaCredito
    {
        public static SUBESTACIONES Subestacion(string nc)
        {
            var sub = new DetalleCredito().Subestacion(nc);
            return sub;
        }
        public static List<DatosConsulta> DatosBusquedaSolicitudes(string noCre = "", string rpu = "", string rfc = "", int estatus = 0, int reg = 0, string distrRs = "", string distrNc = "", int zon = 0, DateTime? fechaIn = null, DateTime? fechaFin = null)
        {
            var variables = new DetalleCredito().DatosBusquedaSolicitudes(noCre, rpu, rfc, estatus, reg, distrRs, distrNc, zon, fechaIn, fechaFin);
            return variables;
        }

        public static DataTable Credito(string noCre)
        {
             SqlParameter[] paras =
            {
                new SqlParameter("@NoCredito", noCre)
            };

             var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_DatosCredito", paras);


            return dtResult;
        }

        public static CAT_CODIGO_POSTAL_SEPOMEX Sepomex(string cp)
        {
            var spmx = new DetalleCredito().Sepomex(cp);
            return spmx;
        }

        public static CAT_ESTADO Estados(int cveEstado)
        {
            var estados = new DetalleCredito().Estados(cveEstado);
            return estados;
        }

        public static CAT_ESTADO Estados(string cveTrama)
        {
            var estados = new DetalleCredito().Estados(cveTrama);
            return estados;
        }

        public static CAT_DELEG_MUNICIPIO DelMun(int cveDm)
        {
            var dm = new DetalleCredito().DelMun(cveDm);
            return dm;
        }

        public static CAT_TIPO_PROPIEDAD TipoPropiedad(int clave)
        {
            var tp = new DetalleCredito().TipoPropiedad(clave);
            return tp;
        }


        public static List<CAT_ESTATUS_CREDITO> EstatusCredito()
        {
            var estatus = new DetalleCredito().EstatusCredito();
            return estatus;
        }

        public static List<CAT_ACCIONES> ObtenAcciones(int estatus, int idRol)
        { 
            var lista = new CreCredito().ObtenAcciones(estatus,idRol);
            return lista;
        }

        public static List<AccionUsuario> ObtenAccionesUsuario(int estatus, int idUsuario)
        {
            var lstUsuario = new CreCredito().ObtenAccionesUsuario(estatus, idUsuario);

            return lstUsuario;
        }

        public static Entidades.ResponseData DatosTrama(string rpu, DateTime fechaConsulta)
        {
            var trama = new DetalleCredito().DatosTrama(rpu, fechaConsulta);
            return trama;
        }

        public static Entidades.ResponseData DatosTrama(string NC)
        {
            var trama = new DetalleCredito().DatosTrama(NC);
            return trama;
        }

        public static bool EsNuevo(string NC)
        {
            var cre = new DetalleCredito().EsNuevo(NC);
            return cre;
        }

        public static List<K_CREDITO_AMORTIZACION> TablaAmortizacion(string NC)
        {
            var tabla = new DetalleCredito().TablaAmortizacion(NC);
            return tabla;
        }
    }
}
