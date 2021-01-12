using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Spieler : Person
    {
        string Aufgabe { get; set; }

        public Spieler()
        {
            PersonId = 0;
            Name = string.Empty;
            Nachname = string.Empty;
            Geburtsdatum = DateTime.Now;
            MannschaftId = 0;
            Aufgabe = string.Empty;
        }

        public override void Asign(IPerson person)
        {
            base.Asign(person);
            Aufgabe = (person as Spieler).Aufgabe;
        }
    }
}
