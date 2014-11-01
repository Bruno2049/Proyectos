using PAEEEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.AccesoDatos.ModuloCentral
{
    public class Consultas
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();
        public List<CRE_Consultas> ObtenerConsultas()
        {
            var estatus = from est in _contexto.CRE_Consultas
                          select est;

            return estatus.ToList();
        }
        public  CRE_Consultas InsertaConsultas(CRE_Consultas Query)
        {
            CRE_Consultas newQuery;

            using (var r = new Repositorio<CRE_Consultas>())
            {
                newQuery = r.Agregar(Query);
            }

            return newQuery;
        }
        public  bool ActualizaConsultas(CRE_Consultas Query)
        {
            bool actualiza;

            using (var r = new Repositorio<CRE_Consultas>())
            {
                actualiza = r.Actualizar(Query);
            }

            return actualiza;
        }
        public  bool EliminaConsultas(CRE_Consultas Query)
        {
            bool elimina;

            using (var r = new Repositorio<CRE_Consultas>())
            {
                elimina = r.Eliminar(Query);
            }

            return elimina;
        }
    
    }
}
