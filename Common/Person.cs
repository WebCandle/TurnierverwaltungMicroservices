#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      05.02.2021
#endregion

using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public abstract class Person : IPerson
    {
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public int MannschaftId { get; set; }

        public virtual void Asign(IPerson person)
        {
            Name = person.Name;
            Nachname = person.Nachname;
            Nachname = person.Nachname;
            Geburtsdatum = person.Geburtsdatum;
            MannschaftId = person.MannschaftId;
        }
    }
}
