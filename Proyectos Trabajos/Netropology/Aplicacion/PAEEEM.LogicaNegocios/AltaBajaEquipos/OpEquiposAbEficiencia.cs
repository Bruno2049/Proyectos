using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.Entidades.Alta_Equipos;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.Entidades.Trama;
using PAEEEM.Entidades.Utilizables;
using PAEEEM.DataAccessLayer;
using System.Data;

namespace PAEEEM.LogicaNegocios.AltaBajaEquipos
{
    public class OpEquiposAbEficiencia
    {

        #region Catalogos Presupuesto de Inversion
        //METODO PARA OBTENER LAS TECNOLOGIAS
        public List<CComboBox> Tecnologias()
        {
            var cboTecnologias = new List<CComboBox>();
            var tecnologias = new List<CAT_TECNOLOGIA>();

            tecnologias = new Tecnologia().ObtieneTecnologias(p => true);

            if (tecnologias.Count > 0)
            {
                cboTecnologias.Add(new CComboBox(-1, "Seleccione"));

                foreach (var tecnologia in tecnologias)
                {
                    cboTecnologias.Add(new CComboBox(tecnologia.Cve_Tecnologia, tecnologia.Dx_Nombre_General));
                }

            }

            return cboTecnologias;
        }


        //METODO PARA OBTENER LAS TECNOLOGIAS POR TARIFA
        public List<CComboBox> Tecnologias(int CveTarifa)
        {
            var cboTecnologias = new List<CComboBox>();
            var tecnologias = new List<CAT_TECNOLOGIA>();

            tecnologias = new Tecnologia().ObtieneTecnologiasXTarifa(CveTarifa);

            if (tecnologias.Count > 0)
            {
                cboTecnologias.Add(new CComboBox(-1, "Seleccione"));

                foreach (var tecnologia in tecnologias)
                {
                    cboTecnologias.Add(new CComboBox(tecnologia.Cve_Tecnologia, tecnologia.Dx_Nombre_General));
                }

            }

            return cboTecnologias;
        }

        public List<CComboBox> TecnologiasProveedor(int CveTarifa, int idProveedor)
        {
            var cboTecnologias = new List<CComboBox>();
            var tecnologias = new List<CAT_TECNOLOGIA>();

            tecnologias = new Tecnologia().ObtieneTecnologiasXTarifaProveedor(CveTarifa, idProveedor);

            if (tecnologias.Count > 0)
            {
                cboTecnologias.Add(new CComboBox(-1, "Seleccione"));

                foreach (var tecnologia in tecnologias)
                {
                    cboTecnologias.Add(new CComboBox(tecnologia.Cve_Tecnologia, tecnologia.Dx_Nombre_General));
                }

            }

            return cboTecnologias;
        }

        ////METODO PARA OBTENER LAS TECNOLOGIAS POR TARIFA
        //public List<CComboBox> ProductosConsumos(int ID_TIPO_PRODUCTO)
        //{
        //    var cboTecnologias = new List<CComboBox>();
        //    var tecnologias = new List<CAT_TECNOLOGIA>();

        //    tecnologias = new Tecnologia().ObtieneTecnologiasXTarifa(ID_TIPO_PRODUCTO);

        //    if (tecnologias.Count > 0)
        //    {
        //        cboTecnologias.Add(new CComboBox(-1, "Seleccione"));

        //        foreach (var tecnologia in tecnologias)
        //        {
        //            cboTecnologias.Add(new CComboBox(tecnologia.Cve_Tecnologia, tecnologia.Dx_Nombre_General));
        //        }

        //    }

        //    return cboTecnologias;
        //}

        //METODO PARA OBTENER LAS TECNOLOGIAS POR TARIFA
        
        public List<CComboBox> ProductosConsumos(int ID_TIPO_PRODUCTO)
        {
            var cboConsumos = new List<CComboBox>();
            var consumos = new List<ABE_TABLAS_AHORRO>();
            string valor = "";

            consumos = new Tecnologia().ObtieneConsumos(ID_TIPO_PRODUCTO);
            cboConsumos.Add(new CComboBox(-1, "Seleccione"));
            if (consumos.Count > 0)
            {
                foreach (var consumo in consumos)
                {
                    cboConsumos.Add(new CComboBox(consumo.IDAHORRO, consumo.CAP_INICIAL.ToString()));
                }

            }

            return cboConsumos;
        }


        public List<CComboBox> ProductosConsumosAA_ME(int cveTecnologia)
        {
            var cboConsumos = new List<CComboBox>();
            var consumos = new List<ABE_TABLAS_AHORRO>();
            string valor = "";

            consumos = new Tecnologia().ObtieneConsumosAA_ME(cveTecnologia);
            cboConsumos.Add(new CComboBox(-1, "Seleccione"));
            if (consumos.Count > 0)
            {
                foreach (var consumo in consumos)
                {
                    cboConsumos.Add(new CComboBox(consumo.IDAHORRO, consumo.CAP_INICIAL.ToString()));
                }

            }

            return cboConsumos;
        }

        

