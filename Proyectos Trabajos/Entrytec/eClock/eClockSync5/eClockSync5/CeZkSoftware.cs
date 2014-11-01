using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace eClockSync5
{
    class CeZkSoftware : CeTerminalSync
    {
        public zkemkeeper.CZKEMClass m_ZKActivex = null;


        private int m_NoChecadas = 0;
        public bool EsiClock = false;
        bool EsMerck = eClockSync5.Properties.Settings.Default.EsMerck;
        private object m_Acepta_TA = null;
        public TipoAccesos ObtenTipoAcceso(int TipoAccesoTerminal)
        {
            if (m_Acepta_TA == null)
                m_Acepta_TA = TERMINAL_ACEPTA_TA;
            if (!Convert.ToBoolean(m_Acepta_TA))
                return TipoAccesos.Correcto;
            TipoAccesos TA = TipoAccesos.Correcto;
            switch (TipoAccesoTerminal)
            {
                case 1: TA = TipoAccesos.Correcto; break;
                case 0:
                    TA = TipoAccesos.Salida; break;
            }
            return TA;
        }

        public override bool Conecta()
        {
            try
            {
                m_ZKActivex = new zkemkeeper.CZKEMClass();
                m_ZKActivex.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(m_ZKActivex_OnFinger);
                m_ZKActivex.OnConnected += new zkemkeeper._IZKEMEvents_OnConnectedEventHandler(m_ZKActivex_OnConnected);
                m_ZKActivex.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(m_ZKActivex_OnVerify);
                m_ZKActivex.OnAttTransaction += new zkemkeeper._IZKEMEvents_OnAttTransactionEventHandler(m_ZKActivex_OnAttTransaction);
                m_ZKActivex.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(m_ZKActivex_OnAttTransactionEx);
                m_ZKActivex.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(m_ZKActivex_OnFingerFeature);
                m_ZKActivex.OnKeyPress += new zkemkeeper._IZKEMEvents_OnKeyPressEventHandler(m_ZKActivex_OnKeyPress);
                m_ZKActivex.OnEnrollFinger += new zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(m_ZKActivex_OnEnrollFinger);
                m_ZKActivex.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(m_ZKActivex_OnDeleteTemplate);
                m_ZKActivex.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(m_ZKActivex_OnNewUser);
                m_ZKActivex.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(m_ZKActivex_OnHIDNum);
                m_ZKActivex.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(m_ZKActivex_OnAlarm);
                m_ZKActivex.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(m_ZKActivex_OnDoor);
                m_ZKActivex.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(m_ZKActivex_OnWriteCard);
                m_ZKActivex.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(m_ZKActivex_OnEmptyCard);
                if (EsMerck)
                    AgregaLog("EsMerck");

                if (m_TConexion.NoTerminal == 0)
                    m_TConexion.NoTerminal = 1;

                m_Conectado = false;
                switch (m_TConexion.TipoConexion)
                {
                    case CeC_Terminales.tipo.Red:
                        try
                        {


                            if (!m_ZKActivex.Connect_Net(m_TConexion.Direccion, m_TConexion.Puerto))
                            {
                                AgregaError("No se pudo conectar a Red " + m_TConexion.Direccion + ":" + m_TConexion.Puerto);
                                return false;
                            }
                        }
                        catch (Exception ex) { AgregaError(ex); AgregaError("Posiblemente el activex no este disponible"); return false; }
                        break;
                    case CeC_Terminales.tipo.USB:
                        if (!m_ZKActivex.Connect_USB(m_TConexion.NoTerminal))
                        {
                            AgregaError("No se pudo conectar a USB " + m_TConexion.NoTerminal);
                            return false;
                        }
                        break;
                    case CeC_Terminales.tipo.Serial:
                        if (!m_ZKActivex.Connect_Com(m_TConexion.Puerto, m_TConexion.NoTerminal, m_TConexion.Velocidad))
                        {
                            AgregaError("No se pudo conectar a Serial " + m_TConexion.Puerto + ":" + m_TConexion.NoTerminal + ":" + m_TConexion.Velocidad);
                            return false;
                        }
                        break;
                }

                m_Conectado = true;
                TERMINAL_MSG = "Conectado";
                return true;
            }
            catch (Exception ex)
            {

                CeLog2.AgregaError(ex);
                Desconecta();
            }


            return false;
        }

        public override bool Desconecta()
        {

            if (m_Conectado)
                m_ZKActivex.Disconnect();
            //            m_ZKActivex.Dispose();
            //m_FrmZK.Dispose();
            /*            m_ZKActivex = null;
                        m_FrmZK = null;*/

            return m_Conectado = false;
        }

        public override Errores PoleoChecadas()
        {
            try
            {
                int _errorCode = 0;
                int _machineNumber = 0;
                int _enrollNumber = 0;
                int _enrollMachineNumber = 0;
                int _verifyMode = 0;
                int _inOutMode = 0;
                int _year = 0;
                int _month = 0;
                int _day = 0;
                int _hour = 0;
                int _minute = 0;
                int _seccond = 0;
                int _WorkCode = 0;
                if (!m_ZKActivex.ReadGeneralLogData(m_TConexion.NoTerminal))
                {
                    m_ZKActivex.GetLastError(ref _errorCode);
                    if (_errorCode != 0)
                    {
                        AgregaError("PoleoChecadas Error Code: " + _errorCode.ToString());
                        return Errores.Error_IO;
                    }
                    return Errores.No_Requiere;
                }
                _errorCode = 1;
                m_NoChecadas = ObtenNoChecadas();


                while (_errorCode == 1)
                {
                    string Identificador = "";
                    bool RespLog = false;
                    if (EsiClock)
                    {
                        string NoMaquina = "";
                        RespLog = m_ZKActivex.SSR_GetGeneralLogData(m_TConexion.NoTerminal, out Identificador, out _verifyMode,
                            out _inOutMode, out _year, out _month, out _day, out _hour, out _minute, out _seccond, ref _WorkCode);
                    }
                    else
                    {
                        RespLog = m_ZKActivex.GetGeneralLogData(m_TConexion.NoTerminal, ref _machineNumber, ref _enrollNumber, ref _enrollMachineNumber,
                        ref _verifyMode, ref _inOutMode, ref _year, ref _month, ref _day, ref _hour, ref _minute);
                        Identificador = _enrollNumber.ToString();
                    }

                    if (RespLog)
                    {
                        try
                        {
                            DateTime Fecha = new DateTime(_year, _month, _day, _hour, _minute, _seccond);
                            TipoAccesos TA = ObtenTipoAcceso(_inOutMode);
                            if (EsMerck)
                            {
                                ///Solo válido para Merck
                                if (Identificador.Length > 4 && m_DatosTerminal.TERMINAL_CAMPO_ADICIONAL.Length > 0)
                                {
                                    Identificador = Identificador.Substring(0, Identificador.Length - 4);
                                }
                            }
                            AgregaChecada(Identificador, Fecha, TA);
                        }
                        catch (Exception ex)
                        {
                            StringBuilder _data = new StringBuilder();
                            _data.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7}", _enrollNumber,
        _verifyMode, _inOutMode, _year, _month, _day, _hour, _minute);
                            AgregaError(ex);
                            AgregaError("Datos Checada " + _data.ToString());
                        }
                    }
                    m_ZKActivex.GetLastError(ref _errorCode);
                }
                if (_errorCode >= 0)
                    return Errores.Correcto;
                return Errores.Error_IO;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
            return Errores.Error_Desconocido;

        }
        public int ObtenNoChecadas()
        {
            int _value = 0;
            if (m_ZKActivex.GetDeviceStatus(1, 6, ref _value))
                return _value;
            return -9999;
        }
        private DateTime ObtenFechaHora()
        {
            int _year = 0;
            int _month = 0;
            int _day = 0;
            int _hour = 0;
            int _minute = 0;
            int _seccond = 0;
            if (m_ZKActivex.GetDeviceTime(m_TConexion.NoTerminal, ref _year, ref _month, ref _day, ref _hour, ref _minute, ref _seccond))
            {
                return new DateTime(_year, _month, _day, _hour, _minute, _seccond);
            }
            return DateTime.Now;
        }
        protected override Errores BorraChecadas()
        {
            if (ObtenNoChecadas() != m_NoChecadas)
                return Errores.No_Requiere;
            if (m_ZKActivex.ClearGLog(m_TConexion.NoTerminal))
                return Errores.Correcto;
            return Errores.Error_IO;
        }
        override protected Errores AsignaFechaHora(DateTime FechaHora)
        {
            DateTime FH = ObtenFechaHora();
            TimeSpan TS = FechaHora - FH;
            if (Math.Abs(TS.TotalSeconds) < 40)
                return Errores.No_Requiere;
            if (m_ZKActivex.SetDeviceTime2(m_TConexion.NoTerminal, FechaHora.Year, FechaHora.Month, FechaHora.Day, FechaHora.Hour, FechaHora.Minute, FechaHora.Second))
            {
                return Errores.Correcto;
            }
            AgregaError("No se pudo AsignaFechaHora Error = ");
            return Errores.Error_IO;
        }

        protected string ObtenIDEmpleado(WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila)
        {
            try
            {
                string IDEmpleado = Convert.ToInt32(Fila.PERSONAS_A_VEC_T1).ToString();
                //                return IDEmpleado;
                ///Solo obtiene el campo llave
                try
                {
                    if (EsMerck && Fila.PERSONAS_A_VEC_T2.Length > 0)
                    {
                        uint NS = Convert.ToUInt32(Fila.PERSONAS_A_VEC_T2);
                        //AgregaLog( "IDEmpleado = "+ IDEmpleado + ", NS = " + NS);
                        IDEmpleado = Convert.ToInt32(Fila.PERSONA_LINK_ID).ToString("") + Convert.ToInt32(NS % 10000).ToString("0000");
                    }
                }
                catch { }
                return IDEmpleado;
            }
            catch
            {
                return "Error";
            }
        }
        protected override Errores EnvioListaBlanca()
        {
            Tecnologias Tecno = (Tecnologias)m_DatosTerminal.TIPO_TECNOLOGIA_ID;
            if (Tecno != Tecnologias.Huella && Tecno != Tecnologias.Rostro)
                return EnvioVectores();
            return Errores.No_Requiere;
        }

        static bool gBorrarHuellas = eClockSync5.Properties.Settings.Default.BorrarHuellas;
        static string gNArchivo = "";
        protected override Errores EnvioVectores()
        {

            int ErroresGenerados = 0;
            if (m_DatosEmpleados == null)
                return Errores.Error_Desconocido;

            if (m_DatosEmpleados.Rows.Count <= 0)
            {
                IUVectoresNuevo = "";
                return Errores.No_Requiere_Cambio_En_Vectores;
            }
            if (!m_Conectado)
                return Errores.Error_Conexion;
            if (!m_ZKActivex.EnableDevice(m_TConexion.NoTerminal, false))
                return Errores.Error_Conexion;
            Tecnologias Tecno = (Tecnologias)m_DatosTerminal.TIPO_TECNOLOGIA_ID;
            bool EsAsistencia = false;
            if (!m_DatosTerminal.IsTERMINAL_ASISTENCIANull() && m_DatosTerminal.TERMINAL_ASISTENCIA > 0)
                EsAsistencia = true;

            foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
            {

                try
                {


                    bool Agregar = false;
                    byte[] Huella1 = null;
                    byte[] Huella2 = null;
                    byte[] Huella3 = null;
                    int NoHuellas = 0;

                    if (Fila.IsPERSONAS_A_VEC_T1Null() || Fila.PERSONAS_A_VEC_T1.Trim().Length <= 0)
                    {
                        AgregaLog("Empleado " + Fila.PERSONA_LINK_ID + " - " + Fila.PERSONA_NOMBRE + " Sin Dato(PERSONAS_A_VEC_1)");
                        continue;
                    }
                    if (!Fila.IsPERSONAS_A_VEC_T2Null() && Fila.PERSONAS_A_VEC_T2 == "<BORRAR>")
                    {
                        if (Borrar(Fila.PERSONAS_A_VEC_T1))
                            ConfirmaEmpleado(Fila);
                        continue;
                    }
                    try
                    {
                        if (!Fila.IsPERSONAS_A_VEC_1Null() && Fila.PERSONAS_A_VEC_1.Length > 0)
                        {
                            Huella1 = Fila.PERSONAS_A_VEC_1;
                            NoHuellas++;
                        }
                    }
                    catch { }

                    try
                    {
                        if (!Fila.IsPERSONAS_A_VEC_2Null() && Fila.PERSONAS_A_VEC_2.Length > 0)
                        {
                            Huella2 = Fila.PERSONAS_A_VEC_2;
                            NoHuellas++;
                        }
                    }
                    catch { }

                    try
                    {
                        if (!Fila.IsPERSONAS_A_VEC_3Null() && Fila.PERSONAS_A_VEC_3.Length > 0)
                        {
                            Huella3 = Fila.PERSONAS_A_VEC_3;
                            NoHuellas++;
                        }
                    }
                    catch { }

                    if (Fila.IsPERSONAS_A_VEC_T2Null() || Fila.PERSONAS_A_VEC_T2.Trim().Length <= 0)
                        if (EsMerck && !EsAsistencia && NoHuellas < 1)
                        {
                            AgregaLog("Sin huella o Sin tarjeta " + Fila.PERSONA_LINK_ID + " - " + Fila.PERSONA_NOMBRE);
                        }

                    string Clave = "";
                    AgregaLog("Empleado " + Fila.PERSONA_LINK_ID + " - " + Fila.PERSONA_NOMBRE + " Preparando envío ");
                    string IDEmpleado = ObtenIDEmpleado(Fila);
                    AgregaLog("IDEmpleado " + IDEmpleado);
                    int iIDEmpleado = Convert.ToInt32(IDEmpleado);

                    bool PersonaSinHuella = false;
                    bool PersonaSupervisor = false;

                    //IDEmpleado = Fila.PERSONAS_A_VEC_T2;// Convert.ToInt32(Fila.PERSONA_LINK_ID).ToString();
                    /*if (IDEmpleado.Length < 8 || IDEmpleado.Length > 10)
                        continue;*/
                    string Nombre = Fila.PERSONA_NOMBRE;

                    bool Correcto = true;
                    if ((Tecno == Tecnologias.Huella) && (Fila.IsPERSONA_ID_S_HUELLANull() || Fila.PERSONA_ID_S_HUELLA == 0))
                        if (NoHuellas < 1)
                        {
                            AgregaLog("La persona " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE + " no tiene permitido ingresar sin huella, no se cargo");
                            ConfirmaError(Fila, " no tiene permitido ingresar sin huella, no se cargo");
                            continue;
                        }

                    if (!Fila.IsPERSONA_ID_S_HUELLANull() && Fila.PERSONA_ID_S_HUELLA != 0)
                    {
                        if (!Fila.IsPERSONA_S_HUELLA_CLAVENull() && Fila.PERSONA_S_HUELLA_CLAVE != "")
                            Clave = Fila.PERSONA_S_HUELLA_CLAVE.Replace(",", "");
                        else
                            if (!Fila.IsPERSONAS_A_VEC_2Null())
                                Clave = Fila.PERSONAS_A_VEC_T2;
                        if (Clave.IndexOf('*') >= 0)
                            PersonaSupervisor = true;
                        Clave = Clave.Replace("*", "");
                        if (Clave != "")
                            PersonaSinHuella = true;
                    }
                    //if (!PersonaSinHuella)
                    //    continue;
                    int Privilegio = 0;
                    if (PersonaSupervisor)
                        Privilegio = 3;

                    if (EsiClock)
                    {
                        if (!Fila.IsPERSONAS_A_VEC_T2Null() && Fila.PERSONAS_A_VEC_T2.Length > 0)
                        {
                            m_ZKActivex.SetStrCardNumber(Fila.PERSONAS_A_VEC_T2);
                        }
                        Correcto = m_ZKActivex.SSR_SetUserInfo(m_TConexion.NoTerminal, IDEmpleado, Nombre, Clave, Privilegio, true);
                        if (IDEmpleado != Fila.PERSONA_LINK_ID.ToString("0"))
                            Correcto = m_ZKActivex.SSR_SetUserInfo(m_TConexion.NoTerminal, Fila.PERSONA_LINK_ID.ToString("0"), Nombre, Clave, Privilegio, true);
                    }
                    else
                    {
                        m_ZKActivex.SetStrCardNumber(Fila.PERSONAS_A_VEC_T1);
                        Correcto = m_ZKActivex.SetUserInfo(m_TConexion.NoTerminal, iIDEmpleado, Nombre, Clave, Privilegio, true);
                    }

                    if (Tecno != Tecnologias.Huella && Tecno != Tecnologias.Rostro)
                        NoHuellas = 0;

                    if (NoHuellas == 0 && Correcto)
                    {
                        AgregaLog("Acceso sin Huella " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE + " / " + Clave);
                        byte Datos = 0;
                        if (EsiClock)
                            Correcto = m_ZKActivex.SetUserInfoEx(m_TConexion.NoTerminal, iIDEmpleado, 135, ref Datos);//PW o RF
                        else
                            Correcto = m_ZKActivex.SetUserInfoEx(m_TConexion.NoTerminal, iIDEmpleado, 7, ref Datos);//PW o RF
                        if (Correcto)
                            ConfirmaEmpleado(Fila);
                        else
                            ConfirmaError(Fila, "No se pudo asignar acceso sin huella");
                        continue;
                    }

                    byte[] Huella = new byte[384 * 2 * NoHuellas];

                    ///Parce puesto ya que reusan tarjetas y quieren que anque esten dadas de alta en el modulo sin huella
                    ///puedan seguir checando con huella si la tienen
                    if (EsMerck)
                        PersonaSinHuella = false;


                    if (NoHuellas > 0 && Correcto && !PersonaSinHuella)
                    {

                        AgregaLog("Preparando Huella " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                        BorrarVectores(IDEmpleado);
                        if (Huella3 != null)
                        {
                            Correcto = EnviaVectores(IDEmpleado, CeC_Vectores.Nuevo(Huella3));
                        }
                        else
                        {
                            if (Huella1 != null)
                            {
                                if (EsiClock)
                                    Correcto = m_ZKActivex.SSR_SetUserTmp(m_TConexion.NoTerminal, IDEmpleado, 0, ref Huella1[0]);
                                else
                                    Correcto = m_ZKActivex.SetUserTmp(m_TConexion.NoTerminal, iIDEmpleado, 0, ref Huella1[0]);
                            }

                            if (Huella2 != null)
                            {
                                if (EsiClock)
                                    Correcto = m_ZKActivex.SSR_SetUserTmp(m_TConexion.NoTerminal, IDEmpleado, 1, ref Huella2[0]);
                                else
                                    Correcto = m_ZKActivex.SetUserTmp(m_TConexion.NoTerminal, iIDEmpleado, 1, ref Huella2[0]);
                            }
                        }
                    }
                    else
                    {
                        BorrarVectores(IDEmpleado);
                    }

                    if (!Correcto)
                    {

                        System.IO.StringWriter Texto = new System.IO.StringWriter();
                        try
                        {
                            Fila.Table.WriteXml(Texto);
                        }
                        catch { }
                        AgregaError("No se pudo cargar la huella Persona = " + Fila.PERSONA_ID + " NoEmpleado = " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                        try
                        {

                            string NARchivo = CeLog2.S_NombreDestino + Terminal_ID.ToString() + " " + DateTime.Now.ToString("HHMM") + ".xml";
                            if (gNArchivo != NARchivo)
                            {
                                gNArchivo = NARchivo;
                                System.IO.File.WriteAllText(gNArchivo, Texto.ToString());
                            }
                        }
                        catch { }
                        Huella = null;
                        ConfirmaError(Fila, "Error al transferir");
                        ErroresGenerados++;
                    }
                    else
                    {
                        //Si es correcta la transmisión
                        ConfirmaEmpleado(Fila);
                    }
                }
                catch (System.Exception e)
                {
                    ErroresGenerados++;
                    AgregaError(e);
                }
            }
            /// No importa si se borran los empleados que no estan en la BD aunque el cargar
            /// sus registros no fuera posible
            //if (ErroresGenerados == 0)
            {

            }
            m_ZKActivex.EnableDevice(m_TConexion.NoTerminal, true);
            if (ErroresGenerados == 0)
                return Errores.Correcto;
            return Errores.Error_Desconocido;
        }
        private bool BorraUsuarios()
        {

            try
            {
                AgregaLog("Iniciando BorraUsuarios");
                if (m_ZKActivex.ReadAllUserID(m_TConexion.NoTerminal))
                {
                    int _errorCode = 1;
                    while (_errorCode != 0)
                    {
                        string NEmp = "";
                        int iNEmp = 0;
                        string Nombre = "";
                        string Password = "";
                        int Privilegio = 0;
                        bool Habilitado = false;
                        if (EsiClock)
                            m_ZKActivex.SSR_GetAllUserInfo(m_TConexion.NoTerminal, out NEmp, out Nombre, out Password, out Privilegio, out Habilitado);
                        else
                        {
                            m_ZKActivex.GetAllUserInfo(m_TConexion.NoTerminal, ref iNEmp, ref Nombre, ref Password, ref Privilegio, ref Habilitado);
                            NEmp = iNEmp.ToString();
                        }
                        m_ZKActivex.GetLastError(ref _errorCode);
                        bool Borrar = true;
                        foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
                        {
                            string IDEmpleado = ObtenIDEmpleado(Fila);

                            if (IDEmpleado == NEmp || Fila.PERSONA_LINK_ID.ToString() == NEmp)
                            {
                                Borrar = false;
                                break;
                            }
                        }

                        if (Borrar == true && NEmp != null)
                        {
                            if (EsiClock)
                            {
                                m_ZKActivex.SSR_DelUserTmp(m_TConexion.NoTerminal, NEmp, 0);
                                m_ZKActivex.SSR_DelUserTmp(m_TConexion.NoTerminal, NEmp, 1);
                                m_ZKActivex.SSR_DeleteEnrollDataExt(m_TConexion.NoTerminal, NEmp, 0);
                                m_ZKActivex.SSR_DeleteEnrollData(m_TConexion.NoTerminal, NEmp, 0);
                            }
                            else
                            {
                                m_ZKActivex.DelUserTmp(m_TConexion.NoTerminal, iNEmp, 0);
                                m_ZKActivex.DelUserTmp(m_TConexion.NoTerminal, iNEmp, 1);
                                m_ZKActivex.DeleteEnrollData(m_TConexion.NoTerminal, iNEmp, m_TConexion.NoTerminal, 0);
                                NEmp = iNEmp.ToString();
                            }
                            CeLog2.AgregaLog("Borrando " + NEmp);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);

            }
            return false;
        }

        private bool Borrar(string ID)
        {
            if (EsiClock)
            {
                m_ZKActivex.SSR_DelUserTmp(m_TConexion.NoTerminal, ID, 0);
                m_ZKActivex.SSR_DelUserTmp(m_TConexion.NoTerminal, ID, 1);
                m_ZKActivex.SSR_DeleteEnrollDataExt(m_TConexion.NoTerminal, ID, 0);
                m_ZKActivex.SSR_DeleteEnrollData(m_TConexion.NoTerminal, ID, 0);
            }
            else
            {
                int iID = Convert.ToInt32(ID);
                m_ZKActivex.DelUserTmp(m_TConexion.NoTerminal, iID, 0);
                m_ZKActivex.DelUserTmp(m_TConexion.NoTerminal, iID, 1);
                m_ZKActivex.DeleteEnrollData(m_TConexion.NoTerminal, iID, m_TConexion.NoTerminal, 0);

            }
            return true;
        }

        protected string ZKFPVersion()
        {
            try
            {
                string Firm = "";
                if (m_ZKActivex.GetSysOption(m_TConexion.NoTerminal, "~ZKFPVersion", out Firm))
                {
                    if (Firm == "10")
                        return "10";
                }

            }
            catch { }
            return "9";
        }
        private bool BorrarVectores(string NEmp)
        {
            int FingerIndex = 0;// de 0 a 9;
            int Flag = 0;

            for (FingerIndex = 0; FingerIndex < 10; FingerIndex++)
            {
                if (EsiClock)
                    m_ZKActivex.SSR_DelUserTmp(m_TConexion.NoTerminal, NEmp, FingerIndex);
                else
                    m_ZKActivex.DelUserTmp(m_TConexion.NoTerminal, Convert.ToInt32(NEmp), FingerIndex);
            }
            return true;
        }
        private bool EnviaVectores(string NEmp, CeC_Vectores Vectores)
        {
            bool Correcto = true;
            int Cont = 0;
            foreach (string Huella in Vectores.Huellas)
            {
                if (Huella == "")
                    continue;
                if (Vectores.HuellasOpciones.Count <= 0)
                {
                    int iNEmp = Convert.ToInt32(NEmp);
                    if (!m_ZKActivex.SetUserTmpStr(m_TConexion.NoTerminal, iNEmp, Cont, Huella))
                        Correcto = false;
                }
                else
                {
                    int Flag = CeC.Convierte2Int(Vectores.HuellasOpciones[Cont]);
                    if (!m_ZKActivex.SetUserTmpExStr(m_TConexion.NoTerminal, NEmp, Cont, Flag, Huella))
                        Correcto = false;
                    Cont++;
                }
                
            }
            return Correcto;
        }
        private CeC_Vectores ObtenVectores(string NEmp)
        {
            CeC_Vectores Vectores = new CeC_Vectores();
            byte[] Vector = new byte[2550];
            int LenVector = 0;
            bool Guardado = false;
            string sVector = "";

            int FingerIndex = 0;// de 0 a 9;
            int Flag = 0;
            int iNEmp = Convert.ToInt32(NEmp);
            for (FingerIndex = 0; FingerIndex < 10; FingerIndex++)
            {
                Guardado = m_ZKActivex.GetUserTmpStr(m_TConexion.NoTerminal, iNEmp, FingerIndex, ref sVector, ref LenVector);
                if (Guardado)
                    Vectores.Huellas.Add(sVector.Substring(0, LenVector));
                /*
                ///Se debe probar con diferentes versiones de firmware
                Guardado = m_ZKActivex.GetUserTmpExStr(m_TConexion.NoTerminal, NEmp, FingerIndex, out Flag, out sVector, out LenVector);
                if (Guardado)
                {
                    Vectores.Huellas.Add(sVector.Substring(0, LenVector));
                    Vectores.HuellasOpciones.Add(Flag.ToString());
                }*/
                /*else
                {
                    Vectores.Huellas.Add("");
                    Vectores.HuellasOpciones.Add("");
                }*/
            }
            return Vectores;
        }
        public override bool AbrePuerta(int TimeOut)
        {
            m_ZKActivex.ACUnlock(m_TConexion.NoTerminal, TimeOut);
            return true;
        }
        protected override Errores PoleoVectores()
        {
            try
            {
                string ZKFPVer = ZKFPVersion();
                if (m_ZKActivex.ReadAllUserID(m_TConexion.NoTerminal))
                {
                    m_ZKActivex.ReadAllTemplate(m_TConexion.NoTerminal);
                    int _errorCode = 1;
                    while (_errorCode != 0)
                    {
                        string NEmp = "";
                        int iNEmp = 0;
                        string Nombre = "";
                        string Password = "";
                        int Privilegio = 0;
                        bool Habilitado = false;
                        if (EsiClock)
                            m_ZKActivex.SSR_GetAllUserInfo(m_TConexion.NoTerminal, out NEmp, out Nombre, out Password, out Privilegio, out Habilitado);
                        else
                        {
                            m_ZKActivex.GetAllUserInfo(m_TConexion.NoTerminal, ref iNEmp, ref Nombre, ref Password, ref Privilegio, ref Habilitado);
                            NEmp = iNEmp.ToString();
                        }
                        m_ZKActivex.GetLastError(ref _errorCode);
                        if (NEmp != null)
                        {
                            /* byte[] Vector = new byte[2550];
                             int LenVector = 0;
                             bool Guardado = false;
                             string sVector = "";
                            
                             int FingerIndex = 0;// de 0 a 9;
                             int Flag = 0;
                             if (EsiClock)
                                 Guardado = m_ZKActivex.SSR_GetUserTmp(m_TConexion.NoTerminal, NEmp, 0, out Vector[0], out LenVector);
                             else
                             {
                                 for (FingerIndex = 0; FingerIndex < 10; FingerIndex++)
                                 {

                                     Guardado = m_ZKActivex.GetUserTmpExStr(m_TConexion.NoTerminal, NEmp, FingerIndex, out Flag, out sVector, out LenVector);
                                     if (Guardado)
                                         break;
                                 }
                             }
                             if (Guardado)
                                 AgregaHuella(NEmp, 0, ObtenArregloBytes(Vector, LenVector));

                             if (EsiClock)
                                 Guardado = m_ZKActivex.SSR_GetUserTmp(m_TConexion.NoTerminal, NEmp, 1, out Vector[0], out LenVector);
                             else
                             {
                                 Guardado = false;
                                 for (FingerIndex++; FingerIndex < 10; FingerIndex++)
                                 {
                                     Guardado = m_ZKActivex.GetUserTmp(m_TConexion.NoTerminal, iNEmp, FingerIndex, ref Vector[0], ref LenVector);
                                     if (Guardado)
                                         break;
                                 }


                             }
                             if (Guardado)
                                 AgregaHuella(NEmp, 1, ObtenArregloBytes(Vector, LenVector));*/

                            CeC_Vectores Vectores = ObtenVectores(NEmp);
                            if (Vectores.Huellas.Count > 0 || Vectores.IRIS.Count > 0 ||
                                Vectores.Rostros.Count > 0 || Vectores.Palmas.Count > 0)
                                AgregaHuella(NEmp, 3, Vectores.ObtenBytes());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
                return Errores.Error_Desconocido;
            }
            return Errores.Correcto;
        }

        public override bool IniciaEventos()
        {
            try
            {
                if (m_ZKActivex.RegEvent(m_TConexion.NoTerminal, 65535))
                {

                }
            }
            catch { }
            return true;
        }


        /*************************************************************************************************
* Before you refer to this demo,we strongly suggest you read the development manual deeply first.*
* This part is for demonstrating the RealTime Events that triggered  by your operations          *
**************************************************************************************************/
        #region RealTime Events

        //When you place your finger on sensor of the device,this event will be triggered
        private void m_ZKActivex_OnFinger()
        {
            AgregaLog("RTEvent OnFinger Has been Triggered");
        }
        private void m_ZKActivex_OnConnected()
        {
            AgregaLog("RTEvent onConnected Has been Triggered");
        }

        //After you have placed your finger on the sensor(or swipe your card to the device),this event will be triggered.
        //If you passes the verification,the returned value userid will be the user enrollnumber,or else the value will be -1;
        private void m_ZKActivex_OnVerify(int iUserID)
        {

            if (iUserID != -1)
            {
                AgregaLog("RTEvent OnVerify Has been Triggered,Verifying...\nVerified OK,the UserID is " + iUserID.ToString());
            }
            else
            {
                AgregaLog("RTEvent OnVerify Has been Triggered,Verifying...\nVerified Failed... ");
            }
        }

        //If your fingerprint(or your card) passes the verification,this event will be triggered
        private void m_ZKActivex_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            AgregaLog("RTEvent OnAttTrasactionEx Has been Triggered,Verified OK" + CeC.SaltoLinea +
            "...UserID:" + CeC.SaltoLinea +
            "...isInvalid:" + CeC.SaltoLinea +
            "...attState:" + iAttState.ToString() + CeC.SaltoLinea +
            "...VerifyMethod:" + iVerifyMethod.ToString() + CeC.SaltoLinea +
            "...Workcode:" + iWorkCode.ToString() + CeC.SaltoLinea +
            "...Time:" + iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());
            AgregaChecada(sEnrollNumber, new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond), ObtenTipoAcceso(iAttState), true);
        }

        //If your fingerprint(or your card) passes the verification,this event will be triggered
        private void m_ZKActivex_OnAttTransaction(int iEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond)
        {
            AgregaLog("RTEvent OnAttTrasaction Has been Triggered,Verified OK" + CeC.SaltoLinea +
            "...UserID:" + iEnrollNumber.ToString() + CeC.SaltoLinea +
            "...isInvalid:" + iIsInValid.ToString() + CeC.SaltoLinea +
            "...attState:" + iAttState.ToString() + CeC.SaltoLinea +
            "...VerifyMethod:" + iVerifyMethod.ToString() + CeC.SaltoLinea +
            "...Time:" + iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());
            /*TipoAccesos TA = TipoAccesos.Correcto;
            switch (iAttState)
            {
                case 0: TA = TipoAccesos.Correcto; break;
                case 1: TA = TipoAccesos.Salida; break;
                case 2: TA = TipoAccesos.Salida_a_Comer; break;
                case 3: TA = TipoAccesos.Regreso_de_comer; break;
                case 4: TA = TipoAccesos.Entrada; break;
                case 5: TA = TipoAccesos.Salida; break;
            }*/
            if (iMonth <= 0 || iDay <= 0)
                return;
            AgregaChecada(iEnrollNumber.ToString(), new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond), ObtenTipoAcceso(iAttState), true);

        }

        //When you have enrolled your finger,this event will be triggered and return the quality of the fingerprint you have enrolled
        private void m_ZKActivex_OnFingerFeature(int iScore)
        {
            if (iScore < 0)
            {
                AgregaLog("The quality of your fingerprint is poor");
            }
            else
            {
                AgregaLog("RTEvent OnFingerFeature Has been Triggered...Score:　" + iScore.ToString());
            }
        }

        //When you are enrolling your finger,this event will be triggered.
        private void m_ZKActivex_OnEnrollFinger(int iEnrollNumber, int iFingerIndex, int iActionResult, int iTemplateLength)
        {
            if (iActionResult == 0)
            {
                AgregaLog("RTEvent OnEnrollFiger Has been Triggered...." + CeC.SaltoLinea +
            ".....UserID: " + iEnrollNumber + " Index: " + iFingerIndex.ToString() + " tmpLen: " + iTemplateLength.ToString());
            }
            else
            {
                AgregaLog("RTEvent OnEnrollFiger was Triggered by Error");
            }
        }

        //When you press the keypad,this event will be triggered.
        private void m_ZKActivex_OnKeyPress(int iKey)
        {
            AgregaLog("RTEvent OnKeyPress Has been Triggered, Key: " + iKey.ToString());
        }

        //When you have deleted one one fingerprint template,this event will be triggered.
        private void m_ZKActivex_OnDeleteTemplate(int iEnrollNumber, int iFingerIndex)
        {
            AgregaLog("RTEvent OnDeleteTemplate Has been Triggered..." + CeC.SaltoLinea +
            "...UserID=" + iEnrollNumber.ToString() + " FingerIndex=" + iFingerIndex.ToString());
        }

        //When you have enrolled a new user,this event will be triggered.
        private void m_ZKActivex_OnNewUser(int iEnrollNumber)
        {
            AgregaLog("RTEvent OnNewUser Has been Triggered..." + CeC.SaltoLinea +
            "...NewUserID=" + iEnrollNumber.ToString());
        }

        //When you swipe a card to the device, this event will be triggered to show you the card number.
        private void m_ZKActivex_OnHIDNum(int iCardNumber)
        {
            AgregaLog("RTEvent OnHIDNum Has been Triggered..." + CeC.SaltoLinea +
            "...Cardnumber=" + iCardNumber.ToString());
            TarjetaNoIdentificada(iCardNumber.ToString());
        }

        //When the dismantling machine or duress alarm occurs, trigger this event.
        private void m_ZKActivex_OnAlarm(int iAlarmType, int iEnrollNumber, int iVerified)
        {
            AgregaLog("RTEvnet OnAlarm Has been Triggered..." + CeC.SaltoLinea +
            "...AlarmType=" + iAlarmType.ToString() + CeC.SaltoLinea +
            "...EnrollNumber=" + iEnrollNumber.ToString() + CeC.SaltoLinea +
            "...Verified=" + iVerified.ToString());
        }

        //Door sensor event
        private void m_ZKActivex_OnDoor(int iEventType)
        {
            AgregaLog("RTEvent Ondoor Has been Triggered..." + CeC.SaltoLinea +
            "...EventType=" + iEventType.ToString());
        }

        //When you have emptyed the Mifare card,this event will be triggered.
        private void m_ZKActivex_OnEmptyCard(int iActionResult)
        {

            if (iActionResult == 0)
            {
                AgregaLog("RTEvent OnEmptyCard Has been Triggered..." + CeC.SaltoLinea +
            "...Empty Mifare Card OK");
            }
            else
            {
                AgregaLog("RTEvent OnEmptyCard Has been Triggered..." + CeC.SaltoLinea +
            "...Empty Failed");
            }
        }

        //When you have written into the Mifare card ,this event will be triggered.
        private void m_ZKActivex_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength)
        {

            if (iActionResult == 0)
            {
                AgregaLog("RTEvent OnWriteCard Has been Triggered..." + CeC.SaltoLinea +
            "...Write Mifare Card OK" + CeC.SaltoLinea +
            "...EnrollNumber=" + iEnrollNumber.ToString() + CeC.SaltoLinea +
            "...TmpLength=" + iLength.ToString());
            }
            else
            {
                AgregaLog("RTEvent OnWriteCard Has been Triggered..." + CeC.SaltoLinea +
            "...Write Failed");
            }
        }



        #endregion

        public override bool ConsultaEstadoTerminal()
        {
            int Usuarios = 0;
            if (!m_ZKActivex.GetDeviceStatus(m_TConexion.NoTerminal, 2, ref Usuarios))
                return false;
            return true;
            if (m_ZKActivex.ReadRTLog(m_TConexion.NoTerminal))
            {
                while (m_ZKActivex.GetRTLog(m_TConexion.NoTerminal))
                {
                    Sleep(0);
                }
            }
            return true;
        }
    }
}
