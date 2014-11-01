<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Page language="c#"  CodeFile="WF_Terminales.aspx.cs" AutoEventWireup="true" Inherits="WF_Terminales" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.DocumentExport.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid.DocumentExport" TagPrefix="igtbldocexp" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Terminales</title>
	<style type="text/css">
		html, body, #wrapper, #Form1
		{
			border-style: none;
			border-color: inherit;
			border-width: medium;
			width: 100%;
			margin: 0;
			padding: 0;
			text-align: center;
			overflow: hidden;
		}
	    .style1
        {
            width: 920px;
        }
        .style40
        {
            font-family: "Segoe UI";
            font-size: small;
        }
	    .style42
        {
            height: 0%;
            width: -32;
        }
        .style43
        {
            height: 0%;
            width: 42px;
        }
        .style48
        {
            height: 0%;
            width: 41px;
        }
	</style>
</head>
<body style="font-size: small; font-family: 'Segoe UI'; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table id="wrapper" cellspacing="0" border="0" class="style1">
				<tr>
					<td style="HEIGHT: 0%" align="right" colspan="22">
						<asp:CheckBox id="Chb_EC_TERMINALES" runat="server" AutoPostBack="true" Text="Ver Terminales Borradas"
							Font-Size="Small" Font-Names="Arial Narrow" 
                            oncheckedchanged="TerminalesCheckBox1_CheckedChanged" 
                            style="font-family: 'Segoe UI'"></asp:CheckBox></td>
				</tr>
				<tr>
					<td align="center" style="height: 300px" colspan="22" >
                        <igtbl:UltraWebGrid ID="Uwg_EC_TERMINALES" runat="server" Browser="Xml" 
                            OnInitializeLayout="Grid_InitializeLayout" 
                            OnInitializeDataSource="Grid_InitializeDataSource" Width="100%" 
                            Height="350px" oninitializerow="Grid_InitializeRow">
                            <Bands>
                                <igtbl:UltraGridBand>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                AllowUpdateDefault="Yes" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortMulti"
                                LoadOnDemand="Xml" Name="Grid" RowHeightdefault="20px" RowSelectorsDefault="No"
                                SelectTypeRowDefault="Single" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="true"
                                tableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                <GroupByBox>
                                    <Style BackColor="ActiveBorder" BorderColor="Window"></Style>
                                    <BoxStyle BackColor="ActiveBorder" BorderColor="Window"></BoxStyle>
                                </GroupByBox>
                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                </GroupByRowStyleDefault>
                                <ActivationObject BorderColor="" BorderWidth="">
                                </ActivationObject>
                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </FooterStyleDefault>
                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                    <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                    <Padding Left="3px" />
                                </RowStyleDefault>
                                <FilterOptionsDefault>
                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                        BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px">
                                        <Padding Left="2px" />
                                    </FilterOperandDropDownStyle>
                                    <FilterHighlightrowStyle BackColor="#151C55" ForeColor="White">
                                    </FilterHighlightrowStyle>
                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px" Height="300px" Width="200px">
                                        <Padding Left="2px" />
                                    </FilterDropDownStyle>
                                </FilterOptionsDefault>
                                <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </HeaderStyleDefault>
                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                </EditCellStyleDefault>
                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt"
                                    Width="100%" Height="350px">
                                </FrameStyle>
                                <Pager MinimumPagesForDisplay="2">
                                    
                                </Pager>
                                <AddNewBox>
                                    <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                    </Style>
                                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" BorderStyle="Solid">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
                                    </BoxStyle>
                                </AddNewBox>
                            </DisplayLayout>
                        </igtbl:UltraWebGrid>
                    </td>
				</tr>
				<tr>
					<td style="HEIGHT: 0%" align="center" colspan="22"><asp:label id="Lbl_Error" runat="server" 
                            Font-Size="X-Small" Font-Names="Segoe UI" ForeColor="Red" CssClass="style40"></asp:label>
                        <asp:label id="Lbl_Correcto" runat="server" Font-Size="X-Small" 
                            Font-Names="Segoe UI" ForeColor="Green" CssClass="style40"></asp:label>
                    </td>
				</tr>
				<tr>
					<td style="HEIGHT: 0%" align="center" colspan="2">
						&nbsp;</td>
					<td style="HEIGHT: 0%" align="center" colspan="2">
						&nbsp;</td>
					<td style="HEIGHT: 0%" align="center" colspan="2">
						&nbsp;</td>
					<td style="HEIGHT: 0%" align="center" colspan="2">
						&nbsp;</td>
					<td align="center" class="style43">
						&nbsp;</td>
					<td align="center" class="style43">
						<igtxt:webimagebutton id="WIBtn_Borrar" runat="server" Height="24px" 
                            Text="Borrar" UseBrowserDefaults="False" Width="100px" 
                            OnClick="btn_BorrarTerminal_Click" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" HorizontalAll="Center"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif" RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Delete.png" Height="16px" Width="16px"></Image>
                                <ButtonStyle Cursor="Default"></ButtonStyle>
							</Appearance>
						</igtxt:webimagebutton>
                    </td>
					<td style="HEIGHT: 0%" align="center">
						<igtxt:webimagebutton id="WIBtn_Editar" runat="server" Height="24px" Text="Editar" UseBrowserDefaults="False"
							Width="100px" OnClick="btn_EditarTerminal_Click" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" HorizontalAll="Center"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/Edit.png" Height="16px" Width="16px"></Image>
                                <ButtonStyle Cursor="Default"></ButtonStyle>
							</Appearance>
						</igtxt:webimagebutton>
                    </td>
					<td style="HEIGHT: 0%" align="center">
						<igtxt:webimagebutton id="WIBtn_Nuevo" runat="server" Height="24px" Text="Nueva" UseBrowserDefaults="False"
							Width="100px" OnClick="btn_AgregarTerminal_Click" ImageTextSpacing="4">
							<Alignments VerticalImage="Middle" HorizontalAll="Center"></Alignments>
							<RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
								RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
							<Appearance>
								<Style Cursor="Default">
								</Style>
								<Image Url="./Imagenes/New.png" Height="16px" Width="16px"></Image>

