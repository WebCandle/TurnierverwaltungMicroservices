#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      04.02.2021
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    public class Mannschaft: IMannschaft
    {
        [Key]
        public int MannschaftId { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<IPerson> Personen { get; set; }
        public DateTime Gruendungsdatum { get; set; }

        public Mannschaft()
        {
            MannschaftId = 0;
            Name = string.Empty;
            Personen = null;
            Gruendungsdatum = DateTime.Now;
        }

        public Mannschaft(string name, DateTime gruendungsdatum)
        {
            Name = name;
            Gruendungsdatum = gruendungsdatum;
        }

        public void Asign(IMannschaft mannschaft)
        {
            Name = mannschaft.Name;
            Gruendungsdatum = mannschaft.Gruendungsdatum;
        }

    }
}
