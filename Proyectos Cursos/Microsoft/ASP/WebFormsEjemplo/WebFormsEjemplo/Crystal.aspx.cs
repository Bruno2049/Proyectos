using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsEjemplo.LDN;

namespace WebFormsEjemplo
{
    public partial class Crystal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Conexion Conector = new Conexion();
            Reporte RPT = new Reporte();
            RPT.SetDataSource(Conector.Datos("SELECT * FROM PROYECTO"));
            this.CrystalReportViewer1.ReportSource = RPT;
            this.CrystalReportViewer1.Enabled = true;
        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            
        }
    }
}