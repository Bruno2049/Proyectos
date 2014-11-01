using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;

public partial class WF_Wizardb : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    CeC_Campos Campos;
    string camposerror;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Configuracion de Campos";
        Sesion.DescripcionPagina = "Active los campos (datos) que deseará usar en el sistema y configure la etiqueta. La(s) Etiqueta(s) del Nombre (Segmentado o Completo) y del Numero de Empleado son OBLIGATORIAS";
        camposerror = "";

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Configuracion, true))
        {
            BGuardarCambios.Visible = false;
            WebPanel1.Visible = false;
            WebPanel6.Visible = false;
            WPCamposPersonalizados.Visible = WPDatosEmp.Visible = WPDatosPersonales.Visible = false;
            WPDirLab.Visible = WPDirPer.Visible = WPTelLab.Visible = WPTelPer.Visible = false;
            return;
        }
        //**************************************************

        if (Sesion.EsWizard == 0)
        {
            BGuardarCambios.Text = "Guardar Cambios";
            BGuardarCambios.Appearance.Image.Url = "./Imagenes/Copia de GuardarComo.png";
        }
        this.Master.FindControl("WC_Menu1").FindControl("mnu_Main").Visible = !Convert.ToBoolean(Sesion.EsWizard);
        this.Master.FindControl("WCBotonesEncabezado1").Visible = !Convert.ToBoolean(Sesion.EsWizard);
        if (!IsPostBack)
        {
            CeC_Campos.Inicializa();
            RBLNombre.SelectedIndex = 1;
            WGBNombreCompleto.Visible = false;
            WGBNombreSeparado.Visible = true;
            WGBDirLab.Enabled = false;
            WGBDirPer.Enabled = false;
            WGBTelLab.Enabled = false;
            WGBTelPer.Enabled = false;
            WGBDatosPersonales.Enabled = false;

            WGBDatosEmp.Enabled = false;
            WGBCamposPersonalizados.Enabled = false;
            CBDatosEmp.Checked = true;
            CBCentroCostos.Checked = true;
            CBArea.Checked = true;
            CBDepartamento.Checked = true;
            CBPuesto.Checked = true;
            CBDPTel1.Checked = true;
            CBDTTel1.Checked = true;
            CBDPCalleyNum.Checked = true;
            CBDPColonia.Checked = true;
            CBDPDelegacion.Checked = true;
            CBDPCiudad.Checked = true;
            CBDTCalleyNum.Checked = true;
            CBDTColonia.Checked = true;
            CBDTDelegacion.Checked = true;
            CBDTCiudad.Checked = true;


            LTRACVE.Text = CeC_Campos.ObtenEtiqueta(LTRACVEDato.Text);
            LNombre.Text = CeC_Campos.ObtenEtiqueta(LNombreCompletoDato.Text);
            LNombre.Text = CeC_Campos.ObtenEtiqueta(LNombreDato.Text);
            LAPaterno.Text = CeC_Campos.ObtenEtiqueta(LAPaternoDato.Text);
            LAMaterno.Text = CeC_Campos.ObtenEtiqueta(LAMaternoDato.Text);

            LFechaNac.Text = CeC_Campos.ObtenEtiqueta(LFechaNacDato.Text);
            LRFC.Text = CeC_Campos.ObtenEtiqueta(LRFCDato.Text);
            LCURP.Text = CeC_Campos.ObtenEtiqueta(LCURPDato.Text);
            LIMSS.Text = CeC_Campos.ObtenEtiqueta(LIMSSDato.Text);
            LEstudios.Text = CeC_Campos.ObtenEtiqueta(LEstudiosDato.Text);
            LSexo.Text = CeC_Campos.ObtenEtiqueta(LSexoDato.Text);
            LNacionalidad.Text = CeC_Campos.ObtenEtiqueta(LNacionalidadDato.Text);
            LFIngreso.Text = CeC_Campos.ObtenEtiqueta(LFIngresoDato.Text);
            LFBaja.Text = CeC_Campos.ObtenEtiqueta(LFBajaDato.Text);


            LDPCalleyNum.Text = CeC_Campos.ObtenEtiqueta(LDPCalleyNumDato.Text);
            LDPColonia.Text = CeC_Campos.ObtenEtiqueta(LDPColoniaDato.Text);
            LDPDelegacion.Text = CeC_Campos.ObtenEtiqueta(LDPDelegacionDato.Text);
            LDPCiudad.Text = CeC_Campos.ObtenEtiqueta(LDPCiudadDato.Text);
            LDPEstado.Text = CeC_Campos.ObtenEtiqueta(LDPEstadoDato.Text);
            LDPPais.Text = CeC_Campos.ObtenEtiqueta(LDPPaisDato.Text);
            LDPCP.Text = CeC_Campos.ObtenEtiqueta(LDPCPDato.Text);


            LDTCalleyNum.Text = CeC_Campos.ObtenEtiqueta(LDTCalleyNumDato.Text);
            LDTColonia.Text = CeC_Campos.ObtenEtiqueta(LDTColoniaDato.Text);
            LDTDelegacion.Text = CeC_Campos.ObtenEtiqueta(LDTDelegacionDato.Text);
            LDTCiudad.Text = CeC_Campos.ObtenEtiqueta(LDTCiudadDato.Text);
            LDTEstado.Text = CeC_Campos.ObtenEtiqueta(LDTEstadoDato.Text);
            LDTPais.Text = CeC_Campos.ObtenEtiqueta(LDTPaisDato.Text);
            LDTCP.Text = CeC_Campos.ObtenEtiqueta(LDTCPDato.Text);

            LDPTel1.Text = CeC_Campos.ObtenEtiqueta(LDPTel1Dato.Text);
            LDPTel2.Text = CeC_Campos.ObtenEtiqueta(LDPTel2Dato.Text);
            LDPCel1.Text = CeC_Campos.ObtenEtiqueta(LDPCel1Dato.Text);
            LDPCel2.Text = CeC_Campos.ObtenEtiqueta(LDPCel2Dato.Text);

            LDTTel1.Text = CeC_Campos.ObtenEtiqueta(LDTTel1Dato.Text);
            LDTTel2.Text = CeC_Campos.ObtenEtiqueta(LDTTel2Dato.Text);
            LDTCel1.Text = CeC_Campos.ObtenEtiqueta(LDTCel1Dato.Text);
            LDTCel2.Text = CeC_Campos.ObtenEtiqueta(LDTCel2Dato.Text);

            LCentroCostos.Text = CeC_Campos.ObtenEtiqueta(LCentroCostosDato.Text);
            LArea.Text = CeC_Campos.ObtenEtiqueta(LAreaDato.Text);
            LDepartamento.Text = CeC_Campos.ObtenEtiqueta(LDepartamentoDato.Text);
            LPuesto.Text = CeC_Campos.ObtenEtiqueta(LPuestoDato.Text);
            LGrupo.Text = CeC_Campos.ObtenEtiqueta(LGrupoDato.Text);
            LNoCredencial.Text = CeC_Campos.ObtenEtiqueta(LNoCredencialDato.Text);
            LLineaProduccion.Text = CeC_Campos.ObtenEtiqueta(LLineaProduccionDato.Text);
            LClaveEmp.Text = CeC_Campos.ObtenEtiqueta(LClaveEmpDato.Text);
            LCompania.Text = CeC_Campos.ObtenEtiqueta(LCompaniaDato.Text);
            LRegion.Text = CeC_Campos.ObtenEtiqueta(LRegionDato.Text);
            LDivision.Text = CeC_Campos.ObtenEtiqueta(LDivisionDato.Text);
            LTipoNomina.Text = CeC_Campos.ObtenEtiqueta(LTipoNominaDato.Text);
            LZona.Text = CeC_Campos.ObtenEtiqueta(LZonaDato.Text);

            LCampo1.Text = CeC_Campos.ObtenEtiqueta(LCampo1Dato.Text);
            LCampo2.Text = CeC_Campos.ObtenEtiqueta(LCampo2Dato.Text);
            LCampo3.Text = CeC_Campos.ObtenEtiqueta(LCampo3Dato.Text);
            LCampo4.Text = CeC_Campos.ObtenEtiqueta(LCampo4Dato.Text);
            LCampo5.Text = CeC_Campos.ObtenEtiqueta(LCampo5Dato.Text);
            TxtTRACVE.Text = LTRACVE.Text;


            if (CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_USUARIOS where USUARIO_USUARIO = 'admin' AND USUARIO_CLAVE = 'admin'") < 1)
            {
                CBFecNac.Checked = !CeC_Campos.ObtenCampoInvisible(LFechaNacDato.Text);
                CBRFC.Checked = !CeC_Campos.ObtenCampoInvisible(LRFCDato.Text);
                CBCURP.Checked = !CeC_Campos.ObtenCampoInvisible(LCURPDato.Text);
                CBIMSS.Checked = !CeC_Campos.ObtenCampoInvisible(LIMSSDato.Text);
                CBEstudios.Checked = !CeC_Campos.ObtenCampoInvisible(LEstudiosDato.Text);
                CBSexo.Checked = !CeC_Campos.ObtenCampoInvisible(LSexoDato.Text);
                CBNacionalidad.Checked = !CeC_Campos.ObtenCampoInvisible(LNacionalidadDato.Text);
                CBFecIngreso.Checked = !CeC_Campos.ObtenCampoInvisible(LFIngresoDato.Text);
                CBFecBaja.Checked = !CeC_Campos.ObtenCampoInvisible(LFBajaDato.Text);
                CBDPCalleyNum.Checked = !CeC_Campos.ObtenCampoInvisible(LDPCalleyNumDato.Text);
                CBDPColonia.Checked = !CeC_Campos.ObtenCampoInvisible(LDPColoniaDato.Text);
                CBDPDelegacion.Checked = !CeC_Campos.ObtenCampoInvisible(LDPDelegacionDato.Text);
                CBDPCiudad.Checked = !CeC_Campos.ObtenCampoInvisible(LDPCiudadDato.Text);
                CBDPEstado.Checked = !CeC_Campos.ObtenCampoInvisible(LDPEstadoDato.Text);
                CBDPPais.Checked = !CeC_Campos.ObtenCampoInvisible(LDPPaisDato.Text);
                CBDPCP.Checked = !CeC_Campos.ObtenCampoInvisible(LDPCPDato.Text);
                CBDTCalleyNum.Checked = !CeC_Campos.ObtenCampoInvisible(LDTCalleyNumDato.Text);
                CBDTColonia.Checked = !CeC_Campos.ObtenCampoInvisible(LDTColoniaDato.Text);
                CBDTDelegacion.Checked = !CeC_Campos.ObtenCampoInvisible(LDTDelegacionDato.Text);
                CBDTCiudad.Checked = !CeC_Campos.ObtenCampoInvisible(LDTCiudadDato.Text);
                CBDTEstado.Checked = !CeC_Campos.ObtenCampoInvisible(LDTEstadoDato.Text);
                CBDTPais.Checked = !CeC_Campos.ObtenCampoInvisible(LDTPaisDato.Text);
                CBDTCP.Checked = !CeC_Campos.ObtenCampoInvisible(LDTCPDato.Text);
                CBDPTel1.Checked = !CeC_Campos.ObtenCampoInvisible(LDPTel1Dato.Text);
                CBDPTel2.Checked = !CeC_Campos.ObtenCampoInvisible(LDPTel2Dato.Text);
                CBDPCel1.Checked = !CeC_Campos.ObtenCampoInvisible(LDPCel1Dato.Text);
                CBDPCel2.Checked = !CeC_Campos.ObtenCampoInvisible(LDPCel2Dato.Text);
                CBDTTel1.Checked = !CeC_Campos.ObtenCampoInvisible(LDTTel1Dato.Text);
                CBDTTel2.Checked = !CeC_Campos.ObtenCampoInvisible(LDTTel2Dato.Text);
                CBDTCel1.Checked = !CeC_Campos.ObtenCampoInvisible(LDTCel1Dato.Text);
                CBDTCel2.Checked = !CeC_Campos.ObtenCampoInvisible(LDTCel2Dato.Text);
                CBCentroCostos.Checked = !CeC_Campos.ObtenCampoInvisible(LCentroCostosDato.Text);
                CBArea.Checked = !CeC_Campos.ObtenCampoInvisible(LAreaDato.Text);
                CBDepartamento.Checked = !CeC_Campos.ObtenCampoInvisible(LDepartamentoDato.Text);
                CBPuesto.Checked = !CeC_Campos.ObtenCampoInvisible(LPuestoDato.Text);
                CBGrupo.Checked = !CeC_Campos.ObtenCampoInvisible(LGrupoDato.Text);
                CBNoCredencial.Checked = !CeC_Campos.ObtenCampoInvisible(LNoCredencialDato.Text);
                CBLineaProduccion.Checked = !CeC_Campos.ObtenCampoInvisible(LLineaProduccionDato.Text);
                CBClaveEmp.Checked = !CeC_Campos.ObtenCampoInvisible(LClaveEmpDato.Text);
                CBCompania.Checked = !CeC_Campos.ObtenCampoInvisible(LCompaniaDato.Text);
                CBRegion.Checked = !CeC_Campos.ObtenCampoInvisible(LRegionDato.Text);
                CBDivision.Checked = !CeC_Campos.ObtenCampoInvisible(LDivisionDato.Text);
                CBTipoNomina.Checked = !CeC_Campos.ObtenCampoInvisible(LTipoNominaDato.Text);
                CBZona.Checked = !CeC_Campos.ObtenCampoInvisible(LZonaDato.Text);
                CBCampo1.Checked = !CeC_Campos.ObtenCampoInvisible(LCampo1Dato.Text);
                CBCampo2.Checked = !CeC_Campos.ObtenCampoInvisible(LCampo2Dato.Text);
                CBCampo3.Checked = !CeC_Campos.ObtenCampoInvisible(LCampo3Dato.Text);
                CBCampo4.Checked = !CeC_Campos.ObtenCampoInvisible(LCampo4Dato.Text);
                CBCampo5.Checked = !CeC_Campos.ObtenCampoInvisible(LCampo5Dato.Text);
            }
            if (CBFecNac.Checked || CBRFC.Checked || CBCURP.Checked || CBIMSS.Checked
               || CBEstudios.Checked || CBSexo.Checked || CBNacionalidad.Checked ||
               CBFecIngreso.Checked || CBFecBaja.Checked)
            {
                CBDatosPersonales.Checked = true;
                WGBDatosPersonales.Enabled = true;
            }
            if (CBDPCalleyNum.Checked || CBDPColonia.Checked || CBDPDelegacion.Checked
                || CBDPCiudad.Checked || CBDPEstado.Checked || CBDPPais.Checked || CBDPCP.Checked)
            {
                CBDirPersonal.Checked = true;
                WGBDirPer.Enabled = true;
            }
            if (CBDTCalleyNum.Checked || CBDTColonia.Checked || CBDTDelegacion.Checked
                || CBDTCiudad.Checked || CBDTEstado.Checked || CBDTPais.Checked || CBDTCP.Checked)
            {
                CBDirLaboral.Checked = true;
                WGBDirLab.Enabled = true;
            }
            if (CBDPTel1.Checked || CBDPTel2.Checked || CBDPCel1.Checked || CBDPCel2.Checked)
            {
                CBTelPersonal.Checked = true;
                WGBTelPer.Enabled = true;
            }
            if (CBDTTel1.Checked || CBDTTel2.Checked || CBDTCel1.Checked || CBDTCel2.Checked)
            {
                CBTelLaboral.Checked = true;
                WGBTelLab.Enabled = true;
            }
            if (CBCentroCostos.Checked || CBArea.Checked || CBDepartamento.Checked || CBPuesto.Checked ||
                CBGrupo.Checked || CBNoCredencial.Checked || CBLineaProduccion.Checked || CBClaveEmp.Checked ||
                CBCompania.Checked || CBRegion.Checked || CBDivision.Checked || CBTipoNomina.Checked || CBZona.Checked)
            {
                CBDatosEmp.Checked = true;
                WGBDatosEmp.Enabled = true;
            }
            if (CBCampo1.Checked || CBCampo2.Checked || CBCampo3.Checked || CBCampo4.Checked || CBCampo5.Checked)
            {
                CBCamposPersonalizados.Checked = true;
                WGBCamposPersonalizados.Enabled = true;
            }
            TxtNombreCompleto.Text = LNombreCompleto.Text;
            TxtNombre.Text = LNombre.Text;
            TxtAPaterno.Text = LAPaterno.Text;
            TxtAMaterno.Text = LAMaterno.Text;

            TxtFechaNac.Text = LFechaNac.Text;
            TxtRFC.Text = LRFC.Text;
            TxtCURP.Text = LCURP.Text;
            TxtIMSS.Text = LIMSS.Text;
            TxtEstudios.Text = LEstudios.Text;
            TxtSexo.Text = LSexo.Text;
            TxtNacionalidad.Text = LNacionalidad.Text;
            TxtFecIngreso.Text = LFIngreso.Text;
            TxtFecBaja.Text = LFBaja.Text;

            TxtDPCalleyNum.Text = LDPCalleyNum.Text;
            TxtDPColonia.Text = LDPColonia.Text;
            TxtDPDelegacion.Text = LDPDelegacion.Text;
            TxtDPCiudad.Text = LDPCiudad.Text;
            TxtDPEstado.Text = LDPEstado.Text;
            TxtDPPais.Text = LDPPais.Text;
            TxtDPCP.Text = LDPCP.Text;

            TxtDTCalleyNum.Text = LDTCalleyNum.Text;
            TxtDTColonia.Text = LDTColonia.Text;
            TxtDTDelegacion.Text = LDTDelegacion.Text;
            TxtDTCiudad.Text = LDTCiudad.Text;
            TxtDTEstado.Text = LDTEstado.Text;
            TxtDTPais.Text = LDTPais.Text;
            TxtDTCP.Text = LDTCP.Text;

            TxtDPTel1.Text = LDPTel1.Text;
            TxtDPTel2.Text = LDPTel2.Text;
            TxtDPCel1.Text = LDPCel1.Text;
            TxtDPCel2.Text = LDPCel2.Text;

            TxtDTTel1.Text = LDTTel1.Text;
            TxtDTTel2.Text = LDTTel2.Text;
            TxtDTCel1.Text = LDTCel1.Text;
            TxtDTCel2.Text = LDTCel2.Text;

            TxtCentroCostos.Text = LCentroCostos.Text;
            TxtArea.Text = LArea.Text;
            TxtDepartamento.Text = LDepartamento.Text;
            TxtPuesto.Text = LPuesto.Text;
            TxtGrupo.Text = LGrupo.Text;
            TxtNoCredencial.Text = LNoCredencial.Text;
            TxtLineaProduccion.Text = LLineaProduccion.Text;
            TxtClaveEmpleado.Text = LClaveEmp.Text;
            TxtCompania.Text = LCompania.Text;
            TxtRegion.Text = LRegion.Text;
            TxtDivision.Text = LDivision.Text;
            TxtTipoNomina.Text = LTipoNomina.Text;
            TxtZona.Text = LZona.Text;

            TxtCampo1.Text = LCampo1.Text;
            TxtCampo2.Text = LCampo2.Text;
            TxtCampo3.Text = LCampo3.Text;
            TxtCampo4.Text = LCampo4.Text;
            TxtCampo5.Text = LCampo5.Text;



            ///
            LTRACVETipo.Text = CeC_Campos.ObtenTipoDatoCampo(LTRACVEDato.Text);
            LNombreCompletoTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LNombreCompletoDato.Text);
            LNombreTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LNombreDato.Text);
            LAPaternoTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LAPaternoDato.Text);
            LAMaternoTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LAMaternoDato.Text);

            LFecNacTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LFechaNacDato.Text);
            LRFCTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LRFCDato.Text);
            LCURPTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LCURPDato.Text);
            LIMSSTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LIMSSDato.Text);
            LEstudiosTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LEstudiosDato.Text);
            LSexoTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LSexoDato.Text);
            LNacionalidadTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LNacionalidadDato.Text);
            LFechaIngresoTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LFIngresoDato.Text);
            LFechaBajaTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LFBajaDato.Text);

            LDPCalleyNumTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPCalleyNumDato.Text);
            LDPColoniaTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPColoniaDato.Text);
            LDPDelegacionTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPDelegacionDato.Text);
            LDPCiudadTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPCiudadDato.Text);
            LDPEstadoTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPEstadoDato.Text);
            LDPPaisTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPPaisDato.Text);
            LDPCPTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPCPDato.Text);

            LDTCalleyNumTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTCalleyNumDato.Text);
            LDTColoniaTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTColoniaDato.Text);
            LDTDelegacionTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTDelegacionDato.Text);
            LDTCiudadTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTCiudadDato.Text);
            LDTEstadoTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTEstadoDato.Text);
            LDTPaisTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTPaisDato.Text);
            LDTCPTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTCPDato.Text);

            LDPTel1Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPTel1Dato.Text);
            LDPTel2Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPTel2Dato.Text);
            LDPCel1Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPCel1Dato.Text);
            LDPCel2Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDPCel2Dato.Text);

            LDTTel1Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTTel1Dato.Text);
            LDTTel2Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTTel2Dato.Text);
            LDTCel1Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTCel1Dato.Text);
            LDTCel2Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDTCel2Dato.Text);

            LCentroCostosTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LCentroCostosDato.Text);
            LAreaTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LAreaDato.Text);
            LDepartamentoTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDepartamentoDato.Text);
            LPuestoTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LPuestoDato.Text);
            LGrupoTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LGrupoDato.Text);
            LNoCredencialTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LNoCredencialDato.Text);
            LLineaProduccionTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LLineaProduccionDato.Text);
            LClaveEmplTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LClaveEmpDato.Text);
            LCompaniaTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LCompaniaDato.Text);
            LRegionTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LRegionDato.Text);
            LDivisionTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LDivisionDato.Text);
            LTipoNominaTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LTipoNominaDato.Text);
            LZonaTipo.Text = CeC_Campos.ObtenTipoDatoCampo(LZonaDato.Text);

            LCampo1Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LCampo1Dato.Text);
            LCampo2Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LCampo2Dato.Text);
            LCampo3Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LCampo3Dato.Text);
            LCampo4Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LCampo4Dato.Text);
            LCampo5Tipo.Text = CeC_Campos.ObtenTipoDatoCampo(LCampo5Dato.Text);

            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asistente de Configuración", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);

        }

    }
    protected void RBLNombre_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBLNombre.SelectedIndex == 0)
        {
            WGBNombreCompleto.Visible = true;
            WGBNombreSeparado.Visible = false;
        }
        else
        {

            WGBNombreCompleto.Visible = false;
            WGBNombreSeparado.Visible = true;
        }
    }
    protected void CBDatosPersonales_CheckedChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            WGBDatosPersonales.Enabled = CBDatosPersonales.Checked;
            WPDatosPersonales.Expanded = CBDatosPersonales.Checked;
            if (WPDatosPersonales.Expanded)
            {
                WPCamposPersonalizados.Expanded = false;
                WPDatosEmp.Expanded = false;
                WPDirLab.Expanded = false;
                WPDirPer.Expanded = false;
                WPTelLab.Expanded = false;
                WPDirPer.Expanded = false;

            }

        }
    }
    protected void CBDirPersonal_CheckedChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            WGBDirPer.Enabled = CBDirPersonal.Checked;
            WPDirPer.Expanded = CBDirPersonal.Checked;
            CBDPCalleyNum.Checked = true;
            CBDPColonia.Checked = true;
            CBDPDelegacion.Checked = true;
            if (WPDirPer.Expanded)
            {
                WPCamposPersonalizados.Expanded = false;
                WPDatosEmp.Expanded = false;
                WPDatosPersonales.Expanded = false;
                WPDirLab.Expanded = false;
                WPTelLab.Expanded = false;

            }
        }
    }
    protected void CBTelPersonal_CheckedChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            WGBTelPer.Enabled = CBTelPersonal.Checked;
            WPTelPer.Expanded = CBTelPersonal.Checked;
            CBDPTel1.Checked = true;
            if (WPTelPer.Expanded)
            {
                WPCamposPersonalizados.Expanded = false;
                WPDatosEmp.Expanded = false;
                WPDatosPersonales.Expanded = false;
                WPDirLab.Expanded = false;
                WPDirPer.Expanded = false;
                WPTelLab.Expanded = false;
            }
        }
    }
    protected void CBDirLaboral_CheckedChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            WGBDirLab.Enabled = CBDirLaboral.Checked;
            WPDirLab.Expanded = CBDirLaboral.Checked;
            CBDTCalleyNum.Checked = true;
            CBDTColonia.Checked = true;
            CBDTDelegacion.Checked = true;
            CBDTCiudad.Checked = true;
            if (WPDirLab.Expanded)
            {
                WPCamposPersonalizados.Expanded = false;
                WPDatosEmp.Expanded = false;
                WPDatosPersonales.Expanded = false;
                WPDirPer.Expanded = false;
                WPTelLab.Expanded = false;
                WPTelPer.Expanded = false;
            }
        }

    }
    protected void CBTelLaboral_CheckedChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            WGBTelLab.Enabled = CBTelLaboral.Checked;
            WPTelLab.Expanded = CBTelLaboral.Checked;
            CBDTTel1.Checked = true;
            if (WPTelLab.Expanded)
            {
                WPCamposPersonalizados.Expanded = false;
                WPDatosEmp.Expanded = false;
                WPDatosPersonales.Expanded = false;
                WPDirLab.Expanded = false;
                WPDirPer.Expanded = false;
                WPDirPer.Expanded = false;
            }
        }
    }

    protected void CBDatosEmp_CheckedChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            WGBDatosEmp.Enabled = CBDatosEmp.Checked;
            WPDatosEmp.Expanded = CBDatosEmp.Checked;
            /*  CBCentroCostos.Checked=true;
              CBArea.Checked = true;
              CBDepartamento.Checked = true;
              CBPuesto.Checked = true;*/
            if (WPDatosEmp.Expanded)
            {
                WPCamposPersonalizados.Expanded = false;
                WPDatosPersonales.Expanded = false;
                WPDirLab.Expanded = false;
                WPDirPer.Expanded = false;
                WPTelLab.Expanded = false;
                WPDirPer.Expanded = false;
            }
        }
    }
    protected void CBCamposPersonalizados_CheckedChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            WGBCamposPersonalizados.Enabled = CBCamposPersonalizados.Checked;
            WPCamposPersonalizados.Expanded = CBCamposPersonalizados.Checked;
            if (WPCamposPersonalizados.Expanded)
            {
                WPDatosEmp.Expanded = false;
                WPDatosPersonales.Expanded = false;
                WPDirLab.Expanded = false;
                WPDirPer.Expanded = false;
                WPTelLab.Expanded = false;
                WPDirPer.Expanded = false;
            }
        }
    }
    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Page_Load(sender, e);
    }
    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {


        Campos = new CeC_Campos();
        if ((TxtTRACVE.Text != ""))
        {
            if ((WGBNombreCompleto.Visible && TxtNombreCompleto.Text != "") || (WGBNombreSeparado.Visible && TxtNombre.Text != "" && TxtAPaterno.Text != "" && TxtAMaterno.Text != ""))
            {
                if (ValidaDatos())
                {
                    GuardaCampos();

                }

            }
            else
                MessageBox.Show("Falta Etiqueta de Campo en Nombre");
        }

        else
            MessageBox.Show("Falta Etiqueta de Campo en el No de Empleado");
    }
    public bool ValidaDatos()
    {

        if (CBDatosPersonales.Checked)
        {
            if (CBFecNac.Checked && TxtFechaNac.Text == "")
                camposerror = camposerror + "," + LFechaNac.Text;
            if (CBRFC.Checked && TxtRFC.Text == "")
                camposerror = camposerror + "," + LRFC.Text;
            if (CBCURP.Checked && TxtCURP.Text == "")
                camposerror = camposerror + "," + LCURP.Text;
            if (CBIMSS.Checked && TxtIMSS.Text == "")
                camposerror = camposerror + "," + LIMSS.Text;
            if (CBEstudios.Checked && TxtEstudios.Text == "")
                camposerror = camposerror + "," + LEstudios.Text;
            if (CBSexo.Checked && TxtSexo.Text == "")
                camposerror = camposerror + "," + LSexo.Text;
            if (CBNacionalidad.Checked && TxtNacionalidad.Text == "")
                camposerror = camposerror + "," + LNacionalidad.Text;
            if (CBFecIngreso.Checked && TxtFecIngreso.Text == "")
                camposerror = camposerror + "," + LFIngreso.Text;
            if (CBFecBaja.Checked && TxtFecBaja.Text == "")
                camposerror = camposerror + "," + LFBaja.Text;
        }
        if (CBDirPersonal.Checked)
        {
            if (CBDPCalleyNum.Checked && TxtDPCalleyNum.Text == "")
                camposerror = camposerror + "," + LDPCalleyNum.Text;
            if (CBDPColonia.Checked && TxtDPColonia.Text == "")
                camposerror = camposerror + "," + LDPColonia.Text;
            if (CBDPDelegacion.Checked && TxtDPDelegacion.Text == "")
                camposerror = camposerror + "," + LDPDelegacion.Text;
            if (CBDPCiudad.Checked && TxtDPCiudad.Text == "")
                camposerror = camposerror + "," + LDPCiudad.Text;
            if (CBDPEstado.Checked && TxtDPEstado.Text == "")
                camposerror = camposerror + "," + LDPEstado.Text;
            if (CBDPPais.Checked && TxtDPPais.Text == "")
                camposerror = camposerror + "," + LDPPais.Text;
            if (CBDPCP.Checked && TxtDPCP.Text == "")
                camposerror = camposerror + "," + LDPCP.Text;
        }
        if (CBTelPersonal.Checked)
        {
            if (CBDPTel1.Checked && TxtDPTel1.Text == "")
                camposerror = camposerror + "," + LDPTel1.Text;
            if (CBDPTel2.Checked && TxtDPTel2.Text == "")
                camposerror = camposerror + "," + LDPTel2.Text;
            if (CBDPCel1.Checked && TxtDPCel1.Text == "")
                camposerror = camposerror + "," + LDPCel1.Text;
            if (CBDPCel2.Checked && TxtDPCel2.Text == "")
                camposerror = camposerror + "," + LDPCel2.Text;
        }
        if (CBDirLaboral.Checked)
        {
            if (CBDTCalleyNum.Checked && TxtDTCalleyNum.Text == "")
                camposerror = camposerror + "," + LDTCalleyNum.Text;
            if (CBDTColonia.Checked && TxtDTColonia.Text == "")
                camposerror = camposerror + "," + LDTColonia.Text;
            if (CBDTDelegacion.Checked && TxtDTDelegacion.Text == "")
                camposerror = camposerror + "," + LDPDelegacion.Text;
            if (CBDTCiudad.Checked && TxtDTCiudad.Text == "")
                camposerror = camposerror + "," + LDTCiudad.Text;
            if (CBDTEstado.Checked && TxtDTEstado.Text == "")
                camposerror = camposerror + "," + LDTEstado.Text;
            if (CBDTPais.Checked && TxtDTPais.Text == "")
                camposerror = camposerror + "," + LDTPais.Text;
            if (CBDTCP.Checked && TxtDTCP.Text == "")
                camposerror = camposerror + "," + LDTCP.Text;
        }
        if (CBTelLaboral.Checked)
        {
            if (CBDTTel1.Checked && TxtDTTel1.Text == "")
                camposerror = camposerror + "," + LDTTel1.Text;
            if (CBDTTel2.Checked && TxtDTTel2.Text == "")
                camposerror = camposerror + "," + LDTTel2.Text;
            if (CBDTCel1.Checked && TxtDTCel1.Text == "")
                camposerror = camposerror + "," + LDTCel1.Text;
            if (CBDTCel2.Checked && TxtDTCel2.Text == "")
                camposerror = camposerror + "," + LDTCel2.Text;
        }
        if (CBDatosEmp.Checked)
        {
            if (CBCentroCostos.Checked && TxtCentroCostos.Text == "")
                camposerror = camposerror + "," + LCentroCostos.Text;
            if (CBArea.Checked && TxtArea.Text == "")
                camposerror = camposerror + "," + LArea.Text;
            if (CBDepartamento.Checked && TxtDepartamento.Text == "")
                camposerror = camposerror + "," + LDepartamento.Text;
            if (CBPuesto.Checked && TxtPuesto.Text == "")
                camposerror = camposerror + "," + LPuesto.Text;
            if (CBGrupo.Checked && TxtGrupo.Text == "")
                camposerror = camposerror + "," + LGrupo.Text;
            if (CBNoCredencial.Checked && TxtNoCredencial.Text == "")
                camposerror = camposerror + "," + LNoCredencial.Text;
            if (CBLineaProduccion.Checked && TxtLineaProduccion.Text == "")
                camposerror = camposerror + "," + LLineaProduccion.Text;
            if (CBClaveEmp.Checked && TxtClaveEmpleado.Text == "")
                camposerror = camposerror + "," + LClaveEmp.Text;
            if (CBCompania.Checked && TxtCompania.Text == "")
                camposerror = camposerror + "," + LCompania.Text;
            if (CBRegion.Checked && TxtRegion.Text == "")
                camposerror = camposerror + "," + LRegion.Text;
            if (CBDivision.Checked && TxtDivision.Text == "")
                camposerror = camposerror + "," + LDivision.Text;
            if (CBTipoNomina.Checked && TxtTipoNomina.Text == "")
                camposerror = camposerror + "," + LTipoNomina.Text;
            if (CBZona.Checked && TxtZona.Text == "")
                camposerror = camposerror + "," + LZona.Text;
        }

        if (CBCamposPersonalizados.Checked)
        {
            if (CBCampo1.Checked && TxtCampo1.Text == "")
                camposerror = camposerror + "," + LCampo1.Text;
            if (CBCampo2.Checked && TxtCampo2.Text == "")
                camposerror = camposerror + "," + LCampo2.Text;
            if (CBCampo3.Checked && TxtCampo3.Text == "")
                camposerror = camposerror + "," + LCampo3.Text;
            if (CBCampo4.Checked && TxtCampo4.Text == "")
                camposerror = camposerror + "," + LCampo4.Text;
            if (CBCampo5.Checked && TxtCampo5.Text == "")
                camposerror = camposerror + "," + LCampo5.Text;
        }
        LError.Text = camposerror;
        if (LError.Text != "")
        {
            LError.Text = "Los Siguientes Campos No Tienen Etiqueta " + camposerror;
            return false;
        }
        else
            return true;
    }
    public void GuardaCampos()
    {
        if (RBLNombre.SelectedIndex == 0)
        {
            Campos.GuardaConfigCamposW(LNombreCompletoDato.Text, TxtNombreCompleto.Text, false, true, false);
            Campos.GuardaConfigCamposW(LNombreDato.Text, LNombreDato.Text, true, true, false);
            Campos.GuardaConfigCamposW(LAPaternoDato.Text, LAPaternoDato.Text, true, true, false);
            Campos.GuardaConfigCamposW(LAMaternoDato.Text, LAMaterno.Text, true, true, false);
            Sesion.ConfiguraSuscripcion.NombrePersona = "NOMBRE_COMPLETO";
        }
        else
        {
            Campos.GuardaConfigCamposW(LNombreDato.Text, TxtNombre.Text, false, true, false);
            Campos.GuardaConfigCamposW(LAPaternoDato.Text, TxtAPaterno.Text, false, true, false);
            Campos.GuardaConfigCamposW(LAMaternoDato.Text, TxtAMaterno.Text, false, true, false);
            Campos.GuardaConfigCamposW(LNombreCompletoDato.Text, LNombreCompleto.Text, true, true, false);
            Sesion.ConfiguraSuscripcion.NombrePersona = "APATERNO+' '+ AMATERNO+' '+NOMBRE";
        }
        Campos.GuardaConfigCamposW(LTRACVEDato.Text, TxtTRACVE.Text, false, false, false);
        Campos.GuardaConfigCamposW("PERSONA_LINK_ID", TxtTRACVE.Text, false, false, false);
        if (CBDatosPersonales.Checked)
        {
            if (CBFecNac.Checked)
                Campos.GuardaConfigCamposW(LFechaNacDato.Text, TxtFechaNac.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LFechaNacDato.Text, LFechaNac.Text, true, true, false);
            if (CBRFC.Checked)
                Campos.GuardaConfigCamposW(LRFCDato.Text, TxtRFC.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LRFCDato.Text, LRFC.Text, true, true, false);
            if (CBCURP.Checked)
                Campos.GuardaConfigCamposW(LCURPDato.Text, TxtCURP.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LCURPDato.Text, LCURP.Text, true, true, false);
            if (CBIMSS.Checked)
                Campos.GuardaConfigCamposW(LIMSSDato.Text, TxtIMSS.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LIMSSDato.Text, LIMSS.Text, true, true, false);
            if (CBEstudios.Checked)
                Campos.GuardaConfigCamposW(LEstudiosDato.Text, TxtEstudios.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LEstudiosDato.Text, LEstudios.Text, true, true, true);
            if (CBSexo.Checked)
                Campos.GuardaConfigCamposW(LSexoDato.Text, TxtSexo.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LSexoDato.Text, LSexo.Text, true, true, true);
            if (CBNacionalidad.Checked)
                Campos.GuardaConfigCamposW(LNacionalidadDato.Text, TxtNacionalidad.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LNacionalidadDato.Text, LNacionalidad.Text, true, true, true);
            if (CBFecIngreso.Checked)
                Campos.GuardaConfigCamposW(LFIngresoDato.Text, TxtFecIngreso.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LFIngresoDato.Text, LFIngreso.Text, true, true, false);
            if (CBFecBaja.Checked)
                Campos.GuardaConfigCamposW(LFBajaDato.Text, TxtFecBaja.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LFBajaDato.Text, LFBaja.Text, true, true, false);
        }
        else
        {
            Campos.GuardaConfigCamposW(LFechaNacDato.Text, LFechaNac.Text, true, true, false);
            Campos.GuardaConfigCamposW(LRFCDato.Text, LRFC.Text, true, true, false);
            Campos.GuardaConfigCamposW(LCURPDato.Text, LCURP.Text, true, true, false);
            Campos.GuardaConfigCamposW(LIMSSDato.Text, LIMSS.Text, true, true, false);
            Campos.GuardaConfigCamposW(LEstudiosDato.Text, LEstudios.Text, true, true, true);
            Campos.GuardaConfigCamposW(LSexoDato.Text, LSexo.Text, true, true, true);
            Campos.GuardaConfigCamposW(LNacionalidadDato.Text, LNacionalidad.Text, true, true, true);
            Campos.GuardaConfigCamposW(LFIngresoDato.Text, LFIngreso.Text, true, true, false);
            Campos.GuardaConfigCamposW(LFBajaDato.Text, LFBaja.Text, true, true, false);

        }
        if (CBDirPersonal.Checked)
        {
            if (CBDPCalleyNum.Checked)
                Campos.GuardaConfigCamposW(LDPCalleyNumDato.Text, TxtDPCalleyNum.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPCalleyNumDato.Text, LDPCalleyNum.Text, true, true, false);
            if (CBDPColonia.Checked)
                Campos.GuardaConfigCamposW(LDPColoniaDato.Text, TxtDPColonia.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPColoniaDato.Text, LDPColonia.Text, true, true, false);
            if (CBDPDelegacion.Checked)
                Campos.GuardaConfigCamposW(LDPDelegacionDato.Text, TxtDPDelegacion.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPDelegacionDato.Text, LDPDelegacion.Text, true, true, false);
            if (CBDPCiudad.Checked)
                Campos.GuardaConfigCamposW(LDPCiudadDato.Text, TxtDPCiudad.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPCiudadDato.Text, LDPCiudad.Text, true, true, false);
            if (CBDPEstado.Checked)
                Campos.GuardaConfigCamposW(LDPEstadoDato.Text, TxtDPEstado.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPEstadoDato.Text, LDPEstado.Text, true, true, false);
            if (CBDPPais.Checked)
                Campos.GuardaConfigCamposW(LDPPaisDato.Text, TxtDPPais.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPPaisDato.Text, LDPPais.Text, true, true, false);
            if (CBDPCP.Checked)
                Campos.GuardaConfigCamposW(LDPCPDato.Text, TxtDPCP.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPCPDato.Text, LDPCP.Text, true, true, false);
        }
        else
        {
            Campos.GuardaConfigCamposW(LDPCalleyNumDato.Text, LDPCalleyNum.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDPColoniaDato.Text, LDPColonia.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDPDelegacionDato.Text, LDPDelegacion.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDPCiudadDato.Text, LDPCiudad.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDPEstadoDato.Text, LDPEstado.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDPPaisDato.Text, LDPPais.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDPCPDato.Text, LDPCP.Text, true, true, false);
        }
        if (CBTelPersonal.Checked)
        {
            if (CBDPTel1.Checked)
                Campos.GuardaConfigCamposW(LDPTel1Dato.Text, TxtDPTel1.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPTel1Dato.Text, LDPTel1.Text, true, true, false);
            if (CBDPTel2.Checked)
                Campos.GuardaConfigCamposW(LDPTel2Dato.Text, TxtDPTel2.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPTel2Dato.Text, LDPTel2.Text, true, true, false);
            if (CBDPCel1.Checked)
                Campos.GuardaConfigCamposW(LDPCel1Dato.Text, TxtDPCel1.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPCel1Dato.Text, LDPCel1.Text, true, true, false);
            if (CBDPCel2.Checked)
                Campos.GuardaConfigCamposW(LDPCel2Dato.Text, TxtDPCel2.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDPCel2Dato.Text, LDPCel2.Text, true, true, false);
        }
        else
        {
            Campos.GuardaConfigCamposW(LDPTel1Dato.Text, LDPTel1.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDPTel2Dato.Text, LDPTel2.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDPCel1Dato.Text, LDPCel1.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDPCel2Dato.Text, LDPCel2.Text, true, true, false);
        }
        if (CBDirLaboral.Checked)
        {
            if (CBDTCalleyNum.Checked)
                Campos.GuardaConfigCamposW(LDTCalleyNumDato.Text, TxtDTCalleyNum.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDTCalleyNumDato.Text, LDTCalleyNum.Text, true, true, false);
            if (CBDTColonia.Checked)
                Campos.GuardaConfigCamposW(LDTColoniaDato.Text, TxtDTColonia.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LDTColoniaDato.Text, LDTColonia.Text, true, true, true);
            if (CBDTDelegacion.Checked)
                Campos.GuardaConfigCamposW(LDTDelegacionDato.Text, TxtDTDelegacion.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LDTDelegacionDato.Text, LDTDelegacion.Text, true, true, true);
            if (CBDTCiudad.Checked)
                Campos.GuardaConfigCamposW(LDTCiudadDato.Text, TxtDTCiudad.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LDTCiudadDato.Text, LDTCiudad.Text, true, true, true);
            if (CBDTEstado.Checked)
                Campos.GuardaConfigCamposW(LDTEstadoDato.Text, TxtDTEstado.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LDTEstadoDato.Text, LDTEstado.Text, true, true, true);
            if (CBDTPais.Checked)
                Campos.GuardaConfigCamposW(LDTPaisDato.Text, TxtDTPais.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LDTPaisDato.Text, LDTPais.Text, true, true, true);
            if (CBDTCP.Checked)
                Campos.GuardaConfigCamposW(LDTCPDato.Text, TxtDTCP.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDTCPDato.Text, LDTCP.Text, true, true, false);
        }
        else
        {
            Campos.GuardaConfigCamposW(LDTCalleyNumDato.Text, LDTCalleyNum.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDTColoniaDato.Text, LDTColonia.Text, true, true, true);
            Campos.GuardaConfigCamposW(LDTDelegacionDato.Text, LDTDelegacion.Text, true, true, true);
            Campos.GuardaConfigCamposW(LDTCiudadDato.Text, LDTCiudad.Text, true, true, true);
            Campos.GuardaConfigCamposW(LDTEstadoDato.Text, LDTEstado.Text, true, true, true);
            Campos.GuardaConfigCamposW(LDTPaisDato.Text, LDTPais.Text, true, true, true);
            Campos.GuardaConfigCamposW(LDTCPDato.Text, LDTCP.Text, true, true, false);
        }
        if (CBTelLaboral.Checked)
        {
            if (CBDTTel1.Checked)
                Campos.GuardaConfigCamposW(LDTTel1Dato.Text, TxtDTTel1.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDTTel1Dato.Text, LDTTel1.Text, true, true, false);
            if (CBDTTel2.Checked)
                Campos.GuardaConfigCamposW(LDTTel2Dato.Text, TxtDTTel2.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDTTel2Dato.Text, LDTTel2.Text, true, true, false);
            if (CBDTCel1.Checked)
                Campos.GuardaConfigCamposW(LDTCel1Dato.Text, TxtDTCel1.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDTCel1Dato.Text, LDTCel1.Text, true, true, false);
            if (CBDTCel2.Checked)
                Campos.GuardaConfigCamposW(LDTCel2Dato.Text, TxtDTCel2.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LDTCel2Dato.Text, LDTCel2.Text, true, true, false);
        }
        else
        {
            Campos.GuardaConfigCamposW(LDTTel1Dato.Text, LDTTel1.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDTTel2Dato.Text, LDTTel2.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDTCel1Dato.Text, LDTCel1.Text, true, true, false);
            Campos.GuardaConfigCamposW(LDTCel2Dato.Text, LDTCel2.Text, true, true, false);
        }
        if (CBDatosEmp.Checked)
        {
            if (CBCentroCostos.Checked)
                Campos.GuardaConfigCamposW(LCentroCostosDato.Text, TxtCentroCostos.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LCentroCostosDato.Text, LCentroCostos.Text, true, true, true);
            if (CBArea.Checked)
                Campos.GuardaConfigCamposW(LAreaDato.Text, TxtArea.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LAreaDato.Text, LArea.Text, true, true, true);
            if (CBDepartamento.Checked)
                Campos.GuardaConfigCamposW(LDepartamentoDato.Text, TxtDepartamento.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LDepartamentoDato.Text, LDepartamento.Text, true, true, true);
            if (CBPuesto.Checked)
                Campos.GuardaConfigCamposW(LPuestoDato.Text, TxtPuesto.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LPuestoDato.Text, LPuesto.Text, true, true, true);
            if (CBGrupo.Checked)
                Campos.GuardaConfigCamposW(LGrupoDato.Text, TxtGrupo.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LGrupoDato.Text, LGrupo.Text, true, true, true);
            if (CBNoCredencial.Checked)
                Campos.GuardaConfigCamposW(LNoCredencialDato.Text, TxtNoCredencial.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LNoCredencialDato.Text, LNoCredencial.Text, true, true, false);
            if (CBLineaProduccion.Checked)
                Campos.GuardaConfigCamposW(LLineaProduccionDato.Text, TxtLineaProduccion.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LLineaProduccionDato.Text, LLineaProduccion.Text, true, true, true);
            if (CBClaveEmp.Checked)
                Campos.GuardaConfigCamposW(LClaveEmpDato.Text, TxtClaveEmpleado.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LClaveEmpDato.Text, LClaveEmp.Text, true, true, false);
            if (CBCompania.Checked)
                Campos.GuardaConfigCamposW(LCompaniaDato.Text, TxtCompania.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LCompaniaDato.Text, LCompania.Text, true, true, true);
            if (CBRegion.Checked)
                Campos.GuardaConfigCamposW(LRegionDato.Text, TxtRegion.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LRegionDato.Text, LRegion.Text, true, true, true);
            if (CBDivision.Checked)
                Campos.GuardaConfigCamposW(LDivisionDato.Text, TxtDivision.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LDivisionDato.Text, LDivision.Text, true, true, true);
            if (CBTipoNomina.Checked)
                Campos.GuardaConfigCamposW(LTipoNominaDato.Text, TxtTipoNomina.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LTipoNominaDato.Text, LTipoNomina.Text, true, true, true);
            if (CBZona.Checked)
                Campos.GuardaConfigCamposW(LZonaDato.Text, TxtZona.Text, false, false, true);
            else
                Campos.GuardaConfigCamposW(LZonaDato.Text, LZona.Text, true, true, true);
        }
        else
        {
            Campos.GuardaConfigCamposW(LCentroCostosDato.Text, LCentroCostos.Text, true, true, true);
            Campos.GuardaConfigCamposW(LAreaDato.Text, LArea.Text, true, true, true);
            Campos.GuardaConfigCamposW(LDepartamentoDato.Text, LDepartamento.Text, true, true, true);
            Campos.GuardaConfigCamposW(LPuestoDato.Text, LPuesto.Text, true, true, true);
            Campos.GuardaConfigCamposW(LGrupoDato.Text, LGrupo.Text, true, true, true);
            Campos.GuardaConfigCamposW(LNoCredencialDato.Text, LNoCredencial.Text, true, true, false);
            Campos.GuardaConfigCamposW(LLineaProduccionDato.Text, LLineaProduccion.Text, true, true, true);
            Campos.GuardaConfigCamposW(LClaveEmpDato.Text, LClaveEmp.Text, true, true, false);
            Campos.GuardaConfigCamposW(LCompaniaDato.Text, LCompania.Text, true, true, true);
            Campos.GuardaConfigCamposW(LRegionDato.Text, LRegion.Text, true, true, true);
            Campos.GuardaConfigCamposW(LDivisionDato.Text, LDivision.Text, true, true, true);
            Campos.GuardaConfigCamposW(LTipoNominaDato.Text, LTipoNomina.Text, true, true, true);
            Campos.GuardaConfigCamposW(LZonaDato.Text, LZona.Text, true, true, true);
        }
        if (CBCamposPersonalizados.Checked)
        {
            if (CBCampo1.Checked)
                Campos.GuardaConfigCamposW(LCampo1Dato.Text, TxtCampo1.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LCampo1Dato.Text, LCampo1.Text, true, true, false);
            if (CBCampo2.Checked)
                Campos.GuardaConfigCamposW(LCampo2Dato.Text, TxtCampo2.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LCampo2Dato.Text, LCampo2.Text, true, true, false);
            if (CBCampo3.Checked)
                Campos.GuardaConfigCamposW(LCampo3Dato.Text, TxtCampo3.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LCampo3Dato.Text, LCampo3.Text, true, true, false);
            if (CBCampo4.Checked)
                Campos.GuardaConfigCamposW(LCampo4Dato.Text, TxtCampo4.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LCampo4Dato.Text, LCampo4.Text, true, true, false);
            if (CBCampo5.Checked)
                Campos.GuardaConfigCamposW(LCampo5Dato.Text, TxtCampo5.Text, false, false, false);
            else
                Campos.GuardaConfigCamposW(LCampo5Dato.Text, LCampo5.Text, true, true, false);
        }
        else
        {
            Campos.GuardaConfigCamposW(LCampo1Dato.Text, LCampo1.Text, true, true, false);
            Campos.GuardaConfigCamposW(LCampo2Dato.Text, LCampo2.Text, true, true, false);
            Campos.GuardaConfigCamposW(LCampo3Dato.Text, LCampo3.Text, true, true, false);
            Campos.GuardaConfigCamposW(LCampo4Dato.Text, LCampo4.Text, true, true, false);
            Campos.GuardaConfigCamposW(LCampo5Dato.Text, LCampo5.Text, true, true, false);
        }
        CeC_Campos.RecargaCampos();
        CeC_BD.CreaRelacionesEmpleados();
//        CeC_BD.ActualizaNombresEmpleados();
        if (Sesion.EsWizard > 0)
            Sesion.Redirige("WF_Wizardc.aspx");
        else
        {
            Sesion.Redirige(Sesion.OrigenConfigCampos);
            return;
        }
        Sesion.OrigenConfigCampos = "WF_Main.aspx";
    }

}
