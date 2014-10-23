<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Control1.aspx.cs" Inherits="Server_Controls.Control1" %>

<!DOCTYPE html>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string myScript = @"function AlertHello() { alert('Hello ASP.NET'); }";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
        "MyScript", myScript, true);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link href="Css/Style.css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Server Controls</title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Click" Text="Button" AccessKey="k" />
            <asp:Label ID="Label1" runat="server" Text="Label" CssClass="labels" ToolTip="Presiona el boton"></asp:Label>
        </div>
    </form>
</body>
</html>
