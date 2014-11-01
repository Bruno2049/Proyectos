using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.ModuloCentral;
using System.Globalization;

namespace PAEEEM.CentralModule
{
    public partial class AutorizacionProveedores : System.Web.UI.Page
    {
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
                    
                    Session["CurrentRegionalAuthorization"] = 0;
                    Session["CurrentTipoSupplierAuthorization"] = 0;
                    Session["CurrentZoneAuthorization"] = 0;
                    Session["CurrentStatusSupplierAuthorization"] = 0;

                    carga_cmbRegional();
                    carga_cmbZona();
                    carga_cmbEstatus();
//                    carga_cmbTipo();

                    revisaUsuario();

                }
                catch (Exception)
                {
                }
            }

            Check_Motivos_asp.Attributes.Add("onchange", "ValidarCbxMotivo()");
            txtObservaciones.Attributes.Add("onkeyup", "ValidarObservaciones(this, 200);");
            txt_motivosReactivar.Attributes.Add("onkeyup", "ValidarObservacionesReactivar(this, 200);");
  
           
        }

        private void revisaUsuario()
        {
            int id_rol = Convert.ToInt32((Session["IdRolUserLogueado"]));
            if (id_rol == 6)
            {
                carga_cmbTipo(true);
                rad_cmbRegional.SelectedValue = id_rol.ToString();
                rad_cmbRegional.Enabled = false;
                carga_cmbZona();
                bindRadGrid();
            }
            else
            {
                carga_cmbTipo(false);
                bindRadGrid();
                
            }
            

        }

        private void carga_cmbRegional()
        {
            var dsRegional = AccesoDatos.Catalogos.Region.CatRegion();
            if (dsRegional != null)
            {
                rad_cmbRegional.DataSource = dsRegional;
                rad_cmbRegional.DataValueField = "Cve_Region";
                rad_cmbRegional.DataTextField = "Dx_Nombre_Region";
                rad_cmbRegional.DataBind();
            }
            rad_cmbRegional.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem(""));
            rad_cmbRegional.SelectedIndex = 0;
        }

        private void carga_cmbZona()
        {
            var dsZone = string.IsNullOrEmpty(rad_cmbRegional.SelectedValue) ? AccesoDatos.Catalogos.cat_autorizacionProveedor.catZonas() : AccesoDatos.Catalogos.cat_autorizacionProveedor.catZonasXidRegion(Convert.ToInt32(rad_cmbRegional.SelectedValue));
            if (dsZone != null )
            {
                rad_cmbZona.DataSource = dsZone;
                rad_cmbZona.DataValueField = "Cve_Zona";
                rad_cmbZona.DataTextField = "Dx_Nombre_Zona";
                rad_cmbZona.DataBind();
            }
            rad_cmbZona.Items.Insert(0,new Telerik.Web.UI.RadComboBoxItem(""));
            rad_cmbZona.SelectedIndex = 0;
        }

        private void carga_cmbTipo(bool ban)
        {
            if (ban != true)
            {
                rad_cmbTipo.Items.Insert(0, new RadComboBoxItem("", ""));
                rad_cmbTipo.Items.Insert(1, new RadComboBoxItem("Matriz", "M"));
                rad_cmbTipo.Items.Insert(2, new RadComboBoxItem("Sucursal Física", "SB_F"));
                rad_cmbTipo.Items.Insert(3, new RadComboBoxItem("Sucursal Virtual", "SB_V"));
                rad_cmbTipo.SelectedIndex = 0;
            }
            else
            {
                rad_cmbTipo.Items.Insert(0, new RadComboBoxItem("", "S"));              
                rad_cmbTipo.Items.Insert(1, new RadComboBoxItem("Sucursal Física", "SB_F"));
                rad_cmbTipo.Items.Insert(2, new RadComboBoxItem("Sucursal Virtual", "SB_V"));
                rad_cmbTipo.SelectedIndex = 0;
            }
        }

        private void carga_cmbEstatus()
        {
            var dsEstatus = AccesoDatos.Catalogos.cat_autorizacionProveedor.catEstatus();
            if (dsEstatus != null)
            {
                rad_cmbEstatus.DataSource = dsEstatus;
                rad_cmbEstatus.DataValueField = "Cve_Estatus_Proveedor";
                rad_cmbEstatus.DataTextField = "Dx_Estatus_Proveedor";
                rad_cmbEstatus.DataBind();

                rad_cmbEstatus.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem(""));
                rad_cmbEstatus.SelectedIndex = 0;

                rad_cmbEstatus.Items.Remove(rad_cmbEstatus.Items.Count - 1);
                
            }
        }

        private void bindRadGrid()
        {
            try
            {
                var tipoOfSupplier = "";
                int pageCount;

                int id_rol = Convert.ToInt32((Session["IdRolUserLogueado"]));
                if (id_rol == 6)
                {
                    tipoOfSupplier = (rad_cmbTipo.SelectedIndex == 0 || rad_cmbTipo.SelectedIndex == -1) ? "S" : rad_cmbTipo.SelectedValue;
                }
                else
                {
                    tipoOfSupplier = (rad_cmbTipo.SelectedIndex == 0 || rad_cmbTipo.SelectedIndex == -1) ? "" : rad_cmbTipo.SelectedValue;
                }            
        
                var zone = (rad_cmbZona.SelectedIndex == 0 || rad_cmbZona.SelectedIndex == -1) ? "" : rad_cmbZona.SelectedValue;
                var estatus = (rad_cmbEstatus.SelectedIndex == 0 || rad_cmbEstatus.SelectedIndex == -1) ? "" : rad_cmbEstatus.SelectedValue;
                var regional = (rad_cmbRegional.SelectedIndex == 0 || rad_cmbRegional.SelectedIndex == -1) ? "" : rad_cmbRegional.SelectedValue;

                var supplier = CAT_PROVEEDORDal.ClassInstance.Get_Provider_ForAuthorization(zone, tipoOfSupplier, estatus, regional, 1, 99000, out pageCount);

                if (supplier != null)
                {
                    if (supplier.Rows.Count == 0)
                    {
                        rg_Distribuidor.DataSource = null;
                        rg_Distribuidor.DataBind();                       
                    }
                  
                    rg_Distribuidor.DataSource = supplier;
                    rg_Distribuidor.DataBind();
                }
                else
                {
                    rg_Distribuidor.DataSource = null;
                }

             
            }
            catch (Exception ex)
            {
                RWM_Ventana.RadAlert("Aviso: \n" + ex.Message, 300, 100, "Atencion", null);
            }
        }

        protected void ckbSelect_OnCheckedChanged(object sender, EventArgs e)
        {
            popupReactivar.OpenerElementID = null;
            modalPopupCancelar.OpenerElementID = null;
            var chk = (CheckBox)sender;
            var row = (GridDataItem)chk.Parent.Parent;
            int rol = Convert.ToInt16(Session["IdRolUserLogueado"]);

            var combo = (RadComboBox)rg_Distribuidor.Items[row.ItemIndex].FindControl("LSB_Acciones");

            if (chk.Checked == false)
            {
                foreach (GridDataItem item in rg_Distribuidor.Items)
                {
                    rg_Distribuidor.Items[item.ItemIndex].Enabled = true;

                    ((RadComboBox)rg_Distribuidor.Items[row.ItemIndex].Cells[5].FindControl("LSB_Acciones")).Enabled = false;
                    ((RadComboBox)rg_Distribuidor.Items[row.ItemIndex].Cells[5].FindControl("LSB_Acciones")).Items.Clear();
                    ((RadComboBox)rg_Distribuidor.Items[row.ItemIndex].Cells[5].FindControl("LSB_Acciones")).Items.Insert(0,new RadComboBoxItem("Elige Opcion"));
                    rad_btnAceptar.Enabled = false;
                }
            }
            else
            {
                foreach (GridDataItem item in rg_Distribuidor.Items)
                {
                    if (row.ItemIndex == item.ItemIndex)
                    {
                        rg_Distribuidor.Items[item.ItemIndex].Enabled = true;
                        ((RadComboBox)rg_Distribuidor.Items[row.ItemIndex].Cells[5].FindControl("LSB_Acciones")).Enabled = true;
                        rad_btnAceptar.Enabled = true;
                    }

                    else
                    {
                        rg_Distribuidor.Items[item.ItemIndex].Enabled = false;
                    }
                }


                var estatusProveedor = (rg_Distribuidor.Items[row.ItemIndex].Cells[4].FindControl("lblEstatus") as Label).Text;

                List<Entidades.AdminUsuarios.AccionUsuario> listaAcciones = null;

                if (estatusProveedor == ProviderStatus.PENDIENTE.ToString())
                {
                   listaAcciones  = new AccionesMonitor_Rol().accionesEstatus((int)(ProviderStatus.PENDIENTE));
                }
                else if (estatusProveedor == ProviderStatus.ACTIVO.ToString())
                {
                    listaAcciones = new AccionesMonitor_Rol().accionesEstatus((int)(ProviderStatus.ACTIVO));
                }
                else if (estatusProveedor == ProviderStatus.INACTIVO.ToString())
                {
                    listaAcciones = new AccionesMonitor_Rol().accionesEstatus((int)(ProviderStatus.INACTIVO));
                }
                
                
              //--  //Modificar: Se Creo la tabla ACCION_ESTATUS_PROVEEDOR en la cual se debe de definir las acciones que se pueden mostrar dependiendo
                // del estatus en el que se encuentre el proveedor. Esta tabla tendra relacion con la tabla  CAT_ACCCIONES campo ID_ACCIONES
                
                //var lista = new AccionesMonitor_Rol().AccionesMonitorRol(3, rol);
                //if (lista.Count > 0)
                //{
                //    var estatus = (rg_Distribuidor.Items[row.ItemIndex].Cells[4].FindControl("lblEstatus") as Label).Text;
                //    if (estatus.ToUpper() == GlobalVar.PENDING_SUPPLIER)//"PENDIENTE")
                //    {
                //        lista.RemoveAll(me => me.IdAccion == 18 || me.IdAccion == 19 || me.IdAccion == 23);
                //    }
                //    else if (estatus.ToUpper() == GlobalVar.ACTIVE_SUPPLIER)// "ACTIVO")
                //    {
                //        lista.RemoveAll(me => me.IdAccion == 17 || me.IdAccion == 19);
                //    }
                //    else if (estatus.ToUpper() == GlobalVar.INACTIVE_SUPPLIER)// "INACTIVO")
                //    {
                //        lista.RemoveAll(me => me.IdAccion == 17 || me.IdAccion == 18 || me.IdAccion == 23);
                //    }
                //    else if (estatus.ToUpper() == GlobalVar.CANCELED_SUPPLIER)
                //    {
                //        lista.RemoveAll(l => l.IdAccion != 0);
                //    }

                    combo.DataSource = listaAcciones;
                    combo.DataValueField = "IdAccion";
                    combo.DataTextField = "NombreAccion";
                    combo.DataBind();
                    combo.Items.Insert(0, new RadComboBoxItem("Elegir Opcion"));
                    combo.SelectedIndex = 0;
                
            }
        }

        protected void rg_Distribuidor_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            bindRadGrid();
        }

        protected void rg_Distribuidor_DataBound(object sender, EventArgs e)
        {
           
            for (var i = 0; i < rg_Distribuidor.Items.Count; i++)
            {
                var key = rg_Distribuidor.Items[i];
                var dataKey1 = rg_Distribuidor.Items[i];
                if (dataKey1 != null)
                {
                    var estatu = (rg_Distribuidor.Items[i].Cells[4].FindControl("lblEstatus") as Label).Text;
                    var ckbSelecto = rg_Distribuidor.Items[i].FindControl("ckbSelect") as CheckBox;
                    if (estatu == "CANCELADO")
                    {
                        if (ckbSelecto != null) ckbSelecto.Visible = false;
                    }
                }
            }
        }

        protected void rad_cmbRegional_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
             if (!IsPostBack) return;       
            var zona = rad_cmbZona.SelectedValue;
            carga_cmbZona();
            try { rad_cmbZona.SelectedValue = zona; } 
            catch (Exception)
            { }                  
            Session["CurrentZoneAuthorization"] = rad_cmbZona.SelectedIndex;

            bindRadGrid();
            Session["CurrentRegionalAuthorization"] = rad_cmbZona.SelectedIndex;
           
        }

        protected void rad_cmbZona_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!IsPostBack) return;
            bindRadGrid();
            Session["CurrentZoneAuthorization"] = rad_cmbZona.SelectedIndex;
        }

        protected void rad_cmbTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!IsPostBack) return;
            bindRadGrid();
            Session["CurrentTipoSupplierAuthorization"] = rad_cmbTipo.SelectedIndex;
        }

        protected void rad_cmbEstatus_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!IsPostBack) return;
            bindRadGrid();
            Session["CurrentStatusSupplierAuthorization"] = rad_cmbEstatus.SelectedIndex;
            
        }

        protected void rad_btnAceptar_Click(object sender, EventArgs e)
        {

            

            GridDataItem row = null;
            

            foreach (GridDataItem item in rg_Distribuidor.Items)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }


            switch (((RadComboBox)rg_Distribuidor.Items[row.ItemIndex].Cells[4].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "17":
                    RWM_Ventana.RadConfirm("Confirmar Activar el Proveedor Seleccionado.", "confirmCallBackFn", 300, 100, null, "Activar");
                    break;
                //case "18":
                //    RWM_Ventana.RadConfirm("Confirmar Inactivar el Proveedor Seleccionado.", "confirmCallBackFn", 300, 100, null, "Inactivar");
                //    break;
                //case "19":
                //    RWM_Ventana.RadConfirm("Confirmar Reactivar Proveedor Seleccionado.", "confirmCallBackFn", 300, 100, null, "Reactivar");
                //    break;
                //case "23":
                //    RWM_Ventana.RadConfirm("Confirmar Cancelar Definitivamente el Proveedor Seleccionado.", "confirmCallBackFn", 300, 100, null, "Cancelar");
                //    break;
            }

       
        }

        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            
            GridDataItem row = null;

            foreach (GridDataItem item in rg_Distribuidor.Items)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            switch (((RadComboBox)rg_Distribuidor.Items[row.ItemIndex].Cells[4].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "17":
                    Activar();
                    break;
                //case "18":
                //    Desactivar();
                //    break;
                //case "19":
                //    Reactivar();
                //    break;
                //case "23":
                //    Cancelar();
                //    break;

            }
        }


        #region button action

        protected void Activar()
        {
            string supplierList;
            string supplierBranch;
            
            GetSelectSuppliserIdList(out supplierList, out supplierBranch, (int)ProviderStatus.PENDIENTE);
            char[] delimiterChars = { ',' };
            if (supplierList != "" || supplierBranch != "")
            {
                if (supplierList != "")
                {
                    var idAfectados = supplierList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfoProv = AccesoDatos.Log.CatProveedor.ObtienePorId(Convert.ToInt32(id));
                        var result = ActualizarEstatus(id, (int)ProviderStatus.ACTIVO);                      
                        if (result !=false)
                        {                          
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "ACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "Motivos??", 
                                "", "Cve_Estatus_Proveedor: " + originalInfoProv.Cve_Estatus_Proveedor, 
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.ACTIVO));
                        }
                    }
                }
                if (supplierBranch != "")
                {
                    var idAfectados = supplierBranch.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var infoOriginalProvBranch = AccesoDatos.Catalogos.cat_autorizacionProveedor.obtieneSucursal(Convert.ToInt32(id));
                        var result = actualizarEstatusSucursalB(id, (int)ProviderStatus.ACTIVO);
                        if (result !=false)
                        {
                          
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "ACTIVAR", id.ToString(CultureInfo.InvariantCulture),
                                "Motivos??",
                                "", "Cve_Estatus_Proveedor: " + infoOriginalProvBranch.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.ACTIVO));
                        }
                    }
                }
            }
            else
            {
                ClearGridViewChecked((int)ProviderStatus.PENDIENTE);                
                RWM_Ventana.RadAlert("Por favor, seleccione Proveedores en estatus PENDIENTE", 300, 100, "Atencion:", null);
            }
            bindRadGrid();
        }
      
        protected void Desactivar()
        {
            string supplierList;
            string supplierBranch;
            char[] delimiterChars = { ',' };
            GetSelectSuppliserIdList(out supplierList, out supplierBranch, (int)ProviderStatus.ACTIVO);
            if (supplierList != "" || supplierBranch != "")
            {
                if (supplierList != "")
                {
                    var idAfectados = supplierList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfoProv = AccesoDatos.Log.CatProveedor.ObtienePorId(Convert.ToInt32(id));
                        var result = ActualizarEstatus(id, (int)ProviderStatus.INACTIVO);
                        if (result != false)
                        {                           
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "INACTIVAR", id.ToString(CultureInfo.InvariantCulture),lismotivos,lisObservaciones, "Cve_Estatus_Proveedor: " + originalInfoProv.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.INACTIVO));
                        }
                    }

                }
                if (supplierBranch != "")
                {
                    var idAfectados = supplierBranch.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var infoOriginalProvBranch = AccesoDatos.Catalogos.cat_autorizacionProveedor.obtieneSucursal(Convert.ToInt32(id));
                       var result = actualizarEstatusSucursalB(id, (int)ProviderStatus.INACTIVO);
                        if (result !=false)
                        {
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "INACTIVAR", id.ToString(CultureInfo.InvariantCulture), lismotivos, lisObservaciones, "Cve_Estatus_Proveedor: " + infoOriginalProvBranch.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.INACTIVO));
                        }
                    }
                }
            }
            else
            {
                ClearGridViewChecked((int)ProviderStatus.ACTIVO);
               
                RWM_Ventana.RadAlert("Por favor, seleccione Proveedores en estatus ACTIVO", 300, 100, "Atencion:", null);
            }
            bindRadGrid();
        }

        protected void Reactivar()
        {
            string supplierList;
            string supplierBranch;
            
            char[] delimiterChars = { ',' };
            GetSelectSuppliserIdList(out supplierList, out supplierBranch, (int)ProviderStatus.INACTIVO);
            if (supplierList != "" || supplierBranch != "")
            {
                if (supplierList != "")
                {
                    var idAfectados = supplierList.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var originalInfoProv = AccesoDatos.Log.CatProveedor.ObtienePorId(Convert.ToInt32(id)); 
                        var result = ActualizarEstatus(id, (int)ProviderStatus.ACTIVO);
                        if (result != false)
                        {
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "REACTIVAR", id.ToString(CultureInfo.InvariantCulture),lisMotivosReactivar, "", "Cve_Estatus_Proveedor: " + originalInfoProv.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.ACTIVO));
                        }
                    }
                }
                if (supplierBranch != "")
                {
                    var idAfectados = supplierBranch.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var infoOriginalProvBranch = AccesoDatos.Catalogos.cat_autorizacionProveedor.obtieneSucursal(Convert.ToInt32(id));
                        var result = actualizarEstatusSucursalB(id, (int)ProviderStatus.ACTIVO);
                        if (result != false)
                        {
                            
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "REACTIVAR", id.ToString(CultureInfo.InvariantCulture), lisMotivosReactivar,"",
                                "Cve_Estatus_Proveedor: " + infoOriginalProvBranch.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.ACTIVO));
                        }
                    }
                }
            }
            else
            {
                ClearGridViewChecked((int)ProviderStatus.INACTIVO);
                RWM_Ventana.RadAlert("Por favor, seleccione Proveedores en estatus INACTIVO", 300, 100, "Atencion:", null);
                
            }

          //  if (result <= 0) return;
            bindRadGrid();
        }

        protected void Cancelar()
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
                        var result = ActualizarEstatus(id, (int)ProviderStatus.CANCELADO);
                        if (result != false)
                        {  
                           
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "CANCELADO", id.ToString(CultureInfo.InvariantCulture),lismotivos, lisObservaciones, "Cve_Estatus_Proveedor: " + originalInfoProv.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.CANCELADO));
                        }
                    }
                }
                if (supplierBranch != "")
                {
                    var idAfectados = supplierBranch.Split(delimiterChars);
                    foreach (var id in idAfectados)
                    {
                        var infoOriginalProvBranch = AccesoDatos.Catalogos.cat_autorizacionProveedor.obtieneSucursal(Convert.ToInt32(id));
                        var result = actualizarEstatusSucursalB(id, (int)ProviderStatus.CANCELADO);
                        if (result != false)
                        {
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "EMPRESAS", "CANCELADO", id.ToString(CultureInfo.InvariantCulture), lismotivos, lisObservaciones, "Cve_Estatus_Proveedor: " + infoOriginalProvBranch.Cve_Estatus_Proveedor,
                                "Cve_Estatus_Proveedor: " + Convert.ToString((int)ProviderStatus.CANCELADO));
                        }
                    }
                }
            }
            else
            {
                RWM_Ventana.RadAlert("Por favor, seleccione un proveedor", 300, 100, "Atencion:", null);
            }
            bindRadGrid();
        }

        private void ClearGridViewChecked(int status)
        {
            for (var i = 0; i < rg_Distribuidor.Items.Count; i++)
            {
                var ckbSelect = rg_Distribuidor.Items[i].FindControl("ckbSelect") as CheckBox;
                var dataKey = rg_Distribuidor.MasterTableView.DataKeyNames[i];
                if (dataKey != null && (ckbSelect != null && (ckbSelect.Checked && int.Parse(dataKey[1].ToString()) != status)))
                {
                    ckbSelect.Checked = false;
                }
            }
        }

        private void GetSelectSuppliserIdList(out string supplierList, out string branchList, int status)
        {
            supplierList = "";
            branchList = "";
            for (var i = 0; i < rg_Distribuidor.Items.Count; i++)
            {
                var ckbSelect = rg_Distribuidor.Items[i].FindControl("ckbSelect") as CheckBox;
                if (status == 0)
                {
                    if (ckbSelect != null && !ckbSelect.Checked) continue;
                    var dataKey = rg_Distribuidor.MasterTableView.DataKeyValues[i];
                    if (dataKey != null && dataKey["Type"].ToString() == "M")
                    {
                        var key = rg_Distribuidor.MasterTableView.DataKeyValues[i];
                        if (key != null)
                            supplierList += key["ID"] + ",";
                    }
                    else
                    {
                        var key = rg_Distribuidor.MasterTableView.DataKeyValues[i];
                        if (key != null) branchList += key["ID"] + ",";
                    }
                }
                else
                {
                    var dataKey = rg_Distribuidor.MasterTableView.DataKeyValues[i];
                    if (dataKey != null && (ckbSelect != null && (!ckbSelect.Checked || int.Parse(dataKey["Cve_Estatus_Proveedor"].ToString()) != status))) continue;
                    var key = rg_Distribuidor.MasterTableView.DataKeyValues[i];
                    if (key != null && key["Type"].ToString() == "M")
                    {
                        var dataKey1 = rg_Distribuidor.MasterTableView.DataKeyValues[i];
                        if (dataKey1 != null)
                            supplierList += dataKey1["ID"] + ",";
                    }
                    else
                    {
                        var dataKey1 = rg_Distribuidor.MasterTableView.DataKeyValues[i];
                        if (dataKey1 != null) branchList += dataKey1["ID"] + ",";
                    }
                }
            }
            supplierList = supplierList.TrimEnd(new[] { ',' });
            branchList = branchList.TrimEnd(new[] { ',' });
        }

        

        #endregion
        
        //Actualizar

        public bool ActualizarEstatus(string SupplierID, int Status)
        {
            bool Result = false;
            try
            {
                var infoProveedor = AccesoDatos.Catalogos.cat_autorizacionProveedor.obtieneParaActualizar(Convert.ToInt32(SupplierID));
                var usuarios = AccesoDatos.Catalogos.cat_autorizacionProveedor.obtieneUsuarios(int.Parse(SupplierID), "S");

                infoProveedor.Cve_Estatus_Proveedor = Status;

                var sucursales = AccesoDatos.Catalogos.cat_autorizacionProveedor.listaSucursales(infoProveedor.Id_Proveedor);             

                Result = new AccesoDatos.Catalogos.cat_autorizacionProveedor().ActalizaMatriz(infoProveedor);

                //Actualiza Usuarios
                for (int i = 0; i < usuarios.Count; i++)
                {
                    if (Status == (int)ProviderStatus.INACTIVO)
                    {
                        usuarios[i].Estatus = "I";

                    }
                    else if (Status == (int)ProviderStatus.CANCELADO)
                    {
                        usuarios[i].Estatus = "I";
                    }
                    else if (Status == (int)ProviderStatus.ACTIVO)
                    {
                        usuarios[i].Estatus = "A";
                    }

                    var r = new AccesoDatos.Catalogos.cat_autorizacionProveedor().actualizaUsuarios(usuarios[i]);
                }
                //User

                //Actualiza Sucursales de la Matriz

                for (int i = 0; i < sucursales.Count; i++)
                {
                    sucursales[i].Cve_Estatus_Proveedor = Status;

                    var r = new AccesoDatos.Catalogos.cat_autorizacionProveedor().ActalizaSucursalBranch(sucursales[i]);
                }
                ///
                              

            }
            catch (Exception ex)
            {
                RWM_Ventana.RadAlert("Atencion: \n" + ex.Message, 300, 100, "Atencion", null);

            }
            return Result;
        }

        public bool actualizarEstatusSucursalB(string BranchID, int Status)
        {
            bool Result = false;
            try
            {
                var sucursal = AccesoDatos.Catalogos.cat_autorizacionProveedor.obtieneSucursal(int.Parse(BranchID));
                var usuarios = AccesoDatos.Catalogos.cat_autorizacionProveedor.obtieneUsuarios(int.Parse(BranchID), "S_B");

                var sucVirtual = AccesoDatos.Catalogos.cat_autorizacionProveedor.listaSucursalesVirtuales(sucursal.Id_Branch);

                sucursal.Cve_Estatus_Proveedor = Status;


                //Usuarios Sucursales
                for (int i = 0; i < usuarios.Count; i++)
                {
                    if (Status == (int)ProviderStatus.INACTIVO)
                    {
                        usuarios[i].Estatus = "I";

                    }
                    else if (Status == (int)ProviderStatus.CANCELADO)
                    {
                        usuarios[i].Estatus = "I";
                    }
                    else if (Status == (int)ProviderStatus.ACTIVO)
                    {
                        usuarios[i].Estatus = "A";
                    }

                    var r = new AccesoDatos.Catalogos.cat_autorizacionProveedor().actualizaUsuarios(usuarios[i]);
                }
                ///

                //Actualiza Sucursales Virtuales dependientes de la Sucursal Fisica

                for (int i = 0; i < sucVirtual.Count; i++)
                {
                    sucVirtual[i].Cve_Estatus_Proveedor = Status;

                    var r = new AccesoDatos.Catalogos.cat_autorizacionProveedor().ActalizaSucursalBranch(sucVirtual[i]);
                }
                ///


                Result = new AccesoDatos.Catalogos.cat_autorizacionProveedor().ActalizaSucursalBranch(sucursal);
            }
            catch (Exception ex)
            {
                RWM_Ventana.RadAlert("Atencion: \n" + ex.Message, 300, 100, "Atencion", null);
            }
            return Result;
        }

       

        //ventanaModal
        protected void LSB_Acciones_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            popupReactivar.OpenerElementID = null;
            modalPopupCancelar.OpenerElementID = null;
            txt_OTROS.Text = "";
            txtObservaciones.Text = "";
            Check_Motivos_asp.Items.Clear();
            cargaMotivos();
                        var combo = (RadComboBox)sender;
            var row = combo.NamingContainer as GridEditableItem;
            switch (
                ((RadComboBox)rg_Distribuidor.MasterTableView.Items[row.ItemIndex].Cells[5].FindControl("LSB_Acciones"))
                    .SelectedValue)
            {
                case "17": //Activar No modal
                    modalPopupCancelar.OpenerElementID = null;
                    break;
                case "18": //inactivar
                    lbl_MODAL_Encabezado.Text = "¿Está seguro de Inactivar el Proveedor?";
                    lbl_Modal_TituloMenor.Text = "Seleccione el o los motivo(s) por lo(s) cual(es) se esta Inactivando el Proveedor";
                 
                    modalPopupCancelar.OpenerElementID = rad_btnAceptar.ClientID;
                    break;
                case "19"://Reactivar -->Modal
                    popupReactivar.OpenerElementID = rad_btnAceptar.ClientID;
                    break;
                case "23"://Cancelar
                    lbl_MODAL_Encabezado.Text = "¿Está seguro de realizar la Cancelación del Proveedor, una vez Cancelado no podrá activarlo nuevamente?";
                    lbl_Modal_TituloMenor.Text = "Seleccione el o los motivo(s) por lo(s) cual(es) se esta Cancelando el Proveedor";
                
                    modalPopupCancelar.OpenerElementID = rad_btnAceptar.ClientID;
                    break;
                
                
            }

                   }

        protected void cargaMotivos()
        {
           
            
            var motivosCanc = AccesoDatos.Catalogos.cat_autorizacionProveedor.motivosCancelacion();

            Check_Motivos_asp.DataSource = motivosCanc;
            Check_Motivos_asp.DataTextField = "DESCRIPCION";
            Check_Motivos_asp.DataValueField = "ID_MOTIVO";
            Check_Motivos_asp.DataBind();

            

        }

        //
        public string lismotivos = "";
        public string lisObservaciones = "";

        protected void btn_AceptarPOP_Click(object sender, EventArgs e)
        {
          //txtObservaciones
            lisObservaciones = txtObservaciones.Text.ToUpper() ;
            for(int i=0;i<Check_Motivos_asp.Items.Count;i++)
            {
                if (Check_Motivos_asp.Items[i].Selected)
                {
                    if (Check_Motivos_asp.Items[i].Text != @"OTRO")
                    {
                        lismotivos += Check_Motivos_asp.Items[i].Text.ToUpper() +" ";
                    }
                    
                }

            }

            if (Check_Motivos_asp.Items[22].Selected)
            {
                lismotivos += txt_OTROS.Text.ToUpper();
            }

       

            GridDataItem row = null;

            foreach (GridDataItem item in rg_Distribuidor.Items)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            switch (((RadComboBox)rg_Distribuidor.Items[row.ItemIndex].Cells[4].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "17":
                    Activar();
                    break;
                case "18":
                    Desactivar();
                    break;
                case "19":
                    Reactivar();
                    break;
                case "23":
                    Cancelar();
                    break;

            }
        }

        //
        public string lisMotivosReactivar = "";
        protected void btnFinalizarReactivar_Click(object sender, EventArgs e)
        {
            lisMotivosReactivar = txt_motivosReactivar.Text.ToUpper(); ;

            //

            GridDataItem row = null;

            foreach (GridDataItem item in rg_Distribuidor.Items)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            switch (((RadComboBox)rg_Distribuidor.Items[row.ItemIndex].Cells[4].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "17":
                    Activar();
                    break;
                case "18":
                    Desactivar();
                    break;
                case "19":
                    Reactivar();
                    break;
                case "23":
                    Cancelar();
                    break;

            }
        }
                  

    }
}