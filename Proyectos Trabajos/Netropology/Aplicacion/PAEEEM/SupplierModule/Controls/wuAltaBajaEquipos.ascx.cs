using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.AccesoDatos.AltaBajaEquipos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.Entidades.Alta_Equipos;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.LogicaNegocios.Trama;
using Telerik.Web.UI;

namespace WebProyectoEquipos
{
    public partial class wuAltaBajaEquipos : UserControl
    {
        #region Region de Atributos

        //LISTA DE PRODUCTO DE BAJA EFICIENCIA
        private List<EquipoBajaEficiencia> EBajaEficiencia
        {
            get
            {
                return ViewState["PRODUCTOS_BE"] == null
                           ? new List<EquipoBajaEficiencia>()
                           : ViewState["PRODUCTOS_BE"] as List<EquipoBajaEficiencia>;
            }
            set { ViewState["PRODUCTOS_BE"] = value; }
        }

        //LISTA DE PRODUCTO DE ALTA EFICIENCIA
        private List<EquipoAltaEficiencia> EAltaEficiencia
        {
            get
            {
                return ViewState["PRODUCTOS_AE"] == null
                           ? new List<EquipoAltaEficiencia>()
                           : ViewState["PRODUCTOS_AE"] as List<EquipoAltaEficiencia>;
            }
            set { ViewState["PRODUCTOS_AE"] = value; }
        }


        private EquipoBajaEficiencia EBajaEficienciaSeleccionado
        {
            get
            {
                return ViewState["PBAJAEFICIENCIASELECCIONADO"] == null ?
                    null : //new EquipoBajaEficiencia() :
                    ViewState["PBAJAEFICIENCIASELECCIONADO"] as EquipoBajaEficiencia;
            }
            set { ViewState["PBAJAEFICIENCIASELECCIONADO"] = value; }
        }

        private EquipoBajaEficiencia EBajaEficienciaAgrupar1
        {
            get
            {
                return (EquipoBajaEficiencia)ViewState["PBAJAEFICIENCIAAGRUPAR1"];
            }
            set { ViewState["PBAJAEFICIENCIAAGRUPAR1"] = value; }
        }

        private EquipoBajaEficiencia EBajaEficienciaAgrupar2
        {
            get
            {
                return (EquipoBajaEficiencia)ViewState["PBAJAEFICIENCIAAGRUPAR2"];
            }
            set { ViewState["PBAJAEFICIENCIAAGRUPAR2"] = value; }
        }

        public string UserId
        {
            get
            {
                return ViewState["UserId"] == null ? "" : ViewState["UserId"].ToString();
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }

        public string IdProveedor
        {
            get
            {
                return ViewState["idProveedor"] == null ? "" : ViewState["idProveedor"].ToString();
            }
            set
            {
                ViewState["idProveedor"] = value;
            }
        }

        public CompTarifa Tarifas
        {
            get { return ViewState["TARIFA"] == null ? new CompTarifa() : ViewState["TARIFA"] as CompTarifa; }
            set { ViewState["TARIFA"] = value; }
        }

        public string TipoTarifa
        {
            get { return (string)ViewState["TIPO_TARIFA"]; }
            set { ViewState["TIPO_TARIFA"] = value; }
        }

        public decimal FDeg
        {
            get { return ViewState["FDEG"] == null ? 0.0M : Convert.ToDecimal(ViewState["FDEG"]); }
            set { ViewState["FDEG"] = value; }
        }

        public decimal FBio
        {
            get { return ViewState["FBIO"] == null ? 0.0M : Convert.ToDecimal(ViewState["FBIO"]); }
            set { ViewState["FBIO"] = value; }
        }

        public decimal Fmes
        {
            get { return ViewState["FMES"] == null ? 0.0M : Convert.ToDecimal(ViewState["FBIO"]); }
            set { ViewState["FEMES"] = value; }
        }

        public decimal Horas
        {
            get { return ViewState["HORAS"] == null ? 0.0M : Convert.ToDecimal(ViewState["HORAS"]); }
            set { ViewState["HORAS"] = value; }
        }

        public List<Presupuesto> Presupuesto
        {
            get
            {
                return ViewState["PRESUPUESTO"] == null ? new List<Presupuesto>() : ViewState["PRESUPUESTO"] as List<Presupuesto>;
            }

            set { ViewState["PRESUPUESTO"] = value; }
        }


        #endregion

        public string RPU { get; set; }
        public string IdCliente { get; set; }
        public string CveTarifa { get; set; }

        public const int ElementoSubestacionesElectricas = 6;
        public const int ElementoRefrigeracion = 1;
        public const int ElementoIluminacionLineal = 3;
        public const int ElementoIluminacionLed = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
           
                RPU = "001960400999";
                IdCliente = "359";
                IdProveedor = "359";
                CveTarifa = "3";

                if (!Page.IsPostBack)
                {

                    //CargaInformacionGeneral();
                    CargaTecnologias();

                    rgEquiposBaja.MasterTableView.GroupByExpressions.Add(new GridGroupByExpression("Group By Dx_Tecnologia"));

                    GridGroupByExpression expression = new GridGroupByExpression();
                    GridGroupByField gridGroupByField = new GridGroupByField();

                    gridGroupByField = new GridGroupByField();
                    gridGroupByField.FieldName = "Dx_Tecnologia";
                    gridGroupByField.FormatString = "{0}";
                    expression.SelectFields.Add(gridGroupByField);

                    gridGroupByField = new GridGroupByField();
                    gridGroupByField.FieldName = "Dx_Tecnologia";
                    expression.GroupByFields.Add(gridGroupByField);

                    rgEquiposBaja.MasterTableView.GroupByExpressions.Add(expression);


                    GridSortExpression ordenamiento = new GridSortExpression();
                    ordenamiento.FieldName = "Cve_Grupo";
                    ordenamiento.SetSortOrder("Ascending");
                    rgEquiposBaja.MasterTableView.SortExpressions.AddSortExpression(ordenamiento);

                    EstatusEquipoBaja();
                    fSEA.Visible = false;
                    RealizaTarifa();

                    
                    //Presupuesto = new OpEquiposAbEficiencia().ObtieneConceptosPresupuesto();
                    rgPresupuesto.DataSource = Presupuesto;
                    rgPresupuesto.DataBind();                    
                    
                }                       




        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MuestraBotonesGrid();
        }


