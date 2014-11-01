using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    [Serializable]
    public class AdminProducto
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public AdminProducto()
        {}

        #region Metodos de Consulta

        public static CAT_PRODUCTO ObtenProducto(int idproducto)
        {
            CAT_PRODUCTO resultado = null;

            using (var r =new Repositorio<CAT_PRODUCTO>())
            {
                resultado = r.Extraer(me => me.Cve_Producto == idproducto);
            }

            return resultado;
        }

        public static CAT_MODULOS_SE ObtenModulosSe(int idProducto)
        {
            CAT_MODULOS_SE resultado = null;

            using (var r = new Repositorio<CAT_MODULOS_SE>())
            {
                resultado = r.Extraer(me => me.Cve_Producto == idProducto);
            }

            return resultado;
        }

        public static List<TIPO_ENCAPSULADO> ObtenTipoEncapsulado()
        {
            List<TIPO_ENCAPSULADO> resultado;

            using (var r = new Repositorio<TIPO_ENCAPSULADO>())
            {
                resultado = r.Filtro(me => me.Cve_Tipo > 0);
            }

            return resultado;
        }
         public static List<TIPO_PROTECCION_INTERNA> ObtenTipoProteccionInterna()
        {
            List<TIPO_PROTECCION_INTERNA> resultado;

            using (var r = new Repositorio<TIPO_PROTECCION_INTERNA>())
            {
                resultado = r.Filtro(me => me.Cve_Proteccion_Interna > 0);
            }

            return resultado;
        }

         public static List<TIPO_PROTECCION_EXTERNA> ObtenTipoProteccionExterna()
         {
             List<TIPO_PROTECCION_EXTERNA> resultado;

             using (var r = new Repositorio<TIPO_PROTECCION_EXTERNA>())
             {
                 resultado = r.Filtro(me => me.Cve_Proteccion_Externa > 0);
             }

             return resultado;
         }

         public static List<MATERIAL_CUBIERTA> ObtenMaterialCubierta()
         {
             List<MATERIAL_CUBIERTA> resultado;

             using (var r = new Repositorio<MATERIAL_CUBIERTA>())
             {
                 resultado = r.Filtro(me => me.Cve_Material > 0);
             }

             return resultado;
         }

         public static List<PERDIDAS> ObtenPerdidas()
         {
             List<PERDIDAS> resultado;

             using (var r = new Repositorio<PERDIDAS>())
             {
                 resultado = r.Filtro(me => me.Cve_Perdidas > 0);
             }

             return resultado;
         }

         public static List<PROTECCION_SIN_CORRIENTE> ObtenProteccionSCorriente()
         {
             List<PROTECCION_SIN_CORRIENTE> resultado;

             using (var r = new Repositorio<PROTECCION_SIN_CORRIENTE>())
             {
                 resultado = r.Filtro(me => me.Cve_Proteccion > 0);
             }

             return resultado;
         }

         public static List<PROTECCION_CONTRA_FUEGO> ObtenProteccionContraFuego()
         {
             List<PROTECCION_CONTRA_FUEGO> resultado;

             using (var r = new Repositorio<PROTECCION_CONTRA_FUEGO>())
             {
                 resultado = r.Filtro(me => me.Cve_Prot_Contra_Fuego > 0);
             }

             return resultado;
         }

         public static List<PROTECCION_CONTRA_EXPLOSION> ObtenProteccionContraExplosion()
         {
             List<PROTECCION_CONTRA_EXPLOSION> resultado;

             using (var r = new Repositorio<PROTECCION_CONTRA_EXPLOSION>())
             {
                 resultado = r.Filtro(me => me.Cve_Protec_Contra_Exp > 0);
             }

             return resultado;
         }

         public static List<ANCLAJE> ObtenAnclaje()
         {
             List<ANCLAJE> resultado;

             using (var r = new Repositorio<ANCLAJE>())
             {
                 resultado = r.Filtro(me => me.Cve_Anclaje > 0);
             }

             return resultado;
         }

         public static List<TERMINAL_TIERRA> ObtenTerminalTierra()
         {
             List<TERMINAL_TIERRA> resultado;

             using (var r = new Repositorio<TERMINAL_TIERRA>())
             {
                 resultado = r.Filtro(me => me.Cve_Terminal_Tierra > 0);
             }

             return resultado;
         }

         public static List<PROTECCION_GABINETE> ObtenProteccionGabinete()
         {
             List<PROTECCION_GABINETE> resultado;

             using (var r = new Repositorio<PROTECCION_GABINETE>())
             {
                 resultado = r.Filtro(me => me.Cve_Proteccion_Gabinete > 0);
             }

             return resultado;
         }


        public static List<CAT_SE_CLASE> ObtenCatSeClase()
        {
            List<CAT_SE_CLASE> resultado = null;

            using (var r = new Repositorio<CAT_SE_CLASE>())
            {
                resultado = r.Filtro(me => me.Estatus == 1);

            }

            return resultado;
        }

        public static List<CAT_SE_TIPO> ObCatSeTipo(string idClase)
        {
            List<CAT_SE_TIPO> resultado = null;

            using (var r = new Repositorio<CAT_SE_TIPO>())
            {
                resultado = r.Filtro(me => me.Atributo_1 == "1" && me.Atributo_2 == idClase);
            }

            return resultado;
        }

        public static List<CAT_FABRICANTE> ObtenCatFabricantes()
        {
            List<CAT_FABRICANTE> resultado = null;

            using (var r = new Repositorio<CAT_FABRICANTE>())
            {
                resultado = r.Filtro(me => me.Cve_Fabricante > 0);
            }

            return resultado;
        }

        public static List<CAT_TIPO_PRODUCTO> ObtenCatTipoProductos(int idTecnologia)
        {
            List<CAT_TIPO_PRODUCTO> resultado = null;

            using (var r = new Repositorio<CAT_TIPO_PRODUCTO>())
            {
                resultado = r.Filtro(me => me.Cve_Tecnologia == idTecnologia
                    && me.EQUIPO_ALTA == true);
            }

            return resultado;
        }

        public static List<CAT_CAPACIDAD_SUSTITUCION> ObtenCatCapacidadSust(int idTecnologia)
        {
            List<CAT_CAPACIDAD_SUSTITUCION> resultado;

            using (var r = new Repositorio<CAT_CAPACIDAD_SUSTITUCION>())
            {
                resultado = r.Filtro(me => me.Cve_Tecnologia == idTecnologia);
            }

            return resultado;
        }

        public static List<CAT_MARCA> ObtenCatMarcas()
        {
            List<CAT_MARCA> resultado = null;

            using (var r = new Repositorio<CAT_MARCA>())
            {
                resultado = r.Filtro(me => me.Cve_Marca > 0);
            }

            return resultado;
        }

        public static List<CAT_POTENCIA> ObtenCatPotencias()
        {
            List<CAT_POTENCIA> resultado = null;

            using (var r = new Repositorio<CAT_POTENCIA>())
            {
                resultado = r.Filtro(me => me.Cve_Potencia > 0);
            }

            return resultado;
        }

        public static List<CAT_CAPACIDAD_SUSTITUCION> ObtenCatCapacidadSustitucion(int idTecnologia)
        {
            List<CAT_CAPACIDAD_SUSTITUCION> resultado = null;

            using (var r = new Repositorio<CAT_CAPACIDAD_SUSTITUCION>())
            {
                resultado = r.Filtro(me => me.Cve_Tecnologia == idTecnologia);
            }

            return resultado;
        }

        #endregion

        #region Metodos de Insercion

        public static CAT_PRODUCTO InsertProducto(CAT_PRODUCTO producto)
        {
            CAT_PRODUCTO resultado;

            using (var r = new Repositorio<CAT_PRODUCTO>())
            {
                resultado = r.Agregar(producto);
            }

            return resultado;
        }

        public static CAT_MODULOS_SE InsertModulosSe(CAT_MODULOS_SE modulos)
        {
            CAT_MODULOS_SE resultado = null;

            using (var r = new Repositorio<CAT_MODULOS_SE>())
            {
                resultado = r.Agregar(modulos);
            }

            return resultado;
        }

        #endregion

        #region Metodos de Actualizacion

        public static bool ActualizaProducto(CAT_PRODUCTO producto)
        {
            bool actualizo;

            using (var r = new Repositorio<CAT_PRODUCTO>())
            {
                actualizo = r.Actualizar(producto);
            }

            return actualizo;
        }

        public static bool ActualizaModulosSE(CAT_MODULOS_SE modulos)
        {
            bool actualizo = false;

            using (var r = new Repositorio<CAT_MODULOS_SE>())
            {
                actualizo = r.Actualizar(modulos);
            }

            return actualizo;
        }

        #endregion
    }
}
