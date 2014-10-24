﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<Libro_MVC_4_Pro.Models.ExternalLogin>>" %>

<% if (Model.Count > 0) { %>
    <h3>Inicios de sesión externos registrados</h3>
    <table>
        <tbody>
        <% foreach (Libro_MVC_4_Pro.Models.ExternalLogin externalLogin in Model) { %>
            <tr>
                <td><%: externalLogin.ProviderDisplayName %></td>
                <td>
                    <% if (ViewBag.ShowRemoveButton) {
                        using (Html.BeginForm("Disassociate", "Account")) { %>
                            <%: Html.AntiForgeryToken() %>
                            <div>
                                <%: Html.Hidden("provider", externalLogin.Provider) %>
                                <%: Html.Hidden("providerUserId", externalLogin.ProviderUserId) %>
                                <input type="submit" value="Quitar" title="Quitar esta <%: externalLogin.ProviderDisplayName %> credencial de su cuenta" />
                            </div>
                        <% }
                    } else { %>
                        &nbsp;
                    <% } %>
                </td>
            </tr>
        <% } %>
        </tbody>
    </table>
<% } %>
