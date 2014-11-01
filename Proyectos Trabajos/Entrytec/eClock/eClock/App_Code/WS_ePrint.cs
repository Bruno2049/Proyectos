using System;
using System.Web;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;

namespace eClock
{
    /// <summary>
    /// Descripción breve de WS_ePrint
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class WS_ePrint : System.Web.Services.WebService
    {
        enum EstadoImpresion
        {
            No_definido = 0,
            Pendiente = 1,
            Confirmado = 2,
            Imprimiendo = 3,
            Impreso = 4,
            Reimpresion = 5,
            No_credito = 6,
            Tipo_de_fuente_desconocida = 7,
            Empacando = 8,
            Enviado = 9,
            Entregado = 10
        }
        public DSImpresiones _dsImpresiones = new DSImpresiones();
        private DataSetIscard _DataSetIscard = new DataSetIscard();
        public WS_ePrint()
        {
        }
        /// <summary>
        /// Valida el Ussuario y el Password del usuario, que se toma de la tabla EC_USUARIOS
        /// </summary>
        /// <param name="Usuario">Identificador del Usuario</param>
        /// <param name="Password">Password del Usuario</param>
        /// <returns>Regersa el ID del usuario si la operación se realizo con exito, de lo contrario regresa -9999</returns>
        [WebMethod(Description = "Inicia sesión", EnableSession = true)]
        public int ValidarUsuario(string Usuario, string Password)
        {
            try
            {
                int UsuarioId = CeC_Sesion.ValidarUsuario(Usuario, Password);
                if (UsuarioId >= 0)
                {
                    if (CrearSesion(UsuarioId) > 0)
                        return UsuarioId;
                }
                return -9999;
            }
            catch (Exception ex)
            {
                return -9999;
            }
        }
        [WebMethod(Description = "Envia datos de Usuario", EnableSession = true)]
        public string ObtenNombreUsuario(int UsuarioID)
        {
            string Nombre = "";
            try
            { Nombre = (string)CeC_BD.EjecutaEscalar("SELECT USUARIO_NOMBRE FROM EC_USUARIOS WHERE (USUARIO_ID = " + UsuarioID.ToString() + ")"); }
            catch
            { return Nombre; }
            return Nombre;
        }
        [WebMethod(Description = "Envia datos de Usuario", EnableSession = true)]
        public string CambiaLinkComprarPuntos(string Link)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Descargar Imagen Diseño >> No ha iniciado Sesion");
                return null;
            }
            if (CeC_Config.LinkVerComprarPuntos != Link)
                return CeC_Config.LinkVerComprarPuntos;
            return Link;
        }

        [WebMethod(Description = "Envia datos de Usuario", EnableSession = true)]
        public string CambiaLinkDescargaArchivos(string Link)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Descargar Imagen Diseño >> No ha iniciado Sesion");
                return null;
            }
            if (CeC_Config.LinkDescargaArchivos != Link)
                return CeC_Config.LinkDescargaArchivos;
            return Link;
        }
        [WebMethod(Description = "Envia datos de Usuario", EnableSession = true)]
        public string CambiaLinkAltaUsuario(string Link)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Descargar Imagen Diseño >> No ha iniciado Sesion");
                return null;
            }
            if (CeC_Config.LinkAltaUsuario != Link)
                return CeC_Config.LinkAltaUsuario;
            return Link;
        }
        [WebMethod(Description = "Envia datos de Usuario", EnableSession = true)]
        public string CambiaLinkPaginAyuda(string Link)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Descargar Imagen Diseño >> No ha iniciado Sesion");
                return null;
            }
            if (CeC_Config.LinkPaginAyuda != Link)
                return CeC_Config.LinkPaginAyuda;
            return Link;
        }
        [WebMethod(Description = "Envia datos de Usuario", EnableSession = true)]
        public string CambiaLinkPropaganda(string Link)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Descargar Imagen Diseño >> No ha iniciado Sesion");
                return null;
            }
            if (CeC_Config.LinkPropaganda != Link)
                return CeC_Config.LinkPropaganda;
            return Link;
        }
        [WebMethod(Description = "Envia datos de Usuario", EnableSession = true)]
        public string CambiaLinkVerHistorial(string Link)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Descargar Imagen Diseño >> No ha iniciado Sesion");
                return null;
            }
            if (CeC_Config.LinkVerHistorial != Link)
                return CeC_Config.LinkVerHistorial;
            return Link;
        }
        [WebMethod(Description = "Envia datos de Usuario", EnableSession = true)]
        public string CambiaLinkVerSplash(string Link)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Descargar Imagen Diseño >> No ha iniciado Sesion");
                return null;
            }
            if (CeC_Config.LinkVerSplash != Link)
                return CeC_Config.LinkVerSplash;
            return Link;
        }
        /// <summary>
        /// Obtiene el Sesion_ID Actual en caso de haberse logeado
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "Obtiene la sesion actual", EnableSession = true)]
        public int ObtenSESION_ID()
        {
            try { return Convert.ToInt32(Session["SESION_ID"]); }
            catch { }
            return 0;
        }
        /// <summary>
        /// Crea una Sesion a partir de un Usuario en la tabla de EC_SESIONES
        /// </summary>
        /// <param name="UsuarioID">Identificador del Usuario</param>
        /// <returns>Regresa un Identificador de Sesion si la operacion se realizo con exito de lo contrario regresa -9999</returns>
        [WebMethod(Description = "Crea una Sesion a partir de un usuario", EnableSession = true)]
        private int CrearSesion(int UsuarioID)
        {
            int SesionID = CeC_Autonumerico.GeneraAutonumerico("EC_SESIONES", "SESION_ID");
            string qry = "INSERT INTO EC_SESIONES (SESION_ID, USUARIO_ID, SESION_INICIO_FECHAHORA) VALUES( " + SesionID.ToString() + "," + UsuarioID.ToString() + "," + CeC_BD.SqlFechaHora(DateTime.Now) + " )";
            int ret = CeC_BD.EjecutaComando(qry);
            int Sesion_ID = 0;
            if (ret <= 0)
            {
                Sesion_ID = -9999;
            }
            else
            {
                Sesion_ID = SesionID;
            }
            try
            {

                Session["SESION_ID"] = Sesion_ID;
                Session["USUARIO_ID"] = UsuarioID;
                Session["SUSCRIPCION_ID"] = CeC_BD.EjecutaEscalarInt("SELECT MIN(SUSCRIPCION_ID) FROM EC_PERMISOS_SUSCRIP  WHERE USUARIO_ID =" + UsuarioID);

            }
            catch { }
            return Sesion_ID;
        }
        [WebMethod(Description = "Obtiene el usuario ID", EnableSession = true)]
        private int USUARIO_ID()
        {

            try
            {

                return Convert.ToInt32(Session["USUARIO_ID"]);
            }
            catch { }
            return -1;


        }
        [WebMethod(Description = "Obtiene el GRUPO_1_ID", EnableSession = true)]
        private int GRUPO_1_ID()
        {

            try
            {

                return Convert.ToInt32(Session["GRUPO_1_ID"]);
            }
            catch { }
            return -1;
        }

        [WebMethod(Description = "Obtiene el SUSCRIPCION_ID", EnableSession = true)]
        private int SUSCRIPCION_ID()
        {

            try
            {

                return Convert.ToInt32(Session["SUSCRIPCION_ID"]);
            }
            catch { }
            return -1;
        }

        /// <summary>
        /// Descarga una imagen del diseño, si este no existe regresa null
        /// </summary>
        /// <param name="DisenoID">Identificador del diseño</param>
        /// <returns>En caso de que exista el diseño regresa un bitmap, en caso contrario regresa null</returns>
        [WebMethod(Description = "Descarga una imagen del diseño", EnableSession = true)]
        public byte[] DescargarImagenDiseno(int DisenoID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Descargar Imagen Diseño >> No ha iniciado Sesion");
                return null;
            }
            byte[] Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO_IMA");
            if (Datos != null)
                return Datos;
            else
                return null;
        }
        [WebMethod(Description = "Descarga una imagen del diseño", EnableSession = true)]
        public byte[] DescargarImagenAtrasDiseno(int DisenoID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Descargar Imagen Diseño >> No ha iniciado Sesion");
                return null;
            }
            byte[] Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO_IMA_REV");
            if (Datos != null)
                return Datos;
            else
                return null;
        }
        /// <summary>
        /// Descarga el diseño comprimido, si este no existe regresa null
        /// </summary>
        /// <param name="DisenoID">Identificador del diseño</param>
        /// <returns>En caso de que exista el diseño regresa un stream comprimido, de lo contrario regresa null</returns>
        [WebMethod(Description = "Descarga el diseño en Formato zISCard", EnableSession = true)]
        public byte[] DescargarDiseno(int DisenoID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Descargar Diseño >> No ha iniciado Sesion");
                return null;
            }
            byte[] Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO");
            if (Datos != null)
                return Datos;
            else
                return null;
        }
        [WebMethod(Description = "Descarga el ultimo diseño del usuario en formato zISCard", EnableSession = true)]
        public byte[] DescargarDisenoUsuario(string Usuario)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Decargar Diseño Usuario >> No ha iniciado Sesion");
                return null;
            }
            int UsuarioId = CeC_BD.EjecutaEscalarInt("SELECT EC_USUARIOS_DISENOS.USUARIO_ID FROM EC_USUARIOS_DISENOS INNER JOIN EC_USUARIOS ON EC_USUARIOS_DISENOS.USUARIO_ID=EC_USUARIOS.USUARIO_ID where EC_USUARIOS.USUARIO_USUARIO = '" + Usuario + "' AND EC_USUARIOS.USUARIO_BORRADO = '0'");
            if (UsuarioId != -9999)
            {
                int DisenoID = CeC_BD.EjecutaEscalarInt("SELECT MAX(DISENO_ID) FROM EC_USUARIOS_DISENOS WHERE USUARIO_ID = " + UsuarioId.ToString());
                return DescargarDiseno(DisenoID);
            }
            else
                return null;
        }
        [WebMethod(Description = "Guardar una imagen del diseño", EnableSession = true)]
        public bool CargarImagenDiseno(int DisenoID, int UsuarioID, byte[] DisenoImagen, byte[] DisenoImagenAtras)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Cargar Imagen Diseño >> No ha iniciado Sesion");
                return false;
            }
            string qry;
            int ret;
            if (DisenoImagen != null)
            {
                qry = "SELECT DISENO_ID FROM eC_DISENOS WHERE DISENO_ID = " + DisenoID.ToString();
                ret = CeC_BD.EjecutaEscalarInt(qry);
                if (ret == -9999)
                {
                    qry = "INSERT INTO eC_DISENOS(DISENO_ID) VALUES (" + DisenoID.ToString() + ")";
                    ret = CeC_BD.EjecutaComando(qry);
                    qry = "INSERT INTO EC_USUARIOS_DISENOS(USUARIO_ID,DISENO_ID) VALUES(" + UsuarioID.ToString() + "," + DisenoID.ToString() + ")";
                    ret = CeC_BD.EjecutaComando(qry);
                }
                if (CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO_IMA", DisenoImagen))
                {
                    if (CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO_IMA_REV", DisenoImagenAtras))
                        return true;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        [WebMethod(Description = "Guarda un diseño a partir de una tabla", EnableSession = true)]
        public int AgregarDiseno(DS_ePrint.DTDisenosDataTable TablaDiseno)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Agregar Diseño >> No ha iniciado Sesion");
                return -1;
            }
            string qry;
            int ret;
            try
            {
                DS_ePrint.DTDisenosRow FilaDiseno = TablaDiseno[0];
                int UsuarioID = Convert.ToInt32(FilaDiseno.USUARIO_ID);
                int DisenoID = CeC_Autonumerico.GeneraAutonumerico("EC_DISENOS", "DISENO_ID");
                DS_ePrintTableAdapters.DTADisenos DADisenos = new DS_ePrintTableAdapters.DTADisenos();
                ret = DADisenos.InsertDiseno(Convert.ToDecimal(DisenoID), FilaDiseno.DISENO_NOMBRE, FilaDiseno.DISENO_SQL, FilaDiseno.DISENO_F_CREADO, FilaDiseno.DISENO_F_EDITADO, 0);
                qry = "INSERT INTO EC_USUARIOS_DISENOS(USUARIO_ID,DISENO_ID,USUARIO_PROP) VALUES(" + UsuarioID.ToString() + "," + DisenoID.ToString() + "," + FilaDiseno.USUARIO_PROP + ")";
                ret = CeC_BD.EjecutaComando(qry);
                CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO", FilaDiseno.DISENO_DISENO);
                CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO_IMA", FilaDiseno.DISENO_DISENO_IMA);
                CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO_IMA_REV", FilaDiseno.DISENO_DISENO_IMA_REV);

                byte[] Thumbnail = GuardarThumbnail(FilaDiseno.DISENO_DISENO_IMA);
                CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO_THUMB", Thumbnail);
                return (int)DisenoID;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        [WebMethod(Description = "Actualiza un diseño a partir de una tabla", EnableSession = true)]
        public bool ActualizarDiseno(DS_ePrint.DTDisenosDataTable TablaDiseno)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Actualizar Diseño >> No ha iniciado Sesion");
                return false;
            }
            string qry;
            int ret;
            try
            {
                DS_ePrint.DTDisenosRow FilaDiseno = TablaDiseno[0];
                int UsuarioID = Convert.ToInt32(FilaDiseno.USUARIO_ID);
                int DisenoID = Convert.ToInt32(FilaDiseno.DISENO_ID);
                DS_ePrintTableAdapters.DTADisenos DADisenos = new DS_ePrintTableAdapters.DTADisenos();
                ret = DADisenos.UpdateDiseno(FilaDiseno.DISENO_NOMBRE, FilaDiseno.DISENO_SQL, FilaDiseno.DISENO_F_CREADO, FilaDiseno.DISENO_F_EDITADO, 0, FilaDiseno.DISENO_ID);
                CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO", FilaDiseno.DISENO_DISENO);
                CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO_IMA", FilaDiseno.DISENO_DISENO_IMA);
                CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO_IMA_REV", FilaDiseno.DISENO_DISENO_IMA_REV);
               
                byte[] Thumbnail = GuardarThumbnail(FilaDiseno.DISENO_DISENO_IMA);
                CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO_THUMB", Thumbnail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [WebMethod(Description = "Guarda un diseño comprimido", EnableSession = true)]
        public bool CargarDiseno(int DisenoID,int UsuarioID, byte[] Diseno)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Cargar Diseño >> No ha iniciado Sesion");
                return false;
            }
            string qry;
            int ret;
            if (Diseno != null)
            {
                qry = "SELECT DISENO_ID FROM eC_DISENOS WHERE DISENO_ID = " + DisenoID.ToString();
                ret = CeC_BD.EjecutaEscalarInt(qry);
                if (ret == -9999)
                {
                    qry = "INSERT INTO eC_DISENOS(DISENO_ID) VALUES (" + DisenoID.ToString() + ")";
                    ret = CeC_BD.EjecutaComando(qry);
                    qry = "INSERT INTO EC_USUARIOS_DISENOS(USUARIO_ID,DISENO_ID) VALUES(" + UsuarioID.ToString() + "," + DisenoID.ToString() + ")";
                    ret = CeC_BD.EjecutaComando(qry);
                }
                if (CeC_BD.AsignaBinario("EC_DISENOS", "DISENO_ID", DisenoID, "DISENO_DISENO", Diseno))
                    return true;
                else
                    return false;
            } 
            else
                return false;
        }
        [WebMethod(Description = "Devuelve un nuevo Identificador de Diseños", EnableSession = true)]
        public int DisenoID()
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Diseño ID >> No ha iniciado Sesion");
                return -9999;
            }
            return CeC_Autonumerico.GeneraAutonumerico("EC_DISENOS", "DISENO_ID");
        }
        [WebMethod(Description = "Obtiene el Data Set con las imagenes y campos que el usuario tiene",EnableSession=true)]
        public DS_ePrint.DTDisenosDataTable ObtenerTablaDisenos(int UsuarioID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Obtener Tabla de Diseños >> No ha iniciado Sesion");
                return null;
            }
            DS_ePrintTableAdapters.DTADisenos DADisenos = new DS_ePrintTableAdapters.DTADisenos();
            DS_ePrint.DTDisenosAuxiliarDataTable DTDisenosAuxiliar = new DS_ePrint.DTDisenosAuxiliarDataTable();
            byte[] Datos;
            DS_ePrint.DTDisenosDataTable DTDisenos = new DS_ePrint.DTDisenosDataTable();
            try
            {
                DADisenos.Fill(DTDisenosAuxiliar, Convert.ToDecimal(UsuarioID));
                if (DTDisenosAuxiliar != null)
                {
                    for (int i = 0; i < DTDisenosAuxiliar.Rows.Count; i++)
                    {
                        DS_ePrint.DTDisenosRow FilaNueva = DTDisenos.NewDTDisenosRow();
                        FilaNueva[DTDisenos.DISENO_IDColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_IDColumn.Caption];
                        FilaNueva[DTDisenos.DISENO_NOMBREColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_NOMBREColumn.Caption];
                        FilaNueva[DTDisenos.DISENO_F_CREADOColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_F_CREADOColumn.Caption];
                        FilaNueva[DTDisenos.DISENO_F_EDITADOColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_F_EDITADOColumn.Caption];
                        FilaNueva[DTDisenos.DISENO_SQLColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_SQLColumn.Caption];
                        FilaNueva[DTDisenos.USUARIO_DISENO_SLColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.USUARIO_DISENO_SLColumn.Caption];
                        FilaNueva[DTDisenos.USUARIO_IDColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.USUARIO_IDColumn.Caption];
                        FilaNueva[DTDisenos.USUARIO_PROPColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.USUARIO_PROPColumn.Caption];
                        Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", Convert.ToInt32(DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_IDColumn.Caption]), "DISENO_DISENO_THUMB");
                        if (Datos != null)
                            FilaNueva[DTDisenos.DISENO_DISENO_THUMBColumn.Caption] = Datos;
                        Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", Convert.ToInt32(DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_IDColumn.Caption]), "DISENO_DISENO_IMA");
                        if (Datos != null)
                            FilaNueva[DTDisenos.DISENO_DISENO_IMAColumn.Caption] = Datos;
                        //DTDisenos.AddDTDisenosRow(FilaNueva);
                        Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", Convert.ToInt32(DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_IDColumn.Caption]), "DISENO_DISENO_IMA_REV");
                        if (Datos != null)
                            FilaNueva[DTDisenos.DISENO_DISENO_IMA_REVColumn.Caption] = Datos;
                        DTDisenos.AddDTDisenosRow(FilaNueva);
                    }
                    return DTDisenos;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod(Description = "Obtiene el Data Set con las imagenes y campos que el usuario tiene", EnableSession = true)]
        public DS_ePrint.DTDisenosDataTable ObtenerTablaTodosDisenos(int UsuarioID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Obtener Tabla de Diseños >> No ha iniciado Sesion");
                return null;
            }
            DS_ePrintTableAdapters.DTADisenos DADisenos = new DS_ePrintTableAdapters.DTADisenos();
            DS_ePrint.DTDisenosAuxiliarDataTable DTDisenosAuxiliar = new DS_ePrint.DTDisenosAuxiliarDataTable();
            byte[] Datos;
            DS_ePrint.DTDisenosDataTable DTDisenos = new DS_ePrint.DTDisenosDataTable();
            try
            {
                DADisenos.FillByTodos(DTDisenosAuxiliar);
                if (DTDisenosAuxiliar != null)
                {
                    for (int i = 0; i < DTDisenosAuxiliar.Rows.Count; i++)
                    {
                        DS_ePrint.DTDisenosRow FilaNueva = DTDisenos.NewDTDisenosRow();
                        FilaNueva[DTDisenos.DISENO_IDColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_IDColumn.Caption];
                        FilaNueva[DTDisenos.DISENO_NOMBREColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_NOMBREColumn.Caption];
                        FilaNueva[DTDisenos.DISENO_F_CREADOColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_F_CREADOColumn.Caption];
                        FilaNueva[DTDisenos.DISENO_F_EDITADOColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_F_EDITADOColumn.Caption];
                        FilaNueva[DTDisenos.DISENO_SQLColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_SQLColumn.Caption];
                        FilaNueva[DTDisenos.USUARIO_DISENO_SLColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.USUARIO_DISENO_SLColumn.Caption];
                        FilaNueva[DTDisenos.USUARIO_IDColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.USUARIO_IDColumn.Caption];
                        FilaNueva[DTDisenos.USUARIO_PROPColumn.Caption] = DTDisenosAuxiliar[i][DTDisenosAuxiliar.USUARIO_PROPColumn.Caption];
                        Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", Convert.ToInt32(DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_IDColumn.Caption]), "DISENO_DISENO_THUMB");
                        if (Datos != null)
                            FilaNueva[DTDisenos.DISENO_DISENO_THUMBColumn.Caption] = Datos;
                        Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", Convert.ToInt32(DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_IDColumn.Caption]), "DISENO_DISENO_IMA");
                        if (Datos != null)
                            FilaNueva[DTDisenos.DISENO_DISENO_IMAColumn.Caption] = Datos;
                        //DTDisenos.AddDTDisenosRow(FilaNueva);
                        Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", Convert.ToInt32(DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_IDColumn.Caption]), "DISENO_DISENO_IMA_REV");
                        if (Datos != null)
                            FilaNueva[DTDisenos.DISENO_DISENO_IMA_REVColumn.Caption] = Datos;

                        Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", Convert.ToInt32(DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_IDColumn.Caption]), "DISENO_DISENO_THUMB");
                        if (Datos != null)
                            FilaNueva[DTDisenos.DISENO_DISENO_THUMBColumn.Caption] = Datos;
                        Datos = CeC_BD.ObtenBinario("EC_DISENOS", "DISENO_ID", Convert.ToInt32(DTDisenosAuxiliar[i][DTDisenosAuxiliar.DISENO_IDColumn.Caption]), "DISENO_DISENO_IMA");
                        if (Datos != null)
                            FilaNueva[DTDisenos.DISENO_DISENO_IMAColumn.Caption] = Datos;
                        DTDisenos.AddDTDisenosRow(FilaNueva);
                    }
                    return DTDisenos;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod(Description = "Obtiene los campos que son visibles de eClock", EnableSession = true)]
        public CEtiquetasCampos[] EtiquetasCampos()
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Etiquetas Campos >> No ha iniciado Sesion");
                return null;
            }
            CEtiquetasCampos[] _CamposEtiquetas;
            string[] NomCampos = CeC_Campos.ObtenListaCamposTE().Split(new char[] { ',' });
            _CamposEtiquetas = new CEtiquetasCampos[NomCampos.Length];
            for (int i = 0; i < NomCampos.Length; i++)
            {
                _CamposEtiquetas[i] = new CEtiquetasCampos(NomCampos[i].Trim(), CeC_Campos.ObtenEtiqueta(NomCampos[i].Trim()));
            }
            return _CamposEtiquetas;
        }
        [WebMethod(Description="Obtiene los nombres de los campos visible",EnableSession=true)]
        public ArrayList NombreCampos()
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Nombre Campos >> No ha iniciado Sesion");
                return null;
            }
            ArrayList ArrayCampos = new ArrayList();
            string[] NomCampos = CeC_Campos.ObtenListaCamposTE().Split(new char[] { ',' });
            for (int i = 0; i < NomCampos.Length; i++)
            {
                ArrayCampos.Add(NomCampos[i]);
            }
            return ArrayCampos;
        }
        [WebMethod(Description = "Obtiene la lista de empleados", EnableSession = true)]
        [SoapDocumentMethod]
        public DataSetIscard ObtenerEmpleados(int UsuarioID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Obtener Empleados >> No ha iniciado Sesion");
                return null;
            }
            _DataSetIscard.Conectar(CeC_BD.CadenaConexion(), "EC_PERSONAS_DATOS", CeC_Campos.ObtenListaCamposTE(), EtiquetasCampos(), CeC_Campos.ObtenCampoTELlave(), CeC_Config.FotografiaActiva, CeC_Config.FirmaActiva, UsuarioID);
            return _DataSetIscard;
        }
        [WebMethod(Description = "Obtiene la lista de empleados", EnableSession = true)]
        [SoapDocumentMethod]
        public DataSetIscard ObtenerEmpleadosporPersonaID(int UsuarioID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Obtener Empleados >> No ha iniciado Sesion");
                return null;
            }
            _DataSetIscard.Conectar(CeC_BD.CadenaConexion(), "EC_PERSONAS_DATOS", CeC_Campos.ObtenListaCamposTE(), EtiquetasCampos(), CeC_Campos.ObtenCampoTELlave(), CeC_Config.FotografiaActiva, CeC_Config.FirmaActiva, UsuarioID);
            return _DataSetIscard;
        }
        [WebMethod(Description = "Obtiene las restricciones que tiene el usuario", EnableSession = true)]
        public DS_ePrint.DTRestriccionesDataTable ObtenerRestricciones(int UsuarioID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Obtener Restricciones >> No ha iniciado Sesion");
                return null;
            }
            try
            {
                DS_ePrint.DTRestriccionesDataTable DTRestricciones = new DS_ePrint.DTRestriccionesDataTable();
                DS_ePrintTableAdapters.DTARestricciones DARestricciones = new DS_ePrintTableAdapters.DTARestricciones();
                DARestricciones.FillByUsuario(DTRestricciones, Convert.ToDecimal(UsuarioID));
                return DTRestricciones;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod(Description = "Actualiza un registro de un empleado", EnableSession = true)]
        public bool ActualizarRegistroEmpleado(DS_ePrint.DT_CamposModDataTable Campos, string NombreTabla, string CampoLlave)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Actualizar Registro Empleado >> No ha iniciado Sesion");
                return false;
            }
            int _PersonaID;
            string _Sentencia = "UPDATE " + NombreTabla + " SET ";
            int _IndiceLlave=0;
            
            for (int i = 0; i < Campos.Count; i++)
            {
                if (Campos[i].NombreCampo == CampoLlave)
                    _IndiceLlave = i;
                switch (Campos[i].Tipo)
                {
                    case "System.Decimal":
                    case "System.Int32":
                        if (i < Campos.Count - 1)
                            _Sentencia += Campos[i].NombreCampo + "=" + Campos[i].Valor.ToString() + ",";
                        else
                            _Sentencia += Campos[i].NombreCampo + "=" + Campos[i].Valor.ToString();
                        break;
                    case "System.String":
                        if (i < Campos.Count - 1)
                            _Sentencia += Campos[i].NombreCampo + "='" + Campos[i].Valor.ToString() + "',";
                        else
                            _Sentencia += Campos[i].NombreCampo + "='" + Campos[i].Valor.ToString() + "'";
                        break;
                    case "System.Byte[]":
                        if (i == Campos.Count - 1)
                            _Sentencia = _Sentencia.Remove(_Sentencia.Length - 1, 1);
                        _PersonaID = (Convert.ToInt32(Campos[_IndiceLlave].Valor));
                        if (((Array)Campos[i].Valor).Length > 1)
                        {
                            Array _Arreglo = (Array)Campos[i].Valor;
                            byte[] _ArregloBytes = new byte[_Arreglo.Length];
                            Array.Copy(_Arreglo, _ArregloBytes, _Arreglo.Length);
                            if (Campos[i].NombreCampo == "FOTOGRAFIA")
                                CeC_Personas.AsignaFoto(_PersonaID, _ArregloBytes);
                            if (Campos[i].NombreCampo == "FIRMA")
                                CeC_Personas.AsignaFirma(_PersonaID, _ArregloBytes);
                        }
                        else
                        {
                            if (Campos[i].NombreCampo == "FOTOGRAFIA")
                                CeC_Personas.BorraFoto(_PersonaID);
                            if (Campos[i].NombreCampo == "FIRMA")
                                CeC_Personas.BorraFirma(_PersonaID);

                        }
                        break;
                }
            }
            string ID = Campos[_IndiceLlave].Valor.ToString();
            _Sentencia += " WHERE " + CampoLlave + "=" + ID;
            
            int _ret = CeC_BD.EjecutaComando(_Sentencia);
            if (_ret > 0)
            {
                CeC_Sesion Sesion = new CeC_Sesion();
                CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "IsCard", Convert.ToInt32(ID), _Sentencia,0);
                return true;
            }
            return false;
        }
        [WebMethod(Description = "Actualiza un registro de un empleado", EnableSession = true)]
        public bool InsertaRegistroEmpleado(DS_ePrint.DT_CamposModDataTable Campos, string NombreTabla, string CampoLlave)
        {
            int Persona_ID = -1;
            int Persona_Link_ID = -1;
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Insertar Registro Empleado >> No ha iniciado Sesion");
                return false;
            }
            Array _ArregloFoto;
            byte[] _ArregloBytesFoto = new byte[1] { 0 };
            Array _ArregloFirma;
            byte[] _ArregloBytesFirma = new byte[1] { 0 };

            bool _TieneFoto = false;
            bool _TieneFirma = false;
            string _Sentencia = "INSERT INTO " + NombreTabla + "(";
            int _IndiceLlave = 0;
            int ValorLlave = -1;
            Persona_ID = CeC_Personas.Agrega(Convert.ToInt32(Campos[1].Valor), 1, SUSCRIPCION_ID());
            for (int i = 0; i < Campos.Count; i++)
            {
                if (Campos[i].Tipo != "System.Byte[]")
                {
                    if (Campos[i].NombreCampo == CampoLlave)
                        _IndiceLlave = i;
                    if (i < Campos.Count - 1)
                        _Sentencia += Campos[i].NombreCampo + ",";
                    else
                        _Sentencia += Campos[i].NombreCampo + ")";
                }
            }
            if(_Sentencia.EndsWith(","))
                _Sentencia = _Sentencia.Remove(_Sentencia.Length - 1, 1);
            _Sentencia += ") VALUES(";
            for (int i = 0; i < Campos.Count; i++)
            {
                switch (Campos[i].Tipo)
                {
                    case "System.Decimal":
                    case "System.Int32":
                        if (i < Campos.Count - 1)
                        {

                            if (Campos[i].NombreCampo == CampoLlave)
                            {

                                _Sentencia += Persona_ID.ToString() + ",";
                            }
                            else
                                _Sentencia += Campos[i].Valor + ",";
                        }
                        else
                            _Sentencia += Campos[i].NombreCampo + ")";
                        break;
                    case "System.String":
                        if (i < Campos.Count - 1)
                            _Sentencia += "'" + Campos[i].Valor + "',";
                        else
                            _Sentencia += "'" + Campos[i].NombreCampo + "')";
                        break;
                    case "System.Byte[]":
                        if (i == Campos.Count - 1)
                        {
                            _Sentencia = _Sentencia.Remove(_Sentencia.Length - 1, 1);
                            _Sentencia += ")";
                        }
                        if (((Array)Campos[i].Valor).Length > 1)
                        {
                            switch (Campos[i].NombreCampo)
                            {
                                case "FOTOGRAFIA":
                                    _TieneFoto = true;
                                    _ArregloFoto = (Array)Campos[i].Valor;
                                    _ArregloBytesFoto = new byte[_ArregloFoto.Length];
                                    Array.Copy(_ArregloFoto, _ArregloBytesFoto, _ArregloFoto.Length);
                                    break;
                                case "FIRMA":
                                    _TieneFirma = true;
                                    _ArregloFirma = (Array)Campos[i].Valor;
                                    _ArregloBytesFirma = new byte[_ArregloFirma.Length];
                                    Array.Copy(_ArregloFirma, _ArregloBytesFirma, _ArregloFirma.Length);
                                    break;
                            }
                        }
                        break;
                }
            }
            int _ret = CeC_BD.EjecutaComando(_Sentencia);
            if (_ret > 0)
            {
                /*Persona_Link_ID = ValorLlave;
                //     CeC_BD.CreaRelacionesEmpleados();
                
                Persona_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERSONAS", "PERSONA_ID");
                CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS (PERSONA_ID, PERSONA_LINK_ID, " +
                    " GRUPO_1_ID) VALUES(" + Persona_ID.ToString() + "," + Persona_Link_ID.ToString() + "," + GRUPO_1_ID().ToString() + ")");
                */


                //_PersonaID = CeC_Personas.ObtenPersonaID());
                if (_TieneFoto)
                    CeC_Personas.AsignaFoto(Persona_ID, _ArregloBytesFoto);
                if (_TieneFirma)
                    CeC_Personas.AsignaFirma(Persona_ID, _ArregloBytesFirma);
                return true;
            }
            return false;
        }

        [WebMethod(Description = "Inserta los datos de Facturacion de un usuario y regresa UsuarioDatosID", EnableSession = true)]
        public DSImpresiones.EC_US_DATDataTable ObtenDatosFacturacion(int UsuarioID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return null;
            }

            DSImpresionesTableAdapters.EC_US_DATTableAdapter taImpresiones = new DSImpresionesTableAdapters.EC_US_DATTableAdapter();
            return taImpresiones.GetDataByUsuarioID(UsuarioID);
        }

        [WebMethod(Description = "Inserta los datos de Facturacion de un usuario y regresa UsuarioDatosID", EnableSession = true)]
        public decimal InsertaDatosFacturacion(int UsuarioID, string RFC, string RazonSocial, string DireccionF1, string DireccionF2, string CiudadF, string EstadoF, string CodigoPF, string PaisF, string TelefonoF, string NombreEnvio, string DireccionE1, string DireccionE2, string CiudadE, string EstadoE, string CodigoPE, string PaisE, string MensajeriaE, string Comentario, string TelefonoE)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return -1;
            }


            decimal UsuarioDatosID = CeC_Autonumerico.GeneraAutonumerico("EC_US_DAT", "US_DAT_ID");
            string _Sentencia;
            _Sentencia = "INSERT INTO [EC_US_DAT]([US_DAT_ID] ,[USUARIO_ID] ,[US_DAT_RFC] ,[US_DAT_RS] ,[US_DAT_F_DIR1]," +
            "[US_DAT_F_DIR2],[US_DAT_F_CIUDAD] ,[US_DAT_F_EDO] ,[US_DAT_F_CP],[US_DAT_F_PAIS] ,[US_DAT_F_TELEFONO],[US_DAT_E_A]" +
            ",[US_DAT_E_DIR1] ,[US_DAT_E_DIR2] ,[US_DAT_E_CIUDAD],[US_DAT_E_EDO],[US_DAT_E_CP],[US_DAT_E_PAIS],[US_DAT_E_MENS]," +
            "[US_DAT_E_COMEN],[US_DAT_FECHAMOD],[US_DAT_E_TELEFONO])VALUES(" + UsuarioDatosID.ToString() + "," + UsuarioID.ToString() +
            ",'" + RFC + "','" + RazonSocial + "','" + DireccionF1 + "','" + DireccionF2 + "','" + CiudadF + "','" + EstadoF + "','" + CodigoPF + "'," +
            "'" + PaisF + "','" + TelefonoF + "','" + NombreEnvio + "','" + DireccionE1 + "','" + DireccionE2 + "','" + CiudadE + "','" + EstadoE + "','" + CodigoPE + "','" +
            PaisE + "','" + MensajeriaE + "','" + Comentario + "',GETDATE(),'" + TelefonoE + "')";
            int _ret = CeC_BD.EjecutaComando(_Sentencia);
            if (_ret > 0)
                return UsuarioDatosID;
            return -1;

        }

        [WebMethod(Description = "Inserta los datos de Facturacion de un usuario", EnableSession = true)]
        public bool ActualizaDatosFacturacion(decimal UsuarioDatoID, int UsuarioID, string RFC, string RazonSocial, string DireccionF1, string DireccionF2, string CiudadF, string EstadoF, string CodigoPF, string PaisF, string TelefonoF, string NombreEnvio, string DireccionE1, string DireccionE2, string CiudadE, string EstadoE, string CodigoPE, string PaisE, string MensajeriaE, string Comentario, string TelefonoE)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return false;
            }
            string _Sentencia;

            _Sentencia = "UPDATE [EC_US_DAT] SET [USUARIO_ID] = " + UsuarioID.ToString() + ",[US_DAT_RFC] = '" + RFC + " ',[US_DAT_RS] ='" + RazonSocial + "'," +
                         "[US_DAT_F_DIR1] = '" + DireccionF1 + "',[US_DAT_F_DIR2] = '" + DireccionF2 + "',[US_DAT_F_CIUDAD] = '" + CiudadF + "'," +
                         "[US_DAT_F_EDO] = '" + EstadoF + "',[US_DAT_F_CP] = '" + CodigoPF + "',[US_DAT_F_PAIS] = '" + PaisF + "'," +
                         "[US_DAT_F_TELEFONO] ='" + TelefonoF + "' ,[US_DAT_E_A] ='" + NombreEnvio + "',[US_DAT_E_DIR1] = '" + DireccionF1 + "'," +
                         "[US_DAT_E_DIR2] = '" + DireccionF2 + "',[US_DAT_E_CIUDAD] ='" + CiudadF + "',[US_DAT_E_EDO] = '" + EstadoF + "'," +
                         "[US_DAT_E_CP] = '" + CodigoPF + "',[US_DAT_E_PAIS] = '" + PaisE + "',[US_DAT_E_MENS] = '" + MensajeriaE + "'," +
                         "[US_DAT_E_COMEN] ='" + Comentario + "',[US_DAT_FECHAMOD] = GETDATE(),[US_DAT_E_TELEFONO] = '" + TelefonoE +
                         "' WHERE  [US_DAT_ID] =" + UsuarioDatoID.ToString();
            int _ret = CeC_BD.EjecutaComando(_Sentencia);
            if (_ret > 0)
                return true;
            return false;

        }
        [WebMethod(Description = "Borra la impresion en cola", EnableSession = true)]
        public bool BorraImpresionPendiente(int ImpresionID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return false;
            }

            string _Sentencia;
            _Sentencia = "Delete from [EC_US_IMPRESION] where [US_IMPRESION_ID] =  " + ImpresionID.ToString();

            int _ret = CeC_BD.EjecutaComando(_Sentencia);
            if (_ret > 0)
                return true;
            return false;

        }

        [WebMethod(Description = "Confirma la impresion en cola y hece el pedido", EnableSession = true)]
        public bool ActualizaImpresion(int PedidoID, int ImpresionID, decimal DiseñoID, int UsuarioID, decimal PersonaID, int Status, decimal TipoImpresion, decimal IFrente, decimal IReverso, decimal TipoTarjetaID, decimal NoImpresiones, int DatosUsuarioID)
        {

            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return false;
            }

            string _Sentencia;
            _Sentencia = "UPDATE EC_US_IMPRESION SET [TIPO_IMPRESION_ID]=" + ImpresionID.ToString() + "[PEDIDO_ID] =" + PedidoID.ToString() + " ,[ESTADO_IMPRESION_ID] =  " + Status.ToString() + ","
                        + "[TIPO_TARJETA_ID] = " + TipoTarjetaID.ToString() + " ,[DISENO_ID] =" + DiseñoID.ToString() + ",[USUARIO_ID] =  " + UsuarioID.ToString() + ",[PERSONA_ID] = " + PersonaID.ToString() + ","
                        + "[FECHA_IMPRESION] = GETDATE(),[US_IMPRESION_NO] = " + NoImpresiones.ToString() + ",[SESION_ID] = " + ObtenSESION_ID().ToString() + " ,"
                        + "[US_IMPRESION_FRENTE] = " + IFrente.ToString() + " ,[US_IMPRESION_REVER] = " + IReverso.ToString() + "WHERE US_IMPRESION_ID = " + ImpresionID.ToString();
            int _ret = CeC_BD.EjecutaComando(_Sentencia);
            if (_ret > 0)
                return true;
            return false;


        }
        [WebMethod(Description = "Actualiza un registro en un campo especifico", EnableSession = true)]
        public bool ActualizaImpresionporCampo(string Campo, string Valor, int US_IMPRESION_ID)
        {

            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return false;
            }

            string _Sentencia;
            _Sentencia = "UPDATE EC_US_IMPRESION SET " + Campo + " = " + Valor + " WHERE US_IMPRESION_ID = " + US_IMPRESION_ID.ToString();
            int _ret = CeC_BD.EjecutaComando(_Sentencia);
            if (_ret > 0)
                return true;
            return false;


        }
        [WebMethod(Description = "Imprime o Coloca la impresion en cola", EnableSession = true)]
        public bool ColocaImpresionPendiente(decimal DiseñoID, int UsuarioID, decimal PersonaID, int Status, decimal TipoImpresion, decimal PedidoID, decimal IFrente, decimal IReverso, decimal TipoTarjetaID, decimal NoImpresiones)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return false;
            }
            int ImpresionID = CeC_Autonumerico.GeneraAutonumerico("EC_US_IMPRESION", "US_IMPRESION_ID");
            string _Sentencia;
            _Sentencia = "INSERT INTO [EC_US_IMPRESION]([US_IMPRESION_ID] ,[TIPO_IMPRESION_ID] ,[PEDIDO_ID],[ESTADO_IMPRESION_ID],[TIPO_TARJETA_ID] " +
                         ",[DISENO_ID],[USUARIO_ID],[PERSONA_ID],[FECHA_IMPRESION],[US_IMPRESION_NO],[SESION_ID],[US_IMPRESION_FRENTE],[US_IMPRESION_REVER])" +
                         " VALUES ( " + ImpresionID.ToString() + "," + TipoImpresion.ToString() + "," + PedidoID.ToString() + "," + Status.ToString() + "," +
                         TipoTarjetaID.ToString() + "," + DiseñoID.ToString() + "," + UsuarioID.ToString() + "," + PersonaID.ToString() + ",GETDATE()," +
                         NoImpresiones.ToString() + "," + ObtenSESION_ID().ToString() + "," + IFrente.ToString() + "," + IReverso.ToString() + ")";

            int _ret = CeC_BD.EjecutaComando(_Sentencia);
            if (_ret > 0)
                return true;
            return false;

        }
        [WebMethod(Description = "Envia el estado de una impresion", EnableSession = true)]
        public int ObtenEstadoImpresion(int ImpresionID)
        {
            int _status = 0;
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Estado de Impresion>> No ha iniciado Sesion");
                return _status;
            }

            string _Sentencia;
            if (ImpresionID > 0)
            {
                _Sentencia = "SELECT ESTADO_IMPRESION_ID FROM EC_US_IMPRESION WHERE (US_IMPRESION_ID =" + ImpresionID.ToString() + ")";
                _status = (int)CeC_BD.EjecutaComando(_Sentencia);
                return _status;
            }

            return _status;

        }
        [WebMethod(Description = "Envia el estado de una impresion", EnableSession = true)]
        public decimal ObtenCostoEnvio(int TipoEnvio)
        {
            decimal _status = 0;
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Estado de Impresion>> No ha iniciado Sesion");
                return _status;
            }

            string _Sentencia;
            if (TipoEnvio > 0)
            {
                _Sentencia = "SELECT EC_PRODUCTOS.PRODUCTO_COSTO FROM EC_TIPO_ENVIO INNER JOIN EC_PRODUCTOS ON " +
                             "EC_TIPO_ENVIO.PRODUCTO_ID = EC_PRODUCTOS.PRODUCTO_ID WHERE(EC_TIPO_ENVIO.TIPO_ENVIO_ID = " + TipoEnvio.ToString() + ")";
                _status = CeC_BD.EjecutaEscalarDecimal(_Sentencia);
                return _status;
            }

            return _status;

        }
        [WebMethod(Description = "Envia un DataTable de Impresiones", EnableSession = true)]
        public DSImpresiones.ImpresionesDataTable ObtenImpresiones(int UsuarioID, int Status)
        {

            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return null;
            }
            DSImpresionesTableAdapters.ImpresionesTableAdapter TA_Impresiones = new DSImpresionesTableAdapters.ImpresionesTableAdapter();
            if (Status > 0)
                return TA_Impresiones.GetDataByEstadoImpresion(UsuarioID, Status);
            return null;
        }
        [WebMethod(Description = "Envia un DataTable de Impresiones que no coincida con el estado asginado", EnableSession = true)]
        public DSImpresiones.ImpresionesDataTable ObtenImpresionesNoEstado(int UsuarioID, int Status)
        {

            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return null;
            }
            DSImpresionesTableAdapters.ImpresionesTableAdapter TA_Impresiones = new DSImpresionesTableAdapters.ImpresionesTableAdapter();
            if (Status > 0)
                return TA_Impresiones.GetDataByUserEstadoDiferente(UsuarioID, Status);
            return null;
        }
        [WebMethod(Description = "Envia un DataTable de Impresiones ", EnableSession = true)]
        public DSImpresiones.ImpresionesDataTable ObtenerImpresionesProceso()
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return null;
            }
            DSImpresionesTableAdapters.ImpresionesTableAdapter TA_Impresiones = new DSImpresionesTableAdapters.ImpresionesTableAdapter();
            return TA_Impresiones.GetData();

        }

        [WebMethod(Description = "Envia un DataTable de Tipos de Tarjeta", EnableSession = true)]
        public DSImpresiones.EC_TIPO_TARJETADataTable ObtenTiposdeTarjeta()
        {

            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return null;
            }
            DSImpresionesTableAdapters.EC_TIPO_TARJETATableAdapter TA_Tarjetas = new DSImpresionesTableAdapters.EC_TIPO_TARJETATableAdapter();

            return TA_Tarjetas.GetData();

        }
        [WebMethod]
        public bool val_ePrint(DateTime FLocal, string[] MAC)
        {
            DateTime FechaExpira = Convert.ToDateTime(CeC_Config.FechaExpira_ePrint);
            if (FLocal <= FechaExpira)
                return true;
            return false;
        }
        [WebMethod(Description = "Envia un DataTable de Estados de Impresion", EnableSession = true)]
        public DSImpresiones.EC_ESTADO_IMPRESIONDataTable ObtenEstadosImpresion()
        {

            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return null;
            }
            DSImpresionesTableAdapters.EC_ESTADO_IMPRESIONTableAdapter TA_EstadosImpresion = new DSImpresionesTableAdapters.EC_ESTADO_IMPRESIONTableAdapter();

            return TA_EstadosImpresion.GetData();

        }
        [WebMethod(Description = "Envia un DataTable de Tipos de Envio", EnableSession = true)]
        public DSImpresiones.EC_TIPO_ENVIODataTable ObtenTiposdeEnvio()
        {

            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Imprimir online>> No ha iniciado Sesion");
                return null;
            }
            DSImpresionesTableAdapters.EC_TIPO_ENVIOTableAdapter TA_Tarjetas = new DSImpresionesTableAdapters.EC_TIPO_ENVIOTableAdapter();

            return TA_Tarjetas.GetData();

        }
        [WebMethod(Description = "Revisa la version de ePrint, y manda un mensaje de actualizacion en caso de requerirlo")]
        public bool MandaMensajeActualiza(string Version)
        {
            if (Version == CeC_Config.VersionePrint)
                return false;
            return true;
        }
        [WebMethod(Description = "Obtiene el PersonaID apartir del Grupo y el Usuario", EnableSession = true)]
        public decimal ObtenPersonaID(int UsuarioID, int Persona_Link_ID)
        {
            decimal _status = 0;
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Estado de Impresion>> No ha iniciado Sesion");
                return _status;
            }

            string _Sentencia;
            _Sentencia = "SELECT EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS INNER JOIN EC_USUARIOS_GRUPOS_1 ON EC_PERSONAS.GRUPO_1_ID = EC_USUARIOS_GRUPOS_1.GRUPO_1_ID WHERE (EC_USUARIOS_GRUPOS_1.USUARIO_ID = " + UsuarioID.ToString() + ") AND (EC_PERSONAS.PERSONA_LINK_ID = " + Persona_Link_ID.ToString() + ")";
            _status = (decimal)CeC_BD.EjecutaEscalar(_Sentencia);

            return _status;
        }
 

        [WebMethod(Description = "Actualiza un registro de un empleado", EnableSession = true)]
        public bool DesactivaRegistroEmpleado(DS_ePrint.DT_CamposModDataTable Campos, string NombreTabla, string CampoLlave)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Desactivar Registro Empleado >> No ha iniciado Sesion");
                return false;
            }
            string _Sentencia;
            int _IndiceLlave = 0;
            for (int i = 0; i < Campos.Count; i++)
            {
                if (Campos[i].NombreCampo == CampoLlave)
                    _IndiceLlave = i;
            }
            return CeC_Personas.Borra(Convert.ToInt32(Campos[_IndiceLlave].Valor), SUSCRIPCION_ID());

        }
        [WebMethod(Description = "Actualiza la foto empleado", EnableSession = true)]
        public bool ActualizarFoto(byte[] Foto, string ValorCampoLlave)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Actualizar Foto >> No ha iniciado Sesion");
                return false;
            }
            int PersonaID = CeC_Personas.ObtenPersonaID(Convert.ToInt32(ValorCampoLlave));
            return CeC_Personas.AsignaFoto(PersonaID, Foto);
        }

        [WebMethod(Description = "Obtiene la foto del empleado", EnableSession = true)]
        public byte[] ObtenFoto(int ValorCampoLlave)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Obten Foto >> No ha iniciado Sesion");
                return null;
            }
            int _PersonaID = Convert.ToInt32(ValorCampoLlave);
            byte[] Foto = CeC_Personas.ObtenFoto(_PersonaID);
            if(Foto.Length > 55000)
            {
                //return null;
                return GuardarThumbnail(Foto,300,400);
            }
            return Foto;
        }
        [WebMethod(Description = "Obtiene la firma del empleado", EnableSession = true)]
        public byte[] ObtenFirma(int ValorCampoLlave)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Obten Firma >> No ha iniciado Sesion");
                return null;
            }
            int _PersonaID = Convert.ToInt32(ValorCampoLlave);
            return CeC_Personas.ObtenFirma(_PersonaID);
        }

        [WebMethod(Description = "Obtiene la foto del empleado", EnableSession = true)]
        public byte[] ObtenFotoPersonaID(int PersonaID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Obten Foto >> No ha iniciado Sesion");
                return null;
            }
        
            return CeC_Personas.ObtenFoto(PersonaID);
        }

        [WebMethod(Description = "Obtiene la firma del empleado", EnableSession = true)]
        public byte[] ObtenFirmaPersonaID(int PersonaID)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Obten Firma >> No ha iniciado Sesion");
                return null;
            }

            return CeC_Personas.ObtenFirma(PersonaID);
        }
        
        [WebMethod(Description = "Obtiene una imagen especificada por el Nombre del Campo", EnableSession = true)]
        public byte[] ObtenTipoImagen(int ValorCampoLlave,string NombreCampo)
        {
            if (ObtenSESION_ID() <= 0)
            {
                CIsLog2.AgregaError("Obten Imagen >> No ha iniciado Sesion");
                return null;
            }
            int _PersonaID = CeC_Personas.ObtenPersonaID(ValorCampoLlave);
            switch (NombreCampo)
            {
                case "FOTOGRAFIA":
                    return CeC_Personas.ObtenTipoImagen(_PersonaID, CeC_Personas.TipoImagen.Foto);
                    break;
                case "HUELLA":
                    return CeC_Personas.ObtenTipoImagen(_PersonaID, CeC_Personas.TipoImagen.Huella);
                    break;
                case "FIRMA":
                    return CeC_Personas.ObtenTipoImagen(_PersonaID, CeC_Personas.TipoImagen.Firma);
                    break;
                case "DOCUMENTO":
                    return CeC_Personas.ObtenTipoImagen(_PersonaID, CeC_Personas.TipoImagen.Documento);
                    break;
            }
            return null;
            
        }
        private bool ThumbnailCallback()
        {
            return false;
        }
        private Bitmap Thumbnail(Bitmap bmp)
        {
            return Thumbnail(bmp, 32, 32);
            try
            {
                Image.GetThumbnailImageAbort Callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image ImagenFinal = bmp.GetThumbnailImage(32, 32, Callback, IntPtr.Zero);
                return (Bitmap)ImagenFinal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Bitmap Thumbnail(Bitmap bmp, int Ancho, int Alto)
        {
            try
            {
                Image.GetThumbnailImageAbort Callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image Thumbnail = bmp.GetThumbnailImage(Ancho, Alto, Callback, IntPtr.Zero);
                return (Bitmap)Thumbnail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private byte[] GuardarThumbnail(byte[] Imagen)
        {
            return GuardarThumbnail(Imagen, 32, 32);
        }
        private byte[] GuardarThumbnail(byte[] Imagen, int Ancho, int Alto)
        {
            MemoryStream stream = new MemoryStream();
            Bitmap ImagenOrigen = new Bitmap(new MemoryStream(Imagen));

            int AnchoFinal = Ancho;
            int AltoFinal = ImagenOrigen.Height * Ancho / ImagenOrigen.Width;
            if (AltoFinal > Alto)
            {
                AltoFinal = Alto;
                AnchoFinal = ImagenOrigen.Width * Alto / ImagenOrigen.Height;
            }


            Bitmap bmp = Thumbnail(ImagenOrigen, AnchoFinal, AltoFinal);
            bmp.Save(stream, ImageFormat.Jpeg);
            byte[] Datos = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(Datos, 0, (int)Datos.Length);
            return Datos;
        }
    }
    public class CCampo
    {
        
        private string _NombreCampo;
        private string _Tipo;
        private object _Valor;
        public CCampo(string NombreCampo, string Tipo, object Valor)
        {
            this.NombreCampo = NombreCampo;
            this.Tipo = Tipo;
            this.Valor = Valor;
        }
        public CCampo()
        {
        }
        [XmlElement(ElementName = "NombreCampo", Type = typeof(string))]
        [SoapElement(ElementName = "NombreCampo")]
        public string NombreCampo
        {
            get { return _NombreCampo; }
            set { _NombreCampo = value; }
        }
        [XmlElement(ElementName = "Tipo", Type = typeof(string))]
        [SoapElement(ElementName = "Tipo")]
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        [XmlElement(ElementName = "Valor", Type = typeof(object))]
        [SoapElement(ElementName = "Valor")]
        public object Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
    }
    [XmlRoot(ElementName = "Registro")]
    public class CRegsitro
    {
        private Collection<CCampo> _Campos = new Collection<CCampo>();
        [XmlElement(ElementName = "Campos", Type = typeof(Collection<CCampo>))]
        [SoapElement(ElementName = "Campos")]
        public Collection<CCampo> Campos
        {
            get { return _Campos; }
            set { _Campos = value; }
        }
    }
    public class CEtiquetasCampos
    {
        private string _NombreCampo;
        private string _EtiquetaCampo;
        public CEtiquetasCampos(string Nombre,string Etiqueta)
        {
            _NombreCampo = Nombre;
            _EtiquetaCampo = Etiqueta;
        }
        public CEtiquetasCampos()
        {
            _NombreCampo = string.Empty;
            _EtiquetaCampo = string.Empty;
        }
        [XmlElement(ElementName = "NombreCampo", Type = typeof(string))]
        [SoapElement(ElementName = "NombreCampo")]
        public string NombreCampo
        {
            get { return _NombreCampo; }
            set { _NombreCampo = value; }
        }
        [XmlElement(ElementName = "EtiquetaCampo", Type = typeof(string))]
        [SoapElement(ElementName = "EtiquetaCampo")]
        public string EtiquetaCampo
        {
            get { return _EtiquetaCampo; }
            set { _EtiquetaCampo = value; }
        }
    }
    [XmlRoot(ElementName = "DataSetIscard")]
    public class DataSetIscard
    {
        public DataSetIscard()
        {
        }
        private DataSet _DataSetEmpleados = new DataSet();
        private OleDbConnection _Conexion = new OleDbConnection();
        private OleDbCommand _ComandoSelect = new OleDbCommand();
        private OleDbDataAdapter _Adaptador = new OleDbDataAdapter();
        private CEtiquetasCampos[] _EtiquetasCampos;
        private string _Campos;
        private string _NombreTabla;
        private string _CampoLlave;
        private bool _PermitirFoto;
        private bool _PermitirFirma;
        [XmlIgnore]
        [SoapIgnore]
        public OleDbDataAdapter Adaptador
        {
            get { return _Adaptador; }
            set { _Adaptador = value; }
        }
        [XmlElement(ElementName = "DataSetEmpleados", Type = typeof(DataSet))]
        [SoapElement(ElementName = "DataSetEmpleados")]
        public DataSet DataSetEmpleados
        {
            get { return _DataSetEmpleados; }
            set { _DataSetEmpleados = value; }
        }
        [XmlElement(ElementName = "Campos", Type = typeof(string))]
        [SoapElement(ElementName = "Campos")]
        public string Campos
        {
            get { return _Campos; }
            set { _Campos = value; }
        }
        [XmlElement(ElementName = "NombreTabla", Type = typeof(string))]
        [SoapElement(ElementName = "NombreTabla")]
        public string NombreTabla
        {
            get { return _NombreTabla; }
            set { _NombreTabla = value; }
        }
        [XmlElement(ElementName = "CampoLlave", Type = typeof(string))]
        [SoapElement(ElementName = "CampoLlave")]
        public string CampoLlave
        {
            get { return _CampoLlave; }
            set { _CampoLlave = value; }
        }
        [XmlElement(ElementName = "PermitirFoto", Type = typeof(Boolean))]
        [SoapElement(ElementName = "PermitirFoto")]
        public bool PermitirFoto
        {
            get { return _PermitirFoto; }
            set { _PermitirFoto = value; }
        }
        [XmlElement(ElementName = "PermitirFirma", Type = typeof(Boolean))]
        [SoapElement(ElementName = "PermitirFirma")]
        public bool PermitirFirma
        {
            get { return _PermitirFirma; }
            set { _PermitirFirma = value; }
        }
        [XmlElement(ElementName = "EtiquetasCampos", Type = typeof(CEtiquetasCampos))]
        [SoapElement(ElementName = "EtiquetasCampos")]
        public CEtiquetasCampos[] EtiquetasCampos
        {
            get { return _EtiquetasCampos; }
            set { _EtiquetasCampos = value; }
        }
        public bool ConectarporPersonaID(string CadenaConexion, string NombreTabla, string Campos, CEtiquetasCampos[] EtiquetasCampos, string CampoLlave, bool PermitirFoto, bool PermitirFrma, int PersonaID)
        {
            try
            {
                _NombreTabla = NombreTabla;
                _Campos = Campos;
                _CampoLlave = CampoLlave;
                _PermitirFoto = PermitirFoto;
                _PermitirFirma = PermitirFrma;
                _EtiquetasCampos = EtiquetasCampos;
                _Conexion.ConnectionString = CadenaConexion;
                _ComandoSelect.Connection = _Conexion;
                _Adaptador.SelectCommand = _ComandoSelect;
                _Adaptador.SelectCommand.CommandText = "SELECT " +
                    CamposTabla(_Campos, _NombreTabla) +
                    " FROM " + _NombreTabla +
                    " INNER JOIN EC_PERSONAS ON " + _NombreTabla + "." + _CampoLlave + "=EC_PERSONAS.PERSONA_LINK_ID" +
                    " INNER JOIN EC_USUARIOS_GRUPOS_1 ON EC_PERSONAS.GRUPO_1_ID=EC_USUARIOS_GRUPOS_1.GRUPO_1_ID" +
                    " WHERE EC_PERSONAS.PERSONA_ID = " + PersonaID.ToString() +
                    " AND EC_PERSONAS.PERSONA_BORRADO=0";
                //" WHERE EC_PERSONAS.PERSONA_BORRADO=0";
                //" AND EC_PERSONAS.PERSONA_BORRADO=0";
                _Conexion.Open();
                _Adaptador.Fill(_DataSetEmpleados, _NombreTabla);
                _Conexion.Close();
                LlenarEtiquetasDataSet();
                if (_PermitirFoto)
                    _DataSetEmpleados.Tables[_NombreTabla].Columns.Add("FOTOGRAFIA", typeof(byte[]));
                if (_PermitirFirma)
                    _DataSetEmpleados.Tables[_NombreTabla].Columns.Add("FIRMA", typeof(byte[]));
                return true;
            }
            catch (Exception ex)
            {
                if (_Conexion.State == System.Data.ConnectionState.Open)
                    _Conexion.Close();
                return false;
            }
        }
        public bool Conectar(string CadenaConexion, string NombreTabla, string Campos, CEtiquetasCampos[] EtiquetasCampos, string CampoLlave, bool PermitirFoto,bool PermitirFrma,int UsuarioID)
        {
            try
            {
                _NombreTabla = NombreTabla;
                _Campos = Campos;
                _CampoLlave = CampoLlave;
                _PermitirFoto = PermitirFoto;
                _PermitirFirma = PermitirFrma;
                _EtiquetasCampos = EtiquetasCampos;
                _Conexion.ConnectionString = CadenaConexion;
                _ComandoSelect.Connection = _Conexion;
                _Adaptador.SelectCommand = _ComandoSelect;



                _Adaptador.SelectCommand.CommandText = "SELECT " +
                    CamposTabla(_Campos, "") +
                    " FROM " + _NombreTabla +
                    " INNER JOIN EC_PERSONAS ON " + _NombreTabla + "." + _CampoLlave + "=EC_PERSONAS.PERSONA_ID" +
                    //" INNER JOIN EC_PERMISOS_SUSCRIP ON EC_PERSONAS.SUSCRIPCION_ID=EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID" +
                    //" WHERE EC_PERMISOS_SUSCRIP.USUARIO_ID = " + UsuarioID.ToString() +
                    //" AND EC_PERSONAS.PERSONA_BORRADO=0";
                    " WHERE EC_PERSONAS.PERSONA_BORRADO=0 AND EC_PERSONAS.PERSONA_ID IN (" + CeC_Personas.ObtenQryPersonasdeUsuario(UsuarioID) + ")";
                
                    //" AND EC_PERSONAS.PERSONA_BORRADO=0";
                _Conexion.Open();
                _Adaptador.Fill(_DataSetEmpleados, _NombreTabla);
                _Conexion.Close();
                LlenarEtiquetasDataSet();
                if (_PermitirFoto)
                    _DataSetEmpleados.Tables[_NombreTabla].Columns.Add("FOTOGRAFIA", typeof(byte[]));
                if(_PermitirFirma)
                    _DataSetEmpleados.Tables[_NombreTabla].Columns.Add("FIRMA", typeof(byte[]));
                return true;
            }
            catch (Exception ex)
            {
                if (_Conexion.State == System.Data.ConnectionState.Open)
                    _Conexion.Close();
                return false;
            }
        }
        private string CamposTabla(string Campos, string NombreTabla)
        {
            string _CamposTabla = "";
            string[] _Campos = Campos.Split(new char[] { ',' });
            if (NombreTabla != "")
                NombreTabla += ".";
            for (int i = 0; i < _Campos.Length; i++)
            {
                if (i < _Campos.Length - 1)
                    _CamposTabla = _CamposTabla + NombreTabla + _Campos[i] + ",";
                else
                    _CamposTabla = _CamposTabla + NombreTabla + _Campos[i];
            }
            return _CamposTabla.Replace(" ","");
        }
        private void LlenarEtiquetasDataSet()
        {
            int cont = 0;
            while (cont < _DataSetEmpleados.Tables[_NombreTabla].Columns.Count)
            {
                for (int i = 0; i < _EtiquetasCampos.Length; i++)
                {
                    if (_DataSetEmpleados.Tables[_NombreTabla].Columns[cont].ColumnName == _EtiquetasCampos[i].NombreCampo.Trim())
                    {
                        _DataSetEmpleados.Tables[_NombreTabla].Columns[cont].Caption = _EtiquetasCampos[i].EtiquetaCampo;
                        break;
                    }
                }
                cont++;
            }
        }
    }
}