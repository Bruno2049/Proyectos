using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.AdminUsuarios;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.UserModule
{
    public partial class UserEdition : Page
    {
        private const int DefaultPasswordLength = 8;

        /// <summary>
        /// User ID
        /// </summary>
        public string UserId
        {
            get { return ViewState["UserId"] == null ? "" : ViewState["UserId"].ToString(); }
            set { ViewState["UserId"] = value; }
        }

        public string Estatus
        {
            get { return ViewState["Estatus"] == null ? "" : ViewState["Estatus"].ToString(); }
            set { ViewState["Estatus"] = value; }
        }

        public string DatosAnteriores
        {
            get { return ViewState["DatosAnteriores"] == null ? "" : ViewState["DatosAnteriores"].ToString(); }
            set { ViewState["DatosAnteriores"] = value; }
        }

        public string DatosActuales
        {
            get { return ViewState["DatosActuales"] == null ? "" : ViewState["DatosActuales"].ToString(); }
            set { ViewState["DatosActuales"] = value; }
        }

        /// <summary>
        /// init data when page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (IsPostBack) return;
            //Init date control
            literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //Init default data
            InitDefaultData();


            //Check is edit or new
            if (Request.QueryString["UserID"] != null && Request.QueryString["UserID"] != "")
            {
                UserId =
                    System.Text.Encoding.Default.GetString(
                        Convert.FromBase64String(Request.QueryString["UserID"].Replace("%2B", "+")));
                //Load current user information
                LoadCurrentUserInfo();
            }
            else
            {
                InitStatus("");
            }
        }

        private void InitStatus(string status)
        {
            drpStatus.Items.Clear();
            var pItem = new ListItem("Pendiente", "P");
            var aItem = new ListItem("Activo", "A");
            var iItem = new ListItem("Inactivo", "I");
            var cItem = new ListItem("Cancelado", "C");
            aItem.Selected = true;

            switch (status)
            {
                case "P": // Pendiente, Activo
                    drpStatus.Items.Add(pItem);
                    drpStatus.Items.Add(aItem);
                    break;
                case "A": // Activo, Inactivo y Cancelado
                    drpStatus.Items.Add(aItem);
                    drpStatus.Items.Add(iItem);
                    drpStatus.Items.Add(cItem);
                    break;
                case "I": // Activo, Inactivo y Cancelado
                    drpStatus.Items.Add(aItem);
                    drpStatus.Items.Add(iItem);
                    drpStatus.Items.Add(cItem);
                    break;
                case "C": // Cancelado
                    drpStatus.Items.Add(cItem);
                    cItem.Selected = true;
                    break;
                default:
                    drpStatus.Items.Add(pItem);
                    drpStatus.Items.Add(aItem);
                    break;
            }

            if (!string.IsNullOrEmpty(status))
                drpStatus.SelectedValue = status;
        }

        /// <summary>
        /// Init options
        /// </summary>
        private void InitDefaultData()
        {
            var userModel = (US_USUARIOModel) Session["UserInfo"];
            if (userModel == null) return;
            switch (userModel.Id_Rol)
            {
                case (int) UserRole.CENTRALOFFICE:
                    InitCentralRole();
                    break;
                case (int) UserRole.REGIONAL:
                    InitRegionalRole();
                    break;
                case (int) UserRole.ZONE:
                    InitZoneRole();
                    break;
            }
        }

        /// <summary>
        /// Init roles for central office
        /// </summary>
        private void InitCentralRole()
        {
            drpRole.Items.Clear();
            drpRole.Items.Add(new ListItem("", "0"));
            drpRole.Items.Add(new ListItem("Oficina Central",
                ((int) UserRole.CENTRALOFFICE).ToString(CultureInfo.InvariantCulture)));
            drpRole.Items.Add(new ListItem("Regional", ((int) UserRole.REGIONAL).ToString(CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// Init roles for regional
        /// </summary>
        private void InitRegionalRole()
        {
            drpRole.Items.Clear();
            drpRole.Items.Add(new ListItem("", "0"));
            drpRole.Items.Add(new ListItem("Proveedor", ((int) UserRole.SUPPLIER).ToString(CultureInfo.InvariantCulture)));
            drpRole.Items.Add(new ListItem("Disposición",
                ((int) UserRole.DISPOSALCENTER).ToString(CultureInfo.InvariantCulture)));
            //End
            drpRole.Items.Add(new ListItem("Zona", ((int) UserRole.ZONE).ToString(CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// Init roles for zone office
        /// </summary>
        private void InitZoneRole()
        {
            drpRole.Items.Clear();
            drpRole.Items.Add(new ListItem("", "0"));
            drpRole.Items.Add(new ListItem("Proveedor", ((int) UserRole.SUPPLIER).ToString(CultureInfo.InvariantCulture)));
            drpRole.Items.Add(new ListItem("CAyD",
                ((int) UserRole.DISPOSALCENTER).ToString(CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// Load current user information
        /// </summary>
        private void LoadCurrentUserInfo()
        {
            var userModel = US_USUARIOBll.ClassInstance.Get_US_USUARIOByPKID(UserId);
            if (userModel == null) return;
            //Visible department drop down list
            department.Style.Add(HtmlTextWriterStyle.Display, "");
            drpDepartment.Enabled = true;
            //Init controls
            txtUserName.Text = userModel.Nombre_Usuario;
            txtEmail.Text = userModel.CorreoElectronico;
            txtPhone.Text = userModel.Numero_Telefono;
            txtFullUserName.Text = userModel.Nombre_Completo_Usuario;

            drpRole.SelectedValue = userModel.Id_Rol.ToString(CultureInfo.InvariantCulture);
            var roleType = userModel.Id_Rol;
            //Load departments by role
            switch (roleType)
            {
                case (int) UserRole.REGIONAL:
                    LoadRegional();
                    drpDepartment.SelectedValue = userModel.Id_Departamento.ToString(CultureInfo.InvariantCulture);
                    branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                    lblDepartment.Text = @"Regional";
                    break;
                case (int) UserRole.SUPPLIER:
                    LoadSupplier();
                    if (userModel.Tipo_Usuario == GlobalVar.SUPPLIER)
                    {
                        drpDepartment.SelectedValue = userModel.Id_Departamento.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        drpbranch.SelectedValue = userModel.Id_Departamento.ToString(CultureInfo.InvariantCulture);
                    }
                    lblDepartment.Text = @"Proveedor Corporativo";
                    lblBranch.Text = @"Proveedor Sucursal";
                    break;
                case (int) UserRole.MANUFACTURER:
                    lblDepartment.Text = @"Fabricante";
                    branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                    break;
                case (int) UserRole.DISPOSALCENTER:
                    LoadDisposal();
                    if (userModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER)
                    {
                        drpDepartment.SelectedValue = userModel.Id_Departamento.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        drpbranch.SelectedValue = userModel.Id_Departamento.ToString(CultureInfo.InvariantCulture);
                    }
                    lblDepartment.Text = @"CAyD Corporativo";
                    lblBranch.Text = @"CAyD Sucursal";
                    break;
                case (int) UserRole.ZONE:
                    lblDepartment.Text = @"Zona";
                    branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                    LoadZona();
                    drpDepartment.SelectedValue = userModel.Id_Departamento.ToString(CultureInfo.InvariantCulture);
                    break;
                case (int) UserRole.CENTRALOFFICE:
                    department.Style.Add(HtmlTextWriterStyle.Display, "none");
                    drpDepartment.Enabled = false;
                    branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                    break;
            }
            InitStatus(userModel.Estatus);
            //disable some controls
            drpRole.Enabled = false;
            drpbranch.Enabled = false;
            drpDepartment.Enabled = false;
            divMotivos.Visible = true;
        }

        #region Metodos sin usar COMENTADOS

        /*
        /// <summary>
        /// Find supplier with supplier branch
        /// </summary>
        /// <param name="supplierBranchId">Supplier Branch</param>
        /// <returns></returns>
        private string FindSupplierId(int supplierBranchId)
        {
            var supplierId = "";

            var dtSupplierBranch = SupplierBrancheDal.ClassInstance.GetSupplierBranch(supplierBranchId);
            if (dtSupplierBranch != null && dtSupplierBranch.Rows.Count > 0)
            {
                supplierId = dtSupplierBranch.Rows[0]["Id_Proveedor"].ToString();
            }

            return supplierId;
        }
*/

        /*
                private string FindDisposalId(int disposalBranchId)
                {
                    var disposalId = "";

                    var dtDisposalBranch = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetDisposalByBranch(disposalBranchId);
                    if (dtDisposalBranch != null && dtDisposalBranch.Rows.Count > 0)
                    {
                        disposalId = dtDisposalBranch.Rows[0]["Id_Centro_Disp"].ToString();
                    }

                    return disposalId;
                }
        */

        #endregion

        /// <summary>
        /// Save user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            var iResult = 0;
            var userModel = new US_USUARIOModel();
            //Verify the page data input is reasonable
            //if (!IsValid) return;
            string message;
            if (!string.IsNullOrEmpty(UserId))
            {
                var originalUserModel = US_USUARIOBll.ClassInstance.Get_US_USUARIOByPKID(UserId);
                if (originalUserModel != null)
                {
                    if (Changes(originalUserModel))
                    {
                        userModel.Id_Usuario = originalUserModel.Id_Usuario;
                        userModel.Nombre_Usuario = txtUserName.Text;
                        userModel.CorreoElectronico = txtEmail.Text;
                        userModel.Numero_Telefono = txtPhone.Text;
                        userModel.Contrasena = originalUserModel.Contrasena;
                        userModel.Nombre_Completo_Usuario = txtFullUserName.Text;
                        userModel.Estatus = drpStatus.SelectedValue;
                        userModel.Id_Rol = originalUserModel.Id_Rol;
                        userModel.Id_Departamento = originalUserModel.Id_Departamento;
                        userModel.Tipo_Usuario = originalUserModel.Tipo_Usuario;

                        Estatus = originalUserModel.Estatus;

                        if (!userModel.Nombre_Usuario.Equals(originalUserModel.Nombre_Usuario,StringComparison.InvariantCultureIgnoreCase)) //Changed by Jerry 2011/08/16
                        {
                            if (US_USUARIOBll.ClassInstance.IsUserNameExist(userModel.Nombre_Usuario))
                            {
                                message = (string) GetGlobalResourceObject("DefaultResource", "msgUserNameExist");
                                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "UserExist",
                                    "alert('" + message + "');", true);
                                txtUserName.Focus();
                                return;
                            }
                        }

                        if (originalUserModel.Estatus != drpStatus.SelectedValue)
                        {
                            MailUtility.StatusChangeEmail(userModel.Nombre_Usuario, userModel.CorreoElectronico,
                                drpStatus.SelectedItem.Text);
                            InitStatus(drpStatus.SelectedValue);
                        }

                        if (txtMotivos.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "NotSelectRole",
                                "alert('¡Debe ingresar el motivo por el cual esta realizando cambios al usuario!')",
                                true);
                        }
                        else
                        {
                            iResult = US_USUARIOBll.ClassInstance.Update_US_USUARIO(userModel);

                            if (iResult > 0)
                            {
                                if (Estatus != drpStatus.SelectedValue)
                                {
                                    //var pItem = new ListItem("Pendiente", "P");
                                    //var aItem = new ListItem("Activo", "A");
                                    //var iItem = new ListItem("Inactivo", "I");
                                    //var cItem = new ListItem("Cancelado", "C");
                                    if (drpStatus.SelectedValue == "C")
                                    {
                                        /*INSERTAR EVENTO CANCELADO DE USUARIO EN LOG*/
                                        var cambios = Insertlog.GetCambiosDatos(originalUserModel, userModel);
                                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "USUARIOS",
                                            "CANCELADO", userModel.Nombre_Usuario,
                                            txtMotivos.Text, "", cambios[0], cambios[1]);
                                    }
                                    if (drpStatus.SelectedValue == "I")
                                    {
                                        /*INSERTAR EVENTO INHABILITAR DE USUARIO EN LOG*/
                                        var cambios = Insertlog.GetCambiosDatos(originalUserModel, userModel);
                                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "USUARIOS",
                                            "INHABILITAR", userModel.Nombre_Usuario,
                                            txtMotivos.Text, "", cambios[0], cambios[1]);
                                    }
                                    if (drpStatus.SelectedValue == "A")
                                    {
                                        /*INSERTAR EVENTO REACTIVAR DE USUARIO EN LOG*/
                                        var cambios = Insertlog.GetCambiosDatos(originalUserModel, userModel);
                                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "USUARIOS",
                                            "REACTIVAR", userModel.Nombre_Usuario,
                                            txtMotivos.Text, "", cambios[0], cambios[1]);
                                    }
                                }
                                else
                                {
                                    /*INSERTAR EVENTO CAMBIO DE USUARIO EN LOG*/
                                    var cambios = Insertlog.GetCambiosDatos(originalUserModel, userModel);
                                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                        Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "USUARIOS", "CAMBIO",
                                       userModel.Nombre_Usuario, txtMotivos.Text,
                                        "",cambios[0],cambios[1]);
                                }
                                //Show success message
                                message = (string)GetGlobalResourceObject("DefaultResource", "msgSaveUserSuccess");
                                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "InsertSuccess",
                                    "confirm('" + message + "');", true);
                            }
                            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "ChangeCursorStatus",
                                "document.body.style.cursor = 'default';", true);
                        }
                    }
                }
            }
            else
            {
                //DO insert logic
                var encryptPassword = ValidacionesUsuario.Encriptar(RandomPassword.Generate(DefaultPasswordLength));

                userModel.Nombre_Usuario = txtUserName.Text;
                userModel.CorreoElectronico = txtEmail.Text;
                userModel.Numero_Telefono = txtPhone.Text;
                userModel.Contrasena = encryptPassword;
                //userModel.Contrasena = RandomPassword.Generate(DefaultPasswordLength);
                userModel.Nombre_Completo_Usuario = txtFullUserName.Text;
                userModel.Id_Rol = drpRole.SelectedValue != "" ? Convert.ToInt32(drpRole.SelectedValue) : 0;

                if (drpRole.SelectedIndex == 0 || drpRole.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "NotSelectRole",
                        "alert('¡Debe seleccionarse un Rol !')", true);
                }
                else
                {
                    if (userModel.Id_Rol == (int) UserRole.SUPPLIER ||
                        userModel.Id_Rol == (int) UserRole.DISPOSALCENTER)
                    {

                        if (drpbranch.SelectedIndex != -1 && drpbranch.SelectedIndex != 0)
                            //branch selected to save branch user
                        {
                            userModel.Id_Departamento = Convert.ToInt32(drpbranch.SelectedValue);
                        }
                        else if (drpDepartment.SelectedIndex != -1 && drpDepartment.SelectedIndex != 0)
                            //save supplier user
                        {
                            userModel.Id_Departamento = Convert.ToInt32(drpDepartment.SelectedValue);
                        }
                        else
                        {
                            message = (string) GetGlobalResourceObject("DefaultResource", "msgMissingSelection");
                            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "MissingSelection",
                                "alert('" + message + "');", true);
                            return;
                        }
                    }
                    else
                    {
                        userModel.Id_Departamento = drpDepartment.SelectedValue != ""
                            ? Convert.ToInt32(drpDepartment.SelectedValue)
                            : 0;
                    }
                    userModel.Estatus = drpStatus.SelectedValue;
                    switch (userModel.Id_Rol)
                    {
                        case (int) UserRole.SUPPLIER:
                            if (drpbranch.SelectedIndex != -1 && drpbranch.SelectedIndex != 0)
                                //branch selected to save branch user
                            {
                                userModel.Tipo_Usuario = GlobalVar.SUPPLIER_BRANCH;
                            }
                            else
                            {
                                userModel.Tipo_Usuario = GlobalVar.SUPPLIER;
                            }
                            break;
                        case (int) UserRole.REGIONAL:
                            userModel.Tipo_Usuario = GlobalVar.REGIONAL_OFFICE;
                            break;
                        case (int) UserRole.MANUFACTURER:
                            userModel.Tipo_Usuario = GlobalVar.MANUFACTURER;
                            break;
                        case (int) UserRole.DISPOSALCENTER:
                            if (drpbranch.SelectedIndex != -1 && drpbranch.SelectedIndex != 0)
                                //branch selected to save branch user
                            {
                                userModel.Tipo_Usuario = GlobalVar.DISPOSAL_CENTER_BRANCH;
                            }
                            else
                            {
                                userModel.Tipo_Usuario = GlobalVar.DISPOSAL_CENTER;
                            }
                            break;
                        case (int) UserRole.CENTRALOFFICE:
                            userModel.Tipo_Usuario = GlobalVar.CENTRAL_OFFICE;
                            break;
                        case (int) UserRole.ZONE:
                            userModel.Tipo_Usuario = GlobalVar.ZONE_OFFICE;
                            break;
                    }

                    if (!US_USUARIOBll.ClassInstance.IsUserNameExist(txtUserName.Text))
                    {
                        if ((drpDepartment.SelectedIndex == 0 || drpDepartment.SelectedIndex == -1))
                        {
                            if (int.Parse(drpRole.SelectedValue) == (int) UserRole.ZONE)
                            {
                                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "NotSelectZone",
                                    "alert('? Debe  seleccionarse una Zona !')", true);
                            }
                            else
                            {
                                iResult = US_USUARIOBll.ClassInstance.Insert_US_USUARIO(userModel);
                            }
                        }
                        else
                        {
                            iResult = US_USUARIOBll.ClassInstance.Insert_US_USUARIO(userModel);
                        }

                        if (iResult > 0)
                        {
                            try
                            {
                                /*INSERTAR EVENTO ALTA DE USUARIO EN LOG*/
                                //var idUsuarioGenerado = Insertlog.GetIdUser(userModel.Nombre_Usuario);
                                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                                    Convert.ToInt16(Session["IdDepartamento"]), "USUARIOS", "ALTA",
                                    userModel.Nombre_Usuario, "",
                                    "Fecha alta: " + DateTime.Now, "", "");

                                //clear user interfaces
                                CleanData();
                                //Send email
                                var decryptPassword = ValidacionesUsuario.Desencriptar(userModel.Contrasena);
                                MailUtility.RegisterEmail(userModel.Nombre_Usuario, userModel.CorreoElectronico,
                                    decryptPassword);

                                //Show success message
                                message = (string) GetGlobalResourceObject("DefaultResource", "msgSaveUserSuccess");
                                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "InsertSuccess",
                                    "confirm('" + message + "');", true);
                            }
                            catch (Exception ee)
                            {
                                message = (string)GetGlobalResourceObject("DefaultResource", "msgSaveUserSuccess") +
                                          " Con error al enviar el correo electrónico";//(string)GetGlobalResourceObject("DefaultResource", "err");
                                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "InsertSuccess",
                                    "confirm('" + message + "');", true);

                            }
                        }
                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "ChangeCursorStatus",
                            "document.body.style.cursor = 'default';", true);
                    }
                    else
                    {
                        message = (string) GetGlobalResourceObject("DefaultResource", "msgUserNameExist");
                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "UserExist",
                            "alert('" + message + "');", true);
                        txtUserName.Focus();
                    }
                }

            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "ChangeCursorStatus",
                "document.body.style.cursor = 'default';", true);
        }

        /// <summary>
        /// Clean all controls to initial status
        /// </summary>
        private void CleanData()
        {
            txtUserName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtFullUserName.Text = "";
            drpRole.SelectedIndex = 0;
            if (drpDepartment.Items.Count > 0)
            {
                drpDepartment.SelectedIndex = 0;
            }
            if (drpbranch.Items.Count > 0)
            {
                drpbranch.SelectedIndex = 0;
            }
            InitStatus("");
            //Visible department drop down list
            department.Style.Add(HtmlTextWriterStyle.Display, "none");
            drpDepartment.Enabled = false;
            branch.Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        /// <summary>
        /// Check if there are some values changed
        /// </summary>
        /// <param name="originalUserModel"></param>
        /// <returns></returns>
        private bool Changes(US_USUARIOModel originalUserModel)
        {
            var bResult =
                !originalUserModel.Nombre_Usuario.Equals(txtUserName.Text, StringComparison.OrdinalIgnoreCase) ||
                !originalUserModel.CorreoElectronico.Equals(txtEmail.Text, StringComparison.OrdinalIgnoreCase) ||
                !originalUserModel.Numero_Telefono.Equals(txtPhone.Text, StringComparison.OrdinalIgnoreCase) ||
                !originalUserModel.Nombre_Completo_Usuario.Equals(txtFullUserName.Text,
                    StringComparison.OrdinalIgnoreCase)
                    /*!OriginalUserModel.Direccion.Equals(txtAddress.Text, StringComparison.OrdinalIgnoreCase) */||
                !originalUserModel.Estatus.Equals(drpStatus.SelectedValue);

            return bResult;
        }

        /// <summary>
        /// cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsersManager.aspx");
        }

        /// <summary>
        /// Role changes to load departments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpRole.SelectedIndex != -1 && drpRole.SelectedIndex != 0)
            {
                //Visible department drop down list
                department.Style.Add(HtmlTextWriterStyle.Display, "");
                drpDepartment.Enabled = true;

                var roleType = Convert.ToInt32(drpRole.SelectedValue);
                //Load departments by role
                switch (roleType)
                {
                    case (int) UserRole.REGIONAL:
                        lblDepartment.Text = @"Regional";
                        branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        LoadRegional();
                        break;
                    case (int) UserRole.SUPPLIER:
                        LoadSupplier();
                        lblDepartment.Text = @"Proveedor Corporativo";
                        lblBranch.Text = @"Proveedor Sucursal";
                        break;
                    case (int) UserRole.MANUFACTURER:
                        branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        lblDepartment.Text = @"Fabricante";
                        break;
                    case (int) UserRole.DISPOSALCENTER:
                        LoadDisposal();
                        lblDepartment.Text = @"CAyD Corporativo";
                        lblBranch.Text = @"CAyD Sucursal";
                        break;
                    case (int) UserRole.CENTRALOFFICE:
                        department.Style.Add(HtmlTextWriterStyle.Display, "none");
                        drpDepartment.Enabled = false;
                        branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        break;
                    case (int) UserRole.ZONE:
                        lblDepartment.Text = @"Zona";
                        branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        LoadZona();
                        break;
                }
            }
            else
            {
                //Hide department drop down list
                department.Style.Add(HtmlTextWriterStyle.Display, "none");
                drpDepartment.Enabled = false;
                //Valdepartment.Enabled = false;
                branch.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
        }

        /// <summary>
        /// Init regional drop down list
        /// </summary>
        private void LoadRegional()
        {
            //load suppliers
            var dtRegionals = RegionalDal.ClassInstance.GetRegionals();
            if (dtRegionals == null) return;
            drpDepartment.DataSource = dtRegionals;
            drpDepartment.DataTextField = "Dx_Nombre_Region";
            drpDepartment.DataValueField = "Cve_Region";
            drpDepartment.DataBind();
            drpDepartment.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Load zones
        /// </summary>
        private void LoadZona()
        {
            US_USUARIOModel userModel = null;
            if (Session["UserInfo"] != null)
            {
                userModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (userModel == null) return;
            var regional = userModel.Id_Departamento;
            var dtZona = CatZonaDal.ClassInstance.GetZonaWithRegional(regional);
            if (dtZona == null) return;
            drpDepartment.DataSource = dtZona;
            drpDepartment.DataTextField = "Dx_Nombre_Zona";
            drpDepartment.DataValueField = "Cve_Zona";
            drpDepartment.DataBind();
            drpDepartment.Items.Insert(0, new ListItem("", "0"));
        }

        /// <summary>
        /// Init supplier drop down list
        /// </summary>
        private void LoadSupplier()
        {
            //Visible department drop down list
            branch.Style.Add(HtmlTextWriterStyle.Display, "");
            //load suppliers
            DataTable dtSuppliers = null;
            DataTable dtSupplierBranches = null;
            US_USUARIOModel userModel = null;
            if (Session["UserInfo"] != null)
            {
                userModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (userModel == null) return;
            int pageCount;
            switch (userModel.Id_Rol)
            {
                case (int) UserRole.REGIONAL:
                {
                    var region = userModel.Id_Departamento;
                    dtSuppliers = SupplierDal.ClassInstance.GetRegionSuppliers(region);
                    dtSupplierBranches = CAT_PROVEEDORDal.ClassInstance.Get_Provider("", "S", "2", region, 1, 999999,
                        out pageCount);
                }
                    break;
                case (int) UserRole.ZONE:
                {
                    var zone = userModel.Id_Departamento;
                    dtSuppliers = SupplierDal.ClassInstance.GetSuppliers(zone);
                    dtSupplierBranches =
                        CAT_PROVEEDORDal.ClassInstance.Get_Provider(zone.ToString(CultureInfo.InvariantCulture), "S",
                            "2", 0, 1, 999999, out pageCount);
                }
                    break;
            }

            if (dtSuppliers != null)
            {
                drpDepartment.DataSource = dtSuppliers;
                drpDepartment.DataTextField = "Dx_Nombre_Comercial";
                drpDepartment.DataValueField = "Id_Proveedor";
                drpDepartment.DataBind();
                drpDepartment.Items.Insert(0, new ListItem("", "0"));
                drpDepartment.Enabled = true;
            }
            else
            {
                drpDepartment.Items.Clear();
                drpDepartment.Enabled = false;
            }
            if (dtSupplierBranches != null)
            {
                drpbranch.DataSource = dtSupplierBranches;
                drpbranch.DataTextField = "Dx_Nombre_Comercial";
                drpbranch.DataValueField = "ID";
                drpbranch.DataBind();
                drpbranch.Items.Insert(0, new ListItem("", "0"));
                drpbranch.Enabled = true;
            }
            else
            {
                drpbranch.Items.Clear();
                drpbranch.Enabled = false;
            }
        }

        /// <summary>
        /// Show branches related with department 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpbranch.Enabled = drpDepartment.SelectedIndex <= 0;
        }

        protected void drpbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpDepartment.Enabled = drpbranch.SelectedIndex <= 0;
        }

        #region Metodos sin usar COMENTADOS
        /*
        /// <summary>
        /// Init supplier branches
        /// </summary>
        /// <param name="supplierId"></param>
        private void InitSupplierBranches(int supplierId)
        {
            US_USUARIOModel userModel = null;
            if (Session["UserInfo"] != null)
            {
                userModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (supplierId <= 0)
            {
                drpbranch.Items.Clear();
                drpbranch.Items.Insert(0, new ListItem("", "0"));
            }
            else if (userModel != null)
            {
                //zone = UserModel.Id_Departamento;
                //if (SupplierID < 0)
                //{
                //    dtSupplierBranches = SupplierBrancheDal.ClassInstance.GetSupplierBranch(SupplierID, zone);
                //}
                //else
                //{
                //    dtSupplierBranches = SupplierBrancheDal.ClassInstance.GetSupplierBranches(SupplierID, zone);
                //}

                var dtSupplierBranches = SupplierBrancheDal.ClassInstance.GetSupplierBranchWithSupplier(supplierId);
                if (dtSupplierBranches == null) return;
                drpbranch.DataSource = dtSupplierBranches;
                drpbranch.DataTextField = "Dx_Nombre_Comercial";
                drpbranch.DataValueField = "Id_Branch";
                drpbranch.DataBind();
                drpbranch.Items.Insert(0, new ListItem("", "0"));
            }
        }
*/
        /*
                /// <summary>
                /// Init disposal branches 
                /// </summary>
                /// <param name="disposalId"></param>
                private void InitDisposalBranches(int disposalId)
                {
                    //int zone = -1;
                    US_USUARIOModel userModel = null;
                    if (Session["UserInfo"] != null)
                    {
                        userModel = Session["UserInfo"] as US_USUARIOModel;
                    }
                    if (disposalId <= 0)
                    {
                        drpbranch.Items.Clear();
                        drpbranch.Items.Insert(0, new ListItem("", "0"));
                    }
                    else if (userModel != null)
                    {
                        var dtDisposalBranches = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetDisposalBranchByDisposal(disposalId.ToString(CultureInfo.InvariantCulture));
                        if (dtDisposalBranches == null) return;
                        drpbranch.DataSource = dtDisposalBranches;
                        drpbranch.DataTextField = "Dx_Nombre_Comercial";
                        drpbranch.DataValueField = "Id_Centro_Disp_Sucursal";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, new ListItem("", "0"));
                    }
                }
        */

        #endregion

        /// <summary>
        /// Load disposal centers
        /// </summary>
        private void LoadDisposal()
        {
            branch.Style.Add(HtmlTextWriterStyle.Display, "");
            DataTable dtDisposal = null;
            DataTable disposalCenters = null;
            US_USUARIOModel userModel = null;
            if (Session["UserInfo"] != null)
            {
                userModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (userModel == null) return;
            int pageCount;
            switch (userModel.Id_Rol)
            {
                case (int) UserRole.ZONE:
                {
                    var zone = userModel.Id_Departamento;
                    dtDisposal = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposals(zone);
                    disposalCenters = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterAndBranchWithZoneAndStatus(0,
                        zone.ToString(CultureInfo.InvariantCulture), "S", 2, "", 1, 999999, out pageCount);
                }
                    break;
                case (int) UserRole.REGIONAL:
                {
                    var region = userModel.Id_Departamento;
                    dtDisposal = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalsByRegional(region);
                    disposalCenters =
                        CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterAndBranchWithZoneAndStatus(region, "", "S", 2,
                            "", 1, 999999, out pageCount);
                }
                    break;
            }

            if (dtDisposal != null)
            {
                drpDepartment.DataSource = dtDisposal;
                drpDepartment.DataTextField = "Dx_Nombre_Comercial";
                drpDepartment.DataValueField = "Id_Centro_Disp";
                drpDepartment.DataBind();
                drpDepartment.Items.Insert(0, new ListItem("", "0"));
                drpDepartment.Enabled = true;
            }
            else
            {
                drpDepartment.Items.Clear();
                drpDepartment.Enabled = false;
            }
            if (disposalCenters != null)
            {
                drpbranch.DataSource = disposalCenters;
                drpbranch.DataTextField = "Dx_Nombre_Comercial";
                drpbranch.DataValueField = "Id_Centro_Disp";
                drpbranch.DataBind();
                drpbranch.Items.Insert(0, new ListItem("", "0"));
                drpbranch.Enabled = true;
            }
            else
            {
                drpbranch.Items.Clear();
                drpbranch.Enabled = false;
            }
        }
    }
}
