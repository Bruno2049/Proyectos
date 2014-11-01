using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.CAyD;

namespace PAEEEM.AccesoDatos.CayD
{
    public class ReasignarSolicitudes
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public List<CAT_CENTRO_DISP_SUCURSAL> CAyDs()
        {
            var centros = from c in _contexto.CAT_CENTRO_DISP_SUCURSAL
                          where c.Cve_Estatus_Centro_Disp == 2
                          select c;
            return centros.ToList();
        }

        public List<CAT_PROVEEDORBRANCH> Distribuidores()
        {
            var pro = from p in _contexto.CAT_PROVEEDORBRANCH
                      where p.Cve_Estatus_Proveedor == 2
                      select p;
            return pro.ToList();
        }

        public bool ExisteFechaRecepcion(string solicitud)
        {
            K_CREDITO_SUSTITUCION relacion = null;

            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                relacion = r.Extraer(me => me.No_Credito == solicitud);
            }

            return relacion.Dt_Fecha_Recepcion != null;
        }

        public List<DatosReasignar> ObtenerSolicitudes(int RS, int CAyD,string solicitud,string folio)
        {
            var query = from cre in _contexto.CRE_Credito
                        join k in _contexto.K_CREDITO_SUSTITUCION on cre.No_Credito equals k.No_Credito
                        join pro in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals pro.Id_Branch
                        //join ccd in _contexto.CAT_CENTRO_DISP on k.Id_Centro_Disp equals ccd.Id_Centro_Disp into leftccd
                        //from ccd in leftccd.DefaultIfEmpty()
                        join ccds in _contexto.CAT_CENTRO_DISP_SUCURSAL on k.Id_Centro_Disp equals ccds.Id_Centro_Disp_Sucursal //into leftccds
                        //from ccds in leftccds.DefaultIfEmpty()
                        where (cre.Cve_Estatus_Credito == 1 || cre.Cve_Estatus_Credito == 2)
                               && (k.Dt_Fecha_Recepcion == null)
                               && (k.Fg_Tipo_Centro_Disp != null)
                               && (RS == 0 ? 1 == 1 : pro.Id_Branch == RS)
                               && (CAyD == 0 ? 1 == 1 : /*k.Fg_Tipo_Centro_Disp == "M" ? ccd.Id_Centro_Disp == CAyD :*/ ccds.Id_Centro_Disp_Sucursal == CAyD)
                               && (solicitud == "" ? 1 == 1 : solicitud == cre.No_Credito)
                               && (folio == "" ? 1 == 1 : folio == k.Id_Pre_Folio)
                        select new DatosReasignar
                        {
                            NoSolicitud = cre.No_Credito,
                            DistrNC = pro.Dx_Nombre_Comercial,
                            DistrRS = pro.Dx_Razon_Social,
                            Folio = k.Id_Pre_Folio,
                            CAyD = /*k.Fg_Tipo_Centro_Disp == "M" ? ccd.Dx_Nombre_Comercial :*/ ccds.Dx_Nombre_Comercial
                        };
            return query.OrderBy(me=>me.NoSolicitud).ToList();
        }

        //public List<DatosReasignar> ObtenerSolicitudes(int RS, int CAyD, string solicitud, string folio)
        //{
        //    var query = from cre in _contexto.CRE_Credito
        //                join k in _contexto.K_CREDITO_SUSTITUCION on cre.No_Credito equals k.No_Credito
        //                join pro in _contexto.CAT_PROVEEDORBRANCH on cre.Id_Branch equals pro.Id_Branch
        //                join ccd in _contexto.CAT_CENTRO_DISP on k.Id_Centro_Disp equals ccd.Id_Centro_Disp into leftccd
        //                from ccd in leftccd.DefaultIfEmpty()
        //                join ccds in _contexto.CAT_CENTRO_DISP_SUCURSAL on k.Id_Centro_Disp equals ccds.Id_Centro_Disp_Sucursal into leftccds
        //                from ccds in leftccds.DefaultIfEmpty()
        //                where (cre.Cve_Estatus_Credito == 1 || cre.Cve_Estatus_Credito == 2)
        //                       && (k.Dt_Fecha_Recepcion == null)
        //                       && (k.Fg_Tipo_Centro_Disp != null)
        //                       && (RS == 0 ? 1 == 1 : pro.Id_Branch == RS)
        //                       && (CAyD == 0 ? 1 == 1 : k.Fg_Tipo_Centro_Disp == "M" ? ccd.Id_Centro_Disp == CAyD : ccds.Id_Centro_Disp_Sucursal == CAyD)
        //                       && (solicitud == "" ? 1 == 1 : solicitud == cre.No_Credito)
        //                       && (folio == "" ? 1 == 1 : folio == k.Id_Pre_Folio)
        //                select new DatosReasignar
        //                {
        //                    NoSolicitud = cre.No_Credito,
        //                    DistrNC = pro.Dx_Nombre_Comercial,
        //                    DistrRS = pro.Dx_Razon_Social,
        //                    Folio = k.Id_Pre_Folio,
        //                    CAyD = k.Fg_Tipo_Centro_Disp == "M" ? ccd.Dx_Nombre_Comercial : ccds.Dx_Nombre_Comercial
        //                };
        //    return query.OrderBy(me => me.NoSolicitud).ToList();
        //}

        public bool ActualizarCAyD(K_CREDITO_SUSTITUCION informacion)
        {
            bool actualiza;

            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                actualiza = r.Actualizar(informacion);
            }

            return actualiza;
        }

        public K_CREDITO_SUSTITUCION ObtenerSolicitudByFolio(string folio)
        {
            K_CREDITO_SUSTITUCION solicitud = null;

            using (var r = new Repositorio<K_CREDITO_SUSTITUCION>())
            {
                solicitud = r.Extraer(me => me.Id_Pre_Folio == folio);
            }

            return solicitud;
        }
    }
}
