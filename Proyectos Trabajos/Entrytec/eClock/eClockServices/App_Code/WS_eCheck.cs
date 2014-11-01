using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// Descripción breve de WS_eCheck
/// </summary>


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.None)]
public class WS_eCheck : System.Web.Services.WebService
{

    public enum TipoAccesos
    {
        No_definido = 0,
        Correcto,
        Entrada,
        Salida,
        Salida_a_Comer,
        Regreso_de_comer,
        Incorrecto
    }
    public enum Tipo_DEXTRAS
    {
        No_definido = 0,
        Accesos,
        Huellas
    }



    public WS_eCheck()
    {

    }

    /// <summary>
    /// Valida el Ussuario y el Password del usuario, que se toma de la tabla EC_USUARIOS
    /// </summary>
    /// <param name="Usuario">Identificador del Usuario</param>
    /// <param name="Password">Password del Usuario</param>
    /// <returns>Regersa el ID del usuario si la operación se realizo con exito, de lo contrario regresa -9999</returns>
    [WebMethod(Description = "Inicia sesión con encriptación", EnableSession = true)]
    public int ValidarUsuarioCrypt(string Usuario, string PasswordEncriptado)
    {
        if (!CeC_BD.EstaeClockListo())
            return -9998;
        int UsuarioId = CeC_Sesion.ValidarUsuarioCrypt(Usuario, PasswordEncriptado);
        if (UsuarioId < 0)
            return -9999;
        else
        {
            if (CrearSesion(UsuarioId, 0) > 0)
                return UsuarioId;
            else
                return -9999;

        }
    }

    /// <summary>
    /// Valida el Ussuario y el Password del usuario, que se toma de la tabla EC_USUARIOS
    /// </summary>
    /// <param name="Usuario">Identificador del Usuario</param>
    /// <param name="Password">Password del Usuario</param>
    /// <returns>Regersa el ID del usuario si la operación se realizo con exito, de lo contrario regresa -9999</returns>
    [WebMethod(Description = "Inicia sesión", EnableSession = true)]
    public int ValidarUsuario(string Usuario, string Password)
    {
        return ValidarUsuarioTerminal(Usuario, Password, 0);
    }

