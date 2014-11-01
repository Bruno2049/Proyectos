namespace PAEEEM.Reportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for ReportPrueba.
    /// </summary>
    public partial class ReportPrueba : Telerik.Reporting.Report
    {
        public ReportPrueba()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public static string GeneraEncabezado(string usuario)
        {
            var fechaHoy = DateTime.Now.ToLongDateString();

            var encabezado = usuario + " - " + fechaHoy;

            return encabezado;
        }
    }
}