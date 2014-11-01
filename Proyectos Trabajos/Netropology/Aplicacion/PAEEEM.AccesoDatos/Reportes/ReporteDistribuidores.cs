using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Reportes;

namespace PAEEEM.AccesoDatos.Reportes
{
    public class ReporteDistribuidores
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        #region Consultas Reportes y Graficas

        public List<Distribuidor> ObtenDistribuidores(DateTime fechaInicio, DateTime fechaFin, int idRegion)
        {
            List<Distribuidor> lstDistribuidores = null;

            if (idRegion != 0)
            {
                lstDistribuidores = (from branch in _contexto.CAT_PROVEEDORBRANCH
                                     join proveedor in _contexto.CAT_PROVEEDOR
                                         on branch.Id_Proveedor
                                         equals proveedor.Id_Proveedor
                                     join estadoFiscal in _contexto.CAT_ESTADO
                                         on proveedor.Cve_Estado_Fisc
                                         equals estadoFiscal.Cve_Estado
                                     join municipioFiscal in _contexto.CAT_DELEG_MUNICIPIO
                                         on proveedor.Cve_Deleg_Municipio_Fisc
                                         equals municipioFiscal.Cve_Deleg_Municipio
                                     join estadoFisico in _contexto.CAT_ESTADO
                                         on branch.Cve_Estado_Part
                                         equals estadoFisico.Cve_Estado
                                     join municipioFisico in _contexto.CAT_DELEG_MUNICIPIO
                                         on branch.Cve_Deleg_Municipio_Fisc
                                         equals municipioFisico.Cve_Deleg_Municipio
                                     join region in _contexto.CAT_REGION
                                         on branch.Cve_Region
                                         equals region.Cve_Region
                                     join zona in _contexto.CAT_ZONA
                                         on branch.Cve_Zona
                                         equals zona.Cve_Zona
                                     join estatus in _contexto.CAT_ESTATUS_PROVEEDOR
                                         on branch.Cve_Estatus_Proveedor
                                         equals estatus.Cve_Estatus_Proveedor
                                     where branch.Dt_Fecha_Creacion >= fechaInicio
                                           && branch.Dt_Fecha_Creacion <= fechaFin
                                           && branch.Cve_Region == idRegion
                                     select new Distribuidor
                                         {
                                             IdMatriz = proveedor.Id_Proveedor,
                                             IdSucursal = branch.Id_Branch,
                                             Rfc = proveedor.Dx_RFC,
                                             RazonSocial = proveedor.Dx_Razon_Social,
                                             NombreComercial = proveedor.Dx_Nombre_Comercial,
                                             IdIntelisis = proveedor.ID_ProveedorIntelisis,
                                             IvaDistribuidor = branch.Pct_Tasa_IVA,
                                             CpFiscal = proveedor.Dx_Domicilio_Fiscal_CP,
                                             EstadoFiscal = estadoFiscal.Dx_Nombre_Estado,
                                             MunicipioFiscal = municipioFiscal.Dx_Deleg_Municipio,
                                             CalleFiscal = proveedor.Dx_Domicilio_Fiscal_Calle,
                                             NumeroFiscal = proveedor.Dx_Domicilio_Fiscal_Num,
                                             CpFisico = branch.Dx_Domicilio_Part_CP,
                                             IdEstado = estadoFisico.Cve_Estado,
                                             EstadoFisico = estadoFisico.Dx_Nombre_Estado,
                                             IdMunicipio = municipioFisico.Cve_Deleg_Municipio,
                                             MunicipioFisico = municipioFisico.Dx_Deleg_Municipio,
                                             CalleFisico = branch.Dx_Domicilio_Part_Calle,
                                             NumeroFisico = branch.Dx_Domicilio_Part_Num,
                                             RepresentanteLegal = proveedor.Dx_Nombre_Repre_Legal,
                                             NombreContacto = branch.Dx_Nombre_Repre,
                                             EmailContacto = branch.Dx_Email_Repre,
                                             TelefonoContacto = branch.Dx_Telefono_Repre,
                                             IdRegion = region.Cve_Region,
                                             Region = region.Dx_Nombre_Region,
                                             IdZona = zona.Cve_Zona,
                                             Zona = zona.Dx_Nombre_Zona,
                                             IdEstatus = estatus.Cve_Estatus_Proveedor,
                                             Estatus = estatus.Dx_Estatus_Proveedor,
                                             FechaAlta = (DateTime) branch.Dt_Fecha_Creacion,
                                             FechaUltimoEstatus = (DateTime) branch.Dt_Fecha_Branch,
                                             RefrigeracionComercial = "No",
                                             AireAcondicionado = "No",
                                             IluminacionLineal = "No",
                                             MotoresElectricos = "No",
                                             Subestaciones = "No",
                                             IluminacionLed = "No",
                                             BancoCapaciores = "No",
                                             IluminacionInduccion = "No",
                                             CamarasRefrigeracion = "No",
                                             CalentadoresSolares = "No"
                                         }).ToList();
            }
            else
            {
                lstDistribuidores = (from branch in _contexto.CAT_PROVEEDORBRANCH
                                     join proveedor in _contexto.CAT_PROVEEDOR
                                         on branch.Id_Proveedor
                                         equals proveedor.Id_Proveedor
                                     join estadoFiscal in _contexto.CAT_ESTADO
                                         on proveedor.Cve_Estado_Fisc
                                         equals estadoFiscal.Cve_Estado
                                     join municipioFiscal in _contexto.CAT_DELEG_MUNICIPIO
                                         on proveedor.Cve_Deleg_Municipio_Fisc
                                         equals municipioFiscal.Cve_Deleg_Municipio
                                     join estadoFisico in _contexto.CAT_ESTADO
                                         on branch.Cve_Estado_Part
                                         equals estadoFisico.Cve_Estado
                                     join municipioFisico in _contexto.CAT_DELEG_MUNICIPIO
                                         on branch.Cve_Deleg_Municipio_Fisc
                                         equals municipioFisico.Cve_Deleg_Municipio
                                     join region in _contexto.CAT_REGION
                                         on branch.Cve_Region
                                         equals region.Cve_Region
                                     join zona in _contexto.CAT_ZONA
                                         on branch.Cve_Zona
                                         equals zona.Cve_Zona
                                     join estatus in _contexto.CAT_ESTATUS_PROVEEDOR
                                         on branch.Cve_Estatus_Proveedor
                                         equals estatus.Cve_Estatus_Proveedor
                                     where branch.Dt_Fecha_Creacion >= fechaInicio
                                           && branch.Dt_Fecha_Creacion <= fechaFin
                                     select new Distribuidor
                                     {
                                         IdMatriz = proveedor.Id_Proveedor,
                                         IdSucursal = branch.Id_Branch,
                                         Rfc = proveedor.Dx_RFC,
                                         RazonSocial = proveedor.Dx_Razon_Social,
                                         NombreComercial = proveedor.Dx_Nombre_Comercial,
                                         IdIntelisis = proveedor.ID_ProveedorIntelisis,
                                         IvaDistribuidor = branch.Pct_Tasa_IVA,
                                         CpFiscal = proveedor.Dx_Domicilio_Fiscal_CP,
                                         EstadoFiscal = estadoFiscal.Dx_Nombre_Estado,
                                         MunicipioFiscal = municipioFiscal.Dx_Deleg_Municipio,
                                         CalleFiscal = proveedor.Dx_Domicilio_Fiscal_Calle,
                                         NumeroFiscal = proveedor.Dx_Domicilio_Fiscal_Num,
                                         CpFisico = branch.Dx_Domicilio_Part_CP,
                                         EstadoFisico = estadoFisico.Dx_Nombre_Estado,
                                         MunicipioFisico = municipioFisico.Dx_Deleg_Municipio,
                                         CalleFisico = branch.Dx_Domicilio_Part_Calle,
                                         NumeroFisico = branch.Dx_Domicilio_Part_Num,
                                         RepresentanteLegal = proveedor.Dx_Nombre_Repre_Legal,
                                         NombreContacto = branch.Dx_Nombre_Repre,
                                         EmailContacto = branch.Dx_Email_Repre,
                                         TelefonoContacto = branch.Dx_Telefono_Repre,
                                         Region = region.Dx_Nombre_Region,
                                         Zona = zona.Dx_Nombre_Zona,
                                         Estatus = estatus.Dx_Estatus_Proveedor,
                                         FechaAlta = (DateTime)branch.Dt_Fecha_Creacion,
                                         FechaUltimoEstatus = (DateTime)branch.Dt_Fecha_Branch,
                                         RefrigeracionComercial = "No",
                                         AireAcondicionado = "No",
                                         IluminacionLineal = "No",
                                         MotoresElectricos = "No",
                                         Subestaciones = "No",
                                         IluminacionLed = "No",
                                         BancoCapaciores = "No",
                                         IluminacionInduccion = "No",
                                         CamarasRefrigeracion = "No",
                                         CalentadoresSolares = "No"
                                     }).ToList();
            }

            return lstDistribuidores;
        }

