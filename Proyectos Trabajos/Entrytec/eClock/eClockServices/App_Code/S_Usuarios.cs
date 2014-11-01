using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Usuarios
{
    // Veriable miembro de la clase que guarda el ID del Periodo que se está editando
    protected int m_Usuario_ID;
    // Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
    // Para crear una operación que devuelva XML,
    //     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
    //     e incluya la siguiente línea en el cuerpo de la operación:
    //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
    [OperationContract]
    public void DoWork()
    {
        // Agregue aquí la implementación de la operación
        return;
    }
    /// <summary>
    /// Verifica si el usuario existe dentro del registro y en base a
    /// la existencia verifica si tiene derechos.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="UsuarioUsuario"></param>
    /// <returns></returns>
    [OperationContract]
    public bool ExisteUsuario(string SesionSeguridad, string UsuarioUsuario)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        if (!CeC_Restricciones.TieneDerecho(Sesion.PERFIL_ID, "S.Usuario.Listar"))
            return false;
        return CeC_Usuarios.ExisteUsuario(UsuarioUsuario);
    }
    /// <summary>
    /// Busca el ID del usuario que se requiere y si tiene alguna restriccion sobre la busqueda.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="UsuarioUsuario"></param>
    /// <returns></returns>
    [OperationContract]
    public int ObtenUsuarioID(string SesionSeguridad, string UsuarioUsuario)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        if (!CeC_Restricciones.TieneDerecho(Sesion.PERFIL_ID, "S.Usuario.Listar"))
            return -2;
        return CeC_Usuarios.ObtenUsuarioID(UsuarioUsuario);
    }
    /// <summary>
    /// Obtiene la subscripción activa para el usuario que se requiere.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="UsuarioID"></param>
    /// <returns>Devolviendo como parametro la suscripción.</returns>
    [OperationContract]
    public int ObtenSuscripcionID(string SesionSeguridad, int UsuarioID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        if (!CeC_Restricciones.TieneDerecho(Sesion.PERFIL_ID, "S.Usuario.Listar"))
            return -2;
        return CeC_Usuarios.ObtenSuscripcionID(UsuarioID);
    }
    /// <summary>
    /// Efectua el cambio de password en base a un password anterior y un password nuevo.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="ClaveAnterior"></param>
    /// <param name="ClaveNueva"></param>
    /// <returns>Devuelve el cambio de password para el usuario en especifico.</returns>
    [OperationContract]
    public bool CambiaPassword(string SesionSeguridad, string ClaveAnterior, string ClaveNueva)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        return CeC_Usuarios.CambiaPassword(Sesion.USUARIO_ID, ClaveAnterior, ClaveNueva);
    }

    [OperationContract]
    public bool ValidaPassword(string SesionSeguridad, string Clave)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        return CeC_Usuarios.ValidaPassword(Sesion.USUARIO_ID, Clave);
    }

    /// <summary>
    /// Agrega un nuevo Usuario
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="UsuarioId">Identificador de la tabla de usuarios</param>
    /// <param name="PerfilId">Identificar del Perfil</param>
    /// <param name="UsuarioUsuario">Usuario (login)</param>
    /// <param name="UsuarioNombre">Nombre propio de la persona</param>
    /// <param name="UsuarioDescripcion">Cualquier comentario sobre el usuario</param>
    /// <param name="UsuarioClave">Contraseña del usuario</param>
    /// <param name="UsuarioEmail">Contiene el e-mail del usuario</param>
    /// <param name="UsuarioEnvmaila">Indica que a este usuario se le enviaran mails de asistencia con todos sus empleados seleccionados</param>
    /// <param name="SuscripcionId">Suscripcion Predeterminada</param>
    /// <param name="UsuarioAgrupacion">Contiene la agrupación a la cual tiene control, por default esta en blanco y significa a todos los empleados de la suscripcion</param>
    /// <param name="UsuarioEssup">Indica que este usuario es supervisor, reporteador o administrador</param>
    /// <param name="UsuarioEsemp">Indica que este usuario es empleado</param>
    /// <param name="PersonaId">Identificador del persona_ID en caso de ser empleado</param>
    /// <param name="UsuarioBorrado">Si es positivo indica que este usuario esta borrado(dado de baja)</param>
    /// <returns>Verdadero si se agrego el usuario correctamente. Falso si no se agrego el usuario.</returns>
    [OperationContract]
    public bool AgregaUsuario(string SesionSeguridad, int PerfilId, string UsuarioUsuario, string UsuarioNombre, string UsuarioDescripcion, string UsuarioClave, string UsuarioEmail, bool UsuarioEnvmaila, int SuscripcionId, string UsuarioAgrupacion, bool UsuarioEssup, bool UsuarioEsemp, int PersonaId, bool UsuarioBorrado)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return false;
            if (!CeC_Restricciones.TieneDerecho(Sesion.PERFIL_ID, "S.Usuario.Alta"))
                return false;
            if (SuscripcionId != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
                return false;

            CeC_Usuarios l_Usuario = new CeC_Usuarios(Sesion);
            return l_Usuario.Actualiza(-9999,
                                        PerfilId,
                                        UsuarioUsuario,
                                        UsuarioNombre,
                                        UsuarioDescripcion,
                                        UsuarioClave,
                                        UsuarioEmail,
                                        UsuarioEnvmaila,
                                        SuscripcionId,
                                        UsuarioAgrupacion,
                                        UsuarioEssup,
                                        UsuarioEsemp,
                                        PersonaId,
                                        UsuarioBorrado,
                                        Sesion);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    /// <summary>
    /// Modifica los datos de un Usuario.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="UsuarioId">Identificador de la tabla de usuarios</param>
    /// <param name="PerfilId">Identificar del Perfil</param>
    /// <param name="UsuarioUsuario">Usuario (login)</param>
    /// <param name="UsuarioNombre">Nombre propio de la persona</param>
    /// <param name="UsuarioDescripcion">Cualquier comentario sobre el usuario</param>
    /// <param name="UsuarioClave">Contraseña del usuario</param>
    /// <param name="UsuarioEmail">Contiene el e-mail del usuario</param>
    /// <param name="UsuarioEnvmaila">Indica que a este usuario se le enviaran mails de asistencia con todos sus empleados seleccionados</param>
    /// <param name="SuscripcionId">Suscripcion Predeterminada</param>
    /// <param name="UsuarioAgrupacion">Contiene la agrupación a la cual tiene control, por default esta en blanco y significa a todos los empleados de la suscripcion</param>
    /// <param name="UsuarioEssup">Indica que este usuario es supervisor, reporteador o administrador</param>
    /// <param name="UsuarioEsemp">Indica que este usuario es empleado</param>
    /// <param name="PersonaId">Identificador del persona_ID en caso de ser empleado</param>
    /// <param name="UsuarioBorrado">Si es positivo indica que este usuario esta borrado(dado de baja)</param>
    /// <returns></returns>
    [OperationContract]
    public bool ModificaUsuario(string SesionSeguridad, int PerfilId, string UsuarioUsuario, string UsuarioNombre, string UsuarioDescripcion, string UsuarioClave, string UsuarioEmail, bool UsuarioEnvmaila, int SuscripcionId, string UsuarioAgrupacion, bool UsuarioEssup, bool UsuarioEsemp, int PersonaId, bool UsuarioBorrado)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            int UsuarioId = -99999;
            if (Sesion == null)
                return false;
            if (UsuarioUsuario == "")
                UsuarioId = CeC_Usuarios.ObtenUsuarioID(UsuarioEmail);
            else
                UsuarioId = CeC_Usuarios.ObtenUsuarioID(UsuarioUsuario);
            if (UsuarioEmail == "")
                UsuarioId = CeC_Usuarios.ObtenUsuarioID(UsuarioUsuario);
            else
                UsuarioId = CeC_Usuarios.ObtenUsuarioID(UsuarioEmail);
            CeC_Usuarios l_Usuario = new CeC_Usuarios(UsuarioId, Sesion);
            // Establecemos el usuario como borrado.
            // La funcion AgregaUsuario permite modificar al usuario.
            return l_Usuario.Actualiza(UsuarioId,
                            PerfilId,
                            UsuarioUsuario,
                            UsuarioNombre,
                            UsuarioDescripcion,
                            UsuarioClave,
                            UsuarioEmail,
                            UsuarioEnvmaila,
                            SuscripcionId,
                            UsuarioAgrupacion,
                            UsuarioEssup,
                            UsuarioEsemp,
                            PersonaId,
                            UsuarioBorrado,
                            Sesion);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    /// <summary>
    /// Borra un usuario (se pone como inactivo).
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="UsuarioId">Usuario que se va a borrar</param>
    /// <returns>Verdadero si se borro con exito el usuario. </returns>
    [OperationContract]
    public bool BorraUsuario(string SesionSeguridad, int UsuarioId)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return false;
            // Creamos un objeto usuario con todos sus datos obtenidos desde su UsuarioID.
            CeC_Usuarios l_Usuario = new CeC_Usuarios(UsuarioId, Sesion);
            // Establecemos el usuario como borrado.
            return l_Usuario.Actualiza(l_Usuario.Usuario_Id,
                                        l_Usuario.Perfil_Id,
                                        l_Usuario.Usuario_Usuario,
                                        l_Usuario.Usuario_Nombre,
                                        l_Usuario.Usuario_Descripcion,
                                        l_Usuario.Usuario_Clave,
                                        l_Usuario.Usuario_Email,
                                        l_Usuario.Usuario_Envmaila,
                                        l_Usuario.Suscripcion_Id,
                                        l_Usuario.Usuario_Agrupacion,
                                        l_Usuario.Usuario_Essup,
                                        l_Usuario.Usuario_Esemp,
                                        l_Usuario.Persona_Id,
                                        true,
                                        Sesion);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    /*
    /// <summary>
    /// Muestra los Usuarios disponibles
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="SuscripcionID">Identificador de la Suscripción</param>
    /// <param name="MostrarBorrados">Valida si se muestran los Usuarios borrados (TRUE muestra todos los Usuarios incluidos los borrados)</param>
    /// <returns>DataSet con USUARIO_ID, USUARIO_USUARIO, USUARIO_NOMBRE</returns>
    [OperationContract]
    public DataSet Mostrar(string SesionSeguridad, int SuscripcionID, bool MostrarBorrados)
    {
        try
        { 
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            return CeC_Usuarios.Mostrar(SuscripcionID, MostrarBorrados);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return null;
    }*/
    [OperationContract]
    public int CreaUsuarioSuscripcion(string SesionSeguridad, string Usuario, string Clave, string Nombre, string Descripcion, string eMail)
    {
        try
        {
            CeC_Sesion Sesion = null;

            if (SesionSeguridad != "SETUP")
            {
                Sesion = CeC_Sesion.Carga(SesionSeguridad);
                if (Sesion == null)
                    return -999;

            }
            if (CeC_Usuarios.ObtenUsuarioID(Usuario) > 0)
                return -9;

            int Intento = 1;
            string Dia = DateTime.Now.ToString("yyMMdd");
            int Ultimo = CeC_BD.EjecutaEscalarInt("SELECT COUNT(*) FROM EC_SUSCRIPCION WHERE SUBSTRING(SUSCRIPCION_NOMBRE,1,6) = '" + Dia + "'");
            if (Ultimo > 0)
                Intento += Ultimo;
            string SuscripcionNombre = Dia + Intento;
            int SuscripcionID = CeC_Suscripcion.CreaSuscripcion(Sesion, SuscripcionNombre, Nombre, "");
            if (SuscripcionID < 1)
                return SuscripcionID;
            int SesionID = 0;
            if (Sesion != null)
                SesionID = Sesion.SESION_ID;
            return CeC_Usuarios.AgregaUsuario(Usuario, 4, Nombre, Descripcion, eMail, Clave, SuscripcionID, SesionID, true, 0);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return -99999;
    }
    /// <summary>
    /// Crea un usuario para un empleado ya existente
    /// No valida Seguridad
    /// </summary>
    /// <param name="Usuario"></param>
    /// <param name="Clave"></param>
    /// <param name="Nombre"></param>
    /// <param name="Descripcion"></param>
    /// <param name="eMail"></param>
    /// <returns></returns>
    [OperationContract]
    public string CreaUsuarioEmpleado(string NoEmpleado, string Clave, string eMail, string Suscripcion)
    {
        int SuscripcionID = CeC_Suscripcion.ObtenSuscripcionID(Suscripcion);
        if (SuscripcionID <= 0)
            return "NO_SUSCRIPCION";

        string NombreUsuario = ObtenUsuarioNombreEmpleado(Suscripcion, NoEmpleado);
        int UsuarioID = CeC_Usuarios.ObtenUsuarioID(NombreUsuario);
        if (UsuarioID > 0)
            return "USUARIO_YA_EXISTE";
        int PersonaID = CeC_Personas.ObtenPersonaIDBySuscripcion(CeC.Convierte2Int(NoEmpleado), SuscripcionID);
        if (PersonaID <= 0)
            return "NO_EMPLEADO";

        UsuarioID = CeC_Usuarios.AgregaUsuario(NombreUsuario, 7, CeC_Personas.ObtenPersonaNombre(PersonaID), "", eMail, Clave, SuscripcionID, 0, false, PersonaID);
        if (UsuarioID > 0)
            return "OK";
        return "NOK";
    }
    /// <summary>
    /// Servicio que se encarga de recordar la contraseña dentro de el kiosko
    /// a un usuario que no recuerda su contraseña se le envia su contraseña via e-mail
    /// </summary>
    /// <param name="NoEmpleadoOeMail"></param>
    /// <param name="Suscripcion"></param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenUsuarioNombreEmpleado(string Suscripcion, string NoEmpleado)
    {
        string NombreUsuario = Suscripcion + "_" + NoEmpleado;
        return NombreUsuario;
    }

    [OperationContract]
    public string OlvidoClaveEmpleado(string NoEmpleado, string Suscripcion)
    {
        int SuscripcionID = CeC_Suscripcion.ObtenSuscripcionID(Suscripcion);
        if (SuscripcionID <= 0)
            return "NO_SUSCRIPCION";
        string NombreUsuario = ObtenUsuarioNombreEmpleado(Suscripcion, NoEmpleado);
        int UsuarioID = CeC_Usuarios.ObtenUsuarioID(NombreUsuario);
        if (UsuarioID > 0)
            if (CeC_Usuarios.EnviaPassword(UsuarioID))
                return "OK";
        return "NOK";
    }

    [OperationContract]
    public string OlvidoClave(string UsuarioOeMail, string Firma)
    {
        if (Firma != UsuarioOeMail)
            return "NOKF";
        int UsuarioID = CeC_Usuarios.ObtenUsuarioID(UsuarioOeMail);
        if (UsuarioID > 0)
            if (CeC_Usuarios.EnviaPassword(UsuarioID))
                return "OK";
        return "NOK";
    }

    [OperationContract]
    public string ObtenerUsuarioSincronizador(string SesionSeguridad, int SuscripcionID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return null;
        eClockBase.Modelos.Model_USUARIOS Usuario = new eClockBase.Modelos.Model_USUARIOS();
        Usuario.USUARIO_ID = CeC_Suscripcion.ObtenUsuarioID(SuscripcionID);
        return CeC_Tabla.ObtenDatos("EC_USUARIOS", "USUARIO_ID", JsonConvert.SerializeObject(Usuario) , Sesion);
    }
}
