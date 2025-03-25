using Microsoft.EntityFrameworkCore;

namespace RexCommerce.RepositoryLibrary.Tests
{
    public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
    {
        public DbSet<TestModel1> TestDbSet1 { get; set; }

        public DbSet<TestModel2> TestDbSet2 { get; set; }
    }
}
