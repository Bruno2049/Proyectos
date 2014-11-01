using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;

namespace eClockSync
{
    class CeBioEntryPlus : CeTerminalSync
    {
        /// <summary>
        /// Contiene el numero de logs nuevo
        /// </summary>
        protected int IUNOLOGSNuevo = 0;

        /// <summary>
        /// Contiene el numero de logs desde la ultima sincronizacion
        /// </summary>
        protected int IUNOLOGS
        {
            get { return ObtenParametro("IUNOLOGS", 0); }
            set { AsignaParametro("IUNOLOGS", value); }
        }

        /// <summary>
        /// Contiene el numero 1 o posiblemente el siteID de proximidad
        /// </summary>
        protected int BEP_cardCustomID
        {
            get { return ObtenParametro("BEP_cardCustomID", 0); }
            set { AsignaParametro("BEP_cardCustomID", value); }
        }

        int m_Handle = 0;
        static bool gIniciado = false;
        int m_NoLog = 0;
        public override bool Conecta()
        {
            try
            {
                if (!gIniciado)
                {
                    BSSDK_NS.BSSDK.BS_InitSDK();
                    gIniciado = true;
                }
                m_Conectado = false;
                IPAddress addr = IPAddress.Parse(m_TConexion.Direccion);

                int result = BSSDK_NS.BSSDK.BS_OpenSocket(addr.ToString(), m_TConexion.Puerto, ref m_Handle);
                if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
                {
                    AgregaError("No se pudo conectar con el dispositivo Error = " + result);
                    return false;
                }
                m_Conectado = true;

                int Pos = m_DatosTerminal.TERMINAL_NOMBRE.IndexOf("(") + 1;
                string ID = m_DatosTerminal.TERMINAL_NOMBRE.Substring(Pos, m_DatosTerminal.TERMINAL_NOMBRE.IndexOf(")") - Pos);

                result = BSSDK_NS.BSSDK.BS_SetDeviceID(m_Handle,
                    Convert.ToUInt32(ID), BSSDK_NS.BSSDK.BS_DEVICE_BEPLUS);
                if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
                {
                    AgregaError("No se pudo asignar el ID del dispositivo Error = " + result);

                }
                return true;
            }
            catch
            {

                Desconecta();
            }


            return false;
        }

        public override bool Desconecta()
        {
            if (!m_Conectado)
                return true;
            int R = BSSDK_NS.BSSDK.BS_CloseSocket(m_Handle);
            if (R == BSSDK_NS.BSSDK.BS_SUCCESS)
                return true;
            AgregaError("No se pudo desconectar Error = " + R);
            return false;
        }

        public bool AgregaLogBSLogRecordBorrado(BSSDK_NS.BSSDK.BSLogRecord Registro)
        {
            DateTime eventTime = new DateTime(1970, 1, 1);
            try
            {
                eventTime = eventTime.AddSeconds(Registro.eventTime);
            }
            catch{}

            CeLog2.AgregaLog("BSLogRecord Borrado userID = " + Registro.userID.ToString() + ", tnaEvent = " + Registro.tnaEvent.ToString() +
                "subEvent = " + Registro.subEvent.ToString() + ", reserved = " + Registro.reserved.ToString() +
                "eventType = " + Registro.eventType.ToString() + ", eventTime = " + Registro.eventTime.ToString() +
                "eventTime = " + eventTime.ToLongTimeString()
                 );

            return true;
        }
        public override Errores PoleoChecadas()
        {
            //int IUNOLOGSNuevo = 0;
           int result = BSSDK_NS.BSSDK.BS_GetLogCount(m_Handle, ref IUNOLOGSNuevo);
            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                return Errores.Error_Conexion;
            }

            if (IUNOLOGSNuevo == IUNOLOGS)
                return Errores.No_Requiere;

            BSSDK_NS.BSSDK.BSLogRecord[] Log = null;
            int R = BSSDK_NS.BSSDK.BS_ReadLogBEP(m_Handle, out Log);
            
