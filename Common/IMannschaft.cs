using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common
{
    public interface IMannschaft
    {
        [Key]
        int MannschaftId { get; set; }
        IEnumerable<IPerson> Personen { get; set; }
        string Name { get; set; }

    }
}
