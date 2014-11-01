using System;
using System.Globalization;
using System.Web.UI;
using System.IO;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.RegionalModule
{
    public partial class DisposalCenterEdit : Page
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
            if (IsPostBack) return;
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }

            //Setup default date to date time control for displaying
            literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                
            //Initialize filter conditions
            InitializeComponents();

            //Check is edit or new
            InitializeEditionData();
        }

        private void InitializeEditionData()
        {
            //Check if it is new or edit, if edit, load the default data for editing
            if (Request.QueryString["DisposalID"] == null || Request.QueryString["DisposalID"]== "" ||Request.QueryString["Type"] == null || Request.QueryString["Type"] == "") return;
            DisposalCenterId = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["DisposalID"].Replace("%2B", "+"))));
            DisposalCenterType = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Type"].Replace("%2B", "+")));

            //Load current user information
            LoadCurrentDisposalCenterInfo();
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
            var userModel = Session["UserInfo"] as US_USUARIOModel;

            if (userModel == null) return;
            // RSA 20120927 todos los proveedores matriz
            var disposalCenters = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposals();
            if (disposalCenters == null) return;
            drpMainCenter.DataSource = disposalCenters;
            drpMainCenter.DataTextField = "Dx_Razon_Social";
            drpMainCenter.DataValueField = "Id_Centro_Disp";
            drpMainCenter.DataBind();
            drpMainCenter.Items.Insert(0, "");
            drpMainCenter.SelectedIndex = 0;
        }

        private void InitializeEstadoData()
        {
            var dtEstado = CAT_ESTADOBLL.ClassInstance.Get_All_CAT_ESTADO();

            //Init estado controls
            if (dtEstado == null) return;
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

        private void InitializeMunicipio1()
        {
            var cveEstado = (drpPartEstado.SelectedIndex == 0 || drpPartEstado.SelectedIndex == -1) ? -1 : int.Parse(drpPartEstado.SelectedValue);

            var delegetMunicipios = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(cveEstado);

            //Init part deleg municipio controls
            if (delegetMunicipios == null) return;
            //Init part deleg municipio
            drpPartDelegMunicipio.DataSource = delegetMunicipios;
            drpPartDelegMunicipio.DataTextField = "Dx_Deleg_Municipio";
            drpPartDelegMunicipio.DataValueField = "Cve_Deleg_Municipio";
            drpPartDelegMunicipio.DataBind();
            drpPartDelegMunicipio.Items.Insert(0, "");
            drpPartDelegMunicipio.SelectedIndex = 0;
        }

        private void InitializeMunicipio2()
        {
            var cveEstado = (drpFiscalEstado.SelectedIndex == 0 || drpFiscalEstado.SelectedIndex == -1) ? -1 : int.Parse(drpFiscalEstado.SelectedValue);

            var delegetMunicipios = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(cveEstado);

            //Init fiscal deleg municipio controls
            if (delegetMunicipios == null) return;
            //Init fiscal deleg municipio
            drpFiscalDelegMunicipio.DataSource = delegetMunicipios;
            drpFiscalDelegMunicipio.DataTextField = "Dx_Deleg_Municipio";
            drpFiscalDelegMunicipio.DataValueField = "Cve_Deleg_Municipio";
            drpFiscalDelegMunicipio.DataBind();
            drpFiscalDelegMunicipio.Items.Insert(0, "");
            drpFiscalDelegMunicipio.SelectedIndex = 0;
        }

        private void InitializeZoneData()
        {
            var userModel = Session["UserInfo"] as US_USUARIOModel;

            if (userModel == null) return;
            var zones = CatZonaDal.ClassInstance.GetZonaWithRegional(userModel.Id_Departamento);
            if (zones == null) return;
            drpZone.DataSource = zones;
            drpZone.DataTextField = "Dx_Nombre_Zona";
            drpZone.DataValueField = "Cve_Zona";
            drpZone.DataBind();
            drpZone.Items.Insert(0, "");
            drpZone.SelectedIndex = 0;
        }

        private void LoadCurrentDisposalCenterInfo()
        {
            var disposalCenterModel = DisposalCenterType.ToLower() == "matriz" ? CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalsByPk(DisposalCenterId) : CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetDisposalByBranch(DisposalCenterId);

            if (disposalCenterModel == null || disposalCenterModel.Rows.Count <= 0) return;
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
                ckbFiscal.Checked = disposalCenterModel.Rows[0]["Fg_Mismo_Domicilio"].ToString() == "True";
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
            
            drpZone.SelectedValue = disposalCenterModel.Rows[0]["Cve_Zona"].ToString();

            txtNoEmpleados.Text = disposalCenterModel.Rows[0]["No_Empleados"].ToString();
            txtMarcaAnalizadorGas.Text = disposalCenterModel.Rows[0]["Marca_Analizador_Gas"].ToString();
            txtModeloAnalizadorGas.Text = disposalCenterModel.Rows[0]["Modelo_Analizador_Gas"].ToString();
            txtSerieAnalizadorGas.Text = disposalCenterModel.Rows[0]["Serie_Analizador_Gas"].ToString();
            txtHorarioDesde.Text = disposalCenterModel.Rows[0]["Horario_Desde"].ToString();
            txtHorarioHasta.Text = disposalCenterModel.Rows[0]["Horario_Hasta"].ToString();
            txtDiasSemana.Text = disposalCenterModel.Rows[0]["Dias_Semana"].ToString();
            txtNoRegistroAmbiental.Text = disposalCenterModel.Rows[0]["No_Registro_Ambiental"].ToString();

            var itmSelected = ddlTipo.Items.FindByValue(disposalCenterModel.Rows[0]["Tipo"].ToString());
            if (itmSelected != null)
                itmSelected.Selected = true;
            else
            {
                ddlTipo.Items.Insert(1, disposalCenterModel.Rows[0]["Tipo"].ToString());
                ddlTipo.SelectedIndex = 1;
            }

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

            lblMotivos.Visible = true;
            txtMotivos.Visible = true;

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

            //Verify the page data input is reasonable
            if (IsValid) //added by mark 2012-07-12
            {
                //updated by mark 2012-07-12
                if (drpZone.SelectedIndex == -1 || drpZone.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(panel1, GetType(), "NotSelectZone",
                        "alert('¡ Debe seleccionarse una Zona !')", true);
                }
                else
                {
                    if (IsDisposalCenter())
                    {
                        if (NewAdded() && ExistsRfc())
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "savefail",
                                "alert('El RFC ya fue dado de alta.');", true);
                            return;
                        }
                        if (NewAdded() && ExistsSupplier())
                        {
                            // RSA 20120927 avoid duplicated
                            ScriptManager.RegisterStartupScript(Page, GetType(), "savefail",
                                "alert('El CAYD ya fue dado de alta.');", true);
                            return;
                        }
                        if (!NewAdded())
                        {
                            if (txtMotivos.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "savefail",
                                    "alert('¡Debe ingresar el motivo por el cual esta realizando cambios al CAyD!')",
                                    true);
                                return;
                            }
                        }

                        success = SaveDisposalCenter(filePath, ref idAfectado, ref tipoModificacion, ref datosAnteriores,ref datosActuales);
                    }
                    else
                    {
                        if (drpMainCenter.SelectedIndex != -1 && drpMainCenter.SelectedIndex != 0)
                        {
                            if (NewAdded() && ExistsSupplierBranch())
                            {
                                // RSA 20120927 avoid duplicated
                                ScriptManager.RegisterStartupScript(Page, GetType(), "savefail",
                                    "alert('El CAYD ya fue dado de alta.');", true);
                                return;
                            }
                            if (!NewAdded())
                            {
                                if (txtMotivos.Text == "")
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "savefail",
                                        "alert('¡Debe ingresar el motivo por el cual esta realizando cambios al CAyD!')",
                                        true);
                                    return;
                                }
                            }
                            success = SaveDisposalCenterBranch(filePath, ref idAfectado, ref tipoModificacion, ref datosAnteriores,ref datosActuales);
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
                if (IsDisposalCenter() && NewAdded())
                {
                    InitializeDisposalCenters();
                }
                if (NewAdded())
                {
                    //CLEAR DATA
                    ClearData();
                }
                switch (tipoModificacion)
                {
                    case 1: //INSERT

                        /*INSERTAR EVENTO ALTA DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "EMPRESAS", "ALTA",idAfectado.ToString(CultureInfo.InvariantCulture),
                            "", "Fecha alta: " + DateTime.Now,"","");

                        ScriptManager.RegisterStartupScript(Page, GetType(), "savesuccess","alert('Los datos del CAYD fueron guardados correctamente.');", true);
                        break;

                    case 2://UPDATE
                        /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                         var cambioDatos = Insertlog.GetCambiosDatos(datosAnteriores, datosActuales);
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "EMPRESAS", "CAMBIOS", idAfectado.ToString(CultureInfo.InvariantCulture), txtMotivos.Text,
                            "",cambioDatos[0],cambioDatos[1]);

                        ScriptManager.RegisterStartupScript(Page, GetType(), "savesuccess","alert('Los datos del CAYD fueron guardados correctamente.');", true);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(Page, GetType(), "savefail",
                            "alert('Los datos no se guardaron en el LOG.');", true);
                        break;
                }
                //end               
            }
            else
                ScriptManager.RegisterStartupScript(Page, GetType(), "savefail",
                    "alert('Los datos del CAYD no pudieron ser guardados, verifique.');", true);
        }

        protected void btnAssignTechnology_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssignTechnologiesToDisposal.aspx?DisposalID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(DisposalCenterId.ToString(CultureInfo.InvariantCulture))).Replace("+", "%2B") +
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
            var result = DisposalCenterId == 0 && DisposalCenterType == "";
            return result;
        }

        private bool IsDisposalCenter()
        {
            var result = DisposalCenterType.ToLower() == "matriz" || ckbBranch.Checked == false;
            return result;
        }

        private CAT_CENTRO_DISPModel GetDisposalCenterData(string filePath)
        {
            var model = new CAT_CENTRO_DISPModel();

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
                model.Cve_Estado_Part = drpPartEstado.SelectedValue;
            }
            if (drpPartDelegMunicipio.SelectedIndex != 0 && drpPartDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Part = drpPartDelegMunicipio.SelectedValue;
            }

            model.Fg_Mismo_Domicilio = ckbFiscal.Checked ? 1 : 0;

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
            
            model.Dt_Fecha_Centro_Disp = DateTime.Now;

            if (drpZone.SelectedIndex != 0 && drpZone.SelectedIndex != -1)
            {
                model.Cve_Zona = drpZone.SelectedValue;
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
                model.Cve_Estatus_Centro_Disp = (int)DisposalCenterStatus.PENDIENTE;
                model.Codigo_Centro_Disp = string.Format("{0:D3}", LsUtility.GetNumberSequence("CENTRODISP"));
            }
            else
            {
                model.Id_Centro_Disp = Convert.ToInt32(txtClave.Text);
            }

            int noEmpleados;
            int.TryParse(txtNoEmpleados.Text.Trim(), out noEmpleados);
            model.No_Empleados = noEmpleados;
            model.Marca_Analizador_Gas = txtMarcaAnalizadorGas.Text.Trim() == "" ? "" : txtMarcaAnalizadorGas.Text.Trim();
            model.Modelo_Analizador_Gas = txtModeloAnalizadorGas.Text.Trim() == "" ? "" : txtModeloAnalizadorGas.Text.Trim();
            model.Serie_Analizador_Gas = txtSerieAnalizadorGas.Text.Trim() == "" ? "" : txtSerieAnalizadorGas.Text.Trim();
            var timeDesde = new DateTime(2012, 1, 1, 00, 00, 00);
            try { timeDesde = DateTime.ParseExact(txtHorarioDesde.Text.Trim(), "HH:mm:ss", CultureInfo.CurrentCulture); }
            catch (Exception) { }
            model.Horario_Desde = timeDesde;
            var timeHasta = new DateTime(2012, 1, 1, 00, 00, 00);
            try { timeHasta = DateTime.ParseExact(txtHorarioHasta.Text.Trim(), "HH:mm:ss", CultureInfo.CurrentCulture); }
            catch (Exception) { }
            model.Horario_Hasta = timeHasta;
            model.Dias_Semana = txtDiasSemana.Text.Trim() == "" ? "" : txtDiasSemana.Text.Trim();
            model.No_Registro_Ambiental = txtNoRegistroAmbiental.Text.Trim() == "" ? "" : txtNoRegistroAmbiental.Text.Trim();
            model.Tipo = ddlTipo.SelectedValue.Trim() == "" ? "" : ddlTipo.SelectedValue.Trim();

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
            var model = new CAT_CENTRO_DISP_SUCURSALModel();
            if (drpMainCenter.SelectedIndex != 0 && drpMainCenter.SelectedIndex != -1)
            {
                model.Id_Centro_Disp = Convert.ToInt32(drpMainCenter.SelectedValue);
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
                model.Cve_Estado_Part = drpPartEstado.SelectedValue;
            }
            if (drpPartDelegMunicipio.SelectedIndex != 0 && drpPartDelegMunicipio.SelectedIndex != -1)
            {
                model.Cve_Deleg_Municipio_Part = drpPartDelegMunicipio.SelectedValue;
            }

            model.Fg_Mismo_Domicilio = ckbFiscal.Checked ? 1 : 0;

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
            
            model.Dt_Fecha_Centro_Disp_Sucursal = DateTime.Now;

            if (drpZone.SelectedIndex != 0 && drpZone.SelectedIndex != -1)
            {
                model.Cve_Zona = drpZone.SelectedValue;
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
                model.Cve_Estatus_Centro_Disp = (int)DisposalCenterStatus.PENDIENTE;
                model.Codigo_Centro_Disp_Sucursal = string.Format("{0:D3}", LsUtility.GetNumberSequence("BRANCHDISP"));
            }
            else
            {
                model.Id_Centro_Disp_Sucursal = Convert.ToInt32(txtClave.Text);
            }

            int noEmpleados;
            int.TryParse(txtNoEmpleados.Text.Trim(), out noEmpleados);
            model.No_Empleados = noEmpleados;
            model.Marca_Analizador_Gas = txtMarcaAnalizadorGas.Text.Trim() == "" ? "" : txtMarcaAnalizadorGas.Text.Trim();
            model.Modelo_Analizador_Gas = txtModeloAnalizadorGas.Text.Trim() == "" ? "" : txtModeloAnalizadorGas.Text.Trim();
            model.Serie_Analizador_Gas = txtSerieAnalizadorGas.Text.Trim() == "" ? "" : txtSerieAnalizadorGas.Text.Trim();
            var timeDesde = new DateTime(2012, 1, 1, 00, 00, 00);
            try { timeDesde = DateTime.ParseExact(txtHorarioDesde.Text.Trim(), "HH:mm:ss", CultureInfo.CurrentCulture); }
            catch (Exception) { }
            model.Horario_Desde = timeDesde;
            var timeHasta = new DateTime(2012, 1, 1, 00, 00, 00);
            try { timeHasta = DateTime.ParseExact(txtHorarioHasta.Text.Trim(), "HH:mm:ss", CultureInfo.CurrentCulture); }
            catch (Exception) { }
            model.Horario_Hasta = timeHasta;
            model.Dias_Semana = txtDiasSemana.Text.Trim() == "" ? "" : txtDiasSemana.Text.Trim();
            model.No_Registro_Ambiental = txtNoRegistroAmbiental.Text.Trim() == "" ? "" : txtNoRegistroAmbiental.Text.Trim();
            model.Tipo = ddlTipo.SelectedValue.Trim() == "" ? "" : ddlTipo.SelectedValue.Trim();

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
                if (filePath == "") return;
                //check if folder is exist, if not create it
                if (!Directory.Exists(Server.MapPath(filePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(filePath));
                }
            }
            catch (Exception ex)
            {
                var message = "Crear un directorio de imágenes no.\r\n" + ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "IOError", "alert(" + message + ");");
            }
        }

        private bool ExistsRfc()
        {
            var model = new CAT_CENTRO_DISPModel {Dx_RFC = txtRFC.Text};

            var valid = CAT_CENTRO_DISPDAL.ClassInstance.Select_MainDisposalCenter_RFC(model) > 0;

            return valid;
        }

        private bool ExistsSupplier()
        {
            var model = new CAT_CENTRO_DISPModel();

            if (Session["UserInfo"] != null)
            {
                var userModel = Session["UserInfo"] as US_USUARIOModel;

                if (userModel != null) model.Cve_Region = userModel.Id_Departamento;
            }
            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue);

            var valid = CAT_CENTRO_DISPDAL.ClassInstance.Select_MainDisposalCenter(model) > 0;
            
            return valid;
        }

        private bool ExistsSupplierBranch()
        {
            const bool valid = false;

            var model = new CAT_CENTRO_DISP_SUCURSALModel();

            if (Session["UserInfo"] != null)
            {
                var userModel = Session["UserInfo"] as US_USUARIOModel;

                if (userModel != null) model.Cve_Region = userModel.Id_Departamento;
            }
            model.Dx_Razon_Social = txtSocialName.Text.Trim() == "" ? "" : txtSocialName.Text.Trim();
            model.Cve_Zona = Convert.ToInt32(drpZone.SelectedValue);

            return valid;
        }

        private bool SaveDisposalCenter(string filePath,ref int idMod, ref int tipoMod,ref Object datosAnteriores, ref Object datosActuales)
        {
            bool success;
            var model = GetDisposalCenterData(filePath);

            if (NewAdded())
            {
                success = CAT_CENTRO_DISPDAL.ClassInstance.Insert_MainDisposalCenter(model) > 0;
                idMod = Insertlog.GetIdEmpresa("DisponsalCenter");
                tipoMod = 1;
                datosAnteriores = null;
                datosActuales = null;
            }
            else
            {
                datosAnteriores = AccesoDatos.Log.CatCentroDisp.ObtienePorId(model.Id_Centro_Disp);
                success = CAT_CENTRO_DISPDAL.ClassInstance.Update_MainDisposalCenter(model) > 0;
                idMod = model.Id_Centro_Disp;
                datosActuales = AccesoDatos.Log.CatCentroDisp.ObtienePorId(idMod);
                tipoMod = 2;

            }
            return success;
        }

        private bool SaveDisposalCenterBranch(string filePath, ref int idMod, ref int tipoMod, ref Object datosAnteriores, ref Object datosActuales)
        {
            bool success;
            var model = GetDisposalCenterBranchData(filePath);

            if (NewAdded())
            {
                success = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.Insert_BranchDisposalCenter(model) > 0;
                idMod = Insertlog.GetIdEmpresa("DisponsalCenterBranch");
                tipoMod = 1;
                datosAnteriores = null;
                datosActuales = null;
            }
            else
            {
                datosAnteriores = AccesoDatos.Log.CatCentroDispSucursal.ObtienePorId(model.Id_Centro_Disp_Sucursal);
                success = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.Update_BranchDisposalCenter(model) > 0;
                idMod = model.Id_Centro_Disp_Sucursal;
                datosActuales = AccesoDatos.Log.CatCentroDispSucursal.ObtienePorId(idMod);
                tipoMod = 2;
            }

            return success;
        }

        private void ClearData()
        {
            ckbBranch.Checked = false;
            drpMainCenter.SelectedIndex = 0;
            lblMainCenter.Visible = false;//added by tina 2012/04/27
            drpMainCenter.Visible = false;//added by tina 2012/04/27
            drpMainCenterRequired.Enabled = false;
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
            drpZone.SelectedIndex = 0;

            txtNoEmpleados.Text = "";
            txtMarcaAnalizadorGas.Text = "";
            txtModeloAnalizadorGas.Text = "";
            txtSerieAnalizadorGas.Text = "";
            txtHorarioDesde.Text = "";
            txtHorarioHasta.Text = "";
            txtDiasSemana.Text = "";
            txtNoRegistroAmbiental.Text = "";
            ddlTipo.SelectedValue = "";

            txtTelefonoAtn1.Text = "";
            txtTelefonoAtn2.Text = "";
            txtDxApPaternoRepLeg.Text = "";
            txtDxApMaternoRepLeg.Text = "";
            txtDxEmailRepreLegal.Text = "";
            txtDxTelefonoRepreLeg.Text = "";
            txtDxCelularRepreLeg.Text = "";
            txtDxApPaternoRepre.Text = "";
            txtDxApMaternoRepre.Text = "";
            txtDxCelularRepre.Text = "";
        }
        #endregion

        #region Controls Changed Events
        protected void ckbBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            if (ckbBranch.Checked)
            {
                lblMainCenter.Visible = true;
                drpMainCenter.Visible = true;
                drpMainCenterRequired.Enabled = true;
                lblCaptura.Text = @"Nombre o Razón Social (Sucursal)";
            }
            else
            {
                lblMainCenter.Visible = false;
                drpMainCenter.Visible = false;
                drpMainCenterRequired.Enabled = false;
                lblCaptura.Text = @"Nombre o Razón Social (Matriz)";
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
            if (!IsPostBack) return;
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

        #endregion
    }
}
