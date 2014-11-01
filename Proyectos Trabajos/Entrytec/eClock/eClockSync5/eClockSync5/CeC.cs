using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Reflection;
using System.Reflection.Emit;

namespace eClockSync5
{
    /// <summary>
    /// Clase con funciones para manipulación de cadenas y conversiones de tipo y su manejo de errores
    /// </summary>
    public class CeC
    {
        public static DateTime FechaNula { get { return new DateTime(2006, 01, 01); } }
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
        /// Detiene el proceso actual x milisegundos para dar tiempo al procesador a que complete otras tareas
        /// </summary>
        /// <param name="Milisegundos">Tiempo en milisegundos</param>
        /// <returns>Verdadero si fue posible detener el proceso</returns>
        public static bool Sleep(int Milisegundos)
        {
            try
            {
                //if(UsarSleep
                System.Threading.Thread.Sleep(Milisegundos);
                return true;
            }
            catch (Exception ex)
            {
                System.Threading.Thread.ResetAbort();
                //CIsLog2.AgregaError(ex);

            }
            //CIsLog2.AgregaError("Recuperando de error en Sleep");

            return false;
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

        /// <summary>
        /// Comprueba si existe la cadena de caracteres "Valor" en la cadena de caracteres "Valores" separada por "Separador"
        /// </summary>
        /// <param name="Valores">Cadena de caracteres</param>
        /// <param name="Valor">Cadena de caracteres buscada</param>
        /// <param name="Separador">Caracter usado como separador</param>
        /// <returns>TRUE si se encontro "Valor" en "Valores"</returns>
        public static bool ExisteEnSeparador(string Valores, string Valor, string Separador)
        {
            try
            {
                string[] sSeparador = new string[1];
                sSeparador[0] = Separador;

                string[] sValores = Valores.Split(sSeparador, StringSplitOptions.RemoveEmptyEntries);
                foreach (string nValor in sValores)
                    if (nValor.Trim() == Valor.Trim())
                        return true;
            }
            catch { }
            return false;
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
            int Valor = 0;
            for (int Cont = 0; Cont < Hex.Length; Cont++)
                Valor = Valor * 16 + HexByte2Byte(Convert.ToByte(Hex[Cont]));
            return Valor;
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
        ///
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable(Object o)
        {
            PropertyInfo[] properties = o.GetType().GetProperties();
            DataTable dt = CreateDataTable(properties);
            FillData(properties, dt, o);
            return dt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable(Object[] array)
        {
            PropertyInfo[] properties = array.GetType().GetElementType().GetProperties();
            DataTable dt = CreateDataTable(properties);

            if (array.Length != 0)
            {
                foreach (object o in array)
                    FillData(properties, dt, o);

            }

            return dt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static DataTable CreateDataTable(PropertyInfo[] properties)
        {
            DataTable dt = new DataTable("Tabla");
            DataColumn dc = null;

            foreach (PropertyInfo pi in properties)
            {
                dc = new DataColumn();
                dc.ColumnName = pi.Name;
                dc.DataType = pi.PropertyType;

                dt.Columns.Add(dc);
            }

            return dt;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="dt"></param>
        /// <param name="o"></param>
        private static void FillData(PropertyInfo[] properties, DataTable dt, Object o)
        {
            DataRow dr = dt.NewRow();

            foreach (PropertyInfo pi in properties)
                dr[pi.Name] = pi.GetValue(o, null);

            dt.Rows.Add(dr);
        }


        public static bool ConvierteDataRow2Object(DataRow Fila, object ObjetoDestino, bool EsNuevo)
        {
            try
            {

                PropertyInfo[] Propiedades = ObjetoDestino.GetType().GetProperties();

                if (Propiedades == null || Propiedades.Length < 1)
                {
                    return false;
                }



                for (int Cont = 0; Cont < Propiedades.Length; Cont++)
                {
                    if (Propiedades[Cont].CanWrite)
                    {
                        object Dato = Propiedades[Cont].GetValue(ObjetoDestino, null);
                        if (ObtenValorDR(Fila, Propiedades[Cont].Name, ref Dato))
                        {
                            Propiedades[Cont].SetValue(ObjetoDestino, Dato, null);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //CIsLog2.AgregaError(ex);
            }
            return false;
        }


        #region ObtenValorDRes
        /// <summary>
        /// Lee los valores en caso de ser de tipo string,int,bool,datetime
        /// </summary>
        /// <param name="Variable"></param>
        /// <param name="Destino"></param>
        /// <returns></returns>
        public static bool ObtenValorDR(DataRow Fila, string Variable, ref object Destino)
        {
            try
            {
                if (Destino == null)
                {
                    switch (Fila[Variable].GetType().ToString())
                    {
                        case "System.String":
                            Destino = ObtenValorDR(Fila, Variable,"");
                            break;
                        case "System.Int32":
                            Destino = ObtenValorDR(Fila, Variable, -99999);
                            break;
                        case "System.Boolean":
                            Destino = ObtenValorDR(Fila, Variable, false);
                            break;
                        case "System.DateTime":
                            Destino = ObtenValorDR(Fila, Variable, FechaNula);
                            break;
                        case "System.Decimal":
                            Destino = ObtenValorDR(Fila, Variable,Convert.ToDecimal(-99999));
                            break;
                        case "System.Double":
                            Destino = ObtenValorDR(Fila, Variable, Convert.ToDouble(-99999));
                            break;
                    }
                }
                switch (Destino.GetType().ToString())
                {
                    case "System.String":
                        Destino = ObtenValorDR(Fila, Variable, Convert.ToString(Destino));
                        break;
                    case "System.Int32":
                        Destino = ObtenValorDR(Fila, Variable, Convert.ToInt32(Destino));
                        break;
                    case "System.Boolean":
                        Destino = ObtenValorDR(Fila, Variable, Convert.ToBoolean(Destino));
                        break;
                    case "System.DateTime":
                        Destino = ObtenValorDR(Fila, Variable, Convert.ToDateTime(Destino));
                        break;
                    case "System.Decimal":
                        Destino = ObtenValorDR(Fila, Variable, Convert.ToDecimal(Destino));
                        break;
                    case "System.Double":
                        Destino = ObtenValorDR(Fila, Variable, Convert.ToDouble(Destino));
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
        /// Obtiene el valor string de el data row seleccionado
        /// </summary>
        /// <param name="Variable"></param>
        /// <param name="ValorPredeterminado"></param>
        /// <returns></returns>
        public static string ObtenValorDR(DataRow Fila, string Variable, string ValorPredeterminado)
        {
            return CeC.Convierte2String(Fila[Variable], ValorPredeterminado);
        }
        /// <summary>
        /// Obtiene el valor de configuracion
        /// </summary>
        /// <param name="Variable"></param>
        /// <param name="ValorPredeterminado"></param>
        /// <returns></returns>
        public static int ObtenValorDR(DataRow Fila, string Variable, int ValorPredeterminado)
        {
            return CeC.Convierte2Int(Fila[Variable], ValorPredeterminado);
        }
        /// <summary>
        /// Obtiene el valor de configuracion
        /// </summary>
        /// <param name="Variable"></param>
        /// <param name="ValorPredeterminado"></param>
        /// <returns></returns>
        public static bool ObtenValorDR(DataRow Fila, string Variable, bool ValorPredeterminado)
        {
            return CeC.Convierte2Bool(Fila[Variable], ValorPredeterminado);
        }
        /// <summary>
        /// Obtiene el valor de configuracion
        /// </summary>
        /// <param name="Variable"></param>
        /// <param name="ValorPredeterminado"></param>
        /// <returns></returns>
        public static DateTime ObtenValorDR(DataRow Fila, string Variable, DateTime ValorPredeterminado)
        {
            return CeC.Convierte2DateTime(Fila[Variable], ValorPredeterminado);
        }
        /// <summary>
        /// Obtiene el valor de configuracion
        /// </summary>
        /// <param name="Variable"></param>
        /// <param name="ValorPredeterminado"></param>
        /// <returns></returns>
        public static Decimal ObtenValorDR(DataRow Fila, string Variable, Decimal ValorPredeterminado)
        {
            return CeC.Convierte2Decimal(Fila[Variable], ValorPredeterminado);
        }
        /// <summary>
        /// Obtiene el valor de configuracion
        /// </summary>
        /// <param name="Variable"></param>
        /// <param name="ValorPredeterminado"></param>
        /// <returns></returns>
        public static Double ObtenValorDR(DataRow Fila, string Variable, Double ValorPredeterminado)
        {
            return CeC.Convierte2Double(Fila[Variable], ValorPredeterminado);
        }
        #endregion

    }
}