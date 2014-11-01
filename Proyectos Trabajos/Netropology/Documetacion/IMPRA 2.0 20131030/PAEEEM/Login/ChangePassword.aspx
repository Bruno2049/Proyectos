<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="PAEEEM.ChangePassword" Title="SIP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <style type="text/css">
        .Label
        {
            width: 30%;
            color: Maroon;
            float: left;
            text-align: right;
        }
        .TextBox
        {
            width: 50%;
            float: right;
            margin-right: 15%;
        }
        .Button
        {
            width: 120px;
            font-family:Verdana;
            font-size:13px;
            color:#666666;
            background-image:url('../images/btn.png');
        }
        #divContainer
        {
            text-align: center;
            margin-left: 33%;
            margin-right: 33%;
        }
        #lblTips
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <table style="width: 100%">
					<tr>
					<div id="divContainer">
									<td align="center" style="background-image:url('../images/fondo4.png'); background-position:center;background-repeat:no-repeat; height: 209px;"> 
        <div style="width:450px; height: 30px;">
            <asp:Label ID="lblUserName" runat="server" Text="Usuario" CssClass="Label" Width="130px" Font-Names="Verdana" Font-Size="13px" ForeColor="#666666"></asp:Label>&nbsp
            &nbsp &nbsp &nbsp
            <asp:TextBox ID="txtUserName" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUserName"
                ErrorMessage="¡ Debe Ingresar un Usuario !" runat="server">*</asp:RequiredFieldValidator></div>
        <div style="width: 450px; height: 30px;">
            <asp:Label ID="lblOldPassword" runat="server" Text="Contraseña Anterior" CssClass="Label" Width="130px" Font-Names="Verdana" Font-Size="13px" ForeColor="#666666"></asp:Label>&nbsp
            &nbsp &nbsp &nbsp
            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" CssClass="TextBox" Width="200px"></asp:TextBox>&nbsp<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOldPassword"
                ErrorMessage="¡ Debe Ingresar la Contraseña Anterior !" runat="server">*</asp:RequiredFieldValidator></div>
        <div style="width: 450px; height: 30px;">
            <asp:Label ID="lblNewPassword" runat="server" Text="Nueva Contraseña" CssClass="Label" Width="130px" Font-Names="Verdana" Font-Size="13px" ForeColor="#666666"></asp:Label>&nbsp
            &nbsp &nbsp &nbsp
            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="TextBox" Width="200px"></asp:TextBox>
			&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNewPassword"
                ErrorMessage="¡ Debe Ingresar la Nueva Contraseña !" runat="server">*</asp:RequiredFieldValidator></div>
        <div style="width: 450px; height: 30px;">
            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirmar Contraseña" CssClass="Label" Width="130px" Font-Names="Verdana" Font-Size="13px" ForeColor="#666666"></asp:Label>&nbsp
            &nbsp &nbsp &nbsp
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="TextBox" Width="200px"></asp:TextBox>
			&nbsp;<asp:RequiredFieldValidator ID="required1" ControlToValidate="txtConfirmPassword"
                ErrorMessage="Confirmar la Nueva Contraseña" runat="server">*</asp:RequiredFieldValidator></div>
        <div>
            <asp:Label ID="lblTips" runat="server"></asp:Label>
        </div>
        
    </td>
					</div></tr>
					<tr>
									<td align="center"> 
            <asp:Button ID="btnSubmit" runat="server" Text="Aceptar" OnClick="btnSubmit_Click"
                CssClass="Button" Width="80px" />&nbsp;
            <asp:Button ID="btnReset" runat="server" Text="Restablecer" OnClick="btnReset_Click"
                CausesValidation="false" CssClass="Button" Width="107px" />&nbsp;
            <asp:Button ID="btnCancel" Text="Salir" runat="server" OnClick="btnCancel_Click"
                CausesValidation="false" CssClass="Button" Width="80px" />
    </td>
					</tr>
	</table>
    
   
    </form>
</body>
</html>
