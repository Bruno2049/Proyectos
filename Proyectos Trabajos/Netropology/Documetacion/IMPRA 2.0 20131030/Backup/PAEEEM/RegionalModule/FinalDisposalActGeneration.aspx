<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinalDisposalActGeneration.aspx.cs"
    Inherits="PAEEEM.DisposalModule.FinalDisposalActGeneration" Title="Generación Acta Circunstanciada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>

    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />
    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 90px;
            color: #333333;
            font-size: 16px;
        }
        .DropDownList
        {
            width: 250px;
        }
        .Button
        {
            width: 120px;
        }
    </style>

    <script type="text/javascript">
        function lockScreen() {
            var lock = document.getElementById('lock');        
             lock.style.width = '150px';
            lock.style.height = '30px';                    
            lock.style.top = document.body.offsetHeight/2 - lock.style.height.replace('px','')/2 + 'px';
            lock.style.left = document.body.offsetWidth/2 - lock.style.width.replace('px','')/2 + 'px';
            if (lock)
                lock.className = 'LockOn'; 
        }         
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="lock" class="LockOff">
                <img src="../images/progress.gif" alt="¡ En Proceso, Por favor Espere !" style="vertical-align: middle;
                    position: relative;" />
            </div>
            <div id="container">
            	<div>
                    <br/>
                    <br />
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/RegionalModule/images/t_actas.png" />
                    <br />
                </div>
                <div>
                    <br />
                </div>
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td colspan="3" style="text-align: center">
                            <asp:Literal ID="literal1" runat='server' Text="Fecha Acta Circunstanciada" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="lblActType" runat="server" CssClass="Label" Text="Tipo Acta" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <div>
                                    <asp:DropDownList ID="drpActType" runat="server" CssClass="DropDownList"
                                        AutoPostBack="True" onselectedindexchanged="drpActType_SelectedIndexChanged">
                                        <asp:ListItem Text="Inhabilitación y Destrucción" Value="0" />
                                        <asp:ListItem Text="Recuperación de Residuos" Value="1" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblFromDate" Text="Desde" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtFromDate" runat="server" Width="200px" onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'ctl00_MainContent_txtToDate\')}'})"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblToDate" Text="Hasta" runat="server" CssClass="Label" /></div>
                        </td>
                        <td>
                            <div>
                                <asp:TextBox ID="txtToDate" runat="server" Width="200px" onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_MainContent_txtFromDate\')}',maxDate:'#F{$dp.$D(\'ctl00_MainContent_txtToDate\')}'})"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="lblProgram" runat="server" CssClass="Label" Text="Programa" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpProgram" runat="server" AutoPostBack="true" CssClass="DropDownList"
                                    OnSelectedIndexChanged="drpProgram_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Label ID="lblDisposal" runat="server" CssClass="Label" 
                                    Text="CAyD" Width="150px" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpDisposal" runat="server" AutoPostBack="True" 
                                    CssClass="DropDownList" 
                                    onselectedindexchanged="drpDisposal_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Label ID="lblTechnology" runat="server" CssClass="Label" Text="Tecnología" />
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:DropDownList ID="drpTechnology" runat="server" CssClass="DropDownList"  AutoPostBack="true"
                                    onselectedindexchanged="drpTechnology_SelectedIndexChanged" />
                            </div>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnGenerate" runat="server" Text="Generar" CssClass="Button" OnClick="btnGenerate_Click"
                                OnClientClick="lockScreen()" />
                        </td>
                        <td style="text-align:center">
                        <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="Button"
                                OnClientClick="return confirm('Confirmar Guardar el Acta Circunstanciada Generada'); lockScreen();" onclick="btnSave_Click" />
                        </td>
                        <td style="text-align: left">
                            <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"
                                OnClick="btnCancel_Click" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
