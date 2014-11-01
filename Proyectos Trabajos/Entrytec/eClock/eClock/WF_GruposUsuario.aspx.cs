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

public partial class WF_GruposUsuario : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    DS_Grupos DSGrupos = new DS_Grupos();
    DS_GruposTableAdapters.EC_SUSCRIPCIONTableAdapter TAGrupo1 = new DS_GruposTableAdapters.EC_SUSCRIPCIONTableAdapter();
    DS_GruposTableAdapters.EC_GRUPOS_2TableAdapter TAGrupo2 = new DS_GruposTableAdapters.EC_GRUPOS_2TableAdapter();
    DS_GruposTableAdapters.EC_GRUPOS_3TableAdapter TAGrupo3 = new DS_GruposTableAdapters.EC_GRUPOS_3TableAdapter();
    int Grupo;
    int Usuario_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Grupo = Sesion.WF_UsuariosE_GrupoNo;
        Usuario_id = Sesion.WF_Usuarios_USUARIO_ID;
        Sesion.TituloPagina = "Asignacion de Grupos a Usuario";
        Sesion.DescripcionPagina = "Seleccione los Grupos que se Asignaran al Usuario";
        lnombre.Text = CeC_BD.EjecutaEscalarString("SELECT USUARIO_NOMBRE FROM EC_USUARIOS WHERE USUARIO_ID = " +Usuario_id);
        if (!IsPostBack)
        {
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asignación de Grupos a Usuario", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void btnguardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Usuario_id = Sesion.WF_Usuarios_USUARIO_ID;
        Grupo = Sesion.WF_UsuariosE_GrupoNo;
        int existe;
        switch (Grupo)
        {
            case 1:
                for (int i = 0; i < DSGrupos.EC_SUSCRIPCION.Rows.Count; i++)
                {
                    if (DSGrupos.EC_SUSCRIPCION[i].RowState == DataRowState.Modified)
                    {
                        int idgrupo = Convert.ToInt32(DSGrupos.EC_SUSCRIPCION[i].SUSCRIPCION_ID);
                        if (DSGrupos.EC_SUSCRIPCION[i].IsACTIVONull() || !DSGrupos.EC_SUSCRIPCION[i].ACTIVO)
                            TAGrupo1.DeleteQuery(Usuario_id, idgrupo);
                        else
                        {
                            existe = CeC_BD.EjecutaEscalarInt("SELECT COUNT(USUARIO_ID)FROM EC_PERMISOS_SUSCRIP WHERE USUARIO_ID=" + Usuario_id + " AND SUSCRIPCION_ID =" + idgrupo);
                            if (existe == 0)
                            {
                                TAGrupo1.InsertQuery(Usuario_id, idgrupo);
                            }
                        }
                    }
                }
                break;
            case 2:
                for (int i = 0; i < DSGrupos.EC_GRUPOS_2.Rows.Count; i++)
                {
                    if (DSGrupos.EC_GRUPOS_2[i].RowState == DataRowState.Modified)
                    {
                        int idgrupo = Convert.ToInt32(DSGrupos.EC_GRUPOS_2[i].GRUPO_2_ID);
                        if (!DSGrupos.EC_GRUPOS_2[i].ACTIVO)
                            TAGrupo1.DeleteQuery(Usuario_id, idgrupo);
                        else
                        {
                            existe = CeC_BD.EjecutaEscalarInt("SELECT COUNT(USUARIO_ID)FROM EC_USUARIOS_GRUPOS_2 WHERE USUARIO_ID=" + Usuario_id + " AND GRUPO_2_ID =" + idgrupo);
                            if (existe == 0)
                            {
                                TAGrupo2.InsertQuery(Usuario_id, idgrupo);
                            }
                        }
                    }
                }
                break;
            case 3:
                for (int i = 0; i < DSGrupos.EC_GRUPOS_3.Rows.Count; i++)
                {
                    if (DSGrupos.EC_GRUPOS_3[i].RowState == DataRowState.Modified)
                    {
                        int idgrupo = Convert.ToInt32(DSGrupos.EC_GRUPOS_3[i].GRUPO_3_ID);
                        if (!DSGrupos.EC_GRUPOS_3[i].ACTIVO)
                            TAGrupo1.DeleteQuery(Usuario_id, idgrupo);
                        else
                        {
                            existe = CeC_BD.EjecutaEscalarInt("SELECT COUNT(USUARIO_ID)FROM EC_USUARIOS_GRUPOS_3 WHERE USUARIO_ID=" + Usuario_id + " AND GRUPO_3_ID =" + idgrupo);
                            if (existe == 0)
                            {
                                TAGrupo3.InsertQuery(Usuario_id, idgrupo);
                            }
                        }
                    }
                }
                break;
        }
    }

    protected void btnregresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_UsuariosE.aspx");
    }

    protected void UWGGrupo_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        Grupo = Sesion.WF_UsuariosE_GrupoNo;
        Sesion = CeC_Sesion.Nuevo(this);
        CeC_Grid.AplicaFormato(UWGGrupo, true, true, false, true);
        UWGGrupo.Height = Unit.Pixel(200);
    }

    protected void UWGGrupo_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Usuario_id = Sesion.WF_Usuarios_USUARIO_ID;
        Grupo = Sesion.WF_UsuariosE_GrupoNo;
        switch (Grupo)
        {
            case 1:
                TAGrupo1.FillGrupo1(DSGrupos.EC_SUSCRIPCION, Usuario_id);
//                TAGrupo1.FillBy(DSGrupos.EC_SUSCRIPCION, Usuario_id);
                UWGGrupo.DataSource = DSGrupos.EC_SUSCRIPCION;
                UWGGrupo.DataMember = DSGrupos.EC_SUSCRIPCION.TableName;
                UWGGrupo.DataKeyField = "SUSCRIPCION_ID";
            break;
            case 2 :
                TAGrupo2.FillGrupo2(DSGrupos.EC_GRUPOS_2, Usuario_id);
                UWGGrupo.DataSource = DSGrupos.EC_GRUPOS_2;
                UWGGrupo.DataMember = DSGrupos.EC_GRUPOS_2.TableName;
                UWGGrupo.DataKeyField = "GRUPO_2_ID";
            break;
            case 3:
                TAGrupo3.FillGrupo3(DSGrupos.EC_GRUPOS_3, Usuario_id);
                UWGGrupo.DataSource = DSGrupos.EC_GRUPOS_3;
                UWGGrupo.DataMember = DSGrupos.EC_GRUPOS_3.TableName;
                UWGGrupo.DataKeyField = "GRUPO_3_ID";
            break;
        }
    }

    protected void UWGGrupo_UpdateRowBatch(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
    {
        Grupo = Sesion.WF_UsuariosE_GrupoNo;
        Sesion = CeC_Sesion.Nuevo(this);
        switch (Grupo)
        {
            case 1:
                CeC_BD.ActualizaRowBatch(UWGGrupo, e, DSGrupos, DSGrupos.EC_SUSCRIPCION.TableName.ToString(), "SUSCRIPCION_ID");
                break;
            case 2:
                CeC_BD.ActualizaRowBatch(UWGGrupo, e, DSGrupos, DSGrupos.EC_GRUPOS_2.TableName.ToString(), "GRUPO_2_ID");
                break;
            case 3:
                CeC_BD.ActualizaRowBatch(UWGGrupo, e, DSGrupos, DSGrupos.EC_GRUPOS_3.TableName.ToString(), "GRUPO_3_ID");
                break;
        }
    }
}