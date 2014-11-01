using System.Globalization;
using System.Linq;
using System.Web.UI;
using PAEEEM.AccesoDatos.AltaBajaEquipos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades.Alta_Equipos;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using PAEEEM.LogicaNegocios.Tarifas;
using PAEEEM.LogicaNegocios.Trama;
using Telerik.Web.UI;

namespace PAEEEM.SupplierModule
{


    public partial class wucRegistroEquipos : System.Web.UI.UserControl
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
            get { return ViewState["FDEG"] == null ?0.0M : Convert.ToDecimal(ViewState["FDEG"]) ; }
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
            try
            {
                RPU = "001960400999";
                IdCliente = "359";
                IdProveedor = "359";
                CveTarifa = "3";

                if (!Page.IsPostBack)
                {

                    //CargaInformacionGeneral();
                    CargaTecnologias();

                    //rgEquiposBaja.MasterTableView.GroupByExpressions.Add(new GridGroupByExpression("Group By Dx_Tecnologia"));

                    GridGroupByExpression expression = new GridGroupByExpression();
                    GridGroupByField gridGroupByField = new GridGroupByField();

                    gridGroupByField = new GridGroupByField();
                    gridGroupByField.FieldName = "Dx_Tecnologia";
                    gridGroupByField.FormatString = "{0}";
                    expression.SelectFields.Add(gridGroupByField);

                    gridGroupByField = new GridGroupByField();
                    gridGroupByField.FieldName = "Dx_Tecnologia";
                    expression.GroupByFields.Add(gridGroupByField);

                    //rgEquiposBaja.MasterTableView.GroupByExpressions.Add(expression);


                    GridSortExpression ordenamiento = new GridSortExpression();
                    ordenamiento.FieldName = "Cve_Grupo";
                    ordenamiento.SetSortOrder("Ascending");
                    //rgEquiposBaja.MasterTableView.SortExpressions.AddSortExpression(ordenamiento);

                    EstatusEquipoBaja();
                    fSEA.Visible = false;
                    RealizaTarifa();
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos", null);
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

            try
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


                //OBTENCION DEL FACTOR DEGRADACION 
                //FBio = new RegionesMunFactBio().ObtienePorCondicion(p => p.IDREGION == 2 && p.IDESTADO == 1 && p.IDMUNICIPIO == 1).FACTOR_BIOCLIMATICO;
                //var idRegBioclima = new Estado().ObtienePorCondicion(p => p.Cve_Estado == 1).IDREGION_BIOCLIMA;
                //FDeg = Convert.ToDecimal(new RegionesBioclimaticas().ObtienePorCondicion(p => p.IDREGION_BIOCLIMA == idRegBioclima).FDEGRC);
                //Fmes = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 12).VALOR);
            }            
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos", null);
            }          

            
        }


        public void CargaInformacionGeneral()
        {
            try
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
                txtDX_DOMICILIO_FISC.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Fisc_Calle"].ToString() + " " + dtCreditTemp.Rows[0]["Dx_Domicilio_Fisc_Num"].ToString();
                txtDX_CP_FISC.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Fisc_CP"].ToString();

                DataRow tipoIndustria = CAT_TIPO_INDUSTRIADal.ClassInstance.Get_CAT_TIPO_INDUSTRIAByID(int.Parse(dtCreditTemp.Rows[0]["Cve_Tipo_Industria"].ToString()));
                txtDX_TIPO_INDUSTRIA.Text = tipoIndustria["Dx_Tipo_Industria"].ToString();            
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos", null);
            }
            
        }


        //METODO PARA CARGAR INICIALMENTE LAS TECNOLOGIAS
        private void CargaTecnologias()
        {
            try
            {
                cboTecnologias.DataSource = new OpEquiposAbEficiencia().Tecnologias(int.Parse(CveTarifa));
                cboTecnologias.DataValueField = "IdElemento";
                cboTecnologias.DataTextField = "Elemento";
                cboTecnologias.DataBind();
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos", null);
            }

            
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
            Entidades.ABE_UNIDAD_TECNOLOGIA unidad = new OpEquiposAbEficiencia().UnidadTecnologica(int.Parse(cboTecnologias.SelectedValue));
            producto.Cve_Unidad = unidad.IDUNIDAD;
            producto.Dx_Unidad = unidad.UNIDAD;
            if (producto.Cve_Tecnologia == ElementoSubestacionesElectricas)
                producto.Tarifa = cboSistema.SelectedItem.Text;
            else
                producto.Tarifa = "";
            producto.Cantidad = 0;
            lista.Add(producto);
            EBajaEficiencia = lista;
            //rgEquiposBaja.DataSource = EBajaEficiencia;
            //rgEquiposBaja.DataBind();

            EstatusEquipoBaja();

        }
       
        private void VerEquiposAlta(bool equiposAlta)
        {
            if (equiposAlta)
            {
                fSEA.Visible = true;
                //rgEquiposAlta.DataSource = EAltaEficiencia;
                //rgEquiposAlta.DataBind();
            }
            else
            {
                fSEA.Visible = false;
                //rgEquiposAlta.DataSource = new List<EquipoAltaEficiencia>();
                //rgEquiposAlta.DataBind();
            }

        }


        protected void cboFt_Tipo_Producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridDataItem editedItem = (sender as RadComboBox).NamingContainer as GridDataItem;
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
        

        #endregion

               
        #region Logica y Eventos de Equipo de Alta Eficiencia       
      

        //protected void rgEquiposAlta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    try
        //    {
        //        rgEquiposAlta.DataSource = EAltaEficiencia;
        //    }
        //    catch (Exception ex)
        //    {
        //        rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
        //    }
        //}


        //protected void rgEquiposAlta_UpdateCommand(object sender, GridCommandEventArgs e)
        //{

        //    try
        //    {
        //        var item = e.Item as GridEditableItem;

        //        if (item != null)
        //        {
        //            var keyName = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());


        //            if (EAltaEficiencia != null)
        //            {
        //                EAltaEficiencia.Where(p => p.ID == keyName).Select
        //                (p =>
        //                {
        //                    p.Cve_Marca = int.Parse(((RadComboBox)item.FindControl("cboCve_Marca")).SelectedValue);
        //                    p.Dx_Marca = ((RadComboBox)item.FindControl("cboCve_Marca")).Text;
        //                    p.Cve_Modelo = int.Parse(((RadComboBox)item.FindControl("cboCve_Modelo")).SelectedValue);
        //                    p.Dx_Modelo = ((RadComboBox)item.FindControl("cboCve_Modelo")).Text;
        //                    p.Cantidad = int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
        //                    p.Precio_Distribuidor = Convert.ToDecimal(((Label)item.FindControl("lblPrecio_DistribuidorEdicion")).Text);
        //                    p.Precio_Unitario = Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtPrecio_Unitario")).Text);
        //                    p.Importe_Total_Sin_IVA = Convert.ToDecimal(((Label)item.FindControl("lblImporte_Total_Sin_IVAEdicion")).Text);
        //                    p.Gasto_Instalacion = Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtGasto_Instalacion")).Text);
        //                    return p;
        //                }
        //                ).ToList();

        //                rgEquiposAlta.DataSource = EAltaEficiencia;
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
        //    }

        //}


        //protected void rgEquiposAlta_DeleteCommand(object sender, GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        var item = e.Item as GridEditableItem;

        //        if (item != null)
        //        {
        //            var keyName = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());

        //            if (EAltaEficiencia != null)
        //            {                       
        //                EAltaEficiencia.RemoveAll(p => p.ID == keyName);
        //                rgEquiposAlta.DataSource = EAltaEficiencia;
        //                rgEquiposAlta.DataBind();
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
        //    }
        //}        


        //protected void rgEquiposAlta_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item.IsInEditMode)
        //    {
        //        var item = (GridEditableItem)e.Item;

        //        if (!(e.Item is IGridInsertItem))
        //        {

        //            //CONFIGURACION DEL COMBOBOX MARCA

        //            var cboCveMarca = ((RadComboBox)item.FindControl("cboCve_Marca"));
        //            cboCveMarca.DataSource = new OpEquiposAbEficiencia().MarcasAltaEficiencia(EBajaEficienciaSeleccionado.Cve_Tecnologia, int.Parse(IdProveedor));
        //            cboCveMarca.DataTextField = "Elemento";
        //            cboCveMarca.DataValueField = "IdElemento";
        //            cboCveMarca.DataBind();

        //            var idMarca = DataBinder.Eval(e.Item.DataItem, "Cve_Marca").ToString();
        //            var itemMarca = cboCveMarca.FindItemByValue(idMarca.Trim());
        //            cboCveMarca.SelectedValue = itemMarca.Value;


        //            //CONFIGURACION DEL COMBOBOX MODELO
        //            var idBaja = ((Label)item.FindControl("ID_Baja")).Text;

        //            var equipoBajaEficiencia = EBajaEficiencia.First(p => p.ID == int.Parse(idBaja));

        //            var cboCveModelo = item.FindControl("cboCve_Modelo") as RadComboBox;
        //            cboCveModelo.DataSource = new OpEquiposAbEficiencia().ModelosAltaEficiencia(
        //                    int.Parse(equipoBajaEficiencia.Cve_Tecnologia.ToString(CultureInfo.InvariantCulture)),
        //                    int.Parse(cboCveMarca.SelectedValue),
        //                    int.Parse(IdProveedor));
        //            cboCveModelo.DataTextField = "Elemento";
        //            cboCveModelo.DataValueField = "IdElemento";
        //            cboCveModelo.DataBind();

        //            var idModelo = DataBinder.Eval(e.Item.DataItem, "Cve_Modelo").ToString();
        //            var itemModelo = cboCveModelo.FindItemByValue(idModelo);

        //            cboCveModelo.SelectedValue = itemModelo.Value;


        //        }
        //        else
        //        {
        //            var cboCveMarca = ((RadComboBox)item.FindControl("cboCve_Marca"));
        //            cboCveMarca.DataSource = new OpEquiposAbEficiencia().MarcasAltaEficiencia(EBajaEficienciaSeleccionado.Cve_Tecnologia, int.Parse(IdProveedor));
        //            cboCveMarca.DataTextField = "Elemento";
        //            cboCveMarca.DataValueField = "IdElemento";
        //            cboCveMarca.DataBind();

        //            //var idMarca = DataBinder.Eval(e.Item.DataItem, "Cve_Marca").ToString();
        //            //var itemMarca = cboCveMarca.FindItemByValue(idMarca.Trim());
        //            cboCveMarca.SelectedValue = "-1";

        //            var cboCveModelo = item.FindControl("cboCve_Modelo") as RadComboBox;
        //            cboCveModelo.Items.Clear();
        //            cboCveModelo.Items.Insert(0, new RadComboBoxItem("Seleccione", "-1"));

        //            rgEquiposBaja.DataSource = EBajaEficiencia;
        //            rgEquiposBaja.DataBind();

        //        }

        //    }
        //}


        //protected void cboCve_Marca_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var cboMarca = sender as RadComboBox;

        //        if (cboMarca != null)
        //        {
        //            var editedItem = cboMarca.NamingContainer as GridEditableItem;

        //            if (editedItem != null)
        //            {
        //                var cboModelo = editedItem["Cve_Modelo"].FindControl("cboCve_Modelo") as RadComboBox;                                                    


        //                if (cboModelo != null)
        //                {
        //                    cboModelo.DataSource = new OpEquiposAbEficiencia().ModelosAltaEficiencia(
        //                                            int.Parse(EBajaEficienciaSeleccionado.Cve_Tecnologia.ToString(CultureInfo.InvariantCulture)),
        //                                            int.Parse(cboMarca.SelectedValue),
        //                                            int.Parse(IdProveedor));
        //                    cboModelo.DataTextField = "Elemento";
        //                    cboModelo.DataValueField = "IdElemento";
        //                    cboModelo.DataBind();

        //                    cboModelo.SelectedValue = "-1";

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //         rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300,150, "Equipo de Alta Eficiencia",null);
        //    }



        //}


        //protected void cboCve_Modelo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    try
        //    {

        //        var cboModelo = sender as RadComboBox;

        //        if (cboModelo != null)
        //        {
        //            var editedItem = cboModelo.NamingContainer as GridEditableItem;

        //            if (editedItem != null)
        //            {

        //                var lblPrecioDistribuidorEdicion = editedItem.FindControl("lblPrecio_DistribuidorEdicion") as Label;

        //                lblPrecioDistribuidorEdicion.Text = new OpEquiposAbEficiencia().PrecioDistribuidorProductoAltaEficiencia(
        //                        int.Parse(cboModelo.SelectedValue),
        //                        int.Parse(IdProveedor));
        //            }

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
        //    }
        //}


        //protected void txtCantidad_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var editedItem = (sender as RadNumericTextBox).NamingContainer as GridEditableItem;
        //        var txtCantidad = (RadNumericTextBox)editedItem.FindControl("txtCantidad");
        //        var txtPrecioUnitario = (RadNumericTextBox)editedItem.FindControl("txtPrecio_Unitario");
        //        var lblImporteTotalSinIvaEdicion = (Label)editedItem.FindControl("lblImporte_Total_Sin_IVAEdicion");

        //        lblImporteTotalSinIvaEdicion.Text = (Valor(txtCantidad.Text) * Valor(txtPrecioUnitario.Text)).ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
        //    }
            
        //}


        //protected void txtPrecio_Unitario_TextChanged(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        var editedItem = (sender as RadNumericTextBox).NamingContainer as GridEditableItem;
        //        var txtCantidad = (RadNumericTextBox)editedItem.FindControl("txtCantidad");
        //        var txtPrecioUnitario = (RadNumericTextBox)editedItem.FindControl("txtPrecio_Unitario");
        //        var lblImporteTotalSinIvaEdicion = (Label)editedItem.FindControl("lblImporte_Total_Sin_IVAEdicion");

        //        lblImporteTotalSinIvaEdicion.Text = (Valor(txtCantidad.Text) * Valor(txtPrecioUnitario.Text)).ToString(CultureInfo.InvariantCulture);
        //    }
        //    catch (Exception ex)
        //    {
        //        rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
        //    }
            
        //}


        //protected void rgEquiposAlta_InsertCommand(object sender, GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        var item = e.Item as GridEditableItem;

        //        if (item != null)
        //        {
        //            //if (EBajaEficienciaSeleccionado == null)
        //            //{
        //            //    rwmVentana.RadAlert("No hay equipo de baja efeciencia seleccionado.", 300, 150, "Equipo de Alta Eficiencia", null);                                                           
        //            //    return;
        //            //}
        //            //if (EBajaEficienciaSeleccionado.Cantidad == 0)
        //            //{
        //            //    rwmVentana.RadAlert("Necesita capturar la información requerida en el equipo de baja eficiencia.", 300, 150, "Equipo de Alta Eficiencia", null);                                                                                   
        //            //    return;
        //            //}



        //            var equipoBe = EBajaEficiencia.First(p => p.ID == EBajaEficienciaSeleccionado.ID);
        //            var equipoAe = new EquipoAltaEficiencia();

        //            //REALIZA UNA CONSULTA A CAT_TECNOLOGIA
        //            var validaTecnologia = new Tecnologia().ValidaEquipoParaAlta(equipoBe.Cve_Tecnologia);
        //            int totalEquiposAlta = 0;

        //            bool esnumero = int.TryParse(validaTecnologia.DxEquipoAlta,out totalEquiposAlta);
        //            bool realizaInsercion = false;

        //            //if (validaTecnologia.DxEquipoAlta.ToUpper() == "MUCHOS")
        //            //    realizaInsercion = true;
        //            //else if (esnumero)
        //            //{
        //            //    if (EAltaEficiencia.Count < totalEquiposAlta)
        //            //        realizaInsercion = true;
        //            //}
        //            //else
        //            //    realizaInsercion = false;
                    
                    
        //            if(realizaInsercion)
        //            {
        //                if (equipoBe.EquiposAltaEficiencia != null)
        //                    if (equipoBe.EquiposAltaEficiencia.Count > 0)
        //                        equipoAe.ID = equipoBe.EquiposAltaEficiencia.Max(p => p.ID) + 1;
        //                    else
        //                        equipoAe.ID = 1;
        //                else
        //                {
        //                    equipoAe.ID = 1;
        //                    equipoBe.EquiposAltaEficiencia = new List<EquipoAltaEficiencia>();
        //                }


        //                equipoAe.ID_Baja = equipoBe.ID;
        //                equipoAe.Cve_Marca = int.Parse(((RadComboBox)item.FindControl("cboCve_Marca")).SelectedValue);
        //                equipoAe.Dx_Marca = ((RadComboBox)item.FindControl("cboCve_Marca")).Text;
        //                equipoAe.Cve_Modelo = int.Parse(((RadComboBox)item.FindControl("cboCve_Modelo")).SelectedValue);
        //                equipoAe.Dx_Modelo = ((RadComboBox)item.FindControl("cboCve_Modelo")).Text;
        //                equipoAe.Cantidad = int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
        //                equipoAe.Precio_Distribuidor = Convert.ToDecimal(string.IsNullOrEmpty(((Label)item.FindControl("lblPrecio_DistribuidorEdicion")).Text) ? "0" :
        //                                               ((Label)item.FindControl("lblPrecio_DistribuidorEdicion")).Text);
        //                equipoAe.Precio_Unitario = Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtPrecio_Unitario")).Text);
        //                equipoAe.Importe_Total_Sin_IVA = Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtPrecio_Unitario")).Text);
        //                equipoAe.Gasto_Instalacion = Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtGasto_Instalacion")).Text);

        //                equipoBe.EquiposAltaEficiencia.Add(equipoAe);
        //                EAltaEficiencia = equipoBe.EquiposAltaEficiencia;

        //                rgEquiposAlta.DataSource = EAltaEficiencia;
        //                rgEquiposAlta.DataBind();
        //            }
        //            else
        //            {
        //                rwmVentana.RadAlert("No puede agregar mas equipos de acuerdo a lo establecido" , 300, 150, "Equipo de Alta Eficiencia", null);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
        //    }
        //}


        #endregion                                          
        

       
    }
}
