
using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.LogicaNegocios.ModuloCentral;
using PAEEEM.DataAccessLayer.CentralModule;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using System.Configuration;
//using Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace PAEEEM.CentralModule
{
    public partial class LoadQuery : System.Web.UI.Page
    {
        int pageCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            limpiar();

        }

        private void cargaConsultas()
        {
            var varConsultas = new cmConsultas();
            grdQuery.DataSource = varConsultas.obtieneConsultas();
            //AspNetPager.RecordCount = pageCount;
            grdQuery.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void limpiar()
        {
            txtConsulta.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtQuery.Text = string.Empty;
            rdpFecCrea.SelectedDate = DateTime.Now;
            Session["Accion"] = null;
            Session["CreadoPor"] = null;
            enabled(false);
            cargaConsultas();

        }
        protected void enabled(bool Enabled)
        {
            txtConsulta.Enabled = Enabled;
            txtDescripcion.Enabled = Enabled;
            txtQuery.Enabled = Enabled;
            rdpFecCrea.Enabled = Enabled;
            if (Enabled) txtConsulta.Focus();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string sAccion = string.Empty;
            if (Session["Accion"] != null)
            {
                var objQuery = new PAEEEM.Entidades.CRE_Consultas();
                var objSalida = new cmConsultas();
                sAccion = (string)Session["Accion"];
                objQuery.Validado = 1;
                objQuery.Estatus = 1;
                switch (sAccion)
                {
                    case "Edit":
                        if (!validaDato())
                        {
                            RadWindowManager1.RadAlert("Es necesaria la información: Nombre de la consulta, descripción y consulta", 300, 150, "Guardar", null);
                        }
                        else
                        {
                            if (!validaConsulta(txtQuery.Text.ToString()))
                            {
                                RadWindowManager1.RadAlert("La consulta tiene un formato incorrecto o no existen resultados", 300, 150, "Guardar", null);
                            }
                            else
                            {
                                objQuery.CVE_Consulta = Convert.ToInt32(txtCVE.Text.ToString());
                                objQuery.Nombre_Consulta = txtConsulta.Text.ToString();
                                objQuery.Descripcion_Consulta = txtDescripcion.Text.ToString();
                                objQuery.Consulta = txtQuery.Text.ToString();
                                objQuery.Fecha_Adicion = rdpFecCrea.SelectedDate.Value;
                                objQuery.Adicionado_Por = (string)Session["CreadoPor"];
                                objSalida.ActualizaConsultas(objQuery);
                                limpiar();
                            }
                        }
                        break;
                    case "New":
                        if (!validaDato())
                        {
                            RadWindowManager1.RadAlert("Es necesaria la información: Nombre de la consulta, descripción y consulta", 300, 150, "Guardar", null);
                        }
                        else
                        {
                            if (!validaConsulta(txtQuery.Text.ToString()))
                            {
                                RadWindowManager1.RadAlert("La consulta tiene un formato incorrecto o no existen resultados", 300, 150, "Guardar", null);
                            }
                            else
                            {
                                objQuery.Nombre_Consulta = txtConsulta.Text.ToString();
                                objQuery.Descripcion_Consulta = txtDescripcion.Text.ToString();
                                objQuery.Consulta = txtQuery.Text.ToString();
                                objQuery.Fecha_Adicion = rdpFecCrea.SelectedDate.Value;
                                objQuery.Adicionado_Por = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;
                                objSalida.InsertaConsultas(objQuery);
                                limpiar();
                            }
                        }
                        break;
                }
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            limpiar();
            enabled(true);
            Session.Add("Accion", "New");
        }
        protected bool validaDato()
        {

            bool bSalida = true;
            bSalida = txtConsulta.Text == string.Empty ? false : true;
            bSalida = txtDescripcion.Text == string.Empty ? false : bSalida == false ? false : true;
            bSalida = txtQuery.Text == string.Empty ? false : bSalida == false ? false : true;
            return bSalida;

        }

        protected void grdQuery_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = -1;
            var objQuery = new PAEEEM.Entidades.CRE_Consultas();
            var objSalida = new cmConsultas();
            index = Convert.ToInt32(e.CommandArgument.ToString());
            System.Web.UI.WebControls.Label sCVE = (System.Web.UI.WebControls.Label)grdQuery.Rows[index].FindControl("lblgCVE");
            System.Web.UI.WebControls.Label sConsulta = (System.Web.UI.WebControls.Label)grdQuery.Rows[index].FindControl("lblConsulta");
            System.Web.UI.WebControls.Label sDescripcion = (System.Web.UI.WebControls.Label)grdQuery.Rows[index].FindControl("lblDescripcion");
            System.Web.UI.WebControls.Label sQuery = (System.Web.UI.WebControls.Label)grdQuery.Rows[index].FindControl("lblQuery");
            System.Web.UI.WebControls.Label sCreacion = (System.Web.UI.WebControls.Label)grdQuery.Rows[index].FindControl("lblCreacion");
            txtCVE.Text = sCVE.Text.ToString();
            txtConsulta.Text = sConsulta.Text.ToString();
            txtDescripcion.Text = sDescripcion.Text.ToString();
            txtQuery.Text = sQuery.Text.ToString();
            rdpFecCrea.SelectedDate = Convert.ToDateTime(sCreacion.Text.ToString());
            switch (e.CommandName)
            {
                case "Edit":
                    Session.Add("Accion", e.CommandName);
                    Session.Add("CreadoPor", sCreacion.Text.ToString());
                    enabled(true);
                    break;
                case "Borrar":
                    objQuery.CVE_Consulta = Convert.ToInt32(sCVE.Text.ToString());
                    objQuery.Nombre_Consulta = sConsulta.Text.ToString();
                    objQuery.Descripcion_Consulta = sDescripcion.Text.ToString();
                    objQuery.Consulta = sQuery.Text.ToString();
                    objQuery.Fecha_Adicion = Convert.ToDateTime(sCreacion.Text.ToString());
                    objSalida.EliminaConsultas(objQuery);
                    limpiar();
                    break;
                case "DownLoad":
                    descargas(sQuery.Text.ToString(), sConsulta.Text.ToString());
                    break;
            }
        }

        protected void grdQuery_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected bool validaConsulta(string sQuery)
        {
            try
            {
                var varConsultas = new PAEEEM.DataAccessLayer.CentralModule.CRE_Cosnsultas();
                System.Data.DataTable dt = varConsultas.Get_Data(sQuery);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            //return true;
        }
        protected void descargas(string sQuery, string sDocument)
        {
            try
            {

                var varConsultas = new PAEEEM.DataAccessLayer.CentralModule.CRE_Cosnsultas();
                System.Data.DataTable dt = varConsultas.Get_Data(sQuery);

                if (dt.Rows.Count > 0)
                {

                    //dt = city.GetAllCity();//your datatable
                    string attachment = "attachment; filename=" + sDocument + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.ms-excel";
                    string tab = "";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        Response.Write(tab + dc.ColumnName);
                        tab = "\t";
                    }
                    Response.Write("\n");
                    int i;
                    foreach (DataRow dr in dt.Rows)
                    {
                        tab = "";
                        for (i = 0; i < dt.Columns.Count; i++)
                        {
                            Response.Write(tab + dr[i].ToString());
                            tab = "\t";
                        }
                        Response.Write("\n");
                    }
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "NextError",
                    string.Format("alert('{0}');", ex.Message.ToString().Replace("'", "")), true);
            }
        }
        //protected void descargas(string sQuery, string sDocument)
        //{
        //    try
        //    {



        //        var varConsultas = new PAEEEM.DataAccessLayer.CentralModule.CRE_Cosnsultas();
        //        System.Data.DataTable dtSalida = varConsultas.Get_Data(sQuery);

        //        if (dtSalida.Rows.Count > 0)
        //        {
        //            object misValue = System.Reflection.Missing.Value;
        //            Microsoft.Office.Interop.Excel.Application _excelApp;
        //            Microsoft.Office.Interop.Excel.Workbook _myworkbook;
        //            Microsoft.Office.Interop.Excel.Worksheet _myWorkSheet;
        //            //nueva aplicación excel.
        //            _excelApp = new Microsoft.Office.Interop.Excel.Application();
        //            if (_excelApp == null)
        //            {

        //                ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "NextError",
        //                string.Format("alert('{0}');", "EXCEL could not be started. Check that your office installation and project references are correct."), true);
        //                return;
        //            }
        //            _excelApp.Visible = false;

        //            //añadimos un workbook
        //            _myworkbook = _excelApp.Workbooks.Add();

        //            int worksheetIndex = 1;

        //            ////una worksheet para cada tabla nueva.
        //            //myworkbook.Worksheets.Add(misValue, excelApp.ActiveWorkbook.Worksheets[excelApp.ActiveWorkbook.Worksheets.Count], worksheetIndex, misValue);

        //            //selecciono la última worksheet creada para escribir ahí los datos
        //            _myWorkSheet = (Worksheet)_myworkbook.Worksheets.get_Item(worksheetIndex);

        //            int rowIndex = 1;
        //            foreach (DataRow row in dtSalida.Rows)
        //            {
        //                int columnIndex = 1;

        //                foreach (object dc in row.ItemArray)
        //                {
        //                    _myWorkSheet.Cells[rowIndex, columnIndex] = dc.ToString();
        //                    columnIndex++;
        //                }
        //                rowIndex++;
        //            }
        //            worksheetIndex++;

        //            _myworkbook.Close(true, misValue, misValue);
        //            _excelApp.Quit();


        //            //if (!String.IsNullOrEmpty(sDocument))
        //            //{
        //            //    string dlDir = ConfigurationManager.AppSettings["RepositorioLayOutExcel"];
        //            //    string path = dlDir + sDocument;

        //            //    System.IO.FileInfo toDownload = new System.IO.FileInfo(path);

        //            //    if (toDownload.Exists)
        //            //    {
        //            //        Response.Clear();
        //            //        Response.AddHeader("Content-Disposition",
        //            //            "attachment; filename=" + toDownload.Name);
        //            //        Response.AddHeader("Content-Length", toDownload.Length.ToString());
        //            //        Response.ContentType = "application/octet-stream";
        //            //        Response.WriteFile(dlDir + sDocument);
        //            //        Response.End();
        //            //    }
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "NextError",
        //            string.Format("alert('{0}');", ex.Message.ToString().Replace("'","")), true);
        //    }
        //}
    }
}