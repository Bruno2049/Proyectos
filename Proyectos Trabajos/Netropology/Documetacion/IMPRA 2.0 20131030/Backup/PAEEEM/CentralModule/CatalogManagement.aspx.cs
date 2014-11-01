using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM.CentralModule
{
    public partial class CatalogManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                InitializeCatalogOptions();

                RestoreOriginalOption();
            }
        }

        private void RestoreOriginalOption()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["CatalogName"]))
            {
                drpCatalog.SelectedValue = Request.QueryString["CatalogName"];
                this.drpCatalog_SelectedIndexChanged(drpCatalog, new EventArgs());
            }
        }
        
        private void InitializeCatalogOptions()
        {
            List<string> catalogs = GlossaryDal.ClassInstance.GetConfiguredCatalogs();
            this.drpCatalog.DataSource = catalogs;
            this.drpCatalog.DataBind();
            this.drpCatalog.Items.Insert(0, new ListItem());
        }

        protected void drpCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpCatalog.SelectedIndex != 0 && drpCatalog.SelectedIndex != -1 && drpCatalog.SelectedValue != "")
            {
                btnAdd.Visible = true;

                InitializeDataGridView(drpCatalog.SelectedValue.Trim());
                this.AspNetPager.Visible = true;//Added 2012-02-28
            }
            else
            {
                btnAdd.Visible = false;

                gridViewCatalogRecords.DataSource = null;
                gridViewCatalogRecords.DataBind();
                gridViewCatalogRecords.Columns.Clear();
                this.AspNetPager.Visible = false;//Added 2012-02-28
            }
        }

        private void InitializeDataGridView(string TableName)
        {            
            CleanOriginalGridViewColumns();

            //Get Field List
            List<GlossaryField> glossaryField = GlossaryDal.ClassInstance.GetConfigurations(TableName);

            //get Datakey
            string keyValueList = "";
            foreach (GlossaryField glf in glossaryField)
            {
                if (glf.IsPrimary)
                {
                    keyValueList = keyValueList + glf.ColumnName + ",";
                }
                if (glf.IsDisplay)//bound Field
                {
                    BoundField boundField = new BoundField();
                    boundField.HeaderText = glf.DisplayName;
                    boundField.DataField = glf.ColumnName;
                    gridViewCatalogRecords.Columns.Add(boundField);
                }
            }
            keyValueList = keyValueList.TrimEnd(new char[] { ',' });
            //set datakey for gridview
            gridViewCatalogRecords.DataKeyNames = keyValueList.Split(','); // updated by tina 2012-02-27
            //edit button
            ButtonField EditButtonField = new ButtonField();
            EditButtonField.HeaderText = "Editar";
            EditButtonField.Text = "Editar";
            EditButtonField.ButtonType = ButtonType.Button;
            EditButtonField.CommandName = "Edit";
            gridViewCatalogRecords.Columns.Add(EditButtonField);
            //delete button
            ButtonField DeleteButtonField = new ButtonField();
            DeleteButtonField.HeaderText = "borrar";
            DeleteButtonField.Text = "borrar";
            DeleteButtonField.ButtonType = ButtonType.Button;
            DeleteButtonField.CommandName = "Delete";
            gridViewCatalogRecords.Columns.Add(DeleteButtonField);

            // updated by tina 2012-02-27
            int pageCount = 0;
            this.AspNetPager.CurrentPageIndex = 1;
            gridViewCatalogRecords.DataSource = GlossaryDal.ClassInstance.GetCatalogRecordsWithPage(TableName, keyValueList, 1, this.AspNetPager.PageSize, out pageCount);
            AspNetPager.RecordCount = pageCount;
            gridViewCatalogRecords.DataBind();
            // end
        }

        private void CleanOriginalGridViewColumns()
        {
            this.gridViewCatalogRecords.Columns.Clear();
        }
        // Added by tina 2012-02-27
        private void RefreshDataGridView()
        {
            int pageCount = 0;
            gridViewCatalogRecords.DataSource = GlossaryDal.ClassInstance.GetCatalogRecordsWithPage(drpCatalog.SelectedValue.Trim(), gridViewCatalogRecords.DataKeyNames.ToString(), this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out pageCount);
            AspNetPager.RecordCount = pageCount;
            gridViewCatalogRecords.DataBind();
        }

        protected void gridViewCatalogRecords_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewCommandEventArgs args = new GridViewCommandEventArgs(e.Row, e.Row.Cells[e.Row.Cells.Count - 1].Controls[0], new CommandEventArgs("Delete", null));
                ((Button)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0]).Attributes.Add("onclick", "if(confirm('confirmar la eliminación?')){ __doPostBack('gridViewCatalogRecords','$" + args + "')}else{return false;}");
            }
        }

        protected void gridViewCatalogRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string TableName = drpCatalog.SelectedValue.Trim();
                List<GlossaryField> glossaryField = GlossaryDal.ClassInstance.GetConfigurations(TableName);
                DataTable catalogRecords = null;
                int startIndex = 0;

                foreach (GlossaryField field in glossaryField)
                {
                    if (field.IsDisplay)
                    {
                        //Change Foreign column value
                        if (field.IsForeign)
                        {
                            if (e.Row.Cells[startIndex].Text.Replace("&nbsp;", "") != "")
                            {
                                catalogRecords = GlossaryDal.ClassInstance.GetCatalogRecords(field.ParentTable);
                                catalogRecords.DefaultView.RowFilter = field.ParentColumn + "=" + e.Row.Cells[startIndex].Text;
                                e.Row.Cells[startIndex].Text = catalogRecords.DefaultView.ToTable().Rows[0][field.DisplayColumn].ToString();
                            }
                        }
                        else
                        {
                            //Format datetime
                            if (field.DataType == "date" || field.DataType == "datetime")
                            {
                                if (e.Row.Cells[startIndex].Text.Replace("&nbsp;", "") != "")
                                {
                                    e.Row.Cells[startIndex].Text = DateTime.Parse(e.Row.Cells[startIndex].Text).ToString("yyyy/MM/dd");
                                }
                            }
                        }
                        startIndex++;
                    }
                }
            }
        }
        protected void gridViewCatalogRecords_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //delete keyField List and key field value list
                List<string> dataKeyFieldList = new List<string>();
                List<string> dataKeyValueList = new List<string>();
              
                //Get Data key values
                string[] dataKeyNames = gridViewCatalogRecords.DataKeyNames;
                foreach (string key in dataKeyNames)
                {
                    dataKeyFieldList.Add(key);
                    dataKeyValueList.Add(gridViewCatalogRecords.DataKeys[e.RowIndex].Values[key].ToString());
                }

                //get Delete table Name
                string tableName = drpCatalog.SelectedValue.ToString();

                //delete data
                if (dataKeyNames.Length > 0)
                {
                    int result = GlossaryDal.ClassInstance.DeleteCatalogRecords(tableName, dataKeyFieldList, dataKeyValueList);
                    if (result > 0)
                    {
                        //refresh data grid
                        InitializeDataGridView(drpCatalog.SelectedValue);
                    }
                }
            }
            catch (Exception)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "warning", "<script language='javascript'>alert('Error: there exists related records in other tables.');</script>");                
            }
        }
        /// <summary>
        /// Edit or delete Row data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridViewCatalogRecords_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //Get current rowindex
                int CurrentRowIndex;
                string CommandArgument = e.CommandArgument.ToString();
                int.TryParse(CommandArgument, out CurrentRowIndex);
               
                //Edit keyField List and key field value list
                string dataKeyField = "";
                string dataKeyValue = "";

                //Get Data key values
                string[] dataKeyNames = gridViewCatalogRecords.DataKeyNames;
                foreach (string key in dataKeyNames)
                {
                    dataKeyField += key + ",";
                    dataKeyValue += gridViewCatalogRecords.DataKeys[CurrentRowIndex].Values[key].ToString() + ",";
                }
                dataKeyField = dataKeyField.TrimEnd(new char[] { ',' });
                dataKeyValue = dataKeyValue.TrimEnd(new char[] { ',' });

                //get Delete table Name
                string tableName = drpCatalog.SelectedValue;

                //Edit data

                Response.Redirect("DynamicPageRender.aspx?CatalogName=" + tableName + "&dataKeyField=" + dataKeyField + "&dataKeyValue=" + dataKeyValue);
            }
        }
        /// <summary>
        /// Add Catalog table record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (drpCatalog.SelectedIndex != 0 && drpCatalog.SelectedIndex != -1)
            {
                Response.Redirect("DynamicPageRender.aspx?CatalogName=" + drpCatalog.SelectedValue);
            }
        }
        // Added by tina 2012-02-27
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                RefreshDataGridView();
            }
        }
    }
}
