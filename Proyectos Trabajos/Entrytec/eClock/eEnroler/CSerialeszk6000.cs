using System;
using System.Collections.Generic;
using System.Text;



    class CSerialeszk6000
    {
        string[] Seriales = null;
        public CSerialeszk6000()
        {
            Seriales = ObtenerSeriales();
        }
        public string[] ObtenerSeriales()
        {
            if (System.IO.File.Exists("c:\\windows\\serial.cfg"))
                return System.IO.File.ReadAllLines("c:\\windows\\serial.cfg");
            else
                return new string[0];
        }
        public static string CalculaHash(string Texto)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider Sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            string HashSR = BitConverter.ToString(Sha1.ComputeHash(new System.IO.MemoryStream(System.Text.ASCIIEncoding.Default.GetBytes(Texto))));
          //  string HashSR = BitConverter.ToString(Sha1.ComputeHash(new System.IO.MemoryStream(new byte[]{1,2,3}) ));
            return HashSR;
        }
        string Validada(string Cadena)
        {
            string Ret = "";
            Cadena = Cadena.ToUpper();
            foreach (char Caracter in Cadena)
            {
                if (Caracter >= '0' && Caracter <= '9')
                    Ret += Caracter;
                if (Caracter >= 'A' && Caracter <= 'F')
                    Ret += Caracter;

            }
            return Ret;
        }
        public bool esValido(string Serial)
        {
            string m_Serial = Validada(Serial);
          //  System.Windows.Forms.MessageBox.Show(Serial);
            Serial = CalculaHash(m_Serial);
           // System.Windows.Forms.MessageBox.Show(Serial);
            if (Seriales.Length > 0)
            {
                for (int x = 0; x < Seriales.Length; x++)
                {
             //       System.Windows.Forms.MessageBox.Show(Seriales[x]);
                    if (Serial.CompareTo(Seriales[x]) == 0)
                        return true;
                }
            }
            return false;
        }

    }
   
