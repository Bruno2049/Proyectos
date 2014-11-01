using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.Reportes;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Reportes;

namespace PAEEEM.LogicaNegocios.Reportes
{
    public class ReporteDistribuidoresBL
    {
        #region Consultas Reportes y Graficas

        public List<Distribuidor> ObtenDistribuidores(DateTime fechaInicio, DateTime fechaFin, int idRegion, int idZona, 
                                                        int idEstado, int idMunicipio, int idMatriz, int idSucursal, int idEstatus)
        {
            var lstDistribuidores = new ReporteDistribuidores().ObtenDistribuidores(fechaInicio, fechaFin, idRegion);

            if (lstDistribuidores.Count > 0)
            {
                if (idZona != 0)
                    lstDistribuidores = lstDistribuidores.FindAll(me => me.IdZona == idZona);
                
                if (idEstado != 0)
                {
                    lstDistribuidores = lstDistribuidores.FindAll(me => me.IdEstado == idEstado);

                    if (idMunicipio != 0)
                        lstDistribuidores = lstDistribuidores.FindAll(me => me.IdMunicipio == idMunicipio);
                }

                if (idMatriz != 0)
                {
                    lstDistribuidores = lstDistribuidores.FindAll(me => me.IdMatriz == idMatriz);

                    if (idSucursal != 0)
                        lstDistribuidores = lstDistribuidores.FindAll(me => me.IdSucursal == idSucursal);
                }

                if (idEstatus != 0)
                    lstDistribuidores = lstDistribuidores.FindAll(me => me.IdEstatus == idEstatus);

                foreach (var distribuidor in lstDistribuidores)
                {
                    var lstTecnologias = new ReporteDistribuidores().ObtenTecnologiasProveedor(distribuidor.IdMatriz);

                    if (lstTecnologias.Count > 0)
                    {
                        foreach (var catTecnologia in lstTecnologias)
                        {
                            switch (catTecnologia.Cve_Tecnologia)
                            {
                                case 1:
                                    distribuidor.RefrigeracionComercial = "Si";
                                    break;
                                case 2:
                                    distribuidor.AireAcondicionado = "Si";
                                    break;
                                case 3:
                                    distribuidor.IluminacionLineal = "Si";
                                    break;
                                case 4:
                                    distribuidor.MotoresElectricos = "Si";
                                    break;
                                case 5:
                                    distribuidor.Subestaciones = "Si";
                                    break;
                                case 6:
                                    distribuidor.IluminacionLed = "Si";
                                    break;
                                case 7:
                                    distribuidor.BancoCapaciores = "Si";
                                    break;
                                case 8:
                                    distribuidor.IluminacionInduccion = "Si";
                                    break;
                                case 9:
                                    distribuidor.CamarasRefrigeracion = "Si";
                                    break;
                            }
                        }
                    }
                }
            }

            return lstDistribuidores;
        }

        public List<RegionDistribuidor> ObtenDistribuidoresXregion()
        {
            var lstDistProveedores = new ReporteDistribuidores().ObtenDistribuidoresXregion();
            return lstDistProveedores;
        }

        public List<RegionDistribuidor> ObtenDistribuidoresXzona(int idRegion)
        {
            var lstDistProveedores = new ReporteDistribuidores().ObtenDistribuidoresXzona(idRegion);
            return lstDistProveedores;
        }

        public List<DISTRIBUIDORES_ACTIVOS> ObtenHistDistribuidoresActivos(int anioInicio, int mesInicio, int anioFin, int mesFin)
        {
            var lstDistActivos = new ReporteDistribuidores().ObtenHistDistribuidoresActivos();

            if (anioInicio != 0)
                lstDistActivos = lstDistActivos.FindAll(me => me.Anio >= anioInicio);

            if(anioFin != 0)
                lstDistActivos = lstDistActivos.FindAll(me => me.Anio <= anioFin);

            return lstDistActivos;
        }

        #endregion

        #region Catalogos

        public List<CAT_ESTATUS_PROVEEDOR> ObtenEstatusProveedor()
        {
            var lstEstatusProveedor = new ReporteDistribuidores().ObtenEstatusProveedor();
            return lstEstatusProveedor;
        }

        public List<CAT_REGION> ObtenCatalogoRegion()
        {
            var lstCatRegion = new ReporteDistribuidores().ObtenCatalogoRegion();
            return lstCatRegion;
        }

        public List<CAT_ZONA> ObtenCatalogoZona(int idRegion)
        {
            var lstCatZona = new ReporteDistribuidores().ObtenCatalogoZona(idRegion);
            return lstCatZona;
        }

        public List<CAT_TECNOLOGIA> ObtenCatalogoTecnologia()
        {
            var lstCatTecnologia = new ReporteDistribuidores().ObtenCatalogoTecnologia();
            return lstCatTecnologia;
        }

        public List<CAT_PROVEEDOR> ObtenCatalogoMatriz()
        {
            var lstCatMatriz = new ReporteDistribuidores().ObtenCatalogoMatriz();
            return lstCatMatriz;
        }

        public List<CAT_PROVEEDORBRANCH> ObtenCatalogoSucursales(int idProveedor)
        {
            var lstCatSucursales = new ReporteDistribuidores().ObtenCatalogoSucursales(idProveedor);
            return lstCatSucursales;
        }

        public CAT_ZONA ObtenZona(int idZona)
        {
            var zona = new ReporteDistribuidores().ObtenZona(idZona);
            return zona;
        }

        #endregion
    }
}
