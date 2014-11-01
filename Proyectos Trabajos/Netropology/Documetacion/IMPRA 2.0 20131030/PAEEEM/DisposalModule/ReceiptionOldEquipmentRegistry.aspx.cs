using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.BussinessLayer;

namespace PAEEEM.DisposalModule
{
    public partial class ReceiptionOldEquipmentRegistry : System.Web.UI.Page
    {
        private static object lockObject = new object();
        private const string SIN_CONFIRMDAD = "0";

        #region Define Global variable
        public int TechnologyID
        {
            get
            {
                return ViewState["TechnologyID"] == null ? 0 : int.Parse(ViewState["TechnologyID"].ToString());
            }
            set
            {
                ViewState["TechnologyID"] = value;
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
                if (!IsPostBack)
                {
                    //return to login page when session is null
                    if (null == Session["UserInfo"])
                    {
                        Response.Redirect("../Login/Login.aspx");
                        return;
                    }

                    PrepareGlobalParameters();

                    LoadEditionData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(Page), "LoadError",
                                        "alert('Excepción que se produce durante la primera página cargada:" + ex.Message + "');", true);
            }
        }

        private void LoadEditionData()
        {
            //Get old equipment id
            string CreditSusID = System.Text.Encoding.Default.GetString(Convert.FromBase64String(
                                                                                        Request.QueryString["CreditSusID"].ToString().Replace("%2B", "+")));

            //get Credit No
            CreditNumber = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetNoCreditByCreditSusID(CreditSusID);

            //Get Old Equipment Information
            DataTable OldProductRelatedInfo = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetOldEquipmentByID(CreditSusID, DisposalCenterType);

            if (OldProductRelatedInfo != null && OldProductRelatedInfo.Rows.Count > 0)
            {
                //set default value for each control
                txtPreOldEquipmentID.Text = OldProductRelatedInfo.Rows[0]["Id_Pre_Folio"].ToString();
                txtDistribuidor.Text = OldProductRelatedInfo.Rows[0]["ProviderName"].ToString();
                txtNameCeri.Text = OldProductRelatedInfo.Rows[0]["ProviderComercialName"].ToString();
                txtCliente.Text = OldProductRelatedInfo.Rows[0]["Dx_Razon_Social"].ToString();
                txtPrograma.Text = OldProductRelatedInfo.Rows[0]["Dx_Nombre_Programa"].ToString();
                txtTecnología.Text = OldProductRelatedInfo.Rows[0]["Dx_Nombre_General"].ToString();
                txtEstatus.Text = OldProductRelatedInfo.Rows[0]["Dx_Estatus_Credito"].ToString();
                txtCoordinación.Text = OldProductRelatedInfo.Rows[0]["Dx_Nombre_Region"].ToString();
                txtZona.Text = OldProductRelatedInfo.Rows[0]["Dx_Nombre_Zona"].ToString();

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
                if (OldProductRelatedInfo.Rows[0]["Cve_Tecnologia"].ToString() != "")
                {
                    TechnologyID = Int32.Parse(OldProductRelatedInfo.Rows[0]["Cve_Tecnologia"].ToString());
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
                this.txtFecha.Text = DateTime.Now.Date.ToShortDateString();

                //register event
                this.drpConformidad.Attributes.Add("onchange", "check()");
            }
        }

        private void PrepareGlobalParameters()
        {
            //retrieve the disposal center number and user type used to get disposal center related zone and region information
            DisposalCenterNumber = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento.ToString();
            string UserType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;

            if (UserType == GlobalVar.DISPOSAL_CENTER)
            {
                DisposalCenterType = "M";
            }
            else
            {
                DisposalCenterType = "B";
            }
        }
        //add by coco 2011-11-25
        /// <summary>
        /// Initialize Capacidad Drop down list
        /// </summary>
        /// <param name="Technology"></param>
        private void InitializeCapacidad()
        {
            // RSA 20120927 Lista de capacidad de distinta fuente
            DataTable Capacity = CAT_CAPACIDAD_SUSTITUCIONDal.ClassInstance.Get_CapacidaByTechnology(TechnologyID);
            if (Capacity != null)
            {
                drpCapacidad.DataSource = Capacity;
                drpCapacidad.DataTextField = "No_Capacidad";
                drpCapacidad.DataValueField = "Cve_Capacidad_Sust";
                drpCapacidad.DataBind();
            }

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
                int result = 0;
                K_CREDITO_SUSTITUCIONModel instance = new K_CREDITO_SUSTITUCIONModel();
                K_PRODUCTO_CHARACTERSEntity instanceProduct = new K_PRODUCTO_CHARACTERSEntity();//add by coco 2012-1-6

                string CreditSusID = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["CreditSusID"].ToString().Replace("%2B", "+")));
                //get credito_sustitucion information
                instance.Id_Credito_Sustitucion = int.Parse(CreditSusID);
                instance.Fg_Si_Funciona = drpConformidad.SelectedValue;
                instance.Dt_Fecha_Recepcion = DateTime.Parse(this.txtFecha.Text);

