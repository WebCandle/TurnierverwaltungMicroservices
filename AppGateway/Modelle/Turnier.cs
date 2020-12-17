#region Dateikopf
// Datei:       Turnier.cs
// Klasse:      Mannschaft
// Datum:      03.06.2020
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Turnierverwaltung
{
    [Serializable]
    public class Turnier
    {
        #region Eigenschaften
        private long _Turnier_ID;
        private string _VereinName;
        private DateTime _Datum_Von;
        private DateTime _Datum_Bis;
        private string _Adresse;
        private List<Mannschaft> _Mannschaften;
        private List<Spiel> _Spiele;
        #endregion

        #region Accessoren/Modifiers
        public List<Mannschaft> Mannschaften { get => _Mannschaften; set => _Mannschaften = value; }
        public string VereinName { get => _VereinName; set => _VereinName = value; }
        public DateTime Datum_Von { get => _Datum_Von; set => _Datum_Von = value; }
        public DateTime Datum_Bis { get => _Datum_Bis; set => _Datum_Bis = value; }
        public string Adresse { get => _Adresse; set => _Adresse = value; }
        public long Turnier_ID { get => _Turnier_ID; set => _Turnier_ID = value; }
        public List<Spiel> Spiele { get => _Spiele; set => _Spiele = value; }
        #endregion

        #region Konstruktoren
        public Turnier(long turnier_id)
        {
            Turnier_ID = turnier_id;
            Mannschaften = FetchMannschaften(turnier_id);
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT `Turnier_ID`, `Verein_Name`, `Adresse`, `Datum_von`, `Datum_bis` FROM `turnier` WHERE `Turnier_ID` = {0} LIMIT 1", _Turnier_ID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Turnier_ID = long.Parse(reader["Turnier_ID"].ToString());
                                    VereinName = reader["Verein_Name"].ToString();
                                    Adresse = reader["Adresse"].ToString();
                                    Datum_Von = Convert.ToDateTime(reader["Datum_von"].ToString());
                                    Datum_Bis = Convert.ToDateTime(reader["Datum_bis"].ToString());
                                    GetSpiele();
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            } catch(Exception ex)
            {
                VereinName = "<Konnte nicht ermitteln!>";
                Adresse = "<Konnte nicht ermitteln!>";
                Datum_Von = DateTime.Now;
                Datum_Bis = DateTime.Now;
            }
            }
        public Turnier(string vereinName, DateTime von, DateTime bis, string adresse, List<Mannschaft> mannschaften)
        {
            VereinName = vereinName;
            Datum_Von = von;
            Datum_Bis = bis;
            Adresse = adresse;
            Mannschaften = mannschaften;
        }
        #endregion

        #region Worker
        public void Delete()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        string qry = string.Format("DELETE FROM `turnier` WHERE `Turnier_ID` = {0}", Turnier_ID);
                        cmd.CommandText = qry;
                        cmd.ExecuteNonQuery();
                        using (MySqlCommand cmd1 = conn.CreateCommand())
                        {
                            string qry1 = string.Format("DELETE FROM `turnier_mannschaft` WHERE `Turnier_ID` = {0}", Turnier_ID);
                            cmd1.CommandText = qry1;
                            cmd1.ExecuteNonQuery();
                        }

                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void Save()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        string qry;
                        if ( Turnier_ID == 0)
                        {
                            //insert
                            qry = string.Format("INSERT INTO `turnier`(`Verein_Name`, `Adresse`, `Datum_von`, `Datum_bis`) VALUES (\"{0}\",\"{1}\",STR_TO_DATE(\"{2}\", '%d.%m.%y'),STR_TO_DATE(\"{3}\", '%d.%m.%y'))", MySqlHelper.EscapeString(VereinName), MySqlHelper.EscapeString(Adresse), MySqlHelper.EscapeString(Datum_Von.ToShortDateString()), MySqlHelper.EscapeString(Datum_Bis.ToShortDateString()));
                            cmd.CommandText = qry;
                            cmd.ExecuteNonQuery();
                            Turnier_ID = cmd.LastInsertedId;
                        }
                        else
                        {
                            //update
                            cmd.CommandText = string.Format("DELETE FROM `turnier_mannschaft` WHERE `turnier_id` = {0}",Turnier_ID);
                            cmd.ExecuteNonQuery();
                            qry = string.Format("UPDATE `turnier` SET `Verein_Name`=\"{0}\",`Adresse`=\"{1}\",`Datum_von`=STR_TO_DATE(\"{2}\", '%d.%m.%y'),`Datum_bis`=STR_TO_DATE(\"{3}\", '%d.%m.%y') WHERE  `Turnier_ID`={4} LIMIT 1", MySqlHelper.EscapeString(VereinName), MySqlHelper.EscapeString(Adresse), MySqlHelper.EscapeString(Datum_Von.ToShortDateString()), MySqlHelper.EscapeString(Datum_Bis.ToShortDateString()),Turnier_ID);
                            cmd.CommandText = qry;
                            cmd.ExecuteNonQuery();
                        }

                        foreach (Mannschaft mannschaft in Mannschaften)
                        {
                            using (MySqlCommand cmd1 = conn.CreateCommand())
                            {
                                string qry1 = string.Format("INSERT INTO `turnier_mannschaft`(`Turnier_ID`, `Mannschaft_ID`) VALUES ({0},{1})", Turnier_ID, mannschaft.Mannschaft_ID);
                                cmd1.CommandText = qry1;
                                cmd1.ExecuteNonQuery();
                            }
                        }

                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static List<Turnier> GetAll()
        {
            //hat hier nicht akzeptiert !!
            List<Turnier> turniere = new List<Turnier>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM `turnier`";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    long turnier_id = long.Parse(reader["Turnier_ID"].ToString());
                                    Turnier turnier = new Turnier(turnier_id);
                                    turniere.Add(turnier);
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return turniere;
        }
        public static List<Mannschaft> FetchMannschaften(long trunier_id)
        {
            List<Mannschaft> mannschaften = new List<Mannschaft>();
            using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                        cmd.CommandText = "SELECT `Turnier_Mannschaft_ID`, `Turnier_ID`, `Mannschaft_ID` FROM `turnier_mannschaft` WHERE `Turnier_ID` = " + trunier_id.ToString();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    long mannschaft_id = long.Parse(reader["Mannschaft_ID"].ToString());
                                    mannschaften.Add(new Mannschaft(mannschaft_id));
                                }
                            }
                        }
                }
                conn.Close();
            }
            return mannschaften;
        }
        public List<Spiel> GetSpiele()
        {
            Spiele = new List<Spiel>();
            using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT `Spiel_ID`, `Turnier_ID`, `Mannschaft_ID`, `Punkte`, `Gegen_Mannschaft_ID`, `Gegen_Punkte` FROM `spiel` WHERE `Turnier_ID` = " + Turnier_ID.ToString();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                long spiel_id = long.Parse(reader["Spiel_ID"].ToString());
                                long mannschaft_id = long.Parse(reader["Mannschaft_ID"].ToString());
                                int punkte = Convert.ToInt32(reader["Punkte"].ToString());
                                long gegen_mannschaft_id = long.Parse(reader["Gegen_Mannschaft_ID"].ToString());
                                int gegen_punkte = Convert.ToInt32(reader["Gegen_Punkte"].ToString());
                                Spiel spiel = new Spiel(spiel_id, Turnier_ID, mannschaft_id, punkte, gegen_mannschaft_id, gegen_punkte);
                                Spiele.Add(spiel);
                            }
                        }
                    }
                }
                conn.Close();
            }
            return Spiele;
        }
        public void DeleteSpiel(long spiel_id)
        {
            Spiel spiel = Spiele.Where(x => x.Spiel_ID == spiel_id).First();
            spiel.Delete();
            Spiele.Remove(spiel);
        }
        public TTabelle getTabelle()
        {
            TTabelle tabelle = new TTabelle(Spiele);
            foreach (Mannschaft mannschaft in Mannschaften)
            {
                TRow row = new TRow();
                row.Mannschaft = mannschaft;
                row.Spiele = tabelle.getAnzahlSpiele(mannschaft.Mannschaft_ID);
                row.Siege = tabelle.getSiege(mannschaft.Mannschaft_ID);
                row.Unentschieden = tabelle.getUnentschieden(mannschaft.Mannschaft_ID);
                row.Niederlagen = tabelle.getNiederlagen(mannschaft.Mannschaft_ID);
                row.Tore = tabelle.getTore(mannschaft.Mannschaft_ID);
                row.gegenTore = tabelle.getGegenTore(mannschaft.Mannschaft_ID);
                row.Tordifferenz = row.Tore - row.gegenTore;
                tabelle.Rows.Add(row);
                
            }
            //var rows = tabelle.Rows.OrderByDescending(x => x.Tore).ThenByDescending(x=>x.Tordifferenz).ThenByDescending(x=>x.Siege).ThenBy(x => x.Niederlagen).ThenBy(x=>x.gegenTore).ToList() ;
            var rows = tabelle.Rows.OrderByDescending(x => x.Tore).ThenByDescending(x => x.Tordifferenz).ThenByDescending(x => x.Siege).ToList();
            tabelle.Rows = rows;
            return tabelle;
        }

        #endregion
    }
}
