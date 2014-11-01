using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;

/// <summary>
/// Descripción breve de WS_Monedero
/// </summary>
namespace eClock
{
    [WebService(Namespace = "/eClock")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class WS_Monedero : System.Web.Services.WebService
    {

        public WS_Monedero()
        {

            //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
            //InitializeComponent(); 
        }
        /// <summary>
        /// Regresa el ID de la persona por medio del Link_ID
        /// </summary>
        /// <param name="Persona_link_ID">Link de la persona</param>
        /// <returns>ID de la persona</returns>
        [WebMethod]
        public int PersonaID(int PersonalinkID)
        {
            return CeC_Personas.ObtenPersonaID(PersonalinkID);
        }
        /// <summary>
        /// Ejecuta una sentencia SQL para obtener el valor del CAMPO1 y determinar si es Centro de Costos
        /// </summary>
        /// <param name="PersonalinkID">Clave del empleado.</param>
        /// <returns>El valor (entero) para determinar si es Centro de Costos</returns>
        [WebMethod]
        public int ObtenEsCCT(int PersonalinkID)
        {
            return CeC_BD.EjecutaEscalarInt("SELECT CAMPO1 FROM EC_PERSONAS_DATOS WHERE PERSONA_LINK_ID = " + PersonalinkID);
        }
        /// <summary>
        /// Obtiene el nombre de la persona a partir de ID
        /// </summary>
        /// <param name="Persona_ID">ID de la persona</param>
        /// <returns>Un string que contiene el nombre de la persona</returns>
        [WebMethod]
        public String PersonaNombre(int PersonaID)
        {
            return CeC_BD.ObtenPersonaNombre(PersonaID);
        }
        /// <summary>
        /// Obtiene la fecha y hora del sistema
        /// </summary>
        /// <returns>Un DateTime que contiene la fecha y hora del sistema</returns>
        [WebMethod]
        public DateTime FechaHora()
        {
            return DateTime.Now;
        }
        /// <summary>
        /// Obtiene el número de comidas del dia de hoy a partir de el ID de la persona
        /// </summary>
        /// <param name="Persona_ID">ID de la persona</param>
        /// <returns>Entero que contiene el número de comidas de hoy</returns>
        [WebMethod(Description = "Obtiene el numero de comidas de la fecha actual", MessageName = "NoComidasHoyWindowsCE")]
        public int NoComidasHoy(int PersonaID)
        {
            int NoComidas = CeC_BD.EjecutaEscalarInt("SELECT COUNT(PERSONA_COMIDA_FECHA) FROM EC_PERSONAS_COMIDA WHERE PERSONA_COMIDA_FECHA BETWEEN " + CeC_BD.SqlFecha(DateTime.Now) + " AND " + CeC_BD.SqlFecha(DateTime.Now.AddDays(1)) + " AND PERSONA_ID=" + PersonaID.ToString());
            if (NoComidas <= 0)
                return 0;
            return NoComidas;
        }
        [WebMethod(Description = "Obtiene el numero de comidas de la fecha seleccionada", MessageName = "NoComidasHoyWindows", EnableSession = true)]
        public int NoComidasFecha(int PersonaID, DateTime Fecha)
        {
            //string Qry = "SELECT SUM(CASE WHEN EC_PERSONAS_COMIDA.TIPO_COMIDA_ID = 1 THEN 1 ELSE 0 END) " +
            //            "FROM EC_PERSONAS_COMIDA " +
            //            "WHERE PERSONA_COMIDA_FECHA >= @" + CeC_BD.SqlFecha(Fecha) + "@ AND PERSONA_COMIDA_FECHA <= @" + CeC_BD.SqlFecha(Fecha.AddDays(1)) + "@ " +
            //            "AND PERSONA_ID= " + PersonaID;
            string Qry = "SELECT SUM(CASE WHEN EC_PERSONAS_COMIDA.TIPO_COMIDA_ID = 1 THEN 1 ELSE 0 END) " +
                        "FROM EC_PERSONAS_COMIDA " +
                        "WHERE PERSONA_COMIDA_FECHA BETWEEN " + CeC_BD.SqlFecha(Fecha) + " AND " + CeC_BD.SqlFecha(Fecha.AddDays(1)) + 
                        "AND PERSONA_ID= " + PersonaID;
            int NoComidas = CeC_BD.EjecutaEscalarInt(Qry);
            if (NoComidas <= 0)
                return 0;
            return NoComidas;
        }
        /// <summary>
        /// Genera un Autonúmerico, de una tabla determinada y un campo determinado
        /// </summary>
        /// <param name="Campo">Nombre del Campo de la tabla</param>
        /// <param name="Tabla">Nombre de la tabla</param>
        /// <returns>Regresa un entero que es el siguiente en la tabla</returns>
        private int Autonumerico(string Campo, string Tabla)
        {
            return CeC_Autonumerico.GeneraAutonumerico(Tabla, Campo);
        }
        /// <summary>
        /// Agrega una comida a una persona a partir del ID
        /// </summary>
        /// <param name="Persona_ID">ID de la persona</param>
        /// <param name="Tipo_Comida_ID">ID del tipo de comida</param>
        /// <param name="Tipo_Cobro_ID">ID del tipo de cobro</param>
        /// <returns></returns>
        [WebMethod]
        public bool AgregarComida(int PersonaID, int TipoComidaID, int TipoCobroID)
        {
            CIsLog2.AgregaLog("AgregarComida " + PersonaID + " " + TipoComidaID + " " + TipoCobroID);

            if (CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_COMIDA(PERSONA_COMIDA_ID,PERSONA_ID,TIPO_COMIDA_ID,TIPO_COBRO_ID,PERSONA_COMIDA_FECHA,SESION_ID,TIPO_COMIDA_COSTOA) VALUES(" + Autonumerico("PERSONA_COMIDA_ID", "EC_PERSONAS_COMIDA").ToString() + "," + PersonaID.ToString() + "," + TipoComidaID.ToString() + "," + TipoCobroID.ToString() + "," + CeC_BD.SqlFechaHora(DateTime.Now) + ",0,(SELECT TIPO_COMIDA_COSTO FROM EC_TIPO_COMIDA WHERE TIPO_COMIDA_ID = " + TipoComidaID.ToString() + "))") > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Agrega una comida a una persona a partir del ID y una Fecha (carga manual)
        /// </summary>
        /// <param name="Persona_ID">ID de la persona</param>
        /// <param name="Tipo_Comida_ID">ID del tipo de comida</param>
        /// <param name="Tipo_Cobro_ID">ID del tipo de cobro</param>
        /// <param name="Fecha">Fecha de cobro</param>
        /// <returns></returns>
        [WebMethod]
        public bool AgregarComidaFecha(int PersonaID, int TipoComidaID, int TipoCobroID, DateTime Fecha)
        {
            CIsLog2.AgregaLog("AgregarComida " + PersonaID + " " + TipoComidaID + " " + TipoCobroID);
            decimal Costo = CeC_BD.EjecutaEscalarDecimal("SELECT TIPO_COMIDA_COSTO FROM EC_TIPO_COMIDA WHERE TIPO_COMIDA_ID = " + TipoComidaID.ToString() + "");
            if (CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_COMIDA(PERSONA_COMIDA_ID,PERSONA_ID,TIPO_COMIDA_ID,TIPO_COBRO_ID,PERSONA_COMIDA_FECHA,SESION_ID,TIPO_COMIDA_COSTOA) VALUES(" + Autonumerico("PERSONA_COMIDA_ID", "EC_PERSONAS_COMIDA").ToString() + "," + PersonaID.ToString() + "," + TipoComidaID.ToString() + "," + TipoCobroID.ToString() + "," + CeC_BD.SqlFecha(Fecha) + ",0," + Costo.ToString() + ")") > 0)
                return true;
            return false;
        }


        /// <summary>
        /// Obtiene el saldo de la persona a partir de su ID
        /// </summary>
        /// <param name="Persona_ID">ID de la persona</param>
        /// <returns>Decimal que contiene el saldo de la persona</returns>
        [WebMethod]
        public decimal Saldo(int PersonaID)
        {
            Decimal S = CeC_BD.EjecutaEscalarDecimal("SELECT MONEDERO_SALDO FROM eC_MONEDERO WHERE MONEDERO_ID in (SELECT MAX(MONEDERO_ID) FROM eC_MONEDERO WHERE PERSONA_ID = " + PersonaID.ToString() + " )");
            if (S == -9999)
                return 0;
            return S;
        }
        /// <summary>
        /// Obtiene un DataSet que contiene los productos que no han sido borrados
        /// </summary>
        /// <returns>DataSet que contiene los productos</returns>
        [WebMethod]
        public DataSet Productos()
        {
            return (DataSet)CeC_BD.EjecutaDataSet("SELECT PRODUCTO,PRODUCTO_COSTO,PRODUCTO_NO FROM eC_PRODUCTOS WHERE PRODUCTO_BORRADO=0 ORDER BY PRODUCTO_NO");
        }
        /// <summary>
        /// Cobra los productos en la tabla de productos y monedero eC_MONEDERO_PROD
        /// </summary>
        /// <param name="Productos_ID">Matriz de productos</param>
        /// <param name="MonederoID">Identificador del Monedero</param>
        /// <returns>Regresa verdadero si se proceso con exito,en caso contrario regresa False</returns>
        private bool MonederoProductos(string[] ProductosID, int MonederoID)
        {
            /*for (int i = 0; i < ProductosID.Length; i++)
            {

                CeC_BD.EjecutaComando("INSERT INTO eC_MONEDERO_PROD(MONEDERO_ID,PRODUCTO_ID,MONEDERO_PROD_ORD) VALUES(" + MonederoID.ToString() + "," + ProductosID + "," + i.ToString() + ")");
            }
            return true;*/
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand();
            DataSet dataset = new DataSet();
            OleDbDataAdapter adapatador = new OleDbDataAdapter("SELECT PRODUCTO_ID,PRODUCTO FROM eC_PRODUCTOS WHERE PRODUCTO_BORRADO = 0", conexion);
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                adapatador.Fill(dataset);
                for (int i = 0; i < ProductosID.Length; i++)
                {
                    DataRow[] dr = dataset.Tables[0].Select("PRODUCTO='" + ProductosID[i] + "'");

                    comando.CommandText = "INSERT INTO eC_MONEDERO_PROD(MONEDERO_ID,PRODUCTO_ID,MONEDERO_PROD_ORD) VALUES(" + MonederoID.ToString() + "," + dr[0].ItemArray[0].ToString() + "," + i.ToString() + ")";
                    int ret = comando.ExecuteNonQuery();
                }
                conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return false;
            }
        }
        private decimal MonederoConsumo(string[] Productos)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            DataSet dataset = new DataSet();
            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT PRODUCTO,PRODUCTO_COSTO,PRODUCTO_ID FROM eC_PRODUCTOS WHERE PRODUCTO_BORRADO=0", conexion);
            try
            {
                conexion.Open();
                adaptador.Fill(dataset);
                conexion.Close();
                Decimal Monedero_Consumo = 0;
                for (int i = 0; i < Productos.Length; i++)
                {
                    DataRow[] dr = dataset.Tables[0].Select("PRODUCTO = '" + Productos[i] + "'");
                    Monedero_Consumo += Convert.ToDecimal(dr[0].ItemArray[1]);
                }
                dataset.Dispose();
                adaptador.Dispose();
                return Monedero_Consumo;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return -9999;
            }
        }
        /// <summary>
        /// Cobra los productos de una persona a partir de su ID
        /// </summary>
        /// <param name="Persona_ID">ID de la persona</param>
        /// <param name="Productos">Productos que ha consumido la persona</param>
        /// <param name="Tipo_Cobro_ID">ID del tipo de cobro</param>
        /// <returns></returns>
        [WebMethod]
        public bool CobraProductos(int PersonaID, string[] Productos, int TipoCobroID, out int RefereciaMonederoID, out string Mensaje)
        {
            if (Productos.Length == 0)
            {
                RefereciaMonederoID = 0;
                Mensaje = "Ha ocurrido un error";
                return false;
            }
            else
            {
                int MonderoID = Autonumerico("MONEDERO_ID", "EC_MONEDERO");
                RefereciaMonederoID = MonderoID;
                Decimal Consumo = MonederoConsumo(Productos);
                Decimal SaldoActual = Saldo(PersonaID) - Consumo;
                if (CeC_Config.PermiteCreditoDesayuno != true && SaldoActual < 0)
                {
                    Mensaje = "Saldo insuficiente";
                    return false;
                }
                if (CeC_BD.EjecutaComando("INSERT INTO eC_MONEDERO(MONEDERO_ID,TIPO_COBRO_ID,PERSONA_ID,SESION_ID,MONEDERO_FECHA,MONEDERO_CONSUMO,MONEDERO_SALDO) VALUES(" + MonderoID.ToString() + "," + TipoCobroID.ToString() + "," + PersonaID.ToString() + ",0," + CeC_BD.SqlFechaHora(DateTime.Now) + "," + Consumo.ToString() + "," + SaldoActual.ToString() + ")") <= 0)
                {
                    Mensaje = "Ha ocurrido un error";
                    return false;
                }
                else
                {
                    MonederoProductos(Productos, MonderoID);
                    Mensaje = "Cobro satisfactorio";
                    return true;
                }
                /*
                string Qry = "INSERT INTO eC_MONEDERO(MONEDERO_ID,TIPO_COBRO_ID,PERSONA_ID,SESION_ID,MONEDERO_FECHA,MONEDERO_CONSUMO,MONEDERO_SALDO) VALUES(" + MonderoID.ToString() + "," + TipoCobroID.ToString() + "," + PersonaID.ToString() + ",0," + CeC_BD.SqlFechaHora(DateTime.Now) + "," + Consumo.ToString() + "," + SaldoActual.ToString() + ")";
                OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
                OleDbCommand comando = new OleDbCommand(Qry, conexion);
                try
                {
                    conexion.Open();
                    int Ret = comando.ExecuteNonQuery();
                    conexion.Close();
                    if (Ret == 0)
                    {
                        Mensaje = "Ha ocurrido un error";
                        return false;
                    }
                    else
                    {
                        MonederoProductos(Productos, MonderoID);
                        Mensaje = "Cobro satisfactorio";
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    if (conexion.State == ConnectionState.Open)
                        conexion.Dispose();
                    Mensaje = "Ha ocurrido un error";
                    return false;
                }*/
            }
        }
        /// <summary>
        /// Asigna una huella a determinada persona a partir de su ID
        /// </summary>
        /// <param name="Persona_ID">ID de la persona</param>
        /// <param name="Terminal_ID">ID de la terminal</param>
        /// <param name="Huella">Huella de la persona</param>
        /// <returns></returns>
        [WebMethod]
        public bool AsignaHuella(int PersonaID, int TerminalID, byte[] Huella)
        {
            string qry = "SELECT ALMACEN_VEC_ID FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString();
            int AlmacenID = CeC_BD.EjecutaEscalarInt(qry);
            if (AlmacenID != -9999)
            {
                byte[] Datos = CeC_BD.ObtenBinario("EC_PERSONAS_A_VEC_1", "PERSONA_ID", PersonaID, "ALMACEN_VEC_ID", AlmacenID, "PERSONAS_A_VEC_1");
                if (Datos != null)
                {
                    if (CeC_BD.AsignaBinario("EC_PERSONAS_A_VEC", "PERSONA_ID", PersonaID, "ALMACEN_VEC_ID", AlmacenID, "PERSONAS_A_VEC_1", Huella))
                        return true;
                    else
                        return false;
                }
                else
                {
                    qry = "INSERT INTO EC_PERSONAS_A_VEC(PERSONA_ID,ALMACEN_VEC_ID) VALUES (" + PersonaID.ToString() + "," + AlmacenID.ToString() + ")";
                    int ret = CeC_BD.EjecutaComando(qry);
                    if (CeC_BD.AsignaBinario("EC_PERSONAS_A_VEC", "PERSONA_ID", PersonaID, "ALMACEN_VEC_ID", AlmacenID, "PERSONAS_A_VEC_1", Huella))
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                return false;
            }
        }
        [WebMethod]
        public bool ExisteHuella(int PersonaID, int TerminalID)
        {
            byte[] Datos = ObtenerHuella(PersonaID, TerminalID);
            if (Datos != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Indica si puede o no desayunar una persona a partir de su ID
        /// </summary>
        /// <param name="Persona_ID"></param>
        /// <returns></returns>
        [WebMethod]
        public bool PuedeDesayunar(int PersonaID, out string Mensaje)
        {
            if (CeC_Config.PermiteCreditoDesayuno)
            {
                if (CeC_Config.PermiteDesayunarDespues == true)
                {
                    Mensaje = "Puede desayunar";
                    return true;
                }
                else
                {
                    if (ValidarTurnoDia(PersonaID) == true)
                    {
                        Mensaje = "Puede desayunar";
                        return true;
                    }
                    else
                    {
                        if (VerificarNomina(PersonaID) == true)
                        {
                            Mensaje = "Puede desayunar";
                            return true;
                        }
                        else
                        {
                            Mensaje = "No puede desayunar debido a su turno";
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (Saldo(PersonaID) > 0)
                {
                    if (CeC_Config.PermiteDesayunarDespues == true)
                    {
                        Mensaje = "Puede desayunar";
                        return true;
                    }
                    else
                    {
                        if (ValidarTurnoDia(PersonaID) == true)
                        {
                            Mensaje = "Puede desayunar";
                            return true;
                        }
                        else
                        {
                            if (VerificarNomina(PersonaID) == true)
                            {
                                Mensaje = "Puede desayunar";
                                return true;
                            }
                            else
                            {
                                Mensaje = "No puede desayunar debido a su turno";
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    Mensaje = "Saldo insuficiente";
                    return false;
                }
            }
        }
        [WebMethod]
        public DataSet ListaTerminales()
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand("SELECT TERMINAL_ID,TERMINAL_NOMBRE FROM EC_TERMINALES WHERE TERMINAL_BORRADO=0", conexion);
            OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
            DataSet dataset = new DataSet();
            try
            {
                conexion.Open();
                adaptador.Fill(dataset);
                conexion.Close();
                return dataset;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return null;
            }
        }
        [WebMethod]
        public DataSet ListaPersonaIDs()
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO=0", conexion);
            OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
            DataSet dataset = new DataSet();
            try
            {
                conexion.Open();
                adaptador.Fill(dataset);
                conexion.Close();
                return dataset;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return null;
            }
        }
        [WebMethod]
        public bool PuedeComer(int PersonaID)
        {
            try
            {
                if (CeC_Config.PermiteComerDespues == true)
                    return true;
                else
                {
                    int Turno_Dia_ID = CeC_BD.EjecutaEscalarInt("SELECT TURNO_DIA_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + PersonaID.ToString() + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(DateTime.Now));
                    DateTime HoraSalidaComer = CeC_BD.EjecutaEscalarDateTime("SELECT TURNO_DIA_HCS FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + Turno_Dia_ID.ToString());
                    DateTime HoraRegresoComer = CeC_BD.EjecutaEscalarDateTime("SELECT TURNO_DIA_HCR FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + Turno_Dia_ID.ToString());
                    if ((DateTime.Now.TimeOfDay >= HoraSalidaComer.TimeOfDay) && (DateTime.Now.TimeOfDay <= HoraRegresoComer.TimeOfDay))
                        return true;
                    else
                        return false;
                    /*
                                            comando.CommandText = "SELECT TURNO_DIA_HCS FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + Turno_Dia_ID.ToString();
                    DateTime HoraSalidaComer = Convert.ToDateTime(comando.ExecuteScalar());
                    TimeSpan HoraS = new TimeSpan(HoraSalidaComer.Hour, HoraSalidaComer.Minute, HoraSalidaComer.Second);
                    comando.CommandText = "SELECT TURNO_DIA_HCR FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID = " + Turno_Dia_ID.ToString();
                    DateTime HoraRegresoComer = Convert.ToDateTime(comando.ExecuteScalar());
                    TimeSpan HoraR = new TimeSpan(HoraRegresoComer.Hour, HoraRegresoComer.Minute, HoraRegresoComer.Second);
                    TimeSpan HoraA = new TimeSpan(HoraActual.Hour, HoraActual.Minute, HoraActual.Second);
                    if ((HoraA > HoraS) && (HoraA < HoraR))
                        return true;
                    else
                        return false;*/
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Obtiene el TRACVE de la persona a partir de su ID
        /// </summary>
        /// <param name="Persona_ID">ID de la persona</param>
        /// <returns></returns>
        [WebMethod]
        public int PersonaLinkID(int PersonaID)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand("SELECT PERSONA_LINK_ID FROM EC_PERSONAS WHERE PERSONA_ID = " + PersonaID.ToString(), conexion);
            try
            {
                conexion.Open();
                object valor = comando.ExecuteScalar();
                conexion.Close();
                return Convert.ToInt32(valor);
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return -9999;
            }
        }
        private bool ValidarTurnoDia(int PersonaID)
        {
            int Turno_Dia_ID = CeC_BD.EjecutaEscalarInt("SELECT TURNO_DIA_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + PersonaID.ToString() + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(FechaHora()));

            if (Turno_Dia_ID <= 0)
                return true;
            else
                return false;
        }
        private bool VerificarNomina(int PersonaID)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand("SELECT TIPO_NOMINA FROM EC_PERSONAS_DATOS WHERE PERSONA_ID = " + PersonaID.ToString(), conexion);
            try
            {
                conexion.Open();
                bool ret = false;
                object valor = comando.ExecuteScalar();
                if (Convert.ToString(valor) == "NS")
                {
                    if (ObtenerHoraEntrada(PersonaID) == true)
                        ret = true;
                    else
                        ret = false;
                }
                else
                {
                    if (ExisteChecada(PersonaID) == false)
                    {
                        ret = true;
                    }
                }
                conexion.Close();
                return ret;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return false;
            }
        }
        private bool ObtenerHoraEntrada(int PersonaID)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand("SELECT TURNO_DIA_HE FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID in (SELECT TURNO_DIA_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + PersonaID.ToString() + " AND PERSONA_DIARIO_FECHA = to_date('" + FechaHora().ToString("dd/MM/yyyy") + "','DD/mm/YYYY'))", conexion);
            try
            {
                conexion.Open();
                object valor = comando.ExecuteScalar();
                conexion.Close();
                DateTime HoraEntrada = Convert.ToDateTime(valor);
                HoraEntrada.AddMinutes(26);
                if (FechaHora() < HoraEntrada)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return false;
            }
        }
        private bool ExisteChecada(int PersonaID)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand("SELECT TURNO_DIA_HEMAX FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID in (SELECT TURNO_DIA_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + PersonaID.ToString() + " AND PERSONA_DIARIO_FECHA = to_date('" + FechaHora().ToString("dd/MM/yyyy") + "','DD/mm/YYYY'))", conexion);
            try
            {
                conexion.Open();
                object valor = comando.ExecuteScalar();
                DateTime HoraMaxima = DateTime.Today + CeC_BD.DateTime2TimeSpan(Convert.ToDateTime(valor));
                comando.CommandText = "SELECT TURNO_DIA_HEMIN FROM EC_TURNOS_DIA WHERE TURNO_DIA_ID in (SELECT TURNO_DIA_ID FROM EC_PERSONAS_DIARIO WHERE PERSONA_ID = " + PersonaID.ToString() + " AND PERSONA_DIARIO_FECHA = to_date('" + FechaHora().ToString("dd/MM/yyyy") + "','DD/mm/YYYY'))";
                valor = comando.ExecuteScalar();
                DateTime HoraMinima = DateTime.Today + CeC_BD.DateTime2TimeSpan(Convert.ToDateTime(valor));
                comando.CommandText = "SELECT count(ACCESO_FECHAHORA) FROM EC_ACCESOS WHERE PERSONA_ID = " + PersonaID.ToString() + " AND ACCESO_FECHAHORA <= " + CeC_BD.SqlFechaHora(HoraMaxima) + " AND ACCESO_FECHAHORA >= " + CeC_BD.SqlFechaHora(HoraMinima);
                valor = comando.ExecuteScalar();
                conexion.Close();
                if (Convert.ToInt32(valor) == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return false;
            }
        }
        /// <summary>
        /// Cobra en monedero de una persona con la fecha especificada
        /// </summary>
        /// <param name="Persona_LinkID">Persona_LinkID</param>
        /// <param name="Cantidad">Cantidad a Cobrar(Si es negativo es abono)</param>
        /// <param name="Fecha">Fecha en la que se registra el cobro</param>
        /// <returns></returns>
        [WebMethod]
        public Decimal CobrarMonedero(int Persona_LinkID, Decimal Cantidad, DateTime Fecha)
        {
            int Persona_ID = CeC_Personas.ObtenPersonaID(Persona_LinkID);
            Decimal ViejoSaldo = Saldo(Persona_ID);
            if (ViejoSaldo == -9999)
                ViejoSaldo = 0;
            Decimal NuevoSaldo = ViejoSaldo - Cantidad;
            try
            {

                int MonderoID = CeC_Autonumerico.GeneraAutonumerico("EC_MONEDERO", "MONEDERO_ID");
                CeC_BD.EjecutaComando("INSERT INTO eC_MONEDERO(MONEDERO_ID,TIPO_COBRO_ID,PERSONA_ID,SESION_ID,MONEDERO_FECHA,MONEDERO_CONSUMO,MONEDERO_SALDO) VALUES(" + MonderoID + ",0," + Persona_ID.ToString() + ",0," + CeC_BD.SqlFechaHora(Fecha) + "," + Cantidad + "," + NuevoSaldo + ")");
                return NuevoSaldo;
            }
            catch (Exception ex)
            {
                return -9999;
            }
        }
        [WebMethod]
        public int TienePrimeraComida(int PersonaID)
        {
            CIsLog2.AgregaLog("TienePrimeraComida " + PersonaID);
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand("SELECT TIPO_COMIDA_ID FROM EC_PERSONAS_COMIDA WHERE PERSONA_COMIDA_FECHA BETWEEN " + CeC_BD.SqlFecha(FechaHora()) + " AND " + CeC_BD.SqlFecha(FechaHora().AddDays(1)) + " AND PERSONA_ID = " + PersonaID.ToString(), conexion);
            try
            {
                conexion.Open();
                object valor = comando.ExecuteScalar();
                conexion.Close();
                if (Convert.ToInt32(valor) == 1)
                    return 1;
                else if (valor == null || valor == DBNull.Value)
                    return 0;
                else if (Convert.ToInt32(valor) == 2)
                    return 2;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return 0;
            }
            return 0;
        }
        [WebMethod]
        public bool BorrarRegistrosPrueba(DateTime FechaI, DateTime FechaF)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT PERSONA_ID FROM EC_PERSONAS_P", conexion);
            OleDbCommand comando = new OleDbCommand();
            String ConsultaComidas = "DELETE EC_PERSONAS_COMIDA WHERE (PERSONA_COMIDA_FECHA BETWEEN " + CeC_BD.SqlFechaHora(FechaI) + " AND " + CeC_BD.SqlFechaHora(FechaF) + ") AND (";
            String ConsultaDesayunos = "DELETE eC_MONEDERO WHERE (MONEDERO_FECHA BETWEEN " + CeC_BD.SqlFechaHora(FechaI) + " AND " + CeC_BD.SqlFechaHora(FechaF) + ") AND (";
            DataSet dataset = new DataSet();
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                adaptador.Fill(dataset);
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    if (i != dataset.Tables[0].Rows.Count - 1)
                    {
                        ConsultaComidas += "(PERSONA_ID=" + dataset.Tables[0].Rows[i]["PERSONA_ID"].ToString() + ") OR";
                        ConsultaDesayunos += "(PERSONA_ID=" + dataset.Tables[0].Rows[i]["PERSONA_ID"].ToString() + ") OR";
                    }
                    else
                    {
                        ConsultaComidas += "(PERSONA_ID=" + dataset.Tables[0].Rows[i]["PERSONA_ID"].ToString() + "))";
                        ConsultaDesayunos += "(PERSONA_ID=" + dataset.Tables[0].Rows[i]["PERSONA_ID"].ToString() + "))";
                    }
                    BorrarMonederoProd(Convert.ToInt32(dataset.Tables[0].Rows[i]["PERSONA_ID"]), FechaI, FechaF);
                }
                comando.CommandText = ConsultaComidas;
                int ret = comando.ExecuteNonQuery();
                comando.CommandText = ConsultaDesayunos;
                ret = comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return false;
            }
        }
        private bool BorrarMonederoProd(int PersonaID, DateTime FechaI, DateTime FechaF)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand();
            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT MONEDERO_ID FROM eC_MONEDERO WHERE (PERSONA_ID = " + PersonaID.ToString() + ") AND (MONEDERO_FECHA BETWEEN " + CeC_BD.SqlFechaHora(FechaI) + " AND " + CeC_BD.SqlFechaHora(FechaF) + ")", conexion);
            DataSet dataset = new DataSet();
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                adaptador.Fill(dataset);
                int ret;
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    comando.CommandText = "DELETE eC_MONEDERO_PROD WHERE MONEDERO_ID=" + dataset.Tables[0].Rows[i]["MONEDERO_ID"].ToString();
                    ret = comando.ExecuteNonQuery();
                }
                conexion.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return false;
            }
        }
        [WebMethod]
        public bool BorraRegistroPrueba(int PersonaID, DateTime FechaI, DateTime FechaF)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand();
            OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT MONEDERO_ID FROM eC_MONEDERO WHERE (PERSONA_ID=" + PersonaID.ToString() + ") AND (MONEDERO_FECHA BETWEEN " + CeC_BD.SqlFechaHora(FechaI) + " AND " + CeC_BD.SqlFechaHora(FechaF) + ")", conexion);
            DataSet dataset = new DataSet();
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                adaptador.Fill(dataset);
                int ret;
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    comando.CommandText = "DELETE eC_MONEDERO_PROD WHERE MONEDERO_ID=" + dataset.Tables[0].Rows[i]["MONEDERO_ID"].ToString();
                    ret = comando.ExecuteNonQuery();
                }
                comando.CommandText = "DELETE eC_MONEDERO WHERE (PERSONA_ID=" + PersonaID.ToString() + ") AND (MONEDERO_FECHA BETWEEN " + CeC_BD.SqlFechaHora(FechaI) + " AND " + CeC_BD.SqlFechaHora(FechaF) + ")";
                ret = comando.ExecuteNonQuery();
                comando.CommandText = "DELETE EC_PERSONAS_COMIDA WHERE (PERSONA_ID=" + PersonaID.ToString() + ") AND (PERSONA_COMIDA_FECHA BETWEEN " + CeC_BD.SqlFechaHora(FechaI) + " AND" + CeC_BD.SqlFechaHora(FechaF) + ")";
                ret = comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return false;
            }
        }
        [WebMethod]
        public int ExisteSupervisor(string Usuario)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand("SELECT USUARIO_USUARIO FROM EC_USUARIOS WHERE USUARIO_USUARIO = '" + Usuario + "' AND USUARIO_BORRADO=0", conexion);
            try
            {
                if (Usuario != "")
                {
                    conexion.Open();
                    object valor = comando.ExecuteScalar();
                    if ((valor == null) || (valor == DBNull.Value))
                    {
                        conexion.Close();
                        return -9999;
                    }
                    else
                    {
                        comando.CommandText = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + Usuario;
                        valor = comando.ExecuteScalar();
                        conexion.Close();
                        return Convert.ToInt32(valor);
                    }
                }
                else
                    return -9999;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return -9999;
            }
        }
        public string ObtenNS2Tracve(string NS)
        {
            string _NS;
            byte[] Datos = BitConverter.GetBytes(Convert.ToUInt32(NS));
            byte[] _Datos = new byte[4];
            for (int i = 0; i < 4; i++)
                _Datos[i] = Datos[3 - i];
            _NS = "00000000" + Bcd2String(_Datos, 0, 4);
            string aux;
            aux = CeC_BD.EjecutaEscalarString("SELECT PERSONA_LINK_ID FROM EC_PERSONAS_DATOS WHERE NO_CREDENCIAL='" + _NS + "'");
            if (aux != "")
                return aux;
            _NS = "00000000" + Bcd2String(Datos, 0, 4);
            return CeC_BD.EjecutaEscalarString("SELECT PERSONA_LINK_ID FROM EC_PERSONAS_DATOS WHERE NO_CREDENCIAL='" + _NS + "'");

        }
        [WebMethod]
        public string ExistePersonaTracve(string NS)
        {


            CIsLog2.AgregaLog("ExistePersonaTracve " + NS);
            string _NS;
            int Persona_Link_ID = 0;
            try
            {
                if (NS.Length >= 14)
                    Persona_Link_ID = Convert.ToInt32(NS.Substring(0, 4));
            }
            catch (Exception ex) { CIsLog2.AgregaError(ex); }

            if (Persona_Link_ID <= 0)
            {
                int _posicion = NS.Length > 10 ? NS.Length - 10 : 0;
                int _long = NS.Length > 10 ? 10 : NS.Length;
                return ObtenNS2Tracve(NS.Substring(_posicion, _long));
            }
            else
            {
                string NS_Entero = Convert.ToUInt32(NS.Substring(4)).ToString();
                if (Persona_Link_ID != Convert.ToInt32(CeC_BD.EjecutaEscalarString("SELECT PERSONA_LINK_ID FROM EC_PERSONAS_DATOS WHERE PERSONA_LINK_ID = " + Persona_Link_ID)))
                {
                    CIsLog2.AgregaError("No existe tracve");
                    return "";
                }
                string Ans = CeC_BD.EjecutaEscalarString("SELECT NO_CREDENCIAL FROM EC_PERSONAS_DATOS WHERE PERSONA_LINK_ID = " + Persona_Link_ID);
                if (Ans.Length >= 9 && Ans.Length <= 10)
                {
                    if (Ans != NS_Entero)
                    {
                        CIsLog2.AgregaError("tarjeta personalizada nuevo no coincide");
                        return "";
                    }
                }
                else
                {
                    if (Ans.Length > 0)
                    {
                        string tracve = ObtenNS2Tracve(NS_Entero);
                        if (Convert.ToInt32(tracve) != Persona_Link_ID)
                        {
                            CIsLog2.AgregaError("tarjeta personalizada viejo no coincide");
                            return "";
                        }
                    }
                    CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DATOS SET NO_CREDENCIAL = '" + NS_Entero + "' WHERE PERSONA_LINK_ID = " + Persona_Link_ID);
                }



                return Persona_Link_ID.ToString();
            }
        }
        [WebMethod]
        public int ExistePersonaID(int PersonaLinkID)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID=" + PersonaLinkID.ToString() + " AND PERSONA_BORRADO=0", conexion);
            try
            {
                conexion.Open();
                object valor = comando.ExecuteScalar();
                conexion.Close();
                if (valor == null)
                    return -9999;
                else
                    return Convert.ToInt32(valor);
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return -9999;
            }
        }
        [WebMethod]
        public bool ExistePersonaIDAlmacenID(int PersonaID, int AlmacenID)
        {
            OleDbConnection conexion = new OleDbConnection(CeC_BD.CadenaConexion());
            OleDbCommand comando = new OleDbCommand("SELECT PERSONA_ID FROM EC_PERSONAS_A_VEC WHERE PERSONA_ID = " + PersonaID.ToString() + " AND ALMACEN_VEC_ID = " + AlmacenID.ToString(), conexion);
            try
            {
                conexion.Open();
                object valor = comando.ExecuteScalar();
                conexion.Close();
                if (valor == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return false;
            }
        }
        /// <summary>
        /// Obtiene la huella de una persona a partir de su ID y terminal
        /// </summary>
        /// <param name="Persona_ID">ID de la persona</param>
        /// <param name="Terminal_ID">ID de la terminal</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] ObtenerHuella(int PersonaID, int TerminalID)
        {
            string qry = "SELECT ALMACEN_VEC_ID FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString();
            int AlmacenID = CeC_BD.EjecutaEscalarInt(qry);
            if (AlmacenID != -9999)
            {
                byte[] Huella = CeC_BD.ObtenBinario("EC_PERSONAS_A_VEC", "PERSONA_ID", PersonaID, "ALMACEN_VEC_ID", AlmacenID, "PERSONAS_A_VEC_1");
                if (Huella != null)
                {
                    return Huella;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        [WebMethod]
        public DataSet ObtenerHuellas(int TerminalID)
        {
            OracleConnection conexion = new OracleConnection(CeC_BD.CadenaConexionOracle());
            OracleCommand comando = new OracleCommand();
            OracleDataAdapter adaptador = new OracleDataAdapter(comando);
            DataSet dataset = new DataSet();
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.CommandText = "SELECT ALMACEN_VEC_ID FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + " AND TERMINAL_BORRADO=0";
                object Almacen = comando.ExecuteScalar();
                if (Almacen != null || Almacen != DBNull.Value)
                {
                    int AlmacenID = Convert.ToInt32(Almacen);
                    comando.CommandText = "SELECT PERSONA_ID,PERSONAS_A_VEC_1 FROM EC_PERSONAS_A_VEC WHERE ALMACEN_VEC_ID = " + AlmacenID.ToString() + " ORDER BY PERSONA_ID";
                    adaptador.Fill(dataset);
                    conexion.Close();
                    return dataset;
                }
                else
                {
                    if (conexion.State == ConnectionState.Open)
                        conexion.Dispose();
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Dispose();
                return null;
            }
        }
        public string Bcd2String(byte[] Arreglo, int Pos, int Len)
        {
            string Texto = "";
            if (Len < 0)
                Len = 0;
            if (Pos > Arreglo.Length)
                return "";
            if (Pos < 0)
                Pos = 0;

            if (Len <= 0 || Pos + Len > Arreglo.Length)
                Len = Arreglo.Length - Pos;
            for (int Cont = 0; Cont < Len; Cont++)
            {
                Texto += HexByte2String(Convert.ToByte(Arreglo[Pos + Cont] / 16)) + HexByte2String(Convert.ToByte(Arreglo[Pos + Cont] % 16));

            }
            return Texto;
        }
        public string HexByte2String(byte Hex)
        {
            if (Hex < 0)
                return "0";
            if (Hex > 16)
                return "F";
            byte[] Car = new byte[1];
            Car[0] = Convert.ToByte('0');
            int Numero = 0;
            if (Hex < 10)
                Numero = Hex + Car[0];

            Car[0] = Convert.ToByte('A');
            if (Hex >= 10)
                Numero = (Hex - 10) + Car[0];
            string Str = "";
            Str += Convert.ToChar(Numero);
            return Str;
        }
    }

}
