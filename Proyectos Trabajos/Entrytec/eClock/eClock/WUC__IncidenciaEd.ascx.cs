using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WUC__IncidenciaEd : System.Web.UI.UserControl
{
    CeC_Sesion Sesion;
    string PorInsertar;
    
    string PersonasDiariosIds;
    string Error = "";
    string Correcto = "";
    protected bool AgregaCampo(string CampoNombre, int TipoIncidenciaRID, CeC_Interprete Interprete)
    {
        try
        {
            CeC_Campos_Inc_R Campo = new CeC_Campos_Inc_R(CampoNombre, TipoIncidenciaRID, Sesion);

            object CampoControl = CeC_Campos.CreaCampo(CampoNombre, "");
            if (CampoControl != null)
            {
                if (CampoControl.GetType().FullName == "Infragistics.WebUI.WebCombo.WebCombo")
                {
                    Infragistics.WebUI.WebCombo.WebCombo Combo = (Infragistics.WebUI.WebCombo.WebCombo)CampoControl;
                    Combo.InitializeLayout += new Infragistics.WebUI.WebCombo.InitializeLayoutEventHandler(Combo_InitializeLayout);

                }
                try
                {
                    CeC_Campos.AsignaValorCampo(CampoControl, Interprete.ObtenValor(CampoNombre));
                }
                catch { }
                ((System.Web.UI.WebControls.WebControl)CampoControl).Width = Unit.Pixel(150);
                //Indica si el campo es obligatorio
                //if (!Campo.Campo_Inc_R_Obl)
                //    ((System.Web.UI.WebControls.WebControl)CampoControl).Enabled = false;
                TableRow Fila = new TableRow();
                TableCell Cell1 = new TableCell();
                TableCell Cell2 = new TableCell();
                TableCell Cell3 = new TableCell();
                System.Web.UI.WebControls.Label Lbl = new Label();

                Lbl.Text = CeC_Campos.ObtenEtiqueta(CampoNombre);
                //Cell1.Text = CeC_Campos.ObtenEtiqueta(NombreCampo);
                //Cell1.Width = Unit.Pixel(150);
                Lbl.Width = Unit.Pixel(150);
                Cell1.HorizontalAlign = HorizontalAlign.Left;
                Cell1.Controls.Add(Lbl);
                Fila.Cells.Add(Cell1);

                Cell2.HorizontalAlign = HorizontalAlign.Left;
                Cell2.Controls.Add((System.Web.UI.Control)CampoControl);
                Fila.Cells.Add(Cell2);
                Cell1.Controls.Add(Lbl);

                System.Web.UI.WebControls.Label Lbl2 = new Label();

                //Lbl2.Text = Campo.Tabla_Campo_Ayuda;
                //Cell1.Text = CeC_Campos.ObtenEtiqueta(NombreCampo);
                //Cell1.Width = Unit.Pixel(150);
                Lbl2.Width = Unit.Pixel(150);
                Fila.Cells.Add(Cell3);
                Tbl_Campos.Rows.Add(Fila);
                return true;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    void Combo_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato((Infragistics.WebUI.WebCombo.WebCombo)sender);
    }


    protected string ObtenCampoTexto(object Campo)
    {
        try
        {

            if (Campo == null)
                return "";
            string SError = "";
            object Obj = CeC_Campos.ObtenValorCampo(Campo);
            return Obj.ToString();
            // string SError = CeC_Campos.GuardaCampo(Campo, Persona_ID, Sesion.SESION_ID);
            if (SError.Length > 0)
            {
                Error += SError + "\n";
                return "";
            }
            return "";
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "";
    }

    protected object ObtenCampo(string Nombre)
    {
        foreach (TableRow TR in Tbl_Campos.Rows)
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

    public bool CargaDatos(int TipoIncidenciaRID, string Valores)
    {
        CeC_Campos.Inicializa();
        CeC_Interprete Interprete = new CeC_Interprete(Valores);
        string[] Campos = CeC.ObtenArregoSeparador(CeC_Campos_Inc_R.ObtenCampos(TipoIncidenciaRID, Sesion), ",");
        if (Campos.Length > 0)
        {
           /* Tbx_Comentario.Visible = false;
            Lbl_Comentario.Visible = false;*/
        }
        foreach (string Campo in Campos)
        {
            string NombreCampo = Campo.Trim();
            AgregaCampo(NombreCampo, TipoIncidenciaRID, Interprete);
        }

        return true;
    }
    public string ObtenValores(int TipoIncidenciaRID, string Valores)
    {
        CeC_Campos.Inicializa();
        CeC_Interprete Interprete = new CeC_Interprete(Valores);
        string[] Campos = CeC.ObtenArregoSeparador(CeC_Campos_Inc_R.ObtenCampos(TipoIncidenciaRID, Sesion), ",");
        if (Campos.Length > 0)
        {
            /* Tbx_Comentario.Visible = false;
             Lbl_Comentario.Visible = false;*/
        }
        foreach (string Campo in Campos)
        {
            string NombreCampo = Campo.Trim();
            Interprete.AsignaParametro(Campo, ObtenCampoTexto(ObtenCampo(Campo)));            
        }
        return Interprete.ObtenCadena();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}