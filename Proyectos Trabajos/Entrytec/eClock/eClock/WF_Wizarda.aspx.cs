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

public partial class WF_Wizarda : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Asistente de Configuracion";
            Sesion.DescripcionPagina = "Configure el sistema para que se adapte a sus necesidades";
        Sesion.EsWizard = 1;
        try
        {
            this.Master.FindControl("WC_Menu1").FindControl("mnu_Main").Visible = !Convert.ToBoolean(Sesion.EsWizard);
            this.Master.FindControl("WCBotonesEncabezado1").Visible = !Convert.ToBoolean(Sesion.EsWizard);
        }
        catch { }

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Configuracion, true))
        {
            BGuardarCambios.Visible = false;
            WebPanel1.Visible = false;
            return;
        }
        //**************************************************

        //Agregar Módulo Log
        if (!IsPostBack)
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asistente de Configuración", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

        CeC_Campos Campos = new CeC_Campos();
        if (CeC_Config.MostrarWizardInicio)
        {
            Campos.GuardaConfigCamposW("PERSONA_LINK_ID", "No. Empleado", false, false, false);
            Campos.GuardaConfigCamposW("NOMBRE_COMPLETO", "Nombre Completo", true, true, false);
            Campos.GuardaConfigCamposW("NOMBRE", "Nombre", false, false, false);
            Campos.GuardaConfigCamposW("APATERNO", "Apellido Paterno", false, false, false);
            Campos.GuardaConfigCamposW("AMATERNO", "Apellido Materno", false, false, false);
            Campos.GuardaConfigCamposW("FECHA_NAC", "Fecha de nacimiento", true, true, false);
            Campos.GuardaConfigCamposW("RFC", "RFC", true, true, false);
            Campos.GuardaConfigCamposW("CURP", "CURP", true, true, false);
            Campos.GuardaConfigCamposW("IMSS", "IMSS", true, true, false);
            Campos.GuardaConfigCamposW("ESTUDIOS", "Estudios", true, true, false);
            Campos.GuardaConfigCamposW("SEXO", "Sexo", true, true, true);
            Campos.GuardaConfigCamposW("NACIONALIDAD", "Nacionalidad", true, true, true);
            Campos.GuardaConfigCamposW("FECHA_INGRESO", "Fecha de ingreso", true, true, false);
            Campos.GuardaConfigCamposW("FECHA_BAJA", "Fecha de baja", true, true, false);
            Campos.GuardaConfigCamposW("DP_CALLE_NO", "DP Calle Y No.", true, true, false);
            Campos.GuardaConfigCamposW("DP_COLONIA", "DP Colonia", true, true, false);
            Campos.GuardaConfigCamposW("DP_DELEGACION", "DP Delegacion", true, true, true);
            Campos.GuardaConfigCamposW("DP_CIUDAD", "DP Ciudad", true, true, true);
            Campos.GuardaConfigCamposW("DP_ESTADO", "DP Estado", true, true, true);
            Campos.GuardaConfigCamposW("DP_PAIS", "DP Pais", true, true, true);
            Campos.GuardaConfigCamposW("DP_CP", "DP CP", true, true, true);
            Campos.GuardaConfigCamposW("DP_TELEFONO1", "DP Telefono1", true, true, false);
            Campos.GuardaConfigCamposW("DP_TELEFONO2", "DP Telefono2", true, true, false);
            Campos.GuardaConfigCamposW("DP_CELULAR1", "DP Celular1", true, true, false);
            Campos.GuardaConfigCamposW("DP_CELULAR2", "DP Celular2", true, true, false);
            Campos.GuardaConfigCamposW("DT_CALLE_NO", "DT Calle y No.", true, true, false);
            Campos.GuardaConfigCamposW("DT_COLONIA", "DT Colonia", true, true, false);
            Campos.GuardaConfigCamposW("DT_DELEGACION", "DT Delegacion", true, true, true);
            Campos.GuardaConfigCamposW("DT_CIUDAD", "DT Ciudad", true, true, true);
            Campos.GuardaConfigCamposW("DT_ESTADO", "DT Estado", true, true, true);
            Campos.GuardaConfigCamposW("DT_PAIS", "DT Pais", true, true, true);
            Campos.GuardaConfigCamposW("DT_CP", "DT CP", true, true, false);
            Campos.GuardaConfigCamposW("DT_TELEFONO1", "DT Telefono1", true, true, false);
            Campos.GuardaConfigCamposW("DT_TELEFONO2", "DT Telefono2", true, true, false);
            Campos.GuardaConfigCamposW("DT_CELULAR1", "DT Celular1", true, true, false);
            Campos.GuardaConfigCamposW("DT_CELULAR2", "DT Celular2", true, true, false);
            Campos.GuardaConfigCamposW("CENTRO_DE_COSTOS", "Centro de costos", false, false, true);
            Campos.GuardaConfigCamposW("AREA", "Area", false, false, true);
            Campos.GuardaConfigCamposW("DEPARTAMENTO", "Departamento", false, false, true);
            Campos.GuardaConfigCamposW("PUESTO", "Puesto", false, false, true);
            Campos.GuardaConfigCamposW("GRUPO", "Grupo", true, true, true);
            Campos.GuardaConfigCamposW("NO_CREDENCIAL", "No. Credencial", true, true, false);
            Campos.GuardaConfigCamposW("LINEA_PRODUCCION", "Linea de produccion", true, true, true);
            Campos.GuardaConfigCamposW("CLAVE_EMPL", "Clave de empleado", true, true, true);
            Campos.GuardaConfigCamposW("COMPANIA", "Compania", true, true, true);
            Campos.GuardaConfigCamposW("DIVISION", "Division", true, true, true);
            Campos.GuardaConfigCamposW("REGION", "Region", true, true, true);
            Campos.GuardaConfigCamposW("TIPO_NOMINA", "Tipo de nomina", true, true, true);
            Campos.GuardaConfigCamposW("ZONA", "Zona", true, true, true);
            Campos.GuardaConfigCamposW("CAMPO1", "Campo1", true, true, false);
            Campos.GuardaConfigCamposW("CAMPO2", "Campo2", true, true, false);
            Campos.GuardaConfigCamposW("CAMPO3", "Campo3", true, true, false);
            Campos.GuardaConfigCamposW("CAMPO4", "Campo4", true, true, false);
            Campos.GuardaConfigCamposW("CAMPO5", "Campo5", true, true, false);
            Campos.GuardaConfigCamposW("REGLA_VACA_ID", "Regla de Vacaciones", true, true, false);
            Campos.GuardaConfigCamposW("SUELDO_DIA", "Sueldo Dia", true, true, false);
         /*   CeC_BD.EjecutaEscalar("UPDATE EC_CAMPOS SET CATALOGO_ID = 1 WHERE CATALOGO_ID = 2 OR CATALOGO_ID = 3");
            CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CATALOGO_ID = 2 WHERE CAMPO_ETIQUETA LIKE 'CENTRO DE COSTOS'");*/
            CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CATALOGO_ID = 0 WHERE CAMPO_NOMBRE = 'SUSCRIPCION_NOMBRE'");
            CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CATALOGO_ID = 0 WHERE CAMPO_NOMBRE = 'GRUPO_2_NOMBRE'");
            CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CATALOGO_ID = 0 WHERE CAMPO_NOMBRE = 'GRUPO_3_NOMBRE'");

            CeC_Config.CampoGrupo1 = "EC_PERSONAS_DATOS.CENTRO_DE_COSTOS";
            CeC_Config.NombreGrupo1 = "Centro de costos";
            CeC_Config.CampoGrupo2 = "EC_PERSONAS_DATOS.PUESTO";
            CeC_Config.NombreGrupo2 = "Puesto";

        }
        CeC_BD.CreaRelacionesEmpleados();
        CeC_Campos.RecargaCampos();
        Sesion.Redirige("WF_Wizardd.aspx");
    }
}
