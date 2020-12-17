<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Turnierverwaltung.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 40%; margin: 100px auto">
        <asp:Login ID="LoginMaske" runat="server" OnAuthenticate="Login_Authenticate" BorderWidth="1" DisplayRememberMe="False" Font-Size="X-Large" LoginButtonStyle-BackColor="Aqua" LoginButtonStyle-BorderColor="#666666" Orientation="Vertical" PasswordLabelText="Kennwort" TextBoxStyle-BorderStyle="NotSet" UserNameLabelText="Benutzer" BorderStyle="Dotted" BorderPadding="20" FailureText="Falsches Kennwort oder Benutzer!"></asp:Login>
    </div>
</asp:Content>
