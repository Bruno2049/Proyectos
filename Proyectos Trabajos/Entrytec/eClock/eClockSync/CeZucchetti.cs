using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
namespace eClockSync
{

    class CeZucchetti : CeTerminalSync
    {
        protected int m_Sitio_Hijo_ID = -1;
        protected int m_LenTarjeta = 0;
        protected int m_LenHora = 6;
        protected int m_LenEsclavos = 0;
        protected bool m_EsEntrada = false;
        protected bool m_EsSalida = false;
        /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo AXCARDS
        /// </summary>
        protected string IUAXCARDSNuevo = "";

        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo AXCARDS
        /// </summary>
        protected string IUAXCARDS
        {
            get { return ObtenParametro("IUAXCARDS", ""); }
            set { AsignaParametro("IUAXCARDS", value); }
        }

        /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo AXPOOLS
        /// </summary>
        protected string IUAXPOOLSNuevo = "";

        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo AXPOOLS
        /// </summary>
        protected string IUAXPOOLS
        {
            get { return ObtenParametro("IUAXPOOLS", ""); }
            set { AsignaParametro("IUAXPOOLS", value); }
        }

        /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo AXTIMET
        /// </summary>
        protected string IUAXTIMETNuevo = "";

        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo AXTIMET
        /// </summary>
        protected string IUAXTIMET
        {
            get { return ObtenParametro("IUAXTIMET", ""); }
            set { AsignaParametro("IUAXTIMET", value); }
        }

        /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo AXGATES
        /// </summary>
        protected string IUAXGATESNuevo = "";

        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo AXGATES
        /// </summary>
        protected string IUAXGATES
        {
            get { return ObtenParametro("IUAXGATES", ""); }
            set { AsignaParametro("IUAXGATES", value); }
        }


        /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo AXTGATE
        /// </summary>
        protected string IUAXTGATENuevo = "";

        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo AXTGATE
        /// </summary>
        protected string IUAXTGATE
        {
            get { return ObtenParametro("IUAXTGATE", ""); }
            set { AsignaParametro("IUAXTGATE", value); }
        }

        /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo WMSG
        /// </summary>
        protected string IUWMSGNuevo = "";

        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo WMSG
        /// </summary>
        protected string IUWMSG
        {
            get { return ObtenParametro("WMSG", ""); }
            set { AsignaParametro("WMSG", value); }
        }

        /// <summary>
        /// Contiene el listado de las terminales (lectoras) que son hijas de la presente
        /// controladora.
        /// </summary>
        WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESDataTable TerminalesHijas = null;

        #region Enumeraciones
        public enum ISZTipoConexion
        {
            NET92O = 0x00,	//NET92 con viejo PSLD, MicroLD modo 1
            NET92N = 0x01,	//NET92 con PSLD/2, PSLD/12
            NET92E = 0x05,	//NET92 con PSLD/3, MicroLD modo 2
            IPSLD = 0x02,	//conexión PSLD/XI 
            PROX = 0x03,	//conexión directa RS232 
            IPROX = 0x04,	//conexión maestro/esclavo 
            WSOCK = 0x06,	//conexión etherbox/winsocket 
            IRDA = 0x07,	//IRDA
            WRADIO = 0x08	//IRDA
        };

        #endregion

        #region Proxc.dll
        [DllImport("Proxc.dll")]
        public static extern int InitCOMM
            (
                Int16 nPort,
                Int16 nDevice,
                Int32 nBaud,
                Int16 nParity
            );

        [DllImport("Proxc.dll")]
        public static extern int ExitCOMM
            (
            );
        [DllImport("Proxc.dll")]
        public static extern int ChangeAdapter
            (
                Byte[] szIPAddressAdaptador
            );
        public static int ChangeAdapter(string IPAddressAdaptador)
        {
            return ChangeAdapter(ObtenArregloBytes(IPAddressAdaptador));
        }
        [DllImport("Proxc.dll")]
        public static extern int SetIPAddress
            (
                Byte[] szIPAddress
            );
        public static int SetIPAddress(string IPAddress)
        {
            return SetIPAddress(ObtenArregloBytes(IPAddress));
        }

        [DllImport("Proxc.dll")]
        public static extern int GetTime
            (
                Byte[] szStrTime
            );

        [DllImport("Proxc.dll")]
        public static extern int SetTime
            (
                Byte[] szStrTime
            );

        [DllImport("Proxc.dll")]
        public static extern int GetDate
            (
                Byte[] szStrDate
            );

        [DllImport("Proxc.dll")]
        public static extern int SetDate
            (
                Byte[] szStrDate
            );
        [DllImport("Proxc.dll")]
        public static extern int ListFiles
            (
                 Byte[] szPattern,
                 Byte[] szFileList
            );
        public static int ListFiles(string szPattern, Byte[] szFileList)
        {
            return ListFiles(ObtenArregloBytes(szPattern), szFileList);
        }
        [DllImport("Proxc.dll")]
        public static extern int SendCommand(Byte[] szCommand, Byte[] szReply);
        public static int SendCommand(string szCommand, ref string szReply)
        {
            int R = 0;
            try
            {
                byte[] Datos = new byte[5080];

                R = SendCommand(ObtenArregloBytes(szCommand), Datos);
                szReply = "";
                if (R == 0)
                    szReply = ObtenString(Datos);
            }
            catch (Exception ex)
            {

                CeLog2.AgregaError(ex);
            }
            return R;
        }
        [DllImport("Proxc.dll")]
        public static extern int PostCommand(Byte[] szCommand);
        public static int PostCommand(string szCommand)
        {
            return PostCommand(ObtenArregloBytes(szCommand));
        }

