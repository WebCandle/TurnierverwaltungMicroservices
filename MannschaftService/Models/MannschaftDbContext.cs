#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      27.01.2021
#endregion

using Microsoft.EntityFrameworkCore;
using Common;

namespace MannschaftService.Models
{
    public class MannschaftDbContext:DbContext
    {
        public DbSet<Mannschaft> Mannschaften { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=mannschaft_service_db;user=root;password=");
        }
    }
}
