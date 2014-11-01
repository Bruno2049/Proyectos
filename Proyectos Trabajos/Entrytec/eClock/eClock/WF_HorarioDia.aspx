<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_HorarioDia.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_HorarioDia" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    &nbsp;<igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
        borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
        stylesetname="PaneleClock">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial" __designer:dtid="1407374883553304">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150" __designer:dtid="1407374883553305"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px" __designer:dtid="1407374883553306"></Padding>
</PanelStyle>

<Header TextAlignment="Left" __designer:dtid="1407374883553307">
<ExpandedAppearance __designer:dtid="1407374883553308">
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True" __designer:dtid="1407374883553309">
<BorderDetails ColorTop="158, 190, 245" WidthBottom="0px" ColorLeft="158, 190, 245" ColorRight="0, 45, 150" __designer:dtid="1407374883553310"></BorderDetails>

<Padding Top="1px" Left="4px" Bottom="1px" __designer:dtid="1407374883553311"></Padding>
</Styles>
</ExpandedAppearance>

<HoverAppearance __designer:dtid="1407374883553312">
<Styles CssClass="igwpHeaderHoverBlue2k7" __designer:dtid="1407374883553313"></Styles>
</HoverAppearance>

<CollapsedAppearance __designer:dtid="1407374883553314">
<Styles CssClass="igwpHeaderCollapsedBlue2k7" __designer:dtid="1407374883553315"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="0px" Width="0px" __designer:dtid="1407374883553316"></ExpansionIndicator>
</Header>
<Template __designer:dtid="1407374883553317">
<BR /><TABLE style="FONT-SIZE: x-small; COLOR: saddlebrown; FONT-FAMILY: Arial; HEIGHT: 177px" id="Table1" cellSpacing=1 cellPadding=1 width=150 border=0 __designer:dtid="8725724278030337"><TBODY><TR __designer:dtid="8725724278030338"><TD __designer:dtid="8725724278030339"><asp:Label id="LEntrada" runat="server" Width="64px" __designer:dtid="8725724278030340" Height="8px" __designer:wfdid="w769">Entrada</asp:Label></TD><TD __designer:dtid="8725724278030341"><P __designer:dtid="8725724278030342"><igtxt:WebDateTimeEdit id="EEntrada" runat="server" BorderColor="#7F9DB9" Font-Names="Tahoma" BorderWidth="1px" BorderStyle="Solid" Width="80px" Font-Size="8pt" __designer:dtid="8725724278030343" Fields="2006-1-1-0-0-0-0" AutoPostBack="True" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="t" DisplayModeFormat="t" __designer:wfdid="w770">
									<ButtonsAppearance __designer:dtid="8725724278030344" CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
										CustomButtonImageUrl="ig_edit_0b.gif">
										<ButtonPressedStyle __designer:dtid="8725724278030345" BackColor="#83A6F4"></ButtonPressedStyle>
										<ButtonDisabledStyle __designer:dtid="8725724278030346" BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
										<ButtonStyle __designer:dtid="8725724278030347" Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
										<ButtonHoverStyle __designer:dtid="8725724278030348" BackColor="#DCEDFD"></ButtonHoverStyle>
									</ButtonsAppearance>
									<SpinButtons __designer:dtid="8725724278030349" DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
										Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
										UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
								</igtxt:WebDateTimeEdit></P></TD></TR><TR __designer:dtid="8725724278030350"><TD __designer:dtid="8725724278030351"><asp:Label id="LRetardo" runat="server" Width="64px" __designer:dtid="8725724278030352" Height="8px" __designer:wfdid="w771">Retardo</asp:Label></TD><TD __designer:dtid="8725724278030353"><P __designer:dtid="8725724278030354"><igtxt:WebDateTimeEdit id="ERetardo" runat="server" BorderColor="#7F9DB9" Font-Names="Tahoma" BorderWidth="1px" BorderStyle="Solid" Width="80px" Font-Size="8pt" __designer:dtid="8725724278030355" Fields="2006-1-1-0-0-0-0" AutoPostBack="True" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="t" DisplayModeFormat="t" __designer:wfdid="w772">
									<ButtonsAppearance __designer:dtid="8725724278030356" CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
										CustomButtonImageUrl="ig_edit_0b.gif">
										<ButtonPressedStyle __designer:dtid="8725724278030357" BackColor="#83A6F4"></ButtonPressedStyle>
										<ButtonDisabledStyle __designer:dtid="8725724278030358" BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
										<ButtonStyle __designer:dtid="8725724278030359" Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
										<ButtonHoverStyle __designer:dtid="8725724278030360" BackColor="#DCEDFD"></ButtonHoverStyle>
									</ButtonsAppearance>
									<SpinButtons __designer:dtid="8725724278030361" DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
										Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
										UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
								</igtxt:WebDateTimeEdit></P></TD></TR><TR __designer:dtid="8725724278030362"><TD __designer:dtid="8725724278030363"><P __designer:dtid="8725724278030364"><asp:Label id="LEMax" runat="server" Width="64px" __designer:dtid="8725724278030365" Height="8px" __designer:wfdid="w773">E. Max</asp:Label></P></TD><TD __designer:dtid="8725724278030366"><P __designer:dtid="8725724278030367"><igtxt:WebDateTimeEdit id="EEMax" runat="server" BorderColor="#7F9DB9" Font-Names="Tahoma" BorderWidth="1px" BorderStyle="Solid" Width="80px" Font-Size="8pt" __designer:dtid="8725724278030368" Fields="2006-1-1-0-0-0-0" AutoPostBack="True" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="t" DisplayModeFormat="t" __designer:wfdid="w774">
									<ButtonsAppearance __designer:dtid="8725724278030369" CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
										CustomButtonImageUrl="ig_edit_0b.gif">
										<ButtonPressedStyle __designer:dtid="8725724278030370" BackColor="#83A6F4"></ButtonPressedStyle>
										<ButtonDisabledStyle __designer:dtid="8725724278030371" BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
										<ButtonStyle __designer:dtid="8725724278030372" Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
										<ButtonHoverStyle __designer:dtid="8725724278030373" BackColor="#DCEDFD"></ButtonHoverStyle>
									</ButtonsAppearance>
									<SpinButtons __designer:dtid="8725724278030374" DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
										Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
										UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
								</igtxt:WebDateTimeEdit></P></TD></TR><TR __designer:dtid="8725724278030375"><TD __designer:dtid="8725724278030376"><asp:Label id="LComida" runat="server" Width="64px" __designer:dtid="8725724278030377" Height="8px" __designer:wfdid="w775">Comida</asp:Label></TD><TD __designer:dtid="8725724278030378"><P __designer:dtid="8725724278030379"><igtxt:WebDateTimeEdit id="EComida" runat="server" BorderColor="#7F9DB9" Font-Names="Tahoma" BorderWidth="1px" BorderStyle="Solid" Width="80px" Font-Size="8pt" __designer:dtid="8725724278030380" Fields="2006-1-1-0-0-0-0" AutoPostBack="True" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="t" DisplayModeFormat="t" __designer:wfdid="w776">
									<ButtonsAppearance __designer:dtid="8725724278030381" CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
										CustomButtonImageUrl="ig_edit_0b.gif">
										<ButtonPressedStyle __designer:dtid="8725724278030382" BackColor="#83A6F4"></ButtonPressedStyle>
										<ButtonDisabledStyle __designer:dtid="8725724278030383" BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
										<ButtonStyle __designer:dtid="8725724278030384" Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
										<ButtonHoverStyle __designer:dtid="8725724278030385" BackColor="#DCEDFD"></ButtonHoverStyle>
									</ButtonsAppearance>
									<SpinButtons __designer:dtid="8725724278030386" DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
										Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
										UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
								</igtxt:WebDateTimeEdit></P></TD></TR><TR __designer:dtid="8725724278030387"><TD __designer:dtid="8725724278030388"><asp:Label id="LRegreso" runat="server" Width="64px" __designer:dtid="8725724278030389" Height="16" __designer:wfdid="w777">Regreso</asp:Label></TD><TD __designer:dtid="8725724278030390"><P __designer:dtid="8725724278030391"><igtxt:WebDateTimeEdit id="ERegreso" runat="server" BorderColor="#7F9DB9" Font-Names="Tahoma" BorderWidth="1px" BorderStyle="Solid" Width="80px" Font-Size="8pt" __designer:dtid="8725724278030392" Fields="2006-1-1-0-0-0-0" AutoPostBack="True" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="t" DisplayModeFormat="t" __designer:wfdid="w778">
									<ButtonsAppearance __designer:dtid="8725724278030393" CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
										CustomButtonImageUrl="ig_edit_0b.gif">
										<ButtonPressedStyle __designer:dtid="8725724278030394" BackColor="#83A6F4"></ButtonPressedStyle>
										<ButtonDisabledStyle __designer:dtid="8725724278030395" BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
										<ButtonStyle __designer:dtid="8725724278030396" Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
										<ButtonHoverStyle __designer:dtid="8725724278030397" BackColor="#DCEDFD"></ButtonHoverStyle>
									</ButtonsAppearance>
									<SpinButtons __designer:dtid="8725724278030398" DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
										Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
										UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
								</igtxt:WebDateTimeEdit></P></TD></TR><TR __designer:dtid="8725724278030399"><TD __designer:dtid="8725724278030400"><P __designer:dtid="8725724278030401"><asp:Label id="LSalida" runat="server" Width="64px" __designer:dtid="8725724278030402" Height="8px" __designer:wfdid="w779">Salida</asp:Label></P></TD><TD __designer:dtid="8725724278030403"><P __designer:dtid="8725724278030404"><igtxt:WebDateTimeEdit id="ESalida" runat="server" BorderColor="#7F9DB9" Font-Names="Tahoma" BorderWidth="1px" BorderStyle="Solid" Width="80px" Font-Size="8pt" __designer:dtid="8725724278030405" Fields="2006-1-1-0-0-0-0" AutoPostBack="True" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="t" DisplayModeFormat="t" __designer:wfdid="w780">
									<ButtonsAppearance __designer:dtid="8725724278030406" CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
										CustomButtonImageUrl="ig_edit_0b.gif">
										<ButtonPressedStyle __designer:dtid="8725724278030407" BackColor="#83A6F4"></ButtonPressedStyle>
										<ButtonDisabledStyle __designer:dtid="8725724278030408" BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
										<ButtonStyle __designer:dtid="8725724278030409" Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
										<ButtonHoverStyle __designer:dtid="8725724278030410" BackColor="#DCEDFD"></ButtonHoverStyle>
									</ButtonsAppearance>
									<SpinButtons __designer:dtid="8725724278030411" DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
										Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
										UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
								</igtxt:WebDateTimeEdit></P></TD></TR><TR __designer:dtid="8725724278030412"><TD __designer:dtid="8725724278030413"><P __designer:dtid="8725724278030414"><asp:Label id="LBloque" runat="server" Width="64px" __designer:dtid="8725724278030415" Height="8px" ToolTip="Rango de Entradas" __designer:wfdid="w781"> Bloque</asp:Label></P></TD><TD __designer:dtid="8725724278030416"><P __designer:dtid="8725724278030417"><igtxt:WebDateTimeEdit id="EBloque" runat="server" BorderColor="#7F9DB9" Font-Names="Tahoma" BorderWidth="1px" BorderStyle="Solid" Width="80px" Font-Size="8pt" __designer:dtid="8725724278030418" Fields="2006-1-1-0-0-0-0" AutoPostBack="True" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="t" DisplayModeFormat="t" ToolTip="Rango de Entrada" __designer:wfdid="w782">
									<ButtonsAppearance __designer:dtid="8725724278030419" CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
										CustomButtonImageUrl="ig_edit_0b.gif">
										<ButtonPressedStyle __designer:dtid="8725724278030420" BackColor="#83A6F4"></ButtonPressedStyle>
										<ButtonDisabledStyle __designer:dtid="8725724278030421" BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
										<ButtonStyle __designer:dtid="8725724278030422" Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
										<ButtonHoverStyle __designer:dtid="8725724278030423" BackColor="#DCEDFD"></ButtonHoverStyle>
									</ButtonsAppearance>
									<SpinButtons __designer:dtid="8725724278030424" DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
										Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
										UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
								</igtxt:WebDateTimeEdit></P></TD></TR><TR __designer:dtid="8725724278030425"><TD __designer:dtid="8725724278030426"><P __designer:dtid="8725724278030427"><asp:Label id="LHTrabajo" runat="server" Width="64px" __designer:dtid="8725724278030428" Height="8px" ToolTip="Horas de Trabajo" __designer:wfdid="w783">H. Trabajo</asp:Label></P></TD><TD __designer:dtid="8725724278030429"><P __designer:dtid="8725724278030430"><igtxt:WebDateTimeEdit id="EHTrabajo" runat="server" BorderColor="#7F9DB9" Font-Names="Tahoma" BorderWidth="1px" BorderStyle="Solid" Width="80px" Font-Size="8pt" __designer:dtid="8725724278030431" Fields="2006-1-1-0-0-0-0" AutoPostBack="True" UseBrowserDefaults="False" CellSpacing="1" EditModeFormat="t" DisplayModeFormat="t" ToolTip="Horas de Trabajo" __designer:wfdid="w784">
									<ButtonsAppearance __designer:dtid="8725724278030432" CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
										CustomButtonImageUrl="ig_edit_0b.gif">
										<ButtonPressedStyle __designer:dtid="8725724278030433" BackColor="#83A6F4"></ButtonPressedStyle>
										<ButtonDisabledStyle __designer:dtid="8725724278030434" BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
										<ButtonStyle __designer:dtid="8725724278030435" Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
										<ButtonHoverStyle __designer:dtid="8725724278030436" BackColor="#DCEDFD"></ButtonHoverStyle>
									</ButtonsAppearance>
									<SpinButtons __designer:dtid="8725724278030437" DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
										Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
										UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
								</igtxt:WebDateTimeEdit></P></TD></TR></TBODY></TABLE>
</Template>
</igmisc:webpanel>
</asp:Content>