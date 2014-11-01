namespace eClockSync5
{
    partial class Frm_ModoFoto
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ModoFoto));
            this.Tmr_Ocultar = new System.Windows.Forms.Timer(this.components);
            this.Tmr_ChecaEstado = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mostrarEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reiniciarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.Lbl_Tarjeta = new System.Windows.Forms.Label();
            this.Lbl_Hora = new System.Windows.Forms.Label();
            this.Lbl_Fecha = new System.Windows.Forms.Label();
            this.Pnl_Persona = new System.Windows.Forms.Panel();
            this.Lbl_Franja = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Img_Usuario = new System.Windows.Forms.PictureBox();
            this.Lbl_NoCuenta = new System.Windows.Forms.Label();
            this.Lbl_Nombre = new System.Windows.Forms.Label();
            this.Lbl_Bienvenida = new System.Windows.Forms.Label();
            this.Reloj = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.Pnl_Persona.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Img_Usuario)).BeginInit();
            this.SuspendLayout();
            // 
            // Tmr_Ocultar
            // 
            this.Tmr_Ocultar.Interval = 500;
            this.Tmr_Ocultar.Tick += new System.EventHandler(this.Tmr_Ocultar_Tick);
            // 
            // Tmr_ChecaEstado
            // 
            this.Tmr_ChecaEstado.Interval = 5000;
            this.Tmr_ChecaEstado.Tick += new System.EventHandler(this.Tmr_ChecaEstado_Tick);
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
            this.reiniciarToolStripMenuItem.Click += new System.EventHandler(this.reiniciarToolStripMenuItem_Click);
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
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "eClockSync";
            this.notifyIcon1.Visible = true;
            // 
            // Lbl_Tarjeta
            // 
            this.Lbl_Tarjeta.AutoSize = true;
            this.Lbl_Tarjeta.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Tarjeta.Location = new System.Drawing.Point(-3, 277);
            this.Lbl_Tarjeta.Name = "Lbl_Tarjeta";
            this.Lbl_Tarjeta.Size = new System.Drawing.Size(60, 13);
            this.Lbl_Tarjeta.TabIndex = 2;
            this.Lbl_Tarjeta.Text = "Lbl_Tarjeta";
            // 
            // Lbl_Hora
            // 
            this.Lbl_Hora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Lbl_Hora.AutoSize = true;
            this.Lbl_Hora.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Hora.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Hora.ForeColor = System.Drawing.Color.White;
            this.Lbl_Hora.Location = new System.Drawing.Point(12, 382);
            this.Lbl_Hora.Name = "Lbl_Hora";
            this.Lbl_Hora.Size = new System.Drawing.Size(279, 59);
            this.Lbl_Hora.TabIndex = 4;
            this.Lbl_Hora.Text = "09:29 am";
            // 
            // Lbl_Fecha
            // 
            this.Lbl_Fecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_Fecha.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Fecha.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Fecha.ForeColor = System.Drawing.Color.White;
            this.Lbl_Fecha.Location = new System.Drawing.Point(140, 382);
            this.Lbl_Fecha.Name = "Lbl_Fecha";
            this.Lbl_Fecha.Size = new System.Drawing.Size(565, 59);
            this.Lbl_Fecha.TabIndex = 5;
            this.Lbl_Fecha.Text = "26-Junio-2013";
            this.Lbl_Fecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Pnl_Persona
            // 
            this.Pnl_Persona.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Pnl_Persona.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_Persona.BackgroundImage = global::eClockSync5.Properties.Resources.cuadro_blanco_sombra;
            this.Pnl_Persona.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pnl_Persona.Controls.Add(this.Lbl_Franja);
            this.Pnl_Persona.Controls.Add(this.panel3);
            this.Pnl_Persona.Controls.Add(this.Lbl_NoCuenta);
            this.Pnl_Persona.Controls.Add(this.Lbl_Nombre);
            this.Pnl_Persona.Controls.Add(this.Lbl_Tarjeta);
            this.Pnl_Persona.Controls.Add(this.Lbl_Bienvenida);
            this.Pnl_Persona.Location = new System.Drawing.Point(101, 61);
            this.Pnl_Persona.Name = "Pnl_Persona";
            this.Pnl_Persona.Padding = new System.Windows.Forms.Padding(13);
            this.Pnl_Persona.Size = new System.Drawing.Size(519, 303);
            this.Pnl_Persona.TabIndex = 6;
            // 
            // Lbl_Franja
            // 
            this.Lbl_Franja.BackColor = System.Drawing.Color.Red;
            this.Lbl_Franja.Dock = System.Windows.Forms.DockStyle.Right;
            this.Lbl_Franja.Location = new System.Drawing.Point(496, 13);
            this.Lbl_Franja.Name = "Lbl_Franja";
            this.Lbl_Franja.Size = new System.Drawing.Size(10, 277);
            this.Lbl_Franja.TabIndex = 19;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkBlue;
            this.panel3.Controls.Add(this.Img_Usuario);
            this.panel3.Location = new System.Drawing.Point(297, 45);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(172, 219);
            this.panel3.TabIndex = 18;
            // 
            // Img_Usuario
            // 
            this.Img_Usuario.BackColor = System.Drawing.Color.Transparent;
            this.Img_Usuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Img_Usuario.Image = global::eClockSync5.Properties.Resources.usuario;
            this.Img_Usuario.Location = new System.Drawing.Point(10, 10);
            this.Img_Usuario.Margin = new System.Windows.Forms.Padding(20);
            this.Img_Usuario.Name = "Img_Usuario";
            this.Img_Usuario.Size = new System.Drawing.Size(152, 199);
            this.Img_Usuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Img_Usuario.TabIndex = 11;
            this.Img_Usuario.TabStop = false;
            // 
            // Lbl_NoCuenta
            // 
            this.Lbl_NoCuenta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_NoCuenta.AutoSize = true;
            this.Lbl_NoCuenta.BackColor = System.Drawing.Color.Navy;
            this.Lbl_NoCuenta.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_NoCuenta.ForeColor = System.Drawing.Color.White;
            this.Lbl_NoCuenta.Location = new System.Drawing.Point(74, 239);
            this.Lbl_NoCuenta.Name = "Lbl_NoCuenta";
            this.Lbl_NoCuenta.Size = new System.Drawing.Size(116, 25);
            this.Lbl_NoCuenta.TabIndex = 17;
            this.Lbl_NoCuenta.Text = "18054805";
            // 
            // Lbl_Nombre
            // 
            this.Lbl_Nombre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_Nombre.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Nombre.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Nombre.ForeColor = System.Drawing.Color.DarkBlue;
            this.Lbl_Nombre.Location = new System.Drawing.Point(16, 160);
            this.Lbl_Nombre.Name = "Lbl_Nombre";
            this.Lbl_Nombre.Size = new System.Drawing.Size(270, 104);
            this.Lbl_Nombre.TabIndex = 16;
            this.Lbl_Nombre.Text = "Mariana Campillo Ríos";
            // 
            // Lbl_Bienvenida
            // 
            this.Lbl_Bienvenida.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lbl_Bienvenida.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Bienvenida.Font = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Bienvenida.ForeColor = System.Drawing.Color.DarkBlue;
            this.Lbl_Bienvenida.Location = new System.Drawing.Point(16, 13);
            this.Lbl_Bienvenida.Name = "Lbl_Bienvenida";
            this.Lbl_Bienvenida.Size = new System.Drawing.Size(268, 147);
            this.Lbl_Bienvenida.TabIndex = 13;
            this.Lbl_Bienvenida.Text = "Bienvenida";
            this.Lbl_Bienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Reloj
            // 
            this.Reloj.Enabled = true;
            this.Reloj.Interval = 1000;
            this.Reloj.Tick += new System.EventHandler(this.Reloj_Tick);
            // 
            // Frm_ModoFoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::eClockSync5.Properties.Resources.Bienvenida;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(717, 450);
            this.Controls.Add(this.Lbl_Hora);
            this.Controls.Add(this.Lbl_Fecha);
            this.Controls.Add(this.Pnl_Persona);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_ModoFoto";
            this.Text = "Modo Foto";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_ModoFoto_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Frm_ModoFoto_MouseClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.Pnl_Persona.ResumeLayout(false);
            this.Pnl_Persona.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Img_Usuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Tmr_Ocultar;
        private System.Windows.Forms.Timer Tmr_ChecaEstado;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mostrarEstadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reiniciarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label Lbl_Tarjeta;
        private System.Windows.Forms.Label Lbl_Hora;
        private System.Windows.Forms.Label Lbl_Fecha;
        private System.Windows.Forms.Panel Pnl_Persona;
        private System.Windows.Forms.PictureBox Img_Usuario;
        private System.Windows.Forms.Label Lbl_Bienvenida;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label Lbl_NoCuenta;
        private System.Windows.Forms.Label Lbl_Nombre;
        private System.Windows.Forms.Timer Reloj;
        private System.Windows.Forms.Label Lbl_Franja;
    }
}