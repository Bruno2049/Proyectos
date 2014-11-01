using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
namespace eEnroler
{
    class CeT_Mifare
    {
        [DllImport("mi.dll", EntryPoint = "GET_SNR", CharSet = CharSet.Auto)]
        public static extern int GET_SNR(int handle, int DeviceAddress,  Byte mode,
            Byte half, Byte []snr, Byte []Buffer);
        [DllImport("mi.dll", EntryPoint = "RDM_GetSnr", CharSet = CharSet.Auto)]
        public static extern int RDM_GetSnr(int commHandle, int deviceAddress,
                                ref Byte pCardNo);
        public static int DeviceAddress = 0;
        public static int commHandle = 0;
        public static UInt32 ObtenNSTarjeta()
        {
            
            /*StringBuilder Snr = new StringBuilder(16);
            StringBuilder Buffer = new StringBuilder(128);*/
            //string Snr= "";//= new byte[16];
           // string Buffer = "";// = new byte[128];

                byte[] Snr =new byte[16];
                byte[] Buffer = new byte[128];
                Snr[0] = 0;
                Buffer[0] = 0;
                
               // RDM_GetSnr(hComm, DeviceAddress, ref Buffer[0]);
                if (GET_SNR(commHandle, DeviceAddress, 38, 0, Snr, Buffer) != 0)
                    return 0;
                byte[] Datos = new byte[4];
                /*if (API_PCDRead(commHandle, DeviceAddress, 0, 0, Snr, Buffer) != 0)
                {
                }*/

            Datos[0] = Datos[1] = Datos[2] = Datos[3] = 0x00;
            for (int Cont = 0; Cont < 4 ; Cont++)
            {
                //Invierte el arreglo
                //Datos[3 - Cont] = Buffer[Cont];
                Datos[Cont] = Buffer[Cont];
            }
            return BitConverter.ToUInt32(Datos, 0);

        }
                [DllImport("mi.dll", EntryPoint = "API_PCDWrite", CharSet = CharSet.Auto)]
        public static extern int API_PCDWrite(int commHandle, int DeviceAddress,
                                 Byte mode, Byte blk_add, Byte num_blk, Byte[]snr, Byte []buffer);
                [DllImport("mi.dll", EntryPoint = "API_PCDRead", CharSet = CharSet.Auto)]
        public static extern int API_PCDRead(int commHandle, int DeviceAddress,
                                 Byte mode, Byte blk_add, Byte num_blk, Byte[]snr, Byte []buffer);

        public static int EscribeBloque(byte add,  byte[] Datos)
        {
            if (add < 0 || add > 64)
                return 10;
            if (add % 4 == 3)
                return 11; 
            byte[] snr = new byte[128];
            for(int X = 0; X < 16; X++)
                snr[X] = 0xFF;

            byte[] buf = new byte[128];
            for (int X = 0; X < 16; X++)
                buf[X] = 0xFF;
            buf[0] = 0xff;
            buf[1] = 0xff;
            buf[2] = 0xff;
            buf[3] = 0xff;
            buf[4] = 0xff;
            buf[5] = 0xff;

            int Res = API_PCDWrite(commHandle, DeviceAddress, 0, add, 1,  buf,Datos);
            return Res;
        }
    }
}
