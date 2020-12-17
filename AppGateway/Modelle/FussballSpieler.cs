#region Dateikopf
// Datei:       FussballSpieler.cs
// Klasse:      FussballSpieler
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
    public class FussballSpieler : Person
    {
        #region Eigenschaften
        private long _FussballSpieler_ID;
        private int _Spiele;
        private int _Tore;
        private string _Position;
        #endregion

        #region Accessoren/Modifiers
        public int Spiele { get => _Spiele; set => _Spiele = value; }
        public int Tore { get => _Tore; set => _Tore = value; }
        public string Position { get => _Position; set => _Position = value; }
        public long FussballSpieler_ID { get => _FussballSpieler_ID; set => _FussballSpieler_ID = value; }
        #endregion

        #region Konstruktorn
        public FussballSpieler() : base()
        {
            Name = "<Neuer FussballSpieler>";
        }
        public FussballSpieler(long fussballspieler_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT * FROM `fussballspieler` AS F INNER JOIN `person` AS P ON Fussballspieler_ID = Art_ID AND Art = \"FussballSpieler\" WHERE Fussballspieler_ID = {0}",fussballspieler_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Person_ID = long.Parse(reader["Person_ID"].ToString());
                                    FussballSpieler_ID = fussballspieler_id;
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
        public FussballSpieler(string name, string vorname, DateTime geburtsdatum, Geschlecht geschlecht, int spiele, int tore, string position) : base(name, vorname, geburtsdatum, geschlecht)
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
        //public static FussballSpieler FetchByID(long id)
        //{
        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
        //        {
        //            conn.Open();
        //            using (MySqlCommand cmd = conn.CreateCommand())
        //            {
        //                cmd.CommandText = string.Format("SELECT * FROM `fussballspieler` INNER JOIN `person` On `person`.`Art_ID` = `fussballspieler`.`Fussballspieler_ID` AND `person`.`Art` = \"FussballSpieler\" WHERE `fussballspieler`.`Fussballspieler_ID` = {0} ",id.ToString());
        //                using (MySqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        reader.Read();
        //                        long person_id = long.Parse(reader["Person_ID"].ToString());
        //                        int spiele = Convert.ToInt32(reader["Spiele"]);
        //                        int tore = Convert.ToInt32(reader["Tore"]);
        //                        string position = reader["Position"].ToString();
        //                        FussballSpieler fussballSpieler = new FussballSpieler();

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        #endregion
    }
}
