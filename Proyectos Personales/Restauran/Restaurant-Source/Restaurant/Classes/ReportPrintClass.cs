using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.Reporting.WinForms;
using System.Data;

namespace Restaurant.Classes
{
    public class ReportPrintClass
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        double leftMargin = 0;
        double rightMargin = 0;
        double topMargin = 0;
        double bottomMargin = 0;
        double pageWidth = 8.27;
        double pageHeight = 11.69;

        public ReportPrintClass(string _DataSourceName, DataTable _DataSourceValue, string _ReportPath,double pagewidth,double pageheight , double leftmargin, double rightmargin, double topmargin, double bottommargin)
        {
            this.pageWidth = pagewidth;
            this.pageHeight = pageheight;
            this.leftMargin = leftmargin;
            this.rightMargin = rightmargin;
            this.topMargin = topmargin;
            this.bottomMargin = bottommargin;
            LocalReport report = new LocalReport();
            report.ReportPath = _ReportPath;
            report.DataSources.Add(new ReportDataSource(_DataSourceName, _DataSourceValue));
            Export(report);
            m_currentPageIndex = 0;
        }

        public ReportPrintClass(string _DataSourceName, DataTable _DataSourceValue, string _ReportPath, ReportParameter[] arrParams,double pagewidth,double pageheight, double leftmargin, double rightmargin, double topmargin, double bottommargin)
        {
            this.pageWidth = pagewidth;
            this.pageHeight = pageheight;
            this.leftMargin = leftmargin;
            this.rightMargin = rightmargin;
            this.topMargin = topmargin;
            this.bottomMargin = bottommargin;
            LocalReport report = new LocalReport();
            report.ReportPath = _ReportPath;

            report.DataSources.Add(new ReportDataSource(_DataSourceName, _DataSourceValue));
            report.SetParameters(arrParams);
            Export(report);
            m_currentPageIndex = 0;
        }

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new FileStream(name +
               "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>" + pageWidth + "in</PageWidth>" +
              "  <PageHeight>" + pageHeight + "in</PageHeight>" +
              "  <MarginTop>"+topMargin+"in</MarginTop>" +
              "  <MarginLeft>" + leftMargin + "in</MarginLeft>" +
              "  <MarginRight>" + rightMargin + "in</MarginRight>" +
              "  <MarginBottom>" + bottomMargin + "in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            try
            {
                report.Render("Image", deviceInfo, CreateStream, out warnings);
            }
            catch (LocalProcessingException ex)
            {
            }
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void Print(string printerName, Int16 noCopy)
        {
            if (m_streams == null || m_streams.Count == 0)
                return;
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.Copies = noCopy;
            printDoc.PrinterSettings.PrinterName = printerName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format(
                   "Can't find printer \"{0}\".", printerName);
                MessageBox.Show(msg, "Print Error");
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            try
            {
                printDoc.Print();
            }
            catch (Exception ex)
            {

            }
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

    }
}
