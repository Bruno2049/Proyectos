using System;
using System.Web.UI;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.TarifaSubEstaciones;
using PAEEEM.LogicaNegocios.Trama;

namespace PAEEEM.SupplierModule
{
    public partial class CambiarRPU : Page
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
            if (ValidacionTrama.IsServiceCodeLongEnough(TXB_Nuevo_RPU.Text) == false)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "El RPU no es Valido", "alert('El RPU no es valido');", true);
                TXB_Nuevo_RPU.Text = "";
            }

            else if (SubEstaciones.ClassInstance.HayRegistroDeNuevoRPUDist(TXB_No_Credito.Text) == false)
            {
                var nuevo = new Cambio_RPU
                {
                    No_Credito = TXB_No_Credito.Text,
                    RPU_Distribuidor = TXB_Nuevo_RPU.Text,
                    Fecha_Captura_Dist = DateTime.Now,
                    Usuario_Distribuidor = ((US_USUARIOModel) Session["UserInfo"]).Nombre_Usuario
                };
                var obtenCorreoJefe = SubEstaciones.ClassInstance.IrPorCorreoZona(nuevo.Usuario_Distribuidor,
                    ((US_USUARIOModel) Session["UserInfo"]).Tipo_Usuario);
                nuevo.Usuario_Jefe_Zona = obtenCorreoJefe.Usuario;
                var inserto = SubEstaciones.ClassInstance.InsertaNuevoRPUDist(nuevo);
                if (inserto != null)
                {
                    BTN_Enviar.Enabled = false;

                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                 Convert.ToInt16(Session["IdRolUserLogueado"]),
                 Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                 "SOLICITUD DE CREDITO", "CAMBIO DE TARIFA DISTRIBUIDOR", inserto.No_Credito,
                 "", "RPU NUEVO: "+ inserto.RPU_Distribuidor, "", "");

                    try
                    {
                        MailUtility.MailNuevoRpu("MensajeRPUDist.html", obtenCorreoJefe.RazonSocialDistribuidor,
                           obtenCorreoJefe.NombreJefeZona, obtenCorreoJefe.NombreZona,
                            nuevo.No_Credito, obtenCorreoJefe.Correo);

                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                            "Mensaje",
                            "alert('Se almacenó correctamente el RPU. Se ha enviado correo a jefe de zona');", true);
                    }
                    catch (Exception err)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                            "Mensaje",
                            "alert('Se almacenó correctamente el RPU. Fue imposible enviar Correo');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                        "Mensaje",
                        "alert('Ocurrió un error al tratar de insertar los datos');", true);
                }
            }
            else if (SubEstaciones.ClassInstance.HayRegistroDeNuevoRPUDist(TXB_No_Credito.Text))
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page), "Se almacenó correctamente el RPU",
                    "alert('Ya se ha almacenado el RPU');", true);
            }

        }

        protected void BTN_Salir_Click(object sender, EventArgs e)
        {
            Response.Redirect("../SupplierModule/CreditMonitor.aspx");
        }
    }
}