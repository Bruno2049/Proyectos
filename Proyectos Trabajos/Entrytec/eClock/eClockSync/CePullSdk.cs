using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
//using Axzkonline;
//using System;
//using System.Collections.Generic;
//using System.Text;
using System.Diagnostics;
//using System.Runtime.InteropServices;
//using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace eClockSync
{
    class CePullSdk : CeTerminalSync
    {
        IntPtr h = IntPtr.Zero;

        /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo user
        /// </summary>
        protected string IUuserNuevo = "";

        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo user
        /// </summary>
        protected string IUuser
        {
            get { return ObtenParametro("IUuser", ""); }
            set { AsignaParametro("IUuser", value); }
        }


                /// <summary>
        /// Contiene la longitud o HASH del nuevo archivo userauthorize
        /// </summary>
        protected string IUuserauthorizeNuevo = "";

        /// <summary>
        /// Contiene la longitud o HASH del ultimo archivo userauthorize
        /// </summary>
        protected string IUuserauthorize
        {
            get { return ObtenParametro("IUuserauthorize", ""); }
            set { AsignaParametro("IUuserauthorize", value); }
        }


        

        private int m_NoChecadas = 0;
        public bool EsiClock = false;
        private object m_Acepta_TA = null;
        public TipoAccesos ObtenTipoAcceso(int TipoAccesoTerminal)
        {
            if (m_Acepta_TA == null)
                m_Acepta_TA = TERMINAL_ACEPTA_TA;
            if (!Convert.ToBoolean(m_Acepta_TA))
                return TipoAccesos.Correcto;
            TipoAccesos TA = TipoAccesos.Correcto;
            switch (TipoAccesoTerminal)
            {
                case 1: TA = TipoAccesos.Correcto; break;
                case 0:
                    TA = TipoAccesos.Salida; break;
            }
            return TA;
        }

        [DllImport("plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();

        List<CePullSdk> Lectoras = new System.Collections.Generic.List<CePullSdk>();

        public override bool Conecta()
        {
            try
            {

                if (m_TConexion.NoTerminal == 0)
                    m_TConexion.NoTerminal = 1;
                m_Conectado = false;
                string Conexion = "";
                switch (m_TConexion.TipoConexion)
                {
                    case CeC_Terminales.tipo.Red:
                        try
                        {
                            Conexion = "protocol=TCP,ipaddress=" + m_TConexion.Direccion + ",port=" + m_TConexion.Puerto + ",timeout=2000,passwd=";

                        }
                        catch (Exception ex) { AgregaError(ex); AgregaError("Posiblemente el activex no este disponible"); return false; }
                        break;
                    case CeC_Terminales.tipo.RS485:
                        Conexion = "protocol=RS485,port=COM" + m_TConexion.Puerto + ",baudrate=" + m_TConexion.Velocidad + "bps,deviceid=" + m_TConexion.NoTerminal + ",timeout=2000,passwd=";
                        break;
                }
                h = Connect(Conexion);
                if (h != IntPtr.Zero)
                {
                    m_Conectado = true;
                    int SitioHijoID = SITIO_HIJO_ID;
                    Lectoras.Clear();
                    if (SitioHijoID > 0)
                    {
                        WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESDataTable Terminales = ws_eCheck.ObtenTerminales(SitioHijoID);
                        foreach (WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESRow Terminal in Terminales)
                        {
                            CePullSdk Dat = new CePullSdk();
                            Dat.Inicia(Terminal, ws_eCheck);
                            if (Dat.m_TConexion.NoTerminal > 0)
                            {
                                if (Lectoras.Count < Dat.m_TConexion.NoTerminal)
                                {
                                    for (int Cont = Lectoras.Count; Cont < Dat.m_TConexion.NoTerminal; Cont++)
                                    {
                                        CePullSdk Lectora = new CePullSdk();
                                        
                                        Lectoras.Add(Lectora);
                                    }
                                }
                                Lectoras[Dat.m_TConexion.NoTerminal - 1].Inicia(Terminal, ws_eCheck);

                            }
                        }


                    }


                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                CeLog2.AgregaError(ex);
                Desconecta();
            }


            return false;
        }

        //4.2 call Disconnect function
        [DllImport("plcommpro.dll", EntryPoint = "Disconnect")]
        public static extern void Disconnect(IntPtr h);

        public override bool Desconecta()
        {

            if (m_Conectado)
                Disconnect(h);
            //            m_ZKActivex.Dispose();
            //m_FrmZK.Dispose();
            /*            m_ZKActivex = null;
                        m_FrmZK = null;*/

            return m_Conectado = false;
        }

        [DllImport("plcommpro.dll", EntryPoint = "GetDeviceData")]
        public static extern int GetDeviceData(IntPtr h, ref byte buffer, int buffersize, string tablename, string filename, string filter, string options);

        public override Errores PoleoChecadas()
        {
            try
            {
                if (!m_Conectado)
                    return Errores.Error_Conexion;
                m_NoChecadas = ObtenNoChecadas();
                int BUFFERSIZE = 1 * 1024 * 1024;
                byte[] buffer = new byte[BUFFERSIZE];
                string str = "Cardno\tPin\tVerified\tDoorID\tEventType\tInOutState\tTime_second";
                string devtablename = "transaction";
                int ret = 0;
                ret = GetDeviceData(h, ref buffer[0], BUFFERSIZE, devtablename, str, "", "");
                m_Checadas = new eClockSync.WS_eCheck.DS_WSPersonasTerminales.DT_CHECADASDataTable();
                foreach (CePullSdk Pull in Lectoras)
                    Pull.m_Checadas = new eClockSync.WS_eCheck.DS_WSPersonasTerminales.DT_CHECADASDataTable();

                if (ret >= 0)
                {
                    
                    string Texto = Encoding.Default.GetString(buffer);
                    try
                    {
                        System.IO.File.WriteAllText(CeLog2.S_NombreDestino + Terminal_ID.ToString() + " tr" + DateTime.Now.ToString("HHMM") + ".txt", Texto);
                    }
                    catch { }
                    System.IO.StringReader SR = new System.IO.StringReader(Texto);
                    bool Cabecera = true;
                    while (true)
                    {
                        string Linea = "";
                        string[] Columnas = null;
                        try
                        {
                            Linea = SR.ReadLine();
                            if (Linea == null)
                                break;
                            if (Cabecera)
                            {
                                Cabecera = false;
                                continue;
                            }
                            Columnas = Linea.Split(new char[] { ',' });
                            if (Columnas.Length > 1)
                                AgregaChecada(Columnas[0], Columnas[1], Convert.ToInt32(Columnas[2]), Convert.ToInt32(Columnas[3]), Convert.ToInt32(Columnas[4]), Convert.ToInt32(Columnas[5]), Convert.ToInt32(Columnas[6]));
                            //                            return Errores.Error_IO;

                        }
                        catch (Exception ex)
                        {
                            CeLog2.AgregaError(ex);
                            return Errores.Error_Desconocido;
                        }

                    }
                    int NOC = ObtenNoChecadas();
                    m_Checadas = new eClockSync.WS_eCheck.DS_WSPersonasTerminales.DT_CHECADASDataTable();
                    foreach (CePullSdk Pull in Lectoras)
                        if (Pull.Terminal_ID > 0 && Pull.m_Checadas != null && Pull.m_Checadas.Rows.Count > 0)
                            if (!ws_eCheck.EnviaChecadas(Pull.Terminal_ID, Pull.m_Checadas))
                            {
                                AgregaError("ws_eCheck.EnviaChecadas No se pudieron enviar checadas de " + Pull.Terminal_ID);
                                return Errores.Error_Conexion;
                            }

                    /*if (Terminal_ID > 0 && m_Checadas != null && m_Checadas.Rows.Count > 0)
                        if (!ws_eCheck.EnviaChecadas(Terminal_ID, m_Checadas))
                        {
                            AgregaError("ws_eCheck.EnviaChecadas No se pudieron enviar checadas de " + Terminal_ID);
                            return Errores.Error_Conexion;
                        }*/


                    return Errores.Correcto;
                }
                return Errores.Error_IO;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
            return Errores.Error_Desconocido;

        }

        bool AgregaChecada(string CardNO, string Pin, int Verified, int DoorID, int EventType, int InOutState, int Time_second)
        {
            try
            {
                if (Pin == "0")
                    return false;
                /*if(Pin != "6" && )
                    return false;*/
                CePullSdk EquipoAcceso = null;
                if (DoorID > 0 && DoorID <= Lectoras.Count)
                    EquipoAcceso = Lectoras[DoorID - 1];
                else
                    EquipoAcceso = this;
                TipoAccesos TAcceso = TipoAccesos.Incorrecto;
                if(EventType == 0 || EventType == 24)
                    TAcceso = TipoAccesos.Correcto;
                /*if (Verified == 6)
                    TAcceso = TipoAccesos.Correcto;*/
                EquipoAcceso.AgregaChecada(Pin, Int2DateTime(Time_second), TAcceso);
                return true;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
            return false;
        }

        //4.8 call GetDeviceDataCount function

        [DllImport("plcommpro.dll", EntryPoint = "GetDeviceDataCount")]
        public static extern int GetDeviceDataCount(IntPtr h, string tablename, string filter, string options);


        public int ObtenNoChecadas()
        {
            int _value = 0;
            int ret = GetDeviceDataCount(h, "transaction", "", "");
            if (ret >= 0)
            {
                //MessageBox.Show("ret=" + ret);
                return ret;
            }
            /*if (m_ZKActivex.GetDeviceStatus(1, 6, ref _value))
                return _value;*/
            return -9999;
        }
        private DateTime ObtenFechaHora()
        {
            /* int _year = 0;
             int _month = 0;
             int _day = 0;
             int _hour = 0;
             int _minute = 0;
             int _seccond = 0;
             if (m_ZKActivex.GetDeviceTime(m_TConexion.NoTerminal, ref _year, ref _month, ref _day, ref _hour, ref _minute, ref _seccond))
             {
                 return new DateTime(_year, _month, _day, _hour, _minute, _seccond);
             }*/
            return DateTime.Now;
        }
        [DllImport("plcommpro.dll", EntryPoint = "DeleteDeviceData")]
        public static extern int DeleteDeviceData(IntPtr h, string tablename, string data, string options);
        protected override Errores BorraChecadas()
        {
            if (m_NoChecadas == 0 || ObtenNoChecadas() != m_NoChecadas)
                return Errores.No_Requiere;
            //return Errores.Error_Desconocido;
            int ret = DeleteDeviceData(h, "transaction", "", "");
            if (ret >= 0)
                return Errores.Correcto;
            return Errores.Error_IO;
        }

        [DllImport("plcommpro.dll", EntryPoint = "SetDeviceParam")]
        public static extern int SetDeviceParam(IntPtr h, string itemvalues);

        int DateTime2Int(DateTime FechaHora)
        {
            DateTime dt = FechaHora;
            //MessageBox.Show("Now datetime is:" + dt);
            int tt = ((dt.Year - 2000) * 12 * 31 + (dt.Month - 1) * 31 + (dt.Day - 1)) * (24 * 60 * 60) + dt.Hour * 60 * 60 + dt.Minute * 60 + dt.Second;
            return tt;
        }

        DateTime Int2DateTime(int FechaHora)
        {
            DateTime DT = new System.DateTime(2000, 01, 01);
            int R = FechaHora;
            DT = DT.AddSeconds(R % 60);
            R = R / 60;
            DT = DT.AddMinutes(R % 60);
            R = R / 60;
            DT = DT.AddHours(R % 24);
            R = R / 24;
            DT = DT.AddDays(R % 31);
            R = R / 31;
            DT = DT.AddMonths(R % 12);
            R = R / 12;
            DT = DT.AddYears(R);
            return DT;
        }

        override protected Errores AsignaFechaHora(DateTime FechaHora)
        {

            if (!m_Conectado)
                return Errores.Error_Conexion;

            DateTime dt = DateTime.Now;
            //MessageBox.Show("Now datetime is:" + dt);
            int tt = ((dt.Year - 2000) * 12 * 31 + (dt.Month - 1) * 31 + (dt.Day - 1)) * (24 * 60 * 60) + dt.Hour * 60 * 60 + dt.Minute * 60 + dt.Second;
            int Ret = 0;
            Ret = SetDeviceParam(h, "DateTime=" + tt);
            if (Ret >= 0)
            {
                return Errores.Correcto;
            }
            AgregaError("No se pudo AsignaFechaHora Error = ");
            return Errores.Error_IO;
        }

        //4.6 call SetDeviceData function

        [DllImport("plcommpro.dll", EntryPoint = "SetDeviceData")]
        public static extern int SetDeviceData(IntPtr h, string tablename, string data, string options);

        /// <summary>
        /// indica que se deberá enviar la lista de blanca a la terminal
        /// </summary>
        /// <returns></returns>
        protected override Errores EnvioListaBlanca()
        {
            List<WS_eCheck.DS_WSPersonasTerminales> DatosPersonasTerminales = new System.Collections.Generic.List<WS_eCheck.DS_WSPersonasTerminales>();
            foreach (CePullSdk Pull in Lectoras)
            {
                if (Pull.Terminal_ID > 0)
                {
                    WS_eCheck.DS_WSPersonasTerminales DS = m_ws_eCheck.ListaEmpleados(Pull.Terminal_ID);
                    if (DS != null && DS.DT_PersonasTerminales.Rows.Count > 0)
                    {
                        Pull.m_DatosEmpleados = DS.DT_PersonasTerminales;
                    }
                }
            }

            //Usuarios
            System.IO.StringWriter user = new StringWriter();
            //user.WriteLine("CardNo,Pin,Password,Group\tStartTime\tEndTime");

            //Usuarios
            System.IO.StringWriter userauthorize = new StringWriter();
            //userauthorize.WriteLine("Pin\tAuthorizeTimezoneId\tAuthorizeDoorId");
            System.IO.StringWriter SR = new System.IO.StringWriter();
            bool CampoAdicional = false;
            if (!this.m_DatosTerminal.IsTERMINAL_CAMPO_ADICIONALNull() && m_DatosTerminal.TERMINAL_CAMPO_ADICIONAL != "")
                CampoAdicional = true;
            Errores Error = Errores.Correcto;
            foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
            {
                try
                {

                    if (Fila.IsPERSONAS_A_VEC_T1Null() || Fila.PERSONAS_A_VEC_T1 == "")
                    {
                        AgregaError("Sin Campo llave PersonaLinkID = " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                        continue;
                    }
                    string Pin = Fila.PERSONAS_A_VEC_T1;
                    string CardNo = Pin;
                    if (CampoAdicional)
                        CardNo = Fila.PERSONAS_A_VEC_T2;
                    user.WriteLine("Pin=" + Pin + "\tCardNo=" + CardNo);
                    userauthorize.WriteLine("Pin=" + Pin + "\tAuthorizeTimezoneId=1\tAuthorizeDoorId=" + ObtenDoorsIds(Convert.ToInt32(Fila.PERSONA_ID)).ToString());
                }
                catch (Exception ex)
                {
                    AgregaError(ex);
                }
            }
            IUuserNuevo= CalculaHash(user.ToString());
            IUuserauthorizeNuevo = CalculaHash(userauthorize.ToString());

            if (IUuser != IUuserNuevo)
            {
                DeleteDeviceData(h, "user", "CardNo\tPin\tPassword\tGroup\tStartTime\tEndTime", "");
                AgregaLog(user.ToString());
                if (SetDeviceData(h, "user", user.ToString(), "") >= 0)
                {
                    IUuser = IUuserNuevo;
                }
                else
                {
                    AgregaError("Al enviar el archivo user");
                    Error = Errores.Error_Conexion;
                }
            }
            if (IUuserauthorize != IUuserauthorizeNuevo)
            {
                DeleteDeviceData(h, "userauthorize", "Pin\tAuthorizeTimezoneId\tAuthorizeDoorId", "");
                AgregaLog(userauthorize.ToString());
                if (SetDeviceData(h, "userauthorize", userauthorize.ToString(), "") >= 0)
                {
                    IUuserauthorize = IUuserauthorizeNuevo;
                }
                else
                {
                    AgregaError("Al enviar el archivo userauthorize");
                    Error = Errores.Error_Conexion;
                }
            }

            return Error;
        }

        int ObtenDoorsIds(int Persona_ID)
        {
            int R = 0;
            for (int Cont = 1; Cont <= Lectoras.Count; Cont++)
            {
                if (EnPuerta(Persona_ID, Cont))
                    R += 1 << (Cont - 1);
            }
            return R;
        }

        bool EnPuerta(int Persona_ID, int Puerta)
        {
            if (Puerta < 1 && Puerta > Lectoras.Count)
                return false;
            try
            {
                if (Lectoras[Puerta - 1].m_DatosEmpleados.FindByPERSONA_ID(Persona_ID) != null)
                    return true;

            }
            catch { }
            return false;
        }

        protected override void CambioTimeZoneRemota()
        {
            foreach (CePullSdk Lectora in Lectoras)
                Lectora.TimeZoneRemota = TimeZoneRemota;
        }
    }
}
