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
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // trvMostrarJefes
            // 
            this.trvMostrarJefes.Location = new System.Drawing.Point(13, 13);
            this.trvMostrarJefes.Name = "trvMostrarJefes";
            this.trvMostrarJefes.Size = new System.Drawing.Size(155, 292);
            this.trvMostrarJefes.TabIndex = 0;
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
            this.dgvEmpleados.Size = new System.Drawing.Size(439, 292);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 354);
            this.Controls.Add(this.btnCargarGrid);
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.btnCargarTreeView);
            this.Controls.Add(this.trvMostrarJefes);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvMostrarJefes;
        private System.Windows.Forms.Button btnCargarTreeView;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.Button btnCargarGrid;
    }
}

