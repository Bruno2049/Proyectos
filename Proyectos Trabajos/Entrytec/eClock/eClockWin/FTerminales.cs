using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;

namespace eClockWin
{
    public partial class FTerminales : Form
    {
        
        private int m_Handle = -1;

        private int m_NumOfDevice = 0;
        private uint[] m_DeviceID;
        private int[] m_DeviceType;
        private uint[] m_DeviceAddr;
        public int m_Sesion_ID = 7;
        public FTerminales()
        {
            InitializeComponent();
        }

        private void FTerminales_Load(object sender, EventArgs e)
        {


            //            backgroundWorker1.RunWorkerAsync();

        }
        private void BuscaTerminalesBio()
        {
            int result = BSSDK_NS.BSSDK.BS_InitSDK();

            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                MessageBox.Show("No se puede inicializar la Libreria BS_SDK.dll", "Error");
                return;
            }

            result = BSSDK_NS.BSSDK.BS_OpenInternalUDP(ref m_Handle);

            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                MessageBox.Show("No se puede abrir el socket UDP interno", "Error");
                return;
            }
            m_DeviceID = new uint[32];
            m_DeviceType = new int[32];
            m_DeviceAddr = new uint[32];

            try
            {
                result = BSSDK_NS.BSSDK.BS_SearchDeviceInLAN(m_Handle, ref m_NumOfDevice, m_DeviceID, m_DeviceType, m_DeviceAddr);
            }
            finally
            {

            }

            if (result != BSSDK_NS.BSSDK.BS_SUCCESS)
            {
                MessageBox.Show("No se encontró algún dispositivo", "Error");
                return;
            }

            for (int i = 0; i < m_NumOfDevice; i++)
            {
                string reader = "";

                if (m_DeviceType[i] == BSSDK_NS.BSSDK.BS_DEVICE_BIOSTATION)
                {
                    reader += "Biostation ";
                }
                else
                {
                    reader += "Bioentry Plus ";
                }

                reader += (m_DeviceAddr[i] & 0xff) + ".";
                reader += ((m_DeviceAddr[i] >> 8) & 0xff) + ".";
                reader += ((m_DeviceAddr[i] >> 16) & 0xff) + ".";
                reader += ((m_DeviceAddr[i] >> 24) & 0xff);

                reader += "(" + m_DeviceID[i] + ")";
                LboTerminales.Items.Add(reader);
            }
        }
        private void BuscaTerminales()
        {
            Cursor.Current = Cursors.WaitCursor;

            Cursor.Current = Cursors.Default;
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BuscaTerminales();
        }

        private void TBioEntry_Tick(object sender, EventArgs e)
        {
            TBioEntry.Enabled = false;
            LblBuscando.Visible = true;
            BuscaTerminalesBio();
            LblBuscando.Visible = false;
        }

        private void networkConfigButton_Click(object sender, EventArgs e)
        {
            if (LboTerminales.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione primero la terminal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FConfigTerm networkConfig = new FConfigTerm();

            networkConfig.SetDevice(m_Handle, m_DeviceID[LboTerminales.SelectedIndex], m_DeviceAddr[LboTerminales.SelectedIndex]);

            if (networkConfig.ShowDialog() == DialogResult.OK)
            {
                LblBuscando.Visible = true;

                LboTerminales.Items.Clear();
                TBioEntry.Interval = 7000;
                TBioEntry.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_NumOfDevice; i++)
            {
                WSChecador.ModeloTerminales Modelo = eClockWin.WSChecador.ModeloTerminales.No_definido;

                if (m_DeviceType[i] == BSSDK_NS.BSSDK.BS_DEVICE_BIOSTATION)
                    Modelo = eClockWin.WSChecador.ModeloTerminales.BioEntryPlus;
                else
                    Modelo = eClockWin.WSChecador.ModeloTerminales.BioEntryPlus;
                string IP = "";
                IP += (m_DeviceAddr[i] & 0xff) + ".";
                IP += ((m_DeviceAddr[i] >> 8) & 0xff) + ".";
                IP += ((m_DeviceAddr[i] >> 16) & 0xff) + ".";
                IP += ((m_DeviceAddr[i] >> 24) & 0xff);
                FConfigTerm Config = new FConfigTerm();
                Config.SetDevice(m_Handle, m_DeviceID[i], m_DeviceAddr[i]);
                Config.ReadConfig();
                int Puerto = Config.Puerto;
                CeC_Terminales Term = new CeC_Terminales();
                Term.Direccion = IP;
                Term.Puerto = Puerto;
                Term.TipoConexion = CeC_Terminales.tipo.Red;
                if (!CIS_Global.wsChecador.AgregaTerminal(m_Sesion_ID, m_DeviceID[i].ToString(), Modelo, eClockWin.WSChecador.Tecnologias.Huella, eClockWin.WSChecador.Tecnologias.Proximidad, "NO_EMPLEADO", "NO_CREDENCIAL", Term.ObtenCadenaConexion(), true))
                {
                    MessageBox.Show("No se pudo guardar la información en IsTime", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            MessageBox.Show("Datos guardados satisfactoriamente", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}