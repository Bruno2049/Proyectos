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
using System.IO;
using System.Data.OleDb;

namespace eClock
{
	/// <summary>
	/// Descripción breve de WF_PerfilesPermisos.
	/// </summary>
	public partial class WF_PerfilesPermisos : System.Web.UI.Page
	{
		protected System.Data.OleDb.OleDbConnection Conexion;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		protected System.Data.OleDb.OleDbDataAdapter DA_Restriccones;
		protected DS_Restricciones dS_Restricciones1;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
		protected System.Data.OleDb.OleDbDataAdapter DA_Perfiles;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand3;
		protected System.Data.OleDb.OleDbDataAdapter DA_Res;
		string [] NodosPGuardar;
		Infragistics.WebUI.UltraWebNavigator.Node node2 = new Infragistics.WebUI.UltraWebNavigator.Node();
		CeC_Sesion Sesion;
		DataTable Tabla1 = new DataTable("Restricciones");
		DataSet DS = new DataSet("Datos");
		DataRow DR;
		string [] Array_Mod = new string[1];
		string TotaldelArray = "";
		private string [] Array_Permisos_B = new string[10];

		private void ControlVisible(bool Caso)
		{
		}
		
		public string ArrayUnir(string Cadena, bool caso)
		{
			string valor = "";
			char [] caracter = new char[1];
			caracter[0] = Convert.ToChar(".");
			string [] ArrayCadena = Cadena.Split(caracter);

			for (int i =0; i< ArrayCadena.Length-1; i++)
			{
				if (i ==ArrayCadena.Length)
				{
					valor += ArrayCadena[i] ;
					break;
				}
				else
				{
					valor += ArrayCadena[i] + ".";
				}
			}
			if (valor.Substring(valor.Length-1,1) == ".")
				valor = valor.Substring(0,valor.Length -1);
			return valor; 			
		}

		private void AgregaHijo(string hijo, string NombreCadena)
		{
			Infragistics.WebUI.UltraWebNavigator.Node node1 = UltraWebTree1.Find((object)hijo);
			
			char [] Caracter = new char[1];
			Caracter[0] = Convert.ToChar(".");
			string [] Ret = hijo.Split(Caracter);

			if (node1==null)
			{
				//bucamos el padre 
				if (Ret.Length==1)
				{
					node2 = UltraWebTree1.Nodes.Add(hijo,(object)hijo);
					node2.Tag = hijo;
					node2.CheckBox  = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
					node2.Checked  = ChecarResgistro(hijo,Array_Mod);
					return;
				}
				node1 = UltraWebTree1.Find((object)ArrayUnir(hijo,false));
				node2 = (Infragistics.WebUI.UltraWebNavigator.Node)node1.Nodes.Add(NombreCadena);
				node2.Tag = hijo;
				node2.CheckBox  = Infragistics.WebUI.UltraWebNavigator.CheckBoxes.True;
				node2.Checked  = ChecarResgistro(hijo,Array_Mod);
			}
		}

		private void DoTree()
		{
			dS_Restricciones1.EC_RESTRICCIONES_PERFILES.Clear();

			DA_Restriccones.Fill(dS_Restricciones1.EC_RESTRICCIONES);
			DA_Res.SelectCommand.Parameters[0].Value = Sesion.WF_PerfilesE_Perfil_ID;
			DA_Res.Fill(dS_Restricciones1.EC_RESTRICCIONES_PERFILES);
				
			Array_Mod = new string [(int)dS_Restricciones1.EC_RESTRICCIONES_PERFILES.Rows.Count];

			for (int i =0 ; i<dS_Restricciones1.EC_RESTRICCIONES_PERFILES.Rows.Count; i++)
			{
				DS_Restricciones.EC_RESTRICCIONES_PERFILESRow FilaRestricciones = (DS_Restricciones.EC_RESTRICCIONES_PERFILESRow)dS_Restricciones1.EC_RESTRICCIONES_PERFILES.Rows[i];
				Array_Mod[i] = FilaRestricciones.RESTRICCION;
			}
            Arbol(Convert.ToInt32(Sesion.WF_PerfilesE_Perfil_ID));
		}

