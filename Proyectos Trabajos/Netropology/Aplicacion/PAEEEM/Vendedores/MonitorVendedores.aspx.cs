using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Vendedores;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.Vendedores;
using Telerik.Web.UI;

namespace PAEEEM.Vendedores
{
    public partial class MonitorVendedores : System.Web.UI.Page
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

        private List<TempImagen> fotos
        {
            get
            {
                return Session["ImagenesTemp"] == null
                    ? new List<TempImagen>()
                    : (List<TempImagen>) Session["ImagenesTemp"];
            }
            set { Session["ImagenesTemp"] = value; }
        }

        private List<DatosVendedoresDist> vendedores
        {
            get
            {
                return Session["VENDEDORESD"] == null
                    ? new List<DatosVendedoresDist>()
                    : (List<DatosVendedoresDist>)Session["VENDEDORESD"];
            }
            set { Session["VENDEDORESD"] = value; }
        }
        #endregion

        #region Metodos
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                llenarGridInicial();
                cargarImagenes();
                CatEstatus();
            }
            
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            cargarImagenes();
        }

        private void llenarGridInicial()
        {
            fotos = new List<TempImagen>();
            vendedores = new List<DatosVendedoresDist>();
            var user = (US_USUARIOModel)Session["UserInfo"];
            /*var vendedores*/ vendedores = new RegVendedores().ConsultarVendedoresInicial(user.Id_Departamento);
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
        }

        private void llenarGrid()
        {
            fotos = new List<TempImagen>();
            var user = (US_USUARIOModel)Session["UserInfo"];
            var vendedores = new RegVendedores().ConsultarVendedores(txtCURP.Text, txtNombre.Text, int.Parse(cmbEstatus.SelectedValue), user.Id_Departamento);
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
        }

        private void CatEstatus()
        {
            cmbEstatus.DataSource = new RegVendedores().ObtenerEstatusVendedores();
            cmbEstatus.DataTextField = "Descripcion";
            cmbEstatus.DataValueField = "Cve_Estatus_Vendedor";
            cmbEstatus.DataBind();
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Seleccione", "0"));
            cmbEstatus.SelectedIndex = 0;
        }

        private void cargarImagenes()
        {
            foreach (GridDataItem VARIABLE in rgVendedores.MasterTableView.Items)
            {
                ((ImageButton)VARIABLE.FindControl("verImagen")).Attributes.Add("OnClick", "poponload('" + VARIABLE.Cells[4].Text + "')");
            }

        }
        #endregion

        #region Eventos
        protected void rgVendedores_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                //string curp = e.Item.Cells[4].Text;
                //foto=new TempImagen();
                //foto.CURP = curp;
                //foto.Imagen = fotos.FindAll(me => me.CURP == curp).FirstOrDefault().Imagen;
                
            }
        }
       
        protected void txtLimpiar_Click(object sender, EventArgs e)
        {
            txtCURP.Text = "";
            txtNombre.Text = "";
            cmbEstatus.SelectedIndex = 0;
        }

        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            fotos = new List<TempImagen>();
            var user = (US_USUARIOModel)Session["UserInfo"];
            var vendedores = new RegVendedores().ConsultarVendedores(txtCURP.Text,txtNombre.Text,int.Parse(cmbEstatus.SelectedValue),user.Id_Departamento);
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
            cargarImagenes();
        } 

        protected void rgVendedores_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            llenarGrid();
        }

        protected void rgVendedores_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            llenarGrid();
        }

        protected void rgVendedores_OnSortCommand(object sender, GridSortCommandEventArgs e)
        {
            llenarGrid();
        }

        #endregion
    }
}