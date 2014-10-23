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

namespace Waiter
{
    public partial class frmNewOrder : Form
    {

        DataGridTableStyle dataGridTableStyle1 = new DataGridTableStyle();
        DataGridNotEditableColumn dataGridColumnNotEditable = new DataGridNotEditableColumn();
        DataGridTextBoxColumn dataGridColumnProductName = new DataGridTextBoxColumn();
        DataGridTextBoxColumn dataGridColumnAmount = new DataGridTextBoxColumn();

        DataTable OriginalOrderDetail = new DataTable();

        string firstTableNo = "";
        string OrderType = "New";

        public frmNewOrder()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            OrderType = "New";
            Cursor.Current = Cursors.Default;
        }

        public frmNewOrder(Int64 orderNo, string tableNo)
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            OrderType = "Edit";
            txtOrderNo.Text = orderNo.ToString();
            txtTableNo.Text = tableNo;
            firstTableNo = tableNo;
            Cursor.Current = Cursors.Default;
        }

        #region Graphical Events
        private void frmNewOrder_Paint(object sender, PaintEventArgs e)
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

        private void btnSelectTable_MouseDown(object sender, MouseEventArgs e)
        {
            btnSelectTable.Image = global::Waiter.Properties.Resources.SelectTableDown;
        }

        private void btnSelectTable_MouseUp(object sender, MouseEventArgs e)
        {
            btnSelectTable.Image = global::Waiter.Properties.Resources.SelectTableUp;
        }

        private void btnDeleteRow_MouseDown(object sender, MouseEventArgs e)
        {
            btnDeleteRow.Image = global::Waiter.Properties.Resources.DeleteDown;
        }

        private void btnDeleteRow_MouseUp(object sender, MouseEventArgs e)
        {
            btnDeleteRow.Image = global::Waiter.Properties.Resources.DeleteUp;
        }

        private void btnAdd_MouseUp(object sender, MouseEventArgs e)
        {
            btnAdd.Image = global::Waiter.Properties.Resources.AddUp;
        }

        private void btnAdd_MouseDown(object sender, MouseEventArgs e)
        {
            btnAdd.Image = global::Waiter.Properties.Resources.AddDown;
        }

        private void btnSubtract_MouseDown(object sender, MouseEventArgs e)
        {
            btnSubtract.Image = global::Waiter.Properties.Resources.SubtractDown;
        }

        private void btnSubtract_MouseUp(object sender, MouseEventArgs e)
        {
            btnSubtract.Image = global::Waiter.Properties.Resources.SubtractUp;
        }

        private void btnSave_MouseDown(object sender, MouseEventArgs e)
        {
            btnSave.Image = global::Waiter.Properties.Resources.SaveDown;
        }

        private void btnSave_MouseUp(object sender, MouseEventArgs e)
        {
            btnSave.Image = global::Waiter.Properties.Resources.SaveUp;
        }

        private void btnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            btnCancel.Image = global::Waiter.Properties.Resources.CancelDown;
        }

        private void btnCancel_MouseUp(object sender, MouseEventArgs e)
        {
            btnCancel.Image = global::Waiter.Properties.Resources.CancelUp;
        }

        private void btnAddProduct_MouseDown(object sender, MouseEventArgs e)
        {
            btnAddProduct.Image = global::Waiter.Properties.Resources.AddProductDown;
        }

        private void btnAddProduct_MouseUp(object sender, MouseEventArgs e)
        {
            btnAddProduct.Image = global::Waiter.Properties.Resources.AddProductUp;
        }
        #endregion

        private void frmNewOrder_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Order.dtOrderDetail.Rows.Clear();
            if (OrderType == "Edit")
            {
                if (LoadOrderDetail(Convert.ToInt64(txtOrderNo.Text)))
                {
                    if (!LockOrder(Convert.ToInt64(txtOrderNo.Text), GlobalClass._UserID))
                    {
                        MessageBox.Show(string.Format(MessagesListClass.OrderIsLock, txtOrderNo.Text), "Order is locked!", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
                else
                {
                    MessageBox.Show(string.Format(MessagesListClass.OrderIsNotAvailable, txtOrderNo.Text), "Order Not Available!", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            dataGridOrderItems.DataSource = Order.dtOrderDetail.DefaultView;
            InitializeDataGridColumns();
            Cursor.Current = Cursors.Default;
        }

        private bool LoadOrderDetail(Int64 orderNo)
        {
            bool result = false;
            try
            {
                string spName = "PPC_Get_Order_Detail";
                SqlParameter[] spParams = new SqlParameter[1];
                spParams[0] = new SqlParameter("@OrderNo", orderNo);
                DataSet ds = DatabaseClass.ExecuteDataSet(spName, spParams);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                DataRow dr = Order.dtOrderDetail.NewRow();
                                dr["ProductID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductID"]);
                                dr["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                                dr["UnitName"] = ds.Tables[0].Rows[i]["UnitName"].ToString();
                                dr["Amount"] = Convert.ToInt32(ds.Tables[0].Rows[i]["Amount"]);
                                dr["NotEditable"] = Convert.ToBoolean(ds.Tables[0].Rows[i]["NotEditable"]);
                                Order.dtOrderDetail.Rows.Add(dr);
                                result = true;
                            }
                            OriginalOrderDetail = Order.dtOrderDetail.Copy();
                        }
                    }
                }
            }
            catch (SqlException)
            { }
            return result;
        }

        private bool LockOrder(Int64 orderNo, int userID)
        {
            try
            {
                SqlParameter[] spParams = new SqlParameter[2];
                spParams[0] = new SqlParameter("@OrderNo", orderNo);
                spParams[1] = new SqlParameter("@LockKeeperUserID", userID);
                string spName = "Lock_Order";
                int result = DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private bool UnlockOrder(Int64 orderNo, int userID)
        {
            try
            {
                SqlParameter[] spParams = new SqlParameter[2];
                spParams[0] = new SqlParameter("@OrderNo", orderNo);
                spParams[1] = new SqlParameter("@LockKeeperUserID", userID);
                string spName = "UnLock_Order";
                int result = DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmProducts objfrmProducts = new frmProducts();
            objfrmProducts.ShowDialog();
            objfrmProducts.Dispose();
            Cursor.Current = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void InitializeDataGridColumns()
        {
            dataGridColumnNotEditable.DataGridSource = Order.dtOrderDetail;

            dataGridOrderItems.TableStyles.Add(dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            dataGridTableStyle1.GridColumnStyles.Add(dataGridColumnNotEditable);
            dataGridTableStyle1.GridColumnStyles.Add(dataGridColumnProductName);
            dataGridTableStyle1.GridColumnStyles.Add(dataGridColumnAmount);
            dataGridTableStyle1.MappingName = "OrderItems";
            // 
            // dataGridColumnState
            // 
            dataGridColumnNotEditable.HeaderText = "";
            dataGridColumnNotEditable.MappingName = "NotEditable";
            dataGridColumnNotEditable.Width = 20;
            // 
            // dataGridColumnProductName
            // 
            dataGridColumnProductName.HeaderText = "Product";
            dataGridColumnProductName.MappingName = "ProductName";
            dataGridColumnProductName.Width = 135;
            // 
            // dataGridColumnAmount
            // 
            dataGridColumnAmount.HeaderText = "Amount";
            dataGridColumnAmount.MappingName = "Amount";
            dataGridColumnAmount.Width = 60;
        }

        private void btnSelectTable_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmTables objfrmfrmTables = new frmTables();
            DialogResult dg = objfrmfrmTables.ShowDialog();
            if (dg == DialogResult.OK)
            {
                txtTableNo.Text = objfrmfrmTables.tableNo;
            }
            objfrmfrmTables.Dispose();
            Cursor.Current = Cursors.Default;
        }

        private void dataGridOrderItems_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridOrderItems.CurrentRowIndex > -1)
            {
                dataGridOrderItems.Select(dataGridOrderItems.CurrentRowIndex);
                btnAdd.Image = global::Waiter.Properties.Resources.AddUp;
                btnSubtract.Image = global::Waiter.Properties.Resources.SubtractUp;
                btnDeleteRow.Image = global::Waiter.Properties.Resources.DeleteUp;
                btnAdd.Enabled = true;
                btnSubtract.Enabled = true;
                btnDeleteRow.Enabled = true;
            }
            else
            {
                btnAdd.Image = global::Waiter.Properties.Resources.AddDisable;
                btnSubtract.Image = global::Waiter.Properties.Resources.SubtractDisable;
                btnDeleteRow.Image = global::Waiter.Properties.Resources.DeleteDisable;
                btnAdd.Enabled = false;
                btnSubtract.Enabled = false;
                btnDeleteRow.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Order.dtOrderDetail.Rows.Count > 0)
            {
                if (OrderType == "New")
                {
                    #region processes of the New
                    if (!string.IsNullOrEmpty(txtTableNo.Text) & txtTableNo.Text != "Not Selected")
                    {
                        if (DatabaseClass.CheckConnectionToServer())
                        {
                            try
                            {
                                string errorText = "";
                                SqlTransaction objSqlTransaction;
                                DatabaseClass.CloseSqlConnection();
                                DatabaseClass.objSqlConnection.Open();
                                objSqlTransaction = DatabaseClass.objSqlConnection.BeginTransaction();
                                try
                                {
                                    string spName = "Make_OrderHeader";
                                    SqlParameter[] spParams = new SqlParameter[2];
                                    spParams[0] = new SqlParameter("@TableNo", txtTableNo.Text.Trim());
                                    spParams[1] = new SqlParameter("@CreatorUserID", GlobalClass._UserID);
                                    object objOrderNo = DatabaseClass.ExecuteScaler(spName, CommandType.StoredProcedure, spParams, objSqlTransaction);
                                    if (objOrderNo != null)
                                    {
                                        Int64 orderNo = Convert.ToInt64(objOrderNo);
                                        if (orderNo > 0)
                                        {
                                            int itemsCount = Order.dtOrderDetail.Rows.Count;
                                            for (int i = 0; i < itemsCount; i++)
                                            {
                                                // This means that this item is a new item
                                                spName = "Make_OrderDetail";
                                                spParams = new SqlParameter[3];
                                                spParams[0] = new SqlParameter("@OrderNo", orderNo);
                                                spParams[1] = new SqlParameter("@ProductID", Convert.ToInt32(Order.dtOrderDetail.Rows[i]["ProductID"]));
                                                spParams[2] = new SqlParameter("@Amount", Convert.ToInt32(Order.dtOrderDetail.Rows[i]["Amount"]));
                                                int result = DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams, objSqlTransaction);
                                                if (result == -1)
                                                {
                                                    // This means that this item is not avialable anymore by the kitchen
                                                    errorText += string.Format(MessagesListClass.ProductIsNotAvailable, Order.dtOrderDetail.Rows[i]["ProductName"].ToString()) + "\n";
                                                    Order.dtOrderDetail.Rows[i].Delete();
                                                    i--;
                                                    itemsCount--;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(string.Format(MessagesListClass.ActionFailed, "Save"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                                        }
                                        if (errorText == "")
                                        {
                                            // All the items are correct and ready to save
                                            objSqlTransaction.Commit();
                                            DatabaseClass.CloseSqlConnection();
                                            MessageBox.Show(string.Format(MessagesListClass.OrderCreatedSuccessfully, orderNo.ToString()));
                                            this.DialogResult = DialogResult.OK;
                                        }
                                        else
                                        {
                                            objSqlTransaction.Rollback();
                                            DatabaseClass.CloseSqlConnection();
                                            MessageBox.Show(errorText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(string.Format(MessagesListClass.ActionFailed, "Save"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                                    }
                                }
                                catch
                                {
                                    objSqlTransaction.Rollback();
                                    DatabaseClass.CloseSqlConnection();
                                    MessageBox.Show(MessagesListClass.UnhandleException, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                                }
                            }
                            catch
                            {
                                MessageBox.Show(MessagesListClass.UnhandleException, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                            }
                        }
                        else
                        {
                            MessageBox.Show(MessagesListClass.noConnection, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        MessageBox.Show(MessagesListClass.NoTableIsSelected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    }
                    #endregion
                }
                else if (OrderType == "Edit")
                {
                    # region processes of the Edit
                    if (DatabaseClass.CheckConnectionToServer())
                    {
                        try
                        {
                            string errorText = "";
                            bool doWeHaveChanges = false;
                            SqlTransaction objSqlTransaction;
                            DatabaseClass.CloseSqlConnection();
                            DatabaseClass.objSqlConnection.Open();
                            objSqlTransaction = DatabaseClass.objSqlConnection.BeginTransaction();
                            try
                            {
                                bool orderStateChanged = false;
                                if (firstTableNo != txtTableNo.Text)
                                {
                                    string spName = "Update_Order_TableNo";
                                    SqlParameter[] spParams = new SqlParameter[3];
                                    spParams[0] = new SqlParameter("@OrderNo", Convert.ToInt64(txtOrderNo.Text));
                                    spParams[1] = new SqlParameter("@CurrentTableNo", firstTableNo);
                                    spParams[2] = new SqlParameter("@NewTableNo", txtTableNo.Text);
                                    DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams, objSqlTransaction);
                                    doWeHaveChanges = true;

                                    //here we should update the state of the order to Edited.
                                    spName = "Set_Order_Edited";
                                    spParams = new SqlParameter[1];
                                    spParams[0] = new SqlParameter("@OrderNo", Convert.ToInt64(txtOrderNo.Text));
                                    int result=DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams, objSqlTransaction);
                                    if (result > 0)
                                    {
                                        orderStateChanged = true;
                                    }
                                }
                                DataTable finalDataTable = CompareDataTables(OriginalOrderDetail, Order.dtOrderDetail);
                                if (finalDataTable.Rows.Count > 0)
                                {
                                    if (orderStateChanged==false)// This because the state of the order has already updated to edited in updating the table No part.
                                    {
                                        string spName = "Set_Order_Edited";
                                        SqlParameter[] spParams = new SqlParameter[1];
                                        spParams[0] = new SqlParameter("@OrderNo", Convert.ToInt64(txtOrderNo.Text));
                                        DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams, objSqlTransaction);
                                    }

                                    for (int i = 0; i < finalDataTable.Rows.Count; i++)
                                    {
                                        DataRow dr = finalDataTable.Rows[i];
                                        if ((Int16)dr["EditState"] == 1 || (Int16)dr["EditState"] == 3)
                                        {
                                            // This means that when new item has added or the amount of an existing item has increased.
                                            // Therefore we should check the availability of these items again to be come sure that
                                            // while we were editing the order, no items of the order has changed its availability state.
                                            // If so we roll back the saving state to make the waiter aware that somethings has changed
                                            // so he/she should ckeck again the order and then do the process of the saving again.
                                            if ((Int16)dr["EditState"] == 1)
                                            {
                                                // This means that this item is a new item
                                                string spName = "Make_OrderDetail";
                                                SqlParameter[] spParams = new SqlParameter[3];
                                                spParams[0] = new SqlParameter("@OrderNo", Convert.ToInt64(txtOrderNo.Text));
                                                spParams[1] = new SqlParameter("@ProductID", Convert.ToInt32(dr["ProductID"]));
                                                spParams[2] = new SqlParameter("@Amount", Convert.ToInt32(dr["Amount"]));
                                                int result = DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams, objSqlTransaction);
                                                if (result == -1)
                                                {
                                                    // This means that this item is not avialable anymore by the kitchen
                                                    errorText += string.Format(MessagesListClass.ProductIsNotAvailable, Order.dtOrderDetail.Rows.Find(Convert.ToInt32(dr["ProductID"]))["ProductName"].ToString()) + "\n";
                                                    Order.dtOrderDetail.Rows.Find(Convert.ToInt32(dr["ProductID"])).Delete();
                                                }
                                                else
                                                {
                                                    doWeHaveChanges = true;
                                                }
                                            }
                                            else if ((Int16)dr["EditState"] == 3)
                                            {
                                                // This means that this item has increased its amount value
                                                string spName = "Update_OrderDetail";
                                                SqlParameter[] spParams = new SqlParameter[5];
                                                spParams[0] = new SqlParameter("@OrderNo", Convert.ToInt64(txtOrderNo.Text));
                                                spParams[1] = new SqlParameter("@ProductID", Convert.ToInt32(dr["ProductID"]));
                                                spParams[2] = new SqlParameter("@Amount", Convert.ToInt32(dr["Amount"]));
                                                spParams[3] = new SqlParameter("@ChangeState", 3);
                                                spParams[4] = new SqlParameter("@EditAmount", Convert.ToInt32(dr["EditAmount"]));
                                                int result = DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams, objSqlTransaction);
                                                if (result == -1)
                                                {
                                                    // This means that this item is not avialable anymore by the kitchen so the waiter 
                                                    // can not add the amount of the order for this item.
                                                    errorText += string.Format(MessagesListClass.ProductIsNotAvailable, Order.dtOrderDetail.Rows.Find(Convert.ToInt32(dr["ProductID"]))["ProductName"].ToString()) + "\n";
                                                    Order.dtOrderDetail.Rows.Find(Convert.ToInt32(dr["ProductID"]))["Amount"] = OriginalOrderDetail.Rows.Find(Convert.ToInt32(dr["ProductID"]))["Amount"];
                                                }
                                                else
                                                {
                                                    doWeHaveChanges = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // This means that these items has deleted or its value has decreased.
                                            // Therfore we should update them in the orderDetail table
                                            if ((Int16)dr["EditState"] == 2)
                                            {
                                                // This means that the item has deleted from the order.
                                                string spName = "Update_OrderDetail";
                                                SqlParameter[] spParams = new SqlParameter[5];
                                                spParams[0] = new SqlParameter("@OrderNo", Convert.ToInt64(txtOrderNo.Text));
                                                spParams[1] = new SqlParameter("@ProductID", Convert.ToInt32(dr["ProductID"]));
                                                spParams[2] = new SqlParameter("@Amount", DBNull.Value);
                                                spParams[3] = new SqlParameter("@ChangeState", 2);
                                                spParams[4] = new SqlParameter("@EditAmount", DBNull.Value);
                                                int result = DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams, objSqlTransaction);
                                                if (result == 0)
                                                {
                                                    // an error has fired
                                                    errorText += MessagesListClass.UnhandleException + "\n";
                                                }
                                                else
                                                {
                                                    doWeHaveChanges = true;
                                                }
                                            }
                                            else if ((Int16)dr["EditState"] == 4)
                                            {
                                                // This means that the item has decreased its value.
                                                string spName = "Update_OrderDetail";
                                                SqlParameter[] spParams = new SqlParameter[5];
                                                spParams[0] = new SqlParameter("@OrderNo", Convert.ToInt64(txtOrderNo.Text));
                                                spParams[1] = new SqlParameter("@ProductID", Convert.ToInt32(dr["ProductID"]));
                                                spParams[2] = new SqlParameter("@Amount", Convert.ToInt32(dr["Amount"]));
                                                spParams[3] = new SqlParameter("@ChangeState", 4);
                                                spParams[4] = new SqlParameter("@EditAmount", Convert.ToInt32(dr["EditAmount"]));
                                                int result = DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams, objSqlTransaction);
                                                if (result == 0)
                                                {
                                                    // an error has fired
                                                    errorText += MessagesListClass.UnhandleException + "\n";
                                                }
                                                else
                                                {
                                                    doWeHaveChanges = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (errorText == "")
                                {
                                    // All the items are correct and ready to save
                                    objSqlTransaction.Commit();
                                    DatabaseClass.CloseSqlConnection();
                                    if (doWeHaveChanges)
                                    {
                                        MessageBox.Show(string.Format(MessagesListClass.OrderEditedSuccessfully, txtOrderNo.Text));
                                    }
                                    this.DialogResult = DialogResult.OK;
                                }
                                else
                                {
                                    objSqlTransaction.Rollback();
                                    DatabaseClass.CloseSqlConnection();
                                    MessageBox.Show(errorText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                                }
                            }
                            catch
                            {
                                objSqlTransaction.Rollback();
                                DatabaseClass.CloseSqlConnection();
                                MessageBox.Show(MessagesListClass.UnhandleException, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                            }
                        }
                        catch
                        {
                            MessageBox.Show(MessagesListClass.UnhandleException, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        MessageBox.Show(MessagesListClass.noConnection, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    }
                    #endregion
                }
            }
            else
            {
                MessageBox.Show(MessagesListClass.OrderIsEmpty, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            Cursor.Current = Cursors.Default;
        }

        private DataTable CompareDataTables(DataTable originalDataTable, DataTable editedDataTable)
        {
            DataTable finalDataTable = new DataTable();
            finalDataTable.Columns.Add("ProductID", typeof(int));
            finalDataTable.Columns.Add("Amount", typeof(int));
            finalDataTable.Columns.Add("EditState", typeof(Int16));
            finalDataTable.Columns.Add("EditAmount", typeof(int));

            for (int i = 0; i < originalDataTable.Rows.Count; i++)
            {
                DataRow dr = editedDataTable.Rows.Find(originalDataTable.Rows[i]["ProductID"]);
                if (dr == null)
                {
                    // Deleted Item
                    DataRow objDataRow = finalDataTable.NewRow();
                    objDataRow["ProductID"] = Convert.ToInt32(originalDataTable.Rows[i]["ProductID"]);
                    objDataRow["EditState"] = 2;
                    finalDataTable.Rows.Add(objDataRow);
                }
            }

            for (int i = 0; i < editedDataTable.Rows.Count; i++)
            {

                DataRow dr = originalDataTable.Rows.Find(editedDataTable.Rows[i]["ProductID"]);
                if (dr == null)
                {
                    // New Item
                    DataRow objDataRow = finalDataTable.NewRow();
                    objDataRow["ProductID"] = Convert.ToInt32(editedDataTable.Rows[i]["ProductID"]);
                    objDataRow["Amount"] = Convert.ToInt32(editedDataTable.Rows[i]["Amount"]);
                    objDataRow["EditState"] = 1;
                    finalDataTable.Rows.Add(objDataRow);
                }
                else
                {
                    int amountOriginal = Convert.ToInt32(dr["Amount"]);
                    int amountEdited = Convert.ToInt32(editedDataTable.Rows[i]["Amount"]);
                    if (amountOriginal != amountEdited)
                    {
                        if (amountOriginal > amountEdited)
                        {
                            // Subtract
                            DataRow objDataRow = finalDataTable.NewRow();
                            objDataRow["ProductID"] = Convert.ToInt32(editedDataTable.Rows[i]["ProductID"]);
                            objDataRow["Amount"] = amountEdited;
                            objDataRow["EditAmount"] = amountOriginal - amountEdited;
                            objDataRow["EditState"] = 4;
                            finalDataTable.Rows.Add(objDataRow);
                        }
                        else
                        {
                            // Add
                            DataRow objDataRow = finalDataTable.NewRow();
                            objDataRow["ProductID"] = Convert.ToInt32(editedDataTable.Rows[i]["ProductID"]);
                            objDataRow["Amount"] = amountEdited;
                            objDataRow["EditAmount"] = amountEdited - amountOriginal;
                            objDataRow["EditState"] = 3;
                            finalDataTable.Rows.Add(objDataRow);
                        }
                    }
                }
            }
            return finalDataTable;
        }

        private void frmNewOrder_Closing(object sender, CancelEventArgs e)
        {
            if (OrderType == "Edit")
            {
                UnlockOrder(Convert.ToInt64(txtOrderNo.Text), GlobalClass._UserID);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dataGridOrderItems.CurrentRowIndex > -1)
            {
                Order.dtOrderDetail.Rows[dataGridOrderItems.CurrentCell.RowNumber]["Amount"] = Convert.ToInt32(Order.dtOrderDetail.Rows[dataGridOrderItems.CurrentCell.RowNumber]["Amount"]) + 1;
            }
            else
            {
                MessageBox.Show(MessagesListClass.NoRowIsSelected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dataGridOrderItems.CurrentRowIndex > -1)
            {
                if (OrderType == "New")
                {
                    DataRow dr = Order.dtOrderDetail.Rows[dataGridOrderItems.CurrentRowIndex];
                    Order.dtOrderDetail.Rows[dataGridOrderItems.CurrentRowIndex].Delete();
                }
                else if (OrderType == "Edit")
                {
                    DataRow dr = Order.dtOrderDetail.Rows[dataGridOrderItems.CurrentRowIndex];
                    if (Convert.ToBoolean(OriginalOrderDetail.Rows.Find(dr["ProductID"])["NotEditable"]) == false)
                    {
                        Order.dtOrderDetail.Rows[dataGridOrderItems.CurrentRowIndex].Delete();
                    }
                    else
                    {
                        MessageBox.Show(string.Format(MessagesListClass.ActionFailed, "deleted"), "", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            else
            {
                MessageBox.Show(MessagesListClass.NoRowIsSelected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dataGridOrderItems.CurrentRowIndex > -1)
            {
                if (OrderType == "New")
                {
                    DataRow dr = Order.dtOrderDetail.Rows[dataGridOrderItems.CurrentRowIndex];
                    int currentAmount = Convert.ToInt32(dr["Amount"]);
                    if (currentAmount > 1)
                    {
                        dr["Amount"] = currentAmount - 1;
                    }
                    else
                    {
                        Order.dtOrderDetail.Rows[dataGridOrderItems.CurrentRowIndex].Delete();
                    }
                }
                else if (OrderType == "Edit")
                {
                    DataRow dr = Order.dtOrderDetail.Rows[dataGridOrderItems.CurrentRowIndex];
                    int currentAmount = Convert.ToInt32(dr["Amount"]);
                    bool unEditable = Convert.ToBoolean(OriginalOrderDetail.Rows.Find(dr["ProductID"])["NotEditable"]);
                    if (currentAmount > 1)
                    {
                        if (unEditable == false)
                        {
                            dr["Amount"] = currentAmount - 1;
                        }
                        else
                        {
                            if (currentAmount > Convert.ToInt32(OriginalOrderDetail.Rows.Find(dr["ProductID"])["Amount"]))
                            {
                                dr["Amount"] = currentAmount - 1;
                            }
                            else
                            {
                                MessageBox.Show(string.Format(MessagesListClass.ProductIsUneditable, "reduced"), "", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                            }
                        }
                    }
                    else
                    {
                        if (unEditable == false)
                        {
                            Order.dtOrderDetail.Rows[dataGridOrderItems.CurrentRowIndex].Delete();
                        }
                        else
                        {
                            MessageBox.Show(string.Format(MessagesListClass.ProductIsUneditable, "deleted"), "", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(MessagesListClass.NoRowIsSelected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            Cursor.Current = Cursors.Default;
        }
    }

    public static class Order
    {
        public static DataTable dtOrderDetail;
        static Order()
        {
            dtOrderDetail = new DataTable("OrderItems");
            DataColumn dc1 = new DataColumn("ProductID", typeof(int));
            DataColumn[] arrPK = new DataColumn[1];
            arrPK[0] = dc1;
            dtOrderDetail.Columns.Add(dc1);
            dtOrderDetail.Columns.Add("ProductName", typeof(string));
            dtOrderDetail.Columns.Add("UnitName", typeof(string));
            dtOrderDetail.Columns.Add("Amount", typeof(int));
            dtOrderDetail.Columns.Add("NotEditable", typeof(bool));
            dtOrderDetail.PrimaryKey = arrPK;
        }
    }

    public class DataGridNotEditableColumn : DataGridColumnStyle
    {
        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager
source, int rowNum, Brush
    backBrush, Brush foreBrush, bool alignToRight)
        {

            g.DrawRectangle(new Pen(Color.Black), bounds);
            g.FillRectangle(new SolidBrush(Color.White), bounds);
            if (Convert.ToBoolean(dataGridSource.Rows[rowNum]["NotEditable"]) == true)
            {
                g.DrawIcon(global::Waiter.Properties.Resources.UnEditable, bounds.X, bounds.Y);
            }
            else if (Convert.ToBoolean(dataGridSource.Rows[rowNum]["NotEditable"]) == false)
            {
                g.DrawIcon(global::Waiter.Properties.Resources.edit, bounds.X, bounds.Y);
            }
        }

        private DataTable dataGridSource = null;
        public DataTable DataGridSource
        {
            get { return dataGridSource; }
            set { dataGridSource = value; }
        }
    }
}