using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades.CirculoCredito;
using PAEEEM.AccesoDatos.CirculoCredito;
using PAEEEM.Entidades;

namespace PAEEEM.LogicaNegocios.CirculoCredito
{
    public class GeneracionPaquetes
    {
        public static List<DatosPackPendiente> GenerarPak(string usu)
        {
            return PaquetesPendientes.datosPendientes(usu,0);
        }

        public static List<DatosPackPendiente> GenerarPakRechazado(string usu)
        {
            return PaquetesPendientes.datosPendientes(usu,4);
        }


        public static string GenerasFolio(int idusu)
        {
            var folio = "PAQUETE" + PaquetesPendientes.zona(idusu);
            return folio;
        }

        public static int noPaquete(string folio, string usu)
        {
            List<DatosPackPendiente> fol = PaquetesPendientes.noPaquete(folio, usu);

            if (fol.Count!=0)
            {
                return PaquetesPendientes.noPaquete(folio, usu).Last().x+1; 
            }
            else
            {
                return 1;  
            }            
        }

        public static void Guardapaquete(PAQUETES_CIRCULO_CREDITO pac)
        {
            PaquetesPendientes.Guardapaquete2(pac);
        }


        public static List<CAT_ESTATUS_PAQUETE> catalogo()
        {
            List<CAT_ESTATUS_PAQUETE> n = PaquetesPendientes.catalogo();            
            return n;
        }

        public static List<CAT_ESTATUS_PAQUETE> catEstatusZon()
        {
            List<CAT_ESTATUS_PAQUETE> n = PaquetesPendientes.catalogoEstatusZona();
            return n;
        }

    }
}
