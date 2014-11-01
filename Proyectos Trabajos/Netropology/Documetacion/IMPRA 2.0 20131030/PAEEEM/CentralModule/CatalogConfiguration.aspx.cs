using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM.CentralModule
{
    public partial class CatalogConfiguration : System.Web.UI.Page
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
            }
        }

        private void InitializeCatalogOptions()
        {
            List<string> catalogs = GlossaryDal.ClassInstance.GetTablesName();
            this.drpCatalog.DataSource = catalogs;
            this.DataBind();
            this.drpCatalog.Items.Insert(0, new ListItem());
        }

        protected void btnSetup_Click(object sender, EventArgs e)
        {
            int count = 0;
            List<GlossaryField> fieldRecords = new List<GlossaryField>();
            foreach (GridViewRow Row in this.gridViewFields.Rows)
            {
                GlossaryField field = GetFieldsRecord(Row);

                fieldRecords.Add(field);
            }

            foreach (GlossaryField field in fieldRecords)
            {
                if (ViewState["HasConfiguration"] != null && (bool)ViewState["HasConfiguration"])
                {
                    count += GlossaryDal.ClassInstance.UpdateConfiguration(field);
                }
                else
                {
                    count += GlossaryDal.ClassInstance.InsertConfiguration(field);
                }
            }

            if (count == fieldRecords.Count)
            { 
                //TODO:show message for successfully storing
            }
        }

        private GlossaryField GetFieldsRecord(GridViewRow Row)
        {
            GlossaryField field = new GlossaryField();
            field.OwnedTable = this.drpCatalog.Text;
            field.ColumnName = Row.Cells[0].Text;
            field.DataType = Row.Cells[1].Text;
            if (((CheckBox)Row.FindControl("checkPrimary")).Checked)
            {
                field.IsPrimary = true;
            }

            if (((CheckBox)Row.FindControl("checkForeign")).Checked)
            {
                field.IsForeign = true;
                field.ParentTable = ((DropDownList)Row.FindControl("drpParentTable")).Text;
                field.ParentColumn = ((DropDownList)Row.FindControl("drpParentColumn")).Text;
                field.DisplayColumn = ((DropDownList)Row.FindControl("drpDisplayColumn")).Text;
            }

            if (((CheckBox)Row.FindControl("checkDisplay")).Checked)
            {
                field.IsDisplay = true;
            }

            field.DisplayName = ((TextBox)Row.FindControl("txtDisplayName")).Text;

            if (((CheckBox)Row.FindControl("checkIdentity")).Checked)
            {
                field.IsIdentity = true;
            }

            if (((CheckBox)Row.FindControl("checkMandatory")).Checked)
            {
                field.IsMandatory = true;
            }

            if (((CheckBox)Row.FindControl("checkEmail")).Checked)
            {
                field.IsEmail = true;
            }

            if (((CheckBox)Row.FindControl("checkPhone")).Checked)
            {
                field.IsPhone = true;
            }

            if (((CheckBox)Row.FindControl("checkZipCode")).Checked)
            {
                field.IsZip = true;
            }
            return field;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../default.aspx");
        }

        protected void drpCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpCatalog.SelectedIndex != 0)
            {
                RefreshGridView();

                ControlButtons(true);
            }
        }

        private void ControlButtons(bool visible)
        {
            this.btnSetup.Visible = visible;
            this.btnCancel.Visible = visible;
        }

        private void RefreshGridView()
        {
            this.gridViewFields.DataBound += new EventHandler(gridViewFields_DataBound);
            string table = this.drpCatalog.Text;

            List<GlossaryField> fields = GlossaryDal.ClassInstance.GetFieldsWithTableName(table);
            this.gridViewFields.DataSource = fields;
            this.gridViewFields.DataBind();            
        }

        void gridViewFields_DataBound(object sender, EventArgs e)
        {
            //List<string> parentTables = GlossaryDal.ClassInstance.GetTablesName();
            List<GlossaryField> fields = GlossaryDal.ClassInstance.GetConfigurations(this.drpCatalog.Text);
            bool hasConfiguration = false;
            if (fields.Count > 0)
            {
                hasConfiguration = true;
                ViewState["HasConfiguration"] = hasConfiguration;
            }

            foreach (GridViewRow Row in this.gridViewFields.Rows)
            {
                //InitializeEmbededParentTableCombo(parentTables, Row);

                SetupConfiguredValueInEachRow(hasConfiguration, fields, Row);
            }
        }        

        //protected void checkboxDisplay_CheckedChanged(object sender, EventArgs e)
        //{
        //    GridViewRow gridViewRow = (GridViewRow)((CheckBox)sender).NamingContainer;
        //    TextBox textboxDisplayName = gridViewRow.FindControl("txtDisplayName") as TextBox;

        //    if (((CheckBox)sender).Checked)
        //    {
        //        textboxDisplayName.Enabled = true;
        //    }
        //    else
        //    {
        //        textboxDisplayName.Text = string.Empty;
        //        textboxDisplayName.Enabled = false;
        //    }
        //}

        protected void checkboxPrimary_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)((CheckBox)sender).NamingContainer;
            CheckBox checkboxIdentity = gridViewRow.FindControl("checkIdentity") as CheckBox;

            if (((CheckBox)sender).Checked)
            {
                checkboxIdentity.Enabled = true;
            }
            else
            {
                checkboxIdentity.Checked = false;
                checkboxIdentity.Enabled = false;
            }
        }

        private void SetupConfiguredValueInEachRow(bool hasConfiguration, List<GlossaryField> fields, GridViewRow row)
        {
            if (hasConfiguration)
            {
                List<string> parentTables = GlossaryDal.ClassInstance.GetTablesName();
                InitializeEmbededParentTableCombo(parentTables, row);

                foreach (GlossaryField field in fields)
                {
                    if (IsSameField(row, field))
                    {
                        row.Cells[1].Text = field.DataType;
                        ((CheckBox)row.FindControl("checkPrimary")).Checked = field.IsPrimary;
                        ((CheckBox)row.FindControl("checkForeign")).Checked = field.IsForeign;
                        if (field.IsForeign)
                        {
                            ((DropDownList)row.FindControl("drpParentTable")).Enabled = true;
                            ((DropDownList)row.FindControl("drpParentColumn")).Enabled = true;
                            ((DropDownList)row.FindControl("drpDisplayColumn")).Enabled = true;

                            ((DropDownList)row.FindControl("drpParentTable")).Text = field.ParentTable;

                            //get columns of the parent table selected
                            List<GlossaryField> fieldCollections = GlossaryDal.ClassInstance.GetFieldsWithTableName(field.ParentTable);
                            InitializeEmbeddedParentColumnCombo(((DropDownList)row.FindControl("drpParentTable")), fieldCollections);
                            ((DropDownList)row.FindControl("drpParentColumn")).Text = field.ParentColumn;

                            InitializeEmbeddedDisplayColumnCombo(((DropDownList)row.FindControl("drpParentTable")), fieldCollections);
                            ((DropDownList)row.FindControl("drpDisplayColumn")).Text = field.DisplayColumn;
                        }                        
                        ((CheckBox)row.FindControl("checkDisplay")).Checked = field.IsDisplay;
                        ((TextBox)row.FindControl("txtDisplayName")).Text = field.DisplayName;
                        ((CheckBox)row.FindControl("checkIdentity")).Checked = field.IsIdentity;
                        ((CheckBox)row.FindControl("checkMandatory")).Checked = field.IsMandatory;
                        ((CheckBox)row.FindControl("checkPhone")).Checked = field.IsPhone;
                        ((CheckBox)row.FindControl("checkZipCode")).Checked = field.IsZip;

                        if (field.IsIdentity)
                        {
                            ((CheckBox)row.FindControl("checkIdentity")).Enabled = true;
                        }

                        if (field.IsDisplay)
                        {
                            ((TextBox)row.FindControl("txtDisplayName")).Enabled = true;
                        }
                    }
                }
            }
        }

        private static bool IsSameField(GridViewRow row, GlossaryField field)
        {
            return field.ColumnName == row.Cells[0].Text;         
        }

        protected void ForeignKey_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox foreignKey = (CheckBox)sender;
            GridViewRow row = (GridViewRow)foreignKey.NamingContainer;
            List<string> parentTables = GlossaryDal.ClassInstance.GetTablesName();

            if (foreignKey != null && foreignKey.Checked)
            {
                ((DropDownList)row.FindControl("drpParentTable")).Enabled = true;
                InitializeEmbededParentTableCombo(parentTables, row);
            }
            else
            {
                ((DropDownList)row.FindControl("drpParentTable")).SelectedIndex = 0;
                ((DropDownList)row.FindControl("drpParentTable")).Enabled = false;
                if (((DropDownList)row.FindControl("drpDisplayColumn")).SelectedIndex != -1)
                    ((DropDownList)row.FindControl("drpDisplayColumn")).SelectedIndex = 0;
                ((DropDownList)row.FindControl("drpDisplayColumn")).Enabled = false;
                if (((DropDownList)row.FindControl("drpParentColumn")).SelectedIndex != -1)
                    ((DropDownList)row.FindControl("drpParentColumn")).SelectedIndex = 0;
                ((DropDownList)row.FindControl("drpParentColumn")).Enabled = false;
            }
        }

        private void InitializeEmbededParentTableCombo(List<string> parentTables, GridViewRow Row)
        {
            if (Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList parentTable = (DropDownList)Row.FindControl("drpParentTable");                
                parentTable.DataSource = parentTables;             
                parentTable.DataBind();
                parentTable.Items.Insert(0, new ListItem());                

                parentTable.SelectedIndexChanged += new EventHandler(parentTable_SelectedIndexChanged);
            }
        }

        protected void parentTable_DataBound(object sender, EventArgs e)
        {
            DropDownList parentTable = (DropDownList)sender;
            for (int i = 0; i < parentTable.Items.Count; i++)
            {
                parentTable.Items[i].Attributes.Add("Title", parentTable.Items[i].Text);
            }
        }

        protected void parentTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList parentTable = (DropDownList)sender;
            if (parentTable != null && parentTable.SelectedIndex != 0)
            {
                string tableName = parentTable.Text;
                List<GlossaryField> fields = GlossaryDal.ClassInstance.GetFieldsWithTableName(tableName);

                InitializeEmbeddedParentColumnCombo(parentTable, fields);

                InitializeEmbeddedDisplayColumnCombo(parentTable, fields);
            }
            else
            {
                ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpDisplayColumn")).Enabled = false;
                ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpParentColumn")).Enabled = false;
            }
        }

        private static void InitializeEmbeddedDisplayColumnCombo(DropDownList parentTable, List<GlossaryField> fields)
        {
            ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpDisplayColumn")).DataSource = fields;
            ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpDisplayColumn")).DataTextField = "ColumnName";
            ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpDisplayColumn")).DataBind();
            ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpDisplayColumn")).Items.Insert(0, new ListItem());
            ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpDisplayColumn")).Enabled = true;            
        }

        protected void DisplayColumn_DataBound(object sender, EventArgs e)
        {
            DropDownList displayColumn = (DropDownList)sender;
            for (int i = 0; i < displayColumn.Items.Count; i++)
            {
                displayColumn.Items[i].Attributes.Add("Title", displayColumn.Items[i].Text);
            }
        }

        private static void InitializeEmbeddedParentColumnCombo(DropDownList parentTable, List<GlossaryField> fields)
        {
            ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpParentColumn")).DataSource = fields;
            ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpParentColumn")).DataTextField = "ColumnName";
            ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpParentColumn")).DataBind();
            ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpParentColumn")).Items.Insert(0, new ListItem());
            ((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpParentColumn")).Enabled = true;
            //((DropDownList)((GridViewRow)parentTable.NamingContainer).FindControl("drpParentColumn")).SelectedIndexChanged += new EventHandler(ParentColumn_SelectedIndexChanged);            
        }

        protected void ParentColumn_DataBound(object sender, EventArgs e)
        {
            DropDownList parentColumn = (DropDownList)sender;
            for (int i = 0; i < parentColumn.Items.Count; i++)
            {
                parentColumn.Items[i].Attributes.Add("Title", parentColumn.Items[i].Text);
            }
        }

    }
}
