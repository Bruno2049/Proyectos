using System.Collections.Generic;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos.Catalogos;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class AdministraProducto
    {
        public AdministraProducto()
        {}

        #region Administracion Producto

        public static CAT_PRODUCTO ObtenProducto(int idProducto)
        {
            var producto = AdminProducto.ObtenProducto(idProducto);

            return producto;
        }

        public static CAT_PRODUCTO InsertaProducto(CAT_PRODUCTO producto)
        {
            CAT_PRODUCTO nuevoProducto = null;
            var datosProducto = AdminProducto.InsertProducto(producto);

            if (datosProducto != null)
            {
                nuevoProducto = datosProducto;
            }

            return nuevoProducto;
        }

        public static bool ActualizaProducto(CAT_PRODUCTO producto)
        {
            var actualizo = AdminProducto.ActualizaProducto(producto);
            return actualizo;
        }

        public static CAT_MODULOS_SE ObtenModulosSe(int idProducto)
        {
            var modulosSe = AdminProducto.ObtenModulosSe(idProducto);

            return modulosSe;
        }
        
        public static CAT_MODULOS_SE InsertModulosSe(CAT_MODULOS_SE modulos)
        {
            var inserto = AdminProducto.InsertModulosSe(modulos);

            return inserto;
        }

        public static bool ActualizaModulosSe(CAT_MODULOS_SE modulos)
        {
            var actualizo = AdminProducto.ActualizaModulosSE(modulos);

            return actualizo;
        }

        #endregion

        #region Catalogos

        public static List<CAT_SE_CLASE> ObCatSeClases()
        {
            var resultado = AdminProducto.ObtenCatSeClase();

            return resultado;
        }

        public static List<CAT_SE_TIPO> ObSeTipos(string idClase)
        {
            var resultado = AdminProducto.ObCatSeTipo(idClase);

            return resultado;
        }

        public static List<TIPO_ENCAPSULADO> ObBcTipoEncapsulado()
        {
            var resultado = AdminProducto.ObtenTipoEncapsulado();

            return resultado;
        }

        public static List<TIPO_PROTECCION_INTERNA> ObBcProteccionInterna()
        {
            var resultado = AdminProducto.ObtenTipoProteccionInterna();

            return resultado;
        }

        public static List<TIPO_PROTECCION_EXTERNA> ObBcProteccionExterna()
        {
            var resultado = AdminProducto.ObtenTipoProteccionExterna();

            return resultado;
        }

        public static List<MATERIAL_CUBIERTA> ObBcMaterialCubierta()
        {
            var resultado = AdminProducto.ObtenMaterialCubierta();

            return resultado;
        }

        public static List<PERDIDAS> ObBcPerdidas()
        {
            var resultado = AdminProducto.ObtenPerdidas();

            return resultado;
        }

        public static List<PROTECCION_SIN_CORRIENTE> ObBcProteccionSCorriente()
        {
            var resultado = AdminProducto.ObtenProteccionSCorriente();

            return resultado;
        }

        public static List<PROTECCION_CONTRA_FUEGO> ObBcProteccionContraFuego()
        {
            var resultado = AdminProducto.ObtenProteccionContraFuego();

            return resultado;
        }

        public static List<PROTECCION_CONTRA_EXPLOSION> ObBcProteccionContraExplosion()
        {
            var resultado = AdminProducto.ObtenProteccionContraExplosion();

            return resultado;
        }

        public static List<ANCLAJE> ObBcAnclaje()
        {
            var resultado = AdminProducto.ObtenAnclaje();

            return resultado;
        }

        public static List<TERMINAL_TIERRA> ObBcTerminalTierra()
        {
            var resultado = AdminProducto.ObtenTerminalTierra();

            return resultado;
        }

        public static List<PROTECCION_GABINETE> ObBcProteccionGabinete()
        {
            var resultado = AdminProducto.ObtenProteccionGabinete();

            return resultado;
        }

        public static List<CAT_FABRICANTE> ObtenCatFabricantes()
        {
            var resultado = AdminProducto.ObtenCatFabricantes();

            return resultado;
        }

        public static List<CAT_TIPO_PRODUCTO> ObtenCatTipoProductos(int idTecnologia)
        {
            var resultado = AdminProducto.ObtenCatTipoProductos(idTecnologia);

            return resultado;
        }

        public static List<CAT_CAPACIDAD_SUSTITUCION> ObtenCatCapacidadSustitucion(int idTecnologia)
        {
            var resultado = AdminProducto.ObtenCatCapacidadSust(idTecnologia);

            return resultado;
        }

        public static List<CAT_MARCA> ObtenCatMarcas()
        {
            var resultado = AdminProducto.ObtenCatMarcas();

            return resultado;
        }

        public static List<CAT_POTENCIA> ObtenCatPotencias()
        {
            var resultado = AdminProducto.ObtenCatPotencias();

            return resultado;
        }

   

        #endregion
    }
}
