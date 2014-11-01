namespace PAEEEM.Reportes
{
    partial class ReportPrueba
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.TxtNombreReporte = new Telerik.Reporting.TextBox();
            this.TxtEncabezadoUsuario = new Telerik.Reporting.TextBox();
            this.TblDatosDistribuidor = new Telerik.Reporting.Table();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TxtNombreReporte,
            this.TxtEncabezadoUsuario});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(6.9999995231628418D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TblDatosDistribuidor});
            this.detail.Name = "detail";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2000001668930054D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.4000000953674316D);
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // TxtNombreReporte
            // 
            this.TxtNombreReporte.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.TxtNombreReporte.Name = "TxtNombreReporte";
            this.TxtNombreReporte.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.59999990463256836D));
            this.TxtNombreReporte.Value = "Reporte Distribuidores";
            // 
            // TxtEncabezadoUsuario
            // 
            this.TxtEncabezadoUsuario.Bindings.Add(new Telerik.Reporting.Binding("Value", "= GeneraEncabezado(Parameters.Usuario)"));
            this.TxtEncabezadoUsuario.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.199999809265137D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.TxtEncabezadoUsuario.Name = "TxtEncabezadoUsuario";
            this.TxtEncabezadoUsuario.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.800000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0.59999990463256836D));
            this.TxtEncabezadoUsuario.Value = "textBox1";
            // 
            // TblDatosDistribuidor
            // 
            this.TblDatosDistribuidor.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D)));
            this.TblDatosDistribuidor.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D)));
            this.TblDatosDistribuidor.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D)));
            this.TblDatosDistribuidor.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(0.54999995231628418D)));
            this.TblDatosDistribuidor.Body.SetCellContent(0, 0, this.textBox4);
            this.TblDatosDistribuidor.Body.SetCellContent(0, 1, this.textBox5);
            this.TblDatosDistribuidor.Body.SetCellContent(0, 2, this.textBox6);
            tableGroup1.ReportItem = this.textBox1;
            tableGroup2.ReportItem = this.textBox2;
            tableGroup3.ReportItem = this.textBox3;
            this.TblDatosDistribuidor.ColumnGroups.Add(tableGroup1);
            this.TblDatosDistribuidor.ColumnGroups.Add(tableGroup2);
            this.TblDatosDistribuidor.ColumnGroups.Add(tableGroup3);
            this.TblDatosDistribuidor.ColumnHeadersPrintOnEveryPage = true;
            this.TblDatosDistribuidor.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5,
            this.textBox6});
            this.TblDatosDistribuidor.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.TblDatosDistribuidor.Name = "TblDatosDistribuidor";
            tableGroup4.Groupings.AddRange(new Telerik.Reporting.Data.Grouping[] {
            new Telerik.Reporting.Data.Grouping(null)});
            tableGroup4.Name = "DetailGroup";
            this.TblDatosDistribuidor.RowGroups.Add(tableGroup4);
            this.TblDatosDistribuidor.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(24.30000114440918D), Telerik.Reporting.Drawing.Unit.Cm(1.0999999046325684D));
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.54999995231628418D));
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.54999995231628418D));
            // 
            // textBox3
            // 
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.54999995231628418D));
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.54999995231628418D));
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.54999995231628418D));
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.54999995231628418D));
            // 
            // ReportPrueba
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1,
            this.reportHeaderSection1});
            this.Name = "ReportPrueba";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(25.200000762939453D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox TxtNombreReporte;
        private Telerik.Reporting.TextBox TxtEncabezadoUsuario;
        private Telerik.Reporting.Table TblDatosDistribuidor;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
    }
}