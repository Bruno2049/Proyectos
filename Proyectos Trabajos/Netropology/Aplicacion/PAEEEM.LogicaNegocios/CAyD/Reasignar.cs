using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.CayD;
using PAEEEM.Entidades.CAyD;
using PAEEEM.Entidades;
namespace PAEEEM.LogicaNegocios.CAyD
{
    public class Reasignar
    {
        public bool ExisteFechaRecepcion(string solicitud)
        {
            var resul = new ReasignarSolicitudes().ExisteFechaRecepcion(solicitud);
            return resul;
        }

        public List<DatosReasignar> ObtenerSolicitudes(int RS, int CAyD, string solicitud, string folio)
        {
            var resul = new ReasignarSolicitudes().ObtenerSolicitudes(RS, CAyD, solicitud, folio);
            return resul;
        }

        public List<CAT_CENTRO_DISP_SUCURSAL> CAyDs()
        {
            var resul = new ReasignarSolicitudes().CAyDs();
            return resul;
        }

        public List<CAT_PROVEEDORBRANCH> Distribuidores()
        {
            var resul = new ReasignarSolicitudes().Distribuidores();
            return resul;
        }

        public bool ActualizarCAyD(K_CREDITO_SUSTITUCION informacion)
        {
            bool resul = new ReasignarSolicitudes().ActualizarCAyD(informacion);
            return resul;
        }

        public K_CREDITO_SUSTITUCION ObtenerSolicitudByFolio(string folio)
        {
            var resul = new ReasignarSolicitudes().ObtenerSolicitudByFolio(folio);
            return resul;
        }
    }
}
