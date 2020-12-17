#region Dateikopf
// Datei:       HandballSpieler.cs
// Klasse:      HandballSpieler
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
    public class HandballSpieler : Person
    {
        #region Eigenschaften
        private long _HandballSpieler_ID;
        private int _Spiele;
        private int _Tore;
        private string _Position;
        #endregion


        #region Accessoren/Modifiers
        public int Spiele { get => _Spiele; set => _Spiele = value; }
        public int Tore { get => _Tore; set => _Tore = value; }
        public string Position { get => _Position; set => _Position = value; }
        public long HandballSpieler_ID { get => _HandballSpieler_ID; set => _HandballSpieler_ID = value; }
        #endregion

        #region Konstruktorn
        public HandballSpieler() : base()
        {
            Name = "<Neuer HandballSpieler>";
        }
        public HandballSpieler(long handballspieler_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT * FROM `HandballSpieler` AS F INNER JOIN `person` AS P ON HandballSpieler_ID = Art_ID AND Art = \"HandballSpieler\" WHERE HandballSpieler_ID = {0}", handballspieler_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Person_ID = long.Parse(reader["Person_ID"].ToString());
                                    HandballSpieler_ID = handballspieler_id;
                                    Name = reader["Nachname"].ToString();
                                    Vorname = reader["Vorname"].ToString();
                                    Spiele = Convert.ToInt32(reader["Spiele"].ToString());
                                    Tore = Convert.ToInt32(reader["Tore"].ToString());
                                    Position = reader["Position"].ToString();
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
        public HandballSpieler(string name, string vorname, DateTime geburtsdatum, Geschlecht geschlecht, int spiele, int tore, string position) : base(name, vorname, geburtsdatum, geschlecht)
        {
            Spiele = spiele;
            Tore = tore;
            Position = position;
        }
        #endregion

        #region Worker
        public void PositionAendern(string position)
        {
            Position = position;
        }
        #endregion
    }
}
