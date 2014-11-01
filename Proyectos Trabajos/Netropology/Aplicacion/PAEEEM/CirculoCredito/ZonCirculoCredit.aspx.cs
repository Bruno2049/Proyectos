using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using PAEEEM.LogicaNegocios.CirculoCredito;
using PAEEEM.Entidades.CirculoCredito;

namespace PAEEEM.CirculoCredito
{
    public partial class ZonCirculoCredit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCBoxs();
                modalPopup.OpenerElementID = btnRechazar.ClientID;
                TexComen.Attributes.Add("onkeyup", "ValidarComentarios(this, 150);");

                RadTxtNoCredit.Attributes.Add("onKeyUp", "Validarfiltros()");
                /////////
                RadCbxEstatus.Items.Insert(0, new RadComboBoxItem("Seleccione"));
                RadCbxEstatus.SelectedIndex = 2;

                RadCbxDist.DataSource = ConsultPaquete.consulDistXfoliYnopak(Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue), RadCbxfolio.SelectedValue, Convert.ToInt32(RadCbxPaquete.SelectedValue == "" ? "0" : RadCbxPaquete.SelectedValue), Convert.ToString(Session["UserName"]));
                RadCbxDist.DataValueField = "distribuidor";
                RadCbxDist.DataTextField = "distribuidor";
                RadCbxDist.DataBind();

                RadCbxDist.Items.Insert(0, new RadComboBoxItem("Seleccione"));

                RevisiPaquete.Visible = true;
                NoCredit.Visible = true;
                RadTxtNoCredit.Visible = true;
                lblFolio.Visible = true;
                LblNoPaquete.Visible = true;
                lbldist.Visible = true;
                RadCbxDist.Visible = true;

                RadCbxPaquete.Visible = true;
                RadCbxfolio.Visible = true;

                btnBuscar.Visible = true;
                btnBuscar.Enabled = false;

                LLenarpaquete();
                rgPaqueteRevision.DataBind();
            }
           
        }

        private List<ConsultaPaquetes> LstDatosRevision
        {
            get
            {
                return Session["LstDatosRevision"] == null
                           ? new List<ConsultaPaquetes>()
                           : Session["LstDatosRevision"] as List<ConsultaPaquetes>;
            }
            set { Session["LstDatosRevision"] = value; }
        }



        protected void CargarCBoxs()
        {
            RadCbxEstatus.DataSource = GeneracionPaquetes.catEstatusZon();
            RadCbxEstatus.DataValueField = "ID_ESTATUS_PAQUETE";
            RadCbxEstatus.DataTextField = "DESCRIPCION";
            RadCbxEstatus.DataBind();

            
            

            ////
            RadCbxfolio.DataSource = ConsultPaquete.catalogoFolio(Convert.ToString(Session["UserName"]));
            RadCbxfolio.DataValueField = "FOLIO";
            RadCbxfolio.DataTextField = "FOLIO";
            RadCbxfolio.DataBind();

            RadCbxPaquete.DataSource = ConsultPaquete.PackXFolioRevi(RadCbxfolio.SelectedValue);
            RadCbxPaquete.DataValueField = "NO_PAQUETE";
            RadCbxPaquete.DataTextField = "NO_PAQUETE";
            RadCbxPaquete.DataBind();


            ////


        }

        protected void LLenarpaquete()
        {

            LstDatosRevision = ConsultPaquete.consulRevisionPaquet(Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue), RadTxtNoCredit.Text, Convert.ToInt32(RadCbxPaquete.SelectedValue == "" ? "0" : RadCbxPaquete.SelectedValue), RadCbxfolio.SelectedValue == "" ? "seleccione" : RadCbxfolio.SelectedValue, RadCbxDist.SelectedValue == "" ? "seleccione" : RadCbxDist.SelectedValue, Convert.ToString(Session["UserName"]));
            rgPaqueteRevision.DataSource = LstDatosRevision;

            ValidaConsultapakeRadGrid();
        }

        protected void llenarPackAceptados()
        {
            LstDatosRevision = ConsultPaquete.consultarAceptados(Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue), RadTxtNoCredit.Text, Convert.ToInt32(RadCbxPaquete.SelectedValue == "" ? "0" : RadCbxPaquete.SelectedValue), RadCbxfolio.SelectedValue, RadCbxDist.SelectedValue == "" ? "seleccione" : RadCbxDist.SelectedValue, Convert.ToString(Session["UserName"]));
            rgPaqueteAceptado.DataSource = LstDatosRevision;

            ValidaConsultapakeRadGridAceptados();
        }

        protected void llenarPackPendientes()
        {
            LstDatosRevision = ConsultPaquete.consultarPendientes(Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue), RadTxtNoCredit.Text, RadCbxDist.SelectedValue == "" ? "seleccione" : RadCbxDist.SelectedValue, Convert.ToString(Session["UserName"]));
            rgPaquetePendiente.DataSource = LstDatosRevision;
        }

        protected void ConsultaPack()
        {
            LstDatosRevision = ConsultPaquete.consultarPack(Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue), RadTxtNoCredit.Text, Convert.ToInt32(RadCbxPaquete.SelectedValue == "" ? "0" : RadCbxPaquete.SelectedValue), RadCbxfolio.SelectedValue, RadCbxDist.SelectedValue == "" ? "seleccione" : RadCbxDist.SelectedValue, Convert.ToString(Session["UserName"]));
            rgConsultaPaquete.DataSource = LstDatosRevision;

            ValidaConsultaPaquetes();
        }

        protected void ValidaConsultapakeRadGrid()
        {
            for (int i = 0; i < LstDatosRevision.Count; i++)
            {
                foreach (GridDataItem dataItem in rgPaqueteRevision.MasterTableView.Items)
                {

                    if (LstDatosRevision[i].noCredit == rgPaqueteRevision.MasterTableView.Items[dataItem.ItemIndex]["noCredit"].Text)
                    {
                        (dataItem.FindControl("verCarta") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatosRevision[i].noCredit + "','carta','consulta')");
                        (dataItem.FindControl("verCarta") as ImageButton).Visible = LstDatosRevision[i].carta != null ? true : false;
                        (dataItem.FindControl("lblCarta") as Label).Visible = LstDatosRevision[i].carta != null ? false : true;

                        (dataItem.FindControl("verActa") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatosRevision[i].noCredit + "','acta','consulta')");
                        (dataItem.FindControl("verActa") as ImageButton).Visible = LstDatosRevision[i].acta != null ? true : false;
                        (dataItem.FindControl("lblActa") as Label).Visible = LstDatosRevision[i].acta != null ? false : true;

                        (dataItem.FindControl("lblConFecha") as Label).Visible = LstDatosRevision[i].fechaRevision != null ? true : false;
                        (dataItem.FindControl("lblSinFecha") as Label).Visible = LstDatosRevision[i].fechaRevision != null ? false : true;
                    }
                }
            }

        }

        protected void ValidaConsultapakeRadGridAceptados()
        {
            for (int i = 0; i < LstDatosRevision.Count; i++)
            {
                foreach (GridDataItem dataItem in rgPaqueteAceptado.MasterTableView.Items)
                {

                    if (LstDatosRevision[i].noCredit == rgPaqueteAceptado.MasterTableView.Items[dataItem.ItemIndex]["noCredit"].Text)
                    {
                        (dataItem.FindControl("verCarta") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatosRevision[i].noCredit + "','carta','consulta')");
                        (dataItem.FindControl("verCarta") as ImageButton).Visible = LstDatosRevision[i].carta != null ? true : false;
                        (dataItem.FindControl("lblCarta") as Label).Visible = LstDatosRevision[i].carta != null ? false : true;

                        (dataItem.FindControl("verActa") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatosRevision[i].noCredit + "','acta','consulta')");
                        (dataItem.FindControl("verActa") as ImageButton).Visible = LstDatosRevision[i].acta != null ? true : false;
                        (dataItem.FindControl("lblActa") as Label).Visible = LstDatosRevision[i].acta != null ? false : true;

                    }
                }
            }

        }

        protected void ValidaConsultaPaquetes()
        {
            for (int i = 0; i < LstDatosRevision.Count; i++)
            {
                foreach (GridDataItem dataItem in rgConsultaPaquete.MasterTableView.Items)
                {

                    if (LstDatosRevision[i].noCredit == rgConsultaPaquete.MasterTableView.Items[dataItem.ItemIndex]["noCredit"].Text)
                    {
                        (dataItem.FindControl("verCarta") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatosRevision[i].noCredit + "','carta','consulta')");
                        (dataItem.FindControl("verCarta") as ImageButton).Visible = LstDatosRevision[i].carta != null ? true : false;
                        (dataItem.FindControl("lblCarta") as Label).Visible = LstDatosRevision[i].carta != null ? false : true;

                        (dataItem.FindControl("verActa") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatosRevision[i].noCredit + "','acta','consulta')");
                        (dataItem.FindControl("verActa") as ImageButton).Visible = LstDatosRevision[i].acta != null ? true : false;
                        (dataItem.FindControl("lblActa") as Label).Visible = LstDatosRevision[i].acta != null ? false : true;

                    }
                }
            }

        }


        protected void rgPaqueteRevision_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            LLenarpaquete();
        }

        protected void rgPaqueteRevision_DataBound(object sender, EventArgs e)
        {
            LLenarpaquete();
        }

        protected void RadCbxEstatus_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCbxEstatus.SelectedValue == "2")
            {
                RadCbxfolio.DataSource = ConsultPaquete.catalogoFolio(Convert.ToString(Session["UserName"]));
                RadCbxfolio.DataValueField = "FOLIO";
                RadCbxfolio.DataTextField = "FOLIO";
                RadCbxfolio.DataBind();
                //
                RadCbxPaquete.DataSource = ConsultPaquete.PackXFolioRevi(RadCbxfolio.SelectedValue);
                RadCbxPaquete.DataValueField = "NO_PAQUETE";
                RadCbxPaquete.DataTextField = "NO_PAQUETE";
                RadCbxPaquete.DataBind();

                RadCbxDist.DataSource = ConsultPaquete.consulDistXfoliYnopak(Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue), RadCbxfolio.SelectedValue, Convert.ToInt32(RadCbxPaquete.SelectedValue == "" ? "0" : RadCbxPaquete.SelectedValue), Convert.ToString(Session["UserName"]));
                RadCbxDist.DataValueField = "distribuidor";
                RadCbxDist.DataTextField = "distribuidor";
                RadCbxDist.DataBind();

                RadCbxDist.Items.Insert(0, new RadComboBoxItem("Seleccione"));


                rgPaqueteAceptado.DataSource = null;
                rgPaqueteAceptado.DataBind();
                LstDatosRevision = ConsultPaquete.consulRevisionPaquet(Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue), RadTxtNoCredit.Text, Convert.ToInt32(RadCbxPaquete.SelectedValue == "" ? "0" : RadCbxPaquete.SelectedValue), RadCbxfolio.SelectedValue == "" ? "seleccione" : RadCbxfolio.SelectedValue, RadCbxDist.SelectedValue == "" ? "seleccione" : RadCbxDist.SelectedValue, Convert.ToString(Session["UserName"]));
                AceptadosPaquete.Visible = false;
                RevisiPaquete.Visible = true;
                PendientePaquete.Visible = false;
                ConsultPaquetes.Visible = false;

                NoCredit.Visible = true;

                lblFolio.Visible = true;
                LblNoPaquete.Visible = true;
                lbldist.Visible = true;
 
                RadCbxfolio.Visible = true;
                RadCbxPaquete.Visible = true;

                btnBuscar.Enabled = false;
                

                LLenarpaquete();
                rgPaqueteRevision.DataBind();

            }
            else if (RadCbxEstatus.SelectedValue == "3")
            {
                RadCbxfolio.DataSource = ConsultPaquete.catalogoFolio(Convert.ToString(Session["UserName"]));
                RadCbxfolio.DataValueField = "FOLIO";
                RadCbxfolio.DataTextField = "FOLIO";
                RadCbxfolio.DataBind();
                //PackXFolioRevi
                RadCbxPaquete.DataSource = ConsultPaquete.PackXFolio(RadCbxfolio.SelectedValue, Convert.ToInt32(RadCbxEstatus.SelectedValue));
                RadCbxPaquete.DataValueField = "NO_PAQUETE";
                RadCbxPaquete.DataTextField = "NO_PAQUETE";
                RadCbxPaquete.DataBind();
                RadCbxfolio.Items.Insert(0, new RadComboBoxItem("Seleccione"));
                RadCbxPaquete.Items.Insert(0, new RadComboBoxItem("Seleccione"));

                rgPaqueteRevision.DataSource = null;
                rgPaqueteRevision.DataBind();
                LstDatosRevision = ConsultPaquete.consultarAceptados(Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue), RadTxtNoCredit.Text, Convert.ToInt32(RadCbxPaquete.SelectedValue == "" ? "0" : RadCbxPaquete.SelectedValue), RadCbxfolio.SelectedValue, RadCbxDist.SelectedValue == "" ? "seleccione" : RadCbxDist.SelectedValue, Convert.ToString(Session["UserName"]));
                
                RadCbxDist.DataSource = ConsultPaquete.consulDist(Convert.ToString(Session["UserName"]));
                RadCbxDist.DataValueField = "distribuidor";
                RadCbxDist.DataTextField = "distribuidor";
                RadCbxDist.DataBind();
                RadCbxDist.Items.Insert(0, new RadComboBoxItem("Seleccione"));


                NoCredit.Visible = true;
                RadTxtNoCredit.Visible = true;
                lblFolio.Visible = true;
                LblNoPaquete.Visible = true;
                lbldist.Visible = true;

                RadCbxfolio.Visible = true;
                RadCbxPaquete.Visible = true;

                PendientePaquete.Visible = false;
                AceptadosPaquete.Visible = true;
                RevisiPaquete.Visible = false;
                ConsultPaquetes.Visible = false;

                btnBuscar.Enabled = false;
  

                llenarPackAceptados();
                rgPaqueteAceptado.DataBind();
            }
            else if (RadCbxEstatus.SelectedValue == "1")
            {
                RadTxtNoCredit.Attributes.Add("onKeyUp", "Validarfiltros('otros')");

                ConsultPaquetes.Visible = false;
                PendientePaquete.Visible = true;
                AceptadosPaquete.Visible = false;
                RevisiPaquete.Visible = false;

                NoCredit.Visible = true;
                RadTxtNoCredit.Visible = true;
                lbldist.Visible = true;

                RadCbxfolio.Visible = false;
                RadCbxPaquete.Visible = false;

                lblFolio.Visible = false;

 
                LblNoPaquete.Visible = false;

                btnBuscar.Enabled = false;

                llenarPackPendientes();
                rgPaquetePendiente.DataBind();

                RadCbxfolio.DataSource = ConsultPaquete.catalogoFolio(Convert.ToString(Session["UserName"]));
                RadCbxfolio.DataValueField = "FOLIO";
                RadCbxfolio.DataTextField = "FOLIO";
                RadCbxfolio.DataBind();
                //PackXFolioRevi
                RadCbxPaquete.DataSource = ConsultPaquete.PackXFolio(RadCbxfolio.SelectedValue, Convert.ToInt32(RadCbxEstatus.SelectedValue));
                RadCbxPaquete.DataValueField = "NO_PAQUETE";
                RadCbxPaquete.DataTextField = "NO_PAQUETE";
                RadCbxPaquete.DataBind();
                RadCbxfolio.Items.Insert(0, new RadComboBoxItem("Seleccione"));
                RadCbxPaquete.Items.Insert(0, new RadComboBoxItem("Seleccione"));



                RadCbxDist.DataSource = ConsultPaquete.consulDist(Convert.ToString(Session["UserName"]));
                RadCbxDist.DataValueField = "distribuidor";
                RadCbxDist.DataTextField = "distribuidor";
                RadCbxDist.DataBind();

                RadCbxDist.Items.Insert(0, new RadComboBoxItem("Seleccione"));
            } 
            else if (RadCbxEstatus.SelectedValue == "")
            {
                RadCbxDist.DataSource = ConsultPaquete.consulDist(Convert.ToString(Session["UserName"]));
                RadCbxDist.DataValueField = "distribuidor";
                RadCbxDist.DataTextField = "distribuidor";
                RadCbxDist.DataBind();

                RadCbxDist.Items.Insert(0, new RadComboBoxItem("Seleccione"));
                RadCbxfolio.Visible = true;
                RadCbxPaquete.Visible = true;
                lblFolio.Visible = true;
                LblNoPaquete.Visible = true;

                PendientePaquete.Visible = false;
                AceptadosPaquete.Visible = false;
                RevisiPaquete.Visible = false;
                ConsultPaquetes.Visible = true;

                RadCbxPaquete.Items.Clear();
                RadCbxfolio.Items.Insert(0, new RadComboBoxItem("Seleccione"));
                RadCbxPaquete.Items.Insert(0, new RadComboBoxItem("Seleccione"));
                RadCbxfolio.SelectedIndex = 0;

                ConsultaPack();
                rgConsultaPaquete.DataBind();
            }


        }

        protected void RadCbxfolio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if ( RadCbxEstatus.SelectedValue=="1")
            {
                RadCbxPaquete.DataSource = ConsultPaquete.PackXFolio(RadCbxfolio.SelectedValue, Convert.ToInt32(RadCbxEstatus.SelectedValue));
                RadCbxPaquete.DataValueField = "NO_PAQUETE";
                RadCbxPaquete.DataTextField = "NO_PAQUETE";
                RadCbxPaquete.DataBind();

                RadCbxPaquete.Items.Insert(0, new RadComboBoxItem("Seleccione"));


                llenarPackPendientes();
                rgPaquetePendiente.DataBind();
            }
            else if (RadCbxEstatus.SelectedValue=="2")
            {

                RadCbxPaquete.DataSource = ConsultPaquete.PackXFolioRevi(RadCbxfolio.SelectedValue);
                RadCbxPaquete.DataValueField = "NO_PAQUETE";
                RadCbxPaquete.DataTextField = "NO_PAQUETE";
                RadCbxPaquete.DataBind();

                LLenarpaquete();
                rgPaqueteRevision.DataBind();
            }
            else if (RadCbxEstatus.SelectedValue=="3")
            {
                RadCbxPaquete.DataSource = ConsultPaquete.PackXFolio(RadCbxfolio.SelectedValue, Convert.ToInt32(RadCbxEstatus.SelectedValue));
                RadCbxPaquete.DataValueField = "NO_PAQUETE";
                RadCbxPaquete.DataTextField = "NO_PAQUETE";
                RadCbxPaquete.DataBind();

                RadCbxPaquete.Items.Insert(0, new RadComboBoxItem("Seleccione"));


                llenarPackAceptados();
                rgPaqueteAceptado.DataBind();
            }
            else if (RadCbxEstatus.SelectedValue == "")
            {
                RadCbxPaquete.DataSource = ConsultPaquete.PackXFolio(RadCbxfolio.SelectedValue, Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue));
                RadCbxPaquete.DataValueField = "NO_PAQUETE";
                RadCbxPaquete.DataTextField = "NO_PAQUETE";
                RadCbxPaquete.DataBind();

                RadCbxPaquete.Items.Insert(0, new RadComboBoxItem("Seleccione"));

                ConsultaPack(); ;
                rgConsultaPaquete.DataBind();
            }

           
            
           
            bool ban = false;
            foreach (GridDataItem item in rgPaqueteRevision.MasterTableView.Items)
            {
                if ((item.FindControl("chkOperacion") as CheckBox).Checked)
                { ban = true; break; }
                else { ban = false; }
            }

            if (RadCbxfolio.SelectedValue!=""&&RadCbxPaquete.SelectedValue!=""&& ban==true)
            {
                btnAceptar.Enabled = true;
                btnRechazar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
                btnRechazar.Enabled = false;      
            }
        }

        protected void RadCbxPaquete_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCbxPaquete.SelectedValue=="")
            {
                RadCbxDist.DataSource = ConsultPaquete.consulDist(Convert.ToString(Session["UserName"]));
                RadCbxDist.DataValueField = "distribuidor";
                RadCbxDist.DataTextField = "distribuidor";
                RadCbxDist.DataBind();

                RadCbxDist.Items.Insert(0, new RadComboBoxItem("Seleccione"));

                ConsultaPack();
                rgConsultaPaquete.DataBind();
            }
            else
            {
                RadCbxDist.DataSource = ConsultPaquete.consulDistXfoliYnopak(Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue),RadCbxfolio.SelectedValue, Convert.ToInt32(RadCbxPaquete.SelectedValue == "seleccione" ? "0" : RadCbxPaquete.SelectedValue), Convert.ToString(Session["UserName"]));
                RadCbxDist.DataValueField = "distribuidor";
                RadCbxDist.DataTextField = "distribuidor";
                RadCbxDist.DataBind();

                RadCbxDist.Items.Insert(0, new RadComboBoxItem("Seleccione"));

                LLenarpaquete();
                rgPaqueteRevision.DataBind();

                llenarPackAceptados();
                rgPaqueteAceptado.DataBind();
            }

            bool ban = false;
            foreach (GridDataItem item in rgPaqueteRevision.MasterTableView.Items)
            {
                if ((item.FindControl("chkOperacion") as CheckBox).Checked)
                { ban = true; break; }
                else { ban = false; }
            }

            if (RadCbxfolio.SelectedValue != "" && RadCbxPaquete.SelectedValue != "" && ban == true)
            {
                btnAceptar.Enabled = true;
                btnRechazar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
                btnRechazar.Enabled = false;
            }

        }

        protected void chkOperacion_CheckedChanged(object sender, EventArgs e)
        {
            bool ban = false;
            foreach (GridDataItem item in rgPaqueteRevision.MasterTableView.Items)
            {
                if ((item.FindControl("chkOperacion") as CheckBox).Checked)
                { ban = true; break; }
                else { ban = false; }
            }

            if (ban == true && ((RadCbxfolio.SelectedValue == "" && RadCbxPaquete.SelectedValue == "") || (RadCbxfolio.SelectedValue == "" && RadCbxPaquete.SelectedValue != "") || (RadCbxfolio.SelectedValue != "" && RadCbxPaquete.SelectedValue == "")))
            {
                btnAceptar.Enabled = false;
                btnRechazar.Enabled = false;
            }
            else if (ban == false && ((RadCbxfolio.SelectedValue == "" && RadCbxPaquete.SelectedValue == "") || (RadCbxfolio.SelectedValue == "" && RadCbxPaquete.SelectedValue != "") || (RadCbxfolio.SelectedValue != "" && RadCbxPaquete.SelectedValue == "")))
            {
                btnAceptar.Enabled = false;
                btnRechazar.Enabled = false;
            }
            else if (ban == false&&(RadCbxfolio.SelectedValue != "" && RadCbxPaquete.SelectedValue != ""))
            {
                btnAceptar.Enabled = false;
                btnRechazar.Enabled = false; 
            } 
            else
            {
                btnAceptar.Enabled = true;
                btnRechazar.Enabled = true;
            }

            if (RadTxtNoCredit.Text!=""&& ban==true)
            {
                btnAceptar.Enabled = true;
                btnRechazar.Enabled = true;
            }



        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (RadCbxEstatus.SelectedValue == "1")
            {
                llenarPackPendientes();
                rgPaquetePendiente.DataBind();
                
            }
            else if (RadCbxEstatus.SelectedValue == "2")
            {
                LLenarpaquete();
                rgPaqueteRevision.DataBind();

            }
            else if (RadCbxEstatus.SelectedValue == "3")
            {
                llenarPackAceptados();
                rgPaqueteAceptado.DataBind();
            }
            else if (RadCbxEstatus.SelectedValue == "")
            {
                ConsultaPack();
                rgConsultaPaquete.DataBind();
            }


        }

        protected void rgPaqueteAceptado_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            llenarPackAceptados();
        }

        protected void rgPaqueteAceptado_DataBound(object sender, EventArgs e)
        {
            llenarPackAceptados();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string pac = "";
            foreach (GridDataItem item in rgPaqueteRevision.MasterTableView.Items)
            {
                if ((item.FindControl("chkOperacion") as CheckBox).Checked)
                {
                    pac = rgPaqueteRevision.MasterTableView.Items[item.ItemIndex]["folioPakete"].Text;
                    ConsultPaquete.ActualizarPack(rgPaqueteRevision.MasterTableView.Items[item.ItemIndex]["folioPakete"].Text, Convert.ToInt32(rgPaqueteRevision.MasterTableView.Items[item.ItemIndex]["noPakete"].Text), rgPaqueteRevision.MasterTableView.Items[item.ItemIndex]["noCredit"].Text, 3, "");
                }

            }

            RadWindowManager1.RadAlert("EL PAQUETE No " + pac + "  se ha Aceptado!!!!!", 450, 150, "ACEPTACIÓN DE PAQUETE", null);

            btnAceptar.Enabled = false;
            btnRechazar.Enabled = false;
            LLenarpaquete();
            rgPaqueteRevision.DataBind();
            LstDatosRevision = ConsultPaquete.consulRevisionPaquet(Convert.ToInt32(RadCbxEstatus.SelectedValue), RadTxtNoCredit.Text, Convert.ToInt32(RadCbxPaquete.SelectedValue == "" ? "0" : RadCbxPaquete.SelectedValue), RadCbxfolio.SelectedValue == "" ? "seleccione" : RadCbxfolio.SelectedValue, RadCbxDist.SelectedValue == "" ? "seleccione" : RadCbxDist.SelectedValue, Convert.ToString(Session["UserName"]));

            RadCbxfolio.DataSource = ConsultPaquete.catalogoFolio(Convert.ToString(Session["UserName"]));
            RadCbxfolio.DataValueField = "FOLIO";
            RadCbxfolio.DataTextField = "FOLIO";
            RadCbxfolio.DataBind();
            //
            RadCbxPaquete.DataSource = ConsultPaquete.PackXFolioRevi(RadCbxfolio.SelectedValue);
            RadCbxPaquete.DataValueField = "NO_PAQUETE";
            RadCbxPaquete.DataTextField = "NO_PAQUETE";
            RadCbxPaquete.DataBind();


            RadCbxDist.Items.Clear();

            LLenarpaquete();
            rgPaqueteRevision.DataBind();
        

            
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rgPaqueteRevision.MasterTableView.Items)
            {
                if ((item.FindControl("chkOperacion") as CheckBox).Checked)
                {
                    ConsultPaquete.ActualizarPack(rgPaqueteRevision.MasterTableView.Items[item.ItemIndex]["folioPakete"].Text, Convert.ToInt32(rgPaqueteRevision.MasterTableView.Items[item.ItemIndex]["noPakete"].Text), rgPaqueteRevision.MasterTableView.Items[item.ItemIndex]["noCredit"].Text, 4, TexComen.Text);
                }

            }
            btnAceptar.Enabled = false;
            btnRechazar.Enabled = false;
            LLenarpaquete();
            rgPaqueteRevision.DataBind();
            TexComen.Text = "";
            LstDatosRevision = ConsultPaquete.consulRevisionPaquet(Convert.ToInt32(RadCbxEstatus.SelectedValue), RadTxtNoCredit.Text, Convert.ToInt32(RadCbxPaquete.SelectedValue == "" ? "0" : RadCbxPaquete.SelectedValue), RadCbxfolio.SelectedValue == "" ? "seleccione" : RadCbxfolio.SelectedValue, RadCbxDist.SelectedValue == "" ? "seleccione" : RadCbxDist.SelectedValue, Convert.ToString(Session["UserName"]));

            RadCbxfolio.DataSource = ConsultPaquete.catalogoFolio(Convert.ToString(Session["UserName"]));
            RadCbxfolio.DataValueField = "FOLIO";
            RadCbxfolio.DataTextField = "FOLIO";
            RadCbxfolio.DataBind();
            //
            RadCbxPaquete.DataSource = ConsultPaquete.PackXFolioRevi(RadCbxfolio.SelectedValue);
            RadCbxPaquete.DataValueField = "NO_PAQUETE";
            RadCbxPaquete.DataTextField = "NO_PAQUETE";
            RadCbxPaquete.DataBind();


            RadCbxDist.Items.Clear();

             LLenarpaquete();
                rgPaqueteRevision.DataBind();
        }

        protected void bntCancelar_Click(object sender, EventArgs e)
        {
            TexComen.Text = "";
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }

        protected void rgPaquetePendiente_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            llenarPackPendientes();
        }

        protected void rgPaquetePendiente_DataBound(object sender, EventArgs e)
        {
            llenarPackPendientes();
        }

        protected void RadCbxDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCbxEstatus.SelectedValue=="1")
            {
                llenarPackPendientes();
                rgPaquetePendiente.DataBind();
            }
            else if (RadCbxEstatus.SelectedValue=="3")
            {
                llenarPackAceptados();
                rgPaqueteAceptado.DataBind();
            }
            else if (RadCbxEstatus.SelectedValue=="2")
            {
                LLenarpaquete();
                rgPaqueteRevision.DataBind();
            }
            else if (RadCbxEstatus.SelectedValue=="")
            {
                  ConsultaPack();
                rgConsultaPaquete.DataBind();
            }
            
        }

        protected void rgConsultaPaquete_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ConsultaPack();
        }

        protected void rgConsultaPaquete_DataBound(object sender, EventArgs e)
        {
            ConsultaPack();
        }


    }
}