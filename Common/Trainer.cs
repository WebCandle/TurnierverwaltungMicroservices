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
    }
}
