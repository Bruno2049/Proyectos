using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using PAEEEM.BussinessLayer;
using PAEEEM.Helpers;

namespace PAEEEM
{
    public partial class CreditReview : System.Web.UI.Page
    {
        #region Global Variables
        /// <summary>
        /// property
        /// </summary>
        public string Creditno
        {
            get
            {
                return ViewState["Creditno"] == null ? "" : ViewState["Creditno"].ToString();//Changed by Jerry 2011/08/12
            }
            set
            {
                ViewState["Creditno"] = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
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
        public Double strPct_Tasa_IVA
        {
            get
            {
                return ViewState["strPct_Tasa_IVA"] == null ? 0.0 : Double.Parse(ViewState["strPct_Tasa_IVA"].ToString());
            }
            set
            {
                ViewState["strPct_Tasa_IVA"] = value;
            }
        }
        public Double Pct_CAT
        {
            get
            {
                return ViewState["Pct_CAT"] == null ? 0.0 : Double.Parse(ViewState["Pct_CAT"].ToString());
            }
            set
            {
                ViewState["Pct_CAT"] = value;
            }
        }
        public Double Pct_Tasa_Interes
        {
            get
            {
                return ViewState["Pct_Tasa_Interes"] == null ? 0.0 : Double.Parse(ViewState["Pct_Tasa_Interes"].ToString());
            }
            set
            {
                ViewState["Pct_Tasa_Interes"] = value;
            }
        }
        public Double Pct_Tasa_Fija
        {
            get
            {
                return ViewState["Pct_Tasa_Fija"] == null ? 0.0 : Double.Parse(ViewState["Pct_Tasa_Fija"].ToString());
            }
            set
            {
                ViewState["Pct_Tasa_Fija"] = value;
            }
        }
        public Dictionary<string, string> Dics
        {
            get
            {
                return ViewState["Dics"] == null ? new Dictionary<string, string>() : (Dictionary<string, string>)ViewState["Dics"];
            }
            set
            {
                ViewState["Dics"] = value;
            }
        }
        public string StatusFlag
        {
            get
            {
                return ViewState["StatusFlag"] == null ? "" : ViewState["StatusFlag"].ToString();
            }
            set
            {
                ViewState["StatusFlag"] = value;
            }
        }
        public DataTable ProgramDt
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
        public string No_consumo_promedio
        {
            get
            {
                return ViewState["No_consumo_promedio"] == null ? "0" : ViewState["No_consumo_promedio"].ToString();
            }
            set
            {
                ViewState["No_consumo_promedio"] = value;
            }
        }
        // Add by tina 2011/08/10
        public int StatusID
        {
            get
            {
                return ViewState["StatusID"] == null ? 1 : int.Parse(ViewState["StatusID"].ToString());
            }
            set
            {
                ViewState["StatusID"] = value;
            }
        }
        // End
        //add coco 2011-08-03
        //const string ProGramID = "1";
        //end add
        //add by coco 2011-08-08
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
        //add by coco 20110824
        private string TechnologyList
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
        //end add

        //added by tina 2012-07-26
        private decimal SubTotal
        {
            get
            {
                return ViewState["SubTotal"] == null ? 0 : decimal.Parse(ViewState["SubTotal"].ToString());
            }
            set
            {
                ViewState["SubTotal"] = value;
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

        // private string mop { get ; set; }
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

        #region Page Init Methods

        /// <summary>
        /// Init default controls when page first load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    if (Session["UserInfo"] == null)
                    {
                        Response.Redirect("../Login/Login.aspx");
                        return;
                    }
                    //add by coco 2011-08-11  Get all CAT_DELEG_MUNICIPIO
                    DataTable dtDelegMunicipio = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_All_CAT_DELEG_MUNICIPIO();
                    HttpContext.Current.Cache.Insert("DeletMunicipio", dtDelegMunicipio);
                    //end add
                    //Init current department id
                    InitDepartmentProperty();
                    //Load current credit data for editing
                    InitCurrentEditData();
                    //Init controls status by credit status
                    InitControlStatus();
                    // Init Persona Moral
                    InitPersonaMoral();

                    txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof (Page), "PageLoad",
                    "alert('" + ex.Message + "');", true);
            }
        }

        private void InitPersonaMoral()
        {
            try
            {
                bool view = (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Value) != CompanyType.MORAL;
                // lblBirthDate.Visible = view;
                // txtBirthDate.Visible = view;
                if (view)
                {
                    lblBirthDate.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelBirthDateInmoral") as string;
                    lblName.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelPersonaNombre") as string;
                }
                else
                {
                    lblBirthDate.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelBirthDateMoral") as string;
                    lblName.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelPersonaRazon") as string;
                }

                lblLastname.Visible = view;
                txtLastname.Visible = view;
                lblMotherName.Visible = view;
                txtMotherName.Visible = view;
                lblSexo.Visible = view;
                rblSex.Visible = view;
                lblRegimenMatri.Visible = view;
                ddlRegimenMatri.Visible = view;
                lblEstadoCivil.Visible = view;
                rblEstadoCivil.Visible = view;
            }
            catch {
                throw;
            }
        }



        private void InitControlStatus()
        {
            int statusid = 0;
            if (Request["statusid"] != null)
            {
                int.TryParse(Request["statusid"], out statusid);
                // Add by Tina 2011/08/10
                StatusID = statusid;

                // End
                if (statusid > 0)
                {
                    //Changed by Jerry 2012/04/24 Change Operator "||" to "&&"
                    if (statusid != (int)PAEEEM.Helpers.CreditStatus.PENDIENTE && statusid != (int)PAEEEM.Helpers.CreditStatus.BENEFICIARIO_CON_ADEUDOS &&
                        statusid != (int)PAEEEM.Helpers.CreditStatus.TARIFA_FUERA_DE_PROGRAMA && statusid != (int)PAEEEM.Helpers.CreditStatus.Calificación_MOP_no_válida)
                    {
                        EnControl(false);
                        // Add by Tina 2011/08/09
                        btnCreditHistoryReview.Visible = true;
                        // End
                    }
                    //// RSA 20130813 Enable editing
                    //else if (statusid == (int)PAEEEM.Helpers.CreditStatus.PENDIENTE && string.IsNullOrEmpty(mop))
                    //{
                    //    EnControl(true);

                    //    btnCreditHistoryReview.Visible = false;
                    //}
                    // Add by Tina 2011/08/09
                    else
                    {
                        //add by FRR
                        EnControl(false);

                        btnCreditHistoryReview.Visible = false;
                    }
                }
            }
        }

        private void InitCurrentEditData()
        {
            string CreditNumber;
            if (Request["creditno"] != null)
            {
                CreditNumber = Request["creditno"].ToString();
                Creditno = CreditNumber;//Changed by Jerry 2011/08/12
                txtCredito.Text = Creditno;
                DataTable dtCredit = K_CREDITODal.ClassInstance.GetCreditsReview(CreditNumber);

                    if (dtCredit != null && dtCredit.Rows.Count > 0)
                    {
                        ProgramDt = CAT_PROGRAMABLL.ClassInstance.Get_CAT_PROGRAMAbyPk(dtCredit.Rows[0]["ID_Prog_Proy"].ToString());
 //                       idDepartment = dtCredit.Rows[0]["Id_Proveedor"].ToString();  //Commented by jerry 2011/09/07
                        LoadCreditFromData(dtCredit);
//                    }
                }
            }
        }

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
                //updated by tina 2012-07-25
                else if (User.Tipo_Usuario == GlobalVar.SUPPLIER)
                {
                    idDepartment = User.Id_Departamento.ToString();
                }
                else if (User.Tipo_Usuario == GlobalVar.REGIONAL_OFFICE || User.Tipo_Usuario == GlobalVar.CENTRAL_OFFICE)
                {
                    if (Request["creditno"] != null)
                    {
                        DataTable dtCredit = K_CREDITODal.ClassInstance.GetCreditsReview(Request["creditno"].ToString());
                        if (dtCredit != null && dtCredit.Rows.Count > 0)
                        {
                            if (dtCredit.Rows[0]["Tipo_Usuario"].ToString() == GlobalVar.SUPPLIER)
                            {
                                idDepartment = dtCredit.Rows[0]["Id_Proveedor"].ToString();
                            }
                            else
                            {
                                CAT_PROVEEDORModel Model = CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORByBranchID(dtCredit.Rows[0]["Id_Proveedor"].ToString());
                                idDepartment = Model.Id_Proveedor.ToString();
                            }
                        }
                    }
                }
                //end
            }
        }

