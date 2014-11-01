using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using RecogSys.RdrAccess;

namespace eClockSync
{
    class CeHp2 : CeTerminalSync
    {




        const int TRUE = 1;                           // Constants to deal with int return of BOOL methods
        const int FALSE = 0;

        CRsiComHandle m_CanalHdl = null;
        CRsiNetwork m_CanalDatos = null;
        CRsiHandReader m_CanalLectora = new CRsiHandReader();
        int m_Canal = 0;
        RSI_STATUS m_Status = new RSI_STATUS();
        public int Ping()
        {
            CRsiComWinsock CanalRed = (CRsiComWinsock) m_CanalHdl;
            for (int i = 0; i < 3; i++)
                if (CanalRed.Ping() == TRUE)
                    return TRUE;
            CanalRed.Disconnect();                        // unable to connect, try disconnecting in case we didn't close
            CanalRed.ResetSocket();
            return FALSE;
        }
        public override bool Conecta()
        {
            m_CanalHdl = null;
            m_CanalDatos = null;
            m_Conectado = false;
            int R = 0;

            switch (m_TConexion.TipoConexion)
            {
                case CeC_Terminales.tipo.Red:
                    {

                        CRsiComWinsock CanalRed = new CRsiComWinsock();
                        m_CanalHdl = CanalRed;
                        CanalRed.SetHost(m_TConexion.Direccion);
                        CanalRed.SetPortA(Convert.ToUInt16(m_TConexion.Puerto));

                        if (Ping() == TRUE)
                        {
                            if (CanalRed.Connect() != TRUE)
                            {
                                CanalRed.Disconnect();                        // unable to connect, try disconnecting in case we didn't close
                                CanalRed.ResetSocket();                       // a previous use, then reset the socket, and try to connect 
                                if (CanalRed.Connect() != TRUE)
                                {
                                    AgregaError("Error en m_CanalLectora.Connect");
                                    return false;
                                }
                            }
                        }
                        else
                            return false;
                        if (CanalRed.IsConnected() != TRUE)
                        {
                            AgregaError("Error en CanalRed.IsConnected()");
                            return false;
                        }

                    }
                    break;
                case CeC_Terminales.tipo.Serial:

                    break;
                case CeC_Terminales.tipo.Modem:
                    return false;
                    break;
                case CeC_Terminales.tipo.USB:
                case CeC_Terminales.tipo.RS485:
                    return false;
                    break;

            }
            m_CanalDatos = new CRsiNetwork(m_CanalHdl);
            m_CanalDatos.SetNetworkName("2nd Floor");
            m_CanalDatos.Attach(m_CanalLectora);
            m_CanalLectora.SetAddress(Convert.ToByte(m_TConexion.NoTerminal));
            /*
                if (m_CanalLectora.SetAddress(Convert.ToByte(m_TConexion.NoTerminal)) != TRUE)
                {
                    AgregaError("Error en m_CanalLectora.SetAddress");
                    return false;
                }
            */
            if (TRUE == m_CanalLectora.IsConnected())
                if (TRUE == m_CanalLectora.CmdGetStatus())
                    return m_Conectado = true;

            return false;
        }


        public override bool Desconecta()
        {
            if (m_CanalHdl != null)
            {
                switch (m_TConexion.TipoConexion)
                {
                    case CeC_Terminales.tipo.Red:
                        ((CRsiComWinsock)m_CanalHdl).Disconnect();
                        break;
                }
                return m_Conectado = false;
            }
            if (m_Canal <= 0)
                return false;

            m_Canal = 0;
            return m_Conectado = false;
        }


