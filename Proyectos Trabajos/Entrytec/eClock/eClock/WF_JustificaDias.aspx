<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_JustificaDias.aspx.cs" Inherits="WF_JustificaDias" %>

<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Justificar Día</title>
</head>
<body style="font-size: 11px; margin: 10px; font-family: tahoma; text-align: center">
    <form id="form1" runat="server">
        &nbsp;<igmisc:webpanel id="WebPanel2" runat="server" EnableAppStyling="True" 
            StyleSetName="Caribbean">


<Header Text="&amp;nbsp;" TextAlignment="Left" __designer:dtid="4503599627370539">


<ExpansionIndicator Height="0px" __designer:dtid="4503599627370548"></ExpansionIndicator>
</Header>
<Template __designer:dtid="4503599627370549">
<TABLE style="WIDTH: 380px" __designer:dtid="4222124650659845"><TBODY><TR __designer:dtid="4222124650659846"><TD colSpan=2 __designer:dtid="4222124650659847"><STRONG __designer:dtid="4222124650659848"><SPAN style="FONT-SIZE: 16pt" __designer:dtid="4222124650659849">Justificación de días</SPAN></STRONG></TD></TR><TR __designer:dtid="4222124650659850"><TD colSpan=2 __designer:dtid="4222124650659851"></TD></TR><TR __designer:dtid="4222124650659852"><TD style="WIDTH: 555px; HEIGHT: 18px; TEXT-ALIGN: left" __designer:dtid="4222124650659853"><SPAN style="FONT-SIZE: 10pt" __designer:dtid="4222124650659854">Tipo de Justificacion</SPAN></TD><TD style="WIDTH: 300px; HEIGHT: 18px; TEXT-ALIGN: left" __designer:dtid="4222124650659855">
    <igcmbo:WebCombo id="Cbx_TipoIncidencia" __designer:dtid="4222154715430912" 
        runat="server" SelForeColor="" Width="231px" Version="3.00" 
        SelBackColor="17, 69, 158" 
        OnInitializeLayout="Cbx_TipoIncidencia_InitializeLayout" ForeColor="Black" 
        Font-Names="Arial Narrow"     DataValueField="TIPO_INCIDENCIA_ID" 
        DataTextField="TIPO_INCIDENCIA_NOMBRE" BorderWidth="1px" BorderStyle="Solid" 
        BorderColor="LightGray" BackColor="White" __designer:wfdid="w78"><Columns __designer:dtid="4222154715430913">
<igtbl:UltraGridColumn>
<Header Caption="Column0"></Header>
</igtbl:UltraGridColumn>
</Columns>

<ExpandEffects ShadowColor="LightGray" __designer:dtid="4222154715430925"></ExpandEffects>

<DropDownLayout BorderCollapse="Separate" ColWidthDefault="150px" RowHeightDefault="20px" Version="3.00" BaseTableName="" __designer:dtid="4222154715430926">
<FrameStyle Cursor="Default" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" Font-Names="Verdana" Font-Size="8pt" ForeColor="#759AFD" Height="130px" Width="325px" __designer:dtid="4222154715430927"></FrameStyle>

<HeaderStyle  BackColor="Navy" BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" ForeColor="White" __designer:dtid="4222154715430928">
<BorderDetails WidthLeft="0px" WidthTop="0px" WidthRight="1px" WidthBottom="1px" __designer:dtid="4222154715430929"></BorderDetails>
</HeaderStyle>

<RowAlternateStyle BackColor="WhiteSmoke" ForeColor="Black" __designer:dtid="4222154715430930"></RowAlternateStyle>

<RowStyle BackColor="CornflowerBlue" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" ForeColor="White" __designer:dtid="4222154715430931">
<Padding Left="3px" __designer:dtid="4222154715430932"></Padding>

<BorderDetails WidthLeft="0px" WidthTop="0px" __designer:dtid="4222154715430933"></BorderDetails>
</RowStyle>

