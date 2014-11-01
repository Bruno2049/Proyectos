using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.UserModule
{
    public partial class UsersManager : Page
    {
        /// <summary>
        /// init data when load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (null == Session["UserInfo"])
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }
            //Init date control
           
            literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //Load users by role
            InitDefaultData();
        }
        /// <summary>
        /// Init grid view data by role
        /// </summary>
        private void InitDefaultData()
        {
            var userModel = (US_USUARIOModel)Session["UserInfo"];
            if (userModel == null) return;
            var roleType = userModel.Id_Rol;
            DataTable dtUsers = null;

            int pageCount;
            switch (roleType)
            {
                case (int)UserRole.CENTRALOFFICE:
                    DisableFilterControl("C");
                    LoadRoleDropDownList();
                    LoadUserDropDownList("C", "", 0, 0);
                    dtUsers = LoadUsersByCentral(out pageCount);
                    break;
                case (int)UserRole.REGIONAL:
                    DisableFilterControl("R");
                    LoadDepartmentDropDownListWithZone(userModel.Id_Departamento);
                    LoadUserDropDownList("R", "", userModel.Id_Departamento, 0);
                    dtUsers = LoadUsersByRegional(out pageCount);
                    break;
                case (int)UserRole.ZONE:
                    DisableFilterControl("Z");
                    LoadUserDropDownList("Z", "", userModel.Id_Departamento, 0);
                    dtUsers = LoadUsersByZone(out pageCount);
                    break;
                default:
                    pageCount = 0;
                    break;
            }
            //Bind data to grid view control
            if (dtUsers == null) return;
            if (dtUsers.Rows.Count == 0)
            {
                dtUsers.Rows.Add(dtUsers.NewRow());
            }
            AspNetPager.RecordCount = pageCount;
            gvUserList.DataSource = dtUsers;
            gvUserList.DataBind();
        }
        /// <summary>
        /// Load users for central office
        /// </summary>
        /// <returns></returns>
        private DataTable LoadUsersByCentral(out int pageCount)
        {
            var listRoles = new List<string> {"R_O", "S", "D_C", "M", "C_O", "S_B", "D_C_B", "Z_O"};
            //updated by tina 2012-07-11

            var selectedRole = "";
            var selectedDepartment = 0;
            var userId = 0;

            if (ddlRole.SelectedIndex > 0)
            {
                switch (int.Parse(ddlRole.SelectedValue))
                {
                    case (int) UserRole.CENTRALOFFICE:
                        selectedRole = "C_O";
                        break;
                    case (int) UserRole.REGIONAL:
                        selectedRole = "R_O";
                        selectedDepartment = int.Parse(ddlDepartment.SelectedValue); //added by tina 2012-07-17
                        break;
                    case (int) UserRole.ZONE:
                        selectedRole = "Z_O";
                        selectedDepartment = int.Parse(ddlDepartment.SelectedValue); //added by tina 2012-07-17
                        break;
                    case (int) UserRole.SUPPLIER:
                        selectedRole = "S_ALL";
                        if (ddlDepartment.Visible)
                        {
                            if (ddlDepartment.SelectedIndex > 0)
                            {
                                if (ddlDepartment.SelectedValue.Contains("(SUPPLIER)"))
                                {
                                    selectedRole = "S";
                                }
                                else if (ddlDepartment.SelectedValue.Contains("(BRANCH)"))
                                {
                                    selectedRole = "S_B";
                                }
                                int.TryParse(
                                    ddlDepartment.SelectedValue.Substring(0, ddlDepartment.SelectedValue.IndexOf('-')),
                                    out selectedDepartment);
                            }
                        }
                        break;
                    case (int) UserRole.DISPOSALCENTER:
                        selectedRole = "D_ALL";
                        if (ddlDepartment.Visible)
                        {
                            if (ddlDepartment.SelectedIndex > 0)
                            {
                                if (ddlDepartment.SelectedValue.Contains("(MATRIZ)"))
                                {
                                    selectedRole = "D_C";
                                }
                                else if (ddlDepartment.SelectedValue.Contains("(SUCURSAL)"))
                                {
                                    selectedRole = "D_C_B";
                                }
                                int.TryParse(
                                    ddlDepartment.SelectedValue.Substring(0, ddlDepartment.SelectedValue.IndexOf('-')),
                                    out selectedDepartment);
                            }
                        }
                        break;
                    case (int) UserRole.MANUFACTURER:
                        selectedRole = "M";
                        break;
                }
            }


            if (ddlUserName.SelectedIndex > -1)
            {
                int.TryParse(ddlUserName.SelectedValue, out userId);
            }
            var dtResult = US_USUARIODal.ClassInstance.GetUsersByUserType(listRoles, selectedRole, selectedDepartment, userId, "Nombre_Usuario ASC", AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out pageCount);
            //end            

            return dtResult;
        }
        /// <summary>
        /// Load users for regional office
        /// </summary>
        /// <returns></returns>
        private DataTable LoadUsersByRegional(out int pageCount)
        {
            var zone = 0;//added by tina 2012-07-11
            var userId = 0;//added by tina 2012-07-11
            US_USUARIOModel userModel = null;
            if (Session["UserInfo"] != null)
            {
                userModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (userModel != null)
            {
                var regional = userModel.Id_Departamento;
                //updated by tina 2012-07-11
                if (ddlDepartment.SelectedIndex > -1)
                {
                    int.TryParse(ddlDepartment.SelectedValue, out zone);
                }
                if (ddlUserName.SelectedIndex > -1)
                {
                    int.TryParse(ddlUserName.SelectedValue, out userId);
                }
                var dtResult = US_USUARIODal.ClassInstance.GetUsersWithRegionalRole(regional, zone, userId, "Nombre_Usuario ASC", AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out pageCount);
                //end                

                return dtResult;
            }
            pageCount = 0;
            return null;
        }
        /// <summary>
        /// Load users with zone office
        /// </summary>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        private DataTable LoadUsersByZone(out int pageCount)
        {
            var userId = 0;//added by tina 2012-07-11
            US_USUARIOModel userModel = null;
            if (Session["UserInfo"] != null)
            {
                userModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (userModel != null)
            {
                int zone = userModel.Id_Departamento;
                //updated by tina 2012-07-11
                if (ddlUserName.SelectedIndex > -1)
                {
                    int.TryParse(ddlUserName.SelectedValue, out userId);
                }
                var dtResult = US_USUARIODal.ClassInstance.GetUsersWithZoneRole(zone, userId, "Nombre_Usuario ASC", AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out pageCount);
                //end

                return dtResult;
            }
            pageCount = 0;
            return null;
        }
        //added by tina 2012-07-10
        private void LoadRoleDropDownList()
        {
            var roleList = US_ROLBll.ClassInstance.Get_AllUS_ROL();
            ddlRole.DataSource = roleList;
            ddlRole.DataTextField = "Nombre_Rol";
            ddlRole.DataValueField = "Id_Rol";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("", "0"));
        }
        private void LoadDepartmentDropDownListWithRegional()
        {
            var dtRegional = RegionalDal.ClassInstance.GetRegionals();
            if (dtRegional == null) return;
            ddlDepartment.DataSource = dtRegional;
            ddlDepartment.DataTextField = "Dx_Nombre_Region";
            ddlDepartment.DataValueField = "Cve_Region";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("", "0"));
        }
        private void LoadDepartmentDropDownListWithZone(int regional)
        {
            var dtZone = regional > 0 ? CatZonaDal.ClassInstance.GetZonaWithRegional(regional) : CatZonaDal.ClassInstance.GetAllZone();

            if (dtZone == null) return;
            ddlDepartment.DataSource = dtZone;
            ddlDepartment.DataTextField = "Dx_Nombre_Zona";
            ddlDepartment.DataValueField = "Cve_Zona";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("", "0"));
        }

        private void LoadDepartmentDropDownListWithSupplier()
        {
            var dtCommercialName = CAT_PROVEEDORDal.ClassInstance.GetSupplierAndBranchCompanyName();
            if (dtCommercialName == null) return;
            ddlDepartment.DataSource = dtCommercialName;
            ddlDepartment.DataTextField = "Dx_Nombre_Comercial";
            ddlDepartment.DataValueField = "Id_Proveedor";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("", "0"));
        }

        private void LoadDepartmentDropDownListWithDisposal()
        {
            var dtCommercialName = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalAndBranchCompanyName();
            if (dtCommercialName == null) return;
            ddlDepartment.DataSource = dtCommercialName;
            ddlDepartment.DataTextField = "Dx_Nombre_Comercial";
            ddlDepartment.DataValueField = "Id_Centro_Disp";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("", "0"));
        }

        private void LoadUserDropDownList(string logUserRole, string selectedRole, int logUserDepartment, int selectedDepartment)
        {
            var dtUsers = US_USUARIODal.ClassInstance.GetUsersByUserTypeAndDepartment(logUserRole, selectedRole, logUserDepartment, selectedDepartment);
            if (dtUsers == null) return;
            ddlUserName.DataSource = dtUsers;
            ddlUserName.DataTextField = "Nombre_Usuario";
            ddlUserName.DataValueField = "Id_Usuario";
            ddlUserName.DataBind();
            ddlUserName.Items.Insert(0, new ListItem("", "0"));
        }

        private void DisableFilterControl(string logUserRole)
        {
            lblUserName.Visible = true;
            ddlUserName.Visible = true;
            switch (logUserRole)
            {
                case "C":
                    lblRole.Visible = true;
                    ddlRole.Visible = true;
                    lblDepartment.Visible = false;
                    ddlDepartment.Visible = false;
                    break;
                case "R":
                    lblRole.Visible = false;
                    ddlRole.Visible = false;
                    lblDepartment.Visible = true;
                    ddlDepartment.Visible = true;
                    lblDepartment.Text = @"Zona";
                    break;
                case "Z":
                    lblRole.Visible = false;
                    ddlRole.Visible = false;
                    lblDepartment.Visible = false;
                    ddlDepartment.Visible = false;
                    break;
            }
        }

        private void RefreshGridView()
        {
            var userModel = (US_USUARIOModel)Session["UserInfo"];
            if (userModel == null) return;
            var roleType = userModel.Id_Rol;
            DataTable dtUsers = null;

            int pageCount;
            switch (roleType)
            {
                case (int)UserRole.CENTRALOFFICE:
                    dtUsers = LoadUsersByCentral(out pageCount);
                    break;
                case (int)UserRole.REGIONAL:
                    dtUsers = LoadUsersByRegional(out pageCount);
                    break;
                case (int)UserRole.ZONE:
                    dtUsers = LoadUsersByZone(out pageCount);
                    break;
                default:
                    pageCount = 0;
                    break;
            }
            //Bind data to grid view control
            if (dtUsers == null) return;
            if (dtUsers.Rows.Count == 0)
            {
                dtUsers.Rows.Add(dtUsers.NewRow());
            }
            AspNetPager.RecordCount = pageCount;
            gvUserList.DataSource = dtUsers;
            gvUserList.DataBind();
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            if (ddlRole.SelectedIndex <= -1) return;
            switch (int.Parse(ddlRole.SelectedValue))
            {
                case 0:
                    lblDepartment.Visible = false;
                    ddlDepartment.Visible = false;
                    LoadUserDropDownList("C", "", 0, 0);
                    break;
                case (int)UserRole.CENTRALOFFICE:
                    lblDepartment.Visible = false;
                    ddlDepartment.Visible = false;
                    LoadUserDropDownList("C", "C_O", 0, 0);
                    break;
                case (int)UserRole.REGIONAL:
                    lblDepartment.Visible = true;
                    ddlDepartment.Visible = true;
                    lblDepartment.Text = @"Región";
                    LoadDepartmentDropDownListWithRegional();
                    LoadUserDropDownList("C", "R_O", 0, 0);
                    break;
                case (int)UserRole.ZONE:
                    lblDepartment.Visible = true;
                    ddlDepartment.Visible = true;
                    lblDepartment.Text = @"Zona";
                    LoadDepartmentDropDownListWithZone(0);
                    LoadUserDropDownList("C", "Z_O", 0, 0);
                    break;
                case (int)UserRole.SUPPLIER:
                    lblDepartment.Visible = true;
                    ddlDepartment.Visible = true;
                    lblDepartment.Text = @"Empresa";
                    LoadDepartmentDropDownListWithSupplier();
                    LoadUserDropDownList("C", "S_ALL", 0, 0);
                    break;
                case (int)UserRole.DISPOSALCENTER:
                    lblDepartment.Visible = true;
                    ddlDepartment.Visible = true;
                    lblDepartment.Text = @"Empresa";
                    LoadDepartmentDropDownListWithDisposal();
                    LoadUserDropDownList("C", "D_ALL", 0, 0);
                    break;
                case (int)UserRole.MANUFACTURER:
                    //updated by tina 2012-07-17
                    lblDepartment.Visible = false;
                    ddlDepartment.Visible = false;
                    //end
                    LoadUserDropDownList("C", "M", 0, 0);
                    break;
            }
            RefreshGridView();
            Session["CurrentRoleUserMananger"] = ddlRole.SelectedIndex;
            Session["CurrentDepartmentUserMananger"] = 0;
            Session["CurrentUserNameUserMananger"] = 0;
            AspNetPager.GoToPage(1);
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            var userModel = (US_USUARIOModel)Session["UserInfo"];
            if (userModel == null) return;
            if (ddlDepartment.SelectedIndex > -1)
            {
                var selectedDepartment = 0;
                //updated by tina 2012-07-17
                if (ddlDepartment.SelectedIndex > 0)
                {
                    if (userModel.Id_Rol == (int)UserRole.CENTRALOFFICE)
                    {                           
                        if (int.Parse(ddlRole.SelectedValue) == (int)UserRole.SUPPLIER || int.Parse(ddlRole.SelectedValue) == (int)UserRole.DISPOSALCENTER)
                        {
                            int.TryParse(ddlDepartment.SelectedValue.Substring(0, ddlDepartment.SelectedValue.IndexOf('-')), out selectedDepartment);
                        }
                        else
                        {
                            selectedDepartment = int.Parse(ddlDepartment.SelectedValue);
                        }
                    }
                    else
                    {
                        selectedDepartment = int.Parse(ddlDepartment.SelectedValue);
                    }                   
                }                        
                //end
                if (userModel.Id_Rol == (int)UserRole.CENTRALOFFICE)
                {
                    string selectedRole;
                    switch (int.Parse(ddlRole.SelectedValue))
                    {
                        case (int)UserRole.REGIONAL:
                            LoadUserDropDownList("C", "R_O", 0, selectedDepartment);
                            break;
                        case (int)UserRole.ZONE:
                            LoadUserDropDownList("C", "Z_O", 0, selectedDepartment);
                            break;
                        case (int)UserRole.SUPPLIER:
                            selectedRole = "S_ALL";
                            if (ddlDepartment.SelectedIndex > 0)
                            {
                                if (ddlDepartment.SelectedValue.Contains("(SUPPLIER)"))
                                {
                                    selectedRole = "S";
                                }
                                else if (ddlDepartment.SelectedValue.Contains("(BRANCH)"))
                                {
                                    selectedRole = "S_B";
                                }
                            }
                            LoadUserDropDownList("C", selectedRole, 0, selectedDepartment);
                            break;
                        case (int)UserRole.DISPOSALCENTER:
                            selectedRole = "D_ALL";
                            if (ddlDepartment.SelectedIndex > 0)
                            {
                                if (ddlDepartment.SelectedValue.Contains("(MATRIZ)"))
                                {
                                    selectedRole = "D_C";
                                }
                                else if (ddlDepartment.SelectedValue.Contains("(SUCURSAL)"))
                                {
                                    selectedRole = "D_C_B";
                                }
                            }
                            LoadUserDropDownList("C", selectedRole, 0, selectedDepartment);
                            break;
                    }
                }
                else
                {
                    LoadUserDropDownList("R", "", userModel.Id_Departamento, selectedDepartment);
                }
            }
            RefreshGridView();
            Session["CurrentDepartmentUserMananger"] = ddlDepartment.SelectedIndex;
            Session["CurrentUserNameUserMananger"] = 0;//added by tina 2012-07-18
            AspNetPager.GoToPage(1);
        }

        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            RefreshGridView();
            Session["CurrentUserNameUserMananger"] = ddlUserName.SelectedIndex;
            AspNetPager.GoToPage(1);
        }
        //end
        /// <summary>
        /// refresh data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            //updated by tina 2012-07-11
            if (IsPostBack)
            {
                RefreshGridView();
            }
            //end
        }
        //added by tina 2012-07-11
        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            if (!IsPostBack) return;
            //setup filter conditions for data refreshing
            if (ddlRole.Visible)
            {
                ddlRole.SelectedIndex = Session["CurrentRoleUserMananger"] != null ? (int)Session["CurrentRoleUserMananger"] : 0;
            }
            if (ddlDepartment.Visible)
            {
                ddlDepartment.SelectedIndex = Session["CurrentDepartmentUserMananger"] != null ? (int)Session["CurrentDepartmentUserMananger"] : 0;
            }
            if (ddlUserName.Visible)
            {
                ddlUserName.SelectedIndex = Session["CurrentUserNameUserMananger"] != null ? (int)Session["CurrentUserNameUserMananger"] : 0;
            }
        }
        //end
        /// <summary>
        /// Hide the edit button when row data bound for some status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvUserList.Rows)
            {
                var dataKey = gvUserList.DataKeys[row.RowIndex];
                if (dataKey != null && (dataKey.Value != DBNull.Value && Convert.ToInt32(dataKey.Value) > 0))
                {
                    if (row.RowType != DataControlRowType.DataRow) continue;
                    if (row.Cells[5].Text.Equals("A", StringComparison.OrdinalIgnoreCase))
                    {
                        row.FindControl("linkButtonActivate").Visible = false;
                        row.FindControl("linkButtonDelete").Visible = false;
                        //Change Status From Status Code to Status Name
                        row.Cells[5].Text = @"Activo";
                    }
                    else if (row.Cells[5].Text.Equals("I", StringComparison.OrdinalIgnoreCase))
                    {
                        //Change Status From Status Code to Status Name
                        row.FindControl("linkButtonDelete").Visible = false;
                        row.Cells[5].Text = @"Inactivo";
                    }
                    else if (row.Cells[5].Text.Equals("P", StringComparison.OrdinalIgnoreCase))
                    {
                        //Change Status From Status Code to Status Name
                        row.Cells[5].Text = @"Pendiente";
                    }
                    else // "C"
                    {
                        row.FindControl("linkButtonEdit").Visible = false;
                        row.FindControl("linkButtonActivate").Visible = false;
                        row.FindControl("linkButtonDelete").Visible = false;
                        //Change Status From Status Code to Status Name
                        row.Cells[5].Text = @"Cancelado";
                    }
                }
                else
                {
                    row.FindControl("linkButtonEdit").Visible  = false;
                    row.FindControl("linkButtonActivate").Visible = false;
                    row.FindControl("linkButtonDelete").Visible = false;
                }
            }
        }
        /// <summary>
        /// Show edit form when command button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var rowIndex = Convert.ToInt32(e.CommandArgument);
            var dataKey = gvUserList.DataKeys[rowIndex];
            if (dataKey == null) return;

            var userId = dataKey.Value.ToString();
            
            var originalUserModel = US_USUARIOBll.ClassInstance.Get_US_USUARIOByPKID(userId);

            string message;
            int iResult;
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("UserEdition.aspx?UserID="+Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(userId)).Replace("+","%2B"));
                    break;
                case "Borrar":
                    iResult = US_USUARIODal.ClassInstance.Delete_US_USUARIO(userId);
                    if (iResult > 0)
                    {
                        /*INSERTAR EVENTO BORRAR DEL RIPO PROCESO USUARIOS EN LOG*/
                        var datos = Insertlog.GetDatosObjeto(originalUserModel);
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "USUARIOS", "BORRAR", userId.ToString(CultureInfo.InvariantCulture), "MOtivo??", "", datos,
                            "");
                        message = (string) GetGlobalResourceObject("DefaultResource", "msgDeleteUserSuccess");
                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "DeleteSuccessfully",
                            "confirm('" + message + "');", true);
                        //Refresh grid data
                        InitDefaultData();
                    }
                    break;
                default:
                {                
                    iResult = US_USUARIODal.ClassInstance.ActivateUser(userId);
                    //add by coco 2011-08-12
                    var userMod = US_USUARIODal.ClassInstance.Get_US_USUARIOByPKID(userId);
                    MailUtility.StatusChangeEmail(userMod.Nombre_Usuario, userMod.CorreoElectronico, "activo");
                    //end add
                    if (iResult > 0)
                    {
                        /*INSERTAR EVENTO REACTIVAR DE USUARIO EN LOG*/
                        var cambio = Insertlog.GetCambiosDatos(originalUserModel, userMod);
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "USUARIOS", "REACTIVAR", userId.ToString(CultureInfo.InvariantCulture), "Motivo??", "",
                            cambio[0], cambio[1]);

                        message = (string) GetGlobalResourceObject("DefaultResource", "msgActivateUserSuccess");
                        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "AvtivateSuccessfully",
                            "confirm('" + message + "');", true);
                        //Refresh grid data
                        InitDefaultData();
                    }
                }
                    break;
            }
        }
        /// <summary>
        /// Assign e.CommandArgument when row created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var linkButton = (LinkButton)e.Row.FindControl("linkButtonEdit");
            if (linkButton != null)
            {
                linkButton.CommandArgument = e.Row.RowIndex.ToString(CultureInfo.InvariantCulture);
            }

            var linkDelete = (LinkButton)e.Row.FindControl("linkButtonDelete");
            if (linkDelete != null)
            {
                linkDelete.CommandArgument = e.Row.RowIndex.ToString(CultureInfo.InvariantCulture);
            }

            var linkActivate = (LinkButton)e.Row.FindControl("linkButtonActivate");
            if (linkActivate != null)
            {
                linkActivate.CommandArgument = e.Row.RowIndex.ToString(CultureInfo.InvariantCulture);
            }
        }
        /// <summary>
        /// Show new user page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserEdition.aspx");
        }
    }
}
