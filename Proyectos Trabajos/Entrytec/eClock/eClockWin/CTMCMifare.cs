using System;
using System.Collections.Generic;
using System.Text;

namespace eClockWin
{
    class CTMCMifare
    {
        System.IO.Ports.SerialPort m_PuertoSerie = null;
        public uint m_TimeOutRX = 1000;
        uint m_TimeOutTX = 1000;
        public bool CierraPuerto()
        {
            if (m_PuertoSerie != null)
            {
                m_PuertoSerie.Close();
                m_PuertoSerie.Dispose();
                m_PuertoSerie = null;
                return true;
            }
            return false;
        }
        public bool IniciaPuerto(int PuertoSerie)
        {
            try
            {
                m_PuertoSerie = new System.IO.Ports.SerialPort("COM" + PuertoSerie, 57600, System.IO.Ports.Parity.None, 8);
                m_PuertoSerie.Open();

                return true;
            }
            catch { }
            return false;
        }
        public bool LimpiaBuffer()
        {
            if (m_PuertoSerie == null)
                return false;
            if (m_PuertoSerie.BytesToWrite > 0)
            {
//                CeTLog2.AgregaErrorMsg(" m_PuertoSerie.BytesToWrite > 0 = " + m_PuertoSerie.BytesToWrite.ToString());
                m_PuertoSerie.DiscardOutBuffer();
                //                m_PuertoSerie.
            }


#if WindowsCE                        
                while (m_PuertoSerie.BytesToRead > 0)
                    m_PuertoSerie.ReadByte();
            
#else
            m_PuertoSerie.DiscardInBuffer();
#endif
            return true;
        }

        public void Sleep(int Milisegundos)
        {
            System.Threading.Thread.Sleep(Milisegundos);
        }

