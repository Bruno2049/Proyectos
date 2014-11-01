using System;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using Infragistics.WebUI.UltraWebGrid;


	/// <summary>
	/// Descripción breve de CIT_Perfiles.
	/// </summary>
	public class CIT_Perfiles
	{
			
		//public System.Web.UI.Page CIT_Perfiles_m_Pagina;
	

		public static void CrearVentanaConfirmacion(System.Web.UI.Page  Pagina, string Mensaje, Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid)
		{
			string javaScr = "<script language =\"javascript\">" +
            "document.write (\"<div style = 'Z-INDEX: 101;LEFT: 30% ; TOP:40%; POSITION: absolute; height:200; width:40%;BORDER-RIGHT: #A0CDF8 thick double; BORDER-TOP: #A0CDF8 thick double; BORDER-LEFT: #0B4886 thick double; BORDER-BOTTOM: #0B4886 thick double; BACKGROUND-COLOR: #1380EC; '><Table border = 1 width = 100% Height = 100% ><tr><td align=center><font face='Arial Narrow' color='white'>" + Mensaje + "</font></td></tr></Table> " +
			" </div>\")" +
			"</script>";
			//Pagina.RegisterClientScriptBlock("CrearVentanaConfirmacion",javaScr);
		}
		public static void CrearVentana(System.Web.UI.Page  Pagina, string Mensaje,string Titulo,string Button1,string LinkButton1, string Button2,string LinkButton2)
		{
			string Boton1 = "";
			string Boton2 = "";
			
			if (Button2.Length>0)
				Boton2 = "<input type=\'Button\' id = \'b2\'  onclick= \'Redir2()\'  value = \'"+Button2+"\'>";
			
			if (Button1.Length>0)
				Boton1 = "<input type=\'Button\' id = \'b1\'  onclick= \'Redir()\' value = \'"+Button1+"\'>";


            string javaScr = "<script language =\"javascript\">" +
                "function Redir(){";
            if(LinkButton1.IndexOf(".aspx") > 0 || LinkButton1.IndexOf(".htm") > 0)
                javaScr += " window.location.replace(\"" + LinkButton1 + "\") ";
            else
                javaScr += " " + LinkButton1 +" ";
            javaScr += "}";

            javaScr += "function Redir2(){";
            javaScr += " window.location.replace(\"" + LinkButton2 + "\" )";
            javaScr += "}";

            javaScr += "document.write (\"<div style = 'Z-INDEX: 101;LEFT: 30% ; TOP:40%; POSITION: absolute; height:200; width:40%;BORDER-RIGHT: #A0CDF8 thick double; BORDER-TOP: #A0CDF8 thick double; BORDER-LEFT: #0B4886 thick double; BORDER-BOTTOM: #0B4886 thick double; BACKGROUND-COLOR: #1380EC;'><Table border = 0 width = 100% Height = 100%><tr><td style ='Height:0%'><font face='Arial Narrow' color=''><b>" + Titulo + "</b></font></td></tr><tr><td align=center><font face='Arial Narrow' color='white'>" + Mensaje + "</font></td></tr><tr><td align = center>" + Boton1 + "    " + Boton2 + "</td></tr></Table>";
            javaScr += " </div>\")";
            javaScr += "</script>";

			Pagina.RegisterClientScriptBlock("CrearVentana",javaScr);
		}

		

		public static string [] Acceso(int Pefil_ID,System.Web.UI.Page Pagina)
		{
			int Contador = 1;
            string ConexionString = CeC_BD.CadenaConexion();
			string [] ResultadosAcceso = new string[1];
			OleDbConnection	Conexion = new OleDbConnection(ConexionString);

			Conexion.Open();
			
			string Query = "Select * from EC_RESTRICCIONES,EC_RESTRICCIONES_perfiles where EC_RESTRICCIONES_perfiles.restriccion_id = EC_RESTRICCIONES.restriccion_id and   EC_RESTRICCIONES_perfiles.perfil_id  = " +Pefil_ID+ " order by EC_RESTRICCIONES.RESTRICCION_ID";
			
			char [] Caracter = new char[1];
			Caracter[0] = Convert.ToChar(".");

			//string [] ArrayPremisos = Restricciones.Split(Caracter);
			//string Permiso = "";
			
			OleDbCommand Commando = new OleDbCommand(Query,Conexion);
			OleDbDataReader Lector;
			Lector = Commando.ExecuteReader();
				
				try
				{
					while(Lector.Read())
					{
						string Valor = "";
						try
						{
							Valor = Convert.ToString(Lector.GetValue(1));
							Contador++;
							ResultadosAcceso = (string [])Redimensionar(ResultadosAcceso,Contador);
							ResultadosAcceso[Contador-1] = ArreglarCadena(Valor);
						}
						catch(Exception ex)
						{
							Valor = "";
						}
					}
					Lector.Close();
					Conexion.Dispose();
                    GuardarValoresSession(Pagina, ResultadosAcceso);
				
					

					
					return ResultadosAcceso;
				}
				catch(Exception err)
				{
					Lector.Close();
					Conexion.Dispose();
								
					string  s = err.Message;
				GuardarValoresSession(Pagina,ResultadosAcceso);
					return ResultadosAcceso;
				}
			}
		
		private static void GuardarValoresSession(System.Web.UI.Page PaginaWeb,string [] ArrayCadena)
		{

			string Contenido = "";

			for(int i = 0 ; i< ArrayCadena.Length; i++)
			{
				Contenido += ArrayCadena[i] + "|";							

			}
            CeC_Sesion.GuardaStringSesion(PaginaWeb, "PermisosComparacion", Contenido);

		}
		public static string obtenerValorWebconfig(string Variable)
		{
			AppSettingsReader LectorCong = new AppSettingsReader();
			string ConexionString = ((string)LectorCong.GetValue(Variable,typeof(string)));
			return ConexionString;
		}
		public static string ArreglarCadena(string CadenaArreglar)
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
		public static string ArrayUnir(string [] ArrayCadena,int Nivel)
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
	
		public static Array Redimensionar(Array orgArray, Int32 tamaño) 
		{ 
			Type t = orgArray.GetType().GetElementType(); 
			Array nArray = Array.CreateInstance(t, tamaño); 
			Array.Copy(orgArray, 0, nArray, 0, Math.Min(orgArray.Length, tamaño)); 
			return nArray; 
		} 

	}

