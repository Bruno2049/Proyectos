
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#"  CodeFile="WF_ExportarIncidenciasSicoss.aspx.cs" AutoEventWireup="True" Inherits="WF_ExportarIncidenciasSicoss" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eClock</title>
    <script id="igClientScript" type="text/javascript">
<!--

<%-- 

function VerTerminales(){
	window.showModalDialog('WF_TerminalesGrid.aspx', '', 'dialogWidth:330px;dialogHeight:450px');
}
--%>
var abrirTerminalesGrid = false;
function btnImportar_MouseDown(oButton, oEvent){
    if (abrirTerminalesGrid)
        VerTerminales();
	//Add code to handle your event here.
}
// -->
</script>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">

    <form id="form1" runat="server">
    </form>
</body>
</html>