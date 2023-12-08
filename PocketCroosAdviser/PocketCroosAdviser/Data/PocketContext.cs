using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PocketCroosAdviser.Models;

namespace PocketCroosAdviser.Data
{

    public sealed class PocketContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<BlFilm> BlFilms { get; set; }
        public DbSet<Ganres> Ganres { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConfigurationManager.AppSettings["ConnectionString"]);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine(ConfigurationManager.AppSettings["ConnectionString"]);
            modelBuilder.Entity<Ganres>().HasData(
                new Ganres { Id = 1, Name = "драма", Picked = false},
                new Ganres { Id = 2, Name = "комедия", Picked = false },
                new Ganres { Id = 3, Name = "триллер", Picked = false },
                new Ganres { Id = 4, Name = "ужасы", Picked = false },
                new Ganres { Id = 5, Name = "боевик", Picked = false }
            );
        }

    }

}