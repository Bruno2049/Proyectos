using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
namespace eClockSync5
{
    public partial class Frm_ModoFoto : Form
    {
        int TimeOutLimpiar = 0;

        bool m_Cerrar = false;
        List<CeTerminalSync> m_Terminales = null;
        public Frm_ModoFoto()
        {
            InitializeComponent();
        }

        private void Tmr_Ocultar_Tick(object sender, EventArgs e)
        {

        }

        private bool ChecandoEstado = false;
        private void Tmr_ChecaEstado_Tick(object sender, EventArgs e)
        {
            if (ChecandoEstado)
                return;
            ChecandoEstado = true;
            foreach (CeTerminalSync Terminal in m_Terminales)
            {
                if (!Terminal.EstaConectado())
                {
                    Terminal.TarjetaNoIdentificadaEvent -= Terminal_TarjetaNoIdentificadaEvent;
                    Terminal.TarjetaNoIdentificadaEvent += Terminal_TarjetaNoIdentificadaEvent;

                    Terminal.Inicializa();
                    if (Terminal.Conectar())
                    {

                        ThreadPool.QueueUserWorkItem(Terminal.ThreadEnLinea);
                    }
                }
                // ThreadPool.QueueUserWorkItem(Terminal.ThreadEnLinea);
            }
            ChecandoEstado = false;
        }

        void Terminal_TarjetaNoIdentificadaEvent(CeTerminalSync Terminal, string Tarjeta)
        {
            TimeOutLimpiar = 5;
            Pnl_Persona.Visible = true;
            Lbl_Tarjeta.Text = Tarjeta;
            Lbl_Nombre.Text = "";
            Lbl_NoCuenta.Text = "";
            Img_Usuario.Image = null;
            Lbl_Franja.BackColor = Color.Red;
            Lbl_Bienvenida.Text = "Bienvenido";
            if (CeS_eCheck.EstaConectado())
            {


                DataSet DS = CeS_eCheck.m_Ws_eCheck.ObtenDatosPersona(Convert.ToInt32(Terminal.m_DatosTerminal.TERMINAL_ID), Tarjeta);

                if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                {
                    Lbl_Bienvenida.Text = "Tarjeta no existente";
                }
                else
                {
                    DataRow Fila = DS.Tables[0].Rows[0];
                    int PersonaID = Convert.ToInt32(Fila["PERSONA_ID"]);
                    int PersonaLinkID = Convert.ToInt32(Fila["PERSONA_LINK_ID"]);
                    string PersonaNombre = Convert.ToString(Fila["PERSONA_NOMBRE"]);
                    string CampoAdicional = Convert.ToString(Fila["DATO_ADD"]);
                    int TIENE_ACCESO = Convert.ToInt32(Fila["TIENE_ACCESO"]);
                    Lbl_Nombre.Text = PersonaNombre;
                    Lbl_NoCuenta.Text = CampoAdicional;
                    try
                    {
                        if (PersonaID > 0)
                        {
                            eClockBase.Controladores.Personas Persona = new eClockBase.Controladores.Personas(m_Sesion);
                            Persona.ObtenFotoEvent += delegate(byte[] Foto)
                                {
                                    if (Foto != null)
                                    {
                                        MemoryStream ms = new MemoryStream(Foto);
                                        Image ImgFoto = Image.FromStream(ms);
                                        Img_Usuario.Image = ImgFoto;
                                    }
                                };
                            Persona.ObtenFoto(PersonaID);
                        }
                    }
                    catch (Exception ex)
                    {
                        CeLog2.AgregaError(ex);
                    }
                    if (TIENE_ACCESO > 0)
                    {
                        Terminal.AbrePuerta(500);
                        Lbl_Franja.BackColor = Color.Green;
                    }
                    else
                    {
                        Lbl_Bienvenida.Text = "Tiene un mensaje en la impresora";
                        try
                        {                                                        
                            ElTicket Ticket = new ElTicket();
                            try{
                                CrystalDecisions.CrystalReports.Engine.TextObject Texto = Ticket.ReportDefinition.Sections["Section2"].ReportObjects["Lbl_Accion"] as CrystalDecisions.CrystalReports.Engine.TextObject;
                                //Texto.Text = Texto.Text + CampoAdicional;
                                Texto.Text = CampoAdicional;
                            }catch{}
                            Ticket.PrintOptions.PrinterName = eClockSync5.Properties.Settings.Default.NombreImpresora;
                            Ticket.PrintToPrinter(1, false, 0, 0);
                            
                        }
                        catch (Exception ex)
                        {
                            CeLog2.AgregaError(ex);
                        }
                    }
                    //                    Terminal.AbrePuerta(1000);
                }



            }
        }


        eClockBase.CeC_SesionBase m_Sesion = null;
        private void Frm_ModoFoto_Load(object sender, EventArgs e)
        {
            eClockBase.CeC_Stream.MetodoStream = new eClock5.CeC_StreamFile();
            eClockBase.CeC_LogDestino.StreamWriter = eClock5.CeC_StreamFile.sAgregaTexto("eClock5.log");

            m_Sesion = eClock5.CeC_Sesion.ObtenSesion(this);
            if (!CeS_eCheck.EstaConectado())
            {
                MessageBox.Show("No Pudo Conectar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }

            Inicia();
        }

        private void Cerrar()
        {
            m_Cerrar = true;
            this.Close();
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Cerrar();
        }

        public bool Inicia()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                bool bSetMaxThread = ThreadPool.SetMaxThreads(255, 500);
                if (!bSetMaxThread)
                {
                    Console.WriteLine("Setting max threads of the threadpool failed!");
                }

                m_Terminales = CeS_eCheck.ObtenTerminales(null);

                Tmr_ChecaEstado_Tick(null, null);
                Tmr_ChecaEstado.Enabled = true;

                return true;
            }
            catch { }
            return false;
        }

        private void mostrarEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reiniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Vista.MuestraUrl("");
        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Configuracion Frm = new Frm_Configuracion();
            Frm.ShowDialog();
        }

        private void Frm_ModoFoto_MouseClick(object sender, MouseEventArgs e)
        {
           // this.Close();
        }

        private void Reloj_Tick(object sender, EventArgs e)
        {

            if (TimeOutLimpiar == 0)
                Pnl_Persona.Visible = false;
            TimeOutLimpiar--;
            Lbl_Hora.Text = DateTime.Now.ToString("T");
            Lbl_Fecha.Text = DateTime.Now.ToString("m");
        }
    }
}
