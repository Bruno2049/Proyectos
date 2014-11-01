using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using eClockBase;
using System.Data.Odbc;
using System.Data;
using System.Data.OleDb;
using Newtonsoft.Json;

namespace eClock5.Vista.Migrar
{
    /// <summary>
    /// Lógica de interacción para Anviz.xaml
    /// </summary>
    public partial class Anviz : UserControl
    {
        CeC_SesionBase Sesion;
        
        //private OleDbConnection Conexion;
        OpenFileDialog ArchivoBD = new OpenFileDialog();
        private OleDbConnection Conexion;

        public Anviz()
        {
            InitializeComponent();
        }

        private void Btn_Examinar_Click(object sender, RoutedEventArgs e)
        {
            ArchivoBD.Filter = "Archivos  Aceptados |*.mdb;";
            ArchivoBD.ShowDialog();
            Tbx_RutaDeLaCarpeta.Text = ArchivoBD.FileName;
        }
        #region Probar Conexion
        /// <summary>
        /// Prueba la conexion con el archivo de base de datos
        /// </summary>
        /// <param name="Ruta"></param>
        private void ProbarConexion(string Ruta)
        {
            OleDbConnection Conexion = new OleDbConnection();
            // TODO: Modify the connection string and include any
            // additional required properties for your database.
            Conexion.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"User ID=;" + Tbx_Usuario.Text +
                @"Password=;" + Tbx_Contraseña.Text +
                @"Data source=" + Ruta;
                //@"Data source= C:\Documents and Settings\username\" +
                //@"My Documents\AccessFile.mdb";
            try
            {
                Conexion.Open();
                MessageBox.Show("Conexión exitosa.");
                // Insert code to process data.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo al conectar con el origen de datos.");
                CeC_Log.AgregaError(ex);
            }
            finally
            {
                Conexion.Close();
            }
        }
        #endregion
        private void Lbl_Siguiente_Click(object sender, RoutedEventArgs e)
        {
            Migrar();
        }
        /// <summary>
        /// Contiene los métodos para migrar los Turnos, Empleados, Terminales, Tipos de Incidencias, Accesos, Justificaciones
        /// de eClock Básico (Anviz) a eClock5
        /// </summary>
        private void Migrar()
        {
            Sesion = CeC_Sesion.ObtenSesion(this);
            OleDbConnection Conexion = new OleDbConnection();
            try
            {
                //String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Att\\Att2003.mdb";
                String CadenaConexion = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                                        @"User ID=;" + Tbx_Usuario.Text +
                                        @"Password=;" + Tbx_Contraseña.Text +
                                        @"Data source=" + this.ArchivoBD.FileName;

                if (Conexion.State != ConnectionState.Open)
                {
                    Conexion.ConnectionString = CadenaConexion;
                    Conexion.Open();
                }
                ImportarTurnos(Conexion);
                ImportarTerminales(Conexion);
                ImportarIncidencias(Conexion);
                ImportarTiposIncidencias(Conexion);
                ImportarEmpleados(Conexion);
                ImportarAccesos(Conexion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo al conectar con el origen de datos.");
                CeC_Log.AgregaError(ex);
            }
            finally
            {
                Conexion.Close();
            }
        }

        private void ImportarAccesos(OleDbConnection Conexion)
        {
            DataSet DS_AccesosAnviz = new DataSet();
            eClockBase.Modelos.Accesos.Model_AccesosAgregar Accesos = new eClockBase.Modelos.Accesos.Model_AccesosAgregar();
            List<eClockBase.Modelos.Accesos.Model_AccesosAgregar> AccesosAgregar = new List<eClockBase.Modelos.Accesos.Model_AccesosAgregar>();
            try
            {
                String QryAccesosAnviz = "SELECT * FROM Checkinout ";
                //String QryAccesosAnviz = "SELECT * FROM Checkinout WHERE (CheckTime >= #8/18/2010#) AND (CheckTime <= #8/19/2010#)";
                //Creamos un dataadapter donde depositaremos los resultados.
                OleDbDataAdapter AdaptadorAccesos = new OleDbDataAdapter(QryAccesosAnviz, Conexion);
                AdaptadorAccesos.Fill(DS_AccesosAnviz);

                if (DS_AccesosAnviz == null || DS_AccesosAnviz.Tables.Count < 1 || DS_AccesosAnviz.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Sin datos en el origen");
                    return;
                }
                //eClockBase.Controladores.Personas
                foreach (DataRow DR in DS_AccesosAnviz.Tables[0].Rows)
                {
                    Accesos.PERSONA_LINK_ID = CeC.Convierte2Int(DR["Userid"]);
                    Accesos.TERMINAL_ID = CeC.Convierte2Int(DR["Sensorid"]);
                    Accesos.ACCESO_FECHAHORA = CeC.Convierte2DateTime(DR["CheckTime"]);
                    //Accesos.TIPO_ACCESO = CeC.Convierte2String(DR["CheckType"]);
                    
                    AccesosAgregar.Add(Accesos);
                    Accesos = new eClockBase.Modelos.Accesos.Model_AccesosAgregar();
                }
                eClockBase.Controladores.Accesos ControladorAccesos = new eClockBase.Controladores.Accesos(Sesion);
                ControladorAccesos.AgregarAccesosEvent += ControladorAccesos_AgregarAccesosEvent;
                ControladorAccesos.AgregarAccesos(AccesosAgregar);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo al importar datos de los Accesos.");
                CeC_Log.AgregaError(ex);
            }
        }

        void ControladorAccesos_AgregarAccesosEvent(bool Guardado)
        {
            //throw new NotImplementedException();
        }
        /// <summary>
        /// Importa los datos de las Justificaciones de eClock Básico (Anviz) a eClock5
        /// </summary>
        /// <param name="Ruta">Ruta donde se encuentra el archivo de BD del eClock Básico</pa
        private void ImportarIncidencias(OleDbConnection Conexion)
        {
            eClockBase.Modelos.Incidencias.Model_Incidencias Incidencia = new eClockBase.Modelos.Incidencias.Model_Incidencias();
            List<eClockBase.Modelos.Incidencias.Model_Incidencias> IncidenciasAgregar = new List<eClockBase.Modelos.Incidencias.Model_Incidencias>();
            DataSet DS_JustificacioneslesAnviz = new DataSet();

            try
            {
                String QryJustificacionesAnviz = "SELECT * FROM UserLeave";

                //Creamos un dataadapter donde depositaremos los resultados.
                OleDbDataAdapter AdaptadorJustificaciones = new OleDbDataAdapter(QryJustificacionesAnviz, Conexion);

                AdaptadorJustificaciones.Fill(DS_JustificacioneslesAnviz);
                if (DS_JustificacioneslesAnviz == null || DS_JustificacioneslesAnviz.Tables.Count < 1 || DS_JustificacioneslesAnviz.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Sin datos en el origen");
                    return;
                }

                foreach (DataRow Justificacion_Anviz in DS_JustificacioneslesAnviz.Tables[0].Rows)
                {
                    Incidencia.Persona_Link_ID = CeC.Convierte2Int(Justificacion_Anviz["Userid"]);
                    Incidencia.FInicio = CeC.Convierte2DateTime(Justificacion_Anviz["BeginTime"]);
                    Incidencia.FFin = CeC.Convierte2DateTime(Justificacion_Anviz["EndTime"]);
                    Incidencia.TipoIncidenciaID = CeC.Convierte2Int(Justificacion_Anviz["LeaveClassid"]);
                    Incidencia.Comentario = CeC.Convierte2String(Justificacion_Anviz["Whys"]);                    
                    
                    IncidenciasAgregar.Add(Incidencia);
                    Incidencia = new eClockBase.Modelos.Incidencias.Model_Incidencias();
                }
                eClockBase.Controladores.Incidencias ControladorIncidencias = new eClockBase.Controladores.Incidencias(Sesion);
                ControladorIncidencias.CargarIncidenciasEvent += ControladorIncidencias_CargarIncidenciasEvent;
                ControladorIncidencias.CargarIncidencias(IncidenciasAgregar);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo al importar datos de las Justificaciones.");
                CeC_Log.AgregaError(ex);
            }
        }

        void ControladorIncidencias_CargarIncidenciasEvent(bool Guardado)
        {

            //throw new NotImplementedException();
        }
        private void ImportarEmpleados(OleDbConnection Conexion)
        {
            
            DataSet DS_Empleados_Anviz = new DataSet();
            try
            {
                string QryEmpleadosAnviz = "SELECT * FROM Userinfo";
                OleDbDataAdapter AdaptadorEmpleadosAnviz = new OleDbDataAdapter(QryEmpleadosAnviz, Conexion);

                AdaptadorEmpleadosAnviz.Fill(DS_Empleados_Anviz);
                if (DS_Empleados_Anviz == null || DS_Empleados_Anviz.Tables.Count < 1 || DS_Empleados_Anviz.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Sin datos en el origen");
                    return;
                }
                eClockBase.Controladores.Personas ControladorPersonas = new eClockBase.Controladores.Personas(Sesion);
                ControladorPersonas.AgregarPersonasEvent += ControladorPersonas_AgregarPersonasEvent;
                ControladorPersonas.AgregarPersonas(eClock5.BaseModificada.CeC_BD.DataSet2JsonList(DS_Empleados_Anviz));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo al importar datos de los Tipos de Incidencias.");
                CeC_Log.AgregaError(ex);
            }
        }

        void ControladorPersonas_AgregarPersonasEvent(bool Guardado)
        {
            //throw new NotImplementedException();
        }

        private void ImportarTiposIncidencias(OleDbConnection Conexion)
        {
            eClockBase.Modelos.Model_TIPO_INCIDENCIAS EC_TIPO_INCIDENCIAS = new eClockBase.Modelos.Model_TIPO_INCIDENCIAS();
            DataSet DS_TipoIncidencias = new DataSet();
            try
            {
                string QryTipoIncidencias = "SELECT * FROM LeaveClass";
                OleDbDataAdapter AdaptadorTipoIncidencias = new OleDbDataAdapter(QryTipoIncidencias, Conexion);

                AdaptadorTipoIncidencias.Fill(DS_TipoIncidencias);
                if (DS_TipoIncidencias == null || DS_TipoIncidencias.Tables.Count < 1 || DS_TipoIncidencias.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Sin datos en el origen");
                    return;
                }
                foreach (DataRow TipoIncidencias_Anviz in DS_TipoIncidencias.Tables[0].Rows)
                {
                    EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID = -1;
                    EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_NOMBRE = CeC.Convierte2String(TipoIncidencias_Anviz["Classname"]);
                    EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ABR = "";
                    EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_BORRADO = false;
                    EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_COLOR = 0;
                    EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_AGRUPADOR = null;

                    eClockBase.Controladores.TipoIncidencias ControladorTipoIncidencias = new eClockBase.Controladores.TipoIncidencias(Sesion);
                    ControladorTipoIncidencias.GuardadoEvent += ControladorTipoIncidencias_GuardadoEvent;
                    ControladorTipoIncidencias.Guardar(EC_TIPO_INCIDENCIAS);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo al importar datos de los Tipos de Incidencias.");
                CeC_Log.AgregaError(ex);
            }
        }

        void ControladorTipoIncidencias_GuardadoEvent(bool Guardado)
        {
            //throw new NotImplementedException();
        }
        
        #region Importar Terminales
        /// <summary>
        /// Importa los datos de las Terminales de eClock Básico (Anviz) a eClock5
        /// </summary>
        /// <param name="Ruta">Ruta donde se encuentra el archivo de BD del eClock Básico</param>
        private void ImportarTerminales(OleDbConnection Conexion)
        {
            eClockBase.Modelos.Model_TERMINALES EC_TERMINALES = new eClockBase.Modelos.Model_TERMINALES();
            DataSet DS_TerminalesAnviz = new DataSet();

            try
            {
                String QryTerminalesAnviz = "SELECT * FROM FingerClient";

                //Creamos un dataadapter donde depositaremos los resultados.
                OleDbDataAdapter AdaptadorTurnos = new OleDbDataAdapter(QryTerminalesAnviz, Conexion);

                AdaptadorTurnos.Fill(DS_TerminalesAnviz);
                if(DS_TerminalesAnviz == null || DS_TerminalesAnviz.Tables.Count < 1 || DS_TerminalesAnviz.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Sin datos en el origen");
                    return;
                }

                foreach(DataRow Terminal_Anviz in DS_TerminalesAnviz.Tables[0].Rows)
                {
                    EC_TERMINALES.TERMINAL_ID = -1;
                    EC_TERMINALES.TIPO_TERMINAL_ACCESO_ID = 1;
                    EC_TERMINALES.TERMINAL_NOMBRE = CeC.Convierte2String(Terminal_Anviz["ClientName"]);
                    EC_TERMINALES.ALMACEN_VEC_ID = 5;
                    EC_TERMINALES.SITIO_ID = 1;
                    EC_TERMINALES.MODELO_TERMINAL_ID = 22;
                    EC_TERMINALES.TIPO_TECNOLOGIA_ID = 5;
                    EC_TERMINALES.TIPO_TECNOLOGIA_ADD_ID = 0;
                    EC_TERMINALES.TERMINAL_SINCRONIZACION = 5;
                    EC_TERMINALES.TERMINAL_CAMPO_LLAVE = "";
                    EC_TERMINALES.TERMINAL_CAMPO_ADICIONAL = "";
                    EC_TERMINALES.TERMINAL_ENROLA = false;
                    EC_TERMINALES.TERMINAL_DIR = CeC.Convierte2String(Terminal_Anviz["IPaddress"]);
                    EC_TERMINALES.TERMINAL_ASISTENCIA = true;
                    EC_TERMINALES.TERMINAL_COMIDA = false;
                    EC_TERMINALES.TERMINAL_MINUTOS_DIF = 0;
                    EC_TERMINALES.TERMINAL_VALIDAHORARIOE = true;
                    EC_TERMINALES.TERMINAL_VALIDAHORARIOS = true;
                    EC_TERMINALES.TERMINAL_BORRADO = false;
                    EC_TERMINALES.TERMINAL_DESCRIPCION = "";
                    EC_TERMINALES.TERMINAL_BIN = null;
                    EC_TERMINALES.TERMINAL_MODELO = CeC.Convierte2String(Terminal_Anviz["MachineType"]);
                    EC_TERMINALES.TERMINAL_NO_SERIE = "";
                    EC_TERMINALES.TERMINAL_FIRMWARE_VER = "";
                    EC_TERMINALES.TERMINAL_NO_HUELLAS = 0;
                    EC_TERMINALES.TERMINAL_NO_EMPLEADOS = 0;
                    EC_TERMINALES.TERMINAL_NO_TARJETAS = 0;
                    EC_TERMINALES.TERMINAL_NO_ROSTROS = 0;
                    EC_TERMINALES.TERMINAL_NO_CHECADAS = 0;
                    EC_TERMINALES.TERMINAL_NO_PALMAS = 0;
                    EC_TERMINALES.TERMINAL_NO_IRIS = 0;
                    EC_TERMINALES.TERMINAL_GARANTIA_INICIO = CeC.FechaNula;
                    EC_TERMINALES.TERMINAL_GARANTIA_FIN = CeC.FechaNula;
                    EC_TERMINALES.TERMINAL_AGRUPACION = "";

                    eClockBase.Controladores.Terminales ControladorTerminal = new eClockBase.Controladores.Terminales(Sesion);
                    ControladorTerminal.GuardadoEvent += ControladorTerminal_GuardadoEvent;
                    ControladorTerminal.Guardar(EC_TERMINALES);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo al importar datos de las Terminales.");
                CeC_Log.AgregaError(ex);
            }
        }

        void ControladorTerminal_GuardadoEvent(bool Guardado)
        {
            //throw new NotImplementedException();
        }
        #endregion
        #region Importar Turnos
        /// <summary>
        /// Importa los datos del turno de eClock Básico (Anviz) a eClock5
        /// </summary>
        /// <param name="Ruta">Ruta donde se encuentra el archivo de BD del eClock Básico</param>
        private void ImportarTurnos(OleDbConnection Conexion)
        {
            eClockBase.Modelos.Turnos.Model_Turno EC_TURNOS = new eClockBase.Modelos.Turnos.Model_Turno();
            eClockBase.Modelos.Model_TURNOS_DIA EC_TURNOS_DIA = new eClockBase.Modelos.Model_TURNOS_DIA();
            eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA EC_TURNOS_SEMANAL_DIA = new eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA();

            DataSet DS_Anviz = new DataSet();
            DataSet DS_TurnosAnviz = new DataSet();
            DataSet DS_TurnosDiaAnviz = new DataSet();
            DataSet DS_TurnosSemanalDiaAnviz = new DataSet();
            DataSet DS_Empleados = new DataSet();
            DataSet DS_TiposIncidencias = new DataSet();
            DataSet DS_Accesos = new DataSet();
            DataSet DS_Justificaciones = new DataSet();

            try
            {
                String QryTurnosAnviz = "SELECT * FROM Schedule";
                String QryTurnosDiaAnviz = "SELECT * FROM TimeTable";
                String QryTurnosSemanalDiaAnviz = "SELECT * FROM SchTime";


                //Creamos un dataadapter donde depositaremos los resultados.
                OleDbDataAdapter AdaptadorTurnos = new OleDbDataAdapter(QryTurnosAnviz, Conexion);
                OleDbDataAdapter AdaptadorTurnosDiaAnviz = new OleDbDataAdapter(QryTurnosDiaAnviz, Conexion);
                OleDbDataAdapter AdaptadorTurnosSemanalDiaAnviz = new OleDbDataAdapter(QryTurnosSemanalDiaAnviz, Conexion);


                AdaptadorTurnos.Fill(DS_TurnosAnviz);
                AdaptadorTurnosDiaAnviz.Fill(DS_TurnosDiaAnviz);
                AdaptadorTurnosSemanalDiaAnviz.Fill(DS_TurnosSemanalDiaAnviz);

                if(DS_TurnosDiaAnviz == null || DS_TurnosDiaAnviz.Tables.Count < 1 || DS_Accesos.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Sin datos en el origen");
                    return;
                }

                foreach (DataRow Turno_Dia_Anviz in  DS_TurnosDiaAnviz.Tables[0].Rows)
                {
                    EC_TURNOS.Turno.TURNO_ID = -1;
                    EC_TURNOS.Turno.TIPO_TURNO_ID = 5;
                    EC_TURNOS.Turno.TURNO_NOMBRE = eClockBase.CeC.Convierte2String(Turno_Dia_Anviz["Timename"]);
                    EC_TURNOS.Turno.TURNO_ASISTENCIA = true;
                    EC_TURNOS.Turno.TURNO_PHEXTRAS = false;
                    EC_TURNOS.Turno.TURNO_INDIVIDUAL = false;
                    EC_TURNOS.Turno.TURNO_GRUPOS = "";
                    EC_TURNOS.Turno.TURNO_COLOR = 0;
                    EC_TURNOS.Turno.TURNO_BORRADO = false;

                    // Entradas y Salidas
                    DateTime Intime = CeC.Convierte2DateTime(Turno_Dia_Anviz["Intime"]);
                    DateTime Outtime = CeC.Convierte2DateTime(Turno_Dia_Anviz["Outtime"]);
                    // Entradas y Salidas mínimas y máximas
                    DateTime BIntime = CeC.Convierte2DateTime(Turno_Dia_Anviz["BIntime"]);
                    DateTime BOuttime = CeC.Convierte2DateTime(Turno_Dia_Anviz["BOuttime"]);
                    DateTime EIntime = CeC.Convierte2DateTime(Turno_Dia_Anviz["EIntime"]);
                    DateTime EOuttime = CeC.Convierte2DateTime(Turno_Dia_Anviz["EOuttime"]);
                    // Tiempo de retardo. En eClock Básico se meten como enteros.
                    DateTime Latetime = CeC.FechaNula;

                    if (CeC.Convierte2Int(Turno_Dia_Anviz["Latetime"]) >= 60)
                    {
                        Latetime = Latetime.AddHours(CeC.Convierte2Int(Turno_Dia_Anviz["Latetime"]) / 60);
                        Latetime = Latetime.AddMinutes(CeC.Convierte2Int(Turno_Dia_Anviz["Latetime"]) % 60);
                    }

                    //
                    EC_TURNOS_DIA.TURNO_DIA_ID = -1;
                    EC_TURNOS_DIA.TURNO_DIA_HEMIN = new DateTime(2006, 1, 1, BIntime.Hour, Intime.Minute, BIntime.Second);
                    EC_TURNOS_DIA.TURNO_DIA_HE = new DateTime(2006, 1, 1, Intime.Hour, Intime.Minute, Intime.Second);
                    EC_TURNOS_DIA.TURNO_DIA_HEMAX = new DateTime(2006, 1, 1, EIntime.Hour, EIntime.Minute, EIntime.Second);
                    EC_TURNOS_DIA.TURNO_DIA_HERETARDO = Latetime;
                    EC_TURNOS_DIA.TURNO_DIA_HSMIN = new DateTime(2006, 1, 1, BOuttime.Hour, BOuttime.Minute, BOuttime.Second);
                    EC_TURNOS_DIA.TURNO_DIA_HS = new DateTime(2006, 1, 1, Outtime.Hour, Outtime.Minute, Outtime.Second);
                    EC_TURNOS_DIA.TURNO_DIA_HSMAX = new DateTime(2006, 1, 1, EOuttime.Hour, EOuttime.Minute, EOuttime.Second);
                    EC_TURNOS_DIA.TURNO_DIA_HBLOQUE = CeC.FechaNula;
                    EC_TURNOS_DIA.TURNO_DIA_HBLOQUET = CeC.FechaNula;
                    EC_TURNOS_DIA.TURNO_DIA_HTIEMPO = EC_TURNOS_DIA.TURNO_DIA_HS.AddHours(-EC_TURNOS_DIA.TURNO_DIA_HE.Hour);
                    EC_TURNOS_DIA.TURNO_DIA_HAYCOMIDA = false;
                    EC_TURNOS_DIA.TURNO_DIA_HCS = CeC.FechaNula;
                    EC_TURNOS_DIA.TURNO_DIA_HCR = CeC.FechaNula;
                    EC_TURNOS_DIA.TURNO_DIA_HCTIEMPO = CeC.FechaNula;
                    EC_TURNOS_DIA.TURNO_DIA_HCTOLERA = CeC.FechaNula;
                    EC_TURNOS_DIA.TURNO_DIA_PHEX = false;
                    EC_TURNOS_DIA.TURNO_DIA_NO_ASIS = false;
                    EC_TURNOS_DIA.TURNO_DIA_HERETARDO_B = Latetime;
                    EC_TURNOS_DIA.TURNO_DIA_HERETARDO_C = Latetime;
                    EC_TURNOS_DIA.TURNO_DIA_HERETARDO_D = Latetime;
                    EC_TURNOS.TurnoDias.Add(EC_TURNOS_DIA);
                    if(DS_TurnosSemanalDiaAnviz == null && DS_TurnosSemanalDiaAnviz.Tables.Count < 1)
                    foreach (DataRow Turno_Semanal_Dia_Anviz in DS_TurnosSemanalDiaAnviz.Tables[0].Rows)
                    {

                        if (CeC.Convierte2Int(Turno_Semanal_Dia_Anviz["Timeid"]) == CeC.Convierte2Int(Turno_Dia_Anviz["Timeid"]))
                        {
                            EC_TURNOS_SEMANAL_DIA.TURNO_SEMANAL_DIA_ID = -1;
                            EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID = -1;
                            EC_TURNOS_SEMANAL_DIA.TURNO_ID = -1;
                            EC_TURNOS_SEMANAL_DIA.DIA_SEMANA_ID = eClockBase.CeC.Convierte2Int(Turno_Semanal_Dia_Anviz["BeginDay"]) + 1;
                            EC_TURNOS.TurnoSemanalDias.Add(EC_TURNOS_SEMANAL_DIA);
                            EC_TURNOS_SEMANAL_DIA = new eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA();
                        }
                    }

                    eClockBase.Controladores.Turnos ControladorTurno = new eClockBase.Controladores.Turnos(Sesion);
                    ControladorTurno.GuardadoEvent += CntTurno_GuardadoEvent;
                    ControladorTurno.Guardar(EC_TURNOS);
                    // Se vacian las listas par poder introducir nuevos datos.
                    EC_TURNOS.TurnoDias = new List<eClockBase.Modelos.Model_TURNOS_DIA>();
                    EC_TURNOS.TurnoSemanalDias = new List<eClockBase.Modelos.Model_TURNOS_SEMANAL_DIA>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo al importar datos de Turnos.");
                CeC_Log.AgregaError(ex);
            }
        }
        #endregion
        void CntTurno_GuardadoEvent(bool Guardado)
        {
            //throw new NotImplementedException();
        }

        private void Lbl_Prueba_Click(object sender, RoutedEventArgs e)
        {
            ProbarConexion(ArchivoBD.FileName);
        }
    }
}
