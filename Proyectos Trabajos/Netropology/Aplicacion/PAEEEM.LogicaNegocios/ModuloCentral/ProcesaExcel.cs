using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.CargaMasivaProductos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.Entidades.Utilizables;
using PAEEEM.Helpers;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class ProcesaExcel
    {
       private List<CAT_FABRICANTE> lstFabricante;
       private List<CAT_MARCA> lstMarcas;
       private List<CAT_CAPACIDAD_SUSTITUCION> lstCapacidad ;
       private List<CAT_TIPO_PRODUCTO> lstTipoProductos;
       private List<CAT_SE_TIPO> lstTipo;
       private List<CAT_SE_TRANSFORM_RELACION> lstRelacion;
        // Banco de Capacitores
        private List<TIPO_ENCAPSULADO>  lstEncapsulados;
        private List<TIPO_PROTECCION_INTERNA> lstTipoProteccionInterna;
        private List<TIPO_PROTECCION_EXTERNA> lstTipoProteccionExterna;
        private List<MATERIAL_CUBIERTA> lstMaterialCubiertas; 
        private List<PERDIDAS> lstPerdidas;
        private List<PROTECCION_SIN_CORRIENTE> lstProteccionSobreCorriente;
        private List<PROTECCION_CONTRA_FUEGO> lstProteccionContraFuego;
        private List<PROTECCION_CONTRA_EXPLOSION> lstProteccionContraExplosiones;
        private List<ANCLAJE> lstAnclaje;
        private List<TERMINAL_TIERRA> lstTerminalTierra;
        private List<CComboBox> lstTipoDeControlador;
        private List<CComboBox> lstComboBinario;
        public List<PROTECCION_GABINETE> lstProteccionGabiente;

        public ProcesaExcel(int idTecnologia)
        {
            var bll = new CargaProductos();
            lstFabricante = bll.ListaFabricantes();
            lstMarcas = bll.ListaMarcas();
            lstCapacidad = bll.ListaCapacidad(idTecnologia);
            if (idTecnologia == 5)
            {
              lstTipo = bll.ListaTipos();
              lstRelacion = bll.ListaRelacion();
            }
            else if (idTecnologia == 7)
            {
                lstTipoProductos = bll.ListaTiposProductos(idTecnologia);
                lstEncapsulados = bll.TipoEncapsulado();
                lstTipoProteccionInterna = bll.TipoProteccionInterna();
                lstTipoProteccionExterna = bll.TipoProteccionExterna();
                lstMaterialCubiertas = bll.MaterialCubiertas();
                lstPerdidas = bll.Perdidas();
                lstProteccionSobreCorriente = bll.ProteccionSobreCorriente();
                lstProteccionContraFuego = bll.ProteccionContraFuego();
                lstProteccionContraExplosiones = bll.ProteccionContraExplosiones();
                lstAnclaje = bll.Anclaje();
                lstTerminalTierra = bll.TerminalTierra();
                lstTipoDeControlador = bll.TipoDeControlador();
                lstComboBinario = bll.ComboBinario();
                lstProteccionGabiente = bll.ProteccionGabiente();

            }
            else lstTipoProductos = bll.ListaTiposProductos(idTecnologia);
        }

        public CAT_PRODUCTO ValidaRegistro(LayOutExcel row, string idTecnologia, int idLogHeader)
        {

            var newProduct = new CAT_PRODUCTO();
            var lstError = new List<Load_LogDetail>();
            bool flagSinError = true;
            
            var idFabricante = lstFabricante.FirstOrDefault(f => f.Dx_Nombre_Fabricante.Equals(row.Fabricante.ToUpper()));
            if (idFabricante != null) newProduct.Cve_Fabricante = idFabricante.Cve_Fabricante;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                             {
                                 LogHeaderId = idLogHeader,
                                 NoRegistro = int.Parse(row.NoRegistro),
                                 Campo = "Fabricante",
                                 Error = "No existe en catalogo"
                             });
            }

            var idTipo = lstTipoProductos.FirstOrDefault(t => t.Dx_Tipo_Producto.Equals(row.TipoProducto.ToUpper()));
           if (idTipo != null) newProduct.Ft_Tipo_Producto = idTipo.Ft_Tipo_Producto;
           else
           {
               flagSinError = false;
               lstError.Add(new Load_LogDetail()
               {
                   LogHeaderId = idLogHeader,
                   NoRegistro = int.Parse(row.NoRegistro),
                   Campo = "Tipo de Producto",
                   Error = "No existe en catalogo"
               });
           }
           if (!row.NombreProducto.Equals(string.Empty)) newProduct.Dx_Nombre_Producto = row.NombreProducto.ToUpper();
           else
           {
               flagSinError = false;
               lstError.Add(new Load_LogDetail()
               {
                   LogHeaderId = idLogHeader,
                   NoRegistro = int.Parse(row.NoRegistro),
                   Campo = "Nombre de Producto",
                   Error = "Campo vacio"
               });
           }

           var idMarca = lstMarcas.FirstOrDefault(t => t.Dx_Marca.Equals(row.Marca.ToUpper()));
           if (idMarca != null) newProduct.Cve_Marca = idMarca.Cve_Marca;
           else
           {
               flagSinError = false;
               lstError.Add(new Load_LogDetail()
               {
                   LogHeaderId = idLogHeader,
                   NoRegistro = int.Parse(row.NoRegistro),
                   Campo = "Marca",
                   Error = "No existe en catalogo"
               });
           }

           if (!row.Modelo.Equals(string.Empty)) newProduct.Dx_Modelo_Producto = row.Modelo.ToUpper();
           else
           {
               flagSinError = false;
               lstError.Add(new Load_LogDetail()
               {
                   LogHeaderId = idLogHeader,
                   NoRegistro = int.Parse(row.NoRegistro),
                   Campo = "Modelo",
                   Error = "No existe en catalogo"
               });
           }
           
           var precio = ObtenerNumero(row.PrecioMaximo);
           if (precio > 0) newProduct.Mt_Precio_Max = precio;
           else
           {
               flagSinError = false;
               lstError.Add(new Load_LogDetail()
               {
                   LogHeaderId = idLogHeader,
                   NoRegistro = int.Parse(row.NoRegistro),
                   Campo = "Precio Maximo",
                   Error = "Valor igual o menor a cero"
               });
           }

            double intCapacidad = double.Parse(ObtenerNumero(row.Capacidad).ToString(CultureInfo.InvariantCulture));
            var idCapacidad = lstCapacidad.FirstOrDefault(t => t.No_Capacidad == intCapacidad);
           if (idCapacidad != null) newProduct.Cve_Capacidad_Sust = idCapacidad.Cve_Capacidad_Sust;
           else
           {
               flagSinError = false;
               lstError.Add(new Load_LogDetail()
               {
                   LogHeaderId = idLogHeader,
                   NoRegistro = int.Parse(row.NoRegistro),
                   Campo = "Capacidad",
                   Error = "No existe en catalogo"
               });
           }

           newProduct.Cve_Tecnologia = int.Parse(idTecnologia);
           bool hayCoincidencia = new CargaProductos().NoExisteProducto(newProduct);
               if (hayCoincidencia)
               {
                       flagSinError = false;
                       lstError.Add(new Load_LogDetail()
                                 {
                                     LogHeaderId = idLogHeader,
                                     NoRegistro = int.Parse(row.NoRegistro),
                                     Campo = "Todos",
                                     Error = "El Producto ya existe en la Base de Datos."
                                 });
                }

                if (flagSinError)
                {
                    newProduct.Cve_Estatus_Producto = 1;
                    newProduct.Dt_Fecha_Producto = DateTime.Now;
                    
                }
                else
                {
                    foreach (var loadLogDetail in lstError)
                    {
                        int rowinsert = new CargaProductos().InsertaLogDetalle(loadLogDetail);
                    }
                }
            return flagSinError ? newProduct : null;
        }
        
        public CAT_PRODUCTO ValidaRegistroSubEstacionAerea(LayOutExcel row, string idTecnologia, int idLogHeader)
        {

            var newProduct = new CAT_PRODUCTO();
            var lstError = new List<Load_LogDetail>();
            bool flagSinError = true;

            var idFabricante = lstFabricante.FirstOrDefault(f => f.Dx_Nombre_Fabricante.Equals(row.Fabricante.ToUpper()));
            if (idFabricante != null) newProduct.Cve_Fabricante = idFabricante.Cve_Fabricante;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Fabricante",
                    Error = "No existe en catalogo"
                });
            }

            if (!row.Modelo.Equals(string.Empty)) newProduct.Dx_Modelo_Producto = row.Modelo.ToUpper();
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Modelo",
                    Error = "No existe en catalogo"
                });
            }


            var idTipo = lstTipo.FirstOrDefault(t => t.Dx_Nombre_Tipo.Equals(row.TipoProducto.ToUpper()));
            if (idTipo != null) newProduct.Cve_Tipo_SE = idTipo.Cve_Tipo;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Tipo",
                    Error = "No existe en catalogo"
                });
            }

            var idMarca = lstMarcas.FirstOrDefault(t => t.Dx_Marca.Equals(row.Marca.ToUpper()));
            if (idMarca != null) newProduct.Cve_Marca = idMarca.Cve_Marca;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Marca",
                    Error = "No existe en catalogo"
                });
            }

            double intCapacidad = double.Parse(ObtenerNumero(row.Capacidad).ToString(CultureInfo.InvariantCulture));
            var idCapacidad = lstCapacidad.FirstOrDefault(t => t.No_Capacidad == intCapacidad);
            if (idCapacidad != null) newProduct.Cve_Capacidad_Sust = idCapacidad.Cve_Capacidad_Sust;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Capacidad",
                    Error = "No existe en catalogo"
                });
            }

            var PrecioTotalSubestacion = ObtenerNumero(row.PrecioTotalSubestacion);
            if (PrecioTotalSubestacion > 0) newProduct.Precio_Total_SE = PrecioTotalSubestacion;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Subestación",
                    Error = "Valor igual o menor a cero"
                });
            }
            
            newProduct.Cve_Tecnologia = int.Parse(idTecnologia);
            bool hayCoincidencia = new CargaProductos().NoExisteProductoSubEstacion(newProduct);
            if (hayCoincidencia)
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Todos",
                    Error = "El Producto ya existe en la Base de Datos."
                });
            }



            if (flagSinError)
            {
                newProduct.Dx_Nombre_Producto = "SUBESTACION ELECTRICA";
                newProduct.Ft_Tipo_Producto = 11;
                newProduct.Cve_Clase_SE = 1;
                newProduct.Cve_Estatus_Producto = 1;
                newProduct.Dt_Fecha_Producto = DateTime.Now;
            }
            else
            {
                foreach (var loadLogDetail in lstError)
                {
                    int rowinsert = new CargaProductos().InsertaLogDetalle(loadLogDetail);
                }
            }
            return flagSinError ? newProduct : null;
        }
        public CAT_MODULOS_SE ValidaRegistroSubEstacionAerea_SE(LayOutExcel row,int idLogHeader)
        {
            var newProduct = new CAT_MODULOS_SE();
            var lstError = new List<Load_LogDetail>();
            bool flagSinError = true;

            var idMarca = lstMarcas.FirstOrDefault(t => t.Dx_Marca.Equals(row.Marca.ToUpper()));
            if (idMarca != null) newProduct.Cve_Marca_Transformador = idMarca.Cve_Marca;
            
         
            double intCapacidad = double.Parse(ObtenerNumero(row.Capacidad).ToString(CultureInfo.InvariantCulture));
            var idCapacidad = lstCapacidad.FirstOrDefault(t => t.No_Capacidad == intCapacidad);
            if (idCapacidad != null) newProduct.Cve_Capacidad_Transformador = idCapacidad.Cve_Capacidad_Sust;
            

            var idRelacion = lstRelacion.FirstOrDefault(t => t.Dx_Dsc_Relacion.Equals(row.RelacionTrans.ToUpper()));
            if (idRelacion != null) newProduct.Cve_Relacion = idRelacion.Cve_Relacion;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Relación Transformación",
                    Error = "No existe en catalogo"
                });
            }

            var precio = ObtenerNumero(row.Precio);
            if (precio > 0) newProduct.Precio_Transformador = precio;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioTotalModTransformador = ObtenerNumero(row.PrecioTotalModTransformador);
            if (PrecioTotalModTransformador > 0) newProduct.Precio_Modulo_Transformador = PrecioTotalModTransformador;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Modulo de Transformador",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioT = ObtenerNumero(row.PrecioConectorT);
            if (precioT > 0) newProduct.Precio_Conectores_T = precioT;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio conectores T",
                    Error = "Valor igual o menor a cero"
                });
            }
            
            var precioAisladTen = ObtenerNumero(row.PrecioAisladorTension);
            if (precioAisladTen > 0) newProduct.Precio_Aisladores_Tension = precioAisladTen;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Aisladores Tensión",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioAisladSop = ObtenerNumero(row.PrecioAisladorSoporte);
            if (precioAisladSop > 0) newProduct.Precio_Aisladores_Soporte = precioAisladSop;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Aisladores Soporte",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioCGCLA = ObtenerNumero(row.PrecioCabGuiaApatarayos);
            if (precioCGCLA > 0) newProduct.Precio_Cable_Apartarrayos = precioCGCLA;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Cable Guía de Conexión de Línea a Apartarrayos",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioCGCCS= ObtenerNumero(row.PrecioCabGuiaCortaCto);
            if (precioCGCCS > 0) newProduct.Precio_Cable_Corta_Circuito = precioCGCCS;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Cable Guía de Conexión de Línea a Corta circuito",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioCGCLT = ObtenerNumero(row.PrecioCabGuiaTransform);
            if (precioCGCLT > 0) newProduct.Precio_Cable_Transformador = precioCGCLT;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Cable Guía de Conexión de Línea a Transformador",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioApartaRayo = ObtenerNumero(row.PrecioApartarrayos);
            if (precioApartaRayo > 0) newProduct.Precio_Apartarrayo = precioApartaRayo;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Apartarrayo",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioCortoCto = ObtenerNumero(row.PrecioCortaCto);
            if (precioCortoCto > 0) newProduct.Precio_Corta_Circuito = precioCortoCto;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Corta Circuito",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioFusible = ObtenerNumero(row.PrecioFusible);
            if (precioFusible > 0) newProduct.Precio_Fusible = precioFusible;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Fusible",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioTotalModMediaTension = ObtenerNumero(row.PrecioTotalModMediaTension);
            if (PrecioTotalModMediaTension > 0) newProduct.Precio_Modulo_Media_Tension = PrecioTotalModMediaTension;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Modulo de Media Tensión",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioCabTREqMT = ObtenerNumero(row.PrecioCabTREqMT);
            if (PrecioCabTREqMT > 0) newProduct.Precio_Cable_TR= PrecioCabTREqMT;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Cable de conexión de TR",
                    Error = "Valor igual o menor a cero"
                });
            }


            var PrecioHerrajes = ObtenerNumero(row.PrecioHerrajes);
            if (PrecioHerrajes > 0) newProduct.Precio_Herrajes_Instalacion = PrecioHerrajes;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Herrajes para la instalación",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioPoste = ObtenerNumero(row.PrecioPoste);
            if (PrecioPoste > 0) newProduct.Precio_Poste = PrecioPoste;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Poste",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioSistemaTierra = ObtenerNumero(row.PrecioSistemaTierra);
            if (PrecioSistemaTierra > 0) newProduct.Precio_Sistemas_Tierra = PrecioSistemaTierra;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Sistema(s) de Tierra",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioCabConST = ObtenerNumero(row.PrecioCabConST);
            if (PrecioSistemaTierra > 0) newProduct.Precio_Cable_Sistemas_Tierra = PrecioCabConST;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Cable de conexión a sistema a tierra",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioTotalModAcceYProt = ObtenerNumero(row.PrecioTotalModAcceYProt);
            if (PrecioTotalModAcceYProt > 0) newProduct.Precio_Modulo_Acce_Prot = PrecioTotalModAcceYProt;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Módulo Accesorios y Protecciones",
                    Error = "Valor igual o menor a cero"
                });
            }

            if (!flagSinError)
            {
                foreach (var loadLogDetail in lstError)
                {
                    int rowinsert = new CargaProductos().InsertaLogDetalle(loadLogDetail);
                }
            }
            return flagSinError ? newProduct : null;
        }

        public CAT_PRODUCTO ValidaRegistroSubEstacionAcometida(LayOutExcel row, string idTecnologia, int idLogHeader)
        {

            var newProduct = new CAT_PRODUCTO();
            var lstError = new List<Load_LogDetail>();
            bool flagSinError = true;

            var idFabricante = lstFabricante.FirstOrDefault(f => f.Dx_Nombre_Fabricante.Equals(row.Fabricante.ToUpper()));
            if (idFabricante != null) newProduct.Cve_Fabricante = idFabricante.Cve_Fabricante;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Fabricante",
                    Error = "No existe en catalogo"
                });
            }

            if (!row.Modelo.Equals(string.Empty)) newProduct.Dx_Modelo_Producto = row.Modelo.ToUpper();
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Modelo",
                    Error = "No existe en catalogo"
                });
            }

            var idMarca = lstMarcas.FirstOrDefault(t => t.Dx_Marca.Equals(row.Marca.ToUpper()));
            if (idMarca != null) newProduct.Cve_Marca = idMarca.Cve_Marca;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Marca",
                    Error = "No existe en catalogo"
                });
            }

            double intCapacidad = double.Parse(ObtenerNumero(row.Capacidad).ToString(CultureInfo.InvariantCulture));
            var idCapacidad = lstCapacidad.FirstOrDefault(t => t.No_Capacidad == intCapacidad);
            if (idCapacidad != null) newProduct.Cve_Capacidad_Sust = idCapacidad.Cve_Capacidad_Sust;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Capacidad",
                    Error = "No existe en catalogo"
                });
            }

            var PrecioTotalSubestacion = ObtenerNumero(row.PrecioTotalSubestacion);
            if (PrecioTotalSubestacion > 0) newProduct.Precio_Total_SE = PrecioTotalSubestacion;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Subestación",
                    Error = "Valor igual o menor a cero"
                });
            }

            newProduct.Cve_Tipo_SE = 10;
            newProduct.Cve_Tecnologia = int.Parse(idTecnologia);
            bool hayCoincidencia = new CargaProductos().NoExisteProductoSubEstacion(newProduct);
            if (hayCoincidencia)
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Todos",
                    Error = "El Producto ya existe en la Base de Datos."
                });
            }



            if (flagSinError)
            {
                newProduct.Cve_Tipo_SE = 10;
                newProduct.Dx_Nombre_Producto = "SUBESTACION ELECTRICA";
                newProduct.Ft_Tipo_Producto = 11;
                newProduct.Cve_Clase_SE = 2;
                newProduct.Cve_Estatus_Producto = 1;
                newProduct.Dt_Fecha_Producto = DateTime.Now;
            }
            else
            {
                foreach (var loadLogDetail in lstError)
                {
                    int rowinsert = new CargaProductos().InsertaLogDetalle(loadLogDetail);
                }
            }
            return flagSinError ? newProduct : null;
        }
        public CAT_MODULOS_SE ValidaRegistroSubEstacionAcometida_SE(LayOutExcel row, int idLogHeader)
        {
            var newProduct = new CAT_MODULOS_SE();
            var lstError = new List<Load_LogDetail>();
            bool flagSinError = true;

            var idMarca = lstMarcas.FirstOrDefault(t => t.Dx_Marca.Equals(row.Marca.ToUpper()));
            if (idMarca != null) newProduct.Cve_Marca_Transformador = idMarca.Cve_Marca;
            
            
            double intCapacidad = double.Parse(ObtenerNumero(row.Capacidad).ToString(CultureInfo.InvariantCulture));
            var idCapacidad = lstCapacidad.FirstOrDefault(t => t.No_Capacidad == intCapacidad);
            if (idCapacidad != null) newProduct.Cve_Capacidad_Transformador = idCapacidad.Cve_Capacidad_Sust;
           
            var idRelacion = lstRelacion.FirstOrDefault(t => t.Dx_Dsc_Relacion.Equals(row.RelacionTrans.ToUpper()));
            if (idRelacion != null) newProduct.Cve_Relacion = idRelacion.Cve_Relacion;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Relación Transformación",
                    Error = "No existe en catalogo"
                });
            }

            var precio = ObtenerNumero(row.Precio);
            if (precio > 0) newProduct.Precio_Transformador = precio;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioFusibleMT = ObtenerNumero(row.PrecioFusibleMT);
            if (PrecioFusibleMT > 0) newProduct.Precio_Fusible_Subestacion = PrecioFusibleMT;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Fusible de Subestación en Media Tensión",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioTotalModTransformador = ObtenerNumero(row.PrecioTotalModTransformador);
            if (PrecioTotalModTransformador > 0) newProduct.Precio_Modulo_Transformador = PrecioTotalModTransformador;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Modulo de Transformador",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioT = ObtenerNumero(row.PrecioConectorT);
            if (precioT > 0) newProduct.Precio_Conectores_T = precioT;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio conectores T",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioAisladTen = ObtenerNumero(row.PrecioAisladorTension);
            if (precioAisladTen > 0) newProduct.Precio_Aisladores_Tension = precioAisladTen;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Aisladores Tensión",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioAisladSop = ObtenerNumero(row.PrecioAisladorSoporte);
            if (precioAisladSop > 0) newProduct.Precio_Aisladores_Soporte = precioAisladSop;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Aisladores Soporte",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioCGCLA = ObtenerNumero(row.PrecioCabGuiaApatarayos);
            if (precioCGCLA > 0) newProduct.Precio_Cable_Apartarrayos = precioCGCLA;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Cable Guía de Conexión de Línea a Apartarrayos",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioCGCCS = ObtenerNumero(row.PrecioCabGuiaCortaCto);
            if (precioCGCCS > 0) newProduct.Precio_Cable_Corta_Circuito = precioCGCCS;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Cable Guía de Conexión de Línea a Corta circuito",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioCGCLT = ObtenerNumero(row.PrecioCabGuiaTransform);
            if (precioCGCLT > 0) newProduct.Precio_Cable_Transformador = precioCGCLT;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Cable Guía de Conexión de Línea a Transformador",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioApartaRayo = ObtenerNumero(row.PrecioApartarrayos);
            if (precioApartaRayo > 0) newProduct.Precio_Apartarrayo = precioApartaRayo;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Apartarrayo",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioCortoCto = ObtenerNumero(row.PrecioCortaCto);
            if (precioCortoCto > 0) newProduct.Precio_Corta_Circuito = precioCortoCto;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Corta Circuito",
                    Error = "Valor igual o menor a cero"
                });
            }

            var precioFusible = ObtenerNumero(row.PrecioFusible);
            if (precioFusible > 0) newProduct.Precio_Fusible = precioFusible;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Fusible",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioCabGuiaConAcometida = ObtenerNumero(row.PrecioCabGuiaConAcometida);
            if (PrecioCabGuiaConAcometida > 0) newProduct.Precio_Cable_Term_Acometida = PrecioCabGuiaConAcometida;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Cable Guía de Conexión de Línea a Terminales de Acometida",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioConexMTAcometida = ObtenerNumero(row.PrecioConexMTAcometida);
            if (PrecioConexMTAcometida > 0) newProduct.Precio_Cable_SubTerraneo = PrecioConexMTAcometida;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Cable de conexión de media tensión subterráneo (acometida)",
                    Error = "Valor igual o menor a cero"
                });
            }
            
            var PrecioTotalModMediaTension = ObtenerNumero(row.PrecioTotalModMediaTension);
            if (PrecioTotalModMediaTension > 0) newProduct.Precio_Modulo_Media_Tension = PrecioTotalModMediaTension;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Modulo de Media Tensión",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioCabTREqMT = ObtenerNumero(row.PrecioCabTREqMT);
            if (PrecioCabTREqMT > 0) newProduct.Precio_Cable_TR = PrecioCabTREqMT;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Cable de conexión de TR",
                    Error = "Valor igual o menor a cero"
                });
            }


            var PrecioHerrajes = ObtenerNumero(row.PrecioHerrajes);
            if (PrecioHerrajes > 0) newProduct.Precio_Herrajes_Instalacion = PrecioHerrajes;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Herrajes para la instalación",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioPoste = ObtenerNumero(row.PrecioPoste);
            if (PrecioPoste > 0) newProduct.Precio_Poste = PrecioPoste;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Poste",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioSistemaTierra = ObtenerNumero(row.PrecioSistemaTierra);
            if (PrecioSistemaTierra > 0) newProduct.Precio_Sistemas_Tierra = PrecioSistemaTierra;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Sistema(s) de Tierra",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioCabConST = ObtenerNumero(row.PrecioCabConST);
            if (PrecioSistemaTierra > 0) newProduct.Precio_Cable_Sistemas_Tierra = PrecioCabConST;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Cable de conexión a sistema a tierra",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioTotalModAcceYProt = ObtenerNumero(row.PrecioTotalModAcceYProt);
            if (PrecioTotalModAcceYProt > 0) newProduct.Precio_Modulo_Acce_Prot = PrecioTotalModAcceYProt;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Módulo Accesorios y Protecciones",
                    Error = "Valor igual o menor a cero"
                });
            }

            if (!flagSinError)
            {
                foreach (var loadLogDetail in lstError)
                {
                    int rowinsert = new CargaProductos().InsertaLogDetalle(loadLogDetail);
                }
            }
            return flagSinError ? newProduct : null;
        }

        public CAT_PRODUCTO ValidaRegistroSubEstacionIntRed(LayOutExcel row, string idTecnologia, int idLogHeader)
        {

            var newProduct = new CAT_PRODUCTO();
            var lstError = new List<Load_LogDetail>();
            bool flagSinError = true;

            var idFabricante = lstFabricante.FirstOrDefault(f => f.Dx_Nombre_Fabricante.Equals(row.Fabricante.ToUpper()));
            if (idFabricante != null) newProduct.Cve_Fabricante = idFabricante.Cve_Fabricante;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Fabricante",
                    Error = "No existe en catalogo"
                });
            }

            if (!row.Modelo.Equals(string.Empty)) newProduct.Dx_Modelo_Producto = row.Modelo.ToUpper();
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Modelo",
                    Error = "No existe en catalogo"
                });
            }

            var idMarca = lstMarcas.FirstOrDefault(t => t.Dx_Marca.Equals(row.Marca.ToUpper()));
            if (idMarca != null) newProduct.Cve_Marca = idMarca.Cve_Marca;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Marca",
                    Error = "No existe en catalogo"
                });
            }

            double intCapacidad = double.Parse(ObtenerNumero(row.Capacidad).ToString(CultureInfo.InvariantCulture));
            var idCapacidad = lstCapacidad.FirstOrDefault(t => t.No_Capacidad == intCapacidad);
            if (idCapacidad != null) newProduct.Cve_Capacidad_Sust = idCapacidad.Cve_Capacidad_Sust;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Capacidad",
                    Error = "No existe en catalogo"
                });
            }

            var PrecioTotalSubestacion = ObtenerNumero(row.PrecioTotalSubestacion);
            if (PrecioTotalSubestacion > 0) newProduct.Precio_Total_SE = PrecioTotalSubestacion;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Subestación",
                    Error = "Valor igual o menor a cero"
                });
            }

            newProduct.Cve_Tipo_SE = 10;
            newProduct.Cve_Tecnologia = int.Parse(idTecnologia);
            bool hayCoincidencia = new CargaProductos().NoExisteProductoSubEstacion(newProduct);
            if (hayCoincidencia)
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Todos",
                    Error = "El Producto ya existe en la Base de Datos."
                });
            }

            if (flagSinError)
            {
                newProduct.Dx_Nombre_Producto = "SUBESTACION ELECTRICA";
                newProduct.Ft_Tipo_Producto = 11;
                newProduct.Cve_Clase_SE = 2;
                newProduct.Cve_Estatus_Producto = 1;
                newProduct.Dt_Fecha_Producto = DateTime.Now;
            }
            else
            {
                foreach (var loadLogDetail in lstError)
                {
                    int rowinsert = new CargaProductos().InsertaLogDetalle(loadLogDetail);
                }
            }
            return flagSinError ? newProduct : null;
        }
        public CAT_MODULOS_SE ValidaRegistroSubEstacionIntRed_SE(LayOutExcel row, int idLogHeader)
        {
            var newProduct = new CAT_MODULOS_SE();
            var lstError = new List<Load_LogDetail>();
            bool flagSinError = true;

            var idMarca = lstMarcas.FirstOrDefault(t => t.Dx_Marca.Equals(row.Marca.ToUpper()));
            if (idMarca != null) newProduct.Cve_Marca_Transformador = idMarca.Cve_Marca;


            double intCapacidad = double.Parse(ObtenerNumero(row.Capacidad).ToString(CultureInfo.InvariantCulture));
            var idCapacidad = lstCapacidad.FirstOrDefault(t => t.No_Capacidad == intCapacidad);
            if (idCapacidad != null) newProduct.Cve_Capacidad_Transformador = idCapacidad.Cve_Capacidad_Sust;

            var idRelacion = lstRelacion.FirstOrDefault(t => t.Dx_Dsc_Relacion.Equals(row.RelacionTrans.ToUpper()));
            if (idRelacion != null) newProduct.Cve_Relacion = idRelacion.Cve_Relacion;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Relación Transformación",
                    Error = "No existe en catalogo"
                });
            }

            var precio = ObtenerNumero(row.Precio);
            if (precio > 0) newProduct.Precio_Transformador = precio;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioFusibleMT = ObtenerNumero(row.PrecioFusibleMT);
            if (PrecioFusibleMT > 0) newProduct.Precio_Fusible_Subestacion = PrecioFusibleMT;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Fusible de Subestación en Media Tensión",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioTotalModTransformador = ObtenerNumero(row.PrecioTotalModTransformador);
            if (PrecioTotalModTransformador > 0) newProduct.Precio_Modulo_Transformador = PrecioTotalModTransformador;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Modulo de Transformador",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioEmpalmes = ObtenerNumero(row.PrecioEmpalmes);
            if (PrecioEmpalmes > 0) newProduct.Precio_Empalmes = PrecioEmpalmes;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Empalmes para cables de Media Tensión",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioExtremos = ObtenerNumero(row.PrecioExtremos);
            if (PrecioExtremos > 0) newProduct.Precio_Cable_Ambos_Extremos = PrecioExtremos;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Cable de Conexión Subterranéo para ambos extremos",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioTotalModMediaTension = ObtenerNumero(row.PrecioTotalModMediaTension);
            if (PrecioTotalModMediaTension > 0) newProduct.Precio_Modulo_Media_Tension = PrecioTotalModMediaTension;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Modulo de Media Tensión",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioCabTREqMT = ObtenerNumero(row.PrecioCabTREqMT);
            if (PrecioCabTREqMT > 0) newProduct.Precio_Cable_TR = PrecioCabTREqMT;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Cable de conexión de TR",
                    Error = "Valor igual o menor a cero"
                });
            }


            var PrecioHerrajes = ObtenerNumero(row.PrecioHerrajes);
            if (PrecioHerrajes > 0) newProduct.Precio_Herrajes_Instalacion = PrecioHerrajes;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Herrajes para la instalación",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioPoste = ObtenerNumero(row.PrecioPoste);
            if (PrecioPoste > 0) newProduct.Precio_Poste = PrecioPoste;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Poste",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioSistemaTierra = ObtenerNumero(row.PrecioSistemaTierra);
            if (PrecioSistemaTierra > 0) newProduct.Precio_Sistemas_Tierra = PrecioSistemaTierra;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Sistema(s) de Tierra",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioCabConST = ObtenerNumero(row.PrecioCabConST);
            if (PrecioSistemaTierra > 0) newProduct.Precio_Cable_Sistemas_Tierra = PrecioCabConST;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Cable de conexión a sistema a tierra",
                    Error = "Valor igual o menor a cero"
                });
            }

            var PrecioTotalModAcceYProt = ObtenerNumero(row.PrecioTotalModAcceYProt);
            if (PrecioTotalModAcceYProt > 0) newProduct.Precio_Modulo_Acce_Prot = PrecioTotalModAcceYProt;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Total Módulo Accesorios y Protecciones",
                    Error = "Valor igual o menor a cero"
                });
            }

            if (!flagSinError)
            {
                foreach (var loadLogDetail in lstError)
                {
                    int rowinsert = new CargaProductos().InsertaLogDetalle(loadLogDetail);
                }
            }
            return flagSinError ? newProduct : null;
        }

        public CAT_PRODUCTO ValidaRegistroBancoCapacitores(LayOutExcel row, string idTecnologia, int idLogHeader)
        {

            var newProduct = new CAT_PRODUCTO();
            var lstError = new List<Load_LogDetail>();
            bool flagSinError = true;

            var idFabricante = lstFabricante.FirstOrDefault(f => f.Dx_Nombre_Fabricante.Equals(row.Fabricante.ToUpper()));
            if (idFabricante != null) newProduct.Cve_Fabricante = idFabricante.Cve_Fabricante;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Fabricante",
                    Error = "No existe en catalogo"
                });
            }

            var idTipo = lstTipoProductos.FirstOrDefault(t => t.Dx_Tipo_Producto.Equals(row.TipoProducto.ToUpper()));
            if (idTipo != null) newProduct.Ft_Tipo_Producto = idTipo.Ft_Tipo_Producto;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Tipo de Producto",
                    Error = "No existe en catalogo"
                });
            }
           

            var idMarca = lstMarcas.FirstOrDefault(t => t.Dx_Marca.Equals(row.Marca.ToUpper()));
            if (idMarca != null) newProduct.Cve_Marca = idMarca.Cve_Marca;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Marca",
                    Error = "No existe en catalogo"
                });
            }

            if (!row.Modelo.Equals(string.Empty)) newProduct.Dx_Modelo_Producto = row.Modelo.ToUpper();
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Modelo",
                    Error = "No existe en catalogo"
                });
            }

            var precio = ObtenerNumero(row.PrecioMaximo);
            if (precio > 0) newProduct.Mt_Precio_Max = precio;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Precio Maximo",
                    Error = "Valor igual o menor a cero"
                });
            }

            double intCapacidad = double.Parse(ObtenerNumero(row.Capacidad).ToString(CultureInfo.InvariantCulture));
            var idCapacidad = lstCapacidad.FirstOrDefault(t => t.No_Capacidad == intCapacidad);
            if (idCapacidad != null) newProduct.Cve_Capacidad_Sust = idCapacidad.Cve_Capacidad_Sust;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Capacidad",
                    Error = "No existe en catalogo"
                });
            }


            var idEncapsulado = lstEncapsulados.FirstOrDefault(t => t.Descripcion.Equals(row.TipoEncapsulado.ToUpper()));
            if (idEncapsulado != null) newProduct.Cve_Tipo = idEncapsulado.Cve_Tipo;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Tipo de Encapsulado",
                    Error = "No existe en catalogo"
                });
            }

            if (!row.TipoDielectrico.Equals(string.Empty)) newProduct.Dx_Tipo_Dielectrico = row.TipoDielectrico.ToUpper();
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Tipo de dieléctrico",
                    Error = "No existe en catalogo"
                });
            }


            var idProteccionInterna = lstTipoProteccionInterna.FirstOrDefault(t => t.Descripcion.Equals(row.IncluyeProteccionInterna.ToUpper()));
            if (idProteccionInterna != null) newProduct.Cve_Proteccion_Interna = idProteccionInterna.Cve_Proteccion_Interna;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Incluye protección interna",
                    Error = "No existe en catalogo"
                });
            }

            var idProteccionExterna = lstTipoProteccionExterna.FirstOrDefault(t => t.Descripcion.Equals(row.TipoProteccionExterna.ToUpper()));
            if (idProteccionExterna != null) newProduct.Cve_Proteccion_Externa = idProteccionExterna.Cve_Proteccion_Externa;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Tipo de Protección externa",
                    Error = "No existe en catalogo"
                });
            }

            var idMaterial = lstMaterialCubiertas.FirstOrDefault(t => t.Descripcion.Equals(row.MaterialCubierta.ToUpper()));
            if (idMaterial != null) newProduct.Cve_Material = idMaterial.Cve_Material;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Material de Cubierta ",
                    Error = "No existe en catalogo"
                });
            }

            var idPerdidas = lstPerdidas.FirstOrDefault(t => t.Descripcion.Equals(row.Perdidas));
            if (idPerdidas != null) newProduct.Cve_Perdidas = idPerdidas.Cve_Perdidas;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Pérdidas",
                    Error = "No existe en catalogo"
                });
            }

            var idProteccionInternaSC = lstProteccionSobreCorriente.FirstOrDefault(t => t.Descripcion.Equals(row.ProteccionInternaSobrecorriente.ToUpper()));
            if (idProteccionInternaSC != null) newProduct.Cve_Proteccion = idProteccionInternaSC.Cve_Proteccion;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Protección interna de sobreccorriente",
                    Error = "No existe en catalogo"
                });
            }

            var idProteccionContraFuego = lstProteccionContraFuego.FirstOrDefault(t => t.Descripcion.Equals(row.ProteccionVsFuego.ToUpper()));
            if (idProteccionContraFuego != null) newProduct.Cve_Prot_Contra_Fuego = idProteccionContraFuego.Cve_Prot_Contra_Fuego;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Protección vs Fuego",
                    Error = "No existe en catalogo"
                });
            }

            var idProteccionVsExplosion = lstProteccionContraExplosiones.FirstOrDefault(t => t.Descripcion.Equals(row.ProteccionVsExplosion.ToUpper()));
            if (idProteccionVsExplosion != null) newProduct.Cve_Protec_Contra_Exp = idProteccionVsExplosion.Cve_Protec_Contra_Exp;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Protección vs Explosión",
                    Error = "No existe en catalogo"
                });
            }

            var idAnclaje = lstAnclaje.FirstOrDefault(t => t.Descripcion.Equals(row.Anclaje.ToUpper()));
            if (idAnclaje != null) newProduct.Cve_Anclaje = idAnclaje.Cve_Anclaje;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Anclaje",
                    Error = "No existe en catalogo"
                });
            }

            var idTerminalTierra = lstTerminalTierra.FirstOrDefault(t => t.Descripcion.Equals(row.TerminalTierra.ToUpper()));
            if (idTerminalTierra != null) newProduct.Cve_Terminal_Tierra = idTerminalTierra.Cve_Terminal_Tierra;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Terminal de tierra",
                    Error = "No existe en catalogo"
                });
            }

            var idTipoControlador = lstTipoDeControlador.FirstOrDefault(t => t.Elemento.Equals(row.TipoControlador.ToUpper()));
            if (idTipoControlador != null) newProduct.Cve_Tipo_Controlador = idTipoControlador.IdElemento;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Tipo de Controlador ",
                    Error = "No existe en catalogo"
                });
            }

            var ProteccionVsSobreCorriente = lstComboBinario.FirstOrDefault(t => t.Elemento.Equals(row.ProteccionVsSobreCorriente.ToUpper()));
            if (ProteccionVsSobreCorriente != null) newProduct.Cve_P_Corriente_Controlador = ProteccionVsSobreCorriente.IdElemento;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Protección vs Sobrecorriente en controlador",
                    Error = "No existe en catalogo"
                });
            }

            var ProteccionVsSobreTemperatura = lstComboBinario.FirstOrDefault(t => t.Elemento.Equals(row.ProteccionVsSobreTemperatura.ToUpper()));
            if (ProteccionVsSobreTemperatura != null) newProduct.Cve_P_Sobre_Temperatura = ProteccionVsSobreTemperatura.IdElemento;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()   
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Protección vs Sobretemperatura",
                    Error = "No existe en catalogo"
                });
            }


            var ProteccionVsSobreVoltaje = lstComboBinario.FirstOrDefault(t => t.Elemento.Equals(row.ProteccionVsSobreVoltaje.ToUpper()));
            if (ProteccionVsSobreVoltaje != null) newProduct.Cve_P_Sobre_Distorsion_V= ProteccionVsSobreVoltaje.IdElemento;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Protección vs Sobredistorsión en Voltaje",
                    Error = "No existe en catalogo"
                });
            }

            var BloqueoDisplay = lstComboBinario.FirstOrDefault(t => t.Elemento.Equals(row.BloqueoDisplay.ToUpper()));
            if (BloqueoDisplay != null) newProduct.Cve_Bloqueo_Prog_Display = BloqueoDisplay.IdElemento;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Bloqueo de Programación en Display",
                    Error = "No existe en catalogo"
                });
            }

            var TipoComunicacion = lstComboBinario.FirstOrDefault(t => t.Elemento.Equals(row.TipoComunicacion.ToUpper()));
            if (TipoComunicacion != null) newProduct.Cve_Tipo_Comunicacion = byte.Parse(TipoComunicacion.IdElemento.ToString());
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Tipo de Comunicación",
                    Error = "No existe en catalogo"
                });
            }

            var ProteccionGabiente = lstProteccionGabiente.FirstOrDefault(t => t.Descripcion.Equals(row.ProteccionGabiente));
            if (ProteccionGabiente != null) newProduct.Cve_Proteccion_Gabinete = ProteccionGabiente.Cve_Proteccion_Gabinete;
            else
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Protección del Gabinete ",
                    Error = "No existe en catalogo"
                });
            }

            newProduct.Dx_Nombre_Producto = "BANCO DE CAPACITORES";
            newProduct.Cve_Tecnologia = int.Parse(idTecnologia);
            bool hayCoincidencia = new CargaProductos().NoExisteProducto(newProduct);
            if (hayCoincidencia)
            {
                flagSinError = false;
                lstError.Add(new Load_LogDetail()
                {
                    LogHeaderId = idLogHeader,
                    NoRegistro = int.Parse(row.NoRegistro),
                    Campo = "Todos",
                    Error = "El Producto ya existe en la Base de Datos."
                });
            }

            if (flagSinError)
            {
                newProduct.Cve_Estatus_Producto = 1;
                newProduct.Dt_Fecha_Producto = DateTime.Now;

            }
            else
            {
                foreach (var loadLogDetail in lstError)
                {
                    int rowinsert = new CargaProductos().InsertaLogDetalle(loadLogDetail);
                }
            }
            return flagSinError ? newProduct : null;
        }
       

        public decimal ObtenerNumero(string campo)
        {
            decimal valDouble = -1;
            try
            {
                valDouble = campo.Equals(string.Empty) ? 0 : decimal.Parse(campo);
                return valDouble;
            }
            catch (Exception)
            { return valDouble; }
         }

    }
}
