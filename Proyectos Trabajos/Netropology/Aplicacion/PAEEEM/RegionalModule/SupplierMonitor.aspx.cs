using System;
using System.Data;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using System.Web.UI;
using PAEEEM.LogicaNegocios.ModuloCentral;

namespace PAEEEM.RegionalModule
{
    public partial class SupplierMonitor : Page
    {
        private string EditCommandArgument
        {
            get
            {
                return ViewState["EditCommandArgument"] == null ? "" : ViewState["EditCommandArgument"].ToString();
            }
            set
            {
                ViewState["EditCommandArgument"] = value;
            }
        }

        private string AssignProductCommandArgument
        {
            get
            {
                return ViewState["AssignProductCommandArgument"] == null ? "" : ViewState["AssignProductCommandArgument"].ToString();
            }
            set
            {
                ViewState["AssignProductCommandArgument"] = value;
            }
        }

        private string AssignDisposalCommandArgument
        {
            get
            {
                return ViewState["AssignDisposalCommandArgument"] == null ? "" : ViewState["AssignDisposalCommandArgument"].ToString();
            }
            set
            {
                ViewState["AssignDisposalCommandArgument"] = value;
            }
        }

        #region Initialize Components

        /// <summary>
        /// Init Default Data When page Load
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
            lblFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //Initialize filter controls                
            InitializeFilterComponents();
            //First load grid view data when page loaded
            LoadGridViewData();
            //Clear filter conditions
            ClearFilterConditions();
        }

        /// <summary>
        /// Init drop down list data
        /// </summary>
        private void InitializeFilterComponents()
        {
            InitializeZone();
            InitializeSupplierStatus();
        }

