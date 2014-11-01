<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucValidacionCrediticia.ascx.cs" Inherits="PAEEEM.SupplierModule.Controls.WucValidacionCrediticia" %>
<div id="divForm" runat="server" style="align-content: center"> 
<table>
                <tr>
                    <td>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/SupplierModule/images/t_validar_servicio.png" />
                            <asp:Label ID="lblTitle" runat="server" Text="VALIDAR HISTORIAL CREDITICIO" CssClass="Title"
                                Visible="false">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="lblFecha" runat="server" Text="Fecha" CssClass="Label" ForeColor="#333333">
                            </asp:Label>
                            &nbsp;
                            <asp:TextBox ID="txtFecha" runat="server" Enabled="false" BorderWidth="0" Font-Size="11pt">
                            </asp:TextBox>
                        </div>
                    </td>
                </tr>
            </table>
          <table width="100%">
                 <tr>
                    <td width="180px" style="height: 25px">
                        <asp:Label ID="Label2" runat="server" Text="Nombre o Razón Social" CssClass="Label"
                            Width="165px" ForeColor="#666666" Font-Size="11pt">
                        </asp:Label>
                    </td>
                    <td style="width: 200px; height: 25px;">
                        <asp:TextBox ID="txtRazonSocial" runat="server" Font-Size="11px" CssClass="TextBox"
                            Enabled="False" Width="200px">
                        </asp:TextBox>
                    </td>
                    <td style="width: 115px; height: 25px;">
                        <div>
                        </div>
                    </td>
                    <td style="width: 150px; height: 25px;">
                        <asp:Label ID="Label3" runat="server" Text="Número Crédito" CssClass="Label" Width="150px"
                            ForeColor="#666666" Font-Size="11pt">
                        </asp:Label>
                    </td>
                    <td style="height: 25px">
                        <asp:TextBox ID="txtCreditoNum" runat="server" Font-Size="11px" CssClass="TextBox"
                            Enabled="False" Width="200px">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="180px">
                        &nbsp;
                    </td>
                    <td style="width: 200px">
                        &nbsp;
                    </td>
                    <td style="width: 115px">
                        &nbsp;
                    </td>
                    <td style="width: 150px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
  <table width="100%">
                                <tr>
                                    <td style="width: 670px">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/SupplierModule/images/t6.png" />
                                    </td>
                                    <td>
                                        <img alt="" runat="server" src="~/SupplierModule/images/t12.png" width="166" height="21"/>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Monto a Financiar (MXP)" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label4"></asp:Label>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtRequestAmount"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                    <td>
                                       <%-- <asp:Button runat="server" Text="Formato Autorización" CssClass="Button" Width="200px"
                                            ID="btnDisplayCreditRequest" OnClick="btnDisplayCreditRequest_Click" />
                                --%>    </td>
                                </tr>
                                <tr>
                                    <td style="width: 180px">
                                        <asp:Label runat="server" Text="Número de Pagos" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label5"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtCreditYearsNumber"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                       <%-- <asp:Button runat="server" Text="Tabla de Amortización" CssClass="Button" Width="200px"
                                            ID="btnDisplayPaymentSchedule" OnClick="btnDisplayPaymentSchedule_Click" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Periodicidad de Pago" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="Label6"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtPaymentPeriod"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                     <%--   <asp:Button runat="server" Text="Carta Presupuesto" CssClass="Button" Width="200px"
                                            ID="btnDisplayQuota" OnClick="btnDisplayQuota_Click" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div runat="server" style="display:none">
                                        <asp:Label runat="server" Text="Importe Contrato" CssClass="Label" Font-Size="11pt"
                                            ForeColor="#666666" Width="150px" ID="lblImporte"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div runat="server" style="display:none">
                                        <asp:TextBox runat="server" CssClass="TextBox" Font-Size="11px" Width="200px" ID="txtContratoImporte"></asp:TextBox>
                                        </div>
                                    </td>
                                 <%--   <td style="width: 267px">
                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^[0-9]*[1-9][0-9]*$"
                                            ControlToValidate="txtContratoImporte" ErrorMessage="(*) Campo Obligatorio" ID="revContratoImporte">
                                        </asp:RegularExpressionValidator>
                                    </td>--%>
                                    <td>
                                       <%-- <asp:Button runat="server" Text="Solicitud" CssClass="Button" Width="200px" ID="btnDisplayRequest"
                                            OnClick="btnDisplayRequest_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField runat="server" Value="mop" ID="hiddenfield" />
                            <table width="100%">
                                <tr>
                                   <td class="style1" style="width: 70px">
                                     <%--   <asp:Button runat="server" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?');"
                                            Text="Salir" CssClass="Button" Width="70px" ID="btnSalirValidate" OnClick="btnSalirValidate_Click" /><br />
                                  --%>  </td>
                                    <td width="150px">
                                      <%--  <asp:Button runat="server" Text="Consulta Crediticia" Width="150px" ID="btnConsultaCrediticia"
                                            OnClick="btnConsultaCrediticia_Click" /><br />
                                 --%>   </td>
                                    <td>
                                        <asp:Label ID="lblMopInvalido" runat="server" Text='<%$ Resources:DefaultResource, MOPErrorInvalid %>' Visible="false"
                                            style="font-size: x-large; color: #FF0000; font-weight: 700; background-color: #FFFF00"></asp:Label>
                                    </td>
                                </tr>
                            </table>
    
   
</div>