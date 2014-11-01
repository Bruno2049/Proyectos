using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class MotivosNoCreditos
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public List<MotivosCredito> ConsultaPresupuestoInversion(string rpu)
        {
            var query = from k in _contexto.K_LOG.AsEnumerable()
                        join p in _contexto.CAT_TIPO_PROCESOS on k.IDTIPOPROCESO equals p.IDTIPOPROCESO
                        join c in _contexto.CAT_TAREAS_PROCESOS on k.IDTAREA equals c.IDTAREA
                        join z in _contexto.CAT_ZONA on k.IDZONA equals z.Cve_Zona
                        join r in _contexto.CAT_REGION on z.Cve_Region equals r.Cve_Region
                        where k.IDTIPOPROCESO == 11 && k.TAREA_LOTE_NOCRED == rpu
                        select new MotivosCredito
                        {
                            NoIntentos = "",
                            NombreRazonSocial = "N/A",
                            Causa = p.DESCRIPCION,
                            Motivo = c.DESCRIPCION,
                            Datos = k.NOTA,
                            Fecha = k.FECHA_ADICION,
                            Region = r.Dx_Nombre_Region,
                            Zona = z.Dx_Nombre_Zona,
                            Distribuidor = "N/A",
                            IdTrama = "N/A",
                            Secuencia_E_Alta = (int)k.Secuencia_E_Alta,
                            Secuencia_E_Baja = (int)k.Secuencia_E_Baja
                            
                        };

            return query.ToList();
        }

        public List<MotivosCredito> ConsultaValidacionRpu(string rpu)
        {
            var query = from k in _contexto.K_LOG.AsEnumerable()
                join p in _contexto.CAT_TIPO_PROCESOS on k.IDTIPOPROCESO equals p.IDTIPOPROCESO
                join c in _contexto.CAT_TAREAS_PROCESOS on k.IDTAREA equals c.IDTAREA
                join z in _contexto.CAT_ZONA on k.IDZONA equals z.Cve_Zona
                join r in _contexto.CAT_REGION on z.Cve_Region equals r.Cve_Region
                where k.IDTIPOPROCESO == 10 && k.TAREA_LOTE_NOCRED == rpu
                select new MotivosCredito
                {
                    NoIntentos = "",
                    NombreRazonSocial = "N/A",
                    Causa = p.DESCRIPCION,
                    Motivo = c.DESCRIPCION,
                    Datos = k.NOTA,
                    Fecha = k.FECHA_ADICION,
                    Region = r.Dx_Nombre_Region,
                    Zona = z.Dx_Nombre_Zona,
                    Distribuidor = "N/A",
                    IdTrama = "N/A"
                };

            return query.ToList();
        }

        public List<MotivosCredito> ConsultaCancelaciones(string rpu)
        {
            var query = from cre in _contexto.CRE_Credito.AsEnumerable()
                join cr in _contexto.CANCELAR_RECHAZAR on cre.No_Credito equals cr.No_Credito
                join cli in _contexto.CLI_Cliente on
                    new {CP = (int) cre.Id_Proveedor, CB = (int) cre.Id_Branch, CC = (int) cre.IdCliente} equals
                    new {CP = cli.Id_Proveedor, CB = cli.Id_Branch, CC = cli.IdCliente}
                join mot in _contexto.MOTIVOS_RECHAZOS_CANCELACIONES on cr.ID_MOTIVO equals mot.ID_MOTIVO
                join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                from catPb in pb.DefaultIfEmpty()
                join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                join catR in _contexto.CAT_REGION on (cre.Id_Branch == 0 ? catP.Cve_Region : catPb.Cve_Region) equals
                    catR.Cve_Region
                join catZ in _contexto.CAT_ZONA on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals
                    catZ.Cve_Zona

                where cre.RPU == rpu
                select new MotivosCredito
                {
                    NoIntentos = "0",
                    NombreRazonSocial = cli.Razon_Social ?? cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
                    Causa = "Cancelaciones",
                    Motivo = mot.DESCRIPCION,
                    Datos = cre.No_Credito,
                    Fecha = cr.FECHA_ADICION,
                    Region = catR.Dx_Nombre_Region,
                    Zona = catZ.Dx_Nombre_Zona,
                    Distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
                    IdTrama = cre.Id_Trama.ToString()
                };

            return query.ToList();
        }

        public List<MotivosCredito> ConsultaCrediticiaMopNoValido(string rpu)
        {
            var query = from cre in _contexto.CRE_Credito.AsEnumerable()
                join cli in _contexto.CLI_Cliente on
                    new {CP = (int) cre.Id_Proveedor, CB = (int) cre.Id_Branch, CC = (int) cre.IdCliente} equals
                    new {CP = cli.Id_Proveedor, CB = cli.Id_Branch, CC = cli.IdCliente}
                join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                from catPb in pb.DefaultIfEmpty()
                join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                join catR in _contexto.CAT_REGION on (cre.Id_Branch == 0 ? catP.Cve_Region : catPb.Cve_Region) equals
                    catR.Cve_Region
                join catZ in _contexto.CAT_ZONA on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals
                    catZ.Cve_Zona
                where cre.RPU == rpu && cre.Cve_Estatus_Credito == 10

                select new MotivosCredito
                {
                    NoIntentos = "0",
                    NombreRazonSocial = cli.Razon_Social ?? cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
                    Causa = "Consultas crediticias",
                    Motivo = "MOP no valido",
                    Datos = "",
                    Fecha = (DateTime) cre.Fecha_Calificación_MOP_no_válida,
                    Region = catR.Dx_Nombre_Region,
                    Zona = catZ.Dx_Nombre_Zona,
                    Distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
                    IdTrama = cre.Id_Trama.ToString()
                };

            return query.ToList();
        }

        public List<MotivosCredito> ExcedeConsultasCrediticias(string rpu)
        {
            var query = from k in _contexto.K_LOG.AsEnumerable()
                //join p in _contexto.CAT_TIPO_PROCESOS on k.IDTIPOPROCESO equals p.IDTIPOPROCESO
                join c in _contexto.CAT_TAREAS_PROCESOS on k.IDTAREA equals c.IDTAREA
                join r in _contexto.CAT_REGION on k.IDREGION equals r.Cve_Region
                join z in _contexto.CAT_ZONA on k.IDZONA equals z.Cve_Zona
                where k.IDTIPOPROCESO == 4 && k.IDTAREA == 88 && k.TAREA_LOTE_NOCRED == rpu
                select new MotivosCredito
                {
                    NoIntentos = "",
                    NombreRazonSocial = "N/A",
                    Causa = "Consultas crediticias",
                    Motivo = c.DESCRIPCION,
                    Datos = NumeroConsultasCrediticias(rpu) + "",
                    Fecha = k.FECHA_ADICION,
                    Region = r.Dx_Nombre_Region,
                    Zona = z.Dx_Nombre_Zona,
                    Distribuidor = "N/A",
                    IdTrama = "N/A"
                };

            return query.ToList();
        }

        private int NumeroConsultasCrediticias(string rpu)
        {

            var getrfc = (from cre in _contexto.CRE_Credito
                join cli in _contexto.CLI_Cliente on
                    new {CP = (int) cre.Id_Proveedor, CB = (int) cre.Id_Branch, CC = (int) cre.IdCliente} equals
                    new {CP = (int) cli.Id_Proveedor, CB = (int) cli.Id_Branch, CC = (int) cli.IdCliente}
                where cre.RPU == rpu

                select new DatosConsulta
                {
                    RFC = cli.RFC,
                    RPU = cre.RPU,
                    No_Credito = cre.No_Credito,
                }).ToList();

            string _rpu = getrfc[0].RPU, rfc = getrfc[0].RFC;

            var consultasCredi = (from cre in _contexto.CRE_Credito
                join cli in _contexto.CLI_Cliente on
                    new {CP = (int) cre.Id_Proveedor, CB = (int) cre.Id_Branch, CC = (int) cre.IdCliente} equals
                    new {CP = (int) cli.Id_Proveedor, CB = (int) cli.Id_Branch, CC = (int) cli.IdCliente}
                where cre.RPU == _rpu && cli.RFC == rfc

                select new DatosConsulta
                {
                    RFC = cli.RFC,
                    RPU = cre.RPU,
                    No_Credito = cre.No_Credito,
                    no_consultasCrediticias =
                        (int) cre.No_Consultas_Crediticias == null ? 0 : (int) cre.No_Consultas_Crediticias
                }).ToList();

            int numCon = 0;
            foreach (var item in consultasCredi)
            {
                numCon += item.no_consultasCrediticias;
            }
            return numCon;
        }

        public List<MotivosCredito> Consulta2(string rpu)
        {
            var query = from k in _contexto.K_LOG.AsEnumerable()
                        join p in _contexto.CAT_TIPO_PROCESOS on k.IDTIPOPROCESO equals p.IDTIPOPROCESO
                        join c in _contexto.CAT_TAREAS_PROCESOS on k.IDTAREA equals c.IDTAREA
                        join z in _contexto.CAT_ZONA on k.IDZONA equals z.Cve_Zona
                        join r in _contexto.CAT_REGION on z.Cve_Region equals r.Cve_Region
                        where
                            (k.IDTIPOPROCESO == 10 || k.IDTIPOPROCESO == 11 || (k.IDTIPOPROCESO == 4 && k.IDTAREA == 88)) &&
                            k.TAREA_LOTE_NOCRED == rpu
                        select new MotivosCredito
                        {
                            NoIntentos = "",
                            NombreRazonSocial = "N/A",
                            Causa = p.IDTIPOPROCESO == 10 || p.IDTIPOPROCESO == 11
                                ? p.DESCRIPCION
                                : p.IDTIPOPROCESO == 4 && c.IDTAREA == 8
                                    ? "Consultas crediticias"
                                    : "",
                            Motivo = c.DESCRIPCION,
                            Datos = p.IDTIPOPROCESO == 10 || p.IDTIPOPROCESO == 11
                                ? k.MOTIVO
                                : p.IDTIPOPROCESO == 4 && c.IDTAREA == 8
                                    ? NumeroConsultasCrediticias(rpu) + ""
                                    : "",
                            Fecha = k.FECHA_ADICION,
                            Region = r.Dx_Nombre_Region,
                            Zona = z.Dx_Nombre_Zona,
                            Distribuidor = "N/A",
                            IdTrama = "N/A",
                            Secuencia_E_Alta = k.Secuencia_E_Alta.HasValue ? (int)k.Secuencia_E_Alta : 0,
                            Secuencia_E_Baja = k.Secuencia_E_Baja.HasValue ? (int)k.Secuencia_E_Baja : 0
                        };

            var query2 = from cre in _contexto.CRE_Credito.AsEnumerable()
                join cr in _contexto.CANCELAR_RECHAZAR on cre.No_Credito equals cr.No_Credito into crecr
                from cr in crecr.DefaultIfEmpty()
                join cli in _contexto.CLI_Cliente on
                    new {CP = (int) cre.Id_Proveedor, CB = (int) cre.Id_Branch, CC = (int) cre.IdCliente} equals
                    new {CP = cli.Id_Proveedor, CB = cli.Id_Branch, CC = cli.IdCliente}
                join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
                from catPb in pb.DefaultIfEmpty()
                join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
                join catR in _contexto.CAT_REGION on (cre.Id_Branch == 0 ? catP.Cve_Region : catPb.Cve_Region) equals
                    catR.Cve_Region
                join catZ in _contexto.CAT_ZONA on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals
                    catZ.Cve_Zona
                join mot in _contexto.MOTIVOS_RECHAZOS_CANCELACIONES on cr != null ? cr.ID_MOTIVO : 0 equals
                    mot.ID_MOTIVO into m
                from mot in m.DefaultIfEmpty()
                where cre.RPU == rpu && (cre.Cve_Estatus_Credito == 10 || cr != null )

                select new MotivosCredito
                {
                    NoIntentos = "",
                    NombreRazonSocial = cli.Razon_Social ?? cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
                    Causa = cre.Cve_Estatus_Credito == 10 ? "Consultas crediticias" : "Cancelaciones",
                    Motivo = cre.Cve_Estatus_Credito == 10 ? "MOP no valido" : mot.DESCRIPCION,
                    Datos = cre.Cve_Estatus_Credito == 10 ? "" : cre.No_Credito,
                    Fecha =
                        cre.Cve_Estatus_Credito == 10
                            ? (DateTime) cre.Fecha_Calificación_MOP_no_válida
                            : cr.FECHA_ADICION,
                    Region = catR.Dx_Nombre_Region,
                    Zona = catZ.Dx_Nombre_Zona,
                    Distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
                    IdTrama = cre.Id_Trama.ToString()
                };

            //var query3 = from cre in _contexto.CRE_Credito.AsEnumerable()
            //             join cr in _contexto.CANCELAR_RECHAZAR on cre.No_Credito equals cr.No_Credito
            //             join cli in _contexto.CLI_Cliente on
            //                 new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals
            //                 new { CP = cli.Id_Proveedor, CB = cli.Id_Branch, CC = cli.IdCliente }
            //             join mot in _contexto.MOTIVOS_RECHAZOS_CANCELACIONES on cr.ID_MOTIVO equals mot.ID_MOTIVO
            //             join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
            //             from catPb in pb.DefaultIfEmpty()
            //             join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
            //             join catR in _contexto.CAT_REGION on (cre.Id_Branch == 0 ? catP.Cve_Region : catPb.Cve_Region) equals
            //                 catR.Cve_Region
            //             join catZ in _contexto.CAT_ZONA on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals
            //                 catZ.Cve_Zona

            //             where cre.RPU == rpu
            //             select new MotivosCredito
            //             {
            //                 NoIntentos = "0",
            //                 NombreRazonSocial = cli.Razon_Social ?? cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
            //                 Causa = "Cancelaciones",
            //                 Motivo = mot.DESCRIPCION,
            //                 Datos = cre.No_Credito,
            //                 Fecha = cr.FECHA_ADICION,
            //                 Region = catR.Dx_Nombre_Region,
            //                 Zona = catZ.Dx_Nombre_Zona,
            //                 Distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
            //                 IdTrama = cre.Id_Trama.ToString()
            //             };

            return query.Concat(query2).ToList(); //Concat(query2).Concat(query3).ToList();
        }

        public List<MotivosCredito> Consulta(string rpu)
        {
            var query = from k in _contexto.K_LOG.AsEnumerable()
                join p in _contexto.CAT_TIPO_PROCESOS on k.IDTIPOPROCESO equals p.IDTIPOPROCESO
                join c in _contexto.CAT_TAREAS_PROCESOS on k.IDTAREA equals c.IDTAREA
                join z in _contexto.CAT_ZONA on k.IDZONA equals z.Cve_Zona
                join r in _contexto.CAT_REGION on z.Cve_Region equals r.Cve_Region
                where
                    (k.IDTIPOPROCESO == 10 || k.IDTIPOPROCESO == 11 || (k.IDTIPOPROCESO == 4 && k.IDTAREA == 88)) &&
                    k.TAREA_LOTE_NOCRED == rpu
                select new MotivosCredito
                {
                    NoIntentos = "",
                    NombreRazonSocial = "N/A",
                    Causa = p.IDTIPOPROCESO == 10 || p.IDTIPOPROCESO == 11
                        ? p.DESCRIPCION
                        : p.IDTIPOPROCESO == 4 && c.IDTAREA == 8
                            ? "Consultas crediticias"
                            : "",
                    Motivo = c.DESCRIPCION,
                    Datos = p.IDTIPOPROCESO == 10 || p.IDTIPOPROCESO == 11
                        ? k.MOTIVO
                        : p.IDTIPOPROCESO == 4 && c.IDTAREA == 8
                            ? NumeroConsultasCrediticias(rpu) + ""
                            : "",
                    Fecha = k.FECHA_ADICION,
                    Region = r.Dx_Nombre_Region,
                    Zona = z.Dx_Nombre_Zona,
                    Distribuidor = "N/A",
                    IdTrama = "N/A",
                    Secuencia_E_Alta = k.Secuencia_E_Alta.HasValue?(int)k.Secuencia_E_Alta:0,
                    Secuencia_E_Baja = k.Secuencia_E_Baja.HasValue?(int)k.Secuencia_E_Baja:0
                };

            //var query2 = from cre in _contexto.CRE_Credito.AsEnumerable()
            //            join cli in _contexto.CLI_Cliente on
            //                new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals
            //                new { CP = cli.Id_Proveedor, CB = cli.Id_Branch, CC = cli.IdCliente }
            //            join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
            //            from catPb in pb.DefaultIfEmpty()
            //            join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
            //            join catR in _contexto.CAT_REGION on (cre.Id_Branch == 0 ? catP.Cve_Region : catPb.Cve_Region) equals
            //                catR.Cve_Region
            //            join catZ in _contexto.CAT_ZONA on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals
            //                catZ.Cve_Zona
            //            where cre.RPU == rpu && cre.Cve_Estatus_Credito == 10

            //            select new MotivosCredito
            //            {
            //                NoIntentos = "",
            //                NombreRazonSocial = cli.Razon_Social ?? cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
            //                Causa = "Consultas crediticias",
            //                Motivo = "MOP no valido",
            //                Datos = "",
            //                Fecha = (DateTime)cre.Fecha_Calificación_MOP_no_válida,
            //                Region = catR.Dx_Nombre_Region,
            //                Zona = catZ.Dx_Nombre_Zona,
            //                Distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
            //                IdTrama = cre.Id_Trama.ToString()
            //            };

            //var query3 = from cre in _contexto.CRE_Credito.AsEnumerable()
            //            join cr in _contexto.CANCELAR_RECHAZAR on cre.No_Credito equals cr.No_Credito
            //            join cli in _contexto.CLI_Cliente on
            //                new { CP = (int)cre.Id_Proveedor, CB = (int)cre.Id_Branch, CC = (int)cre.IdCliente } equals
            //                new { CP = cli.Id_Proveedor, CB = cli.Id_Branch, CC = cli.IdCliente }
            //            join mot in _contexto.MOTIVOS_RECHAZOS_CANCELACIONES on cr.ID_MOTIVO equals mot.ID_MOTIVO
            //            join catPb in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals catPb.Id_Branch into pb
            //            from catPb in pb.DefaultIfEmpty()
            //            join catP in _contexto.CAT_PROVEEDOR on cre.Id_Proveedor equals catP.Id_Proveedor
            //            join catR in _contexto.CAT_REGION on (cre.Id_Branch == 0 ? catP.Cve_Region : catPb.Cve_Region) equals
            //                catR.Cve_Region
            //            join catZ in _contexto.CAT_ZONA on (cre.Id_Branch == 0 ? catP.Cve_Zona : catPb.Cve_Zona) equals
            //                catZ.Cve_Zona

            //            where cre.RPU == rpu
            //            select new MotivosCredito
            //            {
            //                NoIntentos = "0",
            //                NombreRazonSocial = cli.Razon_Social ?? cli.Nombre + " " + cli.Ap_Paterno + " " + cli.Ap_Materno,
            //                Causa = "Cancelaciones",
            //                Motivo = mot.DESCRIPCION,
            //                Datos = cre.No_Credito,
            //                Fecha = cr.FECHA_ADICION,
            //                Region = catR.Dx_Nombre_Region,
            //                Zona = catZ.Dx_Nombre_Zona,
            //                Distribuidor = cre.Id_Branch == 0 ? catP.Dx_Nombre_Comercial : catPb.Dx_Nombre_Comercial,
            //                IdTrama = cre.Id_Trama.ToString()
            //            };

            return query.ToList(); //Concat(query2).Concat(query3).ToList();
        }

        public List<EquiposBaja> ObtenEquiposBajaEficienciaCredito(string rpu, int secuencia , string noIntento)
        {
            var resultado = (from creditoSustitucion in _contexto.EQUIPO_BAJA_NP.AsEnumerable()                            
                             join t in _contexto.CAT_TECNOLOGIA
                                 on creditoSustitucion.Cve_Tecnologia
                                 equals t.Cve_Tecnologia
                                 
                             where creditoSustitucion.No_RPU == rpu && creditoSustitucion.Secuencia_Intento==secuencia
                             
                             select new EquiposBaja
                             {
                                 NoIntentos = noIntento,
                                 ID = creditoSustitucion.Id_Sustitucion,
                                 Dx_Tecnologia = t.Dx_Nombre_General,
                                 Dx_Grupo = creditoSustitucion.Grupo,
                                 Dx_Tipo_Producto = creditoSustitucion.Dx_Modelo_Producto ?? "",
                                 Dx_Consumo = creditoSustitucion.CapacidadSistema,
                                 Cantidad = 0,//(int)creditoSustitucion.No_Unidades!=null?creditoSustitucion.No_Unidades:0,
                                 Secuencia_E_Baja = (int)creditoSustitucion.Secuencia_Intento
                             }
                            ).ToList();
            return resultado;

        }

        public List<EquiposAlta> ObtenEquiposAltaEficienciaCredito(string rpu,int secuencia, string noIntento )
        {
            var resultado = (from creditoProducto in _contexto.EQUIPOS_ALTA_NP.AsEnumerable()
                             join c in _contexto.CRE_Credito
                                 on creditoProducto.No_RPU
                                 equals c.RPU
                             join d in _contexto.K_PROVEEDOR_PRODUCTO
                                 on c.Id_Proveedor
                                 equals d.Id_Proveedor
                             join p in _contexto.CAT_PRODUCTO
                                 on creditoProducto.Cve_Producto
                                 equals p.Cve_Producto
                             join t in _contexto.CAT_TECNOLOGIA
                                 on p.Cve_Tecnologia
                                 equals t.Cve_Tecnologia
                             join m in _contexto.CAT_MARCA
                                 on p.Cve_Marca
                                 equals m.Cve_Marca
                             where 
                                 creditoProducto.No_RPU == rpu &&
                                 creditoProducto.Cve_Producto == d.Cve_Producto
                                 && d.Mt_Precio_Unitario <= p.Mt_Precio_Max
                                 && creditoProducto.Secuencia_Intento==secuencia
                             select new EquiposAlta
                             {
                                 NoIntentos = noIntento,
                                 ID = creditoProducto.ID_ALTA,
                                 Producto = p.Dx_Nombre_Producto,
                                 Dx_Marca = m.Dx_Marca,
                                 Dx_Modelo = p.Dx_Modelo_Producto,
                                 Dx_Sistema = creditoProducto.CapacidadSistema,
                                 Cantidad = (int)creditoProducto.No_Cantidad,
                                 Importe_Total_Sin_IVA =
                                     (decimal)creditoProducto.Mt_Precio_Unitario_Sin_IVA *
                                     (int)creditoProducto.No_Cantidad, //(decimal)creditoProducto.Mt_Total,
                                 Gasto_Instalacion = (decimal)creditoProducto.Mt_Gastos_Instalacion_Mano_Obra,
                                 Secuencia_E_Alta = (int)creditoProducto.Secuencia_Intento
                             }
                            ).ToList();

            return resultado;
        }

    }
}

