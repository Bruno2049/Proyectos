﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using PubliPayments.Entidades;
using PubliPayments.Negocios;

namespace PubliPayments.Reportes.ReporteGestionMovil
{
    public partial class GestionXSolucion : XtraReport
    {
        private readonly string _delegacion;
        private readonly string _fechaCarga;
        private readonly string _estFinal;
        private readonly string _despacho;
        private readonly string _supervisor;
        private readonly List<string> _listaColumnasVisibles = new List<string>();
        private readonly int _primeraTabla;
        private string _tipoFormulario;

        public GestionXSolucion(string delegacion = "", string fechaCarga = "", string estFinal = "", string despacho = "", string supervisor = "",string tipoFormulario="")
        {
            InitializeComponent();
            xrPanel1.BackColor = Color.FromArgb(65, 51, 57);

            _delegacion = delegacion;
            _fechaCarga = fechaCarga;
            _estFinal = estFinal;
            _despacho = despacho;
            _supervisor = supervisor;
            _tipoFormulario = tipoFormulario;

            if (_supervisor != "" && _despacho != "")
            {
                _primeraTabla = 3;
            }
            else
            {
                _primeraTabla = _despacho != "" ? 2 : 1;
            }

            switch (Config.AplicacionActual().idAplicacion)
            {
                case 1:
                    xrLabel5.Text = @"Instituto del Fondo Nacional de la Vivienda para los Trabajadores (Infonavit)";
                    xrLabel2.Text = @"  ©2013  -  ";
                    xrPictureBox3.Visible = true;
                    break;
                default:
                    xrLabel5.Text = "";
                    xrLabel2.Text = "";
                    xrPictureBox3.Visible = false;
                    break;
            }

            xrLabel3.Text = @"Última Actualización: " + new EntGestionMovil().ObtenerFechaActualizacion();
        }

