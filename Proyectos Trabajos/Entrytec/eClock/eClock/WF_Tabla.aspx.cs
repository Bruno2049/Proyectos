using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infragistics.Web.UI.NavigationControls;
using Infragistics.Web.UI.GridControls;
using Infragistics.Documents.Reports;

public partial class WF_Tabla : System.Web.UI.Page
{

    CeC_Sesion Sesion = null;
    CeC_TablaBD Tabla = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        bool Actualiza = true;
        bool ForzaActualiza = false;
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.SESION_ID <= 0)
            return;


        /*     //Solo se deberá ejecutar esta linea en pruebas y exista un cambio en el diseño de la bd.
             if(!IsPostBack)
                 CeC_Tablas.IniciaTablas(Sesion);
             */

        if (Request.Params["__EVENTTARGET"] == "Menu")
        {
            Actualiza = false;
            DataMenuItem Elemento = ObtenElementoMenu(CeC.ObtenColumnaSeparador(Request.Params["__EVENTARGUMENT"], "<~>", 0));
            if (Elemento.Key == "TXT")
                ExportarTXT();
            if (Elemento.Key == "CSV")
                ExportarCSV();

            if (Elemento.Key == "Borrar")
                Borrar(CeC.ObtenColumnaSeparador(Request.Params["__EVENTARGUMENT"], "<~>", 1));

            if (Elemento.Key == "Mostrar Borrados")
            {
                bool EstadoMuestraBoorado = MuestraBorrado();
                Elemento.Value = (!EstadoMuestraBoorado).ToString();
                if (!EstadoMuestraBoorado)
                    Elemento.Text = "Ocultar Borrados";
                else
                    Elemento.Text = "Mostrar Borrados";
                Grid.Rows.Clear();
               // Grid.ClearDataSource();
                ForzaActualiza = true;
                Actualiza = true;
            }            
        }

        if(Actualiza)
        {


            Tabla = new CeC_TablaBD(new CeC_Tablas(Sesion), Sesion);
            Tabla.Grid_DataBinding(Grid, Sesion, MuestraBorrado());
            if(ForzaActualiza)
                Grid.DataBind();
        }
        if (!IsPostBack)
            CargaComandos();



        if (Request.Params["__EVENTTARGET"] == "Menu")
        {
            DataMenuItem Elemento = ObtenElementoMenu(CeC.ObtenColumnaSeparador(Request.Params["__EVENTARGUMENT"], "<~>", 0));
            string Tag = Elemento.Key;
            string TagPadre = "";
            try
            {
                TagPadre = Elemento.ParentItem.Key;
            }
            catch { }
            if (Tag == "Excel" || TagPadre == "PDF" || TagPadre == "XPS")
            {
                if (Tag == "Excel")
                {
                    GridExcel.DownloadName = "Exportacion.xls";
                    GridExcel.Export(Grid);
                }
                if (Tag == "XPS" || Tag == "XPS_Vertical")
                {
                    GridExporter.Format = FileFormat.XPS;
                    GridExporter.TargetPaperOrientation = Infragistics.Documents.Reports.Report.PageOrientation.Portrait;
                    GridExporter.DownloadName = "Exportacion.xps";
                }
                if (Tag == "XPS_Horizontal")
                {
                    GridExporter.Format =FileFormat.XPS;
                    GridExporter.TargetPaperOrientation = Infragistics.Documents.Reports.Report.PageOrientation.Landscape;
                    GridExporter.DownloadName = "Exportacion.xps";
                }

                if (Tag == "PDF" || Tag == "PDF_Vertical")
                {
                    GridExporter.Format = FileFormat.PDF;
                    GridExporter.TargetPaperOrientation = Infragistics.Documents.Reports.Report.PageOrientation.Portrait;
                    GridExporter.DownloadName = "Exportacion.pdf";
                }
                if (Tag == "PDF_Horizontal")
                {
                    GridExporter.Format = FileFormat.PDF;
                    GridExporter.TargetPaperOrientation = Infragistics.Documents.Reports.Report.PageOrientation.Landscape;
                    GridExporter.DownloadName = "Exportacion.pdf";
                }

                if (Tag == "PDF" || TagPadre == "PDF" || Tag == "XPS" || TagPadre == "XPS")
                {
                    GridExporter.Export(Grid);
                }

                return;
            }
        }

    }
    protected bool MuestraBorrado()
    {
        return CeC.Convierte2Bool(ObtenElementoMenu("Mostrar Borrados").Value);
    }
    protected bool CargaComandos()
    {
        CeC_TablasComandos.CreaDefinicionCampos(Sesion);
        CeC_TablasComandos Comandos = new CeC_TablasComandos(Sesion);
        DataSet DS = Comandos.ObtenDS(Sesion, "TABLA_NOMBRE = '" + Tabla.m_Tabla + "'", "TABLA_COMANDO_NOMBRE");
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                CeC_TablasComandos Comando = new CeC_TablasComandos(Sesion);
                Comando.Carga(DR);
                ActualizaElementoMenu(Comando);
            }
            return true;
        }
        return false;
    }
    protected DataMenuItem ActualizaElementoMenu(CeC_TablasComandos Comando)
    {

        DataMenuItem Elemento = null;
        try
        {
            Elemento = ObtenElementoMenu(Comando.Tabla_Comando_Nombre);
            if (Elemento == null)
            {
                Elemento = CreaElementoMenu(Menu.Items, Comando.Tabla_Comando_Nombre, Comando.Tabla_Comando_Nombre);
            }
            if (Comando.Tabla_Comando_Tooltip != "")
                Elemento.ToolTip = Comando.Tabla_Comando_Tooltip;
            if (Comando.Tabla_Comando_Etiqueta != "")
                Elemento.Text = Comando.Tabla_Comando_Etiqueta;
            Elemento.Value = Comando.Tabla_Comando_Codigo;
        }
        catch { }
        return Elemento;
    }
    protected DataMenuItem CreaElementoMenu(DataMenuItemCollection Padre, string Path, string Llave)
    {
        int Pos = Path.IndexOf('\\');
        if (Pos > 0)
        {
            return CreaElementoMenu(Padre.Add(Path.Substring(0, Pos)).Items, Path.Substring(Pos + 1), Llave);
        }
        return Padre.Add(Llave, Llave);
    }
    protected DataMenuItem ObtenElementoMenu(string Posicion)
    {
        DataMenuItem Elemento = null;
        
        Elemento = Menu.Items.FindDataMenuItemByKey(Posicion);
        if (Elemento != null)
            return Elemento;
        string[] sPosiciones = CeC.ObtenArregoSeparador(CeC.ObtenColumnaSeparador(Posicion, ":", 1), ".");
        DataMenuItemCollection Elementos = Menu.Items;

        foreach (string sPosicion in sPosiciones)
        {
            Elemento = Elementos[CeC.Convierte2Int(sPosicion)];
            Elementos = Elemento.Items;
        }
        return Elemento;
    }
    protected void ExportarTXT()
    {
        CeC_TablaBD Tabla = new CeC_TablaBD(new CeC_Tablas(Sesion), Sesion);
        string Exportacion = Tabla.ExportarPlano("\t", Sesion, MuestraBorrado());
        if (Exportacion.Length > 0)
        {
            Response.AddHeader("Content-disposition", "attachment; filename=" + Tabla.m_Tabla + ".txt");
            Response.ContentType = "application/octet-stream";
            Response.Write(Exportacion);
            Response.End();
        }
    }
    protected void ExportarCSV()
    {
        CeC_TablaBD Tabla = new CeC_TablaBD(new CeC_Tablas(Sesion), Sesion);
        string Exportacion = Tabla.ExportarPlano(",", Sesion, MuestraBorrado());
        if (Exportacion.Length > 0)
        {
            Response.AddHeader("Content-disposition", "attachment; filename=" + Tabla.m_Tabla + ".csv");
            Response.ContentType = "application/octet-stream";
            Response.Write(Exportacion);
            Response.End();
        }
    }
    protected void Borrar(string Registros)
    {
        string[] sRegistros = CeC.ObtenArregoSeparador(Registros, "~");
        if (sRegistros.Length > 1)
        {
            CeC_TablaBD Tabla = new CeC_TablaBD(new CeC_Tablas(Sesion), Sesion);
            string tmp;
            foreach (string sRegistro in sRegistros)
            {
                string[] IDS = CeC.ObtenArregoSeparador(sRegistro, "|");
                Tabla.Borrar(IDS, Sesion);
            }
        }
    }

    protected void Menu_ItemClick(object sender, Infragistics.Web.UI.NavigationControls.DataMenuItemEventArgs e)
    {
        string Val = e.Item.Value;
    }
    protected void Grid_RowSelectionChanged(object sender, SelectedRowEventArgs e)
    {
        return;
        //Validar cuando sean ID string
        IList<int> ids = new List<int>();
        int id = 0;
        int index = 0;

        foreach (GridRecord row in e.CurrentSelectedRows)
        {
            if (row == null)
            {
                IDPair pair = e.CurrentSelectedRows.GetIDPair(index);
                id = Convert.ToInt32(pair.Key[0]);
            }
            else
            {
                id = Convert.ToInt32(row.DataKey[0]);
            }

            if (!ids.Contains(id))
            {
                ids.Add(id);
            }

            index++;
        }

        //TODO: use the selected IDs to update the database, call a
        // service, or any other operation
        System.Diagnostics.Debug.WriteLine(ids.Count);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void WebImageButton2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

    }
}