        public override Errores PoleoChecadas()
        {
            try
            {
                int rtn = 1;              // below, rtn will be 1 if reader responds and 0 (zero) if error (no response)
                m_Vectores = new WS_eCheck.DS_WSPersonasTerminales.DT_VectoresDataTable();
                bool hasLog = false;                        // variable to track whether there's a log to get.
                RecogSys.RdrAccess.RSI_STATUS myRdrStatus = new RecogSys.RdrAccess.RSI_STATUS();  // variable to receive reader’s status.
                RecogSys.RdrAccess.RSI_DATALOG myDataLog = new RecogSys.RdrAccess.RSI_DATALOG();  // variable to receive retrieved log.
                rtn = m_CanalLectora.CmdGetStatus(myRdrStatus);      // Gets reader’s status and checks whether there are
                hasLog = (0 < myRdrStatus.dlog_rdy);
                if (hasLog && TRUE == rtn)
                {                // If there are logs and the reader didn’t return an error...  
                    while (hasLog && TRUE == rtn)
                    {           // Keep repeating until no more logs or an error.
                        rtn = m_CanalLectora.CmdGetPreviousDatalog(myDataLog);   // Get the log.
                        if (FALSE == rtn)
                        {                      // If attempt to get log fails (it shouldn’t), try
                            int counter = 0;                   // up to three times to get it again.
                            while (counter < 3 && FALSE == rtn)
                            {
                                rtn = m_CanalLectora.CmdGetPreviousDatalog(myDataLog);  // (Attempt to get the same data log again.)
                                counter++;

                            }

                        }

                        Console.WriteLine("Datalog format = {0}", myDataLog.format);
                        if (myDataLog.format == RSI_DATALOG_FORMAT.RSI_DLF_IDENTITY_VERIFIED || myDataLog.format == RSI_DATALOG_FORMAT.RSI_DLF_IDENTITY_UNKNOWN)
                            if (!AgregaChecada(myDataLog))
                            {
                                if (m_Vectores.Count > 0)
                                    IUVectoresEnroll = "";
                                return Errores.Error_Conexion;
                            }

                        if (myDataLog.format == RSI_DATALOG_FORMAT.RSI_DLF_USER_ENROLLED && m_DatosTerminal.TERMINAL_ENROLA != 0)
                        {
                            RecogSys.RdrAccess.RSI_USER_RECORD Registro = new RecogSys.RdrAccess.RSI_USER_RECORD();
                            if (m_CanalLectora.CmdGetUser(myDataLog.pOperand, Registro) != 1)
                            {
                                CeLog2.AgregaLog("Error al CmdGetUser " + myDataLog.pOperand.ToString());
                                /*if (m_Vectores.Count > 0)
                                    IUVectoresEnroll = "";
                                return Errores.Error_Conexion;*/
                            }
                            else
                                AgregaHuella(Convert.ToInt64(Registro.pID.GetID()), Registro.pTemplateVector.Get());
                        }

                        rtn = m_CanalLectora.CmdGetDatalog(myDataLog);   // Get the log.
                        if (FALSE == rtn)
                        {                      // If attempt to get log fails (it shouldn’t), try
                            int counter = 0;                   // up to three times to get it again.
                            while (counter < 3 && FALSE == rtn)
                            {
                                rtn = m_CanalLectora.CmdGetDatalog(myDataLog);  // (Attempt to get the same data log again.)
                                counter++;

                            }

                        }

                        rtn = m_CanalLectora.CmdGetStatus(myRdrStatus);       // Get reader’s status again to see if there are any more
                        hasLog = (0 < myRdrStatus.dlog_rdy);         // logs. (If no more logs or if error, end while loop.)
                    }
                    if (FALSE == rtn)
                    {
                        //MessageBox.Show ("There was an error while retrieving the logs from the reader.");
                    }
                }
                if (m_Vectores.Count > 0)
                    if (!m_ws_eCheck.RecibeVectores(Terminal_ID, m_Vectores))
                        IUVectoresEnroll = "";
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
            return 0;

        }



        bool AgregaChecada(RecogSys.RdrAccess.RSI_DATALOG Dl)
        {
            try
            {
                DateTime FechaHora = new DateTime(2000 + Dl.pTimestamp.year, Dl.pTimestamp.month, Dl.pTimestamp.day, Dl.pTimestamp.hour, Dl.pTimestamp.minute, Dl.pTimestamp.second);
                string Numero = Dl.pOperand.GetID().ToString();
                Numero = Convert.ToInt32(Numero).ToString();

                TipoAccesos TA = TipoAccesos.Incorrecto;

                if (Dl.format == RSI_DATALOG_FORMAT.RSI_DLF_IDENTITY_VERIFIED || Dl.format == RSI_DATALOG_FORMAT.RSI_DLF_IDENTITY_UNKNOWN)
                    if (Dl.format == RSI_DATALOG_FORMAT.RSI_DLF_IDENTITY_VERIFIED)
                    {
                        TA = TipoAccesos.Correcto;

                        switch (Dl.pRawData2.GetWord(0))
                        {
                            case 1: TA = TipoAccesos.Entrada; break;
                            case 2: TA = TipoAccesos.Salida_a_Comer; break;
                            case 3: TA = TipoAccesos.Salida; break;
                            case 5: TA = TipoAccesos.Regreso_de_comer; break;
                            case 7: TA = TipoAccesos.No_definido; break;
                        }
                    }
                CeLog2.AgregaLog("AgregaChecada " + Numero + " - " + FechaHora.ToString() + " - " + TA.ToString());

                return AgregaChecada(Numero, FechaHora, TA, true);
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
            return false;
        }

        override protected Errores AsignaFechaHora(DateTime FechaHora)
        {

            DateTime currentDateTime = new DateTime();
            currentDateTime = DateTime.Now;             // Put system time into the arbitrary variable currentDateTime.
            RecogSys.RdrAccess.RSI_TIME_DATE FechaHoraRSI;
            FechaHoraRSI = new RecogSys.RdrAccess.RSI_TIME_DATE();
            int Year = FechaHora.Year % 1000;
            FechaHoraRSI.day = Convert.ToByte(FechaHora.Day);
            FechaHoraRSI.month = Convert.ToByte(FechaHora.Month);
            FechaHoraRSI.year = Convert.ToByte(Year);
            FechaHoraRSI.hour = Convert.ToByte(FechaHora.Hour);
            FechaHoraRSI.minute = Convert.ToByte(FechaHora.Minute);
            FechaHoraRSI.second = Convert.ToByte(FechaHora.Second);

            if (m_CanalLectora.CmdPutTime(FechaHoraRSI) != TRUE)
                return Errores.Error_Desconocido;
            return Errores.Correcto;

            return Errores.Correcto;
        }

        static string gNArchivo = "";
        static bool gBorrarHuellas = eClockSync.Properties.Settings.Default.BorrarHuellas;


        private bool BorraUsuarios()
        {

            try
            {
                return true;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);

            }
            return false;
        }

        protected override Errores EnvioVectores()
        {
            /*
            if (!REEMPLAZAR_VECTORES && !m_DatosTerminal.IsTERMINAL_ENROLANull() && m_DatosTerminal.TERMINAL_ENROLA > 0)
            {
                return Errores.Correcto;
            }*/
            int ErroresGenerados = 0;
            if (m_DatosEmpleados == null)
                return Errores.Error_Desconocido;

            if (m_DatosEmpleados.Rows.Count <= 0)
            {
                //IUVectoresNuevo = "";
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
                    bool Correcto = true;
                    bool PersonaSinHuella = false;
                    bool PersonaSupervisor = false;
                    string Clave = "";

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
                        if (Clave != "")
                            PersonaSinHuella = true;
                    }
                    if (!PersonaSinHuella && (Fila.IsPERSONAS_A_VEC_1Null() || Fila.PERSONAS_A_VEC_1.Length < 9))
                    {
                        AgregaLog("Empleado " + Fila.PERSONA_LINK_ID + " - " + Fila.PERSONA_NOMBRE + " Sin Dato(PERSONAS_A_VEC_1)");
                        continue;
                    }
                    RecogSys.RdrAccess.RSI_USER_RECORD NuevaHuella = new RecogSys.RdrAccess.RSI_USER_RECORD();
                    NuevaHuella.pID.SetID(Fila.PERSONAS_A_VEC_T1);
                    if (PersonaSupervisor)
                        NuevaHuella.authorityLevel = RSI_AUTHORITY_LEVEL.RSI_AUTHORITY_SECURITY;
                    else
                        NuevaHuella.authorityLevel = RSI_AUTHORITY_LEVEL.RSI_AUTHORITY_NONE;
                    NuevaHuella.rejectThreshold = 0;
                    NuevaHuella.timeZone = 0;
                    if (!PersonaSinHuella)
                        NuevaHuella.pTemplateVector.Set(Fila.PERSONAS_A_VEC_1);
                    RSI_STATUS Respuesta = new RSI_STATUS();
                    if (m_CanalLectora.CmdPutUserRecord(NuevaHuella,  Respuesta) == 0)
                        Correcto = false;

                    if (!Correcto)
                    {
                        RSI_USER_RECORD pUser = new RSI_USER_RECORD();
                        if (m_CanalLectora.CmdGetUser(NuevaHuella.pID, pUser) == TRUE)
                        {
                            ConfirmaEmpleado(Fila);
                            AgregaLog("Palma Ya Existe = " + Fila.PERSONA_ID + " NoEmpleado = " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
                            continue;
                        }
                        System.IO.StringWriter Texto = new System.IO.StringWriter();
                        try
                        {
                            Fila.Table.WriteXml(Texto);
                        }
                        catch { }
                        AgregaError("No se pudo cargar la palma Persona = " + Fila.PERSONA_ID + " NoEmpleado = " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);
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

                        ErroresGenerados++;
                    }
                    else
                    {
                        //Si es correcta la transmisión
                        ConfirmaEmpleado(Fila);
                        AgregaLog("Palma transferida = " + Fila.PERSONA_ID + " NoEmpleado = " + Fila.PERSONA_LINK_ID + " " + Fila.PERSONA_NOMBRE);

                    }
                }
                catch (System.Exception e)
                {
                    ErroresGenerados++;
                    AgregaError(e);
                }
            }
            if (gBorrarHuellas)
                BorraUsuarios();
            if (ErroresGenerados > 0)
            {
                AgregaError("HP Existieron  " + ErroresGenerados + " errores");
                return Errores.Error_Desconocido;
            }
            return Errores.Correcto;

        }

