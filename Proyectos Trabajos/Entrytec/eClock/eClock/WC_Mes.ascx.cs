using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
/// <summary>
///		Descripción breve de WC_Mes.
/// </summary>
public partial class WC_Mes : System.Web.UI.UserControl
{

    //Contiene el Mes y el año que deberá cargar
    public DateTime Mes;

    public void DiasVisibles(bool Visible)
    {
        for (int i = 1; i < 32; i++)
        {
            FindControl("LB" + i.ToString()).Visible = Visible;
        }
    }


    public System.Web.UI.WebControls.LinkButton ObtenLB(int Dia)
    {
        return (System.Web.UI.WebControls.LinkButton)FindControl("LB" + Dia.ToString());
    }
    public bool MuestraTDias()
    {
        ObtenLB(29).ForeColor = LB1.ForeColor;
        ObtenLB(30).ForeColor = LB1.ForeColor;
        ObtenLB(31).ForeColor = LB1.ForeColor;

        return true;
    }

    public bool Inicializa(int Persona_ID, DateTime mes)
    {
        return Inicializa(Persona_ID, mes, true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Persona_ID"></param>
    /// <param name="mes"></param>
    /// <param name="MostrarMes">Indica si mostrara la etiqueta del mes o la de el Nombre de la persona</param>
    /// <returns></returns>
    public bool Inicializa(int Persona_ID, DateTime mes, bool MostrarMes)
    {


        Mes = new DateTime(mes.Year, mes.Month, 1);
        DateTime DiaInicio = Mes;
        DateTime DiaFin = Mes.AddMonths(1);
        object ObjConexion = CeC_BD.ObtenConexion();
        if (ObjConexion == null)
        {
            return false;
        }
        if (MostrarMes)
            LMes.Text = DiaInicio.ToString("MMMM yyyy");
        else
            LMes.Text = CeC_BD.ObtenPersonaNombre(Persona_ID);
        LMes.ToolTip = Persona_ID + "|" + DiaInicio.ToString();
        for (int Cont = 1; Cont <= 31; Cont++)
        {
            System.Web.UI.WebControls.LinkButton LB = ObtenLB(Cont);
            LB.Visible = false;
        }
        OleDbCommand Comando = ((OleDbConnection)ObjConexion).CreateCommand();
        if (CeC_BD.EsOracle)
        {
            Comando.CommandText = @"SELECT " +
            " EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID, " + //0
            " EC_PERSONAS_DIARIO.PERSONA_ID, " + //1
            " EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, " + //2
            " EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ABR, " + //03
            " EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_NOMBRE, " +//04
            " EC_DIAS_FESTIVOS.DIA_FESTIVO_NOMBRE, " +//05
            " EC_TIPO_INC_SIS.TIPO_INC_SIS_ABR, " + //06
                " EC_TIPO_INC_SIS.TIPO_INC_SIS_NOMBRE, " +//07
                " TO_CHAR((SELECT EC_ACCESOS.ACCESO_FECHAHORA FROM EC_ACCESOS  WHERE ACCESO_ID = ACCESO_E_ID),'DD/MM/YYYY HH24:MI:SS' ) as ACCESO_E, " +
                " TO_CHAR((SELECT EC_ACCESOS.ACCESO_FECHAHORA  FROM EC_ACCESOS WHERE ACCESO_ID = ACCESO_S_ID),'DD/MM/YYYY HH24:MI:SS' )  as ACCESO_S, " +
                " TO_CHAR((SELECT EC_ACCESOS.ACCESO_FECHAHORA FROM EC_ACCESOS WHERE ACCESO_ID = ACCESO_CS_ID),'DD/MM/YYYY HH24:MI:SS' )  as ACCESO_CS, " +
                " TO_CHAR((SELECT EC_ACCESOS.ACCESO_FECHAHORA FROM  EC_ACCESOS WHERE ACCESO_ID = ACCESO_CR_ID),'DD/MM/YYYY HH24:MI:SS' )  as ACCESO_CR, " +
                " DIA_FESTIVO_BORRADO" +//12
            " FROM EC_PERSONAS_DIARIO, " +
            " EC_TIPO_INC_SIS, EC_INCIDENCIAS, EC_TIPO_INCIDENCIAS, EC_DIAS_FESTIVOS WHERE EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID = EC_TIPO_INC_SIS.TIPO_INC_SIS_ID AND EC_PERSONAS_DIARIO.INCIDENCIA_ID = EC_INCIDENCIAS.INCIDENCIA_ID AND EC_INCIDENCIAS.TIPO_INCIDENCIA_ID = EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID AND EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA = EC_DIAS_FESTIVOS.DIA_FESTIVO_FECHA (+) AND (EC_PERSONAS_DIARIO.PERSONA_ID = ?) AND (EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= ? AND EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA < ?)";
        }
        else
        {
            Comando.CommandText = "SELECT     EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID, EC_PERSONAS_DIARIO.PERSONA_ID, " +
                 " EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ABR,  " +
                  " EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_NOMBRE, EC_DIAS_FESTIVOS.DIA_FESTIVO_NOMBRE,  " +
                  " EC_TIPO_INC_SIS.TIPO_INC_SIS_ABR, EC_TIPO_INC_SIS.TIPO_INC_SIS_NOMBRE, CONVERT(varchar, " +
                     "  (SELECT     ACCESO_FECHAHORA " +
                       "  FROM          EC_ACCESOS " +
                        " WHERE      (ACCESO_ID = EC_PERSONAS_DIARIO.ACCESO_E_ID)), 114) AS ACCESO_E, CONVERT(varchar, " +
                      " (SELECT     ACCESO_FECHAHORA " +
                        " FROM          EC_ACCESOS AS EC_ACCESOS_3 " +
                        " WHERE      (ACCESO_ID = EC_PERSONAS_DIARIO.ACCESO_S_ID)), 114) AS ACCESO_S, CONVERT(varchar, " +
                      " (SELECT     ACCESO_FECHAHORA " +
                        " FROM          EC_ACCESOS AS EC_ACCESOS_2 " +
                        " WHERE      (ACCESO_ID = EC_PERSONAS_DIARIO.ACCESO_CS_ID)), 114) AS ACCESO_CS, CONVERT(varchar, " +
                      " (SELECT     ACCESO_FECHAHORA " +
                        " FROM          EC_ACCESOS AS EC_ACCESOS_1 " +
                        " WHERE      (ACCESO_ID = EC_PERSONAS_DIARIO.ACCESO_CR_ID)), 114) AS ACCESO_CR,  " +
                  " EC_DIAS_FESTIVOS.DIA_FESTIVO_BORRADO " +
" FROM         EC_PERSONAS_DIARIO INNER JOIN " +
"                     EC_TIPO_INC_SIS ON EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID = EC_TIPO_INC_SIS.TIPO_INC_SIS_ID INNER JOIN " +
"                   EC_INCIDENCIAS ON EC_PERSONAS_DIARIO.INCIDENCIA_ID = EC_INCIDENCIAS.INCIDENCIA_ID INNER JOIN " +
  "                 EC_TIPO_INCIDENCIAS ON EC_INCIDENCIAS.TIPO_INCIDENCIA_ID = EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID LEFT OUTER JOIN " +
    "               EC_DIAS_FESTIVOS ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA = EC_DIAS_FESTIVOS.DIA_FESTIVO_FECHA " +
" WHERE     (EC_PERSONAS_DIARIO.PERSONA_ID = ?) AND (EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= ?) AND  " +
"                     (EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA < ?) ";
        }
        Comando.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
        Comando.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
        Comando.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA1", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));

        Comando.Parameters["PERSONA_ID"].Value = Persona_ID;
        Comando.Parameters["PERSONA_DIARIO_FECHA"].Value = DiaInicio;
        Comando.Parameters["PERSONA_DIARIO_FECHA1"].Value = DiaFin;
        try
        {
            OleDbDataReader DR = Comando.ExecuteReader();
            while (DR.Read())
            {
                DateTime Dia = Convert.ToDateTime(DR.GetValue(2));
                string Mensage = "";
                string Texto = "__";
                string HEntrada = "";
                System.Web.UI.WebControls.LinkButton LB = ObtenLB(Dia.Day);
                LB.Visible = true;

                /*	if (Dia == new DateTime(2006,07,01))
                    {
                        Mensage = "JAJAJ";
                    }*/
                if (!DR.IsDBNull(6) && DR.GetValue(6).ToString().Length > 0)
                    Texto = DR.GetValue(6).ToString();
                if (!DR.IsDBNull(7) && DR.GetValue(7).ToString().Length > 0)
                    Mensage = DR.GetValue(7).ToString();

                if (!DR.IsDBNull(5) && DR.GetValue(5).ToString().Length > 0)
                {
                    if (DR.GetValue(12).ToString() == "0")
                    {
                        Texto = "*";
                        Mensage = DR.GetValue(5).ToString();
                    }
                }
                if (!DR.IsDBNull(3) && DR.GetValue(3).ToString().Length > 0)
                {
                    Texto = DR.GetValue(3).ToString();
                    if (!DR.IsDBNull(4) && DR.GetValue(4).ToString().Length > 0)
                        Mensage = DR.GetValue(4).ToString();
                }
                if (Texto != "*")
                {
                    if (!DR.IsDBNull(0))
                    {

                    }
                    string HE = "";
                    string HS = "";
                    if (!DR.IsDBNull(8))
                        HE = "\n" + DR.GetValue(8).ToString();

                    if (!DR.IsDBNull(9))
                        HS = "\n" + DR.GetValue(9).ToString();
                    Mensage += " " + HE + " " + HS;
                    LB.BackColor = Color.White;
                }
                else
                {
                    LB.BackColor = Color.DarkOrange;
                }
                if (Texto.Length < 2)
                {
                    Texto += "_";
                }

                if (Dia.DayOfWeek == System.DayOfWeek.Sunday)
                {
                    LB.BorderColor = Color.DarkOrange;
                    LB.BorderStyle = BorderStyle.Solid;
                    LB.BorderWidth = 1;
                }
                else
                    LB.BorderWidth = 0;
                if (Dia.Day > 28)
                {
                    LB.ForeColor = Color.Black;
                }
                LB.Text = Texto;
                LB.ToolTip = Mensage;
            }
            DR.Close();

            ((OleDbConnection)ObjConexion).Dispose();
        }

        catch (System.Exception e)
        {
            CIsLog2.AgregaErrorMsg(e);
            string Mensage = e.Message;
        }
        try
        {
            for (int i = 1; i < 32; i++)
            {
                if (ObtenLB(i).Text.StartsWith("F"))
                    ObtenLB(i).ForeColor = Color.Red;
                if (ObtenLB(i).Text.StartsWith("R"))
                    ObtenLB(i).ForeColor = Color.DarkOrange;
                if (ObtenLB(i).Text.StartsWith("V"))
                    ObtenLB(i).ForeColor = Color.Lime;
                if (ObtenLB(i).Text.StartsWith("D"))
                    ObtenLB(i).ForeColor = Color.Green;
                if (ObtenLB(i).Text.StartsWith("A"))
                    ObtenLB(i).ForeColor = Color.Blue;
                if (ObtenLB(i).Text.StartsWith("_"))
                    ObtenLB(i).ForeColor = Color.Gray;

            }
        }
        catch (Exception ex)
        { }
        return false;



    }
    private void Page_Load(object sender, System.EventArgs e)
    {
        // Introducir aquí el código de usuario para inicializar la página
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
    ///		Método necesario para admitir el Diseñador. No se puede modificar
    ///		el contenido del método con el editor de código.
    /// </summary>
    private void InitializeComponent()
    {
        this.LB1.Click += new System.EventHandler(this.LB_Click);
        this.LB28.Click += new System.EventHandler(this.LB_Click);
        this.LB29.Click += new System.EventHandler(this.LB_Click);
        this.LB30.Click += new System.EventHandler(this.LB_Click);
        this.LB31.Click += new System.EventHandler(this.LB_Click);
        this.LB2.Click += new System.EventHandler(this.LB_Click);
        this.LB3.Click += new System.EventHandler(this.LB_Click);
        this.LB4.Click += new System.EventHandler(this.LB_Click);
        this.LB5.Click += new System.EventHandler(this.LB_Click);
        this.LB6.Click += new System.EventHandler(this.LB_Click);
        this.LB7.Click += new System.EventHandler(this.LB_Click);
        this.LB8.Click += new System.EventHandler(this.LB_Click);
        this.LB9.Click += new System.EventHandler(this.LB_Click);
        this.LB10.Click += new System.EventHandler(this.LB_Click);
        this.LB11.Click += new System.EventHandler(this.LB_Click);
        this.LB12.Click += new System.EventHandler(this.LB_Click);
        this.LB13.Click += new System.EventHandler(this.LB_Click);
        this.LB14.Click += new System.EventHandler(this.LB_Click);
        this.LB15.Click += new System.EventHandler(this.LB_Click);
        this.LB16.Click += new System.EventHandler(this.LB_Click);
        this.LB17.Click += new System.EventHandler(this.LB_Click);
        this.LB18.Click += new System.EventHandler(this.LB_Click);
        this.LB19.Click += new System.EventHandler(this.LB_Click);
        this.LB20.Click += new System.EventHandler(this.LB_Click);
        this.LB21.Click += new System.EventHandler(this.LB_Click);
        this.LB22.Click += new System.EventHandler(this.LB_Click);
        this.LB23.Click += new System.EventHandler(this.LB_Click);
        this.LB24.Click += new System.EventHandler(this.LB_Click);
        this.LB25.Click += new System.EventHandler(this.LB_Click);
        this.LB26.Click += new System.EventHandler(this.LB_Click);
        this.LB27.Click += new System.EventHandler(this.LB_Click);
        this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion
    //Aqui empieza mi codigo
    public int String_Conversor(string ctr_name)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Nuevo(this.Page);
            string[] Parms = LMes.ToolTip.Split(new char[] { '|' });
            try
            {
                Sesion.WF_Personas_Diario_Fecha = Convert.ToDateTime(Parms[1]).AddDays(Convert.ToInt32(ctr_name.Substring(2)) - 1);
            }
            catch { }
            try
            {
                Sesion.WF_Personas_Diario_Persona_ID = Convert.ToInt32(Parms[0]);
            }
            catch { }

            //				Sesion.WF_Personas_Diario_Fecha = Convert.ToDateTime(LMes.ToolTip).AddDays(Convert.ToInt32(ctr_name.Substring (2)) - 1);
            Sesion.Redirige("WF_Personas_DiarioE.aspx");
        }
        catch
        {

        }

        /*			int returnValue = 0;
                    string caracter = "";
                    string nombre = Convert.ToString(ctr_name);
                    caracter = nombre.Substring (2);
                    TextBox1.Text = caracter;
                    returnValue = Convert.ToInt32 (caracter);*/
        return 0;
    }



    private void LB_Click(object sender, System.EventArgs e)
    {

        String_Conversor(((System.Web.UI.WebControls.LinkButton)sender).ID);

    }


}
