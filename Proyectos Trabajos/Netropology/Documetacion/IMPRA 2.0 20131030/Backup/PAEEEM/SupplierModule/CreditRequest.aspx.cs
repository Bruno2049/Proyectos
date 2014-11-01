using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM
{
    public partial class CreditRequest : System.Web.UI.Page
    {
        #region Define Global variable
        const double gastosPercentage = .3 / .7;
        public DataTable GridViewDataSource
        {
            get
            {
                return ViewState["GridViewDataSource"] == null ? null : ViewState["GridViewDataSource"] as DataTable;
            }
            set
            {
                ViewState["GridViewDataSource"] = value;
            }
        }
        public string UserID
        {
            get
            {
                return ViewState["UserID"] == null ? "" : ViewState["UserID"].ToString();
            }
            set
            {
                ViewState["UserID"] = value;
            }
        }
        public string idDepartment
        {
            get
            {
                return ViewState["idDepartment"] == null ? "" : ViewState["idDepartment"].ToString();
            }
            set
            {
                ViewState["idDepartment"] = value;
            }
        }
        public string CreditNo
        {
            get
            {
                return ViewState["CreditNo"] == null ? "0" : ViewState["CreditNo"].ToString();
            }
            set
            {
                ViewState["CreditNo"] = value;
            }
        }
        public DataTable ProgramDt   //DataTable ProgramDt = CAT_PROGRAMABLL.ClassInstance.Get_All_CAT_PROGRAMAByPK("1");
        {
            get
            {
                return ViewState["ProgramDt"] == null ? null : ViewState["ProgramDt"] as DataTable;
            }
            set
            {
                ViewState["ProgramDt"] = value;
            }
        }
        public string TipoUser
        {
            get
            {
                return ViewState["TipoUser"] == null ? "" : ViewState["TipoUser"].ToString();
            }
            set
            {
                ViewState["TipoUser"] = value;
            }
        }
        public Dictionary<string, string> Dics
        {
            get
            {
                return ViewState["Dics"] ==null ? new Dictionary<string, string> () :(Dictionary<string,string>) ViewState["Dics"];
            }
            set
            {
                ViewState["Dics"] = value;
            }
        }
        public string UserName
        {
            get
            {
                return ViewState["UserName"] == null ? "" : ViewState["UserName"].ToString();
            }
            set
            {
                ViewState["UserName"] = value;
            }
        }
        public string UserEmail
        {
            get
            {
                return ViewState["UserEmail"] == null ? "" : ViewState["UserEmail"].ToString();
            }
            set
            {
                ViewState["UserEmail"] = value;
            }
        }
        //add by coco 2011-08-05
        public K_CREDITO_COSTOEntity creditCostEntity
        {
            get
            {
                return ViewState["creditCostEntity"] == null ? new K_CREDITO_COSTOEntity() : ViewState["creditCostEntity"] as K_CREDITO_COSTOEntity;
            }
            set
            {
                ViewState["creditCostEntity"] = value;
            }
        }
        public K_CREDITO_DESCUENTOEntity creditDesEntity
        {
            get
            {
                return ViewState["creditDesEntity"] == null ? new K_CREDITO_DESCUENTOEntity() : ViewState["creditDesEntity"] as K_CREDITO_DESCUENTOEntity;
            }
            set
            {
                ViewState["creditDesEntity"] = value;
            }
        }
        public decimal Ddescount
        {
            get
            {
                return ViewState["Ddescount"] == null ? 0 : decimal.Parse(ViewState["Ddescount"].ToString());
            }
            set
            {
                ViewState["Ddescount"] = value;
            }
        }
        public decimal DCost
        {
            get
            {
                return ViewState["DCost"] == null ? 0 : decimal.Parse(ViewState["DCost"].ToString());
            }
            set
            {
                ViewState["DCost"] = value;
            }
        }
        public Boolean DescountFlag
        {
            get
            {
                return ViewState["DescountFlag"] == null ? default(Boolean) : Boolean.Parse(ViewState["DescountFlag"].ToString());
            }
            set
            {
                ViewState["DescountFlag"] = value;
            }
        }
        public Boolean CostFlag
        {
            get
            {
                return ViewState["CostFlag"] == null ? default(Boolean) : Boolean.Parse(ViewState["CostFlag"].ToString());
            }
            set
            {
                ViewState["CostFlag"] = value;
            }
        }
        //end add
        //Added by Jerry 2011/08/12
        private string RGN_CFE
        {
            get 
            {
                return ViewState["RGN_CFE"] != null?ViewState["RGN_CFE"].ToString():"";
            }
            set
            {
                ViewState["RGN_CFE"] = value;
            }
        }

        private string ZoneType
        {
            get
            {
                return ViewState["ZoneType"] != null ? ViewState["ZoneType"].ToString() : "";
            }
            set
            {
                ViewState["ZoneType"] = value;
            }
        }

        //add by coco 20110824
        private string technologyID
        {
            get
            {
                return ViewState["technologyID"] != null ? ViewState["technologyID"].ToString() : "";
            }
            set
            {
                ViewState["technologyID"] = value;
            }
        }
        //end add

        // Add by Tina 2011/08/04
        public int Tipo_Sociedad
        {
            get
            {
                return ViewState["Tipo_Sociedad"] == null ? 1 : int.Parse(ViewState["Tipo_Sociedad"].ToString());
            }
            set
            {
                ViewState["Tipo_Sociedad"] = value;
            }
        }

        //added by tina 2012-07-26
        public float Pct_Tasa_IVA
        {
            get
            {
                return ViewState["Pct_Tasa_IVA"] == null ? 0 : float.Parse(ViewState["Pct_Tasa_IVA"].ToString());
            }
            set
            {
                ViewState["Pct_Tasa_IVA"] = value;
            }
        }
        //end
        public Dictionary<string, string> DxCveCC
        {
            get
            {
                if (ViewState["DxCveCC"] == null)
                    ViewState["DxCveCC"] = new Dictionary<string, string>();
                return (Dictionary<string, string>)ViewState["DxCveCC"];
            }
            set
            {
                ViewState["DxCveCC"] = value;
            }
        }
        const string DxCveCC_SE = "SE";

        //RSA 20130813 Enable edition, store the credit number, if any
        private string CreditNumber
        {
            get
            {
                return ViewState["CreditNumber"] != null ? ViewState["CreditNumber"].ToString() : "";
            }
            set
            {
                ViewState["CreditNumber"] = value;
            }
        }
        //end
        // RSA 20130826 Gastos
        public Dictionary<string, int> CveGasto
        {
            get
            {
                if (ViewState["CveGasto"] == null)
                    ViewState["CveGasto"] = new Dictionary<string, int>();
                return (Dictionary<string, int>)ViewState["CveGasto"];
            }
            set
            {
                ViewState["CveGasto"] = value;
            }
        }
        #endregion

        #region Load Related Events
        /// <summary>
        /// init data when page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                if (!IsPostBack)
                {
                    DataTable dtAllCreditProperties = null;

                    // RSA 20130813 Enable editing, read credit number if any
                    if (Request.QueryString["Token"] != null && Request.QueryString["Token"].ToString() != "")
                    {
                        // a token exits, try and retrieve the existing credit information
                        CreditNumber = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Token"].ToString()));

                        dtAllCreditProperties = GetAllCreditProperties(CreditNumber);
                        if (IsEditionAllowed(dtAllCreditProperties))
                        {
                            ShowCreditNumber(lblCredito1, txtCredito1);
                            ShowCreditNumber(lblCredito2, txtCredito2);
                            ShowCreditNumber(lblCredito3, txtCredito3);
                            ShowCreditNumber(lblCredito4, txtCredito4);

                            Session["ValidRPU"] = dtAllCreditProperties.Rows[0]["No_RPU"].ToString();
                            imgAlta1.Visible = false;
                            imgAlta2.Visible = false;
                            imgEdit1.Visible = true;
                            imgEdit2.Visible = true;
                        }
                        else
                        {
                            Response.Redirect("CreditMonitor.aspx");
                        }
                    }

                    US_USUARIOModel User = (US_USUARIOModel)Session["UserInfo"];
                    UserID = User.Id_Usuario.ToString();
                    //edit by coco 20110-01
                    InitDepartmentProperty();         
                    //end edit
                    TipoUser = User.Tipo_Usuario.ToString();
                    UserName = User.Nombre_Usuario.ToString();
                    UserEmail = User.CorreoElectronico.ToString();
                    //add by coco 2011-08-11  Get all CAT_DELEG_MUNICIPIO
                    DataTable dtDelegMunicipio = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_All_CAT_DELEG_MUNICIPIO();
                    HttpContext.Current.Cache.Insert("DeletMunicipio", dtDelegMunicipio);
                    //end add
                    InitAttributes();
                    InitStepOneData();  // RSA 20130814 Sets Coacreditado defaults, dropdown initialization required before InitDefaultData assignment
                    //Init default data
                    InitDefaultData();
                    InitSavedData(dtAllCreditProperties);
                    InitStepTwoData();
                    //InitialPage3Dropdownlist
                    InitStepThreeDropdownList();
                    ProgramDt = CAT_PROGRAMABLL.ClassInstance.Get_CAT_PROGRAMAbyPk(Global.PROGRAM.ToString());//Changed by Jerry 2011/08/08

                    // RSA 20110824
                    if (Session["ValidRPU"] != null)
                    {
                        txtNO_RPU.Text = Session["ValidRPU"].ToString();
                        Session["ValidRPU"] = null;
                    }
                    else
                    {
                        Response.Redirect("../Login/Login.aspx");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "PageLoad", "alert('" + ex.Message + "');", true);
            }
        }

        private void ShowCreditNumber(Label lblCredito, TextBox txtCredito)
        {
            lblCredito.Visible = true;
            txtCredito.Visible = true;
            txtCredito.Text = CreditNumber;
        }
        /// <summary>
        /// Add attributes to the controls to fix inputs, such as capitalize, replace non ascii like "ñ" with
        /// it's ascii equivalent "n"
        /// </summary>
        private void InitAttributes()
        {
            txtName.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtLastname.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtMotherName.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtEmail.Attributes.Add("onkeyup", "CapitalASCII(this)");

            txtDX_CURP.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_Nombre_Comercial.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDX_RFC.Attributes.Add("onkeyup", "CapitalASCII(this)");

            txtDX_NOMBRE_REPRE_LEGAL.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDX_NO_IDENTIFICACION_REPRE_LEGAL.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDX_EMAIL_REPRE_LEGAL.Attributes.Add("onkeyup", "CapitalASCII(this)");

            txtDx_Domicilio_fisc_calle.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_Domicilio_fisc_num.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_Domicilio_Fisc_Colonia.Attributes.Add("onkeyup", "CapitalASCII(this)");

            txtDx_domicilio_neg_calle.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_domicilio_neg_num.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_Domicilio_Neg_Colonia.Attributes.Add("onkeyup", "CapitalASCII(this)");

            txtDx_First_Name_aval.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_Last_Name_aval.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_Mother_Name_aval.Attributes.Add("onkeyup", "CapitalASCII(this)");
            // txtDx_RFC_CURP_Aval.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_RFC_Aval.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_CURP_Aval.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_domicilio_aval_calle.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_domicilio_aval_num.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_Domicilio_Aval_Colonia.Attributes.Add("onkeyup", "CapitalASCII(this)");

            txtDx_nombre_coacreditado.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_RFC_CURP_Coacreditado.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_Domicilio_Coacreditado_Colonia.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_domicilio_coacreditado_calle.Attributes.Add("onkeyup", "CapitalASCII(this)");
            txtDx_domicilio_coacreditado_num.Attributes.Add("onkeyup", "CapitalASCII(this)");
        }
        //private System.Web.Caching.CacheItemUpdateCallback RefreshCache(string str, System.Web.Caching.CacheItemUpdateReason reason, out object o, out System.Web.Caching.CacheDependency depedency, out DateTime date, out TimeSpan timespan)
        //{
        //    DataTable dtDelegMunicipio = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_All_CAT_DELEG_MUNICIPIO();
        //    Cache.Insert("DeletMunicipio", dtDelegMunicipio, null, DateTime.Now.AddMinutes(2), TimeSpan.Zero, RefreshCache);
        //}
        /// <summary>
        /// Init dropdownlist data
        /// </summary>
        private void InitDefaultData()
        {
            BindddlDX_TIPO_INDUSTRIA();
            BindddlDX_TIPO_SOCIEDAD();
            BindddlDX_TIPO_ACREDITACION();
            BindddlDX_REG_CONYUGAL_REPRE_LEGAL();
            BindddlDX_IDENTIFICACION_REPRE_LEGAL();
            BindddlDx_nombre_estado();
            BindddlDx_deleg_municipio();
            BindddlDx_deleg_municipio_aval();
            BindddlDx_deleg_municipio_Neg();
            BindddlDx_Tipo_propiedad();
            radDx_sexo1.Checked = true;
            radFG_EDO_CIVIL_REPRE_LEGAL1.Checked = true;
            radFG_SEXO_REPRE_LEGAL1.Checked = true;
            lbbNowdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            // END
        }
        /// <summary>
        /// Load saved data from dtCredit, if null use the temp table
        /// </summary>
        private void InitSavedData(DataTable dtAllCreditProperties)
        {
            // RSA 20130813 Enable editing, read data from credit if any, otherwise try to use Temp table
            if (dtAllCreditProperties != null)
            {
                LoadCreditRecord(dtAllCreditProperties);
            }
            else
            {
                CheckOrLoadTempTableRecord();//add by coco 2012-07-18
            }

            // Hide selectors that depend on Tipo Sociedad, do it after any other control setting such as ViewddlDX_REG_CONYUGAL_REPRE_LEGAL
            ddlDX_TIPO_SOCIEDAD_SelectedIndexChanged(null, null);

            // make sure the fields use the right char (upper case, ascii, only)
            // CapitalASCIIFields();
        }
        //add by coco 2012-07-18
        private void CheckOrLoadTempTableRecord()
        {
            DataTable dtCreditTemp = K_CREDITO_TEMPDAL.ClassInstance.Get_K_Credito_Temp(UserID);
            if ( dtCreditTemp != null && dtCreditTemp.Rows.Count > 0 )
            {
                //added by tina 2012-07-26
                if (Session["ValidRPU"] == null || Session["ValidRPU"].ToString() != dtCreditTemp.Rows[0]["No_RPU"].ToString())
                {
                    return;
                }
                //end
                //Load temporary saved data
                //First Section
                ddlDX_TIPO_SOCIEDAD.SelectedIndex = ddlDX_TIPO_SOCIEDAD.Items.IndexOf(ddlDX_TIPO_SOCIEDAD.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Tipo_Sociedad"].ToString()));
                txtName.Text = dtCreditTemp.Rows[0]["Dx_First_Name"].ToString();
                txtLastname.Text = dtCreditTemp.Rows[0]["Dx_Last_Name"].ToString();
                ckbLastname.Checked = string.IsNullOrEmpty(txtLastname.Text);
                txtLastname.Enabled = !ckbLastname.Checked;
                reqTxtLastName.Enabled = !ckbLastname.Checked;
                txtMotherName.Text = dtCreditTemp.Rows[0]["Dx_Mother_Name"].ToString();
                ckbMothername.Checked = string.IsNullOrEmpty(txtMotherName.Text);
                txtMotherName.Enabled = !ckbMothername.Checked;
                reqTxtMotherName.Enabled = !ckbMothername.Checked;
                txtBirthDate.Text = dtCreditTemp.Rows[0]["Dt_Fecha_Nacimiento"].ToString();
                ddlDX_TIPO_INDUSTRIA.SelectedIndex = ddlDX_TIPO_INDUSTRIA.Items.IndexOf(ddlDX_TIPO_INDUSTRIA.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Tipo_Industria"].ToString()));
                txtTelephone.Text = dtCreditTemp.Rows[0]["Dx_Telephone"].ToString();
                txtEmail.Text = dtCreditTemp.Rows[0]["Dx_Email"].ToString();
                txtDX_CURP.Text = dtCreditTemp.Rows[0]["Dx_CURP"].ToString();
                txtDx_Nombre_Comercial.Text = dtCreditTemp.Rows[0]["Dx_Nombre_Comercial"].ToString();
                txtDX_RFC.Text = dtCreditTemp.Rows[0]["Dx_RFC"].ToString();
                txtDX_NOMBRE_REPRE_LEGAL.Text = dtCreditTemp.Rows[0]["Dx_Nombre_Repre_Legal"].ToString();
                ddlDX_ACREDITACION_REPRE_LEGAL.SelectedValue = dtCreditTemp.Rows[0]["Cve_Acreditacion_Repre_legal"].ToString();
                // RSA 20130816 Enable editing, weird things happened to an event handler when checking one option
                // without unchecking the other (cause it was checked by the InitDefaultData step?)
                if (dtCreditTemp.Rows[0]["Fg_Sexo_Repre_legal"].ToString().Equals("1"))
                {
                    radFG_SEXO_REPRE_LEGAL1.Checked = true;
                    radFG_SEXO_REPRE_LEGAL2.Checked = false;
                }
                else if ( dtCreditTemp.Rows[0]["Fg_Sexo_Repre_legal"].ToString().Equals("2") )
                {
                    radFG_SEXO_REPRE_LEGAL1.Checked = false;
                    radFG_SEXO_REPRE_LEGAL2.Checked = true;
                }
                txtNO_RPU.Text = dtCreditTemp.Rows[0]["No_RPU"].ToString();
                if ( dtCreditTemp.Rows[0]["Fg_Edo_Civil_Repre_legal"].ToString().Equals("1") )
                {
                    radFG_EDO_CIVIL_REPRE_LEGAL1.Checked = true;
                    radFG_EDO_CIVIL_REPRE_LEGAL2.Checked = false;
                }
                else if ( dtCreditTemp.Rows[0]["Fg_Edo_Civil_Repre_legal"].ToString().Equals("2") )
                {
                    radFG_EDO_CIVIL_REPRE_LEGAL1.Checked = false;
                    radFG_EDO_CIVIL_REPRE_LEGAL2.Checked = true;
                }
                // RSA 20130816 Group logic of Reg Conyugal visibility
                ViewddlDX_REG_CONYUGAL_REPRE_LEGAL();

                ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedIndex = ddlDX_REG_CONYUGAL_REPRE_LEGAL.Items.IndexOf(ddlDX_REG_CONYUGAL_REPRE_LEGAL.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Reg_Conyugal_Repre_legal"].ToString()));
                ddlDX_IDENTIFICACION_REPRE_LEGAL.SelectedIndex = ddlDX_IDENTIFICACION_REPRE_LEGAL.Items.IndexOf(ddlDX_IDENTIFICACION_REPRE_LEGAL.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Identificacion_Repre_legal"].ToString()));
                this.txtDX_NO_IDENTIFICACION_REPRE_LEGAL.Text = dtCreditTemp.Rows[0]["Dx_No_Identificacion_Repre_Legal"].ToString();
                this.txtMT_VENTAS_MES_EMPRESA.Text = dtCreditTemp.Rows[0]["Mt_Ventas_Mes_Empresa"].ToString();
                this.txtMT_GASTOS_MES_EMPRESA.Text = dtCreditTemp.Rows[0]["Mt_Gastos_Mes_Empresa"].ToString();
                txtDX_EMAIL_REPRE_LEGAL.Text = dtCreditTemp.Rows[0]["Dx_Email_Repre_legal"].ToString();
                txtNO_CONSUMO_PROMEDIO.Text = dtCreditTemp.Rows[0]["No_consumo_promedio"].ToString();
                //Second Section
                txtDx_Domicilio_fisc_calle.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Fisc_Calle"].ToString();
                txtDx_Domicilio_fisc_num.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Fisc_Num"].ToString();
                txtDx_Domicilio_fisc_cp.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Fisc_CP"].ToString();
                ddlDx_nombre_estado.SelectedIndex = ddlDx_nombre_estado.Items.IndexOf(ddlDx_nombre_estado.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Estado_Fisc"].ToString()));
                ddlDx_deleg_municipio.SelectedIndex = ddlDx_deleg_municipio.Items.IndexOf(ddlDx_deleg_municipio.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Deleg_Municipio_Fisc"].ToString()));
                //txtCiudad.Text = dtCreditTemp.Rows[0]["Dx_Ciudad"].ToString();
                //txtInterior.Text = dtCreditTemp.Rows[0]["Dx_Numero_Interior"].ToString();
                ddlDx_Tipo_propiedad.SelectedIndex = ddlDx_Tipo_propiedad.Items.IndexOf(ddlDx_Tipo_propiedad.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Tipo_Propiedad_Fisc"].ToString()));
                txtDx_tel_neg.Text = dtCreditTemp.Rows[0]["Dx_Tel_Fisc"].ToString();
                txtDx_Domicilio_Fisc_Colonia.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Fisc_Colonia"].ToString();
                //Third Section
                if ( dtCreditTemp.Rows[0]["Fg_Mismo_Domicilio"].ToString().Equals("True") || dtCreditTemp.Rows[0]["Fg_Mismo_Domicilio"].Equals("1") )
                {
                    ckbFg_mismo_domicilio.Checked = true;
                }
                else
                {
                    ckbFg_mismo_domicilio.Checked = false;
                }
                txtDx_domicilio_neg_calle.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Neg_Calle"].ToString();
                txtDx_domicilio_neg_num.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Neg_Num"].ToString();
                txtDx_domicilio_neg_cp.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Neg_CP"].ToString();
                ddlDx_nombre_estado_Neg.SelectedIndex = ddlDx_nombre_estado_Neg.Items.IndexOf(ddlDx_nombre_estado_Neg.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Estado_Neg"].ToString()));
                ddlDx_deleg_municipio_Neg.SelectedIndex = ddlDx_deleg_municipio_Neg.Items.IndexOf(ddlDx_deleg_municipio_Neg.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Deleg_Municipio_Neg"].ToString()));
                ddlDx_Tipo_propiedad_Neg.SelectedIndex = ddlDx_Tipo_propiedad_Neg.Items.IndexOf(ddlDx_Tipo_propiedad_Neg.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Tipo_Propiedad_Neg"].ToString()));
                txtDx_tel_neg_Neg.Text = dtCreditTemp.Rows[0]["Dx_Tel_Neg"].ToString();
                txtDx_Domicilio_Neg_Colonia.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Neg_Colonia"].ToString();
                //Fourth Section
                // RSA 20131007 read first, last and mother names if available, otherwise use the old field
                txtDx_First_Name_aval.Text = string.IsNullOrEmpty(dtCreditTemp.Rows[0]["Dx_First_Name_Aval"].ToString())
                    ? dtCreditTemp.Rows[0]["Dx_nombre_Aval"].ToString() : dtCreditTemp.Rows[0]["Dx_First_Name_Aval"].ToString();
                txtDx_Last_Name_aval.Text = dtCreditTemp.Rows[0]["Dx_Last_Name_Aval"].ToString();
                txtDx_Mother_Name_aval.Text = dtCreditTemp.Rows[0]["Dx_Mother_Name_Aval"].ToString();
                txtDt_BirthDate_Aval.Text = dtCreditTemp.Rows[0]["Dt_BirthDate_Aval"].ToString();
                // RSA 20131007 Add fields for both RFC and CURP
                txtDx_RFC_Aval.Text = dtCreditTemp.Rows[0]["Dx_RFC_Aval"].ToString();
                txtDx_CURP_Aval.Text = dtCreditTemp.Rows[0]["Dx_CURP_Aval"].ToString();
                // But if they are empty read the old RFC_CURP value and assign it depending on it's size
                if (string.IsNullOrEmpty(txtDx_CURP_Aval.Text + txtDx_RFC_Aval.Text))
                {
                    if (dtCreditTemp.Rows[0]["Dx_RFC_CURP_Aval"].ToString().Length > 13)
                        txtDx_CURP_Aval.Text = dtCreditTemp.Rows[0]["Dx_RFC_CURP_Aval"].ToString();
                    else
                        txtDx_RFC_Aval.Text = dtCreditTemp.Rows[0]["Dx_RFC_CURP_Aval"].ToString();
                }
                txtDx_tel_aval.Text = dtCreditTemp.Rows[0]["Dx_Tel_Aval"].ToString();
                if (dtCreditTemp.Rows[0]["Fg_Sexo_Aval"].ToString().Equals("1"))
                {
                    radDx_sexo1.Checked = true;
                    radDx_sexo2.Checked = false;
                }
                else if (dtCreditTemp.Rows[0]["Fg_Sexo_Aval"].ToString().Equals("2"))
                {
                    radDx_sexo1.Checked = false;
                    radDx_sexo2.Checked = true;
                }
                txtDx_domicilio_aval_calle.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Aval_Calle"].ToString();
                txtDx_domicilio_aval_num.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Aval_Num"].ToString();
                txtDx_domicilio_aval_cp.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Aval_CP"].ToString();
                ddlDx_nombre_estado_aval.SelectedIndex = ddlDx_nombre_estado_aval.Items.IndexOf(ddlDx_nombre_estado_aval.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Estado_Aval"].ToString()));
                ddlDx_deleg_municipio_aval.SelectedIndex = ddlDx_deleg_municipio_aval.Items.IndexOf(ddlDx_deleg_municipio_aval.Items.FindByValue(dtCreditTemp.Rows[0]["Cve_Deleg_Municipio_Aval"].ToString()));
                txtDx_Domicilio_Aval_Colonia.Text = dtCreditTemp.Rows[0]["Dx_Domicilio_Aval_Colonia"].ToString();
            //    txtNo_RPU_aval.Text = dtCreditTemp.Rows[0]["No_RPU_AVAL"].ToString();
            //    txtMt_ventas_mes_aval.Text = dtCreditTemp.Rows[0]["Mt_Ventas_Mes_Aval"].ToString();
            //    txtMt_gastos_mes_aval.Text = dtCreditTemp.Rows[0]["Mt_Gastos_Mes_Aval"].ToString();
            //
            }
        }
        //end add
        // RSA 20130813 Enable editing, load existing credit values
        private void LoadCreditRecord(DataTable dtAllCreditProperties)
        {
            // At this point it has been already validated that dtCredit & dtAuxiliar != null and Rows.Count > 0
            //Load existing data
            //First Section
            ddlDX_TIPO_SOCIEDAD.SelectedIndex = ddlDX_TIPO_SOCIEDAD.Items.IndexOf(ddlDX_TIPO_SOCIEDAD.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Tipo_Sociedad"].ToString()));
            txtName.Text = dtAllCreditProperties.Rows[0]["Dx_Nombres"].ToString();
            txtLastname.Text = dtAllCreditProperties.Rows[0]["Dx_Apellido_Paterno"].ToString();
            ckbLastname.Checked = string.IsNullOrEmpty(txtLastname.Text);
            reqTxtLastName.Enabled = !ckbLastname.Checked;
            txtMotherName.Text = dtAllCreditProperties.Rows[0]["Dx_Apellido_Materno"].ToString();
            ckbMothername.Checked = string.IsNullOrEmpty(txtMotherName.Text);
            reqTxtMotherName.Enabled = !ckbMothername.Checked;

            DateTime birthDate;
            if (DateTime.TryParse(dtAllCreditProperties.Rows[0]["Dt_Nacimiento_Fecha"].ToString(), out birthDate))
                txtBirthDate.Text = birthDate.ToString("yyyy-MM-dd");
            else
                txtBirthDate.Text = dtAllCreditProperties.Rows[0]["Dt_Nacimiento_Fecha"].ToString();
            ddlDX_TIPO_INDUSTRIA.SelectedIndex = ddlDX_TIPO_INDUSTRIA.Items.IndexOf(ddlDX_TIPO_INDUSTRIA.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Tipo_Industria"].ToString()));
            txtTelephone.Text = dtAllCreditProperties.Rows[0]["ATB04"].ToString();
            txtEmail.Text = dtAllCreditProperties.Rows[0]["ATB05"].ToString();
            txtDX_CURP.Text = dtAllCreditProperties.Rows[0]["Dx_CURP"].ToString();
            txtDx_Nombre_Comercial.Text = dtAllCreditProperties.Rows[0]["Dx_Nombre_Comercial"].ToString();
            txtDX_RFC.Text = dtAllCreditProperties.Rows[0]["Dx_RFC"].ToString();
            txtDX_NOMBRE_REPRE_LEGAL.Text = dtAllCreditProperties.Rows[0]["Dx_Nombre_Repre_Legal"].ToString();
            ddlDX_ACREDITACION_REPRE_LEGAL.SelectedValue = dtAllCreditProperties.Rows[0]["Cve_Acreditacion_Repre_legal"].ToString();
            // RSA 20130816 Enable editing, weird things happened to an event handler when checking one option
            // without unchecking the other (cause it was checked by the InitDefaultData step?)
            if (dtAllCreditProperties.Rows[0]["Fg_Sexo_Repre_legal"].ToString().Equals("1"))
            {
                radFG_SEXO_REPRE_LEGAL1.Checked = true;
                radFG_SEXO_REPRE_LEGAL2.Checked = false;
            }
            else if (dtAllCreditProperties.Rows[0]["Fg_Sexo_Repre_legal"].ToString().Equals("2"))
            {
                radFG_SEXO_REPRE_LEGAL1.Checked = false;
                radFG_SEXO_REPRE_LEGAL2.Checked = true;
            }
            txtNO_RPU.Text = dtAllCreditProperties.Rows[0]["No_RPU"].ToString();
            if (dtAllCreditProperties.Rows[0]["Fg_Edo_Civil_Repre_legal"].ToString().Equals("1"))
            {
                radFG_EDO_CIVIL_REPRE_LEGAL1.Checked = true;
                radFG_EDO_CIVIL_REPRE_LEGAL2.Checked = false;
            }
            else if (dtAllCreditProperties.Rows[0]["Fg_Edo_Civil_Repre_legal"].ToString().Equals("2"))
            {
                radFG_EDO_CIVIL_REPRE_LEGAL1.Checked = false;
                radFG_EDO_CIVIL_REPRE_LEGAL2.Checked = true;
            }
            // RSA 20130816 Group logic of Reg Conyugal visibility
            ViewddlDX_REG_CONYUGAL_REPRE_LEGAL();

            ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedIndex = ddlDX_REG_CONYUGAL_REPRE_LEGAL.Items.IndexOf(ddlDX_REG_CONYUGAL_REPRE_LEGAL.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Reg_Conyugal_Repre_legal"].ToString()));
            ddlDX_IDENTIFICACION_REPRE_LEGAL.SelectedIndex = ddlDX_IDENTIFICACION_REPRE_LEGAL.Items.IndexOf(ddlDX_IDENTIFICACION_REPRE_LEGAL.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Identificacion_Repre_legal"].ToString()));
            this.txtDX_NO_IDENTIFICACION_REPRE_LEGAL.Text = dtAllCreditProperties.Rows[0]["Dx_No_Identificacion_Repre_Legal"].ToString();
            float valor;
            if (float.TryParse(dtAllCreditProperties.Rows[0]["Mt_Ventas_Mes_Empresa"].ToString(), out valor))
                this.txtMT_VENTAS_MES_EMPRESA.Text = ((int)valor).ToString();
            else
                this.txtMT_VENTAS_MES_EMPRESA.Text = dtAllCreditProperties.Rows[0]["Mt_Ventas_Mes_Empresa"].ToString();
            if (float.TryParse(dtAllCreditProperties.Rows[0]["Mt_Gastos_Mes_Empresa"].ToString(), out valor))
                this.txtMT_GASTOS_MES_EMPRESA.Text = ((int)valor).ToString();
            else
                this.txtMT_GASTOS_MES_EMPRESA.Text = dtAllCreditProperties.Rows[0]["Mt_Gastos_Mes_Empresa"].ToString();
            txtDX_EMAIL_REPRE_LEGAL.Text = dtAllCreditProperties.Rows[0]["Dx_Email_Repre_legal"].ToString();
            if (float.TryParse(dtAllCreditProperties.Rows[0]["No_consumo_promedio"].ToString(), out valor))
                this.txtNO_CONSUMO_PROMEDIO.Text = ((int)valor).ToString();
            else
                this.txtNO_CONSUMO_PROMEDIO.Text = dtAllCreditProperties.Rows[0]["No_consumo_promedio"].ToString();
            //Second Section
            txtDx_Domicilio_fisc_calle.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Fisc_Calle"].ToString();
            txtDx_Domicilio_fisc_num.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Fisc_Num"].ToString();
            txtDx_Domicilio_fisc_cp.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Fisc_CP"].ToString();
            ddlDx_nombre_estado.SelectedIndex = ddlDx_nombre_estado.Items.IndexOf(ddlDx_nombre_estado.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Estado_Fisc"].ToString()));
            ddlDx_nombre_estado_SelectedIndexChanged(null, null);
            ddlDx_deleg_municipio.SelectedIndex = ddlDx_deleg_municipio.Items.IndexOf(ddlDx_deleg_municipio.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Deleg_Municipio_Fisc"].ToString()));
            //txtCiudad.Text = dtCreditTemp.Rows[0]["Dx_Ciudad"].ToString();
            //txtInterior.Text = dtCreditTemp.Rows[0]["Dx_Numero_Interior"].ToString();
            ddlDx_Tipo_propiedad.SelectedIndex = ddlDx_Tipo_propiedad.Items.IndexOf(ddlDx_Tipo_propiedad.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Tipo_Propiedad_Fisc"].ToString()));
            txtDx_tel_neg.Text = dtAllCreditProperties.Rows[0]["Dx_Tel_Fisc"].ToString();
            txtDx_Domicilio_Fisc_Colonia.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Fisc_Colonia"].ToString();
            //Third Section
            if (dtAllCreditProperties.Rows[0]["Fg_Mismo_Domicilio"].ToString().Equals("True") || dtAllCreditProperties.Rows[0]["Fg_Mismo_Domicilio"].Equals("1"))
            {
                ckbFg_mismo_domicilio.Checked = true;
            }
            else
            {
                ckbFg_mismo_domicilio.Checked = false;
            }
            txtDx_domicilio_neg_calle.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Neg_Calle"].ToString();
            txtDx_domicilio_neg_num.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Neg_Num"].ToString();
            txtDx_domicilio_neg_cp.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Neg_CP"].ToString();
            ddlDx_nombre_estado_Neg.SelectedIndex = ddlDx_nombre_estado_Neg.Items.IndexOf(ddlDx_nombre_estado_Neg.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Estado_Neg"].ToString()));
            ddlDx_nombre_estado_Neg_SelectedIndexChanged(null, null);
            ddlDx_deleg_municipio_Neg.SelectedIndex = ddlDx_deleg_municipio_Neg.Items.IndexOf(ddlDx_deleg_municipio_Neg.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Deleg_Municipio_Neg"].ToString()));
            ddlDx_Tipo_propiedad_Neg.SelectedIndex = ddlDx_Tipo_propiedad_Neg.Items.IndexOf(ddlDx_Tipo_propiedad_Neg.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Tipo_Propiedad_Neg"].ToString()));
            txtDx_tel_neg_Neg.Text = dtAllCreditProperties.Rows[0]["Dx_Tel_Neg"].ToString();
            txtDx_Domicilio_Neg_Colonia.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Neg_Colonia"].ToString();
            //Fourth Section
            // RSA 20131007 read first, last and mother names if available, otherwise use the old field
            txtDx_First_Name_aval.Text = string.IsNullOrEmpty(dtAllCreditProperties.Rows[0]["Dx_First_Name_Aval"].ToString())
                ? dtAllCreditProperties.Rows[0]["Dx_nombre_Aval"].ToString() : dtAllCreditProperties.Rows[0]["Dx_First_Name_Aval"].ToString();
            txtDx_Last_Name_aval.Text = dtAllCreditProperties.Rows[0]["Dx_Last_Name_Aval"].ToString();
            txtDx_Mother_Name_aval.Text = dtAllCreditProperties.Rows[0]["Dx_Mother_Name_Aval"].ToString();
            if (DateTime.TryParse(dtAllCreditProperties.Rows[0]["Dt_BirthDate_Aval"].ToString(), out birthDate))
                txtDt_BirthDate_Aval.Text = birthDate.ToString("yyyy-MM-dd");
            else
                txtDt_BirthDate_Aval.Text = dtAllCreditProperties.Rows[0]["Dt_BirthDate_Aval"].ToString();
            // RSA 20131007 Add fields for both RFC and CURP
            txtDx_RFC_Aval.Text = dtAllCreditProperties.Rows[0]["Dx_RFC_Aval"].ToString();
            txtDx_CURP_Aval.Text = dtAllCreditProperties.Rows[0]["Dx_CURP_Aval"].ToString();
            // But if they are empty read the old RFC_CURP value and assign it depending on it's size
            if (string.IsNullOrEmpty(txtDx_CURP_Aval.Text + txtDx_RFC_Aval.Text))
            {
                if (dtAllCreditProperties.Rows[0]["Dx_RFC_CURP_Aval"].ToString().Length > 13)
                    txtDx_CURP_Aval.Text = dtAllCreditProperties.Rows[0]["Dx_RFC_CURP_Aval"].ToString();
                else
                    txtDx_RFC_Aval.Text = dtAllCreditProperties.Rows[0]["Dx_RFC_CURP_Aval"].ToString();
            }
            txtDx_tel_aval.Text = dtAllCreditProperties.Rows[0]["Dx_Tel_Aval"].ToString();
            if (dtAllCreditProperties.Rows[0]["Fg_Sexo_Aval"].ToString().Equals("1"))
            {
                radDx_sexo1.Checked = true;
                radDx_sexo2.Checked = false;
            }
            else if (dtAllCreditProperties.Rows[0]["Fg_Sexo_Aval"].ToString().Equals("2"))
            {
                radDx_sexo1.Checked = false;
                radDx_sexo2.Checked = true;
            }
            txtDx_domicilio_aval_calle.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Aval_Calle"].ToString();
            txtDx_domicilio_aval_num.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Aval_Num"].ToString();
            txtDx_domicilio_aval_cp.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Aval_CP"].ToString();
            ddlDx_nombre_estado_aval.SelectedIndex = ddlDx_nombre_estado_aval.Items.IndexOf(ddlDx_nombre_estado_aval.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Estado_Aval"].ToString()));
            ddlDx_nombre_estado_aval_SelectedIndexChanged(null, null);
            ddlDx_deleg_municipio_aval.SelectedIndex = ddlDx_deleg_municipio_aval.Items.IndexOf(ddlDx_deleg_municipio_aval.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Deleg_Municipio_Aval"].ToString()));
            txtDx_Domicilio_Aval_Colonia.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Aval_Colonia"].ToString();
            //    txtNo_RPU_aval.Text = dtCredit.Rows[0]["No_RPU_AVAL"].ToString();
            //    txtMt_ventas_mes_aval.Text = dtCredit.Rows[0]["Mt_Ventas_Mes_Aval"].ToString();
            //    txtMt_gastos_mes_aval.Text = dtCredit.Rows[0]["Mt_Gastos_Mes_Aval"].ToString();
            //

            // Coacreditado
            txtDx_nombre_coacreditado.Text = dtAllCreditProperties.Rows[0]["Dx_Nombre_Coacreditado"].ToString();
            txtDx_RFC_CURP_Coacreditado.Text = dtAllCreditProperties.Rows[0]["Dx_RFC_CURP_Coacreditado"].ToString();
            txtDx_telefono_coacreditado.Text = dtAllCreditProperties.Rows[0]["Dx_Telefono_Coacreditado"].ToString();
            txtDx_Domicilio_Coacreditado_Colonia.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Coacreditado_Colonia"].ToString();

            if (dtAllCreditProperties.Rows[0]["Fg_Sexo_Coacreditado"].ToString().Equals("1"))
            {
                RadioButton11.Checked = true;
                RadioButton12.Checked = false;
            }
            else if (dtAllCreditProperties.Rows[0]["Fg_Sexo_Coacreditado"].ToString().Equals("2"))
            {
                RadioButton11.Checked = false;
                RadioButton12.Checked = true;
            }

            txtDx_domicilio_coacreditado_calle.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Coacreditado_Calle"].ToString();
            txtDx_domicilio_coacreditado_num.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Coacreditado_Num"].ToString();
            txtDx_domicilio_coacreditado_cp.Text = dtAllCreditProperties.Rows[0]["Dx_Domicilio_Coacreditado_CP"].ToString();
            ddlDx_nombre_estado2.SelectedIndex = ddlDx_nombre_estado2.Items.IndexOf(ddlDx_nombre_estado2.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Estado_Coacreditado"].ToString()));
            ddlDx_nombre_estado2_SelectedIndexChanged(null, null);
            ddlDx_deleg_municipio2.SelectedIndex = ddlDx_deleg_municipio2.Items.IndexOf(ddlDx_deleg_municipio2.Items.FindByValue(dtAllCreditProperties.Rows[0]["Cve_Deleg_Municipio_Coacreditado"].ToString()));
        
            // RSA 20131023 Save the original total amount of the credital
            hfTotal.Value = dtAllCreditProperties.Rows[0]["Mt_Monto_Solicitado"].ToString();
        }
        // END
        #endregion

        #region Prepare Drop Down List Data
        private void BindddlDX_TIPO_INDUSTRIA()
        {
            DataTable IndustriaTypeDataTable = CAT_TIPO_INDUSTRIABLL.ClassInstance.Get_All_CAT_TIPO_INDUSTRIA();

            if (IndustriaTypeDataTable != null)
            {
                ddlDX_TIPO_INDUSTRIA.DataSource = IndustriaTypeDataTable;
                ddlDX_TIPO_INDUSTRIA.DataValueField = "Cve_Tipo_Industria";
                ddlDX_TIPO_INDUSTRIA.DataTextField = "Dx_Tipo_Industria";
                ddlDX_TIPO_INDUSTRIA.DataBind();
                ddlDX_TIPO_INDUSTRIA.Items.Insert(0, "");
                if (IndustriaTypeDataTable.Rows.Count > 0)
                {
                    ddlDX_TIPO_INDUSTRIA.SelectedIndex = 1;
                }
                else
                {
                    ddlDX_TIPO_INDUSTRIA.SelectedIndex = 0;
                }
            }
        }
        private void BindddlDX_TIPO_SOCIEDAD()
        {
            DataTable SociedadTypeDataTable = CAT_TIPO_SOCIEDADBLL.ClassInstance.Get_All_CAT_TIPO_SOCIEDAD();
            if (SociedadTypeDataTable != null)
            {
                ddlDX_TIPO_SOCIEDAD.DataSource = SociedadTypeDataTable;
                ddlDX_TIPO_SOCIEDAD.DataValueField = "Cve_Tipo_Sociedad";
                ddlDX_TIPO_SOCIEDAD.DataTextField = "Dx_Tipo_Sociedad";
                ddlDX_TIPO_SOCIEDAD.DataBind();
                ddlDX_TIPO_SOCIEDAD.Items.Insert(0, "");
                if (SociedadTypeDataTable.Rows.Count > 0)
                {
                    ddlDX_TIPO_SOCIEDAD.SelectedIndex = 1;
                    txtDX_CURP.Enabled = true;
                    revDX_CURP.Enabled = true;
                    rfvDX_CURP.Enabled = true;
                }
                else
                {
                    ddlDX_TIPO_SOCIEDAD.SelectedIndex = 0;
                }
            }
        }
        private void BindddlDX_TIPO_ACREDITACION()
        {
            DataTable AcreditAcionDataTable = CAT_TIPO_ACREDITACIONBLL.ClassInstance.Get_All_CAT_TIPO_ACREDITACION();
            if (AcreditAcionDataTable != null)
            {
                ddlDX_ACREDITACION_REPRE_LEGAL.DataSource = AcreditAcionDataTable;
                ddlDX_ACREDITACION_REPRE_LEGAL.DataValueField = "Cve_Acreditacion_Repre_legal";
                ddlDX_ACREDITACION_REPRE_LEGAL.DataTextField = "Dx_Acreditacion_Repre_Legal";
                ddlDX_ACREDITACION_REPRE_LEGAL.DataBind();
                ddlDX_ACREDITACION_REPRE_LEGAL.Items.Insert(0, "");
                ddlDX_ACREDITACION_REPRE_LEGAL.SelectedIndex = 0;
            }
        }
        private void BindddlDX_REG_CONYUGAL_REPRE_LEGAL()
        {
            DataTable RegimenConyugalDatatable = CAT_REGIMEN_CONYUGALBLL.ClassInstance.Get_All_CAT_REGIMEN_CONYUGAL();
            if (RegimenConyugalDatatable != null)
            {
                ddlDX_REG_CONYUGAL_REPRE_LEGAL.DataSource = RegimenConyugalDatatable;
                ddlDX_REG_CONYUGAL_REPRE_LEGAL.DataValueField = "Cve_Reg_Conyugal_Repre_legal";
                ddlDX_REG_CONYUGAL_REPRE_LEGAL.DataTextField = "Dx_Reg_Conyugal_Repre_legal";
                ddlDX_REG_CONYUGAL_REPRE_LEGAL.DataBind();
                ddlDX_REG_CONYUGAL_REPRE_LEGAL.Items.Insert(0, "");
                ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedIndex = 0;
            }
        }
        private void BindddlDX_IDENTIFICACION_REPRE_LEGAL()
        {
            DataTable IdentificationDataTable = CAT_IDENTIFICACIONBLL.ClassInstance.Get_All_CAT_IDENTIFICACION();
            if (IdentificationDataTable != null)
            {
                ddlDX_IDENTIFICACION_REPRE_LEGAL.DataSource = IdentificationDataTable;
                ddlDX_IDENTIFICACION_REPRE_LEGAL.DataValueField = "Cve_Identificacion_Repre_legal";
                ddlDX_IDENTIFICACION_REPRE_LEGAL.DataTextField = "Dx_Identificacion_Repre_legal";
                ddlDX_IDENTIFICACION_REPRE_LEGAL.DataBind();
                ddlDX_IDENTIFICACION_REPRE_LEGAL.Items.Insert(0, "");
                ddlDX_IDENTIFICACION_REPRE_LEGAL.SelectedIndex = 0;
            }
        }
        private void BindddlDx_nombre_estado()
        {
            DataTable EstadoDataTable = CAT_ESTADOBLL.ClassInstance.Get_All_CAT_ESTADO();
            if (EstadoDataTable != null)
            {
                ddlDx_nombre_estado.DataSource = EstadoDataTable;
                ddlDx_nombre_estado.DataValueField = "Cve_Estado";
                ddlDx_nombre_estado.DataTextField = "Dx_Nombre_Estado";
                ddlDx_nombre_estado.DataBind();
                ddlDx_nombre_estado.Items.Insert(0, "");
                ddlDx_nombre_estado.SelectedIndex = 0;

                ddlDx_nombre_estado_Neg.DataSource = EstadoDataTable;
                ddlDx_nombre_estado_Neg.DataValueField = "Cve_Estado";
                ddlDx_nombre_estado_Neg.DataTextField = "Dx_Nombre_Estado";
                ddlDx_nombre_estado_Neg.DataBind();
                ddlDx_nombre_estado_Neg.Items.Insert(0, "");
                ddlDx_nombre_estado_Neg.SelectedIndex = 0;

                ddlDx_nombre_estado_aval.DataSource = EstadoDataTable;
                ddlDx_nombre_estado_aval.DataValueField = "Cve_Estado";
                ddlDx_nombre_estado_aval.DataTextField = "Dx_Nombre_Estado";
                ddlDx_nombre_estado_aval.DataBind();
                ddlDx_nombre_estado_aval.Items.Insert(0, "");
                ddlDx_nombre_estado_aval.SelectedIndex = 0;
            }
        }
        private void BindddlDx_deleg_municipio()
        {
            int IEstado = -1;
            if (!ddlDx_nombre_estado.SelectedValue.Equals(""))
            {
                IEstado =Convert.ToInt32(ddlDx_nombre_estado.SelectedValue);
            }
            //edit by coco 2011-08-11
            DataTable DelegMunicipoDataTable = FilterDelegMunicipio(IEstado);// CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(IEstado);           
            //end edit
            if (DelegMunicipoDataTable != null)
            {
                ddlDx_deleg_municipio.DataSource = DelegMunicipoDataTable;
                ddlDx_deleg_municipio.DataValueField = "Cve_Deleg_Municipio";
                ddlDx_deleg_municipio.DataTextField = "Dx_Deleg_Municipio";
                ddlDx_deleg_municipio.DataBind();
                ddlDx_deleg_municipio.Items.Insert(0, "");
                ddlDx_deleg_municipio.SelectedIndex = 0;
            }
        }

        private DataTable FilterDelegMunicipio(int IEstado)
        {
            DataTable tempDataTable;
            if (HttpContext.Current.Cache["DeletMunicipio"] == null)
            {
                    DataTable dtDelegMunicipio = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_All_CAT_DELEG_MUNICIPIO();
                    Cache.Insert("DeletMunicipio", dtDelegMunicipio);
            }
            // Update by Tina 2011/08/18
            //tempDataTable = (DataTable)HttpContext.Current.Cache["DeletMunicipio"];
            tempDataTable = ((DataTable)HttpContext.Current.Cache["DeletMunicipio"]).Clone();
            tempDataTable.Merge((DataTable)HttpContext.Current.Cache["DeletMunicipio"]);
            // End
                DataView dv = tempDataTable.DefaultView;
                if (IEstado != -1)
                {
                    dv.RowFilter = "Cve_Estado=" + IEstado;
                    tempDataTable = dv.ToTable();
                }                
            return tempDataTable;
        }
        private void BindddlDx_deleg_municipio_Neg()
        {
            int IEstado = -1;
            if (!ddlDx_nombre_estado_Neg.SelectedValue.Equals(""))
            {
                IEstado = Convert.ToInt32(ddlDx_nombre_estado_Neg.SelectedValue);
            }
            //edit by coco 2011-08-11
            DataTable DelegMunicipoDataTable = FilterDelegMunicipio(IEstado);//CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(IEstado);
            //end edit
            if (DelegMunicipoDataTable != null)
            {
                ddlDx_deleg_municipio_Neg.DataSource = DelegMunicipoDataTable;
                ddlDx_deleg_municipio_Neg.DataValueField = "Cve_Deleg_Municipio";
                ddlDx_deleg_municipio_Neg.DataTextField = "Dx_Deleg_Municipio";
                ddlDx_deleg_municipio_Neg.DataBind();
                ddlDx_deleg_municipio_Neg.Items.Insert(0, "");
                ddlDx_deleg_municipio_Neg.SelectedIndex = 0;
            }
        }
        private void BindddlDx_deleg_municipio_aval()
        {
            int IEstado = -1;
            if (!ddlDx_nombre_estado_aval.SelectedValue.Equals(""))
            {
                IEstado = Convert.ToInt32(ddlDx_nombre_estado_aval.SelectedValue);
            }
            //edit by coco 2011-08-11
            DataTable DelegMunicipoDatatable = FilterDelegMunicipio(IEstado);//CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(IEstado);
           //end edit
            if (DelegMunicipoDatatable != null)
            {
                ddlDx_deleg_municipio_aval.DataSource = DelegMunicipoDatatable;
                ddlDx_deleg_municipio_aval.DataValueField = "Cve_Deleg_Municipio";
                ddlDx_deleg_municipio_aval.DataTextField = "Dx_Deleg_Municipio";
                ddlDx_deleg_municipio_aval.DataBind();
                ddlDx_deleg_municipio_aval.Items.Insert(0, "");
                ddlDx_deleg_municipio_aval.SelectedIndex = 0;
            }
        }
        private void BindddlDx_Tipo_propiedad()
        {
            DataTable PropiedadTypeDataTable = CAT_TIPO_PROPIEDADBLL.ClassInstance.Get_All_CAT_TIPO_PROPIEDAD();
            if (PropiedadTypeDataTable != null)
            {
                ddlDx_Tipo_propiedad.DataSource = PropiedadTypeDataTable;
                ddlDx_Tipo_propiedad.DataValueField = "Cve_Tipo_Propiedad";
                ddlDx_Tipo_propiedad.DataTextField = "Dx_Tipo_Propiedad";
                ddlDx_Tipo_propiedad.DataBind();
                ddlDx_Tipo_propiedad.Items.Insert(0, "");
                ddlDx_Tipo_propiedad.SelectedIndex = 0;

                ddlDx_Tipo_propiedad_Neg.DataSource = PropiedadTypeDataTable;
                ddlDx_Tipo_propiedad_Neg.DataValueField = "Cve_Tipo_Propiedad";
                ddlDx_Tipo_propiedad_Neg.DataTextField = "Dx_Tipo_Propiedad";
                ddlDx_Tipo_propiedad_Neg.DataBind();
                ddlDx_Tipo_propiedad_Neg.Items.Insert(0, "");
                ddlDx_Tipo_propiedad_Neg.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// initial Financial Support Person Dx_nombre_estado
        /// </summary>
        private void BindddlDx_nombre_estado2()
        {
            DataTable EstadoDataTable = CAT_ESTADOBLL.ClassInstance.Get_All_CAT_ESTADO();
            if (EstadoDataTable != null)
            {
                ddlDx_nombre_estado2.DataSource = EstadoDataTable;
                ddlDx_nombre_estado2.DataValueField = "Cve_Estado";
                ddlDx_nombre_estado2.DataTextField = "Dx_Nombre_Estado";
                ddlDx_nombre_estado2.DataBind();
                ddlDx_nombre_estado2.Items.Insert(0, "");
                ddlDx_nombre_estado2.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// initial Financial Support Person DX_DELEG_MUNICIPIO
        /// </summary>
        private void BindddlDX_DELEG_MUNICIPIO2()
        {
            int iEstado = -1;

            if (!ddlDx_nombre_estado2.SelectedValue.Equals(""))
            {
                iEstado = Convert.ToInt32(ddlDx_nombre_estado2.SelectedValue);
            }

            DataTable DelegMunicipoDatatable = FilterDelegMunicipio(iEstado); //CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(iEstado);
            if (DelegMunicipoDatatable != null)
            {
                ddlDx_deleg_municipio2.DataSource = DelegMunicipoDatatable;
                ddlDx_deleg_municipio2.DataValueField = "Cve_Deleg_Municipio";
                ddlDx_deleg_municipio2.DataTextField = "Dx_Deleg_Municipio";
                ddlDx_deleg_municipio2.DataBind();
                ddlDx_deleg_municipio2.Items.Insert(0, "");
                ddlDx_deleg_municipio2.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Init options for period pago
        /// </summary>
        private void InitStepThreeDropdownList()
        {
            DataTable dt = CAT_PERIODO_PAGODal.ClassInstance.Get_ALL_CAT_PERIODO_PAGO();
            ddlDx_periodo_pago.DataSource = dt;
            ddlDx_periodo_pago.DataValueField = "Cve_Periodo_Pago";
            ddlDx_periodo_pago.DataTextField = "Dx_Periodo_Pago";
            ddlDx_periodo_pago.DataBind();
            ddlDx_periodo_pago.Items.Insert(0, "");
            ddlDx_periodo_pago.SelectedIndex = 0;
        }
        #endregion

        #region Actions in Step One
        /// <summary>
        /// sociedad type changes to refresh related controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDX_TIPO_SOCIEDAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // RSA 20130816 if no selection do nothing, a validation will notify the user that this field is mandatory
            if (string.IsNullOrEmpty(ddlDX_TIPO_SOCIEDAD.SelectedValue))
            {
                Validate();
                return;
            }

            if (CompanyType.PERSONAFISICA != (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue) && CompanyType.REPECO != (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue))
            {
                txtDX_CURP.Enabled = false;
                revDX_CURP.Enabled = false;
                rfvDX_CURP.Enabled = false;

                lbl_EDO_CIVIL_REPRE_LEGAL.Visible = false;
                radFG_EDO_CIVIL_REPRE_LEGAL1.Visible = false;
                radFG_EDO_CIVIL_REPRE_LEGAL2.Visible = false;

                // RSA 20130917 reset the fields that don't apply for this type of society
                txtDX_CURP.Text = string.Empty;
                radFG_EDO_CIVIL_REPRE_LEGAL2.Checked = false;
                radFG_EDO_CIVIL_REPRE_LEGAL1.Checked = true;
                // RSA now reset the coacreditado controls based on the previous assignment
                ViewddlDX_REG_CONYUGAL_REPRE_LEGAL();
            }
            else
            {
                txtDX_CURP.Enabled = true;
                revDX_CURP.Enabled = true;
                rfvDX_CURP.Enabled = true;
                lblEmail.Visible = false;
                txtEmail.Visible = false;
                lblTelephone.Visible = false;
                txtTelephone.Visible = false;

                lbl_EDO_CIVIL_REPRE_LEGAL.Visible = true;
                radFG_EDO_CIVIL_REPRE_LEGAL1.Visible = true;
                radFG_EDO_CIVIL_REPRE_LEGAL2.Visible = true;
                // RSA 20130816 Group logic of Reg Conyugal visibility
                // only if it applies to the Tipo Sociedad
                ViewddlDX_REG_CONYUGAL_REPRE_LEGAL();
            }
            // RSA 2012-10-19 Apply to REPECO too
            if (CompanyType.PERSONAFISICA != (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue) && CompanyType.REPECO != (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue))
            {
                //DivEnable.Visible = false;
                //lblBirthDate.Visible = false;
                //txtBirthDate.Visible = false;
                lblBirthDate.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelBirthDateMoral").ToString() + " (*)";
                lblName.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelPersonaRazon").ToString() + " (*)";

                //txtBirthDate.Text = "";
                lblLastname.Visible = false;
                txtLastname.Visible = false;
                reqTxtLastName.Enabled = false;
                ckbLastname.Visible = false;
                //txtLastname.Text = "";
                lblMotherName.Visible = false;
                txtMotherName.Visible = false;
                reqTxtMotherName.Enabled = false;
                ckbMothername.Visible = false;
                //txtMotherName.Text = "";
                revRFC12.Enabled = true;
                revRFC13.Enabled = false;

                // RSA 20130917 Clear the values that doesn't apply
                // txtBirthDate.Text = string.Empty;
                txtLastname.Text = string.Empty;
                txtMotherName.Text = string.Empty;

                // RSA 20130917 set validation of birthdate
                //revTxtBirthDate.Enabled = false;
                //rfvTxtBirthDate.Enabled = false;
            }
            else
            {
                //DivEnable.Visible = true;
                //lblBirthDate.Visible = true;
                //txtBirthDate.Visible = true;
                lblBirthDate.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelBirthDateInmoral").ToString() + " (*)";
                lblName.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelPersonaNombre").ToString() + " (*)";

                lblLastname.Visible = true;
                txtLastname.Visible = true;
                ckbLastname.Visible = true;
                ApplyApellido(ckbLastname, txtLastname, reqTxtLastName); // enable validation depending on checkbox
                lblMotherName.Visible = true;
                txtMotherName.Visible = true;
                ckbMothername.Visible = true;
                ApplyApellido(ckbMothername, txtMotherName, reqTxtMotherName); // enable validation depending on checkbox
                lblEmail.Visible = true;
                txtEmail.Visible = true;
                lblTelephone.Visible = true;
                txtTelephone.Visible = true;

                revRFC12.Enabled = false;
                revRFC13.Enabled = true;

                // RSA 20130917 set validation of birthdate
                //revTxtBirthDate.Enabled = true;
                //rfvTxtBirthDate.Enabled = true;
            }
        }
        protected void ddlDX_REG_CONYUGAL_REPRE_LEGAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetBienesMancomunados();
        }

        /// <summary>
        /// logic for civil representante legal checked changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void radFG_EDO_CIVIL_REPRE_LEGAL1_CheckedChanged(object sender, EventArgs e)
        {
            // RSA 20130816 Group logic of Reg Conyugal visibility
            ViewddlDX_REG_CONYUGAL_REPRE_LEGAL();
        }
        /// <summary>
        /// Enable “REGIMEN MATRIMONIAL” only if the field “ESTADO CIVIL” selection is “CASADO(A)”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void radFG_EDO_CIVIL_REPRE_LEGAL2_CheckedChanged(object sender, EventArgs e)
        {
            // RSA 20130816 Group logic of Reg Conyugal visibility
            ViewddlDX_REG_CONYUGAL_REPRE_LEGAL();
        }
        /// <summary>
        /// Hide or show REG_CONYUGAL selector based on EDO_CIVIL selection
        /// </summary>
        // RSA 20130816 Group logic of Reg Conyugal visibility
        private void ViewddlDX_REG_CONYUGAL_REPRE_LEGAL()
        {
            if (radFG_EDO_CIVIL_REPRE_LEGAL2.Checked)
            {
                // ddlDX_REG_CONYUGAL_REPRE_LEGAL.Enabled = true;
                lblDX_REG_CONYUGAL_REPRE_LEGAL.Visible = true;
                ddlDX_REG_CONYUGAL_REPRE_LEGAL.Visible = true;
            }
            else
            {
                ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedIndex = 0;
                // ddlDX_REG_CONYUGAL_REPRE_LEGAL.Enabled = false;
                lblDX_REG_CONYUGAL_REPRE_LEGAL.Visible = false;
                ddlDX_REG_CONYUGAL_REPRE_LEGAL.Visible = false;

                ResetBienesMancomunados();
            }
        }
        private void ResetBienesMancomunados()
        {
            // RSA 20130917 Clear information related to the Coacreditado if it doesn't apply
            if (ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedIndex != 2)
            {
                txtDx_nombre_coacreditado.Text = string.Empty;
                txtDx_RFC_CURP_Coacreditado.Text = string.Empty;
                txtDx_telefono_coacreditado.Text = string.Empty;
                txtDx_Domicilio_Coacreditado_Colonia.Text = string.Empty;

                RadioButton11.Checked = true;
                RadioButton12.Checked = false;

                txtDx_domicilio_coacreditado_calle.Text = string.Empty;
                txtDx_domicilio_coacreditado_num.Text = string.Empty;
                txtDx_domicilio_coacreditado_cp.Text = string.Empty;

                ddlDx_nombre_estado2.SelectedIndex = 0;
                ddlDx_deleg_municipio2.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Refresh deleg municipio when estado changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDx_nombre_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDx_deleg_municipio();
        }
        /// <summary>
        /// if ckbFg_mismo_domicilio checked Copy FISCAL ADDRESS for BUSINESS ADDRESS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbFg_mismo_domicilio_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbFg_mismo_domicilio.Checked)
            {
                txtDx_domicilio_neg_calle.Text = txtDx_Domicilio_fisc_calle.Text;
                txtDx_domicilio_neg_num.Text = txtDx_Domicilio_fisc_num.Text;
                txtDx_domicilio_neg_cp.Text = txtDx_Domicilio_fisc_cp.Text;
                if (ddlDx_nombre_estado_Neg.SelectedValue != "" && this.ddlDx_nombre_estado_Neg.SelectedValue != this.ddlDx_nombre_estado.SelectedValue)
                {                    
                    //this.ddlDx_nombre_estado_Neg.SelectedIndex =this.ddlDx_nombre_estado_Neg.Items.IndexOf(this.ddlDx_nombre_estado_Neg.Items.FindByValue(this.ddlDx_nombre_estado.SelectedValue));
                    this.ddlDx_nombre_estado_Neg.SelectedValue = this.ddlDx_nombre_estado.SelectedValue;
                    this.ddlDx_nombre_estado_Neg_SelectedIndexChanged(this, new EventArgs());
                    ddlDx_deleg_municipio_Neg.SelectedValue = ddlDx_deleg_municipio.SelectedValue;
                }
                else
                {
                    this.ddlDx_nombre_estado_Neg.SelectedValue = this.ddlDx_nombre_estado.SelectedValue;
                    ddlDx_nombre_estado_Neg_SelectedIndexChanged(null, null);
                    ddlDx_deleg_municipio_Neg.SelectedValue = ddlDx_deleg_municipio.SelectedValue;
                }
                
                ddlDx_Tipo_propiedad_Neg.SelectedValue = ddlDx_Tipo_propiedad.SelectedValue;
                txtDx_tel_neg_Neg.Text = txtDx_tel_neg.Text;
                txtDx_Domicilio_Neg_Colonia.Text = txtDx_Domicilio_Fisc_Colonia.Text;
            }
            else
            {
                txtDx_domicilio_neg_calle.Text = "";
                txtDx_domicilio_neg_num.Text = "";
                txtDx_domicilio_neg_cp.Text = "";
                ddlDx_nombre_estado_Neg.SelectedIndex = 0;
                ddlDx_deleg_municipio_Neg.SelectedIndex = 0;
                ddlDx_Tipo_propiedad_Neg.SelectedIndex = 0;
                txtDx_tel_neg_Neg.Text = "";
                txtDx_Domicilio_Neg_Colonia.Text = "";
            }
        }
        protected void ckbLastname_CheckedChanged(object sender, EventArgs e)
        {
            ApplyApellido(ckbLastname, txtLastname, reqTxtLastName);
        }
        protected void ckbMothername_CheckedChanged(object sender, EventArgs e)
        {
            ApplyApellido(ckbMothername, txtMotherName, reqTxtMotherName);
        }
        private void ApplyApellido(CheckBox flag, TextBox entry, RequiredFieldValidator validator)
        {
            if (flag.Checked)
            {
                entry.Text = string.Empty;
                entry.Enabled = false;
                validator.Enabled = false;
            }
            else
            {
                entry.Enabled = true;
                validator.Enabled = true;
            }
        }
        /// <summary>
        /// Refresh deleg municipio when estado changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDx_nombre_estado_Neg_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDx_deleg_municipio_Neg();
        }
        /// <summary>
        /// Refresh deleg municipio when estado changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDx_nombre_estado_aval_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDx_deleg_municipio_aval();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void txtNo_RPU_aval_TextChanged(object sender, EventArgs e)
        //{          
        //    if (!string.IsNullOrEmpty(txtNo_RPU_aval.Text))
        //    {
        //        if (IsServiceCodeLongEnough(txtNo_RPU_aval.Text))

        //        {
        //            Dics = SICOMHelper.GetAttributes(txtNo_RPU_aval.Text);
        //            if (!ValidateRate(Dics["Rate"]) || !ValidateUserStatus(Dics["UserStatus"]) || !ValidateNoDebit(Dics["CurrentBillingStatus"], Dics["DueDate"]) || !ValidateMinConsumptionDate(Dics["MinConsumptionDate"]))
        //            {
        //                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "ServiceCodeIsInvalidate") as string;
        //                ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('" + strMsg + "');", true);
        //                txtNo_RPU_aval.Focus();
        //            }
        //        }             
        //    }
            
        //}
        #endregion

        #region Actions in Step Two
        /// <summary>
        /// Refresh deleg municipio when estado changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDx_nombre_estado2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDX_DELEG_MUNICIPIO2();
        }
        #endregion

        #region Actions in Step Three
        /// <summary>
        /// Refresh related controls when technology selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTecnolog_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DropDownTech = (DropDownList)sender;
            string Tech = DropDownTech.SelectedValue;
            GridViewRow gridViewRow = (GridViewRow)DropDownTech.NamingContainer;
            int RowIndex = Convert.ToInt32(gridViewRow.RowIndex.ToString());
            DropDownList DropDownPro = gvTecPro.Rows[RowIndex].FindControl("ddlProduct") as DropDownList;
            DropDownList DropDownMarca = gvTecPro.Rows[RowIndex].FindControl("ddlMarca") as DropDownList;
            //add by coco 20110823
            DropDownList DropDownTypeOfProduct = gvTecPro.Rows[RowIndex].FindControl("ddlTypeOfProduct") as DropDownList;
            TextBox txtCantidad  = gvTecPro.Rows[RowIndex].FindControl("txtCantidad") as TextBox;
            TextBox txtGastos  = gvTecPro.Rows[RowIndex].FindControl("txtGastos") as TextBox;
            //end add

            // RSA 20130903 make it visible iff technology is set to one with CveGasto == 1
            txtGastos.Visible = DropDownTech.SelectedIndex > 0 && CveGasto[DropDownTech.SelectedValue] == 1;

            //Reload product options when technology changes
            //edit by coco 20110823          
            if (Tech.Equals(""))
            {
                Tech = technologyID;
                // DropDownTypeOfProduct.Enabled = false;
            }
            else
            {
                // DropDownTypeOfProduct.Enabled = true;
            }
            DropDownMarca.Enabled = false;
            DropDownPro.Enabled = false;

            DropDownTypeOfProduct.DataSource = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(Tech);
            DropDownTypeOfProduct.DataTextField = "Dx_Tipo_Producto";
            DropDownTypeOfProduct.DataValueField = "Ft_Tipo_Producto";
            DropDownTypeOfProduct.DataBind();
            DropDownTypeOfProduct.Items.Insert(0, "");
            DropDownTypeOfProduct.SelectedIndex = 0;
            //add by coco 20110905
            SetupTooltip("ddlTypeOfProduct");
            DropDownTypeOfProduct.ToolTip = DropDownTypeOfProduct.SelectedItem.Text;

            //RSA 20130910 Reset marca when technology is changed
            DataTable Product = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ByTechnology(Tech, "", idDepartment, DropDownTypeOfProduct.SelectedValue);
            string strProductID = "0";
            if (Product != null)
            {
                DropDownPro.DataSource = Product;
                DropDownPro.DataTextField = "Dx_Modelo_Producto";
                //end edit
                DropDownPro.DataValueField = "Cve_Producto";
                DropDownPro.DataBind();
                DropDownPro.Items.Insert(0, "");
                DropDownPro.SelectedIndex = 0;
                //add by coco 2011-08-10
                for (int j = 0; j < Product.Rows.Count; j++)
                {
                    if (!Product.Rows[j]["Cve_Producto"].ToString().Equals(""))
                    {
                        strProductID = strProductID + "," + Product.Rows[j]["Cve_Producto"].ToString();
                    }
                }
                //add by coco 20110905
                SetupTooltip("ddlProduct");
                DropDownPro.ToolTip = DropDownPro.SelectedItem.Text;
            }
            if (DropDownMarca != null)
            {
                string strTempProduct = "";
                if (!DropDownPro.SelectedValue.Equals(""))
                {
                    strTempProduct = DropDownPro.SelectedValue;
                }
                else
                {
                    strTempProduct = strProductID;
                }
                DataTable Marca = CAT_MARCADal.ClassInstance.Get_CAT_MARCADal(strTempProduct);
                if (Marca != null)
                {
                    DropDownMarca.DataSource = Marca;
                    DropDownMarca.DataTextField = "Dx_Marca";
                    DropDownMarca.DataValueField = "Cve_Marca";
                    DropDownMarca.DataBind();
                    DropDownMarca.Items.Insert(0, "");

                    // RSA 2012-9-13 Changed location of this step
                    //Changed by Coco 20110824: clear other selections
                    DropDownMarca.SelectedIndex = 0;
                }
                //add by coco 20110905
                SetupTooltip("ddlMarca");
                DropDownMarca.ToolTip = DropDownMarca.SelectedItem.Text;
                //Add by coco 2011-08-05
                //Reload product capacity options when technology changes
                DropDownList DropDownCapacidad = gvTecPro.Rows[RowIndex].FindControl("ddlCapacidad") as DropDownList;
                Label LabelCapacidad = gvTecPro.HeaderRow.FindControl("lblCapacidad") as Label;

                lDescription.Text = "";
                if (IsSE(Tech))
                {
                    btnAddRow.Enabled = false;
                    DataTable Capacity = new DataTable();
                    Capacity.Columns.Add("Cve_Producto_Capacidad");
                    Capacity.Columns.Add("Ft_Capacidad");
                    Capacity.Rows.Add(new object[] { "3", "OM" });
                    Capacity.Rows.Add(new object[] { "4", "HM" });
                    DropDownCapacidad.DataSource = Capacity;
                    DropDownCapacidad.DataBind();

                    txtCantidad.Text = "1";
                    txtCantidad.Enabled = false;

                    LabelCapacidad.Text = "Tarifa";

                    // RSA 2012-09-11 product selection modified
                    lDescription.Visible = true;
                    DropDownTypeOfProduct.SelectedIndex = 1;
                    //DropDownTypeOfProduct.Enabled = false;
                    //DropDownMarca.SelectedIndex = 1;
                    DropDownMarca.Enabled = true;   // false

                    DropDownPro.Enabled = true;
                }
                else
                {
                    btnAddRow.Enabled = true;
                    DropDownCapacidad.DataSource = ProductCapacityDal.ClassInstance.Get_ProductCapacity(Tech);
                    DropDownCapacidad.DataTextField = "Ft_Capacidad";
                    DropDownCapacidad.DataValueField = "Cve_Producto_Capacidad";
                    DropDownCapacidad.DataBind();
                    DropDownCapacidad.Items.Insert(0, "");

                    LabelCapacidad.Text = "Capacidad";

                    // RSA 2012-09-11 product selection complexified
                    lDescription.Visible = false;
                    //DropDownTypeOfProduct.Enabled = true;
                    DropDownMarca.Enabled = true;
                }
                DropDownCapacidad.SelectedIndex = 0;

                //Enable/Disable the capacity option control by technology selected
                DataTable dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIATipoByPK(DropDownTech.SelectedValue);
                if (dtTechnology != null && dtTechnology.Rows.Count > 0)
                {
                    if (!dtTechnology.Rows[0]["Dx_Nombre"].ToString().Trim().StartsWith("Iluminacion") && !IsSE(Tech))
                    {
                        DropDownCapacidad.Enabled = false;
                    }
                    else
                    {
                        DropDownCapacidad.Enabled = true;
                    }
                    //End add
                }
                ((TextBox)gridViewRow.FindControl("txtGridText1")).Text = "";//unit price clear
                ((TextBox)gridViewRow.FindControl("txtSubtotal")).Text = "";//clear sub total
                ((HiddenField)gridViewRow.FindControl("hfExactGridText1")).Value = "";//unit price clear
                ((HiddenField)gridViewRow.FindControl("hfExactSubtotal")).Value = "";//clear sub total
                ((TextBox)gridViewRow.FindControl("txtGastos")).Text = "";//clear sub total

                this.AccountTotal();
                //add by coco 20110905
                SetupTooltip("ddlTecnolog");
                DropDownTech.ToolTip = DropDownTech.SelectedItem.Text;
            }
        }

        private bool IsSE(string techId)
        {
            return DxCveCC.ContainsKey(techId) && DxCveCC[techId] == DxCveCC_SE;
        }
        /// <summary>//Added by Coco 2011/09/05
        /// Setup tootltip for drop down list control          
        /// </summary>
        /// <param name="strFlag"></param>
      private void SetupTooltip(string controlName)
        {
            for (int j = 0; j < gvTecPro.Rows.Count; j++)
            {
                DropDownList dropDownList = gvTecPro.Rows[j].FindControl(controlName) as DropDownList;
                for (int i = 0; i < dropDownList.Items.Count; i++)
                {
                    dropDownList.Items[i].Attributes.Add("Title", dropDownList.Items[i].Text);
                }
            }
        }
        /// <summary>
        /// Refresh related controls when marca selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strProductID = "0"; //add by coco 2011-08-10
            DropDownList DropDownMarca = (DropDownList)sender;
            string Marca = DropDownMarca.SelectedValue;
            GridViewRow gridViewRow = (GridViewRow)DropDownMarca.NamingContainer;
            int RowIndex = Convert.ToInt32(gridViewRow.RowIndex.ToString());
            DropDownList DropDownProduct = gvTecPro.Rows[RowIndex].FindControl("ddlProduct") as DropDownList;
            DropDownList DropDownTechnology = gvTecPro.Rows[RowIndex].FindControl("ddlTecnolog") as DropDownList;
            //add by coco 20110823
          DropDownList DropDownTypeOfProduct = gvTecPro.Rows[RowIndex].FindControl("ddlTypeOfProduct") as DropDownList;
            //end add
          if (Marca == "")
          {
              DropDownProduct.Enabled = false;
          }
          else
          {
              DropDownProduct.Enabled = true;
          }
            
            //Reload product when marca selection changed
          string strTempTech = "";
          if (DropDownTechnology.SelectedValue.Equals(""))
          {
              strTempTech = technologyID;
          }
          else
          {
              strTempTech = DropDownTechnology.SelectedValue;
          }

            DataTable dtProduct = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ByTechnology(strTempTech, Marca, idDepartment,DropDownTypeOfProduct.SelectedValue);//edit by coco 2011-08-10
            //add by coco 2011-08-10
            for (int i = 0; i < dtProduct.Rows.Count; i++)
            {
                if (!dtProduct.Rows[i]["Cve_Producto"].ToString().Equals(""))
                {
                    strProductID = strProductID + "," + dtProduct.Rows[i]["Cve_Producto"].ToString();
                }
            }
            //end add
            DropDownProduct.DataSource = dtProduct;
            DropDownProduct.DataTextField = "Dx_Modelo_Producto";
            DropDownProduct.DataValueField = "Cve_Producto";
            DropDownProduct.DataBind();
            DropDownProduct.Items.Insert(0, "");
            DropDownProduct.SelectedIndex = 0;       

            //Changed by Coco 20110824: clear other selections         
            ((TextBox)gridViewRow.FindControl("txtGridText1")).Text = "";//unit price clear
            ((TextBox)gridViewRow.FindControl("txtSubtotal")).Text = "";//clear sub total
            ((HiddenField)gridViewRow.FindControl("hfExactGridText1")).Value = "";//unit price clear
            ((HiddenField)gridViewRow.FindControl("hfExactSubtotal")).Value = "";//clear sub total
            this.AccountTotal();
           
            //add by coco 20110905     
            SetupTooltip("ddlTecnolog");
            DropDownTechnology.ToolTip = DropDownTechnology.SelectedItem.Text;
            SetupTooltip("ddlProduct");
            DropDownProduct.ToolTip = DropDownProduct.SelectedItem.Text;
            SetupTooltip("ddlMarca");
            DropDownMarca.ToolTip = DropDownMarca.SelectedItem.Text;
            SetupTooltip("ddlTypeOfProduct");
            DropDownTypeOfProduct.ToolTip = DropDownTypeOfProduct.SelectedItem.Text;
        }
        //add by coco 20110823
        /// <summary>
        /// Type of Product changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTypeOfProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DropDownTypeOfProduct = (DropDownList)sender;
            string TypeProduct = DropDownTypeOfProduct.SelectedValue;
            GridViewRow gridViewRow = (GridViewRow)DropDownTypeOfProduct.NamingContainer;
            int RowIndex = Convert.ToInt32(gridViewRow.RowIndex.ToString());
            DropDownList DropDownTechnology = gvTecPro.Rows[RowIndex].FindControl("ddlTecnolog") as DropDownList;
            DropDownList DropDownMarca = gvTecPro.Rows[RowIndex].FindControl("ddlMarca") as DropDownList;
            DropDownList DropDownModelo = gvTecPro.Rows[RowIndex].FindControl("ddlProduct") as DropDownList;
            DataTable dtTypeProduct = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByPk(TypeProduct);

            if (TypeProduct == "")
            {
                DropDownMarca.Enabled = false;
            }
            else
            {
                DropDownMarca.Enabled = true;
            }
            DropDownModelo.Enabled = false;
            
            //Changed by Coco 20110824: clear other selections
            DropDownMarca.SelectedIndex = 0;
            if (dtTypeProduct.Rows.Count > 0)
            {
                DropDownTechnology.SelectedValue = dtTypeProduct.Rows[0]["Cve_Tecnologia"].ToString();
            }
            else
            {
                DropDownTechnology.SelectedIndex = 0;
            }

            //add by coco 20110905
            DropDownList DropDownProduct = gvTecPro.Rows[RowIndex].FindControl("ddlProduct") as DropDownList;
            string strTempTech = "";
            if (!DropDownTechnology.SelectedValue.Equals(""))
            {
                strTempTech = DropDownTechnology.SelectedValue;
            }
            else
            {
                strTempTech = technologyID;
            }
            if (DropDownProduct != null)
            {
                DropDownProduct.DataSource = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ByTechnology(strTempTech, DropDownMarca.SelectedValue, idDepartment, TypeProduct);
                DropDownProduct.DataTextField = "Dx_Modelo_Producto";               
                DropDownProduct.DataValueField = "Cve_Producto";
                DropDownProduct.DataBind();
                DropDownProduct.Items.Insert(0, "");
                DropDownProduct.SelectedIndex = 0;
               
            }
           
            ((TextBox)gridViewRow.FindControl("txtGridText1")).Text = "";//unit price clear
            ((TextBox)gridViewRow.FindControl("txtSubtotal")).Text = "";//clear sub total
            ((HiddenField)gridViewRow.FindControl("hfExactGridText1")).Value = "";//unit price clear
            ((HiddenField)gridViewRow.FindControl("hfExactSubtotal")).Value = "";//clear sub total
            this.AccountTotal();
            //add by coco 20110905     
            SetupTooltip("ddlTecnolog");
            DropDownTechnology.ToolTip = DropDownTechnology.SelectedItem.Text;
            SetupTooltip("ddlProduct");
            DropDownProduct.ToolTip = DropDownProduct.SelectedItem.Text;
            SetupTooltip("ddlMarca");
            DropDownMarca.ToolTip = DropDownMarca.SelectedItem.Text;
            SetupTooltip("ddlTypeOfProduct");
            DropDownTypeOfProduct.ToolTip = DropDownTypeOfProduct.SelectedItem.Text;
        }
        //end add
        /// <summary>
        /// Refresh related controls when product selection changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                #region Local Variables
                DropDownList DropDownProduct = (DropDownList)sender;
                string Product = DropDownProduct.SelectedValue;
                GridViewRow Row = (GridViewRow)DropDownProduct.NamingContainer;
                int RowIndex = Row.RowIndex;
                DataTable dtProduct = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ByPK(Product);
                DropDownList DropDownTechnology = gvTecPro.Rows[RowIndex].FindControl("ddlTecnolog") as DropDownList;
                DropDownList DropDownMarca = gvTecPro.Rows[RowIndex].FindControl("ddlMarca") as DropDownList;
                //edit by coco 20110823
                //TextBox txtModelo = gvTecPro.Rows[RowIndex].FindControl("txtModelo") as TextBox;
                DropDownList DropDownTypeOfProduct = gvTecPro.Rows[RowIndex].FindControl("ddlTypeOfProduct") as DropDownList;
                //end edit
                HiddenField hfBasePrice = gvTecPro.Rows[RowIndex].FindControl("hfBasePrice") as HiddenField;
                TextBox txtGridText1 = gvTecPro.Rows[RowIndex].FindControl("txtGridText1") as TextBox;
                TextBox txtSubtotal = gvTecPro.Rows[RowIndex].FindControl("txtSubtotal") as TextBox;
                HiddenField hfExactGridText1 = gvTecPro.Rows[RowIndex].FindControl("hfExactGridText1") as HiddenField;
                HiddenField hfExactSubtotal = gvTecPro.Rows[RowIndex].FindControl("hfExactSubtotal") as HiddenField;
                TextBox txtCantidad = gvTecPro.Rows[RowIndex].FindControl("txtCantidad") as TextBox;
                #endregion
                //edit by coco 2011-08-10
                if (dtProduct!=null && dtProduct.Rows.Count > 0) // updated by tina 2012-02-27
                {
                    DropDownTechnology.SelectedValue = dtProduct.Rows[0]["Cve_Tecnologia"].ToString();
                    DropDownMarca.SelectedValue = dtProduct.Rows[0]["Cve_Marca"].ToString();
                    //add by coco 20110823
                    DropDownTypeOfProduct.SelectedValue = dtProduct.Rows[0]["Ft_Tipo_Producto"].ToString();
                    //end add
                }//end edit
                ////add by coco 20110905
                //setUpTooltip("ddlTecnolog");
                //DropDownTechnology.ToolTip = DropDownTechnology.SelectedItem.Text;
                //Added by coco 2011-08-05
                //Reload product capacity
                // updated by tina 2012-02-27
                DropDownList DropDownCapacidad = gvTecPro.Rows[RowIndex].FindControl("ddlCapacidad") as DropDownList;
                if (DropDownTechnology.SelectedIndex != -1 && !IsSE(DropDownTechnology.SelectedValue))
                {
                    DropDownCapacidad.DataSource = ProductCapacityDal.ClassInstance.Get_ProductCapacity(DropDownTechnology.SelectedValue);
                    DropDownCapacidad.DataTextField = "Ft_Capacidad";
                    DropDownCapacidad.DataValueField = "Cve_Producto_Capacidad";
                    DropDownCapacidad.DataBind();
                    DropDownCapacidad.Items.Insert(0, "");
                    DropDownCapacidad.SelectedIndex = 0;
                   
                }
                else if (DropDownTechnology.SelectedIndex != -1 && IsSE(DropDownTechnology.SelectedValue))
                {
                    // RSA 2012-09-11 product selection complexified
                    lDescription.Text = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ForSE_Description(DropDownProduct.SelectedValue);
                }
                // end
                //End add

                //get the unitary cost of the specific model
                //edit by tina 2012-07-25
                DataTable ProductDataTable = K_PROVEEDOR_PRODUCTOBLL.ClassInstance.Get_K_PROVEEDOR_PRODUCTO_ByPK(Product, idDepartment);
                if (ProductDataTable != null && ProductDataTable.Rows.Count > 0)
                {
                    if (Pct_Tasa_IVA == 0)
                    {
                        GetPctTasaIVA();
                    }
                    double precio = (double.Parse(ProductDataTable.Rows[0]["Mt_Precio_Unitario"].ToString())) / (1 + Pct_Tasa_IVA / 100);
                    txtGridText1.Text = TwoDecimals(precio).ToString("0.00");  // -IVA
                    hfExactGridText1.Value = precio.ToString();  // -IVA
                    hfBasePrice.Value    = precio.ToString();        // -IVA
                    //txtGridText1.ToolTip = hfExactGridText1.Value;
                    if (!txtCantidad.Text.Equals("") && !txtGridText1.Text.Equals(""))
                    {
                        if (IsNumeric(txtCantidad.Text) && IsNumeric(txtGridText1.Text))
                        {
                            double val = int.Parse(txtCantidad.Text) * double.Parse(hfExactGridText1.Value);
                            hfExactSubtotal.Value = val.ToString();
                            txtSubtotal.Text = TwoDecimals(val).ToString("0.00");
                            // txtSubtotal.ToolTip = hfExactSubtotal.Value;
                        }
                        else
                        {
                            hfExactSubtotal.Value = "0";
                            txtSubtotal.Text = "0.00";
                            // txtSubtotal.ToolTip = "0.00";
                        }
                    }
                    else
                    {
                        hfExactSubtotal.Value = "0";
                        txtSubtotal.Text = "0.00";
                        // txtSubtotal.ToolTip = "0.00";
                    }
                    //Re-calculate the total
                    AccountTotal();
                }
                else
                {
                    hfExactSubtotal.Value = "0";
                    hfExactGridText1.Value = "0";
                    txtSubtotal.Text = "0.00";
                    txtGridText1.Text = "";
                    // txtSubtotal.ToolTip = "0.00";
                    txtGridText1.ToolTip = "0.00";
                }
                //Re-calculate the total
                AccountTotal();
                //add by coco 20110905    
                SetupTooltip("ddlTypeOfProduct");
                DropDownTypeOfProduct.ToolTip = DropDownTypeOfProduct.SelectedItem.Text;
                SetupTooltip("ddlMarca");
                DropDownMarca.ToolTip = DropDownMarca.SelectedItem.Text;               
                SetupTooltip("ddlTecnolog");
                DropDownTechnology.ToolTip = DropDownTechnology.SelectedItem.Text;
                SetupTooltip("ddlProduct");
                DropDownProduct.ToolTip = DropDownProduct.SelectedItem.Text;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// Input cantidad 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCantidad = (TextBox)sender;
            string Cantidad = txtCantidad.Text;

            if (!IsNumeric(Cantidad))
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "DataFormatIncorrect") as string;
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                return;
            }

            GridViewRow Row = (GridViewRow)txtCantidad.NamingContainer;
            CalculateRowSubTotal(Row);

            // RSA 20130909 if a value already exists for Gastos, validate it too
            TextBox txtGastos = Row.FindControl("txtGastos") as TextBox;
            if (!string.IsNullOrEmpty(txtGastos.Text))
                txtGastos_TextChanged(txtGastos, null);
        }
        /// <summary>
        /// Input Precio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtGridText1_TextChanged(object sender, EventArgs e)
        {
            TextBox txtGridText1 = (TextBox)sender;

            GridViewRow Row = (GridViewRow)txtGridText1.NamingContainer;
            HiddenField hfExactGridText1 = Row.FindControl("hfExactGridText1") as HiddenField;
            HiddenField hfBasePrice = Row.FindControl("hfBasePrice") as HiddenField;

            double amount = 0;
            if (!double.TryParse(txtGridText1.Text, System.Globalization.NumberStyles.Currency, System.Threading.Thread.CurrentThread.CurrentCulture, out amount)
                || !IsValidPrecio(hfBasePrice.Value, amount.ToString()))
            {
                double precio = 0;
                double.TryParse(hfBasePrice.Value, out precio);

                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "DataPrecioIncorrect") as string;
                strMsg = string.Format(strMsg, (Math.Floor(precio * 100) / 100).ToString("n"));
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                hfExactGridText1.Value = string.Empty;
                txtGridText1.Text = string.Empty;
                //txtGridText1.ToolTip = hfExactGridText1.Value;
                return;
            }

            hfExactGridText1.Value = amount.ToString();
            //txtGridText1.ToolTip = hfExactGridText1.Value;
            CalculateRowSubTotal(Row);

            // RSA 20130909 if a value already exists for Gastos, validate it too
            TextBox txtGastos = Row.FindControl("txtGastos") as TextBox;
            if (!string.IsNullOrEmpty(txtGastos.Text))
                txtGastos_TextChanged(txtGastos, null);
        }
        /// <summary>
        /// Input gastos 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtGastos_TextChanged(object sender, EventArgs e)
        {
            TextBox txtGastos = (TextBox)sender;
            string Gastos = txtGastos.Text;

            GridViewRow Row = (GridViewRow)txtGastos.NamingContainer;
            HiddenField hfExactSubtotal = Row.FindControl("hfExactSubtotal") as HiddenField;
            string Subtotal = hfExactSubtotal.Value;

            if (!IsValidGastos(Subtotal, Gastos))
            {
                double dSubtotal = 0;
                double.TryParse(Subtotal, out dSubtotal);

                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "DataGastosIncorrect") as string;
                strMsg = string.Format(strMsg, (Math.Floor(gastosPercentage * dSubtotal * 100) / 100).ToString("n"));
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                txtGastos.Text = string.Empty;
                return;
            }

            AccountTotal();
        }


        private void CalculateRowSubTotal(GridViewRow Row)
        {
            TextBox txtCantidad = gvTecPro.Rows[Row.RowIndex].FindControl("txtCantidad") as TextBox;
            TextBox txtSubtotal = gvTecPro.Rows[Row.RowIndex].FindControl("txtSubtotal") as TextBox;
            HiddenField hfExactSubtotal = gvTecPro.Rows[Row.RowIndex].FindControl("hfExactSubtotal") as HiddenField;
            HiddenField hfExactGridText1 = gvTecPro.Rows[Row.RowIndex].FindControl("hfExactGridText1") as HiddenField;

            // RSA Validate inputs and set sub total
            if (!string.IsNullOrEmpty(txtCantidad.Text) 
                    && IsNumeric(txtCantidad.Text)
                    && !string.IsNullOrEmpty(hfExactGridText1.Value) && IsNumeric(hfExactGridText1.Value))
            {
                double importe = int.Parse(txtCantidad.Text) * TwoDecimals(double.Parse(hfExactGridText1.Value));
                hfExactSubtotal.Value = importe.ToString();
                txtSubtotal.Text = TwoDecimals(importe).ToString("0.00");
                // txtSubtotal.ToolTip = hfExactSubtotal.Value;
            }
            else
            {
                hfExactSubtotal.Value = "0";
                txtSubtotal.Text = "0.00";
                // txtSubtotal.ToolTip = "0.00";
            }
            //Re-calculate the total
            AccountTotal();
        }

        //comment by tina 2012-07-25
        //protected void txtGridText_TextChanged( object sender, EventArgs e )
        //{
        //    TextBox txtProductUnit = (TextBox)sender;
        //    string ProductUnit = txtProductUnit.Text;

        //    if ( !IsNumeric(ProductUnit) )
        //    {
        //        string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "DataFormatIncorrect1") as string;
        //        ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
        //        return;
        //    }
        //    else
        //    {
        //        GridViewRow Row = (GridViewRow)txtProductUnit.NamingContainer;
        //        DropDownList drpProduct = gvTecPro.Rows[Row.RowIndex].FindControl("ddlProduct") as DropDownList;
        //        K_PROVEEDOR_PRODUCTOEntity Provider_ProductEntity = new K_PROVEEDOR_PRODUCTOEntity();
        //        Provider_ProductEntity.Id_Proveedor = int.Parse(idDepartment);
        //        if ( !drpProduct.SelectedValue.ToString().Equals("") )
        //        {
        //            Provider_ProductEntity.Cve_Producto = int.Parse(drpProduct.SelectedValue);
        //        }
        //        else
        //        {
        //            Provider_ProductEntity.Cve_Producto = 0;
        //        }
        //        if ( !ProductUnit.Equals("") )
        //        {
        //            Provider_ProductEntity.Mt_Precio_Unitario = decimal.Parse(ProductUnit);
        //        }
        //        else
        //        {
        //            Provider_ProductEntity.Mt_Precio_Unitario = 0;
        //        }
        //        Provider_ProductEntity.Dt_Fecha_Prov_Prod = DateTime.Now.Date;
        //        try
        //        {
        //            int Result = K_PROVEEDOR_PRODUCTODal.ClassInstance.Update_K_PROVEEDOR_PRODUCTO(Provider_ProductEntity);
        //            TextBox txtSubtotal = gvTecPro.Rows[Row.RowIndex].FindControl("txtSubtotal") as TextBox;
        //            TextBox txtCantidad = gvTecPro.Rows[Row.RowIndex].FindControl("txtCantidad") as TextBox;
        //            DropDownList DropDownCapacidad = gvTecPro.Rows[Row.RowIndex].FindControl("ddlCapacidad") as DropDownList;

        //            if ( !string.IsNullOrEmpty(txtCantidad.Text) && !string.IsNullOrEmpty(ProductUnit) )
        //            {
        //                if ( IsNumeric(ProductUnit) )
        //                {
        //                    txtSubtotal.Text = (int.Parse(txtCantidad.Text) * decimal.Parse(ProductUnit)).ToString();
        //                }
        //            }
        //            else
        //            {
        //                txtSubtotal.Text = "0";
        //            }
        //            //Re-calculate the total
        //            AccountTotal();
        //        }
        //        catch ( Exception ex )
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + ex.Message + "');", true);
        //        }
        //    }
        //}
        //end
        #endregion

        #region Button Actions
        /// <summary>
        /// Refresh related controls when navigation next button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wizardPages_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                Page.Validate("Siguiente");//force to validate the page controls

                if (!Page.IsValid)
                {
                    e.Cancel = true;
                    return;
                }

                if (e.CurrentStepIndex == 0)//First step
                {
                    string StatusFlag="";
                    string ErrorCode;
                    // RSA Enable edition, if not editing a credit then check if one already exists
                    if (!IsEditingCredit() && K_CREDITODal.ClassInstance.IsServiceCodeExist(txtNO_RPU.Text))
                    {
                        string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "TheServiceCodehaveUsed") as string;
                        ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        if (!ValidateServiceCode(this.txtNO_RPU.Text, out ErrorCode, out StatusFlag))
                        {/*
                            CreditEntity creditModel = GetDataFromStepOne();
                            CAT_AUXILIAREntity AuxiliarEntity = GetCatAuxiliarData();

                            creditModel.Dt_Fecha_Ultmod = DateTime.Now.Date;
                            creditModel.Dx_Usr_Ultmod = UserID;
                            creditModel.Tipo_Usuario = TipoUser;

                            if (StatusFlag == "B")//Beneficiario con adeudos status
                            {
                                creditModel.Cve_Estatus_Credito = (int)CreditStatus.BENEFICIARIO_CON_ADEUDOS;
                                creditModel.Dt_Fecha_Beneficiario_con_adeudos = DateTime.Now.Date;
                            }
                            else//Tarifa fuera de programa status
                            {
                                creditModel.Cve_Estatus_Credito = (int)CreditStatus.TARIFA_FUERA_DE_PROGRAMA;
                                creditModel.Dt_Fecha_Tarifa_fuera_de_programa = DateTime.Now.Date;
                            }
                          */
                            //Added by Jerry 2011/08/12
                            //creditModel.No_Credito = "PAEEEM" + this.RGN_CFE.Substring(2, 5) + /*this.ZoneType + "-" + "1"TBD +*/ string.Format("{0:00000}", Convert.ToInt32(LsUtility.GetNumberSequence("CREDITO")));

                            //int iResult = K_CREDITOBll.ClassInstance.Insert_K_CreditPage1Data(creditModel, AuxiliarEntity);

                            //if (iResult > 0)
                            {
                                e.Cancel = true;
                                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + ErrorCode.Replace("\r", "\\r").Replace("\n", "\\n") + "');", true);
                                //Response.Redirect("CreditMonitor.aspx");
                            }
                        }
                        // RSA 20131009 validate person's RFC and CURP, and aval's RFC and CURP INI
                        else if (!ValidateRFC("Selector", txtName.Text, txtLastname.Text, txtMotherName.Text, txtBirthDate.Text, txtDX_RFC.Text.ToUpper()))
                        {
                            string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateRFCPersona") as string;
                            ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                            e.Cancel = true;
                        }
                        else if (!ValidateCURP(txtDX_RFC.Text.ToUpper(), txtDX_CURP.Text.ToUpper()))
                        {
                            string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateCURPPersona") as string;
                            ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                            e.Cancel = true;
                        }
                        // now validate aval, it is always a "Persona Fisica"
                        else if (!ValidateRFC("Aval", txtDx_First_Name_aval.Text, txtDx_Last_Name_aval.Text, txtDx_Mother_Name_aval.Text
                                    , txtDt_BirthDate_Aval.Text, txtDx_RFC_Aval.Text.ToUpper()))
                        {
                            string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateRFCAval") as string;
                            ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                            e.Cancel = true;
                        }
                        else if (!ValidateCURP(txtDx_RFC_Aval.Text.ToUpper(), txtDx_CURP_Aval.Text.ToUpper()))
                        {
                            string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateCURPAval") as string;
                            ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                            e.Cancel = true;
                        }
                        // RSA 20131028 Validate Aval age too
                        else if (!ValidateAge(txtDt_BirthDate_Aval.Text))
                        {
                            string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateAgeAval") as string;
                            ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                            e.Cancel = true;
                            return;
                        }
                        // FIN
                        else
                        {
                            if (CompanyType.PERSONAFISICA == (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue) || CompanyType.REPECO == (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue))
                            {
                                if (!ValidateAge(txtBirthDate.Text))
                                {
                                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateAge") as string;
                                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                                    e.Cancel = true;
                                    return;
                                }
                            }

                            if ((CompanyType.PERSONAFISICA == (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue) || CompanyType.REPECO == (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue)) && radFG_EDO_CIVIL_REPRE_LEGAL2.Checked == true && string.Compare(ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedItem.Text, "BIENES MANCOMUNADOS", true) == 0)
                            // if ((string.Compare(ddlDX_TIPO_SOCIEDAD.SelectedItem.Text, "PERSONA FISICA", true) == 0 || string.Compare(ddlDX_TIPO_SOCIEDAD.SelectedItem.Text, "REPECO", true) == 0 ) && radFG_EDO_CIVIL_REPRE_LEGAL2.Checked == true && string.Compare(ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedItem.Text, "BIENES MANCOMUNADOS", true) == 0)
                            // if (string.Compare(ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedItem.Text, "BIENES MANCOMUNADOS", true) == 0)
                            {
                                wizardPages.ActiveStepIndex = 1;
                            }
                            else
                            {
                                wizardPages.ActiveStepIndex = 2;
                                SetupTooltip("ddlTecnolog");
                                SetupTooltip("ddlTypeOfProduct");
                                SetupTooltip("ddlMarca");
                                SetupTooltip("ddlProduct");
                                txtDx_razon_social3.Text = txtName.Text + " " + txtLastname.Text + " " + txtMotherName.Text;
                                txtDx_tipo_industria3.Text = ddlDX_TIPO_INDUSTRIA.SelectedItem.Text.ToString();
                             }
                        }
                    }
                }
                if (e.CurrentStepIndex == 1)//Second step
                {
                    wizardPages.ActiveStepIndex = 2;
                    txtDx_razon_social3.Text = txtName.Text + " " + txtLastname.Text + " " + txtMotherName.Text;
                    txtDx_tipo_industria3.Text = ddlDX_TIPO_INDUSTRIA.SelectedItem.Text;
                    SetupTooltip("ddlTecnolog");
                    SetupTooltip("ddlTypeOfProduct");
                    SetupTooltip("ddlMarca");
                    SetupTooltip("ddlProduct");
                }
                if (e.CurrentStepIndex == 2)
                {
                    string strMsg = string.Empty;

                    //Added by coco 20110905
                    if (txtTotal.Text.Equals("") || txtTotal.Text.Equals("0") || !IsAllProductDataEntered(out strMsg))//updated by tina 2012-07-26
                    {
                        ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                        e.Cancel = true;

                        SetupTooltip("ddlTecnolog");
                        SetupTooltip("ddlTypeOfProduct");
                        SetupTooltip("ddlMarca");
                        SetupTooltip("ddlProduct");

                        return;
                    }                   
                    //end add
                    else if (!IsValidRFCAmountLimit(txtDX_RFC.Text, double.Parse(txtTotal.Text, System.Globalization.NumberStyles.Currency, System.Threading.Thread.CurrentThread.CurrentCulture)))
                    {
                        strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "QuoteGreaterThanRFCLimit") as string;
                        ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                        e.Cancel = true;

                        return;
                    }
                    InitStepThreeData();
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + ex.Message.Replace('\'', '"') + "');", true);
                new LsApplicationException(this, "Credit Request", ex, true);
            }
        }
        // RSA 20131011 Calculate age taking into account the actual dates
        // first substract the years, if your birthday was today or earlier then that´s your age
        // if your brithday will be later this year, then decrease
        // RSA 20131028 Validate age too, receive the birth date as a string in format yyyy-mm-dd, previously validated
        private Boolean ValidateAge(string birth)
        {
            bool message = false;

            DateTime today = DateTime.Today;
            // start by comparing the years
            int age = today.Year - int.Parse(birth.Substring(0, 4));

            // decrease age if this year's birthday is still to come ("bigger" than today)
            if (birth.Substring(5).CompareTo(today.ToString("MM-dd")) > 0)
                age--;

            // RSA 20131022, strictly less than 66 (65 and months is ok, 66 is not)
            if (age >= 18 && age < 66)
            {
                message = true;
            }
            return message;
        }
        /// <summary>
        /// Validate the RFC provided against the reference one generated from the name and birthday of the person
        /// All the inputs must have been validated earlier (valid birth date, rfc size and case)
        /// The validation process depends on the type of validation selected
        ///     Moral will use the first 2 chars of the provided rfc homoclave as input to generate the reference one
        ///     Fisica will generete the full reference rfc
        ///     Aval will generate the full reference rfc but validates only the first 10 chars
        ///     Selector will use the selection of Tipo Sociedad to aply either type Moral or Fisica (Repeco is treated as Fisica)
        /// </summary>
        /// <param name="type">Type of validation: Moral, Fisica, Aval or Selector</param>
        /// <param name="name">Name of person, moral or otherwise (fisica, repeco)</param>
        /// <param name="last">Last name of the person, fisica or repeco only</param>
        /// <param name="mother">Mother name of the person, fisica or repeco only</param>
        /// <param name="birth">Date of birth of the persona, moral or otherwise</param>
        /// <param name="rfc">Provided rfc to validate</param>
        /// <returns>true if the provided rfc matches the generated reference one</returns>
        private Boolean ValidateRFC(string type, string name, string last, string mother, string birth, string rfc)
        {
            Boolean result = false;
            string RFCdeGenerador;

            // if type equals "Selector" then redefine it based on selection of Tipo Sociedad
            if (type.ToUpper().Equals("SELECTOR"))
            {
                type = (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue) == CompanyType.MORAL
                    ? "Moral"
                    : "Fisica";
            }

            switch (type.ToUpper())
            {
                case "MORAL":
                    // the process generates the first part of the RFC from the name and birth date, it will
                    // copy 2 chars from the provided rfc's homoclave, and recalculate final digito validador
                    RFCdeGenerador = K_CREDITODal.ClassInstance.GenerateRFCMoral(name, birth, rfc);
                    result = rfc.Equals(RFCdeGenerador);
                    break;
                case "FISICA":  // CompanyType.PERSONAFISICA & CompanyType.REPECO
                    RFCdeGenerador = K_CREDITODal.ClassInstance.GenerateRFCInmoral(name, last, mother, birth);
                    result = rfc.Equals(RFCdeGenerador);
                    break;
                case "AVAL":
                    RFCdeGenerador = K_CREDITODal.ClassInstance.GenerateRFCInmoral(name, last, mother, birth);
                    // For Aval validate only that the first 10 characters match
                    result = rfc.StartsWith(RFCdeGenerador.Substring(0, 10));
                    break;
            }

            return result;
        }
        /// <summary>
        /// Validate the CURP against the RFC
        /// All the inputs must have been validated earlier (rfc size, rfc and curp case)
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateCURP(string rfc, string curp)
        {
            Boolean result = false;

            CompanyType persona = (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue);

            // if it is persona Moral then there is no CURP return true
            // or if it is not Moral, then validate that curp starts with the first part of rfc (no homoclave)
            if (persona == CompanyType.MORAL || curp.StartsWith(rfc.Substring(0, rfc.Length - 3)))
            {
                result = true;
            }

            return result;
        }
        private Boolean IsValidRFCAmountLimit(string RFC, double monto)
        {
            bool result = false;

            if (ProgramDt.Rows.Count > 0 && ProgramDt.Rows[0]["Mt_Monto_Maximo"] != null)
            {
                double limit = Convert.ToDouble(ProgramDt.Rows[0]["Mt_Monto_Maximo"]);
                double current = K_CREDITODal.ClassInstance.TotalAmountByRFC(RFC);
                // RSA 20131029 set factorSolicitadoTotal = 1, the limit is exactelly 350
                double factorSolicitadoTotal = 1; // 350.0/349.0;
                double previous = 0;

                // RSA 20131023 read the original amount of the credit that we are replacing (if editing)
                if (IsEditingCredit())
                    double.TryParse(hfTotal.Value, out previous);

                // RSA 20131003 logic changed to use Mt_Monto_Solicitado (that doesn't include interest)
                // and apply factorSolicitadoTotal to account for small variations that could happen
                if (((current + monto - previous) * factorSolicitadoTotal) <= limit)
                    result = true;
                else
                    result = false;
            }

            return result;
        }
        /// <summary>
        /// Gets all the credit's properties related to the person
        /// Doesn't include product information
        /// </summary>
        /// <param name="creditNumber"></param>
        /// <returns>Datatable</returns>
        private DataTable GetAllCreditProperties(string creditNumber)
        {
            DataTable result = null;

            // validate that the credit exists, its status is Pendiente, and the auxiliar informartion exists            
            // and has no mop yet, only then continue with the edition, otherwise return to the monitor
            DataTable dtCredit = K_CREDITODal.ClassInstance.GetCreditsReview(CreditNumber);
            if (dtCredit != null && dtCredit.Rows.Count > 0)
            {
                result = dtCredit;
                result.Columns.Add("Dx_Nombres");
                result.Columns.Add("Dx_Apellido_Paterno");
                result.Columns.Add("Dx_Apellido_Materno");
                result.Columns.Add("Dt_Nacimiento_Fecha");
                result.Columns.Add("No_MOP");

                DataTable dtAuxiliar = CAT_AUXILIARDal.ClassInstance.Get_CAT_AUXILIARByCreditNo(CreditNumber.ToString());
                if (dtAuxiliar != null && dtAuxiliar.Rows.Count > 0)
                {
                    result.Rows[0]["Dx_Nombres"] = dtAuxiliar.Rows[0]["Dx_Nombres"];
                    result.Rows[0]["Dx_Apellido_Paterno"] = dtAuxiliar.Rows[0]["Dx_Apellido_Paterno"];
                    result.Rows[0]["Dx_Apellido_Materno"] = dtAuxiliar.Rows[0]["Dx_Apellido_Materno"];
                    result.Rows[0]["Dt_Nacimiento_Fecha"] = dtAuxiliar.Rows[0]["Dt_Nacimiento_Fecha"];
                    result.Rows[0]["No_MOP"] = dtAuxiliar.Rows[0]["No_MOP"];
                }
            }

            return result;
        }
        /// <summary>
        /// Validate if the credit is editable
        /// </summary>
        /// <param name="dtCredit"></param>
        /// <returns></returns>
        private bool IsEditionAllowed(DataTable dtAllCreditData)
        {
            bool result = false;

            if (dtAllCreditData != null
                && CreditStatus.PENDIENTE == (CreditStatus)Enum.Parse(typeof(CreditStatus), dtAllCreditData.Rows[0]["Cve_Estatus_Credito"].ToString())
                && string.IsNullOrEmpty(dtAllCreditData.Rows[0]["No_MOP"].ToString()))
            {
                result = true;
            }

            return result;
        }
        /// <summary>
        /// save Credit request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wizardPages_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                // RSA 20130815 Enable editing, if editing a credit but not valid anymore send an error
                if (IsEditingCredit())
                {
                    DataTable dtAllCredit = GetAllCreditProperties(CreditNumber);
                    if (!IsEditionAllowed(dtAllCredit))
                    {
                        string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "CreditEditionInvalid") as string;
                        ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                        return;
                    }
                }

                // RSA 20130903 TODO test that the amounts are properly calculated based on the products listed
                AccountTotal();

                CreditEntity creditModel = null;

                #region Credit Request
                creditModel = GetDataFromStepOne();

                //Step Two Data
                creditModel.Dx_Nombre_Coacreditado = txtDx_nombre_coacreditado.Text;
                creditModel.Dx_RFC_CURP_Coacreditado = txtDx_RFC_CURP_Coacreditado.Text;
                creditModel.Dx_Telefono_Coacreditado = txtDx_telefono_coacreditado.Text;
                creditModel.Dx_Domicilio_Coacreditado_Colonia = txtDx_Domicilio_Coacreditado_Colonia.Text;

                if (RadioButton11.Checked)
                {
                    creditModel.Fg_Sexo_Coacreditado = 1;
                }
                else if (RadioButton12.Checked)
                {
                    creditModel.Fg_Sexo_Coacreditado = 2;
                }

                creditModel.Dx_Domicilio_Coacreditado_Calle = txtDx_domicilio_coacreditado_calle.Text;
                creditModel.Dx_Domicilio_Coacreditado_Num = txtDx_domicilio_coacreditado_num.Text;
                creditModel.Dx_Domicilio_Coacreditado_CP = txtDx_domicilio_coacreditado_cp.Text;
                if (ddlDx_nombre_estado2.SelectedIndex != -1 && ddlDx_nombre_estado2.SelectedIndex != 0)
                {
                    creditModel.Cve_Estado_Coacreditado = Convert.ToInt32(ddlDx_nombre_estado2.SelectedValue);
                }

                if (ddlDx_deleg_municipio2.SelectedIndex != -1 && ddlDx_deleg_municipio2.SelectedIndex != 0)
                {
                    creditModel.Cve_Deleg_Municipio_Coacreditado = Convert.ToInt32(ddlDx_deleg_municipio2.SelectedValue);
                }

                //Step Four Data
                creditModel.Mt_Monto_Solicitado = Session["monto_solicitado"] != null ? Convert.ToDouble(Session["monto_solicitado"]) : default(double);//txtMt_monto_solicitado.Text;
                if (!txtNo_Plazo_Pago.Text.Equals(""))
                {
                    creditModel.No_Plazo_Pago = Convert.ToInt32(txtNo_Plazo_Pago.Text);
                }

                if (!ddlDx_periodo_pago.SelectedValue.Equals(""))
                {
                    creditModel.Cve_Periodo_Pago = Convert.ToInt32(ddlDx_periodo_pago.SelectedValue.ToString());
                }

                //CAT_PROGRAMA
                DataTable ProgramDt = CAT_PROGRAMABLL.ClassInstance.Get_CAT_PROGRAMAbyPk(Global.PROGRAM.ToString());//Change by Jerry 2011/08/08
                if (ProgramDt != null && ProgramDt.Rows.Count > 0)
                {
                    creditModel.Pct_Tasa_Interes = float.Parse(ProgramDt.Rows[0]["Pct_Tasa_Interes"].ToString());
                    creditModel.Pct_Tasa_Fija = float.Parse(ProgramDt.Rows[0]["Pct_Tasa_Fija"].ToString());

                    if (creditModel.Cve_Periodo_Pago == 1)
                    {
                        creditModel.Pct_CAT = float.Parse(ProgramDt.Rows[0]["Pct_CAT_Factura_Mensual"].ToString());
                    }
                    else if (creditModel.Cve_Periodo_Pago == 2)
                    {
                        creditModel.Pct_CAT = float.Parse(ProgramDt.Rows[0]["Pct_CAT_Factura_Bimestral"].ToString());
                    }
                    //updated by tina 2012-07-26
                    if (Pct_Tasa_IVA == 0)
                    {
                        GetPctTasaIVA();
                    }
                    creditModel.Pct_Tasa_IVA = Pct_Tasa_IVA;
                    //end
                    //updated by tina 2012-07-25
                    if (TipoUser == GlobalVar.SUPPLIER)
                    {
                        CAT_PROVEEDORModel ProModel = new CAT_PROVEEDORModel();
                        ProModel = CAT_PROVEEDORBll.ClassInstance.Get_CAT_PROVEEDORByPKID(idDepartment);

                        creditModel.Pct_Tasa_IVA = float.Parse(ProModel.Pct_Tasa_IVA.ToString());
                    }
                    else if (TipoUser == GlobalVar.SUPPLIER_BRANCH)
                    {
                        if (Session["UserInfo"] != null)
                        {
                            US_USUARIOModel User = (US_USUARIOModel)Session["UserInfo"];

                            DataTable dtSupplierBranch = SupplierBrancheDal.ClassInstance.GetSupplierBranch(User.Id_Departamento);
                            if (dtSupplierBranch != null && dtSupplierBranch.Rows.Count > 0)
                            {
                                creditModel.Pct_Tasa_IVA = float.Parse(dtSupplierBranch.Rows[0]["Pct_Tasa_IVA"].ToString());
                            }
                        }
                    }
                    //end

                    creditModel.Pct_Tasa_IVA_Intereses = string.IsNullOrEmpty(ProgramDt.Rows[0]["Pct_Tasa_IVA_Intereses"].ToString()) ? 0 : float.Parse(ProgramDt.Rows[0]["Pct_Tasa_IVA_Intereses"].ToString());

                }



                //add by coco 2012-07-16
                creditModel.Telephone = txtTelephone.Text;
                creditModel.Email = txtEmail.Text;
                //end add

                #endregion

                #region Step Three Data

                double ProductCapacity = 0;//Added by coco 2011-08-04
                List<K_CREDITO_PRODUCTOEntity> creProduct = new List<K_CREDITO_PRODUCTOEntity>();

                creditModel.No_Ahorro_Economico = 0;
                creditModel.No_Ahorro_Energetico = 0;
                double ahorro_energetico;
                bool esSE = false;
                double originalConsumption = 0;

                // RSA 20130826 Read gastos value
                decimal gastosTotal = 0;

                for (int i = 0; i < gvTecPro.Rows.Count; i++)
                {
                    DropDownList DropDownTechnology = gvTecPro.Rows[i].FindControl("ddlTecnolog") as DropDownList;
                    DropDownList DropDownProduct = gvTecPro.Rows[i].FindControl("ddlProduct") as DropDownList;
                    DropDownList DropDownMarca = gvTecPro.Rows[i].FindControl("ddlMarca") as DropDownList;
                    TextBox txtModelo = gvTecPro.Rows[i].FindControl("txtModelo") as TextBox;
                    TextBox txtCantidad = gvTecPro.Rows[i].FindControl("txtCantidad") as TextBox;
                    HiddenField hfExactGridText1 = gvTecPro.Rows[i].FindControl("hfExactGridText1") as HiddenField;
                    HiddenField hfExactSubtotal = gvTecPro.Rows[i].FindControl("hfExactSubtotal") as HiddenField;
                    TextBox txtGastos = gvTecPro.Rows[i].FindControl("txtGastos") as TextBox;                    
                    DropDownList ddlCapacidad = gvTecPro.Rows[i].FindControl("ddlCapacidad") as DropDownList;//Added by coco 2011-08-04

                    esSE = IsSE(DropDownTechnology.SelectedValue);
                    if (DropDownProduct.SelectedIndex != 0 && DropDownProduct.SelectedIndex != -1)
                    {
                        K_CREDITO_PRODUCTOEntity model = new K_CREDITO_PRODUCTOEntity();
                        //Edit by coco 2011-08-04   
                        DataTable dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIATipoByPK(DropDownTechnology.SelectedValue);
                        if (dtTechnology != null)
                        {
                            // RSA 20130312 Read monthly value from table (only used if !03 &&!SE) no distinction for Iluminación
                            //if (dtTechnology.Rows[0]["Dx_Nombre"].ToString().Trim().StartsWith("Iluminacion"))// separately Iluminación device energySave
                            //{
                                DataTable dtProduct = CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByPK(DropDownProduct.SelectedValue);
                            //    double tempCapacidad = ddlCapacidad.SelectedItem.Text.ToString().Equals("") ? 0 : double.Parse(ddlCapacidad.SelectedItem.Text);

                                if (dtProduct != null)
                                {
                            //        double TempEficienciaEnergia = dtProduct.Rows[0]["No_Eficiencia_Energia"] == DBNull.Value ? 0 : double.Parse(dtProduct.Rows[0]["No_Eficiencia_Energia"].ToString());
                            //        ProductCapacity = ProductCapacity + (tempCapacidad - (dtProduct.Rows[0]["No_Capacidad"].ToString() == "" ? 0 : double.Parse(dtProduct.Rows[0]["No_Capacidad"].ToString()))) * TempEficienciaEnergia * Convert.ToInt32(txtCantidad.Text);
                                    ProductCapacity += double.Parse(dtProduct.Rows[0]["Ahorro_Consumo"].ToString()) * Convert.ToInt32(txtCantidad.Text);
                                }
                            //}
                            //else
                            //{
                            //    double TempProductCapacity = K_CREDITODal.ClassInstance.CalculateTotalEnergyConsumptionSavings(DropDownProduct.SelectedValue);
                            //    ProductCapacity = ProductCapacity + TempProductCapacity * Convert.ToInt32(txtCantidad.Text);
                            //}
                            model.Cve_Producto_Capacidad = ddlCapacidad.SelectedValue.Equals("") ? 0 : int.Parse(ddlCapacidad.SelectedValue);
                        }
                        //end edit
                        model.Cve_Producto = string.IsNullOrEmpty(DropDownProduct.SelectedValue) ? default(Int32) : int.Parse(DropDownProduct.SelectedValue);
                        model.No_Cantidad = string.IsNullOrEmpty(txtCantidad.Text) ? 0 : int.Parse(txtCantidad.Text);
                        model.Mt_Precio_Unitario = string.IsNullOrEmpty(hfExactGridText1.Value) ? default(decimal)
                            : (decimal)TwoDecimals(double.Parse(hfExactGridText1.Value) * (1 + Pct_Tasa_IVA / 100));           // +IVA
                        model.Mt_Gastos_Instalacion_Mano_Obra = string.IsNullOrEmpty(txtGastos.Text) ? default(decimal)
                            : (decimal)TwoDecimals(double.Parse(txtGastos.Text) * (1 + Pct_Tasa_IVA / 100));    // +IVA

                        gastosTotal += model.Mt_Gastos_Instalacion_Mano_Obra;

                        //without tax
                        //edit by tina 2012-07-25
                        // DataTable ProVProDdt = K_PROVEEDOR_PRODUCTOBLL.ClassInstance.Get_K_PROVEEDOR_PRODUCTO_ByPK(model.Cve_Producto.ToString(), idDepartment);
                        // if (ProVProDdt != null && ProVProDdt.Rows.Count > 0)
                        // {
                        //     decimal deUnit = decimal.Parse(ProVProDdt.Rows[0]["Mt_Precio_Unitario"].ToString());
                        //     decimal deUnit1 = decimal.Parse(creditModel.Pct_Tasa_IVA.ToString());
                        //     model.Mt_Precio_Unitario_Sin_IVA = deUnit / (1 + deUnit1 / 100);
                        // }
                        model.Mt_Precio_Unitario_Sin_IVA = (decimal)TwoDecimals(double.Parse(hfExactGridText1.Value));
                        model.Mt_Total = string.IsNullOrEmpty(hfExactSubtotal.Value) ? default(decimal)
                            : (decimal)TwoDecimals(double.Parse(hfExactSubtotal.Value) * (1 + Pct_Tasa_IVA / 100));   // +IVA
                        model.Dt_Fecha_Credito_Producto = DateTime.Now.Date;

                        // RSA 20130815 Enable edition, assign the credit number to the model, if there is one
                        if (IsEditingCredit())
                            model.No_Credito = CreditNumber;

                        creProduct.Add(model);

                        if (esSE)
                        {
                            // RSA 2012-10-10 A petición de Clarita: IVA fijo en 16% para regla del 20% adicional
                            // RSA 2013-03-12 Se regresa a IVA del distribuidor
                            creditModel.No_Ahorro_Economico += K_CREDITOBll.ClassInstance.CalculateAhorroEconomicoSE(Dics["Rate"], ddlCapacidad.SelectedItem.Text, double.Parse(Dics["HighAvgConsumption"]), Convert.ToString(model.Cve_Producto), creditModel.Pct_Tasa_IVA / 100, out ahorro_energetico, ref originalConsumption);
                        }
                        else if (Dics["Rate"] == "03")
                        {
                            if (creditModel.Cve_Periodo_Pago != 2)
                            {
                                creditModel.No_Ahorro_Economico += K_CREDITOBll.ClassInstance.CalculateEconomicConsumptionSavings3(creditModel.Cve_Estado_Neg, double.Parse(Dics["HighAvgConsumption"]), /*creditModel.Pct_Tasa_IVA / 100*/ (10 + double.Parse(Dics["ClaveIva"])) / 100, Convert.ToDouble(Dics["DEMANDAR_AVG"]), Convert.ToString(model.Cve_Producto), 1, out ahorro_energetico, ref originalConsumption);
                            }
                            else
                            {
                                creditModel.No_Ahorro_Economico += K_CREDITOBll.ClassInstance.CalculateEconomicConsumptionSavings3(creditModel.Cve_Estado_Neg, double.Parse(Dics["HighAvgConsumption"]), /*creditModel.Pct_Tasa_IVA / 100*/ (10 + double.Parse(Dics["ClaveIva"])) / 100, Convert.ToDouble(Dics["DEMANDAR_AVG"]), Convert.ToString(model.Cve_Producto), 2, out ahorro_energetico, ref originalConsumption);
                            }
                        }
                        else
                            ahorro_energetico = 0;
                        // else no es SE y no es "03" calculated later

                        creditModel.No_Ahorro_Energetico += ahorro_energetico;
                    }
                }
                #endregion

                #region"H_SCHEDULE_JOBS"
                ScheduleJobEntity ScheduleEntity = new ScheduleJobEntity();

                ScheduleEntity.Create_Date = DateTime.Now.Date;
                ScheduleEntity.Email_Body = HttpContext.GetGlobalResourceObject("DefaultResource", "Email25DaysPendingBody") as string;
                ScheduleEntity.Email_Title = HttpContext.GetGlobalResourceObject("DefaultResource", "Email25DaysPendingTitle") as string;
                ScheduleEntity.Job_Status = GlobalVar.WAITING_FOR_PROCESS;
                ScheduleEntity.Supplier_Name = UserName;
                ScheduleEntity.Supplier_Email = UserEmail;
                //ScheduleEntity.Warning_Date = DateTime.Now.Date;
                //ScheduleEntity.Canceled_Date = DBNull.Value;
                #endregion

                #region"CAT_AUXILIAR"
                CAT_AUXILIAREntity Cat_Auxiliar = GetCatAuxiliarData();
                #endregion

                creditModel.Mt_Gastos_Instalacion_Mano_Obra = (double)gastosTotal;
                creditModel.Dt_Fecha_Pendiente = DateTime.Now.Date;
                creditModel.Dt_Fecha_Ultmod = DateTime.Now.Date;
                creditModel.Dx_Usr_Ultmod = UserID;
                creditModel.Tipo_Usuario = TipoUser;

                #region Calculation Fields
                //No_Ahorro_Energetico
                
                double dbEffciencyProductSelect = ProductCapacity;//Changed by coco 2011-08-04

                if (Dics["HighAvgConsumption"] != "")
                {
                    if (Dics["Rate"] != "03" && !esSE)  // 03calculation at the same time of the ahorroEconomico
                    {
                        //if (creditModel.Cve_Periodo_Pago != 2)

                        //    // creditModel.No_Ahorro_Energetico = Convert.ToDouble(System.Math.Abs((double.Parse(Dics["HighAvgConsumption"]) - dbEffciencyProductSelect / 12)));
                        //    creditModel.No_Ahorro_Energetico = System.Math.Max(0, Convert.ToDouble(((double.Parse(Dics["HighAvgConsumption"]) - dbEffciencyProductSelect / 12))));


                        //else
                        //    // creditModel.No_Ahorro_Energetico = Convert.ToDouble(System.Math.Abs((double.Parse(Dics["HighAvgConsumption"]) - dbEffciencyProductSelect / 6)));
                        //    creditModel.No_Ahorro_Energetico = System.Math.Max(0, Convert.ToDouble(((double.Parse(Dics["HighAvgConsumption"]) - dbEffciencyProductSelect / 6))));
                        // RSA 20130312 Compare monthly values
                        // RSA 20130506 If negative do NOT set it to 0, instead it'll be rejected later, in the validation section
                        creditModel.No_Ahorro_Energetico = double.Parse(Dics["HighAvgConsumption"]) - dbEffciencyProductSelect;
                    }
                }
                if (Dics["HighAvgConsumption"] != "")
                {
                    creditModel.No_consumo_promedio = float.Parse(Dics["HighAvgConsumption"]);//;                   
                }
                //No_Ahorro_Economico
                double iAverageConsume = 0;
                iAverageConsume = (double)creditModel.No_consumo_promedio;

                if (Dics["Rate"].ToString() != "03" && !esSE)
                    creditModel.No_Ahorro_Economico = K_CREDITOBll.ClassInstance.CalculateEconomicConsumptionSavings(creditModel.Cve_Estado_Neg, iAverageConsume, creditModel.No_Ahorro_Energetico, /*creditModel.Pct_Tasa_IVA / 100*/ (10 + double.Parse(Dics["ClaveIva"])) / 100, Dics["Rate"].ToString(), ref originalConsumption);
                
                //Mt_Capacidad_Pago
                DataTable CreditAmortizacionDt = K_CREDITOBll.ClassInstance.CalculateCreditAmortizacion(creditModel.No_Credito, creditModel.Mt_Monto_Solicitado, creditModel.Pct_Tasa_Fija / 100, creditModel.No_Plazo_Pago, creditModel.Cve_Periodo_Pago, creditModel.Pct_Tasa_Interes / 100, creditModel.Pct_Tasa_IVA / 100, creditModel.Pct_CAT / 100, Dics["PeriodEndDate"]);

                // creditModel.Mt_Capacidad_Pago = K_CREDITOBll.ClassInstance.CalculatePaymentCapacity(creditModel.No_Plazo_Pago, creditModel.Mt_Ingreso_Neto_Mes_Empresa, creditModel.No_Ahorro_Economico, creditModel.Pct_CAT / 100, creditModel.Cve_Periodo_Pago);

                
                
                if (creditModel.Cve_Periodo_Pago != 2)
                     creditModel.Mt_Capacidad_Pago = (creditModel.Mt_Ingreso_Neto_Mes_Empresa + creditModel.No_Ahorro_Economico) / Convert.ToDouble(CreditAmortizacionDt.Rows[0]["Mt_Pago"]);
                else
                     creditModel.Mt_Capacidad_Pago = ((creditModel.Mt_Ingreso_Neto_Mes_Empresa * 2) + creditModel.No_Ahorro_Economico) / Convert.ToDouble(CreditAmortizacionDt.Rows[0]["Mt_Pago"]);
                
                #endregion
                //monto solicitado must be less than capacidad pago
                //if (creditModel.Mt_Monto_Solicitado > creditModel.Mt_Capacidad_Pago)
                //if  ((creditModel.Mt_Ventas_Mes_Aval-creditModel.Mt_Gastos_Mes_Aval)/Convert.ToDouble(CreditAmortizacionDt.Rows[0]["Mt_Pago"]) < 1.75)
                //{
                //    wizardPages.ActiveStepIndex = 2;

                //    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "QuoteGreaterThanPaymentCapacityAval") as string;
                //    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                //}                
                //else

                // RSA 2012-10-09 Validar regla 
                bool esMenor20pct = false;
                double facturaAhorro = 0;
                if (originalConsumption > 0)
                {
                    double limite = originalConsumption * 1.2;
                    facturaAhorro = originalConsumption - creditModel.No_Ahorro_Economico;
                    double pagoMaximo = limite - facturaAhorro;
                    double factorMes = creditModel.Cve_Periodo_Pago == 1 ? 1 : 2;

                    esMenor20pct = double.Parse(CreditAmortizacionDt.Rows[0]["Mt_Pago"].ToString()) / factorMes <= pagoMaximo;
                }

                // RSA 2012-10-09 it's always monthly
                // double factor = creditModel.Cve_Periodo_Pago != 2 ? 12 :6 ;
                double factor = 12;
                if (creditModel.No_Ahorro_Energetico < 0)
                {
                    wizardPages.ActiveStepIndex = 2;

                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "QuoteNegativeSaving") as string;
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                }
                else if (facturaAhorro < 0)
                {
                    wizardPages.ActiveStepIndex = 2;

                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "QuoteNegativeBilling") as string;
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                }
                else if (/*esSE &&*/ creditModel.Mt_Monto_Solicitado / (factor * creditModel.No_Ahorro_Economico) > 4)
                {
                    wizardPages.ActiveStepIndex = 2;

                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "QuoteGreaterPSR") as string;
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                }
                else if (!esSE && creditModel.Mt_Capacidad_Pago < 2)
                {
                    wizardPages.ActiveStepIndex = 2;

                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "QuoteGreaterThanPaymentCapacity") as string;
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                }
                else if (!esMenor20pct)
                {
                    wizardPages.ActiveStepIndex = 2;

                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "PaymentGraterThan20PctCurrentRate") as string;
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                }
                // RSA 20130814 Enable edition, if not editing a credit then check if one already exists
                else if (!IsEditingCredit() && K_CREDITODal.ClassInstance.IsServiceCodeExist(creditModel.No_RPU))
                {
                    wizardPages.ActiveStepIndex = 2;

                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "TheServiceCodehaveUsed") as string;
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                }
                else if (!IsValidRFCAmountLimit(creditModel.Dx_RFC, creditModel.Mt_Monto_Solicitado))
                {
                    wizardPages.ActiveStepIndex = 2;

                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "QuoteGreaterThanRFCLimit") as string;
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                }
                // FRR 20130923 Negative discount
                else if ((creditDesEntity.Mt_Descuento - creditCostEntity.Mt_Costo)<0)
                {
                    wizardPages.ActiveStepIndex = 2;

                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "NegativeDiscount") as string;
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                }

                else
                {
                    int iResult;
                    string strMsg = string.Empty;

                    // RSA 20130814 Enable edition
                    /// Ready to save the information, if it's editing a credit update it, otherwise insert data
                    if (IsEditingCredit())
                    {
                        creditModel.No_Credito = CreditNumber;
                        Cat_Auxiliar.No_Credito = creditModel.No_Credito;
                        creditCostEntity.No_Credito = creditModel.No_Credito;
                        creditDesEntity.No_Credito = creditModel.No_Credito;
                        //Revisar procesos de update comparados con los de intsert para verificar que se actalice todo lo necesario
                        double deTemp = 0;
                        for (int i = 0; i < CreditAmortizacionDt.Rows.Count; i++)
                        {
                            deTemp = deTemp + double.Parse(CreditAmortizacionDt.Rows[i]["Mt_Pago"].ToString());
                        }
                        creditModel.Mt_Monto_Total_Pagar = deTemp;

                        iResult = K_CREDITOBll.ClassInstance.Update_CreditReview(creditModel, creProduct, CreditAmortizacionDt, Cat_Auxiliar, creditCostEntity, creditDesEntity);
                        strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgSaveSucessReview") as string;
                    }
                    else
                    {
                        //Added by Jerry 2011/08/12
                        creditModel.No_Credito = "PAEEEM" + this.RGN_CFE.Substring(2, 5) + /*this.ZoneType + "-" + "1"TBD +*/ string.Format("{0:00000}", Convert.ToInt32(LsUtility.GetNumberSequence("CREDITO")));
                        iResult = K_CREDITOBll.ClassInstance.Insert_K_CreditoData(creditModel, creditCostEntity, creditDesEntity, creProduct, ScheduleEntity, Cat_Auxiliar, Dics["PeriodEndDate"]);
                        strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "Savesuccessfull") as string;
                    }

                    CreditNo = iResult.ToString();
                    if (iResult > 0)
                    {
                        CreditNumber = creditModel.No_Credito;  // RSA 20130819, credit saved, treat it as edition
                        CreditNo = creditModel.No_Credito;//Added by Jerry 2011/08/12

                        string strMsg_Costos = " Con capacidad de realizar " + creditModel.Mt_Capacidad_Pago.ToString("0,0.0") + "pagos(s) por periodo, Ahorro económico por periodo: " + creditModel.No_Ahorro_Economico.ToString("0,0.00") + " $ Ahorrados por factura, Nuevo Consumo Energético: " +
                         creditModel.No_Ahorro_Energetico.ToString("0,0.00") + " KWh por factura, Consumo Promedio: " + creditModel.No_consumo_promedio.ToString("0,0.00") + " KWh estimado por factura.";

                        strMsg += " " + CreditNo + strMsg_Costos;
                        ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                        btn41.Enabled = true;
                        btn42.Enabled = true;
                        btn43.Enabled = true;
                        ButtonRQT.Enabled = true;



                        Button btnSave = wizardPages.FindControl("FinishNavigationTemplateContainerID").FindControl("btnFinishCom") as Button;
                        Button btnPre = wizardPages.FindControl("FinishNavigationTemplateContainerID").FindControl("btnFinishPre") as Button;
                        if (btnSave != null)
                        {
                            btnSave.Enabled = false;
                            // RSA 20130819 no point on returning since save is disabled, (enable saving now that update is implemented)
                            btnPre.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgSaveError") as string;
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
            }
        }

        /// <summary>
        /// Clear Grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                BindDataGirdViewData("C");
                txtTotal.Text = "0";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// gridView add new row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //edit by coco 20110823
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            if (Pct_Tasa_IVA == 0)
            {
                GetPctTasaIVA();
            }
            for (int i = 0; i < GridViewDataSource.Rows.Count; i++)
            {
                DataRow OldRow = GridViewDataSource.Rows[i];

                DropDownList DropDownTechnology = gvTecPro.Rows[i].FindControl("ddlTecnolog") as DropDownList;
                DropDownList DropDownTypeOfProduct = gvTecPro.Rows[i].FindControl("ddlTypeOfProduct") as DropDownList;//edit by coco 20110823
                DropDownList DropDownModel = gvTecPro.Rows[i].FindControl("ddlProduct") as DropDownList;
                DropDownList DropDownMarca = gvTecPro.Rows[i].FindControl("ddlMarca") as DropDownList;              
                TextBox txtCantidad = gvTecPro.Rows[i].FindControl("txtCantidad") as TextBox;
                HiddenField hfExactGridText1 = gvTecPro.Rows[i].FindControl("hfExactGridText1") as HiddenField;
                HiddenField hfExactSubtotal = gvTecPro.Rows[i].FindControl("hfExactSubtotal") as HiddenField;
                TextBox txtGastos = gvTecPro.Rows[i].FindControl("txtGastos") as TextBox;
                //Added by coco 2011-08-04
                DropDownList DropDownCapacidad = gvTecPro.Rows[i].FindControl("ddlCapacidad") as DropDownList;
                //end add

                if (IsSE(DropDownTechnology.SelectedValue))
                    OldRow["Technology"] = "";
                else
                    OldRow["Technology"] = DropDownTechnology.SelectedIndex != -1 ? DropDownTechnology.SelectedValue.ToString() : "";
                OldRow["TypeProduct"] = DropDownTypeOfProduct.SelectedIndex != -1 ? DropDownTypeOfProduct.SelectedValue.ToString() : "";
                OldRow["Marca"] = DropDownMarca.SelectedIndex != -1 ? DropDownMarca.SelectedValue.ToString() : "";
                //add by coco 2011-08-04
                OldRow["Capacidad"] = DropDownCapacidad.SelectedIndex != -1 ? DropDownCapacidad.SelectedValue.ToString() : "";
                //end add
                OldRow["Modelo"] = DropDownModel.SelectedIndex != -1 ? DropDownModel.SelectedValue.ToString() : "";
                OldRow["Cantidad"] = txtCantidad.Text;
                double value;
                OldRow["PrecioUnitario"] = double.TryParse(hfExactGridText1.Value, out value) ? (value * (1 + Pct_Tasa_IVA / 100)).ToString() : string.Empty;   // +IVA
                OldRow["Subtotal"] = double.TryParse(hfExactSubtotal.Value, out value) ? (value * (1 + Pct_Tasa_IVA / 100)).ToString() : string.Empty;          // +IVA
                OldRow["Gastos"] = double.TryParse(txtGastos.Text, out value) ? (value * (1 + Pct_Tasa_IVA / 100)).ToString() : string.Empty;                   // +IVA
            }

            DataRow NewRow;
            NewRow = GridViewDataSource.NewRow();
            GridViewDataSource.Rows.Add(NewRow);

            gvTecPro.DataSource = GridViewDataSource;
            gvTecPro.DataBind();
        }
        //end edit
        /// <summary>
        /// gridview databound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTecPro_DataBound(object sender, EventArgs e)
        {
            US_USUARIOModel User = (US_USUARIOModel)Session["UserInfo"];
            Label LabelCapacidad = gvTecPro.HeaderRow.FindControl("lblCapacidad") as Label;
            for (int i = 0; i < GridViewDataSource.Rows.Count; i++)
            {
                //edit by coco 20110823
                DropDownList DropDownTechnology = gvTecPro.Rows[i].FindControl("ddlTecnolog") as DropDownList;
                DropDownList DropDownTypeOfProduct = gvTecPro.Rows[i].FindControl("ddlTypeOfProduct") as DropDownList;
                DropDownList DropDownModelo = gvTecPro.Rows[i].FindControl("ddlProduct") as DropDownList;
                DropDownList DropDownMarca = gvTecPro.Rows[i].FindControl("ddlMarca") as DropDownList;
                //TextBox txtModelo = gvTecPro.Rows[i].FindControl("txtModelo") as TextBox;
                //end edit 20110823
                TextBox txtCantidad = gvTecPro.Rows[i].FindControl("txtCantidad") as TextBox;
                HiddenField hfExactGridText1 = gvTecPro.Rows[i].FindControl("hfExactGridText1") as HiddenField;
                HiddenField hfExactSubtotal = gvTecPro.Rows[i].FindControl("hfExactSubtotal") as HiddenField;
                TextBox txtGridText1 = gvTecPro.Rows[i].FindControl("txtGridText1") as TextBox;
                TextBox txtSubtotal = gvTecPro.Rows[i].FindControl("txtSubtotal") as TextBox;
                HiddenField hfBasePrice = gvTecPro.Rows[i].FindControl("hfBasePrice") as HiddenField;
                TextBox txtGastos = gvTecPro.Rows[i].FindControl("txtGastos") as TextBox;
                //add by coco 2011-08-04
                DropDownList DropDownCapacidad = gvTecPro.Rows[i].FindControl("ddlCapacidad") as DropDownList;
                //end add

                //add by coco 20110823
              
                if (DropDownTechnology != null)
                {
                    //Edit by coco 2011-08-03
                                        
                    DataTable Technology = CAT_TECNOLOGIABLL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgramAndDxCveCC(Global.PROGRAM.ToString(), User.Id_Usuario, GridViewDataSource.Rows.Count > 1 ? DxCveCC_SE : "");//Changed by Jerry 2011/08/08

                    if (Technology != null && Technology.Rows.Count > 0)//edit by coco 20110823
                    {
                        DropDownTechnology.DataSource = Technology;
                        //end edit
                        DropDownTechnology.DataTextField = "Dx_Nombre_Particular";
                        DropDownTechnology.DataValueField = "Cve_Tecnologia";
                        DropDownTechnology.DataBind();
                        DropDownTechnology.Items.Insert(0, "");

                        if (i < GridViewDataSource.Rows.Count)
                        {
                            DropDownTechnology.SelectedValue = GridViewDataSource.Rows[i]["Technology"].ToString();
                            if (IsSE(GridViewDataSource.Rows[i]["Technology"].ToString()))
                                btnAddRow.Enabled = false;
                        }
                        else
                        {
                            DropDownTechnology.SelectedIndex = 0;
                        }
                        if (DropDownTechnology.SelectedIndex == 0)
                        {
                            //DropDownTypeOfProduct.Enabled =false;
                            DropDownModelo.Enabled =false;
                            DropDownMarca.Enabled = false;
                        }

                        DxCveCC.Clear();
                        CveGasto.Clear();
                        for (int j = 0; j < Technology.Rows.Count; j++)
                        {
                            if (j == 0)
                            {
                                technologyID = Technology.Rows[0]["Cve_Tecnologia"].ToString();
                            }
                            else
                            {
                                technologyID = technologyID + "," + Technology.Rows[j]["Cve_Tecnologia"].ToString();
                            }
                            DxCveCC.Add(Technology.Rows[j]["Cve_Tecnologia"].ToString(), Technology.Rows[j]["Dx_Cve_CC"].ToString());
                            CveGasto.Add(Technology.Rows[j]["Cve_Tecnologia"].ToString(), (int)Technology.Rows[j]["Cve_Gasto"]);
                        }

                        // RSA 20130903 make it visible iff technology is set to one with CveGasto == 1
                        txtGastos.Visible = DropDownTechnology.SelectedIndex > 0 && CveGasto[DropDownTechnology.SelectedValue] == 1;

                        for (int i2 = 0; i2 < DropDownTechnology.Items.Count; i2++)
                        {
                            DropDownTechnology.Items[i2].Attributes.Add("Title", DropDownTechnology.Items[i2].Text);
                        }
                    }
                }
                if (DropDownTypeOfProduct != null)
                {
                    DataTable TypeOfProduct = new DataTable();
                    if (!DropDownTechnology.SelectedValue.Equals(""))
                    {
                        TypeOfProduct = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(DropDownTechnology.SelectedValue);
                    }
                    else
                    {
                       TypeOfProduct=  CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(technologyID);
                    }
                    if (TypeOfProduct != null)
                    {
                        DropDownTypeOfProduct.DataSource = TypeOfProduct;
                        DropDownTypeOfProduct.DataTextField = "Dx_Tipo_Producto";
                        DropDownTypeOfProduct.DataValueField = "Ft_Tipo_Producto";
                        DropDownTypeOfProduct.DataBind();
                        DropDownTypeOfProduct.Items.Insert(0, "");

                        if (i < GridViewDataSource.Rows.Count)
                        {
                            DropDownTypeOfProduct.SelectedValue = GridViewDataSource.Rows[i]["TypeProduct"].ToString();
                        }
                        else
                        {
                            DropDownTypeOfProduct.SelectedIndex = 0;
                        }

                        if (DropDownTypeOfProduct.SelectedIndex == 0)
                        {
                            DropDownModelo.Enabled = false;
                            DropDownMarca.Enabled = false;
                        }
                    }
                    for (int i2 = 0; i2 < DropDownTypeOfProduct.Items.Count; i2++)
                    {
                        DropDownTypeOfProduct.Items[i2].Attributes.Add("Title", DropDownTypeOfProduct.Items[i2].Text);
                    }
                }
                //end edit by coco 20110823

                string strProductID = "0"; //add by coco 2011-08-10
                //edit by coco 20110823
                if (DropDownModelo != null)
                {          
                    DataTable Product=null;
                    Product = CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByTechnology_1(technologyID, idDepartment, DropDownTypeOfProduct.SelectedValue);

                    if (Product != null)
                    {
                        DropDownModelo.DataSource = Product;
                        DropDownModelo.DataTextField = "Dx_Modelo_Producto";
                        DropDownModelo.DataValueField = "Cve_Producto";
                        DropDownModelo.DataBind();
                        DropDownModelo.Items.Insert(0, "");

                        if (i < GridViewDataSource.Rows.Count)
                        {
                            DropDownModelo.SelectedValue = GridViewDataSource.Rows[i]["Modelo"].ToString();
                            lDescription.Text = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ForSE_Description(DropDownModelo.SelectedValue);
                        }
                        else
                        {
                            DropDownModelo.SelectedIndex = 0;
                            lDescription.Text = string.Empty;
                        }
                        //add by coco 2011-08-10
                        for (int j = 0; j < Product.Rows.Count; j++)
                        {
                            if (!Product.Rows[j]["Cve_Producto"].ToString().Equals(""))
                            {                             
                                strProductID = strProductID + "," + Product.Rows[j]["Cve_Producto"].ToString();
                            }
                        }
                        //end add
                    }
                    for (int i2 = 0; i2 < DropDownModelo.Items.Count; i2++)
                    {
                        DropDownModelo.Items[i2].Attributes.Add("Title", DropDownModelo.Items[i2].Text);
                    }
                }
             
                if (DropDownMarca != null)
                {
                    string strTempProduct = "";
                    if (!DropDownModelo.SelectedValue.Equals(""))
                    {
                        strTempProduct = DropDownModelo.SelectedValue;
                    }
                    else
                    {
                        strTempProduct = strProductID;
                    }
                    DataTable Marca = CAT_MARCADal.ClassInstance.Get_CAT_MARCADal(strTempProduct);
                    if (Marca != null)
                    {
                        DropDownMarca.DataSource = Marca;
                        DropDownMarca.DataTextField = "Dx_Marca";
                        DropDownMarca.DataValueField = "Cve_Marca";
                        DropDownMarca.DataBind();
                        DropDownMarca.Items.Insert(0, "");

                        if (i < GridViewDataSource.Rows.Count)
                        {
                            DropDownMarca.SelectedValue = GridViewDataSource.Rows[i]["Marca"].ToString();
                        }
                        else
                        {
                            DropDownMarca.SelectedIndex = 0;
                        }

                        if (DropDownMarca.SelectedIndex == 0)
                        {                            
                            DropDownModelo.Enabled = false;
                        }
                    }
                    for (int i2 = 0; i2 < DropDownMarca.Items.Count; i2++)
                    {
                        DropDownMarca.Items[i2].Attributes.Add("Title", DropDownMarca.Items[i2].Text);
                    }
                }

                //add by coco 2011-08-04
                if (DropDownCapacidad != null)
                {
                    string strTempTech = "";
                    if (DropDownTechnology.SelectedValue.Equals(""))
                    {
                        strTempTech = technologyID;
                    }
                    else
                    {
                        strTempTech = DropDownTechnology.SelectedValue;
                    }

                    DataTable Capacity;
                    if (IsSE(DropDownTechnology.SelectedValue))
                    {
                        Capacity = new DataTable();
                        Capacity.Columns.Add("Cve_Producto_Capacidad");
                        Capacity.Columns.Add("Ft_Capacidad");
                        Capacity.Rows.Add(new object[] { "3", "OM" });
                        Capacity.Rows.Add(new object[] { "4", "HM" });

                        // RSA 20131022 when reading saved data make sure all fields are set correctly
                        txtCantidad.Text = "1";
                        txtCantidad.Enabled = false;

                        LabelCapacidad.Text = "Tarifa";

                        lDescription.Visible = true;
                    }
                    else
                    {
                        Capacity = ProductCapacityDal.ClassInstance.Get_ProductCapacity(strTempTech);

                        LabelCapacidad.Text = "Capacidad";
                    }
                    if (Capacity != null)
                    {
                        DropDownCapacidad.DataSource = Capacity;
                        DropDownCapacidad.DataTextField = "Ft_Capacidad";
                        DropDownCapacidad.DataValueField = "Cve_Producto_Capacidad";
                        DropDownCapacidad.DataBind();
                        DropDownCapacidad.Items.Insert(0, "");

                        if (i < GridViewDataSource.Rows.Count)
                        {
                            DropDownCapacidad.SelectedValue = GridViewDataSource.Rows[i]["Capacidad"].ToString();
                        }
                        else
                        {
                            DropDownCapacidad.SelectedIndex = 0;
                        }
                    }
                }
                //end add
                if (i < GridViewDataSource.Rows.Count)
                {
                    // RSA now set the updated values from the data source
                    if (Pct_Tasa_IVA == 0)
                    {
                        GetPctTasaIVA();
                    }
                    double value;

                    // RSA 20130904 Read the real price for this product
                    DataTable ProductDataTable = K_PROVEEDOR_PRODUCTOBLL.ClassInstance.Get_K_PROVEEDOR_PRODUCTO_ByPK(DropDownModelo.SelectedValue, idDepartment);
                    if (ProductDataTable != null
                        && ProductDataTable.Rows.Count > 0
                        && double.TryParse(ProductDataTable.Rows[0]["Mt_Precio_Unitario"].ToString(), out value)
                    )
                    {
                        hfBasePrice.Value = (value / (double)(1 + Pct_Tasa_IVA / 100)).ToString();
                    }
                    else
                    {
                        hfBasePrice.Value = "0";
                    }

                    txtCantidad.Text = GridViewDataSource.Rows[i]["Cantidad"].ToString();
                    hfExactGridText1.Value = double.TryParse(GridViewDataSource.Rows[i]["PrecioUnitario"].ToString(), out value) ? (value / (1 + Pct_Tasa_IVA / 100)).ToString() : "";    // -IVA
                    hfExactSubtotal.Value = double.TryParse(GridViewDataSource.Rows[i]["Subtotal"].ToString(), out value) ? (value / (1 + Pct_Tasa_IVA / 100)).ToString() : "";           // -IVA
                    txtGridText1.Text = double.TryParse(GridViewDataSource.Rows[i]["PrecioUnitario"].ToString(), out value) ? TwoDecimals(value / (1 + Pct_Tasa_IVA / 100)).ToString("0.00") : "";   // -IVA
                    txtSubtotal.Text = double.TryParse(GridViewDataSource.Rows[i]["Subtotal"].ToString(), out value) ? TwoDecimals(value / (1 + Pct_Tasa_IVA / 100)).ToString("0.00") : "";          // -IVA
                    //txtGridText1.ToolTip = hfExactGridText1.Value;
                    // txtSubtotal.ToolTip = hfExactSubtotal.Value;
                    txtGastos.Text = double.TryParse(GridViewDataSource.Rows[i]["Gastos"].ToString(), out value) ? (value / (1 + Pct_Tasa_IVA / 100)).ToString("0.00") : "";              // -IVA
                }

                DataTable dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIATipoByPK(DropDownTechnology.SelectedValue);
                if (dtTechnology != null && dtTechnology.Rows.Count > 0)
                {
                    if (!dtTechnology.Rows[0]["Dx_Nombre"].ToString().Trim().StartsWith("Iluminacion") && !IsSE(DropDownTechnology.SelectedValue))
                    {
                        DropDownCapacidad.Enabled = false;
                    }
                    else
                    {
                        DropDownCapacidad.Enabled = true;
                    }
                }
            }

            AccountTotal();
        }

        //add by coco 2012-07-18
        protected void btnPreliminary_Click( object sender, EventArgs e )
        {
            K_CREDITO_TEMPEntity creditTempModel = creditTempModel = GetPreliminaryStoredData();

            int Result = K_CREDITOBll.ClassInstance.Insert_K_Credito_Temp(creditTempModel);

            if ( Result > 0 )
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "PreliminarySave", "alert('Salvado temporal exitoso!');", true);
            }
        }
        private K_CREDITO_TEMPEntity GetPreliminaryStoredData()
        {
            //section1 data
            K_CREDITO_TEMPEntity creditTempEntity = new K_CREDITO_TEMPEntity();
            creditTempEntity.Cve_Tipo_Sociedad = ddlDX_TIPO_SOCIEDAD.SelectedValue;
            creditTempEntity.Dx_First_Name = txtName.Text;
            creditTempEntity.Dx_Last_Name = txtLastname.Text;
            creditTempEntity.Dx_Mother_Name = txtMotherName.Text;
            creditTempEntity.Dt_Fecha_Nacimiento = txtBirthDate.Text;
            creditTempEntity.Cve_Tipo_Industria = ddlDX_TIPO_INDUSTRIA.SelectedValue;
            creditTempEntity.Dx_Telephone = txtTelephone.Text;
            creditTempEntity.Dx_Email = txtEmail.Text;
            creditTempEntity.Dx_CURP = txtDX_CURP.Text;
            creditTempEntity.Dx_Nombre_Comercial = txtDx_Nombre_Comercial.Text;
            creditTempEntity.Dx_RFC = txtDX_RFC.Text;
            creditTempEntity.Dx_Nombre_Repre_Legal = txtDX_NOMBRE_REPRE_LEGAL.Text;
            creditTempEntity.Cve_Acreditacion_Repre_legal = ddlDX_ACREDITACION_REPRE_LEGAL.SelectedValue;
            if ( radFG_SEXO_REPRE_LEGAL1.Checked == true )
            {
                creditTempEntity.Fg_Sexo_Repre_legal = "1";
            }
            else if ( radFG_SEXO_REPRE_LEGAL2.Checked == true )
            {
                creditTempEntity.Fg_Sexo_Repre_legal = "2";
            }
            creditTempEntity.No_RPU = txtNO_RPU.Text;
            if ( radFG_EDO_CIVIL_REPRE_LEGAL1.Checked == true )
            {
                creditTempEntity.Fg_Edo_Civil_Repre_legal = "1";
            }
            else if ( radFG_EDO_CIVIL_REPRE_LEGAL2.Checked == true )
            {
                creditTempEntity.Fg_Edo_Civil_Repre_legal = "2";
            }
            creditTempEntity.Cve_Reg_Conyugal_Repre_legal = this.ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedValue;
            creditTempEntity.Cve_Identificacion_Repre_legal = this.ddlDX_IDENTIFICACION_REPRE_LEGAL.SelectedValue;
            creditTempEntity.Dx_No_Identificacion_Repre_Legal = this.txtDX_NO_IDENTIFICACION_REPRE_LEGAL.Text;
            creditTempEntity.Mt_Ventas_Mes_Empresa = this.txtMT_VENTAS_MES_EMPRESA.Text;
            creditTempEntity.Mt_Gastos_Mes_Empresa = this.txtMT_GASTOS_MES_EMPRESA.Text;
            creditTempEntity.Dx_Email_Repre_legal = txtDX_EMAIL_REPRE_LEGAL.Text;
            creditTempEntity.No_consumo_promedio = txtNO_CONSUMO_PROMEDIO.Text;
            //section2 data
            creditTempEntity.Dx_Domicilio_Fisc_Calle = txtDx_Domicilio_fisc_calle.Text;
            creditTempEntity.Dx_Domicilio_Fisc_Num = txtDx_Domicilio_fisc_num.Text;
            creditTempEntity.Dx_Domicilio_Fisc_CP = txtDx_Domicilio_fisc_cp.Text;
            creditTempEntity.Cve_Estado_Fisc = ddlDx_nombre_estado.SelectedValue;
            creditTempEntity.Cve_Deleg_Municipio_Fisc = ddlDx_deleg_municipio.SelectedValue;
            //creditTempEntity.Dx_Ciudad = txtCiudad.Text;
            //creditTempEntity.Dx_Numero_Interior = txtInterior.Text;
            creditTempEntity.Cve_Tipo_Propiedad_Fisc = ddlDx_Tipo_propiedad.SelectedValue;
            creditTempEntity.Dx_Tel_Fisc = txtDx_tel_neg.Text;
            creditTempEntity.Dx_Domicilio_Fisc_Colonia = txtDx_Domicilio_Fisc_Colonia.Text;
            //section3 data
            if ( ckbFg_mismo_domicilio.Checked )
            {
                creditTempEntity.Fg_Mismo_Domicilio = "1";
            }
            else
            {
                creditTempEntity.Fg_Mismo_Domicilio = "0";
            }
            creditTempEntity.Dx_Domicilio_Neg_Calle = txtDx_domicilio_neg_calle.Text;
            creditTempEntity.Dx_Domicilio_Neg_Num = txtDx_domicilio_neg_num.Text;
            creditTempEntity.Dx_Domicilio_Neg_CP = txtDx_domicilio_neg_cp.Text;
            creditTempEntity.Cve_Estado_Neg = ddlDx_nombre_estado_Neg.SelectedValue;
            creditTempEntity.Cve_Deleg_Municipio_Neg = ddlDx_deleg_municipio_Neg.SelectedValue;
            creditTempEntity.Cve_Tipo_Propiedad_Neg = ddlDx_Tipo_propiedad_Neg.SelectedValue;
            creditTempEntity.Dx_Tel_Neg = txtDx_tel_neg_Neg.Text;
            creditTempEntity.Dx_Domicilio_Neg_Colonia = txtDx_Domicilio_Neg_Colonia.Text;
            //section4 data
            // RSA 20131007 single fields for first, last and mother names, keep a full name field (trim white spaces at end or middle as necesary)
            creditTempEntity.Dx_Nombre_Aval = ((txtDx_First_Name_aval.Text + " " + txtDx_Last_Name_aval.Text).Trim() + " " + txtDx_Mother_Name_aval.Text).Trim();
            creditTempEntity.Dx_First_Name_Aval = txtDx_First_Name_aval.Text;
            creditTempEntity.Dx_Last_Name_Aval = txtDx_Last_Name_aval.Text;
            creditTempEntity.Dx_Mother_Name_Aval = txtDx_Mother_Name_aval.Text;
            creditTempEntity.Dt_BirthDate_Aval = txtDt_BirthDate_Aval.Text;
            creditTempEntity.Dx_RFC_Aval = txtDx_RFC_Aval.Text;
            creditTempEntity.Dx_CURP_Aval = txtDx_CURP_Aval.Text;
            creditTempEntity.Dx_RFC_CURP_Aval = txtDx_RFC_Aval.Text;
            creditTempEntity.Dx_Tel_Aval = txtDx_tel_aval.Text;
            if ( radDx_sexo1.Checked )
            {
                creditTempEntity.Fg_Sexo_Aval = "1";
            }
            else if ( radDx_sexo2.Checked )
            {
                creditTempEntity.Fg_Sexo_Aval = "2";
            }
            creditTempEntity.Dx_Domicilio_Aval_Calle = txtDx_domicilio_aval_calle.Text;
            creditTempEntity.Dx_Domicilio_Aval_Num = txtDx_domicilio_aval_num.Text;
            creditTempEntity.Dx_Domicilio_Aval_CP = txtDx_domicilio_aval_cp.Text;
            creditTempEntity.Cve_Estado_Aval = ddlDx_nombre_estado_aval.SelectedValue;
            creditTempEntity.Cve_Deleg_Municipio_Aval = ddlDx_deleg_municipio_aval.SelectedValue;
            creditTempEntity.Dx_Domicilio_Aval_Colonia = txtDx_Domicilio_Aval_Colonia.Text;
            //creditTempEntity.No_RPU_AVAL = txtNo_RPU_aval.Text;
            //creditTempEntity.Mt_Ventas_Mes_Aval = txtMt_ventas_mes_aval.Text;
            //creditTempEntity.Mt_Gastos_Mes_Aval = txtMt_gastos_mes_aval.Text;
            creditTempEntity.User_Name = UserID;

            return creditTempEntity;
        }
        //end add
        #endregion

        #region Private Methods
        /// <summary>
        /// Validate service code
        /// </summary>
        /// <param name="strService"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        private Boolean ValidateServiceCode(string servicecode, out string ErrorCode,out string StatusFlag)
        {
            Boolean ServiceCodeValidate = false;
            StatusFlag = "";
            ErrorCode = "";


            if (IsServiceCodeLongEnough(servicecode) /*SICOMHelper.tempRpus.Keys.Contains<string>(servicecode)*/)
            {
                 Dics = SICOMHelper.GetAttributes(servicecode);
                //Added by Jerry 2011/08/12
                this.RGN_CFE = Dics["CN"];
                //this.ZoneType = Dics["ZoneType"];

                //End
                 // RSA 20110316 communication status
                 if (!IsComStatusValid(Dics["ComStatus"]))
                 {
                     ErrorCode = (string)GetGlobalResourceObject("DefaultResource"
                         , "SICOM_Communication_Status_" + Dics["ComStatus"]);
                     StatusFlag = "W";
                 }
                 else if (IsUserStatusValid(Dics["UserStatus"]))  //only user status "0" and "01" are valid       
                 {
                     if (string.IsNullOrEmpty(Dics["HighAvgConsumption"]))
                     {
                         ErrorCode = "El usuario no presenta consumo.";
                         StatusFlag = "B";
                     }
                     else if (ValidateRate(Dics["Rate"]))
                     { 
                           if( ValidateUserStatus(Dics["UserStatus"]))
                           {
                               if (ValidateNoDebit(Dics["CurrentBillingStatus"], Dics["DueDate"]) && !IsInDebts(Dics))
                               {
                                   if (true)//ValidateMinConsumptionDate(Dics["MinConsumptionDate"]))
                                   {
                                       ServiceCodeValidate = true;
                                       if (Dics["Periodof"] != "1")
                                       {
                                           ddlDx_periodo_pago.SelectedValue = "2";
                                       }
                                       else
                                       {
                                           ddlDx_periodo_pago.SelectedValue = "1";
                                       }         
                                   }
                                   else
                                   {
                                       ErrorCode = "La fecha recibida del servidor de CFE es inválida";
                                       StatusFlag = "B";
                                   }
                               }
                               else
                               {
                                   ErrorCode = "El usuario presenta adeudo con CFE";
                                   StatusFlag = "B";
                               }
                           }
                           else
                           {
                               ErrorCode = "Usuario Inactivo";
                               StatusFlag = "B";
                           }
                     }
                     else
                     {   
                         ErrorCode = "La tarifa del usuario no aplica para el programa";
                         StatusFlag = "A";
                     }
                 }
                 else
                 {
                     ErrorCode = "El estatus del usuario es inválido" + Dics["UserStatus"];//"The user status is invalid.";
                 }

            }
            else
            {
                ErrorCode =  "El número de servicio es incorrecto";
            }

            return ServiceCodeValidate;
        }
        /// <summary>
        /// RSA
        /// Is it long enough
        /// </summary>
        /// <param name="currentdebitstatus"></param>
        /// <param name="duedate"></param>
        /// <returns></returns>
        private bool IsServiceCodeLongEnough(string rpu)
        {
            bool bResult = true;
            if (rpu.Length != 12)
            {
                bResult = false;
            }
            return bResult;
        }
        // RSA 20110316 communication status
        /// <summary>
        /// Check communication status
        /// </summary>
        /// <param name="status">status</param>
        /// <returns>bool</returns>
        private bool IsComStatusValid(string status)
        {
            bool bResult = false;

            if (status.StartsWith("A"))
            {
                bResult = true;
            }
            return bResult;
        }
        /// <summary>
        /// Check user status
        /// </summary>
        /// <param name="status">status</param>
        /// <returns>bool</returns>
        private bool IsUserStatusValid(string status)
        {
            bool bResult = false;

            if (status == "01")
            {
                bResult = true;
            }
            return bResult;
        }

        /// <summary>
        /// Validate Rate
        /// </summary>
        /// <param name="Rate"></param>
        /// <returns></returns>
        private Boolean ValidateRate(string Rate)
        {
            Boolean Flag = false;
            DataTable dtRate = CAT_PROGRAMADal.ClassInstance.get_Cat_Tarifa(Global.PROGRAM.ToString(), Rate);//Change by Jerry 2011/08/08
            if (dtRate != null && dtRate.Rows.Count > 0)
            {
                Flag = true;
            }
            return Flag;
        }
        /// <summary>
        /// Validate User Status
        /// </summary>
        /// <param name="UserStatus"></param>
        /// <returns></returns>
        private Boolean ValidateUserStatus(string UserStatus)
        {
            bool Flag = false;
            if (!string.IsNullOrEmpty(UserStatus))
            {
                if (int.Parse(UserStatus) == int.Parse(ProgramDt.Rows[0]["No_Estatus_CFE"].ToString()))
                {
                    Flag = true;
                }
            }
            return Flag;
        }
        /// <summary>
        /// validate CurrentDebit
        /// </summary>
        /// <param name="CurrentBillStatus"></param>
        /// <param name="DueDate"></param>
        /// <returns></returns>
        private Boolean ValidateNoDebit(string CurrentBillStatus, string DueDate)
        {
            Boolean Flag = true;
            DateTime dueDate;
           string[] dateFormat ={"yyyyMMdd","yyyy/MM/dd","yyyy-MM-dd","yyyyMM"};
            try
            {
                dueDate = DateTime.ParseExact(DueDate, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                if (CurrentBillStatus == "0" && dueDate < DateTime.Now.Date)
                {
                    Flag = false;
                }
                return Flag;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Is in debts
        /// </summary>
        /// <param name="dics">Dictionary parameters</param>
        /// <returns>bool</returns>
        private bool IsInDebts(Dictionary<string, string> dics)
        {
            bool bResult = false;

            // if empty or with a value bigger than 00 then is in debts
            if (string.IsNullOrEmpty(dics["DebitNumber"]) || dics["DebitNumber"].CompareTo("00") != 0)
                bResult = true;

            return bResult;
        }
        
        private Boolean ValidateMinConsumptionDate(string temp)
        {
            ////Boolean Flag = false;
            ////string[] dateFormat = { "yyyyMMdd", "yyyy/MM/dd", "yyyy-MM-dd", "yyyyMM" };
            ////DateTime tempDate;
            //try
            //{
            //    //tempDate = DateTime.ParseExact(temp, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            //    //if (DateTime.Now.Date.Year - tempDate.Year >= 1)
            //    //{
            //    //    Flag = true;
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("La fecha de consumo mínimo regresada por CFE es nula", ex);
            //}
            ////return Flag;
            return true;
        }
        /// <summary>
        /// fix inputs, such as capitalize, replace non ascii like "ñ" with
        /// it's ascii equivalent "n"
        /// </summary>
        private string CapitalASCII(string text)
        {
            text = text.ToUpper();
            text = text.Replace('Á', 'A');
            text = text.Replace('É', 'E');
            text = text.Replace('Í', 'I');
            text = text.Replace('Ó', 'O');
            text = text.Replace('Ú', 'U');
            // RSA 20131008 allow ñ
            // text = text.Replace('Ñ', 'N');

            return text;
        }
        private void CapitalASCIIFields()
        {
            txtName.Text = CapitalASCII(txtName.Text);
            txtLastname.Text = CapitalASCII(txtLastname.Text);
            txtMotherName.Text = CapitalASCII(txtMotherName.Text);
            txtEmail.Text = CapitalASCII(txtEmail.Text);

            txtDX_CURP.Text = CapitalASCII(txtDX_CURP.Text);
            txtDx_Nombre_Comercial.Text = CapitalASCII(txtDx_Nombre_Comercial.Text);
            txtDX_RFC.Text = CapitalASCII(txtDX_RFC.Text);

            txtDX_NOMBRE_REPRE_LEGAL.Text = CapitalASCII(txtDX_NOMBRE_REPRE_LEGAL.Text);
            txtDX_NO_IDENTIFICACION_REPRE_LEGAL.Text = CapitalASCII(txtDX_NO_IDENTIFICACION_REPRE_LEGAL.Text);
            txtDX_EMAIL_REPRE_LEGAL.Text = CapitalASCII(txtDX_EMAIL_REPRE_LEGAL.Text);

            txtDx_Domicilio_fisc_calle.Text = CapitalASCII(txtDx_Domicilio_fisc_calle.Text);
            txtDx_Domicilio_fisc_num.Text = CapitalASCII(txtDx_Domicilio_fisc_num.Text);
            txtDx_Domicilio_Fisc_Colonia.Text = CapitalASCII(txtDx_Domicilio_Fisc_Colonia.Text);

            txtDx_domicilio_neg_calle.Text = CapitalASCII(txtDx_domicilio_neg_calle.Text);
            txtDx_domicilio_neg_num.Text = CapitalASCII(txtDx_domicilio_neg_num.Text);
            txtDx_Domicilio_Neg_Colonia.Text = CapitalASCII(txtDx_Domicilio_Neg_Colonia.Text);

            txtDx_First_Name_aval.Text = CapitalASCII(txtDx_First_Name_aval.Text);
            txtDx_Last_Name_aval.Text = CapitalASCII(txtDx_Last_Name_aval.Text);
            txtDx_Mother_Name_aval.Text = CapitalASCII(txtDx_Mother_Name_aval.Text);
            txtDx_RFC_Aval.Text = CapitalASCII(txtDx_RFC_Aval.Text);
            txtDx_CURP_Aval.Text = CapitalASCII(txtDx_CURP_Aval.Text);
            txtDx_domicilio_aval_calle.Text = CapitalASCII(txtDx_domicilio_aval_calle.Text);
            txtDx_domicilio_aval_num.Text = CapitalASCII(txtDx_domicilio_aval_num.Text);
            txtDx_Domicilio_Aval_Colonia.Text = CapitalASCII(txtDx_Domicilio_Aval_Colonia.Text);

            txtDx_nombre_coacreditado.Text = CapitalASCII(txtDx_nombre_coacreditado.Text);
            txtDx_RFC_CURP_Coacreditado.Text = CapitalASCII(txtDx_RFC_CURP_Coacreditado.Text);
            txtDx_Domicilio_Coacreditado_Colonia.Text = CapitalASCII(txtDx_Domicilio_Coacreditado_Colonia.Text);
            txtDx_domicilio_coacreditado_calle.Text = CapitalASCII(txtDx_domicilio_coacreditado_calle.Text);
            txtDx_domicilio_coacreditado_num.Text = CapitalASCII(txtDx_domicilio_coacreditado_num.Text);
        }
        /// <summary>
        /// Get data from step one
        /// </summary>
        /// <param name="CreditModel"></param>
        private CreditEntity GetDataFromStepOne()
        {
            CreditEntity creditModel = new CreditEntity();

            //creditModel.No_Credito = 0;
            creditModel.ID_Prog_Proy = Global.PROGRAM;//Change by Jerry 2011/08/08
            creditModel.Cve_Estatus_Credito = (int)CreditStatus.PENDIENTE;
            //Changed by Jerry 2011/09/07
            if (Session["UserInfo"] != null)
            {
                creditModel.Id_Proveedor = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
            }
            //else
            //{
            //    throw new Exception("La sesión ha expirado, debe iniciar sesión nuevamente");
            //}
            // RSA 2012-10-19 Apply to REPECO too
            if (CompanyType.PERSONAFISICA == (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue) || CompanyType.REPECO == (CompanyType)Enum.Parse(typeof(CompanyType), ddlDX_TIPO_SOCIEDAD.SelectedValue))     
            // if (string.Compare(ddlDX_TIPO_SOCIEDAD.SelectedItem.Text, "PERSONA FISICa", true) == 0 || string.Compare(ddlDX_TIPO_SOCIEDAD.SelectedItem.Text, "REPECO", true) == 0) //Only the section for "Persona Fisica" FRR
            {
                creditModel.Dx_Razon_Social = txtName.Text + " " + txtLastname.Text + " " + txtMotherName.Text;
            }
            else
            {
                creditModel.Dx_Razon_Social = txtName.Text;
            }

            creditModel.Cve_Tipo_Industria = (this.ddlDX_TIPO_INDUSTRIA.SelectedIndex != 0 && this.ddlDX_TIPO_INDUSTRIA.SelectedIndex != -1) ? Convert.ToInt32(ddlDX_TIPO_INDUSTRIA.SelectedValue) : 0;
            creditModel.Cve_Tipo_Sociedad = (this.ddlDX_TIPO_SOCIEDAD.SelectedIndex != 0 && this.ddlDX_TIPO_SOCIEDAD.SelectedIndex != -1) ? Convert.ToInt32(ddlDX_TIPO_SOCIEDAD.SelectedValue) : 0;
            creditModel.Dx_CURP = txtDX_CURP.Text;
            creditModel.Dx_Nombre_Comercial = txtDx_Nombre_Comercial.Text;
            creditModel.Dx_RFC = txtDX_RFC.Text;
            creditModel.Dx_Nombre_Repre_Legal = txtDX_NOMBRE_REPRE_LEGAL.Text;
            creditModel.Cve_Acreditacion_Repre_legal = (this.ddlDX_ACREDITACION_REPRE_LEGAL.SelectedIndex != 0 && this.ddlDX_ACREDITACION_REPRE_LEGAL.SelectedIndex != -1) ? Convert.ToInt32(ddlDX_ACREDITACION_REPRE_LEGAL.SelectedValue) : 0;
            
            if (radFG_SEXO_REPRE_LEGAL1.Checked == true)
            {
                creditModel.Fg_Sexo_Repre_legal = 1;
            }
            else if (radFG_SEXO_REPRE_LEGAL2.Checked == true)
            {
                creditModel.Fg_Sexo_Repre_legal = 2;
            }

            creditModel.No_RPU = txtNO_RPU.Text;

            if (radFG_EDO_CIVIL_REPRE_LEGAL1.Checked == true)
            {
                creditModel.Fg_Edo_Civil_Repre_legal = 1;
            }
            else if (radFG_EDO_CIVIL_REPRE_LEGAL2.Checked == true)
            {
                creditModel.Fg_Edo_Civil_Repre_legal = 2;
            }

            creditModel.Cve_Reg_Conyugal_Repre_legal = (this.ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedIndex != 0 && this.ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedIndex != -1) ? Convert.ToInt32(ddlDX_REG_CONYUGAL_REPRE_LEGAL.SelectedValue) : 0;
            creditModel.Cve_Identificacion_Repre_legal = (this.ddlDX_IDENTIFICACION_REPRE_LEGAL.SelectedIndex != 0 && this.ddlDX_IDENTIFICACION_REPRE_LEGAL.SelectedIndex != -1) ? Convert.ToInt32(ddlDX_IDENTIFICACION_REPRE_LEGAL.SelectedValue) : 0;
            creditModel.Dx_No_Identificacion_Repre_Legal = txtDX_NO_IDENTIFICACION_REPRE_LEGAL.Text;
            creditModel.Mt_Ventas_Mes_Empresa = (!string.IsNullOrEmpty(this.txtMT_VENTAS_MES_EMPRESA.Text)) ? Convert.ToDouble(this.txtMT_VENTAS_MES_EMPRESA.Text) : default(double);
            creditModel.Mt_Gastos_Mes_Empresa = (!string.IsNullOrEmpty(this.txtMT_GASTOS_MES_EMPRESA.Text)) ? Convert.ToDouble(this.txtMT_GASTOS_MES_EMPRESA.Text) : default(double);
            creditModel.Mt_Ingreso_Neto_Mes_Empresa = creditModel.Mt_Ventas_Mes_Empresa - creditModel.Mt_Gastos_Mes_Empresa;
            creditModel.Dx_Email_Repre_legal = txtDX_EMAIL_REPRE_LEGAL.Text;
            //DOMICILIO FISCAL   //
            creditModel.Dx_Domicilio_Fisc_Calle = this.txtDx_Domicilio_fisc_calle.Text;
            creditModel.Dx_Domicilio_Fisc_Num = txtDx_Domicilio_fisc_num.Text;
            creditModel.Dx_Domicilio_Fisc_CP = txtDx_Domicilio_fisc_cp.Text;

            if (ddlDx_nombre_estado.SelectedIndex != -1 && ddlDx_nombre_estado.SelectedIndex != 0)
            {
                creditModel.Cve_Estado_Fisc = Convert.ToInt32(ddlDx_nombre_estado.SelectedValue);
            }
            if (ddlDx_deleg_municipio.SelectedIndex != -1 && ddlDx_deleg_municipio.SelectedIndex != 0)
            {
                creditModel.Cve_Deleg_Municipio_Fisc = Convert.ToInt32(ddlDx_deleg_municipio.SelectedValue);
            }
            if (ddlDx_Tipo_propiedad.SelectedIndex != -1 && ddlDx_Tipo_propiedad.SelectedIndex != 0)
            {
                creditModel.Cve_Tipo_Propiedad_Fisc = Convert.ToInt32(ddlDx_Tipo_propiedad.SelectedValue);
            }

            creditModel.Dx_Tel_Fisc = txtDx_tel_neg.Text;
            creditModel.Dx_Domicilio_Fisc_Colonia = txtDx_Domicilio_Fisc_Colonia.Text;
            //check box BUSINESS ADDRESS
            if (ckbFg_mismo_domicilio.Checked)
            {
                creditModel.Fg_Mismo_Domicilio = true;
            }
            else
            {
                creditModel.Fg_Mismo_Domicilio = false;
            }
            creditModel.Dx_Domicilio_Neg_Calle = txtDx_domicilio_neg_calle.Text;
            creditModel.Dx_Domicilio_Neg_Num = txtDx_domicilio_neg_num.Text;
            creditModel.Dx_Domicilio_Neg_CP = txtDx_domicilio_neg_cp.Text;

            if (ddlDx_nombre_estado_Neg.SelectedIndex != -1 && ddlDx_nombre_estado_Neg.SelectedIndex != 0)
            {
                creditModel.Cve_Estado_Neg = Convert.ToInt32(ddlDx_nombre_estado_Neg.SelectedValue);
            }
            if (ddlDx_deleg_municipio_Neg.SelectedIndex != -1 && ddlDx_deleg_municipio_Neg.SelectedIndex != 0)
            {
                creditModel.Cve_Deleg_Municipio_Neg = Convert.ToInt32(ddlDx_deleg_municipio_Neg.SelectedValue);
            }
            if (ddlDx_Tipo_propiedad_Neg.SelectedIndex != -1 && ddlDx_Tipo_propiedad_Neg.SelectedIndex != 0)
            {
                creditModel.Cve_Tipo_Propiedad_Neg = Convert.ToInt32(ddlDx_Tipo_propiedad_Neg.SelectedValue);
            }

            creditModel.Dx_Tel_Neg = txtDx_tel_neg_Neg.Text;
            creditModel.Dx_Domicilio_Neg_Colonia = txtDx_Domicilio_Neg_Colonia.Text;
            //DATOS DEL AVAL
            // RSA 20131007 single fields for first, last and mother names, keep a full name field (trim white spaces at end or middle as necesary)
            creditModel.Dx_Nombre_Aval = ((txtDx_First_Name_aval.Text + " " + txtDx_Last_Name_aval.Text).Trim() + " " + txtDx_Mother_Name_aval.Text).Trim();
            creditModel.Dx_First_Name_Aval = txtDx_First_Name_aval.Text;
            creditModel.Dx_Last_Name_Aval = txtDx_Last_Name_aval.Text;
            creditModel.Dx_Mother_Name_Aval = txtDx_Mother_Name_aval.Text;
            creditModel.Dt_BirthDate_Aval = txtDt_BirthDate_Aval.Text;
            creditModel.Dx_RFC_CURP_Aval = txtDx_RFC_Aval.Text;
            creditModel.Dx_RFC_Aval = txtDx_RFC_Aval.Text;
            creditModel.Dx_CURP_Aval = txtDx_CURP_Aval.Text;
            creditModel.Dx_Tel_Aval = txtDx_tel_aval.Text;

            if (radDx_sexo1.Checked)
            {
                creditModel.Fg_Sexo_Aval = 1;
            }
            else if (radDx_sexo2.Checked)
            {
                creditModel.Fg_Sexo_Aval = 2;
            }

            creditModel.Dx_Domicilio_Aval_Calle = txtDx_domicilio_aval_calle.Text;
            creditModel.Dx_Domicilio_Aval_Num = txtDx_domicilio_aval_num.Text;
            creditModel.Dx_Domicilio_Aval_CP = txtDx_domicilio_aval_cp.Text;

            if (ddlDx_nombre_estado_aval.SelectedIndex != -1 && ddlDx_nombre_estado_aval.SelectedIndex != 0)
            {
                creditModel.Cve_Estado_Aval = Convert.ToInt32(ddlDx_nombre_estado_aval.SelectedValue);
            }
            if (ddlDx_deleg_municipio_aval.SelectedIndex != -1 && ddlDx_deleg_municipio_aval.SelectedIndex != 0)
            {
                creditModel.Cve_Deleg_Municipio_Aval = Convert.ToInt32(ddlDx_deleg_municipio_aval.SelectedValue);
            }

            //creditModel.Mt_Ventas_Mes_Aval = (!string.IsNullOrEmpty(this.txtMt_ventas_mes_aval.Text)) ? Convert.ToDouble(this.txtMt_ventas_mes_aval.Text) : default(double);// txtMt_ventas_mes_aval.Text;
            //creditModel.Mt_Gastos_Mes_Aval = (!string.IsNullOrEmpty(this.txtMt_gastos_mes_aval.Text)) ? Convert.ToDouble(this.txtMt_gastos_mes_aval.Text) : default(double);// txtMt_gastos_mes_aval.Text;
            //creditModel.Mt_Ingreso_Neto_Mes_Aval = creditModel.Mt_Ventas_Mes_Aval - creditModel.Mt_Gastos_Mes_Aval;
            //creditModel.No_RPU_AVAL = txtNo_RPU_aval.Text;
            creditModel.Dx_Domicilio_Aval_Colonia = txtDx_Domicilio_Aval_Colonia.Text;

            return creditModel;
        }
        /// <summary>
        /// initial Credit Request - Financial Support Person page
        /// </summary>
        private void InitStepOneData()
        {
            lblDate1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            BindddlDx_nombre_estado2();
            BindddlDX_DELEG_MUNICIPIO2();
            RadioButton11.Checked = true;
        }
        /// <summary>
        /// Page load page2 data
        /// </summary>
        private void InitStepTwoData()
        {
            lblDate2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtDx_razon_social3.Text = txtName.Text + " " + txtLastname.Text + " " + txtMotherName.Text;
            txtDx_tipo_industria3.Text = ddlDX_TIPO_INDUSTRIA.SelectedItem.Text;

            //Added by coco 2011-08-05
            #region"K_CREDITO_DESCUENTO"
            creditDesEntity = new K_CREDITO_DESCUENTOEntity();
            //creditDesEntity.No_Credito = 0;
            creditDesEntity.Dt_Credito_Descuento = DateTime.Now.Date;

            DataTable ProgramDestDt = K_PROGRAMA_DESCUENTOBLL.ClassInstance.Get_All_K_PROGRAMA_DESCUENTO(Global.PROGRAM.ToString());//Changed by Jerry 2011/08/08
            if (ProgramDestDt != null && ProgramDestDt.Rows.Count > 0)
            {
                if (ProgramDestDt.Rows[0]["Fg_Aplicar_Pct"] != null && Boolean.Parse(ProgramDestDt.Rows[0]["Fg_Aplicar_Pct"].ToString()) == false)
                {
                    DescountFlag = false;
                    if (ProgramDestDt.Rows[0]["Mt_Descuento"] != null)
                    {
                        creditDesEntity.Mt_Descuento = decimal.Parse(ProgramDestDt.Rows[0]["Mt_Descuento"].ToString());
                    }
                }
                else if (ProgramDestDt.Rows[0]["Fg_Aplicar_Pct"] != null && Boolean.Parse(ProgramDestDt.Rows[0]["Fg_Aplicar_Pct"].ToString()) == true)
                {
                    DescountFlag = true;
                    if (!string.IsNullOrEmpty(ProgramDestDt.Rows[0]["Mt_Descuento"].ToString()))
                    {
                        creditDesEntity.Mt_Descuento = decimal.Parse(ProgramDestDt.Rows[0]["Mt_Descuento"].ToString()) / 100;
                    }
                    else
                    {
                        creditDesEntity.Mt_Descuento = 0;
                    }
                }
                else
                {
                    creditDesEntity.Mt_Descuento = 0;
                }
                Ddescount = creditDesEntity.Mt_Descuento;
            }
            #endregion

            #region"K_CREDITO_COSTO"
            creditCostEntity = new K_CREDITO_COSTOEntity();
            //creditCostEntity.No_Credito = 0;
            creditCostEntity.Dt_Credito_Costo = DateTime.Now.Date;
            DataTable ProgramCostDt = K_PROGRAMA_COSTOBLL.ClassInstance.Get_All_K_PROGRAMA_COSTO(Global.PROGRAM.ToString());//Changed by Jerry 2011/08/08
            if (ProgramCostDt != null && ProgramCostDt.Rows.Count > 0)
            {
                if (ProgramCostDt.Rows[0]["Fg_Aplicar_Pct"] != null && Boolean.Parse(ProgramCostDt.Rows[0]["Fg_Aplicar_Pct"].ToString()) == false)
                {
                    CostFlag = false;
                    if (ProgramCostDt.Rows[0]["Mt_Costo"] != null)
                    {
                        creditCostEntity.Mt_Costo = decimal.Parse(ProgramCostDt.Rows[0]["Mt_Costo"].ToString());
                    }
                }
                else if (ProgramCostDt.Rows[0]["Fg_Aplicar_Pct"] != null && Boolean.Parse(ProgramCostDt.Rows[0]["Fg_Aplicar_Pct"].ToString()) == true)
                {
                    CostFlag = true;
                    if (!string.IsNullOrEmpty(ProgramCostDt.Rows[0]["Mt_Costo"].ToString()))
                    {
                        creditCostEntity.Mt_Costo = decimal.Parse(ProgramCostDt.Rows[0]["Mt_Costo"].ToString()) / 100;
                    }
                    else
                    {
                        creditCostEntity.Mt_Costo = 0;
                    }
                }
                else
                {
                    creditCostEntity.Mt_Costo = 0;
                }
                DCost = creditCostEntity.Mt_Costo;
            }
            #endregion
            //End add

            // RSA 20130815 Enable edition, Bind data after setting cost and discount flags
            BindDataGirdViewData("P");
        }
        /// <summary>
        /// page load page3 data
        /// </summary>
        private void InitStepThreeData()
        {
            lblDate3.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtDx_razon_social4.Text = txtName.Text + " " + txtLastname.Text + " " + txtMotherName.Text;
            txtDx_tipo_industria4.Text = ddlDX_TIPO_INDUSTRIA.SelectedItem.Text;

            if (txtTotal.Text == "")
            {
                txtMt_monto_solicitado.Text = "0";
            }
            else
            {
                txtMt_monto_solicitado.Text = txtTotal.Text;
            }
            
            //ddlDx_periodo_pago.SelectedValue = "1";//1---Mensual ;2--Bimestral
            if (ProgramDt.Rows.Count > 0 && ProgramDt.Rows[0]["No_Plazo"] != null)
            {
                if (ddlDx_periodo_pago.SelectedValue == "1")
                {
                    txtNo_Plazo_Pago.Text = (int.Parse(ProgramDt.Rows[0]["No_Plazo"].ToString()) * 12).ToString();
                }
                else if (ddlDx_periodo_pago.SelectedValue == "2")
                {
                    txtNo_Plazo_Pago.Text = (int.Parse(ProgramDt.Rows[0]["No_Plazo"].ToString()) * 6).ToString();
                }
            }
        }
        /// <summary>
        /// initial Generate Quota GridView
        /// </summary>
        private void BindDataGirdViewData(string Flag)
        {
            GridViewDataSource = new DataTable();
            
            GridViewDataSource.Columns.Add("Technology", Type.GetType("System.String"));
            //edit by coco 20110823
            GridViewDataSource.Columns.Add("TypeProduct", Type.GetType("System.String"));
            //end edit
            GridViewDataSource.Columns.Add("Marca", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("Modelo", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("Cantidad", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("PrecioUnitario", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("Subtotal", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("Capacidad", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("Gastos", Type.GetType("System.String"));

            DataTable CreditProductDt = new DataTable();

            if (Flag == "P")
            {
                CreditProductDt = K_CREDITO_PRODUCTODal.ClassInstance.get_K_Credit_ProductByCreditNo(CreditNumber);
            }
            else if (Flag == "C")
            {
                CreditProductDt = K_CREDITO_PRODUCTODal.ClassInstance.get_K_Credit_ProductByCreditNo("0");
            }

            // Update by Tina 2011/08/17
            if (CreditProductDt.Rows.Count == 0)
            {
                //CreditProductDt.Rows.Add(CreditProductDt.NewRow());
                GridViewDataSource.Rows.Add(GridViewDataSource.NewRow());
            }
            else
            {
                DataRow row;
                foreach (DataRow dataRow in CreditProductDt.Rows)
                {
                    row = GridViewDataSource.NewRow();

                    row["Technology"] = dataRow["Technology"] != DBNull.Value ? dataRow["Technology"].ToString() : "";
                    row["TypeProduct"] = dataRow["TypeProduct"] != DBNull.Value ? dataRow["TypeProduct"].ToString() : "";//edit by coco 20110823
                    row["Marca"] = dataRow["Marca"] != DBNull.Value ? dataRow["Marca"].ToString() : "";
                    row["Capacidad"] = dataRow["Capacidad"] != DBNull.Value ? dataRow["Capacidad"].ToString() : "";

                    row["Modelo"] = dataRow["Modelo"] != DBNull.Value ? dataRow["Modelo"].ToString() : "";
                    row["Cantidad"] = dataRow["Cantidad"] != DBNull.Value ? dataRow["Cantidad"].ToString() : "";
                    row["PrecioUnitario"] = dataRow["PrecioUnitario"] != DBNull.Value ? dataRow["PrecioUnitario"].ToString() : "";
                    row["Subtotal"] = dataRow["Subtotal"] != DBNull.Value ? dataRow["Subtotal"].ToString() : "";
                    row["Gastos"] = dataRow["Gastos"] != DBNull.Value ? dataRow["Gastos"].ToString() : "0";
                    GridViewDataSource.Rows.Add(row);
                }
            }

            gvTecPro.DataSource = GridViewDataSource;
            gvTecPro.DataBind();
        }
        /// <summary>
        /// Is not numeric
        /// </summary>
        /// <param name="strTemp"></param>
        /// <returns></returns>
        private Boolean IsNumeric(string strTemp)
        {
            try
            {
                if (decimal.Parse(strTemp) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Is valid Precio
        /// </summary>
        /// <param name="oldPrice"></param>
        /// <param name="newPrice"></param>
        /// <returns></returns>
        private Boolean IsValidPrecio(string oldPrice, string newPrice)
        {
            double dOld;
            double dNew;

            // validate it can be parsed a number, integer (equal to it self with decimals truncated), in range [0, 20]
            // ignore a difference of less than a cent
            return double.TryParse(newPrice, out dNew) && dNew > 0
                && double.TryParse(oldPrice, out dOld) && dOld + .001 > dNew;
        }
        /// <summary>
        /// Is valid Gastos
        /// </summary>
        /// <param name="importe"></param>
        /// <param name="gastos"></param>
        /// <returns></returns>
        private Boolean IsValidGastos(string subTotal, string gastos)
        {
            double dSubTotal;
            double dGastos;
            //FRR change in case txt in null
            if (gastos.Equals(""))
            {
                gastos = "0";
            }


            // validate it can be parsed a number, integer (equal to it self with decimals truncated), in range [0, 20]
            return double.TryParse(subTotal, out dSubTotal)
                && double.TryParse(gastos, out dGastos)
                && dGastos >= 0
                && dGastos <= gastosPercentage * dSubTotal;
        }
        /// <summary>
        /// Get auxiliar data
        /// </summary>
        /// <returns></returns>
        private CAT_AUXILIAREntity GetCatAuxiliarData()
        {
            CAT_AUXILIAREntity auxiliarModel = new CAT_AUXILIAREntity();

            // Comment by Tina 2011/08/09
            //if (string.Compare(ddlDX_TIPO_SOCIEDAD.SelectedItem.Text, "PERSONA FISICa", true) == 0)
            //{
            //    auxiliarModel.No_Credito = 0;
            //}
            //else
            //{
            //    auxiliarModel.No_Credito = 1;
            //}
            // End
            if (txtBirthDate.Text == "")
            {
                auxiliarModel.Dt_Nacimiento_Fecha = DBNull.Value;
            }
            else
            {
                auxiliarModel.Dt_Nacimiento_Fecha = txtBirthDate.Text;
            }
            auxiliarModel.Dx_Nombres = txtName.Text;
            auxiliarModel.Dx_Apellido_Paterno = txtLastname.Text;
            auxiliarModel.Dx_Apellido_Materno = txtMotherName.Text;
            //auxiliarModel.Dx_Ciudad = txtCiudad.Text;
            //auxiliarModel.Dx_Numero_Interior = txtInterior.Text;

            return auxiliarModel;
        }
        /// <summary>
        /// return a value with just two decimals
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private double TwoDecimals(double amount)
        {
            // take care of .99999999 doing a fist rounding with more decimals than needed
            double round = Math.Round(amount, 4);
            // now truncate to consider just 2 decimals
            double truncate = Math.Truncate(round * 10000) / 10000;

            return truncate;
        }
        /// <summary>
        /// Calculate the total value
        /// </summary>
        private void AccountTotal()
        {
            decimal AcountTotal = 0;
            creditCostEntity.Mt_Costo = 0;
            K_TECNOLOGIA_MATERIALDAL ktm = new K_TECNOLOGIA_MATERIALDAL();
            decimal gastosTotal = 0;
            if (Pct_Tasa_IVA == 0)
            {
                GetPctTasaIVA();
            }

            creditDesEntity.Mt_Descuento = 0;
            creditCostEntity.Mt_Costo = 0;
            for (int i = 0; i < gvTecPro.Rows.Count; i++)
            {
                HiddenField hfExactSubtotal = gvTecPro.Rows[i].FindControl("hfExactSubtotal") as HiddenField;
                TextBox txtCantidad = gvTecPro.Rows[i].FindControl("txtCantidad") as TextBox;
                TextBox txtGastos = gvTecPro.Rows[i].FindControl("txtGastos") as TextBox;
                DropDownList ddlCapacidad = gvTecPro.Rows[i].FindControl("ddlCapacidad") as DropDownList;
                DropDownList ddlTech = gvTecPro.Rows[i].FindControl("ddlTecnolog") as DropDownList;

                decimal deTotal = 0;
                if (!hfExactSubtotal.Value.Equals(""))
                {
                    deTotal = (decimal)TwoDecimals(double.Parse(hfExactSubtotal.Value) * (1 + Pct_Tasa_IVA / 100));  // +IVA

                    // 0 means no value for this technology
                    if (ktm.GetMaterialMaxOrderByTechnology(Convert.ToInt32(ddlTech.SelectedValue)) > 0)
                    {
                        if (DescountFlag)
                        {
                            creditDesEntity.Mt_Descuento += (decimal)TwoDecimals((double)Ddescount * (double)deTotal);
                        }

                        // cost <=> ddlCapacidad disabled
                        if (!txtCantidad.Text.Equals("") && !ddlCapacidad.Enabled && !CostFlag)
                            creditCostEntity.Mt_Costo += decimal.Parse(txtCantidad.Text) * DCost;
                    }
                }

                AcountTotal = AcountTotal + deTotal;

                // RSA 20130826 We need to validate the technologies with expenses in order to decide if
                // expenses ar valid or not
                if (ddlTech != null && ddlTech.SelectedIndex > 0
                    && CveGasto[ddlTech.SelectedValue] == 1
                    && !txtGastos.Text.Equals(""))
                {
                    gastosTotal += (decimal)TwoDecimals(double.Parse(txtGastos.Text) * (1 + Pct_Tasa_IVA / 100));  // +IVA
                }
            }

            if (AcountTotal.Equals(0))
            {
                this.txtSubTotal.Text = "";
                this.txtTotalCost.Text = "";
                this.txtTotalDescount.Text = "";
                txtTotal.Text = "0";
                //added by tina 2012-07-26
                txtIVA.Text = "";
                txtCostEquipo.Text = "";
                txtDescount.Text = "";
                //end
            }
            else
            {
                // RSA add expenses and remove IVA
                decimal costEquipo = AcountTotal + gastosTotal;
                AcountTotal = (AcountTotal + gastosTotal) / (1+ Convert.ToDecimal(Pct_Tasa_IVA) / 100);

                //add by coco 2011-08-05
                /*
                if (DescountFlag)
                {
                    creditDesEntity.Mt_Descuento = Ddescount * (AcountTotal + (AcountTotal * Convert.ToDecimal(Pct_Tasa_IVA) / 100));
                }
                if (CostFlag)
                {
                    creditCostEntity.Mt_Costo = DCost * AcountTotal;
                }
                */

                this.txtSubTotal.Text = TwoDecimals((double)AcountTotal).ToString("c");
                this.txtTotalCost.Text = creditCostEntity.Mt_Costo.ToString("c");
                this.txtTotalDescount.Text = creditDesEntity.Mt_Descuento.ToString("c");
                txtIVA.Text = (costEquipo - (decimal)TwoDecimals((double)AcountTotal)).ToString("c");
                txtCostEquipo.Text = costEquipo.ToString("c");
                txtDescount.Text = (creditDesEntity.Mt_Descuento - creditCostEntity.Mt_Costo).ToString("c");
                txtTotal.Text = (AcountTotal + AcountTotal * Convert.ToDecimal(Pct_Tasa_IVA) / 100 - (creditDesEntity.Mt_Descuento - creditCostEntity.Mt_Costo)).ToString("c");
                Session["monto_solicitado"] = Math.Round(AcountTotal + AcountTotal * Convert.ToDecimal(Pct_Tasa_IVA) / 100 - (creditDesEntity.Mt_Descuento - creditCostEntity.Mt_Costo), 2);
                // FRR 20130923 Negative discount
                if ((creditDesEntity.Mt_Descuento - creditCostEntity.Mt_Costo)<0)
                {
                //    wizardPages.ActiveStepIndex = 2;

                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "NegativeDiscount") as string;
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                }
                
                
                //end
            }
            //end add
        }

        //add by coco 20110901
        /// <summary>
        /// Get supplierID by supplier_branchID
        /// </summary>
        private void InitDepartmentProperty()
        {
            if (Session["UserInfo"] != null)
            {
                US_USUARIOModel User = (US_USUARIOModel)Session["UserInfo"];

                if (User.Tipo_Usuario == GlobalVar.SUPPLIER_BRANCH)
                {
                    CAT_PROVEEDORModel Model = CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORByBranchID(User.Id_Departamento.ToString());
                    idDepartment = Model.Id_Proveedor.ToString();
                }
                else
                {
                    idDepartment = User.Id_Departamento.ToString();
                }
            }
        }
        //end add

        //added by tina 2012-07-26
        private bool IsAllProductDataEntered(out string strMsg)
        {
            bool result = true;
            strMsg = string.Empty;
            foreach (GridViewRow row in gvTecPro.Rows)
            {
                DropDownList ddlTechnology = row.FindControl("ddlTecnolog") as DropDownList;
                DropDownList ddlTypeOfProduct = row.FindControl("ddlTypeOfProduct") as DropDownList;
                DropDownList ddlModel = row.FindControl("ddlProduct") as DropDownList;
                DropDownList ddlMarca = row.FindControl("ddlMarca") as DropDownList;
                //DropDownList ddlCapacidad = row.FindControl("ddlCapacidad") as DropDownList;
                TextBox txtCantidad = row.FindControl("txtCantidad") as TextBox;
                HiddenField hfExactSubtotal = row.FindControl("hfExactSubtotal") as HiddenField;
                HiddenField hfExactGridText1 = row.FindControl("hfExactGridText1") as HiddenField;
                HiddenField hfBasePrice = row.FindControl("hfBasePrice") as HiddenField;
                TextBox txtGastos = row.FindControl("txtGastos") as TextBox;

                if (ddlTechnology.SelectedIndex <= 0)
                {
                    strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "PleaseSelectProduct") as string;
                    result = false;
                    break;
                }
                if (ddlTypeOfProduct.SelectedIndex <= 0)
                {
                    strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "PleaseSelectProduct") as string;
                    result = false;
                    break;
                }
                if (ddlModel.SelectedIndex <= 0)
                {
                    strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "PleaseSelectProduct") as string;
                    result = false;
                    break;
                }
                if (ddlMarca.SelectedIndex <= 0)
                {
                    strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "PleaseSelectProduct") as string;
                    result = false;
                    break;
                }
                //if (ddlCapacidad.SelectedIndex <= 0 )
                //{
                //    result = false;
                //    break;
                //}
                if (txtCantidad.Text.Trim() == "" || !IsNumeric(txtCantidad.Text.Trim()))
                {
                    strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "DataFormatIncorrect") as string;
                    result = false;
                    break;
                }
                if (txtGastos.Visible && !IsValidGastos(hfExactSubtotal.Value, txtGastos.Text))
                {
                    double dSubtotal = 0;
                    double.TryParse(hfExactSubtotal.Value, out dSubtotal);

                    strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "DataGastosIncorrect") as string;
                    strMsg = string.Format(strMsg, (Math.Floor(gastosPercentage * dSubtotal * 100) / 100).ToString("n"));
                    result = false;
                    break;
                }
                if (!IsValidPrecio(hfBasePrice.Value, hfExactGridText1.Value))
                {
                    double precio = 0;
                    double.TryParse(hfBasePrice.Value, out precio);

                    strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "DataPrecioIncorrect") as string;
                    strMsg = string.Format(strMsg, (Math.Floor(precio * 100) / 100).ToString("n"));
                    result = false;
                    break;
                }
            }
            return result;
        }

        private void GetPctTasaIVA()
        {
            if (TipoUser == GlobalVar.SUPPLIER)
            {
                CAT_PROVEEDORModel ProModel = new CAT_PROVEEDORModel();
                ProModel = CAT_PROVEEDORBll.ClassInstance.Get_CAT_PROVEEDORByPKID(idDepartment);

                Pct_Tasa_IVA = float.Parse(ProModel.Pct_Tasa_IVA.ToString());
            }
            else if (TipoUser == GlobalVar.SUPPLIER_BRANCH)
            {
                if (Session["UserInfo"] != null)
                {
                    US_USUARIOModel User = (US_USUARIOModel)Session["UserInfo"];

                    DataTable dtSupplierBranch = SupplierBrancheDal.ClassInstance.GetSupplierBranch(User.Id_Departamento);
                    if (dtSupplierBranch != null && dtSupplierBranch.Rows.Count > 0)
                    {
                        Pct_Tasa_IVA = float.Parse(dtSupplierBranch.Rows[0]["Pct_Tasa_IVA"].ToString());
                    }
                }
            }
        }
        //end

        /// <summary>
        /// Check if we are editing an existing credit or not
        /// </summary>
        /// <returns>true if it is editing an existing credit</returns>
        private bool IsEditingCredit()
        {
            return !string.IsNullOrEmpty(CreditNumber);
        }
        #endregion

        #region Cacel Actions
        protected void BtnCacel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditMonitor.aspx");
        }
        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditMonitor.aspx");
        }
        protected void btnCancel3_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditMonitor.aspx");
        }
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreditMonitor.aspx");
        }
        #endregion

        #region Print Functions
        protected void btn41_Click(object sender, EventArgs e)
        {
            if (Tipo_Sociedad != (int)CompanyType.PERSONAFISICA
                /*&& Tipo_Sociedad != (int)CompanyType.PERSONAFISICACONACTIVIDADEMPRESARIAL*/)  // RSA persona moral
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=AutorizacionConsultaBuroPM&CreditNumber=" + CreditNo + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=AutorizacionConsulta Buro&CreditNumber=" + CreditNo + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

        }

        protected void btn42_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Presupuesto de Inversion&CreditNumber=" + CreditNo + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btn43_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Tabla de Amortizacion&CreditNumber=" + CreditNo + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayRequest_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Solicitud de Credito&CreditNumber=" + CreditNo + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        #endregion
    }
}