        /// <summary>
        /// Initial zone by regional
        /// </summary>
        private void InitializeZone()
        {
            var userModel = Session["UserInfo"] as US_USUARIOModel;
            if (userModel == null) return;
            var zones = CatZonaDal.ClassInstance.GetZonaWithRegional(userModel.Id_Departamento);

            if (zones == null || zones.Rows.Count <= 0) return;
            drpZona.DataSource = zones;
            drpZona.DataTextField = "Dx_Nombre_Zona";
            drpZona.DataValueField = "Cve_Zona";
            drpZona.DataBind();
            drpZona.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Initial Provider Estatus
        /// </summary>
        private void InitializeSupplierStatus()
        {
            var status = CAT_ESTATUS_PROVEEDORDal.ClassInstance.Get_Provider_Estatus();
            if (status == null || status.Rows.Count <= 0) return;
            drpEstatus.DataSource = status;
            drpEstatus.DataTextField = "Dx_Estatus_Proveedor";
            drpEstatus.DataValueField = "Cve_Estatus_Proveedor";
            drpEstatus.DataBind();
            drpEstatus.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// clear filter conditions
        /// </summary>
        private void ClearFilterConditions()
        {
            Session["CurrentZoneSupplierMonitor"] = 0;
            Session["CurrentTipoSupplierMonitor"] = 0;
            Session["CurrentEstatusSupplierMonitor"] = 0;
        }

        #endregion

        #region Grid View Control Events

        private DataTable _technologies;

        private void LoadGridViewData()
        {
            var userModel = Session["UserInfo"] as US_USUARIOModel;
            if (userModel == null) return;
            int pageCount;
            //Filter conditions
            var zone = (drpZona.SelectedIndex == 0 || drpZona.SelectedIndex == -1) ? "" : drpZona.SelectedValue;
            var tipo = (drpTipo.SelectedIndex == 0 || drpTipo.SelectedIndex == -1) ? "MB" : drpTipo.SelectedValue;
            var estatus = (drpEstatus.SelectedIndex == 0 || drpEstatus.SelectedIndex == -1)
                ? ""
                : drpEstatus.SelectedValue;

            //bool Flag = false;
            var dtSupplier = CAT_PROVEEDORDal.ClassInstance.Get_Provider(zone, tipo, estatus, userModel.Id_Departamento,
                AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out pageCount);
            if (dtSupplier == null) return;
            if (dtSupplier.Rows.Count == 0)
            {
                dtSupplier.Rows.Add(dtSupplier.NewRow());
            }

            _technologies = CAT_TECNOLOGIADAL.ClassInstance.Get_ALL_Material_Technology_Provider();
            //Bind to grid view
            AspNetPager.RecordCount = pageCount;
            grvSupplierMonitor.DataSource = dtSupplier;
            grvSupplierMonitor.DataBind();
        }

        protected void grvSupplierMonitor_DataBound(object sender, EventArgs e)
        {
            //foreach (GridViewRow gridViewRow in grvSupplierMonitor.Rows)
            //{
            //    var btnEdit = gridViewRow.FindControl("btnEdit") as Button;
            //    var btnAssignProduct = gridViewRow.FindControl("btnAssignProduct") as Button;
            //    var btnAssignDisposal = gridViewRow.FindControl("btnAssignDisposal") as Button;

            //    //Visible or invisible edit button
            //    if (btnEdit != null)
            //    {
            //        if (gridViewRow.RowType == DataControlRowType.DataRow &&
            //            (gridViewRow.Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.PENDING_SUPPLIER ||
            //             gridViewRow.Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.ACTIVE_SUPPLIER ||
            //             gridViewRow.Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.INACTIVE_SUPPLIER))
            //        {
            //            btnEdit.Visible = true;
            //            btnEdit.CommandArgument = grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][0] + ";" +
            //                                      grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][1];
            //        }
            //        else
            //        {
            //            btnEdit.Visible = false;
            //        }
            //    }

            //    //Visible or invisible assign product button
            //    if (btnAssignProduct != null)
            //    {
            //        if (gridViewRow.RowType == DataControlRowType.DataRow &&
            //            gridViewRow.Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.ACTIVE_SUPPLIER &&
            //            grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][1].ToString().ToUpper() == "PROVEEDOR")
            //        {
            //            btnAssignProduct.Visible = true;
            //            btnAssignProduct.CommandArgument =
            //                grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][0].ToString();
            //        }
            //        else
            //        {
            //            btnAssignProduct.Visible = false;
            //        }
            //    }

            //    //Visible or invisible assign disposal center button
            //    if (btnAssignDisposal == null) continue;
            //    if (gridViewRow.RowType == DataControlRowType.DataRow &&
            //        gridViewRow.Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.ACTIVE_SUPPLIER)
            //    {
            //        var id = grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][0].ToString();
            //        var type = grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][1].ToString();
            //        var t = _technologies.Select("Id_Proveedor=" + id + " AND Tipo='" + type + "'");
            //        if (t.Length > 0)
            //        {
            //            btnAssignDisposal.Visible = true;
            //            btnAssignDisposal.CommandArgument = grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][0] +
            //                                                ";" +
            //                                                grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][1];
            //        }
            //        else
            //            btnAssignDisposal.Visible = false;
            //    }
            //    else
            //    {
            //        btnAssignDisposal.Visible = false;
            //    }
            //}
        }

        #endregion

        #region Controls Changed Events

        /// <summary>
        /// Refresh Data When Pager Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadGridViewData();
            }
        }

