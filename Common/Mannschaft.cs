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
        [ForeignKey("PersonId")]
        [NotMapped]
        public List<Person> Personen { get; set; }
        public string Name { get; set; }
    }
}
