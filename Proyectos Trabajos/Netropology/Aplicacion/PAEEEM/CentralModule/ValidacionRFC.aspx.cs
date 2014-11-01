using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PAEEEM.LogicaNegocios;
using PAEEEM.LogicaNegocios.Validacion_RFC_L;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Validacion_RFC_E;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using Telerik.Web.UI;
using System.Text;

namespace PAEEEM.CentralModule
{
    public partial class ValidacionRFC : System.Web.UI.Page
    {
        private List<GridZona> Lista = new List<GridZona>();

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }

            Binding();
        }

        public void Binding()
        {
            int ID_JefeZona = ((US_USUARIOModel)Session["UserInfo"]).Id_Usuario;

            Lista = Validacion_RFC_L.ClassInstance.ObtenGridRFCVali(ID_JefeZona);
            GridViewRFC.DataSource = Lista;
            GridViewRFC.DataBind();
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {

        }

        protected void GridViewRFC_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
        {

        }

        protected void GridViewRFC_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {

        }

        protected void GridViewRFC_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {

        }

        protected void Lupa_Click(object sender, ImageClickEventArgs e)
        {

            var chk = (ImageButton)sender;
            var row = (GridDataItem)chk.Parent.Parent;
            var a = row.GetDataKeyValue("ID_Validacion");

            var URL = ("VisorPdf.aspx?Token=" + a.ToString());
            Response.BufferOutput = true;
            Response.Redirect("VisorPdf.aspx?Token=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(a.ToString())));

            RadWindow2.NavigateUrl = URL;
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "showWindoww", "MostrarPDF(" + a.ToString() + ");",true);

           

            
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            var Lista = new List<string>();

            foreach (GridDataItem item in GridViewRFC.Items)
            {
                var check = item.FindControl("CBX_Selecion") as CheckBox;
                var Row = item.GetDataKeyValue("ID_Validacion");

                if (check.Checked)
                {
                    Lista.Add(Row.ToString());
                }
            }


            if (Lista.Any())
            {
                try
                {
                    var rfc = new List<string>();
                    foreach (string Id_Validacion in Lista)
                    {

                        int ID_JefeZona = ((US_USUARIOModel)Session["UserInfo"]).Id_Usuario;
                        bool a = Validacion_RFC_L.ClassInstance.ActualizaRegistro(Convert.ToInt32(Id_Validacion), ID_JefeZona, null, 2);
                        var Reg = Validacion_RFC_L.ClassInstance.TraeRegistro(Convert.ToInt32(Id_Validacion));

                        var Correo = Validacion_RFC_L.ClassInstance.ObtenCorreoDist(Reg.Id_Distribuidor);
                        Correo.NombreJefeZona = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Completo_Usuario;

                        Correo.RFC = Reg.RFC;

                        try
                        {
                            MailUtility.MailValidacionRFC("NotificacionDistValValido.html",
                                                            Correo.NombreDistribuidor,
                                                            Correo.NombreJefeZona, Correo.RFC, Correo.Correo, null);
                        }
                        catch (Exception)
                        { 
                        }

                        rfc.Add(Correo.RFC);
                    }


                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),
                            "Mensaje",
                            string.Format("alert('Los siguentes RFC han sido confirmados: {0}');", string.Join(",", rfc)), true);
                    Binding();

                }
                catch (Exception er)
                {
                }
            }
            else 
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),"Mensaje","alert('Debes seleccionar almenos una solicitud');", true);
            }
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            var lstRFC=new List<string>();

            foreach (GridDataItem item in GridViewRFC.Items)
            {
                var check = item.FindControl("CBX_Selecion") as CheckBox;
                var Row = item.GetDataKeyValue("ID_Validacion");
                if (check.Checked)
                {
                    lstRFC.Add(Row.ToString());
                }
            }

            if (lstRFC.Count==1)
            {
                TXTRFC.Text = lstRFC.First();
                RadWindow1.VisibleOnPageLoad = true;

            }

            else if(lstRFC.Count > 1)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AlertInform", "alert('Solo Se puede rechazar una solicitud');", true);
                return;
            }
            else if (lstRFC.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AlertInform", "alert('Debes selecciona una solicitud');", true);
                return;
            }
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                int ID_Validacion = Convert.ToInt32(TXTRFC.Text);
                int ID_JefeZona = ((US_USUARIOModel)Session["UserInfo"]).Id_Usuario;
                string Comentarios = TBX_Motivos.Text;

                if (ID_JefeZona != 0)
                {
                    bool a = Validacion_RFC_L.ClassInstance.ActualizaRegistro(ID_Validacion, ID_JefeZona, Comentarios, 3);
                    var Reg = Validacion_RFC_L.ClassInstance.TraeRegistro(Convert.ToInt32(ID_Validacion));
                    var Correo = Validacion_RFC_L.ClassInstance.ObtenCorreoDist(Reg.Id_Distribuidor);
                    Correo.NombreJefeZona = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Completo_Usuario;
                    
                    Correo.RFC = Reg.RFC;

                    try
                    {
                        MailUtility.MailValidacionRFC("NotificacionDistValRechazado.html",
                                                        Correo.NombreDistribuidor,
                                                        Correo.NombreJefeZona, Correo.RFC, Correo.Correo, Comentarios);
                    }
                    catch(Exception err)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),
                            "Mensaje",
                            "alert('No fue posible enviar el correo a "+Correo.NombreDistribuidor+"');", true);
                    }

                    RadWindow1.Dispose();
                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),
                            "Mensaje",
                            "alert('El RFC han sido rechazado');", true);
                    TXTRFC.Text = "";
                    TBX_Motivos.Text = "";
                    RadWindow1.VisibleOnPageLoad = false;
                    Binding();
                }
            }
            catch(Exception er)
            {
            }
        }

        protected void Hidden_Click(object sender, EventArgs e)
        {
            RadWindow1.VisibleOnPageLoad = false;
        }

        protected void GridViewRFC_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            Binding();
        }

        protected void GridViewRFC_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            Binding();
        }
     
    }
}