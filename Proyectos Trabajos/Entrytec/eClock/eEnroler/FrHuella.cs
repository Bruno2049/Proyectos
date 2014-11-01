using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZKFPEngXControl;
using zkemkeeper;


namespace eEnroler
{
    public delegate void DelegateMuestraHuella();
    public partial class FrHuella : Form
    {

        int m_TerminalID = eEnroler.Properties.Settings.Default.Terminal;
        int m_UsuarioID = 0;
        int m_PersonaID = 0;
        int m_PersonaLinkID = 0;

        public DelegateMuestraHuella m_MuestraHuella = null;
        public object[] TemplatesEnroll = new object[5];
        public int m_ContadorHuella = 0;
        public bool m_EstaEnrolando = false;
        public byte[] m_HuellaVector = null;
        public byte[] m_HuellaImagen = null;
        public byte[][] m_HuellaImagenEnr = null;
        public bool m_EsHuella1 = true;
        string serial = "";
        public FrHuella()
        {

            InitializeComponent();



        }
        public void MuestraSemaforo(bool mverde, bool mrojo)
        {



        }
        public void limpiarFrontEnd()
        {


        }

        public void LimpiarDatos()
        {
            Img_Huella.Image = null;


        }


        private void FrHuella_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
            Tmr_Cargar.Enabled = true;
            //serial = axZKFPEngX1.SensorSN;
            //  System.IO.File.WriteAllText("c:\\gibran.txt",serial);

            

        }

        private void FrHuella_KeyPress(object sender, KeyPressEventArgs e)
        {

        }



        private void etNoEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {

        }





        public bool GuardaDatos()
        {
            try
            {

                return true;
            }
            catch { return false; }

        }
        public bool LeeDatos()
        {
            try
            {

                return true;
            }
            catch { return false; }

        }



