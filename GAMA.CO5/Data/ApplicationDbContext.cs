using GAMA.CO5.Models;
using Microsoft.EntityFrameworkCore;

namespace GAMA.CO5.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}