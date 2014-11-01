using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.Vendedores;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.Vendedores;
using Telerik.Web.UI;

namespace PAEEEM.Vendedores
{
    public partial class EditarVendedor : System.Web.UI.Page
    {
        #region Variables
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

        public string Curp { get; set; }
        #endregion

        #region Metodos
        private void cargarInformacion()
        {
            var vendedor = new RegVendedores().ObtenerVendedor(Curp);
            if (vendedor == null) return;
            txtCurp.Text = vendedor.CURP;
            txtNombre.Text = vendedor.NOMBRE;
            txtAP.Text = vendedor.APELLIDO_PATERNO;
            txtAM.Text = vendedor.APELLIDO_MATERNO;
            rdpFecha.SelectedDate = vendedor.FEC_NACIMINIENTO;
            cmbIdentificacion.SelectedValue = vendedor.ID_TIPO.ToString();
            txtNumIdenti.Text = vendedor.NUMERO_IDENTIFICACION;
            cmbAcceso.SelectedValue = vendedor.ACCESO_SISTEMA ? "1" : "0";
            foto = new TempImagen();
            foto.CURP = vendedor.CURP;
            foto.Imagen = vendedor.FOTO_IDENTIFICACION;
            EliminarImagen.Visible = true;
            verImagen.Visible = true;
            UploadedImagen.Enabled = false;
            ImgCorrecto.Visible = true;
        }

        private void TipoIdent()
        {
            cmbIdentificacion.DataSource = new RegVendedores().ObtenerTipoIdentificacionVendedores();
            cmbIdentificacion.DataTextField = "DESCRIPCION";
            cmbIdentificacion.DataValueField = "ID_TIPO";
            cmbIdentificacion.DataBind();
            cmbIdentificacion.Items.Insert(0, new RadComboBoxItem("Seleccione", "0"));
            cmbIdentificacion.SelectedIndex = 0;
        }

        private string validarCampos()
        {
            long ban;

            if (txtCurp.Text.Length != 18) return "Verificar CURP";
            if (txtNombre.Text == "") return "Escribe un nombre";
            if (txtAP.Text == "") return "Escribe el apellido paterno";
            if (txtAM.Text == "") return "Escribe el apellido materno";
            if (rdpFecha.SelectedDate == null) return "Selecciona un fecha de nacimiento";
            if (cmbIdentificacion.SelectedIndex == 0) return "Seleccione un tipo de identificacion";
            if (txtNumIdenti.Text.Length != 18 || !long.TryParse(txtNumIdenti.Text, out ban)) return "Verificar numero identificacion";
            if (cmbAcceso.SelectedIndex == 0) return "seleccione el acceso al sistema";
            if (UploadedImagen.Enabled) return "Selecciona la imagen";
            return "";
        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //UploadedImagen.UploadedFiles.Clear();
                //Curp = Request.QueryString["Curp"];
                Curp = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Curp"].ToString().Replace("%2B", "+")));
                TipoIdent();
                cargarInformacion();
            }
        }

        protected void UploadedImagen_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            var Upload = sender as RadAsyncUpload;
            if (Upload != null)
            {
                foto = new TempImagen();
                foto.CURP = txtCurp.Text;
                var doc = new byte[e.File.ContentLength];
                e.File.InputStream.Read(doc, 0, e.File.ContentLength);
                foto.Imagen = doc;

                if (e.IsValid)
                {
                    EliminarImagen.Visible = true;
                    verImagen.Visible = true;
                    UploadedImagen.Enabled = false;
                    ImgCorrecto.Visible = true;
                }
            }
        }

        protected void EliminarImagen_OnClick(object sender, ImageClickEventArgs e)
        {
            foto = new TempImagen();
            verImagen.Visible = false;
            EliminarImagen.Visible = false;
            UploadedImagen.Enabled = true;
            UploadedImagen.UploadedFiles.Clear();
            ImgCorrecto.Visible = false;
        }

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            Curp = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Curp"].ToString().Replace("%2B", "+")));
            bool ban = true;
            var InfoGeneral = new ValVendedores().ObtienePorCurp(Curp);
            InfoGeneral.ID_TIPO = byte.Parse(cmbIdentificacion.SelectedValue);
            InfoGeneral.NUMERO_IDENTIFICACION = txtNumIdenti.Text;
            InfoGeneral.ACCESO_SISTEMA = cmbAcceso.SelectedValue == "1";
            InfoGeneral.FOTO_IDENTIFICACION = foto.Imagen;
            string validar = validarCampos();
            if (validar == "")
            {
                if (new ValVendedores().ActualizarVendedor(InfoGeneral))
                {
                    if (!InfoGeneral.ACCESO_SISTEMA)
                    {
                        var Usuarios = new ValVendedores().ObtenerUsuarios(InfoGeneral.ID_VENDEDOR);

                        foreach (var usUsuario in Usuarios)
                        {
                            usUsuario.Estatus = GlobalVar.STATUS_USER_INACTIVE;
                            ban = new ValVendedores().ActualizarUsuario(usUsuario);
                            /*INSERTAR EVENTO INHABILITAR DE USUARIO EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]), "USUARIOS",
                                "INHABILITAR", usUsuario.Nombre_Usuario,
                                "", "", "", "");
                        }

                        RadWindowManager1.RadAlert(
                            "El vendedor se ha editado correctamente.",
                            300, 100, "Editar", null);

                    }
                    else
                    {
                        var Usuarios = new ValVendedores().ObtenerUsuarios(InfoGeneral.ID_VENDEDOR);

                        foreach (var usUsuario in Usuarios)
                        {
                            usUsuario.Estatus = GlobalVar.STATUS_USER_ACTIVE;
                            ban = new ValVendedores().ActualizarUsuario(usUsuario);
                            /*INSERTAR EVENTO REACTIVAR DE USUARIO EN LOG*/
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]), "USUARIOS",
                                "REACTIVAR", usUsuario.Nombre_Usuario,
                                "", "", "", "");
                        }


                        RadWindowManager1.RadAlert(
                            "El vendedor se ha editado correctamente.",
                            300, 100, "Editar", null);
                    }
                }

                else
                {
                    RadWindowManager1.RadAlert(
                        "No se pudo editar el vendedor.",
                        300, 100, "Cancelado", null);
                }
            }
            else
            {
                RadWindowManager1.RadAlert("" + validar.ToUpper() + "", 300, 100, "Verificar", null);
            }
        }

        protected void btnSalir_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Vendedores/ValidarVendedores.aspx");
        }
        #endregion
    }
}