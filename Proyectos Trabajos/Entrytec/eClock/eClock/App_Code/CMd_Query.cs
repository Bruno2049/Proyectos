using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

/// <summary>
/// Descripción breve de CMd_Query
/// </summary>
public class CMd_Query : CMd_Base
{
    public CMd_Query()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Obtiene el nombre del módulo
    /// </summary>
    /// <returns></returns>
    public override string LeeNombre()
    {
        return "Permite la ejecucion de sentencias SQL en una accion especifica";
    }
    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez al día
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezAlDia()
    {
        if (EjecutarUnaVezAlDia_Qry_1.Length > 0)
            CeC_BD.EjecutaComando(EjecutarUnaVezAlDia_Qry_1);
        if (EjecutarUnaVezAlDia_Qry_2.Length > 0)
            CeC_BD.EjecutaComando(EjecutarUnaVezAlDia_Qry_2);
        if (EjecutarUnaVezAlDia_Qry_3.Length > 0)
            CeC_BD.EjecutaComando(EjecutarUnaVezAlDia_Qry_3);
        if (EjecutarUnaVezAlDia_Qry_4.Length > 0)
            CeC_BD.EjecutaComando(EjecutarUnaVezAlDia_Qry_4);
        if (EjecutarUnaVezAlDia_Qry_5.Length > 0)
            CeC_BD.EjecutaComando(EjecutarUnaVezAlDia_Qry_5);
        return true;

    }

    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez cada hora
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezCadaHora()
    {

        if (EjecutarUnaVezCadaHora_Qry_1.Length > 0)
            CeC_BD.EjecutaComando(EjecutarUnaVezCadaHora_Qry_1);
        if (EjecutarUnaVezCadaHora_Qry_2.Length > 0)
            CeC_BD.EjecutaComando(EjecutarUnaVezCadaHora_Qry_2);
        if (EjecutarUnaVezCadaHora_Qry_3.Length > 0)
            CeC_BD.EjecutaComando(EjecutarUnaVezCadaHora_Qry_3);
        if (EjecutarUnaVezCadaHora_Qry_4.Length > 0)
            CeC_BD.EjecutaComando(EjecutarUnaVezCadaHora_Qry_4);
        if (EjecutarUnaVezCadaHora_Qry_5.Length > 0)
            CeC_BD.EjecutaComando(EjecutarUnaVezCadaHora_Qry_5);
        return true;
    }

    public override bool ActualizaEmpleados(int SuscripcionID, bool Manual)
    {
        if (ActualizaEmpleados_Qry_1.Length > 0)
            CeC_BD.EjecutaComando(ActualizaEmpleados_Qry_1);
        if (ActualizaEmpleados_Qry_2.Length > 0)
            CeC_BD.EjecutaComando(ActualizaEmpleados_Qry_2);
        if (ActualizaEmpleados_Qry_3.Length > 0)
            CeC_BD.EjecutaComando(ActualizaEmpleados_Qry_3);
        if (ActualizaEmpleados_Qry_4.Length > 0)
            CeC_BD.EjecutaComando(ActualizaEmpleados_Qry_4);
        if (ActualizaEmpleados_Qry_5.Length > 0)
            CeC_BD.EjecutaComando(ActualizaEmpleados_Qry_5);
        return true;
    }

    public override bool ActualizaTiposIncidencias(int SuscripcionID)
    {
        if (ActualizaTiposIncidencias_Qry_1.Length > 0)
            CeC_BD.EjecutaComando(ActualizaTiposIncidencias_Qry_1);
        if (ActualizaTiposIncidencias_Qry_2.Length > 0)
            CeC_BD.EjecutaComando(ActualizaTiposIncidencias_Qry_2);
        if (ActualizaTiposIncidencias_Qry_3.Length > 0)
            CeC_BD.EjecutaComando(ActualizaTiposIncidencias_Qry_3);
        if (ActualizaTiposIncidencias_Qry_4.Length > 0)
            CeC_BD.EjecutaComando(ActualizaTiposIncidencias_Qry_4);
        if (ActualizaTiposIncidencias_Qry_5.Length > 0)
            CeC_BD.EjecutaComando(ActualizaTiposIncidencias_Qry_5);
        return true;
    }

