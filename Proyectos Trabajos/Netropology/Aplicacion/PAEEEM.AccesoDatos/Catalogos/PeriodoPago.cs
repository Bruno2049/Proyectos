using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class PeriodoPago
    {

        public CAT_PERIODO_PAGO Agregar(CAT_PERIODO_PAGO periodoPago) 
        {
            CAT_PERIODO_PAGO trFacturacion = null;

            using(var r = new Repositorio<CAT_PERIODO_PAGO>())
            {
                trFacturacion = r.Extraer(c => c.Cve_Periodo_Pago == periodoPago.Cve_Periodo_Pago);

                if (trFacturacion == null)
                    trFacturacion = r.Agregar(periodoPago);
                else
                    throw new Exception("La facturacion agregada ya existe en la BD.");
            }

            return trFacturacion;
        }


        public CAT_PERIODO_PAGO ObtienePorCondicion(Expression<Func<CAT_PERIODO_PAGO,bool>> criterio)
        {
            CAT_PERIODO_PAGO trFacturacion = null;


            using(var r = new Repositorio<CAT_PERIODO_PAGO>())
            {
                trFacturacion = r.Extraer(criterio);
            }

            return trFacturacion;
        }


        public List<CAT_PERIODO_PAGO> ObtieneTodos() 
        {
            List<CAT_PERIODO_PAGO> listFacturacion = null;

            using(var r = new Repositorio<CAT_PERIODO_PAGO>())
            {
                listFacturacion = r.Filtro(null);
            }

            return listFacturacion;
        }


        public bool Actualizar(CAT_PERIODO_PAGO periodoPago) 
        {
            bool actualiza = false;

            var trInfoGeneral = ObtienePorCondicion(tr => tr.Cve_Periodo_Pago == periodoPago.Cve_Periodo_Pago);

            if (trInfoGeneral != null)
            {
                using(var r = new Repositorio<CAT_PERIODO_PAGO>())
                {
                    actualiza = r.Actualizar(periodoPago);
                }
            }
            else
            {
                throw new Exception("La facturación con Id: " + periodoPago.Cve_Periodo_Pago + " no fue encontrada.");
            }

            return actualiza;
        }


        public bool Elminar(int idPeriodoPago)
        {
            bool elimina = false;

            var trFacturacion = ObtienePorCondicion(p => p.Cve_Periodo_Pago == idPeriodoPago);

            if (trFacturacion != null)
            {
                using(var r = new Repositorio<CAT_PERIODO_PAGO>())
                {
                    elimina = r.Eliminar(trFacturacion);
                }
            }
            else
            {
                throw new Exception("No se encontro la facturacion indicada.");
            }

            return elimina;
        }


        public List<CAT_PERIODO_PAGO> FiltraPorCondicion(Expression<Func<CAT_PERIODO_PAGO,bool>> criterio) 
        {
            List<CAT_PERIODO_PAGO> listFacturacion = null;

            using (var r = new Repositorio<CAT_PERIODO_PAGO>())
            {
                listFacturacion = r.Filtro(criterio);
            }

            return listFacturacion;
        }

    }
}
