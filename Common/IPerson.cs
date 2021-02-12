#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      12.01.2021
#endregion

using System;
using System.ComponentModel.DataAnnotations;

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
