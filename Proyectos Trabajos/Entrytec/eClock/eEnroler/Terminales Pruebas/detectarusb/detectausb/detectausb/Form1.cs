using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//Incluir esto es obligatorio  
 using System.Runtime.InteropServices;  
  
namespace detectausb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

         /* 
  * Escribe aquí el código de tu programa (¡O donde prefieras!)… 
  */  
   
 //Estructura de datos que almacena la gestión de conexiones  
   
 [StructLayout(LayoutKind.Sequential)]  
 public struct DEV_BROADCAST_VOLUME  
 {  
     public int dbcv_size;  
     public int dbcv_devicetype;  
     public int dbcv_reserved;  
     public int dbcv_unitmask;  
 }  
   
 //Método a sobreescribir para gestionar la llegada de nuevas unidades de disco  
 protected override void WndProc(ref Message m)  
 {  
     //Estas definiciones están en dbt.h y winuser.h  
     //Se ha producido un cambio en los dispositivos  
     const int WM_DEVICECHANGE = 0219;  
     // El sistema detecta un nuevo dispositivo  
     const int DBT_DEVICEARRIVAL = 8000;  
     //Solicita retirada del dispositivo  
     const int DBT_DEVICEQUERYREMOVE = 8001;  
     //Ha fallado la retirada del dispositivo  
     const int DBT_DEVICEQUERYREMOVEFAILED = 8002;  
     //Pendiente extracción del dispositivo  
     const int DBT_DEVICEREMOVEPENDING = 8003;  
     //Dispositivo extraído del sistema  
     const int DBT_DEVICEREMOVECOMPLETE =8004;  
     // Volumen lógico (Se ha insertado un disco)  
     const int DBT_DEVTYP_VOLUME = 00000002;  
     switch (m.Msg)  
     {  
         //Cambian los dispositivos del sistema  
         case WM_DEVICECHANGE:  
         switch (m.WParam.ToInt32())  
         {  
             //Llegada de un dispositivo  
             case DBT_DEVICEARRIVAL:  
             {  
                 int devType = Marshal.ReadInt32(m.LParam, 4);  
                 //Si es un volumen lógico..(unidad de disco)  
                 if (devType == DBT_DEVTYP_VOLUME)  
                 {  
                     DEV_BROADCAST_VOLUME vol;  
                     vol = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(  
                         m.LParam, typeof(DEV_BROADCAST_VOLUME));  
                     MessageBox.Show("Insertada unida de disco, unidad:" + LetraUnidad(vol.dbcv_unitmask));  
                 }  
             }  
             break;  
             case DBT_DEVICEREMOVECOMPLETE:  
                 MessageBox.Show("Dispositivo retirado.");  
                 break;  
         }  
         break;  
     }  
     //Ahora usar el manejador predeterminado  
     base.WndProc(ref m);  
 }  
   
 //Método para detectar la letra de unidad  
 char LetraUnidad(int unitmask)  
 {  
     char[] units ={ 'A', 'B', 'C', 'D', 'E', 'F', 'G',  
         'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',  
         'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };  
     int i = 0;  
     //Convetimos la máscara en un array primario y buscamos  
     //el índice de la primera ocurrencia (la letra de unidad)  
     System.Collections.BitArray ba = new  
         System.Collections.BitArray(System.BitConverter.GetBytes(unitmask));  
     foreach (bool var in ba)  
     {  
         if (var == true)  
             break;  
         i++;  
     }  
     return units[i];  
 }  

    }

}