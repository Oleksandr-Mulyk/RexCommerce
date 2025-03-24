using RexCommerce.RepositoryLibrary;

namespace RexCommerce.CatalogGrpcService.Data
{
    public class CategoryRepository(ApplicationDbContext applicationDbContext) :
        DbContextRepository<ICategory, Category>(applicationDbContext.Categories, applicationDbContext),
        IRepository<ICategory>
    {
    }
}