        private void axZKFPEngX1_OnEnroll(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEvent e)
        {
            if (e.actionResult)
            {
                m_HuellaVector = (byte[])e.aTemplate;
                if (!CS_WebService.Conecta())
                    return;
                int NoHuella = 0;
                int AVecID = 0;
                if (m_EsHuella1)
                {
                    NoHuella = 1;
                    AVecID = eEnroler.Properties.Settings.Default.AVecH1;
                }
                else
                {
                    NoHuella = 2;
                    AVecID = eEnroler.Properties.Settings.Default.AVecH2;
                }

                bool Correcto = true;
                Correcto = CS_WebService.AsignaHuella(m_PersonaID, m_TerminalID, m_HuellaVector, NoHuella);
                if (eEnroler.Properties.Settings.Default.GuardaImagen)
                {
                    if (Correcto)
                        Correcto = CS_WebService.AsignaVectorAVec(m_PersonaID, AVecID, m_HuellaImagenEnr[0], 1);
                    if (Correcto)
                        Correcto = CS_WebService.AsignaVectorAVec(m_PersonaID, AVecID, m_HuellaImagenEnr[1], 2);
                    if (Correcto)
                        Correcto = CS_WebService.AsignaVectorAVec(m_PersonaID, AVecID, m_HuellaImagenEnr[2], 3);
                }
                //                MessageBox.Show("Enrolado");
                if (Correcto)
                {
                    HabilitaBorrar(NoHuella == 1 ? true : false, true);
                    MessageBox.Show("Persona enrolada satisfactoriamente");
                }
                else
                    MessageBox.Show("No se pudo guardar la huella", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                MessageBox.Show("No se pudo enrolar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            FinalizaEnrolamiento();
        }





        private void axZKFPEngX1_OnImageReceived(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEvent e)
        {


            if (m_EstaEnrolando)
            {
                MuestraHuella();
                LMensaje.Text = "Quite la Huella del Lector";
            }
        }

        private void axZKFPEngX1_OnFingerLeaving(object sender, EventArgs e)
        {
            if (m_EstaEnrolando)
            {
                LMensaje.Text = "Coloque la Huella en el Lector";
                m_ContadorHuella++;
                LNoHuella.Text = m_ContadorHuella.ToString();
                LimpiarDatos();
            }
        }

        private void axZKFPEngX1_OnFingerTouching(object sender, EventArgs e)
        {
            if (m_EstaEnrolando)
                LMensaje.Text = "Quite la Huella del Lector";
        }



        private void IniciaEnrolamiento()
        {
            if (eEnroler.Properties.Settings.Default.EseClock)
            {

            }
            m_HuellaImagenEnr = new byte[3][];
            m_ContadorHuella = 1;
            m_EstaEnrolando = true;
            Gbx_Empleado.Enabled = false;
            LMensaje.Text = "Coloque la Huella en el Lector";
            LNoHuella.Visible = true;
            LNoHuella.Text = "1";
            axZKFPEngX1.BeginEnroll();
        }
        private void FinalizaEnrolamiento()
        {

            m_EstaEnrolando = false;
            Gbx_Empleado.Enabled = true;
            LMensaje.Text = "";
            LNoHuella.Visible = true;
            LNoHuella.Text = "";

        }

        public void MuestraHuella()
        {

            LimpiarDatos();
            object Imagen = new object();
            axZKFPEngX1.GetFingerImage(ref Imagen);
            m_HuellaImagen = (byte[])Imagen;


            try
            {
                m_HuellaImagenEnr[m_ContadorHuella - 1] = m_HuellaImagen;
                System.IO.MemoryStream ms = new System.IO.MemoryStream((byte[])m_HuellaImagen);
                Img_Huella.Image = System.Drawing.Bitmap.FromStream(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //          Update();
        }
        private void guardar(object[] TemplatesEnroll)
        {
            try
            {

            }
            catch { }
        }
        public void Enrolar()
        {

            // axZKFPEngX1.BeginEnroll();
            if (m_EstaEnrolando)
            {
                if (m_ContadorHuella < 3)
                {
                    TemplatesEnroll[m_ContadorHuella] = (byte[])axZKFPEngX1.GetTemplate();
                    MuestraHuella();
                    //MessageBox.Show("Error");
                    LNoHuella.Text = (m_ContadorHuella + 1).ToString();

                }
                if (m_ContadorHuella >= 2)
                    guardar(TemplatesEnroll);
                m_ContadorHuella++;
            }





        }

        private void axZKFPEngX1_OnCapture(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEvent e)
        {
            Enrolar();
        }

        void EnrolaHuellaeClock(bool EsHuella1)
        {
            FrHuellaAnviz Dlg = new FrHuellaAnviz();
            if(Dlg.ShowDialog() != DialogResult.OK)
                return;

            
            if (!CS_WebService.Conecta())            
                return;
            int NoHuella = 0;
            int AVecID = 0;
            if (EsHuella1)
            {
                NoHuella = 1;
                AVecID = eEnroler.Properties.Settings.Default.AVecH1;
            }
            else
            {
                NoHuella = 2;
                AVecID = eEnroler.Properties.Settings.Default.AVecH2;
            }

            bool Correcto = true;
            Correcto = CS_WebService.AsignaHuella(m_PersonaID, m_TerminalID, Dlg.VectorCapturado, NoHuella);
            if (eEnroler.Properties.Settings.Default.GuardaImagen)
            {
               // MessageBox.Show("No se guardo la imagen (Pendiente)");
                
                
                if (Correcto)
                    Correcto = CS_WebService.AsignaVectorAVec(m_PersonaID, AVecID, Dlg.ObtenImagen(0), 1);
                if (Correcto)
                    Correcto = CS_WebService.AsignaVectorAVec(m_PersonaID, AVecID, Dlg.ObtenImagen(1), 2);
 //               if (Correcto)
   //                 Correcto = CS_WebService.AsignaVectorAVec(m_PersonaID, AVecID, m_HuellaImagenEnr[2], 3);*/
            }
            //                MessageBox.Show("Enrolado");
            if (Correcto)
            {
                HabilitaBorrar(NoHuella == 1 ? true : false, true);
                MessageBox.Show("Persona enrolada satisfactoriamente");
            }
            else
                MessageBox.Show("No se pudo guardar la huella", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void Btn_Enrolar1_Click(object sender, EventArgs e)
        {
            if (eEnroler.Properties.Settings.Default.EseClock)
                EnrolaHuellaeClock(true);
            else
            {
                m_EsHuella1 = true;
                IniciaEnrolamiento();
            }
        }
        private void Btn_Enrolar2_Click(object sender, EventArgs e)
        {
            if (eEnroler.Properties.Settings.Default.EseClock)
                EnrolaHuellaeClock(false);
            else
            {
                m_EsHuella1 = false;
                IniciaEnrolamiento();
            }
        }

        private void axZKFPEngX1_OnFeatureInfo(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEvent e)
        {

        }

        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            Gbx_Empleado.Enabled = false;
            if (!CS_WebService.Conecta())
                return;
            m_PersonaLinkID = 0;
            Img_Huella.Image = null;
            Pbx_Foto.Image = null;
            try
            {
                m_PersonaLinkID = Convert.ToInt32(Tbx_NoEmpleado.Text);

            }
            catch
            {
                MessageBox.Show("El número de empleado no es válido", "Atención");
                return;
            }
            m_PersonaID = CS_WebService.ObtenPersonaID(m_PersonaLinkID);
            if (m_PersonaID < 1)
            {
                MessageBox.Show("El Empleado no existe", "Atención");
                return;
            }
            Lbl_NoEmp.Text = m_PersonaLinkID.ToString();
            Lbl_Nombre.Text = CS_WebService.PersonaNombre(m_PersonaID);
            Lbl_Tarjeta.Text = CS_WebService.ObtenValorCampoAdicional(eEnroler.Properties.Settings.Default.Terminal, m_PersonaID);
            try
            {
                byte[] Foto = CS_WebService.ObtenFoto(m_PersonaID);
                Pbx_Foto.Image = System.Drawing.Bitmap.FromStream(new System.IO.MemoryStream(Foto));
            }
            catch { }
            if (CS_WebService.HayHuella(m_PersonaID, m_TerminalID, 1))
            {
                HabilitaBorrar(true, true);
                try
                {
                    //byte[] Foto = m_WsChecador.ObtenAVec(m_PersonaID,eEnroler.Properties.Settings.Default.AVecH2,1);
                    //Img_Huella.Image = System.Drawing.Bitmap.FromStream(new System.IO.MemoryStream(Foto));
                }
                catch { }
            }
            else
                HabilitaBorrar(true, false);
            if (CS_WebService.HayHuella(m_PersonaID, m_TerminalID, 2))
                HabilitaBorrar(false, true);
            else
                HabilitaBorrar(false, false);
            Gbx_Empleado.Enabled = true;
        }
        void HabilitaBorrar(bool Huella1, bool Borrar)
        {
            if (Huella1)
                Btn_Borrar1.Enabled = Borrar;
            else
                Btn_Borrar2.Enabled = Borrar;
        }

        private void Btn_Borrar1_Click(object sender, EventArgs e)
        {
            if (CS_WebService.AsignaHuella(m_PersonaID, m_TerminalID, null, 1))
            {
                HabilitaBorrar(true, false);
                MessageBox.Show("Huella borrada satisfactoriamente");
            }
            else
                MessageBox.Show("No se pudo borrar la huella", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void Btn_Borrar2_Click(object sender, EventArgs e)
        {
            if (CS_WebService.AsignaHuella(m_PersonaID, m_TerminalID, null, 2))
            {
                HabilitaBorrar(false, false);
                MessageBox.Show("Huella borrada satisfactoriamente");
            }
            else
                MessageBox.Show("No se pudo borrar la huella", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Lbl_Tarjeta_TextChanged(object sender, EventArgs e)
        {
            string Text = Lbl_NoEmp.Text;
            try
            {
                Text += Lbl_Tarjeta.Text.Substring(Lbl_Tarjeta.Text.Length - 4, 4);
                LblClave.Text = Text;
            }
            catch { }
        }

        public void Limpia(byte[] Arreglo, byte Caracter)
        {
            for (int X = 0; X < Arreglo.Length; X++)
                Arreglo[X] = Caracter;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (eEnroler.Properties.Settings.Default.EsMifare)
            {
                uint TID = CeT_Mifare.ObtenNSTarjeta();
                byte[] Bloque = new byte[16];

                Limpia(Bloque, 0xFF);
                Bloque[0] = 0x6a;
                Bloque[1] = 0x01;
                Bloque[6] = 0x00;
                Bloque[7] = 0x00;
                int ID = 0;
                if (eEnroler.Properties.Settings.Default.CodificarTarjeta)
                    ID = m_PersonaLinkID * 10000 + Convert.ToInt32(TID % 10000);
                else
                    ID = m_PersonaLinkID;
                byte[] bID = BitConverter.GetBytes(ID);
                Bloque[2] = bID[0];
                Bloque[3] = bID[1];
                Bloque[4] = bID[2];
                Bloque[5] = bID[3];

                try
                {
                    string Nemp = m_PersonaLinkID.ToString("0000");
                    byte[] Bloque2 = ObtenArregloBytes(Nemp.PadRight(16, ' '));
                    if (CeT_Mifare.EscribeBloque(1, Bloque) == 0 && CeT_Mifare.EscribeBloque(0x3E, Bloque2) == 0)
                    {
                        string Texto = TID.ToString();

                        if (CS_WebService.AsignaCampoAdicional(m_TerminalID, m_PersonaID, Texto))
                        {
                            Lbl_Tarjeta.Text = Texto;
                            return;
                        }
                        else
                            MessageBox.Show("No se pudo guardar la tarjeta, intente nuevamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch { }
                MessageBox.Show("No se pudo leer o guardar la tarjeta, intente nuevamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                FrLeerTarjeta Dlg = new FrLeerTarjeta();
                if (Dlg.ShowDialog() == DialogResult.OK)
                {
                    if (CS_WebService.AsignaCampoAdicional(m_TerminalID, m_PersonaID, Dlg.NoSerie))
                    {
                        Lbl_Tarjeta.Text = Dlg.NoSerie;
                        return;
                    }
                    else
                        MessageBox.Show("No se pudo guardar la tarjeta, intente nuevamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void Tmr_Cargar_Tick(object sender, EventArgs e)
        {
            Tmr_Cargar.Enabled = false;
            if (!CS_WebService.Conecta())
            {
             //   MessageBox.Show("No se pudo conectar a eClock", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
                return;
            }
            Enabled = true;
            if (!eEnroler.Properties.Settings.Default.EseClock)
            {
                int x = axZKFPEngX1.InitEngine();
                m_MuestraHuella = new DelegateMuestraHuella(MuestraHuella);
                axZKFPEngX1.BeginInit();
                string Serial = "";
                Serial = axZKFPEngX1.SensorSN;
            }
        }

    }
}