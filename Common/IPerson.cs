using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common
{
    public interface IPerson
    {
        [Key]
        int PersonId { get; set; }
        string Name { get; set; }
        string  Nachname { get; set; }
        DateTime Geburtsdatum { get; set; }
        int MannschaftId { get; set; }
        IMannschaft Mannschaft { get; set; }
    }
}
