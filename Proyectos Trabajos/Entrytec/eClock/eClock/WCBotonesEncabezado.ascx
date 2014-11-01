<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCBotonesEncabezado.ascx.cs" Inherits="WCBotonesEncabezado" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<igtxt:webimagebutton id="Btn_Principal" runat="server" enabletheming="True" onclick="Btn_Principal_Click"
    text="Inicio" tooltip="Pagina Principal">
                                                            <Alignments HorizontalAll="Left" VerticalImage="Top"  />
                                                            <Appearance>
                                                                <Image Url="./skins/ico-home.gif"  />
                                                                <ButtonStyle BackColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Bold="True"
                                                                    Font-Names="Tahoma" Font-Size="11px" ForeColor="#939393">
                                                                    <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
                                                                </ButtonStyle>
                                                                <InnerBorder StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None"  />
                                                            </Appearance>
                                                            <HoverAppearance>
                                                                <Image Url="./skins/ico-home-over.gif"  />
                                                                <ButtonStyle ForeColor="WhiteSmoke">
                                                                </ButtonStyle>
                                                            </HoverAppearance>
                                                            <RoundedCorners RenderingType="Disabled"  />
    <FocusAppearance>
        <ButtonStyle Cursor="Hand">
        </ButtonStyle>
    </FocusAppearance>
                                                        </igtxt:webimagebutton>
&nbsp;<igtxt:webimagebutton id="Btn_Salir" runat="server" enabletheming="True" onclick="Btn_Salir_Click"
    text="Salir" tooltip="Salir del Sistema">
    <Alignments HorizontalAll="Left" VerticalImage="Top"  />
    <Appearance>
        <Image Url="./skins/ico-login.gif"  />
        <ButtonStyle BackColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Bold="True"
            Font-Names="Tahoma" Font-Size="11px" ForeColor="#939393">
            <BorderDetails StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None" />
        </ButtonStyle>
        <InnerBorder StyleBottom="None" StyleLeft="None" StyleRight="None" StyleTop="None"  />
    </Appearance>
    <HoverAppearance>
        <Image Url="./skins/ico-login-over.gif"  />
        <ButtonStyle ForeColor="WhiteSmoke">
        </ButtonStyle>
    </HoverAppearance>
    <RoundedCorners RenderingType="Disabled"  />
</igtxt:WebImageButton>
&nbsp; &nbsp;