    /// <summary>
    /// Valida el Ussuario y el Password del usuario, que se toma de la tabla EC_USUARIOS
    /// </summary>
    /// <param name="Usuario">Identificador del Usuario</param>
    /// <param name="Password">Password del Usuario</param>
    /// <returns>Regersa el ID del usuario si la operación se realizo con exito, de lo contrario regresa -9999</returns>
    [WebMethod(Description = "Inicia sesión con TerminalID", EnableSession = true)]
    public int ValidarUsuarioTerminal(string Usuario, string Password, int Terminal_ID)
    {
        if (!CeC_BD.EstaeClockListo())
            return -9998;
        int UsuarioId = CeC_Sesion.ValidarUsuario(Usuario, Password);
        if (UsuarioId < 0)
            return -9999;
        else
        {
            if (CrearSesion(UsuarioId, Terminal_ID) > 0)
                return UsuarioId;
            else
                return -9999;

        }
    }
    public string HexByte2String(byte Hex)
    {
        if (Hex < 0)
            return "0";
        if (Hex > 16)
            return "F";
        byte[] Car = new byte[1];
        Car[0] = Convert.ToByte('0');
        int Numero = 0;
        if (Hex < 10)
            Numero = Hex + Car[0];

        Car[0] = Convert.ToByte('A');
        if (Hex >= 10)
            Numero = (Hex - 10) + Car[0];
        string Str = "";
        Str += Convert.ToChar(Numero);
        return Str;
    }
    public string Bcd2String(byte[] Arreglo, int Pos, int Len)
    {
        string Texto = "";
        if (Len < 0)
            Len = 0;
        if (Pos > Arreglo.Length)
            return "";
        if (Pos < 0)
            Pos = 0;

        if (Len <= 0 || Pos + Len > Arreglo.Length)
            Len = Arreglo.Length - Pos;
        for (int Cont = 0; Cont < Len; Cont++)
        {
            Texto += HexByte2String(Convert.ToByte(Arreglo[Pos + Cont] / 16)) + HexByte2String(Convert.ToByte(Arreglo[Pos + Cont] % 16));

        }
        return Texto;
    }
    public string ObtenNS2CampoTELlave(string NS)
    {
        string _NS;
        byte[] Datos = BitConverter.GetBytes(Convert.ToUInt32(NS));
        byte[] _Datos = new byte[4];
        for (int i = 0; i < 4; i++)
            _Datos[i] = Datos[3 - i];
        _NS = "00000000" + Bcd2String(_Datos, 0, 4);
        string aux;
        aux = CeC_BD.EjecutaEscalarString("SELECT " + CeC_Campos.CampoTE_Llave + " FROM EC_PERSONAS_DATOS WHERE NS='" + _NS + "'");
        if (aux != "")
            return aux;
        _NS = "00000000" + Bcd2String(Datos, 0, 4);
        return CeC_BD.EjecutaEscalarString("SELECT " + CeC_Campos.CampoTE_Llave + " FROM EC_PERSONAS_DATOS WHERE NS='" + _NS + "'");

    }
    [WebMethod(Description = "Verifica si existe la persona a partir de la lectura de la credencial", MessageName = "ExistePersonaWindowsCE")]
    public string ExistePersona(int TerminalID, int LongitudNoEmpleado, int LongitudNoSerie, string LecturaCredencial, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return string.Empty;
        string _PersonaLinkID = "";
        string _ConsultaNoSerie = "";
        string _CampoLlave = CeC_Campos.CampoTE_Llave;

        string _CampoLlaveAdicional = CeC_BD.EjecutaEscalarString("SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + " AND TERMINAL_BORRADO=0");
        if (LongitudNoEmpleado <= 0)
        {
            return ObtenNS2CampoTELlave(LecturaCredencial);
        }
        else
        {
            if (LecturaCredencial.Length >= (LongitudNoEmpleado + LongitudNoSerie))
            {
                _PersonaLinkID = LecturaCredencial.Substring(0, LongitudNoEmpleado);
                if (_PersonaLinkID != CeC_BD.EjecutaEscalarString("SELECT " + _CampoLlave + " FROM EC_PERSONAS_DATOS WHERE " + _CampoLlave + " = " + _PersonaLinkID))
                {
                    CIsLog2.AgregaError("No existe tracve");
                    return string.Empty;
                }
                _ConsultaNoSerie = CeC_BD.EjecutaEscalarString("SELECT NS FROM EC_PERSONAS_DATOS WHERE " + _CampoLlave + " = " + _PersonaLinkID);
                if (_ConsultaNoSerie.Length >= 9 && _ConsultaNoSerie.Length <= 10)
                {
                    if (_ConsultaNoSerie != LecturaCredencial.Substring(LongitudNoEmpleado))
                    {
                        CIsLog2.AgregaError("tarjeta personalizada nuevo no coincide");
                        return string.Empty;
                    }
                }
                else
                {
                    if (_ConsultaNoSerie.Length > 0)
                    {
                        string _ValorCampoLlave = ObtenNS2CampoTELlave(_PersonaLinkID);
                        if (_ValorCampoLlave != _PersonaLinkID)
                        {
                            CIsLog2.AgregaError("tarjeta personalizada viejo no coincide");
                            return string.Empty;
                        }
                    }
                }
            }
            else if (LecturaCredencial.Length <= LongitudNoSerie)
            {
                _ConsultaNoSerie = CeC_BD.EjecutaEscalarString("SELECT " + _CampoLlaveAdicional + "  FROM EC_PERSONAS_DATOS WHERE " + _CampoLlaveAdicional + " = '" + LecturaCredencial + "'");
                if (_ConsultaNoSerie != string.Empty)
                    return CeC_BD.EjecutaEscalarString("SELECT " + _CampoLlave + " FROM EC_PERSONAS_DATOS WHERE " + _CampoLlaveAdicional + " = '" + LecturaCredencial + "'");
                else
                    return string.Empty;
            }
            return _PersonaLinkID;
        }
    }
    [WebMethod]
    public string ExistePersonaTracve(string NS)
    {
        CIsLog2.AgregaLog("ExistePersonaTracve " + NS);
        string _NS;
        int Persona_Link_ID = 0;
        try
        {
            if (NS.Length >= 14)
                Persona_Link_ID = Convert.ToInt32(NS.Substring(0, 4));
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }

        if (Persona_Link_ID <= 0)
        {
            int _posicion = NS.Length > 10 ? NS.Length - 10 : 0;
            int _long = NS.Length > 10 ? 10 : NS.Length;
            return ObtenNS2CampoTELlave(NS.Substring(_posicion, _long));
        }
        else
        {
            string NS_Entero = Convert.ToUInt32(NS.Substring(4)).ToString();
            if (Persona_Link_ID != Convert.ToInt32(CeC_BD.EjecutaEscalarString("SELECT TRACVE FROM EC_PERSONAS_DATOS WHERE TRACVE = " + Persona_Link_ID)))
            {
                CIsLog2.AgregaError("No existe tracve");
                return "";
            }
            string Ans = CeC_BD.EjecutaEscalarString("SELECT NS FROM EC_PERSONAS_DATOS WHERE TRACVE = " + Persona_Link_ID);
            if (Ans.Length >= 9 && Ans.Length <= 10)
            {
                if (Ans != NS_Entero)
                {
                    CIsLog2.AgregaError("tarjeta personalizada nuevo no coincide");
                    return "";
                }
            }
            else
            {
                if (Ans.Length > 0)
                {
                    string tracve = ObtenNS2CampoTELlave(NS_Entero);
                    if (Convert.ToInt32(tracve) != Persona_Link_ID)
                    {
                        CIsLog2.AgregaError("tarjeta personalizada viejo no coincide");
                        return "";
                    }
                }
                CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DATOS SET NS = '" + NS_Entero + "' WHERE TRACVE = " + Persona_Link_ID);
            }



            return Persona_Link_ID.ToString();
        }
    }
    [WebMethod(Description = "Obtiene la clase que apunta a la sesion actual en BD", EnableSession = true)]
    private CeC_SesionBD ObtenSesionBD()
    {
        return new CeC_SesionBD(ObtenSESION_ID());
    }

    /// <summary>
    /// Crea una Sesion a partir de un Usuario en la tabla de EC_SESIONES
    /// </summary>
    /// <param name="UsuarioID">Identificador del Usuario</param>
    /// <returns>Regresa un Identificador de Sesion si la operacion se realizo con exito de lo contrario regresa -9999</returns>
    [WebMethod(Description = "Crea una Sesion a partir de un usuario", EnableSession = true)]
    private int CrearSesion(int UsuarioID)
    {
        return CrearSesion(UsuarioID, 0);
    }

    /// <summary>
    /// Crea una Sesion a partir de un Usuario en la tabla de EC_SESIONES
    /// </summary>
    /// <param name="UsuarioID">Identificador del Usuario</param>
    /// <returns>Regresa un Identificador de Sesion si la operacion se realizo con exito de lo contrario regresa -9999</returns>
    [WebMethod(Description = "Crea una Sesion a partir de un usuario", EnableSession = true)]
    private int CrearSesion(int UsuarioID, int Terminal_ID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Nuevo(this, UsuarioID);

        if (Sesion != null)
            return Sesion.SESION_ID;
        return -9999;

        int SesionID = CeC_Autonumerico.GeneraAutonumerico("EC_SESIONES", "SESION_ID");
        string qry = "INSERT INTO EC_SESIONES (SESION_ID, USUARIO_ID, SESION_INICIO_FECHAHORA, SESION_TERMINAL_ID) VALUES( " + SesionID.ToString() + "," + UsuarioID.ToString() + "," + CeC_BD.SqlFechaHora(DateTime.Now) + ", " + Terminal_ID + " )";
        int ret = CeC_BD.EjecutaComando(qry);
        int Sesion_ID = 0;
        if (ret <= 0)
        {
            Sesion_ID = -9999;
        }
        else
        {
            Sesion_ID = SesionID;
        }
        try
        {
            Session["USUARIO_ID"] = UsuarioID;
            Session["SESION_ID"] = Sesion_ID;
        }
        catch { }
        return Sesion_ID;
    }

    [WebMethod(Description = "Cierra la sesión actual", EnableSession = true)]
    private bool CierraSesion()
    {
        bool Cerrado = CeC_Sesion.CierraSesion(ObtenSESION_ID());
        if (!Cerrado) return false;
        Session["SESION_ID"] = -1;
        return true;
    }

    /// <summary>
    /// Obtiene el Sesion_ID Actual en caso de haberse logeado
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene la sesion actual", EnableSession = true)]
    public int ObtenSESION_ID()
    {
        try { return Convert.ToInt32(Session["SESION_ID"]); }
        catch (Exception ex) { }
        return 0;
    }

    /// <summary>
    /// Obtiene el Suscripcion_ID Actual en caso de haberse logeado
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene la Suscripcion actual", EnableSession = true)]
    public int ObtenSUSCRIPCION_ID()
    {
        try { return Convert.ToInt32(Session["SUSCRIPCION_ID"]); }
        catch (Exception ex) { }
        return 0;
    }

    [WebMethod(Description = "Obtiene el usuarioID", EnableSession = true)]
    public int ObtenUsuario_ID()
    {
        try { return Convert.ToInt32(Session["USUARIO_ID"]); }
        catch (Exception ex) { }
        return 0;
    }

    /// <summary>
    /// Obtiene la lista de terminales asignadas a un sitio
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene la lista de terminales asignadas a un sitio", EnableSession = true)]
    public DS_WSPersonasTerminales.EC_TERMINALESDataTable ObtenTerminales(int Sitio_ID)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenTerminales >> No ha iniciado Sesion");
            return null;
        }
        DS_WSPersonasTerminalesTableAdapters.EC_TERMINALESTableAdapter TA = new DS_WSPersonasTerminalesTableAdapters.EC_TERMINALESTableAdapter();
        return TA.GetDataBySitio(Sitio_ID);
    }
    [WebMethod(Description = "Obtiene los datos de una termial", EnableSession = true)]
    public DS_WSPersonasTerminales.EC_TERMINALESDataTable ObtenTerminal(int TerminalID)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenTerminal >> No ha iniciado Sesion");
            return null;
        }
        DS_WSPersonasTerminalesTableAdapters.EC_TERMINALESTableAdapter TA = new DS_WSPersonasTerminalesTableAdapters.EC_TERMINALESTableAdapter();
        return TA.GetDataByTerminal(TerminalID);
    }
    /// <summary>
    /// Obtiene los datos de un sitio
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene los datos de un sitio", EnableSession = true)]
    public DS_WSPersonasTerminales.EC_SITIOSDataTable ObtenSitio(int Sitio_ID)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenSitio >> No ha iniciado Sesion");
            return null;
        }
        DS_WSPersonasTerminalesTableAdapters.EC_SITIOSTableAdapter TA = new DS_WSPersonasTerminalesTableAdapters.EC_SITIOSTableAdapter();
        return TA.GetDataSitio(Sitio_ID);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene el ID de el sitio predeterminado del usuarios (el primero)", EnableSession = true)]
    public int ObtenSitioID()
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenSitio >> No ha iniciado Sesion");
            return -2;
        }

        return CeC_Sitios.ObtenSitioID(ObtenUsuario_ID());

    }

    [WebMethod(Description = "Obtiene los datos de un sitio", EnableSession = true)]
    public DataSet ObtenSitiosDSMenu()
    {
        if (ObtenSESION_ID() <= 0 && ObtenUsuario_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenSitio >> No ha iniciado Sesion");
            return null;
        }
        return CeC_Sitios.ObtenSitiosDSMenu(ObtenUsuario_ID());

    }
    [WebMethod(Description = "Obtiene los datos de un sitio", EnableSession = true)]
    public int TerminalAgrega(string Nombre, int sitio_id, int tmodelo_id, int tipo_tecn_id, int tipo_tecn_add_id, string tcampollave, string tdireccion, int tminutos_dif)
    {
        if (ObtenSESION_ID() <= 0 && ObtenUsuario_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenSitio >> No ha iniciado Sesion");
            return -1;
        }
        return CeC_Sitios.TerminalesInserta(ObtenSESION_ID(), ObtenUsuario_ID(), Nombre, sitio_id, tmodelo_id, tipo_tecn_id, tipo_tecn_add_id, tcampollave, tdireccion, tminutos_dif);

    }
    [WebMethod(Description = "Obtiene los datos de un sitio", EnableSession = true)]
    public int TerminalBorra(int terminalID)
    {
        if (ObtenSESION_ID() <= 0 && ObtenUsuario_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenSitio >> No ha iniciado Sesion");
            return -1;
        }
        return CeC_Sitios.TerminalesDehabilita(terminalID);

    }
    [WebMethod(Description = "Obtiene los datos de un sitio", EnableSession = true)]
    public int SitioAgrega(string NombreSitio, int segundosSync)
    {

        if (ObtenSESION_ID() <= 0 && ObtenUsuario_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenSitio >> No ha iniciado Sesion");
            return -1;
        }

        return CeC_Sitios.Inserta(ObtenSESION_ID(), CeC_Usuarios.ObtenSuscripcionID(ObtenUsuario_ID()), NombreSitio, CeC_BD.FechaNula, CeC_BD.FechaNula, segundosSync);
    }

    [WebMethod(Description = "Obtiene los datos de un sitio", EnableSession = true)]
    public int TerminalActualiza(string Nombre, int sitio_id, int tmodelo_id, int tipo_tecn_id, string tcampollave, string tdireccion, int tminutos_dif, int terminalid)
    {
        if (ObtenSESION_ID() <= 0 && ObtenUsuario_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenSitio >> No ha iniciado Sesion");
            return -1;
        }
        return CeC_Sitios.TerminalesActualiza(ObtenSESION_ID(), CeC_Usuarios.ObtenSuscripcionID(ObtenUsuario_ID()), Nombre, sitio_id, tmodelo_id, tipo_tecn_id, tcampollave, tdireccion, tminutos_dif, terminalid);

    }
    [WebMethod(Description = "Obtiene los datos de un sitio", EnableSession = true)]
    public bool SitioActualiza(string NombreSitio, int segundosSync, int SitioID)
    {
        if (ObtenSESION_ID() <= 0 && ObtenUsuario_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenSitio >> No ha iniciado Sesion");
            return false;
        }
        return CeC_Sitios.SitiosActualiza(ObtenSESION_ID(), CeC_Usuarios.ObtenSuscripcionID(ObtenUsuario_ID()), SitioID, NombreSitio, segundosSync);
    }
    [WebMethod(Description = "Obtiene los datos de un sitio", EnableSession = true)]
    public DataSet ObtenTerminalesDSMenu()
    {
        if (ObtenSESION_ID() <= 0 && ObtenUsuario_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenSitio >> No ha iniciado Sesion");
            return null;
        }

        return CeC_Sitios.ObtenTerminalesDSMenu(ObtenUsuario_ID());

    }

    [WebMethod(Description = "Obtiene los datos de un sitio", EnableSession = true)]
    public DataSet ObtenTerminalesDSMenuporSitio(int SitioID)
    {
        if (ObtenSESION_ID() <= 0 && ObtenUsuario_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenSitio >> No ha iniciado Sesion");
            return null;
        }

        return CeC_Sitios.ObtenTerminalesDSMenuporSitio(SitioID, ObtenUsuario_ID());

    }

    /// <summary>
    /// Obtiene un parámetro de la terminal
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene un parámetro de la terminal", EnableSession = true)]
    public bool RecibeVectores(int Terminal_ID, DS_WSPersonasTerminales.DT_VectoresDataTable Datos)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("RecibeVectores >> No ha iniciado Sesion");
            return false;
        }
        if (Datos == null)
            return false;
        if (Datos.Rows.Count <= 0)
            return false;
        CeC_Terminales_Param Param = new CeC_Terminales_Param(Terminal_ID);
        int ALMACEN_VEC_ID = Param.ALMACEN_VEC_ID;

        foreach (DS_WSPersonasTerminales.DT_VectoresRow DatosVector in Datos)
        {
            int Persona_ID = CeC_Personas.ObtenPersonaID(Terminal_ID, Param.TERMINAL_CAMPO_LLAVE, DatosVector.PERSONAS_A_VEC_T1);
            if (Persona_ID > 0)
            {
                if (!DatosVector.IsPERSONAS_A_VEC_1Null())
                    AsignaVectorAVec(Persona_ID, ALMACEN_VEC_ID, DatosVector.PERSONAS_A_VEC_1, 1);
                if (!DatosVector.IsPERSONAS_A_VEC_2Null())
                    AsignaVectorAVec(Persona_ID, ALMACEN_VEC_ID, DatosVector.PERSONAS_A_VEC_2, 2);
                if (!DatosVector.IsPERSONAS_A_VEC_3Null())
                    AsignaVectorAVec(Persona_ID, ALMACEN_VEC_ID, DatosVector.PERSONAS_A_VEC_3, 3);
            }
            else
            {
                if (!DatosVector.IsPERSONAS_A_VEC_1Null())
                    AgregaTerminalesDExtras(Terminal_ID, Tipo_DEXTRAS.Huellas, DatosVector.PERSONAS_A_VEC_T1, "PERSONAS_A_VEC_1", DatosVector.PERSONAS_A_VEC_1);
                if (!DatosVector.IsPERSONAS_A_VEC_2Null())
                    AgregaTerminalesDExtras(Terminal_ID, Tipo_DEXTRAS.Huellas, DatosVector.PERSONAS_A_VEC_T1, "PERSONAS_A_VEC_2", DatosVector.PERSONAS_A_VEC_2);
                if (!DatosVector.IsPERSONAS_A_VEC_3Null())
                    AgregaTerminalesDExtras(Terminal_ID, Tipo_DEXTRAS.Huellas, DatosVector.PERSONAS_A_VEC_T1, "PERSONAS_A_VEC_3", DatosVector.PERSONAS_A_VEC_3);

            }

        }
        return true;
    }

    /// <summary>
    /// Obtiene un parámetro de la terminal
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene un parámetro de la terminal", EnableSession = true)]
    public string ObtenParametro(int Terminal_ID, string Parametro, string ValorPredeterminado)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenParametro >> No ha iniciado Sesion");
            return "";
        }
        try
        {
            CeC_Terminales_Param Param = new CeC_Terminales_Param(Terminal_ID);
            switch (Parametro)
            {
                case "TERMINAL_ESENTRADA":
                    return Param.TERMINAL_ESSALIDA.ToString();
                    break;
                case "TERMINAL_ESSALIDA":
                    return Param.TERMINAL_ESSALIDA.ToString();
                    break;
                case "TERMINAL_ACEPTA_TA":
                    return Param.TERMINAL_ACEPTA_TA.ToString();
                    break;
                case "SITIO_HIJO_ID":
                    return Param.SITIO_HIJO_ID.ToString();
                    break;

                default:
                    return CeC_BD.ObtenParamTerminal(Terminal_ID, Parametro, ValorPredeterminado);
            }
        }
        catch { }

        return ValorPredeterminado;
    }

    /// <summary>
    /// Asigna un parámetro a la terminal
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Asigna un parámetro a la terminal", EnableSession = true)]
    public bool AsignaParametro(int Terminal_ID, string Parametro, string Valor)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("Asigna Parametro >> No ha iniciado Sesion");
            return false;
        }
        return CeC_BD.GuardaParamTerminal(Terminal_ID, Parametro, Valor);
    }


    /// <summary>
    /// Obtiene los datos de un sitio
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene los datos de un sitio", EnableSession = true)]
    public DateTime ObtenFechaHora()
    {
        DateTime fs = DateTime.Now;
        // return DateTime.Now;
        return fs;
    }
    [WebMethod(Description = "Obtiene la zona horaria", EnableSession = true)]
    public long ObtenZonaHorario()
    {
        return TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Ticks;
    }

    [WebMethod(Description = "Obtiene el valor del campo llave de una persona", EnableSession = true)]
    public int ExistePersonaIDCampoLlave(int TerminalID, string ValorCampoLlave, out string ValorCampoAdicional)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("existePersonaIDCampoLlave >> No ha iniciado Sesion");

            ValorCampoAdicional = "";
            return -9999;
        }
        int PersonaID = CeC_BD.PersonaID(TerminalID, ValorCampoLlave, out ValorCampoAdicional);
        if (PersonaID != -9999)
        {
            if (ExistePersonaIDTerminalID(PersonaID, TerminalID))
                return PersonaID;
            else
                return -9999;
        }
        else
            return PersonaID;
    }
    [WebMethod]
    public bool ExisteSupervisor(int NoSupervisor, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return false;
        int _Usuario1ID = -9999;
        int _Usuario2ID = -9999;
        int _Usuario3ID = -9999;
        int _Grupo1ID = CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + NoSupervisor.ToString());
        int _Grupo2ID = CeC_BD.EjecutaEscalarInt("SELECT GRUPO_2_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + NoSupervisor.ToString());
        int _Grupo3ID = CeC_BD.EjecutaEscalarInt("SELECT GRUPO_3_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + NoSupervisor.ToString());
        if (_Grupo1ID != -9999)
            _Usuario1ID = CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_PERMISOS_SUSCRIP WHERE SUSCRIPCION_ID = " + _Grupo1ID.ToString());
        if (_Grupo2ID != -9999)
            _Usuario2ID = CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_PERMISOS_SUSCRIP WHERE SUSCRIPCION_ID = " + _Grupo2ID.ToString());
        if (_Grupo3ID != -9999)
            _Usuario3ID = CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_PERMISOS_SUSCRIP WHERE SUSCRIPCION_ID = " + _Grupo3ID.ToString());
        if (_Usuario1ID != -9999)
            return true;
        if (_Usuario2ID != -9999)
            return true;
        if (_Usuario3ID != -9999)
            return true;
        return false;
    }
    [WebMethod(EnableSession = true)]
    public int ExistePersonaIDCampoAdicional(int TerminalID, string ValorCampoAdicional)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("Asigna ExistePersonaIDCampoAdicional >> No ha iniciado Sesion");
            return -9999;
        }
        int PersonaID = CeC_BD.PersonaID(TerminalID, ValorCampoAdicional);
        if (PersonaID != -9999)
        {
            if (ExistePersonaIDTerminalID(PersonaID, TerminalID))
                return PersonaID;
            else
                return -9999;
        }
        else
            return PersonaID;
    }
    [WebMethod(Description = "Obtiene un valor del campo adicional de una terminal", MessageName = "ObtenValorCampoAdicionalWindows", EnableSession = true)]
    public string ObtenValorCampoAdicional(int TerminalID, int PersonaID)
    {
        if (ObtenSESION_ID() <= 0)
            return string.Empty; ;
        string qry;
        string CampoLlaveAdicional = CeC_BD.EjecutaEscalarString("SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + " AND TERMINAL_BORRADO=0");
        if (CampoLlaveAdicional != "")
        {
            qry = "SELECT " + CampoLlaveAdicional + " FROM EC_PERSONAS_DATOS WHERE " + CeC_Campos.CampoTE_Llave + " = " + CeC_BD.ValorCampoLlave(PersonaID).ToString();
            return CeC_BD.EjecutaEscalarString(qry);
        }
        else
            return string.Empty; ;
    }
    [WebMethod(Description = "Obtiene un valor del campo adicional de una terminal", MessageName = "ObtenValorCampoAdicionalWindowsCE")]
    public string ObtenValorCampoAdicional(int TerminalID, int PersonaID, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return string.Empty; ;
        string qry;
        string CampoLlaveAdicional = CeC_BD.EjecutaEscalarString("SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + " AND TERMINAL_BORRADO=0");
        if (CampoLlaveAdicional != "")
        {
            qry = "SELECT " + CampoLlaveAdicional + " FROM EC_PERSONAS_DATOS WHERE " + CeC_Campos.CampoTE_Llave + " = " + CeC_BD.ValorCampoLlave(PersonaID).ToString();
            return CeC_BD.EjecutaEscalarString(qry);
        }
        else
            return string.Empty; ;
    }
    [WebMethod(Description = "Asigna un valor al campo adicional de una terminal", MessageName = "AsignaValorCampoAdicionalWindows", EnableSession = true)]
    public bool AsignaCampoAdicional(int TerminalID, int PersonaID, string ValorCampoAdicional)
    {
        if (ObtenSESION_ID() <= 0)
            return false;
        string CampoLlaveAdicional = CeC_BD.EjecutaEscalarString("SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + " AND TERMINAL_BORRADO=0");
        string qry = "UPDATE EC_PERSONAS_DATOS SET " + CampoLlaveAdicional + " = '" + ValorCampoAdicional + "' WHERE PERSONA_ID = " + PersonaID;
        int ret = CeC_BD.EjecutaComando(qry);
        if (ret > 0)
        {
            CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "CampoAdicional", PersonaID, CampoLlaveAdicional + " = " + ValorCampoAdicional, ObtenSESION_ID());
            return true;
        }
        else
            return false;
    }
    [WebMethod(Description = "Asigna un valor al campo adicional de una terminal", MessageName = "AsignaValorCampoAdicionalWindowsCE")]
    public bool AsignaCampoAdicional(int TerminalID, int PersonaID, string ValorCampoAdicional, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return false;
        string CampoLlaveAdicional = CeC_BD.EjecutaEscalarString("SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + " AND TERMINAL_BORRADO=0");
        string qry = "UPDATE EC_PERSONAS_DATOS SET " + CampoLlaveAdicional + " = '" + ValorCampoAdicional + "' WHERE " + CeC_Campos.CampoTE_Llave + " = " + CeC_BD.ValorCampoLlave(PersonaID).ToString();
        int ret = CeC_BD.EjecutaComando(qry);
        if (ret > 0)
            return true;
        else
            return false;
    }
    [WebMethod(Description = "Obtiene el valor del campo llave", MessageName = "ValorCampoLlaveWindows", EnableSession = true)]
    public string ValorCampoLlave(int PersonaID)
    {
        if (ObtenSESION_ID() <= 0)
            return string.Empty;
        return CeC_BD.ValorCampoLlave(PersonaID);
    }
    [WebMethod(Description = "Obtiene el valor del campo llave", MessageName = "ValorCampoLlaveWindowsCE")]
    public string ValorCampoLlave(int PersonaID, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return string.Empty;
        return CeC_BD.ValorCampoLlave(PersonaID);
    }
    /// <summary>
    /// Obtiene la foto de una persona a partir de un identificador
    /// </summary>
    /// <param name="PersonaID">Identificador de la persona </param>
    /// <returns>Arreglo de bytes que representa la foto de la persona</returns>
    [WebMethod(Description = "Obtiene la foto de una persona a partir de un identificador", MessageName = "ObtenFotoWindows", EnableSession = true)]
    public byte[] ObtenFoto(int PersonaID)
    {
        if (ObtenSESION_ID() <= 0)
            return null;
        byte[] Foto = CeC_BD.ObtenBinario("EC_PERSONAS_IMA", "PERSONA_ID", PersonaID, "PERSONA_IMA");
        if (Foto != null)
        {
            return Foto;
        }
        else
        {
            return null;
        }
    }
    [WebMethod(Description = "Obtiene la foto de una persona a partir de un identificador", MessageName = "ObtenFotoWindowsCE")]
    public byte[] ObtenFoto(int PersonaID, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return null;
        byte[] Foto = CeC_BD.ObtenBinario("EC_PERSONAS_IMA", "PERSONA_ID", PersonaID, "PERSONA_IMA");
        if (Foto != null)
        {
            return Foto;
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// Obtiene el nombre de una persona
    /// </summary>
    /// <param name="PersonaID">Identificador de la persona</param>
    /// <returns>Nombre de la persona</returns>
    [WebMethod(Description = "Obtiene el nombre de la persona", MessageName = "PersonaNombreWindows", EnableSession = true)]
    public string PersonaNombre(int PersonaID)
    {
        if (ObtenSESION_ID() <= 0)
            return "";
        string qry = "SELECT PERSONA_NOMBRE FROM EC_PERSONAS WHERE PERSONA_ID = " + PersonaID.ToString() + " AND PERSONA_BORRADO=0";
        return CeC_BD.EjecutaEscalarString(qry);
    }
    [WebMethod(Description = "Obtiene el nombre de la persona", MessageName = "PersonaNombreWindowsCE")]
    public string PersonaNombre(int PersonaID, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return "";
        string qry = "SELECT PERSONA_NOMBRE FROM EC_PERSONAS WHERE PERSONA_ID = " + PersonaID.ToString() + " AND PERSONA_BORRADO=0";
        return CeC_BD.EjecutaEscalarString(qry);
    }
    /// <summary>
    /// Registra los accesos a partir de un DataSet que contiene los acceso y las persona
    /// </summary>
    /// <param name="DataSetAccesos">DataSet de accesos</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public DS_WSAccesos RegistrarChecadas(DS_WSAccesos DataSetAccesos)
    {
        if (ObtenSESION_ID() <= 0)
            return null;
        if (DataSetAccesos != null)
        {
            if (DataSetAccesos.DT_Accesos.Rows.Count != 0)
            {
                CeC_BD.RegistrarAccesos(ref DataSetAccesos);
            }
        }
        return DataSetAccesos;
    }
    /// <summary>
    /// Registra un acceso de alguna persona
    /// </summary>
    /// <param name="PersonaID">Identificador de la persona</param>
    /// <param name="TerminalID">Identificador de la terminal</param>
    /// <param name="TipoAccesoID">Identificador del tipo de Acceso 1=Correcto, 2=Entrada, 3=Salida</param>
    /// <param name="FechaHora">Fecha y hora en la que se registro el acceso</param>
    /// <returns></returns>
    [WebMethod(Description = "Registra una chacada de laguna persona", MessageName = "RegistrarChecadaWindows", EnableSession = true)]
    public bool RegistrarChecada(int PersonaID, int TerminalID, int TipoAccesoID, DateTime FechaHora)
    {
        if (ObtenSESION_ID() <= 0)
            return false;
        return CeC_Accesos.AgregarAcceso(PersonaID, TerminalID, TipoAccesoID, FechaHora, ObtenSESION_ID(), ObtenSUSCRIPCION_ID()) > 0 ? true : false;
    }
    /// <summary>
    /// Registra un acceso de alguna persona con la fecha del servidor
    /// </summary>
    /// <param name="PersonaID">dentificador de la persona</param>
    /// <param name="TerminalID">Identificador de la terminal</param>
    /// <param name="TipoAccesoID">Identificador del tipo de Acceso 1=Correcto, 2=Entrada, 3=Salida</param>
    /// <returns></returns>
    [WebMethod(Description = "Registra una chacada de laguna persona", MessageName = "RegistrarChecadaSinFechaWindows", EnableSession = true)]
    public bool RegistrarChecada(int PersonaID, int TerminalID, int TipoAccesoID)
    {
        if (ObtenSESION_ID() <= 0)
            return false;
        return CeC_Accesos.AgregarAcceso(PersonaID, TerminalID, TipoAccesoID, ObtenFechaHora(), ObtenSESION_ID(), ObtenSUSCRIPCION_ID()) > 0 ? true : false;
    }
    [WebMethod(Description = "Registra una chacada de la persona", MessageName = "RegistrarChecadaSinFechaWindowsCE")]
    public bool RegistrarChecada(int PersonaID, int TerminalID, int TipoAccesoID, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return false;
        return CeC_Accesos.AgregarAcceso(PersonaID, TerminalID, TipoAccesoID, ObtenFechaHora(), ObtenSESION_ID(), ObtenSUSCRIPCION_ID()) > 0 ? true : false;
    }
    /// <summary>
    /// Obtiene la huella de alguna persona
    /// </summary>
    /// <param name="PersonaID">Identificaor de la persona</param>
    /// <param name="TerminalID">Identificaor de la terminal</param>
    /// <returns></returns>
    [WebMethod(Description = "obtiene la huella de la persona", EnableSession = true)]
    public byte[] ObtenHuella(int PersonaID, int TerminalID)
    {
        return ObtenHuella(PersonaID, TerminalID, 1);
    }
    /// <summary>
    /// Obtiene la huella de alguna persona
    /// </summary>
    /// <param name="PersonaID">Identificaor de la persona</param>
    /// <param name="TerminalID">Identificaor de la terminal</param>
    /// <param name="NoHuella">Numero de huella empezando desde 1</param>
    /// <returns></returns>
    [WebMethod(Description = "obtiene la huella de la persona", MessageName = "ObtenHuellaEx", EnableSession = true)]
    public byte[] ObtenHuella(int PersonaID, int TerminalID, int NoHuella)
    {
        if (NoHuella < 1 || NoHuella > 3)
            return null;
        if (ObtenSESION_ID() <= 0)
            return null;
        string qry = "SELECT ALMACEN_VEC_ID FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString();
        int AlmacenID = CeC_BD.EjecutaEscalarInt(qry);
        if (AlmacenID != -9999)
        {
            byte[] Huella = CeC_BD.ObtenBinario("EC_PERSONAS_A_VEC", "PERSONA_ID", PersonaID, "ALMACEN_VEC_ID", AlmacenID, "PERSONAS_A_VEC_" + NoHuella);
            if (Huella != null)
            {
                return Huella;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PersonaID"></param>
    /// <param name="TerminalID"></param>
    /// <param name="NoHuella"></param>
    /// <returns></returns>
    [WebMethod(Description = "obtiene la huella de la persona", EnableSession = true)]
    public bool HayHuella(int PersonaID, int TerminalID, int NoHuella)
    {
        byte[] Huella = ObtenHuella(PersonaID, TerminalID, NoHuella);
        try
        {
            if (Huella != null && Huella.Length > 1)
                return true;
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Asigna un vector a un almacen de vectores
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Asigna un vector a un almacen de vectores", EnableSession = true)]
    public bool AsignaVectorAVec(int PersonaID, int AlmacenID, byte[] Huella, int NoHuella)
    {
        if (ObtenSESION_ID() <= 0)
            return false;

        if (NoHuella < 1)
            return false;
        if (NoHuella > 3)
            return false;
        if (AlmacenID <= 0)
            return false;
        string Hash = "";
        int PID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS_A_VEC WHERE ALMACEN_VEC_ID = " + AlmacenID + " AND PERSONA_ID = " + PersonaID);
        if (PID <= 0)
        {
            string qry = "INSERT INTO EC_PERSONAS_A_VEC(PERSONA_ID,ALMACEN_VEC_ID) VALUES (" + PersonaID.ToString() + "," + AlmacenID.ToString() + ")";
            int ret = CeC_BD.EjecutaComando(qry);
        }
        else
        {
            string HashAnt = CeC_BD.EjecutaEscalarString("SELECT PERSONAS_A_VEC_" + NoHuella + "_HASH FROM EC_PERSONAS_A_VEC WHERE ALMACEN_VEC_ID = " + AlmacenID + " AND PERSONA_ID = " + PersonaID);
            Hash = CeC_BD.CalculaHash(CeC.Bcd2String(Huella));
            if (HashAnt == Hash)
                return true;
        }


        if (CeC_BD.AsignaBinario("EC_PERSONAS_A_VEC", "PERSONA_ID", PersonaID, "ALMACEN_VEC_ID", AlmacenID, "PERSONAS_A_VEC_" + NoHuella, Huella))
        {
            CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_A_VEC SET PERSONAS_A_VEC_" + NoHuella + "_UMOD = " + CeC_BD.SqlFechaHora(DateTime.Now)
                + ", PERSONAS_A_VEC_" + NoHuella + "_HASH = '" + Hash + "' "
                + " WHERE ALMACEN_VEC_ID = " + AlmacenID + " AND PERSONA_ID = " + PersonaID);
            return true;
        }
        else
            return false;
        /*
        int PN = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS_A_VEC WHERE PERSONA_ID = " + PersonaID + " AND ALMACEN_VEC_ID = " + AlmacenID + "");
        if (PN <= 0)
            CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_A_VEC(PERSONA_ID,ALMACEN_VEC_ID) VALUES (" + PersonaID.ToString() + "," + AlmacenID.ToString() + ")");

        if (CeC_BD.AsignaBinario("EC_PERSONAS_A_VEC", "PERSONA_ID", PersonaID, "ALMACEN_VEC_ID", AlmacenID, "PERSONAS_A_VEC_" + NoHuella.ToString(), Huella))
            return true;
        else
            return false;
        */
        return false;
    }
    /// <summary>
    /// Almacena la huella de alguna persona
    /// </summary>
    /// <param name="PersonaID">Identificador de la persona</param>
    /// <param name="TerminalID">Identificador de la terminal</param>
    /// <param name="Huella">Arreglo de bytes que contiene la foto</param>
    /// <returns>Regresa verdadero si se almaceno correctamente de lo contrario devolvera falso</returns>
    [WebMethod(Description = "Guardar los vectores de huella en un almacen", MessageName = "AsignaHuellaEx", EnableSession = true)]
    public bool AsignaHuella(int PersonaID, int TerminalID, byte[] Huella)
    {
        return AsignaHuella(PersonaID, TerminalID, Huella, 1);
    }

    /// <summary>
    /// Almacena la huella de alguna persona
    /// </summary>
    /// <param name="PersonaID">Identificador de la persona</param>
    /// <param name="TerminalID">Identificador de la terminal</param>
    /// <param name="Huella">Arreglo de bytes que contiene la foto</param>
    /// <param name="NoHuella">Numero de huella empezando desde 1</param>
    /// <returns>Regresa verdadero si se almaceno correctamente de lo contrario devolvera falso</returns>
    [WebMethod(Description = "Guardar los vectores de huella en un almacen", EnableSession = true)]
    public bool AsignaHuella(int PersonaID, int TerminalID, byte[] Huella, int NoHuella)
    {
        if (NoHuella < 1 || NoHuella > 3)
            return false;
        if (ObtenSESION_ID() <= 0)
            return false;

        string qry = "SELECT ALMACEN_VEC_ID FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString();
        int AlmacenID = CeC_BD.EjecutaEscalarInt(qry);

        if (AlmacenID != -9999)
        {
            return AsignaVectorAVec(PersonaID, AlmacenID, Huella, NoHuella);
        }
        else
        {
            return false;
        }
    }

    [WebMethod(Description = "Guardar los vectores de huella en un almacen", EnableSession = true)]
    public bool AsignaHuellaCampoLlave(string ValorCampoLlave, int TerminalID, byte[] Huella, int NoHuella)
    {
        if (ObtenSESION_ID() <= 0)
            return false;

        int PersonaID = 0;
        PersonaID = CeC_Terminales.ObtenPersonaID(TerminalID, ValorCampoLlave);
        if (PersonaID < 0)
            return false;
        return AsignaHuella(PersonaID, TerminalID, Huella, NoHuella);

    }

    /// <summary>
    /// Obtiene la etiqueta del campo llave de alguna terminal
    /// </summary>
    /// <param name="TerminalID">Identificador de la terminal</param>
    /// <returns>Devuelve la etiqueta del campo llave de la terminal</returns>
    [WebMethod(Description = "Devuelve la etiqueta del campo Llave", EnableSession = true)]
    public string EtiquetaLlave(int TerminalID)
    {
        if (ObtenSESION_ID() <= 0)
            return "";
        return CeC_BD.EtiquetaLlave(TerminalID);
    }
    /// <summary>
    /// Obtiene la etiqueta del campo llave adicional
    /// </summary>
    /// <param name="TerminalID">Identificador de la terminal</param>
    /// <returns></returns>
    [WebMethod(Description = "Devuelve la etiqueta del campo Llave Adicional", EnableSession = true)]
    public string EtiquetaLlaveAdicional(int TerminalID)
    {
        if (ObtenSESION_ID() <= 0)
            return "";
        return CeC_BD.EtiquetaLlaveAdicional(TerminalID);
    }
    [WebMethod(Description = "Calcula Hash", EnableSession = true)]
    public string CalculaHash(string Texto)
    {
        if (ObtenSESION_ID() <= 0)
            return "";
        System.Security.Cryptography.SHA1CryptoServiceProvider Sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        string HashSR = BitConverter.ToString(Sha1.ComputeHash(new System.IO.MemoryStream(System.Text.ASCIIEncoding.Default.GetBytes(Texto))));
        return HashSR;
    }

    /// <summary>
    /// Indica que se ha procesado devidamente la lista de empleados y se debe confirmar.
    /// </summary>
    /// <param name="TerminalID"></param>
    /// <returns></returns>
    [WebMethod(Description = "Indica que se ha procesado devidamente la lista de empleados y se debe confirmar.", EnableSession = true)]
    public bool ConfirmaListaEmpleados(int TerminalID)
    {

        if (ObtenSESION_ID() <= 0)
            return false;
        try
        {
            DS_WSPersonasTerminales DS = ObtenSesionBD().DS_EmpleadosTerminal;
            if (DS == null)
                return false;

            foreach (DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in DS.DT_PersonasTerminales)
            {
                CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_TERMINALES SET PERSONA_TERMINAL_CAMPO_L = '" + Fila.PERSONAS_A_VEC_T1
                    + "', PERSONA_TERMINAL_CAMPO_A = '"
                    + Fila.PERSONAS_A_VEC_T2 + "' WHERE  PERSONA_ID =" + Fila.PERSONA_ID
                    + " AND TERMINAL_ID = " + TerminalID);
            }
            CeC_Terminales_Param Param = new CeC_Terminales_Param(TerminalID);
            Param.ListaEmpleadosHash = CalculaHash(DS.GetXml());
        }
        catch
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// Obtiene una lista de empleados con huellas a cargar en una terminal
    /// Validando si existieron cambios desde su ultima descarga, si No hay cambios
    /// regresará null
    /// </summary>
    /// <param name="TerminalID">Identificador de terminal</param>
    /// <returns>Nulo si no hay cambios</returns>
    [WebMethod(EnableSession = true)]
    public DS_WSPersonasTerminales ListaEmpleadosCambios(int TerminalID)
    {
        if (ObtenSESION_ID() <= 0)
            return null;
        DS_WSPersonasTerminales DS = ListaEmpleados(TerminalID);
        if (DS == null)
            return null;
        string HashSR = CalculaHash(DS.GetXml());
        CeC_Terminales_Param Param = new CeC_Terminales_Param(TerminalID);
        //Falta validar para que no este sincronizando las listas cuando es controladora todo el tiempo
        if (Param.SITIO_HIJO_ID > 0 || Param.ListaEmpleadosHash != HashSR)
        {
            ObtenSesionBD().DS_EmpleadosTerminal = DS;
            return DS;
        }
        return null;
    }
    /// <summary>
    /// Obtiene un DataSet con la lista de empleado que pueden tener acceso a la terminal
    /// </summary>
    /// <param name="TerminalID">Identificador de la terminal</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public DS_WSPersonasTerminales ListaEmpleados(int TerminalID)
    {
        if (ObtenSESION_ID() <= 0)
            return null;
        string CampoLlave = string.Empty;
        string CampoLlaveAdicional = string.Empty;
        DS_WSPersonasTerminales DSChecador;
        try
        {
            CeC_Terminales_Param Terminal = new CeC_Terminales_Param(TerminalID);
            CampoLlave = Terminal.TERMINAL_CAMPO_LLAVE;
            CampoLlaveAdicional = Terminal.TERMINAL_CAMPO_ADICIONAL;
            if (CampoLlaveAdicional.Length == 0)
                CampoLlaveAdicional = "' '";
            else
                if (CampoLlaveAdicional.IndexOf('.') < 0)
                    CampoLlaveAdicional = CeC.AgregaSeparador("EC_PERSONAS_DATOS", CampoLlaveAdicional, ".");

            if (CampoLlave.IndexOf('.') < 0)
                CampoLlave = CeC.AgregaSeparador("EC_PERSONAS_DATOS", CampoLlave, ".");

            DSChecador = new DS_WSPersonasTerminales();
            if (CeC_BD.EsOracle)
            {
                DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("" +
                            "SELECT EC_PERSONAS.PERSONA_ID, " + CampoLlave + " as PERSONAS_A_VEC_T1, " + CampoLlaveAdicional + " as PERSONAS_A_VEC_T2, ' ' as PERSONAS_A_VEC_T3, " +
                            "PERSONA_NOMBRE, EC_PERSONAS_DATOS.PERSONA_LINK_ID, EC_PERSONAS_S_HUELLA.PERSONA_ID AS PERSONA_ID_S_HUELLA, " +
                            " EC_PERSONAS_A_VEC.PERSONAS_A_VEC_1, EC_PERSONAS_A_VEC.PERSONAS_A_VEC_2, " +
                            " EC_PERSONAS_A_VEC.PERSONAS_A_VEC_3, PERSONA_S_HUELLA_CLAVE " +
                            " FROM            EC_PERSONAS, EC_PERSONAS_DATOS, EC_PERSONAS_TERMINALES, EC_PERSONAS_A_VEC, EC_PERSONAS_S_HUELLA " +
                            " WHERE        EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID AND EC_PERSONAS.PERSONA_ID = EC_PERSONAS_TERMINALES.PERSONA_ID AND  " +
                            " EC_PERSONAS_TERMINALES.PERSONA_ID = EC_PERSONAS_A_VEC.PERSONA_ID (+) AND  " +
                            " EC_PERSONAS.PERSONA_ID = EC_PERSONAS_S_HUELLA.PERSONA_ID (+) AND (EC_PERSONAS_TERMINALES.TERMINAL_ID = " + Terminal.TERMINAL_ID + ") AND " +
                            " (EC_PERSONAS_A_VEC.ALMACEN_VEC_ID (+) = " + Terminal.ALMACEN_VEC_ID + ") AND EC_PERSONAS.PERSONA_BORRADO = 0  AND EC_PERSONAS_TERMINALES.PERSONA_TERMINAL_BORRADO=0 ORDER BY PERSONA_LINK_ID");
                foreach (DataRow DR in DS.Tables[0].Rows)
                {

                    try
                    {
                        DS_WSPersonasTerminales.DT_PersonasTerminalesRow FilaNueva = DSChecador.DT_PersonasTerminales.NewDT_PersonasTerminalesRow();
                        FilaNueva.PERSONA_ID = Convert.ToInt32(DR["PERSONA_ID"]);


                        FilaNueva.PERSONA_LINK_ID = Convert.ToInt32(DR["PERSONA_LINK_ID"]);
                        FilaNueva.PERSONA_NOMBRE = Convert.ToString(DR["PERSONA_NOMBRE"]);
                        try { FilaNueva.PERSONAS_A_VEC_T1 = Convert.ToString(DR["PERSONAS_A_VEC_T1"]); }
                        catch { }
                        try { FilaNueva.PERSONAS_A_VEC_T2 = Convert.ToString(DR["PERSONAS_A_VEC_T2"]); }
                        catch { }
                        try { FilaNueva.PERSONAS_A_VEC_T3 = Convert.ToString(DR["PERSONAS_A_VEC_T3"]); }
                        catch { }
                        //                            if (FilaNueva.IsPERSONA_ID_S_HUELLANull() || FilaNueva.PERSONA_ID_S_HUELLA == 0)
                        {
                            try { FilaNueva.PERSONAS_A_VEC_1 = (byte[])DR["PERSONAS_A_VEC_1"]; }
                            catch { }
                            try { FilaNueva.PERSONAS_A_VEC_2 = (byte[])DR["PERSONAS_A_VEC_2"]; }
                            catch { }
                            try { FilaNueva.PERSONAS_A_VEC_3 = (byte[])DR["PERSONAS_A_VEC_3"]; }
                            catch { }
                            FilaNueva.PERSONA_ID_S_HUELLA = 0;

                        }
                        try { FilaNueva.PERSONA_ID_S_HUELLA = Convert.ToInt32(DR["PERSONA_ID_S_HUELLA"]); }
                        catch { }
                        try { FilaNueva.PERSONA_S_HUELLA_CLAVE = Convert.ToString(DR["PERSONA_S_HUELLA_CLAVE"]); }
                        catch { }
                        DSChecador.DT_PersonasTerminales.AddDT_PersonasTerminalesRow(FilaNueva);
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError(ex);

                    }
                }
                return DSChecador;

            }
            else
            {
                DSChecador = new DS_WSPersonasTerminales();
                /*
                DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("" +
                "SELECT EC_PERSONAS.PERSONA_ID, " + CampoLlave + " as PERSONAS_A_VEC_T1, " +
                "" + CampoLlaveAdicional + " as PERSONAS_A_VEC_T2,' ' as PERSONAS_A_VEC_T3,EC_PERSONAS.PERSONA_NOMBRE, " +
                "EC_PERSONAS.PERSONA_LINK_ID, " +
                "(SELECT PERSONA_ID FROM EC_PERSONAS_S_HUELLA WHERE EC_PERSONAS.PERSONA_ID=EC_PERSONAS_S_HUELLA.PERSONA_ID) as PERSONA_ID_S_HUELLA, " +
                "(SELECT PERSONAS_A_VEC_1 FROM EC_PERSONAS_A_VEC WHERE EC_PERSONAS_A_VEC.ALMACEN_VEC_ID = EC_TERMINALES.ALMACEN_VEC_ID AND EC_PERSONAS.PERSONA_ID=EC_PERSONAS_A_VEC.PERSONA_ID) AS PERSONAS_A_VEC_1, " +
                "(SELECT PERSONAS_A_VEC_2 FROM EC_PERSONAS_A_VEC WHERE EC_PERSONAS_A_VEC.ALMACEN_VEC_ID = EC_TERMINALES.ALMACEN_VEC_ID AND EC_PERSONAS.PERSONA_ID=EC_PERSONAS_A_VEC.PERSONA_ID) AS PERSONAS_A_VEC_2, " +
                "(SELECT PERSONAS_A_VEC_3 FROM EC_PERSONAS_A_VEC WHERE EC_PERSONAS_A_VEC.ALMACEN_VEC_ID = EC_TERMINALES.ALMACEN_VEC_ID AND EC_PERSONAS.PERSONA_ID=EC_PERSONAS_A_VEC.PERSONA_ID) AS PERSONAS_A_VEC_3, " +
                "(SELECT PERSONA_S_HUELLA_CLAVE FROM EC_PERSONAS_S_HUELLA WHERE EC_PERSONAS.PERSONA_ID=EC_PERSONAS_S_HUELLA.PERSONA_ID) as PERSONA_S_HUELLA_CLAVE " +
                "FROM  " +
                "EC_PERSONAS,EC_PERSONAS_TERMINALES, EC_PERSONAS_DATOS, EC_TERMINALES WHERE EC_PERSONAS.PERSONA_BORRADO=0  AND EC_PERSONAS_TERMINALES.PERSONA_TERMINAL_BORRADO=0 AND EC_PERSONAS_TERMINALES.TERMINAL_ID="
                + Terminal.TERMINAL_ID + " AND " +
                "EC_TERMINALES.TERMINAL_ID = EC_PERSONAS_TERMINALES.TERMINAL_ID AND " +
                "EC_PERSONAS.PERSONA_ID=EC_PERSONAS_TERMINALES.PERSONA_ID AND EC_PERSONAS.PERSONA_ID=EC_PERSONAS_DATOS.PERSONA_ID  ");
                */
                string Qry = "SELECT PERSONA_ID,PERSONA_LINK_ID,PERSONA_NOMBRE, \n" +
                    " DATO_LLAVE as PERSONAS_A_VEC_T1,DATO_ADD as PERSONAS_A_VEC_T2,'' as PERSONAS_A_VEC_T3, \n" +
                    " PERSONAS_A_VEC_1,PERSONAS_A_VEC_2,PERSONAS_A_VEC_3,PERSONA_ID_S_HUELLA,PERSONA_S_HUELLA_CLAVE FROM EC_V_PERSONAS_TERMINALES WHERE PERSONA_TERMINAL_BORRADO = 0 AND TERMINAL_ID="
                + Terminal.TERMINAL_ID;
                DataSet DS = CeC_BD.EjecutaDataSet(Qry);


                foreach (DataRow DR in DS.Tables[0].Rows)
                {

                    try
                    {
                        DS_WSPersonasTerminales.DT_PersonasTerminalesRow FilaNueva = DSChecador.DT_PersonasTerminales.NewDT_PersonasTerminalesRow();
                        FilaNueva.PERSONA_ID = Convert.ToInt32(DR["PERSONA_ID"]);

                        FilaNueva.PERSONA_LINK_ID = Convert.ToInt32(DR["PERSONA_LINK_ID"]);
                        FilaNueva.PERSONA_NOMBRE = Convert.ToString(DR["PERSONA_NOMBRE"]);
                        try { FilaNueva.PERSONAS_A_VEC_T1 = Convert.ToString(DR["PERSONAS_A_VEC_T1"]); }
                        catch { }
                        try { FilaNueva.PERSONAS_A_VEC_T2 = Convert.ToString(DR["PERSONAS_A_VEC_T2"]); }
                        catch { }
                        try { FilaNueva.PERSONAS_A_VEC_T3 = Convert.ToString(DR["PERSONAS_A_VEC_T3"]); }
                        catch { }
                        //                            if (FilaNueva.IsPERSONA_ID_S_HUELLANull() || FilaNueva.PERSONA_ID_S_HUELLA == 0)
                        {
                            try { FilaNueva.PERSONAS_A_VEC_1 = (byte[])DR["PERSONAS_A_VEC_1"]; }
                            catch { }
                            try { FilaNueva.PERSONAS_A_VEC_2 = (byte[])DR["PERSONAS_A_VEC_2"]; }
                            catch { }
                            try { FilaNueva.PERSONAS_A_VEC_3 = (byte[])DR["PERSONAS_A_VEC_3"]; }
                            catch { }
                            FilaNueva.PERSONA_ID_S_HUELLA = 0;
                        }

                        try { FilaNueva.PERSONA_ID_S_HUELLA = Convert.ToInt32(DR["PERSONA_ID_S_HUELLA"]); }
                        catch { }
                        try { FilaNueva.PERSONA_S_HUELLA_CLAVE = Convert.ToString(DR["PERSONA_S_HUELLA_CLAVE"]); }
                        catch { }
                        DSChecador.DT_PersonasTerminales.AddDT_PersonasTerminalesRow(FilaNueva);
                    }
                    catch (Exception ex)
                    {
                        CIsLog2.AgregaError(ex);

                    }
                }
                return DSChecador;
                /*DS_WSPersonasTerminalesTableAdapters.DA_PersonasTerminales DAChecador = new DS_WSPersonasTerminalesTableAdapters.DA_PersonasTerminales();
                DS_WSPersonasTerminalesTableAdapters.DA_Personas DAChecadorPersonas = new DS_WSPersonasTerminalesTableAdapters.DA_Personas();
                int ret = DAChecadorPersonas.InsertPersonas(Convert.ToDecimal(TerminalID));
                DAChecador.Fill(DSChecador.DT_PersonasTerminales, Convert.ToDecimal(TerminalID));
                for (int i = 0; i < DSChecador.DT_PersonasTerminales.Rows.Count; i++)
                {
                    if (CampoLlave != "")
                    {
                        qry = "SELECT " + CampoLlave + " FROM EC_PERSONAS_DATOS WHERE " + CeC_Campos.CampoTE_Llave + " = " + DSChecador.DT_PersonasTerminales.Rows[i][DSChecador.DT_PersonasTerminales.PERSONA_LINK_IDColumn.Caption];
                        DSChecador.DT_PersonasTerminales.Rows[i][DSChecador.DT_PersonasTerminales.PERSONAS_A_VEC_T1Column.Caption] = CeC_BD.EjecutaEscalarString(qry);
                    }
                    else
                        DSChecador.DT_PersonasTerminales.Rows[i][DSChecador.DT_PersonasTerminales.PERSONAS_A_VEC_T1Column.Caption] = "";

                    if (CampoLlaveAdicional != "")
                    {
                        qry = "SELECT " + CampoLlaveAdicional + " FROM EC_PERSONAS_DATOS WHERE " + CeC_Campos.CampoTE_Llave + " = " + DSChecador.DT_PersonasTerminales.Rows[i][DSChecador.DT_PersonasTerminales.PERSONA_LINK_IDColumn.Caption];
                        DSChecador.DT_PersonasTerminales.Rows[i][DSChecador.DT_PersonasTerminales.PERSONAS_A_VEC_T2Column.Caption] = CeC_BD.EjecutaEscalarString(qry);
                    }
                    else
                        DSChecador.DT_PersonasTerminales.Rows[i][DSChecador.DT_PersonasTerminales.PERSONAS_A_VEC_T2Column.Caption] = "";
                }
                return DSChecador;*/

            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return null;
        }
    }

    [WebMethod(EnableSession = true)]
    public DS_WSPersonasTerminales ListaEmpleadosV5(int TerminalID)
    {
        if (ObtenSESION_ID() <= 0)
            return null;

        DS_WSPersonasTerminales DSChecador;
        try
        {
            DSChecador = new DS_WSPersonasTerminales();

            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(
                "SELECT PERSONA_ID,DATO_LLAVE,DATO_ADD,PERSONAS_A_VEC_1,PERSONAS_A_VEC_2,PERSONAS_A_VEC_3,PERSONA_ID_S_HUELLA,PERSONA_S_HUELLA_CLAVE,PERSONA_LINK_ID, PERSONA_NOMBRE,PERSONA_TERMINAL_BORRADO " +
                " FROM EC_V_PERSONAS_TERM_CAMBIOS WHERE TERMINAL_ID =" + TerminalID);
            foreach (DataRow DR in DS.Tables[0].Rows)
            {

                try
                {
                    DS_WSPersonasTerminales.DT_PersonasTerminalesRow FilaNueva = DSChecador.DT_PersonasTerminales.NewDT_PersonasTerminalesRow();
                    FilaNueva.PERSONA_ID = CeC.Convierte2Int(DR["PERSONA_ID"]);


                    FilaNueva.PERSONA_LINK_ID = CeC.Convierte2Int(DR["PERSONA_LINK_ID"]);
                    FilaNueva.PERSONA_NOMBRE = CeC.Convierte2String(DR["PERSONA_NOMBRE"]);

                    try { FilaNueva.PERSONAS_A_VEC_T1 = CeC.Convierte2String(DR["DATO_LLAVE"]); }
                    catch { }
                    try { FilaNueva.PERSONAS_A_VEC_T2 = CeC.Convierte2String(DR["DATO_ADD"]); }
                    catch { }


                    try { FilaNueva.PERSONAS_A_VEC_1 = (byte[])DR["PERSONAS_A_VEC_1"]; }
                    catch { }
                    try { FilaNueva.PERSONAS_A_VEC_2 = (byte[])DR["PERSONAS_A_VEC_2"]; }
                    catch { }
                    try { FilaNueva.PERSONAS_A_VEC_3 = (byte[])DR["PERSONAS_A_VEC_3"]; }
                    catch { }
                    FilaNueva.PERSONA_ID_S_HUELLA = 0;

                    try { FilaNueva.PERSONA_ID_S_HUELLA = CeC.Convierte2Int(DR["PERSONA_ID_S_HUELLA"]); }
                    catch { }
                    try { FilaNueva.PERSONA_S_HUELLA_CLAVE = CeC.Convierte2String(DR["PERSONA_S_HUELLA_CLAVE"]); }
                    catch { }
                    if (CeC.Convierte2Bool(DR["PERSONA_TERMINAL_BORRADO"]))
                        FilaNueva.PERSONAS_A_VEC_T2 = "<BORRAR>";
                    DSChecador.DT_PersonasTerminales.AddDT_PersonasTerminalesRow(FilaNueva);
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);

                }
            }
            return DSChecador;




        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return null;
        }
    }

    public bool AgregaTerminalesDExtras(int TerminalID, Tipo_DEXTRAS Tipo, string Texto, byte[] Bin, int LenBin)
    {
        if (ObtenSESION_ID() <= 0)
            return false;
        if (LenBin <= 0) return false;
        byte[] Datos = new byte[LenBin];
        Array.Copy(Bin, 0, Datos, 0, LenBin);
        return AgregaTerminalesDExtras(TerminalID, Tipo, Texto, "", Datos);

    }

    public bool AgregaTerminalesDExtras(int TerminalID, Tipo_DEXTRAS Tipo, string Texto)
    {
        if (ObtenSESION_ID() <= 0)
            return false;
        return AgregaTerminalesDExtras(TerminalID, Tipo, Texto, "", null);
    }
    public bool AgregaTerminalesDExtras(int TerminalID, Tipo_DEXTRAS Tipo, string Texto1, string Texto2)
    {
        if (ObtenSESION_ID() <= 0)
            return false;
        return AgregaTerminalesDExtras(TerminalID, Tipo, Texto1, Texto2, null);
    }
    public bool AgregaTerminalesDExtras(int TerminalID, Tipo_DEXTRAS Tipo, string Texto, byte[] Bin)
    {
        if (ObtenSESION_ID() <= 0)
            return false;
        return AgregaTerminalesDExtras(TerminalID, Tipo, Texto, "", Bin);
    }
    public bool AgregaTerminalesDExtras(int TerminalID, Tipo_DEXTRAS Tipo, string Texto1, string Texto2, byte[] Bin)
    {
        if (ObtenSESION_ID() <= 0)
            return false;

        return CeC_Terminales_DExtras.AgregaDExtra(TerminalID, CeC.Convierte2Int(Tipo), ObtenSESION_ID(), ObtenSUSCRIPCION_ID(), Texto1, Texto2, Bin);


    }
    [WebMethod(EnableSession = true)]
    public bool AgregaChecada(int TerminalID, string Llave, DateTime FechaHora, TipoAccesos TAcceso)
    {
        if (ObtenSESION_ID() <= 0)
            return false;
        //return CeC_Accesos.AgregaChecada(TerminalID, Llave, FechaHora, CeC.Convierte2Int(TAcceso), ObtenSESION_ID(), ObtenSUSCRIPCION_ID(), true);
        return CeC_Accesos.AgregaChecada(TerminalID, Llave, FechaHora, CeC.Convierte2Int(TAcceso), ObtenSESION_ID(), ObtenSUSCRIPCION_ID(), false);
        /*
        int Persona_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS_TERMINALES WHERE PERSONA_TERMINAL_CAMPO_L = '" + Llave + "' AND TERMINAL_ID = " + TerminalID);
        if (Persona_ID <= 0)
            Persona_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS_TERMINALES WHERE PERSONA_TERMINAL_CAMPO_A = '" + Llave + "' AND TERMINAL_ID = " + TerminalID);

        if (Persona_ID > 0)
            if (RegistrarChecada(Persona_ID, TerminalID, Convert.ToInt32(TAcceso), FechaHora))
                return true;
        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-MX");

        bool Ret = AgregaTerminalesDExtras(TerminalID, Tipo_DEXTRAS.Accesos, Llave, FechaHora.ToString(culture) + ";" + Convert.ToInt32(TAcceso).ToString());
        CeC_Asistencias.CambioAccesosNAsign();
        return Ret;
        */
    }

    /// <summary>
    /// Envia un listado de checadas donde no esta validado el numero de empleado o cualfuera su
    /// campo llave.
    /// </summary>
    /// <param name="TerminalID">Identificador de terminal</param>
    /// <param name="Checadas"></param>
    /// <returns></returns>
    [WebMethod(Description = "Envia las checadas", EnableSession = true)]
    public bool EnviaChecadas(int TerminalID, DS_WSPersonasTerminales.DT_CHECADASDataTable Checadas)
    {

        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaLog("EnviaChecadas > No ha iniciado Sesion");
            return false;
        }
        if (Checadas == null)
        {
            CIsLog2.AgregaLog("EnviaChecadas > No hay checadas");
            return false;
        }

        try
        {
            CIsLog2.AgregaLog("EnviaChecadas DateTime.Now" + DateTime.Now.ToString());
            foreach (DS_WSPersonasTerminales.DT_CHECADASRow Fila in Checadas)
            {
                CIsLog2.AgregaLog("EnviaChecadas " + Fila.FECHAHORA.ToString());
                if (AgregaChecada(TerminalID, Fila.ID, Fila.FECHAHORA, (TipoAccesos)Fila.TIPO_ACCESO_ID) == false)
                    return false;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return true;
    }
    /// <summary>
    /// Determina si alguna persona existe en una terminal
    /// </summary>
    /// <param name="PersonaID">Identificador de la persona</param>
    /// <param name="TerminalID">identificador de la terminal</param>
    /// <returns>Regresa verdadero si la persona puede accesar a la terminal, en caso contrario regresara falso</returns>
    [WebMethod(Description = "Verifica si esta asignada la persona a la terminal", EnableSession = true)]
    public bool ExistePersonaIDTerminalID(int PersonaID, int TerminalID)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ExistePersonaIDTerminalID >> No ha Iniciado Sesion");
            return false;
        }
        string qry = "SELECT PERSONA_ID FROM EC_PERSONAS_TERMINALES WHERE PERSONA_ID = " + PersonaID.ToString() + " AND TERMINAL_ID = " + TerminalID.ToString();
        int ret = CeC_BD.EjecutaEscalarInt(qry);
        if (ret != -9999)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Obtiene a la persona que tienen ese persona_link_id
    /// </summary>
    /// <param name="PersonaID"></param>
    /// <returns></returns>

    [WebMethod(Description = "Obtiene un listado de personas con el mismo Campo Llave", EnableSession = true)]
    public DS_WS_eCheck.PERSONASDataTable ObtenPersonas(int Persona_Link_ID)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("Asigna Parametro >> No ha iniciado Sesion");
            return null;
        }
        //          DS_WS_eCheck DS = new DS_WS_eCheck();
        DS_WS_eCheckTableAdapters.PERSONASableAdapter TA = new DS_WS_eCheckTableAdapters.PERSONASableAdapter();
        //            TA.FillPersona_ID(DS.PERSONAS, CeC_Personas.ObtenPersonaID(Persona_Link_ID));
        return TA.GetDataPersona_ID(CeC_Personas.ObtenPersonaID(Persona_Link_ID));
        //        return DS.PERSONAS;
    }
    /// <summary>
    /// Obtiene el Persona_ID a partir del Persona_Link_ID
    /// </summary>
    [WebMethod(Description = "Obtiene el identificador de la persona", EnableSession = true)]
    public int ObtenPersonaID(int Persona_Link_ID)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenPersonaID >> No ha iniciado Sesion");
            return -9999;
        }
        return CeC_Personas.ObtenPersonaID(Persona_Link_ID, ObtenUsuario_ID());
    }

    /// <summary>
    /// Obtiene el Persona_Link_ID a partir del Persona_ID
    /// </summary>
    [WebMethod(Description = "Obtiene el identificador del empleado", EnableSession = true)]
    public int ObtenPersonaLinkID(int Persona_ID)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenPersonaLinkID >> No ha iniciado Sesion");
            return -9999;
        }
        return CeC_BD.ObtenPersonaLinkID(Persona_ID);
    }
    [WebMethod(Description = "Obtiene el identificador de la terminal a partir de la ip y el identificador 485", EnableSession = true)]
    public int ObtenTerminalIDIp(string IP, int ID485)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenPersonaID >> No ha iniciado Sesion");
            return -9999;
        }
        int Terminal_ID = CeC_BD.EjecutaEscalarInt("SELECT TERMINAL_ID FROM EC_TERMINALES WHERE TERMINAL_DIR LIKE '%Red:" + IP + "%'");
        if (Terminal_ID > 0)
        {
            CeC_Terminales_Param Term = new CeC_Terminales_Param(Terminal_ID);
            if (Term.SITIO_HIJO_ID > 0)
            {
                Terminal_ID = CeC_BD.EjecutaEscalarInt("SELECT TERMINAL_ID FROM EC_TERMINALES WHERE TERMINAL_DIR LIKE '%RS485:%:%:" + ID485.ToString() + "'");
            }
        }
        return Terminal_ID;
    }
    [WebMethod(Description = "Obtiene el identificador de la persona", MessageName = "ObtenPersonaIDWindowsCE")]
    public int ObtenPersonaID(int Persona_Link_ID, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return -9999;
        return CeC_Personas.ObtenPersonaID(Persona_Link_ID);
    }
    [WebMethod(Description = "Valida el horario de turnos de una persona", MessageName = "ValidarHorarioWindowsCE")]
    public bool ValidarHorario(int PersonaID, bool EsEntrada, string Usuario, string Password)
    {
        if (CeC_Sesion.ValidarUsuarioeC(Usuario, Password) <= 0)
            return false;
        string qry = "";
        int TurnoDiaID = 0;
        DateTime FechaHoraActual = ObtenFechaHora();
        DateTime HEMin;
        DateTime HEMax;
        DateTime HE;
        DateTime HSMin;
        DateTime HSMax;
        DateTime HS;
        CeC_Asistencias.GeneraPrevioPersonaDiario(PersonaID);
        CeC_Turnos.AsignaHorario(PersonaID, FechaHoraActual);
        int Persona_Diario_ID = CeC_Asistencias.EstaDetronHorario(PersonaID, ObtenFechaHora());
        if (Persona_Diario_ID < 0)
            return false;
        if (EsEntrada)
        {
            qry = "SELECT TURNO_DIA_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID = " + Persona_Diario_ID.ToString();
            TurnoDiaID = CeC_BD.EjecutaEscalarInt(qry);
            if (TurnoDiaID != -9999)
            {
                HE = DateTime.Today + CeC_BD.DateTime2TimeSpan(CeC_BD.EjecutaEscalarDateTime("SELECT TURNO_DIA_HE FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + TurnoDiaID.ToString()));
                HEMin = DateTime.Today + CeC_BD.DateTime2TimeSpan(CeC_BD.EjecutaEscalarDateTime("SELECT TURNO_DIA_HEMIN FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + TurnoDiaID.ToString()));
                HEMax = DateTime.Today + CeC_BD.DateTime2TimeSpan(CeC_BD.EjecutaEscalarDateTime("SELECT TURNO_DIA_HEMAX FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + TurnoDiaID.ToString()));
                if (FechaHoraActual < HE || FechaHoraActual < HEMax || FechaHoraActual < HEMin)
                    return true;
                if (FechaHoraActual > HEMax)
                    return false;
            }
            else
                return false;
        }
        else
        {
            if (Cec_Incidencias.TieneIncidencia(PersonaID, ObtenFechaHora()) > 0)
                return true;
            else
            {
                qry = "SELECT TURNO_DIA_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID = " + Persona_Diario_ID.ToString();
                TurnoDiaID = CeC_BD.EjecutaEscalarInt(qry);
                if (TurnoDiaID != -9999)
                {
                    HS = DateTime.Today + CeC_BD.DateTime2TimeSpan(CeC_BD.EjecutaEscalarDateTime("SELECT TURNO_DIA_HS FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + TurnoDiaID.ToString()));
                    HSMin = DateTime.Today + CeC_BD.DateTime2TimeSpan(CeC_BD.EjecutaEscalarDateTime("SELECT TURNO_DIA_HSMIN FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + TurnoDiaID.ToString()));
                    HSMax = DateTime.Today + CeC_BD.DateTime2TimeSpan(CeC_BD.EjecutaEscalarDateTime("SELECT TURNO_DIA_HSMAX FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + TurnoDiaID.ToString()));
                    if (FechaHoraActual < HSMin)
                        return false;
                    if (FechaHoraActual > HS)
                        return true;
                    if (FechaHoraActual > HSMax)
                        return true;
                }
                else
                    return false;
            }
        }
        return false;
    }
    /// <summary>
    /// Obtiene a las personas que tienen ese nombre
    /// </summary>
    /// <param name="Persona_Nombre"></param>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene los nombres de las personas activas", EnableSession = true)]
    public DS_WS_eCheck.PERSONASDataTable ObtenPersonasNombre(string Persona_Nombre)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenPersonaNombre>> No ha iniciado Sesion");
            return null;
        }
        DS_WS_eCheck DS = new DS_WS_eCheck();
        DS_WS_eCheckTableAdapters.PERSONASableAdapter TA = new DS_WS_eCheckTableAdapters.PERSONASableAdapter();
        TA.ActualizaIn("'%" + Persona_Nombre + "%'");
        return TA.GetDataByPersona_Nombre();
    }
    /// <summary>
    /// Obtiene el numero de personas activas en la base de datos
    /// </summary>
    /// <param name="Persona_Nombre"></param>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene el numero de personas en la base de datos activas", EnableSession = true)]
    public int ObtenNoPersonas()
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("ObtenNoPersonas>> No ha iniciado Sesion");
            return -1;
        }
        return CeC_BD.ObtenNoPersonas();
    }
    /// <summary>
    /// Obtiene el valor de un campo de la tabla EC_PERSONAS_DATOS
    /// </summary
    /// <param name="Campo_Nombre"></param>
    /// <param name="PersonaID"></param>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene el valor de un campo de la tabla EC_PERSONAS_DATOS", MessageName = "ObtenValorCampo")]
    public object ObtenValorCampo(string Campo_Nombre, int PersonaID)
    {

        string qryempleado = "SELECT PERSONA_LINK_ID FROM EC_PERSONAS WHERE PERSONA_ID = " + PersonaID;
        int empleado = CeC_BD.EjecutaEscalarInt(qryempleado);
        string qrycampo = "SELECT " + Campo_Nombre + " FROM EC_PERSONAS_DATOS WHERE  PERSONA_LINK_ID= " + empleado;
        object ValorCampo = CeC_BD.EjecutaEscalarString(qrycampo);
        return ValorCampo;
    }
    public enum ModeloTerminales
    {
        No_definido = 0,
        TRAX,
        HandPunch,
        AC215,
        AC115,
        EnTRAX,
        Lector_de_Huella_USB,
        Lector_de_Huella_MS,
        Lector_6055,
        Lector_Windows_Chip,
        Lector_Serial_CB_BM,
        UlTRAX,
        Prox,
        SuperMax,
        Max,
        NeoMax,
        Camara_Usb,
        Camara_Intellinet,
        Panel_Plumas,
        BioEntryPlus,
        BioStation
    }
    public enum Tecnologias
    {
        Ninguna = 0,
        Código_de_Barras,
        Banda_Magnética,
        Proximidad,
        Mifare,
        Huella,
        Palma,
        Chip_de_contacto,
        Rostro
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Sesion_ID">Identificador de sesión el cual será verificado para poder agregar la termnal</param>
    /// <param name="NombreID">Es el identificador que se guardará en el nombre de la terminal </param>
    /// <param name="Modelo"></param>
    /// <param name="Tecnologia"></param>
    /// <param name="TecnologiaAdd"></param>
    /// <param name="CampoLlave"></param>
    /// <param name="CampoAdd"></param>
    /// <param name="Direccion"></param>
    /// <param name="Enrola"></param>
    /// <returns></returns>
    [WebMethod(Description = "Agrega o actualiza una terminal en el listado de terminales", MessageName = "AgregaTerminal", EnableSession = true)]
    public bool AgregaTerminal(int Sesion_ID, string NombreID, ModeloTerminales Modelo, Tecnologias Tecnologia, Tecnologias TecnologiaAdd, string CampoLlave, string CampoAdd, string Direccion, bool Enrola)
    {
        if (Sesion_ID <= 0)
            return false;
        if (!CeC_Sesion.SesionActiva(Sesion_ID))
            return false;
        Session["SESION_ID"] = Sesion_ID;
        try
        {
            int Terminal_ID = CeC_BD.EjecutaEscalarInt("SELECT TERMINAL_ID FROM EC_TERMINALES WHERE TERMINAL_NOMBRE LIKE '%" + NombreID + "%'");

            DS_TerminalesTableAdapters.EC_TERMINALESTableAdapter TA = new DS_TerminalesTableAdapters.EC_TERMINALESTableAdapter();
            DS_Terminales.EC_TERMINALESDataTable DT = new DS_Terminales.EC_TERMINALESDataTable();
            TA.Fill(DT, Terminal_ID);
            bool Nuevo = false;
            DS_Terminales.EC_TERMINALESRow Fila = null;
            if (DT.Rows.Count < 1)
            {
                Nuevo = true;
                Fila = DT.NewEC_TERMINALESRow();
                Fila.TERMINAL_ID = CeC_Autonumerico.GeneraAutonumerico("EC_TERMINALES", "TERMINAL_ID");
                Fila.TERMINAL_NOMBRE = Modelo.ToString().Replace("_", " ") + " (" + NombreID + ")";
                Fila.SITIO_ID = 1;
                Fila.ALMACEN_VEC_ID = 1;
                Fila.TERMINAL_BORRADO = 0;
                Fila.TERMINAL_ASISTENCIA = 1;
                Fila.TERMINAL_ENROLA = 1;
                Fila.TERMINAL_VALIDAHORARIOE = 0;
                Fila.TERMINAL_VALIDAHORARIOS = 0;
                Fila.TIPO_TERMINAL_ACCESO_ID = 0;
                Fila.TERMINAL_SINCRONIZACION = 0;
                Fila.TERMINAL_COMIDA = 0;
            }
            else
                Fila = DT[0];
            Fila.MODELO_TERMINAL_ID = Convert.ToInt32(Modelo);
            Fila.TERMINAL_CAMPO_LLAVE = CampoLlave;
            Fila.TERMINAL_CAMPO_ADICIONAL = CampoAdd;
            Fila.TIPO_TECNOLOGIA_ID = Convert.ToInt32(Tecnologia);
            Fila.TIPO_TECNOLOGIA_ADD_ID = Convert.ToInt32(TecnologiaAdd);
            Fila.TERMINAL_DIR = Direccion;
            if (Enrola)
                Fila.TERMINAL_ENROLA = 1;
            else
                Fila.TERMINAL_ENROLA = 0;

            if (Nuevo)
                DT.AddEC_TERMINALESRow(Fila);
            TA.Update(DT);
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);

        }

        return false;
    }
    /// <summary>
    /// Obtiene la lista de terminales de enrolamiento
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene la lista de terminales de enrolamiento", EnableSession = true)]
    public DS_WSPersonasTerminales.EC_TERMINALESDataTable ObtenTerminalesEnrolamiento(int Sesion_ID)
    {
        if (Sesion_ID <= 0 || !CeC_Sesion.SesionActiva(Sesion_ID))
        {
            CIsLog2.AgregaError("ObtenTerminalesEnrolamiento >> No ha iniciado Sesion");
            return null;
        }
        Session["SESION_ID"] = Sesion_ID;
        DS_WSPersonasTerminalesTableAdapters.EC_TERMINALESTableAdapter TA = new DS_WSPersonasTerminalesTableAdapters.EC_TERMINALESTableAdapter();
        return TA.GetDataEnroladoras();
    }
    /// <summary>
    /// Valida el AntiPassBack de un Empleado
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Valida el AntiPassBack de un Empleado", EnableSession = true)]
    public bool ValidaAntiPassBack(int PersonaID, int TipoChecada)
    {
        int AntipassBack = CeC_BD.EjecutaEscalarInt("SELECT EC_ACCESOS_1.PERSONA_ID FROM (SELECT MAX(ACCESO_ID) AS ACCESO_ID " +
                            "FROM EC_ACCESOS WHERE (PERSONA_ID = " + PersonaID.ToString() + ") AND (ACCESO_FECHAHORA <= CONVERT(Datetime, GETDATE(), 102)) " +
                            "AND (ACCESO_FECHAHORA > CONVERT(Datetime,CONVERT(CHAR, GETDATE(), 105), 103))) AS T INNER JOIN " +
                            "EC_ACCESOS AS EC_ACCESOS_1 ON T.ACCESO_ID = EC_ACCESOS_1.ACCESO_ID " +
                            "WHERE (EC_ACCESOS_1.TIPO_ACCESO_ID = " + TipoChecada.ToString() + ") ");
        if (AntipassBack > 0)
            return true;
        else
            return false;

    }


    /// <summary>
    /// Obtiene un DataSet con la lista de empleados y los horarios correspondientes al dia corriente
    /// </summary>
    /// <param name="TerminalID">Identificador de la terminal</param>
    /// <returns>Dataset con una tabla de empleados y otra de horarios</returns>
    [WebMethod(Description = "Devuelve la lista de los empleados y la lista de los turnos corrientes", EnableSession = true)]
    public DS_WSHorariosPersona HorariosPersona(int TerminalID)
    {
        if (ObtenSESION_ID() <= 0)
            return null;
        string CampoLlave = string.Empty;
        DS_WSHorariosPersona DSHorarios = new DS_WSHorariosPersona();
        try
        {
            CeC_Terminales_Param Terminal = new CeC_Terminales_Param(TerminalID);
            CampoLlave = Terminal.TERMINAL_CAMPO_LLAVE;
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            conexion.Open();
            OleDbDataAdapter DA_HorariosPersona = new OleDbDataAdapter("SELECT EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_PERSONAS_DIARIO.TURNO_DIA_ID," +
                    " EC_TURNOS_DIA.TURNO_DIA_HE, EC_TURNOS_DIA.TURNO_DIA_HS" +
                    " FROM EC_PERSONAS_DIARIO INNER JOIN EC_TURNOS_DIA ON EC_PERSONAS_DIARIO.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID" +
                    " WHERE (EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA = CONVERT(CHAR(12), GETDATE(), 103))" +
                    " GROUP BY EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_PERSONAS_DIARIO.TURNO_DIA_ID, EC_TURNOS_DIA.TURNO_DIA_HE, EC_TURNOS_DIA.TURNO_DIA_HS" +
                    " \n ORDER BY EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA,EC_PERSONAS_DIARIO.TURNO_DIA_ID", conexion);
            OleDbDataAdapter DA_Personas = new OleDbDataAdapter("SELECT EC_PERSONAS.PERSONA_ID," + CampoLlave + " AS CAMPO_LLAVE, EC_PERSONAS.PERSONA_NOMBRE," +
                    " EC_PERSONAS.TURNO_ID, EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID, EC_TURNOS_SEMANAL_DIA.DIA_SEMANA_ID, EC_PERSONAS_TERMINALES.TERMINAL_ID" +
                    " FROM EC_PERSONAS INNER JOIN EC_PERSONAS_DATOS ON EC_PERSONAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS.PERSONA_LINK_ID INNER JOIN" +
                    " EC_TURNOS_SEMANAL_DIA ON EC_PERSONAS.TURNO_ID = EC_TURNOS_SEMANAL_DIA.TURNO_ID INNER JOIN" +
                    " EC_TURNOS_DIA ON EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID INNER JOIN" +
                    " EC_PERSONAS_TERMINALES ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_TERMINALES.PERSONA_ID " +
                    " WHERE (EC_PERSONAS.PERSONA_BORRADO = 0) AND EC_PERSONAS_TERMINALES.TERMINAL_ID = " + TerminalID +
                    " \n ORDER BY EC_PERSONAS.PERSONA_ID, EC_TURNOS_SEMANAL_DIA.DIA_SEMANA_ID", conexion);
            DataSet DS = new DataSet();
            DA_HorariosPersona.Fill(DS, "Horarios");
            DA_Personas.Fill(DS, "Personas");
            conexion.Close();
            foreach (DataRow DR in DS.Tables["Horarios"].Rows)
            {
                try
                {
                    DS_WSHorariosPersona.DT_HorariosRow FilaHorarios = DSHorarios.DT_Horarios.NewDT_HorariosRow();
                    FilaHorarios.PERSONA_DIARIO_FECHA = Convert.ToDateTime(DR["PERSONA_DIARIO_FECHA"]);
                    FilaHorarios.TURNO_DIA_ID = Convert.ToInt32(DR["TURNO_DIA_ID"]);
                    FilaHorarios.TURNO_DIA_HE = Convert.ToDateTime(DR["TURNO_DIA_HE"]);
                    FilaHorarios.TURNO_DIA_HS = Convert.ToDateTime(DR["TURNO_DIA_HS"]);
                    DSHorarios.DT_Horarios.AddDT_HorariosRow(FilaHorarios);
                }
                catch { }
            }
            foreach (DataRow DR in DS.Tables["Personas"].Rows)
            {
                try
                {
                    DS_WSHorariosPersona.DT_PersonasRow FilaPersonas = DSHorarios.DT_Personas.NewDT_PersonasRow();
                    FilaPersonas.PERSONA_ID = Convert.ToInt32(DR["TURNO_DIA_ID"]);
                    FilaPersonas.CAMPO_LLAVE = Convert.ToString(DR["CAMPO_LLAVE"]);
                    FilaPersonas.PERSONA_NOMBRE = Convert.ToString(DR["PERSONA_NOMBRE"]);
                    FilaPersonas.TURNO_ID = Convert.ToInt32(DR["TURNO_ID"]);
                    FilaPersonas.TURNO_DIA_ID = Convert.ToInt32(DR["TURNO_DIA_ID"]);
                    FilaPersonas.DIA_SEMANA_ID = Convert.ToInt32(DR["DIA_SEMANA_ID"]);
                    FilaPersonas.TERMINAL_ID = Convert.ToInt32(DR["TERMINAL_ID"]);
                    DSHorarios.DT_Personas.AddDT_PersonasRow(FilaPersonas);
                }
                catch { }
            }
            return DSHorarios;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return null;
        }
    }
    [WebMethod(Description = "Obtiene el link de nuevo usuario")]
    public string ObtenLinkNuevoUsuario()
    {
        return CeC_Config.LinkNuevoUsuario;
    }

    [WebMethod(Description = "Obtiene el link de olvido de contraseña")]
    public string ObtenLinkOlvidoClave()
    {
        return CeC_Config.LinkOlvidoClave;
    }
    [WebMethod(Description = "no Ejecutar")]
    public DS_WSPersonasTerminales.DT_PersonasTerminalesDataTable ParcheDS_WSPersonasTerminales()
    {
        return null;
    }


    [WebMethod(Description = "Agrega un movimiento a Terminales DExtras", EnableSession = true)]
    public bool AgregaDExtra(int TerminalID, CeC_Terminales_DExtras.Tipo_Term_DEXTRAS Tipo, string Extra)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("AgregaDExtra >> No ha Iniciado Sesion");
            return false;
        }
        return CeC_Terminales_DExtras.AgregaDExtra(TerminalID, Tipo, ObtenSESION_ID(), ObtenSUSCRIPCION_ID(), Extra);
    }
    [WebMethod(Description = "Confirma la transmisión de una persona", EnableSession = true)]
    public bool Confirma(int TerminalID, int PersonaID)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("Confirma >> No ha Iniciado Sesion");
            return false;
        }

        if (CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_TERMINALES SET PERSONA_TERMINAL_L_FH_UC = DATO_LLAVE_UMOD, " +
"PERSONA_TERMINAL_A_FH_UC = DATO_ADD_UMOD, " +
"PERSONA_TERMINAL_V1_FH_UC = PERSONAS_A_VEC_1_UMOD," +
"PERSONA_TERMINAL_V2_FH_UC = PERSONAS_A_VEC_2_UMOD," +
"PERSONA_TERMINAL_V3_FH_UC = PERSONAS_A_VEC_3_UMOD," +
"PERSONA_TERMINAL_SH_UC = PERSONA_S_HUELLA_FECHA, " +
"PERSONA_TERMINAL_B_APLICADO = PERSONA_TERMINAL_B_FH,  " +
"PERSONA_TERMINAL_UPDATE = " + CeC_BD.SqlFechaHora(DateTime.Now) + " " +
"FROM EC_PERSONAS_TERMINALES " +
"INNER JOIN " +
"EC_V_PERSONAS_TERMINALES ON EC_PERSONAS_TERMINALES.PERSONA_ID = EC_V_PERSONAS_TERMINALES.PERSONA_ID AND  " +
"EC_PERSONAS_TERMINALES.TERMINAL_ID = EC_V_PERSONAS_TERMINALES.TERMINAL_ID " +
 " WHERE EC_PERSONAS_TERMINALES.TERMINAL_ID = " + TerminalID + " AND EC_PERSONAS_TERMINALES.PERSONA_ID = " + PersonaID) > 0)
            return true;
        return false;
    }
    [WebMethod(Description = "Confirma la transmisión de una persona con Error", EnableSession = true)]
    public bool ConfirmaError(int TerminalID, int PersonaID, string Motivo)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("Confirma >> No ha Iniciado Sesion");
            return false;
        }
        if (CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_TERMINALES SET PERSONA_TERMINAL_ERROR = '" + CeC_BD.ObtenParametroCadena(Motivo)
            + "', PERSONA_TERMINAL_ERRORFH = " + CeC_BD.SqlFechaHora(DateTime.Now) +
             " WHERE EC_PERSONAS_TERMINALES.TERMINAL_ID = " + TerminalID + " AND EC_PERSONAS_TERMINALES.PERSONA_ID = " + PersonaID) > 0)
            return true;
        return false;
    }
    [WebMethod(Description = "Obtiene los datos de una persona", EnableSession = true)]
    public DataSet ObtenDatosPersona(int Terminal_ID, string ValorCampoLlave)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("Confirma >> No ha Iniciado Sesion");
            return null;
        }
        return CeC_Terminales.ObtenDatosPersona(Terminal_ID, ValorCampoLlave);
    }

    [WebMethod(Description = "Indica si una persona_id tiene acceso por determinada terminal", EnableSession = true)]
    public bool TieneAcceso(int Terminal_ID, int PersonaID)
    {
        if (ObtenSESION_ID() <= 0)
        {
            CIsLog2.AgregaError("Confirma >> No ha Iniciado Sesion");
            return false;
        }

        return CeC_Terminales.TieneAcceso(Terminal_ID, PersonaID) > 0;
    }
}
