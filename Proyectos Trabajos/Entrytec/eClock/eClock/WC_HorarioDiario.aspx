<%@ Page language="c#" CodeFile="WC_HorarioDiario.aspx.cs" AutoEventWireup="false" Inherits="eClock.WC_HorarioDiario" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WC_HorarioDiario</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server" style="FONT-SIZE: x-small; FONT-FAMILY: Arial">
			<igtxt:WebDateTimeEdit id="WebDateTimeEdit1" style="Z-INDEX: 100; LEFT: 64px; POSITION: absolute; TOP: 104px"
				runat="server" BorderStyle="Solid" BorderColor="#7F9DB9" BorderWidth="1px" Font-Size="8pt" Font-Names="Tahoma"
				DisplayModeFormat="t" EditModeFormat="t" UseBrowserDefaults="False" CellSpacing="1" Width="56px">
				<ButtonsAppearance CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
					CustomButtonImageUrl="ig_edit_0b.gif">
					<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
					<ButtonDisabledStyle BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
					<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
					<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
				</ButtonsAppearance>
				<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
					Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
					UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
			</igtxt:WebDateTimeEdit>
			<igtxt:WebDateTimeEdit id="WebDateTimeEdit7" style="Z-INDEX: 114; LEFT: 64px; POSITION: absolute; TOP: 8px"
				runat="server" BorderStyle="Solid" BorderColor="#7F9DB9" BorderWidth="1px" Font-Size="8pt" Font-Names="Tahoma"
				DisplayModeFormat="t" EditModeFormat="t" UseBrowserDefaults="False" CellSpacing="1" Width="56px">
				<ButtonsAppearance CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
					CustomButtonImageUrl="ig_edit_0b.gif">
					<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
					<ButtonDisabledStyle BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
					<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
					<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
				</ButtonsAppearance>
				<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
					Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
					UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
			</igtxt:WebDateTimeEdit>
			<asp:Label id="Label7" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server">Entrada</asp:Label>
			<igtxt:WebDateTimeEdit id="WebDateTimeEdit5" style="Z-INDEX: 108; LEFT: 64px; POSITION: absolute; TOP: 32px"
				runat="server" BorderStyle="Solid" BorderColor="#7F9DB9" BorderWidth="1px" Font-Size="8pt" Font-Names="Tahoma"
				DisplayModeFormat="t" EditModeFormat="t" UseBrowserDefaults="False" CellSpacing="1" Width="56px">
				<ButtonsAppearance CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
					CustomButtonImageUrl="ig_edit_0b.gif">
					<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
					<ButtonDisabledStyle BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
					<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
					<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
				</ButtonsAppearance>
				<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
					Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
					UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
			</igtxt:WebDateTimeEdit>
			<asp:Label id="Label5" style="Z-INDEX: 107; LEFT: 8px; POSITION: absolute; TOP: 128px" runat="server">Salida</asp:Label>
			<igtxt:WebDateTimeEdit id="WebDateTimeEdit4" style="Z-INDEX: 113; LEFT: 64px; POSITION: absolute; TOP: 80px"
				runat="server" BorderStyle="Solid" BorderColor="#7F9DB9" BorderWidth="1px" Font-Size="8pt" Font-Names="Tahoma"
				DisplayModeFormat="t" EditModeFormat="t" UseBrowserDefaults="False" CellSpacing="1" Width="56px">
				<ButtonsAppearance CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
					CustomButtonImageUrl="ig_edit_0b.gif">
					<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
					<ButtonDisabledStyle BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
					<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
					<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
				</ButtonsAppearance>
				<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
					Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
					UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
			</igtxt:WebDateTimeEdit>
			<asp:Label id="Label4" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 104px" runat="server">Regreso</asp:Label>
			<igtxt:WebDateTimeEdit id="WebDateTimeEdit3" style="Z-INDEX: 110; LEFT: 64px; POSITION: absolute; TOP: 56px"
				runat="server" BorderStyle="Solid" BorderColor="#7F9DB9" BorderWidth="1px" Font-Size="8pt" Font-Names="Tahoma"
				DisplayModeFormat="t" EditModeFormat="t" UseBrowserDefaults="False" CellSpacing="1" Width="56px">
				<ButtonsAppearance CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
					CustomButtonImageUrl="ig_edit_0b.gif">
					<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
					<ButtonDisabledStyle BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
					<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
					<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
				</ButtonsAppearance>
				<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
					Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
					UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
			</igtxt:WebDateTimeEdit>
			<asp:Label id="Label3" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 80px" runat="server">Comida</asp:Label>
			<igtxt:WebDateTimeEdit id="WebDateTimeEdit2" style="Z-INDEX: 109; LEFT: 64px; POSITION: absolute; TOP: 128px"
				runat="server" BorderStyle="Solid" BorderColor="#7F9DB9" BorderWidth="1px" Font-Size="8pt" Font-Names="Tahoma"
				DisplayModeFormat="t" EditModeFormat="t" UseBrowserDefaults="False" CellSpacing="1" Width="56px">
				<ButtonsAppearance CustomButtonDisabledImageUrl="ig_edit_01b.gif" CustomButtonDefaultTriangleImages="Arrow"
					CustomButtonImageUrl="ig_edit_0b.gif">
					<ButtonPressedStyle BackColor="#83A6F4"></ButtonPressedStyle>
					<ButtonDisabledStyle BorderColor="#E4E4E4" BackColor="#F1F1ED"></ButtonDisabledStyle>
					<ButtonStyle Width="13px" BorderWidth="1px" BorderColor="#ABC1F4" BorderStyle="Solid" BackColor="#C5D5FC"></ButtonStyle>
					<ButtonHoverStyle BackColor="#DCEDFD"></ButtonHoverStyle>
				</ButtonsAppearance>
				<SpinButtons DefaultTriangleImages="ArrowSmall" Width="15px" UpperButtonDisabledImageUrl="ig_edit_11b.gif"
					Display="OnRight" LowerButtonDisabledImageUrl="ig_edit_21b.gif" LowerButtonImageUrl="ig_edit_2b.gif"
					UpperButtonImageUrl="ig_edit_1b.gif"></SpinButtons>
			</igtxt:WebDateTimeEdit>
			<asp:Label id="Label2" style="Z-INDEX: 104; LEFT: 8px; POSITION: absolute; TOP: 56px" runat="server">E. Max</asp:Label>
			<asp:Label id="Label1" style="Z-INDEX: 106; LEFT: 8px; POSITION: absolute; TOP: 32px" runat="server">Retardo</asp:Label>
		</form>
	</body>
</HTML>