        public void MuestraBotonesGrid()
        {
            if (EBajaEficiencia.Count > 0)
            {
                btnAgrupar.Visible = true;
                btnDesagrupar.Visible = true;
            }
            else
            {
                btnAgrupar.Visible = false;
                btnDesagrupar.Visible = false;
            }
        }


        public void RealizaTarifa()
        {
            
                var parseo = (ParseoTrama)Session["PARSEO_TRAMA"];

                //REALIZA LAS OPERACIONES ADECUADAS DE ACUERDO A LA TARIFA
                var tipoTarifa = "03";//parseo.ComplexParseo.InformacionGeneral.Conceptos.First(p => p.Id == 4).Dato;

                TipoTarifa = tipoTarifa.ToUpper();

                switch (tipoTarifa.ToUpper())
                {
                    case "02":
                        break;
                    case "03":
                        //Tarifas = new AlgoritmoTarifa03(parseo.ComplexParseo).T03;
                        break;
                    case "HM":
                        //Tarifas = new AlgoritmoTarifaHM(parseo.ComplexParseo).Thm;
                        break;
                    case "OM":
                        //Tarifas = new AlgoritmoTarifaOM(parseo.ComplexParseo).Tom;
                        break;
                }

                //AQUI DEBE DE IR EL BLOQUE DE RECIBIR LAS PROPIEDADES DE CADA TARIFA PARA VALIDARLA



                //OBTENCION DEL FACTOR DEGRADACION 
                FBio = new RegionesMunFactBio().ObtienePorCondicion(p => p.IDREGION == 2 && p.IDESTADO == 1 && p.IDMUNICIPIO == 1).FACTOR_BIOCLIMATICO;
                var idRegBioclima = new Estado().ObtienePorCondicion(p => p.Cve_Estado == 1).IDREGION_BIOCLIMA;
                FDeg = Convert.ToDecimal(new RegionesBioclimaticas().ObtienePorCondicion(p => p.IDREGION_BIOCLIMA == idRegBioclima).FDEGRC);
                Fmes = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 12).VALOR);            
                //Horas = Convert.ToDecimal(new )
        }


        public void CargaInformacionGeneral()
        {            
                var user = (US_USUARIOModel)Session["UserInfo"];
                //UserId = user.Id_Usuario.ToString(CultureInfo.InvariantCulture);

                // estas dos lineas son solo para cuando se desarrolla
                var dtCreditTemp = K_CREDITO_TEMPDAL.ClassInstance.Get_K_Credito_Temp("632"); //user.Id_Usuario.ToString(CultureInfo.InvariantCulture));
                txtServiceCode.Text = dtCreditTemp.Rows[0]["No_RPU"].ToString();
                Session["ValidRPU"] = dtCreditTemp.Rows[0]["No_RPU"].ToString();

                if (dtCreditTemp == null || dtCreditTemp.Rows.Count <= 0) return;
                //added by tina 2012-07-26
                if (Session["ValidRPU"] == null || Session["ValidRPU"].ToString() != dtCreditTemp.Rows[0]["No_RPU"].ToString())
                {
                    return;
                }

                txtDX_NOMBRE_COMERCIAL.Text = dtCreditTemp.Rows[0]["Dx_Nombre_Comercial"].ToString();
                txtDX_DOMICILIO_FISC.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Fisc_Calle"] + " " + dtCreditTemp.Rows[0]["Dx_Domicilio_Fisc_Num"];
                txtDX_CP_FISC.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Fisc_CP"].ToString();

                DataRow tipoIndustria = CAT_TIPO_INDUSTRIADal.ClassInstance.Get_CAT_TIPO_INDUSTRIAByID(int.Parse(dtCreditTemp.Rows[0]["Cve_Tipo_Industria"].ToString()));
                txtDX_TIPO_INDUSTRIA.Text = tipoIndustria["Dx_Tipo_Industria"].ToString();            
        }


        //METODO PARA CARGAR INICIALMENTE LAS TECNOLOGIAS
        private void CargaTecnologias()
        {            
            cboTecnologias.DataSource = new OpEquiposAbEficiencia().Tecnologias(int.Parse(CveTarifa));
            cboTecnologias.DataValueField = "IdElemento";
            cboTecnologias.DataTextField = "Elemento";
            cboTecnologias.DataBind();            
        }       


        #region Logica de Equipo de Baja Eficiencia

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            List<EquipoBajaEficiencia> lista = EBajaEficiencia;
            EquipoBajaEficiencia producto = new EquipoBajaEficiencia();
            producto.ID = FindMaxValue<EquipoBajaEficiencia>(EBajaEficiencia, m => m.ID) + 1;
            producto.Cve_Grupo = FindMaxValue<EquipoBajaEficiencia>(EBajaEficiencia, m => m.Cve_Grupo) + 1;
            producto.Dx_Grupo = IndiceAColumna(producto.Cve_Grupo);
            producto.Cve_Tecnologia = int.Parse(cboTecnologias.SelectedValue);
            producto.Dx_Tecnologia = cboTecnologias.SelectedItem.Text;
            producto.Ft_Tipo_Producto = -1;
            producto.Dx_Tipo_Producto = "";
            producto.Cve_Consumo = -1;
            producto.Dx_Consumo = "";
            PAEEEM.Entidades.ABE_UNIDAD_TECNOLOGIA unidad = new OpEquiposAbEficiencia().UnidadTecnologica(int.Parse(cboTecnologias.SelectedValue));
            producto.Cve_Unidad = unidad.IDUNIDAD;
            producto.Dx_Unidad = unidad.UNIDAD;
            producto.DetalleTecnologia = new Tecnologia().ValidaEquipoParaAlta(int.Parse(cboTecnologias.SelectedValue));
            if (producto.Cve_Tecnologia == ElementoSubestacionesElectricas)
                producto.Tarifa = cboSistema.SelectedItem.Text;
            else
                producto.Tarifa = "";
            producto.Cantidad = 0;
            lista.Add(producto);
            EBajaEficiencia = lista;
            rgEquiposBaja.DataSource = EBajaEficiencia;
            rgEquiposBaja.DataBind();

            EstatusEquipoBaja();

        }


        protected void rgEquiposBaja_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                rgEquiposBaja.DataSource = EBajaEficiencia;
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }
        }


        protected void rgEquiposBaja_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                var item = e.Item as GridEditableItem;
                if (item != null)
                {
                    var keyName = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());

                    if (EBajaEficiencia != null)
                    {
                        var equipobajaEficiencia = EBajaEficiencia.First(p => p.ID == keyName);

                        if (equipobajaEficiencia.Cve_Tecnologia == ElementoRefrigeracion)
                        {
                            EBajaEficiencia.Where(p => p.ID == keyName).Select(p =>
                            {
                                p.Ft_Tipo_Producto = int.Parse(((RadComboBox)item.FindControl("cboFt_Tipo_Producto")).SelectedValue);
                                p.Dx_Tipo_Producto = ((RadComboBox)item.FindControl("cboFt_Tipo_Producto")).Text;
                                p.Cve_Consumo = int.Parse(((RadNumericTextBox)item.FindControl("txtCve_Capacidad")).Text);
                                p.Dx_Consumo = ((RadNumericTextBox)item.FindControl("txtCve_Capacidad")).Text;
                                p.Cantidad = int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
                                return p;
                            }).ToList();
                        }
                        else
                        {
                            EBajaEficiencia.Where(p => p.ID == keyName).Select(p =>
                            {
                                p.Ft_Tipo_Producto = int.Parse(((RadComboBox)item.FindControl("cboFt_Tipo_Producto")).SelectedValue);
                                p.Dx_Tipo_Producto = ((RadComboBox)item.FindControl("cboFt_Tipo_Producto")).Text;
                                p.Cve_Consumo = int.Parse(((RadComboBox)item.FindControl("cboCve_Capacidad")).SelectedValue);
                                p.Dx_Consumo = ((RadComboBox)item.FindControl("cboCve_Capacidad")).Text;
                                p.Cantidad = int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
                                return p;
                            }).ToList();
                        }

                        var EquipoBaja = EBajaEficiencia.First(p => p.ID == keyName);

                        //EL SIGUIENTE BLOQUE DE CODIDGO SIRVE PARA REALIZAR EL PROCESO DE                         
                        CalculosAhorroTecnologia(EquipoBaja);

                    }

                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }
        }


        protected void rgEquiposBaja_DeleteCommand(object sender, GridCommandEventArgs e)
        {

            try
            {

                var item = (GridEditableItem)e.Item;

                if (item != null)
                {
                    var keyName = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());

                    EBajaEficienciaSeleccionado = EBajaEficiencia.Find(p => p.ID == keyName);
                    var productoBajaEficiencia = EBajaEficiencia.Find(p => p.ID == keyName);
                    int grupo = productoBajaEficiencia.Cve_Grupo;
                    EBajaEficiencia.RemoveAll(p => p.ID == productoBajaEficiencia.ID);


                    foreach (EquipoBajaEficiencia equipo in EBajaEficiencia)
                    {
                        if (equipo.Cve_Grupo > grupo)
                        {
                            equipo.Cve_Grupo -= 1;
                            equipo.Dx_Grupo = IndiceAColumna(equipo.Cve_Grupo);
                        }
                    }

                    //VALIDACION DE EQUIPO DE ALTA EFICIENCIA VISUALIZADO
                    if (EAltaEficiencia.Count > 0)
                    {
                        var equiposAlta = EAltaEficiencia.FirstOrDefault(p => p.ID_Baja == EBajaEficienciaSeleccionado.ID);
                        if (equiposAlta != null)
                        {
                            EAltaEficiencia = new List<EquipoAltaEficiencia>();
                            rgEquiposAlta.DataSource = new List<EquipoAltaEficiencia>();
                            rgEquiposAlta.DataBind();
                            fSEA.Visible = false;
                        }
                    }
                    else
                    {
                        rgEquiposAlta.MasterTableView.ClearEditItems();
                        fSEA.Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }


        }


        protected void rgEquiposBaja_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;
                var productoBajaEficiencia = EBajaEficiencia.Find(p => p.ID == int.Parse(item.GetDataKeyValue("ID").ToString()));
                int idTecnologia = productoBajaEficiencia.Cve_Tecnologia;

                if (!(e.Item is IGridInsertItem))
                {
                    //                    
                    var cboFt_Tipo_Producto = (RadComboBox)item.FindControl("cboFt_Tipo_Producto");
                    cboFt_Tipo_Producto.DataSource = new OpEquiposAbEficiencia().ProductosBajaEficiencia(idTecnologia);
                    cboFt_Tipo_Producto.DataTextField = "Elemento";
                    cboFt_Tipo_Producto.DataValueField = "IdElemento";
                    cboFt_Tipo_Producto.DataBind();
                   

                    var idTiproducto = DataBinder.Eval(e.Item.DataItem, "Ft_Tipo_Producto").ToString();
                    cboFt_Tipo_Producto.SelectedValue = idTiproducto;


                    if (productoBajaEficiencia.Cve_Tecnologia == 1)
                    {
                        var cboCve_Capacidad = item.FindControl("cboCve_Capacidad") as RadComboBox;
                        var txtCve_Capacidad = item.FindControl("txtCve_Capacidad") as RadNumericTextBox;
                        txtCve_Capacidad.Visible = true;
                        cboCve_Capacidad.Visible = false;
                    }
                    else
                    {
                        var txtCve_Capacidad = item.FindControl("txtCve_Capacidad") as RadNumericTextBox;
                        txtCve_Capacidad.Visible = false;

                        //SOLO PARA TECNOLOGIAS DE AIRE ACONDICIONADO Y MOTORES ELECTRICOS
                        if (productoBajaEficiencia.Cve_Tecnologia == 2 || productoBajaEficiencia.Cve_Tecnologia == 4)
                        {
                            var cboCve_Capacidad = item.FindControl("cboCve_Capacidad") as RadComboBox;
                           // cboCve_Capacidad.DataSource = new OpEquiposAbEficiencia().ProductosConsumosAA_ME(productoBajaEficiencia.Cve_Tecnologia);
                            cboCve_Capacidad.DataTextField = "Elemento";
                            cboCve_Capacidad.DataValueField = "IdElemento";
                            cboCve_Capacidad.DataBind();

                            cboCve_Capacidad.SelectedValue = "-1";
                            cboCve_Capacidad.Visible = true;
                        }
                        else if (productoBajaEficiencia.Cve_Tecnologia == 3)
                        {
                            cboFt_Tipo_Producto.AutoPostBack = true;
                            cboFt_Tipo_Producto.SelectedIndexChanged += cboFt_Tipo_Producto_SelectedIndexChanged;
                            var cboCve_Capacidad = item.FindControl("cboCve_Capacidad") as RadComboBox;
                            cboCve_Capacidad.DataSource = new OpEquiposAbEficiencia().ProductosConsumos(int.Parse(cboFt_Tipo_Producto.SelectedValue));
                            cboCve_Capacidad.DataTextField = "Elemento";
                            cboCve_Capacidad.DataValueField = "IdElemento";
                            cboCve_Capacidad.DataBind();

                            var idCapacidad = DataBinder.Eval(e.Item.DataItem, "Cve_Consumo").ToString();
                            cboCve_Capacidad.SelectedValue = idCapacidad;
                            cboCve_Capacidad.Visible = true;                           

                        }                        
                        
                                                
                    }


                }              

            }
        }
       

        protected void rgEquiposBaja_ItemCommand(object sender, GridCommandEventArgs e)
        {            

            if (e.CommandName == "Seleccionar")
            {
                EBajaEficienciaSeleccionado = EBajaEficiencia.Find(p => p.ID == int.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString()));

                GridDataItem item = (GridDataItem)e.Item;
                var eBajaEficiencia = EBajaEficiencia.Find(p => p.ID == int.Parse(item.GetDataKeyValue("ID").ToString()));

                item.Selected = true;
                legEquiposAlta.InnerText = "Equipos de Alta Eficiencia  - " + eBajaEficiencia.Dx_Tecnologia + " - Grupo " + eBajaEficiencia.Dx_Grupo;

                var equiposAlta = false;
                //VALIDACION PARA VISUALIZAR EQUIPOS DE ALTA
                if (eBajaEficiencia.DetalleTecnologia.CveTipoMovimiento == "1")
                {
                    equiposAlta = true;
                }
                else if (eBajaEficiencia.DetalleTecnologia.CveTipoMovimiento == "2")
                {
                    if (eBajaEficiencia.Cve_Tecnologia == ElementoRefrigeracion)
                    {
                        if (eBajaEficiencia.Ft_Tipo_Producto != -1 && eBajaEficiencia.Cantidad > 0)
                        {
                            equiposAlta = true;
                        }
                    }
                    else
                    {
                        if (eBajaEficiencia.Ft_Tipo_Producto != -1 && eBajaEficiencia.Cve_Consumo != -1 && eBajaEficiencia.Cantidad > 0)
                            equiposAlta = true;
                    }
                }

                

                VerEquiposAlta(equiposAlta);

            }
            else if (e.CommandName == "Edit")
            {
                var EquipoB = EBajaEficiencia.Find(p => p.ID == int.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString()));

                if (EquipoB.DetalleTecnologia.CveTipoMovimiento == "1")
                {
                    e.Canceled = true;
                    rwmVentana.RadAlert("En equipos de adquision no hay captura de equipo de baja eficiencia.", 300, 150, "Equipo de Alta Eficiencia", null);                    
                }

            }
            else
            {
                return;
            }


        }


        private void VerEquiposAlta(bool equiposAlta)
        {
            if (equiposAlta)
            {
                fSEA.Visible = true;
                rgEquiposAlta.DataSource = EAltaEficiencia;
                rgEquiposAlta.DataBind();
            }
            else
            {
                fSEA.Visible = false;
                rgEquiposAlta.DataSource = new List<EquipoAltaEficiencia>();
                rgEquiposAlta.DataBind();
            }

        }


        protected void cboFt_Tipo_Producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var cboFt_Tipo_Producto = sender as RadComboBox;

                if (cboFt_Tipo_Producto != null)
                {
                    var editedItem = cboFt_Tipo_Producto.NamingContainer as GridEditableItem;

                    if (editedItem != null)
                    {
                        var cboCapacidad = editedItem["Cve_Consumo"].FindControl("cboCve_Capacidad") as RadComboBox;
                        if (cboCapacidad != null)
                        {
                            cboCapacidad.DataSource = new OpEquiposAbEficiencia().ProductosConsumos(int.Parse(cboFt_Tipo_Producto.SelectedValue));
                            cboCapacidad.DataTextField = "Elemento";
                            cboCapacidad.DataValueField = "IdElemento";
                            cboCapacidad.DataBind();

                            cboCapacidad.SelectedValue = "-1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }

        }       


        protected void cboTecnologias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(cboTecnologias.SelectedValue) == ElementoSubestacionesElectricas)
            {
                lblSistema.Visible = true;
                cboSistema.Visible = true;
            }
            {
                lblSistema.Visible = false;
                cboSistema.Visible = false;
            }
        }


        protected void chkAgrupar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridDataItem editedItem = (sender as CheckBox).NamingContainer as GridDataItem;

                if ((EBajaEficienciaAgrupar1 != null && EBajaEficienciaAgrupar2 != null)
                   && (sender as CheckBox).Checked)
                {
                    (sender as CheckBox).Checked = false;
                    return;
                }

                EquipoBajaEficiencia productoBajaEficiencia = EBajaEficiencia.Find(p => p.ID == int.Parse(editedItem.GetDataKeyValue("ID").ToString()));

                // Validar si Aplica la agrupacion,
                // solo iluminacion y refrigeracion

                if (productoBajaEficiencia.Cve_Tecnologia != ElementoRefrigeracion &&
                    productoBajaEficiencia.Cve_Tecnologia != ElementoIluminacionLineal &&
                    productoBajaEficiencia.Cve_Tecnologia != ElementoIluminacionLed)
                {
                    // Mostrar mensaje de que no se selecciono una tecnologia apropiada para agrupar
                    (sender as CheckBox).Checked = false;
                    return;
                }


                if (EBajaEficienciaAgrupar1 == null)
                {
                    EBajaEficienciaAgrupar1 = productoBajaEficiencia;

                }
                else
                {
                    // Solo se pueden agrupar productos de la misma tecnologia
                    if (productoBajaEficiencia.Cve_Tecnologia == EBajaEficienciaAgrupar1.Cve_Tecnologia)
                    {
                        EBajaEficienciaAgrupar2 = productoBajaEficiencia;
                    }
                    else
                    {
                        (sender as CheckBox).Checked = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }

        }


        protected void btnAgrupar_Click(object sender, EventArgs e)
        {
            try
            {
                int indice = EBajaEficiencia.FindIndex(p => p.ID == EBajaEficienciaAgrupar2.ID);//ProductosBajaEficiencia.IndexOf(ProductoBajaEficienciaAgrupar2);

                EBajaEficiencia[indice].Cve_Grupo = EBajaEficienciaAgrupar1.Cve_Grupo;
                EBajaEficiencia[indice].Dx_Grupo = EBajaEficienciaAgrupar1.Dx_Grupo;

                EBajaEficienciaAgrupar1 = null;
                EBajaEficienciaAgrupar2 = null;

                EstatusEquipoBaja();
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }

        }


        protected void btnDesagrupar_Click(object sender, EventArgs e)
        {
            try
            {
                int indice = EBajaEficiencia.FindIndex(p => p.ID == EBajaEficienciaAgrupar1.ID);

                // Desagrupa solo si hay mas de 1 elemento en el grupo
                if (EBajaEficiencia.FindAll(p => p.Cve_Grupo == EBajaEficienciaAgrupar1.Cve_Grupo).Count > 1)
                {
                    EBajaEficiencia[indice].Cve_Grupo = FindMaxValue<EquipoBajaEficiencia>(EBajaEficiencia, m => m.ID) + 1; ;
                    EBajaEficiencia[indice].Dx_Grupo = IndiceAColumna(EBajaEficiencia[indice].Cve_Grupo);
                }

                EBajaEficienciaAgrupar1 = null;
                EBajaEficienciaAgrupar2 = null;

                EstatusEquipoBaja();
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }

        }


        public void EstatusEquipoBaja()
        {
            EBajaEficienciaSeleccionado = null;
            EBajaEficienciaAgrupar1 = null;
            EBajaEficienciaAgrupar2 = null;
        }


        public int FindMaxValue<T>(List<T> list, Converter<T, int> projection)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            int maxValue = int.MinValue;
            foreach (T item in list)
            {
                int value = projection(item);
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }
            return maxValue;
        }


        public static string IndiceAColumna(int indice)
        {
            int ColumnaBase = 26;
            int DigitosMax = 7;
            string Digitos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (indice <= 0)
                throw new IndexOutOfRangeException("El indice debe ser un número positvo");

            if (indice <= ColumnaBase)
                return Digitos[indice - 1].ToString();

            var sb = new StringBuilder().Append(' ', DigitosMax);
            var actual = indice;
            var offset = DigitosMax;
            while (actual > 0)
            {
                sb[--offset] = Digitos[--actual % ColumnaBase];
                actual /= ColumnaBase;
            }
            return sb.ToString(offset, DigitosMax - offset);
        }


        public float Valor(string valor)
        {
            float ret = 0;
            float.TryParse(valor, out ret);
            return ret;
        }


        private void CalculosAhorroTecnologia(EquipoBajaEficiencia equipoBaja, int idEquipoBaja = 0)
        {
            switch (equipoBaja.Cve_Tecnologia)
            {
                case 1: //REFRIGERACION
                    var rf = equipoBaja.RC;
                    equipoBaja.RC = new RefrigeracionComercial();
                    
                    //equipoBaja.RC.FDeg = FDeg;
                    //equipoBaja.RC.FBio = FBio;
                    //equipoBaja.RC.FMes = Fmes;
                    //equipoBaja.RC.Horas =;

                    ////DATOS DEL EQUIPO DE BAJA
                    //equipoBaja.RC.NoEb = equipoBaja.Cantidad;
                    //equipoBaja.RC.AEb = ;
                    //equipoBaja.RC.CapEb = Convert.ToDecimal(equipoBaja.Dx_Consumo);
                    

                    //if (idEquipoBaja > 0)
                    //{
                    //    //DATOS DEL EQUIPO DE ALTA
                    //    equipoBaja.RC.NoEa =;
                    //    equipoBaja.RC.AEa =;
                    //    equipoBaja.RC.CapEa =;
                    //}



                    break;
                case 2: // AIRE ACONDICIONADO
                    break;
                case 3: //ILUMINACION LINEAL
                    break;
                case 4: // MOTORES ELECTRICOS
                    break;
            }
        }


        #endregion


        #region Logica y Eventos de Equipo de Alta Eficiencia


        protected void rgEquiposAlta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                rgEquiposAlta.DataSource = EAltaEficiencia;
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }
        }

        
        protected void rgEquiposAlta_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                var item = e.Item as GridEditableItem;

                if (item != null)
                {
                    var equipoBe = EBajaEficiencia.First(p => p.ID == EBajaEficienciaSeleccionado.ID);
                    var equipoAe = new EquipoAltaEficiencia();

                    if (ValidaAltaEquipo(equipoBe.DetalleTecnologia))
                    {
                        if (equipoBe.EquiposAltaEficiencia != null)
                            if (equipoBe.EquiposAltaEficiencia.Count > 0)
                                equipoAe.ID = equipoBe.EquiposAltaEficiencia.Max(p => p.ID) + 1;
                            else
                                equipoAe.ID = 1;
                        else
                        {
                            equipoAe.ID = 1;
                            equipoBe.EquiposAltaEficiencia = new List<EquipoAltaEficiencia>();
                        }


                        equipoAe.ID_Baja = equipoBe.ID;
                        equipoAe.Cve_Marca = int.Parse(((RadComboBox)item.FindControl("cboCve_Marca")).SelectedValue);
                        equipoAe.Dx_Marca = ((RadComboBox)item.FindControl("cboCve_Marca")).Text;
                        equipoAe.Cve_Modelo = int.Parse(((RadComboBox)item.FindControl("cboCve_Modelo")).SelectedValue);
                        equipoAe.Dx_Modelo = ((RadComboBox)item.FindControl("cboCve_Modelo")).Text;
                        equipoAe.Cantidad = int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
                        equipoAe.Precio_Distribuidor = Convert.ToDecimal(string.IsNullOrEmpty(((Label)item.FindControl("lblPrecio_DistribuidorEdicion")).Text) ? "0" :
                                                       ((Label)item.FindControl("lblPrecio_DistribuidorEdicion")).Text);
                        equipoAe.Precio_Unitario = Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtPrecio_Unitario")).Text);
                        equipoAe.Importe_Total_Sin_IVA = Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtPrecio_Unitario")).Text);
                        equipoAe.Gasto_Instalacion = Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtGasto_Instalacion")).Text);

                        equipoBe.EquiposAltaEficiencia.Add(equipoAe);
                        EAltaEficiencia = equipoBe.EquiposAltaEficiencia;

                        rgEquiposAlta.DataSource = EAltaEficiencia;
                        rgEquiposAlta.DataBind();

                        ResumenPresupuesto();
                    }
                    else
                    {
                        rwmVentana.RadAlert("No puede agregar mas equipos de acuerdo a lo establecido", 300, 150, "Equipo de Alta Eficiencia", null);
                    }
                }

            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }
        }

        
        protected void rgEquiposAlta_UpdateCommand(object sender, GridCommandEventArgs e)
        {

            try
            {
                var item = e.Item as GridEditableItem;

                if (item != null)
                {
                    var keyName = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());


                    if (EAltaEficiencia != null)
                    {
                        EAltaEficiencia.Where(p => p.ID == keyName).Select
                        (p =>
                        {
                            p.Cve_Marca = int.Parse(((RadComboBox)item.FindControl("cboCve_Marca")).SelectedValue);
                            p.Dx_Marca = ((RadComboBox)item.FindControl("cboCve_Marca")).Text;
                            p.Cve_Modelo = int.Parse(((RadComboBox)item.FindControl("cboCve_Modelo")).SelectedValue);
                            p.Dx_Modelo = ((RadComboBox)item.FindControl("cboCve_Modelo")).Text;
                            p.Cantidad = int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
                            p.Precio_Distribuidor = Convert.ToDecimal(((Label)item.FindControl("lblPrecio_DistribuidorEdicion")).Text);
                            p.Precio_Unitario = Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtPrecio_Unitario")).Text);
                            p.Importe_Total_Sin_IVA = Convert.ToDecimal(((Label)item.FindControl("lblImporte_Total_Sin_IVAEdicion")).Text);
                            p.Gasto_Instalacion = Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtGasto_Instalacion")).Text);
                            return p;
                        }
                        ).ToList();
                        

                        EBajaEficiencia.First(p => p.ID == EBajaEficienciaSeleccionado.ID).EquiposAltaEficiencia = EAltaEficiencia;

                        rgEquiposAlta.DataSource = EAltaEficiencia;
                        rgEquiposAlta.DataBind();

                        ResumenPresupuesto();
                    }

                }

            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }

        }


        protected void rgEquiposAlta_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                var item = e.Item as GridEditableItem;

                if (item != null)
                {
                    var keyName = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());

                    if (EAltaEficiencia != null)
                    {
                        EAltaEficiencia.RemoveAll(p => p.ID == keyName);
                        rgEquiposAlta.DataSource = EAltaEficiencia;
                        rgEquiposAlta.DataBind();

                        ResumenPresupuesto();
                    }

                }

            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }
        }


        protected void rgEquiposAlta_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                if (!ValidaAltaEquipo(EBajaEficienciaSeleccionado.DetalleTecnologia))
                {
                    e.Canceled = true;
                    rwmVentana.RadAlert("No puede agregar mas equipos de acuerdo a lo establecido", 300, 150, "Equipo de Alta Eficiencia", null);
                }
            }
        }


        protected void rgEquiposAlta_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                if (!(e.Item is IGridInsertItem))
                {

                    //CONFIGURACION DEL COMBOBOX MARCA

                    var cboCveMarca = ((RadComboBox)item.FindControl("cboCve_Marca"));
                    cboCveMarca.DataSource = new OpEquiposAbEficiencia().MarcasAltaEficiencia(EBajaEficienciaSeleccionado.Cve_Tecnologia, int.Parse(IdProveedor));
                    cboCveMarca.DataTextField = "Elemento";
                    cboCveMarca.DataValueField = "IdElemento";
                    cboCveMarca.DataBind();

                    var idMarca = DataBinder.Eval(e.Item.DataItem, "Cve_Marca").ToString();
                    var itemMarca = cboCveMarca.FindItemByValue(idMarca.Trim());
                    cboCveMarca.SelectedValue = itemMarca.Value;


                    //CONFIGURACION DEL COMBOBOX MODELO
                    var idBaja = ((Label)item.FindControl("lbIdBaja")).Text;

                    var equipoBajaEficiencia = EBajaEficiencia.First(p => p.ID == int.Parse(idBaja));

                    var cboCveModelo = item.FindControl("cboCve_Modelo") as RadComboBox;
                    cboCveModelo.DataSource = new OpEquiposAbEficiencia().ModelosAltaEficiencia(
                            int.Parse(equipoBajaEficiencia.Cve_Tecnologia.ToString(CultureInfo.InvariantCulture)),
                            int.Parse(cboCveMarca.SelectedValue),
                            int.Parse(IdProveedor));
                    cboCveModelo.DataTextField = "Elemento";
                    cboCveModelo.DataValueField = "IdElemento";
                    cboCveModelo.DataBind();

                    var idModelo = DataBinder.Eval(e.Item.DataItem, "Cve_Modelo").ToString();
                    var itemModelo = cboCveModelo.FindItemByValue(idModelo);

                    cboCveModelo.SelectedValue = itemModelo.Value;


                }
                else
                {
                    var cboCveMarca = ((RadComboBox)item.FindControl("cboCve_Marca"));
                    cboCveMarca.DataSource = new OpEquiposAbEficiencia().MarcasAltaEficiencia(EBajaEficienciaSeleccionado.Cve_Tecnologia, int.Parse(IdProveedor));
                    cboCveMarca.DataTextField = "Elemento";
                    cboCveMarca.DataValueField = "IdElemento";
                    cboCveMarca.DataBind();

                    //var idMarca = DataBinder.Eval(e.Item.DataItem, "Cve_Marca").ToString();
                    //var itemMarca = cboCveMarca.FindItemByValue(idMarca.Trim());
                    cboCveMarca.SelectedValue = "-1";

                    var cboCveModelo = item.FindControl("cboCve_Modelo") as RadComboBox;
                    cboCveModelo.Items.Clear();
                    cboCveModelo.Items.Insert(0, new RadComboBoxItem("Seleccione", "-1"));

                    rgEquiposBaja.DataSource = EBajaEficiencia;
                    rgEquiposBaja.DataBind();

                }

            }
        }
        

        protected void cboCve_Marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var cboMarca = sender as RadComboBox;

                if (cboMarca != null)
                {
                    var editedItem = cboMarca.NamingContainer as GridEditableItem;

                    if (editedItem != null)
                    {
                        var cboModelo = editedItem["Cve_Modelo"].FindControl("cboCve_Modelo") as RadComboBox;


                        if (cboModelo != null)
                        {
                            cboModelo.DataSource = new OpEquiposAbEficiencia().ModelosAltaEficiencia(
                                                    int.Parse(EBajaEficienciaSeleccionado.Cve_Tecnologia.ToString(CultureInfo.InvariantCulture)),
                                                    int.Parse(cboMarca.SelectedValue),
                                                    int.Parse(IdProveedor));
                            cboModelo.DataTextField = "Elemento";
                            cboModelo.DataValueField = "IdElemento";
                            cboModelo.DataBind();

                            cboModelo.SelectedValue = "-1";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }



        }


        protected void cboCve_Modelo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                var cboModelo = sender as RadComboBox;

                if (cboModelo != null)
                {
                    var editedItem = cboModelo.NamingContainer as GridEditableItem;

                    if (editedItem != null)
                    {

                        var lblPrecioDistribuidorEdicion = editedItem.FindControl("lblPrecio_DistribuidorEdicion") as Label;

                        lblPrecioDistribuidorEdicion.Text = new OpEquiposAbEficiencia().PrecioDistribuidorProductoAltaEficiencia(
                                int.Parse(cboModelo.SelectedValue),
                                int.Parse(IdProveedor));
                    }

                }


            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }
        }


        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var editedItem = (sender as RadNumericTextBox).NamingContainer as GridEditableItem;
                var txtCantidad = (RadNumericTextBox)editedItem.FindControl("txtCantidad");
                var txtPrecioUnitario = (RadNumericTextBox)editedItem.FindControl("txtPrecio_Unitario");
                var lblImporteTotalSinIvaEdicion = (Label)editedItem.FindControl("lblImporte_Total_Sin_IVAEdicion");

                lblImporteTotalSinIvaEdicion.Text = (Valor(txtCantidad.Text) * Valor(txtPrecioUnitario.Text)).ToString();
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }

        }


        protected void txtPrecio_Unitario_TextChanged(object sender, EventArgs e)
        {

            try
            {
                var editedItem = (sender as RadNumericTextBox).NamingContainer as GridEditableItem;
                var txtCantidad = (RadNumericTextBox)editedItem.FindControl("txtCantidad");
                var txtPrecioUnitario = (RadNumericTextBox)editedItem.FindControl("txtPrecio_Unitario");
                var lblImporteTotalSinIvaEdicion = (Label)editedItem.FindControl("lblImporte_Total_Sin_IVAEdicion");

                lblImporteTotalSinIvaEdicion.Text = (Valor(txtCantidad.Text) * Valor(txtPrecioUnitario.Text)).ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }

        }


        private bool ValidaAltaEquipo(CompDetalleTecnologia detalleTecnologia)
        {            
                        
            bool realizaInsercion = false;

            if (detalleTecnologia.DxEquipoAlta.ToUpper() == "MUCHOS")
                realizaInsercion = true;
            else
            {
                var totalEquiposAlta = 0;
                bool esnumero = int.TryParse(detalleTecnologia.DxEquipoAlta, out totalEquiposAlta);
                if (esnumero)
                {
                    if (EAltaEficiencia.Count < totalEquiposAlta)
                        realizaInsercion = true;
                }
                
            }

            return realizaInsercion;
        }

        #endregion


        public void ResumenPresupuesto()
        {
            var todosEA = EBajaEficiencia.SelectMany(p => p.EquiposAltaEficiencia).ToList();
            

            //SUBTOTAL
            Presupuesto.First(p => p.IdPresupuesto == 1).Resultado = 36;
            //Iva
            Presupuesto.First(p => p.IdPresupuesto == 2).Resultado = 23;
            //Costo Equipo(s)
            Presupuesto.First(p => p.IdPresupuesto == 3).Resultado = 37;
            //Costo Acopio y Destrucción
            Presupuesto.First(p => p.IdPresupuesto == 4).Resultado = 47;
            //Incentivo 10%
            Presupuesto.First(p => p.IdPresupuesto == 5).Resultado = 74;
            //Descuento
            Presupuesto.First(p => p.IdPresupuesto == 6).Resultado = 47;
            //Gastos de Instalación
            Presupuesto.First(p => p.IdPresupuesto == 7).Resultado = todosEA.Sum(p => p.Gasto_Instalacion);
            //Total
            Presupuesto.First(p => p.IdPresupuesto == 8).Resultado = 74;


            rgPresupuesto.DataSource = Presupuesto;
            rgPresupuesto.DataBind();
        }

    }
}