        [DllImport("Proxc.dll")]
        public static extern int SetParameter(int NoParametro, Byte[] ValorParametro);
        public static int SetParameter(int NoParametro, string ValorParametro)
        {
            try
            {
                return SetParameter(NoParametro, ObtenArregloBytes(ValorParametro));

            }
            catch (Exception ex)
            {

                CeLog2.AgregaError(ex);
            }
            return -1;
        }
        [DllImport("Proxc.dll")]
        public static extern int GetParameter(int NoParametro, Byte[] ValorParametro);
        public static int GetParameter(int NoParametro, ref string ValorParametro)
        {
            int R = 0;
            try
            {
                byte[] Datos = new byte[5080];

                R = GetParameter(NoParametro, Datos);
                ValorParametro = "";
                if (R == 0)
                    ValorParametro = ObtenString(Datos);
            }
            catch (Exception ex)
            {

                CeLog2.AgregaError(ex);
            }
            return R;
        }
        public static string GetParameter(int NoParametro, string ValorParametro)
        {
            string Param = "";
            if (GetParameter(NoParametro, ref Param) == 0)
                return Param;
            return ValorParametro;
        }
        public static int GetParameter(int NoParametro, int ValorParametro)
        {
            return Convert.ToInt32(GetParameter(NoParametro, ValorParametro.ToString()));
        }
        [DllImport("Proxc.dll")]
        public static extern int SendFile
            (
                Byte[] szFNamePc,
                Byte[] szFNameProx,
                Int16 nPFlag,
                Int16 nRecLen
            );
        public static int SendFile(string FNamePc, string FNameProx, int nPFlag, int nRecLen)
        {
            return SendFile(ObtenArregloBytes(FNamePc), ObtenArregloBytes(FNameProx), Convert.ToInt16(nPFlag), Convert.ToInt16(nRecLen));
        }
        [DllImport("Proxc.dll")]
        public static extern int SetKeyRec
            (
                Int16 KeyRecord
            );

        [DllImport("Proxc.dll")]
        public static extern int SetKeyRecStr
            (
                Byte[] szKeyRecord
            );

        [DllImport("Proxc.dll")]
        public static extern int RecvFile
            (
                Byte[] szFNameProx,
                Byte[] szFNamePc
            );
        public static int RecvFile(string FNameProx, string FNamePc)
        {
            return RecvFile(ObtenArregloBytes(FNameProx), ObtenArregloBytes(FNamePc));
        }

        [DllImport("Proxc.dll")]
        public static extern int DeleFile
            (
                Byte[] szFileName
            );
        public static int DeleFile(string FileName)
        {
            return DeleFile(ObtenArregloBytes(FileName));
        }

        [DllImport("Proxc.dll")]
        public static extern int SetTerminal
            (
                Int16 nAddress
            );




        [DllImport("Proxc.dll")]
        public static extern int GetError
            (
                Byte[] szErrMessage
            );


        #endregion

        string LeeNDia(int Dia)
        {
            switch (Dia)
            {
                case 0:
                    return "Domingo";
                    break;
                case 1:
                    return "Lunes";
                    break;
                case 2:
                    return "Martes";
                    break;
                case 3:
                    return "Miércoles";
                    break;
                case 4:
                    return "Jueves";
                    break;
                case 5:
                    return "Viernes";
                    break;
                case 6:
                    return "Sábado";
                    break;
                default:
                    return "Desconocido";
            }
            return "";
        }

        protected override bool SePuedeSincronizar()
        {
            //Se tiene que encontrar otra forma de saber si la terminal esta ocupada
            return true;
            Tecnologias Tecno = (Tecnologias)m_DatosTerminal.TIPO_TECNOLOGIA_ID;
            if (Tecno != Tecnologias.Huella)
                return true;

            int R = SetParameter(80, "32");
            //int R = SetParameter(80, new byte[] { 32 });
            Sleep(500);
            string Valor = "";
            R = GetParameter(80, ref Valor);
            if (Valor == "0")
                return true;
            AgregaDebug("Terminal Ocupada");
            return false;
        }

