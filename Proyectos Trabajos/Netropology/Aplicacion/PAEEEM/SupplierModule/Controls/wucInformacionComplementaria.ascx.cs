using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.LogicaNegocios.SolicitudCredito;

namespace PAEEEM.SupplierModule.Controls
{
    public partial class wucInformacionComplementaria : System.Web.UI.UserControl
    {
        #region Variables Globales

        public List<Colonia> LstColoniasAsentamientos
        {
            get { return (List<Colonia>) ViewState["LstColoniasAsentamientos"]; }
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
            get { return (int) ViewState["idCliente"]; }
            set { ViewState["idCliente"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NoRPU = Session["ValidRPU"].ToString();
                //NoCredito = Session["CreditNumber"].ToString();
                LLenaHorarios();
                LlenarCatalogoEstados();
                DesHabilitarHorarios();
                LlenaDDxSexo();
                PanelPersonaFisica.Visible = false;
                PanelPersonaMoral.Visible = false;
            }
        }

        #region Llenar Catalogos

        protected void LLenaHorarios()
        {
            List<Valor_Catalogo> listHorarios = CatalogosSolicitud.ObtenHorariosTrabajo();

            DDXInicioLunes.DataSource = listHorarios;
            DDXInicioLunes.DataValueField = "CveValorCatalogo";
            DDXInicioLunes.DataTextField = "DescripcionCatalogo";
            DDXInicioLunes.DataBind();
            DDXInicioLunes.Items.Insert(0, new ListItem("", ""));
            DDXInicioLunes.SelectedIndex = 0;

            DDXInicioMartes.DataSource = listHorarios;
            DDXInicioMartes.DataValueField = "CveValorCatalogo";
            DDXInicioMartes.DataTextField = "DescripcionCatalogo";
            DDXInicioMartes.DataBind();
            DDXInicioMartes.Items.Insert(0, new ListItem("", ""));
            DDXInicioMartes.SelectedIndex = 0;

            DDXInicioMiercoles.DataSource = listHorarios;
            DDXInicioMiercoles.DataValueField = "CveValorCatalogo";
            DDXInicioMiercoles.DataTextField = "DescripcionCatalogo";
            DDXInicioMiercoles.DataBind();
            DDXInicioMiercoles.Items.Insert(0, new ListItem("", ""));
            DDXInicioMiercoles.SelectedIndex = 0;

            DDXInicioJueves.DataSource = listHorarios;
            DDXInicioJueves.DataValueField = "CveValorCatalogo";
            DDXInicioJueves.DataTextField = "DescripcionCatalogo";
            DDXInicioJueves.DataBind();
            DDXInicioJueves.Items.Insert(0, new ListItem("", ""));
            DDXInicioJueves.SelectedIndex = 0;

            DDXInicioViernes.DataSource = listHorarios;
            DDXInicioViernes.DataValueField = "CveValorCatalogo";
            DDXInicioViernes.DataTextField = "DescripcionCatalogo";
            DDXInicioViernes.DataBind();
            DDXInicioViernes.Items.Insert(0, new ListItem("", ""));
            DDXInicioViernes.SelectedIndex = 0;

            DDXInicioSabado.DataSource = listHorarios;
            DDXInicioSabado.DataValueField = "CveValorCatalogo";
            DDXInicioSabado.DataTextField = "DescripcionCatalogo";
            DDXInicioSabado.DataBind();
            DDXInicioSabado.Items.Insert(0, new ListItem("", ""));
            DDXInicioSabado.SelectedIndex = 0;

            DDXInicioDomingo.DataSource = listHorarios;
            DDXInicioDomingo.DataValueField = "CveValorCatalogo";
            DDXInicioDomingo.DataTextField = "DescripcionCatalogo";
            DDXInicioDomingo.DataBind();
            DDXInicioDomingo.Items.Insert(0, new ListItem("", ""));
            DDXInicioDomingo.SelectedIndex = 0;

            DDXFinLunes.DataSource = listHorarios;
            DDXFinLunes.DataValueField = "CveValorCatalogo";
            DDXFinLunes.DataTextField = "DescripcionCatalogo";
            DDXFinLunes.DataBind();
            DDXFinLunes.Items.Insert(0, new ListItem("", ""));
            DDXFinLunes.SelectedIndex = 0;

            DDXFinMartes.DataSource = listHorarios;
            DDXFinMartes.DataValueField = "CveValorCatalogo";
            DDXFinMartes.DataTextField = "DescripcionCatalogo";
            DDXFinMartes.DataBind();
            DDXFinMartes.Items.Insert(0, new ListItem("", ""));
            DDXFinMartes.SelectedIndex = 0;

            DDXFinMiercoles.DataSource = listHorarios;
            DDXFinMiercoles.DataValueField = "CveValorCatalogo";
            DDXFinMiercoles.DataTextField = "DescripcionCatalogo";
            DDXFinMiercoles.DataBind();
            DDXFinMiercoles.Items.Insert(0, new ListItem("", ""));
            DDXFinMiercoles.SelectedIndex = 0;

            DDXFinJueves.DataSource = listHorarios;
            DDXFinJueves.DataValueField = "CveValorCatalogo";
            DDXFinJueves.DataTextField = "DescripcionCatalogo";
            DDXFinJueves.DataBind();
            DDXFinJueves.Items.Insert(0, new ListItem("", ""));
            DDXFinJueves.SelectedIndex = 0;

            DDXFinViernes.DataSource = listHorarios;
            DDXFinViernes.DataValueField = "CveValorCatalogo";
            DDXFinViernes.DataTextField = "DescripcionCatalogo";
            DDXFinViernes.DataBind();
            DDXFinViernes.Items.Insert(0, new ListItem("", ""));
            DDXFinViernes.SelectedIndex = 0;

            DDXFinSabado.DataSource = listHorarios;
            DDXFinSabado.DataValueField = "CveValorCatalogo";
            DDXFinSabado.DataTextField = "DescripcionCatalogo";
            DDXFinSabado.DataBind();
            DDXFinSabado.Items.Insert(0, new ListItem("", ""));
            DDXFinSabado.SelectedIndex = 0;

            DDXFinDomingo.DataSource = listHorarios;
            DDXFinDomingo.DataValueField = "CveValorCatalogo";
            DDXFinDomingo.DataTextField = "DescripcionCatalogo";
            DDXFinDomingo.DataBind();
            DDXFinDomingo.Items.Insert(0, new ListItem("", ""));
            DDXFinDomingo.SelectedIndex = 0;
        }

        protected void DesHabilitarHorarios()
        {
            DDXInicioLunes.Attributes.Add("disabled", "true");
            DDXFinLunes.Attributes.Add("disabled", "true");

            DDXInicioMartes.Attributes.Add("disabled", "true");
            DDXFinMartes.Attributes.Add("disabled", "true");

            DDXInicioMiercoles.Attributes.Add("disabled", "true");
            DDXFinMiercoles.Attributes.Add("disabled", "true");

            DDXInicioJueves.Attributes.Add("disabled", "true");
            DDXFinJueves.Attributes.Add("disabled", "true");

            DDXInicioViernes.Attributes.Add("disabled", "true");
            DDXFinViernes.Attributes.Add("disabled", "true");

            DDXInicioSabado.Attributes.Add("disabled", "true");
            DDXFinSabado.Attributes.Add("disabled", "true");

            DDXInicioDomingo.Attributes.Add("disabled", "true");
            DDXFinDomingo.Attributes.Add("disabled", "true");
        }

        protected void LlenarCatalogoEstados()
        {
            var lstEstado = CatalogosSolicitud.ObtenCatEstadosRepublica();
            if (lstEstado != null)
            {
                DDXEstadoOS.DataSource = lstEstado;
                DDXEstadoOS.DataValueField = "Cve_Estado";
                DDXEstadoOS.DataTextField = "Dx_Nombre_Estado";
                DDXEstadoOS.DataBind();

                DDXEstadoOS.Items.Insert(0, new ListItem("", ""));
                DDXEstadoOS.SelectedIndex = 0;

                DDXEstadoPN.DataSource = lstEstado;
                DDXEstadoPN.DataValueField = "Cve_Estado";
                DDXEstadoPN.DataTextField = "Dx_Nombre_Estado";
                DDXEstadoPN.DataBind();

                DDXEstadoPN.Items.Insert(0, new ListItem("", ""));
                DDXEstadoPN.SelectedIndex = 0;

                DDXEstadoAC.DataSource = lstEstado;
                DDXEstadoAC.DataValueField = "Cve_Estado";
                DDXEstadoAC.DataTextField = "Dx_Nombre_Estado";
                DDXEstadoAC.DataBind();

                DDXEstadoAC.Items.Insert(0, new ListItem("", ""));
                DDXEstadoAC.SelectedIndex = 0;

                DDXEstadoPNRL.DataSource = lstEstado;
                DDXEstadoPNRL.DataValueField = "Cve_Estado";
                DDXEstadoPNRL.DataTextField = "Dx_Nombre_Estado";
                DDXEstadoPNRL.DataBind();

                DDXEstadoPNRL.Items.Insert(0, new ListItem("", ""));
                DDXEstadoPNRL.SelectedIndex = 0;

                DDXEstadoClienteAC.DataSource = lstEstado;
                DDXEstadoClienteAC.DataValueField = "Cve_Estado";
                DDXEstadoClienteAC.DataTextField = "Dx_Nombre_Estado";
                DDXEstadoClienteAC.DataBind();

                DDXEstadoClienteAC.Items.Insert(0, new ListItem("", ""));
                DDXEstadoClienteAC.SelectedIndex = 0;

                DDXEstadoClienteAC.DataSource = lstEstado;
                DDXEstadoClienteAC.DataValueField = "Cve_Estado";
                DDXEstadoClienteAC.DataTextField = "Dx_Nombre_Estado";
                DDXEstadoClienteAC.DataBind();

                DDXEstadoClienteAC.Items.Insert(0, new ListItem("", ""));
                DDXEstadoClienteAC.SelectedIndex = 0;
            }
        }

        protected void LlenarDDxMunicipo(int idEstado, DropDownList ddxMunicipio)
        {
            try
            {
                ddxMunicipio.Items.Clear();

                var lstMunicipio = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

                if (lstMunicipio != null)
                {
                    ddxMunicipio.DataSource = lstMunicipio;
                    ddxMunicipio.DataValueField = "Cve_Deleg_Municipio";
                    ddxMunicipio.DataTextField = "Dx_Deleg_Municipio";
                    ddxMunicipio.DataBind();

                    ddxMunicipio.Items.Insert(0, "Seleccione");
                    ddxMunicipio.SelectedIndex = 0;
                    ddxMunicipio.Focus();
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void LlenarDDxColonias(int idEstado, int idMunicipio, DropDownList ddxColonia,
            DropDownList ddxcoloniaHidden)
        {
            try
            {
                ddxColonia.Items.Clear();

                var lstColonia = CatalogosSolicitud.ObtenCatCodigoPostals(idEstado, idMunicipio);

                if (lstColonia != null)
                {
                    ddxColonia.DataSource = lstColonia;
                    ddxColonia.DataValueField = "Cve_CP";
                    ddxColonia.DataTextField = "Dx_Colonia";
                    ddxColonia.DataBind();

                    ddxColonia.Items.Insert(0, new ListItem("", ""));
                    ddxColonia.SelectedIndex = 0;

                    ddxcoloniaHidden.DataSource = lstColonia;
                    ddxcoloniaHidden.DataValueField = "Cve_CP";
                    ddxcoloniaHidden.DataTextField = "Codigo_Postal";
                    ddxcoloniaHidden.DataBind();

                    ddxcoloniaHidden.Items.Insert(0, new ListItem("", ""));
                    ddxcoloniaHidden.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void LlenaDDxSexo()
        {
              var lstGenero = CatalogosSolicitud.ObtenCatSexos();
              if (lstGenero != null)
              {
                  DDXSexo.DataSource = lstGenero;
                  DDXSexo.DataValueField = "Fg_Sexo";
                  DDXSexo.DataTextField = "Dx_Sexo";
                  DDXSexo.DataBind();

                  DDXSexo.Items.Insert(0, "Selecciones");
                  DDXSexo.SelectedIndex = 0;
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
                        grdColonias.DataSource = LstColoniasAsentamientos;
                        grdColonias.DataBind();
                        PanelColonias.Visible = true;
                    }
                    else
                    {
                        var sScript =
                            "<script language='JavaScript'>alert('No se encuentra el Codigo Postal, contactar al Agente FIDE.');</script>";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
                    }
                }
                else
                {
                    var sScript =
                        "<script language='JavaScript'>alert('No se encuentra el Codigo Postal, contactar al Agente FIDE.');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);
                }
            }
            catch (Exception ex)
            {
                var sScript =
                    "<script language='JavaScript'>alert('Ocurrió un problema al cargar las Colonias.');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);

            }
        }

        #endregion

        #region Eventos

        protected void RBListTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBListTipoPersona.SelectedValue == "1")
            {
                PanelPersonaMoral.Visible = false;
                PanelPersonaFisica.Visible = true;
            }
            if (RBListTipoPersona.SelectedValue == "2")
            {
                PanelPersonaMoral.Visible = true;
                PanelPersonaFisica.Visible = false;
            }
        }

        protected void DDXEstadoPNRL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipo(int.Parse(DDXEstadoPNRL.SelectedValue), DDXMunicipioPNRL);
        }

        protected void DDXEstadoClienteAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipo(int.Parse(DDXEstadoClienteAC.SelectedValue), DDXMunicipipClienteAC);
        }

        protected void DDXEstadoPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipo(int.Parse(DDXEstadoPN.SelectedValue), DDXMunicipioPN);
        }

