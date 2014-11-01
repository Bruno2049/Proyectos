<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorImagenes.aspx.cs" Inherits="PAEEEM.Vendedores.VisorImagenes" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="clientScriptManager" runat="server">
    </asp:ScriptManager>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
        <div>
            <img alt="imagen1" src="VisorImagenes.aspx?id=1" />
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" 
        Skin="Office2010Silver">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="rwmVentana" runat="server" Skin="Office2010Silver">
    </telerik:RadWindowManager>
    </form>
</body>
</body>
</html>
