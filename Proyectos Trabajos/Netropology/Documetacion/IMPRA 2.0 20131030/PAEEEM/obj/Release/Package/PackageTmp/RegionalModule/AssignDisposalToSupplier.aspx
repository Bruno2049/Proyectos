<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignDisposalToSupplier.aspx.cs"
    Inherits="PAEEEM.RegionalModule.AssignDisposalToSupplier" Title="Asignar Centro de Acopio y Destrucción a Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Label
        {
            width: 160px;
            color: #333333;
            font-size: 16px;
        }
        .TextBox
        {
            width: 300px;
        }
        .Button
        {
            width: 150px;
        }
    </style>
    <script type="text/javascript">
        function OnTreeNodeChecked() 
        { 
            var eleCurrent = event.srcElement; 
            var hiddenTreeNodeChecked=document.getElementById("hiddenTreeNodeChecked"); 
            if (eleCurrent.type == 'checkbox') 
            { 
                var checkedNodeID = eleCurrent.id;
                if(eleCurrent.checked)
                {
                    if(hiddenTreeNodeChecked.value!="")
                    {
                        var lastChecked=document.getElementById(hiddenTreeNodeChecked.value);
                        if(lastChecked!=null)
                        {
                            if(lastChecked.type=='checkbox')
                            {
                                lastChecked.checked=false;
                            }
                        }
                    }
                    hiddenTreeNodeChecked.value=checkedNodeID;
                }
                else
                {
                    hiddenTreeNodeChecked.value="";
                }
            } 
        } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="container">
                <div>
                    <br>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/DisposalModule/images/t_equipos22.png" />
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server" CssClass="Label" />&nbsp
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <table width="100%">
                    <tr>
                        <td width="18%">
                            <asp:Label ID="Label4" Text="Nombre o Razón Social" runat="server" CssClass="Label" />
                        </td>
                        <td width="32%">
                            <asp:TextBox ID="txtSociaNamel" runat="server" CssClass="TextBox" Enabled="False"></asp:TextBox>
                        </td>
                        <td width="18%">
                            <asp:Label ID="Label5" Text="Nombre Comercial" runat="server" CssClass="Label" />
                        </td>
                        <td width="32%">
                            <asp:TextBox ID="txtBusinessName" runat="server" CssClass="TextBox" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="25%">
                        </td>
                        <td width="50%" style="text-align: left; vertical-align: top">
                            <asp:Panel ID="Panel1" runat="server" Width="400px" Height="300px" ScrollBars="Auto">
                                <asp:TreeView ID="treeViewDisposal" runat="server" ShowCheckBoxes="All" ShowExpandCollapse="true"
                                    ExpandDepth="0" ShowLines="true" EnableClientScript="true">
                                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                                        NodeSpacing="0px" VerticalPadding="0px" />
                                    <ParentNodeStyle Font-Bold="False" />
                                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                                        VerticalPadding="0px" />
                                </asp:TreeView>
                            </asp:Panel>
                            <input id="hiddenTreeNodeChecked" type="hidden" />
                        </td>
                        <td width="25%">
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="45%" style="text-align: right">
                            <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="Button" OnClick="btnSave_Click"
                                OnClientClick="return confirm('Confirmar Guardar Asignación de Productos al Proveedor')" />
                        </td>
                        <td width="10%">
                        </td>
                        <td width="45%" style="text-align: center">
                            <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClick="btnCancel_Click"
                                OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
