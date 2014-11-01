using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;

namespace eClockSync
{
    class CeHp : CeTerminalSync
    {
        #region Enumeraciones
        public enum RSI_RESULT
        {
            RSI_SUCCESS,			// 0
            RSI_ERROR_DATA,			// 1
            RSI_ERROR_COM,			// 2
            RSI_NO_CONNECTION,		// 3,  added by Prasanth Pulavarthi
            RSI_COM_TIMEOUT,		// 4,  added by Prasanth Pulavarthi
            RSI_BAD_CRC,			// 5,  added by Prasanth Pulavarthi
            RSI_PARSE_OVERFLOW,		// 6,  added by Prasanth Pulavarthi
            RSI_PARSE_UNDERFLOW,	// 7,  added by Prasanth Pulavarthi
            RSI_INVALID_CHANNEL,	// 8
            RSI_INVALID_SOCKET,		// 9,  added by JHT
            RSI_INVALID_BUFFER,		// 10, added by JHT
            RSI_SOCKET_ERROR,		// 11, added by JHT
            RSI_CANCEL_IO,			// 12
            RSI_FOPEN_ERROR,		// 13
            RSI_INVALID_FKEY_FILE,	// 14
            RSI_WS_NOTREADY,		// 15
            RSI_WS_NOTUSABLE,		// 16
        };

        public enum RSI_DATA_LOG_FORMAT
        {
            RSI_DLF_BUFFER_EMPTY = 0,
            RSI_DLF_USER_ENROLLED = 1,
            RSI_DLF_NO_HAND_READ = 2,
            RSI_DLF_IDENTITY_UNKNOWN = 3,
            RSI_DLF_EXIT_GRANTED = 4,
            RSI_DLF_SCORE_IS = 5,
            RSI_DLF_ACCESS_DENIED = 6,
            RSI_DLF_IDENTITY_VERIFIED = 7,
            RSI_DLF_USER_REMOVED = 8,
            RSI_DLF_ENTER_COMMAND_MODE = 9,
            RSI_DLF_LEAVE_COMMAND_MODE = 10,
            RSI_DLF_RECALIBRATED = 11,
            RSI_DLF_TWO_MAN_TIMEOUT = 12,
            RSI_DLF_DOOR_FORCED_OPEN = 13,
            RSI_DLF_TAMPER_ACTIVATED = 14,
            RSI_DLF_SUPERVISOR_OVERRIDE = 15,
            RSI_DLF_USER_ADDED_FROM_CARD = 16,
            RSI_DLF_AUX_INPUT_ON = 17,
            RSI_DLF_REQ_EXIT_ACTIVATED = 18,
            RSI_DLF_AUX_OUTPUT_ON = 19,
            RSI_DLF_BAUD_RATE_CHANGED = 20,
            RSI_DLF_MESSAGES_READ = 21,
            RSI_DLF_UNIT_ADDRESS_CHANGED = 22,
            RSI_DLF_SITE_CODE_CHANGED = 23,
            RSI_DLF_TIME_AND_DATE_SET = 24,
            RSI_DLF_LOCK_SETUP_CHANGED = 25,
            RSI_DLF_PASSWORDS_CHANGED = 26,
            RSI_DLF_REJECT_THRESHOLD_SET = 27,
            RSI_DLF_AUX_OUT_SETUP_CHANGED = 28,
            RSI_DLF_PRINTER_SETUP_CHANGED = 29,
            RSI_DLF_EXT_DATALOGS = 30,
            RSI_DLF_DOOR_OPEN_TOO_LONG = 31,
            RSI_DLF_USERS_LISTED = 32,
            RSI_DLF_DATA_BASE_SAVED = 33,
            RSI_DLF_DATA_BASE_RESTORED = 34,
            RSI_DLF_AMNESTY_PUNCH_GRANTED = 35,
            RSI_DLF_REJ_OVERRIDE_CHANGED = 36,
            RSI_DLF_AUTH_LEVEL_CHANGED = 37,
            RSI_DLF_OPER_MODE_CHANGED = 38,
            RSI_DLF_OUTPUT_MODE_CHANGED = 39,
            RSI_DLF_MAX_ID_LEN_CHANGED = 40,
            RSI_DLF_MEMORY_CLEARED = 41,
            RSI_DLF_ACCESS_REFUSED_TZ = 42,
            RSI_DLF_TIME_ZONES_CHANGED = 43,
            RSI_DLF_USER_TIME_ZONE_CHANGED = 44,
            RSI_DLF_DURESS_ALARM = 45,
            RSI_DLF_LOCK_OUTPUT_ON = 46,
            RSI_DLF_LOCK_OUTPUT_OFF = 47,
            RSI_DLF_AUX_OUTPUT_OFF = 48,
            RSI_DLF_SPECIAL_ENROLLMENT = 49,
            RSI_DLF_AUX_UNLOCK_WIEGAND = 50,
            RSI_DLF_TIME_RESTRICTIONS = 51,
        }	;

