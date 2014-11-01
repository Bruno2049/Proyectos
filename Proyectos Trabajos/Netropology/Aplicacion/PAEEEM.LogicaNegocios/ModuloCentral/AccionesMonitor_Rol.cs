using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades.AdminUsuarios;
using PAEEEM.AccesoDatos.Catalogos;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class AccionesMonitor_Rol
    {
        public List<AccionUsuario> AccionesMonitorRol(int monitor, int idRol)
        {
            var resul = new AccionesRol().AccionesMonitorRol(monitor, idRol);
            return resul;
        }

        public List<AccionUsuario> accionesEstatus(int cveEstatusProv)
        {
            var re = new AccionesRol().accionesEstatus(cveEstatusProv);
            return re;
        }
    }
}
