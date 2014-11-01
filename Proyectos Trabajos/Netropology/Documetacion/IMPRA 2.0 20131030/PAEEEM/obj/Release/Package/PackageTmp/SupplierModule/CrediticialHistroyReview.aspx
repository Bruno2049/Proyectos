<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrediticialHistroyReview.aspx.cs"
    Inherits="PAEEEM.CrediticialHistroyReview" Title="Revisión del Historial de Crédito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
        .Label
        {
            width: 20%;
            color: Maroon;
        }
        .Label1
        {
            width: 50%;
            color: Maroon;
        }
        .Label_1
        {
            color: Maroon;
            width: 13%;
        }
        .Title
        {
            text-align: center;
            font-weight: bold;
            font-size: 1em;
            color: #666666;
            font-variant: small-caps;
            text-transform: none;
            margin-bottom: 0px;
        }
        .titulo{
				
				
				
				background-image:url('../Resources/Images/tec2.png');
				background-repeat: no-repeat;
				background-position:center;				
				
				border-color:#CCCCCC;
				width:150px;
				height:26px;
				
}
.titulo1{
				
				
				
				background-image:url('../Resources/Images/centro1.png');
				background-repeat: no-repeat;
				background-position:center;				
				border:0px;
				border-color:#CCCCCC;
				width:80px;
				height:26px;
				
}

