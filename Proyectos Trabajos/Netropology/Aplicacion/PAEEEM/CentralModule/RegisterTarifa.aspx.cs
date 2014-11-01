using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.LogicaNegocios.Tarifas;

namespace PAEEEM.CentralModule
{
    public partial class RegisterTarifa : Page
    {
        #region Propiedades

        private List<DetalleTarifa> _listTarifa1 = new List<DetalleTarifa>();

        public string Fecha
        {
            get { return ViewState["Fecha"] == null ? "" : ViewState["Fecha"].ToString(); }
            set { ViewState["Fecha"] = value; }
        }

        public int TipoTarifa
        {
            get { return ViewState["TipoTarifa"] == null ? 0 : int.Parse(ViewState["TipoTarifa"].ToString()); }
            set { ViewState["TipoTarifa"] = value; }
        }

        public string UserName
        {
            get { return ViewState["UserName"] == null ? string.Empty : ViewState["UserName"].ToString(); }
            set { ViewState["UserName"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            UserName = Session["UserName"].ToString();

            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                var numMes = DateTime.Now.Month;
                Fecha = MesLetra(numMes) + '-' + DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
                lblMesAnioActual.Text = Fecha;
                LlenaTarifa();
                //divSalir.Visible = true;
            }
        }

        #region METODOS

        private void BindGridViewData(GridView tipoTarifa, int tarifa)
        {
            _listTarifa1 = RegistrarTarifas.RetornaDatosTarifas(tarifa, Fecha, -1);
            if (_listTarifa1.Count == 0)
            {
                _listTarifa1.Add(new DetalleTarifa());
            }
            tipoTarifa.DataSource = _listTarifa1;
            tipoTarifa.DataBind();
        }

        private void LlenaTarifa()
        {
            var dtTarifa = RegistrarTarifas.TiposTarifa();
            if (dtTarifa == null) return;
            drpTipoTarifa.DataSource = dtTarifa;
            drpTipoTarifa.DataTextField = "Dx_Tarifa";
            drpTipoTarifa.DataValueField = "Cve_Tarifa";
            drpTipoTarifa.DataBind();
            drpTipoTarifa.Items.Insert(0, "Seleccione");
            //drpTipoTarifa.SelectedIndex = 0;
            drpTipoTarifa.SelectedValue = "Seleccione";
        }

        protected string MesLetra(int numMes)
        {
            const string mes = "ENERO";
            switch (numMes)
            {
                case 1:
                    return "ENERO";
                case 2:
                    return "FEBRERO";
                case 3:
                    return "MARZO";
                case 4:
                    return "ABRIL";
                case 5:
                    return "MAYO";
                case 6:
                    return "JUNIO";
                case 7:
                    return "JULIO";
                case 8:
                    return "AGOSTO";
                case 9:
                    return "SEPTIEMBRE";
                case 10:
                    return "OCTUBRE";
                case 11:
                    return "NOVIEMBRE";
                case 12:
                    return "DICIEMBRE";
                default:
                    return mes;
            }
        }

        #endregion

        #region EVENTOS

        protected void dprTipoTarifa_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoTarifa = (drpTipoTarifa.SelectedIndex != 0 && drpTipoTarifa.SelectedIndex != -1)
                ? Convert.ToInt32(drpTipoTarifa.SelectedValue)
                : -1;
            var diaMes = DateTime.Now.Day;

            switch (TipoTarifa)
            {
                case 1: //Tarifa 02
                    BindGridViewData(gvTarifa_02, TipoTarifa);
                    //if (diaMes >= 4)
                    //    gvTarifa_02.Columns[6].Visible = false;
                    div_Tarifa_02.Visible = true;
                    div_Tarifa_03.Visible = false;
                    div_Tarifa_OM.Visible = false;
                    div_Tarifa_HM.Visible = false;
                    //divSalir.Visible = true;
                    break;
                case 2: //Tarifa 03
                    BindGridViewData(gvTarifa_03, TipoTarifa);
                    //if (diaMes >= 4)
                    //    gvTarifa_03.Columns[4].Visible = false;
                    div_Tarifa_03.Visible = true;
                    div_Tarifa_02.Visible = false;
                    div_Tarifa_OM.Visible = false;
                    div_Tarifa_HM.Visible = false;
                    break;
                case 3: //Tarifa OM
                    BindGridViewData(gvTarifa_OM, TipoTarifa);
                    //if (diaMes >= 4)
                    //    gvTarifa_OM.Columns[6].Visible = false;
                    div_Tarifa_OM.Visible = true;
                    div_Tarifa_03.Visible = false;
                    div_Tarifa_02.Visible = false;
                    div_Tarifa_HM.Visible = false;
                    break;
                case 4: //Tarifa HM
                    BindGridViewData(gvTarifa_HM, TipoTarifa);
                    //if (diaMes >= 4)
                    //    gvTarifa_HM.Columns[9].Visible = false;
                    div_Tarifa_HM.Visible = true;
                    div_Tarifa_03.Visible = false;
                    div_Tarifa_OM.Visible = false;
                    div_Tarifa_02.Visible = false;
                    break;
            }
        }

