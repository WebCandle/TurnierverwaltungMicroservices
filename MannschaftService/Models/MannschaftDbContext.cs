using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
