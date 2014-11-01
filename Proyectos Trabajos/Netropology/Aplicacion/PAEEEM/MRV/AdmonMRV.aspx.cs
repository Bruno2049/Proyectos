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
using Telerik.Web.UI;

namespace PAEEEM.MRV
{
    public partial class AdmonMRV : System.Web.UI.Page
    {
        #region Atributos

        protected string NumeroCredito
        {
            get { return ViewState["NumeroCredito"] as string; }
            set { ViewState["NumeroCredito"] = value; }
        }

        private List<Medicion> LstMedicion
        {
            get
            {
                return ViewState["LstMedicion"] == null
                           ? new List<Medicion>()
                           : ViewState["LstMedicion"] as List<Medicion>;
            }
            set { ViewState["LstMedicion"] = value; }
        }

        private List<Cuestionario> LstCuestionario
        {
            get
            {
                return ViewState["LstCuestionario"] == null
                           ? new List<Cuestionario>()
                           : ViewState["LstCuestionario"] as List<Cuestionario>;
            }
            set { ViewState["LstCuestionario"] = value; }
        }

        #endregion

        #region Carga Inicial

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
                {
                    var idCredito =
                        System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Token"]));
                    NumeroCredito = idCredito;
                    CargaInicial();
                }
                
                //NumeroCredito = "PAEEEMDA01A18051";
                
            }
        }

        protected void CargaInicial()
        {
            wucEncabezadoMRV1.Inicializa(NumeroCredito);
            LLenaGridMediciones();
            LLenaGridCuestionarios();
        }

        #endregion

        #region Eventos Grid

        protected void rgMediciones_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgMediciones.DataSource = LstMedicion;
        }

        protected void rgMediciones_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var id = int.Parse(dataItem.GetDataKeyValue("IdMedicion").ToString());

                var estatus = LstMedicion.First(me => me.IdMedicion == id).Estatus;
                var img = (ImageButton)dataItem["colEdit"].Controls[0];
                img.Visible = !estatus;
            }
        }

        protected void rgMediciones_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var item = (GridDataItem)e.Item;
                var medicion = LstMedicion.FirstOrDefault(me => me.IdMedicion == int.Parse(item.GetDataKeyValue("IdMedicion").ToString()));

                if (medicion != null)
                {
                    Response.Redirect("CapturaMRV.aspx?Token=" +
                                      Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)) +
                                      "&Med=" + medicion.IdMedicion.ToString() + "&Acc=2");
                }
            }
            else if (e.CommandName == "View")
            {
                var item = (GridDataItem)e.Item;
                var medicion = LstMedicion.FirstOrDefault(me => me.IdMedicion == int.Parse(item.GetDataKeyValue("IdMedicion").ToString()));

                if (medicion != null)
                {
                    Response.Redirect("CapturaMRV.aspx?Token=" +
                                      Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)) +
                                      "&Med=" + medicion.IdMedicion.ToString() + "&Acc=3");
                }
            }
            else
            {
                return;
            }
        }

        protected void rgCuestionarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgCuestionarios.DataSource = LstCuestionario;
        }

        protected void rgCuestionarios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var id = int.Parse(dataItem.GetDataKeyValue("IdCuestionario").ToString());

                var estatus = LstCuestionario.First(me => me.IdCuestionario == id).Estatus;
                var img = (ImageButton)dataItem["colEdit"].Controls[0];
                img.Visible = !estatus;
            }
        }

        protected void rgCuestionarios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var item = (GridDataItem)e.Item;
                var cuestionario =
                    LstCuestionario.FirstOrDefault(
                        me => me.IdCuestionario == int.Parse(item.GetDataKeyValue("IdCuestionario").ToString()));

                if (cuestionario != null)
                {
                    Response.Redirect("CuestionarioMRV.aspx?Token=" +
                                      Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)) +
                                      "&Que=" + cuestionario.IdCuestionario.ToString() + "&Acc=2");
                }
            }
            else if (e.CommandName == "View")
            {
                var item = (GridDataItem)e.Item;

                var cuestionario =
                    LstCuestionario.FirstOrDefault(
                        me => me.IdCuestionario == int.Parse(item.GetDataKeyValue("IdCuestionario").ToString()));

                if (cuestionario != null)
                {
                    Response.Redirect("CuestionarioMRV.aspx?Token=" +
                                      Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)) +
                                      "&Que=" + cuestionario.IdCuestionario.ToString() + "&Acc=3");
                }
            }
            else
            {
                return;
            }
        }

        #endregion

        #region Eventos Botones

        protected void RadBtnAgregaMedicion_Click(object sender, EventArgs e)
        {
            try
            {
                CreaMedicion();
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert(ex.Message, 300, 150, "Mediciones", null);
            }            
        }

        protected void RadBtnAgregaCuestionario_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuestionarioMRV.aspx?Token=" +
                                          Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)) +
                                          "&Acc=1");
            //try
            //{
            //    var lstCuestionariosProceso = new CuestionarioLN().ObtenListaCuestionarios(NumeroCredito, false);

            //    if (lstCuestionariosProceso.Count > 0)
            //    {
            //        rwmVentana.RadAlert("La solicitud tiene captura de cuestionarios en proceso", 300, 150, "Cuestionarios", null);
            //    }
            //    else
            //    {
            //        DateTime ultimaFechaCuestionario;

            //        if (LstCuestionario.Count > 0)
            //            ultimaFechaCuestionario = LstCuestionario.LastOrDefault().FechaCuestionario.Date;
            //        else
            //            ultimaFechaCuestionario = Convert.ToDateTime("01/01/2000");

            //        if (ValidaMes(ultimaFechaCuestionario))
            //        {
            //            Response.Redirect("CuestionarioMRV.aspx?Token=" +
            //                              Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)) +
            //                              "&Acc=1");
            //        }
            //        else
            //        {
            //            rwmVentana.RadAlert("La solicitud ya tiene capturada un cuestionario para el mes en curso", 300, 150, "Cuestionarios", null);
            //        }
            //    }
            //}
            //catch (Exception)
            //{
                
            //}
        }

        protected void RadBtnConsultaConsumo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultaConsumo.aspx?Token=" +
                                      Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)));
        }

        protected void RadBtnSalirAdmon_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
            //Response.Redirect("~/SupplierModule/CreditMonitor.aspx");
        }

        #endregion

        #region Metodos Protegidos

        protected void LLenaGridMediciones()
        {
            var lstMediciones = new MedicionesLN().ObtenListaMedicionesCredito(NumeroCredito);

            if (lstMediciones != null)
            {
                LstMedicion = lstMediciones;
            }
        }

        protected void LLenaGridCuestionarios()
        {
            var lstCuestionarios = new CuestionarioLN().ObtenListaCuestionariosCredito(NumeroCredito);

            if (lstCuestionarios != null)
            {
                LstCuestionario = lstCuestionarios;
            }
        }

        protected void CreaMedicion()
        {
            var lstMedicionesProceso = new MedicionesLN().ObtenMedicionesCredito(NumeroCredito, false);


            if (lstMedicionesProceso != null && lstMedicionesProceso.Count > 0)
            {
                rwmVentana.RadAlert("La solicitud tiene captura de mediciones en proceso", 300, 150, "Mediciones", null);
            }
            else
            {
                DateTime ultimaFechaMedicion;

                if (LstMedicion.Count > 0)
                    ultimaFechaMedicion = LstMedicion.LastOrDefault().FechaMedicion.Date;
                else
                    ultimaFechaMedicion = Convert.ToDateTime("01/01/2000");

                if (ValidaMes(ultimaFechaMedicion))
                {
                    var usuario = ((US_USUARIOModel) Session["UserInfo"]).Nombre_Usuario;
                    var medicion = new MRV_MEDICION
                        {
                            No_Credito = NumeroCredito,
                            FechaMedicion = DateTime.Now.Date,
                            Estatus = false,
                            AdicionadoPor = usuario
                        };

                    var newMedicion = new MedicionesLN().GuardaMedicion(medicion);

                    if (newMedicion != null)
                    {
                        new MedicionesLN().GuardaGruposMedicion(NumeroCredito, newMedicion.IdMedicion, usuario);
                        Response.Redirect("CapturaMRV.aspx?Token=" +
                                          Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(NumeroCredito)) +
                                          "&Med=" + medicion.IdMedicion.ToString() + "&Acc=1");
                    }
                }
                else
                {
                    rwmVentana.RadAlert("La solicitud ya tiene capturada una medición para el mes en curso", 300, 150, "Mediciones", null);
                }
            }

        }

        protected bool ValidaMes(DateTime ultimaFecha)
        {
            var fechaActual = DateTime.Now.Date;

            if (ultimaFecha.Year == fechaActual.Year && ultimaFecha.Month == fechaActual.Month)
            {
                return false;
            }

            return true;
        }

        #endregion        
        
    }
}