            if (R != BSSDK_NS.BSSDK.BS_SUCCESS)
                return Errores.Error_Desconocido;
            m_NoLog = Log.Length;
            if (Log == null || Log.Length < 1)
                return Errores.No_Requiere;
            foreach (BSSDK_NS.BSSDK.BSLogRecord Registro in Log)
            {
                try
                {
                    TipoAccesos TA = TipoAccesos.Incorrecto;
                    bool Agregar = false;
                    if (Registro.eventType == BSSDK_NS.BSSDK.BE_EVENT_VERIFY_BAD_FINGER
                        || Registro.eventType == BSSDK_NS.BSSDK.BE_EVENT_VERIFY_CANCELED
                        || Registro.eventType == BSSDK_NS.BSSDK.BE_EVENT_VERIFY_FAIL)
                    {
                        Agregar = true;
                    }

                    if (Registro.eventType == BSSDK_NS.BSSDK.BE_EVENT_VERIFY_SUCCESS
                        || Registro.eventType == BSSDK_NS.BSSDK.BE_EVENT_VERIFY_NO_FINGER
                        || Registro.eventType == BSSDK_NS.BSSDK.BE_EVENT_IDENTIFY_SUCCESS)
                    {
                        Agregar = true;
                        TA = TipoAccesos.Correcto;
                    }
                    AgregaLogBSLogRecordBorrado(Registro);
                    if (Agregar)
                    {
                        DateTime eventTime = new DateTime(1970, 1, 1).AddSeconds(Registro.eventTime);
                        AgregaChecada(Registro.userID.ToString(), eventTime, TA);
                    }

                        
                }
                catch (Exception ex)
                {
                    AgregaError(ex);
                    try
                    {
                        DateTime eventTime = new DateTime(1970, 1, 1).AddSeconds(Registro.eventTime);

                        AgregaError("userID = " + Registro.userID.ToString() + ", eventTime = " + eventTime.ToString() + ", eventType = " + Registro.eventType);
                    }
                    catch (Exception ext) { AgregaError(ex); }

                }
            }
            return Errores.Correcto;
        }