        byte[] CadenaLimpia()
        {
            return new byte[1];
        }
        byte[] Byte2ByteArray(byte B)
        {
            byte[] BComando = new byte[1];
            BComando[0] = B;
            return BComando;
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
        public static string ObtenString(byte[] Arreglo)
        {
            return ObtenString(Arreglo, 0);
        }
        public static string ObtenString(byte[] Arreglo, int Pos)
        {
            return ObtenString(Arreglo, Pos, 0);
        }
        public static string ObtenString(byte[] Arreglo, int Pos, int Len)
        {
            string Texto = "";
            if (Len < 0)
                Len = 0;
            if (Pos > Arreglo.Length)
                return "";
            if (Pos < 0)
                Pos = 0;

            if (Len <= 0 || Pos + Len > Arreglo.Length)
                Len = Arreglo.Length - Pos;
            for (int Cont = 0; Cont < Len; Cont++)
            {
                Texto += (char)Arreglo[Pos + Cont];
            }
            return Texto;
        }
        public static byte Byte2HexByte(byte Hex)
        {
            if (Hex < 0)
                return Convert.ToByte('0');
            if (Hex > 16)
                return Convert.ToByte('F');
            if (Hex < 10)
                return Convert.ToByte(Hex + Convert.ToByte('0'));
            return Convert.ToByte(Hex - 10 + Convert.ToByte('A'));
        }
        public static byte HexByte2Byte(byte Hex)
        {
            if (Hex < Convert.ToByte('0'))
                return 0;
            if (Hex > Convert.ToByte('f'))
                return 16;

            if (Hex >= Convert.ToByte('a') && Hex <= Convert.ToByte('f'))
                return Convert.ToByte(Hex - Convert.ToByte('a') + 10);
            if (Hex >= Convert.ToByte('A') && Hex <= Convert.ToByte('F'))
                return Convert.ToByte(Hex - Convert.ToByte('A') + 10);
            if (Hex >= Convert.ToByte('0') && Hex <= Convert.ToByte('9'))
                return Convert.ToByte(Hex - Convert.ToByte('0'));
            return 0;
        }

        public static string HexByte2String(byte Hex)
        {
            if (Hex < 0)
                return "0";
            if (Hex > 16)
                return "F";
            byte[] Car = new byte[1];
            Car[0] = Convert.ToByte('0');
            int Numero = 0;
            if (Hex < 10)
                Numero = Hex + Car[0];

            Car[0] = Convert.ToByte('A');
            if (Hex >= 10)
                Numero = (Hex - 10) + Car[0];
            string Str = "";
            Str += Convert.ToChar(Numero);
            return Str;
        }
        public static byte[] Bcd2Bytes(byte[] Arreglo, int Pos, int Len)
        {
            if (Arreglo == null)
                return null;
            if (Pos > Arreglo.Length)
                return null;
            if (Pos + Len > Arreglo.Length)
                Len = Arreglo.Length - Pos;
#if DEBUG_HID            
            CeTLog2.AgregaDEBUG_HIDF("Bcd2Bytes", "Arreglo = " + BitConverter.ToString(Arreglo));
#endif
            byte[] Res = new byte[Len * 2];
            for (int Cont = Pos; Cont < Len; Cont++)
            {
                Res[Cont * 2] = Byte2HexByte(Convert.ToByte(Arreglo[Cont] / 16));
                Res[Cont * 2 + 1] = Byte2HexByte(Convert.ToByte(Arreglo[Cont] % 16));
            }
            return Res;
        }
        public static byte[] String2Bcd(string TextoEnHexa)
        {
            byte[] Arreglo = ObtenArregloBytes(TextoEnHexa);
            if (Arreglo == null)
                return null;
            return Bytes2Bcd(Arreglo, 0, Arreglo.Length);
        }
        public static byte[] Bytes2Bcd(byte[] Arreglo, int Pos, int Len)
        {
            if (Arreglo == null)
                return null;
            if (Pos > Arreglo.Length)
                return null;
            if (Pos + Len > Arreglo.Length)
                Len = Arreglo.Length - Pos;
            if (Len % 2 > 0)
                Len -= 1;
            byte[] Res = new byte[Len / 2];
            for (int Cont = Pos; Cont < Len; Cont += 2)
            {
                Res[Cont / 2] = Convert.ToByte(HexByte2Byte(Arreglo[Cont]) * 16);
                if (Cont + 1 < Len)
                    Res[Cont / 2] += HexByte2Byte(Arreglo[Cont + 1]);
            }
#if DEBUG_HID
            CeTLog2.AgregaDEBUG_HIDF("Bytes2Bcd", "    Res = " +  BitConverter.ToString(Res));
#endif
            return Res;
        }
        public static string Bcd2String(byte[] Arreglo, int Pos, int Len)
        {
            string Texto = "";
            if (Len < 0)
                Len = 0;
            if (Pos > Arreglo.Length)
                return "";
            if (Pos < 0)
                Pos = 0;

            if (Len <= 0 || Pos + Len > Arreglo.Length)
                Len = Arreglo.Length - Pos;
            for (int Cont = 0; Cont < Len; Cont++)
            {
                Texto += HexByte2String(Convert.ToByte(Arreglo[Pos + Cont] / 16)) + HexByte2String(Convert.ToByte(Arreglo[Pos + Cont] % 16));

            }
            return Texto;
        }
        public int Envia(byte[] Cadena, uint Len)
        {
            if (m_PuertoSerie == null) return 0;
            //System.Threading.Thread.Sleep(1000);
            //LimpiaBuffer();
#if DEBUG_HID
            AgregaLog("Envia", "Cadena " + Bcd2String(Cadena, 0, Convert.ToInt32(Len)) + " Len = " + Len.ToString());
#endif
            m_PuertoSerie.Write(Cadena, 0, Convert.ToUInt16(Len));

            return 1;
        }
        public int Envia(string Cadena)
        {
            if (m_PuertoSerie == null) return 0;
            //System.Threading.Thread.Sleep(1000);
            //LimpiaBuffer();
#if DEBUG_HID
            AgregaLog("Envia", "Cadena " + Bcd2String(Cadena, 0, Convert.ToInt32(Len)) + " Len = " + Len.ToString());
#endif
            m_PuertoSerie.Write(Cadena);            
            return 1;
        }
        public string LeeLinea()
        {
            try
            {
                DateTime DT = DateTime.Now;
                if (m_PuertoSerie == null) return "";
                m_PuertoSerie.ReadTimeout = Convert.ToInt16(m_TimeOutRX);
                string Texto = "";
                while(true)
                {
                    //Texto = new string(
                    System.Threading.Thread.Sleep(1);
                    if (m_PuertoSerie.BytesToRead > 0)
                    {
                        string Res = m_PuertoSerie.ReadExisting();
                        Texto += Res;
                        for (int Cont = 0; Cont < Res.Length; Cont++)
                        {
                            if (Res[Cont] == 13)
                                return Texto;
                        }
                    }
                    if (((TimeSpan)(DateTime.Now - DT)).TotalMilliseconds >= m_TimeOutRX)
                    {
                        break;
                    }
                }
                return Texto;
            }
            catch { }
            return "";
        }
        public string EnviaEspera(string Cadena)
        {
            LimpiaBuffer();
            if (Envia(Cadena) > 0)
                return LeeLinea();
            return "";
        }
        /// <summary>
        /// Permite escribir en un bloque determinado texto
        /// </summary>
        /// <param name="Bloque">
        /// Numero de bloque a escribir,  the  blocks  are  numbered  in  sequence 
        /// starting from 00 
        /// The following value can be used, according to the card type:  
        /// MIFARE® 1K block from 00 to 3F 
        /// MIFARE® 4K block from 00 to FF 
        /// SR176 block from 00 to 0F 
        /// SRIX4K block from 00 to 7F </param>
        /// <param name="Valor">Texto que será guardado en el bloque</param>
        /// <returns>"Correcto" si se guardo apropiadamente de lo contrario el mensaje de error</returns>
        public string Comando_WT(byte Bloque, string Valor)
        {
            string BloqueTexto = "wt" + HexByte2String(Convert.ToByte(Bloque / 16)) + HexByte2String(Convert.ToByte(Bloque % 16)) + Valor + "\r" ;
            string R = EnviaEspera(BloqueTexto);
            if (R == "")
                return "No existio respuesta";
            R = R.Trim();
            if (R == Valor.Trim())
                return "Correcto";            
            if(R == "N")
                return "No hay tarjeta o no se encontro";
            if (R == "F")
                return "Error al escribir";
            if (R == "I")
                return "Bloque invalido";
            if (R == "U")
                return "Error en la lectura posterior a la escritura";
            if (R == "F")
                return "Imposible leer despues de la escritura";
            return "Desconocido";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NoParametro"></param>
        /// <returns></returns>
        public int Comando_RP(byte NoParametro)
        {
            try
            {
                string BloqueTexto = "rp" + HexByte2String(Convert.ToByte(NoParametro / 16)) + HexByte2String(Convert.ToByte(NoParametro % 16));

                string R = EnviaEspera(BloqueTexto).Trim();
                if (R == "F")
                    return -1;
                byte[] Res = String2Bcd(R);
                return Res[0];
            }
            catch { }
            return -2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>El UID or el dato configurado para ser leido automaticamente
        /// o de lo contrario N para "No hay tarjeta o no se encontro"
        /// F "Error en la configuracion de autolectura"</returns>
        public string Comando_S()
        {
            string R = EnviaEspera("s");
                return R.Trim();

        }
    }
}