<SelectedRowStyle  BackColor="Sienna" ForeColor="WhiteSmoke" __designer:dtid="4222154715430934"></SelectedRowStyle>
</DropDownLayout>
</igcmbo:WebCombo> </TD></TR><TR __designer:dtid="4222124650659856"><TD style="WIDTH: 555px; TEXT-ALIGN: left" __designer:dtid="4222124650659857"><SPAN style="FONT-SIZE: 10pt" __designer:dtid="4222124650659858">Motivo o comentario</SPAN></TD><TD style="WIDTH: 300px; TEXT-ALIGN: left" __designer:dtid="4222124650659859"><igtxt:WebTextEdit id="txt_TipoIncidenciaMotivo" __designer:dtid="4222128945627136" runat="server" Width="231px" Font-Names="Arial Narrow" BorderWidth="1px" BorderStyle="Solid" BorderColor="#7F9DB9" UseBrowserDefaults="False" CellSpacing="1" __designer:wfdid="w80">
                        <ButtonsAppearance __designer:dtid="4222128945627137" CustomButtonDefaultTriangleImages="Arrow">
                            <ButtonStyle __designer:dtid="4222128945627138" BackColor="#C5D5FC" BorderColor="#ABC1F4" BorderStyle="Solid" BorderWidth="1px"
                                ForeColor="#506080" Width="13px">
                            </ButtonStyle>
                            <ButtonHoverStyle __designer:dtid="4222128945627139" BackColor="#DCEDFD">
                            </ButtonHoverStyle>
                            <ButtonPressedStyle __designer:dtid="4222128945627140" BackColor="#83A6F4">
                            </ButtonPressedStyle>
                            <ButtonDisabledStyle __designer:dtid="4222128945627141" BackColor="#E1E1DD" BorderColor="#D7D7D7" ForeColor="#BEBEBE">
                            </ButtonDisabledStyle>
                        </ButtonsAppearance>
                    </igtxt:WebTextEdit> <asp:RequiredFieldValidator id="RequiredFieldValidator1" __designer:dtid="4222137535561728" runat="server" ErrorMessage="(*) Se requiere introducir el motivo o comentario" ControlToValidate="txt_TipoIncidenciaMotivo" __designer:wfdid="w81"></asp:RequiredFieldValidator></TD></TR><TR __designer:dtid="4222137535561729"><TD style="TEXT-ALIGN: center" colSpan=2 __designer:dtid="4222137535561730"><asp:Label id="LCorrecto" __designer:dtid="4222137535561731" runat="server" ForeColor="Green" Font-Names="Arial Narrow" __designer:wfdid="w82"></asp:Label><asp:Label id="LError" __designer:dtid="4222137535561732" runat="server" ForeColor="Red" Font-Names="Arial Narrow" __designer:wfdid="w83"></asp:Label></TD></TR><TR __designer:dtid="4222137535561733"><TD style="TEXT-ALIGN: center" colSpan=2 __designer:dtid="4222137535561734"><igtxt:WebImageButton id="BGuardarCambios" onclick="BGuardarCambios_Click" __designer:dtid="4222137535561735" runat="server" Width="150px" UseBrowserDefaults="False" Text="Guardar Cambios" Height="22px" __designer:wfdid="w84">
                        <Alignments __designer:dtid="4222137535561736" VerticalAll="Bottom" VerticalImage="Middle"  />
                        <RoundedCorners __designer:dtid="4222137535561737" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"
                            HoverImageUrl="ig_butXP2wh.gif" ImageUrl="ig_butXP1wh.gif" MaxHeight="80" MaxWidth="400"
                            PressedImageUrl="ig_butXP4wh.gif" RenderingType="FileImages"  />
                        <Appearance __designer:dtid="4222137535561738">
                            <Style __designer:dtid="4222137535561739" Cursor="Default">
									</Style>
                            <Image __designer:dtid="4222137535561740" Height="16px" Url="./Imagenes/Save_as.png" Width="16px"  />
                        </Appearance>
                    </igtxt:WebImageButton> </TD></TR><TR __designer:dtid="4222137535561741"><TD style="WIDTH: 555px" __designer:dtid="4222137535561742"></TD><TD style="WIDTH: 300px" __designer:dtid="4222137535561743"></TD></TR><TR __designer:dtid="4222137535561744"><TD colSpan=2 __designer:dtid="4222137535561745"></TD></TR><TR __designer:dtid="4222137535561746"><TD colSpan=2 __designer:dtid="4222137535561747"></TD></TR></TBODY></TABLE>&nbsp;
</Template>
</igmisc:webpanel>

    </form>
</body>
</html>
