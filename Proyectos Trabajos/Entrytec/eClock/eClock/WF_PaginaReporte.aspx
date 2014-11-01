<%@ Register TagPrefix="uc1"  TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Page language="c#" MasterPageFile="~/MasterPage.master"  CodeFile="WF_PaginaReporte.aspx.cs" AutoEventWireup="True" Inherits="eClock.WF_PaginaReporte" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="PdfViewer CS 2k3" Namespace="PdfViewer" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD style="width: 933px; height: 350px;">
                        
<Template>
<cc1:ShowPdf id="ShowPdf1" runat="server" BorderWidth="2px" BorderStyle="None" Width="100%" Height="100%" FilePath="fw9.pdf"></cc1:ShowPdf>
</Template>
                        <br />
                        <igmisc:webpanel id="WebPanel2" runat="server" backcolor="White" bordercolor="SteelBlue"
                            borderstyle="Outset" borderwidth="2px" font-bold="False" font-names="ARIAL" forecolor="Black"
                            stylesetname="PaneleClock" width="100%" Visible="False">
<PanelStyle BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" Font-Names="Arial">
<BorderDetails ColorTop="0, 45, 150" ColorLeft="158, 190, 245" ColorBottom="0, 45, 150" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="5px" Left="5px" Bottom="5px" Right="5px"></Padding>
</PanelStyle>

<Header TextAlignment="Left">
<ExpandedAppearance>
<Styles BackgroundImage="./images/GridTitulo.gif" BorderStyle="Ridge" ForeColor="Black" BorderWidth="1px" BorderColor="Transparent" Height="15px" Font-Size="9pt" Font-Names="Arial" Font-Bold="True">
<BorderDetails ColorTop="158, 190, 245" WidthBottom="0px" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"></BorderDetails>

<Padding Top="1px" Left="4px" Bottom="1px"></Padding>
</Styles>
</ExpandedAppearance>

<HoverAppearance>
<Styles CssClass="igwpHeaderHoverBlue2k7"></Styles>
</HoverAppearance>

<CollapsedAppearance>
<Styles CssClass="igwpHeaderCollapsedBlue2k7"></Styles>
</CollapsedAppearance>

<ExpansionIndicator Height="10px" Width="10px"></ExpansionIndicator>
</Header>
<Template>
&nbsp;
</Template>
</igmisc:webpanel>
                    </TD>
				</TR>
			</TABLE>
</asp:Content>