        override protected Errores AsignaFechaHora(DateTime FechaHora)
        {
            DateTime FH = BSSDK_NS.BSSDK.BS_GetTime(m_Handle);
            TimeSpan TS = FechaHora - FH;
            if (TS.TotalSeconds < 40)
                return Errores.No_Requiere;
            AgregaLog("Actualizando FechaHora de " + FH.ToString());
            int R = BSSDK_NS.BSSDK.BS_SetTime(m_Handle, FechaHora);
            if (R == BSSDK_NS.BSSDK.BS_SUCCESS)
                return Errores.Correcto;
            AgregaError("No se pudo AsignaFechaHora Error = " + R);
            return Errores.Error_Desconocido;
        }
        static string gNArchivo = "";
        protected override Errores EnvioVectores()
        {
            byte cardCustomID = Convert.ToByte(BEP_cardCustomID);
            if (cardCustomID == 0)
            {
                cardCustomID = 1;
                BEP_cardCustomID = 1;
            }
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
            foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
            {
                try
                {
                    
                    bool Agregar = false;
                    byte[] Huella1 = null;
                    byte[] Huella2 = null;
                    int NoHuellas = 0;

                    try
                    {
                        if (!Fila.IsPERSONAS_A_VEC_T1Null() && Fila.PERSONAS_A_VEC_1.Length > 0)
                        {
                            Huella1 = Fila.PERSONAS_A_VEC_1;
                            if (Huella1.Length != 384 * 2)
                                Huella1 = null;
                            else
                                NoHuellas++;
                        }
                    }
                    catch {}

                    try
                    {
                        if (!Fila.IsPERSONAS_A_VEC_T2Null() && Fila.PERSONAS_A_VEC_2.Length > 0)
                        {
                            Huella2 = Fila.PERSONAS_A_VEC_2;
                            if (Huella2.Length != 384 * 2)
                                Huella2 = null;
                            else
                                NoHuellas++;
                        }
                    }
                    catch  {                    }
                    if (NoHuellas == 0 && Fila.PERSONA_ID_S_HUELLA == 0)
                        continue;
                    BSSDK_NS.BSSDK.BEUserHdr Usuario = new BSSDK_NS.BSSDK.BEUserHdr();
                    Usuario.userID = Convert.ToUInt32(Fila.PERSONA_LINK_ID);

                    Usuario.startTime = 0x0;  //no start time check
                    Usuario.expiryTime = 0x0;
                    Usuario.securityLevel = 0;
                    Usuario.adminLevel = 1;
                    Usuario.startTime = 0x0;  //no start time check
                    Usuario.accessGroupMask = 0xFFFFFFFF;
                    Usuario.isDuress = new byte[2];
                    Usuario.isDuress[0] = 0;
                    Usuario.isDuress[1] = 0;
                    try
                    {
                        Usuario.cardID = Convert.ToUInt32(Fila.PERSONAS_A_VEC_T2);
                        Usuario.cardFlag = 0;
                        Usuario.cardVersion = 19;
                        Usuario.cardCustomID = cardCustomID;
                        if (Fila.PERSONA_ID_S_HUELLA > 0)
                            Usuario.cardFlag = 1;// Solo Tarjeta
                    }
                    catch { }
                    Usuario.reserved2 = new int[21];

                    byte[] Huella = new byte[384 * 2 * NoHuellas];
                    if (NoHuellas > 0)
                    {
                        AgregaLog("Preparando Huella " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                        if (Huella1 != null)
                            Array.Copy(Huella1, Huella, 384 * 2);
                        if (Huella2 != null)
                            Array.Copy(Huella2, 0, Huella, 384 * 2 * (NoHuellas - 1), 384 * 2);

                        Usuario.numOfFinger = Convert.ToUInt16(NoHuellas);

                        Usuario.fingerChecksum = new ushort[2];
                        for (int Cont = 0; Cont < 384 * 2; Cont++)
                        {
                            Usuario.fingerChecksum[0] += Huella[Cont];
                        }
                        if (NoHuellas > 1)
                        {
                            for (int Cont = 384 * 2; Cont < Huella.Length; Cont++)
                            {
                                Usuario.fingerChecksum[1] += Huella[Cont];
                            }
                        }
                        else
                            Usuario.fingerChecksum[1] = 0;


                    }
                    else
                    {
                        Usuario.numOfFinger = 0;
                        Usuario.fingerChecksum = new ushort[2];
                        Usuario.fingerChecksum[0] = 0;
                        Usuario.fingerChecksum[1] = 0;
                        //Huella = null;
                    }
                    int result = BSSDK_NS.BSSDK.BS_EnrollUserBEPlus(m_Handle, Usuario, Huella);
                    if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
                    {
                        System.IO.StringWriter Texto = new System.IO.StringWriter();
                        try
                        {
                            Fila.Table.WriteXml(Texto);
                        }
                        catch { }
                        AgregaError("No se pudo cargar la huella Persona = " + Fila.PERSONA_ID + " NoEmpleado = "+ Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                        try
                        {
                            
                            string NARchivo = CeLog2.S_NombreDestino + Terminal_ID.ToString() + " " + DateTime.Now.ToString("HHMM") + ".xml";
                            if(gNArchivo != NARchivo)
                            {
                                gNArchivo = NARchivo;
                                System.IO.File.WriteAllText(gNArchivo, Texto.ToString());
                            }
                        }
                        catch { }
                        Huella = null;
                        ErroresGenerados++;
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
                BorraUsuarios();
            }

            if (ErroresGenerados == 0)
                return Errores.Correcto;
            return Errores.Error_Desconocido;
        }
        private bool BorraUsuarios()
        {

            try
            {
                BSSDK_NS.BSSDK.BEUserHdr[] usersHdr = null;
                int NoUsuarios = 0;
                if (BSSDK_NS.BSSDK.BS_GetAllUserInfoBEPlus(m_Handle, out usersHdr, ref NoUsuarios) == BSSDK_NS.BSSDK.BS_SUCCESS)
                {
                    foreach (BSSDK_NS.BSSDK.BEUserHdr UserHdr in usersHdr)
                    {
                        bool Borrar = true;
                        foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
                        {
                            if (Fila.PERSONA_LINK_ID == UserHdr.userID)
                            {
                                Borrar = false;
                                break;
                            }
                        }

                        if (Borrar == true)
                        {
                            BSSDK_NS.BSSDK.BS_DeleteUser(m_Handle, UserHdr.userID);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);

            }
            return false;
        }
        protected override Errores PoleoVectores()
        {
            return Errores.No_Requiere_Cambio_En_Vectores;
        }
        protected override Errores BorraChecadas()
        {
            //Como ya se enviaron los logs guarda en variable los ultimos logs
            IUNOLOGS = IUNOLOGSNuevo;

            if (SePuedeSincincronizarListas())
            {
                int NumOfLog = 0;
                int result = BSSDK_NS.BSSDK.BS_GetLogCount(m_Handle, ref NumOfLog);
                if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
                {
                    AgregaLog("No se borro log");

                    return Errores.Error_Desconocido;
                }
                if (NumOfLog != m_NoLog)
                {
                    AgregaError("No fueron borrados debido a que no coinciden los logs descargados con los actuales");
                    return Errores.Correcto;
                }
#if BEP_PARCHE
                const int NUMERO_MAX_LOG = 8192 * 2;
                const int NUMERO_BORRAR_LOG = 8192;
                if (NumOfLog < NUMERO_MAX_LOG)
                {
                    return Errores.Correcto;
                }
                {
                    int Borrados = 0;
                    result = BSSDK_NS.BSSDK.BS_DeleteLog(m_Handle, NUMERO_BORRAR_LOG, ref Borrados);
                    return Errores.Correcto;
                }
#endif
                if (NumOfLog == m_NoLog)
                {
                    int Borrados = 0;
                    result = BSSDK_NS.BSSDK.BS_DeleteAllLog(m_Handle, NumOfLog, ref Borrados);
                    AgregaLog("BorraChecadas ");

                }

            }

            return Errores.Correcto;
        }
    }
}
