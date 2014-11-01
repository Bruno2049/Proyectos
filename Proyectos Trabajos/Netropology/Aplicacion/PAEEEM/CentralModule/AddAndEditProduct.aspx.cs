using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.Helpers;
using System.Collections.Generic;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.LogicaNegocios.ModuloCentral;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.CentralModule
{
    public partial class AddAndEditProduct : Page
    {
        #region Propiedades

        const string DxCveCcSe = "SE";
        public Dictionary<string, string> DxCveCc
        {
            get
            {
                if (ViewState["DxCveCc"] == null)
                    ViewState["DxCveCc"] = new Dictionary<string, string>();
                return (Dictionary<string, string>)ViewState["DxCveCc"];
            }
            set
            {
                ViewState["DxCveCc"] = value;
            }
        }

        public int StatusId
        {
            get { return ViewState["StatusId"] == null ? 0 : int.Parse(ViewState["StatusId"].ToString()); }
            set { ViewState["StatusId"] = value; }
        }

        public string ProductId
        {
            get
            {
                return ViewState["ProductID"] == null ? "" : ViewState["ProductID"].ToString();
            }
            set
            {
                ViewState["ProductID"] = value;
            }
        }

        public int IdPlantilla
        {
            get
            {
                return int.Parse(ViewState["IdPlantilla"].ToString());
            }
            set
            {
                ViewState["IdPlantilla"] = value;
            }
        }

        public List<CampoCustomizableProducto> LstCustomizableProductos
        {
            get { return (List<CampoCustomizableProducto>)ViewState["LstCustomizableProductos"]; }
            set { ViewState["LstCustomizableProductos"] = value; }
        }
        /// <summary>
        /// Init Default Data When page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #endregion

        #region Carga Inicial

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            try
            {
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                //Initialize Manufacture Dropdownlist
                InitializeDrpManufacture();
                //Initialize Technology dropdownlist
                InitializeDrpTechnology();
                //Initialize Tipo Of Products dropdownlist
                //InitializeDrpTipoProduct();
                //Initialize Marca dropdownlist
                InitializeDrpMarca();
                //Initialize Capacidad dropdownlist
                //InitializeDrpCapacidad(); // added by Tina 2012-02-24
                //LLenaDrpPotencia();
                LlenarCatalogoClase();
                //LlenarCatalogoTipo();
                LLenaCatalogosGenericos();
                //LlenaCatalogos();
                
                LlenarCatalogoTipoEncapsulado();
                LlenarCatalogoProteccionInterna();
                LlenarCatalogoProteccionExterna();
                LlenarCatalogoMaterialCubierta();
                LlenarCatalogoPerdidas();
                LlenarCatalogoProteccionSCorriente();
                LlenarCatalogoProteccionContraFuego();
                LlenarCatalogoProteccionContraExplosion();
                LlenarCatalogoAnclaje();
                LlenarCatalogoTerminalTierra();
                LlenarCatalogoProteccionGabinete();

                // RSA 2012-09-13 SE attributes Start
                InitializeDrpSE_TIPO();
                //InitializeDrpSE_TRANSFORMADOR();
                //InitializeDrpSE_TRANSFORM_FASE();
                //InitializeDrpSE_TRANSFORM_MARCA();
                InitializeDrpSE_TRANSFORM_RELACION();
                //InitializeDrpSE_APARTARRAYO();
                //InitializeDrpSE_APARTARRAYO_MARCA();
                //InitializeDrpSE_CORTACIRCUITO();
                //InitializeDrpSE_CORTACIRC_MARCA();
                //InitializeDrpSE_INTERRUPTOR();
                //InitializeDrpSE_INTERRUPTOR_MARCA();
                //InitializeDrpSE_CONDUCTOR();
                //InitializeDrpSE_CONDUCTOR_MARCA();
                //InitializeDrpSE_COND_CONEXION();
                //InitializeDrpSE_COND_CONEXION_MARCA();
                // RSA 2012-09-13 SE attributes End

                //CreaCamposDinamicos();
                PanelPlantillaComun.Visible = PanelPlantillaBC.Visible = containerSE.Visible = false;

                //Edit Product
                if (!string.IsNullOrEmpty(Request.QueryString["ProductID"]))
                {
                    ProductId = Request.QueryString["ProductID"];

                    CargaDatosProducto();
                    //LoadEditData(ProductId);
                }
                lblFechaDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                PlantillasAntiguas.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "LoadException", "alert('" + ex.Message + "');", true);
            }
        }

        private void LlenaCatalogoCapacidad()
        {
            var tecnologia = (drpTechnology.SelectedIndex == 0 || drpTechnology.SelectedIndex == -1)
                ? ""
                : drpTechnology.SelectedValue;
            var lstCapacidades =
                AdministraProducto.ObtenCatCapacidadSustitucion(int.Parse(tecnologia));

            if (lstCapacidades != null)
            {
                DDLCapacidad.DataSource = lstCapacidades.OrderBy(me => me.No_Capacidad);
                DDLCapacidad.DataTextField = "No_Capacidad";
                DDLCapacidad.DataValueField = "Cve_Capacidad_Sust";
                DDLCapacidad.DataBind();

                DDLCapacidad.Items.Insert(0, new ListItem("", ""));
                DDLCapacidad.SelectedIndex = 0;
            }
        }

        #endregion

        #region Metodos Privados
        /// <summary>
        /// Load Product Information
        /// </summary>
        /// <param name="productId"></param>
        private void LoadEditData(string productId)
        {
            var dtProduct = CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByPK(productId);
            if (dtProduct == null || dtProduct.Rows.Count <= 0) return;
            drpManufacture.SelectedValue = dtProduct.Rows[0]["Cve_Fabricante"].ToString();
            drpTechnology.SelectedValue = dtProduct.Rows[0]["Cve_Tecnologia"].ToString();
            drpTipoProduct.SelectedValue = dtProduct.Rows[0]["Ft_Tipo_Producto"].ToString();
            drpMarca.SelectedValue = dtProduct.Rows[0]["Cve_Marca"].ToString();
            txtNameProduct.Text = dtProduct.Rows[0]["Dx_Nombre_Producto"].ToString();
            txtModel.Text = dtProduct.Rows[0]["Dx_Modelo_Producto"].ToString();
            txtMaxPrice.Text = string.Format("{0:0.00}", dtProduct.Rows[0]["Mt_Precio_Max"]);
            txtEficience.Text = string.Format("{0:0.00}", dtProduct.Rows[0]["No_Eficiencia_Energia"]);
            txtConsumer.Text = string.Format("{0:0.00}", dtProduct.Rows[0]["No_Max_Consumo_24h"]);
            txtAhorroConsumo.Text = string.Format("{0:0.00}", dtProduct.Rows[0]["Ahorro_Consumo"]);
            txtAhorroDemanda.Text = string.Format("{0:0.00}", dtProduct.Rows[0]["Ahorro_Demanda"]);

            // updated by tina 2012-02-24
            if (!string.IsNullOrEmpty(dtProduct.Rows[0]["Cve_Capacidad_Sust"].ToString()))
            {
                drpCapacidad.SelectedValue = dtProduct.Rows[0]["Cve_Capacidad_Sust"].ToString();
            }
            // end
            StatusId = int.Parse(dtProduct.Rows[0]["Cve_Estatus_Producto"].ToString());

            // RSA 2012-09-13 SE attributes Start
            drpSE_TIPO.SelectedValue = dtProduct.Rows[0]["Cve_Tipo_SE"].ToString();
            drpSE_TRANSFORMADOR.SelectedValue = dtProduct.Rows[0]["Cve_Transformador_SE"].ToString();
            drpSE_TRANSFORM_FASE.SelectedValue = dtProduct.Rows[0]["Cve_Fase_Transformador"].ToString();
            drpSE_TRANSFORM_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Marca_Transform"].ToString();
            drpSE_TRANSFORM_RELACION.SelectedValue = dtProduct.Rows[0]["Cve_Relacion_Transform"].ToString();
            drpSE_APARTARRAYO.SelectedValue = dtProduct.Rows[0]["Cve_Apartarrayos_SE"].ToString();
            drpSE_APARTARRAYO_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Marca_Apartar"].ToString();
            drpSE_CORTACIRCUITO.SelectedValue = dtProduct.Rows[0]["Cve_Cortacircuito_SE"].ToString();
            drpSE_CORTACIRC_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Marca_Cortacirc"].ToString();
            drpSE_INTERRUPTOR.SelectedValue = dtProduct.Rows[0]["Cve_Interruptor_SE"].ToString();
            drpSE_INTERRUPTOR_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Marca_Interrup"].ToString();
            drpSE_CONDUCTOR.SelectedValue = dtProduct.Rows[0]["Cve_Conductor_SE"].ToString();
            drpSE_CONDUCTOR_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Marca_Conductor"].ToString();
            drpSE_COND_CONEXION.SelectedValue = dtProduct.Rows[0]["Cve_Cond_Conex_SE"].ToString();
            drpSE_COND_CONEXION_MARCA.SelectedValue = dtProduct.Rows[0]["Cve_Cond_Conex_Mca"].ToString();

            ShowSe(IsSe(drpTechnology.SelectedValue));
            // RSA 2012-09-13 SE attributes End

            var tecnologia = CatalogosTecnologia.ClassInstance.ObtenTecnologiaSeleccionada(int.Parse(drpTechnology.SelectedValue));

            if (tecnologia.CvePlantilla != null)
                IdPlantilla = (int)tecnologia.CvePlantilla;

            var existen =
                  CatalogosPlanilla.ClassInstance.ExistenCustomProducto(IdPlantilla,
                        int.Parse(productId));

            if (existen)
                LLenaCamposCustomizadosProducto();
            else
            {
                LlenaCamposCustomizadosNuevos();
            }

            CreaCamposDinamicos();

            lblMotivos.Visible = true;
            txtMotivos.Visible = true;
        }

        private void ClearData()
        {
            drpManufacture.SelectedIndex = -1;
            drpCapacidad.SelectedIndex = -1;
            drpMarca.SelectedIndex = -1;
            drpTechnology.SelectedIndex = -1;
            drpTipoProduct.SelectedIndex = -1;
            txtConsumer.Text = "";
            txtEficience.Text = "";
            txtMaxPrice.Text = "";
            txtModel.Text = "";
            txtNameProduct.Text = "";

            txtAhorroConsumo.Text = string.Empty;
            txtAhorroDemanda.Text = string.Empty;

            drpSE_TIPO.SelectedIndex = -1;
            drpSE_TRANSFORMADOR.SelectedIndex = -1;
            drpSE_TRANSFORM_FASE.SelectedIndex = -1;
            drpSE_TRANSFORM_MARCA.SelectedIndex = -1;
            drpSE_TRANSFORM_RELACION.SelectedIndex = -1;
            drpSE_APARTARRAYO.SelectedIndex = -1;
            drpSE_APARTARRAYO_MARCA.SelectedIndex = -1;
            drpSE_CORTACIRCUITO.SelectedIndex = -1;
            drpSE_CORTACIRC_MARCA.SelectedIndex = -1;
            drpSE_INTERRUPTOR.SelectedIndex = -1;
            drpSE_INTERRUPTOR_MARCA.SelectedIndex = -1;
            drpSE_CONDUCTOR.SelectedIndex = -1;
            drpSE_CONDUCTOR_MARCA.SelectedIndex = -1;
            drpSE_COND_CONEXION.SelectedIndex = -1;
            drpSE_COND_CONEXION_MARCA.SelectedIndex = -1;
        }
        /// <summary>
        /// return Product monitor page
        /// </summary>

        private void ShowSe(bool show)
        {
            PanelSE.Enabled = show;
            PanelSE.Visible = show;
            PanelNotSE.Visible = !show;
            PanelNotSE.Enabled = !show;
        }
        private bool IsSe(string techId)
        {
            return DxCveCc.ContainsKey(techId) && DxCveCc[techId] == DxCveCcSe;
        }

        private void CargaPlantillaProducto()
        {
            var plantilla = CatalogosTecnologia.OCatPlantilla(IdPlantilla);
            PanelPlantillaComun.Visible = PanelPlantillaBC.Visible = containerSE.Visible = false;

            switch (plantilla.Dx_Descripcion)
            {
                case "PLANTILLA BC":
                    PanelPlantillaBC.Visible = true;
                    PanelCammposCustomizables.Visible = false;
                    break;

                case "PLANTILLA SE":
                    containerSE.Visible = true;
                    PanelCammposCustomizables.Visible = false;
                    break;

                default:
                    PanelPlantillaComun.Visible = true;
                    PanelCammposCustomizables.Visible = true;
                    break;
            }

            if (drpTechnology.SelectedItem.Text.Contains("ILUMINACION"))
            {
                //TxtSistemaArreglo.Visible = true;
                ddlSistemaArreglo.Visible = true;
                LblSistemaArreglo.Visible = true;
                LblCapacidadPlantillaComun.Visible = false;
                DDLCapacidad.Visible = false;
            }
            else
            {
                ddlSistemaArreglo.Visible = false;
                LblSistemaArreglo.Visible = false;
                LblCapacidadPlantillaComun.Visible = true;
                DDLCapacidad.Visible = true;
            }
        }

        private void GuardaProducto()
        {
            try
            {
                var plantilla = CatalogosTecnologia.OCatPlantilla(IdPlantilla);
                var tecnologia = CatalogosTecnologia.BuscaTecnologia(int.Parse(drpTechnology.SelectedValue));
                var isSe = false;
                CAT_MODULOS_SE datosModulosSe = null;
                CAT_PRODUCTO producto = null;

                if (!string.IsNullOrEmpty(Request.QueryString["ProductID"]))
                {
                    var cveProducto = int.Parse(Request.QueryString["ProductID"]);
                    producto = AdminProducto.ObtenProducto(cveProducto);
                }
                else
                {
                    producto = new CAT_PRODUCTO
                    {
                        Cve_Tecnologia = (drpTechnology.SelectedIndex == 0 || drpTechnology.SelectedIndex == -1)
                            ? 0
                            : int.Parse(drpTechnology.SelectedValue),
                        Cve_Estatus_Producto = (int)ProductStatus.ACTIVO,
                        Dt_Fecha_Producto = DateTime.Now
                    };
                }
                

                //Plantilla BC
                switch (plantilla.Dx_Descripcion    )
                {
                    case "PLANTILLA BC":
                        producto.Cve_Fabricante = (DDXFabricanteBC.SelectedIndex == 0 ||
                                                   DDXFabricanteBC.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXFabricanteBC.SelectedValue);
                        producto.Ft_Tipo_Producto = (DDXTipoProductoBC.SelectedIndex == 0 ||
                                                     DDXTipoProductoBC.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXTipoProductoBC.SelectedValue);
                        producto.Cve_Marca = (DDXMarcaBC.SelectedIndex == 0 || DDXMarcaBC.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXMarcaBC.SelectedValue);
                        producto.Dx_Modelo_Producto = TxtModeloBC.Text;
                        producto.Dx_Nombre_Producto = "BANCO DE CAPACITORES";
                        //producto.Cve_Potencia = (DDXPotenciaBC.SelectedIndex == 0 ||
                        //                         DDXPotenciaBC.SelectedIndex == -1)
                        //    ? 0
                        //    : int.Parse(DDXPotenciaBC.SelectedValue);
                        producto.Cve_Capacidad_Sust = (DDXPotenciaBC.SelectedIndex == 0 ||
                                                 DDXPotenciaBC.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXPotenciaBC.SelectedValue);
                        producto.Mt_Precio_Max = (TxtPrecioMaximoBC.Text == "")
                            ? 0
                            : Math.Round(decimal.Parse(TxtPrecioMaximoBC.Text), 4);
                        producto.Cve_Tipo = byte.Parse(DDXTipoEncapsuladoBC.SelectedValue);
                        producto.Dx_Tipo_Dielectrico = TxtTipoDielectrico.Text;
                        producto.Cve_Proteccion_Interna = (DDXProteccionInternaBC.SelectedIndex == 0 ||
                                                           DDXProteccionInternaBC.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXProteccionInternaBC.SelectedValue);
                        producto.Cve_Proteccion_Externa = byte.Parse(DDxTipoProteccionExBC.SelectedValue);
                        producto.Cve_Material = byte.Parse(DDxMaterialCubiertaBC.SelectedValue);
                        producto.Cve_Perdidas = byte.Parse(DDXPerdidasBC.SelectedValue);
                        producto.Cve_Proteccion = byte.Parse(DDxProteccionSCorrienteBC.SelectedValue);
                        producto.Cve_Prot_Contra_Fuego = byte.Parse(DDxProteccionVSFuegoBC.SelectedValue);
                        producto.Cve_Anclaje = byte.Parse(DDxAnclajeBC.SelectedValue);
                        producto.Cve_Terminal_Tierra = byte.Parse(DDxTerminalTierraBC.SelectedValue);
                        producto.Cve_Tipo_Controlador = (DDXTipoControladorBC.SelectedIndex == 0 ||
                                                         DDXTipoControladorBC.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXTipoControladorBC.SelectedValue);
                        producto.Cve_P_Corriente_Controlador = (DDXPriteccionCorrienteBC.SelectedIndex == 0 ||
                                                                DDXPriteccionCorrienteBC.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXPriteccionCorrienteBC.SelectedValue);
                        producto.Cve_P_Sobre_Temperatura = (DDXProteccionTemperaturaBC.SelectedIndex == 0 ||
                                                            DDXProteccionTemperaturaBC.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXProteccionTemperaturaBC.SelectedValue);
                        producto.Cve_P_Sobre_Distorsion_V = (DDXProteccionDistorsionBC.SelectedIndex == 0 ||
                                                             DDXProteccionDistorsionBC.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXProteccionDistorsionBC.SelectedValue);
                        producto.Cve_Bloqueo_Prog_Display = (DDXBloqueoBC.SelectedIndex == 0 ||
                                                             DDXBloqueoBC.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXBloqueoBC.SelectedValue);
                        producto.Cve_Tipo_Comunicacion = byte.Parse(DDXTipoComunicaBC.SelectedValue);
                        producto.Cve_Proteccion_Gabinete = byte.Parse(DDXProteccionGabBC.SelectedValue);
                        producto.Cve_Protec_Contra_Exp = byte.Parse(DDxProteccionVSExplosionBC.SelectedValue);
                        break;
                    case "PLANTILLA SE":
                        datosModulosSe = new CAT_MODULOS_SE();
                        isSe = true;
                        producto.Ft_Tipo_Producto = 11;
                        producto.Cve_Marca = 0;
                        producto.Cve_Fabricante = (DDXFabricanteSE.SelectedIndex == 0 ||
                                                   DDXFabricanteSE.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXFabricanteSE.SelectedValue);
                        producto.Dx_Modelo_Producto = TxtModeloSE.Text;
                        producto.Dx_Nombre_Producto = "SUBESTACION ELECTRICA";
                        producto.Cve_Clase_SE = (DDXClaseSE.SelectedIndex == 0 || DDXClaseSE.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXClaseSE.SelectedValue);
                        producto.Cve_Tipo_SE = (DDXTipoSE.SelectedIndex == 0 || DDXTipoSE.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXTipoSE.SelectedValue);

                        producto.Cve_Marca = (DDXMarcaSE.SelectedIndex == 0 ||
                                                                   DDXMarcaSE.SelectedIndex == -1)
                             ? 0
                             : int.Parse(DDXMarcaSE.SelectedValue); //Nuevo

                        datosModulosSe.Cve_Marca_Transformador = (DDXMarcaSE.SelectedIndex == 0 ||
                                                                  DDXMarcaSE.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXMarcaSE.SelectedValue);

                        producto.Cve_Capacidad_Sust = (DDXCapacidadSE.SelectedIndex == 0 ||
                                                                      DDXCapacidadSE.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXCapacidadSE.SelectedValue); //Nuevo

                        datosModulosSe.Cve_Capacidad_Transformador = (DDXCapacidadSE.SelectedIndex == 0 ||
                                                                      DDXCapacidadSE.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXCapacidadSE.SelectedValue);
                        datosModulosSe.Precio_Transformador = (TxtPrecioSE.Text == "")
                            ? 0
                            : decimal.Parse(TxtPrecioSE.Text);

                        datosModulosSe.Cve_Relacion = (DDXRelTransSE.SelectedIndex == 0 ||
                                                                 DDXRelTransSE.SelectedIndex == -1)
                                                                    ? 0
                                                                    : int.Parse(DDXRelTransSE.SelectedValue);
                        //datosModulosSe.Cve_Marca_Fusible_Subestacion = (DDXMarcaFusibleSE.SelectedIndex == 0 ||
                        //                                                DDXMarcaFusibleSE.SelectedIndex == -1)
                        //    ? 0
                        //    : int.Parse(DDXMarcaFusibleSE.SelectedValue);
                        //datosModulosSe.Cve_Cap_Fusible_Subestacion = (DDXCapacidadFusibleSE.SelectedIndex == 0 ||
                        //                                              DDXCapacidadFusibleSE.SelectedIndex == -1)
                        //    ? 0
                        //    : int.Parse(DDXCapacidadFusibleSE.SelectedValue);
                        datosModulosSe.Precio_Fusible_Subestacion = (TxtPrecioFusibleSE.Text == "")
                            ? 0
                            : decimal.Parse(TxtPrecioFusibleSE.Text);
                        datosModulosSe.Precio_Modulo_Transformador = datosModulosSe.Precio_Transformador +
                                                                     datosModulosSe.Precio_Fusible_Subestacion;
                        datosModulosSe.Precio_Conectores_T = (TxtPrecioConectores.Text == "")
                            ? 0
                            : decimal.Parse(TxtPrecioConectores.Text);
                        datosModulosSe.Precio_Aisladores_Tension = (TxtAisladores.Text == "")
                            ? 0
                            : decimal.Parse(TxtAisladores.Text);
                        datosModulosSe.Precio_Aisladores_Soporte = (TxtAisladoresSoporte.Text == "")
                            ? 0
                            : decimal.Parse(TxtAisladoresSoporte.Text);
                        datosModulosSe.Precio_Cable_Apartarrayos = (TxtCableGuia.Text == "")
                            ? 0
                            : decimal.Parse(TxtCableGuia.Text);
                        datosModulosSe.Precio_Cable_Corta_Circuito = (TxtCableGuiaCorto.Text == "")
                            ? 0
                            : decimal.Parse(TxtCableGuiaCorto.Text);
                        datosModulosSe.Precio_Cable_Transformador = (TxtCableGuiaTransformador.Text == "")
                            ? 0
                            : decimal.Parse(TxtCableGuiaTransformador.Text);
                        datosModulosSe.Precio_Apartarrayo = (TxtApartarayo.Text == "")
                            ? 0
                            : decimal.Parse(TxtApartarayo.Text);
                        datosModulosSe.Precio_Corta_Circuito = (TxtCortaCircuito.Text == "")
                            ? 0
                            : decimal.Parse(TxtCortaCircuito.Text);
                        datosModulosSe.Precio_Fusible = (TxtFusible.Text == "") ? 0 : decimal.Parse(TxtFusible.Text);
                        datosModulosSe.Precio_Cable_Term_Acometida = (TxtCableGuiaConexion.Text == "")
                            ? 0
                            : decimal.Parse(TxtCableGuiaConexion.Text);
                        datosModulosSe.Precio_Cable_SubTerraneo = (TxtCableConexionSubterraneo.Text == "")
                            ? 0
                            : decimal.Parse(TxtCableConexionSubterraneo.Text);
                        datosModulosSe.Precio_Empalmes = (TxtEmpalmes.Text == "")
                            ? 0
                            : decimal.Parse(TxtEmpalmes.Text);

                        datosModulosSe.Precio_Cable_Ambos_Extremos = (TxtExtremos.Text == "")
                            ? 0
                            : decimal.Parse(TxtExtremos.Text);

                        datosModulosSe.Precio_Modulo_Media_Tension = datosModulosSe.Precio_Conectores_T +
                                                                     datosModulosSe.Precio_Aisladores_Tension +
                                                                     datosModulosSe.Precio_Aisladores_Soporte +
                                                                     datosModulosSe.Precio_Cable_Apartarrayos +
                                                                     datosModulosSe.Precio_Cable_Corta_Circuito +
                                                                     datosModulosSe.Precio_Cable_Transformador +
                                                                     datosModulosSe.Precio_Apartarrayo +
                                                                     datosModulosSe.Precio_Corta_Circuito +
                                                                     datosModulosSe.Precio_Fusible +
                                                                     datosModulosSe.Precio_Cable_Term_Acometida +
                                                                     datosModulosSe.Precio_Cable_SubTerraneo +
                                                                     datosModulosSe.Precio_Empalmes +
                                                                     datosModulosSe.Precio_Cable_Ambos_Extremos;
                        datosModulosSe.Precio_Cable_TR = (TxtCableConexionTR.Text == "")
                            ? 0
                            : decimal.Parse(TxtCableConexionTR.Text);
                        datosModulosSe.Precio_Herrajes_Instalacion = (TxtHerrajesInstalacion.Text == "")
                            ? 0
                            : decimal.Parse(TxtHerrajesInstalacion.Text);
                        datosModulosSe.Precio_Poste = (TxtPoste.Text == "") ? 0 : decimal.Parse(TxtPoste.Text);
                        datosModulosSe.Precio_Sistemas_Tierra = (TxtSistemTierra.Text == "")
                            ? 0
                            : decimal.Parse(TxtSistemTierra.Text);
                        datosModulosSe.Precio_Cable_Sistemas_Tierra = (TxtConexionTierra.Text == "")
                            ? 0
                            : decimal.Parse(TxtConexionTierra.Text);
                        datosModulosSe.Precio_Modulo_Acce_Prot = datosModulosSe.Precio_Herrajes_Instalacion +
                                                                 datosModulosSe.Precio_Poste +
                                                                 datosModulosSe.Precio_Sistemas_Tierra +
                                                                 datosModulosSe.Precio_Cable_Sistemas_Tierra;
                       
                        producto.Precio_Total_SE = datosModulosSe.Precio_Modulo_Transformador +
                                                   datosModulosSe.Precio_Modulo_Media_Tension +
                                                   datosModulosSe.Precio_Cable_TR +
                                                   datosModulosSe.Precio_Modulo_Acce_Prot;

                        producto.Mt_Precio_Max = producto.Precio_Total_SE; //Nuevo
                        break;
                    default:
                        producto.Cve_Fabricante = (DDXFabricante.SelectedIndex == 0 || DDXFabricante.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXFabricante.SelectedValue);
                        producto.Ft_Tipo_Producto = (DDXTipoProducto.SelectedIndex == 0 ||
                                                     DDXTipoProducto.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXTipoProducto.SelectedValue);

                        producto.Dx_Nombre_Producto = TxtNombreProducto.Text;
                        producto.Cve_Marca = (DDXMarcaComun.SelectedIndex == 0 || DDXMarcaComun.SelectedIndex == -1)
                            ? 0
                            : int.Parse(DDXMarcaComun.SelectedValue);
                        producto.Dx_Modelo_Producto = TxtModelo.Text;
                        producto.Mt_Precio_Max = (TxtPrecioMaximo.Text == "") ? 0 : decimal.Parse(TxtPrecioMaximo.Text);
                        //producto.No_Eficiencia_Energia = (TxtEficienciaEnergia.Text == "") ? 0 : float.Parse(TxtEficienciaEnergia.Text);
                        if (drpTechnology.SelectedItem.Text.Contains("ILUMINA"))
                        {
                            producto.Cve_Capacidad_Sust = (ddlSistemaArreglo.SelectedIndex == 0 ||
                                                     ddlSistemaArreglo.SelectedIndex == -1)
                            ? Int32.Parse("0")
                            : Int32.Parse(ddlSistemaArreglo.SelectedValue.ToString());
                        }
                        else
                        {
                            producto.Cve_Capacidad_Sust = DDLCapacidad.SelectedValue == "" ? 0 : int.Parse(DDLCapacidad.SelectedValue);
                        }                        
                        
                        break;
                }

                if (!string.IsNullOrEmpty(Request.QueryString["ProductID"]))
                {
                    producto.Cve_Producto = int.Parse(Request.QueryString["ProductID"]);
                    var actualiza = AdministraProducto.ActualizaProducto(producto);

                    if (plantilla.Dx_Descripcion != "PLANTILLA SE" && plantilla.Dx_Descripcion != "PLANTILLA BC")
                    {
                        GuardaCamposCustomizadosProducto();
                    }
                    

                    if (isSe)
                    {
                        datosModulosSe.Cve_Producto = producto.Cve_Producto;
                        actualiza = AdministraProducto.ActualizaModulosSe(datosModulosSe);
                    }

                    if (actualiza)

                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "AddEdit",
                            "alert('Se ha guardado con éxito el producto ')", true);
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "Error",
                            "alert('Ocurrió un problema al Guardar el producto ')", true);
                    }
                }
                else
                {

                    producto.Dx_Producto_Code = tecnologia.Dx_Cve_CC +
                                               LsUtility.GetNumberSequence("ProductCod")
                                                   .ToString(CultureInfo.InvariantCulture)
                                                   .PadLeft(6, '0');

                    var nuevoProducto = AdministraProducto.InsertaProducto(producto);

                   
                    if (nuevoProducto != null)
                    {
                        ProductId = nuevoProducto.Cve_Producto.ToString(CultureInfo.InvariantCulture);

                        if (plantilla.Dx_Descripcion != "PLANTILLA SE" && plantilla.Dx_Descripcion != "PLANTILLA BC")
                        {
                            GuardaCamposCustomizadosProducto();
                        }

                        if (isSe)
                        {
                            datosModulosSe.Cve_Producto = nuevoProducto.Cve_Producto;
                            AdministraProducto.InsertModulosSe(datosModulosSe);
                        }

                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "AddEdit",
                            "alert('Se ha guardado con éxito el producto con el codigo " + ProductId + " ')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "Error",
                            "alert('Ocurrió un problema al Guardar el producto ')", true);
                    }
                }

               // Response.Redirect("ProductMonitor.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "Error",
                            "alert('" + ex.Message + " ')", true);
            }
        }

        private void CargaDatosProducto()
        {
            try
            {
                var producto = AdministraProducto.ObtenProducto(int.Parse(ProductId));

                if (producto != null)
                {
                    drpTechnology.SelectedValue = producto.Cve_Tecnologia.ToString(CultureInfo.InvariantCulture);

                    var tecnologia = CatalogosTecnologia.BuscaTecnologia(producto.Cve_Tecnologia);
                    LlenaCatalogos_idTecnologia();

                    if (tecnologia.Cve_Plantilla != null)
                        IdPlantilla = (int)tecnologia.Cve_Plantilla;

                    var plantilla = CatalogosTecnologia.OCatPlantilla(IdPlantilla);

                    if (plantilla.Dx_Descripcion == "PLANTILLA BC")
                    {
                        LlenarCatalogoProteccionSCorriente();
                        DDXFabricanteBC.SelectedValue = producto.Cve_Fabricante.ToString(CultureInfo.InvariantCulture);

                        DDXTipoProductoBC.SelectedValue = producto.Ft_Tipo_Producto.ToString();
                        DDXMarcaBC.SelectedValue = producto.Cve_Marca.ToString(CultureInfo.InvariantCulture);

                        TxtModeloBC.Text = producto.Dx_Modelo_Producto;
                        DDXPotenciaBC.SelectedValue = producto.Cve_Capacidad_Sust.ToString();

                        TxtPrecioMaximoBC.Text = string.Format("{0:0.00}", producto.Mt_Precio_Max);

                        DDXTipoEncapsuladoBC.SelectedValue = producto.Cve_Tipo.ToString();
                        TxtTipoDielectrico.Text = producto.Dx_Tipo_Dielectrico;
                        DDXProteccionInternaBC.SelectedValue = producto.Cve_Proteccion_Interna.ToString();
                        DDxTipoProteccionExBC.SelectedValue = producto.Cve_Proteccion_Externa.ToString();
                        DDxMaterialCubiertaBC.SelectedValue = producto.Cve_Material.ToString();
                        DDXPerdidasBC.SelectedValue = producto.Cve_Perdidas.ToString();
                        DDXProteccionInternaBC.SelectedValue = producto.Cve_Proteccion_Interna.ToString();
                        DDxProteccionVSFuegoBC.SelectedValue = producto.Cve_Prot_Contra_Fuego.ToString();
                        DDxAnclajeBC.SelectedValue = producto.Cve_Anclaje.ToString();
                        DDxTerminalTierraBC.SelectedValue = producto.Cve_Terminal_Tierra.ToString();
                        DDXTipoControladorBC.SelectedValue = producto.Cve_Tipo_Controlador.ToString();
                        DDXPriteccionCorrienteBC.SelectedValue = producto.Cve_P_Corriente_Controlador.ToString();
                        DDXProteccionTemperaturaBC.SelectedValue = producto.Cve_P_Sobre_Temperatura.ToString();
                        DDXProteccionDistorsionBC.SelectedValue = producto.Cve_P_Sobre_Distorsion_V.ToString();
                        DDXBloqueoBC.SelectedValue = producto.Cve_Bloqueo_Prog_Display.ToString();
                        DDXTipoComunicaBC.SelectedValue = producto.Cve_Tipo_Comunicacion.ToString();
                        DDXProteccionGabBC.SelectedValue = producto.Cve_Proteccion_Gabinete.ToString();
                        DDxProteccionVSExplosionBC.SelectedValue = producto.Cve_Protec_Contra_Exp.ToString();
                        DDxProteccionSCorrienteBC.SelectedValue = producto.Cve_Proteccion.ToString();
                    }
                    else if (plantilla.Dx_Descripcion == "PLANTILLA SE")
                    {
                        var datosModulosSe = AdministraProducto.ObtenModulosSe(int.Parse(ProductId));
                        //isSe = true;

                        DDXFabricanteSE.SelectedValue = producto.Cve_Fabricante.ToString(CultureInfo.InvariantCulture);
                        TxtModeloSE.Text = producto.Dx_Modelo_Producto;
                        DDXClaseSE.SelectedValue = producto.Cve_Clase_SE.ToString();
                        LlenarCatalogoTipo();
                        DDXTipoSE.SelectedValue = producto.Cve_Tipo_SE.ToString();

                        if (datosModulosSe != null)
                        {
                            DDXMarcaSE.SelectedValue = datosModulosSe.Cve_Marca_Transformador.ToString();
                            InitializeDrpCapacidad();
                            DDXCapacidadSE.SelectedValue = datosModulosSe.Cve_Capacidad_Transformador.ToString();
                            TxtPrecioSE.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Transformador);
                            DDXRelTransSE.SelectedValue = datosModulosSe.Cve_Relacion.ToString();

                            //DDXMarcaFusibleSE.SelectedValue = datosModulosSe.Cve_Marca_Fusible_Subestacion.ToString();
                            //DDXCapacidadFusibleSE.SelectedValue = datosModulosSe.Cve_Cap_Fusible_Subestacion.ToString();
                            TxtPrecioFusibleSE.Text = string.Format("{0:0.00}",
                                                                    datosModulosSe.Precio_Fusible_Subestacion);

                            TxtPrecioToTalModTrans.Text = string.Format("{0:0.00}",
                                                                        datosModulosSe.Precio_Modulo_Transformador);
                            
                            //plantilla SE - Modulo media tension
                            TxtPrecioConectores.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Conectores_T);
                            TxtAisladores.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Aisladores_Tension);
                            TxtAisladoresSoporte.Text = string.Format("{0:0.00}",
                                                                      datosModulosSe.Precio_Aisladores_Soporte);
                            TxtCableGuia.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Cable_Apartarrayos);
                            TxtCableGuiaCorto.Text = string.Format("{0:0.00}",
                                                                   datosModulosSe.Precio_Cable_Corta_Circuito);
                            TxtCableGuiaTransformador.Text = string.Format("{0:0.00}",
                                                                           datosModulosSe.Precio_Cable_Transformador);
                            TxtApartarayo.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Apartarrayo);
                            TxtCortaCircuito.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Corta_Circuito);
                            TxtFusible.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Fusible);
                            TxtCableGuiaConexion.Text = string.Format("{0:0.00}",
                                                                      datosModulosSe.Precio_Cable_Term_Acometida);
                            TxtCableConexionSubterraneo.Text = string.Format("{0:0.00}",
                                                                             datosModulosSe.Precio_Cable_SubTerraneo);
                            TxtPrecioTotalMediaTension.Text = string.Format("{0:0.00}",
                                                                            datosModulosSe.Precio_Modulo_Media_Tension);
                            TxtCableConexionTR.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Cable_TR);

                            //Maritza
                            TxtEmpalmes.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Empalmes);
                            TxtExtremos.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Cable_Ambos_Extremos);
                            //

                            TxtHerrajesInstalacion.Text = string.Format("{0:0.00}",
                                                                        datosModulosSe.Precio_Herrajes_Instalacion);

                            TxtPoste.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Poste);
                            TxtSistemTierra.Text = string.Format("{0:0.00}", datosModulosSe.Precio_Sistemas_Tierra);
                            TxtConexionTierra.Text = string.Format("{0:0.00}",
                                                                   datosModulosSe.Precio_Cable_Sistemas_Tierra);
                            TxtPrecioAccProtecciones.Text = string.Format("{0:0.00}",
                                                                          datosModulosSe.Precio_Modulo_Acce_Prot);

                            TxtPrecioTotalSE.Text = string.Format("{0:0.00}", producto.Precio_Total_SE);
                        }
                        DDXTipoSE_SelectedIndexChanged();
                    }
                    else
                    {
                        DDXFabricante.SelectedValue = producto.Cve_Fabricante.ToString(CultureInfo.InvariantCulture);
                        DDXTipoProducto.SelectedValue = producto.Ft_Tipo_Producto.ToString();                       
                        TxtNombreProducto.Text = producto.Dx_Nombre_Producto;
                        DDXMarcaComun.SelectedValue = producto.Cve_Marca.ToString(CultureInfo.InvariantCulture);
                        TxtModelo.Text = producto.Dx_Modelo_Producto;
                        TxtPrecioMaximo.Text = string.Format("{0:0.00}", producto.Mt_Precio_Max);
                        

                        if (drpTechnology.SelectedItem.Text.Contains("ILUMINA"))
                        {
                            LledarDdlSistemaArreglo(int.Parse(drpTechnology.SelectedValue), (int)producto.Ft_Tipo_Producto);
                            ddlSistemaArreglo.SelectedValue = producto.Cve_Capacidad_Sust.ToString();
                            LblCapacidadPlantillaComun.Visible = DDLCapacidad.Visible = false;
                        }
                        else
                        {
                            ddlSistemaArreglo.Visible = LblSistemaArreglo.Visible = false;
                            DDLCapacidad.SelectedValue = producto.Cve_Capacidad_Sust.ToString();
                        }

                        var existen =
                        CatalogosPlanilla.ClassInstance.ExistenCustomProducto(IdPlantilla,
                            int.Parse(ProductId));

                        if (existen)
                            LLenaCamposCustomizadosProducto();
                        else
                        {
                            LlenaCamposCustomizadosNuevos();
                        }

                        CreaCamposDinamicos();
                    }

                    
                    CargaPlantillaProducto();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "Error",
                            "alert('" + ex.Message + " ')", true);
            }
        }

        private void DDXTipoSE_SelectedIndexChanged()
        {
            var clase = DDXClaseSE.SelectedValue != "" ? int.Parse(DDXClaseSE.SelectedValue) : 1;
            var tipo = DDXTipoSE.SelectedValue != "" ? int.Parse(DDXTipoSE.SelectedValue) : 2;
            var muestra = true;

            if (clase == 2)
            {
                if (tipo == 11)
                {
                    muestra = false;

                    LblPoste.Visible = !muestra;
                    TxtPoste.Visible = !muestra;

                    LblPrecioConectores.Visible = TxtPrecioConectores.Visible = muestra;
                    LblAisladores.Visible = TxtAisladores.Visible = muestra;
                    LblAisladoresSoporte.Visible = TxtAisladoresSoporte.Visible = muestra;
                    LblCableGuia.Visible = TxtCableGuia.Visible = muestra;
                    LblCableGuiaCorto.Visible = TxtCableGuiaCorto.Visible = muestra;
                    LblCableGuiaTransformador.Visible = TxtCableGuiaTransformador.Visible = muestra;
                    LblApartarayo.Visible = TxtApartarayo.Visible = muestra;
                    LblCortaCircuito.Visible = TxtCortaCircuito.Visible = muestra;
                    LblFusible.Visible = TxtFusible.Visible = muestra;
                    LabelCableGuia.Visible = TxtCableGuiaConexion.Visible = muestra;
                    LableCableConexion.Visible = TxtCableConexionSubterraneo.Visible = muestra;

                    LblEmpalmes.Visible = TxtEmpalmes.Visible = !muestra;
                    LblCableExtremos.Visible = TxtExtremos.Visible = !muestra;

                    LblDDXMarcaFusibleSE.Visible = TxtPrecioFusibleSE.Visible = !muestra;
                }
                else
                {
                    muestra = true;

                    LblPoste.Visible = muestra;
                    TxtPoste.Visible = muestra;

                    LblPrecioConectores.Visible = TxtPrecioConectores.Visible = muestra;
                    LblAisladores.Visible = TxtAisladores.Visible = muestra;
                    LblAisladoresSoporte.Visible = TxtAisladoresSoporte.Visible = muestra;
                    LblCableGuia.Visible = TxtCableGuia.Visible = muestra;
                    LblCableGuiaCorto.Visible = TxtCableGuiaCorto.Visible = muestra;
                    LblCableGuiaTransformador.Visible = TxtCableGuiaTransformador.Visible = muestra;
                    LblApartarayo.Visible = TxtApartarayo.Visible = muestra;
                    LblCortaCircuito.Visible = TxtCortaCircuito.Visible = muestra;
                    LblFusible.Visible = TxtFusible.Visible = muestra;
                    LabelCableGuia.Visible = TxtCableGuiaConexion.Visible = muestra;
                    LableCableConexion.Visible = TxtCableConexionSubterraneo.Visible = muestra;

                    LblEmpalmes.Visible = TxtEmpalmes.Visible = !muestra;
                    LblCableExtremos.Visible = TxtExtremos.Visible = !muestra;

                    LblDDXMarcaFusibleSE.Visible = TxtPrecioFusibleSE.Visible = muestra;
                }

                LblVerificacionUVIE.Visible = true;
            }
            else
            {
                muestra = true;

                LblPoste.Visible = muestra;
                TxtPoste.Visible = muestra;

                LblPrecioConectores.Visible = TxtPrecioConectores.Visible = muestra;
                LblAisladores.Visible = TxtAisladores.Visible = muestra;
                LblAisladoresSoporte.Visible = TxtAisladoresSoporte.Visible = muestra;
                LblCableGuia.Visible = TxtCableGuia.Visible = muestra;
                LblCableGuiaCorto.Visible = TxtCableGuiaCorto.Visible = muestra;
                LblCableGuiaTransformador.Visible = TxtCableGuiaTransformador.Visible = muestra;
                LblApartarayo.Visible = TxtApartarayo.Visible = muestra;
                LblCortaCircuito.Visible = TxtCortaCircuito.Visible = muestra;
                LblFusible.Visible = TxtFusible.Visible = muestra;
                LabelCableGuia.Visible = TxtCableGuiaConexion.Visible = !muestra;
                LableCableConexion.Visible = TxtCableConexionSubterraneo.Visible = !muestra;

                LblEmpalmes.Visible = TxtEmpalmes.Visible = !muestra;
                LblCableExtremos.Visible = TxtExtremos.Visible = !muestra;

                LblDDXMarcaFusibleSE.Visible = TxtPrecioFusibleSE.Visible = !muestra;

                LblVerificacionUVIE.Visible = tipo != 4;
            }
        }

        #endregion

        /// <summary>
        /// Initialize Manufacture Dropdownlist
        /// </summary>
        #region Llenar DropDonwList

        private void InitializeDrpManufacture()
        {
            //var dtManufacture = CAT_FABRICANTEDal.ClassInstance.Get_All_CAT_FABRICANTE();
            //if (dtManufacture != null && dtManufacture.Rows.Count > 0)
            //{
            //    drpManufacture.DataSource = dtManufacture;
            //    drpManufacture.DataValueField = "Cve_Fabricante";
            //    drpManufacture.DataTextField = "Dx_Nombre_Fabricante";
            //    drpManufacture.DataBind();
            //}
            //drpManufacture.Items.Insert(0, new ListItem("", ""));
            //drpManufacture.SelectedIndex = 0;

            var lstFabricante = AdministraProducto.ObtenCatFabricantes();

            if (lstFabricante != null)
            {
                DDXFabricante.DataSource = lstFabricante;
                DDXFabricante.DataValueField = "Cve_Fabricante";
                DDXFabricante.DataTextField = "Dx_Nombre_Fabricante";
                DDXFabricante.DataBind();
                DDXFabricante.Items.Insert(0, new ListItem("", ""));
                DDXFabricante.SelectedIndex = 0;

                DDXFabricanteBC.DataSource = lstFabricante;
                DDXFabricanteBC.DataValueField = "Cve_Fabricante";
                DDXFabricanteBC.DataTextField = "Dx_Nombre_Fabricante";
                DDXFabricanteBC.DataBind();
                DDXFabricanteBC.Items.Insert(0, new ListItem("", ""));
                DDXFabricanteBC.SelectedIndex = 0;

                DDXFabricanteSE.DataSource = lstFabricante;
                DDXFabricanteSE.DataValueField = "Cve_Fabricante";
                DDXFabricanteSE.DataTextField = "Dx_Nombre_Fabricante";
                DDXFabricanteSE.DataBind();
                DDXFabricanteSE.Items.Insert(0, new ListItem("", ""));
                DDXFabricanteSE.SelectedIndex = 0;

            }
        }
        /// <summary>
        /// Initialize Technology dropdownlist
        /// </summary>
        private void InitializeDrpTechnology()
        {
            var dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIA();
            // RSA 2012-09-11 Create dictionary
            DxCveCc.Clear();
            for (var j = 0; j < dtTechnology.Rows.Count; j++)
            {
                DxCveCc.Add(dtTechnology.Rows[j]["Cve_Tecnologia"].ToString(), dtTechnology.Rows[j]["Dx_Cve_CC"].ToString());
            }
            if (dtTechnology.Rows.Count > 0)
            {
                drpTechnology.DataSource = dtTechnology;
                drpTechnology.DataValueField = "Cve_Tecnologia";
                drpTechnology.DataTextField = "Dx_Nombre_General";
                drpTechnology.DataBind();
            }
            drpTechnology.Items.Insert(0, new ListItem("", ""));
            drpTechnology.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Tipo Of Products dropdownlist
        /// </summary>
        private void InitializeDrpTipoProduct()
        {
            var technology = (drpTechnology.SelectedIndex == 0 || drpTechnology.SelectedIndex == -1) ? "" : drpTechnology.SelectedValue;
            var lstTipoProducto =
              AdministraProducto.ObtenCatTipoProductos(int.Parse(technology));

            if (lstTipoProducto != null)
            {
                DDXTipoProducto.DataSource = lstTipoProducto;
                DDXTipoProducto.DataTextField = "Dx_Tipo_Producto";
                DDXTipoProducto.DataValueField = "Ft_Tipo_Producto";
                DDXTipoProducto.DataBind();
                DDXTipoProducto.Items.Insert(0, new ListItem("", ""));
                DDXTipoProducto.SelectedIndex = 0;


                DDXTipoProductoBC.DataSource = lstTipoProducto;
                DDXTipoProductoBC.DataTextField = "Dx_Tipo_Producto";
                DDXTipoProductoBC.DataValueField = "Ft_Tipo_Producto";
                DDXTipoProductoBC.DataBind();
                DDXTipoProductoBC.Items.Insert(0, new ListItem("", ""));
                DDXTipoProductoBC.SelectedIndex = 0;
            }

            //var dtTipoProduct = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(technology);
            //if (dtTipoProduct != null)// added by tina 2012-02-24
            //{
            //    drpTipoProduct.DataSource = dtTipoProduct;
            //    drpTipoProduct.DataTextField = "Dx_Tipo_Producto";
            //    drpTipoProduct.DataValueField = "Ft_Tipo_Producto";
            //    drpTipoProduct.DataBind();
            //}
            //drpTipoProduct.Items.Insert(0, new ListItem("", ""));
            //drpTipoProduct.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca dropdownlist
        /// </summary>
        private void InitializeDrpMarca()
        {
            //DataTable dtMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_MARCADal();
            //if (dtMarca != null && dtMarca.Rows.Count > 0)
            //{
            //    drpMarca.DataSource = dtMarca;
            //    drpMarca.DataValueField = "Cve_Marca";
            //    drpMarca.DataTextField = "Dx_Marca";
            //    drpMarca.DataBind();
            //}
            //drpMarca.Items.Insert(0, new ListItem("", ""));
            //drpMarca.SelectedIndex = 0;

            var lstMarcas = AdministraProducto.ObtenCatMarcas();

            if (lstMarcas != null)
            {
                DDXMarcaComun.DataSource = lstMarcas;
                DDXMarcaComun.DataValueField = "Cve_Marca";
                DDXMarcaComun.DataTextField = "Dx_Marca";
                DDXMarcaComun.DataBind();
                DDXMarcaComun.Items.Insert(0, new ListItem("", ""));
                DDXMarcaComun.SelectedIndex = 0;

                DDXMarcaBC.DataSource = lstMarcas;
                DDXMarcaBC.DataValueField = "Cve_Marca";
                DDXMarcaBC.DataTextField = "Dx_Marca";
                DDXMarcaBC.DataBind();
                DDXMarcaBC.Items.Insert(0, new ListItem("", ""));
                DDXMarcaBC.SelectedIndex = 0;

                DDXMarcaSE.DataSource = lstMarcas;
                DDXMarcaSE.DataValueField = "Cve_Marca";
                DDXMarcaSE.DataTextField = "Dx_Marca";
                DDXMarcaSE.DataBind();
                DDXMarcaSE.Items.Insert(0, new ListItem("", ""));
                DDXMarcaSE.SelectedIndex = 0;

                //DDXMarcaFusibleSE.DataSource = lstMarcas;
                //DDXMarcaFusibleSE.DataValueField = "Cve_Marca";
                //DDXMarcaFusibleSE.DataTextField = "Dx_Marca";
                //DDXMarcaFusibleSE.DataBind();
                //DDXMarcaFusibleSE.Items.Insert(0, new ListItem("", ""));
                //DDXMarcaFusibleSE.SelectedIndex = 0;
            }
        }

        private void LLenaDrpPotencia()
        {
            var lstPotencia = AdministraProducto.ObtenCatCapacidadSustitucion(int.Parse(drpTechnology.SelectedValue));

            if (lstPotencia != null)
            {
                DDXPotenciaBC.DataSource = lstPotencia;
                DDXPotenciaBC.DataValueField = "Cve_Capacidad_Sust";
                DDXPotenciaBC.DataTextField = "No_Capacidad";
                DDXPotenciaBC.DataBind();
                DDXPotenciaBC.Items.Insert(0, new ListItem("", ""));
                DDXPotenciaBC.SelectedIndex = 0;
            }
        }

        protected void LlenarCatalogoClase()
        {
            var lstClase = AdministraProducto.ObCatSeClases();
            DDXClaseSE.DataSource = lstClase;
            DDXClaseSE.DataValueField = "Cve_Clase";
            DDXClaseSE.DataTextField = "Dx_Clase";
            DDXClaseSE.DataBind();

            DDXClaseSE.Items.Insert(0, new ListItem("", ""));
            DDXClaseSE.SelectedIndex = 0;
        }

        protected void LlenarCatalogoTipo()
        {
            DDXTipoSE.Items.Clear();

            var lstTipo = AdministraProducto.ObSeTipos(DDXClaseSE.SelectedValue);
            DDXTipoSE.DataSource = lstTipo;
            DDXTipoSE.DataValueField = "Cve_Tipo";
            DDXTipoSE.DataTextField = "Dx_Nombre_Tipo";
            DDXTipoSE.DataBind();

            DDXTipoSE.Items.Insert(0, new ListItem("", ""));
            DDXTipoSE.SelectedIndex = 0;
        }

        private void LLenaCatalogosTemp(DropDownList catalogo)
        {
            var lstValorCatalogos = new List<Valor_Catalogo>();
            var valor1 = new Valor_Catalogo { CveValorCatalogo = "1", DescripcionCatalogo = "X" };
            var valor2 = new Valor_Catalogo { CveValorCatalogo = "0", DescripcionCatalogo = "Y" };
            lstValorCatalogos.Add(valor1);
            lstValorCatalogos.Add(valor2);

            catalogo.DataSource = lstValorCatalogos;
            catalogo.DataValueField = "CveValorCatalogo";
            catalogo.DataTextField = "DescripcionCatalogo";
            catalogo.DataBind();
            catalogo.Items.Insert(0, new ListItem("", ""));
            catalogo.SelectedIndex = 0;
        }

        protected void LlenarCatalogoTipoEncapsulado()
        {
            DDXTipoEncapsuladoBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcTipoEncapsulado();
            DDXTipoEncapsuladoBC.DataSource = lstTipo;
            DDXTipoEncapsuladoBC.DataValueField = "Cve_Tipo";
            DDXTipoEncapsuladoBC.DataTextField = "Descripcion";
            DDXTipoEncapsuladoBC.DataBind();

            DDXTipoEncapsuladoBC.Items.Insert(0, new ListItem("", ""));
            DDXTipoEncapsuladoBC.SelectedIndex = 0;
        }

        protected void LlenarCatalogoProteccionInterna()
        {
            DDXProteccionInternaBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcProteccionInterna();
            DDXProteccionInternaBC.DataSource = lstTipo;
            DDXProteccionInternaBC.DataValueField = "Cve_Proteccion_Interna";
            DDXProteccionInternaBC.DataTextField = "Descripcion";
            DDXProteccionInternaBC.DataBind();

            DDXProteccionInternaBC.Items.Insert(0, new ListItem("", ""));
            DDXProteccionInternaBC.SelectedIndex = 0;
        }

        protected void LlenarCatalogoProteccionExterna()
        {
            DDxTipoProteccionExBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcProteccionExterna();
            DDxTipoProteccionExBC.DataSource = lstTipo;
            DDxTipoProteccionExBC.DataValueField = "Cve_Proteccion_Externa";
            DDxTipoProteccionExBC.DataTextField = "Descripcion";
            DDxTipoProteccionExBC.DataBind();

            DDxTipoProteccionExBC.Items.Insert(0, new ListItem("", ""));
            DDxTipoProteccionExBC.SelectedIndex = 0;
        }

        protected void LlenarCatalogoMaterialCubierta()
        {
            DDxMaterialCubiertaBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcMaterialCubierta();
            DDxMaterialCubiertaBC.DataSource = lstTipo;
            DDxMaterialCubiertaBC.DataValueField = "Cve_Material";
            DDxMaterialCubiertaBC.DataTextField = "Descripcion";
            DDxMaterialCubiertaBC.DataBind();

            DDxMaterialCubiertaBC.Items.Insert(0, new ListItem("", ""));
            DDxMaterialCubiertaBC.SelectedIndex = 0;
        }

        protected void LlenarCatalogoPerdidas()
        {
            DDXPerdidasBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcPerdidas();
            DDXPerdidasBC.DataSource = lstTipo;
            DDXPerdidasBC.DataValueField = "Cve_Perdidas";
            DDXPerdidasBC.DataTextField = "Descripcion";
            DDXPerdidasBC.DataBind();

            DDXPerdidasBC.Items.Insert(0, new ListItem("", ""));
            DDXPerdidasBC.SelectedIndex = 0;
        }

        protected void LlenarCatalogoProteccionSCorriente()
        {
            DDxProteccionSCorrienteBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcProteccionSCorriente();
            DDxProteccionSCorrienteBC.DataSource = lstTipo;
            DDxProteccionSCorrienteBC.DataValueField = "Cve_Proteccion";
            DDxProteccionSCorrienteBC.DataTextField = "Descripcion";
            DDxProteccionSCorrienteBC.DataBind();

            DDxProteccionSCorrienteBC.Items.Insert(0, new ListItem("", ""));
            DDxProteccionSCorrienteBC.SelectedIndex = 0;
        }

        protected void LlenarCatalogoProteccionContraFuego()
        {
            DDxProteccionVSFuegoBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcProteccionContraFuego();
            DDxProteccionVSFuegoBC.DataSource = lstTipo;
            DDxProteccionVSFuegoBC.DataValueField = "Cve_Prot_Contra_Fuego";
            DDxProteccionVSFuegoBC.DataTextField = "Descripcion";
            DDxProteccionVSFuegoBC.DataBind();

            DDxProteccionVSFuegoBC.Items.Insert(0, new ListItem("", ""));
            DDxProteccionVSFuegoBC.SelectedIndex = 0;
        }

        protected void LlenarCatalogoProteccionContraExplosion()
        {
            DDxProteccionVSExplosionBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcProteccionContraExplosion();
            DDxProteccionVSExplosionBC.DataSource = lstTipo;
            DDxProteccionVSExplosionBC.DataValueField = "Cve_Protec_Contra_Exp";
            DDxProteccionVSExplosionBC.DataTextField = "Descripcion";
            DDxProteccionVSExplosionBC.DataBind();

            DDxProteccionVSExplosionBC.Items.Insert(0, new ListItem("", ""));
            DDxProteccionVSExplosionBC.SelectedIndex = 0;
        }

        protected void LlenarCatalogoAnclaje()
        {
            DDxAnclajeBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcAnclaje();
            DDxAnclajeBC.DataSource = lstTipo;
            DDxAnclajeBC.DataValueField = "Cve_Anclaje";
            DDxAnclajeBC.DataTextField = "Descripcion";
            DDxAnclajeBC.DataBind();

            DDxAnclajeBC.Items.Insert(0, new ListItem("", ""));
            DDxAnclajeBC.SelectedIndex = 0;
        }

        protected void LlenarCatalogoTerminalTierra()
        {
            DDxTerminalTierraBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcTerminalTierra();
            DDxTerminalTierraBC.DataSource = lstTipo;
            DDxTerminalTierraBC.DataValueField = "Cve_Terminal_Tierra";
            DDxTerminalTierraBC.DataTextField = "Descripcion";
            DDxTerminalTierraBC.DataBind();

            DDxTerminalTierraBC.Items.Insert(0, new ListItem("", ""));
            DDxTerminalTierraBC.SelectedIndex = 0;
        }

        protected void LlenarCatalogoProteccionGabinete()
        {
            DDXProteccionGabBC.Items.Clear();

            var lstTipo = AdministraProducto.ObBcProteccionGabinete();
            DDXProteccionGabBC.DataSource = lstTipo;
            DDXProteccionGabBC.DataValueField = "Cve_Proteccion_Gabinete";
            DDXProteccionGabBC.DataTextField = "Descripcion";
            DDXProteccionGabBC.DataBind();

            DDXProteccionGabBC.Items.Insert(0, new ListItem("", ""));
            DDXProteccionGabBC.SelectedIndex = 0;
        }

        protected void LledarDdlSistemaArreglo(int cveTecnologia, int tipoProducto)
        {
            ddlSistemaArreglo.Items.Clear();
            var lstSistemaArreglo = new OpEquiposAbEficiencia().ObtenCapacidadesSustitucionSa(cveTecnologia, tipoProducto);

            if (lstSistemaArreglo != null)
            {
                ddlSistemaArreglo.DataSource = lstSistemaArreglo.FindAll(me => me.Cve_Capacidad_Sust > 249);
                ddlSistemaArreglo.DataTextField = "Dx_Capacidad";
                ddlSistemaArreglo.DataValueField = "Cve_Capacidad_Sust";
                ddlSistemaArreglo.DataBind();
                ddlSistemaArreglo.Items.Insert(0, new ListItem("", ""));
                ddlSistemaArreglo.SelectedIndex = 0;
            }
        }

        protected void LlenaCatalogos()
        {
            LLenaCatalogosTemp(DDXProteccionGabBC);
        }
        
        private void LLenaCatalogosGenericos()
        {
            var lstValorCatalogos = new List<Valor_Catalogo>();
            var valor1 = new Valor_Catalogo { CveValorCatalogo = "1", DescripcionCatalogo = "Si" };
            var valor2 = new Valor_Catalogo { CveValorCatalogo = "0", DescripcionCatalogo = "No" };
            lstValorCatalogos.Add(valor1);
            lstValorCatalogos.Add(valor2);

            DDXTipoComunicaBC.DataSource = lstValorCatalogos;
            DDXTipoComunicaBC.DataValueField = "CveValorCatalogo";
            DDXTipoComunicaBC.DataTextField = "DescripcionCatalogo";
            DDXTipoComunicaBC.DataBind();
            DDXTipoComunicaBC.Items.Insert(0, new ListItem("", ""));
            DDXTipoComunicaBC.SelectedIndex = 0;

            DDXProteccionInternaBC.DataSource = lstValorCatalogos;
            DDXProteccionInternaBC.DataValueField = "CveValorCatalogo";
            DDXProteccionInternaBC.DataTextField = "DescripcionCatalogo";
            DDXProteccionInternaBC.DataBind();
            DDXProteccionInternaBC.Items.Insert(0, new ListItem("", ""));
            DDXProteccionInternaBC.SelectedIndex = 0;

            DDXPriteccionCorrienteBC.DataSource = lstValorCatalogos;
            DDXPriteccionCorrienteBC.DataValueField = "CveValorCatalogo";
            DDXPriteccionCorrienteBC.DataTextField = "DescripcionCatalogo";
            DDXPriteccionCorrienteBC.DataBind();
            DDXPriteccionCorrienteBC.Items.Insert(0, new ListItem("", ""));
            DDXPriteccionCorrienteBC.SelectedIndex = 0;

            DDXProteccionTemperaturaBC.DataSource = lstValorCatalogos;
            DDXProteccionTemperaturaBC.DataValueField = "CveValorCatalogo";
            DDXProteccionTemperaturaBC.DataTextField = "DescripcionCatalogo";
            DDXProteccionTemperaturaBC.DataBind();
            DDXProteccionTemperaturaBC.Items.Insert(0, new ListItem("", ""));
            DDXProteccionTemperaturaBC.SelectedIndex = 0;

            DDXProteccionDistorsionBC.DataSource = lstValorCatalogos;
            DDXProteccionDistorsionBC.DataValueField = "CveValorCatalogo";
            DDXProteccionDistorsionBC.DataTextField = "DescripcionCatalogo";
            DDXProteccionDistorsionBC.DataBind();
            DDXProteccionDistorsionBC.Items.Insert(0, new ListItem("", ""));
            DDXProteccionDistorsionBC.SelectedIndex = 0;

            DDXBloqueoBC.DataSource = lstValorCatalogos;
            DDXBloqueoBC.DataValueField = "CveValorCatalogo";
            DDXBloqueoBC.DataTextField = "DescripcionCatalogo";
            DDXBloqueoBC.DataBind();
            DDXBloqueoBC.Items.Insert(0, new ListItem("", ""));
            DDXBloqueoBC.SelectedIndex = 0;           


            lstValorCatalogos = new List<Valor_Catalogo>();
            valor1 = new Valor_Catalogo { CveValorCatalogo = "1", DescripcionCatalogo = "Con" };
            valor2 = new Valor_Catalogo { CveValorCatalogo = "0", DescripcionCatalogo = "Sin" };
            lstValorCatalogos.Add(valor1);
            lstValorCatalogos.Add(valor2);

            DDXTipoControladorBC.DataSource = lstValorCatalogos;
            DDXTipoControladorBC.DataValueField = "CveValorCatalogo";
            DDXTipoControladorBC.DataTextField = "DescripcionCatalogo";
            DDXTipoControladorBC.DataBind();
            DDXTipoControladorBC.Items.Insert(0, new ListItem("", ""));
            DDXTipoControladorBC.SelectedIndex = 0;
        }

        private void InitializeDrpCapacidad() // added by Tina 2012-02-24
        {
            var technologyId = drpTechnology.SelectedIndex != -1 && drpTechnology.SelectedIndex != 0 ? Convert.ToInt32(drpTechnology.SelectedValue) : 0;
            var lstCapacidad =
              AdministraProducto.ObtenCatCapacidadSustitucion(technologyId);

            if (lstCapacidad != null)
            {
                DDXCapacidadSE.DataSource = lstCapacidad;
                DDXCapacidadSE.DataValueField = "Cve_Capacidad_Sust";
                DDXCapacidadSE.DataTextField = "No_Capacidad";
                DDXCapacidadSE.DataBind();
                DDXCapacidadSE.Items.Insert(0, new ListItem("", ""));
                DDXCapacidadSE.SelectedIndex = 0;

                //DDXCapacidadFusibleSE.DataSource = lstCapacidad;
                //DDXCapacidadFusibleSE.DataValueField = "Cve_Capacidad_Sust";
                //DDXCapacidadFusibleSE.DataTextField = "No_Capacidad";
                //DDXCapacidadFusibleSE.DataBind();
                //DDXCapacidadFusibleSE.Items.Insert(0, new ListItem("", ""));
                //DDXCapacidadFusibleSE.SelectedIndex = 0;
            }


            //var dtCapacidad = technologyId != 0 ? CAT_CAPACIDAD_SUSTITUCIONDal.ClassInstance.Get_CAT_CAPACIDAD_SUSTITUCIONByTechnology(technologyId) : CAT_CAPACIDAD_SUSTITUCIONDal.ClassInstance.Get_ALL_CAT_CAPACIDAD_SUSTITUCION();
            //if (dtCapacidad != null)
            //{
            //    drpCapacidad.DataSource = dtCapacidad;
            //    drpCapacidad.DataTextField = "No_Capacidad";
            //    drpCapacidad.DataValueField = "Cve_Capacidad_Sust";
            //    drpCapacidad.DataBind();
            //}
            //drpCapacidad.Items.Insert(0, new ListItem("", ""));
            //drpCapacidad.SelectedIndex = 0;
        }

        // RSA 2012-09-12 Initialize SE combos Start
        // Save Product Information
        // Initialize Tipo dropdownlist
        private void InitializeDrpSE_TIPO()
        {
            var dtSeTipo = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_TIPODal();
            if (dtSeTipo != null && dtSeTipo.Rows.Count > 0)
            {
                drpSE_TIPO.DataSource = dtSeTipo;
                drpSE_TIPO.DataValueField = "Cve_Tipo";
                drpSE_TIPO.DataTextField = "Dx_Nombre_Tipo";
                drpSE_TIPO.DataBind();
            }
            drpSE_TIPO.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Transformador dropdownlist
        /// </summary>
        private void InitializeDrpSE_TRANSFORMADOR()
        {
            var dtSeTransformador = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_TRANSFORMADORDal();
            if (dtSeTransformador != null && dtSeTransformador.Rows.Count > 0)
            {
                drpSE_TRANSFORMADOR.DataSource = dtSeTransformador;
                drpSE_TRANSFORMADOR.DataValueField = "Cve_Transformador";
                drpSE_TRANSFORMADOR.DataTextField = "Dx_Dsc_Transformador";
                drpSE_TRANSFORMADOR.DataBind();
            }
            drpSE_TRANSFORMADOR.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Fase del Transformador dropdownlist
        /// </summary>
        private void InitializeDrpSE_TRANSFORM_FASE()
        {
            var dtSeTransformFase = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_TRANSFORM_FASEDal();
            if (dtSeTransformFase != null && dtSeTransformFase.Rows.Count > 0)
            {
                drpSE_TRANSFORM_FASE.DataSource = dtSeTransformFase;
                drpSE_TRANSFORM_FASE.DataValueField = "Cve_Fase";
                drpSE_TRANSFORM_FASE.DataTextField = "Dx_Nombre_Fase";
                drpSE_TRANSFORM_FASE.DataBind();
            }
            // drpSE_TRANSFORM_FASE.Items.Insert(0, new ListItem("", ""));
            drpSE_TRANSFORM_FASE.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca del Transformador dropdownlist
        /// </summary>
        private void InitializeDrpSE_TRANSFORM_MARCA()
        {
            var dtSeTransformMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_TRANSFORM_MARCADal();
            if (dtSeTransformMarca != null && dtSeTransformMarca.Rows.Count > 0)
            {
                drpSE_TRANSFORM_MARCA.DataSource = dtSeTransformMarca;
                drpSE_TRANSFORM_MARCA.DataValueField = "Cve_Marca";
                drpSE_TRANSFORM_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_TRANSFORM_MARCA.DataBind();
            }
            drpSE_TRANSFORM_MARCA.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Relación de Transformación dropdownlist
        /// </summary>
        private void InitializeDrpSE_TRANSFORM_RELACION()
        {
            var dtSeTransformRelacion = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_TRANSFORM_RELACIONDal();
            if (dtSeTransformRelacion != null && dtSeTransformRelacion.Rows.Count > 0)
            {
                DDXRelTransSE.DataSource = dtSeTransformRelacion;
                DDXRelTransSE.DataValueField = "Cve_Relacion";
                DDXRelTransSE.DataTextField = "Dx_Dsc_Relacion";
                DDXRelTransSE.DataBind();
            }
            DDXRelTransSE.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Apartarrayo dropdownlist
        /// </summary>
        private void InitializeDrpSE_APARTARRAYO()
        {
            var dtSeApartarrayo = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_APARTARRAYODal();
            if (dtSeApartarrayo != null && dtSeApartarrayo.Rows.Count > 0)
            {
                drpSE_APARTARRAYO.DataSource = dtSeApartarrayo;
                drpSE_APARTARRAYO.DataValueField = "Cve_Apartarrayo";
                drpSE_APARTARRAYO.DataTextField = "Dx_Dsc_Apartarrayo";
                drpSE_APARTARRAYO.DataBind();
            }
            drpSE_APARTARRAYO.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca del Apartarrayo dropdownlist
        /// </summary>
        private void InitializeDrpSE_APARTARRAYO_MARCA()
        {
            var dtSeApartarrayoMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_APARTARRAYO_MARCADal();
            if (dtSeApartarrayoMarca != null && dtSeApartarrayoMarca.Rows.Count > 0)
            {
                drpSE_APARTARRAYO_MARCA.DataSource = dtSeApartarrayoMarca;
                drpSE_APARTARRAYO_MARCA.DataValueField = "Cve_Marca";
                drpSE_APARTARRAYO_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_APARTARRAYO_MARCA.DataBind();
            }
            drpSE_APARTARRAYO_MARCA.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Cortacircuito – Fusible dropdownlist
        /// </summary>
        private void InitializeDrpSE_CORTACIRCUITO()
        {
            var dtSeCortacircuito = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_CORTACIRCUITODal();
            if (dtSeCortacircuito != null && dtSeCortacircuito.Rows.Count > 0)
            {
                drpSE_CORTACIRCUITO.DataSource = dtSeCortacircuito;
                drpSE_CORTACIRCUITO.DataValueField = "Cve_Cortacircuito";
                drpSE_CORTACIRCUITO.DataTextField = "Dx_Dsc_Cortacircuito";
                drpSE_CORTACIRCUITO.DataBind();
            }
            drpSE_CORTACIRCUITO.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca Cortacircuito – Fusible dropdownlist
        /// </summary>
        private void InitializeDrpSE_CORTACIRC_MARCA()
        {
            var dtSeCortacircMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_CORTACIRC_MARCADal();
            if (dtSeCortacircMarca != null && dtSeCortacircMarca.Rows.Count > 0)
            {
                drpSE_CORTACIRC_MARCA.DataSource = dtSeCortacircMarca;
                drpSE_CORTACIRC_MARCA.DataValueField = "Cve_Marca";
                drpSE_CORTACIRC_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_CORTACIRC_MARCA.DataBind();
            }
            drpSE_CORTACIRC_MARCA.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Interruptor Termomagnético dropdownlist
        /// </summary>
        private void InitializeDrpSE_INTERRUPTOR()
        {
            var dtSeInterruptor = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_INTERRUPTORDal();
            if (dtSeInterruptor != null && dtSeInterruptor.Rows.Count > 0)
            {
                drpSE_INTERRUPTOR.DataSource = dtSeInterruptor;
                drpSE_INTERRUPTOR.DataValueField = "Cve_Interruptor";
                drpSE_INTERRUPTOR.DataTextField = "Dx_Dsc_Interruptor";
                drpSE_INTERRUPTOR.DataBind();
            }
            drpSE_INTERRUPTOR.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca Interruptor Termomagnético dropdownlist
        /// </summary>
        private void InitializeDrpSE_INTERRUPTOR_MARCA()
        {
            var dtSeInterruptorMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_INTERRUPTOR_MARCADal();
            if (dtSeInterruptorMarca != null && dtSeInterruptorMarca.Rows.Count > 0)
            {
                drpSE_INTERRUPTOR_MARCA.DataSource = dtSeInterruptorMarca;
                drpSE_INTERRUPTOR_MARCA.DataValueField = "Cve_Marca";
                drpSE_INTERRUPTOR_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_INTERRUPTOR_MARCA.DataBind();
            }
            drpSE_INTERRUPTOR_MARCA.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Conductor de Tierra dropdownlist
        /// </summary>
        private void InitializeDrpSE_CONDUCTOR()
        {
            var dtSeConductor = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_CONDUCTORDal();
            if (dtSeConductor != null && dtSeConductor.Rows.Count > 0)
            {
                drpSE_CONDUCTOR.DataSource = dtSeConductor;
                drpSE_CONDUCTOR.DataValueField = "Cve_Conductor";
                drpSE_CONDUCTOR.DataTextField = "Dx_Dsc_Conductor";
                drpSE_CONDUCTOR.DataBind();
            }
            drpSE_CONDUCTOR.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca Conductor de Tierra dropdownlist
        /// </summary>
        private void InitializeDrpSE_CONDUCTOR_MARCA()
        {
            var dtSeConductorMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_CONDUCTOR_MARCADal();
            if (dtSeConductorMarca != null && dtSeConductorMarca.Rows.Count > 0)
            {
                drpSE_CONDUCTOR_MARCA.DataSource = dtSeConductorMarca;
                drpSE_CONDUCTOR_MARCA.DataValueField = "Cve_Marca";
                drpSE_CONDUCTOR_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_CONDUCTOR_MARCA.DataBind();
            }
            drpSE_CONDUCTOR_MARCA.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Conductor de Conexión a Centro de Carga dropdownlist
        /// </summary>
        private void InitializeDrpSE_COND_CONEXION()
        {
            var dtSeCondConexion = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_COND_CONEXIONDal();
            if (dtSeCondConexion != null && dtSeCondConexion.Rows.Count > 0)
            {
                drpSE_COND_CONEXION.DataSource = dtSeCondConexion;
                drpSE_COND_CONEXION.DataValueField = "Cve_Conductor_Conex";
                drpSE_COND_CONEXION.DataTextField = "Dx_Dsc_Conductor_Conex";
                drpSE_COND_CONEXION.DataBind();
            }
            drpSE_COND_CONEXION.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca Conductor de Conexión dropdownlist
        /// </summary>
        private void InitializeDrpSE_COND_CONEXION_MARCA()
        {
            var dtSeCondConexionMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_SE_COND_CONEXION_MARCADal();
            if (dtSeCondConexionMarca != null && dtSeCondConexionMarca.Rows.Count > 0)
            {
                drpSE_COND_CONEXION_MARCA.DataSource = dtSeCondConexionMarca;
                drpSE_COND_CONEXION_MARCA.DataValueField = "Cve_Marca";
                drpSE_COND_CONEXION_MARCA.DataTextField = "Dx_Nombre_Marca";
                drpSE_COND_CONEXION_MARCA.DataBind();
            }
            drpSE_COND_CONEXION_MARCA.SelectedIndex = 0;
        }
        // RSA 2012-09-12 Initialize SE combos End

        #endregion

        #region Eventos Controles

        protected void btnSave_Click(object sender, EventArgs e)
        {
            RadWindowManager1.RadConfirm("Confirmar Guardar Producto.¿Esta seguro de Guardar Cambios?.","confirmCallBackFn", 300, 100, null, "Monitor de Productos");
        }


        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            int anterior = ProductoCat.ObtieneProducXid(Convert.ToInt32(Request.QueryString["ProductID"]));
            GuardaProducto();
            int actual = ProductoCat.ObtieneProducXid(Convert.ToInt32(Request.QueryString["ProductID"]));
            var productPreUnitari = ProductoCat.ObtienePorId(Convert.ToInt32(Request.QueryString["ProductID"])) == null ? actual : ProductoCat.ObtienePorId(Convert.ToInt32(Request.QueryString["ProductID"])).Mt_Precio_Unitario;

            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                  Convert.ToInt16(Session["IdRolUserLogueado"]),
                                  Convert.ToInt16(Session["IdDepartamento"]), "PRODUCTOS",
                                  "CAMBIO PRECIO", Request.QueryString["ProductID"],
                                  "", "", "" + anterior, "" + actual);
            //var product = ProductoCat.ProducXid(Convert.ToInt32(Request.QueryString["ProductID"]));
            //if (productPreUnitari > actual)
            //{
            //    product.Cve_Estatus_Producto = (int)ProductStatus.INACTIVO;
            //    ProductoCat.Actualizar(product);
            //}

            int con = 0;
            List<CatProductProveedor> productos = ProductoCat.ObtieneProducDistri(Convert.ToInt32(Request.QueryString["ProductID"]));
            
            List<CatProductProveedor> productos2=productos.FindAll(o => o.StatusProveedor == 2).ToList();

            if (productos2.Count >0) 
            {
                for (int i = 0; i < 1; i++)
                {
                    // MailUtility.ProductModifcado("AumentoPrecioMaxProducto.html", productos[i].correo, Convert.ToString(DateTime.Now), productos[i].representante, productos[i].marca, productos[i].modelo);

                    if (actual < productos2[i].precio)
                    {
                        try
                        {
                            MailUtility.ProductModifcado("ProductoInhabilitado.html", "adrian_car9@msn.com", Convert.ToString(DateTime.Now), productos[i].representante, productos[i].marca, productos[i].modelo);

                        }
                        catch (Exception)
                        {
                            con++;
                        }
                    }
                    if (actual > anterior)
                    {
                        try
                        {
                            MailUtility.ProductModifcado("AumentoPrecioMaxProducto.html", "adrian_car9@msn.com", Convert.ToString(DateTime.Now), productos[i].representante, productos[i].marca, productos[i].modelo);

                        }
                        catch (Exception)
                        {
                            con++;
                        }

                    }
                }

                if (con > 0)
                {
                    RadWindowManager1.RadAlert("PROBLEMAS CON EL CORREO, INTENTE MASTARDE", 300, 150, "Send email.", "confirmCallBackFn2");
                }
                else
                {
                    RadWindowManager1.RadAlert("CORREOS ENVIADOS CORRECTAMENTE!!", 300, 150, "Send email.", "confirmCallBackFn2");
                }
            }
            else
            {
                HiddenButton2_Click(sender, e);
            }


        }

        protected void HiddenButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductMonitor.aspx");
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductMonitor.aspx");
        }
        /// <summary>
        /// Technology changed Initialize Product type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaCatalogos_idTecnologia();
        }

       protected void LlenaCatalogos_idTecnologia()
        {
            InitializeDrpTipoProduct();
            InitializeDrpCapacidad();
            LlenaCatalogoCapacidad();
            LLenaDrpPotencia();
            var tecnologia = CatalogosTecnologia.ClassInstance.ObtenTecnologiaSeleccionada(int.Parse(drpTechnology.SelectedValue));

            if (tecnologia.CvePlantilla != null) IdPlantilla = (int)tecnologia.CvePlantilla;

            LlenaCamposCustomizadosNuevos();
            PanelCammposCustomizables.Controls.Clear();
            CreaCamposDinamicos();
            CargaPlantillaProducto();
        }
        #endregion

        #region Plantilla Dinamica

        private void CreaCamposDinamicos()
        {
            PanelCammposCustomizables.Controls.Add(new LiteralControl("<table width='80%'>"));
            PanelCammposCustomizables.Controls.Add(new LiteralControl("<tr>"));
            PanelCammposCustomizables.Controls.Add(new LiteralControl("<td colspan ='2' align='center' class='trh'>Campos Adicionales</td>"));
            PanelCammposCustomizables.Controls.Add(new LiteralControl("</tr>"));

            var cont = 1;
            foreach (var campoCustomizableProducto in LstCustomizableProductos)
            {
                var etiquetaCampo = new Label { Text = campoCustomizableProducto.DxDescripcionCampo, CssClass = "Label" };

                PanelCammposCustomizables.Controls.Add((cont %= 2) == 0
                    ? new LiteralControl("<tr class='tr1'>")
                    : new LiteralControl("<tr class='tr2'>"));

                PanelCammposCustomizables.Controls.Add(new LiteralControl("<td style='text-align: right'>"));
                PanelCammposCustomizables.Controls.Add(etiquetaCampo);
                PanelCammposCustomizables.Controls.Add(new LiteralControl("</td>"));
                PanelCammposCustomizables.Controls.Add(new LiteralControl("<td style='text-align: center'>"));

                if (campoCustomizableProducto.DxTipo == "Cat")
                {
                    using (var campoCatalogo = ArmaCampoCatalogo(campoCustomizableProducto))
                    {
                        PanelCammposCustomizables.Controls.Add(campoCatalogo);
                    }
                }

                if (campoCustomizableProducto.DxTipo == "Text")
                {
                    using (var campoTexto = ArmaCampoTexto(campoCustomizableProducto))
                    {
                        PanelCammposCustomizables.Controls.Add(campoTexto);
                    }
                }

                PanelCammposCustomizables.Controls.Add(new LiteralControl("</td>"));
                PanelCammposCustomizables.Controls.Add(new LiteralControl("<tr>"));
                cont++;
            }

            PanelCammposCustomizables.Controls.Add(new LiteralControl("</table>"));

        }

        private void LlenaCamposCustomizadosNuevos()
        {
            try
            {
                LstCustomizableProductos =
              CatalogosPlanilla.ClassInstance.ObtenCustomizableProductosNuevo(IdPlantilla);
            }
            catch (Exception)
            {
                LstCustomizableProductos = null;
            }

        }

        private void LLenaCamposCustomizadosProducto()
        {
            try
            {
                LstCustomizableProductos =
              CatalogosPlanilla.ClassInstance.ObtenCustomizableProductos(IdPlantilla,
                    int.Parse(ProductId));
            }
            catch (Exception)
            {
                LstCustomizableProductos = null;
            }

        }

        private TextBox ArmaCampoTexto(CampoCustomizableProducto campo)
        {
            using (
                var campoTexto = new TextBox
                {
                    CssClass = "TextBox",
                    TextMode = TextBoxMode.SingleLine,
                    ID = "Txt_" + campo.CveCampo,
                    ToolTip = campo.ToolTip,
                    Text = campo.Valor
                })
                return campoTexto;
        }

        private DropDownList ArmaCampoCatalogo(CampoCustomizableProducto catalogo)
        {
            var campoCatalogo = new DropDownList
            {
                CssClass = "DropDownList",
                ID = "DDX_" + catalogo.CveCampo,
                DataSource = catalogo.LisValorCatalogos,
                DataValueField = "CveValor",
                DataTextField = "DxValor"
            };
            campoCatalogo.DataBind();

            campoCatalogo.Items.Insert(0, new ListItem("", ""));

            campoCatalogo.SelectedValue = catalogo.Valor != "" ? catalogo.Valor : "";

            return campoCatalogo;
        }

        private RequiredFieldValidator ArmaRequiredFieldValidator(string nombreControl)
        {
            var controlRequerido = new RequiredFieldValidator
            {
                ID = "Required_" + nombreControl,
                ControlToValidate = nombreControl,
                ValidationGroup = "save",
                ErrorMessage = @"(*) Campo Requerido"
            };

            return controlRequerido;
        }

        protected override void CreateChildControls()
        {
            if (Page.IsPostBack)
            {
                if (LstCustomizableProductos != null)
                    CreaCamposDinamicos();
            }
        }

        private void LeerControlesDinamicos()
        {
            foreach (var campoCustomizableProducto in LstCustomizableProductos)
            {
                if (campoCustomizableProducto.DxTipo == "Cat")
                {
                    var catalogo = (DropDownList)PanelCammposCustomizables.FindControl("DDX_" + campoCustomizableProducto.CveCampo);
                    campoCustomizableProducto.Valor = catalogo.SelectedValue;
                }

                if (campoCustomizableProducto.DxTipo == "Text")
                {
                    var campoTexto = (TextBox)PanelCammposCustomizables.FindControl("Txt_" + campoCustomizableProducto.CveCampo);
                    campoCustomizableProducto.Valor = campoTexto.Text;
                }
            }
        }

        private void GuardaCamposCustomizadosProducto()
        {
            List<CampoCustomizableProducto> lstCamposProducto;
            LeerControlesDinamicos();

            if (ProductId != "")
            {
                var existen =
                  CatalogosPlanilla.ClassInstance.ExistenCustomProducto(IdPlantilla,
                        int.Parse(ProductId));

                if (existen)
                {
                    lstCamposProducto =
                      CatalogosPlanilla.ClassInstance.ActualizaCustomizableProductos(
                            LstCustomizableProductos);
                }
                else
                {
                    lstCamposProducto =
                      CatalogosPlanilla.ClassInstance.InsertaCustomizableProductos(
                            LstCustomizableProductos, int.Parse(ProductId));
                }
            }
            else
            {
                var idProductoNew = AccesoDatos.Log.CatProducto.GetIdProducto();
                lstCamposProducto =
                      CatalogosPlanilla.ClassInstance.InsertaCustomizableProductos(
                            LstCustomizableProductos, idProductoNew);
            }

            LstCustomizableProductos = lstCamposProducto;
        }

        #endregion

        protected void DDXClaseSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarCatalogoTipo();
        }

        protected void DDXTipoProductoBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DDXTipoProductoBC.SelectedItem.Text.Contains("BANCO FIJO"))
                PanelPlantillaBC2.Visible = false;

            if (DDXTipoProductoBC.SelectedItem.Text.Contains("BANCO AUTOMATICO"))
                PanelPlantillaBC2.Visible = true;
        }

        protected void DDXFabricanteBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDXTipoProductoBC.Items.Clear();
            var lstTipoProd = CatalogosSolicitud.ObtenTipoProducto(7);
            DDXTipoProductoBC.DataSource = lstTipoProd;
            DDXTipoProductoBC.DataValueField = "Ft_Tipo_Producto";
            DDXTipoProductoBC.DataTextField = "Dx_Tipo_Producto";
            DDXTipoProductoBC.DataBind();

            DDXTipoProductoBC.Items.Insert(0, new ListItem("", ""));
            DDXTipoProductoBC.SelectedIndex = 0;
        }

        protected void DDXTipoSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            var clase = DDXClaseSE.SelectedValue != "" ? int.Parse(DDXClaseSE.SelectedValue) : 1;
            var tipo = DDXTipoSE.SelectedValue != "" ? int.Parse(DDXTipoSE.SelectedValue) : 2;
            var muestra = true;

            if (clase == 2)
            {
                if (tipo == 11)
                {
                    muestra = false;

                    LblPoste.Visible = !muestra;
                    TxtPoste.Visible = !muestra;

                    LblPrecioConectores.Visible = TxtPrecioConectores.Visible = muestra;
                    LblAisladores.Visible = TxtAisladores.Visible = muestra;
                    LblAisladoresSoporte.Visible = TxtAisladoresSoporte.Visible = muestra;
                    LblCableGuia.Visible = TxtCableGuia.Visible = muestra;
                    LblCableGuiaCorto.Visible = TxtCableGuiaCorto.Visible = muestra;
                    LblCableGuiaTransformador.Visible = TxtCableGuiaTransformador.Visible = muestra;
                    LblApartarayo.Visible = TxtApartarayo.Visible = muestra;
                    LblCortaCircuito.Visible = TxtCortaCircuito.Visible = muestra;
                    LblFusible.Visible = TxtFusible.Visible = muestra;
                    LabelCableGuia.Visible = TxtCableGuiaConexion.Visible = muestra;
                    LableCableConexion.Visible = TxtCableConexionSubterraneo.Visible = muestra;

                    LblEmpalmes.Visible = TxtEmpalmes.Visible = !muestra;
                    LblCableExtremos.Visible = TxtExtremos.Visible = !muestra;

                    LblDDXMarcaFusibleSE.Visible = TxtPrecioFusibleSE.Visible = !muestra;
                }
                else
                {
                    muestra = true;

                    LblPoste.Visible = muestra;
                    TxtPoste.Visible = muestra;

                    LblPrecioConectores.Visible = TxtPrecioConectores.Visible = muestra;
                    LblAisladores.Visible = TxtAisladores.Visible = muestra;
                    LblAisladoresSoporte.Visible = TxtAisladoresSoporte.Visible = muestra;
                    LblCableGuia.Visible = TxtCableGuia.Visible = muestra;
                    LblCableGuiaCorto.Visible = TxtCableGuiaCorto.Visible = muestra;
                    LblCableGuiaTransformador.Visible = TxtCableGuiaTransformador.Visible = muestra;
                    LblApartarayo.Visible = TxtApartarayo.Visible = muestra;
                    LblCortaCircuito.Visible = TxtCortaCircuito.Visible = muestra;
                    LblFusible.Visible = TxtFusible.Visible = muestra;
                    LabelCableGuia.Visible = TxtCableGuiaConexion.Visible = muestra;
                    LableCableConexion.Visible = TxtCableConexionSubterraneo.Visible = muestra;

                    LblEmpalmes.Visible = TxtEmpalmes.Visible = !muestra;
                    LblCableExtremos.Visible = TxtExtremos.Visible = !muestra;

                    LblDDXMarcaFusibleSE.Visible = TxtPrecioFusibleSE.Visible = muestra;
                }

                LblVerificacionUVIE.Visible = true;
            }
            else
            {
                muestra = true;

                LblPoste.Visible = muestra;
                TxtPoste.Visible = muestra;

                LblPrecioConectores.Visible = TxtPrecioConectores.Visible = muestra;
                LblAisladores.Visible = TxtAisladores.Visible = muestra;
                LblAisladoresSoporte.Visible = TxtAisladoresSoporte.Visible = muestra;
                LblCableGuia.Visible = TxtCableGuia.Visible = muestra;
                LblCableGuiaCorto.Visible = TxtCableGuiaCorto.Visible = muestra;
                LblCableGuiaTransformador.Visible = TxtCableGuiaTransformador.Visible = muestra;
                LblApartarayo.Visible = TxtApartarayo.Visible = muestra;
                LblCortaCircuito.Visible = TxtCortaCircuito.Visible = muestra;
                LblFusible.Visible = TxtFusible.Visible = muestra;
                LabelCableGuia.Visible = TxtCableGuiaConexion.Visible = !muestra;
                LableCableConexion.Visible = TxtCableConexionSubterraneo.Visible = !muestra;

                LblEmpalmes.Visible = TxtEmpalmes.Visible = !muestra;
                LblCableExtremos.Visible = TxtExtremos.Visible = !muestra;

                LblDDXMarcaFusibleSE.Visible = TxtPrecioFusibleSE.Visible = !muestra;

                LblVerificacionUVIE.Visible = tipo != 4;
            }
        }

        protected void DDXCapacidadSE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DDXRelTransSE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DDXTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            var technology = (drpTechnology.SelectedIndex == 0 || drpTechnology.SelectedIndex == -1) ? 0 : int.Parse(drpTechnology.SelectedValue);
            var tipoProducto = (DDXTipoProducto.SelectedIndex == 0 || DDXTipoProducto.SelectedIndex == -1) ? 0 : int.Parse(DDXTipoProducto.SelectedValue);//DDXTipoProducto

            LledarDdlSistemaArreglo(technology,tipoProducto);

        }



   

    }
}
