//////////////////////////////////////////////////////////////////////////////
// This source code and all associated files and resources are copyrighted by
// the author(s). This source code and all associated files and resources may
// be used as long as they are used according to the terms and conditions set
// forth in The Code Project Open License (CPOL).
//
// Copyright (c) 2012 Jonathan Wood
// http://www.blackbeltcoder.com
//

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CsvFile;
using System.Data.SqlClient;

namespace CsvFileTest
{
    public partial class Form1 : Form
    {
        private const int MaxColumns = 64;
        protected string FileName;
        protected bool Modified;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeGrid();
            ClearFile();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveIfModified())
                ClearFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveIfModified())
            {
                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                    ReadFile(openFileDialog1.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileName != null)
                WriteFile(FileName);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = FileName;
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                WriteFile(saveFileDialog1.FileName);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveIfModified())
                Close();
        }

        /// <summary>
        /// //////////////////////////////////////////////
        /// </summary>

        private void InitializeGrid()
        {
            for (int i = 1; i <= MaxColumns; i++)
            {
                dataGridView1.Columns.Add(
                    String.Format("Column{0}", i),
                    String.Format("Column {0}", i));
            }
        }

        private void ClearFile()
        {
            dataGridView1.Rows.Clear();
            FileName = null;
            Modified = false;
        }

        private bool ReadFile(string filename)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                dataGridView1.Rows.Clear();
                List<string> columns = new List<string>();
                using (var reader = new CsvFileReader(filename))
                {
                    while (reader.ReadRow(columns))
                    {
                        dataGridView1.Rows.Add(columns.ToArray());
                    }
                }
                FileName = filename;
                Modified = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error reading from {0}.\r\n\r\n{1}", filename, ex.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return false;
        }

        private bool WriteFile(string filename)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                // Like Excel, we'll get the highest column number used,
                // and then write out that many columns for every row
                int numColumns = GetMaxColumnUsed();
                using (var writer = new CsvFileWriter(filename))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            List<string> columns = new List<string>();
                            for (int col = 0; col < numColumns; col++)
                                columns.Add((string)row.Cells[col].Value ?? String.Empty);
                            writer.WriteRow(columns);
                        }
                    }
                }
                FileName = filename;
                Modified = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error writing to {0}.\r\n\r\n{1}", filename, ex.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return false;
        }

        // Determines the maximum column number used in the grid
        private int GetMaxColumnUsed()
        {
            int maxColumnUsed = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    for (int col = row.Cells.Count - 1; col >= 0; col--)
                    {
                        if (row.Cells[col].Value != null)
                        {
                            if (maxColumnUsed < (col + 1))
                                maxColumnUsed = (col + 1);
                            continue;
                        }
                    }
                }
            }
            return maxColumnUsed;
        }

        private bool SaveIfModified()
        {
            if (!Modified)
                return true;

            DialogResult result = MessageBox.Show("The current file has changed. Save changes?", "Save Changes", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                if (FileName != null)
                {
                    return WriteFile(FileName);
                }
                else
                {
                    saveFileDialog1.FileName = FileName;
                    if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                        return WriteFile(saveFileDialog1.FileName);
                    return false;
                }
            }
            else if (result == DialogResult.No)
            {
                return true;
            }
            else // DialogResult.Cancel
            {
                return false;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Modified = true;
        }

        //FUNCIONES PARA TRADUCCION


        private static int generaAutonum(SqlConnection con)
        {
            int res;
            string query = "SELECT MAX(LOCALIZACION_ID) FROM EC_LOCALIZACIONES";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader result = cmd.ExecuteReader();
            result.Read();

            try
            {
                res = int.Parse(result[0].ToString());
                res++;
            }
            catch
            {
                res = 1;
            }
            result.Close();
            return res;
        }

        private static bool validaEtiqueta(SqlConnection con, string etiqueta, string idioma)
        {
            bool existe = false;
            
            string query = "SELECT LOCALIZACION_LLAVE FROM EC_LOCALIZACIONES WHERE LOCALIZACION_LLAVE LIKE '" + etiqueta + "' AND LOCALIZACION_IDIOMA LIKE '" + idioma + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader result = cmd.ExecuteReader();
            result.Read();

            try
            {
                result[0].ToString();
                existe = true;
            }
            catch
            {
                existe = false;
            }
            result.Close();
            
            return existe;
        }

        private static void actualizaEtiqueta(SqlConnection con, string etiqueta, string valorNuevo, string idioma)
        {
            valorNuevo = valorNuevo.Replace("'", "''");
            string query = "UPDATE EC_LOCALIZACIONES SET LOCALIZACION_ETIQUETA = '" + valorNuevo + "' WHERE LOCALIZACION_LLAVE LIKE '" + etiqueta + "' AND LOCALIZACION_IDIOMA = '" + idioma + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader result = cmd.ExecuteReader();
            result.Close();
        }

        private static void insertaEtiqueta(SqlConnection con, string etiqueta, string valorNuevo, string idioma)
        {
            int autonum = generaAutonum(con);
            valorNuevo = valorNuevo.Replace("'", "''");
            string query = "INSERT INTO EC_LOCALIZACIONES (LOCALIZACION_ID,LOCALIZACION_LLAVE, LOCALIZACION_IDIOMA, LOCALIZACION_ETIQUETA) VALUES(" + autonum.ToString() + ",'" + etiqueta + "','" + idioma + "','" + valorNuevo + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader result = cmd.ExecuteReader();
            result.Close();
        }

        private void tEtiquetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = 101;
            backgroundWorker1.RunWorkerAsync();
        }

        private void tCamposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = 101;
            backgroundWorker2.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string conectionString = "Data Source= bishop.entrytec.com.mx;Persist Security Info=True;User ID=sa;Password=1n4csys*;Initial Catalog=eClock";
            SqlConnection con = new SqlConnection(conectionString);
            string query = "";
            bool sigue = false;

            try
            {
                con.Open();
                sigue = true;
            }
            catch
            {
                sigue = false;
            }

            System.ComponentModel.BackgroundWorker Trabajo = sender as System.ComponentModel.BackgroundWorker;

            if (sigue == true)
            {
                for (int i = 1; i <= dataGridView1.Rows.Count - 2; i++)
                {
                    int P = i * 100 / (dataGridView1.Rows.Count - 2);
                    Trabajo.ReportProgress(P);

                    sigue = false;
                    query = "SELECT LOCALIZACION_LLAVE FROM EC_LOCALIZACIONES WHERE LOCALIZACION_LLAVE LIKE '" + dataGridView1[0, i].Value.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader result = cmd.ExecuteReader();
                    result.Read();

                    try
                    {
                        result[0].ToString();
                        sigue = true;
                        result.Close();
                    }
                    catch
                    {
                        sigue = false;
                        result.Close();
                    }


                    //COMPARA SI LA ETIQUETA YA SE ENCUENTRA EN LA BASE DE DATOS
                    if (sigue == true)
                    {
                        //ENTRA AQUI SI LA ETIQUETA YA SE ENCUENTRA EN LA BASE DE DATOS

                        //Tareas para etiquetas en el idioma por default
                        if (validaEtiqueta(con, dataGridView1[0, i].Value.ToString(), "") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[1, i].Value.ToString(), "");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[1, i].Value.ToString(), "");
                        }

                        //Tareas para etiquetas en el idioma español 

                        if (validaEtiqueta(con, dataGridView1[0, i].Value.ToString(), "es") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[2, i].Value.ToString(), "es");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[2, i].Value.ToString(), "es");
                        }

                        //Tareas para etiquetas en el idioma ingles

                        if (validaEtiqueta(con, dataGridView1[0, i].Value.ToString(), "en") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[3, i].Value.ToString(), "en");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[3, i].Value.ToString(), "en");
                        }

                        //Tareas para etiquetas en el idioma portugues

                        if (validaEtiqueta(con, dataGridView1[0, i].Value.ToString(), "de") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[4, i].Value.ToString(), "de");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[4, i].Value.ToString(), "de");
                        }

                        //Tareas para etiquetas en el idioma italiano

                        if (validaEtiqueta(con, dataGridView1[0, i].Value.ToString(), "it") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[5, i].Value.ToString(), "it");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[5, i].Value.ToString(), "it");
                        }

                        //Tareas para etiquetas en el idioma frances.

                        if (validaEtiqueta(con, dataGridView1[0, i].Value.ToString(), "fr") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[6, i].Value.ToString(), "fr");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[6, i].Value.ToString(), "fr");
                        }
                    }
                    else
                    {
                        //ENTRA SI LA ETIQUETA NO EXISTE EN LA BASE DE DATOS

                        //Introduce los valores de las etiquetas en todos los idiomas
                        insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[1, i].Value.ToString(), "");
                        insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[2, i].Value.ToString(), "es");
                        insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[3, i].Value.ToString(), "en");
                        insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[4, i].Value.ToString(), "de");
                        insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[5, i].Value.ToString(), "it");
                        insertaEtiqueta(con, dataGridView1[0, i].Value.ToString(), dataGridView1[6, i].Value.ToString(), "fr");
                    }

                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar a la base de datos verifique su conexión");
            }

            progressBar1.Visible = false;
            MessageBox.Show("Se han anctualizado los valores de las traducciones de etiquetas");
            con.Close();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
            }
            else
            {

            }
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string conectionString = "Data Source= bishop.entrytec.com.mx;Persist Security Info=True;User ID=sa;Password=1n4csys*;Initial Catalog=eClock";
            SqlConnection con = new SqlConnection(conectionString);
            string query = "";
            bool sigue = false;

            try
            {
                con.Open();
                sigue = true;
            }
            catch
            {
                sigue = false;
            }

            System.ComponentModel.BackgroundWorker Trabajo = sender as System.ComponentModel.BackgroundWorker;

            if (sigue == true)
            {
                for (int i = 1; i <= dataGridView1.Rows.Count - 3; i++)
                {
                    int P = i * 100 / (dataGridView1.Rows.Count - 2);
                    Trabajo.ReportProgress(P);
                    
                    sigue = false;
                    query = "SELECT LOCALIZACION_LLAVE FROM EC_LOCALIZACIONES WHERE LOCALIZACION_LLAVE LIKE '" + dataGridView1[0, i + 1].Value.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader result = cmd.ExecuteReader();
                    result.Read();

                    try
                    {
                        result[0].ToString();
                        sigue = true;
                        result.Close();
                    }
                    catch
                    {
                        sigue = false;
                        result.Close();
                    }


                    //COMPARA SI EL CAMPO YA SE ENCUENTRA EN LA BASE DE DATOS
                    if (sigue == true)
                    {
                        //ENTRA AQUI SI LA ETIQUETA YA SE ENCUENTRA EN LA BASE DE DATOS

                        //Tareas para campos en el idioma por default
                        if (validaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), "") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[1, i + 1].Value.ToString(), "");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[1, i + 1].Value.ToString(), "");
                        }

                        //Tareas para campos en el idioma por español
                        if (validaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), "es") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[7, i + 1].Value.ToString(), "es");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[7, i + 1].Value.ToString(), "es");
                        }

                        //Tareas para campos en el idioma por ingles
                        if (validaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), "en") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[9, i + 1].Value.ToString(), "en");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[9, i + 1].Value.ToString(), "en");
                        }

                        //Tareas para campos en el idioma por frances
                        if (validaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), "fr") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[11, i + 1].Value.ToString(), "fr");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[11, i + 1].Value.ToString(), "fr");
                        }

                        //Tareas para campos en el idioma por italiano
                        if (validaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), "it") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[13, i + 1].Value.ToString(), "it");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[13, i + 1].Value.ToString(), "it");
                        }

                        //Tareas para campos en el idioma por portugues
                        if (validaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), "de") == true)
                        {
                            actualizaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[15, i + 1].Value.ToString(), "de");
                        }
                        else
                        {
                            insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[15, i + 1].Value.ToString(), "de");
                        }
                    }
                    else
                    {
                        //ENTRA SI EL CAMPO NO EXISTE EN LA BASE DE DATOS

                        //Introduce los valores de los campos en todos los idiomas
                        insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[1, i + 1].Value.ToString(), "");
                        insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[7, i + 1].Value.ToString(), "es");
                        insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[9, i + 1].Value.ToString(), "en");
                        insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[11, i + 1].Value.ToString(), "de");
                        insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[13, i + 1].Value.ToString(), "it");
                        insertaEtiqueta(con, dataGridView1[0, i + 1].Value.ToString(), dataGridView1[15, i + 1].Value.ToString(), "fr");
                    }
                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar a la base de datos verifique su conexión");
            }

            MessageBox.Show("Se han anctualizado los valores de las traducciones de etiquetas");
            con.Close();
        }

        private void backgroundWorker2_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