		private void Arbol(int PerfilID)
		{
			for(int j = 0 ;j < dS_Restricciones1.EC_RESTRICCIONES.Rows.Count; j++)
			{
				DS_Restricciones.EC_RESTRICCIONESRow Fila_Restriccion = (DS_Restricciones.EC_RESTRICCIONESRow)dS_Restricciones1.EC_RESTRICCIONES.Rows[j];
				char [] Separador = new char[]{Convert.ToChar(".")}; 
				string [] NumeroHijos = Fila_Restriccion.RESTRICCION.ToString().Split(Separador);
				string NombreRestriccion = Fila_Restriccion.RESTRICCION_NOMBRE.ToString();
				
				ArreglarArray(NumeroHijos);
				for (int i = 0 ; i< NumeroHijos.Length; i++)
				{
					string Valor_Hijo = ArrayUnir(NumeroHijos,i);
					AgregaHijo(Valor_Hijo,NombreRestriccion);
				}
			}
		}

		private void Habilitarcontroles(bool Caso,string Restriccion)
		{
			if(!Sesion.TienePermiso(CEC_RESTRICCIONES.S))
			{					
					UltraWebTree1.Visible = Caso;
					BGuardarCambios.Visible = Caso;
					BDeshacerCambios.Visible = Caso;
			}
		}
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aquí el código de usuario para inicializar la página
			Sesion = CeC_Sesion.Nuevo(this);
			Sesion.TituloPagina = "Configuración de Perfiles"; 
			Sesion.DescripcionPagina = "Asignación de Permisos a Perfiles"; 

			string [] Permiso = new string[10];
			Permiso[0] = CEC_RESTRICCIONES.S0Perfiles0Permisos;

			//if (!Sesion.ValidaAcceso("S.Perfiles.Permisos"))
			if (!Sesion.Acceso(Permiso,CIT_Perfiles.Acceso(Sesion.PERFIL_ID,this)))
			{
				CIT_Perfiles.CrearVentana(this,Sesion.MensajeVentanaJScript(),Sesion.TituloPagina,"Aceptar","WF_Main.aspx","","");
				Habilitarcontroles(false,Sesion.Restriccion.ToString());
				return;
			}

			//**************************************************
			if (!IsPostBack)
			{
				
                //CrecarTabla();				
                //int r = ComboPerfiles.FindByValue((object)Sesion.PERFIL_ID.ToString()).Row.Index;              
				DA_Restriccones.Fill(dS_Restricciones1.EC_RESTRICCIONES);
                //CeC_Grid.AsignaCatalogo(ComboPerfiles, "PERFIL_ID");
                if (Sesion.WF_PerfilesE_Perfil_ID == -1)
                {
                    Sesion.WF_PerfilesE_Perfil_ID = CeC_Autonumerico.GeneraAutonumerico("EC_PERFILES", "PERFIL_ID");
                    CeC_BD.EjecutaComando("INSERT INTO EC_PERFILES (PERFIL_ID, PERFIL_NOMBRE, PERFIL_BORRADO) VALUES (" + Sesion.WF_PerfilesE_Perfil_ID + ", 'Nuevo Perfil" + Sesion.WF_PerfilesE_Perfil_ID + "' , 1)");
                    LNombre.Text = "NUEVO PERFIL";

                }
                else
                {
                    LNombre.Text = CeC_BD.EjecutaEscalarString("SELECT PERFIL_NOMBRE FROM EC_PERFILES WHERE PERFIL_ID = " + Sesion.WF_PerfilesE_Perfil_ID);
                    txtPerfilNombre.Text = LNombre.Text;
                    CBBorrado.Checked = Convert.ToBoolean(CeC_BD.EjecutaEscalarInt("SELECT PERFIL_BORRADO FROM EC_PERFILES WHERE PERFIL_ID = " + Sesion.WF_PerfilesE_Perfil_ID));
                }
                Sesion.WF_PerfilesNodosSeleccion = "";
                node2.Nodes.Clear();
                UltraWebTree1.ClearAll();
                DoTree();
                string prueba = Sesion.WF_PerfilesE_Perfil_ID.ToString() ;
                
                txtPerfilID.Text = Sesion.WF_PerfilesE_Perfil_ID.ToString();
                
                //Agregar ModuloLog***
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Perfiles Permisos", 0, "Consulta de Perfiles", Sesion.SESION_ID);
			}
		}

