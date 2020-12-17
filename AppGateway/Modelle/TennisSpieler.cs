#region Dateikopf
// Datei:       TennisSpieler.cs
// Klasse:      TennisSpieler
// Datum:      07.02.2020
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
    public class TennisSpieler : Person
    {
        #region Eigenschaften
        private long _TennisSpieler_ID;
        private int _Spiele;
        private int _Tore;
        #endregion

        #region Accessoren/Modifiers
        public int Spiele { get => _Spiele; set => _Spiele = value; }
        public int Tore { get => _Tore; set => _Tore = value; }
        public long TennisSpieler_ID { get => _TennisSpieler_ID; set => _TennisSpieler_ID = value; }
        #endregion

        #region Konstruktorn
        public TennisSpieler() : base()
        {
            Name = "<Neuer TennisSpieler>";
        }
        public TennisSpieler(long tennisspieler_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT * FROM `tennisspieler` AS F INNER JOIN `person` AS P ON Tennisspieler_ID = Art_ID AND Art = \"TennisSpieler\" WHERE Tennisspieler_ID = {0}", tennisspieler_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Person_ID = long.Parse(reader["Person_ID"].ToString());
                                    TennisSpieler_ID = long.Parse(reader["Tennisspieler_ID"].ToString());
                                    Name = reader["Nachname"].ToString();
                                    Vorname = reader["Vorname"].ToString();
                                    Spiele = Convert.ToInt32(reader["Spiele"].ToString());
                                    Tore = Convert.ToInt32(reader["Tore"].ToString());
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
        public TennisSpieler(string name, string vorname, DateTime geburtsdatum, Geschlecht geschlecht, int spiele, int tore) : base(name, vorname, geburtsdatum, geschlecht)
        {
            Spiele = spiele;
            Tore = tore;
        }
        #endregion

        #region Worker

        #endregion
    }
}
