using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Waiter.Classes;
using System.Data.SqlClient;

namespace Waiter
{
    public partial class frmActiveOrdersList : Form
    {

        DataGridTableStyle dataGridTableStyle1 = new DataGridTableStyle();
        DataGridImageColumn dataGridColumnState = new DataGridImageColumn();
        DataGridTextBoxColumn dataGridColumnOrderNo = new DataGridTextBoxColumn();
        DataGridTextBoxColumn dataGridColumnTableNo = new DataGridTextBoxColumn();

        DataView ordersDataView;

        public frmActiveOrdersList()
        {
            InitializeComponent();
            Cursor.Current = Cursors.Default;
        }

        #region Graphical Events
        private void frmActiveOrdersList_Paint(object sender, PaintEventArgs e)
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

        private void btnRefresh_MouseDown(object sender, MouseEventArgs e)
        {
            btnRefresh.Image = global::Waiter.Properties.Resources.RefreshDown;
        }

        private void btnRefresh_MouseUp(object sender, MouseEventArgs e)
        {
            btnRefresh.Image = global::Waiter.Properties.Resources.RefreshUp;
        }

        private void btnBack_MouseDown(object sender, MouseEventArgs e)
        {
            btnBack.Image = global::Waiter.Properties.Resources.BackDown;
        }

        private void btnBack_MouseUp(object sender, MouseEventArgs e)
        {
            btnBack.Image = global::Waiter.Properties.Resources.BackUp;
        }

        private void btnEdit_MouseDown(object sender, MouseEventArgs e)
        {
            btnEdit.Image = global::Waiter.Properties.Resources.EditDown;
        }

        private void btnEdit_MouseUp(object sender, MouseEventArgs e)
        {
            btnEdit.Image = global::Waiter.Properties.Resources.EditUp;
        }

        private void btnServed_MouseDown(object sender, MouseEventArgs e)
        {
            btnServed.Image = global::Waiter.Properties.Resources.ServedDown;
        }

        private void btnServed_MouseUp(object sender, MouseEventArgs e)
        {
            btnServed.Image = global::Waiter.Properties.Resources.ServedUp;
        }

        private void btnNew_MouseDown(object sender, MouseEventArgs e)
        {
            btnNew.Image = global::Waiter.Properties.Resources.NewDown;
        }

        private void btnNew_MouseUp(object sender, MouseEventArgs e)
        {
            btnNew.Image = global::Waiter.Properties.Resources.NewUp;
        }
        #endregion