        public List<CComboBox> Productos(int idTecnologia)
        {
            var cboProductos = new List<CComboBox>();
            var productos = new List<CAT_PRODUCTO>();

            productos = new Producto().FitrarPorCondicion(p => p.Cve_Tecnologia == idTecnologia);

            if (productos.Count > 0)
            {
                cboProductos.Add(new CComboBox(-1, "Seleccione"));

                foreach (var producto in productos)
                {
                    cboProductos.Add(new CComboBox(producto.Cve_Producto, producto.Dx_Nombre_Producto));
                }
            }


            return cboProductos;
        }

        
        public List<CComboBox> MarcasAltaEficiencia(int Cve_Producto, int IdProveedor)
        {
            var cboMarcas = new List<CComboBox>();
            DataTable marcas;

            marcas = new CAT_MARCADal().Get_CAT_MARCAByIdProveedor(Cve_Producto.ToString(), IdProveedor.ToString());

            if (marcas != null)
            {
                cboMarcas.Add(new CComboBox(-1, "Seleccione"));

                for (int i = 1; i <= marcas.Rows.Count; i++)
                {
                    cboMarcas.Add(new CComboBox(int.Parse(marcas.Rows[i - 1]["Cve_Marca"].ToString()), marcas.Rows[i - 1]["Dx_Marca"].ToString()));
                }
            }

            return cboMarcas;
        }

        public List<CComboBox> ModelosAltaEficiencia(int Cve_Tecnologia, int Cve_Marca, int IdProveedor)
        {
            var cboModelos = new List<CComboBox>();
            DataTable modelos;

            modelos = new CAT_PRODUCTODal().GetModelByTechnologyAndMarca(Cve_Tecnologia.ToString(), Cve_Marca.ToString());

            if (modelos != null)
            {
                cboModelos.Add(new CComboBox(-1, "Seleccione"));

                for (int i = 1; i <= modelos.Rows.Count; i++)
                {
                    // REVISAR NUMERADOR
                    cboModelos.Add(new CComboBox(i, 
                        modelos.Rows[i - 1]["Dx_Modelo_Producto"].ToString()));
                }
            }
            else
            {
                cboModelos.Add(new CComboBox(-1, "Seleccione"));
            }

            return cboModelos;
        }

        //public List<CComboBox> CapacidadAA_ME(int idTecnologia)
        //{
        //    var cbCapacidades = new List<CComboBox>();
        //    var capacidades = new List<CAT_CAPACIDAD_SUSTITUCION>();

        //    capacidades = new CapacidadSustitucion().FitrarPorCondicion(p => p.Cve_Tecnologia == idTecnologia);

        //    if (capacidades.Count > 0)
        //    {
        //        cbCapacidades.Add(new CComboBox(-1, "Seleccione"));

        //        foreach (var capacidadSust in capacidades)
        //        {
        //            cbCapacidades.Add(new CComboBox(capacidadSust.Cve_Capacidad_Sust, capacidadSust.Dx_Capacidad));
        //        }
        //    }

        //    return cbCapacidades;
        //}

        public List<CComboBox> ProductosBajaEficiencia(int idTecnologia)
        {
            var cboTipoProductos = new List<CComboBox>();
            var productos = new List<CAT_TIPO_PRODUCTO>();

            //productos = new TIPO_PRODUCTO().FitrarPorCondicion(p => p.Cve_Tecnologia == idTecnologia && p.EQUIPO_BAJA == true);
            productos = new TIPO_PRODUCTO().FitrarPorCondicion(p => p.Cve_Tecnologia == idTecnologia && p.ESTATUS != false && p.EQUIPO_BAJA == true);
            cboTipoProductos.Add(new CComboBox(-1, "Seleccione"));
            if (productos.Count > 0)
            {


                foreach (var producto in productos)
                {
                    cboTipoProductos.Add(new CComboBox(producto.Ft_Tipo_Producto, producto.Dx_Tipo_Producto));
                }
            }


            return cboTipoProductos;
        }

        public string PrecioDistribuidorProductoAltaEficiencia(int Cve_Modelo, int Id_Proveedor)
        {
            string precioDistribuidor = "0";

            precioDistribuidor = new CAT_PRODUCTODal().ObtienePrecioDistribuidor(Cve_Modelo, Id_Proveedor).ToString();

            return precioDistribuidor;
        }


        public ABE_UNIDAD_TECNOLOGIA UnidadTecnologica(int idTecnologia)
        {
            ABE_UNIDAD_TECNOLOGIA unidadTecnologica = new ABE_UNIDAD_TECNOLOGIA();

            unidadTecnologica = new UNIDAD_TECNOLOGICA().ObtienePorCondicion(p => p.IDTECNOLOGIA == idTecnologia);

            return unidadTecnologica;
        }

        public List<Presupuesto> ObtieneConceptosPresupuesto()
        {
            var listConceptos = new ParametrosGlobales().ObtieneConceptosPresupuesto();

            foreach (var concepto in listConceptos)
            {
                switch (concepto.IdPresupuesto)
                {
                    case 3:
                        concepto.IdOrden = 1;
                        break;
                    case 7:
                        concepto.IdOrden = 2;
                        break;
                    case 2:
                        concepto.IdOrden = 3;
                        break;
                    case 1:
                        concepto.IdOrden = 4;
                        break;
                    case 5:
                        concepto.IdOrden = 5;
                        break;
                    case 4:
                        concepto.IdOrden = 6;
                        break;
                    case 6:
                        concepto.IdOrden = 7;
                        break;
                    case 8:
                        concepto.IdOrden = 8;
                        break;

                    default:
                        break;
                }
            }
            

            return listConceptos;
        }

        #endregion

        #region Metodos Alta y Baja de Equipos