        protected void DDXEstadoAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipo(int.Parse(DDXEstadoAC.SelectedValue), DDXMunicipioAC);
        }

        protected void DDXEstadoOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipo(int.Parse(DDXEstadoOS.SelectedValue), DDXMunicipioOS);
        }

        protected void DDXMunicipioOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxColonias(int.Parse(DDXEstadoOS.SelectedValue), int.Parse(DDXMunicipioOS.SelectedValue),
                DDXColoniaOS, DDXColoniaOSHidden);
        }

        protected void DDXColoniaOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDXColoniaOSHidden.SelectedValue = DDXColoniaOS.SelectedValue;
            TxtCPOS.Text = DDXColoniaOSHidden.SelectedItem.Text;
        }

        protected void ImgBtnBuscarCP_Click(object sender, ImageClickEventArgs e)
        {
            CargaGridColonias(TxtCPOS.Text);
        }

        protected void ChkRepLegal_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkRepLegal.Checked)
            {
                TxtNombreRepLegal.Text =Session["NombreCliente"].ToString();
                TxtApPaternoRepLegal.Text = Session["ApellidoPatCliente"].ToString();
                TxtApMaternoRepLegal.Text = Session["ApellidoMatCliente"].ToString();
                TxtEmailRepLegal.Text = Session["EmailCliente"].ToString();
                TxtTelefonoRepLegal.Text = Session["TelefonoCliente"].ToString();
            }
            else
            {
                TxtNombreRepLegal.Text = "";
                TxtApPaternoRepLegal.Text = "";
                TxtApMaternoRepLegal.Text = "";
                TxtEmailRepLegal.Text = "";
                TxtTelefonoRepLegal.Text = "";
            }
        }

        protected void grdColonias_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cveCampoSelec = Convert.ToInt32(grdColonias.DataKeys[grdColonias.SelectedRow.RowIndex].Values[0]);
            var asentamiento = LstColoniasAsentamientos.FirstOrDefault(me => me.CveCp == cveCampoSelec);

            if (asentamiento != null)
            {
                DDXEstadoOS.SelectedValue = asentamiento.CveEstado.ToString();
                LlenarDDxMunicipo(asentamiento.CveEstado, DDXMunicipioOS);
                DDXMunicipioOS.SelectedValue = asentamiento.CveDelegMunicipio.ToString();
                LlenarDDxColonias(asentamiento.CveEstado, (int) asentamiento.CveDelegMunicipio, DDXColoniaOS,
                    DDXColoniaOSHidden);
                DDXColoniaOS.SelectedValue = asentamiento.CveCp.ToString();
                TxtCPOS.Text = asentamiento.CodigoPostal;

                grdColonias.DataSource = null;
                grdColonias.DataBind();

                TxtCalleOS.Focus();

                PanelColonias.Visible = false;
            }
        }

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
        //            DDXEstadoOS.SelectedValue = asentamiento.CveEstado.ToString();
        //            LlenarDDxMunicipo(asentamiento.CveEstado,DDXMunicipioOS);
        //            DDXMunicipioOS.SelectedValue = asentamiento.CveDelegMunicipio.ToString();
        //            LlenarDDxColonias(asentamiento.CveEstado, (int) asentamiento.CveDelegMunicipio, DDXColoniaOS,
        //                DDXColoniaOSHidden);
        //            DDXColoniaOS.SelectedValue = asentamiento.CveCp.ToString();
        //            TxtCPOS.Text = asentamiento.CodigoPostal;

        //            grdColonias.DataSource = null;
        //            grdColonias.DataBind();

        //            TxtCalleOS.Focus();

        //            PanelColonias.Visible = false;
        //        }
        //    }
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Page.Validate();
            InsertaHorarios(1);
        }

        #endregion

        #region Metodos Protegidos

        protected void InsertaHorarios(byte tipoHorario)
        {
            var lstHorarios = new List<CLI_HORARIOS_OPERACION>();

            var horario = new CLI_HORARIOS_OPERACION
            {
                No_Credito = Session["CreditNumber"].ToString(),
                IDTIPOHORARIO = tipoHorario,
                ID_DIA_OPERACION = 1, //Lunes
                Hora_Inicio = ChkLunes.Checked ? DDXInicioLunes.SelectedItem.Text : "",
                //Hora_Fin = ChkLunes.Checked ? DDXFinLunes.SelectedItem.Text : ""
                //Cve_Dia = 1,
                //Valor_Entrada = ChkLunes.Checked ? Convert.ToDouble(DDXInicioLunes.SelectedValue) : 0.0,
                //Dx_Entrada = ChkLunes.Checked ? DDXInicioLunes.SelectedItem.Text : "",
                //Valor_Salida = ChkLunes.Checked ? Convert.ToDouble(DDXFinLunes.SelectedValue) : 0.0,
                //Dx_salida = ChkLunes.Checked ? DDXFinLunes.SelectedItem.Text : "",
                //Total_Horas = ChkLunes.Checked ? Convert.ToDouble(HiddenFieldLunes.Value) : 0.0
            };
            lstHorarios.Add(horario);

            horario = new CLI_HORARIOS_OPERACION
            {
                No_Credito = Session["CreditNumber"].ToString(),
                IDTIPOHORARIO = tipoHorario,
                ID_DIA_OPERACION = 2, //Martes
                Hora_Inicio = ChkMartes.Checked ? DDXInicioMartes.SelectedItem.Text : "",
                //Hora_Fin = ChkMartes.Checked ? DDXFinMartes.SelectedItem.Text : "",
            };
            lstHorarios.Add(horario);

            horario = new CLI_HORARIOS_OPERACION
            {
                No_Credito = Session["CreditNumber"].ToString(),
                IDTIPOHORARIO = tipoHorario,
                ID_DIA_OPERACION = 3, //Miercoles
                Hora_Inicio = ChkMiercoles.Checked ? DDXInicioMiercoles.SelectedItem.Text : "",
                //Hora_Fin = ChkMiercoles.Checked ? DDXFinMiercoles.SelectedItem.Text : ""
            };
            lstHorarios.Add(horario);

            horario = new CLI_HORARIOS_OPERACION
            {
                No_Credito = Session["CreditNumber"].ToString(),
                IDTIPOHORARIO = tipoHorario,
                ID_DIA_OPERACION = 4, //Jueves
                Hora_Inicio = ChkJueves.Checked ? DDXInicioJueves.SelectedItem.Text : "",
                //Hora_Fin = ChkJueves.Checked ? DDXFinJueves.SelectedItem.Text : ""
            };
            lstHorarios.Add(horario);

            horario = new CLI_HORARIOS_OPERACION
            {
                No_Credito = Session["CreditNumber"].ToString(),
                IDTIPOHORARIO = tipoHorario,
                ID_DIA_OPERACION = 4, //Viernes
                Hora_Inicio = ChkViernes.Checked ? DDXInicioViernes.SelectedItem.Text : "",
                //Hora_Fin = ChkViernes.Checked ? DDXFinViernes.SelectedItem.Text : ""
            };
            lstHorarios.Add(horario);

            horario = new CLI_HORARIOS_OPERACION
            {
                No_Credito = Session["CreditNumber"].ToString(),
                IDTIPOHORARIO = tipoHorario,
                ID_DIA_OPERACION = 6, //Sabado
                Hora_Inicio = ChkSabado.Checked ? DDXInicioSabado.SelectedItem.Text : "",
                //Hora_Fin = ChkSabado.Checked ? DDXFinSabado.SelectedItem.Text : ""
            };
            lstHorarios.Add(horario);

            horario = new CLI_HORARIOS_OPERACION
            {
                No_Credito = Session["CreditNumber"].ToString(),
                IDTIPOHORARIO = tipoHorario,
                ID_DIA_OPERACION = 7, //Domingo
                Hora_Inicio = ChkDomingo.Checked ? DDXInicioDomingo.SelectedItem.Text : "",
                //Hora_Fin = ChkDomingo.Checked ? DDXFinDomingo.SelectedItem.Text : ""
            };
            lstHorarios.Add(horario);


            var horasOperacionTotal = new H_OPERACION_TOTAL
            {
                No_Credito = Session["CreditNumber"].ToString(),
                IDTIPOHORARIO = tipoHorario,
                HORAS_SEMANA = Convert.ToByte(TxtHorasSemana),
                SEMANAS_AÑO = Convert.ToByte(TxtSemanasAnio),
                HORAS_AÑO = Convert.ToByte(TxtHorasAnio)
            };

            //Inserta Horarios por Dias y el Total 
            var resultado = SolicitudCreditoAcciones.ActualizaHorarioOperacion_IdCredSust(lstHorarios, horasOperacionTotal);

            if (resultado != null)
            {

            }
        }

        protected void CargaHorariosOperacion(List<CLI_HORARIOS_OPERACION> lisHorariosOperacio)
        {
            //foreach (var VARIABLE in COLLECTION)
            //{

            //}
        }

        #endregion

        #region Metodos Publicos

        public Cliente ObtenInfoComplementariaCliente()
        {
            var cliente = new Cliente();


            cliente.IdCliente =Convert.ToInt32(Session["idCliente"]);
            cliente.ReferenciasNotariales = new List<CLI_Referencias_Notariales>();
            cliente.DireccionesCliente = new List<DIR_Direcciones>();
          //  cliente.RepresentanteLegal = new CLI_Ref_Cliente();
                //REPRESENTALTE LEGAL
            cliente.RepresentanteLegal = new CLI_Ref_Cliente
            {
                IdTipoReferencia = 1, 
                Nombre = TxtNombreRepLegal.Text,
                Ap_Paterno = TxtApPaternoRepLegal.Text,
                Ap_Materno = TxtApMaternoRepLegal.Text,
                email = TxtEmailRepLegal.Text,
                Telefono_Local = TxtTelefonoRepLegal.Text,
                Fecha_Adicion = DateTime.Now,
                AdicionadoPor = Session["UserName"].ToString(),
                Estatus = 1
            };
            

            //OBLIGADO SOLIDARIO PERSONA FISICA
            if (RBListTipoPersona.SelectedValue == "1")
            {
                var domObligadoSolidario = new DIR_Direcciones
                {
                    IdTipoDomicilio = 3,
                    CVE_CP = int.Parse(DDXColoniaOS.SelectedValue),
                    CP = TxtCPOS.Text,
                    Cve_Estado = Convert.ToByte(DDXEstadoOS.SelectedValue),
                    Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipioOS.SelectedValue),
                    Calle = TxtCalleOS.Text,
                    Num_Ext = TxtNoExteriorOS.Text,
                    Telefono_Local = TxtTelefonoOS.Text,
                    Fecha_Adicion = DateTime.Now,
                    AdicionadoPor = Session["UserName"].ToString(),
                    Estatus = 1
                };
                cliente.DireccionesCliente.Add(domObligadoSolidario);

                cliente.ObligadoSolidario = new CLI_Ref_Cliente();
                cliente.ObligadoSolidario.IdTipoReferencia = 2;
                cliente.ObligadoSolidario.Cve_Tipo_Sociedad = Byte.Parse(RBListTipoPersona.SelectedValue);
                cliente.ObligadoSolidario.Nombre = TxtNombrePF.Text;
                cliente.ObligadoSolidario.Ap_Paterno = TxtApPaterno.Text;
                cliente.ObligadoSolidario.Ap_Materno = TxtApMaterno.Text;
                cliente.ObligadoSolidario.Genero = Byte.Parse(DDXSexo.SelectedValue);
                cliente.ObligadoSolidario.Fec_Nacimiento = Convert.ToDateTime(TxtFechaNacimietoOS.Text);
                cliente.ObligadoSolidario.RFC = TxtRFCOS.Text;
                cliente.ObligadoSolidario.CURP = TxtCURPOS.Text;
                cliente.ObligadoSolidario.Telefono_Local = TxtTelefonoOS.Text;

                cliente.ObligadoSolidario.Fecha_Adicion = DateTime.Now;
                cliente.ObligadoSolidario.AdicionadoPor = Session["UserName"].ToString();
                cliente.ObligadoSolidario.Estatus = 1;

            }

            //OBLIGADO SOLIDARIO PERSONA MORAL
            if (RBListTipoPersona.SelectedValue == "2")
            {
                cliente.ObligadoSolidario = new CLI_Ref_Cliente();
                cliente.ObligadoSolidario.IdTipoReferencia = 2;
                cliente.ObligadoSolidario.Razon_Social = TxtRazonSocialOS.Text;
                cliente.ObligadoSolidario.Cve_Tipo_Sociedad = Byte.Parse(RBListTipoPersona.SelectedValue);
                cliente.ObligadoSolidario.Fecha_Adicion = DateTime.Now;
                cliente.ObligadoSolidario.AdicionadoPor = Session["UserName"].ToString();
                cliente.ObligadoSolidario.Estatus = 1;

                //REPRESENTANTE LEGAL DEL OBLIGADO SOLIDARIO
                cliente.RepLegalObligadoSolidario = new CLI_Ref_Cliente();
                cliente.RepLegalObligadoSolidario.IdTipoReferencia = 3;
                cliente.RepLegalObligadoSolidario.Nombre = TxtNombreRepLegalOS.Text;
                cliente.RepLegalObligadoSolidario.Ap_Paterno = TxtApPaternoRepLegalOS.Text;
                cliente.RepLegalObligadoSolidario.Ap_Materno = TxtApMaternoRepLegalOS.Text;
                cliente.RepLegalObligadoSolidario.email = TxtEmailRepLegalOS.Text;
                cliente.RepLegalObligadoSolidario.Fecha_Adicion = DateTime.Now;
                cliente.RepLegalObligadoSolidario.AdicionadoPor = Session["UserName"].ToString();
                cliente.RepLegalObligadoSolidario.Estatus = 1;
                cliente.RepLegalObligadoSolidario.Telefono_Local = TxtTelefonoRLOS.Text;

                var poderRepLegalObligadoSolidario = new CLI_Referencias_Notariales
                {
                    IdTipoReferencia = 4,
                    Numero_Escritura = TxtNoEscrituraPN.Text,
                    Fecha_Escritura = Convert.ToDateTime(TxtFechaEscrituraPN.Text),
                    Nombre_Notario = TxtNombreNotarioPN.Text,
                    Estado = int.Parse(DDXEstadoPN.SelectedValue),
                    Municipio = int.Parse(DDXMunicipioPN.SelectedValue),
                    Numero_Notaria = TxtNotariaPN.Text
                };
                cliente.ReferenciasNotariales.Add(poderRepLegalObligadoSolidario);

                var actaConsObligadoSolidario = new CLI_Referencias_Notariales
                {
                    IdTipoReferencia = 5,
                    Numero_Escritura = TxtNumeroEscrituraAC.Text,
                    Fecha_Escritura = Convert.ToDateTime(TxtFechaAC.Text),
                    Nombre_Notario = TxtNombreNotarioAC.Text,
                    Estado = int.Parse(DDXEstadoAC.SelectedValue),
                    Municipio = int.Parse(DDXMunicipioAC.SelectedValue),
                    Numero_Notaria = TxtNotariaAC.Text
                };
                cliente.ReferenciasNotariales.Add(actaConsObligadoSolidario);
            }

            var poderRepresentanteLegal = new CLI_Referencias_Notariales
            {
                IdTipoReferencia = 6,
                Numero_Escritura = TxtNumeroEscrituraPNCliente.Text,
               Fecha_Escritura = Convert.ToDateTime(TxtPNCliente.Text),
                Nombre_Notario = TxtNomNotarioPNCliente.Text,
                Estado = int.Parse(DDXEstadoPNRL.SelectedValue),
                Municipio = int.Parse(DDXMunicipioPNRL.SelectedValue),
                Numero_Notaria = TxtNotariaPNRL.Text
            };
            cliente.ReferenciasNotariales.Add(poderRepresentanteLegal);

            var actaConstitutiva = new CLI_Referencias_Notariales
            {
                IdTipoReferencia = 7,
                Numero_Escritura = TxtNoEscrituraClienteAC.Text,
                Fecha_Escritura = Convert.ToDateTime(TxtFechaClienteAC.Text),
                Nombre_Notario = TxtNomNotarioClienteAC.Text,
                Estado = int.Parse(DDXEstadoClienteAC.SelectedValue),
                Municipio = int.Parse(DDXMunicipipClienteAC.SelectedValue),
                Numero_Notaria = TxtNotariaClienteAC.Text
            };
            cliente.ReferenciasNotariales.Add(actaConstitutiva);

            var actaMatrimonio = new CLI_Referencias_Notariales
            {
                IdTipoReferencia = 8,
                Numero_Escritura = TxtNumeroActaMat.Text,
                Nombre_Notario = TxtNombreConyuge.Text,
                Numero_Notaria = TxtRegistroCivil.Text
            };
            cliente.ReferenciasNotariales.Add(actaMatrimonio);

            return cliente;
        }

        public bool GuardaInfoCompCliente()
        {
            var cliente = ObtenInfoComplementariaCliente();
            var datosInfoCompCliente = SolicitudCreditoAcciones.ActualizaInfoComplementaria(cliente);
            return datosInfoCompCliente != null;
        }

        protected void CargaDatosCliente()
        {
            var cliente = SolicitudCreditoAcciones.ObtenClienteComplejo(Convert.ToInt32(Session["idCliente"]));

            TxtNombreRepLegal.Text = cliente.RepresentanteLegal.Nombre;
            TxtApPaternoRepLegal.Text = cliente.RepresentanteLegal.Ap_Paterno;
            TxtApMaternoRepLegal.Text = cliente.RepresentanteLegal.Ap_Materno;
            TxtEmailRepLegal.Text = cliente.RepresentanteLegal.email;
            TxtTelefonoRepLegal.Text = cliente.RepresentanteLegal.Telefono_Local;

            if (cliente.DatosCliente.Cve_Tipo_Sociedad == 1)
            {
                RBListTipoPersona.SelectedValue = cliente.ObligadoSolidario.Cve_Tipo_Sociedad.ToString();
                TxtNombrePF.Text = cliente.ObligadoSolidario.Nombre;
                TxtApPaterno.Text = cliente.ObligadoSolidario.Ap_Paterno;
                TxtApMaterno.Text = cliente.ObligadoSolidario.Ap_Materno;
                DDXSexo.SelectedValue = cliente.ObligadoSolidario.Genero.ToString();
                //TxtFechaNacimietoOS.Text = cliente.ObligadoSolidario.Fec_Nacimiento.ToString();
                TxtRFCOS.Text = cliente.ObligadoSolidario.RFC;
                TxtCURPOS.Text = cliente.ObligadoSolidario.CURP;
                TxtTelefonoOS.Text = cliente.ObligadoSolidario.Telefono_Local;

                var direccion = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 3);

                if (direccion != null)
                {
                    DDXColoniaOS.SelectedValue = direccion.CVE_CP.ToString();
                    TxtCPOS.Text = direccion.CP;
                    DDXEstadoOS.SelectedValue = direccion.Cve_Estado.ToString();
                    DDXMunicipioOS.SelectedValue = direccion.Cve_Deleg_Municipio.ToString();
                    TxtCalleOS.Text = direccion.Calle;
                    TxtNoExteriorOS.Text = direccion.Num_Ext;
                    TxtTelefonoOS.Text = direccion.Telefono_Local;
                }
            }

            if (cliente.DatosCliente.Cve_Tipo_Sociedad == 2)
            {
                TxtRazonSocialOS.Text = cliente.ObligadoSolidario.Razon_Social;
                RBListTipoPersona.SelectedValue = cliente.ObligadoSolidario.Cve_Tipo_Sociedad.ToString();

                TxtNombreRepLegalOS.Text = cliente.RepLegalObligadoSolidario.Nombre;
                TxtApPaternoRepLegalOS.Text = cliente.RepLegalObligadoSolidario.Ap_Paterno;
                TxtApMaternoRepLegalOS.Text = cliente.RepLegalObligadoSolidario.Ap_Materno;
                TxtEmailRepLegalOS.Text = cliente.RepLegalObligadoSolidario.email;
                TxtTelefonoRLOS.Text = cliente.RepresentanteLegal.Telefono_Local;

                var poderRepLegalObligadoSolidario =
                    cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 4);

                if (poderRepLegalObligadoSolidario != null)
                {
                    TxtNoEscrituraPN.Text = poderRepLegalObligadoSolidario.Numero_Escritura;
                    //TxtFechaEscrituraPN.Text = poderRepLegalObligadoSolidario.Fecha_Escritura.ToString();
                    TxtNombreNotarioPN.Text = poderRepLegalObligadoSolidario.Nombre_Notario;
                    DDXEstadoPN.SelectedValue = poderRepLegalObligadoSolidario.Estado.ToString();
                    DDXMunicipioPN.SelectedValue = poderRepLegalObligadoSolidario.Municipio.ToString();
                    TxtNotariaPN.Text = poderRepLegalObligadoSolidario.Numero_Notaria;
                }

                var actaConsObligadoSolidario =
                    cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 5);

                if (actaConsObligadoSolidario != null)
                {
                    TxtNumeroEscrituraAC.Text = actaConsObligadoSolidario.Numero_Escritura;
                    //  TxtFechaAC.Text = actaConsObligadoSolidario.Fecha_Escritura.ToString();
                    //  TxtNombreNotarioAC.Text = actaConsObligadoSolidario.Nombre_Notario;
                    DDXEstadoAC.SelectedValue = actaConsObligadoSolidario.Estado.ToString();
                    DDXMunicipioAC.SelectedValue = actaConsObligadoSolidario.Municipio.ToString();
                    //  TxtNotariaAC.Text = actaConsObligadoSolidario.Numero_Notaria;
                }
            }

            var poderRepresentanteLegal = cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 6);

            if (poderRepresentanteLegal != null)
            {
                TxtNumeroEscrituraPNCliente.Text = poderRepresentanteLegal.Numero_Escritura;
                //TxtPNCliente.Text = poderRepresentanteLegal.Fecha_Escritura.ToString();
                TxtNomNotarioPNCliente.Text = poderRepresentanteLegal.Nombre_Notario;
                DDXEstadoPNRL.SelectedValue = poderRepresentanteLegal.Estado.ToString();
                DDXMunicipioPNRL.SelectedValue = poderRepresentanteLegal.Municipio.ToString();
                TxtNotariaPNRL.Text = poderRepresentanteLegal.Numero_Notaria;
            }

            var actaConstitutiva = cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 7);

            if (actaConstitutiva != null)
            {
                TxtNoEscrituraClienteAC.Text = actaConstitutiva.Numero_Escritura;
                //TxtFechaClienteAC.Text = actaConstitutiva.Fecha_Escritura.ToString();
                TxtNomNotarioClienteAC.Text = actaConstitutiva.Nombre_Notario;
                DDXEstadoClienteAC.SelectedValue = actaConstitutiva.Estado.ToString();
                DDXMunicipipClienteAC.SelectedValue = actaConstitutiva.Municipio.ToString();
                TxtNotariaClienteAC.Text = actaConstitutiva.Numero_Notaria;
            }

            var actaMatrimonio = cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 8);

            if (actaMatrimonio != null)
            {
                TxtNumeroActaMat.Text = actaMatrimonio.Numero_Escritura;
                TxtNombreConyuge.Text = actaMatrimonio.Nombre_Notario;
                TxtRegistroCivil.Text = actaMatrimonio.Numero_Notaria;
            }
        }

        #endregion

       
    }
}
