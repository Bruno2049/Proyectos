using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Infragistics.Documents.Reports.Report;
using Infragistics.WebUI.UltraWebGrid.ExcelExport;
using ReportText = Infragistics.Documents.Reports.Report.Text;
using Infragistics.Documents.Reports.Report.Grid;
using System.Drawing;

/// <summary>
/// Descripción breve de CeC_Reportes
/// </summary>
public class CeC_Reportes
{
    public const string lbl_DetalleReporte = "lbl_DetalleReporte";

    public enum REPORTE
    {
        AsistenciasCC,
        AsistenciaDiaria,
        AsistenciaMens,
        AsistenciaMensCC,
        AsistenciaSicoss,
        GraficasGrupo1,
        GraficasGrupo2,
        GraficasGrupo3,
        GraficosPersona
    };
    public CeC_Reportes()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static string ImageneClock64
    {
        get { return "imagenes/eclock64.png"; }

    }
    static public void AplicaFormatoReporte(Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e, string Titulo,string Descripcion)
    {
        AplicaFormatoReporte(e, Titulo,Descripcion, ImageneClock64);
    }
    static public void AplicaFormatoReporte(Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e, string Titulo, string Descripcion, CeC_Sesion Sesion)
    {
        AplicaFormatoReporte(e, Titulo, Descripcion, ImageneClock64, Sesion);
    }
    static public void AplicaFormatoReporte(Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e, string Titulo, string Descripcion, string Imagen)
    {
        AplicaFormatoReporte(e, Titulo,Descripcion, Imagen, null);
    }
    /// <summary>
    /// Agrega la cabecera con el titulo del reporte y el pie de pagina
    /// </summary>
    /// <param name="e"></param>
    /// <param name="Titulo"></param>
    /// <param name="Imagen">Nombre de la imagen que se usará como logo, en caso de no existir la creará usando la imagen predeterminada</param>
    /// <param name="Sesion"></param>
    static public void AplicaFormatoReporte(Infragistics.WebUI.UltraWebGrid.DocumentExport.DocumentExportEventArgs e, string Titulo, string Descripcion, string Imagen, CeC_Sesion Sesion)
    {
        try
        {
            bool MostrarCompania = false;
            string Compania = "";
            if (Sesion != null && Sesion.ConfiguraSuscripcion.CompaniaMuestraEnReportes)
            {
                Compania = Sesion.ConfiguraSuscripcion.CompaniaNombre;
                if (Compania != "")
                {
                    MostrarCompania = true;
                    Titulo += "\n" + Compania;
                }
            }
            string ArchivoImagen = HttpRuntime.AppDomainAppPath + Imagen;
            if (!System.IO.File.Exists(ArchivoImagen))
            {
                System.IO.File.Copy(HttpRuntime.AppDomainAppPath + ImageneClock64, ArchivoImagen);
            }
            Infragistics.Documents.Reports.Report.Section.ISection Seccion = e.Section;
            /// Cabecera del reporte
            Infragistics.Documents.Reports.Report.Section.ISectionHeader Cabecera = Seccion.AddHeader();
            Infragistics.Documents.Reports.Graphics.Image imgLogo = new Infragistics.Documents.Reports.Graphics.Image(ArchivoImagen);
            Infragistics.Documents.Reports.Report.IImage Logo = Cabecera.AddImage(imgLogo, 1, 1);
            ReportText.IText heading = Cabecera.AddText(60, 10);
            ReportText.Style headingStyle = new ReportText.Style(new Infragistics.Documents.Reports.Graphics.Font("Tahoma", 18), Infragistics.Documents.Reports.Graphics.Brushes.DarkSlateBlue);
            heading.AddContent(Titulo, headingStyle);
            ReportText.IText headingDescripcion = Cabecera.AddText(60, 35);
            ReportText.Style headingStyleDescripcion = new ReportText.Style(new Infragistics.Documents.Reports.Graphics.Font("Tahoma", 12), Infragistics.Documents.Reports.Graphics.Brushes.LightSteelBlue);
            headingDescripcion.AddContent(Descripcion, headingStyleDescripcion);

            ReportText.IText caption2 = Cabecera.AddText(1, 1);
            ReportText.Style captionStyle2 = new ReportText.Style(new Infragistics.Documents.Reports.Graphics.Font("Tahoma", 10), Infragistics.Documents.Reports.Graphics.Brushes.LightSteelBlue);
            caption2.AddContent("Fecha de Impresión: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), captionStyle2);
            caption2.Alignment.Horizontal = Alignment.Right;

            //caption.Alignment.Horizontal = Alignment.Center;
            caption2.Margins.Bottom = 15;
            heading.Margins.Bottom = 15;
            Cabecera.Repeat = true;
            
            if (MostrarCompania)
                Cabecera.Height = 65;
            else
                Cabecera.Height = 50;
            Infragistics.Documents.Reports.Report.Section.PageNumbering PPaginas = Seccion.PageNumbering;
            PPaginas.Style = new Infragistics.Documents.Reports.Report.Text.Style(new Infragistics.Documents.Reports.Graphics.Font("Tahoma", 8), Infragistics.Documents.Reports.Graphics.Brushes.LightSteelBlue);
            PPaginas.Template = "Página [Page #] de [TotalPages]";
            PPaginas.SkipFirst = false;
            PPaginas.Alignment.Horizontal = Infragistics.Documents.Reports.Report.Alignment.Right;
            PPaginas.Alignment.Vertical = Infragistics.Documents.Reports.Report.Alignment.Bottom;
            PPaginas.OffsetY = -18;

            Infragistics.Documents.Reports.Report.Section.ISectionFooter sectionFooter =
            Seccion.AddFooter();// sec.AddFooter();
            sectionFooter.Repeat = true;
            sectionFooter.Height = 50;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }
}