        public struct RSI_STATUS
        {
            public bool h_read; 			// byte 1	bit 0	SYS_STAT0
            public bool led1;				// byte 1 	bit 1
            public bool led2;				// byte 1 	bit 2
            public bool led3;				// byte 1 	bit 3
            public bool led4;				// byte 1 	bit 4
            public bool any_key;			// byte 1 	bit 5
            public bool aux_out1;			// byte 1   bit 6
            public bool aux_out2;			// byte 1   bit 7
            public bool res_sys;			// byte 2 	bit 0	SYS_STAT1
            public bool verify_rdy;		// byte 2 	bit 1
            public bool rslts_rdy;			// byte 2 	bit 2
            public bool failed_cmd;		// byte 2 	bit 3
            public bool dlog_rdy;			// byte 2 	bit 4
            public bool id_nim;			// byte 2 	bit 5
            public bool cmd_bsy;			// byte 2 	bit 6
            public bool kp_id;				// byte 2 	bit 7
            public bool tmpr_st;			// byte 3	bit 0	MON_STAT
            public bool aux_in2;			// byte 3   bit 1
            public bool aux_st;			// byte 3	bit 3
            public bool aux_in1;			// byte 3	bit 3	// new readers
            public bool door_st;			// byte 3	bit 2
            public bool rex_st;			// byte 3	bit 4
            public bool lock_st;			// byte 3	bit 7
            public bool auxo_st;			// byte 3	bit 6
        }	;

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct RSI_DATA_LOG_DATA_1
        {
            public byte[] data;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct RSI_DATA_LOG_DATA_2
        {
            public byte[] data;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct RSI_DATA_LOG_ELEMENT
        {

            public byte addr;
            public RSI_TIME_DATE time;
            public byte pad1;
            public RSI_DATA_LOG_FORMAT format;
            public DATOS data1;
            public byte pad2;
            public byte pad3;
            public DATOS data2;

        }	;

        // Structure for time_date.
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct RSI_TIME_DATE
        {
            [MarshalAs(UnmanagedType.U1)]
            public byte second; // 0 - 59
            [MarshalAs(UnmanagedType.U1)]
            public byte minute; // 0 - 59
            [MarshalAs(UnmanagedType.U1)]
            public byte hour; // 0 - 23 
            [MarshalAs(UnmanagedType.U1)]
            public byte day; // 1 - 31
            [MarshalAs(UnmanagedType.U1)]
            public byte month; // 1 - 12
            [MarshalAs(UnmanagedType.U1)]
            public byte year; // 0 - 99
        }	;

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct RSI_USER_RECORD
        {
            [MarshalAs(UnmanagedType.U1)]
            public byte B1;
            [MarshalAs(UnmanagedType.U1)]
            public byte B2;
            [MarshalAs(UnmanagedType.U1)]
            public byte B3;
            [MarshalAs(UnmanagedType.U1)]
            public byte B4;
            [MarshalAs(UnmanagedType.U1)]
            public byte B5;
            [MarshalAs(UnmanagedType.U1)]
            public byte B6;
            [MarshalAs(UnmanagedType.U1)]
            public byte B7;
            [MarshalAs(UnmanagedType.U1)]
            public byte B8;
            [MarshalAs(UnmanagedType.U1)]
            public byte B9;
            [MarshalAs(UnmanagedType.U1)]
            public byte B10;
            [MarshalAs(UnmanagedType.U1)]
            public byte B11;
            [MarshalAs(UnmanagedType.U1)]
            public byte B12;
            [MarshalAs(UnmanagedType.U1)]
            public byte B13;
            [MarshalAs(UnmanagedType.U1)]
            public byte B14;
            [MarshalAs(UnmanagedType.U1)]
            public byte B15;
            [MarshalAs(UnmanagedType.U1)]
            public byte B16;
        }	;

        class RSI_USER_RECORD_CS
        {
            private RSI_USER_RECORD UR;
            public RSI_USER_RECORD_CS()
            {

            }
            public RSI_USER_RECORD_CS(RSI_USER_RECORD UserRecord)
            {
                UR = UserRecord;
            }
            public static explicit operator RSI_USER_RECORD_CS(RSI_USER_RECORD UserRecord)
            {

                RSI_USER_RECORD_CS retValue = new RSI_USER_RECORD_CS(UserRecord);



                return retValue;

            }
            public long ID
            {
                get
                {
                    if (UR.B1 == 255)
                        return 0;
                    byte[] bID = new byte[5];
                    bID[0] = UR.B1;
                    bID[1] = UR.B2;
                    bID[2] = UR.B3;
                    bID[3] = UR.B4;
                    bID[4] = UR.B5;
                    return Convert.ToInt64(Bcd2String(bID, 0, 5));
                }
                set
                {
                    string sID = value.ToString("0000000000");
                    if (sID.Length > 10)
                    {
                        sID = sID.Substring(sID.Length - 10);
                    }
                    byte[] bID = String2Bcd(sID);
                    UR.B1 = bID[0];
                    UR.B2 = bID[1];
                    UR.B3 = bID[2];
                    UR.B4 = bID[3];
                    UR.B5 = bID[4];
                }
            }
            public byte[] VectorCompleto
            {
                get
                {
                    byte[] Vec = new byte[11];
                    byte[] bVector = Template;
                    bVector.CopyTo(Vec, 0);
                    Vec[9] = UR.B15;
                    Vec[10] = UR.B16;
                    return Vec;
                }
                set
                {
                    if (value == null)
                        Template = null;
                    else
                    {
                        byte[] bVector = new byte[9];
                        value.CopyTo(bVector, 0);

                        Template = bVector;

                    }
                    if (value != null && value.Length == 11)
                    {
                        UR.B15 = value[9];
                        UR.B16 = value[10];
                    }
                }
            }
            public byte[] Template
            {
                get
                {
                    byte[] bVector = new byte[9];
                    bVector[0] = UR.B6;
                    bVector[1] = UR.B7;
                    bVector[2] = UR.B8;
                    bVector[3] = UR.B9;
                    bVector[4] = UR.B10;
                    bVector[5] = UR.B11;
                    bVector[6] = UR.B12;
                    bVector[7] = UR.B13;
                    bVector[8] = UR.B14;
                    return bVector;
                }
                set
                {
                    if (value == null || value.Length != 9)
                    {
                        UR.B6 = 255;
                        UR.B7 = 255;
                        UR.B8 = 255;
                        UR.B9 = 255;
                        UR.B10 = 255;
                        UR.B11 = 255;
                        UR.B12 = 255;
                        UR.B13 = 255;
                        UR.B14 = 255;
                        return;
                    }
                    UR.B6 = value[0];
                    UR.B7 = value[1];
                    UR.B8 = value[2];
                    UR.B9 = value[3];
                    UR.B10 = value[4];
                    UR.B11 = value[5];
                    UR.B12 = value[6];
                    UR.B13 = value[7];
                    UR.B14 = value[8];
                }
            }

        }
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct DATOS
        {
            [MarshalAs(UnmanagedType.U1)]
            public byte DATO1; // 0 - 59
            [MarshalAs(UnmanagedType.U1)]
            public byte DATO2; // 0 - 59
            [MarshalAs(UnmanagedType.U1)]
            public byte DATO3; // 0 - 23 
            [MarshalAs(UnmanagedType.U1)]
            public byte DATO4; // 1 - 31
            [MarshalAs(UnmanagedType.U1)]
            public byte DATO5; // 1 - 12
        }	;

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct DATOSVECTORES
        {
            [MarshalAs(UnmanagedType.U1)]
            public byte[] DATO; // 0 - 59
        };

        const int RSI_LEN_DATA_BANK = 4096;
        public class RSI_DATA_BANK
        {
            public byte[] data;
            public RSI_DATA_BANK()
            {
                data = new byte[RSI_LEN_DATA_BANK];
            }
        }	;

        #endregion

        #region CeHpDll.dll
        [DllImport("CeHpDll.dll")]
        public static extern int _rsiGetDataLogElement
    (
        int Canal,
        int Previo
    );
        [DllImport("CeHpDll.dll")]
        public static extern RSI_DATA_LOG_FORMAT _RSI_DATA_LOG_FORMAT
    (
    );
        [DllImport("CeHpDll.dll")]
        public static extern RSI_TIME_DATE _RSI_TIME_DATE
    (
    );
        [DllImport("CeHpDll.dll")]
        public static extern DATOS _RSI_DATA_LOG_DATA_1
    (
    );
        [DllImport("CeHpDll.dll")]
        public static extern DATOS _RSI_DATA_LOG_DATA_2
    (
    );
        [DllImport("CeHpDll.dll")]
        public static extern int Canal
    (
        int Canal,
        int Previo
    );
        [DllImport("CeHpDll.dll")]
        public static extern int DescargaUsuarios
            (
            int Canal
            );


        [DllImport("CeHpDll.dll")]
        public static extern int LimpiaUsuarios();

        [DllImport("CeHpDll.dll")]
        public static extern int PreAgregaUsuario(
            RSI_USER_RECORD Usuario
            );

        [DllImport("CeHpDll.dll")]
        public static extern int EnviaUsuarios(
            int Canal
            );

        [DllImport("CeHpDll.dll")]
        public static extern RSI_USER_RECORD ObtenUsuarioMem
            (
            int PosUsuario
            );

        #endregion

        #region RSIDLL32.dll
        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiStartupWsock
            (
            );

        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiCleanupWsock
            (
            );

        [DllImport("RSIDLL32.dll")]
        public static extern int rsiInitWsock
            (
                Byte[] HostName,
                int Puerto
            );

        public static int rsiInitWsock
        (
            string HostName,
            int Puerto
        )
        {
            return rsiInitWsock(ObtenArregloBytes(HostName), Puerto);
        }
        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiCloseWsock
            (
                int Canal
            );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PuertoSerial">Desde 1</param>
        /// <returns>Canal</returns>
        [DllImport("RSIDLL32.dll")]
        public static extern int rsiInstallChannel
            (
                int PuertoSerial
            );
        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiCloseChannel
            (
                int Canal
            );


        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiResume
            (
                int Canal
            );

        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiOpenDirectChannel
            (
                int Canal,
                int baud
            );

        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiSetHandReader
            (
                int Canal,
                Byte IDTerminal,
            ref RSI_STATUS Estatus
            );

        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiEnterIdleMode
            (
                int Canal
            );

        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiGetStatus
            (
                int Canal,
                ref RSI_STATUS Estatus
            );

        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiGetDataLogElement
            (
                int Canal,
                [Out, MarshalAs(UnmanagedType.LPStruct)] RSI_DATA_LOG_ELEMENT DataLog,
            int Previo
            );


        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiHereIsTime
            (
                int Canal,
                ref RSI_TIME_DATE time
            );

        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiGetDataBank
            (
                int Canal,
                byte Banco,
            ref RSI_DATA_BANK dbnk
            );
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct RSI_DATA_BANK2
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4096)]
            public byte[] data;

        }	;



        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiSetDataBank
            (
                int Canal,
                byte Banco,
            ref RSI_DATA_BANK dbnk
            );

        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiSetDataBank
            (
                int Canal,
                byte Banco,
            ref RSI_DATA_BANK2 dbnk
            );

