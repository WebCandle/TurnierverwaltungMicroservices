#region Dateikopf
// Datei:       Mannschaft.cs
// Klasse:      Mannschaft
// Datum:      07.02.2020
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
    public class Mannschaft
    {
        #region Eigenschaften
        private long _Mannschaft_ID;
        private string _Name;
        private List<Person> _Mitglieder;
        private string _Sportart;
        #endregion

        #region Accessoren/Modifiers
        public string Name { get => _Name; set => _Name = value; }
        public List<Person> Mitglieder { get => _Mitglieder; set => _Mitglieder = value; }
        public string Sportart { get => _Sportart; set => _Sportart = value; }
        public long Mannschaft_ID { get => _Mannschaft_ID; set => _Mannschaft_ID = value; }
        #endregion

        #region Konstruktoren
        public Mannschaft()
        {
            Name = "<Neue Mannschaft>";
            Mitglieder = new List<Person>();
        }
        public Mannschaft(long id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT `Mannschaft_ID`, `Name`, `Sport_Art` FROM `mannschaft` WHERE `Mannschaft_ID` = {0} LIMIT 1",id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    _Mannschaft_ID = long.Parse(reader["Mannschaft_ID"].ToString());
                                    Name = reader["Name"].ToString();
                                    Sportart = reader["Sport_Art"].ToString();
                                    Mitglieder = Person.GetAllByMannschaftID(_Mannschaft_ID);
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Name = "<Konnte nicht ermitteln!>";
                Sportart = "<Konnte nicht ermitteln!>";
            }
        }
        public Mannschaft(string name, List<Person> personen)
        {
            Name = name;

            Mitglieder = personen;
        }
        public Mannschaft(long id, string name, string sportart)
        {
            Mannschaft_ID = id;
            Name = name;
            Sportart = sportart;
        }
        #endregion

        #region Worker
        public void MitgliedAnnehmen(Person person)
        {
            Mitglieder.Add(person);
        }
        public void MitgliedEntlassen(Person person)
        {
            Mitglieder.Remove(person);
        }
        public void SortMitgliederByName()
        {
            //Sort-Alguritmus basiert auf Bubblesort
            //bool PaarSortiert;
            //do
            //{
            //    PaarSortiert = true;
            //    for (int i = 0; i < Mitglieder.Count - 1; i++)
            //    {
            //        if (Mitglieder.ElementAt(i).CompareByName(Mitglieder.ElementAt(i + 1)) == 1)
            //        {
            //            Person temp = Mitglieder[i];
            //            Mitglieder[i] = Mitglieder[i + 1];
            //            Mitglieder[i + 1] = temp;
            //            PaarSortiert = false;
            //        }
            //    }
            //} while (!PaarSortiert);
        }
        public void Save()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd1 = conn.CreateCommand())
                    {
                        string qry1;
                        if (Mannschaft_ID != 0)
                        {
                            //Update
                            qry1 = string.Format("UPDATE `mannschaft` SET `Name`=\"{0}\",`Sport_Art`=\"{1}\" WHERE `Mannschaft_ID` = {2} LIMIT 1", Name, Sportart, Mannschaft_ID);
                            cmd1.CommandText = qry1;
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            //Insert
                            qry1 = string.Format("INSERT INTO `mannschaft`(`Name`, `Sport_Art`) VALUES (\"{0}\",\"{1}\")", Name, Sportart);
                            cmd1.CommandText = qry1;
                            cmd1.ExecuteNonQuery();
                            Mannschaft_ID = cmd1.LastInsertedId;
                        }
                        DeleteMitglieder();
                        foreach (Person person in Mitglieder)
                        {
                            qry1 = string.Format("INSERT INTO `mannschaft_mitglieder`( `Mannschaft_ID`, `Person_ID`) VALUES (\"{0}\",\"{1}\")", Mannschaft_ID, person.Person_ID);
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

        public void DeleteMitglieder()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd1 = conn.CreateCommand())
                    {
                        string qry1 = string.Format("DELETE FROM `mannschaft_mitglieder` WHERE `Mannschaft_ID` =  {0}", Mannschaft_ID);
                        cmd1.CommandText = qry1;
                        cmd1.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void Delete()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        string qry = string.Format("DELETE FROM `mannschaft` WHERE `Mannschaft_ID` = {0} LIMIT 1", Mannschaft_ID);
                        cmd.CommandText = qry;
                        cmd.ExecuteNonQuery();
                        DeleteMitglieder();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

        }
        public static List<Mannschaft> GetAll()
        {
            List<Mannschaft> mannschaften = new List<Mannschaft>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Mannschaft_ID FROM `mannschaft`";
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    long mannschaft_id = long.Parse(reader["Mannschaft_ID"].ToString());
                                    Mannschaft mannschaft = new Mannschaft(mannschaft_id);
                                    mannschaften.Add(mannschaft);
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
            return mannschaften;
        }
        #endregion
    }
}
