using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.CreditosAutorizadosA;
using PAEEEM.Entidades;
using PAEEEM.Entidades.CreditosAutorizadosE;


namespace PAEEEM.LogicaNegocios.CreditosAutorizadosL
{
    public class CreditosAutorizadosL
    {
        private static readonly CreditosAutorizadosL _classInstance = new CreditosAutorizadosL();

        public static CreditosAutorizadosL ClassInstance
        {
            get { return _classInstance; }
        }

        public CreditosAutorizadosL()
        {
        }

        public List<CAT_REGION> ObtenListaRegion()
        {
            return CreditosAutorizadosA.ClassInstance.ObtenListaRegion();
        }

        public List<CAT_ZONA> ObtenListaZona()
        {
            return CreditosAutorizadosA.ClassInstance.ObtenListaZona();
        }

        public List<CAT_ZONA> ObtenListaZonaFiltro(int Cve_Region)
        {
            return CreditosAutorizadosA.ClassInstance.ObtenListaZonaFiltro(Cve_Region);
        }

        public List<CAT_ESTADO> ObtenListaEstados()
        {
            return CreditosAutorizadosA.ClassInstance.ObtenListaEstado();
        }

        public List<CAT_DELEG_MUNICIPIO> ObtenListaMunicipios(int Cve_Estado)
        {
            return CreditosAutorizadosA.ClassInstance.ObtenListaMunicipios(Cve_Estado);
        }

        public CAT_ZONA ObtenUbicacion(int Cve_Zona)
        {
            return CreditosAutorizadosA.ClassInstance.ObtenUbicacion(Cve_Zona);
        }

        public CAT_ZONA ObtenUbicacionZona(int Cve_Region, int Cve_Zona)
        {
            return CreditosAutorizadosA.ClassInstance.ObtenUbuacionZona(Cve_Region, Cve_Zona);
        }

        public List<GridCreditosLiberados> ObteGridCreditosLiberados(int Region, int Zona, int Estado, int Municipio, string No_Credito, DateTime? Desde, DateTime? Hasta)
        {
            var Lista = CreditosAutorizadosA.ClassInstance.ListaCreditosLiberados(Region,Zona,Estado,Municipio,No_Credito,Desde,Hasta);

            //List<GridCreditosLiberados> Filtrada = new List<GridCreditosLiberados>();

            //foreach (GridCreditosLiberados Item in Lista)
            //{
            //    if (Item.FechaLiberado >= Desde && Item.FechaLiberado <= Hasta)
            //    {
            //        if (Estado == -1)
            //        {
            //            if (Region == 0)
            //            {
            //            }
            //            else
            //            {
            //                if (Zona == 0)
            //                {
            //                    if (No_Credito != "")
            //                    {
            //                        if (
            //                            Item.No_Credito == No_Credito &&
            //                            Item.Cve_region == Region
            //                            )
            //                        {
            //                            Filtrada.Add(Item);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (Item.Cve_region == Region)
            //                        {
            //                            Filtrada.Add(Item);
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    if (No_Credito != "")
            //                    {
            //                        if (
            //                            Item.No_Credito == No_Credito &&
            //                            Item.Cve_region == Region &&
            //                            Item.Cve_Zona == Zona
            //                            )
            //                        {
            //                            Filtrada.Add(Item);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (Item.Cve_region == Region &&
            //                            Item.Cve_Zona == Zona
            //                            )
            //                        {
            //                            Filtrada.Add(Item);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        else if (Estado == 0)
            //        {
            //        }
            //        else
            //        {
            //        }
            //    }
            //    else
            //    {
            //    }
            //}
            return Lista;
        }
    }
}