using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.Alta_Equipos;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;

namespace PAEEEM.SupplierModule
{
    public partial class WebForm1 : Page
    {
        //#region Region de Atributos


        //private List<EquiposBajaEficiencia> ProductosEB
        //{
        //    get { return ViewState["PRODUCTOS_EB"] == null ? new List<EquiposBajaEficiencia>() : (List<EquiposBajaEficiencia>)ViewState["PRODUCTOS_EB"]; }
        //    set { ViewState["PRODUCTOS_EB"] = value; }
        //}

        //#endregion


        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (Page.IsPostBack) return;

        //    gvEquiposBaja.Visible = false;
        //    btnAgregaProduto.Visible = false;

        //    CargaTecnologias();
        //}






        //#region Eventos de Equipo de Baja Eficiencia

        //protected void cboTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var cboTipoProducto = (DropDownList) sender;
        //        GridViewRow fila = (GridViewRow) cboTipoProducto.NamingContainer;

        //        if (fila != null)
        //        {
        //            var cboCapacidad = (DropDownList) fila.FindControl("cboCapacidad");

        //            var idTecnologia = int.Parse(cboTecnologias.SelectedValue);
        //            var idProducto = int.Parse(cboTipoProducto.SelectedValue);

        //            cboCapacidad.DataSource = new OpEquiposAbEficiencia().Capacidad(idTecnologia, idProducto);
        //            cboCapacidad.DataTextField = "Elemento";
        //            cboCapacidad.DataValueField = "IdElemento";
        //            cboCapacidad.DataBind();                   
        //        }



        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}  


        //protected void gvEquiposBaja_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    var idProducto = Convert.ToInt32(gvEquiposBaja.DataKeys[e.RowIndex].Value);


        //    if (idProducto > 0)
        //    {
        //        //OBTENCION DEL PRODUCTOS SELECCIONADO
        //        var productos = ProductosEB;

        //        //ELIMINACION DEL PRODUCTO
        //        productos.RemoveAll(p => p.IdProducto == idProducto);

        //        ProductosEB = productos;
        //        gvEquiposBaja.DataSource = ProductosEB;
        //        gvEquiposBaja.DataBind();
        //    }


        //}

        //protected void btnAgregaProduto_Click(object sender, EventArgs e)
        //{
        //    AgregaProducto();
        //}

        //protected void gvEquiposBaja_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {                              
        //        var idTecnologia = int.Parse(cboTecnologias.SelectedValue);

        //        #region Tipo Producto
        //        var cboTipoProducto = (DropDownList)e.Row.Cells[1].FindControl("cboTipoProducto");

        //        //CARGA CATALAGO DE TIPO PRODUCTO               
        //        cboTipoProducto.DataSource = new OpEquiposAbEficiencia().Productos(idTecnologia);
        //        cboTipoProducto.DataTextField = "Elemento";
        //        cboTipoProducto.DataValueField = "IdElemento";
        //        cboTipoProducto.DataBind();

        //        #endregion

        //    }

        //}


        //#endregion



        //#region Logica de Equipo de Baja Eficiencia

        ////METODO PARA CARGAR INICIALMENTE LAS TECNOLOGIAS
        //private void CargaTecnologias()
        //{
        //    cboTecnologias.DataSource = new OpEquiposAbEficiencia().Tecnologias();
        //    cboTecnologias.DataValueField = "IdElemento";
        //    cboTecnologias.DataTextField = "Elemento";
        //    cboTecnologias.DataBind();
        //}


        //private void AgregaProducto()
        //{
        //    var productos = ProductosEB;

        //    if (productos.Count > 0)
        //    {
        //        var IdUltimoProducto = productos.Count;

        //        productos.Add(new EquiposBajaEficiencia
        //            {
        //                IdProducto = IdUltimoProducto + 1
        //            });

        //    }
        //    else
        //        productos.Add(new EquiposBajaEficiencia {IdProducto = 1});


        //    ProductosEB = productos;



        //    gvEquiposBaja.DataSource = productos;
        //    gvEquiposBaja.DataBind();    

        //}


        //#endregion

        //protected void gvEquiposBaja_DataBound(object sender, EventArgs e)
        //{

        //}

        //protected void btnAgregaTecnologia_Click(object sender, EventArgs e)
        //{
        //    if (cboTecnologias.SelectedIndex > 0)
        //    {
        //        gvEquiposBaja.Visible = true;
        //        gvEquiposBaja.DataSource = null;
        //        gvEquiposBaja.DataBind();
        //        btnAgregaProduto.Visible = true;
        //    }
        //    else
        //    {
        //        gvEquiposBaja.Visible = false;
        //        gvEquiposBaja.DataSource = null;
        //        gvEquiposBaja.DataBind();
        //        btnAgregaProduto.Visible = false;

        //    }
        //}
    }
}