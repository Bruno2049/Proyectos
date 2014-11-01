using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
namespace eClockReports
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Reportes.Asistencia.Lineal Reporte = new Reportes.Asistencia.Lineal();
            //Reportes.CrystalReport3 Reporte = new Reportes.CrystalReport3();
            List<eClockBase.Modelos.Asistencias.Reporte_Asistencias> Datos = new List<eClockBase.Modelos.Asistencias.Reporte_Asistencias>();
            Reporte.SetDataSource(Datos);
            Reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Prueba.pdf");
//            CrystalReportViewer1.ReportSource = Reporte;

        }
    }
}