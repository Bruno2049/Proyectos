<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="uc1" TagName="WC_Menu" Src="WC_Menu.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Page Language="c#"   CodeFile="WF_EmpleadosFil.aspx.cs" AutoEventWireup="True"
    Inherits="WF_EmpleadosFil" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="aspwe" %>
<%@ Register Assembly="Infragistics2.WebUI.WebSchedule.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="ig_sched" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eClock</title>
    <style type="text/css">
        .style2
        {
            width: 587px;
        }
        .style5
        {
            width: 254px;
        }
        .style6
        {
            width: 255px;
        }
    </style>
    </head>
<body style="font-size: 11px; font-family: tahoma; text-align: center; margin: 0px; ">
    <script id="igClientScript" type="text/javascript">
<!--

        function txtNoEmpleado_KeyDown(oEdit, keyCode, oEvent) {
            //Add code to handle your event here.
            document.getElementById('WebPanel2_WebGroupBox1_RBCampos_0').checked = true;
        }

        function txtNombre_KeyDown(oEdit, keyCode, oEvent) {
            //Add code to handle your event here.
            document.getElementById('WebPanel2_WebGroupBox1_RBCampos_1').checked = true;
        }

        function WCGrupo_AfterSelectChange(webComboId) {
            //Add code to handle your event here.
            document.getElementById('WebPanel2_WebGroupBox1_RBCampos_2').checked = true;
        }

        function WCGrupo2_BeforeSelectChange(webComboId) {
            //Add code to handle your event here.
            document.getElementById('WebPanel2_WebGroupBox1_RBCampos_3').checked = true;
        }

        function WCGrupo3_BeforeSelectChange(webComboId) {
            //Add code to handle your event here.
            document.getElementById('_ctl0_ContentPlaceHolder1_WebPanel2_WebGroupBox1_RBCampos_4').checked = true;
        }
