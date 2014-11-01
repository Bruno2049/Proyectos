<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyCaptcha.ascx.cs" Inherits="PAEEEM.Captcha.MyCaptcha" EnableViewState="true" %>
<style type="text/css">

</style>
<table >
    <tr>
        <td style="text-align: center" >
            <asp:Image ID="ImgCaptcha" runat="server" />
        </td>
    </tr>
    <tr>
        <td valign="middle">
            <asp:Label ID="LblMsg" runat="server" Text="<%$ Resources:DefaultResource, CaptchaMsj%>"></asp:Label>
            <asp:TextBox ID="TxtCpatcha" runat="server" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="middle">
            <asp:LinkButton ID="btnTryNewWords" runat="server" Font-Names="Tahoma" 
                Font-Size="Smaller" onclick="btnTryNewWords_Click" Text="<%$ Resources:DefaultResource, CaptchaTryNewWords%>"></asp:LinkButton>
        </td>
    </tr>
</table>
