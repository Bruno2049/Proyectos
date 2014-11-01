using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using PAEEEM;
using PAEEEM.AccesoDatos.AltaBajaEquipos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.BussinessLayer;
using PAEEEM.Captcha;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.Entidades.Alta_Equipos;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using PAEEEM.LogicaNegocios.Trama;
using Telerik.Web.UI;
using PAEEEM.LogicaNegocios.ModuloCentral;
using PAEEEM.LogicaNegocios.Tarifas;
using Telerik.Web.UI.GridExcelBuilder;

namespace PAEEEM.SupplierModule
{
    public partial class wuAltaBajaEquipos : UserControl
    {

        #region Region de Atributos

        protected int IdTrama
        {
            get { return (int)ViewState["IdTrama"]; }
            set { ViewState["IdTrama"] = value; }
        }
        protected int IdProveedor
        {
            get { return (int)ViewState["IdProveedor"]; }
            set { ViewState["IdProveedor"] = value; }
        }
        protected int IdSucursal
        {
            get { return (int)ViewState["IdSucursal"]; }
            set { ViewState["IdSucursal"] = value; }
        }
        public short IdNegocio
        {
            get { return (short)ViewState["IdNegocio"]; }
            set { ViewState["IdNegocio"] = value; }
        }

        public string NumeroCredito
        {
            get { return ViewState["NumeroCredito"] as string; }
            set { ViewState["NumeroCredito"] = value; }
        }

        public string RazonSocial
        {
            get { return ViewState["RazonSocial"] as string; }
            set { ViewState["RazonSocial"] = value; }
        }

        //LISTA DE PRODUCTO DE BAJA EFICIENCIA
        private List<EquipoBajaEficiencia> LstBajaEficiencia
        {
            get
            {
                return ViewState["LstBajaEficiencia"] == null
                           ? new List<EquipoBajaEficiencia>()
                           : ViewState["LstBajaEficiencia"] as List<EquipoBajaEficiencia>;
            }
            set { ViewState["LstBajaEficiencia"] = value; }
        }

        //LISTA DE PRODUCTO DE ALTA EFICIENCIA
        private List<EquipoAltaEficiencia> LstAltaEficiencia
        {
            get
            {
                return ViewState["LstAltaEficiencia"] == null
                           ? new List<EquipoAltaEficiencia>()
                           : ViewState["LstAltaEficiencia"] as List<EquipoAltaEficiencia>;
            }
            set { ViewState["LstAltaEficiencia"] = value; }
        }

        //LISTA DE PRODUCTO DE BAJA EFICIENCIA
        public List<EquipoBajaEficiencia> LstBajaEficienciaAgrupar
        {
            get
            {
                return ViewState["LstBajaEficienciaAgrupar"] == null
                           ? new List<EquipoBajaEficiencia>()
                           : ViewState["LstBajaEficienciaAgrupar"] as List<EquipoBajaEficiencia>;
            }
            set { ViewState["LstBajaEficienciaAgrupar"] = value; }
        }

        private EquipoBajaEficiencia EquipoBajaSelecionado
        {
            get
            {
                return ViewState["EquipoBajaSelecionado"] == null
                           ? null
                           : ViewState["EquipoBajaSelecionado"] as EquipoBajaEficiencia;
            }
            set { ViewState["EquipoBajaSelecionado"] = value; }
        }

        public List<EquipoBajaEficiencia> LstGrupos
        {
            get
            {
                return ViewState["LstGrupos"] == null
                           ? new List<EquipoBajaEficiencia>()
                           : ViewState["LstGrupos"] as List<EquipoBajaEficiencia>;
            }
            set { ViewState["LstGrupos"] = value; }
        }

        public DatosFacturacion Datos
        {
            get { return ViewState["Datos"] == null ? new DatosFacturacion() : ViewState["Datos"] as DatosFacturacion; }
            set { ViewState["Datos"] = value; }
        }

        public string IdDistribuidor
        {
            get { return ViewState["IdDistribuidor"] == null ? "" : ViewState["IdDistribuidor"].ToString(); }
            set { ViewState["IdDistribuidor"] = value; }
        }

        public int RegionBioclimatica
        {
            get { return int.Parse(ViewState["RegionBioclimatica"].ToString()); }
            set { ViewState["RegionBioclimatica"] = value; }
        }

        public int IdRegion
        {
            get { return int.Parse(ViewState["IdRegion"].ToString()); }
            set { ViewState["IdRegion"] = value; }
        }

        public CompTarifa InfoTarifa
        {
            get { return ViewState["InfoTarifa"] == null ? new CompTarifa() : ViewState["InfoTarifa"] as CompTarifa; }
            set { ViewState["InfoTarifa"] = value; }
        }

        public CompFacturacion FactSinAhorro
        {
            get
            {
                return ViewState["FactSinAhorro"] == null
                           ? new CompFacturacion()
                           : ViewState["FactSinAhorro"] as CompFacturacion;
            }
            set { ViewState["FactSinAhorro"] = value; }
        }

        public CompFacturacion FactConAhorro
        {
            get
            {
                return ViewState["FactConAhorro"] == null
                           ? new CompFacturacion()
                           : ViewState["FactConAhorro"] as CompFacturacion;
            }
            set { ViewState["FactConAhorro"] = value; }
        }

        public CompConceptosCappago CapacidadPago
        {
            get
            {
                return ViewState["CapacidadPago"] == null
                           ? new CompConceptosCappago()
                           : ViewState["CapacidadPago"] as CompConceptosCappago;
            }
            set { ViewState["CapacidadPago"] = value; }
        }

        public CompConceptosPsr Psr
        {
            get { return ViewState["Psr"] == null ? new CompConceptosPsr() : ViewState["Psr"] as CompConceptosPsr; }
            set { ViewState["Psr"] = value; }
        }

        public CompResultado Resultado
        {
            get { return ViewState["Resultado"] == null ? new CompResultado() : ViewState["Resultado"] as CompResultado; }
            set { ViewState["Resultado"] = value; }
        }

        public string TipoTarifa
        {
            get { return (string)ViewState["TipoTarifa"]; }
            set { ViewState["TipoTarifa"] = value; }
        }

        public decimal FDeg
        {
            get { return ViewState["FDeg"] == null ? 0.0M : Convert.ToDecimal(ViewState["FDeg"]); }
            set { ViewState["FDeg"] = value; }
        }

        public decimal FBio
        {
            get { return ViewState["FBio"] == null ? 0.0M : Convert.ToDecimal(ViewState["FBio"]); }
            set { ViewState["FBio"] = value; }
        }

        public decimal Fmes
        {
            get { return ViewState["Fmes"] == null ? 0.0M : Convert.ToDecimal(ViewState["Fmes"]); }
            set { ViewState["Fmes"] = value; }
        }

        public DatosPyme Pyme
        {
            get { return ViewState["Pyme"] == null ? new DatosPyme() : ViewState["Pyme"] as DatosPyme; }
            set { ViewState["Pyme"] = value; }
        }

        public List<Presupuesto> Presupuesto
        {
            get
            {
                return ViewState["PRESUPUESTO"] == null
                           ? new List<Presupuesto>()
                           : ViewState["PRESUPUESTO"] as List<Presupuesto>;
            }

            set { ViewState["PRESUPUESTO"] = value; }
        }

        public decimal TotalCapacidad
        {
            get { return ViewState["TotalCapacidad"] == null ? 0.0M : Convert.ToDecimal(ViewState["TotalCapacidad"]); }
            set { ViewState["TotalCapacidad"] = value; }
        }

        public decimal TopeMaximoCapacidad
        {
            get
            {
                return ViewState["TopeMaximoCapacidad"] == null
                           ? 0.0M
                           : Convert.ToDecimal(ViewState["TopeMaximoCapacidad"]);
            }
            set { ViewState["TopeMaximoCapacidad"] = value; }
        }

        public decimal TotalCapacidadEquiposAlta
        {
            get
            {
                return ViewState["TotalCapacidadEquiposAlta"] == null
                           ? 0.0M
                           : Convert.ToDecimal(ViewState["TotalCapacidadEquiposAlta"]);
            }
            set { ViewState["TotalCapacidadEquiposAlta"] = value; }
        }

        private List<EquipoAltaEficiencia> LstCapacidadesAltaEficiencia
        {
            get
            {
                return ViewState["LstCapacidadesAltaEficiencia"] == null
                           ? new List<EquipoAltaEficiencia>()
                           : ViewState["LstCapacidadesAltaEficiencia"] as List<EquipoAltaEficiencia>;
            }
            set { ViewState["LstCapacidadesAltaEficiencia"] = value; }
        }

        public bool IsSE
        {
            get { return ViewState["IsSE"] != null && bool.Parse(ViewState["IsSE"].ToString()); }
            set { ViewState["IsSE"] = value; }
        }

        public bool IsBC
        {
            get { return ViewState["IsBC"] != null && bool.Parse(ViewState["IsBC"].ToString()); }
            set { ViewState["IsBC"] = value; }
        }

        public bool EsCaptura
        {
            get { return ViewState["EsCaptura"] != null && bool.Parse(ViewState["EsCaptura"].ToString()); }
            set { ViewState["EsCaptura"] = value; }
        }

        public bool EsEdicion
        {
            get { return ViewState["EsEdicion"] != null && bool.Parse(ViewState["EsEdicion"].ToString()); }
            set { ViewState["EsEdicion"] = value; }
        }

        public bool CombinaTecnologias
        {
            get { return bool.Parse(ViewState["CombinaTecnologias"].ToString()); }
            set { ViewState["CombinaTecnologias"] = value; }
        }

        public DataTable AmortizacionDetalle
        {
            get
            {
                return ViewState["AmortizacionDetalle"] == null
                           ? new DataTable()
                           : ViewState["AmortizacionDetalle"] as DataTable;
            }
            set { ViewState["AmortizacionDetalle"] = value; }
        }

        #endregion

        #region Variables Globales

        public string RPU { get; set; }
        public string IdCliente { get; set; }

        public const int ElementoSubestacionesElectricas = 6;
        public const int ElementoRefrigeracion = 1;
        public const int ElementoIluminacionLineal = 3;
        public const int ElementoIluminacionLed = 6;

        //public const String Trama =
        //    "0A401602115002220131201181DA15A0100101001OPERADORA ALCOMESA S A DE C V LOPEZ MATEOS 701 1 E INDUSTRIAMEXICALI            BC   200271363000020131130201312312014011520131200003892{00003892{00001900024300027000001424009391000000000000000000000000000000000{0000000401099A0000000000000{0000000048535E0000000004586K0000000008992F0000000454041{0000000049944E0000000000000{0000001084410{0000000000000{0000000000000{000000000000{000000000000{0000000000000{88001628100  000           37N30U 00000000000000000080                  00000000000000000000                  00000000000000000000           37N30U 00000000000000000080           37N30U 00000000000000000080                  000000000000000000000                00000000000000000000                00000000000000000000                00000000000000000000                00000000000000000000                00000000000000000000                0000000000000000000000004792800002209104041870000551680000210895304322000067304000192089200484300004440800002009418040060000000000000000000000000000000000902400000002989600000000000000000000000000000000902400000002989600000000000000000000000001180000014200000000000065363358000022597012339009692000000025545000000000000001176270000000000{0000000000000{OAL990226EHA INDUSTRIAL                    FIANZA 182596 $53003.16  SE13.2/300KVA,220/127VCA 368413I9D0000000000000000000000ND10000000580430{00000000000{0000000000{00000  00000000000000{00000000000{0000000000{00000  00000000000000{00000000000{0000000000{00000  00000000000000{00000000000{0000000000{00000  00000000000000{00000000000{0000000000{00000  00000000000000{00000000000{0000000000{00000  00000000000000{00000000000{0000000000{000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000repartir                      2014011620131217000000113274390000001132740}000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000Dif. Amt.                                                                                                                                                 00000000000000{00000000000{0000000000{                     00000000000000{00000000000{0000000000{                     00000000000000{00000000000{0000000000{                     00000000000000{00000000000{0000000000{                     00000000000000{00000000000{0000000000{                     00000000000000{00000000000{0000000000{                     00000000000000{00000000000{0000000000{                                                                                                                                                           00000000000000000000000000000000DIC12000044408000020094180400610739ENE13000043512000019094660433210804FEB13000042264000023094200398110582MAR13000053592000024093190428810223   00000000000000000000000000000000ABR13000058808000027092020442100000MAY13000069128000191091450478921315JUN13000078720000217090770503820885JUL13000085080000209090070522219983AGO13000081952000219089860500720609SEP13000067304000192089200484320779OCT13000046696000170089620440222290OCT13000008472000021089530432211256NOV13000047928000022091040418711498DIC1300003892000001909391036841166600   0000   000000000000000000000000000000000{0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000141007000   0000   000000000000000000000000000000000{00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000149000001250000000000000000000001350000012900000000000000000000014500000158000000000000000000000164000001680000000000000000000001850000018400000000000000000000000000000194000001900000000000000000000002150000021700000000000000000000021900000207000000000000000000000220000002180000000000000000000001930000019100000000000000000000016900000170000000000000016200000136000000000000000000000159000001430000000000000000000001420000011800000000000000000000708100015203000029050000000000006707000150900000290500000000000001410000011700000000000000000000015800000142000000000000000000000000000000002013120001000000000000000000000000000000022597000000000000000000969200000000000000000123390000000000000000000000310000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{000000000000000000000000000000000{00000000000000000000000000000000000000000000001420000011800000000000000002703710{1084410{2123280{0000000000000{0000000000000{000000000000000000{0000000000000{                        00000000000000000000000000000{0000000000000{AE        0000160697772014-01-03T14:14:07A000000000000000000000000000000000000000000000000000000000000000000 000000000000000000000000000000000000000000000000000000000000000000 000000000000000000000000000000000000000000000000000000000000000000 000000000000000000000000000000000000000000000000000000000000000000 000000000000000000000000000000000000000000000000000000000000000000 000000000000000000000000000000000000000000000000000000000000000000 000000000000000000000000000000000000000000000000000000000000000000 000000000000000000000000000000000000000000000000000000000000000000 000000000000000000000000000000000000000000000000000000000000000000 000000000000000000000000000000000000000000000000000000000000000000 000000000000000000000000000000000000000000000000000000000000000000 0000000000000000000000000000000000          0000000000000000          0000000000000000          0000000000000000          0000000000000000          0000000000000000          0000000000000000          0000000000000000000000000000000                                                                                                                                                                                                        0120140120          v0201201401152014012020131213201312172013111320131119201310132013101720130914201309202013081420130820201307132013071920130614201306192013051520130521201304132013041820130314201303212013021420130221201301132013012120121214201212192012111420121121201210132012101920120914201209212012081220120820201207142012072020120614201206252012051420120521201204142012042020120314201203232012021320120220                              ";

        private AlgoritmoTarifa02 _t02;
        private AlgoritmoTarifa03 _t03;
        private AlgoritmoTarifaHM _tHm;
        private AlgoritmoTarifaOM _tOm;

        #endregion

        #region Carga Inicial

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MuestraBotonesGrid();
        }

        public CompTarifa InicializaUserControl(string numeroCredito, string razonSocial)
        {
            NumeroCredito = numeroCredito;
            RazonSocial = razonSocial;
            if (NumeroCredito != null)
            {
                InfoTarifa = new CompTarifa
                    {
                        AnioFactValido = true,
                        CumpleConsumo = true,
                        PeriodosValidos = true,
                        ValidaFechaTablasTarifas = true
                    };

                CargaEquiposCredito();
            }
            else
            {
                CargaInformacionGeneral();
                CargaTecnologias();
                RealizaTarifa();
                EsCaptura = true;
                RadBtnEditaPresupuesto.Visible = false;
            }
            
            return InfoTarifa;
        }

        public void InicializaEdicionPresupuesto(string numeroCredito)
        {
            try
            {
                ParseoTrama();
                CargaInformacionGeneral();
                CargaTecnologias();
                RealizaTarifa();
                EsEdicion = true;
                EsCaptura = true;
                RadBtnEditaPresupuesto.Visible = false;
                
                var parseo = (ParseoTrama)Session["PARSEO_TRAMA"];
                Datos.ConsumoPromedio = parseo.ComplexParseo.InfoConsumo.Detalle.Promedio;
                Datos.DemandaMaxima = parseo.ComplexParseo.InfoDemanda.Detalle.DemandaMax;
                Datos.FactorPotencia = Math.Round(
                                        parseo.ComplexParseo.HistorialDetconsumos.Where(p => p.Id > 0).Sum(p => p.FactorPotencia) /
                                        InfoTarifa.Periodos, 4);
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert(ex.Message, 300, 150, "Alta y Baja de Equipos", null);
            }
            
        }

        #endregion

        #region Metodos Dinamicos

        protected void ParseoTrama()
        {
            string error = "";
            string estado = "";
            var validator = new CodeValidator();
            var rpu = Session["ValidRPU"].ToString();
            if (validator.ValidateServiceCodeEdit(rpu, out error, ref estado))
            {
                var userModel = (US_USUARIOModel)Session["UserInfo"];
                
                if (userModel != null && userModel.Id_Rol == 3) // distribuidor
                {
                    var trama = new OpEquiposAbEficiencia().ObtenTrama(NumeroCredito);

                    if (!string.IsNullOrEmpty(trama))
                        Session["PARSEO_TRAMA"] = new ParseoTrama(trama);
                    else
                        Session["PARSEO_TRAMA"] = validator.ParseoTrama;
                }
            }
            else
            {
                throw new Exception(error);
            }
        }
        
