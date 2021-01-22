using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common
{
    public interface IMannschaft
    {
        [Key]
        int MannschaftId { get; set; }
        [NotMapped]
        List<IPerson> Personen { get; set; }
        string Name { get; set; }
        DateTime Gruendungsdatum { get; set; }
    }
}
