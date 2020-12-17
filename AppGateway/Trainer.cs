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

namespace Turnierverwaltung
{
    public class Trainer : Person
    {
        #region Eigenschaften
        private int _Vereine;
        private string _Sportart;
        #endregion

        #region Accessoren/Modifiers
        public int Vereine { get => _Vereine; set => _Vereine = value; }
        public string Sportart { get => _Sportart; set => _Sportart = value; }
        #endregion

        #region Konstruktoren
        public Trainer() : base()
        {
            Name = "<Neuer Trainer>";
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
