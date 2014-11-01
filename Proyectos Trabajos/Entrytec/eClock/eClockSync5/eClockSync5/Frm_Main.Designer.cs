namespace eClockSync5
{
    partial class Frm_Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mostrarEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reiniciarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tmr_Ocultar = new System.Windows.Forms.Timer(this.components);
            this.Lst_Terminales = new System.Windows.Forms.ListView();
            this.TERMINAL_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TERMINAL_NOMBRE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TERMINAL_EDO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TERMINAL_MSG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SITIO_NOMBRE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tmr_ChecaEstado = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "eClockSync";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mostrarEstadoToolStripMenuItem,
            this.reiniciarToolStripMenuItem,
            this.toolStripMenuItem2,
            this.abrirToolStripMenuItem,
            this.toolStripMenuItem3,
            this.configuraciónToolStripMenuItem,
            this.toolStripMenuItem1,
            this.cerrarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(233, 132);
            // 
            // mostrarEstadoToolStripMenuItem
            // 
            this.mostrarEstadoToolStripMenuItem.Name = "mostrarEstadoToolStripMenuItem";
            this.mostrarEstadoToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.mostrarEstadoToolStripMenuItem.Text = "Mostrar estado comunicación";
            this.mostrarEstadoToolStripMenuItem.Click += new System.EventHandler(this.mostrarEstadoToolStripMenuItem_Click);
            // 
            // reiniciarToolStripMenuItem
            // 
            this.reiniciarToolStripMenuItem.Name = "reiniciarToolStripMenuItem";
            this.reiniciarToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.reiniciarToolStripMenuItem.Text = "Reiniciar";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(229, 6);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.abrirToolStripMenuItem.Text = "Abrir eClock";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(229, 6);
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.configuraciónToolStripMenuItem.Text = "Configuración";
            this.configuraciónToolStripMenuItem.Click += new System.EventHandler(this.configuraciónToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(229, 6);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // Tmr_Ocultar
            // 
            this.Tmr_Ocultar.Interval = 500;
            this.Tmr_Ocultar.Tick += new System.EventHandler(this.Tmr_Ocultar_Tick);
            // 
            // Lst_Terminales
            // 
            this.Lst_Terminales.AllowColumnReorder = true;
            this.Lst_Terminales.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TERMINAL_ID,
            this.TERMINAL_NOMBRE,
            this.TERMINAL_EDO,
            this.TERMINAL_MSG,
            this.SITIO_NOMBRE});
            this.Lst_Terminales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lst_Terminales.GridLines = true;
            this.Lst_Terminales.Location = new System.Drawing.Point(0, 0);
            this.Lst_Terminales.Name = "Lst_Terminales";
            this.Lst_Terminales.Size = new System.Drawing.Size(746, 464);
            this.Lst_Terminales.TabIndex = 1;
            this.Lst_Terminales.UseCompatibleStateImageBehavior = false;
            this.Lst_Terminales.View = System.Windows.Forms.View.Details;
            // 
            // TERMINAL_ID
            // 
            this.TERMINAL_ID.Tag = "TERMINAL_ID";
            this.TERMINAL_ID.Text = "Identificador";
            this.TERMINAL_ID.Width = 34;
            // 
            // TERMINAL_NOMBRE
            // 
            this.TERMINAL_NOMBRE.Tag = "TERMINAL_NOMBRE";
            this.TERMINAL_NOMBRE.Text = "Nombre";
            this.TERMINAL_NOMBRE.Width = 164;
            // 
            // TERMINAL_EDO
            // 
            this.TERMINAL_EDO.Tag = "TERMINAL_EDO";
            this.TERMINAL_EDO.Text = "Estado Actual";
            this.TERMINAL_EDO.Width = 137;
            // 
            // TERMINAL_MSG
            // 
            this.TERMINAL_MSG.Tag = "TERMINAL_MSG";
            this.TERMINAL_MSG.Text = "Mensaje";
            this.TERMINAL_MSG.Width = 312;
            // 
            // SITIO_NOMBRE
            // 
            this.SITIO_NOMBRE.Tag = "SITIO_NOMBRE";
            this.SITIO_NOMBRE.Text = "Sitio";
            this.SITIO_NOMBRE.Width = 135;
            // 
            // Tmr_ChecaEstado
            // 
            this.Tmr_ChecaEstado.Interval = 5000;
            this.Tmr_ChecaEstado.Tick += new System.EventHandler(this.Tmr_ChecaEstado_Tick);
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 464);
            this.Controls.Add(this.Lst_Terminales);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_Main";
            this.Text = "Estado de comunicación";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem reiniciarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mostrarEstadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.Timer Tmr_Ocultar;
        private System.Windows.Forms.ListView Lst_Terminales;
        private System.Windows.Forms.ColumnHeader TERMINAL_ID;
        private System.Windows.Forms.ColumnHeader TERMINAL_NOMBRE;
        private System.Windows.Forms.ColumnHeader TERMINAL_EDO;
        private System.Windows.Forms.ColumnHeader SITIO_NOMBRE;
        private System.Windows.Forms.ColumnHeader TERMINAL_MSG;
        private System.Windows.Forms.Timer Tmr_ChecaEstado;
    }
}

