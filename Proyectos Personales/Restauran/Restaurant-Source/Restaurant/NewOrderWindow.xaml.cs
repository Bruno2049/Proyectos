using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Globalization;
using System.Data.Common;
using Restaurant.Classes;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        #region These are global object which will be use to communicate with the database whithin this class
        SqlDatabase objSqlDatabase = new SqlDatabase(GlobalClass._ConStr);
        DatabaseClass objDatabaseClass = new DatabaseClass(GlobalClass._ConStr);
        #endregion

        bool doFilter = false;
        DataView productDataView = null;
        DataTable productDataTable = null;
        private string orderType = "New";
        private string firstTableNo = "";
        DataTable OriginalOrderDetail = new DataTable();

        public NewOrderWindow()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
        }

        public NewOrderWindow(Int64 orderNo, string tableNo)
        {
            this.InitializeComponent();
            // Insert code required on object creation below this point.
            txbWindowTitle.Text = "Edit Order";
            txbOrderNo.Text = orderNo.ToString();
            firstTableNo = tableNo;
            txbTableNo.Text = tableNo;
            orderType = "Edit";
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //Here we should call the method which load initial data to the controls
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (objDatabaseClass.CheckConnection())
            {
                if (orderType == "New")
                {
                    Int64? lastOrderNo = GetLastOrderNo();
                    if (lastOrderNo != null)
                    {
                        txbOrderNo.Text = (lastOrderNo + 1).ToString();
                    }
                    LoadTables();
                    LoadGroups();
                    doFilter = true;
                    MakeOrderDataTable();
                    OrderGridView.ItemsSource = orderDataTable.DefaultView;
                    LoadProducts();
                    if (cmbGroups.Items.Count > 0)
                    {
                        cmbGroups.SelectedIndex = 0;
                    }
                }
                else
                {
                    ((Storyboard)this.Resources["StoryboardProductsHide"]).Begin(this);
                    LoadTables();
                    LoadGroups();
                    doFilter = true;
                    MakeOrderDataTable();
                    OrderGridView.ItemsSource = orderDataTable.DefaultView;
                    LoadProducts();
                    if (cmbGroups.Items.Count > 0)
                    {
                        cmbGroups.SelectedIndex = 0;
                    }
                    if (cmbTableNo.Items.Count > 0)
                    {
                        List<TableClass> list = (List<TableClass>)cmbTableNo.ItemsSource;
                        var query = from l in list
                                    where l.TableNo == firstTableNo
                                    select l;
                        cmbTableNo.SelectedItem = query.First();
                    }
                    if (LoadOrderDetail(Convert.ToInt64(txbOrderNo.Text)))
                    {
                        if (!LockOrder(Convert.ToInt64(txbOrderNo.Text), GlobalClass._UserID))
                        {
                            MessageBox.Show(string.Format(ErrorMessages.Default.OrderIsLock, txbOrderNo.Text), "Order Is Locked!", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.DialogResult = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format(ErrorMessages.Default.OrderIsNotAvailable, txbOrderNo.Text), "Order Is Not Available!", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.DialogResult = false;
                    }
                }
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
            }
            this.Cursor = Cursors.Arrow;
        }

        //This method populate the Group Combobox
        private void LoadGroups()
        {
            cmbGroups.ItemsSource = null;
            string spName = "Get_All_Groups";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                List<GroupClass> groupList = new List<GroupClass>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    groupList.Add(new GroupClass(Convert.ToInt32(ds.Tables[0].Rows[i]["GroupID"]), ds.Tables[0].Rows[i]["GroupName"].ToString()));
                }
                ds = null;
                cmbGroups.ItemsSource = groupList;
                cmbGroups.DisplayMemberPath = "GroupName";
                cmbGroups.SelectedValuePath = "GroupID";
            }
            catch (SqlException)
            { }
        }

        //This method populate the Product Grid with available products
        private void LoadProducts()
        {
            string spName = "Get_Available_Products";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                productDataTable = ds.Tables[0];
                productDataView = productDataTable.DefaultView;
                ProductsGridView.ItemsSource = productDataView;
                ProductsGridView.SelectedIndex = -1;
            }
            catch (SqlException)
            { }
        }

        //This method load Restaurant Tables into tables combobox
        private void LoadTables()
        {
            cmbTableNo.ItemsSource = null;
            string spName = "Get_All_Tables";
            try
            {
                DataSet ds = objSqlDatabase.ExecuteDataSet(CommandType.StoredProcedure, spName);
                List<TableClass> tablesList = new List<TableClass>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tablesList.Add(new TableClass(ds.Tables[0].Rows[i]["TableNo"].ToString(), Convert.ToInt16(ds.Tables[0].Rows[i]["Capacity"]), ds.Tables[0].Rows[i]["State"].ToString()));
                }
                ds = null;
                cmbTableNo.ItemsSource = tablesList;
            }
            catch (SqlException)
            { }
        }

        DataTable orderDataTable;
        //This method creates the order datatable and binds it to the order GridView
        private void MakeOrderDataTable()
        {
            orderDataTable = new DataTable();
            DataColumn dc = new DataColumn("ProductID", typeof(Int64));
            orderDataTable.Columns.Add(dc);
            orderDataTable.Columns.Add("ProductName", typeof(string));
            orderDataTable.Columns.Add("UnitName", typeof(string));
            orderDataTable.Columns.Add("Amount", typeof(int));
            orderDataTable.Columns.Add("NotEditable", typeof(bool));
            DataColumn[] arrPK = new DataColumn[1];
            arrPK[0] = dc;
            orderDataTable.PrimaryKey = arrPK;
        }

        // When an order comes in edit mode this  method will be called to load order detail
        private bool LoadOrderDetail(Int64 orderNo)
        {
            bool result = false;
            try
            {
                string spName = "PPC_Get_Order_Detail";
                object[] spParams = new object[1];
                spParams[0] = orderNo;
                DataSet ds = objSqlDatabase.ExecuteDataSet(spName, spParams);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                DataRow dr = orderDataTable.NewRow();
                                dr["ProductID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductID"]);
                                dr["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                                dr["UnitName"] = ds.Tables[0].Rows[i]["UnitName"].ToString();
                                dr["Amount"] = Convert.ToInt32(ds.Tables[0].Rows[i]["Amount"]);
                                dr["NotEditable"] = Convert.ToBoolean(ds.Tables[0].Rows[i]["NotEditable"]);
                                orderDataTable.Rows.Add(dr);
                                result = true;
                            }
                            OriginalOrderDetail = orderDataTable.Copy();
                        }
                    }
                }
            }
            catch (SqlException)
            { }
            return result;
        }

        // when the order comes in edit mode we should lock the order in the database until the edit has finished
        private bool LockOrder(Int64 orderNo, int userID)
        {
            try
            {
                object[] spParams = new object[2];
                spParams[0] = orderNo;
                spParams[1] = userID;
                string spName = "Lock_Order";
                int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
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

        //At the end of editing we should unlock the order in database to make it free to other users to use it
        private bool UnlockOrder(Int64 orderNo, int userID)
        {
            try
            {
                object[] spParams = new object[2];
                spParams[0] = orderNo;
                spParams[1] = userID;
                string spName = "UnLock_Order";
                int result = objSqlDatabase.ExecuteNonQuery(spName, spParams);
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

        //When the mouse enters the borderTableNo area the PopUpSelectTable will be shown
        private void borderTableNo_MouseEnter(object sender, MouseEventArgs e)
        {
            PopUpSelectTable.IsOpen = true;
        }

        //When the mouse leves the borderTableNo area the PopUpSelectTable will be hid
        private void PopUpSelectTable_MouseLeave(object sender, MouseEventArgs e)
        {
            PopUpSelectTable.IsOpen = false;
        }

        //If the selected table in restaurant tables combobox change this event will update the selected table
        private void cmbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doFilter)
            {
                int selectedGroupID = Convert.ToInt32(cmbGroups.SelectedValue);
                productDataView.RowFilter = "GroupID=" + selectedGroupID + "";
            }
        }

        bool dragStarted;
        private void ListViewItemDragBegin(object sender, MouseButtonEventArgs e)
        {
            dragStarted = true;
            base.OnPreviewMouseDown(e);
        }

        private void ListViewItemDragMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (dragStarted && sender is ListView)
                {
                    //create data object 
                    if (((ListView)sender).SelectedItems.Count > 0)
                    {
                        DataRowView theList = (DataRowView)((ListView)sender).SelectedItem;
                        ListBox li = sender as ListBox;
                        DataObject data = new DataObject();
                        data.SetData(theList);

                        //trap mouse events for the list, and perform drag/drop 
                        System.Windows.DragDrop.DoDragDrop(li, data, DragDropEffects.Copy);

                        dragStarted = false;
                        base.OnPreviewMouseMove(e);
                    }
                }
            }
            catch { }
        }

        private void OrderGridView_Drop(object sender, DragEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Data.GetData(typeof(DataRowView));
            AddToOrder(drv);
        }

        //Adds an item to the order 
        private void AddToOrder(DataRowView drv)
        {
            DataRow dr = orderDataTable.NewRow();
            dr["ProductID"] = drv["ProductID"];
            dr["ProductName"] = drv["ProductName"];
            dr["UnitName"] = drv["UnitName"];
            dr["Amount"] = 1;
            dr["NotEditable"] = false;
            try
            {
                orderDataTable.Rows.Add(dr);
            }
            catch (ConstraintException)
            {
                DataRow dr1 = orderDataTable.Rows.Find(drv["ProductID"]);
                dr1["Amount"] = Convert.ToInt32(dr1["Amount"]) + 1;
            }
        }

        //removes an item from the order 
        private void RemoveFromOrder(DataRowView drv)
        {
            DataRow dr = orderDataTable.Rows.Find(drv["ProductID"]);
            orderDataTable.Rows.Remove(dr);
        }

        private void btnAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGridView.SelectedItems.Count > 0)
            {
                DataRowView drv = (DataRowView)ProductsGridView.SelectedItem;
                AddToOrder(drv);
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoRowSelected, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (OrderGridView.SelectedItems.Count > 0)
            {
                if (orderType == "New")
                {
                    DataRowView drv = (DataRowView)OrderGridView.SelectedItem;
                    RemoveFromOrder(drv);
                }
                else if (orderType == "Edit")
                {
                    DataRowView drv = (DataRowView)OrderGridView.SelectedItem;
                    if (Convert.ToBoolean(OriginalOrderDetail.Rows.Find(drv["ProductID"])["NotEditable"]) == false)
                    {
                        RemoveFromOrder(drv);
                    }
                    else
                    {
                        MessageBox.Show(string.Format(ErrorMessages.Default.OrderItemIsNotEditable, "deleted"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show(ErrorMessages.Default.NoRowSelected, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Check the value is digit
        private void txtAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string text = e.Text;
            if (string.IsNullOrEmpty(text)) return;
            foreach (char ch in text)
            {
                if (!(char.IsDigit(ch)))
                {
                    e.Handled = true;
                }
            }
        }

        // Check the value is valid
        private void txtAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (orderType == "New")
            {
                if (string.IsNullOrEmpty(((TextBox)e.Source).Text))
                {
                    ((TextBox)e.Source).Text = "1";
                    ((TextBox)e.Source).SelectAll();
                }
                if (((TextBox)e.Source).Text == "0")
                {
                    ((TextBox)e.Source).Text = "1";
                    ((TextBox)e.Source).SelectAll();
                }
            }
            else if (orderType == "Edit")
            {
                TextBox txb = (TextBox)sender;
                DataRowView selectedRow = (DataRowView)txb.Tag;
                if (string.IsNullOrEmpty(((TextBox)e.Source).Text))
                {
                    ((TextBox)e.Source).Text = OriginalOrderDetail.Rows.Find(selectedRow["ProductID"])["Amount"].ToString();
                    ((TextBox)e.Source).SelectAll();
                }
                else if (((TextBox)e.Source).Text == "0")
                {
                    ((TextBox)e.Source).Text = OriginalOrderDetail.Rows.Find(selectedRow["ProductID"])["Amount"].ToString();
                    ((TextBox)e.Source).SelectAll();
                }
                else
                {
                    DataRow dr = OriginalOrderDetail.Rows.Find(selectedRow["ProductID"]);
                    if (dr != null)
                    {
                        bool notEditable = Convert.ToBoolean(dr["NotEditable"]);
                        if (notEditable == true)
                        {
                            int currentAmount = Convert.ToInt32(txb.Text);
                            if (currentAmount < Convert.ToInt32(OriginalOrderDetail.Rows.Find(selectedRow["ProductID"])["Amount"]))
                            {
                                selectedRow["Amount"] = Convert.ToInt32(OriginalOrderDetail.Rows.Find(selectedRow["ProductID"])["Amount"]);
                                MessageBox.Show(string.Format(ErrorMessages.Default.OrderItemIsNotEditable, "reduced"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
        }

        //Returns the last orderNo which was retrieved from the server
        private Int64? GetLastOrderNo()
        {
            Int64? orderNo = null;
            try
            {
                object result = objSqlDatabase.ExecuteScalar(CommandType.StoredProcedure, "Get_Last_OrderNo");
                if (result == DBNull.Value)
                {
                    orderNo = 0;
                }
                else
                {
                    orderNo = (Int64?)result;
                }
            }
            catch (SqlException)
            {
                orderNo = null;
            }
            return orderNo;
        }

        //In this event we do the process of saving order.
        private void btnSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            if (orderType == "New")
            {
                #region processes of New
                bool everythingIsOK = true;
                string errorText = "";
                Int64 temp;
                if (!Int64.TryParse(txbOrderNo.Text, out temp))
                {
                    everythingIsOK = false;
                    errorText = string.Format(ErrorMessages.Default.FieldEmpty, "OrderNo");
                }
                if (string.IsNullOrEmpty(txbTableNo.Text))
                {
                    everythingIsOK = false;
                    errorText += string.Format(ErrorMessages.Default.FieldEmpty, "TableNo");
                }
                if (!(OrderGridView.Items.Count > 0))
                {
                    everythingIsOK = false;
                    errorText += ErrorMessages.Default.NoOrderDetail;
                }
                if (everythingIsOK)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        DbConnection objTransactionCon = null;
                        DbTransaction objTransaction = null;
                        try
                        {
                            object[] spParams = new object[2];
                            spParams[0] = txbTableNo.Text.Trim();
                            spParams[1] = GlobalClass._UserID;
                            DbCommand objDbCommand = objSqlDatabase.GetStoredProcCommand("Make_OrderHeader", spParams);
                            objTransactionCon = objSqlDatabase.CreateConnection();
                            objTransactionCon.Open();
                            objTransaction = objTransactionCon.BeginTransaction();
                            Int64 orderNo = 0;
                            orderNo = Convert.ToInt64(objSqlDatabase.ExecuteScalar(objDbCommand, objTransaction));
                            if (orderNo > 0)
                            {
                                bool allProductsAreAvailable = true;
                                int itemsCount = orderDataTable.Rows.Count;
                                for (int i = 0; i < itemsCount; i++)
                                {
                                    Int32 productID = Convert.ToInt32(orderDataTable.Rows[i]["ProductID"]);
                                    Int32 amount = Convert.ToInt32(orderDataTable.Rows[i]["Amount"]);
                                    spParams = new object[3];
                                    spParams[0] = orderNo;
                                    spParams[1] = productID;
                                    spParams[2] = amount;
                                    objDbCommand = objSqlDatabase.GetStoredProcCommand("Make_OrderDetail", spParams);
                                    Int32 result = Convert.ToInt32(objSqlDatabase.ExecuteScalar(objDbCommand, objTransaction));
                                    if (result == -1)
                                    {
                                        allProductsAreAvailable = false;
                                        orderDataTable.Rows.Find(productID).Delete();
                                        i--;
                                        itemsCount--;
                                    }
                                }
                                if (allProductsAreAvailable)
                                {
                                    objTransaction.Commit();
                                    MessageBox.Show(string.Format(ErrorMessages.Default.OrderCreatedSuccessfully, orderNo));
                                    this.DialogResult = true;
                                }
                                else
                                {
                                    objTransaction.Rollback();
                                    doFilter = false;
                                    LoadGroups();
                                    doFilter = true;
                                    LoadProducts();
                                    if (cmbGroups.Items.Count > 0)
                                    {
                                        cmbGroups.SelectedIndex = 0;
                                    }
                                    MessageBox.Show(ErrorMessages.Default.SomeOrderItemsNotAvailable, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else
                            {
                                objTransaction.Rollback();
                                MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (SqlException)
                        {
                            objTransaction.Rollback();
                            MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(errorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                #endregion
            }
            else if (orderType == "Edit")
            {
                #region processes of Edit
                bool everythingIsOK = true;
                string errorText = "";
                Int64 temp;
                if (!Int64.TryParse(txbOrderNo.Text, out temp))
                {
                    everythingIsOK = false;
                    errorText = string.Format(ErrorMessages.Default.FieldEmpty, "OrderNo");
                }
                if (string.IsNullOrEmpty(txbTableNo.Text))
                {
                    everythingIsOK = false;
                    errorText += string.Format(ErrorMessages.Default.FieldEmpty, "TableNo");
                }
                if (!(OrderGridView.Items.Count > 0))
                {
                    everythingIsOK = false;
                    errorText += ErrorMessages.Default.NoOrderDetail;
                }
                if (everythingIsOK)
                {
                    if (objDatabaseClass.CheckConnection())
                    {
                        try
                        {
                            string errorTextTransaction = "";
                            bool doWeHaveChanges = false;
                            DbConnection objTransactionCon = null;
                            DbTransaction objTransaction = null;
                            objTransactionCon = objSqlDatabase.CreateConnection();
                            objTransactionCon.Open();
                            objTransaction = objTransactionCon.BeginTransaction();
                            try
                            {
                                bool orderStateChanged = false;
                                if (firstTableNo != txbTableNo.Text)
                                {
                                    string spName = "Update_Order_TableNo";
                                    object[] spParams = new object[3];
                                    spParams[0] = Convert.ToInt64(txbOrderNo.Text);
                                    spParams[1] = firstTableNo;
                                    spParams[2] = txbTableNo.Text;
                                    DbCommand objDbCommand = objSqlDatabase.GetStoredProcCommand(spName, spParams);
                                    objSqlDatabase.ExecuteNonQuery(objDbCommand, objTransaction);
                                    doWeHaveChanges = true;

                                    //here we should update the state of the order to Edited.
                                    spName = "Set_Order_Edited";
                                    spParams = new object[1];
                                    spParams[0] = Convert.ToInt64(txbOrderNo.Text);
                                    objDbCommand = objSqlDatabase.GetStoredProcCommand(spName, spParams);
                                    int result = objSqlDatabase.ExecuteNonQuery(objDbCommand, objTransaction);
                                    if (result > 0)
                                    {
                                        orderStateChanged = true;
                                    }
                                }
                                DataTable finalDataTable = CompareDataTables(OriginalOrderDetail, orderDataTable);
                                if (finalDataTable.Rows.Count > 0)
                                {
                                    if (orderStateChanged == false)// This because the state of the order has already updated to edited in updating the table No part.
                                    {
                                        string spName = "Set_Order_Edited";
                                        object[] spParams = new object[1];
                                        spParams[0] = Convert.ToInt64(txbOrderNo.Text);
                                        DbCommand objDbCommand = objSqlDatabase.GetStoredProcCommand(spName, spParams);
                                        objSqlDatabase.ExecuteNonQuery(objDbCommand, objTransaction);
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
                                                object[] spParams = new object[3];
                                                spParams[0] = Convert.ToInt64(txbOrderNo.Text);
                                                spParams[1] = Convert.ToInt32(dr["ProductID"]);
                                                spParams[2] = Convert.ToInt32(dr["Amount"]);
                                                DbCommand objDbCommand = objSqlDatabase.GetStoredProcCommand(spName, spParams);
                                                int result = objSqlDatabase.ExecuteNonQuery(objDbCommand, objTransaction);
                                                if (result == -1)
                                                {
                                                    // This means that this item is not avialable anymore by the kitchen
                                                    errorTextTransaction += string.Format(ErrorMessages.Default.ProductIsNotAvailable, orderDataTable.Rows.Find(Convert.ToInt32(dr["ProductID"]))["ProductName"].ToString()) + "\n";
                                                    orderDataTable.Rows.Find(Convert.ToInt32(dr["ProductID"])).Delete();
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
                                                object[] spParams = new object[5];
                                                spParams[0] = Convert.ToInt64(txbOrderNo.Text);
                                                spParams[1] = Convert.ToInt32(dr["ProductID"]);
                                                spParams[2] = Convert.ToInt32(dr["Amount"]);
                                                spParams[3] = 3;
                                                spParams[4] = Convert.ToInt32(dr["EditAmount"]);
                                                DbCommand objDbCommand = objSqlDatabase.GetStoredProcCommand(spName, spParams);
                                                int result = objSqlDatabase.ExecuteNonQuery(objDbCommand, objTransaction);
                                                if (result == -1)
                                                {
                                                    // This means that this item is not avialable anymore by the kitchen so the waiter 
                                                    // can not add the amount of the order for this item.
                                                    errorTextTransaction += string.Format(ErrorMessages.Default.ProductIsNotAvailable, orderDataTable.Rows.Find(Convert.ToInt32(dr["ProductID"]))["ProductName"].ToString()) + "\n";
                                                    orderDataTable.Rows.Find(Convert.ToInt32(dr["ProductID"]))["Amount"] = OriginalOrderDetail.Rows.Find(Convert.ToInt32(dr["ProductID"]))["Amount"];
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
                                                object[] spParams = new object[5];
                                                spParams[0] = Convert.ToInt64(txbOrderNo.Text);
                                                spParams[1] = Convert.ToInt32(dr["ProductID"]);
                                                spParams[2] = DBNull.Value;
                                                spParams[3] = 2;
                                                spParams[4] = DBNull.Value;
                                                DbCommand objDbCommand = objSqlDatabase.GetStoredProcCommand(spName, spParams);
                                                int result = objSqlDatabase.ExecuteNonQuery(objDbCommand, objTransaction);
                                                if (result == 0)
                                                {
                                                    // an error has fired
                                                    errorTextTransaction += ErrorMessages.Default.UnhandledException + "\n";
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
                                                object[] spParams = new object[5];
                                                spParams[0] = Convert.ToInt64(txbOrderNo.Text);
                                                spParams[1] = Convert.ToInt32(dr["ProductID"]);
                                                spParams[2] = Convert.ToInt32(dr["Amount"]);
                                                spParams[3] = 4;
                                                spParams[4] = Convert.ToInt32(dr["EditAmount"]);
                                                DbCommand objDbCommand = objSqlDatabase.GetStoredProcCommand(spName, spParams);
                                                int result = objSqlDatabase.ExecuteNonQuery(objDbCommand, objTransaction);
                                                if (result == 0)
                                                {
                                                    // an error has fired
                                                    errorTextTransaction += ErrorMessages.Default.UnhandledException + "\n";
                                                }
                                                else
                                                {
                                                    doWeHaveChanges = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (errorTextTransaction == "")
                                {
                                    // All the items are correct and ready to save
                                    objTransaction.Commit();
                                    objTransactionCon.Close();
                                    if (doWeHaveChanges)
                                    {
                                        MessageBox.Show(string.Format(ErrorMessages.Default.OrderEditedSuccessfully, txbOrderNo.Text));
                                    }
                                    this.DialogResult = true;
                                }
                                else
                                {
                                    objTransaction.Rollback();
                                    objTransactionCon.Close();
                                    MessageBox.Show(errorTextTransaction, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            catch
                            {
                                objTransaction.Rollback();
                                objTransactionCon.Close();
                                MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch
                        {
                            MessageBox.Show(ErrorMessages.Default.UnhandledException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(ErrorMessages.Default.NoConnection, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(errorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                #endregion
            }
        }

        // This method compares the original datatable and the edited ones an return the third data tables which
        // includes the items that should be updated in the database
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (orderType == "Edit")
            {
                UnlockOrder(Convert.ToInt64(txbOrderNo.Text), GlobalClass._UserID);
            }
        }

       
    }

}