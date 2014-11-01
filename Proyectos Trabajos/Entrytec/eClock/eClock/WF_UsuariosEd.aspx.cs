using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WF_UsuariosEd : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected int m_UsuarioID;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        //Titulo y Descripcion del Form
        Sesion.TituloPagina = "Edición de Usuarios";
        Sesion.DescripcionPagina = "Ingrese los datos de el Usuario";
        // Checamos si es la primera vez que se carga la página y no contiene valores introducidos
        // por el cliente
        m_UsuarioID = CeC.Convierte2Int(Sesion.WF_Usuarios_USUARIO_ID);
        // Si la configuración es nueva se le asigna el valor -1 para que se genere un autonumerico para el nuevo ID
        if (m_UsuarioID < 0)
        {
            m_UsuarioID = -1;
        }
        // Checamos si es la primera vez que se carga la página y no contiene valores introducidos
        // por el cliente
        Wco_EC_PERFILES.DataValueField = "PERFIL_ID";
        //            PerfilId.Columns[0].Key = "PERFIL_ID";
        Wco_EC_PERFILES.DisplayValue = "PERFIL_NOMBRE";
        Wco_EC_PERFILES.DataTextField = "PERFIL_NOMBRE";

        Wco_EC_PERFILES.DataSource = CeC_Perfiles.ObtenPerfilesDS(false);
        Wco_EC_PERFILES.DataBind();
        if (!IsPostBack)
        {
            // Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Usuarios", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            // Cargamos los datos del formulario
            CargarDatos(true);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Cargar"></param>
    private void CargarDatos(bool Cargar)
    {
        // Variable local que guardara los datos del usuario
        CeC_Usuarios l_Usuario = new CeC_Usuarios(m_UsuarioID, Sesion);
        // Se inicializan los controles Text Box con una cadena vacia
        if (Wtx_USUARIO_USUARIO.Value == null)
            Wtx_USUARIO_USUARIO.Value = "";
        if (Wtx_USUARIO_NOMBRE.Value == null)
            Wtx_USUARIO_NOMBRE.Value = "";
        if (Wtx_USUARIO_DESCRIPCION.Value == null)
            Wtx_USUARIO_DESCRIPCION.Value = "";
        if (Wtx_USUARIO_CLAVE.Value == null)
            Wtx_USUARIO_CLAVE.Value = "";
        if (Wtx_USUARIO_EMAIL.Value == null)
            Wtx_USUARIO_EMAIL.Value = "";
        try
        {
            // Si es una carga de datos
            if (Cargar)
            {
                // Asigna al control Text Box el elemento guardado en la Base de Datos para el elemento seleccionado
                try
                {
                    //Wne_SITIO_ID.Text = l_Sitio.Sitio_Id.ToString();
                    Wco_EC_PERFILES.DataValue = l_Usuario.Perfil_Id;
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_UsuariosEd.CargarDatos", ex); }
                try
                {
                    Wtx_USUARIO_USUARIO.Text = l_Usuario.Usuario_Usuario.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_UsuariosEd.CargarDatos", ex); }
                try
                {
                    Wtx_USUARIO_NOMBRE.Text = l_Usuario.Usuario_Nombre.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_UsuariosEd.CargarDatos", ex); }
                try
                {
                    Wtx_USUARIO_EMAIL.Text = l_Usuario.Usuario_Email.ToString(); ;
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_UsuariosEd.CargarDatos", ex); }
                try
                {
                    Wtx_USUARIO_CLAVE.Text = l_Usuario.Usuario_Clave.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_UsuariosEd.CargarDatos", ex); }
                try
                {
                    Wtx_USUARIO_CLAVEC.Text = l_Usuario.Usuario_Clave.ToString();
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_UsuariosEd.CargarDatos", ex); }
                try
                {
                    Chb_USUARIO_BORRADO.Checked = l_Usuario.Usuario_Borrado;
                }
                catch (Exception ex)
                { CIsLog2.AgregaError("WF_UsuariosEd.CargarDatos", ex); }
            }
            // Si no es una carga de datos y se va a modificar o guardar nuevos datos.
            else
            {
                if (CeC_Usuarios.ObtenUsuarioID(Wtx_USUARIO_USUARIO.Text) > 0)
                {
                    Lbl_Error.Text = "Nombre de Usuario ya existe.";
                    return;
                }
                if (l_Usuario.Actualiza(m_UsuarioID, //CeC.Convierte2Int(l_Usuario.Usuario_Id),
                                        CeC.Convierte2Int(Wco_EC_PERFILES.DataValue),
                                        CeC.Convierte2String(Wtx_USUARIO_USUARIO.Value),
                                        CeC.Convierte2String(Wtx_USUARIO_NOMBRE.Value),
                                        CeC.Convierte2String(Wtx_USUARIO_DESCRIPCION.Value),
                                        CeC.Convierte2String(Wtx_USUARIO_CLAVE.Value),
                                        CeC.Convierte2String(Wtx_USUARIO_EMAIL.Value),
                                        true,
                                        CeC.Convierte2Int(Sesion.SuscripcionParametro),
                                        "",
                                        true,
                                        false,
                                        Sesion.USUARIO_ID,
                                        Chb_USUARIO_BORRADO.Checked,
                                        Sesion))
                {
                    Lbl_Correcto.Text = "Datos guardados";
                    Lbl_Error.Text = "";
                }
                else
                {
                    Lbl_Error.Text = "Error al guardar los datos";
                    Lbl_Correcto.Text = "";
                    return;
                }
            }
        }
        catch { }

    }

    protected void WIBtn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            Lbl_Error.Text = "";
            Lbl_Correcto.Text = "";
            CargarDatos(false);
        }
        catch (Exception ex)
        {
            Lbl_Error.Text = "Error :" + ex.Message + "\nNo se han llenado los datos correctamente.";
        }
    }
    protected void WIBtn_Deshacer_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CargarDatos(true);
    }
}