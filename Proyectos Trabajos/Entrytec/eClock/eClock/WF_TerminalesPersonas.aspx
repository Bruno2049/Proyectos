<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="WF_TerminalesPersonas.aspx.cs" Inherits="WF_TerminalesPersonas" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Terminales</title>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
        <table width="600" style="height: 380px">
            <tr>
                <td align="left" style="font-size: 12px; font-style: italic; font-family: arial">
                    <table style="width:100%;">
                        <tr>
                            <td>
                    Asignando personas a la Terminal:</td>
                            <td>
                                <asp:Label ID="lnombre" runat="server" Width="383px"></asp:Label>
                               
                               
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 300px">
                    <igtbl:UltraWebGrid ID="Grid" runat="server" Browser="Xml" OnInitializeDataSource="Grid_InitializeDataSource"
                        OnInitializeLayout="Grid_InitializeLayout" Style="left: 0px; top: 0px" Width="100%" Height="100%">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowRowNumberingDefault="Continuous"
                            BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect" HeaderClickActionDefault="SortMulti"
                            LoadOnDemand="Xml" Name="Grid" RowHeightDefault="20px" RowSelectorsDefault="No"
                            RowsRange="30" ScrollBar="Always" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                            XmlLoadOnDemandType="Accumulative" AllowSortingDefault="Yes">
                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Width="100%"
                                Height="100%">
                            </FrameStyle>
                            <Pager MinimumPagesForDisplay="2">
                            </Pager>
                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                            </RowStyleDefault>
                            <ActivationObject BorderColor="" BorderWidth="">
                            </ActivationObject>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid>
            </tr>
            <tr>
                <td align="center" style="height: 36px">
                      <asp:Label ID="Lbl_Correcto" runat="server" ForeColor="Green"></asp:Label>
                      <asp:Label ID="Lbl_Error" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 36px">
                      <igtxt:WebImageButton ID="BtnSeleccionarTodos" runat="server" Height="22px" OnClick="BtnSeleccionarTodos_Click"
                        Text="Acceso a Todos" UseBrowserDefaults="False" Width="129px" 
                        ImageTextSpacing="4" 
                        ToolTip="Permite el acceso de todas las personas por la terminal">
                        <Appearance>
                            <Image Url="./Imagenes/CheckAll.png" Height="16px" Width="16px" />
                            <Style Cursor="Default"></Style>

<ButtonStyle Cursor="Default"></ButtonStyle>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                    </igtxt:WebImageButton>
                      
                    <igtxt:WebImageButton ID="BtnDeseleccionarTodos" runat="server" Height="22px" OnClick="BtnDeseleccionarTodos_Click"
                        Text="Denegar a todos" UseBrowserDefaults="False" Width="133px" 
                        ImageTextSpacing="4" ToolTip="Niega el acceso a todas las personas">
                        <Appearance>
                            <Image Url="./Imagenes/UncheckAll.png" Height="16px" Width="16px" />
                            <Style Cursor="Default"></Style>

<ButtonStyle Cursor="Default"></ButtonStyle>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                    </igtxt:WebImageButton>
                      <igtxt:WebImageButton ID="BtnPermitir" runat="server" Height="22px" OnClick="BtnPermitir_Click"
                        Text="Permitir" UseBrowserDefaults="False" Width="129px" 
                        ImageTextSpacing="4" 
                        ToolTip="Da acceso a las personas seleccionadas a checar por la terminal">
                        <Appearance>
                            <Image Url="./Imagenes/CheckAll.png" Height="16px" Width="16px" />
                            <Style Cursor="Default"></Style>

<ButtonStyle Cursor="Default"></ButtonStyle>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                    </igtxt:WebImageButton>
                      
                    <igtxt:WebImageButton ID="BtnDenegar" runat="server" Height="22px" OnClick="BtnDenegar_Click"
                        Text="Denegar" UseBrowserDefaults="False" Width="133px" 
                        ImageTextSpacing="4" ToolTip="Niega el acceso a las personas seleccionadas">
                        <Appearance>
                            <Image Url="./Imagenes/UncheckAll.png" Height="16px" Width="16px" />
                            <Style Cursor="Default"></Style>

<ButtonStyle Cursor="Default"></ButtonStyle>
                        </Appearance>
                        <RoundedCorners HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif"
                            MaxHeight="80" MaxWidth="400" PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif" />
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom" />
                    </igtxt:WebImageButton>
                </td>
            </tr>
        </table>
    
 </div>
    </form>
</body>
</html>
