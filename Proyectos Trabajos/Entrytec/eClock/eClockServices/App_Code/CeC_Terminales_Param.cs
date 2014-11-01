using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Esta clase esta diseñada para contener todas las variables de configuracion por 
/// para las terminales del eClock Web, por default la terminal es 0 (Ninguna)
/// </summary>
public class CeC_Terminales_Param
{
    private int m_Terminal_ID = 0;
    public CeC_Terminales_Param()
    {

    }
    public CeC_Terminales_Param(int Terminal_ID)
    {
        TERMINAL_ID = Terminal_ID;
    }
    /// <summary>
    /// Obtiene o establece el identificador de terminal que se usará para cargar o guardar
    /// la configuración
    /// </summary>
    public int TERMINAL_ID
    {
        set { m_Terminal_ID = value; }
        get { return m_Terminal_ID; }
    }
    /// <summary>
    /// Obtiene o establece la dirección IP de la terminal
    /// </summary>
    public string DireccionIP
    {
        get { return CeC_BD.ObtenParamTerminal(TERMINAL_ID, "DireccionIP", ""); }
        set { CeC_BD.GuardaParamTerminal(TERMINAL_ID, "DireccionIP", value); }
    }
    /// <summary>
    /// Obtiene o establece la direccion de MD5Huellas
    /// </summary>
    public string MD5Huellas
    {
        get { return CeC_BD.ObtenParamTerminal(TERMINAL_ID, "MD5Huellas", ""); }
        set { CeC_BD.GuardaParamTerminal(TERMINAL_ID, "MD5Huellas", value); }
    }
    /// <summary>
    /// Obtiene o establece el Hash de la lista de empleados
    /// </summary>
    public string ListaEmpleadosHash
    {
        get { return CeC_BD.ObtenParamTerminal(TERMINAL_ID, "ListaEmpleadosHash", ""); }
        set { CeC_BD.GuardaParamTerminal(TERMINAL_ID, "ListaEmpleadosHash", value); }
    }

    /// <summary>
    /// Obtiene o establece el número de huellas por persona que serán enviadas
    /// a la terminal
    /// </summary>
    public int NO_Huellas_X_Persona
    {
        //Predeterminado 2 huellas
        get { return CeC_BD.ObtenParamTerminal(TERMINAL_ID, "NO_Huellas_X_Persona", 2); }
        set { CeC_BD.GuardaParamTerminal(TERMINAL_ID, "NO_Huellas_X_Persona", value); }
    }
    /// <summary>
    /// Obtiene el valor de AlmacenID
    /// </summary>
    public int ALMACEN_VEC_ID
    {
        get
        {
            string qry = "SELECT ALMACEN_VEC_ID FROM EC_TERMINALES WHERE TERMINAL_ID = " + TERMINAL_ID.ToString();
            int AlmacenID = CeC_BD.EjecutaEscalarInt(qry);
            if (AlmacenID < 0)
                return 0;
            return AlmacenID;
        }
    }
    /// <summary>
    /// Obtiene el valor del sitio hijo, aquel sitio que esta terminal controla por 485 
    /// si es 0 significa que no tiene un sitio hijo
    /// </summary>
    public int SITIO_HIJO_ID
    {
        get { return CeC_BD.EjecutaEscalarInt("SELECT SITIO_HIJO_ID FROM EC_TERMINALES WHERE TERMINAL_ID = " + TERMINAL_ID.ToString() + " "); }
        set { CeC_BD.GuardaParamTerminal(TERMINAL_ID, "SITIO_HIJO_ID", value); }
    }

    public bool TERMINAL_ASISTENCIA
    {
        get
        {
            return CeC_BD.EjecutaEscalarBool("SELECT TERMINAL_ASISTENCIA FROM EC_TERMINALES WHERE TERMINAL_ID = " + TERMINAL_ID.ToString() + " ", false);
        }
    }
    /// <summary>
    /// Obtiene Terminal_Campo_Llave
    /// </summary>
    public string TERMINAL_CAMPO_LLAVE
    {
        get
        {
            return CeC_BD.EjecutaEscalarString("SELECT TERMINAL_CAMPO_LLAVE FROM EC_TERMINALES WHERE TERMINAL_ID = " + TERMINAL_ID.ToString() + " ");
        }
    }
    /// <summary>
    /// Obtiene el Terminal_Campo_Adicional
    /// </summary>
    public string TERMINAL_CAMPO_ADICIONAL
    {
        get
        {
            return CeC_BD.EjecutaEscalarString("SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES WHERE TERMINAL_ID = " + TERMINAL_ID.ToString() + " ");

        }
    }
    /// <summary>
    /// Determina si la terminal es de entrada
    /// </summary>
    public bool TERMINAL_ESENTRADA
    {
        get { return CeC_BD.EjecutaEscalarBool("SELECT TERMINAL_ESENTRADA FROM EC_TERMINALES WHERE TERMINAL_ID = " + TERMINAL_ID.ToString() + " ", true); }
        set { CeC_BD.GuardaParamTerminal(TERMINAL_ID, "TERMINAL_ESENTRADA", Convert.ToInt32(value)); }
    }
    /// <summary>
    /// Determina si la trminal es de salida
    /// </summary>
    public bool TERMINAL_ESSALIDA
    {
        get { return CeC_BD.EjecutaEscalarBool("SELECT TERMINAL_ESSALIDA FROM EC_TERMINALES WHERE TERMINAL_ID = " + TERMINAL_ID.ToString() + " ", true); }
        set { CeC_BD.GuardaParamTerminal(TERMINAL_ID, "TERMINAL_ESSALIDA", Convert.ToInt32(value)); }
    }
    /// <summary>
    /// Indica si se aceptarán los tipos de acceso en equipos ZK software y Anviz
    /// </summary>
    public bool TERMINAL_ACEPTA_TA
    {
        get { return CeC_BD.EjecutaEscalarBool("SELECT TERMINAL_ACEPTA_TA FROM EC_TERMINALES WHERE TERMINAL_ID = " + TERMINAL_ID.ToString() + " ", true); }
        set { CeC_BD.GuardaParamTerminal(TERMINAL_ID, "TERMINAL_ACEPTA_TA", Convert.ToInt32(value)); }
    }
    /// <summary>
    /// Determina si la terminal acepta entrada/salida a comida
    /// </summary>
    public bool TERMINAL_COMIDA
    {
        get { return Convert.ToBoolean(CeC_BD.EjecutaEscalarInt("SELECT TERMINAL_COMIDA FROM EC_TERMINALES WHERE TERMINAL_ID = " + TERMINAL_ID.ToString() + " ")); }
        // set { CeC_BD.GuardaParamTerminal(TERMINAL_ID, "TERMINAL_COMIDA", Convert.ToInt32(value)); }
    }
    /// <summary>
    /// Obtiene o establece la descripcion de la terminal
    /// </summary>
    public string TerminalDescripcion
    {
        get { return CeC_BD.ObtenParamTerminal(TERMINAL_ID, "TerminalDescripcion", ""); }
        set { CeC_BD.GuardaParamTerminal(TERMINAL_ID, "TerminalDescripcion", value); }
    }
}