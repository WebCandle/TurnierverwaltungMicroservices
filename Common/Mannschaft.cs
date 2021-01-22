using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        public void Asign(IMannschaft mannschaft)
        {
            Name = mannschaft.Name;
            Gruendungsdatum = mannschaft.Gruendungsdatum;
        }
    }
}