        #endregion

        int m_Canal = 0;
        RSI_STATUS m_Status = new RSI_STATUS();
        public override bool Conecta()
        {
            m_Conectado = false;
            int R = 0;
            RSI_RESULT RR = RSI_RESULT.RSI_SUCCESS;
            switch (m_TConexion.TipoConexion)
            {
                case CeC_Terminales.tipo.Red:

                    RR = rsiStartupWsock();
                    if (RR == RSI_RESULT.RSI_SUCCESS)
                    {
                        m_Canal = rsiInitWsock(m_TConexion.Direccion, m_TConexion.Puerto);
                        if (m_Canal == 0)
                        {
                            AgregaError("Error en rsiInitWsock");
                            return false;
                        }
                    }
                    else
                        return false;
                    break;
                case CeC_Terminales.tipo.Serial:
                    m_Canal = rsiInstallChannel(m_TConexion.Puerto - 1); //Com1 = 0
                    if (m_Canal == 0)
                    {
                        AgregaError("Error en rsiInstallChannel puerto " + m_TConexion.Puerto + " -1");
                        return false;
                    }
                    RR = rsiOpenDirectChannel(m_Canal, m_TConexion.Velocidad);
                    if (((RSI_RESULT)RR) != RSI_RESULT.RSI_SUCCESS)
                    {
                        AgregaError("Error en rsiOpenDirectChannel Velocidad " + m_TConexion.Velocidad);
                        return false;
                    }

                    break;
                case CeC_Terminales.tipo.Modem:
                    return false;
                    break;
                case CeC_Terminales.tipo.USB:
                case CeC_Terminales.tipo.RS485:
                    return false;
                    break;

            }

            if (rsiSetHandReader(m_Canal, Convert.ToByte(m_TConexion.NoTerminal), ref m_Status) != RSI_RESULT.RSI_SUCCESS)
            {
                AgregaError("Error en rsiSetHandReader NoTerminal " + m_TConexion.NoTerminal);
                rsiResume(m_Canal);
                return false;
            }
            m_Conectado = true;
            return true;
        }


