using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Services.Protocols;
using PAEEEM.AccesoDatos;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;

namespace PAEEEM.LogicaNegocios.AltaBajaEquipos
{
    public class InfoCompAltaBajaEquipos
    {
        public static DataTable Get_Info_Equipos_Baja(string noCredito,string valor)
        {

            const string sql = " Select S.No_Credito,Id_Credito_Sustitucion,grupo, " +
                                       " t.Dx_Nombre_General as Tecnologia, " +
                                       " t.Cve_Tecnologia,  " +
                                       " p.Dx_Tipo_Producto as producto , " +
                                       " s.CapacidadSistema as capacidad, " +
                                       " s.Unidad as unidades, " +
                                       " ISNULL(s.No_Unidades,1) as cantidad " +
                                " FROM [K_CREDITO_SUSTITUCION]  s join cat_tecnologia t on s.cve_tecnologia = t.cve_tecnologia " +
                                " 								join CAT_TIPO_PRODUCTO p on s.Dx_Tipo_Producto = p.Ft_Tipo_Producto  " +
                                " 								LEFT join CAT_CAPACIDAD_SUSTITUCION capSus on s.Cve_Capacidad_Sust= capSus.Cve_Capacidad_Sust " +
                                " WHERE No_Credito = @No_Credito ORDER BY Cve_Tecnologia";

            var para = new[] { 
                    new SqlParameter("@No_Credito",noCredito)
                };

            var dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, para);

            return dt;
        }

        public static List<GridEquiposBaja> Get_Info_Equipos_Baja(string noCredito)
        {
            return AccesoDatos.AltaBajaEquipos.BajaEquipos.Get_Info_Equipos_Baja(noCredito);
        }
        public static DataTable Get_CAT_CENTRO_DISPByTECHNOLOGY(int cveEstatusCentroDisp, string cveTecnologia, int idUsuario)
        {
            var sql =
                "SELECT * FROM ( SELECT DISTINCT A.Id_Centro_Disp,A.Cve_Estatus_Centro_Disp,A.Cve_Region,A.Dx_Razon_Social,A.Dx_Nombre_Comercial,A.Dx_RFC,Dx_Domicilio_Part_Calle,A.Dx_Domicilio_Part_Num," +
                "A.Dx_Domicilio_Part_CP,A.Cve_Deleg_Municipio_Part,A.Cve_Estado_Part,A.Fg_Mismo_Domicilio,A.Dx_Domicilio_Fiscal_Calle,A.Dx_Domicilio_Fiscal_Num,A.Dx_Domicilio_Fiscal_CP," +
                "A.Cve_Deleg_Municipio_Fisc,A.Cve_Estado_Fisc,A.Dx_Nombre_Repre,A.Dx_Email_Repre,A.Dx_Telefono_Repre,A.Dx_Nombre_Repre_Legal,A.Dx_Nombre_Banco,A.Dx_Cuenta_Banco," +
                "A.Binary_Acta_Constitutiva,A.Binary_Poder_Notarial,A.Dt_Fecha_Centro_Disp,A.Cve_Zona,A.Fg_Tipo_Centro_Disp FROM" +
                " (" +
                "    select (CONVERT(VARCHAR(10),Id_Centro_Disp)+'-'+'(Matriz)') as Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social+'(Matriz)' as Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                "    Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                "    Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                "    Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp,Cve_Zona,'M' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP" +
                " UNION" +
                "    select (CONVERT(VARCHAR(10),Id_Centro_Disp_Sucursal)+'-'+'(Sucursal)') as Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social+'(Sucursal)' as Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num," +
                "    Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP," +
                "    Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco," +
                "    Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp_Sucursal as Dt_Fecha_Centro_Disp,Cve_Zona,'B' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP_SUCURSAL" +
                " ) A" +

                " inner join K_CENTRO_DISP_TECNOLOGIA B" +
                " on (A.Id_Centro_Disp= (CONVERT(VARCHAR(10),B.Id_Centro_Disp)+'-'+'(Matriz)') or  A.Id_Centro_Disp= (CONVERT(VARCHAR(10),B.Id_Centro_Disp)+'-'+'(Sucursal)'))" +
                " and A.Fg_Tipo_Centro_Disp=B.Fg_Tipo_Centro_Disp " +

                " INNER JOIN" +
                " (" +
                "    SELECT US_USUARIO.Id_Usuario,K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Centro_Disp,/*'M' as*/ Fg_Tipo_Centro_Disp FROM US_USUARIO" +
                "    INNER JOIN CAT_PROVEEDOR ON US_USUARIO.Id_Departamento = CAT_PROVEEDOR.Id_Proveedor AND US_USUARIO.Tipo_Usuario = 'S' " +
                "    INNER JOIN K_CAT_PROVEEDOR_CAT_CENTRO_DISP ON CAT_PROVEEDOR.Id_Proveedor=K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Proveedor" +
                "            AND K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Fg_Tipo_Proveedor='M' " +
                "    UNION" +
                "    SELECT US_USUARIO.Id_Usuario,K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Centro_Disp,/*'B' as*/ Fg_Tipo_Centro_Disp FROM US_USUARIO" +
                "    INNER JOIN CAT_PROVEEDORBRANCH ON US_USUARIO.Id_Departamento = CAT_PROVEEDORBRANCH.Id_Branch AND US_USUARIO.Tipo_Usuario = 'S_B' " +
                "    INNER JOIN K_CAT_PROVEEDOR_CAT_CENTRO_DISP ON CAT_PROVEEDORBRANCH.Id_Branch=K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Proveedor" +
                "            AND K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Fg_Tipo_Proveedor='B' " +
                " ) C" +
                " ON C.Id_Centro_Disp= B.Id_Centro_Disp and C.Fg_Tipo_Centro_Disp=B.Fg_Tipo_Centro_Disp " +
                " INNER JOIN CAT_TECNOLOGIA tec on b.Cve_Tecnologia = tec.Cve_Tecnologia" +
                " AND tec.Dx_Nombre_General = '" + cveTecnologia + "'"+
                " AND C.Id_Centro_Disp= substring(A.Id_Centro_Disp, 1, charindex('-', A.Id_Centro_Disp)-1) and C.Fg_Tipo_Centro_Disp=A.Fg_Tipo_Centro_Disp" +
                " WHERE C.ID_USUARIO =@ID_Usuario";

            if (cveEstatusCentroDisp != 0)
            {
                sql = sql + " AND Cve_Estatus_Centro_Disp=@Cve_Estatus_Centro_Disp ";
            }

            sql = sql + " ) CONS ORDER BY Dx_Razon_Social";  
            var para = new[]
            {
                new SqlParameter("@Cve_Tecnologia", cveTecnologia),
                new SqlParameter("@Cve_Estatus_Centro_Disp", cveEstatusCentroDisp),
                new SqlParameter("@ID_Usuario", idUsuario)

            };
            var dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, para);

