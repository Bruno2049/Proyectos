using System;
using System.Web.UI;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.TarifaSubEstaciones;
using PAEEEM.LogicaNegocios.Trama;
using PAEEEM.Helpers;

namespace PAEEEM.RegionalModule
{
    public partial class CambiarRPUZona : Page
    {
            private string _noCredito;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!IsPostBack)
            {
                if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
                {
                    _noCredito = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Token"]));
                }

                var datos = SubEstaciones.ClassInstance.IrPorDatos(_noCredito);

                TXB_No_Credito.Text = datos.No_Credito;
                
                TXB_Nombre_Razon.Text = string.IsNullOrEmpty(datos.Razon_Social) ? datos.Nombre : datos.Razon_Social;

                TXB_Antiguo_RPU.Text = datos.Antiguo_RPU;
            }
        }

        protected void BTN_Enviar_Click(object sender, EventArgs e)
        {

            var nuevo = SubEstaciones.ClassInstance.ObtenDatosRpuDist(TXB_No_Credito.Text);
            var correoDist = SubEstaciones.ClassInstance.ObtenCorreoJefeZona(nuevo.Usuario_Jefe_Zona,nuevo.Usuario_Distribuidor);

            if (ValidacionTrama.IsServiceCodeLongEnough(TXB_Nuevo_RPU.Text) == false)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "El RPU NO es valido", "alert('El RPU NO es valido');",
                    true);
                TXB_Nuevo_RPU.Text = "";
            }

            else
            {
                bool actualizo;
                bool elimino;

                var inserto = SubEstaciones.ClassInstance.ActualizaRPUJefeZona(TXB_No_Credito.Text, TXB_Nuevo_RPU.Text,
                    out actualizo,
                    out elimino);

                if (actualizo)
                {
                    BTN_Enviar.Enabled = false;

                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                        Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                        "SOLICITUD DE CREDITO", "CAMBIO DE TARIFA ZONA", inserto.No_Credito,
                        "", "RPU NUEVO: " + inserto.RPU_Jefe_Zona, "", "");

                    try
                    {

                        if (correoDist != null)
                            MailUtility.MailNuevoRpu("MensajeRPUZona.html", correoDist.RazonSocialDistribuidor,
                                correoDist.NombreJefeZona,correoDist.NombreZona, nuevo.No_Credito, correoDist.Correo);
                        else
                            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                                "Se almacenó correctamente el RPU", "alert('No se pudo enviar Correo');", true);

                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                            "El RPU se almacenó Correctamente",
                            " alert('El RPU es valido. Se ha iniciado el procedimiento correspondiente');", true);
                    }
                    catch (Exception err)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                            "Mensaje",
                            "alert('Se almacenó correctamente el RPU. No se pudo enviar Correo');", true);
                    }
                }

                if (!elimino) return;
                BTN_Enviar.Enabled = false;
                TXB_Nuevo_RPU.Text = "";


                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                    "SOLICITUD DE CREDITO", "CAMBIO DE TARIFA ZONA", inserto.No_Credito,
                    "", "RPU NUEVO: " + inserto.RPU_Jefe_Zona, "", "");

                try
                {
                    if (correoDist != null)
                        MailUtility.MailRpuNoCoincide("MensajeRPUZonaErrado.html", correoDist.RazonSocialDistribuidor,
                            correoDist.NombreJefeZona,correoDist.NombreZona, nuevo.No_Credito, correoDist.Correo);
                    else
                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                            "Se almacenó correctamente el RPU", "alert('Se almacenó correctamente el RPU. No se pudo enviar Correo');", true);

                }
                catch (Exception err)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                        "Se almacenó correctamente el RPU",
                        "alert('Se almacenó correctamente el RPU. No se pudo enviar Correo');", true);
                }

                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                    "El RPU NO coincide con el del Distribuidor",
                    "alert('El RPU NO es valido. Se debe reiniciar el procedimiento correspondiente');", true);
            }
        }

        protected void BTN_Salir_Click(object sender, EventArgs e)
        {
            Response.Redirect("../RegionalModule/CreditAuthorization.aspx");
        }
    }
}