        public override bool Desconecta()
        {
            if (m_Canal <= 0)
                return false;
            rsiResume(m_Canal);
            switch (m_TConexion.TipoConexion)
            {
                case CeC_Terminales.tipo.Red:
                    rsiCloseWsock(m_Canal);
                    rsiCleanupWsock();
                    break;
                case CeC_Terminales.tipo.Serial:
                    rsiCloseChannel(m_Canal);
                    break;
            }
            m_Canal = 0;
            return m_Conectado = false;
        }

        public override Errores PoleoChecadas()
        {
            try
            {
                RSI_RESULT rsiResult;
                Sleep(100);
                rsiResult = rsiGetStatus(m_Canal, ref m_Status);
                if (rsiResult != RSI_RESULT.RSI_SUCCESS)
                {
                    return Errores.Error_IO;
                }
                RSI_DATA_LOG_ELEMENT Dl = new RSI_DATA_LOG_ELEMENT();

                Sleep(1);
                int Reintentos = 0;
                do
                {
                    byte[] Datos = new byte[100];
                    int C = Canal(m_Canal, 1);
                    rsiResult = (RSI_RESULT)_rsiGetDataLogElement(m_Canal, 1);
                    for (Reintentos = 0; Reintentos <= 3; Reintentos++)
                        if (rsiResult == RSI_RESULT.RSI_SUCCESS)
                            break;
                        else
                        {
                            rsiResult = (RSI_RESULT)_rsiGetDataLogElement(m_Canal, 1);
                            if (rsiResult != RSI_RESULT.RSI_SUCCESS && Reintentos == 3)
                                return Errores.Error_Conexion;
                        }

                    RSI_DATA_LOG_FORMAT Formato = _RSI_DATA_LOG_FORMAT();
                    Dl.format = Formato;
                    Dl.time = _RSI_TIME_DATE();
                    Dl.data1 = _RSI_DATA_LOG_DATA_1();
                    Dl.data2 = _RSI_DATA_LOG_DATA_2();
                    //                _RSI_DATA_LOG_DATA_1();
                    if (Formato == RSI_DATA_LOG_FORMAT.RSI_DLF_IDENTITY_VERIFIED || Dl.format == RSI_DATA_LOG_FORMAT.RSI_DLF_IDENTITY_UNKNOWN)
                    {
                        //parche
                        //AgregaChecada(Dl);
                        if (!AgregaChecada(Dl))
                            return Errores.Error_Conexion;

                    }
                    rsiResult = (RSI_RESULT)_rsiGetDataLogElement(m_Canal, 0);
                    for (Reintentos = 0; Reintentos <= 3; Reintentos++)
                        if (rsiResult == RSI_RESULT.RSI_SUCCESS)
                            break;
                        else
                        {
                            rsiResult = (RSI_RESULT)_rsiGetDataLogElement(m_Canal, 0);
                            if (rsiResult != RSI_RESULT.RSI_SUCCESS && Reintentos == 3)
                                return Errores.Error_Conexion;
                        }
                    Sleep(1);
                } while ((Dl.format != RSI_DATA_LOG_FORMAT.RSI_DLF_BUFFER_EMPTY) && (rsiResult == RSI_RESULT.RSI_SUCCESS));
                return Errores.Correcto;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
            return Errores.Error_Desconocido;

        }
        byte[] Datos2Datos(DATOS Dato)
        {
            Byte[] bt = new byte[5];
            bt[0] = Dato.DATO1;
            bt[1] = Dato.DATO2;
            bt[2] = Dato.DATO3;
            bt[3] = Dato.DATO4;
            bt[4] = Dato.DATO5;
            return bt;
        }
        bool AgregaChecada(RSI_DATA_LOG_ELEMENT Dl)
        {
            try
            {
                DateTime FechaHora = new DateTime(2000 + Dl.time.year, Dl.time.month, Dl.time.day, Dl.time.hour, Dl.time.minute, Dl.time.second);
                string Numero = Bcd2String(Datos2Datos(Dl.data1), 0, 5);
                Numero = Convert.ToInt32(Numero).ToString();

                TipoAccesos TA = TipoAccesos.Incorrecto;

                if (Dl.format == RSI_DATA_LOG_FORMAT.RSI_DLF_IDENTITY_VERIFIED || Dl.format == RSI_DATA_LOG_FORMAT.RSI_DLF_IDENTITY_UNKNOWN)
                    if (Dl.format == RSI_DATA_LOG_FORMAT.RSI_DLF_IDENTITY_VERIFIED)
                    {
                        TA = TipoAccesos.Correcto;

                        switch (Dl.data2.DATO1)
                        {
                            case 1: TA = TipoAccesos.Entrada; break;
                            case 2: TA = TipoAccesos.Salida_a_Comer; break;
                            case 3: TA = TipoAccesos.Salida; break;
                            case 5: TA = TipoAccesos.Regreso_de_comer; break;
                            case 7: TA = TipoAccesos.No_definido; break;
                        }
                    }
                CeLog2.AgregaLog("AgregaChecada " + Numero + " - " + FechaHora.ToString() + " - " + TA.ToString());

                return AgregaChecada(Numero, FechaHora, TA, true);
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
            return false;
        }

        override protected Errores AsignaFechaHora(DateTime FechaHora)
        {
            RSI_TIME_DATE FechaHoraRSI;

            int Year = FechaHora.Year % 1000;
            FechaHoraRSI.day = Convert.ToByte(FechaHora.Day);
            FechaHoraRSI.month = Convert.ToByte(FechaHora.Month);
            FechaHoraRSI.year = Convert.ToByte(Year);
            FechaHoraRSI.hour = Convert.ToByte(FechaHora.Hour);
            FechaHoraRSI.minute = Convert.ToByte(FechaHora.Minute);
            FechaHoraRSI.second = Convert.ToByte(FechaHora.Second);
            RSI_RESULT R = rsiHereIsTime(m_Canal, ref FechaHoraRSI);
            if (R != RSI_RESULT.RSI_SUCCESS)
                return Errores.Error_Desconocido;
            return Errores.Correcto;
        }

        protected override Errores EnvioVectores()
        {
            if( !REEMPLAZAR_VECTORES &&
             !m_DatosTerminal.IsTERMINAL_ENROLANull() && m_DatosTerminal.TERMINAL_ENROLA > 0)
            {
                return Errores.Correcto;
            }

            if (m_DatosEmpleados == null)
                return Errores.Error_Desconocido;

            if (m_DatosEmpleados.Rows.Count <= 0)
            {
                IUVectoresNuevo = "";
                return Errores.No_Requiere_Cambio_En_Vectores;
            }
            int NoBancos = 2;
            if (m_DatosEmpleados.Rows.Count > 512)
                NoBancos = 38;
            RSI_DATA_BANK2[] BancoDatos = new RSI_DATA_BANK2[NoBancos];
            for (int ContBancos = 0; ContBancos < NoBancos; ContBancos++)
            {
                BancoDatos[ContBancos] = new RSI_DATA_BANK2();
                BancoDatos[ContBancos].data = new byte[4096];
                for (int Cont = 0; Cont < 4096; Cont++)
                {
                    BancoDatos[ContBancos].data[Cont] = 255;
                }
            }
            int Personas = 0;
            foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
            {
                try
                {
                    bool Agregar = false;
                    byte[] Vector = new byte[11];
                    if (!Fila.IsPERSONA_ID_S_HUELLANull() && Fila.PERSONA_ID_S_HUELLA != 0)
                    {
                        for (int Cont = 0; Cont < 9; Cont++)
                            Vector[Cont] = 0xFF;

                        Array.Copy(String2Bcd("0000"), 0, Vector, 9, 2);

                        Agregar = true;
                    }
                    else
                    {
                        if (!Fila.IsPERSONAS_A_VEC_1Null() && Fila.PERSONAS_A_VEC_1.Length == 11)
                        {
                            Array.Copy(Fila.PERSONAS_A_VEC_1, Vector, 11);
#if BAHIA
                            Array.Copy(String2Bcd("A000"), 0, Vector, 9, 2);
#endif
                            Agregar = true;
                        }
                    }
                    if (Agregar)
                    {
                        string ID = Convert.ToInt32(Fila.PERSONAS_A_VEC_T1).ToString("0000000000");
                        Array.Copy(String2Bcd(ID), 0, BancoDatos[Personas / 256].data, (Personas % 256) * 16, 5);
                        Array.Copy(Vector, 0, BancoDatos[Personas / 256].data, (Personas % 256) * 16 + 5, 9);
                        //                            Array.Copy(String2Bcd("0500"), 0, BancoDatos[Personas / 256].data, (Personas % 256) * 16 + 14, 2);
                        Array.Copy(Vector, 9, BancoDatos[Personas / 256].data, (Personas % 256) * 16 + 14, 2);

                        Personas++;
                    }

                }
                catch (System.Exception e)
                {
                    CeLog2.AgregaError(e);
                }
            }
            string Vectores = "";
            for (int ContBancos1 = 0; ContBancos1 < NoBancos ; ContBancos1++)
                Vectores += BitConverter.ToString(BancoDatos[ContBancos1].data);
                //BitConverter.ToString(BancoDatos[0].data) + BitConverter.ToString(BancoDatos[1].data);
            IUVectoresNuevo = CalculaHash(Vectores);
            if (IUVectores == IUVectoresNuevo)
                return Errores.No_Requiere_Cambio_En_Vectores;
            RSI_RESULT Resp;
            rsiEnterIdleMode(m_Canal);
            Resp = RSI_RESULT.RSI_SUCCESS;
            for (int ContBancos = 0; ContBancos < NoBancos && Resp == RSI_RESULT.RSI_SUCCESS; ContBancos++)
                Resp = rsiSetDataBank(m_Canal, Convert.ToByte( ContBancos), ref BancoDatos[ContBancos]);
#if DEBUG
            System.IO.File.WriteAllBytes("c:\\rsiSetDataBank.txt", BancoDatos[0].data);
#endif
            rsiResume(m_Canal);
            if (Resp == RSI_RESULT.RSI_SUCCESS)
            {
                IUVectores = IUVectoresNuevo;
                AgregaLog("SE ha enviando el archivo de palmas correctamente");
                return Errores.Correcto;
            }
            return Errores.Error_Desconocido;
        }

        public int AgregaHuella(long ID, byte[] Template)
        {
            try
            {

                WS_eCheck.DS_WSPersonasTerminales.DT_VectoresRow Fila = m_Vectores.FindByPERSONAS_A_VEC_T1(ID.ToString());

                bool EsNuevo = false;
                if (Fila == null)
                {
                    Fila = m_Vectores.NewDT_VectoresRow();
                    Fila.PERSONAS_A_VEC_T1 = ID.ToString();
                    EsNuevo = true;
                }

                Fila.PERSONAS_A_VEC_1 = Template;


                if (EsNuevo)
                {
                    m_Vectores.AddDT_VectoresRow(Fila);
                }

            }
            catch (Exception ex)
            {
                AgregaError(ex);
            }
            return 1;
        }
        /// <summary>
        /// Esta funcion se llama despues de que se ha poleado 
        /// los vectores y subido dicha información al servidor
        /// </summary>
        /// <returns></returns>
        protected override Errores PoleoVectoresSatisfactorio()
        {
            /*
#if BAHIA
            int NoBancos = 1;
            RSI_DATA_BANK2[] BancoDatos = new RSI_DATA_BANK2[NoBancos];
            for (int ContBancos = 0; ContBancos < NoBancos; ContBancos++)
            {
                BancoDatos[ContBancos] = new RSI_DATA_BANK2();
                BancoDatos[ContBancos].data = new byte[4096];
                for (int Cont = 0; Cont < 4096; Cont++)
                {
                    BancoDatos[ContBancos].data[Cont] = 255;
                }
            }
            rsiSetDataBank(m_Canal, 0, ref BancoDatos[0]);

#endif */
            return Errores.Correcto;
        }

        protected override Errores PoleoVectores()
        {
            try
            {           /* RSI_DATA_BANK []BancoDatos = new RSI_DATA_BANK[3];
                BancoDatos[0] = new RSI_DATA_BANK();
                BancoDatos[1] = new RSI_DATA_BANK();*/
                int Usuarios = DescargaUsuarios(m_Canal);
                for (int Cont = 0; Cont < Usuarios; Cont++)
                {
                    RSI_USER_RECORD_CS UR = new RSI_USER_RECORD_CS(ObtenUsuarioMem(Cont));
                    AgregaHuella(UR.ID, UR.VectorCompleto);


                }
                //    object Datos = _rsiGetDataBank(m_Canal, 0);
                /*            rsiGetDataBank(m_Canal, 0, ref BancoDatos[0]);
                            rsiGetDataBank(m_Canal, 1, ref BancoDatos[1]);
                            string Vectores = BitConverter.ToString(BancoDatos[0].data) + BitConverter.ToString(BancoDatos[1].data);
                            IUVectoresNuevo = CalculaHash(Vectores);*/
                return Errores.Correcto;/*
                int R = DescargaHuellas();
                if (R > 0)
                    return Errores.Correcto;
                if (R == -1)
                    return Errores.No_Requiere_Cambio_En_Vectores;
                return Errores.Error_Desconocido;*/
            }
            catch (System.Exception e)
            {
                CeLog2.AgregaError(e);
            }
            return Errores.Error_Desconocido;
        }

        //Pendientes la sincronizacion de Vectores
    }
}
