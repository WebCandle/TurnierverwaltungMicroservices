#region Dateikopf
// Datei:       Person.cs
// Klasse:      Person
// Datum:      06.02.2020
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnierverwaltung
{
    public abstract class Person
    {
        #region Eigenschaften
        private string _Name;
        private string _Vorname;
        private DateTime _Geburtsdatum;
        private Geschlecht _Geschlecht;
        #endregion

        #region Accessor/Modifiers
        public string Name { get => _Name; set => _Name = value; }
        public DateTime Geburtsdatum { get => _Geburtsdatum; set => _Geburtsdatum = value; }
        public Geschlecht Geschlecht { get => _Geschlecht; set => _Geschlecht = value; }
        public string Vorname { get => _Vorname; set => _Vorname = value; }
        #endregion

        #region Konstruktoren
        public Person()
        {
            Name = "";
            Geburtsdatum = DateTime.Now;
            Geschlecht = Geschlecht.Maenlich;
        }
        public Person(Person person)
        {
            Name = person.Name;
            Vorname = person.Vorname;
            Geburtsdatum = person.Geburtsdatum;
            Geschlecht = person.Geschlecht;
        }
        public Person(string name, string vorname, DateTime geburtsdatum, Geschlecht geschlecht)
        {
            Name = name;
            Vorname = vorname;
            Geburtsdatum = geburtsdatum;
            Geschlecht = geschlecht;
        }
        #endregion

        #region Worker
        
        #endregion
    }
}
