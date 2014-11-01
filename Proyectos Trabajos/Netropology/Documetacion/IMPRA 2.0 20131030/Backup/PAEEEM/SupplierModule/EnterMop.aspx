<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterMop.aspx.cs" Inherits="PAEEEM.EnterMop" Title="SIP"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>¡ Por favor, Introduzca el Valor MOP !</title>
    <base target="_self"></base>
</head>
<style type="text/css">
        .Label
        {
            color: Maroon;
        }
        .Button
        {
            width: 120px;
        }
        .TextBox
        {
        	
            width:245px;
        }
        </style>
<script> 
    function cancel() 
    { 
        var a='Cancel';
        self.returnValue=a;
        self.close();
    } 
    function sendMop()
    {
        if(Page_ClientValidate()) 
        {
            var a=document.getElementById("<%=txtMop.ClientID%>").value;
            self.returnValue = a ;
            self.close();      
        }
        else
        {return false;}
    }
</script>

<body>
    <form id="form1" runat="server">
    <div align="center">    
        <br />
        <h4 align="center" style="font-weight:lighter; font-style:italic;"> ¡ Por favor, Introduzca el Valor MOP !</h4>
        <asp:TextBox ID="txtMop" runat="server" CssClass="TextBox"></asp:TextBox>
        <br />
        <asp:RegularExpressionValidator runat="server" ErrorMessage="Sólo son Válidos Números Enteros" ControlToValidate="txtMop" ValidationExpression="^[-+]?\d$"/>
        <br />
        <br />
        <%--Update by Tina 2011/08/03--%>
        <asp:Button ID="btnOk" runat="server" Text="Aceptar" OnClientClick="sendMop()" CssClass="Button"/>&nbsp &nbsp
        <%--End--%>
        <asp:Button ID="btnCancel" runat="server" Text="Salir" OnClientClick="cancel()" CssClass="Button" CausesValidation="false"/>    
    </div>
    </form>
</body>
</html>
