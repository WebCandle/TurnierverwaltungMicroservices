using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Turnierverwaltung
{
    public partial class TurnierTabelle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = new User(Session);
            if (!user.Auth)
            {
                Session["redirect"] = "~/TurnierTabelle.aspx";
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {

                if (Request.QueryString["item"] != null)
                {
                    long turnier_id = long.Parse(Request.QueryString["item"]);
                    if (turnier_id > 0)
                    {
                        Turnier turnier = new Turnier(turnier_id);
                        PrintTurnier(turnier);
                    }
                    else
                    {
                        PrintAll();
                    }
                }
                else
                {
                    PrintAll();
                }
            }
        }
        public void PrintTurnier(Turnier turnier)
        {
            Label lbl = new Label();
            lbl.Text = string.Format("<h2>{0}</h2>", turnier.VereinName);
            Pnl.Controls.Add(lbl);

            Table tbl = new Table();
            tbl.GridLines = GridLines.Both;
            tbl.Width = Unit.Percentage(100);
            TableHeaderRow headerRow = new TableHeaderRow();

            TableHeaderCell hc1 = new TableHeaderCell();
            hc1.Text = "Platz";
            headerRow.Cells.Add(hc1);

            TableHeaderCell hc2 = new TableHeaderCell();
            hc2.Text = "Mannschaft";
            headerRow.Cells.Add(hc2);

            TableHeaderCell hc3 = new TableHeaderCell();
            hc3.Text = "Spiele";
            headerRow.Cells.Add(hc3);

            TableHeaderCell hc4 = new TableHeaderCell();
            hc4.Text = "Siege";
            headerRow.Cells.Add(hc4);

            TableHeaderCell hc5 = new TableHeaderCell();
            hc5.Text = "Unentschieden";
            headerRow.Cells.Add(hc5);

            TableHeaderCell hc6 = new TableHeaderCell();
            hc6.Text = "Niederlage";
            headerRow.Cells.Add(hc6);

            TableHeaderCell hc7 = new TableHeaderCell();
            hc7.Text = "Tore";
            headerRow.Cells.Add(hc7);

            TableHeaderCell hc8 = new TableHeaderCell();
            hc8.Text = "Gegentore";
            headerRow.Cells.Add(hc8);

            TableHeaderCell hc9 = new TableHeaderCell();
            hc9.Text = "Tordifferenz";
            headerRow.Cells.Add(hc9);

            tbl.Rows.Add(headerRow);
            TTabelle tabelle = turnier.getTabelle();
            int platz = 1;
            foreach (TRow trow in tabelle.Rows)
            {
                TableRow row = new TableRow();
                //
                TableCell c1 = new TableCell();
                c1.Text = platz.ToString();
                row.Cells.Add(c1);
                //
                TableCell c2 = new TableCell();
                c2.Text = trow.Mannschaft.Name;
                row.Cells.Add(c2);
                //
                TableCell c3 = new TableCell();
                c3.Text = trow.Spiele.ToString();
                row.Cells.Add(c3);
                //
                TableCell c4 = new TableCell();
                c4.Text = trow.Siege.ToString();
                row.Cells.Add(c4);
                //
                TableCell c5 = new TableCell();
                c5.Text = trow.Unentschieden.ToString();
                row.Cells.Add(c5);
                //
                TableCell c6 = new TableCell();
                c6.Text = trow.Niederlagen.ToString();
                row.Cells.Add(c6);
                //
                TableCell c7 = new TableCell();
                c7.Text = trow.Tore.ToString();
                row.Cells.Add(c7);
                //
                TableCell c8 = new TableCell();
                c8.Text = trow.gegenTore.ToString();
                row.Cells.Add(c8);
                //
                TableCell c9 = new TableCell();
                c9.Text = trow.Tordifferenz.ToString();
                row.Cells.Add(c9);
                //
                tbl.Rows.Add(row);
                platz++;
            }

            Pnl.Controls.Add(tbl);
        }
        public void PrintAll()
        {
            foreach (Turnier turnier in Turnier.GetAll())
            {
                PrintTurnier(turnier);
            }
        }
    }

}