<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucFiltro.ascx.cs" Inherits="PAEEEM.CirculoCredito.wucFiltro" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Label ID="LabelEncabezado" runat="server" Text="MÓDULO CIRCULO DE CRÉDITO"
    Font-Size="Small" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
<hr class="ruleNet" />
<fieldset class="fieldset_Netro">

    <table align="center">
        <br />
        <tr style="width: 350px;">
            
            <td>
                
                <asp:Label ID="LblEstatus" runat="server" Text="Estatus:" Font-Size="Small"></asp:Label>
                <telerik:RadComboBox ID="RadCbxEstatus" runat="server" Width="100px">
                </telerik:RadComboBox>
                <%--<asp:Label ID="Label1" runat="server" Text="Numero de Servicio" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                <telerik:RadTextBox ID="txtRPU" runat="server" EmptyMessage="RPU"></telerik:RadTextBox>--%>
            </td>
            <td align="center">
                <asp:Label ID="NoCredit" runat="server" Text="No. Credito:" Font-Size="Small"></asp:Label>
                <telerik:RadTextBox ID="RadTxtNoCredit" runat="server" EmptyMessage="No. Credito">
                </telerik:RadTextBox>
            </td>
            <td align="center">
                <asp:Label ID="LblNoPaquete" runat="server" Text="No. Paquete:" Font-Size="Small"></asp:Label>
                 <telerik:RadTextBox ID="RadTxtNoPaquete" runat="server" EmptyMessage="No. Paquete">
                </telerik:RadTextBox>
            </td>
            <td align="center">
                <asp:Label ID="lblFolio" runat="server" Text="Folio Paquete:" Font-Size="Small"></asp:Label>
                <telerik:RadTextBox ID="RadTxtFolio" runat="server" EmptyMessage="Folio Paquete">
                </telerik:RadTextBox>
            </td>
        </tr>

    </table>
</fieldset>
