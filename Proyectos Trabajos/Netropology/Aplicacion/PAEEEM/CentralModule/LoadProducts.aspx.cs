using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.AccesoDatos.CargaMasivaProductos;
using PAEEEM.DisposalModule;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.ModuloCentral;


namespace PAEEEM.CentralModule
{
    public partial class LoadProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            CatTecnologia();

        }

        private void CatTecnologia()
        {
            cboTecnologias.DataSource = new CargaMasivaProductos().ListaTecnologias();
            cboTecnologias.DataTextField = "Dx_nombre_general";
            cboTecnologias.DataValueField = "Cve_Tecnologia";
            cboTecnologias.DataBind();
        }

        protected void cboTecnologias_SelectedIndexChanged(object sender,Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubEstacion.SelectedValue = "0";
            var itemSelec = cboTecnologias.SelectedValue;
            if (itemSelec.Equals("5"))
            {
                lbSubEstatciones.Visible = true;
                cboSubEstacion.Visible = true;
                dvPanel.Style.Add("display", "none");
            }
            else
            {
                lbSubEstatciones.Visible = false;
                cboSubEstacion.Visible = false;
                if (itemSelec.Equals("0")) dvPanel.Style.Add("display", "none");
                else dvPanel.Style.Add("display", "inline");
            }

            dvResumenCarga.Style.Add("display", "none");
            DivGrid.Style.Add("display", "none");
            
        }

        protected void cboSubEstacion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var itemSelec = cboSubEstacion.SelectedValue;
            if (itemSelec.Equals("0")) dvPanel.Style.Add("display", "none");
            else dvPanel.Style.Add("display", "inline");

            dvResumenCarga.Style.Add("display", "none");
            DivGrid.Style.Add("display", "none");
        }
        
        protected void CargaArchivoFileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                if (e.File.FileName != null)
                {
                    string nombreArchivo = e.File.FileName;
                    var strExtension = e.File.GetExtension();
                    if (strExtension == ".xls" || strExtension == ".xlsx")
                    {
                        string rutatmp = ConfigurationManager.AppSettings["CarpetaCargaProductosExcel"] + nombreArchivo;
                        e.File.SaveAs(rutatmp);

                        var itemSelec = cboTecnologias.SelectedValue;
                        int idTecnologiaSelect = int.Parse(itemSelec);

                        if (idTecnologiaSelect == 1 || idTecnologiaSelect == 2 || idTecnologiaSelect == 3 ||
                            idTecnologiaSelect == 4 || idTecnologiaSelect == 6 || idTecnologiaSelect == 8 ||
                            idTecnologiaSelect == 9)
                        {
                            var lstRows = new AppExcel(rutatmp).ReadExcel();
                            ProcesaRegistros(nombreArchivo, idTecnologiaSelect, lstRows);
                        }
                        else if (idTecnologiaSelect == 5)
                        {
                            var subEstacion = cboSubEstacion.SelectedValue;
                            switch (subEstacion)
                            {
                                case "1" :
                                    var lstRows = new AppExcel(rutatmp).ReadExcelSubEstacionAerea();
                                    ProcesaRegistros(nombreArchivo, idTecnologiaSelect, lstRows);
                                break;
                                case "2":
                                    var lstRowsAcometida = new AppExcel(rutatmp).ReadExcelSubEstacionConAcometida();
                                    ProcesaRegistros(nombreArchivo, idTecnologiaSelect, lstRowsAcometida);
                                break;
                                case "3":
                                    var lstRowsIntRed = new AppExcel(rutatmp).ReadExcelSubEstacionIntRed();
                                    ProcesaRegistros(nombreArchivo, idTecnologiaSelect, lstRowsIntRed);
                                break;
                            }
                        }
                        else if (idTecnologiaSelect == 7)
                        {
                            var lstRows = new AppExcel(rutatmp).ReadExcelBancoCapacitores();
                            ProcesaRegistros(nombreArchivo, idTecnologiaSelect, lstRows);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "NextError",
                    string.Format("alert('{0}');", ex.Message), true);
            }
        }

        private void ProcesaRegistros(string fileName, int idTecnologiaSelect, List<LayOutExcel> lstRows)
        {
           
            var LogHeader = new Load_LogHeader();
            LogHeader.NombreArchivo = fileName;
            LogHeader.FechaCarga = DateTime.Now;
            LogHeader.Usr = ((US_USUARIOModel) Session["UserInfo"]).Nombre_Usuario;
            LogHeader.Estatus = true;
            LogHeader.NoRegistros = lstRows.Count;
            int idLogHeader = new CargaMasivaProductos().InsertLogHeader(LogHeader);
            hidIdLogHeader.Value = idLogHeader.ToString(CultureInfo.InvariantCulture);
            int RegErro = 0, RegBuenos = 0;

            var bll = new ProcesaExcel(idTecnologiaSelect);

            if (idTecnologiaSelect == 1 || idTecnologiaSelect == 2 || idTecnologiaSelect == 3 ||
                idTecnologiaSelect == 4 || idTecnologiaSelect == 6 || idTecnologiaSelect == 8 || idTecnologiaSelect == 9)
            {

                foreach (var row in lstRows)
                {

                    var nuevoProducto = bll.ValidaRegistro(row, idTecnologiaSelect.ToString(CultureInfo.InvariantCulture), idLogHeader);
                    if (nuevoProducto != null)
                    {
                        new CargaMasivaProductos().InsertarProducto(nuevoProducto);
                        RegBuenos += 1;
                    }
                    else
                    {
                        RegErro += 1;
                    }
                }
                
            }
            else if (idTecnologiaSelect == 5)
            {
                var subEstacion = cboSubEstacion.SelectedValue;
                switch (subEstacion)
                {
                    case "1":
                        foreach (var row in lstRows)
                        {
                            var nuevoProducto = bll.ValidaRegistroSubEstacionAerea(row, idTecnologiaSelect.ToString(CultureInfo.InvariantCulture), idLogHeader);
                            var nuevoProductoSE = bll.ValidaRegistroSubEstacionAerea_SE(row, idLogHeader);
                            if (nuevoProducto != null && nuevoProductoSE != null)
                            {
                                int idProd = new CargaMasivaProductos().InsertarProductoReturnID(nuevoProducto);
                                if (idProd > 0)
                                {
                                    nuevoProductoSE.Cve_Producto = idProd;
                                    new CargaMasivaProductos().InsertaModuloSE(nuevoProductoSE);
                                    RegBuenos += 1;
                                }
                            }
                            else
                            {
                                RegErro += 1;
                            }
                        }
                        break;
                    case "2":
                        foreach (var row in lstRows)
                        {
                            var nuevoProducto = bll.ValidaRegistroSubEstacionAcometida(row, idTecnologiaSelect.ToString(CultureInfo.InvariantCulture), idLogHeader);
                            var nuevoProductoSE = bll.ValidaRegistroSubEstacionAcometida_SE(row, idLogHeader);
                            if (nuevoProducto != null && nuevoProductoSE != null)
                            {
                                int idProd = new CargaMasivaProductos().InsertarProductoReturnID(nuevoProducto);
                                if (idProd > 0)
                                {
                                    nuevoProductoSE.Cve_Producto = idProd;
                                    new CargaMasivaProductos().InsertaModuloSE(nuevoProductoSE);
                                    RegBuenos += 1;
                                }
                            }
                            else
                            {
                                RegErro += 1;
                            }
                        }
                        break;
                    case "3":
                        foreach (var row in lstRows)
                        {
                            var nuevoProducto = bll.ValidaRegistroSubEstacionIntRed(row, idTecnologiaSelect.ToString(CultureInfo.InvariantCulture), idLogHeader);
                            var nuevoProductoSE = bll.ValidaRegistroSubEstacionIntRed_SE(row, idLogHeader);
                            if (nuevoProducto != null && nuevoProductoSE != null)
                            {
                                int idProd = new CargaMasivaProductos().InsertarProductoReturnID(nuevoProducto);
                                if (idProd > 0)
                                {
                                    nuevoProductoSE.Cve_Producto = idProd;
                                    new CargaMasivaProductos().InsertaModuloSE(nuevoProductoSE);
                                    RegBuenos += 1;
                                }
                            }
                            else
                            {
                                RegErro += 1;
                            }
                        }
                   break;
                }
            }
            else if (idTecnologiaSelect == 7)
            {
                foreach (var row in lstRows)
                {

                    var nuevoProducto = bll.ValidaRegistroBancoCapacitores(row, idTecnologiaSelect.ToString(CultureInfo.InvariantCulture), idLogHeader);
                    if (nuevoProducto != null)
                    {
                        new CargaMasivaProductos().InsertarProducto(nuevoProducto);
                        RegBuenos += 1;
                    }
                    else
                    {
                        RegErro += 1;
                    }
                }
            }

            LogHeader.NoRegSinError = RegBuenos;
            LogHeader.NoRegConError = RegErro;
            new CargaMasivaProductos().ActualizaLogHeader(LogHeader);

            dvResumenCarga.Style.Add("display","inline");
            lbTotalrows.Text = string.Format("Total de registros procesados:{0}",lstRows.Count);
            lbrowsOk.Text = string.Format("Registros cargados correctamente:{0}", RegBuenos);
            lbrowsKo.Text = string.Format("Registros con error:{0}",RegErro);


            if (RegErro > 0)
            {
                DivGrid.Style.Add("display", "inline");
                gdLogErrores.DataSource = new CargaMasivaProductos().ObtenerDetailsErroresCarga(LogHeader.LogHeaderId);
                gdLogErrores.DataBind();

            }
            else
            {
                DivGrid.Style.Add("display", "none");
            }
        }

        protected void btnDescargar_Click(object sender, ImageClickEventArgs e)
        {

            try
            {


                int idTecnologia = int.Parse(cboTecnologias.SelectedValue);
                int? idClase = null;
                if (idTecnologia == 5) idClase = int.Parse(cboSubEstacion.SelectedValue);

                string filename =
                    new CargaMasivaProductos().ObtenerNombreArchivoLayOuts(idTecnologia, idClase).NombreLayOut;

                if (!String.IsNullOrEmpty(filename))
                {
                    string dlDir = ConfigurationManager.AppSettings["RepositorioLayOutExcel"];
                    string path = dlDir + filename;

                    System.IO.FileInfo toDownload = new System.IO.FileInfo(path);

                    if (toDownload.Exists)
                    {
                        Response.Clear();
                        Response.AddHeader("Content-Disposition",
                            "attachment; filename=" + toDownload.Name);
                        Response.AddHeader("Content-Length", toDownload.Length.ToString());
                        Response.ContentType = "application/octet-stream";
                        Response.WriteFile(dlDir + filename);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                    string.Format("alert('{0}');", ex.Message), true);
            }
        }

        private void CargaDatosGrid()
        {
            if (!string.IsNullOrEmpty(hidIdLogHeader.Value))
            {
                int idlogHeader = int.Parse(hidIdLogHeader.Value);
                gdLogErrores.DataSource = new CargaMasivaProductos().ObtenerDetailsErroresCarga(idlogHeader);
            }
        }

        protected void gdLogErrores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            CargaDatosGrid();
        }


    }
}