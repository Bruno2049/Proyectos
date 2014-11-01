using System;
using System.Collections;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.ModuloCentral;

namespace PAEEEM.CentralModule
{
    public partial class SupplierAuthorization : Page
    {
        #region Initialize Components
        /// <summary>
        ///  Init Default Data When page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (null == Session["UserInfo"])
                    {
                        Response.Redirect("../Login/Login.aspx");
                        return;
                    }
                    //Clear Filter
                    Session["CurrentRegionalAuthorization"] = 0;
                    Session["CurrentTipoSupplierAuthorization"] = 0;
                    Session["CurrentZoneAuthorization"] = 0;
                    Session["CurrentStatusSupplierAuthorization"] = 0;
                    //Initialize regional dropdownlist
                    InitializeDrpRegional();
                    //Initialize zone dropdownlist
                    InitializeDrpZona();
                    //Initialize Status dropdownlist
                    InitializeDrpStatus();
                    //Bind data for gridview
                    BindDataGridView();
                    lblFecha.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "LoadException",
                        "alert('" + ex.Message + "');", true);
                }
            }
        }

        /// <summary>
        /// Initialize Regional Dropdownlist
        /// </summary>
        private void InitializeDrpRegional()
        {
            var dtRegional = CatZonaDal.ClassInstance.GetAllRegion();
            if (dtRegional != null && dtRegional.Rows.Count > 0)
            {
                drpRegional.DataSource = dtRegional;
                drpRegional.DataValueField = "Cve_Region";
                drpRegional.DataTextField = "Dx_Nombre_Region";
                drpRegional.DataBind();
            }
            drpRegional.Items.Insert(0, new ListItem("", ""));
            drpRegional.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Zone Dropdownlist
        /// </summary>
        private void InitializeDrpZona()
        {
            var dtZone = string.IsNullOrEmpty(drpRegional.SelectedValue) ? CatZonaDal.ClassInstance.GetAllZone() : CatZonaDal.ClassInstance.GetZonaWithRegional(Convert.ToInt32(drpRegional.SelectedValue));
            if (dtZone != null && dtZone.Rows.Count > 0)
            {
                drpZona.DataSource = dtZone;
                drpZona.DataValueField = "Cve_Zona";
                drpZona.DataTextField = "Dx_Nombre_Zona";
                drpZona.DataBind();
            }
            drpZona.Items.Insert(0, new ListItem("", ""));
            drpZona.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Status dropdownlist
        /// </summary>
        private void InitializeDrpStatus()
        {
            var dtStatus = CAT_ESTATUS_PROVEEDORDal.ClassInstance.Get_Provider_Estatus();
            if (dtStatus != null && dtStatus.Rows.Count > 0)
            {
                drpEstatus.DataSource = dtStatus;
                drpEstatus.DataValueField = "Cve_Estatus_Proveedor";
                drpEstatus.DataTextField = "Dx_Estatus_Proveedor";
                drpEstatus.DataBind();
            }
            drpEstatus.Items.Insert(0, new ListItem("", ""));
            drpEstatus.SelectedIndex = 0;
        }
        #endregion

        #region Grid View Events
        /// <summary>
        /// Init grid view during the page first load
        /// </summary>    
        private void BindDataGridView()
        {
            try
            {
                //obtain products list 
                int pageCount;
                var emptyRow = false;
                //filter conditions             
                var tipoOfSupplier = (drpTipo.SelectedIndex == 0 || drpTipo.SelectedIndex == -1) ? "" : drpTipo.SelectedValue;
                var zone = (drpZona.SelectedIndex == 0 || drpZona.SelectedIndex == -1) ? "" : drpZona.SelectedValue;
                var estatus = (drpEstatus.SelectedIndex == 0 || drpEstatus.SelectedIndex == -1) ? "" : drpEstatus.SelectedValue;
                var regional = (drpRegional.SelectedIndex == 0 || drpRegional.SelectedIndex == -1) ? "" : drpRegional.SelectedValue;

                var supplier = CAT_PROVEEDORDal.ClassInstance.Get_Provider_ForAuthorization(zone, tipoOfSupplier, estatus, regional, AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out pageCount);

                if (supplier != null)
                {
                    if (supplier.Rows.Count == 0)
                    {
                        emptyRow = true;
                        supplier.Rows.Add(supplier.NewRow());
                    }
                    //Bind to grid view
                    AspNetPager.RecordCount = pageCount;
                    grvSupplier.DataSource = supplier;
                    grvSupplier.DataBind();
                }

                //hide the checkbox and Edit button when the row is empty
                if (!emptyRow) return;
                var ckbSelect = grvSupplier.Rows[0].FindControl("ckbSelect") as CheckBox;
                if (ckbSelect != null)
                {
                    ckbSelect.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "GridViewInitError",
                                                                        "alert('Excepción que se produce durante la vista de cuadrícula de inicialización:" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvSupplier_DataBound(object sender, EventArgs e)
        {
            for (var i = 0; i < grvSupplier.Rows.Count; i++)
            {
                var key = grvSupplier.DataKeys[i];
                var dataKey1 = grvSupplier.DataKeys[i];
                if (dataKey1 != null)
                {
                    var status = key != null && key[1].ToString() != "" ? int.Parse(dataKey1[1].ToString()) : 0;
                    var ckbSelect = grvSupplier.Rows[i].FindControl("ckbSelect") as CheckBox;
                    if (status == (int)ProviderStatus.CANCELADO)
                    {
                        if (ckbSelect != null) ckbSelect.Visible = false;
                    }
                }
                //Change Supplier Type 
                var dataKey = grvSupplier.DataKeys[i];
                if (dataKey == null) continue;
                switch (dataKey[2].ToString())
                {
                    case "M":
                        grvSupplier.Rows[i].Cells[3].Text = GlobalVar.SUPPLIER_M;
                        break;
                    case "B":
                        grvSupplier.Rows[i].Cells[3].Text = GlobalVar.SUPPLIER_B;
                        break;
                }
            }
        }
        /// <summary>
        /// Changed page 
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDataGridView();
            }
        }

        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            if (!IsPostBack) return;
            //setup filter conditions for data refreshing                
            drpRegional.SelectedIndex = Session["CurrentRegionalAuthorization"] != null ? (int)Session["CurrentRegionalAuthorization"] : 0;
            drpTipo.SelectedIndex = Session["CurrentTipoSupplierAuthorization"] != null ? (int)Session["CurrentTipoSupplierAuthorization"] : 0;
            drpZona.SelectedIndex = Session["CurrentZoneAuthorization"] != null ? (int)Session["CurrentZoneAuthorization"] : 0;
            drpEstatus.SelectedIndex = Session["CurrentStatusSupplierAuthorization"] != null ? (int)Session["CurrentStatusSupplierAuthorization"] : 0;
        }
        #endregion

        #region button action
        /// <summary>
        /// Active Supplier and Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActive_Click(object sender, EventArgs e)
        {
            string supplierList;
            string supplierBranch;
            // only if its Status is "Pendiente". The status will change to "Activo".
            GetSelectSuppliserIdList(out supplierList, out supplierBranch, (int)ProviderStatus.PENDIENTE);
            char[] delimiterChars = {','};
            if (supplierList != "" || supplierBranch != "")
            {
                if (supplierList != "")
                {
                    var idAfectados = supplierList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfoProv = AccesoDatos.Log.CatProveedor.ObtienePorId(Convert.ToInt32(id));
                        var result = CAT_PROVEEDORDal.ClassInstance.UpdateProviderStatus(id, (int)ProviderStatus.ACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO ACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "ACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "", "Cve_Estatus_Proveedor: " + originalInfoProv.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.ACTIVO));
                        }
                    }
                }
                if (supplierBranch != "")
                {
                    var idAfectados = supplierBranch.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var infoOriginalProvBranch = AccesoDatos.Log.CatProveedorbranch.ObtienePorId(Convert.ToInt32(id));
                        var result = SupplierBrancheDal.ClassInstance.UpdateProviderBranchStatus(id, (int)ProviderStatus.ACTIVO);
                        if (result > 0)
                        {
                            var iduser = Session["IdUserLogueado"];
                            var idRol = Session["IdRolUserLogueado"];
                            var idDepartamento = Session["IdDepartamento"];
                            var tarea = id.ToString(CultureInfo.InvariantCulture);
                            var estatusAnterior = infoOriginalProvBranch.Cve_Estatus_Proveedor;
                            var estatusActivo = Convert.ToString((int) ProviderStatus.ACTIVO);
                            /*INSERTAR EVENTO ACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(iduser),
                                Convert.ToInt16(idRol),
                                Convert.ToInt16(idDepartamento),
                                "EMPRESAS", "ACTIVAR", tarea,
                                "MOtivos??", "", "Cve_Estatus_Proveedor: " + estatusAnterior,
                                "Cve_Estatus_Proveedor: " + estatusActivo);
                        }
                    }
                }
            }
            else
            {
                ClearGridViewChecked((int)ProviderStatus.PENDIENTE);
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione Proveedor pendientes.');", true);
            }
            BindDataGridView();
        }
        /// <summary>
        /// Get Selected Product list
        /// </summary>
        /// <returns></returns>
        private void GetSelectSuppliserIdList(out string supplierList, out string branchList, int status)
        {
            supplierList = "";
            branchList = "";
            for (var i = 0; i < grvSupplier.Rows.Count; i++)
            {
                var ckbSelect = grvSupplier.Rows[i].FindControl("ckbSelect") as CheckBox;
                if (status == 0)// Cancel Supplier and Branch
                {
                    if (ckbSelect != null && !ckbSelect.Checked) continue;
                    var dataKey = grvSupplier.DataKeys[i];
                    if (dataKey != null && dataKey[2].ToString() == "M")
                    {
                        var key = grvSupplier.DataKeys[i];
                        if (key != null)
                            supplierList += key[0] + ",";
                    }
                    else
                    {
                        var key = grvSupplier.DataKeys[i];
                        if (key != null) branchList += key[0] + ",";
                    }
                }
                else//Active and Desactive Supplier and Branch
                {
                    var dataKey = grvSupplier.DataKeys[i];
                    if (dataKey != null && (ckbSelect != null && (!ckbSelect.Checked || int.Parse(dataKey[1].ToString()) != status))) continue;
                    var key = grvSupplier.DataKeys[i];
                    if (key != null && key[2].ToString() == "M")
                    {
                        var dataKey1 = grvSupplier.DataKeys[i];
                        if (dataKey1 != null)
                            supplierList += dataKey1[0] + ",";
                    }
                    else
                    {
                        var dataKey1 = grvSupplier.DataKeys[i];
                        if (dataKey1 != null) branchList += dataKey1[0]+ ",";
                    }
                }
            }
            supplierList = supplierList.TrimEnd(new[] { ',' });
            branchList = branchList.TrimEnd(new[] { ',' });
        }
        /// <summary>
        /// Deactivation Supplier and Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeActive_Click(object sender, EventArgs e)
        {
            string supplierList;
            string supplierBranch;
            char[] delimiterChars = {','};
            GetSelectSuppliserIdList(out supplierList, out supplierBranch, (int)ProviderStatus.ACTIVO);
            if (supplierList != "" || supplierBranch != "")
            {
                if (supplierList != "")
                {
                    var idAfectados = supplierList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfoProv = AccesoDatos.Log.CatProveedor.ObtienePorId(Convert.ToInt32(id));
                         var result = CAT_PROVEEDORDal.ClassInstance.UpdateProviderStatus(id, (int)ProviderStatus.INACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO INACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "INACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "", "Cve_Estatus_Proveedor: " + originalInfoProv.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.INACTIVO));
                        }
                    }
                    
                }
                if (supplierBranch != "")
                {
                    var idAfectados = supplierBranch.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var infoOriginalProvBranch = AccesoDatos.Log.CatProveedor.ObtienePorId(Convert.ToInt32(id));
                        var result = SupplierBrancheDal.ClassInstance.UpdateProviderBranchStatus(id, (int)ProviderStatus.INACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO INACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "INACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "", "Cve_Estatus_Proveedor: " + infoOriginalProvBranch.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.INACTIVO));
                        }
                    }
                }
            }
            else
            {
                ClearGridViewChecked((int)ProviderStatus.ACTIVO);
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione Proveedor ACTIVO.');", true);
            }
            BindDataGridView();
        }
        /// <summary>
        /// Cancel Supplier and Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string supplierList;
            string supplierBranch;
            char[] delimiterChars = { ',' };
            GetSelectSuppliserIdList(out supplierList, out supplierBranch, 0);
            if (supplierList != "" || supplierBranch != "")
            {
                if (supplierList != "")
                {
                    var idAfectados = supplierList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfoProv = AccesoDatos.Log.CatProveedor.ObtienePorId(Convert.ToInt32(id));
                        var result = CAT_PROVEEDORDal.ClassInstance.UpdateProviderStatus(id, (int) ProviderStatus.CANCELADO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO CANCELADO DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "CANCELADO", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "", "Cve_Estatus_Proveedor: " + originalInfoProv.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.CANCELADO));
                        }
                    }
                }
                if (supplierBranch != "")
                {
                    var idAfectados = supplierBranch.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var infoOriginalProvBranch = AccesoDatos.Log.CatProveedor.ObtienePorId(Convert.ToInt32(id));
                        var result = SupplierBrancheDal.ClassInstance.UpdateProviderBranchStatus(id,(int) ProviderStatus.CANCELADO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO CANCELADO DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "CANCELADO", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "", "Cve_Estatus_Proveedor: " + infoOriginalProvBranch.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.CANCELADO));
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione Proveedor.');", true);
            }
            BindDataGridView();
        }

        private void ClearGridViewChecked(int status)
        {
            for (var i = 0; i < grvSupplier.Rows.Count; i++)
            {
                var ckbSelect = grvSupplier.Rows[i].FindControl("ckbSelect") as CheckBox;
                var dataKey = grvSupplier.DataKeys[i];
                if (dataKey != null && (ckbSelect != null && (ckbSelect.Checked && int.Parse(dataKey[1].ToString()) != status)))
                {
                    ckbSelect.Checked = false;
                }
            }
        }
        /// <summary>
        /// Reactive Supplier and Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReActive_Click(object sender, EventArgs e)
        {
            string supplierList;
            string supplierBranch;
            var result = 0;
            char[] delimiterChars = {','};
            GetSelectSuppliserIdList(out supplierList, out supplierBranch, (int)ProviderStatus.INACTIVO);
            if (supplierList != "" || supplierBranch != "")
            {
                if (supplierList != "")
                {
                    var idAfectados = supplierList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfoProv = AccesoDatos.Log.CatProveedor.ObtienePorId(Convert.ToInt32(id));
                        result = CAT_PROVEEDORDal.ClassInstance.UpdateProviderStatus(id,(int) ProviderStatus.ACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO REACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "REACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "", "Cve_Estatus_Proveedor: " + originalInfoProv.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.ACTIVO));
                        }
                    }
                }
                if (supplierBranch != "")
                {
                    var idAfectados = supplierBranch.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var infoOriginalProvBranch = AccesoDatos.Log.CatProveedorbranch.ObtienePorId(Convert.ToInt32(id));
                        result = SupplierBrancheDal.ClassInstance.UpdateProviderBranchStatus(id,(int) ProviderStatus.ACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO REACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "REACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "",
                                "Cve_Estatus_Proveedor: " + infoOriginalProvBranch.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.ACTIVO));
                        }
                    }
                }
            }
            else
            {
                ClearGridViewChecked((int)ProviderStatus.INACTIVO);
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione Proveedor INACTIVO.');", true);
            }

            if (result <= 0) return;
            BindDataGridView();
        }
        #endregion

        #region Filter Changed Events
        /// <summary>
        /// regional change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            // RSA 2012-10-02 update zone selector, try to preserve it's selection
            var zona = drpZona.SelectedValue;
            InitializeDrpZona();
            try { drpZona.SelectedValue = zona; }   // try to keep previous selection
            catch (Exception)
            { }                   // if it fails, use the default empty selection
            Session["CurrentZoneAuthorization"] = drpZona.SelectedIndex;

            BindDataGridView();
            Session["CurrentRegionalAuthorization"] = drpRegional.SelectedIndex;
            AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// zone change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            BindDataGridView();
            Session["CurrentZoneAuthorization"] = drpZona.SelectedIndex;
            AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Type change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            BindDataGridView();
            Session["CurrentTipoSupplierAuthorization"] = drpTipo.SelectedIndex;
            AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Status change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            BindDataGridView();
            Session["CurrentStatusSupplierAuthorization"] = drpEstatus.SelectedIndex;
            AspNetPager.GoToPage(1);
        }
        #endregion

        // Add by Edu
        #region catalago_Acciones
        protected void ckbSelect_OnCheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            var row = (GridViewRow)chk.Parent.Parent;
            int rol = Convert.ToInt16(Session["IdRolUserLogueado"]);

            var combo = (DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones");

            if (chk.Checked == false)
            {
                foreach (GridViewRow item in grvSupplier.Rows)
                {
                    grvSupplier.Rows[item.DataItemIndex].Enabled = true;

                    ((DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones")).Enabled = false;
                    ((DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones")).Items.Clear();
                    ((DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones")).Items.Insert(0, "Elige Opcion");
                    BtnAceptar.Enabled = false;
                }
            }
            else
            {
                foreach (GridViewRow item in grvSupplier.Rows)
                {
                    if (row.DataItemIndex == item.DataItemIndex)
                    {
                        grvSupplier.Rows[item.DataItemIndex].Enabled = true;
                        ((DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones")).Enabled = true;
                        BtnAceptar.Enabled = true;
                    }

                    else
                    {
                        grvSupplier.Rows[item.DataItemIndex].Enabled = false;
                    }
                }

                var lista = new AccionesMonitor_Rol().AccionesMonitorRol(3, rol);
                if (lista.Count > 0)
                {
                    var estatus = grvSupplier.Rows[row.DataItemIndex].Cells[4].Text;
                    if (estatus.ToUpper() == GlobalVar.PENDING_SUPPLIER)//"PENDIENTE")
                    {
                        lista.RemoveAll(me => me.IdAccion == 18 || me.IdAccion == 19);
                    }
                    else if (estatus.ToUpper() == GlobalVar.ACTIVE_SUPPLIER)// "ACTIVO")
                    {
                        lista.RemoveAll(me => me.IdAccion == 17 || me.IdAccion == 19);
                    }
                    else if (estatus.ToUpper() == GlobalVar.INACTIVE_SUPPLIER)// "INACTIVO")
                    {
                        lista.RemoveAll(me=>me.IdAccion==17||me.IdAccion==18);
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

            foreach (GridViewRow item in grvSupplier.Rows)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }


            switch (((DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[4].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "17":
                    RadWindowManager1.RadConfirm("Confirmar Activar el Proveedor Seleccionado.", "confirmCallBackFn", 300, 100, null, "Activar");
                    break;
                case "18":
                    RadWindowManager1.RadConfirm("Confirmar Inactivar el Proveedor Seleccionado.", "confirmCallBackFn", 300, 100, null, "Inactivar");
                    break;
                case "19":
                    RadWindowManager1.RadConfirm("Confirmar Reactivar Proveedor Seleccionado.", "confirmCallBackFn", 300, 100, null, "Reactivar");
                    break;
                case "23":
                    RadWindowManager1.RadConfirm("Confirmar Cancelar Definitivamente el Proveedor Seleccionado.", "confirmCallBackFn", 300, 100, null, "Cancelar");
                    break;
            }
        }

        protected void HiddenButton_OnClick(object sender, EventArgs e)
        {
            GridViewRow row = null;

            foreach (GridViewRow item in grvSupplier.Rows)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            switch (((DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[4].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "17":

                    btnActive_Click(sender, e);
                    break;
                case "18":
                    btnDeActive_Click(sender, e);
                    break;
                case "19":
                    btnReActive_Click(sender, e);
                    break;
                case "23":
                    btnCancel_Click(sender, e);
                    break;

            }
        }    
        #endregion
    }
}
