<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreditMonitor.aspx.cs" Inherits="PAEEEM.CreditMonitor" Title="Monitor de Crédito"%>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
        .Label
        {
            width: 160px;
            color: #333333;
            font-size:16px;
        }
        .Label_1
        {
            width: 100px;
            color:#333333;   
            font-size:16px;                     
        }
        .DropDownList
        {
            width:330px    
        }
        .Button
        {
            width:120px;
        }
        .CenterButton
        {
            width:120px;           
            margin-right:5px;
        }
    </style>
    <script type="text/javascript">

        function confirmCallBackFn(arg) {
            if (arg == true) {
                var oButton = document.getElementById("ctl00_MainContent_" + "HiddenButton");
                oButton.click();
            }
        }

        function showDialogInitially() {
            var wnd = $find("<%=modalPopup.ClientID %>");
                wnd.show();
                Sys.Application.remove_load(showDialogInitially);
            }

        function ValidarCbxMotivo() {
            document.getElementById('<%=TexMoti.ClientID%>').value = "";
            document.getElementById('<%=TexObserv.ClientID%>').value = "";
            
            var cbxmotivo = document.getElementById('<%=DDLMotivo.ClientID%>');
            if (cbxmotivo.options[cbxmotivo.selectedIndex].value != 39 && cbxmotivo.options[cbxmotivo.selectedIndex].value != "Seleccione") {
                document.getElementById('<%=TexObserv.ClientID%>').style.display = 'block';
                document.getElementById('<%=observaciones.ClientID%>').style.display = 'block';
                document.getElementById('<%=TexMoti.ClientID%>').style.display = 'none';
                document.getElementById('<%=motivo.ClientID%>').style.display = 'none';
                document.getElementById('<%=btnFinalizar.ClientID%>').disabled = false;


            } else if (cbxmotivo.options[cbxmotivo.selectedIndex].value ==39) {
                document.getElementById('<%=TexMoti.ClientID%>').style.display = 'block';
                document.getElementById('<%=motivo.ClientID%>').style.display = 'block';
                document.getElementById('<%=TexObserv.ClientID%>').style.display = 'none';
                document.getElementById('<%=observaciones.ClientID%>').style.display = 'none';

                var obser = document.getElementById('<%=TexObserv.ClientID%>');
                var moti = document.getElementById('<%=TexMoti.ClientID%>');


                if (obser.value.length > 0 || moti.value.length > 0) {
                        document.getElementById('<%=btnFinalizar.ClientID%>').disabled = false;
                         }
                         else {
                             document.getElementById('<%=btnFinalizar.ClientID%>').disabled = true;
                                }
            }
            else 
            {
                document.getElementById('<%=TexObserv.ClientID%>').style.display = 'none';
                document.getElementById('<%=observaciones.ClientID%>').style.display = 'none';
                document.getElementById('<%=TexMoti.ClientID%>').style.display = 'none';
                document.getElementById('<%=motivo.ClientID%>').style.display = 'none';
                document.getElementById('<%=btnFinalizar.ClientID%>').disabled = true;
            }

    


         }

         function ValidarObservaciones(textareaControl, maxlength) {
             var obser = document.getElementById('<%=TexObserv.ClientID%>');
             if (obser.value.length > maxlength) {
                 textareaControl.value = textareaControl.value.substring(0, maxlength);
             }
         }

         function ValidarMotivo(textareaControl, maxlength) {
             var moti = document.getElementById('<%=TexMoti.ClientID%>');
             if (moti.value.length > maxlength) {
                 textareaControl.value = textareaControl.value.substring(0, maxlength);
             }

             if (moti.value.length > 0) {
                 document.getElementById('<%=btnFinalizar.ClientID%>').disabled = false;
             }
             else {
                 document.getElementById('<%=btnFinalizar.ClientID%>').disabled = true;
             }
         }

     </script> 

