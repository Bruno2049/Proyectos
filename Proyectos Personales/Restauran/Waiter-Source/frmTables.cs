using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Waiter.Classes;

namespace Waiter
{
    public partial class frmTables : Form
    {

        DataGridTableStyle dataGridTableStyle1 = new DataGridTableStyle();
        DataGridTableStateColumn dataGridColumnState = new DataGridTableStateColumn();
        DataGridTextBoxColumn dataGridColumnTableNo = new DataGridTextBoxColumn();
        DataGridTextBoxColumn dataGridColumnCapacity = new DataGridTextBoxColumn();

        DataView TableDataView;
        public string tableNo = "";

        public frmTables()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            this.tableNo = tableNo;
            Cursor.Current = Cursors.Default;
        }

        #region Graphical Events
        private void frmTables_Paint(object sender, PaintEventArgs e)
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

        private void btnOk_MouseDown(object sender, MouseEventArgs e)
        {
            btnOk.Image = global::Waiter.Properties.Resources.OKDown;
        }

        private void btnOk_MouseUp(object sender, MouseEventArgs e)
        {
            btnOk.Image = global::Waiter.Properties.Resources.OKUp;
        }
        #endregion

        private void LoadTableNo()
        {
            try
            {
                string commandText = "SELECT TableNo,Capacity,[State] FROM RestaurantTables ORDER BY [State],TableNo";
                if (DatabaseClass.CheckConnectionToServer())
                {
                    DataSet ds=DatabaseClass.ExecuteDataSet(commandText, CommandType.Text);
                    if (ds!=null)
                    {
                        ds.Tables[0].TableName = "RestaurantTables";
                        TableDataView=ds.Tables[0].DefaultView;
                        dataGridColumnState.DataGridSource = ds.Tables[0];
                        dataGridTables.DataSource = TableDataView;
                    }
                }
                else
                {
                    MessageBox.Show(MessagesListClass.noConnection, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
            }
            catch
            { }
        }

        private void frmTables_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadTableNo();
            InitializeDataGridColumns();
            Cursor.Current = Cursors.Default;
        }

        private void InitializeDataGridColumns()
        {
            dataGridTables.TableStyles.Add(dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            dataGridTableStyle1.GridColumnStyles.Add(dataGridColumnState);
            dataGridTableStyle1.GridColumnStyles.Add(dataGridColumnTableNo);
            dataGridTableStyle1.GridColumnStyles.Add(dataGridColumnCapacity);
            dataGridTableStyle1.MappingName = "RestaurantTables";
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
            // dataGridColumnCapacity
            // 
            dataGridColumnCapacity.HeaderText = "Capacity";
            dataGridColumnCapacity.MappingName = "Capacity";
            dataGridColumnCapacity.Width = 90;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dataGridTables.CurrentRowIndex > -1)
            {
                tableNo = TableDataView.Table.Rows[dataGridTables.CurrentRowIndex]["TableNo"].ToString();
                bool state = Convert.ToBoolean(TableDataView.Table.Rows[dataGridTables.CurrentRowIndex]["State"]);
                if (state == true)
                {
                    DialogResult dg=MessageBox.Show(string.Format(MessagesListClass.SelectedTableIsOccupied, tableNo), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dg == DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show(MessagesListClass.NoRowIsSelected, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridTables_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridTables.CurrentRowIndex > -1)
            {
                dataGridTables.Select(dataGridTables.CurrentRowIndex);
            }
        }
    }

    public class DataGridTableStateColumn : DataGridColumnStyle
    {
        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager
source, int rowNum, Brush
    backBrush, Brush foreBrush, bool alignToRight)
        {

            g.DrawRectangle(new Pen(Color.Black), bounds);
            g.FillRectangle(new SolidBrush(Color.White), bounds);
            if (Convert.ToInt32(dataGridSource.Rows[rowNum]["State"]) == 0)
            {
                g.DrawIcon(global::Waiter.Properties.Resources.Empty, bounds.X, bounds.Y);
            }
            else if (Convert.ToInt32(dataGridSource.Rows[rowNum]["State"]) == 1)
            {
                g.DrawIcon(global::Waiter.Properties.Resources.Occupied, bounds.X, bounds.Y);
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