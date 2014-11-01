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
    public partial class downQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            limpiar();
        }

        private void limpiar()
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
        private void cargaConsultas()
        {
            var varConsultas = new cmConsultas();
            grdQuery.DataSource = varConsultas.obtieneConsultas();
            grdQuery.DataBind();
        }
        protected void enabled(bool Enabled)
        {
            txtConsulta.Enabled = Enabled;
            txtDescripcion.Enabled = Enabled;
            txtQuery.Enabled = Enabled;
            rdpFecCrea.Enabled = Enabled;
            if (Enabled) txtConsulta.Focus();
        }
        protected void grdQuery_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = -1;
            var objQuery = new PAEEEM.Entidades.CRE_Consultas();
            var objSalida = new cmConsultas();
            index = Convert.ToInt32(e.CommandArgument.ToString());
            System.Web.UI.WebControls.Label sCVE = (System.Web.UI.WebControls.Label)grdQuery.Rows[index].FindControl("lblgCVE");
            System.Web.UI.WebControls.Label sConsulta = (System.Web.UI.WebControls.Label)grdQuery.Rows[index].FindControl("lblNameQ");
            System.Web.UI.WebControls.Label sDescripcion = (System.Web.UI.WebControls.Label)grdQuery.Rows[index].FindControl("lblDescripcionQ");
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
                    enabled(false);
                    break;                
                case "DownLoad":
                    descargas(sQuery.Text.ToString(), sConsulta.Text.ToString());
                    break;
            }
        }

        protected void grdQuery_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }


        protected void descargas(string sQuery, string sDocument)
        {
            try
            {

                var varConsultas = new PAEEEM.DataAccessLayer.CentralModule.CRE_Cosnsultas();
                System.Data.DataTable dt = varConsultas.Get_Data(sQuery);

                if (dt.Rows.Count > 0)
                {

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
    }
}