        public int AgregaHuella(long ID, byte[] Template)
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

                Fila.PERSONAS_A_VEC_1 = Template;


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
        /// Esta funcion se llama despues de que se ha poleado 
        /// los vectores y subido dicha información al servidor
        /// </summary>
        /// <returns></returns>
        protected override Errores PoleoVectoresSatisfactorio()
        {
            /*
#if BAHIA
            int NoBancos = 1;
            RSI_DATA_BANK2[] BancoDatos = new RSI_DATA_BANK2[NoBancos];
            for (int ContBancos = 0; ContBancos < NoBancos; ContBancos++)
            {
                BancoDatos[ContBancos] = new RSI_DATA_BANK2();
                BancoDatos[ContBancos].data = new byte[4096];
                for (int Cont = 0; Cont < 4096; Cont++)
                {
                    BancoDatos[ContBancos].data[Cont] = 255;
                }
            }
            rsiSetDataBank(m_Canal, 0, ref BancoDatos[0]);

#endif */
            return Errores.Correcto;
        }


        protected override Errores PoleoVectores()
        {
            try
            {
                return Errores.No_Requiere;
                if (IUVectoresEnroll != "")
                    return Errores.No_Requiere;

                RSI_READER_INFO rdrInfo = new RSI_READER_INFO(); // Create rdrInfo variable to receive reader info.
                int banksToGet;                                  // Variable to contain number of banks to retrieve.
                CRsiDataBank retrievedBank = new CRsiDataBank(); // Variable to receive bank of data.
                int rtn;                                         // Arbitrarily-named variable to monitor responses.
                RSI_DISPLAY_MESSAGE backupMessage = new RSI_DISPLAY_MESSAGE(); // Variable for display message
                rtn = m_CanalLectora.CmdGetReaderInfo(rdrInfo);   // Get reader information to figure out how many banks we need
                // to back up. This only works with F-series readers.
                if (FALSE == rtn)
                {                       // Warn if reader isn't responding (could indicate disconnected 
                    //MessageBox.Show("The reader isn’t responding.");  //    reader or E-series reader).
                    return Errores.Error_Conexion;
                }
                else
                {
                    if (0 == rdrInfo.usersEnrolled)
                    {         // See if there are any users
                        return Errores.No_Requiere;
                        //MessageBox.Show("This reader doesn't have any users to back up.");
                    }
                    else
                    {
                        m_CanalLectora.CmdEnterIdleMode();          // Put reader in idle mode so reader's user database
                        // can't be changed while the backup is in process.
                        backupMessage.userSpecific = 0;
                        backupMessage.pMsg.Set("Espere..        Respaldando..."); // Has spaces after -- so "Backing Up" is
                        //backupMessage.pMsg.Set("Please Wait--   Backing Up"); // Has spaces after -- so "Backing Up" is
                        // forced to the 2nd line instead of being split.
                        m_CanalLectora.CmdPutDisplayMessage(backupMessage);  // Show message so user knows why reader isn’t responding.
                        /*
                        for(int NoUsuario = 0; NoUsuario < rdrInfo.usersEnrolled ; NoUsuario ++)
                        {
                            RSI_ID ID =  new RSI_ID();
                            RSI_USER_RECORD Registro = new RSI_USER_RECORD();
                            ID.SetID()
                            m_CanalLectora.CmdGetUser(,)

                        }*/
                        int MaxHuellas = 256;
                        if (RSI_MODEL.RSI_MODEL_HP4K == rdrInfo.model)
                        {                 // HP4000 has 53 users per bank;   
                            banksToGet = (int)(Math.Ceiling(rdrInfo.usersEnrolled / 53.0));  // round up to get last partial bank.
                            MaxHuellas = 53;
                        }
                        else
                        {
                            banksToGet = (int)(Math.Ceiling(rdrInfo.usersEnrolled / 256.0)); // All others have 256 users per bank
                        }

                        for (ushort counter = 0; counter < banksToGet; counter++)
                        {
                            rtn = m_CanalLectora.CmdGetDataBank(counter, retrievedBank);   // Get the databank from the reader.
                            // Here you’d store what you retrieved.
                            if (FALSE == rtn)
                            {
                                AgregaError("Error en m_CanalLectora.CmdGetDataBank " + counter);
                                return Errores.Error_IO;
                            }
                            //retrievedBank.ResetUserIterator(RSI_FAMILY.RSI_FAMILY_HANDPUNCH);
                            byte[] DatosBloque = retrievedBank.Get();
                            int NoUsuarios = retrievedBank.HasUsers();
                            for (int Cont = 0; Cont < MaxHuellas; Cont++)
                            {
                                byte[] Datos = new byte[16];
                                byte[] Vector = new byte[9];
                                Array.Copy(DatosBloque, Cont * 16, Datos, 0, 16);
                                Array.Copy(Datos, 5, Vector, 0, 9);
                                string ID = Bcd2String(Datos, 0, 5);
                                if (ID == "FFFFFFFFFF")
                                    continue;
                                long iID = Convert.ToInt64(ID);
                                if (iID <= 0)
                                    continue;
/*                                RSI_ID pID = new RSI_ID();
                                pID.pBCD = *Datos[0];
                                RecogSys.RdrAccess.RSI_USER_RECORD UR = retrievedBank.GetBasicUserAt(Cont);
                                */
                                //RSI_USER_RECORD_CS UR = new RSI_USER_RECORD_CS(ObtenUsuarioMem(Cont));
                                AgregaHuella(iID, Vector);
                            }
                            //retrievedBank.GetBasicUserAt(0).
                        }

                        backupMessage.pMsg.Set("-    Listo     -");
                        rtn = m_CanalLectora.CmdPutDisplayMessage(backupMessage);  // Set reader prompt back to "Ready" 
                        rtn = m_CanalLectora.CmdExitIdleMode();               // Leaves idle mode so people can use reader again.
                    }

                }

                IUVectoresEnrollNuevo = "Listo";
                return Errores.Correcto;
            }
            catch (System.Exception e)
            {
                CeLog2.AgregaError(e);
            }
            return Errores.Error_Desconocido;
        }

        //Pendientes la sincronizacion de Vectores
    }
}
