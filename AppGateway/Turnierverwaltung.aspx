<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Turnierverwaltung.aspx.cs" Inherits="Turnierverwaltung.Turnierverwaltung" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Turnierverwaltung</h2>
    <asp:Panel ID="PnlVerwaltung" runat="server">
        <h3>Hinzufügen von Turnieren</h3>
        <div runat="server" ID="Msg" visible="false"></div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Vereinsname"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Adresse"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Von"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Bis"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <div id="txtVereinName_container" runat="server">
                        <asp:TextBox ID="txtVereinName" CssClass="form-control" runat="server"></asp:TextBox></div>
                </td>
                <td>
                    <div runat="server" id="txtAdresse_container">
                        <asp:TextBox ID="txtAdresse" CssClass="form-control" runat="server"></asp:TextBox></div>
                </td>
                <td>
                    <asp:TextBox TextMode="Date" ID="txtDatumVon" runat="server" CssClass="form-control"></asp:TextBox></td>
                <td>
                    <asp:TextBox TextMode="Date" ID="txtDatumBis" runat="server" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>

        </table>
        <table id="tbl_Mannschaften" style="width: 100%;" runat="server">
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Teilnehmende Mannschaften"></asp:Label>
                </td>
            </tr>
           <tr>
                <td>
                    <asp:CheckBoxList ID="CheckBxLstMannschaften" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" CellSpacing="1" RepeatColumns="5" CellPadding="1" Width="100%"></asp:CheckBoxList></td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Button ID="btnAdd" CssClass="btn btn-success" runat="server" Text="Sichern" OnClick="btnAdd_Click" />
        <asp:Button ID="Btn_Bearbeiten" runat="server" Text="Sichern" CssClass="btn btn-success" OnClick="Btn_Bearbeiten_Click" Visible="false" />&nbsp;&nbsp;
        <asp:Button ID="Btn_Cancel" CssClass="btn btn-secondary" runat="server" Text="Abbrechen" OnClick="Btn_Cancel_Click" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="PnlTurniere" runat="server">
            <h2>verfügbare Turniere:</h2>
    <asp:Table ID="Tbl" runat="server" Width="100%" GridLines="Both">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Verein</asp:TableHeaderCell>
            <asp:TableHeaderCell>Adresse</asp:TableHeaderCell>
            <asp:TableHeaderCell>Mannschaften</asp:TableHeaderCell>
            <asp:TableHeaderCell>Von/Bis</asp:TableHeaderCell>
            <asp:TableHeaderCell>Spiele/Tabelle</asp:TableHeaderCell>
            <asp:TableHeaderCell>Bearbeiten</asp:TableHeaderCell>
            <asp:TableHeaderCell>Entfernen</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <br />
    <asp:Button ID="Btn_XMLDownload" CssClass="btn btn-primary" runat="server" Text="Als XML Herunterladen" OnClick="Btn_XMLDownload_Click" />
    </asp:Panel>

</asp:Content>
