using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms.VisualStyles;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.SolicitudCredito;

namespace PAEEEM.SupplierModule.Controls
{
    public partial class wucCapturaBasica :UserControl
    {
        #region Variables Globales
        
        public List<Colonia> LstColoniasAsentamientos
        {
            get { return (List<Colonia>)ViewState["LstColoniasAsentamientos"]; }
            set { ViewState["LstColoniasAsentamientos"] = value; }
        }

        public string NoRPU
        {
            get { return ViewState["NoRPU"].ToString(); }
            set { ViewState["NoRPU"] = value; }
        }

        public string NoCredito
        {
            get { return ViewState["NoCredito"].ToString(); }
            set { ViewState["NoCredito"] = value; }
        }

        public int idCliente
        {
            get { return (int)ViewState["idCliente"]; }
            set { ViewState["idCliente"] = value; }
        }

        public Cliente DatosCliente;
        public DIR_Direcciones DireccionesCliente;
        //public DIR_Direcciones DireccionNegocioCliente;
        //public DIR_Direcciones DireccionFiscalCliente;

        #endregion

        #region Carga Inicial

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //NoRPU = "001960400999"; //Session["ValidRPU"]
               // NoCredito = "PAEEEMDB07A02247";
                LlenarCatalogoEstados();
                LlenarDDxSexo();
                LLenarDDxRegimenMatrimonial();
                LLenarDdxAcreditacion();
                LlenaDDxTipoIdentificacion();
                LlenaDDxTipoIdentificacion();
                LlenarDDxTipoPropiedad();
                LlenarLisBoxEstadoCivil();
                LlenarDDxTipoPersona();
            }

        }

        #endregion

        #region Metodos Protegidos

        public void CargaDatosPyme()
        {

            var datosPyme = SolicitudCreditoAcciones.BuscaDatosPyme(Session["ValidRPU"].ToString());

            if (datosPyme != null)
            {
                var TIPOPERSONA = Convert.ToInt32(Session["TipoPersona"]);
                if (Convert.ToInt32(Session["TipoPersona"]) == 1)
                {
                    PanelPersonaFisica.Visible = true;
                    PanelPersonaMoral.Visible = false;
                }
                else
                {
                    PanelPersonaMoral.Visible = true;
                    PanelPersonaFisica.Visible = false;
                }

                DDXTipoPersona.SelectedIndex =Convert.ToInt32(datosPyme.Cve_Tipo_Sociedad);
                if (datosPyme.Cve_Tipo_Sociedad == 1)
                {
                    TxtRFCFisica.Text = datosPyme.RFC;
                    
                }
                else
                {
                    TxtRFCMoral.Text = datosPyme.RFC;
                }
                DDXEstado.SelectedValue = datosPyme.Cve_Estado.ToString();
                if (datosPyme.Cve_Estado != null)
                {
                    LlenarDDxMunicipo((int)datosPyme.Cve_Estado);
                    DDXMunicipio.SelectedValue = datosPyme.Cve_Deleg_Municipio.ToString();
                    if (datosPyme.Cve_Deleg_Municipio != null)
                        LlenarDDxColonias((int)datosPyme.Cve_Estado, (int)datosPyme.Cve_Deleg_Municipio);

                }
                DDXColonia.SelectedValue = datosPyme.Cve_CP.ToString();
                TxtCP.Text = datosPyme.Codigo_Postal;
            }
        }

        protected void CargaGridColonias(string codigoPostal)
        {
            try
            {
                LstColoniasAsentamientos = CatalogosSolicitud.ObtenColoniasXCp(codigoPostal);

                if (LstColoniasAsentamientos != null)
                {
                    if (LstColoniasAsentamientos.Count > 0)
                    {
                        DDXColonia.DataSource = LstColoniasAsentamientos;
                        DDXColonia.DataValueField = "FG_Sexo";
                        DDXColonia.DataTextField = "Dx_Sexo";
                        DDXColonia.DataBind();

                        DDXColonia.Items.Insert(0,"");
                        DDXColonia.SelectedIndex = 0;
                    }
                    else
                    {
                        var sScript = "<script language='JavaScript'>alert('No se encuentra el Codigo Postal, contactar al Agente FIDE.');</script>";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
                    }
                }
                else
                {
                    var sScript = "<script language='JavaScript'>alert('No se encuentra el Codigo Postal, contactar al Agente FIDE.');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
                }
            }
            catch (Exception ex)
            {
                var sScript = "<script language='JavaScript'>alert('Ocurrió un problema al cargar las Colonias.');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);

            }
        }

        protected void CargaDatosCliente()
        {
            //var cliente = SolicitudCreditoAcciones.ObtenClienteComplejo(idProveedor, idSucursal, idCliente, idNegocio);
            var cliente = new Cliente();

            DDXTipoPersona.SelectedValue = cliente.DatosCliente.Cve_Tipo_Sociedad.ToString();

            if (cliente.DatosCliente.Cve_Tipo_Sociedad == 1)
            {
                TxtApellidoPaterno.Text = cliente.DatosCliente.Ap_Paterno;
                TxtApellidoMaterno.Text = cliente.DatosCliente.Ap_Materno;
                TxtNombrePFisica.Text = cliente.DatosCliente.Nombre;
                DDXSexo.SelectedValue = cliente.DatosCliente.Genero.ToString();
                TxtFechaNacimieto.Text = cliente.DatosCliente.Fec_Nacimiento.ToString();
                TxtRFCFisica.Text = cliente.DatosCliente.RFC;
                TxtCP.Text = cliente.DatosCliente.CURP;
                RBEstadoCivil.SelectedValue = cliente.DatosCliente.Cve_Estado_Civil.ToString();
                DDXRegimenMatrimonial.SelectedValue = cliente.DatosCliente.IdRegimenConyugal.ToString();
                //DDXOcupacion.SelectedValue = cliente.DatosCliente.IdTipoAcreditacion.ToString();
                //DDXTipoIdentificaFisica.SelectedValue = cliente.DatosCliente.IdTipoIdentificacion.ToString();
                //TxtNroIdentificacion.Text = cliente.DatosCliente.Numero_Identificacion;
                //TxtEmail.Text = cliente.DatosCliente.email;
            }

            if (cliente.DatosCliente.Cve_Tipo_Sociedad == 2)
            {
                TxtRazonSocial.Text = cliente.DatosCliente.Razon_Social;
                TxtFechaConstitucion.Text = cliente.DatosCliente.Fec_Nacimiento.ToString();
                TxtRFCMoral.Text = cliente.DatosCliente.RFC;
                //DDXTipoIdentificaMoral.SelectedValue = cliente.DatosCliente.IdTipoIdentificacion.ToString();
                //TxtNumeroIdentMoral.Text = cliente.DatosCliente.Numero_Identificacion;
                //TxtEmailMoral.Text = cliente.DatosCliente.email;
                //DDXAcredita.SelectedValue = cliente.DatosCliente.IdTipoAcreditacion.ToString();
            }

            var domicilioNegocio = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 1);

            if (domicilioNegocio != null)
            {
                DDXColonia.SelectedValue = domicilioNegocio.CVE_CP.ToString();
                TxtCalle.Text = domicilioNegocio.Calle;
                domicilioNegocio.Num_Ext = TxtNumeroExterior.Text;
                domicilioNegocio.CP = TxtCP.Text;
                domicilioNegocio.Cve_Estado =Convert.ToByte(DDXEstado.SelectedValue);
                domicilioNegocio.Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipio.SelectedValue);
                domicilioNegocio.Telefono_Local = TxtTelefono.Text;
                domicilioNegocio.Cve_Tipo_Propiedad = int.Parse(DDXTipoPropiedad.Text);
            }

            var domicilioFiscal = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 2);

            if (domicilioFiscal != null)
            {
                DDXColoniaFiscal.SelectedValue = domicilioFiscal.CVE_CP.ToString();
                TxtCalleFiscal.Text = domicilioFiscal.Calle;
                TxtNumeroExterior.Text = domicilioFiscal.Num_Ext;
                TxtCPFiscal.Text = domicilioFiscal.CP;
                DDXEstadoFiscal.SelectedValue = domicilioFiscal.Cve_Estado.ToString();
                DDXMunicipioFiscal.SelectedValue = domicilioFiscal.Cve_Deleg_Municipio.ToString();
                TxttelefonoFiscal.Text = domicilioFiscal.Telefono_Local;
                DDXTipoPropiedadFiscal.SelectedValue = domicilioFiscal.Cve_Tipo_Propiedad.ToString();
            }
        }

        #endregion

        #region Eventos Controles

        protected void ImgBtnBuscarCP_Click(object sender, ImageClickEventArgs e)
        {
            CargaGridColonias(TxtCPFiscal.Text);
        }

        protected void DDXEstadoFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipoFiscal(int.Parse(DDXEstadoFiscal.SelectedValue));
        }

        protected void DDXMunicipioFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxColoniaFiscal(int.Parse(DDXEstadoFiscal.SelectedValue), int.Parse(DDXMunicipioFiscal.SelectedValue));
        }

        protected void DDXColoniaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDXColoniaFiscalHidden.SelectedValue = DDXColoniaFiscal.SelectedValue;
            TxtCPFiscal.Text = DDXColoniaFiscalHidden.SelectedItem.Text;
        }

        //protected void DDXTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DDXTipoPersona.SelectedValue == "1")
        //    {

        //        PanelPersonaFisica.Visible = true;
        //        PanelPersonaMoral.Visible = false;

        //    }

        //    if (DDXTipoPersona.SelectedValue == "2")
        //    {



        //        PanelPersonaFisica.Visible = false;
        //        PanelPersonaMoral.Visible = true;
        //    }
        //}

        //protected void BtnColonia_Click(object sender, EventArgs e)
        //{
        //    var checkseleccionados = 0;
        //    var cveCampoSelec = 0;

        //    for (int i = 0; i < grdColonias.Rows.Count; i++)
        //    {
        //        var ckbSelect = grdColonias.Rows[i].FindControl("ckbSelect") as CheckBox;

        //        if (ckbSelect != null && ckbSelect.Checked)
        //        {
        //            var dataKey = grdColonias.DataKeys[i];
        //            if (dataKey != null)
        //                cveCampoSelec = int.Parse(dataKey[0].ToString());
        //            checkseleccionados++;
        //        }
        //    }

        //    if (checkseleccionados == 0)
        //    {
        //        var sScript = "<script language='JavaScript'>alert('Debe Seleccionar un registro.');</script>";
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
        //    }
        //    else if (checkseleccionados > 1)
        //    {
        //        var sScript = "<script language='JavaScript'>alert('Debe Seleccionar solo un registro.');</script>";
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
        //    }

        //    else
        //    {

        //        var asentamiento = LstColoniasAsentamientos.FirstOrDefault(me => me.CveCp == cveCampoSelec);

        //        if (asentamiento != null)
        //        {
        //            DDXEstadoFiscal.SelectedValue = asentamiento.CveEstado.ToString();
        //            LlenarDDxMunicipoFiscal(asentamiento.CveEstado);
        //            DDXMunicipioFiscal.SelectedValue = asentamiento.CveDelegMunicipio.ToString();
        //            LlenarDDxColoniaFiscal(asentamiento.CveEstado, (int)asentamiento.CveDelegMunicipio);
        //            DDXColoniaFiscal.SelectedValue = asentamiento.CveCp.ToString();
        //            TxtCPFiscal.Text = asentamiento.CodigoPostal;

        //            grdColonias.DataSource = null;
        //            grdColonias.DataBind();

        //            TxtCalleFiscal.Focus();

        //            PanelColonias.Visible = false;
        //        }
        //    }

        //}

        protected void ChkDomFiscal_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkDomFiscal.Checked)
            {
                DDXEstadoFiscal.SelectedValue = DDXEstado.SelectedValue;
                LlenarDDxMunicipoFiscal(int.Parse(DDXEstadoFiscal.SelectedValue));
                DDXMunicipioFiscal.SelectedValue = DDXMunicipio.SelectedValue;
                LlenarDDxColoniaFiscal(int.Parse(DDXEstadoFiscal.SelectedValue),
                    int.Parse(DDXMunicipioFiscal.SelectedValue));
                DDXColoniaFiscal.SelectedValue = DDXColonia.SelectedValue;
                TxtCPFiscal.Text = TxtCP.Text;
                TxtCalleFiscal.Text = TxtCalle.Text;
                TxttelefonoFiscal.Text = TxtTelefono.Text;
                TxtExteriorFiscal.Text = TxtNumeroExterior.Text;
                DDXTipoPropiedadFiscal.SelectedValue = DDXTipoPropiedad.SelectedValue;
            }
            else
            {
                DDXEstadoFiscal.SelectedIndex = 0;
                DDXMunicipioFiscal.Items.Clear();
                DDXColoniaFiscal.Items.Clear();
                TxtCPFiscal.Text = "";
                DDXTipoPropiedadFiscal.SelectedIndex = 0;
                TxtCalleFiscal.Text = "";
                TxttelefonoFiscal.Text = "";
                TxtExteriorFiscal.Text = "";
            }
        }

        protected void RBEstadoCivil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBEstadoCivil.SelectedValue == "1")
            {
                DDXRegimenMatrimonial.Enabled = false;
                RFV7.Enabled = false;
            }

            if (RBEstadoCivil.SelectedValue == "2")
            {
                DDXRegimenMatrimonial.Enabled = true;
                RFV7.Enabled = true;
            }
        }

        //protected void grdColonias_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var dataKey = grdColonias.DataKeys[grdColonias.SelectedRow.RowIndex];
        //    if (dataKey != null)
        //    {
        //        if (dataKey.Values != null)
        //        {
        //            var cveCampoSelec = Convert.ToInt32(dataKey.Values[0]);

        //            var asentamiento = LstColoniasAsentamientos.FirstOrDefault(me => me.CveCp == cveCampoSelec);

        //            if (asentamiento != null)
        //            {
        //                DDXEstadoFiscal.SelectedValue = asentamiento.CveEstado.ToString(CultureInfo.InvariantCulture);
        //                LlenarDDxMunicipoFiscal(asentamiento.CveEstado);
        //                DDXMunicipioFiscal.SelectedValue = asentamiento.CveDelegMunicipio.ToString();
        //                if (asentamiento.CveDelegMunicipio != null)
        //                    LlenarDDxColoniaFiscal(asentamiento.CveEstado, (int)asentamiento.CveDelegMunicipio);
        //                DDXColoniaFiscal.SelectedValue = asentamiento.CveCp.ToString(CultureInfo.InvariantCulture);
        //                TxtCPFiscal.Text = asentamiento.CodigoPostal;

        //                grdColonias.DataSource = null;
        //                grdColonias.DataBind();

        //                TxtCalleFiscal.Focus();

        //                PanelColonias.Visible = false;
        //            }
        //        }
        //    }
        //}

        #endregion

        #region LLenar catalogos

        protected void LlenarCatalogoEstados()
        {
            var lstEstado = CatalogosSolicitud.ObtenCatEstadosRepublica();
            if (lstEstado != null)
            {
                DDXEstado.DataSource = lstEstado;
                DDXEstado.DataValueField = "Cve_Estado";
                DDXEstado.DataTextField = "Dx_Nombre_Estado";
                DDXEstado.DataBind();

                DDXEstado.Items.Insert(0, new ListItem("", ""));
                DDXEstado.SelectedIndex = 0;

                DDXEstadoFiscal.DataSource = lstEstado;
                DDXEstadoFiscal.DataValueField = "Cve_Estado";
                DDXEstadoFiscal.DataTextField = "Dx_Nombre_Estado";
                DDXEstadoFiscal.DataBind();

                DDXEstadoFiscal.Items.Insert(0, new ListItem("", ""));
                DDXEstadoFiscal.SelectedIndex = 0;
            }
        }

        protected void LlenarDDxMunicipo(int idEstado)
        {
            try
            {
                DDXMunicipio.Items.Clear();

                var lstMunicipio = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

                if (lstMunicipio != null)
                {
                    DDXMunicipio.DataSource = lstMunicipio;
                    DDXMunicipio.DataValueField = "Cve_Deleg_Municipio";
                    DDXMunicipio.DataTextField = "Dx_Deleg_Municipio";
                    DDXMunicipio.DataBind();

                    DDXMunicipio.Items.Insert(0, new ListItem("", ""));
                    DDXMunicipio.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void LlenarDDxMunicipoFiscal(int idEstado)
        {
            try
            {
                DDXMunicipioFiscal.Items.Clear();

                var lstMunicipio = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

                if (lstMunicipio != null)
                {
                    DDXMunicipioFiscal.DataSource = lstMunicipio;
                    DDXMunicipioFiscal.DataValueField = "Cve_Deleg_Municipio";
                    DDXMunicipioFiscal.DataTextField = "Dx_Deleg_Municipio";
                    DDXMunicipioFiscal.DataBind();

                    DDXMunicipioFiscal.Items.Insert(0, new ListItem("", ""));
                    DDXMunicipioFiscal.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void LlenarDDxColonias(int idEstado, int idMunicipio)
        {
            try
            {
                DDXColonia.Items.Clear();

                var lstColonia = CatalogosSolicitud.ObtenCatCodigoPostals(idEstado, idMunicipio);

                if (lstColonia != null)
                {
                    DDXColonia.DataSource = lstColonia;
                    DDXColonia.DataValueField = "Cve_CP";
                    DDXColonia.DataTextField = "Dx_Colonia";
                    DDXColonia.DataBind();

                    DDXColonia.Items.Insert(0, new ListItem("", ""));
                    DDXColonia.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        protected void LlenarDDxColoniaFiscal(int idEstado, int idMunicipio)
        {
            try
            {
                DDXColoniaFiscal.Items.Clear();
                DDXColoniaFiscalHidden.Items.Clear();

                var lstColonia = CatalogosSolicitud.ObtenCatCodigoPostals(idEstado, idMunicipio);

                if (lstColonia != null)
                {
                    DDXColoniaFiscal.DataSource = lstColonia;
                    DDXColoniaFiscal.DataValueField = "Cve_CP";
                    DDXColoniaFiscal.DataTextField = "Dx_Colonia";
                    DDXColoniaFiscal.DataBind();

                    DDXColoniaFiscal.Items.Insert(0, "Seleccione");
                    DDXColoniaFiscal.SelectedIndex = 0;

                    DDXColoniaFiscalHidden.DataSource = lstColonia;
                    DDXColoniaFiscalHidden.DataValueField = "Cve_CP";
                    DDXColoniaFiscalHidden.DataTextField = "Codigo_Postal";
                    DDXColoniaFiscalHidden.DataBind();
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void LlenarDDxSexo()
        {
            var lstSexo = CatalogosSolicitud.ObtenCatSexos();

            if (lstSexo != null)
            {
                DDXSexo.DataSource = lstSexo;
                DDXSexo.DataValueField = "FG_Sexo";
                DDXSexo.DataTextField = "Dx_Sexo";
                DDXSexo.DataBind();

                DDXSexo.Items.Insert(0, new ListItem("", ""));
                DDXSexo.SelectedIndex = 0;
            }
        }

        protected void LLenarDDxRegimenMatrimonial()
        {
            var lstregimen = CatalogosSolicitud.ObtenCatRegimenConyugal();

            if (lstregimen != null)
            {
                DDXRegimenMatrimonial.DataSource = lstregimen;
                DDXRegimenMatrimonial.DataValueField = "IdRegimenConyugal";
                DDXRegimenMatrimonial.DataTextField = "RegimenConyugal";
                DDXRegimenMatrimonial.DataBind();

                DDXRegimenMatrimonial.Items.Insert(0, new ListItem("", ""));
                DDXRegimenMatrimonial.SelectedIndex = 0;
            }
        }

        protected void LLenarDdxAcreditacion()
        {
            var lstAcreditacion = CatalogosSolicitud.ObtenCatTipoAcreditacion();

            if (lstAcreditacion != null)
            {
                DDXOcupacion.DataSource = lstAcreditacion;
                DDXOcupacion.DataValueField = "IdTipoAcreditacion";
                DDXOcupacion.DataTextField = "TipoAcreditacion";
                DDXOcupacion.DataBind();

                DDXOcupacion.Items.Insert(0, new ListItem("", ""));
                DDXOcupacion.SelectedIndex = 0;

                DDXAcredita.DataSource = lstAcreditacion;
                DDXAcredita.DataValueField = "IdTipoAcreditacion";
                DDXAcredita.DataTextField = "TipoAcreditacion";
                DDXAcredita.DataBind();

                DDXAcredita.Items.Insert(0, new ListItem("", ""));
                DDXAcredita.SelectedIndex = 0;
            }
        }

        protected void LlenaDDxTipoIdentificacion()
        {
            var lstTipoIdentifica = CatalogosSolicitud.ObtenCatIdentificacion();

            if (lstTipoIdentifica != null)
            {
                DDXTipoIdentificaFisica.DataSource = lstTipoIdentifica;
                DDXTipoIdentificaFisica.DataValueField = "IdTipoIdentificacion";
                DDXTipoIdentificaFisica.DataTextField = "TipoIdentificacion";
                DDXTipoIdentificaFisica.DataBind();

                DDXTipoIdentificaFisica.Items.Insert(0, new ListItem("", ""));
                DDXTipoIdentificaFisica.SelectedIndex = 0;

                DDXTipoIdentificaMoral.DataSource = lstTipoIdentifica;
                DDXTipoIdentificaMoral.DataValueField = "IdTipoIdentificacion";
                DDXTipoIdentificaMoral.DataTextField = "TipoIdentificacion";
                DDXTipoIdentificaMoral.DataBind();

                DDXTipoIdentificaMoral.Items.Insert(0, new ListItem("", ""));
                DDXTipoIdentificaMoral.SelectedIndex = 0;
            }
        }

        protected void LlenarDDxTipoPropiedad()
        {
            var lstTipoPrpiedad = CatalogosSolicitud.ObtenCatTipoPropiedad();

            if (lstTipoPrpiedad != null)
            {
                DDXTipoPropiedad.DataSource = lstTipoPrpiedad;
                DDXTipoPropiedad.DataValueField = "Cve_Tipo_Propiedad";
                DDXTipoPropiedad.DataTextField = "Dx_Tipo_Propiedad";
                DDXTipoPropiedad.DataBind();

                DDXTipoPropiedad.Items.Insert(0, new ListItem("", ""));
                DDXTipoPropiedad.SelectedIndex = 0;

                DDXTipoPropiedadFiscal.DataSource = lstTipoPrpiedad;
                DDXTipoPropiedadFiscal.DataValueField = "Cve_Tipo_Propiedad";
                DDXTipoPropiedadFiscal.DataTextField = "Dx_Tipo_Propiedad";
                DDXTipoPropiedadFiscal.DataBind();

                DDXTipoPropiedadFiscal.Items.Insert(0,  "");
                DDXTipoPropiedadFiscal.SelectedIndex = 0;
            }
        }

        protected void LlenarLisBoxEstadoCivil()
        {
            var lstEstadoCivil = CatalogosSolicitud.ObtenCatEstadoCivil();

            if (lstEstadoCivil != null)
            {
                RBEstadoCivil.DataSource = lstEstadoCivil;
                RBEstadoCivil.DataValueField = "Cve_Estado_Civil";
                RBEstadoCivil.DataTextField = "Dx_Estado_Civil";
                RBEstadoCivil.DataBind();

                RBEstadoCivil.Items.Insert(0,"");
                RBEstadoCivil.SelectedIndex = 0;
            }
        }

        protected void LlenarDDxTipoPersona()
        {
            var lstTipoPersona = CatalogosSolicitud.ObtenCatTipoSociedad();

            if (lstTipoPersona != null)
            {
                DDXTipoPersona.DataSource = lstTipoPersona;
                DDXTipoPersona.DataValueField = "Cve_Tipo_Sociedad";
                DDXTipoPersona.DataTextField = "Dx_Tipo_Sociedad";
                DDXTipoPersona.DataBind();

                DDXTipoPersona.Items.Insert(0, new ListItem("", ""));
                DDXTipoPersona.SelectedIndex = 0;
            }
        }
        
        #endregion                     

        #region Validaciones

        protected bool ValidaEdad()
        {
            var fechaHoy = DateTime.Today;
            var fechaNacimiento = Convert.ToDateTime(TxtFechaNacimieto.Text);
            var edad = fechaHoy.Year - fechaNacimiento.Year;

            if ((edad < 18) || (edad > 5))
                return false;

            if (edad != 66) return true;
            return fechaNacimiento.Month > fechaHoy.Month;
        }

        #endregion

        #region Metodos Publicos

        public Cliente ObtenCliente()
        {
            var cliente = new Cliente {DatosCliente = {IdCliente = idCliente, Cve_Tipo_Sociedad = Byte.Parse(DDXTipoPersona.SelectedValue)}};

            if (DDXTipoPersona.SelectedValue == "1")
            {
                cliente.DatosCliente.Ap_Paterno = TxtApellidoPaterno.Text;
                cliente.DatosCliente.Ap_Materno = TxtApellidoMaterno.Text;
                cliente.DatosCliente.Nombre = TxtNombrePFisica.Text;
                cliente.DatosCliente.Genero = byte.Parse(DDXSexo.SelectedValue);
              //  cliente.DatosCliente.Fec_Nacimiento = Convert.ToDateTime(TxtFechaNacimieto.Text);
                cliente.DatosCliente.RFC = TxtRFCFisica.Text;
                cliente.DatosCliente.CURP = TxtCP.Text;
                cliente.DatosCliente.Cve_Estado_Civil = Byte.Parse(RBEstadoCivil.SelectedValue);
                cliente.DatosCliente.IdRegimenConyugal = Byte.Parse(DDXRegimenMatrimonial.SelectedValue);
                //cliente.DatosCliente.IdTipoAcreditacion = Byte.Parse(DDXOcupacion.SelectedValue);
                //cliente.DatosCliente.IdTipoIdentificacion = Byte.Parse(DDXTipoIdentificaFisica.SelectedValue);
                //cliente.DatosCliente.Numero_Identificacion = TxtNroIdentificacion.Text;
                //cliente.DatosCliente.email = TxtEmail.Text;
            }

            if (DDXTipoPersona.SelectedValue == "2")
            {
                cliente.DatosCliente.Razon_Social = TxtRazonSocial.Text;
                cliente.DatosCliente.RFC = TxtRFCMoral.Text;
                //cliente.DatosCliente.IdTipoIdentificacion = Byte.Parse(DDXTipoIdentificaMoral.SelectedValue);
                //cliente.DatosCliente.Numero_Identificacion = TxtNumeroIdentMoral.Text;
                //cliente.DatosCliente.email = TxtEmailMoral.Text;
                //cliente.DatosCliente.IdTipoAcreditacion = Byte.Parse(DDXAcredita.SelectedValue);
            }

            var domicilioNegocio = new DIR_Direcciones
            {
                IdTipoDomicilio = 1,
                CVE_CP = int.Parse(DDXColonia.SelectedValue),
                Calle = TxtCalle.Text,
                Num_Ext = TxtNumeroExterior.Text,
                CP = TxtCP.Text,
                Cve_Estado = Convert.ToByte(DDXEstado.SelectedValue),
                Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipio.SelectedValue),
                Telefono_Local = TxtTelefono.Text,
                Cve_Tipo_Propiedad = int.Parse(DDXTipoPropiedad.Text)
            };


            var domicilioFiscal = new DIR_Direcciones();
            domicilioNegocio.IdTipoDomicilio = 2;
            domicilioNegocio.CVE_CP = int.Parse(DDXColoniaFiscal.SelectedValue);
            domicilioNegocio.Calle = TxtCalleFiscal.Text;
            domicilioNegocio.Num_Ext = TxtNumeroExterior.Text;
            domicilioNegocio.CP = TxtCPFiscal.Text;
            domicilioNegocio.Cve_Estado = Convert.ToByte(DDXEstadoFiscal.SelectedValue);
            domicilioNegocio.Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipioFiscal.SelectedValue);
            domicilioNegocio.Telefono_Local = TxttelefonoFiscal.Text;
            domicilioNegocio.Cve_Tipo_Propiedad = int.Parse(DDXTipoPropiedadFiscal.SelectedValue);

            cliente.DireccionesCliente.Add(domicilioNegocio);
            cliente.DireccionesCliente.Add(domicilioFiscal);

            return cliente;
        }

        public void GuardaCliente()
        {
            var cliente = ObtenCliente();
            var newDatosCliente = SolicitudCreditoAcciones.ActualizaDatosInfoGeneral(cliente);
        }
        protected void BtnGuardaTermporal_Click(object sender, EventArgs e)
        {
            //*****************agregar el validate page
            DatosCliente = new Cliente();
            DatosCliente.DireccionesCliente = new List<DIR_Direcciones>();
            DatosCliente.IdCliente = Convert.ToInt32(Session["IdCliente"]);
            DatosCliente.DatosCliente = new CLI_Cliente
            {
                IdCliente = DatosCliente.IdCliente,
                Cve_Tipo_Sociedad = Byte.Parse(DDXTipoPersona.SelectedValue),
                Nombre = DDXTipoPersona.SelectedIndex == 1 ? TxtNombrePFisica.Text : "",
                Ap_Paterno = DDXTipoPersona.SelectedIndex == 1 ? TxtApellidoPaterno.Text : "",
                Ap_Materno = DDXTipoPersona.SelectedIndex == 1 ? TxtApellidoMaterno.Text : "",
                Fec_Nacimiento = DDXTipoPersona.SelectedIndex == 1 ? Convert.ToDateTime(TxtFechaNacimieto.Text) : Convert.ToDateTime(TxtFechaConstitucion.Text),
                Razon_Social = DDXTipoPersona.SelectedIndex == 1 ? TxtRazonSocial.Text : "",
                Genero = DDXTipoPersona.SelectedIndex == 1 ? Convert.ToByte(DDXSexo.SelectedIndex) : Convert.ToByte(0),
                RFC = DDXTipoPersona.SelectedIndex == 1 ? TxtRFCFisica.Text : TxtRFCMoral.Text,
                CURP = DDXTipoPersona.SelectedIndex == 1 ? TxtCURP.Text : "",
                Cve_Estado_Civil = (byte?) (DDXTipoPersona.SelectedIndex == 1 ? RBEstadoCivil.SelectedIndex : (int?)null),
                //IdRegimenConyugal = DDXTipoPersona.SelectedIndex == 1 ? Convert.ToByte(DDXRegimenMatrimonial.SelectedIndex) : (byte?)null, IdTipoAcreditacion = DDXTipoPersona.SelectedIndex == 1 ? Convert.ToByte(DDXOcupacion.SelectedIndex) : Convert.ToByte(DDXAcredita.SelectedIndex),
                //IdTipoIdentificacion = DDXTipoPersona.SelectedIndex == 1 ? Convert.ToByte(DDXTipoIdentificaFisica.SelectedIndex) : Convert.ToByte(DDXTipoIdentificaMoral.SelectedIndex),
                //Numero_Identificacion = DDXTipoPersona.SelectedIndex == 1 ? TxtNroIdentificacion.Text : TxtNumeroIdentMoral.Text, email = DDXTipoPersona.SelectedIndex == 1 ? TxtEmail.Text : TxtEmailMoral.Text,
                Fecha_Adicion = DateTime.Now,
                AdicionadoPor = Session["UserName"].ToString(),
                Estatus = 1,
                //Tipo_Industria = 0
            }; // DE DONDE SE TOMA ESTE VALOR



            var direcciones = new DIR_Direcciones
            {
                IdCliente = DatosCliente.IdCliente,
                IdTipoDomicilio = 1, // negocio
                IdDomicilio = 0,//DE DONDE SE TOMA
                CP = TxtCP.Text,
                Cve_Estado = Convert.ToByte(DDXEstado.SelectedIndex),
                Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipio.SelectedIndex),
                Colonia = DDXColonia.SelectedValue,
                Calle = TxtCalle.Text,
                Telefono_Local = TxtTelefono.Text,
                Num_Ext = TxtNumeroExterior.Text,
                Cve_Tipo_Propiedad = DDXTipoPropiedad.SelectedIndex,
                //Cve_CP = ??
                Fecha_Adicion = DateTime.Now,
                AdicionadoPor = Session["UserName"].ToString(),
                Estatus = 1
            };

            DatosCliente.DireccionesCliente.Add(direcciones);

            direcciones = new DIR_Direcciones
            {
                IdCliente = DatosCliente.IdCliente,
                IdTipoDomicilio = 2, //Fiscal
                IdDomicilio = 0, // DE DOND SE TOMA???
                CP = TxtCPFiscal.Text,
                Cve_Estado = Convert.ToByte(DDXEstadoFiscal.SelectedIndex),
                Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipioFiscal.SelectedIndex),
                Colonia = DDXColoniaFiscal.SelectedValue,
                Calle = TxtCalleFiscal.Text,
                Telefono_Oficina = TxttelefonoFiscal.Text,
                Num_Ext = TxtExteriorFiscal.Text,
                Cve_Tipo_Propiedad = DDXTipoPropiedadFiscal.SelectedIndex,
                //Cve_CP = de que campo se toma
                Fecha_Adicion = DateTime.Now,
                AdicionadoPor = Session["UserName"].ToString(),
                Estatus = 1
            };
            DatosCliente.DireccionesCliente.Add(direcciones);

            SolicitudCreditoAcciones.ActualizaDatosInfoGeneral(DatosCliente);

        }

        public Cliente GetObjetoCliente()
        {
            DatosCliente = new Cliente
            {
                DatosCliente = new CLI_Cliente
                {
                    IdCliente = Convert.ToInt32(Session["IdCliente"]),
                    Cve_Tipo_Sociedad = Byte.Parse(DDXTipoPersona.SelectedValue)
                },
                DireccionesCliente = new List<DIR_Direcciones>()
            };

            DatosCliente.DatosCliente = new CLI_Cliente
            {
                IdCliente = DatosCliente.IdCliente,
                Cve_Tipo_Sociedad = Convert.ToByte(DDXTipoPersona.SelectedIndex),
                Nombre = DDXTipoPersona.SelectedIndex == 1 ? TxtNombrePFisica.Text : "",
                Ap_Paterno = DDXTipoPersona.SelectedIndex == 1 ? TxtApellidoPaterno.Text : "",
                Ap_Materno = DDXTipoPersona.SelectedIndex == 1 ? TxtApellidoMaterno.Text : "",
                Fec_Nacimiento = DDXTipoPersona.SelectedIndex == 1 ? Convert.ToDateTime(TxtFechaNacimieto.Text) : Convert.ToDateTime(TxtFechaConstitucion),
                Razon_Social = DDXTipoPersona.SelectedIndex == 1 ? TxtRazonSocial.Text : "",
                Genero =
                    DDXTipoPersona.SelectedIndex == 1 ? Convert.ToByte(DDXSexo.SelectedIndex) : Convert.ToByte(0),
                RFC = DDXTipoPersona.SelectedIndex == 1 ? TxtRFCFisica.Text : TxtRFCMoral.Text,
                CURP = DDXTipoPersona.SelectedIndex == 1 ? TxtCURP.Text : "",
                Cve_Estado_Civil = (byte?) (DDXTipoPersona.SelectedIndex == 1 ? RBEstadoCivil.SelectedIndex : (int?)null),
                IdRegimenConyugal =
                    DDXTipoPersona.SelectedIndex == 1
                        ? Convert.ToByte(DDXRegimenMatrimonial.SelectedIndex)
                        : (byte?)null,
                //IdTipoAcreditacion =DDXTipoPersona.SelectedIndex == 1 ? Convert.ToByte(DDXOcupacion.SelectedIndex) : Convert.ToByte(DDXAcredita.SelectedIndex),
                //IdTipoIdentificacion =DDXTipoPersona.SelectedIndex == 1 ? Convert.ToByte(DDXTipoIdentificaFisica.SelectedIndex) : Convert.ToByte(DDXTipoIdentificaMoral.SelectedIndex),
                //Numero_Identificacion = DDXTipoPersona.SelectedIndex == 1 ? TxtNroIdentificacion.Text : TxtNumeroIdentMoral.Text, email = DDXTipoPersona.SelectedIndex == 1 ? TxtEmail.Text : TxtEmailMoral.Text,
                Fecha_Adicion = DateTime.Now,
                AdicionadoPor = Session["UserName"].ToString(),
                Estatus = 1,
                //Tipo_Industria = 0 // DE DONDE SE TOMA ESTE VALOR
            };

            var direcciones = new DIR_Direcciones
            {
                IdCliente = DatosCliente.IdCliente,
                IdTipoDomicilio = 1, // negocio
                IdDomicilio = 0, //DE DONDE SE TOMA
                CP = TxtCP.Text,
                Cve_Estado = Convert.ToByte(DDXEstado.SelectedIndex),
                Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipio.SelectedIndex),
                Colonia = DDXColonia.SelectedValue,
                Calle = TxtCalle.Text,
                Telefono_Local = TxtTelefono.Text,
                Num_Ext = TxtNumeroExterior.Text,
                Cve_Tipo_Propiedad = DDXTipoPropiedad.SelectedIndex,
                //Cve_CP = ??
                Fecha_Adicion = DateTime.Now,
                AdicionadoPor = Session["UserName"].ToString(),
                Estatus = 1
            };

            DatosCliente.DireccionesCliente.Add(direcciones);

            direcciones = new DIR_Direcciones
            {
                IdCliente = DatosCliente.IdCliente,
                IdTipoDomicilio = 2, //Fiscal
                IdDomicilio = 0, // DE DOND SE TOMA???
                CP = TxtCPFiscal.Text,
                Cve_Estado = Convert.ToByte(DDXEstadoFiscal.SelectedIndex),
                Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipioFiscal.SelectedIndex),
                Colonia = DDXColoniaFiscal.SelectedValue,
                Calle = TxtCalleFiscal.Text,
                Telefono_Oficina = TxttelefonoFiscal.Text,
                Num_Ext = TxtExteriorFiscal.Text,
                Cve_Tipo_Propiedad = DDXTipoPropiedadFiscal.SelectedIndex,
                //Cve_CP = de que campo se toma
                Fecha_Adicion = DateTime.Now,
                AdicionadoPor = Session["UserName"].ToString(),
                Estatus = 1
            };

            DatosCliente.DireccionesCliente.Add(direcciones);

            return DatosCliente;
        }


        #endregion


        protected void TxtCPFiscal_TextChanged(object sender, EventArgs e)
        {
            CargaGridColonias(TxtCPFiscal.Text);
        }

       
       

        //protected void Img1_Click(object sender, ImageClickEventArgs e)
        //{
        //    divMuestraCal.Visible = true;
        //}
        //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        //{
        //    TxtFechaNacimieto.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
        //    divMuestraCal.Visible = false;
        //}

        //protected void Img2_Click(object sender, ImageClickEventArgs e)
        //{
        //    dateField2.Visible = true;
        //}

        //protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        //{
        //    TxtFechaConstitucion.Text = Calendar2.SelectedDate.ToString("yyyy-MM-dd");
        //    dateField2.Visible = false;
        //}

    }
}