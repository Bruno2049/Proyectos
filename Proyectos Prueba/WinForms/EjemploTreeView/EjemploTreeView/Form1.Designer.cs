namespace EjemploTreeView
{
    partial class Form1
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
            this.trvMostrarJefes = new System.Windows.Forms.TreeView();
            this.btnCargarTreeView = new System.Windows.Forms.Button();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.btnCargarGrid = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // trvMostrarJefes
            // 
            this.trvMostrarJefes.AccessibleRole = System.Windows.Forms.AccessibleRole.Link;
            this.trvMostrarJefes.Location = new System.Drawing.Point(13, 13);
            this.trvMostrarJefes.Name = "trvMostrarJefes";
            this.trvMostrarJefes.Size = new System.Drawing.Size(155, 292);
            this.trvMostrarJefes.TabIndex = 0;
            this.trvMostrarJefes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvMostrarJefes_AfterSelect);
            // 
            // btnCargarTreeView
            // 
            this.btnCargarTreeView.Location = new System.Drawing.Point(13, 312);
            this.btnCargarTreeView.Name = "btnCargarTreeView";
            this.btnCargarTreeView.Size = new System.Drawing.Size(155, 30);
            this.btnCargarTreeView.TabIndex = 1;
            this.btnCargarTreeView.Text = "Cargar TreeView";
            this.btnCargarTreeView.UseVisualStyleBackColor = true;
            this.btnCargarTreeView.Click += new System.EventHandler(this.btnCargarTreeView_Click);
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpleados.Location = new System.Drawing.Point(186, 13);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.Size = new System.Drawing.Size(439, 148);
            this.dgvEmpleados.TabIndex = 2;
            // 
            // btnCargarGrid
            // 
            this.btnCargarGrid.Location = new System.Drawing.Point(515, 312);
            this.btnCargarGrid.Name = "btnCargarGrid";
            this.btnCargarGrid.Size = new System.Drawing.Size(109, 29);
            this.btnCargarGrid.TabIndex = 3;
            this.btnCargarGrid.Text = "Cargar GridView";
            this.btnCargarGrid.UseVisualStyleBackColor = true;
            this.btnCargarGrid.Click += new System.EventHandler(this.btnCargarGrid_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(186, 187);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(438, 20);
            this.textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(186, 213);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(439, 20);
            this.textBox2.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(186, 239);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(438, 20);
            this.textBox3.TabIndex = 6;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(186, 265);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(438, 20);
            this.textBox4.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 354);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCargarGrid);
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.btnCargarTreeView);
            this.Controls.Add(this.trvMostrarJefes);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvMostrarJefes;
        private System.Windows.Forms.Button btnCargarTreeView;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.Button btnCargarGrid;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
    }
}

