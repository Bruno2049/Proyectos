using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.AltaBajaEquipos
{
    public class TablasAhorro
    {

        public ABE_TABLAS_AHORRO Agregar(ABE_TABLAS_AHORRO tablasAhorro)
        {
            ABE_TABLAS_AHORRO abeTAhorro = null;

            using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
            {
                abeTAhorro = r.Extraer(c => c.IDAHORRO == tablasAhorro.IDAHORRO);

                if (abeTAhorro == null)
                    abeTAhorro = r.Agregar(abeTAhorro);
                else
                    throw new Exception("El registro del Ahorro ya existe en la BD.");
            }

            return abeTAhorro;
        }


        public ABE_TABLAS_AHORRO ObtienePorCondicion(Expression<Func<ABE_TABLAS_AHORRO, bool>> criterio)
        {
            ABE_TABLAS_AHORRO abeTAhorro = null;


            using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
            {
                abeTAhorro = r.Extraer(criterio);
            }

            return abeTAhorro;
        }


        public List<ABE_TABLAS_AHORRO> ObtieneTodos()
        {
            List<ABE_TABLAS_AHORRO> listTAhorros = null;

            using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
            {
                listTAhorros = r.Filtro(null);
            }

            return listTAhorros;
        }


        public bool Actualizar(ABE_TABLAS_AHORRO tablasAhorro)
        {
            bool actualiza = false;

            var abeTAhorro = ObtienePorCondicion(p => p.IDAHORRO == tablasAhorro.IDAHORRO);

            if (tablasAhorro != null)
            {
                using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
                {
                    actualiza = r.Actualizar(tablasAhorro);
                }
            }
            else
            {
                throw new Exception("El registro de Ahorro con Id: " + abeTAhorro.IDAHORRO + " no fue encontrado.");
            }

            return actualiza;
        }


        public bool Elminar(int idTablaAhorro)
        {
            bool elimina = false;

            var abeTablasAhorro = ObtienePorCondicion(p => p.IDAHORRO == idTablaAhorro);

            if (abeTablasAhorro != null)
            {
                using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
                {
                    elimina = r.Eliminar(abeTablasAhorro);
                }
            }
            else
            {
                throw new Exception("No se encontro el registro del ahorro indicado.");
            }

            return elimina;
        }


        public List<ABE_TABLAS_AHORRO> FiltraPorCondicion(Expression<Func<ABE_TABLAS_AHORRO, bool>> criterio)
        {
            List<ABE_TABLAS_AHORRO> listAhorros = null;

            using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
            {
                listAhorros = r.Filtro(criterio);
            }

            return listAhorros;
        }

    }
}
