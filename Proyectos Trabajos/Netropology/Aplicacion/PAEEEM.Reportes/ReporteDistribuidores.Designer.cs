namespace PAEEEM.Reportes
{
    partial class ReporteDistribuidores
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.TxtNombreReporte = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TxtNombreReporte,
            this.textBox1});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(8D);
            this.detail.Name = "detail";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(3D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D);
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // TxtNombreReporte
            // 
            this.TxtNombreReporte.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.20000000298023224D), Telerik.Reporting.Drawing.Unit.Cm(0.20000000298023224D));
            this.TxtNombreReporte.Name = "TxtNombreReporte";
            this.TxtNombreReporte.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.4000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.TxtNombreReporte.Value = "Reporte Distribuidores";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.5D), Telerik.Reporting.Drawing.Unit.Cm(0.20000000298023224D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.300000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0.59999990463256836D));
            this.textBox1.Value = "textBox1";
            // 
            // ReporteDistribuidores
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1,
            this.reportHeaderSection1});
            this.Name = "ReporteDistribuidores";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(25D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox TxtNombreReporte;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection1;
    }
}