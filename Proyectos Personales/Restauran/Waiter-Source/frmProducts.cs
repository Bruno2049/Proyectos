using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Waiter.Classes;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;

namespace Waiter
{
    public partial class frmProducts : Form
    {

        List<Product> productsList = new List<Product>();

        public frmProducts()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            Cursor.Current = Cursors.Default;
        }

        #region Graphical Events
        private void frmProducts_Paint(object sender, PaintEventArgs e)
        {
            Bitmap LeftTile = global::Waiter.Properties.Resources.LeftTile;
            Bitmap RightTile = global::Waiter.Properties.Resources.RightTile;
            Bitmap UpTile = global::Waiter.Properties.Resources.UpTile;
            Bitmap DownTile = global::Waiter.Properties.Resources.DownTile;

            Bitmap UpLeftCorner = global::Waiter.Properties.Resources.UpLeftCorner;
            Bitmap UpRightCorner = global::Waiter.Properties.Resources.UpRightCorner;
            Bitmap DownLeftCorner = global::Waiter.Properties.Resources.DownLeftCorner;
            Bitmap DownRightCorner = global::Waiter.Properties.Resources.DownRightCorner;

            TextureBrush brush = new TextureBrush(UpTile);
            Rectangle rect = new Rectangle(0, 0, this.Width, 8);
            e.Graphics.FillRectangle(brush, rect);

            brush = new TextureBrush(LeftTile);
            rect = new Rectangle(0, 0, 8, this.Height);
            e.Graphics.FillRectangle(brush, rect);

            brush = new TextureBrush(RightTile);
            rect = new Rectangle(this.Width - 8, 0, this.Width - 8, this.Height);
            e.Graphics.FillRectangle(brush, rect);

            brush = new TextureBrush(DownTile);
            rect = new Rectangle(0, this.Height - 6, this.Width, 8);
            e.Graphics.FillRectangle(brush, rect);

            brush = new TextureBrush(UpLeftCorner);
            rect = new Rectangle(0, 0, 8, 8);
            e.Graphics.FillRectangle(brush, rect);

            brush = new TextureBrush(UpRightCorner);
            rect = new Rectangle(this.Width - 8, 0, 8, 8);
            e.Graphics.FillRectangle(brush, rect);

            brush = new TextureBrush(DownLeftCorner);
            rect = new Rectangle(0, this.Height - 6, 8, 8);
            e.Graphics.FillRectangle(brush, rect);

            brush = new TextureBrush(DownRightCorner);
            rect = new Rectangle(this.Width - 8, this.Height - 6, 8, 8);
            e.Graphics.FillRectangle(brush, rect);
        }

        private void btnBack_MouseDown(object sender, MouseEventArgs e)
        {
            btnBack.Image = global::Waiter.Properties.Resources.BackDown;
        }

        private void btnBack_MouseUp(object sender, MouseEventArgs e)
        {
            btnBack.Image = global::Waiter.Properties.Resources.BackUp;
        }

        private void btnAddToOrder_MouseDown(object sender, MouseEventArgs e)
        {
            btnAddToOrder.Image = global::Waiter.Properties.Resources.AddToOrderDown;
        }

        private void btnAddToOrder_MouseUp(object sender, MouseEventArgs e)
        {
            btnAddToOrder.Image = global::Waiter.Properties.Resources.AddToOrderUp;
        }

        private void btnAddToOrderAndExit_MouseDown(object sender, MouseEventArgs e)
        {
            btnAddToOrderAndExit.Image = global::Waiter.Properties.Resources.AddToOrderAndExitDown;
        }

        private void btnAddToOrderAndExit_MouseUp(object sender, MouseEventArgs e)
        {
            btnAddToOrderAndExit.Image = global::Waiter.Properties.Resources.AddToOrderAndExitUp;
        }
        #endregion

