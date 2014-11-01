using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace eClockSync5
{
    class CeClockTerm : CeTerminalSync
    {
        static bool gIniciado = false;
        int m_SerieNumero = 0;
        private int m_NoChecadas = 0;
        int m_TEspera = 0;
        public override bool Conecta()
        {
            try
            {
                if (m_TConexion.NoTerminal == 0)
                    m_TConexion.NoTerminal = 1;
                m_SerieNumero = m_TConexion.NoTerminal;
                m_Conectado = false;
                switch (m_TConexion.TipoConexion)
                {
                    case CeC_Terminales.tipo.Red:
                        //m_SerieNumero = m_TConexion.NoTerminal;
                        if (CeClockTerminal.CKT_RegisterNet(m_SerieNumero, m_TConexion.Direccion) != 1)
                        {
                            AgregaError("No se pudo conectar a Red " + m_TConexion.Direccion + ":" + m_TConexion.Puerto);
                            return false;
                        }
                        int Personas = 0;
                        int Huellas = 0;
                        int Checadas = 0;
                        if (CeClockTerminal.CKT_GetCounts(m_SerieNumero, ref Personas, ref Huellas, ref Checadas) != 1)
                        {
                            AgregaError("No se pudo conectar a Red (CKT_GetCounts)" + m_TConexion.Direccion + ":" + m_TConexion.Puerto);
                            return false;
                        }
                        AgregaLog("Conectado : " + Personas + " Persona(s), " + Huellas + " Huella(s), " + Checadas + " Checada(s)");
                        break;
                    case CeC_Terminales.tipo.USB:
                        {
                            if (CeClockTerminal.CKT_RegisterUSB(m_SerieNumero, 0) != 1)
                            {
                                string[] Puertos = System.IO.Ports.SerialPort.GetPortNames();
                                AgregaLog("intentando conectar por Serial");
                                foreach (string Puerto in Puertos)
                                {
                                    AgregaLog("intentando conectar con el puerto " + Puerto);
                                    if (CeClockTerminal.CKT_RegisterSno(m_SerieNumero, Convert.ToInt32(Puerto.Substring(3))) == 1)
                                    {
                                        AgregaLog("Conectado por el puerto " + Puerto);
                                        m_Conectado = true;
                                        return true;
                                    }
                                }
                                AgregaError("No se pudo conectar a USB " + m_TConexion.NoTerminal);
                                return false;
                            }
                        }
                        break;
                    case CeC_Terminales.tipo.Serial:

                        if (CeClockTerminal.CKT_RegisterSno(m_SerieNumero, m_TConexion.Puerto) != 1)
                        {
                            AgregaError("No se pudo conectar a Serial " + m_TConexion.Puerto + ":" + m_SerieNumero + ":" + m_TConexion.Velocidad);
                            return false;
                        }
                        break;
                }

                m_Conectado = true;
                return true;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
                Desconecta();
            }


            return false;
        }

        public override bool Desconecta()
        {
            if (!gIniciado)
                return false;
            if (m_Conectado)
            {
                CeClockTerminal.CKT_UnregisterSnoNet(m_SerieNumero);
                CeClockTerminal.CKT_Disconnect();

            }
            //            m_ZKActivex.Dispose();

            /*            m_ZKActivex = null;
                        m_FrmZK = null;*/
            gIniciado = false;
            return m_Conectado = false;
        }

        protected string ObtenIDEmpleado(WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila)
        {
            string IDEmpleado = Convert.ToInt32(Fila.PERSONAS_A_VEC_T1).ToString();
            return IDEmpleado;

            ///Solo obtiene el campo llave
            try
            {
                if (Fila.PERSONAS_A_VEC_T2.Length > 0)
                {
                    uint NS = Convert.ToUInt32(Fila.PERSONAS_A_VEC_T2);
                    IDEmpleado = Convert.ToInt32(Fila.PERSONA_LINK_ID).ToString("") + Convert.ToInt32(NS % 10000).ToString("0000");
                }
            }
            catch { }
            return IDEmpleado;
        }

        private void EsperaEntreComando()
        {
            Sleep(m_TEspera);


        }

        private void AgregaNoSePudoCargar(WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila, int Respuesta, int NoHuella)
        {
            switch (Respuesta)
            {
                case 3: AgregaError("No se pudo cargar la huella " + NoHuella + " (No es Persona) Persona = " + Fila.PERSONA_ID + " NoEmpleado = " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                    break;
                case 0:
                    AgregaError("No se pudo cargar la huella " + NoHuella + " Persona = " + Fila.PERSONA_ID + " NoEmpleado = " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                    break;
                case 6: AgregaError("No se pudo cargar la huella " + NoHuella + " (error de Checksum) Persona = " + Fila.PERSONA_ID + " NoEmpleado = " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                    break;

            }
        }
        public bool RegularizaHuella(ref byte[] Huella)
        {
            try
            {
                if (Huella[1] != 2)
                {

                    AgregaLog("Regularizando Huella");
                    Huella[0] = 192;
                    Huella[1] = 2;
                    Huella[2] = 0;
                    Huella[3] = 0;
                    Huella[4] = 0;
                    Huella[5] = 0;
                    Huella[6] = 0;
                    Huella[7] = 169;
                    return true;
                }
            }
            catch { }
            return false;
        }
        static string gNArchivo = "";
        protected override Errores EnvioVectores()
        {
            bool EsTarjetaOHuella = TERMINAL_TARJETA_O_HUELLA;
            if (EsTarjetaOHuella)
                AgregaLog("Terminal configurada para usar tarjeta o huella");
            int ErroresGenerados = 0;
            if (m_DatosEmpleados == null)
                return Errores.Error_Desconocido;

            if (m_DatosEmpleados.Rows.Count <= 0)
            {
                IUVectoresNuevo = "";
                return Errores.No_Requiere_Cambio_En_Vectores;
            }
            if (!m_Conectado)
                return Errores.Error_Conexion;


            foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
            {
                try
                {
                    if (ObtenHashPersona(Fila) == ObtenUltimoHashPersona(Fila.PERSONA_ID))
                    {
                        AgregaLog("Omitiendo, información idéntica" + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                        continue;
                    }

                    bool Agregar = false;
                    byte[] Huella1 = null;
                    byte[] Huella2 = null;
                    int NoHuellas = 0;


                    try
                    {
                        if (!Fila.IsPERSONAS_A_VEC_T1Null() && Fila.PERSONAS_A_VEC_1.Length > 0)
                        {
                            Huella1 = Fila.PERSONAS_A_VEC_1;
                            NoHuellas++;
                        }
                    }
                    catch { }

                    try
                    {
                        if (!Fila.IsPERSONAS_A_VEC_T2Null() && Fila.PERSONAS_A_VEC_2.Length > 0)
                        {
                            Huella2 = Fila.PERSONAS_A_VEC_2;
                            NoHuellas++;
                        }
                    }
                    catch { }
                    string Clave = "";
                    string IDEmpleado = ObtenIDEmpleado(Fila);
                    int iIDEmpleado = Convert.ToInt32(IDEmpleado);
                    //IDEmpleado = Fila.PERSONAS_A_VEC_T2;// Convert.ToInt32(Fila.PERSONA_LINK_ID).ToString();
                    /*if (IDEmpleado.Length < 8 || IDEmpleado.Length > 10)
                        continue;*/
                    string Nombre = Fila.PERSONA_NOMBRE;

                    bool Correcto = true;
                    bool PersonaSinHuella = false;
                    bool PersonaSupervisor = false;

                    if (Fila.IsPERSONA_ID_S_HUELLANull() || Fila.PERSONA_ID_S_HUELLA == 0)
                        if (NoHuellas < 1)
                        {
                            //AgregaLog("La persona " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE + " no tiene permitido ingresar sin huella, no se cargo");
                            continue;
                        }
                    if (!Fila.IsPERSONA_ID_S_HUELLANull() && Fila.PERSONA_ID_S_HUELLA != 0)
                    {

                        if (!Fila.IsPERSONA_S_HUELLA_CLAVENull() && Fila.PERSONA_S_HUELLA_CLAVE != "")
                            Clave = Fila.PERSONA_S_HUELLA_CLAVE.Replace(",", "");
                        else
                            if (!Fila.IsPERSONAS_A_VEC_2Null())
                                Clave = Fila.PERSONAS_A_VEC_T2;
                        if (Clave.IndexOf('*') >= 0)
                            PersonaSupervisor = true;
                        Clave = Clave.Replace("*", "");
                        if (Clave.Length > 6)
                            Clave.Substring(0, 6);
                        if (Clave != "")
                            PersonaSinHuella = true;
                        //Clave = Clave.PadLeft(8, '0');
                    }


                    Encoding u8 = Encoding.UTF8;
                    CeClockTerminal.PERSONINFO PI = new CeClockTerminal.PERSONINFO();
                    //PI.Name = u8.GetBytes(Nombre.PadRight(12).Substring(0, 12));
                    PI.Name = ObtenArregloBytes(Nombre, 12, 0);
                    //PI.CardNo = 4294967295;
                    PI.Group = 1;
                    PI.CardNo = -1;
                    if (PersonaSupervisor)
                        PI.Other = 1;
                    PI.Password = ObtenArregloBytes(Clave, 8, 0);
                    if (Clave.Length > 0)
                        AgregaLog("Preparando password " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE + " PI.Password = " + PI.Password.Length + " " + PI.Password.ToString());
                    //PI.Password = String2Bcd("".PadLeft(16, '0'));
                    if (!Fila.IsPERSONAS_A_VEC_T2Null() && Fila.PERSONAS_A_VEC_T2.Trim().Length > 0)
                    {
                        try
                        {
                            PI.CardNo = Convert.ToInt32(Fila.PERSONAS_A_VEC_T2);
                        }
                        catch { }
                    }
                    /* for(int Cont = 0;  Cont < 12;Cont ++)
                         PI.Name[Cont] = bNombre[Cont];*/
                    PI.PersonID = iIDEmpleado;


                    if (!EsTarjetaOHuella && NoHuellas > 0 && PI.CardNo > 0 && !PersonaSinHuella)
                        PI.KQOption = 1;
                    int iIntento;
                    for (iIntento = 0; iIntento < 4; iIntento++)
                    {
                        if (CeClockTerminal.CKT_ModifyPersonInfo(m_SerieNumero, ref PI) != 1)
                        {
                            AgregaError("CeClockTerminal.CKT_ModifyPersonInfo -> (Verifique ID TErminal) " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                            Correcto = false;
                        }
                        else
                        {
                            EsperaEntreComando();
                            Correcto = true;
                            break;
                        }
                        Sleep(100);
                    }
                    if (!Correcto || iIntento >= 2)
                    {
                        Correcto = Correcto;

                        return Errores.Error_Conexion;
                    }


                    byte[] Huella = new byte[384 * 2 * NoHuellas];
                    if (NoHuellas > 0 && Correcto && !PersonaSinHuella)
                    {

                        int Respuesta = 0;
                        AgregaLog("Preparando Huella " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                        if (Huella1 != null)
                        {
                            RegularizaHuella(ref Huella1);
                            Respuesta = CeClockTerminal.CKT_PutFPTemplate(m_SerieNumero, iIDEmpleado, 0, Huella1, Huella1.Length);
                            if (Respuesta != 1)
                            {
                                AgregaNoSePudoCargar(Fila, Respuesta, 1);
                                Correcto = false;
                            }
                            else
                                EsperaEntreComando();
                        }

                        if (Huella2 != null)
                        {
                            RegularizaHuella(ref Huella2);
                            Respuesta = CeClockTerminal.CKT_PutFPTemplate(m_SerieNumero, iIDEmpleado, 1, Huella2, Huella2.Length);
                            if (Respuesta != 1)
                            {
                                AgregaNoSePudoCargar(Fila, Respuesta, 2);
                                Correcto = false;
                            }
                            else
                                EsperaEntreComando();
                        }




                    }
                    else
                    {

                        //Huella = null;
                    }

                    if (!Correcto)
                    {
                        System.IO.StringWriter Texto = new System.IO.StringWriter();
                        try
                        {
                            Fila.Table.WriteXml(Texto);
                        }
                        catch { }
                        AgregaError("No se pudo cargar la huella Persona = " + Fila.PERSONA_ID + " NoEmpleado = " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                        try
                        {

                            string NARchivo = CeLog2.S_NombreDestino + Terminal_ID.ToString() + " " + DateTime.Now.ToString("HHMM") + ".xml";
                            if (gNArchivo != NARchivo)
                            {
                                gNArchivo = NARchivo;
                                System.IO.File.WriteAllText(gNArchivo, Texto.ToString());
                            }
                        }
                        catch { }
                        Huella = null;
                        ErroresGenerados++;
                    }
                    else
                    {
                        //Si es correcta la transmisión
                        ConfirmaEmpleado(Fila);
                    }
                }
                catch (System.Exception e)
                {
                    ErroresGenerados++;
                    AgregaError(e);
                }
            }
            /// No importa si se borran los empleados que no estan en la BD aunque el cargar
            /// sus registros no fuera posible
            //if (ErroresGenerados == 0)
            {
                BorraUsuarios();
            }

            if (ErroresGenerados == 0)
                return Errores.Correcto;
            return Errores.Error_Desconocido;
        }
        private bool BorraUsuarios()
        {

            try
            {
                int pLongRun = 0;
                int RecordCont = 0;
                int RetCont = 0;
                int pPersonas = 0;
                int pTemp = 0;
                string ABorrar = "";
                CeClockTerminal.PERSONINFO PI = new CeClockTerminal.PERSONINFO();
                if (CeClockTerminal.CKT_ListPersonInfoEx(m_SerieNumero, ref pLongRun) == 1)
                {
                    int R = 1;
                    while (true)
                    {
                        R = CeClockTerminal.CKT_ListPersonProgress(pLongRun, ref RecordCont, ref RetCont, ref pPersonas);

                        if (R == 0)
                            break;
                        pTemp = pPersonas;
                        for (int i = 0; i < RetCont; i++)
                        {
                            try
                            {
                                CeClockTerminal.PCopyMemory(ref PI, pPersonas, Marshal.SizeOf(PI));
                                //PI.PersonID
                                string NEmp = PI.PersonID.ToString();
                                bool Borrar = true;
                                foreach (WS_eCheck.DS_WSPersonasTerminales.DT_PersonasTerminalesRow Fila in m_DatosEmpleados)
                                {
                                    string IDEmpleado = ObtenIDEmpleado(Fila);

                                    if (IDEmpleado == NEmp || Fila.PERSONA_LINK_ID.ToString() == NEmp)
                                    {
                                        Borrar = false;
                                        break;
                                    }
                                }

                                if (Borrar == true && NEmp != null)
                                {
                                    ABorrar = AgregaSeparador(ABorrar, PI.PersonID.ToString(), ",");
                                }
                            }
                            catch { }
                            pPersonas += Marshal.SizeOf(PI);
                        }
                        CeClockTerminal.CKT_FreeMemory(pTemp);
                        if (R == 1)
                            break;
                    }
                }

                string[] sAborrar = ObtenArregoSeparador(ABorrar, ",");
                foreach (string Borrar in sAborrar)
                {
                    CeClockTerminal.CKT_DeletePersonInfo(m_SerieNumero, Convert.ToInt32(Borrar), 255);
                }
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);

            }
            return false;
        }
        public override Errores PoleoChecadas()
        {
            try
            {
                CeClockTerminal.CLOCKINGRECORD Checada = new CeClockTerminal.CLOCKINGRECORD();
                Checada.Time = new byte[20];
                int LenChecada = System.Runtime.InteropServices.Marshal.SizeOf(Checada);
                int NoRegistro = 0;
                int RetCount = 0;
                int NoChecadas = 0;
                int ptChecadas = 0;
                int ptTemp = 0;
                m_NoChecadas = 0;
                int Resp = CeClockTerminal.CKT_GetClockingRecordEx(m_SerieNumero, ref NoChecadas);
                if (Resp == 1)
                {
                    for (; ; )
                    {
                        int Ret = CeClockTerminal.CKT_GetClockingRecordProgress(NoChecadas, ref NoRegistro, ref RetCount, ref ptChecadas);

                        if (Ret == 0)
                            break;
                        if (RetCount <= 0)
                            break;
                        ptTemp = ptChecadas;
                        if (RetCount > 10000)
                        {
                            AgregaLog("RetCount > 10000 saliendo");
                            break;
                        }
                        for (int Cont = 0; Cont < RetCount; Cont++)
                        {
                            CeClockTerminal.PCopyMemory(ref Checada, ptChecadas, LenChecada);
                            ptChecadas += LenChecada;

                            DateTime FechaHora = Convert.ToDateTime(Encoding.Default.GetString(Checada.Time));
                            //FechaHora = FechaHora.AddDays(-1);
                            AgregaChecada(Checada.PersonID.ToString(), FechaHora, TipoAccesos.Correcto);
                            m_NoChecadas++;
                        }
                        if (ptTemp != 0)
                            CeClockTerminal.CKT_FreeMemory(ptTemp);
                    }
                    return Errores.Correcto;
                }
                return Errores.No_Requiere;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
            return Errores.Error_Desconocido;

        }
        public int ObtenNoChecadas()
        {
            int _value = 0;
            int Personas = 0;
            int Checadas = 0;
            int Huellas = 0;

            if (CeClockTerminal.CKT_GetCounts(m_SerieNumero, ref Personas, ref Huellas, ref Checadas) == 1)
                return Checadas;
            return -9999;
        }
        private DateTime ObtenFechaHora()
        {
            CeClockTerminal.DATETIMEINFO FechaHora = new CeClockTerminal.DATETIMEINFO();

            if (CeClockTerminal.CKT_GetDeviceClock(m_SerieNumero, ref FechaHora) > 0)
            {
                return new DateTime(FechaHora.Year_Renamed, FechaHora.Month_Renamed, FechaHora.Day_Renamed, FechaHora.Hour_Renamed, FechaHora.Month_Renamed, FechaHora.Second_Renamed);
            }
            return DateTime.Now;
        }
        protected override Errores BorraChecadas()
        {
            if (ObtenNoChecadas() != m_NoChecadas && m_NoChecadas > 0)
                return Errores.No_Requiere;
            if (CeClockTerminal.CKT_ClearClockingRecord(m_SerieNumero, 0, 0) > 0)
                return Errores.Correcto;
            return Errores.Error_IO;
        }
        override protected Errores AsignaFechaHora(DateTime FechaHora)
        {
            DateTime FH = ObtenFechaHora();
            TimeSpan TS = FechaHora - FH;
            if (Math.Abs(TS.TotalSeconds) < 40)
                return Errores.No_Requiere;

            AgregaLog("AsignaFechaHora " + FechaHora.ToLongTimeString());
            if (CeClockTerminal.CKT_SetDeviceDate(m_SerieNumero, Convert.ToInt16(FechaHora.Year), Convert.ToByte(FechaHora.Month), Convert.ToByte(FechaHora.Day)) > 0 &&
                CeClockTerminal.CKT_SetDeviceTime(m_SerieNumero, Convert.ToByte(FechaHora.Hour), Convert.ToByte(FechaHora.Minute), Convert.ToByte(FechaHora.Second)) > 0)
            {
                return Errores.Correcto;
            }
            AgregaError("No se pudo AsignaFechaHora Error = ");
            return Errores.Error_IO;
        }

        protected override Errores PoleoVectores()
        {
            try
            {
                int pLongRun = 0;
                int RecordCont = 0;
                int RetCont = 0;
                int pPersonas = 0;
                int pTemp = 0;
                string ADescargar = "";
                CeClockTerminal.PERSONINFO PI = new CeClockTerminal.PERSONINFO();
                if (CeClockTerminal.CKT_ListPersonInfoEx(m_SerieNumero, ref pLongRun) == 1)
                {
                    int R = 1;
                    while (true)
                    {
                        R = CeClockTerminal.CKT_ListPersonProgress(pLongRun, ref RecordCont, ref RetCont, ref pPersonas);

                        if (R == 0)
                            break;
                        pTemp = pPersonas;
                        for (int i = 0; i < RetCont; i++)
                        {
                            try
                            {
                                CeClockTerminal.PCopyMemory(ref PI, pPersonas, Marshal.SizeOf(PI));

                                ADescargar = AgregaSeparador(ADescargar, PI.PersonID.ToString() + "|" + PI.FPMark.ToString(), ",");
                            }
                            catch { }
                            pPersonas += Marshal.SizeOf(PI);
                        }
                        CeClockTerminal.CKT_FreeMemory(pTemp);
                        if (R == 1)
                            break;
                    }
                }
                byte[] Vector = null;
                int LenVector = 0;
                string[] sPersonas = ObtenArregoSeparador(ADescargar, ",");
                foreach (string sPersona in sPersonas)
                {
                    string[] Valores = ObtenArregoSeparador(sPersona, "|");
                    int Persona = Convert.ToInt32(Valores[0]);
                    int FPMark = Convert.ToInt32(Valores[1]);
                    ///Actualizar para mas de 2 huellas
                    //Verifica si tiene huella 1 
                    if (FPMark == 1 || FPMark == 3)
                    {
                        if (CeClockTerminal.CKT_GetFPTemplate(m_SerieNumero, Persona, 0, ref Vector, ref LenVector) == 1)
                            AgregaHuella(Persona.ToString(), 0, Vector);
                    }
                    //Verifica si tiene huella 2
                    if (FPMark == 2 || FPMark == 3)
                    {
                        if (CeClockTerminal.CKT_GetFPTemplate(m_SerieNumero, Persona, 1, ref Vector, ref LenVector) == 1)
                            AgregaHuella(Persona.ToString(), 1, Vector);
                    }
                }
            }
            catch (Exception ex)
            {
                AgregaError(ex);
                return Errores.Error_Desconocido;
            }
            return Errores.Correcto;
        }

        public override bool IniciaEventos()
        {
            try
            {

                if (CeClockTerminal.CKT_NetDaemon() <= 0)
                {
                    return false;
                }
            }
            catch { }
            return true;
        }

        public override bool ConsultaEstadoTerminal()
        {
            int Usuarios = 0;
            CeClockTerminal.CLOCKINGRECORD Checada = new CeClockTerminal.CLOCKINGRECORD();
            Checada.Time = new byte[20];
            int LenChecada = System.Runtime.InteropServices.Marshal.SizeOf(Checada);
            int ptChecadas = 0;
            int NoChecadas = CeClockTerminal.CKT_ReadRealtimeClocking(ref ptChecadas);
            if (NoChecadas > 0)
            {
                for (int Cont = 0; Cont < NoChecadas; Cont++)
                {
                    CeClockTerminal.PCopyMemory(ref Checada, ptChecadas, LenChecada);
                    ptChecadas += LenChecada;

                    DateTime FechaHora = Convert.ToDateTime(Encoding.Default.GetString(Checada.Time));

                    AgregaChecada(Checada.PersonID.ToString(), FechaHora, TipoAccesos.Correcto,true);
                }
                CeClockTerminal.CKT_FreeMemory(ptChecadas);
            }
            return true;
        }
    }
}
