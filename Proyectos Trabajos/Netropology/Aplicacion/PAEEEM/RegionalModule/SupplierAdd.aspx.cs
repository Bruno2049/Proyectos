using System;
using System.Globalization;
using System.Web.UI;
using System.IO;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.RegionalModule
{
    public partial class SupplierAdd : Page
    {
        #region Global Variables
        /// <summary>
        /// property
        /// </summary>
        private int SupplierId
        {
            get
            {
                return ViewState["SupplierId"] == null ? 0 : Convert.ToInt32(ViewState["SupplierId"].ToString());
            }
            set
            {
                ViewState["SupplierId"] = value;
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
            if (IsPostBack) return;
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }

            //Init date control
            literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //Init default data
            InitializeComponents();
            //Check is edit or new
            InitializeEditionData();
        }

        private void InitializeEditionData()
        {
            if (Request.QueryString["SupplierID"] == null || Request.QueryString["SupplierID"] == "" ||
                Request.QueryString["Type"] == null || Request.QueryString["Type"] == "") return;

            SupplierId = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["SupplierID"].Replace("%2B", "+"))));
            SupplierType = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Type"].Replace("%2B", "+")));
            //Load current user information
            LoadCurrentSupplierInfo();
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
            var userModel = Session["UserInfo"] as US_USUARIOModel;

            if (userModel == null) return;
            // RSA 20120927 todos los proveedores matriz
            // DataTable providers = CAT_PROVEEDORDal.ClassInstance.GetProveedorWithRegion(UserModel.Id_Departamento);
            var providers = CAT_PROVEEDORDal.ClassInstance.GetProveedor();
            if (providers == null) return;
            drpSupplier.DataSource = providers;
            drpSupplier.DataTextField = "Dx_Razon_Social";
            drpSupplier.DataValueField = "Id_Proveedor";
            drpSupplier.DataBind();
            drpSupplier.Items.Insert(0, "");
            drpSupplier.SelectedIndex = 0;
        }

        /// <summary>
        /// Initial Estado Data
        /// </summary>
        private void InitializeEstado()
        {
            var statesOfMexico = CAT_ESTADOBLL.ClassInstance.Get_All_CAT_ESTADO();
            //Init estado controls
            if (statesOfMexico == null || statesOfMexico.Rows.Count <= 0) return;
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

        /// <summary>
        /// Initial Part Dele Municipio
        /// </summary>
        private void InitializePartDelegMunicipio()
        {
            var cveEstado = (drpPartEstado.SelectedIndex == 0 || drpPartEstado.SelectedIndex == -1) ? -1 : int.Parse(drpPartEstado.SelectedValue);

            var citiesOfMexico = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(cveEstado);
            //Init part deleg municipio controls
            if (citiesOfMexico == null) return;
            //Init part deleg municipio
            drpPartDelegMunicipio.DataSource = citiesOfMexico;
            drpPartDelegMunicipio.DataTextField = "Dx_Deleg_Municipio";
            drpPartDelegMunicipio.DataValueField = "Cve_Deleg_Municipio";
            drpPartDelegMunicipio.DataBind();
            drpPartDelegMunicipio.Items.Insert(0, "");
            drpPartDelegMunicipio.SelectedIndex = 0;
        }

        /// <summary>
        /// Initial Fiscal Dele Municipio
        /// </summary>
        private void InitializeFiscalDelegMunicipio()
        {
            var cveEstado = (drpFiscalEstado.SelectedIndex == 0 || drpFiscalEstado.SelectedIndex == -1) ? -1 : int.Parse(drpFiscalEstado.SelectedValue);

            var citiesOfMexico = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(cveEstado);
            //Init fiscal deleg municipio controls
            if (citiesOfMexico == null) return;
            //Init fiscal deleg municipio
            drpFiscalDelegMunicipio.DataSource = citiesOfMexico;
            drpFiscalDelegMunicipio.DataTextField = "Dx_Deleg_Municipio";
            drpFiscalDelegMunicipio.DataValueField = "Cve_Deleg_Municipio";
            drpFiscalDelegMunicipio.DataBind();
            drpFiscalDelegMunicipio.Items.Insert(0, "");
            drpFiscalDelegMunicipio.SelectedIndex = 0;
        }

        /// <summary>
        /// Initial Zone Data
        /// </summary>
        private void InitializeZone()
        {            
            var userModel = Session["UserInfo"] as US_USUARIOModel;

            if (userModel == null) return;

            var zonesOfMexico = CatZonaDal.ClassInstance.GetZonaWithRegional(userModel.Id_Departamento);
            if (zonesOfMexico == null) return;
            drpZone.DataSource = zonesOfMexico;
            drpZone.DataTextField = "Dx_Nombre_Zona";
            drpZone.DataValueField = "Cve_Zona";
            drpZone.DataBind();
            drpZone.Items.Insert(0, "");
            drpZone.SelectedIndex = 0;
        }

        private void LoadCurrentSupplierInfo()
        {
            var dtSupplierInfo = SupplierType.ToLower() == "proveedor" ? CAT_PROVEEDORDal.ClassInstance.GetSupplierByPk(SupplierId) : SupplierBrancheDal.ClassInstance.GetSupplierBranch(SupplierId);

            if (dtSupplierInfo == null || dtSupplierInfo.Rows.Count <= 0) return;

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
                ckbFiscal.Checked = dtSupplierInfo.Rows[0]["Fg_Mismo_Domicilio"].ToString() == "True";
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
            txtMotivos.Visible = true;
            lblMotivos.Visible = true;
        }

        #endregion

        #region Button Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var idAfectado = 0;
            var tipoModificacion = 0; // 1 = Insert, 2 = Update
            Object datosAnteriores = null;
            Object datosActuales = null;
            var success = false;
            const string filePath = "\\DisposalCenterAndSupplierImage\\";

            Page.Validate();

            if (Page.IsValid)
            {
                if (IsSupplierData())
                {
                    if (NewAdded() && ExistsRfc())
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "savefail", "alert('El RFC ya fue dado de alta.');", true);
                        return;
                    }
                    if (NewAdded() && ExistsSupplier())
                    {
                        // RSA 20120927 avoid duplicated
                        ScriptManager.RegisterStartupScript(Page, GetType(), "savefail", "alert('El Proveedor ya fue dado de alta.');", true);
                        return;
                    }
                    if (!NewAdded())
                    {
                        if (txtMotivos.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "savefail",
                                "alert('¡Debe ingresar el motivo por el cual esta realizando cambios al Proveedor!')",
                                true);
                            return;
                        }
                    }
                    success = SaveSupplier(filePath, ref idAfectado, ref tipoModificacion, ref datosAnteriores, ref datosActuales);
                   
                }
                else
                {
                    if (drpSupplier.SelectedIndex != -1 && drpSupplier.SelectedIndex != 0)
                    {
                        if (NewAdded() && ExistsSupplierBranch())
                        {
                            // RSA 20120927 avoid duplicated
                            ScriptManager.RegisterStartupScript(Page, GetType(), "savefail", "alert('El proveedor ya fue dado de alta.');", true);
                            return;
                        }
                        if (!NewAdded())
                        {
                            if (txtMotivos.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "savefail",
                                    "alert('¡Debe ingresar el motivo por el cual esta realizando cambios al Proveedor!')",
                                    true);
                                return;
                            }
                        }
                        success = SaveSupplierBranch(filePath, ref idAfectado, ref tipoModificacion, ref datosAnteriores, ref datosActuales);
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
                    fileUploadAttorneyPower.SaveAs(Server.MapPath(filePath) +
                                                   DateTime.Now.ToString("yyyyMMdd-HHmmss_A_") +
                                                   fileUploadAttorneyPower.FileName);
                }

                if (fileUploadConstitutiva.HasFile)
                {
                    fileUploadConstitutiva.SaveAs(Server.MapPath(filePath) + DateTime.Now.ToString("yyyyMMdd-HHmmss_C_") +
                                                  fileUploadConstitutiva.FileName);
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
                switch (tipoModificacion)
                {
                    case 1://INSERT
                        /*INSERTAR EVENTO ALTA DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "EMPRESAS", "ALTA", idAfectado.ToString(CultureInfo.InvariantCulture),
                            "", "Fecha alta: " + DateTime.Now, "", "");

                        ScriptManager.RegisterStartupScript(Page, GetType(), "savesuccess","alert('Los datos del Proveedor fueron guardados correctamente.');", true);
                        break;

                    case 2://CAMBIOS
                        /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                        var cambioDatos = Insertlog.GetCambiosDatos(datosAnteriores, datosActuales);
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "EMPRESAS", "CAMBIOS", idAfectado.ToString(CultureInfo.InvariantCulture),
                            txtMotivos.Text, "", cambioDatos[0], cambioDatos[1]);

                         ScriptManager.RegisterStartupScript(Page, GetType(), "savesuccess","alert('Los datos del Proveedor fueron guardados correctamente.');", true);
                        break;
                }
                //end               
            }
            else
                ScriptManager.RegisterStartupScript(Page, GetType(), "savefail",
                    "alert('Los datos del Proveedor NO pudieron ser guardados. Verifique.');", true);
        }

        protected void btnAssignProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssignProductToSupplier.aspx?SupplierID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(SupplierId.ToString(CultureInfo.InvariantCulture))).Replace("+", "%2B"));
        }

        protected void btnAssignDisposal_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssignDisposalToSupplier.aspx?SupplierID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(SupplierId.ToString(CultureInfo.InvariantCulture))).Replace("+", "%2B") +
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
            var result = SupplierId == 0 && SupplierType == "";
            return result;
        }

        private bool IsSupplierData()
        {
            var result = SupplierType.ToLower() == "proveedor" || ckbBranch.Checked == false;
            return result;
        }

        private CAT_PROVEEDORModel GetSupplierData(string filePath)
        {
            var model = new CAT_PROVEEDORModel();

            if (Session["UserInfo"] != null)
            {
                var userModel = Session["UserInfo"] as US_USUARIOModel;
                if (userModel != null) model.Cve_Region = userModel.Id_Departamento;
            }

            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Dx_Nombre_Comercial = txtBusinessName.Text.Trim() == "" ? "" : txtBusinessName.Text.Trim();
            model.Dx_RFC = txtRFC.Text.Trim() == "" ? "" : txtRFC.Text.Trim();
            model.Dx_Domicilio_Part_Calle = txtPartCalle.Text.Trim() == "" ? "" : txtPartCalle.Text.Trim();
            model.Dx_Domicilio_Part_Num = txtPartNum.Text.Trim() == "" ? "" : txtPartNum.Text.Trim();
            model.Dx_Domicilio_Part_CP = txtPartCP.Text.Trim() == "" ? "" : txtPartCP.Text.Trim();

            if (drpPartEstado.SelectedIndex != 0 && drpPartEstado.SelectedIndex != -1)
            {
                model.Cve_Estado_Part = Convert.ToInt32(drpPartEstado.SelectedValue);
            }
            if (drpPartDelegMunicipio.SelectedIndex != 0 && drpPartDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Part = Convert.ToInt32(drpPartDelegMunicipio.SelectedValue);
            }

            model.Fg_Mismo_Domicilio = ckbFiscal.Checked;
            model.Dx_Domicilio_Fiscal_Calle = txtFiscalCalle.Text.Trim() == "" ? "" : txtFiscalCalle.Text.Trim();
            model.Dx_Domicilio_Fiscal_Num = txtFiscalNum.Text.Trim() == "" ? "" : txtFiscalNum.Text.Trim();
            model.Dx_Domicilio_Fiscal_CP = txtFiscalCP.Text.Trim() == "" ? "" : txtFiscalCP.Text.Trim();

            if (drpFiscalEstado.SelectedIndex != 0 && drpFiscalEstado.SelectedIndex != -1)
            {
                model.Cve_Estado_Fisc = Convert.ToInt32(drpFiscalEstado.SelectedValue);
            }
            if (drpFiscalDelegMunicipio.SelectedIndex != 0 && drpFiscalDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Fisc = Convert.ToInt32(drpFiscalDelegMunicipio.SelectedValue);
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
                model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue);
            }

            if (fileUploadConstitutiva.HasFile)
            {
                model.Binary_Acta_Constitutiva = Path.Combine(filePath, DateTime.Now.ToString("yyyyMMdd-HHmmss_C_") + fileUploadConstitutiva.FileName);
            }

            if (fileUploadAttorneyPower.HasFile)
            {
                model.Binary_Poder_Notarial = Path.Combine(filePath, DateTime.Now.ToString("yyyyMMdd-HHmmss_A_") + fileUploadAttorneyPower.FileName);
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
            var model = new SupplierBranchModel();

            if (drpSupplier.SelectedIndex != 0 && drpSupplier.SelectedIndex != -1)
            {
                model.Id_Proveedor = Convert.ToInt32(drpSupplier.SelectedValue);
            }

            if (Session["UserInfo"] != null)
            {
                var userModel = Session["UserInfo"] as US_USUARIOModel;
                if (userModel != null) model.Cve_Region = userModel.Id_Departamento;
            }

            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Dx_Nombre_Comercial = txtBusinessName.Text.Trim() == "" ? "" : txtBusinessName.Text.Trim();
            model.Dx_RFC = txtRFC.Text.Trim() == "" ? "" : txtRFC.Text.Trim();
            model.Dx_Domicilio_Part_Calle = txtPartCalle.Text.Trim() == "" ? "" : txtPartCalle.Text.Trim();
            model.Dx_Domicilio_Part_Num = txtPartNum.Text.Trim() == "" ? "" : txtPartNum.Text.Trim();
            model.Dx_Domicilio_Part_CP = txtPartCP.Text.Trim() == "" ? "" : txtPartCP.Text.Trim();

            if (drpPartEstado.SelectedIndex != 0 && drpPartEstado.SelectedIndex != -1)
            {
                model.Cve_Estado_Part = Convert.ToInt32(drpPartEstado.SelectedValue);
            }

            if (drpPartDelegMunicipio.SelectedIndex != 0 && drpPartDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Part = Convert.ToInt32(drpPartDelegMunicipio.SelectedValue);
            }

            model.Fg_Mismo_Domicilio = ckbFiscal.Checked;
            model.Dx_Domicilio_Fiscal_Calle = txtFiscalCalle.Text.Trim() == "" ? "" : txtFiscalCalle.Text.Trim();
            model.Dx_Domicilio_Fiscal_Num = txtFiscalNum.Text.Trim() == "" ? "" : txtFiscalNum.Text.Trim();
            model.Dx_Domicilio_Fiscal_CP = txtFiscalCP.Text.Trim() == "" ? "" : txtFiscalCP.Text.Trim();

            if (drpFiscalEstado.SelectedIndex != 0 && drpFiscalEstado.SelectedIndex != -1)
            {
                model.Cve_Estado_Fisc = Convert.ToInt32(drpFiscalEstado.SelectedValue);
            }
            if (drpFiscalDelegMunicipio.SelectedIndex != 0 && drpFiscalDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Fisc = Convert.ToInt32(drpFiscalDelegMunicipio.SelectedValue);
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
                model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue);
            }

            if (fileUploadConstitutiva.HasFile)
            {
                model.Binary_Acta_Constitutiva = Path.Combine(filePath, DateTime.Now.ToString("yyyyMMdd-HHmmss_C_") + fileUploadConstitutiva.FileName);
            }

            if (fileUploadAttorneyPower.HasFile)
            {
                model.Binary_Poder_Notarial = Path.Combine(filePath, DateTime.Now.ToString("yyyyMMdd-HHmmss_A_") + fileUploadAttorneyPower.FileName);
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
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "IOError", "alert(" + message + ");");
            }
        }

        private bool SaveSupplier(string filePath, ref int idMod, ref int tipoMod, ref Object datosAnteriores, ref Object datosActuales)
        {
            bool success;
            var model = GetSupplierData(filePath);

            if (NewAdded())
            {
                success = CAT_PROVEEDORDal.ClassInstance.Insert_CAT_PROVEEDOR(model) > 0; //CAT_PROVEEDOR
                idMod = Insertlog.GetIdProveedor("DisponsalCenter");
                tipoMod = 1;
                datosAnteriores = new CAT_PROVEEDOR();
                datosActuales = AccesoDatos.Log.CatProveedor.ObtienePorId(idMod);
            }
            else
            {
                datosAnteriores = AccesoDatos.Log.CatProveedor.ObtienePorId(model.Id_Proveedor);
                success = CAT_PROVEEDORDal.ClassInstance.Update_CAT_PROVEEDOR(model) > 0; //CAT_PROVEEDOR
                idMod = model.Id_Proveedor;
                datosActuales = AccesoDatos.Log.CatProveedor.ObtienePorId(idMod);
                tipoMod = 2;
            }

            return success;
        }

        private bool SaveSupplierBranch(string filePath, ref int idMod, ref int tipoMod, ref Object datosAnteriores, ref Object datosActuales)
        {
            bool success;
            var model = GetSupplierBranchData(filePath);

            if (NewAdded())
            {
                success = SupplierBrancheDal.ClassInstance.Insert_CAT_PROVEEDORBRANCH(model) > 0; //CAT_PROVEEDORBRANCH
                idMod = Insertlog.GetIdProveedor("DisponsalCenterBranch");
                tipoMod = 1;
                datosAnteriores = null;
                datosActuales = null;
                //datosAnteriores = new CAT_PROVEEDORBRANCH();
                //datosActuales = AccesoDatos.Log.CatProveedorbranch.ObtienePorId(idMod);
            }
            else
            {
                datosAnteriores = AccesoDatos.Log.CatProveedorbranch.ObtienePorId(model.Id_Branch);
                success = SupplierBrancheDal.ClassInstance.Update_CAT_PROVEEDOR(model) > 0; //CAT_PROVEEDORBRANCH
                idMod = model.Id_Branch;
                datosActuales = AccesoDatos.Log.CatProveedorbranch.ObtienePorId(idMod);
                tipoMod = 2;
            }

            return success;
        }

        private bool ExistsSupplier()
        {
            var model = new CAT_PROVEEDORModel();

            if (Session["UserInfo"] != null)
            {
                var userModel = Session["UserInfo"] as US_USUARIOModel;
                if (userModel != null) model.Cve_Region = userModel.Id_Departamento;
            }
            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue);

            var valid = CAT_PROVEEDORDal.ClassInstance.Select_CAT_PROVEEDOR(model) > 0;
            return valid;
        }

        private bool ExistsSupplierBranch()
        {
            const bool valid = false;
            var model = new SupplierBranchModel();

            if (Session["UserInfo"] != null)
            {
                var userModel = Session["UserInfo"] as US_USUARIOModel;
                if (userModel != null) model.Cve_Region = userModel.Id_Departamento;
            }
            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue);

            // valid = SupplierBrancheDal.ClassInstance.Select_CAT_PROVEEDOR(model) > 0 ? true : false;
            return valid;
        }

        private bool ExistsRfc()
        {
            var model = new CAT_PROVEEDORModel {Dx_RFC = txtRFC.Text};
            var valid = CAT_PROVEEDORDal.ClassInstance.Select_CAT_PROVEEDOR_RFC(model) > 0;
            return valid;
        }

        private void ClearData()
        {
            ckbBranch.Checked = false;
            drpSupplier.SelectedIndex = 0;
            lblSupplier.Visible = false;//added by tina 2012/04/27
            drpSupplier.Visible = false;//added by tina 2012/04/27
            drpSupplierRequired.Enabled = false;
            txtSocialName.Text = "";
            txtBusinessName.Text = "";
            txtRFC.Text = "";
            txtPartCalle.Text = "";
            txtPartNum.Text = "";
            txtPartCP.Text = "";
            drpPartEstado.SelectedIndex = 0;
            drpPartDelegMunicipio.SelectedIndex = 0;
            ckbFiscal.Checked = false;
            txtFiscalCalle.Text = "";
            txtFiscalCP.Text = "";
            txtFiscalNum.Text = "";
            drpFiscalEstado.SelectedIndex = 0;
            drpFiscalDelegMunicipio.SelectedIndex = 0;
            txtRepresentativeName.Text = "";
            txtRepresentativeEmail.Text = "";
            txtTelephone.Text = "";
            txtLegalName.Text = "";
            //updated by tina 2012-07-17
            drpRate.SelectedIndex = 0;
            //end
            txtBankName.Text = "";
            txtBankAccount.Text = "";
            drpZone.SelectedIndex = 0;
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
                    lblCaptura.Text = @"Nombre o Razón Social (Sucursal)";
                }
                else
                {
                    lblSupplier.Visible = false;
                    drpSupplier.Visible = false;
                    drpSupplierRequired.Enabled = false;
                    lblCaptura.Text = @"Nombre o Razón Social (Matriz)";
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
            if (!IsPostBack) return;

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

        #endregion 
    }
}