		public string ArrayUnir(string [] ArrayCadena)
		{
			string valor = "";
			for (int i =0; i< ArrayCadena.Length; i++)
			{
				if (i ==ArrayCadena.Length)
				{
					valor += ArrayCadena[i] ;
					break;
				}
				else
				{
					valor += ArrayCadena[i] + ".";
				}
			}
			if (valor.Substring(valor.Length-1,1) == ".")
					valor = valor.Substring(0,valor.Length -1);	
			return valor; 			
		}

		public string ArrayUnir(string [] ArrayCadena,int Nivel)
		{
			string valor = "";
			for (int i =0; i< ArrayCadena.Length; i++)
			{
				if (i ==Nivel)
				{
					valor += ArrayCadena[i] ;
					break;
				}
				else
				{
					valor += ArrayCadena[i] + ".";
				}
			}
			return valor; 			
		}

		public void ArreglarArray(string [] ArrayParam)
		{
			for (int i = 0 ; i<ArrayParam.Length; i ++)
			{
				string valor1 = ArrayParam[i];

				if (valor1.Substring(0,1) =="'" )
					valor1 = valor1.Substring(1,valor1.Length-1);

				if (valor1.Substring(valor1.Length-1,1)=="'")
					valor1 = valor1.Substring(0,valor1.Length-1);

				ArrayParam[i] = valor1;
			}
		}

		private string ArreglarCadena(string CadenaArreglar)
		{
			string CadenaFinal = "";
			for (int i = 0 ; i<CadenaArreglar.Length; i ++)
			{
				string valor1 = CadenaArreglar.Substring(i,1);

				if (valor1!="'")
					 CadenaFinal =CadenaFinal+ valor1;
			}
				return CadenaFinal;
		}

		private string ArreglarCadena(string CadenaArreglar, char  [] Separadores)
		{
			string CadenaFinal = "";
			
			for (int i = 0 ; i<CadenaArreglar.Length; i ++)
			{
				string valor1 = CadenaArreglar.Substring(i,1);
				for (int j = 0; j< Separadores.Length; j ++)
				{
					if (valor1==Separadores[j].ToString())
					{
						valor1 = valor1.Remove(i,1);
						CadenaFinal = valor1;
					}
				}
			}
			return CadenaFinal;
		}

		public int ObtenerNivelPadre(string [] Hijos,int NumDirectorio)
		{
			if (NumDirectorio ==0)
				return -1;
			return NumDirectorio-1;
		}

		public string ObtenerPadre(string [] Hijos,int NumDirectorio)
		{
			if (NumDirectorio ==0)
				return "";
			return Hijos[NumDirectorio -1];
		}

