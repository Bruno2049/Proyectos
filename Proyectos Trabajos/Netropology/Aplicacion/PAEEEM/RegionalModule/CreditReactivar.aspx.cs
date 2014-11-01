using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PAEEEM.LogicaNegocios.Credito;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.ReactivarCredito;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.Captcha;
using PAEEEM.AccesoDatos.SolicitudCredito;


namespace PAEEEM.RegionalModule
{
    public partial class CreditReactivar : System.Web.UI.Page
    {
        public List<DATOS_REACTIVACION> LstDatReacti
        {
            set
            {
                ViewState["LstDat_Reactivar"] = value;
            }
            get
            {
                return (List<DATOS_REACTIVACION>)ViewState["LstDat_Reactivar"];
            }
        }

        protected void Next()
        {
            var paginaActual = AspNetPager.CurrentPageIndex;
            var tamanioPagina = AspNetPager.PageSize;


            gvReactivar.DataSource = LstDatReacti.FindAll(
                    me =>
                        me.Rownum >= (((paginaActual - 1) * tamanioPagina) + 1) &&
                        me.Rownum <= (paginaActual * tamanioPagina));

            gvReactivar.DataBind();
        }

        private void BindRegionZona()
        {
            DDXRegion.DataSource = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catRegion();
            DDXRegion.DataTextField = "Dx_Nombre_Region";
            DDXRegion.DataValueField = "Cve_Region";
            DDXRegion.DataBind();

            DDXRegion.Items.Insert(0, new ListItem("Seleccione", "0"));
            DDXRegion.SelectedIndex = 0;

            DDXZona.Items.Insert(0, new ListItem("Seleccione región", "0"));
          
        }

        protected void Page_Load(object sender, EventArgs e)
        {
         if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }
            
            if (IsPostBack) return;
            TxtNoRPU.Attributes.Add("onKeyUp", "Validarfiltros()");
            TxtNoCredito.Attributes.Add("onKeyUp", "Validarfiltros()");
            TxtCliente.Attributes.Add("onKeyUp", "Validarfiltros()");
            TxtNomComercial.Attributes.Add("onKeyUp", "Validarfiltros()");