        DataTable TablaDatosSoluciones(string delegacion = "", string fechaCarga = "", string estFinal = "",string tipoFormulario="")
        {

            var lista = new GestionMovil().GestionXSolucion(delegacion ?? "", fechaCarga ?? "", estFinal ?? "", tipoFormulario ?? "");
            //var lista = new EntGestionMovil().GestionXSolucion(delegacion ?? "", fechaCarga ?? "", estFinal ?? "", tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "idDespacho" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Despacho" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Sin resultado final" });

            var titulosTabla = new string[7];
            titulosTabla[0] = "Móvil";
            titulosTabla[1] = "Sin Asignar";
            titulosTabla[2] = "Sin Asignar 2 Visita";
            titulosTabla[3] = "Sin asignar 3 Visita";
            titulosTabla[4] = "Válidas";
            titulosTabla[5] = "Válidas Aprobadas";
            titulosTabla[6] = "Válidas Sin Aprobar";

            for (var i = 2; i < 9; i++)
            {
                if (lista.Tables[1].Rows[0][i].ToString() != "0")
                {
                    var columna = new DataColumn { ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture)) };
                    tabla.Columns.Add(columna);
                    columnasCancelar += ((i - 2) + "|");
                    _listaColumnasVisibles.Add("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture));
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idDespacho"] = row["idDominio"];
                fila["Despacho"] = row["NombreDominio"];
                fila["Sin resultado final"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idDespacho"] = 0;
            fila2["Despacho"] = "Total";
            fila2["Sin resultado final"] = lista.Tables[1].Rows[0][1];
            for (var d = 0; d < (listaColumnas.Length - 1); d++)
            {
                fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value ? "0" : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                totalFila2 += Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
            }
            totalFila2 += Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            return tabla;
        }

        private DataTable TablaDatosSupervisor(string delegacion = "", string fechaCarga = "", string estFinal = "", string despacho = "", string tipoFormulario="")
        {
            var lista = new GestionMovil().GestionXSolucionDespacho(delegacion ?? "", fechaCarga ?? "", estFinal ?? "", despacho ?? "", tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn {ColumnName = "idUsuarioPadre"});
            tabla.Columns.Add(new DataColumn { ColumnName = "Usuario" });
            tabla.Columns.Add(new DataColumn {ColumnName = "Nombre"});
            tabla.Columns.Add(new DataColumn {ColumnName = "Sin resultado final"});

            for (var i = 2; i < 9; i++)
            {
                if (_primeraTabla != 2)
                {
                    if (_listaColumnasVisibles.Contains("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture)))
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
                else
                {
                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                        _listaColumnasVisibles.Add("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
            var columna1 = new DataColumn {ColumnName = "Total"};
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idUsuarioPadre"] = row["idUsuarioPadre"];
                fila["Usuario"] = row["Usuario"];
                fila["Nombre"] = row["Nombre"];
                fila["Sin resultado final"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila +=
                        Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);
                
            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idUsuarioPadre"] = 0;
            fila2["Usuario"] = "";
            fila2["Nombre"] = "Total";
            fila2["Sin resultado final"] = lista.Tables[1].Rows[0][1];
            for (var d = 0; d < (listaColumnas.Length - 1); d++)
            {
                fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value ? "0" : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                totalFila2 += Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
            }
            totalFila2 += Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);
                
            
            return tabla;
        }

        DataTable TablaDatosGestor(string delegacion = "", string fechaCarga = "", string estFinal = "", string despacho = "", string supervisor = "", string tipoFormulario="")
        {
            var lista = new GestionMovil().GestionXSolucionSupervisor(delegacion ?? "", fechaCarga ?? "", estFinal ?? "", despacho ?? "", supervisor ?? "", tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "idUsuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Usuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Nombre" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Sin resultado final" });

            for (var i = 2; i < 9; i++)
            {
                if (_primeraTabla != 3)
                {
                    if (_listaColumnasVisibles.Contains("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture)))
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
                else
                {
                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                        _listaColumnasVisibles.Add("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idUsuario"] = row["idUsuario"];
                fila["Usuario"] = row["Usuario"];
                fila["Nombre"] = row["Nombre"];
                fila["Sin resultado final"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idUsuario"] = 0;
            fila2["Usuario"] = "";
            fila2["Nombre"] = "Total";
            fila2["Sin resultado final"] = lista.Tables[1].Rows[0][1];
            for (var d = 0; d < (listaColumnas.Length - 1); d++)
            {
                fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value ? "0" : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                totalFila2 += Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
            }
            totalFila2 += Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            return tabla;
        }

        private void GestionXSolucion_BeforePrint(object sender, PrintEventArgs e)
        {
            if (_supervisor != "" && _despacho != "")
            {
                Detail.Controls.Add(CrearTablaGestores(_despacho, _supervisor, -1));
            }
            else
            {
                Detail.Controls.Add(_despacho != "" ? CrearTablaSupervisores(_despacho, -1) : CreateXrTable());
            }
        }

        public XRTable CreateXrTable()
        {
            var table = new XRTable { Borders = BorderSide.All };

            var titulosTabla = new string[7];
            titulosTabla[0] = "Móvil";
            titulosTabla[1] = "Sin Asignar";
            titulosTabla[2] = "Sin Asignar 2 Visita";
            titulosTabla[3] = "Sin asignar 3 Visita";
            titulosTabla[4] = "Válidas";
            titulosTabla[5] = "Válidas Aprobadas";
            titulosTabla[6] = "Válidas Sin Aprobar";

            var tablaValoresBd = TablaDatosSoluciones(_delegacion, _fechaCarga, _estFinal, _tipoFormulario);
            if (tablaValoresBd.Columns.Count > 20)
            {
                var tamanioAnterior = (PageWidth - Margins.Left - Margins.Right);
                Landscape = true;
                var tamanioPagHoriz = PageWidth - Margins.Left - Margins.Right;
                var punto = (tamanioPagHoriz - tamanioAnterior);
                xrLabel3.Location = Point.Add(xrLabel3.Location, new Size(punto, 0));
                xrLabel6.Location = Point.Add(xrLabel6.Location, new Size(punto, 0));
                xrLabel7.Location = Point.Add(xrLabel7.Location, new Size(punto, 0));
                xrPictureBox4.Location = Point.Add(xrPictureBox4.Location, new Size(punto, 0));
                xrPanel1.WidthF = tamanioPagHoriz;
                xrPanel2.WidthF = tamanioPagHoriz;
                xrLabel1.WidthF = tamanioPagHoriz;
                labelFiltros.WidthF = tamanioPagHoriz;
                LabelIndicadorTitulo.WidthF = tamanioPagHoriz;
                xrLabel3.WidthF = tamanioPagHoriz;
            }
            table.BeginInit();

            var rowTitulos = new XRTableRow();
            int tamanioColNumer = ((PageWidth - Margins.Left - Margins.Right - 300) / (tablaValoresBd.Columns.Count - 3));

            for (var i = 1; i < tablaValoresBd.Columns.Count; i++)
            {
                var cell = new XRTableCell
                {
                    Text =
                        tablaValoresBd.Columns[i].Caption.IndexOf("Valor", StringComparison.Ordinal) != -1
                            ? titulosTabla[Convert.ToInt32(tablaValoresBd.Columns[i].Caption.Substring(5))]
                            : tablaValoresBd.Columns[i].Caption,
                    Width = i > 2 ? tamanioColNumer : 150
                };
                rowTitulos.Cells.Add(cell);
                rowTitulos.BackColor = Color.FromArgb(130, 130, 130);
                rowTitulos.ForeColor = Color.FromArgb(255, 255, 255);
            }
            table.Rows.Add(rowTitulos);

            for (int i = 0; i < tablaValoresBd.Rows.Count; i++)
            {
                var row = new XRTableRow();
                int numColumnas = tablaValoresBd.Columns.Count;
                for (int j = 1; j < numColumnas; j++)
                {
                    var cell = new XRTableCell
                    {
                        Text = tablaValoresBd.Rows[i][j].ToString(),
                        CanShrink = true,
                        CanGrow = true,
                        Width = j > 2 ? tamanioColNumer : 150
                    };
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
                if (i != tablaValoresBd.Rows.Count - 1)
                {
                    var rowDespacho = new XRTableRow();
                    var celdaSubreportDespacho = new XRTableCell
                    {
                        WidthF = PageWidth - Margins.Left - Margins.Right,
                        Height = 10,
                        Borders = BorderSide.None
                    };
                    celdaSubreportDespacho.Controls.Add(CrearTablaSupervisores(tablaValoresBd.Rows[i][0].ToString(), tamanioColNumer));
                    rowDespacho.Cells.Add(celdaSubreportDespacho);
                    table.Rows.Add(rowDespacho);
                }
            }

            table.BeforePrint += table_BeforePrint;
            table.AdjustSize();
            table.EndInit();
            return table;
        }
        public XRTable CrearTablaSupervisores(string despacho, int tamanioColNumer)
        {
            var table = new XRTable { Borders = BorderSide.All };
            
            var titulosTabla = new string[7];
            titulosTabla[0] = "Móvil";
            titulosTabla[1] = "Sin Asignar";
            titulosTabla[2] = "Sin Asignar 2 Visita";
            titulosTabla[3] = "Sin asignar 3 Visita";
            titulosTabla[4] = "Válidas";
            titulosTabla[5] = "Válidas Aprobadas";
            titulosTabla[6] = "Válidas Sin Aprobar";

            var tablaValoresBd = TablaDatosSupervisor(_delegacion ?? "", _fechaCarga ?? "", _estFinal ?? "", despacho, _tipoFormulario);
            if (tablaValoresBd.Columns.Count > 20)
            {
                var tamanioAnterior = (PageWidth - Margins.Left - Margins.Right);
                Landscape = true;
                var tamanioPagHoriz = PageWidth - Margins.Left - Margins.Right;
                var punto = (tamanioPagHoriz - tamanioAnterior);
                xrLabel3.Location = Point.Add(xrLabel3.Location, new Size(punto, 0));
                xrLabel6.Location = Point.Add(xrLabel6.Location, new Size(punto, 0));
                xrLabel7.Location = Point.Add(xrLabel7.Location, new Size(punto, 0));
                xrPictureBox4.Location = Point.Add(xrPictureBox4.Location, new Size(punto, 0));
                xrPanel1.WidthF = tamanioPagHoriz;
                xrPanel2.WidthF = tamanioPagHoriz;
                xrLabel1.WidthF = tamanioPagHoriz;
                labelFiltros.WidthF = tamanioPagHoriz;
                LabelIndicadorTitulo.WidthF = tamanioPagHoriz;
                xrLabel3.WidthF = tamanioPagHoriz;
            }

            table.BeginInit();

            if (tamanioColNumer == -1)
            {
                tamanioColNumer = ((PageWidth - Margins.Left - Margins.Right - 300) / (tablaValoresBd.Columns.Count - 3));
            }
            var rowTitulos = new XRTableRow();

            for (var i = 1; i < tablaValoresBd.Columns.Count; i++)
            {

                var cell = new XRTableCell
                {
                    Text =
                        tablaValoresBd.Columns[i].Caption.IndexOf("Valor", StringComparison.Ordinal) != -1
                            ? titulosTabla[Convert.ToInt32(tablaValoresBd.Columns[i].Caption.Substring(5))]
                            : tablaValoresBd.Columns[i].Caption,
                    Width = i > 3 ? tamanioColNumer : 90
                };
                rowTitulos.Cells.Add(cell);
                rowTitulos.BackColor = Color.FromArgb(160, 160, 160);
                rowTitulos.ForeColor = Color.FromArgb(255, 255, 255);
            }
            table.Rows.Add(rowTitulos);

            for (int i = 0; i < tablaValoresBd.Rows.Count; i++)
            {
                var row = new XRTableRow();
                int numColumnas = tablaValoresBd.Columns.Count;
                for (int j = 1; j < numColumnas; j++)
                {
                    var cell = new XRTableCell
                    {
                        Text = tablaValoresBd.Rows[i][j].ToString(),
                        CanShrink = true,
                        CanGrow = true,
                        Width = j > 3 ? tamanioColNumer : 90
                    };
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
                if (i != tablaValoresBd.Rows.Count - 1)
                {
                    var rowDespacho = new XRTableRow();
                    var celdaSubreportDespacho = new XRTableCell
                    {
                        WidthF = PageWidth - Margins.Left - Margins.Right,
                        Height = 10,
                        Borders = BorderSide.None
                    };
                    celdaSubreportDespacho.Controls.Add(CrearTablaGestores(despacho, tablaValoresBd.Rows[i][0].ToString(), tamanioColNumer));
                    rowDespacho.Cells.Add(celdaSubreportDespacho);
                    table.Rows.Add(rowDespacho);
                }
            }

            table.BeforePrint += table1_BeforePrint;
            //table.AdjustSize();
            table.EndInit();
            return table;
        }

        public XRTable CrearTablaGestores(string despacho, string supervisor, int tamanioColNumer)
        {
            var table = new XRTable { Borders = BorderSide.All };
            
            var titulosTabla = new string[7];
            titulosTabla[0] = "Móvil";
            titulosTabla[1] = "Sin Asignar";
            titulosTabla[2] = "Sin Asignar 2 Visita";
            titulosTabla[3] = "Sin asignar 3 Visita";
            titulosTabla[4] = "Válidas";
            titulosTabla[5] = "Válidas Aprobadas";
            titulosTabla[6] = "Válidas Sin Aprobar";

            var tablaValoresBd = TablaDatosGestor(_delegacion, _fechaCarga, _estFinal, despacho, supervisor, _tipoFormulario);
            if (tablaValoresBd.Columns.Count > 20)
            {
                var tamanioAnterior = (PageWidth - Margins.Left - Margins.Right);
                Landscape = true;
                var tamanioPagHoriz = PageWidth - Margins.Left - Margins.Right;
                var punto = (tamanioPagHoriz - tamanioAnterior);
                xrLabel3.Location = Point.Add(xrLabel3.Location, new Size(punto, 0));
                xrLabel6.Location = Point.Add(xrLabel6.Location, new Size(punto, 0));
                xrLabel7.Location = Point.Add(xrLabel7.Location, new Size(punto, 0));
                xrPictureBox4.Location = Point.Add(xrPictureBox4.Location, new Size(punto, 0));
                xrPanel1.WidthF = tamanioPagHoriz;
                xrPanel2.WidthF = tamanioPagHoriz;
                xrLabel1.WidthF = tamanioPagHoriz;
                labelFiltros.WidthF = tamanioPagHoriz;
                LabelIndicadorTitulo.WidthF = tamanioPagHoriz;
                xrLabel3.WidthF = tamanioPagHoriz;
            }
            table.BeginInit();

            if (tamanioColNumer == -1) { tamanioColNumer = ((PageWidth - Margins.Left - Margins.Right - 300) / (tablaValoresBd.Columns.Count - 3)); }
            var rowTitulos = new XRTableRow();
            for (var i = 1; i < tablaValoresBd.Columns.Count; i++)
            {

                var cell = new XRTableCell
                {
                    Text =
                        tablaValoresBd.Columns[i].Caption.IndexOf("Valor", StringComparison.Ordinal) != -1
                            ? titulosTabla[Convert.ToInt32(tablaValoresBd.Columns[i].Caption.Substring(5))]
                            : tablaValoresBd.Columns[i].Caption,
                    Width = i > 3 ? tamanioColNumer : 80
                };
                rowTitulos.Cells.Add(cell);
                rowTitulos.BackColor = Color.FromArgb(220, 220, 220);
                rowTitulos.ForeColor = Color.FromArgb(0, 0, 0);
            }
            table.Rows.Add(rowTitulos);

            for (int i = 0; i < tablaValoresBd.Rows.Count; i++)
            {
                var row = new XRTableRow();
                int numColumnas = tablaValoresBd.Columns.Count;
                for (int j = 1; j < numColumnas; j++)
                {
                    var cell = new XRTableCell
                    {
                        Text = tablaValoresBd.Rows[i][j].ToString(),
                        CanShrink = true,
                        CanGrow = true,
                        Width = j > 3 ? tamanioColNumer : 80
                    };
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);

            }

            table.BeforePrint += table2_BeforePrint;
            
            table.EndInit();
            return table;
        }

        private void table_BeforePrint(object sender, PrintEventArgs e)
        {
            var table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = PageWidth - Margins.Left - Margins.Right;
        }
        private void table1_BeforePrint(object sender, PrintEventArgs e)
        {
            var table = ((XRTable)sender);
            table.Location = new Point(30);
            table.WidthF = PageWidth - Margins.Left - Margins.Right - 30;
        }
        private void table2_BeforePrint(object sender, PrintEventArgs e)
        {
            var table = ((XRTable)sender);
            table.Location = new Point(30);
            table.WidthF = PageWidth - Margins.Left - Margins.Right - 60;
        }

        
    }
}