		public static Array aRedimensionar(Array orgArray, Int32 tamaño) 
		{ 
			Type t = orgArray.GetType().GetElementType(); 
			Array nArray = Array.CreateInstance(t, tamaño); 
			Array.Copy(orgArray, 0, nArray, 0, Math.Min(orgArray.Length, tamaño)); 
			return nArray; 
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
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			this.Conexion = new System.Data.OleDb.OleDbConnection();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.DA_Restriccones = new System.Data.OleDb.OleDbDataAdapter();
			this.dS_Restricciones1 = new DS_Restricciones();
			this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
			this.DA_Perfiles = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand3 = new System.Data.OleDb.OleDbCommand();
			this.DA_Res = new System.Data.OleDb.OleDbDataAdapter();
			((System.ComponentModel.ISupportInitialize)(this.dS_Restricciones1)).BeginInit();
			this.UltraWebTree1.NodeChecked += new Infragistics.WebUI.UltraWebNavigator.NodeCheckedEventHandler(this.UltraWebTree1_NodeChecked);
			this.BDeshacerCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BDeshacerCambios_Click);
			this.BGuardarCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BGuardarCambios_Click);
			// 
			// Conexion
			// 
			this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = "SELECT RESTRICCION_ID, RESTRICCION, RESTRICCION_NOMBRE, RESTRICCION_BORRADO FROM " +
				"EC_RESTRICCIONES WHERE (RESTRICCION_BORRADO = 0) ORDER BY RESTRICCION_ID";
			this.oleDbSelectCommand1.Connection = this.Conexion;
			// 
			// DA_Restriccones
			// 
			this.DA_Restriccones.DeleteCommand = this.oleDbDeleteCommand1;
			this.DA_Restriccones.InsertCommand = this.oleDbInsertCommand1;
			this.DA_Restriccones.SelectCommand = this.oleDbSelectCommand1;
			this.DA_Restriccones.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "EC_RESTRICCIONES", new System.Data.Common.DataColumnMapping[] {
																																																						   new System.Data.Common.DataColumnMapping("RESTRICCION_ID", "RESTRICCION_ID"),
																																																						   new System.Data.Common.DataColumnMapping("RESTRICCION", "RESTRICCION"),
																																																						   new System.Data.Common.DataColumnMapping("RESTRICCION_NOMBRE", "RESTRICCION_NOMBRE"),
																																																						   new System.Data.Common.DataColumnMapping("RESTRICCION_BORRADO", "RESTRICCION_BORRADO")})});
			this.DA_Restriccones.UpdateCommand = this.oleDbUpdateCommand1;
			// 
			// dS_Restricciones1
			// 
			this.dS_Restricciones1.DataSetName = "DS_Restricciones";
			this.dS_Restricciones1.Locale = new System.Globalization.CultureInfo("es-MX");
			// 
			// oleDbSelectCommand2
			// 
			this.oleDbSelectCommand2.CommandText = "SELECT PERFIL_ID, PERFIL_NOMBRE, PERFIL_BORRADO FROM EC_PERFILES WHERE (PERFIL_B" +
				"ORRADO = 0)";
			this.oleDbSelectCommand2.Connection = this.Conexion;
			// 
			// DA_Perfiles
			// 
			this.DA_Perfiles.DeleteCommand = this.oleDbDeleteCommand2;
			this.DA_Perfiles.InsertCommand = this.oleDbInsertCommand2;
			this.DA_Perfiles.SelectCommand = this.oleDbSelectCommand2;
			this.DA_Perfiles.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "EC_PERFILES", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_ID", "PERFIL_ID"),
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_NOMBRE", "PERFIL_NOMBRE"),
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_BORRADO", "PERFIL_BORRADO")})});
			this.DA_Perfiles.UpdateCommand = this.oleDbUpdateCommand2;
			// 
			// oleDbSelectCommand3
			// 
			this.oleDbSelectCommand3.CommandText = @"SELECT EC_RESTRICCIONES.RESTRICCION_ID, EC_RESTRICCIONES.RESTRICCION, EC_RESTRICCIONES.RESTRICCION_NOMBRE, EC_RESTRICCIONES.RESTRICCION_BORRADO, EC_RESTRICCIONES_PERFILES.PERFIL_ID FROM EC_RESTRICCIONES_PERFILES, EC_RESTRICCIONES WHERE EC_RESTRICCIONES_PERFILES.RESTRICCION_ID = EC_RESTRICCIONES.RESTRICCION_ID AND (EC_RESTRICCIONES.RESTRICCION_BORRADO = 0) AND (EC_RESTRICCIONES_PERFILES.PERFIL_ID = ?)";
			this.oleDbSelectCommand3.Connection = this.Conexion;
			this.oleDbSelectCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERFIL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERFIL_ID", System.Data.DataRowVersion.Current, null));
			// 
			// DA_Res
			// 
			this.DA_Res.DeleteCommand = this.oleDbDeleteCommand3;
			this.DA_Res.InsertCommand = this.oleDbInsertCommand3;
			this.DA_Res.SelectCommand = this.oleDbSelectCommand3;
			this.DA_Res.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																							 new System.Data.Common.DataTableMapping("Table", "EC_RESTRICCIONES_PERFILES", new System.Data.Common.DataColumnMapping[] {
																																																						   new System.Data.Common.DataColumnMapping("RESTRICCION_ID", "RESTRICCION_ID"),
																																																						   new System.Data.Common.DataColumnMapping("RESTRICCION", "RESTRICCION"),
																																																						   new System.Data.Common.DataColumnMapping("RESTRICCION_NOMBRE", "RESTRICCION_NOMBRE"),
																																																						   new System.Data.Common.DataColumnMapping("RESTRICCION_BORRADO", "RESTRICCION_BORRADO"),
																																																						   new System.Data.Common.DataColumnMapping("PERFIL_ID", "PERFIL_ID")})});
			this.DA_Res.UpdateCommand = this.oleDbUpdateCommand3;
			((System.ComponentModel.ISupportInitialize)(this.dS_Restricciones1)).EndInit();

		}
		#endregion

		private void ComboPerfiles_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
		{
		}

        protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			///Libera controles de seleccion de perfil
			LCorrecto.Text ="";
			LError.Text = "";

			CeC_BD.EjecutaComando("Delete from EC_RESTRICCIONES_Perfiles where Perfil_id ="+Sesion.WF_PerfilesE_Perfil_ID);

			char [] caracter = new char[1];
			caracter[0] = Convert.ToChar("|");
			NodosPGuardar  = Sesion.WF_PerfilesNodosSeleccion.Split(caracter);
			
			try
			{
				for (int i = 0; i< NodosPGuardar.Length; i++)
				{
					string pruebs =  NodosPGuardar[i].ToString();
					Console.Write(NodosPGuardar[i].ToString());
					if (NodosPGuardar[i].Length > 0)
					{
						int Restriccion_id = 0;
						int ret = 0;
			
						string Restriccion = NodosPGuardar[i].ToString();
						Restriccion_id  = RegresaRestriccion_ID(Restriccion);
						CeC_BD.EjecutaComando("Insert into EC_RESTRICCIONES_perfiles(RESTRICCION_ID,PERFIL_ID) Values("+Restriccion_id+","+Convert.ToInt32(Sesion.WF_PerfilesE_Perfil_ID)+")");
					}
				}
                CeC_BD.EjecutaComando("UPDATE EC_PERFILES SET PERFIL_NOMBRE = '" + txtPerfilNombre.Text + "', PERFIL_BORRADO = " + Convert.ToInt32(CBBorrado.Checked) + " WHERE PERFIL_ID = " + Sesion.WF_PerfilesE_Perfil_ID);
				LCorrecto.Text = "Perfil Modificado";
			}
			catch(Exception ex)
			{
				LError.Text = "Error :" + ex.Message;
			}
		}

		private string [] CargarCambios(int ID_Perfil)
		{					
			DA_Res.SelectCommand.Parameters[0].Value  = ID_Perfil;
			DA_Res.Fill(dS_Restricciones1.EC_RESTRICCIONES_PERFILES);
			string [] ArrayValores = new string[(int)dS_Restricciones1.EC_RESTRICCIONES_PERFILES.Rows.Count];
			for (int i = 0 ; i <dS_Restricciones1.EC_RESTRICCIONES_PERFILES.Rows.Count;i++)
			{
				DS_Restricciones.EC_RESTRICCIONES_PERFILESRow Fila = (DS_Restricciones.EC_RESTRICCIONES_PERFILESRow)dS_Restricciones1.EC_RESTRICCIONES_PERFILES.Rows[i];
				string  Ret = ArreglarCadena(Fila.RESTRICCION.ToString());
				ArrayValores[i] = Ret; 
			}
			return ArrayValores;
		}

		private int RegresaRestriccion_ID(string Restriccion)
		{
			DA_Restriccones.Fill(dS_Restricciones1.EC_RESTRICCIONES);
			for (int i = 0 ; i< dS_Restricciones1.EC_RESTRICCIONES.Rows.Count; i++)
			{
				DS_Restricciones.EC_RESTRICCIONESRow RR = (DS_Restricciones.EC_RESTRICCIONESRow)dS_Restricciones1.EC_RESTRICCIONES.Rows[i];
				if (Restriccion==ArreglarCadena(RR.RESTRICCION.ToString()))
				{
					return (int)RR.RESTRICCION_ID;
				}
			}
			return -9999;
		}

		private void UltraWebTree1_NodeChecked(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeCheckedEventArgs e)
		{
			char [] caracter = new char[1];
			caracter[0] = Convert.ToChar("|");
			NodosPGuardar  = Sesion.WF_PerfilesNodosSeleccion.Split(caracter);
			
			if (e.Node.Checked)
			{
				for (int i = 0;i<NodosPGuardar.Length; i++)
				{
					if (NodosPGuardar[i].ToString() == e.Node.Tag.ToString())
					{
						//no se inserta
						return;
					}
				}
				//se inserta
				Sesion.WF_PerfilesNodosSeleccion = Sesion.WF_PerfilesNodosSeleccion + "|" + e.Node.Tag;
				//Array_Permisos_B[Convert.ToInt32(ComboPerfiles.SelectedRow.Cells[0].Text)] = Sesion.WF_PerfilesNodosSeleccion; 
			}
			else
			{
				//quitar
				Sesion.WF_PerfilesNodosSeleccion = "";
				for (int i = 0;i<NodosPGuardar.Length; i++)
				{
					if (NodosPGuardar[i].ToString() == e.Node.Tag.ToString())
						NodosPGuardar[i] ="";
				
					Sesion.WF_PerfilesNodosSeleccion = Sesion.WF_PerfilesNodosSeleccion + "|" + NodosPGuardar[i];
					//Array_Permisos_B[Convert.ToInt32(ComboPerfiles.SelectedRow.Cells[0].Text)] = Sesion.WF_PerfilesNodosSeleccion;
				}

				//se inserta

				//Sesion.WF_PerfilesNodosSeleccion = Sesion.WF_PerfilesNodosSeleccion + "|" + e.Node.Tag;
			}
		}

		private bool ChecarResgistro(string Registro,string [] Array_Resultados)
		{
			for (int i = 0 ; i<Array_Resultados.Length; i++)
			{
				if (Array_Resultados[i] != null)
				if (ArreglarCadena(Array_Resultados[i].ToString()) == Registro)
				{
					//aqui llenamos variable para lo de perfiles
					Sesion.WF_PerfilesNodosSeleccion = Sesion.WF_PerfilesNodosSeleccion + "|" + Registro;
					//Array_Permisos_B[Convert.ToInt32(ComboPerfiles.SelectedRow.Cells[0].Text)] = Sesion.WF_PerfilesNodosSeleccion;
					return true;
				}
			}
			return false;
		}

		protected void BEditarPerfiles_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			/// Libera los controles para editar el Perfil Seleccionado
		}

		private void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			/// Libera los controles para editar el Perfil Seleccionado
		}

        protected void ComboPerfiles_Load(object sender, EventArgs e)
        {  
        }

        protected void btnregresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.Redirige("WF_Perfiles.aspx");
        }
    }
}