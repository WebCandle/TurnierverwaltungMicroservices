using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Trainer : Person
    {
        public decimal Gehalt { get; set; }

        public Trainer()
        {
            PersonId = 0;
            Name = string.Empty;
            Nachname = string.Empty;
            Geburtsdatum = DateTime.Now;
            MannschaftId = 0;
            Gehalt = 0;
        }

        public Trainer(string name, string nachname, DateTime dateTime, int mannschaftId, decimal gehalt)
        {
            Name = name;
            Nachname = nachname;
            Geburtsdatum = dateTime;
            MannschaftId = mannschaftId;
            Gehalt = gehalt;
        }
        public override void Asign(IPerson person)
        {
            base.Asign(person);
            Gehalt = (person as Trainer).Gehalt;
        }
    }
}
