namespace DiagramaSP
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.trvFunciones = new System.Windows.Forms.TreeView();
            this.tbcPrincipal = new System.Windows.Forms.TabControl();
            this.tbpVista = new System.Windows.Forms.TabPage();
            this.Eliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txbCodigoFuncionHijo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txbCodigoFuncion = new System.Windows.Forms.TextBox();
            this.txbNoLinea = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxNombreFuncion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbIdNodoPadre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbIdNodo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.actualizarArbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbcPrincipal.SuspendLayout();
            this.tbpVista.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvFunciones
            // 
            this.trvFunciones.Location = new System.Drawing.Point(12, 40);
            this.trvFunciones.Name = "trvFunciones";
            this.trvFunciones.Size = new System.Drawing.Size(325, 659);
            this.trvFunciones.TabIndex = 0;
            // 
            // tbcPrincipal
            // 
            this.tbcPrincipal.Controls.Add(this.tbpVista);
            this.tbcPrincipal.Location = new System.Drawing.Point(343, 40);
            this.tbcPrincipal.Name = "tbcPrincipal";
            this.tbcPrincipal.SelectedIndex = 0;
            this.tbcPrincipal.Size = new System.Drawing.Size(1007, 659);
            this.tbcPrincipal.TabIndex = 1;
            // 
            // tbpVista
            // 
            this.tbpVista.Controls.Add(this.Eliminar);
            this.tbpVista.Controls.Add(this.btnGuardar);
            this.tbpVista.Controls.Add(this.btnLimpiar);
            this.tbpVista.Controls.Add(this.btnEditar);
            this.tbpVista.Controls.Add(this.label6);
            this.tbpVista.Controls.Add(this.txbCodigoFuncionHijo);
            this.tbpVista.Controls.Add(this.label5);
            this.tbpVista.Controls.Add(this.txbCodigoFuncion);
            this.tbpVista.Controls.Add(this.txbNoLinea);
            this.tbpVista.Controls.Add(this.label4);
            this.tbpVista.Controls.Add(this.tbxNombreFuncion);
            this.tbpVista.Controls.Add(this.label3);
            this.tbpVista.Controls.Add(this.txbIdNodoPadre);
            this.tbpVista.Controls.Add(this.label2);
            this.tbpVista.Controls.Add(this.txbIdNodo);
            this.tbpVista.Controls.Add(this.label1);
            this.tbpVista.Location = new System.Drawing.Point(4, 22);
            this.tbpVista.Name = "tbpVista";
            this.tbpVista.Padding = new System.Windows.Forms.Padding(3);
            this.tbpVista.Size = new System.Drawing.Size(999, 633);
            this.tbpVista.TabIndex = 0;
            this.tbpVista.Text = "Vista";
            this.tbpVista.UseVisualStyleBackColor = true;
            // 
            // Eliminar
            // 
            this.Eliminar.Location = new System.Drawing.Point(521, 64);
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Size = new System.Drawing.Size(454, 23);
            this.Eliminar.TabIndex = 17;
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(521, 23);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(454, 23);
            this.btnGuardar.TabIndex = 15;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(521, 155);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(454, 23);
            this.btnLimpiar.TabIndex = 14;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(521, 111);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(454, 23);
            this.btnEditar.TabIndex = 13;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(518, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Codigo de la Funcion hijo";
            // 
            // txbCodigoFuncionHijo
            // 
            this.txbCodigoFuncionHijo.Location = new System.Drawing.Point(521, 240);
            this.txbCodigoFuncionHijo.Multiline = true;
            this.txbCodigoFuncionHijo.Name = "txbCodigoFuncionHijo";
            this.txbCodigoFuncionHijo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbCodigoFuncionHijo.Size = new System.Drawing.Size(454, 376);
            this.txbCodigoFuncionHijo.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Codigo de la Funcion";
            // 
            // txbCodigoFuncion
            // 
            this.txbCodigoFuncion.Location = new System.Drawing.Point(10, 240);
            this.txbCodigoFuncion.Multiline = true;
            this.txbCodigoFuncion.Name = "txbCodigoFuncion";
            this.txbCodigoFuncion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbCodigoFuncion.Size = new System.Drawing.Size(471, 376);
            this.txbCodigoFuncion.TabIndex = 9;
            // 
            // txbNoLinea
            // 
            this.txbNoLinea.Location = new System.Drawing.Point(143, 158);
            this.txbNoLinea.Name = "txbNoLinea";
            this.txbNoLinea.Size = new System.Drawing.Size(284, 20);
            this.txbNoLinea.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "No Linea";
            // 
            // tbxNombreFuncion
            // 
            this.tbxNombreFuncion.Location = new System.Drawing.Point(143, 111);
            this.tbxNombreFuncion.Name = "tbxNombreFuncion";
            this.tbxNombreFuncion.Size = new System.Drawing.Size(284, 20);
            this.tbxNombreFuncion.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nombre de la funcion";
            // 
            // txbIdNodoPadre
            // 
            this.txbIdNodoPadre.Location = new System.Drawing.Point(143, 69);
            this.txbIdNodoPadre.Name = "txbIdNodoPadre";
            this.txbIdNodoPadre.Size = new System.Drawing.Size(284, 20);
            this.txbIdNodoPadre.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IdNodoPadre";
            // 
            // txbIdNodo
            // 
            this.txbIdNodo.Location = new System.Drawing.Point(143, 29);
            this.txbIdNodo.Name = "txbIdNodo";
            this.txbIdNodo.Size = new System.Drawing.Size(284, 20);
            this.txbIdNodo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IdNodo";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1362, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualizarArbolToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem1});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(61, 22);
            this.toolStripDropDownButton1.Text = "Archivo";
            // 
            // actualizarArbolToolStripMenuItem
            // 
            this.actualizarArbolToolStripMenuItem.Name = "actualizarArbolToolStripMenuItem";
            this.actualizarArbolToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.actualizarArbolToolStripMenuItem.Text = "Actualizar Arbol";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem1.Text = "Salir";
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 742);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tbcPrincipal);
            this.Controls.Add(this.trvFunciones);
            this.Name = "Inicio";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tbcPrincipal.ResumeLayout(false);
            this.tbpVista.ResumeLayout(false);
            this.tbpVista.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvFunciones;
        private System.Windows.Forms.TabControl tbcPrincipal;
        private System.Windows.Forms.TabPage tbpVista;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbIdNodo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxNombreFuncion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbIdNodoPadre;
        private System.Windows.Forms.TextBox txbNoLinea;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbCodigoFuncionHijo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbCodigoFuncion;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button Eliminar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem actualizarArbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

