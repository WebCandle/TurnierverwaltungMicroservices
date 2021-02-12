#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      27.01.2021
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        void Asign(IMannschaft mannschaft);
    }
}
