using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Vendedores;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.Vendedores;
using Telerik.Web.UI;
using System.Text.RegularExpressions;

namespace PAEEEM.Vendedores
{
    public partial class RegistroVendedores : System.Web.UI.Page
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
        #endregion

        #region Metodos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TipoIdent();
                UploadedImagen.UploadedFiles.Clear();
                btnEnviar.Enabled = false;
            }
            
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
            if (txtNumIdenti.Text.Length != 18 || !long.TryParse(txtNumIdenti.Text,out ban)) return "Verificar numero identificacion";
            if (cmbAcceso.SelectedIndex == 0) return "seleccione el acceso al sistema";
            if (UploadedImagen.Enabled) return "Selecciona la imagen";
            return "";
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtAM.Text = "";
            txtAP.Text = "";
            rdpFecha.SelectedDate = null;
            cmbIdentificacion.SelectedIndex = 0;
            txtNumIdenti.Text = "";
            cmbAcceso.SelectedIndex = 0;

            foto = new TempImagen();
            verImagen.Visible = false;
            EliminarImagen.Visible = false;
            UploadedImagen.Enabled = true;
            UploadedImagen.UploadedFiles.Clear();
            ImgCorrecto.Visible = false;
        }

        private bool validarCurp(string curp)
        {
            if (curp.Length == 18 && Regex.IsMatch(this.txtCurp.Text, @"^[a-zA-Z]{4}\d{6}[a-zA-Z]{6}\d{2}$"))
            {
                return true;
            }
            else return false;

        }
        #endregion

        #region Eventos
        protected void btnRefres_Click(object sender, EventArgs e)
        {
            
        }

        protected void EliminarImagen_Click(object sender, ImageClickEventArgs e)
        {
            foto = new TempImagen();
            verImagen.Visible = false;
            EliminarImagen.Visible = false;
            UploadedImagen.Enabled = true;
            UploadedImagen.UploadedFiles.Clear();
            ImgCorrecto.Visible = false;
        }

        protected void UploadedImagen_FileUploaded(object sender, FileUploadedEventArgs e)
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

        protected void btnEnviar_OnClick(object sender, EventArgs e)
        {
            try
            {
                string validar = validarCampos();
                if (validar == "")
                {
                    var vendedor = new VENDEDORES
                    {
                        Cve_Estatus_Vendedor = 1,
                        CURP = txtCurp.Text.ToUpper(),
                        ID_TIPO = byte.Parse(cmbIdentificacion.SelectedValue),
                        NOMBRE = txtNombre.Text.ToUpper(),
                        APELLIDO_PATERNO = txtAP.Text.ToUpper(),
                        APELLIDO_MATERNO = txtAM.Text.ToUpper(),
                        FEC_NACIMINIENTO = (DateTime) rdpFecha.SelectedDate,
                        ACCESO_SISTEMA = cmbAcceso.SelectedValue == "1",
                        FOTO_IDENTIFICACION = foto.Imagen,
                        NUMERO_IDENTIFICACION = txtNumIdenti.Text
                    };
                    var user = (US_USUARIOModel) Session["UserInfo"];
                    vendedor.ADICIONADO_POR = user.Nombre_Usuario;
                    vendedor.FECHA_ADICION = DateTime.Now;

                    var newVendedor = new RegVendedores().ObtenerVendedor(txtCurp.Text);

                    if (newVendedor == null)
                    {
                        newVendedor = new RegVendedores().GuardarVendedor(vendedor);
                    }

                    if (newVendedor != null)
                    {
                        var registroDist = new RELACION_VENDEDOR_DISTRIBUIDOR
                        {
                            ID_VENDEDOR = newVendedor.ID_VENDEDOR,
                            Id_Branch = user.Id_Departamento,
                            ADICIONADO_POR = user.Nombre_Usuario,
                            FECHA_ADICION = DateTime.Now
                        };

                        var existeRegistro = new RegVendedores().ObtenerRelacionVendedorDist(newVendedor.ID_VENDEDOR,
                            registroDist.Id_Branch);
                        if (!existeRegistro)
                        {
                            var a = new RegVendedores().GuardarRegistro(registroDist);
                            RadWindowManager1.RadAlert("Su registro fue enviado a validación.", 300, 100, "Registrado",
                                null);
                            txtCurp.Text = "";
                            cmbIdentificacion.SelectedIndex = 0;
                            txtNombre.Text = "";
                            txtAP.Text = "";
                            txtAM.Text = "";
                            rdpFecha.SelectedDate = null;
                            cmbAcceso.SelectedIndex = 0;
                            foto = null;
                            txtNumIdenti.Text = "";

                            verImagen.Visible = false;
                            EliminarImagen.Visible = false;
                            UploadedImagen.Enabled = true;
                            UploadedImagen.UploadedFiles.Clear();
                            ImgCorrecto.Visible = false;
                            btnEnviar.Enabled = false;
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("El vendedor ya se encuentra registrado con el distribuidor.", 300, 100, "Registrado",
                                null);
                            btnEnviar.Enabled = false;
                        }
                    }
                }
                else
                {
                    RadWindowManager1.RadAlert(""+validar.ToUpper()+"", 300, 100, "Verificar", null);
                }
            }
            catch (Exception)
            {
                RadWindowManager1.RadAlert("No se puede registar el vendedor.",300, 100, "ERROR",null);
                btnEnviar.Enabled = true;
            }
        }

        protected void btnSalir_OnClick(object sender, EventArgs e)
        {
            RadWindowManager1.RadConfirm("¿Está seguro de cancelar el registro?, no se guardara información.", "confirmCallBackFn", 300, 100,null, "Confirmar");
            //Response.Redirect("~/Default.aspx");
        }

        protected void txtCurp_OnTextChanged(object sender, EventArgs e)
        {
            if (txtCurp.Text != "" && validarCurp(txtCurp.Text))
            {
                var anomalia = new RegVendedores().ObtenerAnomalias(txtCurp.Text);
                btnEnviar.Enabled = true;
                if (anomalia == null)
                {
                    var vendedor = new RegVendedores().ObtenerVendedor(txtCurp.Text);
                    if (vendedor == null) return;
                    RadWindowManager1.RadConfirm("El vendedor ya esta registrado en el sistema. ¿Desea registrarlo?.", "confirmCallBackFn2", 300, 100, null, "Confirmar");
                    //txtNombre.Text = vendedor.NOMBRE;
                    //txtAP.Text = vendedor.APELLIDO_PATERNO;
                    //txtAM.Text = vendedor.APELLIDO_MATERNO;
                    //rdpFecha.SelectedDate = vendedor.FEC_NACIMINIENTO;
                    //cmbIdentificacion.SelectedValue = vendedor.ID_TIPO.ToString();
                    //txtNumIdenti.Text = vendedor.NUMERO_IDENTIFICACION;
                    //cmbAcceso.SelectedValue = vendedor.ACCESO_SISTEMA ? "1" : "0";
                    //foto = new TempImagen();
                    //foto.CURP = vendedor.CURP;
                    //foto.Imagen = vendedor.FOTO_IDENTIFICACION;
                    //EliminarImagen.Visible = false;
                    //verImagen.Visible = true;
                    //UploadedImagen.Enabled = false;
                    //ImgCorrecto.Visible = true;

                }
                else
                {
                    RadWindowManager1.RadAlert(
                        "El vendedor cuenta con anomalías registradas, por lo cual no se puede registrar el vendedor.",
                        300, 100, "Anomalia", null);
                    btnEnviar.Enabled = false;
                    LimpiarCampos();
                }
            }
            else
            {
                RadWindowManager1.RadAlert(
                        "Formato incorrecto de curp .",
                        300, 100, "ERROR", null);
                btnEnviar.Enabled = false;
                LimpiarCampos();
            }
        }

        protected void HiddenButton_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void HiddenButton2_Click(object sender, EventArgs e)
        {
            //var anomalia = new RegVendedores().ObtenerAnomalias(txtCurp.Text);
            //btnEnviar.Enabled = true;
            //if (anomalia == null)
            //{
            var vendedor = new RegVendedores().ObtenerVendedor(txtCurp.Text);
            //    if (vendedor == null) return;
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
                EliminarImagen.Visible = false;
                verImagen.Visible = true;
                UploadedImagen.Enabled = false;
                ImgCorrecto.Visible = true;

            //}
            //else
            //{
            //    RadWindowManager1.RadAlert(
            //        "El vendedor cuenta con anomalías registradas, por lo cual no se puede registrar el vendedor.",
            //        300, 100, "Anomalia", null);
            //    btnEnviar.Enabled = false;
            //    LimpiarCampos();
            //}
        }
        #endregion
    }

}
