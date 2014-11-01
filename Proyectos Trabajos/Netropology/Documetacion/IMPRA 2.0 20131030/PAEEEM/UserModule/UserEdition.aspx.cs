using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Sockets;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;

namespace PAEEEM
{
    public partial class UserEdition : System.Web.UI.Page
    {
        private const int DEFAULT_PASSWORD_LENGTH = 8;
        /// <summary>
        /// User ID
        /// </summary>
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
        /// <summary>
        /// init data when page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["UserInfo"] == null)
                //{
                //    Response.Redirect("../Login/Login.aspx");
                //    return;
                //}
                //Init date control
                this.literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                //Init default data
                InitDefaultData();

                //Check is edit or new
                if (Request.QueryString["UserID"] != null && Request.QueryString["UserID"].ToString() != "")
                {
                    UserID = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["UserID"].ToString().Replace("%2B", "+")));
                    //Load current user information
                    LoadCurrentUserInfo();
                }
                else
                {
                    InitStatus("");
                }
            }
        }
        private void InitStatus(string status)
        {
            drpStatus.Items.Clear();
            ListItem pItem = new ListItem("Pendiente", "P");
            ListItem aItem = new ListItem("Activo", "A");
            ListItem iItem = new ListItem("Inactivo", "I");
            ListItem cItem = new ListItem("Cancelado", "C");
            aItem.Selected = true;

            switch(status)
            {
                case "P":
                    // Pendiente, Activo
                    drpStatus.Items.Add(pItem);
                    drpStatus.Items.Add(aItem);
                    break;
                case "A":
                    // Activo, Inactivo y Cancelado
                    drpStatus.Items.Add(aItem);
                    drpStatus.Items.Add(iItem);
                    drpStatus.Items.Add(cItem);
                    break;
                case "I":
                    // Activo, Inactivo y Cancelado
                    drpStatus.Items.Add(aItem);
                    drpStatus.Items.Add(iItem);
                    drpStatus.Items.Add(cItem);
                    break;
                case "C":
                    // Cancelado
                    drpStatus.Items.Add(cItem);
                    cItem.Selected = true;
                    break;
                default:
                    drpStatus.Items.Add(pItem);
                    drpStatus.Items.Add(aItem);
                    break;
            }

            if (!string.IsNullOrEmpty(status))
                this.drpStatus.SelectedValue = status;
        }
        /// <summary>
        /// Init options
        /// </summary>
        private void InitDefaultData()
        { 
            US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];
            if (UserModel != null)
            {
                if (UserModel.Id_Rol == (int)UserRole.CENTRALOFFICE)
                {
                    InitCentralRole();
                }
                else if (UserModel.Id_Rol == (int)UserRole.REGIONAL)
                {
                    InitRegionalRole();
                }
                //Add by Jerry 2011/08/02 used for zone office to add users
                else if (UserModel.Id_Rol == (int)UserRole.ZONE)
                {
                    InitZoneRole();
                }
                //End
                else
                {
                    //Do nothing for other role user; 
                }
            }
        }
        /// <summary>
        /// Init roles for central office
        /// </summary>
        private void InitCentralRole()
        { 
            this.drpRole.Items.Clear();
            this.drpRole.Items.Add(new ListItem("", "0"));
            this.drpRole.Items.Add(new ListItem("Oficina Central", ((int)UserRole.CENTRALOFFICE).ToString()));
            this.drpRole.Items.Add(new ListItem("Regional",((int)UserRole.REGIONAL).ToString()));
            //this.drpRole.Items.Add(new ListItem("Proveedor",((int)UserRole.SUPPLIER).ToString()));
            //this.drpRole.Items.Add(new ListItem("Fabricante",((int)UserRole.MANUFACTURER).ToString()));
            //this.drpRole.Items.Add(new ListItem("CAyD",((int)UserRole.DISPOSALCENTER).ToString()));
        }
        /// <summary>
        /// Init roles for regional
        /// </summary>
        private void InitRegionalRole()
        {
            this.drpRole.Items.Clear();
            this.drpRole.Items.Add(new ListItem("", "0"));
            //Comment by Jerry 2011/08/02 
            this.drpRole.Items.Add(new ListItem("Proveedor", ((int)UserRole.SUPPLIER).ToString()));
            this.drpRole.Items.Add(new ListItem("Disposición", ((int)UserRole.DISPOSALCENTER).ToString()));
            //End
            this.drpRole.Items.Add(new ListItem("Zona", ((int)UserRole.ZONE).ToString()));
        }
        /// <summary>
        /// Init roles for zone office
        /// </summary>
        private void InitZoneRole()
        {
            this.drpRole.Items.Clear();
            this.drpRole.Items.Add(new ListItem("", "0"));
            this.drpRole.Items.Add(new ListItem("Proveedor", ((int)UserRole.SUPPLIER).ToString()));
            this.drpRole.Items.Add(new ListItem("CAyD", ((int)UserRole.DISPOSALCENTER).ToString()));
        }
        /// <summary>
        /// Load current user information
        /// </summary>
        private void LoadCurrentUserInfo()
        {
            US_USUARIOModel UserModel = US_USUARIOBll.ClassInstance.Get_US_USUARIOByPKID(UserID);
            if (UserModel != null)
            {
                //Visible department drop down list
                this.department.Style.Add(HtmlTextWriterStyle.Display, "");
                this.drpDepartment.Enabled = true;
                //this.Valdepartment.Enabled = true;
                //Init controls
                this.txtUserName.Text = UserModel.Nombre_Usuario;
                this.txtEmail.Text = UserModel.CorreoElectronico;
                this.txtPhone.Text = UserModel.Numero_Telefono;
                // this.txtAddress.Text = UserModel.Direccion;
                this.txtFullUserName.Text = UserModel.Nombre_Completo_Usuario;

                this.drpRole.SelectedValue = UserModel.Id_Rol.ToString();
                int RoleType;
                RoleType = UserModel.Id_Rol;
                //Load departments by role
                switch (RoleType)
                {
                    case (int)UserRole.REGIONAL:
                        LoadRegional();
                        this.drpDepartment.SelectedValue = UserModel.Id_Departamento.ToString();
                        this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        this.lblDepartment.Text = "Regional";
                        break;
                    case (int)UserRole.SUPPLIER:
                        LoadSupplier();
                        if (UserModel.Tipo_Usuario == GlobalVar.SUPPLIER)
                        {
                            this.drpDepartment.SelectedValue = UserModel.Id_Departamento.ToString();
                        }
                        else
                        {
                            //Find supplier id by supplier branch id
                            //this.drpDepartment.SelectedValue = FindSupplierID(UserModel.Id_Departamento);
                            //InitSupplierBranches(UserModel.Id_Departamento);
                            //InitSupplierBranches(int.Parse(this.drpDepartment.SelectedValue));
                            this.drpbranch.SelectedValue = UserModel.Id_Departamento.ToString();
                        }
                        this.lblDepartment.Text = "Proveedor Corporativo";
                        this.lblBranch.Text = "Proveedor Sucursal";
                        break;
                    case (int)UserRole.MANUFACTURER:
                        this.lblDepartment.Text = "Fabricante";
                        this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        break;
                    case (int)UserRole.DISPOSALCENTER:
                        LoadDisposal();
                        if (UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER)
                        {
                            this.drpDepartment.SelectedValue = UserModel.Id_Departamento.ToString();
                        }
                        else
                        {
                            //this.drpDepartment.SelectedValue = FindDisposalID(UserModel.Id_Departamento);
                            //InitDisposalBranches(UserModel.Id_Departamento);
                            //InitDisposalBranches(int.Parse(this.drpDepartment.SelectedValue));
                            this.drpbranch.SelectedValue = UserModel.Id_Departamento.ToString();
                        }
                        this.lblDepartment.Text = "CAyD Corporativo";
                        this.lblBranch.Text = "CAyD Sucursal";
                        break;
                    case (int)UserRole.ZONE:
                        this.lblDepartment.Text = "Zona";
                        this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        LoadZona();
                        this.drpDepartment.SelectedValue = UserModel.Id_Departamento.ToString();
                        break;
                    case (int)UserRole.CENTRALOFFICE:
                        this.department.Style.Add(HtmlTextWriterStyle.Display, "none");
                        this.drpDepartment.Enabled = false;
                        //this.Valdepartment.Enabled = false;
                        this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        break;
                    default:
                        break;
                }
                InitStatus(UserModel.Estatus);
                //disable some controls
                this.drpRole.Enabled = false;
                this.drpbranch.Enabled = false;
                this.drpDepartment.Enabled = false;
            }
        }
        /// <summary>
        /// Find supplier with supplier branch
        /// </summary>
        /// <param name="supplierBranchID">Supplier Branch</param>
        /// <returns></returns>
        private string FindSupplierID(int supplierBranchID)
        {
            string supplierID = "";

            DataTable dtSupplierBranch = SupplierBrancheDal.ClassInstance.GetSupplierBranch(supplierBranchID);
            if (dtSupplierBranch != null && dtSupplierBranch.Rows.Count > 0)
            {
                supplierID = dtSupplierBranch.Rows[0]["Id_Proveedor"].ToString();
            }

            return supplierID;
        }
        private string FindDisposalID(int disposalBranchID)
        {
            string disposalID = "";

            DataTable dtDisposalBranch = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetDisposalByBranch(disposalBranchID);
            if (dtDisposalBranch != null && dtDisposalBranch.Rows.Count > 0)
            {
                disposalID = dtDisposalBranch.Rows[0]["Id_Centro_Disp"].ToString();
            }

            return disposalID;
        }
        /// <summary>
        /// Save user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            int iResult = 0;
            string Message;
            US_USUARIOModel UserModel = new US_USUARIOModel();
            //Verify the page data input is reasonable
            if (this.IsValid)//add by mark 2012-07-12
            {
                if (!string.IsNullOrEmpty(UserID))
                {
                    //Do update logic
                    US_USUARIOModel OriginalUserModel = US_USUARIOBll.ClassInstance.Get_US_USUARIOByPKID(UserID);
                    if (OriginalUserModel != null)
                    {
                        if (Changes(OriginalUserModel))
                        {
                            UserModel.Id_Usuario = OriginalUserModel.Id_Usuario;
                            UserModel.Nombre_Usuario = this.txtUserName.Text;
                            UserModel.CorreoElectronico = this.txtEmail.Text;
                            UserModel.Numero_Telefono = this.txtPhone.Text;
                            UserModel.Contrasena = OriginalUserModel.Contrasena;
                            //UserModel.Direccion = this.txtAddress.Text;
                            UserModel.Nombre_Completo_Usuario = this.txtFullUserName.Text;
                            UserModel.Estatus = this.drpStatus.SelectedValue;
                            UserModel.Id_Rol = OriginalUserModel.Id_Rol;
                            UserModel.Id_Departamento = OriginalUserModel.Id_Departamento;
                            UserModel.Tipo_Usuario = OriginalUserModel.Tipo_Usuario;

                            if (!UserModel.Nombre_Usuario.Equals(OriginalUserModel.Nombre_Usuario, StringComparison.InvariantCultureIgnoreCase))//Changed by Jerry 2011/08/16
                            {
                                if (US_USUARIOBll.ClassInstance.IsUserNameExist(UserModel.Nombre_Usuario))
                                {
                                    Message = (string)GetGlobalResourceObject("DefaultResource", "msgUserNameExist");
                                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UserExist", "alert('" + Message + "');", true);
                                    this.txtUserName.Focus();
                                    return;
                                }
                            }

                            //edit by coco 2011-08-12
                            if (OriginalUserModel.Estatus != this.drpStatus.SelectedValue)
                            {
                                //MailUtility.SendCompleted += new System.Net.Mail.SendCompletedEventHandler(MailUtility_SendCompleted);
                                MailUtility.StatusChangeEmail(UserModel.Nombre_Usuario, UserModel.CorreoElectronico, drpStatus.SelectedItem.Text);
                                InitStatus(this.drpStatus.SelectedValue);
                            }
                            iResult = US_USUARIOBll.ClassInstance.Update_US_USUARIO(UserModel);
                            if (iResult > 0)
                            {
                                Message = (string)GetGlobalResourceObject("DefaultResource", "msgUpdateUserSuccess");
                                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UpdateSuccess", "confirm('" + Message + "');", true);
                            }
                            //Changed by Jerry 2011/09/07
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "ChangeCursorStatus", "document.body.style.cursor = 'default';", true);
                        }
                    }
                }
                else
                {
                    //DO insert logic
                    UserModel.Nombre_Usuario = this.txtUserName.Text;
                    UserModel.CorreoElectronico = this.txtEmail.Text;
                    UserModel.Numero_Telefono = this.txtPhone.Text;
                    UserModel.Contrasena = RandomPassword.Generate(DEFAULT_PASSWORD_LENGTH);
                    //UserModel.Direccion = this.txtAddress.Text;
                    UserModel.Nombre_Completo_Usuario = this.txtFullUserName.Text;
                    UserModel.Id_Rol = this.drpRole.SelectedValue.ToString() != "" ? Convert.ToInt32(this.drpRole.SelectedValue) : 0;
                    //added by mark 2012-07-11
                    if (drpRole.SelectedIndex == 0 || drpRole.SelectedIndex == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "NotSelectRole", "alert('¡Debe seleccionarse un Rol !')", true);
                    }
                    else
                    { // add by mark 2012-07-11
                        if (UserModel.Id_Rol == (int)UserRole.SUPPLIER || UserModel.Id_Rol == (int)UserRole.DISPOSALCENTER)
                        {

                            if (this.drpbranch.SelectedIndex != -1 && this.drpbranch.SelectedIndex != 0)//branch selected to save branch user
                            {
                                UserModel.Id_Departamento = Convert.ToInt32(this.drpbranch.SelectedValue);
                            }
                            else if (this.drpDepartment.SelectedIndex != -1 && this.drpDepartment.SelectedIndex != 0)//save supplier user
                            {
                                UserModel.Id_Departamento = Convert.ToInt32(this.drpDepartment.SelectedValue);
                            }
                            else
                            {
                                Message = (string)GetGlobalResourceObject("DefaultResource", "msgMissingSelection");
                                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "MissingSelection", "alert('" + Message + "');", true);
                                return;
                            }
                        }
                        else
                        {
                            UserModel.Id_Departamento = this.drpDepartment.SelectedValue.ToString() != "" ? Convert.ToInt32(this.drpDepartment.SelectedValue) : 0;
                        }
                        UserModel.Estatus = this.drpStatus.SelectedValue;
                        if (UserModel.Id_Rol == (int)UserRole.SUPPLIER)
                        {
                            if (this.drpbranch.SelectedIndex != -1 && this.drpbranch.SelectedIndex != 0)//branch selected to save branch user
                            {
                                UserModel.Tipo_Usuario = GlobalVar.SUPPLIER_BRANCH;
                            }
                            else
                            {
                                UserModel.Tipo_Usuario = GlobalVar.SUPPLIER;
                            }
                        }
                        else if (UserModel.Id_Rol == (int)UserRole.REGIONAL)
                        {

                            UserModel.Tipo_Usuario = GlobalVar.REGIONAL_OFFICE;
                        }
                        else if (UserModel.Id_Rol == (int)UserRole.MANUFACTURER)
                        {
                            UserModel.Tipo_Usuario = GlobalVar.MANUFACTURER;
                        }
                        else if (UserModel.Id_Rol == (int)UserRole.DISPOSALCENTER)
                        {
                            //UserModel.Tipo_Usuario = GlobalVar.DISPOSAL_CENTER;
                            if (this.drpbranch.SelectedIndex != -1 && this.drpbranch.SelectedIndex != 0)//branch selected to save branch user
                            {
                                UserModel.Tipo_Usuario = GlobalVar.DISPOSAL_CENTER_BRANCH;
                            }
                            else
                            {
                                UserModel.Tipo_Usuario = GlobalVar.DISPOSAL_CENTER;
                            }
                        }
                        else if (UserModel.Id_Rol == (int)UserRole.CENTRALOFFICE)
                        {
                            UserModel.Tipo_Usuario = GlobalVar.CENTRAL_OFFICE;
                        }
                        else if (UserModel.Id_Rol == (int)UserRole.ZONE)
                        {
                            UserModel.Tipo_Usuario = GlobalVar.ZONE_OFFICE;
                        }
                        else
                        {
                            //Do nothing
                        }

                        if (!US_USUARIOBll.ClassInstance.IsUserNameExist(this.txtUserName.Text))//Changed by Jerry 2011/08/16
                        {
                            //updated by mark 2012-07-12
                            if ((drpDepartment.SelectedIndex == 0 || drpDepartment.SelectedIndex == -1))
                            {
                                //if (int.Parse(drpRole.SelectedValue) == (int)UserRole.SUPPLIER)
                                //{
                                //    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "NotSelectSupplier", "alert('? Debe seleccionarse un Proveedor !')", true);
                                //}
                                //else if (int.Parse(drpRole.SelectedValue) == (int)UserRole.DISPOSALCENTER)
                                //{
                                //    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "NotSelectDisposal", "alert('? Debe seleccionarse un CAyD !')", true);
                                //}
                                //else 
                                if (int.Parse(drpRole.SelectedValue) == (int)UserRole.ZONE)
                                {
                                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "NotSelectZone", "alert('? Debe  seleccionarse una Zona !')", true);
                                }
                                else
                                {
                                    iResult = US_USUARIOBll.ClassInstance.Insert_US_USUARIO(UserModel);
                                }
                            }
                            else
                            {
                                iResult = US_USUARIOBll.ClassInstance.Insert_US_USUARIO(UserModel);
                            }
                            //end by mark     

                            if (iResult > 0)
                            {
                                //Show success message
                                Message = (string)GetGlobalResourceObject("DefaultResource", "msgSaveUserSuccess");
                                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "InsertSuccess", "confirm('" + Message + "');", true);
                                //clear user interfaces
                                CleanData();
                                //Send email
                                MailUtility.RegisterEmail(UserModel.Nombre_Usuario, UserModel.CorreoElectronico, UserModel.Contrasena);
                            }
                            //Changed by Jerry 2011/09/07
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "ChangeCursorStatus", "document.body.style.cursor = 'default';", true);
                        }
                        else
                        {
                            Message = (string)GetGlobalResourceObject("DefaultResource", "msgUserNameExist");
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "UserExist", "alert('" + Message + "');", true);
                            this.txtUserName.Focus();
                        }
                    }//end by mark

                }
                //Changed by Jerry 2011/09/07
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "ChangeCursorStatus", "document.body.style.cursor = 'default';", true);
            }
        }

        //void MailUtility_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    US_USUARIOModel UserModel = new US_USUARIOModel();
        //    US_USUARIOModel OriginalUserModel = US_USUARIOBll.ClassInstance.Get_US_USUARIOByPKID(UserID);
        //    if (OriginalUserModel != null)
        //    {
        //        UserModel.Id_Usuario = OriginalUserModel.Id_Usuario;
        //        UserModel.Nombre_Usuario = this.txtUserName.Text;
        //        UserModel.CorreoElectronico = this.txtEmail.Text;
        //        UserModel.Numero_Telefono = this.txtPhone.Text;
        //        UserModel.Contrasena = OriginalUserModel.Contrasena;
        //        UserModel.Direccion = this.txtAddress.Text;
        //        UserModel.Estatus = this.drpStatus.SelectedValue;
        //        UserModel.Id_Rol = OriginalUserModel.Id_Rol;
        //        UserModel.Id_Departamento = OriginalUserModel.Id_Departamento;
        //        UserModel.Tipo_Usuario = OriginalUserModel.Tipo_Usuario;

        //        US_USUARIOBll.ClassInstance.Update_US_USUARIO(UserModel);
        //    }
        //}
        /// <summary>
        /// Clean all controls to initial status
        /// </summary>
        private void CleanData()
        {
            this.txtUserName.Text = "";
            this.txtEmail.Text = "";
            this.txtPhone.Text = "";
            //this.txtAddress.Text = "";
            this.txtFullUserName.Text = "";
            this.drpRole.SelectedIndex = 0;
            if (this.drpDepartment.Items.Count > 0)
            {
                this.drpDepartment.SelectedIndex = 0;
            }
            if (this.drpbranch.Items.Count > 0)
            {
                this.drpbranch.SelectedIndex = 0;
            }
            //this.drpStatus.SelectedIndex = 1;
            InitStatus("");
            //Visible department drop down list
            this.department.Style.Add(HtmlTextWriterStyle.Display, "none");
            this.drpDepartment.Enabled = false;
            //this.Valdepartment.Enabled = false;
            this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
        /// <summary>
        /// Check if there are some values changed
        /// </summary>
        /// <param name="OriginalUserModel"></param>
        /// <returns></returns>
        private bool Changes(US_USUARIOModel OriginalUserModel)
        {
            bool bResult = false;
            if (!OriginalUserModel.Nombre_Usuario.Equals(this.txtUserName.Text, StringComparison.OrdinalIgnoreCase) || !OriginalUserModel.CorreoElectronico.Equals(this.txtEmail.Text, StringComparison.OrdinalIgnoreCase) ||
                !OriginalUserModel.Numero_Telefono.Equals(this.txtPhone.Text, StringComparison.OrdinalIgnoreCase) || !OriginalUserModel.Nombre_Completo_Usuario.Equals(this.txtFullUserName.Text, StringComparison.OrdinalIgnoreCase) /*!OriginalUserModel.Direccion.Equals(this.txtAddress.Text, StringComparison.OrdinalIgnoreCase) */ ||
                !OriginalUserModel.Estatus.Equals(this.drpStatus.SelectedValue))
            {
                bResult = true;
            }

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
            if (this.drpRole.SelectedIndex != -1 && this.drpRole.SelectedIndex != 0)
            {
                //Visible department drop down list
                this.department.Style.Add(HtmlTextWriterStyle.Display, "");
                this.drpDepartment.Enabled = true;
                //this.Valdepartment.Enabled = true;

                int RoleType;
                RoleType = Convert.ToInt32(this.drpRole.SelectedValue);
                //Load departments by role
                switch (RoleType)
                {
                    case (int)UserRole.REGIONAL:
                        this.lblDepartment.Text = "Regional";
                        this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        LoadRegional();
                        break;
                    case (int)UserRole.SUPPLIER:
                        LoadSupplier();
                        //InitSupplierBranches(-1);
                        this.lblDepartment.Text = "Proveedor Corporativo";
                        this.lblBranch.Text = "Proveedor Sucursal";
                        break;
                    case (int)UserRole.MANUFACTURER:
                        this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        this.lblDepartment.Text = "Fabricante";
                        break;
                    case (int)UserRole.DISPOSALCENTER:
                        LoadDisposal();
                        //InitDisposalBranches(-1);
                        this.lblDepartment.Text = "CAyD Corporativo";
                        this.lblBranch.Text = "CAyD Sucursal";
                        break;
                    case (int)UserRole.CENTRALOFFICE:
                        this.department.Style.Add(HtmlTextWriterStyle.Display, "none");
                        this.drpDepartment.Enabled = false;
                        //this.Valdepartment.Enabled = false;
                        this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        break;
                    case (int)UserRole.ZONE:
                        this.lblDepartment.Text = "Zona";
                        this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
                        LoadZona();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //Hide department drop down list
                this.department.Style.Add(HtmlTextWriterStyle.Display, "none");
                this.drpDepartment.Enabled = false;
                //this.Valdepartment.Enabled = false;
                this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
        }
        /// <summary>
        /// Init regional drop down list
        /// </summary>
        private void LoadRegional()
        {
            //load suppliers
            DataTable dtRegionals = null;
            dtRegionals = RegionalDal.ClassInstance.GetRegionals();
            if (dtRegionals != null)
            {
                this.drpDepartment.DataSource = dtRegionals;
                this.drpDepartment.DataTextField = "Dx_Nombre_Region";
                this.drpDepartment.DataValueField = "Cve_Region";
                this.drpDepartment.DataBind();
                this.drpDepartment.Items.Insert(0,new ListItem("", ""));
            }
        }
        /// <summary>
        /// Load zones
        /// </summary>
        private void LoadZona()
        {
            DataTable dtZona = null;
            int regional;
            US_USUARIOModel UserModel = null;
            if (Session["UserInfo"] != null)
            {
                UserModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (UserModel != null)
            {
                regional = UserModel.Id_Departamento;
                dtZona = CatZonaDal.ClassInstance.GetZonaWithRegional(regional);
                if (dtZona != null)
                {
                    this.drpDepartment.DataSource = dtZona;
                    this.drpDepartment.DataTextField = "Dx_Nombre_Zona";
                    this.drpDepartment.DataValueField = "Cve_Zona";
                    this.drpDepartment.DataBind();
                    this.drpDepartment.Items.Insert(0, new ListItem("", "0"));
                }
            }
        }
        /// <summary>
        /// Init supplier drop down list
        /// </summary>
        private void LoadSupplier()
        {
            //Visible department drop down list
            this.branch.Style.Add(HtmlTextWriterStyle.Display, "");
            //load suppliers
            DataTable dtSuppliers = null;
            DataTable dtSupplierBranches = null;
            int PageCount;
            int region, zone;
            US_USUARIOModel UserModel = null;
            if (Session["UserInfo"] != null)
            {
                UserModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (UserModel != null)
            {
                if ( UserModel.Id_Rol == (int)UserRole.REGIONAL )
                {
                    region = UserModel.Id_Departamento;
                    dtSuppliers = SupplierDal.ClassInstance.GetRegionSuppliers(region);
                    dtSupplierBranches = CAT_PROVEEDORDal.ClassInstance.Get_Provider("", "S", "2", region, 1, 999999, out PageCount);
                }
                else if ( UserModel.Id_Rol == (int)UserRole.ZONE )
                {
                    zone = UserModel.Id_Departamento;
                    dtSuppliers = SupplierDal.ClassInstance.GetSuppliers(zone);
                    dtSupplierBranches = CAT_PROVEEDORDal.ClassInstance.Get_Provider(zone.ToString(), "S", "2", 0, 1, 999999, out PageCount);
                }

                if (dtSuppliers != null)
                {
                    this.drpDepartment.DataSource = dtSuppliers;
                    this.drpDepartment.DataTextField = "Dx_Nombre_Comercial";
                    this.drpDepartment.DataValueField = "Id_Proveedor";
                    this.drpDepartment.DataBind();
                    this.drpDepartment.Items.Insert(0, new ListItem("", "0"));
                    this.drpDepartment.Enabled = true;
                }
                else
                {
                    this.drpDepartment.Items.Clear();
                    this.drpDepartment.Enabled = false;
                }
                if (dtSupplierBranches != null)
                {
                    this.drpbranch.DataSource = dtSupplierBranches;
                    this.drpbranch.DataTextField = "Dx_Nombre_Comercial";
                    this.drpbranch.DataValueField = "ID";
                    this.drpbranch.DataBind();
                    this.drpbranch.Items.Insert(0, new ListItem("", "0"));
                    this.drpbranch.Enabled = true;
                }
                else
                {
                    this.drpbranch.Items.Clear();
                    this.drpbranch.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Show branches related with department 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpDepartment.SelectedIndex <= 0)
                drpbranch.Enabled = true;
            else
                drpbranch.Enabled = false;
            //int RoleType;
            //RoleType = Convert.ToInt32(this.drpRole.SelectedValue);
            //switch (RoleType)
            //{
            //    case (int)UserRole.SUPPLIER:
            //        if (this.drpDepartment.SelectedIndex != -1 && this.drpDepartment.SelectedIndex != 0)
            //        {
            //            InitSupplierBranches(Convert.ToInt32(drpDepartment.SelectedValue));
            //        }
            //        else
            //        {
            //            InitSupplierBranches(-1);
            //        }
            //        break;
            //    case (int)UserRole.DISPOSALCENTER:
            //        if (this.drpDepartment.SelectedIndex != -1 && this.drpDepartment.SelectedIndex != 0)
            //        {
            //            InitDisposalBranches(Convert.ToInt32(drpDepartment.SelectedValue));
            //        }
            //        else
            //        {
            //            InitDisposalBranches(-1);
            //        }
            //        break;
            //    default:
            //        break;
            //}
            //if (this.drpDepartment.SelectedIndex != -1 && this.drpDepartment.SelectedIndex != 0)
            //{
            //    InitSupplierBranches(Convert.ToInt32(this.drpDepartment.SelectedValue));
            //}
            //Comment by Jerry 2011/08/02
            //else
            //{
            //    //Hide department drop down list
            //    this.branch.Style.Add(HtmlTextWriterStyle.Display, "none");
            //}
            //End
        }

        protected void drpbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpbranch.SelectedIndex <= 0)
                drpDepartment.Enabled = true;
            else
                drpDepartment.Enabled = false;
        }

        /// <summary>
        /// Init supplier branches
        /// </summary>
        /// <param name="SupplierID"></param>
        private void InitSupplierBranches(int SupplierID)
        {
            //Edit by Jerry 2011/08/02
            //int zone;
            US_USUARIOModel UserModel = null;
            if (Session["UserInfo"] != null)
            {
                UserModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (SupplierID <= 0)
            {
                this.drpbranch.Items.Clear();
                this.drpbranch.Items.Insert(0, new ListItem("", "0"));
            }
            else if (UserModel != null)
            {
                //zone = UserModel.Id_Departamento;
                DataTable dtSupplierBranches = null;
                //if (SupplierID < 0)
                //{
                //    dtSupplierBranches = SupplierBrancheDal.ClassInstance.GetSupplierBranch(SupplierID, zone);
                //}
                //else
                //{
                //    dtSupplierBranches = SupplierBrancheDal.ClassInstance.GetSupplierBranches(SupplierID, zone);
                //}

                dtSupplierBranches = SupplierBrancheDal.ClassInstance.GetSupplierBranchWithSupplier(SupplierID);
                if (dtSupplierBranches != null)
                {
                    this.drpbranch.DataSource = dtSupplierBranches;
                    this.drpbranch.DataTextField = "Dx_Nombre_Comercial";
                    this.drpbranch.DataValueField = "Id_Branch";
                    this.drpbranch.DataBind();
                    this.drpbranch.Items.Insert(0, new ListItem("", "0"));
                }
            }
        }
        /// <summary>
        /// Init disposal branches 
        /// </summary>
        /// <param name="DisposalID"></param>
        private void InitDisposalBranches(int DisposalID)
        {
            DataTable dtDisposalBranches = null;
            //int zone = -1;
            US_USUARIOModel UserModel = null;
            if (Session["UserInfo"] != null)
            {
                UserModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (DisposalID <= 0)
            {
                this.drpbranch.Items.Clear();
                this.drpbranch.Items.Insert(0, new ListItem("", "0"));
            }
            else if (UserModel != null)
            {
                //if (UserModel.Id_Rol == (int)UserRole.CENTRALOFFICE)
                //{
                //    zone = -1;
                //}
                //else if (UserModel.Id_Rol == (int)UserRole.ZONE)
                //{
                //    zone = UserModel.Id_Departamento;
                //}

                //dtDisposalBranches = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetDisposalBranches(zone, DisposalID);
                dtDisposalBranches = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetDisposalBranchByDisposal(DisposalID.ToString());
                if (dtDisposalBranches != null)
                {
                    this.drpbranch.DataSource = dtDisposalBranches;
                    this.drpbranch.DataTextField = "Dx_Nombre_Comercial";
                    this.drpbranch.DataValueField = "Id_Centro_Disp_Sucursal";
                    this.drpbranch.DataBind();
                    this.drpbranch.Items.Insert(0, new ListItem("", "0"));
                }
            }
        }
        /// <summary>
        /// Load disposal centers
        /// </summary>
        private void LoadDisposal()
        {
            this.branch.Style.Add(HtmlTextWriterStyle.Display, "");
            DataTable dtDisposal = null;
            DataTable disposalCenters = null;
            int PageCount;
            int region, zone;
            US_USUARIOModel UserModel = null;
            if (Session["UserInfo"] != null)
            {
                UserModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (UserModel != null)
            {
                //if (UserModel.Id_Rol == (int)UserRole.CENTRALOFFICE)
                //{
                //    dtDisposal = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposals();
                //}
                //else 
                if (UserModel.Id_Rol == (int)UserRole.ZONE)
                {
                    zone = UserModel.Id_Departamento;
                    dtDisposal = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposals(zone);
                    disposalCenters = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterAndBranchWithZoneAndStatus(0, zone.ToString(), "S", 2, "", 1, 999999, out PageCount);
                }
                else if ( UserModel.Id_Rol == (int)UserRole.REGIONAL )
                {
                    region = UserModel.Id_Departamento;
                    dtDisposal = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalsByRegional(region);
                    disposalCenters = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterAndBranchWithZoneAndStatus(region, "", "S", 2, "", 1, 999999, out PageCount);
                }

                if (dtDisposal != null)
                {
                    this.drpDepartment.DataSource = dtDisposal;
                    this.drpDepartment.DataTextField = "Dx_Nombre_Comercial";
                    this.drpDepartment.DataValueField = "Id_Centro_Disp";
                    this.drpDepartment.DataBind();
                    this.drpDepartment.Items.Insert(0, new ListItem("", "0"));
                    this.drpDepartment.Enabled = true;
                }
                else
                {
                    this.drpDepartment.Items.Clear();
                    this.drpDepartment.Enabled = false;
                }
                if (disposalCenters != null)
                {
                    this.drpbranch.DataSource = disposalCenters;
                    this.drpbranch.DataTextField = "Dx_Nombre_Comercial";
                    this.drpbranch.DataValueField = "Id_Centro_Disp";
                    this.drpbranch.DataBind();
                    this.drpbranch.Items.Insert(0, new ListItem("", "0"));
                    this.drpbranch.Enabled = true;
                }
                else
                {
                    this.drpbranch.Items.Clear();
                    this.drpbranch.Enabled = false;
                }
            }
        }
    }
}
