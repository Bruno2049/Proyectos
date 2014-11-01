using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace eClockSync5
{
    public partial class Frm_Sitios : Form
    {
        public string SitiosIDs = "";
        public string SitiosIDsSeleccionados = "";
        public Frm_Sitios()
        {
            InitializeComponent();
        }

        private void Lnk_Sitios_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Frm_Vista.MuestraUrl("Sitios");
        }

        private void Frm_Sitios_Load(object sender, EventArgs e)
        {
            DataSet DS = CeS_eCheck.ObtenSitios();
            if(DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow DR in DS.Tables[0].Rows)
                {
                    CbxLst_Sitios.Items.Add(CeC.Convierte2String(DR["SITIO_NOMBRE"]) + "(" + CeC.Convierte2String(DR["SITIO_ID"]) + ")", CeC.ExisteEnSeparador(SitiosIDs, CeC.Convierte2String(DR["SITIO_ID"]), ","));
                }
            }                        
        }

        private void Btn_Aceptar_Click(object sender, EventArgs e)
        {

            foreach (object Seleccionado in CbxLst_Sitios.CheckedItems)
            {
                string Texto = CeC.Convierte2String(Seleccionado);
                int PosPar = Texto.LastIndexOf("(") + 1;
                string ID = Texto.Substring(PosPar, Texto.LastIndexOf(")") - PosPar);
                SitiosIDsSeleccionados = CeC.AgregaSeparador(SitiosIDsSeleccionados, ID, ",");
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
