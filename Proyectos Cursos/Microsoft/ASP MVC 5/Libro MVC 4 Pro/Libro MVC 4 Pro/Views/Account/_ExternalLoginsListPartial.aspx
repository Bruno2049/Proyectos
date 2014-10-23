<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<Microsoft.Web.WebPages.OAuth.AuthenticationClientData>>" %>

<% if (Model.Count == 0) { %>
    <div class="message-info">
        <p>No hay ningún servicio de autenticación externa configurado. Consulte <a href="http://go.microsoft.com/fwlink/?LinkId=252166">este artículo</a>
        para obtener detalles sobre la configuración de esta aplicación ASP.NET para que admita el inicio de sesión a través de servicios externos.</p>
    </div>
<% } else {
    using (Html.BeginForm("ExternalLogin", "Account", new { ReturnUrl = ViewBag.ReturnUrl })) { %>
    <%: Html.AntiForgeryToken() %>
    <fieldset id="socialLoginList">
        <legend>Inicie sesión con otro servicio</legend>
        <p>
        <% foreach (Microsoft.Web.WebPages.OAuth.AuthenticationClientData p in Model) { %>
            <button type="submit" name="provider" value="<%: p.AuthenticationClient.ProviderName %>" title="Inicie sesión con su <%: p.DisplayName %> cuenta"><%: p.DisplayName%></button>
        <% } %>
        </p>
    </fieldset>
    <% }
} %>
