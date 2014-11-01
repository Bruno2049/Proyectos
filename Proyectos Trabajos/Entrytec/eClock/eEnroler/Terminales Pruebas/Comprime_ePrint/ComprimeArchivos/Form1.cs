using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComprimeArchivos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog File = new OpenFileDialog();
            File.Filter = "Archivos ePrint|*.eprint|Todos los archivos|*.*";
            File.DefaultExt = ".eprint";
            try
            {
                if (File.ShowDialog() == DialogResult.OK)
                {
                    ZISCard.CZISCard Comprime = new ZISCard.CZISCard();
                    string fileDestino = File.FileName.Replace(".eprint", ".7z");
                    Comprime.Comprimir(File.FileName, fileDestino);
                    MessageBox.Show("El archivos se ha comprimido correctamente");

                }
            }
            catch (Exception s){MessageBox.Show("Error: "+ s.Message); }

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}