<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CreditAuthorization.aspx.cs"
    Inherits="PAEEEM.RegionalModule.CreditAuthorization" Title="Autorización de Solicitudes de Crédito" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .Label {
            width: 60px;
            color: #333333;
            font-size: 16px;
        }

        .LabelStyle2 {
            width: 250px;
            color: #333333;
            font-size: 16px;
        }
        /** .DropDownList
        {
            width: 240px;
        }**/ .Button {
            width: 120px;
        }

        .TextBox {
            width: 120px;
        }

        .dvpercent {
            width: 98%;
        }

        .part1 {
            width: 100%;
            float: left;
        }

        .part2 {
            width: 100%;
            float: right;
            text-align: right;
        }

        .divbtn {
            text-align: center;
        }
    </style>
    <script type="text/javascript">

        function showDialogInitially() {
            var wnd = $find("<%=modalPopup.ClientID %>");
            wnd.show();
            Sys.Application.remove_load(showDialogInitially);
        }

        function ValidarCbxMotivo() {
            var cbxmotivo = document.getElementById('<%=DDLMotivo.ClientID%>');

            document.getElementById('<%=TexMoti.ClientID%>').value = "";
            document.getElementById('<%=TexObserv.ClientID%>').value = "";

            if (cbxmotivo.options[cbxmotivo.selectedIndex].value != 39 && cbxmotivo.options[cbxmotivo.selectedIndex].value != "Seleccione") {
                 document.getElementById('<%=TexObserv.ClientID%>').style.display = 'block';
                 document.getElementById('<%=observaciones.ClientID%>').style.display = 'block';
                 document.getElementById('<%=TexMoti.ClientID%>').style.display = 'none';
                 document.getElementById('<%=motivo.ClientID%>').style.display = 'none';
                 document.getElementById('<%=btnFinalizar.ClientID%>').disabled = false;
            }
            else if (cbxmotivo.options[cbxmotivo.selectedIndex].value == 39)
            {
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
         ///////////////////////////////////////////////////////////////////////////////////////
         function ValidarCbxMotivoRecha() {
             var cbxmotivo = document.getElementById('<%=DDLMotivoRechazo.ClientID%>');

             document.getElementById('<%=TexMoti.ClientID%>').value = "";
             document.getElementById('<%=TexObserv.ClientID%>').value = "";

             if (cbxmotivo.options[cbxmotivo.selectedIndex].value != 40 && cbxmotivo.options[cbxmotivo.selectedIndex].value != "Seleccione") {
                document.getElementById('<%=TexObservRechazo.ClientID%>').style.display = 'block';
                document.getElementById('<%=observacionesRechazo.ClientID%>').style.display = 'block';
                document.getElementById('<%=TexMotiRechazo.ClientID%>').style.display = 'none';
                document.getElementById('<%=motivoRechazo.ClientID%>').style.display = 'none';
                document.getElementById('<%=btnFinalizarRechazo.ClientID%>').disabled = false;
             }
             else if (cbxmotivo.options[cbxmotivo.selectedIndex].value == 40)
             {
                 document.getElementById('<%=TexMotiRechazo.ClientID%>').style.display = 'block';
                 document.getElementById('<%=motivoRechazo.ClientID%>').style.display = 'block';
                 document.getElementById('<%=TexObservRechazo.ClientID%>').style.display = 'none';
                 document.getElementById('<%=observacionesRechazo.ClientID%>').style.display = 'none';

                 var obser = document.getElementById('<%=TexObservRechazo.ClientID%>');
                 var moti = document.getElementById('<%=TexMotiRechazo.ClientID%>');


                if (obser.value.length > 0 || moti.value.length > 0) {
                     document.getElementById('<%=btnFinalizarRechazo.ClientID%>').disabled = false;
                }
                else
                {
                 document.getElementById('<%=btnFinalizarRechazo.ClientID%>').disabled = true;
                }
             }
             else
             {
                document.getElementById('<%=TexObservRechazo.ClientID%>').style.display = 'none';
                document.getElementById('<%=observacionesRechazo.ClientID%>').style.display = 'none';
                document.getElementById('<%=TexMotiRechazo.ClientID%>').style.display = 'none';
                document.getElementById('<%=motivoRechazo.ClientID%>').style.display = 'none';
                document.getElementById('<%=btnFinalizarRechazo.ClientID%>').disabled = true;
            }

        }

         function ValidarObservacionesRecha(textareaControl, maxlength) {
             var obser = document.getElementById('<%=TexObservRechazo.ClientID%>');
             if (obser.value.length > maxlength) {
                 textareaControl.value = textareaControl.value.substring(0, maxlength);
             }
         }

         function ValidarMotivoRecha(textareaControl, maxlength) {
             var moti = document.getElementById('<%=TexMotiRechazo.ClientID%>');
             if (moti.value.length > maxlength) {
                 textareaControl.value = textareaControl.value.substring(0, maxlength);
             }

             if (moti.value.length > 0) {
                 document.getElementById('<%=btnFinalizarRechazo.ClientID%>').disabled = false;
             }
             else {
                 document.getElementById('<%=btnFinalizarRechazo.ClientID%>').disabled = true;
             }
         }


         function confirmCallBackFn(arg) {
             if (arg == true) {
                 var oButton = document.getElementById('<%=hidBtnPreAutorizar.ClientID%>');
                 oButton.click();
             }
         }


     </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <div id="container">
                <div class="dvpercent">
                    <div>
                        <br>
                    </div>
                    <div class="part1" style="background-image: url('../Resources/Images/t_aprovacion.png'); background-repeat: no-repeat; height: 26px">
                        <asp:Label ID="lblApproveTitle" Text="APROBACION DE CREDITOS" runat="server" CssClass="LabelStyle2"
                            Font-Bold="True" Visible="false" />
                        <div class="part2">
                            <table width="100%">
                                <tr>
                                    <td width="800px"></td>
                                    <td width="45px">
                                        <asp:Label ID="lblFecha" Text="Fecha" runat="server" CssClass="Label" Width="45px" />

                                    </td>

                                    <td width="90px">
                                        <asp:TextBox ID="txtFecha" runat="server" Enabled="false" Width="90px"></asp:TextBox>

                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                    <br />


                    <table width="100%">
                        <tr>
                            <td>

                                <asp:Label ID="lblDistribute" Text="Proveedor" Font-Size="11pt" runat="server" CssClass="Label" />


                            </td>

                            <td width="500px">
                                <asp:DropDownList ID="ddlDistribuidor" Font-Size="11px" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlDistribuidor_SelectedIndexChanged" Width="320px" />
                            </td>
                            <td width="800px"></td>
                            <td></td>
                            <td width="400px"></td>
                            <td>
                                <asp:Label ID="Label1" Text="Sucursal" Font-Size="11pt" runat="server" CssClass="Label" Width="80px" />


                            </td>

                            <td>
                                <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="true" Font-Size="11px"
                                    OnSelectedIndexChanged="drpBranch_SelectedIndexChanged" Width="320px" />
                            </td>

                        </tr>
                        <tr>
                            <td>

                                <asp:Label ID="lblEstatus" Text="Estatus" Font-Size="11pt" runat="server" CssClass="Label" />
                            </td>

                            <td >
                                <asp:DropDownList   ID="ddlEstatus" Font-Size="11px"  Width="320px" runat="server"  
                                     OnSelectedIndexChanged="ddlEstatus_SelectedIndexChanged"  AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td width="800px">

                            </td>
                            <td></td>
                            <td width="400px"></td>
                            <td>
                                <asp:Label ID="lblTecno" Text="Tecnología" runat="server" Font-Size="11pt" CssClass="Label" />
                            </td>

                            <td>
                                <asp:DropDownList ID="ddlTecno" AutoPostBack="true" runat="server" Font-Size="11px"
                                    OnSelectedIndexChanged="ddlTecno_SelectedIndexChanged" Width="320px" />
                            </td>
                        </tr>
                    </table>
               </div>
            </div>

            <br />
            <div class="dvpercent">

                <asp:GridView ID="gvCredit" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="20" DataKeyNames="No_Credito,Cve_Estatus_Credito" OnSelectedIndexChanged="gvCredit_SelectedIndexChanged">
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" HorizontalAlign="Right" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:HyperLinkField HeaderText="Número Crédito" HeaderStyle-Font-Underline="true"
                            DataTextField="No_Credito" DataTextFormatString="{0:0000}" DataNavigateUrlFields="No_Credito,Cve_Estatus_Credito"
                            DataNavigateUrlFormatString="../RegionalModule/CreditReview.aspx?creditno={0} &statusid={1} &Flag =A" >
                        <HeaderStyle Font-Underline="True" />
                        </asp:HyperLinkField>
                        <asp:BoundField HeaderText="Nombre o Razón Social" DataField="Razon_Social" />
                        <asp:BoundField HeaderText="Monto Crédito Solicitado" DataField="Monto_Solicitado" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fecha Alta" DataField="Fecha_Pendiente" DataFormatString="{0: yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" >
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fecha Último Estatus" DataField="Dt_Fecha_Estatus" DataFormatString="{0: yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <%--<asp:BoundField HeaderText="Estatus" DataField="Cve_Estatus_Credito"  />--%>
                        <asp:BoundField HeaderText="Estatus" DataField="Estatus_Credito" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <%--<asp:Button ID="btnEdit" runat="server" Text="Editar" CommandArgument='<%# Bind("No_Credito") %>'
                                    OnClientClick="return confirm('Confirmar Editar crédito');" OnClick="btnEdit_Click" Visible="false"/>--%>
                                <asp:DropDownList ID="LSB_Acciones" runat="server" Enabled="False" AutoPostBack="true" OnSelectedIndexChanged="LSB_Acciones_SelectedIndexChanged">
                                    <asp:ListItem>Elegir Opcion</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>         
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="True" HeaderText="Seleccionar">
                            <ItemTemplate>
                                <div align="center">
                                    <asp:CheckBox ID="ckbSelect" runat="server" OnCheckedChanged="ckbSelect_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                </asp:GridView>

                <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20" CustomInfoTextAlign="Right" CustomInfoSectionWidth="33%"
                    AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
                    PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
                    UrlPaging="false" FirstPageText="Primero" LastPageText="Ultimo" NextPageText="Siguiente"
                    PrevPageText="Prev" CurrentPageButtonClass="cpb" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging">
                </webdiyer:AspNetPager>
            </div>
            <div class="dvpercent" align="right">
                <div class="divbtn" align="right" style="float: right;">
                    <table width="100%">
                        <tr>
                            <td width="342px"></td>
                            
                                <%--<td>
                                    <asp:Button ID="btnRevision" runat="server" Text="En Revisión" OnClientClick="return confirm('Confirmar Cambiar Estatus a: &quot;En Revisión &quot;');"
                                        CssClass="Button" OnClick="btnRevision_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnAutorizado" runat="server" Text="Autorizar" OnClientClick="return confirm('Confirmar la Autorización de la Solicitud de Crédito Seleccionada');"
                                        CssClass="Button" OnClick="btnAutorizado_Click" />

                                </td>
                                <td>
                                    <asp:Button ID="btnRechazado" runat="server" Text="Rechazar" CssClass="Button" OnClientClick="return confirm('Confirmar el Rechazo de la Solicitud de Crédito Seleccionada');"
                                        OnClick="btnRechazado_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPendiente" runat="server" Text="Pendiente" CssClass="Button"
                                        OnClientClick="return confirm('Confirmar dejar Pendiente la  Solicitud de Crédito Seleccionada');"
                                        OnClick="btnPendiente_Click" />
                                    <td></td>
                                </td>--%>

                             <td>
                                
                                <asp:Button ID="btn_Aceptar" runat="server" CssClass="Button" OnClick="btn_Aceptar_Click" Text="Aceptar"/>
                            </td>
                            <%--<td>
                                <asp:Button ID="btn_NuevaSolicitud" runat="server" CssClass="Button" OnClick="btn_NuevaSolicitud_Click" Text="Nueva Solicitud" />
                            </td>--%>
                                    <td></td>
                                </td>
                                <td>
                                   <%-- <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Button"
                                        OnClientClick="return confirm('Confirmar Cancelar la  Solicitud de Crédito Seleccionada');"
                                        OnClick="btnCancelar_Click" />--%>
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
                                                <asp:DropDownList ID="DDLMotivo" Width="300" Font-Size="11px" runat="server" CssClass="DropDownList"  />
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
                                                <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" Enabled="false"  OnClick="btnFinalizar_Click" />
                                            </td>
                                            </tr>
                                        </table>
                                    </div> 
                            </ContentTemplate> 
                        </telerik:RadWindow>    
                    </telerik:RadAjaxPanel> 

                     <telerik:RadAjaxPanel runat="server"  ID="RadAjaxPanel1" LoadingPanelID="ralpConfiguration"> 
                        <telerik:RadWindow ID="RadWindow1" runat="server" Width="600px" Height="450px" Modal="true"> 
                            <ContentTemplate> 
                                    <div style="padding: 10px; text-align: center;"> 
                                        <table>
                                            <tr>
                                            <td>
                                                <p style="text-align: center;"> 
                                                    Seleccione el motivo por el cual se rechazara la solicitud,
                                                     en caso de no encontrarlo dentro de la lista, 
                                                    seleccionar la opción de Otro e indicar el motivo.
                                                </p>  
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan="3" align="center">
                                                <br />MOTIVO: 
                                                <asp:DropDownList ID="DDLMotivoRechazo" Width="300" Font-Size="11px" runat="server" CssClass="DropDownList"  />
                                            </td>
                                            </tr>
                                            <tr >
                                            <td colspan="3" align="center">
                                                <br />
                                                <asp:Label ID="observacionesRechazo" runat="server" Text="OBSERVACIONES:" style="display:none"/>
                                                <asp:TextBox ID="TexObservRechazo" runat="server" Height="110px" Width="360px" style="display:none" TextMode="MultiLine"></asp:TextBox>
                                                <asp:Label ID="motivoRechazo" runat="server" Text="MOTIVO:" style="display:none"/>
                                                <asp:TextBox ID="TexMotiRechazo" runat="server" Height="110px" Width="360px" style="display:none" TextMode="MultiLine"  ></asp:TextBox>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td align="center">
                                                <br />
                                                <asp:Button ID="btnFinalizarRechazo" runat="server" Text="Finalizar" Enabled="false" OnClick="btnFinalizarRechazo_Click"  />
                                            </td>
                                            </tr>
                                        </table>
                                    </div> 
                            </ContentTemplate> 
                        </telerik:RadWindow>    
                    </telerik:RadAjaxPanel> 

            <%--   ///<------///--%>
            <div style="display: none">
                <asp:Button ID="hidBtnPreAutorizar" runat="server" Text="Button" 
                    onclick="hidBtnPreAutorizar_Click" />
            </div>
            <telerik:RadWindowManager ID="rwmVentana" runat="server" Skin="Office2010Silver">                                  
            </telerik:RadWindowManager> 
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
