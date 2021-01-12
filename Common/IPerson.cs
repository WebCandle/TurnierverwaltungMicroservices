using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        void Asign(IPerson person);
    }
}
