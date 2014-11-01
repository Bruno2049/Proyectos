using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_ReorganizarEmpleados : System.Web.UI.Page
{
    CeC_Sesion Sesion = null;
    public bool Valida()
    {

        if (Sesion.SESION_ID > 0 && (Sesion.PERFIL_ID == 1 || Sesion.PERFIL_ID == 4))
        {
            return true;
        }
        Sesion.MsgNoTienePermiso();
        return false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Sesion = CeC_Sesion.Nuevo(this);
        if (Valida())
        {
            if (!IsPostBack)
            {
                string Campos = Sesion.ConfiguraSuscripcion.CamposAgrupaciones;
                if (Campos.Length > 0)
                {
                    string[] sCampos = CeC.ObtenArregoSeparador(Campos, "|");
                    foreach (string sCampo in sCampos)
                        AgregaCampo(sCampo);
                }
                Cmb_Campos.DataSource = CeC_Campos.ObtenListaCamposTEDataSet();
                Cmb_Campos.DataTextField = "CAMPO_ETIQUETA";
                Cmb_Campos.DataValueField = "CAMPO_NOMBRE";
                Cmb_Campos.DataBind();
            }
        }
    }
    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = "";
        LCorrecto.Text = "";
        if (Valida())
        {
            if(Lbx_Campos.Items.Count < 1)
            {
                LError.Text = "Tiene que elegir los campos con los que se generará la agrupación";
                return;
            }
            string Campos = "";
            foreach(ListItem LI in Lbx_Campos.Items)
            {
                if(Campos.Length > 0)
                    Campos+= "|";
                Campos += LI.Value;
            }
            bool Ret = CeC_Agrupaciones.RegeneraAgrupaciones(Sesion.SUSCRIPCION_ID, Campos, true);
            if (Ret)
            {
                Sesion.ConfiguraSuscripcion.CamposAgrupaciones = Campos;
                LCorrecto.Text = "Se han guardado los datos satisfactoriamente <BR> Requerira actualizar la pagina actual para ver los cambios ";
                return;
            }
            LError.Text = "No se pudieron aplicar los cambios";
        }
    }
    private void AgregaCampo(string Campo)
    {
        string Etiqueta = CeC_Campos.ObtenEtiqueta(Campo);
        AgregaCampo(Campo, Etiqueta);
    }
    private void AgregaCampo(string Campo, string Etiqueta)
    {
        ListItem LI = new ListItem(Etiqueta, Campo);
        Lbx_Campos.Items.Add(LI);
    }

    protected void Btn_Agregar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (Cmb_Campos.SelectedItem != null)
        {
            ListItem LAnt = Cmb_Campos.SelectedItem;
            AgregaCampo(LAnt.Value, LAnt.Text);
        }
    }
    protected void Btn_Quitar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (Lbx_Campos.SelectedItem != null)
        {
            Lbx_Campos.Items.Remove(Lbx_Campos.SelectedItem);
        }
    }
    protected void Btn_Subir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (Lbx_Campos.SelectedItem != null)
        {
            
            //Lbx_Campos.Items.Remove(Lbx_Campos.SelectedItem);
        }
    }
    protected void BGuardarCambios1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (Lbx_Campos.SelectedItem != null)
        {
            
        }
    }
}
