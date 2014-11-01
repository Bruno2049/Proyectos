using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class InformacionParseo
    {
      
        public TR_INFORMACION_PARSEO Agregar(TR_INFORMACION_PARSEO infoGeneral)
        {
            TR_INFORMACION_PARSEO trInfoGeneral = null;

            using (var r = new Repositorio<TR_INFORMACION_PARSEO>())
            {
                trInfoGeneral = r.Extraer(c => c.ID_INFO == infoGeneral.ID_INFO);

                if (trInfoGeneral == null)                             
                    trInfoGeneral = r.Agregar(infoGeneral);                                    
                else               
                    throw new Exception("La regla de la trama ya existe en la BD.");               
            }

            return trInfoGeneral;
        }       

        public TR_INFORMACION_PARSEO ObtienePorCondicion(Expression<Func<TR_INFORMACION_PARSEO, bool>> criterio) 
        {
            TR_INFORMACION_PARSEO trInfoGeneral = null;

            using(var r = new Repositorio<TR_INFORMACION_PARSEO>())
            {
                trInfoGeneral = r.Filtro(criterio).FirstOrDefault();
            }


            return trInfoGeneral;
        }


        public bool Actualizar(TR_INFORMACION_PARSEO infoGeneral)
        {
            bool actualiza = false;

            var trInfoGeneral = ObtienePorCondicion(info => info.ID_INFO == infoGeneral.ID_INFO);

            if (trInfoGeneral != null)
            {
                using(var r = new Repositorio<TR_INFORMACION_PARSEO>())
                {
                    actualiza = r.Actualizar(trInfoGeneral);
                }
            }
            else 
            {
                throw new Exception("La operacion de la trama con Id: " + trInfoGeneral.ID_INFO + " no fue encontrada.");
            }

            return actualiza;
        }


        public bool Eliminar(int id)
        {
            bool elimina = false;

            var trInfoGeneral = ObtienePorCondicion(info => info.ID_INFO == id);

            if (trInfoGeneral != null)
            {                
                using (var r = new Repositorio<TR_INFORMACION_PARSEO>())
                {
                    elimina = r.Eliminar(trInfoGeneral);                    
                }
            }
            else
            {
                throw new Exception("No se encontro la operacion de la trama indicada.");
            }

            return false;
        }


        public List<TR_INFORMACION_PARSEO> FitrarPorCondicion(Expression<Func<TR_INFORMACION_PARSEO,bool>> criterio)
        {
            List<TR_INFORMACION_PARSEO> listInfoGeneral = null;

            using (var r = new Repositorio<TR_INFORMACION_PARSEO>())
            {
                listInfoGeneral = r.Filtro(criterio);
            }

            return listInfoGeneral;
        }
        

    }
}

