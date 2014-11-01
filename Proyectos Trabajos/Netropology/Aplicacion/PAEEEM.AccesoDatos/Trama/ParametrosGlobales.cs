using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AltaBajaEquipos;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class ParametrosGlobales
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public TR_PARAMETROS_GLOBALES Agregar(TR_PARAMETROS_GLOBALES parametros)
        {
            TR_PARAMETROS_GLOBALES trParametros = null;

            using (var r = new Repositorio<TR_PARAMETROS_GLOBALES>())
            {
                trParametros = r.Extraer(c => c.IDPARAMETRO == parametros.IDPARAMETRO && c.IDSECCION == parametros.IDSECCION);

                if (trParametros == null)
                    trParametros = r.Agregar(trParametros);
                else
                    throw new Exception("El parametro ya existe en la BD.");
            }

            return trParametros;
        }


        public TR_PARAMETROS_GLOBALES ObtienePorCondicion(Expression<Func<TR_PARAMETROS_GLOBALES, bool>> criterio)
        {
            TR_PARAMETROS_GLOBALES trParametro = null;


            using (var r = new Repositorio<TR_PARAMETROS_GLOBALES>())
            {
                trParametro = r.Extraer(criterio); 
            }

            return trParametro;
        }


        public List<TR_PARAMETROS_GLOBALES> ObtieneTodos()
        {
            List<TR_PARAMETROS_GLOBALES> listParametros = null;

            using (var r = new Repositorio<TR_PARAMETROS_GLOBALES>())
            {
                listParametros = r.Filtro(null);
            }

            return listParametros;
        }


        public bool Actualizar(TR_PARAMETROS_GLOBALES parametro)
        {
            bool actualiza = false;

            var trParametro = ObtienePorCondicion(p => p.IDPARAMETRO == parametro.IDPARAMETRO && p.IDSECCION == parametro.IDSECCION);

            if (parametro != null)
            {
                using (var r = new Repositorio<TR_PARAMETROS_GLOBALES>())
                {
                    actualiza = r.Actualizar(parametro);
                }
            }
            else
            {
                throw new Exception("El parametro con IdParametro: " + trParametro.IDPARAMETRO + " y idSeccion: "+ trParametro.IDSECCION + " no fue encontrada.");
            }

            return actualiza;
        }


        public bool Elminar(int idParametro, int idSeccion)
        {
            bool elimina = false;

            var trParametros = ObtienePorCondicion(p => p.IDPARAMETRO == idParametro && p.IDSECCION == idSeccion);

            if (trParametros != null)
            {
                using (var r = new Repositorio<TR_PARAMETROS_GLOBALES>())
                {
                    elimina = r.Eliminar(trParametros);
                }
            }
            else
            {
                throw new Exception("No se encontro el parametro indicado.");
            }

            return elimina;
        }


        public List<TR_PARAMETROS_GLOBALES> FiltraPorCondicion(Expression<Func<TR_PARAMETROS_GLOBALES,bool>> criterio)
        {
            List<TR_PARAMETROS_GLOBALES> listParametros = null;

            using (var r = new Repositorio<TR_PARAMETROS_GLOBALES>())
            {
                listParametros = r.Filtro(criterio);
            }

            return listParametros;
        }


        public List<Presupuesto> ObtieneConceptosPresupuesto()
        {
            var conceptos = (from ctos in _contexto.TR_PARAMETROS_GLOBALES
                             where ctos.IDSECCION == 13
                             select new Presupuesto
                                 {
                                     IdPresupuesto = ctos.IDPARAMETRO,
                                     Nombre = ctos.VALOR,
                                 }).ToList();
            return conceptos;
        }


    }
}
