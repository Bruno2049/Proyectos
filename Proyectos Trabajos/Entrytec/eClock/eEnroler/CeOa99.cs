using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;

namespace eEnroler
{
    public class CeOa99
    {
        [DllImport("AvzScanner.dll", EntryPoint = "AvzFindDevice", CharSet = CharSet.Auto)]
        public static extern short AvzFindDevice(Byte[] Buffer);
        [DllImport("AvzScanner.dll", EntryPoint = "AvzOpenDevice", CharSet = CharSet.Auto)]
        public static extern int AvzOpenDevice(Int16 uDeviceID, IntPtr hWnd);
        [DllImport("AvzScanner.dll", EntryPoint = "AvzCloseDevice", CharSet = CharSet.Auto)]
        public static extern int AvzCloseDevice(Int16 uDeviceID);
        [DllImport("AvzScanner.dll", EntryPoint = "AvzGetImage", CharSet = CharSet.Auto)]
        public static extern int AvzGetImage(Int16 uDeviceID, Byte[] BufferImage, ref  Int16 bFingerOn);

        [DllImport("AvzScanner.dll", EntryPoint = "AvzSaveClrBMPFile", CharSet = CharSet.Auto)]
        public static extern int AvzSaveClrBMPFile(Byte[] RutaArchivo, Byte[] BufferImage);
        [DllImport("AvzScanner.dll", EntryPoint = "AvzSaveHueBMPFile", CharSet = CharSet.Auto)]
        public static extern int AvzSaveHueBMPFile(Byte[] RutaArchivo, Byte[] BufferImage);

        [DllImport("AvzScanner.dll", EntryPoint = "AvzProcess", CharSet = CharSet.Auto)]
        public static extern int AvzProcess(Byte[] BufferImage, Byte[] Feature, Byte[] BufferBinImage, bool bthin, bool bdrawfea);

        [DllImport("AvzScanner.dll", EntryPoint = "AvzPackFeature", CharSet = CharSet.Auto)]
        public static extern int AvzPackFeature(Byte[] Feature1, Byte[] Feature2, Byte[] FeatureResult);

        [DllImport("AvzScanner.dll", EntryPoint = "AvzUnpackFeature", CharSet = CharSet.Auto)]
        public static extern Int16 AvzUnpackFeature(Byte[] pPackFeature, Byte[] pFeature1, Byte[] pFeature2);

        [DllImport("AvzScanner.dll", EntryPoint = "AvzMatch", CharSet = CharSet.Auto)]
        public static extern Int16 AvzMatch(Byte[] feature1, 
  				 Byte[] feature2, 
				 Int16 level);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FeatureIn">Vector a comparar</param>
        /// <param name="FeatureLib">Librería de Vectores</param>
        /// <param name="FingerNum">Numero de vectores en la librería</param>
        /// <param name="Level">Nivel de verificación se usará 0</param>
        /// <returns></returns>
        [DllImport("AvzScanner.dll", EntryPoint = "AvzMatchN", CharSet = CharSet.Auto)]
        public static extern Int16 AvzMatchN(Byte[] FeatureIn, Byte[] FeatureLib, Int16 FingerNum, Int16 Level);
        [DllImport("AvzScanner.dll", EntryPoint = "AvzMatchN", CharSet = CharSet.Auto)]
        public static extern Int16 AvzMatchN(Byte[] FeatureIn, int FeatureLib, Int16 FingerNum, Int16 Level);

        public static int VarPtr(Object e)
        {
            GCHandle GC = GCHandle.Alloc(e, GCHandleType.Pinned);
            int  GC2 = GC.AddrOfPinnedObject().ToInt32();
            GC.Free();
            return GC2;
        }




        public static int AvzMatchN(Int16 uDeviceID, Byte[] FeatureLib, Int16 FingerNum, Int16 Level)
        {
            if (DateTime.Now > new DateTime(2013, 01, 01))
                return -2;
            Int16 bFingerOn = 99;
            byte[] ImgTemp = new byte[78400];
            byte[] Features = new byte[256];
            byte[] ImgTempBin = new byte[78400];
            CeOa99.AvzGetImage(uDeviceID, ImgTemp, ref bFingerOn);
            if (bFingerOn == 1)
            {
                if (CeOa99.AvzProcess(ImgTemp, Features, ImgTempBin, true, true) == 0)
                {
                    int R = AvzMatchN(Features, VarPtr(FeatureLib), FingerNum, Level);
                    if (R > FingerNum)
                        return -1;
                    return R;
                }
                return -3;
            }
            return -2;
        }

        [DllImport("AvzScanner.dll", EntryPoint = "AvzMatchN")]
        public static extern Int16 AvzMatchN(Byte[] FeatureIn, Byte[,] FeatureLib, Int16 FingerNum, Int16 Level);

        public static int AvzMatchN(Int16 uDeviceID, Byte[,] FeatureLib, Int16 FingerNum, Int16 Level)
        {
            if (DateTime.Now > new DateTime(2013, 01, 01))
                return -2;
            Int16 bFingerOn = 99;
            byte[] ImgTemp = new byte[78400];
            byte[] Features = new byte[338];
            byte[] ImgTempBin = new byte[78400];
            CeOa99.AvzGetImage(uDeviceID, ImgTemp, ref bFingerOn);
            if (bFingerOn == 1)
            {
                if (CeOa99.AvzProcess(ImgTemp, Features, ImgTempBin, true, true) == 0)
                {
                    int R = AvzMatchN(Features, FeatureLib, FingerNum, Level);
                    if (R > FingerNum)
                        return -1;
                    return R;
                }
                return -3;
            }
            return -2;
        }

    }
}
