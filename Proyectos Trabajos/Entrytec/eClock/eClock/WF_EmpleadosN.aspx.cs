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

public partial class WF_EmpleadosN : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    private string QueryGrupo = "";
    private string Parametro = "";

    private void Habilitarcontroles()
    {
        Uwg_EC_PERSONAS.Visible = false;
        Chb_PERSONA_BORRADO.Visible = false;
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados))
        {
            if (Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Listado0Grupo) || Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Reportes0Reportes_Empleados0Grupo))
                QueryGrupo = "and (EC_PERSONAS.SUSCRIPCION_ID in (Select EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID from EC_PERMISOS_SUSCRIP where EC_PERMISOS_SUSCRIP.usuario_id = " + Sesion.USUARIO_ID + ") or EC_PERSONAS.grupo_2_id in (Select EC_USUARIOS_grupos_2.grupo_2_id from EC_USUARIOS_grupos_2 where EC_USUARIOS_grupos_2.usuario_id = " + Sesion.USUARIO_ID + ") or EC_PERSONAS.grupo_3_id in (Select EC_USUARIOS_grupos_3.grupo_3_id from EC_USUARIOS_grupos_3 where EC_USUARIOS_grupos_3.usuario_id = " + Sesion.USUARIO_ID + "))";
            else
                WIBtn_Actualizar.Visible = WIBtn_Terminales.Visible = false;

            if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Editar))
                WIBtn_Editar.Visible = false;

            if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Nuevo))
                WIBtn_Nuevo.Visible = WIBtn_Enrolar.Visible = false;

            if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Borrar))
                WIBtn_Borrar.Visible = false;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Parametro = Sesion.Parametros;
        if (Sesion.EsSoloReporte > 0)
        {
            return;
        }
        if (!IsPostBack)
        {
            if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Empleados, true))
            {
                Habilitarcontroles();
                return;
            }
            Sesion.TituloPagina = "Empleados";
            Sesion.DescripcionPagina = "Seleccione un empleado para editarlo, borrarlo ó crear un nuevo empleado";
            Sesion.WF_EmpleadosBus_Query = "";

            CargaDatosDS();
            Uwg_EC_PERSONAS.DataBind();
            //LTitulo.Text = Sesion.TituloPagina;
            //LDescripcion.Text = Sesion.DescripcionPagina;
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Empleados", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
        if (Parametro.Length > 1 && Parametro.Substring(1) != "null")
        {
            int Persona_ID = Convert.ToInt32(Parametro.Substring(1));
            Sesion.WF_Empleados_PERSONA_ID = Persona_ID;
            //tenemos que por lo menos pasarle una query de por lo menos 1 carácter a la variable 
            //WF_EmpleadosBusQuery ya que es variable de session;
            //al iniciar este modulo nos cercioramos de que la variable antes mencionada 
            //tenga una longitud de 0

            Sesion.WF_EmpleadosBus_Query = "select * from EC_PERSONAS";

            if (Parametro[0] == 'E')
            {
                //Agregar ModuloLog***
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Empleados", Persona_ID, "", Sesion.SESION_ID);
                Sesion.Redirige("WF_EmpleadosEd.aspx");
            }
            if (Parametro[0] == 'B')
            {
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Empleados", Persona_ID, "", Sesion.SESION_ID);
                Borrar(Persona_ID);
            }
            if (Parametro[0] == 'T')
            {
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Empleados Terminal", Persona_ID, "", Sesion.SESION_ID);
                Sesion.Redirige("WF_PersonasTerminales.aspx");
            }
            if (Parametro[0] == 'R')
            {
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Empleados Huella", Persona_ID, "", Sesion.SESION_ID);
                //       Sesion.Redirige("eClockDesc.application");
                Sesion.Redirige("eClockDesc.application?SESION_ID=" + Sesion.SESION_ID + "&HUELLAS=1&PERSONA_ID=" + Persona_ID);

            }
        }
    }
    protected DataSet ds = null;
    protected void CargaDatosDS()
    {
        Sesion = CeC_Sesion.Nuevo(this);
        //  CeC_Campos.ReiniciaCampos();
        if (CeC_Config.EsMIRO)
        {
            Wco_EC_PERSONAS_DATOS_TIPO_NOMINA.Visible = true;
            WIBtn_ActualizarTurnos.Visible = true;
            WGB_ActualizarTurnos.Visible = true;
            DataSet Ds = (DataSet)CeC_BD.EjecutaDataSet("SELECT TIPO_NOMINA FROM EC_PERSONAS_DATOS GROUP BY TIPO_NOMINA ");
            if (Ds != null)
            {
                Wco_EC_PERSONAS_DATOS_TIPO_NOMINA.DataSource = Ds;
                Wco_EC_PERSONAS_DATOS_TIPO_NOMINA.DataValueField = "TIPO_NOMINA";
                Wco_EC_PERSONAS_DATOS_TIPO_NOMINA.DataTextField = "TIPO_NOMINA";
                Wco_EC_PERSONAS_DATOS_TIPO_NOMINA.DataBind();
            }
        }
        ds = CeC_Campos.ObtenDataSetTRestriccionesGrupos(Chb_PERSONA_BORRADO.Checked, Sesion, this);
        if (ds != null)
        {
            Uwg_EC_PERSONAS.DataSource = ds;
            Uwg_EC_PERSONAS.DataMember = ds.Tables[0].TableName;
            Uwg_EC_PERSONAS.DataKeyField = CeC_Campos.CampoTE_Llave;
            Uwg_EC_PERSONAS.DataBind();
        }
    }

    protected void Uwg_EC_PERSONAS_DataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        CargaDatosDS();
        Uwg_EC_PERSONAS.DataBind();
    }

    protected void Uwg_EC_PERSONAS_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        Uwg_EC_PERSONAS.Attributes.Add("CellClickHandler", "if (Grid_CellClickHandler" +
                "'Grid',igtbl_getActiveCell('Grid'),0) {return false;})");
        CeC_Grid.AplicaFormato(Uwg_EC_PERSONAS,true,true,false,false);
        //      Grid.Height = Unit.Percentage(140);

    }

    protected void Chb_PERSONA_BORRADO_CheckedChanged(object sender, EventArgs e)
    {
        CargaDatosDS();
        Uwg_EC_PERSONAS.DataBind();
    }
    private int ObtenActivo()
    {
        int Numero_Resgistos = Uwg_EC_PERSONAS.Rows.Count;

        for (int i = 0; i < Numero_Resgistos; i++)
        {

            if (Uwg_EC_PERSONAS.Rows[i].Selected)
            {
                int Id_Empledo = Convert.ToInt32(Uwg_EC_PERSONAS.Rows[i].DataKey);
                return Id_Empledo;
            }
        }
        return -1;
    }
    bool Borrar(int Persona_ID)
    {
        Lbl_Correcto.Text = "";
        Lbl_Error.Text = "";

        try
        {


            if (Persona_ID <= 0)
            {
                Lbl_Error.Text = "Debe de seleccionar una fila";
                return false;
            }
            if (CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET PERSONA_BORRADO = 1 WHERE PERSONA_ID = " + Persona_ID.ToString()) > 0) ;
            {
                Lbl_Correcto.Text = " Empleado " + CeC_BD.ObtenPersonaLinkID(Persona_ID).ToString() + " dado de baja correctamente";

                //Agregar ModuloLog***
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO, "Empleados", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE, Sesion.SESION_ID);
                //*****

                CargaDatosDS();
                Uwg_EC_PERSONAS.DataBind();
                Lbl_Correcto.Text = "El emplado se ha borrado con exito, pero puede recuperarlo";
                return true; ;

            }

            Lbl_Error.Text = "Error : No se pudo dar de baja el empleado";
        }
        catch (Exception ex)
        {
            Lbl_Error.Text = "Error :" + ex.Message;
            return false;
        }
        return false;
    }

    protected void UltraWebGrid1_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        CargaDatosDS();
    }



    protected void Btn_Salir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

        Sesion.Redirige("WF_Login.aspx");
    }
    protected void Btn_Usuario_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

        Sesion.Redirige("WF_UsuariosE.aspx");
    }
    protected void WIBtn_Actualizar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CMd_Base.gActualizaEmpleados(Sesion.SUSCRIPCION_ID, true);
        /*
        CMd_Sicoss Sicoss = new CMd_Sicoss();
        if (Sicoss.Habilitado)
        {
            if (Sicoss.EjecutarUnaVezAlDia())
            {
                Sesion.Redirige("WF_EmpleadosN.aspx.cs");
            }
        }*/
    }
    protected void WIBtn_Nuevo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.WF_Empleados_PERSONA_ID = -1;
        Sesion.WF_EmpleadosBus_Query = "solo para que pase el query";
        Sesion.Redirige("WF_EmpleadosEd.aspx");
    }
    protected void WIBtn_ActualizarTurnos_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            if (Wco_EC_PERSONAS_DATOS_TIPO_NOMINA.DataValue != null)
            {
                if (!CMd_Base.gActualizaTurnos(Sesion.SUSCRIPCION_ID, Wco_EC_PERSONAS_DATOS_TIPO_NOMINA.DataValue.ToString()))
                {
                    Lbl_Correcto.Text = "";
                    Lbl_Error.Text = "Hubo errores al actualizar los turnos de algunos de los empleados, verifique lo siguiente: " +
                                    "El Tipo de Nómina tenga empleados." +
                                    "La suscripción este activa y sea válida." +
                                    "Consulte el log para mayor información.";
                    return;
                }
                Lbl_Error.Text = "";
                Lbl_Correcto.Text = "Se actualizaron correctamente los turnos para los empleados. Consulte el log para mayor información.";
                //LnkBtn_Descarga.Visible = true;
            }
            else
            {
                Lbl_Correcto.Text = "";
                Lbl_Error.Text = "Seleccione del menu despegable un Tipo de Nómina válido.";
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }
    /// <summary>
    /// Descarga el archivo con los resultados de la actualización de Turnos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LnkBtn_Descarga_Click(object sender, EventArgs e)
    {
        Sesion.Redirige("WF_Descarga.aspx?Parametros=ActualizacionTurnos.tmp");
    }
}
