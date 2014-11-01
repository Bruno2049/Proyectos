using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WF_CreacionTurnosExpress : System.Web.UI.Page
{
    DS_CreacionTurnosExpress DS = new DS_CreacionTurnosExpress();
    DS_CreacionTurnosExpressTableAdapters.EC_TURNOSTableAdapter TA = new DS_CreacionTurnosExpressTableAdapters.EC_TURNOSTableAdapter();
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.Parametros.Length > 1 && Sesion.Parametros.Substring(1) != "null")
        {
            BGuardarCambios_Click(null, null);
        }

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Turnos0CreacionExpress, true))
        {
            UWGTurnos.Visible = Webpanel1.Visible = BGuardarCambios.Visible = false;
            return;
        }
        //**************************************************

        if (!IsPostBack)
        {
            txtComida.Text = Sesion.ConfiguraSuscripcion.ComidaTurnosExpress.ToString();
            txtTolerancia.Text = Sesion.ConfiguraSuscripcion.ToleranciaTurnosExpress.ToString();

            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Creación de Turnos Express", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
        Sesion.TituloPagina = "Creación de Turnos Express";
        Sesion.DescripcionPagina = "Seleccione un turno y de doble click sobre los datos a modificar y al estar activado el campo modifíquelos. Para elegir que días tendrá ese turno, seleccione las casillas para cada día de la semana. Si desea crear un nuevo turno, haga lo mismo sobre la fila en blanco en la parte inferior de la tabla. Los datos de hora deberán ir en formato HH:MM de las 00:00 a las 23:59. Los Datos Generales se aplican para todos los turnos. Si desea crear turnos mas complejos, lea el manual.";
        if (Sesion.EsWizard > 0)
        {
            btnGuardarCambios.Text = "Siguiente";
            btnGuardarCambios.Appearance.Image.Url = "./Imagenes/gtk-go-forward.png";
            Sesion.TituloPagina = "Asistente de Configuracion (Creacion de Turnos Express)";
          //  Sesion.DescripcionPagina = "Creacion de Turnos Express";
        }

    }

    protected void UWGTurnos_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        try
        {
            CeC_Grid.AplicaFormato(UWGTurnos, false, false, false, true);
            for (int i = 4; i < 10; i++)
                UWGTurnos.Columns[i].Width = 60;
            UWGTurnos.Columns[0].Hidden = true;
            UWGTurnos.Columns[2].EditorControlID = "txthora";
            UWGTurnos.Columns[3].EditorControlID = "txthora";
            UWGTurnos.Columns[2].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.Custom;
            UWGTurnos.Columns[3].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.Custom;
            UWGTurnos.Columns[2].Width = 120;
            UWGTurnos.Columns[3].Width = 120;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_CreacionTurnosExpress.UWGTurnos_InitializeLayout", ex);
        }
    }

    protected void UWGTurnos_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        try
        {
            Sesion = CeC_Sesion.Nuevo(this);
            TA.FillTurno(DS.EC_TURNOS, Sesion.SUSCRIPCION_ID);
            UWGTurnos.DataSource = DS.EC_TURNOS;
            UWGTurnos.DataBind();
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_CreacionTurnosExpress.UWGTurnos_InitializeDataSource", ex);
        }
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int MinutosTolerancia = Convert.ToInt32(txtTolerancia.Text);
        int MinutosComida = Convert.ToInt32(txtComida.Text);
        Sesion.ConfiguraSuscripcion.ToleranciaTurnosExpress = MinutosTolerancia;
        Sesion.ConfiguraSuscripcion.ComidaTurnosExpress = MinutosComida;
        for (int i = 0; i < UWGTurnos.Rows.Count; i++)
        {
            int turnoid =Convert.ToInt32(UWGTurnos.Rows[i].Cells[UWGTurnos.Columns.FromKey("TURNO_ID").Index].Value);
            string nombreturno;
            if (turnoid == 0)
            {
                turnoid = CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS", "TURNO_ID",Sesion);
                nombreturno = UWGTurnos.Rows[i].Cells[UWGTurnos.Columns.FromKey("TURNO_NOMBRE").Index].Value.ToString();
                CeC_BD.EjecutaComando("INSERT INTO EC_TURNOS (TURNO_ID, TIPO_TURNO_ID, TURNO_NOMBRE, TURNO_ASISTENCIA, TURNO_PHEXTRAS, TURNO_INDIVIDUAL) VALUES ("+ turnoid +",5,'"+ nombreturno +"',1,1,0)");
            }
            else
            {
                //update
                CeC_BD.EjecutaComando("UPDATE EC_TURNOS SET TURNO_NOMBRE='" + UWGTurnos.Rows[i].Cells[1].Value.ToString() + "' WHERE TURNO_ID=" + turnoid);
                //borar turnossemanaldia
                CeC_BD.EjecutaComando("DELETE FROM EC_TURNOS_SEMANAL_DIA WHERE TURNO_ID =" + turnoid);
            }
            TimeSpan hentrada = Convert.ToDateTime(UWGTurnos.Rows[i].Cells[2].Text).TimeOfDay;
            TimeSpan hsalida = Convert.ToDateTime(UWGTurnos.Rows[i].Cells[3].Text).TimeOfDay;
            if(hsalida < hentrada)
                hsalida = hsalida.Add(new TimeSpan(1,0, 0, 0));
            else
                if (hentrada.Days > 1)
                {
                    hentrada = hentrada.Add(new TimeSpan(-1,0, 0, 0));
                    hsalida = hsalida.Add(new TimeSpan(-1,0, 0, 0));
                }

            TimeSpan DiferenciaHorarios = hentrada + new TimeSpan(24, 0, 0) - hsalida;
            TimeSpan MediaHorarios = new TimeSpan(0, 0,Convert.ToInt32( DiferenciaHorarios.TotalSeconds / 2));
            DateTime DTHE = CeC_BD.TimeSpan2DateTime(hentrada);
            DateTime DTHERetardo = CeC_BD.TimeSpan2DateTime(hentrada).AddMinutes(MinutosTolerancia);
            DateTime DTHEMin = CeC_BD.TimeSpan2DateTime(hentrada) - MediaHorarios;
            DateTime DTHEMax = CeC_BD.TimeSpan2DateTime(hsalida).AddMinutes(-MinutosTolerancia-1).AddSeconds(59);
            DateTime DTHSMin = CeC_BD.TimeSpan2DateTime(hsalida).AddMinutes(-MinutosTolerancia);
            DateTime DTHS = CeC_BD.TimeSpan2DateTime(hsalida);
            DateTime DTHSMax = CeC_BD.TimeSpan2DateTime(hsalida) + MediaHorarios;

            DateTime DTHCTiempo = CeC_BD.FechaNula.AddMinutes(MinutosComida);
            DateTime DTHCTolera = CeC_BD.FechaNula.AddMinutes(MinutosTolerancia);

            int TurnoDia_ID = CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS_DIA", "TURNO_DIA_ID",Sesion);
/*            int idturnodia = CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS_DIA", "TURNO_DIA_ID");

            TimeSpan H24 = new TimeSpan(24, 0, 0);
            TimeSpan Medio = hentrada + H24 - hsalida;

            TimeSpan Medio2 = new TimeSpan(Medio.Ticks / 2) + hsalida;
            TimeSpan HEMin = Medio2.Subtract(new TimeSpan(Convert.ToInt32(Medio2.TotalDays), 0, 0, 0));
            TimeSpan HEMax = hsalida.Subtract(new TimeSpan(1,0,0));
            string Tolerancia = CeC_Config.ToleranciaTurnosExpress.ToString();
            string Comida = CeC_Config.ComidaTurnosExpress.ToString();
            
            string salidamin = CeC_BD.SqlFechaHora(CeC_BD.TimeSpan2DateTime(hsalida));*/
                                          
           int a = CeC_BD.EjecutaComando("INSERT INTO EC_TURNOS_DIA (TURNO_DIA_ID," +
               " TURNO_DIA_HE, TURNO_DIA_HEMIN, TURNO_DIA_HEMAX," +
               " TURNO_DIA_HERETARDO, TURNO_DIA_HS," +
               " TURNO_DIA_HSMIN, TURNO_DIA_HSMAX," +
               " TURNO_DIA_HCTIEMPO, TURNO_DIA_HCTOLERA, TURNO_DIA_PHEX, TURNO_DIA_HAYCOMIDA) VALUES (" + TurnoDia_ID + "," +
               CeC_BD.SqlFechaHora(DTHE) + "," + CeC_BD.SqlFechaHora(DTHEMin) + "," + CeC_BD.SqlFechaHora(DTHEMax) + "," +
               CeC_BD.SqlFechaHora(DTHERetardo) + "," + CeC_BD.SqlFechaHora(DTHS) + "," +
               CeC_BD.SqlFechaHora(DTHSMin) + "," + CeC_BD.SqlFechaHora(DTHSMax) + "," +
               CeC_BD.SqlFechaHora(DTHCTiempo) + "," + CeC_BD.SqlFechaHora(DTHCTolera) + ",1,1)");

            for (int j = 4; j < 11; j++)
            {
                
                //agrega turnos_semanal_dia
                if(Convert.ToInt32(UWGTurnos.Rows[i].Cells[j].Value)>0)
                {
                    int idturnosemanaldia = CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS_SEMANAL_DIA", "TURNO_SEMANAL_DIA_ID", Sesion);
                    CeC_BD.EjecutaComando("INSERT INTO EC_TURNOS_SEMANAL_DIA (TURNO_SEMANAL_DIA_ID,TURNO_DIA_ID, DIA_SEMANA_ID, TURNO_ID) VALUES (" + idturnosemanaldia + "," + TurnoDia_ID + "," + (j - 3) + "," + turnoid + ")");
                }
            }  
        }


     }
}
