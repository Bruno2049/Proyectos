using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WF_ProductosE : System.Web.UI.Page
{
    DS_Productos DSProductos = new DS_Productos();
    DS_ProductosTableAdapters.EC_PRODUCTOSTableAdapter ProdutcosTA = new DS_ProductosTableAdapters.EC_PRODUCTOSTableAdapter();
    DS_Productos.EC_PRODUCTOSRow Fila = null;
    DS_Productos.EC_PRODUCTOSRow Fila2 = null;
    CeC_Sesion Sesion;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Edición de Productos";
        Sesion.DescripcionPagina = "Especifique los datos del Producto";
        ProdutcosTA.FillById(DSProductos.EC_PRODUCTOS, Sesion.WF_Productos_PRODUCTO_ID);
        Fila = DSProductos.EC_PRODUCTOS.NewEC_PRODUCTOSRow();
        if (Sesion.WF_Productos_PRODUCTO_ID > 0)
            Fila2 = DSProductos.EC_PRODUCTOS[0];
        if (!IsPostBack)// Solo se llena cuando la pagina no se llama a si misma
        {
            if (Sesion.WF_Productos_PRODUCTO_ID > 0)
            {
                txtidproducto.Text = Fila2.PRODUCTO_ID.ToString();
                //if (!Fila.IsPRODUCTO_NONull())
                txtnumprod.Text = Fila2.PRODUCTO_NO.ToString();
                //if (!Fila.IsPRODUCTONull())
                txtnombre.Text = Fila2.PRODUCTO.ToString();
                //if (!Fila.IsPRODUCTO_COSTONull())
                txtcosto.Text = Fila2.PRODUCTO_COSTO.ToString();
                if (Fila2.PRODUCTO_BORRADO == 1)
                    CBBorrar.Checked = true;
            }
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Productos", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        this.Response.Redirect("WF_Productos.aspx");
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (txtnombre.Text != "" && txtcosto.Text != "" && txtnumprod.Text != "")
        {
            try
            {
                Fila.PRODUCTO = txtnombre.Text;
                Fila.SESION_ID = Sesion.SESION_ID;
                Fila.PRODUCTO_COSTO = Convert.ToDecimal(txtcosto.Text);
                Fila.PRODUCTO_NO = Convert.ToInt32(txtnumprod.Text);
                Fila.PRODUCTO_BORRADO = Convert.ToInt32(CBBorrar.Checked);
                if (Sesion.WF_Productos_PRODUCTO_ID > 0)
                    Fila2.PRODUCTO_BORRADO = 1;
                Fila.PRODUCTO_ID = CeC_Autonumerico.GeneraAutonumerico(DSProductos.EC_PRODUCTOS.TableName, DSProductos.EC_PRODUCTOS.PRODUCTO_IDColumn.ColumnName);
                DSProductos.EC_PRODUCTOS.AddEC_PRODUCTOSRow(Fila);

                ProdutcosTA.Update(DSProductos.EC_PRODUCTOS);
                Sesion.Redirige("WF_Productos.aspx");
            }
            catch
            {
                LError.Text = "Los cambios no se han podido guardar";
            }
        }
        else
        {
            LError.Text = "Verifique que los campos tengan información";
        }
    }
}
