using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;

namespace eClockSync
{
    class CeTerminalSync
    {
        string sLog = "";
        public WS_eCheck.WS_eCheck ws_eCheck = null;
        protected int Terminal_ID = 0;
        public CeC_Terminales m_TConexion = new CeC_Terminales();
        protected TimeSpan m_DiferenciaFechaHora = new TimeSpan(0, 0, 0);
        private TimeSpan m_TimeZoneRemota;

        protected TimeSpan TimeZoneRemota
        {
            get { return m_TimeZoneRemota; }
            set { 
                m_TimeZoneRemota = value; 
                CambioTimeZoneRemota(); 
            }
        }
        protected virtual void CambioTimeZoneRemota()
        {

        }
        protected DateTime FechaHora
        {
            get { return DateTime.Now - m_DiferenciaFechaHora; }
        }
        protected TimeSpan Hora
        {
            get { return FechaHora.TimeOfDay; }
        }
        protected string ObtenParametro(string Parametro, string ValorPredeterminado)
        {
            return m_ws_eCheck.ObtenParametro(Terminal_ID, Parametro, ValorPredeterminado);
        }

        protected bool AsignaParametro(string Parametro, string Valor)
        {
            return m_ws_eCheck.AsignaParametro(Terminal_ID, Parametro, Valor);
        }
        protected int ObtenParametro(string Parametro, int ValorPredeterminado)
        {
            string Valor = ObtenParametro(Parametro, ValorPredeterminado.ToString());
            try
            {
                return Convert.ToInt32(Valor);
            }
            catch { }
            return ValorPredeterminado;
        }

        protected bool AsignaParametro(string Parametro, int Valor)
        {
            return AsignaParametro(Parametro, Valor.ToString());
        }

        /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo de huellas o vectores enrolados
        /// </summary>
        protected string IUVectoresEnrollNuevo = "";
        /// <summary>
        /// Contiene la longitud  o HASH del ultimo archivo de huellas o vectores enrolados
        /// </summary>
        protected string IUVectoresEnroll
        {
            get { return ObtenParametro("IUVectoresEnroll", ""); }
            set { AsignaParametro("IUVectoresEnroll", value); }
        }


        /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo de huellas o vectores
        /// </summary>
        protected string IUVectoresNuevo = "";
        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo de huellas o vectores transmitidos
        /// </summary>
        protected string IUVectores
        {
            get { return ObtenParametro("IUVectores", ""); }
            set { AsignaParametro("IUVectores", value); }
        }

        /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo de NO huellas o vectores
        /// </summary>
        protected string IUVectoresNoNuevo = "";

        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo de NO huellas o vectores
        /// </summary>
        protected string IUVectoresNo
        {
            get { return ObtenParametro("IUVectoresNo", ""); }
            set { AsignaParametro("IUVectoresNo", value); }
        }


        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo de checadas
        /// </summary>
        protected string IUChecadasNuevo = "";

        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo de checadas
        /// </summary>
        protected string IUChecadas
        {
            get { return ObtenParametro("IUChecadas", ""); }
            set { AsignaParametro("IUChecadas", value); }
        }

        /// <summary>
        /// Indica si permitirá reemplazar vectores
        /// </summary>
        protected bool REEMPLAZAR_VECTORES
        {
            get { return Convert.ToBoolean(Convert.ToInt32(ObtenParametro("REEMPLAZAR_VECTORES", "0"))); }
            set { AsignaParametro("REEMPLAZAR_VECTORES", Convert.ToInt32(value).ToString()); }
        }

        /// <summary>
        /// Obtiene o establece el Sitio Hijo, si tiene sitio hijo significa
        /// que es controladora de sitio.
        /// </summary>
        protected int SITIO_HIJO_ID
        {
            get { return Convert.ToInt32(ObtenParametro("SITIO_HIJO_ID", "0")); }
            set { AsignaParametro("SITIO_HIJO_ID", Convert.ToString(value)); }
        }