        /// <summary>
        /// Get Filter Conditions From Cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanging(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            drpZona.SelectedIndex = Session["CurrentZoneSupplierMonitor"] != null
                ? (int) Session["CurrentZoneSupplierMonitor"]
                : 0;
            drpTipo.SelectedIndex = Session["CurrentTipoSupplierMonitor"] != null
                ? (int) Session["CurrentTipoSupplierMonitor"]
                : 0;
            drpEstatus.SelectedIndex = Session["CurrentEstatusSupplierMonitor"] != null
                ? (int) Session["CurrentEstatusSupplierMonitor"]
                : 0;
        }

        /// <summary>
        /// drpZone Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentZoneSupplierMonitor"] = drpZona.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        /// <summary>
        /// Supplier Type Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentTipoSupplierMonitor"] = drpTipo.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        /// <summary>
        /// Estatus Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentEstatusSupplierMonitor"] = drpEstatus.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        #endregion

        #region Button Clicked

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("../CentralModule/AltaProveedor.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //var btnEdit = (Button) sender;
            //var cmdArgs = btnEdit.CommandArgument.Split(';');
            var cmdArgs = EditCommandArgument.Split(';');

            if (cmdArgs.Length >= 2)
            { 
                Response.Redirect("../CentralModule/AltaProveedor.aspx?SupplierID=" +
                                  Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[0]))
                                      .Replace("+", "%2B") +
                                  "&Type=" +
                                  Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[1]))
                                      .Replace("+", "%2B")+"&Tipo=" +
                                  Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[1]))
                                      .Replace("+", "%2B"));
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {           
            var cmdArgs = EditCommandArgument.Split(';');

            if (cmdArgs.Length >= 2)
            {
                Response.Redirect("../CentralModule/AltaProveedor.aspx?SupplierID=" +
                                  Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[0]))
                                      .Replace("+", "%2B") + "&Type=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("Consultar")) +
                                  "&Tipo=" +
                                  Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[1]))
                                      .Replace("+", "%2B"));
            }
        }

        protected void btnAssignProduct_Click(object sender, EventArgs e)
        {
            //var btnAssignProduct = (Button) sender;

            Response.Redirect("AssignProductToSupplier.aspx?SupplierID=" +
                              Convert.ToBase64String(
                                  System.Text.Encoding.Default.GetBytes(AssignProductCommandArgument))//btnAssignProduct.CommandArgument))
                                  .Replace("+", "%2B"));
        }

        protected void btnAssignDisposal_Click(object sender, EventArgs e)
        {
            //var btnAssignDisposal = (Button) sender;
            //var cmdArgs = btnAssignDisposal.CommandArgument.Split(';');
            var cmdArgs = AssignDisposalCommandArgument.Split(';');

            if (cmdArgs.Length >= 2)
            {
                Response.Redirect("AssignDisposalToSupplier.aspx?SupplierID=" +
                                  Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[0]))
                                      .Replace("+", "%2B") +
                                  "&Type=" +
                                  Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[1]))
                                      .Replace("+", "%2B"));
            }
        }

        #endregion

        // ADD BY EDU
        //#region catalago acciones
        protected void ckbSelect_OnCheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox) sender;
            var row = (GridViewRow) chk.Parent.Parent;
            int rol = Convert.ToInt16(Session["IdRolUserLogueado"]);

            var combo = (DropDownList) grvSupplierMonitor.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones");

            if (chk.Checked == false)
            {
                foreach (GridViewRow item in grvSupplierMonitor.Rows)
                {
                    grvSupplierMonitor.Rows[item.DataItemIndex].Enabled = true;

                    ((DropDownList) grvSupplierMonitor.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones"))
                        .Enabled = false;
                    ((DropDownList) grvSupplierMonitor.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones"))
                        .Items.Clear();
                    ((DropDownList) grvSupplierMonitor.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones"))
                        .Items.Insert(0, "Elige Opcion");
                    BtnAceptar.Enabled = false;
                }
            }
            else
            {
                foreach (GridViewRow item in grvSupplierMonitor.Rows)
                {
                    if (row.DataItemIndex == item.DataItemIndex)
                    {
                        grvSupplierMonitor.Rows[item.DataItemIndex].Enabled = true;
                        ((DropDownList) grvSupplierMonitor.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones"))
                            .Enabled = true;
                        BtnAceptar.Enabled = true;
                    }

                    else
                    {
                        grvSupplierMonitor.Rows[item.DataItemIndex].Enabled = false;
                    }
                }
                var lista = new AccionesMonitor_Rol().AccionesMonitorRol(2, rol);
                if (lista.Count > 0)
                {
                    //Visible or invisible edit
                    if (grvSupplierMonitor.Rows[row.DataItemIndex].Cells[4].Text.Replace("&nbsp;", "").ToUpper() ==
                        GlobalVar.PENDING_SUPPLIER ||
                        grvSupplierMonitor.Rows[row.DataItemIndex].Cells[4].Text.Replace("&nbsp;", "").ToUpper() ==
                        GlobalVar.ACTIVE_SUPPLIER ||
                        grvSupplierMonitor.Rows[row.DataItemIndex].Cells[4].Text.Replace("&nbsp;", "").ToUpper() ==
                        GlobalVar.INACTIVE_SUPPLIER)
                    {
                        //btnEdit.Visible = true;
                        EditCommandArgument = grvSupplierMonitor.DataKeys[row.DataItemIndex][0] + ";" +
                                              grvSupplierMonitor.DataKeys[row.DataItemIndex][1];
                    }
                    else
                    {
                        //btnEdit.Visible = false;
                        lista.RemoveAll(me => me.IdAccion == 24);
                    }

                    //Visible or invisible assign product
                    if (
                        grvSupplierMonitor.Rows[row.DataItemIndex].Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.ACTIVE_SUPPLIER &&
                        ((Label)grvSupplierMonitor.Rows[row.DataItemIndex].FindControl("lblTipo")).Text == "Sucursal Fisica")
                    {
                        //btnAssignProduct.Visible = true;
                        AssignProductCommandArgument = grvSupplierMonitor.DataKeys[row.DataItemIndex][0].ToString();
                    }
                    else
                    {
                        //btnAssignProduct.Visible = false;
                        lista.RemoveAll(me => me.IdAccion == 20);
                    }

                    //Visible or invisible assign disposal center button
                    if (
                       grvSupplierMonitor.Rows[row.DataItemIndex].Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.ACTIVE_SUPPLIER)
                    {
                        var id = grvSupplierMonitor.DataKeys[row.DataItemIndex][0].ToString();
                        var type = grvSupplierMonitor.DataKeys[row.DataItemIndex][1].ToString();
                        _technologies = CAT_TECNOLOGIADAL.ClassInstance.Get_ALL_Material_Technology_Provider();
                        var t = _technologies.Select("Id_Proveedor=" + id + " AND Tipo='" + type + "'");
                        if (t.Length > 0)
                        {
                            //btnAssignDisposal.Visible = true;
                            AssignDisposalCommandArgument = grvSupplierMonitor.DataKeys[row.DataItemIndex][0] +
                                                                ";" +
                                                                grvSupplierMonitor.DataKeys[row.DataItemIndex][1];
                        }
                        else
                            //btnAssignDisposal.Visible = false;
                            lista.RemoveAll(me => me.IdAccion == 21);
                    }
                    else
                    {
                        //btnAssignDisposal.Visible = false;
                        lista.RemoveAll(me => me.IdAccion == 21);
                    }

                    combo.DataSource = lista;
                    combo.DataValueField = "IdAccion";
                    combo.DataTextField = "NombreAccion";
                    combo.DataBind();
                    combo.Items.Insert(0, "Elegir Opcion");
                    combo.SelectedIndex = 0;
                }
            }
        }

        protected void BtnAceptar_OnClick(object sender, EventArgs e)
        {
            GridViewRow row = null;
            try
            {
                foreach (GridViewRow item in grvSupplierMonitor.Rows)
                {
                    if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                    {
                        row = item;
                    }
                }


                switch (((DropDownList)grvSupplierMonitor.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones")).SelectedValue)
                {
                    case "20":
                        RadWindowManager1.RadConfirm("Confirmar Asignar Productos al Proveedor seleccionado.", "confirmCallBackFn", 300, 100, null, "Asignar Productos");
                        break;
                    case "21":
                        RadWindowManager1.RadConfirm("Confirmar Asignar CAyD al Proveedor seleccionado.", "confirmCallBackFn", 300, 100, null, "Asignar CAyD");
                        break;
                    case "24":
                        RadWindowManager1.RadConfirm("Confirmar Editar Proveedor seleccionado.", "confirmCallBackFn", 300, 100, null, "Editar");
                        break;
                    case "30":
                        RadWindowManager1.RadConfirm("Confirmar Mostrar la informacion seleccionada.", "confirmCallBackFn", 300, 100, null, "Editar");
                        break;
                }
            }
            catch (Exception)
            {
                return;

            }
        }

        protected void HiddenButton_OnClick(object sender, EventArgs e)
        {
            GridViewRow row = null;

            foreach (GridViewRow item in grvSupplierMonitor.Rows)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }


            switch (((DropDownList)grvSupplierMonitor.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "20":
                    btnAssignProduct_Click(sender,e);
                    break;
                case "21":
                    btnAssignDisposal_Click(sender,e);
                    break;
                case "24":
                    btnEdit_Click(sender,e);
                    break;
                case "30":
                    btnConsultar_Click(sender, e);
                    break;
            }
        }
    }
}
