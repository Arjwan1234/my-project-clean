using GAMA.CO5.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GAMA.CO5.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<HomeHero> HomeHeroes { get; set; }
        public DbSet<HomeAbout> HomeAbouts { get; set; }
        public DbSet<HomeNews> HomeNews { get; set; }
    }
}