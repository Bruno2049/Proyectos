using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WF_TurnosEdicion : Page
{
    CeC_Sesion Sesion;
    int tol = 1;
    int turnoID;
    string[,] horasDias;
    bool[] asist = new bool[7];
    DateTime[] entMin = new DateTime[7];
    DateTime[] ent = new DateTime[7];
    DateTime[] entMax = new DateTime[7];
    DateTime[] salCom = new DateTime[7];
    DateTime[] regCom = new DateTime[7];
    DateTime[] tieCom = new DateTime[7];
    DateTime[] salMin = new DateTime[7];
    DateTime[] sal = new DateTime[7];
    DateTime[] salMax = new DateTime[7];
    DateTime[] jornada = new DateTime[7];
    bool[] sigDia = new bool[7];
    // 
    CDias[] dia = new CDias[7];
    int ErrorDia = 0;
    string NombreErrorDia = "";
    int m_tipoTurno;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        try
        {

            lblCorrecto.Text = Sesion.MensajeCorrecto;
            lblError.Text = Sesion.MensajeError;
            Sesion.MensajeCorrecto = "";
            Sesion.MensajeError = "";

        }
        catch { }
        turnoID = Sesion.WF_Turnos_TURNO_ID;

        if (!IsPostBack)
        {
            if (Sesion.WF_Turnos_TURNO_ID == -1)
            {
                Sesion.TituloPagina = "Edición de Turnos";
                Sesion.DescripcionPagina = "Introduzca los datos necesarios para su nuevo turno. Una vez guardado el turno, no se podrán hacer modificaciones posteriores.";
                this.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>InicializarVariables();" +
                    "InicializarNuevo();</script>");
            }
            else
            {
                int[] valores = new int[5];

                valores = CargarTurno();
                this.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>InicializarVariables();" +
                    "InicializarEdicion(" + valores[0] + "," + valores[1] + "," + valores[2] +
                    "," + valores[3] + "," + valores[4] + ");</script>");
                Sesion.TituloPagina = "Consulta de Turnos";
                Sesion.DescripcionPagina = "Consulte los datos del turno seleccionado";
            }
        }

    }

    protected void btnRegresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Turnos.aspx");
    }

    protected void btnGuardar_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            lblCorrecto.Text = Sesion.MensajeCorrecto = "";
            lblError.Text = Sesion.MensajeError = "";

            if (txtNomTurno.Text.Equals(""))
            {
                Sesion.MensajeError = lblError.Text = "Defina el nombre del turno";
                return;
            }
            //        if (hdnGenAsist.Value.Equals("1"))
            {
                if (hdnTipoAsist.Value.Equals("0") && txtTol.Text.Equals(""))
                {
                    Sesion.MensajeError = lblError.Text = "Especifique la tolerancia";
                    return;
                }
                /*if (hdnTipoAsist.Value.Equals("1") && txtJornadaTrab.Text.Equals(""))
                {
                    Sesion.MensajeError = lblError.Text = "Especifique la jornada de trabajo";
                    return;
                }*/
                tol = Convert.ToInt32(txtTol.Value);
                horasDias = new string[7, 9];
                string[] horasDia = new string[9];
                bool error = false;
                if (!Haydatosnulos())
                {

                    asist[0] = Convert.ToBoolean(Convert.ToInt32(hdnDom.Value));
                    asist[1] = Convert.ToBoolean(Convert.ToInt32(hdnLun.Value));
                    asist[2] = Convert.ToBoolean(Convert.ToInt32(hdnMar.Value));
                    asist[3] = Convert.ToBoolean(Convert.ToInt32(hdnMie.Value));
                    asist[4] = Convert.ToBoolean(Convert.ToInt32(hdnJue.Value));
                    asist[5] = Convert.ToBoolean(Convert.ToInt32(hdnVie.Value));
                    asist[6] = Convert.ToBoolean(Convert.ToInt32(hdnSab.Value));


                    entMin[0] = (EntMinDom.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.Hora2DateTime(EntMinDom.Text);
                    entMin[1] = (EntMinLun.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.Hora2DateTime(EntMinLun.Text);
                    entMin[2] = (EntMinMar.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMinMar.Text).TimeOfDay);
                    entMin[3] = (EntMinMie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMinMie.Text).TimeOfDay);
                    entMin[4] = (EntMinJue.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMinJue.Text).TimeOfDay);
                    entMin[5] = (EntMinVie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMinVie.Text).TimeOfDay);
                    entMin[6] = (EntMinSab.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMinSab.Text).TimeOfDay);

                    ent[0] = (EntDom.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.Hora2DateTime(EntDom.Text);
                    ent[1] = (EntLun.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.Hora2DateTime(EntLun.Text);
                    ent[2] = (EntMar.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMar.Text).TimeOfDay);
                    ent[3] = (EntMie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMie.Text).TimeOfDay);
                    ent[4] = (EntJue.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntJue.Text).TimeOfDay);
                    ent[5] = (EntVie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntVie.Text).TimeOfDay);
                    ent[6] = (EntSab.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntSab.Text).TimeOfDay);

                    entMax[0] = (EntMaxDom.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMaxDom.Text).TimeOfDay);
                    entMax[1] = (EntMaxLun.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMaxLun.Text).TimeOfDay);
                    entMax[2] = (EntMaxMar.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMaxMar.Text).TimeOfDay);
                    entMax[3] = (EntMaxMie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMaxMie.Text).TimeOfDay);
                    entMax[4] = (EntMaxJue.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMaxJue.Text).TimeOfDay);
                    entMax[5] = (EntMaxVie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMaxVie.Text).TimeOfDay);
                    entMax[6] = (EntMaxSab.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(EntMaxSab.Text).TimeOfDay);

                    salCom[0] = (SalComDom.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalComDom.Text).TimeOfDay);
                    salCom[1] = (SalComLun.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalComLun.Text).TimeOfDay);
                    salCom[2] = (SalComMar.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalComMar.Text).TimeOfDay);
                    salCom[3] = (SalComMie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalComMie.Text).TimeOfDay);
                    salCom[4] = (SalComJue.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalComJue.Text).TimeOfDay);
                    salCom[5] = (SalComVie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalComVie.Text).TimeOfDay);
                    salCom[6] = (SalComSab.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalComSab.Text).TimeOfDay);

                    regCom[0] = (RegComDom.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(RegComDom.Text).TimeOfDay);
                    regCom[1] = (RegComLun.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(RegComLun.Text).TimeOfDay);
                    regCom[2] = (RegComMar.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(RegComMar.Text).TimeOfDay);
                    regCom[3] = (RegComMie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(RegComMie.Text).TimeOfDay);
                    regCom[4] = (RegComJue.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(RegComJue.Text).TimeOfDay);
                    regCom[5] = (RegComVie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(RegComVie.Text).TimeOfDay);
                    regCom[6] = (RegComSab.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(RegComSab.Text).TimeOfDay);

                    tieCom[0] = (TieComDom.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(TieComDom.Text).TimeOfDay);
                    tieCom[1] = (TieComLun.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(TieComLun.Text).TimeOfDay);
                    tieCom[2] = (TieComMar.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(TieComMar.Text).TimeOfDay);
                    tieCom[3] = (TieComMie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(TieComMie.Text).TimeOfDay);
                    tieCom[4] = (TieComJue.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(TieComJue.Text).TimeOfDay);
                    tieCom[5] = (TieComVie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(TieComVie.Text).TimeOfDay);
                    tieCom[6] = (TieComSab.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(TieComSab.Text).TimeOfDay);

                    salMin[0] = (SalMinDom.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMinDom.Text).TimeOfDay);
                    salMin[1] = (SalMinLun.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMinLun.Text).TimeOfDay);
                    salMin[2] = (SalMinMar.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMinMar.Text).TimeOfDay);
                    salMin[3] = (SalMinMie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMinMie.Text).TimeOfDay);
                    salMin[4] = (SalMinJue.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMinJue.Text).TimeOfDay);
                    salMin[5] = (SalMinVie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMinVie.Text).TimeOfDay);
                    salMin[6] = (SalMinSab.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMinSab.Text).TimeOfDay);

                    sal[0] = (SalDom.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalDom.Text).TimeOfDay);
                    sal[1] = (SalLun.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalLun.Text).TimeOfDay);
                    sal[2] = (SalMar.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMar.Text).TimeOfDay);
                    sal[3] = (SalMie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMie.Text).TimeOfDay);
                    sal[4] = (SalJue.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalJue.Text).TimeOfDay);
                    sal[5] = (SalVie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalVie.Text).TimeOfDay);
                    sal[6] = (SalSab.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalSab.Text).TimeOfDay);

                    salMax[0] = (SalMaxDom.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMaxDom.Text).TimeOfDay);
                    salMax[1] = (SalMaxLun.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMaxLun.Text).TimeOfDay);
                    salMax[2] = (SalMaxMar.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMaxMar.Text).TimeOfDay);
                    salMax[3] = (SalMaxMie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMaxMie.Text).TimeOfDay);
                    salMax[4] = (SalMaxJue.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMaxJue.Text).TimeOfDay);
                    salMax[5] = (SalMaxVie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMaxVie.Text).TimeOfDay);
                    salMax[6] = (SalMaxSab.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(SalMaxSab.Text).TimeOfDay);


                    jornada[0] = (JornadaDom.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(JornadaDom.Text).TimeOfDay);
                    jornada[1] = (JornadaLun.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(JornadaLun.Text).TimeOfDay);
                    jornada[2] = (JornadaMar.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(JornadaMar.Text).TimeOfDay);
                    jornada[3] = (JornadaMie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(JornadaMie.Text).TimeOfDay);
                    jornada[4] = (JornadaJue.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(JornadaJue.Text).TimeOfDay);
                    jornada[5] = (JornadaVie.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(JornadaVie.Text).TimeOfDay);
                    jornada[6] = (JornadaSab.Text.Equals("")) ? CeC_BD.FechaNula : CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(JornadaSab.Text).TimeOfDay);

                    sigDia[0] = chkSigDiaDom1.Checked;
                    sigDia[1] = chkSigDiaLun1.Checked;
                    sigDia[2] = chkSigDiaMar1.Checked;
                    sigDia[3] = chkSigDiaMie1.Checked;
                    sigDia[4] = chkSigDiaJue1.Checked;
                    sigDia[5] = chkSigDiaVie1.Checked;
                    sigDia[6] = chkSigDiaSab1.Checked;
                    error = ValidarHoras();
                    if (error)
                    {
                        Sesion.MensajeError = " No son correctos los parametros de la hora";
                    }
                }
                else
                {
                    error = true;

                }


                if (error)
                {
                    switch (ErrorDia)
                    {
                        case 0: NombreErrorDia = "Domingo"; break;
                        case 1: NombreErrorDia = "Lunes"; break;
                        case 2: NombreErrorDia = "Martes"; break;
                        case 3: NombreErrorDia = "Miercoles"; break;
                        case 4: NombreErrorDia = "Jueves"; break;
                        case 5: NombreErrorDia = "Viernes"; break;
                        case 6: NombreErrorDia = "Sabado"; break;
                    }
                    NombreErrorDia = Sesion.MensajeError + " en las casillas del dia " + NombreErrorDia;
                    Sesion.MensajeError = lblError.Text = "Verifique los datos insertados.\n" + NombreErrorDia;
                    //Sesion.WF_Turnos_TURNO_ID = turnoID;
                    Sesion.Redirige("WF_TurnosEdicion.aspx");
                    return;

                }

            }
            if (GuardarTurno())
            {
                Sesion.MensajeCorrecto = lblCorrecto.Text = "Los datos se han guardado correctamente";
                Sesion.WF_Turnos_TURNO_ID = turnoID;
                Sesion.Redirige("WF_TurnosEdicion.aspx");
            }
            else
            {
                Sesion.MensajeError = lblError.Text = "Los datos no se han podido guardar";
                // Sesion.WF_Turnos_TURNO_ID = turnoID;
                Sesion.Redirige("WF_TurnosEdicion.aspx");
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.btnGuardar_Click1", ex);
        }
    }

    /// <summary>
    /// Valida las horas para tener horarios coherentes
    /// </summary>
    /// <returns>True: si el horario tiene horas incoherentes
    /// False: si el horario esta bien definido</returns>
    public bool ValidarHoras()
    {
        try
        {
            int ini; int fin;
            if (hdnMismoHorario.Value == "1")
            {
                ini = 1; fin = 2;
            }
            else
            {
                ini = 0; fin = 7;
            }
            for (int i = ini; i < fin; i++)
            {
                if (asist[i])
                {
                    ErrorDia = i;
                    if (hdnTipoAsist.Value == "0")
                    {
                        // hdnRestAcceso.Value = "0";
                        if (hdnRestAcceso.Value == "0")
                        {
                            switch (hdnComida.Value)
                            {
                                case "0":
                                    if (sal[i] < ent[i].Add(new TimeSpan(0, tol, 0)))
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 7);
                                    break;
                                case "1":
                                    if (salCom[i] < ent[i].Add(new TimeSpan(0, tol, 0)) && salCom[i] != regCom[i])
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 4);
                                    if (regCom[i] < salCom[i])
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 5);
                                    if (sal[i] < regCom[i] && salCom[i] != regCom[i])
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 7);
                                    break;
                                case "2":
                                case "3":
                                    if (sal[i] < ent[i].Add(tieCom[i].TimeOfDay).AddMinutes(tol))
                                    {
                                        /*//Se agregan 24 horas porque se asume que es del día siguiente:
                                        Agregar24Hr(i, 7);
                                        //Si tiene habilitado día*/
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 7);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (hdnComida.Value)
                            {
                                case "0":
                                    if (ent[i].Add(new TimeSpan(0, tol, 0)) < entMin[i])
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 2);
                                    if (entMax[i] < ent[i].Add(new TimeSpan(0, tol, 0)))
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 3);
                                    if (sal[i] < entMax[i])
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 7);
                                    break;
                                case "1":
                                    if (ent[i].Add(new TimeSpan(0, tol, 0)) < entMin[i])
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 2);
                                    if (entMax[i] < ent[i].Add(new TimeSpan(0, tol, 0)))
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 3);
                                    if (salCom[i] < entMax[i] && salCom[i] != regCom[i])
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 4);
                                    if (regCom[i] < salCom[i])
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 5);
                                    if (sal[i] < regCom[i])
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 7);
                                    break;
                                case "2":
                                case "3":
                                    if (ent[i].Add(new TimeSpan(0, tol, 0)) < entMin[i])
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 2);
                                    if (entMax[i] < ent[i].Add(new TimeSpan(0, tol, 0)))
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 3);
                                    if (sal[i] < entMax[i].Add(tieCom[i].TimeOfDay))
                                        if (!sigDia[i])
                                            return true;
                                        else
                                            Agregar24Hr(i, 7);
                                    break;
                            }
                        }
                    }
                }
            }


            return false;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.ValidarHoras", ex);
            return false;
        }
    }
    /// <summary>
    /// Clase que contiene los horarios de entrada, de comida y de salida
    /// </summary>
    public class CDias
    {
        public bool Activo = false;
        public string hentradaMinima = "";
        public string hentrada = "";
        public string hentradaMaxima = "";
        public string hsalidaComida = "";
        public string hregresoComida = "";
        public string tiempoComida = "";
        public string salidaMinima = "";
        public string salida = "";
        public string salidaMaxima = "";

    }
    /// <summary>
    /// Inicializa los campos con los datos correspondientes
    /// </summary>
    public void InicializaDias()
    {
        try
        {
            // Si tiene el mismo horario para todos los dias se crea un objeto con los horarios para entrada, comida y salida
            if (Convert.ToBoolean(Convert.ToInt32(hdnMismoHorario.Value)))
                dia = new CDias[1];
            for (int x = 0; x < dia.Length; x++)
                dia[x] = new CDias();
            //  Domingo
            dia[0].Activo = Convert.ToBoolean(Convert.ToInt32(hdnDom.Value));
            dia[0].hentradaMinima = EntMinDom.Text;
            dia[0].hentradaMaxima = EntMaxDom.Text;
            dia[0].hentrada = EntDom.Text;
            dia[0].hsalidaComida = SalComDom.Text;
            dia[0].hregresoComida = RegComDom.Text;
            dia[0].salida = SalDom.Text;
            dia[0].tiempoComida = TieComDom.Text;
            sigDia[0] = chkSigDiaDom1.Checked;
            if (dia.Length == 1)
                return;
            //  Lunes
            dia[1].Activo = Convert.ToBoolean(Convert.ToInt32(hdnLun.Value));
            dia[1].hentradaMinima = EntMinLun.Text;
            dia[1].hentradaMaxima = EntMaxLun.Text;
            dia[1].hentrada = EntLun.Text;
            dia[1].hsalidaComida = SalComLun.Text;
            dia[1].hregresoComida = RegComLun.Text;
            dia[1].salida = SalLun.Text;
            dia[1].tiempoComida = TieComLun.Text;
            sigDia[1] = chkSigDiaLun1.Checked;

            //Martes
            dia[2].Activo = Convert.ToBoolean(Convert.ToInt32(hdnMar.Value));
            dia[2].hentradaMinima = EntMinMar.Text;
            dia[2].hentradaMaxima = EntMaxMar.Text;
            dia[2].hentrada = EntMar.Text;
            dia[2].hsalidaComida = SalComMar.Text;
            dia[2].hregresoComida = RegComMar.Text;
            dia[2].salida = SalMar.Text;
            dia[2].tiempoComida = TieComMar.Text;
            sigDia[2] = chkSigDiaMar1.Checked;

            //Miercoles
            dia[3].Activo = Convert.ToBoolean(Convert.ToInt32(hdnMie.Value));
            dia[3].hentradaMinima = EntMinMie.Text;
            dia[3].hentradaMaxima = EntMaxMie.Text;
            dia[3].hentrada = EntMie.Text;
            dia[3].hsalidaComida = SalComMie.Text;
            dia[3].hregresoComida = RegComMie.Text;
            dia[3].salida = SalMie.Text;
            dia[3].tiempoComida = TieComMie.Text;
            sigDia[3] = chkSigDiaMie1.Checked;

            //Jueves
            dia[4].Activo = Convert.ToBoolean(Convert.ToInt32(hdnJue.Value));
            dia[4].hentradaMinima = EntMinJue.Text;
            dia[4].hentradaMaxima = EntMaxJue.Text;
            dia[4].hentrada = EntJue.Text;
            dia[4].hsalidaComida = SalComJue.Text;
            dia[4].hregresoComida = RegComJue.Text;
            dia[4].salida = SalJue.Text;
            dia[4].tiempoComida = TieComJue.Text;
            sigDia[4] = chkSigDiaJue1.Checked;

            //Viernes
            dia[5].Activo = Convert.ToBoolean(Convert.ToInt32(hdnVie.Value));
            dia[5].hentradaMinima = EntMinVie.Text;
            dia[5].hentradaMaxima = EntMaxVie.Text;
            dia[5].hentrada = EntVie.Text;
            dia[5].hsalidaComida = SalComVie.Text;
            dia[5].hregresoComida = RegComVie.Text;
            dia[5].salida = SalVie.Text;
            dia[5].tiempoComida = TieComVie.Text;
            sigDia[5] = chkSigDiaVie1.Checked;

            //Sabado
            dia[6].Activo = Convert.ToBoolean(Convert.ToInt32(hdnSab.Value));
            dia[6].hentradaMinima = EntMinSab.Text;
            dia[6].hentradaMaxima = EntMaxSab.Text;
            dia[6].hentrada = EntSab.Text;
            dia[6].hsalidaComida = SalComSab.Text;
            dia[6].hregresoComida = RegComSab.Text;
            dia[6].salida = SalSab.Text;
            dia[6].tiempoComida = TieComSab.Text;
            sigDia[6] = chkSigDiaSab1.Checked;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.InicializaDias", ex);
        }
    }
    /// <summary>
    /// Verifica si hay datos que no se han introducido
    /// </summary>
    /// <returns>Verdadero si hay datos no introducidos. Falso en otro caso</returns>
    public bool Haydatosnulos()
    {
        try
        {
            InicializaDias();

            if (Convert.ToBoolean(Convert.ToInt32(hdnGenAsist.Value)))
            {
                for (int i = 0; i < dia.Length; i++)
                {
                    if (dia[i].Activo)
                    {
                        ErrorDia = i;

                        switch (Convert.ToInt32(hdnTipoAsist.Value))
                        {
                            case 0:
                                if (dia[i].hentrada == " ")
                                    return true;
                                if (dia[i].salida == " " || dia[i].salida == "00:00")
                                    if (Convert.ToDateTime(dia[i].salida) < Convert.ToDateTime(dia[i].hentrada) && !sigDia[i])
                                    {
                                        Sesion.MensajeError = "La salida es menor a la hora de entrada. Si es correcto,seleccione la opcion 'Siguiente Dia'"; return true;
                                    }
                                if (Convert.ToBoolean(Convert.ToInt32(hdnRestAcceso.Value)))
                                {
                                    if (dia[i].hentradaMaxima == " " || dia[i].hentradaMaxima == "00:00")
                                        if (Convert.ToDateTime(dia[i].hentradaMaxima) < Convert.ToDateTime(dia[i].hentrada) && !sigDia[i])
                                        {
                                            Sesion.MensajeError = "La entrada maxima es menor a la hora de entrada. Si es correcto,seleccione la opcion 'Siguiente Dia'"; return true;
                                        }
                                    if (dia[i].hentradaMinima == " " || dia[i].hentradaMinima == "00:00")
                                        if (Convert.ToDateTime(dia[i].hentradaMinima) > Convert.ToDateTime(dia[i].hentrada) && !sigDia[i])
                                        {
                                            Sesion.MensajeError = "La entrada minima es mayor a la hora de entrada. Si es correcto,seleccione la opcion 'Siguiente Dia'"; return true;
                                        }
                                }
                                switch (Convert.ToInt32(hdnComida.Value))
                                {
                                    case 1:
                                        if (dia[i].hregresoComida != dia[i].hsalidaComida)
                                            if (Convert.ToDateTime(dia[i].hsalidaComida) > Convert.ToDateTime(dia[i].hregresoComida) && !sigDia[i])
                                            {
                                                Sesion.MensajeError = "La salida a comer es menor a la hora de regreso. Si es correcto,seleccione la opcion 'Siguiente Dia'"; return true;
                                            }
                                        break;
                                    case 2:
                                        if (dia[i].tiempoComida == " ")
                                            dia[i].tiempoComida = "00:00";
                                        break;
                                }
                                break;

                            case 1:
                                if (dia[i].hentradaMaxima == " " || dia[i].hentradaMaxima == "00:00")
                                    if (Convert.ToDateTime(dia[i].hentradaMaxima) < Convert.ToDateTime(dia[i].hentrada) && !sigDia[i])
                                    {
                                        Sesion.MensajeError = "La entrada maxima es menor a la hora de entrada. Si es correcto,seleccione la opcion 'Siguiente Dia'"; return true;
                                    }
                                if (dia[i].hentradaMinima == " " || dia[i].hentradaMinima == "00:00")
                                    if (Convert.ToDateTime(dia[i].hentradaMinima) > Convert.ToDateTime(dia[i].hentrada) && !sigDia[i])
                                    {
                                        Sesion.MensajeError = "La entrada minima es mayor a la hora de entrada. Si es correcto,seleccione la opcion 'Siguiente Dia'"; return true;
                                    }
                                if (Convert.ToInt32(hdnComida.Value) == 2)
                                    if (dia[i].tiempoComida == " ")
                                        dia[i].tiempoComida = "00:00";
                                break;

                        }
                    }
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.Haydatosnulos", ex);
            return false;
        }
    }

    /// <summary>
    /// Obtiene la hora de entrada minima de un día, se usa para turnos semanales, 
    /// donde las horas de trabajo pueden ser diferentes dependiendo el día.
    /// </summary>
    /// <param name="Dia">Dia, si este es -1 tomará el día 6 , si es 7 se usará el 0</param>
    /// <returns></returns>
    public DateTime ObtenEntradaMinima(int Dia)
    {
        try
        {
            if (Dia == -1)
                Dia = 6;
            if (Dia == 7)
                Dia = 0;
            return ent[Dia].AddHours(-3);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.ObtenEntradaMinima", ex);
            return CeC_BD.FechaNula;
        }
    }

    public DateTime ObtenSalidaMaxima(int Dia)
    {
        try
        {
            int DiaSiguiente = Dia + 1;
            if (DiaSiguiente == 7)
                DiaSiguiente = 0;
            if (asist[DiaSiguiente])
            {
                return ObtenEntradaMinima(DiaSiguiente);
            }
            return ObtenEntradaMinima(Dia).AddDays(1);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.ObtenSalidaMaxima", ex);
            return CeC_BD.FechaNula;
        }
    }
    /// <summary>
    /// Guarda los datos del turno en la base de datos
    /// </summary>
    /// <returns>True si se guardaron los datos correctamente
    /// False si hubo problemas y/o errores al guardar los datos</returns>
    public bool GuardarTurno()
    {
        try
        {
            int ini; int fin;
            DateTime ret = new DateTime();
            DateTime tolCom = CeC_BD.FechaNula.AddMinutes(txtTolComida.ValueInt);

            int turnoDiaID;
            ArrayList dias = new ArrayList();
            int tiempoEx = (chkTiempoEx.Checked) ? 1 : 0;
            bool MismoHorario = false;

            int NoAsistencia = 0;
            if (hdnGenAsist.Value.Equals("0"))
                NoAsistencia = 1;

            int TipoComida = Convert.ToInt32(hdnComida.Value);
            // Si es mismo horario para cada día
            if (hdnMismoHorario.Value.Equals("1"))
            {
                MismoHorario = true;
                ini = 1; fin = 2;
            }
            else
            {
                ini = 0; fin = 7;
            }
            /*  if (hdnGenAsist.Value.Equals("1"))
              {*/
            if (hdnTipoAsist.Value.Equals("0"))
                //
                if (hdnRestAcceso.Value.Equals("0") && TipoComida == 0 && hdnMismoHorario.Value.Equals("1"))
                {
                    m_tipoTurno = 5;
                }
                else
                {
                    m_tipoTurno = 1;
                }
            //m_tipoTurno = (hdnRestAcceso.Value.Equals("0") && TipoComida == 0 && hdnMismoHorario.Value.Equals("1")) ? 5 : 1;
            else
                m_tipoTurno = 2;
            // Si el Turno ID es -1, se creara un nuevo Turno. En otro caso, se actualiza el Turno
            if (Sesion.WF_Turnos_TURNO_ID == -1)
            {
                turnoID = CeC_Turnos.AgregaTurno(m_tipoTurno, txtNomTurno.Text, Convert.ToInt16(chkTiempoEx.Checked), 1, 0, 0, Sesion);
                if (turnoID < 0)
                    return false;
            }
            else
            {
                if (CeC_Turnos.ActualizaTurno(turnoID, m_tipoTurno, txtNomTurno.Text, Convert.ToInt16(chkTiempoEx.Checked), 1, Sesion.SESION_ID) < 1)
                    return false;
            }
            for (int i = ini; i < fin; i++)
            {

                try
                {
                    switch (m_tipoTurno)
                    {
                        case 1:
                            if (asist[i])
                            {
                                if (hdnRestAcceso.Value.Equals("0"))
                                {
                                    entMin[i] = ObtenEntradaMinima(i);
                                    entMax[i] = sal[i].AddHours(-1);
                                    salMin[i] = sal[i].AddHours(-1);
                                    salMax[i] = ObtenSalidaMaxima(i);
                                }
                                ret = ent[i].Add(CeC_BD.DateTime2TimeSpan(CeC_BD.FechaNula.AddMinutes(tol)));
                                if (TipoComida == 1)
                                    tieCom[i] = regCom[i].Subtract(salCom[i].TimeOfDay);
                                if (TipoComida == 2)
                                {
                                    regCom[i] = CeC_BD.FechaNula;
                                    salCom[i] = CeC_BD.FechaNula;
                                }
                                //tolCom = tieCom[i].AddMinutes(1);

                                DateTime[] horasturno = new DateTime[11];
                                horasturno[0] = entMin[i];
                                horasturno[1] = ent[i];
                                if (ent[i] > entMax[i])
                                    entMax[i] = entMax[i].AddDays(1);
                                horasturno[2] = entMax[i];
                                horasturno[3] = ret;
                                horasturno[4] = salMin[i];
                                horasturno[5] = sal[i];

                                if (sal[i] > salMax[i])
                                    salMax[i] = salMax[i].AddDays(1);
                                horasturno[6] = salMax[i];
                                horasturno[7] = salCom[i];
                                horasturno[8] = regCom[i];
                                horasturno[9] = tieCom[i];
                                horasturno[10] = tolCom;
                                turnoDiaID = CeC_Turnos.AgregaTurnoDia(horasturno, tiempoEx, m_tipoTurno, NoAsistencia, 1, Sesion);
                                GeneraTurnoSemana(turnoDiaID, turnoID, i);
                            }
                            break;
                        case 2:
                            if (asist[i])
                            {
                                //ent[i] = entMin[i];

                                sal[i] = ent[i].Add(jornada[i].TimeOfDay);
                                /*entMin[i] = new DateTime(entMax[i].AddTicks(sal[i].Ticks).Ticks / 2);
                                entMin[i] = entMin[i].AddHours(-12);*/
                                ret = entMax[i];
                                //tolCom = tieCom[i].AddMinutes(1);

                                salMin[i] = sal[i];
                                salMax[i] = entMin[i].AddDays(1);

                                DateTime bloque = CeC_BD.TimeSpan2DateTime(new TimeSpan(0, txtBloque.ValueInt, 0));
                                DateTime tolBloq = CeC_BD.TimeSpan2DateTime(new TimeSpan(0, txtTol.ValueInt, 0));

                                DateTime[] horasturno = new DateTime[12];
                                horasturno[0] = entMin[i];
                                horasturno[1] = ent[i];
                                horasturno[2] = entMax[i];
                                horasturno[3] = ret;
                                horasturno[4] = salMin[i];
                                horasturno[5] = sal[i];
                                horasturno[6] = salMax[i];
                                horasturno[7] = tieCom[i];
                                horasturno[8] = tolCom;
                                horasturno[9] = bloque;
                                horasturno[10] = tolBloq;
                                horasturno[11] = jornada[i];
                                turnoDiaID = CeC_Turnos.AgregaTurnoDia(horasturno, tiempoEx, m_tipoTurno, NoAsistencia, 1, Sesion);
                                GeneraTurnoSemana(turnoDiaID, turnoID, i);

                            }
                            break;
                        case 5:
                            if (asist[i])
                            {
                                entMin[i] = new DateTime(ent[i].AddTicks(sal[i].Ticks).Ticks / 2);
                                entMin[i] = entMin[i].AddHours(-12);
                                ret = ent[i].Add(CeC_BD.DateTime2TimeSpan(CeC_BD.FechaNula.AddMinutes(tol)));
                                salMin[i] = entMax[i] = sal[i].AddHours(-1);
                                tieCom[i] = tieCom[i].AddHours(1);
                                //tolCom = tieCom[i].AddMinutes(1);
                                salMax[i] = new DateTime(ent[i].AddTicks(sal[i].Ticks).Ticks / 2);
                                salMax[i] = salMax[i].AddHours(12);
                                DateTime[] horasturno = new DateTime[9];
                                horasturno[0] = entMin[i];
                                horasturno[1] = ent[i];
                                horasturno[2] = entMax[i];
                                horasturno[3] = ret;
                                horasturno[4] = salMin[i];
                                horasturno[5] = sal[i];
                                horasturno[6] = salMax[i];
                                horasturno[7] = tieCom[i];
                                horasturno[8] = tolCom;
                                turnoDiaID = CeC_Turnos.AgregaTurnoDia(horasturno, tiempoEx, m_tipoTurno, NoAsistencia, 1, Sesion);
                                GeneraTurnoSemana(turnoDiaID, turnoID, i);
                            }
                            break;
                    }
                    //return true;
                }
                catch
                {
                    return false;
                }
            }
            /*}
            else
            {
                if (Sesion.WF_Turnos_TURNO_ID == -1)
                {
                    turnoID = CeC_Turnos.AgregaTurno(4, txtNomTurno.Text, 0, 0, 0,0,Sesion);
                }
                else
                {
                    turnoID = Sesion.WF_Turnos_TURNO_ID;
                    CeC_Turnos.ActualizaTurno(turnoID, 4, txtNomTurno.Text, 0, 0,Sesion.SESION_ID);
                }
          
                DateTime[] horasdia = new DateTime [12];
                for (int x = 0; x <= horasdia.Length - 1; x++)
                       horasdia[x] = CeC_BD.FechaNula;
                turnoDiaID = CeC_Turnos.AgregaTurnoDia(horasdia, tiempoEx, 5, NoAsistencia, 0, Sesion);        
                GeneraTurnoSemana(turnoDiaID, turnoID, 0);
            }
            */
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.GuardarTurno", ex);
            return false;
        }
    }

    /// <summary>
    /// Agrega 24 horas a la hora cuando el turno termina al dia siguiente
    /// </summary>
    /// <param name="dia">Indice del día</param>
    /// <param name="j">Indice de la hora a agregar las 24 horas</param>
    public void Agregar24Hr(int dia, int j)
    {
        try
        {
            entMin[dia] = (j <= 1) ? entMin[dia].AddDays(1) : entMin[dia];
            ent[dia] = (j <= 2) ? ent[dia].AddDays(1) : ent[dia];
            entMax[dia] = (j <= 3) ? entMax[dia].AddDays(1) : entMax[dia];
            salCom[dia] = (j <= 4) ? salCom[dia].AddDays(1) : salCom[dia];
            regCom[dia] = (j <= 5) ? regCom[dia].AddDays(1) : regCom[dia];
            sal[dia] = (j <= 7) ? sal[dia].AddDays(1) : sal[dia];
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.Agregar24Hr", ex);
        }
    }

    /// <summary>
    /// Guarda en la base de datos el horario correspondiente al día de la semana
    /// </summary>
    /// <param name="turnoDiaID">Identificador del horario del día</param>
    /// <param name="turnoID">Identificador del turno</param>
    /// <param name="dia">Día de la semana a aplicar el horario</param>
    public void GeneraTurnoSemana(int turnoDiaID, int turnoID, int dia)
    {
        try
        {
            ArrayList dias = new ArrayList();
            int turnoSemID;
            //if (CeC_BD.EjecutaDataSet("SELECT * FROM EC_TURNOS_SEMANAL_DIA WHERE TURNO_ID = " + turnoID ) != null)
            // CeC_BD.EjecutaComando("UPDATE EC_TURNOS_SEMANAL_DIA SET TURNO_ID = 0 WHERE TURNO_ID = " + turnoID);
            // if (hdnGenAsist.Value.Equals("1"))
            {
                if (hdnMismoHorario.Value.Equals("1"))
                {
                    if (chkAsistGralDom.Checked)
                        dias.Add(1);
                    if (chkAsistGralLun.Checked)
                        dias.Add(2);
                    if (chkAsistGralMar.Checked)
                        dias.Add(3);
                    if (chkAsistGralMie.Checked)
                        dias.Add(4);
                    if (chkAsistGralJue.Checked)
                        dias.Add(5);
                    if (chkAsistGralVie.Checked)
                        dias.Add(6);
                    if (chkAsistGralSab.Checked)
                        dias.Add(7);
                    for (int j = 0; j < dias.Count; j++)
                    {
                        turnoSemID = CeC_Turnos.AgregaTurnoSemanalDia(turnoDiaID, turnoID, Convert.ToInt32(dias[j]), Sesion);
                    }
                }
                else
                {
                    turnoSemID = CeC_Turnos.AgregaTurnoSemanalDia(turnoDiaID, turnoID, (dia + 1), Sesion);
                }
            }
            /* else
             {
                 for (int j = 1; j < 8; j++)
                 {
                     turnoSemID = CeC_Turnos.AgregaTurnoSemanalDia(turnoDiaID, turnoID, j, Sesion);
                 }
             }*/
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.GeneraTurnoSemana", ex);
        }
    }

    /// <summary>
    /// Obtiene el tipo de comida
    /// 
    /// </summary>
    /// <param name="Fila"></param>
    /// <returns>
    /// 0. Sin comida
    /// 1. Horario fijo
    /// 2. por Tiempo
    /// 3. Avanzado
    /// </returns>
    int ObtenTipoComida(DS_TurnosEdicion.TURNODIASRow Fila)
    {
        try
        {
            DateTime TURNO_DIA_HCS = Fila["TURNO_DIA_HCS"] != DBNull.Value ? Convert.ToDateTime(Fila["TURNO_DIA_HCS"]) : CeC_BD.FechaNula;
            DateTime TURNO_DIA_HCR = Fila["TURNO_DIA_HCR"] != DBNull.Value ? Convert.ToDateTime(Fila["TURNO_DIA_HCR"]) : CeC_BD.FechaNula;
            DateTime TURNO_DIA_HCTIEMPO = Fila["TURNO_DIA_HCTIEMPO"] != DBNull.Value ? Convert.ToDateTime(Fila["TURNO_DIA_HCTIEMPO"]) : CeC_BD.FechaNula;
            DateTime TURNO_DIA_HCTOLERA = Fila["TURNO_DIA_HCTOLERA"] != DBNull.Value ? Convert.ToDateTime(Fila["TURNO_DIA_HCTOLERA"]) : CeC_BD.FechaNula;
            //Valida si se capturo el tiempo de comida
            if (TURNO_DIA_HCTIEMPO == CeC_BD.FechaNula)
            {
                //Valida si se capturo la hora de salida a comer y el regreso
                if (TURNO_DIA_HCS == CeC_BD.FechaNula || TURNO_DIA_HCR == CeC_BD.FechaNula)
                    return 0;
                else
                    return 1;
            }
            else
            {
                //Valida si se capturo la hora de salida a comer y el regreso
                if (TURNO_DIA_HCS == CeC_BD.FechaNula || TURNO_DIA_HCR == CeC_BD.FechaNula)
                    return 2;
                else
                    return 3;
            }

        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.ObtenTipoComida", ex);
            return 0;
        }
        //return 0;
    }

    /// <summary>
    /// Carga los datos del turno en los controles correspondientes
    /// </summary>
    /// <returns>Arreglo con las características del turno</returns>
    public int[] CargarTurno()
    {
        DS_TurnosEdicion ds = new DS_TurnosEdicion();
        DS_TurnosEdicionTableAdapters.TURNOSTableAdapter taTurno = new DS_TurnosEdicionTableAdapters.TURNOSTableAdapter();
        DS_TurnosEdicionTableAdapters.TURNODIASTableAdapter taTurnoDias = new DS_TurnosEdicionTableAdapters.TURNODIASTableAdapter();
        DS_TurnosEdicion.TurnosEdicionRow fila = ds.TurnosEdicion.NewTurnosEdicionRow();
        taTurno.Turno(ds.TURNOS, Sesion.WF_Turnos_TURNO_ID);
        int NoDiasAsignados = taTurnoDias.TurnoDias(ds.TURNODIAS, Sesion.WF_Turnos_TURNO_ID);
        //Arreglo que será devuelto por la función.
        //valores[0]: Generación de Asistencia.
        //valores[1]: Tipo de Asistencia.
        //valores[2]: Mismo Horario para todos los días
        //valores[3]: Acceso Restringido
        //valores[4]: Comida
        int[] valores = new int[5];
        bool mismaHora;
        TimeSpan tolerancia = new TimeSpan();
        ArrayList diasTurno = new ArrayList();
        try
        {
            int tipoTurno = Convert.ToInt32(ds.TURNOS.Rows[0]["TIPO_TURNO_ID"]);
            txtNomTurno.Text = ds.TURNOS.Rows[0]["TURNO_NOMBRE"].ToString();
            chkTiempoEx.Checked = ds.TURNOS.Rows[0]["TURNO_PHEXTRAS"].ToString().Equals("1");
            hdnDom.Value = hdnLun.Value = hdnMar.Value = hdnMie.Value = hdnJue.Value = hdnVie.Value = hdnSab.Value = "0";

            if (NoDiasAsignados <= 0)
                return valores;

            //        ObtenTipoComida(ds.TURNODIAS[0]);
            int TipoComidaTemp = 0;
            valores[0] = 1;
            foreach (DS_TurnosEdicion.TURNODIASRow Fila in ds.TURNODIAS)
            {
                if (Fila.TURNO_DIA_NO_ASIS > 0)
                {
                    valores[0] = 0;
                }
                if ((TipoComidaTemp = ObtenTipoComida(Fila)) > 0)
                    break;


            }
            int TipoComida = valores[4] = TipoComidaTemp;

            /*  if (ds.TURNODIAS[0].DIA_SEMANA_ID == 1 && ds.TURNODIAS.Count > 1)
                  TipoComida = valores[4] = ObtenTipoComida(ds.TURNODIAS[1]);*/
            txtTolComida.Value = ds.TURNODIAS[0].TURNO_DIA_HCTOLERA.Subtract(CeC_BD.FechaNula).TotalMinutes;
            // 0,'No Asignado'  
            // 1,'Semanal'  
            // 2,'Flexible'  
            // 3,'Diario' 
            // 4,'Abierto' 
            // 5,'Simple'
            switch (tipoTurno)
            {
                case 1://Turno Semanal

                    valores[1] = 0;
                    if (!ds.TURNODIAS.Rows[0]["TURNO_DIA_HEMIN"].Equals(Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HE"]).Subtract(new TimeSpan(3, 0, 0))) ||
                            !ds.TURNODIAS.Rows[0]["TURNO_DIA_HEMAX"].Equals(Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HS"]).Subtract(new TimeSpan(1, 0, 0))))
                        valores[3] = 1;
                    else
                        valores[3] = 0;

                    mismaHora = (ds.TURNODIAS.Rows.Count != 1) ? ds.TURNODIAS.Rows[0]["TURNO_DIA_ID"].Equals(ds.TURNODIAS.Rows[1]["TURNO_DIA_ID"]) : true;
                    tolerancia = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HERETARDO"]).TimeOfDay.Subtract(Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HE"]).TimeOfDay);
                    txtTol.Text = ((tolerancia.Hours * 60) + tolerancia.Minutes).ToString();
                    if (mismaHora)
                    {
                        try
                        {
                            valores[2] = 1;
                            EntLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                            //EntLun.Value = null;
                            SalLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                            chkSigDiaLun1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HS"]).Day == 2);

                            if (valores[3] == 1)
                            {
                                EntMinLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                EntMaxLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                SalMinLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HSMIN"]).TimeOfDay.ToString();
                                SalMaxLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HSMAX"]).TimeOfDay.ToString();
                            }
                            if (TipoComida == 1 || TipoComida == 3)
                            {
                                SalComLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HCS"]).TimeOfDay.ToString();
                                RegComLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HCR"]).TimeOfDay.ToString();
                            }
                            if (TipoComida == 2 || TipoComida == 3)
                                TieComLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                            for (int i = 0; i < ds.TURNODIAS.Rows.Count; i++)
                                diasTurno.Add(Convert.ToInt32(ds.TURNODIAS.Rows[i]["DIA_SEMANA_ID"]));
                            ActivaChkGral(diasTurno);
                        }
                        catch { }
                    }
                    else
                    {
                        valores[2] = 0;
                        for (int i = 0; i < ds.TURNODIAS.Rows.Count; i++)
                        {
                            switch (Convert.ToInt32(ds.TURNODIAS.Rows[i]["DIA_SEMANA_ID"]))
                            {
                                case 1:
                                    hdnDom.Value = "1";
                                    EntDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaDom1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (valores[3] == 1)
                                    {
                                        EntMinDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                        EntMaxDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                        SalMinDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMIN"]).TimeOfDay.ToString();
                                        SalMaxDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMAX"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 1 || TipoComida == 3)
                                    {
                                        SalComDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCS"]).TimeOfDay.ToString();
                                        RegComDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCR"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 2 || TipoComida == 3)
                                        TieComDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 2:
                                    hdnLun.Value = "1";
                                    EntLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaLun1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (valores[3] == 1)
                                    {
                                        EntMinLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                        EntMaxLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                        SalMinLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMIN"]).TimeOfDay.ToString();
                                        SalMaxLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMAX"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 1 || TipoComida == 3)
                                    {
                                        SalComLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCS"]).TimeOfDay.ToString();
                                        RegComLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCR"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 2 || TipoComida == 3)
                                        TieComLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 3:
                                    hdnMar.Value = "1";
                                    EntMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaMar1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (valores[3] == 1)
                                    {
                                        EntMinMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                        EntMaxMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                        SalMinMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMIN"]).TimeOfDay.ToString();
                                        SalMaxMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMAX"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 1 || TipoComida == 3)
                                    {
                                        SalComMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCS"]).TimeOfDay.ToString();
                                        RegComMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCR"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 2 || TipoComida == 3)
                                        TieComMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 4:
                                    hdnMie.Value = "1";
                                    EntMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaMie1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (valores[3] == 1)
                                    {
                                        EntMinMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                        EntMaxMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                        SalMinMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMIN"]).TimeOfDay.ToString();
                                        SalMaxMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMAX"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 1 || TipoComida == 3)
                                    {
                                        SalComMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCS"]).TimeOfDay.ToString();
                                        RegComMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCR"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 2 || TipoComida == 3)
                                        TieComMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 5:
                                    hdnJue.Value = "1";
                                    EntJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaJue1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (valores[3] == 1)
                                    {
                                        EntMinJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                        EntMaxJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                        SalMinJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMIN"]).TimeOfDay.ToString();
                                        SalMaxJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMAX"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 1 || TipoComida == 3)
                                    {
                                        SalComJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCS"]).TimeOfDay.ToString();
                                        RegComJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCR"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 2 || TipoComida == 3)
                                        TieComJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 6:
                                    hdnVie.Value = "1";
                                    EntVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaVie1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (valores[3] == 1)
                                    {
                                        EntMinVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                        EntMaxVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                        SalMinVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMIN"]).TimeOfDay.ToString();
                                        SalMaxVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMAX"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 1 || TipoComida == 3)
                                    {
                                        SalComVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCS"]).TimeOfDay.ToString();
                                        RegComVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCR"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 2 || TipoComida == 3)
                                        TieComVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 7:
                                    hdnSab.Value = "1";
                                    EntSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaSab1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (valores[3] == 1)
                                    {
                                        EntMinSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                        EntMaxSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                        SalMinSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMIN"]).TimeOfDay.ToString();
                                        SalMaxSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HSMAX"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 1 || TipoComida == 3)
                                    {
                                        SalComSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCS"]).TimeOfDay.ToString();
                                        RegComSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCR"]).TimeOfDay.ToString();
                                    }
                                    if (TipoComida == 2 || TipoComida == 3)
                                        TieComSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                            }
                        }
                    }
                    return valores;
                case 2://Turno Flexible
                    valores[1] = 1;
                    valores[3] = 0;
                    mismaHora = (ds.TURNODIAS.Rows.Count != 1) ? ds.TURNODIAS.Rows[0]["TURNO_DIA_ID"].Equals(ds.TURNODIAS.Rows[1]["TURNO_DIA_ID"]) : true;
                    if (mismaHora)
                    {
                        EntMinLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                        EntLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                        EntMaxLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                        chkSigDiaLun1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HEMAX"]).Day == 2);


                        JornadaLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HTIEMPO"]).TimeOfDay.ToString();

                        txtBloque.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HBLOQUE"]).TimeOfDay.TotalMinutes.ToString();
                        txtTol.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HBLOQUET"]).TimeOfDay.TotalMinutes.ToString();

                        for (int i = 0; i < ds.TURNODIAS.Rows.Count; i++)
                            diasTurno.Add(Convert.ToInt32(ds.TURNODIAS.Rows[i]["DIA_SEMANA_ID"]));
                        ActivaChkGral(diasTurno);
                        valores[2] = 1;

                        if (TipoComida == 2 || TipoComida == 3)
                        {
                            TieComLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                        }
                    }
                    else
                    {
                        valores[2] = 0;

                        txtBloque.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HBLOQUE"]).TimeOfDay.TotalMinutes.ToString();
                        txtTol.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HBLOQUET"]).TimeOfDay.TotalMinutes.ToString();

                        for (int i = 0; i < ds.TURNODIAS.Rows.Count; i++)
                        {
                            switch (Convert.ToInt32(ds.TURNODIAS.Rows[i]["DIA_SEMANA_ID"]))
                            {
                                case 1:
                                    hdnDom.Value = "1";
                                    EntMinDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                    EntDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    EntMaxDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                    JornadaDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HTIEMPO"]).TimeOfDay.ToString();
                                    chkSigDiaDom1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (!ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"].Equals(CeC_BD.FechaNula))
                                        TieComDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 2:
                                    hdnLun.Value = "1";
                                    EntMinLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                    EntLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    EntMaxLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                    JornadaLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HTIEMPO"]).TimeOfDay.ToString();
                                    chkSigDiaLun1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (!ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"].Equals(CeC_BD.FechaNula))
                                        TieComLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 3:
                                    hdnMar.Value = "1";
                                    EntMinMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                    EntMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    EntMaxMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                    JornadaMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HTIEMPO"]).TimeOfDay.ToString();
                                    chkSigDiaMar1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (!ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"].Equals(CeC_BD.FechaNula))
                                        TieComMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 4:
                                    hdnMie.Value = "1";
                                    EntMinMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                    EntMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    EntMaxMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                    JornadaMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HTIEMPO"]).TimeOfDay.ToString();
                                    chkSigDiaMie1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (!ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"].Equals(CeC_BD.FechaNula))
                                        TieComMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 5:
                                    hdnJue.Value = "1";
                                    EntMinJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                    EntJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    EntMaxJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                    JornadaJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HTIEMPO"]).TimeOfDay.ToString();
                                    chkSigDiaJue1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (!ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"].Equals(CeC_BD.FechaNula))
                                        TieComJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 6:
                                    hdnVie.Value = "1";
                                    EntMinVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                    EntVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    EntMaxVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                    JornadaVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HTIEMPO"]).TimeOfDay.ToString();
                                    chkSigDiaVie1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (!ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"].Equals(CeC_BD.FechaNula))
                                        TieComVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                                case 7:
                                    hdnSab.Value = "1";
                                    EntMinSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMIN"]).TimeOfDay.ToString();
                                    EntSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    EntMaxSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HEMAX"]).TimeOfDay.ToString();
                                    JornadaSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HTIEMPO"]).TimeOfDay.ToString();
                                    chkSigDiaSab1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    if (!ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"].Equals(CeC_BD.FechaNula))
                                        TieComSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HCTIEMPO"]).TimeOfDay.ToString();
                                    break;
                            }
                        }
                    }
                    return valores;
                case 4://Turno sin generación de asistencia
                    valores[0] = valores[1] = valores[3] = valores[4] = 0;
                    valores[2] = 1;
                    return valores;
                    break;
                case 5://Turno Simple

                    valores[1] = valores[3] = valores[4] = 0;
                    mismaHora = (ds.TURNODIAS.Rows.Count != 1) ? ds.TURNODIAS.Rows[0]["TURNO_DIA_ID"].Equals(ds.TURNODIAS.Rows[1]["TURNO_DIA_ID"]) : true;
                    tolerancia = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HERETARDO"]).TimeOfDay.Subtract(Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HE"]).TimeOfDay);
                    txtTol.Text = ((tolerancia.Hours * 60) + tolerancia.Minutes).ToString();
                    if (mismaHora)
                    {
                        valores[2] = 1;
                        EntLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                        SalLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                        chkSigDiaLun1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HS"]).Day == 2);
                        tolerancia = Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HERETARDO"]).TimeOfDay.Subtract(Convert.ToDateTime(ds.TURNODIAS.Rows[0]["TURNO_DIA_HE"]).TimeOfDay);
                        txtTol.Text = ((tolerancia.Hours * 60) + tolerancia.Minutes).ToString();
                        for (int i = 0; i < ds.TURNODIAS.Rows.Count; i++)
                            diasTurno.Add(Convert.ToInt32(ds.TURNODIAS.Rows[i]["DIA_SEMANA_ID"]));
                        // Activa las casillas de los dias que tendran horario. 
                        // diasTurno es un arreglo con los dias
                        // 0, 'Ninguno',0  
                        // 1, 'Domingo',1  
                        // 2, 'Lunes',1  
                        // 3, 'Martes',1   
                        // 4, 'Miercoles',1  
                        // 5, 'Jueves',1   
                        // 6, 'Viernes',1    
                        // 7, 'Sabado',1   
                        // 8, 'Todos los dias',0 
                        ActivaChkGral(diasTurno);
                    }
                    else
                    {
                        valores[2] = 0;
                        for (int i = 0; i < ds.TURNODIAS.Rows.Count; i++)
                        {
                            switch (Convert.ToInt32(ds.TURNODIAS.Rows[i]["DIA_SEMANA_ID"]))
                            {
                                case 1:
                                    hdnDom.Value = "1";
                                    EntDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalDom.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaDom1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    break;
                                case 2:
                                    hdnLun.Value = "1";
                                    EntLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalLun.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaLun1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    break;
                                case 3:
                                    hdnMar.Value = "1";
                                    EntMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalMar.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaMar1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    break;
                                case 4:
                                    hdnMie.Value = "1";
                                    EntMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalMie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaMie1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    break;
                                case 5:
                                    hdnJue.Value = "1";
                                    EntJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalJue.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaJue1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    break;
                                case 6:
                                    hdnVie.Value = "1";
                                    EntVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalVie.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaVie1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    break;
                                case 7:
                                    hdnSab.Value = "1";
                                    EntSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HE"]).TimeOfDay.ToString();
                                    SalSab.Text = Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).TimeOfDay.ToString();
                                    chkSigDiaSab1.Checked = (Convert.ToDateTime(ds.TURNODIAS.Rows[i]["TURNO_DIA_HS"]).Day == 2);
                                    break;
                            }
                        }
                    }
                    return valores;
            }
            return valores;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.CargaTurno", ex);
            return valores;
        }
    }

    /// <summary>
    /// Activa los checkbox de los días correspondientes al turno
    /// </summary>
    /// <param name="diasTurno">Arreglo con los días correspondientes al turno</param>
    public void ActivaChkGral(ArrayList diasTurno)
    {
        try
        {
            // 0, 'Ninguno',0  
            // 1, 'Domingo',1  
            // 2, 'Lunes',1  
            // 3, 'Martes',1   
            // 4, 'Miercoles',1  
            // 5, 'Jueves',1   
            // 6, 'Viernes',1    
            // 7, 'Sabado',1   
            // 8, 'Todos los dias',0  
            chkAsistGralDom.Checked = false;
            chkAsistGralLun.Checked = false;
            chkAsistGralMar.Checked = false;
            chkAsistGralMie.Checked = false;
            chkAsistGralJue.Checked = false;
            chkAsistGralVie.Checked = false;
            chkAsistGralSab.Checked = false;
            for (int i = 0; i < diasTurno.Count; i++)
            {
                switch (Convert.ToInt32(diasTurno[i]))
                {
                    case 1: chkAsistGralDom.Checked = true; break;
                    case 2: chkAsistGralLun.Checked = true; break;
                    case 3: chkAsistGralMar.Checked = true; break;
                    case 4: chkAsistGralMie.Checked = true; break;
                    case 5: chkAsistGralJue.Checked = true; break;
                    case 6: chkAsistGralVie.Checked = true; break;
                    case 7: chkAsistGralSab.Checked = true; break;
                }
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosEdicion.ActivaChkGral", ex);
        }
    }
    protected void chkTiempoEx_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void WebPanel3_ExpandedStateChanging(object sender, Infragistics.WebUI.Misc.WebPanel.ExpandedStateChangingEventArgs e)
    {
        e.Cancel = true;
    }
    protected void WebPanel3_ExpandedStateChanged(object sender, Infragistics.WebUI.Misc.WebPanel.ExpandedStateChangedEventArgs e)
    {

    }
}
