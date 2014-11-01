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
using System.Data;


namespace eClock5.Vista.Empleados
{
    /// <summary>
    /// Lógica de interacción para Importar.xaml
    /// </summary>
    public partial class Importar : UserControl
    {
        public Importar()
        {
            InitializeComponent();
        }

        private void Btn_RutaArchivo_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                // Set filter for file extension and default file extension
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Excel Files (.xls)|*.xls|Excel Files (.xlsx)|*.xlsx|Text files tab separated (.txt)|*.txt|comma-separated values (.csv)|*.csv|All files (*.*)|*.*";

                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox
                if (result == true)
                {
                    Tbx_RutaArchvo.Text = dlg.FileName;
                    /*

                    switch (System.IO.Path.GetExtension(dlg.FileName))
                    {
                        case ".xlsx":
                            CeC_BD BDXls = new CeC_BD(@"Provider=Microsoft.Jet.OLEDB.4.0;" + 
                            @"Data Source=" + dlg.FileName  + ";" + 
                            @"Extended Properties=" + Convert.ToChar(34).ToString() + 
                            @"Excel 8.0;");

                            System.Data.DataSet DS =  BDXls.lEjecutaDataSet(" SELECT * FROM [" 
                            + 0 + " $ " + 11 + " ] ");

                            break;
                    }
                    string Modelo = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                    string Datos = System.IO.File.ReadAllText(dlg.FileName);
                     * */
                }
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }


        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            string Error = "";
            CeC_BD BDImporta = null;
            string Tabla = "";
            switch (Control.Nombre)
            {
                case "Btn_Regresar":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case "Btn_Guardar":
                    switch (System.IO.Path.GetExtension(Tbx_RutaArchvo.Text))
                    {
                        case ".xls":
                        case ".xlsx":
                            BDImporta = new CeC_BD("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Tbx_RutaArchvo.Text + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'");
                            DataTable schemaTable = BDImporta.lObtenTablas();
                            if (schemaTable == null)
                            {
                                Error = "No se pudo abrir el archivo";
                            }
                            else
                                if (schemaTable.Rows.Count < 1)
                                {
                                    Error = "Sin hojas";
                                }
                                else
                                    Tabla = "[" + schemaTable.Rows[0]["TABLE_NAME"].ToString() + "]";
                            break;
                        case ".txt":
                            BDImporta = new CeC_BD("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Path.GetDirectoryName(Tbx_RutaArchvo.Text) + ";Extended Properties='text;HDR=YES;FMT=TabDelimited'");
                            Tabla = "[" + System.IO.Path.GetFileName(Tbx_RutaArchvo.Text) + "]";
                            break;
                        case ".csv":
                            BDImporta = new CeC_BD("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Path.GetDirectoryName(Tbx_RutaArchvo.Text) + ";Extended Properties='text;HDR=YES;FMT=Delimited'");
                            Tabla = "[" + System.IO.Path.GetFileName(Tbx_RutaArchvo.Text) + "]";
                            break;
                    }
                    break;
            }


            if (Tabla != "")
            {
                DataSet DS = BDImporta.lEjecutaDataSet("SELECT * FROM " + Tabla);
                if (DS != null)
                {
                    if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                    {
                        string Json = CeC_BD.DataSet2JsonList(DS);
                        eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                        Sesion.MuestraMensaje("Importando " + DS.Tables[0].Rows.Count + " registros");
                        eClockBase.Controladores.Personas Persona = new eClockBase.Controladores.Personas(Sesion);
                        Persona.AgregarPersonasEvent += Persona_AgregarPersonasEvent;
                        Persona.AgregarPersonas(Json);
                    }
                }
            }
        }

        void Persona_AgregarPersonasEvent(bool Guardado)
        {

        }
    }
}
