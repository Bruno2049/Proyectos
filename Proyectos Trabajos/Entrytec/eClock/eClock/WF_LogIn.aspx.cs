using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_LogIn.
    /// </summary>
    /// 

    public partial class WF_LogIn : System.Web.UI.Page
    {
        CeC_Sesion Sesion;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            /*IsProtectServer.CeT_PS PS = new IsProtectServer.CeT_PS();
            //PS.Activacion = "1234-5678-9101-1213";
            PS.Directorio = HttpRuntime.AppDomainAppPath;*/
            CeC_BD.IniciaAplicacion();
            if (CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_USUARIOS WHERE USUARIO_ID = 1") != 1 || !CeC_BD.Activado())
            {
                
                //          this.Response.Redirect("WF_Activacion.aspx", false);
                return;
            }
            // Introducir aquí el código de usuario para inicializar la página
            /*   Random rdm1 = new Random(unchecked(1000000));
               this.RegisterClientScriptBlock("MensageScript" + rdm1.Next().ToString(), "<script language=JavaScript> " +
               "function BotonIS_MouseDown(oButton, oEvent){oButton.getElementAt(0).click();oButton.setEnabled(false);}</script>");
               BEntrar.ClientSideEvents.MouseDown = "BotonIS_MouseDown";
               */
            /*if (!CeC_BD.EstaeClockListo())
            {
                CeC_Config.imglogin = System.IO.File.ReadAllBytes(HttpRuntime.AppDomainAppPath + "\\Imagenes\\imglogin.JPG");
                CeC_Config.imgencabezado = System.IO.File.ReadAllBytes(HttpRuntime.AppDomainAppPath + "\\Imagenes\\imgencabezado.jpg");
                CeC_Config.imgreporte = System.IO.File.ReadAllBytes(HttpRuntime.AppDomainAppPath + "\\Imagenes\\imgreporte.jpg");

                Sesion = CeC_Sesion.Nuevo(this, 1);
                Sesion.USUARIO_ID = 1;
                Sesion.Redirige("WF_Wizarda.aspx");
                return;
            }*/
            if (IsPostBack)
            {
                string Evento = Request.Params.Get("__EVENTTARGET");
                if (Evento == "OnClickTerminal")
                {
                    Response.Redirect(CeC_Config.LinkeClockComprar, false);
                }
                if (Evento == "OnClickComentarios")
                {
                    Response.Redirect(CeC_Config.LinkeClockComentarios, false);
                }
                if (Evento == "OnClickSitio")
                {
                    Response.Redirect(CeC_Config.LinkeClock, false);
                }
            }
            else
            {
                
            }
        }

        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.BEntrar.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BEntrar_Click);

        }
        #endregion



        protected void BEntrar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            /*IsProtectServer.CeT_PS PS = new IsProtectServer.CeT_PS();
            PS.Directorio = HttpRuntime.AppDomainAppPath;
            if (!PS.Coinciden(IsProtectServer.CeT_PS.Productos.eClock))
            {
                this.Response.Redirect("WF_Activacion.aspx", false);
            }
            else
            {*/
            int UsuarioRespuesta = CeC_Sesion.ValidarUsuario(UsuarioUsuario.Text, UsuarioClave.Text);
            if (UsuarioRespuesta <= 0)
            {
                lbl_Estado.Text = "Usuario o contraseña incorrectos";
                img_Error.ImageUrl = "Imagenes/warning_triangle.png";
                pnl_Estado.BorderColor = System.Drawing.Color.Red;
            }
            else
            {
                lbl_Estado.Text = "Usuario Correcto";
                Sesion = CeC_Sesion.Nuevo(this, UsuarioRespuesta);
                Sesion.EsWizard = 0;
                Sesion.Redirige(Sesion.LinkLogIn);
                
                CeC_BD.IniciaAplicacion();
            }
            //}
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect(CeC_Config.LinkOlvidoClave);
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect(CeC_Config.LinkNuevoUsuario);
        }
    }
}
