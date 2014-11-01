using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos.ModuloCentral;


namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class cmConsultas
    {
        public List<CRE_Consultas> obtieneConsultas()
        {
            var varConsulta = new Consultas();
            return varConsulta.ObtenerConsultas();
        }
        public CRE_Consultas InsertaConsultas(CRE_Consultas Query)
        {
            var varConsulta = new Consultas();
            return varConsulta.InsertaConsultas(Query);
        }
        public bool ActualizaConsultas(CRE_Consultas Query)
        {
            var varConsulta = new Consultas();
            return varConsulta.ActualizaConsultas(Query);
        }
        public bool EliminaConsultas(CRE_Consultas Query) {
            var varConsulta = new Consultas();
            return varConsulta.EliminaConsultas(Query);
        }
    }
}
