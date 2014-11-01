using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.SolicitudCredito
{
    public class CREDITO_LOG
    {
        private static readonly CREDITO_LOG _classInstance = new CREDITO_LOG();
        public static CREDITO_LOG ClassInstance { get { return _classInstance; } }

        public CREDITO_LOG()
        { 
        
        }

        public K_CREDITO_LOG Agregar(K_CREDITO_LOG creditoLog)
        {
            K_CREDITO_LOG credito = null;

            using (var r = new Repositorio<K_CREDITO_LOG>())
            {
                credito = r.Agregar(creditoLog);
            }

            return credito;
        }
    }
}
