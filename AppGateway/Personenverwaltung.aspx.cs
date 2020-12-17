using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.IO;

namespace Turnierverwaltung
{
    public partial class Personenverwaltung : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = new User(Session);
            if (!user.Auth)
            {
                Session["redirect"] = "~/Personenverwaltung.aspx";
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                if (Page.IsCallback == false)
                {
                    Sportart.Items.Clear();
                    Sportart.Items.Add("Fussball");
                    Sportart.Items.Add("Handball");
                    Sportart.Items.Add("Tennis");
                    Txt_Datum.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    if (Request.QueryString["do"] == "entfernen")
                    {
                        if (user.Has_Permission("admin"))
                        {
                            long item = long.Parse(Request.QueryString["item"]);
                            Person person = new Person(item);
                            person.Delete();
                            Response.Redirect("~/Personenverwaltung.aspx");
                        }
                        else
                        {
                            Access_Denied();
                        }

                    }
                    else if (Request.QueryString["do"] == "bearbeiten")
                    {
                        RadioButtonListPersonenType.Visible = false;
                        lblTitle.Visible = false;
                        Btn_Add.Visible = false;
                        Btn_Bearbeiten.Visible = true;
                        Btn_Cancel.Visible = true;
                        long item = Convert.ToInt32(Request.QueryString["item"]);
                        Person person = new Person(item);
                        if (person.Art == "FussballSpieler")
                        {
                            RenderFussballForm();
                            FussballSpieler s = new FussballSpieler(person.Art_ID);
                            Txt1.Text = s.Spiele.ToString();
                            Txt2.Text = s.Tore.ToString();
                            Txt3.Text = s.Position;
                        }
                        else if (person.Art == "HandballSpieler")
                        {
                            RenderHandballForm();
                            HandballSpieler s = new HandballSpieler(person.Art_ID);
                            Txt1.Text = s.Spiele.ToString();
                            Txt2.Text = s.Tore.ToString();
                            Txt3.Text = s.Position;
                        }
                        else if (person.Art == "TennisSpieler")
                        {
                            RenderTennisForm();
                            TennisSpieler s = new TennisSpieler(person.Art_ID);
                            Txt1.Text = s.Spiele.ToString();
                            Txt2.Text = s.Tore.ToString();
                        }
                        else if (person.Art == "Spieler")
                        {
                            RenderSpielerForm();
                            Spieler s = new Spieler(person.Art_ID);
                            Txt1.Text = s.Spiele.ToString();
                            Txt2.Text = s.Tore.ToString();
                            Sportart.Items.FindByValue(s.Sportart).Selected = true;
                        }
                        else if (person.Art == "Mitarbeiter")
                        {
                            RenderMitarbeiterForm();
                            Mitarbeiter m = new Mitarbeiter(person.Art_ID);
                            Txt1.Text = m.Aufgabe;
                            Sportart.Text = m.Sportart;
                        }
                        else if (person.Art == "Physiotherapeut")
                        {
                            RenderPhysiotherapeutForm();
                            Physiotherapeut ph = new Physiotherapeut(person.Art_ID);
                            Txt1.Text = ph.Jahre.ToString();
                            Sportart.Text = ph.Sportart;

                        }
                        else if (person.Art == "Trainer")
                        {
                            RenderTrainerForm();
                            Trainer t = new Trainer(person.Art_ID);
                            Txt1.Text = t.Vereine.ToString();
                            Sportart.Text = t.Sportart;
                        }
                        Txt_Name.Text = person.Name;
                        Txt_Vorname.Text = person.Vorname;
                        Txt_Datum.Text = person.Geburtsdatum.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        RadioButtonListPersonenType.Visible = true;
                        lblTitle.Visible = true;
                        Btn_Add.Visible = true;
                        Btn_Bearbeiten.Visible = false;
                        Btn_Cancel.Visible = false;
                    }
                }
                Lbl_Msg.Visible = false;
                Render();
            }
        }
        protected void RenderFussballForm()
        {
            Lbl1.Text = "Anzahl Spiele";
            Txt1.TextMode = TextBoxMode.Number;
            Lbl2.Text = "Geschossene Tore";
            Lbl2.Visible = true;
            Txt2.Visible = true;
            Txt2.TextMode = TextBoxMode.Number;
            Lbl3.Visible = true;
            Txt3.Visible = true;
            Lbl3.Text = "Spielposition";
            Sportart.Visible = false;
        }
        protected void RenderHandballForm()
        {
            Lbl1.Text = "Anzahl Spiele";
            Lbl2.Text = "	Geworfene Tore";
            Txt1.TextMode = TextBoxMode.Number;
            Txt2.TextMode = TextBoxMode.Number;
            Lbl3.Text = "Einsatzbereich";
            Lbl2.Visible = true;
            Txt2.Visible = true;
            Lbl3.Visible = true;
            Txt3.Visible = true;
        }
        protected void RenderTennisForm()
        {
            Lbl1.Text = "Anzahl Spiele";
            Lbl2.Text = "	Gewonnene Spiele";
            Txt1.TextMode = TextBoxMode.Number;
            Txt2.TextMode = TextBoxMode.Number;
            Lbl2.Visible = true;
            Txt2.Visible = true;
            Lbl3.Visible = false;
            Txt3.Visible = false;
            Sportart.Visible = false;
        }
        protected void RenderSpielerForm()
        {
            Lbl1.Text = "Anzahl Spiele";
            Lbl2.Text = "Gewonnene Spiele";
            Txt1.TextMode = TextBoxMode.Number;
            Txt2.TextMode = TextBoxMode.Number;
            Lbl2.Visible = true;
            Txt2.Visible = true;
            Lbl3.Visible = true;
            Lbl3.Text = "Sportart";
            Txt3.Visible = false;
            Sportart.Visible = true;
        }
        protected void RenderPhysiotherapeutForm()
        {
            Lbl1.Text = "Anzahl Jahre";
            Txt1.TextMode = TextBoxMode.Number;
            Lbl2.Visible = false;
            Txt2.Visible = false;
            Lbl3.Visible = true;
            Sportart.Visible = true;
            Lbl3.Text = "Sportart";
            Txt3.Visible = false;
        }
        protected void RenderTrainerForm()
        {
            Lbl1.Text = "Anzahl Vereine";
            Txt1.TextMode = TextBoxMode.Number;
            Lbl2.Visible = false;
            Txt2.Visible = false;
            Txt3.Visible = false;
            Lbl3.Text = "Sportart";
            Lbl3.Visible = true;
            Sportart.Visible = true;
        }
        protected void RenderMitarbeiterForm()
        {
            Lbl1.Text = "Aufgaben";
            Txt1.TextMode = TextBoxMode.SingleLine;
            Lbl2.Visible = false;
            Txt2.Visible = false;
            Lbl3.Visible = true;
            Lbl3.Text = "Sportart";
            Sportart.Visible = true;
            Txt3.Visible = false;
        }
        protected void RadioButtonListPersonenType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListPersonenType.SelectedItem.Value == "Fussballspieler")
            {
                RenderFussballForm();
            }
            else if (RadioButtonListPersonenType.SelectedItem.Value == "Handballspieler")
            {
                RenderHandballForm();
            }
            else if (RadioButtonListPersonenType.SelectedItem.Value == "Tennisspieler")
            {
                RenderTennisForm();
            }
            else if (RadioButtonListPersonenType.SelectedItem.Value == "anderer Spielertyp")
            {
                RenderSpielerForm();
            }
            else if (RadioButtonListPersonenType.SelectedItem.Value == "Physiotherapeut")
            {
                RenderPhysiotherapeutForm();
            }
            else if (RadioButtonListPersonenType.SelectedItem.Value == "Trainer")
            {
                RenderTrainerForm();
            }
            else if (RadioButtonListPersonenType.SelectedItem.Value == "Person mit anderen Aufgaben")
            {
                RenderMitarbeiterForm();
            }

        }
        public void Access_Denied()
        {
            Msg.InnerText = "Sie haben keine Berichtigung!";
            Msg.Attributes["class"] = "alert alert-warning";
            Msg.Visible = true;
        }
        public void Feld_Required()
        {
            Msg.InnerText = "Felder sind erforderlich!";
            Msg.Attributes["class"] = "alert alert-warning";
            Msg.Visible = true;
        }
        private void Render()
        {
            Tbl.Rows.Clear();

            TableHeaderRow header = new TableHeaderRow();

            TableHeaderCell h1 = new TableHeaderCell();
            h1.Text = "ID";
            header.Cells.Add(h1);
            TableHeaderCell h2 = new TableHeaderCell();
            h2.Text = "Name";
            header.Cells.Add(h2);
            TableHeaderCell h3 = new TableHeaderCell();
            h3.Text = "Vorname";
            header.Cells.Add(h3);
            TableHeaderCell h4 = new TableHeaderCell();
            h4.Text = "Geburtsdatum";
            header.Cells.Add(h4);
            TableHeaderCell h5 = new TableHeaderCell();
            h5.Text = "Sportart";
            header.Cells.Add(h5);
            TableHeaderCell h6 = new TableHeaderCell();
            h6.Text = "Anzahl Spiele";
            header.Cells.Add(h6);
            TableHeaderCell h7 = new TableHeaderCell();
            h7.Text = "Erzielte Tore";
            header.Cells.Add(h7);
            TableHeaderCell h8 = new TableHeaderCell();
            h8.Text = "Gewonnene Spiele";
            header.Cells.Add(h8);
            TableHeaderCell h9 = new TableHeaderCell();
            h9.Text = "Anzahl Jahre";
            header.Cells.Add(h9);
            TableHeaderCell h10 = new TableHeaderCell();
            h10.Text = "Anzahl Vereine";
            header.Cells.Add(h10);
            TableHeaderCell h11 = new TableHeaderCell();
            h11.Text = "Einsatz Bereich";
            header.Cells.Add(h11);
            TableHeaderCell h12 = new TableHeaderCell();
            h12.Text = "Bearbeiten";
            header.Cells.Add(h12);
            TableHeaderCell h13 = new TableHeaderCell();
            h13.Text = "Entfernen";
            header.Cells.Add(h13);

            Tbl.Rows.Add(header);

            foreach (Person p in Person.GetAll())
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.Text = p.Person_ID.ToString();
                row.Cells.Add(cell1);
                TableCell cell2 = new TableCell();
                cell2.Text = p.Name;
                row.Cells.Add(cell2);
                TableCell cell3 = new TableCell();
                cell3.Text = p.Vorname;
                row.Cells.Add(cell3);
                TableCell cell4 = new TableCell();
                cell4.Text = p.Geburtsdatum.ToShortDateString();
                row.Cells.Add(cell4);

                if (p is FussballSpieler)
                {
                    TableCell cell5 = new TableCell();
                    cell5.Text = "FussballSpieler";
                    row.Cells.Add(cell5);
                    FussballSpieler f = p as FussballSpieler;
                    TableCell cell6 = new TableCell();
                    cell6.Text = f.Spiele.ToString();
                    row.Cells.Add(cell6);
                    TableCell cell7 = new TableCell();
                    cell7.Text = "";
                    row.Cells.Add(cell7);
                    TableCell cell8 = new TableCell();
                    cell8.Text = f.Tore.ToString();
                    row.Cells.Add(cell8);
                    TableCell cell9 = new TableCell();
                    cell9.Text = "";
                    row.Cells.Add(cell9);
                    TableCell cell10 = new TableCell();
                    cell10.Text = "";
                    row.Cells.Add(cell10);
                    TableCell cell11 = new TableCell();
                    cell11.Text = f.Position;
                    row.Cells.Add(cell11);
                }
                else if (p is HandballSpieler)
                {
                    TableCell cell5 = new TableCell();
                    cell5.Text = "HandballSpieler";
                    row.Cells.Add(cell5);
                    HandballSpieler h = p as HandballSpieler;
                    TableCell cell6 = new TableCell();
                    cell6.Text = h.Spiele.ToString();
                    row.Cells.Add(cell6);
                    TableCell cell7 = new TableCell();
                    cell7.Text = h.Tore.ToString();
                    row.Cells.Add(cell7);
                    TableCell cell8 = new TableCell();
                    cell8.Text = "";
                    row.Cells.Add(cell8);
                    TableCell cell9 = new TableCell();
                    cell9.Text = "";
                    row.Cells.Add(cell9);
                    TableCell cell10 = new TableCell();
                    cell10.Text = "";
                    row.Cells.Add(cell10);
                    TableCell cell11 = new TableCell();
                    cell11.Text = h.Position;
                    row.Cells.Add(cell11);
                }
                else if (p is TennisSpieler)
                {
                    TableCell cell5 = new TableCell();
                    cell5.Text = "TennisSpieler";
                    row.Cells.Add(cell5);
                    TennisSpieler t = p as TennisSpieler;
                    TableCell cell6 = new TableCell();
                    cell6.Text = t.Spiele.ToString();
                    row.Cells.Add(cell6);
                    TableCell cell7 = new TableCell();
                    cell7.Text = "";
                    row.Cells.Add(cell7);
                    TableCell cell8 = new TableCell();
                    cell8.Text = t.Tore.ToString();
                    row.Cells.Add(cell8);
                    TableCell cell9 = new TableCell();
                    cell9.Text = "";
                    row.Cells.Add(cell9);
                    TableCell cell10 = new TableCell();
                    cell10.Text = "";
                    row.Cells.Add(cell10);
                    TableCell cell11 = new TableCell();
                    cell11.Text = "";
                    row.Cells.Add(cell11);
                }
                else if (p is Physiotherapeut)
                {
                    Physiotherapeut ph = p as Physiotherapeut;
                    TableCell cell5 = new TableCell();
                    cell5.Text = ph.Sportart;
                    row.Cells.Add(cell5);
                    TableCell cell6 = new TableCell();
                    cell6.Text = "";
                    row.Cells.Add(cell6);
                    TableCell cell7 = new TableCell();
                    cell7.Text = "";
                    row.Cells.Add(cell7);
                    TableCell cell8 = new TableCell();
                    cell8.Text = "";
                    row.Cells.Add(cell8);
                    TableCell cell9 = new TableCell();
                    cell9.Text = "";
                    row.Cells.Add(cell9);
                    TableCell cell10 = new TableCell();
                    cell10.Text = "";
                    row.Cells.Add(cell10);
                    TableCell cell11 = new TableCell();
                    cell11.Text = "";
                    row.Cells.Add(cell11);
                }
                else if (p is Trainer)
                {
                    Trainer t = p as Trainer;
                    TableCell cell5 = new TableCell();
                    cell5.Text = t.Sportart;
                    row.Cells.Add(cell5);
                    TableCell cell6 = new TableCell();
                    cell6.Text = "";
                    row.Cells.Add(cell6);
                    TableCell cell7 = new TableCell();
                    cell7.Text = "";
                    row.Cells.Add(cell7);
                    TableCell cell8 = new TableCell();
                    cell8.Text = "";
                    row.Cells.Add(cell8);
                    TableCell cell9 = new TableCell();
                    cell9.Text = "";
                    row.Cells.Add(cell9);
                    TableCell cell10 = new TableCell();
                    cell10.Text = "";
                    row.Cells.Add(cell10);
                    TableCell cell11 = new TableCell();
                    cell11.Text = "";
                    row.Cells.Add(cell11);
                }
                else
                {
                    TableCell cell5 = new TableCell();
                    if (p is Spieler)
                        cell5.Text = "Spieler";
                    else if (p is Mitarbeiter)
                        cell5.Text = "Mitarbeiter";
                    row.Cells.Add(cell5);
                    TableCell cell6 = new TableCell();
                    cell6.Text = "";
                    row.Cells.Add(cell6);
                    TableCell cell7 = new TableCell();
                    cell7.Text = "";
                    row.Cells.Add(cell7);
                    TableCell cell8 = new TableCell();
                    cell8.Text = "";
                    row.Cells.Add(cell8);
                    TableCell cell9 = new TableCell();
                    cell9.Text = "";
                    row.Cells.Add(cell9);
                    TableCell cell10 = new TableCell();
                    cell10.Text = "";
                    row.Cells.Add(cell10);
                    TableCell cell11 = new TableCell();
                    cell11.Text = "";
                    row.Cells.Add(cell11);
                }

                HyperLink link1 = new HyperLink();
                link1.NavigateUrl = "~/Personenverwaltung.aspx?do=bearbeiten&item=" + p.Person_ID;
                link1.Text = "Berabeiten";
                TableCell cell12 = new TableCell();
                cell12.Controls.Add(link1);
                row.Cells.Add(cell12);

                HyperLink link2 = new HyperLink();
                link2.NavigateUrl = "~/Personenverwaltung.aspx?do=entfernen&item=" + p.Person_ID;
                link2.Text = "Entfernen";

                TableCell cell13 = new TableCell();
                cell13.Controls.Add(link2);
                row.Cells.Add(cell13);

                Tbl.Rows.Add(row);
            }
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            User user = new User(Session);
            if (user.Has_Permission("admin"))
            {
                if (Request.Form["ctl00$MainContent$Txt_Vorname"] == "")
                {
                    Feld_Required();
                    Txt_Vorname_Container.Attributes["class"] = "form-group has-error";
                }
                else if (Request.Form["ctl00$MainContent$Txt_Name"] == "")
                {
                    Feld_Required();
                    Txt_Name_Container.Attributes["class"] = "form-group has-error";
                }
                else
                {
                    string name = Request.Form["ctl00$MainContent$Txt_Name"];
                    string vorname = Request.Form["ctl00$MainContent$Txt_Vorname"];
                    DateTime datum = Convert.ToDateTime(Request.Form["ctl00$MainContent$Txt_Datum"]);
                    string txt1 = Request.Form["ctl00$MainContent$Txt1"];
                    string txt2 = Request.Form["ctl00$MainContent$Txt2"];
                    string txt3 = Request.Form["ctl00$MainContent$Txt3"];
                    string sportart = Request.Form["ctl00$MainContent$Sportart"];
                    int spiele, tore, jahre, vereine;
                    if (RadioButtonListPersonenType.SelectedItem.Value == "Fussballspieler")
                    {
                        bool is_valid = true;
                        if (Int32.TryParse(txt1, out spiele))
                        {
                            if (Int32.TryParse(txt2, out tore))
                            {
                                //nichts
                            }
                            else
                            {
                                Txt2_Container.Attributes["class"] = "form-group has-error";
                                Feld_Required();
                                is_valid = false;
                            }
                        }
                        else
                        {
                            Txt1_Container.Attributes["class"] = "form-group has-error";
                            Feld_Required();
                            is_valid = false;
                        }
                        if (is_valid)
                        {
                            FussballSpieler fussballSpieler = new FussballSpieler(name, vorname, datum, Geschlecht.Maenlich, Convert.ToInt32(txt1), Convert.ToInt32(txt2), txt3);
                            Lbl_Msg.Visible = true;
                            fussballSpieler.Save();
                            Response.Redirect("~/Personenverwaltung.aspx");
                        }

                    }
                    else if (RadioButtonListPersonenType.SelectedItem.Value == "Handballspieler")
                    {
                        bool is_valid = true;
                        if (Int32.TryParse(txt1, out spiele))
                        {
                            if (Int32.TryParse(txt2, out tore))
                            {
                                //nichts
                            }
                            else
                            {
                                Txt2_Container.Attributes["class"] = "form-group has-error";
                                Feld_Required();
                                is_valid = false;
                            }
                        }
                        else
                        {
                            Txt1_Container.Attributes["class"] = "form-group has-error";
                            Feld_Required();
                            is_valid = false;
                        }
                        if (is_valid)
                        {
                            HandballSpieler handballSpieler = new HandballSpieler(name, vorname, datum, Geschlecht.Maenlich, Convert.ToInt32(txt1), Convert.ToInt32(txt2), txt3);
                            handballSpieler.Save();
                            Response.Redirect("~/Personenverwaltung.aspx");
                        }
                    }
                    else if (RadioButtonListPersonenType.SelectedItem.Value == "Tennisspieler")
                    {
                        bool is_valid = true;
                        if (Int32.TryParse(txt1, out spiele))
                        {
                            if (Int32.TryParse(txt2, out tore))
                            {
                                //nichts
                            }
                            else
                            {
                                Txt2_Container.Attributes["class"] = "form-group has-error";
                                Feld_Required();
                                is_valid = false;
                            }
                        }
                        else
                        {
                            Txt1_Container.Attributes["class"] = "form-group has-error";
                            Feld_Required();
                            is_valid = false;
                        }
                        if (is_valid)
                        {
                            TennisSpieler tennisSpieler = new TennisSpieler(name, vorname, datum, Geschlecht.Maenlich, Convert.ToInt32(txt1), Convert.ToInt32(txt2));
                            tennisSpieler.Save();
                            Response.Redirect("~/Personenverwaltung.aspx");
                        }
                    }
                    else if (RadioButtonListPersonenType.SelectedItem.Value == "anderer Spielertyp")
                    {
                        bool is_valid = true;
                        if (Int32.TryParse(txt1, out spiele))
                        {
                            if (Int32.TryParse(txt2, out tore))
                            {
                                //nichts
                            }
                            else
                            {
                                Txt2_Container.Attributes["class"] = "form-group has-error";
                                Feld_Required();
                                is_valid = false;
                            }
                        }
                        else
                        {
                            Txt1_Container.Attributes["class"] = "form-group has-error";
                            Feld_Required();
                            is_valid = false;
                        }
                        if (is_valid)
                        {
                            Spieler spieler = new Spieler(name, vorname, datum, Geschlecht.Maenlich, Convert.ToInt32(txt1), Convert.ToInt32(txt2), sportart);
                            spieler.Save();
                            Response.Redirect("~/Personenverwaltung.aspx");
                        }

                    }
                    else if (RadioButtonListPersonenType.SelectedItem.Value == "Physiotherapeut")
                    {
                        bool is_valid = true;
                        if (Int32.TryParse(txt1, out jahre))
                        {
                        }
                        else
                        {
                            Txt1_Container.Attributes["class"] = "form-group has-error";
                            Feld_Required();
                            is_valid = false;
                        }
                        if (is_valid)
                        {
                            Physiotherapeut physiotherapeut = new Physiotherapeut(name, vorname, datum, Geschlecht.Maenlich, Convert.ToInt32(txt1), sportart);
                            physiotherapeut.Save();
                            Response.Redirect("~/Personenverwaltung.aspx");
                        }
                    }
                    else if (RadioButtonListPersonenType.SelectedItem.Value == "Trainer")
                    {
                        bool is_valid = true;
                        if (Int32.TryParse(txt1, out vereine))
                        {
                        }
                        else
                        {
                            Txt1_Container.Attributes["class"] = "form-group has-error";
                            Feld_Required();
                            is_valid = false;
                        }
                        if (is_valid)
                        {
                            Trainer trainer = new Trainer(name, vorname, datum, Geschlecht.Maenlich, Convert.ToInt32(txt1), sportart);
                            trainer.Save();
                            Response.Redirect("~/Personenverwaltung.aspx");
                        }
                    }
                    else if (RadioButtonListPersonenType.SelectedItem.Value == "Person mit anderen Aufgaben")
                    {
                        Mitarbeiter mitarbeiter = new Mitarbeiter(name, vorname, datum, Geschlecht.Maenlich, txt1, sportart);
                        mitarbeiter.Save();
                        Response.Redirect("~/Personenverwaltung.aspx");
                    }
                }
            }
            else
            {
                Access_Denied();
            }
        }
        protected void Btn_Bearbeiten_Click(object sender, EventArgs e)
        {
            User user = new User(Session);
            if (user.Has_Permission("admin"))
            {
                long item = Convert.ToInt32(Request.QueryString["item"]);
                Person person = new Person(item);

                if (Request.Form["ctl00$MainContent$Txt_Vorname"] == "")
                {
                    Feld_Required();
                    Txt_Vorname_Container.Attributes["class"] = "form-group has-error";
                }
                else if (Request.Form["ctl00$MainContent$Txt_Name"] == "")
                {
                    Feld_Required();
                    Txt_Name_Container.Attributes["class"] = "form-group has-error";
                }
                else if (person.Art == "FussballSpieler")
                {
                    FussballSpieler s = new FussballSpieler(person.Art_ID);
                    bool is_valid = true;
                    if (Int32.TryParse(Request.Form["ctl00$MainContent$Txt1"], out int spiele))
                    {
                        s.Spiele = spiele;
                        if (Int32.TryParse(Request.Form["ctl00$MainContent$Txt2"], out int tore))
                        {
                            s.Tore = tore;
                        }
                        else
                        {
                            Txt2_Container.Attributes["class"] = "form-group has-error";
                            Feld_Required();
                            is_valid = false;
                        }
                    }
                    else
                    {
                        Txt1_Container.Attributes["class"] = "form-group has-error";
                        Feld_Required();
                        is_valid = false;
                    }
                    if (is_valid)
                    {
                        s.Position = Request.Form["ctl00$MainContent$Txt3"];
                        s.Name = Request.Form["ctl00$MainContent$Txt_Name"];
                        s.Vorname = Request.Form["ctl00$MainContent$Txt_Vorname"];
                        s.Geburtsdatum = Convert.ToDateTime(Request.Form["ctl00$MainContent$Txt_Datum"]);
                        s.Save();
                        Response.Redirect("~/Personenverwaltung.aspx");
                    }

                }
                else if (person.Art == "HandballSpieler")
                {
                    HandballSpieler s = new HandballSpieler(person.Art_ID);
                    bool is_valid = true;
                    if (Int32.TryParse(Request.Form["ctl00$MainContent$Txt1"], out int spiele))
                    {
                        s.Spiele = spiele;
                        if (Int32.TryParse(Request.Form["ctl00$MainContent$Txt2"], out int tore))
                        {
                            s.Tore = tore;
                        }
                        else
                        {
                            Txt2_Container.Attributes["class"] = "form-group has-error";
                            Feld_Required();
                            is_valid = false;
                        }
                    }
                    else
                    {
                        Txt1_Container.Attributes["class"] = "form-group has-error";
                        Feld_Required();
                        is_valid = false;
                    }
                    if (is_valid)
                    {
                        s.Position = Request.Form["ctl00$MainContent$Txt3"];
                        s.Name = Request.Form["ctl00$MainContent$Txt_Name"];
                        s.Vorname = Request.Form["ctl00$MainContent$Txt_Vorname"];
                        s.Geburtsdatum = Convert.ToDateTime(Request.Form["ctl00$MainContent$Txt_Datum"]);
                        s.Save();
                        Response.Redirect("~/Personenverwaltung.aspx");
                    }

                }
                else if (person.Art == "TennisSpieler")
                {
                    TennisSpieler s = new TennisSpieler(person.Art_ID);
                    bool is_valid = true;
                    if (Int32.TryParse(Request.Form["ctl00$MainContent$Txt1"], out int spiele))
                    {
                        s.Spiele = spiele;
                        if (Int32.TryParse(Request.Form["ctl00$MainContent$Txt2"], out int tore))
                        {
                            s.Tore = tore;
                        }
                        else
                        {
                            Txt2_Container.Attributes["class"] = "form-group has-error";
                            Feld_Required();
                            is_valid = false;
                        }
                    }
                    else
                    {
                        Txt1_Container.Attributes["class"] = "form-group has-error";
                        Feld_Required();
                        is_valid = false;
                    }
                    if (is_valid)
                    {
                        s.Name = Request.Form["ctl00$MainContent$Txt_Name"];
                        s.Vorname = Request.Form["ctl00$MainContent$Txt_Vorname"];
                        s.Geburtsdatum = Convert.ToDateTime(Request.Form["ctl00$MainContent$Txt_Datum"]);
                        s.Save();
                        Response.Redirect("~/Personenverwaltung.aspx");
                    }

                }
                else if (person.Art == "Spieler")
                {
                    Spieler s = new Spieler(person.Art_ID);
                    bool is_valid = true;
                    if (Int32.TryParse(Request.Form["ctl00$MainContent$Txt1"], out int spiele))
                    {
                        s.Spiele = spiele;
                        if (Int32.TryParse(Request.Form["ctl00$MainContent$Txt2"], out int tore))
                        {
                            s.Tore = tore;
                        }
                        else
                        {
                            Txt2_Container.Attributes["class"] = "form-group has-error";
                            Feld_Required();
                            is_valid = false;
                        }
                    }
                    else
                    {
                        Txt1_Container.Attributes["class"] = "form-group has-error";
                        Feld_Required();
                        is_valid = false;
                    }
                    if (is_valid)
                    {
                        s.Sportart = Request.Form["ctl00$MainContent$Sportart"];
                        s.Name = Request.Form["ctl00$MainContent$Txt_Name"];
                        s.Vorname = Request.Form["ctl00$MainContent$Txt_Vorname"];
                        s.Geburtsdatum = Convert.ToDateTime(Request.Form["ctl00$MainContent$Txt_Datum"]);
                        s.Save();
                        Response.Redirect("~/Personenverwaltung.aspx");
                    }

                }
                else if (person.Art == "Mitarbeiter")
                {
                    RenderMitarbeiterForm();
                    Mitarbeiter m = new Mitarbeiter(person.Art_ID);
                    m.Aufgabe = Request.Form["ctl00$MainContent$Txt1"];
                    m.Sportart = Request.Form["ctl00$MainContent$Sportart"];
                    m.Name = Request.Form["ctl00$MainContent$Txt_Name"];
                    m.Vorname = Request.Form["ctl00$MainContent$Txt_Vorname"];
                    m.Geburtsdatum = Convert.ToDateTime(Request.Form["ctl00$MainContent$Txt_Datum"]);
                    m.Save();
                    Response.Redirect("~/Personenverwaltung.aspx");
                }
                else if (person.Art == "Physiotherapeut")
                {
                    RenderPhysiotherapeutForm();
                    Physiotherapeut ph = new Physiotherapeut(person.Art_ID);
                    if (Int32.TryParse(Request.Form["ctl00$MainContent$Txt1"], out int jahre))
                    {
                        ph.Jahre = jahre;
                        Sportart.Text = Request.Form["ctl00$MainContent$Sportart"];
                        ph.Name = Request.Form["ctl00$MainContent$Txt_Name"];
                        ph.Vorname = Request.Form["ctl00$MainContent$Txt_Vorname"];
                        ph.Geburtsdatum = Convert.ToDateTime(Request.Form["ctl00$MainContent$Txt_Datum"]);
                        ph.Save();
                        Response.Redirect("~/Personenverwaltung.aspx");
                    }
                    else
                    {
                        Txt1_Container.Attributes["class"] = "form-group has-error";
                        Feld_Required();
                    }

                }
                else if (person.Art == "Trainer")
                {
                    RenderTrainerForm();
                    Trainer t = new Trainer(person.Art_ID);
                    if (Int32.TryParse(Request.Form["ctl00$MainContent$Txt1"], out int vereine))
                    {
                        t.Vereine = vereine;
                        t.Sportart = Request.Form["ctl00$MainContent$Sportart"];
                        t.Name = Request.Form["ctl00$MainContent$Txt_Name"];
                        t.Vorname = Request.Form["ctl00$MainContent$Txt_Vorname"];
                        t.Geburtsdatum = Convert.ToDateTime(Request.Form["ctl00$MainContent$Txt_Datum"]);
                        t.Save();
                        Response.Redirect("~/Personenverwaltung.aspx");
                    }
                    else
                    {
                        Txt1_Container.Attributes["class"] = "form-group has-error";
                        Feld_Required();
                    }
                }

            }
            else
            {
                Access_Denied();
            }
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Personenverwaltung.aspx");
        }

        protected void Btn_XMLDownload_Click(object sender, EventArgs e)
        {
            //Turnier turnier = new Turnier(Global.Personen);
            //XmlSerializer SR = new XmlSerializer(typeof(Turnier));
            //FileStream FS = new FileStream(Server.MapPath("~/Files")+"/Personen.xml", FileMode.Create);
            //SR.Serialize(FS, turnier);
            //FS.Close();
            //Response.Redirect("~/Files/Personen.xml");
        }
    }
}