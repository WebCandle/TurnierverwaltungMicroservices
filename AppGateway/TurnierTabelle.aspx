<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TurnierTabelle.aspx.cs" Inherits="Turnierverwaltung.TurnierTabelle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Request.QueryString["item"] == null)
        { %>
    <h1>Turniertabellen</h1>
    <% } %>
    <asp:Panel ID="Pnl" runat="server"></asp:Panel>
    <% if (Request.QueryString["item"] != null)
        { %>
    <br />
    <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Turnierverwaltung.aspx">zurück</asp:HyperLink>
    <% } %>
</asp:Content>