        protected void EditCustomer(object sender, GridViewEditEventArgs e)
        {
            gvTarifa_02.EditIndex = e.NewEditIndex;
            BindGridViewData(gvTarifa_02, TipoTarifa);
        }

        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTarifa_02.EditIndex = -1;
            BindGridViewData(gvTarifa_02, TipoTarifa);
        }

        protected void GuardaDatosTarifa02(object sender, GridViewUpdateEventArgs e)
        {
            var idTarifa = ((Label) gvTarifa_02.Rows[e.RowIndex].FindControl("lblID_TARIFA_02")).Text;
            var cargoFijo = ((TextBox) gvTarifa_02.Rows[e.RowIndex].FindControl("txtCargoFijo")).Text;
            var cargoFirst50KWh = ((TextBox) gvTarifa_02.Rows[e.RowIndex].FindControl("txtCargoFirst50KWh")).Text;
            var cargoMayor50KWh = ((TextBox) gvTarifa_02.Rows[e.RowIndex].FindControl("txtCargoMayor50KWh")).Text;
            var cargoKWhAdicional = ((TextBox) gvTarifa_02.Rows[e.RowIndex].FindControl("txtCargoKWhAdicional")).Text;

            if (cargoFijo != "" && cargoFirst50KWh != "" && cargoKWhAdicional != "" && cargoMayor50KWh != "")
            {
                var validacion = RegistrarTarifas.ValidaDatosTarifa02(Convert.ToInt16(idTarifa),
                    Convert.ToDouble(cargoFijo), Convert.ToDouble(cargoFirst50KWh),
                    Convert.ToDouble(cargoMayor50KWh), Convert.ToDouble(cargoKWhAdicional), Fecha, UserName,
                    Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), 0, 0);

                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page), "AlertInform",
                    "alert('" + validacion + "');",
                    true);

                gvTarifa_02.EditIndex = -1;
                BindGridViewData(gvTarifa_02, TipoTarifa);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page), "AlertInform",
                    "alert('Introduzca todos los valores de la tarifa');",
                    true);
            }

        }

        protected void EditCustomer03(object sender, GridViewEditEventArgs e)
        {
            gvTarifa_03.EditIndex = e.NewEditIndex;
            BindGridViewData(gvTarifa_03, TipoTarifa);
        }

        protected void CancelEdit03(object sender, GridViewCancelEditEventArgs e)
        {
            gvTarifa_03.EditIndex = -1;
            BindGridViewData(gvTarifa_03, TipoTarifa);
        }

        protected void GuardaDatosTarifa03(object sender, GridViewUpdateEventArgs e)
        {
            var idTarifa = ((Label) gvTarifa_03.Rows[e.RowIndex].FindControl("lblID_TARIFA_03")).Text;
            var cargoDemandaMax = ((TextBox) gvTarifa_03.Rows[e.RowIndex].FindControl("txtCargoDemandaMax")).Text;
            var cargoAdicional = ((TextBox) gvTarifa_03.Rows[e.RowIndex].FindControl("txtCargoAdicional")).Text;

            if (cargoDemandaMax != "" && cargoAdicional != "")
            {
                var validacion = RegistrarTarifas.ValidaDatosTarifa03(Convert.ToInt16(idTarifa),
                    Convert.ToDouble(cargoDemandaMax), Convert.ToDouble(cargoAdicional), Fecha, UserName,
                    Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), 0, 0);

                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page), "AlertInform",
                    "alert('" + validacion + "');",
                    true);

                gvTarifa_03.EditIndex = -1;
                BindGridViewData(gvTarifa_03, TipoTarifa);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page), "AlertInform",
                    "alert('Introduzca todos los valores de la tarifa');",
                    true);
            }

        }

        protected void EditCustomerOm(object sender, GridViewEditEventArgs e)
        {
            gvTarifa_OM.EditIndex = e.NewEditIndex;
            BindGridViewData(gvTarifa_OM, TipoTarifa);
        }

        protected void CancelEditOm(object sender, GridViewCancelEditEventArgs e)
        {
            gvTarifa_OM.EditIndex = -1;
            BindGridViewData(gvTarifa_OM, TipoTarifa);
        }

        protected void GuardaDatosTarifaOm(object sender, GridViewUpdateEventArgs e)
        {
            var idTarifaOm = ((Label) gvTarifa_OM.Rows[e.RowIndex].FindControl("lblIdTarifaOm")).Text;
            var idRegionOm = ((Label) gvTarifa_OM.Rows[e.RowIndex].FindControl("lblIdRegionOm")).Text;
            var cargoKwDemandaMax = ((TextBox) gvTarifa_OM.Rows[e.RowIndex].FindControl("txtcargoKWDemandaMax")).Text;
            var cargoKWhDemandaConsumida =
                ((TextBox) gvTarifa_OM.Rows[e.RowIndex].FindControl("txtCargoKWhDemandaConsumida")).Text;

            if (cargoKwDemandaMax != "" && cargoKWhDemandaConsumida != "")
            {
                var validacion = RegistrarTarifas.ValidaDatosTarifaOm(Convert.ToInt16(idTarifaOm),
                    Convert.ToInt16(idRegionOm),
                    Convert.ToDouble(cargoKwDemandaMax), Convert.ToDouble(cargoKWhDemandaConsumida), Fecha, UserName,
                    Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), 0, 0);

                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page), "AlertInform",
                    "alert('" + validacion + "');",
                    true);

                gvTarifa_OM.EditIndex = -1;
                BindGridViewData(gvTarifa_OM, TipoTarifa);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page), "AlertInform",
                    "alert('Introduzca todos los valores de la tarifa');",
                    true);
            }

        }

        protected void EditCustomerHm(object sender, GridViewEditEventArgs e)
        {
            gvTarifa_HM.EditIndex = e.NewEditIndex;
            BindGridViewData(gvTarifa_HM, TipoTarifa);
        }

        protected void CancelEditHm(object sender, GridViewCancelEditEventArgs e)
        {
            gvTarifa_HM.EditIndex = -1;
            BindGridViewData(gvTarifa_HM, TipoTarifa);
        }

        protected void GuardaDatosTarifaHm(object sender, GridViewUpdateEventArgs e)
        {
            var idTarifaHm = ((Label) gvTarifa_HM.Rows[e.RowIndex].FindControl("lblIdTarifaHm")).Text;
            var idRegionHm = ((Label) gvTarifa_HM.Rows[e.RowIndex].FindControl("lblIdRegionHM")).Text;
            var cargoKwDemandaFac = ((TextBox) gvTarifa_HM.Rows[e.RowIndex].FindControl("txtcargoKWDemandaFac")).Text;
            var cargoKWhEnergiaBase =
                ((TextBox) gvTarifa_HM.Rows[e.RowIndex].FindControl("txtCargoKWhEnergiaBase")).Text;
            var cargoKWhEnergiaIntermedia =
                ((TextBox) gvTarifa_HM.Rows[e.RowIndex].FindControl("txtCargoKWhEnergiaIntermedia")).Text;
            var cargoKWhEnergiaPunta =
                ((TextBox) gvTarifa_HM.Rows[e.RowIndex].FindControl("txtCargoKWhEnergiaPunta")).Text;

            if (cargoKwDemandaFac != "" && cargoKWhEnergiaPunta != "" && cargoKWhEnergiaIntermedia != "" &&
                cargoKWhEnergiaBase != "")
            {
                var validacion = RegistrarTarifas.ValidaDatosTarifaHm(Convert.ToInt16(idTarifaHm),
                    Convert.ToInt16(idRegionHm),
                    Convert.ToDouble(cargoKwDemandaFac), Convert.ToDouble(cargoKWhEnergiaBase),
                    Convert.ToDouble(cargoKWhEnergiaIntermedia),
                    Convert.ToDouble(cargoKWhEnergiaPunta), Fecha, UserName,
                    Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), 0, 0);

                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page), "AlertInform",
                    "alert('" + validacion + "');",
                    true);

                gvTarifa_HM.EditIndex = -1;
                BindGridViewData(gvTarifa_HM, TipoTarifa);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page), "AlertInform",
                    "alert('Introduzca todos los valores de la tarifa');",
                    true);
            }

        }

        protected void btnSalir_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        #endregion
    }
}