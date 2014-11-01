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

public partial class WF_Productos : System.Web.UI.Page
{
    DS_Productos DSProductos = new DS_Productos();
    DS_ProductosTableAdapters.EC_PRODUCTOSTableAdapter ProductosTA = new DS_ProductosTableAdapters.EC_PRODUCTOSTableAdapter();
    CeC_Sesion Sesion;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Productos";
        Sesion.DescripcionPagina = "Altas Bajas y Modificaciones de Productos";

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Monedero0Productos, true))
        {
            UWGProductos.Visible = false;
            btnborrar.Visible = false;
            btneditar.Visible = false;
            btnnuevo.Visible = false;
            return;
        }
        //**************************************************

        //Agregar Módulo Log
        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Productos", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
    }

    protected void UWGProductos_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        if (CBBorrados.Checked)
            ProductosTA.Fill(DSProductos.EC_PRODUCTOS);
        else
            ProductosTA.FillByNOBORRADO(DSProductos.EC_PRODUCTOS);
        UWGProductos.DataSource = DSProductos.EC_PRODUCTOS;
        UWGProductos.DataMember = DSProductos.EC_PRODUCTOS.TableName;
        UWGProductos.DataKeyField = DSProductos.EC_PRODUCTOS.PRODUCTO_IDColumn.ColumnName;
    }

    protected void UWGProductos_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(UWGProductos, false, false, false, false);
        UWGProductos.Columns.FromKey("SESION_ID").Hidden = true;
    }

    private int ObtenActivo()
    {
        int Numero_Resgistos = UWGProductos.Rows.Count;
        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (UWGProductos.Rows[i].Selected)
            {
                int Id = Convert.ToInt32(UWGProductos.Rows[i].DataKey);
                return Id;
            }
        }
        return -1;
    }

    protected void btnborrar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int id;
        id = ObtenActivo();
        ProductosTA.Aborrado(1, id);
        Sesion.Redirige("WF_Productos.aspx");
    }

    protected void btnnuevo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.WF_Productos_PRODUCTO_ID = -9999; //Cuando el ID Es -9999 Se Sabe Que Es Un Registro Nuevo
        Sesion.Redirige("WF_ProductosE.aspx");
    }

    protected void btneditar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int Numero_Resgistos = UWGProductos.Rows.Count;

        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (UWGProductos.Rows[i].Selected)
            {

                int id;
                id = ObtenActivo();
                string strPromocionNom = Convert.ToString(UWGProductos.Rows[i].Cells[4].Value);
                Sesion.WF_Productos_PRODUCTO_ID = Convert.ToInt32(id); //Se Guarda En Sesion Para Saber cual era
                Sesion.Redirige("WF_ProductosE.aspx");                   //el ID Seleccionado 
                return;
            }
        }
    }
}