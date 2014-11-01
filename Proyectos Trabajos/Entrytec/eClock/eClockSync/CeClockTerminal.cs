using System.Diagnostics;
using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Runtime.InteropServices;


public class CeClockTerminal
{
    // Consts

    public const short CKT_ERROR_INVPARAM = -1;
    public const short CKT_ERROR_NETDAEMONREADY = -1;
    public const short CKT_ERROR_CHECKSUMERR = -2;
    public const short CKT_ERROR_MEMORYFULL = -1;
    public const short CKT_ERROR_INVFILENAME = -3;
    public const short CKT_ERROR_FILECANNOTOPEN = -4;
    public const short CKT_ERROR_FILECONTENTBAD = -5;
    public const short CKT_ERROR_FILECANNOTCREATED = -2;
    public const short CKT_ERROR_NOTHISPERSON = -1;

    public const short CKT_RESULT_OK = 1;
    public const short CKT_RESULT_ADDOK = 1;
    public const short CKT_RESULT_HASMORECONTENT = 2;


    //Public Const PERSONINFOSIZE As Short = 44


    // Types

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct NETINFO
    {
        [MarshalAs(UnmanagedType.I4)]
        public int ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] IP;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Mask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Gateway;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] ServerIP;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] MAC;
    }

    public struct DATETIMEINFO
    {
        public int ID;
        public short Year_Renamed;
        public byte Month_Renamed;
        public byte Day_Renamed;
        public byte Hour_Renamed;
        public byte Minute_Renamed;
        public byte Second_Renamed;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PERSONINFO
    {
        [MarshalAs(UnmanagedType.I4)]
        public int PersonID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Password;
        [MarshalAs(UnmanagedType.I4)]
        public int CardNo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] Name;
        [MarshalAs(UnmanagedType.I4)]
        public int Dept; //²¿ÃÅ
        [MarshalAs(UnmanagedType.I4)]
        public int Group; //²¿ÃÅ
        [MarshalAs(UnmanagedType.I4)]
        public int KQOption; //¿¼ÇÚÄ£Ê½
        [MarshalAs(UnmanagedType.I4)]
        public int FPMark; //
        [MarshalAs(UnmanagedType.I4)]
        public int Other; //ÌØÊâÐÅÏ¢ =0 ÆÕÍ¨ÈËÔ±, =1 ¹ÜÀíÔ±
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct CLOCKINGRECORD
    {
        [MarshalAs(UnmanagedType.I4)]
        public int ID;
        [MarshalAs(UnmanagedType.I4)]
        public int PersonID;
        [MarshalAs(UnmanagedType.I4)]
        public int Stat;
        [MarshalAs(UnmanagedType.I4)]
        public int BackupCode;
        [MarshalAs(UnmanagedType.I4)]
        public int WorkTyte;
        //<VBFixedString(20), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=20)> _
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] Time;
    }

    public struct DEVICEINFO
    {
        public int ID;
        public int MajorVersion;
        public int MinorVersion;
        public int AdminPassword;
        public int DoorLockDelay;
        public int SpeakerVolume;
        public int Parameter;
        public int DefaultAuth;
        public int Capacity;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct RINGTIME
    {
        [MarshalAs(UnmanagedType.I4)]
        public int hour;
        [MarshalAs(UnmanagedType.I4)]
        public int minute;
        [MarshalAs(UnmanagedType.I4)]
        public int week;
    }

    public struct TIMESECT
    {
        public byte bHour;
        public byte bMinute;
        public byte eHour;
        public byte eMinute;
    }


    public struct CKT_MessageInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int PersonID;
        [MarshalAs(UnmanagedType.I4)]
        public int sYear;
        [MarshalAs(UnmanagedType.I4)]
        public int sMon;
        [MarshalAs(UnmanagedType.I4)]
        public int sDay;
        [MarshalAs(UnmanagedType.I4)]
        public int eYear;
        [MarshalAs(UnmanagedType.I4)]
        public int eMon;
        [MarshalAs(UnmanagedType.I4)]
        public int eDay;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] msg;
    }

    public struct CKT_MessageHead
    {
        [MarshalAs(UnmanagedType.I4)]
        public int PersonID;
        [MarshalAs(UnmanagedType.I4)]
        public int sYear;
        [MarshalAs(UnmanagedType.I4)]
        public int sMon;
        [MarshalAs(UnmanagedType.I4)]
        public int sDay;
        [MarshalAs(UnmanagedType.I4)]
        public int eYear;
        [MarshalAs(UnmanagedType.I4)]
        public int eMon;
        [MarshalAs(UnmanagedType.I4)]
        public int eDay;
    }

    // Routines

    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_FreeMemory(uint memory);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_FreeMemory(int memory);


    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_RegisterSno(int Sno, int ComPort);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_RegisterUSB(int Sno, int index);

    //Public Declare Function CKT_RegisterNet Lib "tc400.dll" (ByVal Sno As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal Addr As String) As Integer
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_RegisterNet(int Sno, string Addr);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void CKT_UnregisterSnoNet(int Sno);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_NetDaemon();
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_ComDaemon();
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void CKT_Disconnect();
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_ReportConnections(ref int ppSno);


    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetDeviceNetInfo(int Sno, ref NETINFO pNetInfo);
    //Public Declare Function CKT_SetDeviceIPAddr Lib "tc400.dll" (ByVal Sno As Integer, ByRef IP As Byte) As Integer
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetDeviceIPAddr(int Sno, byte[] IP);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetDeviceMask(int Sno, ref byte Mask);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetDeviceGateway(int Sno, ref byte Gate);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetDeviceServerIPAddr(int Sno, ref byte Svr);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetDeviceMAC(int Sno, ref byte MAC);


    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetDeviceClock(int Sno, ref DATETIMEINFO pDateTimeInfo);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetDeviceDate(int Sno, short Year_Renamed, byte Month_Renamed, byte Day_Renamed);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetDeviceTime(int Sno, byte Hour_Renamed, byte Minute_Renamed, byte Second_Renamed);


    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetFPTemplate(int Sno, int PersonID, int FPID, ref int pFPData, ref int FPDataLen);
    public static int CKT_GetFPTemplate(int Sno, int PersonID, int FPID, ref byte []pFPData, ref int FPDataLen)
    {
        int pHuella = 0;
        int pLenHuella = 0;
        int R = CKT_GetFPTemplate(Sno, PersonID, FPID, ref  pHuella, ref pLenHuella);
        if (pHuella != 0)
            CKT_FreeMemory(pHuella);
        if (R == 1)
        {
            pFPData = new byte[pLenHuella];
            FPDataLen = pLenHuella;
            PCopyMemory(ref pFPData[0], pHuella, pLenHuella);
        }
        return R;
    }
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_PutFPTemplate(int Sno, int PersonID, int FPID, byte[] pFPData, int FPDataLen);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetFPTemplateSaveFile(int Sno, int PersonID, int FPID, string FPDataFilename);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_PutFPTemplateLoadFile(int Sno, int PersonID, int FPID, string FPDataFilename);


    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetFPRawData(int Sno, int PersonID, int FPID, ref byte FPRawData);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_PutFPRawData(int Sno, int PersonID, int FPID, ref byte FPRawData);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetFPRawDataSaveFile(int Sno, int PersonID, int FPID, string FPDataFilename);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_PutFPRawDataLoadFile(int Sno, int PersonID, int FPID, string FPDataFilename);


    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_ListPersonInfo(int Sno, ref int pRecordCount, ref int ppPersons);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_ModifyPersonInfo(int Sno, ref PERSONINFO person);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_DeletePersonInfo(int Sno, int PersonID, int backupID);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_EraseAllPerson(int Sno);


    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_ListPersonInfoEx(int Sno, ref int ppLongRun);
    [CLSCompliant(false)]
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_ListPersonProgress(int pLongRun, ref int pRecCount, ref int pRetCount, ref Int32 ppPersons);


    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetCounts(int Sno, ref int pPersonCount, ref int pFPCount, ref int pClockingsCount);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_ClearClockingRecord(int Sno, int type, int count);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetClockingRecordEx(int Sno, ref int ppLongRun);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetClockingNewRecordEx(int Sno, ref int ppLongRun);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetClockingRecordProgress(int pLongRun, ref int pRecCount, ref int pRetCount, ref int ppPersons);


    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_ResetDevice(int Sno);

    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetDeviceInfo(int Sno, ref DEVICEINFO devinfo);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetDefaultAuth(int Sno, int Auth);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetDoor(int Sno, int Second_Renamed);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetSpeakerVolume(int Sno, int Volume);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetDeviceAdminPassword(int Sno, [MarshalAs(UnmanagedType.LPStr)]string Password);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetRealtimeMode(int Sno, int RealMode);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetFixWGHead(int Sno, int WGHead);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetWG(int Sno, int WGMode);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetRingAllow(int Sno, int type);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetRepeatKQ(int Sno, int time);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetAutoUpdate(int Sno, int AutoUpdate);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_ForceOpenLock(int Sno);


    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_ReadRealtimeClocking(ref int ppClockings);

    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetTimeSection(int Sno, int ord, [Out()]TIMESECT[] ts);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetTimeSection(int Sno, int ord, [In()]TIMESECT[] ts);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetGroup(int Sno, int ord, [Out()]int[] grp);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetGroup(int Sno, int ord, int[] grp);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetHitRingInfo(int Sno, [Out()]RINGTIME[] array);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_SetHitRingInfo(int Sno, int ord, ref RINGTIME ring);

    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetMessageByIndex(int Sno, int idx, ref CKT_MessageInfo msg);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_AddMessage(int Sno, ref CKT_MessageInfo msg);
    //Public Declare Function CKT_GetAllMessageHead Lib "tc400.dll" (ByVal Sno As Integer, <[In](), Out()> ByVal mh As CKT_MessageHead()) As Integer
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_GetAllMessageHead(int Sno, [Out()]CKT_MessageHead[] mh);
    [DllImport("tc400.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int CKT_DelMessageByIndex(int Sno, int idx);


    [DllImport("kernel32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void GetLocalTime(ref SYSTEMTIME lpSystemTime);

    //Private Declare Sub PCopyMemory Lib "kernel32"  Alias "RtlMoveMemory"(ByRef Destination As Any, ByVal Source As Integer, ByVal Length As Integer)
    [DllImport("kernel32", EntryPoint = "RtlMoveMemory", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void PCopyMemory(ref int Destination, int Source, int Length);
    [DllImport("kernel32", EntryPoint = "RtlMoveMemory", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void PCopyMemory(ref byte Destination, int Source, int Length);
    [DllImport("kernel32", EntryPoint = "RtlMoveMemory", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void PCopyMemory(ref PERSONINFO Destination, int Source, int Length);
    [DllImport("kernel32", EntryPoint = "RtlMoveMemory", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void PCopyMemory(ref CLOCKINGRECORD Destination, int Source, int Length);
    [CLSCompliant(false)]
    [DllImport("kernel32", EntryPoint = "RtlMoveMemory", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void PCopyMemory(ref NETINFO Destination, UInt32 Source, int Length);
    [DllImport("kernel32", EntryPoint = "RtlMoveMemory", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void PCopyMemory(ref RINGTIME Destination, int Source, int Length);
    [DllImport("kernel32", EntryPoint = "RtlMoveMemory", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void PCopyMemory(ref TIMESECT Destination, int Source, int Length);
    [DllImport("kernel32", EntryPoint = "RtlMoveMemory", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void PCopyMemory(int Source, ref TIMESECT Destination, int Length);
    [DllImport("kernel32", EntryPoint = "RtlMoveMemory", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void PPCopyMemory(int Destination, int Source, int Length);
    [DllImport("kernel32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern void Sleep(int dwMilliseconds);
    public struct SYSTEMTIME
    {
        private short wYear;
        private short wMonth;
        private short wDayOfWeek;
        private short wDay;
        private short wHour;
        private short wMinute;
        private short wSecond;
        private short wMilliseconds;
    }
}
