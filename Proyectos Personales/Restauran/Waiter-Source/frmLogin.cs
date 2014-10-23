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
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            Cursor.Current = Cursors.Default;
        }

        #region Graphical Events
        private void Form1_Paint(object sender, PaintEventArgs e)
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

        private void btnLogin_MouseDown(object sender, MouseEventArgs e)
        {
            btnLogin.Image = global::Waiter.Properties.Resources.LoginDown;
        }

        private void btnLogin_MouseUp(object sender, MouseEventArgs e)
        {
            btnLogin.Image = global::Waiter.Properties.Resources.LoginUp;
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void DoLogin()
        {
            Cursor.Current = Cursors.WaitCursor;
            string errorText = "";
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                errorText = MessagesListClass.userNameNotValid + "\n";
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorText += MessagesListClass.passwordNotValid;
            }
            if (errorText == "")
            {
                string userName = txtUserName.Text;
                string password = GlobalClass.GetMd5Hash(txtPassword.Text);
                if (userName == "Config" & password == "096E62FD294491F0D2E9C007012C9E94")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    frmConfiguration objfrmConfiguration = new frmConfiguration();
                    DialogResult dg = objfrmConfiguration.ShowDialog();
                    if (dg == DialogResult.OK)
                    {
                        DatabaseClass.RefreshConnectionString();
                    }
                    objfrmConfiguration.Dispose();
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    if (DatabaseClass.CheckConnectionToServer())
                    {
                        SqlParameter[] spParams = new SqlParameter[2];
                        spParams[0] = new SqlParameter("@UserName", userName);
                        spParams[1] = new SqlParameter("@Password", password);
                        string spName = "Check_User";
                        DataSet ds = DatabaseClass.ExecuteDataSet(spName, spParams);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    Int16 permissionID = Convert.ToInt16(ds.Tables[0].Rows[0]["PermissionID"]);
                                    if (permissionID == 4)
                                    {
                                        GlobalClass._UserID = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"]);
                                        GlobalClass._FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                                        GlobalClass._PermissionID = permissionID;
                                        GlobalClass._Password = ds.Tables[0].Rows[0]["Password"].ToString();
                                        frmActiveOrdersList objfrmActiveOrdersList = new frmActiveOrdersList();
                                        objfrmActiveOrdersList.ShowDialog();
                                        objfrmActiveOrdersList.Dispose();
                                        txtUserName.Text = "";
                                        txtPassword.Text = "";
                                        txtUserName.Focus();
                                        txtUserName.SelectAll();
                                    }
                                    else
                                    {
                                        MessageBox.Show(MessagesListClass.UserNotAllowed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                                        txtUserName.Text = "";
                                        txtPassword.Text = "";
                                        txtUserName.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(MessagesListClass.UserNotValid, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                                    txtPassword.Text = "";
                                    txtUserName.Focus();
                                    txtUserName.SelectAll();
                                }
                            }
                            else
                            {
                                MessageBox.Show(MessagesListClass.UnhandleException, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                            }
                        }
                        else
                        {
                            MessageBox.Show(MessagesListClass.UnhandleException, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        MessageBox.Show(MessagesListClass.noConnection, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    }
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
            Application.Exit();
        }

        private void btnkeyboard_Click(object sender, EventArgs e)
        {
            if (inputPanelMain.Enabled)
            {
                inputPanelMain.Enabled = false;
            }
            else
            {
                inputPanelMain.Enabled = true;
            }
        }

        private void danLogo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmAbout objfrmAbout = new frmAbout();
            objfrmAbout.ShowDialog();
            objfrmAbout.Dispose();
            Cursor.Current = Cursors.Default;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                DoLogin();
            }
        }

    }
}