using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM
{
    public partial class AddRateandCost : System.Web.UI.Page
    {
        /// <summary>
        /// property
        /// </summary>
        public int TarifaID
        {
            get
            {
                return ViewState["TarifaID"] == null ? 0 : int.Parse(ViewState["TarifaID"].ToString());
            }
            set
            {
                ViewState["TarifaID"] = value;
            }
        }

        /// <summary>
        /// Init default data when page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                txtFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                // Init Estado and Tarifa
                BindEstadoAndTarifa();

                if (Request["ID"] != null && Request["ID"].ToString() != "")
                {
                    TarifaID = int.Parse(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["ID"].ToString().Replace("%2B", "+"))));

                    InitTarifaAndCostForEdit();
                }

                EnableTarifa();
            }
        }

        /// <summary>
        /// Load Detail for edit
        /// </summary>
        private void InitTarifaAndCostForEdit()
        {
            DataTable dtTarifaCost = K_TARIFA_COSTODAL.ClassInstance.GetTarifaAndCostByPK(TarifaID);
            if (dtTarifaCost.Rows[0]["Cve_Estado"] != null)
            {
                drpEstado.SelectedValue = dtTarifaCost.Rows[0]["Cve_Estado"].ToString();
            }
            txtPeriodo.Text = Convert.ToDateTime(dtTarifaCost.Rows[0]["Dt_Periodo_Tarifa_Costo"].ToString()).ToString("yyyy-MM");
            drpTarifa.SelectedValue = dtTarifaCost.Rows[0]["Cve_Tarifa"].ToString();
            txtCostoFijo.Text = dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Fijo"].ToString();
            txtCostoBasico.Text = dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Basico"].ToString();
            txtCostoIntermedio.Text = dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Intermedio"].ToString();
            txtCostoExcedente.Text = dtTarifaCost.Rows[0]["Mt_Costo_Kw_h_Excedente"].ToString();

            // Update by Tina 2011/08/03
            drpEstado.Enabled = false;
            txtPeriodo.Enabled = false;
            // End
            // Add by Tina 2011/08/11
            drpTarifa.Enabled = false;
            // End

            txtTarifaDemanda.Text = dtTarifaCost.Rows[0]["Mt_Tarifa_Demanda"].ToString();
            txtCostoTarifaConsumo.Text = dtTarifaCost.Rows[0]["Mt_Costo_Tarifa_Consumo"].ToString();
            EnableTarifa();
        }

        private void EnableTarifa()
        {
            if (drpTarifa.SelectedIndex > 0 && (drpTarifa.SelectedItem.Text == "OM" || drpTarifa.SelectedItem.Text == "HM"))
            {
                txtTarifaDemanda.Enabled = true;
                txtCostoTarifaConsumo.Enabled = true;
                RequiredFieldValidatorTarifaDemanda.Enabled = true;
                RegularExpressionValidatorTarifaDemanda.Enabled = true;
                RequiredFieldValidatorTarifaConsumo.Enabled = true;
                RegularExpressionValidatorTarifaConsumo.Enabled = true;

                txtCostoFijo.Enabled = false;
                RequiredFieldValidator1.Enabled = false;
                RegularExpressionValidator5.Enabled = false;
                txtCostoBasico.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                RegularExpressionValidator1.Enabled = false;
                txtCostoIntermedio.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                RegularExpressionValidator2.Enabled = false;
                txtCostoExcedente.Enabled = false;
                RequiredFieldValidator4.Enabled = false;
                RegularExpressionValidator3.Enabled = false;
            }
            else
            {
                txtTarifaDemanda.Enabled = false;
                txtCostoTarifaConsumo.Enabled = false;
                RequiredFieldValidatorTarifaDemanda.Enabled = false;
                RegularExpressionValidatorTarifaDemanda.Enabled = false;
                RequiredFieldValidatorTarifaConsumo.Enabled = false;
                RegularExpressionValidatorTarifaConsumo.Enabled = false;

                txtCostoFijo.Enabled = true;
                RequiredFieldValidator1.Enabled = true;
                RegularExpressionValidator5.Enabled = true;
                txtCostoBasico.Enabled = true;
                RequiredFieldValidator2.Enabled = true;
                RegularExpressionValidator1.Enabled = true;
                txtCostoIntermedio.Enabled = true;
                RequiredFieldValidator3.Enabled = true;
                RegularExpressionValidator2.Enabled = true;
                txtCostoExcedente.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                RegularExpressionValidator3.Enabled = true;
            }
        }
        
        /// <summary>
        /// Load estado and tarifa
        /// </summary>
        private void BindEstadoAndTarifa()
        {
            InitEstado();
            InitTarifa();
        }
       
        /// <summary>
        /// Init Estado
        /// </summary>
        private void InitEstado()
        {
            DataTable dtEstado = CAT_ESTADOBLL.ClassInstance.Get_All_CAT_ESTADO();
            if (dtEstado != null)
            {
                this.drpEstado.DataSource = dtEstado;
                this.drpEstado.DataTextField = "Dx_Nombre_Estado";
                this.drpEstado.DataValueField = "Cve_Estado";
                this.drpEstado.DataBind();
                this.drpEstado.Items.Insert(0, "");
                this.drpEstado.SelectedIndex = 0;
            }
        }
       
        /// <summary>
        /// Init Tarifa
        /// </summary>
        private void InitTarifa()
        {
            DataTable dtTarifa = CAT_TARIFABLL.ClassInstance.Get_All_CAT_TARIFA();
            if (dtTarifa != null)
            {
                this.drpTarifa.DataSource = dtTarifa;
                this.drpTarifa.DataTextField = "Dx_Tarifa";
                this.drpTarifa.DataValueField = "Cve_Tarifa";
                this.drpTarifa.DataBind();
                this.drpTarifa.Items.Insert(0, "");
                this.drpTarifa.SelectedIndex = 0;
            }
        }
       
        /// <summary>
        /// Save tarifa and cost data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            K_TARIFA_COSTOModel model = new K_TARIFA_COSTOModel();

            if (Page.IsValid)
            {
                // Update by Tina 2011/08/02
                if (drpEstado.SelectedIndex != 0 && drpEstado.SelectedIndex != -1)
                {
                    if ((drpTarifa.SelectedIndex != 0 && drpTarifa.SelectedIndex != -1))
                    {
                        LoadDataFromUI(out model);

                        // edit
                        if (Request["ID"] != null)
                        {
                            result = K_TARIFA_COSTODAL.ClassInstance.UpdateTarifaAndCost(model);
                        }
                        // add
                        else
                        {
                            // Validate duplicate information By Tarifa and Period
                            // Update by Tina 2011/08/11
                            //DataTable dtTarifaCost = K_TARIFA_COSTODAL.ClassInstance.GetTarifaAndCostByEstadoAndPeriod(int.Parse(drpEstado.SelectedValue), Convert.ToDateTime(txtPeriodo.Text.Trim()));
                            DataTable dtTarifaCost = K_TARIFA_COSTODAL.ClassInstance.GetTarifaAndCostWithDateEstado(drpTarifa.SelectedItem.Text, int.Parse(drpEstado.SelectedValue), Convert.ToDateTime(txtPeriodo.Text.Trim()));
                            // End
                            if (dtTarifaCost == null || dtTarifaCost.Rows.Count <= 0)
                            {
                                // Perform the saving
                                result = K_TARIFA_COSTOBLL.ClassInstance.Insert_K_TARIFA_COSTO(model);
                                CleanData();
                            }
                            else
                            {
                                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgTarifaExists") as string;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveError", "confirm('" + strMsg + "');", true);
                            }
                        }
                        if (result > 0)
                        {
                            string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgSavesuccessfully") as string;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveSucess", "confirm('" + strMsg + "');", true);
                        }
                    }
                    else
                    {
                        string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "mgPleasechooseatarifa") as string;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveError", "alert('" + strMsg + "');", true);
                        drpTarifa.Focus();
                    }
                }
                else
                {
                    string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "mgPleasechooseaestado") as string;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveError", "alert('" + strMsg + "');", true);
                    drpEstado.Focus();
                }
                // End
            }
        }
       
        /// <summary>
        /// get the data for save
        /// </summary>
        private void LoadDataFromUI(out K_TARIFA_COSTOModel model)
        {
            model = new K_TARIFA_COSTOModel();
            if (!string.IsNullOrEmpty(this.drpEstado.SelectedValue))
            {
                model.Cve_Estado = Convert.ToInt32(this.drpEstado.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtPeriodo.Text))
            {
                model.Dt_Periodo_Tarifa_Costo = Convert.ToDateTime(txtPeriodo.Text.Trim() + "-01");
            }
            if (!string.IsNullOrEmpty(this.drpTarifa.SelectedValue))
            {
                model.Cve_Tarifa = Convert.ToInt32(this.drpTarifa.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.txtCostoFijo.Text))
            {
                model.Mt_Costo_Kw_h_Fijo = Convert.ToDecimal(txtCostoFijo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtCostoBasico.Text))
            {
                model.Mt_Costo_Kw_h_Basico = Convert.ToDecimal(txtCostoBasico.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtCostoIntermedio.Text))
            {
                model.Mt_Costo_Kw_h_Intermedio = Convert.ToDecimal(txtCostoIntermedio.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtCostoExcedente.Text))
            {
                model.Mt_Costo_Kw_h_Excedente = Convert.ToDecimal(txtCostoExcedente.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtTarifaDemanda.Text))
            {
                model.MT_Tarifa_Demanda = Convert.ToDecimal(txtTarifaDemanda.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtCostoTarifaConsumo.Text))
            {
                model.MT_Costo_Tarifa_Consumo = Convert.ToDecimal(txtCostoTarifaConsumo.Text.Trim());
            }
            model.Dt_Fecha_UltMod = DateTime.Now;
            if (Request["ID"] != null)
            {
                model.Fl_Tarifa_Costo = TarifaID;
            }
        }
        
        /// <summary>
        /// Clean data to init controls to default status
        /// </summary>
        private void CleanData()
        {
            this.txtCostoBasico.Text = "";
            this.txtCostoExcedente.Text = "";
            this.txtCostoFijo.Text = "";
            this.txtCostoIntermedio.Text = "";
            this.txtPeriodo.Text = "";
            this.drpEstado.SelectedIndex = 0;
            this.drpTarifa.SelectedIndex = 0;
            this.txtTarifaDemanda.Text = "";
            this.txtCostoTarifaConsumo.Text = "";
        }
       
        /// <summary>
        /// Cancel to return back manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGo_Click(object sender, EventArgs e)
        {
            Response.Redirect("RateandCostManager.aspx");
        }

        protected void drpTarifa_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableTarifa();
        }
    }
}
