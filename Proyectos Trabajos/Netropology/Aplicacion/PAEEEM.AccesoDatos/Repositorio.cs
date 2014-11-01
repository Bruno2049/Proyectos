using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        private PAEEEM_DESAEntidades Contexto;

        private DbSet<TEntity> EntitySet
        {
            get { return Contexto.Set<TEntity>(); }
        }

        public Repositorio()
        {
            Contexto = new PAEEEM_DESAEntidades();
            
            //LINEA DE CODIGO PARA CONSUMIR UN WCF
            Contexto.Configuration.ProxyCreationEnabled = false;            
        }


        public TEntity Agregar(TEntity agregar)
        {
            TEntity Resultado = null;

            try
            {
                EntitySet.Add(agregar);
                Contexto.SaveChanges();
                Resultado = agregar;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Resultado;
        }

        public bool Eliminar(TEntity elminar)
        {
            bool Resultado = false;

            try
            {
                
                EntitySet.Attach(elminar);
                EntitySet.Remove(elminar);
                Resultado = Contexto.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Resultado;
        }

        public bool Actualizar(TEntity actualizar)
        {
            bool Resultado = false;

            try
            {
                EntitySet.Attach(actualizar);
                Contexto.Entry<TEntity>(actualizar).State = EntityState.Modified;
                Resultado = Contexto.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }

        public TEntity Extraer(System.Linq.Expressions.Expression<Func<TEntity, bool>> criterio)
        {
            TEntity Resultado = null;

            try
            {                
                Resultado = EntitySet.FirstOrDefault(criterio);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Resultado;
        }

        public List<TEntity> Filtro(System.Linq.Expressions.Expression<Func<TEntity, bool>> criterio)
        {
            List<TEntity> Resultado = null;

            try
            {
                Resultado = EntitySet.Where(criterio).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Resultado;
        }
        


        public void Dispose()
        {
            if (Contexto != null)
                Contexto.Dispose();
        }

    }
}
