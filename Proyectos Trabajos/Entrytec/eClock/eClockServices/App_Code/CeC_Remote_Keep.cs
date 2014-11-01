using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Data.OleDb;
using System.IO;

namespace eClock
{
    /// <summary>
    /// Descripción breve de CeC_Remote_Keep.
    /// </summary>
    public class CeC_Remote_Keep
    {
        public CeC_Remote_Keep()
        {
            //
            // TODO: agregar aquí la lógica del constructor
            //
        }

        public string GuardarChecadas(int IDTerminal)
        {
            object Obj = CeC_BD.EjecutaDataSet("SELECT TERMINALES_DEXTRAS_ID, TERMINAL_ID, TIPO_TERM_DEXTRAS_ID, TERMINALES_DEXTRAS_TEXTO1, TERMINALES_DEXTRAS_TEXTO2 FROM EC_TERMINALES_DEXTRAS WHERE TERMINAL_ID=" + IDTerminal);
            if (Obj == null)
                return "";
            DataSet DS = (DataSet)Obj;
            System.IO.StringWriter SW = new System.IO.StringWriter();
            DS.WriteXml(SW, System.Data.XmlWriteMode.IgnoreSchema);

            return SW.ToString();
            /*
			DataSet DS = new DataSet("0");
			DataTable DT = new DataTable("Terminales");
			DataRow DR;

			DT.Columns.Add("TERMINALES_DEXTRAS_ID",System.Type.GetType("System.Decimal"));
			DT.Columns.Add("TERMINAL_ID",System.Type.GetType("System.Decimal"));
			DT.Columns.Add("TIPO_TERM_DEXTRAS_ID",System.Type.GetType("System.Decimal"));
			DT.Columns.Add("TERMINALES_DEXTRAS_TEXTO1",System.Type.GetType("System.String"));
			DT.Columns.Add("TERMINALES_DEXTRAS_TEXTO2",System.Type.GetType("System.String"));

			string Qry = "SELECT TERMINALES_DEXTRAS_ID, TERMINAL_ID, TIPO_TERM_DEXTRAS_ID, TERMINALES_DEXTRAS_TEXTO1, TERMINALES_DEXTRAS_TEXTO2 FROM EC_TERMINALES_DEXTRAS WHERE TERMINAL_ID=" +IDTerminal;



			OleDbConnection Conexion = (OleDbConnection)CeC_BD.ObtenConexion();
			
			if (Conexion.State != System.Data.ConnectionState.Open)
						Conexion.Open();
			
			OleDbCommand commando = new OleDbCommand(Qry,Conexion);
			OleDbDataReader Lector;
			Lector = commando.ExecuteReader();

				
				while(Lector.Read())
				{
					try
					{
						DR = DT.NewRow();
						DR[0] = Lector.GetInt32(0);
						DR[1] = Lector.GetInt32(1);
						DR[2] = Lector.GetInt32(2);
						DR[3] = Lector.GetString(3);
						DR[4] = Lector.GetString(4);
						DT.Rows.Add(DR);
					}
					catch
					{

					}
				}

				DS.Tables.Add(DT);
				Lector.Close();
				Conexion.Close();

			System.IO.StringWriter SW = new System.IO.StringWriter();
			DS.WriteXml(SW, System.Data.XmlWriteMode.IgnoreSchema); 
			
			return SW.ToString();*/

        }
    }
}
