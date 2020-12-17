using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnierverwaltung
{
    public class TRow
    {
        public Mannschaft Mannschaft { set; get; }
        public int Spiele { set; get; }
        public int Siege { set; get; }
        public int Unentschieden { set; get; }
        public int Niederlagen { set; get; }
        public int Tore { set; get; }
        public int gegenTore { set; get; }
        public int Tordifferenz { set; get; }
        public int Punkte { set; get; }
    }
    public class TTabelle
    {
        public List<TRow> Rows { get; set; }
        public List<Spiel> Spiele { get; set; }
        public TTabelle(List<Spiel> spiele)
        {
            Rows = new List<TRow>();
            Spiele = spiele;
        }
        public int getAnzahlSpiele(long mannschaft_id)
        {
            int anzahl = 0;
            foreach (Spiel spiel in Spiele)
            {
                if (spiel.Mannschaft_ID == mannschaft_id || spiel.Gegen_Mannschaft_ID == mannschaft_id)
                    anzahl++;
            }
            return anzahl;
        }
        public int getSiege(long mannschaft_id)
        {
            int siege = 0;
            foreach (Spiel spiel in Spiele)
            {
                if ((spiel.Mannschaft_ID == mannschaft_id && spiel.Punkte > spiel.Gegen_Punkte) || (spiel.Gegen_Mannschaft_ID == mannschaft_id && spiel.Gegen_Punkte > spiel.Punkte) )
                    siege++;

            }
            return siege;
        }
        public int getUnentschieden(long mannschaft_id)
        {
            int unentschieden = 0;
            foreach (Spiel spiel in Spiele)
            {
                if ((spiel.Mannschaft_ID == mannschaft_id && spiel.Punkte == spiel.Gegen_Punkte) || (spiel.Gegen_Mannschaft_ID == mannschaft_id && spiel.Gegen_Punkte == spiel.Punkte))
                    unentschieden++;

            }
            return unentschieden;
        }
        public int getNiederlagen(long mannschaft_id)
        {
            int niederlagen = 0;
            foreach (Spiel spiel in Spiele)
            {
                if ((spiel.Mannschaft_ID == mannschaft_id && spiel.Punkte < spiel.Gegen_Punkte) || (spiel.Gegen_Mannschaft_ID == mannschaft_id && spiel.Gegen_Punkte < spiel.Punkte))
                    niederlagen++;

            }
            return niederlagen;
        }
        public int getTore(long mannschaft_id)
        {
            int tore = 0;
            foreach (Spiel spiel in Spiele)
            {
                if (spiel.Mannschaft_ID == mannschaft_id)
                    tore += spiel.Punkte;
                else if (spiel.Gegen_Mannschaft_ID == mannschaft_id)
                    tore += spiel.Gegen_Punkte;

            }
            return tore;
        }
        public int getGegenTore(long mannschaft_id)
        {
            int tore = 0;
            foreach (Spiel spiel in Spiele)
            {
                if (spiel.Mannschaft_ID == mannschaft_id)
                    tore += spiel.Gegen_Punkte;
                else if (spiel.Gegen_Mannschaft_ID == mannschaft_id)
                    tore += spiel.Punkte;

            }
            return tore;
        }
    }
}