using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Turnierverwaltung
{
    public partial class Turnierverwaltung : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = new User(Session);
            if (!user.Auth)
            {
                Session["redirect"] = "~/Turnierverwaltung.aspx";
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    txtDatumVon.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtDatumBis.Text = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
                    CheckBxLstMannschaften.Items.Clear();
                    List<Mannschaft> mannschaften = Mannschaft.GetAll();
                    foreach (Mannschaft mannschaft in mannschaften)
                    {
                        CheckBxLstMannschaften.Items.Add(new ListItem("&nbsp;" + mannschaft.Name, mannschaft.Mannschaft_ID.ToString()));
                    }
                    if (Request.QueryString["do"] == "entfernen")
                    {
                        if(user.Has_Permission("admin"))
                        {
                            long turnier_id = long.Parse(Request.QueryString["item"]);
                            Turnier turnier = new Turnier(turnier_id);
                            turnier.Delete();
                            Response.Redirect("~/Turnierverwaltung.aspx", true);
                        }
                        else
                        {
                            Access_Denied();
                        }
                    }
                    else if (Request.QueryString["do"] == "bearbeiten")
                    {
                        long turnier_id = long.Parse(Request.QueryString["item"]);
                        Turnier turnier = new Turnier(turnier_id);
                        txtVereinName.Text = turnier.VereinName;
                        txtAdresse.Text = turnier.Adresse;
                        txtDatumVon.Text = turnier.Datum_Von.ToString("yyyy-MM-dd");
                        txtDatumBis.Text = turnier.Datum_Bis.ToString("yyyy-MM-dd");
                        tbl_Mannschaften.Visible = false;
                        btnAdd.Visible = false;
                        Btn_Cancel.Visible = true;
                        Btn_Bearbeiten.Visible = true;
                        PnlTurniere.Visible = false;
                    }
                }
                Render();
            }
                   
        }
        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Turnierverwaltung.aspx",true);
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            User user = new User(Session);
            if(user.Has_Permission("admin"))
            {
                if(Request.Form["ctl00$MainContent$txtVereinName"] != "")
                {
                    List<Mannschaft> mannschaften = new List<Mannschaft>();
                    foreach (ListItem item in CheckBxLstMannschaften.Items)
                    {
                        if (item.Selected)
                        {
                            mannschaften.Add(new Mannschaft(long.Parse(item.Value)));
                        }
                    }
                    Turnier turnier = new Turnier(Request.Form["ctl00$MainContent$txtVereinName"], Convert.ToDateTime(Request.Form["ctl00$MainContent$txtDatumVon"]), Convert.ToDateTime(Request.Form["ctl00$MainContent$txtDatumBis"]), Request.Form["ctl00$MainContent$txtAdresse"], mannschaften);
                    turnier.Save();
                    Response.Redirect("~/Turnierverwaltung.aspx", true);
                }
                else
                {
                    Feld_Required();
                    txtVereinName_container.Attributes["class"] = "form-group has-error";
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
                if (Request.Form["ctl00$MainContent$txtVereinName"] != "")
                {
                    long item = Convert.ToInt32(Request.QueryString["item"]);
                    Turnier turnier = new Turnier(item);
                    turnier.VereinName = Request.Form["ctl00$MainContent$txtVereinName"];
                    turnier.Adresse = Request.Form["ctl00$MainContent$txtAdresse"];
                    turnier.Datum_Bis = Convert.ToDateTime(Request.Form["ctl00$MainContent$txtDatumBis"]);
                    turnier.Datum_Von = Convert.ToDateTime(Request.Form["ctl00$MainContent$txtDatumVon"]);
                    turnier.Save();
                    Response.Redirect("~/Turnierverwaltung.aspx", true);
                }
                else
                {
                    Feld_Required();
                    txtVereinName_container.Attributes["class"] = "form-group has-error";
                }
            }
            else
            {
                Access_Denied();
            }
        }
        private void Render()
        {
            Tbl.Rows.Clear();

            TableHeaderRow header = new TableHeaderRow();

            TableHeaderCell h1 = new TableHeaderCell();
            h1.Text = "Verein";
            header.Cells.Add(h1);
            TableHeaderCell h2 = new TableHeaderCell();
            h2.Text = "Adresse";
            header.Cells.Add(h2);

            TableHeaderCell h22 = new TableHeaderCell();
            h22.Text = "Mannschaften";
            header.Cells.Add(h22);

            TableHeaderCell h3 = new TableHeaderCell();
            h3.Text = "Von/Bis";
            header.Cells.Add(h3);
            TableHeaderCell h4 = new TableHeaderCell();
            h4.Text = "Spiele/Tabelle";
            header.Cells.Add(h4);

            TableHeaderCell h50 = new TableHeaderCell();
            h50.Text = "Bearbeiten";
            header.Cells.Add(h50);

            TableHeaderCell h5 = new TableHeaderCell();
            h5.Text = "Entfernen";
            header.Cells.Add(h5);


            Tbl.Rows.Add(header);

            foreach (Turnier turnier in Turnier.GetAll())
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.Text = turnier.VereinName;
                row.Cells.Add(cell1);
                TableCell cell2 = new TableCell();
                cell2.Text = turnier.Adresse;
                row.Cells.Add(cell2);
                TableCell cell3 = new TableCell();
                cell3.Text = "";
                foreach (Mannschaft mannschaft in turnier.Mannschaften)
                {
                    cell3.Text += mannschaft.Name+ "<br />";
                }
                row.Cells.Add(cell3);

                TableCell cell222 = new TableCell();
                cell222.Text = turnier.Datum_Von.ToShortDateString()+"/"+turnier.Datum_Bis.ToShortDateString();
                row.Cells.Add(cell222);

                HyperLink link1 = new HyperLink();
                link1.NavigateUrl = "~/Spiele.aspx?item=" + turnier.Turnier_ID.ToString();
                link1.Text = "Spiele";
                HyperLink link12 = new HyperLink();
                link12.NavigateUrl = "~/TurnierTabelle.aspx?item=" + turnier.Turnier_ID.ToString();
                link12.Text = "Tabelle";
                Label l = new Label();
                l.Text = " / ";
                TableCell cell12 = new TableCell();
                cell12.Controls.Add(link1);
                cell12.Controls.Add(l);
                cell12.Controls.Add(link12);
                row.Cells.Add(cell12);

                HyperLink link3 = new HyperLink();
                link3.NavigateUrl = "~/Turnierverwaltung.aspx?do=bearbeiten&item=" + turnier.Turnier_ID.ToString();
                link3.Text = "Bearbeiten";

                TableCell cell133 = new TableCell();
                cell133.Controls.Add(link3);
                row.Cells.Add(cell133);

                HyperLink link2 = new HyperLink();
                link2.NavigateUrl = "~/Turnierverwaltung.aspx?do=entfernen&item=" + turnier.Turnier_ID.ToString();
                link2.Text = "Entfernen";

                TableCell cell13 = new TableCell();
                cell13.Controls.Add(link2);
                row.Cells.Add(cell13);

                Tbl.Rows.Add(row);
            }
        }

        protected void Btn_XMLDownload_Click(object sender, EventArgs e)
        {

        }
    }
}