    string m_EjecutarUnaVezAlDia_Qry_1 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("EjecutarUnaVezAlDia_Qry_1")]
    public string EjecutarUnaVezAlDia_Qry_1
    {
        get { return m_EjecutarUnaVezAlDia_Qry_1; }
        set { m_EjecutarUnaVezAlDia_Qry_1 = value; }
    }
    string m_EjecutarUnaVezAlDia_Qry_2 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("EjecutarUnaVezAlDia_Qry_2")]
    public string EjecutarUnaVezAlDia_Qry_2
    {
        get { return m_EjecutarUnaVezAlDia_Qry_2; }
        set { m_EjecutarUnaVezAlDia_Qry_2 = value; }
    }

    string m_EjecutarUnaVezAlDia_Qry_3 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("EjecutarUnaVezAlDia_Qry_3")]
    public string EjecutarUnaVezAlDia_Qry_3
    {
        get { return m_EjecutarUnaVezAlDia_Qry_3; }
        set { m_EjecutarUnaVezAlDia_Qry_3 = value; }
    }
    string m_EjecutarUnaVezAlDia_Qry_4 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("EjecutarUnaVezAlDia_Qry_4")]
    public string EjecutarUnaVezAlDia_Qry_4
    {
        get { return m_EjecutarUnaVezAlDia_Qry_4; }
        set { m_EjecutarUnaVezAlDia_Qry_4 = value; }
    }

    string m_EjecutarUnaVezAlDia_Qry_5 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("EjecutarUnaVezAlDia_Qry_5")]
    public string EjecutarUnaVezAlDia_Qry_5
    {
        get { return m_EjecutarUnaVezAlDia_Qry_5; }
        set { m_EjecutarUnaVezAlDia_Qry_5 = value; }
    }


    string m_EjecutarUnaVezCadaHora_Qry_1 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("EjecutarUnaVezCadaHora_Qry_1")]
    public string EjecutarUnaVezCadaHora_Qry_1
    {
        get { return m_EjecutarUnaVezCadaHora_Qry_1; }
        set { m_EjecutarUnaVezCadaHora_Qry_1 = value; }
    }
    string m_EjecutarUnaVezCadaHora_Qry_2 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("EjecutarUnaVezCadaHora_Qry_2")]
    public string EjecutarUnaVezCadaHora_Qry_2
    {
        get { return m_EjecutarUnaVezCadaHora_Qry_2; }
        set { m_EjecutarUnaVezCadaHora_Qry_2 = value; }
    }

    string m_EjecutarUnaVezCadaHora_Qry_3 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("EjecutarUnaVezCadaHora_Qry_3")]
    public string EjecutarUnaVezCadaHora_Qry_3
    {
        get { return m_EjecutarUnaVezCadaHora_Qry_3; }
        set { m_EjecutarUnaVezCadaHora_Qry_3 = value; }
    }
    string m_EjecutarUnaVezCadaHora_Qry_4 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("EjecutarUnaVezCadaHora_Qry_4")]
    public string EjecutarUnaVezCadaHora_Qry_4
    {
        get { return m_EjecutarUnaVezCadaHora_Qry_4; }
        set { m_EjecutarUnaVezCadaHora_Qry_4 = value; }
    }

    string m_EjecutarUnaVezCadaHora_Qry_5 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("EjecutarUnaVezCadaHora_Qry_5")]
    public string EjecutarUnaVezCadaHora_Qry_5
    {
        get { return m_EjecutarUnaVezCadaHora_Qry_5; }
        set { m_EjecutarUnaVezCadaHora_Qry_5 = value; }
    }

    string m_ActualizaEmpleados_Qry_1 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("ActualizaEmpleados_Qry_1")]
    public string ActualizaEmpleados_Qry_1
    {
        get { return m_ActualizaEmpleados_Qry_1; }
        set { m_ActualizaEmpleados_Qry_1 = value; }
    }
    string m_ActualizaEmpleados_Qry_2 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("ActualizaEmpleados_Qry_2")]
    public string ActualizaEmpleados_Qry_2
    {
        get { return m_ActualizaEmpleados_Qry_2; }
        set { m_ActualizaEmpleados_Qry_2 = value; }
    }

    string m_ActualizaEmpleados_Qry_3 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("ActualizaEmpleados_Qry_3")]
    public string ActualizaEmpleados_Qry_3
    {
        get { return m_ActualizaEmpleados_Qry_3; }
        set { m_ActualizaEmpleados_Qry_3 = value; }
    }
    string m_ActualizaEmpleados_Qry_4 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("ActualizaEmpleados_Qry_4")]
    public string ActualizaEmpleados_Qry_4
    {
        get { return m_ActualizaEmpleados_Qry_4; }
        set { m_ActualizaEmpleados_Qry_4 = value; }
    }

    string m_ActualizaEmpleados_Qry_5 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("ActualizaEmpleados_Qry_5")]
    public string ActualizaEmpleados_Qry_5
    {
        get { return m_ActualizaEmpleados_Qry_5; }
        set { m_ActualizaEmpleados_Qry_5 = value; }
    }



    string m_ActualizaTiposIncidencias_Qry_1 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("ActualizaTiposIncidencias_Qry_1")]
    public string ActualizaTiposIncidencias_Qry_1
    {
        get { return m_ActualizaTiposIncidencias_Qry_1; }
        set { m_ActualizaTiposIncidencias_Qry_1 = value; }
    }
    string m_ActualizaTiposIncidencias_Qry_2 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("ActualizaTiposIncidencias_Qry_2")]
    public string ActualizaTiposIncidencias_Qry_2
    {
        get { return m_ActualizaTiposIncidencias_Qry_2; }
        set { m_ActualizaTiposIncidencias_Qry_2 = value; }
    }

    string m_ActualizaTiposIncidencias_Qry_3 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("ActualizaTiposIncidencias_Qry_3")]
    public string ActualizaTiposIncidencias_Qry_3
    {
        get { return m_ActualizaTiposIncidencias_Qry_3; }
        set { m_ActualizaTiposIncidencias_Qry_3 = value; }
    }
    string m_ActualizaTiposIncidencias_Qry_4 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("ActualizaTiposIncidencias_Qry_4")]
    public string ActualizaTiposIncidencias_Qry_4
    {
        get { return m_ActualizaTiposIncidencias_Qry_4; }
        set { m_ActualizaTiposIncidencias_Qry_4 = value; }
    }

    string m_ActualizaTiposIncidencias_Qry_5 = "";
    [Description("Sentencia SQL que se ejecutará una vez al día")]
    [DisplayNameAttribute("ActualizaTiposIncidencias_Qry_5")]
    public string ActualizaTiposIncidencias_Qry_5
    {
        get { return m_ActualizaTiposIncidencias_Qry_5; }
        set { m_ActualizaTiposIncidencias_Qry_5 = value; }
    }

}
