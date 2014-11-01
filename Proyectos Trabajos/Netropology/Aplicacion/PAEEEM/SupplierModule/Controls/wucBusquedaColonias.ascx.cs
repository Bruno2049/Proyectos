using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.LogicaNegocios.SolicitudCredito;

namespace PAEEEM.SupplierModule.Controls
{
    public partial class wucBusquedaColonias : System.Web.UI.UserControl
    {
        public List<Colonia> LstColoniasAsentamientos
        {
            get { return (List<Colonia>)ViewState["LstColoniasAsentamientos"]; }
            set { ViewState["LstColoniasAsentamientos"] = value; }
        }

        public string Cp;
        public bool carga; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaGridColonias(Cp);
            }
            else
            {
                if(carga)
                    CargaGridColonias(Cp);
            }
        }

        private void CargaGridColonias(string codigoPostal)
        {
            try
            {
                LstColoniasAsentamientos = CatalogosSolicitud.ObtenColoniasXCp(codigoPostal);

                if (LstColoniasAsentamientos != null)
                {
                    if (LstColoniasAsentamientos.Count > 0)
                    {
                        grdColonias.DataSource = LstColoniasAsentamientos;
                        grdColonias.DataBind();
                    }
                    else
                    {
                        var sScript = "<script language='JavaScript'>alert('No se encuentra el Codigo Postal, contactar al Agente FIDE.');</script>";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
                    }
                }
                else
                {
                    var sScript = "<script language='JavaScript'>alert('No se encuentra el Codigo Postal, contactar al Agente FIDE.');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
                }
            }
            catch (Exception ex)
            {

                
            }
        }

        public Colonia RegresaColonia()
        {
            Colonia colonia = null;
            var checkseleccionados = 0;
            var cveCampoSelec = 0;

            for (int i = 0; i < grdColonias.Rows.Count; i++)
            {
                var ckbSelect = grdColonias.Rows[i].FindControl("ckbSelect") as CheckBox;

                if (ckbSelect != null && ckbSelect.Checked)
                {
                    var dataKey = grdColonias.DataKeys[i];
                    if (dataKey != null)
                        cveCampoSelec = int.Parse(dataKey[0].ToString());
                    checkseleccionados++;
                }
            }

            if (checkseleccionados == 0)
            {
                //var sScript = "<script language='JavaScript'>alert('Debe Seleccionar al menos un registro.');</script>";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
            }

            else if (checkseleccionados > 1)
            {
                //var sScript = "<script language='JavaScript'>alert('Debe Seleccionar solo un registro.');</script>";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
            }

            else
            {
                colonia = LstColoniasAsentamientos.FirstOrDefault(me => me.CveCp == cveCampoSelec);
            }

            return colonia;

        }

        protected void BtnColonia_Click(object sender, EventArgs e)
        {

        }
    }
}