        public int AgregaHuella(byte Status, string ID, byte[] Template, int LenTemplate)
        {
            try
            {
                byte[] Bin = new byte[LenTemplate];
                Array.Copy(Template, 0, Bin, 0, LenTemplate);


                if (LenTemplate <= 0)
                    return -10;
                if (Status != 162 && Status != 163)
                    return -11;
                int NoHuella = Status - 162 + 1;                
                WS_eCheck.DS_WSPersonasTerminales.DT_VectoresRow Fila = m_Vectores.FindByPERSONAS_A_VEC_T1(ID);
                bool EsNuevo = false;
                if (Fila == null)
                {
                    Fila = m_Vectores.NewDT_VectoresRow();
                    Fila.PERSONAS_A_VEC_T1 = ID;
                    EsNuevo = true;
                }
                switch (NoHuella)
                {
                    case 1:
                        Fila.PERSONAS_A_VEC_1 = Bin;
                        break;
                    default:
                        Fila.PERSONAS_A_VEC_2 = Bin;
                        break;
                }

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

        public int GeneraFinger()
        {
            try
            {
                if (m_DatosEmpleados == null)
                    return -1;

                if (m_DatosEmpleados.Rows.Count <= 0)
                {
                    IUVectoresNuevo = "";
                    return 0;
                }
                int Huellas = 0;

                string NombreArchivo = Directorio + "FINGER";
                FileStream FS = System.IO.File.OpenWrite(NombreArchivo);

                foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
                {
                    try
                    {
                        if (Fila.IsPERSONA_ID_S_HUELLANull() || Fila.PERSONA_ID_S_HUELLA == 0)
                        {
                            int ID = 0;
                            try
                            {
                                ID = Convert.ToInt32(Fila.PERSONAS_A_VEC_T1);
                            }
                            catch
                            {
                                AgregaError("GeneraFinger Llave no numerica " + Fila.PERSONA_ID.ToString() + " - " + Fila.PERSONAS_A_VEC_T1.ToString());
                                continue;
                            }
                            Byte[] IDByte = ObtenArregloBytes(ID.ToString("00000000000"));

                            if (!Fila.IsPERSONAS_A_VEC_1Null() && Fila.PERSONAS_A_VEC_1.Length >= 80)
                            {
                                Array.Copy(IDByte, 0, Fila.PERSONAS_A_VEC_1, 1, 11);
                                Fila.PERSONAS_A_VEC_1[0] = 162;
                                FS.Write(Fila.PERSONAS_A_VEC_1, 0, Fila.PERSONAS_A_VEC_1.Length);
                                Huellas++;
                            }
                            if (!Fila.IsPERSONAS_A_VEC_2Null() && Fila.PERSONAS_A_VEC_2.Length >= 80)
                            {
                                Array.Copy(IDByte, 0, Fila.PERSONAS_A_VEC_2, 1, 11);
                                Fila.PERSONAS_A_VEC_2[0] = 163;
                                FS.Write(Fila.PERSONAS_A_VEC_1, 0, Fila.PERSONAS_A_VEC_2.Length);
                                Huellas++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        AgregaError(ex);
                    }
                }
                FS.Close();
                IUVectoresNuevo = CalculaHashArchivo(NombreArchivo);

                return Huellas;
            }
            catch (Exception ex)
            {
                AgregaError(ex);
            }




            return -1;
        }

        public int GeneraNoFinger()
        {
            if (m_DatosEmpleados == null)
                return -1;

            if (m_DatosEmpleados.Rows.Count <= 0)
            {
                IUVectoresNoNuevo = "";
                return 0;
            }
            byte[] CrLf = new byte[2];
            CrLf[0] = 13;
            CrLf[1] = 10;
            try
            {

                string NombreArchivo = Directorio + "NOFINGER";
                FileStream FS = System.IO.File.OpenWrite(NombreArchivo);

                foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
                {
                    try
                    {
                        if (!Fila.IsPERSONA_ID_S_HUELLANull() && Fila.PERSONA_ID_S_HUELLA != 0)
                        {
                            int ID = 0;
                            try
                            {
                                ID = Convert.ToInt32(Fila.PERSONAS_A_VEC_T1);
                            }
                            catch
                            {
                                AgregaError("GeneraNOFinger Llave no numerica " + Fila.PERSONA_ID.ToString() + " - " + Fila.PERSONAS_A_VEC_T1.ToString());
                                continue;
                            }


                            string Linea = ID.ToString("00000000000") + "0";
                            if (Linea.Length == 12)
                            {
                                FS.Write(ObtenArregloBytes(Linea), 0, 12);
                                FS.Write(CrLf, 0, 2);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        AgregaError(ex);
                    }
                }
                FS.Close();

                IUVectoresNoNuevo = CalculaHashArchivo(NombreArchivo);
                return 0;
            }
            catch (Exception e)
            {
                AgregaError(e);
            }
            return -1;
        }

        public void InhabilitaTrax(bool Inhabilitar)
        {
            string Respuesta = "";
            if (Inhabilitar)
            {
                SendCommand("CONSIDLE", ref Respuesta);
                //Sleep(100);
            }
            else
            {
                //Sleep(100);
                SendCommand("OFFLINE", ref Respuesta);
            }
        }

        public int DescargaHuellas()
        {
            string ArchivoHuellas = "FINGER";
            AgregaDebug("DescargaHuellas");
            byte[] Code = new byte[500];
            if (ListFiles(ArchivoHuellas, Code) <= 0)
                return 0;
            IUVectoresEnrollNuevo = ObtenString(Code, 9, 6);
            AgregaDebug("Len " + IUVectoresEnrollNuevo);

            if (IUVectoresEnroll == IUVectoresEnrollNuevo)
                return -1;
            int LongitudArchivo = Convert.ToInt32(IUVectoresEnrollNuevo);
            if (LongitudArchivo % 80 != 0)
            {
                AgregaError("No coinciden las longitudes de las huellas, posiblemente se descargo mal el archivo");
                return -2;
            }
            if (LongitudArchivo == 0)
            {
                AgregaError("No hay Huellas que descargar");
                return -1;
            }
            InhabilitaTrax(true);
            if (DescargaArchivo(ArchivoHuellas) > 0)
            {
                try
                {
                    byte Status = 0;
                    string ID = "";
                    byte[] Template = new byte[2000];
                    int LenTemplate = 0;
                    Sleep(50);
                    byte[] Archivo = File.ReadAllBytes(Directorio + ArchivoHuellas);
                    /*    if(Convert.ToInt32(IUVectoresEnrollNuevo) / 80 * 82 != Archivo.Length)
                        {
                            AgregaError("No coinciden las longitudes de las huellas, posiblemente se descargo mal el archivo");
                            return -2;
                        }*/
                    string Linea = "";
                    bool InicioLinea = true;
                    Sleep(50);

                    int NoHuellas = 0;
                    for (int Cont = 0; Cont < Archivo.Length; Cont++)
                    {
                        try
                        {
                            if (InicioLinea && (Archivo[Cont] == 161 || Archivo[Cont] == 162 || Archivo[Cont] == 163))
                            {
                                if (LenTemplate > 0)
                                {
                                    NoHuellas++;
                                    AgregaHuella(Template[0], Convert.ToInt32(ObtenString(Template, 1, 11)).ToString(), Template, LenTemplate);
                                }
                                LenTemplate = 0;
                            }
                            InicioLinea = false;
                            Template[LenTemplate] = Archivo[Cont];
                            LenTemplate++;
                            if (Archivo[Cont] == 10)
                            {
                                //Cont++;
                                InicioLinea = true;
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            AgregaError(ex);
                        }
                        Sleep(0);
                    }
                    AgregaHuella(Template[0], Convert.ToInt32(ObtenString(Template, 1, 11)).ToString(), Template, LenTemplate);
                    NoHuellas++;
                    AgregaDebug("DescargaHuellas No Huellas = " + NoHuellas.ToString());
                    InhabilitaTrax(false);
                    return NoHuellas;
                }
                catch (Exception ex)
                {
                    AgregaError(ex);
                }


            }
            InhabilitaTrax(false);
            return -21;
        }


        //Obtiene la fecha y hora guardadas en la terminal
        private object LeeFechaHora()
        {
            Byte[] Fecha = new Byte[40];
            Byte[] Hora = new Byte[40];

            int lRetVal = GetDate(Fecha);
            if (lRetVal == 0)
            {
                lRetVal = GetTime(Hora);
                if (lRetVal == 0)
                {

                    string SFecha = ObtenString(Fecha);
                    string SHora = ObtenString(Hora);
                    return new DateTime(
                        Convert.ToInt32(SFecha.Substring(1, 2)),
                        Convert.ToInt32(SFecha.Substring(3, 2)),
                        Convert.ToInt32(SFecha.Substring(5, 2)),
                        Convert.ToInt32(SHora.Substring(0, 2)),
                        Convert.ToInt32(SHora.Substring(2, 2)),
                        Convert.ToInt32(SHora.Substring(4, 2))
                        );

                }
            }
            return null;
        }

        private int PonFecha(DateTime Fecha)
        {
            byte[] BFecha = new byte[50];
            GetDate(BFecha);
            string SFechaOri = ObtenString(BFecha, 0, 7);
            int Dia = ((int)Fecha.DayOfWeek);
            if (Dia < 0)
                Dia = 7;
            string SFecha = Convert.ToString(Dia) + Fecha.ToString("yyMMdd");
            if (SFecha != SFechaOri)
                SetDate(ObtenArregloBytes(SFecha));
            return 0;
        }

        //Asigna la fecha y hora a la terminal si esta la requiere
        private int PonFechaHoraR(DateTime FechaHora)
        {
            byte[] BFecha = new byte[50];
            GetDate(BFecha);
            string SFechaOri = ObtenString(BFecha, 0, 7);
            int Dia = ((int)FechaHora.DayOfWeek);
            if (Dia < 0)
                Dia = 7;
            string SFecha = Convert.ToString(Dia) + FechaHora.ToString("yyMMdd");
            bool Pon = false;
            if (SFecha != SFechaOri)
                Pon = true;
            GetTime(BFecha);
            string SHoraOri = ObtenString(BFecha, 0, 6);
            string Hora = FechaHora.ToString("HHmmss");
            AgregaLog("SHoraOri=" + SHoraOri + ", Hora=" + Hora);
            if (SHoraOri.Length < 3 || SHoraOri.Substring(0, 3) != Hora.Substring(0, 3))
                Pon = true;
            if (Pon)
                PonFechaHora(FechaHora);
            return 0;
        }

        private int PonFechaHora(DateTime FechaHora)
        {

            int Dia = ((int)FechaHora.DayOfWeek);
            if (Dia < 0)
                Dia = 7;
            string Fecha = Convert.ToString(Dia) + FechaHora.ToString("yyMMdd");
            string Hora = FechaHora.ToString("HHmmss");

            int lRetVal = SetDate(ObtenArregloBytes(Fecha));
            if (lRetVal == 0)
            {
                lRetVal = SetTime(ObtenArregloBytes(Hora));
            }
            return lRetVal;
        }

        public int DescargaArchivo(string NombreArchivo)
        {
            int Intentos = 0;
            int lRetVal;
            Sleep(100);
            for (int Cont = 0; Cont < 3; Cont++)
            {
                AgregaDebug("NombreArchivo {0:G} {1:G}", NombreArchivo, Directorio + NombreArchivo);
                lRetVal = RecvFile(NombreArchivo, Directorio + NombreArchivo);
                if (lRetVal == -1)
                {
                    AgregaDebug("NombreArchivo ret {0:D}", lRetVal);
                    return lRetVal;
                }
                if (lRetVal >= 0)
                {
                    AgregaDebug("NombreArchivo ret {0:D}", lRetVal);
                    return lRetVal;
                }
            }
            AgregaDebug("NombreArchivo ret {0:D}", -3);
            return -3;
        }

        public DateTime FechaDeArchivo(string NombreArchivo)
        {
            string SFecha = NombreArchivo.Substring(NombreArchivo.Length - 5);
            DateTime Fecha = new DateTime(2010 +
                Convert.ToInt32(SFecha.Substring(0, 1)),
                Convert.ToInt32(SFecha.Substring(1, 2)),
                Convert.ToInt32(SFecha.Substring(3, 2))
                );
            return Fecha;
        }

        public int ConectaIP(string IP)
        {
            if (eClockSync.Properties.Settings.Default.AdaptadorRed != "127.0.0.1")
                ChangeAdapter(eClockSync.Properties.Settings.Default.AdaptadorRed);
            return SetIPAddress(IP);
        }

        public override bool Conecta()
        {
            m_Conectado = false;
            int R = 0;
            switch (m_TConexion.TipoConexion)
            {
                case CeC_Terminales.tipo.Red:
                    R = InitCOMM(Convert.ToInt16(m_TConexion.Puerto), Convert.ToInt16(ISZTipoConexion.WSOCK), 0, 0);
                    if (R != 0)
                    {
                        R = ConectaIP(m_TConexion.Direccion);

                    }
                    break;
                case CeC_Terminales.tipo.Serial:
                    R = InitCOMM(Convert.ToInt16(m_TConexion.Puerto), Convert.ToInt16(ISZTipoConexion.PROX), m_TConexion.Velocidad, 0);
                    break;
            }
            if (R >= 0)
            {
                if (Convert.ToInt16(m_TConexion.NoTerminal) == 0)
                {
                    R = SetTerminal(1);
                    if (R == -1)
                        R = SetTerminal(Convert.ToInt16(m_TConexion.NoTerminal));
                }
                else
                {
                    R = SetTerminal(Convert.ToInt16(m_TConexion.NoTerminal));
                    if (R == -1)
                        R = SetTerminal(1);
                }
                return m_Conectado = true;
            }
            return false;
        }

        public override bool Desconecta()
        {
            ExitCOMM();
            return m_Conectado = false;
        }

        private int GeneraAXCARDS()
        {
            if (m_DatosEmpleados == null)
                return -1;

            if (m_DatosEmpleados.Rows.Count <= 0)
            {
                IUAXCARDSNuevo = "";
                return 0;
            }

            int LenTarjeta = GetParameter(43, 0);
            m_LenTarjeta = LenTarjeta;
            if (LenTarjeta <= 0)
            {
                IUAXCARDSNuevo = "";
                return 0;
            }

            try
            {

                string NombreArchivo = Directorio + "AXCARDS";
                StreamWriter SW = File.CreateText(NombreArchivo);

                WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesDataTable[] ListasPersonas = null;
                int[] ZonasHorarias = null;
                if (TerminalesHijas == null || TerminalesHijas.Count <= 0)
                {
                    ListasPersonas = new eClockSync.WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesDataTable[1];
                    ListasPersonas[0] = m_DatosEmpleados;
                    ZonasHorarias = new int[1];
                    ZonasHorarias[0] = 0;
                }
                else
                {

                    ListasPersonas = new eClockSync.WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesDataTable[TerminalesHijas.Count + 1];
                    ZonasHorarias = new int[TerminalesHijas.Count + 1];
                    ListasPersonas[0] = m_DatosEmpleados;
                    ZonasHorarias[0] = 0;

                    for (int Cont = 0; Cont < TerminalesHijas.Count; Cont++)
                    {
                        CeC_Terminales TConexion = new CeC_Terminales();
                        TConexion.CargarCadenaConexion(TerminalesHijas[Cont].TERMINAL_DIR);
                        ZonasHorarias[1 + Cont] = TConexion.NoTerminal;
                        try
                        {
                            ListasPersonas[1 + Cont] = ws_eCheck.ListaEmpleados(Convert.ToInt32(TerminalesHijas[Cont].TERMINAL_ID)).DT_PersonasTerminales;
                        }
                        catch
                        {
                            ListasPersonas[1 + Cont] = null;
                        }

                    }


                }
                for (int Cont = 0; Cont < ListasPersonas.Length; Cont++)
                {
                    foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in ListasPersonas[Cont])
                    {
                        try
                        {
                            Int64 ID = 0;
                            if (Fila.IsPERSONAS_A_VEC_T1Null())
                                continue;
                            if (Fila.PERSONAS_A_VEC_T1.Length <= 0)
                                continue;
                            try
                            {
                                ID = Convert.ToInt64(Fila.PERSONAS_A_VEC_T1);
                            }
                            catch
                            {
                                AgregaError("GeneraAXCARDS Llave no numerica " + Fila.PERSONA_ID.ToString() + " - " + Fila.PERSONAS_A_VEC_T1.ToString());
                                continue;
                            }
                            string Linea = ID.ToString().PadLeft(LenTarjeta, '0');
                            if (Linea.Length > LenTarjeta)
                            {
                                AgregaError("El numero de tarjeta es mayor que la longitud pemitida por la terminal " + Linea + " - " + LenTarjeta);
                                continue;
                            }
                            Linea += ZonasHorarias[Cont].ToString("00");
                            SW.WriteLine(Linea);

                        }
                        catch (Exception ex)
                        {
                            AgregaError(ex);
                        }
                    }
                }
                SW.Close();


                IUAXCARDSNuevo = CalculaHashArchivo(NombreArchivo);
                return 0;
            }
            catch (Exception e)
            {
                AgregaError(e);
            }
            return -1;
        }

        private int GeneraAXPOOLS()
        {
            /*
             * AXPOOLS (A) 6
             * format: FFFFFF */
            IUAXPOOLSNuevo = "";
            try
            {

                string NombreArchivo = Directorio + "AXPOOLS";
                StreamWriter SW = File.CreateText(NombreArchivo);
                SW.WriteLine("A-----");
                SW.Close();
                IUAXPOOLSNuevo = CalculaHashArchivo(NombreArchivo);
                return 0;

            }
            catch (Exception e)
            {
                AgregaError(e);
            }
            return -1;
        }

        private int GeneraAXTIMET()
        {
            /*
             * AXTIMET (A) 16
             * format: HHMMhhmmDDDDDDDD*/
            IUAXTIMETNuevo = "";
            try
            {

                string NombreArchivo = Directorio + "AXTIMET";
                StreamWriter SW = File.CreateText(NombreArchivo);
                SW.WriteLine("00002400SMTWTFSH");
                SW.Close();
                IUAXTIMETNuevo = CalculaHashArchivo(NombreArchivo);
                return 0;

            }
            catch (Exception e)
            {
                AgregaError(e);
            }
            return -1;
        }

        private int GeneraAXGATES()
        {
            /*AXGATES (N) 1*/

            IUAXGATESNuevo = "";
            try
            {
                string Texto = "";
                Texto = Texto.PadRight(31, '0');
                string NombreArchivo = Directorio + "AXGATES";
                StreamWriter SW = File.CreateText(NombreArchivo);
                foreach (WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESRow Terminal in TerminalesHijas)
                {
                    CeC_Terminales TConexion = new CeC_Terminales();
                    TConexion.CargarCadenaConexion(Terminal.TERMINAL_DIR);
                    if (TConexion.NoTerminal <= 0)
                        continue;
                    m_EsEntrada = TERMINAL_ESENTRADA;
                    m_EsSalida = TERMINAL_ESSALIDA;
                    string T = "";
                    if (m_EsEntrada && m_EsSalida)
                        T = "3";
                    else
                        if (m_EsEntrada)
                            T = "1";
                        else
                            T = "0";
                    Texto = Texto.Substring(0, TConexion.NoTerminal - 1) + T + Texto.Substring(TConexion.NoTerminal);
                }
                SW.WriteLine(Texto);
                SW.Close();
                IUAXGATESNuevo = CalculaHashArchivo(NombreArchivo);
                return 0;

            }
            catch (Exception e)
            {
                AgregaError(e);
            }
            return -1;
        }

        private int GeneraAXTGATE()
        {
            /*AXTGATE P 3
             format:RRPPAA*/
            IUAXTGATENuevo = "";

            try
            {

                string NombreArchivo = Directorio + "AXTGATE";
                StreamWriter SW = File.CreateText(NombreArchivo);
                foreach (WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESRow Terminal in TerminalesHijas)
                {
                    CeC_Terminales TConexion = new CeC_Terminales();
                    TConexion.CargarCadenaConexion(Terminal.TERMINAL_DIR);
                    // bool EsEntrada = TERMINAL_ESENTRADA;
                    // bool EsSalida = TERMINAL_ESSALIDA;
                    string Texto = TConexion.NoTerminal.ToString("00") + TConexion.NoTerminal.ToString("00") + "00";
                    SW.WriteLine(Texto);

                }
                SW.Close();
                IUAXTGATENuevo = CalculaHashArchivo(NombreArchivo);
                return 0;

            }
            catch (Exception e)
            {
                AgregaError(e);
            }
            return -1;
        }
        private void CargaSitioHijo()
        {

            try
            {
                if (m_Sitio_Hijo_ID >= 0)
                    return;
                m_Sitio_Hijo_ID = SITIO_HIJO_ID;
                if (m_Sitio_Hijo_ID > 0)
                {
                    TerminalesHijas = ws_eCheck.ObtenTerminales(m_Sitio_Hijo_ID);
                }
            }
            catch (System.Exception e)
            {
                TerminalesHijas = null;
            }
        }
        private int GeneraWMSG()
        {

            IUWMSGNuevo = "";

            try
            {
                int Nombres = 0;
                string NombreArchivo = Directorio + "WMSG";
                StreamWriter SW = File.CreateText(NombreArchivo);
                int LenTarjeta = GetParameter(43, 0);
                foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
                {
                    try
                    {
                        int ID = 0;
                        try
                        {
                            ID = Convert.ToInt32(Fila.PERSONAS_A_VEC_T1);
                        }
                        catch
                        {
                            AgregaError("GeneraWMSG Llave no numerica " + Fila.PERSONA_ID.ToString() + " - " + Fila.PERSONAS_A_VEC_T1.ToString());
                            continue;
                        }

                        string Linea = ID.ToString().PadLeft(LenTarjeta, '0');

                        if (!Fila.IsPERSONA_NOMBRENull())
                        {
                            Linea += Fila.PERSONA_NOMBRE.Trim();
                            SW.WriteLine(Linea);
                            Nombres++;
                        }

                    }
                    catch (Exception ex)
                    {
                        AgregaError(ex);
                    }
                }
                SW.Close();

                IUWMSGNuevo = CalculaHashArchivo(NombreArchivo);
                return Nombres;

            }
            catch (Exception e)
            {
                AgregaError(e);
            }
            return -1;
        }
        /// <summary>
        /// indica que se deberá enviar la lista de blanca a la terminal
        /// </summary>
        /// <returns></returns>
        protected override Errores EnvioListaBlanca()
        {
            if (GetParameter(79, 0) == 1)
            {
                if (GeneraWMSG() > 0)
                {
                    if (IUWMSGNuevo != IUWMSG)
                    {
                        DeleFile("WMSG");
                        if (SendFile(Directorio + "WMSG", "WMSG", 0, 0) < 0)
                        {
                            AgregaError("Al enviar el archivo WMSG");
                           // Error = Errores.Error_Conexion;
                        }
                        else
                            IUWMSG = IUWMSGNuevo;
                    }
                }
            }
            CargaSitioHijo();
            if (GetParameter(44, 0) != 4)
            {
                AgregaLog("Solo Envia la lista blanca cuando el parámetro es 44");
                return Errores.Correcto;
            }




            GeneraAXCARDS();
            Errores Error = Errores.Correcto;
            if (m_LenTarjeta <= 0)
                return Error;
            GeneraAXPOOLS();
            GeneraAXTIMET();
            if (m_Sitio_Hijo_ID > 0)
            {
                GeneraAXGATES();
                GeneraAXTGATE();
            }

            if (IUAXCARDSNuevo != IUAXCARDS)
            {
                DeleFile("AXCARDS");
                if (SendFile(Directorio + "AXCARDS", "AXCARDS", 1, m_LenTarjeta / 2 + 1) < 0)
                {
                    AgregaError("Al enviar el archivo AXCARDS");
                    Error = Errores.Error_Conexion;
                }
                else
                    IUAXCARDS = IUAXCARDSNuevo;
            }
            if (IUAXPOOLSNuevo != IUAXPOOLS)
            {
                DeleFile("AXPOOLS");
                if (SendFile(Directorio + "AXPOOLS", "AXPOOLS", 0, 6) < 0)
                {
                    AgregaError("Al enviar el archivo AXPOOLS");
                    Error = Errores.Error_Conexion;
                }
                else
                    IUAXPOOLS = IUAXPOOLSNuevo;
            }
            if (IUAXTIMETNuevo != IUAXTIMET)
            {
                DeleFile("AXTIMET");
                if (SendFile(Directorio + "AXTIMET", "AXTIMET", 0, 16) < 0)
                {
                    AgregaError("Al enviar el archivo AXTIMET");
                    Error = Errores.Error_Conexion;
                }
                else
                    IUAXTIMET = IUAXTIMETNuevo;

            }
            if (m_Sitio_Hijo_ID > 0)
            {
                if (IUAXGATESNuevo != IUAXGATES)
                {
                    DeleFile("AXGATES");
                    if (SendFile(Directorio + "AXGATES", "AXGATES", 0, 1) < 0)
                    {
                        AgregaError("Al enviar el archivo AXGATES");
                        Error = Errores.Error_Conexion;
                    }
                    else
                        IUAXGATES = IUAXGATESNuevo;
                }

                if (IUAXTGATENuevo != IUAXTGATE)
                {
                    DeleFile("AXTGATE");
                    if (SendFile(Directorio + "AXTGATE", "AXTGATE", 1, 3) < 0)
                    {
                        AgregaError("Al enviar el archivo AXTGATE");
                        Error = Errores.Error_Conexion;
                    }
                    else
                        IUAXTGATE = IUAXTGATENuevo;
                }
            }


            return Error;
        }

        protected override Errores EnvioParametros()
        {
            //Solo se debería hacer la primera vez
            Tecnologias Tecno = (Tecnologias)m_DatosTerminal.TIPO_TECNOLOGIA_ID;
            if (Tecno != Tecnologias.Huella)
                return Errores.Correcto;
            SetParameter(80, "242");
            return Errores.Correcto;
        }



        protected override Errores PoleoVectoresSatisfactorio()
        {

            return Errores.Correcto;
        }

        protected override Errores PoleoVectores()
        {
            int R = DescargaHuellas();
            if (R > 0)
                return Errores.Correcto;
            if (R == -1)
                return Errores.No_Requiere_Cambio_En_Vectores;
            if (R == 0)
                return Errores.No_Requiere_Cambio_En_Vectores;
            return Errores.Error_Desconocido;
        }

        protected override Errores EnvioVectores()
        {
            int Ret = GeneraFinger();
            if (Ret < 0)
                return Errores.Error_Desconocido;
            if (IUVectores == IUVectoresNuevo)
                return Errores.Correcto;
            InhabilitaTrax(true);
            // EnviaMensage(1, "Desc. Huellas");
            DeleFile("FINGER");

            if (SendFile(Directorio + "FINGER", "FINGER", 0, 80) >= 0)
            {
                IUVectores = IUVectoresNuevo;
                AgregaLog("SE ha enviando el archivo Finger correctamente");
                Sleep(100);
                InhabilitaTrax(false);
                Sleep(100);
                return Errores.Correcto;
            }
            InhabilitaTrax(false);
            Sleep(100);
            return Errores.Error_Desconocido;
        }

        protected override Errores EnvioSinVector()
        {
            if (GeneraNoFinger() < 0)
                return Errores.Error_Desconocido;

            if (IUVectoresNo == IUVectoresNoNuevo)
                return Errores.Correcto;

            DeleFile("NOFINGER");
            Sleep(150);
            String ArchivoNoFinger = "NOFINGER";
            if (!System.IO.File.Exists(Directorio + ArchivoNoFinger))
            {
                IUVectoresNo = IUVectoresNoNuevo;
                return Errores.Correcto;
            }
            int Res = SendFile(Directorio + ArchivoNoFinger, ArchivoNoFinger, 0, 12);
            if (Res > 0)
            {
                IUVectoresNo = IUVectoresNoNuevo;
                return Errores.Correcto;
            }

            return Errores.Error_IO;
        }
        protected override Errores EnviaMensage(int Linea, string Mensage)
        {
            if (Linea >= 4 || Linea < 0)
                return Errores.Error_Parametros;
            Byte[] MensageByte = new byte[Linea + 1 + 4 + Mensage.Length];
            MensageByte[0] = Convert.ToByte('%');
            MensageByte[1] = Convert.ToByte('S');
            MensageByte[2] = 30;
            for (int Cont = 0; Cont < Linea; Cont++)
                MensageByte[3 + Cont] = 124;
            Array.Copy(ObtenArregloBytes(Mensage), 0, MensageByte, 3 + Linea, Mensage.Length);
            MensageByte[3 + Linea + Mensage.Length] = 0;
            PostCommand(MensageByte);
            return Errores.Correcto;
        }
        public int ProcesaArchivoLog(string NombreArchivo)
        {
            try
            {
                int Checadas = 0;
                string SFecha = NombreArchivo.Substring(NombreArchivo.Length - 5);
                DateTime Fecha = new DateTime(2010 +
                    Convert.ToInt32(SFecha.Substring(0, 1)),
                    Convert.ToInt32(SFecha.Substring(1, 2)),
                    Convert.ToInt32(SFecha.Substring(3, 2))
                    );
                StreamReader SR = File.OpenText(Directorio + NombreArchivo);
                string Linea = "";
                while (!SR.EndOfStream)
                {
                    try
                    {
                        Linea = SR.ReadLine();
                        AgregaDebug("ProcesaArchivoLog Linea {0:G}", Linea);
                        DateTime FechaHora = Fecha;
                        if (m_LenHora == 6)
                            FechaHora += new TimeSpan(
                                Convert.ToInt32(Linea.Substring(0, 2)),
                                Convert.ToInt32(Linea.Substring(2, 2)),
                                Convert.ToInt32(Linea.Substring(4, 2)));
                        else
                            FechaHora += new TimeSpan(
                                Convert.ToInt32(Linea.Substring(0, 2)),
                                Convert.ToInt32(Linea.Substring(2, 2)),0);
                        TipoAccesos TAcceso;
                        switch (Linea.Substring(m_LenHora,1))
                        {
                        case "0":
                            TAcceso = TipoAccesos.Entrada;
                        	break;
                        case "1":
                            TAcceso = TipoAccesos.Salida;
                            break;
                        default:
                            TAcceso = TipoAccesos.Correcto;
                            break;
                        }
                        if(!m_EsEntrada && !m_EsSalida)
                            TAcceso = TipoAccesos.Correcto;

                        
                        Linea = Linea.Substring(m_LenHora+1);

                        string ID = Linea.Substring(0, m_LenTarjeta);
                        //AgregaDebug("m_LenHora " + m_LenHora + ", Linea.Substring(0, m_LenTarjeta) {0:G}", ID);
                        /* if (ID.Length < 11)
                         {
                             string Once = "00000000000";
                             ID = Once.Substring(0, 11 - ID.Length) + ID;
                         }*/
                        try
                        {
                            ID = Convert.ToUInt64(ID).ToString();
                        }
                        catch (System.Exception e)
                        {

                            AgregaError(e);
                        }

                        int TerminalHija = 0;
                        if (m_LenEsclavos > 0)
                            TerminalHija = Convert.ToInt32(Linea.Substring(m_LenTarjeta, m_LenEsclavos));
                        if (m_LenEsclavos == 0 || TerminalHija == 0)
                        {
                            if (AgregaChecada(ID, FechaHora, TipoAccesos.Correcto))
                            {
                                Checadas++;
                                continue;
                            }
                        }
                        else
                        {
                            int IDTerminalAcceso = Terminal_ID;
                            foreach (WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESRow Terminal in TerminalesHijas)
                            {
                                CeC_Terminales TConexion = new CeC_Terminales();
                                TConexion.CargarCadenaConexion(Terminal.TERMINAL_DIR);
                                if (TConexion.NoTerminal == TerminalHija)
                                    IDTerminalAcceso = Convert.ToInt32(Terminal.TERMINAL_ID);
                            }
                            AgregaChecada(IDTerminalAcceso, ID, FechaHora, TipoAccesos.Correcto);
                            Checadas++;
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        AgregaError(ex);
                    }

                }
                SR.Close();
                return Checadas;
            }
            catch (Exception ex)
            {
                AgregaLog("ProcesaArchivoLog " + NombreArchivo );
                AgregaError(ex);
            }
            return 0;
        }



        protected override Errores AsignaFechaHora(DateTime FechaHora)
        {
            int R = PonFechaHoraR(FechaHora);
            if (R == 0)
                return Errores.Correcto;
            return Errores.Error_Desconocido;
        }

        public override Errores PoleoChecadas()
        {
            Byte[] Archivos = new byte[60000];
            CargaSitioHijo();
            m_EsEntrada = TERMINAL_ESENTRADA;
            m_EsSalida = TERMINAL_ESSALIDA;
            IUChecadasNuevo = "";
            AgregaDebug("PoleoChecadas ");
            int lRetVal = ListFiles("PRE", Archivos);

            int NArchivos = lRetVal;

            if (NArchivos <= 0)
            {
                AgregaDebug("PoleoChecadas ret {0:D}", lRetVal);
                return Errores.Correcto;
            }
            int Cont;
            if (m_LenTarjeta <= 0)
                m_LenTarjeta = GetParameter(43, 0);
            if (m_LenTarjeta <= 0)
                return Errores.Error_Parametros;
            if (GetParameter(1, 0) < 10)
                m_LenHora = 6;
            else
                m_LenHora = 4;

            if (GetParameter(18, 0) == 6)
                m_LenEsclavos = 2;
            else
                m_LenEsclavos = 0;
            bool HayCambios = false;
            for (Cont = 0; Cont < NArchivos; Cont++)
            {
                string NArchivo = ObtenString(Archivos, 16 * Cont, 8);
                try
                {
                    if (FechaDeArchivo(NArchivo) == FechaHora.Date)
                    {
                        IUChecadasNuevo = ObtenString(Archivos, 16 * Cont + 9, 6);
                        AgregaDebug("Len " + IUVectoresEnrollNuevo);

                        if (IUChecadasNuevo == IUChecadas)
                        {

                            return Errores.Correcto;
                        }
                        else
                            HayCambios = true;
                        break;
                    }
                }
                catch (System.Exception e)
                {
                	
                }

            }

            if (!HayCambios)
            {
                AgregaLog("No hay cambios en checadas pero puede haber un archivo sin descargar");
                return Errores.Correcto;
            }

            for (Cont = 0; Cont < NArchivos; Cont++)
            {
                string NArchivo = ObtenString(Archivos, 16 * Cont, 8);
                lRetVal = DescargaArchivo(NArchivo);
                if (lRetVal < 0)
                    return Errores.Error_Conexion;

                bool Actual = false;

                ProcesaArchivoLog(NArchivo);

                Sleep(50);


            }

            return Errores.Correcto;
        }
        protected override Errores BorraChecadas()
        {

            Byte[] Archivos = new byte[60000];

            AgregaLog("BorraChecadas ");
            int lRetVal = ListFiles("PRE", Archivos);

            int NArchivos = lRetVal;

            if (NArchivos <= 0)
            {
                AgregaError("BorraChecadas ret "+ lRetVal);
                return Errores.Correcto;
            }
            for (int Cont = 0; Cont < NArchivos; Cont++)
            {

                string NArchivo = ObtenString(Archivos, 16 * Cont, 8);
                if (FechaDeArchivo(NArchivo) != FechaHora.Date)
                {
                    AgregaLog("BorraChecadas " + NArchivo);
                    DeleFile(NArchivo);

                    Sleep(50);
                }
            }
            return Errores.Correcto;
        }
    }
}
