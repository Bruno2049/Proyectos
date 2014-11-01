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

public partial class WF_Sitios : System.Web.UI.Page
{
    //DS_Sitios DS;
    CeC_Sesion Sesion;
    //DS_SitiosTableAdapters.EC_SITIOSTableAdapter TA;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.Redirige("WF_Tabla.aspx?Parametros=EC_SITIOS");
        //// Permisos****************************************
        //if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Sitios, true))
        //{
        //    Uwg_EC_SITIOS.Visible = false;
        //    Chb_EC_SITIOS.Visible = false;
        //    WIBtn_Borrar.Visible = false;
        //    WIBtn_Nuevo.Visible = false;
        //    WIBtn_Editar.Visible = false;
        //    return;
        //}
        ////**************************************************
        //DS = new DS_Sitios();
        //TA = new DS_SitiosTableAdapters.EC_SITIOSTableAdapter();
        //Sesion.TituloPagina = "Sitios";
        //Sesion.DescripcionPagina = "Seleccione un sitio para borrarlo o editarlo, o cree un sitio nuevo";
        //if (!IsPostBack)
        //{
        //    TA.Fill(DS.EC_SITIOS, Convert.ToDecimal(Chb_EC_SITIOS.Checked));
        //    //Agregar Módulo Log
        //    Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Sitios", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        //}
    }

    //protected void BAgregar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    //{
    //    Sesion.WF_Sitios_Sitio_ID = -1;
    //    Sesion.Redirige("WF_SitiosE.aspx?SuscripcionID=" + Sesion.SuscripcionParametro);
    //}

    //protected void BEditar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    //{
    //    int Numero_registros = Uwg_EC_SITIOS.Rows.Count;

    //    for (int i = 0; i < Numero_registros; i++)
    //    {

    //        if (Uwg_EC_SITIOS.Rows[i].Selected)
    //        {
    //            int Sitio_ID = Convert.ToInt32(Uwg_EC_SITIOS.Rows[i].Cells[0].Value);
    //            Sesion.WF_Sitios_Sitio_ID = Sitio_ID;
    //            Sesion.Redirige("WF_SitiosE.aspx");
    //            return;
    //        }
    //    }
    //    Lbl_Error.Text = "Debes de seleccionar una fila";
    //}

    //protected void BBorrar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    //{
    //    TA = new DS_SitiosTableAdapters.EC_SITIOSTableAdapter();
    //    int Numero_resgistros = Uwg_EC_SITIOS.Rows.Count;
    //    for (int i = 0; i < Numero_resgistros; i++)
    //    {
    //        if (Uwg_EC_SITIOS.Rows[i].Selected)
    //        {
    //            int Sitio_ID = Convert.ToInt32(Uwg_EC_SITIOS.Rows[i].Cells[0].Value);
    //            TA.Aborrado(DS.EC_SITIOS, Sitio_ID);
    //        }
    //    }
    //    Uwg_EC_SITIOS.DataBind();
    //}

    //protected void CBBorrados_CheckedChanged(object sender, EventArgs e)
    //{
    //    ActualizaDatos();
    //}

    //protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    //{
    //    CeC_Grid.AplicaFormato(Uwg_EC_SITIOS, true, false, false, true);
    //}
    //void ActualizaDatos()
    //{
    //    try
    //    {
    //        TA = new DS_SitiosTableAdapters.EC_SITIOSTableAdapter();
    //        DS = new DS_Sitios();

    //        TA.FillBySuscripcionID(DS.EC_SITIOS, Convert.ToDecimal(Chb_EC_SITIOS.Checked), Sesion.SuscripcionParametro);
    //        Uwg_EC_SITIOS.DataSource = DS.EC_SITIOS;
    //    }
    //    catch { }
    //}
    //protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    //{
    //    Sesion = CeC_Sesion.Nuevo(this);
    //    ActualizaDatos();
    //    CeC_Grid.AplicaFormato(Uwg_EC_SITIOS, true, false, false, true);
    //}
}