<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucEncabezadoMRV.ascx.cs" Inherits="PAEEEM.MRV.wucEncabezadoMRV" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Label ID="LabelEncabezado" runat="server" Text="Modulo MRV" 
    Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
<hr class="ruleNet" />
<fieldset class="fieldset_Netro">
    <%--<legend style="font-size: 14px; color: #000099;">Modulo MRV</legend>--%>
<table style="width: 100%">
    <tr>
        <td>
            <asp:Label ID="LblNoSolicitud" runat="server" Text="Número de Solicitud" Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;
        </td>
        <td>
            <telerik:RadTextBox ID="RadTxtNumSolicitud" runat="server" Width="90%" 
                Enabled="False">
            </telerik:RadTextBox>
        </td>
        <td>
            <asp:Label ID="LblNombreCliente" runat="server" Text="Nombre" Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;
        </td>
        <td>
            <telerik:RadTextBox ID="RadTxtNombreCliente" runat="server" Width="90%" 
                Enabled="False">
            </telerik:RadTextBox>
        </td>
        <td>
            <asp:Label ID="LblEstado" runat="server" Text="Estado" Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;
        </td>
        <td>
            <telerik:RadTextBox ID="RadTxtEstado" runat="server" Width="90%" 
                Enabled="False">
            </telerik:RadTextBox>
        </td>
        <td>
            <asp:Label ID="lblDelegMunicipio" runat="server" Text="Delegación/Municipio" Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;
        </td>
        <td>
            <telerik:RadTextBox ID="RadTxtDelegMunicipio" runat="server" Width="90%" 
                Enabled="False">
            </telerik:RadTextBox>
        </td>
    </tr>
</table>
</fieldset>
