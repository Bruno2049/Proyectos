namespace Waiter
{
    partial class frmProducts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProducts));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbGroups = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.PictureBox();
            this.btnAddToOrder = new System.Windows.Forms.PictureBox();
            this.btnAddToOrderAndExit = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.lstProducts = new System.Windows.Forms.ListView();
            this.columnHeaderProductID = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderProductName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderUnit = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.Text = "Group:";
            // 
            // cmbGroups
            // 
            this.cmbGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroups.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.cmbGroups.Location = new System.Drawing.Point(51, 9);
            this.cmbGroups.Name = "cmbGroups";
            this.cmbGroups.Size = new System.Drawing.Size(181, 24);
            this.cmbGroups.TabIndex = 0;
            this.cmbGroups.SelectedIndexChanged += new System.EventHandler(this.cmbGroups_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(7, 288);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(72, 22);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnBack_MouseDown);
            this.btnBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnBack_MouseUp);
            // 
            // btnAddToOrder
            // 
            this.btnAddToOrder.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddToOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnAddToOrder.Image")));
            this.btnAddToOrder.Location = new System.Drawing.Point(84, 288);
            this.btnAddToOrder.Name = "btnAddToOrder";
            this.btnAddToOrder.Size = new System.Drawing.Size(72, 22);
            this.btnAddToOrder.Click += new System.EventHandler(this.btnAddToOrder_Click);
            this.btnAddToOrder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddToOrder_MouseDown);
            this.btnAddToOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAddToOrder_MouseUp);
            // 
            // btnAddToOrderAndExit
            // 
            this.btnAddToOrderAndExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddToOrderAndExit.Image = ((System.Drawing.Image)(resources.GetObject("btnAddToOrderAndExit.Image")));
            this.btnAddToOrderAndExit.Location = new System.Drawing.Point(161, 288);
            this.btnAddToOrderAndExit.Name = "btnAddToOrderAndExit";
            this.btnAddToOrderAndExit.Size = new System.Drawing.Size(72, 22);
            this.btnAddToOrderAndExit.Click += new System.EventHandler(this.btnAddToOrderAndExit_Click);
            this.btnAddToOrderAndExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddToOrderAndExit_MouseDown);
            this.btnAddToOrderAndExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAddToOrderAndExit_MouseUp);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.Text = "Amount:";
            // 
            // nudAmount
            // 
            this.nudAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nudAmount.Location = new System.Drawing.Point(66, 256);
            this.nudAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.Size = new System.Drawing.Size(124, 22);
            this.nudAmount.TabIndex = 2;
            this.nudAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lstProducts
            // 
            this.lstProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProducts.Columns.Add(this.columnHeaderProductID);
            this.lstProducts.Columns.Add(this.columnHeaderProductName);
            this.lstProducts.Columns.Add(this.columnHeaderUnit);
            this.lstProducts.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.lstProducts.FullRowSelect = true;
            this.lstProducts.Location = new System.Drawing.Point(8, 39);
            this.lstProducts.Name = "lstProducts";
            this.lstProducts.Size = new System.Drawing.Size(224, 208);
            this.lstProducts.TabIndex = 1;
            this.lstProducts.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderProductID
            // 
            this.columnHeaderProductID.Text = "";
            this.columnHeaderProductID.Width = 0;
            // 
            // columnHeaderProductName
            // 
            this.columnHeaderProductName.Text = "Product Name";
            this.columnHeaderProductName.Width = 140;
            // 
            // columnHeaderUnit
            // 
            this.columnHeaderUnit.Text = "Unit";
            this.columnHeaderUnit.Width = 60;
            // 
            // frmProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(87)))), ((int)(((byte)(144)))));
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.lstProducts);
            this.Controls.Add(this.nudAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddToOrderAndExit);
            this.Controls.Add(this.btnAddToOrder);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.cmbGroups);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmProducts";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProducts_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmProducts_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbGroups;
        private System.Windows.Forms.PictureBox btnBack;
        private System.Windows.Forms.PictureBox btnAddToOrder;
        private System.Windows.Forms.PictureBox btnAddToOrderAndExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.ListView lstProducts;
        private System.Windows.Forms.ColumnHeader columnHeaderProductID;
        private System.Windows.Forms.ColumnHeader columnHeaderProductName;
        private System.Windows.Forms.ColumnHeader columnHeaderUnit;
    }
}