using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;

namespace PAEEEM.CentralModule
{
    public partial class DynamicPageRender : System.Web.UI.Page
    {
        private const int COLUMN_COUNT_PER_ROW = 6;
        private const string PHONE_FORMAT_EXPRE = @"^(\d{10})$";
        private const string EMAIL_FORMAT_EXPRE = @"^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$";
        private const string ZIP_FORMAT_EXPRE = @"^(\d{5})$";

        private DataTable OriginalRecord
        {
            get
            {
                if (ViewState["OriginalRecord"] != null)
                {
                    return (DataTable)ViewState["OriginalRecord"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["OriginalRecord"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
            }
            RenderControlsBasedOnConfigurations();

            LoadData();
        }
        /// <summary>
        /// Edit mode should transfer query string: table name, primary key field name, 
        /// </summary>
        private void LoadData()
        {
            if (IsEdit())
            {
                string tableName = Request.QueryString["CatalogName"];
                string dataKeyField = Request.QueryString["dataKeyField"];
                string dataKeyValue = Request.QueryString["dataKeyValue"];

                DataTable catalogRecords = GlossaryDal.ClassInstance.GetCatalogRecordsByTableNameAndPrimaryKey(tableName, dataKeyField, dataKeyValue);

                List<GlossaryField> fields = GlossaryDal.ClassInstance.GetConfigurations(tableName);

                if (catalogRecords != null)
                {
                    foreach (GlossaryField field in fields)
                    {
                        if (!field.IsIdentity)
                        {
                            if (field.IsForeign)
                            {
                                Control ctrlDropDownList = this.ContainerHolder.FindControl("drp" + field.ColumnName/*field.ParentColumn*/);//Changed by Jerry
                                if (ctrlDropDownList is DropDownList)
                                {
                                    ((DropDownList)ctrlDropDownList).SelectedValue = catalogRecords.Rows[0][field.ColumnName].ToString();//Changed by Jerry
                                }
                            }
                            else
                            {
                                Control ctrlTextBox = this.ContainerHolder.FindControl("txt" + field.ColumnName);
                                if (ctrlTextBox is TextBox)
                                {
                                    string value = catalogRecords.Rows[0][field.ColumnName].ToString();
                                    if (field.DataType == "date" || field.DataType == "datetime")
                                    {
                                        DateTime temp;
                                        DateTime.TryParse(value, out temp);
                                        ((TextBox)ctrlTextBox).Text = string.Format("{0:yyyy/MM/dd}", temp);
                                    }
                                    else
                                    {
                                        ((TextBox)ctrlTextBox).Text = value;
                                    }
                                }
                            }
                        }
                    }

                    //Cache original record
                    OriginalRecord = catalogRecords;
                }
            }
        }

        private bool IsEdit()
        {
            bool result = false;

            if (!string.IsNullOrEmpty(Request.QueryString["dataKeyField"]) && !string.IsNullOrEmpty(Request.QueryString["dataKeyValue"]))
            {
                result = true;
            }

            return result;
        }

        private void RenderControlsBasedOnConfigurations()
        {
            string tableName = Request.QueryString["CatalogName"];
            if (!string.IsNullOrEmpty(tableName))
            {
                List<GlossaryField> fields = GlossaryDal.ClassInstance.GetConfigurations(tableName);
                List<Control> controlCollection = new List<Control>();

                foreach (GlossaryField field in fields)
                {
                    if (!field.IsIdentity)
                    {
                        //render labels
                        Label lblLabel = new Label();
                        if (field.DisplayName != "")
                        {
                            lblLabel.Text = field.DisplayName;
                        }
                        else
                        {
                            lblLabel.Text = field.ColumnName;
                        }
                        controlCollection.Add(lblLabel);

                        //render comboboxes
                        if (field.IsForeign)
                        {
                            DropDownList drpParent = BindParentTable(field);
                            drpParent.Width = new Unit("100%");
                            drpParent.ID = "drp" + field.ColumnName;//field.ParentColumn;//Changed by Jerry 
                            controlCollection.Add(drpParent);
                        }
                        else
                        {
                            //render textboxs
                            TextBox txtField = new TextBox();
                            txtField.ID = "txt" + field.ColumnName;
                            controlCollection.Add(txtField);
                            FormatTextBoxString(field, txtField);
                        }
                    }
                }

                //render controls in table format
                RenderTable(controlCollection);
            }
        }

        private void ShowWarningMessage(string message)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"alert('" + message + "');");//Some fields are not entered or in wrong format.
            sb.Append(@"</script>");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "warning", sb.ToString(), false);

        }

