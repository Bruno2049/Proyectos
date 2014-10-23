using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Waiter
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            Cursor.Current = Cursors.Default;
        }

        private void frmAbout_Paint(object sender, PaintEventArgs e)
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnBack_MouseDown(object sender, MouseEventArgs e)
        {
            btnBack.Image = global::Waiter.Properties.Resources.OKDown;
        }

        private void btnBack_MouseUp(object sender, MouseEventArgs e)
        {
            btnBack.Image = global::Waiter.Properties.Resources.OKUp;
        }
    }
}