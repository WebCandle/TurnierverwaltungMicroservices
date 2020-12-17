#region Dateikopf
// Datei:       Spieler.cs
// Klasse:      Spiel
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
    public class Spiel
    {
        #region Eigenschaften
        private long _Spiel_ID;
        private long _Turnier_ID;
        private long _Mannschaft_ID;
        private int _Punkte;
        private long _gegen_Mannschaft_ID;
        private int _gegen_Punkte;
        #endregion

        #region Accessor/Modifier
        public long Spiel_ID { get => _Spiel_ID; set => _Spiel_ID = value; }
        public long Mannschaft_ID { get => _Mannschaft_ID; set => _Mannschaft_ID = value; }
        public int Punkte { get => _Punkte; set => _Punkte = value; }
        public long Gegen_Mannschaft_ID { get => _gegen_Mannschaft_ID; set => _gegen_Mannschaft_ID = value; }
        public int Gegen_Punkte { get => _gegen_Punkte; set => _gegen_Punkte = value; }
        public long Turnier_ID { get => _Turnier_ID; set => _Turnier_ID = value; }
        #endregion

        #region Konstruktoren
        public Spiel(long turnier_id, long mannschaft_id, int punkte, long gegen_mannschaft_id, int gegen_punkte)
        {
            Turnier_ID = turnier_id;
            Mannschaft_ID = mannschaft_id;
            Punkte = punkte;
            Gegen_Mannschaft_ID = gegen_mannschaft_id;
            Gegen_Punkte = gegen_punkte;
        }
        public Spiel(long spiel_id, long turnier_id, long mannschaft_id, int punkte, long gegen_mannschaft_id, int gegen_punkte)
        {
            Spiel_ID = spiel_id;
            Turnier_ID = turnier_id;
            Mannschaft_ID = mannschaft_id;
            Punkte = punkte;
            Gegen_Mannschaft_ID = gegen_mannschaft_id;
            Gegen_Punkte = gegen_punkte;
        }
        public Spiel(long spiel_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT `Spiel_ID`, `Turnier_ID`, `Mannschaft_ID`, `Punkte`, `Gegen_Mannschaft_ID`, `Gegen_Punkte` FROM `spiel` WHERE `Spiel_ID` =  {0} LIMIT 1", spiel_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Spiel_ID = long.Parse(reader["Spiel_ID"].ToString());
                                    Turnier_ID = long.Parse(reader["Turnier_ID"].ToString());
                                    Mannschaft_ID = long.Parse(reader["Mannschaft_ID"].ToString());
                                    Punkte = Convert.ToInt32(reader["Punkte"].ToString());
                                    Gegen_Mannschaft_ID = long.Parse(reader["Gegen_Mannschaft_ID"].ToString());
                                    Gegen_Punkte = Convert.ToInt32(reader["Gegen_Punkte"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            #endregion
        }
            #region Worker
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
                        if ( Spiel_ID > 0)
                        {
                            //Update
                            qry = string.Format("UPDATE `spiel` SET `Mannschaft_ID`={0},`Punkte`={1},`Gegen_Mannschaft_ID`={2},`Gegen_Punkte`={3} WHERE `Spiel_ID`={4}",Mannschaft_ID,Punkte,Gegen_Mannschaft_ID,Gegen_Punkte,Spiel_ID);
                            cmd.CommandText = qry;
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //Insert
                            qry = string.Format("INSERT INTO `spiel`(`Turnier_ID`, `Mannschaft_ID`, `Punkte`, `Gegen_Mannschaft_ID`, `Gegen_Punkte`) VALUES ({0},{1},{2},{3},{4})", Turnier_ID, Mannschaft_ID, Punkte, Gegen_Mannschaft_ID, Gegen_Punkte);
                            cmd.CommandText = qry;
                            cmd.ExecuteNonQuery();
                            Spiel_ID = cmd.LastInsertedId;
                        }
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
            if (Spiel_ID != 0)
            {
                try
                {

                    using (MySqlConnection conn = new MySqlConnection(Global.mySqlConnectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            string qry = string.Format("DELETE FROM `spiel` WHERE `Spiel_ID` = {0} LIMIT 1",Spiel_ID);
                            cmd.CommandText = qry;
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }

        }
        #endregion

    }
}