        public bool TERMINAL_ESENTRADA
        {
            get { return Convert.ToBoolean(ObtenParametro("TERMINAL_ESENTRADA", 0)); }
            set { AsignaParametro("TERMINAL_ESENTRADA", Convert.ToInt32(value).ToString()); }
        }
        public bool TERMINAL_ESSALIDA
        {
            get { return Convert.ToBoolean(ObtenParametro("TERMINAL_ESSALIDA", 0)); }
            set { AsignaParametro("TERMINAL_ESSALIDA", Convert.ToInt32(value).ToString()); }
        }
        /// <summary>
        /// Indica si se aceptarán los tipos de acceso en equipos ZK software y Anviz
        /// </summary>
        public bool TERMINAL_ACEPTA_TA
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(ObtenParametro("TERMINAL_ACEPTA_TA", 0));
                }
                catch { }
                return false;
            }
            set { AsignaParametro("TERMINAL_ACEPTA_TA", Convert.ToInt32(value).ToString()); }
        }

        public bool TERMINAL_TARJETA_O_HUELLA
        {
            get { return Convert.ToBoolean(Convert.ToInt32(ObtenParametro("TERMINAL_TARJETA_O_HUELLA", "0"))); }
            set { AsignaParametro("TERMINAL_TARJETA_O_HUELLA", Convert.ToInt32(value).ToString()); }
        }

        public string CalculaHashArchivo(string Archivo)
        {
            try
            {
                System.Security.Cryptography.SHA1CryptoServiceProvider Sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                Byte[] Bytes = System.IO.File.ReadAllBytes(Archivo);
                string HashSR = BitConverter.ToString(Sha1.ComputeHash(Bytes));
                return HashSR;
            }
            catch
            {

            }
            return "";
        }

        public void AgregaError(Exception ex)
        {
            sLog += "Error ->Message:" + ex.Message + "\nError ->Source:" + ex.Source + "\n";
            CeLog2.AgregaError(ex);
            AgregaError("CeLog2.AgregaError(ex)");
        }


        public void AgregaLog(string Mensage)
        {
            sLog += Mensage + " " + DateTime.Now + "\n";
            CeLog2.AgregaLog(Descripcion + Mensage);
        }
        public string Descripcion
        {
            get
            {
                try
                { return m_DatosTerminal.TERMINAL_ID + " (" + m_DatosTerminal.SITIO_ID + ") -" + m_DatosTerminal.TERMINAL_DIR + "- "; }
                catch { }
                return "";

            }
        }
        public void AgregaError(string Mensage)
        {
            try
            {
                sLog += "Error ->" + Mensage + " " + DateTime.Now + "\n";
                CeLog2.AgregaError(Descripcion + Mensage);
            }
            catch (Exception ex)
            {
                CeLog2.AgregaErrorMsg(ex);
            }
        }

        public void AgregaDebug(string Mensage)
        {
#if DEBUG
            CeLog2.AgregaDebug(Descripcion + Mensage);
#endif
        }
        public void AgregaDebug(string Mensage, params object[] Parametros)
        {
#if DEBUG

            CeLog2.AgregaDebug(Descripcion + Mensage, Parametros);
#endif

        }
        protected enum ModeloTerminales
        {
            No_definido = 0,
            TRAX,
            HandPunch,
            AC215,
            AC115,
            EnTRAX,//5
            Lector_de_Huella_USB,
            Lector_de_Huella_MS,
            Lector_6055,
            Lector_Windows_Chip,
            Lector_Serial_CB_BM,//10
            UlTRAX,
            Prox,
            SuperMax,
            Max,
            NeoMax,//15
            Camara_Usb,
            Camara_Intellinet,
            Panel_Plumas,
            BioEntryPlus,
            BioStation,//20
            ZkSoftwareiClock,
            eClock,
            ZkSoftware,
            ZkSoftwarePull
        }
        public enum TipoAccesos
        {
            No_definido = 0,
            Correcto,
            Entrada,
            Salida,
            Salida_a_Comer,
            Regreso_de_comer,
            Incorrecto
        }
        public enum Tipo_DEXTRAS
        {
            No_definido = 0,
            Accesos,
            Huellas
        }

        protected enum Tecnologias
        {
            Ninguna = 0,
            Código_de_Barras,
            Banda_Magnética,
            Proximidad,
            Mifare,
            Huella,
            Palma,
            Chip_de_contacto,
            Rostro
        }

        public enum Errores
        {
            Correcto = 0,
            Error_Desconocido,
            Error_Parametros,
            Error_IO,
            Error_Conexion,
            No_Requiere_Cambio_En_Vectores,
            No_Requiere

        };
        protected WS_eCheck.DS_WSPersonasTerminales.DT_VectoresDataTable m_Vectores = null;
        protected WS_eCheck.DS_WSPersonasTerminales.EC_SITIOSRow m_DatosSitio = null;
        public WS_eCheck.DS_WSPersonasTerminales.EC_SITIOSRow DatosSitio
        {
            get { return m_DatosSitio; }
            set { m_DatosSitio = value; }
        }
        protected bool m_Conectado = false;
        public static string RutaTemporal = "";
        protected string Directorio;
        public WS_eCheck.DS_WSPersonasTerminales.DT_CHECADASDataTable m_Checadas = null;
        protected WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESRow m_DatosTerminal = null;
        protected WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesDataTable m_DatosEmpleados = null;
        protected WS_eCheck.WS_eCheck m_ws_eCheck = null;
        public static CeTerminalSync NuevaClase(WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESRow DatosTerminal)
        {
            CeLog2.AgregaLog("MODELO_TERMINAL_ID = " + DatosTerminal.MODELO_TERMINAL_ID + ", " + ((ModeloTerminales)DatosTerminal.MODELO_TERMINAL_ID).ToString());
            switch ((ModeloTerminales)DatosTerminal.MODELO_TERMINAL_ID)
            {
                case ModeloTerminales.EnTRAX:
                case ModeloTerminales.TRAX:
                case ModeloTerminales.Max:
                case ModeloTerminales.NeoMax:
                case ModeloTerminales.Prox:
                case ModeloTerminales.SuperMax:
                    return new CeZucchetti();
                    break;
                case ModeloTerminales.HandPunch:
#if RsiDotNetDll
                    return new CeHp2();
#else
                    return new CeHp();
#endif
                    break;
                case ModeloTerminales.AC215:
                    return new CeVT_AS215();
                    break;
                case ModeloTerminales.Panel_Plumas:
                    return new CePanelPlumas();
                    break;
                case ModeloTerminales.BioEntryPlus:
                    return new CeBioEntryPlus();
                    break;
                case ModeloTerminales.ZkSoftwareiClock:
                    return new CeZkSoftwareiClock();
                    break;
                case ModeloTerminales.eClock:
                    return new CeClockTerm();
                    break;
                case ModeloTerminales.ZkSoftware:
                    return new CeZkSoftware();
                    break;
                case ModeloTerminales.ZkSoftwarePull:
                    return new CePullSdk();
                    break;
            }
            return null;
        }
        public static void Sleep(int Milisegundos)
        {
            try
            {
                System.Threading.Thread.Sleep(Milisegundos);
                return;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
            CeLog2.AgregaError("Recuperando de error en Sleep");
        }

        public bool SePuedeSincincronizarListas()
        {
            try
            {
                if (!eClockSync.Properties.Settings.Default.ValidarHorario)
                    return true;
                if (m_DatosSitio.IsSITIO_HDESDE_SVECNull())
                    m_DatosSitio.SITIO_HDESDE_SVEC = FechaNula + new TimeSpan(0, 0, 0);
                if (m_DatosSitio.IsSITIO_HHASTA_SVECNull())
                    m_DatosSitio.SITIO_HHASTA_SVEC = FechaNula + new TimeSpan(23, 59, 59);
                DateTime FechaDesde = FechaHora.Date + (m_DatosSitio.SITIO_HDESDE_SVEC - FechaNula);
                DateTime FechaHasta = FechaHora.Date + (m_DatosSitio.SITIO_HHASTA_SVEC - FechaNula);

                if (FechaHasta > FechaDesde)
                {
                    if (FechaHasta >= FechaHora && FechaHora >= FechaDesde)
                        return true;
                }
                else
                    if (FechaDesde >= FechaHora && FechaHora >= FechaHasta)
                        return true;
                CeLog2.AgregaLog("Fuera de horario de sincronizacion de listas");
                return false;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);

            }
            return true;
        }

        void AgregaDExtraCompleted(object sender, WS_eCheck.AgregaDExtraCompletedEventArgs Args)
        { }

        void ConfirmaCompleted(object sender, WS_eCheck.ConfirmaCompletedEventArgs Args)
        { }

        public bool Inicia(WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESRow DatosTerminal, WS_eCheck.WS_eCheck ws_eCheck)
        {
            m_DatosTerminal = DatosTerminal;
            Terminal_ID = Convert.ToInt32(DatosTerminal.TERMINAL_ID);
            m_ws_eCheck = ws_eCheck;
            m_ws_eCheck.Timeout = 1000000;
            CreaDirectorio(Terminal_ID.ToString());
            m_TConexion.CargarCadenaConexion(m_DatosTerminal.TERMINAL_DIR);
            return true;
        }
        public bool Sincroniza(WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESRow DatosTerminal, WS_eCheck.WS_eCheck ws_eCheck)
        {
            Inicia(DatosTerminal, ws_eCheck);
            try
            {
                bool PuedeSincronizar = true;
                AgregaLog("Conectando...");
                /*WS_eCheck.DS_WSPersonasTerminales DShhh = m_ws_eCheck.ListaEmpleadosCambios(Terminal_ID);
                 if (DShhh != null && DShhh.DT_PersonasTerminales.Rows.Count > 0)
                 {
                 }
                 else
                 {
                     AgregaLog("--No hay empleados...");
                 }
                 */
                m_ws_eCheck.AgregaDExtraCompleted += new WS_eCheck.AgregaDExtraCompletedEventHandler(AgregaDExtraCompleted);
                m_ws_eCheck.ConfirmaCompleted += new WS_eCheck.ConfirmaCompletedEventHandler(ConfirmaCompleted);

                if (!Conecta())
                {
                    AgregaError("Error al sincronizar");
                    PuedeSincronizar = false;

                    m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Error_Conexion, sLog);

                }
                else
                    m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Conexion_Correcta, "");

                if (m_Conectado && PuedeSincronizar)
                {
                    TimeZoneRemota = new TimeSpan(ws_eCheck.ObtenZonaHorario());
                }

                if (m_Conectado && PuedeSincronizar)
                    PuedeSincronizar = SePuedeSincronizar();
                if (m_Conectado && PuedeSincronizar)
                {
                    AgregaLog("Enviando Fecha y Hora...");
                    if (TransmiteFechayHora())
                        m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.FechaHora_Enviada, "");
                    else
                        m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.FechaHora_Error, sLog);
                }

                if (m_Conectado && PuedeSincronizar)
                {
                    AgregaLog("Recibiendo Checadas...");
                    if (DescargaChecadas())
                        m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Checadas_Descargadas, "");
                    else
                        m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Checadas_Error, sLog);
                }

                if (m_Conectado && PuedeSincronizar && SePuedeSincincronizarListas())
                {
                    AgregaLog("Sync Vectores...");
                    bool EsTerminalVectores = false;
                    Tecnologias Tecno = (Tecnologias)DatosTerminal.TIPO_TECNOLOGIA_ID;
                    if (Tecno == Tecnologias.Huella || Tecno == Tecnologias.Rostro || Tecno == Tecnologias.Palma)
                        EsTerminalVectores = true;
                    if (DatosTerminal.TERMINAL_ENROLA != 0)
                    {
                        AgregaLog("Descargando Vectores...");
                        PuedeSincronizar = DescargaVectores();
                        if (PuedeSincronizar)
                            m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Vectores_Descargados, "");
                        else
                            m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Vectores_Error_Desc, sLog);
                    }

                    Directorio += "LS ";
                    if (PuedeSincronizar)
                    {
                        AgregaLog("Descargando Datos del Servidor...");
                        WS_eCheck.DS_WSPersonasTerminales DS = m_ws_eCheck.ListaEmpleadosCambios(Terminal_ID);
                        //corregir para que sincronice cuando no hay registros
                        if (DS != null && DS.DT_PersonasTerminales.Rows.Count > 0)
                        {
                            m_DatosEmpleados = DS.DT_PersonasTerminales;
                            bool Correcto = true;

                            if (m_Conectado && Correcto)
                            {
                                AgregaLog("Enviando Lista Blanca...");
                                Correcto = TransmiteLB();
                                if (Correcto)
                                    m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Vectores_Enviados, "");
                                else
                                    m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Vectores_Error_Env, sLog);
                            }
                            if (m_Conectado && Correcto)
                            {
                                AgregaLog("Enviando Lista Negra...");
                                Correcto = TransmiteLN();
                            }
                            //Pot lo pronto aunque exista error al transmitir vectores, 
                            //continuara la sincronizacion
                            if (m_Conectado && EsTerminalVectores)
                            {
                                AgregaLog("Enviando Vectores...");
                                Correcto = TransmiteVectores();
                                if (Correcto)
                                    m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Vectores_Enviados, "");
                                else
                                    m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Vectores_Error_Env, sLog);
                            }
                            if (m_Conectado && EsTerminalVectores && Correcto)
                            {
                                AgregaLog("Enviando Sin Vector...");
                                Correcto = TransmiteSinVector();
                            }

                            if (Correcto)
                            {
                                AgregaLog("Confirmando Lista Empleados...");
                                m_ws_eCheck.ConfirmaListaEmpleados(Terminal_ID);
                            }
                        }
                        else
                            AgregaLog("No hay empleados...");
                    }



                }


                if (m_Conectado && PuedeSincronizar)
                {
                    AgregaLog("Enviando Turnos...");
                    TransmiteTurnos();
                }
                if (m_Conectado && PuedeSincronizar)
                {
                    AgregaLog("Enviando Dias Festivos...");
                    TransmiteDiasFestivos();
                }
                if (m_Conectado && PuedeSincronizar)
                {
                    AgregaLog("Enviando Parámetros...");
                    TransmiteParametros();
                }
            }
            catch (Exception e)
            {

                AgregaError(e);
            }

            if (m_Conectado)
                Desconecta();
            AgregaLog("Desconectado");
            m_ws_eCheck.AgregaDExtra(Terminal_ID, WS_eCheck.Tipo_Term_DEXTRAS.Log_Comunicacion, sLog);

            return true;
        }


        public bool CreaDir(string Dir)
        {
            try { System.IO.Directory.CreateDirectory(Dir); }
            catch { }

            return true;
        }

        public bool CreaDirectorio(string Terminal)
        {
            string Fecha = DateTime.Now.ToString("yyyy-MM-dd");
            string Hora = DateTime.Now.ToString("HH-mm-ss");
            Directorio = RutaTemporal;
            CreaDir(Directorio);
            Directorio += "eClock " + Terminal + " " + Fecha + " " + Hora;
            Directorio += " ";
            return true;
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

        public static int Hex2Int(string Hex)
        {
            int Valor = 0;
            for (int Cont = 0; Cont < Hex.Length; Cont++)
                Valor = Valor * 16 + HexByte2Byte(Convert.ToByte(Hex[Cont]));
            return Valor;
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

        public static byte[] String2Bcd(string TextoEnHexa)
        {
            byte[] Arreglo = ObtenArregloBytes(TextoEnHexa);
            if (Arreglo == null)
                return null;
            return Bytes2Bcd(Arreglo, 0, Arreglo.Length);
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
        /// Obtiene un arreglo de Bytes apartir de una cadena de caracteres
        /// </summary>
        /// <param name="Cadena"></param>
        /// <returns></returns>
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
        /// Obtiene un arreglo de Bytes apartir de una cadena de caracteres, si la longitud es menor que la cadena, esta será cortada si es mayor esta será rellenada con el caracter deseado
        /// </summary>
        /// <param name="Cadena"></param>
        /// <param name="Len">Longitud deseada del arreglo resultante</param>
        /// <returns></returns>
        public static Byte[] ObtenArregloBytes(string Cadena, int Len, byte Relleno)
        {
            if (Len <= 0)
                Len = Cadena.Length;
            if (Len < 1)
                return null;
            Byte[] Arreglo = new byte[Len];
            for (int Cont = 0; Cont < Len; Cont++)
            {
                if (Cont < Cadena.Length)
                    Arreglo[Cont] = Convert.ToByte(Cadena[Cont]);
                else
                    Arreglo[Cont] = Relleno;
            }
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
            for (int Cont = 0; Cont < Len && Arreglo[Pos + Cont] != 0; Cont++)
            {
                Texto += (char)Arreglo[Pos + Cont];
            }
            return Texto;
        }

        public static DateTime FechaNula
        {
            get { return new DateTime(2006, 01, 01); }
        }

        /// <summary>
        /// Obtiene la fecha y hora de la terminal conectada
        /// </summary>
        /// <returns>regresa fecha nula si no </returns>
        virtual public DateTime ObtenFechaHora()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Asigna a la terminal
        /// </summary>
        /// <param name="FechaHora"></param>
        /// <returns></returns>
        virtual protected Errores AsignaFechaHora(DateTime FechaHora)
        {
            return Errores.Correcto;
        }

        public bool AgregaChecada(int Terminal_ID, string Llave, DateTime FechaHora, TipoAccesos TAcceso)
        {
            WS_eCheck.DS_WSPersonasTerminales.DT_CHECADASDataTable Checada = new eClockSync.WS_eCheck.DS_WSPersonasTerminales.DT_CHECADASDataTable();
            Checada.AddDT_CHECADASRow(Llave, FechaHora, Convert.ToInt32(TAcceso));

            if (m_ws_eCheck.EnviaChecadas(Terminal_ID, Checada))
            {
                return true;
            }
            return false;
        }

        public bool AgregaChecada(string Llave, DateTime FechaHora, TipoAccesos TAcceso)
        {
            return AgregaChecada(Llave, FechaHora, TAcceso, false);
        }
        public bool AgregaChecada(string Llave, DateTime FechaHora, TipoAccesos TAcceso, bool Inmediatamente)
        {
            try
            {
                if (m_Checadas == null)
                    return false;
                AgregaLog("Checada " + Llave + "|" + FechaHora.ToString("yyMMddHHmmss") + "|" + TAcceso.ToString());
                /// Convierte la fecha y hora locales a la fecha a una fecha y hora intermedias para que el servidor la transforme a su fecha y coincidad, para que se guarde bien.
                FechaHora = DateTime2DateTimeRemota(FechaHora);
                //Requiere ser optimizado
                if (Inmediatamente)
                {
                    AgregaChecada(Terminal_ID, Llave, FechaHora, TAcceso);
                }
                else
                    m_Checadas.AddDT_CHECADASRow(Llave, FechaHora, Convert.ToInt32(TAcceso));
                return true;
            }
            catch (System.Exception e)
            {
                CeLog2.AgregaError(e);
            }
            return false;

        }
        /// <summary>
        /// Convierte la fecha y hora locales a la fecha a una fecha y hora intermedias para que el servidor la transforme a su fecha y coincidad, para que se guarde bien.
        /// </summary>
        /// <param name="FechaHora"></param>
        /// <returns></returns>
        public DateTime DateTime2DateTimeRemota(DateTime FechaHora)
        {
            DateTime DTRetorno = FechaHora - m_TimeZoneRemota + TimeZone.CurrentTimeZone.GetUtcOffset(FechaHora);
           // CeLog2.AgregaLog("DateTime2DateTimeRemota " + FechaHora.ToString() + " -> " + DTRetorno.ToString());

            //  m_TimeZoneRemota.
            return DTRetorno;
        }
        public virtual bool Conecta()
        {
            return m_Conectado = true;
        }

        public virtual bool Desconecta()
        {
            return m_Conectado = false;
        }

        public virtual Errores PoleoChecadas()
        {

            return Errores.No_Requiere;
        }

        /// <summary>
        /// indica que se deberá enviar la lista de blanca a la terminal
        /// </summary>
        /// <returns></returns>
        protected virtual Errores EnvioListaBlanca()
        {

            return Errores.Correcto;
        }

        /// <summary>
        /// indica que se deberá enviar la lista de parametros a la terminal
        /// </summary>
        /// <returns></returns>
        protected virtual Errores EnvioParametros()
        {

            return Errores.Correcto;
        }
        /// <summary>
        /// Agrega una huella a el datatable de la terminal
        /// </summary>
        /// <param name="ID">Identificador del Empleado</param>
        /// <param name="No">Numero de Huella desde 0</param>
        /// <param name="Template">Huella o vector</param>
        /// <returns></returns>
        protected int AgregaHuella(string ID, int NoHuella, byte[] Template)
        {
            try
            {

                WS_eCheck.DS_WSPersonasTerminales.DT_VectoresRow Fila = m_Vectores.FindByPERSONAS_A_VEC_T1(ID.ToString());

                bool EsNuevo = false;
                if (Fila == null)
                {
                    Fila = m_Vectores.NewDT_VectoresRow();
                    Fila.PERSONAS_A_VEC_T1 = ID.ToString();
                    EsNuevo = true;
                }
                if (NoHuella == 0)
                    Fila.PERSONAS_A_VEC_1 = Template;
                if (NoHuella == 1)
                    Fila.PERSONAS_A_VEC_2 = Template;

                if (EsNuevo)
                {
                    m_Vectores.AddDT_VectoresRow(Fila);
                }

            }
            catch (Exception ex)
            {
                AgregaError(ex);
            }
            return 1;
        }

        /// <summary>
        /// indica que se debe polear las huellas de la terminal
        /// </summary>
        /// <returns></returns>
        protected virtual Errores PoleoVectores()
        {

            return Errores.No_Requiere;
        }


        /// <summary>
        /// indica que se deberá enviar la lista de vectores a la terminal
        /// </summary>
        /// <returns></returns>
        protected virtual Errores EnvioVectores()
        {

            return Errores.Correcto;
        }


        /// <summary>
        /// indica que se deberá enviar la lista de personas sin vector a la terminal
        /// </summary>
        /// <returns></returns>
        protected virtual Errores EnvioSinVector()
        {

            return Errores.Correcto;
        }

        /// <summary>
        /// Esta funcion se llama despues de que se ha poleado 
        /// los vectores y subido dicha información al servidor
        /// </summary>
        /// <returns></returns>
        protected virtual Errores PoleoVectoresSatisfactorio()
        {

            return Errores.Correcto;
        }

        protected virtual Errores BorraChecadas()
        {

            return Errores.No_Requiere;
        }

        protected virtual bool SePuedeSincronizar()
        {
            return true;
        }

        protected virtual Errores EnviaMensage(int Linea, string Mensage)
        {
            return Errores.Error_Desconocido;
        }
        public bool DescargaChecadas()
        {
            try
            {
                m_Checadas = new eClockSync.WS_eCheck.DS_WSPersonasTerminales.DT_CHECADASDataTable();
                Errores Err = PoleoChecadas();
                if (Err == Errores.Correcto)
                {
                    bool Borrar = true; 
                    if (eClockSync.Properties.Settings.Default.NoBorrarChecadas)
                        Borrar = false;
                    if (m_Checadas == null || m_Checadas.Rows.Count <= 0)
                    {
                        //IUChecadas = IUChecadasNuevo;
                        //return true;
                    }
                    else
                        if (!m_ws_eCheck.EnviaChecadas(Terminal_ID, m_Checadas))
                            Borrar = false;
                    if (Borrar)
                    {
                        Errores Error;

                        Error = BorraChecadas();
                        if (Error == Errores.No_Requiere)
                        {
                            AgregaLog("No_Requiere BorraChecadas");
                            return true;
                        }
                        IUChecadas = IUChecadasNuevo;
                        return true;
                    }

                }
                if (Err == Errores.No_Requiere)
                    return true;
                m_Checadas = null;
                AgregaError("Error en la DescargaChecadas");

            }
            catch (System.Exception e)
            {
                AgregaError(e);
            }
            return false;
        }

        public bool AsignaHuella(int PersonaID, byte[] Huella, int NoHuella)
        {
            return m_ws_eCheck.AsignaHuella(PersonaID, Terminal_ID, Huella, NoHuella);
        }

        public bool DescargaVectores()
        {
            m_Vectores = new eClockSync.WS_eCheck.DS_WSPersonasTerminales.DT_VectoresDataTable();
            Errores Error = PoleoVectores();
            if (Errores.No_Requiere_Cambio_En_Vectores == Error || Errores.No_Requiere == Error)
            {
                IUVectoresEnroll = IUVectoresEnrollNuevo;
                return true;
            }
            if (Error == Errores.Correcto)
            {
                bool Res = m_ws_eCheck.RecibeVectores(Terminal_ID, m_Vectores);
                if (Res)
                    if (PoleoVectoresSatisfactorio() == Errores.Correcto)
                    {
                        IUVectoresEnroll = IUVectoresEnrollNuevo;
                        return true;
                    }

            }
            AgregaError("Error en la DescargaVectores");
            return false;
        }

        public bool TransmiteFechayHora()
        {
            try
            {
                DateTime Local = DateTime.Now;


                DateTime FechaHora = m_ws_eCheck.ObtenFechaHora();
                TimeSpan TS = DateTime.Now - Local;
                m_DiferenciaFechaHora = DateTime.Now - (FechaHora + TS);
                try
                {
                    FechaHora = FechaHora.AddMinutes(Convert.ToInt32(m_DatosTerminal.TERMINAL_MINUTOS_DIF));
                }
                catch { }
                Errores Error = AsignaFechaHora(FechaHora + TS);
                if (Errores.Correcto == Error)
                    return true;
                if (Errores.No_Requiere == Error)
                    return true;
            }
            catch (System.Exception e)
            {
                AgregaError(e);
            }
            AgregaError("Error en TransmiteFechayHora");
            return false;

        }
        public bool TransmiteLB()
        {
            try
            {
                Errores Error = EnvioListaBlanca();
                if (Error == Errores.Correcto)
                {
                    AgregaLog("Envio de TransmiteLB correcta");
                    return true;
                }
                if (Error == Errores.No_Requiere)
                {
                    AgregaLog("No_Requiere TransmiteVectores");
                    return true;
                }
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }

            AgregaError("Error en TransmiteLB");
            return false;
        }

        public bool TransmiteLN()
        {
            return true;
        }
        public DS_Cambios m_CambiosPersonas = null;
        public string NombreXmlCambiosPersonas
        {
            get { return System.IO.Path.Combine(RutaTemporal, Terminal_ID.ToString() + "_Cambios.XML"); }
        }
        public bool CargaCambiosPersonas()
        {
            try
            {
                m_CambiosPersonas = new DS_Cambios();
                if (System.IO.File.Exists(NombreXmlCambiosPersonas))
                    m_CambiosPersonas.ReadXml(NombreXmlCambiosPersonas);
                return true;
            }
            catch (Exception ex)
            { AgregaError(ex); }
            return false;
        }

        public bool GuardaCambiosPersonas()
        {
            try
            {
                if (System.IO.File.Exists(NombreXmlCambiosPersonas))
                    System.IO.File.Delete(NombreXmlCambiosPersonas);
                m_CambiosPersonas.WriteXml(NombreXmlCambiosPersonas);
                return true;
            }
            catch (Exception ex)
            { AgregaError(ex); }
            return false;
        }
        /// <summary>
        /// Calcula el valor del Hash de un texto
        /// </summary>
        /// <param name="Texto">Texto de donde se va a obtener el hash</param>
        /// <returns></returns>
        public string CalculaHash(string Texto)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider Sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();

            string HashSR = BitConverter.ToString(Sha1.ComputeHash(new System.IO.MemoryStream(System.Text.ASCIIEncoding.Default.GetBytes(Texto))));
            //AgregaLog("CalculaHash -> " + Texto + "  Hash " + HashSR);
            return HashSR;
        }

        public string CalculaHash(object[] Arreglo)
        {
            string sArreglo = "";
            foreach (object Objeto in Arreglo)
            {
                if (Objeto.ToString() == "System.Byte[]")
                    sArreglo += BitConverter.ToString((byte[])Objeto);
                else
                    sArreglo += Objeto.ToString();
            }
            return CalculaHash(sArreglo);
        }

        public string ObtenHashPersona(WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila)
        {
            try
            {

                return CalculaHash(Fila.ItemArray);
            }
            catch { }
            return "";
        }
        public string ObtenHashPersona(decimal Persona_ID)
        {
            try
            { return ObtenHashPersona(m_DatosEmpleados.FindByPERSONA_ID(Persona_ID)); }
            catch { }
            return "";
        }
        public string ObtenUltimoHashPersona(decimal Persona_ID)
        {
            try
            {
                DS_Cambios.DT_PersonasRow Persona = m_CambiosPersonas.DT_Personas.FindByPERSONA_ID(Persona_ID);
                return Persona.HASH_TOTAL;
            }
            catch
            {

            }
            return "";
        }
        public bool ConfirmaEmpleado(WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila)
        {
            return ConfirmaEmpleado(Fila.PERSONA_ID, ObtenHashPersona(Fila), Fila);
        }
        public bool ConfirmaEmpleado(decimal Persona_ID, string Hash, WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila)
        {
            try
            {
                ws_eCheck.Confirma(Terminal_ID, Convert.ToInt32(Persona_ID));
                if (m_CambiosPersonas == null)
                {
                    AgregaLog("ConfirmaEmpleado m_CambiosPersonas == null Cargando...");
                    CargaCambiosPersonas();
                }
                DS_Cambios.DT_PersonasRow Persona = m_CambiosPersonas.DT_Personas.FindByPERSONA_ID(Persona_ID);
                bool EsNuevo = false;
                if (Persona == null)
                {
                    Persona = m_CambiosPersonas.DT_Personas.NewDT_PersonasRow();
                    EsNuevo = true;
                    Persona.PERSONA_ID = Persona_ID;

                }
                try
                { Persona.PERSONA_LINK_ID = Fila.PERSONA_LINK_ID; }
                catch { }
                try
                { Persona.PERSONA_NOMBRE = Fila.PERSONA_NOMBRE; }
                catch { }
                try
                { Persona.PERSONA_S_HUELLA_CLAVE = Fila.PERSONA_S_HUELLA_CLAVE; }
                catch { }
                try
                { Persona.PERSONA_ID_S_HUELLA = Fila.PERSONA_ID_S_HUELLA; }
                catch { }
                try
                { Persona.PERSONAS_A_VEC_1 = Fila.PERSONAS_A_VEC_1; }
                catch { }
                try
                { Persona.PERSONAS_A_VEC_2 = Fila.PERSONAS_A_VEC_2; }
                catch { }
                try
                { Persona.PERSONAS_A_VEC_3 = Fila.PERSONAS_A_VEC_3; }
                catch { }
                try
                { Persona.PERSONAS_A_VEC_T1 = Fila.PERSONAS_A_VEC_T1; }
                catch { }
                try
                { Persona.PERSONAS_A_VEC_T2 = Fila.PERSONAS_A_VEC_T2; }
                catch { }
                try
                { Persona.PERSONAS_A_VEC_T3 = Fila.PERSONAS_A_VEC_T3; }
                catch { }
                Persona.HASH_TOTAL = Hash;
                Persona.ULTIMO_ENVIO = DateTime.Now;
                if (EsNuevo)
                    m_CambiosPersonas.DT_Personas.AddDT_PersonasRow(Persona);
                GuardaCambiosPersonas();
                return true;
            }
            catch (Exception ex)
            {
                AgregaError(ex);

            }
            return false;
        }

        public bool TransmiteVectores()
        {
            try
            {
                CargaCambiosPersonas();
                Errores Err = EnvioVectores();
                GuardaCambiosPersonas();
                if (Err == Errores.Correcto)
                {
                    AgregaLog("Envío de TransmiteVectores correcta");
                    return true;
                }
                if (Err == Errores.No_Requiere)
                {
                    AgregaLog("No_Requiere TransmiteVectores");
                    return true;
                }
                if (Err == Errores.No_Requiere_Cambio_En_Vectores)
                {
                    AgregaLog("No_Requiere_Cambio_En_Vectores TransmiteVectores");
                    return true;
                }
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
            AgregaError("Error en TransmiteVectores");
            return false;
        }

        public bool TransmiteSinVector()
        {
            try
            {
                Errores Err = EnvioSinVector();
                if (Err == Errores.Correcto)
                {
                    AgregaLog("Envio de TransmiteSinVector correcta");
                    return true;
                }
                if (Err == Errores.No_Requiere)
                {
                    AgregaLog("No_Requiere TransmiteSinVector");
                    return true;
                }
                if (Err == Errores.No_Requiere_Cambio_En_Vectores)
                {
                    AgregaLog("No_Requiere_Cambio_En_Vectores TransmiteSinVector");
                    return true;
                }
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }

            AgregaError("Error en TransmiteSinVector");
            return false;
        }

        public bool TransmiteTurnos()
        {
            return true;
        }

        public bool TransmiteDiasFestivos()
        {
            return true;
        }
        public bool TransmiteParametros()
        {
            try
            {
                Errores Err = EnvioParametros();
                if (Err == Errores.Correcto)
                {
                    AgregaLog("Envio de TransmiteParametros correcta");
                    return true;
                }
                if (Err == Errores.No_Requiere)
                {
                    AgregaLog("No_Requiere TransmiteParametros");
                    return true;
                }
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }

            AgregaError("Error en TransmiteParametros");
            return false;
        }
        public static string AgregaSeparador(string Valores, string NuevoValor, string Separador)
        {
            if (Valores.Length > 0)
                Valores += Separador;
            return Valores + NuevoValor;
        }
        public static string[] ObtenArregoSeparador(string Valores, string Separador)
        {
            string Ret = "";
            try
            {
                string[] sSeparador = new string[1];
                sSeparador[0] = Separador;

                string[] sValores = Valores.Split(sSeparador, StringSplitOptions.RemoveEmptyEntries);
                return sValores;
            }
            catch { }
            return null;
        }
    }


}