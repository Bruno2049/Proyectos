using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.CentralModule
{
    public partial class DisposalCenterAuthorization : Page
    {
        #region Initialize Components
        /// <summary>
        ///  Init Default Data When page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            try
            {
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                //Clear Filter
                Session["CurrentRegionalProductAuthorization"] = 0;
                Session["CurrentTipoDisposalAuthorization"] = 0;
                Session["CurrentZoneProductAuthorization"] = 0;
                Session["CurrentStatusProductAuthorization"] = 0;
                Session["CurrentCentroAcopioAuthorization"] = 0;
                //Initialize regional dropdownlist
                InitializeDrpRegional();
                //Initialize zone dropdownlist
                InitializeDrpZona();
                //Initialize Status dropdownlist
                InitializeDrpStatus();
                //Initialize CAyD dropdownlist
                InitializeDrpCayD();
                //Bind data for gridview
                BindDataGridView();
                //Initial date
                lblFecha.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "LoadException", "alert('" + ex.Message + "');", true);
            }
        }

        // Initialize Regional Dropdownlist
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
            DataTable dtStatus = CAT_ESTATUS_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterEstatus();
            if (dtStatus != null && dtStatus.Rows.Count > 0)
            {
                drpEstatus.DataSource = dtStatus;
                drpEstatus.DataValueField = "Cve_Estatus_Centro_Disp";
                drpEstatus.DataTextField = "Dx_Estatus_Centro_Disp";
                drpEstatus.DataBind();
            }
            drpEstatus.Items.Insert(0, new ListItem("", ""));
            drpEstatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Initialize Status dropdownlist
        /// </summary>
        private void InitializeDrpCayD()
        {
            var dtCayD = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterAndBranch();
            if (dtCayD != null && dtCayD.Rows.Count > 0)
            {
                drpCAyD.DataSource = dtCayD;
                drpCAyD.DataValueField = "Id_Centro_Disp";
                drpCAyD.DataTextField = "Dx_Razon_Social";
                drpCAyD.DataBind();
            }
            drpCAyD.Items.Insert(0, new ListItem("", ""));
            drpCAyD.SelectedIndex = 0;
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
                var tipoOfDisposal = (drpTipo.SelectedIndex == 0 || drpTipo.SelectedIndex == -1) ? "" : drpTipo.SelectedValue;
                var zone = (drpZona.SelectedIndex == 0 || drpZona.SelectedIndex == -1) ? "" : drpZona.SelectedValue;
                var estatus = (drpEstatus.SelectedIndex == 0 || drpEstatus.SelectedIndex == -1) ? 0 : int.Parse(drpEstatus.SelectedValue);
                var cayD = (drpCAyD.SelectedIndex == 0 || drpCAyD.SelectedIndex == -1) ? "" : drpCAyD.SelectedValue;
                var regional = (drpRegional.SelectedIndex == 0 || drpRegional.SelectedIndex == -1) ? "" : drpRegional.SelectedValue;
                var idCd = string.Empty;

                if (!string.IsNullOrEmpty(cayD))
                {
                    string[] partes = cayD.Split('-');
                    if (partes.Length > 1)
                    {
                        idCd = partes[0];
                        tipoOfDisposal = partes[1] == "(Matriz)" ? "M" : "B";
                        drpTipo.SelectedIndex = 0;
                    }
                }

                DataTable disposal = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalAndBranchWithZoneAndStatus(zone, tipoOfDisposal, estatus, idCd, regional, AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out pageCount);

                if (disposal != null)
                {
                    if (disposal.Rows.Count == 0)
                    {
                        emptyRow = true;
                        disposal.Rows.Add(disposal.NewRow());
                    }
                    //Bind to grid view
                    AspNetPager.RecordCount = pageCount;
                    grvDisposalCenter.DataSource = disposal;
                    grvDisposalCenter.DataBind();
                }

                //hide the checkbox and Edit button when the row is empty
                if (!emptyRow) return;
                var ckbSelect = grvDisposalCenter.Rows[0].FindControl("ckbSelect") as CheckBox;
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
        protected void grvDisposalCenter_DataBound(object sender, EventArgs e)
        {
            for (var i = 0; i < grvDisposalCenter.Rows.Count; i++)
            {
                var status = grvDisposalCenter.DataKeys[i][1].ToString() != "" ? int.Parse(grvDisposalCenter.DataKeys[i][1].ToString()) : 0;
                var ckbSelect = grvDisposalCenter.Rows[i].FindControl("ckbSelect") as CheckBox;
                if (status != (int) ProviderStatus.CANCELADO) continue;
                if (ckbSelect != null) ckbSelect.Visible = false;
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
            drpRegional.SelectedIndex = Session["CurrentRegionalProductAuthorization"] != null ? (int)Session["CurrentRegionalProductAuthorization"] : 0;
            drpTipo.SelectedIndex = Session["CurrentTipoDisposalAuthorization"] != null ? (int)Session["CurrentTipoDisposalAuthorization"] : 0;
            drpZona.SelectedIndex = Session["CurrentZoneProductAuthorization"] != null ? (int)Session["CurrentZoneProductAuthorization"] : 0;
            drpEstatus.SelectedIndex = Session["CurrentStatusProductAuthorization"] != null ? (int)Session["CurrentStatusProductAuthorization"] : 0;
            drpCAyD.SelectedIndex = Session["CurrentCentroAcopioAuthorization"] != null ? (int)Session["CurrentCentroAcopioAuthorization"] : 0;
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
            catch (Exception) { }                   // if it fails, use the default empty selection
            Session["CurrentZoneProductAuthorization"] = drpZona.SelectedIndex;

            BindDataGridView();
            Session["CurrentRegionalProductAuthorization"] = drpRegional.SelectedIndex;
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
            Session["CurrentZoneProductAuthorization"] = drpZona.SelectedIndex;
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
            Session["CurrentTipoDisposalAuthorization"] = drpTipo.SelectedIndex;
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
            Session["CurrentStatusProductAuthorization"] = drpEstatus.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        /// <summary>
        /// Status change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCAyD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            BindDataGridView();
            Session["CurrentCentroAcopioAuthorization"] = drpCAyD.SelectedIndex;
            AspNetPager.GoToPage(1);
        }
        #endregion

        #region Button Action

        /// <summary>
        /// Active Disposal Center And Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActive_Click(object sender, EventArgs e)
        {
            string disposalList;
            string disposalBranch;
            // only if its Status is "Pendiente". The status will change to "Activo".
            GetSelectSuppliserIdList(out disposalList, out disposalBranch, (int) DisposalCenterStatus.PENDIENTE);
            char[] delimiterChars = { ',' };
            if (disposalList != "" || disposalBranch != "")
            {
                if (disposalList != "")
                {
                    var idAfectados = disposalList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfodisposal = AccesoDatos.Log.CatCentroDisp.ObtienePorId(Convert.ToInt32(id));
                        var result = CAT_CENTRO_DISPDAL.ClassInstance.UpdateDisposalStatus(id, (int)DisposalCenterStatus.ACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO ACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "ACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "", "Cve_Estatus_Centro_Disp: " + originalInfodisposal.Cve_Estatus_Centro_Disp,
                                "Cve_Estatus_Centro_Disp: " + Convert.ToString((int)DisposalCenterStatus.ACTIVO));
                        }
                    }
                }
                if (disposalBranch != "")
                {
                    var idAfectadosBrach = disposalBranch.Split(delimiterChars);
                    foreach (var id in idAfectadosBrach)
                    {
                        var originalInfodisposalBranch= AccesoDatos.Log.CatCentroDispSucursal.ObtienePorId(Convert.ToInt32(id));
                        var result = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.UpdateDisposalBranchStatus(id, (int)DisposalCenterStatus.ACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO ACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "ACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "", "Cve_Estatus_Centro_Disp: " + originalInfodisposalBranch.Cve_Estatus_Centro_Disp,
                                "Cve_Estatus_Centro_Disp: " + Convert.ToString((int)DisposalCenterStatus.ACTIVO));
                        }
                    }
                }
            }
            else
            {
                ClearGridViewChecked((int) DisposalCenterStatus.PENDIENTE);
                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "SelectProduct",
                    "alert('Por favor, seleccione CAyD pendientes.');", true);
            }
            BindDataGridView();
        }

        /// <summary>
        /// Get Select Disposal Center
        /// </summary>
        /// <param name="Status"></param>
        private void GetSelectSuppliserIdList(out string DisposalList, out string DisposalBranch, int Status)
        {
            DisposalList = "";
            DisposalBranch = "";
            for (var i = 0; i < grvDisposalCenter.Rows.Count; i++)
            {
                var ckbSelect = grvDisposalCenter.Rows[i].FindControl("ckbSelect") as CheckBox;
                if (Status == 0)// Cancel Supplier and Branch
                {
                    if (ckbSelect == null || !ckbSelect.Checked) continue;
                    if (grvDisposalCenter.Rows[i].Cells[4].Text.ToUpper() == "MATRIZ") // updated by tina 2012-02-29
                    {
                        DisposalList += grvDisposalCenter.DataKeys[i][0] + ",";
                    }
                    else
                    {
                        DisposalBranch += grvDisposalCenter.DataKeys[i][0] + ",";
                    }
                }
                else//Active and Desactive Supplier and Branch
                {
                    if (ckbSelect == null ||
                        (!ckbSelect.Checked || int.Parse(grvDisposalCenter.DataKeys[i][1].ToString()) != Status))
                        continue;
                    if (grvDisposalCenter.Rows[i].Cells[4].Text.ToUpper() == "MATRIZ") // updated by tina 2012-02-29
                    {
                        DisposalList += grvDisposalCenter.DataKeys[i][0] + ",";
                    }
                    else
                    {
                        DisposalBranch += grvDisposalCenter.DataKeys[i][0] + ",";
                    }
                }
            }
            DisposalList = DisposalList.TrimEnd(new[] { ',' });
            DisposalBranch = DisposalBranch.TrimEnd(new[] { ',' });
        }
        /// <summary>
        /// DesActvie Disposal Center And Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeActive_Click(object sender, EventArgs e)
        {
            string disposalList;
            string disposalBranch;
            char[] delimiterChars = { ',' };
            // only if its Status is "Active". The status will change to "InActivo".
            GetSelectSuppliserIdList(out disposalList, out disposalBranch, (int)DisposalCenterStatus.ACTIVO);
            if (disposalList != "" || disposalBranch != "")
            {
                if (disposalList != "")
                {
                    var idAfectados = disposalList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfodisposal = AccesoDatos.Log.CatCentroDisp.ObtienePorId(Convert.ToInt32(id));
                        var result = CAT_CENTRO_DISPDAL.ClassInstance.UpdateDisposalStatus(id, (int)DisposalCenterStatus.INACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO INACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "INACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "",
                                "Cve_Estatus_Centro_Disp: " + originalInfodisposal.Cve_Estatus_Centro_Disp,
                                "Cve_Estatus_Centro_Disp: " + Convert.ToString((int) DisposalCenterStatus.INACTIVO));
                        }
                    }
                }
                if (disposalBranch != "")
                {
                    var idAfectadosBrach = disposalBranch.Split(delimiterChars);
                    foreach (var id in idAfectadosBrach)
                    {
                        var originalInfodisposalBrach = AccesoDatos.Log.CatCentroDispSucursal.ObtienePorId(Convert.ToInt32(id));
                        var result = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.UpdateDisposalBranchStatus(id, (int)DisposalCenterStatus.INACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO INACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "INACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "",
                                "Cve_Estatus_Centro_Disp: " + originalInfodisposalBrach.Cve_Estatus_Centro_Disp,
                                "Cve_Estatus_Centro_Disp: " + Convert.ToString((int) DisposalCenterStatus.INACTIVO));
                        }
                    }
                }
            }
            else
            {
                ClearGridViewChecked((int)DisposalCenterStatus.ACTIVO);
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione CAyD Activo.');", true);
            }
            BindDataGridView();
        }

        /// <summary>
        /// Cancel Disposal Center And Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string disposalList;
            string disposalBranch;
            char[] delimiterChars = { ',' };
            GetSelectSuppliserIdList(out disposalList, out disposalBranch, 0);
            if (disposalList != "" || disposalBranch != "")
            {
                if (disposalList != "")
                {
                    var idAfectados = disposalList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfodisposal = AccesoDatos.Log.CatCentroDisp.ObtienePorId(Convert.ToInt32(id));
                        var result = CAT_CENTRO_DISPDAL.ClassInstance.UpdateDisposalStatus(id, (int)DisposalCenterStatus.CANCELADO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO CANCELADO DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "CANCELADO", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "",
                                "Cve_Estatus_Centro_Disp: " + originalInfodisposal.Cve_Estatus_Centro_Disp,
                                "Cve_Estatus_Centro_Disp: " + Convert.ToString((int) DisposalCenterStatus.CANCELADO));
                        }
                    }
                }
                if (disposalBranch != "")
                {
                    var idAfectadosBrach = disposalBranch.Split(delimiterChars);
                    foreach (var id in idAfectadosBrach)
                    {
                        var originalInfodisposalBrach = AccesoDatos.Log.CatCentroDispSucursal.ObtienePorId(Convert.ToInt32(id));
                        var result = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.UpdateDisposalBranchStatus(id, (int)DisposalCenterStatus.CANCELADO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO CANCELADO DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "CANCELADO", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "",
                                "Cve_Estatus_Centro_Disp: " + originalInfodisposalBrach.Cve_Estatus_Centro_Disp,
                                "Cve_Estatus_Centro_Disp: " + Convert.ToString((int)DisposalCenterStatus.CANCELADO));
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "SelectProduct",
                    "alert('Por favor, seleccione CAyD Pendiente.');", true);
            }
            BindDataGridView();
        }

        private void ClearGridViewChecked(int status)
        {
            for (var i = 0; i < grvDisposalCenter.Rows.Count; i++)
            {
                var ckbSelect = grvDisposalCenter.Rows[i].FindControl("ckbSelect") as CheckBox;
                var dataKey = grvDisposalCenter.DataKeys[i];
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
            string disposalList;
            string disposalBranch;
            char[] delimiterChars = {','};
            // only if its Status is "Inactive". The status will change to "Reactive".
            GetSelectSuppliserIdList(out disposalList, out disposalBranch, (int)DisposalCenterStatus.INACTIVO);
            
            if (disposalList != "" || disposalBranch != "")
            {
                if (disposalList != "")
                {
                    var idAfectados = disposalList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfodisposal = AccesoDatos.Log.CatCentroDisp.ObtienePorId(Convert.ToInt32(id));
                        var result = CAT_CENTRO_DISPDAL.ClassInstance.UpdateDisposalStatus(id, (int)DisposalCenterStatus.ACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO REACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "REACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "",
                                "Cve_Estatus_Centro_Disp: " + originalInfodisposal.Cve_Estatus_Centro_Disp,
                                "Cve_Estatus_Centro_Disp: " + Convert.ToString((int)DisposalCenterStatus.ACTIVO));
                        }
                    }
                }
                if (disposalBranch != "")
                {
                    var idAfectadosBrach = disposalBranch.Split(delimiterChars);
                    foreach (var id in idAfectadosBrach)
                    {
                        var originalInfodisposalBrach = AccesoDatos.Log.CatCentroDispSucursal.ObtienePorId(Convert.ToInt32(id));
                        var result = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.UpdateDisposalBranchStatus(id, (int)DisposalCenterStatus.ACTIVO);
                        if (result > 0)
                        {
                            /*INSERTAR EVENTO REACTIVAR DEL TIPO DE PROCESO EMPRESAS EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "REACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "MOtivos??", "",
                                "Cve_Estatus_Centro_Disp: " + originalInfodisposalBrach.Cve_Estatus_Centro_Disp,
                                "Cve_Estatus_Centro_Disp: " + Convert.ToString((int)DisposalCenterStatus.ACTIVO));
                        }
                    }
                    
                }
            }
            else
            {
                ClearGridViewChecked((int)DisposalCenterStatus.INACTIVO);
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione CAyD Inactivo.');", true);
            }
            BindDataGridView();
        }
        #endregion
    }
}
