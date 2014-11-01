using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.AccesoDatos.Tarifas;
using PAEEEM.Entidades;

namespace PAEEEM.LogicaNegocios.Tarifas
{
    public class FactorDegradacion
    {
        private static readonly FactorDegradacion _classInstance = new FactorDegradacion();

        public static FactorDegradacion ClassInstance
        {
            get { return _classInstance; }
        }

        public object IrPorDatos(int region, int tecnologia)
        {
            return FactorDegradacionDatos.ClassInstance.IrPorDatos(region, tecnologia);
        }

        public bool Actualiza_FD (decimal factorDegradacion, int idregionBioclima, int cveTecnologia, string usuario)
        {            
            return FactorDegradacionDatos.ClassInstance.Actualiza_FD(factorDegradacion, idregionBioclima, cveTecnologia, usuario);
        }
    }
}