        //METODOS PARA LLENAR CATALOGOS DE ALTA Y BAJA DE EQUIPOS (DPF)
        public List<Valor_Catalogo> ObtenCapacidades(int tipoProducto, int cveEsquema, int idTecnologia)
        {
            if (cveEsquema == 0)
            {
                var lstValorCatalogo = new List<Valor_Catalogo>();
                var lstCapacidades = new Tecnologia().ObtenCapacidadSustitucion(idTecnologia); //new Tecnologia().ObtenCapacidades(tipoProducto);
                
                foreach (var catCapacidadSustitucion in lstCapacidades)
                {
                    var valorCatalogo = new Valor_Catalogo();
                    valorCatalogo.CveValorCatalogo = catCapacidadSustitucion.Cve_Capacidad_Sust.ToString();
                    valorCatalogo.DescripcionCatalogo = catCapacidadSustitucion.No_Capacidad.ToString();

                    lstValorCatalogo.Add(valorCatalogo);
                }

                return lstValorCatalogo;
            }
            else
            {
                var lstSistema = new Tecnologia().ObtenSistemaArreglo(tipoProducto);

                return lstSistema;
            }
        }

        public List<CAT_CAPACIDAD_SUSTITUCION> ObtenCapacidadesSustitucionSa(int idTecnologia, int tipoProducto)
        {
            var lstCapacidades = new Tecnologia().ObtenCapacidadSustitucionSa(idTecnologia, tipoProducto);

            return lstCapacidades;
        }

        public K_PROVEEDOR_PRODUCTO FitrarPorCondicionProvvedorProd(
            Expression<Func<K_PROVEEDOR_PRODUCTO, bool>> criterio)
        {
            var producto = new Producto().FitrarPorCondicionProvvedorProd(criterio);

            return producto;
        }

        public List<EquipoAltaEficiencia> ObtenProductosAltaEficiencia(int idTipoProducto, int cveMarca, int idProveedor, double
                                                                                                        capacidadMaxima, int idTecnologia)
        {
            var lstProductosAlta = new Producto().ObtenProductosAltaEficiencia(idTipoProducto, cveMarca, idProveedor,
                                                                               capacidadMaxima, idTecnologia);

            return lstProductosAlta;
        }

        public List<EquipoAltaEficiencia> ObtenProductosSistemasAltaEficiencia(int idProveedor, int cveMarca, int idSistemaArreglo, int idTecnologia)
        {
            var lstProductosAlta = new Producto().ObtenProductosSistemasAltaEficiencia(idProveedor, cveMarca, idSistemaArreglo, idTecnologia);

            return lstProductosAlta;
        }

        public List<EquipoAltaEficiencia> ObtenSubestacionesElectricas(int cveMarca, int idProveedor)
        {
            var lstSubestaciones = new Producto().ObtenSubestacionesElectricas(cveMarca, idProveedor);

            return lstSubestaciones;
        }

        public List<EquipoAltaEficiencia> ObtenBancoCapacitores(int cveMarca, int idProveedor, int idTecnologia, double kvars)
        {
            var capacidadMaxima = kvars* 1.25;

            var lstBancoCapacitores = new Producto().ObtenBancoCapacitores(cveMarca, idProveedor, idTecnologia, capacidadMaxima);

            return lstBancoCapacitores;
        }

        public CAT_PROGRAMA ObtenDatosPrograma(int idPrograma)
        {
            var programa = new Producto().ObtenDatosPrograma(idPrograma);

            return programa;
        }

        public ABE_HORAS_TECNOLOGIA ObtenHorasOPeracion(int IdSector, int IdEstado, int IdMunicipio)
        {
            var horasOPeracion = new Producto().ObtenHorasOPeracion(IdSector, IdEstado, IdMunicipio);

            return horasOPeracion;
        }

        public ABE_VALORES_PRODUCTO ObtenValoresProducto(int IdTipoProducto)
        {
            var valoresProducto = new Producto().ObtenValoresProducto(IdTipoProducto);

            return valoresProducto;
        }

        public ABE_TABLAS_AHORRO ObtenNomEse22(int idTipoProducto, decimal capacidad)
        {
            var ahorros = new Producto().ObtenNomEse22(idTipoProducto, capacidad);

            return ahorros;
        }

        public int ObtenTipoProducto(int IdProducto)
        {
            var tipoProducto = new Producto().ObtenTipoProducto(IdProducto);

            return tipoProducto;
        }

        public ABE_TABLAS_AHORRO ObtenAhorrosAireAcondicionado(int idTecnologia, decimal capacidad)
        {
            var ahorros = new Producto().ObtenAhorrosAireAcondicionado(idTecnologia, capacidad);

            return ahorros;
        }

        public SISTEMA_ARREGLO ObtenSistemaEb(int idSistemaArreglo)
        {
            var sistema = new Producto().ObtenSistemaEb(idSistemaArreglo);

            return sistema;
        }

         public byte ObtenIdSistemaArreglo(int idCapacidadSustitucion)
         {
             var idSistemaArreglo = new Tecnologia().ObtenIdSistemaArreglo(idCapacidadSustitucion);

             return idSistemaArreglo;
         }

        public decimal ObtenPotenciaEaIl(int idProducto)
        {
            var potencia = new Producto().ObtenPotenciaEaIl(idProducto);

            return potencia;
        }

        public ABE_TABLAS_AHORRO ObtenPotenciaNominalEa(decimal capacidad, int cveTecnologia)
        {
            var ahorros = new Producto().ObtenPotenciaNominalEa(capacidad, cveTecnologia);

            return ahorros;
        }

        //GENERAR CREDITO Y SUS EQUIPOS DE ALTA Y BAJA RELACIONADOS (DPF)

        public bool VerificaRpuSolictudActiva(string rpu)
        {
            return  new CreCredito().BuscaRpu(rpu);
        }

        public CRE_Credito InsertaCredito(CRE_Credito credito)
        {
            var newCredito = new CreCredito().InsertaCredito(credito);

            return newCredito;
        }

        public static bool ActualizaCredito(CRE_Credito credito)
        {
            
            var actualiza = new CreCredito().ActualizaCredito(credito);

            return actualiza;
        }