        protected void CargaInformacionGeneral()
        {

            InicializaParametrosGlobales();
            
            var datosPyme = SolicitudCreditoAcciones.BuscaDatosPyme(txtServiceCode.Text);

            if (datosPyme != null)
            {
                #region Tomar los datos de las tablas correspondientes
                //var direccioNeg = Captura.ObtenDireccionCliente(IdProveedor, IdSucursal, Convert.ToInt32(Session["IdCliente"]), IdNegocio, 1);
                //var cliente = Captura.ObtenCliente(IdProveedor, IdSucursal, Convert.ToInt32(Session["IdCliente"]));
                //var negocio = Captura.ObtenNegocioCliente(IdProveedor, IdSucursal, Convert.ToInt32(Session["IdCliente"]),
                //    IdNegocio);
                //var direccioNeg = Captura.ObtenDireccionCliente(IdProveedor, IdSucursal, Convert.ToInt32(Session["IdCliente"]), IdNegocio, 1);
                //var cliente = Captura.ObtenCliente(IdProveedor, IdSucursal, Convert.ToInt32(Session["IdCliente"]));
                //var negocio = Captura.ObtenNegocioCliente(IdProveedor, IdSucursal, Convert.ToInt32(Session["IdCliente"]),
                //    IdNegocio);

                //txtDX_NOMBRE_COMERCIAL.Text = RazonSocial.ToUpper();
                //txtDX_CP_FISC.Text = direccioNeg.CP;
                //Pyme.CodigoPostal = direccioNeg.CP;
                //Pyme.CveCp = direccioNeg.CVE_CP != null ? (int)direccioNeg.CVE_CP : 0;
                //Pyme.CveDelegMunicipio = direccioNeg.Cve_Deleg_Municipio != null ? (int)direccioNeg.Cve_Deleg_Municipio : 0; // 6
                //Pyme.CveEstado = direccioNeg.Cve_Estado != null ? (int)direccioNeg.Cve_Estado : 0; // 25
                //Pyme.CveSectorEconomico = negocio.Cve_Sector != null
                //                              ? (int)negocio.Cve_Sector
                //                              : 0;
                //Pyme.CveTipoIndustria = negocio.Cve_Tipo_Industria != null ? (int)negocio.Cve_Tipo_Industria : 0;
                //Pyme.DxNombreComercial = negocio.Nombre_Comercial;
                //Pyme.NoEmpleados = negocio.Numero_Empleados != null ? (int)negocio.Numero_Empleados : 0;
                //Pyme.PromVtasMensuales = negocio.Ventas_Mes != null
                //                             ? (decimal)negocio.Ventas_Mes
                //                             : 0.0M;
                //Pyme.RFC = cliente.RFC;
                //Pyme.TotGastosMensuales = negocio.Gastos_Mes != null
                //                              ? (decimal)negocio.Gastos_Mes
                //                              : 0.0M;

                //var estadoDomicilio = CatalogosSolicitud.ObtenEstadoXId(Pyme.CveEstado).Dx_Nombre_Estado;
                //var municipioDomicilio = CatalogosSolicitud.ObtenMunicipioXId(Pyme.CveDelegMunicipio).Dx_Deleg_Municipio;
                //var tipoIndustria =
                //    CatalogosSolicitud.ObtenTipoIndustria((int)negocio.Cve_Tipo_Industria).DESCRIPCION_SCIAN;

                //txtDX_DOMICILIO_FISC.Text = estadoDomicilio + @", " + municipioDomicilio;
                //txtDX_TIPO_INDUSTRIA.Text = tipoIndustria;

                //InicializaPropiedades(Pyme.CveEstado, Pyme.CveDelegMunicipio);
                #endregion

                txtDX_NOMBRE_COMERCIAL.Text = RazonSocial.ToUpper();
                txtDX_CP_FISC.Text = datosPyme.Codigo_Postal;
                Pyme.CodigoPostal = datosPyme.Codigo_Postal;
                Pyme.CveCp = datosPyme.Cve_CP != null ? (int)Pyme.CveCp : 0;
                Pyme.CveDelegMunicipio = datosPyme.Cve_Deleg_Municipio != null ? (int)datosPyme.Cve_Deleg_Municipio : 0; // 6
                Pyme.CveEstado = datosPyme.Cve_Estado != null ? (int)datosPyme.Cve_Estado : 0; // 25
                Pyme.CveSectorEconomico = datosPyme.Cve_Sector_Economico != null
                                              ? (int)datosPyme.Cve_Sector_Economico

                                              : 0;
                Pyme.CveTipoIndustria = datosPyme.Cve_Tipo_Industria != null ? (int)datosPyme.Cve_Tipo_Industria : 0;
                Pyme.DxNombreComercial = datosPyme.Dx_Nombre_Comercial;
                Pyme.NoEmpleados = datosPyme.No_Empleados != null ? (int)datosPyme.No_Empleados : 0;
                Pyme.PromVtasMensuales = datosPyme.Prom_Vtas_Mensuales != null
                                             ? (decimal)datosPyme.Prom_Vtas_Mensuales
                                             : 0.0M;
                Pyme.RFC = datosPyme.RFC.ToUpper();
                Pyme.TotGastosMensuales = datosPyme.Tot_Gastos_Mensuales != null
                                              ? (decimal)datosPyme.Tot_Gastos_Mensuales
                                              : 0.0M;

                var estadoDomicilio = CatalogosSolicitud.ObtenEstadoXId(Pyme.CveEstado).Dx_Nombre_Estado;
                var municipioDomicilio = CatalogosSolicitud.ObtenMunicipioXId(Pyme.CveDelegMunicipio).Dx_Deleg_Municipio;
                var tipoIndustria =
                    CatalogosSolicitud.ObtenTipoIndustria((int)datosPyme.Cve_Tipo_Industria).DESCRIPCION_SCIAN;
                
                txtDX_DOMICILIO_FISC.Text = estadoDomicilio + @", " + municipioDomicilio;
                txtDX_TIPO_INDUSTRIA.Text = tipoIndustria;

                InicializaPropiedades(Pyme.CveEstado, Pyme.CveDelegMunicipio);
            }
        }

        protected void InicializaPropiedades(int cveEstado, int cveMunicipio)
        {
            var parseo = (ParseoTrama)Session["PARSEO_TRAMA"];
            
            if (Session["UserInfo"] != null)
            {
                var user = (US_USUARIOModel) Session["UserInfo"];

                if (user.Tipo_Usuario == GlobalVar.SUPPLIER_BRANCH)
                {
                    var proveedor =
                        CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORByBranchID(user.Id_Departamento.ToString());
                    IdDistribuidor = proveedor.Id_Proveedor.ToString();
                    IdProveedor = proveedor.Id_Proveedor;
                }
                else
                {
                    var proveedor =
                        CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORByPKID(user.Id_Departamento.ToString());
                    IdDistribuidor = user.Id_Departamento.ToString();
                    IdProveedor = user.Id_Departamento;
                }
            }
            try
            {
                //IdProveedor = 66;
                IdTrama = parseo.idResponseData;

                Pyme.CveMunicipioNafin =
                    int.Parse(new DelegacionMunicipio().ObtienePorCondicion(p => p.Cve_Deleg_Municipio == cveMunicipio).Cve_NAFIN);
                
                IdRegion =
                    new RegionesMunFactBio().FiltraPorCondicion(p => p.IDESTADO == cveEstado).FirstOrDefault().IDREGION;

                FBio =
                    new RegionesMunFactBio().ObtienePorCondicion(
                        p => p.IDREGION == IdRegion && p.IDESTADO == cveEstado && p.IDMUNICIPIO == Pyme.CveMunicipioNafin)
                                            .FACTOR_BIOCLIMATICO;
                RegionBioclimatica =
                    (int)new Estado().ObtienePorCondicion(p => p.Cve_Estado == cveEstado).IDREGION_BIOCLIMA;

                FDeg =
                    Convert.ToDecimal(
                        new RegionesBioclimaticas().ObtienePorCondicion(p => p.IDREGION_BIOCLIMA == RegionBioclimatica)
                                                   .FDEGRC);
                Fmes = Convert.ToDecimal(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 12).VALOR);

                TipoTarifa = parseo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 4).Dato;
                TipoTarifa = TipoTarifa.Substring(0, 1) == "7" ? "HM" : TipoTarifa;
                TipoTarifa = TipoTarifa.Substring(0, 1) == "6" ? "OM" : TipoTarifa;

