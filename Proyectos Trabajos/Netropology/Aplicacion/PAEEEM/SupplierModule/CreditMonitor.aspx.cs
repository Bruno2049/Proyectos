using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using System.Transactions;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.TarifaSubEstaciones;
using PAEEEM.LogicaNegocios.Credito;
using PAEEEM.LogicaNegocios.ModuloCentral;

using PAEEEM.Entidades;

namespace PAEEEM
{
    public partial class CreditMonitor : System.Web.UI.Page
    {
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
            literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //Session is not null to load default data                
            btn_Aceptar.Enabled = false;
            InitDropDownList();
            LoadGridViewData();
            Session["CurrentStatusCreditMonitor"] = 0;
            Session["CurrentRazonCreditMonitor"] = 0;
            Session["CurrentDateCreditMonitor"] = 0;
            Session["CurrenttechnologyCreditMonitor"] = 0;

            btn_Aceptar.Enabled = false;

            ////add by @l3x////
            DDLMotivo.Attributes.Add("onchange", "ValidarCbxMotivo()");
            TexObserv.Attributes.Add("onkeyup", "ValidarObservaciones(this, 300);");
            TexMoti.Attributes.Add("onkeyup", "ValidarMotivo(this, 300);");
        }
        /// <summary>
        /// Init drop down list data
        /// </summary>
        private void InitDropDownList()
        {
            InitStatus();
            InitTechnology();
            InitRazonSocial();
            InitPendienteDate();
        }
        /// <summary>
        /// Load status options
        /// </summary>
        private void InitStatus()
        {
            var dtCreditStatus = CatEstatusDal.ClassInstance.GetCreditEstatus();
            if (dtCreditStatus == null) return;
            drpEstatus.DataSource = dtCreditStatus;
            drpEstatus.DataTextField = "Dx_Estatus_Credito";
            drpEstatus.DataValueField = "Cve_Estatus_Credito";
            drpEstatus.DataBind();
            drpEstatus.Items.Insert(0, new ListItem("", "-1"));
        }
        /// <summary>
        /// Load technology
        /// </summary>
        private void InitTechnology()
        {
            var dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgram(Global.PROGRAM.ToString(CultureInfo.InvariantCulture));//Changed by Jerry 2011-08-08

            if (dtTechnology == null) return;

            drpTechnology.DataSource = dtTechnology;
            drpTechnology.DataTextField = "Dx_Nombre_Particular";
            drpTechnology.DataValueField = "Cve_Tecnologia";
            drpTechnology.DataBind();
            drpTechnology.Items.Insert(0, new ListItem("", "-1"));
        }

        /// <summary>
        /// Load credit Pending Date and P. Física o razón social
        /// </summary>
        private void InitRazonSocial()
        {
            int proveedorId;
            string userType;

            if (Session["UserInfo"] == null) return;
            var user = (US_USUARIOModel)Session["UserInfo"];

            if (user.Tipo_Usuario == GlobalVar.SUPPLIER_BRANCH)
            {
                var model = CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORByBranchID(user.Id_Departamento.ToString(CultureInfo.InvariantCulture));
                
                proveedorId = model.Id_Proveedor;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }
            else
            {
                proveedorId = user.Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }

            var dtRazonSocial = K_CREDITODal.ClassInstance.GetRazonSocial(proveedorId, userType);

            if (dtRazonSocial == null) return;

            drpRazonSocial.DataSource = dtRazonSocial;
            drpRazonSocial.DataTextField = "Dx_Razon_Social";
            drpRazonSocial.DataBind();
            drpRazonSocial.Items.Insert(0, new ListItem(""));
        }

