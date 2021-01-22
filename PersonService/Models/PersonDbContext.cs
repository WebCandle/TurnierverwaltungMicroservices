using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using Common;

namespace PersonService.Models
{
    public class PersonDbContext:DbContext
    {
        public DbSet<Person> Personen { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=person_service_db;user=root;password=");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Spieler>();
            builder.Entity<Trainer>();

            base.OnModelCreating(builder);
        }
    }
}
