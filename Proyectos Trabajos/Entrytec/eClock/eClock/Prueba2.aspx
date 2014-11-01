<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prueba2.aspx.cs" Inherits="Prueba2" %>

<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI.NavigationControls" tagprefix="ig" %>

<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>



<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>



<%@ Register src="WUC__IncidenciaEd.ascx" tagname="WUC__IncidenciaEd" tagprefix="uc1" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:WUC__IncidenciaEd ID="WUC__IncidenciaEd1" runat="server" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Cargar" />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
        Text="Guardar" />
    <asp:TextBox ID="TextBox1" runat="server" Width="788px"></asp:TextBox>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>