                var iva = Convert.ToDecimal(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 3 && p.IDSECCION == 1).VALOR);
                var porcentageInstalacion = Convert.ToDecimal(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 16).VALOR);

                Datos.ValorIva = iva / 100.0M;
                Datos.PorcentageMaximoInstalacion = porcentageInstalacion / 100.0M;

                var tipoFacturacion =
                    parseo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato;
                tipoFacturacion = tipoFacturacion != "1" ? "2" : tipoFacturacion;

                Datos.PeriodoPago =
                    int.Parse(tipoFacturacion);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un problema al cargar Parametros Globales", ex);
            }

        }

        public void MuestraBotonesGrid()
        {
            if (LstBajaEficiencia.Count > 0)
            {
                btnAgrupar.Visible = true;
                btnDesagrupar.Visible = true;
            }
            else
            {
                btnAgrupar.Visible = false;
                btnDesagrupar.Visible = false;
            }
        }

        public bool ResumenPresupuesto()
        {
            try
            {
                if (LstAltaEficiencia.Count > 0)
                {
                    Presupuesto = new ValidacionTarifas().ObtenResumenPresupuesto(LstAltaEficiencia, LstBajaEficiencia,
                                                                                  Datos.ValorIva);

                    Datos.MontoTotalPagar = Math.Round(Presupuesto.First(p => p.IdPresupuesto == 8).Resultado, 4);
                    rgPresupuesto.DataSource = Presupuesto.OrderBy(me => me.IdOrden);
                    rgPresupuesto.DataBind();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void CalculaCapacidadesMaximas(int esquema, int grupo)
        {
            TotalCapacidad = 0.0M;
            TopeMaximoCapacidad = 0.0M;
            TotalCapacidadEquiposAlta = 0.0M;

            if (esquema == 0)
            {
                foreach (var equipoBajaEficiencia in LstBajaEficiencia.FindAll(me => me.Cve_Grupo == grupo))
                {
                    equipoBajaEficiencia.Dx_Consumo =
                        equipoBajaEficiencia.Dx_Consumo != ""
                            ? equipoBajaEficiencia.Dx_Consumo
                            : "0";
                    TotalCapacidad =
                        TotalCapacidad + (decimal.Parse(equipoBajaEficiencia.Dx_Consumo) * equipoBajaEficiencia.Cantidad);
                }

                if (EquipoBajaSelecionado.DetalleTecnologia.CveFactorSusticion == 2)
                    TopeMaximoCapacidad = TotalCapacidad * 1.2M;
                if (EquipoBajaSelecionado.DetalleTecnologia.CveFactorSusticion == 3)
                    TopeMaximoCapacidad = TotalCapacidad;

                foreach (var equipoAltaEficiencia in LstAltaEficiencia.FindAll(me => me.Cve_Grupo == grupo))
                {
                    TotalCapacidadEquiposAlta =
                        TotalCapacidadEquiposAlta +
                        (Convert.ToDecimal(equipoAltaEficiencia.No_Capacidad) * equipoAltaEficiencia.Cantidad);
                }
            }
        }

        protected void InicializaParametrosGlobales()
        {
            MuestraBotonesGrid();
            txtServiceCode.Text = Session["ValidRPU"].ToString();
            Datos = new DatosFacturacion();
            Pyme = new DatosPyme();

            var ordenamiento = new GridSortExpression { FieldName = "Cve_Grupo" };
            ordenamiento.SetSortOrder("Ascending");
            rgEquiposBaja.MasterTableView.SortExpressions.AddSortExpression(ordenamiento);

            EstatusEquipoBaja();
            fSEA.Visible = false;

            cboTecnologias.Visible = btnAgregar.Visible = Label2.Visible = true;

            Presupuesto = new OpEquiposAbEficiencia().ObtieneConceptosPresupuesto();
            rgPresupuesto.DataSource = Presupuesto.OrderBy(me => me.IdOrden);
            rgPresupuesto.DataBind();

            LstBajaEficienciaAgrupar = new List<EquipoBajaEficiencia>();
            LstAltaEficiencia = new List<EquipoAltaEficiencia>();
            LstBajaEficiencia = new List<EquipoBajaEficiencia>();
            LstGrupos = new List<EquipoBajaEficiencia>();

            rgEquiposBaja.DataSource = LstBajaEficiencia;
            rgEquiposBaja.DataBind();

            rgEquiposAlta.DataSource = null;
            rgEquiposAlta.DataBind();
        }
        #endregion

        #region Carga de Catalogos

        //METODO PARA CARGAR INICIALMENTE LAS TECNOLOGIAS
        protected void CargaTecnologias()
        {
            try
            {
                var idTarifa = new OpEquiposAbEficiencia().ObtenTarifa(TipoTarifa).Cve_Tarifa;
                //cboTecnologias.DataSource = new OpEquiposAbEficiencia().TecnologiasProveedor(idTarifa, IdProveedor);
                var tec = new OpEquiposAbEficiencia().TecnologiasProveedor(idTarifa, IdProveedor);
                var tecnologias = new DatosMontos().ObtieneTecnologiasAquisicion(idTarifa,IdProveedor);
                decimal? montoincentivo = new DatosMontos().montoDisponibleIncentivo();
                decimal montoMinimoIncentivo = new DatosMontos().montoMinimoIncentivo();

                if (montoincentivo > montoMinimoIncentivo)
                    cboTecnologias.DataSource = tec;
                else
                    cboTecnologias.DataSource = tecnologias;

                cboTecnologias.DataValueField = "IdElemento";
                cboTecnologias.DataTextField = "Elemento";
                cboTecnologias.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un problema al cargar las Tecnologías", ex);
            }

        }

        private void CargaCatalogoGrupos()
        {
            CboGrupos.Items.Clear();
            var lstGrupos = new List<EquipoBajaEficiencia>(); //LstBajaEficiencia.Distinct().Distinct();

            foreach (var equipoBajaEficiencia in LstBajaEficiencia)
            {
                var equipo = lstGrupos.FirstOrDefault(me => me.Cve_Grupo == equipoBajaEficiencia.Cve_Grupo);

                if (equipo == null)
                {
                    lstGrupos.Add(equipoBajaEficiencia);
                }
            }

            LstGrupos = lstGrupos;

            CboGrupos.DataSource = lstGrupos;
            CboGrupos.DataValueField = "Cve_Grupo";
            CboGrupos.DataTextField = "Dx_Grupo";
            CboGrupos.DataBind();

            CboGrupos.Items.Insert(0, new RadComboBoxItem(""));
            CboGrupos.SelectedIndex = 0;
        }

        #endregion

        #region Logica de Equipo de Baja Eficiencia

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidaTecnologiasCombinadas())
            {
                List<EquipoBajaEficiencia> lista = LstBajaEficiencia;
                var producto = new EquipoBajaEficiencia();
                producto.ID = FindMaxValue<EquipoBajaEficiencia>(LstBajaEficiencia, m => m.ID) + 1;
                producto.Cve_Grupo = FindMaxValue<EquipoBajaEficiencia>(LstBajaEficiencia, m => m.Cve_Grupo) + 1;
                producto.Cve_GrupoOrig = producto.Cve_Grupo;
                producto.Dx_Grupo = IndiceAColumna(producto.Cve_Grupo);
                producto.Dx_GrupoOrig = producto.Dx_Grupo;
                producto.Cve_Tecnologia = int.Parse(cboTecnologias.SelectedValue);
                producto.Dx_Tecnologia = cboTecnologias.SelectedItem.Text;
                producto.Ft_Tipo_Producto = -1;
                producto.Dx_Tipo_Producto = "";
                producto.Cve_Consumo = -1;
                producto.Dx_Consumo = "";
                PAEEEM.Entidades.ABE_UNIDAD_TECNOLOGIA unidad =
                    new OpEquiposAbEficiencia().UnidadTecnologica(int.Parse(cboTecnologias.SelectedValue));
                producto.Cve_Unidad = unidad.IDUNIDAD;
                producto.Dx_Unidad = unidad.UNIDAD;
                producto.DetalleTecnologia =
                    new Tecnologia().ValidaEquipoParaAlta(int.Parse(cboTecnologias.SelectedValue));

                producto.Cantidad = 0;

                if (producto.DetalleTecnologia.NumeroGrupos == 1)
                {
                    var productoMismaTecnologia =
                        LstBajaEficiencia.FirstOrDefault(me => me.Cve_Tecnologia == producto.Cve_Tecnologia);

                    if (productoMismaTecnologia != null)
                    {
                        producto.Cve_Grupo = productoMismaTecnologia.Cve_Grupo;
                        producto.Dx_Grupo = productoMismaTecnologia.Dx_Grupo;
                        producto.Dx_GrupoOrig = productoMismaTecnologia.Dx_GrupoOrig;
                        LimpiaEquiposAlta(productoMismaTecnologia.Cve_Grupo);
                        ResumenPresupuesto();
                    }

                }

                if (cboTecnologias.SelectedItem.Text.Contains("BANCO"))
                {
                    if (Datos.AplicaFPBc && Datos.AplicaPeridosBC)
                    {
                        var kvarCapacidad = Datos.KvAR * (1.25M);
                        var capacidadMinima = new OpEquiposAbEficiencia().ObtenMinimoCapacidadBC();

                        if (kvarCapacidad >= capacidadMinima)
                        {
                            lista.Add(producto);
                            LstBajaEficiencia = lista;
                            rgEquiposBaja.DataSource = LstBajaEficiencia;
                            rgEquiposBaja.DataBind();

                            EstatusEquipoBaja();
                        }
                        else
                        {
                            rwmVentana.RadAlert("Usuario no presenta los KVars minimos para un Banco de Capacitores", 300, 150,
                                            "Equipos", null);
                        }
                    }
                    else
                    {
                        rwmVentana.RadAlert("El usuario no aplica para la Tecnología Banco de Capacitores", 300, 150,
                                            "Equipos", null);
                    }
                }
                else
                {
                    lista.Add(producto);
                    LstBajaEficiencia = lista;
                    rgEquiposBaja.DataSource = LstBajaEficiencia;
                    rgEquiposBaja.DataBind();

                    EstatusEquipoBaja();
                }
            }
        }

        public bool ValidaTecnologiasCombinadas()
        {
            var cveTecnologia = int.Parse(cboTecnologias.SelectedValue);
            var lstcombinacionesTecnologia =
                new CatalogosTecnologia().ObtenCombinacionTecnologias(cveTecnologia);

            var lstTecnologiasBaja = new List<EquipoBajaEficiencia>();

            if (LstBajaEficiencia.Count > 0)
            {
                foreach (var equipoBajaEficiencia in LstBajaEficiencia)
                {
                    var tecnologia =
                        lstTecnologiasBaja.FirstOrDefault(me => me.Cve_Tecnologia == equipoBajaEficiencia.Cve_Tecnologia);

                    if (tecnologia == null)
                    {
                        lstTecnologiasBaja.Add(equipoBajaEficiencia);
                    }
                }

                foreach (var equipoBajaEficiencia in lstTecnologiasBaja)
                {
                    if (equipoBajaEficiencia.Cve_Tecnologia != cveTecnologia)
                    {
                        var tecnologiaCombinada =
                            lstcombinacionesTecnologia.FirstOrDefault(
                                me => me.CveTecnologiaCombinada == equipoBajaEficiencia.Cve_Tecnologia);

                        if (tecnologiaCombinada == null)
                        {
                            rwmVentana.RadAlert(
                                "La tecnología " + cboTecnologias.SelectedItem.Text +
                                " no puede combinarse con la tecnología " +
                                equipoBajaEficiencia.Dx_Tecnologia, 300, 150, "Equipos de Baja Eficiencia", null);
                            return false;
                        }

                        Datos.EsCombinacionTecnologias = true;
                    }
                }
            }

            return true;
        }

        private void VerEquiposAlta(bool equiposAlta)
        {
            if (equiposAlta)
            {
                fSEA.Visible = true;

                var lstEquiposAltaGrupo =
                    LstAltaEficiencia.FindAll(me => me.Cve_Grupo == EquipoBajaSelecionado.Cve_Grupo);

                if (lstEquiposAltaGrupo.Count > 0)
                {
                    rgEquiposAlta.DataSource = lstEquiposAltaGrupo;
                    rgEquiposAlta.DataBind();
                }

                else
                {
                    rgEquiposAlta.DataSource = new List<EquipoAltaEficiencia>();
                    rgEquiposAlta.DataBind();
                }
            }
            else
            {
                fSEA.Visible = false;
                rgEquiposAlta.DataSource = new List<EquipoAltaEficiencia>();
                rgEquiposAlta.DataBind();
            }

        }

        public void EstatusEquipoBaja()
        {
            LstBajaEficienciaAgrupar = new List<EquipoBajaEficiencia>();
        }

        public void LimpiaEquiposAlta(int cveGrupo)
        {
            LstAltaEficiencia.RemoveAll(me => me.Cve_Grupo == cveGrupo);
        }

        public int FindMaxValue<T>(List<T> list, Converter<T, int> projection)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            int maxValue = int.MinValue;
            foreach (T item in list)
            {
                int value = projection(item);
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }
            return maxValue;
        }

        public static string IndiceAColumna(int indice)
        {
            int ColumnaBase = 26;
            int DigitosMax = 7;
            string Digitos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (indice <= 0)
                throw new IndexOutOfRangeException("El indice debe ser un número positvo");

            if (indice <= ColumnaBase)
                return Digitos[indice - 1].ToString();

            var sb = new StringBuilder().Append(' ', DigitosMax);
            var actual = indice;
            var offset = DigitosMax;
            while (actual > 0)
            {
                sb[--offset] = Digitos[--actual % ColumnaBase];
                actual /= ColumnaBase;
            }
            return sb.ToString(offset, DigitosMax - offset);
        }

        public float Valor(string valor)
        {
            float ret = 0;
            float.TryParse(valor, out ret);
            return ret;
        }

        private bool ValidaEquiposBaja(CompDetalleTecnologia detalleTecnologia, int cveGrupo, int cantidadEquipos)
        {

            bool realizaInsercion = false;

            if (detalleTecnologia.DxEquipoAlta.ToUpper() == "MUCHOS")
                realizaInsercion = true;
            else
            {
                var totalEquiposBaja = 0;
                bool esnumero = int.TryParse(detalleTecnologia.DxEquipoAlta, out totalEquiposBaja);
                if (esnumero)
                {
                    var lstEquiposBajaGrupo = LstBajaEficiencia.FindAll(me => me.Cve_Grupo == cveGrupo);

                    if ((lstEquiposBajaGrupo.Sum(me => me.Cantidad) + cantidadEquipos) <= totalEquiposBaja)
                        realizaInsercion = true;
                }

            }

            return realizaInsercion;
        }

        #endregion

        #region RadGrid Equipos de Baja Eficiencia

        protected void rgEquiposBaja_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                rgEquiposBaja.DataSource = LstBajaEficiencia;
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }
        }

        protected void rgEquiposBaja_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                var item = e.Item as GridEditableItem;

                if (item != null)
                {
                    var keyName = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());
                    var montoChatarrizacion = 0.0M;

                    if (LstBajaEficiencia != null)
                    {
                        var equipobajaEficiencia = LstBajaEficiencia.First(p => p.ID == keyName);
                        var cantidad = int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);

                        //if (ValidaEquiposBaja(equipobajaEficiencia.DetalleTecnologia, equipobajaEficiencia.Cve_Grupo, cantidad))
                        //{

                        if (equipobajaEficiencia.DetalleTecnologia.CveChatarrizacion == true)
                        {
                            montoChatarrizacion =
                                (decimal)equipobajaEficiencia.DetalleTecnologia.MontoChatarrizacion;
                        }

                        //if (equipobajaEficiencia.Cve_Tecnologia == ElementoRefrigeracion)
                        if (equipobajaEficiencia.Dx_Tecnologia.Contains("REFRIGERA"))
                        {
                            LstBajaEficiencia.Where(p => p.ID == keyName).Select(p =>
                            {
                                p.Ft_Tipo_Producto =
                                    int.Parse(
                                        ((RadComboBox)item.FindControl("cboFt_Tipo_Producto")).SelectedValue);
                                p.Dx_Tipo_Producto =
                                    ((RadComboBox)item.FindControl("cboFt_Tipo_Producto")).Text;
                                //p.Cve_Consumo = int.Parse(((RadNumericTextBox)item.FindControl("txtCve_Capacidad")).Text);
                                p.Dx_Consumo = ((RadNumericTextBox)item.FindControl("txtCve_Capacidad")).Text;
                                p.Cantidad =
                                    int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
                                p.MontoChatarrizacion = montoChatarrizacion;
                                return p;
                            }).ToList();
                        }
                        else
                        {
                            LstBajaEficiencia.Where(p => p.ID == keyName).Select(p =>
                            {
                                p.Ft_Tipo_Producto =
                                    int.Parse(
                                        ((RadComboBox)item.FindControl("cboFt_Tipo_Producto")).SelectedValue);
                                p.Dx_Tipo_Producto =
                                    ((RadComboBox)item.FindControl("cboFt_Tipo_Producto")).Text;
                                p.Cve_Consumo =
                                    int.Parse(((RadComboBox)item.FindControl("cboCve_Capacidad")).SelectedValue);
                                p.Dx_Consumo = ((RadComboBox)item.FindControl("cboCve_Capacidad")).Text;
                                p.Cantidad =
                                    int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
                                p.MontoChatarrizacion = montoChatarrizacion;
                                return p;
                            }).ToList();
                        }

                        CargaCatalogoGrupos();
                        var equipoBaja = LstBajaEficiencia.First(p => p.ID == keyName);

                        //}
                        //else
                        //{
                        //    rwmVentana.RadAlert("No se puede asignar más de un equipo de Baja", 300, 150, "Equipos de Baja Eficiencia", null);
                        //}

                    }

                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }
        }

        protected void rgEquiposBaja_DeleteCommand(object sender, GridCommandEventArgs e)
        {

            try
            {
                var item = (GridEditableItem)e.Item;

                if (item != null)
                {
                    var keyName = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());

                    EquipoBajaSelecionado = LstBajaEficiencia.Find(p => p.ID == keyName);
                    var productoBajaEficiencia = LstBajaEficiencia.Find(p => p.ID == keyName);
                    int grupo = productoBajaEficiencia.Cve_Grupo;
                    LstBajaEficiencia.RemoveAll(p => p.ID == productoBajaEficiencia.ID);

                    CargaCatalogoGrupos();
                    LimpiaEquiposAlta(productoBajaEficiencia.Cve_Grupo);
                    VerEquiposAlta(false);
                    ResumenPresupuesto();
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }
        }

        protected void rgEquiposBaja_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = (GridDataItem)e.Item;

                    

                    //ocutar boton edit

                    var img = (System.Web.UI.WebControls.ImageButton)dataItem["colEditar"].Controls[0];
                    

                    int cap = 0;

                    if (img.CommandName.Contains("Edit"))
                    {
                        var prod =
                            LstBajaEficiencia.Find(p => p.ID == int.Parse(dataItem.GetDataKeyValue("ID").ToString()));

                        if (prod.Cantidad > 0) // && int.Parse(prod.Dx_Consumo) > 0)
                        {
                            img.Visible = false;
                        }

                        if (prod.Dx_Tecnologia.Contains("SUBESTACION") || prod.Dx_Tecnologia.Contains("BANCO"))
                            img.Visible = false;
                    }

                    if (!EsCaptura)
                    {
                        var img2 = (System.Web.UI.WebControls.ImageButton) dataItem["colEliminar"].Controls[0];

                        if (img2.CommandName.Contains("Delete"))
                        {
                            img2.Visible = false;
                        }
                    }
                    else
                    {
                        string id = dataItem.GetDataKeyValue("ID").ToString();
                        string gpo = LstBajaEficiencia.First(p => p.ID.ToString() == id).Cve_Grupo.ToString();

                        if (LstAltaEficiencia.Any(p => p.Cve_Grupo.ToString() == gpo))
                            dataItem.BackColor = Color.GreenYellow;
                        else
                            dataItem.BackColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {
            }

            if (e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                if (((RadNumericTextBox)(item.FindControl("txtCve_Capacidad"))).Text.Trim().Length > 0 &&
                    ((RadNumericTextBox)(item.FindControl("txtCve_Capacidad"))).Text.Trim() != "0")
                {
                    var productoBajaEficiencia =
                        LstBajaEficiencia.Find(p => p.ID == int.Parse(item.GetDataKeyValue("ID").ToString()));
                    int idTecnologia = productoBajaEficiencia.Cve_Tecnologia;

                }
                else
                {
                    var productoBajaEficiencia =
                        LstBajaEficiencia.Find(p => p.ID == int.Parse(item.GetDataKeyValue("ID").ToString()));
                    int idTecnologia = productoBajaEficiencia.Cve_Tecnologia;

                    if (!(e.Item is IGridInsertItem))
                    {
                        //                    
                        var cboFt_Tipo_Producto = (RadComboBox)item.FindControl("cboFt_Tipo_Producto");
                        cboFt_Tipo_Producto.DataSource =
                            new OpEquiposAbEficiencia().ProductosBajaEficiencia(idTecnologia);
                        cboFt_Tipo_Producto.DataTextField = "Elemento";
                        cboFt_Tipo_Producto.DataValueField = "IdElemento";
                        cboFt_Tipo_Producto.DataBind();

                        var idTiproducto = DataBinder.Eval(e.Item.DataItem, "Ft_Tipo_Producto").ToString();
                        cboFt_Tipo_Producto.SelectedValue = idTiproducto;

                        if (productoBajaEficiencia.Dx_Tecnologia.Contains("REFRIGERA"))
                        {
                            var cboCve_Capacidad = item.FindControl("cboCve_Capacidad") as RadComboBox;
                            var txtCve_Capacidad = item.FindControl("txtCve_Capacidad") as RadNumericTextBox;
                            txtCve_Capacidad.Visible = true;
                            txtCve_Capacidad.Text = productoBajaEficiencia.Dx_Consumo;
                            cboCve_Capacidad.Visible = false;
                        }
                        else
                        {
                            var txtCve_Capacidad = item.FindControl("txtCve_Capacidad") as RadNumericTextBox;
                            txtCve_Capacidad.Visible = false;

                            var cboCapacidad = item.FindControl("cboCve_Capacidad") as RadComboBox;
                            if (cboCapacidad != null)
                            {
                                if (cboFt_Tipo_Producto.SelectedValue != "-1")
                                {
                                    cboCapacidad.DataSource =
                                        new OpEquiposAbEficiencia().ObtenCapacidades(
                                            int.Parse(cboFt_Tipo_Producto.SelectedValue),
                                            productoBajaEficiencia.DetalleTecnologia.CveEsquema,
                                            productoBajaEficiencia.Cve_Tecnologia);
                                    cboCapacidad.DataTextField = "DescripcionCatalogo";
                                    cboCapacidad.DataValueField = "CveValorCatalogo";
                                    cboCapacidad.DataBind();

                                    if (productoBajaEficiencia.Cve_Consumo != 0)
                                    {
                                        cboCapacidad.SelectedValue = productoBajaEficiencia.Cve_Consumo.ToString();
                                    }
                                    else
                                    {
                                        cboCapacidad.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
                                        cboCapacidad.SelectedIndex = 0;
                                    }
                                }
                                cboCapacidad.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        protected void rgEquiposBaja_ItemCommand(object sender, GridCommandEventArgs e)
        {

            if (e.CommandName == "Seleccionar")
            {
                bool permitir = true;

                //EBajaEficienciaSeleccionado = LstBajaEficiencia.Find(p => p.ID == int.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString()));

                var item = (GridDataItem)e.Item;
                var eBajaEficiencia =
                    LstBajaEficiencia.Find(p => p.ID == int.Parse(item.GetDataKeyValue("ID").ToString()));

                if (eBajaEficiencia.DetalleTecnologia.CveTipoMovimiento == "2")
                {
                foreach (var eqBaja in LstBajaEficiencia.Where(p => p.Cve_Grupo == eBajaEficiencia.Cve_Grupo))
                {
                    if (eqBaja.Cantidad == 0)
                    {
                        permitir = false;
                        break;
                    }
                }
                }

                if (permitir)
                {
                    EquipoBajaSelecionado = eBajaEficiencia;

                    item.Selected = true;
                    legEquiposAlta.InnerText = "Equipos de Alta Eficiencia  - " + eBajaEficiencia.Dx_Tecnologia +
                                               " - Grupo " + eBajaEficiencia.Dx_Grupo;

                    var equiposAlta = false;
                    //LstAltaEficiencia = eBajaEficiencia.EquiposAltaEficiencia;
                    //VALIDACION PARA VISUALIZAR EQUIPOS DE ALTA
                    if (eBajaEficiencia.DetalleTecnologia.CveTipoMovimiento == "1")
                    {
                        equiposAlta = true;
                    }
                    else if (eBajaEficiencia.DetalleTecnologia.CveTipoMovimiento == "2")
                    {
                        //if (eBajaEficiencia.Cve_Tecnologia == ElementoRefrigeracion)
                        if (eBajaEficiencia.Dx_Tecnologia.Contains("REFRIGERA"))
                        {

                            if (eBajaEficiencia.Ft_Tipo_Producto != -1 && eBajaEficiencia.Cantidad > 0)
                            {
                                equiposAlta = true;
                            }
                        }
                        else
                        {
                            if (eBajaEficiencia.Ft_Tipo_Producto != -1 && eBajaEficiencia.Cve_Consumo != -1 &&
                                eBajaEficiencia.Cantidad > 0)
                                equiposAlta = true;
                        }
                    }

                    if(EsCaptura)
                        CalculaCapacidadesMaximas(eBajaEficiencia.DetalleTecnologia.CveEsquema, eBajaEficiencia.Cve_Grupo);
                    
                    VerEquiposAlta(equiposAlta);
                }
                else
                {
                    rwmVentana.RadAlert("Existen datos sin editar en este grupo.", 300, 150, "Información", null);
                }

            }
            else if (e.CommandName == "Edit")
            {
                var EquipoB =
                    LstBajaEficiencia.Find(
                        p => p.ID == int.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString()));

                if (EquipoB.DetalleTecnologia.CveTipoMovimiento == "1")
                {
                    e.Canceled = true;
                    rwmVentana.RadAlert("En equipos de adquision no hay captura de equipo de baja eficiencia.", 300, 150,
                                        "Equipo de Alta Eficiencia", null);
                }

            }
            else
            {
                return;
            }
        }

        protected void cboFt_Tipo_Producto_SelectedIndexChanged1(object sender,
                                                                 RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                var cboFtTipoProducto = sender as RadComboBox;

                if (cboFtTipoProducto != null)
                {
                    var editedItem = cboFtTipoProducto.NamingContainer as GridEditableItem;
                    var equipoBajaEficiencia =
                        LstBajaEficiencia.Find(p => p.ID == int.Parse(editedItem.GetDataKeyValue("ID").ToString()));

                    if (editedItem != null)
                    {
                        var cboCapacidad = editedItem["Cve_Consumo"].FindControl("cboCve_Capacidad") as RadComboBox;
                        if (cboCapacidad != null)
                        {
                            cboCapacidad.DataSource =
                                new OpEquiposAbEficiencia().ObtenCapacidades(
                                    int.Parse(cboFtTipoProducto.SelectedValue),
                                    equipoBajaEficiencia.DetalleTecnologia.CveEsquema,
                                    equipoBajaEficiencia.Cve_Tecnologia);
                            cboCapacidad.DataTextField = "DescripcionCatalogo";
                            cboCapacidad.DataValueField = "CveValorCatalogo";
                            cboCapacidad.DataBind();

                            cboCapacidad.Items.Insert(0, new RadComboBoxItem("Seleccione..."));
                            cboCapacidad.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }
        }

        protected void chkAgrupar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var editedItem = (sender as CheckBox).NamingContainer as GridDataItem;

                var equipoBajaEficiencia =
                    LstBajaEficiencia.Find(p => p.ID == int.Parse(editedItem.GetDataKeyValue("ID").ToString()));

                if (!(sender as CheckBox).Checked)
                {
                    LstBajaEficienciaAgrupar.Remove(equipoBajaEficiencia);
                }

                else
                {

                    if (equipoBajaEficiencia.DetalleTecnologia.DxEquiposBaja.ToUpper() != "MUCHOS")
                    {
                        (sender as CheckBox).Checked = false;
                        rwmVentana.RadAlert(
                            "No se pueden agrupar equipos de la Tecnología " + equipoBajaEficiencia.Dx_Tecnologia, 300,
                            150,
                            "Equipos de Baja Eficiencia", null);
                        return;
                    }

                    if (equipoBajaEficiencia.DetalleTecnologia.NumeroGrupos == 1)
                    {
                        (sender as CheckBox).Checked = false;
                        rwmVentana.RadAlert(
                            "No se puede manipula la agrupación de equipos de la Tecnología " +
                            equipoBajaEficiencia.Dx_Tecnologia, 300, 150,
                            "Equipos de Baja Eficiencia", null);
                        return;
                    }


                    if (LstBajaEficienciaAgrupar.Count > 0)
                    {
                        var tecnologia =
                            LstBajaEficienciaAgrupar.Select(me => me.Cve_Tecnologia).Distinct().FirstOrDefault();

                        if (tecnologia != equipoBajaEficiencia.Cve_Tecnologia)
                        {
                            (sender as CheckBox).Checked = false;
                            rwmVentana.RadAlert("Solo se pueden Equipos productos de la misma tecnologia", 300, 150,
                                                "Equipos de Baja Eficiencia", null);
                            return;
                        }
                        else
                        {
                            LstBajaEficienciaAgrupar.Add(equipoBajaEficiencia);
                        }
                    }
                    else
                    {
                        LstBajaEficienciaAgrupar.Add(equipoBajaEficiencia);
                    }
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }

        }

        protected void btnAgrupar_Click(object sender, EventArgs e)
        {
            try
            {
                if (LstBajaEficienciaAgrupar.Count > 0)
                {
                    //var primerEquipo = LstBajaEficienciaAgrupar.First();
                    //LimpiaEquiposAlta(primerEquipo.Cve_Grupo);

                    var cveGrupo = FindMaxValue<EquipoBajaEficiencia>(LstBajaEficiencia, m => m.Cve_Grupo) + 1;
                    var dxGrupo = IndiceAColumna(cveGrupo);

                    foreach (var equipoBajaEficiencia in LstBajaEficienciaAgrupar)
                    {
                        LimpiaEquiposAlta(equipoBajaEficiencia.Cve_Grupo);
                        var equipoBaja = LstBajaEficiencia.FirstOrDefault(me => me.ID == equipoBajaEficiencia.ID);

                        if (equipoBaja != null)
                        {
                            equipoBaja.Cve_Grupo = cveGrupo;
                            equipoBaja.Dx_Grupo = dxGrupo;
                            equipoBajaEficiencia.Cve_Grupo = cveGrupo;
                            equipoBajaEficiencia.Dx_Grupo = dxGrupo;
                        }
                    }
                    ResumenPresupuesto();
                    CargaCatalogoGrupos();
                }

                //int indice = LstBajaEficiencia.FindIndex(p => p.ID == EBajaEficienciaAgrupar2.ID);//ProductosBajaEficiencia.IndexOf(ProductoBajaEficienciaAgrupar2);

                //LstBajaEficiencia[indice].Cve_Grupo = EBajaEficienciaAgrupar1.Cve_Grupo;
                //LstBajaEficiencia[indice].Dx_Grupo = EBajaEficienciaAgrupar1.Dx_Grupo;

                //EBajaEficienciaAgrupar1 = null;
                //EBajaEficienciaAgrupar2 = null;

                EstatusEquipoBaja();
                rgEquiposBaja.DataSource = LstBajaEficiencia;
                rgEquiposBaja.DataBind();
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }

            if (rgEquiposAlta.Visible)
                rgEquiposAlta.Rebind();
        }

        protected void btnDesagrupar_Click(object sender, EventArgs e)
        {
            try
            {
                if (LstBajaEficienciaAgrupar.Count > 0)
                {

                    var cveGrupo = FindMaxValue<EquipoBajaEficiencia>(LstBajaEficiencia, m => m.Cve_Grupo) + 1;
                    //var dxGrupo = IndiceAColumna(cveGrupo);

                    foreach (var equipoBajaEficiencia in LstBajaEficienciaAgrupar)
                    {
                        LimpiaEquiposAlta(equipoBajaEficiencia.Cve_Grupo);
                        var equipoBaja = LstBajaEficiencia.FirstOrDefault(me => me.ID == equipoBajaEficiencia.ID);

                        if (equipoBaja != null)
                        {
                            equipoBaja.Cve_Grupo = cveGrupo; //equipoBajaEficiencia.Cve_GrupoOrig;
                            var dxGrupo = IndiceAColumna(cveGrupo);
                            equipoBaja.Dx_Grupo = dxGrupo;
                            equipoBajaEficiencia.Cve_Grupo = cveGrupo;
                            equipoBajaEficiencia.Dx_Grupo = dxGrupo;
                        }

                        cveGrupo++;
                    }

                    ResumenPresupuesto();
                    CargaCatalogoGrupos();
                }

                //int indice = LstBajaEficiencia.FindIndex(p => p.ID == EBajaEficienciaAgrupar1.ID);

                //// Desagrupa solo si hay mas de 1 elemento en el grupo
                //if (LstBajaEficiencia.FindAll(p => p.Cve_Grupo == EBajaEficienciaAgrupar1.Cve_Grupo).Count > 1)
                //{
                //    LstBajaEficiencia[indice].Cve_Grupo = FindMaxValue<EquipoBajaEficiencia>(LstBajaEficiencia, m => m.ID) + 1; ;
                //    LstBajaEficiencia[indice].Dx_Grupo = IndiceAColumna(LstBajaEficiencia[indice].Cve_Grupo);
                //}

                //EBajaEficienciaAgrupar1 = null;
                //EBajaEficienciaAgrupar2 = null;

                EstatusEquipoBaja();
                rgEquiposBaja.DataSource = LstBajaEficiencia;
                rgEquiposBaja.DataBind();
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipos de Baja Eficiencia", null);
            }

            if (rgEquiposAlta.Visible)
                rgEquiposAlta.Rebind();
        }

        #endregion

        #region RadGrid de Equipo de Alta Eficiencia

        protected void rgEquiposAlta_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                rgEquiposAlta.DataSource =
                    LstAltaEficiencia.FindAll(me => me.Cve_Grupo == EquipoBajaSelecionado.Cve_Grupo);
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }
        }

        protected void rgEquiposAlta_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                var item = e.Item as GridEditableItem;

                if (item != null)
                {
                    var equipoBe = LstBajaEficiencia.First(p => p.ID == EquipoBajaSelecionado.ID);
                    var equipoAe = new EquipoAltaEficiencia();
                    var cantidad = int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);

                    if (LstAltaEficiencia != null)
                    {
                        if (LstAltaEficiencia.Count > 0)
                            equipoAe.ID = LstAltaEficiencia.Max(p => p.ID) + 1;
                        else
                            equipoAe.ID = 1;
                    }

                    var importeTotalSinIva =
                        Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtPrecio_Unitario")).Text) * cantidad;
                    equipoAe.Cve_Grupo = equipoBe.Cve_Grupo;
                    equipoAe.Dx_Grupo = equipoBe.Dx_Grupo;
                    equipoAe.Cve_Tecnologia = equipoBe.Cve_Tecnologia;
                    equipoAe.Dx_Tecnologia = equipoBe.Dx_Tecnologia;
                    equipoAe.Cve_Marca = int.Parse(((RadComboBox)item.FindControl("cboCve_Marca")).SelectedValue);
                    equipoAe.Dx_Marca = ((RadComboBox)item.FindControl("cboCve_Marca")).Text;
                    equipoAe.Cve_Modelo = int.Parse(((RadComboBox)item.FindControl("cboCve_Modelo")).SelectedValue);
                    equipoAe.Dx_Modelo = ((RadComboBox)item.FindControl("cboCve_Modelo")).Text;
                    equipoAe.FtTipoProducto = new OpEquiposAbEficiencia().ObtenTipoProducto(equipoAe.Cve_Modelo);
                    equipoAe.Cantidad = int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
                    equipoAe.Dx_Sistema = ((Label) item.FindControl("lblNo_Capacidad")).Text;
                    equipoAe.Precio_Distribuidor =
                        Convert.ToDecimal(
                            string.IsNullOrEmpty(((Label)item.FindControl("lblPrecio_DistribuidorEdicion")).Text)
                                ? "0"
                                : ((Label)item.FindControl("lblPrecio_DistribuidorEdicion")).Text);
                    equipoAe.Precio_Unitario =
                        Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtPrecio_Unitario")).Text);
                    equipoAe.Importe_Total_Sin_IVA = importeTotalSinIva;
                    equipoAe.Importe_Total = equipoAe.Importe_Total_Sin_IVA +
                                             (equipoAe.Importe_Total_Sin_IVA*Datos.ValorIva);
                    equipoAe.Gasto_Instalacion =
                        Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtGasto_Instalacion")).Text);

                    if (EquipoBajaSelecionado.DetalleTecnologia.CveIncentivo)
                    {
                        equipoAe.MontoIncentivo = (equipoAe.Importe_Total_Sin_IVA + (equipoAe.Importe_Total_Sin_IVA * Datos.ValorIva)) *
                                                  (equipoBe.DetalleTecnologia.MontoIncentivo / 100.0M);
                    }

                    if (EquipoBajaSelecionado.DetalleTecnologia.CveEsquema != 1)
                    {
                        equipoAe.No_Capacidad =
                            Convert.ToDecimal(
                                LstCapacidadesAltaEficiencia.FirstOrDefault(me => me.Cve_Modelo == equipoAe.Cve_Modelo)
                                                            .CapacidadAquipo);

                        if (ValidaTopeCapacidad((equipoAe.No_Capacidad * equipoAe.Cantidad), 0.0M, false,
                                                equipoAe.Cve_Grupo))
                        {
                            LstAltaEficiencia.Add(equipoAe);

                            var equipoConAhorros = CalculaAhorroTecnologia(equipoAe);

                            //equipoAe.KwAhorro = equipoConAhorros.KwAhorro;
                            //equipoAe.KwhAhorro = equipoAe.KwhAhorro;

                            if (equipoAe.KwAhorro < 0 || equipoAe.KwhAhorro < 0)
                            {
                                LstAltaEficiencia.RemoveAll(p => p.ID == equipoAe.ID);

                                rwmVentana.RadAlert(
                                    "Su equipo ineficiente no presenta el suficiente consumo de energía para realizar la sustitución por este equipo eficiente",
                                    300, 150,
                                    "Equipo de Alta Eficiencia", null);
                            }
                            else
                            {
                                //LstAltaEficiencia.Add(equipoAe);

                                LstAltaEficiencia.Where(p => p.ID == equipoAe.ID).Select
                                    (p =>
                                    {
                                        p.KwAhorro = equipoConAhorros.KwAhorro;
                                        p.KwhAhorro = equipoConAhorros.KwhAhorro;
                                        return p;
                                    }).ToList();

                                if (equipoAe.Dx_Tecnologia.Contains("SUBESTACIONES"))
                                {
                                    IsSE = true;
                                }

                                if (equipoAe.Dx_Tecnologia.Contains("BANCO"))
                                    IsBC = true;

                                TotalCapacidadEquiposAlta = TotalCapacidadEquiposAlta +
                                                            (equipoAe.No_Capacidad * equipoAe.Cantidad);
                            }

                            rgEquiposBaja.Rebind();
                        }
                        else
                        {
                            rwmVentana.RadAlert(
                                "La capacidad de los Equipos seleccionados supera la capacidad máxima permitida",
                                300, 150,
                                "Equipo de Alta Eficiencia", null);
                        }
                    }
                    else
                    {
                        LstAltaEficiencia.Add(equipoAe);
                        var equipoConAhorros = CalculaAhorroTecnologia(equipoAe);

                        if (equipoAe.KwAhorro < 0 || equipoAe.KwhAhorro < 0)
                        {
                            LstAltaEficiencia.RemoveAll(p => p.ID == equipoAe.ID);
                            rwmVentana.RadAlert(
                                "Su equipo ineficiente no presenta el suficiente consumo de energía para realizar la sustitución por este equipo eficiente",
                                300, 150,
                                "Equipo de Alta Eficiencia", null);
                        }
                        else
                        {
                            LstAltaEficiencia.Where(p => p.ID == equipoAe.ID).Select
                                (p =>
                                {
                                    p.KwAhorro = equipoConAhorros.KwAhorro;
                                    p.KwhAhorro = equipoConAhorros.KwhAhorro;
                                    return p;
                                }).ToList();
                        }
                    }

                    if (ResumenPresupuesto())
                    {
                        rgEquiposAlta.DataSource =
                            LstAltaEficiencia.FindAll(me => me.Cve_Grupo == EquipoBajaSelecionado.Cve_Grupo);
                        rgEquiposAlta.DataBind(); 
                    }
                    else
                    {
                        LstAltaEficiencia.RemoveAll(me => me.ID == equipoAe.ID);
                        rwmVentana.RadAlert("Capture un monto mayor para el equipo de Alta", 300, 150, "Equipo de Alta Eficiencia", null);
                    }
                                       
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }
        }

        protected void rgEquiposAlta_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                var item = e.Item as GridEditableItem;

                if (item != null)
                {
                    var keyName = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());

                    if (LstAltaEficiencia != null)
                    {
                        var gastoChatarrizacion = 0.0M;
                        var montoIncentivo = 0.0M;
                        var equipoAlta = LstAltaEficiencia.FirstOrDefault(me => me.ID == keyName);
                        var capacidadAnterior = equipoAlta.No_Capacidad;

                        //TotalCapacidadEquiposAlta = TotalCapacidadEquiposAlta -
                        //                            Convert.ToDecimal(equipoAlta.No_Capacidad);

                        //if (EquipoBajaSelecionado.DetalleTecnologia.CveChatarrizacion == true)
                        //{
                        //    gastoChatarrizacion =
                        //        (decimal)EquipoBajaSelecionado.DetalleTecnologia.MontoChatarrizacion;
                        //}

                        if (EquipoBajaSelecionado.DetalleTecnologia.CveIncentivo == true)
                        {
                            montoIncentivo = equipoAlta.Importe_Total_Sin_IVA *
                                             (EquipoBajaSelecionado.DetalleTecnologia.MontoIncentivo / 100.0M);
                        }

                        equipoAlta.Cve_Marca =
                            int.Parse(((RadComboBox)item.FindControl("cboCve_Marca")).SelectedValue);
                        equipoAlta.Dx_Marca = ((RadComboBox)item.FindControl("cboCve_Marca")).Text;
                        equipoAlta.Cve_Modelo =
                            int.Parse(((RadComboBox)item.FindControl("cboCve_Modelo")).SelectedValue);
                        equipoAlta.Dx_Modelo = ((RadComboBox)item.FindControl("cboCve_Modelo")).Text;
                        equipoAlta.FtTipoProducto =
                            new OpEquiposAbEficiencia().ObtenTipoProducto(
                                int.Parse(((RadComboBox)item.FindControl("cboCve_Modelo")).SelectedValue));
                        equipoAlta.Cantidad = int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
                        equipoAlta.Precio_Distribuidor =
                            Convert.ToDecimal(((Label)item.FindControl("lblPrecio_DistribuidorEdicion")).Text);
                        equipoAlta.Precio_Unitario =
                            Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtPrecio_Unitario")).Text);
                        equipoAlta.Importe_Total_Sin_IVA =
                            Convert.ToDecimal(((Label)item.FindControl("lblImporte_Total_Sin_IVAEdicion")).Text);
                        equipoAlta.Gasto_Instalacion =
                            Convert.ToDecimal(((RadNumericTextBox)item.FindControl("txtGasto_Instalacion")).Text);
                        equipoAlta.MontoChatarrizacion = gastoChatarrizacion;
                        equipoAlta.MontoIncentivo = montoIncentivo;

                        var capacidadNueva = EquipoBajaSelecionado.DetalleTecnologia.CveEsquema != 1
                                                 ? Convert.ToDecimal(LstCapacidadesAltaEficiencia.FirstOrDefault(
                                                     me => me.Cve_Modelo == equipoAlta.Cve_Modelo)
                                                                                                 .CapacidadAquipo)
                                                 : 0;

                        equipoAlta.No_Capacidad = capacidadNueva;

                        //TotalCapacidadEquiposAlta = TotalCapacidadEquiposAlta -
                        //                            Convert.ToDecimal(equipoAlta.No_Capacidad);

                        var equipoConAhorros = CalculaAhorroTecnologia(equipoAlta);

                        if (ValidaTopeCapacidad((Convert.ToDecimal(capacidadNueva) * equipoAlta.Cantidad),
                                                capacidadAnterior, true, EquipoBajaSelecionado.Cve_Grupo))
                        {

                            LstAltaEficiencia
                                .Where(p => p.ID == keyName)
                                .Select(p =>
                                {
                                    p.Cve_Marca =
                                        int.Parse(((RadComboBox)item.FindControl("cboCve_Marca")).SelectedValue);
                                    p.Dx_Marca = ((RadComboBox)item.FindControl("cboCve_Marca")).Text;
                                    p.Cve_Modelo =
                                        int.Parse(
                                            ((RadComboBox)item.FindControl("cboCve_Modelo")).SelectedValue);
                                    p.Dx_Modelo = ((RadComboBox)item.FindControl("cboCve_Modelo")).Text;
                                    p.FtTipoProducto =
                                        new OpEquiposAbEficiencia().ObtenTipoProducto(
                                            int.Parse(
                                                ((RadComboBox)item.FindControl("cboCve_Modelo")).SelectedValue));
                                    p.Cantidad =
                                        int.Parse(((RadNumericTextBox)item.FindControl("txtCantidad")).Text);
                                    p.Precio_Distribuidor =
                                        Convert.ToDecimal(
                                            ((Label)item.FindControl("lblPrecio_DistribuidorEdicion")).Text);
                                    p.Precio_Unitario =
                                        Convert.ToDecimal(
                                            ((RadNumericTextBox)item.FindControl("txtPrecio_Unitario")).Text);
                                    p.Importe_Total_Sin_IVA =
                                        Convert.ToDecimal(
                                            ((Label)item.FindControl("lblImporte_Total_Sin_IVAEdicion")).Text);
                                    p.Gasto_Instalacion =
                                        Convert.ToDecimal(
                                            ((RadNumericTextBox)item.FindControl("txtGasto_Instalacion")).Text);
                                    p.MontoChatarrizacion = gastoChatarrizacion;
                                    p.MontoIncentivo = montoIncentivo;
                                    p.No_Capacidad = capacidadNueva;
                                    p.KwAhorro = equipoConAhorros.KwAhorro;
                                    p.KwhAhorro = equipoConAhorros.KwhAhorro;
                                    return p;
                                }).ToList();

                            TotalCapacidadEquiposAlta = (TotalCapacidadEquiposAlta - capacidadAnterior) +
                                                        capacidadNueva;

                            rgEquiposAlta.DataSource =
                                LstAltaEficiencia.FindAll(me => me.Cve_Grupo == EquipoBajaSelecionado.Cve_Grupo);
                            rgEquiposAlta.DataBind();

                            ResumenPresupuesto();
                        }
                        else
                        {
                            //TotalCapacidadEquiposAlta = TotalCapacidadEquiposAlta +
                            //                        Convert.ToDecimal(equipoAlta.No_Capacidad);

                            rwmVentana.RadAlert(
                                "La capacidad de los Equipos seleccionados supera la capacidad máxima permitida",
                                300,
                                150, "Equipo de Alta Eficiencia", null);
                        }

                        //LstBajaEficiencia.First(p => p.ID == EBajaEficienciaSeleccionado.ID).EquiposAltaEficiencia = LstAltaEficiencia;                        
                    }

                }

            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }

        }

        protected void rgEquiposAlta_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                var item = e.Item as GridEditableItem;

                if (item != null)
                {
                    var keyName = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());

                    if (LstAltaEficiencia != null)
                    {
                        var equipoAlta = LstAltaEficiencia.FirstOrDefault(me => me.ID == keyName);
                        if (equipoAlta != null)
                        {
                            TopeMaximoCapacidad = TopeMaximoCapacidad -
                                                  (equipoAlta.No_Capacidad * equipoAlta.Cantidad);
                        }

                        LstAltaEficiencia.RemoveAll(p => p.ID == keyName);
                        rgEquiposAlta.DataSource =
                            LstAltaEficiencia.FindAll(me => me.Cve_Grupo == EquipoBajaSelecionado.Cve_Grupo);
                        rgEquiposAlta.DataBind();

                        ResumenPresupuesto();
                        rgEquiposBaja.Rebind();


                        if (equipoAlta.Dx_Tecnologia.Contains("SUBESTACIONES"))
                        {
                            IsSE = false;
                        }

                        if (equipoAlta.Dx_Tecnologia.Contains("BANCO"))
                            IsBC = false;
                    }

                    ResumenPresupuesto();
                }

            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }
        }

        protected void rgEquiposAlta_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                if (!ValidaAltaEquipo(EquipoBajaSelecionado.DetalleTecnologia, EquipoBajaSelecionado.Cve_Grupo, 0))
                {
                    e.Canceled = true;
                    rwmVentana.RadAlert("No puede agregar mas equipos de acuerdo a lo establecido", 300, 150,
                                        "Equipo de Alta Eficiencia", null);
                }
                else
                {
                    CalculaCapacidadesMaximas(EquipoBajaSelecionado.DetalleTecnologia.CveEsquema,
                                              EquipoBajaSelecionado.Cve_Grupo);
                }
            }
        }

        protected void rgEquiposAlta_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                var img = (System.Web.UI.WebControls.ImageButton)item["EditCommandColumn"].Controls[0];
                img.Visible = (img.CommandName.Contains("Insert"));

                if (!EsCaptura)
                {
                    var img2 = (System.Web.UI.WebControls.ImageButton)item["colEliminar"].Controls[0];

                    if (img2.CommandName.Contains("Delete"))
                    {
                        img2.Visible = false;
                    }
                }
            }

            if (e.Item.IsInEditMode)
            {
                var item = (GridEditableItem)e.Item;

                if (!(e.Item is IGridInsertItem))
                {

                    rwmVentana.RadAlert("No editable. Para modificarlo, elimínelo y vuela a insertarlo.", 300, 150,
                                        "Información", null);

                    var cboCveMarca = ((RadComboBox)item.FindControl("cboCve_Marca"));
                    var txtGasto_Instalacion = ((RadNumericTextBox)item.FindControl("txtGasto_Instalacion"));
                    cboCveMarca.DataSource =
                        new OpEquiposAbEficiencia().MarcasAltaEficiencia(EquipoBajaSelecionado.Cve_Tecnologia, IdProveedor);
                    cboCveMarca.DataTextField = "Elemento";
                    cboCveMarca.DataValueField = "IdElemento";
                    cboCveMarca.DataBind();
                    e.Item.Edit = false;
                    rgEquiposAlta.Rebind();

                    if (!EquipoBajaSelecionado.DetalleTecnologia.CveGasto)
                    {
                        txtGasto_Instalacion.Text = "0";
                        txtGasto_Instalacion.Enabled = false;
                    }

                }
                else
                {
                    var txtGasto_Instalacion = ((RadNumericTextBox)item.FindControl("txtGasto_Instalacion"));
                    var txtCantidad = ((RadNumericTextBox)item.FindControl("txtCantidad"));
                    var cboCveMarca = ((RadComboBox)item.FindControl("cboCve_Marca"));
                    cboCveMarca.DataSource =
                        new OpEquiposAbEficiencia().MarcasAltaEficiencia(EquipoBajaSelecionado.Cve_Tecnologia, IdProveedor);
                    cboCveMarca.DataTextField = "Elemento";
                    cboCveMarca.DataValueField = "IdElemento";
                    cboCveMarca.DataBind();

                    cboCveMarca.SelectedValue = "-1";

                    var cboCveModelo = item.FindControl("cboCve_Modelo") as RadComboBox;
                    if (cboCveModelo != null)
                    {
                        cboCveModelo.Items.Clear();
                        cboCveModelo.Items.Insert(0, new RadComboBoxItem("Seleccione", "-1"));
                    }

                    if (!EquipoBajaSelecionado.DetalleTecnologia.CveGasto)
                    {
                        txtGasto_Instalacion.Text = "0";
                        txtGasto_Instalacion.Enabled = false;
                    }

                    if (EquipoBajaSelecionado.Dx_Tecnologia.Contains("SUBESTACION") ||
                        EquipoBajaSelecionado.Dx_Tecnologia.Contains("BANCO"))
                    {
                        txtCantidad.Text = "1";
                        txtCantidad.Enabled = false;
                    }
                }

            }
        }

        protected void cboCve_Marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var cboMarca = sender as RadComboBox;

                if (cboMarca != null)
                {
                    var editedItem = cboMarca.NamingContainer as GridEditableItem;

                    if (editedItem != null)
                    {
                        var cboModelo = editedItem["Cve_Modelo"].FindControl("cboCve_Modelo") as RadComboBox;


                        if (cboModelo != null)
                        {
                            var lstEquiposAlta = new List<EquipoAltaEficiencia>();

                            if (EquipoBajaSelecionado.DetalleTecnologia.CveEsquema != 1)
                            {
                                if (EquipoBajaSelecionado.DetalleTecnologia.DxNombreGeneral.Contains("SUBESTACIONES"))
                                {
                                    lstEquiposAlta =
                                        new OpEquiposAbEficiencia().ObtenSubestacionesElectricas(
                                            int.Parse(cboMarca.SelectedValue),
                                            IdProveedor);
                                }
                                else if (EquipoBajaSelecionado.DetalleTecnologia.DxNombreGeneral.Contains("BANCO"))
                                {
                                    lstEquiposAlta =
                                        new OpEquiposAbEficiencia().ObtenBancoCapacitores(
                                            int.Parse(cboMarca.SelectedValue),
                                            IdProveedor,
                                            EquipoBajaSelecionado.Cve_Tecnologia,
                                            Convert.ToDouble(Datos.KvAR));
                                }
                                else
                                {
                                    lstEquiposAlta = new OpEquiposAbEficiencia().ObtenProductosAltaEficiencia(
                                        EquipoBajaSelecionado.Ft_Tipo_Producto, int.Parse(cboMarca.SelectedValue),
                                        IdProveedor,
                                        Convert.ToDouble(TopeMaximoCapacidad - GetCapacidadAcumulada()), EquipoBajaSelecionado.Cve_Tecnologia);
                                }
                            }
                            else
                            {
                                lstEquiposAlta =
                                    new OpEquiposAbEficiencia().ObtenProductosSistemasAltaEficiencia(
                                        IdProveedor, int.Parse(cboMarca.SelectedValue),
                                        EquipoBajaSelecionado.Cve_Consumo, EquipoBajaSelecionado.Cve_Tecnologia);
                            }

                            LstCapacidadesAltaEficiencia = lstEquiposAlta;

                            cboModelo.DataSource = lstEquiposAlta;
                            cboModelo.DataTextField = "Dx_Modelo";
                            cboModelo.DataValueField = "Cve_Modelo";
                            cboModelo.DataBind();

                            cboModelo.Items.Insert(0, new RadComboBoxItem("Seleccione"));
                            cboModelo.SelectedIndex = 0;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }

        }

        protected void cboCve_Modelo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                var cboModelo = sender as RadComboBox;

                if (cboModelo != null)
                {
                    var editedItem = cboModelo.NamingContainer as GridEditableItem;

                    if (editedItem != null)
                    {

                        var lblPrecioDistribuidorEdicion =
                            editedItem.FindControl("lblPrecio_DistribuidorEdicion") as Label;
                        var lblNo_Capacidad = editedItem.FindControl("lblNo_Capacidad") as Label;

                        var modelo = int.Parse(cboModelo.SelectedValue);
                        var proveedor = IdProveedor;

                        var proveedorProducto =
                            new OpEquiposAbEficiencia().FitrarPorCondicionProvvedorProd(
                                me =>
                                me.Cve_Producto == modelo &&
                                me.Id_Proveedor == proveedor);

                        var capacidad =
                            LstCapacidadesAltaEficiencia.FirstOrDefault(
                                me => me.Cve_Modelo == int.Parse(cboModelo.SelectedValue));

                        if (proveedorProducto != null)
                        {
                            var precioSinIva = Convert.ToDecimal(proveedorProducto.Mt_Precio_Unitario) / (1 + Datos.ValorIva);
                            lblPrecioDistribuidorEdicion.Text = precioSinIva.ToString("0.00");

                            if (EquipoBajaSelecionado.DetalleTecnologia.CveEsquema != 1)
                                lblNo_Capacidad.Text = capacidad.CapacidadAquipo.ToString();
                            else
                                lblNo_Capacidad.Text = capacidad.Dx_Sistema;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var editedItem = (sender as RadNumericTextBox).NamingContainer as GridEditableItem;
                var txtCantidad = (RadNumericTextBox)editedItem.FindControl("txtCantidad");
                var txtPrecioUnitario = (RadNumericTextBox)editedItem.FindControl("txtPrecio_Unitario");
                var lblImporteTotalSinIvaEdicion = (Label)editedItem.FindControl("lblImporte_Total_Sin_IVAEdicion");
                var totalEquipoAE = EquipoBajaSelecionado.DetalleTecnologia.DxEquipoAlta.ToUpper();


                //FALTA VALIDACION DE CANTIDAD DE ACUERDO A  TECNOLOGIA
                if (totalEquipoAE == "MUCHOS")
                {
                    lblImporteTotalSinIvaEdicion.Text =
                        (Valor(txtCantidad.Text) * Valor(txtPrecioUnitario.Text)).ToString();
                }
                else
                {
                    var esNumero = false;
                    int totalEAEnumero = 0;

                    esNumero = int.TryParse(totalEquipoAE, out totalEAEnumero);


                    if (esNumero)
                    {
                        if (int.Parse(totalEquipoAE) > totalEAEnumero)
                        {
                            txtCantidad.Text = "";
                            rwmVentana.RadAlert(
                                "No puede agregar mas de " + totalEAEnumero.ToString() +
                                " equipo(s) de alta eficiencia para la tecnologia actual.", 300, 150,
                                "Equipo de Alta Eficiencia", null);

                            txtCantidad.Focus();
                        }
                    }
                    else
                    {
                        rwmVentana.RadAlert("Ocurrio un error en el limite de equipos de alta eficiencia", 300, 150,
                                            "Equipo de Alta Eficiencia", null);
                    }

                }



            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }

        }

        protected void txtPrecio_Unitario_TextChanged(object sender, EventArgs e)
        {

            try
            {
                var editedItem = (sender as RadNumericTextBox).NamingContainer as GridEditableItem;
                var txtCantidad = (RadNumericTextBox)editedItem.FindControl("txtCantidad");
                var txtPrecioUnitario = (RadNumericTextBox)editedItem.FindControl("txtPrecio_Unitario");
                var lblImporteTotalSinIvaEdicion = (Label)editedItem.FindControl("lblImporte_Total_Sin_IVAEdicion");
                var txtPrecioDistribuidor =
                    string.IsNullOrEmpty(((Label)editedItem.FindControl("lblPrecio_DistribuidorEdicion")).Text)
                        ? 0.0M
                        : Convert.ToDecimal(((Label)editedItem.FindControl("lblPrecio_DistribuidorEdicion")).Text);
                var precioUnitario = string.IsNullOrEmpty(txtPrecioUnitario.Text)
                                         ? 0.0M
                                         : Convert.ToDecimal(txtPrecioUnitario.Text);

                //VALIDACION PRECIO UNITARIO NO DEBE SER MAYOR AL PRECIO DEL DISTRIBUIDOR
                if (precioUnitario <= txtPrecioDistribuidor && precioUnitario > 0.0M)
                {
                    lblImporteTotalSinIvaEdicion.Text =
                        (Valor(txtCantidad.Text) * Valor(txtPrecioUnitario.Text)).ToString(
                            CultureInfo.InvariantCulture);
                }
                else
                {
                    rwmVentana.RadAlert(
                        "El Precio unitario debe ser mayor a cero o menor o igual al precio del distribuidor.", 300,
                        150, "Equipo de Alta Eficiencia", null);
                    txtPrecioUnitario.Text = "";
                    txtPrecioUnitario.Focus();
                }

            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrio un error: " + ex.Message, 300, 150, "Equipo de Alta Eficiencia", null);
            }

        }

        protected void txtGasto_Instalacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var editedItem = (sender as RadNumericTextBox).NamingContainer as GridEditableItem;
                var txtGastoInstalacion = (RadNumericTextBox)editedItem.FindControl("txtGasto_Instalacion");
                var txtPrecioUnitario = (RadNumericTextBox)editedItem.FindControl("txtPrecio_Unitario");
                var txtCantidad = (RadNumericTextBox)editedItem.FindControl("txtCantidad");

                var precioUnitario = string.IsNullOrEmpty(txtPrecioUnitario.Text)
                                         ? 0.0M
                                         : Convert.ToDecimal(txtPrecioUnitario.Text);

                var cantidad = string.IsNullOrEmpty(txtCantidad.Text)
                                         ? 0.0M
                                         : Convert.ToDecimal(txtCantidad.Text);

                var gasto = string.IsNullOrEmpty(txtGastoInstalacion.Text)
                                ? 0.0M
                                : Convert.ToDecimal(txtGastoInstalacion.Text);

                var porcentage = Datos.PorcentageMaximoInstalacion * 100.0M;

                if (!ValidaGastosInstalcion(gasto, (precioUnitario * cantidad)))
                {
                    rwmVentana.RadAlert(
                        "Los Gastos de Instalación no pueden ser mayores al " + porcentage +
                        "% del Precio Unitario.",
                        300, 150, "Equipo de Alta Eficiencia", null);
                    txtGastoInstalacion.Text = "";
                    txtGastoInstalacion.Focus();
                }

            }
            catch (Exception)
            {
                rwmVentana.RadAlert("No se pudo validar los Gastos de Instalación", 300, 150,
                                    "Equipo de Alta Eficiencia", null);
            }
        }

        protected bool ValidaTopeCapacidad(decimal capacidadNueva, decimal capacidadAnterior, bool esCambio, int grupo)
        {
            if (EquipoBajaSelecionado.DetalleTecnologia.DxNombreGeneral.Contains("SUBESTACION"))
                return true;

            if (EquipoBajaSelecionado.DetalleTecnologia.DxNombreGeneral.Contains("BANCO"))
                return true;

            TotalCapacidadEquiposAlta = 0.0M;

            foreach (var capacidadTotal in LstAltaEficiencia.FindAll(p => p.Cve_Grupo == grupo).Select(equipoAltaEficiencia => equipoAltaEficiencia.No_Capacidad * equipoAltaEficiencia.Cantidad))
            {
                TotalCapacidadEquiposAlta = TotalCapacidadEquiposAlta + capacidadTotal;
            }

            if (esCambio)
            {
                var totalCapacidad = (TotalCapacidadEquiposAlta - capacidadAnterior) + capacidadNueva;

                if (totalCapacidad > TopeMaximoCapacidad)
                    return false;
            }
            else
            {
                if ((TotalCapacidadEquiposAlta + capacidadNueva) > TopeMaximoCapacidad)
                    return false;
            }

            return true;
        }

        protected decimal GetCapacidadAcumulada()
        {
            decimal val = 0;

            var equipos = (List<EquipoAltaEficiencia>)ViewState["LstAltaEficiencia"];

            foreach (var equipo in equipos.FindAll(p => p.Cve_Grupo == EquipoBajaSelecionado.Cve_Grupo))
            {
                val += equipo.Cantidad * equipo.No_Capacidad;
            }

            return val;
        }

        protected bool ValidaGastosInstalcion(decimal gastosInstalcion, decimal precioUnitario)
        {
            var maximoGastosInsalacion = precioUnitario * Datos.PorcentageMaximoInstalacion;

            if (gastosInstalcion > maximoGastosInsalacion)
                return false;

            return true;
        }


        #endregion

        #region Logica de Equipos de Alta Eficiencia

        private bool ValidaAltaEquipo(CompDetalleTecnologia detalleTecnologia, int cveGrupo, int cantidadEquipos)
        {

            bool realizaInsercion = false;

            if (detalleTecnologia.DxEquipoAlta.ToUpper() == "MUCHOS")
                realizaInsercion = true;
            else
            {
                var totalEquiposAlta = 0;
                bool esnumero = int.TryParse(detalleTecnologia.DxEquipoAlta, out totalEquiposAlta);

                if (esnumero)
                {
                    var lstEquiposAltaGrupo = LstAltaEficiencia.FindAll(me => me.Cve_Grupo == cveGrupo);

                    if ((lstEquiposAltaGrupo.Sum(me => me.Cantidad) + cantidadEquipos) <= totalEquiposAlta)
                        realizaInsercion = true;
                }

            }

            return realizaInsercion;
        }

        protected void CboGrupos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (CboGrupos.SelectedIndex != 0)
            {
                EquipoBajaSelecionado =
                    LstBajaEficiencia.First(me => me.Cve_Grupo == int.Parse(CboGrupos.SelectedValue));

                var lstEquiposAltaGrupo =
                    LstAltaEficiencia.FindAll(me => me.Cve_Grupo == int.Parse(CboGrupos.SelectedValue));

                if (lstEquiposAltaGrupo.Count > 0)
                {
                    rgEquiposAlta.DataSource = lstEquiposAltaGrupo;
                    rgEquiposAlta.DataBind();
                }

                else
                {
                    rgEquiposAlta.DataSource = new List<EquipoAltaEficiencia>();
                    rgEquiposAlta.DataBind();
                }

                fSEA.Visible = true;
            }
        }

        #endregion

        #region Crear Cliente y Credito

        public bool CrearClienteONegocio(int idProveedor, int idSucursal, short idNegocio, CLI_Cliente infoCliente = null)
        {
            IdProveedor = idProveedor;
            IdSucursal = idSucursal;
            try
            {
                if (NumeroCredito == null)
                {
                    var user = (US_USUARIOModel) Session["UserInfo"];

                    var newdatosPyMe = (K_DATOS_PYME) Session["NewdatosPyMe"];

                    var negocio = new CLI_Negocio
                        {
                            Id_Proveedor = IdProveedor,
                            Id_Branch = IdSucursal,
                            Nombre_Comercial = Pyme.DxNombreComercial,
                            Ventas_Mes = newdatosPyMe.Prom_Vtas_Mensuales,
                            Gastos_Mes = newdatosPyMe.Tot_Gastos_Mensuales,
                            Numero_Empleados = newdatosPyMe.No_Empleados,
                            Cve_Tipo_Industria = Pyme.CveTipoIndustria,
                            Cve_Sector = (byte?) newdatosPyMe.Cve_Sector_Economico,
                            Fecha_Adicion = DateTime.Now,
                            Estatus = 1,
                            AdicionadoPor = user.Nombre_Usuario,
                        };

                    var cliente = new CLI_Cliente
                        {
                            Id_Proveedor = IdProveedor,
                            Id_Branch = IdSucursal,
                            RFC = Pyme.RFC.ToUpper(),
                            Fecha_Adicion = DateTime.Now,
                            Estatus = 1,
                            AdicionadoPor = user.Nombre_Usuario,
                            CLI_Negocio = new List<CLI_Negocio> {negocio}
                        };
                    if (infoCliente != null)
                    {
                        cliente.Cve_Tipo_Sociedad = infoCliente.Cve_Tipo_Sociedad;
                        if (infoCliente.Cve_Tipo_Sociedad == 1)
                        {
                            cliente.Nombre = infoCliente.Nombre.ToUpper();
                            cliente.Ap_Paterno = infoCliente.Ap_Paterno.ToUpper();
                            cliente.Ap_Materno = infoCliente.Ap_Materno.ToUpper();
                            cliente.Fec_Nacimiento = infoCliente.Fec_Nacimiento;
                        }
                        else if (infoCliente.Cve_Tipo_Sociedad == 2)
                        {
                            cliente.Razon_Social = infoCliente.Razon_Social.ToUpper();
                            cliente.Fec_Nacimiento = infoCliente.Fec_Nacimiento;
                        }
                        negocio.DIR_Direcciones = infoCliente.CLI_Negocio.First().DIR_Direcciones;
                        foreach (var direccion in negocio.DIR_Direcciones)
                        {
                            direccion.Fecha_Adicion = DateTime.Now;
                            direccion.Estatus = 1;
                            direccion.AdicionadoPor = user.Nombre_Usuario;
                        }
                    }

                    var clienteExistente =
                        new OpEquiposAbEficiencia().BuscaClientePorCondicion(
                            me =>
                            me.RFC == Pyme.RFC.ToUpper() && me.Id_Proveedor == infoCliente.Id_Proveedor &&
                            me.Id_Branch == infoCliente.Id_Branch);

                    if (clienteExistente == null)
                    {
                        cliente = SolicitudCreditoAcciones.InsertaCliente(cliente);
                        if (cliente != null)
                        {
                            Session["IdCliente"] = cliente.IdCliente;
                            IdNegocio = cliente.CLI_Negocio.First().IdNegocio;
                        }
                    }
                    else
                    {
                        if (idNegocio != 0)
                        {
                            IdNegocio = idNegocio;
                        }
                        else
                        {
                            var idCliente = clienteExistente.IdCliente;
                            cliente.IdCliente = idCliente;
                            negocio.IdCliente = idCliente;
                            foreach (var direccion in negocio.DIR_Direcciones)
                            {
                                direccion.IdCliente = idCliente;
                            }
                            negocio = SolicitudCreditoAcciones.InsertaNegocio(negocio);
                            if (negocio != null)
                            {
                                Session["IdCliente"] = cliente.IdCliente;
                                IdNegocio = negocio.IdNegocio;
                            }
                        }
                    }
                }
                else
                {
                    var credito = SolicitudCreditoAcciones.ObtenCredito(NumeroCredito);

                    if (credito != null)
                    {
                        IdNegocio = (short) credito.IdNegocio;
                        Session["IdCliente"] = credito.IdCliente;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool InsertaCredito()
        {
            try
            {
                
                var programa = new OpEquiposAbEficiencia().ObtenDatosPrograma(Global.PROGRAM);
                var user = (US_USUARIOModel)Session["UserInfo"];

                var noCredito = NumeroCredito ?? GetNumeroCredito();
                var periodoPago = IsSE ? 1 : Datos.PeriodoPago;

                double deTemp = 0;
                for (int i = 0; i < AmortizacionDetalle.Rows.Count; i++)
                {
                    deTemp = deTemp + double.Parse(AmortizacionDetalle.Rows[i]["Mt_Pago"].ToString());
                }
                var montoTotalPagar = Convert.ToDecimal(deTemp);
                var gastoIntalacion = Presupuesto.First(p => p.IdPresupuesto == 7).Resultado +
                                      (Presupuesto.First(p => p.IdPresupuesto == 7).Resultado*Datos.ValorIva);

                RPU = Session["ValidRPU"].ToString();
                var credito = new CRE_Credito
                {
                    No_Credito = noCredito,
                    IdCliente = int.Parse(Session["IdCliente"].ToString()),
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdNegocio = IdNegocio,
                    RPU = RPU,
                    Id_Trama = IdTrama,
                    No_Ahorro_Economico = Math.Round(Datos.AhorroEconomico,2),
                    Monto_Solicitado = Math.Round(Presupuesto.First(p => p.IdPresupuesto == 8).Resultado, 2),
                    Monto_Total_Pagar = Math.Round(montoTotalPagar, 2),
                    Capacidad_Pago = Math.Round(Datos.CapacidadPago,2),
                    No_Plazo_Pago = Convert.ToByte(Datos.Plazo),
                    Cve_Periodo_Pago = Convert.ToByte(periodoPago),
                    Tasa_Interes = programa.Pct_Tasa_Interes,
                    Tasa_Fija = programa.Pct_Tasa_Fija,
                    CAT = periodoPago == 1
                        ? (double) programa.Pct_CAT_Factura_Mensual
                        : (double) programa.Pct_CAT_Factura_Bimestral,
                    Tasa_IVA = Convert.ToInt32(Datos.ValorIva*100),
                    Adquisicion_Sust = 0,
                    Fecha_Pendiente = DateTime.Now.Date,
                    Fecha_Ultmod = DateTime.Now.Date,
                    Usr_Ultmod = user.Nombre_Usuario,
                    Tasa_IVA_Intereses = Convert.ToInt32(Datos.ValorIva*100),
                    //No_consumo_promedio = Convert.ToDouble(Datos.ConsumoPromedio),
                    Tipo_Usuario = user.Tipo_Usuario,
                    ID_Prog_Proy = programa.ID_Prog_Proy,
                    Cve_Estatus_Credito = (int) CreditStatus.PENDIENTE,
                    Gastos_Instalacion = Math.Round(gastoIntalacion, 2),
                    No_Ahorro_Consumo = Datos.ConsumoPromedio - Datos.KwHConAhorro,
                    No_Ahorro_Demanda = Datos.DemandaMaxima - Datos.KwConAhorro,
                    Consumo_Promedio = Datos.ConsumoPromedio,
                    Demanda_Maxima = Datos.DemandaMaxima,
                    Factor_Potencia = Datos.FactorPotencia,
                    KVARs = Datos.KvAR
                };

                CRE_Credito nuevoCredito = null;

                if (NumeroCredito != null)
                {
                    var creditoActual = SolicitudCreditoAcciones.ObtenCredito(NumeroCredito);
                    var borra = new OpEquiposAbEficiencia().EliminaCredito(creditoActual);

                    if (borra)
                        nuevoCredito = new OpEquiposAbEficiencia().InsertaCredito(credito);
                    else
                        return false;
                }
                else
                {
                    if (! new OpEquiposAbEficiencia().VerificaRpuSolictudActiva(credito.RPU))
                    {
                        nuevoCredito = new OpEquiposAbEficiencia().InsertaCredito(credito);
                    }
                    else
                    {
                        return false;
                    }
                }    

                if (nuevoCredito != null)
                {
                    var insertaEquiposAlta =
                        new OpEquiposAbEficiencia().InsertaEquiposAlta(LstAltaEficiencia, noCredito, Datos.ValorIva);

                    

                    if (insertaEquiposAlta)
                    {
                        var insertaEquiposBaja = new OpEquiposAbEficiencia().InsertaEquiposBaja(LstBajaEficiencia,
                                                                                            noCredito);
                        if (!insertaEquiposBaja)
                        {
                            new OpEquiposAbEficiencia().EliminaProductosBaja(noCredito);
                            new OpEquiposAbEficiencia().EliminaProductosAlta(noCredito);
                            var borra = new OpEquiposAbEficiencia().EliminaCredito(nuevoCredito);

                            return false;
                        }
                        else
                        {
                            var equiposBaja = new OpEquiposAbEficiencia().InsertaCreEquiposBaja(LstBajaEficiencia,
                                                                                                noCredito);
                            var datos = InsertaDatosComplementarios(nuevoCredito.No_Credito);
                        }                        
                    }
                    else
                    {
                        var borra = new OpEquiposAbEficiencia().EliminaCredito(nuevoCredito);
                        return false;
                    }
                   
                    NumeroCredito = nuevoCredito.No_Credito;
                    InicializaUserControl(NumeroCredito, txtDX_NOMBRE_COMERCIAL.Text);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            RadBtnEditaPresupuesto.Visible = true;
            return true;
        }

        protected void CargaEquiposCredito()
        {
            var noCredito = NumeroCredito;
            LstAltaEficiencia = new OpEquiposAbEficiencia().ObtenEquiposAltaEficienciaCredito(noCredito);

            if (LstAltaEficiencia.Count > 0)
            {
                InicializaParametrosGlobales();

                Label2.Visible = cboTecnologias.Visible = lblSistema.Visible = cboTarifaDestino.Visible = false;
                btnAgregar.Visible = false;

                var iva = Convert.ToDecimal(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 3 && p.IDSECCION == 1).VALOR);
                var porcentageInstalacion = Convert.ToDecimal(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 16).VALOR);

                Datos.ValorIva = iva/100.0M;

                var datosPyme = SolicitudCreditoAcciones.BuscaDatosPyme(Session["ValidRPU"].ToString());

                if (datosPyme != null)
                {
                    txtDX_NOMBRE_COMERCIAL.Text = RazonSocial.ToUpper();
                    txtDX_CP_FISC.Text = datosPyme.Codigo_Postal;

                    var estadoDomicilio = CatalogosSolicitud.ObtenEstadoXId((int) datosPyme.Cve_Estado).Dx_Nombre_Estado;
                    var municipioDomicilio =
                        CatalogosSolicitud.ObtenMunicipioXId((int) datosPyme.Cve_Deleg_Municipio).Dx_Deleg_Municipio;
                    var tipoIndustria =
                        CatalogosSolicitud.ObtenTipoIndustria((int) datosPyme.Cve_Tipo_Industria).DESCRIPCION_SCIAN;

                    txtDX_DOMICILIO_FISC.Text = estadoDomicilio + ", " + municipioDomicilio;
                    txtDX_TIPO_INDUSTRIA.Text = tipoIndustria;
                }

                LstBajaEficiencia = new OpEquiposAbEficiencia().ObtenEquiposBajaEficienciaCredito(noCredito);
                LstAltaEficiencia = new OpEquiposAbEficiencia().ObtenEquiposAltaEficienciaCredito(noCredito);

                rgEquiposBaja.DataSource = LstBajaEficiencia;
                rgEquiposBaja.DataBind();

                legEquiposAlta.InnerText = "Equipos de Alta Eficiencia - " + noCredito;
                rgEquiposAlta.DataSource = LstAltaEficiencia;
                rgEquiposAlta.DataBind();
                fSEA.Visible = true;

                //ResumenPresupuesto();
                Presupuesto = new ValidacionTarifas().ObtenResumenPresupuestoConsulta(LstAltaEficiencia, LstBajaEficiencia,
                                                                              Datos.ValorIva, NumeroCredito);

                Datos.MontoTotalPagar = Math.Round(Presupuesto.First(p => p.IdPresupuesto == 8).Resultado, 4);
                rgPresupuesto.DataSource = Presupuesto.OrderBy(me => me.IdOrden);
                rgPresupuesto.DataBind();

                EsCaptura = false;
                RadBtnEditaPresupuesto.Visible = !new OpEquiposAbEficiencia().EsReasignacion(NumeroCredito);

                //add by @l3x
                rgEquiposBaja.Enabled = !new OpEquiposAbEficiencia().EsReactivacion(NumeroCredito);
                rgEquiposAlta.Enabled = !new OpEquiposAbEficiencia().EsReactivacion(NumeroCredito);
                btnAgrupar.Enabled = !new OpEquiposAbEficiencia().EsReactivacion(NumeroCredito);
                btnDesagrupar.Enabled = !new OpEquiposAbEficiencia().EsReactivacion(NumeroCredito);
                RadBtnEditaPresupuesto.Enabled = !new OpEquiposAbEficiencia().EsReactivacion(NumeroCredito);
            }
            else
            {
                ParseoTrama();
                CargaInformacionGeneral();
                CargaTecnologias();
                RealizaTarifa();
                EsCaptura = true;
                RadBtnEditaPresupuesto.Visible = false;
            }
        }

        public string GetNumeroCredito()
        {
            var parseo = (ParseoTrama)Session["PARSEO_TRAMA"];
            var RGN_CFE =
                parseo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 145).Dato;
            var user = (US_USUARIOModel)Session["UserInfo"];
            var tipoZona = new OpEquiposAbEficiencia().ObtenCN(IdTrama);

            var noCredito = "PAEEEM" + RGN_CFE + tipoZona +
                            string.Format("{0:00000}", Convert.ToInt32(LsUtility.GetNumberSequence("CREDITO")));

            return noCredito;
        }

        protected bool InsertaDatosComplementarios(string NoCredito)
        {
            try
            {
                var user = (US_USUARIOModel)Session["UserInfo"];
                var parseo = (ParseoTrama)Session["PARSEO_TRAMA"];
                var idTarifa = new OpEquiposAbEficiencia().ObtenTarifa(TipoTarifa).Cve_Tarifa;

                var capacidadPago = new OpEquiposAbEficiencia().InsertaCapacidadPago(CapacidadPago, user.Nombre_Usuario,
                                                                                     NoCredito);

                var psr = new OpEquiposAbEficiencia().InsertaPSR(Psr, user.Nombre_Usuario, NoCredito);

                var facturacionActual = new OpEquiposAbEficiencia().InsertaFacturacion(FactSinAhorro, user.Nombre_Usuario,
                                                                                       NoCredito, idTarifa, 1);

                var facturacionFutura = new OpEquiposAbEficiencia().InsertaFacturacion(FactConAhorro, user.Nombre_Usuario,
                                                                                       NoCredito, idTarifa, 2);

                var insertaConsumos =
                    new OpEquiposAbEficiencia().InsertaHistoricoConsumo(parseo.ComplexParseo.HistorialDetconsumos, NoCredito,
                                                                        user.Nombre_Usuario);

                var insertaGrupos = new OpEquiposAbEficiencia().InsertaGruposTecnologia(LstGrupos, NoCredito,
                                                                                        user.Nombre_Usuario);

                var creditoCosto = new OpEquiposAbEficiencia().InsertaCreditoCosto(NoCredito,
                                                                                   Presupuesto.First(
                                                                                       p => p.IdPresupuesto == 4).Resultado);

                var creditoDescuento = new OpEquiposAbEficiencia().InsertaCreditoDescuento(NoCredito,
                                                                                           Presupuesto.First(
                                                                                               p => p.IdPresupuesto == 5)
                                                                                                      .Resultado);
                var creditoAmortizacion = new OpEquiposAbEficiencia().InsertaAmortizacionesCredito(AmortizacionDetalle, NoCredito);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool ExistenEquiposAlta(string noCredito)
        {
            var lstEquiposAlta = new OpEquiposAbEficiencia().ObtenEquiposAltaEficienciaCredito(noCredito);

            if (lstEquiposAlta.Count > 0)
                return true;
            else
                return false;
        }
        
        public bool EditaPresupuestoInversion()
        {
            try
            {
                CRE_Credito nuevoCredito = null;
                var creditoActual = SolicitudCreditoAcciones.ObtenCredito(NumeroCredito);
                var programa = new OpEquiposAbEficiencia().ObtenDatosPrograma(Global.PROGRAM);
                var user = (US_USUARIOModel)Session["UserInfo"];

                var noCredito = NumeroCredito ?? GetNumeroCredito();
                var periodoPago = IsSE ? 1 : Datos.PeriodoPago;

                double deTemp = 0;
                for (int i = 0; i < AmortizacionDetalle.Rows.Count; i++)
                {
                    deTemp = deTemp + double.Parse(AmortizacionDetalle.Rows[i]["Mt_Pago"].ToString());
                }
                var montoTotalPagar = Convert.ToDecimal(deTemp);
                var gastoIntalacion = Presupuesto.First(p => p.IdPresupuesto == 7).Resultado +
                                      (Presupuesto.First(p => p.IdPresupuesto == 7).Resultado * Datos.ValorIva);


                RPU = Session["ValidRPU"].ToString();
                //var credito = new CRE_Credito
                //{
                //    No_Credito = noCredito,
                //    IdCliente = int.Parse(Session["IdCliente"].ToString()),
                //    Id_Proveedor = creditoActual.Id_Proveedor,
                //    Id_Branch = creditoActual.Id_Branch,
                //    IdNegocio = creditoActual.IdNegocio,
                //    RPU = RPU,
                //    Id_Trama = IdTrama,
                //    No_Ahorro_Energetico = Convert.ToDouble(Datos.ConsumoPromedio - Datos.KwHConAhorro),
                //    No_Ahorro_Economico = Math.Round(Datos.AhorroEconomico, 2),
                //    Monto_Solicitado = Math.Round(Presupuesto.First(p => p.IdPresupuesto == 8).Resultado, 2),
                //    Monto_Total_Pagar = Math.Round(montoTotalPagar, 2),
                //    Capacidad_Pago = Math.Round(Datos.CapacidadPago, 2),
                //    No_Plazo_Pago = Convert.ToByte(Datos.Plazo),
                //    Cve_Periodo_Pago = Convert.ToByte(periodoPago),
                //    Tasa_Interes = programa.Pct_Tasa_Interes,
                //    Tasa_Fija = programa.Pct_Tasa_Fija,
                //    CAT = periodoPago == 1
                //        ? (double)programa.Pct_CAT_Factura_Mensual
                //        : (double)programa.Pct_CAT_Factura_Bimestral,
                //    Tasa_IVA = Convert.ToInt32(Datos.ValorIva * 100),
                //    Adquisicion_Sust = 0,
                //    Fecha_Pendiente = DateTime.Now.Date,
                //    Fecha_Ultmod = DateTime.Now.Date,
                //    Usr_Ultmod = user.Nombre_Usuario,
                //    Tasa_IVA_Intereses = Convert.ToInt32(Datos.ValorIva * 100),
                //    No_consumo_promedio = Convert.ToDouble(Datos.ConsumoPromedio),
                //    Tipo_Usuario = user.Tipo_Usuario,
                //    ID_Prog_Proy = programa.ID_Prog_Proy,
                //    Cve_Estatus_Credito = (int)CreditStatus.PENDIENTE,
                //    Gastos_Instalacion = Math.Round(gastoIntalacion, 2),
                //    No_Ahorro_Consumo = Datos.ConsumoPromedio - Datos.KwHConAhorro,
                //    No_Ahorro_Demanda = Datos.DemandaMaxima - Datos.KwConAhorro
                //};
                creditoActual.No_Credito = noCredito;
                creditoActual.IdCliente = int.Parse(Session["IdCliente"].ToString());
                creditoActual.No_Ahorro_Energetico = Convert.ToDouble(Datos.ConsumoPromedio - Datos.KwHConAhorro);
                creditoActual.No_Ahorro_Economico = Math.Round(Datos.AhorroEconomico, 2);
                creditoActual.Monto_Solicitado = Math.Round(Presupuesto.First(p => p.IdPresupuesto == 8).Resultado, 2);
                creditoActual.Monto_Total_Pagar = Math.Round(montoTotalPagar, 2);
                creditoActual.Capacidad_Pago = Math.Round(Datos.CapacidadPago, 2);
                creditoActual.No_Plazo_Pago = Convert.ToByte(Datos.Plazo);
                creditoActual.Cve_Periodo_Pago = Convert.ToByte(periodoPago);
                creditoActual.Tasa_Interes = programa.Pct_Tasa_Interes;
                creditoActual.Tasa_Fija = programa.Pct_Tasa_Fija;
                creditoActual.CAT = periodoPago == 1
                    ? (double)programa.Pct_CAT_Factura_Mensual
                    : (double)programa.Pct_CAT_Factura_Bimestral;
                creditoActual.Tasa_IVA = Convert.ToInt32(Datos.ValorIva * 100);
                creditoActual.Adquisicion_Sust = 0;
                creditoActual.Fecha_Ultmod = DateTime.Now.Date;
                creditoActual.Usr_Ultmod = user.Nombre_Usuario;
                creditoActual.Tasa_IVA_Intereses = Convert.ToInt32(Datos.ValorIva * 100);
                creditoActual.No_consumo_promedio = Convert.ToDouble(Datos.ConsumoPromedio);
                creditoActual.Tipo_Usuario = user.Tipo_Usuario;
                creditoActual.ID_Prog_Proy = programa.ID_Prog_Proy;
                creditoActual.Cve_Estatus_Credito = (int)CreditStatus.PENDIENTE;
                creditoActual.Gastos_Instalacion = Math.Round(gastoIntalacion, 2);
                creditoActual.No_Ahorro_Consumo = Datos.ConsumoPromedio - Datos.KwHConAhorro;
                creditoActual.No_Ahorro_Demanda = Datos.DemandaMaxima - Datos.KwConAhorro;

                new OpEquiposAbEficiencia().EliminaPresupuestoInversion(NumeroCredito);
                //var borraCredito = new OpEquiposAbEficiencia().EliminaCredito(creditoActual);
                var actualizaCredito = OpEquiposAbEficiencia.ActualizaCredito(creditoActual);

                //if (actualizaCredito)
                //    nuevoCredito = new OpEquiposAbEficiencia().InsertaCredito(credito);
                //else
                //    return false;
                

                //if (nuevoCredito != null)
                if(actualizaCredito)
                {
                    var insertaEquiposAlta =
                        new OpEquiposAbEficiencia().InsertaEquiposAlta(LstAltaEficiencia, noCredito, Datos.ValorIva);



                    if (insertaEquiposAlta)
                    {
                        var insertaEquiposBaja = new OpEquiposAbEficiencia().InsertaEquiposBaja(LstBajaEficiencia,
                                                                                            noCredito);
                        if (!insertaEquiposBaja)
                        {
                            new OpEquiposAbEficiencia().EliminaProductosBaja(noCredito);
                            new OpEquiposAbEficiencia().EliminaProductosAlta(noCredito);
                            //var borra = new OpEquiposAbEficiencia().EliminaCredito(nuevoCredito);

                            return false;
                        }
                        else
                        {
                            var equiposBaja = new OpEquiposAbEficiencia().InsertaCreEquiposBaja(LstBajaEficiencia,
                                                                                                noCredito);
                            var datos = InsertaDatosComplementarios(noCredito);
                        }
                    }
                    //else
                    //{
                    //    var borra = new OpEquiposAbEficiencia().EliminaCredito(nuevoCredito);
                    //    return false;
                    //}

                    //NumeroCredito = nuevoCredito.No_Credito;
                    EsCaptura = EsEdicion = false;
                    InicializaUserControl(NumeroCredito, txtDX_NOMBRE_COMERCIAL.Text);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        
        #endregion

        #region CalculosTarifa

        /*REALIZAR LA CARGA DE LA FACTURACION SIN AHORRO DE ACUERDO A LA TARIFA DE LA TRAMA*/
        protected void RealizaTarifa()
        {
            try
            {
                var parseo = (ParseoTrama)Session["PARSEO_TRAMA"];

                switch (TipoTarifa)
                {
                    case "02":
                        _t02 = new AlgoritmoTarifa02(parseo.ComplexParseo);
                        InfoTarifa = _t02.T02;
                        FactSinAhorro = _t02.T02.FactSinAhorro;
                        break;
                    case "03":
                        _t03 = new AlgoritmoTarifa03(parseo.ComplexParseo);
                        InfoTarifa = _t03.T03;
                        FactSinAhorro = _t03.T03.FactSinAhorro;
                        break;
                    case "HM":
                        _tHm = new AlgoritmoTarifaHM(parseo.ComplexParseo);
                        InfoTarifa = _tHm.Thm;
                        FactSinAhorro = _tHm.Thm.FactSinAhorro;
                        Datos.KvAR = _tHm.Kvar;
                        Datos.AplicaPeridosBC = _tHm.periodosValidosBC;
                        Datos.AplicaFPBc = _tHm.maximoFactoresPotenciaBC;
                        break;
                    case "OM":
                        _tOm = new AlgoritmoTarifaOM(parseo.ComplexParseo);
                        InfoTarifa = _tOm.Tom;
                        FactSinAhorro = _tOm.Tom.FactSinAhorro;
                        Datos.KvAR = _tOm.Kvar;
                        Datos.AplicaPeridosBC = _tOm.periodosValidosBC;
                        Datos.AplicaFPBc = _tOm.maximoFactoresPotenciaBC;
                        break;
                    default:
                        InfoTarifa = null;
                        break;
                }

                Datos.ConsumoPromedio = parseo.ComplexParseo.InfoConsumo.Detalle.Promedio;
                Datos.DemandaMaxima = parseo.ComplexParseo.InfoDemanda.Detalle.DemandaMax;


                if (InfoTarifa != null)
                {
                    Datos.MontoMaximoFacturar20 = FactSinAhorro.MontoMaxFacturar;
                    Datos.FactorPotencia = Math.Round(
                        parseo.ComplexParseo.HistorialDetconsumos.Where(p => p.Id > 0).Sum(p => p.FactorPotencia)/
                        InfoTarifa.Periodos, 4);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al cargar los datos de la Tarifa " + TipoTarifa, ex);
                //rwmVentana.RadAlert("No se Encontró el Algoritmo para la Traifa del Cliente", 300, 150, "Alta y Baja de Equipos", null);
            }
        }

        protected void CalculaTotalAhorros()
        {
            try
            {
                var totalAhorroKw = 0.0M;
                var totalAhorroKwH = 0.0M;

                var cveGrupo = 0;

                foreach (
                    var equipoBajaEficiencia in
                        LstBajaEficiencia.FindAll(me => me.DetalleTecnologia.NumeroGrupos != 1)
                                            .OrderBy(me => me.Cve_Grupo))
                {
                    if (cveGrupo != equipoBajaEficiencia.Cve_Grupo)
                    {
                        cveGrupo = equipoBajaEficiencia.Cve_Grupo;
                        var ahorroKwGrupo = 0.0M;
                        var ahorroKwHGrupo = 0.0M;

                        foreach (
                            var equipoAltaEficiencia in
                                LstAltaEficiencia.FindAll(me => me.Cve_Grupo == equipoBajaEficiencia.Cve_Grupo))
                        {
                            totalAhorroKw = totalAhorroKw + equipoAltaEficiencia.KwAhorro;
                            totalAhorroKwH = totalAhorroKwH + equipoAltaEficiencia.KwhAhorro;

                            ahorroKwGrupo = ahorroKwGrupo + equipoAltaEficiencia.KwAhorro;
                            ahorroKwHGrupo = ahorroKwHGrupo + equipoAltaEficiencia.KwhAhorro;
                        }

                        LstGrupos.Where(p => p.Cve_Grupo == cveGrupo)
                            .Select(p =>
                            {
                                p.AhorroConsumo = ahorroKwHGrupo;
                                p.AhorroDemanda = ahorroKwGrupo;
                                return p;
                            }).ToList();
                    }
                }

                cveGrupo = 0;

                foreach (
                    var equipoBajaEficiencia in
                        LstBajaEficiencia.FindAll(me => me.DetalleTecnologia.NumeroGrupos == 1)
                                            .OrderBy(me => me.Cve_Grupo))
                {
                    if (cveGrupo != equipoBajaEficiencia.Cve_Grupo)
                    {
                        cveGrupo = equipoBajaEficiencia.Cve_Grupo;
                        var ahorroKwGrupo = 0.0M;
                        var ahorroKwHGrupo = 0.0M;

                        var maxEquipoAlta =
                            LstAltaEficiencia.FindAll(me => me.Cve_Grupo == equipoBajaEficiencia.Cve_Grupo)
                                                .Max(me => me.ID);

                        foreach (
                            var equipoAltaEficiencia in
                                LstAltaEficiencia.FindAll(me => me.Cve_Grupo == equipoBajaEficiencia.Cve_Grupo))
                        {

                            if (equipoAltaEficiencia.ID == maxEquipoAlta)
                            {
                                totalAhorroKw = totalAhorroKw + equipoAltaEficiencia.KwAhorro;
                                totalAhorroKwH = totalAhorroKwH + equipoAltaEficiencia.KwhAhorro;

                                ahorroKwGrupo = ahorroKwGrupo + equipoAltaEficiencia.KwAhorro;
                                ahorroKwHGrupo = ahorroKwHGrupo + equipoAltaEficiencia.KwhAhorro;
                            }
                        }

                        LstGrupos.Where(p => p.Cve_Grupo == cveGrupo)
                            .Select(p =>
                            {
                                p.AhorroConsumo = ahorroKwHGrupo;
                                p.AhorroDemanda = ahorroKwGrupo;
                                return p;
                            }).ToList();
                    }
                }

                Datos.TotalAhorrosKw = Math.Round(totalAhorroKw, 4);
                Datos.TotalAhorrosKwH = Math.Round(totalAhorroKwH, 4);

                Datos.KwConAhorro = Math.Round((Datos.DemandaMaxima - Datos.TotalAhorrosKw), 4);
                Datos.KwHConAhorro = Math.Round((Datos.ConsumoPromedio - Datos.TotalAhorrosKwH), 4);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al realizar la suma de los ahorros", ex);
                //rwmVentana.RadAlert("Ocurrió un problema al realizar la suma de los ahorros", 300, 150, "Alta y Baja de Equipos", null);
                //return;
            }

        }

        protected void CalculaValorAmortizacion()
        {
            try
            {
                var parseo = (ParseoTrama)Session["PARSEO_TRAMA"];
                var programa = new OpEquiposAbEficiencia().ObtenDatosPrograma(Global.PROGRAM);
                var periodoConsumoFinal =
                    parseo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 159).Dato;
                var montoSolicitado = Presupuesto.First(p => p.IdPresupuesto == 8).Resultado;
                double cat = 0.0;
                var periodoPago = IsSE ? 1 : Datos.PeriodoPago;
                Datos.Plazo = periodoPago == 1 ? (int)programa.No_Plazo * 12 : (int)programa.No_Plazo * 6;


                cat = periodoPago == 1
                            ? (double)programa.Pct_CAT_Factura_Mensual
                            : (double)programa.Pct_CAT_Factura_Bimestral;

                DataTable CreditAmortizacionDt =
                    K_CREDITOBll.ClassInstance.CalculateCreditAmortizacion(Session["ValidRPU"].ToString(),
                                                                            Convert.ToDouble(montoSolicitado),
                                                                            (double)programa.Pct_Tasa_Fija / 100,
                                                                            Datos.Plazo,
                                                                            periodoPago,
                                                                            (double)programa.Pct_Tasa_Interes / 100,
                                                                            (double)programa.Pct_Tasa_IVA_Intereses /
                                                                            100,
                                                                            cat,
                                                                            periodoConsumoFinal);

                Datos.ValorAmortizacion = Math.Round((Convert.ToDecimal(CreditAmortizacionDt.Rows[0]["Mt_Pago"])), 4);

                AmortizacionDetalle = CreditAmortizacionDt;
                
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo calcular el Valor de la Amortización", ex);
                //rwmVentana.RadAlert("No se pudo calcular el Valor de la Amortización", 300, 150, "Alta y Baja de Equipos", null);
            }

        }

        protected void CalculaFacturacionConAhorro()
        {
            try
            {
                CompFacturacion compFacturacion = null;
                var parseo = (ParseoTrama)Session["PARSEO_TRAMA"];

                var tarifaNueva = !IsSE ? TipoTarifa : cboTarifaDestino.SelectedItem.Text;

                switch (tarifaNueva)
                {
                    case "02":
                        _t02 = new AlgoritmoTarifa02(parseo.ComplexParseo, true);
                        compFacturacion = _t02.CreaConceptos(Datos.KwHConAhorro, Datos.KwConAhorro);
                        FactConAhorro = _t02.Facturacion(compFacturacion, false);
                        break;
                    case "03":
                        _t03 = new AlgoritmoTarifa03(parseo.ComplexParseo, true);
                        compFacturacion = _t03.CreaConceptos(Datos.KwHConAhorro, Datos.KwConAhorro);
                        FactConAhorro = _t03.Facturacion(compFacturacion, false);
                        break;
                    case "HM":
                        compFacturacion = CalculaFacturacionHM(parseo);
                        FactConAhorro = compFacturacion;
                        //_tHm = new AlgoritmoTarifaHM(parseo.ComplexParseo,true);
                        //compFacturacion = _tHm.FacturacionFutura(Datos.KwHConAhorro, Datos.KwConAhorro);
                        //FactConAhorro = compFacturacion;
                        break;
                    case "OM":
                        //_tOm = new AlgoritmoTarifaOM(parseo.ComplexParseo, true);
                        //compFacturacion = _tOm.FacturacionFutura(Datos.KwHConAhorro, Datos.KwConAhorro);
                        compFacturacion = CalculaFacturacionOM(parseo);
                        FactConAhorro = compFacturacion;
                        break;
                }

                Datos.AhorroEconomico = Math.Round((FactSinAhorro.PagoFactBiMen - FactConAhorro.PagoFactBiMen), 4);
                Datos.ProximaFacturacionMensual = Math.Round((FactConAhorro.Total + Datos.ValorAmortizacion), 4);

                RadSubTotalAhorro.Text = FactConAhorro.Subtotal.ToString();
                RadIvaAhorro.Text = FactConAhorro.Iva.ToString();
                RadTotalAhorro.Text = FactConAhorro.Total.ToString();
                RadFacturacionMensual.Text = FactConAhorro.PagoFactBiMen.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un problema al generar la Facturación Futura", ex);
                //rwmVentana.RadAlert("Ocurrió un problema al generar la Facturación Futura", 300, 150, "Alta y Baja de Equipos", null);
                //return;
            }

        }

        protected CompFacturacion CalculaFacturacionOM(ParseoTrama parseo)
        {
            CompFacturacion compFacturacion = null;

            _tOm = new AlgoritmoTarifaOM(parseo.ComplexParseo, true);

            if (IsSE)
            {
                var otrasTecnologias =
                    LstAltaEficiencia.FindAll(me => me.Dx_Tecnologia != "SUBESTACIONES ELECTRICAS").Count;

                CombinaTecnologias = otrasTecnologias > 0;

                compFacturacion = _tOm.FacturacionFuturaCambioTarifa(TipoTarifa, Datos.ConsumoPromedio,
                                                                     Datos.TotalAhorrosKwH, Datos.DemandaMaxima,
                                                                     Datos.TotalAhorrosKw, CombinaTecnologias);
            }
            else if (IsBC)
            {
                var otrasTecnologias =
                    LstAltaEficiencia.FindAll(me => me.Dx_Tecnologia != "BANCO DE CAPACITORES").Count;

                CombinaTecnologias = otrasTecnologias > 0;

                compFacturacion = _tOm.FacturacionFuturaBC(Datos.ConsumoPromedio,
                                         Datos.TotalAhorrosKwH, Datos.DemandaMaxima,
                                         Datos.TotalAhorrosKw, CombinaTecnologias);
            }
            else
            {
                compFacturacion = _tOm.FacturacionFutura(Datos.KwHConAhorro, Datos.KwConAhorro);
            }

            return compFacturacion;
        }

        protected CompFacturacion CalculaFacturacionHM(ParseoTrama parseo)
        {
            CompFacturacion compFacturacion = null;

            _tHm = new AlgoritmoTarifaHM(parseo.ComplexParseo, true);

            if (IsSE)
            {
                var otrasTecnologias =
                    LstAltaEficiencia.FindAll(me => me.Dx_Tecnologia != "SUBESTACIONES ELECTRICAS").Count;

                CombinaTecnologias = otrasTecnologias > 0;

                compFacturacion = _tHm.FacturacionFuturaCambioTarifa(TipoTarifa, Datos.ConsumoPromedio,
                                                                     Datos.TotalAhorrosKwH, Datos.DemandaMaxima,
                                                                     Datos.TotalAhorrosKw, CombinaTecnologias);
            }
            else if (IsBC)
            {
                var otrasTecnologias =
                    LstAltaEficiencia.FindAll(me => me.Dx_Tecnologia != "BANCO DE CAPACITORES").Count;

                CombinaTecnologias = otrasTecnologias > 0;

                compFacturacion = _tHm.FacturacionFuturaBC(Datos.ConsumoPromedio,
                                         Datos.TotalAhorrosKwH, Datos.DemandaMaxima,
                                         Datos.TotalAhorrosKw, CombinaTecnologias);
            }
            else
            {
                compFacturacion = _tHm.FacturacionFutura(Datos.KwHConAhorro, Datos.KwConAhorro);
            }

            return compFacturacion;
        }

        protected void CalculaCapacidadPago()
        {
            try
            {
                var periodoPago = IsSE ? 1 : Datos.PeriodoPago;
                var conceptosCapPago = new CompConceptosCappago
                {
                    Ventas = (decimal)Pyme.PromVtasMensuales,
                    Gastos = (decimal)Pyme.TotGastosMensuales,
                    Ahorro = Datos.AhorroEconomico / Datos.PeriodoPago
                };
                conceptosCapPago.Flujo = (conceptosCapPago.Ventas - conceptosCapPago.Gastos) +
                                         conceptosCapPago.Ahorro;
                Datos.CapacidadPago = new ValidacionTarifas().GetValorCapPago(conceptosCapPago, periodoPago,
                                                                              Datos.ValorAmortizacion);
                conceptosCapPago.Capacidad = Datos.CapacidadPago;

                CapacidadPago = conceptosCapPago;

                RadVentas.Text = CapacidadPago.Ventas.ToString();
                RadGastos.Text = CapacidadPago.Gastos.ToString();
                RadAhorro.Text = CapacidadPago.Ahorro.ToString();
                RadFlujo.Text = CapacidadPago.Flujo.ToString();
                RadCapacidad.Text = CapacidadPago.Capacidad.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo calcular la Capacidad de Pago", ex);
                //rwmVentana.RadAlert("No se pudo calcular la Capacidad de Pago", 300, 150, "Alta y Baja de Equipos", null);
                //return;
            }
        }

        protected void CalculaPsr()
        {
            try
            {
                var periodoPago = IsSE ? 1 : Datos.PeriodoPago;
                var conceptosPsr = new CompConceptosPsr
                {
                    MontoFinanciamento = Datos.MontoTotalPagar,
                    Periodo = periodoPago == 1 ? 12 : 6,
                    AhorroEconomico = Datos.AhorroEconomico
                };

                conceptosPsr.ValorPsr = new ValidacionTarifas().GetValorPsr(conceptosPsr);

                Psr = conceptosPsr;

                RadCapMonto.Text = Psr.MontoFinanciamento.ToString();
                RadCapAhorro.Text = Psr.AhorroEconomico.ToString();
                RadCapPeriodo.Text = Psr.Periodo.ToString();
                RadCapPSR.Text = Psr.ValorPsr.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo calcular la Capacidad de Pago", ex);
                //rwmVentana.RadAlert("No se pudo calcular el Periodo Simple de Recuperación", 300, 150, "Alta y Baja de Equipos", null);
                //return;
            }
        }

        protected void CalculaResultadosValidacion()
        {
            var periodoPago = IsSE ? 1 : Datos.PeriodoPago;
            var resultado = new ValidacionTarifas().GetResultados(Datos.MontoMaximoFacturar20, Psr.ValorPsr,
                                                                  CapacidadPago.Capacidad,
                                                                  (FactConAhorro.Total / periodoPago),
                                                                  Datos.KwHConAhorro,
                                                                  Datos.ProximaFacturacionMensual);

            Resultado = resultado;
        }

        public CompResultado RealizaCalculosSimulador()
        {
            try
            {

                CalculaTotalAhorros();
                CalculaValorAmortizacion();
                CalculaFacturacionConAhorro();
                CalculaCapacidadPago();
                CalculaPsr();
                CalculaResultadosValidacion();
                MuestraResultadosValidacion();
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert(ex.Message, 300, 150,
                                    "Alta y Baja de Equipos", null);
            }

            return Resultado;
        }

        protected void CreaCreditoCliente()
        {
            if (!Resultado.CapacidadPagoValue)
                rwmVentana.RadAlert("Usuario no cumple con la Capacidad de Pago Necesaria", 300, 150,
                                    "Alta y Baja de Equipos", null);
            else if (!Resultado.NuevaFacturaNegativaValue)
                rwmVentana.RadAlert("La Facturación Futura es Negativa", 300, 150, "Alta y Baja de Equipos", null);
            else if (!Resultado.ConsumoNegativoValue)
                rwmVentana.RadAlert("La Facturación Futura presenta Consumo Negativo", 300, 150,
                                    "Alta y Baja de Equipos", null);
            else if (!Resultado.PsrValue)
                rwmVentana.RadAlert("No se cumple el Periodo Simple de Recuperación Requerido", 300, 150,
                                    "Alta y Baja de Equipos", null);
            else if (!Resultado.ValidacionValue)
                rwmVentana.RadAlert("No se cumple la validación del 20%", 300, 150, "Alta y Baja de Equipos", null);
            else
            {
                if (CrearClienteONegocio(IdProveedor, IdSucursal, IdNegocio))
                {
                    var creaCredito = InsertaCredito();
                }
                else
                {
                    rwmVentana.RadAlert("Ocurrió un problema al guardar los datos del cliente", 300, 150,
                                        "Alta y Baja de Equipos", null);
                }
            }
        }

        private EquipoAltaEficiencia CalculaAhorroTecnologia(EquipoAltaEficiencia equipoAlta)
        {
            var equipoAhorros = new CalculosAhorroTecnologia().CalculaAhorroTecnologia(equipoAlta, EquipoBajaSelecionado,
                                                                                       LstBajaEficiencia,
                                                                                       LstAltaEficiencia, Pyme,
                                                                                       FBio, Fmes);

            return equipoAhorros;
        }

        protected decimal RedondeaValor(decimal valor)
        {
            valor = Math.Truncate(valor * 1000000) / 1000000;
            valor = Math.Round(valor, 4);

            return valor;
        }

        protected void GeneraTablaAmortizacionTemp()
        {
            try
            {
                var parseo = (ParseoTrama)Session["PARSEO_TRAMA"];
                var programa = new OpEquiposAbEficiencia().ObtenDatosPrograma(Global.PROGRAM);
                var periodoConsumoFinal =
                    parseo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 159).Dato;
                var montoSolicitado = Presupuesto.First(p => p.IdPresupuesto == 8).Resultado;
                double cat = 0.0;
                var periodoPago = IsSE ? 1 : Datos.PeriodoPago;

                Datos.Plazo = periodoPago == 1 ? (int)programa.No_Plazo * 12 : (int)programa.No_Plazo * 6;


                cat = periodoPago == 1
                            ? (double)programa.Pct_CAT_Factura_Mensual
                            : (double)programa.Pct_CAT_Factura_Bimestral;

                DataTable CreditAmortizacionDt =
                    K_CREDITOBll.ClassInstance.CalculateCreditAmortizacion(txtServiceCode.Text,
                                                                            Convert.ToDouble(montoSolicitado),
                                                                            (double)programa.Pct_Tasa_Fija / 100,
                                                                            Datos.Plazo,
                                                                            periodoPago,
                                                                            (double)programa.Pct_Tasa_Interes / 100,
                                                                            (double)programa.Pct_Tasa_IVA_Intereses /
                                                                            100,
                                                                            cat,
                                                                            periodoConsumoFinal);

                var presupuestoInversion = new CRE_PRESUPUESTO_INVERSION
                    {
                        RPU = txtServiceCode.Text,
                        NombreCliente = txtDX_NOMBRE_COMERCIAL.Text,
                        Periodo = Datos.PeriodoPago == 1 ? "MENSUAL" : "BIMESTRAL",
                        Monto = montoSolicitado,
                        Tasa = Convert.ToDecimal(programa.Pct_Tasa_Interes),
                        Iva = Convert.ToDecimal(programa.Pct_Tasa_IVA_Intereses),
                        Cat = Convert.ToDecimal(cat)
                    };

                new OpEquiposAbEficiencia().CreaAmortizacionTemporal(presupuestoInversion, CreditAmortizacionDt, txtServiceCode.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo generar la tabla de Amortización", ex);
            }

        }

        protected void MuestraResultadosValidacion()
        {
            var lstResultados = new ValidacionTarifas().ObtenResultadosValidacion(Resultado);
            if (lstResultados.Count <= 0) return;

            rgResultados.DataSource = lstResultados;
            rgResultados.DataBind();
            divResultados.Visible = true;

            #region Inserta Equipos NO PASARON
            
            var noPaso = lstResultados.Any(compResultadoValidacion => compResultadoValidacion.Resultado == "NO PASA");

            if (!noPaso) return;

            var intentoAlta = new OpEquiposAbEficiencia().ObtenMaxIntentoEquiposAlta();

            var insertaEquiposAlta =
                new OpEquiposAbEficiencia().InsertaEquiposAltaNoPasaron(LstAltaEficiencia, txtServiceCode.Text,
                    Datos.ValorIva, intentoAlta, Session["UserName"].ToString());

            if (!insertaEquiposAlta) return;

            var intentoBaja = new OpEquiposAbEficiencia().ObtenMaxIntentoEquiposBaja();
            var insertaEquiposBaja = new OpEquiposAbEficiencia().InsertaEquiposBajaNoPasaron(LstBajaEficiencia,
                txtServiceCode.Text, intentoBaja, Session["UserName"].ToString());
            if (!insertaEquiposBaja)
            {
                new OpEquiposAbEficiencia().EliminaProductosBaja(txtServiceCode.Text);
                new OpEquiposAbEficiencia().EliminaProductosAlta(txtServiceCode.Text);
            }
            else
            {
                var periodoPago = IsSE ? 1 : Datos.PeriodoPago;

                foreach (var compResultadoValidacion in lstResultados)
                {
                    switch (compResultadoValidacion.IdResultado)
                    {
                        case 1: //Incremento en facturación del 20%
                            if (compResultadoValidacion.Resultado == "NO PASA")
                            {
                                /*INSERTAR PROCESO PRESUPUESTO DE INVERSION EN EL LOG */
                                Insertlog.InsertarLogPresupuesto(Convert.ToInt16(Session["IdUserLogueado"]),
                                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                                    Convert.ToInt16(Session["IdDepartamento"]), "PRESUPUESTO DE INVERSIÓN",
                                    "REGLA DE 20%", txtServiceCode.Text,
                                    Convert.ToString(Datos.ProximaFacturacionMensual), Convert.ToString(IdTrama),
                                    "", "", intentoAlta, intentoBaja);
                            }
                            break;
                        case 2: //Periodo simple de recuperación
                            if (compResultadoValidacion.Resultado == "NO PASA")
                            {
                                /*INSERTAR PROCESO PRESUPUESTO DE INVERSION EN EL LOG */
                                Insertlog.InsertarLogPresupuesto(Convert.ToInt16(Session["IdUserLogueado"]),
                                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                                    Convert.ToInt16(Session["IdDepartamento"]), "PRESUPUESTO DE INVERSIÓN",
                                    "PERIODO SIMPLE DE RECUPERACIÓN", txtServiceCode.Text,
                                    Convert.ToString(Psr.ValorPsr), Convert.ToString(IdTrama), "", "",
                                    intentoAlta, intentoBaja);
                            }
                            break;

                        case 3: //Nueva Facturación (Facturacion Negativa)
                            if (compResultadoValidacion.Resultado == "NO PASA")
                            {
                                /*INSERTAR PROCESO PRESUPUESTO DE INVERSION EN EL LOG */
                                Insertlog.InsertarLogPresupuesto(Convert.ToInt16(Session["IdUserLogueado"]),
                                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                                    Convert.ToInt16(Session["IdDepartamento"]), "PRESUPUESTO DE INVERSIÓN",
                                    "FACTURACIÓN NEGATIVA", txtServiceCode.Text,
                                    Convert.ToString((FactConAhorro.Total/periodoPago)),
                                    Convert.ToString(IdTrama), "", "", intentoAlta, intentoBaja);

                            }
                            break;

                        case 4: //Energía Eléctrica Suficiente (Consumo Negativo)
                            if (compResultadoValidacion.Resultado == "NO PASA")
                            {
                                /*INSERTAR PROCESO PRESUPUESTO DE INVERSION EN EL LOG */
                                Insertlog.InsertarLogPresupuesto(Convert.ToInt16(Session["IdUserLogueado"]),
                                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                                    Convert.ToInt16(Session["IdDepartamento"]), "PRESUPUESTO DE INVERSIÓN",
                                    "CONSUMO NEGATIVO", txtServiceCode.Text,
                                    Convert.ToString(Datos.KwHConAhorro), Convert.ToString(IdTrama), "", "",
                                    intentoAlta, intentoBaja);
                            }
                            break;

                        case 5: //Capacidad de Pago
                            if (compResultadoValidacion.Resultado == "NO PASA")
                            {
                                /*INSERTAR PROCESO PRESUPUESTO DE INVERSION EN EL LOG */
                                Insertlog.InsertarLogPresupuesto(Convert.ToInt16(Session["IdUserLogueado"]),
                                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                                    Convert.ToInt16(Session["IdDepartamento"]), "PRESUPUESTO DE INVERSIÓN",
                                    "SIN CAPACIDAD DE PAGO", txtServiceCode.Text,
                                    Convert.ToString(CapacidadPago.Capacidad), Convert.ToString(IdTrama), "", "",
                                    intentoAlta, intentoBaja);
                            }
                            break;
                    }
                }
            }

            #endregion
        }


        public
            bool ValidaMontoMaximo()
        {
            //var montoDispuesto = Math.Round(new OpEquiposAbEficiencia().ValidaMontoMaximoCredito(Pyme.RFC),2);
            decimal montoDispuesto = 0.0M;

            montoDispuesto =
                Math.Round(
                    NumeroCredito != null
                        ? new OpEquiposAbEficiencia().ValidaMontoMaximoCreditoEdit(Pyme.RFC.ToUpper(), NumeroCredito)
                        : new OpEquiposAbEficiencia().ValidaMontoMaximoCredito(Pyme.RFC.ToUpper()), 2);

            var montoSolicitado = Presupuesto.First(p => p.IdPresupuesto == 8).Resultado;
            var montoTotal = montoDispuesto + montoSolicitado;

            var topeMaximoXRFC = Convert.ToDecimal(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 4 && p.IDSECCION == 16).VALOR);

            if (montoTotal <= topeMaximoXRFC)
                return true;
                return false;
        }

        public bool ValidaEquiposAltayBaja()
        {
            return new ValidacionTarifas().ValidaEquiposAltayBaja(LstAltaEficiencia, LstBajaEficiencia);
        }
        
        #endregion

        protected void ActualizaResumen()
        {
            var resumen = new StringBuilder();
            resumen.Append("<table border='1' style='font-size: small; width: 100%; background-color: white'>");
            resumen.Append("<tr>");
            resumen.Append("<td><font color='#fff'>&nbsp;GRUPO&nbsp;</font></td>");
            resumen.Append("<td><font color='#fff'>&nbsp;TECNOLOGIA&nbsp;</font></td>");
            resumen.Append("<td><font color='#fff'>&nbsp;TIPO DE PRODUCTO&nbsp;</font></td>");
            resumen.Append("<td><font color='#fff'>&nbsp;CAPACIDAD/SISTEMA&nbsp;</font></td>");
            resumen.Append("<td><font color='#fff'>&nbsp;UNIDAD&nbsp;</font></td>");
            resumen.Append("<td><font color='#fff'>&nbsp;CANTIDAD&nbsp;</font></td>");
            resumen.Append("</tr>");


            var lstBaja = LstBajaEficiencia.OrderBy(me => me.Cve_Tecnologia).ThenBy(me => me.Cve_Grupo).ToList();

            foreach (var equipoBajaEficiencia in lstBaja)
            {
                resumen.Append("<tr>");
                resumen.AppendFormat("<td><font color='#fff'>{0}</font></td>", equipoBajaEficiencia.Dx_Grupo);
                resumen.AppendFormat("<td><font color='#fff'>{0}</font></td>", equipoBajaEficiencia.Dx_Tecnologia);
                resumen.AppendFormat("<td><font color='#fff'>{0}</font></td>", equipoBajaEficiencia.Dx_Tipo_Producto);
                resumen.AppendFormat("<td><font color='#fff'>{0}</font></td>", equipoBajaEficiencia.Dx_Consumo);
                resumen.AppendFormat("<td><font color='#fff'>{0}</font></td>", equipoBajaEficiencia.Dx_Unidad);
                resumen.AppendFormat("<td><font color='#fff'>{0}</font></td>",
                                     equipoBajaEficiencia.Cantidad.ToString());
                resumen.Append("<td>");
                resumen.Append("</tr>");
            }

            resumen.Append("</table>");

            Literal1.Text = resumen.ToString();
        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {
            RealizaCalculosSimulador();

            GridView2.DataSource = FactConAhorro.ConceptosFacturacion;
            GridView2.DataBind();

            LblAmortizacion.Text = Datos.ValorAmortizacion.ToString();
            lblAhorroKw.Text = Datos.TotalAhorrosKw.ToString();
            LblAhorroKwH.Text = Datos.TotalAhorrosKwH.ToString();
        }

        protected void RadButton2_Click(object sender, EventArgs e)
        {
            try
            {
                ParseoTrama();
                CargaInformacionGeneral();
                CargaTecnologias();
                RealizaTarifa();

                var parseo = (ParseoTrama)Session["PARSEO_TRAMA"];
                LblConsumoPromedio.Text = Datos.ConsumoPromedio.ToString();
                LblDemandaMaxima.Text = Datos.DemandaMaxima.ToString();
                LblTarifa.Text = TipoTarifa;
                txtDX_NOMBRE_COMERCIAL.Text =
                    parseo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 146).Dato;
                txtServiceCode.Text =
                    parseo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 6).Dato;
                LblPeridoFacturacion.Text = Datos.PeriodoPago == 1 ? "Mensual" : "Bimestral";
                var factorPotencia = Math.Round(
                    parseo.ComplexParseo.HistorialDetconsumos.Where(p => p.Id > 0).Sum(p => p.FactorPotencia) /
                    InfoTarifa.Periodos, 4);
                LblFactorPotencia.Text = factorPotencia.ToString();

                GridView1.DataSource = FactSinAhorro.ConceptosFacturacion;
                GridView1.DataBind();

                GridView3.DataSource = parseo.ComplexParseo.HistorialDetconsumos;
                GridView3.DataBind();
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert(ex.Message, 300, 150, "Alta y Baja de Equipos", null);
            }

        }

        protected void cboTecnologias_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboTecnologias.SelectedItem.Text.Contains("SUBESTACION"))
                cboTarifaDestino.Visible = lblSistema.Visible = true;
            else
                cboTarifaDestino.Visible = lblSistema.Visible = false;
        }

        protected void rgEquiposBaja_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void RadButton5_Click(object sender, EventArgs e)
        {
            CreaCreditoCliente();
        }

        protected void RadBtnTablaAmortizacion_Click(object sender, EventArgs e)
        {
            if (EsCaptura)
            {
                if (LstAltaEficiencia.Count > 0)
                {
                    GeneraTablaAmortizacionTemp();
                    ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm",
                                                        "window.open('PrintForm.aspx?ReportName=Tabla AmortizacionTemp&RPU=" +
                                                        txtServiceCode.Text +
                                                        "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                        true);
                }
                else
                {
                    rwmVentana.RadAlert("No se han capturado equipos de Alta", 300, 150, "Tabla de Amortización", null);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm",
                                                "window.open('PrintForm.aspx?ReportName=Tabla de Amortizacion&CreditNumber=" +
                                                NumeroCredito +
                                                "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                true);
            }
        }

        protected void RadBtnEditaPresupuesto_Click(object sender, EventArgs e)
        {
            RadWindowManager1.RadConfirm(
                "Los datos de la generación de presupuesto se borrarán. ¿Esta seguro de Editar los datos?",
                "confirmCallBackFn", 300, 100, null, "Editar Presupuesto");
        }

        protected void RadHiddenButton_Click(object sender, EventArgs e)
        {
            new OpEquiposAbEficiencia().EliminaPresupuestoInversion(NumeroCredito);
            InicializaUserControl(NumeroCredito, txtDX_NOMBRE_COMERCIAL.Text);
        }
    }
}