        /// <summary>
        /// Load credit pendiente date
        /// </summary>
        private void InitPendienteDate()
        {
            int proveedorId;
            string userType;

            if (Session["UserInfo"] == null) return;

            var user = (US_USUARIOModel)Session["UserInfo"];

            if (user.Tipo_Usuario == GlobalVar.SUPPLIER_BRANCH)
            {
                var model = CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORByBranchID(user.Id_Departamento.ToString(CultureInfo.InvariantCulture));
                
                proveedorId = model.Id_Proveedor;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }
            else
            {
                proveedorId = user.Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }
            var dtCreditPendienteDate = K_CREDITODal.ClassInstance.GetPendienteDate(proveedorId, userType);

            if (dtCreditPendienteDate == null) return;

            drpFechaDate.DataSource = dtCreditPendienteDate;
            drpFechaDate.DataTextField = "Dt_Fecha_Pendiente";
            drpFechaDate.DataBind();
            drpFechaDate.Items.Insert(0, new ListItem(""));
        }
        /// <summary>
        /// Load default data when page load
        /// </summary>
        private void LoadGridViewData()
        {
            var pageCount = 0;
            var proveedorId = 0;
            var userType = "";

            if (Session["UserInfo"] != null)
            {
                proveedorId = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }

            var dtCredits = K_CREDITODal.ClassInstance.GetCredits(proveedorId, userType, string.Empty, 1, AspNetPager.PageSize, out pageCount);

            if (dtCredits == null) return;

            if (dtCredits.Rows.Count == 0)
            {
                dtCredits.Rows.Add(dtCredits.NewRow());
            }

            //Bind to grid view
            AspNetPager.RecordCount = pageCount;
            grdCredit.DataSource = dtCredits;
            grdCredit.DataBind();
        }

