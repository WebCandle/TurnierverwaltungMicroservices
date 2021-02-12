#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      02.02.2021
#endregion

using Common;
using Microsoft.EntityFrameworkCore;

namespace IdentityService
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=user_service_db;user=root;password=");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}
