using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_TablaEdicion : System.Web.UI.Page
{
    CeC_Sesion Sesion = null;
    CeC_TablaBD TablaBD;

    protected bool AgregaCampo(string CampoNombre)
    {
        try
        {
            CeC_TablasCampos Campo = new CeC_TablasCampos(TablaBD.m_Tabla, CampoNombre, Sesion);
            if (!Campo.Tabla_Campo_Visible)
                return false;

            object CampoControl = CeC_Campos.CreaCampo(CampoNombre, TablaBD.m_CamposLlave);
            if (CampoControl != null)
            {
                if (CampoControl.GetType().FullName == "Infragistics.WebUI.WebCombo.WebCombo")
                {
                    Infragistics.WebUI.WebCombo.WebCombo Combo = (Infragistics.WebUI.WebCombo.WebCombo)CampoControl;
                    Combo.InitializeLayout += new Infragistics.WebUI.WebCombo.InitializeLayoutEventHandler(Combo_InitializeLayout);

                }
                ((System.Web.UI.WebControls.WebControl)CampoControl).Width = Unit.Pixel(150);
                if(!Campo.Tabla_Campo_Editable)
                    ((System.Web.UI.WebControls.WebControl)CampoControl).Enabled = false;

                try
                { CeC_Campos.AsignaValorCampo(CampoControl, TablaBD.m_Fila[CampoNombre]); }
                catch { }
                TableRow Fila = new TableRow();
                TableCell Cell1 = new TableCell();
                TableCell Cell2 = new TableCell();
                TableCell Cell3 = new TableCell();
                System.Web.UI.WebControls.Label Lbl = new Label();

                CeC_Localizaciones.AplicaLocalizacion(Lbl, CampoNombre,Sesion);
                //Lbl.Text = CeC_Campos.ObtenEtiqueta(CampoNombre);
                //Cell1.Text = CeC_Campos.ObtenEtiqueta(NombreCampo);
                //Cell1.Width = Unit.Pixel(150);
                Lbl.Width = Unit.Pixel(150);
                Cell1.HorizontalAlign = HorizontalAlign.Left;
                Cell1.Controls.Add(Lbl);
                Fila.Cells.Add(Cell1);

                Cell2.HorizontalAlign = HorizontalAlign.Left;
                Cell2.Controls.Add((System.Web.UI.Control)CampoControl);
                CeC_Localizaciones.AplicaLocalizacion((System.Web.UI.Control)CampoControl, CampoNombre, Sesion);
                Fila.Cells.Add(Cell2);
                Cell1.Controls.Add(Lbl);

                System.Web.UI.WebControls.Label Lbl2 = new Label();

                Lbl2.Text = Campo.Tabla_Campo_Ayuda;
                //Cell1.Text = CeC_Campos.ObtenEtiqueta(NombreCampo);
                //Cell1.Width = Unit.Pixel(150);
                Lbl2.Width = Unit.Pixel(150);
                Fila.Cells.Add(Cell3);
                Tabla.Rows.Add(Fila);


                return true;
            }
        }
        catch (Exception ex)
        {
        }
        return false;
    }
    void Combo_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato((Infragistics.WebUI.WebCombo.WebCombo)sender);
    }

    protected bool Guarda()
    {

        string SError = "";
        CeC_Tablas Tablas = new CeC_Tablas(Sesion);
        TablaBD = new CeC_TablaBD(Tablas, Sesion);
        string Param = Sesion.Parametros;
        string[] Campos = CeC.ObtenArregoSeparador(TablaBD.m_Campos, ",");

        foreach (string sCampo in Campos)
        {
            string NombreCampo = sCampo.Trim();
  //          TablaBD.m_Fila[NombreCampo] = "1";
            TablaBD.m_Fila[NombreCampo] = CeC_Campos.ObtenValorCampo(ObtenCampo(NombreCampo));
//            CeC_Campos.ObtenValorCampo(ObtenCampo(NombreCampo), TablaBD.m_Fila[NombreCampo]);
        }

        //CeC_TablasCampos TablaCampo = new CeC_TablasCampos(TablaBD.m_Tabla, Campo, Sesion);
        TablaBD.Guarda(Sesion);
        //string SError = CeC_Campos.GuardaCampo(Campo, Persona_ID, Sesion.SESION_ID);
        if (SError.Length > 0)
        {
            LError.Text += SError + "\n";
            return false;
        }
        return true;
    }

    protected object ObtenCampo(string Nombre)
    {
        foreach (TableRow TR in Tabla.Rows)
        {
            try
            {
                if (TR.Cells[1].Controls[0].ID == Nombre)
                    return TR.Cells[1].Controls[0];
            }
            catch
            {
            }
        }
        return this.FindControl(Nombre);
        foreach (System.Web.UI.Control Contr in this.Controls)
        {
            if (Contr.ID == Nombre)
                return Contr;
        }
        return null;
    }
    protected void CargaDatos()
    {
        CeC_Campos.Inicializa();
        CeC_Tablas Tablas = new CeC_Tablas(Sesion);
        TablaBD = new CeC_TablaBD(Tablas, Sesion);
        string Param = Sesion.Parametros;
        string[] Campos = CeC.ObtenArregoSeparador(TablaBD.m_Campos,",");

        foreach (string Campo in Campos)
        {
            string NombreCampo = Campo.Trim();
            AgregaCampo(NombreCampo);
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        CargaDatos();
    }

    protected void Btn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = "";
        LCorrecto.Text = "";
        Guarda();
    }
}