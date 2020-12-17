#region Dateikopf
// Datei:       Trainer.cs
// Klasse:      Trainer
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
    public class Trainer : Person
    {
        #region Eigenschaften
        private long _Trainer_ID;
        private int _Vereine;
        private string _Sportart;
        #endregion

        #region Accessoren/Modifiers
        public int Vereine { get => _Vereine; set => _Vereine = value; }
        public string Sportart { get => _Sportart; set => _Sportart = value; }
        public long Trainer_ID { get => _Trainer_ID; set => _Trainer_ID = value; }
        #endregion

        #region Konstruktoren
        public Trainer() : base()
        {
            Name = "<Neuer Trainer>";
        }
        public Trainer(long trainer_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT * FROM `trainer` AS F INNER JOIN `person` AS P ON Trainer_ID = Art_ID AND Art = \"Trainer\" WHERE Trainer_ID = {0}", trainer_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Person_ID = long.Parse(reader["Person_ID"].ToString());
                                    Trainer_ID = trainer_id;
                                    Name = reader["Nachname"].ToString();
                                    Vorname = reader["Vorname"].ToString();
                                    Vereine = Convert.ToInt32(reader["Vereine"].ToString());
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
        public Trainer(Trainer trainer) : base(trainer)
        {
            Vereine = trainer.Vereine;
            Sportart = trainer.Sportart;
        }
        public Trainer(string name, string vorname, DateTime geburtsdatum, Geschlecht geschlecht, int vereine, string sportart) : base(name, vorname, geburtsdatum, geschlecht)
        {
            Vereine = vereine;
            Sportart = sportart;
        }
        #endregion

        #region Worker

        #endregion
    }
}
