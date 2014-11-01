<%@ Page Language="C#"AutoEventWireup="true" CodeFile="WF_ConfigUsuario.aspx.cs" Inherits="WF_ConfigUsuario" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Exportar a NOI</title>
    <style type="text/css">
        .style1
        {
            height: 31px;
        }
        .style3
        {
            height: 274px;
        }
        .style4
        {
            width: 710px;
            height: 367px;
        }
        .style5
        {
            height: 36px;
            }
    </style>
</head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <form id="form1" runat="server" >
    <div>
    <igmisc:WebPanel ID="Wpn_EC_CONFIG_USUARIO_" runat="server" EnableAppStyling="True" 
            Height="385px" StyleSetName="Caribbean" Width="707px" >
        <Header Text="Edición de Configuración de Usuario">
        </Header>
        <Template>
            <table class="style4">
                <tr>
                    <td>
                        <asp:Label ID="Lbl_EC_CONFIG_USUARIO" runat="server" 
                            Text="Elija la configuración y de clic en Editar"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <igtbl:UltraWebGrid ID="Uwg_EC_CONFIG_USUARIO" runat="server" Browser="Xml" 
                            Height="273px" oninitializedatasource="Uwg_EC_CONFIG_USUARIO_InitializeDataSource" 
                            oninitializelayout="Uwg_EC_CONFIG_USUARIO_InitializeLayout" >
                            <Bands>
                                <igtbl:UltraGridBand>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                AllowUpdateDefault="Yes" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortMulti"
                                LoadOnDemand="Xml" Name="Grid" RowHeightDefault="20px" RowSelectorsDefault="No"
                                SelectTypeRowDefault="Single" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="273px"
                                    Width="100%">
                                </FrameStyle>
                                <Pager MinimumPagesForDisplay="2">
                                    
                                </Pager>
                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                </EditCellStyleDefault>
                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </FooterStyleDefault>
                                <HeaderStyleDefault BackgroundImage="images/GridTitulo.gif" BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </HeaderStyleDefault>
                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                    <Padding Left="3px" />
                                    <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                </RowStyleDefault>
                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                </GroupByRowStyleDefault>
                                <GroupByBox>
                                    <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                    </BoxStyle>
                                </GroupByBox>
                                <AddNewBox>
                                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" 
                                        BorderWidth="1px">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                            WidthTop="1px" />
                                    </BoxStyle>
                                </AddNewBox>
                                <ActivationObject BorderColor="" BorderWidth="">
                                </ActivationObject>
                                <FilterOptionsDefault>
                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px" Height="300px" Width="200px">
                                        <Padding Left="2px" />
                                    </FilterDropDownStyle>
                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                    </FilterHighlightRowStyle>
                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                        BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px">
                                        <Padding Left="2px" />
                                    </FilterOperandDropDownStyle>
                                </FilterOptionsDefault>
                            </DisplayLayout>
                        </igtbl:UltraWebGrid>
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        &nbsp; &nbsp; &nbsp;
                        <igtxt:WebImageButton ID="Btn_Editar" runat="server" Height="22px" 
                            ImageTextSpacing="4" OnClick="Btn_EditarPeriodo_Click" Text="Editar" 
                            UseBrowserDefaults="False" Width="100px">
                            <Alignments VerticalAll="Bottom" VerticalImage="Middle" />
                            <RoundedCorners DisabledImageUrl="ig_butXP5wh.gif" 
                                FocusImageUrl="ig_butXP3wh.gif" HoverImageUrl="ig_butXP2wh.gif" 
                                ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400" 
                                PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages" />
                            <Appearance>
                                <style cursor="Default">


                                </style>
                                <Image Height="16px" Url="./Imagenes/Edit.png" Width="16px" />
                            </Appearance>
                        </igtxt:WebImageButton>
                        </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Lbl_Error" runat="server" Font-Names="Arial Narrow" 
                            Font-Size="Smaller" ForeColor="Red" style="font-size: small"></asp:Label>
                        <asp:Label ID="Lbl_Correcto" runat="server" Font-Names="Arial Narrow" 
                            Font-Size="Small" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
            </table>
        </Template>
    </igmisc:WebPanel>
 </div>
    </form>
</body>
</html>
