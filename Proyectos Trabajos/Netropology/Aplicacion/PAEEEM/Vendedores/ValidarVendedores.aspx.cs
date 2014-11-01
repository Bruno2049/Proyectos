using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Vendedores;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.AdminUsuarios;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.ModuloCentral;
using PAEEEM.LogicaNegocios.Vendedores;
using Telerik.Web.UI;

namespace PAEEEM.Vendedores
{
    public partial class ValidarVendedores : System.Web.UI.Page
    {
        #region Variables
        private const int DefaultPasswordLength = 8;

        public int Filter
        {
            set { Session["Filter"] = value; }
            get { return (int)Session["Filter"]; }
        }

        public int RoleType
        {
            set { Session["RoleType"] = value; }
            get { return (int)Session["RoleType"]; }
        }

        private TempImagen foto
        {
            get
            {
                return Session["ImagenTemp"] == null
                    ? new TempImagen()
                    : Session["ImagenTemp"] as TempImagen;
            }
            set { Session["ImagenTemp"] = value; }
        }

        private List<TempImagen> fotos
        {
            get
            {
                return Session["ImagenesTemp"] == null
                    ? new List<TempImagen>()
                    : (List<TempImagen>)Session["ImagenesTemp"];
            }
            set { Session["ImagenesTemp"] = value; }
        }

        private List<DatosVendedoresRegZon> vendedores
        {
            get
            {
                return Session["VENDEDORES"] == null
                    ? new List<DatosVendedoresRegZon>()
                    : (List<DatosVendedoresRegZon>)Session["VENDEDORES"];
            }
            set { Session["VENDEDORES"] = value; }
        }

        public string Curp { set; get; }
        #endregion

        #region Metodos
        private void CatEstatus()
        {
            cmbEstatus.DataSource = new ValVendedores().ObtenerEstatusVendedores();
            cmbEstatus.DataTextField = "Descripcion";
            cmbEstatus.DataValueField = "Cve_Estatus_Vendedor";
            cmbEstatus.DataBind();
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Seleccione", "0"));
            cmbEstatus.SelectedIndex = 0;
        }

        private void CatRegionZona()
        {
            if (Session["UserInfo"] != null)
            {
                Filter = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                RoleType = ((US_USUARIOModel)Session["UserInfo"]).Id_Rol;
            }

            if (Filter >= 0)
            {
                if (RoleType == (int)UserRole.ZONE)
                {
                    //zona
                    var zon = new ValVendedores().catZona(Filter);
                    int reg = (int)zon.FirstOrDefault().Cve_Region;
                    cmbRegion.DataSource = new ValVendedores().CatRegion(reg);
                    cmbRegion.DataTextField = "Dx_Nombre_Region";
                    cmbRegion.DataValueField = "Cve_Region";
                    cmbRegion.DataBind();
                    cmbRegion.SelectedIndex = 0;

                    cmbZona.DataSource = zon;
                    cmbZona.DataTextField = "Dx_Nombre_Zona";
                    cmbZona.DataValueField = "Cve_Zona";
                    cmbZona.DataBind();
                    cmbZona.SelectedIndex = 0;
                }
                else if (RoleType == (int)UserRole.REGIONAL)
                {
                    //region
                    cmbRegion.DataSource = new ValVendedores().CatRegion(Filter);
                    cmbRegion.DataTextField = "Dx_Nombre_Region";
                    cmbRegion.DataValueField = "Cve_Region";
                    cmbRegion.DataBind();
                    cmbRegion.SelectedIndex = 0;

                    cmbZona.DataSource = new ValVendedores().CatZonaxIdRegion(int.Parse(cmbRegion.SelectedValue));
                    cmbZona.DataTextField = "Dx_Nombre_Zona";
                    cmbZona.DataValueField = "Cve_Zona";
                    cmbZona.DataBind();
                    cmbZona.Items.Insert(0, new RadComboBoxItem("Seleccione", "0"));
                    cmbZona.SelectedIndex = 0;

                }
            }
        }

