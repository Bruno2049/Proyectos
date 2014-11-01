using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using PAEEEM.LogicaNegocios.CirculoCredito;
using PAEEEM.Entidades.CirculoCredito;
using PAEEEM.Entidades;

namespace PAEEEM.CirculoCredito
{
    public partial class DistCirculoCredit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CirculoCreditTemporal = new List<TemporalPDFs>();
                CargarCBox();

                RadCbxEstatus.Items.Insert(0, new RadComboBoxItem("Seleccione"));
                RadCbxEstatus.SelectedIndex = 1;

                GenePaquete.Visible = true;
          
                LstDatos = GeneracionPaquetes.GenerarPak(Convert.ToString(Session["UserName"]));
                rgPaquetePendiente.DataSource = LstDatos;

                RadTxtNoCredit.Attributes.Add("onKeyUp", "Validarfiltros()");

                CirculoCredit();
            }
        }

        protected void CargarCBox()
        {
            RadCbxEstatus.DataSource = GeneracionPaquetes.catalogo();
            RadCbxEstatus.DataValueField = "ID_ESTATUS_PAQUETE";
            RadCbxEstatus.DataTextField = "DESCRIPCION";
            RadCbxEstatus.DataBind();



            ////
            RadCbxFolio.DataSource = ConsultPaquete.catalogoFolio(Convert.ToString(Session["UserName"]));
            RadCbxFolio.DataValueField = "FOLIO";
            RadCbxFolio.DataTextField = "FOLIO";
            RadCbxFolio.DataBind();

            RadCbxNopack.DataSource = ConsultPaquete.PackXFolio(RadCbxFolio.SelectedValue, Convert.ToInt32(RadCbxEstatus.SelectedValue));
            RadCbxNopack.DataValueField = "NO_PAQUETE";
            RadCbxNopack.DataTextField = "NO_PAQUETE";
            RadCbxNopack.DataBind();

        }


        #region Generar Paquete

        #region Atributos
        private List<DatosPackPendiente> LstDatos
        {
            get
            {
                return ViewState["LstDatos"] == null
                           ? new List<DatosPackPendiente>()
                           : ViewState["LstDatos"] as List<DatosPackPendiente>;
            }
            set { ViewState["LstDatos"] = value; }
        }





        private List<TemporalPDFs> CirculoCreditTemporal
        {
            get
            {
                return Session["CirculoCreditTemporal"] == null
                           ? new List<TemporalPDFs>()
                           : Session["CirculoCreditTemporal"] as List<TemporalPDFs>;
            }
            set { Session["CirculoCreditTemporal"] = value; }
        }



        #endregion
        

        protected void LLenarpaquete()
        {
            LstDatos = GeneracionPaquetes.GenerarPak(Convert.ToString(Session["UserName"]));
            rgPaquetePendiente.DataSource = LstDatos;
            ValidacionesRadGrid();
            ValidarPaquete();

        }

        protected void CirculoCredit()
        {
            for (int i = 0; i < LstDatos.Count; i++)
            {
                var dif = DateTime.Now - Convert.ToDateTime(LstDatos[i].fechaConsulta);
                var t = dif.Days;
                if (t>10)
                {
                     TemporalPDFs temp = new TemporalPDFs();
                     temp.nocredit = LstDatos[i].Nocredit;
                     temp.cheked = true;
                     CirculoCreditTemporal.Add(temp);
                }
                else
                {
                    TemporalPDFs temp = new TemporalPDFs();
                    temp.nocredit = LstDatos[i].Nocredit;
                    temp.cheked = false;
                    CirculoCreditTemporal.Add(temp);
                }
                
            }
        }

        protected void ValidacionesRadGrid()
        {
            for (int i = 0; i < LstDatos.Count; i++)
            {
                foreach (GridDataItem dataItem in rgPaquetePendiente.MasterTableView.Items)
                {

                    if (LstDatos[i].Nocredit == rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["Nocredit"].Text
                   && LstDatos[i].folioConsulta == rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["folioConsulta"].Text
                   && LstDatos[i].statusPaquete == rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["statusPaquete"].Text)
                    {
                        (dataItem.FindControl("verCarta") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatos[i].Nocredit + "','carta','carga')");
                        (dataItem.FindControl("verActa") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatos[i].Nocredit + "','acta','carga')");

                        var dif = DateTime.Now - Convert.ToDateTime(rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["fechaConsulta"].Text);
                        var t = dif.Days;

                        var credit = CirculoCreditTemporal.Find(o => o.nocredit == rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["Nocredit"].Text);
                            
                        //(dataItem.FindControl("chkOperacion") as CheckBox).Checked = t > 10 ? true : false;
                        (dataItem.FindControl("chkOperacion") as CheckBox).Checked =credit!=null?credit.cheked==true?true:false: t > 10 ? true:false;
                        ////headerChkbox
                        

                        rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["colActa"].Visible = (dataItem.FindControl("chkOperacion") as CheckBox).Checked == true ? true : false;
                        rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["colCarta"].Visible = (dataItem.FindControl("chkOperacion") as CheckBox).Checked == true ? true : false;
                        //(dataItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = (dataItem.FindControl("chkOperacion") as CheckBox).Checked == true ? true : false; ;
                        //(dataItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = (dataItem.FindControl("chkOperacion") as CheckBox).Checked == true ? true : false; ;


                        if ((dataItem.FindControl("chkOperacion") as CheckBox).Checked)
                        {
                            TemporalPDFs temp = new TemporalPDFs();
                            temp.nocredit = rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["Nocredit"].Text;
                            temp.cheked= t > 10 ? true : false;
                            var credi = CirculoCreditTemporal.Find(o => o.nocredit == rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["Nocredit"].Text);
                            
                            if (credi == null)
                            {
                                CirculoCreditTemporal.Add(temp);
                            }
                            else
                            {
                                
                                if (credi.carta != null)
                                {
                                    (dataItem.FindControl("verCarta") as ImageButton).Visible = true;
                                    (dataItem.FindControl("EliminarCarta") as ImageButton).Visible = true;
                                    (dataItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = false;
                                }
                                if (credi.acta != null)
                                {
                                    (dataItem.FindControl("verActa") as ImageButton).Visible = true;
                                    (dataItem.FindControl("EliminarActa") as ImageButton).Visible = true;
                                    (dataItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = false;
                                }

                               
                               
                            }

                        }

                    }
                }
            }

        }

        protected void ValidarPaquete() 
        {
            bool ban = false;
            foreach (var Item in CirculoCreditTemporal)
            {
                if (Item.cheked==true)
                {
                    if ((CirculoCreditTemporal.Find(o => o.nocredit == Item.nocredit).carta != null || CirculoCreditTemporal.Find(o => o.nocredit == Item.nocredit).acta != null))
                    { ban = true; }
                    else { ban = false; break; }
                }

            }
            if (ban == true)
            {
                GenerarPaquete.Enabled = true;
                bntGeberarRechazados.Enabled = true;
            }
            else
            {
                GenerarPaquete.Enabled = false;
                bntGeberarRechazados.Enabled = false;
            }

        }

        protected void rgPaquetePendiente_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LLenarpaquete();
        }

        protected void rgPaquetePendiente_DataBound(object sender, EventArgs e)
        {
          LLenarpaquete();
        }

        protected void UploadedCarta_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            var Upload = sender as RadAsyncUpload;
            if (Upload != null)
            {
                var editedItem = Upload.NamingContainer as GridEditableItem;
                if (e.File.FileName != null)
                {

                        var noCredit = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Nocredit"].ToString();
                        var doc = new byte[e.File.ContentLength];
                        e.File.InputStream.Read(doc, 0, e.File.ContentLength);

                        CirculoCreditTemporal.Find(o => o.nocredit == noCredit).carta = doc;

                    if (e.IsValid)
                    {
                        (editedItem.FindControl("EliminarCarta") as ImageButton).Visible = true;
                        (editedItem.FindControl("verCarta") as ImageButton).Visible = true;
                        (editedItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = false;

                    }
                }
                else
                {
                    RadWindowManager1.RadAlert("Ocurrió un Problema al cargar el archivo", 300, 150, "Carta Autorización", null);
                }

            }


            ValidarPaquete(); 
        }


        protected void UploadedActa_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            var Upload = sender as RadAsyncUpload;
            if (Upload != null)
            {
                var editedItem = Upload.NamingContainer as GridEditableItem;
                if (e.File.FileName != null)
                {
                    var noCredit = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Nocredit"].ToString();
                    var doc = new byte[e.File.ContentLength];
                    e.File.InputStream.Read(doc, 0, e.File.ContentLength);

                    CirculoCreditTemporal.Find(o => o.nocredit == noCredit).acta = doc;

                    if (e.IsValid)
                    {
                        (editedItem.FindControl("EliminarActa") as ImageButton).Visible = true;
                        (editedItem.FindControl("verActa") as ImageButton).Visible = true;
                        (editedItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = false;

                    }
                }

            }
            ValidarPaquete(); 

        }

        protected void btnRefres_Click(object sender, EventArgs e)
        {

        }

        protected void EliminarCarta_Click(object sender, ImageClickEventArgs e)
        {
            var eliminar = sender as ImageButton;
            if (eliminar != null)
            {
                var item = eliminar.NamingContainer as GridEditableItem;
                var nocredit = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Nocredit"].ToString();
                CirculoCreditTemporal.Find(o => o.nocredit == nocredit).carta = null;

                if (CirculoCreditTemporal.Find(o => o.nocredit == nocredit).carta == null)
                {
                    (item.FindControl("verCarta") as ImageButton).Visible = false;
                    (item.FindControl("EliminarCarta") as ImageButton).Visible = false;
                    (item.FindControl("UploadedCarta") as RadAsyncUpload).Visible = true;
                }
            }
            ValidarPaquete(); 
             
        }

        protected void EliminarActa_Click(object sender, ImageClickEventArgs e)
        {
            var eliminar = sender as ImageButton;
            if (eliminar != null)
            {
                var item = eliminar.NamingContainer as GridEditableItem;
                var credit = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Nocredit"].ToString();
                CirculoCreditTemporal.Find(o => o.nocredit == credit).acta = null;

                if (CirculoCreditTemporal.Find(o => o.nocredit == credit).acta == null)
                {
                    (item.FindControl("verActa") as ImageButton).Visible = false;
                    (item.FindControl("EliminarActa") as ImageButton).Visible = false;
                    (item.FindControl("UploadedActa") as RadAsyncUpload).Visible = true;
                }
            }
            ValidarPaquete(); 
        }

        protected void chkOperacion_CheckedChanged(object sender, EventArgs e)
        {
            var check = sender as CheckBox;
            if (check != null)
            {
                var editedItem = check.NamingContainer as GridEditableItem;

                var credit = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Nocredit"].ToString();
                var crediCirculo = CirculoCreditTemporal.Find(o => o.nocredit == credit);

                if (!(editedItem.FindControl("chkOperacion") as CheckBox).Checked)
                {
                    //eliminar
                   CirculoCreditTemporal.Find(o => o.nocredit == credit).cheked=false;
                   CirculoCreditTemporal.Find(o => o.nocredit == credit).carta = null;
                   CirculoCreditTemporal.Find(o => o.nocredit == credit).acta = null;

                    (editedItem.FindControl("verActa") as ImageButton).Visible = false;
                    (editedItem.FindControl("EliminarActa") as ImageButton).Visible = false;
                    (editedItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = true;

                    (editedItem.FindControl("verCarta") as ImageButton).Visible = false;
                    (editedItem.FindControl("EliminarCarta") as ImageButton).Visible = false;
                    (editedItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = true;

                    rgPaquetePendiente.MasterTableView.Items[editedItem.ItemIndex]["colActa"].Visible = false;
                    rgPaquetePendiente.MasterTableView.Items[editedItem.ItemIndex]["colCarta"].Visible = false;
                       

                }
                else
                {
                    rgPaquetePendiente.MasterTableView.Items[editedItem.ItemIndex]["colActa"].Visible = true;
                    rgPaquetePendiente.MasterTableView.Items[editedItem.ItemIndex]["colCarta"].Visible = true;
                    ///agregar
                    var cre = CirculoCreditTemporal.Find(o => o.nocredit == rgPaquetePendiente.MasterTableView.Items[editedItem.ItemIndex]["Nocredit"].Text);
                    if (cre==null)
                    {
                        TemporalPDFs temp = new TemporalPDFs();
                        temp.nocredit = credit;
                        temp.cheked = true;
                        CirculoCreditTemporal.Add(temp);
                    }
                    else
                    {
                        CirculoCreditTemporal.Find(o => o.nocredit == credit).cheked = true;
                    }


                }
            }

        ValidarPaquete(); 

        }

        protected void GenerarPaquete_Click(object sender, EventArgs e)
        {
            int NoP = GeneracionPaquetes.noPaquete(GeneracionPaquetes.GenerasFolio(Convert.ToInt32(Session["IdUserLogueado"])), Convert.ToString(Session["UserName"]));
            string folio = GeneracionPaquetes.GenerasFolio(Convert.ToInt32(Session["IdUserLogueado"]));

            int con = 0;
                foreach (var dataItem in CirculoCreditTemporal)
                {
                    PAQUETES_CIRCULO_CREDITO paquete = null;

                    if (dataItem.acta!=null||dataItem.carta!=null)
                    {
                        try
                        {
                            paquete = new PAQUETES_CIRCULO_CREDITO
                            {
                                FOLIO = folio,
                                ADICIONADO_POR = Convert.ToString(Session["UserName"]),
                                NO_PAQUETE = NoP,
                                No_Credito = dataItem.nocredit,
                                ID_ESTATUS_PAQUETE = 2,
                                FECHA_ADICION = DateTime.Now.Date,
                                FECHA_REVISION = null,
                                Carta_Autorizacion = CirculoCreditTemporal.Find(o => o.nocredit == dataItem.nocredit).carta,
                                Acta_Ministerio = CirculoCreditTemporal.Find(o => o.nocredit == dataItem.nocredit).acta,
                                Comentarios = string.Empty

                            };
                            GeneracionPaquetes.Guardapaquete(paquete);
                            dataItem.cheked = false;
                            dataItem.carta = null;
                            dataItem.acta = null;
                            con++;
                        }
                        catch (Exception)
                        {
                            con = 0;
                        }

                    }
                }

                if (con!=0)
                {
                    RadWindowManager1.RadAlert("SE GENERO SU PAQUETE No " + NoP + "  Y FUE ENVÍADO A REVISIÓN  FOLIO:" + folio + "", 450, 150, "GENERACIÓN DE PAQUETE", null);
                    LLenarpaquete();
                    rgPaquetePendiente.DataBind();
                    LLenarpaqueteRechazado();
                    rgpaquetesRechazados.DataBind();
                    GenerarPaquete.Enabled = false;
                }
                else
                {
                    LLenarpaquete();
                    rgPaquetePendiente.DataBind();
                    LLenarpaqueteRechazado();
                    rgpaquetesRechazados.DataBind();
                    RadWindowManager1.RadAlert("Error Algunas solicitudes No Fueron Enviadas", 450, 150, "GENERACIÓN DE PAQUETE", null);
                }

        }

        #endregion 

        #region Consulta Paquetes

        #region Atributos
        private List<ConsultaPaquetes> ConsultaPaquete
        {
            get
            {
                return Session["ConsultaPaquete"] == null
                           ? new List<ConsultaPaquetes>()
                           : Session["ConsultaPaquete"] as List<ConsultaPaquetes>;
            }
            set { Session["ConsultaPaquete"] = value; }
        }


        #endregion


        protected void LLenarConsultaPaque()
        {
            ConsultaPaquete = ConsultPaquete.consultar(Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue), RadTxtNoCredit.Text, Convert.ToInt32(RadCbxNopack.SelectedValue == "" ? "0" : RadCbxNopack.SelectedValue), RadCbxFolio.SelectedValue, Convert.ToString(Session["UserName"]));
            rgConsultaPaquete.DataSource = ConsultaPaquete;

            ValidaConsultapakeRadGrid();
        }

        protected void ValidaConsultapakeRadGrid()
        {
            for (int i = 0; i < ConsultaPaquete.Count; i++)
            {
                foreach (GridDataItem dataItem in rgConsultaPaquete.MasterTableView.Items)
                {

                    if (ConsultaPaquete[i].noCredit == rgConsultaPaquete.MasterTableView.Items[dataItem.ItemIndex]["noCredit"].Text)
                    {
                        (dataItem.FindControl("verCarta") as ImageButton).Attributes.Add("OnClick", "poponload('" + ConsultaPaquete[i].noCredit + "','carta','consulta')");
                        (dataItem.FindControl("verCarta") as ImageButton).Visible =ConsultaPaquete[i].carta!=null?true:false;
                        (dataItem.FindControl("lblCarta") as Label).Visible=ConsultaPaquete[i].carta!=null?false:true;

                       (dataItem.FindControl("verActa") as ImageButton).Attributes.Add("OnClick", "poponload('" + ConsultaPaquete[i].noCredit + "','acta','consulta')");
                       (dataItem.FindControl("verActa") as ImageButton).Visible = ConsultaPaquete[i].acta != null ? true : false;
                       (dataItem.FindControl("lblActa") as Label).Visible = ConsultaPaquete[i].acta != null ? false : true;

                       (dataItem.FindControl("lblConFecha") as Label).Visible = ConsultaPaquete[i].fechaRevision != null ? true : false;
                       (dataItem.FindControl("lblSinFecha") as Label).Visible = ConsultaPaquete[i].fechaRevision != null ? false : true;

                    }
                }
            }

        }

        protected void rgConsultaPaquete_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LLenarConsultaPaque();
        }

        protected void rgConsultaPaquete_DataBound(object sender, EventArgs e)
        {
            LLenarConsultaPaque();
        }
        #endregion 

        protected void RadCbxEstatus_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCbxEstatus.SelectedValue == "1" )
            {
                LstDatos = null;
                CirculoCreditTemporal.RemoveAll(l => l.nocredit!= null);
                ConPaquete.Visible = false;
                GenePaquete.Visible = true;
                GeneRechazado.Visible = false;
                


                NoCredit.Visible = false;
                RadTxtNoCredit.Visible = false;
                LblNoPaquete.Visible = false;
                RadCbxNopack.Visible = false;
                lblFolio.Visible = false;
                RadCbxFolio.Visible = false;
                btnBuscar.Visible = false;
                rgPaquetePendiente.DataSource = LstDatos;
                LLenarpaquete();
                rgPaquetePendiente.DataBind();
                
            }
            else if (RadCbxEstatus.SelectedValue == "2" || RadCbxEstatus.SelectedValue == "3" )
            {
                RadCbxFolio.DataSource = ConsultPaquete.catalogoFolioDis(Convert.ToString(Session["UserName"]));
                RadCbxFolio.DataValueField = "FOLIO";
                RadCbxFolio.DataTextField = "FOLIO";
                RadCbxFolio.DataBind();

                RadCbxFolio.Items.Insert(0, new RadComboBoxItem("seleccione"));

                RadCbxNopack.DataSource = ConsultPaquete.PackXFolio(RadCbxFolio.SelectedValue, Convert.ToInt32(RadCbxEstatus.SelectedValue));
                RadCbxNopack.DataValueField = "NO_PAQUETE";
                RadCbxNopack.DataTextField = "NO_PAQUETE";
                RadCbxNopack.DataBind();

                RadCbxNopack.Items.Insert(0, new RadComboBoxItem("seleccione"));

                ConPaquete.Visible = true;
                GenePaquete.Visible = false;
                GeneRechazado.Visible = false;

                NoCredit.Visible = true;
                RadTxtNoCredit.Visible = true;
                LblNoPaquete.Visible = true;
                RadCbxNopack.Visible = true;
                lblFolio.Visible = true;
                RadCbxFolio.Visible = true;
                btnBuscar.Visible = true;
                LLenarConsultaPaque();
                rgConsultaPaquete.DataBind();
            }
            else if (RadCbxEstatus.SelectedValue == "4")
            {
                LstDatos = null;
                CirculoCreditTemporal.RemoveAll(l => l.nocredit != null);
                ConPaquete.Visible = false;
                GenePaquete.Visible = false;
                GeneRechazado.Visible = true;
                LLenarpaqueteRechazado();


                NoCredit.Visible = false;
                RadTxtNoCredit.Visible = false;
                LblNoPaquete.Visible = false;
                RadCbxNopack.Visible = false;
                lblFolio.Visible = false;
                RadCbxFolio.Visible = false;
                btnBuscar.Visible = false;
                GeneRechazado.DataBind();
            }
            else if (RadCbxEstatus.SelectedValue == "")
            {
                RadCbxFolio.DataSource = ConsultPaquete.catalogoFolioDis(Convert.ToString(Session["UserName"]));
                RadCbxFolio.DataValueField = "FOLIO";
                RadCbxFolio.DataTextField = "FOLIO";
                RadCbxFolio.DataBind();

                RadCbxFolio.Items.Insert(0, new RadComboBoxItem("seleccione"));

                RadCbxNopack.DataSource = ConsultPaquete.PackXFolio(RadCbxFolio.SelectedValue, Convert.ToInt32(RadCbxEstatus.SelectedValue == "" ? "0" : RadCbxEstatus.SelectedValue));
                RadCbxNopack.DataValueField = "NO_PAQUETE";
                RadCbxNopack.DataTextField = "NO_PAQUETE";
                RadCbxNopack.DataBind();

                RadCbxNopack.Items.Insert(0, new RadComboBoxItem("seleccione"));

                ConPaquete.Visible = true;
                GenePaquete.Visible = false;
                GeneRechazado.Visible = false;
               

                NoCredit.Visible = true;
                RadTxtNoCredit.Visible = true;
                LblNoPaquete.Visible = true;
                RadCbxNopack.Visible = true;
                lblFolio.Visible = true;
                RadCbxFolio.Visible = true;
                btnBuscar.Visible = true;

                LLenarConsultaPaque();
                rgConsultaPaquete.DataBind();

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LLenarConsultaPaque();
            rgConsultaPaquete.DataBind();
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }


        protected void ValidacionesRadGridRechaza()
        {
            for (int i = 0; i < LstDatos.Count; i++)
            {
                foreach (GridDataItem dataItem in rgpaquetesRechazados.MasterTableView.Items)
                {

                    if (LstDatos[i].Nocredit == rgpaquetesRechazados.MasterTableView.Items[dataItem.ItemIndex]["Nocredit"].Text
                   && LstDatos[i].folioConsulta == rgpaquetesRechazados.MasterTableView.Items[dataItem.ItemIndex]["folioConsulta"].Text
                   && LstDatos[i].statusPaquete == rgpaquetesRechazados.MasterTableView.Items[dataItem.ItemIndex]["statusPaquete"].Text)
                    {
                        (dataItem.FindControl("verCarta") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatos[i].Nocredit + "','carta','carga')");
                        (dataItem.FindControl("verActa") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatos[i].Nocredit + "','acta','carga')");

                        var dif = DateTime.Now - Convert.ToDateTime(rgpaquetesRechazados.MasterTableView.Items[dataItem.ItemIndex]["fechaConsulta"].Text);
                        var t = dif.Days;

                        var credit = CirculoCreditTemporal.Find(o => o.nocredit == rgpaquetesRechazados.MasterTableView.Items[dataItem.ItemIndex]["Nocredit"].Text);

                        //(dataItem.FindControl("chkOperacion") as CheckBox).Checked = t > 10 ? true : false;
                        (dataItem.FindControl("chkOperacionRechaza") as CheckBox).Checked = credit != null ? credit.cheked == true ? true : false : t > 10 ? true : false;


                        if ((dataItem.FindControl("chkOperacionRechaza") as CheckBox).Checked)
                        {
                            TemporalPDFs temp = new TemporalPDFs();
                            temp.nocredit = rgpaquetesRechazados.MasterTableView.Items[dataItem.ItemIndex]["Nocredit"].Text;
                            temp.cheked = t > 10 ? true : false;
                            var credi = CirculoCreditTemporal.Find(o => o.nocredit == rgpaquetesRechazados.MasterTableView.Items[dataItem.ItemIndex]["Nocredit"].Text);

                            if (credi == null)
                            {
                                CirculoCreditTemporal.Add(temp);
                            }
                            else
                            {

                                if (credi.carta != null)
                                {
                                    (dataItem.FindControl("verCarta") as ImageButton).Visible = true;
                                    (dataItem.FindControl("EliminarCarta") as ImageButton).Visible = true;
                                    (dataItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = false;
                                }
                                if (credi.acta != null)
                                {
                                    (dataItem.FindControl("verActa") as ImageButton).Visible = true;
                                    (dataItem.FindControl("EliminarActa") as ImageButton).Visible = true;
                                    (dataItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = false;
                                }

                            }

                        }

                        rgpaquetesRechazados.MasterTableView.Items[dataItem.ItemIndex]["colActa"].Visible = (dataItem.FindControl("chkOperacionRechaza") as CheckBox).Checked == true ? true : false;
                        rgpaquetesRechazados.MasterTableView.Items[dataItem.ItemIndex]["colCarta"].Visible = (dataItem.FindControl("chkOperacionRechaza") as CheckBox).Checked == true ? true : false;
                        (dataItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = (dataItem.FindControl("chkOperacionRechaza") as CheckBox).Checked == true ? true : false; ;
                        (dataItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = (dataItem.FindControl("chkOperacionRechaza") as CheckBox).Checked == true ? true : false; ;
                    }

                }
            }

        }

        protected void LLenarpaqueteRechazado()
        {
            LstDatos  = GeneracionPaquetes.GenerarPakRechazado(Convert.ToString(Session["UserName"]));
            rgpaquetesRechazados.DataSource = LstDatos;

            ValidacionesRadGridRechaza();
            ValidarPaquete();
        }

        protected void rgpaquetesRechazados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LLenarpaqueteRechazado();
        }

        protected void rgpaquetesRechazados_DataBound(object sender, EventArgs e)
        {
            LLenarpaqueteRechazado();
        }

        protected void chkOperacionRechaza_CheckedChanged(object sender, EventArgs e)
        {
            var check = sender as CheckBox;
            if (check != null)
            {
                var editedItem = check.NamingContainer as GridEditableItem;

                var credit = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Nocredit"].ToString();


                if (!(editedItem.FindControl("chkOperacionRechaza") as CheckBox).Checked)
                {
                    //eliminar
                    CirculoCreditTemporal.Find(o => o.nocredit == credit).cheked = false;
                    CirculoCreditTemporal.Find(o => o.nocredit == credit).carta = null;
                    CirculoCreditTemporal.Find(o => o.nocredit == credit).acta = null;

                    (editedItem.FindControl("verActa") as ImageButton).Visible = false;
                    (editedItem.FindControl("EliminarActa") as ImageButton).Visible = false;
                    (editedItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = false;

                    (editedItem.FindControl("verCarta") as ImageButton).Visible = false;
                    (editedItem.FindControl("EliminarCarta") as ImageButton).Visible = false;
                    (editedItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = false;

                }
                else
                {
                    (editedItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = true;
                    (editedItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = true;
                    ///agregar
                    var cre = CirculoCreditTemporal.Find(o => o.nocredit == rgpaquetesRechazados.MasterTableView.Items[editedItem.ItemIndex]["Nocredit"].Text);
                    if (cre == null)
                    {
                        TemporalPDFs temp = new TemporalPDFs();
                        temp.nocredit = credit;
                        temp.cheked = true;
                        CirculoCreditTemporal.Add(temp);
                    }
                    else
                    {
                        CirculoCreditTemporal.Find(o => o.nocredit == credit).cheked = true;
                    }


                }
            }

            ValidarPaquete(); 
        }

        protected void bntGeberarRechazados_Click(object sender, EventArgs e)
        {
            int NoP = GeneracionPaquetes.noPaquete(GeneracionPaquetes.GenerasFolio(Convert.ToInt32(Session["IdUserLogueado"])), Convert.ToString(Session["UserName"]));
            string folio = GeneracionPaquetes.GenerasFolio(Convert.ToInt32(Session["IdUserLogueado"]));

            int con = 0;
            foreach (var dataItem in CirculoCreditTemporal)
            {
                PAQUETES_CIRCULO_CREDITO paquete = null;

                if (dataItem.acta != null || dataItem.carta != null)
                {
                    try
                    {
                        paquete = new PAQUETES_CIRCULO_CREDITO
                        {
                            FOLIO = folio,
                            ADICIONADO_POR = Convert.ToString(Session["UserName"]),
                            NO_PAQUETE = NoP,
                            No_Credito = dataItem.nocredit,
                            ID_ESTATUS_PAQUETE = 2,
                            FECHA_ADICION = DateTime.Now.Date,
                            FECHA_REVISION = null,
                            Carta_Autorizacion = CirculoCreditTemporal.Find(o => o.nocredit == dataItem.nocredit).carta,
                            Acta_Ministerio = CirculoCreditTemporal.Find(o => o.nocredit == dataItem.nocredit).acta,
                            Comentarios = string.Empty

                        };
                        GeneracionPaquetes.Guardapaquete(paquete);
                        con++;
                    }
                    catch (Exception)
                    {
                        con = 0;
                    }

                }
            }

            if (con != 0)
            {
                RadWindowManager1.RadAlert("SE GENERO SU PAQUETE No " + NoP + "  Y FUE ENVÍADO A REVISIÓN  FOLIO:" + folio + "", 450, 150, "GENERACIÓN DE PAQUETE", null);
                
                LLenarpaqueteRechazado();
                rgpaquetesRechazados.DataBind();
                bntGeberarRechazados.Enabled = false;
            }
            else
            {
                RadWindowManager1.RadAlert("Error Algunas solicitudes No Fueron Enviadas", 450, 150, "GENERACIÓN DE PAQUETE", null);
            }

        }

        protected void RadCbxNopack_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
                LLenarConsultaPaque();
                rgConsultaPaquete.DataBind();
        }

        protected void RadCbxFolio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
                RadCbxNopack.DataSource = ConsultPaquete.PackXFolio(RadCbxFolio.SelectedValue,Convert.ToInt32(RadCbxEstatus.SelectedValue));
                RadCbxNopack.DataValueField = "NO_PAQUETE";
                RadCbxNopack.DataTextField = "NO_PAQUETE";
                RadCbxNopack.DataBind();

                RadCbxNopack.Items.Insert(0, new RadComboBoxItem("seleccione"));

                LLenarConsultaPaque();
                rgConsultaPaquete.DataBind();
            
        }

        protected void headerChkbox_CheckedChanged(object sender, EventArgs e)
        {

            int cont = rgPaquetePendiente.Items.Count;

            foreach (GridDataItem item in rgPaquetePendiente.MasterTableView.Items)
            {
                if ((item.FindControl("chkOperacion") as CheckBox).Checked)
                {
                    (item.FindControl("chkOperacion") as CheckBox).Checked = false;
                    var cre = CirculoCreditTemporal.Find(o => o.nocredit == rgPaquetePendiente.MasterTableView.Items[item.ItemIndex]["Nocredit"].Text);
                    rgPaquetePendiente.MasterTableView.Items[item.ItemIndex]["colActa"].Visible = false;
                    rgPaquetePendiente.MasterTableView.Items[item.ItemIndex]["colCarta"].Visible = false;
                    CirculoCreditTemporal.Find(o => o.nocredit == rgPaquetePendiente.MasterTableView.Items[item.ItemIndex]["Nocredit"].Text).cheked = false;
                    CirculoCreditTemporal.Find(o => o.nocredit == rgPaquetePendiente.MasterTableView.Items[item.ItemIndex]["Nocredit"].Text).acta = null;
                    CirculoCreditTemporal.Find(o => o.nocredit == rgPaquetePendiente.MasterTableView.Items[item.ItemIndex]["Nocredit"].Text).carta = null; 
                }

            }

            ValidarPaquete();
        }



    }
}