<ButtonStyle Cursor="Default"></ButtonStyle>
							</Appearance>
						</igtxt:webimagebutton>
                    </td>
					<td style="HEIGHT: 0%" align="center" colspan="2">
						&nbsp;</td>
					<td style="HEIGHT: 0%" align="center" colspan="2">
						&nbsp;</td>
					<td style="HEIGHT: 0%" align="center" colspan="2">
						&nbsp;</td>
					<td style="HEIGHT: 0%" align="center" colspan="2">
						&nbsp;</td>
					<td style="HEIGHT: 0%" align="center" colspan="2">
						&nbsp;</td>
				</tr>
				<tr>
					<td style="HEIGHT: 0%" align="center" colspan="22">&nbsp;</td>
				</tr>
				<tr>
					<td align="center" class="style48">
                        &nbsp;</td>
					<td align="center" class="style48">
                        &nbsp;</td>
					<td align="center" class="style48">
                        &nbsp;</td>
					<td align="center" class="style48">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style42" colspan="2">
                        <igtxt:WebImageButton ID="WIBtn_Buscar"
                            runat="server" Height="24px"  Text="Buscar Terminales"
                            UseBrowserDefaults="False" Width="152px" OnClick="btn_Buscar_Click" 
                                        ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" HorizontalAll="Center" />
                            <Appearance>
                                <Style Cursor="Default"></Style>
                                <Image Url="./Imagenes/Search.png" Height="16px" Width="16px" />
                                <ButtonStyle Cursor="Default"></ButtonStyle>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                    HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                    PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        </igtxt:WebImageButton>
                    </td>
					<td align="center" class="style43">
                        <igtxt:WebImageButton ID="WIBtn_AccesoPersonas"
                            runat="server" Height="24px" OnClick="btn_AccesoPersonas_Click" Text="Acceso de Personas"
                            UseBrowserDefaults="False" Width="164px" ImageTextSpacing="4">
                            <Alignments VerticalAll="Middle" HorizontalAll="Center" />
                            <Appearance>
                                <Style Cursor="Default"></Style>
                                <Image Url="./Imagenes/selecall.png" Height="16px" Width="16px" />
                                <ButtonStyle Cursor="Default"></ButtonStyle>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        </igtxt:WebImageButton>
                    </td>
					<td align="center" class="style43">
                        <igtxt:webimagebutton id="WIBtn_Imprimir" runat="server" Height="24px" Text="Imprimir" UseBrowserDefaults="False"
							Width="100px" OnClick="btImprimir_Click" ImageTextSpacing="4">
                            <Alignments VerticalImage="Middle" />
                            <Appearance>
                                <Image Height="16px" Url="./Imagenes/printer-inkjet.png" Width="16px" />
                                <ButtonStyle Cursor="Default">
                                </ButtonStyle>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        </igtxt:WebImageButton>
                    </td>
					<td align="center" class="style43">
                        <igtxt:webimagebutton id="WIBtn_MostrarLog" runat="server" Height="24px" Text="Mostrar Log" UseBrowserDefaults="False"
							Width="100px" OnClick="Btn_MostrarLog_Click" ImageTextSpacing="4">
                            <Alignments VerticalAll="Middle" HorizontalAll="Center" />
                            <Appearance>
                                <Image Height="16px" Width="16px" />
                                <ButtonStyle Cursor="Default">
                                </ButtonStyle>
                            </Appearance>
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                                HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                        </igtxt:WebImageButton>
                    </td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
					<td align="center" class="style43">
                        &nbsp;</td>
				</tr>
				<tr>
					<td align="center" colspan="22" >
 </div>
        <igtbldocexp:ultrawebgriddocumentexporter id="GridExporter" runat="server" OnBeginExport="GridExporter_BeginExport">
        </igtbldocexp:ultrawebgriddocumentexporter>
    </form>
</body>
</html>

