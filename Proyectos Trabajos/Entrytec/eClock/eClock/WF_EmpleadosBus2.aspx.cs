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

public partial class WF_EmpleadosBus2 : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    private string QueryGrupo = "";
    private string Parametro = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        WCBotonesEncabezado1.Sesion = Sesion;
        Sesion.TituloPagina = "Búsqueda de Empleados";
        Sesion.DescripcionPagina = "Seleccione un empleado y presione el botón abrir empleado";
        CargaDatosDS();
        Grid.DataBind();
        
        LTitulo.Text = Sesion.TituloPagina;
        LDescripcion.Text = Sesion.DescripcionPagina;
        if(Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Listado0Grupo) || Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Reportes0Reportes_Empleados0Grupo))
        {
            QueryGrupo = "and EC_PERSONAS.SUSCRIPCION_ID in (Select EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID from EC_PERMISOS_SUSCRIP where EC_PERMISOS_SUSCRIP.usuario_id = " + Sesion.USUARIO_ID + ")";
        }
        Parametro = Sesion.Parametros;
        if (Parametro.Length > 1 && Parametro.Substring(1) != "null")
            if (Parametro[0] == 'A')
            {
                //tenemos que por lomenos pasarle una query de porlomenos 1 caracter a la variable 
                //WF_EmpleadosBusQuery ya que es variable de session;
                //al iniciar este modulo nos sercioramos de que la variable antes mencionada 
                //tenga una longitud de 0
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Empleados", Sesion.WF_Empleados_PERSONA_LINK_ID, "", Sesion.SESION_ID);

                Sesion.Redirige(Sesion.WF_EmpleadosBus_Link, Convert.ToInt32(Parametro.Substring(1)));
                return;
            }
        if(!IsPostBack)
        {
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Carga de Desayunos Express", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
        
    }
    protected DataSet ds = null;
    protected void CargaDatosDS()
    {
        Sesion = CeC_Sesion.Nuevo(this);
        ds = CeC_Campos.ObtenDataSetTRestriccionesGrupos(CBEmpBorrados.Checked, Sesion, this);
        if (ds != null)
        {
            Grid.DataSource = ds;
            Grid.DataMember = ds.Tables[0].TableName;
            Grid.DataKeyField = CeC_Campos.CampoTE_Llave;
        }
    }

    protected void Grid_DataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        CargaDatosDS();
        Grid.DataBind();
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        Grid.Attributes.Add("CellClickHandler", "if (Grid_CellClickHandler" +
                "'Grid',igtbl_getActiveCell('Grid'),0) {return false;})");
        CeC_Grid.AplicaFormato(Grid);
        Grid.Height = 280;
    }

    protected void CBEmpBorrados_CheckedChanged1(object sender, EventArgs e)
    {
        CargaDatosDS();
        Grid.DataBind();
    }


}
