using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.Entidades.MRV;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.MRV;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using Telerik.Web.UI;

namespace PAEEEM.MRV
{
    public partial class wucCuestionario : System.Web.UI.UserControl
    {
        #region Atributos

        protected int IdCuestionario
        {
            get { return (int)ViewState["IdCuestionario"]; }
            set { ViewState["IdCuestionario"] = value; }
        }

        protected string NumeroCredito
        {
            get { return ViewState["NumeroCredito"] as string; }
            set { ViewState["NumeroCredito"] = value; }
        }

        private List<EquipoAltaEficienciaMrv> LstEquipos
        {
            get
            {
                return ViewState["LstEquipos"] == null
                           ? new List<EquipoAltaEficienciaMrv>()
                           : ViewState["LstEquipos"] as List<EquipoAltaEficienciaMrv>;
            }
            set { ViewState["LstEquipos"] = value; }
        }

        #endregion

        #region Carga Inicial

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Inicializa(string idCredito)
        {
            NumeroCredito = idCredito;
            IdCuestionario = 0;
            LLenaHorariosEquipos();
            LLenaHorariosNegocio();
            LLenaPreguntaCuestionario();
            LLenaCatalogoMediciones();
            LstEquipos = new List<EquipoAltaEficienciaMrv>();
            Panel1.Visible = Panel2.Visible = false;
        }

        public void InicializaConsultaEdit(string idCredito, int idCuestionario, bool esEdicion)
        {
            NumeroCredito = idCredito;
            IdCuestionario = idCuestionario;
            HidIdCuestionario.Value = IdCuestionario.ToString();
            LLenaHorariosEquipos();
            LLenaHorariosNegocio();
            LLenaPreguntaCuestionario();
            LLenaCatalogoMediciones();
            LLenaEquiposEnOperacion();
            LstEquipos = new List<EquipoAltaEficienciaMrv>();
            CargaDatosCuestionario();

            Panel1.Enabled = esEdicion;
            RadBtnGuardaHorariosEquipos.Visible = esEdicion;
            RadBtnFinalizar.Enabled = esEdicion;
            RadBtnGuardar.Enabled = esEdicion;
            RadBtnListo.Visible = false;
            rgEquipos.Enabled = false;
            RadCmbCuestionarioSegumiento.Enabled = false;
            RadCmbMedicionesDisponibles.Enabled = false;
            //RadTxtFechaMedicionExPostCuest.Enabled = esEdicion;
            RadTxtCometGeneralesCuest.Enabled = esEdicion;
            UploadedArchivoMedicionCuest.Enabled = esEdicion;

            DeshabilitaControlesHorario(esEdicion);
            RadBtnSalir2.Visible = false;
        }

        #endregion

        #region Eventos radgrid

        protected void rgEquipos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var id = int.Parse(dataItem.GetDataKeyValue("IdEquipo").ToString());

