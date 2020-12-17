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

namespace Turnierverwaltung
{
    public class HandballSpieler : Person
    {
        #region Eigenschaften
        private int _Spiele;
        private int _Tore;
        private string _Position;
        #endregion


        #region Accessoren/Modifiers
        public int Spiele { get => _Spiele; set => _Spiele = value; }
        public int Tore { get => _Tore; set => _Tore = value; }
        public string Position { get => _Position; set => _Position = value; }
        #endregion

        #region Konstruktorn
        public HandballSpieler() : base()
        {
            Name = "<Neuer HandballSpieler>";
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
