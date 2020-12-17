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

namespace Turnierverwaltung
{
    public class TennisSpieler : Person
    {
        #region Eigenschaften
        private int _Spiele;
        private int _Tore;
        #endregion

        #region Accessoren/Modifiers
        public int Spiele { get => _Spiele; set => _Spiele = value; }
        public int Tore { get => _Tore; set => _Tore = value; }
        #endregion

        #region Konstruktorn
        public TennisSpieler() : base()
        {
            Name = "<Neuer TennisSpieler>";
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
