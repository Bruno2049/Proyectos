using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SMS.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace eClockSync5
{
    public partial class Frm_Asistente : WizardForm
    {
        public Frm_Asistente()
        {
            InitializeComponent(); 
            Controls.AddRange(new Control[] {
		        new Usc_Bienvenida(),
		        new Usc_Licencia(),
                new Usc_Serial(),
                new Usc_Usuario(),                
                new Usc_Suscripcion(),
                new Usc_Sitios()
		        });
        }

        private void Frm_Asistente_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string idioma = "pt";
            CambiarIdioma(idioma);            
        }

        // Metodo para aplicar el cambio que tiene como parametro el idioma
        private void CambiarIdioma(string idioma)
        {
            Culture = new CultureInfo(idioma);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(idioma);
            eClockSync5.Properties.Settings.Default.lang = idioma;
            eClockSync5.Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string idioma = "fr";
            CambiarIdioma(idioma);  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string idioma = "";
            CambiarIdioma(idioma);  
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string idioma = "en";
            CambiarIdioma(idioma);  
        }

        private void m_nextButton_Click(object sender, EventArgs e)
        {

        }


    }
}
