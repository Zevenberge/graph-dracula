using System;
using Dracula.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dracula.Repository
{
    public class DraculaDbContext : DbContext
    {
        public DraculaDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Casting>()
                .Property(c => c.Role)
                .HasConversion(r => r.ToString(), t => (Role)Enum.Parse(typeof(Role), t));
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actor { get; set; }
        public DbSet<Casting> Casting { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Film> Film { get; set; }
    }
}