        private void RenderTable(List<Control> controlCollection)
        {
            if (controlCollection.Count > 0)
            {
                Table tableContainer = new Table();
                int modWhenTotalControlsDivideColumnsPerRow = controlCollection.Count % COLUMN_COUNT_PER_ROW;
                int totalControlsDivideColumnsPerRow = controlCollection.Count / COLUMN_COUNT_PER_ROW;
                int totalTableRowCount = modWhenTotalControlsDivideColumnsPerRow != 0 ? totalControlsDivideColumnsPerRow + 1 : totalControlsDivideColumnsPerRow;

                for (int i = 0; i < totalTableRowCount; i++)
                {
                    TableRow tableRow = new TableRow();
                    for (int j = 0; j < COLUMN_COUNT_PER_ROW; j++)
                    {
                        int curIndex = (COLUMN_COUNT_PER_ROW * i) + j;
                        if (curIndex <= (controlCollection.Count - 1))
                        {
                            TableCell cell = new TableCell();
                            cell.Controls.Add(controlCollection[curIndex]);

                            tableRow.Cells.Add(cell);
                        }
                    }

                    tableContainer.Rows.Add(tableRow);
                }
                this.ContainerHolder.Controls.Add(tableContainer);
            }
        }

        private void FormatTextBoxString(GlossaryField field, TextBox txtField)
        {
            switch (field.DataType)
            {
                case "nvarchar":
                case "varchar":
                    break;
                case "float":
                case "decimal":
                    txtField.ToolTip = "Numeric (.##)";
                    break;
                case "money":
                    txtField.ToolTip = "Numeric (.##)";
                    break;
                case "date":
                    txtField.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    txtField.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private DropDownList BindParentTable(GlossaryField field)
        {
            DropDownList drpParent = new DropDownList();
            System.Data.DataTable parentTables = GlossaryDal.ClassInstance.GetRecordsWithTableNameAndColumns(field.ParentTable, field.DisplayColumn, field.ParentColumn);

            drpParent.DataSource = parentTables;
            drpParent.DataTextField = field.DisplayColumn;
            drpParent.DataValueField = field.ParentColumn;
            drpParent.DataBind();
            drpParent.Items.Insert(0, "");

            return drpParent;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            string tableName = Request.QueryString["CatalogName"];
            List<GlossaryField> fields = GlossaryDal.ClassInstance.GetConfigurations(tableName);

            if (Validation(fields))
            {
                Dictionary<string, object> fieldValues = GetData(fields);
                if (!IsEdit())
                {
                    result = InsertRecord(fieldValues);
                }
                else
                {
                    if (IsChanged(fieldValues))
                    {
                        result = UpdateRecord(fieldValues);
                    }
                }

                if (result > 0)
                {
                    if (!IsEdit())
                    {
                        CleanData(fields);
                    }
                }
            }
            else
            {
                ShowWarningMessage("Algunos campos no fueron alimentados o su formato es incorrecto.");
            }
        }

        private bool IsChanged(Dictionary<string, object> keyValuePairs)
        {
            bool isChanged = false;
            DataTable originalRecord = ViewState["OriginalRecord"] as DataTable;

            foreach (string columnName in keyValuePairs.Keys)
            {
                string value = originalRecord.Rows[0][columnName].ToString();
                DateTime temp1;
                float temp2;
                if (DateTime.TryParse(value, out temp1) && !float.TryParse(value, out temp2))
                {
                    value = string.Format("{0:yyyy/MM/dd}", temp1);
                }

                if ((keyValuePairs[columnName] != null ? keyValuePairs[columnName].ToString() : "") != value)
                {
                    isChanged = true;
                    break;
                }
            }

            return isChanged;
        }

        private int InsertRecord(Dictionary<string, object> keyValuePairs)
        {
            int result = 0;

            string tableName = Request.QueryString["CatalogName"];

            result = GlossaryDal.ClassInstance.InsertCatalog(tableName, keyValuePairs);

            return result;
        }

        private int UpdateRecord(Dictionary<string, object> keyValuePairs)
        {
            int result = 0;

            string tableName = Request.QueryString["CatalogName"];
            string dataKeyField = Request.QueryString["dataKeyField"];
            string dataKeyValue = Request.QueryString["dataKeyValue"];

            result = GlossaryDal.ClassInstance.UpdateCatalog(tableName, keyValuePairs, dataKeyField, dataKeyValue);

            return result;
        }

        private void CleanData(List<GlossaryField> fields)
        {
            foreach (GlossaryField field in fields)
            {
                if (!field.IsIdentity)
                {
                    Control ctrl;
                    //get the control in page
                    if (field.IsForeign)
                    {
                        ctrl = this.ContainerHolder.FindControl("drp" + field.ColumnName/*field.ParentColumn*/);//Changed by Jerry
                    }
                    else
                    {
                        ctrl = this.ContainerHolder.FindControl("txt" + field.ColumnName);
                    }

                    if (ctrl is DropDownList)
                    {
                        ((DropDownList)ctrl).SelectedIndex = 0;
                    }
                    else
                    {
                        DateTime temp;
                        if (!DateTime.TryParse(((TextBox)ctrl).Text, out temp))
                        {
                            ((TextBox)ctrl).Text = "";
                        }
                    }
                }
            }
        }

        private Dictionary<string, object> GetData(List<GlossaryField> fields)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            foreach (GlossaryField field in fields)
            {
                if (!field.IsIdentity)
                {
                    Control ctrl;
                    //get the control in page
                    if (field.IsForeign)
                    {
                        ctrl = this.ContainerHolder.FindControl("drp" + field.ColumnName/*field.ParentColumn*/);//Changed by Jerry
                    }
                    else
                    {
                        ctrl = this.ContainerHolder.FindControl("txt" + field.ColumnName);
                    }

                    if (ctrl is TextBox)
                    {
                        if (field.DataType == "float" || field.DataType == "decimal" || field.DataType == "money" || field.DataType =="int")
                        {
                            keyValuePairs.Add(field.ColumnName, ((TextBox)ctrl).Text.Trim() != "" ? ((TextBox)ctrl).Text.Trim() : null);
                        }
                        else
                        {
                            keyValuePairs.Add(field.ColumnName, string.IsNullOrEmpty(((TextBox)ctrl).Text) ? null : ((TextBox)ctrl).Text.Trim());
                        }
                    }
                    else
                    {
                        keyValuePairs.Add(field.ColumnName, (((DropDownList)ctrl).SelectedIndex != -1 && ((DropDownList)ctrl).SelectedIndex != 0) ? ((DropDownList)ctrl).SelectedValue : null);
                    }
                }
            }

            return keyValuePairs;
        }

        bool Validation(List<GlossaryField> fields)
        {
            bool isValid = true;
            Control ctrl;

            foreach (GlossaryField field in fields)
            {
                //get the control in page
                if (field.IsForeign)
                {
                    ctrl = this.ContainerHolder.FindControl("drp" + field.ColumnName/*field.ParentColumn*/);//Changed by Jerry
                }
                else
                {
                    ctrl = this.ContainerHolder.FindControl("txt" + field.ColumnName);
                }

                //check mandatory field
                if (field.IsMandatory)
                {
                    if (ctrl is DropDownList)
                    {
                        if (((DropDownList)ctrl).SelectedIndex == -1 || ((DropDownList)ctrl).SelectedIndex == 0)
                        {
                            isValid = false;
                            break;
                        }
                    }
                    else
                    {
                        if (((TextBox)ctrl).Text.Trim() == "")
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

                //check email field
                if (field.IsEmail)
                {
                    if (ctrl is TextBox && !string.IsNullOrEmpty(((TextBox)ctrl).Text.Trim()) && !Regex.IsMatch(((TextBox)ctrl).Text.Trim(), EMAIL_FORMAT_EXPRE))
                    {
                        isValid = false;
                        break;
                    }
                }

                //check telephone field
                if (field.IsPhone)
                {
                    if (ctrl is TextBox && !string.IsNullOrEmpty(((TextBox)ctrl).Text.Trim()) && !Regex.IsMatch(((TextBox)ctrl).Text.Trim(), PHONE_FORMAT_EXPRE))
                    {
                        isValid = false;
                        break;
                    }
                }

                //check zip code field
                if (field.IsZip)
                {
                    if (ctrl is TextBox && !string.IsNullOrEmpty(((TextBox)ctrl).Text.Trim()) && !Regex.IsMatch(((TextBox)ctrl).Text.Trim(), ZIP_FORMAT_EXPRE))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (field.DataType == "float" || field.DataType == "decimal" || field.DataType == "money")
                {
                    float temp;
                    if (!string.IsNullOrEmpty(((TextBox)ctrl).Text.Trim()) && !float.TryParse(((TextBox)ctrl).Text.Trim(), out temp))
                    {
                        isValid = false;
                        break;
                    }
                }

                // added by Tina 2012-02-24
                if (!field.IsForeign && field.DataType == "int")
                {
                    if (ctrl is TextBox && ((TextBox)ctrl).Text.Trim() != "")
                    {
                        int temp;
                        if (!int.TryParse(((TextBox)ctrl).Text.Trim(), out temp))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
            }

            return isValid;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["CatalogName"]))
            {
                Response.Redirect("CatalogManagement.aspx?CatalogName=" + Request.QueryString["CatalogName"].ToString());
            }
        }
    }
}
