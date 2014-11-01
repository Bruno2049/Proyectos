<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs"
    Inherits="PAEEEM.ForgetPassword"  Async="true" Title="SIP"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forget Password</title>
    <style type="text/css">
        .Label
        {
            width: 30%;
            color: Maroon;
            text-align: right;
            float: left;
        }
        .TextBox
        {
            width: 40%;
            margin-right: 10%;
        }
        #TitleLabel
        {
            color: White;
            
            font-size: 0.9em;
            font-weight: bold;
            text-align: center;
        }
        #SubTitlelabel
        {
            color: Black;
            font-style: italic;
            text-align: center;
        }
        #Errormessage
        {
            color: Red;
        }
        .Button
        {
            
            
            font-family: Verdana;
            font-size: 0.8em;
            color:#666666;
            width: 160px;
            background-image:url('../images/btn.png');
        }
        #divContainer
        {
            text-align: center;
            margin-left: 33%;
            margin-right: 33%;
        }
    .style1 {
				text-align: center;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <div id="divContainer" style="width: 526px; height: 277px">
            <table style="width: 526px; height: 250px;">
							
							<tr>
											<td style="background-image:url('../images/fondo4.png'); background-repeat:no-repeat; background-position:center top;">
                <div id="TitleLabel" style="width: 450px">
                    <br />
                    <asp:Label Text="¿ Olvidó su Contraseña ?" runat="server" Font-Names="Verdana" ForeColor="#333333" />
					<br />
					<br />
				</div>
                <div id="SubTitlelabel" style="width: 450px">
                    <asp:Label Text="Introduzca su Usuario para Recibir vía E-mail su Contraseña." runat="server" Font-Names="Verdana" Font-Size="Small" ForeColor="#666666" /></div>
                <br />
                <br />
                <div style="width: 450px" class="style1">
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName" CssClass="Label" Width="200px" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333">Nombre de Usuario:</asp:Label>
					<div class="style1">
&nbsp;
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                        ErrorMessage="¡ Debe Ingresar su Usuario !" ToolTip="Campo Vacío o Incorrecto" ValidationGroup="prPasswordRecovery">*</asp:RequiredFieldValidator>
                	</div>
                </div>
                <br />
                <div id="Errormessage">
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                	<br />
					<br />
                </div>
                
            </td>
							</tr>
							
							<tr>
											<td style="background-repeat:no-repeat; background-position:center top;">
                    <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Text="Ingresar"
                        ValidationGroup="prPasswordRecovery" CssClass="Button" Width="80px" Font-Names="Verdana" 
                                                    Font-Size="13px" ForeColor="#666666" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Regresar" ValidationGroup="Cancelar"
                        CssClass="Button" OnClick="btnCancel_Click" Width="80px" ForeColor="#666666" />
            </td>
							</tr>
			</table>
        
            </div>
    </form>
</body>
</html>