            BindRegionZona();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LstDatReacti = Reactivacion.ClassInstance.ObtenerDatos(TxtNoRPU.Text, TxtNoCredito.Text, TxtCliente.Text, TxtNomComercial.Text,int.Parse(DDXRegion.SelectedValue), int.Parse(DDXZona.SelectedValue));
            if (LstDatReacti.Count > 0)
            {
                gvReactivar.DataSource = LstDatReacti;
                gvReactivar.DataBind();
                var rownum = 1;
                foreach (var creditsReactivasiones in LstDatReacti)
                {
                    creditsReactivasiones.Rownum = rownum;
                    rownum++;
                }

                AspNetPager.RecordCount = LstDatReacti.Count;

            }
            else
            {
                RadWindowManager1.RadAlert("No se encontraron coincidencias con los criterios ingresados", 300, 150, "Reactivación", null);
                gvReactivar.DataSource = LstDatReacti;
                gvReactivar.DataBind();
                var rownum = 1;

                foreach (var creditsReactivasiones in LstDatReacti)
                {
                    creditsReactivasiones.Rownum = rownum;
                    rownum++;
                }

                AspNetPager.RecordCount = LstDatReacti.Count;
            }
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Next();
            }
        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < gvReactivar.Rows.Count; i++)
            {
                //si esta seleccionado
                if (((CheckBox) gvReactivar.Rows[i].FindControl("ckbSelect")).Checked != true) continue;

                var cre = DatosRequeridos.ObtienePorId(gvReactivar.Rows[i].Cells[1].Text);
                var numreac=cre.No_Reactivaciones == null ? 0 : cre.No_Reactivaciones;
                int no_reactivaciones = VariablesGlobales.ObtienePorIdValor(1, 17);
                if (numreac < no_reactivaciones)
                {
                    //si cuenta con saldo suficiente el sistema
                    var saldo = VariablesGlobales.Obtienesaldo(1);


                    if (cre.Monto_Solicitado != null && (decimal)cre.Monto_Solicitado < saldo)
                    {
                        //si no_mop no es valido mayor a 4
                        if (cre.No_MOP > 4 && cre.No_MOP != null)
                        {
                            RadWindowManager1.RadAlert("MOP no válido, pasarán al estatus “MOP no válido", 300, 150, "MOP No Válido", null);

                            Reactivacion.NumMop(gvReactivar.Rows[i].Cells[1].Text, Convert.ToString(Session["UserName"]));

                            #region actualiza gridview
                            LstDatReacti = Reactivacion.ClassInstance.ObtenerDatos(TxtNoRPU.Text, TxtNoCredito.Text, TxtCliente.Text, TxtNomComercial.Text, int.Parse(DDXRegion.SelectedValue), int.Parse(DDXZona.SelectedValue));
                            gvReactivar.DataSource = LstDatReacti;
                            gvReactivar.DataBind();
                            var rownum = 1;

                            foreach (var creditsReactivasiones in LstDatReacti)
                            {
                                creditsReactivasiones.Rownum = rownum;
                                rownum++;
                            }

                            AspNetPager.RecordCount = LstDatReacti.Count;

                            #endregion
                        }
                        //no_mop valido
                        else
                        {

                            var consultPermitidas = VariablesGlobales.ObtienePorIdValor(3, 17);

                            //si el número de consultas crediticias es mayor a la variable global 
                            if ((cre.No_Consultas_Crediticias ?? 0) > consultPermitidas)
                            {
                                RadWindowManager1.RadAlert(
                                    "Esta solicitud ya cuenta con el Número (" + consultPermitidas +
                                    ") permitido de Consultas Crediticias por lo que NO se REACTIVARÁ", 300, 150,
                                    "Consultas Crediticias", null);
                            }
                            //si es menor a 3
                            else
                            {
                                /////trama
                                var estado = "";
                                var validator = new CodeValidator();

                                string error;
                                string[] causa;
                                var lstAmortizaciones = new CreCredito().ObtenAmortizacionesCredito(gvReactivar.Rows[i].Cells[1].Text);
                                var fechaAntes = lstAmortizaciones.Find(me => me.No_Pago == 1 && me.No_Credito == gvReactivar.Rows[i].Cells[1].Text).Dt_Fecha;

                                if (validator.ValidateServiceCode(gvReactivar.Rows[i].Cells[0].Text, out error, ref estado, out causa))
                                {
                                    //var parseo = validator.ParseoTrama;
                                    //var periodoConsumoFinal = parseo.ComplexParseo.InformacionGeneral.Conceptos.Find(p => p.Id == 159).Dato;
                                    //////////////////////////////////////////////
                                    //si el motivo de Cancelacion fue POR 30 DIAS DE INACTIVIDAD

                                    if (gvReactivar.Rows[i].Cells[6].Text == @"POR 30 DIAS DE INACTIVIDAD" && cre.Fecha_Autorizado == null && cre.Fecha_En_revision == null && cre.Fecha_Por_entregar == null && cre.Fecha_Pendiente != null)
                                    {
                                        if (Reactivacion.Masd30DiasSnActividad(gvReactivar.Rows[i].Cells[1].Text, Convert.ToString(Session["UserName"])))
                                        {
                                            LogicaNegocios.SolicitudCredito.SolicitudCreditoAcciones.DecrementarrFonDisponibleEincentivo((decimal)cre.Monto_Solicitado, gvReactivar.Rows[i].Cells[1].Text);
                                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                                Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                                                "REACTIVACION", gvReactivar.Rows[i].Cells[1].Text,
                                                "", "", "", "");

                                            #region actualiza gridview
                                            LstDatReacti = Reactivacion.ClassInstance.ObtenerDatos(TxtNoRPU.Text, TxtNoCredito.Text, TxtCliente.Text, TxtNomComercial.Text, int.Parse(DDXRegion.SelectedValue), int.Parse(DDXZona.SelectedValue));
                                            gvReactivar.DataSource = LstDatReacti;
                                            gvReactivar.DataBind();
                                            var rownum = 1;

                                            foreach (var creditsReactivasiones in LstDatReacti)
                                            {
                                                creditsReactivasiones.Rownum = rownum;
                                                rownum++;
                                            }

                                            AspNetPager.RecordCount = LstDatReacti.Count;
                                            #endregion



                                            RadWindowManager1.RadAlert("Reactivación Realizada.", 300, 150, "Reactivacion", null);
                                        }
                                        else
                                        {
                                            RadWindowManager1.RadAlert("Ocurrió un error al Reactivar.", 300, 150, "Reactivacion", null);
                                        }

                                    }
                                    //Si el motivo de cancelación fue VENCIMIENTO DE PRIMER FECHA DE AMORTIZACI&#211;N || SOLICITO REGION -ZONA
                                    else
                                    {
                                        //Si  tiene fecha de consulta crediticia
                                        if (cre.Fecha_Consulta != null)
                                        {
                                            var diasUltimaConsulta = VariablesGlobales.ObtienePorId(2, 17);

                                            var dif = DateTime.Now - (DateTime)cre.Fecha_Consulta;
                                            var t = dif.Days;

                                            //si la consulta es mayor a la Variable Global  
                                            if (t > Convert.ToInt16(diasUltimaConsulta.VALOR))
                                            {
                                                RadWindowManager1.RadConfirm("Para Reactivar esta Solicitud se requiere BORRAR la Consulta Crediticia.¿Esta seguro de realizar la Reactivación?.", "confirmCallBackFn", 300, 100, null, "Consulta Crediticia");
                                            }
                                            //Si la consulta es Menor a la variable global 
                                            else
                                            {

                                                if (Reactivacion.Menor30Dias(gvReactivar.Rows[i].Cells[1].Text, Convert.ToString(Session["UserName"])))
                                                {
                                                    LogicaNegocios.SolicitudCredito.SolicitudCreditoAcciones.DecrementarrFonDisponibleEincentivo((decimal)cre.Monto_Solicitado, gvReactivar.Rows[i].Cells[1].Text);
                                                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]), Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO", "REACTIVACION", gvReactivar.Rows[i].Cells[1].Text, "", "", "", "");


                                                    #region actualiza gridview

                                                    LstDatReacti = Reactivacion.ClassInstance.ObtenerDatos(TxtNoRPU.Text, TxtNoCredito.Text, TxtCliente.Text, TxtNomComercial.Text, int.Parse(DDXRegion.SelectedValue), int.Parse(DDXZona.SelectedValue));
                                                    gvReactivar.DataSource = LstDatReacti;
                                                    gvReactivar.DataBind();
                                                    var rownum = 1;

                                                    foreach (var creditsReactivasiones in LstDatReacti)
                                                    {
                                                        creditsReactivasiones.Rownum = rownum;
                                                        rownum++;
                                                    }

                                                    AspNetPager.RecordCount = LstDatReacti.Count;

                                                    #endregion


                                                    RadWindowManager1.RadAlert("Reactivación Realizada.", 300, 150, "Reactivacion", null);
                                                }
                                                else
                                                {
                                                    RadWindowManager1.RadAlert("Ocurrió un error al Reactivar", 300, 150, "Reactivacion", null);
                                                }
                                            }
                                        }
                                        //Si no tiene consulta crediticia 
                                        else
                                        {

                                            if (Reactivacion.SinConsulCrediticia(gvReactivar.Rows[i].Cells[1].Text, Convert.ToString(Session["UserName"])))
                                            {
                                                LogicaNegocios.SolicitudCredito.SolicitudCreditoAcciones.DecrementarrFonDisponibleEincentivo((decimal)cre.Monto_Solicitado, gvReactivar.Rows[i].Cells[1].Text);
                                                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]), Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO", "REACTIVACION", gvReactivar.Rows[i].Cells[1].Text, "", "", "", "");

                                                #region actualiza gridview

                                                LstDatReacti = Reactivacion.ClassInstance.ObtenerDatos(TxtNoRPU.Text, TxtNoCredito.Text, TxtCliente.Text, TxtNomComercial.Text, int.Parse(DDXRegion.SelectedValue), int.Parse(DDXZona.SelectedValue));
                                                gvReactivar.DataSource = LstDatReacti;
                                                gvReactivar.DataBind();
                                                var rownum = 1;

                                                foreach (var creditsReactivasiones in LstDatReacti)
                                                {
                                                    creditsReactivasiones.Rownum = rownum;
                                                    rownum++;
                                                }

                                                AspNetPager.RecordCount = LstDatReacti.Count;

                                                #endregion


                                                RadWindowManager1.RadAlert("Reactivación Realizada.", 300, 150, "Reactivacion", null);
                                            }
                                            else
                                            {
                                                RadWindowManager1.RadAlert("Ocurrió un error al Reactivar", 300, 150, "Reactivacion", null);
                                            }
                                        }
                                    }

                                    var lstAmortizacionesDes = new CreCredito().ObtenAmortizacionesCredito(gvReactivar.Rows[i].Cells[1].Text);
                                    var fechaDespues = lstAmortizacionesDes.Find(me => me.No_Pago == 1 && me.No_Credito == gvReactivar.Rows[i].Cells[1].Text).Dt_Fecha;
                                    if (fechaAntes != fechaDespues)
                                    {
                                        RadWindowManager1.RadAlert("'DEBERÁ REIMPRIMIR DOCUMENTACION NECESARIA'", 300, 150, "Reactivacion", null);
                                    }

                                    //////////////////////////////////////////
                                }
                                else
                                {
                                    RadWindowManager1.RadAlert(error + " " + causa, 300, 150, "Reactivación", null);
                                }
                            }
                        }
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("El Programa NO cuenta con el Saldo Suficiente para REACTIVAR la Solicitud.", 300, 150, "Reactivación", null);
                    }
                }
                else
                {
                    RadWindowManager1.RadAlert("El Programa encontro que se acaba de reactivar este Num. de credito favor de Actualizar la Pagina.", 300, 150, "Reactivación", null);
                }

            }
        }

        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            var user = (US_USUARIOModel) Session["UserInfo"];

            for (var i = 0; i < gvReactivar.Rows.Count; i++)
            {
                if (!((CheckBox) gvReactivar.Rows[i].FindControl("ckbSelect")).Checked) continue;
                /////trama
                var estado = "";
                var validator = new CodeValidator();

                string error;
                string[] causa;

                if (validator.ValidateServiceCode(gvReactivar.Rows[i].Cells[0].Text, out error, ref estado, out causa))
                {
                    var parseo = validator.ParseoTrama;
                    var periodoConsumoFinal = parseo.ComplexParseo.InformacionGeneral.Conceptos.Find(p => p.Id == 159).Dato;



                    if (!Reactivacion.Mayor30Dias(gvReactivar.Rows[i].Cells[1].Text, user.Nombre_Usuario)) continue;

                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]), Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO", "REACTIVACION", gvReactivar.Rows[i].Cells[1].Text, "", "", "", "");

                    var cre = DatosRequeridos.ObtienePorId(gvReactivar.Rows[i].Cells[1].Text);

                    if (cre.Monto_Solicitado != null)
                        LogicaNegocios.SolicitudCredito.SolicitudCreditoAcciones.DecrementarrFonDisponibleEincentivo((decimal)cre.Monto_Solicitado, cre.No_Credito);

                    RadWindowManager1.RadAlert("Reactivación Realizada.       'DEBERÁ REIMPRIMIR DOCUMENTACION NECESARIA'", 300, 150, "Reactivacion", null);
                }
                else
                {
                    RadWindowManager1.RadAlert(error + " " + causa, 300, 150, "Reactivacion", null);
                }
               

            }

            #region actualiza gridview
       
            LstDatReacti = Reactivacion.ClassInstance.ObtenerDatos(TxtNoRPU.Text, TxtNoCredito.Text, TxtCliente.Text, TxtNomComercial.Text, int.Parse(DDXRegion.SelectedValue), int.Parse(DDXZona.SelectedValue));
            gvReactivar.DataSource = LstDatReacti;
            gvReactivar.DataBind();
            var rownum = 1;

            foreach (var creditsReactivasiones in LstDatReacti)
            {
                creditsReactivasiones.Rownum = rownum;
                rownum++;
            }

            AspNetPager.RecordCount = LstDatReacti.Count;

            #endregion

            

        }

        protected void DDXZona_TextChanged(object sender, EventArgs e)
        {
            if (DDXZona.SelectedValue != "0" || DDXRegion.SelectedValue != "0" || TxtNoRPU.Text != "" || TxtNoCredito.Text != "" || TxtCliente.Text != "" || TxtNomComercial.Text != "")
            {
                btnBuscar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
            }
        }

        protected void DDXRegion_TextChanged(object sender, EventArgs e)
        {
            if (DDXZona.SelectedValue != "0" || DDXRegion.SelectedValue != "0" || TxtNoRPU.Text != "" || TxtNoCredito.Text != "" || TxtCliente.Text != "" || TxtNomComercial.Text != "")
            {
                btnBuscar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }

        protected void ckbSelect_CheckedChanged(object sender, EventArgs e)
        {
            btnReactivar.Enabled = true;
            var chk = (CheckBox)sender;
            var row = (GridViewRow)chk.Parent.Parent;

            if (chk.Checked == false)
            {
                foreach (GridViewRow item in gvReactivar.Rows)
                {
                    gvReactivar.Rows[item.DataItemIndex].Enabled = true;
                    btnReactivar.Enabled = false;
                }
            }
            else
            {
                foreach (GridViewRow item in gvReactivar.Rows)
                {
                    if (row.DataItemIndex == item.DataItemIndex)
                    {
                        gvReactivar.Rows[item.DataItemIndex].Enabled = true;
                        btnReactivar.Enabled = true;
                    }
                    else
                    {
                        gvReactivar.Rows[item.DataItemIndex].Enabled = false;
                    }
                }
            }
        }

        protected void DDXRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDXRegion.SelectedValue=="0")
            {
                  DDXZona.Items.Clear();
                  DDXZona.Items.Insert(0, new ListItem("Seleccione región", "0"));
                  DDXZona.SelectedIndex = 0;
            }
            else
            {
                DDXZona.DataSource = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZona().FindAll(o => o.Cve_Region == int.Parse(DDXRegion.SelectedValue));
                DDXZona.DataTextField = "Dx_Nombre_Zona";
                DDXZona.DataValueField = "Cve_Zona";
                DDXZona.DataBind();

                DDXZona.Items.Insert(0, new ListItem("Seleccione", "0"));
                DDXZona.SelectedIndex = 0;
            }

        }
    }
}