<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Turnierverwaltung._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Turnierverwaltung</h1>
    <br />
    <% if (Session["auth"] == null || !(bool)Session["auth"])
        { %>
    <p>
        Willkommen in meine ASP.net Anwendung. Zum Testen stehen die folgenden Benutzer zur Verfügung:<br />
        <blockquote>
            <i>User (Benutzer: user, kennwort: user)</i><br />
            <i>Admin (Benutzer: admin, kennwort: admin)</i>
        </blockquote>
    </p>
    <% }
        else
        { %>

    <a href="~/Personenverwaltung.aspx" runat="server">
        <h3>Personenverwaltung</h3>
    </a>
    <a href="~/Mannschaftsverwaltung.aspx" runat="server">
        <h3>Mannschaftsverwaltung</h3>
    </a>
        <a href="~/Turnierverwaltung.aspx" runat="server">
        <h3>Turnierverwaltung</h3>
    </a>
    <li><a runat="server" href="~/TurnierTabelle.aspx"><h3>Turniertabellen</h3></a></li>

    <% } %>
</asp:Content>
