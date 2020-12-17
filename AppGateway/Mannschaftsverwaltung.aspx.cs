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
    public partial class Mannschaftsverwaltung : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = new User(Session);
            if(!user.Auth)
            {
                Session["redirect"] = "~/Mannschaftsverwaltung.aspx";
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                if (Page.IsPostBack == false)
                {
                    Sportart.Items.Clear();
                    Sportart.Items.Add("Fussball");
                    Sportart.Items.Add("Handball");
                    Sportart.Items.Add("Tennis");

                    if (Request.QueryString["do"] == "entfernen")
                    {
                        if (user.Has_Permission("admin"))
                        {
                            long mannschaft_id = long.Parse(Request.QueryString["item"]);
                            Mannschaft mannschaft = new Mannschaft(mannschaft_id);
                            mannschaft.Delete();
                            Response.Redirect("~/Mannschaftsverwaltung.aspx");
                        }
                        else
                        {
                            Access_Denied();
                        }
                    }
                    else if (Request.QueryString["do"] == "bearbeiten")
                    {
                        PnlMannschaften.Visible = false;
                        long mannschaft_id = long.Parse(Request.QueryString["item"]);
                        Mannschaft mannschaft = new Mannschaft(mannschaft_id);
                        Txt_Name.Text = mannschaft.Name;
                        Sportart.Text = mannschaft.Sportart;
                        Fill_Personen(mannschaft.Sportart, mannschaft);
                        Sportart.Enabled = false;
                        Btn_Add.Visible = false;
                        Btn_Sichern.Visible = true;
                        Btn_Abbrechen.Visible = true;
                    }
                    else
                    {
                        Fill_Personen("Fussball", null);
                        Sportart.Enabled = true;
                        PnlMannschaften.Visible = true;
                        Btn_Add.Visible = true;
                        Btn_Sichern.Visible = false;
                        Btn_Abbrechen.Visible = false;
                    }
                }
                Render();
            }
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

            TableHeaderCell h22 = new TableHeaderCell();
            h22.Text = "Sportart";
            header.Cells.Add(h22);

            TableHeaderCell h3 = new TableHeaderCell();
            h3.Text = "Mitglieder";
            header.Cells.Add(h3);
            TableHeaderCell h4 = new TableHeaderCell();
            h4.Text = "Bearbeiten";
            header.Cells.Add(h4);
            TableHeaderCell h5 = new TableHeaderCell();
            h5.Text = "Entfernen";
            header.Cells.Add(h5);


            Tbl.Rows.Add(header);

            foreach (Mannschaft m in Mannschaft.GetAll())
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.Text = m.Mannschaft_ID.ToString();
                row.Cells.Add(cell1);
                TableCell cell2 = new TableCell();
                cell2.Text = m.Name;
                row.Cells.Add(cell2);
                TableCell cell22 = new TableCell();
                cell22.Text = m.Sportart;
                row.Cells.Add(cell22);
                TableCell cell3 = new TableCell();
                cell3.Text = "";
                foreach (Person person in m.Mitglieder)
                {
                    cell3.Text += person.getName()+"<br />";
                }
                row.Cells.Add(cell3);

                HyperLink link1 = new HyperLink();
                link1.NavigateUrl = "~/Mannschaftsverwaltung.aspx?do=bearbeiten&item=" + m.Mannschaft_ID;
                link1.Text = "Berabeiten";
                TableCell cell12 = new TableCell();
                cell12.Controls.Add(link1);
                row.Cells.Add(cell12);

                HyperLink link2 = new HyperLink();
                link2.NavigateUrl = "~/Mannschaftsverwaltung.aspx?do=entfernen&item=" + m.Mannschaft_ID;
                link2.Text = "Entfernen";

                TableCell cell13 = new TableCell();
                cell13.Controls.Add(link2);
                row.Cells.Add(cell13);

                Tbl.Rows.Add(row);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(LstBxP.SelectedItem != null)
            {
                LstBxM.Items.Add(LstBxP.SelectedItem);
                LstBxP.Items.Remove(LstBxP.SelectedItem);
            }
        }

        protected void Btn2_Click(object sender, EventArgs e)
        {
            if(LstBxM.SelectedItem != null)
            {
                LstBxP.Items.Add(LstBxM.SelectedItem);
                LstBxM.Items.Remove(LstBxM.SelectedItem);
            }
        }

        protected void Btn_add(object sender, EventArgs e)
        {
            User user = new User(Session);
            if(user.Has_Permission("admin"))
            {
                if(Request.Form["ctl00$MainContent$Txt_Name"] != "")
                {
                    Mannschaft mannschaft = new Mannschaft();
                    mannschaft.Name = Request.Form["ctl00$MainContent$Txt_Name"];
                    mannschaft.Sportart = Request.Form["ctl00$MainContent$Sportart"];
                    var mitglieder = new string[0];
                    if (Request.Form["ctl00$MainContent$LstBxM"] != null)
                    {
                        mitglieder = Request.Form["ctl00$MainContent$LstBxM"].Split(',');
                    }
                    foreach (string person_id in mitglieder)
                    {
                        Person person = new Person(long.Parse(person_id));
                        mannschaft.MitgliedAnnehmen(person);
                    }
                    mannschaft.Save();
                    Render();
                    Txt_Name_Container.Attributes["class"] = "form-group";
                    Msg.Visible = false;
                }
                else
                {
                    Txt_Name_Container.Attributes["class"] = "form-group has-error";
                    Feld_Required();
                }

            }
            else
            {
                Access_Denied();
            }
        }
        protected void Btn_Abbrechen_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mannschaftsverwaltung.aspx");
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
        protected void Btn_Sichern_Click(object sender, EventArgs e)
        {
            User user = new User(Session);
            if (user.Has_Permission("admin"))
            {
                if (Request.Form["ctl00$MainContent$Txt_Name"] != "")
                {
                    Mannschaft mannschaft = new Mannschaft(long.Parse(Request.QueryString["item"]));
                    mannschaft.Name = Request.Form["ctl00$MainContent$Txt_Name"];
                    mannschaft.Mitglieder.Clear();
                    foreach (ListItem item in LstBxM.Items)
                    {
                        Person person = new Person(long.Parse(item.Value));
                        mannschaft.MitgliedAnnehmen(person);
                    }
                    mannschaft.Save();
                    Response.Redirect("~/Mannschaftsverwaltung.aspx");
                }
                else
                {
                    Txt_Name_Container.Attributes["class"] = "form-group has-error";
                    Feld_Required();
                }
            }
            else
            {
                Access_Denied();
            }
        }
        protected void Btn_XMLDownload_Click(object sender, EventArgs e)
        {
            //Turnier turnier = new Turnier(Global.Mannschaften);
            //XmlSerializer SR = new XmlSerializer(typeof(Turnier));
            //string name = Server.MapPath("~/Files") + "/Personen.xml";
            //FileStream FS = new FileStream(name, FileMode.Create);
            //SR.Serialize(FS, turnier);
            //FS.Close();
            //Response.Redirect("~/Files/Personen.xml");
            //Response.AddHeader("Content-Disposition", "attachment; filename="+name);
        }
        protected void Fill_Personen(string nach_sportart, Mannschaft mannschaft)
        {
            LstBxP.Items.Clear();
            LstBxM.Items.Clear();
            foreach (var person in Person.GetAll())
            {
                ListItem item = new ListItem(person.getName(), person.Person_ID.ToString());
                if (mannschaft != null)
                {
                    if (mannschaft.Mitglieder.Find(x => x.Person_ID == person.Person_ID) != null)
                    {
                        LstBxM.Items.Add(item);
                    }
                    else
                    {
                        if (mannschaft.Sportart == "Fussball")
                        {
                            if (person is HandballSpieler == false && person is TennisSpieler == false)
                            {
                                LstBxP.Items.Add(item);
                            }
                        }
                        else if (mannschaft.Sportart == "Handball")
                        {
                            if (person is FussballSpieler == false && person is TennisSpieler == false)
                            {
                                LstBxP.Items.Add(item);
                            }
                        }
                        else
                        {
                            //Teniss
                            if (person is FussballSpieler == false && person is HandballSpieler == false)
                            {
                                LstBxP.Items.Add(item);
                            }
                        }
                    }
                }
                else
                {
                    if (nach_sportart == "Fussball")
                    {
                        if (person is  HandballSpieler == false && person is TennisSpieler == false)
                        {
                            LstBxP.Items.Add(item);
                        }
                    }
                    else if (nach_sportart == "Handball")
                    {
                        if (person is FussballSpieler == false && person is TennisSpieler == false)
                        {
                            LstBxP.Items.Add(item);
                        }
                    }
                    else
                    {
                        //Teniss
                        if (person is FussballSpieler == false && person is HandballSpieler == false)
                        {
                            LstBxP.Items.Add(item);
                        }
                    }
                }
            }
        }
        protected void Sportart_SelectedIndexChanged(object sender, EventArgs e)
        {
            LstBxM.Items.Clear();
            Fill_Personen(Sportart.SelectedItem.Value,null);
        }
    }
}