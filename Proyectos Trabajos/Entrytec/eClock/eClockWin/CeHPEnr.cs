using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace eClockWin
{
    class CeHPEnr
    {
        #region Enumeraciones

        public enum RSI_PROMPT
        {
	        RSI_RIGHT	= 0,
	        RSI_LEFT	= 1,
	        RSI_BLANK	= 2
        }	;

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

        public struct RSI_LAST_TEMPLATE
        {
            public Int16 Score;
            public RSI_TEMPLATE Templete;
            /*
            public byte Palma1;
            public byte Palma2;
            public byte Palma3;
            public byte Palma4;
            public byte Palma5;
            public byte Palma6;
            public byte Palma7;
            public byte Palma8;
            public byte Palma9;*/
        };

        public struct RSI_TEMPLATE
        {
            public byte Palma1;
            public byte Palma2;
            public byte Palma3;
            public byte Palma4;
            public byte Palma5;
            public byte Palma6;
            public byte Palma7;
            public byte Palma8;
            public byte Palma9;
        };

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
/*
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
 */
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

        #region CIsHpDll.dll
        [DllImport("CIsHpDll.dll")]
        public static extern int _rsiGetDataLogElement
    (
        int Canal,
        int Previo
    );
        [DllImport("CIsHpDll.dll")]
        public static extern RSI_DATA_LOG_FORMAT _RSI_DATA_LOG_FORMAT
    (
    );
        [DllImport("CIsHpDll.dll")]
        public static extern RSI_TIME_DATE _RSI_TIME_DATE
    (
    );
        [DllImport("CIsHpDll.dll")]
        public static extern DATOS _RSI_DATA_LOG_DATA_1
    (
    );
        [DllImport("CIsHpDll.dll")]
        public static extern DATOS _RSI_DATA_LOG_DATA_2
    (
    );
        [DllImport("CIsHpDll.dll")]
        public static extern int Canal
    (
        int Canal,
        int Previo
    );
        [DllImport("CIsHpDll.dll")]
        public static extern int DescargaUsuarios
            (
            int Canal
            );


        [DllImport("CIsHpDll.dll")]
        public static extern int LimpiaUsuarios();

        [DllImport("CIsHpDll.dll")]
        public static extern int PreAgregaUsuario(
            RSI_USER_RECORD Usuario
            );

        [DllImport("CIsHpDll.dll")]
        public static extern int EnviaUsuarios(
            int Canal
            );

        [DllImport("CIsHpDll.dll")]
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

        public static Byte[] ObtenArregloBytes(string Cadena)
        {
            if (Cadena.Length < 1)
                return null;
            Byte[] Arreglo = new byte[Cadena.Length + 1];
            for (int Cont = 0; Cont < Cadena.Length; Cont++)
            {
                Arreglo[Cont] = Convert.ToByte(Cadena[Cont]);
            }
            Arreglo[Cadena.Length] = 0;
            return Arreglo;
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
        public static extern RSI_RESULT rsiGetLastTemplate
            (
                int Canal,
                ref RSI_LAST_TEMPLATE Estatus
            );

        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiVerifyOnExternalData
            (
                int Canal,
                RSI_PROMPT Prompt,
                ref RSI_TEMPLATE tmpl
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
                
        [DllImport("RSIDLL32.dll")]
        public static extern RSI_RESULT rsiEnrollUser
            (
                int Canal,
                RSI_PROMPT Prompt
            );
        
        #endregion

        public static void Sleep(int Milisegundos)
        {
            try
            {
                System.Threading.Thread.Sleep(Milisegundos);
                return;
            }
            catch (Exception ex)
            {
#if!eClockWin
                CeTLog2.AgregaError(ex);
#endif
            }
#if!eClockWin
            CeTLog2.AgregaError("Recuperando de error en Sleep");
#endif

        }
        protected int Terminal_ID = 0;
        public CeC_Terminales m_TConexion = new CeC_Terminales();
        bool m_Conectado = false;
        int m_Canal = 0;
        RSI_STATUS m_Status = new RSI_STATUS();

        public bool Conecta()
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
#if!eClockWin
                            AgregaError("Error en rsiInitWsock");
#endif
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
#if!eClockWin
                        AgregaError("Error en rsiInstallChannel puerto " + m_TConexion.Puerto + " -1");
#endif
                        return false;
                    }
                    RR = rsiOpenDirectChannel(m_Canal, m_TConexion.Velocidad);
                    if (((RSI_RESULT)RR) != RSI_RESULT.RSI_SUCCESS)
                    {
#if!eClockWin
                        AgregaError("Error en rsiOpenDirectChannel Velocidad " + m_TConexion.Velocidad);
#endif
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


                rsiResume(m_Canal);
                return false;
            }
            m_Conectado = true;
            return true;
        }

        public bool Desconecta()
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

        public byte[] Enrolar()
        {
            if (m_Canal <= 0)
                return null;
            RSI_STATUS status = new RSI_STATUS();
            status.rslts_rdy = false;
            status.failed_cmd = false;
            status.dlog_rdy = true;
            RSI_RESULT Resp = rsiEnrollUser(m_Canal, RSI_PROMPT.RSI_BLANK);
            
            /*if (rsiEnrollUser(m_Canal, RSI_PROMPT.RSI_RIGHT) != RSI_RESULT.RSI_SUCCESS)
                return null;*/
            while (status.dlog_rdy && !status.failed_cmd)    // 'D' command
            {
                if (rsiGetStatus(m_Canal, ref status) != RSI_RESULT.RSI_SUCCESS) return null;
                //Dlg.ActualizaEstatus(status);
                //            AfxMessageBox("rsiGetStatus failed!");
            }

            if (status.failed_cmd)
            {
                //        AfxMessageBox("Remote Enrollment failed!");
                System.Windows.Forms.MessageBox.Show("No se puedo enrolar satisfactoriamente intentelo nuevamente", "Atención", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return null;
            }
            Sleep(1000);
            RSI_LAST_TEMPLATE tmpl = new RSI_LAST_TEMPLATE();
            Resp = rsiGetLastTemplate(m_Canal, ref tmpl);
            if (Resp == RSI_RESULT.RSI_SUCCESS)
            {
                byte[] Vector = new byte[11];
                Vector[0] = tmpl.Templete.Palma1;
                Vector[1] = tmpl.Templete.Palma2;
                Vector[2] = tmpl.Templete.Palma3;
                Vector[3] = tmpl.Templete.Palma4;
                Vector[4] = tmpl.Templete.Palma5;
                Vector[5] = tmpl.Templete.Palma6;
                Vector[6] = tmpl.Templete.Palma7;
                Vector[7] = tmpl.Templete.Palma8;
                Vector[8] = tmpl.Templete.Palma9;
                Vector[9] = 0;
                Vector[10] = 0;
                return Vector;
                /*
                Resp = rsiVerifyOnExternalData(m_Canal, RSI_PROMPT.RSI_BLANK,ref  tmpl.Templete);
                if (Resp == RSI_RESULT.RSI_SUCCESS)
                {
                    status.rslts_rdy = false;
                    status.failed_cmd = false;
                    status.dlog_rdy = true;
                    while (status.dlog_rdy && !status.failed_cmd)    // 'D' command
                    {
                        if (rsiGetStatus(m_Canal, ref status) != RSI_RESULT.RSI_SUCCESS) return null;
                    }

                    if (status.failed_cmd)
                    {
                        return null;
                    }
                    byte[] Vector = new byte[9];
                    Vector[0] = tmpl.Templete.Palma1;
                    Vector[1] = tmpl.Templete.Palma2;
                    Vector[2] = tmpl.Templete.Palma3;
                    Vector[3] = tmpl.Templete.Palma4;
                    Vector[4] = tmpl.Templete.Palma5;
                    Vector[5] = tmpl.Templete.Palma6;
                    Vector[6] = tmpl.Templete.Palma7;
                    Vector[7] = tmpl.Templete.Palma8;
                    Vector[8] = tmpl.Templete.Palma9;
                    return Vector;
                }*/
            }
            return null;
        }
    }
}
