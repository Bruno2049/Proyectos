using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.CargaMasivaProductos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Utilizables;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class CargaMasivaProductos
    {
        public List<CAT_TECNOLOGIA> ListaTecnologias()
        {
            var lstTec = new CargaProductos().ListaTecnologias();

            lstTec.Insert(0, new CAT_TECNOLOGIA(){Dx_Nombre_General = "Seleccione Tecnología",Cve_Tecnologia = 0});
            return lstTec;
        }

        public List<CAT_FABRICANTE> LstFabricantes()
        {
            return new CargaProductos().ListaFabricantes();
        }

        public List<CAT_CAPACIDAD_SUSTITUCION> ListaCapacidad(int id)
        {
            return new CargaProductos().ListaCapacidad(id);
        }

        public List<CAT_MARCA> ListaMarcas()
        {
            return new CargaProductos().ListaMarcas();
        }

        public List<CAT_TIPO_PRODUCTO> ListaTiposProductos(int id)
        {
            return new CargaProductos().ListaTiposProductos(id);
        }

        public void InsertarProducto(CAT_PRODUCTO producto)
        {
            new CargaProductos().InsertarNuevoProducto(producto);
        }

        public int InsertarProductoReturnID(CAT_PRODUCTO producto)
        {
           return new CargaProductos().InsertarNuevoProductoReturnID(producto);
        }

        public int InsertLogHeader(Load_LogHeader logHeader)
        {
            return new CargaProductos().InsertaLogHeader(logHeader);
        }

        public int InsertLogDetail(Load_LogDetail logDetail)
        {
           return new CargaProductos().InsertaLogDetalle(logDetail);
        }

        public int ActualizaLogHeader(Load_LogHeader logHeader)
        {
            return new CargaProductos().ActualizaLogHeader(logHeader);
        }

        public List<Load_LogDetail> ObtenerDetailsErroresCarga(int idLogHeader)
        {
            return new CargaProductos().ObtenerDetailsErroresCarga(idLogHeader);
        }

        public void InsertaModuloSE(CAT_MODULOS_SE prod)
        {
            new CargaProductos().InsertarModuloSE(prod);
        }

        public Load_LayOut ObtenerNombreArchivoLayOuts(int idTecnologia, int? idclase)
        {
            return new CargaProductos().ObtenerNombreArchivo(idTecnologia, idclase);
        }

        public List<TIPO_ENCAPSULADO> TipoEncapsulado()
        {
            return new CargaProductos().TipoEncapsulado();
        }

        public List<TIPO_PROTECCION_INTERNA> TipoProteccionInterna()
        {
            return new CargaProductos().TipoProteccionInterna();
        }

        public List<TIPO_PROTECCION_EXTERNA> TipoProteccionExterna()
        {
            return new CargaProductos().TipoProteccionExterna();
        }

        public List<MATERIAL_CUBIERTA> MaterialCubiertas()
        {
            return new CargaProductos().MaterialCubiertas();
        }

        public List<PERDIDAS> Perdidas()
        {
            return new CargaProductos().Perdidas();
        }

        public List<PROTECCION_SIN_CORRIENTE> ProteccionSobreCorriente()
        {
            return new CargaProductos().ProteccionSobreCorriente();
        }

        public List<PROTECCION_CONTRA_FUEGO> ProteccionContraFuego()
        {
            return new CargaProductos().ProteccionContraFuego();
        }

        public List<PROTECCION_CONTRA_EXPLOSION> ProteccionContraExplosiones()
        {
            return new CargaProductos().ProteccionContraExplosiones();
        }

        public List<ANCLAJE> Anclaje()
        {
            return new CargaProductos().Anclaje();
        }

        public List<TERMINAL_TIERRA> TerminalTierra()
        {
            return new CargaProductos().TerminalTierra();
        }

        public List<CComboBox> TipoDeControlador()
        {
            return new CargaProductos().TipoDeControlador();
        }

        public List<CComboBox> ComboBinario()
        {
            return new CargaProductos().ComboBinario();
        }

        public List<PROTECCION_GABINETE> ProteccionGabiente()
        {
            return new CargaProductos().ProteccionGabiente();
        }





    }
}
