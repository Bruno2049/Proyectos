<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="OldEquipmentReceptionList.aspx.cs"
    Inherits="PAEEEM.DisposalModule.OldEquipmentReceptionList" Title="Recepción de Equipo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/PageMask.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript">
            var last=null;
            function judge(obj)
            {
              if(last==null)
              {
                 last=obj.id;
              }
              else
              {
                var lo=document.getElementById(last);
                lo.checked=false;
                last=obj.name;
              }
              obj.checked="checked";
            }
        function check()
        {
            if(confirm('Confirmar Registro de Ingreso del Equipo Seleccionado'))
            {
                window.document.getElementById("<%=hidConfirm.ClientID%>").click();
            }
        }
        
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

    <style type="text/css">
        .Button
        {
            width: 150px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="lock" class="LockOff">
                <img src="../images/progress.gif" alt="¡ En Proceso, Por favor espere !" style="vertical-align: middle;
                    position: relative;" />
            </div>
            <div id="container">
                <div align="center">
                    <br>
                    <asp:Label ID="lblTitle" runat="server" Text="DATOS DEL SALVAMENTO"></asp:Label>
                </div>
                <div align="right">
                    <asp:Label ID="Label1" Text="Fecha" runat="server"/>&nbsp
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <div>
                    <asp:GridView ID="grdProductList" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        AllowPaging="True" PageSize="20" DataKeyNames="Id_Credito_Sustitucion,IsReceipt" OnRowDataBound="grdProductList_RowDataBound">
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Id_Pre_Folio" HeaderText="Folio Pre-boleta"></asp:BoundField>
                            <asp:BoundField DataField="ProviderName" HeaderText="Proveedor" />
                            <asp:BoundField DataField="ProviderComercialName" HeaderText="Nombre Comercial" />
                            <asp:BoundField DataField="Dx_Razon_Social" HeaderText="Beneficiario" />
                            <asp:BoundField DataField="Dx_Nombre_Programa" HeaderText="Programa" />
                            <asp:BoundField DataField="Dx_Nombre_General" HeaderText="Tecnología" />
                            <asp:BoundField DataField="Dx_Estatus_Credito" HeaderText="Estatus" />
                            <asp:BoundField DataField="Dx_Nombre_Region" HeaderText="Región" />
                            <asp:BoundField DataField="Dx_Nombre_Zona" HeaderText="Zona" />
                            <asp:TemplateField HeaderText="Ingresado" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgRegister" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                           
                            <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Seleccionar" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                        AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                        PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                        UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" FirstPageText="Primero"
                        LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior" CurrentPageButtonClass="cpb">
                    </webdiyer:AspNetPager>
                </div>
                <br />
                <div>
                    <asp:Button ID="btnReception" Text="Registro de Ingreso" runat="server" CssClass="Button"
                        OnClick="btnReception_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnPrint" runat="server" Text="Imprimir Boleta" CssClass="Button" Visible="false"
                        OnClientClick="if (confirm('Confirmar Realizar Impresión de la Boleta de Ingreso')) <%--displayDivTest()--%>;"
                        OnClick="btnPrint_Click" />
                    <asp:Button ID="hidConfirm" runat="server" OnClick="hidConfirm_Click" Style="display: none;" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