        private void frmProducts_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (CheckLastChanges())
            {
                LoadGroups();
                productsList = GetProducts();
                if (cmbGroups.Items.Count > 0)
                {
                    doFilterProducts = true;
                    cmbGroups.SelectedIndex = -1;
                    cmbGroups.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show(MessagesListClass.UnhandleException, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                this.DialogResult = DialogResult.Cancel;
            }
            Cursor.Current = Cursors.Default;
        }

        private bool CheckLastChanges()
        {
            bool result = false;
            try
            {
                if (DatabaseClass.OpenSqlConnection())
                {
                    string commandText = "SELECT GroupsLastChangedDate,ProductsLastChangedDate FROM DataHasChanged";
                    SqlDataReader reader = DatabaseClass.ExecuteReader(commandText, CommandType.Text);
                    DateTime groupsLastChangedDate_Server = DateTime.MinValue;
                    DateTime productsLastChangedDate_Server = DateTime.MinValue;
                    if (reader != null)
                    {
                        if (reader.Read())
                        {
                            groupsLastChangedDate_Server = Convert.ToDateTime(reader["GroupsLastChangedDate"]);
                            productsLastChangedDate_Server = Convert.ToDateTime(reader["ProductsLastChangedDate"]);
                            reader.Close();
                        }
                    }
                    DatabaseClass.CloseSqlConnection();

                    DateTime groupsLastChangedDate_Local = DateTime.MinValue;
                    DateTime productsLastChangedDate_Local = DateTime.MinValue;
                    if (DatabaseClass.OpenSqlCeConnection())
                    {
                        commandText = "SELECT GroupsLastChangedDate,ProductsLastChangedDate FROM DataHasChanged";
                        SqlCeDataReader ceReader = DatabaseClass.ExecuteCeReader(commandText);
                        if (ceReader != null)
                        {
                            if (ceReader.Read())
                            {
                                groupsLastChangedDate_Local = Convert.ToDateTime(ceReader["GroupsLastChangedDate"]);
                                productsLastChangedDate_Local = Convert.ToDateTime(ceReader["ProductsLastChangedDate"]);
                                ceReader.Close();
                            }
                        }
                    }
                    DatabaseClass.CloseSqlCeConnection();
                    bool IsGroupsUpdated = false;
                    bool IsProducsUpdated = false;
                    if (groupsLastChangedDate_Local<groupsLastChangedDate_Server)
                    {
                        IsGroupsUpdated = UpdateGroupsData();
                    }
                    else
                    {
                        IsGroupsUpdated = true;
                    }
                    if (productsLastChangedDate_Local<productsLastChangedDate_Server)
                    {
                        IsProducsUpdated = UpdateProductsData();
                    }
                    else
                    {
                        IsProducsUpdated = true;
                    }

                    if (IsGroupsUpdated == true & IsProducsUpdated == true)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch (SqlException)
            {
                result = false;
            }
            return result;
        }

        private bool UpdateGroupsData()
        {
            bool result = false;
            if (DatabaseClass.DeleteTableRows("Groups"))
            {
                string spName = "Get_All_Groups";
                if (DatabaseClass.CheckConnectionToServer())
                {
                    DataSet ds = DatabaseClass.ExecuteDataSet(spName, CommandType.StoredProcedure);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string commandText = "INSERT INTO Groups(GroupID,GroupName) VALUES(" + Convert.ToInt32(ds.Tables[0].Rows[i]["GroupID"]) + ",'" + ds.Tables[0].Rows[i]["GroupName"].ToString() + "')";
                                DatabaseClass.ExecuteCeNonQuary(commandText);
                            }
                            DateTime serverDate = DatabaseClass.GetServerDateTime();
                            string cmdText = "UPDATE DataHasChanged SET GroupsLastChangedDate='" + serverDate + "'";
                            int rowsAffected = DatabaseClass.ExecuteCeNonQuary(cmdText);
                            if (rowsAffected > 0)
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                    }
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        private bool UpdateProductsData()
        {
            bool result = false;
            if (DatabaseClass.DeleteTableRows("Products"))
            {
                string spName = "Get_Available_Products";
                if (DatabaseClass.CheckConnectionToServer())
                {
                    DataSet ds = DatabaseClass.ExecuteDataSet(spName, CommandType.StoredProcedure);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string commandText = "INSERT INTO Products(ProductID,ProductName,UnitName,GroupID,Price) VALUES(" + Convert.ToInt32(ds.Tables[0].Rows[i]["ProductID"]) + ",'" + ds.Tables[0].Rows[i]["ProductName"].ToString() + "','" + ds.Tables[0].Rows[i]["UnitName"].ToString() + "'," + Convert.ToInt32(ds.Tables[0].Rows[i]["GroupID"]) + "," + Convert.ToDecimal(ds.Tables[0].Rows[i]["Price"]) + ")";
                                DatabaseClass.ExecuteCeNonQuary(commandText);
                            }
                            DateTime serverDate = DatabaseClass.GetServerDateTime();
                            string cmdText = "UPDATE DataHasChanged SET ProductsLastChangedDate='" + serverDate + "'";
                            int rowsAffected = DatabaseClass.ExecuteCeNonQuary(cmdText);
                            if (rowsAffected > 0)
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                    }
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        private void LoadGroups()
        {
            string commandText = "SELECT GroupID,GroupName FROM Groups ORDER BY GroupName";
            if (DatabaseClass.OpenSqlCeConnection())
            {
                SqlCeDataReader reader = DatabaseClass.ExecuteCeReader(commandText);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        List<Groups> groupsItems = new List<Groups>();
                        groupsItems.Add(new Groups(Convert.ToInt32(reader["GroupID"]), reader["GroupName"].ToString()));
                        while (reader.Read())
                        {
                            groupsItems.Add(new Groups(Convert.ToInt32(reader["GroupID"]), reader["GroupName"].ToString()));
                        }
                        cmbGroups.DataSource = groupsItems;
                        cmbGroups.DisplayMember = "GroupName";
                        cmbGroups.ValueMember = "GroupID";
                    }
                    reader.Close();
                }
            }
            DatabaseClass.CloseSqlCeConnection();
        }

        private List<Product> GetProducts()
        {
            List<Product> list = new List<Product>();
            string commandText = "SELECT ProductID,GroupID,ProductName,UnitName FROM Products ORDER BY ProductName";
            if (DatabaseClass.OpenSqlCeConnection())
            {
                SqlCeDataReader reader = DatabaseClass.ExecuteCeReader(commandText);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        list.Add(new Product(Convert.ToInt32(reader["ProductID"]), Convert.ToInt32(reader["GroupID"]), reader["ProductName"].ToString(), reader["UnitName"].ToString()));
                        while (reader.Read())
                        {
                            list.Add(new Product(Convert.ToInt32(reader["ProductID"]), Convert.ToInt32(reader["GroupID"]), reader["ProductName"].ToString(), reader["UnitName"].ToString()));
                        }
                    }
                    reader.Close();
                }
                DatabaseClass.CloseSqlCeConnection();
            }
            return list;
        }

        private void LoadProducts(int groupID,List<Product> products)
        {
            lstProducts.Items.Clear();
            if (groupID != -1)
            {
                var filterProducts = from p in products
                                     where p.GroupID == groupID
                                     select p;

                foreach (var product in filterProducts)
                {
                    ListViewItem li = new ListViewItem(product.ProductID.ToString());
                    li.SubItems.Add(product.ProductName);
                    li.SubItems.Add(product.UnitName);
                    lstProducts.Items.Add(li);
                }
                
            }
        }

        bool doFilterProducts = false;
        private void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (doFilterProducts)
            {
                if (cmbGroups.SelectedIndex > -1)
                {
                    int groupID = ((Groups)cmbGroups.SelectedItem).GroupID;
                    LoadProducts(groupID, productsList);
                }
                else
                {
                    LoadProducts(-1, productsList);
                }
            }
            Cursor.Current = Cursors.Default;
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAddToOrder_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (lstProducts.SelectedIndices.Count > 0)
            {
                DataRow dr=Order.dtOrderDetail.NewRow();
                dr["ProductID"] = Convert.ToInt32(lstProducts.Items[lstProducts.SelectedIndices[0]].SubItems[0].Text);
                dr["ProductName"] =lstProducts.Items[lstProducts.SelectedIndices[0]].SubItems[1].Text;
                dr["UnitName"] = lstProducts.Items[lstProducts.SelectedIndices[0]].SubItems[2].Text;
                dr["Amount"] = Convert.ToInt32(nudAmount.Value);
                dr["NotEditable"] = false;
                try
                {
                    Order.dtOrderDetail.Rows.Add(dr);
                }
                catch (ConstraintException)
                {
                    DataRow dr1=Order.dtOrderDetail.Rows.Find(Convert.ToInt32(lstProducts.Items[lstProducts.SelectedIndices[0]].SubItems[0].Text));
                    dr1["Amount"]=Convert.ToInt32(dr1["Amount"])+Convert.ToInt32(nudAmount.Value);
                }
                nudAmount.Value = 1;
                int selectedGroupIndex = cmbGroups.SelectedIndex;
                cmbGroups.SelectedIndex = -1;
                cmbGroups.SelectedIndex = selectedGroupIndex;
            }
            else
            {
                MessageBox.Show(MessagesListClass.NoRowIsSelected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnAddToOrderAndExit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (lstProducts.SelectedIndices.Count > 0)
            {
                DataRow dr = Order.dtOrderDetail.NewRow();
                dr["ProductID"] = Convert.ToInt32(lstProducts.Items[lstProducts.SelectedIndices[0]].SubItems[0].Text);
                dr["ProductName"] = lstProducts.Items[lstProducts.SelectedIndices[0]].SubItems[1].Text;
                dr["UnitName"] = lstProducts.Items[lstProducts.SelectedIndices[0]].SubItems[2].Text;
                dr["Amount"] = Convert.ToInt32(nudAmount.Value);
                dr["NotEditable"] = false;
                try
                {
                    Order.dtOrderDetail.Rows.Add(dr);
                }
                catch (ConstraintException)
                {
                    DataRow dr1 = Order.dtOrderDetail.Rows.Find(Convert.ToInt32(lstProducts.Items[lstProducts.SelectedIndices[0]].SubItems[0].Text));
                    dr1["Amount"] = Convert.ToInt32(dr1["Amount"]) + Convert.ToInt32(nudAmount.Value);
                }
                nudAmount.Value = 1;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(MessagesListClass.NoRowIsSelected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            Cursor.Current = Cursors.Default;
        }
    }

    public class Groups
    {
        private int groupID = -1;
        private string groupName = "";
        public Groups(int groupID, string groupName)
        {
            this.groupID = groupID;
            this.groupName = groupName;
        }

        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }
    }

    public class Product
    {
        private int productID = -1;
        private int groupID = -1;
        private string productName = "";
        private string unitName = "";
        public Product(int productID, int groupID, string productName, string unitName)
        {
            this.productID = productID;
            this.groupID = groupID;
            this.productName = productName;
            this.unitName = unitName;
        }

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; }
        }
    }
}