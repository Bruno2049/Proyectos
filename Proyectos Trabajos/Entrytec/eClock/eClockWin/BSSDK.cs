using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace BSSDK_NS
{
    class BSSDK
    {
        //
        // API Declarations
        //
        [DllImport("BS_SDK.dll",
             CharSet = CharSet.Ansi,
             EntryPoint = "BS_InitSDK")]
        public static extern int BS_InitSDK();

        [DllImport("BS_SDK.dll",
             CharSet = CharSet.Ansi,
             EntryPoint = "BS_OpenInternalUDP")]
        public static extern int BS_OpenInternalUDP(ref int handle);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_SearchDeviceInLAN")]
        public static extern int BS_SearchDeviceInLAN(int handle, ref int numOfDevice, uint[] deviceIDs, int[] deviceTypes, uint[] readerAddrs);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_SetDeviceID")]
        public static extern int BS_SetDeviceID(int handle, uint deviceID, int deviceType);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_ReadConfig")]
        public static extern int BS_ReadConfig(int handle, int configType, ref int configSize, IntPtr data);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_WriteConfig")]
        public static extern int BS_WriteConfig(int handle, int configType, int configSize, IntPtr data);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_ReadConfigUDP")]
        public static extern int BS_ReadConfigUDP(int handle, uint targetAddr, uint targetID, int configType, ref int configSize, IntPtr data);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_WriteConfigUDP")]
        public static extern int BS_WriteConfigUDP(int handle, uint targetAddr, uint targetID, int configType, int configSize, IntPtr data);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_OpenSocket")]
        public static extern int BS_OpenSocket(string addr, int port, ref int handle);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_CloseSocket")]
        public static extern int BS_CloseSocket(int handle);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_GetTime")]
        public static extern int BS_GetTime(int handle, ref int timeVal);
        public static DateTime BS_GetTime(int handle)
        {
            int localTime = 0;
            int Ret = BS_GetTime(handle, ref localTime);
            if (Ret != BSSDK.BS_SUCCESS)
            {
#if!eClockWin
                CeLog2.AgregaError("BS_GetTime -> Error al intentar obtener la hora");
#endif
                return new DateTime(2002,09,24);
            }
            DateTime DT = new DateTime(Convert.ToInt64(localTime) * 10000000 + new DateTime(1970, 1, 1).Ticks);
            return DT;
        }
        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_SetTime")]
        public static extern int BS_SetTime(int handle, int timeVal);

        public static int BS_SetTime(int handle, DateTime timeVal)
        {

            int localTime = (int)((timeVal.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000);
            return BS_SetTime(handle, localTime);
        }
        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_GetUserDBInfo")]
        public static extern int BS_GetUserDBInfo(int handle, ref int numOfUser, ref int numOfTemplate);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_GetAllUserInfoBEPlus")]
        public static extern int BS_GetAllUserInfoBEPlus(int handle, IntPtr userHdr, ref int numOfUser);
        public static int BS_GetAllUserInfoBEPlus(int handle, out BSSDK.BEUserHdr[] userHdr, ref int numOfUser)
        {
            int numUser = 0;
            int numTemplate = 0;
            int result = BSSDK.BS_GetUserDBInfo(handle, ref numUser, ref numTemplate);
            if (result != BSSDK.BS_SUCCESS)
            {
                userHdr = null;
                numOfUser = 0;
                return result;
            }
            userHdr = new BSSDK.BEUserHdr[numUser];
            IntPtr userInfo = Marshal.AllocHGlobal(numUser * Marshal.SizeOf(typeof(BSSDK.BEUserHdr)));


            result = BSSDK.BS_GetAllUserInfoBEPlus(handle, userInfo, ref numUser);


            if (result != BSSDK.BS_SUCCESS)
            {
                Marshal.FreeHGlobal(userInfo);

                return result;
            }

            for (int i = 0; i < numUser; i++)
            {
                userHdr[i] = (BSSDK.BEUserHdr)Marshal.PtrToStructure(new IntPtr(userInfo.ToInt32() + i * Marshal.SizeOf(typeof(BSSDK.BEUserHdr))), typeof(BSSDK.BEUserHdr));

            }


            Marshal.FreeHGlobal(userInfo);
            return result;
        }

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_GetUserInfoBEPlus")]
        public static extern int BS_GetUserInfoBEPlus(int handle, uint userID, IntPtr userHdr);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_DeleteUser")]
        public static extern int BS_DeleteUser(int handle, uint userID);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_DeleteAllUser")]
        public static extern int BS_DeleteAllUser(int handle);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_GetAllAccessGroupEx")]
        public static extern int BS_GetAllAccessGroupEx(int handle, ref int numOfGroup, IntPtr accessGroup);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_GetUserBEPlus")]
        public static extern int BS_GetUserBEPlus(int handle, uint userID, IntPtr userHdr, IntPtr templateData);
        public static int BS_GetUserBEPlus(int handle, uint userID, out BEUserHdr userHdr, out byte[] Templates)
        {
            IntPtr userInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BEUserHdr)));
            IntPtr templateData = Marshal.AllocHGlobal(384 * 4); // max 2 fingers(4 templates) per user
            uint ID = 1;
            int result = BSSDK.BS_GetUserBEPlus(handle, ID, userInfo, templateData);
            userHdr = (BSSDK.BEUserHdr)Marshal.PtrToStructure(userInfo, typeof(BSSDK.BEUserHdr));
            Templates = new byte[384 * 2 * userHdr.numOfFinger];
            Marshal.Copy(templateData, Templates, 0, Templates.Length);
            Marshal.FreeHGlobal(userInfo);
            Marshal.FreeHGlobal(templateData);
            return result;
        }

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_EnrollUserBEPlus")]
        public static extern int BS_EnrollUserBEPlus(int handle, IntPtr userHdr, IntPtr templateData);


        public static int BS_EnrollUserBEPlus(int handle, BEUserHdr userHdr, byte[] btemplateData)
        {
            if (btemplateData == null)
                return -1;
            IntPtr templateData = Marshal.AllocHGlobal(btemplateData.Length);
            IntPtr userInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK.BEUserHdr)));
            Marshal.StructureToPtr(userHdr, userInfo, true);
            Marshal.Copy(btemplateData, 0, templateData, btemplateData.Length);
            int R = BS_EnrollUserBEPlus(handle, userInfo, templateData);
            Marshal.FreeHGlobal(userInfo);
            Marshal.FreeHGlobal(templateData);
            return R;
        }


        [DllImport("BS_SDK.dll",
     CharSet = CharSet.Ansi,
    EntryPoint = "BS_ScanTemplate")]
        public static extern int BS_ScanTemplate(int handle, IntPtr templateData);

        public static int BS_ScanTemplate(int handle, out byte[] Template)
        {
            Template = null;
            IntPtr templateData = Marshal.AllocHGlobal(384); // max 2 fingers(4 templates) per user
            int R = BS_ScanTemplate(handle, templateData);
            if (R == BS_SUCCESS)
            {
                Template = new byte[384];
                Marshal.Copy(templateData, Template, 0, 384);

            }
            Marshal.FreeHGlobal(templateData);
            return R;
        }
        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_ReadCardID")]
        public static extern int BS_ReadCardID(int handle, ref uint cardID);

        [DllImport("BS_SDK.dll",
    CharSet = CharSet.Ansi,
   EntryPoint = "BS_ReadCardIDEx")]
        public static extern int BS_ReadCardIDEx(int handle, ref uint cardID, ref int customID);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_GetLogCount")]
        public static extern int BS_GetLogCount(int handle, ref int logCount);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_ReadLog")]
        public static extern int BS_ReadLog(int handle, int startTime, int endTime, ref int logCount, IntPtr logRecord);

        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_ReadNextLog")]
        public static extern int BS_ReadNextLog(int handle, int startTime, int endTime, ref int logCount, IntPtr logRecord);

        public static int BS_ReadLogBEP(int handle, out BSSDK.BSLogRecord[] Registros)
        {
            int NumOfLog = 0;
            int result = BSSDK.BS_GetLogCount(handle, ref NumOfLog);
            if (result != BSSDK.BS_SUCCESS)
            {
                Registros = null;
                return result;
            }
            IntPtr logRecord = Marshal.AllocHGlobal(NumOfLog * Marshal.SizeOf(typeof(BSSDK.BSLogRecord)));
            int logTotalCount = 0;
            int logCount = 0;
            // System.Windows.Forms.MessageBox.Show("Atencion");
            do
            {



                IntPtr buf = new IntPtr(logRecord.ToInt32() + logTotalCount * Marshal.SizeOf(typeof(BSSDK.BSLogRecord)));

                if (logTotalCount == 0)
                {
                    result = BSSDK.BS_ReadLog(handle, 0, 0, ref logCount, buf);
                }
                else
                {
                    result = BSSDK.BS_ReadNextLog(handle, 0, 0, ref logCount, buf);
                }

                if (result != BSSDK.BS_SUCCESS)
                {
#if!eClockWin
                    CeLog2.AgregaError("BS_ReadLogBEP-> Error de comunicación");
#endif
                    Marshal.FreeHGlobal(logRecord);
                    Registros = null;
                    return result;
                }

                logTotalCount += logCount;

            } while (logCount == 8192);
            if (NumOfLog != logTotalCount)
            {
#if!eClockWin
                CeLog2.AgregaError("BS_ReadLogBEP-> NumOfLog != logTotalCount No coinciden el numero de logs en la terminal y los descargado se intentará nuevamente posteriormente");
#endif
                Marshal.FreeHGlobal(logRecord);
                Registros = null;
                return -1;
            }
            Registros = new BSLogRecord[logTotalCount];
            for (int i = 0; i < logTotalCount; i++)
            {

                BSSDK.BSLogRecord record = (BSSDK.BSLogRecord)Marshal.PtrToStructure(new IntPtr(logRecord.ToInt32() + i * Marshal.SizeOf(typeof(BSSDK.BSLogRecord))), typeof(BSSDK.BSLogRecord));

                //                DateTime eventTime = new DateTime(1970, 1, 1).AddSeconds(record.eventTime);
                Registros[i] = record;
            }
            Marshal.FreeHGlobal(logRecord);
            return result;
        }



        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_DeleteLog")]
        public static extern int BS_DeleteLog(int handle, int logCount, ref int deletedCount);
        [DllImport("BS_SDK.dll",
            CharSet = CharSet.Ansi,
            EntryPoint = "BS_DeleteAllLog")]
        public static extern int BS_DeleteAllLog(int handle, int numOfLog, ref int numOfDeletedLog);

        //
        // Structure Declarations
        //
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct BESysInfoData
        {
            public uint magicNo;
            public int version;
            public uint timestamp;
            public uint checksum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] headerReserved;

            public uint ID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] macAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] boardVer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] firmwareVer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public int[] reserved;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct BEConfigData
        {
            // header
            public uint magicNo;
            public int version;
            public uint timestamp;
            public uint checksum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] headerReserved;

            // operation mode
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] opMode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] opModeSchedule;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] opDoubleMode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public int[] opReserved;

            // ip
            [MarshalAs(UnmanagedType.I1)]
            public bool useDHCP;
            public uint ipAddr;
            public uint gateway;
            public uint subnetMask;
            public uint serverIpAddr;
            public int port;
            [MarshalAs(UnmanagedType.I1)]
            public bool useServer;
            [MarshalAs(UnmanagedType.I1)]
            public bool synchTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public int[] ipReserved;

            // fingerprint
            public int securityLevel;
            public int fastMode;
            public int fingerReserved1;
            public int timeout; // 1 ~ 20 sec
            public int matchTimeout; // Infinite(0) ~ 10 sec
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public int[] fingerReserved2;

            // padding
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3016)]
            public int[] padding;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct BEUserHdr
        {
            public int version;
            public uint userID;

            public int startTime;
            public int expiryTime;

            public uint cardID;
            public byte cardCustomID;
            public byte commandCardFlag;
            public byte cardFlag;
            public byte cardVersion;

            public ushort adminLevel;
            public ushort securityLevel;

            public uint accessGroupMask;

            public ushort numOfFinger;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public ushort[] fingerChecksum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] isDuress;

            public int disabled;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
            public int[] reserved2;
        };


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct BSAccessGroupEx
        {
            public int groupID;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public String name;
            public int numOfReader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public uint[] readerID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] scheduleID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] reserved;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct BSLogRecord
        {
            public byte eventType;
            public byte subEvent;
            public ushort tnaEvent;
            public int eventTime;
            public uint userID;
            public uint reserved;
        };

        // 
        // Constants
        //
        public const int BS_SUCCESS = 0;

        public const int BS_DEVICE_BIOSTATION = 0;
        public const int BS_DEVICE_BEPLUS = 1;

        public const int BEPLUS_CONFIG = 0x50;
        public const int BEPLUS_CONFIG_SYS_INFO = 0x51;

        public const int NO_ACCESS_GROUP = 0xFD;
        public const int FULL_ACCESS_GROUP = 0xFE;

        // log events
        public const int BE_EVENT_SCAN_SUCCESS = 0x58;
        public const int BE_EVENT_ENROLL_BAD_FINGER = 0x16;
        public const int BE_EVENT_ENROLL_SUCCESS = 0x17;
        public const int BE_EVENT_ENROLL_FAIL = 0x18;
        public const int BE_EVENT_ENROLL_CANCELED = 0x19;

        public const int BE_EVENT_VERIFY_BAD_FINGER = 0x26;
        public const int BE_EVENT_VERIFY_SUCCESS = 0x27;
        public const int BE_EVENT_VERIFY_FAIL = 0x28;
        public const int BE_EVENT_VERIFY_CANCELED = 0x29;
        public const int BE_EVENT_VERIFY_NO_FINGER = 0x2a;

        public const int BE_EVENT_IDENTIFY_BAD_FINGER = 0x36;
        public const int BE_EVENT_IDENTIFY_SUCCESS = 0x37;
        public const int BE_EVENT_IDENTIFY_FAIL = 0x38;
        public const int BE_EVENT_IDENTIFY_CANCELED = 0x39;

        public const int BE_EVENT_DELETE_BAD_FINGER = 0x46;
        public const int BE_EVENT_DELETE_SUCCESS = 0x47;
        public const int BE_EVENT_DELETE_FAIL = 0x48;
        public const int BE_EVENT_DELETE_ALL_SUCCESS = 0x49;

        public const int BE_EVENT_VERIFY_DURESS = 0x62;
        public const int BE_EVENT_IDENTIFY_DURESS = 0x63;

        public const int BE_EVENT_TAMPER_SWITCH_ON = 0x64;
        public const int BE_EVENT_TAMPER_SWITCH_OFF = 0x65;

        public const int BE_EVENT_SYS_STARTED = 0x6a;
        public const int BE_EVENT_IDENTIFY_NOT_GRANTED = 0x6d;
        public const int BE_EVENT_VERIFY_NOT_GRANTED = 0x6e;

        public const int BE_EVENT_IDENTIFY_LIMIT_COUNT = 0x6f;
        public const int BE_EVENT_IDENTIFY_LIMIT_TIME = 0x70;

        public const int BE_EVENT_IDENTIFY_DISABLED = 0x71;
        public const int BE_EVENT_IDENTIFY_EXPIRED = 0x72;

        public const int BE_EVENT_APB_FAIL = 0x73;
        public const int BE_EVENT_COUNT_LIMIT = 0x74;
        public const int BE_EVENT_TIME_INTERVAL_LIMIT = 0x75;
        public const int BE_EVENT_INVALID_AUTH_MODE = 0x76;
        public const int BE_EVENT_EXPIRED_USER = 0x77;
        public const int BE_EVENT_NOT_GRANTED = 0x78;

        public const int BE_EVENT_DETECT_INPUT0 = 0x54;
        public const int BE_EVENT_DETECT_INPUT1 = 0x55;

        public const int BE_EVENT_TIMEOUT = 0x90;

        public const int BS_EVENT_RELAY_ON = 0x80;
        public const int BS_EVENT_RELAY_OFF = 0x81;

        public const int BE_EVENT_DOOR0_OPEN = 0x82;
        public const int BE_EVENT_DOOR1_OPEN = 0x83;
        public const int BE_EVENT_DOOR0_CLOSED = 0x84;
        public const int BE_EVENT_DOOR1_CLOSED = 0x85;

        public const int BE_EVENT_DOOR0_FORCED_OPEN = 0x86;
        public const int BE_EVENT_DOOR1_FORCED_OPEN = 0x87;

        public const int BE_EVENT_DOOR0_HELD_OPEN = 0x88;
        public const int BE_EVENT_DOOR1_HELD_OPEN = 0x89;

        public const int BE_EVENT_DOOR0_RELAY_ON = 0x8A;
        public const int BE_EVENT_DOOR1_RELAY_ON = 0x8B;

        public const int BE_EVENT_INTERNAL_INPUT0 = 0xA0;
        public const int BE_EVENT_INTERNAL_INPUT1 = 0xA1;
        public const int BE_EVENT_SECONDARY_INPUT0 = 0xA2;
        public const int BE_EVENT_SECONDARY_INPUT1 = 0xA3;

        public const int BE_EVENT_SIO0_INPUT0 = 0xB0;
        public const int BE_EVENT_SIO0_INPUT1 = 0xB1;
        public const int BE_EVENT_SIO0_INPUT2 = 0xB2;
        public const int BE_EVENT_SIO0_INPUT3 = 0xB3;

        public const int BE_EVENT_SIO1_INPUT0 = 0xB4;
        public const int BE_EVENT_SIO1_INPUT1 = 0xB5;
        public const int BE_EVENT_SIO1_INPUT2 = 0xB6;
        public const int BE_EVENT_SIO1_INPUT3 = 0xB7;

        public const int BE_EVENT_SIO2_INPUT0 = 0xB8;
        public const int BE_EVENT_SIO2_INPUT1 = 0xB9;
        public const int BE_EVENT_SIO2_INPUT2 = 0xBA;
        public const int BE_EVENT_SIO2_INPUT3 = 0xBB;

        public const int BE_EVENT_SIO3_INPUT0 = 0xBC;
        public const int BE_EVENT_SIO3_INPUT1 = 0xBD;
        public const int BE_EVENT_SIO3_INPUT2 = 0xBE;
        public const int BE_EVENT_SIO3_INPUT3 = 0xBF;

        public const int BE_EVENT_LOCKED = 0xC0;
        public const int BE_EVENT_UNLOCKED = 0xC1;

        public const int BE_EVENT_TIME_SET = 0xD2;
    }
}
