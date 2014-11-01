using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Vendedores;

namespace PAEEEM.AccesoDatos.Vendedores
{
    public class RegistrarVendedor
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public List<DatosVendedoresDist> ConsultarVendedoresInicial(int idbranch)
        {
            var query = from vendedor in _contexto.VENDEDORES
                join estatus in _contexto.ESTATUS_VENDEDORES on vendedor.Cve_Estatus_Vendedor equals
                    estatus.Cve_Estatus_Vendedor
                join tipoIdent in _contexto.TIPO_IDENTIFICACION_VENDEDORES on vendedor.ID_TIPO equals tipoIdent.ID_TIPO
                join relacion in _contexto.RELACION_VENDEDOR_DISTRIBUIDOR on vendedor.ID_VENDEDOR equals relacion.ID_VENDEDOR
                join anomalia in _contexto.ANOMALIAS_VENDEDORES on vendedor.ID_VENDEDOR equals anomalia.ID_VENDEDOR into
                    leftVen
                from anomalia in leftVen.DefaultIfEmpty()
                where (vendedor.Cve_Estatus_Vendedor == 0 || vendedor.Cve_Estatus_Vendedor == 1) && relacion.Id_Branch==idbranch
                select new DatosVendedoresDist
                {
                    IdVendedor = vendedor.ID_VENDEDOR,
                    Nombre = vendedor.NOMBRE + " " + vendedor.APELLIDO_PATERNO + " " + vendedor.APELLIDO_MATERNO,
                    Curp = vendedor.CURP,
                    FechaNacimiento = vendedor.FEC_NACIMINIENTO,
                    TipoIdentificacion = tipoIdent.DESCRIPCION,
                    NoIdentificacion = vendedor.NUMERO_IDENTIFICACION,
                    Estatus = estatus.Descripcion,
                    AccesoSistema = vendedor.ACCESO_SISTEMA ? "SI" : "NO",
                    Incidencia = anomalia.DESCRIPCION ?? "N/A",
                    Archivo = vendedor.FOTO_IDENTIFICACION
                };

            return query.ToList();
        }

        public List<DatosVendedoresDist> ConsultarVendedores(string curp, string nombre, int status, int idBranch)
        {
            var query = from vendedor in _contexto.VENDEDORES
                join estatus in _contexto.ESTATUS_VENDEDORES on vendedor.Cve_Estatus_Vendedor equals estatus.Cve_Estatus_Vendedor
                join tipoIdent in _contexto.TIPO_IDENTIFICACION_VENDEDORES on vendedor.ID_TIPO equals tipoIdent.ID_TIPO
                join relacion in _contexto.RELACION_VENDEDOR_DISTRIBUIDOR on vendedor.ID_VENDEDOR equals relacion.ID_VENDEDOR
                join anomalia in _contexto.ANOMALIAS_VENDEDORES on vendedor.ID_VENDEDOR equals anomalia.ID_VENDEDOR into leftVen
                from anomalia in leftVen.DefaultIfEmpty()
                where (curp == ""? 1 == 1 : vendedor.CURP == curp)
                      && (nombre == ""? 1 == 1: vendedor.NOMBRE == nombre)
                      && (status == 0? 1 == 1: vendedor.Cve_Estatus_Vendedor == status)
                      && relacion.Id_Branch==idBranch
                select new DatosVendedoresDist
                {
                    IdVendedor = vendedor.ID_VENDEDOR,
                    Nombre = vendedor.NOMBRE + " " + vendedor.APELLIDO_PATERNO + " " + vendedor.APELLIDO_MATERNO,
                    Curp = vendedor.CURP,
                    FechaNacimiento = vendedor.FEC_NACIMINIENTO,
                    TipoIdentificacion = tipoIdent.DESCRIPCION,
                    NoIdentificacion = vendedor.NUMERO_IDENTIFICACION,
                    Estatus = estatus.Descripcion,
                    AccesoSistema = vendedor.ACCESO_SISTEMA ? "SI" : "NO",
                    Incidencia = anomalia.DESCRIPCION ?? "N/A",
                    Archivo = vendedor.FOTO_IDENTIFICACION
                };

            return query.ToList();
        }

        public VENDEDORES GuardarVendedor(VENDEDORES informacion)
        {
            VENDEDORES newVendedor = null;

            using (var r = new Repositorio<VENDEDORES>())
            {
                r.Agregar(informacion);
                newVendedor = r.Extraer(me => me.CURP == informacion.CURP);
            }

            return newVendedor;
        }

        public RELACION_VENDEDOR_DISTRIBUIDOR GuardarRegistro(RELACION_VENDEDOR_DISTRIBUIDOR informacion)
        {
            RELACION_VENDEDOR_DISTRIBUIDOR newRegistro = null;

            using (var r = new Repositorio<RELACION_VENDEDOR_DISTRIBUIDOR>())
            {
                newRegistro = r.Agregar(informacion);
            }

            return newRegistro;
        }

        public VENDEDORES ObtenerVendedor(string curp)
        {
            VENDEDORES Vendedor = null;

            using (var r = new Repositorio<VENDEDORES>())
            {
                Vendedor = r.Extraer(me => me.CURP == curp);
            }

            return Vendedor;
        }

        public bool ObtenerRelacionVendedorDist(int idVendedor,int idBranch)
        {
            RELACION_VENDEDOR_DISTRIBUIDOR relacion = null;

            using (var r = new Repositorio<RELACION_VENDEDOR_DISTRIBUIDOR>())
            {
                relacion = r.Extraer(me => me.ID_VENDEDOR == idVendedor && me.Id_Branch==idBranch);
            }

            return relacion != null;
        }

        public List<TipoIdentificacion> ObtenerTipoIdentificacionVendedores()
        {
            var Vendedor = from tipo in _contexto.TIPO_IDENTIFICACION_VENDEDORES
                           select new TipoIdentificacion
                           {
                               ID_TIPO = tipo.ID_TIPO,
                               DESCRIPCION = tipo.DESCRIPCION
                           };

            return Vendedor.ToList();//ID_TIPO DESCRIPCION);
        }

        public List<ESTATUS_VENDEDORES> ObtenerEstatusVendedores()
        {
            var  estatus = from est in _contexto.ESTATUS_VENDEDORES
                select est;

            return estatus.ToList();
        }

        public ANOMALIAS_VENDEDORES ObtenerAnomalias(string curp)
        {
            var anomalias = from v in _contexto.VENDEDORES
                join an in _contexto.ANOMALIAS_VENDEDORES on v.ID_VENDEDOR equals an.ID_VENDEDOR
                where v.CURP==curp
                select an;
            
            return anomalias.FirstOrDefault();
        }
    }
}
