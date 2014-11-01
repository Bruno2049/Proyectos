using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace eEnroler
{
    public partial class FrHuellaAnviz : Form
    {
        Int16 m_ID = 0;
        byte[] m_bEnroladores = new byte[128 * 8];
        string[] m_Enroladores = new string[8];


        public FrHuellaAnviz()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrHuellaAnviz_Load(object sender, EventArgs e)
        {
            if (CeOa99.AvzFindDevice(m_bEnroladores) != 1)
            {
                MessageBox.Show("Conecte el enrolador eClock", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 128; j++)
                    if (m_bEnroladores[i * 128 + j] != 0)
                        m_Enroladores[i] += new string((char)m_bEnroladores[i * 128 + j], 1);
                    else
                        break;

            if (CeOa99.AvzOpenDevice(m_ID, Pbx_Huella.Handle) != 0)
            {
                MessageBox.Show("Conecte el enrolador eClock", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            TActualiza.Enabled = true;
            ActualizaNoHuella();
        }

        private void FrHuellaAnviz_FormClosing(object sender, FormClosingEventArgs e)
        {
            TActualiza.Enabled = false;
            while (m_Cargando)
                System.Threading.Thread.Sleep(500);
            CeOa99.AvzCloseDevice(m_ID);
            System.Threading.Thread.Sleep(500);
        }
        int m_NumeroHuella = 0;
        int m_Cont = 0;
        bool m_Cargando = false;
        byte[] ImgTemp = new byte[78400];
        byte[] Features = new byte[338];
        byte[] ImgTempBin = new byte[78400];
        Image Imagen = null;

        byte[][] Feature = new byte[3][];
        public Image[] Huella = new Image[3];
        

        byte[] AnvizFeature = new byte[500];

        public byte[] VectorCapturado;
        public byte[][] HuellaCapturadaBmp = new byte[3][];
        void Cargando(bool EstaCargando)
        {
            TActualiza.Enabled = !EstaCargando;
            m_Cargando = EstaCargando;
        }
            
        private void TActualiza_Tick(object sender, EventArgs e)
        {
            if (m_Cargando)
                return;
            Cargando(true);
            
            Int16 bFingerOn = 99;
            
            CeOa99.AvzGetImage(m_ID,ImgTemp,ref bFingerOn);
            if (bFingerOn == 1)
            {

                string Ruta = "Finger" + m_Cont + ".bmp";
                m_Cont++;
                if (m_Cont == 99)
                    m_Cont = 0;
                Encoding u8 = Encoding.UTF8;
                try
                {
                    System.IO.File.Delete(Ruta);
                    CeOa99.AvzSaveHueBMPFile(u8.GetBytes(Ruta), ImgTemp);
                    Pbx_Huella.Image = Imagen = (Image)Image.FromFile(Ruta).Clone();
                }
                catch
                {
                    
                }

            }
            else
                Pbx_Huella.Image = null;
            Cargando(false);
        }
        private void ActualizaNoHuella()
        {
            LblNoHuella.Text = "" + ++m_NumeroHuella;
            
        }
        byte[] ObtenArreglo(Image Datos)
        {

            return null;
        }
        /// <summary>
        /// Obtiene un arreglo de bytes con el contenido de una imagen en BMP que tiene la huella
        /// </summary>
        /// <param name="NoImagen">numero de imagen empezando desde cero</param>
        /// <returns></returns>
        public byte[] ObtenImagen(int NoImagen)
        {
            
            System.IO.MemoryStream MS = new System.IO.MemoryStream();
            Huella[NoImagen].Save(MS, System.Drawing.Imaging.ImageFormat.Bmp);
            return MS.GetBuffer();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Cargando(true);

            if (CeOa99.AvzProcess(ImgTemp, Features, ImgTempBin, true, true) == 0)
            {
                Huella[m_NumeroHuella - 1] = Imagen;
                Feature[m_NumeroHuella - 1] = Features;
                
                if (m_NumeroHuella == 2)
                {
                    int Res = CeOa99.AvzPackFeature(Feature[0], Feature[1], AnvizFeature);
                    if (Res > 0)
                    {
                        VectorCapturado = new byte[Res];
                        for (int Con = 0; Con < Res; Con++)
                            VectorCapturado[Con] = AnvizFeature[Con];
                        this.DialogResult = DialogResult.OK;
                        Cargando(false);
                        this.Close();
                        return;
                    }
                    MessageBox.Show("La huella no coincide o no pudo ser verificada, inténtelo nuevamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_NumeroHuella = 0;
                }
                ActualizaNoHuella();
            }
            else
            {
                MessageBox.Show("No se pudo capturar la huella, inténtelo nuevamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Cargando(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