        private void EnControl(bool bEnabled)
        {
            txtName.Enabled = bEnabled;
            txtLastname.Enabled = bEnabled;
            txtMotherName.Enabled = bEnabled;
            ddlGiroEmpresa.Enabled = bEnabled;
            txtBirthDate.Enabled = bEnabled;
            //add by coco 2012-07-16
            txtTelephone.Enabled = bEnabled;
            txtEmail.Enabled = bEnabled;
            //end add
            //txtCiudad.Enabled = bEnabled;
            //txtInterior.Enabled = bEnabled;
            ddlPfisica.Enabled = bEnabled;
            txtCURP.Enabled = bEnabled;
            txtRFC.Enabled = bEnabled;
            txtRepresLegal.Enabled = bEnabled;
            ddlAcreditado.Enabled = bEnabled;
            rblSex.Enabled = bEnabled;

            txtRPU.Enabled = bEnabled;
            // RSA 20130813 Enable edition
            rblEstadoCivil_SelectedIndexChanged(null, null);
            rblEstadoCivil.Enabled = bEnabled;
            ddlRegimenMatri.Enabled = bEnabled;

            ddlTipoIdenti.Enabled = bEnabled;
            txtNumero.Enabled = bEnabled;
            txtPromedioMV.Enabled = bEnabled;
            txtTotalGastosMensual.Enabled = bEnabled;
            txtRLemail.Enabled = bEnabled;
            txtFiscalCalle.Enabled = bEnabled;
            txtFiscalNumero.Enabled = bEnabled;
            txtFiscalCP.Enabled = bEnabled;
            ddlFiscalEstado.Enabled = bEnabled;
            ddlFiscalDM.Enabled = bEnabled;
            ddlFiscalTipoPropie.Enabled = bEnabled;
            txtFiscalTele.Enabled = bEnabled;
            txtDx_Domicilio_Fisc_Colonia.Enabled = bEnabled;
            cbMismoFiscal.Enabled = bEnabled;

            txtDx_Domicilio_Neg_Colonia.Enabled = bEnabled;
            txtNegocioCalle.Enabled = bEnabled;
            txtNegocioNumero.Enabled = bEnabled;
            txtNegocioCP.Enabled = bEnabled;
            ddlNegocioEstado.Enabled = bEnabled;
            ddlNegocioDM.Enabled = bEnabled;
            ddlNegocioTipoPropie.Enabled = bEnabled;
            txtNegocioTele.Enabled = bEnabled;
            txtNobreAvalRS.Enabled = bEnabled;
            txtAvalCURP.Enabled = bEnabled;
            txtAvalTele.Enabled = bEnabled;
            rblAvalSexo.Enabled = bEnabled;

            txtDx_Domicilio_Aval_Colonia.Enabled = bEnabled;
            txtAvalCalle.Enabled = bEnabled;
            txtAvalNumero.Enabled = bEnabled;
            txtAvalCP.Enabled = bEnabled;
            ddlAvalEstado.Enabled = bEnabled;
            ddlAvalDM.Enabled = bEnabled;
            //txtAvalPMV.Enabled = bEnabled;
            //txtNo_RPU_aval.Enabled = bEnabled;
            //txtAvalTGM.Enabled = bEnabled;
            txtNobreCoAcreditadoRS.Enabled = bEnabled;
            txtCoAcreditadoCURP.Enabled = bEnabled;
            rblCoAcreditadoSexo.Enabled = bEnabled;
            txtCoAcreditadoTele.Enabled = bEnabled;
            txtDx_Domicilio_Coacreditado_Colonia.Enabled = bEnabled;
            txtCoAcreditadoCalle.Enabled = bEnabled;
            txtCoAcreditadoNumero.Enabled = bEnabled;
            txtCoAcreditadoCP.Enabled = bEnabled;
            ddlCoAcreditadoEstado.Enabled = bEnabled;
            ddlCoAcreditadoDM.Enabled = bEnabled;
            btnSave.Visible = bEnabled;

            gvTecPro.Enabled = bEnabled;
            GridBtn.Visible = bEnabled;
        }
        /// <summary>
        /// Init current credit
        /// </summary>
        /// <param name="dtCredit"></param>
        private void LoadCreditFromData(DataTable dtCredit)
        {
            LoadDDLData(dtCredit);

            #region"DATOS DE LA EMPRESA O PERSONA FISICA CON ACTIVIDAD EMPRESARIAL"
            strPct_Tasa_IVA = float.Parse(dtCredit.Rows[0]["Pct_Tasa_IVA"].ToString());
            Pct_CAT = float.Parse(dtCredit.Rows[0]["Pct_CAT"].ToString());
            Pct_Tasa_Interes = float.Parse(dtCredit.Rows[0]["Pct_Tasa_Interes"].ToString());
            Pct_Tasa_Fija = float.Parse(dtCredit.Rows[0]["Pct_Tasa_Fija"].ToString());

            // RSA 20130813 Enable editing
           // Maritza  DataTable dtCat_Auxilira = CAT_AUXILIARDal.ClassInstance.Get_CAT_AUXILIARByCreditNo(dtCredit.Rows[0]["No_Credito"].ToString());
            //if (dtCat_Auxilira != null && dtCat_Auxilira.Rows.Count > 0)
            //    mop = dtCat_Auxilira.Rows[0]["No_MOP"].ToString();

            //ddlGiroEmpresa 1          
            //ddlPfisica 1
            // RSA 20130813 Fixed redundant check for PersonaFisica
            if (CompanyType.PERSONAFISICA != (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Value)
                && CompanyType.REPECO != (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Value))
            {
                txtName.Text = dtCredit.Rows[0]["DX_RAZON_SOCIAL"].ToString();
                //DivEnable.Visible = false;
                //lblCiudad.Visible = false;
                //txtCiudad.Visible = false;
                //LblInterior.Visible = false;
                //txtInterior.Visible = false;

                // lblBirthDate.Visible = false;
                // txtBirthDate.Visible = false;
                // txtBirthDate.Text = "";
                lblBirthDate.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelBirthDateMoral") as string;
                lblName.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelPersonaRazon") as string;
                // Maritza if (dtCat_Auxilira.Rows.Count > 0)
                //{
                //    txtBirthDate.Text = string.Format("{0:yyyy-MM-dd}", dtCat_Auxilira.Rows[0]["Dt_Nacimiento_Fecha"]);
                //}

                lblLastname.Visible = false;
                txtLastname.Visible = false;
                txtLastname.Text = "";
                lblMotherName.Visible = false;
                txtMotherName.Visible = false;
                txtMotherName.Text = "";
                //add by coco 2012-07-16
                lblTelephone.Visible = false;
                txtTelephone.Visible = false;
                lblEmail.Visible = false;
                txtEmail.Visible = false;
                //end add
            }
            else
            {
                if (dtCredit.Rows.Count > 0)
                {
                    lblBirthDate.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelBirthDateInmoral") as string;
                    lblName.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelPersonaNombre") as string;

                    txtName.Text = dtCredit.Rows[0]["Dx_Nombres"].ToString();
                    txtLastname.Text = dtCredit.Rows[0]["Dx_Apellido_Paterno"].ToString();
                    txtMotherName.Text = dtCredit.Rows[0]["Dx_Apellido_Materno"].ToString();
                    txtBirthDate.Text = string.Format("{0:yyyy-MM-dd}", dtCredit.Rows[0]["Dt_Nacimiento_Fecha"]);//Changed by Jerry 2011/08/09
                   // txtCiudad.Text = dtCat_Auxilira.Rows[0]["Dx_Ciudad"].ToString();
                   // txtInterior.Text = dtCat_Auxilira.Rows[0]["Dx_Numero_Interior"].ToString();
                }
                txtCURP.Enabled = true;
                //add by coco 2012-07-16, cambiado por Halsoft
                //txtTelephone.Text = dtCredit.Rows[0]["ATB04"].ToString();
                //txtEmail.Text = dtCredit.Rows[0]["ATB05"].ToString();

                txtTelephone.Text = dtCredit.Rows[0]["telefono_local"].ToString();
                txtEmail.Text = dtCredit.Rows[0]["email"].ToString();
                //end add
            }
            txtBirthDate.Text = string.Format("{0:yyyy-MM-dd}", dtCredit.Rows[0]["Dt_Nacimiento_Fecha"]);
            txtRFC.Text = dtCredit.Rows[0]["DX_RFC"].ToString();
            txtCURP.Text = dtCredit.Rows[0]["DX_CURP"].ToString();
            txtRepresLegal.Text = dtCredit.Rows[0]["DX_NOMBRE_REPRE_LEGAL"].ToString();
            //ddlAcreditado 1
            //rblSex 1
            if (dtCredit.Rows[0]["FG_SEXO_REPRE_LEGAL"].ToString() != "")
                rblSex.SelectedValue = Convert.IsDBNull(dtCredit.Rows[0]["FG_SEXO_REPRE_LEGAL"]) || dtCredit.Rows[0]["FG_SEXO_REPRE_LEGAL"] == "" ? "1" : dtCredit.Rows[0]["FG_SEXO_REPRE_LEGAL"].ToString();
           
            txtRPU.Text = dtCredit.Rows[0]["NO_RPU"].ToString();//Se comento por que no  existe en la tabla
            //rblEstadoCivil 1
            rblEstadoCivil.SelectedValue = dtCredit.Rows[0]["FG_EDO_CIVIL_REPRE_LEGAL"].ToString();
            if (rblEstadoCivil.SelectedValue == "1")
            {
                ddlRegimenMatri.SelectedValue = "";
                ddlRegimenMatri.Enabled = false;
            }
            else
            {
                ddlRegimenMatri.SelectedValue = dtCredit.Rows[0]["Cl_Regimen_Conyugal"].ToString();
            }

            

            //ddlRegimenMatri 1
            //ddlTipoIdenti 1
            txtNumero.Text = dtCredit.Rows[0]["DX_NO_IDENTIFICACION_REPRE_LEGAL"].ToString();
            txtPromedioMV.Text =decimal.Parse(dtCredit.Rows[0]["MT_VENTAS_MES_EMPRESA"].ToString()).ToString("C");
            txtTotalGastosMensual.Text = decimal.Parse(dtCredit.Rows[0]["MT_GASTOS_MES_EMPRESA"].ToString()).ToString("C");
            txtRLemail.Text = dtCredit.Rows[0]["DX_EMAIL_REPRE_LEGAL"].ToString();
            //No_consumo_promedio
            No_consumo_promedio = dtCredit.Rows[0]["No_consumo_promedio"].ToString();
            #endregion

            #region"DOMICILIO FISCAL"
            txtFiscalCalle.Text = dtCredit.Rows[0]["DX_DOMICILIO_FISC_CALLE"].ToString();
            txtFiscalNumero.Text = dtCredit.Rows[0]["DX_DOMICILIO_FISC_NUM"].ToString();
            txtFiscalCP.Text = dtCredit.Rows[0]["DX_DOMICILIO_FISC_CP"].ToString();

            //ddlFiscalEstado 1
            //ddlFiscalDM 1
            //ddlFiscalTipoPropie 1

            txtFiscalTele.Text = dtCredit.Rows[0]["DX_TEL_FISC"].ToString();
            txtDx_Domicilio_Fisc_Colonia.Text = dtCredit.Rows[0]["Dx_Domicilio_Fisc_Colonia"].ToString();
            #endregion

            #region"DOMICILIO DEL NEGOCIO"
            //cbMismoFiscal
            //cbMismoFiscal.Checked = bool.Parse(dtCredit.Rows[0]["FG_MISMO_DOMICILIO"].ToString()) == true ? true : false;
            cbMismoFiscal.Checked = (dtCredit.Rows[0]["FG_MISMO_DOMICILIO"].ToString()) == "TRUE" ? true : false;
            txtNegocioCalle.Text = dtCredit.Rows[0]["DX_DOMICILIO_NEG_CALLE"].ToString();
            txtNegocioNumero.Text = dtCredit.Rows[0]["DX_DOMICILIO_NEG_NUM"].ToString();
            txtNegocioCP.Text = dtCredit.Rows[0]["DX_DOMICILIO_NEG_CP"].ToString();
            //ddlNegocioEstado 1
            //ddlNegocioDM 1
            //ddlNegocioTipoPropie 1
            txtNegocioTele.Text = dtCredit.Rows[0]["DX_TEL_NEG"].ToString();
            txtDx_Domicilio_Neg_Colonia.Text = dtCredit.Rows[0]["Dx_Domicilio_Neg_Colonia"].ToString();
            #endregion

            #region"DATOS DEL AVAL"
            txtNobreAvalRS.Text = dtCredit.Rows[0]["DX_NOMBRE_AVAL"].ToString();
            txtAvalCURP.Text = dtCredit.Rows[0]["DX_RFC_CURP_AVAL"].ToString();
            txtAvalTele.Text = dtCredit.Rows[0]["DX_TEL_AVAL"].ToString();
            //rblAvalSexo
            rblAvalSexo.SelectedValue = dtCredit.Rows[0]["FG_SEXO_AVAL"].ToString();
            txtAvalCalle.Text = dtCredit.Rows[0]["DX_DOMICILIO_AVAL_CALLE"].ToString();
            txtAvalNumero.Text = dtCredit.Rows[0]["DX_DOMICILIO_AVAL_NUM"].ToString();
            txtAvalCP.Text = dtCredit.Rows[0]["DX_DOMICILIO_AVAL_CP"].ToString();
            //ddlAvalEstado 1
            //ddlAvalDM 1
            //txtAvalPMV.Text = dtCredit.Rows[0]["MT_VENTAS_MES_AVAL"].ToString();
            //txtAvalTGM.Text = dtCredit.Rows[0]["MT_GASTOS_MES_AVAL"].ToString();
            //txtNo_RPU_aval.Text = dtCredit.Rows[0]["No_RPU_aval"].ToString();
            txtDx_Domicilio_Aval_Colonia.Text = dtCredit.Rows[0]["Dx_Domicilio_Aval_Colonia"].ToString();
            #endregion

            //#region"DATOS DEL CO-ACREDITADO"
             txtNobreCoAcreditadoRS.Text = dtCredit.Rows[0]["DX_NOMBRE_COACREDITADO"].ToString();
            //txtCoAcreditadoCURP.Text = dtCredit.Rows[0]["DX_RFC_CURP_COACREDITADO"].ToString();
            ////rblCoAcreditadoSexo
            //rblCoAcreditadoSexo.SelectedValue = dtCredit.Rows[0]["FG_SEXO_COACREDITADO"].ToString();
            //txtCoAcreditadoTele.Text = dtCredit.Rows[0]["DX_TELEFONO_COACREDITADO"].ToString();
            //txtCoAcreditadoCalle.Text = dtCredit.Rows[0]["DX_DOMICILIO_COACREDITADO_CALLE"].ToString();
            //txtCoAcreditadoNumero.Text = dtCredit.Rows[0]["DX_DOMICILIO_COACREDITADO_NUM"].ToString();
            //txtCoAcreditadoCP.Text = dtCredit.Rows[0]["DX_DOMICILIO_COACREDITADO_CP"].ToString();
            ////ddlCoAcreditadoEstado 1
            ////ddlCoAcreditadoDM 1
            //txtDx_Domicilio_Coacreditado_Colonia.Text = dtCredit.Rows[0]["Dx_Domicilio_Coacreditado_Colonia"].ToString();
            //#endregion

            if ((CompanyType.PERSONAFISICA == (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Value) || CompanyType.REPECO == (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Value))
                && rblEstadoCivil.SelectedValue == "2" && string.Compare(ddlRegimenMatri.SelectedItem.Text, "BIENES MANCOMUNADOS", true) == 0)
            {
                CO.Visible = true;
            }
            else
            {
                CO.Visible = false;
            }

            //check if enable CURP field
            if (CompanyType.PERSONAFISICA != (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Value) && CompanyType.REPECO != (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Value))
            {
                txtCURP.Enabled = false;
            }
            //Added by Coco 2011/09/05
            Decimal CostoAD=0, Descuento = 0;
            var res1 = K_CREDITO_COSTODal.ClassInstance.GetTotalCost(this.Creditno);
            CostoAD =(!String.IsNullOrEmpty(res1)) ? Convert.ToDecimal(res1) : 0;
            this.txtTotalCost.Text = CostoAD.ToString("c");

            var res2 = K_CREDITO_DESCUENTODal.ClassInstance.GetTotalDescount(this.Creditno);
            Descuento = (!String.IsNullOrEmpty(res2)) ? Convert.ToDecimal(res2) : 0; 
            //this.txtDescount.Text = Descuento.ToString("c");


            //txtTotal.Text = dtCredit.Rows[0]["Mt_Monto_Solicitado"].ToString();//comment by tina 2012-07-26
            decimal TotalCost, TotalDiscount, Total;
            TotalCost = string.IsNullOrEmpty(CostoAD.ToString()) ? default(decimal) : decimal.Parse(CostoAD.ToString());
            TotalDiscount = string.IsNullOrEmpty(Descuento.ToString()) ? default(decimal) : decimal.Parse(Descuento.ToString());
            
            //Total = string.IsNullOrEmpty(this.txtTotal.Text) ? default(decimal) : decimal.Parse(txtTotal.Text);//comment by tina 2012-07-26

            //this.txtSubTotal.Text = (Total - TotalCost + TotalDiscount).ToString();//comment by tina 2012-07-26
            txtNo_Plazo_Pago.Text = dtCredit.Rows[0]["No_Plazo_Pago"].ToString();

            DataGridviewBind("P");

            //added by tina 2012-07-26
            decimal pctIVA = 0;
            pctIVA = decimal.Parse(dtCredit.Rows[0]["Pct_Tasa_IVA"].ToString());
            decimal Gastos = dtCredit.Rows[0]["Mt_Gastos_Instalacion_Mano_Obra"] == DBNull.Value ? default(decimal) : decimal.Parse(dtCredit.Rows[0]["Mt_Gastos_Instalacion_Mano_Obra"].ToString());
            SubTotal = (SubTotal + Gastos); // (1 + Convert.ToDecimal(pctIVA) / 100); ooc
            
            this.txtSubTotal.Text = string.Format("{0:c}", Math.Round(SubTotal,2));
            this.txtIVA.Text = (SubTotal * pctIVA / 100).ToString("c");
            this.txtCostEquipo.Text = (SubTotal + SubTotal * pctIVA / 100).ToString("c");
          //  this.txtDescount.Text = (TotalDiscount - TotalCost).ToString("c");
           // this.txtDescount.Text = (Convert.ToDecimal(this.txtTotalCost.Text) - Convert.ToDecimal(TotalCost)).ToString("c);


            //this.txtTotalDescount.Text = (CostoAD+Descuento).ToString("c");
            this.txtTotalDescount.Text = Descuento.ToString("c");//(Descuento - CostoAD).ToString("c");
            this.txtDescount.Text = (Descuento - CostoAD).ToString("c");

           // this.txtTotalCost.Text = string.Format("{0:c}", TotalCost);
           // this.txtTotalDescount.Text = string.Format("{0:c}", TotalDiscount);

            Total = SubTotal + SubTotal * pctIVA / 100 - (TotalDiscount - TotalCost);
            //Total = SubTotal + SubTotal * pctIVA / 100 - Descuento;
            this.txtTotal.Text = (Total).ToString("C");

            //Total = string.IsNullOrEmpty(this.txtTotal.Text) ? default(decimal) : decimal.Parse(txtTotal.Text);
            //end

            //substitution Service Code
            //string TempStatusFlag;
            //string ErrorCode;
            //if (!ValidateServiceCode(txtRPU.Text, out ErrorCode, out TempStatusFlag))
            //{
            //    CO.Visible = false;
            //    panel.Visible = false;
            //    Page4.Visible = false;
            //}
            //StatusFlag = TempStatusFlag;
            StatusFlag = string.Empty;

            //Added by coco 2011-08-05
            #region"K_CREDITO_DESCUENTO"
            creditDesEntity = new K_CREDITO_DESCUENTOEntity();
            creditDesEntity.No_Credito = Creditno;
            creditDesEntity.Dt_Credito_Descuento = DateTime.Now.Date;

            DataTable ProgramDestDt = K_PROGRAMA_DESCUENTOBLL.ClassInstance.Get_All_K_PROGRAMA_DESCUENTO(Global.PROGRAM.ToString());
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
            creditCostEntity.No_Credito = Creditno;
            creditCostEntity.Dt_Credito_Costo = DateTime.Now.Date;
            DataTable ProgramCostDt = K_PROGRAMA_COSTOBLL.ClassInstance.Get_All_K_PROGRAMA_COSTO(Global.PROGRAM.ToString());
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
        }
        #endregion

