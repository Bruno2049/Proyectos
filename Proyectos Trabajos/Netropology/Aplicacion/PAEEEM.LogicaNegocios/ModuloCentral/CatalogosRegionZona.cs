using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos;


namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class CatalogosRegionZona
    {
        public static List<CAT_REGION> catRegion()
        {
            List<CAT_REGION> reg = AccesoDatos.Catalogos.Region.CatRegion();
            return reg;
        }


        public static List<CAT_ZONA> catZona()
        {
            List<CAT_ZONA> zon = AccesoDatos.Catalogos.Zona.CatZona();

            return zon;
        }

        public static List<CAT_REGION> catRegion(int idRegion)
        {
            List<CAT_REGION> reg = AccesoDatos.Catalogos.Region.CatRegionid(idRegion);
            return reg;
        }


        public static List<CAT_ZONA> catZona(int idZona)
        {
            List<CAT_ZONA> zon = AccesoDatos.Catalogos.Zona.CatZonaid(idZona);

            return zon;
        }

        public static List<CAT_ZONA> catZonaxidRegion(int idRegion)
        {
            List<CAT_ZONA> zon = AccesoDatos.Catalogos.Zona.CatZonaxRegion(idRegion);

            return zon;
        }
        public static List<CAT_ESTADO> catEstados()
        {
            List<CAT_ESTADO> estado = AccesoDatos.Catalogos.DelegacionMunicipio.obtieneTodosEstados();
            return estado;
        }
        public static List<CAT_DELEG_MUNICIPIO> catDelegacionOMunicipio(int cveEstado)
        {
            List<CAT_DELEG_MUNICIPIO> delegacion =  AccesoDatos.Catalogos.DelegacionMunicipio.cveEstado(cveEstado);
            return delegacion;

        }
        public static List<CAT_CODIGO_POSTAL_SEPOMEX> catColonias(int cveMunicipio)
        {
            List<CAT_CODIGO_POSTAL_SEPOMEX> colonia = AccesoDatos.Catalogos.DelegacionMunicipio.listColonias(cveMunicipio);
                return colonia;
        }
        public static CAT_CODIGO_POSTAL_SEPOMEX CODPOSTAL(string codigo)
        {
            var objeto = AccesoDatos.Catalogos.DelegacionMunicipio.CodigoPostal(codigo);
            return objeto;
               
        }
        public static CAT_CODIGO_POSTAL_SEPOMEX obtieneCodpostal(int valor)
        {
            var clave = AccesoDatos.Catalogos.DelegacionMunicipio.obtieneCodPostal(valor);
            return clave;
        }
        //
        public static List<CAT_CODIGO_POSTAL_SEPOMEX> listasCP(string valor)
        {
            List<CAT_CODIGO_POSTAL_SEPOMEX> claves = null;
             claves = AccesoDatos.Catalogos.DelegacionMunicipio.obtieneCodPostalLista(valor);
             return claves;
        }

        //public static List<CAT_DELEG_MUNICIPIO> obtieneTodasDelegaciones

    }
}