.titulo2{
				
				color: #FFFFFF;
				
				background-image:url('../Resources/Images/tipo1.png');
				background-repeat: no-repeat;	
				background-position:center;			
				border:0px;
				width:100px;
				height:26px;
				border-color:#CCCCCC;
				
}
.titulo3{
				
				color: #FFFFFF;
				
				background-image:url('../Resources/Images/modelo1.png');
				background-repeat: no-repeat;
				background-position:center;				
				border:0px;

				border-color:#CCCCCC;
				width:70px;
				height:26px;
				
}
.titulo4{
				
				color: #FFFFFF;
				font-size:16px;
				background-image:url('../Resources/Images/marca1.png');
				background-repeat: no-repeat;
				background-position:center;				
				border:0px;
				border-color:#CCCCCC;
				width:70px;
				height:26px;
				
}
.titulo5{
				
				color: #FFFFFF;
				border:0px;
				background-image:url('../Resources/Images/numero1.png');
				background-repeat: no-repeat;
				background-position:center;				
				border-color:#CCCCCC;
				width:150px;
				height:26px;
				
}

        .TextBox
        {
            width: 22%;
        }
        .TextBox_1
        {
            width: 18.5%;
        }
        .DropDownList
        {
            width: 22%;
        }
        .DropDownList1
        {
            width: 12%;
        }
        .DropDownList_1
        {
            width: 30%;
        }
        .Button
        {
            width: 15%;
        }
        .Button_1
        {
            width: 28%;
        }
        .RadioButton
        {
            width: 15%;
        }
        .Disvisible
        {
            visibility: hidden;
        }
    .style1 {
				font-size: small;
				color: #0499BC;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div align="left">
        <h6>
        <br>
        <asp:Image runat="server" ImageUrl="images/t_informacion.png"/>
        </h6>
    </div>
    <table width="100%" border="0">
        <tr>
            <td>
                <div align="left">
                    <asp:Label ID="lblCredito" Text="No. Crédito" runat="server" CssClass="Label" Width="150px" ForeColor="#333333" />
                    <asp:TextBox ID="txtCredito" runat="server" CssClass="TextBox_1" Width="200px" Enabled="false">
                    </asp:TextBox>
                </div>
            </td>
            <td>
    <div align="right" style="width:100%">
        <asp:Label ID="lblFecha" Text="Fecha" runat="server" CssClass="Label" ForeColor="#333333" />
        <asp:TextBox ID="txtFecha" runat="server" BorderWidth="0" Enabled="false"></asp:TextBox>
    </div>
            </td>
        </tr>
    </table>

    <br />
    <asp:Panel ID="panelValidate" runat="server" >

    <table width="100%">
    <tr>
    <td>
    <asp:Image runat="server" ImageUrl="images/t6.png"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Image runat="server" ImageUrl="images/t12.png" id="Image1"/>
    </td>
    </tr>
    </table>
    <table>
					<tr>
									<td width="204px">
									<div>
													<asp:Label ID="Label4" runat="server" Text="Monto a Financiar (MXP)" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td width="220px">
									<div>
													<asp:TextBox ID="txtRequestAmount" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td width="200px"></td>
									<td>
									<div>
													<asp:Button ID="btnDisplayCreditRequest" runat="server" Text="Solicitud de Crédito" CssClass="Button" OnClick="btnDisplayCreditRequest_Click" Width="165px" />
									</div>
									</td>
<!--
									<td width="10px"></td>
									<td>
									    <div>
													<asp:Button ID="btnDisplayPaymentSchedule" runat="server" Text="Tabla de Amortización" CssClass="Button" OnClick="btnDisplayPaymentSchedule_Click" Width="165px" />
									    </div>
									</td>
-->									
					</tr>
					<tr>
									<td>
									<div>
													<asp:Label ID="Label5" runat="server" Text="Número de Pagos" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td width="200px">
									<div>
													<asp:TextBox ID="txtCreditYearsNumber" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td></td>
<!--									
									<td>
									<div>
													<asp:Button ID="btnDisplayQuota" runat="server" Text="Carta Presupuesto" CssClass="Button" OnClick="btnDisplayQuota_Click" Width="165px" />
									</div>
									</td>
									<td></td>
-->									
									<td>
									<div>									
											        <asp:Button ID="btnDisplayCreditRequest1" runat="server" Text="Formato Autorización" CssClass="Button_1" OnClick="btnDisplayCreditRequest1_Click" Width="165px" />
									</div>
									</td>								
					</tr>
					<tr>
									<td>
									<div>
													<asp:Label ID="Label6" runat="server" Text="Periodicidad de Pago" CssClass="Label" Width="150px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtPaymentPeriod" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td width="200px"></td>
					</tr>
	</table>
      
    </asp:Panel>

    <asp:Panel ID="panelIntegrate" runat="server" >
   
    <table width="100%">
    <tr>
    <td>
    <br>
    <asp:Image runat="server" ImageUrl="images/t7.png"/>
    </td>
    </tr>
    <tr>
    <td>
	<div>
	<asp:Image runat="server" ImageUrl="images/arrow.png"/>
	<asp:Label ID="Label10" runat="server" Text="PODER NOTARIAL DEL REPRESENTANTE LEGAL" CssClass="Label1" Width="350px" ForeColor="#333333">
	</asp:Label>
	</div>
	</td>
	</tr>
	</table>
    <table width="900px">
					<tr>
									<td colspan="5" class="style1">
									<img alt="" src="images/notice.png" width="22" height="20"> Sólo 
									sí el beneficiario es una Persona Física y cuenta con un 
									Representante Legal, completar la 
									siguiente información</img></td>
					</tr>
					<tr>
									<td width="230px">
									<div>
													<asp:Label ID="Label11" runat="server" Text="Número Escritura" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td width="300px">
									<div style="width: 222px">
													<asp:TextBox ID="txtRepresentative_LegalDocumentNumber" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td width="300px"></td>
									<td width="150px">
									<div>
													<asp:Label ID="Label19" runat="server" Text="Fecha" CssClass="Label" Width="150px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<asp:TextBox ID="txtRepresentative_LegalDocumentFecha" runat="server" CssClass="TextBox" Width="200px">
									</asp:TextBox>
									</td>
					</tr>
					<tr>
									<td>
									<div>
													<asp:Label ID="Label12" runat="server" Text="Nombre Completo del Notario" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td width="300px">
									<div>
													<asp:TextBox ID="txtRepresentative_NotariesProfessionName" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td width="100px"></td>
									<td>
									<div>
													<asp:Label ID="Label20" runat="server" Text="Número Notaría" CssClass="Label" Width="150px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtRepresentative_NotariesProfessionNumber" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
					</tr>
					<tr>
									<td>
									<div>
													<asp:Label ID="Label13" runat="server" Text="Estado" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td width="300px">
									<div>
													<asp:DropDownList ID="drpRepresentative_Estado" runat="server" CssClass="DropDownList" Width="200px">
													</asp:DropDownList>
									</div>
									</td>
									<td width="100px"></td>
									<td>
									<div>
													<asp:Label ID="Label21" runat="server" Text="Delegación o Municipio" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:DropDownList ID="drpRepresentative_OfficeorMunicipality" runat="server" CssClass="DropDownList" Width="200px">
													</asp:DropDownList>
									</div>
									</td>
					</tr>
	</table>
    <table width="100%">
					<tr>
									<td>
									<div>
													<asp:Image runat="server" ImageUrl="images/arrow.png"/>
													<asp:Label ID="Label14" runat="server" Text="ACTA CONSTITUTIVA (PERSONA MORAL)" CssClass="Label1" Width="350px" ForeColor="#333333">
													</asp:Label>
									</div>
									</td>
					</tr>
	</table>
	<table width="900px">
					<tr>
									<td width="200px">
									<div>
													<asp:Label ID="Label15" runat="server" Text="Número Escritura" CssClass="Label" Width="203px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td style="width: 277px">
									<div style="width: 222px">
													<asp:TextBox ID="txtApplicant_LegalDocumentNumber" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td style="width: 202px">
									<div>
													<asp:Label ID="Label23" runat="server" Text="Fecha" CssClass="Label" Width="150px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtApplicant_LegalDocumentFecha" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
					</tr>
					<tr>
									<td>
									<div>
													<asp:Label ID="Label16" runat="server" Text="Nombre Completo del Notario" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtApplicant_NotariesProfessionName" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td>
									<div>
													<asp:Label ID="Label24" runat="server" Text="Número Notaría" CssClass="Label" Width="150px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtApplicant_NotariesProfessionNumber" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
					</tr>
					<tr>
									<td>
									<div>
													<asp:Label ID="Label17" runat="server" Text="Estado" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:DropDownList ID="drpApplicant_Estado" runat="server" CssClass="DropDownList" Width="200px">
													</asp:DropDownList>
									</div>
									</td>
									<td>
									<div>
													<asp:Label ID="Label25" runat="server" Text="Delegación o Municipio" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:DropDownList ID="drpApplicant_OfficeOrMunicipality" runat="server" CssClass="DropDownList" Width="200px">
													</asp:DropDownList>
									</div>
									</td>
					</tr>
	</table>
	<table>
					<tr>
									<td>
									<div>
													<asp:Image runat="server" ImageUrl="images/arrow.png" />
													<asp:Label ID="Label18" runat="server" Text="ESCRITURA DEL INMUEBLE Y ACTA DE MATRIMONIO DEL OBLIGADO SOLIDARIO" CssClass="Label1" Width="600px" ForeColor="#333333">
													</asp:Label>
									</div>
									</td>
					</tr>
	</table>
	<table width="900px">
					<tr>
									<td width="210px">
									<div>
													<asp:Label ID="Label22" runat="server" Text="Número Escritura" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div style="width: 280px">
													<asp:TextBox ID="txtGuarantee_LegalDocumentNumber" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td>
									<div>
													<asp:Label ID="Label27" runat="server" Text="Fecha" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtGuarantee_LegalDocumentFecha" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
					</tr>
					<tr>
									<td>
									<div>
													<asp:Label ID="Label26" runat="server" Text="Nombre Completo del Notario" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtGuarantee_NotariesProfessionName" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td>
									<div>
													<asp:Label ID="Label28" runat="server" Text="Número Notaría" CssClass="Label" Width="150px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtGuarantee_NotariesProfessionNumber" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
					</tr>
					<tr>
									<td>
									<div>
													<asp:Label ID="Label29" runat="server" Text="Estado" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:DropDownList ID="drpGuarantee_Estado" runat="server" CssClass="DropDownList" Width="200px">
													</asp:DropDownList>
									</div>
									</td>
									<td>
									<div>
													<asp:Label ID="Label30" runat="server" Text="Delegación o Municipio" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:DropDownList ID="drpGuarantee_OfficeOrMunicipality" runat="server" CssClass="DropDownList" Width="200px">
													</asp:DropDownList>
									</div>
									</td>
					</tr>
					<tr>
									<td>
									<div>
													<asp:Label ID="Label31" runat="server" Text="Fecha Liberación Gravamen" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtPropertyEncumbrancesFecha" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td>
									<div>
													<asp:Label ID="Label32" runat="server" Text="Emitido por" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtPropertyEncumbrancesName" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
					</tr>
					<tr>
									<td>
									<div>
													<asp:Label ID="Label33" runat="server" Text="Num. Acta de Matrimonio" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtMarriageNumber" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
									<td>
									<div>
													<asp:Label ID="Label34" runat="server" Text="Num. Registro Civil" CssClass="Label" Width="200px" ForeColor="#666666">
													</asp:Label>
									</div>
									</td>
									<td>
									<div>
													<asp:TextBox ID="txtCitizenRegisterOffice" runat="server" CssClass="TextBox" Width="200px">
													</asp:TextBox>
									</div>
									</td>
					</tr>
	</table>
    </asp:Panel>

    <asp:Panel ID="panelReplacement" runat="server" ForeColor="White" >
    <table width="100%">
    <tr>
    <td>
    <div>
    <br>
    <asp:Image runat="server" ImageUrl="images/t10.png"/>
    </div>
    </td>
    </tr>
    </table>
    <table width="100%">
        <caption>
            <asp:RadioButton ID="radAcquisition" runat="server" 
                CssClass="RadioButton" GroupName="gpReplacement" AutoPostBack="True" Text="Adquisición de Equipo"/>
            <asp:RadioButton ID="radReplacement" runat="server" AutoPostBack="True" Text="Sustitución de Equipo"
                Checked="true" CssClass="RadioButton" GroupName="gpReplacement"/>
        </caption>
    </table>
    <!--    <img alt="" src="images/arrow.png" width="16" height="16"> -->
	<asp:Image ID="Image3" runat="server" ImageUrl="images/arrow.png" Height="16px"/>      
	<asp:Label ID="Label37" runat="server" Text="Información Disposición Equipo Baja Eficiencia" CssClass="Label1" ForeColor="#333333">
	</asp:Label>
	<br />

	<%--<asp:GridView ID="gdvReplacement" runat="server" AutoGenerateColumns="False" Width="770px"
            OnDataBound="gdvReplacement_DataBound" BorderStyle="None" DataKeyNames="KeyNumber">
            <Columns>
                <asp:TemplateField HeaderImageUrl="../Resources/Images/tec2.png">
                

                    <ItemTemplate>
                        <asp:DropDownList ID="drpTechnology" runat="server" Width="100%">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle Width="9%" />
                </asp:TemplateField>
                <asp:templatefield HeaderImageUrl="../Resources/Images/centro1.png">
                    <ItemTemplate>
                        <asp:DropDownList ID="drpDisposalCenter" runat="server" Width="100%">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle Width="13%" />
                </asp:TemplateField>
                <asp:templatefield HeaderImageUrl="../Resources/Images/tipo1.png">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTypeOfProduct" runat="server" Width="100%"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="10%" />
                </asp:TemplateField>
               <asp:templatefield HeaderImageUrl="../Resources/Images/modelo1.png">
                    <ItemTemplate>
                        <asp:TextBox ID="txtModelo" runat="server" Width="100%"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="3%" />
                </asp:TemplateField>
               <asp:templatefield HeaderImageUrl="../Resources/Images/marca1.png">
                                   <ItemTemplate>
                        <asp:TextBox ID="txtMarca" runat="server" Width="100%"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:templatefield HeaderImageUrl="../Resources/Images/numero1.png">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSerial" runat="server" Width="100%"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="10%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>--%>
   <asp:GridView runat="server" AutoGenerateColumns="False" DataKeyNames="KeyNumber"  CssClass="GridViewStyle"
                                        BorderStyle="None" Width="100%" ID="gdvReplacement" OnDataBound="gdvReplacement_DataBound">
                                         <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Tecnología">
                                                <ItemTemplate>
                                                    <%--Update by Tina 2011/08/03--%><asp:DropDownList ID="drpTechnology" runat="server"
                                                        Width="100%">
                                                    </asp:DropDownList>
                                                    <%--End--%>
                                                </ItemTemplate>
                                                 <ItemStyle Width="13%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CAyD">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpDisposalCenter" runat="server"
                                                        Width="100%">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle Width="14%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Tipo Producto">
                                                <ItemTemplate>
                                                    <%--Update by Tina 2012/04/13--%>
                                                    <asp:DropDownList ID="drpProductType" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                    <%--End--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="12%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modelo">
                                                <ItemTemplate>
                        <asp:DropDownList ID="drpModelo" runat="server" Width="100%">
                        </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Marca">
                                                <ItemTemplate>
                        <asp:DropDownList ID="drpMarca" runat="server" Width="100%">
                        </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle Width="8%" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Color">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtColor" runat="server" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Peso">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPeso" runat="server" Width="60%"></asp:TextBox>
                                                 <asp:Label ID="lblPesoUnit" runat="server" Text="Kg"></asp:Label> <%--added by tina 2012/04/13--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="11%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Capacidad">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpCapacidad" runat="server"
                                                        Width="100%">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Antiguedad">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAntiguedad" runat="server" Width="50%"></asp:TextBox>&nbsp;Años
                                                </ItemTemplate>
                                                <ItemStyle Width="8%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
</asp:Panel>

    <asp:Panel ID="panelPrint" runat="server" >
    <table width="100%">
    <tr>
    <td>
    <div>
    <br>
    <asp:Image runat="server" ImageUrl="images/t11.png"/>
    </div>
    </td>
    </tr>
    </table>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayCreditCheckList" runat="server" Text="Check List de Crédito"
            CssClass="Button_1" OnClick="btnDisplayCreditCheckList_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayCreditContract" runat="server" Text="Contrato de Financiamiento"
            CssClass="Button_1" OnClick="btnDisplayCreditContract_Click" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayEquipmentAct" runat="server" Text="Acta Entrega - Recepción de Equipos" 
            CssClass="Button_1" OnClick="btnDisplayEquipmentAct_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayGuaranteeEndorsement" runat="server" Text="Endoso en Garantía"
            CssClass="Button_1" OnClick="btnDisplayGuaranteeEndorsement_Click" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayPromissoryNote" runat="server" Text="Pagaré" 
            CssClass="Button_1" OnClick="btnDisplayPromissoryNote_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayGuarantee" runat="server" Text="Carta Compromiso Obligado Solidario" 
            CssClass="Button_1" OnClick="btnDisplayGuarantee_Click" /> 
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayQuota1" runat="server" Text="Carta Presupuesto de Inversión"
            CssClass="Button_1" OnClick="btnDisplayQuota1_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayDisposalBonusReceipt" runat="server" Text="Recibo de Incentivo Energético (Descuento)" Enabled="false"
            CssClass="Button_1" OnClick="btnDisplayDisposalBonusReceipt_Click" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="Button1" runat="server" Text="Tabla de Amortización"
            CssClass="Button_1" OnClick="btnDisplayRepaymentSchedule_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       	<asp:Button runat="server" Text="Tabla de Amortización - Pagaré" 
       	    CssClass="Button_1" ID="BtnAmortPag" OnClick="btnAmortPag_Click" />
       	<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDisplayReceiptToSettle" runat="server" Text="Pre-Boleta CAyD" Enabled="false"
            CssClass="Button_1" OnClick="btnDisplayReceiptToSettle_Click" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnBoletaBajaEficiencia" runat="server" Text="Boleta Ingreso Equipo" Enabled="false"
            CssClass="Button_1" onclick="btnBoletaBajaEficiencia_Click" 
            Visible="False"  />
        <br />    
        
    </asp:Panel>
    <br />

    <table width="100%">
    <tr>
    <td>
    <div align="right">
        <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" 
            OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')" 
            onclick="btnCancel_Click" />
            &nbsp &nbsp<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button" 
            OnClientClick="return confirm('¿ Desea Regresar a la Pantalla Previa ?')" onclick="btnRegresar_Click"/>
    </div>
    </td>
    </tr>
    </table>
    </asp:Content>
