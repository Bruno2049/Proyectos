using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.LogicaNegocios.CAyD;
using Telerik.Web.UI;
using PAEEEM.Entidades.CAyD;
using PAEEEM.LogicaNegocios.LOG;
namespace PAEEEM.DisposalModule
{
    public partial class ReasignarCAyD : System.Web.UI.Page
    {

        private List<DatosReasignar> solicitudes
        {
            get
            {
                return Session["solicitudes"] == null
                    ? new List<DatosReasignar>()
                    : (List<DatosReasignar>)Session["solicitudes"];
            }
            set { Session["solicitudes"] = value; }
        }

        private List<TempReasignar> TempSolicitudes
        {
            get
            {
                return Session["TempSolicitudes"] == null
                    ? new List<TempReasignar>()
                    : (List<TempReasignar>)Session["TempSolicitudes"];
            }
            set { Session["TempSolicitudes"] = value; }
        }

        protected int rs
        {
            get { return (int)Session["rs"]; }
            set { Session["rs"] = value; }
        }

        protected int cayd
        {
            get { return (int)Session["cayd"]; }
            set { Session["cayd"] = value; }
        }

        protected string solicitud
        {
            get { return (string)Session["solicitud"]; }
            set { Session["solicitud"] = value; }
        }

        protected string folio
        {
            get { return (string)Session["folio"]; }
            set { Session["folio"] = value; }
        }

        public void llenarGrid()
        {
            rs = int.Parse(cmbRS.SelectedValue);
            cayd = int.Parse(cmbCAyD.SelectedValue);
            solicitud = txtSolicitud.Text;
            folio = txtFolio.Text;
            //solicitudes = new List<DatosReasignar>();
            solicitudes = new Reasignar().ObtenerSolicitudes(rs, cayd, solicitud, folio);
            var temp = solicitudes.GroupBy(me => me.NoSolicitud).Select(s => s.FirstOrDefault());
            rgSolicitudes.DataSource = temp;
                //rgSolicitudes.DataBind();
        }

        private void CatDistribuidores()
        {
            cmbRS.DataSource = new Reasignar().Distribuidores().OrderBy(me=>me.Dx_Razon_Social);
            cmbRS.DataTextField = "Dx_Razon_Social";
            cmbRS.DataValueField = "Id_Branch";
            cmbRS.DataBind();
            cmbRS.Items.Insert(0, new RadComboBoxItem("Seleccione", "0"));
            cmbRS.SelectedIndex = 0;
        }

        private void CatCAyDs()
        {
            var listaCAyD = new Reasignar().CAyDs().OrderBy(me=>me.Dx_Nombre_Comercial);
            cmbCAyD.DataSource = listaCAyD;
            cmbCAyD.DataTextField = "Dx_Nombre_Comercial";
            cmbCAyD.DataValueField = "Id_Centro_Disp"; //
            cmbCAyD.DataBind();
            cmbCAyD.Items.Insert(0, new RadComboBoxItem("Seleccione", "0"));
            cmbCAyD.SelectedIndex = 0;

            cmbAsignarCAyD.DataSource = listaCAyD;
            cmbAsignarCAyD.DataTextField = "Dx_Nombre_Comercial";
            cmbAsignarCAyD.DataValueField = "Id_Centro_Disp"; //
            cmbAsignarCAyD.DataBind();
            cmbAsignarCAyD.Items.Insert(0, new RadComboBoxItem("Seleccione", "0"));
            cmbAsignarCAyD.SelectedIndex = 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CatDistribuidores();
                CatCAyDs();
                TempSolicitudes = new List<TempReasignar>();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            TempSolicitudes = new List<TempReasignar>();
            llenarGrid();
            rgSolicitudes.DataBind();
            if (solicitudes.Count == 0)
            {
                cmbAsignarCAyD.Enabled = false;
                txtAsignar.Enabled = false;
                RadWindowManager1.RadAlert("No se encontraron coincidencias con los criterios seleccionados.", 300, 100, "Busqueda",
                                null);
            }
            
        }

        protected void txtAsignar_Click(object sender, EventArgs e)
        {
            int CAyDReasignar = int.Parse(cmbAsignarCAyD.SelectedValue);
            if (CAyDReasignar > 0)
                RadWindowManager1.RadConfirm("¿Está seguro de realizar la reasignación?", "confirmCallBackFn", 300, 100, null, "Confirmar");
            else
                RadWindowManager1.RadAlert("Debe seleccionar un CayD para reasignar.", 300, 100, "Seleccione",
                               null);
        }

