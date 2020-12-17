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
    public class Mitarbeiter : Person
    {
        #region Eigenschaften
        private long _Mitarbeiter_ID;
        private string _Aufgabe;
        private string _Sportart;
        #endregion

        #region Accessoren/Modifiers
        public string Sportart { get => _Sportart; set => _Sportart = value; }
        public string Aufgabe { get => _Aufgabe; set => _Aufgabe = value; }
        public long Mitarbeiter_ID { get => _Mitarbeiter_ID; set => _Mitarbeiter_ID = value; }
        #endregion

        #region Konstruktorn
        public Mitarbeiter() : base()
        {
            Name = "<Neuer Mitarbeiter>";
        }
        public Mitarbeiter(long mitarbeiter_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT * FROM `mitarbeiter` AS F INNER JOIN `person` AS P ON Mitarbeiter_ID = Art_ID AND Art = \"Mitarbeiter\" WHERE Mitarbeiter_ID = {0}", mitarbeiter_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Person_ID = long.Parse(reader["Person_ID"].ToString());
                                    Mitarbeiter_ID = mitarbeiter_id;
                                    Name = reader["Nachname"].ToString();
                                    Vorname = reader["Vorname"].ToString();
                                    Aufgabe = reader["Aufgabe"].ToString();
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
        public Mitarbeiter(string name, string vorname, DateTime geburtsdatum, Geschlecht geschlecht, string aufgabe, string sportart) : base(name, vorname, geburtsdatum, geschlecht)
        {
            Aufgabe = aufgabe;
            Sportart = sportart;
        }
        #endregion

        #region Worker

        #endregion
    }
}