        private void cargarImagenes()
        {
            foreach (GridDataItem VARIABLE in rgVendedores.MasterTableView.Items)
            {
                ((ImageButton)VARIABLE.FindControl("verImagen")).Attributes.Add("OnClick", "poponload('" + VARIABLE.Cells[8].Text + "')");
            }

        }

        private void CargarGrid()
        {
            fotos = new List<TempImagen>();
            vendedores= new List<DatosVendedoresRegZon>();
            string curp = txtCURP.Text;
            string nombre = txtNombre.Text;
            int estatus = int.Parse(cmbEstatus.SelectedValue);
            int region = int.Parse(cmbRegion.SelectedValue);
            int zona = int.Parse(cmbZona.SelectedValue);
            string distNC = txtDistNC.Text;
            string distRS = txtDistRS.Text;
            /*var vendedores*/ vendedores = new ValVendedores().DatosVendedores(curp, nombre, estatus, region, zona, distRS, distNC);
            foreach (var v in vendedores)
            {
                var temp = new TempImagen()
                {
                    CURP = v.Curp,
                    Imagen = v.Archivo
                };
                fotos.Add(temp);
            }
            rgVendedores.DataSource = vendedores;
            rgVendedores.DataBind();
            //cargarImagenes();
        }

        private void ActivarVendedor()
        {
            GridEditableItem row = null;

            foreach (GridEditableItem item in rgVendedores.MasterTableView.Items)
            {
                //if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                if (((CheckBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[19].FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            int idVendedor = int.Parse(rgVendedores.MasterTableView.Items[row.ItemIndex]["IdVendedor"].Text);

            var InfoGeneral = new ValVendedores().ObtienePorId(idVendedor);
            InfoGeneral.Cve_Estatus_Vendedor = 2;
            if (new ValVendedores().ActualizarVendedor(InfoGeneral))
            {
                RadWindowManager1.RadAlert(
                    "El vendedor se ha activado correctamente.",
                    300, 100, "Activado", null);
            }
            else
            {
                RadWindowManager1.RadAlert(
                    "No se pudo activar el vendedor.",
                    300, 100, "Error", null);
            }
        }

        private void CancelarVendedor()
        {
            GridEditableItem row = null;

            foreach (GridEditableItem item in rgVendedores.MasterTableView.Items)
            {
                //if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                if (((CheckBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[19].FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            int idVendedor = int.Parse(rgVendedores.MasterTableView.Items[row.ItemIndex]["IdVendedor"].Text);
            var user = (US_USUARIOModel)Session["UserInfo"];
            var anomalia = new ANOMALIAS_VENDEDORES()
            {
                ID_VENDEDOR = idVendedor,
                DESCRIPCION = txtAnomalia.Text,
                ADICIONADO_POR = user.Nombre_Usuario,
                FECHA_ADICION = DateTime.Now
            };
            var newAnomalia = new ValVendedores().GuardarAnomalia(anomalia);
            if (newAnomalia != null)
            {
                var InfoGeneral = new ValVendedores().ObtienePorId(idVendedor);
                InfoGeneral.Cve_Estatus_Vendedor = 4;
                if (new ValVendedores().ActualizarVendedor(InfoGeneral))
                {
                    var Usuarios = new ValVendedores().ObtenerUsuarios(idVendedor);

                    foreach (var usUsuario in Usuarios)
                    {
                        usUsuario.Estatus = GlobalVar.STATUS_USER_CANCEL;
                        new ValVendedores().ActualizarUsuario(usUsuario);
                        /*INSERTAR EVENTO CANCELADO DE USUARIO EN LOG*/
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "USUARIOS",
                            "CANCELADO", usUsuario.Nombre_Usuario,newAnomalia.DESCRIPCION,
                            "Fecha cancelado: " + DateTime.Now, "", "");
                    }

                        RadWindowManager1.RadAlert(
                            "El vendedor se ha cancelado correctamente.",
                            300, 100, "Cancelado", null);
                        CargarGrid(); cargarImagenes();
                    
                }
                else
                {
                    RadWindowManager1.RadAlert(
                        "No se pudo cambiar el estatus del vendedor.",
                        300, 100, "Cancelado", null);
                }
            }
            else
            {
                RadWindowManager1.RadAlert(
                       "No se pudo cambiar cancelar el vendedor.",
                       300, 100, "Cancelado", null);
            }
        }

        private void InactivarVendedor()
        {
            GridEditableItem row = null;

            foreach (GridEditableItem item in rgVendedores.MasterTableView.Items)
            {
                //if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                if (((CheckBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[19].FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            int idVendedor = int.Parse(rgVendedores.MasterTableView.Items[row.ItemIndex]["IdVendedor"].Text);

            var InfoGeneral = new ValVendedores().ObtienePorId(idVendedor);
            InfoGeneral.Cve_Estatus_Vendedor = 3;
            var Usuarios = new ValVendedores().ObtenerUsuarios(idVendedor);
            int resul = 10;
            if (new ValVendedores().ActualizarVendedor(InfoGeneral))
            {
                foreach (var usUsuario in Usuarios)
                {
                    usUsuario.Estatus = GlobalVar.STATUS_USER_INACTIVE;
                }
                resul++;
            }
            else resul = 0;

            if (resul > 0)
            {
                bool e = false;
                foreach (var item in Usuarios)
                {
                    e = new ValVendedores().ActualizarUsuario(item);
                    /*INSERTAR EVENTO INHABILITAR DE USUARIO EN LOG*/
                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                        Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "USUARIOS",
                        "INHABILITAR", Usuarios[0].Nombre_Usuario,
                        "", "", "", "");
                }
                resul++;
            }
            else resul = 0;


            if (resul > 0)
                RadWindowManager1.RadAlert(
                    "El vendedor se ha inactivado correctamente.",
                    300, 100, "Inactivado", null);
            else
            {
                RadWindowManager1.RadAlert(
                    "No se pudo inactivar el vendedor.",
                    300, 100, "Error", null);
            }
        }

        private void ReactivarVendedor()
        {
            GridEditableItem row = null;

            foreach (GridEditableItem item in rgVendedores.MasterTableView.Items)
            {
                //if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                if (((CheckBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[19].FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            int idVendedor = int.Parse(rgVendedores.MasterTableView.Items[row.ItemIndex]["IdVendedor"].Text);

            var InfoGeneral = new ValVendedores().ObtienePorId(idVendedor);
            InfoGeneral.Cve_Estatus_Vendedor = 2;
            if (new ValVendedores().ActualizarVendedor(InfoGeneral))
            {
                var Usuarios = new ValVendedores().ObtenerUsuarios(idVendedor);

                foreach (var usUsuario in Usuarios)
                {
                    usUsuario.Estatus = GlobalVar.STATUS_USER_ACTIVE;
                    new ValVendedores().ActualizarUsuario(usUsuario);
                    /*INSERTAR EVENTO REACTIVAR DE USUARIO EN LOG*/
                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                        Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "USUARIOS",
                        "REACTIVAR", usUsuario.Nombre_Usuario,
                        "", "", "", "");
                }

                    RadWindowManager1.RadAlert(
                        "El vendedor se ha reactivado correctamente.",
                        300, 100, "Reactivado", null);
                
            }
            else
            {
                RadWindowManager1.RadAlert(
                    "No se pudo activar el vendedor.",
                    300, 100, "Error", null);
            }
        }

        private void EditarVendedor()
        {
            GridEditableItem row = null;

            foreach (GridEditableItem item in rgVendedores.MasterTableView.Items)
            {
                //if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                if (((CheckBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[19].FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            Curp = rgVendedores.MasterTableView.Items[row.ItemIndex]["Curp"].Text;
            Response.Redirect("~/Vendedores/EditarVendedor.aspx?Curp=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(Curp)).Replace("+", "%2B"));//Curp); // /SolicitudCredito/DetalleCredito.aspx?creditno=" + NoCredito;

        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CatEstatus();
                CatRegionZona();
                BtnAceptar.Enabled = false;

            }
            
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            cargarImagenes();
        }

        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void ckbSelect_OnCheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            //var row = (GridViewRow)chk.Parent.Parent;
            var row = chk.NamingContainer as GridEditableItem;
            int rol = Convert.ToInt16(Session["IdRolUserLogueado"]);

            var combo = (RadComboBox) row.FindControl("LSB_Acciones");

            if (chk.Checked == false)
            {
                foreach (GridEditableItem item in rgVendedores.MasterTableView.Items)
                {
                    rgVendedores.MasterTableView.Items[item.ItemIndex].Enabled = true;
                    //grvSupplier.Rows[item.DataItemIndex].Enabled = true;

                    ((RadComboBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[16].FindControl("LSB_Acciones")).Enabled = false;
                    ((RadComboBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[16].FindControl("LSB_Acciones")).Items.Clear();
                    ((RadComboBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[16].FindControl("LSB_Acciones")).Items.Insert(0, new RadComboBoxItem("Elige Opcion","0"));
                    BtnAceptar.Enabled = false;

                    //((DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones")).Enabled = false;
                    //((DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones")).Items.Clear();
                    //((DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones")).Items.Insert(0, "Elige Opcion");
                    //BtnAceptar.Enabled = false;
                }
            }
            else
            {
                foreach (GridEditableItem item in rgVendedores.MasterTableView.Items)
                {
                    if (row.ItemIndex == item.ItemIndex)
                    {
                        rgVendedores.MasterTableView.Items[item.ItemIndex].Enabled = true;
                        ((RadComboBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[16].FindControl("LSB_Acciones")).Enabled = true;
                        //BtnAceptar.Enabled = true;
                    }

                    else
                    {
                        rgVendedores.MasterTableView.Items[item.ItemIndex].Enabled = false;
                    }
                    //if (row.ItemIndex == item.DataItemIndex)
                    //{
                    //    grvSupplier.Rows[item.DataItemIndex].Enabled = true;
                    //    ((DropDownList)grvSupplier.Rows[row.DataItemIndex].Cells[5].FindControl("LSB_Acciones")).Enabled = true;
                    //    BtnAceptar.Enabled = true;
                    //}

                    //else
                    //{
                    //    grvSupplier.Rows[item.DataItemIndex].Enabled = false;
                    //}
                }

                var lista = new AccionesMonitor_Rol().AccionesMonitorRol(5, rol);
                
                if (lista.Count > 0)
                {
                    var estatus = rgVendedores.MasterTableView.Items[row.ItemIndex]["Estatus"].Text;


                    if (estatus.ToUpper() == "EN REVISION")//"PENDIENTE")
                    {
                        lista.RemoveAll(me => me.IdAccion == 26 || me.IdAccion == 27 || me.IdAccion == 31);
                    }
                    else if (estatus.ToUpper() == "ACTIVO")
                    {
                        lista.RemoveAll(me => me.IdAccion == 25 || me.IdAccion == 27);
                        if (new ValVendedores().ExisteUsuario(int.Parse(rgVendedores.MasterTableView.Items[row.ItemIndex]["IdVendedor"].Text), int.Parse(rgVendedores.MasterTableView.Items[row.ItemIndex]["IdDistribuidor"].Text)) || rgVendedores.MasterTableView.Items[row.ItemIndex]["AccesoSistema"].Text=="NO")
                            lista.RemoveAll(me => me.IdAccion == 31);
                    }
                    else if (estatus.ToUpper() == "INACTIVO")
                    {
                        lista.RemoveAll(me => me.IdAccion == 25 || me.IdAccion == 26 || me.IdAccion == 28 || me.IdAccion == 29 || me.IdAccion==31);
                    }
                    else if (estatus.ToUpper() == "CANCELADO")
                    {
                        lista.RemoveAll(me => me.IdAccion == 25 || me.IdAccion == 26 || me.IdAccion == 27 || me.IdAccion == 28 || me.IdAccion == 29 || me.IdAccion==31);
                    }

                combo.DataSource = lista;
                combo.DataValueField = "IdAccion";
                combo.DataTextField = "NombreAccion";
                combo.DataBind();
                combo.Items.Insert(0, new RadComboBoxItem("Elegir Opcion","0"));
                combo.SelectedIndex = 0;
                }

               
            }
        }

        protected void BtnAceptar_OnClick(object sender, EventArgs e)
        {
            GridEditableItem row = null;

            foreach (GridEditableItem item in rgVendedores.MasterTableView.Items)
            {
                //if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                if (((CheckBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[19].FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }


            switch (((RadComboBox)rgVendedores.MasterTableView.Items[row.ItemIndex].Cells[16].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "25":
                    RadWindowManager1.RadConfirm("Confirmar Activar el vendedor Seleccionado.", "confirmCallBackFn", 300, 100, null, "Activar");
                    break;
                case "26":
                    RadWindowManager1.RadConfirm("Si inactiva el vendedor, este no podrá capturar solicitudes, ¿Desea continuar?.", "confirmCallBackFn", 300, 100, null, "Inactivar");
                    break;
                case "27":
                    RadWindowManager1.RadConfirm("Si reactiva el vendedor, este podrá capturar nuevamente solicitudes, ¿Desea continuar?.", "confirmCallBackFn", 300, 100, null, "Reactivar");
                    break;
                case "28":
                    //RadWindowManager1.RadConfirm("Si cancela el vendedor no podrá ser registrado nuevamente.", "confirmCallBackFn", 300, 100, null, "Cancelar");
                    break;
                case "29":
                    RadWindowManager1.RadConfirm("Confirmar Editar el vendedor Seleccionado.", "confirmCallBackFn", 300, 100, null, "Editar");
                    break;
                case "31":
                    
                    //ClientScript.RegisterStartupScript(GetType(), "Modal", "openWinNavigateUrl();", true);
                    //RadWindowManager1.RadConfirm("Confirmar crear usuario al vendedor seleccionado.", "confirmCallBackFn", 300, 100, null, "Cancelar");
                    break;

            }
        }

        protected void btnCancelarVendedor_OnClick(object sender, EventArgs e)
        {

        }

        protected void btnAceptarVendedor_OnClick(object sender, EventArgs e)
        {
            CancelarVendedor();
        }

        protected void LSB_Acciones_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var combo = (RadComboBox)sender;
            var row = combo.NamingContainer as GridEditableItem;
            switch (
                ((RadComboBox) rgVendedores.MasterTableView.Items[row.ItemIndex].Cells[16].FindControl("LSB_Acciones"))
                    .SelectedValue)
            {
                case "25":
                    modalPopupUsuarios.OpenerElementID = null;
                    modalPopupCancelar.OpenerElementID = null;
                    break;
                case "26":
                    modalPopupUsuarios.OpenerElementID = null;
                    modalPopupCancelar.OpenerElementID = null;
                    break;
                case "27":
                    modalPopupUsuarios.OpenerElementID = null;
                    modalPopupCancelar.OpenerElementID = null;
                    break;
                case "28":
                    modalPopupUsuarios.OpenerElementID = null;
                    txtAnomalia.Text = "";
                    modalPopupCancelar.OpenerElementID = BtnAceptar.ClientID;
                    break;
                case "29":
                    modalPopupUsuarios.OpenerElementID = null;
                    modalPopupCancelar.OpenerElementID = null;                  
                    break;
                case "31":
                    modalPopupCancelar.OpenerElementID = null; 
                    modalPopupUsuarios.OpenerElementID = BtnAceptar.ClientID;
                    break;
            }

            if (((RadComboBox) rgVendedores.MasterTableView.Items[row.ItemIndex].Cells[16].FindControl("LSB_Acciones"))
                .SelectedValue == "0")
                BtnAceptar.Enabled = false;
            else
            {
                BtnAceptar.Enabled = true;
            }
        }

        protected void btnSalir_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void HiddenButton_OnClick(object sender, EventArgs e)
        {
            GridEditableItem row = null;

            foreach (GridEditableItem item in rgVendedores.MasterTableView.Items)
            {
                //if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                if (((CheckBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[19].FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            //var row = combo.NamingContainer as GridEditableItem;
            switch (
                ((RadComboBox)rgVendedores.MasterTableView.Items[row.ItemIndex].Cells[16].FindControl("LSB_Acciones"))
                    .SelectedValue)
            {
                case "25":// activar
                    ActivarVendedor();
                    break;
                case "26": //inactivar
                    InactivarVendedor();
                    break;
                case "27"://reactivar
                    ReactivarVendedor();
                    break;
                case "28"://cancelar
                    // Listo
                    break;
                case "29"://editar
                   EditarVendedor();
                    break;
                case "31"://crear usuario
                    
                    break;
            }
            CargarGrid();
        }

        protected void rgVendedores_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            CargarGrid();
        }

        protected void rgVendedores_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            CargarGrid();
        }

        protected void rgVendedores_OnSortCommand(object sender, GridSortCommandEventArgs e)
        {
            CargarGrid();
        }

        protected void btnAceptarUsuario_OnClick(object sender, EventArgs e)
        {
            GridEditableItem row = null;

            foreach (GridEditableItem item in rgVendedores.MasterTableView.Items)
            {
                //if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                if (((CheckBox)rgVendedores.MasterTableView.Items[item.ItemIndex].Cells[19].FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            var encryptPassword = ValidacionesUsuario.Encriptar(RandomPassword.Generate(DefaultPasswordLength));
            var userModel = new US_USUARIO();
            userModel.Nombre_Usuario = txtUserName.Text;
            userModel.CorreoElectronico = txtEmail.Text;
            userModel.Numero_Telefono = txtPhone.Text;
            userModel.Contrasena = encryptPassword;
            //userModel.Contrasena = RandomPassword.Generate(DefaultPasswordLength);
            userModel.Nombre_Completo_Usuario = rgVendedores.MasterTableView.Items[row.ItemIndex]["Nombre"].Text;
            userModel.Id_Rol = (int)UserRole.VENDEDOR;
            userModel.Id_Departamento = int.Parse(rgVendedores.MasterTableView.Items[row.ItemIndex]["IdDistribuidor"].Text);
            userModel.Tipo_Usuario = GlobalVar.SUPPLIER_BRANCH;
            userModel.Estatus = GlobalVar.STATUS_USER_ACTIVE;
            userModel.ID_VENDEDOR = int.Parse(rgVendedores.MasterTableView.Items[row.ItemIndex]["IdVendedor"].Text);
            var newUsuario = new ValVendedores().GuardarUsuario(userModel);
            if (newUsuario != null)
            {
                //RadWindowManager1.RadAlert(
                //       "El usuario se ha creado correctamente.",
                //       300, 100, "Usuario", null);
                /*INSERTAR EVENTO ALTA DE USUARIO EN LOG*/
                //var idUsuarioGenerado = Insertlog.GetIdUser(userModel.Nombre_Usuario);
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), "USUARIOS", "ALTA",
                    userModel.Nombre_Usuario, "",
                    "Fecha alta: " + DateTime.Now, "", "");
                string message = "";
                try
                {
                    var decryptPassword = ValidacionesUsuario.Desencriptar(userModel.Contrasena);
                    MailUtility.RegisterEmail(userModel.Nombre_Usuario, userModel.CorreoElectronico,
                                    decryptPassword);

                    //Show success message
                    message = (string)GetGlobalResourceObject("DefaultResource", "msgSaveUserSuccess");
                    
                    RadWindowManager1.RadAlert(
                       "" + message + "",
                       300, 100, "ERROR", null);
                }
                catch (Exception)
                {
                   message = (string)GetGlobalResourceObject("DefaultResource", "msgSaveUserSuccess") +
                                           " Con error al enviar el correo electrónico";//(string)GetGlobalResourceObject("DefaultResource", "err");

                    RadWindowManager1.RadAlert(
                       "" + message + "",
                       300, 100, "ERROR", null);
                }
                CargarGrid();
            }
            else
            {
                RadWindowManager1.RadAlert(
                       "No se pudo crear el usuario.",
                       300, 100, "Usuario", null);
            }
        }

        protected void btnCancelarUsuario_OnClick(object sender, EventArgs e)
        {
        }

        #endregion
    }
}