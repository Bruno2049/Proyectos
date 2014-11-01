using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;

namespace PAEEEM
{
    public partial class UsersManager : System.Web.UI.Page
    {
        /// <summary>
        /// init data when load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                //Init date control
                this.literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                //Load users by role
                InitDefaultData();
            }
        }
        /// <summary>
        /// Init grid view data by role
        /// </summary>
        private void InitDefaultData()
        {
            int PageCount;

            US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];
            if (UserModel != null)
            {
                int RoleType = UserModel.Id_Rol;
                DataTable dtUsers = null;

                if (RoleType == (int)UserRole.CENTRALOFFICE)
                {
                    //added by tina 2012-07-11 
                    DisableFilterControl("C");
                    LoadRoleDropDownList();
                    LoadUserDropDownList("C", "", 0, 0);
                    //end
                    dtUsers = LoadUsersByCentral(out PageCount);
                }
                else if (RoleType == (int)UserRole.REGIONAL)
                {
                    //added by tina 2012-07-11
                    DisableFilterControl("R");
                    LoadDepartmentDropDownListWithZone(UserModel.Id_Departamento);
                    LoadUserDropDownList("R", "", UserModel.Id_Departamento, 0);
                    //end
                    dtUsers = LoadUsersByRegional(out PageCount);
                }
                //Add by Jerry 2011/08/02 used for zone office to add users
                else if (RoleType == (int)UserRole.ZONE)
                {
                    //added by tina 2012-07-11
                    DisableFilterControl("Z");
                    LoadUserDropDownList("Z", "", UserModel.Id_Departamento, 0);
                    //end
                    dtUsers = LoadUsersByZone(out PageCount);
                }
                //End
                else
                {
                    //Do nothing for other role user; 
                    PageCount = 0;
                }
                //Bind data to grid view control
                if (dtUsers != null)
                {
                    if (dtUsers.Rows.Count == 0)
                    {
                        dtUsers.Rows.Add(dtUsers.NewRow());
                    }
                    this.AspNetPager.RecordCount = PageCount;
                    this.gvUserList.DataSource = dtUsers;
                    this.gvUserList.DataBind();
                }
            }
        }
        /// <summary>
        /// Load users for central office
        /// </summary>
        /// <returns></returns>
        private DataTable LoadUsersByCentral(out int PageCount)
        {
            DataTable dtResult = null;
            List<string> ListRoles = new List<string>();
            ListRoles.Add("R_O");//regional office user
            ListRoles.Add("S");//supplier
            ListRoles.Add("D_C");//disposal center user
            ListRoles.Add("M");//manufacturer user
            ListRoles.Add("C_O");//central office
            ListRoles.Add("S_B");
            ListRoles.Add("D_C_B");
            //updated by tina 2012-07-11
            ListRoles.Add("Z_O");

            string selectedRole = "";
            int selectedDepartment = 0;
            int userID = 0;

            if (ddlRole.SelectedIndex > 0)
            {
                switch (int.Parse(ddlRole.SelectedValue))
                {
                    case (int)UserRole.CENTRALOFFICE:
                        selectedRole = "C_O";
                        break;
                    case (int)UserRole.REGIONAL:
                        selectedRole = "R_O";
                        selectedDepartment = int.Parse(ddlDepartment.SelectedValue);//added by tina 2012-07-17
                        break;
                    case (int)UserRole.ZONE:
                        selectedRole = "Z_O";
                        selectedDepartment = int.Parse(ddlDepartment.SelectedValue);//added by tina 2012-07-17
                        break;
                    case (int)UserRole.SUPPLIER:
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
                                int.TryParse(ddlDepartment.SelectedValue.Substring(0, ddlDepartment.SelectedValue.IndexOf('-')), out selectedDepartment);
                            }                           
                        }
                        break;
                    case (int)UserRole.DISPOSALCENTER:
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
                                int.TryParse(ddlDepartment.SelectedValue.Substring(0, ddlDepartment.SelectedValue.IndexOf('-')), out selectedDepartment);
                            }                            
                        }
                        break;
                    case (int)UserRole.MANUFACTURER:
                        selectedRole = "M";
                        break;
                }
            }


            if (ddlUserName.SelectedIndex > -1)
            {
                int.TryParse(ddlUserName.SelectedValue, out userID);
            }
            dtResult = US_USUARIODal.ClassInstance.GetUsersByUserType(ListRoles, selectedRole, selectedDepartment, userID, "Nombre_Usuario ASC", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
            //end            

            return dtResult;
        }
        /// <summary>
        /// Load users for regional office
        /// </summary>
        /// <returns></returns>
        private DataTable LoadUsersByRegional(out int PageCount)
        {
            DataTable dtResult = null;
            int regional;
            int zone = 0;//added by tina 2012-07-11
            int userID = 0;//added by tina 2012-07-11
            US_USUARIOModel UserModel = null;
            if (Session["UserInfo"] != null)
            {
                UserModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (UserModel != null)
            {
                regional = UserModel.Id_Departamento;
                //updated by tina 2012-07-11
                if (ddlDepartment.SelectedIndex > -1)
                {
                    int.TryParse(ddlDepartment.SelectedValue, out zone);
                }
                if (ddlUserName.SelectedIndex > -1)
                {
                    int.TryParse(ddlUserName.SelectedValue, out userID);
                }
                dtResult = US_USUARIODal.ClassInstance.GetUsersWithRegionalRole(regional, zone, userID, "Nombre_Usuario ASC", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
                //end                

                return dtResult;
            }
            PageCount = 0;
            return null;
        }
        /// <summary>
        /// Load users with zone office
        /// </summary>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        private DataTable LoadUsersByZone(out int PageCount)
        {
            DataTable dtResult = null;
            int zone;
            int userID = 0;//added by tina 2012-07-11
            US_USUARIOModel UserModel = null;
            if (Session["UserInfo"] != null)
            {
                UserModel = Session["UserInfo"] as US_USUARIOModel;
            }
            if (UserModel != null)
            {
                zone = UserModel.Id_Departamento;
                //updated by tina 2012-07-11
                if (ddlUserName.SelectedIndex > -1)
                {
                    int.TryParse(ddlUserName.SelectedValue, out userID);
                }
                dtResult = US_USUARIODal.ClassInstance.GetUsersWithZoneRole(zone, userID, "Nombre_Usuario ASC", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
                //end

                return dtResult;
            }
            PageCount = 0;
            return null;
        }
        //added by tina 2012-07-10
        private void LoadRoleDropDownList()
        {
            List<US_ROLModel> roleList = US_ROLBll.ClassInstance.Get_AllUS_ROL();
            ddlRole.DataSource = roleList;
            ddlRole.DataTextField = "Nombre_Rol";
            ddlRole.DataValueField = "Id_Rol";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("", "0"));
        }
        private void LoadDepartmentDropDownListWithRegional()
        {
            DataTable dtRegional = RegionalDal.ClassInstance.GetRegionals();
            if (dtRegional != null)
            {
                ddlDepartment.DataSource = dtRegional;
                ddlDepartment.DataTextField = "Dx_Nombre_Region";
                ddlDepartment.DataValueField = "Cve_Region";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("", "0"));
            }
        }
        private void LoadDepartmentDropDownListWithZone(int regional)
        {
            DataTable dtZone = null;
            if (regional > 0)
            {
                dtZone = CatZonaDal.ClassInstance.GetZonaWithRegional(regional);
            }
            else
            {
                dtZone = CatZonaDal.ClassInstance.GetAllZone();
            }

            if (dtZone != null)
            {
                ddlDepartment.DataSource = dtZone;
                ddlDepartment.DataTextField = "Dx_Nombre_Zona";
                ddlDepartment.DataValueField = "Cve_Zona";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("", "0"));
            }
        }
        private void LoadDepartmentDropDownListWithSupplier()
        {
            DataTable dtCommercialName = null;
            dtCommercialName = CAT_PROVEEDORDal.ClassInstance.GetSupplierAndBranchCompanyName();
            if (dtCommercialName != null)
            {
                ddlDepartment.DataSource = dtCommercialName;
                ddlDepartment.DataTextField = "Dx_Nombre_Comercial";
                ddlDepartment.DataValueField = "Id_Proveedor";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("", "0"));
            }
        }
        private void LoadDepartmentDropDownListWithDisposal()
        {
            DataTable dtCommercialName = null;
            dtCommercialName = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalAndBranchCompanyName();
            if (dtCommercialName != null)
            {
                ddlDepartment.DataSource = dtCommercialName;
                ddlDepartment.DataTextField = "Dx_Nombre_Comercial";
                ddlDepartment.DataValueField = "Id_Centro_Disp";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("", "0"));
            }
        }
        private void LoadUserDropDownList(string logUserRole, string selectedRole, int logUserDepartment, int selectedDepartment)
        {
            DataTable dtUsers = US_USUARIODal.ClassInstance.GetUsersByUserTypeAndDepartment(logUserRole, selectedRole, logUserDepartment, selectedDepartment);
            if (dtUsers != null)
            {
                ddlUserName.DataSource = dtUsers;
                ddlUserName.DataTextField = "Nombre_Usuario";
                ddlUserName.DataValueField = "Id_Usuario";
                ddlUserName.DataBind();
                ddlUserName.Items.Insert(0, new ListItem("", "0"));
            }
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
                    lblDepartment.Text = "Zona";
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
            int PageCount;

            US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];
            if (UserModel != null)
            {
                int RoleType = UserModel.Id_Rol;
                DataTable dtUsers = null;

                if (RoleType == (int)UserRole.CENTRALOFFICE)
                {
                    dtUsers = LoadUsersByCentral(out PageCount);
                }
                else if (RoleType == (int)UserRole.REGIONAL)
                {
                    dtUsers = LoadUsersByRegional(out PageCount);
                }
                else if (RoleType == (int)UserRole.ZONE)
                {
                    dtUsers = LoadUsersByZone(out PageCount);
                }
                else
                {
                    //Do nothing for other role user; 
                    PageCount = 0;
                }
                //Bind data to grid view control
                if (dtUsers != null)
                {
                    if (dtUsers.Rows.Count == 0)
                    {
                        dtUsers.Rows.Add(dtUsers.NewRow());
                    }
                    this.AspNetPager.RecordCount = PageCount;
                    this.gvUserList.DataSource = dtUsers;
                    this.gvUserList.DataBind();
                }
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ddlRole.SelectedIndex > -1)
                {
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
                            lblDepartment.Text = "Región";
                            LoadDepartmentDropDownListWithRegional();
                            LoadUserDropDownList("C", "R_O", 0, 0);
                            break;
                        case (int)UserRole.ZONE:
                            lblDepartment.Visible = true;
                            ddlDepartment.Visible = true;
                            lblDepartment.Text = "Zona";
                            LoadDepartmentDropDownListWithZone(0);
                            LoadUserDropDownList("C", "Z_O", 0, 0);
                            break;
                        case (int)UserRole.SUPPLIER:
                            lblDepartment.Visible = true;
                            ddlDepartment.Visible = true;
                            lblDepartment.Text = "Empresa";
                            LoadDepartmentDropDownListWithSupplier();
                            LoadUserDropDownList("C", "S_ALL", 0, 0);
                            break;
                        case (int)UserRole.DISPOSALCENTER:
                            lblDepartment.Visible = true;
                            ddlDepartment.Visible = true;
                            lblDepartment.Text = "Empresa";
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
                    Session["CurrentRoleUserMananger"] = this.ddlRole.SelectedIndex;
                    Session["CurrentDepartmentUserMananger"] = 0;
                    Session["CurrentUserNameUserMananger"] = 0;
                    AspNetPager.GoToPage(1);
                }
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];
                if (UserModel != null)
                {
                    if (ddlDepartment.SelectedIndex > -1)
                    {
                        string selectedRole = "";
                        int selectedDepartment = 0;

                        //updated by tina 2012-07-17
                        if (ddlDepartment.SelectedIndex > 0)
                        {
                            if (UserModel.Id_Rol == (int)UserRole.CENTRALOFFICE)
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
                        if (UserModel.Id_Rol == (int)UserRole.CENTRALOFFICE)
                        {
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
                            LoadUserDropDownList("R", "", UserModel.Id_Departamento, selectedDepartment);
                        }
                    }
                    RefreshGridView();
                    Session["CurrentDepartmentUserMananger"] = this.ddlDepartment.SelectedIndex;
                    Session["CurrentUserNameUserMananger"] = 0;//added by tina 2012-07-18
                    AspNetPager.GoToPage(1);
                }
            }
        }

        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                RefreshGridView();
                Session["CurrentUserNameUserMananger"] = this.ddlUserName.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
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
            if (IsPostBack)
            {
                //setup filter conditions for data refreshing
                if (this.ddlRole.Visible)
                {
                    this.ddlRole.SelectedIndex = Session["CurrentRoleUserMananger"] != null ? (int)Session["CurrentRoleUserMananger"] : 0;
                }
                if (this.ddlDepartment.Visible)
                {
                    this.ddlDepartment.SelectedIndex = Session["CurrentDepartmentUserMananger"] != null ? (int)Session["CurrentDepartmentUserMananger"] : 0;
                }
                if (this.ddlUserName.Visible)
                {
                    this.ddlUserName.SelectedIndex = Session["CurrentUserNameUserMananger"] != null ? (int)Session["CurrentUserNameUserMananger"] : 0;
                }
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
            foreach (GridViewRow Row in this.gvUserList.Rows)
            {
                if (this.gvUserList.DataKeys[Row.RowIndex].Value != DBNull.Value && Convert.ToInt32(this.gvUserList.DataKeys[Row.RowIndex].Value) > 0)
                {
                    if (Row.RowType == DataControlRowType.DataRow)
                    {
                        if (Row.Cells[5].Text.Equals("A", StringComparison.OrdinalIgnoreCase))
                        {
                            //((LinkButton)Row.FindControl("linkButtonEdit")).Visible  = false;
                            ((LinkButton)Row.FindControl("linkButtonActivate")).Visible = false;
                            ((LinkButton)Row.FindControl("linkButtonDelete")).Visible = false;
                            //Change Status From Status Code to Status Name
                            Row.Cells[5].Text = "Activo";
                        }
                        else if (Row.Cells[5].Text.Equals("I", StringComparison.OrdinalIgnoreCase))
                        {
                            //((LinkButton)Row.FindControl("linkButtonEdit")).Visible = false;
                            //Change Status From Status Code to Status Name
                            ((LinkButton)Row.FindControl("linkButtonDelete")).Visible = false;
                            Row.Cells[5].Text = "Inactivo";
                        }
                        else if (Row.Cells[5].Text.Equals("P", StringComparison.OrdinalIgnoreCase))
                        {
                            //Change Status From Status Code to Status Name
                            Row.Cells[5].Text = "Pendiente";
                        }
                        else // "C"
                        {
                            ((LinkButton)Row.FindControl("linkButtonEdit")).Visible = false;
                            ((LinkButton)Row.FindControl("linkButtonActivate")).Visible = false;
                            ((LinkButton)Row.FindControl("linkButtonDelete")).Visible = false;
                            //Change Status From Status Code to Status Name
                            Row.Cells[5].Text = "Cancelado";
                        }
                    }
                }
                else
                {
                    ((LinkButton)Row.FindControl("linkButtonEdit")).Visible  = false;
                    ((LinkButton)Row.FindControl("linkButtonActivate")).Visible = false;
                    ((LinkButton)Row.FindControl("linkButtonDelete")).Visible = false;
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
            string Message = "";
            int iResult = 0;
            int RowIndex = Convert.ToInt32(e.CommandArgument);
            string UserID = this.gvUserList.DataKeys[RowIndex].Value.ToString();

            if (e.CommandName == "Editar")
            {                
                Response.Redirect("UserEdition.aspx?UserID="+Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(UserID)).Replace("+","%2B"));
            }
            else if (e.CommandName == "Borrar")//Do delete logic
            {                
                iResult = US_USUARIODal.ClassInstance.Delete_US_USUARIO(UserID);
                if (iResult > 0)
                {
                    Message = (string)GetGlobalResourceObject("DefaultResource", "msgDeleteUserSuccess");
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "DeleteSuccessfully", "confirm('" + Message + "');", true);
                    //Refresh grid data
                    InitDefaultData();
                }
            }
            else //DO activate user logic
            {                
                iResult = US_USUARIODal.ClassInstance.ActivateUser(UserID);
                //add by coco 2011-08-12
               US_USUARIOModel userMod = US_USUARIODal.ClassInstance.Get_US_USUARIOByPKID(UserID);
               MailUtility.StatusChangeEmail(userMod.Nombre_Usuario, userMod.CorreoElectronico, "activo");
                //end add
                if (iResult > 0)
                {
                    Message = (string)GetGlobalResourceObject("DefaultResource","msgActivateUserSuccess");
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "AvtivateSuccessfully", "confirm('"+Message+"');", true);
                    //Refresh grid data
                    InitDefaultData();
                }
            }
        }
        /// <summary>
        /// Assign e.CommandArgument when row created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkButton = (LinkButton)e.Row.FindControl("linkButtonEdit");
                if (linkButton != null)
                {
                    linkButton.CommandArgument = e.Row.RowIndex.ToString();
                }

                LinkButton linkDelete = (LinkButton)e.Row.FindControl("linkButtonDelete");
                if (linkDelete != null)
                {
                    linkDelete.CommandArgument = e.Row.RowIndex.ToString();
                }

                LinkButton linkActivate = (LinkButton)e.Row.FindControl("linkButtonActivate");
                if (linkActivate != null)
                {
                    linkActivate.CommandArgument = e.Row.RowIndex.ToString();
                }
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
