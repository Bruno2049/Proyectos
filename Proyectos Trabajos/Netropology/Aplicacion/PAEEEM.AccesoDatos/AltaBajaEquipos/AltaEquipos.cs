using System;
using System.Collections.Generic;
using System.Linq;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.AltaBajaEquipos
{
    public class AltaEquipos
    {
        private static readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public static K_CREDITO_PRODUCTO Insertar(K_CREDITO_PRODUCTO informacion)
        {
            K_CREDITO_PRODUCTO trInformacion;

            using (var r = new Repositorio<K_CREDITO_PRODUCTO>())
            {
                trInformacion = r.Extraer(c => c.ID_CREDITO_PRODUCTO == informacion.ID_CREDITO_PRODUCTO);

                if (trInformacion == null)
                    trInformacion = r.Agregar(informacion);
                else
                    throw new Exception("El registro ya existe en la BD.");
            }
            return trInformacion;
        }

        public static K_CREDITO_PRODUCTO ObtieneKCreditoProductoPorId(int id)
        {
            K_CREDITO_PRODUCTO trInfoGeneral;

            using (var r = new Repositorio<K_CREDITO_PRODUCTO>())
            {
                trInfoGeneral = r.Extraer(tr => tr.ID_CREDITO_PRODUCTO == id);
            }
            return trInfoGeneral;
        }

        public static int ObtieneCveProductoPorIdCreditoProducto(int id)
        {
             K_CREDITO_PRODUCTO datos;
             var cve_prod = 0;
            try
            {
           
            using (var r = new Repositorio<K_CREDITO_PRODUCTO>())
            {
                datos =
                    r.Filtro(
                        me =>
                            me.ID_CREDITO_PRODUCTO == id 
                            ).FirstOrDefault();
            }
            cve_prod = datos.Cve_Producto;

            }
            catch (Exception e)
            {

                var ms = e.InnerException + " " + e.Message;
            }

            return cve_prod;
        }


        public static bool Actualizar(K_CREDITO_PRODUCTO informacion)
        {
            bool actualiza;

            var trInfoGeneral = ObtieneKCreditoProductoPorId(informacion.ID_CREDITO_PRODUCTO);

            if (trInfoGeneral != null)
            {
                using (var r = new Repositorio<K_CREDITO_PRODUCTO>())
                {
                    actualiza = r.Actualizar(informacion);
                }
            }
            else
            {
                throw new Exception("El Id: " + informacion.ID_CREDITO_PRODUCTO + " no fue encontrado.");
            }

            return actualiza;
        }

        public static string ObtieneTipoMovientoTecnologia(int cveProducto)
        {
            var tipoMOvimiento = (from tec in _contexto.CAT_TECNOLOGIA
                join tm in _contexto.CAT_TIPO_MOVIMIENTO on tec.Cve_Tipo_Movimiento equals tm.Cve_Tipo_Movimiento
                join cp in _contexto.CAT_PRODUCTO on tec.Cve_Tecnologia equals cp.Cve_Tecnologia
                                  where cp.Cve_Producto == cveProducto
                select new DatosCompEquipoAlta
                {
                    CveTipoMovimiento = tm.Cve_Tipo_Movimiento
                }).First();

            return tipoMOvimiento.CveTipoMovimiento;
        }

        public static bool TieneFotos(string No_Credito)
        {            
            CRE_FOTOS Fotos;

            using (var r = new Repositorio<CRE_FOTOS>())
            {
                Fotos = r.Extraer(tr => tr.No_Credito == No_Credito && tr.idTipoFoto == 2);
            }

            if (Fotos != null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
       
    }
}
