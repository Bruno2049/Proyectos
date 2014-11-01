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
    public partial class DisposalCenterEdit : System.Web.UI.Page
    {
        #region Global Variables
        private int DisposalCenterId
        {
            get
            {
                return ViewState["DisposalID"] == null ? 0 : Convert.ToInt32(ViewState["DisposalID"].ToString());
            }
            set
            {
                ViewState["DisposalID"] = value;
            }
        }

        private string DisposalCenterType
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

                //Setup default date to date time control for displaying
                this.literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                
                //Initialize filter conditions
                InitializeComponents();

                //Check is edit or new
                InitializeEditionData();
            }
        }

        private void InitializeEditionData()
        {
            //Check if it is new or edit, if edit, load the default data for editing
            if (Request.QueryString["DisposalID"] != null && Request.QueryString["DisposalID"].ToString() != "" && Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() != "")
            {
                DisposalCenterId = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["DisposalID"].ToString().Replace("%2B", "+"))));
                DisposalCenterType = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Type"].ToString().Replace("%2B", "+")));

                //Load current user information
                LoadCurrentDisposalCenterInfo();
            }
        }

        private void InitializeComponents()
        {
            InitializeDisposalCenters();
            InitializeEstadoData();
            InitializeMunicipio1();
            InitializeMunicipio2();
            InitializeZoneData();

            lblMainCenter.Visible = false;
            drpMainCenter.Visible = false;
            drpMainCenterRequired.Enabled = false;
            lblClave.Visible = false;
            txtClave.Visible = false;
            btnAssignTechnology.Visible = false;
        }        

        private void InitializeDisposalCenters()
        {
            DataTable disposalCenters = null;
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            if (UserModel != null)
            {
                // RSA 20120927 todos los proveedores matriz
                disposalCenters = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposals();
                if (disposalCenters != null)
                {
                    drpMainCenter.DataSource = disposalCenters;
                    drpMainCenter.DataTextField = "Dx_Razon_Social";
                    drpMainCenter.DataValueField = "Id_Centro_Disp";
                    drpMainCenter.DataBind();
                    drpMainCenter.Items.Insert(0, "");
                    drpMainCenter.SelectedIndex = 0;
                }
            }
        }

        private void InitializeEstadoData()
        {
            DataTable dtEstado = CAT_ESTADOBLL.ClassInstance.Get_All_CAT_ESTADO();

            //Init estado controls
            if (dtEstado != null)
            {
                //Init part estado
                drpPartEstado.DataSource = dtEstado;
                drpPartEstado.DataTextField = "Dx_Nombre_Estado";
                drpPartEstado.DataValueField = "Cve_Estado";
                drpPartEstado.DataBind();
                drpPartEstado.Items.Insert(0, "");
                drpPartEstado.SelectedIndex = 0;
                //Init fiscal estado
                drpFiscalEstado.DataSource = dtEstado;
                drpFiscalEstado.DataTextField = "Dx_Nombre_Estado";
                drpFiscalEstado.DataValueField = "Cve_Estado";
                drpFiscalEstado.DataBind();
                drpFiscalEstado.Items.Insert(0, "");
                drpFiscalEstado.SelectedIndex = 0;
            }
        }

        private void InitializeMunicipio1()
        {
            int Cve_Estado = (drpPartEstado.SelectedIndex == 0 || drpPartEstado.SelectedIndex == -1) ? -1 : int.Parse(drpPartEstado.SelectedValue);

            DataTable delegetMunicipios = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Cve_Estado);

            //Init part deleg municipio controls
            if (delegetMunicipios != null)
            {
                //Init part deleg municipio
                drpPartDelegMunicipio.DataSource = delegetMunicipios;
                drpPartDelegMunicipio.DataTextField = "Dx_Deleg_Municipio";
                drpPartDelegMunicipio.DataValueField = "Cve_Deleg_Municipio";
                drpPartDelegMunicipio.DataBind();
                drpPartDelegMunicipio.Items.Insert(0, "");
                drpPartDelegMunicipio.SelectedIndex = 0;
            }
        }

        private void InitializeMunicipio2()
        {
            int Cve_Estado = (drpFiscalEstado.SelectedIndex == 0 || drpFiscalEstado.SelectedIndex == -1) ? -1 : int.Parse(drpFiscalEstado.SelectedValue);

            DataTable delegetMunicipios = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Cve_Estado);

            //Init fiscal deleg municipio controls
            if (delegetMunicipios != null)
            {
                //Init fiscal deleg municipio
                drpFiscalDelegMunicipio.DataSource = delegetMunicipios;
                drpFiscalDelegMunicipio.DataTextField = "Dx_Deleg_Municipio";
                drpFiscalDelegMunicipio.DataValueField = "Cve_Deleg_Municipio";
                drpFiscalDelegMunicipio.DataBind();
                drpFiscalDelegMunicipio.Items.Insert(0, "");
                drpFiscalDelegMunicipio.SelectedIndex = 0;
            }
        }

        private void InitializeZoneData()
        {
            DataTable zones = null;
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            if (UserModel!= null)
            {
                zones = CatZonaDal.ClassInstance.GetZonaWithRegional(UserModel.Id_Departamento);
                if (zones != null)
                {
                    drpZone.DataSource = zones;
                    drpZone.DataTextField = "Dx_Nombre_Zona";
                    drpZone.DataValueField = "Cve_Zona";
                    drpZone.DataBind();
                    drpZone.Items.Insert(0, "");
                    drpZone.SelectedIndex = 0;
                }
            }
        }

        private void LoadCurrentDisposalCenterInfo()
        {
            DataTable disposalCenterModel = null;
            if (DisposalCenterType.ToLower() == "matriz") // updated by tina 2012-02-29
            {
                disposalCenterModel = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalsByPk(DisposalCenterId);
            }
            else
            {
                disposalCenterModel = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetDisposalByBranch(DisposalCenterId);
            }

            if (disposalCenterModel != null && disposalCenterModel.Rows.Count > 0)
            {
                //Check if the disposal center is active, if true, visible the technology assignment action
                if (Convert.ToInt32(disposalCenterModel.Rows[0]["Cve_Estatus_Centro_Disp"].ToString()) == (int)DisposalCenterStatus.ACTIVO)
                {
                    btnAssignTechnology.Visible = true;
                }

                //Invisible/visible some controls by the disposal center type
                if (DisposalCenterType.ToLower() == "sucursal") // updated by tina 2012-02-29
                {
                    ckbBranch.Checked = true;
                    ckbBranch.Enabled = false;

                    lblMainCenter.Visible = true;
                    drpMainCenter.Visible = true;
                    drpMainCenterRequired.Enabled = true;
                    drpMainCenter.SelectedValue = disposalCenterModel.Rows[0]["Id_Centro_Disp"].ToString();
                    txtClave.Text = disposalCenterModel.Rows[0]["Id_Centro_Disp_Sucursal"].ToString();
                }
                else
                {
                    ckbBranch.Visible = false;
                    lblMainCenter.Visible = false;
                    drpMainCenter.Visible = false;
                    drpMainCenterRequired.Enabled = false;

                    txtClave.Text = disposalCenterModel.Rows[0]["Id_Centro_Disp"].ToString();
                }

                txtSocialName.Text = disposalCenterModel.Rows[0]["Dx_Razon_Social"].ToString();
                txtBusinessName.Text = disposalCenterModel.Rows[0]["Dx_Nombre_Comercial"].ToString();
                txtRFC.Text = disposalCenterModel.Rows[0]["Dx_RFC"].ToString();

                txtPartCalle.Text = disposalCenterModel.Rows[0]["Dx_Domicilio_Part_Calle"].ToString();
                txtPartNum.Text = disposalCenterModel.Rows[0]["Dx_Domicilio_Part_Num"].ToString();
                txtPartCP.Text = disposalCenterModel.Rows[0]["Dx_Domicilio_Part_CP"].ToString();
                drpPartEstado.SelectedValue = disposalCenterModel.Rows[0]["Cve_Estado_Part"].ToString();
                drpPartDelegMunicipio.SelectedValue = disposalCenterModel.Rows[0]["Cve_Deleg_Municipio_Part"].ToString();

                if (disposalCenterModel.Rows[0]["Fg_Mismo_Domicilio"] != DBNull.Value)
                {
                    if (disposalCenterModel.Rows[0]["Fg_Mismo_Domicilio"].ToString() == "True")
                    {
                        ckbFiscal.Checked = true;
                    }
                    else
                    {
                        ckbFiscal.Checked = false;
                    }
                }

                txtFiscalCalle.Text = disposalCenterModel.Rows[0]["Dx_Domicilio_Fiscal_Calle"].ToString();
                txtFiscalNum.Text = disposalCenterModel.Rows[0]["Dx_Domicilio_Fiscal_Num"].ToString();
                txtFiscalCP.Text = disposalCenterModel.Rows[0]["Dx_Domicilio_Fiscal_CP"].ToString();
                drpFiscalEstado.SelectedValue = disposalCenterModel.Rows[0]["Cve_Estado_Fisc"].ToString();
                drpFiscalDelegMunicipio.SelectedValue = disposalCenterModel.Rows[0]["Cve_Deleg_Municipio_Fisc"].ToString();

                txtRepresentativeName.Text = disposalCenterModel.Rows[0]["Dx_Nombre_Repre"].ToString();
                txtRepresentativeEmail.Text = disposalCenterModel.Rows[0]["Dx_Email_Repre"].ToString();
                txtTelephone.Text = disposalCenterModel.Rows[0]["Dx_Telefono_Repre"].ToString();
                txtLegalName.Text = disposalCenterModel.Rows[0]["Dx_Nombre_Repre_Legal"].ToString();

                //txtBankName.Text = disposalCenterModel.Rows[0]["Dx_Nombre_Banco"].ToString();
                //txtBankAccount.Text = disposalCenterModel.Rows[0]["Dx_Cuenta_Banco"].ToString();

                drpZone.SelectedValue = disposalCenterModel.Rows[0]["Cve_Zona"].ToString();

                txtNoEmpleados.Text = disposalCenterModel.Rows[0]["No_Empleados"].ToString();
                txtMarcaAnalizadorGas.Text = disposalCenterModel.Rows[0]["Marca_Analizador_Gas"].ToString();
                txtModeloAnalizadorGas.Text = disposalCenterModel.Rows[0]["Modelo_Analizador_Gas"].ToString();
                txtSerieAnalizadorGas.Text = disposalCenterModel.Rows[0]["Serie_Analizador_Gas"].ToString();
                txtHorarioDesde.Text = disposalCenterModel.Rows[0]["Horario_Desde"].ToString();
                txtHorarioHasta.Text = disposalCenterModel.Rows[0]["Horario_Hasta"].ToString();
                txtDiasSemana.Text = disposalCenterModel.Rows[0]["Dias_Semana"].ToString();
                txtNoRegistroAmbiental.Text = disposalCenterModel.Rows[0]["No_Registro_Ambiental"].ToString();

                ListItem itmSelected = ddlTipo.Items.FindByValue(disposalCenterModel.Rows[0]["Tipo"].ToString());
                if (itmSelected != null)
                    itmSelected.Selected = true;
                else
                {
                    ddlTipo.Items.Insert(1, disposalCenterModel.Rows[0]["Tipo"].ToString());
                    ddlTipo.SelectedIndex = 1;
                }

                //txtEstatusRegistro.Text = disposalCenterModel.Rows[0]["Estatus_Registro"].ToString();
                txtTelefonoAtn1.Text = disposalCenterModel.Rows[0]["Telefono_Atn1"].ToString();
                txtTelefonoAtn2.Text = disposalCenterModel.Rows[0]["Telefono_Atn2"].ToString();
                txtDxApPaternoRepLeg.Text = disposalCenterModel.Rows[0]["Dx_Ap_Paterno_Rep_Leg"].ToString();
                txtDxApMaternoRepLeg.Text = disposalCenterModel.Rows[0]["Dx_Ap_Materno_Rep_Leg"].ToString();
                txtDxEmailRepreLegal.Text = disposalCenterModel.Rows[0]["Dx_Email_Repre_Legal"].ToString();
                txtDxTelefonoRepreLeg.Text = disposalCenterModel.Rows[0]["Dx_Telefono_Repre_Leg"].ToString();
                txtDxCelularRepreLeg.Text = disposalCenterModel.Rows[0]["Dx_Celular_Repre_Leg"].ToString();
                txtDxApPaternoRepre.Text = disposalCenterModel.Rows[0]["Dx_Ap_Paterno_Repre"].ToString();
                txtDxApMaternoRepre.Text = disposalCenterModel.Rows[0]["Dx_Ap_Materno_Repre"].ToString();
                txtDxCelularRepre.Text = disposalCenterModel.Rows[0]["Dx_Celular_Repre"].ToString();
            }
        }

        #endregion

        #region Button Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool success = false;
            string filePath = "\\DisposalCenterAndSupplierImage\\";

            Page.Validate();

            //Verify the page data input is reasonable
            if (this.IsValid)//added by mark 2012-07-12
            {
                //updated by mark 2012-07-12
                if (drpZone.SelectedIndex == -1 || drpZone.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.panel1, this.GetType(), "NotSelectZone", "alert('¡ Debe seleccionarse una Zona !')", true);
                }
                else
                {
                    if (IsDisposalCenter())
                    {
                        if (NewAdded() && ExistsRFC())
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('El RFC ya fue dado de alta.');", true);
                            return;
                        }
                        else if (NewAdded() && ExistsSupplier())
                        {
                            // RSA 20120927 avoid duplicated
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('El CAYD ya fue dado de alta.');", true);
                            return;
                        }
                        else
                            success = SaveDisposalCenter(filePath);
                    }
                    else
                    {
                        if (this.drpMainCenter.SelectedIndex != -1 && this.drpMainCenter.SelectedIndex != 0)
                        {
                            if (NewAdded() && ExistsSupplierBranch())
                            {
                                // RSA 20120927 avoid duplicated
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('El CAYD ya fue dado de alta.');", true);
                                return;
                            }
                            else
                                success = SaveDisposalCenterBranch(filePath);
                        }
                    }

                }

                //end by mark               
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
                if (IsDisposalCenter() && NewAdded())
                {
                    InitializeDisposalCenters();
                }
                if (NewAdded())
                {
                    //CLEAR DATA
                    ClearData();
                }
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "savesuccess", "alert('Los datos del CAYD fueron guardados correctamente.');", true);

                //end               
            }
            else
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('Los datos del CAYD no pudieron ser guardados, verifique.');", true);

        }

        protected void btnAssignTechnology_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssignTechnologiesToDisposal.aspx?DisposalID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(DisposalCenterId.ToString())).Replace("+", "%2B") +
                                                                               "&Type=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(DisposalCenterType)).Replace("+", "%2B"));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DisposalCenterMonitor.aspx");
        }
        #endregion

        #region Private Methods
        private bool NewAdded()
        {
            bool result = false;
            if (DisposalCenterId == 0 && DisposalCenterType == "")
            {
                result = true;
            }
            return result;
        }

        private bool IsDisposalCenter()
        {
            bool result = false;
            if (DisposalCenterType.ToLower() == "matriz" || ckbBranch.Checked == false) // updated by tina 2012-02-29
            {
                result = true;
            }
            return result;
        }

        private CAT_CENTRO_DISPModel GetDisposalCenterData(string filePath)
        {
            CAT_CENTRO_DISPModel model = new CAT_CENTRO_DISPModel();

            if (Session["UserInfo"] != null)
            {
                US_USUARIOModel UserModel = null;
                UserModel = Session["UserInfo"] as US_USUARIOModel;

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
                model.Cve_Estado_Part = drpPartEstado.SelectedValue;
            }
            if (drpPartDelegMunicipio.SelectedIndex != 0 && drpPartDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Part = drpPartDelegMunicipio.SelectedValue;
            }

            model.Fg_Mismo_Domicilio = ckbFiscal.Checked == true ? 1 : 0;

            model.Dx_Domicilio_Fiscal_Calle = txtFiscalCalle.Text.Trim() == "" ? "" : txtFiscalCalle.Text.Trim();
            model.Dx_Domicilio_Fiscal_Num = txtFiscalNum.Text.Trim() == "" ? "" : txtFiscalNum.Text.Trim();
            model.Dx_Domicilio_Fiscal_CP = txtFiscalCP.Text.Trim() == "" ? "" : txtFiscalCP.Text.Trim();
            if (drpFiscalEstado.SelectedIndex != 0 && drpFiscalEstado.SelectedIndex != -1)
            {
                model.Cve_Estado_Fisc = drpFiscalEstado.SelectedValue;
            }
            if (drpFiscalDelegMunicipio.SelectedIndex != 0 && drpFiscalDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Fisc = drpFiscalDelegMunicipio.SelectedValue;
            }

            model.Dx_Nombre_Repre = txtRepresentativeName.Text.Trim() == "" ? "" : txtRepresentativeName.Text.Trim();
            model.Dx_Email_Repre = txtRepresentativeEmail.Text.Trim() == "" ? "" : txtRepresentativeEmail.Text.Trim();
            model.Dx_Telefono_Repre = txtTelephone.Text.Trim() == "" ? "" : txtTelephone.Text.Trim();
            model.Dx_Nombre_Repre_Legal = txtLegalName.Text.Trim() == "" ? "" : txtLegalName.Text.Trim();

            //model.Dx_Nombre_Banco = txtBankName.Text.Trim() == "" ? "" : txtBankName.Text.Trim();
            //model.Dx_Cuenta_Banco = txtBankAccount.Text.Trim() == "" ? "" : txtBankAccount.Text.Trim();

            model.Dt_Fecha_Centro_Disp = DateTime.Now;

            if (drpZone.SelectedIndex != 0 && drpZone.SelectedIndex != -1)
            {
                model.Cve_Zona = drpZone.SelectedValue;
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
                model.Cve_Estatus_Centro_Disp = (int)DisposalCenterStatus.PENDIENTE;
                model.Codigo_Centro_Disp = string.Format("{0:D3}", LsUtility.GetNumberSequence("CENTRODISP"));
            }
            else
            {
                model.Id_Centro_Disp = Convert.ToInt32(txtClave.Text);
            }

            int noEmpleados = 0;
            int.TryParse(txtNoEmpleados.Text.Trim(), out noEmpleados);
            model.No_Empleados = noEmpleados;
            model.Marca_Analizador_Gas = txtMarcaAnalizadorGas.Text.Trim() == "" ? "" : txtMarcaAnalizadorGas.Text.Trim();
            model.Modelo_Analizador_Gas = txtModeloAnalizadorGas.Text.Trim() == "" ? "" : txtModeloAnalizadorGas.Text.Trim();
            model.Serie_Analizador_Gas = txtSerieAnalizadorGas.Text.Trim() == "" ? "" : txtSerieAnalizadorGas.Text.Trim();
            DateTime timeDesde = new DateTime(2012, 1, 1, 00, 00, 00);
            try { timeDesde = DateTime.ParseExact(txtHorarioDesde.Text.Trim(), "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture); }
            catch (Exception) { }
            model.Horario_Desde = timeDesde;
            DateTime timeHasta = new DateTime(2012, 1, 1, 00, 00, 00);
            try { timeHasta = DateTime.ParseExact(txtHorarioHasta.Text.Trim(), "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture); }
            catch (Exception) { }
            model.Horario_Hasta = timeHasta;
            model.Dias_Semana = txtDiasSemana.Text.Trim() == "" ? "" : txtDiasSemana.Text.Trim();
            model.No_Registro_Ambiental = txtNoRegistroAmbiental.Text.Trim() == "" ? "" : txtNoRegistroAmbiental.Text.Trim();
            model.Tipo = ddlTipo.SelectedValue.Trim() == "" ? "" : ddlTipo.SelectedValue.Trim();

            //model.EstatusRegistro = txtEstatusRegistro.Text.Trim() == "" ? "" : txtEstatusRegistro.Text.Trim();
            model.EstatusRegistro = string.Empty;
            model.TelefonoAtn1 = txtTelefonoAtn1.Text.Trim() == "" ? "" : txtTelefonoAtn1.Text.Trim();
            model.TelefonoAtn2 = txtTelefonoAtn2.Text.Trim() == "" ? "" : txtTelefonoAtn2.Text.Trim();
            model.DxApPaternoRepLeg = txtDxApPaternoRepLeg.Text.Trim() == "" ? "" : txtDxApPaternoRepLeg.Text.Trim();
            model.DxApMaternoRepLeg = txtDxApMaternoRepLeg.Text.Trim() == "" ? "" : txtDxApMaternoRepLeg.Text.Trim();
            model.DxEmailRepreLegal = txtDxEmailRepreLegal.Text.Trim() == "" ? "" : txtDxEmailRepreLegal.Text.Trim();
            model.DxTelefonoRepreLeg = txtDxTelefonoRepreLeg.Text.Trim() == "" ? "" : txtDxTelefonoRepreLeg.Text.Trim();
            model.DxCelularRepreLeg = txtDxCelularRepreLeg.Text.Trim() == "" ? "" : txtDxCelularRepreLeg.Text.Trim();
            model.DxApPaternoRepre = txtDxApPaternoRepre.Text.Trim() == "" ? "" : txtDxApPaternoRepre.Text.Trim();
            model.DxApMaternoRepre = txtDxApMaternoRepre.Text.Trim() == "" ? "" : txtDxApMaternoRepre.Text.Trim();
            model.DxCelularRepre = txtDxCelularRepre.Text.Trim() == "" ? "" : txtDxCelularRepre.Text.Trim();

            return model;
        }

        private CAT_CENTRO_DISP_SUCURSALModel GetDisposalCenterBranchData(string filePath)
        {
            CAT_CENTRO_DISP_SUCURSALModel model = new CAT_CENTRO_DISP_SUCURSALModel();
            if (drpMainCenter.SelectedIndex != 0 && drpMainCenter.SelectedIndex != -1)
            {
                model.Id_Centro_Disp = Convert.ToInt32(drpMainCenter.SelectedValue);
            }
            if (Session["UserInfo"] != null)
            {
                US_USUARIOModel UserModel = null;
                UserModel = Session["UserInfo"] as US_USUARIOModel;

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
                model.Cve_Estado_Part = drpPartEstado.SelectedValue;
            }
            if (drpPartDelegMunicipio.SelectedIndex != 0 && drpPartDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Part = drpPartDelegMunicipio.SelectedValue;
            }

            model.Fg_Mismo_Domicilio = ckbFiscal.Checked == true ? 1 : 0;

            model.Dx_Domicilio_Fiscal_Calle = txtFiscalCalle.Text.Trim() == "" ? "" : txtFiscalCalle.Text.Trim();
            model.Dx_Domicilio_Fiscal_Num = txtFiscalNum.Text.Trim() == "" ? "" : txtFiscalNum.Text.Trim();
            model.Dx_Domicilio_Fiscal_CP = txtFiscalCP.Text.Trim() == "" ? "" : txtFiscalCP.Text.Trim();
            if (drpFiscalEstado.SelectedIndex != 0 && drpFiscalEstado.SelectedIndex != -1)
            {
                model.Cve_Estado_Fisc = drpFiscalEstado.SelectedValue;
            }
            if (drpFiscalDelegMunicipio.SelectedIndex != 0 && drpFiscalDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Fisc = drpFiscalDelegMunicipio.SelectedValue;
            }

            model.Dx_Nombre_Repre = txtRepresentativeName.Text.Trim() == "" ? "" : txtRepresentativeName.Text.Trim();
            model.Dx_Email_Repre = txtRepresentativeEmail.Text.Trim() == "" ? "" : txtRepresentativeEmail.Text.Trim();
            model.Dx_Telefono_Repre = txtTelephone.Text.Trim() == "" ? "" : txtTelephone.Text.Trim();
            model.Dx_Nombre_Repre_Legal = txtLegalName.Text.Trim() == "" ? "" : txtLegalName.Text.Trim();

            //model.Dx_Nombre_Banco = txtBankName.Text.Trim() == "" ? "" : txtBankName.Text.Trim();
            //model.Dx_Cuenta_Banco = txtBankAccount.Text.Trim() == "" ? "" : txtBankAccount.Text.Trim();

            model.Dt_Fecha_Centro_Disp_Sucursal = DateTime.Now;

            if (drpZone.SelectedIndex != 0 && drpZone.SelectedIndex != -1)
            {
                model.Cve_Zona = drpZone.SelectedValue;
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
                model.Cve_Estatus_Centro_Disp = (int)DisposalCenterStatus.PENDIENTE;
                model.Codigo_Centro_Disp_Sucursal = string.Format("{0:D3}", LsUtility.GetNumberSequence("BRANCHDISP"));
            }
            else
            {
                model.Id_Centro_Disp_Sucursal = Convert.ToInt32(txtClave.Text);
            }

            int noEmpleados = 0;
            int.TryParse(txtNoEmpleados.Text.Trim(), out noEmpleados);
            model.No_Empleados = noEmpleados;
            model.Marca_Analizador_Gas = txtMarcaAnalizadorGas.Text.Trim() == "" ? "" : txtMarcaAnalizadorGas.Text.Trim();
            model.Modelo_Analizador_Gas = txtModeloAnalizadorGas.Text.Trim() == "" ? "" : txtModeloAnalizadorGas.Text.Trim();
            model.Serie_Analizador_Gas = txtSerieAnalizadorGas.Text.Trim() == "" ? "" : txtSerieAnalizadorGas.Text.Trim();
            DateTime timeDesde = new DateTime(2012, 1, 1, 00, 00, 00);
            try { timeDesde = DateTime.ParseExact(txtHorarioDesde.Text.Trim(), "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture); }
            catch (Exception) { }
            model.Horario_Desde = timeDesde;
            DateTime timeHasta = new DateTime(2012, 1, 1, 00, 00, 00);
            try { timeHasta = DateTime.ParseExact(txtHorarioHasta.Text.Trim(), "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture); }
            catch (Exception) { }
            model.Horario_Hasta = timeHasta;
            model.Dias_Semana = txtDiasSemana.Text.Trim() == "" ? "" : txtDiasSemana.Text.Trim();
            model.No_Registro_Ambiental = txtNoRegistroAmbiental.Text.Trim() == "" ? "" : txtNoRegistroAmbiental.Text.Trim();
            model.Tipo = ddlTipo.SelectedValue.Trim() == "" ? "" : ddlTipo.SelectedValue.Trim();

            //model.EstatusRegistro = txtEstatusRegistro.Text.Trim() == "" ? "" : txtEstatusRegistro.Text.Trim();
            model.EstatusRegistro = string.Empty;
            model.TelefonoAtn1 = txtTelefonoAtn1.Text.Trim() == "" ? "" : txtTelefonoAtn1.Text.Trim();
            model.TelefonoAtn2 = txtTelefonoAtn2.Text.Trim() == "" ? "" : txtTelefonoAtn2.Text.Trim();
            model.DxApPaternoRepLeg = txtDxApPaternoRepLeg.Text.Trim() == "" ? "" : txtDxApPaternoRepLeg.Text.Trim();
            model.DxApMaternoRepLeg = txtDxApMaternoRepLeg.Text.Trim() == "" ? "" : txtDxApMaternoRepLeg.Text.Trim();
            model.DxEmailRepreLegal = txtDxEmailRepreLegal.Text.Trim() == "" ? "" : txtDxEmailRepreLegal.Text.Trim();
            model.DxTelefonoRepreLeg = txtDxTelefonoRepreLeg.Text.Trim() == "" ? "" : txtDxTelefonoRepreLeg.Text.Trim();
            model.DxCelularRepreLeg = txtDxCelularRepreLeg.Text.Trim() == "" ? "" : txtDxCelularRepreLeg.Text.Trim();
            model.DxApPaternoRepre = txtDxApPaternoRepre.Text.Trim() == "" ? "" : txtDxApPaternoRepre.Text.Trim();
            model.DxApMaternoRepre = txtDxApMaternoRepre.Text.Trim() == "" ? "" : txtDxApMaternoRepre.Text.Trim();
            model.DxCelularRepre = txtDxCelularRepre.Text.Trim() == "" ? "" : txtDxCelularRepre.Text.Trim();

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

        private bool ExistsRFC()
        {
            bool valid = false;

            CAT_CENTRO_DISPModel model = new CAT_CENTRO_DISPModel();

            model.Dx_RFC = txtRFC.Text;

            valid = CAT_CENTRO_DISPDAL.ClassInstance.Select_MainDisposalCenter_RFC(model) > 0 ? true : false;

            return valid;
        }

        private bool ExistsSupplier()
        {
            bool valid = false;

            CAT_CENTRO_DISPModel model = new CAT_CENTRO_DISPModel();

            if (Session["UserInfo"] != null)
            {
                US_USUARIOModel UserModel = null;
                UserModel = Session["UserInfo"] as US_USUARIOModel;

                model.Cve_Region = UserModel.Id_Departamento;
            }
            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue.ToString());

            valid = CAT_CENTRO_DISPDAL.ClassInstance.Select_MainDisposalCenter(model) > 0 ? true : false;


            return valid;
        }

        private bool ExistsSupplierBranch()
        {
            bool valid = false;

            CAT_CENTRO_DISP_SUCURSALModel model = new CAT_CENTRO_DISP_SUCURSALModel();

            if (Session["UserInfo"] != null)
            {
                US_USUARIOModel UserModel = null;
                UserModel = Session["UserInfo"] as US_USUARIOModel;

                model.Cve_Region = UserModel.Id_Departamento;
            }
            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue.ToString());

            // valid = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.Select_BranchDisposalCenter(model) > 0 ? true : false;


            return valid;
        }

        private bool SaveDisposalCenter(string filePath)
        {
            bool success = false;
            CAT_CENTRO_DISPModel model = GetDisposalCenterData(filePath);

            if (NewAdded())
            {
                success = CAT_CENTRO_DISPDAL.ClassInstance.Insert_MainDisposalCenter(model) > 0 ? true : false;
            }
            else
            {
                success = CAT_CENTRO_DISPDAL.ClassInstance.Update_MainDisposalCenter(model) > 0 ? true : false;
            }

            return success;
        }

        private bool SaveDisposalCenterBranch(string filePath)
        {
            bool success = false;
            CAT_CENTRO_DISP_SUCURSALModel model = GetDisposalCenterBranchData(filePath);

            if (NewAdded())
            {
                success = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.Insert_BranchDisposalCenter(model) > 0 ? true : false;
            }
            else
            {
                success = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.Update_BranchDisposalCenter(model) > 0 ? true : false;
            }

            return success;
        }

        private void ClearData()
        {
            this.ckbBranch.Checked = false;
            this.drpMainCenter.SelectedIndex = 0;
            this.lblMainCenter.Visible = false;//added by tina 2012/04/27
            this.drpMainCenter.Visible = false;//added by tina 2012/04/27
            this.drpMainCenterRequired.Enabled = false;
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
            //this.txtBankName.Text = "";
            //this.txtBankAccount.Text = "";
            this.drpZone.SelectedIndex = 0;

            this.txtNoEmpleados.Text = "";
            this.txtMarcaAnalizadorGas.Text = "";
            this.txtModeloAnalizadorGas.Text = "";
            this.txtSerieAnalizadorGas.Text = "";
            this.txtHorarioDesde.Text = "";
            this.txtHorarioHasta.Text = "";
            this.txtDiasSemana.Text = "";
            this.txtNoRegistroAmbiental.Text = "";
            this.ddlTipo.SelectedValue = "";

            //this.txtEstatusRegistro.Text = "";
            this.txtTelefonoAtn1.Text = "";
            this.txtTelefonoAtn2.Text = "";
            this.txtDxApPaternoRepLeg.Text = "";
            this.txtDxApMaternoRepLeg.Text = "";
            this.txtDxEmailRepreLegal.Text = "";
            this.txtDxTelefonoRepreLeg.Text = "";
            this.txtDxCelularRepreLeg.Text = "";
            this.txtDxApPaternoRepre.Text = "";
            this.txtDxApMaternoRepre.Text = "";
            this.txtDxCelularRepre.Text = "";
        }
        #endregion

        #region Controls Changed Events
        protected void ckbBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ckbBranch.Checked)
                {
                    lblMainCenter.Visible = true;
                    drpMainCenter.Visible = true;
                    drpMainCenterRequired.Enabled = true;
                    lblCaptura.Text = "Nombre o Razón Social (Sucursal)";
                }
                else
                {
                    lblMainCenter.Visible = false;
                    drpMainCenter.Visible = false;
                    drpMainCenterRequired.Enabled = false;
                    lblCaptura.Text = "Nombre o Razón Social (Matriz)";
                }
            }
        }

        protected void drpPartEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                InitializeMunicipio1();
            }
        }

        protected void drpFiscalEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                InitializeMunicipio2();
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
                        InitializeMunicipio2();
                    }
                    drpFiscalDelegMunicipio.SelectedValue = drpPartDelegMunicipio.SelectedValue;
                }
                else
                {
                    txtFiscalCalle.Text = "";
                    txtFiscalNum.Text = "";
                    txtFiscalCP.Text = "";
                    drpFiscalEstado.SelectedIndex = 0;
                    InitializeMunicipio2();
                    drpFiscalDelegMunicipio.SelectedIndex = 0;
                }
            }
        }
        #endregion
    }
}
