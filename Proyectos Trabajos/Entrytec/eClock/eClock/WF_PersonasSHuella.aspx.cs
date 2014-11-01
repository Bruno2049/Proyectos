using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_PersonasSHuella.
    /// </summary>
    public partial class WF_PersonasSHuella : System.Web.UI.Page
    {
        protected DS_PersonasSHuella dS_PersonasSHuella1;
        CeC_Sesion Sesion;
        private void ActualizaGrid()
        {
            dS_PersonasSHuella1.Clear();
            DS_PersonasSHuellaTableAdapters.EC_PERSONASTableAdapter TA = new DS_PersonasSHuellaTableAdapters.EC_PERSONASTableAdapter();
            TA.ActualizaIn("SELECT PERSONA_ID FROM EC_PERSONAS WHERE SUSCRIPCION_ID =" + Sesion.SUSCRIPCION_ID);
            TA.Fill(dS_PersonasSHuella1.EC_PERSONAS);
            Grid.DataBind();
        }

        private void LimpiaMsg()
        {
            Lbl_Error.Text = "";
            Lbl_Correcto.Text = "";
        }

        private void AgregaEmpleados(string Qry)
        {
            return;
            int Borrados = 0;
            int NBorrados = 0;
            string TBorrados = "";
            string TNBorrados = "";
            object Obj = CeC_BD.EjecutaDataSet("SELECT PERSONA_ID, PERSONA_LINK_ID, TIPO_PERSONA_ID, PERSONA_NOMBRE FROM EC_PERSONAS WHERE PERSONA_ID in (" + Qry + ")");
            if (Obj == null)
            {
                Lbl_Error.Text = "No se encontraron empleados para agregar";
                return;
            }
            DataSet DS = (DataSet)(Obj);

            for (int Cont = 0; Cont < DS.Tables[0].Rows.Count; Cont++)
            {
                int Persona_ID = Convert.ToInt32(DS.Tables[0].Rows[Cont].ItemArray[0]);
                int NoEmp = Convert.ToInt32(DS.Tables[0].Rows[Cont].ItemArray[1]);
                string Nombre = Convert.ToString(DS.Tables[0].Rows[Cont].ItemArray[2]);
                string Completo = "No. Empleado = " + NoEmp + ", Nombre = " + Nombre;
                if (CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS_S_HUELLA WHERE PERSONA_ID = " + Persona_ID.ToString()) > 0)
                    continue;
                if (CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_S_HUELLA (PERSONA_ID, PERSONA_S_HUELLA_FECHA) VALUES(" + Persona_ID.ToString() + "," + CeC_BD.SqlFechaHora(DateTime.Now) + ")") <= 0)
                {
                    TNBorrados += Completo + "\n";
                    NBorrados++;
                }
                else
                {
                    TBorrados += Completo + "\n";
                    Borrados++;
                }
            }
            if (NBorrados > 0)
            {
                Lbl_Error.Text = "No se pudieron agregar los siguientes empleados: \n" + TNBorrados;
            }
            if (Borrados > 0)
            {
                Lbl_Correcto.Text = "\nSe agregaron los siguientes empleados \n" + TBorrados;
            }
            ActualizaGrid();
        }

        private void Habilitarcontroles()
        {
            Wtx_PERSONA_LINK_ID_S_HUELLA.Visible = false;
            Grid.Visible = false;
            Webimagebutton1.Visible = false;
            Webimagebutton2.Visible = false;
            Webimagebutton3.Visible = false;
            BtnBorrar.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Personas sin Huella";
            // Sesion.DescripcionPagina = "En el listado inferior se encuentran todas las personas que checaran sin huella");

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0SinHuella, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

            if (!IsPostBack)
            {

                Sesion.WF_EmpleadosFil_Qry = "";

                ActualizaGrid();
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Personas Sin Huella", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            }
        }

        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dS_PersonasSHuella1 = new DS_PersonasSHuella();
            ((System.ComponentModel.ISupportInitialize)(this.dS_PersonasSHuella1)).BeginInit();
            // 
            // dS_PersonasSHuella1
            // 
            this.dS_PersonasSHuella1.DataSetName = "DS_PersonasSHuella";
            this.dS_PersonasSHuella1.Locale = new System.Globalization.CultureInfo("es-MX");
            this.dS_PersonasSHuella1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            ((System.ComponentModel.ISupportInitialize)(this.dS_PersonasSHuella1)).EndInit();

        }
        #endregion

        protected void Webimagebutton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            try
            {
                //	LimpiaMsg();
                if (Wtx_PERSONA_LINK_ID_S_HUELLA.Text == "")
                {
                    Lbl_Error.Text = "No eligio al empleado";
                    Lbl_Correcto.Text = "";
//                    Sesion.WF_EmpleadosFil(false, false, false, "Muestra Resultados", "Filtro-Empleados para Listado sin Huella", "WF_PersonasSHuella.aspx?Parametros=1", "Filtro-Empleados para Listado sin Huella", false, true, false);
                    return;
                }
                if (Wtx_PERSONA_LINK_ID_S_HUELLA.Text != "")
                {
                    int Persona_ID = CeC_Personas.ObtenPersonaID(CeC.Convierte2Int(Wtx_PERSONA_LINK_ID_S_HUELLA.Value),Sesion.USUARIO_ID);

                    if (Persona_ID < 0)
                    {
                        Lbl_Error.Text = "Empleado no registrado";
                        Lbl_Error.Text = "";
                        return;
                    }
                    if (CeC_BD.EjecutaEscalarInt("SELECT COUNT(*) FROM EC_PERSONAS_S_HUELLA WHERE PERSONA_ID =" + Persona_ID) < 1)
                    {
                        if (CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_S_HUELLA (PERSONA_ID, PERSONA_S_HUELLA_FECHA,PERSONA_S_HUELLA_CLAVE) VALUES(" + Persona_ID.ToString() + "," + CeC_BD.SqlFechaHora(DateTime.Now) + ",'" + Wtx_PERSONA_S_HUELLA_CLAVE.Text + "')") <= 0)
                            Lbl_Error.Text = "No se pudo agregar el empleado, posiblemente ya se encuentre registrado";
                        else
                        {
                            Lbl_Correcto.Text = "Empleado '" + CeC_BD.EjecutaEscalarString("SELECT PERSONA_NOMBRE FROM EC_PERSONAS WHERE PERSONA_ID = " + Persona_ID.ToString()) + "' agregado correctamente";
                            ActualizaGrid();
                            Lbl_Correcto.Visible = true;
                            Lbl_Error.Text = "";
                            Wtx_PERSONA_LINK_ID_S_HUELLA.Text = "";
                        }
                    }
                    else
                    {
                        Lbl_Error.Text = "El Empleado " + 
                            Wtx_PERSONA_LINK_ID_S_HUELLA.Value.ToString() + 
                            " ya se encuentra en el listado de personas sin huella, fue agregado el dia: " + 
                            CeC_BD.EjecutaEscalarDateTime("SELECT PERSONA_S_HUELLA_FECHA FROM EC_PERSONAS_S_HUELLA WHERE PERSONA_ID =" + Persona_ID);
                        Wtx_PERSONA_LINK_ID_S_HUELLA.Text = "";
                        Lbl_Correcto.Text = "";
                    }
                }
            }
            catch (System.Exception ex)
            {
                Lbl_Error.Text = ex.Message;
                Lbl_Correcto.Text = "";
                return;
            }
        }

        protected void BtnBorrar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            int Borrados = 0;
            int NBorrados = 0;
            string TBorrados = "";
            string TNBorrados = "";
            LimpiaMsg();
            for (int Cont = 0; Cont < Grid.Rows.Count; Cont++)
            {
                if (Convert.ToInt32(Grid.Rows[Cont].Cells[0].Value) != 0)
                {
                    int Persona_ID = Convert.ToInt32(Grid.Rows[Cont].Cells[1].Value);
                    int NoEmp = Convert.ToInt32(Grid.Rows[Cont].Cells[2].Value);
                    string Nombre = Convert.ToString(Grid.Rows[Cont].Cells[3].Value);
                    string Completo = "No. Empleado = " + NoEmp + ", Nombre = " + Nombre;
                    if (CeC_BD.EjecutaComando("DELETE EC_PERSONAS_S_HUELLA WHERE PERSONA_ID = " + Persona_ID.ToString() + "") <= 0)
                    {
                        TNBorrados += Completo + "\n";
                        NBorrados++;
                    }
                    else
                    {
                        TBorrados += Completo + "\n";
                        Borrados++;
                    }
                }
            }
            if (NBorrados > 0)
            {
                Lbl_Error.Text = "No se pudieron quitar los siguientes empleados de la lista: \n" + TNBorrados;
            }
            if (Borrados > 0)
            {
                Lbl_Correcto.Text = "\nSe quitaron los siguientes empleados de la lista \n" + TBorrados;
            }
            ActualizaGrid();
        }

        void CheckTodos(bool Check)
        {
            for (int Cont = 0; Cont < Grid.Rows.Count; Cont++)
            {
                if (Check)
                    Grid.Rows[Cont].Cells[0].Value = 1;
                else
                    Grid.Rows[Cont].Cells[0].Value = 0;
            }
        }

        protected void Webimagebutton2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            CheckTodos(false);
        }

        protected void Webimagebutton3_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            CheckTodos(true);
        }

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid);
            Grid.Rows.Band.RowSelectorStyle.Width = 10;
        }
        protected void Btn_Terminales_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            //Response.Redirect("WF_Terminales.aspx", false);
            Sesion.Redirige("WF_Terminales.aspx","Editar");
        }
}
}
