using System;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;
using PAEEEM.Entidades.RPUZona;

namespace PAEEEM.AccesoDatos.RPUZona
{
    public class ValidacionZonaA
    {
        private static readonly ValidacionZonaA _classInstance = new ValidacionZonaA();

        public static ValidacionZonaA ClassInstance
        {
            get { return _classInstance; }
        }

        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public US_USUARIO ExtraeUsuario(Expression<Func<US_USUARIO, bool>> nombreUsuario)
        {
            US_USUARIO verifica;

            using (var r = new Repositorio<US_USUARIO>())
            {
                verifica = r.Extraer(nombreUsuario);
            }

            var usuarioZona = (from UU in _contexto.US_USUARIO

                               join cp in _contexto.CAT_PROVEEDOR on UU.Id_Departamento equals cp.Id_Proveedor into t1
                               from cp in t1.DefaultIfEmpty()

                               join cpb in _contexto.CAT_PROVEEDORBRANCH on UU.Id_Departamento equals cpb.Id_Proveedor into t2
                               from cpb in t2.DefaultIfEmpty()

                               where UU.Nombre_Usuario == verifica.Nombre_Usuario

                               select new UsuarioDistZona
                               {
                                   Nombre_Usuario = UU.Nombre_Usuario,
                                   Id_Rol = UU.Id_Rol,
                                   Tipo_Usuarion = UU.Tipo_Usuario,
                                   Zona = (UU.Tipo_Usuario == "S") ? cp.Cve_Zona : cpb.Cve_Zona
                               }).ToList().FirstOrDefault();

            return verifica;
        }

        public UsuarioDistZona Trae_Usuario(string nombreUsuario)
        {
            var usuarioZona = (from uu in _contexto.US_USUARIO

                               join cp in _contexto.CAT_PROVEEDOR on uu.Id_Departamento equals cp.Id_Proveedor into t1
                               from cp in t1.DefaultIfEmpty()

                               join cpb in _contexto.CAT_PROVEEDORBRANCH on uu.Id_Departamento equals cpb.Id_Branch into t2
                               from cpb in t2.DefaultIfEmpty()

                               where uu.Nombre_Usuario == nombreUsuario

                               select new UsuarioDistZona
                               {
                                   Nombre_Usuario = uu.Nombre_Usuario,
                                   Id_Rol = uu.Id_Rol,
                                   Tipo_Usuarion = uu.Tipo_Usuario,
                                   Zona = (uu.Tipo_Usuario == "S") ? cp.Cve_Zona : cpb.Cve_Zona
                               }).ToList().FirstOrDefault();


            return usuarioZona;
        }

        public Region_Zona ObtenZonaRpur(string zona)
        {
            var obtenZona = (from rd in _contexto.ResponseData
                             join r in _contexto.Regionalizacion on rd.Zone equals r.CLAVE
                             join cz in _contexto.CAT_ZONA on r.Cve_Zona equals cz.Cve_Zona
                             join cr in _contexto.CAT_REGION on cz.Cve_Region equals cr.Cve_Region
                             where r.CLAVE == zona
                             select new Region_Zona
                             {
                                 cve_Region = cr.Cve_Region,
                                 Region = cr.Clave,
                                 Dx_Nombre_Region = cr.Dx_Nombre_Region,
                                 cve_zona = r.Cve_Zona,
                                 Dx_Nombre_Zona = cz.Dx_Nombre_Zona,
                                 Clave = r.CLAVE,
                                 REGION_CFE = r.REGION_CFE,
                                 ZONA_CFE = r.ZONA_CFE,
                             }).ToList().FirstOrDefault();

            return obtenZona;
        }


        public ZonaRPURD ObtenZonaCfe(string rpu)
        {
            var traeZonaRpurd = (from rd in _contexto.ResponseData
                                 where rd.ServiceCode == rpu
                                 select new ZonaRPURD
                                 {
                                     RPU = rd.ServiceCode,
                                     Zone = rd.Zone
                                 }).ToList().FirstOrDefault();
            return traeZonaRpurd;
        }
    }
}