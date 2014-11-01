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
using System.IO;
using System.Drawing;
public partial class WF_EmpleadosEd : System.Web.UI.Page
{
    protected int Persona_ID;
    protected int PersonaLinkID, PersonaLinkID_Ant = -9999;
    protected CeC_Sesion Sesion;
    CeC_SesionBD SesionBD;
    string CamposError = "";

    protected bool CargaCombo(Infragistics.WebUI.WebCombo.WebCombo Combo, string Campo)
    {
        Combo.DataSource = CeC_Campos.CatalogoDT(Campo, Sesion);
        Combo.DataValueField = Campo;
        Combo.DataTextField = Campo;
        Combo.DataBind();
        return true;
    }

    protected void CargaCombos()
    {
        DataSet Ds = CeC_Turnos.ObtenTurnosDSMenu(Sesion.SUSCRIPCION_ID);
        Wco_TURNO_ID.DataSource = Ds;
        Wco_TURNO_ID.DataValueField = "TURNO_ID";
        Wco_TURNO_ID.DataTextField = "TURNO_NOMBRE";
        Wco_TURNO_ID.DataBind();
        Wco_AGRUPACION_NOMBRE.DataSource = CeC_Agrupaciones.ObtenAgrupaciones(Sesion.USUARIO_ID);
        Wco_AGRUPACION_NOMBRE.DataValueField = "AGRUPACION_NOMBRE";
        Wco_AGRUPACION_NOMBRE.DataTextField = "AGRUPACION_NOMBRE";
        Wco_AGRUPACION_NOMBRE.DataBind();
        /*Cmb_CentroCostos.DataSource = CeC_Campos.CatalogoDT("CENTRO_DE_COSTOS", Sesion);
        Cmb_CentroCostos.DataValueField = "CENTRO_DE_COSTOS";
        Cmb_CentroCostos.DataTextField = "CENTRO_DE_COSTOS";
        Cmb_CentroCostos.DataBind();
        Cmb_Area.DataSource = CeC_Campos.CatalogoDT("AREA", Sesion);
        Cmb_Area.DataValueField = "AREA";
        Cmb_Area.DataTextField = "AREA";
        Cmb_Area.DataBind();
        Cmb_Departamento.DataSource = CeC_Campos.CatalogoDT("DEPARTAMENTO", Sesion);
        Cmb_Departamento.DataValueField = "DEPARTAMENTO";
        Cmb_Departamento.DataTextField = "DEPARTAMENTO";
        Cmb_Departamento.DataBind();
        Cmb_Puesto.DataSource = CeC_Campos.CatalogoDT("PUESTO", Sesion);
        Cmb_Puesto.DataValueField = "PUESTO";
        Cmb_Puesto.DataTextField = "PUESTO";
        Cmb_Puesto.DataBind();
        */
        //CargaCombo(Cmb_Agrupacion, "AGRUPACION_NOMBRE");
        CargaCombo(Wco_CENTRO_DE_COSTOS, "CENTRO_DE_COSTOS");
        CargaCombo(Wco_AREA, "AREA");
        CargaCombo(Wco_DEPARTAMENTO, "DEPARTAMENTO");
        CargaCombo(Wco_PUESTO, "PUESTO");

        CargaCombo(Wco_ZONA, "ZONA");
        CargaCombo(Wco_LINEA_PRODUCCION, "LINEA_PRODUCCION");
        CargaCombo(Wco_COMPANIA, "COMPANIA");
        CargaCombo(Wco_REGION, "REGION");
        CargaCombo(Wco_DIVISION, "DIVISION");
        CargaCombo(Wco_GRUPO, "GRUPO");
        CargaCombo(Wco_TIPO_NOMINA, "TIPO_NOMINA");

    
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Lbl_Correcto.Text = "";
        Lbl_Error.Text = "";
        Sesion = CeC_Sesion.Nuevo(this);
        SesionBD = new CeC_SesionBD(Sesion.SESION_ID);
        Lbl_Error.Text = "";
        Lbl_Correcto.Text = "";
        
        //CeC_BD.IniciaBase(true);
        if (Sesion.Parametros.Length > 0)
        {
            Sesion.WF_Empleados_PERSONA_ID = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(CeC.ObtenColumnaSeparador(Sesion.Parametros, "|",0), "~",0));
        }
        if (Sesion.WF_Empleados_PERSONA_ID > 0)
        {
            if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Empleados0Editar, true))
            {
                return;
            }
        }
        else
        {
            if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Empleados0Nuevo, true))
            {

                return;
            }
        }
        //**************************************************

        if (!CeC_Config.FotografiaActiva)
        {
            Image1.Visible = false;
            Btn_CONFIG_USUARIO_VALOR_subir.Visible = false;
            Btn_CONFIG_USUARIO_VALOR_eliminar.Visible = false;
            Fup_CONFIG_USUARIO_VALOR.Visible = false;
        }
        if (Sesion.WF_Empleados_PERSONA_ID > 0)
        {
            Persona_ID = Sesion.WF_Empleados_PERSONA_ID;
        }
        else
        {
            Persona_ID = -1;
        }
        if (!IsPostBack)
        {
            CargaCombos();


            if (Persona_ID > 0)
                SesionBD.FotoNueva = CeC_Personas.ObtenFoto(Persona_ID);
            else
                SesionBD.FotoNueva = null;
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Empleados", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            CargaDatos();
        }
        Sesion.TituloPagina = "Edición de un Empleado";
        Sesion.DescripcionPagina = "Llene los campos con los datos correctos";

    }

    protected bool GuardaCampo(string NombreCampo, object Campo)
    {
        if (Campo == null && NombreCampo == "PERSONA_LINK_ID")
        {
            CamposError += NombreCampo + "(sin valor) ";
            return false;
        }
        bool Guardado = CeC_Personas.GuardaValor(Persona_ID, NombreCampo, Campo, Sesion.SESION_ID);
        if (!Guardado)
            CamposError += NombreCampo + " ";
        return Guardado;

    }

    protected object ObtenCampo(string Nombre)
    {
        Nombre = Nombre.Replace("EC_PERSONAS_DATOS.", " ").Trim();
        switch (Nombre)
        {
            case "PERSONA_ID":
                return Persona_ID;
                break;
            case "PERSONA_LINK_ID":
                PersonaLinkID = CeC.Convierte2Int(Wtx_PERSONA_LINK_ID.Value);
                return Wtx_PERSONA_LINK_ID.Value;
                break;
            case "NOMBRE_COMPLETO":
                return Wtx_NOMBRE_COMPLETO.Value;
                break;
            case "NOMBRE":
                return Wtx_NOMBRE.Value;
                break;
            case "APATERNO":
                return Wtx_APATERNO.Value;
                break;
            case "AMATERNO":
                return Wtx_AMATERNO.Value;
                break;
            case "FECHA_NAC":
                return Wdc_FECHA_NAC.Value;
                break;
            case "RFC":
                return Wtx_RFC.Value;
                break;
            case "CURP":
                return Wtx_RFC.Value;
                break;
            case "IMSS":
                return Wtx_IMSS.Value;
                break;
            case "ESTUDIOS":
                return Wtx_ESTUDIOS.Value;
                break;
            case "SEXO":
                return Wtx_SEXO.Value;
                break;
            case "NACIONALIDAD":
                return Wtx_NACIONALIDAD.Value;
                break;
            case "FECHA_INGRESO":
                return Wdc_FECHA_INGRESO.Value;
                break;
            case "FECHA_BAJA":
                return Wdc_FECHA_BAJA.Value;
                break;
            case "DP_CALLE_NO":
                return Wtx_DP_CALLE_NO.Value;
                break;
            case "DP_COLONIA":
                return Wtx_DP_COLONIA.Value;
                break;
            case "DP_DELEGACION":
                return Wtx_DP_DELEGACION.Value;
                break;
            case "DP_CIUDAD":
                return Wtx_DP_CIUDAD.Value;
                break;
            case "DP_ESTADO":
                return Wtx_DP_ESTADO.Value;
                break;
            case "DP_PAIS":
                return Wtx_DP_PAIS.Value;
                break;
            case "DP_CP":
                return Wtx_DP_CP.Value;
                break;
            case "DP_TELEFONO1":
                return Wtx_DP_TELEFONO1.Value;
                break;
            case "DP_TELEFONO2":
                return Wtx_DP_TELEFONO2.Value;
                break;
            case "DP_CELULAR1":
                return Wtx_DP_CELULAR1.Value;
                break;
            case "DP_CELULAR2":
                return Wtx_DP_CELULAR2.Value;
                break;
            case "DT_CALLE_NO":
                return Wtx_DT_CALLE_NO.Value;
                break;
            case "DT_COLONIA":
                return Wtx_DT_COLONIA.Value;
                break;
            case "DT_DELEGACION":
                return Wtx_DT_DELEGACION.Value;
                break;
            case "DT_CIUDAD":
                return Wtx_DT_CIUDAD.Value;
                break;
            case "DT_ESTADO":
                return Wtx_DT_ESTADO.Value;
                break;
            case "DT_PAIS":
                return Wtx_DT_PAIS.Value;
                break;
            case "DT_CP":
                return Wtx_DT_CP.Value;
                break;
            case "DT_TELEFONO1":
                return Wtx_DT_TELEFONO1.Value;
                break;
            case "DT_TELEFONO2":
                return Wtx_DT_TELEFONO2.Value;
                break;
            case "DT_CELULAR1":
                return Wtx_DT_CELULAR1.Value;
                break;
            case "DT_CELULAR2":
                return Wtx_DT_CELULAR2.Value;
                break;
            case "CENTRO_DE_COSTOS":
                return Wco_CENTRO_DE_COSTOS.DataValue;
                break;
            case "AREA":
                return Wco_AREA.DataValue;
                break;
            case "DEPARTAMENTO":
                return Wco_DEPARTAMENTO.DataValue;
                break;
            case "PUESTO":
                return Wco_PUESTO.DataValue;
                break;
            case "GRUPO":
                return Wco_GRUPO.DataValue;
                break;
            case "NO_CREDENCIAL":
                return Wtx_NO_CREDENCIAL.Value;
                break;
            case "LINEA_PRODUCCION":
                return Wco_LINEA_PRODUCCION.DataValue;
                break;
            case "CLAVE_EMPL":
                return Wtx_CLAVE_EMPL.Value;
                break;
            case "COMPANIA":
                return Wco_COMPANIA.DataValue;
                break;
            case "DIVISION":
                return Wco_DIVISION.DataValue;
                break;
            case "REGION":
                return Wco_REGION.DataValue;
                break;
            case "TIPO_NOMINA":
                return Wco_TIPO_NOMINA.DataValue;
                break;
            case "ZONA":
                return Wco_ZONA.DataValue;
                break;
            case "CAMPO1":
                return Wtx_CAMPO1.Value;
                break;
            case "CAMPO2":
                return Wtx_CAMPO2.Value;
                break;
            case "CAMPO3":
                return Wtx_CAMPO3.Value;
                break;
            case "CAMPO4":
                return Wtx_CAMPO4.Value;
                break;
            case "CAMPO5":
                return Wtx_CAMPO5.Value;
                break;
            case "REGLA_VACA_ID":
                return Wtx_REGLA_VACA_ID.Value;
                break;
            case "SUELDO_DIA":
                return Wtx_SUELDO_DIA.Value;
                break;
            case "AGRUPACION_NOMBRE":
                return Wco_AGRUPACION_NOMBRE.DataValue;
                break;
            case "TURNO_ID":
                return Wco_TURNO_ID.DataValue;
                break;
            case "PERSONA_EMAIL":
                return Wtx_PERSONA_EMAIL.Value;
                break;
            case "PERSONA_BORRADO":
                if (Chb_PERSONA_BORRADO.Checked)
                    return 1;
                else
                    return 0;
                break;
        }
        return null;
    }

    protected void GuardaDatos(bool edterminales)
    {

        try
        {
            int PersonaLinkID = CeC.Convierte2Int(ObtenCampo(CeC_Campos.CampoTE_Llave));
            PersonaLinkID_Ant = CeC_Empleados.ObtenPersona_Link_ID(Persona_ID);
            int nuevo = 0;
            if (PersonaLinkID != PersonaLinkID_Ant && CeC_Personas.ObtenPersonaIDBySuscripcion(PersonaLinkID, Sesion.SUSCRIPCION_ID) > 0)
            {
                Lbl_Error.Text = CeC_Campos.CampoTE_Llave + " ya existe seleccione otro por favor";
                return;
            }
            if (Persona_ID <= 0)
            {
                Persona_ID = CeC_Empleados.Agrega(PersonaLinkID, Sesion);
                if (Persona_ID <= 0)
                {
                    Lbl_Error.Text = "Ha Llegado al Limite de su Version";
                    return;
                }
                nuevo = 1;
            }
            string[] Campos = CeC_Campos.ObtenListaCamposTE().Split(new Char[] { ',' });
            int Errores = 0;
            int Correctos = 0;
            foreach (string Campo in Campos)
            {
                string NombreCampo = Campo.Trim();
                if (GuardaCampo(NombreCampo, ObtenCampo(NombreCampo)))
                    Correctos++;
                else
                {
                    Errores++;
                }
            }
            if (GuardaCampo("PERSONA_EMAIL", ObtenCampo("PERSONA_EMAIL"))) Correctos++; else Errores++;
            if (GuardaCampo("TURNO_ID", ObtenCampo("TURNO_ID"))) Correctos++; else Errores++;
            if (GuardaCampo("PERSONA_BORRADO", ObtenCampo("PERSONA_BORRADO"))) Correctos++; else Errores++;
            if (GuardaCampo("AGRUPACION_NOMBRE", ObtenCampo("AGRUPACION_NOMBRE"))) Correctos++; else Errores++;

            if (GuardaCampo("PERSONA_NOMBRE", ObtenCampo("NOMBRE_COMPLETO"))) Correctos++; else Errores++;

            if (Errores > 0)
            {
                Lbl_Correcto.Visible = false;
                Lbl_Error.Text = "Hubo un error al guardar los siguientes campos:" + CamposError;

            }
            else
            {
                Lbl_Correcto.Text = "Se guardo correctamente el registro";
            }

            if (Wtx_NOMBRE_COMPLETO.Text.Length <= 0)
            {
                //Actualiza el nombre de este empleado
                CeC_Empleados.AcutalizaPersonaNOMBRE_COMPLETO(Persona_ID, Sesion.SUSCRIPCION_ID, Sesion.SESION_ID, true);
                //CeC_BD.ActualizaNombreEmpleado(Persona_ID);
            }
            //Indica que cambiaron los datos de un empleado
            CeC_Asistencias.CambioDatosEmp();

            if (nuevo == 1)
            {
                CeC_BD.AsignaTerminalAuto(Persona_ID);
            }
            if (SesionBD.ActualizoFoto == "True")
            {
                CeC_Personas.AsignaFoto(Persona_ID, SesionBD.FotoNueva);
                SesionBD.ActualizoFoto = "False";
            }
            if (Errores <= 0)
            {
                if (edterminales)
                {
                    Sesion.WF_Empleados_PERSONA_ID = Persona_ID;
                    CeC_Sesion.Redirige(this, "WF_PersonasTerminales.aspx");
                }
                else
                {
                    //CeC_Sesion.Redirige(this, "WF_EmpleadosN.aspx");
                    return;
                }
            }
            else
            {
                Sesion.WF_Empleados_PERSONA_ID = Persona_ID;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            Lbl_Error.Text = "Error:" + ex.Message;
        }
    }
    protected void CargaDatos()
    {
        CeC_Campos.Inicializa();
        string[] Campos = CeC_Campos.ObtenListaCamposTE().Split(new Char[] { ',' });
        string[] EtCampos = new string[Campos.Length];

        foreach (string Campo in Campos)
        {
            string NombreCampo = Campo.Trim();
            AsignaCampos(NombreCampo);
        }
        AsignaCampos("PERSONA_EMAIL");
        AsignaCampos("TURNO_ID");
        AsignaCampos("PERSONA_BORRADO");
        AsignaCampos("AGRUPACION_NOMBRE");
        if (!Chb_EC_PERSONAS_S_HUELLA.Checked)
        {
            Img_HuellaA1.ImageUrl = "WF_Personas_A_Vec.aspx?Parametros=" + Persona_ID + ",6,1";
            Img_HuellaA2.ImageUrl = "WF_Personas_A_Vec.aspx?Parametros=" + Persona_ID + ",6,2";
            Img_HuellaB1.ImageUrl = "WF_Personas_A_Vec.aspx?Parametros=" + Persona_ID + ",7,1";
            Img_HuellaB2.ImageUrl = "WF_Personas_A_Vec.aspx?Parametros=" + Persona_ID + ",7,2";
        }
    }
    protected void AsignaCampos(string Campo)
    {

        Campo = Campo.Replace("EC_PERSONAS_DATOS.", " ").Trim();
        string EtiquetaCampo = CeC_Campos.ObtenEtiqueta(Campo);
        string ValorCampo = CeC_Campos.ObtenValorCampo(Persona_ID, Campo);
        switch (Campo)
        {

            case "PERSONA_LINK_ID":
                Lbl_PERSONA_LINK_ID.Text = EtiquetaCampo;
                Wtx_PERSONA_LINK_ID.Value = ValorCampo;
                PersonaLinkID = CeC.Convierte2Int(Wtx_PERSONA_LINK_ID.Value);
                break;
            case "NOMBRE_COMPLETO":
                Lbl_NOMBRE_COMPLETO.Text = EtiquetaCampo;
                Wtx_NOMBRE_COMPLETO.Value = ValorCampo;
                break;
            case "NOMBRE":
                Lbl_NOMBRE.Text = EtiquetaCampo;
                Wtx_NOMBRE.Value = ValorCampo;
                break;
            case "APATERNO":
                Lbl_APATERNO.Text = EtiquetaCampo;
                Wtx_APATERNO.Value = ValorCampo;
                break;
            case "AMATERNO":
                Lbl_AMATERNO.Text = EtiquetaCampo;
                Wtx_AMATERNO.Value = ValorCampo;
                break;
            case "FECHA_NAC":
                Lbl_FECHA_NAC.Text = EtiquetaCampo;
                if (ValorCampo != "")
                    Wdc_FECHA_NAC.Value = ValorCampo;
                else
                    Wdc_FECHA_NAC.Value = null;
                break;
            case "RFC":
                Lbl_RFC.Text = EtiquetaCampo;
                Wtx_RFC.Value = ValorCampo;
                break;
            case "CURP":
                Lbl_CURP.Text = EtiquetaCampo;
                Wtx_CURP.Value = ValorCampo;
                break;
            case "IMSS":
                Lbl_IMSS.Text = EtiquetaCampo;
                Wtx_IMSS.Value = ValorCampo;
                break;
            case "ESTUDIOS":
                Lbl_ESTUDIOS.Text = EtiquetaCampo;
                Wtx_ESTUDIOS.Value = ValorCampo;
                break;
            case "SEXO":
                Lbl_SEXO.Text = EtiquetaCampo;
                Wtx_SEXO.Value = ValorCampo;
                break;
            case "NACIONALIDAD":
                Lbl_NACIONALIDAD.Text = EtiquetaCampo;
                Wtx_NACIONALIDAD.Value = ValorCampo;
                break;
            case "FECHA_INGRESO":
                Lbl_FECHA_INGRESO.Text = EtiquetaCampo;
                if (ValorCampo != "")
                    Wdc_FECHA_INGRESO.Value = ValorCampo;
                else
                    Wdc_FECHA_INGRESO.Value = null;
                break;
            case "FECHA_BAJA":
                Lbl_FECHA_BAJA.Text = EtiquetaCampo;
                if (ValorCampo != "")
                    Wdc_FECHA_BAJA.Value = ValorCampo;
                else
                    Wdc_FECHA_BAJA.Value = null;
                break;
            case "DP_CALLE_NO":
                Lbl_DP_CALLE_NO.Text = EtiquetaCampo;
                Wtx_DP_CALLE_NO.Value = ValorCampo;
                break;
            case "DP_COLONIA":
                Lbl_DP_COLONIA.Text = EtiquetaCampo;
                Wtx_DP_COLONIA.Value = ValorCampo;
                break;
            case "DP_DELEGACION":
                Lbl_DP_DELEGACION.Text = EtiquetaCampo;
                Wtx_DP_DELEGACION.Value = ValorCampo;
                break;
            case "DP_CIUDAD":
                Lbl_DP_CIUDAD.Text = EtiquetaCampo;
                Wtx_DP_CIUDAD.Value = ValorCampo;
                break;
            case "DP_ESTADO":
                Lbl_DP_ESTADO.Text = EtiquetaCampo;
                Wtx_DP_ESTADO.Value = ValorCampo;
                break;
            case "DP_PAIS":
                Lbl_DP_PAIS.Text = EtiquetaCampo;
                Wtx_DP_PAIS.Value = ValorCampo;
                break;
            case "DP_CP":
                Lbl_DP_CP.Text = EtiquetaCampo;
                Wtx_DP_CP.Value = ValorCampo;
                break;
            case "DP_TELEFONO1":
                Lbl_DP_TELEFONO1.Text = EtiquetaCampo;
                Wtx_DP_TELEFONO1.Value = ValorCampo;
                break;
            case "DP_TELEFONO2":
                Lbl_DP_TELEFONO2.Text = EtiquetaCampo;
                Wtx_DP_TELEFONO2.Value = ValorCampo;
                break;
            case "DP_CELULAR1":
                Lbl_DP_CELULAR1.Text = EtiquetaCampo;
                Wtx_DP_CELULAR1.Value = ValorCampo;
                break;
            case "DP_CELULAR2":
                Lbl_DP_CELULAR2.Text = EtiquetaCampo;
                Wtx_DP_CELULAR2.Value = ValorCampo;
                break;
            case "DT_CALLE_NO":
                Lbl_DT_CALLE_NO.Text = EtiquetaCampo;
                Wtx_DT_CALLE_NO.Value = ValorCampo;
                break;
            case "DT_COLONIA":
                Lbl_DT_COLONIA.Text = EtiquetaCampo;
                Wtx_DT_COLONIA.Value = ValorCampo;
                break;
            case "DT_DELEGACION":
                Lbl_DT_DELEGACION.Text = EtiquetaCampo;
                Wtx_DT_DELEGACION.Value = ValorCampo;
                break;
            case "DT_CIUDAD":
                Lbl_DT_CIUDAD.Text = EtiquetaCampo;
                Wtx_DT_CIUDAD.Value = ValorCampo;
                break;
            case "DT_ESTADO":
                Lbl_DT_ESTADO.Text = EtiquetaCampo;
                Wtx_DT_ESTADO.Value = ValorCampo;
                break;
            case "DT_PAIS":
                Lbl_DT_PAIS.Text = EtiquetaCampo;
                Wtx_DT_PAIS.Value = ValorCampo;
                break;
            case "DT_CP":
                Lbl_DT_CP.Text = EtiquetaCampo;
                Wtx_DT_CP.Value = ValorCampo;
                break;
            case "DT_TELEFONO1":
                Lbl_DT_TELEFONO1.Text = EtiquetaCampo;
                Wtx_DT_TELEFONO1.Value = ValorCampo;
                break;
            case "DT_TELEFONO2":
                Lbl_DT_TELEFONO2.Text = EtiquetaCampo;
                Wtx_DT_TELEFONO2.Value = ValorCampo;
                break;
            case "DT_CELULAR1":
                Lbl_DT_CELULAR1.Text = EtiquetaCampo;
                Wtx_DT_CELULAR1.Value = ValorCampo;
                break;
            case "DT_CELULAR2":
                Lbl_DT_CELULAR2.Text = EtiquetaCampo;
                Wtx_DT_CELULAR2.Value = ValorCampo;
                break;
            case "CENTRO_DE_COSTOS":
                Lbl_CENTRO_DE_COSTOS.Text = EtiquetaCampo;
                Wco_CENTRO_DE_COSTOS.DataValue = ValorCampo;
                break;
            case "AREA":
                Lbl_AREA.Text = EtiquetaCampo;
                Wco_AREA.DataValue = ValorCampo;
                break;
            case "DEPARTAMENTO":
                Lbl_DEPARTAMENTO.Text = EtiquetaCampo;
                Wco_DEPARTAMENTO.DataValue = ValorCampo;
                break;
            case "PUESTO":
                Lbl_PUESTO.Text = EtiquetaCampo;
                Wco_PUESTO.DataValue = ValorCampo;
                break;
            case "GRUPO":
                Lbl_GRUPO.Text = EtiquetaCampo;
                Wco_GRUPO.DataValue = ValorCampo;
                break;
            case "NO_CREDENCIAL":
                Lbl_NO_CREDENCIAL.Text = EtiquetaCampo;
                Wtx_NO_CREDENCIAL.Value = ValorCampo;
                break;
            case "LINEA_PRODUCCION":
                Lbl_LINEA_PRODUCCION.Text = EtiquetaCampo;
                Wco_LINEA_PRODUCCION.DataValue = ValorCampo;
                break;
            case "CLAVE_EMPL":
                Lbl_CLAVE_EMPL.Text = EtiquetaCampo;
                Wtx_CLAVE_EMPL.Value = ValorCampo;
                break;
            case "COMPANIA":
                Lbl_COMPANIA.Text = EtiquetaCampo;
                Wco_COMPANIA.DataValue = ValorCampo;
                break;
            case "DIVISION":
                Lbl_DIVISION.Text = EtiquetaCampo;
                Wco_DIVISION.DataValue = ValorCampo;
                break;
            case "REGION":
                Lbl_REGION.Text = EtiquetaCampo;
                Wco_REGION.DataValue = ValorCampo;
                break;
            case "TIPO_NOMINA":
                Lbl_TIPO_NOMINA.Text = EtiquetaCampo;
                Wco_TIPO_NOMINA.DataValue = ValorCampo;
                break;
            case "ZONA":
                Lbl_ZONA.Text = EtiquetaCampo;
                Wco_ZONA.DataValue = ValorCampo;
                break;
            case "CAMPO1":
                Lbl_CAMPO1.Text = EtiquetaCampo;
                Wtx_CAMPO1.Value = ValorCampo;
                break;
            case "CAMPO2":
                Lbl_CAMPO2.Text = EtiquetaCampo;
                Wtx_CAMPO2.Value = ValorCampo;
                break;
            case "CAMPO3":
                Lbl_CAMPO3.Text = EtiquetaCampo;
                Wtx_CAMPO3.Value = ValorCampo;
                break;
            case "CAMPO4":
                Lbl_CAMPO4.Text = EtiquetaCampo;
                Wtx_CAMPO4.Value = ValorCampo;
                break;
            case "CAMPO5":
                Lbl_CAMPO5.Text = EtiquetaCampo;
                Wtx_CAMPO5.Value = ValorCampo;
                break;
            case "REGLA_VACA_ID":
                Lbl_REGLA_VACA_ID.Text = EtiquetaCampo;
                Wtx_REGLA_VACA_ID.Value = ValorCampo;
                break;
            case "SUELDO_DIA":
                Lbl_SUELDO_DIA.Text = EtiquetaCampo;
                Wtx_SUELDO_DIA.Value = ValorCampo;
                break;
            case "PERSONA_EMAIL":
                Lbl_PERSONA_EMAIL.Text = EtiquetaCampo;
                Wtx_PERSONA_EMAIL.Value = ValorCampo;
                break;
            case "TURNO_ID":
                Lbl_TURNO_ID.Text = EtiquetaCampo;
                //                cmbTurno.DataValue =
                CeC_Grid.SeleccionaID(Wco_TURNO_ID, ValorCampo);
                break;
            case "PERSONA_BORRADO":
                bool borrado;
                Chb_PERSONA_BORRADO.Text = EtiquetaCampo;
                if (ValorCampo != "")
                    borrado = Convert.ToBoolean((Convert.ToInt32(ValorCampo)));
                else
                    borrado = false;
                Chb_PERSONA_BORRADO.Checked = borrado;
                break;
            case "AGRUPACION_NOMBRE":
                Lbl_AGRUPACION_NOMBRE.Text = EtiquetaCampo;
                Wco_AGRUPACION_NOMBRE.DataValue = ValorCampo;
                break;

        }
    }

    protected void btCancelar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
    }

    protected void btGuardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Lbl_Error.Text = "";
        GuardaDatos(false);
    }
    protected void WebImageButton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        string StrFileName = Fup_CONFIG_USUARIO_VALOR.PostedFile.FileName.Substring(Fup_CONFIG_USUARIO_VALOR.PostedFile.FileName.LastIndexOf("\\") + 1);
        string StrFileType = Fup_CONFIG_USUARIO_VALOR.PostedFile.ContentType;
        int IntFileSize = Fup_CONFIG_USUARIO_VALOR.PostedFile.ContentLength;
        int PersonaId = Persona_ID;
        if (IntFileSize <= 0)
            Response.Write(" <font color='Red' size='2'>Uploading of file " + StrFileName + " failed </font>");
        else
        {
            byte[] Datos = null;
            int Len = Fup_CONFIG_USUARIO_VALOR.PostedFile.ContentLength;
            if (Len > 100000)
            {
                Lbl_Error.Text = "Lo siento la imagen es demaciado grande, comprimala (jpg) para poder subirla.";
                return;
            }
            Lbl_Error.Text = "";
            Datos = new byte[Len];
            Fup_CONFIG_USUARIO_VALOR.PostedFile.InputStream.Read(Datos, 0, Len);
            SesionBD.FotoNueva = Datos;
            SesionBD.ActualizoFoto = "True";
            Image1.ImageUrl = ("WF_Personas_FotoN.aspx");
            Lbl_Correcto.Text = "Se subio apropiadamente la foto, no olvide guardar";

            /*
            if (Datos != null && CeC_Personas.AsignaFoto(PersonaId, Datos))
            {
                //Foto Guardada correctamente
                //LCorrecto.Text = "Foto Guardada correctamente";
                MemoryStream img = new MemoryStream(Datos);
                Bitmap imagen = new Bitmap(img);
                Image1.ImageUrl=("WF_Personas_ImaS.aspx");
            }
            else
            {
                //No se pudo guardar la foto
                //LError.Text = "No se pudo guardar la foto";
            }
      
            }
            if (Persona_ID < 1)
            {
                byte[] foto = new byte[File1.PostedFile.ContentLength];
                //SesionBD.FotoNueva = new byte[File1.PostedFile.ContentLength];
                File1.PostedFile.InputStream.Read(foto, 0, File1.PostedFile.ContentLength);
                if (foto != null)
                {
                    //Foto Guardada correctamente
                    //LCorrecto.Text = "Foto Guardada correctamente";
                    MemoryStream img = new MemoryStream(SesionBD.FotoNueva);
                    Bitmap imagen = new Bitmap(img);
                    Image1.ImageUrl = ("WF_Personas_ImaS.aspx");
                }
            }*/
        }
    }
    protected void WebImageButton3_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

        SesionBD.FotoNueva = null;
        SesionBD.ActualizoFoto = "True";
        Image1.ImageUrl = ("WF_Personas_FotoN.aspx");
    }
    protected void WebTextEdit1_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
    {

    }
    protected void cmbTurno_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_TURNO_ID);
    }

    protected void Cmb_Agrupacion_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_AGRUPACION_NOMBRE);
    }
    protected void Cmb_Puesto_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_PUESTO);
    }
    protected void Cmb_Departamento_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_DEPARTAMENTO);
    }
    protected void Cmb_Area_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_AREA);
    }
    protected void Cmb_CentroCostos_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_CENTRO_DE_COSTOS);
    }
    protected void Cmb_Compania_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_COMPANIA);
    }
    protected void Cmb_Division_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_DIVISION);
    }
    protected void Cmb_Region_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_REGION);
    }
    protected void Cmb_Zona_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_ZONA);
    }
    protected void Cmb_LineaProduccion_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_LINEA_PRODUCCION);
    }
    protected void Cmb_Grupo_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_GRUPO);
    }
    protected void Cmb_TipoNomina_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Wco_TIPO_NOMINA);
    }
    protected void WIBtn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            //	LimpiaMsg();
            if (Wtx_PERSONA_LINK_ID_S_HUELLA.Text == "")
            {
                Lbl_Error_Huella.Text = "No eligio al empleado";
                Lbl_Correcto_Huella.Text = "";
                //                    Sesion.WF_EmpleadosFil(false, false, false, "Muestra Resultados", "Filtro-Empleados para Listado sin Huella", "WF_PersonasSHuella.aspx?Parametros=1", "Filtro-Empleados para Listado sin Huella", false, true, false);
                return;
            }
            if (Wtx_PERSONA_LINK_ID_S_HUELLA.Text != "")
            {
                int Persona_ID = CeC_Personas.ObtenPersonaID(CeC.Convierte2Int(Wtx_PERSONA_LINK_ID_S_HUELLA.Value), Sesion.USUARIO_ID);

                if (Persona_ID < 0)
                {
                    Lbl_Error_Huella.Text = "Empleado no registrado";
                    Lbl_Correcto_Huella.Text = "";
                    return;
                }
                if (CeC_BD.EjecutaEscalarInt("SELECT COUNT(*) FROM EC_PERSONAS_S_HUELLA WHERE PERSONA_ID =" + Persona_ID) < 1)
                {
                    if (CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_S_HUELLA (PERSONA_ID, PERSONA_S_HUELLA_FECHA,PERSONA_S_HUELLA_CLAVE) VALUES(" + Persona_ID.ToString() + "," + CeC_BD.SqlFechaHora(DateTime.Now) + ",'" + Wtx_PERSONA_S_HUELLA_CLAVE.Text + "')") <= 0)
                    {
                        Lbl_Error_Huella.Text = "No se pudo agregar el empleado, posiblemente ya se encuentre registrado";
                        Lbl_Correcto_Huella.Text = "";
                    }
                    else
                    {
                        Lbl_Correcto_Huella.Text = "Empleado '" + CeC_BD.EjecutaEscalarString("SELECT PERSONA_NOMBRE FROM EC_PERSONAS WHERE PERSONA_ID = " + Persona_ID.ToString()) + "' agregado correctamente";
                        //ActualizaGrid();
                        Lbl_Correcto_Huella.Visible = true;
                        Lbl_Error_Huella.Text = "";
                        Wtx_PERSONA_LINK_ID_S_HUELLA.Text = Wtx_PERSONA_LINK_ID_S_HUELLA.Text;
                    }
                }
                else
                {
                    Lbl_Error_Huella.Text = "El Empleado " + Wtx_PERSONA_LINK_ID_S_HUELLA.Value.ToString() + 
                        " ya se encuentra en el listado de personas sin huella, fue agregado el dia: " + 
                        CeC_BD.EjecutaEscalarDateTime("SELECT PERSONA_S_HUELLA_FECHA FROM EC_PERSONAS_S_HUELLA WHERE PERSONA_ID =" + Persona_ID) + 
                        "\nPuede editar la clave desde el menu de Terminales";
                    Wtx_PERSONA_S_HUELLA_CLAVE.Text = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_S_HUELLA_CLAVE FROM EC_PERSONAS_S_HUELLA WHERE PERSONA_ID =" + Persona_ID).ToString();
                    if (Wtx_PERSONA_S_HUELLA_CLAVE.Text == "-9999")
                    {
                        Wtx_PERSONA_S_HUELLA_CLAVE.Text = "Introduzca una clave numérica valida";
                    }
                    else
                    {
                        Wtx_PERSONA_LINK_ID_S_HUELLA.Text = Wtx_PERSONA_LINK_ID_S_HUELLA.Text;
                    }
                    Lbl_Correcto_Huella.Text = "";
                }
            }
        }
        catch (System.Exception ex)
        {
            Lbl_Error_Huella.Text = ex.Message;
            return;
        }
    }
    protected void Chb_SinHuella_CheckedChanged(object sender, EventArgs e)
    {
        Img_HuellaA1.Visible = !Chb_EC_PERSONAS_S_HUELLA.Checked;
        Img_HuellaA2.Visible = !Chb_EC_PERSONAS_S_HUELLA.Checked;
        Img_HuellaB1.Visible = !Chb_EC_PERSONAS_S_HUELLA.Checked;
        Img_HuellaB2.Visible = !Chb_EC_PERSONAS_S_HUELLA.Checked;
        Lbl_Img_HuellaA1.Visible = !Chb_EC_PERSONAS_S_HUELLA.Checked;
        Lbl_Img_HuellaA2.Visible = !Chb_EC_PERSONAS_S_HUELLA.Checked;
        Lbl_Img_HuellaB1.Visible = !Chb_EC_PERSONAS_S_HUELLA.Checked;
        Lbl_Img_HuellaB2.Visible = !Chb_EC_PERSONAS_S_HUELLA.Checked;

        Wtx_PERSONA_LINK_ID_S_HUELLA.Visible = Chb_EC_PERSONAS_S_HUELLA.Checked;
        Wtx_PERSONA_S_HUELLA_CLAVE.Visible = Chb_EC_PERSONAS_S_HUELLA.Checked;
        Lbl_Clave.Visible = Chb_EC_PERSONAS_S_HUELLA.Checked;
        Lbl_Empleado.Visible = Chb_EC_PERSONAS_S_HUELLA.Checked;
        WIBtn_Guardar.Visible = Chb_EC_PERSONAS_S_HUELLA.Checked;

        Wtx_PERSONA_LINK_ID_S_HUELLA.Text = Wtx_PERSONA_LINK_ID.Text;
        Wtx_PERSONA_S_HUELLA_CLAVE.Text = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_S_HUELLA_CLAVE FROM EC_PERSONAS_S_HUELLA WHERE PERSONA_ID =" + Persona_ID).ToString();

        if (Wtx_PERSONA_S_HUELLA_CLAVE.Text == "-9999")
        {
            Wtx_PERSONA_S_HUELLA_CLAVE.Text = "Entre clave numérica";
        }
        else
        {
            Wtx_PERSONA_LINK_ID_S_HUELLA.Text = Wtx_PERSONA_LINK_ID_S_HUELLA.Text;
        }

        if (Chb_EC_PERSONAS_S_HUELLA.Checked)
        {
            Lbl_Error.Text = "";
            Lbl_Correcto.Text = "";
        }
    }
}

