#region Dateikopf
// Datei:       Physiotherapeut.cs
// Klasse:      Physiotherapeut
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
    public class Physiotherapeut : Person
    {
        #region Eigenschaften
        private long _Physiotherapeut_ID;
        private int _Jahre;
        private string _Sportart;
        #endregion

        #region Accessoren/Modifiers
        public int Jahre { get => _Jahre; set => _Jahre = value; }
        public string Sportart { get => _Sportart; set => _Sportart = value; }
        public long Physiotherapeut_ID { get => _Physiotherapeut_ID; set => _Physiotherapeut_ID = value; }
        #endregion

        #region Konstuktoren
        public Physiotherapeut() : base()
        {
            Name = "<Neuer Physiotherapeut>";
        }
        public Physiotherapeut(long physiotherapeut_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT * FROM `physiotherapeut` AS F INNER JOIN `person` AS P ON Physiotherapeut_ID = Art_ID AND Art = \"Physiotherapeut\" WHERE Physiotherapeut_ID = {0}", physiotherapeut_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Person_ID = long.Parse(reader["Person_ID"].ToString());
                                    Physiotherapeut_ID = physiotherapeut_id;
                                    Name = reader["Nachname"].ToString();
                                    Vorname = reader["Vorname"].ToString();
                                    Jahre = Convert.ToInt32(reader["Jahre"].ToString());
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
        public Physiotherapeut(Physiotherapeut physiotherapeut) : base(physiotherapeut)
        {
            Jahre = physiotherapeut.Jahre;
            Sportart = physiotherapeut.Sportart;
        }
        public Physiotherapeut(string name, string vorname, DateTime geburtsdatum, Geschlecht geschlecht, int jahre, string sportart) : base(name, vorname, geburtsdatum, geschlecht)
        {
            Jahre = jahre;
            Sportart = sportart;
        }
        #endregion

        #region Worker

        #endregion
    }
}
