<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mannschaftsverwaltung.aspx.cs" Inherits="Turnierverwaltung.Mannschaftsverwaltung" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Mannschaftsverwaltung</h2>
    <asp:Panel ID="PnlVerwaltung" runat="server">
        <h3>Hinzufügen oder Bearbeiten einer Turnier Mannschaft</h3>
        <div runat="server" ID="Msg" visible="false"></div>
        <br />

        <table style="width: 100%;" id="personentbl" runat="server">
            <tr>
                <td style="width: 40%;">
                    <asp:Label ID="Label2" runat="server" Text="Mannschaftsname:"></asp:Label>
                    <br />
                    <div id="Txt_Name_Container" runat="server">
                    <asp:TextBox ID="Txt_Name" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        </td>
                <td style="width: 10%; text-align: center;"></td>
                <td style="width: 10%; text-align: center;"></td>
                <td style="width: 40%;">
                    <asp:Label ID="Label1" runat="server" Text="Sportart"></asp:Label>
                    <br />
                    <asp:DropDownList ID="Sportart" CssClass="form-control" runat="server" OnSelectedIndexChanged="Sportart_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td>
                
            </tr>
            <tr>
                <td style="width: 40%;">
                    <asp:Label ID="Label3" runat="server" Text="Mitglieder der Mannschaft:"></asp:Label>
                    <br />
                    <asp:ListBox ID="LstBxM" CssClass="form-control" runat="server" Width="100%" SelectionMode="Multiple"></asp:ListBox></td>
                <td style="width: 10%; text-align:center">
                    <asp:Button ID="Btn2" runat="server" CssClass="btn btn-info" Text=">" OnClick="Btn2_Click" /></td>
                <td style="width: 10%; text-align:center">
                    <asp:Button ID="Button1" runat="server" Text="<" CssClass="btn btn-info" OnClick="Button1_Click" /></td>
                <td  style="width: 40%;">
                    <asp:Label ID="Label4" runat="server" Text="verfügbare Personen:"></asp:Label>
                    <br />
                    <asp:ListBox ID="LstBxP" CssClass="form-control" runat="server" Width="100%" SelectionMode="Multiple"></asp:ListBox></td>

            </tr>
        </table>
        <asp:Button ID="Btn_Add" CssClass="btn btn-success" runat="server" Text="Manschaft Hinzufügen" OnClick="Btn_add" />
        <asp:Button ID="Btn_Abbrechen" CssClass="btn btn-secondary" runat="server" Text="Abbrechen" Visible="false" OnClick="Btn_Abbrechen_Click" />&nbsp;&nbsp;
        <asp:Button ID="Btn_Sichern" runat="server" CssClass="btn btn-success" Text="Sichern" Visible="false" OnClick="Btn_Sichern_Click" />

    </asp:Panel>
    <asp:Panel ID="PnlMannschaften" runat="server">
        <h2>verfügbare Mannschaften:</h2>
        <asp:Table ID="Tbl" runat="server" Width="100%" GridLines="Both">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>Sportart</asp:TableHeaderCell>
                <asp:TableHeaderCell>Mitglieder</asp:TableHeaderCell>
                <asp:TableHeaderCell>Bearbeiten</asp:TableHeaderCell>
                <asp:TableHeaderCell>Entfernen</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        <br />
        <asp:Button ID="Btn_XMLDownload" CssClass="btn btn-primary" runat="server" Text="Als XML Herunterladen" OnClick="Btn_XMLDownload_Click" />
    </asp:Panel>
</asp:Content>