        public List<CAT_TECNOLOGIA> ObtenTecnologiasProveedor(int idProveedor)
        {
            List<CAT_TECNOLOGIA> lstTecnologias = null;

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                lstTecnologias = (from tecnologias in _contexto.CAT_TECNOLOGIA
                              join p in _contexto.CAT_PRODUCTO
                                on tecnologias.Cve_Tecnologia equals p.Cve_Tecnologia
                              join k2 in _contexto.K_PROVEEDOR_PRODUCTO
                                on p.Cve_Producto equals k2.Cve_Producto
                              where k2.Id_Proveedor == idProveedor
                              select tecnologias).Distinct().ToList<CAT_TECNOLOGIA>();
            }

            return lstTecnologias;
        }

        public List<RegionDistribuidor> ObtenDistribuidoresXregion()
        {
            var lstRegionDist = (from branch in _contexto.CAT_PROVEEDORBRANCH
                                 join region in _contexto.CAT_REGION
                                     on branch.Cve_Region equals region.Cve_Region
                                 where branch.Cve_Estatus_Proveedor == 2
                                 group branch by new  {branch.Cve_Region, branch.CAT_REGION.Dx_Nombre_Region}
                                 into grp
                                 select new RegionDistribuidor
                                     {
                                         IdRegionZona = (int)grp.Key.Cve_Region,
                                         NombreRegionZona = grp.Key.Dx_Nombre_Region,
                                         NoDistribuidores = grp.Select(me => me.Cve_Region).Count()
                                     }).ToList();

            return lstRegionDist;
        }

        public List<RegionDistribuidor> ObtenDistribuidoresXzona(int idRegion)
        {
            var lstRegionDist = (from branch in _contexto.CAT_PROVEEDORBRANCH
                                 join zona in _contexto.CAT_ZONA
                                     on branch.Cve_Zona equals zona.Cve_Zona
                                 where branch.Cve_Estatus_Proveedor == 2 && branch.Cve_Region == idRegion
                                 group branch by new { branch.Cve_Zona, branch.CAT_ZONA.Dx_Nombre_Zona }
                                     into grp
                                     select new RegionDistribuidor
                                     {
                                         IdRegionZona = (int)grp.Key.Cve_Zona,
                                         NombreRegionZona = grp.Key.Dx_Nombre_Zona,
                                         NoDistribuidores = grp.Select(me => me.Cve_Zona).Count()
                                     }).ToList();

            return lstRegionDist;
        }

        public List<DISTRIBUIDORES_ACTIVOS> ObtenHistDistribuidoresActivos()
        {
            List<DISTRIBUIDORES_ACTIVOS> lstDistActivos = null;

            using (var r = new Repositorio<DISTRIBUIDORES_ACTIVOS>())
            {
                lstDistActivos = r.Filtro(me => me.Estatus == true);
            }

            return lstDistActivos;
        }

        #endregion

        #region Catalogos de Busqueda

        public List<CAT_ESTATUS_PROVEEDOR> ObtenEstatusProveedor()
        {
            using (var r = new Repositorio<CAT_ESTATUS_PROVEEDOR>())
            {
                return r.Filtro(me => me.Cve_Estatus_Proveedor > 0);
            }
        }

        public List<CAT_REGION> ObtenCatalogoRegion()
        {
            using (var r = new Repositorio<CAT_REGION>())
            {
                return r.Filtro(me => me.Cve_Region > 0);
            }
        }

        public List<CAT_ZONA> ObtenCatalogoZona(int idRegion)
        {
            using (var r = new Repositorio<CAT_ZONA>())
            {
                return r.Filtro(me => me.Cve_Region == idRegion);
            }
        }

        public List<CAT_TECNOLOGIA> ObtenCatalogoTecnologia()
        {
            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                return r.Filtro(me => me.Cve_Tecnologia > 0);
            }
        }

        public List<CAT_PROVEEDOR> ObtenCatalogoMatriz()
        {
            using (var r = new Repositorio<CAT_PROVEEDOR>())
            {
                return r.Filtro(me => me.Cve_Estatus_Proveedor == 2);
            }
        }

        public List<CAT_PROVEEDORBRANCH> ObtenCatalogoSucursales(int idProveedor)
        {
            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                return r.Filtro(me => me.Id_Proveedor == idProveedor && me.Cve_Estatus_Proveedor == 2);
            }
        }

        public CAT_ZONA ObtenZona(int idZona)
        {
            using (var r = new Repositorio<CAT_ZONA>())
            {
                return r.Extraer(me => me.Cve_Zona == idZona);
            }
        }

        #endregion
    }
}
