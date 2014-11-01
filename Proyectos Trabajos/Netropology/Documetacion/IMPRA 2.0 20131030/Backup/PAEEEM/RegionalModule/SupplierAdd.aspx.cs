using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.RegionalModule
{
    public partial class SupplierAdd : System.Web.UI.Page
    {
        #region Global Variables
        /// <summary>
        /// property
        /// </summary>
        private int SupplierID
        {
            get
            {
                return ViewState["SupplierID"] == null ? 0 : Convert.ToInt32(ViewState["SupplierID"].ToString());
            }
            set
            {
                ViewState["SupplierID"] = value;
            }
        }
        private string SupplierType
        {
            get
            {
                return ViewState["SupplierType"] == null ? "" : ViewState["SupplierType"].ToString();
            }
            set
            {
                ViewState["SupplierType"] = value;
            }
        }
        #endregion

        #region Initialize Components
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                //Init date control
                this.literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                //Init default data
                InitializeComponents();
                //Check is edit or new
                InitializeEditionData();
            }
        }

        private void InitializeEditionData()
        {
            if (Request.QueryString["SupplierID"] != null && Request.QueryString["SupplierID"].ToString() != "" && 
                        Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() != "")
            {
                SupplierID = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["SupplierID"].ToString().Replace("%2B", "+"))));
                SupplierType = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Type"].ToString().Replace("%2B", "+")));
                //Load current user information
                LoadCurrentSupplierInfo();
            }
        }

        private void InitializeComponents()
        {
            InitializeProveedor();
            InitializeEstado();
            InitializePartDelegMunicipio();
            InitializeFiscalDelegMunicipio();
            InitializeZone();

            lblSupplier.Visible = false;
            drpSupplier.Visible = false;
            drpSupplierRequired.Enabled = false;
            lblClave.Visible = false;
            txtClave.Visible = false;
            txtClaveRequired.Enabled = false;
            btnAssignProduct.Visible = false;
            btnAssignDisposal.Visible = false;
        }

        /// <summary>
        /// Initial Provider Data
        /// </summary>
        private void InitializeProveedor()
        {            
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            if (UserModel != null)
            {
                // RSA 20120927 todos los proveedores matriz
                // DataTable providers = CAT_PROVEEDORDal.ClassInstance.GetProveedorWithRegion(UserModel.Id_Departamento);
                DataTable providers = CAT_PROVEEDORDal.ClassInstance.GetProveedor();
                if (providers != null)
                {
                    drpSupplier.DataSource = providers;
                    drpSupplier.DataTextField = "Dx_Razon_Social";
                    drpSupplier.DataValueField = "Id_Proveedor";
                    drpSupplier.DataBind();
                    drpSupplier.Items.Insert(0, "");
                    drpSupplier.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Initial Estado Data
        /// </summary>
        private void InitializeEstado()
        {
            DataTable statesOfMexico = CAT_ESTADOBLL.ClassInstance.Get_All_CAT_ESTADO();
            //Init estado controls
            if (statesOfMexico != null && statesOfMexico.Rows.Count > 0)
            {
                //Init part estado
                drpPartEstado.DataSource = statesOfMexico;
                drpPartEstado.DataTextField = "Dx_Nombre_Estado";
                drpPartEstado.DataValueField = "Cve_Estado";
                drpPartEstado.DataBind();
                drpPartEstado.Items.Insert(0, "");
                drpPartEstado.SelectedIndex = 0;
                //Init fiscal estado
                drpFiscalEstado.DataSource = statesOfMexico;
                drpFiscalEstado.DataTextField = "Dx_Nombre_Estado";
                drpFiscalEstado.DataValueField = "Cve_Estado";
                drpFiscalEstado.DataBind();
                drpFiscalEstado.Items.Insert(0, "");
                drpFiscalEstado.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Initial Part Dele Municipio
        /// </summary>
        private void InitializePartDelegMunicipio()
        {
            int Cve_Estado = (drpPartEstado.SelectedIndex == 0 || drpPartEstado.SelectedIndex == -1) ? -1 : int.Parse(drpPartEstado.SelectedValue);

            DataTable citiesOfMexico = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Cve_Estado);
            //Init part deleg municipio controls
            if (citiesOfMexico != null)
            {
                //Init part deleg municipio
                drpPartDelegMunicipio.DataSource = citiesOfMexico;
                drpPartDelegMunicipio.DataTextField = "Dx_Deleg_Municipio";
                drpPartDelegMunicipio.DataValueField = "Cve_Deleg_Municipio";
                drpPartDelegMunicipio.DataBind();
                drpPartDelegMunicipio.Items.Insert(0, "");
                drpPartDelegMunicipio.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Initial Fiscal Dele Municipio
        /// </summary>
        private void InitializeFiscalDelegMunicipio()
        {
            int Cve_Estado = (drpFiscalEstado.SelectedIndex == 0 || drpFiscalEstado.SelectedIndex == -1) ? -1 : int.Parse(drpFiscalEstado.SelectedValue);

            DataTable citiesOfMexico = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Cve_Estado);
            //Init fiscal deleg municipio controls
            if (citiesOfMexico != null)
            {
                //Init fiscal deleg municipio
                drpFiscalDelegMunicipio.DataSource = citiesOfMexico;
                drpFiscalDelegMunicipio.DataTextField = "Dx_Deleg_Municipio";
                drpFiscalDelegMunicipio.DataValueField = "Cve_Deleg_Municipio";
                drpFiscalDelegMunicipio.DataBind();
                drpFiscalDelegMunicipio.Items.Insert(0, "");
                drpFiscalDelegMunicipio.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Initial Zone Data
        /// </summary>
        private void InitializeZone()
        {            
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel; 

            if (UserModel != null)
            {
                DataTable zonesOfMexico = CatZonaDal.ClassInstance.GetZonaWithRegional(UserModel.Id_Departamento);
                if (zonesOfMexico != null)
                {
                    drpZone.DataSource = zonesOfMexico;
                    drpZone.DataTextField = "Dx_Nombre_Zona";
                    drpZone.DataValueField = "Cve_Zona";
                    drpZone.DataBind();
                    drpZone.Items.Insert(0, "");
                    drpZone.SelectedIndex = 0;
                }
            }
        }

        private void LoadCurrentSupplierInfo()
        {
            DataTable dtSupplierInfo = null;

            if (SupplierType.ToLower() == "proveedor")
            {
                dtSupplierInfo = CAT_PROVEEDORDal.ClassInstance.GetSupplierByPk(SupplierID);
            }
            else
            {
                dtSupplierInfo = SupplierBrancheDal.ClassInstance.GetSupplierBranch(SupplierID);
            }

            if (dtSupplierInfo != null && dtSupplierInfo.Rows.Count > 0)
            {
                if (SupplierType.ToLower() == "branch")
                {
                    ckbBranch.Checked = true;
                    ckbBranch.Enabled = false;
                    lblSupplier.Visible = true;
                    drpSupplier.Visible = true;
                    drpSupplierRequired.Enabled = true;
                    drpSupplier.SelectedValue = dtSupplierInfo.Rows[0]["Id_Proveedor"].ToString();
                    txtClave.Text = dtSupplierInfo.Rows[0]["Id_Branch"].ToString();
                }
                else
                {
                    if (Convert.ToInt32(dtSupplierInfo.Rows[0]["Cve_Estatus_Proveedor"].ToString()) == (int)ProviderStatus.ACTIVO)
                    {
                        btnAssignProduct.Visible = true;
                    }
                    ckbBranch.Visible = false;
                    lblSupplier.Visible = false;
                    drpSupplier.Visible = false;
                    drpSupplierRequired.Enabled = false;
                    txtClave.Text = dtSupplierInfo.Rows[0]["Id_Proveedor"].ToString();
                }

                txtSocialName.Text = dtSupplierInfo.Rows[0]["Dx_Razon_Social"].ToString();
                txtBusinessName.Text = dtSupplierInfo.Rows[0]["Dx_Nombre_Comercial"].ToString();
                txtRFC.Text = dtSupplierInfo.Rows[0]["Dx_RFC"].ToString();
                txtPartCalle.Text = dtSupplierInfo.Rows[0]["Dx_Domicilio_Part_Calle"].ToString();
                txtPartNum.Text = dtSupplierInfo.Rows[0]["Dx_Domicilio_Part_Num"].ToString();
                txtPartCP.Text = dtSupplierInfo.Rows[0]["Dx_Domicilio_Part_CP"].ToString();
                drpPartEstado.SelectedValue = dtSupplierInfo.Rows[0]["Cve_Estado_Part"].ToString();
                drpPartDelegMunicipio.SelectedValue = dtSupplierInfo.Rows[0]["Cve_Deleg_Municipio_Part"].ToString();

                if (dtSupplierInfo.Rows[0]["Fg_Mismo_Domicilio"] != DBNull.Value)
                {
                    if (dtSupplierInfo.Rows[0]["Fg_Mismo_Domicilio"].ToString() == "True")
                    {
                        ckbFiscal.Checked = true;
                    }
                    else
                    {
                        ckbFiscal.Checked = false;
                    }
                }

                txtFiscalCalle.Text = dtSupplierInfo.Rows[0]["Dx_Domicilio_Fiscal_Calle"].ToString();
                txtFiscalNum.Text = dtSupplierInfo.Rows[0]["Dx_Domicilio_Fiscal_Num"].ToString();
                txtFiscalCP.Text = dtSupplierInfo.Rows[0]["Dx_Domicilio_Fiscal_CP"].ToString();
                drpFiscalEstado.SelectedValue = dtSupplierInfo.Rows[0]["Cve_Estado_Fisc"].ToString();
                drpFiscalDelegMunicipio.SelectedValue = dtSupplierInfo.Rows[0]["Cve_Deleg_Municipio_Fisc"].ToString();
                txtRepresentativeName.Text = dtSupplierInfo.Rows[0]["Dx_Nombre_Repre"].ToString();
                txtRepresentativeEmail.Text = dtSupplierInfo.Rows[0]["Dx_Email_Repre"].ToString();
                txtTelephone.Text = dtSupplierInfo.Rows[0]["Dx_Telefono_Repre"].ToString();
                txtLegalName.Text = dtSupplierInfo.Rows[0]["Dx_Nombre_Repre_Legal"].ToString();
                txtBankName.Text = dtSupplierInfo.Rows[0]["Dx_Nombre_Banco"].ToString();
                txtBankAccount.Text = dtSupplierInfo.Rows[0]["Dx_Cuenta_Banco"].ToString();
                //updated by tina 2012-07-17
                drpRate.SelectedValue = dtSupplierInfo.Rows[0]["Pct_Tasa_IVA"].ToString();
                //end                
                drpZone.SelectedValue = dtSupplierInfo.Rows[0]["Cve_Zona"].ToString();
                //added by tina 2012-07-17
                txtBankName.Enabled = false;
                txtBankAccount.Enabled = false;
                //end
            }
        }
        #endregion

        #region Button Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool success = false;
            string filePath = "\\DisposalCenterAndSupplierImage\\";

            Page.Validate();

            if (Page.IsValid)
            {
                if (IsSupplierData())
                {
                    if (NewAdded() && ExistsRFC())
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('El RFC ya fue dado de alta.');", true);
                        return;
                    }
                    else if (NewAdded() && ExistsSupplier())
                    {
                        // RSA 20120927 avoid duplicated
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('El proveedor ya fue dado de alta.');", true);
                        return;
                    }
                    else
                        success = SaveSupplier(filePath);
                }
                else
                {
                    if (drpSupplier.SelectedIndex != -1 && drpSupplier.SelectedIndex != 0)
                    {
                        if (NewAdded() && ExistsSupplierBranch())
                        {
                            // RSA 20120927 avoid duplicated
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('El proveedor ya fue dado de alta.');", true);
                            return;
                        }
                        else
                            success = SaveSupplierBranch(filePath);
                    }
                }
            }

            if (success)
            {
                //create image folder
                CreateImageDirectory(filePath);
                //save file to server
                if (fileUploadAttorneyPower.HasFile)
                {
                    fileUploadAttorneyPower.SaveAs(Server.MapPath(filePath) + DateTime.Now.ToString("yyyyMMdd-HHmmss_A_") + this.fileUploadAttorneyPower.FileName);
                }

                if (fileUploadConstitutiva.HasFile)
                {
                    fileUploadConstitutiva.SaveAs(Server.MapPath(filePath) + DateTime.Now.ToString("yyyyMMdd-HHmmss_C_") + this.fileUploadConstitutiva.FileName);
                }

                //updated by tina 2012/04/27
                if (IsSupplierData() && NewAdded())
                {
                    InitializeProveedor();
                }
                if (NewAdded())
                {
                    //CLEAR DATA
                    ClearData();
                }
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "savesuccess", "alert('Los datos del proveedor fueron guardados correctamente.');", true);
                //end               
            }
            else 
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('Los datos del proveedor no pudieron ser guardados, verifique.');", true);
        }

        protected void btnAssignProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssignProductToSupplier.aspx?SupplierID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(SupplierID.ToString())).Replace("+", "%2B"));
        }

        protected void btnAssignDisposal_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssignDisposalToSupplier.aspx?SupplierID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(SupplierID.ToString())).Replace("+", "%2B") +
                                                                              "&Type=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(SupplierType)).Replace("+", "%2B"));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupplierMonitor.aspx");
        }
        #endregion

        #region Private Methods
        private bool NewAdded()
        {
            bool result = false;
            if (SupplierID == 0 && SupplierType == "")
            {
                result = true;
            }
            return result;
        }

        private bool IsSupplierData()
        {
            bool result = false;
            if (SupplierType.ToLower() == "proveedor" || ckbBranch.Checked == false)
            {
                result = true;
            }
            return result;
        }

        private CAT_PROVEEDORModel GetSupplierData(string filePath)
        {
            CAT_PROVEEDORModel model = new CAT_PROVEEDORModel();

            if (Session["UserInfo"] != null)
            {
                US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
                model.Cve_Region = UserModel.Id_Departamento;
            }

            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Dx_Nombre_Comercial = txtBusinessName.Text.Trim() == "" ? "" : txtBusinessName.Text.Trim();
            model.Dx_RFC = txtRFC.Text.Trim() == "" ? "" : txtRFC.Text.Trim();
            model.Dx_Domicilio_Part_Calle = txtPartCalle.Text.Trim() == "" ? "" : txtPartCalle.Text.Trim();
            model.Dx_Domicilio_Part_Num = txtPartNum.Text.Trim() == "" ? "" : txtPartNum.Text.Trim();
            model.Dx_Domicilio_Part_CP = txtPartCP.Text.Trim() == "" ? "" : txtPartCP.Text.Trim();

            if (drpPartEstado.SelectedIndex != 0 && drpPartEstado.SelectedIndex != -1)
            {
                model.Cve_Estado_Part = Convert.ToInt32(drpPartEstado.SelectedValue.ToString());
            }
            if (drpPartDelegMunicipio.SelectedIndex != 0 && drpPartDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Part = Convert.ToInt32(drpPartDelegMunicipio.SelectedValue.ToString());
            }

            model.Fg_Mismo_Domicilio = ckbFiscal.Checked == true ? true : false;
            model.Dx_Domicilio_Fiscal_Calle = txtFiscalCalle.Text.Trim() == "" ? "" : txtFiscalCalle.Text.Trim();
            model.Dx_Domicilio_Fiscal_Num = txtFiscalNum.Text.Trim() == "" ? "" : txtFiscalNum.Text.Trim();
            model.Dx_Domicilio_Fiscal_CP = txtFiscalCP.Text.Trim() == "" ? "" : txtFiscalCP.Text.Trim();

            if (drpFiscalEstado.SelectedIndex != 0 && drpFiscalEstado.SelectedIndex != -1)
            {
                model.Cve_Estado_Fisc = Convert.ToInt32(drpFiscalEstado.SelectedValue.ToString());
            }
            if (drpFiscalDelegMunicipio.SelectedIndex != 0 && drpFiscalDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Fisc = Convert.ToInt32(drpFiscalDelegMunicipio.SelectedValue.ToString());
            }

            model.Dx_Nombre_Repre = txtRepresentativeName.Text.Trim() == "" ? "" : txtRepresentativeName.Text.Trim();
            model.Dx_Email_Repre = txtRepresentativeEmail.Text.Trim() == "" ? "" : txtRepresentativeEmail.Text.Trim();
            model.Dx_Telefono_Repre = txtTelephone.Text.Trim() == "" ? "" : txtTelephone.Text.Trim();
            model.Dx_Nombre_Repre_Legal = txtLegalName.Text.Trim() == "" ? "" : txtLegalName.Text.Trim();
            model.Dx_Nombre_Banco = txtBankName.Text.Trim() == "" ? "" : txtBankName.Text.Trim();
            model.Dx_Cuenta_Banco = txtBankAccount.Text.Trim() == "" ? "" : txtBankAccount.Text.Trim();

            //updated by tina 2012-07-17
            if (drpRate.SelectedIndex > 0)
            {
                model.Pct_Tasa_IVA = Convert.ToDouble(drpRate.SelectedValue);
            }
            //end

            model.Dt_Fecha_Proveedor = DateTime.Now;

            if (drpZone.SelectedIndex != 0 && drpZone.SelectedIndex != -1)
            {
                model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue.ToString());
            }

            if (this.fileUploadConstitutiva.HasFile)
            {
                model.Binary_Acta_Constitutiva = Path.Combine(filePath, DateTime.Now.ToString("yyyyMMdd-HHmmss_C_") + this.fileUploadConstitutiva.FileName);
            }

            if (this.fileUploadAttorneyPower.HasFile)
            {
                model.Binary_Poder_Notarial = Path.Combine(filePath, DateTime.Now.ToString("yyyyMMdd-HHmmss_A_") + this.fileUploadAttorneyPower.FileName);
            }

            if (NewAdded())
            {
                model.Cve_Estatus_Proveedor = (int)ProviderStatus.PENDIENTE;
                model.Codigo_Proveedor = string.Format("{0:D3}", LsUtility.GetNumberSequence("PROVEEDOR"));
            }
            else
            {
                model.Id_Proveedor = Convert.ToInt32(txtClave.Text);
            }

            return model;
        }

        private SupplierBranchModel GetSupplierBranchData(string filePath)
        {
            SupplierBranchModel model = new SupplierBranchModel();

            if (drpSupplier.SelectedIndex != 0 && drpSupplier.SelectedIndex != -1)
            {
                model.Id_Proveedor = Convert.ToInt32(drpSupplier.SelectedValue);
            }

            if (Session["UserInfo"] != null)
            {
                US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
                model.Cve_Region = UserModel.Id_Departamento;
            }

            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Dx_Nombre_Comercial = txtBusinessName.Text.Trim() == "" ? "" : txtBusinessName.Text.Trim();
            model.Dx_RFC = txtRFC.Text.Trim() == "" ? "" : txtRFC.Text.Trim();
            model.Dx_Domicilio_Part_Calle = txtPartCalle.Text.Trim() == "" ? "" : txtPartCalle.Text.Trim();
            model.Dx_Domicilio_Part_Num = txtPartNum.Text.Trim() == "" ? "" : txtPartNum.Text.Trim();
            model.Dx_Domicilio_Part_CP = txtPartCP.Text.Trim() == "" ? "" : txtPartCP.Text.Trim();

            if (drpPartEstado.SelectedIndex != 0 && drpPartEstado.SelectedIndex != -1)
            {
                model.Cve_Estado_Part = Convert.ToInt32(drpPartEstado.SelectedValue.ToString());
            }

            if (drpPartDelegMunicipio.SelectedIndex != 0 && drpPartDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Part = Convert.ToInt32(drpPartDelegMunicipio.SelectedValue.ToString());
            }

            model.Fg_Mismo_Domicilio = ckbFiscal.Checked == true ? true : false;
            model.Dx_Domicilio_Fiscal_Calle = txtFiscalCalle.Text.Trim() == "" ? "" : txtFiscalCalle.Text.Trim();
            model.Dx_Domicilio_Fiscal_Num = txtFiscalNum.Text.Trim() == "" ? "" : txtFiscalNum.Text.Trim();
            model.Dx_Domicilio_Fiscal_CP = txtFiscalCP.Text.Trim() == "" ? "" : txtFiscalCP.Text.Trim();

            if (drpFiscalEstado.SelectedIndex != 0 && drpFiscalEstado.SelectedIndex != -1)
            {
                model.Cve_Estado_Fisc = Convert.ToInt32(drpFiscalEstado.SelectedValue.ToString());
            }
            if (drpFiscalDelegMunicipio.SelectedIndex != 0 && drpFiscalDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Fisc = Convert.ToInt32(drpFiscalDelegMunicipio.SelectedValue.ToString());
            }

            model.Dx_Nombre_Repre = txtRepresentativeName.Text.Trim() == "" ? "" : txtRepresentativeName.Text.Trim();
            model.Dx_Email_Repre = txtRepresentativeEmail.Text.Trim() == "" ? "" : txtRepresentativeEmail.Text.Trim();
            model.Dx_Telefono_Repre = txtTelephone.Text.Trim() == "" ? "" : txtTelephone.Text.Trim();
            model.Dx_Nombre_Repre_Legal = txtLegalName.Text.Trim() == "" ? "" : txtLegalName.Text.Trim();
            model.Dx_Nombre_Banco = txtBankName.Text.Trim() == "" ? "" : txtBankName.Text.Trim();
            model.Dx_Cuenta_Banco = txtBankAccount.Text.Trim() == "" ? "" : txtBankAccount.Text.Trim();

            //updated by tina 2012-07-17
            if (drpRate.SelectedIndex > 0)
            {
                model.Pct_Tasa_IVA = float.Parse(drpRate.SelectedValue);
            }
            //end

            model.Dt_Fecha_Branch = DateTime.Now;

            if (drpZone.SelectedIndex != 0 && drpZone.SelectedIndex != -1)
            {
                model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue.ToString());
            }

            if (this.fileUploadConstitutiva.HasFile)
            {
                model.Binary_Acta_Constitutiva = Path.Combine(filePath, DateTime.Now.ToString("yyyyMMdd-HHmmss_C_") + this.fileUploadConstitutiva.FileName);
            }

            if (this.fileUploadAttorneyPower.HasFile)
            {
                model.Binary_Poder_Notarial = Path.Combine(filePath, DateTime.Now.ToString("yyyyMMdd-HHmmss_A_") + this.fileUploadAttorneyPower.FileName);
            }

            if (NewAdded())
            {
                model.Cve_Estatus_Proveedor = (int)ProviderStatus.PENDIENTE;
                model.Codigo_Branch = string.Format("{0:D3}", LsUtility.GetNumberSequence("BRANCHPRO"));
            }
            else
            {
                model.Id_Branch = Convert.ToInt32(txtClave.Text);
            }

            return model;
        }

        private void CreateImageDirectory(string filePath)
        {
            try
            {
                if (filePath != "")
                {
                    //check if folder is exist, if not create it
                    if (!Directory.Exists(Server.MapPath(filePath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(filePath));
                    }
                }
            }
            catch (Exception ex)
            {
                string message = "Crear un directorio de imágenes no.\r\n" + ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "IOError", "alert(" + message + ");");
            }
        }

        private bool SaveSupplier(string filePath)
        {
            bool success = false;
            CAT_PROVEEDORModel model = GetSupplierData(filePath);

            if (NewAdded())
            {
                success = CAT_PROVEEDORDal.ClassInstance.Insert_CAT_PROVEEDOR(model) > 0 ? true : false;
            }
            else
            {
                success = CAT_PROVEEDORDal.ClassInstance.Update_CAT_PROVEEDOR(model) > 0 ? true : false;
            }

            return success;
        }

        private bool SaveSupplierBranch(string filePath)
        {
            bool success = false;
            SupplierBranchModel model = GetSupplierBranchData(filePath);

            if (NewAdded())
            {
                success = SupplierBrancheDal.ClassInstance.Insert_CAT_PROVEEDORBRANCH(model) > 0 ? true : false;
            }
            else
            {
                success = SupplierBrancheDal.ClassInstance.Update_CAT_PROVEEDOR(model) > 0 ? true : false;
            }

            return success;
        }

        private bool ExistsSupplier()
        {
            bool valid = false;

            CAT_PROVEEDORModel model = new CAT_PROVEEDORModel();

            if (Session["UserInfo"] != null)
            {
                US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
                model.Cve_Region = UserModel.Id_Departamento;
            }

            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue.ToString());

            valid = CAT_PROVEEDORDal.ClassInstance.Select_CAT_PROVEEDOR(model) > 0 ? true : false;


            return valid;
        }

        private bool ExistsSupplierBranch()
        {
            bool valid = false;

            SupplierBranchModel model = new SupplierBranchModel();

            if (Session["UserInfo"] != null)
            {
                US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
                model.Cve_Region = UserModel.Id_Departamento;
            }

            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue.ToString());

            // valid = SupplierBrancheDal.ClassInstance.Select_CAT_PROVEEDOR(model) > 0 ? true : false;


            return valid;
        }

        private bool ExistsRFC()
        {
            bool valid = false;

            CAT_PROVEEDORModel model = new CAT_PROVEEDORModel();

            model.Dx_RFC = txtRFC.Text;

            valid = CAT_PROVEEDORDal.ClassInstance.Select_CAT_PROVEEDOR_RFC(model) > 0 ? true : false;

            return valid;
        }

        private void ClearData()
        {
            this.ckbBranch.Checked = false;
            this.drpSupplier.SelectedIndex = 0;
            this.lblSupplier.Visible = false;//added by tina 2012/04/27
            this.drpSupplier.Visible = false;//added by tina 2012/04/27
            this.drpSupplierRequired.Enabled = false;
            this.txtSocialName.Text = "";
            this.txtBusinessName.Text = "";
            this.txtRFC.Text = "";
            this.txtPartCalle.Text = "";
            this.txtPartNum.Text = "";
            this.txtPartCP.Text = "";
            this.drpPartEstado.SelectedIndex = 0;
            this.drpPartDelegMunicipio.SelectedIndex = 0;
            this.ckbFiscal.Checked = false;
            this.txtFiscalCalle.Text = "";
            this.txtFiscalCP.Text = "";
            this.txtFiscalNum.Text = "";
            this.drpFiscalEstado.SelectedIndex = 0;
            this.drpFiscalDelegMunicipio.SelectedIndex = 0;
            this.txtRepresentativeName.Text = "";
            this.txtRepresentativeEmail.Text = "";
            this.txtTelephone.Text = "";
            this.txtLegalName.Text = "";
            //updated by tina 2012-07-17
            drpRate.SelectedIndex = 0;
            //end
            this.txtBankName.Text = "";
            this.txtBankAccount.Text = "";
            this.drpZone.SelectedIndex = 0;
        }
        #endregion

        #region Controls Changed Events
        protected void ckbBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ckbBranch.Checked)
                {
                    lblSupplier.Visible = true;
                    drpSupplier.Visible = true;
                    drpSupplierRequired.Enabled = true;
                    lblCaptura.Text = "Nombre o Razón Social (Sucursal)";
                }
                else
                {
                    lblSupplier.Visible = false;
                    drpSupplier.Visible = false;
                    drpSupplierRequired.Enabled = false;
                    lblCaptura.Text = "Nombre o Razón Social (Matriz)";
                }
            }
        }

        protected void drpPartEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                InitializePartDelegMunicipio();
            }
        }

        protected void drpFiscalEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                InitializeFiscalDelegMunicipio();
            }
        }

        protected void ckbFiscal_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ckbFiscal.Checked)
                {
                    txtFiscalCalle.Text = txtPartCalle.Text;
                    txtFiscalNum.Text = txtPartNum.Text;
                    txtFiscalCP.Text = txtPartCP.Text;
                    if (drpFiscalEstado.SelectedValue != drpPartEstado.SelectedValue)
                    {
                        drpFiscalEstado.SelectedValue = drpPartEstado.SelectedValue;
                        InitializeFiscalDelegMunicipio();                        
                    }
                    drpFiscalDelegMunicipio.SelectedValue = drpPartDelegMunicipio.SelectedValue;
                }
                else
                {
                    txtFiscalCalle.Text = "";
                    txtFiscalNum.Text = "";
                    txtFiscalCP.Text = "";
                    drpFiscalEstado.SelectedIndex = 0;
                    InitializeFiscalDelegMunicipio();
                    drpFiscalDelegMunicipio.SelectedIndex = 0;
                }
            }
        }
        #endregion 
    }
}
