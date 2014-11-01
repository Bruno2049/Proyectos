using System;
using System.Collections.Generic;
using System.Text;

namespace eClockSync
{
    public class CeC
    {
        public static DateTime FechaNula { get { return new DateTime(2006, 01, 01); } }
        public static DateTime FechaNull { get { return new DateTime(0001, 01, 01); } }
        //static bool UsarSleep = 0;
        public CeC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        /// <summary>
        /// Salto de linea con retorno de carro
        /// </summary>
        public static string SaltoLinea
        {
            get { return "\r\n"; }
        }

        /// <summary>
        /// Salto de linea con retorno de carro
        /// </summary>
        public static string SaltoLineaHtml
        {
            get { return "<br>"; }
        }
        /// <summary>
        /// Separa una linea de texto en columnas usando un caracter como delimitador
        /// </summary>
        /// <param name="Valores">Cadena de texto a separar</param>
        /// <param name="Separador">Caracter usado como separador</param>
        /// <param name="Columna">Número de la columna que se quiere obtener</param>
        /// <returns>Columna cuyo indice se indica en el parametro "Columna"</returns>
        public static string ObtenColumnaSeparador(string Valores, string Separador, int Columna)
        {
            string Ret = "";
            try
            {
                string[] sSeparador = new string[1];
                sSeparador[0] = Separador;

                string[] sValores = Valores.Split(sSeparador, StringSplitOptions.RemoveEmptyEntries);
                return sValores[Columna];
            }
            catch { }
            return Ret;
        }

        public static string FechaHora2Hora(DateTime objeto)
        {
            DateTime fnull = new DateTime(0001, 01, 01);
            DateTime fnula = new DateTime(2006, 01, 01);
            if (null == objeto)
                return "--:--";
            else
                if (fnull == objeto)
                    return "--:--";
                else if (fnula == objeto)
                    return "--:--";
                else
                    return objeto.ToString("HH:mm");

        }

        /// <summary>
        /// Separa en lineas de texto una cadena de caracteres (generalmente un archivo)
        /// </summary>
        /// <param name="Valores">Cadena o archivo a separar</param>
        /// <param name="Separador">Caracter usado como separador</param>
        /// <returns>Linea de texto</returns>
        public static string[] ObtenArregoSeparador(string Valores, string Separador)
        {
            return ObtenArregoSeparador(Valores, Separador, false);
        }

