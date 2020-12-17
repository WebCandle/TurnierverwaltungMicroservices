<%@ Page Title="Personenverwaltung" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Personenverwaltung.aspx.cs" Inherits="Turnierverwaltung.Personenverwaltung" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Personenverwaltung</h1>
    <asp:Panel ID="PnlVerwaltung" runat="server">
        <h2 style="font-weight: bold">Hinzufügen oder Bearbeiten von Personen</h2>
        <div runat="server" ID="Msg" visible="false"></div>
        <asp:Label ID="lblTitle" runat="server" Text="Auswahl des Personen Typs:" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        <asp:RadioButtonList ID="RadioButtonListPersonenType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonListPersonenType_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="Fussballspieler">&nbsp;Fussballspieler</asp:ListItem>
            <asp:ListItem Value="Handballspieler">&nbsp;Handballspieler</asp:ListItem>
            <asp:ListItem Value="Tennisspieler">&nbsp;Tennisspieler</asp:ListItem>
            <asp:ListItem Value="anderer Spielertyp">&nbsp;Anderer Spielertyp(Spieler)</asp:ListItem>
            <asp:ListItem Value="Physiotherapeut">&nbsp;Physiotherapeut</asp:ListItem>
            <asp:ListItem Value="Trainer">&nbsp;Trainer</asp:ListItem>
            <asp:ListItem Value="Person mit anderen Aufgaben">&nbsp;Person mit anderen Aufgaben(Mitarbeiter)</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="Lbl_Vorname" runat="server" Text=" Vorname"></asp:Label></td>
                    <td>
                        <div id="Txt_Vorname_Container" runat="server"><asp:TextBox  CssClass="form-control" ID="Txt_Vorname" runat="server"></asp:TextBox></div></td>
                    <td>
                        <asp:Label ID="Lbl_Name" runat="server" Text=" Name"></asp:Label></td>
                    <td>
                        <div id="Txt_Name_Container" runat="server"><asp:TextBox ID="Txt_Name"  CssClass="form-control" runat="server"></asp:TextBox></div></td>
                    <td>
                        <asp:Label ID="Lbl_Datum" runat="server" Text=" Geburtsdatum"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="Txt_Datum" runat="server"  CssClass="form-control" TextMode="Date"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Lbl1" runat="server" Text=" Anzahl Spiele"></asp:Label>
                    </td>
                    <td>
                        <div id="Txt1_Container" runat="server"><asp:TextBox ID="Txt1"  CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox></div>
                    </td>
                    <td>
                        <asp:Label ID="Lbl2" runat="server" Text=" Geschossene Tore"></asp:Label>
                    </td>
                    <td>
                        <div id="Txt2_Container" runat="server"><asp:TextBox ID="Txt2"  CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox></div>
                    </td>
                    <td>
                        <asp:Label ID="Lbl3" runat="server" Text=" Spielposition"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt3" runat="server"  CssClass="form-control"></asp:TextBox>
                        <asp:DropDownList ID="Sportart" runat="server" Visible="false"  CssClass="form-control"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <asp:Label ID="Lbl_Msg" runat="server" Text=""></asp:Label>
        <asp:Button ID="Btn_Add" CssClass="btn btn-success" runat="server" Text="Hinzufügen" OnClick="Add_Click" />&nbsp;
    <asp:Button ID="Btn_Bearbeiten" runat="server" Text="Sichern" CssClass="btn btn-success" OnClick="Btn_Bearbeiten_Click" Visible="false" />&nbsp;&nbsp;
    <asp:Button ID="Btn_Cancel" CssClass="btn btn-secondary" runat="server" Text="Abbrechen" OnClick="Btn_Cancel_Click" Visible="false" />
        <br />
    </asp:Panel>
    <br />

    <asp:Table ID="Tbl" runat="server" BorderStyle="Double" Width="100%" GridLines="Both">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>ID</asp:TableHeaderCell>
            <asp:TableHeaderCell>Name</asp:TableHeaderCell>
            <asp:TableHeaderCell>Vorname</asp:TableHeaderCell>
            <asp:TableHeaderCell>Geburtsdatum</asp:TableHeaderCell>
            <asp:TableHeaderCell>Sportart</asp:TableHeaderCell>
            <asp:TableHeaderCell>Anzahl Spiele</asp:TableHeaderCell>
            <asp:TableHeaderCell>Erzielte Tore</asp:TableHeaderCell>
            <asp:TableHeaderCell>Gewonnene Spiele</asp:TableHeaderCell>
            <asp:TableHeaderCell>Anzahl Jahre</asp:TableHeaderCell>
            <asp:TableHeaderCell>Anzahl Vereine</asp:TableHeaderCell>
            <asp:TableHeaderCell>Einsatz Bereich</asp:TableHeaderCell>
            <asp:TableHeaderCell>Bearbeiten</asp:TableHeaderCell>
            <asp:TableHeaderCell>Entfernen</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <br />
    <asp:Button ID="Btn_XMLDownload" CssClass="btn btn-primary" runat="server" Text="Als XML Herunterladen" OnClick="Btn_XMLDownload_Click" />
</asp:Content>
