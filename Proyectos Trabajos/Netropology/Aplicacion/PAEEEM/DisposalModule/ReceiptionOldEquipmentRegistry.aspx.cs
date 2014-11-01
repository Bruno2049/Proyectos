using System;
using System.Globalization;
using System.Web.UI;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.BussinessLayer;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.DisposalModule
{
    public partial class ReceiptionOldEquipmentRegistry : Page
    {
        private static readonly object LockObject = new object();
        private const string SinConfirmdad = "0";

        #region Define Global variable
        public int TechnologyId
        {
            get
            {
                return ViewState["TechnologyId"] == null ? 0 : int.Parse(ViewState["TechnologyId"].ToString());
            }
            set
            {
                ViewState["TechnologyId"] = value;
            }
        }

        public string DisposalCenterNumber
        {
            get
            {
                return ViewState["DisposalID"] == null ? "0" : ViewState["DisposalID"].ToString();
            }
            set
            {
                ViewState["DisposalID"] = value;
            }
        }

        public string DisposalCenterType
        {
            get
            {
                return ViewState["DisposalCenterType"] == null ? "" : ViewState["DisposalCenterType"].ToString();
            }
            set
            {
                ViewState["DisposalCenterType"] = value;
            }
        }

        public string CreditNumber
        {
            get
            {
                return ViewState["NoCredit"] == null ? "" : ViewState["NoCredit"].ToString();
            }
            set
            {
                ViewState["NoCredit"] = value;
            }
        }
        #endregion

        /// <summary>
        /// Init Default Data When page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack) return;
                //return to login page when session is null
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                PrepareGlobalParameters();

                LoadEditionData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "LoadError",
                                        "alert('Excepción que se produce durante la primera página cargada:" + ex.Message + "');", true);
            }
        }

        private void LoadEditionData()
        {
            //Get old equipment id
            var creditSusId = System.Text.Encoding.Default.GetString(Convert.FromBase64String(
                                                                                        Request.QueryString["CreditSusID"].Replace("%2B", "+")));

            //get Credit No
            CreditNumber = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetNoCreditByCreditSusID(creditSusId);

            //Get Old Equipment Information
            var oldProductRelatedInfo = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetOldEquipmentByID(creditSusId, DisposalCenterType);

            if (oldProductRelatedInfo == null || oldProductRelatedInfo.Rows.Count <= 0) return;

            //set default value for each control
            txtPreOldEquipmentID.Text = oldProductRelatedInfo.Rows[0]["Id_Pre_Folio"].ToString();
            txtDistribuidor.Text = oldProductRelatedInfo.Rows[0]["ProviderName"].ToString();
            txtNameCeri.Text = oldProductRelatedInfo.Rows[0]["ProviderComercialName"].ToString();
            txtCliente.Text = oldProductRelatedInfo.Rows[0]["Dx_Razon_Social"].ToString();
            txtPrograma.Text = oldProductRelatedInfo.Rows[0]["Dx_Nombre_Programa"].ToString();
            txtTecnología.Text = oldProductRelatedInfo.Rows[0]["Dx_Nombre_General"].ToString();
            txtEstatus.Text = oldProductRelatedInfo.Rows[0]["Dx_Estatus_Credito"].ToString();
            txtCoordinación.Text = oldProductRelatedInfo.Rows[0]["Dx_Nombre_Region"].ToString();
            txtZona.Text = oldProductRelatedInfo.Rows[0]["Dx_Nombre_Zona"].ToString();

            // added by tina 2012/04/12
            /* RSA 20120927 ya no se usa
                if (txtTecnología.Text.ToLower().Contains("aire acondicionado"))
                {
                    LabelUnits.Text = "Tons";
                }
                else
                {
                    LabelUnits.Text = "Kg";
                }
                 */
            // end
               
            //edit by coco 2011-11-25
            //cache technology for later use
            if (oldProductRelatedInfo.Rows[0]["Cve_Tecnologia"].ToString() != "")
            {
                TechnologyId = Int32.Parse(oldProductRelatedInfo.Rows[0]["Cve_Tecnologia"].ToString());
            }
            //edit by coco 2011-12-31
            InitializeCapacidad();
            //txtMarca.Text = OldProductRelatedInfo.Rows[0]["Dx_Marca"].ToString();
            //this.drpCapacidad.SelectedValue = OldProductRelatedInfo.Rows[0]["Cve_Capacidad_Sust"].ToString();
            ////end edit
            //txtColor.Text = OldProductRelatedInfo.Rows[0]["Dx_Color"].ToString();
            //txtAntigüedad.Text = OldProductRelatedInfo.Rows[0]["Dx_Antiguedad"].ToString();
            //drpConformidad.SelectedValue = OldProductRelatedInfo.Rows[0]["Fg_Si_Funciona"].ToString();
            //txtPesodelEquipo.Text = OldProductRelatedInfo.Rows[0]["No_Peso"].ToString();
            //txtNúmerodeSerie.Text = OldProductRelatedInfo.Rows[0]["No_Serial"].ToString();

            //set default date
            txtFecha.Text = DateTime.Now.Date.ToShortDateString();

            //register event
            drpConformidad.Attributes.Add("onchange", "check()");
        }

        private void PrepareGlobalParameters()
        {
            //retrieve the disposal center number and user type used to get disposal center related zone and region information
            DisposalCenterNumber = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento.ToString(CultureInfo.InvariantCulture);
            var userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;

            DisposalCenterType = userType == GlobalVar.DISPOSAL_CENTER ? "M" : "B";
        }
        //add by coco 2011-11-25
        /// <summary>
        /// Initialize Capacidad Drop down list
        /// </summary>
        private void InitializeCapacidad()
        {
            // RSA 20120927 Lista de capacidad de distinta fuente
            var capacity = CAT_CAPACIDAD_SUSTITUCIONDal.ClassInstance.Get_CapacidaByTechnology(TechnologyId);
            if (capacity == null) return;
            drpCapacidad.DataSource = capacity;
            drpCapacidad.DataTextField = "No_Capacidad";
            drpCapacidad.DataValueField = "Cve_Capacidad_Sust";
            drpCapacidad.DataBind();

            /*
            DataTable dtCapacidad = CAT_CAPACIDAD_SUSTITUCIONDal.ClassInstance.Get_CAT_CAPACIDAD_SUSTITUCIONByTechnology(TechnologyID);
            if (dtCapacidad != null)
            {
                drpCapacidad.DataSource = dtCapacidad;
                drpCapacidad.DataTextField = "No_Capacidad";//edit by coco 2011-12-31
                drpCapacidad.DataValueField = "Cve_Capacidad_Sust";
                drpCapacidad.DataBind();
                //drpCapacidad.Items.Insert(0, "");
            }
             */
        }
        //end add
        /// <summary>
        /// Save data and reception old equipment and assign id_Folio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int result;
                var instance = new K_CREDITO_SUSTITUCIONModel();
                var instanceProduct = new K_PRODUCTO_CHARACTERSEntity();//add by coco 2012-1-6

                var creditSusId = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["CreditSusID"].Replace("%2B", "+")));
                //get credito_sustitucion information
                instance.Id_Credito_Sustitucion = int.Parse(creditSusId);
                instance.Fg_Si_Funciona = drpConformidad.SelectedValue;
                instance.Dt_Fecha_Recepcion = DateTime.Parse(txtFecha.Text);

                if (instance.Dt_Fecha_Recepcion > DateTime.Now)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "DateError",                                            
                        "alert('La fecha de recepción no puede ser mayor al día de hoy');", true);

                    return;
                }
                //get product_characters
                instanceProduct.Id_Credito_Sustitucion = int.Parse(creditSusId);
                instanceProduct.Dx_Marca = txtMarca.Text;               
                if (drpCapacidad.SelectedValue != "")
                {
                    instanceProduct.Cve_Capacidad_Sust = drpCapacidad.SelectedValue;
                }
                instanceProduct.Dx_Color = txtColor.Text;
                instanceProduct.Dx_Antiguedad = txtAntigüedad.Text;
                //delete by coco 2012-04-12
                //instanceProduct.No_Peso = txtPesodelEquipo.Text;
                //instanceProduct.No_Serial = txtNúmerodeSerie.Text;
                instanceProduct.Id_Pre_Folio = txtPreOldEquipmentID.Text;
                instanceProduct.Dx_Modelo_Producto = txtModel.Text;//add by coco 2012-01-12

                //Added by Jerry 2012-04-12
                var scheduledNotificationEntity = new ScheduledNotificationEntity
                {
                    ToEmail = ((US_USUARIOModel) Session["UserInfo"]).CorreoElectronico,
                    CCEmail =
                        ScheduledNotificationDal.ClassInstance.GetCarbonCopyEmailAddresses(DisposalCenterType,
                            DisposalCenterNumber),
                    SustitutionNumber = int.Parse(creditSusId),
                    CreditNumber = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetNoCreditByCreditSusID(creditSusId),
                    Subject = GlobalVar.NOTIFICATION_EMAIL_SUBJECT,
                    Body = GlobalVar.NOTIFICTION_EMAIL_BODY,
                    CreateDate = DateTime.Now
                };
                var url = "OldEquipmentReceptionList.aspx?BarCode=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(CreditNumber)).Replace("+", "%2B");
             

                lock (LockObject)
                {
                    //edit by coco 2011-12-31
                    var codeFilo = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetFiloIDByDisposalAndTechnology(DisposalCenterNumber, DisposalCenterType,TechnologyId, CreditNumber);
                    instance.Id_Folio = codeFilo.PadLeft(6, '0');
                    //end edit

                    //Changed by Jerry 2012-04-12
                    scheduledNotificationEntity.FolioID = instance.Id_Folio;

                    result = K_CREDITO_SUSTITUCIONBLL.ClassInstance.UpdateCreditSustutionByModel(instance, instanceProduct, scheduledNotificationEntity);
                }

                if (result <= 0) return;

                /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO PRE-AUTORIZADO EN SOLICITUD DE CREDITO*/
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                    "SOLICITUD DE CREDITO", "INGRESO A CAYD", scheduledNotificationEntity.CreditNumber,
                    "", "", "", "");

                Response.Redirect(url);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "UpdateDatabaseError",
                                                                "alert('Excepción que se produce durante la actualización de la propiedad del producto antiguo:" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// Cancel Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("OldEquipmentReceptionList.aspx?BarCode=" +
                                                Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(CreditNumber)).Replace("+", "%2B"));
        }
        /// <summary>
        /// Update All Old Equipment Fg_Si_Funciton and ReceiptionDate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hidConfirm_Click(object sender, EventArgs e)
        {
            var statusChangedDate = !txtFecha.Text.Equals("") ? DateTime.Parse(txtFecha.Text) : DateTime.Now.Date;

            var result = K_CREDITO_SUSTITUCIONDAL.ClassInstance.UpdateCreditSustitutionByNoCredit(CreditNumber, SinConfirmdad, statusChangedDate);

            if (result > 0)
            {
                Response.Redirect("OldEquipmentReceptionList.aspx?BarCode=" +
                                                Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(CreditNumber)).Replace("+", "%2B"));
            }
        }
    }
}