                if (LstEquipos.First(me => me.IdEquipo == id).EnOperacion)
                {
                    var check = ((CheckBox) dataItem.FindControl("chkOperacion"));
                    check.Checked = true;
                }
            }
        }

        protected void rgEquipos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgEquipos.DataSource = LstEquipos;
        }

        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
            bool checkHeader = true;

            var editedItem = (sender as CheckBox).NamingContainer as GridDataItem;
            var idEquipo = int.Parse(editedItem.GetDataKeyValue("IdEquipo").ToString());
            LstEquipos.Find(p => p.IdEquipo == idEquipo).MoverEnOperacion((sender as CheckBox).Checked);

            
            foreach (GridDataItem dataItem in rgEquipos.MasterTableView.Items)
            {
                if (!(dataItem.FindControl("chkOperacion") as CheckBox).Checked)
                {
                    checkHeader = false;
                    break;
                }
            }

            GridHeaderItem headerItem = rgEquipos.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            (headerItem.FindControl("headerChkbox") as CheckBox).Checked = checkHeader;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            
            foreach (GridDataItem dataItem in rgEquipos.MasterTableView.Items)
            {
                (dataItem.FindControl("chkOperacion") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
                var idEquipo = int.Parse(dataItem.GetDataKeyValue("IdEquipo").ToString());
                LstEquipos.Find(p => p.IdEquipo == idEquipo).MoverEnOperacion(headerCheckBox.Checked);
            }
        }
        #endregion

        #region catalogos

        protected void LLenaHorariosEquipos()
        {
            var listHorarios = CatalogosSolicitud.ObtenHorariosTrabajo();
            RadCmbLunes.DataSource = listHorarios;
            RadCmbLunes.DataValueField = "CveValorCatalogo";
            RadCmbLunes.DataTextField = "DescripcionCatalogo";
            RadCmbLunes.DataBind();
            RadCmbLunes.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbLunes.SelectedIndex = 0;

            RadCmbMartes.DataSource = listHorarios;
            RadCmbMartes.DataValueField = "CveValorCatalogo";
            RadCmbMartes.DataTextField = "DescripcionCatalogo";
            RadCmbMartes.DataBind();
            RadCmbMartes.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbMartes.SelectedIndex = 0;

            RadCmbMiercoles.DataSource = listHorarios;
            RadCmbMiercoles.DataValueField = "CveValorCatalogo";
            RadCmbMiercoles.DataTextField = "DescripcionCatalogo";
            RadCmbMiercoles.DataBind();
            RadCmbMiercoles.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbMiercoles.SelectedIndex = 0;

            RadCmbJueves.DataSource = listHorarios;
            RadCmbJueves.DataValueField = "CveValorCatalogo";
            RadCmbJueves.DataTextField = "DescripcionCatalogo";
            RadCmbJueves.DataBind();
            RadCmbJueves.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbJueves.SelectedIndex = 0;

            RadCmbViernes.DataSource = listHorarios;
            RadCmbViernes.DataValueField = "CveValorCatalogo";
            RadCmbViernes.DataTextField = "DescripcionCatalogo";
            RadCmbViernes.DataBind();
            RadCmbViernes.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbViernes.SelectedIndex = 0;

            RadCmbSabado.DataSource = listHorarios;
            RadCmbSabado.DataValueField = "CveValorCatalogo";
            RadCmbSabado.DataTextField = "DescripcionCatalogo";
            RadCmbSabado.DataBind();
            RadCmbSabado.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbSabado.SelectedIndex = 0;

            RadCmbDomingo.DataSource = listHorarios;
            RadCmbDomingo.DataValueField = "CveValorCatalogo";
            RadCmbDomingo.DataTextField = "DescripcionCatalogo";
            RadCmbDomingo.DataBind();
            RadCmbDomingo.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbDomingo.SelectedIndex = 0;
        }

        protected void LLenaHorariosNegocio()
        {
            var listHorarios = CatalogosSolicitud.ObtenHorariosTrabajo();
            RadCmbLunesNeg.DataSource = listHorarios;
            RadCmbLunesNeg.DataValueField = "CveValorCatalogo";
            RadCmbLunesNeg.DataTextField = "DescripcionCatalogo";
            RadCmbLunesNeg.DataBind();
            RadCmbLunesNeg.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbLunesNeg.SelectedIndex = 0;

            RadCmbMartesNeg.DataSource = listHorarios;
            RadCmbMartesNeg.DataValueField = "CveValorCatalogo";
            RadCmbMartesNeg.DataTextField = "DescripcionCatalogo";
            RadCmbMartesNeg.DataBind();
            RadCmbMartesNeg.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbMartesNeg.SelectedIndex = 0;

            RadCmbMiercolesNeg.DataSource = listHorarios;
            RadCmbMiercolesNeg.DataValueField = "CveValorCatalogo";
            RadCmbMiercolesNeg.DataTextField = "DescripcionCatalogo";
            RadCmbMiercolesNeg.DataBind();
            RadCmbMiercolesNeg.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbMiercolesNeg.SelectedIndex = 0;

            RadCmbJuevesNeg.DataSource = listHorarios;
            RadCmbJuevesNeg.DataValueField = "CveValorCatalogo";
            RadCmbJuevesNeg.DataTextField = "DescripcionCatalogo";
            RadCmbJuevesNeg.DataBind();
            RadCmbJuevesNeg.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbJuevesNeg.SelectedIndex = 0;

            RadCmbViernesNeg.DataSource = listHorarios;
            RadCmbViernesNeg.DataValueField = "CveValorCatalogo";
            RadCmbViernesNeg.DataTextField = "DescripcionCatalogo";
            RadCmbViernesNeg.DataBind();
            RadCmbViernesNeg.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbViernesNeg.SelectedIndex = 0;

            RadCmbSabadoNeg.DataSource = listHorarios;
            RadCmbSabadoNeg.DataValueField = "CveValorCatalogo";
            RadCmbSabadoNeg.DataTextField = "DescripcionCatalogo";
            RadCmbSabadoNeg.DataBind();
            RadCmbSabadoNeg.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbSabadoNeg.SelectedIndex = 0;

            RadCmbDomingoNeg.DataSource = listHorarios;
            RadCmbDomingoNeg.DataValueField = "CveValorCatalogo";
            RadCmbDomingoNeg.DataTextField = "DescripcionCatalogo";
            RadCmbDomingoNeg.DataBind();
            RadCmbDomingoNeg.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbDomingoNeg.SelectedIndex = 0;
        }

        protected void LLenaCatalogoMediciones()
        {
            var lstMediciones = new CuestionarioLN().ObtenCataglogoMediciones(NumeroCredito);
            RadCmbMedicionesDisponibles.DataSource = lstMediciones;
            RadCmbMedicionesDisponibles.DataValueField = "IdMedicion";
            RadCmbMedicionesDisponibles.DataTextField = "DescripcionMedicion";
            RadCmbMedicionesDisponibles.DataBind();

            RadCmbMedicionesDisponibles.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbMedicionesDisponibles.SelectedIndex = 0;
        }

        protected void LLenaPreguntaCuestionario()
        {
            RadCmbCuestionarioSegumiento.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbCuestionarioSegumiento.Items.Insert(1, new RadComboBoxItem("Si", "1"));
            RadCmbCuestionarioSegumiento.Items.Insert(2, new RadComboBoxItem("No", "0"));
        }

        protected void LLenaEquipos()
        {
            LstEquipos = new CuestionarioLN().ObtenEquiposAlta(NumeroCredito, IdCuestionario);
            rgEquipos.DataSource = LstEquipos;
            rgEquipos.DataBind();

            bool checkHeader = true;

            foreach (GridDataItem dataItem in rgEquipos.MasterTableView.Items)
            {
                if (!(dataItem.FindControl("chkOperacion") as CheckBox).Checked)
                {
                    checkHeader = false;
                    break;
                }
            }

            GridHeaderItem headerItem = rgEquipos.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            (headerItem.FindControl("headerChkbox") as CheckBox).Checked = checkHeader;
        }

        protected void LLenaEquiposEnOperacion()
        {
            var lstEquiposEnOperacion = new CuestionarioLN().ObtenEquiposCuestionario(IdCuestionario);
            RadCmbEquipos.DataSource = lstEquiposEnOperacion;
            RadCmbEquipos.DataValueField = "IdEquipoCuestionario";
            RadCmbEquipos.DataTextField = "DescripcionEquipo";
            RadCmbEquipos.DataBind();

            RadCmbEquipos.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            RadCmbEquipos.SelectedIndex = 0;
        }

        #endregion        

        #region Metodos Protegidos

        protected void CargaDatosCuestionario()
        {
            var cuestionario = new CuestionarioLN().ObtenCuestionario(IdCuestionario);

            if (cuestionario != null)
            {
                //RadCmbMedicionesDisponibles.SelectedValue = cuestionario.IdMedicion.ToString();
                RadCmbCuestionarioSegumiento.SelectedValue = (bool)cuestionario.CuestionarioRealizado ? "1" : "0";
                RadTxtCometGeneralesCuest.Text = cuestionario.Comentarios;

                if ((bool) cuestionario.CuestionarioRealizado)
                {
                    rgEquipos.Visible = true;
                    Panel1.Visible = true;
                    Panel2.Visible = true;

                    LLenaEquipos();

                    var lstOperacionTotal = new CuestionarioLN().ObtenOperacionTotal(IdCuestionario);

                    if (lstOperacionTotal.Count > 0)
                    {
                        CargaHorariosEquipo((int)lstOperacionTotal.First().IdEquipoCuestionario);
                        CargaHorariosNegocio();
                    }

                    CargaImagenOK();
                }
                else
                {
                    rgEquipos.Visible = true;
                    Panel1.Visible = true;
                    Panel2.Visible = true;
                    RadTxtComentarios.Text = cuestionario.Comentarios;
                }
            }
        }

        protected void CreaCuestionario()
        {
            var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;
            var cuestionario = new MRV_CUESTIONARIO();
            //cuestionario.IdMedicion = int.Parse(RadCmbMedicionesDisponibles.SelectedValue);
            cuestionario.FechaCuestionario = DateTime.Now.Date;
            cuestionario.No_Credito = NumeroCredito;
            cuestionario.CuestionarioRealizado = RadCmbCuestionarioSegumiento.SelectedValue == "1";
            cuestionario.Comentarios = RadTxtComentarios.Text;
            cuestionario.Estatus = false;
            cuestionario.FechaAdicion = DateTime.Now.Date;
            cuestionario.AdicionadoPor = usuario;

            var newCuestionario = new CuestionarioLN().GuardaCuestionario(cuestionario);

            if (newCuestionario != null)
            {
                IdCuestionario = newCuestionario.IdCuestionario;
                HidIdCuestionario.Value = IdCuestionario.ToString();
                new CuestionarioLN().GuardaEquiposCuestionario(LstEquipos, newCuestionario.IdCuestionario, "daniel");
            }
        }

        protected void CargaHorariosEquipo(int idEquipo)
        {
            var lstHorariosEquipo = new CuestionarioLN().ObtenHorariosOperacionEquipo(IdCuestionario, idEquipo);

            if (lstHorariosEquipo.Count > 0)
            {
                foreach (var mrvHorariosOperacion in lstHorariosEquipo)
                {
                    switch (mrvHorariosOperacion.ID_DIA_OPERACION)
                    {
                        case 1:
                            ChkLunes.Checked = true;
                            RadTxtLunes.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbLunes.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 2:
                            ChkMartes.Checked = true;
                            RadTxtMartes.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbMartes.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 3:
                            ChkMiercoles.Checked = true;
                            RadTxtMiercoles.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbMiercoles.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 4:
                            ChkJueves.Checked = true;
                            RadTxtJueves.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbJueves.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 5:
                            ChkViernes.Checked = true;
                            RadTxtViernes.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbViernes.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 6:
                            ChkSabado.Checked = true;
                            RadTxtSabado.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbSabado.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 7:
                            ChkSabado.Checked = true;
                            RadTxtSabado.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbSabado.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                    }
                }

                var totalHoras = new CuestionarioLN().ObtenTotalHorasEquipo(IdCuestionario, idEquipo);

                if (totalHoras != null)
                {
                    TxtHorasSemana.Text = totalHoras.HorasSemana.ToString();
                    RadTxtSemanasAnio.Text = totalHoras.SemanasAnio.ToString();
                    TxtHorasAnio.Text = totalHoras.HorasAnio.ToString();
                }

                RadCmbEquipos.SelectedValue = idEquipo.ToString();
            }
        }

        protected void CargaHorariosNegocio()
        {
            var lstHorariosEquipo = new CuestionarioLN().ObtenHorariosNegocio(IdCuestionario, 1);

            if (lstHorariosEquipo.Count > 0)
            {
                foreach (var mrvHorariosOperacion in lstHorariosEquipo)
                {
                    switch (mrvHorariosOperacion.ID_DIA_OPERACION)
                    {
                        case 1:
                            ChkLunesNeg.Checked = true;
                            RadTxtLunesNeg.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbLunesNeg.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 2:
                            ChkMartesNeg.Checked = true;
                            RadTxtMartesNeg.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbMartesNeg.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 3:
                            ChkMiercolesNeg.Checked = true;
                            RadTxtMiercolesNeg.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbMiercolesNeg.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 4:
                            ChkJuevesNeg.Checked = true;
                            RadTxtJuevesNeg.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbJuevesNeg.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 5:
                            ChkViernesNeg.Checked = true;
                            RadTxtViernesNeg.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbViernesNeg.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 6:
                            ChkSabadoNeg.Checked = true;
                            RadTxtSabadoNeg.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbSabadoNeg.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                        case 7:
                            ChkSabadoNeg.Checked = true;
                            RadTxtSabadoNeg.Text = mrvHorariosOperacion.Horas_Laborales.Value.ToString("#.##");
                            RadCmbSabadoNeg.SelectedValue = mrvHorariosOperacion.ValorHorario;
                            break;
                    }
                }

                var totalHoras = new CuestionarioLN().ObtenTotalHorasNegocio(IdCuestionario, 1);

                if (totalHoras != null)
                {
                    TxtHorasSemanaNeg.Text = totalHoras.HorasSemana.ToString();
                    RadTxtSemanasAnioNeg.Text = totalHoras.SemanasAnio.ToString();
                    TxtHorasAnioNeg.Text = totalHoras.HorasAnio.ToString();
                }
            }
        }

        protected void InsertaHorariosEquipo()
        {
            var lstHorarios = new List<MRV_HORARIOS_OPERACION>();
            MRV_HORARIOS_OPERACION horario = null;
            var idEquipoCuestionario = int.Parse(RadCmbEquipos.SelectedValue);
            var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;

            if (ChkLunes.Checked)
            {
                if (RadCmbLunes.SelectedIndex != 0)
                {
                    horario = new MRV_HORARIOS_OPERACION
                        {
                            IdEquipoCuestionario = idEquipoCuestionario,
                            IdCuestionario = IdCuestionario,
                            IDTIPOHORARIO = 2,
                            ID_DIA_OPERACION = 1,
                            Hora_Inicio = RadCmbLunes.SelectedItem.Text,
                            Horas_Laborales = decimal.Parse(RadTxtLunes.Text),
                            ValorHorario = RadCmbLunes.SelectedValue,
                            Estatus = true,
                            FechaAdicion = DateTime.Now.Date,
                            AdicionadoPor = usuario
                        };
                    lstHorarios.Add(horario);
                }
            }

            if (ChkMartes.Checked)
            {
                if (RadCmbMartes.SelectedIndex != 0)
                {
                    horario = new MRV_HORARIOS_OPERACION
                        {
                            IdEquipoCuestionario = idEquipoCuestionario,
                            IdCuestionario = IdCuestionario,
                            IDTIPOHORARIO = 2,
                            ID_DIA_OPERACION = 2,
                            Hora_Inicio = RadCmbMartes.SelectedItem.Text,
                            Horas_Laborales = decimal.Parse(RadTxtMartes.Text),
                            ValorHorario = RadCmbMartes.SelectedValue,
                            Estatus = true,
                            FechaAdicion = DateTime.Now.Date,
                            AdicionadoPor = usuario
                        };
                    lstHorarios.Add(horario);
                }
            }

            if (ChkMiercoles.Checked)
            {
                if (RadCmbMiercoles.SelectedIndex != 0)
                {
                    horario = new MRV_HORARIOS_OPERACION
                        {
                            IdEquipoCuestionario = idEquipoCuestionario,
                            IdCuestionario = IdCuestionario,
                            IDTIPOHORARIO = 2,
                            ID_DIA_OPERACION = 3,
                            Hora_Inicio = RadCmbMiercoles.SelectedItem.Text,
                            Horas_Laborales = decimal.Parse(RadTxtMiercoles.Text),
                            ValorHorario = RadCmbMiercoles.SelectedValue,
                            Estatus = true,
                            FechaAdicion = DateTime.Now.Date,
                            AdicionadoPor = usuario
                        };
                    lstHorarios.Add(horario);
                }
            }

            if (ChkJueves.Checked)
            {
                if (RadCmbJueves.SelectedIndex != 0)
                {
                    horario = new MRV_HORARIOS_OPERACION
                        {
                            IdEquipoCuestionario = idEquipoCuestionario,
                            IdCuestionario = IdCuestionario,
                            IDTIPOHORARIO = 2,
                            ID_DIA_OPERACION = 4,
                            Hora_Inicio = RadCmbJueves.SelectedItem.Text,
                            Horas_Laborales = decimal.Parse(RadTxtJueves.Text),
                            ValorHorario = RadCmbJueves.SelectedValue,
                            Estatus = true,
                            FechaAdicion = DateTime.Now.Date,
                            AdicionadoPor = usuario
                        };
                    lstHorarios.Add(horario);
                }
            }

            if (ChkViernes.Checked)
            {
                if (RadCmbViernes.SelectedIndex != 0)
                {
                    horario = new MRV_HORARIOS_OPERACION
                        {
                            IdEquipoCuestionario = idEquipoCuestionario,
                            IdCuestionario = IdCuestionario,
                            IDTIPOHORARIO = 2,
                            ID_DIA_OPERACION = 5,
                            Hora_Inicio = RadCmbViernes.SelectedItem.Text,
                            Horas_Laborales = decimal.Parse(RadTxtViernes.Text),
                            ValorHorario = RadCmbViernes.SelectedValue,
                            Estatus = true,
                            FechaAdicion = DateTime.Now.Date,
                            AdicionadoPor = usuario
                        };
                    lstHorarios.Add(horario);
                }
            }

            if (ChkSabado.Checked)
            {
                if (RadCmbSabado.SelectedIndex != 0)
                {
                    horario = new MRV_HORARIOS_OPERACION
                        {
                            IdEquipoCuestionario = idEquipoCuestionario,
                            IdCuestionario = IdCuestionario,
                            IDTIPOHORARIO = 2,
                            ID_DIA_OPERACION = 6,
                            Hora_Inicio = RadCmbSabado.SelectedItem.Text,
                            Horas_Laborales = decimal.Parse(RadTxtSabado.Text),
                            ValorHorario = RadCmbSabado.SelectedValue,
                            Estatus = true,
                            FechaAdicion = DateTime.Now.Date,
                            AdicionadoPor = usuario
                        };
                    lstHorarios.Add(horario);
                }
            }

            if (ChkDomingo.Checked)
            {
                if (RadCmbDomingo.SelectedIndex != 0)
                {
                    horario = new MRV_HORARIOS_OPERACION
                        {
                            IdEquipoCuestionario = idEquipoCuestionario,
                            IdCuestionario = IdCuestionario,
                            IDTIPOHORARIO = 2,
                            ID_DIA_OPERACION = 7,
                            Hora_Inicio = RadCmbDomingo.SelectedItem.Text,
                            Horas_Laborales = decimal.Parse(RadTxtDomingo.Text),
                            ValorHorario = RadCmbDomingo.SelectedValue,
                            Estatus = true,
                            FechaAdicion = DateTime.Now.Date,
                            AdicionadoPor = usuario
                        };
                    lstHorarios.Add(horario);
                }
            }

            new CuestionarioLN().GuardaHorariosEquipo(lstHorarios, idEquipoCuestionario, IdCuestionario);

            var horasOperacionTotal = new MRV_HORAS_OPERACION_TOTAL
                {
                    IdEquipoCuestionario = idEquipoCuestionario,
                    IdCuestionario = IdCuestionario,
                    IDTIPOHORARIO = 2,
                    HorasSemana = double.Parse(TxtHorasSemana.Text),
                    SemanasAnio = double.Parse(RadTxtSemanasAnio.Text),
                    HorasAnio = double.Parse(TxtHorasAnio.Text),
                    Estatus = true,
                    FechaAdicion = DateTime.Now.Date,
                    AdicionadoPor = usuario
                };

            new CuestionarioLN().GuardaOperacionTotalEquipo(horasOperacionTotal);
        }

        protected void GuardaHorarioNegocio()
        {
            try
            {

                var lstHorarios = new List<MRV_HORARIOS_OPERACION>();
                MRV_HORARIOS_OPERACION horario = null;
                var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;

                if (ChkLunesNeg.Checked)
                {
                    if (RadCmbLunesNeg.SelectedIndex != 0)
                    {
                        horario = new MRV_HORARIOS_OPERACION
                            {
                                IdCuestionario = IdCuestionario,
                                IDTIPOHORARIO = 1,
                                ID_DIA_OPERACION = 1,
                                Hora_Inicio = RadCmbLunesNeg.SelectedItem.Text,
                                Horas_Laborales = decimal.Parse(RadTxtLunesNeg.Text),
                                ValorHorario = RadCmbLunesNeg.SelectedValue,
                                Estatus = true,
                                FechaAdicion = DateTime.Now.Date,
                                AdicionadoPor = usuario
                            };
                        lstHorarios.Add(horario);
                    }
                }

                if (ChkMartesNeg.Checked)
                {
                    if (RadCmbMartesNeg.SelectedIndex != 0)
                    {
                        horario = new MRV_HORARIOS_OPERACION
                            {
                                IdCuestionario = IdCuestionario,
                                IDTIPOHORARIO = 1,
                                ID_DIA_OPERACION = 2,
                                Hora_Inicio = RadCmbMartesNeg.SelectedItem.Text,
                                Horas_Laborales = decimal.Parse(RadTxtMartesNeg.Text),
                                ValorHorario = RadCmbMartesNeg.SelectedValue,
                                Estatus = true,
                                FechaAdicion = DateTime.Now.Date,
                                AdicionadoPor = usuario
                            };
                        lstHorarios.Add(horario);
                    }
                }

                if (ChkMiercolesNeg.Checked)
                {
                    if (RadCmbMiercolesNeg.SelectedIndex != 0)
                    {
                        horario = new MRV_HORARIOS_OPERACION
                            {
                                IdCuestionario = IdCuestionario,
                                IDTIPOHORARIO = 1,
                                ID_DIA_OPERACION = 3,
                                Hora_Inicio = RadCmbMiercolesNeg.SelectedItem.Text,
                                Horas_Laborales = decimal.Parse(RadTxtMiercolesNeg.Text),
                                ValorHorario = RadCmbMiercolesNeg.SelectedValue,
                                Estatus = true,
                                FechaAdicion = DateTime.Now.Date,
                                AdicionadoPor = usuario
                            };
                        lstHorarios.Add(horario);
                    }
                }

                if (ChkJuevesNeg.Checked)
                {
                    if (RadCmbJuevesNeg.SelectedIndex != 0)
                    {
                        horario = new MRV_HORARIOS_OPERACION
                            {
                                IdCuestionario = IdCuestionario,
                                IDTIPOHORARIO = 1,
                                ID_DIA_OPERACION = 4,
                                Hora_Inicio = RadCmbJuevesNeg.SelectedItem.Text,
                                Horas_Laborales = decimal.Parse(RadTxtJuevesNeg.Text),
                                ValorHorario = RadCmbJuevesNeg.SelectedValue,
                                Estatus = true,
                                FechaAdicion = DateTime.Now.Date,
                                AdicionadoPor = usuario
                            };
                        lstHorarios.Add(horario);
                    }
                }

                if (ChkViernesNeg.Checked)
                {
                    if (RadCmbViernesNeg.SelectedIndex != 0)
                    {
                        horario = new MRV_HORARIOS_OPERACION
                            {
                                IdCuestionario = IdCuestionario,
                                IDTIPOHORARIO = 1,
                                ID_DIA_OPERACION = 5,
                                Hora_Inicio = RadCmbViernesNeg.SelectedItem.Text,
                                Horas_Laborales = decimal.Parse(RadTxtViernesNeg.Text),
                                ValorHorario = RadCmbViernesNeg.SelectedValue,
                                Estatus = true,
                                FechaAdicion = DateTime.Now.Date,
                                AdicionadoPor = usuario
                            };
                        lstHorarios.Add(horario);
                    }
                }

                if (ChkSabadoNeg.Checked)
                {
                    if (RadCmbSabadoNeg.SelectedIndex != 0)
                    {
                        horario = new MRV_HORARIOS_OPERACION
                            {
                                IdCuestionario = IdCuestionario,
                                IDTIPOHORARIO = 1,
                                ID_DIA_OPERACION = 6,
                                Hora_Inicio = RadCmbSabadoNeg.SelectedItem.Text,
                                Horas_Laborales = decimal.Parse(RadTxtSabadoNeg.Text),
                                ValorHorario = RadCmbSabadoNeg.SelectedValue,
                                Estatus = true,
                                FechaAdicion = DateTime.Now.Date,
                                AdicionadoPor = usuario
                            };
                        lstHorarios.Add(horario);
                    }
                }

                if (ChkDomingoNeg.Checked)
                {
                    if (RadCmbDomingoNeg.SelectedIndex != 0)
                    {
                        horario = new MRV_HORARIOS_OPERACION
                            {
                                IdCuestionario = IdCuestionario,
                                IDTIPOHORARIO = 1,
                                ID_DIA_OPERACION = 7,
                                Hora_Inicio = RadCmbDomingoNeg.SelectedItem.Text,
                                Horas_Laborales = decimal.Parse(RadTxtDomingoNeg.Text),
                                ValorHorario = RadCmbDomingoNeg.SelectedValue,
                                Estatus = true,
                                FechaAdicion = DateTime.Now.Date,
                                AdicionadoPor = usuario
                            };
                        lstHorarios.Add(horario);
                    }
                }

                new CuestionarioLN().GuardaHorariosNegocio(lstHorarios, IdCuestionario, 1);

                var horasOperacionTotal = new MRV_HORAS_OPERACION_TOTAL
                    {
                        IdCuestionario = IdCuestionario,
                        IDTIPOHORARIO = 1,
                        HorasSemana = double.Parse(TxtHorasSemanaNeg.Text),
                        SemanasAnio = double.Parse(RadTxtSemanasAnioNeg.Text),
                        HorasAnio = double.Parse(TxtHorasAnioNeg.Text),
                        Estatus = true,
                        FechaAdicion = DateTime.Now.Date,
                        AdicionadoPor = usuario
                    };

                new CuestionarioLN().GuardaOperacionTotalEquipo(horasOperacionTotal);

                rwmVentana.RadAlert("Se guardó la información de horarios correctamente", 300, 150, "Guarda Horarios Negocio", null);
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert(ex.Message, 300, 150, "Guarda Horarios Negocio", null);
            }
        }

        protected void ActualizaCuestionario(bool estatus)
        {
            try
            {
                var cuestionario = new CuestionarioLN().ObtenCuestionario(IdCuestionario);

                if (cuestionario != null)
                {
                    cuestionario.Comentarios = RadTxtCometGeneralesCuest.Text;
                    cuestionario.Estatus = estatus;
                    var actualiza = new CuestionarioLN().ActualizaCuestionario(cuestionario);
                }
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert(ex.Message, 300, 150, "Actualiza Cuestionario", null);
            }
            
        }

        protected void LimpiaHorariosEquipo()
        {
            ChkLunes.Checked = false;
            ChkMartes.Checked = false;
            ChkMiercoles.Checked = false;
            ChkJueves.Checked = false; 
            ChkViernes.Checked = false;
            ChkSabado.Checked = false;
            ChkDomingo.Checked = false;

            RadCmbLunes.SelectedIndex = 0;
            RadCmbMartes.SelectedIndex = 0;
            RadCmbMiercoles.SelectedIndex = 0;
            RadCmbJueves.SelectedIndex = 0;
            RadCmbViernes.SelectedIndex = 0;
            RadCmbSabado.SelectedIndex = 0;
            RadCmbDomingo.SelectedIndex = 0;

            RadTxtLunes.Text = "";
            RadTxtMartes.Text = "";
            RadTxtMiercoles.Text = "";
            RadTxtJueves.Text = "";
            RadTxtViernes.Text = "";
            RadTxtSabado.Text = "";
            RadTxtDomingo.Text = "";

            TxtHorasSemana.Text = "";
            RadTxtSemanasAnio.Text = "";
            TxtHorasAnio.Text = "";
        }

        protected void CargaImagenOK()
        {
            var foto = new CuestionarioLN().ObtenFotoCuestionario(IdCuestionario);

            if (foto != null)
            {
                imgOkCuestionario.Visible = true;
                imgOkCuestionario.ImageUrl = "~/CentralModule/images/icono_correcto.png";
            }
        }

        #endregion

        #region Eventos Controles

        protected void RadBtnListo_Click(object sender, EventArgs e)
        {
            var cont = rgEquipos.MasterTableView.Items.Cast<GridDataItem>().Count(dataItem => (dataItem.FindControl("chkOperacion") as CheckBox).Checked);

            if (cont > 0)
            {
                CreaCuestionario();

                LLenaEquiposEnOperacion();
                Panel2.Visible = RadCmbCuestionarioSegumiento.SelectedValue == "1";
                RadBtnListo.Visible = false;
                RadBtnSalir2.Visible = false;
            }
            else
            {
                rwmVentana.RadAlert("Debe seleccionar al menos un equipo", 300, 150, "Cuestionario", null);
            }
        }

        protected void RadBtnGuardaHorariosEquipos_Click(object sender, EventArgs e)
        {
            if (RadTxtSemanasAnio.Text != "")
            {
                InsertaHorariosEquipo();
            }
            else
            {
                rwmVentana.RadAlert("Debe capturar el número de semanas del equipo", 300, 150, "Actualiza Cuestionario", null);
                RadTxtSemanasAnio.Focus();
            }
        }

        protected void RadBtnGuardar_Click(object sender, EventArgs e)
        {
            ActualizaCuestionario(false);

            GuardaHorarioNegocio();

            rwmVentana.RadAlert("Se actualizarón los datos correctamente", 300, 150, "Actualiza Cuestionario", null);
        }

        protected void RadCmbCuestionarioSegumiento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbCuestionarioSegumiento.SelectedValue == "1")
            {
                Panel1.Visible = true;
                RadTxtComentarios.Visible = false;

                LLenaEquipos();
            }
            else if (RadCmbCuestionarioSegumiento.SelectedValue == "0")
            {
                Panel1.Visible = false;
                RadTxtComentarios.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
                RadTxtComentarios.Visible = false;
            }
        }

        protected void RadCmbEquipos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbEquipos.SelectedIndex != 0)
            {
                LimpiaHorariosEquipo();
                LLenaHorariosEquipos();
                CargaHorariosEquipo(int.Parse(RadCmbEquipos.SelectedValue));
            }
        }

        protected void UploadedArchivoMedicionCuest_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            if (e.File.FileName != null)
            {
                var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;
                var cuestionarioLn = new CuestionarioLN();
                var foto = new MRV_FOTOS_CUESTIONARIO();
                foto.Nombre = e.File.GetName();
                foto.Extension = e.File.GetExtension();
                foto.Longitud = e.File.ContentLength;
                var b = new byte[e.File.ContentLength];
                e.File.InputStream.Read(b, 0, e.File.ContentLength);

                foto.IdCuestionario = IdCuestionario;
                foto.Foto = b;
                foto.Estatus = true;
                foto.FechaAdicion = DateTime.Now.Date;
                foto.AdicionadoPor = usuario;

                if (cuestionarioLn.GuardaFotoCuestionario(foto))
                {
                    imgOkCuestionario.ImageUrl = "~/CentralModule/images/icono_correcto.png";
                    imgOkCuestionario.Visible = true;
                }
                else
                {
                    imgOkCuestionario.ImageUrl = "~/CentralModule/images/eliminar-icono.png";
                    imgOkCuestionario.Visible = true;
                }

                ClearContents(sender as Control);
            }
            else
            {
                ClearContents(sender as Control);
                rwmVentana.RadAlert("Ocurrió un problema al cargar el archivo", 300, 150, "Actualiza Cuestionario", null);
            }
        }

        private void ClearContents(Control control)
        {
            for (var i = 0; i < Session.Keys.Count; i++)
            {
                if (Session.Keys[i].Contains(control.ClientID))
                {
                    Session.Remove(Session.Keys[i]);
                    break;
                }
            }
        }

        protected void DeshabilitaControlesHorario(bool esEdicion)
        {
            ChkLunes.Enabled = esEdicion;
            ChkMartes.Enabled = esEdicion;
            ChkMiercoles.Enabled = esEdicion;
            ChkJueves.Enabled = esEdicion;
            ChkViernes.Enabled = esEdicion;
            ChkSabado.Enabled = esEdicion;
            ChkDomingo.Enabled = esEdicion;

            RadTxtLunes.Enabled = esEdicion;
            RadTxtMartes.Enabled = esEdicion;
            RadTxtMiercoles.Enabled = esEdicion;
            RadTxtJueves.Enabled = esEdicion;
            RadTxtViernes.Enabled = esEdicion;
            RadTxtSabado.Enabled = esEdicion;
            RadTxtDomingo.Enabled = esEdicion;

            RadCmbLunes.Enabled = esEdicion;
            RadCmbMartes.Enabled = esEdicion;
            RadCmbMiercoles.Enabled = esEdicion;
            RadCmbJueves.Enabled = esEdicion;
            RadCmbViernes.Enabled = esEdicion;
            RadCmbSabado.Enabled = esEdicion;
            RadCmbDomingo.Enabled = esEdicion;

            ChkLunesNeg.Enabled = esEdicion;
            ChkMartesNeg.Enabled = esEdicion;
            ChkMiercolesNeg.Enabled = esEdicion;
            ChkJuevesNeg.Enabled = esEdicion;
            ChkViernesNeg.Enabled = esEdicion;
            ChkSabadoNeg.Enabled = esEdicion;
            ChkDomingoNeg.Enabled = esEdicion;

            RadTxtLunesNeg.Enabled = esEdicion;
            RadTxtMartesNeg.Enabled = esEdicion;
            RadTxtMiercolesNeg.Enabled = esEdicion;
            RadTxtJuevesNeg.Enabled = esEdicion;
            RadTxtViernesNeg.Enabled = esEdicion;
            RadTxtSabadoNeg.Enabled = esEdicion;
            RadTxtDomingoNeg.Enabled = esEdicion;

            RadCmbLunesNeg.Enabled = esEdicion;
            RadCmbMartesNeg.Enabled = esEdicion;
            RadCmbMiercolesNeg.Enabled = esEdicion;
            RadCmbJuevesNeg.Enabled = esEdicion;
            RadCmbViernesNeg.Enabled = esEdicion;
            RadCmbSabadoNeg.Enabled = esEdicion;
            RadCmbDomingoNeg.Enabled = esEdicion;

            RadTxtSemanasAnio.Enabled = esEdicion;
            RadTxtSemanasAnioNeg.Enabled = esEdicion;
        }

        protected void btnRefresh2_Click(object sender, EventArgs e)
        {

        }

        protected void RadBtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdmonMRV.aspx?Token=" +
                                          Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)));
        }

        protected void hidBtnFinalizar_Click(object sender, EventArgs e)
        {
            ActualizaCuestionario(true);

            GuardaHorarioNegocio();

            Response.Redirect("AdmonMRV.aspx?Token=" +
                                          Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)));
        }

        protected void RadBtnFinalizar_Click(object sender, EventArgs e)
        {
            var foto = new CuestionarioLN().ObtenFotoCuestionario(IdCuestionario);

            if (foto != null)
            {
                rwmVentana.RadConfirm(
                    "Al finalizar la captura los datos no se podrán editar. ¿Esta seguro de Finalizar?",
                    "confirmCallBackFn", 300, 100, null, "Finaliza Cuestionario");
            }
            else
            {
                rwmVentana.RadAlert("Debe cargar la imagen del cuestionario", 300, 150, "Actualiza Cuestionario", null);
            }
        }

        #endregion                
        
    }
}