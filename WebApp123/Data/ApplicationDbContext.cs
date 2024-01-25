using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp123.Models;

namespace WebApp123.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Person> Person { get; set; }

        public DbSet<Pet> Pet { get; set; }

        public DbSet<Vaccine> Vaccine { get; set; }
    }
}
