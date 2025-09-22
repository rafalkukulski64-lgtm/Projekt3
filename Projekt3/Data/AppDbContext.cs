using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt3.Models;

namespace Projekt3.Data
{
    
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pokoj> Pokoje { get; set; }
        public DbSet<PunktLokacji> PunktyLokacji { get; set; }
    }
}
