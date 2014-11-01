<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditRequest_new.aspx.cs" Inherits="PAEEEM.SupplierModule.CreditRequest_new"
    MasterPageFile="~/Site.Master" Title="Alta de Solicitud de Crédito" MaintainScrollPositionOnPostback="true" %>




<%@ Register src="wuAltaBajaEquipos.ascx" tagname="wuAltaBajaEquipos" tagprefix="uc1" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <style type="text/css">
        table {
            width: 100%;
            height: auto;
        }

        .columnaUno {
            width: 10%;
        }

        .columnaDos {
            width: 22%;
        }


        .columnaTres {
            width: 24%;
        }

        .columnaSeparacion {
            width: 5%;
        }

        #datosCliente {
            width: 100%;
            height: auto;
        }

        #tecnologias {
            width: 100%;
            height: auto;
            margin-top: 10px;
        }

        .rowSeparacion {
            height: 15px;
        }

        fieldset {
            border: 1px solid lightblue;
            padding: 4px;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:wuAltaBajaEquipos ID="wuAltaBajaEquipos1" runat="server" />

    </asp:Content>
