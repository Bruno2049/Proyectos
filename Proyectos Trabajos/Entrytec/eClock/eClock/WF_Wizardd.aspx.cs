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

public partial class WF_Wizardd : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected eClock.DS_Usuarios dS_Usuarios1 = new eClock.DS_Usuarios();
    eClock.DS_Usuarios.EC_USUARIOS_EDICIONRow Fila;
    protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
    protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
    protected System.Data.OleDb.OleDbDataAdapter DA_EUsuarios = new System.Data.OleDb.OleDbDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (!IsPostBack)
        {

            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Asistente de Configuracion";
            Sesion.DescripcionPagina = "Cambio de Password";
            this.Master.FindControl("WCBotonesEncabezado1").Visible = !Convert.ToBoolean(Sesion.EsWizard);
            this.Master.FindControl("WC_Menu1").FindControl("mnu_Main").Visible = !Convert.ToBoolean(Sesion.EsWizard);
            string pass = CeC_BD.EjecutaEscalarString("SELECT USUARIO_CLAVE FROM EC_USUARIOS WHERE USUARIO_USUARIO LIKE '" + Sesion.USUARIO_USUARIO + "'");
            TxtConfirmaPassword.Text = pass;
            TxtPassword.Text = pass;
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asistente de Configuración", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }
    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Page_Load(sender, e);
    }
    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (TxtConfirmaPassword.Text == TxtPassword.Text)
        {

            if (TxtPassword.Text != "admin" && TxtPassword.Text != "")
            {
                
                System.Data.OleDb.OleDbConnection Conexion = new System.Data.OleDb.OleDbConnection();
                this.oleDbSelectCommand1.CommandText = "SELECT USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, USUARIO_DESCRIPCIO" +
                    "N, USUARIO_CLAVE, USUARIO_BORRADO FROM EC_USUARIOS WHERE (USUARIO_ID = ?)";
                Conexion = (System.Data.OleDb.OleDbConnection)CeC_BD.ObtenConexion();
                this.oleDbSelectCommand1.Connection = Conexion;
                this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "USUARIO_ID", System.Data.DataRowVersion.Current, null));
                this.DA_EUsuarios.SelectCommand = this.oleDbSelectCommand1;

                this.DA_EUsuarios.UpdateCommand = this.oleDbUpdateCommand1;
                this.oleDbUpdateCommand1.CommandText = "UPDATE EC_USUARIOS SET PERFIL_ID = ?, USUARIO_USUARIO = ?, USUARIO_NOMBRE = ?, U" +
                        "SUARIO_DESCRIPCION = ?, USUARIO_CLAVE = ?, USUARIO_BORRADO = ? WHERE (USUARIO_ID" +
                        " = ?)";
                this.oleDbUpdateCommand1.Connection = Conexion;
                this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Current, null));
                this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_USUARIO", System.Data.OleDb.OleDbType.VarChar, 20, "USUARIO_USUARIO"));
                this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_NOMBRE"));
                this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_DESCRIPCION", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_DESCRIPCION"));
                this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_CLAVE", System.Data.OleDb.OleDbType.VarChar, 45, "USUARIO_CLAVE"));
                this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "USUARIO_BORRADO", System.Data.DataRowVersion.Current, null));
                this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_USUARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "USUARIO_ID", System.Data.DataRowVersion.Original, null));
                
                DA_EUsuarios.SelectCommand.Parameters[0].Value = 1;
                DA_EUsuarios.Fill(dS_Usuarios1.EC_USUARIOS_EDICION);

                Fila = (eClock.DS_Usuarios.EC_USUARIOS_EDICIONRow)dS_Usuarios1.EC_USUARIOS_EDICION.Rows[0];
                Fila.USUARIO_CLAVE = Convert.ToString(TxtPassword.Text);
                DA_EUsuarios.Update(dS_Usuarios1.EC_USUARIOS_EDICION);
                Sesion.Redirige("WF_Importacion.aspx");
            }
            else
                LError.Text = ("Su Password no es seguro, intente de nuevo");
        }
        else
        {
            LError.Text = "El Password no coincide, intente de nuevo";
        }
    }
}