        private void LoadOrder()
        {
            btnEdit.Visible = false;
            btnServed.Visible = false;
            lblTotalOrders.Text = "0";
            lblReadyOrders.Text = "0";
            string spName = "Get_Orders_List_For_Waiter";
            if (DatabaseClass.CheckConnectionToServer())
            {
                try
                {
                    DataSet ds = DatabaseClass.ExecuteDataSet(spName, CommandType.StoredProcedure);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            ds.Tables[0].TableName = "Orders";
                            DataColumn[] arrPK = new DataColumn[1];
                            arrPK[0] = ds.Tables[0].Columns["OrderNo"];
                            ds.Tables[0].PrimaryKey = arrPK;
                            lblTotalOrders.Text = ds.Tables[0].Rows.Count.ToString();
                            lblReadyOrders.Text = ds.Tables[1].Rows[0][0].ToString();
                            dataGridColumnState.DataGridSource = ds.Tables[0];
                            ordersDataView = new DataView(ds.Tables[0]);
                            dataGridOrders.DataSource = ordersDataView;
                        }
                    }
                }
                catch (SqlException)
                {

                }
            }
            else
            {
                MessageBox.Show(MessagesListClass.noConnection, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmActiveOrdersList_Load(object sender, EventArgs e)
        {
            LoadOrder();
            InitializeDataGridColumns();
        }

        private void InitializeDataGridColumns()
        {
            dataGridOrders.TableStyles.Add(dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            dataGridTableStyle1.GridColumnStyles.Add(dataGridColumnState);
            dataGridTableStyle1.GridColumnStyles.Add(dataGridColumnTableNo);
            dataGridTableStyle1.GridColumnStyles.Add(dataGridColumnOrderNo);
            dataGridTableStyle1.MappingName = "Orders";
            // 
            // dataGridColumnState
            // 
            dataGridColumnState.HeaderText = "";
            dataGridColumnState.MappingName = "State";
            dataGridColumnState.Width = 20;
            // 
            // dataGridColumnTableNo
            // 
            dataGridColumnTableNo.HeaderText = "Table No";
            dataGridColumnTableNo.MappingName = "TableNo";
            dataGridColumnTableNo.Width = 90;
            // 
            // dataGridColumnOrderNo
            // 
            dataGridColumnOrderNo.HeaderText = "Order No";
            dataGridColumnOrderNo.MappingName = "OrderNo";
            dataGridColumnOrderNo.Width = 90;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadOrder();
            Cursor.Current = Cursors.Default;
        }

        private void dataGridOrders_CurrentCellChanged(object sender, EventArgs e)
        {
            Int16 state = -1;
            try
            {
                state = Convert.ToInt16(ordersDataView.Table.Rows[dataGridOrders.CurrentCell.RowNumber]["State"]);
            }
            catch { }
            if (state == 0 || state == 1)
            {
                btnEdit.Visible = true;
                btnServed.Visible = false;
            }
            else if (state == 3)
            {
                btnEdit.Visible = false;
                btnServed.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
                btnServed.Visible = false;
            }
            if (dataGridOrders.CurrentRowIndex > -1)
            {
                dataGridOrders.Select(dataGridOrders.CurrentRowIndex);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmNewOrder objfrmNewOrder = new frmNewOrder();
            objfrmNewOrder.ShowDialog();
            objfrmNewOrder.Dispose();
            Cursor.Current = Cursors.WaitCursor;
            LoadOrder();
            Cursor.Current = Cursors.Default;
        }

        private void btnServed_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dataGridOrders.CurrentCell.RowNumber > -1)
            {
                string spName = "Set_Order_Served";
                SqlParameter[] spParams = new SqlParameter[1];
                spParams[0] =new SqlParameter("@OrderNo", Convert.ToInt64(ordersDataView.Table.Rows[dataGridOrders.CurrentCell.RowNumber]["OrderNo"]));
                if (DatabaseClass.CheckConnectionToServer())
                {
                    int result = 0;
                    if (DatabaseClass.OpenSqlConnection())
                    {
                        result = DatabaseClass.ExecuteNonQuary(spName, CommandType.StoredProcedure, spParams);
                    }
                    else
                    {
                        MessageBox.Show(MessagesListClass.noConnection, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    }
                    DatabaseClass.CloseSqlConnection();
                    if (result > 0)
                    {
                        LoadOrder();
                    }
                    else
                    {
                        MessageBox.Show(string.Format(MessagesListClass.ActionFailed,"Action"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show(MessagesListClass.noConnection, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show(MessagesListClass.NoRowIsSelected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            Cursor.Current = Cursors.Default;
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dataGridOrders.CurrentCell.RowNumber > -1)
            {
                Int64 orderNo =Convert.ToInt64(ordersDataView.Table.Rows[dataGridOrders.CurrentCell.RowNumber]["OrderNo"]);
                string tableNo = ordersDataView.Table.Rows[dataGridOrders.CurrentCell.RowNumber]["TableNo"].ToString();
                frmNewOrder objfrmNewOrder = new frmNewOrder(orderNo,tableNo);
                objfrmNewOrder.ShowDialog();
                objfrmNewOrder.Dispose();
                Cursor.Current = Cursors.WaitCursor;
                LoadOrder();
            }
            else
            {
                MessageBox.Show(MessagesListClass.NoRowIsSelected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            Cursor.Current = Cursors.Default;
        }

    }

    public class DataGridImageColumn : DataGridColumnStyle
    {
        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager
source, int rowNum, Brush
    backBrush, Brush foreBrush, bool alignToRight)
        {

            g.DrawRectangle(new Pen(Color.Black), bounds);
            g.FillRectangle(new SolidBrush(Color.White), bounds);
            if (Convert.ToInt32(dataGridSource.Rows[rowNum]["State"]) == 0)
            {
                g.DrawIcon(global::Waiter.Properties.Resources.New, bounds.X, bounds.Y);
            }
            else if (Convert.ToInt32(dataGridSource.Rows[rowNum]["State"]) == 1)
            {
                g.DrawIcon(global::Waiter.Properties.Resources.edit, bounds.X, bounds.Y);
            }
            else if (Convert.ToInt32(dataGridSource.Rows[rowNum]["State"]) == 3)
            {
                g.DrawIcon(global::Waiter.Properties.Resources.Ready, bounds.X, bounds.Y);
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