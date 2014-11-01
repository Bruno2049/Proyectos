using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SMS.Windows.Forms;

namespace eClockSync5
{
    public partial class Usc_Suscripcion : InteriorWizardPage
    {
        public Usc_Suscripcion()
        {
            InitializeComponent();
        }

        private void Usc_Suscripcion_Load(object sender, EventArgs e)
        {
            Cbx_Suscripcion_Pais.Items.Add("Mexico");
            Cbx_Suscripcion_Pais.Items.Add("Estados Unidos");
            Cbx_Suscripcion_Pais.Items.Add("Brazil");
            Cbx_Suscripcion_Pais.Items.Add("Francia");
            Cbx_Suscripcion_Pais.Items.Add("Equestria");



            //ViewBag.PERFILES = new eClockMobile.Models.BaseListaModel(eClockMobile.CeC_SesionMVC.SESION_SEGURIDAD, App_GlobalResources.Recursos.Suscripcion_Edicion_TituloGuardar, "EC_SUSCRIPCIONES", "SUSCRIPCION_ID", "SUSCRIPCION_NOMBRE", "", "", false, eClockMobile.CeC_SesionMVC.SUSCRIPCION_ID_SELECCIONADA);


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected override string OnWizardNext()
        {
            MessageBox.Show("Hola");
            return ("Usc_Sitios");
        }
    }
}
