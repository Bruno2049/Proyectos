using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using PAEEEM.LogicaNegocios.Tarifas;
using Telerik.Web.UI;

namespace PAEEEM.SupplierModule
{
    public partial class MonitorAuxuliar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LLenaCatalogosTarifa();
                LLenaCatEstatus();
            }
        }

        protected void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = sender as CheckBox;
            var editedItem = (sender as CheckBox).NamingContainer as GridDataItem;
            var rpu = editedItem.GetDataKeyValue("Rpu").ToString();

            if (checkbox.Checked)
            {
                var cmbAcciones = editedItem["Acciones"].FindControl("RadCmbAcciones") as RadComboBox;
                cmbAcciones.Items.Insert(0, new RadComboBoxItem("Seleccione..."));

                var estatus = editedItem["Estatus"].Text;

                if (estatus == "ACTIVO")
                {
                    cmbAcciones.Items.Add(new RadComboBoxItem("Editar", "1"));
                    cmbAcciones.Items.Add(new RadComboBoxItem("Cancelar", "2"));
                }
                else
                {
                    cmbAcciones.Items.Add(new RadComboBoxItem("Consultar", "3"));
                    cmbAcciones.Items.Add(new RadComboBoxItem("Activar", "4"));
                }

                cmbAcciones.Enabled = true;

                foreach (GridDataItem dataItem in rgAuxuliar.MasterTableView.Items)
                {
                    var radChkSeleccionar = dataItem.FindControl("ChkSeleccionar") as CheckBox;
                    radChkSeleccionar.Enabled = false;
                }

                checkbox.Enabled = true;
            }
            else
            {
                var cmbAcciones = editedItem["Acciones"].FindControl("RadCmbAcciones") as RadComboBox;
                cmbAcciones.Items.Clear();

                foreach (GridDataItem dataItem in rgAuxuliar.MasterTableView.Items)
                {
                    var radChkSeleccionar = dataItem.FindControl("ChkSeleccionar") as CheckBox;
                    radChkSeleccionar.Enabled = true;
                }
            }            
        }

        protected void rgAuxuliar_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var usuario = ((US_USUARIOModel) Session["UserInfo"]).Nombre_Usuario;
            var lstCaptutasDistribuidor = new CapturaAuxilialBL().ObtenCapturasDistribuidor(usuario);
            rgAuxuliar.DataSource = lstCaptutasDistribuidor;
        }

        protected void RadBtnAceptar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem dataItem in rgAuxuliar.MasterTableView.Items)
            {
                var radChkSeleccionar = dataItem.FindControl("ChkSeleccionar") as CheckBox;
                var radCmbAcciones = dataItem.FindControl("RadCmbAcciones") as RadComboBox;

                if (radChkSeleccionar.Checked)
                {
                    var rpu = dataItem.GetDataKeyValue("Rpu").ToString();
                    EjecutaAccion(rpu, int.Parse(radCmbAcciones.SelectedValue));
                }
            }
        }

        protected void EjecutaAccion(string rpu, int accion)
        {
            switch (accion)
            {
                case 1:
                    MuestraCaptura(rpu, accion);
                    break;
                case 2:
                    ActulizaCapturaAux(rpu, accion);
                    break;
                case 3:
                    MuestraCaptura(rpu, accion);
                    break;
                case 4:
                    ActulizaCapturaAux(rpu, accion);
                    break;
            }
        }

        protected void ActulizaCapturaAux(string rpu, int accion)
        {
            byte estatus = 0;

            if (accion == 4)
                estatus = 1;
            if (accion == 2)
                estatus = 2;

            var capturaAux = new CapturaAuxilialBL().ObtenDatosAuxTrama(rpu);

            if (capturaAux != null)
            {
                capturaAux.EstatusCapturaAux = estatus;
                capturaAux.FechaAdicion = DateTime.Now.Date;

                if (new CapturaAuxilialBL().ActualizaDatosAuxTrama(capturaAux))
                {
                    if (estatus == 1)
                        rwmVentana.RadAlert("Se activo correctamente el rpu", 300, 150, "Captura Auxiliar", null);
                    if (estatus == 2)
                        rwmVentana.RadAlert("Se cancelo correctamente el rpu", 300, 150, "Captura Auxiliar", null);
                }
            }
        }

        protected void MuestraCaptura(string rpu, int accion)
        {
            Response.Redirect("CapturaAuxiliar.aspx?Token=" +
                                     Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(rpu)) + "&Acc=" + accion);
        }

        protected void LLenaCatalogosTarifa()
        {
            var lstTarifas = RegistrarTarifas.TiposTarifa();

            if (lstTarifas.Count > 0)
            {
                RadCmbTarifa.DataSource = lstTarifas;
                RadCmbTarifa.DataValueField = "Cve_Tarifa";
                RadCmbTarifa.DataTextField = "Dx_Tarifa";
                RadCmbTarifa.DataBind();

                RadCmbTarifa.Items.Insert(0, new RadComboBoxItem("Seleccione...", "0"));
                RadCmbTarifa.SelectedIndex = 0;
            }
        }

        protected void LLenaCatEstatus()
        {
            RadCmbEstatus.Items.Clear();
            RadCmbEstatus.Items.Add(new RadComboBoxItem("Activo", "1"));
            RadCmbEstatus.Items.Add(new RadComboBoxItem("Cancelado", "2"));

            RadCmbEstatus.Items.Insert(0, new RadComboBoxItem("Seleccione...", "0"));
            RadCmbEstatus.SelectedIndex = 0;
        }

        protected void RadBtnBuscar_Click(object sender, EventArgs e)
        {
            var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;

            var lstCapturasDistribuidor = new CapturaAuxilialBL().ObtenCapturasDistribuidorFiltro(usuario,
                                                                                                  int.Parse(
                                                                                                      RadCmbEstatus
                                                                                                          .SelectedValue),
                                                                                                  int.Parse(
                                                                                                      RadCmbTarifa
                                                                                                          .SelectedValue),
                                                                                                  RadTxtRPU.Text);
            rgAuxuliar.DataSource = lstCapturasDistribuidor;
            rgAuxuliar.DataBind();
        }
    }
}