        /// <summary>
        /// Separa en lineas de texto una cadena de caracteres (generalmente un archivo)
        /// </summary>
        /// <param name="Valores">Cadena o archivo a separar</param>
        /// <param name="Separador">Caracter usado como separador</param>
        /// <param name="NoRemoverVacios">Indica si no debe remover lineas de texto vacias</param>
        /// <returns>Linea de texto</returns>
        public static string[] ObtenArregoSeparador(string Valores, string Separador, bool NoRemoverVacios)
        {
            string Ret = "";
            try
            {
                string[] sSeparador = new string[1];
                sSeparador[0] = Separador;
                StringSplitOptions SP = StringSplitOptions.RemoveEmptyEntries;
                if (NoRemoverVacios)
                    SP = StringSplitOptions.None;
                string[] sValores = Valores.Split(sSeparador, SP);
                return sValores;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Agrega un caracter como separador a dos cadenas
        /// </summary>
        /// <param name="Valores">Primera Cadena de caracteres para agregar separador</param>
        /// <param name="NuevoValor">Segunda Cadena de caracteres para agregar separador</param>
        /// <param name="Separador">Caracter usado como separador</param>
        /// <returns>Cadena de caracteres con el caracter usado como separador</returns>
        public static string AgregaSeparador(string Valores, string NuevoValor, string Separador)
        {
            if (Valores.Length > 0)
                Valores += Separador;
            return Valores + NuevoValor;
        }
        public static string AgregaSeparador(string Valores, List<int> NuevosValores, string Separador)
        {
            if (NuevosValores == null)
                return Valores;
            foreach (object NuevoValor in NuevosValores)
            {
                Valores = AgregaSeparador(Valores, NuevoValor.ToString(), Separador);
            }
            return Valores;
        }
        public static string AgregaSeparador(string Valores, List<Object> NuevosValores, string Separador)
        {
            if (NuevosValores == null)
                return Valores;
            foreach (object NuevoValor in NuevosValores)
            {
                Valores = AgregaSeparador(Valores, NuevoValor.ToString(), Separador);
            }
            return Valores;
        }

        /// <summary>
        /// Comprueba si existe la cadena de caracteres "Valor" en la cadena de caracteres "Valores" separada por "Separador"
        /// </summary>
        /// <param name="Valores">Cadena de caracteres</param>
        /// <param name="Valor">Cadena de caracteres buscada</param>
        /// <param name="Separador">Caracter usado como separador</param>
        /// <returns>TRUE si se encontro "Valor" en "Valores"</returns>
        public static bool ExisteEnSeparador(string Valores, string Valor, string Separador)
        {
            if (PosicionEnSeparador(Valores, Valor, Separador) >= 0)
                return true;
            return false;
        }

        public static int PosicionEnSeparador(string Valores, string Valor, string Separador)
        {
            try
            {
                string[] sSeparador = new string[1];
                sSeparador[0] = Separador;
                int Cont = 0;
                string[] sValores = Valores.Split(sSeparador, StringSplitOptions.RemoveEmptyEntries);
                foreach (string nValor in sValores)
                {
                    if (nValor.Trim() == Valor.Trim())
                        return Cont;
                    Cont++;
                }
            }
            catch { }
            return -1;
        }
        /// <summary>
        /// Convierte una variable objeto en una variable del tipo DateTime
        /// </summary>
        /// <param name="Valor">Objeto a convertir en fecha</param>
        /// <returns>Valor del tipo DateTime</returns>
        public static DateTime Convierte2DateTime(object Valor)
        {
            return Convierte2DateTime(Valor, FechaNula);
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo DateTime
        /// </summary>
        /// <param name="Valor">Objeto a convertir en fecha</param>
        /// <param name="Predeterminado">Valor predeterminado de fecha</param>
        /// <returns>Valor del tipo DateTime</returns>
        public static DateTime Convierte2DateTime(object Valor, DateTime Predeterminado)
        {
            try
            {
                return Convert.ToDateTime(Valor);
            }
            catch { }
            return Predeterminado;
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo DateTime
        /// </summary>
        /// <param name="Valor">Objeto a convertir en fecha</param>
        /// <param name="Predeterminado">Valor predeterminado de fecha</param>
        /// <returns>Valor del tipo DateTime</returns>
        public static DateTime? Convierte2DateTimeN(object Valor, DateTime? Predeterminado)
        {
            try
            {
                return Convert.ToDateTime(Valor);
            }
            catch { }
            return Predeterminado;
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo Int
        /// </summary>
        /// <param name="Valor">Objeto a convertir en entero</param>
        /// <returns>Valor del tipo entero</returns>
        public static int Convierte2Int(object Valor)
        {
            return Convierte2Int(Valor, 0);
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo Int
        /// </summary>
        /// <param name="Valor">Objeto a convertir en entero</param>
        /// <param name="Predeterminado">Valor predeterminado</param>
        /// <returns>Valor del tipo entero</returns>
        public static int Convierte2Int(object Valor, int Predeterminado)
        {
            try
            {
                return Convert.ToInt32(Valor);
            }
            catch { }
            return Predeterminado;
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo Short Int
        /// </summary>
        /// <param name="Valor">Objeto a convertir en entero corto</param>
        /// <returns>Valor del tipo entero corto</returns>
        public static short Convierte2Short(object Valor)
        {
            return Convierte2Short(Valor, 0);
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo Short Int
        /// </summary>
        /// <param name="Valor">Objeto a convertir en entero corto</param>
        /// <param name="Predeterminado">Valor predeterminado</param>
        /// <returns>Valor del tipo entero corto</returns>
        public static short Convierte2Short(object Valor, short Predeterminado)
        {
            try
            {
                return Convert.ToInt16(Valor);
            }
            catch { }
            return Predeterminado;
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo Bool
        /// </summary>
        /// <param name="Valor">Objero a convertir en booleano</param>
        /// <returns>Valor del tipo booleano</returns>
        public static bool Convierte2Bool(object Valor)
        {
            return Convierte2Bool(Valor, false);
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo Bool
        /// </summary>
        /// <param name="Valor">Objero a convertir en booleano</param>
        /// <param name="Predeterminado">Valor predeterminado</param>
        /// <returns>Valor del tipo booleano</returns>
        public static bool Convierte2Bool(object Valor, bool Predeterminado)
        {
            try
            {
                if (Valor.ToString() == "0")
                    return false;
                if (Valor.ToString() == "1")
                    return true;

                return Convert.ToBoolean(Valor);
            }
            catch { }
            return Predeterminado;
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo Double
        /// </summary>
        /// <param name="Valor">Objeto a convertir en Double</param>
        /// <returns>Valor del tipo Double</returns>
        public static double Convierte2Double(object Valor)
        {
            return Convierte2Double(Valor, 0);
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo Double
        /// </summary>
        /// <param name="Valor">Objeto a convertir en Double</param>
        /// <param name="Predeterminado">Valor predeterminado</param>
        /// <returns>Valor del tipo Double</returns>
        public static double Convierte2Double(object Valor, double Predeterminado)
        {
            try
            {
                return Convert.ToDouble(Valor);
            }
            catch { }
            return Predeterminado;
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo Decimal
        /// </summary>
        /// <param name="Valor">Objeto a convertir en Decimal</param>
        /// <returns>Valor del tipo  Decimal</returns>
        public static decimal Convierte2Decimal(object Valor)
        {
            return Convierte2Decimal(Valor, 0);
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo Decimal
        /// </summary>
        /// <param name="Valor">Objeto a convertir en Decimal</param>
        /// <param name="Predeterminado">Valor predeterminado</param>
        /// <returns>Valor del tipo Decimal</returns>
        public static decimal Convierte2Decimal(object Valor, decimal Predeterminado)
        {
            try
            {
                return Convert.ToDecimal(Valor);
            }
            catch { }
            return Predeterminado;
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo String
        /// </summary>
        /// <param name="Valor">Objeto a convertir en String</param>
        /// <returns>Valor del tipo String</returns>
        public static string Convierte2String(object Valor)
        {
            return Convierte2String(Valor, "");
        }

        /// <summary>
        /// Convierte una variable objeto en una variable del tipo String
        /// </summary>
        /// <param name="Valor">Objeto a convertir en String</param>
        /// <param name="Predeterminado">Valor predeterminado</param>
        /// <returns>Valor del tipo String</returns>
        public static string Convierte2String(object Valor, string Predeterminado)
        {
            try
            {
                return Convert.ToString(Valor);
            }
            catch { }
            return Predeterminado;
        }

        public static bool Convierte2Object(object Valor, object Destino)
        {
            try
            {
                switch (Destino.GetType().ToString())
                {
                    case "System.String":
                        Destino = Convierte2String(Valor);
                        break;
                    case "System.Int32":
                        Destino = Convierte2Int(Valor);
                        break;
                    case "System.Boolean":
                        Destino = Convierte2Bool(Valor);
                        break;
                    case "System.DateTime":
                        Destino = Convierte2DateTime(Valor);
                        break;
                    case "System.Decimal":
                        Destino = Convierte2Decimal(Valor);
                        break;
                    case "System.Double":
                        Destino = Convierte2Double(Valor);
                        break;
                }
                return true;
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// Convierte de Hexadecimal a Byte
        /// </summary>
        /// <param name="Hex">Valor hexadecimal a convertir</param>
        /// <returns>Valor del tipo Byte</returns>
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

        /// <summary>
        /// Convierte del tipo Byte a Hexadecimal
        /// </summary>
        /// <param name="Byte">Valor a convertir</param>
        /// <returns>Valor del tipo Hexadecimal</returns>
        public static byte Byte2HexByte(byte Byte)
        {
            if (Byte < 0)
                return Convert.ToByte('0');
            if (Byte > 16)
                return Convert.ToByte('F');
            if (Byte < 10)
                return Convert.ToByte(Byte + Convert.ToByte('0'));
            return Convert.ToByte(Byte - 10 + Convert.ToByte('A'));
        }

        /// <summary>
        /// Convierte un valor del tipo Hexadecimal al tipo Int
        /// </summary>
        /// <param name="Hex">Valor a convertir</param>
        /// <returns>Valor del tipo Int</returns>
        public static int Hex2Int(string Hex)
        {
            int valori = 0;
            if (Hex[0] == '#')
            {
                for (int Cont = 1; Cont < Hex.Length; Cont++)
                    valori = (valori * 16) + HexByte2Byte(Convert.ToByte(Hex[Cont]));
            }
            else
            {
                for (int Cont = 0; Cont < Hex.Length; Cont++)
                    valori = valori * 16 + HexByte2Byte(Convert.ToByte(Hex[Cont]));
            }
            return valori;
        }
        public static long Hex2Long(string Hex)
        {
            long valor = 0;
            if (Hex[0] == '#')
            {
                for (int Cont = 1; Cont < Hex.Length; Cont++)
                {
                    valor = (valor * 16) + HexByte2Byte(Convert.ToByte(Hex[Cont]));
                }
            }
            else
            {
                for (int Cont = 0; Cont < Hex.Length; Cont++)
                    valor = valor * 16 + HexByte2Byte(Convert.ToByte(Hex[Cont]));
            }
            return valor;
        }


        /// <summary>
        /// Convierte un valor del tipo Hexadecimal al tipo String
        /// </summary>
        /// <param name="Hex">Valor del tipo Hexadecimal</param>
        /// <returns>Valor del tipo String</returns>
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

        /// <summary>
        /// Convierte un valor del tipo Byte a representacion BCD
        /// </summary>
        /// <param name="Arreglo">Arreglo de Bytes</param>
        /// <param name="Pos">Posicion en el arreglo de Bytes</param>
        /// <param name="Len">Longitud del arreglo</param>
        /// <returns>Arreglo de bytes con la conversion a BCD</returns>
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
            return Res;
        }

        /// <summary>
        /// Convierte una cadena de caracteres en hexdecimal a representacion BCD
        /// </summary>
        /// <param name="TextoEnHexa">Texto en hexadecimal</param>
        /// <returns>Cadena de caracteres con la conversion a BCD</returns>
        public static byte[] String2Bcd(string TextoEnHexa)
        {
            byte[] Arreglo = ObtenArregloBytes(TextoEnHexa);
            if (Arreglo == null)
                return null;
            return Bytes2Bcd(Arreglo, 0, Arreglo.Length);
        }

        /// <summary>
        /// Convierte una representacion BCD en un tipo String
        /// </summary>
        /// <param name="Arreglo">Arreglo que contiene la representacion BCD</param>
        /// <param name="Pos">Posicion en el arreglo de Bytes</param>
        /// <param name="Len">Longitud del arreglo</param>
        /// <returns>Cadena de caracteres con la representacion BCD</returns>
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

        /// <summary>
        /// Obtine un arreglo de Bytes de una cadena de Bytes
        /// </summary>
        /// <param name="Arreglo">Arreglo de Bytes</param>
        /// <param name="Len">Longitud del arreglo</param>
        /// <returns>Arreglo de Bytes</returns>
        public static Byte[] ObtenArregloBytes(byte[] Arreglo, int Len)
        {
            if (Len > Arreglo.Length)
                Len = Arreglo.Length;
            if (Len < 0)
                return null;
            byte[] NArreglo = new byte[Len];
            for (int Cont = 0; Cont < Len; Cont++)
                NArreglo[Cont] = Arreglo[Cont];
            return NArreglo;
        }

        /// <summary>
        /// Obtiene un arreglo de Bytes de una cadena de caracteres
        /// </summary>
        /// <param name="Cadena">Cadena de caractes</param>
        /// <returns>Arreglo de Bytes</returns>
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

        /// <summary>
        /// Obtiene una cadena de caracteres de un arreglo de bytes
        /// </summary>
        /// <param name="Arreglo">Arreglo de bytes</param>
        /// <returns>Cadena de caracteres</returns>
        public static string ObtenString(byte[] Arreglo)
        {
            return ObtenString(Arreglo, 0);
        }

        /// <summary>
        /// Obtiene una cadena de caracteres de un arreglo de bytes
        /// </summary>
        /// <param name="Arreglo">Arreglo de bytes</param>
        /// <param name="Pos">Posicion en el arreglo de bytes</param>
        /// <returns>Cadena de caracteres</returns>
        public static string ObtenString(byte[] Arreglo, int Pos)
        {
            return ObtenString(Arreglo, Pos, 0);
        }

        /// <summary>
        /// Obtiene una cadena de caracteres de un arreglo de bytes
        /// </summary>
        /// <param name="Arreglo">Arreglo de bytes</param>
        /// <param name="Pos">Posicion en el arreglo de bytes</param>
        /// <param name="Len">Longitud del arreglo de bytes</param>
        /// <returns>Cadena de caracteres</returns>
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
            for (int Cont = 0; Cont < Len && Arreglo[Pos + Cont] != 0; Cont++)
            {
                Texto += (char)Arreglo[Pos + Cont];
            }
            return Texto;
        }

        /// <summary>
        /// Obtiene una cadena de caracteres de un String
        /// </summary>
        /// <param name="Arreglo">Cadena de caracteres</param>
        /// /// <param name="Pos">Posicion en el arreglo</param>
        /// <param name="Len">Longitud del arreglo</param>
        /// <returns>Cadena de caracteres</returns>
        public static string ObtenString(string Cadena, int Pos, int Len)
        {
            if (Cadena.Length < Pos + Len)
                Len = Cadena.Length - Pos;
            if (Len <= 0)
                return "";
            return Cadena.Substring(Pos, Len);

        }

        /// <summary>
        /// Reemplaza el subfijo y prefijo de una cadena, ej para cambiar convert(datetime,'01/01/2006 09:00',103) a TO_DATE('01/01/2006 09:00','DD/MM/YYYY HH24:MI:SS') ";
        /// </summary>
        /// <param name="CadenaOriginal"></param>
        /// <param name="PreFijo"></param>
        /// <param name="SubFijo"></param>
        /// <param name="NuevoPreFijo"></param>
        /// <param name="NuevoSubFijo"></param>
        /// <returns></returns>
        public static string Reemplaza(string CadenaOriginal, string PreFijo, string SubFijo, string NuevoPreFijo, string NuevoSubFijo)
        {
            return "";
        }
        /// <summary>
        /// Regresa una cadena entre parentesis
        /// </summary>
        /// <param name="Cadena"></param>
        /// <returns></returns>
        public static string Parentesis(string Cadena)
        {
            if (Cadena != null && Cadena != "")
                return "(" + Cadena + ")";
            return Cadena;
        }

        /// <summary>
        /// Compara la longitud de la Hora le quita los espacios y la convierte al formato predeterminado
        /// </summary>
        /// <param name="Hora">Hora</param>
        /// <returns>FechaHora, en otro caso devolvera FechaNula</returns>
        public static DateTime Hora2DateTime(string Hora)
        {
            DateTime FechaHora = FechaNula;
            if (Hora == null || Hora == "--:--")
                return FechaNula;
            if (Hora.Length > 5)
            {
                try
                {
                    Hora = Hora.Trim();
                    string[] Horas = Hora.Split(new char[] { ' ' });
                    string[] Elems = Horas[0].Split(new char[] { ':' });
                    int iHora = 0;
                    int iMinuto = 0;
                    int iSegundos = 0;
                    iHora = Convert.ToInt16(Elems[0]);
                    if (Elems.Length > 1)
                        iMinuto = Convert.ToInt16(Elems[1]);
                    if (Elems.Length > 2)
                        iSegundos = Convert.ToInt16(Elems[2]);
                    if (Horas.Length > 1)
                        if (Horas[1].ToUpper().Substring(0, 1) == "P")
                            iHora += 12;
                    FechaHora = FechaHora.AddHours(iHora);
                    FechaHora = FechaHora.AddMinutes(iMinuto);
                    FechaHora = FechaHora.AddSeconds(iSegundos);
                    return FechaHora;
                }
                catch (Exception ex)
                {

                    CeLog2.AgregaError(ex);
                }


                return FechaNula;
            }
            if (Hora.Length < 4)
                return FechaNula;
            if (Hora.Length < 5)
            {
                Hora = "0" + Hora;
            }
            FechaHora = FechaHora.AddHours(Convert.ToInt32(Hora.Substring(0, 2)));
            FechaHora = FechaHora.AddMinutes(Convert.ToInt32(Hora.Substring(3, 2)));
            return FechaHora;
        }

        /// <summary>
        /// Suma a FechaNula un intervalo de tiempo
        /// </summary>
        /// <param name="Tiempo">Intervalo de Tiempo</param>
        /// <returns></returns>
        public static DateTime TimeSpan2DateTime(TimeSpan Tiempo)
        {
            return FechaNula + Tiempo;
            //		return new DateTime(2006, 01, 01 + Tiempo.Days, Tiempo.Hours, Tiempo.Minutes, Tiempo.Seconds);
        }
        /// <summary>
        /// Compara la longitud de la Hora eliminando espacios y le decrementa la FechaNula
        /// </summary>
        /// <param name="Hora">Hora</param>
        /// <returns></returns>
        public static TimeSpan Hora2TimeSpan(string Hora)
        {
            return Hora2DateTime(Hora) - FechaNula;
        }
        /// <summary>
        /// Obtiene un instante de tiempo(fecha y hora) de un intervalo de tiempo dado
        /// </summary>
        /// <param name="TiempoDateTime"></param>
        /// <returns></returns>
        public static DateTime TimeSpanDate2DateTime(DateTime TiempoDateTime)
        {
            TimeSpan Tiempo = DateTime2TimeSpan(TiempoDateTime);
            return TimeSpan2DateTime(Tiempo);
        }
        /// <summary>
        /// Regresa el Tiempo menos el decremento de la FechaNula(01/01/2006)
        /// </summary>
        /// <param name="Tiempo">Tiempo</param>
        /// <returns></returns>
        public static TimeSpan DateTime2TimeSpan(DateTime Tiempo)
        {
            TimeSpan Ts = Tiempo - FechaNula;
            Ts = new TimeSpan(Ts.Days, Ts.Hours, Ts.Minutes, Ts.Seconds);
            return Ts;
        }
        /// <summary>
        /// Regresa un instante de tiempo(Fecha y Hora), de Tiempo menos la FechaNula(01/01/2006)
        /// </summary>
        /// <param name="Tiempo">Intervalo de Tiempo</param>
        /// <returns></returns>
        public static DateTime DateTime2TimeSpanDate(DateTime Tiempo)
        {
            return TimeSpan2DateTime(Tiempo - FechaNula);
        }
        public static int PersonaDiarioID2PersonaID(int PersonaDiarioID)
        {
            return PersonaDiarioID / 10000;
        }
        public static string PersonasDiarioIDs2PersonaIDs(string PersonasDiarioIDs)
        {
            string[] sPersonasDiarioIDS = ObtenArregoSeparador(PersonasDiarioIDs, ",");
            List<int> iPersonasIds = new List<int>();
            foreach (string sPersonaDiarioID in sPersonasDiarioIDS)
            {
                int PersonaID = PersonaDiarioID2PersonaID(Convierte2Int(sPersonaDiarioID));
                if (iPersonasIds.IndexOf(PersonaID) < 0)
                    iPersonasIds.Add(PersonaID);
            }

            return AgregaSeparador("", iPersonasIds, ",");
        }

        public static DateTime PersonaDiarioID2Fecha(int PersonaDiarioID)
        {
            int Res = PersonaDiarioID % 10000;
            int Ano = Res / 366;
            int Dia = Res % 366;
            return new DateTime(2000 + Ano, 1, 1).AddDays(Dia - 1);
        }

        public static List<DateTime> PersonasDiarioIDs2Fechas(string PersonasDiarioIDs)
        {
            string[] sPersonasDiarioIDS = ObtenArregoSeparador(PersonasDiarioIDs, ",");
            List<DateTime> dtFechas = new List<DateTime>();
            foreach (string sPersonaDiarioID in sPersonasDiarioIDS)
            {
                DateTime Fecha = PersonaDiarioID2Fecha(Convierte2Int(sPersonaDiarioID));
                if (dtFechas.IndexOf(Fecha) < 0)
                    dtFechas.Add(Fecha);
            }

            return dtFechas;
        }

        public static List<DateTime> PersonasDiarioIDs2Fechas(string PersonasDiarioIDs, int PersonaID)
        {
            string[] sPersonasDiarioIDS = ObtenArregoSeparador(PersonasDiarioIDs, ",");
            List<DateTime> dtFechas = new List<DateTime>();
            foreach (string sPersonaDiarioID in sPersonasDiarioIDS)
            {
                int PersonaDiarioID = Convierte2Int(sPersonaDiarioID);
                if (PersonaDiarioID2PersonaID(PersonaDiarioID) == PersonaID)
                {
                    DateTime Fecha = PersonaDiarioID2Fecha(PersonaDiarioID);
                    if (dtFechas.IndexOf(Fecha) < 0)
                        dtFechas.Add(Fecha);
                }
            }
            return dtFechas;
        }

        public static int PersonaID2PersonaDiarioID(int PersonaID, DateTime FechaPersonaDiario)
        {
            return PersonaID * 10000 + ((FechaPersonaDiario.Year - 2000) * 366) + FechaPersonaDiario.DayOfYear;
        }

        public static string PersonaID2PersonasDiarioIDs(int PersonaID, List<DateTime> FechasPersonaDiarios)
        {
            string PersonaDiariosIDs = "";
            foreach (DateTime FechaPersonaDiario in FechasPersonaDiarios)
            {
                PersonaDiariosIDs = CeC.AgregaSeparador(PersonaDiariosIDs, PersonaID2PersonaDiarioID(PersonaID, FechaPersonaDiario).ToString(), ",");
            }
            return PersonaDiariosIDs;
        }
        public static string PersonaID2PersonasDiarioIDs(int PersonaID, DateTime FechaDesdePersonaDiario, DateTime FechaHastaPersonaDiario)
        {
            string PersonaDiariosIDs = "";
            for (DateTime Fecha = FechaDesdePersonaDiario; Fecha <= FechaHastaPersonaDiario; Fecha = Fecha.AddDays(1))
            {
                PersonaDiariosIDs = CeC.AgregaSeparador(PersonaDiariosIDs, PersonaID2PersonaDiarioID(PersonaID, Fecha).ToString(), ",");
            }
            return PersonaDiariosIDs;
        }
        

        public static string Json2JsonList(string CadenaJson)
        {
            if (CadenaJson[0] != '[')
                CadenaJson = "[" + CadenaJson + "]";
            return CadenaJson;
        }

        public static decimal ObtenValor(TimeSpan ST, int TipoUnidadID, int TipoRedondeoID)
        {
            double R = 0;
            switch (TipoUnidadID)
            {
                case 0:
                    R = ST.TotalDays;
                    break;
                case 1:
                    R = ST.TotalHours;
                    break;
                case 2:
                    R = ST.TotalMinutes;
                    break;
                case 3:
                    R = ST.TotalSeconds;
                    break;
            }

            switch (TipoRedondeoID)
            {
                case 0:
                    R = Math.Floor(R);
                    break;
                case 1:
                    break;
                case 2://fracciones
                    R = Math.Ceiling(R);
                    break;
                case -1:
                    R = Math.Round(R);
                    break;
            }
            return Convierte2Decimal(R);
        }

        public static bool Compara(string Valor, string AComparar, bool Default)
        {
            if (Valor == null || Valor == "")
                return Default;
            if (Valor.Length < AComparar.Length)
                return false;
            if (Valor.Substring(0, AComparar.Length) == AComparar)
                return true;
            return false;
        }

        public static string AsignaParametro(string Qry, string Parametro, string Valor)
        {
            return Qry.Replace("@" + Parametro + "@", Valor);
        }

        public static bool Compara(object Objeto1, object Objeto2)
        {
            if (Objeto1 == null && Objeto2 != null || Objeto1 != null && Objeto2 == null)
                return false;
            if (ObtenValor(Objeto2, Objeto1.GetType()) == Objeto1)
                return true;
            if (Objeto1.GetType().Name == "String")
                if (Objeto1.ToString() == Objeto2.ToString())
                    return true;
            if (Objeto1.GetType().Name == "Double" || Objeto1.GetType().Name == "Int32")
            {
                double dObjeto1 = Convierte2Double(Objeto1);
                double dObjeto2 = Convierte2Double(Objeto2);
                if (Math.Round(dObjeto1, 3) == Math.Round(dObjeto2, 3))
                    return true;
            }

            return false;
        }

        public static object ObtenValor(object Origen, Type TypeDestino)
        {
            object Destino = null;
            try
            {

                switch (TypeDestino.ToString())
                {
                    case "System.String":
                        Destino = Convierte2String(Origen);
                        break;
                    case "System.Int32":
                        Destino = Convierte2Int(Origen);
                        break;
                    case "System.Boolean":
                        Destino = Convierte2Bool(Origen);
                        break;
                    case "System.DateTime":
                        Destino = Convierte2DateTime(Origen);
                        break;
                    case "System.Decimal":
                        Destino = Convierte2Decimal(Origen);
                        break;
                    case "System.Double":
                        Destino = Convierte2Double(Origen);
                        break;

                }
                return Destino;
            }
            catch
            {
            }
            return Destino;
        }

        public static string Amp2Pipe(string Url)
        {
            if (Url == null || Url == "")
                return "";
            string R = Url.Replace("&amp;", "|");
            R = R.Replace("&", "|");
            return R;
        }

        public static string Pipe2Amp(string Url)
        {
            if (Url == null || Url == "")
                return "";
            return Url.Replace("|", "&");
        }

    }
}
