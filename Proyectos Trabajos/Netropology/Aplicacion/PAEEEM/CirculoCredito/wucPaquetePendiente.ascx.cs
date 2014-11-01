using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.CirculoCredito;
using PAEEEM.LogicaNegocios.CirculoCredito;
using Telerik.Web.UI;
using PAEEEM.Entidades;
using PAEEEM.Entities;

namespace PAEEEM.CirculoCredito
{
    public partial class wucPaquetePendiente : System.Web.UI.UserControl
    {

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LLenarpaquete();
                //
                CirculoCreditTemporal = new List<TemporalPDFs>();
            }
            
        }

        protected void LLenarpaquete()
        {
            //LstDatos = GeneracionPaquetes.GenerarPak();
            //rgPaquetePendiente.DataSource = LstDatos;
            
            //ValidacionesRadGrid();
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
                        (dataItem.FindControl("verCarta") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatos[i].Nocredit + "','carta')");
                        (dataItem.FindControl("verActa") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatos[i].Nocredit + "','acta')");

                        var dif = DateTime.Now - Convert.ToDateTime(rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["fechaConsulta"].Text);
                        var t = dif.Days;
                            (dataItem.FindControl("chkOperacion") as CheckBox).Checked = t > 10 ? true : false;
                            if ((dataItem.FindControl("chkOperacion") as CheckBox).Checked)
                            {
                                TemporalPDFs temp = new TemporalPDFs();
                                temp.nocredit = rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["Nocredit"].Text;
                                var credi=CirculoCreditTemporal.Find(o=>o.nocredit==rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["Nocredit"].Text);
                                if (credi==null)
                                {
                                    CirculoCreditTemporal.Add(temp);
                                }
                                else
                                {
                                    if (credi.carta!=null)
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

                            rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["colActa"].Enabled = (dataItem.FindControl("chkOperacion") as CheckBox).Checked == true ? true : false;
                            rgPaquetePendiente.MasterTableView.Items[dataItem.ItemIndex]["colCarta"].Enabled = (dataItem.FindControl("chkOperacion") as CheckBox).Checked == true ? true : false;

                    }
                }
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
           
            var Upload= sender as RadAsyncUpload;
            if (Upload !=null)
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
		 
	        }

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
                CirculoCreditTemporal.Find(o => o.nocredit == nocredit).carta=null;

                if (CirculoCreditTemporal.Find(o => o.nocredit == nocredit).carta==null)
                {
                    (item.FindControl("verCarta") as ImageButton).Visible = false;
                    (item.FindControl("EliminarCarta") as ImageButton).Visible = false;
                    (item.FindControl("UploadedCarta") as RadAsyncUpload).Visible = true;
                }
            }
        }

        protected void EliminarActa_Click(object sender, ImageClickEventArgs e)
        {
            var eliminar = sender as ImageButton;
            if (eliminar != null)
            {
                var item = eliminar.NamingContainer as GridEditableItem;
                var credit = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Nocredit"].ToString();
                CirculoCreditTemporal.Find(o => o.nocredit == credit).acta=null;
              
                if (CirculoCreditTemporal.Find(o => o.nocredit == credit).acta==null)
                {
                    (item.FindControl("verActa") as ImageButton).Visible = false;
                    (item.FindControl("EliminarActa") as ImageButton).Visible = false;
                    (item.FindControl("UploadedActa") as RadAsyncUpload).Visible = true;
                }
            }
        }

        protected void chkOperacion_CheckedChanged(object sender, EventArgs e)
        {
            var check = sender as CheckBox;
            if (check != null)
            {
                var editedItem = check.NamingContainer as GridEditableItem;
                rgPaquetePendiente.MasterTableView.Items[editedItem.ItemIndex]["colActa"].Enabled = (editedItem.FindControl("chkOperacion") as CheckBox).Checked == true ? true : false;
                rgPaquetePendiente.MasterTableView.Items[editedItem.ItemIndex]["colCarta"].Enabled = (editedItem.FindControl("chkOperacion") as CheckBox).Checked == true ? true : false;

                var credit = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Nocredit"].ToString();

                if (!(editedItem.FindControl("chkOperacion") as CheckBox).Checked)
                {
                    //eliminar
                    CirculoCreditTemporal.Remove(CirculoCreditTemporal.Find(o => o.nocredit == credit));

                    (editedItem.FindControl("verActa") as ImageButton).Visible = false;
                    (editedItem.FindControl("EliminarActa") as ImageButton).Visible = false;
                    (editedItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = true;

                    (editedItem.FindControl("verCarta") as ImageButton).Visible = false;
                    (editedItem.FindControl("EliminarCarta") as ImageButton).Visible = false;
                    (editedItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = true;

                }
                else
                {
                    ///agregar

                    TemporalPDFs temp = new TemporalPDFs();
                    temp.nocredit = credit;
                    CirculoCreditTemporal.Add(temp);

                }




            }
        }

        protected void GenerarPaquete_Click(object sender, EventArgs e)
        {
         
        }

        //protected void rgPaquetePendiente_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        var dataItem = (GridDataItem)e.Item;
        //        var credit = dataItem.GetDataKeyValue("Nocredit").ToString();

        //        var carta = LstDatos.First(me => me.Nocredit == credit).carta;
        //        var acta = LstDatos.First(me => me.Nocredit == credit).acta;
        //        var lisCredit=LstDatos.First(me => me.Nocredit == credit).Nocredit;

        //        if (lisCredit==credit)
        //        {
        //            (dataItem.FindControl("verCarta") as ImageButton).Attributes.Add("OnClick", "poponload('" + LstDatos[dataItem.ItemIndex].Nocredit + "')");
 
        //        }

        //            if (carta == "si")
        //            {
        //                (dataItem.FindControl("verCarta") as ImageButton).Visible = true;
        //                (dataItem.FindControl("EliminarCarta") as ImageButton).Visible = true;
        //                (dataItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = false;
        //            }
        //            else
        //            {
        //                (dataItem.FindControl("UploadedCarta") as RadAsyncUpload).Visible = true;
        //                (dataItem.FindControl("verCarta") as ImageButton).Visible = false;
        //                (dataItem.FindControl("EliminarCarta") as ImageButton).Visible = false;
        //            }

        //            if (acta == "si")
        //            {
        //                (dataItem.FindControl("verActa") as ImageButton).Visible = true;
        //                (dataItem.FindControl("EliminarActa") as ImageButton).Visible = true;
        //                (dataItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = false;
        //            }
        //            else
        //            {
        //                (dataItem.FindControl("UploadedActa") as RadAsyncUpload).Visible = true;
        //                (dataItem.FindControl("verActa") as ImageButton).Visible = false;
        //                (dataItem.FindControl("EliminarActa") as ImageButton).Visible = false;
        //            }

        //    }

        //}






        //protected void verCarta_Click(object sender, ImageClickEventArgs e)
        //{

        //    var ver = sender as ImageButton;
        //    if (ver != null)
        //    {
        //        var Item = ver.NamingContainer as GridEditableItem;
        //        var noCredit = Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Nocredit"].ToString();
        //        HidPdf.Value = noCredit;

        //        ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", "window.open('VisorImagenes.aspx?Id='" + noCredit + " '', '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

        //    }



        //    Response.Redirect("~/CirculoCredito/VisorImagenes.aspx");
        //}





 

    










    }
}