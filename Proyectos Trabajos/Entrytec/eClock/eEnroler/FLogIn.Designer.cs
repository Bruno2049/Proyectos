namespace eEnroler
{
    partial class FLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLogIn));
            this.Tbx_Clave = new System.Windows.Forms.TextBox();
            this.Tbx_Usuario = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Btn_Iniciar = new System.Windows.Forms.Button();
            this.Lnk_NuevoUsuario = new System.Windows.Forms.LinkLabel();
            this.Lnk_OlvidoClave = new System.Windows.Forms.LinkLabel();
            this.LblMensaje = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Linea1 = new System.Windows.Forms.PictureBox();
            this.Linea2 = new System.Windows.Forms.PictureBox();
            this.Pbx_Icono = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Linea1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Linea2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Icono)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Tbx_Clave
            // 
            this.Tbx_Clave.Location = new System.Drawing.Point(179, 172);
            this.Tbx_Clave.Name = "Tbx_Clave";
            this.Tbx_Clave.Size = new System.Drawing.Size(100, 20);
            this.Tbx_Clave.TabIndex = 26;
            this.Tbx_Clave.UseSystemPasswordChar = true;
            // 
            // Tbx_Usuario
            // 
            this.Tbx_Usuario.Location = new System.Drawing.Point(179, 146);
            this.Tbx_Usuario.Name = "Tbx_Usuario";
            this.Tbx_Usuario.Size = new System.Drawing.Size(100, 20);
            this.Tbx_Usuario.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(103, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Contraseña:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Usuario:";
            // 
            // Btn_Iniciar
            // 
            this.Btn_Iniciar.Location = new System.Drawing.Point(298, 161);
            this.Btn_Iniciar.Name = "Btn_Iniciar";
            this.Btn_Iniciar.Size = new System.Drawing.Size(93, 23);
            this.Btn_Iniciar.TabIndex = 22;
            this.Btn_Iniciar.Text = "Iniciar Sesión";
            this.Btn_Iniciar.UseVisualStyleBackColor = true;
            this.Btn_Iniciar.Click += new System.EventHandler(this.Btn_Iniciar_Click);
            // 
            // Lnk_NuevoUsuario
            // 
            this.Lnk_NuevoUsuario.AutoSize = true;
            this.Lnk_NuevoUsuario.Location = new System.Drawing.Point(188, 242);
            this.Lnk_NuevoUsuario.Name = "Lnk_NuevoUsuario";
            this.Lnk_NuevoUsuario.Size = new System.Drawing.Size(203, 13);
            this.Lnk_NuevoUsuario.TabIndex = 21;
            this.Lnk_NuevoUsuario.TabStop = true;
            this.Lnk_NuevoUsuario.Text = "No tengo un usuario y deseo obtener uno";
            this.Lnk_NuevoUsuario.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lnk_NuevoUsuario_LinkClicked);
            // 
            // Lnk_OlvidoClave
            // 
            this.Lnk_OlvidoClave.AutoSize = true;
            this.Lnk_OlvidoClave.Location = new System.Drawing.Point(7, 242);
            this.Lnk_OlvidoClave.Name = "Lnk_OlvidoClave";
            this.Lnk_OlvidoClave.Size = new System.Drawing.Size(135, 13);
            this.Lnk_OlvidoClave.TabIndex = 20;
            this.Lnk_OlvidoClave.TabStop = true;
            this.Lnk_OlvidoClave.Text = "No recuerdo mi contraseña";
            this.Lnk_OlvidoClave.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lnk_OlvidoClave_LinkClicked);
            // 
            // LblMensaje
            // 
            this.LblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensaje.Location = new System.Drawing.Point(68, 47);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.Size = new System.Drawing.Size(323, 42);
            this.LblMensaje.TabIndex = 19;
            this.LblMensaje.Text = "Para continuar debes ingresar tu usuario y contraseña.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(122, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 24);
            this.label1.TabIndex = 18;
            this.label1.Text = "Inicio de sesión";
            // 
            // Linea1
            // 
            this.Linea1.BackColor = System.Drawing.Color.LimeGreen;
            this.Linea1.Location = new System.Drawing.Point(-5, 31);
            this.Linea1.Name = "Linea1";
            this.Linea1.Size = new System.Drawing.Size(408, 10);
            this.Linea1.TabIndex = 17;
            this.Linea1.TabStop = false;
            // 
            // Linea2
            // 
            this.Linea2.BackColor = System.Drawing.Color.LimeGreen;
            this.Linea2.Location = new System.Drawing.Point(-5, 105);
            this.Linea2.Name = "Linea2";
            this.Linea2.Size = new System.Drawing.Size(408, 10);
            this.Linea2.TabIndex = 16;
            this.Linea2.TabStop = false;
            // 
            // Pbx_Icono
            // 
            this.Pbx_Icono.Image = global::eEnroler.Properties.Resources.okshield;
            this.Pbx_Icono.Location = new System.Drawing.Point(6, 47);
            this.Pbx_Icono.Name = "Pbx_Icono";
            this.Pbx_Icono.Size = new System.Drawing.Size(56, 56);
            this.Pbx_Icono.TabIndex = 15;
            this.Pbx_Icono.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::eEnroler.Properties.Resources.candadosinfondo;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 121);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 97);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // FLogIn
            // 
            this.AcceptButton = this.Btn_Iniciar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 259);
            this.Controls.Add(this.Tbx_Clave);
            this.Controls.Add(this.Tbx_Usuario);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Btn_Iniciar);
            this.Controls.Add(this.Lnk_NuevoUsuario);
            this.Controls.Add(this.Lnk_OlvidoClave);
            this.Controls.Add(this.LblMensaje);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Linea1);
            this.Controls.Add(this.Linea2);
            this.Controls.Add(this.Pbx_Icono);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FLogIn";
            this.Text = "Inicio de sesión";
            ((System.ComponentModel.ISupportInitialize)(this.Linea1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Linea2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx_Icono)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Tbx_Clave;
        private System.Windows.Forms.TextBox Tbx_Usuario;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Btn_Iniciar;
        private System.Windows.Forms.LinkLabel Lnk_NuevoUsuario;
        private System.Windows.Forms.LinkLabel Lnk_OlvidoClave;
        private System.Windows.Forms.Label LblMensaje;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Linea1;
        private System.Windows.Forms.PictureBox Linea2;
        private System.Windows.Forms.PictureBox Pbx_Icono;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}