                if (instance.Dt_Fecha_Recepcion > DateTime.Now)
                {
                    ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(Page), "DateError",                                            
                        "alert('La fecha de recepción no puede ser mayor al día de hoy');", true);

                    return;
                }
                //get product_characters
                instanceProduct.Id_Credito_Sustitucion = int.Parse(CreditSusID);
                instanceProduct.Dx_Marca = txtMarca.Text;               
                if (drpCapacidad.SelectedValue != "")
                {
                    instanceProduct.Cve_Capacidad_Sust = this.drpCapacidad.SelectedValue;
                }
                instanceProduct.Dx_Color = txtColor.Text;
                instanceProduct.Dx_Antiguedad = txtAntigüedad.Text;
                //delete by coco 2012-04-12
                //instanceProduct.No_Peso = txtPesodelEquipo.Text;
                //instanceProduct.No_Serial = txtNúmerodeSerie.Text;
                instanceProduct.Id_Pre_Folio = txtPreOldEquipmentID.Text;
                instanceProduct.Dx_Modelo_Producto = txtModel.Text;//add by coco 2012-01-12

                //Added by Jerry 2012-04-12
                ScheduledNotificationEntity scheduledNotificationEntity = new ScheduledNotificationEntity();
                scheduledNotificationEntity.ToEmail = ((US_USUARIOModel)Session["UserInfo"]).CorreoElectronico;
                scheduledNotificationEntity.CCEmail = ScheduledNotificationDal.ClassInstance.GetCarbonCopyEmailAddresses(DisposalCenterType, DisposalCenterNumber);
                scheduledNotificationEntity.SustitutionNumber = int.Parse(CreditSusID);
                scheduledNotificationEntity.CreditNumber = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetNoCreditByCreditSusID(CreditSusID);
                scheduledNotificationEntity.Subject = GlobalVar.NOTIFICATION_EMAIL_SUBJECT;
                scheduledNotificationEntity.Body = GlobalVar.NOTIFICTION_EMAIL_BODY;
                scheduledNotificationEntity.CreateDate = DateTime.Now;

                lock (lockObject)
                {
                    //edit by coco 2011-12-31
                    string codeFilo = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetFiloIDByDisposalAndTechnology(DisposalCenterNumber, DisposalCenterType,TechnologyID, CreditNumber);
                    instance.Id_Folio = codeFilo.PadLeft(6, '0');
                    //end edit

                    //Changed by Jerry 2012-04-12
                    scheduledNotificationEntity.FolioID = instance.Id_Folio;

                    result = K_CREDITO_SUSTITUCIONBLL.ClassInstance.UpdateCreditSustutionByModel(instance, instanceProduct, scheduledNotificationEntity);
                }

                if (result > 0)
                {
                    Response.Redirect("OldEquipmentReceptionList.aspx?BarCode=" +
                                                    Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(CreditNumber)).Replace("+", "%2B"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(Page), "UpdateDatabaseError",
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
            DateTime statusChangedDate = new DateTime();
            if (!txtFecha.Text.Equals(""))
            {
                statusChangedDate = DateTime.Parse(this.txtFecha.Text);
            }
            else
            {
                statusChangedDate = DateTime.Now.Date;
            }

            int Result = K_CREDITO_SUSTITUCIONDAL.ClassInstance.UpdateCreditSustitutionByNoCredit(CreditNumber, SIN_CONFIRMDAD, statusChangedDate);

            if (Result > 0)
            {
                Response.Redirect("OldEquipmentReceptionList.aspx?BarCode=" +
                                                Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(CreditNumber)).Replace("+", "%2B"));
            }
        }
    }
}
