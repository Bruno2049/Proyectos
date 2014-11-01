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

namespace eClockSync5
{
    public partial class Frm_Main : Form
    {
        public string[] args;
        List<CeTerminalSync> m_Terminales = null;
        bool m_Cerrar = false;
        public Frm_Main()
        {
            InitializeComponent();
        }

        private bool Ocultar()
        {
            foreach (string Cadena in args)
                if (Cadena == "OCULTAR")
                    return true;
            return false;
        }
        
        private void Frm_Main_Load(object sender, EventArgs e)
        {
            
            if (!CeS_eCheck.EstaConectado())
            {
                MessageBox.Show("No Pudo Conectar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            if (Ocultar())
                Tmr_Ocultar.Start();
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

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Configuracion Frm = new Frm_Configuracion();
            Frm.ShowDialog();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Vista.MuestraUrl("");
        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason == CloseReason.TaskManagerClosing)
                return;
            if (!m_Cerrar)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void mostrarEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Tmr_Ocultar_Tick(object sender, EventArgs e)
        {
            Tmr_Ocultar.Stop();
            this.Hide();

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

                m_Terminales = CeS_eCheck.ObtenTerminales(Lst_Terminales);

                Tmr_ChecaEstado_Tick(null, null);
                Tmr_ChecaEstado.Enabled = true;

                return true;
            }
            catch { }
            return false;
        }

        private void eventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (CeTerminalSync Terminal in m_Terminales)
            {
                Terminal.IniciaEventos();
            }
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


    }
}
