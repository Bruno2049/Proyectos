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
            this.tbcPrincipal = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnMuestraTab = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.userControl11 = new EjemploTreeView.UserControl1();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.tbcPrincipal.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
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
            this.textBox1.Location = new System.Drawing.Point(325, 176);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(300, 20);
            this.textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(325, 202);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(301, 20);
            this.textBox2.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(325, 228);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(300, 20);
            this.textBox3.TabIndex = 6;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(325, 256);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(300, 20);
            this.textBox4.TabIndex = 7;
            // 
            // tbcPrincipal
            // 
            this.tbcPrincipal.Controls.Add(this.tabPage1);
            this.tbcPrincipal.Controls.Add(this.tabPage2);
            this.tbcPrincipal.Location = new System.Drawing.Point(12, 361);
            this.tbcPrincipal.Name = "tbcPrincipal";
            this.tbcPrincipal.SelectedIndex = 0;
            this.tbcPrincipal.Size = new System.Drawing.Size(613, 329);
            this.tbcPrincipal.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.userControl11);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(605, 303);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(605, 303);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnMuestraTab
            // 
            this.btnMuestraTab.Location = new System.Drawing.Point(388, 312);
            this.btnMuestraTab.Name = "btnMuestraTab";
            this.btnMuestraTab.Size = new System.Drawing.Size(121, 29);
            this.btnMuestraTab.TabIndex = 9;
            this.btnMuestraTab.Text = "Muestra TabPage";
            this.btnMuestraTab.UseVisualStyleBackColor = true;
            this.btnMuestraTab.Click += new System.EventHandler(this.btnMuestraTab_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(325, 282);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(300, 20);
            this.textBox5.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Nombre Empleado";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "IdEmpleado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "IdJefe";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Tag.NombreObj";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Path";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(647, 18);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(418, 324);
            this.splitContainer1.SplitterDistance = 139;
            this.splitContainer1.TabIndex = 17;
            // 
            // userControl11
            // 
            this.userControl11.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.userControl11.Location = new System.Drawing.Point(0, 0);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(400, 303);
            this.userControl11.TabIndex = 0;
            this.userControl11.Texto = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 698);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.btnMuestraTab);
            this.Controls.Add(this.tbcPrincipal);
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
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.tbcPrincipal.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tbcPrincipal;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private UserControl1 userControl11;
        private System.Windows.Forms.Button btnMuestraTab;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

