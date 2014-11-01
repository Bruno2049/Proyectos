<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebaUc.aspx.cs" Inherits="PAEEEM.SupplierModule.PruebaUc" %>


<%--<%@ Register Src="Controls/wucValidaPyme.ascx" TagName="wucValidaPyme" TagPrefix="uc1" %>--%>

<%@ Register Src="Controls/wucCapturaBasica.ascx" TagName="wucCapturaBasica" TagPrefix="uc1" %>
<%@ Register Src="Controls/wucInformacionComplementaria.ascx" TagName="wucInformacionComplementaria" TagPrefix="uc2" %>
<%@ Register Src="Controls/wucValidaPyme.ascx" TagName="wucValidaPyme" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Resources/Css/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />
    <link href="../Resources/Css/Table.css" type="text/css" rel="Stylesheet" />
    <link href="../Resources/Css/TablaNet.css" type="text/css" rel="Stylesheet" />
    <title></title>
    
</head>
<body style="background-color: white; align-content: center; align-items: center;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="clientScriptManager" runat="server">
                        </asp:ScriptManager>
    
    <%--INFORMACION GENERAL
     <div style="align-items: center; width: 100%;">
        <uc1:wucCapturaBasica id="wucCapturaBasica1" runat="server"  />
        <asp:Panel ID="Panel1" runat="server">
            
        </asp:Panel>
    </div>
    <br/><br/>--%>
        
    <div style="align-items: center; width: 100%;">
        <table>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <%--<uc2:wucInformacionComplementaria id="wucInformacionComplementaria1" runat="server"  />--%>
                    <uc1:wucCapturaBasica id="wucCapturaBasica1" runat="server"  />
                    <%--<uc3:wucValidaPyme id="wucValidaPyme1" runat="server"  />--%>
                </td>
            </tr>
        </table>
        
    </div>
    <br/>

    <div>
        <asp:Button ID="btnUserControl" runat="server" Text="Guardar" OnClick="btnUserControl_Click" />
    </div>
    </form>
</body>
</html>
