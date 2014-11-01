<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prueba.aspx.cs" Inherits="Prueba" %>

<%@ Register assembly="Infragistics2.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title></title>
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
 
    <!-- !CSS -->
    <link href="CSS/Html5reset.css" rel="stylesheet" />
    <link href="CSS/Estilo.css" rel="stylesheet" />
 
    <!-- !Modernizr - All other JS at bottom
    <script src="js/modernizr-1.5.min.js"></script> -->
 
    <!-- Grab Microsoft's or Google's CDN'd jQuery. fall back to local if necessary -->
    <!-- <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.7.1.min.js" type="text/javascript"></script> -->
    <!-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script> -->
    <script>        !window.jQuery && document.write('<script src="Scripts/jquery-1.7.1.min.js"><\/script>')</script>
 
</head>
 
<body>
    <form id="form1" runat="server">
    <div id="container">
 
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
 
    </div>
    </form>
</body>
 
</html>