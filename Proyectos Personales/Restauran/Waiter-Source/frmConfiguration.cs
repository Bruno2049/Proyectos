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
    public partial class frmConfiguration : Form
    {

        public frmConfiguration()
        {
            InitializeComponent();
        }

        #region Graphical Events
        private void frmConfiguration_Paint(object sender, PaintEventArgs e)
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

        private void btnSave_MouseDown(object sender, MouseEventArgs e)
        {
            btnSave.Image = global::Waiter.Properties.Resources.SaveDown;
        }

        private void btnSave_MouseUp(object sender, MouseEventArgs e)
        {
            btnSave.Image = global::Waiter.Properties.Resources.SaveUp;
        }

        private void btnTest_MouseDown(object sender, MouseEventArgs e)
        {
            btnTest.Image = global::Waiter.Properties.Resources.TestDown;
        }

        private void btnTest_MouseUp(object sender, MouseEventArgs e)
        {
            btnTest.Image = global::Waiter.Properties.Resources.TestUp;
        }

        private void btnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            btnCancel.Image = global::Waiter.Properties.Resources.CancelDown;
        }

        private void btnCancel_MouseUp(object sender, MouseEventArgs e)
        {
            btnCancel.Image = global::Waiter.Properties.Resources.CancelUp;
        }
        #endregion

        private void frmConfiguration_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string commandText = "SELECT ServerName,DatabaseName,UserID,Password,ConnectionTimeout FROM Config";
            DataSet ds=DatabaseClass.ExecuteCeDataSet(commandText);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtServer.Text = ds.Tables[0].Rows[0]["ServerName"].ToString();
                    txtDatabase.Text = ds.Tables[0].Rows[0]["DatabaseName"].ToString();
                    txtUserID.Text = ds.Tables[0].Rows[0]["UserID"].ToString();
                    txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                    nudTimeout.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["ConnectionTimeout"]);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string errorText=DoValidation();
            if (string.IsNullOrEmpty(errorText))
            {
                string commandText = "UPDATE Config SET ServerName='" + txtServer.Text + "',DatabaseName='" + txtDatabase.Text + "',UserID='" + txtUserID.Text + "',Password='" + txtPassword.Text + "',ConnectionTimeout=" + Convert.ToInt32(nudTimeout.Value) + "";
                int result=DatabaseClass.ExecuteCeNonQuary(commandText);
                if (result > 0)
                {
                    MessageBox.Show(string.Format(MessagesListClass.ActionSucceed,"Save"));
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(string.Format(MessagesListClass.ActionFailed, "Save"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show(errorText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            Cursor.Current = Cursors.Default;
        }

        private string DoValidation()
        {
            string errorText="";
            if (string.IsNullOrEmpty(txtServer.Text))
            {
                errorText = string.Format(MessagesListClass.FieldIsEmpty, "Server") + "\n";
            }
            if (string.IsNullOrEmpty(txtDatabase.Text))
            {
                errorText += string.Format(MessagesListClass.FieldIsEmpty, "Database") + "\n";
            }
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                errorText += string.Format(MessagesListClass.FieldIsEmpty, "User ID") + "\n";
            }
            return errorText;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string errorText = DoValidation();
            if (string.IsNullOrEmpty(errorText))
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=" + txtServer.Text + "; Initial Catalog=" + txtDatabase.Text + "; User ID=" + txtUserID.Text + "; Password=" + txtPassword.Text + "; Connection Timeout=" + nudTimeout.Value.ToString() + "";
                try
                {
                    con.Open();
                    con.Close();
                    MessageBox.Show(MessagesListClass.ConnectionTestSucceed);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(string.Format(MessagesListClass.ConnectionTestFailed, txtServer.Text), "Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show(errorText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnkeyboard_Click(object sender, EventArgs e)
        {
            if (inputPanel1.Enabled)
            {
                inputPanel1.Enabled = false;
            }
            else
            {
                inputPanel1.Enabled = true;
            }
        }
    }
}