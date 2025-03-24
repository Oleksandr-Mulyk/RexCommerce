using Microsoft.EntityFrameworkCore;

namespace RexCommerce.CatalogGrpcService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        DbSet<Product> Products { get; set; }

        DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasMany("_categories").WithMany("_products");
        }
    }
}
