using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_IncidenciasNueva : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    string PorInsertar;
    int TipoIncidenciaRID;
    string PersonasDiariosIds;


    void Combo_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato((Infragistics.WebUI.WebCombo.WebCombo)sender);
    }

    protected void CargaDatos()
    {
        CeC_Campos.Inicializa();
        string[] Campos = CeC.ObtenArregoSeparador(CeC_Campos_Inc_R.ObtenCampos(TipoIncidenciaRID, Sesion), ",");
        if (Campos.Length > 0)
        {
            Tbx_Comentario.Visible = false;
            Lbl_Comentario.Visible = false;
            CamposIncidencia.CargaDatos(TipoIncidenciaRID, "");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        PorInsertar = CeC.ObtenColumnaSeparador(Sesion.Parametros, "@", 0);
        TipoIncidenciaRID = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(PorInsertar, "|", 0));
        PersonasDiariosIds = CeC.Convierte2String(CeC.ObtenColumnaSeparador(PorInsertar, "|", 1));

        if (!IsPostBack)
        {
            Lbl_TipoIncidencia.Text = CeC_IncidenciasRegla.ObtenTipoIncidenciaNombre(TipoIncidenciaRID);
            Lbl_PerFechas.Text = CeC_Asistencias.ObtenTexto(PersonasDiariosIds);
            Lbl_Html.Text = CeC_IncidenciasRegla.ObtenHTML(TipoIncidenciaRID, PersonasDiariosIds);
            if (CeC_TiempoXTiempos.EsTiempoXTiempo(CeC_IncidenciasRegla.ObtenTipoIncidenciaID(TipoIncidenciaRID)))
            {
                if (CeC.ObtenArregoSeparador(PersonasDiariosIds, ",", true).Length > 1)
                {
                    Btn_Guardar.Visible = false;
                    LError.Text = "No se permite justificar mas de un día para este tipo de incidencia";
                }
                else
                {
                    foreach (ListItem itemRadioBtn in Rbl_TipoPermiso.Items)
                    {
                        itemRadioBtn.Attributes.Add("onClick", "Rbl_TipoPermiso_OnClick(this);");
                    }
                    Rbl_TipoPermiso_SelectedIndexChanged(sender, e);
                    Gbx_Accesos.Visible = true;
                }
            }
        }
        CargaDatos();
    }


    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        
    }

    public string ObtenComentarios()
    {
        string Ret = "";
        string[] Campos = CeC.ObtenArregoSeparador(CeC_Campos_Inc_R.ObtenCampos(TipoIncidenciaRID, Sesion), ",");
        if (Campos.Length <= 0)
            return Tbx_Comentario.Text;
        return CamposIncidencia.ObtenValores(TipoIncidenciaRID,"");
/*        TipoIncidenciaRID
        foreach (string Campo in Campos)
        {
            string NombreCampo = Campo.Trim();
            Ret = CeC.AgregaSeparador(Ret, NombreCampo + "=" + CeC_Campos.ObtenValorCampo(ObtenCampo(NombreCampo)).ToString(), "&");
        }

        return Ret;*/
    }
    protected void Btn_Guardar_Click(object sender, EventArgs e)
    {
        try
        {
            LError.Text = "";
            LCorrecto.Text = "";
            //Sesion.Redirige("WF_AsistenciasEmp.aspx?Parametros=REGRESO");
            if (Lbl_Html.Text.IndexOf("Saldo Insuficiente") > 0)
            {
                LError.Text = "No se puede guardar porque no tiene saldo suficiente";
                return;
            }
            int TipoIncidenciaID = CeC_IncidenciasRegla.ObtenTipoIncidenciaID(TipoIncidenciaRID);
            if (CeC_TiempoXTiempos.EsTiempoXTiempo(TipoIncidenciaID))
            {
                DateTime Hora1 = CeC.Convierte2DateTime(Tbx_Hora1.Value);
                DateTime Intervalo = CeC.Convierte2DateTime(Tbx_Hora2.Value);
                DateTime Hora2 = CeC_BD.FechaNula;
                int PersonaDiarioID = CeC.Convierte2Int(PersonasDiariosIds);
                DateTime Fecha = CeC_Asistencias.ObtenFecha(PersonaDiarioID);
                switch (Rbl_TipoPermiso.SelectedValue)
                {
                    case "1":
                        Hora2 = Hora1;
                        Hora1 = Hora2 - CeC_BD.DateTime2TimeSpan(Intervalo);
                        break;
                    case "2":
                        Hora2 = Hora1 + CeC_BD.DateTime2TimeSpan(Intervalo);
                        break;
                    case "3":
                        Hora1 = Fecha + Hora1.TimeOfDay;
                        Hora2 = Fecha + Intervalo.TimeOfDay;
                        if (Hora2 < Hora1)
                            Hora2 = Hora2.AddDays(1);
                        Intervalo = CeC_BD.TimeSpan2DateTime(Hora2 - Hora1);
                        break;
                }
                CeC_TiempoXTiempos TxT = new CeC_TiempoXTiempos(TipoIncidenciaID, Sesion);
                string Extra = "";
                int TipoIncAccesoID = 1;
                bool AsignarIncidenciaTodoElDia = true;
                if (CeC_Accesos_Jus.NuevaJustificacion(1, Intervalo, Tbx_Comentario.Text, true, PersonaDiarioID, Hora1, Hora2, out Extra, Sesion))
                {
                    decimal Valor = TxT.ObtenValor(CeC_BD.DateTime2TimeSpan(Intervalo));
                    if (Valor == 0)
                    {
                        LError.Text = "No se tiene periodo a justificar";
                        return;
                    }
                    if (AsignarIncidenciaTodoElDia)
                    {
                        int IncidenciaID = Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, Tbx_Comentario.Text, Sesion.SESION_ID);
                        if (IncidenciaID > 0)
                        {
                            Cec_Incidencias.AsignaIncidencia(PersonaDiarioID, IncidenciaID);
                            Extra = CeC.AgregaSeparador(Extra, "INCIDENCIA_ID=" + IncidenciaID, "|");
                        }
                    }
                    int TipoIncidenciaRID_Resta = CeC_TiempoXTiempos.IncidenciaRegla2IncidenciaReglaResta(TipoIncidenciaRID, PersonaDiarioID, Sesion);
                    int PersonaID = CeC_Asistencias.ObtenPersonaID(PersonaDiarioID);
                    int ALMACEN_INC_ID = CeC_IncidenciasInventario.AgregaMovimiento(PersonaID, TipoIncidenciaRID_Resta, CeC_IncidenciasInventario.TipoAlmacenIncidencias.Normal,
                        Fecha, -Valor,
                        CeC_BD.FechaNula, false,
                        Tbx_Comentario.Text, Extra, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
                    if (ALMACEN_INC_ID > 0)
                    {
                        LCorrecto.Text = "Movimiento realizado satisfactoriamente";
                        Btn_Guardar.Visible = false;
                        return;
                    }
                }
            }
            else
            {
                if (CeC_IncidenciasRegla.AsignaIncidencia(TipoIncidenciaRID, PersonasDiariosIds, ObtenComentarios(), Sesion.SESION_ID) > 0)
                {
                    LCorrecto.Text = "Movimiento realizado satisfactoriamente";
                    Btn_Guardar.Visible = false;
                    return;
                }
            }
            LError.Text = "No se puede guardar porque hay saldo insuficiente";
        }
        catch { LError.Text = "No se pudo guardar, parámetros no validos"; }
        return;

    }
    protected void Rbl_TipoPermiso_SelectedIndexChanged(object sender, EventArgs e)
    {
        int PersonaDiarioID = CeC.Convierte2Int(PersonasDiariosIds);
        DateTime Hora1 = CeC_BD.FechaNula;
        DateTime Hora2 = CeC_BD.FechaNula;
        TimeSpan Tiempo = new TimeSpan(0, 0, 0);
        switch (Rbl_TipoPermiso.SelectedValue)
        {
            case "1":
                Lbl_Hora1.Text = "Hora de salida";
                Lbl_Hora2.Text = "Tiempo a justificar";
                Hora1 = CeC_Asistencias.ObtenHoraSalida(PersonaDiarioID);
                Hora2 = CeC_Asistencias.ObtenUltimaChecada(PersonaDiarioID, Sesion);
                if (Hora2 > Hora1)
                    Hora2 = Hora1;
                Tiempo = Hora1 - Hora2;
                Hora2 = CeC_BD.TimeSpan2DateTime(Tiempo);
                Tbx_Hora1.Enabled = false;
                Tbx_Hora2.Enabled = false;
                break;
            case "2":
                Lbl_Hora1.Text = "Hora de entrada";
                Lbl_Hora2.Text = "Tiempo justificado";
                Hora1 = CeC_Asistencias.ObtenHoraEntrada(PersonaDiarioID);
                Hora2 = CeC_Asistencias.ObtenPrimeraChecada(PersonaDiarioID, Sesion);
                if (Hora2 < Hora1)
                    Hora2 = Hora1;
                Tiempo = Hora2 - Hora1;

                Hora2 = CeC_BD.TimeSpan2DateTime(Tiempo);
                Tbx_Hora1.Enabled = false;
                Tbx_Hora2.Enabled = false;

                break;
            case "3":
                Lbl_Hora1.Text = "Hora de salida";
                Lbl_Hora2.Text = "Hora de regreso";
                Tbx_Hora1.Enabled = true;
                Tbx_Hora2.Enabled = true;
                Hora1 = CeC_Asistencias.ObtenHoraEntrada(PersonaDiarioID);
                Hora2 = CeC_Asistencias.ObtenHoraSalida(PersonaDiarioID);
                Tiempo = Hora2 - Hora1;
                break;
        }
        Tbx_Hora1.Value = Hora1;
        Tbx_Hora2.Value = Hora2;

        Lbl_Html.Text = CeC_TiempoXTiempos.ObtenHTML(TipoIncidenciaRID, PersonaDiarioID, Sesion, Tiempo);
    }
    protected void Btn_Regresar_Click(object sender, EventArgs e)
    {
        Sesion.Redirige("WF_AsistenciasEmp.aspx?Parametros=REGRESO");
    }

}
