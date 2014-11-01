using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
//using System.Windows.Forms;

//namespaces needed to add 
using System.IO;//used for Class FileStream
using System.Runtime.InteropServices;//used for Class Marshal
using UdiskData;

namespace Decode_ZK
{
    class Decode_ZK
    {
        public void ConvertirArchivo()
        {
            ImportaArchivo();
        }

        //private void btnAttLogExtRead_Click(object sender, EventArgs e)
        private void ImportaArchivo()
        {
            UDisk udisk = new UDisk();

            byte[] byDataBuf = null;
            int iLength;//length of the bytes to get from the data

            string sPIN2 = "";
            string sVerified = "";
            string sTime_second = "";
            string sDeviceID = "";
            string sStatus = "";
            string sWorkcode = "";

            //Archivo de prueba donde se guardan los registros
            const string newFileName = @"E:\tmp\TFT_Prueba_ZK.txt";
            int j = 1;
            System.IO.StreamWriter fs = new System.IO.StreamWriter(newFileName);

            //openFileDialog1.Filter = "1_attlog(*.dat)|*.dat";
            //openFileDialog1.FileName = "1_attlog.dat";//1 stands for one possible deviceid

            byte[] ArchivoEnBytes = { 0 };
            //Aqui se pone la ruta del archivo a leer
            string RutaArchivo = @"U:\Documents\HackZK\1_attlog.dat";

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            try
            {
                //FileStream stream = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Read);
                byDataBuf = File.ReadAllBytes(RutaArchivo);

                //iLength = Convert.ToInt32(stream.Length);
                iLength = Convert.ToInt32(byDataBuf.Length);

                int iStartIndex = 0;
                int iOneLogLength;//the length of one line of attendence log
                for (int i = iStartIndex; i < iLength - 2; i++)//iEnd+1 means the bytes count of the data except the checksum
                {
                    if (byDataBuf[i] == 13 && byDataBuf[i + 1] == 10)
                    {
                        iOneLogLength = (i + 1) + 1 - iStartIndex;
                        byte[] bySSRAttLog = new byte[iOneLogLength];
                        Array.Copy(byDataBuf, iStartIndex, bySSRAttLog, 0, iOneLogLength);

                        udisk.GetAttLogFromDat(bySSRAttLog, iOneLogLength, out sPIN2, out sTime_second, out sDeviceID, out sStatus, out sVerified, out sWorkcode);

                        /*
                         * ListViewItem list = new ListViewItem();
                        list.Text = sPIN;
                        list.SubItems.Add(sTime_second);
                        list.SubItems.Add(sDeviceID);
                        list.SubItems.Add(sStatus);
                        list.SubItems.Add(sVerified);
                        list.SubItems.Add(sWorkcode);
                        lvAttLog.Items.Add(list);
                         * */
                        fs.WriteLine("Registro " + j++);
                        fs.WriteLine("PIN " + sPIN2);
                        fs.WriteLine(sTime_second);
                        fs.WriteLine(sDeviceID);
                        fs.WriteLine(sStatus);
                        fs.WriteLine(sVerified);
                        fs.WriteLine(sWorkcode);
                        fs.WriteLine();

                        bySSRAttLog = null;
                        iStartIndex += iOneLogLength;
                        iOneLogLength = 0;
                    }
                }
                //stream.Close();
            }
            catch { }
        }
    }
}