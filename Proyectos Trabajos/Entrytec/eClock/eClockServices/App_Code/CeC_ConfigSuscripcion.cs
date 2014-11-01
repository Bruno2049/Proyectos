using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Descripción breve de CeC_ConfigSuscripcion
/// </summary>
public class CeC_ConfigSuscripcion : CeC_Config
{
    public int m_SuscripcionID = 0;

    public static CeC_ConfigSuscripcion Nuevo(int UsuarioID)
    {
        CeC_ConfigSuscripcion Ret = new CeC_ConfigSuscripcion();
        Ret.AsignaUsuarioID(UsuarioID);
        return Ret;
    }
    public void AsignaUsuarioID(int UsuarioID)
    {
        m_Usuario_ID = UsuarioID;
    }
    public CeC_ConfigSuscripcion()
    {
    }
    public CeC_ConfigSuscripcion(int SuscripcionID)
        : base(CeC_Suscripcion.ObtenUsuarioID(SuscripcionID))
    {
        m_SuscripcionID = SuscripcionID;
    }
    /// <summary>
    /// Obtiene o establece el valor de configuracion del NombrePersona
    /// </summary>
    public string NombrePersona
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "NombrePersona", "APATERNO +' '+ AMATERNO +' '+ NOMBRE"); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "NombrePersona", value); }
    }

    /// <summary>
    /// regresa el query que obtiene el nombre de la persona ej. CAMPO1 + ' ' + Campo2 + ' ' + Campo3
    /// </summary>
    public string NombrePersona_QRY
    {
        get
        {
            string Variable = "";

            if (CeC_BD.EsOracle)
            {
                string[] Partes = NombrePersona.Split(new char[] { '+' });

                Variable = Partes[Partes.Length - 1];
                for (int Cont = Partes.Length - 2; Cont >= 0; Cont--)
                    Variable = "concat (" + Partes[Cont].Trim() + ", " + Variable + ")";

            }
            else
            {
                Variable = NombrePersona;
            }
            return Variable;
        }
    }

    /// <summary>
    /// Indica si calculara la asistencia en entradas y salidas, solo usar esta propiedad con el operador de la suscripcion
    /// </summary>
    public bool CalucularEntradaSalida
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "CalucularEntradaSalida", false); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "CalucularEntradaSalida", value); }
    }

    /// <summary>
    /// Contiene la cadena separada por comas de las incidencias que son permitidas para asignar como compensaciones, etc
    /// </summary>
    public string TiposIncidenciasHorasExtras
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "TiposIncidenciasHorasExtras", ""); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "TiposIncidenciasHorasExtras", value); }
    }


    public int TipoIncidenciaID_Vacaciones
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "TipoIncidenciaID_Vacaciones", -1); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "TipoIncidenciaID_Vacaciones", value); }
    }

    /// <summary>
    /// Contiene la regla que se usará para el pago de Tiempo extra, ejemplo: 333 (maximo tres horas por dia tres dias a la semana para ser dobles) o 
    /// 9 (las primeras nueve horas a la semana son dobles)
    /// </summary>
    public string HorasExtrasRegla
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "HorasExtrasRegla", "9"); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "HorasExtrasRegla", value); }
    }

    /// <summary>
    /// Contiene el ID del tipo de incidencia usado para las horas extras
    /// </summary>
    public int TipoIncidenciaHorasExtras
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "TipoIncidenciaHorasExtras", 0); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "TipoIncidenciaHorasExtras", value); }
    }

    public int TipoIncidenciaExHoraExtraSimple
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "TipoIncidenciaExHoraExtraSimple", 0); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "TipoIncidenciaExHoraExtraSimple", value); }
    }

    public int TipoIncidenciaExHoraExtraDoble
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "TipoIncidenciaExHoraExtraDoble", 0); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "TipoIncidenciaExHoraExtraDoble", value); }
    }

    public int TipoIncidenciaExHoraExtraTriple
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "TipoIncidenciaExHoraExtraTriple", 0); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "TipoIncidenciaExHoraExtraTriple", value); }
    }

    /// <summary>
    /// Indica que se permitirá capturar más Horas Extras a las calculadas
    /// </summary>
    public bool PermitirMasHorasExtras
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "PermitirMasHorasExtras", true); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "PermitirMasHorasExtras", value); }

    }

    /// <summary>
    /// Contiene el identificador de tipo de incidencia que será usado en caso de que se desee que al 
    /// deber tiempo por tiempo se use el tiempo extra para un pago automático de dicha deuda,
    /// en caso de que no se requiera usar esta opción se deberá asignar 0
    /// </summary>
    public int HoraExtraTxT_TipoIncidenciaID
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "HoraExtraTxT_TipoIncidenciaID", 0); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "HoraExtraTxT_TipoIncidenciaID", value); }
    }

    /// <summary>
    /// Contiene cuantas horas extras se podrán usar por día para el pago automatico de deuda
    /// de Tiempo X Tiempo
    /// </summary>
    public int HoraExtraTxT_MaximoXDia
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "HoraExtraTxT_MaximoXDia", 3); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "HoraExtraTxT_MaximoXDia", value); }

    }

    /// <summary>
    /// Contiene cuantas horas extras se podrán usar por semana para el pago automatico de deuda
    /// de Tiempo X Tiempo
    /// </summary>
    public int HoraExtraTxT_MaxXSemana
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "HoraExtraTxT_MaxXSemana", 9); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "HoraExtraTxT_MaxXSemana", value); }

    }

    /// <summary>
    /// Identificador de Nombre de Periodo que se usará para el calculo (dobles y triples)
    /// de las horas extras
    /// </summary>
    public int PeriodoNID_HorasExtras
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "PeriodoNID_HorasExtras", 1); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "PeriodoNID_HorasExtras", value); }

    }
    /// <summary>
    /// 
    /// Para Oracle el ID es 
    /// </summary>
    public int TipoIncidenciaExPrimaDominical
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "TipoIncidenciaExPrimaDominical", 0); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "TipoIncidenciaExPrimaDominical", value); }
    }
    public string TipoIncidenciaExPrimaDominical_Filtro
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "TipoIncidenciaExPrimaDominical_Filtro", "SELECT PERSONA_ID FROM EC_PERSONAS"); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "TipoIncidenciaExPrimaDominical_Filtro", value); }
    }
    /// <summary>
    /// contiene las incidencias externas que son retardos y se usarán para el enviar las horas de retardo
    /// se separarán por comas
    /// </summary>
    public string TipoIncidenciaSExRetardos
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "TipoIncidenciaSExRetardos", ""); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "TipoIncidenciaSExRetardos", value); }
    }

    /// <summary>
    /// Indica si se mostraran las agrupaciones vacias
    /// </summary>
    public bool MostrarAgrupacionesVacias
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "MostrarAgrupacionesVacias", true); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "MostrarAgrupacionesVacias", value); }
    }

    /// <summary>
    /// Obtiene o establece los campos y el orden que se usarán para la agrupacion
    /// </summary>
    public string CamposAgrupaciones
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "CamposAgrupaciones", ""); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "CamposAgrupaciones", value); }

    }

    /// <summary>
    /// Indica los usuarios ids que si podran modificar las fechas bloqueadas
    /// </summary>
    public string UsuariosIdsModificanFechasBloqueadas
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "UsuariosIdsModificanFechasBloqueadas", ""); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "UsuariosIdsModificanFechasBloqueadas", value); }

    }

    /// <summary>
    /// Indica los usuarios ids que si podran modificar las fechas bloqueadas
    /// </summary>
    public int LC_Empleados
    {
        get { return CeC_Config.ObtenConfigCrypt(USUARIO_ID, "LC_Empleados", 100); }
        set { CeC_Config.GuardaConfigCrypt(USUARIO_ID, "LC_Empleados", value); }
    }
    public int LC_Usuarios
    {
        get { return CeC_Config.ObtenConfigCrypt(USUARIO_ID, "LC_Usuarios", 1); }
        set { CeC_Config.GuardaConfigCrypt(USUARIO_ID, "LC_Usuarios", value); }
    }
    public int LC_Terminales
    {
        get { return CeC_Config.ObtenConfigCrypt(USUARIO_ID, "LC_Terminales", 1); }
        set { CeC_Config.GuardaConfigCrypt(USUARIO_ID, "LC_Terminales", value); }
    }
    /// <summary>
    /// Indica si el nombre de la compañia se mostrara en los reportes
    /// </summary>
    public bool CompaniaMuestraEnReportes
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "CompaniaMuestraEnReportes", true); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "CompaniaMuestraEnReportes", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de CompaniaNombre 
    /// </summary>
    public string CompaniaNombre
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "CompaniaNombre", ""); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "CompaniaNombre", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de CompaniaTelefono
    /// </summary>
    public string CompaniaTelefono
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "CompaniaTelefono", ""); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "CompaniaTelefono", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de CompaniaDireccion
    /// </summary>
    public string CompaniaDireccion
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "CompaniaDireccion", ""); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "CompaniaDireccion", value); }
    }
    /// <summary>
    /// Obtiene o establece el valor de CompaniaURL
    /// </summary>
    public string CompaniaURL
    {
        get { return CeC_Config.ObtenConfig(USUARIO_ID, "CompaniaURL", ""); }
        set { CeC_Config.GuardaConfig(USUARIO_ID, "CompaniaURL", value); }
    }

    public string LEYENDA_REPORTE_ASISTENCIA
    {
        get { return ObtenConfig(USUARIO_ID, "LEYENDA_REPORTE_ASISTENCIA", "Estoy de acuerdo en que los datos son correctos"); }
        set { GuardaConfig(USUARIO_ID, "LEYENDA_REPORTE_ASISTENCIA", value); }
    }

    /// <summary>
    /// Obtiene los Incidencia Ex ID que se podrán enviar a nomina,
    /// por default contiene * que significa que todos los tipos de incidencia ex id podrán ser enviados 
    /// en un envío de incidencias
    /// </summary>
    public string IncidenciasExID_AEnviar
    {
        get { return ObtenConfig(USUARIO_ID, "IncidenciasExID_AEnviar", "*"); }
        set { GuardaConfig(USUARIO_ID, "IncidenciasExID_AEnviar", value); }
    }

    /// <summary>
    /// Obtiene los Incidencia Ex ID que se podrán recibir de nomina,
    /// por default contiene * que significa que todos los tipos de incidencia ex id podrán ser enviados 
    /// en un envío de incidencias
    /// </summary>
    public string IncidenciasExID_ARecibir
    {
        get { return ObtenConfig(USUARIO_ID, "IncidenciasExID_ARecibir", ""); }
        set { GuardaConfig(USUARIO_ID, "IncidenciasExID_ARecibir", value); }
    }

    public string Comida_Campo_Agrupacion
    {
        get { return ObtenConfig(USUARIO_ID, "Comida_Campo_Agrupacion", ""); }
        set { GuardaConfig(USUARIO_ID, "Comida_Campo_Agrupacion", value); }
    }
    public string Monedero_Campo_Agrupacion
    {
        get { return ObtenConfig(USUARIO_ID, "Monedero_Campo_Agrupacion", ""); }
        set { GuardaConfig(USUARIO_ID, "Monedero_Campo_Agrupacion", value); }
    }
    /// <summary>
    /// Alamacena los reportes a los que el usuario quiere acceder. Se separan por coma: (,)
    /// Por Ejemplo: CONSUMO_EMPLEADO,C_EMPLEADO,D_COMIDA
    /// Estos deben de coincidir con los TagString correspondientes.
    /// </summary>
    public string Mostrar_Subtotales
    {
        get { return ObtenConfig(USUARIO_ID, "Mostrar_Subtotales", ""); }
        set { GuardaConfig(USUARIO_ID, "Mostrar_Subtotales", value); }
    }

    /// <summary>
    /// Porcentaje dede donde empieza el valor Aceptable
    /// </summary>
    public int OcupacionAceptable
    {
        get { return ObtenConfig(USUARIO_ID, "OcupacionAceptable", 15); }
        set { GuardaConfig(USUARIO_ID, "OcupacionAceptable", value); }
    }
    public int OcupacionAlerta
    {
        get { return ObtenConfig(USUARIO_ID, "OcupacionAlerta", 30); }
        set { GuardaConfig(USUARIO_ID, "OcupacionAlerta", value); }
    }
    public int OcupacionMinima
    {
        get { return ObtenConfig(USUARIO_ID, "OcupacionMinima", 50); }
        set { GuardaConfig(USUARIO_ID, "OcupacionMinima", value); }
    }

    public bool AutoGuardarRecibosNomina
    {
        get { return ObtenConfig(USUARIO_ID, "AutoGuardarRecibosNomina", true); }
        set { GuardaConfig(USUARIO_ID, "AutoGuardarRecibosNomina", value); }
    }


}