        /// <summary>
        /// Refresh Data When Pager Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                RefreshGridData();
            }
        }

        protected void AspNetPager_PageChanging(object sender, EventArgs e)
        {
            if (!IsPostBack) return;

            drpEstatus.SelectedIndex = Session["CurrentStatusCreditMonitor"] != null ? (int)Session["CurrentStatusCreditMonitor"] : 0;
            drpRazonSocial.SelectedIndex = Session["CurrentRazonCreditMonitor"] != null ? (int)Session["CurrentRazonCreditMonitor"] : 0;
            drpFechaDate.SelectedIndex = Session["CurrentDateCreditMonitor"] != null ? (int)Session["CurrentDateCreditMonitor"] : 0;
            drpTechnology.SelectedIndex = Session["CurrenttechnologyCreditMonitor"] != null ? (int)Session["CurrenttechnologyCreditMonitor"] : 0;
        }

        /// <summary>
        /// Refresh grid data
        /// </summary>
        private void RefreshGridData()
        {
            int pageCount;
            var proveedorId = 0;
            var userType = "";

            if (Session["UserInfo"] != null)
            {
                proveedorId = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;

            }

            var pendingDate = (drpFechaDate.SelectedIndex == 0 || drpFechaDate.SelectedIndex == -1) ? "" : drpFechaDate.SelectedItem.Text;
            var razonSocial = (drpRazonSocial.SelectedIndex == 0 || drpRazonSocial.SelectedIndex == -1) ? "" : drpRazonSocial.SelectedItem.Text;
            var status = (drpEstatus.SelectedIndex == 0 || drpEstatus.SelectedIndex == -1) ? -1 : Convert.ToInt32(drpEstatus.SelectedValue);
            var technology = (drpTechnology.SelectedIndex == 0 || drpTechnology.SelectedIndex == -1) ? -1 : Convert.ToInt32(drpTechnology.SelectedValue);

            var dtCredits = K_CREDITODal.ClassInstance.GetCredits(proveedorId, userType, pendingDate, status, razonSocial, technology, string.Empty, AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out pageCount);

            if (dtCredits == null) return;

            if (dtCredits.Rows.Count == 0)
            {
                dtCredits.Rows.Add(dtCredits.NewRow());
            }

            //Bind to grid view
            AspNetPager.RecordCount = pageCount;
            grdCredit.DataSource = dtCredits;
            grdCredit.DataBind();
        }

        /// <summary>
        /// Do the action when command button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals("Validate", StringComparison.OrdinalIgnoreCase)) return;

            var rowIndex = Convert.ToInt32(e.CommandArgument);
            var dataKey = grdCredit.DataKeys[rowIndex];
            if (dataKey == null) return;
            var creditNumber = dataKey.Value.ToString();
            Response.Redirect("ValidateCrediticialHistory.aspx?CreditNumber=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(creditNumber)).Replace("+", "%2B"));
        }
        /// <summary>
        /// Pass Row Index to CommandArgument
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var linkValidate = (LinkButton)e.Row.FindControl("linkValidate");
            if (linkValidate != null)
            {
                linkValidate.CommandArgument = e.Row.RowIndex.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Refresh grid data when fecha date changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpFechaDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
            Session["CurrentDateCreditMonitor"] = drpFechaDate.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        /// <summary>
        /// Refresh grid data when status changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
            Session["CurrentStatusCreditMonitor"] = drpEstatus.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        /// <summary>
        /// Refresh grid data when razón social changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpRazonSocial_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
            Session["CurrentRazonCreditMonitor"] = drpRazonSocial.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        /// <summary>
        /// Refresh grid data when technology changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
            Session["CurrenttechnologyCreditMonitor"] = drpTechnology.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        /// <summary>
        /// Hide validation button when empty line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdCredit.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow && (grdCredit.DataKeys[0].Value.ToString() == "" || !row.Cells[6].Text.Equals("PENDIENTE", StringComparison.OrdinalIgnoreCase) && !row.Cells[6].Text.Equals("POR ENTREGAR", StringComparison.OrdinalIgnoreCase)))
                {
                    row.FindControl("linkValidate").Visible = false;
                }
                // RSA 20130813 If user Distribuidor and credit Pendiente without MOP then enable edition button
                // and add warning about loosing edition capabilities to linkValidate
                else if (row.Cells[6].Text.Equals("PENDIENTE", StringComparison.OrdinalIgnoreCase) && ((US_USUARIOModel)Session["UserInfo"]).Id_Rol == 3)
                {
                    //((LinkButton)row.FindControl("linkValidate")).OnClientClick
                    //    = "return confirm('Confirmar Realizar Validación e Integración del Expediente\\n\\nNO podrá editar el crédito después de hacer la consulta crediticia');";
                    //row.FindControl("btnEdit").Visible = true; Editado por esteban
                }
                // END
            }
        }
        /// <summary>
        /// Add new credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddCredit_Click(object sender, EventArgs e)
        {
            //Updated by Edu
            var montoTotal = new DatosMontos().montoTotalPrograma();
            var montoTotalDisponible = new DatosMontos().montoDisponiblePrograma();
            var montoMinimoTotal = new DatosMontos().montoMinimoPrograma();

            var montoincentivo = new DatosMontos().montoTotalIncentivo();
            var montoMinimoIncentivo = new DatosMontos().montoMinimoIncentivo();
            var montoDisponibleIncentivo = new DatosMontos().montoDisponibleIncentivo();
            string tipoMonto;
            string cantidad;

            if (((montoDisponibleIncentivo * 100) / montoincentivo) > 90)
            {
                tipoMonto = "Monto del Incentivo Energético";
                cantidad = montoincentivo.ToString();
                try
                {
                    MailUtility.MetasMontoTotal("MontoMetas.html", "eduardohernandez.s159@gmail.com",
                        DateTime.Now.ToLongDateString(), tipoMonto, cantidad);

                    //var correos = new DatosMontos().CorreoUsuario();

                    //foreach (var correo in correos)
                    //{
                    //    MailUtility.MetasMontoTotal("MontoMetas.html", correo.VALOR, DateTime.Now.ToLongDateString(),
                    //        tipoMonto, cantidad);
                    //}
                }
                catch (Exception w)
                {
                    var a = w.Message;
                }
            }

            if (((montoTotalDisponible * 100) / montoTotal) > 90)
            {
                tipoMonto = "Monto de Financiamiento";
                cantidad = montoTotal.ToString();
                try
                {
                    MailUtility.MetasMontoTotal("MontoMetas.html", "eduardohernandez.s159@gmail.com", DateTime.Now.ToLongDateString(), tipoMonto, cantidad);
                    //var correos = new DatosMontos().CorreoUsuario();

                    //foreach (var correo in correos)
                    //{
                    //    MailUtility.MetasMontoTotal("MontoMetas.html", correo.VALOR, DateTime.Now.ToLongDateString(),
                    //        tipoMonto, cantidad);
                    //}
                }
                catch (Exception w)
                {
                    var a = w.Message;
                }
            }


            if (montoTotalDisponible > montoMinimoTotal)
            {
                if (montoDisponibleIncentivo > montoMinimoIncentivo)
                {
                    Response.Redirect("../Captcha/valida.aspx");
                }
                else
                {
                    RadWindowManager1.RadConfirm("Por el momento solo podrá registrar solicitudes de tecnologías consideradas como adquisición.", "confirmCallBackFn", 300, 100, null, "Nueva Solicitud");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AlertInform",
                        "alert('Por el momento no se puede registrar esta solicitud debido a que los recursos del programa se encuentran comprometidos');",
                        true);
            }
        }
        
        /// <summary>
        /// Edit an existing credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var btnEdit = (Button)sender;
            var credit = btnEdit.CommandArgument;
            Response.Redirect("CapturaDatosSolicitud.aspx?Token=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(credit)));
        }

        /// <summary>
        /// Cancel selected credits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnCancelCredit_Click(object sender, EventArgs e)
        //{
        //    string CreditNumber;
        //    int iCount = 0, iSucess = 0;
        //    string strMsg = "";
        //    bool bIsSelected = false;

        //    #region Check If Have Records Be Seleted

        //    for (int i = 0; i < grdCredit.Rows.Count; i++)
        //    {
        //        bIsSelected = ((CheckBox)grdCredit.Rows[i].FindControl("ckbSelect")).Checked;
        //        if (bIsSelected)
        //        {
        //            iCount++;
        //        }
        //    }

        //    if (iCount == 0)
        //    {
        //        strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPleaseSelectOne") as string;
        //        ScriptManager.RegisterStartupScript(this, GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
        //        return;
        //    }
        //    #endregion

        //    #region Cancel The Selected Credits

        //    for (int i = 0; i < grdCredit.Rows.Count; i++)
        //    {
        //        bIsSelected = ((CheckBox)grdCredit.Rows[i].FindControl("ckbSelect")).Checked;
                
        //        if (bIsSelected)
        //        {
        //            CreditNumber = grdCredit.DataKeys[i].Value.ToString();

        //            int iResult = 0;
        //            if (grdCredit.Rows[i].Cells[6].Text.ToUpper() == "POR ENTREGAR"
        //                || grdCredit.Rows[i].Cells[6].Text.ToUpper() == "PENDIENTE")
        //            {
        //                decimal iRequestAmount = 0;
        //                iRequestAmount = grdCredit.Rows[i].Cells[2].Text == "" ? 0 : decimal.Parse(grdCredit.Rows[i].Cells[5].Text.Substring(1));
        //                using (TransactionScope scope = new TransactionScope())
        //                {
        //                    iResult = K_CREDITODal.ClassInstance.CancelCredit(CreditNumber, Session["UserName"].ToString());
        //                    iResult += CAT_PROGRAMADal.ClassInstance.IncreaseCurrentAmount(Global.PROGRAM, iRequestAmount);
        //                    scope.Complete();
        //                }
        //                // End

        //                if (iResult > 0)
        //                {
        //                    iSucess++;
        //                }
        //            }
        //        }
        //    }
        //    //if all had been updated successfully
        //    if (iCount > 0 && iCount == iSucess)
        //    {
        //        strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSSuccessToCancel") as string;
        //        ScriptManager.RegisterStartupScript(this, GetType(), "CancelSuccess", "alert('" + strMsg + "');", true);
        //    }
        //    else
        //    {
        //        strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSFailToCancel") as string;
        //        ScriptManager.RegisterStartupScript(this, GetType(), "CancelFailed", "alert('" + strMsg + "');", true);
        //    }
        //    #endregion

        //    //Refresh grid view data
        //    LoadGridViewData();
        //}

        protected void btn_Aceptar_Click(object sender, EventArgs e)
        {
            GridViewRow row = null;

            foreach (GridViewRow item in grdCredit.Rows)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }
            if (row == null) return;
            var textoLink = row.Cells[0].Controls[0] as HyperLink;
            if (textoLink == null) return;
            var credito = textoLink.Text;
            if (SubEstaciones.ClassInstance.EsSubestaciones(credito) == true && (((DropDownList)grdCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).SelectedValue == "8"))
            {
                Response.Redirect("CambiarRPU.aspx?Token=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(credito)));
            }

            else switch (((DropDownList)grdCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "2":
                    Response.Redirect("CapturaDatosSolicitud.aspx?Token=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(credito)));
                    break;
                case "10":
                    Response.Redirect("~/MRV/AdmonMRV.aspx?Token=" +
                                      Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(credito)));
                    break;
                case "9":
                {
                    var existeMotivo = Reactivacion.HayMotivosCancelacion(credito);
                    if (existeMotivo)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=CartaCancelarRechazar&CreditNumber=" + credito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(panel, typeof(Page), "No hay Motivos de Cancelación", "alert('No hay Motivos de Cancelación para esta Solicitud');", true);
                    }
                }
                    break;
            }
        }

        protected void ckbSelect_CheckedChanged1(object sender, EventArgs e)
        {
            modalPopup.OpenerElementID = null;
            btn_Aceptar.Enabled = true;
            var chk = (CheckBox)sender;
            var row = (GridViewRow)chk.Parent.Parent;

            var combo = (DropDownList)grdCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones");

            if (chk.Checked == false)
            {
                foreach (GridViewRow item in grdCredit.Rows)
                {
                    grdCredit.Rows[item.DataItemIndex].Enabled = true;

                    ((DropDownList)grdCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).Enabled = false;
                    ((DropDownList)grdCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).Items.Clear();
                    ((DropDownList)grdCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).Items.Insert(0, "Elige Opcion");
                    btn_Aceptar.Enabled = false;
                }
            }
            else
            {
                var idUsuario = ((US_USUARIOModel) Session["UserInfo"]).Id_Usuario;
                var textoLink = row.Cells[0].Controls[0] as HyperLink;
                if (textoLink != null)
                {
                    var credito = textoLink.Text;

                    var estatus = row.Cells[6].Text;
                    var cveEstatus = grdCredit.DataKeys[row.RowIndex][1].ToString();
                    var CveEstatus = Int32.Parse(cveEstatus);
                    var idRol = ((US_USUARIOModel)Session["UserInfo"]).Id_Rol;

                    foreach (GridViewRow item in grdCredit.Rows)
                    {
                        if (row.DataItemIndex == item.DataItemIndex)
                        {
                            grdCredit.Rows[item.DataItemIndex].Enabled = true;
                            ((DropDownList)grdCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).Enabled = true;
                            btn_Aceptar.Enabled = true;
                        }

                        else
                        {
                            grdCredit.Rows[item.DataItemIndex].Enabled = false;
                        }
                    }

                    var lista = ConsultaCredito.ObtenAcciones(CveEstatus, idRol);
                    var listaAccionesUsuario = ConsultaCredito.ObtenAccionesUsuario(CveEstatus, idUsuario);

                    if (lista.Count > 0)
                    {
                        if (listaAccionesUsuario.Count > 0)
                        {
                            foreach (var accionUsuario in listaAccionesUsuario)
                            {
                                if (lista.FindAll(me => me.ID_Acciones == accionUsuario.IdAccion).Count > 0)
                                {
                                    var accionRol = lista.Find(me => me.ID_Acciones == accionUsuario.IdAccion);

                                    if (!accionUsuario.EstatusAccion)
                                        lista.Remove(accionRol);
                                    else
                                    {
                                        var accion = new CAT_ACCIONES();
                                        accion.ID_Acciones = Convert.ToByte(accionUsuario.IdAccion);
                                        accion.Nombre_Accion = accionUsuario.NombreAccion;
                                        accion.Estatus = accionUsuario.EstatusAccion;
                                        accion.Nombre_Accion = accionUsuario.NombreAccion;
                                        lista.Add(accion);
                                    }

                                    if(accionUsuario.EstatusAccion == accionRol.Estatus)
                                        lista.Remove(accionRol);
                                }
                                else
                                {
                                    if (accionUsuario.EstatusAccion)
                                    {
                                        var accion = new CAT_ACCIONES();
                                        accion.ID_Acciones = Convert.ToByte(accionUsuario.IdAccion);
                                        accion.Nombre_Accion = accionUsuario.NombreAccion;
                                        accion.Estatus = accionUsuario.EstatusAccion;

                                        lista.Add(accion);
                                    }
                                }
                            }
                        }

                        if (SubEstaciones.ClassInstance.EsSubestaciones(credito))
                        {
                            if (CveEstatus == 2 &&
                                SubEstaciones.ClassInstance.HayRegistroDeNuevoRPUDist(credito) == false)
                            {
                                lista = lista;
                            }

                            else if (CveEstatus == 2 &&
                                     SubEstaciones.ClassInstance.HayRegistroDeNuevoRPUDist(credito) == true)
                            {
                                lista.RemoveAll(me => me.ID_Acciones == 8);
                            }

                            else
                            {
                                lista.RemoveAll(me => me.ID_Acciones == 8);
                            }
                        }
                        else
                        {
                            lista.RemoveAll(p => p.ID_Acciones == 8);
                        }

                        combo.DataSource = lista;
                        combo.DataValueField = "ID_Acciones";
                        combo.DataTextField = "Nombre_Accion";
                        combo.DataBind();

                        combo.Items.Insert(0, "Elegir Opcion");
                        combo.SelectedIndex = 0;
                    }
                    else
                    {
                        combo.DataSource = listaAccionesUsuario.FindAll(me => me.EstatusAccion);
                        combo.DataValueField = "IdAccion";
                        combo.DataTextField = "NombreAccion";
                        combo.DataBind();

                        combo.Items.Insert(0, "Elegir Opcion");
                        combo.SelectedIndex = 0;
                    }
                }               
            }
        }


        ////add by  @l3x//// 
        protected void LSB_Acciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = null;
            var user = (US_USUARIOModel)Session["UserInfo"];
            foreach (GridViewRow item in grdCredit.Rows)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }
            if (((DropDownList)grdCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).SelectedValue == "7")
            {

                modalPopup.OpenerElementID = btn_Aceptar.ClientID;
                DDLMotivo.DataSource = CAT_MotivosCancelRechaza.cat_motivos(user.Id_Rol, 7);
                DDLMotivo.DataTextField = "motivo";
                DDLMotivo.DataValueField = "id_Motivo";
                DDLMotivo.DataBind();

                DDLMotivo.Items.Insert(0, "Seleccione");
                DDLMotivo.SelectedIndex = 0;
            }
            else
            {
                modalPopup.OpenerElementID = null;
            }
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            int iCount = 0, iSucess = 0;
            bool bIsSelected;

            #region Check If Have Records Be Seleted

            for (var i = 0; i < grdCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)grdCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (bIsSelected)
                {
                    iCount++;
                }
            }
            #endregion

            #region Cancel The Selected Credits

            for (var i = 0; i < grdCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)grdCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (!bIsSelected) continue;
                var creditNumber = grdCredit.DataKeys[i].Value.ToString();

                if (grdCredit.Rows[i].Cells[6].Text.ToUpper() != "POR ENTREGAR" && grdCredit.Rows[i].Cells[6].Text.ToUpper() != "PENDIENTE") continue;
                var iResult = 0;
                var textoLink = grdCredit.Rows[i].Cells[0].Controls[0] as HyperLink;
                var credito = textoLink.Text;
                decimal iRequestAmount = 0;
                iRequestAmount = grdCredit.Rows[i].Cells[2].Text == "" ? 0 : decimal.Parse(grdCredit.Rows[i].Cells[5].Text.Substring(1));
                using (var scope = new TransactionScope())
                {
                    var cancelacion = LogicaNegocios.SolicitudCredito.SolicitudCreditoAcciones.Agregarcancel(CancelarRechazar());

                    if (cancelacion != null)
                    {
                        iResult = K_CREDITODal.ClassInstance.CancelCredit(creditNumber, Session["UserName"].ToString());
                        
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),Convert.ToInt16(Session["IdRolUserLogueado"]),Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO","CANCELADO", creditNumber,cancelacion.ID_MOTIVO.ToString(CultureInfo.InvariantCulture), cancelacion.DESCRIPCION_MOTIVO == "" ?  cancelacion.OBSERVACION : cancelacion.DESCRIPCION_MOTIVO, "", "");

                        if(iResult != 0)
                            LogicaNegocios.SolicitudCredito.SolicitudCreditoAcciones.IncrementarFonDisponibleEincentivo(iRequestAmount, credito);

                    }
                    scope.Complete();
                }
                // End

                if (iResult > 0)
                {
                    iSucess++;
                }
            }
            //if all had been updated successfully
            if (iCount > 0 && iCount == iSucess)
            {
              RadWindowManager1.RadAlert("La Solicitud  se ha Cancelado Correctamente", 300, 150, "Cancelar Rechazar", null);
            }
            else
            {
                RadWindowManager1.RadAlert("Ocurrió un error al Cancelar la Solicitud", 300, 150, "Cancelar Rechazar", null);
            }
            #endregion

            //Refresh grid view data
            LoadGridViewData();
        }

        private CANCELAR_RECHAZAR CancelarRechazar()
        {
            GridViewRow row = null;

            foreach (GridViewRow item in grdCredit.Rows)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }
            var textoLink = row.Cells[0].Controls[0] as HyperLink;
            var credito = textoLink.Text;

            var user = (US_USUARIOModel)Session["UserInfo"];
            var datos = new CANCELAR_RECHAZAR
            {
                No_Credito = credito,
                ID_MOTIVO = Convert.ToByte(DDLMotivo.SelectedValue),
                DESCRIPCION_MOTIVO = TexMoti.Text == "" ? null : TexMoti.Text,
                OBSERVACION = TexObserv.Text == "" ? null : TexObserv.Text,
                ADICIONADO_POR = user.Nombre_Usuario,
                FECHA_ADICION = DateTime.Now
            };

            return datos;
        }


        // add by Edu
        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            int proveedorId;
            string userType;

            if (Session["UserInfo"] == null) return;
            var user = (US_USUARIOModel)Session["UserInfo"];
            proveedorId = user.Id_Departamento;
            userType = user.Tipo_Usuario;

            if (new DatosMontos().ProveedorTecAdquisicion(proveedorId))
            {
                Response.Redirect("../Captcha/valida.aspx");
            }
            else
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AlertInform",
                        "alert('Usted no cuenta con tecnología de adquisición asignadas');",
                        true);
        }
    }
}
