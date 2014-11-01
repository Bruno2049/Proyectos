using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;

namespace eClockWin
{
    public partial class FEnrolar : Form
    {
        WSChecador.DS_WSPersonasTerminales.ITW_TERMINALESDataTable DT_Terminales = null;
        public int m_Sesion_Id = 14;
        public int m_Persona_Id = 1792;
        public int m_Persona_Link_Id = 0;
        WSChecador.ModeloTerminales Modelo = eClockWin.WSChecador.ModeloTerminales.No_definido;
        WSChecador.DS_WSPersonasTerminales.ITW_TERMINALESRow FilaTerminal = null;
        CeC_Terminales TermDireccion = new CeC_Terminales();
        public FEnrolar()
        {
            InitializeComponent();
        }

        private void FEnrolar_Load(object sender, EventArgs e)
        {
            try
            {
                DT_Terminales = CIS_Global.wsChecador.ObtenTerminalesEnrolamiento(m_Sesion_Id);
                if (DT_Terminales != null)
                {
                    try
                    {
                        m_Persona_Link_Id = CIS_Global.wsChecador.ObtenPersonaLinkID(m_Persona_Id);
                        LblPersona.Text = CIS_Global.wsChecador.PersonaNombre(m_Persona_Id) + "(" + m_Persona_Link_Id + ")";
                        iTWTERMINALESBindingSource.DataSource = DT_Terminales;
                        CmbTerminales_SelectedIndexChanged(null, null);
                        int result = BSSDK_NS.BSSDK.BS_InitSDK();

                        if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
                        {
                            MessageBox.Show("No se puede inicializar la Libreria BS_SDK.dll", "Error");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Existio un error de parametros en IsTime\npongase en contacto con su administrador", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                    return;
                }
            }
            catch { }
            MessageBox.Show("Existio un error al intentar conectarse a IsTime\npongase en contacto con su administrador", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }
        void HayHuella1(bool HayHuella)
        {
            BtnBorrarH1.Enabled = HayHuella;
            LblHuella1.Visible = HayHuella;
        }
        void HayHuella2(bool HayHuella)
        {
            BtnBorrarH2.Enabled = HayHuella;
            LblHuella2.Visible = HayHuella;
        }
        void HayTarjeta(bool HayHuella)
        {
            BtnBorrarT.Enabled = HayHuella;
            LblTarjeta.Visible = HayHuella;
        }
        private void CmbTerminales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int Terminal_ID = Convert.ToInt32(CmbTerminales.SelectedValue);
                HayHuella1(CIS_Global.wsChecador.HayHuella(m_Persona_Id, Terminal_ID, 1));
                HayHuella2(CIS_Global.wsChecador.HayHuella(m_Persona_Id, Terminal_ID, 2));
                LblTarjeta.Text = CIS_Global.wsChecador.ObtenValorCampoAdicional( Terminal_ID,m_Persona_Id);
                HayTarjeta(LblTarjeta.Text.Length > 0);

                FilaTerminal = DT_Terminales[CmbTerminales.SelectedIndex];
                Modelo = (eClockWin.WSChecador.ModeloTerminales)FilaTerminal.MODELO_TERMINAL_ID;

                TermDireccion.CargarCadenaConexion(FilaTerminal.TERMINAL_DIR);
                if (Modelo == eClockWin.WSChecador.ModeloTerminales.Lector_6055)
                {
                    grb_Huella1.Enabled = false;
                    grb_Huella2.Enabled = false;
                }
                else
                {
                    grb_Huella1.Enabled = true;
                    grb_Huella2.Enabled = true;
                }
            }
            catch
            {

            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private byte[] LeeHuellaBEP()
        {
            IPAddress addr = IPAddress.Parse(TermDireccion.Direccion);

            int handle = 0;

            Cursor.Current = Cursors.WaitCursor;

            int result = BSSDK_NS.BSSDK.BS_OpenSocket(addr.ToString(), TermDireccion.Puerto, ref handle);
            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("No se pudo conectar con el dispositivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            int Pos = FilaTerminal.TERMINAL_NOMBRE.IndexOf("(") + 1;
            string ID = FilaTerminal.TERMINAL_NOMBRE.Substring(Pos, FilaTerminal.TERMINAL_NOMBRE.IndexOf(")") - Pos);

            BSSDK_NS.BSSDK.BS_SetDeviceID(handle,
                Convert.ToUInt32(ID), BSSDK_NS.BSSDK.BS_DEVICE_BEPLUS);

            /*   IntPtr userInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(BSSDK_NS.BSSDK.BEUserHdr)));
               IntPtr templateData = Marshal.AllocHGlobal(384 * 4); // max 2 fingers(4 templates) per user

               result = BSSDK_NS.BSSDK.BS_GetUserBEPlus(handle, 1, userInfo, templateData);
               result = BSSDK_NS.BSSDK.BS_EnrollUserBEPlus(handle, userInfo, templateData);*/

            byte[] Huella = null;
            byte[] Huella1 = null;
            //Colocar Huella 1
            result = BSSDK_NS.BSSDK.BS_ScanTemplate(handle, out Huella1);
            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                MessageBox.Show("No se pudo capturar la huella", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Huella1 = null;
            }
            if (Huella1 != null)
            {
                byte[] Huella2 = null;
                //Colocar confirmación de huella
                result = BSSDK_NS.BSSDK.BS_ScanTemplate(handle, out Huella2);
                if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
                {
                    MessageBox.Show("No se pudo capturar la confirmación de huella", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Huella2 = null;
                }
                if (Huella2 != null)
                {


                    BSSDK_NS.BSSDK.BEUserHdr Usuario = new BSSDK_NS.BSSDK.BEUserHdr();
                    /* byte[] Template = null;
                     BSSDK_NS.BSSDK.BS_GetUserBEPlus(handle, 1, out Usuario,out Template);
                     */
                    //                    Usuario.version = 1;
                    Usuario.userID = Convert.ToUInt32(m_Persona_Link_Id);
                    //                    Usuario.userID = 95;
                    Usuario.startTime = 0x0;  //no start time check
                    Usuario.expiryTime = 0x0;
                    Usuario.securityLevel = 0;
                    Usuario.adminLevel = 1;
                    Usuario.startTime = 0x0;  //no start time check
                    Usuario.accessGroupMask = 0xFFFFFFFF;
                    Usuario.isDuress = new byte[2];
                    Usuario.isDuress[0] = 0;
                    Usuario.isDuress[1] = 0;

                    Usuario.reserved2 = new int[21];

                    Usuario.numOfFinger = 1;
                    // BSSDK_NS.BSSDK.BS_EnrollUserBEPlus(handle, Usuario, Template);
                    Huella = new byte[Huella1.Length + Huella2.Length];
                    Array.Copy(Huella1, Huella, Huella1.Length);
                    Array.Copy(Huella2, 0, Huella, Huella1.Length, Huella2.Length);
                    Usuario.fingerChecksum = new ushort[2];
                    for (int Cont = 0; Cont < Huella.Length; Cont++)
                    {
                        Usuario.fingerChecksum[0] += Huella[Cont];
                    }
                    Usuario.fingerChecksum[1] = 0;
                    result = BSSDK_NS.BSSDK.BS_EnrollUserBEPlus(handle, Usuario, Huella);
                    if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
                    {
                        MessageBox.Show("No se pudo capturar la confirmación de huella/nIntentelo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Huella = null;
                    }
                    //BSSDK_NS.BSSDK.BS_DeleteUser(handle, Usuario);
                }
            }
            BSSDK_NS.BSSDK.BS_CloseSocket(handle);
            return Huella;
        }

        private byte[] LeeHuellaHP()
        {
            Cursor.Current = Cursors.WaitCursor;
            CeHPEnr HP = new CeHPEnr();
            HP.m_TConexion = TermDireccion;
            HP.Conecta();
            byte [] Huella = HP.Enrolar();
            HP.Desconecta();
            
            Cursor.Current = Cursors.Default;
            return Huella;
        }

        private byte[] LeeHuella()
        {
            switch (Modelo)
            {
                case eClockWin.WSChecador.ModeloTerminales.BioEntryPlus:
                    return LeeHuellaBEP();
                    break;
                case eClockWin.WSChecador.ModeloTerminales.HandPunch:
                    return LeeHuellaHP();
                    break;
            }
            MessageBox.Show("El dispositivo no soporta el enrolamiento remoto\n vea el manual del equipo para conocer como capturar huellas o palmas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }

        private bool LeeYAsignaHuella(int NoHuella)
        {
            byte[] Huella = LeeHuella();
            if (Huella != null)
            {
                bool Guardada = false;
                try
                {
                    Guardada = CIS_Global.wsChecador.AsignaHuella(m_Persona_Id, Convert.ToInt32(FilaTerminal.TERMINAL_ID), Huella, NoHuella);
                }
                catch { }
                if (!Guardada)
                    MessageBox.Show("No se pudo guardar la huella en IsTime, \nle recomendamos que lo intente nuevamente mas tarde", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                else
                    return true;
            }
            return false;
        }

        private void BtnLeerH1_Click(object sender, EventArgs e)
        {
            if (LeeYAsignaHuella(1))
                HayHuella1(true);
        }

        private void BtnLeerH2_Click(object sender, EventArgs e)
        {
            if (LeeYAsignaHuella(2))
                HayHuella2(true);
        }

        private uint LeeTarjetaBEP()
        {
            IPAddress addr = IPAddress.Parse(TermDireccion.Direccion);

            int handle = 0;

            Cursor.Current = Cursors.WaitCursor;

            int result = BSSDK_NS.BSSDK.BS_OpenSocket(addr.ToString(), TermDireccion.Puerto, ref handle);
            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("No se pudo conectar con el dispositivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            int Pos = FilaTerminal.TERMINAL_NOMBRE.IndexOf("(") + 1;
            string ID = FilaTerminal.TERMINAL_NOMBRE.Substring(Pos, FilaTerminal.TERMINAL_NOMBRE.IndexOf(")") - Pos);

            BSSDK_NS.BSSDK.BS_SetDeviceID(handle,
                Convert.ToUInt32(ID), BSSDK_NS.BSSDK.BS_DEVICE_BEPLUS);
            UInt32 Tarjeta = 0;
            int Obj = 0;
//            result = BSSDK_NS.BSSDK.BS_ReadCardIDEx(handle, ref Tarjeta, ref Obj);
            result = BSSDK_NS.BSSDK.BS_ReadCardID(handle, ref Tarjeta);
            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                MessageBox.Show("No se pudo capturar la Tarjeta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Tarjeta = 0;
            }
            BSSDK_NS.BSSDK.BS_CloseSocket(handle);
            return Tarjeta;
        }
        private uint LeeTarjeta6055()
        {
            try
            {
                uint ID = 0;
                CTMCMifare Mifare = new CTMCMifare();

                if (Mifare.IniciaPuerto(TermDireccion.Puerto))
                {
                    string R = Mifare.Comando_S();
                    if (R == "N")
                        MessageBox.Show("No hay tarjeta o no se encontro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    if (R == "F")
                        MessageBox.Show("Error en la configuracion de autolectura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    if (R.Length > 2)
                    {
                        if (R.Length > 10)
                            R = R.Substring(R.Length - 10);
                        ID = Convert.ToUInt32(R);
                        
                        int Bloque = Mifare.Comando_RP(1);
                        int Longitud = Mifare.Comando_RP(2);
                        if (Bloque > 0 && Longitud > 0)
                        {
                            UInt32 NoEmpleado = Convert.ToUInt32(m_Persona_Link_Id);
                            string Respuesta = Mifare.Comando_WT(Convert.ToByte(Bloque),NoEmpleado.ToString(new string('0',Longitud)));
                            if (Respuesta != "Correcto")
                            {
                                Mifare.CierraPuerto();
                                MessageBox.Show("No se pudo guardar en la Tarjeta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return 0;
                            }
                        }
                    }
                    Mifare.CierraPuerto();
                    return ID;
                }
                MessageBox.Show("No se pudo capturar la Tarjeta \n Error de apertura de puerto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo capturar la Tarjeta \n " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return 0;
        }
        private uint LeeTarjeta()
        {
            switch (Modelo)
            {
                case eClockWin.WSChecador.ModeloTerminales.BioEntryPlus:
                    return LeeTarjetaBEP();
                    break;
                case eClockWin.WSChecador.ModeloTerminales.Lector_6055:
                    return LeeTarjeta6055();
                    break;
            }
            MessageBox.Show("El dispositivo no soporta la lectura de tarjeta remotamente\n vea el manual del equipo para conocer como capturar el No. de Tarjeta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return 0;
        }
        private void BtnLeerT_Click(object sender, EventArgs e)
        {
            uint Tarjeta = LeeTarjeta();
            if (Tarjeta != 0)
            {
                LblTarjeta.Text = Tarjeta.ToString();
                try
                {
                    int Terminal_ID = Convert.ToInt32(FilaTerminal.TERMINAL_ID);
                    bool Guardada = false;
                    Guardada = CIS_Global.wsChecador.AsignaCampoAdicional(Terminal_ID, m_Persona_Id, LblTarjeta.Text);
                    if (Guardada)
                    {
                        string tar = LblTarjeta.Text;
                        if (CIS_Global.wsChecador.ObtenValorCampoAdicional(Terminal_ID, m_Persona_Id) != tar)
                        {
                            MessageBox.Show("No se pudo guardar la tarjeta, intente nuevamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        HayTarjeta(true);
                    }
                    else
                        MessageBox.Show("Intentar nuevamente la captura del No. de Tarjeta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                        LblTarjeta.Text = "Intentar Nuevamente";
                }
                catch
                {
                }
            }
        }
        bool BorrarHuella(int NoHuella)
        {
            bool Guardada = false;
            try
            {
                Guardada = CIS_Global.wsChecador.AsignaHuella(m_Persona_Id, Convert.ToInt32(FilaTerminal.TERMINAL_ID), null, NoHuella);
            }
            catch { }
            if (!Guardada)
                MessageBox.Show("No se pudo guardar la huella en IsTime, \nle recomendamos que lo intente nuevamente mas tarde", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                return true;
            return false;
        }
        private void BtnBorrarH1_Click(object sender, EventArgs e)
        {
            if (BorrarHuella(1))
                HayHuella1(false);

        }

        private void BtnBorrarH2_Click(object sender, EventArgs e)
        {
            if (BorrarHuella(2))
                HayHuella2(false);
        }

        private void BtnBorrarT_Click(object sender, EventArgs e)
        {

            LblTarjeta.Text = "";
            try
            {
                bool Guardada = false;
                Guardada = CIS_Global.wsChecador.AsignaCampoAdicional(Convert.ToInt32(FilaTerminal.TERMINAL_ID), m_Persona_Id, LblTarjeta.Text);
                if (Guardada)
                    HayTarjeta(false);
                else
                    MessageBox.Show("Intentar nuevamente borrar el No. Tarjeta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
            }
        }
    }
}