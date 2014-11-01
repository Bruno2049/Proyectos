using System.Collections.Generic;
using System.Linq;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.SolicitudCredito
{
    public class Captura
    {
        #region Metodos de Insercion

        public static CRE_Credito InsertaCredito(CRE_Credito credito)
        {
            CRE_Credito newCredito;

            using (var r = new Repositorio<CRE_Credito>())
            {
                newCredito = r.Agregar(credito);
            }

            return newCredito;
        }

        public static CLI_Cliente InsertaCliente(CLI_Cliente cliente)
        {
            CLI_Cliente newCliente;

            if (cliente.CLI_Negocio.Any())
            {
                var negocio = cliente.CLI_Negocio.First();
                negocio.IdNegocio = 1;
                if (negocio.DIR_Direcciones.Any())
                {
                    foreach (var direccion in negocio.DIR_Direcciones)
                    {
                        direccion.IdNegocio = negocio.IdNegocio;
                    }
                }
            }

            using (var r = new Repositorio<CLI_Cliente>())
            {
                newCliente = r.Agregar(cliente);
            }

            return newCliente;
        }

        public static CLI_Negocio InsertaNegocio(CLI_Negocio negocio)
        {
            CLI_Negocio newNegocio;

            negocio.IdNegocio = GenerarIdNegocio(negocio.Id_Proveedor, negocio.Id_Branch, negocio.IdCliente);
            if (negocio.DIR_Direcciones.Any())
            {
                foreach (var direccion in negocio.DIR_Direcciones)
                {
                    direccion.IdNegocio = negocio.IdNegocio;
                }
            }

            using (var r = new Repositorio<CLI_Negocio>())
            {
                newNegocio = r.Agregar(negocio);
            }

            return newNegocio;
        }

        public static DIR_Direcciones InsertaDirecciones(DIR_Direcciones direccion)
        {
            DIR_Direcciones newDireccion;

            using (var r = new Repositorio<DIR_Direcciones>())
            {
                newDireccion = r.Agregar(direccion);
            }

            return newDireccion;
        }

        public static CLI_Ref_Cliente InsertaReferenciaCliente(CLI_Ref_Cliente referenciaCliente)
        {
            CLI_Ref_Cliente newReferencia;

            using (var r = new Repositorio<CLI_Ref_Cliente>())
            {
                newReferencia = r.Agregar(referenciaCliente);
            }

            return newReferencia;
        }

        public static CLI_Referencias_Notariales InsertaReferenciasNotariales(CLI_Referencias_Notariales referencia)
        {
            CLI_Referencias_Notariales newReferenciaNotarial;

            using (var r = new Repositorio<CLI_Referencias_Notariales>())
            {
                newReferenciaNotarial = r.Agregar(referencia);
            }

            return newReferenciaNotarial;
        }

        #endregion

        #region Metodos de Actualizacion

        public static bool ActualizaCliente(CLI_Cliente cliente)
        {
            bool actualiza;

            using (var r = new Repositorio<CLI_Cliente>())
            {
                actualiza = r.Actualizar(cliente);
            }
            if (actualiza && cliente.CLI_Negocio.Any())
            {
                using (var r = new Repositorio<CLI_Negocio>())
                {
                    actualiza = r.Actualizar(cliente.CLI_Negocio.First());
                }
            }

            return actualiza;
        }

        public static bool ActualizaDireccion(DIR_Direcciones direccion)
        {
            bool actualiza;

            using (var r = new Repositorio<DIR_Direcciones>())
            {
                actualiza = r.Actualizar(direccion);
            }

            return actualiza;
        }

        public static bool ActualizaReferencia(CLI_Ref_Cliente referencia)
        {
            bool actualiza;

            using (var r = new Repositorio<CLI_Ref_Cliente>())
            {
                actualiza = r.Actualizar(referencia);
            }

            return actualiza;
        }

        public static bool ActualizaReferenciaNotarial(CLI_Referencias_Notariales referencia)
        {
            bool actualiza;

            using (var r = new Repositorio<CLI_Referencias_Notariales>())
            {
                actualiza = r.Actualizar(referencia);
            }

            return actualiza;
        }

        public static bool ActualizaCredito(CRE_Credito credito)
        {
            bool actualiza;

            using (var r = new Repositorio<CRE_Credito>())
            {
                actualiza = r.Actualizar(credito);
            }

            return actualiza;
        }
        #endregion

        #region Metodos de Consulta

        public static CLI_Cliente ObtenCliente(int idProveedor, int idSucursal, int idCliente)
        {
            CLI_Cliente cliente;

            using (var r = new Repositorio<CLI_Cliente>())
            {
                cliente = r.Extraer(me => me.Id_Proveedor == idProveedor && me.Id_Branch == idSucursal && me.IdCliente == idCliente);
            }

            return cliente;
        }

        public static CLI_Negocio ObtenNegocioCliente(int idProveedor, int idSucursal, int idCliente, int idNegocio)
        {
            CLI_Negocio negocio;

            using (var r = new Repositorio<CLI_Negocio>())
            {
                negocio = r.Extraer(me => me.Id_Proveedor == idProveedor && me.Id_Branch == idSucursal && me.IdCliente == idCliente && me.IdNegocio == idNegocio);
            }

            return negocio;
        }

        public static DIR_Direcciones ObtenDireccionCliente(int idProveedor, int idSucursal, int idCliente, int idNegocio, byte tipoDomicilio)
        {
            DIR_Direcciones direcciones;

            using (var r = new Repositorio<DIR_Direcciones>())
            {
                direcciones = r.Filtro(me => me.Id_Proveedor == idProveedor && me.Id_Branch == idSucursal && me.IdCliente == idCliente && me.IdNegocio == idNegocio && me.IdTipoDomicilio == tipoDomicilio).FirstOrDefault();
            }

            return direcciones;
        }

        public static List<DIR_Direcciones> ObtenDireccionesCliente(int idProveedor, int idSucursal, int idCliente, int idNegocio)
        {
            List<DIR_Direcciones> lstDirecciones;

            using (var r = new Repositorio<DIR_Direcciones>())
            {
                lstDirecciones = r.Filtro(me => me.Id_Proveedor == idProveedor && me.Id_Branch == idSucursal && me.IdCliente == idCliente && me.IdNegocio == idNegocio);
            }

            return lstDirecciones;
        }

        public static List<CLI_Ref_Cliente> ObtenReferenciasCliente(int idProveedor, int idSucursal, int idCliente, int idNegocio)
        {
            List<CLI_Ref_Cliente> lstRefCliente;

            using (var r = new Repositorio<CLI_Ref_Cliente>())
            {
                lstRefCliente = r.Filtro(me => me.Id_Proveedor == idProveedor && me.Id_Branch == idSucursal && me.IdCliente == idCliente && me.IdNegocio == idNegocio);
            }

            return lstRefCliente;
        }

        public static CLI_Ref_Cliente ObtenReferenciasCliente(int idProveedor, int idSucursal, int idCliente, int idNegocio, byte tipoReferencia)
        {
            CLI_Ref_Cliente referencia;

            using (var r = new Repositorio<CLI_Ref_Cliente>())
            {
                referencia = r.Filtro(me => me.Id_Proveedor == idProveedor && me.Id_Branch == idSucursal && me.IdCliente == idCliente && me.IdNegocio == idNegocio && me.IdTipoReferencia == tipoReferencia).FirstOrDefault();
            }

            return referencia;
        }

        public static List<CLI_Referencias_Notariales> ObtenReferenciasNotariales(int idProveedor, int idSucursal, int idCliente, int idNegocio)
        {
            List<CLI_Referencias_Notariales> lstReferenciasNotariales;

            using (var r = new Repositorio<CLI_Referencias_Notariales>())
            {
                lstReferenciasNotariales = r.Filtro(me => me.Id_Proveedor == idProveedor && me.Id_Branch == idSucursal && me.IdCliente == idCliente && me.IdNegocio == idNegocio);
            }

            return lstReferenciasNotariales;
        }

        public static CRE_Credito ObtenCredito(string noCredito)
        {
            CRE_Credito credito;

            using (var r = new Repositorio<CRE_Credito>())
            {
                credito = r.Extraer(me => me.No_Credito == noCredito);
            }

            return credito;
        }

        public static CRE_Credito ObtenCreditoPorIdCliente(int idCliente)
        {
            CRE_Credito credito;

            using (var r = new Repositorio<CRE_Credito>())
            {
                credito = r.Extraer(me => me.IdCliente == idCliente);
            }

            return credito;
        }

        public static bool ValidaDirecciones(int idProveedor, int idSucursal, int idCliente, int idNegocio)
        {
            using (var r = new Repositorio<DIR_Direcciones>())
            {
                var lstDirecciones = r.Filtro(me => me.Id_Proveedor == idProveedor && me.Id_Branch == idSucursal && me.IdCliente == idCliente && me.IdNegocio == idNegocio);
                return lstDirecciones.Count > 0;
            }           
        }

        public static bool ValidaReferenciasCliente(int idProveedor, int idSucursal, int idCliente, int idNegocio)
        {
            using (var r = new Repositorio<CLI_Ref_Cliente>())
            {
                var lstReferencias = r.Filtro(me => me.Id_Proveedor == idProveedor && me.Id_Branch == idSucursal && me.IdCliente == idCliente && me.IdNegocio == idNegocio);
                return lstReferencias.Count > 0;
            }
        }
        #endregion

        #region Privados

        private static short GenerarIdNegocio(int idProveedor, int idSucursal, int idCliente)
        {
            short idNegocio = 1;
            using (var rep = new Repositorio<CLI_Negocio>())
            {
                var negociosCliente = rep.Filtro(n => n.Id_Proveedor == idProveedor && n.Id_Branch == idSucursal && n.IdCliente == idCliente);
                if (negociosCliente.Any())
                {
                    idNegocio = (short) (negociosCliente.Max(n => n.IdNegocio) + 1);
                }

            }
            return idNegocio;
        }

        #endregion

        public static CLI_Referencias_Notariales ObtenReferenciaNotarialPorTipo(int idProveedor, int idSucursal, int idCliente, int idNegocio, int idTipoRef)
        {
            CLI_Referencias_Notariales referenciasNotariales;

            using (var r = new Repositorio<CLI_Referencias_Notariales>())
            {
                referenciasNotariales = r.Extraer(me => me.Id_Proveedor == idProveedor && me.Id_Branch == idSucursal && me.IdCliente == idCliente && me.IdNegocio == idNegocio && me.IdTipoReferencia == idTipoRef);
            }

            return referenciasNotariales;
        }

        public static bool EliminaReferenciaNotarial(CLI_Referencias_Notariales referencia)
        {
            bool elimina;

            using (var r = new Repositorio<CLI_Referencias_Notariales>())
            {
                elimina = r.Eliminar(referencia);
            }

            return elimina;
        }

    }

}