            return dt;

        }

        public static int GetIdCentroDisp(string cveTecnologia, int idUsuario)
        {
            var sql =
                "  select top 1 Dx_Razon_Social from " +
                " ( select (CONVERT(VARCHAR(10),Id_Centro_Disp)+'-'+'(Matriz)') as Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social+'(Matriz)' as Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num, " +
                "    Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP, " +
                "    Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco, " +
                "    Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp,Cve_Zona,'M' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP " +
                "  UNION " +
                "    select (CONVERT(VARCHAR(10),Id_Centro_Disp_Sucursal)+'-'+'(Sucursal)') as Id_Centro_Disp,Cve_Estatus_Centro_Disp,Cve_Region,Dx_Razon_Social+'(Sucursal)' as Dx_Razon_Social,Dx_Nombre_Comercial,Dx_RFC,Dx_Domicilio_Part_Calle,Dx_Domicilio_Part_Num, " +
                "    Dx_Domicilio_Part_CP,Cve_Deleg_Municipio_Part,Cve_Estado_Part,Fg_Mismo_Domicilio,Dx_Domicilio_Fiscal_Calle,Dx_Domicilio_Fiscal_Num,Dx_Domicilio_Fiscal_CP, " +
                "    Cve_Deleg_Municipio_Fisc,Cve_Estado_Fisc,Dx_Nombre_Repre,Dx_Email_Repre,Dx_Telefono_Repre,Dx_Nombre_Repre_Legal,Dx_Nombre_Banco,Dx_Cuenta_Banco, " +
                "    Binary_Acta_Constitutiva,Binary_Poder_Notarial,Dt_Fecha_Centro_Disp_Sucursal as Dt_Fecha_Centro_Disp,Cve_Zona,'B' as Fg_Tipo_Centro_Disp FROM CAT_CENTRO_DISP_SUCURSAL " +
                " ) A " +
                "  inner join K_CENTRO_DISP_TECNOLOGIA B " +
                " on (A.Id_Centro_Disp= (CONVERT(VARCHAR(10),B.Id_Centro_Disp)+'-'+'(Matriz)') or  A.Id_Centro_Disp= (CONVERT(VARCHAR(10),B.Id_Centro_Disp)+'-'+'(Sucursal)')) " +
                " and A.Fg_Tipo_Centro_Disp=B.Fg_Tipo_Centro_Disp  " +
                " INNER JOIN " +
                " ( " +
                "    SELECT US_USUARIO.Id_Usuario,K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Centro_Disp,/*'M' as*/ Fg_Tipo_Centro_Disp FROM US_USUARIO " +
                "     INNER JOIN CAT_PROVEEDOR ON US_USUARIO.Id_Departamento = CAT_PROVEEDOR.Id_Proveedor AND US_USUARIO.Tipo_Usuario = 'S'  " +
                "     INNER JOIN K_CAT_PROVEEDOR_CAT_CENTRO_DISP ON CAT_PROVEEDOR.Id_Proveedor=K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Proveedor " +
                "             AND K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Fg_Tipo_Proveedor='M'  " +
                "    UNION " +
                "   SELECT US_USUARIO.Id_Usuario,K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Centro_Disp,/*'B' as*/ Fg_Tipo_Centro_Disp FROM US_USUARIO " +
                "   INNER JOIN CAT_PROVEEDORBRANCH ON US_USUARIO.Id_Departamento = CAT_PROVEEDORBRANCH.Id_Branch AND US_USUARIO.Tipo_Usuario = 'S_B'  " +
                "  INNER JOIN K_CAT_PROVEEDOR_CAT_CENTRO_DISP ON CAT_PROVEEDORBRANCH.Id_Branch=K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Id_Proveedor " +
                "          AND K_CAT_PROVEEDOR_CAT_CENTRO_DISP.Fg_Tipo_Proveedor='B'  " +
                " ) C " +
                " ON C.Id_Centro_Disp= B.Id_Centro_Disp and C.Fg_Tipo_Centro_Disp=B.Fg_Tipo_Centro_Disp  " +
                " INNER JOIN CAT_TECNOLOGIA tec on b.Cve_Tecnologia = tec.Cve_Tecnologia " +
                " AND tec.Dx_Nombre_General = '" + cveTecnologia + "'" +
                " AND C.Id_Centro_Disp= substring(A.Id_Centro_Disp, 1, charindex('-', A.Id_Centro_Disp)-1) and C.Fg_Tipo_Centro_Disp=A.Fg_Tipo_Centro_Disp " +
                " join K_CREDITO_SUSTITUCION sust on sust.Id_Centro_Disp= c.Id_Centro_Disp " +
                " WHERE C.ID_USUARIO =@ID_Usuario";
            
            var para = new[]
            {
                new SqlParameter("@Cve_Tecnologia", cveTecnologia),
                new SqlParameter("@ID_Usuario", idUsuario)
            };

            var dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, para);
            var id = Convert.ToInt32(dt.Rows[0][0]);
            return id;
        }


        public static DataTable Get_Info_Equipos_Alta(string noCredito)
        {
            //const string sql =
            //    "Select a.ID_CREDITO_PRODUCTO,A.Grupo,t.Cve_Tecnologia,t.Dx_Nombre_General as tecnologia,m.dx_marca as marca,B.Dx_Modelo_Producto as Modelo, " +
            //    "case t.Cve_Esquema when 1 then sus.Dx_Capacidad else cast(sus.No_Capacidad as nvarchar(4)) +' ' + sus.Dx_Unidades end  capacidad," +
            //    "c.Dx_Tipo_Producto as Producto,A.No_Cantidad as Cantidad,A.Mt_Precio_Unitario as PrecioUnitario,A.Cve_Producto_Capacidad as Capacidad, " +
            //    "cre.Gastos_Instalacion,A.Mt_Total  as ImporteTotalSinIva " +
            //    "from K_CREDITO_PRODUCTO A left join CAT_PRODUCTO B on A.Cve_Producto=B.Cve_Producto  " +
            //    "inner join CAT_TIPO_PRODUCTO C on B.Ft_Tipo_Producto =C.Ft_Tipo_Producto  " +
            //    "join CAT_CAPACIDAD_SUSTITUCION sus on b.Cve_Capacidad_Sust = sus.Cve_Capacidad_Sust" +
            //    "join CAT_MARCA m on b.Cve_Marca = m.Cve_Marca " +
            //    "join CAT_TECNOLOGIA t on b.Cve_Tecnologia = t.Cve_Tecnologia " +
            //    "join CRE_Credito cre on a.No_Credito = cre.No_Credito " +
            //    " where a.No_Credito=@No_Credito";
            const string sql =
                " Select a.ID_CREDITO_PRODUCTO,A.Grupo,t.Cve_Tecnologia,t.Dx_Nombre_General as tecnologia, " +
                "m.dx_marca as marca,B.Dx_Modelo_Producto as Modelo, c.Dx_Tipo_Producto as Producto,A.No_Cantidad as Cantidad, " +
                "A.Mt_Precio_Unitario as PrecioUnitario,case t.Cve_Esquema when 1 then sus.Dx_Capacidad else cast(sus.No_Capacidad as varchar(max)) +' ' + sus.Dx_Unidades " +
                "end  capacidad,cre.Gastos_Instalacion,A.Mt_Total  as ImporteTotalSinIva, T.Cve_Esquema  " +
                "from K_CREDITO_PRODUCTO A left join CAT_PRODUCTO B on A.Cve_Producto=B.Cve_Producto  " +
                "inner join CAT_TIPO_PRODUCTO C on B.Ft_Tipo_Producto =C.Ft_Tipo_Producto  " +
                "join CAT_CAPACIDAD_SUSTITUCION sus on b.Cve_Capacidad_Sust = sus.Cve_Capacidad_Sust " +
                "join CAT_MARCA m on b.Cve_Marca = m.Cve_Marca " +
                "join CAT_TECNOLOGIA t on b.Cve_Tecnologia = t.Cve_Tecnologia " +
                "join CRE_Credito cre on a.No_Credito = cre.No_Credito " +
                " where a.No_Credito=@No_Credito";

            var para = new[]
            {
                new SqlParameter("@No_Credito", noCredito)
            };

            var dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, para);

            return dt;
        }

        public static string Get_Tipo_Movimiento(int idCreditoProducto)
        {
            return AccesoDatos.AltaBajaEquipos.AltaEquipos.ObtieneTipoMovientoTecnologia(idCreditoProducto);
        }

        //public static K_CREDITO_PRODUCTO ObtieneProductoPorIdCreditoProducto(int idCreditoProd)
        //{
        //    return AccesoDatos.AltaBajaEquipos.AltaEquipos.ObtieneKCreditoProductoPorId(idCreditoProd);
        //}


        public static int ObtieneCveProductoporIdCreditoProducto(int idCreditoProd)
        {
            return AccesoDatos.AltaBajaEquipos.AltaEquipos.ObtieneCveProductoPorIdCreditoProducto(idCreditoProd);
        }

        public static List<GrdEquiposAlta> Get_Info_Equipos_Alta_Por_Cantidad(DataTable dt)
        {
            // Pasamos el DataTable a una lista de objetos
            var lstEquipos = new List<GrdEquiposAlta>();
            foreach (DataRow row in dt.Rows)
            {
                // Validacion tecnologia illuminacion
                if (row["Cve_Esquema"].ToString().Equals("1"))
                {
                    var equipo = new GrdEquiposAlta();

                    equipo.ID_CREDITO_PRODUCTO = row["ID_CREDITO_PRODUCTO"] != null
                        ? (int)row["ID_CREDITO_PRODUCTO"]
                        : 0;
                    equipo.Grupo = row["Grupo"] != null ? row["Grupo"].ToString() : string.Empty;
                    equipo.tecnologia = row["tecnologia"] != null ? row["tecnologia"].ToString() : string.Empty;
                    equipo.marca = row["marca"] != null ? row["marca"].ToString() : string.Empty;
                    equipo.Modelo = row["Modelo"] != null ? row["Modelo"].ToString() : string.Empty;
                    equipo.Producto = row["Producto"] != null ? row["Producto"].ToString() : string.Empty;
                    equipo.Cantidad = row["Cantidad"] != null
                        ? int.Parse(row["Cantidad"].ToString()):0;
                    equipo.PrecioUnitario = row["PrecioUnitario"] != null
                        ? decimal.Parse(row["PrecioUnitario"].ToString())
                        : 0;
                    equipo.Capacidad = row["Capacidad"] != null ? row["Capacidad"].ToString() : null;
                    equipo.Gastos_Instalacion = row["Gastos_Instalacion"] != null
                        ? decimal.Parse(row["Gastos_Instalacion"].ToString().Trim().Equals(string.Empty)
                            ? "0"
                            : row["Gastos_Instalacion"].ToString())
                        : 0;
                    equipo.ImporteTotalSinIva = row["ImporteTotalSinIva"] != null
                        ? decimal.Parse(row["ImporteTotalSinIva"].ToString())
                        : 0;
                    equipo.idConsecutivo = 1;
                    equipo.Cve_Tecnologia = row["Cve_Tecnologia"] != null
                        ? int.Parse(row["Cve_Tecnologia"].ToString())
                        : 0;

                    lstEquipos.Add(equipo);
                }
                else
                {
                    int cantidad = row["Cantidad"] != null ? (int) row["Cantidad"] : 1;
                    if (cantidad > 0)
                    {
                        for (int i = 0; i < cantidad; i++)
                        {
                            var equipo = new GrdEquiposAlta();
                            
                            equipo.ID_CREDITO_PRODUCTO = row["ID_CREDITO_PRODUCTO"] != null
                                ? (int) row["ID_CREDITO_PRODUCTO"]
                                : 0;
                            equipo.Grupo = row["Grupo"] != null ? row["Grupo"].ToString() : string.Empty;
                            //Cve_Tecnologia = row["Cve_Tecnologia"] !=null?(int)row["Cve_Tecnologia"]:0;
                            equipo.tecnologia = row["tecnologia"] != null ? row["tecnologia"].ToString() : string.Empty;
                            equipo.marca = row["marca"] != null ? row["marca"].ToString() : string.Empty;
                            equipo.Modelo = row["Modelo"] != null ? row["Modelo"].ToString() : string.Empty;
                            equipo.Producto = row["Producto"] != null ? row["Producto"].ToString() : string.Empty;
                            equipo.Cantidad = 1;
                            equipo.PrecioUnitario = row["PrecioUnitario"] != null
                                ? decimal.Parse(row["PrecioUnitario"].ToString())
                                : 0;
                            equipo.Capacidad = row["Capacidad"] != null ? row["Capacidad"].ToString() : null;
                            equipo.Gastos_Instalacion = row["Gastos_Instalacion"] != null
                                ? decimal.Parse(row["Gastos_Instalacion"].ToString().Trim().Equals(string.Empty)
                                    ? "0"
                                    : row["Gastos_Instalacion"].ToString())
                                : 0;
                            equipo.ImporteTotalSinIva = row["ImporteTotalSinIva"] != null
                                ? decimal.Parse(row["ImporteTotalSinIva"].ToString())
                                : 0;
                            equipo.Cve_Tecnologia = row["Cve_Tecnologia"] != null
                                ? int.Parse(row["Cve_Tecnologia"].ToString())
                                : 0;
                            equipo.idConsecutivo = i + 1;
                            lstEquipos.Add(equipo);
                        }
                    }
                }
            }
            return lstEquipos;
        }

        public static List<GridEquiposBaja> Get_Info_Equipos_Baja_Por_Cantidad(DataTable dt)
        {
            // Pasamos el DataTable a una lista de objetos
            var lstEquipos = new List<GridEquiposBaja>();
            foreach (DataRow row in dt.Rows)
            {
                // Validacion tecnologia illuminacion
                if (row["Cve_Tecnologia"].ToString().Equals("3"))
                {
                    var equipo = new GridEquiposBaja();

                    equipo.No_Credito = row["No_Credito"].ToString();
                    equipo.Id_Credito_Sustitucion = row["Id_Credito_Sustitucion"] != null ? (int)row["Id_Credito_Sustitucion"]: 0;
                    equipo.Cve_Tecnologia = row["Cve_Tecnologia"] != null ? (int)row["Cve_Tecnologia"] : 0;
                    equipo.Tecnologia = row["Tecnologia"] != null ? row["Tecnologia"].ToString() : string.Empty;
                    equipo.Grupo = row["grupo"] != null ? row["grupo"].ToString() : string.Empty;
                    equipo.Producto = row["producto"] != null ? row["producto"].ToString() : string.Empty;
                    equipo.Cantidad = row["Cantidad"] != null? int.Parse(row["Cantidad"].ToString()) : 0;
                    equipo.Capacidad = row["Capacidad"] != null ? row["Capacidad"].ToString() :null;
                    equipo.idConsecutivo = 1;
                    equipo.Unidades = row["unidades"] != null ? row["unidades"].ToString() : string.Empty;
                    lstEquipos.Add(equipo);
                }
                else
                {
                    int cantidad = row["Cantidad"] != null ? (int)row["Cantidad"] : 1;
                    if (cantidad > 0)
                    {
                        for (int i = 0; i < cantidad; i++)
                        {
                            var equipo = new GridEquiposBaja();

                            equipo.No_Credito = row["No_Credito"].ToString();
                            equipo.Id_Credito_Sustitucion = row["Id_Credito_Sustitucion"] != null ? (int)row["Id_Credito_Sustitucion"] : 0;
                            equipo.Cve_Tecnologia = row["Cve_Tecnologia"] != null ? (int)row["Cve_Tecnologia"] : 0;
                            equipo.Tecnologia = row["Tecnologia"] != null ? row["Tecnologia"].ToString() : string.Empty;
                            equipo.Grupo = row["grupo"] != null ? row["grupo"].ToString() : string.Empty;
                            equipo.Producto = row["producto"] != null ? row["producto"].ToString() : string.Empty;
                            equipo.Cantidad = 1;
                            equipo.Capacidad = row["Capacidad"] != null ? row["Capacidad"].ToString() : null;
                            equipo.Unidades = row["unidades"] != null ? row["unidades"].ToString() : string.Empty;
                            equipo.idConsecutivo = i + 1;
                            lstEquipos.Add(equipo);
                        }
                    }
                }
            }
            return lstEquipos;
        }



        public static bool GuardarImagenFachada(CRE_FOTOS oFoto)
        {
              using (var context = new PAEEEM_DESAEntidades())
              {
                  
                  if(context.CRE_FOTOS.Any(p=>p.No_Credito== oFoto.No_Credito
                                                            && p.idTipoFoto == oFoto.idTipoFoto
                                                            && p.idConsecutivoFoto == oFoto.idConsecutivoFoto
                                                            )
                      )
                  {
                      var existDocument = context.CRE_FOTOS.FirstOrDefault(p => p.No_Credito == oFoto.No_Credito
                                                                           && p.idTipoFoto == oFoto.idTipoFoto
                                                                           && p.idConsecutivoFoto == oFoto.idConsecutivoFoto
                                                                         );

                      existDocument.Nombre = oFoto.Nombre;
                      existDocument.Extension = oFoto.Extension;
                      existDocument.Longitud = oFoto.Longitud;
                      existDocument.Foto = oFoto.Foto;
                      existDocument.FechaAdicion = oFoto.FechaAdicion;
                      existDocument.AdicionadoPor = oFoto.AdicionadoPor;
                      context.SaveChanges();
                  }
                  else
                  {
                      context.CRE_FOTOS.Add(oFoto);
                      context.SaveChanges();                      
                  }
                   
               
            }

            return true;
        }

        public static bool GuardarImagenCreditoProducto(CRE_FOTOS oFoto)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {

                if (context.CRE_FOTOS.Any(p => p.No_Credito == oFoto.No_Credito
                                                          && p.idTipoFoto == oFoto.idTipoFoto
                                                          && p.idConsecutivoFoto == oFoto.idConsecutivoFoto
                                                          && p.idCreditoProducto == oFoto.idCreditoProducto
                                                          )
                    )
                {
                    var existDocument = context.CRE_FOTOS.FirstOrDefault(p => p.No_Credito == oFoto.No_Credito
                                                                         && p.idTipoFoto == oFoto.idTipoFoto
                                                                         && p.idConsecutivoFoto == oFoto.idConsecutivoFoto
                                                                         && p.idCreditoProducto == oFoto.idCreditoProducto
                                                                       );

                    existDocument.Nombre = oFoto.Nombre;
                    existDocument.Extension = oFoto.Extension;
                    existDocument.Longitud = oFoto.Longitud;
                    existDocument.Foto = oFoto.Foto;
                    existDocument.FechaAdicion = oFoto.FechaAdicion;
                    existDocument.AdicionadoPor = oFoto.AdicionadoPor;
                    context.SaveChanges();
                }
                else
                {
                    context.CRE_FOTOS.Add(oFoto);
                    context.SaveChanges();
                }


            }

            return true;
        }

        public static bool GuardarImagenCreditoSustitucion(CRE_FOTOS oFoto)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {

                if (context.CRE_FOTOS.Any(p => p.No_Credito == oFoto.No_Credito
                                                          && p.idTipoFoto == oFoto.idTipoFoto
                                                          && p.idConsecutivoFoto == oFoto.idConsecutivoFoto
                                                          && p.IdCreditoSustitucion == oFoto.IdCreditoSustitucion
                                                          )
                    )
                {
                    var existDocument = context.CRE_FOTOS.FirstOrDefault(p => p.No_Credito == oFoto.No_Credito
                                                                         && p.idTipoFoto == oFoto.idTipoFoto
                                                                         && p.idConsecutivoFoto == oFoto.idConsecutivoFoto
                                                                         && p.IdCreditoSustitucion == oFoto.IdCreditoSustitucion
                                                                       );

                    existDocument.Nombre = oFoto.Nombre;
                    existDocument.Extension = oFoto.Extension;
                    existDocument.Longitud = oFoto.Longitud;
                    existDocument.Foto = oFoto.Foto;
                    existDocument.FechaAdicion = oFoto.FechaAdicion;
                    existDocument.AdicionadoPor = oFoto.AdicionadoPor;
                    context.SaveChanges();
                }
                else
                {
                    context.CRE_FOTOS.Add(oFoto);
                    context.SaveChanges();
                }


            }

            return true;
        }

        public static CRE_FOTOS ObtenertImagenFachada(CRE_FOTOS oFoto)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                var existDocument = context.CRE_FOTOS.FirstOrDefault(p => p.No_Credito == oFoto.No_Credito
                                                                          && p.idTipoFoto == oFoto.idTipoFoto
                                                                          && p.idConsecutivoFoto == oFoto.idConsecutivoFoto);
                return existDocument;
           
            }
        }
        public static CRE_FOTOS ObtenertImagenCreditoProducto(CRE_FOTOS oFoto)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                var existDocument = context.CRE_FOTOS.FirstOrDefault(p => p.No_Credito == oFoto.No_Credito
                                                                          && p.idTipoFoto == oFoto.idTipoFoto
                                                                          && p.idConsecutivoFoto == oFoto.idConsecutivoFoto
                                                                          && p.idCreditoProducto == oFoto.idCreditoProducto);
                return existDocument;

            }
        }
        public static CRE_FOTOS ObtenertImagenCreditoSustitucion(CRE_FOTOS oFoto)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                var existDocument = context.CRE_FOTOS.FirstOrDefault(p => p.No_Credito == oFoto.No_Credito
                                                                          && p.idTipoFoto == oFoto.idTipoFoto
                                                                          && p.IdCreditoSustitucion == oFoto.IdCreditoSustitucion);
                return existDocument;

            }
        }
        public static bool ImagenAsignada(CRE_FOTOS oFoto)
        {
            try
            {

            using (var context = new PAEEEM_DESAEntidades())
            {
                return (from p in context.CRE_FOTOS
                    where p.No_Credito == oFoto.No_Credito
                          && p.idTipoFoto == oFoto.idTipoFoto
                          && p.idConsecutivoFoto == oFoto.idConsecutivoFoto
                          && p.idCreditoProducto == oFoto.idCreditoProducto
                    select new 
                           {
                               p.No_Credito,
                           }).Any();
            }
            }
            catch (Exception er)
            {
                throw er;
            }
        }
        public static bool ImagenAsignadaCreditoSustitucion(CRE_FOTOS oFoto)
        {
            try
            {

                using (var context = new PAEEEM_DESAEntidades())
                {
                    return (from p in context.CRE_FOTOS
                            where p.No_Credito == oFoto.No_Credito
                                  && p.idTipoFoto == oFoto.idTipoFoto
                                 && p.IdCreditoSustitucion == oFoto.IdCreditoSustitucion
                            select new
                            {
                                p.No_Credito,
                            }).Any();
                }
            }
            catch (Exception er)
            {
                throw er;
            }
        }

        public static bool CargaCompletaCreditoProducto(string no_credito,int idconsecutivo, int idTecnologia,int idCreditoProducto)
        {
            try
            {
                bool resultado = false,conFotos = false,HorarioCompleta;


                using (var context = new PAEEEM_DESAEntidades())
                {
                    var tipoMovimiento = (context.CAT_TECNOLOGIA.Where(p => p.Cve_Tecnologia == idTecnologia).Select(p => new
                                                                                                                   {
                                                                                                                       p.Cve_Tipo_Movimiento
                                                                                                                   })).FirstOrDefault();
                    if (tipoMovimiento != null)
                    {

                        var isCompleat = (from p in context.CRE_FOTOS
                            where p.No_Credito == no_credito
                                  && p.idCreditoProducto == idCreditoProducto
                                  && p.idConsecutivoFoto == idconsecutivo
                                  && p.idTipoFoto != 1
                            select p.No_Credito).Count();

                        switch (tipoMovimiento.Cve_Tipo_Movimiento)
                        {
                            case  "1": // ambos equipos
                                conFotos = isCompleat == 2;
                            break;
                            case "2": // solo equipos nuevos
                                conFotos = isCompleat == 1;
                            break;
                        }

                    }

                    var isHorarioCompleta = (from c in context.CLI_HORARIOS_OPERACION
                                                 where c.No_Credito == no_credito
                                                       && c.ID_CREDITO_PRODUCTO == idCreditoProducto
                                                       && c.IDCONSECUTIVO == idconsecutivo
                                                       && c.IDTIPOHORARIO != 1
                                                 select c).Count();
                    
                    HorarioCompleta = isHorarioCompleta > 0;

                    if (HorarioCompleta && conFotos) resultado = true;
                    
                    return resultado;

                }
            }
            catch (Exception er)
            {
                throw er;
            }
        }
        public static bool CargaCompletaCreditoSustitucion(string no_credito,int idTecnologia, int idCreditoSustitucion)
        {
            try
            {
                bool resultado = false;
                using (var context = new PAEEEM_DESAEntidades())
                {
                    var tipoMovimiento = (context.CAT_TECNOLOGIA.Where(p => p.Cve_Tecnologia == idTecnologia).Select(p => new
                    {
                        p.Cve_Tipo_Movimiento
                    })).FirstOrDefault();
                    
                    if (tipoMovimiento != null)
                    {

                        var isFotoCompleat = (from p in context.CRE_FOTOS
                                          where p.No_Credito == no_credito
                                                && p.IdCreditoSustitucion == idCreditoSustitucion
                                                && p.idTipoFoto != 1
                                          select p.No_Credito).Count();

                        
                        var isHorarioCompleta = (from c in context.CLI_HORARIOS_OPERACION
                                                 where c.No_Credito == no_credito
                                                       && c.Id_Credito_Sustitucion == idCreditoSustitucion
                                                 select c).Count();

                        resultado = (isFotoCompleat * isHorarioCompleta)>0;

                    }

                    return resultado;

                }
            }
            catch (Exception er)
            {
                throw er;
            }
        }

        public static bool ExisteFotoFachada(CRE_FOTOS oFoto)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                var existDocument = context.CRE_FOTOS.Any(p => p.No_Credito == oFoto.No_Credito
                                                                          && p.idTipoFoto == oFoto.idTipoFoto
                                                                          && p.idConsecutivoFoto == oFoto.idConsecutivoFoto
                                                                          && p.idCreditoProducto == null
                                                                          && p.IdCreditoSustitucion == null);
                return existDocument;

            }
        }
        public static bool ExisteFotoEquipoCreditoProducto(CRE_FOTOS oFoto)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                var existDocument = context.CRE_FOTOS.Any(p => p.No_Credito == oFoto.No_Credito
                                                            && p.idTipoFoto == oFoto.idTipoFoto
                                                            && p.idCreditoProducto == oFoto.idCreditoProducto
                                                            && p.idConsecutivoFoto == oFoto.idConsecutivoFoto);
                return existDocument;

            }
        }
        public static bool ExistenHorariosOperacion(CLI_HORARIOS_OPERACION oHoperacion)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                var existHorarios = context.CLI_HORARIOS_OPERACION.Count(p => p.No_Credito == oHoperacion.No_Credito
                                                            && p.IDTIPOHORARIO == oHoperacion.IDTIPOHORARIO
                                                            && p.IDCONSECUTIVO == oHoperacion.IDCONSECUTIVO);
                return existHorarios > 0;

            }
        }
        public static bool ExistenHorariosTotal(H_OPERACION_TOTAL oHoperacion)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                var existHorarios = context.H_OPERACION_TOTAL.Count(p => p.No_Credito == oHoperacion.No_Credito
                                                            && p.IDTIPOHORARIO == oHoperacion.IDTIPOHORARIO
                                                            && p.IDCONSECUTIVO == oHoperacion.IDCONSECUTIVO);
                return existHorarios > 0;

            }
        }

        public static  bool TieneFoto(string No_Credito)
        {
            return PAEEEM.AccesoDatos.AltaBajaEquipos.AltaEquipos.TieneFotos(No_Credito);
        }
    }

}