        protected void txtSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void ckbSelect_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            var row = chk.NamingContainer as GridEditableItem;
            if (((CheckBox)rgSolicitudes.MasterTableView.Items[row.ItemIndex].Cells[7].FindControl("ckbSelect")).Checked)
            {
                TempReasignar temp = new TempReasignar();
                temp.NoSolicitud = rgSolicitudes.MasterTableView.Items[row.ItemIndex]["NoSolicitud"].Text;
                temp.check = true;
                TempSolicitudes.Add(temp);
            }
            else
            {
                TempSolicitudes.RemoveAll(me => me.NoSolicitud == rgSolicitudes.MasterTableView.Items[row.ItemIndex]["NoSolicitud"].Text);
            }
            if (TempSolicitudes.Count > 0)
            {
                cmbAsignarCAyD.Enabled = true;
                txtAsignar.Enabled = true;
            }
            else
            {
                cmbAsignarCAyD.Enabled = false;
                txtAsignar.Enabled = false;
            }
        }

        protected void rgSolicitudes_DataBound(object sender, EventArgs e)
        {
            llenarGrid();

            GridEditableItem row = null;

            foreach (GridEditableItem item in rgSolicitudes.MasterTableView.Items)
            {
                string NC = rgSolicitudes.MasterTableView.Items[item.ItemIndex]["NoSolicitud"].Text;
                TempReasignar temp = TempSolicitudes.Find(me => me.NoSolicitud == NC);
                if (temp != null)
                {
                    ((CheckBox)rgSolicitudes.MasterTableView.Items[item.ItemIndex].Cells[7].FindControl("ckbSelect")).Checked = temp.check;
                }
            }
        }

        protected void rgSolicitudes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            llenarGrid();

            foreach (GridEditableItem item in rgSolicitudes.MasterTableView.Items)
            {
                string NC = rgSolicitudes.MasterTableView.Items[item.ItemIndex]["NoSolicitud"].Text;
                TempReasignar temp = TempSolicitudes.Find(me => me.NoSolicitud == NC);
                if (temp != null)
                {
                    ((CheckBox)rgSolicitudes.MasterTableView.Items[item.ItemIndex].Cells[7].FindControl("ckbSelect")).Checked = temp.check;
                }
            }
        }

        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            int CAyDReasignar = int.Parse(cmbAsignarCAyD.SelectedValue);
            string NombreCAyDReasignar = cmbAsignarCAyD.SelectedItem.Text;

            List<DatosReasignar> listaReasignar = new List<DatosReasignar>();
            List<TempReasignar> listaRemover = new List<TempReasignar>();
            foreach (var item in TempSolicitudes)
            {
                if (new Reasignar().ExisteFechaRecepcion(item.NoSolicitud))
                {
                    RadWindowManager1.RadAlert("La solicitud " + item.NoSolicitud + " no puede ser reasignada debido a que cuenta con folio de ingreso.", 300, 100, "Seleccione",
                               null);
                    listaRemover.Add(item);
                }
            }

            foreach (var item in listaRemover)
            {
                TempSolicitudes.Remove(item);
            }

            foreach (var item in TempSolicitudes)
            {
                listaReasignar.AddRange(solicitudes.FindAll(me => me.NoSolicitud == item.NoSolicitud));
            }

            bool actualizar = true;
            foreach (var item in listaReasignar)
            {
                if (actualizar)
                {
                    var solicitud = new Reasignar().ObtenerSolicitudByFolio(item.Folio);
                    solicitud.Id_Centro_Disp = CAyDReasignar;
                    solicitud.Dt_Fecha_Recepcion = DateTime.Now;
                    actualizar = new Reasignar().ActualizarCAyD(solicitud);

                    /*INSERTAR EVENTO REASIGNAR CAYD DEL TIPO DE PROCESO CAYD EN LOG*/
                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                        Convert.ToInt16(Session["IdDepartamento"]),
                        "CAyD", "REASIGNAR CAYD", item.NoSolicitud,
                        "", "", "CAyD ANTERIOR: " + item.CAyD,
                        "CAyD NUEVO: " + NombreCAyDReasignar);
                }
                else break;
            }
            if (actualizar)
            {
                RadWindowManager1.RadAlert("Las solicitudes seleccionadas fueron reasignadas satisfactoriamente.", 300, 100, "Seleccione",
                               null);
            }
            else { }

            TempSolicitudes.Clear();
            llenarGrid();
            rgSolicitudes.DataBind();
            cmbAsignarCAyD.SelectedIndex = 0;
        }
    }
}