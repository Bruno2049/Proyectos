namespace eEnroler
{
    partial class FrHuella
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrHuella));
            this.Img_Huella = new System.Windows.Forms.PictureBox();
            this.Btn_Enrolar1 = new System.Windows.Forms.Button();
            this.axZKFPEngX1 = new AxZKFPEngXControl.AxZKFPEngX();
            this.GbxHuella = new System.Windows.Forms.GroupBox();
            this.LNoHuella = new System.Windows.Forms.Label();
            this.LMensaje = new System.Windows.Forms.Label();
            this.Gbx_Empleado = new System.Windows.Forms.GroupBox();
            this.LblClave = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Btn_Borrar2 = new System.Windows.Forms.Button();
            this.Btn_Borrar1 = new System.Windows.Forms.Button();
            this.Btn_Enrolar2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Lbl_Tarjeta = new System.Windows.Forms.Label();
            this.Lbl_Nombre = new System.Windows.Forms.Label();
            this.Lbl_NoEmp = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Pbx_Foto = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Tbx_NoEmpleado = new System.Windows.Forms.TextBox();
            this.Btn_Buscar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Tmr_Cargar = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Img_Huella)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axZKFPEngX1)).BeginInit();
            this.GbxHuella.SuspendLayout();
            this.Gbx_Empleado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Foto)).BeginInit();
            this.SuspendLayout();
            // 
            // Img_Huella
            // 
            this.Img_Huella.BackColor = System.Drawing.Color.White;
            this.Img_Huella.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.Img_Huella, "Img_Huella");
            this.Img_Huella.Name = "Img_Huella";
            this.Img_Huella.TabStop = false;
            // 
            // Btn_Enrolar1
            // 
            resources.ApplyResources(this.Btn_Enrolar1, "Btn_Enrolar1");
            this.Btn_Enrolar1.Name = "Btn_Enrolar1";
            this.Btn_Enrolar1.UseVisualStyleBackColor = true;
            this.Btn_Enrolar1.Click += new System.EventHandler(this.Btn_Enrolar1_Click);
            // 
            // axZKFPEngX1
            // 
            resources.ApplyResources(this.axZKFPEngX1, "axZKFPEngX1");
            this.axZKFPEngX1.Name = "axZKFPEngX1";
            this.axZKFPEngX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axZKFPEngX1.OcxState")));
            this.axZKFPEngX1.OnFeatureInfo += new AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEventHandler(this.axZKFPEngX1_OnFeatureInfo);
            this.axZKFPEngX1.OnImageReceived += new AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEventHandler(this.axZKFPEngX1_OnImageReceived);
            this.axZKFPEngX1.OnEnroll += new AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEventHandler(this.axZKFPEngX1_OnEnroll);
            this.axZKFPEngX1.OnCapture += new AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEventHandler(this.axZKFPEngX1_OnCapture);
            this.axZKFPEngX1.OnFingerTouching += new System.EventHandler(this.axZKFPEngX1_OnFingerTouching);
            this.axZKFPEngX1.OnFingerLeaving += new System.EventHandler(this.axZKFPEngX1_OnFingerLeaving);
            // 
            // GbxHuella
            // 
            this.GbxHuella.Controls.Add(this.LNoHuella);
            this.GbxHuella.Controls.Add(this.LMensaje);
            this.GbxHuella.Controls.Add(this.Img_Huella);
            resources.ApplyResources(this.GbxHuella, "GbxHuella");
            this.GbxHuella.Name = "GbxHuella";
            this.GbxHuella.TabStop = false;
            // 
            // LNoHuella
            // 
            resources.ApplyResources(this.LNoHuella, "LNoHuella");
            this.LNoHuella.ForeColor = System.Drawing.Color.Green;
            this.LNoHuella.Name = "LNoHuella";
            // 
            // LMensaje
            // 
            resources.ApplyResources(this.LMensaje, "LMensaje");
            this.LMensaje.Name = "LMensaje";
            // 
            // Gbx_Empleado
            // 
            this.Gbx_Empleado.Controls.Add(this.LblClave);
            this.Gbx_Empleado.Controls.Add(this.label6);
            this.Gbx_Empleado.Controls.Add(this.Btn_Borrar2);
            this.Gbx_Empleado.Controls.Add(this.Btn_Borrar1);
            this.Gbx_Empleado.Controls.Add(this.Btn_Enrolar2);
            this.Gbx_Empleado.Controls.Add(this.label5);
            this.Gbx_Empleado.Controls.Add(this.label4);
            this.Gbx_Empleado.Controls.Add(this.Btn_Enrolar1);
            this.Gbx_Empleado.Controls.Add(this.Lbl_Tarjeta);
            this.Gbx_Empleado.Controls.Add(this.Lbl_Nombre);
            this.Gbx_Empleado.Controls.Add(this.Lbl_NoEmp);
            this.Gbx_Empleado.Controls.Add(this.button1);
            this.Gbx_Empleado.Controls.Add(this.Pbx_Foto);
            this.Gbx_Empleado.Controls.Add(this.label3);
            this.Gbx_Empleado.Controls.Add(this.label2);
            this.Gbx_Empleado.Controls.Add(this.label1);
            resources.ApplyResources(this.Gbx_Empleado, "Gbx_Empleado");
            this.Gbx_Empleado.Name = "Gbx_Empleado";
            this.Gbx_Empleado.TabStop = false;
            // 
            // LblClave
            // 
            resources.ApplyResources(this.LblClave, "LblClave");
            this.LblClave.Name = "LblClave";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // Btn_Borrar2
            // 
            resources.ApplyResources(this.Btn_Borrar2, "Btn_Borrar2");
            this.Btn_Borrar2.Name = "Btn_Borrar2";
            this.Btn_Borrar2.UseVisualStyleBackColor = true;
            this.Btn_Borrar2.Click += new System.EventHandler(this.Btn_Borrar2_Click);
            // 
            // Btn_Borrar1
            // 
            resources.ApplyResources(this.Btn_Borrar1, "Btn_Borrar1");
            this.Btn_Borrar1.Name = "Btn_Borrar1";
            this.Btn_Borrar1.UseVisualStyleBackColor = true;
            this.Btn_Borrar1.Click += new System.EventHandler(this.Btn_Borrar1_Click);
            // 
            // Btn_Enrolar2
            // 
            resources.ApplyResources(this.Btn_Enrolar2, "Btn_Enrolar2");
            this.Btn_Enrolar2.Name = "Btn_Enrolar2";
            this.Btn_Enrolar2.UseVisualStyleBackColor = true;
            this.Btn_Enrolar2.Click += new System.EventHandler(this.Btn_Enrolar2_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // Lbl_Tarjeta
            // 
            resources.ApplyResources(this.Lbl_Tarjeta, "Lbl_Tarjeta");
            this.Lbl_Tarjeta.Name = "Lbl_Tarjeta";
            this.Lbl_Tarjeta.TextChanged += new System.EventHandler(this.Lbl_Tarjeta_TextChanged);
            // 
            // Lbl_Nombre
            // 
            resources.ApplyResources(this.Lbl_Nombre, "Lbl_Nombre");
            this.Lbl_Nombre.Name = "Lbl_Nombre";
            // 
            // Lbl_NoEmp
            // 
            resources.ApplyResources(this.Lbl_NoEmp, "Lbl_NoEmp");
            this.Lbl_NoEmp.Name = "Lbl_NoEmp";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Pbx_Foto
            // 
            resources.ApplyResources(this.Pbx_Foto, "Pbx_Foto");
            this.Pbx_Foto.Name = "Pbx_Foto";
            this.Pbx_Foto.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Tbx_NoEmpleado
            // 
            resources.ApplyResources(this.Tbx_NoEmpleado, "Tbx_NoEmpleado");
            this.Tbx_NoEmpleado.Name = "Tbx_NoEmpleado";
            // 
            // Btn_Buscar
            // 
            resources.ApplyResources(this.Btn_Buscar, "Btn_Buscar");
            this.Btn_Buscar.Name = "Btn_Buscar";
            this.Btn_Buscar.UseVisualStyleBackColor = true;
            this.Btn_Buscar.Click += new System.EventHandler(this.Btn_Buscar_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // Tmr_Cargar
            // 
            this.Tmr_Cargar.Tick += new System.EventHandler(this.Tmr_Cargar_Tick);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FrHuella
            // 
            this.AcceptButton = this.Btn_Buscar;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Btn_Buscar);
            this.Controls.Add(this.Tbx_NoEmpleado);
            this.Controls.Add(this.Gbx_Empleado);
            this.Controls.Add(this.GbxHuella);
            this.Controls.Add(this.axZKFPEngX1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrHuella";
            this.Load += new System.EventHandler(this.FrHuella_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrHuella_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.Img_Huella)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axZKFPEngX1)).EndInit();
            this.GbxHuella.ResumeLayout(false);
            this.Gbx_Empleado.ResumeLayout(false);
            this.Gbx_Empleado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Foto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public AxZKFPEngXControl.AxZKFPEngX axZKFPEngX1;
        public System.Windows.Forms.PictureBox Img_Huella;
        public System.Windows.Forms.Button Btn_Enrolar1;
        private System.Windows.Forms.GroupBox GbxHuella;
        private System.Windows.Forms.GroupBox Gbx_Empleado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Lbl_Tarjeta;
        private System.Windows.Forms.Label Lbl_Nombre;
        private System.Windows.Forms.Label Lbl_NoEmp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Tbx_NoEmpleado;
        private System.Windows.Forms.Button Btn_Buscar;
        public System.Windows.Forms.Button Btn_Borrar2;
        public System.Windows.Forms.Button Btn_Borrar1;
        public System.Windows.Forms.Button Btn_Enrolar2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LblClave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LMensaje;
        private System.Windows.Forms.PictureBox Pbx_Foto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label LNoHuella;
        private System.Windows.Forms.Timer Tmr_Cargar;
        private System.Windows.Forms.Button button2;
    }
}