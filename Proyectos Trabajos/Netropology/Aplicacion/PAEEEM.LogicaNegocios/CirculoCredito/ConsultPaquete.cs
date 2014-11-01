using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.CirculoCredito;
using PAEEEM.AccesoDatos.CirculoCredito;

namespace PAEEEM.LogicaNegocios.CirculoCredito
{
    public class ConsultPaquete
    {
        public static List<ConsultaPaquetes> consultar(int status, string Credito, int Nopak, string folio,string usu)
        {
            return PaquetesPendientes.ConsultaPaquetes(status,Credito,Nopak,folio,usu);
        }

        public static List<ConsultaPaquetes> consulRevisionPaquet(int status, string Credito, int Nopak, string folio,string dist,string usu)
        {
            return PaquetesRevision.datosRevision(status,Credito,Nopak,folio,dist,usu);
        }

        public static List<PAQUETES_CIRCULO_CREDITO> catalogoFolio(string usu)
        {
            List<PAQUETES_CIRCULO_CREDITO> n = PaquetesRevision.catLofios(usu);
            return n;
        }

        public static List<PAQUETES_CIRCULO_CREDITO> catalogoFolioDis(string usu)
        {
            List<PAQUETES_CIRCULO_CREDITO> n = PaquetesRevision.catLofiosDis(usu);
            return n;
        }

        public static List<PAQUETES_CIRCULO_CREDITO> PackXFolio(string folio, int status)
        {
            List<PAQUETES_CIRCULO_CREDITO> n = PaquetesRevision.catPaquetesXFolio(folio,status);
            return n;
        }
        public static List<PAQUETES_CIRCULO_CREDITO> PackXFolioRevi(string folio)
        {
            List<PAQUETES_CIRCULO_CREDITO> n = PaquetesRevision.catPaquetesXFoliorevi(folio);
            return n;
        }

        public static List<ConsultaPaquetes> consulDistXfoliYnopak(int status,string folio, int noPak,string usu)
        {
            return PaquetesRevision.catDistXfoliNopaque(status,folio,noPak,usu);
        }

        public static List<ConsultaPaquetes> consulDist(string usu)
        {
            return PaquetesRevision.catDist(usu);
        }


        public static List<ConsultaPaquetes> consultarAceptados(int status, string Credito, int Nopak, string folio,string dist,string usu)
        {
            return PaquetesRevision.ConsultaPaqAceptados(status, Credito, Nopak, folio,dist,usu);
        }

        public static List<ConsultaPaquetes> consultarPack(int status, string Credito, int Nopak, string folio, string dist, string usu)
        {
            return PaquetesRevision.ConsultaPaq(status, Credito, Nopak, folio, dist, usu);
        }

        public static List<ConsultaPaquetes> consultarPendientes(int status, string Credito, string dist,string usu)
        {
            return PaquetesRevision.ConsultaPaqPendientes(status, Credito, dist,usu);
        }

        public static void ActualizarPack(string folio, int noPack, string credit,int status,string comentario)
        {

            var cir = PaquetesRevision.ObtienePorId(folio,noPack,credit);
     
            try
            {
                if (status == 4) { cir.FECHA_REVISION = null; } else { cir.FECHA_REVISION = DateTime.Now.Date; }
                cir.ID_ESTATUS_PAQUETE = (byte)status;
                cir.Comentarios = comentario;

                PaquetesRevision.Actualizarpac(cir);
            }
            catch (Exception e)
            {

            }
  
        }

    }
}