</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div id="container">
            
            <div>
            <br>
            <asp:Image runat="server" ImageUrl="images/t_monitor.png"/>
            </div>
            
                <div align="right">
                    <asp:Label Text="Fecha" runat="server" CssClass="Label"/>&nbsp 
                    <asp:Literal ID="literalFecha" runat='server' />
                </div>
                <br />
                <table width="100%">
                <tr>
                                <td width="162px"><div><asp:Label Text="Fecha Ingreso" runat="server"  Font-Size="11pt" CssClass="Label" Width="90px" /></div>
                </td>
                                <td><div><asp:DropDownList ID="drpFechaDate" runat="server" Font-Size="11px" AutoPostBack="true" 
                        CssClass="DropDownList" 
                        onselectedindexchanged="drpFechaDate_SelectedIndexChanged" DataTextFormatString="{0:dd-MM-yyyy}"/></div>
                </td>
                                 <td width="160px"><div></div>
                </td>
                                 <td width="55px"><div> <asp:Label Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label_1" Width="50px" /></div>
                </td>
                                 <td><div><asp:DropDownList ID="drpEstatus" AutoPostBack="true" Font-Size="11px" runat="server" 
                        CssClass="DropDownList" 
                        onselectedindexchanged="drpEstatus_SelectedIndexChanged" /></div>
                </td>

                </tr>
                 <tr>
                                <td width="162px"><div><asp:Label Text="Nombre o Razón Social" runat="server" Font-Size="11pt"  CssClass="Label"/></div>
                </td>
                                <td><div><asp:DropDownList ID="drpRazonSocial" runat="server" Font-Size="11px" AutoPostBack="true" 
                        CssClass="DropDownList" 
                        onselectedindexchanged="drpRazonSocial_SelectedIndexChanged"/></div>
                </td>
                                 <td width="160px"><div></div>
                </td>
                                 <td width="55px"><div> <asp:Label Text="Tecnología" Font-Size="11pt" runat="server" CssClass="Label_1"/></div>
                </td>
                                 <td><div><asp:DropDownList ID="drpTechnology" AutoPostBack="true" Font-Size="11px" runat="server" 
                        CssClass="DropDownList" 
                        onselectedindexchanged="drpTechnology_SelectedIndexChanged" /></div>
                </td>

                </tr>

                </table>
              
                <br />
                <div>
                    <asp:GridView ID="grdCredit" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="20" DataKeyNames="No_Credito, Cve_Estatus_Credito"
                    OnRowCommand="OnRowCommand" OnRowCreated="OnRowCreated" OnDataBound="OnDataBound">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:HyperLinkField  DataTextField="No_Credito" DataNavigateUrlFields="No_Credito, Cve_Estatus_Credito" 
                             DataNavigateUrlFormatString="../RegionalModule/CreditReview.aspx?creditno={0}&statusid={1}&Flag=M" HeaderText="Número Crédito" HeaderStyle-Font-Underline="true">
                        <HeaderStyle Font-Underline="True" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="Dt_Fecha_Pendiente" HeaderText="Fecha Ingreso" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Width="80px">
                        <ItemStyle Width="80px" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Dx_Razon_Social" HeaderText="Nombre o Razón Social" />
                        <asp:BoundField DataField="Dx_Nombre_Repre_Legal" HeaderText="Representante" />
                        <asp:BoundField DataField="Dx_Tel_Fisc" HeaderText="Teléfono" />
                        <asp:BoundField DataField="Mt_Monto_Solicitado" HeaderText="Monto Solicitado" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Dx_Estatus_Credito" HeaderText="Estatus" />                  
                        <asp:TemplateField HeaderText="Validar e Integrar" Visible="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkValidate" runat="server" CausesValidation="false" CommandName="Validate"
                                    OnClientClick="return confirm('Confirmar Realizar Validación e Integración del Expediente');" Text="Validar e Integrar" />
                                <asp:HiddenField ID="hdMOP" runat="server" Value='<%# Bind("No_MOP") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <%--<asp:Button ID="btnEdit" runat="server" Text="Editar" CommandArgument='<%# Bind("No_Credito") %>'
                                    OnClientClick="return confirm('Confirmar Editar crédito');" OnClick="btnEdit_Click" Visible="false"/>--%>
                                <asp:DropDownList ID="LSB_Acciones" runat="server" Enabled="False" OnSelectedIndexChanged="LSB_Acciones_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>Elegir Opcion</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>Seleccionar</HeaderTemplate>
                           <ItemTemplate>                               
                           <div align="center">
                                <asp:CheckBox ID="ckbSelect" runat="server" AutoPostBack="True" OnCheckedChanged="ckbSelect_CheckedChanged1"></asp:CheckBox></div>
                               
                           </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Cve_Estatus_Credito" Visible="False" />
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
                    AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                    PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                    UrlPaging="false" OnPageChanged="AspNetPager_PageChanged"  OnPageChanging="AspNetPager_PageChanging" FirstPageText="Primero"
                    LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior" CurrentPageButtonClass="cpb">
                </webdiyer:AspNetPager>
                </div>
                <br />
                <div align="right" style="padding: 5px">
                    <table >
                    <tr>
                    <td>
                        <asp:Button ID="btn_Aceptar" runat="server" CssClass="Button" OnClick="btn_Aceptar_Click" Text="Aceptar"/>
                    </td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnAddCredit" Text="Nueva Solicitud" runat="server" OnClick="btnAddCredit_Click" OnClientClick="return confirm(' ¿ Desea Realizar el Alta de una Nueva Solicitud de Crédito ?');" CssClass="CenterButton"/>
                                
                            
                        </td>
                    <td>
                        <%--<asp:Button ID="btnCancelCredit" Text="Cancelar" runat="server" OnClick="btnCancelCredit_Click" OnClientClick="return confirm(' ¿ Está Seguro que Desea Cancelar las Solicitudes de Crédito seleccionadas ?');" CssClass="CenterButton"/>--%>
                    </td>
                    </tr>
                    </table>
                </div>
            </div>
              <%--   ///addd by @l3x///--%>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager> 
            <telerik:RadAjaxPanel runat="server"  ID="rapConfiguration" LoadingPanelID="ralpConfiguration"> 
                <telerik:RadWindow ID="modalPopup" runat="server" Width="600px" Height="450px" Modal="true"> 
                    <ContentTemplate> 
                            <div style="padding: 10px; text-align: center;"> 
                                <table>
                                    <tr>
                                    <td>
                                        <p style="text-align: center;"> 
                                            Seleccione el motivo por el cual se cancelara la solicitud, 
                                            en caso de no encontrarlo dentro de la lista, 
                                            seleccionar la opción de Otro e indicar el motivo. 
                                        </p>  
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="3" align="center">
                                        <br />MOTIVO: 
                                        <asp:DropDownList ID="DDLMotivo" Font-Size="11px" runat="server" CssClass="DropDownList"  />
                                    </td>
                                    </tr>
                                    <tr >
                                    <td colspan="3" align="center">
                                        <br />
                                        <asp:Label ID="observaciones" runat="server" Text="OBSERVACIONES:" style="display:none"/>
                                        <asp:TextBox ID="TexObserv" runat="server" Height="110px" Width="360px" style="display:none" TextMode="MultiLine"></asp:TextBox>
                                        <asp:Label ID="motivo" runat="server" Text="MOTIVO:" style="display:none"/>
                                        <asp:TextBox ID="TexMoti" runat="server" Height="110px" Width="360px" style="display:none" TextMode="MultiLine"  ></asp:TextBox>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td align="center">
                                        <br />
                                        <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" Enabled="false"  OnClick="btnFinalizar_Click"/>
                                    </td>
                                    </tr>
                                </table>
                            </div> 
                    </ContentTemplate> 
                </telerik:RadWindow>    
            </telerik:RadAjaxPanel> 
            <%--   ///<------///--%>

            <%--added by Edu--%>
        <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false" /> 
                    <telerik:RadWindowManager ID="RadWindowManager3" runat="server" EnableShadow="true"> </telerik:RadWindowManager> 
                    <div style="display: none">
                        <asp:Button ID="HiddenButton" BackColor="#FFFFFF" OnClick="HiddenButton_Click" runat="server" Width="0px" />
                    </div>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
