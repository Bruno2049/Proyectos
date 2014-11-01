using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms.VisualStyles;
using PAEEEM.Entidades.Reportes;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.Reportes;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using Telerik.Charting;
using Telerik.Web.UI;

namespace PAEEEM.CustomReports
{
    public partial class ReporteDistribuidores : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaInicial();
            }
        }

        protected void rgDistribuidores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var lstDistribuidores = new List<Distribuidor>();
            rgDistribuidores.DataSource = lstDistribuidores;
        }

        protected void RadCmbMatriz_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbMatriz.SelectedIndex != 0)
            {
                LlenaCatalogoSucursales(int.Parse(RadCmbMatriz.SelectedValue));
            }
        }

        protected void RadCmbRegion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbRegion.SelectedIndex != 0)
            {
                LLenaCatalogoZona(int.Parse(RadCmbRegion.SelectedValue));
                LLenaCatalogoMatriz();

                RadCmbEstado.SelectedIndex = 0;
                RadCmbEstado.Enabled = false;

                RadCmbMunicipio.Items.Clear();
                RadCmbMunicipio.Enabled = false;
            }
            else
            {
                RadCmbEstado.Enabled = true;
                RadCmbMunicipio.Enabled = true;
            }
        }

        protected void RadCmbEstado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbEstado.SelectedIndex != 0)
            {
                LLenaCatalogoMunicipio(int.Parse(RadCmbEstado.SelectedValue));

                RadCmbRegion.Enabled = false;
                RadCmbZona.Items.Clear();
                RadCmbZona.Enabled = false;
            }
            else
            {
                var userModel = (US_USUARIOModel)Session["UserInfo"];

                if (userModel.Tipo_Usuario == "C_O")
                {
                    RadCmbRegion.Enabled = true;
                    RadCmbZona.Items.Clear();
                    RadCmbZona.Enabled = true;
                }

                if (userModel.Tipo_Usuario == "R_O")
                {
                    RadCmbZona.Enabled = true;
                }
            }
        }

        protected void RadCmbZona_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbZona.SelectedIndex != 0)
            {
                LLenaCatalogoMatriz();
                RadCmbEstado.SelectedIndex = 0;
                RadCmbEstado.Enabled = false;

                RadCmbMunicipio.Items.Clear();
                RadCmbMunicipio.Enabled = false;
            }
            else
            {
                RadCmbEstado.Enabled = true;
                RadCmbMunicipio.Enabled = true;
            }
        }

        protected void RadBtnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaDistribuidores();

            var userModel = (US_USUARIOModel)Session["UserInfo"];

            if (userModel.Tipo_Usuario == "C_O")
            {
                GeneraGrafica();
                GeneraGraficaHistDistribuidores();

                if (RadCmbRegion.Items.Any())
                {
                    if(RadCmbRegion.SelectedIndex != 0)
                        GeneraGraficaDistribuidoresXZona(int.Parse(RadCmbRegion.SelectedValue));
                }
            }
        }

        #endregion

        #region Graficas

        protected void GeneraGrafica()
        {
            var lstDistRegion = new ReporteDistribuidoresBL().ObtenDistribuidoresXregion();
            RadChartDistActivosRegion.DataSource = lstDistRegion;
            RadChartDistActivosRegion.Series[0].DataYColumn = "NoDistribuidores";
            RadChartDistActivosRegion.PlotArea.XAxis.DataLabelsColumn = "NombreRegionZona";
            RadChartDistActivosRegion.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = -90;
            RadChartDistActivosRegion.DataBind();
        }

        protected void GeneraGraficaDistribuidoresXZona(int idRegion)
        {
            var lstDistXzona = new ReporteDistribuidoresBL().ObtenDistribuidoresXzona(idRegion);
            RadChartDistActivosZona.DataSource = lstDistXzona;
            RadChartDistActivosZona.Series[0].DataYColumn = "NoDistribuidores";
            RadChartDistActivosZona.PlotArea.XAxis.DataLabelsColumn = "NombreRegionZona";
            RadChartDistActivosZona.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = -90;
            RadChartDistActivosZona.DataBind();
        }

        protected void GeneraGraficaHistDistribuidores()
        {
            var lstHistDistribuidores = new ReporteDistribuidoresBL().ObtenHistDistribuidoresActivos(2013, 0, 0, 0);
            RadChartHistDistribuidores.DataSource = lstHistDistribuidores;
            RadChartHistDistribuidores.Series[0].DataYColumn = "NoDistActivos";
            RadChartHistDistribuidores.PlotArea.XAxis.DataLabelsColumn = "DescripcionMes";
            RadChartHistDistribuidores.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = -90;
            RadChartHistDistribuidores.DefaultType = ChartSeriesType.Point;
            RadChartHistDistribuidores.DataBind();
        }

        #endregion

        #region Catalogos

        protected void LLenaCatalogoEstatus()
        {
            var lstCatEstatus = new ReporteDistribuidoresBL().ObtenEstatusProveedor();
            RadCmbEstatus.DataSource = lstCatEstatus;
            RadCmbEstatus.DataValueField = "Cve_Estatus_Proveedor";
            RadCmbEstatus.DataTextField = "Dx_Estatus_Proveedor";
            RadCmbEstatus.DataBind();

            RadCmbEstatus.Items.Insert(0, new RadComboBoxItem("Todos", "0"));
            RadCmbEstatus.SelectedIndex = 0;
        }

        protected void LlenaCatalogoRegion()
        {
            var lstCatRegion = new ReporteDistribuidoresBL().ObtenCatalogoRegion();
            RadCmbRegion.DataSource = lstCatRegion;
            RadCmbRegion.DataValueField = "Cve_Region";
            RadCmbRegion.DataTextField = "Dx_Nombre_Region";
            RadCmbRegion.DataBind();

            RadCmbRegion.Items.Insert(0, new RadComboBoxItem("Todos", "0"));
            RadCmbRegion.SelectedIndex = 0;
        }

        protected void LLenaCatalogoZona(int idRegion)
        {
            RadCmbZona.Items.Clear();
            var lstCatZona = new ReporteDistribuidoresBL().ObtenCatalogoZona(idRegion);
            RadCmbZona.DataSource = lstCatZona;
            RadCmbZona.DataValueField = "Cve_Zona";
            RadCmbZona.DataTextField = "Dx_Nombre_Zona";
            RadCmbZona.DataBind();

            RadCmbZona.Items.Insert(0, new RadComboBoxItem("Todos", "0"));
            RadCmbZona.SelectedIndex = 0;
        }

        protected void LLenaCatalogoTecnologias()
        {
            var lstCatTecnologia = new ReporteDistribuidoresBL().ObtenCatalogoTecnologia();
            RadCmbTecnologia.DataSource = lstCatTecnologia;
            RadCmbTecnologia.DataValueField = "Cve_Tecnologia";
            RadCmbTecnologia.DataTextField = "Dx_Nombre_General";
            RadCmbTecnologia.DataBind();

            RadCmbTecnologia.Items.Insert(0, new RadComboBoxItem("Todos", "0"));
            RadCmbTecnologia.SelectedIndex = 0;
        }

        protected void LlenaCatalogoEstados()
        {
            var lstEstados = CatalogosSolicitud.ObtenCatEstadosRepublica();

            if (lstEstados.Count > 0)
            {
                RadCmbEstado.DataSource = lstEstados;
                RadCmbEstado.DataValueField = "Cve_Estado";
                RadCmbEstado.DataTextField = "Dx_Nombre_Estado";
                RadCmbEstado.DataBind();

                RadCmbEstado.Items.Insert(0, new RadComboBoxItem("Todos", "0"));
                RadCmbEstado.SelectedIndex = 0;
            }
        }

        protected void LLenaCatalogoMunicipio(int idEstado)
        {
            var lstMunicipios = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

            if (lstMunicipios.Count > 0)
            {
                RadCmbMunicipio.Items.Clear();
                RadCmbMunicipio.DataSource = lstMunicipios;
                RadCmbMunicipio.DataValueField = "Cve_Deleg_Municipio";
                RadCmbMunicipio.DataTextField = "Dx_Deleg_Municipio";
                RadCmbMunicipio.DataBind();

                RadCmbMunicipio.Items.Insert(0, new RadComboBoxItem("Todos", "0"));
                RadCmbMunicipio.SelectedIndex = 0;
            }
        }

        protected void LLenaCatalogoMatriz()
        {
            RadCmbMatriz.Items.Clear();
            RadCmbSucursal.Items.Clear();

            var lstCatMatriz = new ReporteDistribuidoresBL().ObtenCatalogoMatriz();

            if (RadCmbRegion.SelectedIndex != 0)
            {
                lstCatMatriz = lstCatMatriz.FindAll(me => me.Cve_Region == int.Parse(RadCmbRegion.SelectedValue));

                if (RadCmbZona.SelectedIndex != 0)
                {
                    lstCatMatriz = lstCatMatriz.FindAll(me => me.Cve_Zona == int.Parse(RadCmbZona.SelectedValue));
                }
            }

            RadCmbMatriz.DataSource = lstCatMatriz;
            RadCmbMatriz.DataValueField = "Id_Proveedor";
            RadCmbMatriz.DataTextField = "Dx_Nombre_Comercial";
            RadCmbMatriz.DataBind();

            RadCmbMatriz.Items.Insert(0, new RadComboBoxItem("Todos", "0"));
            RadCmbMatriz.SelectedIndex = 0;
        }

        protected void LlenaCatalogoSucursales(int idMatriz)
        {
            var lstSuscursales = new ReporteDistribuidoresBL().ObtenCatalogoSucursales(idMatriz);
            RadCmbSucursal.Items.Clear();
            RadCmbSucursal.DataSource = lstSuscursales;
            RadCmbSucursal.DataValueField = "Id_Branch";
            RadCmbSucursal.DataTextField = "Dx_Nombre_Comercial";
            RadCmbSucursal.DataBind();

            RadCmbSucursal.Items.Insert(0, new RadComboBoxItem("Todos", "0"));
            RadCmbSucursal.SelectedIndex = 0;
        }

        #endregion                                       

        #region Metodos varios

        protected void CargaInicial()
        {
            LLenaCatalogoEstatus();
            LlenaCatalogoRegion();
            LLenaCatalogoTecnologias();
            LlenaCatalogoEstados();
            LLenaCatalogoMatriz();
            
            var fechaMinima = Convert.ToDateTime("01/01/2014");
            RadDateFechaInicio.MinDate = fechaMinima;
            RadDateFechaInicio.SelectedDate = fechaMinima;

            RadDateFechaFin.MaxDate = DateTime.Now.Date;
            RadDateFechaFin.SelectedDate = DateTime.Now.Date;

            var userModel = (US_USUARIOModel)Session["UserInfo"];
            {
                if (userModel.Tipo_Usuario == "R_O")
                {
                    RadCmbRegion.SelectedValue = userModel.Id_Departamento.ToString(CultureInfo.InvariantCulture);
                    LLenaCatalogoZona(int.Parse(RadCmbRegion.SelectedValue));
                    LLenaCatalogoMatriz();
                    RadCmbRegion.Enabled = false;
                }

                if (userModel.Tipo_Usuario == "Z_O")
                {
                    var zona = new ReporteDistribuidoresBL().ObtenZona(userModel.Id_Departamento);

                    if (zona != null)
                    {
                        RadCmbRegion.SelectedValue = zona.Cve_Region.ToString();
                        LLenaCatalogoZona((int)zona.Cve_Region);
                        RadCmbZona.SelectedValue = zona.Cve_Zona.ToString(CultureInfo.InvariantCulture);
                        LLenaCatalogoMatriz();

                        RadCmbRegion.Enabled = false;
                        RadCmbZona.Enabled = false;
                    }
                }
            }
        }

        protected void BusquedaDistribuidores()
        {
            var region = RadCmbRegion.Enabled ? int.Parse(RadCmbRegion.SelectedValue) : 0;
            var zona = RadCmbZona.Items.Any() ?( RadCmbZona.Enabled ? int.Parse(RadCmbZona.SelectedValue) : 0) : 0;
            var estado = RadCmbEstado.Enabled ? int.Parse(RadCmbEstado.SelectedValue) : 0;
            var municipio = RadCmbMunicipio.Items.Any() ? (RadCmbMunicipio.Enabled ? int.Parse(RadCmbMunicipio.SelectedValue) : 0) : 0;
            var matriz = int.Parse(RadCmbMatriz.SelectedValue);
            var sucursal = RadCmbSucursal.Items.Any() ? int.Parse(RadCmbSucursal.SelectedValue) : 0;
            var estatus = int.Parse(RadCmbEstatus.SelectedValue);

            var lstDistribuidores =
                new ReporteDistribuidoresBL().ObtenDistribuidores((DateTime) RadDateFechaInicio.SelectedDate,
                                                                  (DateTime) RadDateFechaFin.SelectedDate,
                                                                  region, zona, estado, municipio, matriz, sucursal,
                                                                  estatus);

            if (lstDistribuidores.Count > 0)
            {
                rgDistribuidores.DataSource = lstDistribuidores;
                rgDistribuidores.DataBind();
            }
        }

        #endregion
    }
}