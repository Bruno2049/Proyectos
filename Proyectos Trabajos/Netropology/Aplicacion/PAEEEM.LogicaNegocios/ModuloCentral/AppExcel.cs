
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Reflection;
using PAEEEM.Entidades.ModuloCentral;


namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class AppExcel
    {
        private string PathFile;

        public AppExcel(string sPathFile)
        {
            PathFile = sPathFile;
        }


        private DataTable LeerExcel()
        {
            //Creamos la cadena de conexión con el fichero excel
            var cb = new OleDbConnectionStringBuilder();
            cb.DataSource = PathFile;

            if (Path.GetExtension(PathFile).ToUpper() == ".XLS")
            {
                cb.Provider = "Microsoft.Jet.OLEDB.4.0";
                cb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=0;");
            }
            else if (Path.GetExtension(PathFile).ToUpper() == ".XLSX")
            {
                cb.Provider = "Microsoft.ACE.OLEDB.12.0";
                cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");
            }

            var dt = new DataTable("Datos");

            using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))
            {
                //Abrimos la conexión
                conn.Open();

                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [Hoja1$]";

                    //Guardamos los datos en el DataTable
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }


                //Cerramos la conexión
                conn.Close();
            }
            DeleteExcel();
            return dt;
        }

        public List<LayOutExcel> ReadExcel()
        {
            var lista = new List<LayOutExcel>();

            var Datos = LeerExcel();
            int j = 2;
            foreach (DataRow row in Datos.Rows)
            {
             var item = new LayOutExcel();

             if (string.IsNullOrEmpty(row[0].ToString()) && string.IsNullOrEmpty(row[1].ToString())) break;
             
             item.Fabricante = row[0].ToString();
             item.NoRegistro = j.ToString(CultureInfo.InvariantCulture);
             item.TipoProducto = row[1].ToString();
             item.NombreProducto = row[2].ToString();
             item.Marca = row[3].ToString();
             item.Modelo = row[4].ToString();
             item.PrecioMaximo = row[5].ToString();
             item.Capacidad = row[6].ToString();
             lista.Add(item);
             j++;
            }

            return lista;
           
        }

        public List<LayOutExcel> ReadExcelSubEstacionAerea()
        {

            var lista = new List<LayOutExcel>();

            var Datos = LeerExcel();
            int j = 2;
            foreach (DataRow row in Datos.Rows)
            {
             var item = new LayOutExcel();

             if (string.IsNullOrEmpty(row[0].ToString()) && string.IsNullOrEmpty(row[1].ToString())) break;
             
             item.NoRegistro = j.ToString(CultureInfo.InvariantCulture);
             item.Fabricante = row[0].ToString();
             item.Modelo = row[1].ToString();
             item.TipoProducto = row[2].ToString();
             item.Marca = row[3].ToString();
             item.Capacidad = row[4].ToString();
             item.RelacionTrans = row[5].ToString();
             item.Precio = row[6].ToString();
             item.PrecioTotalModTransformador = row[7].ToString();
             item.PrecioConectorT = row[8].ToString();
             item.PrecioAisladorTension = row[9].ToString();
             item.PrecioAisladorSoporte = row[10].ToString();
             item.PrecioCabGuiaApatarayos = row[11].ToString();
             item.PrecioCabGuiaCortaCto = row[12].ToString();
             item.PrecioCabGuiaTransform = row[13].ToString();
             item.PrecioApartarrayos = row[14].ToString();
             item.PrecioCortaCto = row[15].ToString();
             item.PrecioFusible = row[16].ToString();
             item.PrecioTotalModMediaTension = row[17].ToString();
             item.PrecioCabTREqMT = row[18].ToString();
             item.PrecioHerrajes = row[19].ToString();
             item.PrecioPoste = row[20].ToString();
             item.PrecioSistemaTierra = row[21].ToString();
             item.PrecioCabConST = row[22].ToString();
             item.PrecioTotalModAcceYProt = row[23].ToString();
             item.PrecioTotalSubestacion = row[24].ToString();

             lista.Add(item);
             j++;
            }

            return lista;
        }

        public List<LayOutExcel> ReadExcelSubEstacionConAcometida()
        {
            var lista = new List<LayOutExcel>();

            var Datos = LeerExcel();
            int j = 2;
            foreach (DataRow row in Datos.Rows)
            {
             var item = new LayOutExcel();

             if (string.IsNullOrEmpty(row[0].ToString()) && string.IsNullOrEmpty(row[1].ToString())) break;
             
             item.NoRegistro = j.ToString(CultureInfo.InvariantCulture);
             item.Fabricante = row[0].ToString();
             item.Modelo = row[1].ToString();
             item.Marca = row[2].ToString();
             item.Capacidad = row[3].ToString();
             item.RelacionTrans = row[4].ToString();
             item.Precio = row[5].ToString();
             item.PrecioFusibleMT = row[6].ToString();
             item.PrecioTotalModTransformador = row[7].ToString();
             item.PrecioConectorT = row[8].ToString();
             item.PrecioAisladorTension = row[9].ToString();
             item.PrecioAisladorSoporte = row[10].ToString();
             item.PrecioCabGuiaApatarayos = row[11].ToString();
             item.PrecioCabGuiaCortaCto = row[12].ToString();
             item.PrecioCabGuiaTransform = row[13].ToString();
             item.PrecioApartarrayos = row[14].ToString();
             item.PrecioCortaCto = row[15].ToString();
             item.PrecioFusible = row[16].ToString();
             item.PrecioCabGuiaConAcometida = row[17].ToString();
             item.PrecioConexMTAcometida = row[18].ToString();
             item.PrecioTotalModMediaTension = row[19].ToString();
             item.PrecioCabTREqMT = row[20].ToString();
             item.PrecioHerrajes = row[21].ToString();
             item.PrecioPoste = row[22].ToString();
             item.PrecioSistemaTierra = row[23].ToString();
             item.PrecioCabConST = row[24].ToString();
             item.PrecioTotalModAcceYProt = row[25].ToString();
             item.PrecioTotalSubestacion = row[26].ToString();

             lista.Add(item);
             j++;
            }

            return lista;
           
        }

        public List<LayOutExcel> ReadExcelSubEstacionIntRed()
        {
            var lista = new List<LayOutExcel>();

            var Datos = LeerExcel();
            int j = 2;
            foreach (DataRow row in Datos.Rows)
            {
                var item = new LayOutExcel();

                if (string.IsNullOrEmpty(row[0].ToString()) && string.IsNullOrEmpty(row[1].ToString())) break;
                
                item.NoRegistro = j.ToString(CultureInfo.InvariantCulture);
                item.Fabricante = row[0].ToString();
                item.Modelo = row[1].ToString();
                item.Marca = row[2].ToString();
                item.Capacidad = row[3].ToString();
                item.RelacionTrans = row[4].ToString();
                item.Precio = row[5].ToString();
                item.PrecioFusibleMT = row[6].ToString();
                item.PrecioTotalModTransformador = row[7].ToString();
                item.PrecioEmpalmes = row[8].ToString();
                item.PrecioExtremos = row[9].ToString();
                item.PrecioTotalModMediaTension = row[10].ToString();
                item.PrecioCabTREqMT = row[11].ToString();
                item.PrecioHerrajes = row[12].ToString();
                item.PrecioPoste = row[13].ToString();
                item.PrecioSistemaTierra = row[14].ToString();
                item.PrecioCabConST = row[15].ToString();
                item.PrecioTotalModAcceYProt = row[16].ToString();
                item.PrecioTotalSubestacion = row[17].ToString();

                lista.Add(item);
                j++;
            }

            return lista;
        }

        public List<LayOutExcel> ReadExcelBancoCapacitores()
        {
            var lista = new List<LayOutExcel>();
            var Datos = LeerExcel();
            int j = 2;
            foreach (DataRow row in Datos.Rows)
            {
             var item = new LayOutExcel();

                    if (string.IsNullOrEmpty(row[0].ToString()) && string.IsNullOrEmpty(row[1].ToString())) break;
             
                    item.NoRegistro = j.ToString(CultureInfo.InvariantCulture);
                    item.Fabricante = row[0].ToString();
                    item.TipoProducto = row[1].ToString();
                    item.Marca = row[2].ToString();
                    item.Modelo = row[3].ToString();
                    item.Capacidad = row[4].ToString();
                    item.PrecioMaximo = row[5].ToString();
                    item.TipoEncapsulado = row[6].ToString();
                    item.TipoDielectrico = row[7].ToString();
                    item.IncluyeProteccionInterna= row[8].ToString();
                    item.TipoProteccionExterna = row[9].ToString();
                    item.MaterialCubierta = row[10].ToString();
                    item.Perdidas = row[11].ToString();
                    item.ProteccionInternaSobrecorriente = row[12].ToString();
                    item.ProteccionVsFuego = row[13].ToString();
                    item.ProteccionVsExplosion = row[14].ToString();
                    item.Anclaje = row[15].ToString();
                    item.TerminalTierra = row[16].ToString();
                    item.TipoControlador = row[17].ToString();
                    item.ProteccionVsSobreCorriente = row[18].ToString();
                    item.ProteccionVsSobreTemperatura = row[19].ToString();
                    item.ProteccionVsSobreVoltaje = row[20].ToString();
                    item.BloqueoDisplay = row[21].ToString();
                    item.TipoComunicacion = row[22].ToString();
                    item.ProteccionGabiente = row[23].ToString();
                    
                    lista.Add(item);
                    j++;
                }

                return lista;
            
        }

        private void DeleteExcel()
        {
            if(System.IO.File.Exists(PathFile))
               System.IO.File.Delete(PathFile);
        }

    }
}
