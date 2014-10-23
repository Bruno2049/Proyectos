namespace CRUD_Stored_Procedure_
{
    partial class FRM_Principal
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
            this.LBL_Titulo = new System.Windows.Forms.Label();
            this.LBL_Nombre = new System.Windows.Forms.Label();
            this.LBL_Apellido = new System.Windows.Forms.Label();
            this.LBL_Edad = new System.Windows.Forms.Label();
            this.LBL_Direccion = new System.Windows.Forms.Label();
            this.LBL_Correo = new System.Windows.Forms.Label();
            this.LBL_Telefono = new System.Windows.Forms.Label();
            this.LBL_Celular = new System.Windows.Forms.Label();
            this.LBL_SitioWeb = new System.Windows.Forms.Label();
            this.LBL_Compania = new System.Windows.Forms.Label();
            this.TXB_Nombre = new System.Windows.Forms.TextBox();
            this.TXB_Apellido = new System.Windows.Forms.TextBox();
            this.TXB_Edad = new System.Windows.Forms.TextBox();
            this.TXB_Correo = new System.Windows.Forms.TextBox();
            this.TXB_Direccion = new System.Windows.Forms.TextBox();
            this.TXB_Telefono = new System.Windows.Forms.TextBox();
            this.TXB_Celular = new System.Windows.Forms.TextBox();
            this.TXB_SitioWeb = new System.Windows.Forms.TextBox();
            this.TXB_Compania = new System.Windows.Forms.TextBox();
            this.BTN_IrInicio = new System.Windows.Forms.Button();
            this.BTN_Anterior = new System.Windows.Forms.Button();
            this.BTN_Siguiente = new System.Windows.Forms.Button();
            this.BTN_Final = new System.Windows.Forms.Button();
            this.BTN_Guardar = new System.Windows.Forms.Button();
            this.BTN_Actualizar = new System.Windows.Forms.Button();
            this.BTN_Eliminar = new System.Windows.Forms.Button();
            this.BTN_Reporte = new System.Windows.Forms.Button();
            this.BTN_Limpiar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LBL_Titulo
            // 
            this.LBL_Titulo.AutoSize = true;
            this.LBL_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Titulo.Location = new System.Drawing.Point(231, 25);
            this.LBL_Titulo.Name = "LBL_Titulo";
            this.LBL_Titulo.Size = new System.Drawing.Size(158, 20);
            this.LBL_Titulo.TabIndex = 0;
            this.LBL_Titulo.Text = "Registro de Usuarios";
            // 
            // LBL_Nombre
            // 
            this.LBL_Nombre.AutoSize = true;
            this.LBL_Nombre.Location = new System.Drawing.Point(67, 73);
            this.LBL_Nombre.Name = "LBL_Nombre";
            this.LBL_Nombre.Size = new System.Drawing.Size(47, 13);
            this.LBL_Nombre.TabIndex = 1;
            this.LBL_Nombre.Text = "Nombre:";
            // 
            // LBL_Apellido
            // 
            this.LBL_Apellido.AutoSize = true;
            this.LBL_Apellido.Location = new System.Drawing.Point(67, 110);
            this.LBL_Apellido.Name = "LBL_Apellido";
            this.LBL_Apellido.Size = new System.Drawing.Size(47, 13);
            this.LBL_Apellido.TabIndex = 2;
            this.LBL_Apellido.Text = "Apellido:";
            // 
            // LBL_Edad
            // 
            this.LBL_Edad.AutoSize = true;
            this.LBL_Edad.Location = new System.Drawing.Point(67, 150);
            this.LBL_Edad.Name = "LBL_Edad";
            this.LBL_Edad.Size = new System.Drawing.Size(35, 13);
            this.LBL_Edad.TabIndex = 3;
            this.LBL_Edad.Text = "Edad:";
            // 
            // LBL_Direccion
            // 
            this.LBL_Direccion.AutoSize = true;
            this.LBL_Direccion.Location = new System.Drawing.Point(67, 191);
            this.LBL_Direccion.Name = "LBL_Direccion";
            this.LBL_Direccion.Size = new System.Drawing.Size(55, 13);
            this.LBL_Direccion.TabIndex = 4;
            this.LBL_Direccion.Text = "Direccion:";
            // 
            // LBL_Correo
            // 
            this.LBL_Correo.AutoSize = true;
            this.LBL_Correo.Location = new System.Drawing.Point(67, 265);
            this.LBL_Correo.Name = "LBL_Correo";
            this.LBL_Correo.Size = new System.Drawing.Size(41, 13);
            this.LBL_Correo.TabIndex = 5;
            this.LBL_Correo.Text = "Correo:";
            // 
            // LBL_Telefono
            // 
            this.LBL_Telefono.AutoSize = true;
            this.LBL_Telefono.Location = new System.Drawing.Point(67, 309);
            this.LBL_Telefono.Name = "LBL_Telefono";
            this.LBL_Telefono.Size = new System.Drawing.Size(49, 13);
            this.LBL_Telefono.TabIndex = 6;
            this.LBL_Telefono.Text = "Telefono";
            // 
            // LBL_Celular
            // 
            this.LBL_Celular.AutoSize = true;
            this.LBL_Celular.Location = new System.Drawing.Point(67, 352);
            this.LBL_Celular.Name = "LBL_Celular";
            this.LBL_Celular.Size = new System.Drawing.Size(39, 13);
            this.LBL_Celular.TabIndex = 7;
            this.LBL_Celular.Text = "Celular";
            // 
            // LBL_SitioWeb
            // 
            this.LBL_SitioWeb.AutoSize = true;
            this.LBL_SitioWeb.Location = new System.Drawing.Point(67, 400);
            this.LBL_SitioWeb.Name = "LBL_SitioWeb";
            this.LBL_SitioWeb.Size = new System.Drawing.Size(53, 13);
            this.LBL_SitioWeb.TabIndex = 8;
            this.LBL_SitioWeb.Text = "Sitio Web";
            // 
            // LBL_Compania
            // 
            this.LBL_Compania.AutoSize = true;
            this.LBL_Compania.Location = new System.Drawing.Point(67, 445);
            this.LBL_Compania.Name = "LBL_Compania";
            this.LBL_Compania.Size = new System.Drawing.Size(54, 13);
            this.LBL_Compania.TabIndex = 9;
            this.LBL_Compania.Text = "Compañia";
            // 
            // TXB_Nombre
            // 
            this.TXB_Nombre.Location = new System.Drawing.Point(185, 70);
            this.TXB_Nombre.Name = "TXB_Nombre";
            this.TXB_Nombre.Size = new System.Drawing.Size(342, 20);
            this.TXB_Nombre.TabIndex = 10;
            // 
            // TXB_Apellido
            // 
            this.TXB_Apellido.Location = new System.Drawing.Point(185, 107);
            this.TXB_Apellido.Name = "TXB_Apellido";
            this.TXB_Apellido.Size = new System.Drawing.Size(342, 20);
            this.TXB_Apellido.TabIndex = 11;
            // 
            // TXB_Edad
            // 
            this.TXB_Edad.Location = new System.Drawing.Point(185, 147);
            this.TXB_Edad.Name = "TXB_Edad";
            this.TXB_Edad.Size = new System.Drawing.Size(67, 20);
            this.TXB_Edad.TabIndex = 12;
            // 
            // TXB_Correo
            // 
            this.TXB_Correo.Location = new System.Drawing.Point(185, 262);
            this.TXB_Correo.Name = "TXB_Correo";
            this.TXB_Correo.Size = new System.Drawing.Size(342, 20);
            this.TXB_Correo.TabIndex = 13;
            // 
            // TXB_Direccion
            // 
            this.TXB_Direccion.Location = new System.Drawing.Point(185, 188);
            this.TXB_Direccion.Multiline = true;
            this.TXB_Direccion.Name = "TXB_Direccion";
            this.TXB_Direccion.Size = new System.Drawing.Size(342, 60);
            this.TXB_Direccion.TabIndex = 14;
            // 
            // TXB_Telefono
            // 
            this.TXB_Telefono.Location = new System.Drawing.Point(185, 306);
            this.TXB_Telefono.Name = "TXB_Telefono";
            this.TXB_Telefono.Size = new System.Drawing.Size(342, 20);
            this.TXB_Telefono.TabIndex = 15;
            // 
            // TXB_Celular
            // 
            this.TXB_Celular.Location = new System.Drawing.Point(185, 352);
            this.TXB_Celular.Name = "TXB_Celular";
            this.TXB_Celular.Size = new System.Drawing.Size(342, 20);
            this.TXB_Celular.TabIndex = 16;
            // 
            // TXB_SitioWeb
            // 
            this.TXB_SitioWeb.Location = new System.Drawing.Point(185, 397);
            this.TXB_SitioWeb.Name = "TXB_SitioWeb";
            this.TXB_SitioWeb.Size = new System.Drawing.Size(342, 20);
            this.TXB_SitioWeb.TabIndex = 17;
            // 
            // TXB_Compania
            // 
            this.TXB_Compania.Location = new System.Drawing.Point(185, 442);
            this.TXB_Compania.Name = "TXB_Compania";
            this.TXB_Compania.Size = new System.Drawing.Size(342, 20);
            this.TXB_Compania.TabIndex = 18;
            // 
            // BTN_IrInicio
            // 
            this.BTN_IrInicio.Location = new System.Drawing.Point(110, 485);
            this.BTN_IrInicio.Name = "BTN_IrInicio";
            this.BTN_IrInicio.Size = new System.Drawing.Size(75, 23);
            this.BTN_IrInicio.TabIndex = 19;
            this.BTN_IrInicio.Text = "<<";
            this.BTN_IrInicio.UseVisualStyleBackColor = true;
            // 
            // BTN_Anterior
            // 
            this.BTN_Anterior.Location = new System.Drawing.Point(208, 485);
            this.BTN_Anterior.Name = "BTN_Anterior";
            this.BTN_Anterior.Size = new System.Drawing.Size(75, 23);
            this.BTN_Anterior.TabIndex = 20;
            this.BTN_Anterior.Text = "<";
            this.BTN_Anterior.UseVisualStyleBackColor = true;
            // 
            // BTN_Siguiente
            // 
            this.BTN_Siguiente.Location = new System.Drawing.Point(311, 485);
            this.BTN_Siguiente.Name = "BTN_Siguiente";
            this.BTN_Siguiente.Size = new System.Drawing.Size(75, 23);
            this.BTN_Siguiente.TabIndex = 21;
            this.BTN_Siguiente.Text = ">";
            this.BTN_Siguiente.UseVisualStyleBackColor = true;
            // 
            // BTN_Final
            // 
            this.BTN_Final.Location = new System.Drawing.Point(408, 485);
            this.BTN_Final.Name = "BTN_Final";
            this.BTN_Final.Size = new System.Drawing.Size(75, 23);
            this.BTN_Final.TabIndex = 22;
            this.BTN_Final.Text = ">>";
            this.BTN_Final.UseVisualStyleBackColor = true;
            // 
            // BTN_Guardar
            // 
            this.BTN_Guardar.Location = new System.Drawing.Point(70, 535);
            this.BTN_Guardar.Name = "BTN_Guardar";
            this.BTN_Guardar.Size = new System.Drawing.Size(75, 23);
            this.BTN_Guardar.TabIndex = 23;
            this.BTN_Guardar.Text = "Guardar";
            this.BTN_Guardar.UseVisualStyleBackColor = true;
            this.BTN_Guardar.Click += new System.EventHandler(this.BTN_Guardar_Click);
            // 
            // BTN_Actualizar
            // 
            this.BTN_Actualizar.Location = new System.Drawing.Point(161, 535);
            this.BTN_Actualizar.Name = "BTN_Actualizar";
            this.BTN_Actualizar.Size = new System.Drawing.Size(75, 23);
            this.BTN_Actualizar.TabIndex = 24;
            this.BTN_Actualizar.Text = "Actualizar";
            this.BTN_Actualizar.UseVisualStyleBackColor = true;
            // 
            // BTN_Eliminar
            // 
            this.BTN_Eliminar.Location = new System.Drawing.Point(251, 535);
            this.BTN_Eliminar.Name = "BTN_Eliminar";
            this.BTN_Eliminar.Size = new System.Drawing.Size(75, 23);
            this.BTN_Eliminar.TabIndex = 25;
            this.BTN_Eliminar.Text = "Eliminar";
            this.BTN_Eliminar.UseVisualStyleBackColor = true;
            this.BTN_Eliminar.Click += new System.EventHandler(this.BTN_Eliminar_Click);
            // 
            // BTN_Reporte
            // 
            this.BTN_Reporte.Location = new System.Drawing.Point(347, 535);
            this.BTN_Reporte.Name = "BTN_Reporte";
            this.BTN_Reporte.Size = new System.Drawing.Size(75, 23);
            this.BTN_Reporte.TabIndex = 26;
            this.BTN_Reporte.Text = "Reporte";
            this.BTN_Reporte.UseVisualStyleBackColor = true;
            // 
            // BTN_Limpiar
            // 
            this.BTN_Limpiar.Location = new System.Drawing.Point(439, 535);
            this.BTN_Limpiar.Name = "BTN_Limpiar";
            this.BTN_Limpiar.Size = new System.Drawing.Size(75, 23);
            this.BTN_Limpiar.TabIndex = 27;
            this.BTN_Limpiar.Text = "Limpiar";
            this.BTN_Limpiar.UseVisualStyleBackColor = true;
            // 
            // FRM_Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 585);
            this.Controls.Add(this.BTN_Limpiar);
            this.Controls.Add(this.BTN_Reporte);
            this.Controls.Add(this.BTN_Eliminar);
            this.Controls.Add(this.BTN_Actualizar);
            this.Controls.Add(this.BTN_Guardar);
            this.Controls.Add(this.BTN_Final);
            this.Controls.Add(this.BTN_Siguiente);
            this.Controls.Add(this.BTN_Anterior);
            this.Controls.Add(this.BTN_IrInicio);
            this.Controls.Add(this.TXB_Compania);
            this.Controls.Add(this.TXB_SitioWeb);
            this.Controls.Add(this.TXB_Celular);
            this.Controls.Add(this.TXB_Telefono);
            this.Controls.Add(this.TXB_Direccion);
            this.Controls.Add(this.TXB_Correo);
            this.Controls.Add(this.TXB_Edad);
            this.Controls.Add(this.TXB_Apellido);
            this.Controls.Add(this.TXB_Nombre);
            this.Controls.Add(this.LBL_Compania);
            this.Controls.Add(this.LBL_SitioWeb);
            this.Controls.Add(this.LBL_Celular);
            this.Controls.Add(this.LBL_Telefono);
            this.Controls.Add(this.LBL_Correo);
            this.Controls.Add(this.LBL_Direccion);
            this.Controls.Add(this.LBL_Edad);
            this.Controls.Add(this.LBL_Apellido);
            this.Controls.Add(this.LBL_Nombre);
            this.Controls.Add(this.LBL_Titulo);
            this.Name = "FRM_Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Usuarios";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBL_Titulo;
        private System.Windows.Forms.Label LBL_Nombre;
        private System.Windows.Forms.Label LBL_Apellido;
        private System.Windows.Forms.Label LBL_Edad;
        private System.Windows.Forms.Label LBL_Direccion;
        private System.Windows.Forms.Label LBL_Correo;
        private System.Windows.Forms.Label LBL_Telefono;
        private System.Windows.Forms.Label LBL_Celular;
        private System.Windows.Forms.Label LBL_SitioWeb;
        private System.Windows.Forms.Label LBL_Compania;
        private System.Windows.Forms.TextBox TXB_Nombre;
        private System.Windows.Forms.TextBox TXB_Apellido;
        private System.Windows.Forms.TextBox TXB_Edad;
        private System.Windows.Forms.TextBox TXB_Correo;
        private System.Windows.Forms.TextBox TXB_Direccion;
        private System.Windows.Forms.TextBox TXB_Telefono;
        private System.Windows.Forms.TextBox TXB_Celular;
        private System.Windows.Forms.TextBox TXB_SitioWeb;
        private System.Windows.Forms.TextBox TXB_Compania;
        private System.Windows.Forms.Button BTN_IrInicio;
        private System.Windows.Forms.Button BTN_Anterior;
        private System.Windows.Forms.Button BTN_Siguiente;
        private System.Windows.Forms.Button BTN_Final;
        private System.Windows.Forms.Button BTN_Guardar;
        private System.Windows.Forms.Button BTN_Actualizar;
        private System.Windows.Forms.Button BTN_Eliminar;
        private System.Windows.Forms.Button BTN_Reporte;
        private System.Windows.Forms.Button BTN_Limpiar;
    }
}

