<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_TareasPendientes.aspx.cs"
    Inherits="WF_TareasPendientes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tareas pendientes</title>
        <style type="text/css">
        html, body, #wrapper, #Tabla
        {
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            border: none;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="Tabla"  runat="server" 
    style="font-family: Verdana; font-size: small;">
    <table id="wrapper">
        <tr>
            <td height="1px" style="text-align: left">
                <div>
                    <ul>
                        <asp:HyperLink ID="Lnk_SinTerminal" runat="server" NavigateUrl="WF_TerminalE2.aspx?Parametros=0"><li >Agregar Checador.</li><address style="color: #000000; margin-left: 40px">No tiene un checador registrado, de click aqui para agregarlo.</address></asp:HyperLink>
                        <asp:HyperLink ID="Lnk_EmpleadosImportar" runat="server" NavigateUrl="WF_Importacion.aspx"><li >Importar empleados.</li><address style="color: #000000; margin-left: 40px">No tiene empleados creados en el sistema, de click aqui para importarlos o capturarlos masivamente.</address></asp:HyperLink>
                        <asp:HyperLink ID="Lnk_EmpleadosEnTerminal" runat="server" NavigateUrl="WF_PersonasNoRegistradas.aspx"><li >Agregar empleados sin registro.</li><address style="color: #000000; margin-left: 40px">Tiene empleados en su checador que no existen en el sistema, de click aqui para agregarlos al sistema.</address></asp:HyperLink>
                        <asp:HyperLink ID="Lnk_EmpleadosSinTurno" runat="server" NavigateUrl="WF_TurnosAsignacionExpress.aspx?Parametros=SinTurno"><li >Asignar turno a empleados.</li><address style="color: #000000; margin-left: 40px">Tiene empleados sin un turno asignado, de click aqui para asignarles un turno.</address></asp:HyperLink>
                        <asp:HyperLink ID="Lnk_EmpleadosHorasExtras" runat="server" NavigateUrl="WF_HorasExtrasGrupo.aspx?Parametros=|"><li >Autorizar Horas Extras.</li><address style="color: #000000; margin-left: 40px">Tiene empleados sin autorizar sus horas extras, de click aqui para autorizarles sus horas.</address></asp:HyperLink>
                        <asp:HyperLink ID="Lnk_SolicitudesPendientes" runat="server" NavigateUrl="WF_Solicitudes.aspx"><li >Consultar Solicitudes.</li><address style="color: #000000; margin-left: 40px">Tiene solicitudes sin autorizar o denegar, de click aqui para consultarlas.</address></asp:HyperLink>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: bottom">
                <iframe src="http://www.eclock.com.mx/Recursos/PublicidadInicio/index.html" style="height: 220px"
                    width="100%" frameborder="0"></iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