// -->
</script>
    <form id="form1" runat="server">
    <div>
        <table style="height: 400px" width="710">
            <tr>
                <td align="center" style="background-position: right center; background-image: url(./Imagenes/fondoeClock3.jpg);
                    background-repeat: no-repeat; text-align: center;">
                    <table style="text-align: center;" class="style2">
                        <tr>
                            <td class="style5">
                                <strong>
                                    <asp:Label ID="Lbl_Desde" runat="server" Text="Desde" Visible="False"></asp:Label></strong></td>
                            <td class="style6">
                                <strong>
                                    <asp:Label ID="Lbl_Hasta" runat="server" Text="Hasta" Visible="False"></asp:Label></strong></td>
                        </tr>
                        <tr>
                            <td class="style5" >
                                <igsch:WebCalendar ID="WbCal_FIL_FECHA" runat="server" Visible="False">
                                    <Layout DayNameFormat="FirstLetter" FooterFormat="" ShowFooter="False" ShowTitle="False">
                                        <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                                        <WeekendDayStyle BackColor="#E0E0E0" />
                                        <SelectedDayStyle BackColor="CornflowerBlue" />
                                        <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                                        <OtherMonthDayStyle ForeColor="#ACA899" />
                                        <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                                        <TitleStyle BackColor="#9EBEF5" />
                                        <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False">
                                        </CalendarStyle>
                                    </Layout>
                                </igsch:WebCalendar>
                            </td>
                            <td class="style6">
                                <igsch:WebCalendar ID="WbCal_H_FIL_FECHA" runat="server" Visible="False">
                                    <Layout DayNameFormat="FirstLetter" FooterFormat="" ShowFooter="False" ShowTitle="False">
                                        <TodayDayStyle BackColor="Transparent" BorderColor="CornflowerBlue" />
                                        <WeekendDayStyle BackColor="#E0E0E0" />
                                        <SelectedDayStyle BackColor="CornflowerBlue" />
                                        <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                                        <OtherMonthDayStyle ForeColor="#ACA899" />
                                        <DayHeaderStyle BackColor="#7A96DF" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" />
                                        <TitleStyle BackColor="#9EBEF5" />
                                        <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False">
                                        </CalendarStyle>
                                    </Layout>
                                </igsch:WebCalendar>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 15px; text-align: left">
                                <igmisc:WebPanel ID="Wpn_Busqueda_" runat="server" BackColor="White" BorderColor="SteelBlue"
                                    BorderStyle="Outset" BorderWidth="2px" Font-Bold="False" 
                                    Font-Names="ARIAL" ForeColor="Black"
                                    StyleSetName="Caribbean" Width="100%" EnableAppStyling="True">
                                    <PanelStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                        ForeColor="Black" BackColor="White">
                                        
                                    </PanelStyle>
                                    <Header Text="Seleccione un tipo de filtro" TextAlignment="Left">
                                        
                                    </Header>
                                    <Template>
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" Height=""
                        Width="100%" BackColor="White">
                        <asp:RadioButtonList ID="RbnL_Busqueda" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RbnL_Busqueda_SelectedIndexChanged"
                            RepeatColumns="2" Width="352px" Height="1px">
                            <asp:ListItem>Busqueda Simple</asp:ListItem>
                            <asp:ListItem>Busqueda Avanzada</asp:ListItem>
                        </asp:RadioButtonList>
                        <igmisc:WebGroupBox ID="WebGroupBox1" runat="server" 
                            Width="62px" Height="120px" BorderColor="Transparent" 
                            BackColor="Transparent">
                            <Template>
                            <table style="width:100%;">
                                                <tr>
                                                    <td rowspan="4">
                                                        <asp:RadioButtonList ID="RbnL_Campos" runat="server" Height="103px" 
                                                            OnSelectedIndexChanged="RbnL_Campos_SelectedIndexChanged" Width="211px">
                                                            <asp:ListItem>No Empleado</asp:ListItem>
                                                            <asp:ListItem>Nombre</asp:ListItem>
                                                            <asp:ListItem>Agrupacion</asp:ListItem>
                                                            <asp:ListItem Value="Todos Los Empleados">Todos los empleados</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <igtxt:WebNumericEdit ID="Wne_NoEmpleado" runat="server" Width="200px">
                                                            <ClientSideEvents KeyDown="Wne_NoEmpleado_KeyDown" />
                                                        </igtxt:WebNumericEdit>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <igtxt:WebTextEdit ID="Wtx_Nombre" runat="server" Width="200px">
                                                            <ClientSideEvents KeyDown="Wtx_Nombre_KeyDown" />
                                                        </igtxt:WebTextEdit>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <igcmbo:WebCombo ID="Wco_Grupo" runat="server" BorderColor="Black" 
                                                            Font-Names="Tahoma" Font-Size="11px" 
                                                            OnInitializeDataSource="Wco_Grupo_InitializeDataSource" 
                                                            OnInitializeLayout="Wco_Grupo_InitializeLayout" SelBackColor="DodgerBlue" 
                                                            Version="4.00">
                                                            <Columns>
                                                                <igtbl:UltraGridColumn>
                                                                    <header caption="Column0">
                                                                    </header>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                            <ExpandEffects ShadowColor="LightGray" />
                                                            <DropDownLayout BorderCollapse="Separate" RowHeightDefault="20px" 
                                                                Version="4.00">
                                                                <FrameStyle Height="130px" Width="325px">
                                                                </FrameStyle>
                                                                <HeaderStyle BackColor="LightSteelBlue" ForeColor="Gray" />
                                                                <RowAlternateStyle BackColor="White" ForeColor="Black">
                                                                </RowAlternateStyle>
                                                                <RowSelectorStyle BackColor="LightSteelBlue" ForeColor="White">
                                                                </RowSelectorStyle>
                                                                <RowStyle BackColor="Lavender" BorderColor="Transparent" ForeColor="Black" />
                                                                <SelectedRowStyle BackColor="DodgerBlue" />
                                                            </DropDownLayout>
                                                            
                                                        </igcmbo:WebCombo>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                
                            </Template>
                        </igmisc:WebGroupBox>
                        <igmisc:WebGroupBox ID="WebGroupBox2" runat="server" Width="1px" BorderColor="Transparent">
                            <Template>
                    <asp:Table ID="TablaFiltro" runat="server" Visible="False">
                    </asp:Table>
                            </Template>
                        </igmisc:WebGroupBox>
                    </igmisc:WebAsyncRefreshPanel>
                                    </Template>
                                </igmisc:WebPanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 15px; text-align: left">
                                <ig:WebImageViewer ID="WImgV_OptImagen" runat="server" Height="150px" Visible="False"
                                    Width="500px">
                                    <Header Text="Seleccione un tipo de filtro">
                                    </Header>
                                </ig:WebImageViewer>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 0%" align="center">
                    <asp:Label ID="Lbl_Error" runat="server" ForeColor="#CC0033" Font-Names="Arial Narrow"
                        Font-Size="Smaller"></asp:Label>
                    <asp:Label ID="Lbl_Correcto" runat="server" ForeColor="#00C000"
                            Font-Names="Arial Narrow" Font-Size="Smaller"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 0%" align="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <igtxt:WebImageButton ID="WIBtn_RestablecerValores" runat="server" UseBrowserDefaults="False"
                        Text="Restablecer Valores" Height="24px" Width="164px" 
                        OnClick="WIBtn_RestablecerValores_Click" ImageTextSpacing="4" 
                        ClickOnEnterKey="False">
                        <Alignments VerticalImage="Middle" VerticalAll="Bottom"></Alignments>
                        <RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
                            RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
                        <Appearance>
                            <Style Cursor="Default">
								</Style>
                            <Image Url="./Imagenes/Undo.png" Height="16px" Width="16px"></Image>
                        </Appearance>
                    </igtxt:WebImageButton>
                    &nbsp;&nbsp;&nbsp;
                    <igtxt:WebImageButton ID="WIBtn_MostrarReporte" runat="server" UseBrowserDefaults="False"
                        Text="Continuar" Height="24px" Width="158px" 
                        OnClick="WIBtn_MostrarReporte_Click" ImageTextSpacing="4">
                        <Alignments VerticalImage="Middle" HorizontalAll="NotSet" VerticalAll="NotSet"></Alignments>
                        <RoundedCorners MaxHeight="80" ImageUrl="ig_butXP1wh.gif" MaxWidth="400" HoverImageUrl="ig_butXP2wh.gif"
                            RenderingType="FileImages" PressedImageUrl="ig_butXP4wh.gif" DisabledImageUrl="ig_butXP5wh.gif" FocusImageUrl="ig_butXP3wh.gif"></RoundedCorners>
                        <Appearance>
                            <Style Cursor="Default">
								</Style>
                            <Image Url="./Imagenes/Next.png" Height="16px" Width="16px"></Image>
                        </Appearance>
                    </igtxt:WebImageButton>
                </td>
            </tr>
        </table>
    </div>
                    <asp:ScriptManager ID="SM" runat="server">
                    </asp:ScriptManager>
    </form>
</body>
</html>