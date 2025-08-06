using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using simulation.lagerverwaltung2.Models;



namespace simulation.lagerverwaltung2.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lager> Lager { get; set; }
        public DbSet<Kategorie> Kategorie { get; set; }
        public DbSet<Produkt> Produkte { get; set; }
    }

}
