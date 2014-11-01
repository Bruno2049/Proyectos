<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PAEEEM.Login.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>
    <style type="text/css">
        #divPassword {
            text-align: center;
        }

        #divContainer {
            text-align: center;
            margin-left: 35%;
            margin-right: 35%;
        }

        a {
            color: #666666;
        }

            a:visited {
                color: #808080;
            }

            a:active {
                color: #808080;
            }

            a:hover {
                color: #999999;
            }

            body { 
	background-image: url(../images/login4.png); 
	background-attachment: fixed; 
	background-repeat: no-repeat; 
	background-position: top center; 
} 
    </style>
</head>
<body class="body">
    <form id="form1" runat="server" style="width: 100%; height: 100%">

<div id="login" style="text-align:center; margin-left:auto; margin-right:auto;">

       <%-- <table style="width: 100%; height: 100%" align="center">
            <tr>
                <td align="center" style="width: 911px; background-image: url('../images/login.png'); background-position: center top; background-repeat: no-repeat;">
               --%>    
                      <asp:Login ID="ctlLogin" runat="server" OnAuthenticate="ctlLogin_Authenticate" 
                          style="text-align:center; margin-left:auto; margin-right:auto;"
                        DisplayRememberMe="False" Width="342px" Height="180px" BorderPadding="0"
                        BorderStyle="Solid" BorderWidth="0px" Font-Names="Verdana" Font-Size="16px"
                        ForeColor="#666666" PasswordLabelText="Contraseña:"
                        PasswordRequiredErrorMessage="¡ Debe Ingresar su Contraseña !"
                        UserNameLabelText="Usuario:" UserName=""
                        LoginButtonText="Ingresar" TitleText="ACCESO AL SISTEMA DE INFORMACIÓN DEL PAEEEM">
                        <TextBoxStyle Font-Size="1em" />
                        <LoginButtonStyle
                            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#666666" />
                        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                        <TitleTextStyle Font-Bold="True" Font-Size="1.2em" ForeColor="#666666" />
                    </asp:Login>

           <br />

                    <div id="divPassword" style="width: 342px; text-align:center; margin-left:auto; margin-right:auto;">
                        <asp:HyperLink ID="hlChangePassword" runat="server" NavigateUrl="ChangePassword.aspx" Width="150px" Font-Names="Verdana" Font-Size="12px" Font-Underline="False" >Cambiar la Contraseña</asp:HyperLink>
                        <asp:HyperLink ID="hlForgetPassword" runat="server" NavigateUrl="ForgetPassword.aspx" Width="150px" Font-Names="Verdana" Font-Size="12px" Font-Underline="False">
			Olvidé la Contraseña</asp:HyperLink>
                        &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
                    </div>
       <%--         </td>
            </tr>
        </table>--%>

    </div>
    </form>
</body>
</html>