        #region Load Drop Down List Data

        /// <summary>
        /// Load drop down list data
        /// </summary>
        /// <param name="dtCredit"></param>
        private void LoadDDLData(DataTable dtCredit)
        {
            try
            {

                //Dx_Periodo_Pago
                DataTable PeriodPago = CAT_PERIODO_PAGODal.ClassInstance.Get_ALL_CAT_PERIODO_PAGO();
                if (PeriodPago != null)
                {
                    ddlDx_periodo_pago.DataSource = PeriodPago;
                    ddlDx_periodo_pago.DataValueField = "Cve_Periodo_Pago";
                    ddlDx_periodo_pago.DataTextField = "Dx_Periodo_Pago";
                    ddlDx_periodo_pago.DataBind();
                    ddlDx_periodo_pago.Items.Insert(0, new ListItem("", "0"));
                    ddlDx_periodo_pago.SelectedValue = dtCredit.Rows[0]["Cve_Periodo_Pago"].ToString();
                }

                //ddlGiroEmpresa 
                DataTable Industria = CAT_TIPO_INDUSTRIADal.ClassInstance.Get_All_CAT_TIPO_INDUSTRIA();
                if (Industria != null && Industria.Rows.Count > 0)
                {
                    ddlGiroEmpresa.DataSource = Industria;
                    ddlGiroEmpresa.DataTextField = "DESCRIPCION_SCIAN";
                    ddlGiroEmpresa.DataValueField = "Cve_Tipo_Industria";
                    ddlGiroEmpresa.DataBind();
                    ddlGiroEmpresa.Items.Insert(0, new ListItem("", "0"));
                    ddlGiroEmpresa.SelectedValue = dtCredit.Rows[0]["CVE_TIPO_INDUSTRIA"].ToString();
                }

                //ddlPfisica
                DataTable dtSociType = CAT_TIPO_SOCIEDADDal.ClassInstance.Get_All_CAT_TIPO_SOCIEDAD();
                if (dtSociType != null && dtSociType.Rows.Count > 0)
                {
                    ddlPfisica.DataSource = dtSociType;
                    ddlPfisica.DataTextField = "Dx_Tipo_Sociedad";
                    ddlPfisica.DataValueField = "Cve_Tipo_Sociedad";
                    ddlPfisica.DataBind();
                    ddlPfisica.Items.Insert(0, new ListItem("", "0"));
                    ddlPfisica.SelectedValue = dtCredit.Rows[0]["CVE_TIPO_SOCIEDAD"].ToString();
                }

                //ddlAcreditado
                DataTable dtAcrditType = CAT_TIPO_ACREDITACIONDal.ClassInstance.Get_All_CAT_TIPO_ACREDITACION();
                if (dtAcrditType != null && dtAcrditType.Rows.Count > 0)
                {
                    ddlAcreditado.DataSource = dtAcrditType;
                    ddlAcreditado.DataTextField = "Dx_Acreditacion_Repre_Legal";
                    ddlAcreditado.DataValueField = "Cve_Acreditacion_Repre_legal";
                    ddlAcreditado.DataBind();
                    ddlAcreditado.Items.Insert(0, new ListItem("", "0"));
                    ddlAcreditado.SelectedValue = dtCredit.Rows[0]["CVE_ACREDITACION_REPRE_LEGAL"].ToString();
                }

                //ddlRegimenMatri
                DataTable dtRegimenMatri = CAT_REGIMEN_CONYUGALDal.ClassInstance.Get_All_CAT_REGIMEN_CONYUGAL();
                if (dtRegimenMatri != null && dtRegimenMatri.Rows.Count > 0)
                {
                    ddlRegimenMatri.DataSource = dtRegimenMatri;
                    ddlRegimenMatri.DataTextField = "Dx_Reg_Conyugal_Repre_legal";
                    ddlRegimenMatri.DataValueField = "Cve_Reg_Conyugal_Repre_legal";
                    ddlRegimenMatri.DataBind();
                    ddlRegimenMatri.Items.Insert(0, new ListItem("", "0"));
                    ddlRegimenMatri.SelectedValue = dtCredit.Rows[0]["CVE_REG_CONYUGAL_REPRE_LEGAL"].ToString();
                }

                //ddlTipoIdenti
                DataTable dtTipoIdenti = CAT_IDENTIFICACIONDal.ClassInstance.Get_All_CAT_IDENTIFICACION();
                if (dtTipoIdenti != null && dtTipoIdenti.Rows.Count > 0)
                {
                    ddlTipoIdenti.DataSource = dtTipoIdenti;
                    ddlTipoIdenti.DataTextField = "Dx_Identificacion_Repre_legal";
                    ddlTipoIdenti.DataValueField = "Cve_Identificacion_Repre_legal";
                    ddlTipoIdenti.DataBind();
                    ddlTipoIdenti.Items.Insert(0, new ListItem("", "0"));
                    ddlTipoIdenti.SelectedValue = dtCredit.Rows[0]["CVE_IDENTIFICACION_REPRE_LEGAL"].ToString();
                }

                //ddlFiscalEstado 
                DataTable dtEstado = CAT_ESTADODAL.ClassInstance.Get_All_CAT_ESTADO();
                if (dtEstado != null)
                {
                    //Init Fiscal Estado
                    ddlFiscalEstado.DataSource = dtEstado;
                    ddlFiscalEstado.DataTextField = "Dx_Nombre_Estado";
                    ddlFiscalEstado.DataValueField = "Cve_Estado";
                    ddlFiscalEstado.DataBind();
                    ddlFiscalEstado.Items.Insert(0, new ListItem("", "0"));
                    ddlFiscalEstado.SelectedValue = dtCredit.Rows[0]["CVE_ESTADO_FISC"].ToString();

                    //Init Negocio Estado
                    ddlNegocioEstado.DataSource = dtEstado;
                    ddlNegocioEstado.DataTextField = "Dx_Nombre_Estado";
                    ddlNegocioEstado.DataValueField = "Cve_Estado";
                    ddlNegocioEstado.DataBind();
                    ddlNegocioEstado.Items.Insert(0, new ListItem("", ""));
                    ddlNegocioEstado.SelectedValue = dtCredit.Rows[0]["CVE_ESTADO_NEG"].ToString();

                    //Init Aval Estado
                    ddlAvalEstado.DataSource = dtEstado;
                    ddlAvalEstado.DataTextField = "Dx_Nombre_Estado";
                    ddlAvalEstado.DataValueField = "Cve_Estado";
                    ddlAvalEstado.DataBind();
                    ddlAvalEstado.Items.Insert(0, new ListItem("", "0"));
                    ddlAvalEstado.SelectedValue = dtCredit.Rows[0]["CVE_ESTADO_AVAL"].ToString();

                    //Init Coacreditado Estado
                    //ddlCoAcreditadoEstado.DataSource = dtEstado;
                    //ddlCoAcreditadoEstado.DataTextField = "Dx_Nombre_Estado";
                    //ddlCoAcreditadoEstado.DataValueField = "Cve_Estado";
                    //ddlCoAcreditadoEstado.DataBind();
                    //ddlCoAcreditadoEstado.Items.Insert(0, new ListItem("", "0"));
                    //ddlCoAcreditadoEstado.SelectedValue = dtCredit.Rows[0]["CVE_ESTADO_COACREDITADO"].ToString();
                }

                //ddlFiscalDM
                BindddlFiscalDM();
                ddlFiscalDM.SelectedValue = dtCredit.Rows[0]["CVE_DELEG_MUNICIPIO_FISC"].ToString();

                //ddlNegocioDM
                BindddlNegocioDM();
                ddlNegocioDM.SelectedValue = dtCredit.Rows[0]["CVE_DELEG_MUNICIPIO_NEG"].ToString();

                //ddlAvalDM
                BindddlAvalDM();
                ddlAvalDM.SelectedValue = dtCredit.Rows[0]["CVE_DELEG_MUNICIPIO_AVAL"].ToString();

                //ddlCoAcreditadoDM
                //BindddlCoAcreditadoDM();
                //ddlCoAcreditadoDM.SelectedValue = dtCredit.Rows[0]["CVE_DELEG_MUNICIPIO_COACREDITADO"].ToString();

                //ddlFiscalTipoPropie
                DataTable dtFiscalTipoPropie = CAT_TIPO_PROPIEDADDal.ClassInstance.Get_All_CAT_TIPO_PROPIEDAD();
                if (dtFiscalTipoPropie != null && dtFiscalTipoPropie.Rows.Count > 0)
                {
                    ddlFiscalTipoPropie.DataSource = dtFiscalTipoPropie;
                    ddlFiscalTipoPropie.DataTextField = "Dx_Tipo_Propiedad";
                    ddlFiscalTipoPropie.DataValueField = "Cve_Tipo_Propiedad";
                    ddlFiscalTipoPropie.DataBind();
                    ddlFiscalTipoPropie.Items.Insert(0, new ListItem("", "0"));
                    ddlFiscalTipoPropie.SelectedValue = dtCredit.Rows[0]["CVE_TIPO_PROPIEDAD_FISC"].ToString();
                }

                //ddlNegocioTipoPropie
                DataTable dtNegocioTipoPropie = CAT_TIPO_PROPIEDADDal.ClassInstance.Get_All_CAT_TIPO_PROPIEDAD();
                if (dtNegocioTipoPropie != null && dtNegocioTipoPropie.Rows.Count > 0)
                {
                    ddlNegocioTipoPropie.DataSource = dtNegocioTipoPropie;
                    ddlNegocioTipoPropie.DataTextField = "Dx_Tipo_Propiedad";
                    ddlNegocioTipoPropie.DataValueField = "Cve_Tipo_Propiedad";
                    ddlNegocioTipoPropie.DataBind();
                    ddlNegocioTipoPropie.Items.Insert(0, new ListItem("", "0"));
                    ddlNegocioTipoPropie.SelectedValue = dtCredit.Rows[0]["CVE_TIPO_PROPIEDAD_NEG"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        /// <summary>
        ///    //ddlFiscalDM
        /// </summary>
        private void BindddlFiscalDM()
        {
            int Estado = -1;

            if (!ddlFiscalEstado.SelectedValue.Equals("") && !ddlFiscalEstado.SelectedValue.Equals("0"))
            {
                Estado = Convert.ToInt32(ddlFiscalEstado.SelectedValue);
            }

            DataTable dtDM = FilterDelegMunicipio(Estado); //CAT_DELEG_MUNICIPIODAL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Estado);
            if (dtDM != null && dtDM.Rows.Count > 0)
            {
                ddlFiscalDM.DataSource = dtDM;
                ddlFiscalDM.DataTextField = "Dx_Deleg_Municipio";
                ddlFiscalDM.DataValueField = "Cve_Deleg_Municipio";
                ddlFiscalDM.DataBind();
                ddlFiscalDM.Items.Insert(0, new ListItem("", "0"));
            }
        }
        /// <summary>
        ///  ddlNegocioDM 
        /// </summary>
        private void BindddlNegocioDM()
        {
            int Estado = -1;

            if (!ddlNegocioEstado.SelectedValue.Equals("") && !ddlNegocioEstado.SelectedValue.Equals("0"))
            {
                Estado = Convert.ToInt32(ddlNegocioEstado.SelectedValue);
            }

            DataTable dtDM1 = FilterDelegMunicipio(Estado); //CAT_DELEG_MUNICIPIODAL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Estado);
            if (dtDM1 != null && dtDM1.Rows.Count > 0)
            {
                ddlNegocioDM.DataSource = dtDM1;
                ddlNegocioDM.DataTextField = "Dx_Deleg_Municipio";
                ddlNegocioDM.DataValueField = "Cve_Deleg_Municipio";
                ddlNegocioDM.DataBind();
                ddlNegocioDM.Items.Insert(0, new ListItem("", "0"));

            }
        }
        /// <summary>
        /// ddlAvalDM
        /// </summary>
        private void BindddlAvalDM()
        {
            int Estado = -1;

            if (!ddlAvalEstado.SelectedValue.Equals("") && !ddlAvalEstado.SelectedValue.Equals("0"))
            {
                Estado = Convert.ToInt32(ddlAvalEstado.SelectedValue);
            }

            DataTable dtDM2 = FilterDelegMunicipio(Estado);//CAT_DELEG_MUNICIPIODAL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Estado);
            if (dtDM2 != null && dtDM2.Rows.Count > 0)
            {
                ddlAvalDM.DataSource = dtDM2;
                ddlAvalDM.DataTextField = "Dx_Deleg_Municipio";
                ddlAvalDM.DataValueField = "Cve_Deleg_Municipio";
                ddlAvalDM.DataBind();
                ddlAvalDM.Items.Insert(0, new ListItem("", "0"));
            }
        }
        /// <summary>
        /// ddlCoAcreditadoDM
        /// </summary>
        private void BindddlCoAcreditadoDM()
        {
            int iEsatdo = -1;

            if (!ddlCoAcreditadoEstado.SelectedValue.Equals("") && !ddlCoAcreditadoEstado.SelectedValue.Equals("0"))
            {
                iEsatdo = Convert.ToInt32(ddlCoAcreditadoEstado.SelectedValue);
            }

            DataTable dtDM3 = FilterDelegMunicipio(iEsatdo);//CAT_DELEG_MUNICIPIODAL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(iEsatdo);
            if (dtDM3 != null && dtDM3.Rows.Count > 0)
            {
                ddlCoAcreditadoDM.DataSource = dtDM3;
                ddlCoAcreditadoDM.DataTextField = "Dx_Deleg_Municipio";
                ddlCoAcreditadoDM.DataValueField = "Cve_Deleg_Municipio";
                ddlCoAcreditadoDM.DataBind();
                ddlCoAcreditadoDM.Items.Insert(0, new ListItem("", "0"));
            }
        }
        #endregion

        #region Button Actions
        /// <summary>
        /// Save changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //add by coco 20110905
                if (txtTotal.Text.Equals("") || txtTotal.Text.Equals("0"))
                {
                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "PleaseSelectProduct") as string;
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);                   
                    return;
                }
                //end add
                EntidadCredito EntidadCredito = GetDataFromUI();
                CAT_AUXILIAREntity catAuxiliar = new CAT_AUXILIAREntity();

                EntidadCredito.No_Credito = Creditno;

                // RSA 2012-10-19 Apply to REPECO too
                if (CompanyType.PERSONAFISICA == (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Text) || CompanyType.REPECO == (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Text))
                //if (string.Compare(ddlPfisica.SelectedItem.Text, "PERSONA FISICA", true) == 0)
                {
                    catAuxiliar.Dx_Nombres = txtName.Text;
                    catAuxiliar.Dx_Apellido_Paterno = txtLastname.Text;
                    catAuxiliar.Dx_Apellido_Materno = txtMotherName.Text;
                    //catAuxiliar.Dx_Ciudad = txtCiudad.Text;
                    //catAuxiliar.Dx_Numero_Interior = txtInterior.Text;
                    catAuxiliar.Dt_Nacimiento_Fecha = txtBirthDate.Text;
                    catAuxiliar.No_Credito = Creditno;
                }
                else
                {
                    catAuxiliar.No_Credito = "0";//Changed by Jerry 2011/08/12
                }

                if (StatusFlag.Equals("P"))
                {
                    if ((CompanyType.PERSONAFISICA == (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Text) || CompanyType.REPECO == (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Text)) 
                        && string.Compare(ddlRegimenMatri.SelectedItem.Text, "BIENES MANCOMUNADOS", true) == 0)
                    {
                        #region "DATOS DEL CO-ACREDITADO"
                        EntidadCredito.Dx_Nombre_Coacreditado = txtNobreCoAcreditadoRS.Text;
                        EntidadCredito.Dx_RFC_CURP_Coacreditado = txtCoAcreditadoCURP.Text;
                        int sex10 = 1;
                        int.TryParse(rblCoAcreditadoSexo.SelectedValue, out sex10);
                        EntidadCredito.Fg_Sexo_Coacreditado = sex10;

                        EntidadCredito.Dx_Telefono_Coacreditado = txtCoAcreditadoTele.Text;
                        EntidadCredito.Dx_Domicilio_Coacreditado_Calle = txtCoAcreditadoCalle.Text;
                        EntidadCredito.Dx_Domicilio_Coacreditado_Num = txtCoAcreditadoNumero.Text;
                        EntidadCredito.Dx_Domicilio_Coacreditado_CP = txtCoAcreditadoCP.Text;
                        EntidadCredito.Dx_Domicilio_Coacreditado_Colonia = txtDx_Domicilio_Coacreditado_Colonia.Text;
                        int coacreestado = 0;
                        int.TryParse(ddlCoAcreditadoEstado.SelectedValue, out coacreestado);
                        EntidadCredito.Cve_Estado_Coacreditado = coacreestado;
                        int coacredm = 0;
                        int.TryParse(ddlCoAcreditadoDM.SelectedValue, out coacredm);
                        EntidadCredito.Cve_Deleg_Municipio_Coacreditado = coacredm;
                        #endregion
                    }
                    //edit by coco 2011-08-04
                    double ProductCapacity = 0;
                    List<K_CREDITO_PRODUCTOEntity> CreditProduct = GetCreditProduct(out ProductCapacity);

                    decimal gastosTotal = 0;
                    foreach (K_CREDITO_PRODUCTOEntity model in CreditProduct)
                    {
                        gastosTotal += model.Mt_Gastos_Instalacion_Mano_Obra;
                    }
                    EntidadCredito.Mt_Gastos_Instalacion_Mano_Obra = (double)gastosTotal;

                    if (!string.IsNullOrEmpty(txtNo_Plazo_Pago.Text))
                    {
                        EntidadCredito.No_Plazo_Pago = Convert.ToInt32(txtNo_Plazo_Pago.Text);
                    }

                    if (!string.IsNullOrEmpty(ddlDx_periodo_pago.SelectedValue))
                    {
                        EntidadCredito.Cve_Periodo_Pago = Convert.ToInt32(ddlDx_periodo_pago.SelectedValue);
                    }
                    //end add

                    #region Calculation Fields
                    //Mt_Monto_Solicitado
                    EntidadCredito.Mt_Monto_Solicitado = (!string.IsNullOrEmpty(this.txtTotal.Text)) ? Convert.ToDouble(this.txtTotal.Text) : default(double);
                    //No_Ahorro_Energetico
                    double dbEffciencyProductSelect = ProductCapacity;//Changed by coco 2011-08-04

                    if (Dics["HighAvgConsumption"] != "")
                    {
                        EntidadCredito.No_Ahorro_Energetico = Convert.ToDouble(System.Math.Abs((double.Parse(Dics["HighAvgConsumption"]) - dbEffciencyProductSelect / 12)));
                    }
                    if (Dics["HighAvgConsumption"] != "")
                    {
                        EntidadCredito.No_consumo_promedio = float.Parse(Dics["HighAvgConsumption"]);//;                   
                    }

                    //No_Ahorro_Economico
                    double iAverageConsume = 0;
                    iAverageConsume = (double)EntidadCredito.No_consumo_promedio;
                    double originalConsumption = 0;
                    EntidadCredito.No_Ahorro_Economico = K_CREDITOBll.ClassInstance.CalculateEconomicConsumptionSavings(EntidadCredito.Cve_Estado_Neg, iAverageConsume, EntidadCredito.Mt_Monto_Solicitado, strPct_Tasa_IVA / 100, Dics["Rate"].ToString(), ref originalConsumption);                    //Mt_Capacidad_Pago

                    //Mt_Capacidad_Pago
                    EntidadCredito.Mt_Capacidad_Pago = K_CREDITOBll.ClassInstance.CalculatePaymentCapacity(EntidadCredito.No_Plazo_Pago, EntidadCredito.Mt_Ingreso_Neto_Mes_Empresa, EntidadCredito.No_Ahorro_Economico, Pct_CAT / 100, EntidadCredito.Cve_Periodo_Pago);
                    #endregion

                    EntidadCredito.Cve_Estatus_Credito = (int)CreditStatus.PENDIENTE;
                    //K_CREDITO_AMORTIZACION
                    DataTable CreditAmortizacionDt = K_CREDITOBll.ClassInstance.CalculateCreditAmortizacion(Creditno, EntidadCredito.Mt_Monto_Solicitado, Pct_Tasa_Fija / 100, EntidadCredito.No_Plazo_Pago, EntidadCredito.Cve_Periodo_Pago, Pct_Tasa_Interes / 100, strPct_Tasa_IVA / 100, Pct_CAT / 100, Dics["PeriodEndDate"]);
                    if (CreditAmortizacionDt != null)
                    {
                        double deTemp = 0;
                        for (int i = 0; i < CreditAmortizacionDt.Rows.Count; i++)
                        {
                            deTemp = deTemp + double.Parse(CreditAmortizacionDt.Rows[i]["Mt_Pago"].ToString());
                        }
                        EntidadCredito.Mt_Monto_Total_Pagar = deTemp;

                        if (EntidadCredito.Mt_Monto_Solicitado > EntidadCredito.Mt_Capacidad_Pago)
                        {
                            string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "QuoteGreaterThanPaymentCapacity") as string;
                            ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                        }
                        else
                        {
                            int result = K_CREDITOBll.ClassInstance.Update_CreditReview(EntidadCredito, CreditProduct, CreditAmortizacionDt, catAuxiliar, creditCostEntity, creditDesEntity);
                            string strMsg = string.Empty;
                            if (result > 0)
                            {
                                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgSaveSucessReview") as string;
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "savesucess", "alert('" + strMsg + "');", true);
                            }
                        }
                    }
                }
                else
                {
                    if (StatusFlag == "B")//Beneficiario con adeudos status
                    {
                        EntidadCredito.Cve_Estatus_Credito = (int)CreditStatus.BENEFICIARIO_CON_ADEUDOS;
                        EntidadCredito.Dt_Fecha_Beneficiario_con_adeudos = DateTime.Now.Date;
                    }
                    else if (StatusFlag == "A")//Tarifa fuera de programa status
                    {
                        EntidadCredito.Cve_Estatus_Credito = (int)CreditStatus.TARIFA_FUERA_DE_PROGRAMA;
                        EntidadCredito.Dt_Fecha_Tarifa_fuera_de_programa = DateTime.Now.Date;
                    }

                    int result = K_CREDITOBll.ClassInstance.Update_CreditReviewExceptProduct(EntidadCredito, catAuxiliar);

                    if (result > 0)
                    {
                        string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgSaveSucessReview") as string;
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "savesucess", "alert('" + strMsg + "');", true);
                        btnSave.Enabled = false;
                    }
                }
                // Add by Tina 2011/08/11
                StatusID = EntidadCredito.Cve_Estatus_Credito;
                if (StatusID == (int)CreditStatus.PENDIENTE)
                {
                    btnCreditHistoryReview.Visible = true;
                }
                else
                {
                    btnCreditHistoryReview.Visible = false;
                }
                // End
            }
            catch (Exception)
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgSaveFailReview") as string;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('" + strMsg + "');", true);
            }
        }

        private void Salir()
        {
            US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];

            // if (Request.QueryString["Flag"] != null && Request.QueryString["Flag"].ToString() == "M")
            if (UserModel.Id_Rol == 3)
            {
                Response.Redirect("../SupplierModule/CreditMonitor.aspx");
            }
            else if (UserModel.Id_Rol == 1 || UserModel.Id_Rol == 2 || UserModel.Id_Rol == 6)
            {
                Response.Redirect("../RegionalModule/CreditAuthorization.aspx");
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
        /// <summary>
        /// return to manager page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Salir();
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
                DataGridviewBind("C");
                txtTotal.Text = "0";
                this.txtSubTotal.Text = "0.0";
                this.txtTotalCost.Text = "0.0";
                this.txtTotalDescount.Text = "0.0";
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
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridViewDataSource.Rows.Count; i++)
            {
                DataRow OldRow = GridViewDataSource.Rows[i];

                DropDownList ddlTechnology = gvTecPro.Rows[i].FindControl("ddlTecnolog") as DropDownList;
                DropDownList DropDownTypeOfProduct = gvTecPro.Rows[i].FindControl("ddlTypeOfProduct") as DropDownList;//add by coco 20110823
                DropDownList ddlProduct = gvTecPro.Rows[i].FindControl("ddlProduct") as DropDownList;
                DropDownList ddlMarca = gvTecPro.Rows[i].FindControl("ddlMarca") as DropDownList;
                //TextBox txtModelo = gvTecPro.Rows[i].FindControl("txtModelo") as TextBox;
                TextBox txtCantidad = gvTecPro.Rows[i].FindControl("txtCantidad") as TextBox;
                TextBox txtGridText1 = gvTecPro.Rows[i].FindControl("txtGridText1") as TextBox;
                TextBox txtSubtotal = gvTecPro.Rows[i].FindControl("txtSubtotal") as TextBox;
                TextBox txtGastos = gvTecPro.Rows[i].FindControl("txtGastos") as TextBox;
                //add by coco 2011-08-04
                DropDownList ddlCapacidad = gvTecPro.Rows[i].FindControl("ddlCapacidad") as DropDownList;

                if (IsSE(ddlTechnology.SelectedValue))
                    OldRow["Technology"] = "";
                else
                    OldRow["Technology"] = ddlTechnology.SelectedIndex != -1 ? ddlTechnology.SelectedValue : "";
                OldRow["TypeProduct"] = DropDownTypeOfProduct.SelectedIndex != -1 ? DropDownTypeOfProduct.SelectedValue.ToString() : "";//edit by coco 20110823
                OldRow["Marca"] = ddlMarca.SelectedIndex != -1 ? ddlMarca.SelectedValue : "";
                //add by coco 2011-08-04
                OldRow["Capacidad"] = ddlCapacidad.SelectedIndex != -1 ? ddlCapacidad.SelectedValue : "";

                OldRow["Modelo"] = ddlProduct.SelectedIndex != -1 ? ddlProduct.SelectedValue.ToString() : "";//edit by coco 20110823
                OldRow["Cantidad"] = txtCantidad.Text;
                double value;
                OldRow["PrecioUnitario"] = double.TryParse(txtGridText1.Text, out value) ? (value * (1 + strPct_Tasa_IVA / 100)).ToString() : string.Empty;   // +IVA
                OldRow["Subtotal"] = double.TryParse(txtSubtotal.Text, out value) ? (value * (1 + strPct_Tasa_IVA / 100)).ToString() : string.Empty;          // +IVA
                OldRow["Gastos"] = double.TryParse(txtGastos.Text, out value) ? (value * (1 + strPct_Tasa_IVA / 100)).ToString() : string.Empty;              // +IVA
            }

            DataRow NewRow = GridViewDataSource.NewRow();
            GridViewDataSource.Rows.Add(NewRow);

            gvTecPro.DataSource = GridViewDataSource;
            gvTecPro.DataBind();
        }

        // Add by Tina 2011/08/09
        protected void btnCreditHistoryReview_Click(object sender, EventArgs e)
        {
            if (StatusID == (int)PAEEEM.Helpers.CreditStatus.PENDIENTE)
            {
                Response.Redirect("../SupplierModule/ValidateCrediticialHistory.aspx?CreditNumber=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(Creditno.ToString())).Replace("+", "%2B") + " &Flag=M");
            }
            else
            {
                string Flag = Request.QueryString["Flag"] != null ? Request.QueryString["Flag"] : "";
                Response.Redirect("../SupplierModule/CrediticialHistoryReview.aspx?CreditNumber=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(Creditno.ToString())).Replace("+", "%2B") + " &Flag=" + Flag);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validate service code
        /// </summary>
        /// <param name="strService"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        private Boolean ValidateServiceCode(string servicecode, out string ErrorCode, out string StatusFlag)
        {
            Boolean ServiceCodeValidate = false;
            StatusFlag = "";
            ErrorCode = "";
            if (IsServiceCodeLongEnough(servicecode) /*SICOMHelper.tempRpus.Keys.Contains<string>(servicecode)*/)
            {
                Dics = SICOMHelper.GetAttributes(servicecode);
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
                        if (ValidateUserStatus(Dics["UserStatus"]))
                        {
                            if (ValidateNoDebit(Dics["CurrentBillingStatus"], Dics["DueDate"]) && !IsInDebts(Dics))
                            {
                                if (ValidateMinConsumptionDate(Dics["MinConsumptionDate"]))
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
                ErrorCode = "El número de servicio es incorrecto";
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
        /// Validate minimum consumption
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private Boolean ValidateMinConsumptionDate(string temp)
        {
            //Boolean Flag = false;
            //string[] dateFormat = { "yyyyMMdd", "yyyy/MM/dd", "yyyy-MM-dd", "yyyyMM" };
            //DateTime tempDate;
            //try
            //{
            //    tempDate = DateTime.ParseExact(temp, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            //    if (DateTime.Now.Date.Year - tempDate.Year >= 1)
            //    {
            //        Flag = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("La fecha de consumo mínimo regresada por CFE es nula", ex);
            //}
            //return Flag;
            return true;
        }

        /// <summary>
        /// Validate User Status
        /// </summary>
        /// <param name="UserStatus"></param>
        /// <returns></returns>
        private Boolean ValidateUserStatus(string UserStatus)
        {
            bool Flag = false;
            if (!UserStatus.Equals(""))
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
            string[] dateFormat = { "yyyyMMdd", "yyyy/MM/dd", "yyyy-MM-dd", "yyyyMM" };
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

        /// <summary>
        /// Validate Rate
        /// </summary>
        /// <param name="Rate"></param>
        /// <returns></returns>
        private Boolean ValidateRate(string Rate)
        {
            Boolean Flag = false;
            DataTable dtRate = CAT_PROGRAMADal.ClassInstance.get_Cat_Tarifa(Global.PROGRAM.ToString(), Rate);
            if (dtRate.Rows.Count > 0)
            {
                Flag = true;
            }
            return Flag;
        }

        /// <summary>
        /// Get Credit Product data
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        private List<K_CREDITO_PRODUCTOEntity> GetCreditProduct(out double ProductCapacity)
        {
            #region Step Three Data
            //Changed by coco 2011-08-04
            ProductCapacity = 0;

            List<K_CREDITO_PRODUCTOEntity> creProduct = new List<K_CREDITO_PRODUCTOEntity>();

            for (int i = 0; i < gvTecPro.Rows.Count; i++)
            {
                DropDownList DropDownTechnology = gvTecPro.Rows[i].FindControl("ddlTecnolog") as DropDownList;
                DropDownList DropDownProduct = gvTecPro.Rows[i].FindControl("ddlProduct") as DropDownList;
                DropDownList DropDownMarca = gvTecPro.Rows[i].FindControl("ddlMarca") as DropDownList;
                TextBox txtModelo = gvTecPro.Rows[i].FindControl("txtModelo") as TextBox;
                TextBox txtCantidad = gvTecPro.Rows[i].FindControl("txtCantidad") as TextBox;
                TextBox txtGridText1 = gvTecPro.Rows[i].FindControl("txtGridText1") as TextBox;
                TextBox txtSubtotal = gvTecPro.Rows[i].FindControl("txtSubtotal") as TextBox;
                TextBox txtGastos = gvTecPro.Rows[i].FindControl("txtGastos") as TextBox;
                //Added by coco 2011-08-04
                DropDownList DropDownCapacidad = gvTecPro.Rows[i].FindControl("ddlCapacidad") as DropDownList;

                if (DropDownProduct.SelectedIndex != 0 && DropDownProduct.SelectedIndex != -1)
                {
                    K_CREDITO_PRODUCTOEntity model = new K_CREDITO_PRODUCTOEntity();
                    //Edit by coco 2011-08-04   
                    DataTable dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIATipoByPK(DropDownTechnology.SelectedValue);

                    if (dtTechnology != null)
                    {
                        if ((string.Compare(dtTechnology.Rows[0]["Dx_Nombre"].ToString().Trim(), "Iluminacion", true) == 0))// separately illumination device energySave
                        {
                            DataTable dtProduct = CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByPK(DropDownProduct.SelectedValue);
                            double tempCapacidad = DropDownCapacidad.SelectedItem.Text.Equals("") ? 0 : double.Parse(DropDownCapacidad.SelectedItem.Text);

                            if (dtProduct != null)
                            {
                                double TempEficienciaEnergia = dtProduct.Rows[0]["No_Eficiencia_Energia"] == DBNull.Value ? 0 : double.Parse(dtProduct.Rows[0]["No_Eficiencia_Energia"].ToString());
                                ProductCapacity = ProductCapacity + (tempCapacidad - (dtProduct.Rows[0]["No_Capacidad"].ToString() == "" ? 0 : double.Parse(dtProduct.Rows[0]["No_Capacidad"].ToString()))) * TempEficienciaEnergia * Convert.ToInt32(txtCantidad.Text);
                            }
                        }
                        else
                        {
                            double TempProductCapacity = K_CREDITODal.ClassInstance.CalculateTotalEnergyConsumptionSavings(DropDownProduct.SelectedValue);
                            ProductCapacity = ProductCapacity + TempProductCapacity * Convert.ToInt32(txtCantidad.Text);
                        }
                        model.Cve_Producto_Capacidad = DropDownCapacidad.SelectedValue.Equals("") ? 0 : int.Parse(DropDownCapacidad.SelectedValue);
                    }
                    //end edit

                    model.Cve_Producto = string.IsNullOrEmpty(DropDownProduct.SelectedValue) ? default(Int32) : int.Parse(DropDownProduct.SelectedValue);
                    model.No_Cantidad = string.IsNullOrEmpty(txtCantidad.Text) ? 0 : int.Parse(txtCantidad.Text);
                    model.Mt_Precio_Unitario = string.IsNullOrEmpty(txtGridText1.Text) ? default(decimal) : decimal.Parse(txtGridText1.Text);

                    //without tax
                    //edit by tina 2012-07-25
                    DataTable ProVProDdt = K_PROVEEDOR_PRODUCTOBLL.ClassInstance.Get_K_PROVEEDOR_PRODUCTO_ByPK(model.Cve_Producto.ToString(), idDepartment);

                    if (ProVProDdt != null && ProVProDdt.Rows.Count > 0)
                    {
                        decimal deUnit = decimal.Parse(ProVProDdt.Rows[0]["Mt_Precio_Unitario"].ToString());
                        decimal deUnit1 = decimal.Parse(strPct_Tasa_IVA.ToString());
                        model.Mt_Precio_Unitario_Sin_IVA = deUnit / (1 + deUnit1 / 100);
                    }
                    model.Mt_Total = string.IsNullOrEmpty(txtSubtotal.Text) ? default(decimal) : decimal.Parse(txtSubtotal.Text);
                    model.Mt_Gastos_Instalacion_Mano_Obra = string.IsNullOrEmpty(txtGastos.Text) ? default(decimal) : decimal.Parse(txtGastos.Text);
                    model.Dt_Fecha_Credito_Producto = DateTime.Now.Date;
                    model.No_Credito = Creditno;

                    creProduct.Add(model);
                }
            }
            return creProduct;
            #endregion
        }

        /// <summary>
        /// Build credit entity from user interface
        /// </summary>
        /// <returns></returns>
        private EntidadCredito GetDataFromUI()
        {
            EntidadCredito EntidadCredito = new EntidadCredito();

            EntidadCredito.Dx_Razon_Social = txtName.Text + " " + txtLastname.Text + " " + txtMotherName.Text;
            int industryid = 0;
            int.TryParse(ddlGiroEmpresa.SelectedValue, out industryid);
            EntidadCredito.Cve_Tipo_Industria = industryid;
            int pfisica = 0;
            int.TryParse(ddlPfisica.SelectedValue, out pfisica);
            EntidadCredito.Cve_Tipo_Sociedad = pfisica;
            EntidadCredito.Dx_CURP = txtCURP.Text;
            EntidadCredito.Dx_RFC = txtRFC.Text;
            EntidadCredito.Dx_Nombre_Repre_Legal = txtRepresLegal.Text;
            int acreditado = 0;
            int.TryParse(ddlAcreditado.SelectedValue, out acreditado);
            EntidadCredito.Cve_Acreditacion_Repre_legal = acreditado;
            int sex03 = 0;
            int.TryParse(rblSex.SelectedValue, out sex03);
            EntidadCredito.Fg_Sexo_Repre_legal = sex03;
            EntidadCredito.No_RPU = txtRPU.Text;
            int estadocivil = 0;
            int.TryParse(rblEstadoCivil.SelectedValue, out estadocivil);
            EntidadCredito.Fg_Edo_Civil_Repre_legal = estadocivil;

            int regimenmatri = 0;
            int.TryParse(ddlRegimenMatri.SelectedValue, out regimenmatri);
            EntidadCredito.Cve_Reg_Conyugal_Repre_legal = regimenmatri;
            int tipoindenti = 0;
            int.TryParse(ddlTipoIdenti.SelectedValue, out tipoindenti);
            EntidadCredito.Cve_Identificacion_Repre_legal = tipoindenti;
            EntidadCredito.Dx_No_Identificacion_Repre_Legal = txtNumero.Text;
            double promediomv = 0;
            double.TryParse(txtPromedioMV.Text, out promediomv);
            EntidadCredito.Mt_Ventas_Mes_Empresa = promediomv;
            double totalgasm = 0;
            double.TryParse(txtTotalGastosMensual.Text, out totalgasm);
            EntidadCredito.Mt_Gastos_Mes_Empresa = totalgasm;
            EntidadCredito.Mt_Ingreso_Neto_Mes_Empresa = EntidadCredito.Mt_Ventas_Mes_Empresa - EntidadCredito.Mt_Gastos_Mes_Empresa;

            EntidadCredito.Dx_Email_Repre_legal = txtRLemail.Text;
            EntidadCredito.Dx_Domicilio_Fisc_Calle = txtFiscalCalle.Text;
            EntidadCredito.Dx_Domicilio_Fisc_Num = txtFiscalNumero.Text;
            EntidadCredito.Dx_Domicilio_Fisc_CP = txtFiscalCP.Text;
            EntidadCredito.Dx_Domicilio_Fisc_Colonia = txtDx_Domicilio_Fisc_Colonia.Text;
            int fiscalestado = 0;
            int.TryParse(ddlFiscalEstado.SelectedValue, out fiscalestado);
            EntidadCredito.Cve_Estado_Fisc = fiscalestado;
            int fiscaldm = 0;
            int.TryParse(ddlFiscalDM.SelectedValue, out fiscaldm);
            EntidadCredito.Cve_Deleg_Municipio_Fisc = fiscaldm;
            int fiscaltipopropie = 0;
            int.TryParse(ddlFiscalTipoPropie.SelectedValue, out fiscaltipopropie);
            EntidadCredito.Cve_Tipo_Propiedad_Fisc = fiscaltipopropie;
            EntidadCredito.Dx_Tel_Fisc = txtFiscalTele.Text;
            bool fgmdnego = true;
            bool.TryParse(cbMismoFiscal.Text, out fgmdnego);
            EntidadCredito.Fg_Mismo_Domicilio = fgmdnego;

            EntidadCredito.Dx_Domicilio_Neg_Calle = txtNegocioCalle.Text;
            EntidadCredito.Dx_Domicilio_Neg_Num = txtNegocioNumero.Text;
            EntidadCredito.Dx_Domicilio_Neg_CP = txtNegocioCP.Text;
            EntidadCredito.Dx_Domicilio_Neg_Colonia = txtDx_Domicilio_Neg_Colonia.Text;
            int negoestado = 0;
            int.TryParse(ddlNegocioEstado.SelectedValue, out negoestado);
            EntidadCredito.Cve_Estado_Neg = negoestado;
            int negdm = 0;
            int.TryParse(ddlNegocioDM.SelectedValue, out negdm);
            EntidadCredito.Cve_Deleg_Municipio_Neg = negdm;
            int negotipopropie = 0;
            int.TryParse(ddlNegocioTipoPropie.SelectedValue, out negotipopropie);
            EntidadCredito.Cve_Tipo_Propiedad_Neg = negotipopropie;
            EntidadCredito.Dx_Tel_Neg = txtNegocioTele.Text;
            EntidadCredito.Dx_Nombre_Aval = txtNobreAvalRS.Text;
            EntidadCredito.Dx_RFC_CURP_Aval = txtAvalCURP.Text;
            EntidadCredito.Dx_Tel_Aval = txtAvalTele.Text;
            //EntidadCredito.No_RPU_AVAL = this.txtNo_RPU_aval.Text;
            int sex09 = 1;
            int.TryParse(rblAvalSexo.SelectedValue, out sex09);
            EntidadCredito.Fg_Sexo_Aval = sex09;
            EntidadCredito.Dx_Domicilio_Aval_Calle = txtAvalCalle.Text;
            EntidadCredito.Dx_Domicilio_Aval_Num = txtAvalNumero.Text;
            EntidadCredito.Dx_Domicilio_Aval_CP = txtAvalCP.Text;
            EntidadCredito.Dx_Domicilio_Aval_Colonia = txtDx_Domicilio_Aval_Colonia.Text;
            int avalestado = 0;
            int.TryParse(ddlAvalEstado.SelectedValue, out avalestado);
            EntidadCredito.Cve_Estado_Aval = avalestado;
            int avaldm = 0;
            int.TryParse(ddlAvalDM.SelectedValue, out avaldm);
            EntidadCredito.Cve_Deleg_Municipio_Aval = avaldm;
            double avalpmv = 0;
            //double.TryParse(txtAvalPMV.Text, out avalpmv);
            EntidadCredito.Mt_Ventas_Mes_Aval = avalpmv;
            double avaltgm = 0;
            //double.TryParse(txtAvalTGM.Text, out avaltgm);
            EntidadCredito.Mt_Gastos_Mes_Aval = avaltgm;
            EntidadCredito.Mt_Ingreso_Neto_Mes_Aval = EntidadCredito.Mt_Ventas_Mes_Aval - EntidadCredito.Mt_Gastos_Mes_Aval;

            return EntidadCredito;
        }

        /// <summary>
        /// Calculate the total value
        /// </summary>
        private void AccountTotal()
        {
            decimal AcountTotal = 0;
            int ProductCount = 0;//add by coco 20110905
            for (int i = 0; i < gvTecPro.Rows.Count; i++)
            {
                TextBox txtSubtotal = gvTecPro.Rows[i].FindControl("txtSubtotal") as TextBox;
                TextBox Product = gvTecPro.Rows[i].FindControl("txtCantidad") as TextBox;//add by coco 20110905
                decimal deTotal = 0;
                if (!txtSubtotal.Text.Equals(""))
                {
                    deTotal = decimal.Parse(txtSubtotal.Text);
                }
                if (Product != null)//add by coco 20110905
                {
                    if (!Product.Text.Equals(""))
                    {
                        ProductCount = ProductCount + int.Parse(Product.Text);
                    }
                }
                AcountTotal = AcountTotal + deTotal;
            }
            if (ProductCount == 0)
            {
                ProductCount = 1;
            }
            //add by coco 2011-08-05
            if (DescountFlag)
            {
                creditDesEntity.Mt_Descuento = Ddescount * AcountTotal;
            }
            else//add by coco 20110905
            {
                creditDesEntity.Mt_Descuento = creditDesEntity.Mt_Descuento * ProductCount;
            }
            if (CostFlag)
            {
                creditCostEntity.Mt_Costo = DCost * AcountTotal;
            }
            else  //add by coco 20110905
            {
                creditCostEntity.Mt_Costo = creditCostEntity.Mt_Costo * ProductCount;
            }
            //Changed  by coco 2011/09/05
            if (AcountTotal.Equals(0))
            {
                this.txtSubTotal.Text = "";
                this.txtTotalCost.Text = "";
                this.txtTotalDescount.Text = "";
                txtTotal.Text = "0";
            }
            else
            {
                this.txtSubTotal.Text = AcountTotal.ToString();
                this.txtTotalCost.Text = creditCostEntity.Mt_Costo.ToString();
                this.txtTotalDescount.Text = creditDesEntity.Mt_Descuento.ToString();
                txtTotal.Text = (AcountTotal - creditDesEntity.Mt_Descuento + creditCostEntity.Mt_Costo).ToString();
            }
            //end add 
        }

        /// <summary>
        /// Bind Grid view data
        /// </summary>
        /// <param name="dt"></param>
        private void DataGridviewBind(string Flag)
        {
            // Add by Tina 2011/08/17
            GridViewDataSource = new DataTable();

            GridViewDataSource.Columns.Add("Technology", Type.GetType("System.String"));
            //edit by coco 20110823
            GridViewDataSource.Columns.Add("TypeProduct", Type.GetType("System.String"));
            //GridViewDataSource.Columns.Add("Product", Type.GetType("System.String"));
            //end edit            
            GridViewDataSource.Columns.Add("Marca", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("Modelo", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("Cantidad", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("PrecioUnitario", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("Subtotal", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("Capacidad", Type.GetType("System.String"));
            GridViewDataSource.Columns.Add("Gastos", Type.GetType("System.String"));
            // End
            DataTable CreditProductDt = new DataTable();

            if (Flag == "P")
            {
                CreditProductDt = K_CREDITO_PRODUCTODal.ClassInstance.get_K_Credit_ProductByCreditNo(Creditno.ToString());
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
                    row["Gastos"] = dataRow["Gastos"] != DBNull.Value ? dataRow["Gastos"].ToString() : "";
                    GridViewDataSource.Rows.Add(row);
                }
            }

            //GridViewDataSource = CreditProductDt;
            // End
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
        /// 
        /// </summary>
        /// <param name="IEstado"></param>
        /// <returns></returns>
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

        private bool IsSE(string techId)
        {
            return DxCveCC.ContainsKey(techId) && DxCveCC[techId] == DxCveCC_SE;
        }

        /// <summary>
        /// add by coco 20110905
        /// </summary>
        /// <param name="strFlag"></param>
        private void SetupTooltip(string controlName)
        {
            for (int j = 0; j < gvTecPro.Rows.Count; j++)
            {
                DropDownList dropDownList = gvTecPro.Rows[j].FindControl(controlName) as DropDownList;
                for (int i = 0; i < dropDownList.Items.Count; i++)
                {
                    dropDownList.Items[i].Attributes.Add("Title", dropDownList.Items[i].Text.ToString());
                }
            }
        }
        #endregion

        #region Control Events
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbMismoFiscal_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMismoFiscal.Checked)
            {
                txtNegocioCalle.Text = txtFiscalCalle.Text;
                txtNegocioNumero.Text = txtFiscalNumero.Text;
                txtNegocioCP.Text = txtFiscalCP.Text;
                ddlNegocioEstado.SelectedValue = ddlFiscalEstado.SelectedValue == "0" ? "" : ddlFiscalEstado.SelectedValue.ToString();
                ddlNegocioDM.SelectedValue = ddlFiscalDM.SelectedValue;
                ddlNegocioTipoPropie.SelectedValue = ddlFiscalTipoPropie.SelectedValue;
                txtNegocioTele.Text = txtFiscalTele.Text;
                txtDx_Domicilio_Neg_Colonia.Text = txtDx_Domicilio_Fisc_Colonia.Text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPfisica_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CompanyType.PERSONAFISICA != (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Value)
                && CompanyType.REPECO != (CompanyType)Enum.Parse(typeof(CompanyType), ddlPfisica.SelectedItem.Value))
            //if (string.Compare(ddlPfisica.SelectedItem.Text, "PERSONA FISICa", true) != 0)
            {
                txtCURP.Enabled = false;
                txtCURP.Text = "";
                CO.Visible = false;

                //visible 
                //DivEnable.Visible = false;
                // lblBirthDate.Visible = false;
                // txtBirthDate.Visible = false;
                // txtBirthDate.Text = "";
                lblBirthDate.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelBirthDateMoral") as string;
                lblName.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelPersonaRazon") as string;

                lblLastname.Visible = false;
                txtLastname.Visible = false;
                txtLastname.Text = "";
                lblMotherName.Visible = false;
                txtMotherName.Visible = false;
                txtMotherName.Text = "";
            }
            else
            {
                if (rblEstadoCivil.SelectedValue == "2" && string.Compare(ddlRegimenMatri.SelectedItem.Text, "BIENES MANCOMUNADOS", true) == 0)
                {
                    CO.Visible = true;
                }
                else
                {
                    CO.Visible = false;
                }
                txtCURP.Enabled = true;
                //DivEnable.Visible = true;
                lblBirthDate.Visible = true;
                txtBirthDate.Visible = true;
                lblBirthDate.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelBirthDateInmoral") as string;
                lblName.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "LabelPersonaNombre") as string;
                lblLastname.Visible = true;
                txtLastname.Visible = true;
                lblMotherName.Visible = true;
                txtMotherName.Visible = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblEstadoCivil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblEstadoCivil.SelectedValue == "2")
            {
                ddlRegimenMatri.Enabled = true;
                if (string.Compare(ddlPfisica.SelectedItem.Text, "PERSONA FISICA", true) == 0 && string.Compare(ddlRegimenMatri.SelectedItem.Text, "BIENES MANCOMUNADOS", true) == 0)
                {
                    CO.Visible = true;
                }
                else
                {
                    CO.Visible = false;
                }
            }
            else
            {
                ddlRegimenMatri.Enabled = false;
                ddlRegimenMatri.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRegimenMatri_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.Compare(ddlPfisica.SelectedItem.Text, "PERSONA FISICA", true) == 0 && rblEstadoCivil.SelectedValue == "2" && string.Compare(ddlRegimenMatri.SelectedItem.Text, "BIENES MANCOMUNADOS", true) == 0)
            {
                CO.Visible = true;
            }
            else
            {
                CO.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlFiscalEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlFiscalDM();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlNegocioEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlNegocioDM();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlAvalEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlAvalDM();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCoAcreditadoEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlCoAcreditadoDM();
        }

        /// <summary>
        /// Service Code Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtRPU_TextChanged(object sender, EventArgs e)
        {
            string tempStatusFlag;
            string ErrorCode;

            if (K_CREDITODal.ClassInstance.IsServiceCodeExist(this.txtRPU.Text))
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "TheServiceCodehaveUsed") as string;
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                btnSave.Enabled = false;
                return;
            }
            else
            {
                btnSave.Enabled = true;
            }
            if (!ValidateServiceCode(txtRPU.Text, out ErrorCode, out tempStatusFlag))
            {
                CO.Visible = false;
                panel.Visible = false;
                Page4.Visible = false;
                StatusFlag = tempStatusFlag;
                //string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "ServiceCodeIsInvalidate") as string;
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('" + strMsg + "');", true);
            }
            else
            {
                CO.Visible = true;
                panel.Visible = true;
                Page4.Visible = true;
            }
        }

        /// <summary>
        /// databind gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTecPro_DataBound(object sender, EventArgs e)
        {
            US_USUARIOModel User = (US_USUARIOModel)Session["UserInfo"];
            Label LabelCapacidad = gvTecPro.HeaderRow.FindControl("lblCapacidad") as Label;
            for (int i = 0; i < GridViewDataSource.Rows.Count; i++)
            {
                DropDownList ddlTechnology = gvTecPro.Rows[i].FindControl("ddlTecnolog") as DropDownList;
                DropDownList ddlProduct = gvTecPro.Rows[i].FindControl("ddlProduct") as DropDownList;
                DropDownList ddlMarca = gvTecPro.Rows[i].FindControl("ddlMarca") as DropDownList;
                DropDownList DropDownTypeOfProduct = gvTecPro.Rows[i].FindControl("ddlTypeOfProduct") as DropDownList;//Added by coco 20110823
                TextBox txtCantidad = gvTecPro.Rows[i].FindControl("txtCantidad") as TextBox;
                TextBox txtGridText1 = gvTecPro.Rows[i].FindControl("txtGridText1") as TextBox;
                TextBox txtSubtotal = gvTecPro.Rows[i].FindControl("txtSubtotal") as TextBox;
                TextBox txtGastos = gvTecPro.Rows[i].FindControl("txtGastos") as TextBox;
                //add by coco 2011-08-04
                DropDownList ddlCapacidad = gvTecPro.Rows[i].FindControl("ddlCapacidad") as DropDownList;

                //Added by coco 20110823
                if (ddlTechnology != null)
                {
                    DataTable Technology;
                    if (User.Tipo_Usuario == "S"||User.Tipo_Usuario == "S_B")
                        Technology = CAT_TECNOLOGIABLL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgramAndDxCveCC(Global.PROGRAM.ToString(), User.Id_Usuario, GridViewDataSource.Rows.Count > 1 ? DxCveCC_SE : "");//Changed by Jerry 2011/08/08
                    else
                        Technology = CAT_TECNOLOGIABLL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgramAndProductID(Global.PROGRAM.ToString(), "", "", 0);
                    
                    if (Technology != null && Technology.Rows.Count > 0)//Added by coco 20110823
                    {
                        ddlTechnology.DataSource = Technology;
                        ddlTechnology.DataTextField = "Dx_Nombre_Particular";
                        ddlTechnology.DataValueField = "Cve_Tecnologia";
                        ddlTechnology.DataBind();
                        ddlTechnology.Items.Insert(0, "");

                        DxCveCC.Clear();
                        CveGasto.Clear();
                        for (int j = 0; j < Technology.Rows.Count; j++)
                        {
                            if (j == 0)
                            {
                                TechnologyList = Technology.Rows[0]["Cve_Tecnologia"].ToString();
                            }
                            else
                            {
                                TechnologyList = TechnologyList + "," + Technology.Rows[j]["Cve_Tecnologia"].ToString();
                            }
                            DxCveCC.Add(Technology.Rows[j]["Cve_Tecnologia"].ToString(), Technology.Rows[j]["Dx_Cve_CC"].ToString());
                            CveGasto.Add(Technology.Rows[j]["Cve_Tecnologia"].ToString(), (int)Technology.Rows[j]["Cve_Gasto"]);
                        }

                        if (i < GridViewDataSource.Rows.Count)
                        {
                            ddlTechnology.SelectedValue = GridViewDataSource.Rows[i]["Technology"].ToString();
                            if (IsSE(GridViewDataSource.Rows[i]["Technology"].ToString()))
                            {
                                btnAddRow.Enabled = false;
                                lDescription.Visible = true;
                                // RSA 2012-09-11 product selection modified
                                lDescription.Text = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ForSE_Description(GridViewDataSource.Rows[i]["Modelo"].ToString());
                            }
                        }
                        else
                        {
                            ddlTechnology.SelectedIndex = 0;
                        }

                        // RSA 20130903 make it visible iff technology is set to one with CveGasto == 1
                        txtGastos.Visible = ddlTechnology.SelectedIndex > 0 && CveGasto[ddlTechnology.SelectedValue] == 1;
                    }
                }

                if (DropDownTypeOfProduct != null)
                {
                    DataTable TypeOfProduct=null;
                    if (!ddlTechnology.SelectedValue.Equals(""))
                    {
                        TypeOfProduct = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(ddlTechnology.SelectedValue);
                    }
                    else
                    {
                        TypeOfProduct = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(TechnologyList);
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
                    }
                }
                //End Added 20110823

                string strProductID = "0"; //add by coco 2011-08-10
                if (ddlProduct != null)
                {

                    //DataTable Product = CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByTechnology_1(TechnologyList, User.Id_Departamento.ToString(), DropDownTypeOfProduct.SelectedValue);
                    DataTable Product = CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByTechnology_1(TechnologyList, idDepartment, DropDownTypeOfProduct.SelectedValue);
                    if (Product != null)
                    {
                        ddlProduct.DataSource = Product;
                        ddlProduct.DataTextField = "Dx_Modelo_Producto";
                        ddlProduct.DataValueField = "Cve_Producto";
                        ddlProduct.DataBind();
                        ddlProduct.Items.Insert(0, "");

                        if (i < GridViewDataSource.Rows.Count)
                        {
                            ddlProduct.SelectedValue = GridViewDataSource.Rows[i]["Modelo"].ToString();
                        }
                        else
                        {
                            ddlProduct.SelectedIndex = 0;
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
                }
              
                if (ddlMarca != null)
                {
                    string strTempProduct = "";
                    if (!ddlProduct.SelectedValue.Equals(""))
                    {
                        strTempProduct = ddlProduct.SelectedValue;
                    }
                    else
                    {
                        strTempProduct = strProductID;
                    }
                    DataTable Marca = CAT_MARCADal.ClassInstance.Get_CAT_MARCADal(strTempProduct);
                   
                    if (Marca != null)
                    {
                        ddlMarca.DataSource = Marca;
                        ddlMarca.DataTextField = "Dx_Marca";
                        ddlMarca.DataValueField = "Cve_Marca";
                        ddlMarca.DataBind();
                        ddlMarca.Items.Insert(0, "");

                        if (i < GridViewDataSource.Rows.Count)
                        {
                            ddlMarca.SelectedValue = GridViewDataSource.Rows[i]["Marca"].ToString();
                        }
                        else
                        {
                            ddlMarca.SelectedIndex = 0;
                        }
                    }
                }

                //Added by coco 2011-08-04
                if (ddlCapacidad != null)
                {
                    string strTempTech = "";
                    if (ddlTechnology.SelectedValue.Equals(""))
                    {
                        strTempTech = TechnologyList;
                    }
                    else
                    {
                        strTempTech = ddlTechnology.SelectedValue;
                    }
                    DataTable Capacity;
                    Capacity = ProductCapacityDal.ClassInstance.Get_ProductCapacity(strTempTech);
                    //if (IsSE(ddlTechnology.SelectedValue))
                    //{
                    //    //Capacity = new DataTable();
                    //    //Capacity.Columns.Add("Cve_Producto_Capacidad");
                    //    //Capacity.Columns.Add("Ft_Capacidad");
                    //    //Capacity.Rows.Add(new object[] { "3", "OM" });
                    //    //Capacity.Rows.Add(new object[] { "4", "HM" });

                    //    LabelCapacidad.Text = "Tarifa";
                    //}
                    //else
                    //{
                        //Capacity = ProductCapacityDal.ClassInstance.Get_ProductCapacity(strTempTech);

                        LabelCapacidad.Text = "Capacidad";
                    //}
                    if (Capacity != null)
                    {
                        ddlCapacidad.DataSource = Capacity;
                        ddlCapacidad.DataTextField ="Cve_Producto_Capacidad";
                        ddlCapacidad.DataValueField =  "Ft_Capacidad";
                        ddlCapacidad.DataBind();
                        ddlCapacidad.Items.Insert(0, "");

                        if (i < GridViewDataSource.Rows.Count)
                        {
                            ddlCapacidad.SelectedValue = GridViewDataSource.Rows[i]["Capacidad"].ToString();
                        }
                        else
                        {
                            ddlCapacidad.SelectedIndex = 0;
                        }
                    }
                }
                //End Add

                if (i < GridViewDataSource.Rows.Count)
                {
                    double value;
                    //txtModelo.Text = GridViewDataSource.Rows[i]["Modelo"].ToString();//edit by coco 20110823
                    txtCantidad.Text = GridViewDataSource.Rows[i]["Cantidad"].ToString();
                    //txtGastos.Text = double.TryParse(GridViewDataSource.Rows[i]["Gastos"].ToString(), out value) ? (value / (1 + strPct_Tasa_IVA / 100)).ToString("n") : "0.00";
                    //txtGridText1.Text = double.TryParse(GridViewDataSource.Rows[i]["PrecioUnitario"].ToString(), out value) ? (value / (1 + strPct_Tasa_IVA / 100)).ToString("n") : GridViewDataSource.Rows[i]["PrecioUnitario"].ToString();
                    //txtSubtotal.Text = double.TryParse(GridViewDataSource.Rows[i]["Subtotal"].ToString(), out value) ? (value / (1 + strPct_Tasa_IVA / 100)).ToString("n") : GridViewDataSource.Rows[i]["Subtotal"].ToString();
                    txtGastos.Text = double.TryParse(GridViewDataSource.Rows[i]["Gastos"].ToString(), out value) ? (value ).ToString("n") : "0.00";
                    txtGridText1.Text = double.TryParse(GridViewDataSource.Rows[i]["PrecioUnitario"].ToString(), out value) ? (value).ToString("n") : GridViewDataSource.Rows[i]["PrecioUnitario"].ToString();
                    txtSubtotal.Text = double.TryParse(GridViewDataSource.Rows[i]["Subtotal"].ToString(), out value) ? (value).ToString("n") : GridViewDataSource.Rows[i]["Subtotal"].ToString();
                    //added by tina 2012-07-26
                    SubTotal += Math.Round((decimal)value,2);
                    //end
                }

                //add by coco 20110905     
                SetupTooltip("ddlTecnolog");
                ddlTechnology.ToolTip = ddlTechnology.SelectedItem.Text;
                SetupTooltip("ddlProduct");
                ddlProduct.ToolTip = ddlProduct.SelectedItem.Text;
                SetupTooltip("ddlMarca");
                ddlMarca.ToolTip = ddlMarca.SelectedItem.Text;
                SetupTooltip("ddlTypeOfProduct");
                DropDownTypeOfProduct.ToolTip = DropDownTypeOfProduct.SelectedItem.Text;        
            }
        }

        /// <summary>
        /// select Technology
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTecnolog_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DropDownListTechnology = (DropDownList)sender;
            string technologySelectedValue = DropDownListTechnology.SelectedValue;
            GridViewRow gridViewRow = (GridViewRow)DropDownListTechnology.NamingContainer;

            DropDownList DropDownListProduct = gvTecPro.Rows[gridViewRow.RowIndex].FindControl("ddlProduct") as DropDownList;
            DropDownList DropDownListMarca = gvTecPro.Rows[gridViewRow.RowIndex].FindControl("ddlMarca") as DropDownList;
            DropDownList DropDownTypeOfProduct = gvTecPro.Rows[gridViewRow.RowIndex].FindControl("ddlTypeOfProduct") as DropDownList;//Changed by coco 20110823
            TextBox txtCantidad = gvTecPro.Rows[gridViewRow.RowIndex].FindControl("txtCantidad") as TextBox;

            //Changed by coco 20110823          
            if (technologySelectedValue.Equals(""))
            {
                technologySelectedValue = TechnologyList;
            }

            DataTable ProductType = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(technologySelectedValue);
            if (ProductType != null)
            {
                DropDownTypeOfProduct.DataSource = ProductType;
                DropDownTypeOfProduct.DataTextField = "Dx_Tipo_Producto";
                DropDownTypeOfProduct.DataValueField = "Ft_Tipo_Producto";
                DropDownTypeOfProduct.DataBind();
                DropDownTypeOfProduct.Items.Insert(0, "");
                DropDownTypeOfProduct.SelectedIndex = 0;
            }

            DataTable Product = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ByTechnology(technologySelectedValue, DropDownListMarca.SelectedValue, idDepartment, DropDownTypeOfProduct.SelectedValue);
            string strProductID = "0";
            if (Product != null)
            {
                DropDownListProduct.DataSource = Product;
                DropDownListProduct.DataTextField = "Dx_Modelo_Producto";
                //end edit
                DropDownListProduct.DataValueField = "Cve_Producto";
                DropDownListProduct.DataBind();
                DropDownListProduct.Items.Insert(0, "");
                DropDownListProduct.SelectedIndex = 0;
                //add by coco 2011-08-10
                for (int j = 0; j < Product.Rows.Count; j++)
                {
                    if (!Product.Rows[j]["Cve_Producto"].ToString().Equals(""))
                    {
                        strProductID = strProductID + "," + Product.Rows[j]["Cve_Producto"].ToString();
                    }
                }
            }

            if (DropDownListMarca != null)
            {
                string strTempProduct = "";
                if (!DropDownListMarca.SelectedValue.Equals(""))
                {
                    strTempProduct = DropDownListMarca.SelectedValue;
                }
                else
                {
                    strTempProduct = strProductID;
                }
                DataTable Marca = CAT_MARCADal.ClassInstance.Get_CAT_MARCADal(strTempProduct);
                if (Marca != null)
                {
                    DropDownListMarca.DataSource = Marca;
                    DropDownListMarca.DataTextField = "Dx_Marca";
                    DropDownListMarca.DataValueField = "Cve_Marca";
                    DropDownListMarca.DataBind();
                    DropDownListMarca.Items.Insert(0, "");                  
                }

                //Add by coco 2011-08-05
                //Reload product capacity options when technology changes
                DropDownList DropDownCapacidad = gvTecPro.Rows[gridViewRow.RowIndex].FindControl("ddlCapacidad") as DropDownList;
                Label LabelCapacidad = gvTecPro.HeaderRow.FindControl("lblCapacidad") as Label;
                if (IsSE(technologySelectedValue))
                {
                    btnAddRow.Enabled = false;
                    DataTable Capacity = new DataTable();
                    Capacity.Columns.Add("Cve_Producto_Capacidad");
                    Capacity.Columns.Add("Ft_Capacidad");
                    Capacity.Rows.Add(new object[] { "3", "OM" });
                    Capacity.Rows.Add(new object[] { "4", "HM" });
                    DropDownCapacidad.DataSource = Capacity;
                    DropDownCapacidad.DataBind();

                    LabelCapacidad.Text = "Tarifa";

                    txtCantidad.Text = "1";
                    txtCantidad.Enabled = false;
                }
                else
                {
                    //btnAddRow.Enabled = true;
                    DropDownCapacidad.DataSource = ProductCapacityDal.ClassInstance.Get_ProductCapacity(technologySelectedValue);
                    DropDownCapacidad.DataTextField = "Ft_Capacidad";
                    DropDownCapacidad.DataValueField = "Cve_Producto_Capacidad";
                    DropDownCapacidad.DataBind();
                    DropDownCapacidad.Items.Insert(0, "");

                    LabelCapacidad.Text = "Capacidad";
                }
                DropDownCapacidad.SelectedIndex = 0;

                //Enable/Disable the capacity option control by technology selected            
                DataTable dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIATipoByPK(DropDownListTechnology.SelectedValue);
                if (DropDownListTechnology != null && dtTechnology.Rows.Count > 0)
                {
                    if (!dtTechnology.Rows[0]["Dx_Nombre"].ToString().Trim().StartsWith("Iluminacion") && !IsSE(DropDownListTechnology.SelectedValue))
                    //if (string.Compare(dtTechnology.Rows[0]["Dx_Nombre"].ToString().Trim(), "Iluminacion", true) != 0)
                    {
                        DropDownCapacidad.Enabled = false;
                    }
                    else
                    {
                        DropDownCapacidad.Enabled = true;
                    }
                    //end add
                }

                //Changed by Coco 20110824: clear other selections
                DropDownListMarca.SelectedIndex = 0;
                ((TextBox)gridViewRow.FindControl("txtGridText1")).Text = "";//unit price clear
                ((TextBox)gridViewRow.FindControl("txtSubtotal")).Text = "";//clear sub total
                this.AccountTotal();

                //add by coco 20110905     
                SetupTooltip("ddlTecnolog");
                DropDownListTechnology.ToolTip = DropDownListTechnology.SelectedItem.Text;
                SetupTooltip("ddlProduct");
                DropDownListProduct.ToolTip = DropDownListProduct.SelectedItem.Text;
                SetupTooltip("ddlMarca");
                DropDownListMarca.ToolTip = DropDownListMarca.SelectedItem.Text;
                SetupTooltip("ddlTypeOfProduct");
                DropDownTypeOfProduct.ToolTip = DropDownTypeOfProduct.SelectedItem.Text;        
            }
        }
        /// <summary>
        /// select Marca
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlMar = (DropDownList)sender;
            string MarcaSelectedValue = ddlMar.SelectedValue;
            GridViewRow gridViewRow = (GridViewRow)ddlMar.NamingContainer;

            DropDownList ddlPro = gvTecPro.Rows[gridViewRow.RowIndex].FindControl("ddlProduct") as DropDownList;
            DropDownList ddlTech = gvTecPro.Rows[gridViewRow.RowIndex].FindControl("ddlTecnolog") as DropDownList;            
            DropDownList DropDownTypeOfProduct = gvTecPro.Rows[gridViewRow.RowIndex].FindControl("ddlTypeOfProduct") as DropDownList;//add by coco 20110823

            string strTempTech = "";
            if (ddlTech.SelectedValue.Equals(""))
            {
                strTempTech = TechnologyList;
            }
            else
            {
                strTempTech = ddlTech.SelectedValue;
            }

            string strProductID = "0";
            DataTable dtProduct = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ByTechnology(strTempTech, MarcaSelectedValue,idDepartment,DropDownTypeOfProduct.SelectedValue);
            if (dtProduct != null)
            {
                //add by coco 2011-08-10
                for (int i = 0; i < dtProduct.Rows.Count; i++)
                {
                    if (!dtProduct.Rows[i]["Cve_Producto"].ToString().Equals(""))
                    {
                        strProductID = strProductID + "," + dtProduct.Rows[i]["Cve_Producto"].ToString();
                    }
                }
                //end add
                ddlPro.DataSource = dtProduct;
                ddlPro.DataTextField = "Dx_nombre_producto";
                ddlPro.DataValueField = "Cve_Producto";
                ddlPro.DataBind();
                ddlPro.Items.Insert(0, "");
                ddlPro.SelectedIndex = 0;
            }

            ///Changed by Coco 20110824: clear other selections
            ((TextBox)gridViewRow.FindControl("txtGridText1")).Text = "";//unit price clear
            ((TextBox)gridViewRow.FindControl("txtSubtotal")).Text = "";//clear sub total
            this.AccountTotal();

            //add by coco 20110905     
            SetupTooltip("ddlTecnolog");
            ddlTech.ToolTip = ddlTech.SelectedItem.Text;
            SetupTooltip("ddlProduct");
            ddlPro.ToolTip = ddlPro.SelectedItem.Text;
            SetupTooltip("ddlMarca");
            ddlMar.ToolTip = ddlMar.SelectedItem.Text;
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
            int RowIndex = gridViewRow.RowIndex;
            DropDownList DropDownTechnology = gvTecPro.Rows[RowIndex].FindControl("ddlTecnolog") as DropDownList;
            DropDownList DropDownMarca = gvTecPro.Rows[RowIndex].FindControl("ddlMarca") as DropDownList;

            DataTable dtTypeProduct = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByPk(TypeProduct);
            if (dtTypeProduct.Rows.Count > 0)
            {
                DropDownTechnology.SelectedValue = dtTypeProduct.Rows[0]["Cve_Tecnologia"].ToString();
            }
            else
            {
                DropDownTechnology.SelectedIndex = 0;
            }

            string strTempTech = "";
            if (!DropDownTechnology.SelectedValue.Equals(""))
            {
                strTempTech = DropDownTechnology.SelectedValue;
            }
            else
            {
                strTempTech = TechnologyList;
            }

            DropDownList DropDownProduct = gvTecPro.Rows[RowIndex].FindControl("ddlProduct") as DropDownList;
            if (DropDownProduct != null)
            {
                DropDownProduct.DataSource = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ByTechnology(strTempTech, DropDownMarca.SelectedValue, idDepartment, TypeProduct);
                DropDownProduct.DataTextField = "Dx_Modelo_Producto";
                DropDownProduct.DataValueField = "Cve_Producto";
                DropDownProduct.DataBind();
                DropDownProduct.Items.Insert(0, "");
                DropDownProduct.SelectedIndex = 0;
            }

            //Changed by Coco 20110824: clear other selections
            DropDownMarca.SelectedIndex = 0;
            ((TextBox)gridViewRow.FindControl("txtGridText1")).Text = "";//unit price clear
            ((TextBox)gridViewRow.FindControl("txtSubtotal")).Text = "";//clear sub total
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
        /// select product 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            try
            {
                DropDownList ddlPro = (DropDownList)sender;
                string strPro = ddlPro.SelectedValue;
                GridViewRow Row = (GridViewRow)ddlPro.NamingContainer;

                DataTable dt = CAT_PRODUCTOBLL.ClassInstance.Get_CAT_PRODUCTO_ByPK(strPro);
                DropDownList ddlTech = gvTecPro.Rows[Row.RowIndex].FindControl("ddlTecnolog") as DropDownList;
                DropDownList ddlMar = gvTecPro.Rows[Row.RowIndex].FindControl("ddlMarca") as DropDownList;
                TextBox txtModelo = gvTecPro.Rows[Row.RowIndex].FindControl("txtModelo") as TextBox;
                TextBox txtGridText1 = gvTecPro.Rows[Row.RowIndex].FindControl("txtGridText1") as TextBox;

                ddlTech.SelectedValue = dt.Rows[0]["Cve_Tecnologia"].ToString();
                ddlMar.SelectedValue = dt.Rows[0]["Cve_Fabricante"].ToString();
                txtModelo.Text = dt.Rows[0]["Dx_Modelo_Producto"].ToString();

                //get the unitary cost of the specific model
                DataTable unitDt = K_PROVEEDOR_PRODUCTOBLL.ClassInstance.Get_K_PROVEEDOR_PRODUCTO_ByPK(strPro, idDepartment);
                if (unitDt != null && unitDt.Rows.Count > 0)
                {
                    txtGridText1.Text = unitDt.Rows[0]["Mt_Precio_Unitario"].ToString();
                    TextBox txtSubtotal = gvTecPro.Rows[0].FindControl("txtSubtotal") as TextBox;
                    TextBox txtCantidad = gvTecPro.Rows[0].FindControl("txtCantidad") as TextBox;

                    if (!string.IsNullOrEmpty(txtCantidad.Text) && !string.IsNullOrEmpty(txtGridText1.Text))
                    {
                        if (IsNumeric(txtCantidad.Text) && IsNumeric(txtGridText1.Text))
                        {
                            txtSubtotal.Text = (int.Parse(txtCantidad.Text) * decimal.Parse(txtGridText1.Text)).ToString();
                        }
                        else
                        {
                            txtSubtotal.Text = "0";
                        }
                        //edit by coco 2011-08-08
                        AccountTotal();
                        //end edit
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + ex.Message + "');", true);
            }
             */
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
                TextBox txtGridText1 = gvTecPro.Rows[RowIndex].FindControl("txtGridText1") as TextBox;
                TextBox txtSubtotal = gvTecPro.Rows[RowIndex].FindControl("txtSubtotal") as TextBox;
                TextBox txtCantidad = gvTecPro.Rows[RowIndex].FindControl("txtCantidad") as TextBox;
                #endregion
                //edit by coco 2011-08-10
                if (dtProduct.Rows.Count > 0)
                {
                    DropDownTechnology.SelectedValue = dtProduct.Rows[0]["Cve_Tecnologia"].ToString();
                    DropDownMarca.SelectedValue = dtProduct.Rows[0]["Cve_Marca"].ToString();
                    //add by coco 20110823
                    DropDownTypeOfProduct.SelectedValue = dtProduct.Rows[0]["Ft_Tipo_Producto"].ToString();
                    //end add
                }//end edit

                //Added by coco 2011-08-05
                //Reload product capacity
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
                //End add

                //get the unitary cost of the specific model
                //edit by tina 2012-07-25
                DataTable ProductDataTable = K_PROVEEDOR_PRODUCTOBLL.ClassInstance.Get_K_PROVEEDOR_PRODUCTO_ByPK(Product, idDepartment);
                if (ProductDataTable != null && ProductDataTable.Rows.Count > 0)
                {
                    txtGridText1.Text = ProductDataTable.Rows[0]["Mt_Precio_Unitario"].ToString();
                    if (!txtCantidad.Text.Equals("") && !txtGridText1.Text.Equals(""))
                    {
                        if (IsNumeric(txtCantidad.Text) && IsNumeric(txtGridText1.Text))
                        {
                            txtSubtotal.Text = (int.Parse(txtCantidad.Text) * decimal.Parse(txtGridText1.Text)).ToString();
                        }
                        else
                        {
                            txtSubtotal.Text = "0";
                        }
                    }
                    else
                    {
                        txtSubtotal.Text = "0";
                    }
                    //Re-calculate the total
                    AccountTotal();
                }
                else
                {
                    txtSubtotal.Text = "0";
                    txtGridText1.Text = "";
                }
                //Re-calculate the total
                AccountTotal();

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
            TextBox textBoxCantidad = (TextBox)sender;
            string CantidadText = textBoxCantidad.Text;

            if (IsNumeric(CantidadText) == false)
            {
                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "DataFormatIncorrect") as string;
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "NextError", "alert('" + strMsg + "');", true);
                return;
            }

            GridViewRow gridViewRow = (GridViewRow)textBoxCantidad.NamingContainer;

            TextBox txtSubtotal = gvTecPro.Rows[gridViewRow.RowIndex].FindControl("txtSubtotal") as TextBox;
            TextBox txtGridText1 = gvTecPro.Rows[gridViewRow.RowIndex].FindControl("txtGridText1") as TextBox;
            string strGridText1 = txtGridText1.Text;

            if (!CantidadText.Equals("") && !strGridText1.Equals(""))
            {
                if (IsNumeric(strGridText1))
                {
                    txtSubtotal.Text = (int.Parse(CantidadText) * decimal.Parse(strGridText1)).ToString();
                }
                else
                {
                    txtSubtotal.Text = "0";
                }
                //edit by coco 2011-08-08
                AccountTotal();
                //end edit
            }
        }
        #endregion
    }
}
