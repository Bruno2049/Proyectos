using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.LogicaNegocios.Credito
{
    public class variables
    {
        private static readonly variables _classInstance = new variables();
        public static variables ClassInstance { get { return _classInstance; } }

        public List<DetalleVariablesGlobales> obtenerDatos()
        {
            List<DetalleVariablesGlobales> datos = AccesoDatos.Catalogos.VariablesGlobales.ClassInstance.ObtenerVariablesGlobales();
            return datos;
        }
    }
}
