#region Dateikopf
// Datei:       TennisSpieler.cs
// Klasse:      TennisSpieler
// Datum:      02.06.2020
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace Turnierverwaltung
{
    [Serializable]
    public class Spieler: Person
    {
        #region Eigenschaften
        private long _Spieler_ID;
        private int _Spiele;
        private int _Tore;
        private string _Sportart;
        #endregion

        #region Accessoren/Modifiers
        public int Spiele { get => _Spiele; set => _Spiele = value; }
        public int Tore { get => _Tore; set => _Tore = value; }
        public string Sportart { get => _Sportart; set => _Sportart = value; }
        public long Spieler_ID { get => _Spieler_ID; set => _Spieler_ID = value; }
        #endregion

        #region Konstruktorn
        public Spieler() : base()
        {
            Name = "<Neuer Spieler>";
        }
        public Spieler(long spieler_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT * FROM `spieler` AS F INNER JOIN `person` AS P ON F.Spieler_ID = P.Art_ID AND Art = \"Spieler\" WHERE Spieler_ID = {0}", spieler_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Person_ID = long.Parse(reader["Person_ID"].ToString());
                                    Spieler_ID = spieler_id;
                                    Name = reader["Nachname"].ToString();
                                    Vorname = reader["Vorname"].ToString();
                                    Spiele = Convert.ToInt32(reader["Spiele"].ToString());
                                    Tore = Convert.ToInt32(reader["Gewonnene_Spiele"].ToString());
                                    Sportart = reader["Sport_Art"].ToString();
                                    Geburtsdatum = Convert.ToDateTime(reader["Geburtsdatum"].ToString());
                                    Art = reader["Art"].ToString();
                                    Art_ID = long.Parse(reader["Art_ID"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public Spieler(string name, string vorname, DateTime geburtsdatum, Geschlecht geschlecht, int spiele, int tore, string sportart) : base(name, vorname, geburtsdatum, geschlecht)
        {
            Spiele = spiele;
            Tore = tore;
            Sportart = sportart;
        }
        #endregion

        #region Worker

        #endregion
    }
}
