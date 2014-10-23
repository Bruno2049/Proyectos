<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Libro_MVC_4_Pro.Models.RegisterExternalLoginModel>" %>

<asp:Content ID="externalLoginConfirmationTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Registrarse
</asp:Content>

<asp:Content ID="externalLoginConfirmationContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Registrarse.</h1>
        <h2>Asociar su cuenta de <%: ViewBag.ProviderDisplayName %>.</h2>
    </hgroup>

    <% using (Html.BeginForm("ExternalLoginConfirmation", "Account", new { ReturnUrl = ViewBag.ReturnUrl })) { %>
        <%: Html.AntiForgeryToken() %>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Formulario de asociación</legend>
            <p>
                Se ha autenticado correctamente con <strong><%: ViewBag.ProviderDisplayName %></strong>.
                Escriba un nombre de usuario para este sitio y haga clic en el botón Confirmar para finalizar
                el inicio de sesión.
            </p>
            <ol>
                <li class="name">
                    <%: Html.LabelFor(m => m.UserName) %>
                    <%: Html.TextBoxFor(m => m.UserName) %>
                    <%: Html.ValidationMessageFor(m => m.UserName) %>
                </li>
            </ol>
            <%: Html.HiddenFor(m => m.ExternalLoginData) %>
            <input type="submit" value="Registrarse" />
        </fieldset>
    <% } %>
</asp:Content>

<asp:Content ID="scriptsContent" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>