        public bool EliminaCredito(CRE_Credito credito)
        {
            var borra = new CreCredito().EliminaCredito(credito);

            return borra;
        }

        public bool InsertaEquiposAlta(List<EquipoAltaEficiencia> lstEquiposAlta, string noCredito, decimal valorIva)
        {
            K_CREDITO_PRODUCTO productoAlta = null;

            try
            {
                foreach (var equipoAltaEficiencia in lstEquiposAlta)
                {
                    var precioUnitarioIva = equipoAltaEficiencia.Precio_Unitario +
                                            (equipoAltaEficiencia.Precio_Unitario*valorIva);

                    var importeTotal = precioUnitarioIva*equipoAltaEficiencia.Cantidad;
                    var gastosInstalacion = equipoAltaEficiencia.Gasto_Instalacion +
                                            (equipoAltaEficiencia.Gasto_Instalacion*valorIva);

                    productoAlta = new K_CREDITO_PRODUCTO
                        {
                            No_Credito = noCredito,
                            Cve_Producto = equipoAltaEficiencia.Cve_Modelo,
                            No_Cantidad = equipoAltaEficiencia.Cantidad,
                            Mt_Precio_Unitario = Math.Round(precioUnitarioIva,2),
                            Mt_Precio_Unitario_Sin_IVA = equipoAltaEficiencia.Precio_Unitario,
                            Mt_Total = Math.Round(importeTotal,2),
                            Dt_Fecha_Credito_Producto = DateTime.Now.Date,
                            Mt_Gastos_Instalacion_Mano_Obra = Math.Round(gastosInstalacion,2),
                            CapacidadSistema = equipoAltaEficiencia.Dx_Sistema,
                            Grupo = equipoAltaEficiencia.Dx_Grupo,
                            IdGrupo = equipoAltaEficiencia.Cve_Grupo,
                            Incentivo = equipoAltaEficiencia.MontoIncentivo
                        };
                    //Cve_Producto_Capacidad

                    var productoOK = new CreCredito().InsertaProductoAlta(productoAlta);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void EliminaProductosAlta(string idCredito)
        {
            new CreCredito().EliminaProductosAlta(idCredito);
        }

        public bool InsertaEquiposBaja(List<EquipoBajaEficiencia> lstEquiposBaja, string noCredito)
        {
            K_CREDITO_SUSTITUCION productoBaja = null;

            try
            {
                foreach (var equipoBajaEficiencia in lstEquiposBaja)
                {
                    if (equipoBajaEficiencia.Dx_Tecnologia.Contains("SUBESTACION") || equipoBajaEficiencia.Dx_Tecnologia.Contains("BANCO"))
                        continue;

                    if (equipoBajaEficiencia.Dx_Tecnologia.Contains("ILUMINA"))
                    {
                        productoBaja = new K_CREDITO_SUSTITUCION
                            {
                                No_Credito = noCredito,
                                Cve_Tecnologia = equipoBajaEficiencia.Cve_Tecnologia,
                                No_Unidades = equipoBajaEficiencia.Cantidad,
                                Dt_Fecha_Credito_Sustitucion = DateTime.Now.Date,
                                Dx_Tipo_Producto = equipoBajaEficiencia.Ft_Tipo_Producto,
                                Dx_Modelo_Producto = equipoBajaEficiencia.Dx_Tipo_Producto,
                                Grupo = equipoBajaEficiencia.Dx_Grupo,
                                IdGrupo = equipoBajaEficiencia.Cve_Grupo,
                                CapacidadSistema = equipoBajaEficiencia.Dx_Consumo,
                                Unidad = equipoBajaEficiencia.Dx_Unidad,
                                IdSistemaArreglo = Convert.ToByte(equipoBajaEficiencia.Cve_Consumo),
                            };

                        var productoOK = new CreCredito().InsertaProductoBaja(productoBaja);
                    }
                    else
                    {

                        for (int i = 0; i < equipoBajaEficiencia.Cantidad; i++)
                        {

                            productoBaja = new K_CREDITO_SUSTITUCION
                                {
                                    No_Credito = noCredito,
                                    Cve_Tecnologia = equipoBajaEficiencia.Cve_Tecnologia,
                                    //No_Unidades = equipoBajaEficiencia.Cantidad,
                                    Dt_Fecha_Credito_Sustitucion = DateTime.Now.Date,
                                    Dx_Tipo_Producto = equipoBajaEficiencia.Ft_Tipo_Producto,
                                    Dx_Modelo_Producto = equipoBajaEficiencia.Dx_Tipo_Producto,
                                    Grupo = equipoBajaEficiencia.Dx_Grupo,
                                    IdGrupo = equipoBajaEficiencia.Cve_Grupo,
                                    CapacidadSistema = equipoBajaEficiencia.Dx_Consumo,
                                    Unidad = equipoBajaEficiencia.Dx_Unidad
                                };

                            if (equipoBajaEficiencia.Dx_Tecnologia.Contains("AIRE") ||
                                equipoBajaEficiencia.Dx_Tecnologia.Contains("MOTORES"))
                                productoBaja.Cve_Capacidad_Sust = equipoBajaEficiencia.Cve_Consumo;

                            var productoOK = new CreCredito().InsertaProductoBaja(productoBaja);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
                        
            return true;
        }

        public bool InsertaCreEquiposBaja(List<EquipoBajaEficiencia> lstEquiposBaja, string noCredito)
        {
            CRE_CREDITO_EQUIPOS_BAJA productoBaja = null;

            try
            {
                foreach (var equipoBajaEficiencia in lstEquiposBaja)
                {

                    productoBaja = new CRE_CREDITO_EQUIPOS_BAJA
                    {
                        No_Credito = noCredito,
                        Cve_Tecnologia = equipoBajaEficiencia.Cve_Tecnologia,
                        No_Unidades = equipoBajaEficiencia.Cantidad,
                        Dt_Fecha_Credito_Sustitucion = DateTime.Now.Date,
                        Dx_Tipo_Producto = equipoBajaEficiencia.Ft_Tipo_Producto,
                        Dx_Modelo_Producto = equipoBajaEficiencia.Dx_Tipo_Producto,
                        Grupo = equipoBajaEficiencia.Dx_Grupo,
                        IdGrupo = equipoBajaEficiencia.Cve_Grupo,
                        CapacidadSistema = equipoBajaEficiencia.Dx_Consumo,
                        Unidad = equipoBajaEficiencia.Dx_Unidad
                    };

                    if (equipoBajaEficiencia.Dx_Tecnologia.Contains("ILUMINA"))
                        productoBaja.IdSistemaArreglo = Convert.ToByte(equipoBajaEficiencia.Cve_Consumo);

                    if (equipoBajaEficiencia.Dx_Tecnologia.Contains("AIRE") ||
                        equipoBajaEficiencia.Dx_Tecnologia.Contains("MOTORES"))
                        productoBaja.Cve_Capacidad_Sust = equipoBajaEficiencia.Cve_Consumo;

                    var productoOK = new CreCredito().InsertaCreProductoBaja(productoBaja);
                }
            }
            catch (Exception)
            {
                return false;
            }
                        
            return true;
        }

        public void EliminaProductosBaja(string idCredito)
        {
            new CreCredito().EliminaProductosBaja(idCredito);
        }

        public CAT_TARIFA ObtenTarifa(string tarifa)
        {
            var catTarifa = new Producto().ObtenTarifa(tarifa);

            return catTarifa;
        }

        public CLI_Cliente BuscaClientePorCondicion(Expression<Func<CLI_Cliente, bool>> criterio)
        {
            var cliente = new Producto().BuscaClientePorCondicion(criterio);

            return cliente;
        }

        public bool InsertaAmortizacionesCredito(DataTable tablaAmortizacion, string idCredito)
        {
            try
            {
                for (int i = 0; i < tablaAmortizacion.Rows.Count; i++)
                {
                    var creditoAmortizacion = new K_CREDITO_AMORTIZACION();
                    creditoAmortizacion.No_Credito = idCredito;
                    creditoAmortizacion.No_Pago = Convert.ToInt32(tablaAmortizacion.Rows[i]["No_Pago"]);
                    creditoAmortizacion.Dt_Fecha = Convert.ToDateTime(tablaAmortizacion.Rows[i]["Dt_Fecha"]);
                    creditoAmortizacion.No_Dias_Periodo =
                        Convert.ToInt32(tablaAmortizacion.Rows[i]["No_Dias_Periodo"]);
                    creditoAmortizacion.Mt_Capital =
                        Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_Capital"]);
                    creditoAmortizacion.Mt_Interes =
                        Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_Interes"]);
                    creditoAmortizacion.Mt_IVA = Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_IVA"]);
                    creditoAmortizacion.Mt_Pago = Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_Pago"]);
                    creditoAmortizacion.Mt_Amortizacion =
                        Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_Amortizacion"]);
                    creditoAmortizacion.Mt_Saldo =
                        Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_Saldo"]);
                    creditoAmortizacion.Dt_Fecha_Credito_Amortización = DateTime.Now.Date;

                    var newCreAmortozacion = new CreCredito().InsertaAmortizacionCredito(creditoAmortizacion);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public K_CREDITO_COSTO InsertaCreditoCosto(string idCredito, decimal costo)
        {
            var creditoCosto = new K_CREDITO_COSTO();
            creditoCosto.No_Credito = idCredito;
            creditoCosto.Mt_Costo = costo;
            creditoCosto.Dt_Credito_Costo = DateTime.Now.Date;

            var newcreditoCosto = new CreCredito().InsertaCreditoCosto(creditoCosto);

            return newcreditoCosto;
        }

        public K_CREDITO_DESCUENTO InsertaCreditoDescuento(string idCredito, decimal descuento)
        {
            var creditoDescuento = new K_CREDITO_DESCUENTO();
            creditoDescuento.No_Credito = idCredito;
            creditoDescuento.Mt_Descuento = descuento;
            creditoDescuento.Dt_Credito_Descuento = DateTime.Now.Date;

            var newcreditoDescuento = new CreCredito().InsertaCreditoDescuento(creditoDescuento);

            return newcreditoDescuento;
        }

        public CRE_CAPACIDAD_PAGO InsertaCapacidadPago(CompConceptosCappago capacidadPago, string usuario, string idCredito)
        {
            var capacidad = new CRE_CAPACIDAD_PAGO();
            capacidad.No_Credito = idCredito;
            capacidad.Ventas = capacidadPago.Ventas;
            capacidad.Gastos = capacidadPago.Gastos;
            capacidad.Ahorro = capacidadPago.Ahorro;
            capacidad.Flujo = capacidadPago.Flujo;
            capacidad.Capacidad = capacidadPago.Capacidad;
            capacidad.Fecha_Adicion = DateTime.Now.Date;
            capacidad.AdicionadoPor = usuario;
            capacidad.Estatus = 1;

            var newcapacidad = new CreCredito().InsertaCapacidadPago(capacidad);

            return newcapacidad;
        }

        public bool InsertaFacturacion(CompFacturacion facturacion, string usuario, string idCredito, int idTarifa, int idTipoFacturacion)
        {
            try
            {
                var creFacturacion = new CRE_Facturacion();
                creFacturacion.No_Credito = idCredito;
                creFacturacion.Cve_Tarifa = idTarifa;
                creFacturacion.IdTipoFacturacion = Convert.ToByte(idTipoFacturacion);
                creFacturacion.Subtotal = facturacion.Subtotal;
                creFacturacion.Iva = facturacion.Iva;
                creFacturacion.Total = facturacion.Total;
                creFacturacion.PagoFactBiMen = facturacion.PagoFactBiMen;
                creFacturacion.MontoMaxFacturar = facturacion.MontoMaxFacturar;
                creFacturacion.AdicionadoPor = usuario;
                creFacturacion.Estatus = 1;
                creFacturacion.Fecha_Adicion = DateTime.Now.Date;


                var newcreFacturacion = new CreCredito().InsertaFacturacion(creFacturacion);

                if (newcreFacturacion != null)
                {
                    if (facturacion.ConceptosFacturacion.Count > 0)
                    {
                        foreach (var concepto in facturacion.ConceptosFacturacion)
                        {
                            var facturacionDetalle = new CRE_FACTURACION_DETALLE();
                            facturacionDetalle.IdFactura = newcreFacturacion.IdFactura;
                            facturacionDetalle.Concepto = concepto.Concepto;
                            facturacionDetalle.Consumo = concepto.CPromedioODemMax;
                            facturacionDetalle.CostoMensual = concepto.CargoAdicional;
                            facturacionDetalle.Facturacion = concepto.Facturacion;
                            facturacionDetalle.Fecha_Adicion = DateTime.Now.Date;
                            facturacionDetalle.AdicionadoPor = usuario;
                            facturacionDetalle.Estatus = 1;

                            var newFacturacionDetalle = new CreCredito().InsertaFacturaciondetalle(facturacionDetalle);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            
            return true;
        }

        public CRE_PSR InsertaPSR(CompConceptosPsr psr, string usuario, string idCredito)
        {
            var periodoSimple = new CRE_PSR();
            periodoSimple.No_Credito = idCredito;
            periodoSimple.MontoFinanciamento = psr.MontoFinanciamento;
            periodoSimple.AhorroEconomico = psr.AhorroEconomico;
            periodoSimple.Periodo = Convert.ToByte(psr.Periodo);
            periodoSimple.ValorPsr = psr.ValorPsr;
            periodoSimple.Fecha_Adicion = DateTime.Now.Date;
            periodoSimple.AdicionadoPor = usuario;
            periodoSimple.Estatus = 1;

            var newperiodoSimple = new CreCredito().InsertaPSR(periodoSimple);

            return newperiodoSimple;
        }

        public bool InsertaHistoricoConsumo(List<CompHistorialDetconsumo> lstHistorico, string idCredito, string usuario)
        {
            try
            {
                var lsthistDetConsumo = lstHistorico.FindAll(me => me.Fecha > Convert.ToDateTime("01/01/0001"));
                foreach (var creHistoricoConsumo in lsthistDetConsumo)
                {
                    var historico = new CRE_HISTORICO_CONSUMO();
                    historico.No_Credito = idCredito;
                    historico.IdHistorial = Convert.ToByte(creHistoricoConsumo.IdHistorial);
                    historico.Fecha_Periodo = (DateTime)creHistoricoConsumo.Fecha;
                    historico.Consumo = creHistoricoConsumo.Consumo;
                    historico.Demanda = creHistoricoConsumo.Demanda;
                    historico.FactorPotencia = creHistoricoConsumo.FactorPotencia;
                    historico.IdValido = Convert.ToByte(creHistoricoConsumo.Id);
                    historico.Fecha_Adicion = DateTime.Now.Date;
                    historico.Estatus = 1;
                    historico.AdicionadoPor = usuario;

                    var newhistorico = new CreCredito().InsertaHistoricoConsumo(historico);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool InsertaGruposTecnologia(List<EquipoBajaEficiencia> lstGrupos, string idCredito, string usuario)
        {
            try
            {
                foreach (var equipoBajaEficiencia in lstGrupos)
                {
                    var grupoTec = new CRE_GRUPOS_TECNOLOGIA();
                    grupoTec.No_Credito = idCredito;
                    grupoTec.IdGrupo = equipoBajaEficiencia.Cve_Grupo;
                    grupoTec.Grupo = equipoBajaEficiencia.Dx_Grupo;
                    grupoTec.IdTecnologia = equipoBajaEficiencia.Cve_Tecnologia;
                    grupoTec.Tecnologia = equipoBajaEficiencia.Dx_Tecnologia;
                    grupoTec.AhorroConsumo = equipoBajaEficiencia.AhorroConsumo;
                    grupoTec.AhorroDemanda = equipoBajaEficiencia.AhorroDemanda;
                    grupoTec.Estatus = true;
                    grupoTec.FechaAdicion = DateTime.Now.Date;
                    grupoTec.AdicionadoPor = usuario;

                    var newGrupo = new CreCredito().InsertaGrupoTecnologia(grupoTec);
                }
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }

        public void EliminaPresupuestoInversion(string idCredito)
        {
            new CreCredito().EliminaHorariosOperacion(idCredito, 1);
            new CreCredito().EliminaOperacionTotal(idCredito, 1);

            new CreCredito().EliminaHorariosOperacion(idCredito, 2);
            new CreCredito().EliminaOperacionTotal(idCredito, 2);
            new CreCredito().EliminaHorariosOperacion(idCredito, 3);
            new CreCredito().EliminaOperacionTotal(idCredito, 3);
            new CreCredito().EliminaCapacidadPago(idCredito);
            new CreCredito().EliminaPsr(idCredito);
            new CreCredito().EliminaFotos(idCredito);
            new CreCredito().EliminaFacturacion(idCredito);
            new CreCredito().EliminaHistoricoConsumos(idCredito);
            new CreCredito().EliminaCreditoCosto(idCredito);
            new CreCredito().EliminaCreditoDescuento(idCredito);
            new CreCredito().EliminaAmortizacioncredito(idCredito);
            new CreCredito().EliminaCreProductosBaja(idCredito);
            new CreCredito().EliminaProductosAlta(idCredito);
            new CreCredito().EliminaProductosBaja(idCredito);
            new CreCredito().EliminaGruposTecnologia(idCredito);
        }

        public void EliminaHorarios(string idCredito, int tipoOperacion)
        {
            new CreCredito().EliminaHorariosOperacion(idCredito, tipoOperacion);
            new CreCredito().EliminaOperacionTotal(idCredito, tipoOperacion);
        }

        #endregion


        #region Metodos de Consulta- Alta y Baja de Equipos

        public List<EquipoBajaEficiencia> ObtenEquiposBajaEficienciaCredito(string idCredito)
        {
            var lstEquiposBaja = new CreCredito().ObtenEquiposBajaEficienciaCredito(idCredito);

            foreach (var equipoBajaEficiencia in lstEquiposBaja)
            {
                equipoBajaEficiencia.DetalleTecnologia = new Tecnologia().ValidaEquipoParaAlta(equipoBajaEficiencia.Cve_Tecnologia);

                if (equipoBajaEficiencia.DetalleTecnologia.CveChatarrizacion == true)
                {
                    equipoBajaEficiencia.MontoChatarrizacion =
                        (decimal)equipoBajaEficiencia.DetalleTecnologia.MontoChatarrizacion;
                }
            }

            return lstEquiposBaja;
        }

        public List<EquipoAltaEficiencia> ObtenEquiposAltaEficienciaCredito(string idCredito)
        {
            var lstEquiposAlta = new CreCredito().ObtenEquiposAltaEficienciaCredito(idCredito);

            return lstEquiposAlta;
        }

        public decimal ValidaMontoMaximoCredito(string RFC)
        {
            var montoCreditos = new CreCredito().ValidaMontoMaximoCredito(RFC);
            var montoCreditosAnterior = Convert.ToDecimal(new K_CREDITODal().TotalAmountByRFC(RFC));

            return montoCreditos + montoCreditosAnterior;
        }

        public decimal ValidaMontoMaximoCreditoEdit(string RFC, string idCredito)
        {
            var montoCreditos = new CreCredito().ValidaMontoMaximoCreditoEdit(RFC, idCredito);
            var montoCreditosAnterior = Convert.ToDecimal(new K_CREDITODal().TotalAmountByRFC(RFC));

            return montoCreditos + montoCreditosAnterior;
        }

        public string ObtenCN(int idTrama)
        {
            var cnTrama = new CreCredito().ObtenCN(idTrama);

            return cnTrama;
        }

        public decimal ObtenMinimoCapacidadBC()
        {
            var capacidadMinima = new CreCredito().ObtenMinimoCapacidadBC();

            return capacidadMinima;
        }

        public string ObtenTrama(string idCredito)
        {
            var trama = new CreCredito().ObtenTrama(idCredito);

            return trama;
        }

        public bool EsReasignacion(string idCredito)
        {
            var credito = CreCredito.ObtieneCredito(idCredito);

            return credito.Solicitud_Reasignada != null && (bool) credito.Solicitud_Reasignada;
        }

        //add by @l3x
        public bool EsReactivacion(string idCredito)
        {
            bool ban = false;
            var credito = CreCredito.ObtieneCredito(idCredito);
            if (credito.No_Reactivaciones > 0)
                ban = true;

            return ban;
        }


        #endregion

        #region Tabla de AmortizacionTemp

        public void CreaAmortizacionTemporal(CRE_PRESUPUESTO_INVERSION presupuesto, DataTable tablaAmortizacion,
                                             string rpu)
        {
            new CreCredito().EliminaPresupuestoTemp(rpu);
            new CreCredito().EliminaAmortizacioncreditoTemp(rpu);

            var newPresupuesto = new CreCredito().InsertaPresupuestoTemp(presupuesto);

            if (newPresupuesto != null)
                InsertaAmortizacionesCreditoTemp(tablaAmortizacion, rpu);
        }

        public bool InsertaAmortizacionesCreditoTemp(DataTable tablaAmortizacion, string rpu)
        {
            try
            {
                for (int i = 0; i < tablaAmortizacion.Rows.Count; i++)
                {
                    var creditoAmortizacion = new CRE_CREDITO_AMORTIZACION();
                    creditoAmortizacion.RPU = rpu;
                    creditoAmortizacion.No_Pago = Convert.ToInt32(tablaAmortizacion.Rows[i]["No_Pago"]);
                    creditoAmortizacion.Dt_Fecha = Convert.ToDateTime(tablaAmortizacion.Rows[i]["Dt_Fecha"]);
                    creditoAmortizacion.No_Dias_Periodo =
                        Convert.ToInt32(tablaAmortizacion.Rows[i]["No_Dias_Periodo"]);
                    creditoAmortizacion.Mt_Capital =
                        Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_Capital"]);
                    creditoAmortizacion.Mt_Interes =
                        Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_Interes"]);
                    creditoAmortizacion.Mt_IVA = Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_IVA"]);
                    creditoAmortizacion.Mt_Pago = Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_Pago"]);
                    creditoAmortizacion.Mt_Amortizacion =
                        Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_Amortizacion"]);
                    creditoAmortizacion.Mt_Saldo =
                        Convert.ToDecimal(tablaAmortizacion.Rows[i]["Mt_Saldo"]);
                    creditoAmortizacion.Dt_Fecha_Credito_Amortización = DateTime.Now.Date;

                    var newCreAmortozacion = new CreCredito().InsertaAmortizacionCreditoTemp(creditoAmortizacion);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Inserta Equipos NO PASARON
        public bool InsertaEquiposAltaNoPasaron(List<EquipoAltaEficiencia> lstEquiposAlta, string noRpu, decimal valorIva, int intento, string adicionadoPor)
        {
            try
            {
                foreach (var equipoAltaEficiencia in lstEquiposAlta)
                {
                    var precioUnitarioIva = equipoAltaEficiencia.Precio_Unitario +
                                            (equipoAltaEficiencia.Precio_Unitario * valorIva);

                    var importeTotal = precioUnitarioIva * equipoAltaEficiencia.Cantidad;
                    var gastosInstalacion = equipoAltaEficiencia.Gasto_Instalacion +
                                            (equipoAltaEficiencia.Gasto_Instalacion * valorIva);

                    var productoAlta = new EQUIPOS_ALTA_NP
                    {
                        No_RPU = noRpu,
                        Cve_Producto = equipoAltaEficiencia.Cve_Modelo,
                        No_Cantidad = equipoAltaEficiencia.Cantidad,
                        Mt_Precio_Unitario = Math.Round(precioUnitarioIva, 2),
                        Mt_Precio_Unitario_Sin_IVA = equipoAltaEficiencia.Precio_Unitario,
                        Mt_Total = Math.Round(importeTotal, 2),
                        Fecha_Adicion = DateTime.Now.Date,
                        Mt_Gastos_Instalacion_Mano_Obra = Math.Round(gastosInstalacion, 2),
                        CapacidadSistema = equipoAltaEficiencia.Dx_Sistema,
                        Grupo = equipoAltaEficiencia.Dx_Grupo,
                        IdGrupo = equipoAltaEficiencia.Cve_Grupo,
                        Incentivo = equipoAltaEficiencia.MontoIncentivo,
                        Secuencia_Intento = intento,
                        Adicionado_Por = adicionadoPor
                    };

                    var productoOK = new CreCredito().InsertaProductoAltaNoPasaron(productoAlta);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        
        public bool InsertaEquiposBajaNoPasaron(List<EquipoBajaEficiencia> lstEquiposBaja, string noRpu, int intento, string adicionadoPor)
        {
            try
            {
                foreach (var equipoBajaEficiencia in lstEquiposBaja)
                {
                    if (equipoBajaEficiencia.Dx_Tecnologia.Contains("SUBESTACION") || equipoBajaEficiencia.Dx_Tecnologia.Contains("BANCO"))
                        continue;

                    EQUIPO_BAJA_NP productoBaja;
                    if (equipoBajaEficiencia.Dx_Tecnologia.Contains("ILUMINA"))
                    {
                        productoBaja = new EQUIPO_BAJA_NP
                        {
                            No_RPU = noRpu,
                            Cve_Tecnologia = equipoBajaEficiencia.Cve_Tecnologia,
                            No_Unidades = equipoBajaEficiencia.Cantidad,
                            Fecha_Adicion = DateTime.Now.Date,
                            Dx_Tipo_Producto = equipoBajaEficiencia.Ft_Tipo_Producto,
                            Dx_Modelo_Producto = equipoBajaEficiencia.Dx_Tipo_Producto,
                            Grupo = equipoBajaEficiencia.Dx_Grupo,
                            IdGrupo = equipoBajaEficiencia.Cve_Grupo,
                            CapacidadSistema = equipoBajaEficiencia.Dx_Consumo,
                            Unidad = equipoBajaEficiencia.Dx_Unidad,
                            IdSistemaArreglo = Convert.ToByte(equipoBajaEficiencia.Cve_Consumo),
                            Secuencia_Intento =  intento,
                            Adicionado_Por = adicionadoPor
                        };

                        var productoOK = new CreCredito().InsertaProductoBajaNoPasaron(productoBaja);
                    }
                    else
                    {

                        for (int i = 0; i < equipoBajaEficiencia.Cantidad; i++)
                        {

                            productoBaja = new EQUIPO_BAJA_NP
                            {
                                No_RPU = noRpu,
                                Cve_Tecnologia = equipoBajaEficiencia.Cve_Tecnologia,
                                Fecha_Adicion = DateTime.Now.Date,
                                Dx_Tipo_Producto = equipoBajaEficiencia.Ft_Tipo_Producto,
                                Dx_Modelo_Producto = equipoBajaEficiencia.Dx_Tipo_Producto,
                                Grupo = equipoBajaEficiencia.Dx_Grupo,
                                IdGrupo = equipoBajaEficiencia.Cve_Grupo,
                                CapacidadSistema = equipoBajaEficiencia.Dx_Consumo,
                                Unidad = equipoBajaEficiencia.Dx_Unidad,
                                Secuencia_Intento =  intento,
                                Adicionado_Por = adicionadoPor
                            };

                            if (equipoBajaEficiencia.Dx_Tecnologia.Contains("AIRE") ||
                                equipoBajaEficiencia.Dx_Tecnologia.Contains("MOTORES"))
                                productoBaja.Cve_Capacidad_Sust = equipoBajaEficiencia.Cve_Consumo;

                            var productoOK = new CreCredito().InsertaProductoBajaNoPasaron(productoBaja);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

     
        public int ObtenMaxIntentoEquiposBaja()
        {
            return CreCredito.ObtenIntentoMaxEB_NP();
        }

        public int ObtenMaxIntentoEquiposAlta()
        {
            return CreCredito.ObtenIntentoMaxEA_NP();
        }
        #endregion
    }
}
